using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DIP
{
    public partial class VForm : Form
    {
        internal Bitmap pBitmap;
        internal ToolStripStatusLabel pf1;
        internal ToolStripStatusLabel pf2;
        int[] f;
        int[] g;
        int w, h;
        Bitmap gBitmap;
        double currentTheta = 0;
        double currentScale = 1.0;
        public VForm()
        {
            InitializeComponent();
            hScrollBar1.Value = 0;
            textBox1.Text = "0";
            hScrollBar2.Value = 10;
            textBox2.Text = "1.0";
        }
        [DllImport("B11223210.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void turn(int* f0, int w, int h, int* g0, double theta, int newW, int newH);

        [DllImport("B11223210.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void changeSize(int* f0, int w, int h, int* g0, double theta, int newW, int newH);
        private void VForm_Load(object sender, EventArgs e)
        {
            if (pBitmap != null)
            {
                pictureBox1.Image = new Bitmap(pBitmap); // 初始顯示
                gBitmap = new Bitmap(pBitmap);
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

        private int[] bmp2array(Bitmap myBitmap)
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

        private static Bitmap array2bmp(int[] ImgData)
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

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            currentTheta = hScrollBar1.Value;
            textBox1.Text = currentTheta.ToString();
            updatePicture();
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            currentScale = hScrollBar2.Value / 10.0;
            textBox2.Text = currentScale.ToString("0.0");
            updatePicture();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int theta)) return;

            if (theta < 0) theta = 0;
            if (theta > 360) theta = 360;

            currentTheta = theta;
            hScrollBar1.Value = theta;
            updatePicture();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox2.Text, out double scale)) return;
            if (scale < 0.5) scale = 0.5;
            if (scale > 2) scale = 2;

            currentScale = scale;
            hScrollBar2.Value = (int)(scale * 10);
            updatePicture();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MSForm childForm = new MSForm();
            childForm.MdiParent = this.MdiParent;
            childForm.pf1 = this.pf1;
            childForm.pf2 = this.pf2;
            childForm.pBitmap = new Bitmap(pictureBox1.Image); ;
            childForm.Show();
            this.Close();
        }

        private void updatePicture()
        {
            if (gBitmap == null) return;

            int w = gBitmap.Width;
            int h = gBitmap.Height;

            int newW = Math.Max(1, (int)(w * currentScale));
            int newH = Math.Max(1, (int)(h * currentScale));

            int[] f = bmp2array(gBitmap);
            int[] g1 = new int[newW * newH];

            // Step 1: 縮放
            unsafe
            {
                fixed (int* f0 = f)
                fixed (int* g0 = g1)
                {
                    changeSize(f0, w, h, g0, currentScale, newW, newH);
                }
            }

            // Step 2: 旋轉
            double theta = currentTheta * Math.PI / 180.0;
            double cos = Math.Abs(Math.Cos(theta));
            double sin = Math.Abs(Math.Sin(theta));
            int rotW = (int)(newW * cos + newH * sin);
            int rotH = (int)(newW * sin + newH * cos);
            int[] g2 = new int[rotW * rotH];

            unsafe
            {
                fixed (int* f0 = g1)
                fixed (int* g0 = g2)
                {
                    turn(f0, newW, newH, g0, theta, rotW, rotH);
                }
            }

            // 顯示結果
            Bitmap result = array2bmp(g2);
            pictureBox1.Image = result;
            pictureBox1.Width = result.Width;
            pictureBox1.Height = result.Height;
        }
    }
}
