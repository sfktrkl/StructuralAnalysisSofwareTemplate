using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralAnalysisSofwareTemplate
{
    public class Material
    {
        static int numOfMaterial = 0;
        public string material_Name;
        private double unitWeight;
        private double elasticModulus;

        public Material()
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

        public List<string> GetAll()
        {
            List<string> fieldData = new List<string>();

            fieldData.Add(this.material_Name.ToString());
            fieldData.Add(this.unitWeight.ToString());
            fieldData.Add(this.elasticModulus.ToString());

            return fieldData;
        }
    }
}
