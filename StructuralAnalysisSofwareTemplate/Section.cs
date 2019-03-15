﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralAnalysisSofwareTemplate
{
    public class Section
    {
        static int numOfSections = 0;
        public string section_Name;
        private double height;
        private double width;
        private double area;
        private double inertia;
        private Dictionary<string, Member> isUsed = new Dictionary<string, Member>();
        public bool used = false;

        public Section()
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

        public List<string> GetAll()
        {
            List<string> fieldData = new List<string>();

            fieldData.Add(this.section_Name.ToString());
            fieldData.Add(this.height.ToString());
            fieldData.Add(this.width.ToString());
            fieldData.Add(this.area.ToString());
            fieldData.Add(this.inertia.ToString());

            return fieldData;
        }
        
        public void usedBy(string memberName,Member member, bool condition)
        {
            if (condition == true)
            {
                isUsed.Add(memberName, member);
                used = true;
            }
            else
            {
                isUsed.Remove(memberName);
                if (isUsed.Count == 0)
                {
                    used = false;
                }
            }

        }

    }
}