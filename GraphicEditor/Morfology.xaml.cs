using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GraphicEditor
{
    /// <summary>
    /// Logika interakcji dla klasy Morfology.xaml
    /// </summary>
    public partial class Morfology : Window
    {
        BitmapImage baseImage;

        public Morfology(string filePath)
        {
            InitializeComponent();

            baseImage = new BitmapImage(new Uri(filePath));
            canva.Source = baseImage;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (dil.IsChecked == true)
            {
                var bitmapka = Dilation(baseImage);
                canva.Source = BitmapToImageSource(bitmapka);
            }
            else if (ero.IsChecked == true)
            {
                var bitmapka = Erosion(BitmapImage2Bitmap(baseImage));
                canva.Source = BitmapToImageSource(bitmapka);
            }
            else if (opn.IsChecked == true)
            {
                var bitmapka = Openness(BitmapImage2Bitmap(baseImage));
                canva.Source = BitmapToImageSource(bitmapka);
            }
            else if (cls.IsChecked == true)
            {
                var bitmapka = Closer(BitmapImage2Bitmap(baseImage));
                canva.Source = BitmapToImageSource(bitmapka);
            }
        }
        
        public Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                Bitmap bitmap = new Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }

        public BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        public Bitmap Dilation(BitmapImage bitmapa)
        {
            byte[,] sele = {
                { 1, 1, 1},
                { 1, 1, 1},
                { 1, 1, 1}
            };

            Bitmap bmpimg = BitmapImage2Bitmap(bitmapa);
            Bitmap tempbmp = (Bitmap)bmpimg.Clone();
            BitmapData help2 = tempbmp.LockBits(new System.Drawing.Rectangle(0, 0, tempbmp.Width, tempbmp.Height), ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            BitmapData help = bmpimg.LockBits(new System.Drawing.Rectangle(0, 0, bmpimg.Width, bmpimg.Height), ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* wsk = (byte*)help.Scan0;
                byte* tmpWsk = (byte*)help2.Scan0;

                wsk += help.Stride + 3;
                tmpWsk += help.Stride + 3;

                int rest = help.Stride - help.Width * 3;

                for (int i = 1; i < help.Height - 1; i++)
                {
                    for (int j = 1; j < help.Width - 1; j++)
                    {
                        if (wsk[0] == 255)
                        {
                            byte* tmp = tmpWsk - help.Stride - 3;

                            for (int k = 0; k < 3; k++)
                            {
                                for (int l = 0; l < 3; l++)
                                {
                                    tmp[help.Stride * k + l * 3] = tmp[help.Stride * k + l * 3 + 1] = tmp[help.Stride * k + l * 3 + 2] = (byte)(sele[k, l] * 255);
                                }
                            }
                        }
                        wsk += 3;
                        tmpWsk += 3;
                    }
                    wsk += rest + 6;
                    tmpWsk += rest + 6;
                }
            }

            bmpimg.UnlockBits(help);
            tempbmp.UnlockBits(help2);

            bmpimg = (Bitmap)tempbmp.Clone();
            return bmpimg;
        }

        private Bitmap Erosion(Bitmap bitmapa)
        {
            int level = 3;
            byte[,] sele = {
                { 1, 1, 1},
                { 1, 1, 1},
                { 1, 1, 1}
            };
            
            int kernel = (level - 1) / 2;
            float rgb;

            System.Drawing.Rectangle canvas = new System.Drawing.Rectangle(0, 0, bitmapa.Width, bitmapa.Height);
            BitmapData srcData = bitmapa.LockBits(canvas, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            int bytes = srcData.Stride * srcData.Height;
            byte[] pixels = new byte[bytes];
            byte[] res = new byte[bytes];

            Marshal.Copy(srcData.Scan0, pixels, 0, bytes);
            bitmapa.UnlockBits(srcData);
            
            for (int i = 0; i < bytes; i += 4)
            {
                rgb = pixels[i] * .071f;
                rgb += pixels[i + 1] * .71f;
                rgb += pixels[i + 2] * .21f;
                pixels[i] = (byte)rgb;
                pixels[i + 1] = pixels[i];
                pixels[i + 2] = pixels[i];
                pixels[i + 3] = 255;
            }

            for (int y = kernel; y < bitmapa.Height - kernel; y++)
            {
                for (int x = kernel; x < bitmapa.Width - kernel; x++)
                {
                    byte value = 255;
                    for (int yy = -kernel; yy <= kernel; yy++)
                    {
                        for (int xx = -kernel; xx <= kernel; xx++)
                        {
                            if (sele[yy + kernel, xx + kernel] == 1)
                            {
                                value = Math.Min(value, pixels[(y * srcData.Stride + x * 4) + yy * srcData.Stride + xx * 4]);
                            }
                        }
                    }
                    res[y * srcData.Stride + x * 4] = value;
                    res[y * srcData.Stride + x * 4 + 1] = value;
                    res[y * srcData.Stride + x * 4 + 2] = value;
                    res[y * srcData.Stride + x * 4 + 3] = 255;
                }
            }

            Bitmap result = new Bitmap(bitmapa.Width, bitmapa.Height);
            BitmapData resultBitmapa = result.LockBits(canvas, ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Marshal.Copy(res, 0, resultBitmapa.Scan0, bytes);
            result.UnlockBits(resultBitmapa);
            return result;
        }

        public Bitmap Openness(Bitmap bitmapa)
        {
            Bitmap res = Erosion(bitmapa);
            res = Dilation(BitmapToImageSource(res));

            return res;
        }

        public Bitmap Closer(Bitmap bitmapa)
        {
            Bitmap res = Dilation(BitmapToImageSource(bitmapa));
            res = Erosion(res);

            return res;
        }
    }
}
