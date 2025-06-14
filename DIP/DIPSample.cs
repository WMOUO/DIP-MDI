﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace DIP
{
    public partial class DIPSample : Form
    {
        public DIPSample()
        {
            InitializeComponent();
        }

        [DllImport("B11223210.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void encode(int* f0, int w, int h, int* g0);
        [DllImport("B11223210.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void mosaic(int* f0, int w, int h, int* g0, int a, int b, int x);
        [DllImport("B11223210.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void turn_left90(int* f0, int w, int h, int* g0);
        [DllImport("B11223210.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void turn_right90(int* f0, int w, int h, int* g0);

        [DllImport("B11223210.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void turn_180(int* f0, int w, int h, int* g0);
        [DllImport("B11223210.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void turn_horizontal(int* f0, int w, int h, int* g0);
        [DllImport("B11223210.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void turn_vertical(int* f0, int w, int h, int* g0);
        [DllImport("B11223210.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void Histograms(int* f0, int w, int h, double* c0);
        [DllImport("B11223210.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void Histograms_Equalization(int* f0, int w, int h, int* g0, double* c0, double* k0);
        [DllImport("B11223210.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void negative(int* f0, int w, int h, int* g0);
        [DllImport("B11223210.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void average_filter(int* f0, int w, int h, int* g0, int* k0, double* m);
        [DllImport("B11223210.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void gaussian_filter(int* f0, int w, int h, int* g0, int* k0);
        [DllImport("B11223210.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void Otsu_cut(int* f0, int w, int h, int* g0, int* n0);
        [DllImport("B11223210.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void median_filter(int* f0, int w, int h, int* g0, int* k0);
        [DllImport("B11223210.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void sobel_filter(int* f0, int w, int h, int* g0, int* k0);
        [DllImport("B11223210.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void prewitt_filter(int* f0, int w, int h, int* g0, int* k0);
        [DllImport("B11223210.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void connected_component(int* f0, int w, int h, int* g0,int* num);

        Bitmap NpBitmap;
        int[] f;
        int[] g;
        int w, h;

        private void DIPSample_Load(object sender, EventArgs e)
        {
            this.IsMdiContainer = true;
            this.WindowState = FormWindowState.Maximized;
            this.toolStripStatusLabel1.Text = "";
            this.toolStripStatusLabel2.Text = "";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oFileDlg.CheckFileExists = true;
            oFileDlg.CheckPathExists = true;
            oFileDlg.Title = "Open File - DIP Sample";
            oFileDlg.ValidateNames = true;
            oFileDlg.Filter = "bmp files (*.bmp)|*.bmp";
            oFileDlg.FileName = "";

            if (oFileDlg.ShowDialog() == DialogResult.OK)
            {
                MSForm childForm = new MSForm();
                childForm.MdiParent = this;
                childForm.pf1 = toolStripStatusLabel1;
                childForm.pf2 = toolStripStatusLabel2;
                NpBitmap = bmp_read(oFileDlg);
                childForm.pBitmap = NpBitmap;
                w = NpBitmap.Width;
                h = NpBitmap.Height;
                childForm.Show();
            }
        }

        private Bitmap bmp_read(OpenFileDialog oFileDlg)
        {
            Bitmap pBitmap;
            string fileloc = oFileDlg.FileName;
            pBitmap = new Bitmap(fileloc);
            w = pBitmap.Width;
            h = pBitmap.Height;
            return pBitmap;
        }

        public int[] bmp2array(Bitmap myBitmap)
        {
            int[] ImgData = new int[myBitmap.Width * myBitmap.Height];
            BitmapData byteArray = myBitmap.LockBits(new Rectangle(0, 0, myBitmap.Width, myBitmap.Height),
                                          ImageLockMode.ReadWrite,
                                          myBitmap.PixelFormat);
            int ByteOfSkip = byteArray.Stride - byteArray.Width * (int)(byteArray.Stride / myBitmap.Width);
            unsafe
            {
                byte* imgPtr = (byte*)(byteArray.Scan0);
                for (int y = 0; y < byteArray.Height; y++)
                {
                    for (int x = 0; x < byteArray.Width; x++)
                    {
                        ImgData[x + byteArray.Height * y] = (int)*(imgPtr);
                        //ImgData[x, y] = (int)*(imgPtr + 1);
                        //ImgData[x, y] = (int)*(imgPtr + 2);
                        imgPtr += (int)(byteArray.Stride / myBitmap.Width);
                    }
                    imgPtr += ByteOfSkip;
                }
            }
            myBitmap.UnlockBits(byteArray);
            return ImgData;
        }

        public Bitmap array2bmp(int[] ImgData)
        {
            int Width = (int)Math.Sqrt(ImgData.GetLength(0));
            int Height = (int)Math.Sqrt(ImgData.GetLength(0));
            Bitmap myBitmap = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
            BitmapData byteArray = myBitmap.LockBits(new Rectangle(0, 0, Width, Height),
                                           ImageLockMode.WriteOnly,
                                           PixelFormat.Format24bppRgb);
            //Padding bytes的長度
            int ByteOfSkip = byteArray.Stride - myBitmap.Width * 3;
            unsafe
            {                                   // 指標取出影像資料
                byte* imgPtr = (byte*)byteArray.Scan0;
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        *imgPtr = (byte)ImgData[x + Height * y];       //B
                        *(imgPtr + 1) = (byte)ImgData[x + Height * y]; //G 
                        *(imgPtr + 2) = (byte)ImgData[x + Height * y]; //R  
                        imgPtr += 3;
                    }
                    imgPtr += ByteOfSkip; // 跳過Padding bytes
                }
            }
            myBitmap.UnlockBits(byteArray);
            return myBitmap;
        }

        private void rGBtoGrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g;
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = new int[w * h * 3];
                    g = new int[w * h];
                    for (int i = 0; i < w; i++)
                    {
                        for (int j = 0; j < h; j++)
                        {
                            Color c = cF.pBitmap.GetPixel(i, j);
                            int idx = (i + j * w) * 3;
                            f[idx] = c.R; f[idx + 1] = c.G; f[idx + 2] = c.B;
                        }
                    }
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g)
                        {
                            encode(f0, w, h, g0);
                        }
                    }
                    NpBitmap = array2bmp(g);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = toolStripStatusLabel1;
            childForm.pf2 = toolStripStatusLabel2;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void iPToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 馬賽克ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g;
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    g = new int[w * h];
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g)
                        {
                            mosaic(f0, w, h, g0, 100, 200, 4);
                        }
                    }
                    NpBitmap = array2bmp(g);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = toolStripStatusLabel1;
            childForm.pf2 = toolStripStatusLabel2;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void 位元切面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BTForm bt = new BTForm();
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    bt.bitmap = cF.pBitmap;
                    break;
                }
            }
            bt.tssl_1 = toolStripStatusLabel1;
            bt.tssl_2 = toolStripStatusLabel2;
            bt.dip = this;
            bt.Show();
        }

        private void 負片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g;
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    g = new int[w * h];
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g)
                        {
                            negative(f0, w, h, g0);
                        }
                    }
                    NpBitmap = array2bmp(g);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = toolStripStatusLabel1;
            childForm.pf2 = toolStripStatusLabel2;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void 向左旋轉90度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g;
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    g = new int[w * h];
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g)
                        {
                            turn_left90(f0, w, h, g0);
                        }
                    }
                    NpBitmap = array2bmp(g);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = toolStripStatusLabel1;
            childForm.pf2 = toolStripStatusLabel2;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void 向右旋轉90度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g;
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    g = new int[w * h];
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g)
                        {
                            turn_right90(f0, w, h, g0);
                        }
                    }
                    NpBitmap = array2bmp(g);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = toolStripStatusLabel1;
            childForm.pf2 = toolStripStatusLabel2;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void 旋轉180度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g;
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    g = new int[w * h];
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g)
                        {
                            turn_180(f0, w, h, g0);
                        }
                    }
                    NpBitmap = array2bmp(g);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = toolStripStatusLabel1;
            childForm.pf2 = toolStripStatusLabel2;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void 垂直翻轉ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g;
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    g = new int[w * h];
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g)
                        {
                            turn_vertical(f0, w, h, g0);
                        }
                    }
                    NpBitmap = array2bmp(g);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = toolStripStatusLabel1;
            childForm.pf2 = toolStripStatusLabel2;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void 水平翻轉ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g;
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    g = new int[w * h];
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g)
                        {
                            turn_horizontal(f0, w, h, g0);
                        }
                    }
                    NpBitmap = array2bmp(g);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = toolStripStatusLabel1;
            childForm.pf2 = toolStripStatusLabel2;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void 調整亮度和對比ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CBForm cb = new CBForm();
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    cb.bitmap = cF.pBitmap;
                    break;
                }
            }
            cb.tssl_1 = toolStripStatusLabel1;
            cb.tssl_2 = toolStripStatusLabel2;
            cb.dip = this;
            cb.Show();
        }
        //還沒做完
        private void 直方圖ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            double[] c = new double[256];
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (double* c0 = c)
                        {
                            Histograms(f0, w, h, c0);
                        }
                    }
                    break;
                }
            }
            HForm hForm = new HForm();
            hForm.data = c;
            hForm.title = "Histograms";
            hForm.image = NpBitmap;
            hForm.Show();
        }

        private void 直方等化圖ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g = new int[w * h];
            double[] c = new double[256];
            double[] k = new double[256];
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g) fixed (double* c0 = c) fixed (double* k0 = k)
                        {
                            Histograms(f0, w, h, c0);
                            Histograms_Equalization(f0, w, h, g0, c0, k0);
                        }
                    }
                    break;
                }
            }

            HForm hForm = new HForm();
            hForm.data = c;
            hForm.title = "Histograms_Equalization";
            hForm.image = array2bmp(g);
            hForm.Show();
        }

        private void 平均濾波ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g = new int[w * h];
            int[] k = new int[(w + 1) * (h + 1)];
            double[] m = new double[9];
            for (int i = 0; i < 9; i++)
                m[i] = 1.0 / 9;
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g) fixed (int* k0 = k) fixed (double* m0 = m)
                        {
                            average_filter(f0, w, h, g0, k0, m0);
                        }
                    }
                    NpBitmap = array2bmp(g);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = toolStripStatusLabel1;
            childForm.pf2 = toolStripStatusLabel2;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void 高斯濾波ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g = new int[w * h];
            int[] k = new int[(w + 1) * (h + 1)];
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g) fixed (int* k0 = k)
                        {
                            gaussian_filter(f0, w, h, g0, k0);
                        }
                    }
                    NpBitmap = array2bmp(g);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = toolStripStatusLabel1;
            childForm.pf2 = toolStripStatusLabel2;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }


        private void 大小及角度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g;

            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    VForm childForm = new VForm();
                    childForm.MdiParent = this;
                    childForm.pf1 = toolStripStatusLabel1;
                    childForm.pf2 = toolStripStatusLabel2;
                    childForm.pBitmap = cF.pBitmap;
                    childForm.Show();
                }
            }
        }

        private void 中值濾波ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g = new int[w * h];
            int[] k = new int[(w + 1) * (h + 1)];
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g) fixed (int* k0 = k)
                        {
                            median_filter(f0, w, h, g0, k0);
                        }
                    }
                    NpBitmap = array2bmp(g);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = toolStripStatusLabel1;
            childForm.pf2 = toolStripStatusLabel2;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void sobel邊緣偵測ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g = new int[w * h];
            int[] k = new int[(w + 1) * (h + 1)];
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g) fixed (int* k0 = k)
                        {
                            sobel_filter(f0, w, h, g0, k0);
                        }
                    }
                    NpBitmap = array2bmp(g);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = toolStripStatusLabel1;
            childForm.pf2 = toolStripStatusLabel2;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void prewitt邊緣偵測ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g = new int[w * h];
            int[] k = new int[(w + 1) * (h + 1)];
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g) fixed (int* k0 = k)
                        {
                            prewitt_filter(f0, w, h, g0, k0);
                        }
                    }
                    NpBitmap = array2bmp(g);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = toolStripStatusLabel1;
            childForm.pf2 = toolStripStatusLabel2;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void 關於ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Info info = new Info();
            info.Show();
        }

        private void otsu切割ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g = new int[w * h];
            int[] n = new int[1];
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g) fixed (int* n0 = n)
                        {
                            Otsu_cut(f0, w, h, g0, n0);
                        }
                    }
                    NpBitmap = array2bmp(g);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.Text = n[0].ToString();
            childForm.pf1 = toolStripStatusLabel1;
            childForm.pf2 = toolStripStatusLabel2;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void component(object sender, EventArgs e)
        {
            int[] f, g, num = new int[1];
            FCCForm fcc = new FCCForm();
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    f = bmp2array(cF.pBitmap);
                    g = new int[w * h];
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g) fixed (int* n0 = num)
                        {
                            connected_component(f0, w, h, g0, n0);
                        }
                    }
                    NpBitmap = array2bmp(f);
                    fcc.pBitmap = NpBitmap;
                    fcc.bitmap = array2bmp(g);
                    break;
                }
            }   
            fcc.dIP = this;
            fcc.pf1 = toolStripStatusLabel1;           
            fcc.Text = num[0].ToString();
            fcc.Show();
        }
    }
}
