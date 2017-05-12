using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace L2Package
{
    public class Export
    {
        public Index Class;
        public Index Super;
        public int Group;
        public Index NameTableRef;
        public int ObjectFlags;
        public Index SerialSize;
        public Index SerialOffset;
        public int HeaderExportOffset;

        public int ExportTableEntryOfset;

        public Export()
        {
            Class = new Index();
            Super = new Index();
            Group = 0;
            NameTableRef = new Index();
            ObjectFlags = 0;
            SerialSize = new Index();
            SerialOffset = new Index();
        }
        public Export(IHeader header, byte[] cache, int Offset)
        {
            HeaderExportOffset = header.ExportOffset;
            int ByteOffset = Offset + 28; // don't forget about 28 bytes of NcSoftHeader!!
            Class = new Index(cache, HeaderExportOffset + ByteOffset);
            ByteOffset += Class.Size;

            Super = new Index(cache, HeaderExportOffset + ByteOffset);
            ByteOffset += Super.Size;

            Group = BitConverter.ToInt32(cache, HeaderExportOffset + ByteOffset);
            ByteOffset += 4;

            NameTableRef = new Index(cache, HeaderExportOffset + ByteOffset);
            ByteOffset += NameTableRef.Size;

            ObjectFlags = BitConverter.ToInt32(cache, HeaderExportOffset + ByteOffset);
            ByteOffset += 4;

            SerialSize = new Index(cache, HeaderExportOffset + ByteOffset);
            ByteOffset += SerialSize.Size;

            if (SerialSize.Value > 0)
            {
                SerialOffset = new Index(cache, HeaderExportOffset + ByteOffset);
                ByteOffset += SerialOffset.Size;
            }
        }
        public int Size
        {
            get
            {
                return 8 +
                    Class.Size +
                    Super.Size +
                    NameTableRef.Size +
                    SerialSize.Size +
                    SerialOffset.Size;
            }
        }
    }
    public class ExportTable : IExportTable
    {
        public ExportTable(IHeader header, byte[] cache)
        {
            EntryTable = new List<Export>();
            EntryTable.Clear();
            int ByteOffset = 0;
            for (int i = 0; i < header.ExportCount; i++)
            {
                EntryTable.Add(new Export(header, cache, ByteOffset));
                ByteOffset += EntryTable.Last().Size;
            }
        }
        private List<Export> EntryTable { set; get; }

        public int Count
        {
            get
            {
                return EntryTable.Count;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        public object SyncRoot
        {
            get
            {
                return this;
            }
        }
        
        public void CopyTo(Array array, int index)
        {
            foreach (Export item in EntryTable)
                array.SetValue(item, index++);
        }

        public IEnumerator<Export> GetEnumerator()
        {
            return new ExportTableEnumerator<Export>(EntryTable);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ExportTableEnumerator<Export>(EntryTable);
        }
        public Export this[Index index]
        {
            get
            {
                return this[index.Value];
            }
        }
        public Export this[int index]
        {
            get
            {
                if (index < 0 || index >= EntryTable.Count)
                    throw new ArgumentOutOfRangeException();
                return EntryTable[index];
            }
        }

        public int IndexOf(int NameTableIndex)
        {
            return EntryTable.FindIndex(N => N.NameTableRef.Value == NameTableIndex);
        }

        public List<Export> FindAll(Func<Export, bool> pre)
        {
            List<Export> Ex = new List<Export>();
            Ex.AddRange(EntryTable.Where(pre));
            return Ex;
        }
        public Export Find(int NameTableReference)
        {
            foreach (Export Ex in EntryTable)
                if (Ex.NameTableRef == NameTableReference)
                    return Ex;
            return null;
        }
    }
    
    internal class WriteToReadOnlyException : InvalidOperationException
    {
        public WriteToReadOnlyException() { }
        public WriteToReadOnlyException(string message) : base(message) { }
        public WriteToReadOnlyException(string message, Exception innerException) : base(message, innerException) { }
        protected WriteToReadOnlyException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
