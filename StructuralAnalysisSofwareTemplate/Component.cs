using System.Collections.Generic;

namespace StructuralAnalysisSofwareTemplate
{
    public abstract class Parameter
    {
        public object Value { get; protected set; }
        public string Display { get; protected set; }

        public virtual bool IsReadOnly() { return false; }
        public abstract void SetValue(string value);
    }

    public class Name : Parameter
    {
        public Name(string value)
        {
            this.Value = value;
            this.Display = value.ToString();
        }

        public override void SetValue(string value)
        {
            this.Value = (string)value;
        }

        public override bool IsReadOnly()
        {
            return true;
        }
    }

    public class Number : Parameter
    {
        public Number(double value)
        {
            this.Value = value;
            this.Display = value.ToString();
        }
    }

    public class TrueFalse : Parameter
    {
        public TrueFalse(bool value)
        {
            this.Value = value;
            this.Display = value.ToString();
        }
    }

    public abstract class Component
    {
        // Each component is handled by its name.
        public string Name;

        public Dictionary<string, Parameter> parameters;

        // contains which components are using this components.
        public List<Component> UsedBy = new List<Component>();

        // virtual method for deleting components
        public abstract void Delete();
    }
}