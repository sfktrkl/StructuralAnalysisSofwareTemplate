﻿using OSGClassLibrary;
using System.Windows.Forms;
using System;

namespace StructuralAnalysisSofwareTemplate
{
    public partial class _3DView : DockableForm
    {
        private OSGClassWrapper myWrapper = new OSGClassWrapper();

        public _3DView()
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
            myWrapper.TakeInputWrapper(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // draws sphere to screen
            myWrapper.TakeInputWrapper(2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myWrapper.Destroy();
            pictureBox1.Paint -= Painter;
            this.Close();
        }
    }
}