using System;
using System.Windows.Forms;

namespace StructuralAnalysisSofwareTemplate
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = "Ready";

            // Panel is created temporarily

            // temporary objects
            Node Node1 = new Node();
            Database.NodeList.Add(Node1.Name, Node1);

            Node Node2 = new Node();
            Database.NodeList.Add(Node2.Name, Node2);

            Member member1 = new Member();
            Database.MemberList.Add(member1.Name, member1);

            Material material1 = new Material();
            Database.MaterialList.Add(material1.Name, material1);

            Section section1 = new Section();
            Database.SectionList.Add(section1.Name, section1);

            member1.SetAll(Node1, Node2, material1, section1);
            // creates navigator form
            createNavigator();
        }

        private void createSpreadSheet(Type givenClass)
        {
            // adds spread sheet in to form1.panel1
            DataModel dataModel;

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
                dataModel = new MaterialDataModel(Database.MemberList);
            }
            else
            {
                dataModel = new SectionDataModel(Database.SectionList);
            }

            SpreadSheet spreadSheet = new SpreadSheet(dataModel);
            spreadSheet.TopLevel = false;
            panel1.Controls.Add(spreadSheet);
            spreadSheet.Show();
            spreadSheet.Dock = DockStyle.Right;

            spreadSheet.refresh();
            Database.spreadList.Add(spreadSheet);
        }

        private void createNavigator()
        {
            // creates new treeview form
            var navigator = new TreeView();
            navigator.TopLevel = false;
            panel1.Controls.Add(navigator);
            navigator.Show();
            navigator.Dock = DockStyle.Left; // docks the form in to panel (temporary)
            navigator.refresh();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            createNavigator();
        }

        private void navigatorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            createNavigator();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            createSpreadSheet(typeof(Node));
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            createSpreadSheet(typeof(Member));
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            createSpreadSheet(typeof(Material));
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            createSpreadSheet(typeof(Section));
        }

        private void nodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            createSpreadSheet(typeof(Node));
        }

        private void membersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            createSpreadSheet(typeof(Member));
        }

        private void materialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            createSpreadSheet(typeof(Material));
        }

        private void sectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            createSpreadSheet(typeof(Section));
        }
    }
}