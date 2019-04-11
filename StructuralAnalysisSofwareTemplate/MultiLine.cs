using System;
using System.Collections.Generic;

namespace StructuralAnalysisSofwareTemplate
{
    public class MultiLine : Component
    {
        public readonly Dictionary<string, Component> Members = new Dictionary<string, Component>();
        public readonly Dictionary<string, Component> Nodes = new Dictionary<string, Component>();

        public MultiLine()
        {
            this.UniqueName = "MultiLine: " + Database.MultiLineList.Count.ToString();
            this.parameters.Add("MultiLine Name", new Name("MultiLine: " + Database.MultiLineList.Count.ToString()));
            this.parameters.Add("Member Count", new Number(0.0));
            this.parameters.Add("First Node", new NodeInstance());
            this.parameters.Add("Last Node", new NodeInstance());
            this.parameters.Add("Material", new MaterialInstance());
            this.parameters.Add("Section", new SectionInstance());
            this.parameters.Add("Interval", new Interval(new List<Parameter> { this.parameters["First Node"], this.parameters["Last Node"], this.parameters["Member Count"] }));
        }
        /*
        private void CreateNodes()
        {
            var firstNode = (Node)this.parameters["First Node"].Value;
            var xCoordinate1 = Convert.ToDouble(firstNode.parameters["X Coordinate"].Value);
            var yCoordinate1 = Convert.ToDouble(firstNode.parameters["Y Coordinate"].Value);
            this.Nodes.Add(firstNode.UniqueName, firstNode);

            for (int i = 0; i < Convert.ToDouble(this.parameters["Member Count"].Value) - 1; i++)
            {
                double interval = Convert.ToDouble(this.parameters["Interval"].Value);
                var node = new Node(this, xCoordinate1 + interval * (i + 1), yCoordinate1 + interval * (i + 1));
                this.Nodes.Add(node.UniqueName, node);
            }

            var lastNode = (Node)this.parameters["Last Node"].Value;
            this.Nodes.Add(lastNode.UniqueName, lastNode);
        }

        private void CreateMembers()
        {
            for (int i = 0; i < Convert.ToDouble(this.parameters["Member Count"].Value); i++)
            {
                string node1String = "Node: " + i.ToString();
                string node2String = "Node: " + (i + 1).ToString();
                var member = new Member(this, (Node)this.Nodes[node1String], (Node)this.Nodes[node2String]);
                this.Members.Add(member.UniqueName, member);
            }
        }
        */
        public override void Delete()
        {
            Database.MultiLineList.Remove(this.UniqueName);
        }
    }
}