using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadtLandFlussLibDB
{
    public partial class StadtLandFlussModelContainer
    {
        public virtual DbSet<Land> Laender { get; set; }
        public virtual DbSet<Stadt> Staedte { get; set; }
        public virtual DbSet<Fluss> Fluesse { get; set; }

    }
}
