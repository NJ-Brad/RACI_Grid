using RACI_Grid;

namespace RACI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        RaciDocument doc = new();

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            doc = new RaciDocument();
            doc["Take out the trash", "Mom"] = "A";
            doc["Fix dinner", "Dad"] = "R,A";
            doc["Laundry", "Kids"] = "R,A";
            doc["Yard mowing", "Lawn Company"] = "R";

            raciGrid1.Document = doc;
        }

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileHandler1.DataType = typeof(RaciDocument);

            // Passing in a data object is used to derive the data type, if .DataType is not set
            //fileHandler1.Data = raciGrid31.Document;

            if (fileHandler1.Load())
            {
                raciGrid1.Document = fileHandler1.Data as RaciDocument;
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileHandler1.Data = raciGrid1.Document;
            fileHandler1.Save();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileHandler1.FileName = string.Empty;
            fileHandler1.Data = raciGrid1.Document;
            fileHandler1.Save();
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            doc = new RaciDocument();

            raciGrid1.Document = doc;
        }
    }
}
