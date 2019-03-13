using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralAnalysisSofwareTemplate
{
    class Section
    {
        static int numOfSections = 0;
        private string section_Name;
        private double height;
        private double width;
        private double area;
        private double inertia;

        Section()
        {
            this.height = 0;
            this.width = 0;
            this.area = 0;
            this.inertia = 0;
            this.section_Name = "Section: " + numOfSections.ToString();
            numOfSections++;

        }

        public void SetAll(double height, double width)
        {
            this.height = height;
            this.width = width;
            this.area = this.height*this.width;
            this.inertia = this.height*Math.Pow(this.width,3)/12;

        }

    }
}
