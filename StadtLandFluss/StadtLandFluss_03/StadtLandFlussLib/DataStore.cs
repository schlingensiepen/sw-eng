using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadtLandFlussLib
{
    public class DataStore
    {
        public List<Stadt> Staedte = new List<Stadt>();
        public List<Land> Laender = new List<Land>();
        public List<Fluss> Fluesse = new List<Fluss>();
        public List<Person> Personen = new List<Person>();


        public IEnumerable<NamedObject> NamedObjects
        {
            get
            {
                List<NamedObject> res = new List<NamedObject>();
                res.AddRange(Staedte);
                res.AddRange(Laender);
                res.AddRange(Fluesse);
                return res;
            }
        }

        public DataStore()
        {

        }

    }
}
