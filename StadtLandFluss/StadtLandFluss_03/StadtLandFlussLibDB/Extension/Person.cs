using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StadtLandFlussLibDB
{
    public partial class Person
    {
        public Person(
            string vorname, 
            string familienname,
            Stadt geborenIn,
            IEnumerable<Stadt> wohntIn
            ) : this()
        {
            Vorname = vorname;
            Familienname = familienname;
            GeborenIn = geborenIn;

            var wi = wohntIn.ToList();

            if (wi.Count == 0) 
                throw new Exception("Kein Wohnort angegeben.");

            wi.ForEach(s => WohntIn.Add(s));
        }

        public string getFullname()
        {
            return Vorname + " " + Familienname;
        }

        public override string ToString()
        {
            return getFullname();
        }

        public string getDescription()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(getFullname());
            sb.Append(" ist geboren in ");
            sb.Append(GeborenIn.getDescription());

            bool firstWohnort = true;
            foreach (var wohnort in WohntIn)
            {
                if (firstWohnort)
                {
                    sb.Append(" wohnhaft in ");
                    firstWohnort = false;
                }
                else
                {
                    sb.Append(" und ");
                }
                sb.Append(wohnort.getDescription());
            }

            return sb.ToString();
        }
    }
}
