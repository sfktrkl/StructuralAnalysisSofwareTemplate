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

        // arranges parameters affects list which affects this parameter
        // also adds these parameters to this parameter's depends list
        // since this parameter depends these parameters and these parameters are affecting this parameter
        // :D
        public override void arrangeDependsAffects()
        {
            var component = (Component)this.Value;
            // clears all parameters in depends list (parameters which are affecting this parameter)
            this.depends.Clear();
            foreach (var parameters in component.parameters.Values)
            {
                // this parameter depends all of its parameters
                // hence all parameters of this parameter effects this parameter
                this.depends.Add(parameters);
                // for all parameters of this parameter add this parameter as affected parameter
                if (parameters.affects.Contains(this) == false) parameters.affects.Add(this);
            }
            // refresh all affected parameters by this parameter
            this.RefreshAffects();
        }

        // refreshes all parameters which are affected by this parameter
        // it also needs to refresh itself (it's value and display), 
        // should overriden by every instance seperately
        // since for different instance types methods may be change
        public override void RefreshDepends()
        {
            this.RefreshAffects();
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
                    arrangeDependsAffects();

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

                    arrangeDependsAffects();
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

                    arrangeDependsAffects();
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

                    arrangeDependsAffects();
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