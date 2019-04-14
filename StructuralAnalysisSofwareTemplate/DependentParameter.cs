using System;
using System.Collections.Generic;

namespace StructuralAnalysisSofwareTemplate
{
    public abstract class DependentParameter : Parameter
    {
        // sets readonly as true since parameter is dependent to other parameters
        // means that they are not(can not) directly taken by user input
        public DependentParameter()
        {
        }

        protected List<Parameter> depends = new List<Parameter>();

        // refreshes value and display of this parameter
        // method can be differentiate according to the parameter type
        public abstract void RecalculateValue();

        // adds this parameter to affects list of parameters which affects this parameter
        // since this parameter depends these parameters
        public virtual void notifyAffectors()
        {
            foreach (var depended in this.depends)
            {
                depended.affects.Add(this);
            }
        }

        public override void SetValue(object value)
        {
            throw new Exception("Dependent parameter values cannot be set after construction.");
        }

        public override bool IsReadOnly()
        {
            return true;
        }
    }

    public class Area : DependentParameter
    {
        public Area(List<Parameter> depends)
        {
            this.depends = depends;
            this.notifyAffectors();
            this.RecalculateValue();
        }

        public override void RecalculateValue()
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
            this.notifyAffectors();
            this.RecalculateValue();
        }

        public override void RecalculateValue()
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
            this.notifyAffectors();
            this.RecalculateValue();
        }

        public override void RecalculateValue()
        {
            if (this.depends[0].Value != null && this.depends[1].Value != null)
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

    public class Interval : DependentParameter
    {
        public Interval(List<Parameter> depends)
        {
            this.depends = depends;
            this.notifyAffectors();
            this.RecalculateValue();
        }

        public override void RecalculateValue()
        {
            if (this.depends[0].Value != null && this.depends[1].Value != null && Convert.ToDouble(this.depends[2].Value) > 0)
            {
                var node1 = (Node)this.depends[0].Value;
                var node2 = (Node)this.depends[1].Value;
                var x1 = Convert.ToDouble(node1.parameters["X Coordinate"].Value);
                var x2 = Convert.ToDouble(node2.parameters["X Coordinate"].Value);
                var y1 = Convert.ToDouble(node1.parameters["Y Coordinate"].Value);
                var y2 = Convert.ToDouble(node2.parameters["Y Coordinate"].Value);
                var xInterval = (x2 - x1) / Convert.ToDouble(depends[2].Value);
                var yInterval = (y2 - y1) / Convert.ToDouble(depends[2].Value);
                var xyInverval = Math.Sqrt(Math.Pow(xInterval, 2) + Math.Pow(yInterval, 2));
                var interval = new List<double> { xInterval, yInterval, xyInverval };
                this.Value = interval;
                this.Display = interval[2].ToString();
            }
        }
    }
}