using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicEditor.Objects
{
    public class Point
    {
        public int x { get; set; }
        public int y { get; set; }

        public Point() { }
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void MovePoint(int x, int y)
        {
            this.x += x;
            this.y += y;
        }
    }
}
