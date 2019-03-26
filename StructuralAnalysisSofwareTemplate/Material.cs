namespace StructuralAnalysisSofwareTemplate
{
    public class Material : Component
    {
        public Material()
        {
            this.UniqueName = "Material: " + Database.MaterialList.Count.ToString();
            this.parameters.Add("Material Name", new Name("Material: " + Database.MaterialList.Count.ToString()));
            this.parameters.Add("Unit Weight", new Number(0.0));
            this.parameters.Add("Elastic Modulus", new Number(0.0));
        }

        public override void Delete()
        {
            Database.MaterialList.Remove(this.UniqueName);
        }
    }
}