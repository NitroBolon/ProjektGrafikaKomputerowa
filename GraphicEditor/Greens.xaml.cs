using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Logika interakcji dla klasy Greens.xaml
    /// </summary>
    public partial class Greens : Window
    {
        BitmapImage bitmapImage;
        public Greens(string filePath)
        {
            InitializeComponent();

            bitmapImage = new BitmapImage(new Uri(filePath));
            canva.Source = bitmapImage;
        }

        public Bitmap Green(Bitmap bmpInput)
        {
            Bitmap temp = new Bitmap(bmpInput.Width, bmpInput.Height);
            ColorsKwant kwant = new ColorsKwant();

            int licznik = 0;

            for (int j = 0; j < bmpInput.Height; j++)
            {
                for (int i = 0; i < bmpInput.Width; i++)
                {
                    Kolor x = kwant.findClosest(bmpInput.GetPixel(i,j).R, bmpInput.GetPixel(i, j).G, bmpInput.GetPixel(i, j).B);
                    if (x.nazwa == "zielony")
                    {
                        licznik++;
                        temp.SetPixel(i, j, System.Drawing.Color.FromArgb(
                        x.R,
                        x.G,
                        x.B
                    ));
                    } else
                    {
                        temp.SetPixel(i, j, System.Drawing.Color.FromArgb(
                        bmpInput.GetPixel(i, j).R,
                        bmpInput.GetPixel(i, j).G,
                        bmpInput.GetPixel(i, j).B
                    ));
                    }
                }
            }

            res.Content = $"{(double)licznik/(bmpInput.Width*bmpInput.Height)*100}%";

            return temp;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(naive.IsChecked == true)
            {
                var bitmapImage2 = BitmapToImageSource(Green(BitmapImage2Bitmap(bitmapImage)));
                canva.Source = bitmapImage2;
            }
            else
            {
                var bitmapImage2 = BitmapToImageSource(GreenHSV(BitmapImage2Bitmap(bitmapImage)));
                canva.Source = bitmapImage2;
            }
        }

        public Bitmap GreenHSV(Bitmap bmpInput)
        {
            Bitmap temp = new Bitmap(bmpInput.Width, bmpInput.Height);
            ColorsKwant kwant = new ColorsKwant();

            int licznik = 0;

            for (int j = 0; j < bmpInput.Height; j++)
            {
                for (int i = 0; i < bmpInput.Width; i++)
                {
                    System.Drawing.Color color = System.Drawing.Color.FromArgb(bmpInput.GetPixel(i, j).R, bmpInput.GetPixel(i, j).G, bmpInput.GetPixel(i, j).B);
                    float hue = color.GetHue();

                    if(hue>60 && hue < 180)
                    {
                        temp.SetPixel(i, j, System.Drawing.Color.FromArgb(
                            0,
                            255,
                            0
                        ));
                        licznik++;
                    } 
                    else
                    {
                        temp.SetPixel(i, j, System.Drawing.Color.FromArgb(
                            bmpInput.GetPixel(i, j).R,
                            bmpInput.GetPixel(i, j).G,
                            bmpInput.GetPixel(i, j).B
                        ));
                    }
                }
            }

            res.Content = $"{(double)licznik / (bmpInput.Width * bmpInput.Height) * 100}%";

            return temp;
        }
    }
}
