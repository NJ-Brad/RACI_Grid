﻿namespace RACI_Grid
{
    partial class RaciItem
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(0, 0);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(31, 25);
            label1.TabIndex = 0;
            label1.Text = "R";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Click += Label1_Click;
            // 
            // label2
            // 
            label2.BorderStyle = BorderStyle.FixedSingle;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(31, 0);
            label2.Margin = new Padding(0);
            label2.Name = "label2";
            label2.Size = new Size(31, 25);
            label2.TabIndex = 1;
            label2.Text = "A";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            label2.Click += Label2_Click;
            // 
            // label3
            // 
            label3.BorderStyle = BorderStyle.FixedSingle;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(62, 0);
            label3.Margin = new Padding(0);
            label3.Name = "label3";
            label3.Size = new Size(31, 25);
            label3.TabIndex = 2;
            label3.Text = "C";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            label3.Click += Label3_Click;
            // 
            // label4
            // 
            label4.BorderStyle = BorderStyle.FixedSingle;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(93, 0);
            label4.Margin = new Padding(0);
            label4.Name = "label4";
            label4.Size = new Size(31, 25);
            label4.TabIndex = 3;
            label4.Text = "I";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            label4.Click += Label4_Click;
            // 
            // RaciItem
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Margin = new Padding(0);
            Name = "RaciItem";
            Size = new Size(124, 25);
            ResumeLayout(false);
        }

        #endregion
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}
