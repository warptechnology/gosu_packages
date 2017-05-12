using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace L2Package
{
    internal class Name
    {
        public int NameSize;
        public string Value;
        public int Flags;
        public int Offset;

        public override string ToString()
        {
            return Value;
        }

        public int Size
        {
            get
            {
                return 6 + Value.Length;
            }
        }
        private Name() { }

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
        public int IndexOf(string needle)
        {
            return EntryTable.FindIndex(N => N == needle);
        }
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
        public void CopyTo(Array array, int index)
        {
            foreach (Name item in EntryTable)
                array.SetValue(item.ToString(), index++);
        }
        public IEnumerator GetEnumerator()
        {
            return new NameTableEnumerator(EntryTable);

        }

        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            return new NameTableEnumerator(EntryTable);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new NameTableEnumerator(EntryTable);
        }
    }
    public class NameTableEnumerator : IEnumerator<string>
    {
        private List<Name> EntryTable;
        private int Cursor;
        internal NameTableEnumerator(List<Name> et)
        {
            this.EntryTable = et;
            Cursor = -1;
        }
        object IEnumerator.Current
        {
            get
            {
                if ((Cursor < 0) || (Cursor == EntryTable.Count))
                    throw new InvalidOperationException();
                return EntryTable[Cursor].ToString();
            }
        }

        string IEnumerator<string>.Current
        {
            get
            {
                if ((Cursor < 0) || (Cursor == EntryTable.Count))
                    throw new InvalidOperationException();
                return EntryTable[Cursor].ToString();
            }
        }

        bool IEnumerator.MoveNext()
        {
            if (Cursor < EntryTable.Count)
                Cursor++;
            return (!(Cursor == EntryTable.Count));
        }

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
