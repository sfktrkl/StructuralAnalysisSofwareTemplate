using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StructuralAnalysisSofwareTemplate
{
    public static class Database
    {
        public static readonly Dictionary<string, Component> NodeList = new Dictionary<string, Component>();
        public static readonly Dictionary<string, Component> MemberList = new Dictionary<string, Component>();
        public static readonly Dictionary<string, Component> MaterialList = new Dictionary<string, Component>();
        public static readonly Dictionary<string, Component> SectionList = new Dictionary<string, Component>();

        public static List<SpreadSheet> spreadList = new List<SpreadSheet>();

        public static void refreshSpreadList()
        {
            // refreshes all spreadsheets
            foreach (var form in spreadList)
            {
                form.refresh();
            }
        }

        // auto complete for object names
        public static string AutoComplete(string cellValue, Type cellType)
        {
            string objectName;
            // determines prefix according to component type
            if (cellType == typeof(Node))
            {
                objectName = "Node: ";
            }
            else if (cellType == typeof(Member))
            {
                objectName = "Member: ";
            }
            else if (cellType == typeof(Material))
            {
                objectName = "Material: ";
            }
            else if (cellType == typeof(Section))
            {
                objectName = "Section: ";
            }
            else
            {
                objectName = "";
            }

            // if given input is all digit adds objectName as prefix
            if (cellValue.All(char.IsDigit))
            {
                string temp = objectName + cellValue;
                return temp;
            }
            else
            {
                // finds matches for "null" if indented string is null
                Regex regex = new Regex("^([n][u]?[l]?[l]?)$", RegexOptions.IgnoreCase);
                MatchCollection matches = regex.Matches(cellValue);

                // returns null if matches found
                if (matches.Count > 0)
                {
                    return "NULL";
                }
                // returns component's name
                else
                {
                    // if given input is full name return as same
                    return cellValue;
                }
            }
        }

        public static bool AutoComplete(string cellValue)
        {
            // regex for words meaning true (1 or "true")
            Regex regex = new Regex("^([t][r]?[u]?[e]?|1)$", RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(cellValue);

            // overload for AutoComplete method which return (boolean)true for strings which means true
            if (matches.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}