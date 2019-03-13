using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralAnalysisSofwareTemplate
{
    class Material
    {
        static int numOfMaterial = 0;
        private string material_Name;
        private double unitWeight;
        private double elasticModulus;

        Material()
        {
            this.elasticModulus = 0;
            this.unitWeight = 0;
            this.material_Name = "Material: " + numOfMaterial.ToString();
            numOfMaterial++;
        }

        public void SetAll(double elasticModulus, double unitWeight)
        {
            this.elasticModulus = elasticModulus;
            this.unitWeight = unitWeight;
        }
    }
}
