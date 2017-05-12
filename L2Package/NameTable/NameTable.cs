using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace L2Package
{
    /// <summary>
    /// String based value with additional properties.
    /// </summary>
    internal class Name
    {
        /// <summary>
        /// Serial size of a name record.
        /// </summary>
        public int NameSize;
        /// <summary>
        /// String human readable value.
        /// </summary>
        public string Value;
        /// <seealso cref="L2Package.ObjectFlags"/>
        public int Flags;
        /// <summary>
        /// Offset in bytes
        /// </summary>
        public int Offset;

        public override string ToString()
        {
            return Value;
        }

        /// <summary>
        /// Serial size of an object
        /// </summary>
        public int Size
        {
            get
            {
                return 6 + Value.Length;
            }
        }
        private Name() { }

        /// <summary>
        /// Deserializes a Name object from bytes of package
        /// </summary>
        /// <param name="header">Header of a Package</param>
        /// <param name="cache">Decrypted bytes of a package. Use PackageReader to read and decrypt it.</param>
        /// <param name="Offset">Offset in bytes within a package file</param>
        public Name(IHeader header, byte[] cache, int Offset)
        {
            int LastOffset = Offset;
            NameSize = cache[LastOffset];
            LastOffset++; //byte
                          //Item.Name = BitConverter.ToString(cache, i + LastOffset, Item.NameSize);
            Value = Encoding.Default.GetString(cache, LastOffset, NameSize - 1);
            LastOffset += NameSize; // string length of Name with zero terminator
            Flags = BitConverter.ToInt32(cache, LastOffset);
            LastOffset += 4; // 32 bit int for flags
        }

        public static implicit operator string(Name name)
        {
            return name.Value;
        }

        public static implicit operator Name(string txt)
        {
            return new Name()
            {
                Value = txt,
                NameSize = txt.Length,
                Flags = 0
            };
        }
    }
    /// <summary>
    /// The first and most simple one of the three package tables is the name-table. 
    /// The name-table can be considered an index of all unique names used for 
    /// objects and references within the file. Later on, you’ll often find indexes 
    /// into this table instead of a string containing the object-name.
    /// </summary>
    public class NameTable : INameTable
    {
        const int NcSoftHeaderSize = 28;

        private List<Name> EntryTable
        {
            set
            {
                lock (_EntryTable)
                {
                    _EntryTable = value;
                }
            }
            get
            {
                lock (_EntryTable)
                {
                    return _EntryTable;
                }
            }
        }
        private List<Name> _EntryTable;


        /// <summary>
        /// Deserializes a NameTable from bytes of package
        /// </summary>
        /// <param name="header">Header of a Package</param>
        /// <param name="cache">Decrypted bytes of a package. Use PackageReader to read and decrypt it.</param>
        /// <param name="Offset">Offset in bytes within a package file</param>
        public NameTable(IHeader header, byte[] cache)
        {
            _EntryTable = new List<Name>();
            int LastOffset = NcSoftHeaderSize + header.NameOffset;
            for (int i = 0; i < header.NameCount; i++)
            {
                EntryTable.Add(new Name(header, cache, LastOffset));
                LastOffset += EntryTable.Last().Size;
            }
        }
        /// <summary>
        /// Reports the zero-based index of the first occurrence of a specified Name string within this table. 
        /// The method returns -1 if the Name record is not found in this table.
        /// </summary>
        /// <param name="needle">An string object to look for.</param>
        /// <returns>
        /// Positive zero-based index of the first occurrence of a specified Name string.
        /// -1 if is not found
        /// </returns>
        public int IndexOf(string needle)
        {
            return EntryTable.FindIndex(N => N == needle);
        }


        /// <summary>
        /// Returns an Name record at specified Compact-int index
        /// </summary>
        /// <param name="i">Position in table</param>
        /// <returns>Name record at specified position</returns>
        public string this[Index index]
        {
            get
            {
                return this[index.Value].ToString();
            }

            set
            {
                throw new WriteToReadOnlyException();
            }
        }
        /// <summary>
        /// Returns an Name record at specified integer index
        /// </summary>
        /// <param name="i">Position in table</param>
        /// <returns>Name record at specified position</returns>
        public string this[int index]
        {
            get
            {
                if (index < 0 || index >= EntryTable.Count)
                    throw new ArgumentOutOfRangeException();
                return EntryTable[index].ToString();
            }

            set
            {
                throw new WriteToReadOnlyException();
            }
        }
        /// <summary>
        /// No. Of Name records in table.
        /// </summary>
        public int Count
        {
            get
            {
                return EntryTable.Count();
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
                return true;
            }
        }
        /// <summary>
        /// Copies all the elements of the current NameTable to the 
        /// specified one-dimensional array starting at the specified destination array index. 
        /// The index is specified as a 32-bit integer.
        /// </summary>
        /// <param name="array">Destination array</param>
        /// <param name="index">Zero-based starting index in destination array</param>
        public void CopyTo(Array array, int index)
        {
            foreach (Name item in EntryTable)
                array.SetValue(item.ToString(), index++);
        }
        /// <summary>
        /// Returns an enumerator that iterates through an NameTable.
        /// </summary>
        /// <returns>NameTableEnumerator<Import></returns>
        public IEnumerator GetEnumerator()
        {
            return new NameTableEnumerator(EntryTable);

        }
        /// <summary>
        /// Returns an enumerator that iterates through an NameTable.
        /// </summary>
        /// <returns>NameTableEnumerator<string></returns>
        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            return new NameTableEnumerator(EntryTable);
        }

    }
    /// <summary>
    /// An enumerator that iterates through an NameTable.
    /// </summary>
    /// <typeparam name="Import">Import record in NameTable</typeparam>
    public class NameTableEnumerator : IEnumerator<string>
    {
        private List<Name> EntryTable;
        private int Cursor;
        internal NameTableEnumerator(List<Name> et)
        {
            this.EntryTable = et;
            Cursor = -1;
        }
        /// <summary>
        /// Returns Iterator current object
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">
        /// Thrown when iterator not set or after MoveNext() returned false
        /// </exception>
        object IEnumerator.Current
        {
            get
            {
                if ((Cursor < 0) || (Cursor == EntryTable.Count))
                    throw new InvalidOperationException();
                return EntryTable[Cursor].ToString();
            }
        }

        /// <summary>
        /// Returns Iterators current object
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">
        /// Thrown when iterator is not set or after MoveNext() returned false
        /// </exception>
        string IEnumerator<string>.Current
        {
            get
            {
                if ((Cursor < 0) || (Cursor == EntryTable.Count))
                    throw new InvalidOperationException();
                return EntryTable[Cursor].ToString();
            }
        }

        /// <summary>
        /// Moves iterator forward.
        /// </summary>
        /// <returns>
        /// Returns false if moved outside of a collection. 
        /// Otherwise returns true
        /// </returns>
        bool IEnumerator.MoveNext()
        {
            if (Cursor < EntryTable.Count)
                Cursor++;
            return (!(Cursor == EntryTable.Count));
        }

        /// <summary>
        /// Resets the Enumerator. Enumerators points to [-1] of a collection, so use MoveNext to use Current
        /// </summary>
        void IEnumerator.Reset()
        {
            Cursor = -1;
        }

        void IDisposable.Dispose()
        {
            return;
        }
    }
}
