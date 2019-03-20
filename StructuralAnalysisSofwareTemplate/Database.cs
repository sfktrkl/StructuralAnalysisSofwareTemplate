using System;
using System.Collections.Generic;
using System.Linq;

namespace StructuralAnalysisSofwareTemplate
{
    public static class Database
    {
        public static readonly Dictionary<string, Node> NodeList = new Dictionary<string, Node>();
        public static readonly Dictionary<string, Member> MemberList = new Dictionary<string, Member>();
        public static readonly Dictionary<string, Material> MaterialList = new Dictionary<string, Material>();
        public static readonly Dictionary<string, Section> SectionList = new Dictionary<string, Section>();

        public static List<SpreadSheet> spreadList = new List<SpreadSheet>();

        public static void refreshSpreadList()
        {
            // refreshes all spreadsheets
            foreach (var form in spreadList)
            {
                form.refresh(form.loadType);
            }
        }

        public static string autoComplete(string cellValue, Type cellType)
        {
            string objectName;
            // determines prefix according to object type
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

            // auto complete for object names

            // if given input is all digit adds objectName as prefix
            if (cellValue.All(char.IsDigit))
            {
                string temp = objectName + cellValue;
                return temp;
            }
            else
            {
                // if given input is full name return as same
                return cellValue;
            }
        }

        public static bool autoComplete(string cellValue)
        {
            // overload for autocomplete method which return (boolean)true for strings which means true
            if (cellValue == "1" || cellValue == "true" || cellValue == "True" || cellValue == "TRUE")
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