#include "pch.h"
#include <cmath>

extern "C" {
	__declspec(dllexport)void encode(int* f0, int w, int h, int* g0)
	{
		for (int i = 0; i < w * h * 3; i += 3) {
			if (f0[i] == f0[i + 1] && f0[i] == f0[i + 2])
				g0[i / 3] = f0[i];
			else
				g0[i / 3] = (int)(0.299 * f0[i] + 0.587 * f0[i + 1] + 0.114 * f0[i + 2]);
		}
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
		double c = a / 100.0+1;
		for (int i = 0; i < w * h; i++) {
			g0[i] = c * (f0[i] - 128) + 128 + b;
			if (g0[i] > 255)g0[i] = 255;
			if (g0[i] < 0)g0[i] = 0;
		}
	}
	__declspec(dllexport)void Histograms(int* f0, int w, int h, double* c0) {
		for (int i = 0; i < 256; i++)
			c0[i] = 0;

		for (int i = 0; i < w * h; i++) {
			c0[f0[i]]++;
		}
	}
	__declspec(dllexport)void Histograms_Equalization(int* f0, int w, int h,int*g0, double* c0, double* k0) {
		k0[0] = c0[0];
		for (int j = 1; j < 256; j++) {
			k0[j] = k0[j - 1] + c0[j];
		}
		for (int j = 0; j < 256; j++) {
			k0[j] = k0[j] * 255 / (w * h);
			k0[j] = round(k0[j]);
		}
		for (int i = 0; i < w * h; i++) {
			for (int j = 0; j < 256; j++) {
				if (f0[i] == j)
					g0[i] = k0[j];
			}
		}
		for (int i = 0; i < 256; i++)
			c0[i] = 0;

		for (int i = 0; i < w * h; i++) {
			c0[g0[i]]++;
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
	__declspec(dllexport)void average_filter(int* f0, int w, int h, int* g0, int* k0, double* m)
	{
		for (int i = 1; i < h; i++) {
			for (int j = 1; j < w; j++) {
				k0[j + i * h] = f0[(j - 1) + (i - 1) * h];
			}
		}
		for (int k = 0; k < h; k++)
		{
			for (int p = 0; p < w; p++)
			{
				double o = 0;
				for (int i = 0; i < 3; i++) {
					for (int j = 0; j < 3; j++) {
						int u = j + p;
						int b = i + k;
						o += k0[u + b * h] * m[j + i * 3];
					}
				}
				g0[p + k * h] = round(o);
			}
		}
	}
	__declspec(dllexport)void gaussian_filter(int* f0, int w, int h, int* g0, int* k0)
	{
		for (int i = 1; i < h; i++) {
			for (int j = 1; j < w; j++) {
				k0[j + i * h] = f0[(j - 1) + (i - 1) * h];
			}
		}

		double m[9] = { 0.0625,0.125,0.0625,0.125,0.25,0.125,0.0625,0.125,0.0625 };

		for (int k = 0; k < h; k++)
		{
			for (int p = 0; p < w; p++)
			{
				double o = 0;
				for (int i = 0; i < 3; i++) {
					for (int j = 0; j < 3; j++) {
						int u = j + p;
						int b = i + k;
						o += k0[u + b * h] * m[j + i * 3];
					}
				}
				g0[p + k * h] = round(o);

			}
		}
	}
}
