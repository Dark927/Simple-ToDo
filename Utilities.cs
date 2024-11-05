using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ToDoApp
{
    static class Utilities
    {
        public static bool IsTextMatch(string text, string pattern)
        {
            return Regex.IsMatch(text, pattern);
        }
    }
}
