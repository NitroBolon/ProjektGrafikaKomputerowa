using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace GraphicEditor
{
    /// <summary>
    /// Logika interakcji dla klasy NewReshaping.xaml
    /// </summary>
    public partial class NewReshaping : Window
    {
        List<Shape> shapes = new List<Shape>();
        List<Point> temp = new List<Point>();
        int numOfPoints = 2;
        int step = 1;
        int current;
        Point diff;

        public NewReshaping()
        {
            InitializeComponent();
        }

        private void Execute_Click(object sender, RoutedEventArgs e)
        {
            if (draw.IsChecked == true)
            {
                if (step < numOfPoints)
                {
                    temp.Add(new Point(Int32.Parse(x.Text), Int32.Parse(x.Text)));

                    step++;
                }
                else
                {
                    temp.Add(new Point(Int32.Parse(x.Text), Int32.Parse(y.Text)));
                    Shape shape = new Shape(temp);
                    shapes.Add(shape);
                    shape.DrawShape(canva);
                    temp = new List<Point>();

                    step = 1;
                }
            }
            else if (move.IsChecked == true)
            {
                shapes[current].Move(Int32.Parse(x.Text), Int32.Parse(y.Text), canva);
                canva.Children.Clear();
                DrawAll();
            }
            else if (rotate.IsChecked == true)
            {
                shapes[current].Rotate(new Point(Int32.Parse(x.Text), Int32.Parse(y.Text)), Int32.Parse(w.Text), canva);
                canva.Children.Clear();
                DrawAll();
            }
            else if (scaleU.IsChecked == true)
            {
                shapes[current].Scale(1, Int32.Parse(w.Text), canva);
                canva.Children.Clear();
                DrawAll();
            }
            else if (scaleD.IsChecked == true)
            {
                shapes[current].Scale(0, Int32.Parse(w.Text), canva);
                canva.Children.Clear();
                DrawAll();
            }
        }

        private void Read_Click(object sender, RoutedEventArgs e)
        {
            string filePath, line;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "TXT Files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;

                System.IO.StreamReader file = new System.IO.StreamReader(filePath);
                while ((line = file.ReadLine()) != null)
                {
                    List<string> row = line.Split(' ').ToList();
                    List<Point> tmpPoints = new List<Point>();
                    for (int i = 0; i < row.Count; i += 2)
                    {
                        tmpPoints.Add(new Point(Int32.Parse(row[i]), Int32.Parse(row[i + 1])));
                    }
                    Shape tmp = new Shape(tmpPoints);
                    shapes.Add(tmp);
                    tmp.DrawShape(canva);
                }
                file.Close();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string filePath;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "TXT File (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
            {
                filePath = saveFileDialog.FileName;

                System.IO.StreamWriter file = new System.IO.StreamWriter(filePath);
                foreach (Shape shape in shapes)
                {
                    string tmp = "";
                    foreach (Point x in shape.points)
                    {
                        tmp += $"{x.X.ToString()} {x.Y.ToString()} ";
                    }
                    file.WriteLine(tmp);
                }
                file.Close();
            }
        }

        private void canva_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Point lel = e.GetPosition(this.canva);
            int difX = (int)lel.X - (int)diff.X;
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                if (move.IsChecked == true)
                {
                    shapes[current].MoveByMouse((int)e.GetPosition(this.canva).X, (int)e.GetPosition(this.canva).Y, canva);
                    canva.Children.Clear();
                    DrawAll();
                }
                else if (rotate.IsChecked == true)
                {
                    shapes[current].Rotate(diff, difX/10, canva);
                    canva.Children.Clear();
                    DrawAll();
                }
                else if (scaleU.IsChecked == true)
                {
                    shapes[current].Scale(1, difX/10, canva);
                    canva.Children.Clear();
                    DrawAll();
                }
                else if (scaleD.IsChecked == true)
                {
                    shapes[current].Scale(0, difX, canva);
                    canva.Children.Clear();
                    DrawAll();
                }
        }

        private void canva_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (draw.IsChecked == true)
            {
                if (step < numOfPoints)
                {
                    temp.Add(e.GetPosition(this.canva));

                    step++;
                }
                else
                {
                    temp.Add(e.GetPosition(this.canva));
                    Shape shape = new Shape(temp);
                    shapes.Add(shape);
                    shape.DrawShape(canva);
                    temp = new List<Point>();

                    step = 1;
                }
            } 
            else if (setter.IsChecked == true)
            {
                foreach (Line x in canva.Children)
                {
                    if (x.IsMouseOver)
                    {
                        foreach (Shape shape in shapes)
                        {
                            for (int i = 0; i < shape.points.Count; i++)
                            {
                                if ((shape.points[i].X == x.X1 && shape.points[i].Y == x.Y1) || (shape.points[i].X == x.X2 && shape.points[i].Y == x.Y2))
                                {
                                    current = shapes.IndexOf(shape);
                                    par.Content = current.ToString();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void s_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            int x;
            if (Int32.TryParse(s.Text, out x) && Int32.Parse(s.Text) > 2)
            {
                numOfPoints = Int32.Parse(s.Text);
            }
            else
            {
                numOfPoints = 2;
            }
            step = 1;
        }

        private void DrawAll()
        {
            foreach(Shape x in shapes)
            {
                x.DrawShape(canva);
            }
        }

        private void canva_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            diff = e.GetPosition(this.canva);
        }
    }
}
