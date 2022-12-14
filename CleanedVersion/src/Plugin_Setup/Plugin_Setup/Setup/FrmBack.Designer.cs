using System.Drawing;
using System.Windows.Forms;

namespace Plugin_Setup.Setup
{
    partial class FrmBack
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            SizeF autoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleDimensions = autoScaleDimensions;
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.Black;
            Size clientSize = new Size(284, 262);
            this.ClientSize = clientSize;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = "frmBack";
            this.Opacity = 0.8;
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Maximized;
            this.ResumeLayout(false);
        }

        #endregion
    }
}