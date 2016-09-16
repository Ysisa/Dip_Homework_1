using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picture
{
    public partial class ColorProcess
    {
        private System.Drawing.Bitmap curBitmap;
        private System.Drawing.Imaging.BitmapData bmpData;

        private int degree = 0;
        private double rWeitht = 0, gWeitht = 0, bWeitht = 0;

        private delegate void ProcessEventHandle(ref int color);
        private event ProcessEventHandle ProcessToPixel;

        private void setDegree(ref int color)
        {
            //System.Console.WriteLine("color");
            byte[] Argb = BitConverter.GetBytes(color);
            for (int i = 0; i < 4; i++) setByteDegree(ref Argb[i]);
            color = BitConverter.ToInt32(Argb, 0);
        }

        //degree: 使用多少二进制位来表示颜色
        //level: 颜色有多少级, level = 2^degree
        //例如若颜色分为4级
        //degree = 2
        //level = 4
        //255/(4-1) = 85
        //所以颜色的四个级别为0, 85, 170, 255
        private void setByteDegree(ref byte colorByte)
        {
            if (degree > 0)
            {
                int level = (int)Math.Pow(2, degree);
                colorByte = (byte)((uint)colorByte & (uint)((level - 1) << (8 - degree)));
                colorByte = (byte)(255 / (level - 1) * (colorByte >> (8 - degree)));
            }
        }

        //将颜色按照 gray = r*rWeight + g*gWeitht + b*bWeitht设置成灰色
        private void setGray(ref int color)
        {
            byte[] Argb = BitConverter.GetBytes(color);
            byte gray = (byte)(Argb[2] * rWeitht + Argb[1] * gWeitht + Argb[0] * bWeitht);
            for (int i = 0; i < 3; i++) Argb[i] = gray;
            color = BitConverter.ToInt32(Argb, 0);
        }

        private void Process()
        {
            if (curBitmap != null)
            {
                Rectangle rect = new Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                bmpData = curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                IntPtr ptr = bmpData.Scan0;

                int bytes = bmpData.Height * bmpData.Width;
                int[] rgbValues = new int[bytes];

                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

                for (int i = 0; i < bmpData.Height; i++)
                {
                    for (int j = 0; j < bmpData.Width; j++)
                    {
                        ProcessToPixel(ref rgbValues[i * bmpData.Width + j]);
                    }
                }

                System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
                curBitmap.UnlockBits(bmpData);
            }
        }

        public bool setBitmapGray(ref System.Drawing.Bitmap bitmap, double rWeitht, double gWeitht, double bWeitht)
        {
            if (bitmap == null) return false;

            this.curBitmap = bitmap;
            this.rWeitht = rWeitht;
            this.gWeitht = gWeitht;
            this.bWeitht = bWeitht;

            ProcessToPixel = null;
            ProcessToPixel += setGray;

            Process();
            bitmap = curBitmap;
            return true;
        }

        public bool setBitmapColorDegree(ref System.Drawing.Bitmap bitmap, int degree)
        {
            if (bitmap == null) return false;

            this.curBitmap = bitmap;
            this.degree = degree;

            ProcessToPixel = null;
            ProcessToPixel += setDegree;

            Process();
            bitmap = curBitmap;
            return true;
        }
    }
}
