using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicEditor
{
    public class DefinedObject
    {
        public int type { get; set; }
        public double x1 { get; set; }
        public double y1 { get; set; }
        public double x2 { get; set; }
        public double y2 { get; set; }

        public DefinedObject() { }
        public DefinedObject(int typ, double x1, double y1, double x2, double y2)
        {
            this.type = typ;
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }

        public Ellipse DrawEllipse()
        {
            Ellipse circle = new Ellipse()
            {
                Width = Convert.ToInt32(Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2))),
                Height = Convert.ToInt32(Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2))),
                Stroke = Brushes.Black,
                StrokeThickness = 3
            };
            circle.SetValue(Canvas.LeftProperty, x1 - (double)circle.GetValue(Canvas.WidthProperty));
            circle.SetValue(Canvas.TopProperty, y1 - (double)circle.GetValue(Canvas.HeightProperty));
            return circle;
        }
        public Line DrawLine()
        {
            Line line = new Line()
            {
                X1 = x1,
                X2 = x2,
                Y1 = y1,
                Y2 = y2,
                Stroke = Brushes.Black,
                StrokeThickness = 3
            };

            return line;
        }
        public Rectangle DrawRectangle()
        {
            Rectangle rectangle = new Rectangle()
            {
                Width = Math.Abs(x1-x2),
                Height = Math.Abs(y1-y2),
                Stroke = Brushes.Black,
                StrokeThickness = 3
            };
            if(x1 > x2 && y1 <= y2)
            {
                rectangle.SetValue(Canvas.LeftProperty, x2);
                rectangle.SetValue(Canvas.TopProperty, y1);
            }
            else if (x1 > x2 && y1 > y2)
            {
                rectangle.SetValue(Canvas.LeftProperty, x2);
                rectangle.SetValue(Canvas.TopProperty, y2);
            }
            else if (x1 <= x2 && y1 <= y2)
            {
                rectangle.SetValue(Canvas.LeftProperty, x1);
                rectangle.SetValue(Canvas.TopProperty, y1);
            }
            else
            {
                rectangle.SetValue(Canvas.LeftProperty, x1);
                rectangle.SetValue(Canvas.TopProperty, y2);
            }

            return rectangle;
        }
    }
}
