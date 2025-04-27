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
	__declspec(dllexport)void turn(int* f0, int w, int h, int* g0, double theta, int newW, int newH)

	{
		const double pi = 3.14159265358979323846;
		if (fabs(theta) < 1e-6 || fabs(theta - 2 * pi) < 1e-6)
		{
			for (int i = 0; i < w * h; ++i)
				g0[i] = f0[i];
			return;
		}

		int cx = w / 2;
		int cy = h / 2;
		int ncx = newW / 2;
		int ncy = newH / 2;

		double cos_theta = cos(theta);
		double sin_theta = sin(theta);

		for (int y = 0; y < newH; ++y)
		{
			for (int x = 0; x < newW; ++x)
			{
				double srcX = (x - ncx) * cos_theta + (y - ncy) * sin_theta + cx;
				double srcY = -(x - ncx) * sin_theta + (y - ncy) * cos_theta + cy;

				int idx = y * newW + x;

				if (srcX >= 0 && srcX < w - 1 && srcY >= 0 && srcY < h - 1)
				{
					int x0 = (int)floor(srcX);
					int y0 = (int)floor(srcY);
					double dx = srcX - x0;
					double dy = srcY - y0;

					int p00 = f0[y0 * w + x0];
					int p10 = f0[y0 * w + (x0 + 1)];
					int p01 = f0[(y0 + 1) * w + x0];
					int p11 = f0[(y0 + 1) * w + (x0 + 1)];

					double pixel =
						(1 - dx) * (1 - dy) * p00 +
						dx * (1 - dy) * p10 +
						(1 - dx) * dy * p01 +
						dx * dy * p11;

					g0[idx] = (int)(pixel + 0.5);
				}
				else
				{
					g0[idx] = 255;
				}
			}
		}
	}

	__declspec(dllexport) void changeSize(int* f0, int w, int h, int* g0, double scale, int newW, int newH)
	{
		for (int y = 0; y < newH; ++y)
		{
			for (int x = 0; x < newW; ++x)
			{
				double srcX = x / scale;
				double srcY = y / scale;

				int x0 = (int)floor(srcX);
				int y0 = (int)floor(srcY);
				double dx = srcX - x0;
				double dy = srcY - y0;

				int idx = y * newW + x;

				if (x0 >= 0 && x0 + 1 < w && y0 >= 0 && y0 + 1 < h)
				{
					int p00 = f0[y0 * w + x0];
					int p10 = f0[y0 * w + (x0 + 1)];
					int p01 = f0[(y0 + 1) * w + x0];
					int p11 = f0[(y0 + 1) * w + (x0 + 1)];

					double pixel =
						(1 - dx) * (1 - dy) * p00 +
						dx * (1 - dy) * p10 +
						(1 - dx) * dy * p01 +
						dx * dy * p11;

					g0[idx] = (int)(pixel + 0.5);
				}
				else
				{
					g0[idx] = 255;
				}
			}
		}
	}

	__declspec(dllexport)void Otsu_cut(int* f0, int w, int h, int* g0) {
		int gray[256] = { 0 };
		for (int i = 0; i < w * h; i++)
			gray[f0[i]]++;

		double prob[256] = { 0 }, levels[256] = { 0 };
		for (int i = 0; i < 256; i++) {
			prob[i] = (double)gray[i] / (w * h);
			levels[i] = i;
		}

		double max_between_var = 0, min_within_var = 1e9, max_avg_diff = 0;
		int best_thresh_otsu = 0, best_thresh_within = 0, best_thresh_avg = 0;

		for (int t = 1; t < 256; ++t) {
			double w0 = 0, mu0 = 0;
			for (int i = 0; i < t; ++i) {
				w0 += prob[i];
				mu0 += levels[i] * prob[i];
			}
			if (w0 == 0) continue;
			mu0 /= w0;

			double w1 = 1 - w0;
			if (w1 == 0) continue;

			double mu1 = 0;
			for (int i = t; i < 256; ++i) {
				mu1 += levels[i] * prob[i];
			}
			mu1 /= w1;

			double between_var = w0 * w1 * (mu0 - mu1) * (mu0 - mu1);
			if (between_var > max_between_var) {
				max_between_var = between_var;
				best_thresh_otsu = t;
			}

			double sigma0 = 0, sigma1 = 0;
			for (int i = 0; i < t; ++i) {
				sigma0 += prob[i] * (levels[i] - mu0) * (levels[i] - mu0);
			}
			sigma0 /= w0;

			for (int i = t; i < 256; ++i) {
				sigma1 += prob[i] * (levels[i] - mu1) * (levels[i] - mu1);
			}
			sigma1 /= w1;

			double within_var = w0 * sigma0 + w1 * sigma1;
			if (within_var < min_within_var) {
				min_within_var = within_var;
				best_thresh_within = t;
			}

			double avg_diff = abs(mu0 - mu1);
			if (avg_diff > max_avg_diff) {
				max_avg_diff = avg_diff;
				best_thresh_avg = t;
			}
		}

		for (int i = 0; i < w * h; i++)
			g0[i] = (f0[i] < best_thresh_otsu) ? 0 : 255;
	}
}
