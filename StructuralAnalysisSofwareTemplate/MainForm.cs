using System;
using System.Collections.Generic;
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

            // creates the first navigator
            
        }

        private List<SpreadSheet> spreadsheets = new List<SpreadSheet>();
        private List<TreeView> navigators = new List<TreeView>();

        public void RefreshSpreadsheets()
        {
            foreach (var spreadsheet in this.spreadsheets) spreadsheet.RefreshData();
        }

        public void RefreshNavigators()
        {
            foreach (var navigator in this.navigators) navigator.RefreshData();
        }

        public void CreateSpreadSheet(Type givenClass)
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

            SpreadSheet spreadSheet = new SpreadSheet(this, dataModel);
            spreadSheet.TopLevel = false;
            panel1.Controls.Add(spreadSheet);
            spreadSheet.Show();
            spreadSheet.Dock = DockStyle.Right;

            spreadSheet.RefreshData();
            this.spreadsheets.Add(spreadSheet);
        }

        private void CreateNavigator()
        {
            var navigator = new TreeView(this);
            navigator.TopLevel = false;
            panel1.Controls.Add(navigator);
            navigator.Show();
            navigator.Dock = DockStyle.Left; // docks the form in to panel (temporary)
            navigator.RefreshData();

            this.navigators.Add(navigator);
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            this.CreateNavigator();
        }

        private void navigatorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.CreateNavigator();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.CreateSpreadSheet(typeof(Node));
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.CreateSpreadSheet(typeof(Member));
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.CreateSpreadSheet(typeof(Material));
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.CreateSpreadSheet(typeof(Section));
        }

        private void nodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CreateSpreadSheet(typeof(Node));
        }

        private void membersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CreateSpreadSheet(typeof(Member));
        }

        private void materialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CreateSpreadSheet(typeof(Material));
        }

        private void sectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CreateSpreadSheet(typeof(Section));
        }
    }
}