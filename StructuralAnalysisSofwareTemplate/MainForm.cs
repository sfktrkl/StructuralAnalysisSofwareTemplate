﻿using System;
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
            UiManager.CreateSpreadSheet(typeof(Node));
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            UiManager.CreateSpreadSheet(typeof(Member));
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            UiManager.CreateSpreadSheet(typeof(Material));
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            UiManager.CreateSpreadSheet(typeof(Section));
        }

        private void nodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UiManager.CreateSpreadSheet(typeof(Node));
        }

        private void membersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UiManager.CreateSpreadSheet(typeof(Member));
        }

        private void materialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UiManager.CreateSpreadSheet(typeof(Material));
        }

        private void sectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UiManager.CreateSpreadSheet(typeof(Section));
        }
    }
}