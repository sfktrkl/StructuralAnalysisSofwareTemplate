using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralAnalysisSofwareTemplate
{
    public class BaseClass
    {
        // contains which objects are using this object
        private Dictionary<string, Member> isUsed = new Dictionary<string, Member>();
        // returns whether object is used by another object or objects
        public bool used = false;

        // virtual method for deleting objects
        public virtual void delete()
        {

        }

        public void usedBy(string memberName, Member member, bool condition)
        {
            if (condition == true)
            {
                // adds member to used dictionary
                isUsed.Add(memberName, member);
                // when used dictionary has object, used returns true
                used = true;
            }
            else
            {
                isUsed.Remove(memberName);
                if (isUsed.Count == 0)
                {
                    // if used dictionary is empty 
                    // returns used as false
                    used = false;
                }
            }

        }
    }
}
