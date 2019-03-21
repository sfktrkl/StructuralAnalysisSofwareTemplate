namespace StructuralAnalysisSofwareTemplate
{
    public class Node : Component
    {
        public Node()
        {
            this.xCoordinate = 0;
            this.yCoordinate = 0;
            this.xFixity = false;
            this.yFixity = false;
            this.zFixity = false;
            this.xStiffness = 0;
            this.yStiffness = 0;
            this.zStiffness = 0;
            this.Name = "Node: " + Database.NodeList.Count.ToString();
        }

        public double xCoordinate { get; private set; }
        public double yCoordinate { get; private set; }
        public bool xFixity { get; private set; }
        public bool yFixity { get; private set; }
        public bool zFixity { get; private set; }
        public double xStiffness { get; private set; }
        public double yStiffness { get; private set; }
        public double zStiffness { get; private set; }

        public override void Delete()
        {
            Database.NodeList.Remove(this.Name);
        }

        public void SetAll(double X_Coordinate, double Y_Coordinate, bool X_Fixity, bool Y_Fixity, bool Z_Fixity, double X_Stiffness, double Y_Stiffness, double Z_Stiffness)
        {
            this.xCoordinate = X_Coordinate;
            this.yCoordinate = Y_Coordinate;
            this.xFixity = X_Fixity;
            this.yFixity = Y_Fixity;
            this.zFixity = Z_Fixity;
            this.xStiffness = X_Stiffness;
            this.yStiffness = Y_Stiffness;
            this.zStiffness = Z_Stiffness;
        }
    }
}