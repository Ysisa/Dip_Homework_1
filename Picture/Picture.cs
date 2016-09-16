using System;
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
                System.Console.WriteLine("click");

                ColorProcess processor = new ColorProcess();
                processor.setBitmapGray(ref curBitmap, 0.299, 0.587, 0.114);

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
                    int[] rgbValues = new int[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

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
                                        rgbValues[iBefore * curBitmap.Width + jBefore];
                                }
                            }
                        }
                    }
                    else
                    {
                        //用来计数iTemp和jTemp的小数部分, 对应公式中的P和Q
                        double p = 0;
                        double q = 0;

                        //用来计数iTemphejTemp的整数部分, 以计算周围四点
                        int iBefore = 0;
                        int jBefore = 0;

                        //将int类型分解成Argb四个Byte
                        //双线性插值需要四个点, 为他们各创建一个Byte数组
                        //a--b
                        //|  |
                        //c--d
                        //位置关系如上
                        byte[] Argb = new byte[4];
                        byte[] A = new byte[4];
                        byte[] B = new byte[4];
                        byte[] C = new byte[4];
                        byte[] D = new byte[4];

                        //双线性插值
                        for (int i = 0; i < heightAfter; i++)
                        {
                            for (int j = 0; j < widthAfter; j++)
                            {
                                //线性映射来计算出(i, j)对应的(iTemp, jTemp)
                                iTemp = (double)i / heightAfter * curBitmap.Height;
                                jTemp = (double)j / widthAfter * curBitmap.Width;

                                //计算整数部分, 即源图像对应的(i, j)
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
                                    A = BitConverter.GetBytes(rgbValues[iBefore * bmpData.Width + jBefore]);
                                    B = BitConverter.GetBytes(rgbValues[iBefore * bmpData.Width + jBefore + 1]);
                                    C = BitConverter.GetBytes(rgbValues[(iBefore + 1) * bmpData.Width + jBefore]);
                                    D = BitConverter.GetBytes(rgbValues[(iBefore + 1) * bmpData.Width + jBefore + 1]);
                                    //将四角四个像素作为参数输入, 即可双线性运算
                                    for (int k = 0; k < 4; k++)
                                    {
                                        Argb[k] = bilinear(p, q, A[k], B[k], C[k], D[k]);
                                    }
                                    newArray[i * widthAfter + j] = BitConverter.ToInt32(Argb, 0);
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

        private void To128Color_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                System.Console.WriteLine("click");

                ColorProcess processor = new ColorProcess();
                processor.setBitmapColorDegree(ref curBitmap, 7);

                Invalidate();
            }
        }

        private void To32Color_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                System.Console.WriteLine("click");

                ColorProcess processor = new ColorProcess();
                processor.setBitmapColorDegree(ref curBitmap, 5);

                Invalidate();
            }
        }

        private void To8Color_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                System.Console.WriteLine("click");

                ColorProcess processor = new ColorProcess();
                processor.setBitmapColorDegree(ref curBitmap, 3);

                Invalidate();
            }
        }

        private void To4Color_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                System.Console.WriteLine("click");

                ColorProcess processor = new ColorProcess();
                processor.setBitmapColorDegree(ref curBitmap, 2);

                Invalidate();
            }
        }

        private void To2Color_Click(object sender, EventArgs e)
        {
            if (curBitmap != null)
            {
                System.Console.WriteLine("click");

                ColorProcess processor = new ColorProcess();
                processor.setBitmapColorDegree(ref curBitmap, 1);

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

        





        /*
        private byte getRed(int n)
        {
            return (byte)((n & 0x00ff0000) >> 16);
        }

        private byte getGreen(int n)
        {
            return (byte)((n & 0x0000ff00) >> 8);
        }

        private byte getBlue(int n)
        {
            return (byte)(n & 0x0000ff);
        }
        */

    }
}
