#include "pch.h"
#include <cmath>

extern "C" {
	__declspec(dllexport)void encode(int* f0, int w, int h, int* g0)
	{
		for (int i = 0; i < w * h; i++)
			g0[i] = 255 - f0[i];
	}
	__declspec(dllexport)void mosaic(int* f0, int w, int h, int* g0, int a, int b, int x)
	{
		for (int i = 0; i < w * h; i++)
			g0[i] = f0[i];

		for (int i = a; i < b; i += x)
			for (int j = a; j < b; j += x)
				for (int i0 = 0; i0 < x; i0++)
					for (int j0 = 0; j0 < x; j0++) {
						int y = (j + j0) + (i + i0) * h;
						if (f0[j + i * h] + 50 > 255)
							g0[y] = 255;
						else if (f0[j + i * h] < 0)
							g0[y] = 0;
						else
							g0[y] = f0[j + i * h] + 50;
					}
	}
	__declspec(dllexport)void turn_left90(int* f0, int w, int h, int* g0)
	{
		int u = h - 1;
		for (int i = u; i >= 0; i--)
			for (int j = 0; j < w; j++)
			{
				int y = j + (u - i) * h;
				g0[y] = f0[i + j * h];
			}
	}
	__declspec(dllexport)void turn_right90(int* f0, int w, int h, int* g0)
	{
		int u = w - 1;
		for (int i = 0; i < h; i++)
			for (int j = u; j >= 0; j--)
			{
				int y = (u - j) + i * w;
				g0[y] = f0[i + j * w];
			}
	}
	__declspec(dllexport)void turn_180(int* f0, int w, int h, int* g0)
	{
		int u = w - 1;
		for (int i = 0; i < h; i++)
			for (int j = 0; j < w; j++)
			{
				int y = i + j * w;
				g0[y] = f0[(u - i) + (u - j) * w];
			}
	}
	__declspec(dllexport)void turn_horizontal(int* f0, int w, int h, int* g0)
	{
		int u = w - 1;
		for (int i = 0; i < h; i++)
			for (int j = u; j >= 0; j--) {
				int y = (u - j) + i * h;
				g0[y] = f0[j + i * h];
			}
	}
	__declspec(dllexport)void turn_vertical(int* f0, int w, int h, int* g0)
	{
		int u = h - 1;
		for (int i = u; i >= 0; i--)
			for (int j = 0; j < w; j++) {
				int y = j + (u - i) * h;
				g0[y] = f0[j + i * h];
			}
	}
	__declspec(dllexport)void color_change(int* f0, int w, int h, int* g0, int a, int b) {
		float c = a / 1000;
		for (int i = 0; i < w * h; i++) {
			g0[i] = c * f0[i] + b;
			float u = 0.128 * (a - 1000);
			if (a >= 1000) {
				if (g0[i] >= 256 - u)
					g0[i] = 255;
				else if (g0[i] <= u)
					g0[i] = 0;
			}
		}
	}
	__declspec(dllexport)void Histograms(int* f0, int w, int h, double* c0) {
		for (int i = 0; i < 256; i++)
			c0[i] = 0;

		for (int i = 0; i < w * h; i++) {
			c0[f0[i]]++;
		}
	}
	__declspec(dllexport)void Histograms_Equalization(int* f0, int w, int h, double* c0, double* k0) {
		k0[0] = c0[0];
		for (int j = 1; j < 256; j++) {
			k0[j] = k0[j - 1] + c0[j];
		}
		for (int j = 0; j < 256; j++) {
			k0[j] = k0[j] * 255 / (w * h);
			k0[j] = round(k0[j]);
		}
		for (int i = 0; i < 256; i++) {
			c0[(int)k0[i]] = k0[i];
		}
	}
	__declspec(dllexport)void byte_cut(int* f0, int w, int h, int* g0, int n) {
		for (int i = 0; i < w * h; i++) {
			g0[i] = (f0[i] / n) % 2 * 255;
		}
	}
	__declspec(dllexport)void negative(int* f0, int w, int h, int* g0)
	{
		for (int i = 0; i < w * h; i++)
			g0[i] = 255 - f0[i];
	}
	__declspec(dllexport) double locationWithLight(int r, int g, int b)
	{
		double light = 0.299 * r + 0.587 * g + 0.114 * b;
		return light;
	}
}
