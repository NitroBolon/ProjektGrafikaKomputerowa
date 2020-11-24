using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicEditor
{
    /// <summary>
    /// Logika interakcji dla klasy Bezier.xaml
    /// </summary>
    public partial class Bezier : Window
    {
        int step = 1, range = 3;
        List<Point> punkty = new List<Point>();
        List<List<Point>> pointsy = new List<List<Point>>();
        int indeksKrzywej = -1, indeksPunktu = -1;

        public Bezier()
        {
            InitializeComponent();
        }

        private void Draw_Click(object sender, RoutedEventArgs e)
        {
            if (Draw.IsChecked == true)
            {
                if (step < range)
                {
                    punkty.Add(new Point(System.Convert.ToDouble(x.Text), System.Convert.ToDouble(y.Text)));
                    step++;
                }
                else
                {
                    punkty.Add(new Point(System.Convert.ToDouble(x.Text), System.Convert.ToDouble(y.Text)));
                    pointsy.Add(punkty);
                    drawCasteljau(punkty, canva);
                    foreach (var point in punkty)
                    {
                        Line line = new Line()
                        {
                            X1 = System.Convert.ToDouble(point.X - 2),
                            X2 = System.Convert.ToDouble(point.X + 2),
                            Y1 = System.Convert.ToDouble(point.Y - 2),
                            Y2 = System.Convert.ToDouble(point.Y + 2),
                            Stroke = Brushes.Red,
                            StrokeThickness = 5
                        };
                        canva.Children.Add(line);
                    }
                    step = 1;
                    punkty = new List<Point>();
                }
            }
            else
            {
                if (step <= range)
                {
                    punkty.Add(new Point(System.Convert.ToDouble(x.Text), System.Convert.ToDouble(y.Text)));
                    step++;
                }
                else
                {
                    punkty.Add(new Point(System.Convert.ToDouble(x.Text), System.Convert.ToDouble(y.Text)));
                    pointsy.Add(punkty);
                    drawCasteljau(punkty, canva);
                    foreach (var point in punkty)
                    {
                        Line line = new Line()
                        {
                            X1 = System.Convert.ToDouble(point.X - 2),
                            X2 = System.Convert.ToDouble(point.X + 2),
                            Y1 = System.Convert.ToDouble(point.Y - 2),
                            Y2 = System.Convert.ToDouble(point.Y + 2),
                            Stroke = Brushes.Red,
                            StrokeThickness = 5
                        };
                        canva.Children.Add(line);
                    }
                    step = 1;
                    punkty = new List<Point>();
                    RemoveFromCanva(pointsy[indeksKrzywej], canva);
                }
            }
        }

        private void RemoveFromCanva(List<Point> points, Canvas canva)
        {
            var lines = ListOfLines(points);
            foreach (var line in lines)
            {
                canva.Children.Clear();
            }
        }

        private void canva_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Draw.IsChecked == true)
            {
                if (step < range)
                {
                    punkty.Add(e.GetPosition(this.canva));
                    step++;
                }
                else
                {
                    punkty.Add(e.GetPosition(this.canva));
                    pointsy.Add(punkty);
                    drawCasteljau(punkty, canva);
                    foreach (var point in punkty)
                    {
                        Line line = new Line()
                        {
                            X1 = System.Convert.ToDouble(point.X - 2),
                            X2 = System.Convert.ToDouble(point.X + 2),
                            Y1 = System.Convert.ToDouble(point.Y - 2),
                            Y2 = System.Convert.ToDouble(point.Y + 2),
                            Stroke = Brushes.Red,
                            StrokeThickness = 5
                        };
                        canva.Children.Add(line);
                    }
                    step = 1;
                    punkty = new List<Point>();
                }
            }
            else
            {
                Point tmp = e.GetPosition(this.canva);
                for (int i = 0; i < pointsy.Count; i++)
                {
                    foreach (var x in pointsy[i])
                    {
                        if (tmp.X > x.X - 5 && tmp.X < x.X + 5 && tmp.Y > x.Y - 5 && tmp.Y < x.Y + 5)
                        {
                            indeksKrzywej = i;
                            indeksPunktu = pointsy[i].IndexOf(x);
                        }
                    }
                }
            }
        }

        private List<Line> ListOfLines(List<Point> points)
        {
            Point tmp, prev = new Point(-1, -1);
            List<Line> bezierLines = new List<Line>();
            for (double t = 0; t <= 1; t += 0.001)
            {
                tmp = getCasteljauPoint(points.Count - 1, 0, t, points);
                if (prev.X != -1 && prev.Y != -1)
                {
                    Line l = new Line();
                    l.Stroke = Brushes.Black;
                    l.StrokeThickness = 2;

                    l.X1 = prev.X;
                    l.Y1 = prev.Y;
                    l.X2 = tmp.X;
                    l.Y2 = tmp.Y;

                    bezierLines.Add(l);
                }

                prev = tmp;
            }

            tmp = getCasteljauPoint(points.Count - 1, 0, 1, points);

            Line line = new Line();
            line.Stroke = Brushes.Black;
            line.StrokeThickness = 2;

            line.X1 = prev.X;
            line.Y1 = prev.Y;
            line.X2 = tmp.X;
            line.Y2 = tmp.Y;

            bezierLines.Add(line);

            return bezierLines;
        }

        private void rang_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Mod.IsChecked == true)
            {
                return;
            }

            int tmp;
            System.Int32.TryParse(rang.Text, out tmp);
            range = tmp > 3 ? tmp : 3;
            step = 1;
            punkty = new List<Point>();
        }

        public static List<Line> drawCasteljau(List<Point> points, Canvas canvas)
        {
            Point tmp, prev = new Point(-1, -1);
            List<Line> bezierLines = new List<Line>();
            for (double t = 0; t <= 1; t += 0.01)
            {
                tmp = getCasteljauPoint(points.Count - 1, 0, t, points);
                if (prev.X != -1 && prev.Y != -1)
                {
                    Line l = new Line();
                    l.Stroke = Brushes.Black;
                    l.StrokeThickness = 2;

                    l.X1 = prev.X;
                    l.Y1 = prev.Y;
                    l.X2 = tmp.X;
                    l.Y2 = tmp.Y;

                    bezierLines.Add(l);
                    canvas.Children.Add(l);
                }

                prev = tmp;
            }

            tmp = getCasteljauPoint(points.Count - 1, 0, 1, points);

            Line line = new Line();
            line.Stroke = Brushes.Black;
            line.StrokeThickness = 2;

            line.X1 = prev.X;
            line.Y1 = prev.Y;
            line.X2 = tmp.X;
            line.Y2 = tmp.Y;

            bezierLines.Add(line);
            canvas.Children.Add(line);

            return bezierLines;
        }

        private void canva_MouseMove(object sender, MouseEventArgs e)
        {
            if (System.Windows.Input.Mouse.LeftButton == MouseButtonState.Pressed && Mod.IsChecked == true)
            {
                RemoveFromCanva(pointsy[indeksKrzywej], canva);
                pointsy[indeksKrzywej][indeksPunktu] = e.GetPosition(this.canva);
                drawCasteljau(pointsy[indeksKrzywej], canva);
                foreach (var point in pointsy[indeksKrzywej])
                {
                    Line line = new Line()
                    {
                        X1 = System.Convert.ToDouble(point.X - 2),
                        X2 = System.Convert.ToDouble(point.X + 2),
                        Y1 = System.Convert.ToDouble(point.Y - 2),
                        Y2 = System.Convert.ToDouble(point.Y + 2),
                        Stroke = Brushes.Red,
                        StrokeThickness = 5
                    };
                    canva.Children.Add(line);
                }
            }
        }

        public static Point getCasteljauPoint(int r, int i, double t, List<Point> points)
        {
            if (r == 0) return points[i];

            Point p1 = getCasteljauPoint(r - 1, i, t, points);
            Point p2 = getCasteljauPoint(r - 1, i + 1, t, points);

            return new Point((int)((1 - t) * p1.X + t * p2.X), (int)((1 - t) * p1.Y + t * p2.Y));
        }
    }
}
