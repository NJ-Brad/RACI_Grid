using System.Data;
using System.Threading.Channels;
using System.Windows.Forms;

namespace RACI_Grid
{
    public partial class RaciGrid : UserControl
    {
        public RaciGrid()
        {
            InitializeComponent();
        }

        RaciDocument doc = new();

        public RaciDocument Document
        {
            get
            {
                ReadValues();
                return doc;
            }
            set
            {
                doc = value;
                CreateGrid();
            }
        }

        private void CreateGrid()
        {
            // https://stackoverflow.com/questions/998066/linq-distinct-values
            //var query = doc.Elements("whatever")
            //   .DistinctBy(e => ((int)e.Attribute("id"), (int)e.Attribute("cat")));

            addPersonOrGroupExists = false;
            addWorkItemExists = false;

            tableLayoutPanel1.SuspendLayout();

            //if (tableLayoutPanel1.ColumnCount > 1)
            {
                // remove all of the rows, below the header
                while (tableLayoutPanel1.RowCount > 1)
                {
                    for (int i = tableLayoutPanel1.ColumnCount - 1; i > -1; i--)
                    {
                        Control c = (Control)tableLayoutPanel1.GetControlFromPosition(i, tableLayoutPanel1.RowCount - 1);
                        tableLayoutPanel1.Controls.Remove(c);
                    }

                    tableLayoutPanel1.RowStyles.RemoveAt(tableLayoutPanel1.RowCount - 1);
                    tableLayoutPanel1.RowCount -= 1;
                }

                // clean up the headers
                while (tableLayoutPanel1.ColumnCount > 1)
                {
                    Control c = (Control)tableLayoutPanel1.GetControlFromPosition(tableLayoutPanel1.ColumnCount - 1, 0);
                    tableLayoutPanel1.Controls.Remove(c);

                    tableLayoutPanel1.ColumnStyles.RemoveAt(tableLayoutPanel1.ColumnCount - 1);
                    tableLayoutPanel1.ColumnCount -= 1;
                }
            }

            var query = doc.RaciData.DistinctBy(e => ((string)e.ActivityName)).OrderBy(e => ((string)e.ActivityName));
            foreach (var item in query)
            {
                AddWorkItem(item.ActivityName);
            }

            var query2 = doc.RaciData.DistinctBy(e => ((string)e.Person)).OrderBy(e => ((string)e.Person));
            foreach (var item in query2)
            {
                AddPersonOrGroup(item.Person);
            }

            foreach (RaciDataItem rdi in doc.RaciData)
            {
                SetValue(rdi.ActivityName, rdi.Person, rdi.RaciValue);
            }

            AddAddWorkItem("Add");
            AddAddPersonOrGroup("Add");
            tableLayoutPanel1.ResumeLayout();
        }

        private bool addPersonOrGroupExists = false;
        private bool addWorkItemExists = false;

        private void AddPersonOrGroup(string text)
        {
            int numColumns = tableLayoutPanel1.ColumnCount;
            // add a new row
            tableLayoutPanel1.ColumnCount++;
            numColumns = tableLayoutPanel1.ColumnCount;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 125));

            //            RaciGridColumnHeader newHeader = new();
            RaciGridHeader headerCell = new()
            {
                Text = text,
                Visible = true
            };
            headerCell.Changed += HeaderChanged;
            headerCell.DeleteRequested += DeleteRequested;

            tableLayoutPanel1.Controls.Add(headerCell, numColumns - 1, 0);

            // add the RACI items for this new column
            int numRows = tableLayoutPanel1.RowCount;

            if (addWorkItemExists)
                numRows--;

            for (int i = 1; i < numRows; i++)
            {
                RaciItem newItem = new()
                {
                    Visible = true
                };
                newItem.ItemClick += Clicked;

                tableLayoutPanel1.Controls.Add(newItem, numColumns - 1, i);
            }
        }

        private void AddAddPersonOrGroup(string text)
        {
            int numColumns = tableLayoutPanel1.ColumnCount;
            // add a new row
            tableLayoutPanel1.ColumnCount++;
            numColumns = tableLayoutPanel1.ColumnCount;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 125));

            //            RaciGridColumnHeader newHeader = new();
            RaciGridHeaderButton headerCell = new()
            {
                Text = text,
                Visible = true
            };
            headerCell.AddRequested += AddRequested;

            tableLayoutPanel1.Controls.Add(headerCell, numColumns - 1, 0);

            addPersonOrGroupExists = true;
        }


        private void AddWorkItem(string text)
        {
            // https://social.msdn.microsoft.com/Forums/en-US/e20ceaed-c873-4002-9c0a-c3591a4bbb59/add-rows-to-a-tablelayoutpanel-at-runtime?forum=winforms
            int numRows = tableLayoutPanel1.RowCount;
            // add a new row
            tableLayoutPanel1.RowCount++;
            numRows = tableLayoutPanel1.RowCount;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));

            RaciGridHeader headerCell = new()
            {
                Text = text,
                Visible = true
            };
            headerCell.Changed += HeaderChanged;
            headerCell.DeleteRequested += DeleteRequested;

            tableLayoutPanel1.Controls.Add(headerCell, 0, numRows - 1);

            // add the RACI items for this new row
            int numColumns = tableLayoutPanel1.ColumnCount;

            if (addPersonOrGroupExists)
                numColumns--;

            for (int i = 1; i < numColumns; i++)
            {
                RaciItem newItem = new()
                {
                    Visible = true
                };
                newItem.ItemClick += Clicked;

                tableLayoutPanel1.Controls.Add(newItem, i, numRows - 1);
            }
        }

        private void AddAddWorkItem(string text)
        {
            // https://social.msdn.microsoft.com/Forums/en-US/e20ceaed-c873-4002-9c0a-c3591a4bbb59/add-rows-to-a-tablelayoutpanel-at-runtime?forum=winforms
            int numRows = tableLayoutPanel1.RowCount;
            // add a new row
            tableLayoutPanel1.RowCount++;
            numRows = tableLayoutPanel1.RowCount;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));

            RaciGridHeaderButton headerCell = new()
            {
                Text = text,
                Visible = true
            };
            headerCell.AddRequested += AddRequested;

            tableLayoutPanel1.Controls.Add(headerCell, 0, numRows - 1);
            addWorkItemExists = true;
        }

        private void SetValue(string activityName, string person, string value)
        {
            TableLayoutPanelCellPosition pos = GetPosition(activityName, person);
            if ((pos.Row != 0) && (pos.Column != 0))
            {
                RaciItem c = (RaciItem)tableLayoutPanel1.GetControlFromPosition(pos.Column, pos.Row);
                c.Value = value;
            }
        }

        private void ReadValues()
        {
            doc.RaciData.Clear();

            // for each activity
            for (int activityNum = 1; activityNum < tableLayoutPanel1.RowCount; activityNum++)
            {
                RaciGridHeader activityHdr = (RaciGridHeader)tableLayoutPanel1.GetControlFromPosition(0, activityNum);

                // find the person
                for (int personNum = 1; personNum < tableLayoutPanel1.ColumnCount; personNum++)
                {
                    RaciGridHeader personHdr = (RaciGridHeader)tableLayoutPanel1.GetControlFromPosition(personNum, 0);

                    // get the value
                    RaciItem rItem = (RaciItem)tableLayoutPanel1.GetControlFromPosition(personNum, activityNum);

                    // store it
                    doc[activityHdr.Text, personHdr.Text] = rItem.Value;
                }

            }
        }

        private TableLayoutPanelCellPosition GetPosition(string activityName, string person)
        {
            TableLayoutPanelCellPosition rtnVal = new();

            // find the activity
            for (int i = 1; i < tableLayoutPanel1.RowCount; i++)
            {
                Control ctrl = tableLayoutPanel1.GetControlFromPosition(0, i);

                if (ctrl is RaciGridHeader)
                {
                    RaciGridHeader c = (RaciGridHeader)ctrl;
                    if (c.Text.Equals(activityName, StringComparison.OrdinalIgnoreCase))
                    {
                        rtnVal.Row = i;
                        break;
                    }
                }
            }

            // find the person
            for (int i = 1; i < tableLayoutPanel1.ColumnCount; i++)
            {
                Control ctrl = tableLayoutPanel1.GetControlFromPosition(i, 0);

                if (ctrl is RaciGridHeader)
                {
                    RaciGridHeader c = (RaciGridHeader)ctrl;
                    if (c.Text.Equals(person, StringComparison.OrdinalIgnoreCase))
                    {
                        rtnVal.Column = i;
                        break;
                    }
                }
            }

            return rtnVal;
        }

        private void Clicked(object? sender, EventArgs e)
        {
        }

        private void HeaderChanged(object? sender, EventArgs e)
        { }
        private void AddRequested(object? sender, EventArgs e)
        {
            tableLayoutPanel1.SuspendLayout();

            TableLayoutPanelCellPosition pos = tableLayoutPanel1.GetCellPosition((Control)sender);

            if (pos.Row == 0)
            {
                // add a column
                DeleteColumn(pos.Column);
                AddPersonOrGroup("New");
                AddAddPersonOrGroup("Add");
            }

            if (pos.Column == 0)
            {
                // add a column
                DeleteRow(pos.Row);
                AddWorkItem("New");
                AddAddWorkItem("Add");
            }
            tableLayoutPanel1.ResumeLayout();
        }

        private void DeleteColumn(int columnNumber)
        {
            // delete a column

            for (int i = tableLayoutPanel1.RowCount - 1; i > -1; i--)
            {
                Control c = (Control)tableLayoutPanel1.GetControlFromPosition(columnNumber, i);
                tableLayoutPanel1.Controls.Remove(c);
            }

            // for each of the columns to the right
            for (int col = columnNumber + 1; col < tableLayoutPanel1.ColumnCount; col++)
            {
                // for each of the rows
                for (int row = 0; row < tableLayoutPanel1.RowCount; row++)
                {
                    // change the column of the control
                    var control = tableLayoutPanel1.GetControlFromPosition(col, row);
                    if (control != null)
                    {
                        tableLayoutPanel1.SetColumn(control, col - 1);
                    }
                }
            }

            // remove the last column
            tableLayoutPanel1.ColumnStyles.RemoveAt(tableLayoutPanel1.ColumnCount - 1);
            tableLayoutPanel1.ColumnCount -= 1;
        }

        private void DeleteRow(int rowNumber)
        {
            for (int i = tableLayoutPanel1.ColumnCount - 1; i > -1; i--)
            {
                Control c = (Control)tableLayoutPanel1.GetControlFromPosition(i, rowNumber);
                tableLayoutPanel1.Controls.Remove(c);
            }

            // for each of the rows below
            for (int row = rowNumber + 1; row < tableLayoutPanel1.RowCount; row++)
            {
                //    // for each of the columns
                for (int col = 0; col < tableLayoutPanel1.ColumnCount; col++)
                {
                    // change the row of the control
                    var control = tableLayoutPanel1.GetControlFromPosition(col, row);
                    if (control != null)
                    {
                        tableLayoutPanel1.SetRow(control, row - 1);
                    }
                }
            }

            // remove the last row
            tableLayoutPanel1.RowStyles.RemoveAt(tableLayoutPanel1.RowCount - 1);
            tableLayoutPanel1.RowCount -= 1;
        }

        private void DeleteRequested(object? sender, EventArgs e)
        {
            TableLayoutPanelCellPosition pos = tableLayoutPanel1.GetCellPosition((Control)sender);

            if (pos.Row == 0)
            {
                // delete a column
                DeleteColumn(pos.Column);
            }

            if (pos.Column == 0)
            {
                // delete a row
                DeleteRow(pos.Row);
            }
        }

        bool busy = false;

        private void TableLayoutPanel1_Click(object sender, EventArgs e)
        {
            Focus();
        }

        public bool Busy
        {
            get
            {
                return busy;
            }
            set
            {
                busy = value;
                if (busy)
                {
                    panel1.Dock = DockStyle.Fill;
                    panel1.Visible = true;

                    panel1.BringToFront();
                }
                else
                {
                    panel1.Visible = false;
                }
            }
        }
    }
}
