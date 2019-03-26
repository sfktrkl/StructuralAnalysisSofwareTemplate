using System.Collections.Generic;

namespace StructuralAnalysisSofwareTemplate
{
    public abstract class Component
    {
        // Each component is handled by its name.
        public string UniqueName;

        public Dictionary<string, Parameter> parameters = new Dictionary<string, Parameter>();

        // contains which components are using this components.
        public List<Component> UsedBy = new List<Component>();

        // virtual method for deleting components
        public abstract void Delete();
    }
}