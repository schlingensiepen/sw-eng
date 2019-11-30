using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadtLandFlussLib
{
    public class Fluss : NamedObject
    {
        private bool _male;

        public Fluss(string name, bool male) : base(name)
        {
            _male = male;
        }

        public bool Male
        {
            get { return _male; }
            set { _male = value; }
        }

        public override string getDescription()
        {
            return getShowTitle();
        }

        public override string getShowTitle()
        {
            return Name + "(" + (Male ? "m" : "w") + ")";
        }

        public string asPlacement()
        {
            return (Male ? "am " : "an der ") + Name;
        }

    }
}
