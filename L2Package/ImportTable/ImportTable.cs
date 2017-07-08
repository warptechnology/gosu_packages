using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace L2Package
{
    public class Import
    {
        /// <summary>
        /// Package file in which the class of the object is defined; 
        /// an index into the name-table
        /// </summary>
        public Index ClassPackage { set; get; }
        /// <summary>
        /// Class of the object, i.e. ‘Texture’, ‘Palette’, ‘Package’, etc; 
        /// an index into the name-table
        /// </summary>
        public Index ClassName { set; get; }
        /// <summary>
        /// Reference where the object resides; ObjectReference
        /// </summary>
        public int Package { set; get; }
        /// <summary>
        /// The name of the object; an index into the name-table
        /// </summary>
        public Index ObjectName { set; get; } 
        

        public Import()
        {
            ClassPackage = new Index();
            ClassName = new Index();
            Package = 0;
            ObjectName = new Index();
        }
        /// <summary>
        /// Deserializes ImportObject from bytes in package
        /// </summary>
        /// <param name="header">Header of a package</param>
        /// <param name="cache">Decrypted bytes of a package. Use PackageReader to read and decrypt.</param>
        /// <param name="Offset">Offset in cache</param>
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
        /// <summary>
        /// Serialized size of Import row in table. (in bytes)
        /// </summary>
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
    /// <summary>
    /// The third table holds references to objects in external packages. 
    /// For example, a texture might have a DetailTexture 
    /// (which makes for the nice structure if have a very close look at a texture). 
    /// Now, these DetailTextures are all stored in a single package 
    /// (as they are used by many different textures in different package files). 
    /// The property of the texture object only needs to store an index into the import-table then 
    /// as the entry in the import-table already points to the DetailTexture in the other package.
    /// </summary>
    internal class ImportTable : IImportTable
    {
        private List<Import> EntryTable { set; get; }


        /// <summary>
        /// Searches for an Import record.
        /// </summary>
        /// <example>
        /// //returns Import object for a Class "StaticMeshActor"
        /// Import Imp = ImportTable.FindByNameRef(NT.IndexOf("StaticMeshActor"));
        /// </example>
        /// <param name="Ref"></param>
        /// <returns>Import record if exists. Othervise returns null</returns>
        public Import FindByNameRef(int Ref)
        {
            foreach (Import item in EntryTable)
                if (item.ObjectName == Ref)
                    return item;
            return null;
        }
        /// <summary>
        /// Reports the zero-based index of the first occurrence of a specified Import object within this instance. 
        /// The method returns -1 if the Import record is not found in this instance.
        /// </summary>
        /// <param name="needle">An Import object to look for.</param>
        /// <returns>
        /// Positive zero-based index of the first occurrence of a specified Import object.
        /// -1 if the Import record is not found
        /// </returns>
        public int IndexOf(Import needle)
        {
            return EntryTable.FindIndex(I => I.ObjectName == needle.ObjectName);
        }

        /// <summary>
        /// No. Of Import records in table.
        /// </summary>
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

        /// <summary>
        /// Deserialises ImportTable from decrypted package.
        /// </summary>
        /// <param name="header">Deserialized header of a package.</param>
        /// <param name="cache">Decrypted bytes of a package.</param>
        /// <seealso cref="PackageReader"/>
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
        /// <summary>
        /// Copies all the elements of the current ImportTable to the 
        /// specified one-dimensional array starting at the specified destination array index. 
        /// The index is specified as a 32-bit integer.
        /// </summary>
        /// <param name="array">Destination array</param>
        /// <param name="index">Zero-based starting index in destination array</param>
        public void CopyTo(Array array, int index)
        {
            foreach (Import item in EntryTable)
                array.SetValue(item, index++);
        }

        /// <summary>
        /// Returns an enumerator that iterates through an ImportTable.
        /// </summary>
        /// <returns>ImportTableEnumerator<Import></returns>
        public IEnumerator GetEnumerator()
        {
            return new ImportTableEnumerator<Import>(EntryTable);
        }

        /// <summary>
        /// Returns an enumerator that iterates through an ImportTable.
        /// </summary>
        /// <returns>ImportTableEnumerator<Import></returns>
        IEnumerator<Import> IEnumerable<Import>.GetEnumerator()
        {
            return new ImportTableEnumerator<Import>(EntryTable);
        }
        /// <summary>
        /// Returns an Import record at specified Compact-int index
        /// </summary>
        /// <param name="i">Position in table</param>
        /// <returns>Import record at specified position</returns>
        public Import this[Index i]
        {
            get
            {
                return this[i.Value];
            }
        }
        /// <summary>
        /// Returns an Import record at specified integer index
        /// </summary>
        /// <param name="i">Position in table</param>
        /// <returns>Import record at specified position</returns>
        public Import this[int i]
        {
            get
            {
                return EntryTable[i];
            }
        }
    }
    /// <summary>
    /// An enumerator that iterates through an ImportTable.
    /// </summary>
    /// <typeparam name="Import">Import record in ImportTable</typeparam>
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
