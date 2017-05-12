using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Package
{
    enum PropertyType {
        None = 0,
        Byte = 1,
        Int = 2,
        Bool = 3,
        Float = 4,
        Object = 5,
        Name = 6,
        String = 7,                         // old type
        Class = 8,
        Array = 9,
        Struct = 10,
        Vector = 11,                        // not implemented => only seen as struct...
        Rotator = 12,                       // not implemented => only seen as struct...
        Str = 13,
        Map = 14,                           // not implemented
        FixedArray = 15                    // not implemented
    };
    public class Constants
    {
    }
}
