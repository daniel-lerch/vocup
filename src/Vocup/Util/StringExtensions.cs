using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocup.Util
{
    public static class StringExtensions
    {
        /// <summary>
        /// Checks whether one of the specified chars is present in this string.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="chars"></param>
        /// <returns></returns>
        public static bool ContainsAny(this string value, string chars)
        {
            foreach (char item in chars)
            {
                if (value.Contains(item)) return true;
            }
            return false;
        }
    }
}
