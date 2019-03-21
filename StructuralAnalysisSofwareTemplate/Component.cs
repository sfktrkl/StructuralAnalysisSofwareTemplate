using System.Collections.Generic;

namespace StructuralAnalysisSofwareTemplate
{
    public abstract class Component
    {
        // Each component is handled by its name.
        public string Name;

        // contains which components are using this components.
        public List<Component> UsedBy = new List<Component>();

        // virtual method for deleting objects
        public abstract void Delete();
    }
}