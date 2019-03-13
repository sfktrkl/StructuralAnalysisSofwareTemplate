using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StructuralAnalysisSofwareTemplate
{
    public partial class SpreadSheet : Form
    {
        public SpreadSheet()
        {
            InitializeComponent();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            // trial for adding new nodes
            Node node = new Node();
            Form1.nodeList.Add(node);
            TreeView navigator = (TreeView)Application.OpenForms[0];
            navigator.refresh();
        }
    }
}
