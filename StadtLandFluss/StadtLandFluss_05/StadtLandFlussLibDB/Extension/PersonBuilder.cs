using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StadtLandFlussLibDB
{
    public class PersonBuilder
    {
        private String Vorname;
        private String Familienname;
        private Stadt GeborenIn;
        private List<Stadt> WohntIn = new List<Stadt>();
        public PersonBuilder setVorname(string vorname)
        {
            Vorname = vorname;
            return this;
        }

        public PersonBuilder setNachname(string nachname)
        {
            Familienname = nachname;
            return this;
        }

        public PersonBuilder setGeborenIn(Stadt geborenIn)
        {
            GeborenIn = geborenIn;
            return this;
        }

        public PersonBuilder addWohntIn(Stadt wohntIn)
        {
            WohntIn.Add(wohntIn);
            return this;
        }
        public PersonBuilder addWohntIn(IEnumerable<Stadt> wohntIn)
        {
            WohntIn.AddRange(wohntIn);
            return this;
        }
        public Person create()
        {
            return new Person(
                Vorname, 
                Familienname, 
                GeborenIn, 
                WohntIn);
        }
    }
}
