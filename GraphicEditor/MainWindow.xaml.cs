using GraphicEditor.Objects;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace GraphicEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public EditorInstance editor;
        public string mode = "";

        public MainWindow()
        {
            InitializeComponent();
            editor = new EditorInstance(Canva);
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            ModeLabel.Content = "-";
            mode = "";
            Canva.Children.Remove(Canva.Children[Canva.Children.Count - 1]);
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
            //editor.AddCircle(new Objects.Point(50,50),50);
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

        private void Rectangle_Menu_Click(object sender, RoutedEventArgs e)
        {
            ModeLabel.Content = "-";
            mode = "";
        }

        private void Circle_Menu_Click(object sender, RoutedEventArgs e)
        {
            ModeLabel.Content = "-";
            mode = "";
        }

        private void Line_Menu_Click(object sender, RoutedEventArgs e)
        {
            ModeLabel.Content = "-";
            mode = "";
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Canva_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point p = e.GetPosition(this);
            double x = p.X;
            double y = p.Y;
            
            switch (mode)
            {
                case "circle":
                    {
                        editor.AddCircle(new Objects.Point((int)x, (int)y-100),1);
                    }break;
                case "line":
                    {

                    }break;
                case "rectangle":
                    {

                    }break;
                case "move":
                    {

                    }
                    break;
                case "resize":
                    {

                    }
                    break;
                default:
                    {

                    }break;
            }
        }

        private void Canva_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point p = e.GetPosition(this);
            double x = p.X;
            double y = p.Y;

            switch (mode)
            {
                case "circle":
                    {
                        
                    }
                    break;
                case "line":
                    {

                    }
                    break;
                case "rectangle":
                    {

                    }
                    break;
                case "move":
                    {

                    }
                    break;
                case "resize":
                    {

                    }
                    break;
                default:
                    {

                    }
                    break;
            }
        }

        private void Canva_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Point p = e.GetPosition(this);
            double x = p.X;
            double y = p.Y;

            switch (mode)
            {
                case "circle":
                    {
                        //try
                        //{
                        if (Canva.Children.Count > 0)
                        {
                            int tmp = Convert.ToInt32(editor.GetLastElement().GetValue(Canvas.LeftProperty));
                            if (System.Windows.Input.Mouse.LeftButton == MouseButtonState.Pressed)
                            {
                                editor.GetLastElement().SetValue(WidthProperty, Math.Abs(x - tmp));
                                editor.GetLastElement().SetValue(HeightProperty, Math.Abs(x - tmp));
                                editor.GetLastElement().SetValue(Canvas.TopProperty, (double)Math.Abs(Convert.ToInt32(editor.GetLastElement().GetValue(Canvas.TopProperty))- Convert.ToInt32(editor.GetLastElement().GetValue(WidthProperty))/2));
                                editor.GetLastElement().SetValue(Canvas.LeftProperty, (double)Math.Abs(Convert.ToInt32(editor.GetLastElement().GetValue(Canvas.TopProperty)) - Convert.ToInt32(editor.GetLastElement().GetValue(WidthProperty)) / 2));
                            }
                        }
                        //}
                        //catch 
                        //{
                        //    MessageBox.Show("Task failed successfully");
                        //};
                    }
                    break;
                case "line":
                    {

                    }
                    break;
                case "rectangle":
                    {

                    }
                    break;
                case "move":
                    {

                    }
                    break;
                case "resize":
                    {

                    }
                    break;
                default:
                    {

                    }
                    break;
            }
        }
    }
}
