using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StadtLandFlussLib
{
    public class Person
    {
        private string _vorname;
        private string _familienname;

        private List<Stadt> _wohnt_in = new List<Stadt>();
        private Stadt _geborenIn;

        public List<Stadt> WohntIn
        {
            get { return _wohnt_in; }
        }

        public Stadt GeborenIn
        {
            get { return _geborenIn; }
            set
            {
                if (value == null)
                    throw new Exception("Geburtsort ungültig.");
                _geborenIn = value;
            }
        }

        public string Vorname
        {
            get { return _vorname; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Vorname muss gesetzt werden.");
                _vorname = value;
            }
        }

        public string Familienname
        {
            get { return _familienname; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Familienname muss gesetzt werden.");
                _familienname = value;
            }
        }

        public Person(
            string vorname, 
            string familienname,
            Stadt geborenIn,
            IEnumerable<Stadt> wohntIn
            )
        {
            Vorname = vorname;
            Familienname = familienname;
            GeborenIn = geborenIn;

            var wi = wohntIn.ToArray();

            if (wi.Length == 0) 
                throw new Exception("Kein Wohnort angegeben.");

            WohntIn.AddRange(wi);
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
