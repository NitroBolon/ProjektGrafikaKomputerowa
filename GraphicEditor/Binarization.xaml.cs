using System;
using System.Collections.Generic;
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
        Image image = new Image();

        public Binarization(string filePath)
        {
            InitializeComponent();

            BitmapImage bitmap = new BitmapImage(new Uri(filePath, UriKind.Relative));
            writeableBitmap = new WriteableBitmap(bitmap);
            editedBitmap = writeableBitmap;

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
                image = new Image();
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
                image = new Image();
                image.Source = editedBitmap;
                canvas.Children.Add(image);
            }
        }

        private void O1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void O2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Restore_Click(object sender, RoutedEventArgs e)
        {
            editedBitmap = writeableBitmap;
            Image imagen = new Image();
            imagen.Source = editedBitmap;
            canvas.Children.Add(imagen);
        }
    }
}
