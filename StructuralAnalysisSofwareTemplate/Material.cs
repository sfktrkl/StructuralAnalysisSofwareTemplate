namespace StructuralAnalysisSofwareTemplate
{
    public class Material : Component
    {
        public Material()
        {
            this.elasticModulus = 0;
            this.unitWeight = 0;
            this.Name = "Material: " + Database.MaterialList.Count.ToString();
        }

        public double unitWeight { get; private set; }
        public double elasticModulus { get; private set; }

        public override void Delete()
        {
            Database.MaterialList.Remove(this.Name);
        }

        public void SetAll(double unitWeight, double elasticModulus)
        {
            this.elasticModulus = elasticModulus;
            this.unitWeight = unitWeight;
        }
    }
}