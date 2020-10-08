using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicEditor.Objects
{
    class Line
    {
        private Point pointA { get; set; }
        private Point pointB { get; set; }

        public Line() { }
        public Line(Point pointA, Point pointB)
        {
            this.pointA = pointA;
            this.pointB = pointB;
        }

        public void MoveLine(int x, int y)
        {
            this.pointA.MovePoint(x, y);
            this.pointB.MovePoint(x, y);
        }
    }
}
