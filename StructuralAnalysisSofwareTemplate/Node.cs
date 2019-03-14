using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace StructuralAnalysisSofwareTemplate
{
    public class Node
    {

        static int numOfNodes = 0;
        public string Node_Name;
        private double X_Coordinate;
        private double Y_Coordinate;
        private bool X_Fixity;
        private bool Y_Fixity;
        private bool Z_Fixity;
        private double X_Stiffness;
        private double Y_Stiffness;
        private double Z_Stiffness;
        private Dictionary<string, Member> isUsed = new Dictionary<string, Member>();
        public bool used = false;

        public Node()
        {
            this.X_Coordinate = 0;
            this.Y_Coordinate = 0;
            this.X_Fixity = false;
            this.Y_Fixity = false;
            this.Z_Fixity = false;
            this.X_Stiffness = 0;
            this.Y_Stiffness = 0;
            this.Z_Stiffness = 0;
            this.Node_Name= "Node: " + numOfNodes.ToString();
            numOfNodes++;
        }

        public void SetAll(double X_Coordinate, double Y_Coordinate, bool X_Fixity, bool Y_Fixity, bool Z_Fixity, double X_Stiffness, double Y_Stiffness, double Z_Stiffness)
        {
            this.X_Coordinate = X_Coordinate;
            this.Y_Coordinate = Y_Coordinate;
            this.X_Fixity = X_Fixity;
            this.Y_Fixity = Y_Fixity;
            this.Z_Fixity = Z_Fixity;
            this.X_Stiffness = X_Stiffness;
            this.Y_Stiffness = Y_Stiffness;
            this.Z_Stiffness = Z_Stiffness;

        }

        public List<string> GetAll()
        {
            List<string> fieldData = new List<string>();

            fieldData.Add(this.Node_Name.ToString());
            fieldData.Add(this.X_Coordinate.ToString());
            fieldData.Add(this.Y_Coordinate.ToString());
            fieldData.Add(this.X_Fixity.ToString());
            fieldData.Add(this.Y_Fixity.ToString());
            fieldData.Add(this.Z_Fixity.ToString());
            fieldData.Add(this.X_Stiffness.ToString());
            fieldData.Add(this.Y_Stiffness.ToString());
            fieldData.Add(this.Z_Stiffness.ToString());


            return fieldData;
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
