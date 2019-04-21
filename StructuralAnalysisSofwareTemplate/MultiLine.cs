using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace StructuralAnalysisSofwareTemplate
{
    public class MultiLine : Component
    {
        public Dictionary<string, Component> Members = new Dictionary<string, Component>();
        public Dictionary<string, Component> Nodes = new Dictionary<string, Component>();

        public MultiLine()
        {
            this.UniqueName = "MultiLine: " + Database.MultiLineList.Count.ToString();
            this.parameters.Add("MultiLine Name", new Name("MultiLine: " + Database.MultiLineList.Count.ToString()));
            this.parameters.Add("First Node", new NodeInstance());
            this.parameters.Add("Last Node", new NodeInstance());
            this.parameters.Add("Material", new MaterialInstance());
            this.parameters.Add("Section", new SectionInstance());
            this.parameters.Add("Member Count", new MemberCount(0, new List<Parameter> { this.parameters["First Node"], this.parameters["Last Node"], this.parameters["Material"], this.parameters["Section"] }));
            this.parameters.Add("Interval", new Interval(new List<Parameter> { this.parameters["First Node"], this.parameters["Last Node"], this.parameters["Member Count"] }));
        }

        // MemberCount parameter, which organizes node and member create process
        // Not sure about which parameter type has to be used...
        public class MemberCount : DependentParameter
        {
            public MemberCount(uint value, List<Parameter> depends)
            {
                this.depends = depends;
                this.Value = value;
                this.Display = value.ToString();
                this.notifyAffectors();
                this.RecalculateValue();
            }

            public override bool IsReadOnly()
            {
                return false;
            }

            public override void SetValue(object value)
            {
                this.Value = Convert.ToUInt64(value);
                this.Display = value.ToString();
                this.notifyAffectors();
                this.RecalculateValue();
            }

            public override void RecalculateValue()
            {
                this.RefreshAffects();
                foreach (var instance in this.affects) if (instance.GetType() == typeof(MultiLineInstance))
                    {
                        var multiline = (MultiLine)instance.Value;
                        multiline.Nodes.Clear();
                        multiline.Members.Clear();
                        if (Convert.ToUInt64(this.Value) > 0)
                        {
                            try
                            {
                                multiline.CreateNodes();
                                multiline.CreateMembers();
                            }
                            catch
                            {
                                MessageBox.Show("Wrong Input");
                                this.Value = 0;
                                this.Display = this.Value.ToString();
                            }
                        }
                    }
            }
        }

        private void CreateNodes()
        {
            // take first node and save it to dictionary
            var firstNode = (Node)this.parameters["First Node"].Value;
            var xCoordinate1 = Convert.ToDouble(firstNode.parameters["X Coordinate"].Value);
            var yCoordinate1 = Convert.ToDouble(firstNode.parameters["Y Coordinate"].Value);
            var cloneFirst = new Node(this, xCoordinate1, yCoordinate1);
            this.Nodes.Add(cloneFirst.UniqueName, cloneFirst);

            // create nodes between first and last nodes
            var interval = (List<double>)this.parameters["Interval"].Value;

            int i = 0;
            for (i = 0; i < Convert.ToDouble(this.parameters["Member Count"].Value) - 1; i++)
            {
                var node = new Node(this, xCoordinate1 + interval[0] * (i + 1), yCoordinate1 + interval[1] * (i + 1));
                this.Nodes.Add(node.UniqueName, node);
            }

            // take first node and save it to dictionary
            var lastNode = (Node)this.parameters["Last Node"].Value;
            var cloneLast = new Node(this, xCoordinate1 + interval[0] * (i + 1), yCoordinate1 + interval[1] * (i + 1));
            this.Nodes.Add(cloneLast.UniqueName, cloneLast);
        }

        private void CreateMembers()
        {
            for (int i = 0; i < Convert.ToDouble(this.parameters["Member Count"].Value); i++)
            {
                // create member for each 2 node
                string node1String = "Node: " + i;
                string node2String = "Node: " + (i + 1);
                var node1 = (Node)this.Nodes[node1String];
                var node2 = (Node)this.Nodes[node2String];

                var member = new Member(this, node1, node2);
                this.Members.Add(member.UniqueName, member);
            }
        }

        public override void Delete()
        {
            Database.MultiLineList.Remove(this.UniqueName);
        }
    }
}