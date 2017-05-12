using System;
using System.Collections;
using System.Collections.Generic;

namespace L2Package
{
    internal interface IImportTable : ICollection, IEnumerable<Import>
    {
        Import this[int i] { get; }
        Import this[Index i] { get; }

        int Count { get; }
        bool IsSynchronized { get; }
        object SyncRoot { get; }

        void CopyTo(Array array, int index);
        IEnumerator GetEnumerator();
    }
}