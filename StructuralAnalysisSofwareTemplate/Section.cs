using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralAnalysisSofwareTemplate
{
    public class Section : BaseClass
    {
        public string Name;
        private double height;
        private double width;
        private double area;
        private double inertia;

        public Section()
        {
            this.height = 0;
            this.width = 0;
            this.area = 0;
            this.inertia = 0;
            this.Name = "Section: " + Form1.tempDatabase.get(3).Count.ToString();
        }

        public void SetAll(double height, double width)
        {
            this.height = height;
            this.width = width;
            this.area = this.height*this.width;
            this.inertia = this.height*Math.Pow(this.width,3)/12;

        }

        public List<string> GetAll()
        {
            List<string> fieldData = new List<string>();

            fieldData.Add(this.Name.ToString());
            fieldData.Add(this.height.ToString());
            fieldData.Add(this.width.ToString());
            fieldData.Add(this.area.ToString());
            fieldData.Add(this.inertia.ToString());

            return fieldData;
        }
        

    }
}
