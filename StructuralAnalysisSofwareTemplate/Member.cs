using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StructuralAnalysisSofwareTemplate
{
    public class Member : BaseClass
    {
        public string Name;
        private Node Node1;
        private Node Node2;
        private Material Material;
        private Section Section;

        public Member()
        {
            this.Node1 = null;
            this.Node2 = null;
            this.Material = null;
            this.Section = null;
            this.Name = "Member: " + Form1.tempDatabase.get(1).Count.ToString();

        }

        public override void delete()
        {
            // when this member is deleted
            // deletes this member from all objects used dictionaries
            try
            {
                try
                {
                    try
                    {
                        try
                        {
                            this.Section.usedBy(this.Name, this, false);
                        }
                        catch { }
                        this.Material.usedBy(this.Name, this, false);
                    }
                    catch { }
                    this.Node2.usedBy(this.Name, this, false);
                }
                catch { }
                this.Node1.usedBy(this.Name, this, false);
            }
            catch { }
        }


        public void SetAll(Node Node1, Node Node2, Material Material, Section Section)
        {
            this.Node1 = Node1;
            this.Node2 = Node2;
            this.Material = Material;
            this.Section = Section;
            // adds objects used dictionaries to this member
            try
            {
                try
                {
                    try
                    {
                        try
                        {
                            this.Section.usedBy(this.Name, this, true);
                        }
                        catch { }
                        this.Material.usedBy(this.Name, this, true);
                    }
                    catch { }
                    this.Node2.usedBy(this.Name, this, true);
                }
                catch { }
                this.Node1.usedBy(this.Name, this, true);
            }
            catch { }

        }

        public List<string> GetAll()
        {
            List<string> fieldData = new List<string>();

            fieldData.Add(this.Name == null ? "NULL" : this.Name.ToString());
            fieldData.Add(this.Node1 == null ? "NULL" : this.Node1.Name.ToString());
            fieldData.Add(this.Node2 == null ? "NULL" : this.Node2.Name.ToString());
            fieldData.Add(this.Material == null ? "NULL" : this.Material.Name.ToString());
            fieldData.Add(this.Section == null ? "NULL" : this.Section.Name.ToString());
            return fieldData;
        }

    }
}