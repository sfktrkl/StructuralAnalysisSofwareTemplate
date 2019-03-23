using System;
using System.Drawing;
using System.Windows.Forms;

namespace StructuralAnalysisSofwareTemplate
{
    public partial class DockableForm : Form
    {
        private Point previousLocation;
        private Point MouseDownLocation;
        private bool rightClickActive;

        public DockableForm()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel1_MouseUp(object sender, MouseEventArgs e)
        {
            // docks the form according to movement of cursor
            if (rightClickActive == true)
            {
                // relocating navigator in form1 panel
                this.TopLevel = false;
                MainForm form1 = (MainForm)Application.OpenForms["MainForm"];
                Panel panel1 = (Panel)form1.Controls["panel1"];
                panel1.Controls.Add(this);

                if (previousLocation.X > Cursor.Position.X && Math.Abs(previousLocation.Y - Cursor.Position.Y) < 300)
                {
                    this.Dock = DockStyle.Left;
                }
                else if (previousLocation.X < Cursor.Position.X && Math.Abs(previousLocation.Y - Cursor.Position.Y) < 300)
                {
                    this.Dock = DockStyle.Right;
                }
                else
                {
                    this.Dock = DockStyle.Fill;
                }

                Cursor = Cursors.Arrow;
                rightClickActive = false;
            }
        }

        private void splitContainer1_Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.Left = e.X + this.Left - MouseDownLocation.X;
                this.Top = e.Y + this.Top - MouseDownLocation.Y;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void splitContainer1_Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            // enables dragging with left click to panel
            if (e.Button == MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
                this.MdiParent = null;
                this.Dock = DockStyle.None;
            }
            // enables docking with right click to panel1
            if (e.Button == MouseButtons.Right)
            {
                // takes cursor location for docking
                previousLocation = Cursor.Position;
                Cursor = Cursors.Hand;
                rightClickActive = true;
            }
        }
    }
}