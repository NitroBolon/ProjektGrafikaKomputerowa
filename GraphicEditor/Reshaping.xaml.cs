using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GraphicEditor
{
    /// <summary>
    /// Logika interakcji dla klasy Reshaping.xaml
    /// </summary>
    public partial class Reshaping : Window
    {
        WriteableBitmap writeableBitmap, editedBitmap;
        string filePath;
        System.Windows.Controls.Image image = new System.Windows.Controls.Image();

        public Reshaping(string path)
        {
            InitializeComponent();
            filePath = path;
            BitmapImage bitmap = new BitmapImage(new Uri(path, UriKind.Relative));
            writeableBitmap = new WriteableBitmap(bitmap);
            editedBitmap = writeableBitmap;

            image.Source = editedBitmap;
            canvas.Children.Add(image);
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
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
                unsafe
                {
                    byte* pImgData = (byte*)editedBitmap.BackBuffer;

                    for (int row = 0; row < height; row++)
                    {
                        for (int col = 0; col < width; col++)
                        {
                            try
                            {
                                imgdata[row * stride + col * 4 + 0] = (byte)((pImgData[row * stride + col * 4 + 0] + val) % 255);
                                imgdata[row * stride + col * 4 + 1] = (byte)((pImgData[row * stride + col * 4 + 1] + val) % 255);
                                imgdata[row * stride + col * 4 + 2] = (byte)((pImgData[row * stride + col * 4 + 2] + val) % 255);
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

        private void Odejmij_Click(object sender, RoutedEventArgs e)
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
                unsafe
                {
                    byte* pImgData = (byte*)editedBitmap.BackBuffer;

                    for (int row = 0; row < height; row++)
                    {
                        for (int col = 0; col < width; col++)
                        {
                            try
                            {
                                imgdata[row * stride + col * 4 + 0] = (byte)((pImgData[row * stride + col * 4 + 0] - val) % 255);
                                imgdata[row * stride + col * 4 + 1] = (byte)((pImgData[row * stride + col * 4 + 1] - val) % 255);
                                imgdata[row * stride + col * 4 + 2] = (byte)((pImgData[row * stride + col * 4 + 2] - val) % 255);
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

        private void Pomnoz_Click(object sender, RoutedEventArgs e)
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
                unsafe
                {
                    byte* pImgData = (byte*)editedBitmap.BackBuffer;

                    for (int row = 0; row < height; row++)
                    {
                        for (int col = 0; col < width; col++)
                        {
                            try
                            {
                                imgdata[row * stride + col * 4 + 0] = (byte)((pImgData[row * stride + col * 4 + 0] * val) % 255);
                                imgdata[row * stride + col * 4 + 1] = (byte)((pImgData[row * stride + col * 4 + 1] * val) % 255);
                                imgdata[row * stride + col * 4 + 2] = (byte)((pImgData[row * stride + col * 4 + 2] * val) % 255);
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

        private void Podziel_Click(object sender, RoutedEventArgs e)
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
                unsafe
                {
                    byte* pImgData = (byte*)editedBitmap.BackBuffer;

                    for (int row = 0; row < height; row++)
                    {
                        for (int col = 0; col < width; col++)
                        {
                            try
                            {
                                imgdata[row * stride + col * 4 + 0] = (byte)((pImgData[row * stride + col * 4 + 0] / val) % 255);
                                imgdata[row * stride + col * 4 + 1] = (byte)((pImgData[row * stride + col * 4 + 1] / val) % 255);
                                imgdata[row * stride + col * 4 + 2] = (byte)((pImgData[row * stride + col * 4 + 2] / val) % 255);
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

        private void Jasnosc_Click(object sender, RoutedEventArgs e)
        {
            int val;

            if (Int32.TryParse(input.Text, out val))
            {
                System.Drawing.Image x = System.Drawing.Image.FromFile(filePath);
                Bitmap t = AdjustBrightness(x, val);

                MemoryStream Ms = new MemoryStream();
                t.Save(Ms, System.Drawing.Imaging.ImageFormat.Bmp);
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

        private void Szarosc1_Click(object sender, RoutedEventArgs e)
        {
            int width = editedBitmap.PixelWidth;
            int height = editedBitmap.PixelHeight;
            int stride = editedBitmap.BackBufferStride;
            int bytesPerPixel = (editedBitmap.Format.BitsPerPixel + 7) / 8;
            byte[] imgdata = new byte[width * height * bytesPerPixel];

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
                            byte gray = (byte)((0.3 * pImgData[row * stride + col * 4 + 2] + 0.5 * pImgData[row * stride + col * 4 + 1] + 0.2 * pImgData[row * stride + col * 4 + 0]) % 255);
                            imgdata[row * stride + col * 4 + 0] = gray;
                            imgdata[row * stride + col * 4 + 1] = gray;
                            imgdata[row * stride + col * 4 + 2] = gray;
                            imgdata[row * stride + col * 4 + 3] = (byte)255;
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

        private void Szarosc2_Click(object sender, RoutedEventArgs e)
        {
            int width = editedBitmap.PixelWidth;
            int height = editedBitmap.PixelHeight;
            int stride = editedBitmap.BackBufferStride;
            int bytesPerPixel = (editedBitmap.Format.BitsPerPixel + 7) / 8;
            byte[] imgdata = new byte[width * height * bytesPerPixel];

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
                            
                            imgdata[row * stride + col * 4 + 0] = gray;
                            imgdata[row * stride + col * 4 + 1] = gray;
                            imgdata[row * stride + col * 4 + 2] = gray;
                            imgdata[row * stride + col * 4 + 3] = (byte)255;
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

        private void Restore_Click(object sender, RoutedEventArgs e)
        {
            editedBitmap = writeableBitmap;
            System.Windows.Controls.Image imagen = new System.Windows.Controls.Image();
            imagen.Source = editedBitmap;
            canvas.Children.Add(imagen);
        }

        private Bitmap AdjustBrightness(System.Drawing.Image image, float brightness)
        {
            // Make the ColorMatrix.
            float b = brightness;
            ColorMatrix cm = new ColorMatrix(new float[][]
                {
            new float[] {b, 0, 0, 0, 0},
            new float[] {0, b, 0, 0, 0},
            new float[] {0, 0, b, 0, 0},
            new float[] {0, 0, 0, 1, 0},
            new float[] {0, 0, 0, 0, 1},
                });
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(cm);

            // Draw the image onto the new bitmap while applying
            // the new ColorMatrix.
            System.Drawing.Point[] points =
            {
        new System.Drawing.Point(0, 0),
        new System.Drawing.Point(image.Width, 0),
        new System.Drawing.Point(0, image.Height),
    };
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

            // Make the result bitmap.
            Bitmap bm = new Bitmap(image.Width, image.Height);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.DrawImage(image, points, rect,
                    GraphicsUnit.Pixel, attributes);
            }

            // Return the result.
            return bm;
        }
    }
}
