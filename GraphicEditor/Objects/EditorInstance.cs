using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicEditor.Objects
{
    public class EditorInstance
    {
        private List<Line> lines = new List<Line>();
        private List<Ellipse> circles = new List<Ellipse>();
        private List<Rectangle> rectangles = new List<Rectangle>();
        Canvas canva;

        public EditorInstance(Canvas canva) {
            this.canva = canva;
        }

        public void AddLine(Objects.Point pointA, Objects.Point pointB)
        {
            Line line = new Line()
            {
                X1 = pointA.x,
                X2 = pointB.x,
                Y1 = pointA.y,
                Y2 = pointB.y,
                Stroke = Brushes.Black,
                StrokeThickness = 6
            };
            canva.Children.Add(line);
        }
        public void AddCircle(Objects.Point center, int radius)
        {
            Ellipse circle = new Ellipse()
            {
                Width = radius * 2,
                Height = radius * 2,
                Stroke = Brushes.Black,
                StrokeThickness = 6
            };
            canva.Children.Add(circle);

            circle.SetValue(Canvas.LeftProperty, (double)center.x);
            circle.SetValue(Canvas.TopProperty, (double)center.y);
        }
        public void AddRectangle(Objects.Point pointA, Objects.Point pointB, Point center)
        {
            Rectangle rectangle = new Rectangle()
            {
                Width = Math.Abs(pointA.x - pointB.x),
                Height = Math.Abs(pointA.y - pointB.y),
                Stroke = Brushes.Black,
                StrokeThickness = 6
            };
            canva.Children.Add(rectangle);

            rectangle.SetValue(Canvas.LeftProperty, (double)center.x);
            rectangle.SetValue(Canvas.TopProperty, (double)center.y);
        }

        public UIElement GetLastElement()
        {
            return canva.Children[canva.Children.Count-1];
        }
    }
}
