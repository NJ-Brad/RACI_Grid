namespace RACI_Grid
{
    public partial class RaciGridHeader : UserControl
    {
        public RaciGridHeader()
        {
            InitializeComponent();
            //            label1.Text = "This is a really long piece of text";
        }

        public override string Text
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
                label1.Text = value;
            }
        }

        bool labelMode = false;
        public bool LabelMode
        {
            get { return labelMode; }
            set
            {
                labelMode = value;
                HandleLabelMode();
            }
        }

        private async void HandleLabelMode()
        {
            if (labelMode)
            {
                label1.Visible = true;
                textBox1.Visible = false;
            }
            else
            {
                label1.Visible = false;
                textBox1.Visible = true;
                //                textBox1.BringToFront();
            }
        }

        public EventHandler<EventArgs> Changed;
        public EventHandler<EventArgs> DeleteRequested;

        //TextBox? editBox = null;

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox1.AcceptsReturn == false && e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                labelClickedOnce = false;
                label1.Text = textBox1.Text;
                LabelMode = true;

                Parent.Focus(); // stop the blinking cursor
            }//do nothing to avoid the warning sound
            else
                base.OnKeyDown(e);
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        string originalText = "";

        private void TextBox1_Leave(object sender, EventArgs e)
        {
            labelClickedOnce = false;
            LabelMode = true;
            label1.Text = textBox1.Text;

            TriggerIfChanged();
        }

        private void TriggerIfChanged()
        {
            if (originalText != textBox1.Text)
            {
                Changed?.Invoke(this, EventArgs.Empty);
            }
        }

        private void TextBox1_Enter(object sender, EventArgs e)
        {
            originalText = textBox1.Text;
        }

        bool labelClickedOnce = false;

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            // https://stackoverflow.com/questions/9823883/adding-a-right-click-menu-to-an-item
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        //                        rightClickMenuStrip.Show(this, new Point(e.X, e.Y));//places the menu at the pointer position
                        contextMenuStrip1.Show(this, new Point(e.X, e.Y));//places the menu at the pointer position
                    }
                    break;
            }
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        public bool ClickTwiceToEdit { get; set; } = false;

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (ClickTwiceToEdit)
                {
                    if (labelClickedOnce)
                    {
                        //                labelMode = false;
                        labelClickedOnce = false;
                        //                HandleLabelMode();
                        LabelMode = false;
                        textBox1.Focus();
                    }
                    else
                    {
                        labelClickedOnce = true;
                    }
                }
            }
        }

        private void Rename_Click(object sender, EventArgs e)
        {
            LabelMode = false;
            textBox1.Focus();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            DeleteRequested?.Invoke(this, EventArgs.Empty);
        }



        //private void label1_Click(object sender, EventArgs e)
        //{
        //    editBox = new TextBox();
        //    //Point pt = label1.Location;
        //    //pt.Y -= 2;
        //    editBox.Location = label1.Location;
        //    editBox.Site = label1.Site;
        //    editBox.Text = label1.Text;
        //    editBox.Visible = true;
        //    Controls.Add(editBox);
        //    editBox.BringToFront();
        //    editBox.Focus();
        //}
    }
}
