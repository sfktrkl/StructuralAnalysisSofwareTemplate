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

        public void RefreshData()
        {
            // clears the treeview and adds base nodes
            treeView1.Nodes.Clear();

            var parentNode = treeView1.Nodes.Add("Nodes");
            parentNode.Tag = new Node();
            foreach (var obj in Database.NodeList)
            {
                var childNode = parentNode.Nodes.Add(obj.Value.parameters["Node Name"].Display);
                childNode.Tag = obj.Value;
            }

            parentNode = treeView1.Nodes.Add("Members");
            parentNode.Tag = new Member();
            foreach (var obj in Database.MemberList)
            {
                var childNode = parentNode.Nodes.Add(obj.Value.parameters["Member Name"].Display);
                childNode.Tag = obj.Value;
            }

            parentNode = treeView1.Nodes.Add("Materials");
            parentNode.Tag = new Material();
            foreach (var obj in Database.MaterialList)
            {
                var childNode = parentNode.Nodes.Add(obj.Value.parameters["Material Name"].Display);
                childNode.Tag = obj.Value;
            }

            parentNode = treeView1.Nodes.Add("Sections");
            parentNode.Tag = new Section();
            foreach (var obj in Database.SectionList)
            {
                var childNode = parentNode.Nodes.Add(obj.Value.parameters["Section Name"].Display);
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
            UiManager.CreateSpreadSheet(e.Node.Tag.GetType());
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UiManager.RefreshNavigators(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UiManager.CloseNavigator(this);
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
                UiManager.RefreshSpreadsheets();
            }

            UiManager.RefreshNavigators();
        }
    }
}