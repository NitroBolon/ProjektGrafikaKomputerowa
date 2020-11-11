using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace GraphicEditor
{
    /// <summary>
    /// Logika interakcji dla klasy Histogram.xaml
    /// </summary>
    public partial class Histogram : Window
    {
        WriteableBitmap writeableBitmap;
        System.Windows.Controls.Image image = new System.Windows.Controls.Image();
        private int[] red = null;
        private int[] green = null;
        private int[] blue = null;

        public Histogram(string filePath)
        {
            InitializeComponent();

            BitmapImage bitmap = new BitmapImage(new Uri(filePath, UriKind.Relative));
            writeableBitmap = new WriteableBitmap(bitmap);
            image.Source = writeableBitmap;
            canvas.Children.Add(image);
            int height = bitmap.PixelHeight;
            int width = bitmap.PixelWidth;
            int stride = writeableBitmap.BackBufferStride;
            red = new int[256];
            green = new int[256];
            blue = new int[256];
            unsafe
            {
                byte* pImgData = (byte*)writeableBitmap.BackBuffer;

                for (int col = 0; col < width; col++)
                {
                    for (int row = 0; row < height; row++)
                    {
                        red[pImgData[row * stride + col * 4 + 2]]++;
                        green[pImgData[row * stride + col * 4 + 1]]++;
                        blue[pImgData[row * stride + col * 4 + 0]]++;
                    }
                }
            }
        }

        private void Stretch_Click(object sender, RoutedEventArgs e)
        {
            int[] LUTred = calculateLUT(red);
            int[] LUTgreen = calculateLUT(green);
            int[] LUTblue = calculateLUT(blue);
            int height = writeableBitmap.PixelHeight;
            int width = writeableBitmap.PixelWidth;
            int stride = writeableBitmap.BackBufferStride;
            red = new int[256];
            green = new int[256];
            blue = new int[256];
            int bytesPerPixel = (writeableBitmap.Format.BitsPerPixel + 7) / 8;
            byte[] imgdata = new byte[width * height * bytesPerPixel];
            Bitmap newBitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* pImgData = (byte*)writeableBitmap.BackBuffer;
                for (int col = 0; col < width; col++)
                {
                    for (int row = 0; row < height; row++)
                    {
                        Color newPixel = Color.FromArgb(LUTred[pImgData[row * stride + col * 4 + 2]], LUTgreen[pImgData[row * stride + col * 4 + 1]], LUTblue[pImgData[row * stride + col * 4 + 0]]);
                        newBitmap.SetPixel(col, row, newPixel);
                        red[newPixel.R]++;
                        green[newPixel.G]++;
                        blue[newPixel.B]++;
                    }
                }
                MemoryStream Ms = new MemoryStream();
                newBitmap.Save(Ms, ImageFormat.Bmp);
                Ms.Position = 0;
                BitmapImage ObjBitmapImage = new BitmapImage();
                ObjBitmapImage.BeginInit();
                ObjBitmapImage.StreamSource = Ms;
                ObjBitmapImage.EndInit();

                System.Windows.Controls.Image imagen = new System.Windows.Controls.Image();
                imagen.Source = ObjBitmapImage;

                canvas.Children.Add(imagen);
            }
        }

        private void Equalize_Click(object sender, RoutedEventArgs e)
        {
            int[] LUTred = calculateLUT2(red, writeableBitmap.PixelWidth * writeableBitmap.PixelHeight);
            int[] LUTgreen = calculateLUT2(green, writeableBitmap.PixelWidth * writeableBitmap.PixelHeight);
            int[] LUTblue = calculateLUT2(blue, writeableBitmap.PixelWidth * writeableBitmap.PixelHeight);
            int height = writeableBitmap.PixelHeight;
            int width = writeableBitmap.PixelWidth;
            int stride = writeableBitmap.BackBufferStride;
            red = new int[256];
            green = new int[256];
            blue = new int[256];
            int bytesPerPixel = (writeableBitmap.Format.BitsPerPixel + 7) / 8;
            byte[] imgdata = new byte[width * height * bytesPerPixel];
            Bitmap newBitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* pImgData = (byte*)writeableBitmap.BackBuffer;
                for (int col = 0; col < width; col++)
                {
                    for (int row = 0; row < height; row++)
                    {
                        Color newPixel = Color.FromArgb(LUTred[pImgData[row * stride + col * 4 + 2]], LUTgreen[pImgData[row * stride + col * 4 + 1]], LUTblue[pImgData[row * stride + col * 4 + 0]]);
                        newBitmap.SetPixel(col, row, newPixel);
                        red[newPixel.R]++;
                        green[newPixel.G]++;
                        blue[newPixel.B]++;
                    }
                }
                MemoryStream Ms = new MemoryStream();
                newBitmap.Save(Ms, ImageFormat.Bmp);
                Ms.Position = 0;
                BitmapImage ObjBitmapImage = new BitmapImage();
                ObjBitmapImage.BeginInit();
                ObjBitmapImage.StreamSource = Ms;
                ObjBitmapImage.EndInit();

                System.Windows.Controls.Image imagen = new System.Windows.Controls.Image();
                imagen.Source = ObjBitmapImage;

                canvas.Children.Add(imagen);
            }
        }

        private void Restore_Click(object sender, RoutedEventArgs e)
        {
            //writeableBitmap = writeableBitmap;
            System.Windows.Controls.Image imagen = new System.Windows.Controls.Image();
            imagen.Source = writeableBitmap;
            canvas.Children.Add(imagen);
        }

        private int[] calculateLUT(int[] values)
        {
            int minValue = 0;
            for (int i = 0; i < 256; i++)
            {
                if (values[i] != 0)
                {
                    minValue = i;
                    break;
                }
            }

            int maxValue = 255;
            for (int i = 255; i >= 0; i--)
            {
                if (values[i] != 0)
                {
                    maxValue = i;
                    break;
                }
            }

            int[] result = new int[256];
            double a = 255.0 / (maxValue - minValue);
            for (int i = 0; i < 256; i++)
            {
                result[i] = (int)(a * (i - minValue));
            }

            return result;
        }

        private int[] calculateLUT2(int[] values, int size)
        {
            double minValue = 0;
            for (int i = 0; i < 256; i++)
            {
                if (values[i] != 0)
                {
                    minValue = values[i];
                    break;
                }
            }

            int[] result = new int[256];
            double sum = 0;
            for (int i = 0; i < 256; i++)
            {
                sum += values[i];
                result[i] = (int)(((sum - minValue) / (size - minValue)) * 255.0);
            }

            return result;
        }
    }
}
