using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using L2Package.DataStructures;
using System.Collections;
using System.IO;

namespace L2Package
{

    /// <summary>
    /// Serializer for Lineage 2 packages
    /// </summary>
    internal class L2BasicSerializer : IUnrealSerializer
    {
        IHeader Header { set; get; }
        INameTable NameTable { set; get; }
        IExportTable ExportTable { set; get; }
        IImportTable ImportTable { set; get; }

        byte[] Bytes { set; get; }
        private Dictionary<string, Type> TypeStrings;
        /// <summary>
        /// Initializing func for Serializer. 
        /// </summary>
        /// <param name="H">Header of the package</param>
        /// <param name="NT">NameTable of package.</param>
        /// <param name="ET">ExportTable of package.</param>
        /// <param name="IT">ImportTable of Package</param>
        /// <param name="body">Readed and decrypted bytes of package. </param>
        /// <seealso cref="L2Package.PackageReader"/>
        public void Initialize(IHeader H, INameTable NT, IExportTable ET, IImportTable IT, byte[] body)
        {
            Header = H;
            NameTable = NT;
            ExportTable = ET;
            ImportTable = IT;
            Bytes = body;
            Initialized = true;
        }
        public bool Initialized { get; private set; }

        public L2BasicSerializer()
        {
            //TODO: Пребито гвоздями. Установить на шарниры.
            TypeStrings = new Dictionary<string, Type>()
            {
                { "StaticMeshActor", typeof(StaticMeshActor) }
            };
            Factories = new Dictionary<Type, Factory>()
            {

            };
        }
        public UObject Deserialize(Export Exp)
        {
            UObject Object = new UObject();
            Type TypeOfObject = GetExportClass(Exp);

            return Object;
        }

        public byte[] Serialize()
        {
            throw new NotImplementedException();
        }

        internal Type GetExportClass(Export Exp)
        {
            return TypeStrings[NameTable[ImportTable[(Exp.Class.Value + 1) * -1].ObjectName]];
        }

        private delegate UObject Factory(byte[] b, int offset, int sz);
        Dictionary<Type, Factory> Factories { set; get; }
        
        StaticMeshActor DeserializeStaticMeshActor(byte[] Bs, int offset, int Size)
        {
            int Position = offset + 28;
            RawObject RO = new RawObject();
            RO.Header = new byte[15];
            Array.Copy(Bs, Position, RO.Header, 0, 15);
            Position += 15;
            while(Position < offset + 28 + Size)
            {
                Property Prop = new Property(Bs, Position);
                RO.Add(Prop);
                Position += Prop.Size;
            }
            
            return new StaticMeshActor();
        }
        
    }
    internal enum PropertyTypes
    {
        otNone = 0,
        otByte = 1,
        otInt = 2,
        otBool = 3,
        otFloat = 4,
        otObject = 5,
        otName = 6,
        otString = 7,
        otClass = 8,
        otArray = 9,
        otStruct = 10,
        otVector = 11,
        otRotator = 12,
        otStr = 13,
        otMap = 14,
        otFixedArray = 15,
        otExtendedValue = 0x00000100,
        otBuffer = otExtendedValue | 0,
        otWord = otExtendedValue | 1,
    }
    class A { int x; }
    class B : A{ int y; }
}
