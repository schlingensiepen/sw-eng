using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadtLandFlussLibDB
{
    public abstract partial class NamedObject
    {

        public NamedObject()
        {
            Name = null;
        }

        public NamedObject(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return getShowTitle();
        }

        public abstract string getDescription();
        public abstract string getShowTitle();
    }
}

