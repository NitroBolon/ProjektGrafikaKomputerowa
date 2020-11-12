using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace GraphicEditor
{
    /// <summary>
    /// Logika interakcji dla klasy Filters.xaml
    /// </summary>
    public partial class Filters : Window
    {
        BitmapImage bitmapImage;
        string filePath;
        public Filters(string path)
        {
            InitializeComponent();

            filePath = path;
            bitmapImage = new BitmapImage(new Uri(path));
            canvas.Source = bitmapImage;
        }

        private void Mi_Click(object sender, RoutedEventArgs e)
        {
            bitmapImage = BitmapToImageSource(SmoothFilter(BitmapImage2Bitmap(bitmapImage)));
            canvas.Source = bitmapImage;
        }

        private void Me_Click(object sender, RoutedEventArgs e)
        {
            bitmapImage = BitmapToImageSource(MedianFilter(BitmapImage2Bitmap(bitmapImage)));
            canvas.Source = bitmapImage;
        }

        private void So_Click(object sender, RoutedEventArgs e)
        {
            bitmapImage = BitmapToImageSource(SobelFilter(BitmapImage2Bitmap(bitmapImage)));
            canvas.Source = bitmapImage;
        }

        private void Gp_Click(object sender, RoutedEventArgs e)
        {
            bitmapImage = BitmapToImageSource(HighPassSharpenFilter(BitmapImage2Bitmap(bitmapImage)));
            canvas.Source = bitmapImage;
        }

        private void Ga_Click(object sender, RoutedEventArgs e)
        {
            bitmapImage = BitmapToImageSource(GaussianBlurFilter(BitmapImage2Bitmap(bitmapImage)));
            canvas.Source = bitmapImage;
        }

        private void R_Click(object sender, RoutedEventArgs e)
        {
            bitmapImage = new BitmapImage(new Uri(filePath));
            canvas.Source = bitmapImage;
        }

        public static Bitmap HighPassSharpenFilter(Bitmap bmpInput)
        {
            Bitmap temp = new Bitmap(bmpInput.Width, bmpInput.Height);
            System.Drawing.Color c;
            int[,] kernel = new int[3, 3] {
                { 0, -1, 0 },
                { -1, 5, -1 },
                { 0, -1, 0 } };
            int sumR = 0, sumG = 0, sumB = 0;

            for (int j = 1; j < bmpInput.Height - 1; j++)
            {
                for (int i = 1; i < bmpInput.Width - 1; i++)
                {
                    for (int y = j - 1; y < j + 2; y++)
                    {
                        for (int x = i - 1; x < i + 2; x++)
                        {
                            c = bmpInput.GetPixel(x, y);
                            sumR += c.R * kernel[y - j + 1, x - i + 1];
                            sumG += c.G * kernel[y - j + 1, x - i + 1];
                            sumB += c.B * kernel[y - j + 1, x - i + 1];
                        }
                    }

                    if (sumR > 255) sumR = 255;
                    else if (sumR < 0) sumR = 0;
                    if (sumG > 255) sumG = 255;
                    else if (sumG < 0) sumG = 0;
                    if (sumB > 255) sumB = 255;
                    else if (sumB < 0) sumB = 0;

                    temp.SetPixel(i, j, System.Drawing.Color.FromArgb(
                        sumR,
                        sumG,
                        sumB
                    ));

                    sumR = 0;
                    sumG = 0;
                    sumB = 0;
                }
            }

            return temp;
        }

        public static Bitmap GaussianBlurFilter(Bitmap bmpInput)
        {
            Bitmap temp = bmpInput;
            System.Drawing.Color c;
            int[,] kernel = new int[5, 5] {
                {  1,  4,  7,  4,  1},
                {  4, 16, 26, 16,  4},
                {  7, 26, 41, 26,  7},
                {  4, 16, 26, 16,  4},
                {  1,  4,  7,  4,  1} };
            int sumR = 0, sumG = 0, sumB = 0;

            for (int j = 2; j < bmpInput.Height - 2; j++)
            {
                for (int i = 2; i < bmpInput.Width - 2; i++)
                {
                    for (int y = j - 2; y < j + 3; y++)
                    {
                        for (int x = i - 2; x < i + 3; x++)
                        {
                            c = bmpInput.GetPixel(x, y);
                            sumR += c.R * kernel[y - j + 2, x - i + 2];
                            sumG += c.G * kernel[y - j + 2, x - i + 2];
                            sumB += c.B * kernel[y - j + 2, x - i + 2];
                        }
                    }

                    sumR /= 273;
                    sumG /= 273;
                    sumB /= 273;

                    if (sumR > 255) sumR = 255;
                    else if (sumR < 0) sumR = 0;
                    if (sumG > 255) sumG = 255;
                    else if (sumG < 0) sumG = 0;
                    if (sumB > 255) sumB = 255;
                    else if (sumB < 0) sumB = 0;

                    temp.SetPixel(i, j, System.Drawing.Color.FromArgb(
                        sumR,
                        sumG,
                        sumB
                    ));

                    sumR = 0;
                    sumG = 0;
                    sumB = 0;
                }
            }

            return temp;
        }

        public static Bitmap SobelFilter(Bitmap bmpInput)
        {
            Bitmap temp = new Bitmap(bmpInput.Width, bmpInput.Height);
            System.Drawing.Color c;
            int[,] kernel = new int[3, 3] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            int sumR = 0, sumG = 0, sumB = 0;

            for (int j = 1; j < bmpInput.Height - 1; j++)
            {
                for (int i = 1; i < bmpInput.Width - 1; i++)
                {
                    for (int y = j - 1; y < j + 2; y++)
                    {
                        for (int x = i - 1; x < i + 2; x++)
                        {
                            c = bmpInput.GetPixel(x, y);
                            sumR += c.R * kernel[y - j + 1, x - i + 1];
                            sumG += c.G * kernel[y - j + 1, x - i + 1];
                            sumB += c.B * kernel[y - j + 1, x - i + 1];
                        }
                    }

                    if (sumR > 255) sumR = 255;
                    else if (sumR < 0) sumR = 0;
                    if (sumG > 255) sumG = 255;
                    else if (sumG < 0) sumG = 0;
                    if (sumB > 255) sumB = 255;
                    else if (sumB < 0) sumB = 0;

                    temp.SetPixel(i, j, System.Drawing.Color.FromArgb(
                        sumR,
                        sumG,
                        sumB
                    ));

                    sumR = 0;
                    sumG = 0;
                    sumB = 0;
                }
            }

            return temp;
        }

        public static Bitmap SmoothFilter(Bitmap bmpInput)
        {
            Bitmap temp = new Bitmap(bmpInput.Width, bmpInput.Height);
            System.Drawing.Color c;
            float sumR = 0, sumG = 0, sumB = 0;
            temp = bmpInput;
            for (int i = 0; i <= bmpInput.Width - 3; i++)
                for (int j = 0; j <= bmpInput.Height - 3; j++)
                {
                    for (int x = i; x <= i + 2; x++)
                        for (int y = j; y <= j + 2; y++)
                        {
                            c = bmpInput.GetPixel(x, y);
                            sumR = sumR + c.R;
                            sumG = sumG + c.G;
                            sumB = sumB + c.B;
                        }
                    int colorR = (int)Math.Round(sumR / 9, 10);
                    int colorG = (int)Math.Round(sumG / 9, 10);
                    int colorB = (int)Math.Round(sumB / 9, 10);
                    temp.SetPixel(i + 1, j + 1, System.Drawing.Color.FromArgb(colorR, colorG, colorB));
                    sumR = 0;
                    sumG = 0;
                    sumB = 0;
                }
            return temp;
        }

        public static Bitmap MedianFilter(Bitmap bitmapInput)
        {
            Bitmap bitmap = bitmapInput;
            System.Drawing.Color color;
            int[] colorsR, colorsG, colorsB;
            for (int y = 1; y < bitmap.Height - 1; y++)
            {
                for (int x = 1; x < bitmap.Width - 1; x++)
                {
                    //color = bitmap.GetPixel(x, y);
                    colorsR = new int[9] {
                        bitmap.GetPixel(x-1, y-1).R,
                        bitmap.GetPixel(x, y-1).R,
                        bitmap.GetPixel(x+1, y-1).R,
                        bitmap.GetPixel(x-1, y).R,
                        bitmap.GetPixel(x, y).R,
                        bitmap.GetPixel(x+1, y).R,
                        bitmap.GetPixel(x-1, y+1).R,
                        bitmap.GetPixel(x, y+1).R,
                        bitmap.GetPixel(x+1, y+1).R
                    };

                    colorsG = new int[9] {
                        bitmap.GetPixel(x-1, y-1).G,
                        bitmap.GetPixel(x, y-1).G,
                        bitmap.GetPixel(x+1, y-1).G,
                        bitmap.GetPixel(x-1, y).G,
                        bitmap.GetPixel(x, y).G,
                        bitmap.GetPixel(x+1, y).G,
                        bitmap.GetPixel(x-1, y+1).G,
                        bitmap.GetPixel(x, y+1).G,
                        bitmap.GetPixel(x+1, y+1).G
                    };

                    colorsB = new int[9] {
                        bitmap.GetPixel(x-1, y-1).B,
                        bitmap.GetPixel(x, y-1).B,
                        bitmap.GetPixel(x+1, y-1).B,
                        bitmap.GetPixel(x-1, y).B,
                        bitmap.GetPixel(x, y).B,
                        bitmap.GetPixel(x+1, y).B,
                        bitmap.GetPixel(x-1, y+1).B,
                        bitmap.GetPixel(x, y+1).B,
                        bitmap.GetPixel(x+1, y+1).B
                    };

                    bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(
                        GetMedian(colorsR),
                        GetMedian(colorsG),
                        GetMedian(colorsB)
                        ));
                }
            }
            return bitmap;
        }

        public static int GetMedian(int[] source)
        {
            // Create a copy of the input, and sort the copy
            int[] temp = source.ToArray();
            Array.Sort(temp);
            int count = temp.Length;
            if (count == 0)
            {
                throw new InvalidOperationException("Empty collection");
            }
            else if (count % 2 == 0)
            {
                // count is even, average two middle elements
                int a = temp[count / 2 - 1];
                int b = temp[count / 2];
                return (int)((a + b) / 2m);
            }
            else
            {
                // count is odd, return the middle element
                return temp[count / 2];
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
    }
}
