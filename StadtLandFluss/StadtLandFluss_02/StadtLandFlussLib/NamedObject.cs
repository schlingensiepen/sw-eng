using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadtLandFlussLib
{
    public abstract class NamedObject
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Invalid name");
                _name = value;
            }
        }
     
        public NamedObject(string name)
        {            
            Name = name;
        }

        public override string ToString()
        {
            return getShowTitle();
        }

        public abstract string getDescription();
        public abstract string getShowTitle();
    }
}
