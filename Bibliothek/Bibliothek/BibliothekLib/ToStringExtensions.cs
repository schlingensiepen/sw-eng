using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliothekLib
{
    public partial class Media
    {
        public override string ToString()
        {
            return $"{Number} - {Work} - ({Id})";
        }
    }
    public partial class Work
    {
        public override string ToString()
        {
            return $"{Titel} von {Author} " +
                   $"erschienen bei {Verlag} " +
                   $"lagert in {Standort} ({Id})";
        }
    }

    public partial class User
    {
        public override string ToString()
        {
            return $"{Name} {Familyname} ({Id})";
        }
    }

}
