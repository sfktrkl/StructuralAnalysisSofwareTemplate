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
    public partial class SpreadSheet : Form
    {
        // to not trigger the events it is set to false initially
        private bool loaded = false;

        public SpreadSheet()
        {
            InitializeComponent();

        }

        public void refresh(Type givenClass)
        {
            // clears the spreadsheet
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            // binding flags to take private and public field names
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;


            // adds each field name of a class as a column name
            foreach (FieldInfo field in givenClass.GetFields(bindingFlags))
            {
                dataGridView1.Columns.Add(field.Name, field.Name);
            }

            // checks whether objects exist or not and gets values to enter spreadsheet
            if (this.Text == "Nodes")
            {
                if (Form1.nodeList.Count != 0)
                {

                    int col = 0, row = 0;

                    foreach (var item in Form1.nodeList)
                    {
                        dataGridView1.Rows.Add();

                        foreach (var item2 in item.Value.GetAll())
                        {
                            dataGridView1.CurrentCell = dataGridView1[col, row];
                            dataGridView1.CurrentCell.Value = item2;
                            col++;
                        }
                        col = 0;
                        row++;

                    }
                }
            }
            else if (this.Text == "Members")
            {
                if (Form1.memberList.Count != 0)
                {

                    int col = 0, row = 0;

                    foreach (var item in Form1.memberList)
                    {
                        dataGridView1.Rows.Add();

                        foreach (var item2 in item.Value.GetAll())
                        {
                            dataGridView1.CurrentCell = dataGridView1[col, row];
                            dataGridView1.CurrentCell.Value = item2;
                            col++;
                        }
                        col = 0;
                        row++;

                    }
                }
            }
            else if (this.Text == "Materials")
            {
                if (Form1.materialList.Count != 0)
                {

                    int col = 0, row = 0;

                    foreach (var item in Form1.materialList)
                    {
                        dataGridView1.Rows.Add();

                        foreach (var item2 in item.Value.GetAll())
                        {
                            dataGridView1.CurrentCell = dataGridView1[col, row];
                            dataGridView1.CurrentCell.Value = item2;
                            col++;
                        }
                        col = 0;
                        row++;

                    }
                }
            }
            else
            {
                if (Form1.sectionList.Count != 0)
                {

                    int col = 0, row = 0;

                    foreach (var item in Form1.sectionList)
                    {
                        dataGridView1.Rows.Add();

                        foreach (var item2 in item.Value.GetAll())
                        {
                            dataGridView1.CurrentCell = dataGridView1[col, row];
                            dataGridView1.CurrentCell.Value = item2;
                            col++;
                        }
                        col = 0;
                        row++;

                    }
                }
            }

            // makes Name columns readonly
            dataGridView1.Columns[0].ReadOnly = true;
            // hides unnecessary columns (fields)
            if (this.Text == "Nodes" || this.Text == "Materials" || this.Text == "Sections")
            {
                var index = dataGridView1.Columns["isUsed"].Index;
                dataGridView1.Columns[index].Visible = false;
                dataGridView1.Columns[index+1].Visible = false;
            }
            // makes loaded field true
            loaded = true;
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (loaded == true)
            {
                // adds new node with default constructor
                loaded = false;
                Node node = new Node();
                Form1.nodeList.Add(node.Node_Name,node);
                // refresh the treeview
                TreeView navigator = (TreeView)Application.OpenForms[0];
                navigator.refresh();
                // refresh the spreadsheet
                refresh(typeof(Node));
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (loaded == true)
            {
                var index = e.RowIndex;

                if (this.Text == "Nodes")
                {
                    // setting node properties
                    Form1.nodeList[dataGridView1.Rows[index].Cells[0].Value.ToString()].SetAll(
                        Convert.ToDouble(dataGridView1.Rows[index].Cells[1].Value.ToString()),
                        Convert.ToDouble(dataGridView1.Rows[index].Cells[2].Value.ToString()),
                        Convert.ToBoolean(dataGridView1.Rows[index].Cells[3].Value.ToString()),
                        Convert.ToBoolean(dataGridView1.Rows[index].Cells[4].Value.ToString()),
                        Convert.ToBoolean(dataGridView1.Rows[index].Cells[5].Value.ToString()),
                        Convert.ToDouble(dataGridView1.Rows[index].Cells[6].Value.ToString()),
                        Convert.ToDouble(dataGridView1.Rows[index].Cells[7].Value.ToString()),
                        Convert.ToDouble(dataGridView1.Rows[index].Cells[8].Value.ToString())
                    );
                }
                else if (this.Text == "Materials")
                {
                    Form1.materialList[dataGridView1.Rows[index].Cells[0].Value.ToString()].SetAll(
                        Convert.ToDouble(dataGridView1.Rows[index].Cells[1].Value.ToString()),
                        Convert.ToDouble(dataGridView1.Rows[index].Cells[2].Value.ToString())
                    );
                }
                else if (this.Text == "Sections")
                {
                    Form1.sectionList[dataGridView1.Rows[index].Cells[0].Value.ToString()].SetAll(
                        Convert.ToDouble(dataGridView1.Rows[index].Cells[1].Value.ToString()),
                        Convert.ToDouble(dataGridView1.Rows[index].Cells[2].Value.ToString())
                    );
                }
                else
                {
                    // checks previous data if objects are used by member
                    List<string> previousData = Form1.memberList[dataGridView1.Rows[index].Cells[0].Value.ToString()].GetAll();
                    for (int i = 1; i < 5; i++)
                    {
                        if (previousData[i] != "NULL")
                        {
                            try
                            {
                                
                                try
                                {
                                    
                                    try
                                    {
                                        Form1.sectionList[previousData[i]].usedBy(previousData[0], Form1.memberList[dataGridView1.Rows[index].Cells[0].Value.ToString()], false);
                                    }
                                    catch { }
                                    Form1.materialList[previousData[i]].usedBy(previousData[0], Form1.memberList[dataGridView1.Rows[index].Cells[0].Value.ToString()], false);
                                }
                                catch { }
                                Form1.nodeList[previousData[i]].usedBy(previousData[0], Form1.memberList[dataGridView1.Rows[index].Cells[0].Value.ToString()], false);
                            }
                            catch { }
                        }
                    }

                    // creates temporary objects
                    Node node1;
                    Node node2;
                    Material material;
                    Section section;

                    // tries whether object is exist or not
                    try
                    {
                        node1 = Form1.nodeList[dataGridView1.Rows[index].Cells[1].Value.ToString()];
                    }
                    catch
                    {
                        node1 = null;
                        dataGridView1.Rows[index].Cells[1].Value = "NULL";
                    }

                    try
                    {
                        node2 = Form1.nodeList[dataGridView1.Rows[index].Cells[2].Value.ToString()];
                    }
                    catch
                    {
                        node2 = null;
                        dataGridView1.Rows[index].Cells[2].Value = "NULL";
                    }

                    try
                    {
                        material = Form1.materialList[dataGridView1.Rows[index].Cells[3].Value.ToString()];
                    }
                    catch
                    {
                        material = null;
                        dataGridView1.Rows[index].Cells[3].Value = "NULL";
                    }
                    try
                    {
                        section = Form1.sectionList[dataGridView1.Rows[index].Cells[4].Value.ToString()];
                    }
                    catch
                    {
                        section = null;
                        dataGridView1.Rows[index].Cells[4].Value = "NULL";
                    }

                    // sets objects to Member
                    Form1.memberList[dataGridView1.Rows[index].Cells[0].Value.ToString()].SetAll(node1, node2, material, section);

                    
                }

            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string previousValue = dataGridView1.CurrentCell.Value.ToString();

            try
            {
                // sets the clipboard into cell
                string itemName = Clipboard.GetText().ToString();
                dataGridView1.CurrentCell.Value = itemName;
            }
            catch
            {
                dataGridView1.CurrentCell.Value = previousValue;
                MessageBox.Show("Wrong input type!");
            }

        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                // selects the cell in datagridview if cell is right clicked
                if (e.ColumnIndex != -1 && e.RowIndex != -1 && e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    DataGridViewCell c = (sender as DataGridView)[e.ColumnIndex, e.RowIndex];
                    if (!c.Selected)
                    {
                        c.DataGridView.ClearSelection();
                        c.DataGridView.CurrentCell = c;
                        c.Selected = true;

                    }
                }
                // shows the contextmenustrip in cursor position
                contextMenuStrip2.Show(Cursor.Position);
            }
            
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string clickedItem = dataGridView1.CurrentCell.Value.ToString(); ;
            Clipboard.SetText(clickedItem);
        }
    }
}
