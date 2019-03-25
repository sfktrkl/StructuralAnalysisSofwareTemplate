using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StructuralAnalysisSofwareTemplate
{
    public static class AutoComplete
    {
        // auto complete for object names
        public static string ForObject(string str, Type cellType)
        {
            // if given input is all digit adds objectName as prefix
            if (str.All(char.IsDigit))
            {
                return cellType.Name + ": " + str;
            }
            else
            {
                return IsNull(str) ? "NULL" : str;
            }
        }

        // returns (boolean)true for strings which mean null
        public static bool IsNull(string str)
        {
            // regex for words meaning null
            var regex = new Regex("^([n][u]?[l]?[l]?)$", RegexOptions.IgnoreCase);
            return regex.Matches(str).Count > 0;
        }

        // returns (boolean)true for strings which mean true
        public static bool TrueFalse(string str)
        {
            // regex for words meaning true (1 or "true")
            var regex = new Regex("^([t][r]?[u]?[e]?|1)$", RegexOptions.IgnoreCase);
            return regex.Matches(str).Count > 0;
        }
    }
}