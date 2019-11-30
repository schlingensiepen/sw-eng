using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadtLandFlussLibDB
{
    public partial class Fluss
    {

        public Fluss(string name, bool male) : this()
        {
            Name = name;
            Male = male;
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
