using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace StructuralAnalysisSofwareTemplate
{
    public abstract class Parameter
    {
        public object Value { get; protected set; }
        public string Display { get; protected set; }

        public List<DependentParameter> affects = new List<DependentParameter>();

        protected void RefreshAffects()
        {
            foreach (var parameter in affects)
            {
                parameter.RefreshDepends();
            }
        }

        public bool readOnly { get; protected set; }

        public virtual void SetValue(object value)
        {
            this.Value = value;
            this.Display = value.ToString();
        }

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
            this.readOnly = true;
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