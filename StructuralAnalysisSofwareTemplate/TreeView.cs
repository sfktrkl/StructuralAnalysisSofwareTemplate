using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace StructuralAnalysisSofwareTemplate
{
    public partial class TreeView : Form
    {
        private Point previousLocation;
        private Point MouseDownLocation;
        private bool rightClickActive;

        public TreeView()
        {
            InitializeComponent();
            // adds base nodes to treeview
            treeView1.Nodes.Add("Nodes");
            treeView1.Nodes.Add("Members");
            treeView1.Nodes.Add("Materials");
            treeView1.Nodes.Add("Sections");

        }

        public void refresh()
        {
            // clears the treeview and adds base nodes
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add("Nodes");
            treeView1.Nodes.Add("Members");
            treeView1.Nodes.Add("Materials");
            treeView1.Nodes.Add("Sections");

            // loops each object to add treeview
            foreach (var obj in Form1.nodeList)
            {
                treeView1.Nodes[0].Nodes.Add(obj.Value.Node_Name.ToString());
            }
            foreach (var obj in Form1.memberList)
            {
                treeView1.Nodes[1].Nodes.Add(obj.Value.member_Name.ToString());
            }
            foreach (var obj in Form1.materialList)
            {
                treeView1.Nodes[2].Nodes.Add(obj.Value.material_Name.ToString());
            }
            foreach (var obj in Form1.sectionList)
            {
                treeView1.Nodes[3].Nodes.Add(obj.Value.section_Name.ToString());
            }
            expand();

        }

        private void addTabs(string tabName)
        {
            // sets the spread sheet name and calls the function
            string newName;
            if (tabName.Contains("Node"))
            {
                newName = "Nodes";
                createSpreadSheet(newName,typeof(Node));
            }
            else if (tabName.Contains("Member"))
            {
                newName = "Members";
                createSpreadSheet(newName, typeof(Member));
            }
            else if (tabName.Contains("Material"))
            {
                newName = "Materials";
                createSpreadSheet(newName, typeof(Material));
            }
            else
            {
                newName = "Sections";
                createSpreadSheet(newName, typeof(Section));
            }

        }

        public void expand()
        {
            treeView1.ExpandAll();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // selects the node in mouse position and shows ContextMenuStrip
            if (e.Button == MouseButtons.Right)
            {
                treeView1.SelectedNode = e.Node;
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // double click event which sends clicked node text
            string clickedItem = e.Node.Text;
            addTabs(clickedItem);
        }

        private void createSpreadSheet(string spreadSheetName, Type givenClass)
        {
            // adds spread sheet in to form1.panel1
            SpreadSheet spreadSheet = new SpreadSheet();
            spreadSheet.TopLevel = false;
            Form1 form1 = (Form1)Application.OpenForms["Form1"];
            Panel panel1 = (Panel)form1.Controls["panel1"];
            panel1.Controls.Add(spreadSheet);
            spreadSheet.Show();
            spreadSheet.Dock = DockStyle.Right;
            spreadSheet.Text = spreadSheetName;

            spreadSheet.refresh(givenClass);

        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refresh();
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

        private void splitContainer1_Panel1_MouseUp(object sender, MouseEventArgs e)
        {
            // docks the form according to movement of cursor
            if (rightClickActive == true)
            {
                // relocating navigator in form1 panel
                this.TopLevel = false;
                Form1 form1 = (Form1)Application.OpenForms["Form1"];
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

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string clickedItem = treeView1.SelectedNode.Text;
            Clipboard.SetText(clickedItem);

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string clickedItem = treeView1.SelectedNode.Text;
            if (clickedItem.Contains("Node"))
            {
                if (Form1.nodeList[clickedItem].used == true)
                {
                    MessageBox.Show("Node is Used");
                }
                else
                {
                    Form1.nodeList.Remove(clickedItem);
                }
            }
            else if (clickedItem.Contains("Material"))
            {
                if (Form1.materialList[clickedItem].used == true)
                {
                    MessageBox.Show("Material is Used");
                }
                else
                {
                    Form1.materialList.Remove(clickedItem);
                }

            }
            else if (clickedItem.Contains("Section"))
            {
                if(Form1.sectionList[clickedItem].used == true)
                {
                    MessageBox.Show("Section is Used");
                }
                else
                {
                    Form1.sectionList.Remove(clickedItem);
                }
                
            }
            else
            {
                Form1.memberList.Remove(clickedItem);
            }
            refresh();
        }
    }
}
