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

    }
}
