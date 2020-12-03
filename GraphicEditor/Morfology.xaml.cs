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

            }
            else if (ero.IsChecked == true)
            {

            }
            else if (opn.IsChecked == true)
            {

            }
            else if (cls.IsChecked == true)
            {

            }
            else if (hom.IsChecked == true)
            {

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

        public static BitmapImage BitmapToImageSource(Bitmap bitmap)
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

        public Bitmap Dilation(BitmapImage bitmapImage)
        {
            byte[,] sele = new byte[3, 3];
            sele[0, 0] = (byte)1; sele[0, 1] = (byte)1; sele[0, 2] = (byte)1;
            sele[1, 0] = (byte)1; sele[1, 1] = (byte)1; sele[1, 2] = (byte)1;
            sele[2, 0] = (byte)1; sele[2, 1] = (byte)1; sele[2, 2] = (byte)1;
            Bitmap bmpimg = BitmapImage2Bitmap(bitmapImage);
            Bitmap tempbmp = (Bitmap)bmpimg.Clone();
            BitmapData data2 = tempbmp.LockBits(new System.Drawing.Rectangle(0, 0, tempbmp.Width, tempbmp.Height), ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            BitmapData data = bmpimg.LockBits(new System.Drawing.Rectangle(0, 0, bmpimg.Width, bmpimg.Height), ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            byte[,] sElement = sele;

            unsafe
            {
                byte* ptr = (byte*)data.Scan0;
                byte* tptr = (byte*)data2.Scan0;

                ptr += data.Stride + 3;
                tptr += data.Stride + 3;

                int remain = data.Stride - data.Width * 3;

                for (int i = 1; i < data.Height - 1; i++)
                {
                    for (int j = 1; j < data.Width - 1; j++)
                    {
                        if (ptr[0] == 255)
                        {
                            byte* temp = tptr - data.Stride - 3;

                            for (int k = 0; k < 3; k++)
                            {
                                for (int l = 0; l < 3; l++)
                                {
                                    temp[data.Stride * k + l * 3] = temp[data.Stride * k + l * 3 + 1] = temp[data.Stride * k + l * 3 + 2] = (byte)(sElement[k, l] * 255);
                                }
                            }
                        }

                        ptr += 3;
                        tptr += 3;
                    }
                    ptr += remain + 6;
                    tptr += remain + 6;
                }
            }

            bmpimg.UnlockBits(data);
            tempbmp.UnlockBits(data2);

            bmpimg = (Bitmap)tempbmp.Clone();
            return bmpimg;
        }

        private Bitmap Erosion(Bitmap srcImage)
        {
            int width = srcImage.Width;
            int height = srcImage.Height;

            System.Drawing.Rectangle canvas = new System.Drawing.Rectangle(0, 0, width, height);
            BitmapData srcData = srcImage.LockBits(canvas, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            int bytes = srcData.Stride * srcData.Height;
            byte[] pixelBuffer = new byte[bytes];
            byte[] resultBuffer = new byte[bytes];

            Marshal.Copy(srcData.Scan0, pixelBuffer, 0, bytes);
            srcImage.UnlockBits(srcData);


            float rgb;
            for (int i = 0; i < bytes; i += 4)
            {
                rgb = pixelBuffer[i] * .071f;
                rgb += pixelBuffer[i + 1] * .71f;
                rgb += pixelBuffer[i + 2] * .21f;
                pixelBuffer[i] = (byte)rgb;
                pixelBuffer[i + 1] = pixelBuffer[i];
                pixelBuffer[i + 2] = pixelBuffer[i];
                pixelBuffer[i + 3] = 255;
            }

            byte[,] kernel = new byte[3, 3];
            kernel[0, 0] = (byte)1; kernel[0, 1] = (byte)1; kernel[0, 2] = (byte)1;
            kernel[1, 0] = (byte)1; kernel[1, 1] = (byte)1; kernel[1, 2] = (byte)1;
            kernel[2, 0] = (byte)1; kernel[2, 1] = (byte)1; kernel[2, 2] = (byte)1;

            int kernelSize = 3;
            int kernelOffset = (kernelSize - 1) / 2;
            int calcOffset = 0;
            int byteOffset = 0;

            for (int y = kernelOffset; y < height - kernelOffset; y++)
            {
                for (int x = kernelOffset; x < width - kernelOffset; x++)
                {
                    byte value = 255;
                    byteOffset = y * srcData.Stride + x * 4;
                    for (int ykernel = -kernelOffset; ykernel <= kernelOffset; ykernel++)
                    {
                        for (int xkernel = -kernelOffset; xkernel <= kernelOffset; xkernel++)
                        {
                            if (kernel[ykernel + kernelOffset, xkernel + kernelOffset] == 1)
                            {
                                calcOffset = byteOffset + ykernel * srcData.Stride + xkernel * 4;
                                value = Math.Min(value, pixelBuffer[calcOffset]);
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    resultBuffer[byteOffset] = value;
                    resultBuffer[byteOffset + 1] = value;
                    resultBuffer[byteOffset + 2] = value;
                    resultBuffer[byteOffset + 3] = 255;
                }
            }

            Bitmap result = new Bitmap(width, height);
            BitmapData resultData = result.LockBits(canvas, ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Marshal.Copy(resultBuffer, 0, resultData.Scan0, bytes);
            result.UnlockBits(resultData);
            return result;
        }   


    }
}
