using System;
using System.Collections.Generic;
using System.Windows.Forms;

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

        //ReleaseAllFor(int index);
        public abstract List<string> GetRowData(string componentName);

        public abstract void SetComponentFromData(List<dynamic> componentData);
    }

    public class NodeDataModel : DataModel
    {
        public NodeDataModel(Dictionary<string, Component> components) : base(components)
        {
        }

        public override List<string> GetColumnNames()
        {
            return new List<string>
            {
                "Node Name",
                "X Coordinate",
                "Y Coordinate",
                "X Fixity",
                "Y Fixity",
                "Z Fixity",
                "X Stiffness",
                "Y Stiffness",
                "Z Stiffness"
            };
        }

        public override void CreateComponent()
        {
            var component = new Node();
            this.Components.Add(component.Name, component);
        }

        public override List<string> GetRowData(string componentName)
        {
            var component = (Node)this.Components[componentName];
            return new List<string>
            {
                component.Name,
                component.xCoordinate.ToString(),
                component.yCoordinate.ToString(),
                component.xFixity.ToString(),
                component.yFixity.ToString(),
                component.zFixity.ToString(),
                component.xStiffness.ToString(),
                component.yStiffness.ToString(),
                component.zStiffness.ToString()
            };
        }

        public override void SetComponentFromData(List<dynamic> componentData)
        {
            var component = (Node)this.Components[componentData[0]];
            bool xFixity = Database.AutoComplete(componentData[3].ToString());
            bool yFixity = Database.AutoComplete(componentData[4].ToString());
            bool zFixity = Database.AutoComplete(componentData[5].ToString());

            // setting node properties
            component.SetAll(
                Convert.ToDouble(componentData[1]),
                Convert.ToDouble(componentData[2]),
                Convert.ToBoolean(xFixity),
                Convert.ToBoolean(yFixity),
                Convert.ToBoolean(zFixity),
                Convert.ToDouble(componentData[6]),
                Convert.ToDouble(componentData[7]),
                Convert.ToDouble(componentData[8])
            );

        }
    }

    public class MemberDataModel : DataModel
    {
        public MemberDataModel(Dictionary<string, Component> components) : base(components)
        {
        }

        public override List<string> GetColumnNames()
        {
            return new List<string>
            {
                "Member Name",
                "First Node",
                "Second Node",
                "Material",
                "Section"
            };
        }

        public override void CreateComponent()
        {
            var component = new Member();
            this.Components.Add(component.Name, component);
        }

        public override List<string> GetRowData(string componentName)
        {
            var component = (Member)this.Components[componentName];
            return new List<string>
            {
                component.Name,
                component.Node1 != null ? component.Node1.Name : "NULL",
                component.Node2 != null ? component.Node2.Name : "NULL",
                component.Material != null ? component.Material.Name : "NULL",
                component.Section != null ? component.Section.Name : "NULL"
            };
        }

        public override void SetComponentFromData(List<dynamic> componentData)
        {
            // deletes this member from all previous related objects' used dictionaries
            // used property will be added again in the Member.setall function,
            // if new objects are same with previous ones.
            var member = this.Components[componentData[0]];

            // the following line will be a part of dataModel.ReleaseAllFor(index)
            if (member.Section != null) member.Section.UsedBy.Remove(member);
            if (member.Material != null) member.Material.UsedBy.Remove(member);
            if (member.Node1 != null) member.Node1.UsedBy.Remove(member);
            if (member.Node2 != null) member.Node2.UsedBy.Remove(member);

            try
            {
                member.SetAll
                (
                Database.AutoComplete(componentData[1].ToString(), typeof(Node)) == "NULL" ? (Node)null : Database.NodeList[Database.AutoComplete(componentData[1].ToString(), typeof(Node))],
                Database.AutoComplete(componentData[2].ToString(), typeof(Node)) == "NULL" ? (Node)null : Database.NodeList[Database.AutoComplete(componentData[1].ToString(), typeof(Node))],
                Database.AutoComplete(componentData[3].ToString(), typeof(Material)) == "NULL" ? (Material)null : Database.NodeList[Database.AutoComplete(componentData[1].ToString(), typeof(Material))],
                Database.AutoComplete(componentData[3].ToString(), typeof(Section)) == "NULL" ? (Section)null : Database.NodeList[Database.AutoComplete(componentData[1].ToString(), typeof(Section))]
                );
            }
            catch
            {
                MessageBox.Show("Object does not exist!");
                if (member.Section != null) member.Section.UsedBy.Add(member);
                if (member.Material != null) member.Material.UsedBy.Add(member);
                if (member.Node1 != null) member.Node1.UsedBy.Add(member);
                if (member.Node2 != null) member.Node2.UsedBy.Add(member);
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
            return new List<string>
            {
                "Material Name",
                "Unit Weight",
                "Elastic Modulus"
            };
        }

        public override void CreateComponent()
        {
            var component = new Material();
            this.Components.Add(component.Name, component);
        }

        public override List<string> GetRowData(string componentName)
        {
            var component = (Material)this.Components[componentName];

            return new List<string>
            {
                component.Name,
                component.unitWeight.ToString(),
                component.elasticModulus.ToString()
            };
        }

        public override void SetComponentFromData(List<dynamic> componentData)
        {
            var material = this.Components[componentData[0]];

            // setting material properties
            material.SetAll(
                Convert.ToDouble(componentData[1]),
                Convert.ToDouble(componentData[2])
            );
        }
    }

    public class SectionDataModel : DataModel
    {
        public SectionDataModel(Dictionary<string, Component> components) : base(components)
        {
        }

        public override List<string> GetColumnNames()
        {
            return new List<string>
            {
                "Section Name",
                "Height",
                "Width",
                "Area",
                "Inertia"
            };
        }

        public override void CreateComponent()
        {
            var component = new Section();
            this.Components.Add(component.Name, component);
        }

        public override List<string> GetRowData(string componentName)
        {
            var component = (Section)this.Components[componentName];

            return new List<string>
            {
                component.Name,
                component.Height.ToString(),
                component.Width.ToString(),
                component.Area.ToString(),
                component.Inertia.ToString()
            };
        }

        public override void SetComponentFromData(List<dynamic> componentData)
        {
            var section = this.Components[componentData[0]];

            section.Height = Convert.ToDouble(componentData[1]);
            section.Width = Convert.ToDouble(componentData[2]);

        }
    }
}