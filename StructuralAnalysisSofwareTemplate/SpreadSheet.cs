using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace StructuralAnalysisSofwareTemplate
{
    public partial class SpreadSheet : StructuralAnalysisSofwareTemplate.DockableForm
    {
        // to not trigger the events it is set to false initially
        private bool loaded = false;

        public DataModel dataModel;

        public SpreadSheet(DataModel dataModel)
        {
            InitializeComponent();

            this.dataModel = dataModel;
        }

        public void refresh()
        {
            loaded = false;

            // clears the spreadsheet
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            // adds each field name of a class as a column name
            foreach (var name in dataModel.GetColumnNames())
            {
                dataGridView1.Columns.Add(name, name);
            }

            // checks whether objects exist or not and gets values to enter spreadsheet
            int col = 0, row = 0;

            foreach (var component in dataModel.Components)
            {
                dataGridView1.Rows.Add();

                foreach (var item2 in component.Value.GetAll())
                {
                    dataGridView1.CurrentCell = dataGridView1[col, row];
                    dataGridView1.CurrentCell.Value = item2;
                    col++;
                }
                col = 0;
                row++;
            }

            // makes Name columns readonly
            dataGridView1.Columns[0].ReadOnly = true;

            // hides unnecessary columns (fields)
            var index = dataGridView1.Columns["UsedBy"].Index;
            dataGridView1.Columns[index].Visible = false;

            // makes loaded field true
            loaded = true;
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (loaded == true)
            {
                loaded = false;

                // adds new objects with default constructors
                this.dataModel.CreateComponent();

                dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[1];
                // refresh the treeview
                var navigator = (TreeView)Application.OpenForms[0];
                navigator.refresh();
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (loaded == true)
            {
                var index = e.RowIndex;
                if (this.Text.Contains("Node"))
                {
                    bool xFixity = Database.autoComplete(dataGridView1.Rows[index].Cells[3].Value.ToString());
                    bool yFixity = Database.autoComplete(dataGridView1.Rows[index].Cells[4].Value.ToString());
                    bool zFixity = Database.autoComplete(dataGridView1.Rows[index].Cells[5].Value.ToString());

                    // setting node properties
                    Database.get(typeof(Node))[dataGridView1.Rows[index].Cells[0].Value.ToString()].SetAll(
                        Convert.ToDouble(dataGridView1.Rows[index].Cells[1].Value.ToString()),
                        Convert.ToDouble(dataGridView1.Rows[index].Cells[2].Value.ToString()),
                        Convert.ToBoolean(xFixity),
                        Convert.ToBoolean(yFixity),
                        Convert.ToBoolean(zFixity),
                        Convert.ToDouble(dataGridView1.Rows[index].Cells[6].Value.ToString()),
                        Convert.ToDouble(dataGridView1.Rows[index].Cells[7].Value.ToString()),
                        Convert.ToDouble(dataGridView1.Rows[index].Cells[8].Value.ToString())
                    );

                    dataGridView1.Rows[index].Cells[3].Value = xFixity.ToString();
                    dataGridView1.Rows[index].Cells[4].Value = yFixity.ToString();
                    dataGridView1.Rows[index].Cells[5].Value = zFixity.ToString();
                }
                else if (this.Text.Contains("Material"))
                {
                    Database.get(typeof(Material))[dataGridView1.Rows[index].Cells[0].Value.ToString()].SetAll(
                        Convert.ToDouble(dataGridView1.Rows[index].Cells[1].Value.ToString()),
                        Convert.ToDouble(dataGridView1.Rows[index].Cells[2].Value.ToString())
                    );
                }
                else if (this.Text.Contains("Section"))
                {
                    Database.get(typeof(Section))[dataGridView1.Rows[index].Cells[0].Value.ToString()].SetAll(
                        Convert.ToDouble(dataGridView1.Rows[index].Cells[1].Value.ToString()),
                        Convert.ToDouble(dataGridView1.Rows[index].Cells[2].Value.ToString())
                    );
                    // refreshes the area and inertia column in the datagridview
                    if (dataGridView1.Rows[index].Cells[1].Value.ToString() != "0" && dataGridView1.Rows[index].Cells[2].Value.ToString() != "0")
                    {
                        dataGridView1.Rows[index].Cells[3].Value = Database.get(typeof(Section))[dataGridView1.Rows[index].Cells[0].Value.ToString()].GetAll()[3].ToString();
                        dataGridView1.Rows[index].Cells[4].Value = Database.get(typeof(Section))[dataGridView1.Rows[index].Cells[0].Value.ToString()].GetAll()[4].ToString();
                    }
                }
                else
                {
                    // checks previous data if objects are used by member
                    List<string> previousData = Database.get(typeof(Member))[dataGridView1.Rows[index].Cells[0].Value.ToString()].GetAll();
                    for (int i = 1; i < 5; i++)
                    {
                        if (previousData[i] != "NULL")
                        {
                            // deletes this member from all previous related objects' used dictionaries
                            // used property will be added again in the Member.setall function,
                            // if new objects are same with previous ones.

                            // the following line will be a part of dataModel.ReleaseAllFor(index)
                            //if (member.Section != null) member.Section.UsedBy.Remove(member);
                            //if (member.Material != null) member.Material.UsedBy.Remove(member);

                            Database.get(typeof(Section))[previousData[i]].usedBy(previousData[0], Database.get(typeof(Member))[dataGridView1.Rows[index].Cells[0].Value.ToString()], false);
                            Database.get(typeof(Material))[previousData[i]].usedBy(previousData[0], Database.get(typeof(Member))[dataGridView1.Rows[index].Cells[0].Value.ToString()], false);
                            Database.get(typeof(Node))[previousData[i]].usedBy(previousData[0], Database.get(typeof(Member))[dataGridView1.Rows[index].Cells[0].Value.ToString()], false);
                        }
                    }

                    // creates temporary object list
                    List<dynamic> itemList = new List<dynamic>();

                    // loop for every object type
                    for (int i = 1; i < 5; i++)
                    {
                        // assigns k to a value for first two loop
                        // since for two loop should return Node object
                        int k = 0;
                        if (i == 1 || i == 2)
                        {
                            k = 0;
                        }
                        else
                        {
                            k = i - 1;
                        }
                        // detemines return type according to k value (Node, Material or Section)
                        Type itemType = Database.returnType(k);

                        string itemName;
                        try
                        {
                            // auto complete for object names
                            itemName = Database.autoComplete(dataGridView1.Rows[index].Cells[i].Value.ToString(), itemType);
                            // changes cell value
                            dataGridView1.Rows[index].Cells[i].Value = itemName;
                            // adds object to object list
                            itemList.Add(Database.get(itemType)[itemName]);
                        }
                        catch
                        {
                            // if trial to add object from database fails (object name is wrong or object does not exist)
                            // adds nullptr to itemlist as input for member.setall function
                            itemList.Add(null);
                            dataGridView1.Rows[index].Cells[i].Value = "NULL";
                        }
                    }

                    // sets objects to Member
                    Database.get(typeof(Member))[dataGridView1.Rows[index].Cells[0].Value.ToString()].SetAll(itemList[0], itemList[1], itemList[2], itemList[3]);
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
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string clickedItem = dataGridView1.CurrentCell.Value.ToString(); ;
            Clipboard.SetText(clickedItem);
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Database.refreshSpreadList();
        }
    }
}