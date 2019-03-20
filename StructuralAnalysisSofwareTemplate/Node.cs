using System.Collections.Generic;

namespace StructuralAnalysisSofwareTemplate
{
    public class Node : Component
    {
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
            this.Name = "Node: " + Database.NodeList.Count.ToString();
        }

        private double X_Coordinate;
        private double Y_Coordinate;
        private bool X_Fixity;
        private bool Y_Fixity;
        private bool Z_Fixity;
        private double X_Stiffness;
        private double Y_Stiffness;
        private double Z_Stiffness;

        public override void Delete()
        {
            Database.NodeList.Remove(this.Name);
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

        public override List<string> GetAll()
        {
            return new List<string>
            {
                this.Name.ToString(),
                this.X_Coordinate.ToString(),
                this.Y_Coordinate.ToString(),
                this.X_Fixity.ToString(),
                this.Y_Fixity.ToString(),
                this.Z_Fixity.ToString(),
                this.X_Stiffness.ToString(),
                this.Y_Stiffness.ToString(),
                this.Z_Stiffness.ToString()
            };
        }
    }
}