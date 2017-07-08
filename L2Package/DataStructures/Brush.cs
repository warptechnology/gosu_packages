using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace L2Package.DataStructures
{
    public class Brush: AActor
    {
        [UEExport]
        public ECSGOperation csg_operation { set; get; }
        [UEExport]
        public UColor Color { get; set; }
        [UEExport]
        public long poly_flags { set; get; } 
        [UEExport]
        public int bColored { get; set; }
        [UEExport]
        public ObjectRef brush { get; set; }
        [UEExport]
        public Brush()
        {
            Color = new UColor();
            brush = new ObjectRef();
        }
        
        
    }
}
