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

        public string AsString => ToString();

    }
    public partial class Work
    {
        public override string ToString()
        {
            return $"{Titel} von {Author} " +
                   $"erschienen bei {Verlag} " +
                   $"lagert in {Standort} ({Id})";
        }
        public string AsString => ToString();

    }

    public partial class User
    {
        public override string ToString()
        {
            return $"{Name} {Familyname} ({Id})";
        }
        public string AsString => ToString();
    }

}
