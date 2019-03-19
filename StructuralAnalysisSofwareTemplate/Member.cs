using System.Collections.Generic;

namespace StructuralAnalysisSofwareTemplate
{
    public class Member : Component
    {
        public Member()
        {
            this.Name = "Member: " + Database.get(1).Count.ToString();
        }

        private Node Node1;
        private Node Node2;
        private Material Material;
        private Section Section;

        public override void Delete()
        {
            // when this member is deleted
            // deletes this member from all objects used dictionaries
            this.Section.UsedBy.Remove(this);
            this.Material.UsedBy.Remove(this);
            this.Node1.UsedBy.Remove(this);
            this.Node2.UsedBy.Remove(this);
        }

        public void SetAll(Node Node1, Node Node2, Material Material, Section Section)
        {
            this.Node1 = Node1;
            this.Node2 = Node2;
            this.Material = Material;
            this.Section = Section;
            // adds objects used dictionaries to this member

            this.Section.UsedBy.Add(this);
            this.Material.UsedBy.Add(this);
            this.Node1.UsedBy.Add(this);
            this.Node2.UsedBy.Add(this);
        }

        public List<string> GetAll()
        {
            return new List<string>
            {
                this.Name == null ? "NULL" : this.Name.ToString(),
                this.Node1 == null ? "NULL" : this.Node1.Name.ToString(),
                this.Node2 == null ? "NULL" : this.Node2.Name.ToString(),
                this.Material == null ? "NULL" : this.Material.Name.ToString(),
                this.Section == null ? "NULL" : this.Section.Name.ToString()
            };
        }
    }
}