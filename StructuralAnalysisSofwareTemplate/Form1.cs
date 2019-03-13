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
        // public lists to store objects
        public static List<Node> nodeList = new List<Node>();
        public static List<Member> memberList = new List<Member>();
        public static List<Material> materialList = new List<Material>();
        public static List<Section> sectionList = new List<Section>();

        public Form1()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = "Ready";

            // Panel is created temporarily

            // temporary objects
            Node node1 = new Node();
            Node node2 = new Node();
            nodeList.Add(node1);
            nodeList.Add(node2);

            Member member1 = new Member();
            memberList.Add(member1);

            Material material1 = new Material();
            materialList.Add(material1);

            Section section1 = new Section();
            sectionList.Add(section1);

            // creates navigator form
            createNavigator();

        }


        private void createNavigator()
        {
            // creates new treeview form
            TreeView navigator = new TreeView();
            navigator.MdiParent = this;
            panel1.Controls.Add(navigator);
            navigator.Show();
            navigator.Dock = System.Windows.Forms.DockStyle.Left; // docks the form in to panel (temporary)
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
    }

}
