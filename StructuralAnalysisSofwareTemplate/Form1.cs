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
    public partial class Form1 : Form
    {

        static List<Node> nodeList = new List<Node>();

        public Form1()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = "Ready";
            tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            

            treeView1.Nodes.Add("Nodes");
            treeView1.Nodes.Add("Members");
            treeView1.Nodes.Add("Materials");
            treeView1.Nodes.Add("Sections");

            foreach (Node obj in nodeList)
            {
                treeView1.Nodes[0].Nodes.Add(obj.Node_Name.ToString());
            }

        }


        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            
            string clickedItem = e.Node.Text;
            addTabs(clickedItem);

        }

        private void addTabs(string tabName)
        {
            string newName;
            if (tabName.Contains("Node"))
            {
                newName = "Nodes";
            }
            else if (tabName.Contains("Member"))
            {
                newName = "Members";
            }
            else
            {
                newName = "Empty";
            }

            TabPage tabPage = new TabPage(newName);
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;

            DataGridView spreadSheet = new DataGridView();
            tabPage.Controls.Add(spreadSheet);
            spreadSheet.Dock = System.Windows.Forms.DockStyle.Fill;

            BindingFlags bindingFlags = BindingFlags.Public |
                            BindingFlags.NonPublic |
                            BindingFlags.Instance;




            if (newName == "Nodes")
            {

                foreach (FieldInfo field in typeof(Node).GetFields(bindingFlags))
                {
                    spreadSheet.Columns.Add(field.Name, field.Name);
                }

                if (nodeList.Count != 0)
                {

                    int col = 0, row = 0;

                    foreach (var item in nodeList)
                    {
                        spreadSheet.Rows.Add();

                        foreach (var item2 in item.GetAll())
                        {
                            spreadSheet.CurrentCell = spreadSheet[col, row];
                            spreadSheet.CurrentCell.Value = item2;
                            col++;
                        }
                        col = 0;
                        row++;

                    }
                }
                

            }
            else if (newName == "Member")
            {

            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                treeView1.SelectedNode = e.Node;
                contextMenuStrip1.Show(Cursor.Position);
            }
        }
    }

}
