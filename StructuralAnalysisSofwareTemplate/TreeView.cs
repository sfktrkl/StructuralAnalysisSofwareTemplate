using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace StructuralAnalysisSofwareTemplate
{
    public partial class TreeView : StructuralAnalysisSofwareTemplate.DockableForm
    {

        public TreeView()
        {
            InitializeComponent();
            refresh();

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
            for (int i = 0; i < 4; i++)
            {
                foreach (var obj in Form1.tempDatabase.get(i))
                {
                    treeView1.Nodes[i].Nodes.Add(obj.Value.Name.ToString());
                }
            }
            expand();

        }

        private void addTabs(string tabName)
        {
            // sets the spread sheet name and calls the function
            Type item = Form1.tempDatabase.returnType(tabName);
            createSpreadSheet(tabName, item);

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
            Form1.spreadList.Add(spreadSheet);

        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refresh();
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
            Type itemType = Form1.tempDatabase.returnType(clickedItem);

            if (Form1.tempDatabase.get(itemType)[clickedItem].used == true)
            {
                MessageBox.Show("Object is Used");
            }
            else
            {
                Form1.tempDatabase.get(itemType)[clickedItem].delete();
                Form1.tempDatabase.get(itemType).Remove(clickedItem);
                
                foreach (var form in Form1.spreadList)
                {
                        form.refresh(form.loadType);
                }
            }

            refresh();
        }
    }
}
