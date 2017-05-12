using System;
using System.Collections;
using System.Collections.Generic;

namespace L2Package
{
    public interface INameTable : ICollection, IEnumerable<string>
    {
        string this[int index] { get; set; }
        string this[Index index] { get; set; }

        //int Count { get; }
        //bool IsSynchronized { get; }
        //object SyncRoot { get; }

        //void CopyTo(Array array, int index);
        //IEnumerator GetEnumerator();
        int IndexOf(string needle);
    }
}