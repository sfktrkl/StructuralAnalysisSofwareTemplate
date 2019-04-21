using OSGClassLibrary;
using System;

namespace StructuralAnalysisSofwareTemplate
{
    public partial class _3DView : DockableForm
    {
        private OSGClassWrapper myWrapper = new OSGClassWrapper();

        public _3DView()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // draws cube to screen
            myWrapper.TakeInputWrapper(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // draws sphere to screen
            myWrapper.TakeInputWrapper(2);
        }
    }
}