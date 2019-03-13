using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralAnalysisSofwareTemplate
{
    public class Member
    {

        static int numOfMembers;
        public string member_Name;
        private Material memberMaterial;
        private Section memberSection;
        private Node node1;
        private Node node2;


        public Member()
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

        public List<string> GetAll()
        {
            List<string> fieldData = new List<string>();

            fieldData.Add(this.member_Name == null ? "NULL" : this.member_Name.ToString());
            fieldData.Add(this.memberMaterial == null ? "NULL" : this.memberMaterial.ToString());
            fieldData.Add(this.memberSection == null ? "NULL" : this.memberSection.ToString());
            fieldData.Add(this.node1 == null ? "NULL" : this.node1.ToString());
            fieldData.Add(this.node2 == null ? "NULL" : this.node2.ToString());

            return fieldData;
        }


    }
}
