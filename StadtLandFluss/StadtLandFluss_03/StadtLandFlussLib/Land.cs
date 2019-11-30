using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadtLandFlussLib
{
    public class Land : NamedObject
    {
        private Stadt _hauptStadt;

        public Stadt HauptStadt
        {
            get { return _hauptStadt; }
            set
            {
                if (value == null)
                    throw new Exception("Hauptstadt ungültig.");
                _hauptStadt = value;
            }
        }

        public Land(string name) : base(name)
        {
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
