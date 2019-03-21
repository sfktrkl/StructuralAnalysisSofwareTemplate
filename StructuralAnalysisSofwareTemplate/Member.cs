namespace StructuralAnalysisSofwareTemplate
{
    public class Member : Component
    {
        public Member()
        {
            this.Name = "Member: " + Database.MemberList.Count.ToString();
        }

        public Node Node1 { get; private set; }
        public Node Node2 { get; private set; }
        public Material Material { get; private set; }
        public Section Section { get; private set; }

        public override void Delete()
        {
            // when this member is deleted
            // deletes this member from all objects used dictionaries
            this.Section.UsedBy.Remove(this);
            this.Material.UsedBy.Remove(this);
            this.Node1.UsedBy.Remove(this);
            this.Node2.UsedBy.Remove(this);

            Database.MemberList.Remove(this.Name);
        }

        public void SetAll(Node Node1, Node Node2, Material Material, Section Section)
        {
            this.Node1 = Node1;
            this.Node2 = Node2;
            this.Material = Material;
            this.Section = Section;
            // adds objects used dictionaries to this member

            if (this.Section != null) this.Section.UsedBy.Add(this);
            if (this.Material != null) this.Material.UsedBy.Add(this);
            if (this.Node1 != null) this.Node1.UsedBy.Add(this);
            if (this.Node2 != null) this.Node2.UsedBy.Add(this);
        }
    }
}