using System;
using System.Collections;
using System.Collections.Generic;

namespace L2Package
{
    public interface IExportTable : ICollection, IEnumerable<Export>
    {
        Export this[int index] { get; }
        Export this[Index index] { get; }

        //int Count { get; }
        //bool IsSynchronized { get; }
        //object SyncRoot { get; }

        //void CopyTo(Array array, int index);
        IEnumerable<Export> FindAll(Func<Export, bool> pre);
        //IEnumerator<Export> GetEnumerator();
        int IndexOf(Export NameTableIndex);
    }
}