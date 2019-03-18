using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralAnalysisSofwareTemplate
{
    public class TemporaryDatabase
    {
        // public lists to store objects
        private static Dictionary<string, object> nodeList = new Dictionary<string, object>();
        private static Dictionary<string, object> memberList = new Dictionary<string, object>();
        private static Dictionary<string, object> materialList = new Dictionary<string, object>();
        private static Dictionary<string, object> sectionList = new Dictionary<string, object>();
        public List<SpreadSheet> spreadList = new List<SpreadSheet>();

        // get method is used for returning the dictionaries according to object type or by integer
        public dynamic get(Type givenClass)
        {

            if (givenClass == typeof(Node))
            {
                return nodeList;
            }
            else if (givenClass == typeof(Member))
            {
                return memberList;
            }
            else if (givenClass == typeof(Material))
            {
                return materialList;
            }
            else
            {
                return sectionList;
            } 

        }

        public dynamic get(int givenClass)
        {

            if (givenClass == 0)
            {
                return nodeList;
            }
            else if (givenClass == 1)
            {
                return memberList;
            }
            else if (givenClass == 2)
            {
                return materialList;
            }
            else
            {
                return sectionList;
            }

        }

        // returnType method is used for returning the object type according to given string(object.Name property) or by integer
        public dynamic returnType(string given)
        {
            if (given.Contains("Node"))
            {
                return typeof(Node);
            }
            else if (given.Contains("Member"))
            {
                return typeof(Member);
            }
            else if (given.Contains("Material"))
            {
                return typeof(Material);
            }
            else if (given.Contains("Section"))
            {
                return typeof(Section);
            }
            else
            {
                return null;
            }
        }

        public dynamic returnType(int given)
        {
            if (given == 0)
            {
                return typeof(Node);
            }
            else if (given == 1)
            {
                return typeof(Member);
            }
            else if (given == 2)
            {
                return typeof(Material);
            }
            else
            {
                return typeof(Section);
            }
        }

        public void refreshSpreadList()
        {
            // refreshes all spreadsheets
            foreach (var form in spreadList)
            {
                form.refresh(form.loadType);
            }
        }

        public string autoComplete(string cellValue, Type cellType)
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
        public bool autoComplete(string cellValue)
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
