using OSGClassLibrary;
using System;
using System.Windows.Forms;

namespace StructuralAnalysisSofwareTemplate
{
    public partial class View3D : DockableForm
    {
        private OSGClassWrapper myWrapper = new OSGClassWrapper();

        public View3D()
        {
            InitializeComponent();
            pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(Painter);
        }

        private void Painter(object sender, PaintEventArgs e)
        {
            // Renders the OSG Viewer into the drawing area
            myWrapper.Render(pictureBox1.Handle);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // draws cube to screen
            myWrapper.TakeInput(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // draws sphere to screen
            myWrapper.TakeInput(2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Paint -= Painter;
            myWrapper.Destroy();
            this.Close();
        }
    }
}