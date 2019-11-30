using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StadtLandFlussLibDB
{
    public partial class Stadt : NamedObject
    {

        public Stadt(string name, Land liegtIn) : this()
        {
            Name = name;
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
