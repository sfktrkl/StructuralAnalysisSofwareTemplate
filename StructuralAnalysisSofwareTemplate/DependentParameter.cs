using System;
using System.Collections.Generic;

namespace StructuralAnalysisSofwareTemplate
{
    public abstract class DependentParameter : Parameter
    {
        protected List<Parameter> depends = new List<Parameter>();

        // refreshes value and display of this parameter
        // method can be differentiate according to the parameter type
        public abstract void RefreshDepends();

        // sets readonly as true since parameter is dependent to other parameters
        // means that they are not(can not) directly taken by user input
        public DependentParameter()
        {
            this.readOnly = true;
        }
    }

    public class Area : DependentParameter
    {
        public Area(List<Parameter> depends)
        {
            this.depends = depends;
            foreach (var parameter in this.depends)
            {
                parameter.affects.Add(this);
            }

            var height = Convert.ToDouble(this.depends[0].Value);
            var width = Convert.ToDouble(this.depends[1].Value);

            this.Value = height * width;
            this.Display = this.Value.ToString();
        }

        public override void RefreshDepends()
        {
            var height = Convert.ToDouble(depends[0].Value);
            var width = Convert.ToDouble(depends[1].Value);

            this.Value = height * width;
            this.Display = this.Value.ToString();
        }
    }

    public class Inertia : DependentParameter
    {
        public Inertia(List<Parameter> depends)
        {
            this.depends = depends;
            foreach (var parameter in this.depends)
            {
                parameter.affects.Add(this);
            }

            var height = Convert.ToDouble(this.depends[0].Value);
            var width = Convert.ToDouble(this.depends[1].Value);

            this.Value = Math.Pow(height, 3) * width / 12;
            this.Display = this.Value.ToString();
        }

        public override void RefreshDepends()
        {
            var height = Convert.ToDouble(depends[0].Value);
            var width = Convert.ToDouble(depends[1].Value);

            this.Value = Math.Pow(height, 3) * width / 12;
            this.Display = this.Value.ToString();
        }
    }

    public class Length : DependentParameter
    {
        public Length(List<Parameter> depends)
        {
            this.depends = depends;
            foreach (var parameter in this.depends)
            {
                parameter.affects.Add(this);
            }

            if (depends[0].Value != null && depends[1].Value != null)
            {
                var node1 = (Node)this.depends[0].Value;
                var node2 = (Node)this.depends[1].Value;
                var x1 = Convert.ToDouble(node1.parameters["X Coordinate"].Value);
                var x2 = Convert.ToDouble(node2.parameters["X Coordinate"].Value);
                var y1 = Convert.ToDouble(node1.parameters["Y Coordinate"].Value);
                var y2 = Convert.ToDouble(node2.parameters["Y Coordinate"].Value);

                this.Value = Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
                this.Display = this.Value.ToString();
            }
        }

        public override void RefreshDepends()
        {
            if (depends[0].Value != null && depends[1].Value != null)
            {
                var node1 = (Node)this.depends[0].Value;
                var node2 = (Node)this.depends[1].Value;
                var x1 = Convert.ToDouble(node1.parameters["X Coordinate"].Value);
                var x2 = Convert.ToDouble(node2.parameters["X Coordinate"].Value);
                var y1 = Convert.ToDouble(node1.parameters["Y Coordinate"].Value);
                var y2 = Convert.ToDouble(node2.parameters["Y Coordinate"].Value);

                this.Value = Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
                this.Display = this.Value.ToString();
            }
        }
    }
}