using System;
using System.Collections;
using System.Collections.Generic;

namespace L2Package
{
    public class ExportTableEnumerator<Export> : IEnumerator<Export>
    {

        private List<Export> EntryTable;
        private int Cursor;
        internal ExportTableEnumerator(List<Export> et)
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
        public object Current
        {
            get
            {
                if ((Cursor < 0) || (Cursor == EntryTable.Count))
                    throw new IndexOutOfRangeException();
                return EntryTable[Cursor];
            }
        }

        Export IEnumerator<Export>.Current
        {
            get
            {
                if ((Cursor < 0) || (Cursor == EntryTable.Count))
                    throw new InvalidOperationException();
                return EntryTable[Cursor];
            }
        }

        public bool MoveNext()
        {
            if (Cursor < EntryTable.Count)
                Cursor++;
            return (!(Cursor == EntryTable.Count));
        }

        public void Reset()
        {
            Cursor = -1;
        }

        public void Dispose()
        {
            return;
        }
    }
}