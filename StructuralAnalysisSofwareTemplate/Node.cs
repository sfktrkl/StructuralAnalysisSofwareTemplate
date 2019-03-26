namespace StructuralAnalysisSofwareTemplate
{
    public class Node : Component
    {
        public Node()
        {
            this.UniqueName = "Node: " + Database.NodeList.Count.ToString();
            this.parameters.Add("Node Name", new Name("Node: " + Database.NodeList.Count.ToString()));
            this.parameters.Add("X Coordinate", new Number(0.0));
            this.parameters.Add("Y Coordinate", new Number(0.0));
            this.parameters.Add("X Fixity", new TrueFalse(true));
            this.parameters.Add("Y Fixity", new TrueFalse(true));
            this.parameters.Add("Z Fixity", new TrueFalse(true));
            this.parameters.Add("X Stiffness", new Number(0.0));
            this.parameters.Add("Y Stiffness", new Number(0.0));
            this.parameters.Add("Z Stiffness", new Number(0.0));
        }

        public override void Delete()
        {
            Database.NodeList.Remove(this.UniqueName);
        }
    }
}