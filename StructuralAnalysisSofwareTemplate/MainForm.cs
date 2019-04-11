using System;
using System.Windows.Forms;

namespace StructuralAnalysisSofwareTemplate
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            UiManager.mainForm = this;
            toolStripStatusLabel1.Text = "Ready";
            // Panel is created temporarily

            // temporary objects
            Node node1 = new Node();
            Database.NodeList.Add(node1.UniqueName, node1);

            Node node2 = new Node();
            Database.NodeList.Add(node2.UniqueName, node2);

            Member member1 = new Member();
            Database.MemberList.Add(member1.UniqueName, member1);

            Member member2 = new Member();
            Database.MemberList.Add(member2.UniqueName, member2);

            Material material1 = new Material();
            Database.MaterialList.Add(material1.UniqueName, material1);

            Section section1 = new Section();
            Database.SectionList.Add(section1.UniqueName, section1);

            member1.parameters["First Node"].SetValue(node1.UniqueName);
            member1.parameters["Second Node"].SetValue(node2.UniqueName);
            member1.parameters["Material"].SetValue(material1.UniqueName);
            member1.parameters["Section"].SetValue(section1.UniqueName);

            MultiLine multiline = new MultiLine();
            Database.MultiLineList.Add(multiline.UniqueName, multiline);

            // creates the first navigator
            UiManager.CreateNavigator();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            UiManager.CreateNavigator();
        }

        private void navigatorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UiManager.CreateNavigator();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            UiManager.CreateSpreadSheet(new NodeDataModel(Database.NodeList));
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            UiManager.CreateSpreadSheet(new MemberDataModel(Database.MemberList));
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            UiManager.CreateSpreadSheet(new MaterialDataModel(Database.MaterialList));
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            UiManager.CreateSpreadSheet(new SectionDataModel(Database.SectionList));
        }

        private void nodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UiManager.CreateSpreadSheet(new NodeDataModel(Database.NodeList));
        }

        private void membersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UiManager.CreateSpreadSheet(new MemberDataModel(Database.MemberList));
        }

        private void materialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UiManager.CreateSpreadSheet(new MaterialDataModel(Database.MaterialList));
        }

        private void sectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UiManager.CreateSpreadSheet(new SectionDataModel(Database.SectionList));
        }
    }
}