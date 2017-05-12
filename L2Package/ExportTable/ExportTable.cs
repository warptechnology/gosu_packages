using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace L2Package
{
    /// <summary>
    /// Every object in the body of the file has a corresponding entry of Export object in Export table, with information like offset within the file, size etc.
    /// </summary>
    public class Export
    {
        /// <summary>
        /// Reference to ImportTable for Class of the object, i.e. ‘Texture’ or ‘Palette’ etc; stored as a ObjectReference
        /// </summary>
        public Index Class;
        /// <summary>
        /// Object Parent; again a ObjectReference
        /// </summary>
        public Index Super;
        /// <summary>
        /// Internal package/group of the object, i.e. ‘Floor’ for floor-textures; ObjectReference
        /// </summary>
        public int Group;
        /// <summary>
        /// The name of the object; an index into the name-table
        /// </summary>
        public Index NameTableRef;
        /// <summary>
        /// Flags for the object
        /// </summary>
        public int ObjectFlags;
        /// <summary>
        /// Total size of the object
        /// </summary>
        public Index SerialSize;
        /// <summary>
        /// Offset of the object; It is >0 if the SerialSize is larger 0. Else it's 0
        /// </summary>
        public Index SerialOffset;

        private int HeaderExportOffset;

        //public int ExportTableEntryOfset;

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
        /// <summary>
        /// Deserialize a single Export instance
        /// </summary>
        /// <param name="header">Header of the package.</param>
        /// <param name="cache">Decrypted bytes of a packagefile. Use Reader to decrypt</param>
        /// <param name="Offset">Ofset of a serialized Export object in cache</param>
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


        /// <summary>
        /// Serialized size (in bytes).
        /// </summary>
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
    /// <summary>
    /// The export-table is an index for all objects within the package. 
    /// Every object in the body of the file has a corresponding entry in this table, 
    /// with information like offset within the file etc.
    /// </summary>
    public class ExportTable : IExportTable
    {
        /// <summary>
        /// Deserializes ExportTable from package.
        /// </summary>
        /// <param name="header">Header of the package.</param>
        /// <param name="cache">Decrypted bytes of a package. Use Reader to decrypt</param>
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

        /// <summary>
        /// No. Of objects contained in ExportTable
        /// </summary>
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
        /// <summary>
        /// Copies all of Export objects in this instance of ExportTable
        /// to a specified position in an array.
        /// </summary>
        /// <param name="array">Destination array</param>
        /// <param name="index">Destination index</param>
        public void CopyTo(Array array, int index)
        {
            foreach (Export item in EntryTable)
                array.SetValue(item, index++);
        }

        /// <summary>
        /// Returns an enumerator that iterates through an ExportTable.
        /// </summary>
        /// <returns>ExportTableEnumerator<Export></returns>
        public IEnumerator<Export> GetEnumerator()
        {
            return new ExportTableEnumerator<Export>(EntryTable);
        }
        /// <summary>
        /// Returns an enumerator that iterates through an ExportTable.
        /// </summary>
        /// <returns>ExportTableEnumerator<Export></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ExportTableEnumerator<Export>(EntryTable);
        }
        /// <summary>
        /// Compact-int indexer for ExportTable
        /// </summary>
        /// <param name="index">Position in ExportTable</param>
        /// <returns>Export record for specified index</returns>
        public Export this[Index index]
        {
            get
            {
                return this[index.Value];
            }
        }
        /// <summary>
        /// Integer indexer for ExportTable
        /// </summary>
        /// <param name="index">Position in ExportTable</param>
        /// <returns>Export record for specified index</returns>
        public Export this[int index]
        {
            get
            {
                if (index < 0 || index >= EntryTable.Count)
                    throw new ArgumentOutOfRangeException();
                return EntryTable[index];
            }
        }
        /// <summary>
        /// Searches for the Export record with specified NameTable reference and returns 
        /// the index of its first occurrence in an ExportTable.
        /// </summary>
        /// <param name="NameTableIndex">Index of a string in a NameTable</param>
        /// <returns>returns 
        /// The index of first occurrence in an ExportTable
        /// </returns>
        public int IndexOf(int NameTableIndex)
        {
            return EntryTable.FindIndex(N => N.NameTableRef.Value == NameTableIndex);
        }
        /// <summary>
        /// Searches for the Export record with specified predicate and returns 
        /// all objects which sutisfy a predicate condition in an ExportTable.
        /// </summary>
        /// <param name="pre">Predicate for Export objects</param>
        /// <returns>returns 
        /// List of Export records which sutisfy a predicate.
        /// </returns>
        public IEnumerable<Export> FindAll(Func<Export, bool> pre)
        {
            List<Export> Ex = new List<Export>();
            Ex.AddRange(EntryTable.Where(pre));
            return Ex;
        }
        /// <summary>
        /// Searches for the Export record with specified NameTable reference and returns 
        /// that object.
        /// </summary>
        /// <param name="NameTableReference">Reference to the NameTable of desired object</param>
        /// <returns>returns 
        /// Export object.
        /// </returns>
        /// <example>
        /// Export Exp = ExportTable.Find(NameTable.IndexOf("StaticMeshActor1"));
        /// </example>
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
