using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;

namespace GraphicEditor
{
    class DefinedObject
    {
        public string type { get; set; }
        public double leftP { get; set; }
        public double rightP { get; set; }
        public double topP { get; set; }
        public double bottomP { get; set; }

        public DefinedObject() { }
        public DefinedObject(string type, double leftP, double rightP, double topP, double bottomP)
        {
            this.type = type;
            this.leftP = leftP;
            this.rightP = rightP;
            this.topP = topP;
            this.bottomP = bottomP;
        }
    }
}
