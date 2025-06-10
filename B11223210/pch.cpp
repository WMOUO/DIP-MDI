#include "pch.h"
#include <cmath>
#include <algorithm>
#include <vector>
using namespace std;

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
		float c = a / 1000.0;
		for (int i = 0; i < w * h; i++) {
			g0[i] = c * (f0[i] - 128) + 128 + b;
			float u = 0.128 * (a - 1000);
			if (a >= 1000) {
				if (g0[i] >= 256 - u)
					g0[i] = 255;
				else if (g0[i] <= u)
					g0[i] = 0;
			}
			else {
				if (g0[i] <= 128 - u)
					g0[i] = 128;
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

	__declspec(dllexport)void Otsu_cut(int* f0, int w, int h, int* g0, int* n0) {
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

		n0[0] = best_thresh_otsu;

		for (int i = 0; i < w * h; i++)
			g0[i] = (f0[i] < best_thresh_otsu) ? 0 : 255;
	}
	__declspec(dllexport)void median_filter(int* f0, int w, int h, int* g0, int* k0)
	{
		for (int i = 1; i < h; i++) {
			for (int j = 1; j < w; j++) {
				k0[j + i * h] = f0[(j - 1) + (i - 1) * h];
			}
		}
		for (int k = 0; k < h; k++) {
			for (int p = 0; p < w; p++) {
				double window[9];
				int idx = 0;
				for (int i = 0; i < 3; i++) {
					for (int j = 0; j < 3; j++) {
						int u = j + p;
						int b = i + k;
						window[idx++] = k0[u + b * h];
					}
				}
				std::sort(window, window + 9);
				g0[p + k * h] = window[4];
			}
		}
	}
	__declspec(dllexport)void sobel_filter(int* f0, int w, int h, int* g0, int* k0) {
		for (int i = 1; i < h; i++) {
			for (int j = 1; j < w; j++) {
				k0[j + i * h] = f0[(j - 1) + (i - 1) * h];
			}
		}
		double Gx[9] = { -1, 0, 1,
						-2, 0, 2,
						-1, 0, 1 };
		double Gy[9] = { -1, -2, -1,
						 0,  0,  0,
						 1,  2,  1 };

		for (int k = 1; k < h - 1; k++) {
			for (int p = 1; p < w - 1; p++) {
				double sumX = 0;
				double sumY = 0;
				for (int i = 0; i < 3; i++) {
					for (int j = 0; j < 3; j++) {
						int u = p + j - 1;
						int b = k + i - 1;
						sumX += f0[u + b * h] * Gx[j + i * 3];
						sumY += f0[u + b * h] * Gy[j + i * 3];
					}
				}
				double mag = sqrt(sumX * sumX + sumY * sumY);
				if (mag > 255) mag = 255;  // 限制最大值
				g0[p + k * h] = round(mag);
			}
		}
	}
	__declspec(dllexport)void prewitt_filter(int* f0, int w, int h, int* g0, int* k0) {
		for (int i = 1; i < h; i++) {
			for (int j = 1; j < w; j++) {
				k0[j + i * h] = f0[(j - 1) + (i - 1) * h];
			}
		}
		double Gx[9] = { -1, 0, 1,
						-1, 0, 1,
						-1, 0, 1 };
		double Gy[9] = { -1, -1, -1,
						 0,  0,  0,
						 1,  1,  1 };

		for (int k = 0; k < h; k++) {
			for (int p = 0; p < w; p++) {
				double sumX = 0;
				double sumY = 0;
				for (int i = 0; i < 3; i++) {
					for (int j = 0; j < 3; j++) {
						int u = j + p;
						int b = i + k;
						sumX += k0[u + b * h] * Gx[j + i * 3];
						sumY += k0[u + b * h] * Gy[j + i * 3];
					}
				}
				double mag = sqrt(sumX * sumX + sumY * sumY);
				g0[p + k * h] = round(mag);
			}
		}
	}

	__declspec(dllexport)void connected_component(int* f0, int w, int h,int* g0,int* num) {
		typedef struct {
			int x, y;
		} Point;

		Point queue[256*256];
		int front = 0, rear = 0;
		int label = 1;
		int dx[4] = { -1, 1, 0, 0 };
		int dy[4] = { 0, 0, -1, 1 };

		for (int i = 0; i < w * h; i++) {
			g0[i] = 0;
		}
		for (int y = 0; y < h; y++) {
			for (int x = 0; x < w; x++) {
				if (f0[y * w + x] > 0 && g0[y * w + x] == 0) {
					front = rear = 0;
					if (rear < w*h) {
						queue[rear].x = x;
						queue[rear].y = y;
						rear++;
					}
					g0[y * w + x] = label;
					while (!(front == rear)) {
						Point p = queue[front++];
						for (int d = 0; d < 4; ++d) {
							int nx = p.x + dx[d];
							int ny = p.y + dy[d];
							if (nx >= 0 && nx < w && ny >= 0 && ny < h) {
								if (f0[nx + ny*w] == 255 && g0[nx + ny*w] == 0) {
									g0[nx + ny*w] = label;
									if (rear < w*h) {
										queue[rear].x = nx;
										queue[rear].y = ny;
										rear++;
									}
								}
							}
						}
					}
					num[0] = label;
					label++;
				}
			}
		}
	}

}
