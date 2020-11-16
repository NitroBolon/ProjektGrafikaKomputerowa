using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
    /// Logika interakcji dla klasy Binarization.xaml
    /// </summary>
    public partial class Binarization : Window
    {
        WriteableBitmap writeableBitmap, editedBitmap;
        System.Windows.Controls.Image image = new System.Windows.Controls.Image();
        string file;
        BitmapImage bitmap;

        public Binarization(string filePath)
        {
            InitializeComponent();

            bitmap = new BitmapImage(new Uri(filePath, UriKind.Relative));
            writeableBitmap = new WriteableBitmap(bitmap);
            editedBitmap = writeableBitmap;
            file = filePath;
            image.Source = editedBitmap;
            canvas.Children.Add(image);
        }

        private void Bin_Click(object sender, RoutedEventArgs e)
        {
            int width = editedBitmap.PixelWidth;
            int height = editedBitmap.PixelHeight;
            int stride = editedBitmap.BackBufferStride;
            int bytesPerPixel = (editedBitmap.Format.BitsPerPixel + 7) / 8;
            byte[] imgdata = new byte[width * height * bytesPerPixel];

            editedBitmap.Lock();

            int val;

            if (Int32.TryParse(input.Text, out val))
            {
                val = val % 255;
                unsafe
                {
                    byte* pImgData = (byte*)editedBitmap.BackBuffer;

                    for (int row = 0; row < height; row++)
                    {
                        for (int col = 0; col < width; col++)
                        {
                            try
                            {
                                int x = ((pImgData[row * stride + col * 4 + 1] + pImgData[row * stride + col * 4 + 2] + pImgData[row * stride + col * 4 + 0]) / 3) % 255;
                                byte gray;

                                if (x >= val)
                                    gray = (byte)255;
                                else gray = (byte)0;

                                imgdata[row * stride + col * 4 + 0] = gray;
                                imgdata[row * stride + col * 4 + 1] = gray;
                                imgdata[row * stride + col * 4 + 2] = gray;
                                imgdata[row * stride + col * 4 + 3] = pImgData[row * stride + col * 4 + 3];
                            }
                            catch
                            {
                                MessageBox.Show("Unable to proceed action");
                            }
                        }
                    }
                }
                var gradient = BitmapSource.Create(width, height, 96, 96, PixelFormats.Bgra32, null, imgdata, stride);
                editedBitmap.WritePixels(
                    new Int32Rect(0, 0, width, height),
                    imgdata, stride, 0);
                editedBitmap.Unlock();
                image = new System.Windows.Controls.Image();
                image.Source = editedBitmap;
                canvas.Children.Add(image);
            }
        }

        private void BinD_Click(object sender, RoutedEventArgs e)
        {
            int width = editedBitmap.PixelWidth;
            int height = editedBitmap.PixelHeight;
            int stride = editedBitmap.BackBufferStride;
            int bytesPerPixel = (editedBitmap.Format.BitsPerPixel + 7) / 8;
            byte[] imgdata = new byte[width * height * bytesPerPixel];

            editedBitmap.Lock();

            int val, val2;

            if (Int32.TryParse(input.Text, out val) && Int32.TryParse(input2.Text, out val2))
            {
                val = val % 255;
                val2 = val2 % 255;
                if (val > val2)
                {
                    int tmp = val2;
                    val2 = val;
                    val = tmp;
                }

                unsafe
                {
                    byte* pImgData = (byte*)editedBitmap.BackBuffer;

                    for (int row = 0; row < height; row++)
                    {
                        for (int col = 0; col < width; col++)
                        {
                            try
                            {
                                int x = ((pImgData[row * stride + col * 4 + 1] + pImgData[row * stride + col * 4 + 2] + pImgData[row * stride + col * 4 + 0]) / 3) % 255;
                                byte gray;

                                if (x >= val && x<val2)
                                    gray = (byte)255;
                                else gray = (byte)0;

                                imgdata[row * stride + col * 4 + 0] = gray;
                                imgdata[row * stride + col * 4 + 1] = gray;
                                imgdata[row * stride + col * 4 + 2] = gray;
                                imgdata[row * stride + col * 4 + 3] = pImgData[row * stride + col * 4 + 3];
                            }
                            catch
                            {
                                MessageBox.Show("Unable to proceed action");
                            }
                        }
                    }
                }
                var gradient = BitmapSource.Create(width, height, 96, 96, PixelFormats.Bgra32, null, imgdata, stride);
                editedBitmap.WritePixels(
                    new Int32Rect(0, 0, width, height),
                    imgdata, stride, 0);
                editedBitmap.Unlock();
                image = new System.Windows.Controls.Image();
                image.Source = editedBitmap;
                canvas.Children.Add(image);
            }
        }

        private void O1_Click(object sender, RoutedEventArgs e)
        {
            int width = editedBitmap.PixelWidth;
            int height = editedBitmap.PixelHeight;
            int stride = editedBitmap.BackBufferStride;
            int bytesPerPixel = (editedBitmap.Format.BitsPerPixel + 7) / 8;
            byte[] imgdata = new byte[width * height * bytesPerPixel];
            int pixels = width * height;
            int val;
            int counter = 0;
            if(!Int32.TryParse(input.Text, out val))
            {
                return;
            }
            int numOfBlacks = (int)(((float)val/(float)100)*(float)pixels);
            List<int> lista = new List<int>();
            for(int i=0; i<256; i++)
            {
                lista.Add(0);
            }
            editedBitmap.Lock();

            unsafe
            {
                byte* pImgData = (byte*)editedBitmap.BackBuffer;

                for (int row = 0; row < height; row++)
                {
                    for (int col = 0; col < width; col++)
                    {
                        try
                        {
                            byte gray = (byte)(((pImgData[row * stride + col * 4 + 1] + pImgData[row * stride + col * 4 + 2] + pImgData[row * stride + col * 4 + 0]) / 3) % 255);

                            lista[gray]++;
                        }
                        catch
                        {
                            MessageBox.Show("Unable to proceed action");
                        }
                    }
                }
                int i = 0;
                while (counter < numOfBlacks)
                {
                    counter += lista[i];
                    i++;
                }

                for (int row = 0; row < height; row++)
                {
                    for (int col = 0; col < width; col++)
                    {
                        try
                        {
                            byte gray = (byte)(((pImgData[row * stride + col * 4 + 1] + pImgData[row * stride + col * 4 + 2] + pImgData[row * stride + col * 4 + 0]) / 3) % 255);

                            if (gray >= i)
                            {
                                imgdata[row * stride + col * 4 + 0] = 255;
                                imgdata[row * stride + col * 4 + 1] = 255;
                                imgdata[row * stride + col * 4 + 2] = 255;
                                imgdata[row * stride + col * 4 + 3] = (byte)255;
                            } else
                            {
                                imgdata[row * stride + col * 4 + 0] = 0;
                                imgdata[row * stride + col * 4 + 1] = 0;
                                imgdata[row * stride + col * 4 + 2] = 0;
                                imgdata[row * stride + col * 4 + 3] = (byte)255;
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Unable to proceed action");
                        }
                    }
                }
            }
            var gradient = BitmapSource.Create(width, height, 96, 96, PixelFormats.Bgra32, null, imgdata, stride);
            editedBitmap.WritePixels(
                new Int32Rect(0, 0, width, height),
                imgdata, stride, 0);
            editedBitmap.Unlock();
            image = new System.Windows.Controls.Image();
            image.Source = editedBitmap;
            canvas.Children.Add(image);
        }

        private void O2_Click(object sender, RoutedEventArgs e)
        {
            Bitmap curBitmap = new Bitmap(file);
            if (curBitmap != null)
            {
                int iR = 0;
                int iG = 0;
                int iB = 0;

                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, curBitmap.Width, curBitmap.Height);
                System.Drawing.Imaging.BitmapData bmpData =
                    curBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                    curBitmap.PixelFormat);

                IntPtr ptr = bmpData.Scan0;

                int bytes = Math.Abs(bmpData.Stride) * curBitmap.Height;
                byte[] rgbValues = new byte[bytes];

                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

                for (int counter = 0; counter < rgbValues.Length-5; counter += 3)
                {
                    iR = rgbValues[counter + 2];
                    iG = rgbValues[counter + 1];
                    iB = rgbValues[counter + 0];
                    if ((iR + iG + iB) / 3 > 100)
                    {
                        rgbValues[counter + 2] = 255;
                        rgbValues[counter + 1] = 255;
                        rgbValues[counter + 0] = 255;
                    }
                    else
                    {
                        rgbValues[counter + 2] = 0;
                        rgbValues[counter + 1] = 0;
                        rgbValues[counter + 0] = 0;
                    }
                }

                System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

                curBitmap.UnlockBits(bmpData);

                BitmapImage bitmapImage = BitmapToImageSource(curBitmap);
                var canvass = new System.Windows.Controls.Image();
                canvass.Source = bitmapImage;
                canvas.Children.Add(canvass);
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

        private void Restore_Click(object sender, RoutedEventArgs e)
        {
            bitmap = new BitmapImage(new Uri(file, UriKind.Relative));
            writeableBitmap = new WriteableBitmap(bitmap);
            System.Windows.Controls.Image imagen = new System.Windows.Controls.Image();
            imagen.Source = writeableBitmap;
            canvas.Children.Add(imagen);
        }
    }
}
