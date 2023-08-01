namespace RACI_Grid
{
    public partial class RaciGridHeaderButton : UserControl
    {
        public RaciGridHeaderButton()
        {
            InitializeComponent();
            //            label1.Text = "This is a really long piece of text";
        }

        public override string Text
        {
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value;
            }
        }

        public EventHandler<EventArgs> AddRequested;

        private void label1_Click(object sender, EventArgs e)
        {
            AddRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
