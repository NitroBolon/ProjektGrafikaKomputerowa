using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicEditor.Objects
{
    class Rectangle
    {
        private Point upperLeft { get; set; }
        private Point upperRight { get; set; }
        private Point lowerLeft { get; set; }
        private Point lowerRight { get; set; }

        public Rectangle() { }
        public Rectangle(Point upperLeft, Point upperRight, Point lowerLeft, Point lowerRight)
        {
            this.upperLeft = upperLeft;
            this.upperRight = upperRight;
            this.lowerLeft = lowerLeft;
            this.lowerRight = lowerRight;
        }

        public void MoveRectangle(int x, int y)
        {
            this.upperLeft.MovePoint(x, y);
            this.upperRight.MovePoint(x, y);
            this.lowerLeft.MovePoint(x, y);
            this.lowerRight.MovePoint(x, y);
        }
    }
}
