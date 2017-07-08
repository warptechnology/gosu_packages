using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using L2Package.DataStructures;
using System.Collections;

namespace L2Package
{
    internal class L2Package : ICollection, IEnumerable<UObject>
    {
        /// <summary>
        /// Reads specified package.
        /// </summary>
        /// <param name="FilePath">Absolute path to a package.</param>
        /// <param name="szr">Serializer class for a desired version of package.</param>
        public L2Package(string FilePath, IUnrealSerializer szr = null)
        {
            Path = FilePath;
            Reader = new PackageReader();
            Reader.Read(Path);
            Header = new Header(Reader.Bytes);
            NameTable = new NameTable(Header, Reader.Bytes);
            ImportTable = new ImportTable(Header, Reader.Bytes);
            ExportTable = new ExportTable(Header, Reader.Bytes);
            Serializer = szr;
        }
        /// <summary>
        /// Returns the absolute path for the package.
        /// </summary>
        public string Path { get; private set; }

        PackageReader Reader { set; get; }
        Header Header { set; get; }
        NameTable NameTable { set; get; }
        ImportTable ImportTable { set; get; }
        ExportTable ExportTable { set; get; }
        IUnrealSerializer Serializer { set; get; }












        /// <summary>
        /// Returns an UObject stored in package body
        /// </summary>
        /// <param name="Index">Index of an UObject</param>
        /// <returns>UObject or one of its descendants</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown if Index is less then zero or is more the number of elements in package body;
        /// </exception>

        public UObject this[int Index]
        {
            get
            {
                if (Index < 0 || Index > ExportTable.Count)
                    throw new IndexOutOfRangeException();
                Export Ex = ExportTable[Index];
                return Serializer.Deserialize(Ex);
            }
        }

        /// <summary>
        /// Returns an UObject stored in package body
        /// </summary>
        /// <param name="exp">An Export object linked to an UObject</param>
        /// <returns>UObject or one of its descendants</returns>

        internal UObject this[Export exp]
        {
            get
            {
                return Serializer.Deserialize(exp);
            }
        }
        /// <summary>
        /// Number of internal and external object stored in package body.
        /// </summary>
        public int Count
        {
            get
            {
                return ExportTable.Count();
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
        /// Copies all the elements of the current package
        /// to the specified one-dimensional
        /// array starting at the specified destination array index. 
        /// The index is specified as a 32-bit integer. 
        /// </summary>
        /// <param name="array">
        ///   Type: System.Array
        ///   The one-dimensional array that is the destination of the elements 
        ///   copied from the current array.
        /// </param>
        /// <param name="index">
        ///   Type: System.Int32
        ///   A 32-bit integer that represents the index in destination array at which copying begins.
        /// </param>
        /// <exception cref="ArgumentNullException">Array is null</exception>

        public void CopyTo(Array array, int index)
        {
            if (array == null) throw new ArgumentNullException("Destination array is null");
            if (index >= array.Length || index < 0) throw new ArgumentOutOfRangeException("index must be greater then zero and less then size of array.");
            if (this.Count > array.Length - index) throw new ArgumentException();
            Type valueType = array.GetType();
            if (!(typeof(UObject)).IsAssignableFrom(valueType.GetElementType()) || array.Rank != 1)
                throw new ArgumentException("Array can only be an UObject array");
            for (int i = 0; i < ExportTable.Count; i++)
            {
                array.SetValue(Serializer.Deserialize(ExportTable[i]), index + i);
            }
        }


        /// <summary>
        /// Returns an enumerator that iterates through a package.
        /// </summary>
        /// <returns>
        /// An UobjectEnumerator object that can be used to iterate through the package.
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            return new L2PackageEnumerator(ExportTable, Serializer);
        }
        /// <summary>
        /// Returns an enumerator that iterates through a package.
        /// </summary>
        /// <returns>
        /// An UobjectEnumerator object that can be used to iterate through the package.
        /// </returns>
        IEnumerator<UObject> IEnumerable<UObject>.GetEnumerator()
        {
            return new L2PackageEnumerator(ExportTable, Serializer);
        }
    }
    /// <summary>
    /// An UobjectEnumerator object that can be used to iterate through the package.
    /// </summary>
    /// <typeparam name="UObject">Base class for all high level objects in package body</typeparam>
    public class L2PackageEnumerator : IEnumerator<UObject>
    {
        private IExportTable et;
        private int Cursor;
        IUnrealSerializer Serilizer;

        internal L2PackageEnumerator(IExportTable iet, IUnrealSerializer szr)
        {
            et = iet;
            Cursor = -1;
            Serilizer = szr;
        }

        /// <summary>
        /// Value stored in a package at current iterators position
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">
        /// Thrown when iterator not set or after MoveNext() returned false
        /// </exception>
        public UObject Current
        {
            get
            {
                if ((Cursor < 0) || (Cursor == et.Count))
                    throw new IndexOutOfRangeException();
                return Serilizer.Deserialize(et[Cursor]);
            }
        }

        /// <summary>
        /// Value stored in a package at current iterators position
        /// </summary>
        object IEnumerator.Current
        {
            get
            {
                if ((Cursor < 0) || (Cursor == et.Count))
                    throw new IndexOutOfRangeException();
                return Serilizer.Deserialize(et[Cursor]);
            }
        }

        public void Dispose()
        {
            return;
        }
        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// True if the enumerator was successfully advanced to the next element; 
        /// false if the enumerator has passed the end of the package.
        /// </returns>
        public bool MoveNext()
        {
            if (Cursor < et.Count)
                Cursor++;
            return (!(Cursor == et.Count));
        }

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the package.
        /// </summary>

        public void Reset()
        {
            Cursor = -1;
        }
    }

}
