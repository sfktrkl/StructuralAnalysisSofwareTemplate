﻿using System.Collections.Generic;

namespace StructuralAnalysisSofwareTemplate
{
    public class Member : Component
    {
        public Member()
        {
            this.UniqueName = "Member: " + Database.MemberList.Count.ToString();
            this.parameters.Add("Member Name", new Name("Member: " + Database.MemberList.Count.ToString()));
            this.parameters.Add("First Node", new NodeInstance());
            this.parameters.Add("Second Node", new NodeInstance());
            this.parameters.Add("Material", new MaterialInstance());
            this.parameters.Add("Section", new SectionInstance());
            this.parameters.Add("Length", new Length(new List<Parameter> { this.parameters["First Node"], this.parameters["Second Node"] }));
        }

        // overloaded constructor for multiLine
        public Member(MultiLine multiLine, Node firstNode, Node secondNode)
        {
            this.UniqueName = "Member: " + multiLine.Members.Count.ToString();
            this.parameters.Add("Member Name", new Name("Member: " + multiLine.Members.Count.ToString()));

            this.parameters.Add("First Node", new NodeInstance());
            this.parameters["First Node"].SetValue(firstNode.UniqueName, multiLine);

            this.parameters.Add("Second Node", new NodeInstance());
            this.parameters["Second Node"].SetValue(secondNode.UniqueName, multiLine);

            this.parameters.Add("Material", new MaterialInstance());
            var material = (Material)multiLine.parameters["Material"].Value;
            this.parameters["Material"].SetValue(material.UniqueName);

            this.parameters.Add("Section", new SectionInstance());
            var section = (Section)multiLine.parameters["Section"].Value;
            this.parameters["Section"].SetValue(section.UniqueName);

            this.parameters.Add("Length", new Length(new List<Parameter> { this.parameters["First Node"], this.parameters["Second Node"] }));
        }

        public override void Delete()
        {
            // when this member is deleted
            // deletes this member from all components usedBy lists
            foreach (var components in this.parameters.Values)
            {
                var component = (Component)components.Value;
                component.UsedBy.Remove(this);
            }

            Database.MemberList.Remove(this.UniqueName);
        }
    }
}