using System;
using System.Collections.Generic;

namespace StructuralAnalysisSofwareTemplate
{
    public abstract class Parameter
    {
        public Parameter()
        {
        }

        public object Value { get; protected set; }
        public string Display { get; protected set; }

        // stores the list of affected parameters by this parameter
        public List<DependentParameter> affects = new List<DependentParameter>();

        // refreshes the affected parameters by this parameter
        // every parameter calls it when it's constructor or setValue methods are called.
        public void RefreshAffects()
        {
            // for each parameter affected by this parameter calls refresh function
            foreach (var affected in affects)
            {
                affected.RecalculateValue();
            }
        }

        public abstract void SetValue(object value);

        public virtual void SetValue(object value, MultiLine multiline) { }

        public virtual bool IsReadOnly()
        {
            return false;
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