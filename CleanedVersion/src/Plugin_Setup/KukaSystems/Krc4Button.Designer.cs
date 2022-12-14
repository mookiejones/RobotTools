using System;
using System.Drawing;
using System.Windows.Forms;

namespace KukaSystems
{
    sealed partial class Krc4Button
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
            this.label1 = new Label();
            base.SuspendLayout();
            this.label1.Font = new Font("Tahoma", 13f, FontStyle.Regular, GraphicsUnit.Pixel);
            this.label1.Location = new Point(0, 0);
            this.label1.Margin = new Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(66, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "Button1";
            this.label1.TextAlign = ContentAlignment.MiddleCenter;
            this.label1.MouseLeave += new EventHandler(this.label1_MouseLeave);
            this.label1.Click += new EventHandler(this.label1_Click);
            this.label1.MouseDown += new MouseEventHandler(this.label1_MouseDown);
            this.label1.MouseUp += new MouseEventHandler(this.label1_MouseUp);
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.Transparent;
            this.BackgroundImageLayout = ImageLayout.None;
            base.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Font = new Font("Tahoma", 13f, FontStyle.Regular, GraphicsUnit.Pixel);
            base.Margin = new Padding(0);
            this.MaximumSize = new Size(520, 520);
            this.MinimumSize = new Size(3, 3);
            base.Name = "Krc4Button";
            base.Size = new Size(66, 40);
            base.Paint += new PaintEventHandler(this.Krc4Button_Paint);
            base.FontChanged += new EventHandler(this.Krc4Button_FontChanged);
            base.Resize += new EventHandler(this.Krc4Button_Resize);
            base.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
    }
}
