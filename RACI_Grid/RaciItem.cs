namespace RACI_Grid
{
    public partial class RaciItem : UserControl
    {
        public RaciItem()
        {
            InitializeComponent();
        }

        public EventHandler<EventArgs> ItemClick;

        //public event ListChangedEventHandler ListChanged
        //{
        //    add
        //    {
        //        onListChanged += value;
        //    }
        //    remove
        //    {
        //        onListChanged -= value;
        //    }
        //}

        //private ListChangedEventHandler onListChanged;

        public string Value
        {
            get
            {
                string rtnVal = string.Empty;
                if (label1Selected)
                    rtnVal += "R";
                if (label2Selected)
                {
                    if (rtnVal.Length > 0)
                    {
                        rtnVal += ",";
                    }
                    rtnVal += "A";
                }
                if (label3Selected)
                {
                    if (rtnVal.Length > 0)
                    {
                        rtnVal += ",";
                    }
                    rtnVal += "C";
                }
                if (label4Selected)
                {
                    if (rtnVal.Length > 0)
                    {
                        rtnVal += ",";
                    }
                    rtnVal += "I";
                }
                return rtnVal;
            }
            set
            {
                label1Selected = value.Contains('R');
                label2Selected = value.Contains('A');
                label3Selected = value.Contains('C');
                label4Selected = value.Contains('I');
                UpdateButton(label1, label1Selected);
                UpdateButton(label2, label2Selected);
                UpdateButton(label3, label3Selected);
                UpdateButton(label4, label4Selected);
            }
        }

        bool label1Selected = false;
        bool label2Selected = false;
        bool label3Selected = false;
        bool label4Selected = false;

        private void Label1_Click(object sender, EventArgs e)
        {
            Parent.Focus();

            label1Selected = !label1Selected;
            ItemClick?.Invoke(this, EventArgs.Empty);
            UpdateButton(label1, label1Selected);
        }

        private static void UpdateButton(Label label, bool selected)
        {
            if (selected)
            {
                //label1.BackColor = Color.Blue;
                label.BackColor = Color.FromKnownColor(KnownColor.ActiveCaption);
                label.ForeColor = Color.FromKnownColor(KnownColor.ActiveCaptionText);
            }
            else
            {
                // https://dotnetref.blogspot.com/2008/06/setting-backcolor-to-system-colors-like.html
                label.BackColor = Color.FromKnownColor(KnownColor.Control);
                label.ForeColor = Color.FromKnownColor(KnownColor.ControlText);
            }
        }

        private void Label2_Click(object sender, EventArgs e)
        {
            Parent.Focus();
            label2Selected = !label2Selected;
            ItemClick?.Invoke(this, EventArgs.Empty);
            UpdateButton(label2, label2Selected);
        }

        private void Label3_Click(object sender, EventArgs e)
        {
            Parent.Focus();
            label3Selected = !label3Selected;
            ItemClick?.Invoke(this, EventArgs.Empty);
            UpdateButton(label3, label3Selected);
        }

        private void Label4_Click(object sender, EventArgs e)
        {
            Parent.Focus();
            label4Selected = !label4Selected;
            ItemClick?.Invoke(this, EventArgs.Empty);
            UpdateButton(label4, label4Selected);
        }
    }
}
