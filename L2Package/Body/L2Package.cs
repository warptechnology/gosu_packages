using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using L2Package.DataStructures;
using System.Collections;

namespace L2Package
{
    internal class L2Package :  ICollection, IEnumerable<UObject>
    {
        public L2Package(string FilePath, IUnrealSerializer szr = null)
        {
            Path = FilePath;
            Reader = new PackageReader();
            Reader.Read(Path);
            Header = new Header(Reader.Bytes);
            NameTable = new NameTable(Header, Reader.Bytes);
            ImportTable = new ImportTable(Header, Reader.Bytes);
            ExportTable = new ExportTable(Header, Reader.Bytes);
            
        }
        public string Path { get; private set; }

        PackageReader Reader { set; get; }
        Header Header { set; get; }
        NameTable NameTable { set; get; }
        ImportTable ImportTable { set; get; }
        ExportTable ExportTable { set; get; }
        IUnrealSerializer Serializer { set; get; }

        

        public UObject TryGetOne(Export ex)
        {
            //определяем класс объекта
            throw new NotImplementedException();
        }


        public UObject this[string Name]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        internal UObject this[Export exp]
        {
            get
            {
                return Serializer.Deserialize(exp);
            }
        }

        public int Count
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public object SyncRoot
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsSynchronized
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator<UObject> IEnumerable<UObject>.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public class L2PackageEnumerator<UObject> : IEnumerator<UObject>
    {
        public UObject Current
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        object IEnumerator.Current
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }

}
