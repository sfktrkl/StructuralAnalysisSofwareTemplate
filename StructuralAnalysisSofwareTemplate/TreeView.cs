using System;
using System.Windows.Forms;

namespace StructuralAnalysisSofwareTemplate
{
    public partial class TreeView : StructuralAnalysisSofwareTemplate.DockableForm
    {
        public TreeView()
        {
            InitializeComponent();
        }

        public void refresh()
        {
            // clears the treeview and adds base nodes
            treeView1.Nodes.Clear();

            var parentNode = treeView1.Nodes.Add("Nodes");
            foreach (var obj in Database.NodeList)
            {
                var childNode = parentNode.Nodes.Add(obj.Value.Name);
                childNode.Tag = obj.Value;
            }

            parentNode = treeView1.Nodes.Add("Members");
            foreach (var obj in Database.MemberList)
            {
                var childNode = parentNode.Nodes.Add(obj.Value.Name);
                childNode.Tag = obj.Value;
            }

            parentNode = treeView1.Nodes.Add("Materials");
            foreach (var obj in Database.MaterialList)
            {
                var childNode = parentNode.Nodes.Add(obj.Value.Name);
                childNode.Tag = obj.Value;
            }

            parentNode = treeView1.Nodes.Add("Sections");
            foreach (var obj in Database.SectionList)
            {
                var childNode = parentNode.Nodes.Add(obj.Value.Name);
                childNode.Tag = obj.Value;
            }

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
            // sets the spread sheet name and calls the function
            createSpreadSheet(e.Node.Tag.GetType());
        }

        private void createSpreadSheet(Type givenClass)
        {
            DataModel dataModel;

            // adds spread sheet in to form1.panel1
            if (givenClass == typeof(Node))
            {
                dataModel = new NodeDataModel(Database.NodeList);
            }
            else if (givenClass == typeof(Member))
            {
                dataModel = new MemberDataModel(Database.MemberList);
            }
            else if (givenClass == typeof(Material))
            {
                dataModel = new MaterialDataModel(Database.MaterialList);
            }
            else
            {
                dataModel = new SectionDataModel(Database.SectionList);
            }

            SpreadSheet spreadSheet = new SpreadSheet(dataModel);
            spreadSheet.TopLevel = false;
            MainForm form1 = (MainForm)Application.OpenForms["MainForm"];
            Panel panel1 = (Panel)form1.Controls["panel1"];
            panel1.Controls.Add(spreadSheet);
            spreadSheet.Show();
            spreadSheet.Dock = DockStyle.Right;

            spreadSheet.refresh();
            Database.spreadList.Add(spreadSheet);
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
            var obj = (Component)treeView1.SelectedNode.Tag;

            if (obj.UsedBy.Count > 0)
            {
                MessageBox.Show("Object is Used");
            }
            else
            {
                obj.Delete();
                Database.refreshSpreadList();
            }

            refresh();
        }
    }
}