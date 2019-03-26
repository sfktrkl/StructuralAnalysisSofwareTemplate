using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace StructuralAnalysisSofwareTemplate
{
    public abstract class DependentParameter : Parameter
    {
        protected List<Parameter> depends = new List<Parameter>();

        public abstract void RefreshDepends();

        public DependentParameter()
        {
            this.readOnly = true;
        }
    }

    public class NodeInstance : DependentParameter
    {
        public NodeInstance(Node component)
        {
            this.Value = component;
            this.Display = component != null ? component.parameters["Node Name"].Display : "NULL";
            this.readOnly = false;
            this.RefreshAffects();
        }

        public override void SetValue(object value)
        {
            try
            {
                var newValue = AutoComplete.ForObject(value.ToString(), typeof(Node));
                this.Value = newValue == "NULL" ? null : Database.NodeList[newValue.ToString()];
                var node = newValue == "NULL" ? null : (Node)this.Value;
                this.Display = newValue == "NULL" ? "NULL" : node.parameters["Node Name"].Display;
                this.RefreshAffects();
            }
            catch
            {
                MessageBox.Show("Object does not exist!!!");
            }
        }

        public override void RefreshDepends()
        {
        }
    }

    public class MaterialInstance : DependentParameter
    {
        public MaterialInstance(Material component)
        {
            this.Value = component;
            this.Display = component != null ? component.parameters["Material Name"].Display : "NULL";
            this.readOnly = false;
            this.RefreshAffects();
        }

        public override void SetValue(object value)
        {
            try
            {
                var newValue = AutoComplete.ForObject(value.ToString(), typeof(Material));
                this.Value = newValue == "NULL" ? null : Database.MaterialList[newValue.ToString()];
                var material = newValue == "NULL" ? null : (Material)this.Value;
                this.Display = newValue == "NULL" ? "NULL" : material.parameters["Material Name"].Display;
                this.RefreshAffects();
            }
            catch
            {
                MessageBox.Show("Object does not exist!!!");
            }
        }

        public override void RefreshDepends()
        {
        }
    }

    public class SectionInstance : DependentParameter
    {
        public SectionInstance(Section component)
        {
            this.Value = component;
            this.Display = component != null ? component.parameters["Section Name"].Display : "NULL";
            this.readOnly = false;
            this.RefreshAffects();
        }

        public override void SetValue(object value)
        {
            try
            {
                var newValue = AutoComplete.ForObject(value.ToString(), typeof(Section));
                this.Value = newValue == "NULL" ? null : Database.SectionList[newValue.ToString()];
                var section = newValue == "NULL" ? null : (Section)this.Value;
                this.Display = newValue == "NULL" ? "NULL" : section.parameters["Section Name"].Display;

                this.RefreshAffects();
            }
            catch
            {
                MessageBox.Show("Object does not exist!!!");
            }
        }

        public override void RefreshDepends()
        {
        }
    }

    public class MemberInstance : DependentParameter
    {
        public MemberInstance(Member component)
        {
            this.Value = component;
            this.Display = component != null ? component.parameters["Member Name"].Display : "NULL";
            this.readOnly = false;
            this.RefreshAffects();
        }

        public override void SetValue(object value)
        {
            try
            {
                var newValue = AutoComplete.ForObject(value.ToString(), typeof(Member));
                this.Value = newValue == "NULL" ? null : Database.SectionList[newValue.ToString()];
                var member = newValue == "NULL" ? null : (Member)this.Value;
                this.Display = newValue == "NULL" ? "NULL" : member.parameters["Member Name"].Display;

                this.RefreshAffects();
            }
            catch
            {
                MessageBox.Show("Object does not exist!!!");
            }
        }

        public override void RefreshDepends()
        {
        }
    }


    public class Area : DependentParameter
    {
        public Area(List<Parameter> depends)
        {
            this.depends = depends;
            foreach (var parameter in this.depends)
            {
                parameter.affects.Add(this);
            }

            var height = Convert.ToDouble(this.depends[0].Value);
            var width = Convert.ToDouble(this.depends[1].Value);

            this.Value = height * width;
            this.Display = this.Value.ToString();
        }

        public override void RefreshDepends()
        {
            var height = Convert.ToDouble(depends[0].Value);
            var width = Convert.ToDouble(depends[1].Value);

            this.Value = height * width;
            this.Display = this.Value.ToString();
        }
    }

    public class Inertia : DependentParameter
    {
        public Inertia(List<Parameter> depends)
        {
            this.depends = depends;
            foreach (var parameter in this.depends)
            {
                parameter.affects.Add(this);
            }

            var height = Convert.ToDouble(this.depends[0].Value);
            var width = Convert.ToDouble(this.depends[1].Value);

            this.Value = Math.Pow(height, 3) * width / 12;
            this.Display = this.Value.ToString();
        }

        public override void RefreshDepends()
        {
            var height = Convert.ToDouble(depends[0].Value);
            var width = Convert.ToDouble(depends[1].Value);

            this.Value = Math.Pow(height, 3) * width / 12;
            this.Display = this.Value.ToString();
        }
    }

    public class Length : DependentParameter
    {
        public Length(List<Parameter> depends)
        {
            this.depends = depends;
            foreach (var parameter in this.depends)
            {
                parameter.affects.Add(this);
            }

            if (depends[0].Value != null && depends[1].Value != null)
            {
                var node1 = (Node)this.depends[0].Value;
                var node2 = (Node)this.depends[1].Value;
                var x1 = Convert.ToDouble(node1.parameters["X Coordinate"].Value);
                var x2 = Convert.ToDouble(node2.parameters["X Coordinate"].Value);
                var y1 = Convert.ToDouble(node1.parameters["Y Coordinate"].Value);
                var y2 = Convert.ToDouble(node2.parameters["Y Coordinate"].Value);

                this.Value = Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
                this.Display = this.Value.ToString();
            }
        }

        public override void RefreshDepends()
        {
            if (depends[0].Value != null && depends[1].Value != null)
            {
                var node1 = (Node)this.depends[0].Value;
                var node2 = (Node)this.depends[1].Value;
                var x1 = Convert.ToDouble(node1.parameters["X Coordinate"].Value);
                var x2 = Convert.ToDouble(node2.parameters["X Coordinate"].Value);
                var y1 = Convert.ToDouble(node1.parameters["Y Coordinate"].Value);
                var y2 = Convert.ToDouble(node2.parameters["Y Coordinate"].Value);

                this.Value = Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
                this.Display = this.Value.ToString();
            }
        }
    }
}