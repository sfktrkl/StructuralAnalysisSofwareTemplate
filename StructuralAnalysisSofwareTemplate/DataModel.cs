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

        public abstract List<String> GetColumnNames();
        public abstract void CreateComponent();
        ReleaseAllFor(int index);
    }

    public class NodeDataModel : DataModel
    {
        public NodeDataModel(Dictionary<string, Component> components) : base(components) { }

        public override List<String> GetColumnNames()
        {
            return null;
        }

        public override void CreateComponent()
        {
            var component = new Node();
            this.Components.Add(component.Name, component);
        }
    }

    public class MemberDataModel : DataModel
    {
        public MemberDataModel(Dictionary<string, Component> components) : base(components) { }

        public override List<String> GetColumnNames()
        {
            return null;
        }

        public override void CreateComponent()
        {
            var component = new Member();
            this.Components.Add(component.Name, component);
        }
    }

    public class MaterialDataModel : DataModel
    {
        public MaterialDataModel(Dictionary<string, Component> components) : base(components) { }

        public override List<String> GetColumnNames()
        {
            return null;
        }

        public override void CreateComponent()
        {
            var component = new Material();
            this.Components.Add(component.Name, component);
        }
    }

    public class SectionDataModel : DataModel
    {
        public SectionDataModel(Dictionary<string, Component> components) : base(components) { }

        public override List<String> GetColumnNames()
        {
            return null;
        }

        public override void CreateComponent()
        {
            var component = new Section();
            this.Components.Add(component.Name, component);
        }
    }
}