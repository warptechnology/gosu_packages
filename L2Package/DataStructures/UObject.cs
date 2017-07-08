using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace L2Package.DataStructures
{
    public class UObject
    {
        public string Name { set; get; }
        public int Flags { set; get; }
        public string Group { set; get; }
        public string Class { set; get; }

        public UObject()
        {
            Group = "";
            Name = "";
            Flags = 0;
            Class = "";
        }
                
    }
}
