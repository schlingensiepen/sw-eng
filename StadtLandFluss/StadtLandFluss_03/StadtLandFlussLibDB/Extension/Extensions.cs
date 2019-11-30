using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadtLandFlussLibDB
{
    public static class Extensions
    {
        public static string concat(
            this IEnumerable<string> elements, string prefix, string seperator, string lastSep)
        {
            var elementsArr = elements.ToArray();
            if (elementsArr.Length == 0) return "";
            if (elementsArr.Length == 1) return prefix + elementsArr.FirstOrDefault();
            return
                prefix +
                string.Join(seperator, elementsArr.Take(elementsArr.Length - 1))
                + lastSep + elementsArr.LastOrDefault();
        }
    }
}
