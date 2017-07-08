using System;
using System.Collections;
using System.Collections.Generic;

namespace L2Package.Body
{

    /// <summary>
    /// An enumerator that iterates through an properties in RawObject.
    /// </summary>
    /// <returns>NameTableEnumerator<Import></returns>
    internal class PropertiesEnumerator<T> : IEnumerator<Property>
    {
        private List<Property> properties;
        private int Cursor;

        public PropertiesEnumerator(List<Property> properties)
        {
            this.properties = properties;
            Cursor = -1;
        }
        /// <summary>
        /// Returns enumerator current object
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">
        /// thrown when is out of range of RawObject
        /// </exception>
        public Property Current
        {
            get
            {
                if ((Cursor < 0) || (Cursor == properties.Count))
                    throw new IndexOutOfRangeException();
                return properties[Cursor];
            }
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
                if ((Cursor < 0) || (Cursor == properties.Count))
                    throw new IndexOutOfRangeException();
                return properties[Cursor];
            }
        }

        public void Dispose()
        {
            return;
        }
        /// <summary>
        /// Moves iterator forward.
        /// </summary>
        /// <returns>
        /// Returns false if moved outside of a collection. 
        /// Otherwise returns true
        /// </returns>
        public bool MoveNext()
        {
            if (Cursor < properties.Count)
                Cursor++;
            return (!(Cursor == properties.Count));
        }
        /// <summary>
        /// Resets the enumerator
        /// </summary>
        public void Reset()
        {
            Cursor = -1;
        }
    }
}