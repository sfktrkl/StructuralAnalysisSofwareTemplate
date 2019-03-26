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

        public Dictionary<string, Component> Components { get; private set; }

        public abstract List<string> GetColumnNames();

        public abstract void CreateComponent();

        public abstract Tuple<List<string>, List<bool>> GetRowData(string componentName);

        public abstract void SetCellToComponent(string componentName, object data, string parameter);
    }

    public class NodeDataModel : DataModel
    {
        public NodeDataModel(Dictionary<string, Component> components) : base(components)
        {
        }

        public override List<string> GetColumnNames()
        {
            var columnNames = new List<string>();

            // returns all parameter keys for writing column names.
            foreach (KeyValuePair<string, Parameter> parameter in new Node().parameters)
            {
                columnNames.Add(parameter.Key.ToString());
            }

            return columnNames;
        }

        public override void CreateComponent()
        {
            var component = new Node();
            this.Components.Add(component.UniqueName, component);
        }

        public override Tuple<List<string>, List<bool>> GetRowData(string componentName)
        {
            var component = (Node)this.Components[componentName];
            var rowData = new List<string>();
            var readOnlyData = new List<bool>();

            // returns all displays for writing cells values.
            foreach (KeyValuePair<string, Parameter> parameter in component.parameters)
            {
                rowData.Add(parameter.Value.Display);
                readOnlyData.Add(parameter.Value.readOnly);
            }

            return Tuple.Create(rowData, readOnlyData);
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
        }

        public override List<string> GetColumnNames()
        {
            var columnNames = new List<string>();

            // returns all parameter keys for writing column names.
            foreach (KeyValuePair<string, Parameter> parameter in new Member().parameters)
            {
                columnNames.Add(parameter.Key.ToString());
            }

            return columnNames;
        }

        public override void CreateComponent()
        {
            var component = new Member();
            this.Components.Add(component.UniqueName, component);
        }

        public override Tuple<List<string>, List<bool>> GetRowData(string componentName)
        {
            var component = (Member)this.Components[componentName];
            var rowData = new List<string>();
            var readOnlyData = new List<bool>();

            // returns all displays for writing cells values.
            foreach (KeyValuePair<string, Parameter> parameter in component.parameters)
            {
                rowData.Add(parameter.Value.Display);
                readOnlyData.Add(parameter.Value.readOnly);
            }

            return Tuple.Create(rowData, readOnlyData);
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
        }

        public override List<string> GetColumnNames()
        {
            var columnNames = new List<string>();

            // returns all parameter keys for writing column names.
            foreach (KeyValuePair<string, Parameter> parameter in new Material().parameters)
            {
                columnNames.Add(parameter.Key.ToString());
            }

            return columnNames;
        }

        public override void CreateComponent()
        {
            var component = new Material();
            this.Components.Add(component.UniqueName, component);
        }

        public override Tuple<List<string>, List<bool>> GetRowData(string componentName)
        {
            var component = (Material)this.Components[componentName];
            var rowData = new List<string>();
            var readOnlyData = new List<bool>();

            // returns all displays for writing cells values.
            foreach (KeyValuePair<string, Parameter> parameter in component.parameters)
            {
                rowData.Add(parameter.Value.Display);
                readOnlyData.Add(parameter.Value.readOnly);
            }

            return Tuple.Create(rowData, readOnlyData);
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
        }

        public override List<string> GetColumnNames()
        {
            var columnNames = new List<string>();

            // returns all parameter keys for writing column names.
            foreach (KeyValuePair<string, Parameter> parameter in new Section().parameters)
            {
                columnNames.Add(parameter.Key.ToString());
            }

            return columnNames;
        }

        public override void CreateComponent()
        {
            var component = new Section();
            this.Components.Add(component.UniqueName, component);
        }

        public override Tuple<List<string>, List<bool>> GetRowData(string componentName)
        {
            var component = (Section)this.Components[componentName];
            var rowData = new List<string>();
            var readOnlyData = new List<bool>();

            // returns all displays for writing cells values.
            foreach (KeyValuePair<string, Parameter> parameter in component.parameters)
            {
                rowData.Add(parameter.Value.Display);
                readOnlyData.Add(parameter.Value.readOnly);
            }

            return Tuple.Create(rowData, readOnlyData);
        }

        public override void SetCellToComponent(string componentName, object data, string parameter)
        {
            var section = (Section)this.Components[componentName];
            section.parameters[parameter].SetValue(data);
        }
    }
}