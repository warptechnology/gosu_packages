using L2Package.DataStructures;
using System;

namespace L2Package
{
    
    public interface IUnrealSerializer
    {
        void Initialize(IHeader Header, INameTable NameTable, IExportTable ExportTable, IImportTable ImportTable, byte[] body);
        UObject Deserialize(Export Ex);
        byte[] Serialize();
        bool Initialized { get; }
    }
}