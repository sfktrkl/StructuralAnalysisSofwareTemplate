using System.Windows.Forms;

namespace StructuralAnalysisSofwareTemplate
{
    public abstract class DependentInstances : DependentParameter
    {
        public DependentInstances()
        {
            this.readOnly = false;
        }

        public override void RefreshDepends()
        {
            RefreshAffects();
        }
    }

    public class NodeInstance : DependentInstances
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

                foreach (var parameters in node.parameters.Values)
                {
                    parameters.affects.Add(this);
                }
                this.RefreshAffects();
            }
            catch
            {
                MessageBox.Show("Object does not exist!!!");
            }
        }

    }

    public class MaterialInstance : DependentInstances
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

                foreach (var parameters in material.parameters.Values)
                {
                    parameters.affects.Add(this);
                }
                this.RefreshAffects();

            }
            catch
            {
                MessageBox.Show("Object does not exist!!!");
            }
        }

    }

    public class SectionInstance : DependentInstances
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

                foreach (var parameters in section.parameters.Values)
                {
                    parameters.affects.Add(this);
                }

                this.RefreshAffects();
            }
            catch
            {
                MessageBox.Show("Object does not exist!!!");
            }
        }

    }

    public class MemberInstance : DependentInstances
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

                foreach (var parameters in member.parameters.Values)
                {
                    parameters.affects.Add(this);
                }
                this.RefreshAffects();
            }
            catch
            {
                MessageBox.Show("Object does not exist!!!");
            }
        }

    }
}