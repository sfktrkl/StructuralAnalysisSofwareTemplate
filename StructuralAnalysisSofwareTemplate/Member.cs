using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralAnalysisSofwareTemplate
{
    class Member
    {

        static int numOfMembers;
        private string member_Name;
        private Material memberMaterial;
        private Section memberSection;
        private Node node1;
        private Node node2;


        Member()
        {
            this.node1 = null;
            this.node2 = null;
            this.memberMaterial = null;
            this.memberSection = null;
            this.member_Name = "Member: " + numOfMembers.ToString();
            numOfMembers++;

        }

        public void SetAll(Node node1, Node node2, Material memberMaterial, Section memberSection)
        {
            this.node1 = node1;
            this.node2 = node2;
            this.memberMaterial = memberMaterial;
            this.memberSection = memberSection;

        }


    }
}
