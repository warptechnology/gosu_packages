using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace L2Package
{
    internal class Import
    {
        public Index ClassPackage { set; get; } // Package file in which the class of the object is defined; an index into the name-table
        public Index ClassName { set; get; } //Class of the object, i.e. ‘Texture’, ‘Palette’, ‘Package’, etc; an index into the name-table
        public int Package { set; get; } //Reference where the object resides; ObjectReference
        public Index ObjectName { set; get; } //The name of the object; an index into the name-table

        public Import()
        {
            ClassPackage = new Index();
            ClassName = new Index();
            Package = 0;
            ObjectName = new Index();
        }
        public Import(IHeader header, byte[] cache, int Offset) : this()
        {
            int ByteOffset = 28 + Offset;
            ClassPackage = new Index(cache, header.ImportOffset + ByteOffset);
            ByteOffset += ClassPackage.Size;
            ClassName = new Index(cache, header.ImportOffset + ByteOffset);
            ByteOffset += ClassName.Size;
            Package = BitConverter.ToInt32(cache, header.ImportOffset + ByteOffset);
            ByteOffset += 4;
            ObjectName = new Index(cache, header.ImportOffset + ByteOffset);
        }
        public int Size
        {
            get
            {
                return
                    ClassPackage.Size +
                    ClassName.Size +
                    4 +
                    ObjectName.Size;
            }
        }
    }
    internal class ImportTable : IImportTable
    {
        private List<Import> EntryTable { set; get; }

        public Import FindByNameRef(int Ref)
        {
            foreach (Import item in EntryTable)
                if (item.ObjectName == Ref)
                    return item;
            return null;
        }

        public int IndexOf(Import Imp)
        {
            return EntryTable.FindIndex(I => I.ObjectName == Imp.ObjectName);
        }

        public int Count
        {
            get
            {
                return EntryTable.Count;
            }
        }

        public object SyncRoot
        {
            get
            {
                return this;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        internal ImportTable(IHeader header, byte[] cache)
        {
            EntryTable = new List<Import>();
            int Offset = 0;
            for (int i = 0; i < header.ImportCount; i++)
            {
                EntryTable.Add(new Import(header, cache, Offset));
                Offset += EntryTable.Last().Size;
            }
        }

        public void CopyTo(Array array, int index)
        {
            foreach (Import item in EntryTable)
                array.SetValue(item, index++);
        }

        public IEnumerator GetEnumerator()
        {
            return new ImportTableEnumerator<Import>(EntryTable);
        }

        IEnumerator<Import> IEnumerable<Import>.GetEnumerator()
        {
            return new ImportTableEnumerator<Import>(EntryTable);
        }

        public Import this[Index i]
        {
            get
            {
                return this[i.Value];
            }
        }
        public Import this[int i]
        {
            get
            {
                return EntryTable[i];
            }
        }
    }

    public class ImportTableEnumerator<Import> : IEnumerator<Import>
    {
        private List<Import> EntryTable;
        private int Cursor;
        public ImportTableEnumerator(List<Import> it)
        {
            this.EntryTable = it;
            Cursor = -1;
        }
        /// <summary>
        /// Returns Iterator current object
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">
        /// Thrown when iterator not set or after MoveNext() returned false
        /// </exception>
        public Import Current
        {
            get
            {
                if ((Cursor < 0) || (Cursor == EntryTable.Count))
                    throw new IndexOutOfRangeException();
                return EntryTable[Cursor];
            }
        }

        /// <summary>
        /// Returns Iterators current object
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">
        /// Thrown when iterator is not set or after MoveNext() returned false
        /// </exception>
        object IEnumerator.Current
        {
            get
            {
                if ((Cursor < 0) || (Cursor >= EntryTable.Count))
                    throw new IndexOutOfRangeException();
                return EntryTable[Cursor];
            }
        }
        /// <summary>
        /// Does nothing. Just for interface compatability
        /// </summary>
        public void Dispose()
        {
            return;
        }
        /// <summary>
        /// Moves iterator forward.
        /// </summary>
        /// <returns>Returns false if moved outside of a collection. Otherwise returns true</returns>
        public bool MoveNext()
        {
            if (Cursor < EntryTable.Count)
                Cursor++;
            return (!(Cursor == EntryTable.Count));
        }

        /// <summary>
        /// Resets the Enumerator. Enumerators points to [-1] of a collection, so use MoveNext to use Current
        /// </summary>
        public void Reset()
        {
            Cursor = -1;
        }
    }
}
