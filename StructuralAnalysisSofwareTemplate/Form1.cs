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
        public static Dictionary<string,Node> nodeList = new Dictionary<string,Node>();
        public static Dictionary<string,Member> memberList = new Dictionary<string,Member>();
        public static Dictionary<string,Material> materialList = new Dictionary<string,Material>();
        public static Dictionary<string,Section> sectionList = new Dictionary<string,Section>();

        public Form1()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = "Ready";

            // Panel is created temporarily

            // temporary objects
            Node node1 = new Node();
            Node node2 = new Node();
            nodeList.Add(node1.Node_Name,node1);
            nodeList.Add(node2.Node_Name,node2);

            Member member1 = new Member();
            memberList.Add(member1.member_Name,member1);

            Material material1 = new Material();
            materialList.Add(material1.material_Name,material1);

            Section section1 = new Section();
            sectionList.Add(section1.section_Name,section1);

            member1.SetAll(node1, node2, material1, section1);
            // creates navigator form
            createNavigator();

        }


        private void createNavigator()
        {
            // creates new treeview form
            TreeView navigator = new TreeView();
            navigator.TopLevel = false;
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
