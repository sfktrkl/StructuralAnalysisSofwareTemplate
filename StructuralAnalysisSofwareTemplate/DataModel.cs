using System;
using System.Collections.Generic;

namespace StructuralAnalysisSofwareTemplate
{
    public abstract class DataModel
    {
        public DataModel(Dictionary<string, Component> components)
        {
            this.Components = components;
        }

        protected Component Prototype;

        public Dictionary<string, Component> Components { get; private set; }

        public abstract void CreateComponent();

        public virtual List<string> GetColumnNames()
        {
            var columnNames = new List<string>();

            // returns all parameter keys for writing column names.
            foreach (KeyValuePair<string, Parameter> parameter in this.Prototype.parameters)
            {
                columnNames.Add(parameter.Key.ToString());
            }

            return columnNames;
        }

        public virtual List<Tuple<string, bool>> GetRowData(string componentName)
        {
            var component = this.Components[componentName];
            var values = new List<Tuple<string, bool>>();

            // returns all displays for writing cells values.
            foreach (KeyValuePair<string, Parameter> parameter in component.parameters)
            {
                var rowData = new Tuple<string, bool>(parameter.Value.Display, parameter.Value.IsReadOnly());
                values.Add(rowData);
            }

            return values;
        }

        public abstract void SetCellToComponent(string componentName, object data, string parameter);
    }

    public class NodeDataModel : DataModel
    {
        public NodeDataModel(Dictionary<string, Component> components) : base(components)
        {
            this.Prototype = new Node();
        }

        public override void CreateComponent()
        {
            var component = new Node();
            this.Components.Add(component.UniqueName, component);
        }

        public override void SetCellToComponent(string componentName, object data, string parameter)
        {
            var node = (Node)this.Components[componentName];
            node.parameters[parameter].SetValue(data);
        }
    }

    public class MemberDataModel : DataModel
    {
        public MemberDataModel(Dictionary<string, Component> components) : base(components)
        {
            this.Prototype = new Member();
        }

        public override void CreateComponent()
        {
            var component = new Member();
            this.Components.Add(component.UniqueName, component);
        }

        public override void SetCellToComponent(string componentName, object data, string parameter)
        {
            try
            {
                // finds the member and component which will be changed
                var member = (Member)this.Components[componentName];
                var component = (Component)member.parameters[parameter].Value;

                // if any component is exits before (component != null)
                // remove the member from usedBy field of this component
                if (component != null) component.UsedBy.Remove(member);

                // sets the new component
                member.parameters[parameter].SetValue(data);

                // takes the new component and adds this member to its usedBy field
                component = (Component)member.parameters[parameter].Value;
                if (component != null) component.UsedBy.Add(member);
            }
            catch
            {
                var member = (Member)this.Components[componentName];
                member.parameters[parameter].SetValue(data);
            }

        }
    }

    public class MaterialDataModel : DataModel
    {
        public MaterialDataModel(Dictionary<string, Component> components) : base(components)
        {
            this.Prototype = new Material();
        }

        public override void CreateComponent()
        {
            var component = new Material();
            this.Components.Add(component.UniqueName, component);
        }

        public override void SetCellToComponent(string componentName, object data, string parameter)
        {
            var material = (Material)this.Components[componentName];
            material.parameters[parameter].SetValue(data);
        }
    }

    public class SectionDataModel : DataModel
    {
        public SectionDataModel(Dictionary<string, Component> components) : base(components)
        {
            this.Prototype = new Section();
        }

        public override void CreateComponent()
        {
            var component = new Section();
            this.Components.Add(component.UniqueName, component);
        }

        public override void SetCellToComponent(string componentName, object data, string parameter)
        {
            var section = (Section)this.Components[componentName];
            section.parameters[parameter].SetValue(data);
        }
    }
}