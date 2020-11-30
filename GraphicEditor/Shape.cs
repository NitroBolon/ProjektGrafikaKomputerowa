using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace GraphicEditor
{
    public class Shape
    {
        public List<Point> points { get; set; } = new List<Point>();

        public Shape() {}

        public Shape(List<Point> points)
        {
            this.points = points;
        }

        public void Move(int x, int y, Canvas canva)
        {
            for (int i=0; i<points.Count; i++)
            {
                points[i] = new Point { X = points[i].X + x, Y = points[i].Y + y };
            }
        }

        public void Rotate(Point center, double angle, Canvas canva)
        {
            for (int i = 0; i < points.Count; i++)
            {
                points[i] = RotatePoint(points[i], center, angle);
            }
        }

        public void Scale(int x, int s, Canvas canva)
        {
            double sx = Convert.ToDouble(s);
            sx /= 1000;
            sx += x;
            List<vect> lista = new List<vect>();

            for (int i = 1; i < points.Count; i++)
            {
                lista.Add(new vect { vX = (int)((points[i].X - points[0].X)*sx), vY = (int)((points[i].Y - points[0].Y)*sx) });
            }

            for (int i = 1; i < points.Count; i++)
            {
                points[i] = new Point { X = (int)points[0].X + lista[i - 1].vX, Y = points[0].Y + lista[i - 1].vY };
            }
        }

        static Point RotatePoint(Point point, Point piv, double ang)
        {
            double rads = Math.Abs(ang) * (Math.PI / 180);
            double cos = Math.Cos(rads);
            double sin = Math.Sin(rads);
            return new Point
            {
                X = (int)(cos * (point.X - piv.X) - sin * (point.Y - piv.Y) + piv.X),
                Y = (int)(sin * (point.X - piv.X) + cos * (point.Y - piv.Y) + piv.Y)
            };
        }

        public void DrawShape(Canvas canva)
        {
            for(int i=0; i<points.Count - 1; i++)
            {
                canva.Children.Add(new Line()
                {
                    X1 = points[i].X,
                    X2 = points[i + 1].X,
                    Y1 = points[i].Y,
                    Y2 = points[i + 1].Y,
                    Stroke = System.Windows.Media.Brushes.Black,
                    StrokeThickness = 3
                });
            }
            canva.Children.Add(new Line()
            {
                X1 = points[0].X,
                X2 = points[points.Count - 1].X,
                Y1 = points[0].Y,
                Y2 = points[points.Count - 1].Y,
                Stroke = System.Windows.Media.Brushes.Black,
                StrokeThickness = 3
            });
        }

        public void MoveByMouse(int x, int y, Canvas canva)
        {
            List<vect> lista = new List<vect>();

            for (int i = 1; i < points.Count; i++)
            {
                lista.Add(new vect { vX = (int)points[i].X - (int)points[0].X, vY = (int)points[i].Y - (int)points[0].Y });
            }

            points[0] = new Point(x, y);

            for (int i = 1; i < points.Count; i++)
            {
                points[i] = new Point { X = lista[i-1].vX + points[0].X, Y = lista[i-1].vY + points[0].Y };
            }
        }

        public struct vect
        {
            public int vX;
            public int vY;
        }
    }
}
