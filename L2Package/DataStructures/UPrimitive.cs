using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace L2Package.DataStructures
{
    public class UPrimitive : UObject
    {

        public Box bounding_box { set; get; }
        public Sphere bounding_sphere { set; get; }
        public UObject UObject
        {
            get
            {
                UObject obj = new UObject();
                obj.Flags = this.Flags;
                obj.Name = this.Name;
                return obj;
            }
        }

        public UPrimitive()
        {
            bounding_box = new Box();
            bounding_sphere = new Sphere();
        }

    }
}
