using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralAnalysisSofwareTemplate
{
    public class BaseClass
    {
        private Dictionary<string, Member> isUsed = new Dictionary<string, Member>();
        public bool used = false;

        public virtual void delete()
        {

        }

        public void usedBy(string memberName, Member member, bool condition)
        {
            if (condition == true)
            {
                isUsed.Add(memberName, member);
                used = true;
            }
            else
            {
                isUsed.Remove(memberName);
                if (isUsed.Count == 0)
                {
                    used = false;
                }
            }

        }
    }
}
