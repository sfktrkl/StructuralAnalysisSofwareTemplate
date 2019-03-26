using System;
using System.Collections.Generic;

namespace StructuralAnalysisSofwareTemplate
{
    public abstract class Parameter
    {
        public object Value { get; protected set; }
        public string Display { get; protected set; }

        // stores the list of affected parameters by this parameter
        public List<DependentParameter> affects = new List<DependentParameter>();

        // refreshes the affected parameters by this parameter
        // every parameter calls it when it's constructor or setValue methods are called.
        public void RefreshAffects()
        {
            // for each parameter affected by this parameter calls refresh function
            foreach (var parameter in affects)
            {
                parameter.RefreshDepends();
            }
        }

        // readOnly status for datagridview display
        public bool readOnly { get; protected set; }

        public virtual void SetValue(object value)
        {
            this.Value = value;
            this.Display = value.ToString();
        }

        // sets readOnly field false since parameter requires user input
        public Parameter()
        {
            this.readOnly = false;
        }
    }

    public class Name : Parameter
    {
        public Name(string value)
        {
            this.Value = value;
            this.Display = value.ToString();
            this.RefreshAffects();
        }

        public override void SetValue(object value)
        {
            this.Value = value.ToString();
            this.Display = value.ToString();
            this.RefreshAffects();
        }
    }

    public class Number : Parameter
    {
        public Number(double value)
        {
            this.Value = value;
            this.Display = value.ToString();
            this.RefreshAffects();
        }

        public override void SetValue(object value)
        {
            this.Value = Convert.ToDouble(value);
            this.Display = value.ToString();
            this.RefreshAffects();
        }
    }

    public class TrueFalse : Parameter
    {
        public TrueFalse(bool value)
        {
            this.Value = value;
            this.Display = value.ToString();
            this.RefreshAffects();
        }

        public override void SetValue(object value)
        {
            var newValue = AutoComplete.TrueFalse(value.ToString());
            this.Value = newValue;
            this.Display = newValue.ToString();
            this.RefreshAffects();
        }
    }
}