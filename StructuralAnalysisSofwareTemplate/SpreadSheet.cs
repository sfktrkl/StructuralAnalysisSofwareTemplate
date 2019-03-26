using System;
using System.Drawing;
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

            // checks whether objects exist or not and gets values to enter spreadsheet
            foreach (var components in dataModel.Components)
            {
                var newRow = new DataGridViewRow();
                newRow.Tag = components.Value.UniqueName;
                dataGridView1.Rows.Add(newRow);

                var newRowData = dataModel.GetRowData(components.Value.UniqueName);

                for (int i = 0; i < newRowData.Item1.Count; i++)
                {
                    dataGridView1.CurrentCell = dataGridView1[i, dataGridView1.Rows.Count - 2];
                    dataGridView1.CurrentCell.Value = newRowData.Item1[i];
                    if (newRowData.Item2[i])
                    {
                        dataGridView1.CurrentCell.ReadOnly = true;
                        dataGridView1.CurrentCell.Style.BackColor = Color.Gray;
                    }
                }

                dataGridView1.CurrentCell = dataGridView1[1, dataGridView1.Rows.Count - 1];
            }

            loaded = true;
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (loaded == true)
            {
                loaded = false;

                // adds new components with default constructors
                this.dataModel.CreateComponent();
                UiManager.RefreshSpreadsheets(this);

                // refresh the navigators
                UiManager.RefreshNavigators();
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (loaded == true)
            {
                loaded = false;
                // gets string from cell as "data"
                var data = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                // sets cell data to component
                dataModel.SetCellToComponent(dataGridView1.Rows[e.RowIndex].Tag.ToString(), data, dataGridView1.Columns[e.ColumnIndex].Name);

                // gets new row data
                var newRowData = dataModel.GetRowData(dataGridView1.Rows[e.RowIndex].Tag.ToString());

                for (int i = 1; i < dataGridView1.Rows[e.RowIndex].Cells.Count; i++)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[i].Value = newRowData.Item1[i];
                }

                if (e.ColumnIndex == 0) UiManager.RefreshNavigators();
                UiManager.RefreshSpreadsheets();

                dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];
                loaded = true;
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
            UiManager.RefreshSpreadsheets(this);
        }
    }
}