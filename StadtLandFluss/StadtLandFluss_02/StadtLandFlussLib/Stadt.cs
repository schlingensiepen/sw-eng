using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StadtLandFlussLib
{
    public class Stadt : NamedObject
    {
        private List<Fluss> _liegtAn = new List<Fluss>();

        public List<Fluss> LiegtAn {
            get { return _liegtAn; }
        }

        public Land LiegtIn
        {
            get { return _liegtIn; }
            set
            {
                if (value == null)
                    throw new Exception("Städte müssen in Ländern liegen.");
                _liegtIn = value;
            }
        }

        private Land _liegtIn;

        public Stadt(string name, Land liegtIn) : base(name)
        {
            LiegtIn = liegtIn;
        }


        public string getShortDescription()
        {
            return
            Name +
            LiegtAn.Select(f => f.asPlacement()).concat(
                " gelegen ",
                ", ",
                " und "
            );


        }

        public override string getDescription()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(getShortDescription());
            sb.Append(" gelegen in ");
            sb.Append(LiegtIn.getDescription());
            return sb.ToString();
        }

        public override string getShowTitle()
        {
            return getShortDescription();
        }
    }
}
