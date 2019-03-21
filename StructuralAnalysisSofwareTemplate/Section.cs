using System;

namespace StructuralAnalysisSofwareTemplate
{
    public class Section : Component
    {
        public Section()
        {
            this.height = 0;
            this.width = 0;
            this.Area = 0;
            this.Inertia = 0;
            this.Name = "Section: " + Database.SectionList.Count.ToString();
        }

        public double Height
        {
            get { return this.height; }
            set
            {
                this.height = value;
                this.Area = this.height * this.Width;
                this.Inertia = this.height * Math.Pow(this.width, 3) / 12;
            }
        }

        public double Width
        {
            get { return this.width; }
            set
            {
                this.width = value;
                this.Area = this.height * this.Width;
                this.Inertia = this.height * Math.Pow(this.width, 3) / 12;
            }
        }

        public double Area { get; private set; }
        public double Inertia { get; private set; }

        public double height { get; private set; }
        public double width { get; private set; }

        public override void Delete()
        {
            Database.SectionList.Remove(this.Name);
        }
    }
}