﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2Package.DataStructures
{
    public interface IUnrealExportable
    {
        string UnrealString { get; }
        string UnrealStringTemplate { set; get; }
        string[] PropertiesList { get; }
        void ResetTemplate();
    }
}
