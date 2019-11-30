using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadtLandFlussLibDB
{
    public partial class Land : NamedObject
    {

        public Land(string name) : this()
        {
            Name = name;
        }

        public override string getDescription()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Name);
            sb.Append(" dessen Hauptstadt ist ");
            sb.Append(HauptStadt.getShortDescription());
            return sb.ToString();
        }

        public override string getShowTitle()
        {
            return Name;
        }
    }
}
