﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Picture
{
    public partial class Picture : Form
    {

        private string curFileName;
        private System.Drawing.Bitmap curBitmap;

        public Picture()
        {
            InitializeComponent();
        }

        private void open_Click(object sender, EventArgs e)
        {
            OpenFileDialog opnDlg = new OpenFileDialog();
            opnDlg.Filter = "位图(PNG, JPEG)|*.png;*.jpg";
            opnDlg.Title = "打开图像文件";
            opnDlg.ShowHelp = true;

            if (opnDlg.ShowDialog() == DialogResult.OK)
            {
                curFileName = opnDlg.FileName;
                try
                {
                    curBitmap = (Bitmap)Image.FromFile(curFileName);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }

            Invalidate();
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (curBitmap == null)
            {
                return;
            }

            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Title = "保存为";
            saveDlg.OverwritePrompt = true;
            saveDlg.Filter = "PNG|*.png|JPG|*.jpg";
            saveDlg.ShowHelp = true;

            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveDlg.FileName;
                string strFileExtn = fileName.Remove(0, fileName.Length - 3);

                switch (strFileExtn)
                {
                    case "png":
                        curBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    case "jpg":
                        curBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    default:
                        break;
                }
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Picture_Load(object sender, EventArgs e)
        {

        }

        private void Picture_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (curBitmap != null)
            {
                this.Width = Math.Max(200 + curBitmap.Width, 800);
                this.Height = Math.Max(80 + curBitmap.Height, 600);
                g.DrawImage(curBitmap, 160, 20, curBitmap.Width, curBitmap.Height);

            }
        }

        private void pixel_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                Color curColor;
                int ret;

                for (int i = 0; i < curBitmap.Width; i++)
                {
                    for (int j = 0; j < curBitmap.Height; j++)
                    {
                        curColor = curBitmap.GetPixel(i, j);
                        ret = (int)(curColor.R * 0.299 + curColor.G * 0.587 + curColor.B * 0.114);
                        curBitmap.SetPixel(i, j, Color.FromArgb(curColor.A, ret, ret, ret));
                    }
                }

                Invalidate();
            }
        }

        private void memory_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                System.Drawing.Imaging.BitmapData bmpData = curBitmap.LockBits(rect,
                    System.Drawing.Imaging.ImageLockMode.ReadWrite,
                    curBitmap.PixelFormat);

                int pixelSize = 0;
                switch (bmpData.PixelFormat)
                {
                    case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
                        pixelSize = 3;
                        break;
                    case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
                        pixelSize = 4;
                        break;
                    default:
                        break;
                }

                IntPtr ptr = bmpData.Scan0;

                int bytes = bmpData.Stride * bmpData.Height;
                byte[] rgbvalues = new byte[bytes];

                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbvalues, 0, bytes);

                double colorTemp = 0;

                for (int i = 0; i < bmpData.Height; i++)
                {
                    for (int j = 0; j < bmpData.Width * pixelSize; j += pixelSize)
                    {
                        colorTemp = rgbvalues[i * bmpData.Stride + j + 2] * 0.299
                            + rgbvalues[i * bmpData.Stride + j + 1] * 0.587
                            + rgbvalues[i * bmpData.Stride + j] * 0.114;
                        rgbvalues[i * bmpData.Stride + j + 2]
                            = rgbvalues[i * bmpData.Stride + j + 1]
                            = rgbvalues[i * bmpData.Stride + j]
                            = (byte)colorTemp;
                    }
                }

                /*for (int i = 0; i < rgbvalues.Length; i += pixelSize)
                {
                    colorTemp = rgbvalues[i + 2] * 0.299 +
                        rgbvalues[i + 1] * 0.587 + rgbvalues[i] * 0.114;
                    rgbvalues[i] = rgbvalues[i + 1] = rgbvalues[i + 2] = (byte)colorTemp;
                }*/

                System.Runtime.InteropServices.Marshal.Copy(rgbvalues, 0, ptr, bytes);
                curBitmap.UnlockBits(bmpData);

                Invalidate();
            }
        }

        private void zoom_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                Zoom zoomForm = new Zoom();
                zoomForm.setPixel(curBitmap.Width, curBitmap.Height);

                if (zoomForm.ShowDialog() == DialogResult.OK)
                {
                    //内存法, 老办法, 处理的是8位灰度图
                    Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                    System.Drawing.Imaging.BitmapData bmpData
                        = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                    IntPtr ptr = bmpData.Scan0;

                    //图像总像素
                    int bytes = curBitmap.Width * curBitmap.Height;
                    int[] grayValues = new int[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                    //得到处理后图像的横向&纵向像素, 并建立相应的数组
                    int widthAfter = Convert.ToInt32(zoomForm.GetXZoom);
                    int heightAfter = Convert.ToInt32(zoomForm.GetYZoom);
                    int bytesAfter = widthAfter * heightAfter;
                    int[] newArray = new int[bytesAfter];

                    //用来暂时记录与像素(i, j)相对应的像素, 在原图像中的位置, 可能不是整数
                    double iTemp = 0;
                    double jTemp = 0;

                    if (zoomForm.GetNearOrBil == true)
                    {
                        //真正表示(i, j)映射的像素, 由double四舍五入得到
                        int iBefore = 0;
                        int jBefore = 0;

                        //最邻近插值法
                        for (int i = 0; i < heightAfter; i++)
                        {
                            for (int j = 0; j < widthAfter; j++)
                            {
                                //线性映射来计算出(i, j)对应的(iTemp, jTemp)
                                iTemp = (double)i / heightAfter * curBitmap.Height;
                                jTemp = (double)j / widthAfter * curBitmap.Width;

                                //四舍五入得到最终值
                                iBefore = (int)(iTemp + 0.5);
                                jBefore = (int)(jTemp + 0.5);

                                //输出图像像素值
                                if (jBefore < 0 || jBefore >= curBitmap.Width
                                    || iBefore < 0 || iBefore >= curBitmap.Height)
                                {
                                    //空白部分用白色代替
                                    newArray[i * widthAfter + j] = -1;
                                }
                                else
                                {
                                    newArray[i * widthAfter + j] =
                                        grayValues[iBefore * curBitmap.Width + jBefore];
                                }
                            }
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("这里是双线性哟");

                        //用来计数iTemp和jTemp的小数部分, 对应公式中的P和Q
                        double p = 0;
                        double q = 0;

                        //用来计数iTemphejTemp的整数部分, 以计算周围四点
                        int iBefore = 0;
                        int jBefore = 0;

                        //将int类型分解成rgb三个Byte
                        byte r = 0, g = 0, b = 0;

                        //将rgb合成最终结果;
                        int result = 0;

                        //双线性插值
                        for (int i = 0; i < heightAfter; i++)
                        {
                            for (int j = 0; j < widthAfter; j++)
                            {
                                //线性映射来计算出(i, j)对应的(iTemp, jTemp)
                                iTemp = (double)i / heightAfter * curBitmap.Height;
                                jTemp = (double)j / widthAfter * curBitmap.Width;

                                //计算整数部分
                                iBefore = (int)iTemp;
                                jBefore = (int)jTemp;

                                //计算小数部分
                                p = iTemp - (double)iBefore;
                                q = jTemp - (double)jBefore;

                                if (jBefore < 0 || jBefore >= curBitmap.Width - 1 || iBefore < 0 || iBefore >= curBitmap.Height - 1)
                                {
                                    //空白部分用白色代替
                                    newArray[i * widthAfter + j] = -1;
                                }
                                else
                                {
                                    //将四角四个像素作为参数输入, 即可双线性运算
                                    //a--b
                                    //|  |
                                    //c--d
                                    r = bilinear(p, q, getRed(grayValues[iBefore * curBitmap.Width + jBefore]),
                                        getRed(grayValues[iBefore * curBitmap.Width + jBefore + 1]),
                                        getRed(grayValues[(iBefore + 1) * curBitmap.Width + jBefore]),
                                        getRed(grayValues[(iBefore + 1) * curBitmap.Width + jBefore + 1]));
                                    g = bilinear(p, q, getGreen(grayValues[iBefore * curBitmap.Width + jBefore]),
                                        getGreen(grayValues[iBefore * curBitmap.Width + jBefore + 1]),
                                        getGreen(grayValues[(iBefore + 1) * curBitmap.Width + jBefore]),
                                        getGreen(grayValues[(iBefore + 1) * curBitmap.Width + jBefore + 1]));
                                    b = bilinear(p, q, getBlue(grayValues[iBefore * curBitmap.Width + jBefore]),
                                        getBlue(grayValues[iBefore * curBitmap.Width + jBefore + 1]),
                                        getBlue(grayValues[(iBefore + 1) * curBitmap.Width + jBefore]),
                                        getBlue(grayValues[(iBefore + 1) * curBitmap.Width + jBefore + 1]));
                                    result = 0xff;
                                    result = (result << 8) + g;
                                    result = (result << 8) + r;
                                    result = (result << 8) + b;
                                    newArray[i * widthAfter + j] = result;
                                }
                            }
                        }
                    }

                    curBitmap.UnlockBits(bmpData);

                    //新建一个bitmap, 并更改调色板
                    System.Drawing.Bitmap newBitmap = new Bitmap(widthAfter, heightAfter, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                    /*System.Drawing.Imaging.ColorPalette pal = newBitmap.Palette;
                    System.Console.WriteLine(pal.Entries.Length);
                    for (int i = 0; i < pal.Entries.Length; i++)
                    {
                        pal.Entries[i] = Color.FromArgb(255, i, i, i);
                    }
                    newBitmap.Palette = pal;*/

                    //新建一个大小匹配的矩形并锁定newBitmap的像素
                    Rectangle newRect = new Rectangle(0, 0, newBitmap.Width, newBitmap.Height);
                    System.Drawing.Imaging.BitmapData newData
                        = newBitmap.LockBits(newRect, System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);


                    //找到newData头指针, 将数组数据copy进来
                    IntPtr newPtr = newData.Scan0;
                    System.Runtime.InteropServices.Marshal.Copy(newArray, 0, newPtr, bytesAfter);

                    //解锁
                    newBitmap.UnlockBits(newData);

                    //将curBitmap替换掉
                    curBitmap = newBitmap;

                }

                Invalidate();
            }
        }

        //a--b
        //|  |
        //c--d
        private byte bilinear(double p, double q, byte a, byte b, byte c, byte d)
        {
            return (byte)((1 - p) * ((1 - q) * a + q * b) 
                + p * ((1 - q) * c + q * d));
        }

        private byte getRed(int n)
        {
            return (byte)((n & 0x0000ff00) >> 8);
        }

        private byte getGreen(int n)
        {
            //System.Console.WriteLine("{0:x0}", ((n & 0x00ff0000) >> 16));
            return (byte)((n & 0x00ff0000) >> 16);
        }

        private byte getBlue(int n)
        {
            //System.Console.WriteLine("{0:x0}", n);
            return (byte)(n & 0x0000ff);
        }







    }
}
