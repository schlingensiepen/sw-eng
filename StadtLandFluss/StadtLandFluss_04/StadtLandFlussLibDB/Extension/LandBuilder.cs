using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadtLandFlussLibDB
{
    public class LandBuilder
    {
        private string Name;
        private Stadt HauptStadt;
        public LandBuilder setName(string name)
        {
            Name = name;
            return this;
        }

        public LandBuilder setHauptStadt(Stadt hauptStadt)
        {
            HauptStadt = hauptStadt;
            return this;
        }

        public Land create()
        {
            Land res = new Land(Name);
            if (HauptStadt != null) res.HauptStadt = HauptStadt;
            return res;
        }
    }
}
