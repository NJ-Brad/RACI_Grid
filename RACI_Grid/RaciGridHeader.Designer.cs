namespace RACI_Grid
{
    partial class RaciGridHeader
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            textBox1 = new TextBox();
            label1 = new Label();
            contextMenuStrip1 = new ContextMenuStrip(components);
            Rename = new ToolStripMenuItem();
            Delete = new ToolStripMenuItem();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.BackColor = SystemColors.Window;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Location = new Point(2, 2);
            textBox1.Margin = new Padding(0);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(123, 18);
            textBox1.TabIndex = 0;
            textBox1.TextChanged += TextBox1_TextChanged;
            textBox1.Enter += TextBox1_Enter;
            textBox1.KeyDown += TextBox1_KeyDown;
            textBox1.Leave += TextBox1_Leave;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.BackColor = SystemColors.Window;
            label1.ContextMenuStrip = contextMenuStrip1;
            label1.Location = new Point(-1, 2);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(125, 18);
            label1.TabIndex = 1;
            label1.Text = "label1";
            label1.MouseClick += label1_MouseClick;
            label1.MouseDown += label1_MouseDown;
            label1.MouseUp += label1_MouseUp;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { Rename, Delete });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(124, 48);
            // 
            // Rename
            // 
            Rename.Name = "Rename";
            Rename.Size = new Size(123, 22);
            Rename.Text = "Rename";
            Rename.Click += Rename_Click;
            // 
            // Delete
            // 
            Delete.Name = "Delete";
            Delete.Size = new Size(123, 22);
            Delete.Text = "Delete";
            Delete.Click += Delete_Click;
            // 
            // RaciGridHeader
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(label1);
            Controls.Add(textBox1);
            Margin = new Padding(0);
            Name = "RaciGridHeader";
            Size = new Size(125, 25);
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Label label1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem Rename;
        private ToolStripMenuItem Delete;
    }
}
