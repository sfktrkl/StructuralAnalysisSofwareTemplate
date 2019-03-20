using System.Collections.Generic;

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

        private double unitWeight;
        private double elasticModulus;

        public override void Delete()
        {
            Database.MaterialList.Remove(this.Name);
        }

        public void SetAll(double elasticModulus, double unitWeight)
        {
            this.elasticModulus = elasticModulus;
            this.unitWeight = unitWeight;
        }

        public override List<string> GetAll()
        {
            return new List<string>
            {
                this.Name.ToString(),
                this.unitWeight.ToString(),
                this.elasticModulus.ToString()
            };
        }
    }
}