﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace DIP
{
    public partial class MSForm : Form
    {
        internal Bitmap pBitmap;
        internal ToolStripStatusLabel pf1;
        internal ToolStripStatusLabel pf2;
        int w, h;
        [DllImport("B11223210.dll", CallingConvention = CallingConvention.Cdecl)]

        unsafe public static extern double locationWithLight(int r, int g, int b);
        public MSForm()
        {
            InitializeComponent();
        }

        private void MSForm_Load(object sender, EventArgs e)
        {
            bmp_dip(pBitmap, pictureBox1);
            pf1.Text = "(Width,Height)=(" + pBitmap.Width + "," + pBitmap.Height + ")";
            w = pBitmap.Width;
            h = pBitmap.Height;
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

        private void bmp_dip(Bitmap pBitmap, PictureBox pictureBox1)
        {
            this.Width = pBitmap.Width + (this.Width - this.ClientRectangle.Width);
            this.Height = pBitmap.Height + (this.Height - this.ClientRectangle.Height);
            pictureBox1.Image = pBitmap;
            pictureBox1.Width = pBitmap.Width;
            pictureBox1.Height = pBitmap.Height;
        }

        private void bmp_disp(Bitmap pBitmap, PictureBox pictureBox2)
        {
            pictureBox2.Image = pBitmap;
        }

        private void bmp_write(Bitmap pBitmap, SaveFileDialog sFileDlg)
        {
            sFileDlg.Filter = "Bitmap Image|*.bmp";
            sFileDlg.Title = "Save an Image File";
            sFileDlg.ShowDialog();
            if (sFileDlg.FileName != "")
            {
                pBitmap.Save(sFileDlg.FileName);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int x = Math.Max(0, Math.Min(e.X, pBitmap.Width - 1));
            int y = Math.Max(0, Math.Min(e.Y, pBitmap.Height - 1));

            x = Math.Max(0, Math.Min(x, pBitmap.Width - 1));
            y = Math.Max(0, Math.Min(y, pBitmap.Height - 1));
            Color pixel = pBitmap.GetPixel(x, y);
            pf1.Text = $"(Width,Height) = ({pBitmap.Width},{pBitmap.Height})　";
            pf1.Text += "座標："+"(" + e.X + "," + e.Y + ")";
            double brightness = locationWithLight(pixel.R, pixel.G, pixel.B);
            pf2.Text = "亮度：" + brightness.ToString();
        }

    }
}
