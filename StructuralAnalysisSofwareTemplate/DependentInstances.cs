using System.Windows.Forms;

namespace StructuralAnalysisSofwareTemplate
{
    public abstract class DependentInstances : DependentParameter
    {
        // sets readOnly field false since these instances can be changed by user input
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
        public NodeInstance()
        {
            this.Value = null;
            this.Display = "NULL";
        }

        public override void SetValue(object value)
        {
            var newValue = AutoComplete.ForObject(value.ToString(), typeof(Node));

            // if result from autocomplete is not null
            // tries changing the value and display of the parameter
            if (newValue != "NULL")
            {
                try
                {
                    //sets the value and the display of the parameter
                    this.Value = Database.NodeList[newValue.ToString()];
                    var component = (Node)this.Value;
                    this.Display = component.parameters["Node Name"].Display;

                    foreach (var parameters in component.parameters.Values)
                    {
                        // this parameter depends all of its parameters
                        // hence all parameters of this parameter effects this parameter
                        this.depends.Add(parameters);
                        // for all parameters of this parameter add this parameter as affected parameter
                        parameters.affects.Add(this);
                    }
                    this.RefreshAffects();
                }
                catch
                {
                    // if given input (component) does not exist
                    MessageBox.Show("Object does not exist!!!");
                }
            }
            else
            {
                this.Value = null;
                this.Display = "NULL";
            }
        }

        public override void RefreshDepends()
        {
            var component = (Node)this.Value;
            this.Value = component;
            this.Display = component.parameters["Node Name"].Display;
            base.RefreshDepends();
        }
    }

    public class MaterialInstance : DependentInstances
    {
        public MaterialInstance()
        {
            this.Value = null;
            this.Display = "NULL";
        }

        public override void SetValue(object value)
        {
            var newValue = AutoComplete.ForObject(value.ToString(), typeof(Material));

            if (newValue != "NULL")
            {
                try
                {
                    this.Value = Database.MaterialList[newValue.ToString()];
                    var component = (Material)this.Value;
                    this.Display = component.parameters["Material Name"].Display;

                    foreach (var parameters in component.parameters.Values)
                    {
                        this.depends.Add(parameters);
                        parameters.affects.Add(this);
                    }
                    this.RefreshAffects();
                }
                catch
                {
                    MessageBox.Show("Object does not exist!!!");
                }
            }
            else
            {
                this.Value = null;
                this.Display = "NULL";
            }
        }

        public override void RefreshDepends()
        {
            var component = (Material)this.Value;
            this.Value = component;
            this.Display = component.parameters["Material Name"].Display;
            base.RefreshDepends();
        }
    }

    public class SectionInstance : DependentInstances
    {
        public SectionInstance()
        {
            this.Value = null;
            this.Display = "NULL";
        }

        public override void SetValue(object value)
        {
            var newValue = AutoComplete.ForObject(value.ToString(), typeof(Section));

            if (newValue != "NULL")
            {
                try
                {
                    this.Value = Database.SectionList[newValue.ToString()];
                    var component = (Section)this.Value;
                    this.Display = component.parameters["Section Name"].Display;

                    foreach (var parameters in component.parameters.Values)
                    {
                        this.depends.Add(parameters);
                        parameters.affects.Add(this);
                    }
                    this.RefreshAffects();
                }
                catch
                {
                    MessageBox.Show("Object does not exist!!!");
                }
            }
            else
            {
                this.Value = null;
                this.Display = "NULL";
            }
        }

        public override void RefreshDepends()
        {
            var component = (Section)this.Value;
            this.Value = component;
            this.Display = component.parameters["Section Name"].Display;
            base.RefreshDepends();
        }
    }

    public class MemberInstance : DependentInstances
    {
        public MemberInstance()
        {
            this.Value = null;
            this.Display = "NULL";
        }

        public override void SetValue(object value)
        {
            var newValue = AutoComplete.ForObject(value.ToString(), typeof(Member));

            if (newValue != "NULL")
            {
                try
                {
                    this.Value = Database.MemberList[newValue.ToString()];
                    var component = (Member)this.Value;
                    this.Display = component.parameters["Member Name"].Display;

                    foreach (var parameters in component.parameters.Values)
                    {
                        this.depends.Add(parameters);
                        parameters.affects.Add(this);
                    }
                    this.RefreshAffects();
                }
                catch
                {
                    MessageBox.Show("Object does not exist!!!");
                }
            }
            else
            {
                this.Value = null;
                this.Display = "NULL";
            }
        }

        public override void RefreshDepends()
        {
            var component = (Member)this.Value;
            this.Value = component;
            this.Display = component.parameters["Member Name"].Display;
            base.RefreshDepends();
        }
    }
}