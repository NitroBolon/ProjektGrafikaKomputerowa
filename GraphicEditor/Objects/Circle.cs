using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicEditor.Objects
{
    class Circle
    {
        private Point center { get; set; }
        private int radius { get; set; }

        public Circle() { }
        public Circle(Point center, int radius)
        {
            this.center = center;
            this.radius = radius;
        }

        public void MoveCircle(int x, int y)
        {
            this.center.MovePoint(x, y);
        }
    }
}
