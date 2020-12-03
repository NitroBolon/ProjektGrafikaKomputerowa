using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicEditor
{
    public class Kolor
    {
        public string nazwa { get; set; }
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }

        public Kolor() { }

        public Kolor(string nazwa, int R, int G, int B)
        {
            this.nazwa = nazwa;
            this.R = R;
            this.G = G;
            this.B = B;
        }
    }
}
