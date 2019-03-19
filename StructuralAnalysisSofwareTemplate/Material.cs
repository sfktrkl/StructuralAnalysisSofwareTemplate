using System.Collections.Generic;

namespace StructuralAnalysisSofwareTemplate
{
    public class Material : Component
    {
        public Material()
        {
            this.elasticModulus = 0;
            this.unitWeight = 0;
            this.Name = "Material: " + Database.get(2).Count.ToString();
        }

        private double unitWeight;
        private double elasticModulus;

        public void SetAll(double elasticModulus, double unitWeight)
        {
            this.elasticModulus = elasticModulus;
            this.unitWeight = unitWeight;
        }

        public List<string> GetAll()
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