using System;
using System.Collections;
using System.Collections.Generic;

namespace L2Package
{
    internal class PropertiesEnumerator<T> : IEnumerator<Property>
    {
        private List<Property> properties;
        private int Cursor;

        public PropertiesEnumerator(List<Property> properties)
        {
            this.properties = properties;
            Cursor = -1;
        }

        public Property Current
        {
            get
            {
                if ((Cursor < 0) || (Cursor == properties.Count))
                    throw new IndexOutOfRangeException();
                return properties[Cursor];
            }
        }

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

        public bool MoveNext()
        {
            if (Cursor < properties.Count)
                Cursor++;
            return (!(Cursor == properties.Count));
        }

        public void Reset()
        {
            Cursor = -1;
        }
    }
}