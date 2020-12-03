using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GraphicEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string mode = "";
        int step = 1;
        Line l = null;
        Ellipse c = null;
        Rectangle r = null;
        string filePPM = "";
        int compLvl = 50;
        int maxColor = 255;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            ModeLabel.Content = "-";
            mode = "";
            //Canva.Children.Remove(Canva.Children[Canva.Children.Count - 1]);
        }

        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            ModeLabel.Content = "-";
            mode = "";
        }

        private void Move_Click(object sender, RoutedEventArgs e)
        {
            ModeLabel.Content = "Przemieść";
            mode = "move";
        }

        private void Resize_Click(object sender, RoutedEventArgs e)
        {
            ModeLabel.Content = "Zmień rozmiar";
            mode = "resize";
        }

        private void Circle_Click(object sender, RoutedEventArgs e)
        {
            ModeLabel.Content = "Okrąg (ręcznie)";
            mode = "circle";
        }

        private void Rectangle_Click(object sender, RoutedEventArgs e)
        {
            ModeLabel.Content = "Prostokąt (ręcznie)";
            mode = "rectangle";
            //editor.AddRectangle(new Objects.Point(100, 100), new Objects.Point(200,200), new Objects.Point(100,100));
        }

        private void Line_Click(object sender, RoutedEventArgs e)
        {
            ModeLabel.Content = "Linia (ręcznie)";
            mode = "line";
            //editor.AddLine(new Objects.Point(75,75), new Objects.Point(150,150));
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Canva_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var point = e.GetPosition(this.Canva);
            double x = point.X;
            double y = point.Y;

            switch (mode)
            {
                case "circle":
                    {
                        if (step == 1)
                        {
                            Ellipse circle = new Ellipse()
                            {
                                Width = 2,
                                Height = 2,
                                Stroke = Brushes.Black,
                                StrokeThickness = 6
                            };
                            circle.SetValue(Canvas.LeftProperty, x);
                            circle.SetValue(Canvas.TopProperty, y);
                            Canva.Children.Add(circle);

                            step = 2;
                        }
                        else
                        {
                            Ellipse c = (Ellipse)Canva.Children[Canva.Children.Count - 1];
                            var centerX = c.GetValue(Canvas.LeftProperty);
                            var centerY = c.GetValue(Canvas.TopProperty);
                            var radius = Math.Sqrt(Math.Pow((double)centerX - x, 2) + Math.Pow((double)centerY - y, 2));
                            c.SetValue(Canvas.WidthProperty, (object)(radius * 2));
                            c.SetValue(Canvas.HeightProperty, c.GetValue(WidthProperty));
                            c.SetValue(Canvas.LeftProperty, (object)Math.Abs((double)c.GetValue(Canvas.LeftProperty) - radius));
                            c.SetValue(Canvas.TopProperty, (object)Math.Abs((double)c.GetValue(Canvas.TopProperty) - radius));

                            step = 1;
                        }
                    }
                    break;
                case "line":
                    {
                        if (step == 1)
                        {
                            Line line = new Line()
                            {
                                X1 = x,
                                X2 = x + 2,
                                Y1 = y,
                                Y2 = y + 2,
                                Stroke = Brushes.Black,
                                StrokeThickness = 3
                            };
                            Canva.Children.Add(line);

                            step = 2;
                        }
                        else
                        {
                            Line l = (Line)Canva.Children[Canva.Children.Count - 1];
                            l.X2 = x;
                            l.Y2 = y;

                            step = 1;
                        }
                    }
                    break;
                case "rectangle":
                    {
                        if (step == 1)
                        {
                            Rectangle rectangle = new Rectangle()
                            {
                                Width = 2,
                                Height = 2,
                                Stroke = Brushes.Black,
                                StrokeThickness = 3
                            };
                            rectangle.SetValue(Canvas.LeftProperty, x);
                            rectangle.SetValue(Canvas.TopProperty, y);
                            rectangle.SetValue(Canvas.RightProperty, x);
                            rectangle.SetValue(Canvas.BottomProperty, y);
                            Canva.Children.Add(rectangle);

                            step = 2;
                        }
                        else
                        {
                            Rectangle r = (Rectangle)Canva.Children[Canva.Children.Count - 1];
                            double leftP = (double)r.GetValue(Canvas.LeftProperty);
                            double topP = (double)r.GetValue(Canvas.TopProperty);
                            double widthP = (double)r.GetValue(Canvas.WidthProperty);
                            double heightP = (double)r.GetValue(Canvas.HeightProperty);

                            if (x >= leftP && y >= topP) //2nd quarter
                            {
                                r.SetValue(Canvas.BottomProperty, y);
                                r.SetValue(Canvas.RightProperty, x);
                                r.SetValue(Canvas.WidthProperty, (object)(Math.Abs((double)r.GetValue(Canvas.RightProperty) - (double)r.GetValue(Canvas.LeftProperty))));
                                r.SetValue(Canvas.HeightProperty, (object)(Math.Abs((double)r.GetValue(Canvas.TopProperty) - (double)r.GetValue(Canvas.BottomProperty))));
                            }
                            else if (x < leftP && y >= topP) //3rd quarter
                            {
                                r.SetValue(Canvas.BottomProperty, y);
                                r.SetValue(Canvas.LeftProperty, x);
                                r.SetValue(Canvas.WidthProperty, (object)(Math.Abs((double)r.GetValue(Canvas.RightProperty) - (double)r.GetValue(Canvas.LeftProperty))));
                                r.SetValue(Canvas.HeightProperty, (object)(Math.Abs((double)r.GetValue(Canvas.TopProperty) - (double)r.GetValue(Canvas.BottomProperty))));
                            }
                            else if (x >= leftP && y < topP) //1st quarter
                            {
                                r.SetValue(Canvas.TopProperty, y);
                                r.SetValue(Canvas.RightProperty, x);
                                r.SetValue(Canvas.WidthProperty, (object)(Math.Abs((double)r.GetValue(Canvas.RightProperty) - (double)r.GetValue(Canvas.LeftProperty))));
                                r.SetValue(Canvas.HeightProperty, (object)(Math.Abs((double)r.GetValue(Canvas.TopProperty) - (double)r.GetValue(Canvas.BottomProperty))));
                            }
                            else //4th quarter
                            {
                                r.SetValue(Canvas.TopProperty, y);
                                r.SetValue(Canvas.LeftProperty, x);
                                r.SetValue(Canvas.WidthProperty, (object)(Math.Abs((double)r.GetValue(Canvas.RightProperty) - (double)r.GetValue(Canvas.LeftProperty))));
                                r.SetValue(Canvas.HeightProperty, (object)(Math.Abs((double)r.GetValue(Canvas.TopProperty) - (double)r.GetValue(Canvas.BottomProperty))));
                            }

                            step = 1;
                        }
                    }
                    break;
                case "move":
                    {
                        if (step == 1)
                        {
                            foreach (UIElement shape in Canva.Children)
                            {
                                if (shape.GetType() == typeof(Line))
                                {
                                    if (shape.IsMouseOver)
                                    {
                                        l = (Line)shape;
                                        c = null;
                                        r = null;
                                        step = 2;
                                    }
                                }
                                else if (shape.GetType() == typeof(Ellipse))
                                {
                                    if (shape.IsMouseOver)
                                    {
                                        c = (Ellipse)shape;
                                        l = null;
                                        r = null;
                                        step = 2;
                                    }
                                }
                                else if (shape.GetType() == typeof(Rectangle))
                                {
                                    if (shape.IsMouseOver)
                                    {
                                        r = (Rectangle)shape;
                                        c = null;
                                        l = null;
                                        step = 2;
                                    }
                                }
                            }
                        }
                        else if (step == 2)
                        {
                            if (l != null)
                            {
                                double parA = (double)l.GetValue(Line.X2Property) - (double)l.GetValue(Line.X1Property);
                                double parB = (double)l.GetValue(Line.Y2Property) - (double)l.GetValue(Line.Y1Property);
                                l.SetValue(Line.X1Property, x);
                                l.SetValue(Line.Y1Property, y);
                                l.SetValue(Line.X2Property, x + parA);
                                l.SetValue(Line.Y2Property, y + parB);
                            }
                            else if (r != null)
                            {
                                r.SetValue(Canvas.LeftProperty, x);
                                r.SetValue(Canvas.TopProperty, y);
                            }
                            else if (c != null)
                            {
                                c.SetValue(Canvas.LeftProperty, x - (double)c.GetValue(Canvas.WidthProperty) / 2);
                                c.SetValue(Canvas.TopProperty, y - (double)c.GetValue(Canvas.HeightProperty) / 2);
                            }

                            step = 1;
                        }
                    }
                    break;
                case "resize":
                    {
                        if (step == 1)
                        {
                            foreach (UIElement shape in Canva.Children)
                            {
                                if (shape.GetType() == typeof(Line))
                                {
                                    if (shape.IsMouseOver)
                                    {
                                        l = (Line)shape;
                                        c = null;
                                        r = null;
                                        step = 2;
                                    }
                                }
                                else if (shape.GetType() == typeof(Ellipse))
                                {
                                    if (shape.IsMouseOver)
                                    {
                                        c = (Ellipse)shape;
                                        l = null;
                                        r = null;
                                        step = 2;
                                    }
                                }
                                else if (shape.GetType() == typeof(Rectangle))
                                {
                                    if (shape.IsMouseOver)
                                    {
                                        r = (Rectangle)shape;
                                        c = null;
                                        l = null;
                                        step = 2;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (l != null)
                            {
                                l.SetValue(Line.X2Property, x);
                                l.SetValue(Line.Y2Property, y);
                            }
                            else if (r != null)
                            {
                                if ((double)r.GetValue(Canvas.LeftProperty) > x || (double)r.GetValue(Canvas.TopProperty) > y)
                                {

                                }
                                else
                                {
                                    double width = x - (double)r.GetValue(Canvas.LeftProperty);
                                    double height = y - (double)r.GetValue(Canvas.TopProperty);

                                    r.SetValue(Canvas.WidthProperty, width);
                                    r.SetValue(Canvas.HeightProperty, height);
                                    r.SetValue(Canvas.RightProperty, width + (double)r.GetValue(Canvas.LeftProperty));
                                    r.SetValue(Canvas.BottomProperty, height + (double)r.GetValue(Canvas.TopProperty));
                                }
                            }
                            else if (c != null)
                            {
                                double rad = Math.Sqrt(Math.Pow(x - (double)c.GetValue(Canvas.LeftProperty), 2) + Math.Pow(y - (double)c.GetValue(Canvas.TopProperty), 2));
                                c.SetValue(Canvas.WidthProperty, rad);
                                c.SetValue(Canvas.HeightProperty, rad);
                            }

                            step = 1;
                        }
                    }
                    break;
                default:
                    {

                    }
                    break;
            }
        }

        private void DefineObject_Click(object sender, RoutedEventArgs e)
        {
            DrawingParameters parameters = new DrawingParameters((double)Canva.ActualWidth, (double)Canva.ActualHeight);
            parameters.ShowDialog();
            if (Application.Current.Properties["type"] != null)
            {
                DefinedObject x = new DefinedObject(Convert.ToInt32(Application.Current.Properties["type"]), Convert.ToDouble(Application.Current.Properties["sx1"]), Convert.ToDouble(Application.Current.Properties["sy1"]), Convert.ToDouble(Application.Current.Properties["sx2"]), Convert.ToDouble(Application.Current.Properties["sy2"]));
                switch (Application.Current.Properties["type"].ToString())
                {
                    case "1": Canva.Children.Add(x.DrawLine()); break;
                    case "2": Canva.Children.Add(x.DrawEllipse()); break;
                    case "3": Canva.Children.Add(x.DrawRectangle()); break;
                }
            }
            Application.Current.Properties["type"] = "";
        }

        private void Canva_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var parX = (double)0;
            var parY = (double)0;

            foreach (UIElement shape in Canva.Children)
            {
                if (shape.GetType() == typeof(Line))
                {
                    if (shape.IsMouseOver)
                    {
                        l = (Line)shape;
                        c = null;
                        r = null;
                    }
                }
                else if (shape.GetType() == typeof(Ellipse))
                {
                    if (shape.IsMouseOver)
                    {
                        c = (Ellipse)shape;
                        l = null;
                        r = null;
                    }
                }
                else if (shape.GetType() == typeof(Rectangle))
                {
                    if (shape.IsMouseOver)
                    {
                        r = (Rectangle)shape;
                        c = null;
                        l = null;
                    }
                }

                if (r != null || l != null || c != null)
                {
                    MoveObject moveObject = new MoveObject();
                    moveObject.ShowDialog();
                }

                parX = Convert.ToDouble(Application.Current.Properties["xx"]);
                parY = Convert.ToDouble(Application.Current.Properties["yy"]);

                if (l != null)
                {
                    var t1 = (double)(l.GetValue(Line.X2Property));
                    var t2 = (double)(l.GetValue(Line.Y2Property));

                    l.SetValue(Line.X2Property, t1 + parX);
                    l.SetValue(Line.Y2Property, t2 + parY);
                }
                else if (r != null)
                {
                    try
                    {
                        r.SetValue(Canvas.WidthProperty, (Convert.ToDouble(r.GetValue(Canvas.WidthProperty)) + parX));
                        r.SetValue(Canvas.HeightProperty, (Convert.ToDouble(r.GetValue(Canvas.HeightProperty)) + parY));
                        r.SetValue(Canvas.RightProperty, (Convert.ToDouble(r.GetValue(Canvas.RightProperty)) + parX));
                        r.SetValue(Canvas.BottomProperty, (Convert.ToDouble(r.GetValue(Canvas.BottomProperty)) + parY));
                    }
                    catch { }
                }
                else if (c != null)
                {
                    try
                    {
                        c.SetValue(Canvas.WidthProperty, (Convert.ToDouble(c.GetValue(Canvas.WidthProperty)) + parX));
                        c.SetValue(Canvas.HeightProperty, (Convert.ToDouble(c.GetValue(Canvas.HeightProperty)) + parX));
                        c.SetValue(Canvas.RightProperty, (Convert.ToDouble(c.GetValue(Canvas.RightProperty)) + parX));
                        c.SetValue(Canvas.BottomProperty, (Convert.ToDouble(c.GetValue(Canvas.BottomProperty)) + parX));
                    }
                    catch { }
                }

                l = null;
                r = null;
                c = null;
            }
        }

        private void Load_PPM_P3(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PPM Files (*.ppm)|*.ppm";
            if (openFileDialog.ShowDialog() == true)
                filePPM = openFileDialog.FileName;

            StreamReader file = new StreamReader(filePPM);
            string line1, format, line;
            int width = 0, height = 0, actLine = 1, counter = 0;
            WriteableBitmap bitmapa = new WriteableBitmap(255, 255, 96, 96, PixelFormats.Bgr24, null);
            bitmapa.Lock();

            Int32Rect rect;

            while ((line1 = file.ReadLine()) != null)
            {
                line = RemoveComment(line1);
                if (line != "" && !line.StartsWith("#"))
                {
                    if (counter == 0) // read format
                    {
                        try
                        {
                            format = line;
                        }
                        catch
                        {
                            Debug.WriteLine("Blad podczas pobierania formatu pliku");
                            return;
                        }
                    }
                    else if (counter == 1) // read size
                    {
                        try
                        {
                            var x = line.Split(" ");
                            width = Convert.ToInt32(x[0]);
                            height = Convert.ToInt32(x[1]);
                        }
                        catch
                        {
                            Debug.WriteLine("Blad podczas pobierania wymiarow obrazu");
                            return;
                        }
                    }
                    if (counter == 2) // read max color
                    {
                        try
                        {
                            maxColor = Convert.ToInt32(line);
                        }
                        catch
                        {
                            Debug.WriteLine("Blad podczas pobierania max koloru");
                            return;
                        }
                    }
                    else if (counter > 2)
                    {
                        try
                        {
                            List<string> x = line.Split(" ").ToList();
                            x = x.Where(w => w != "").ToList();

                            for (int i = 0; i < width; i++)
                            {
                                byte[] ColorData = { Convert.ToByte(Convert.ToInt32(x[i * 3]) * 255 / maxColor), Convert.ToByte(Convert.ToInt32(x[i * 3 + 1]) * 255 / maxColor), Convert.ToByte(Convert.ToInt32(x[i * 3 + 2]) * 255 / maxColor), 0 };
                                rect = new Int32Rect(i + 1, actLine, 1, 1);
                                var stride = width * 3;
                                bitmapa.WritePixels(rect, ColorData, stride, 0);
                            }
                            actLine++;
                        }
                        catch
                        {
                            Debug.WriteLine("Blad podczas generowania obrazu");
                            return;
                        }
                    }
                    counter++;
                }
            }

            bitmapa.Unlock();
            Image image = new Image();
            image.Source = bitmapa;
            Canva.Children.Add(image);
        }

        private void Load_PPM_P3_list(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PPM Files (*.ppm)|*.ppm";
            if (openFileDialog.ShowDialog() == true)
                filePPM = openFileDialog.FileName;
            StreamReader file = new StreamReader(filePPM);
            WriteableBitmap bitmapa = new WriteableBitmap(7000, 7000, 96, 96, PixelFormats.Bgr24, null);
            int width = 0, height = 0;
            Int32Rect rect;
            string line;

            line = file.ReadLine();
            line = file.ReadLine();
            width = Convert.ToInt32(file.ReadLine());
            height = Convert.ToInt32(file.ReadLine());
            maxColor = Convert.ToInt32(file.ReadLine());

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    byte[] ColorData = { Convert.ToByte(Convert.ToInt32(file.ReadLine()) / (maxColor > 255 ? 257 : 1)), Convert.ToByte(Convert.ToInt32(file.ReadLine()) / (maxColor > 255 ? 257 : 1)), Convert.ToByte(Convert.ToInt32(file.ReadLine()) / (maxColor > 255 ? 257 : 1)), 0 };
                    rect = new Int32Rect(j + 1, i + 1, 1, 1);
                    var stride = width * 3;
                    bitmapa.WritePixels(rect, ColorData, stride, 0);
                }
            }

            Image image = new Image();
            image.Source = bitmapa;
            Canva.Children.Add(image);
        }

        private string RemoveComment(string input)
        {
            string result = "";
            try
            {
                var x = input.Split("#");
                result = x[0];
            }
            catch
            {
                Debug.WriteLine("Blad podczas usuwania komentarzy");
            }
            return result;
        }

        private void Load_PPM_P6(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PPM Files (*.ppm)|*.ppm";
            if (openFileDialog.ShowDialog() == true)
                filePPM = openFileDialog.FileName;
            BinaryReader br;
            string v;
            br = new BinaryReader(new FileStream(filePPM, FileMode.Open));
            WriteableBitmap bitmapa = new WriteableBitmap(7000, 7000, 96, 96, PixelFormats.Bgr24, null);
            v = br.ReadString();
            string[] parameters = v.Split("\n");
            string[] size = parameters[2].Split(" ");
            int width = Convert.ToInt32(size[0]);
            int height = Convert.ToInt32(size[1]);
            maxColor = 255;
            var pic = br.ReadBytes(Convert.ToInt32(size[0]) * Convert.ToInt32(size[1]) * 3);
            Int32Rect rect;

            try
            {
                List<byte> x = pic.ToList();

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        byte[] ColorData = { Convert.ToByte(Convert.ToInt32(x[i * width + j]) / (maxColor > 255 ? 257 : 1)), Convert.ToByte(Convert.ToInt32(x[i * width + j]) / (maxColor > 255 ? 257 : 1)), Convert.ToByte(Convert.ToInt32(x[i * width + j]) / (maxColor > 255 ? 257 : 1)), 0 };
                        rect = new Int32Rect(j + 1, i + 1, 1, 1);
                        var stride = width * 3;
                        bitmapa.WritePixels(rect, ColorData, stride, 0);
                    }
                }

                Image image = new Image();
                image.Source = bitmapa;
                Canva.Children.Add(image);
            }
            catch
            {
                Debug.WriteLine("Blad podczas generowania obrazu");
                return;
            }

            br.Close();
        }

        private void Load_PPM_P6_c(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PPM Files (*.ppm)|*.ppm";
            if (openFileDialog.ShowDialog() == true)
                filePPM = openFileDialog.FileName;
            BinaryReader br;
            string v;
            br = new BinaryReader(new FileStream(filePPM, FileMode.Open));
            WriteableBitmap bitmapa = new WriteableBitmap(7000, 7000, 96, 96, PixelFormats.Bgr24, null);
            v = br.ReadString();
            string[] parameters = v.Split("\n");
            string[] size = parameters[1].Split(" ");
            int width = Convert.ToInt32(size[0]);
            int height = Convert.ToInt32(size[1]);
            maxColor = 255;
            var pic = br.ReadBytes(Convert.ToInt32(size[0]) * Convert.ToInt32(size[1]) * 3);
            Int32Rect rect;

            try
            {
                List<byte> x = pic.ToList();

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        byte[] ColorData = { Convert.ToByte(Convert.ToInt32(x[i * width + j]) / (maxColor > 255 ? 257 : 1)), Convert.ToByte(Convert.ToInt32(x[i * width + j]) / (maxColor > 255 ? 257 : 1)), Convert.ToByte(Convert.ToInt32(x[i * width + j]) / (maxColor > 255 ? 257 : 1)), 0 };
                        rect = new Int32Rect(j + 1, i + 1, 1, 1);
                        var stride = width * 3;
                        bitmapa.WritePixels(rect, ColorData, stride, 0);
                    }
                }

                Image image = new Image();
                image.Source = bitmapa;
                Canva.Children.Add(image);
            }
            catch
            {
                Debug.WriteLine("Blad podczas generowania obrazu");
                return;
            }

            br.Close();
        }

        private void Load_PPM_P6_list(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PPM Files (*.ppm)|*.ppm";
            if (openFileDialog.ShowDialog() == true)
                filePPM = openFileDialog.FileName;
            BinaryReader br;

            br = new BinaryReader(new FileStream(filePPM, FileMode.Open));
            WriteableBitmap bitmapa = new WriteableBitmap(8000, 3000, 96, 96, PixelFormats.Bgr24, null);
            
            var v = br.ReadString();
            v = br.ReadString();
            int width = 0, height = 0;
            maxColor = 255;
            Int32Rect rect;
            width = 7740;
            height = 2454;
            var c = br.ReadByte();
            var pic = br.ReadBytes(width * height * 3);
            try
            {
                List<byte> x = pic.ToList();
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        byte[] ColorData = { Convert.ToByte(Convert.ToInt32(x[i * width + j]) / (maxColor > 255 ? 257 : 1)), Convert.ToByte(Convert.ToInt32(x[i * width + j]) / (maxColor > 255 ? 257 : 1)), Convert.ToByte(Convert.ToInt32(x[i * width + j]) / (maxColor > 255 ? 257 : 1)), 0 };
                        rect = new Int32Rect(j + 1, i + 1, 1, 1);
                        var stride = width * 3;
                        bitmapa.WritePixels(rect, ColorData, stride, 0);
                    }
                }

                Image image = new Image();
                image.Source = bitmapa;
                Canva.Children.Add(image);
            }
            catch
            {
                Debug.WriteLine("Blad podczas generowania obrazu");
                return;
            }

            br.Close();
        }

        private void Load_JPEG(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|JPG Files (*.jpg)|*.jpg";
            if (openFileDialog.ShowDialog() == true)
                filePPM = openFileDialog.FileName;

            BitmapImage bitmapa = new BitmapImage();
            bitmapa.BeginInit();
            bitmapa.UriSource = new Uri(filePPM);
            bitmapa.EndInit();
            Image image = new Image();
            image.Source = bitmapa;
            Canva.Children.Add(image);
        }

        private void Save_JPEG(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JPEG File (*.jpeg)|*.jpeg|JPG File (*.jpg)|*.jpg";
            if (saveFileDialog.ShowDialog() == true)
                filePPM = saveFileDialog.FileName;
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)Canva.RenderSize.Width,
                (int)Canva.RenderSize.Height, 96d, 96d, System.Windows.Media.PixelFormats.Default);
            rtb.Render(Canva);
            var crop = new CroppedBitmap(rtb, new Int32Rect(0, 0, Convert.ToInt32(Canva.ActualWidth), Convert.ToInt32(Canva.ActualHeight)));

            JpegBitmapEncoder jpgEncoder = new JpegBitmapEncoder();
            jpgEncoder.QualityLevel = compLvl;
            jpgEncoder.Frames.Add(BitmapFrame.Create(crop));
            using (var fs = System.IO.File.OpenWrite(filePPM))
            {
                jpgEncoder.Save(fs);
            }
        }

        private void LowerLevel(object sender, RoutedEventArgs e)
        {
            if (compLvl >= 6) compLvl -= 5;
            compress.Content = compLvl.ToString();
        }

        private void HigherLevel(object sender, RoutedEventArgs e)
        {
            if (compLvl <= 95) compLvl += 5;
            compress.Content = compLvl.ToString();
        }

        private void CmykRgb_Click(object sender, RoutedEventArgs e)
        {
            CmykRgb parameters = new CmykRgb();
            parameters.ShowDialog();
        }

        private void HsvRgb_Click(object sender, RoutedEventArgs e)
        {
            HsvRgb parameters = new HsvRgb();
            parameters.ShowDialog();
        }

        private void Reshaping_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg";
            Reshaping wind;
            if (openFileDialog.ShowDialog() == true)
            {
                wind = new Reshaping(openFileDialog.FileName);
                wind.ShowDialog();
            }
        }
       
        private void MakingBetter_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg";
            Filters wind;
            if (openFileDialog.ShowDialog() == true)
            {
                wind = new Filters(openFileDialog.FileName);
                wind.ShowDialog();
            }
        }

        private void Hist_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg";
            Histogram wind;
            if (openFileDialog.ShowDialog() == true)
            {
                wind = new Histogram(openFileDialog.FileName);
                wind.ShowDialog();
            }
        }

        private void Bin_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg";
            Binarization wind;
            if (openFileDialog.ShowDialog() == true)
            {
                wind = new Binarization(openFileDialog.FileName);
                wind.ShowDialog();
            }
        }

        private void Bezier_Click(object sender, RoutedEventArgs e)
        {
            Bezier wind = new Bezier();
            wind.ShowDialog();
        }

        private void Resh_Click(object sender, RoutedEventArgs e)
        {
            NewReshaping wind = new NewReshaping();
            wind.ShowDialog();
        }

        private void Morf_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg";
            Morfology wind;
            if (openFileDialog.ShowDialog() == true)
            {
                wind = new Morfology(openFileDialog.FileName);
                wind.ShowDialog();
            }
        }

        private void Green_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg";
            Greens wind;
            if (openFileDialog.ShowDialog() == true)
            {
                wind = new Greens(openFileDialog.FileName);
                wind.ShowDialog();
            }
        }
    }
}
