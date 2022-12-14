using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace Setup
{
	[DesignerGenerated]
	public class frmBack : Form
	{
		private IContainer components;
		public frmBack()
		{
			this.InitializeComponent();
		}
		[DebuggerNonUserCode]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.components != null)
				{
					this.components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}
		[DebuggerStepThrough]
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
	}
}
