using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace L2Package.DataStructures
{
    public enum ECSGOperation
    {
        CSG_Active = 0,
        CSG_Add = 1,
        CSG_Subtract = 2,
        CSG_Intersect = 3,
        CSG_Deintersect = 4,
        CSG_MAX = 5,
    };
}