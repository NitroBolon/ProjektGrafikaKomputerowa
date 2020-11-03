using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GraphicEditor
{
    /// <summary>
    /// Logika interakcji dla klasy HsvRgb.xaml
    /// </summary>
    public partial class HsvRgb : Window
    {

        public HsvRgb()
        {
            Kafelek1();
            Kafelek2();
            Kafelek3();
            Kafelek4();
            Kafelek5();
            Kafelek6();

            InitializeComponent();
        }

        private void Kafelek1()
        {
            WriteableBitmap bitmapa = new WriteableBitmap(255, 255, 96, 96, PixelFormats.Bgr24, null);
            Int32Rect rect;

            bitmapa.Lock();
            for (int i = 0; i < 255; i++)
            {
                for (int j = 0; j < 255; j++)
                {
                    byte[] ColorData = { Convert.ToByte(i), Convert.ToByte(255-j), Convert.ToByte(255), 0 };

                    rect = new Int32Rect(j, i, 1, 1);
                    var stride = 255 * 3;
                    bitmapa.WritePixels(rect, ColorData, stride, 0);
                }
            }
            bitmapa.Unlock();

            using (FileStream stream5 = new FileStream("../../../Resources/1.png", FileMode.Create))
            {
                PngBitmapEncoder encoder5 = new PngBitmapEncoder();
                encoder5.Frames.Add(BitmapFrame.Create(bitmapa));
                encoder5.Save(stream5);
            }
        }
        private void Kafelek4()
        {
            WriteableBitmap bitmapa = new WriteableBitmap(255, 255, 96, 96, PixelFormats.Bgr24, null);
            Int32Rect rect;

            bitmapa.Lock();
            for (int i = 0; i < 255; i++)
            {
                for (int j = 254; j >=0; j--)
                {
                    byte[] ColorData = { Convert.ToByte(i), Convert.ToByte(j), Convert.ToByte(0), 0};

                    rect = new Int32Rect(j, i, 1, 1);
                    var stride = 255 * 3;
                    bitmapa.WritePixels(rect, ColorData, stride, 0);
                }
            }
            bitmapa.Unlock();

            using (FileStream stream5 = new FileStream("../../../Resources/4.png", FileMode.Create))
            {
                PngBitmapEncoder encoder5 = new PngBitmapEncoder();
                encoder5.Frames.Add(BitmapFrame.Create(bitmapa));
                encoder5.Save(stream5);
            }

            //tile1 = image;
            //tile2 = image;
            //tile3 = image;
            //tile4 = image;
            //tile5 = image;
            //tile6 = image;
        }
        private void Kafelek3()
        {
            WriteableBitmap bitmapa = new WriteableBitmap(255, 255, 96, 96, PixelFormats.Bgr24, null);
            Int32Rect rect;

            bitmapa.Lock();
            for (int i = 0; i < 255; i++)
            {
                for (int j = 0; j < 255; j++)
                {
                    byte[] ColorData = { Convert.ToByte(j), Convert.ToByte(255), Convert.ToByte(255 - i), 0 };

                    rect = new Int32Rect(j, i, 1, 1);
                    var stride = 255 * 3;
                    bitmapa.WritePixels(rect, ColorData, stride, 0);
                }
            }
            bitmapa.Unlock();

            using (FileStream stream5 = new FileStream("../../../Resources/3.png", FileMode.Create))
            {
                PngBitmapEncoder encoder5 = new PngBitmapEncoder();
                encoder5.Frames.Add(BitmapFrame.Create(bitmapa));
                encoder5.Save(stream5);
            }
        }
        private void Kafelek5()
        {
            WriteableBitmap bitmapa = new WriteableBitmap(255, 255, 96, 96, PixelFormats.Bgr24, null);
            Int32Rect rect;

            bitmapa.Lock();
            for (int i = 254; i >= 0; i--)
            {
                for (int j = 0; j < 255; j++)
                {
                    byte[] ColorData = { Convert.ToByte(0), Convert.ToByte(j), Convert.ToByte(255-i), 0 };

                    rect = new Int32Rect(j, i, 1, 1);
                    var stride = 255 * 3;
                    bitmapa.WritePixels(rect, ColorData, stride, 0);
                }
            }
            bitmapa.Unlock();

            using (FileStream stream5 = new FileStream("../../../Resources/5.png", FileMode.Create))
            {
                PngBitmapEncoder encoder5 = new PngBitmapEncoder();
                encoder5.Frames.Add(BitmapFrame.Create(bitmapa));
                encoder5.Save(stream5);
            }
        }
        private void Kafelek2()
        {
            WriteableBitmap bitmapa = new WriteableBitmap(255, 255, 96, 96, PixelFormats.Bgr24, null);
            Int32Rect rect;

            bitmapa.Lock();
            for (int i = 254; i >= 0; i--)
            {
                for (int j = 254; j >= 0; j--)
                {
                    byte[] ColorData = { Convert.ToByte(255), Convert.ToByte(255-j), Convert.ToByte(255 - i), 0 };

                    rect = new Int32Rect(j, i, 1, 1);
                    var stride = 255 * 3;
                    bitmapa.WritePixels(rect, ColorData, stride, 0);
                }
            }
            bitmapa.Unlock();

            using (FileStream stream5 = new FileStream("../../../Resources/2.png", FileMode.Create))
            {
                PngBitmapEncoder encoder5 = new PngBitmapEncoder();
                encoder5.Frames.Add(BitmapFrame.Create(bitmapa));
                encoder5.Save(stream5);
            }
        }
        private void Kafelek6()
        {
            WriteableBitmap bitmapa = new WriteableBitmap(255, 255, 96, 96, PixelFormats.Bgr24, null);
            Int32Rect rect;

            bitmapa.Lock();
            for (int i = 254; i >= 0; i--)
            {
                for (int j = 254; j >= 0; j--)
                {
                    byte[] ColorData = {  Convert.ToByte(255 - j), Convert.ToByte(0), Convert.ToByte(255 - i), 0 };

                    rect = new Int32Rect(j, i, 1, 1);
                    var stride = 255 * 3;
                    bitmapa.WritePixels(rect, ColorData, stride, 0);
                }
            }
            bitmapa.Unlock();

            using (FileStream stream5 = new FileStream("../../../Resources/6.png", FileMode.Create))
            {
                PngBitmapEncoder encoder5 = new PngBitmapEncoder();
                encoder5.Frames.Add(BitmapFrame.Create(bitmapa));
                encoder5.Save(stream5);
            }
        }
    }
}
