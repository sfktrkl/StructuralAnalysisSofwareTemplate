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

        public SpreadSheet(MainForm mainForm, DataModel dataModel)
        {
            InitializeComponent();
            this.dataModel = dataModel;

            this.mainForm = mainForm;
        }

        private MainForm mainForm;

        public void RefreshData()
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

            int col = 0, row = 0;

            // checks whether objects exist or not and gets values to enter spreadsheet
            foreach (var components in dataModel.Components)
            {
                dataGridView1.Rows.Add();

                foreach (var componentData in dataModel.GetRowData(components.Value.Name))
                {
                    dataGridView1.CurrentCell = dataGridView1[col, row];
                    dataGridView1.CurrentCell.Value = componentData;
                    col++;
                }
                col = 0;
                row++;
            }

            dataGridView1.CurrentCell = dataGridView1[1, row-1];
            dataGridView1.Columns[0].ReadOnly = true;
            loaded = true;
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (loaded == true)
            {
                loaded = false;

                // adds new components with default constructors
                this.dataModel.CreateComponent();
                this.RefreshData();

                // refresh the treeview
                this.mainForm.RefreshNavigators();
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            dataModel.SetValueOf(e.RowIndex, e.ColumnIndex, newString);

            if (loaded == true)
            {
                var index = e.RowIndex;


                var componentData = new List<object>();

                foreach (DataGridViewCell cell in dataGridView1.Rows[index].Cells)
                {
                    componentData.Add(cell.Value);
                }

                dataModel.SetComponentFromData(componentData);
                var newRowData = dataModel.GetRowData(dataGridView1.Rows[index].Cells[0].Value.ToString());
                dataGridView1.Rows[index].Cells[e.ColumnIndex].Value = newRowData[e.ColumnIndex];
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
            this.mainForm.RefreshSpreadsheets();
        }
    }
}