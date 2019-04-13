using System.Windows.Forms;

namespace StructuralAnalysisSofwareTemplate
{
    public abstract class DependentInstances : DependentParameter
    {
        // sets readOnly field false since these instances can be changed by user input
        public DependentInstances()
        {
            this.Value = null;
            this.Display = "NULL";
        }

        // arranges parameters affects list which affects this parameter
        // also adds these parameters to this parameter's depends list
        // since this parameter depends these parameters and these parameters are affecting this parameter
        // :D
        public override void notifyAffectors()
        {
            var component = (Component)this.Value;
            // clears all parameters in depends list (parameters which are affecting this parameter)
            this.depends.Clear();
            foreach (var parameter in component.parameters.Values)
            {
                // this parameter depends all of its parameters
                // hence all parameters of this parameter effects this parameter
                this.depends.Add(parameter);
                // for all parameters of this parameter add this parameter as affected parameter
                if (parameter.affects.Contains(this) == false) parameter.affects.Add(this);
            }
            // refresh all affected parameters by this parameter
            this.RefreshAffects();
        }

        // refreshes all parameters which are affected by this parameter
        // it also needs to refresh itself (it's value and display),
        // should overriden by every instance seperately
        // since for different instance types methods may be change
        public override void RecalculateValue()
        {
            this.RefreshAffects();
        }

        public override bool IsReadOnly()
        {
            return false;
        }
    }

    public class NodeInstance : DependentInstances
    {
        public NodeInstance()
        {
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
                    notifyAffectors();
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

        public override void SetValue(object value, MultiLine multiline)
        {
            var newValue = AutoComplete.ForObject(value.ToString(), typeof(Node));

            // if result from autocomplete is not null
            // tries changing the value and display of the parameter
            if (newValue != "NULL")
            {
                try
                {
                    //sets the value and the display of the parameter
                    this.Value = multiline.Nodes[newValue.ToString()];
                    var component = (Node)this.Value;
                    this.Display = component.parameters["Node Name"].Display;
                    notifyAffectors();
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

        public override void RecalculateValue()
        {
            var component = (Node)this.Value;
            this.Value = component;
            this.Display = component.parameters["Node Name"].Display;
            base.RecalculateValue();
        }
    }

    public class MaterialInstance : DependentInstances
    {
        public MaterialInstance()
        {
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

                    notifyAffectors();
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

        public override void RecalculateValue()
        {
            var component = (Material)this.Value;
            this.Value = component;
            this.Display = component.parameters["Material Name"].Display;
            base.RecalculateValue();
        }
    }

    public class SectionInstance : DependentInstances
    {
        public SectionInstance()
        {
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

                    notifyAffectors();
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

        public override void RecalculateValue()
        {
            var component = (Section)this.Value;
            this.Value = component;
            this.Display = component.parameters["Section Name"].Display;
            base.RecalculateValue();
        }
    }

    public class MemberInstance : DependentInstances
    {
        public MemberInstance()
        {
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

                    notifyAffectors();
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

        public override void RecalculateValue()
        {
            var component = (Member)this.Value;
            this.Value = component;
            this.Display = component.parameters["Member Name"].Display;
            base.RecalculateValue();
        }
    }

    public class MultiLineInstance : DependentInstances
    {
        public MultiLineInstance()
        {
        }

        public override void SetValue(object value)
        {
            var newValue = AutoComplete.ForObject(value.ToString(), typeof(MultiLine));

            if (newValue != "NULL")
            {
                try
                {
                    this.Value = Database.MultiLineList[newValue.ToString()];
                    var component = (MultiLine)this.Value;
                    this.Display = component.parameters["MultiLine Name"].Display;

                    this.notifyAffectors();
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

        public override void RecalculateValue()
        {
            var component = (MultiLine)this.Value;
            this.Value = component;
            this.Display = component.parameters["MultiLine Name"].Display;
            base.RecalculateValue();
        }
    }

}