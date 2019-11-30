using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadtLandFlussLib
{
    public class StadtBuilder
    {

        private string Name;
        private List<Fluss> LiegtAn = new List<Fluss>();
        private Land LiegtIn;
        public StadtBuilder setName(string name)
        {
            Name = name;
            return this;
        }

        public StadtBuilder addLiegtAn(Fluss fluss)
        {
            LiegtAn.Add(fluss);
            return this;
        }
        public StadtBuilder addLiegtAn(IEnumerable<Fluss> fluss)
        {
            LiegtAn.AddRange(fluss);
            return this;
        }
        public StadtBuilder setLiegtIn(Land land)
        {
            LiegtIn = land;
            return this;
        }


        public Stadt create()
        {
            Stadt res = new Stadt(Name, LiegtIn);
            res.LiegtAn.AddRange(LiegtAn);
            return res;
        }
    }
}
