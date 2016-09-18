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
    public partial class ScaleProcess
    {
        private System.Drawing.Bitmap curBitmap;  //需要输入
        private System.Drawing.Imaging.BitmapData bmpData;
        private int[] resourceArgb = null;

        private System.Drawing.Bitmap resultBitmap;
        private System.Drawing.Imaging.BitmapData resultData;
        private int[] resultArgb = null;

        private int resultWidth = 0, resultHeight = 0;  //需要输入

        private delegate void ProcessEventHandle();
        private event ProcessEventHandle ProcessToArgbArray;

        private void Process()
        {
            if (curBitmap != null)
            {
                //将输入curBitmap的数据copy进resourceArgb数组里面
                bmpData = curBitmap.LockBits(
                    new Rectangle(0, 0, curBitmap.Width, curBitmap.Height),
                    System.Drawing.Imaging.ImageLockMode.ReadWrite,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                resourceArgb = new int[bmpData.Height * bmpData.Width];

                System.Runtime.InteropServices.Marshal.Copy(
                    bmpData.Scan0, resourceArgb, 0, bmpData.Height * bmpData.Width);
                curBitmap.UnlockBits(bmpData);

                //给resulArgb合适空间以存贮图像数据
                resultArgb = new int[resultWidth * resultHeight];

                ProcessToArgbArray();

                //再将resultArgb数组内的数据copy回resultBitmap
                resultBitmap = new Bitmap(resultWidth, resultHeight,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                resultData = resultBitmap.LockBits(
                    new Rectangle(0, 0, resultBitmap.Width, resultBitmap.Height),
                    System.Drawing.Imaging.ImageLockMode.ReadWrite,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                System.Runtime.InteropServices.Marshal.Copy(
                    resultArgb, 0, resultData.Scan0, resultData.Width * resultData.Height);
                resultBitmap.UnlockBits(resultData);
            }
        }

        private void nearestProcess()
        {
            //用来暂时记录与像素(i, j)相对应的像素, 在原图像中的位置, 可能不是整数
            double iTemp = 0;
            double jTemp = 0;

            //真正表示(i, j)映射的像素, 由double四舍五入得到
            int iResource = 0;
            int jResource = 0;

            for (int i = 0; i < resultHeight; i++)
            {
                for (int j = 0; j < resultWidth; j++)
                {
                    //线性映射来计算出(i, j)对应的(iTemp, jTemp)
                    iTemp = (double)i / resultHeight * curBitmap.Height;
                    jTemp = (double)j / resultWidth * curBitmap.Width;

                    //四舍五入得到最终值
                    iResource = (int)(iTemp + 0.5);
                    jResource = (int)(jTemp + 0.5);

                    //输出图像像素值
                    if (jResource < 0 || jResource >= curBitmap.Width
                        || iResource < 0 || iResource >= curBitmap.Height)
                    {
                        //空白部分用白色代替
                        resultArgb[i * resultWidth + j] = -1;
                    }
                    else
                    {
                        resultArgb[i * resultWidth + j] =
                            resourceArgb[iResource * curBitmap.Width + jResource];
                    }
                }
            }
        }

        private void bilinearProcess()
        {
            //用来暂时记录与像素(i, j)相对应的像素, 在原图像中的位置, 可能不是整数
            double iTemp = 0;
            double jTemp = 0;

            //真正表示(i, j)映射的像素, 由double取整数得到
            int iResource = 0;
            int jResource = 0;

            //iTemp和jTemp中的小数部分, 公式中用到
            double p = 0, q = 0;

            for (int i = 0; i < resultHeight; i++)
            {
                for (int j = 0; j < resultWidth; j++)
                {
                    //线性映射来计算出(i, j)对应的(iTemp, jTemp)
                    iTemp = (double)i / resultHeight * curBitmap.Height;
                    jTemp = (double)j / resultWidth * curBitmap.Width;

                    //得到整数部分
                    iResource = (int)iTemp;
                    jResource = (int)jTemp;

                    //得到小数部分
                    p = iTemp - (double)iResource;
                    q = jTemp - (double)jResource;

                    //输出图像像素值
                    if (jResource < 0 || jResource >= curBitmap.Width
                        || iResource < 0 || iResource >= curBitmap.Height)
                    {
                        //空白部分用白色代替
                        resultArgb[i * resultWidth + j] = -1;
                    }
                    else if (iResource == curBitmap.Height - 1 || jResource == curBitmap.Width - 1)
                    {
                        resultArgb[i * resultWidth + j] = resourceArgb[iResource * curBitmap.Width + jResource];
                    }
                    else
                    {
                        //对Argb三个byte分别双线性运算
                        Byte[] result = new byte[4];

                        //将四角四个像素转为Byte数组
                        //a--b
                        //|  |
                        //c--d
                        Byte[] a = BitConverter.GetBytes(
                            resourceArgb[iResource * curBitmap.Width + jResource]);
                        Byte[] b = BitConverter.GetBytes(
                            resourceArgb[iResource * curBitmap.Width + jResource + 1]);
                        Byte[] c = BitConverter.GetBytes(
                            resourceArgb[(iResource + 1) * curBitmap.Width + jResource]);
                        Byte[] d = BitConverter.GetBytes(
                            resourceArgb[(iResource + 1) * curBitmap.Width + jResource + 1]);

                        //进行双线性运算
                        for (int k = 0; k < 4; k++)
                        {
                            result[k] = (byte)((1 - p) * ((1 - q) * a[k] + q * b[k])
                                + p * ((1 - q) * c[k] + q * d[k]));
                        }

                        resultArgb[i * resultWidth + j] = BitConverter.ToInt32(result, 0);
                    }
                }
            }
        }

        public bool NearestInter(ref System.Drawing.Bitmap bitmap, int width, int height)
        {
            if (bitmap == null) return false;

            this.curBitmap = bitmap;

            this.resultWidth = width;
            this.resultHeight = height;

            ProcessToArgbArray = null;
            ProcessToArgbArray += nearestProcess;

            Process();
            bitmap = resultBitmap;
            return true;
        }

        public bool BilinearInter(ref System.Drawing.Bitmap bitmap, int width, int height)
        {
            if (bitmap == null) return false;

            this.curBitmap = bitmap;

            this.resultWidth = width;
            this.resultHeight = height;

            ProcessToArgbArray = null;
            ProcessToArgbArray += bilinearProcess;

            Process();
            bitmap = resultBitmap;
            return true;
        }
    }
}
