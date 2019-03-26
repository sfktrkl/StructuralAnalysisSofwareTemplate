using System.Collections.Generic;

namespace StructuralAnalysisSofwareTemplate
{
    public class Section : Component
    {
        public Section()
        {
            this.UniqueName = "Section: " + Database.SectionList.Count.ToString();
            this.parameters.Add("Section Name", new Name("Section: " + Database.SectionList.Count.ToString()));
            this.parameters.Add("Height", new Number(0.0));
            this.parameters.Add("Width", new Number(0.0));
            this.parameters.Add("Area", new Area(new List<Parameter> { this.parameters["Height"], this.parameters["Width"] }));
            this.parameters.Add("Inertia", new Inertia(new List<Parameter> { this.parameters["Height"], this.parameters["Width"] }));
        }

        public override void Delete()
        {
            Database.SectionList.Remove(this.UniqueName);
        }
    }
}