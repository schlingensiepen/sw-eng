using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadtLandFlussLibDB
{
    public class FlussBuilder
    {
        private string Name;
        private bool Male;

        public FlussBuilder setName(string name)
        {
            Name = name;
            return this;
        }
        public FlussBuilder setMale()
        {
            Male = true;
            return this;
        }
        public FlussBuilder setMale(bool value)
        {
            Male = value;
            return this;
        }
        public FlussBuilder resetMale()
        {
            Male = false;
            return this;
        }

        public Fluss create()
        {
            return new Fluss(Name, Male);
        }

    }
}
