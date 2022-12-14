using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using KukaSystems;

namespace Setup
{
    partial class FrmMain
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
        // Setup.frmMain
        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FrmMain));
            this.lblTitle = new Label();
            this.lblCompany = new Label();
            this.txtLog = new TextBox();
            this.lblProduct = new Label();
            this.pnl1 = new Panel();
            this.btnReboot = new Krc4Button();
            this.lblMainMessage = new Label();
            this.btnOk = new Krc4Button();
            this.lblLog = new Label();
            this.pnlLicense = new Panel();
            this.btnCancel = new Krc4Button();
            this.btnAccept = new Krc4Button();
            this.rtbLicense = new RichTextBox();
            this.lblLicenseAccept = new Label();
            this.btnContinue = new Krc4Button();
            this.lblLicense = new Label();
            this.timDoJob = new Timer(this.components);
            this.timEnd = new Timer(this.components);
            this.pnl1.SuspendLayout();
            this.pnlLicense.SuspendLayout();
            this.SuspendLayout();
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI Light", 27.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblTitle.ForeColor = Color.White;
            

            lblTitle.Location = new Point(7, 29); 
            this.lblTitle.Name = "lblTitle";
          
            lblTitle.Size = new Size(303, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Technology Setup";
            this.lblCompany.AutoSize = true;
            this.lblCompany.Font = new Font("Segoe UI Light", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblCompany.ForeColor = Color.White;

            lblCompany.Location = new Point(14, 9);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new Size(95, 21);
            this.lblCompany.TabIndex = 1;
            this.lblCompany.Text = "OrangeApps";
            this.txtLog.BackColor = Color.FromArgb(253, 149, 0);
            this.txtLog.BorderStyle = BorderStyle.None;
            this.txtLog.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.txtLog.ForeColor = Color.White;
            Control arg_296_0 = this.txtLog;
           
            txtLog.Location = new Point(4, 54);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;

            txtLog.Size = new Size(389, 203);
            this.txtLog.TabIndex = 2;
            this.lblProduct.AutoSize = true;
            this.lblProduct.Font = new Font("Segoe UI Light", 18f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblProduct.ForeColor = Color.White;

            lblProduct.Location = new Point(12, 79);
            this.lblProduct.Name = "lblProduct";
            lblProduct.Size = new Size(0, 32);
            this.lblProduct.TabIndex = 3;
            this.pnl1.Controls.Add(this.btnReboot);
            this.pnl1.Controls.Add(this.lblMainMessage);
            this.pnl1.Controls.Add(this.btnOk);
            this.pnl1.Controls.Add(this.lblLog);
            this.pnl1.Controls.Add(this.txtLog);
            pnl1.Location = new Point(12, 114);
            this.pnl1.Name = "pnl1";
             pnl1.Size = new Size(396, 424);
            this.pnl1.TabIndex = 4;
            this.btnReboot.BackColor = Color.Transparent;
            this.btnReboot.BackgroundImageLayout = ImageLayout.None;
            this.btnReboot.ButtonImage = null;
            this.btnReboot.CheckBox = false;
            this.btnReboot.Checked = false;
            this.btnReboot.DarkMode = false;
            this.btnReboot.Font = new Font("Tahoma", 13f, FontStyle.Regular, GraphicsUnit.Pixel);

            btnReboot.Location = new Point(286, 384);
            

            btnReboot.Margin = new Padding(0);
            btnReboot.MaximumSize = new Size(520, 520); 
            btnReboot.MinimumSize = new Size(3, 3);
            this.btnReboot.Name = "btnReboot";
            btnReboot.Size = new Size(110, 40);
            this.btnReboot.TabIndex = 7;
            this.btnReboot.TextCaption = "Steuerungs-PC jetzt neu starten";
            this.btnReboot.Visible = false;
            this.lblMainMessage.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblMainMessage.ForeColor = Color.White;

            lblMainMessage.Location = new Point(3, 291);
            this.lblMainMessage.Name = "lblMainMessage";
            lblMainMessage.Size = new Size(390, 74);
            this.lblMainMessage.TabIndex = 6;
            this.lblMainMessage.TextAlign = ContentAlignment.MiddleLeft;
            this.btnOk.BackColor = Color.Transparent;
            this.btnOk.BackgroundImageLayout = ImageLayout.None;
            this.btnOk.ButtonImage = null;
            this.btnOk.CheckBox = false;
            this.btnOk.Checked = false;
            this.btnOk.DarkMode = false;
            this.btnOk.Enabled = false;
            this.btnOk.Font = new Font("Tahoma", 13f, FontStyle.Regular, GraphicsUnit.Pixel);
            btnOk.Location = new Point(154, 384);
            btnOk.Margin = new Padding(0);
            btnOk.MaximumSize = new Size(520, 520);

            btnOk.MinimumSize = new Size(3, 3);
            this.btnOk.Name = "btnOk";
            btnOk.Size = new Size(89, 40);
            this.btnOk.TabIndex = 5;
            this.btnOk.TextCaption = "Ok";
            this.lblLog.AutoSize = true;
            this.lblLog.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblLog.ForeColor = Color.White;
             lblLog.Location = new Point(3, 30);
            this.lblLog.Name = "lblLog";
            lblLog.Size = new Size(38, 21);
            this.lblLog.TabIndex = 4;
            this.lblLog.Text = "Log";
            this.pnlLicense.Controls.Add(this.btnCancel);
            this.pnlLicense.Controls.Add(this.btnAccept);
            this.pnlLicense.Controls.Add(this.rtbLicense);
            this.pnlLicense.Controls.Add(this.lblLicenseAccept);
            this.pnlLicense.Controls.Add(this.btnContinue);
            this.pnlLicense.Controls.Add(this.lblLicense);

            pnlLicense.Location= new Point(12, 114);
            this.pnlLicense.Name = "pnlLicense";
            pnlLicense.Size= new Size(396, 424);
            this.pnlLicense.TabIndex = 5;
            this.btnCancel.Anchor = AnchorStyles.Bottom;
            this.btnCancel.BackColor = Color.Transparent;
            this.btnCancel.BackgroundImageLayout = ImageLayout.None;
            this.btnCancel.ButtonImage = null;
            this.btnCancel.CheckBox = false;
            this.btnCancel.Checked = false;
            this.btnCancel.DarkMode = false;
            this.btnCancel.Font = new Font("Tahoma", 13f, FontStyle.Regular, GraphicsUnit.Pixel);

            btnCancel.Location = new Point(306, 384);
            btnCancel.Margin = new Padding(0);

            btnCancel.MaximumSize = new Size(520, 520);

            btnCancel.MinimumSize= new Size(3, 3);
            this.btnCancel.Name = "btnCancel";

            btnCancel.Size= new Size(90, 40);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.TextCaption = "Cancel";
            this.btnAccept.Anchor = AnchorStyles.Bottom;
            this.btnAccept.BackColor = Color.Transparent;
            this.btnAccept.BackgroundImageLayout = ImageLayout.None;
            this.btnAccept.ButtonImage = null;
            this.btnAccept.CheckBox = true;
            this.btnAccept.Checked = false;
            this.btnAccept.DarkMode = false;
            this.btnAccept.Font = new Font("Tahoma", 13f, FontStyle.Regular, GraphicsUnit.Pixel);

            btnAccept.Location = new Point(0, 384);
            this.btnAccept.Margin = new Padding(0);

            btnAccept.MaximumSize= new Size(520, 520);
            
            btnAccept.MinimumSize=new Size(3,3);
            this.btnAccept.Name = "btnAccept";

            btnAccept.Size=new Size(90,40);
            this.btnAccept.TabIndex = 8;
            this.btnAccept.TextCaption = "I Accept";
            this.rtbLicense.BackColor = Color.FromArgb(253, 149, 0);
            this.rtbLicense.BorderStyle = BorderStyle.None;
            this.rtbLicense.ForeColor = Color.White;

            rtbLicense.Location= new Point(17, 54);

            rtbLicense.Margin =  new Padding(2);
            this.rtbLicense.Name = "rtbLicense";
            this.rtbLicense.ReadOnly = true;

            rtbLicense.Size= new Size(376, 247);
            this.rtbLicense.TabIndex = 7;
            this.rtbLicense.Text = "";
            this.lblLicenseAccept.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblLicenseAccept.ForeColor = Color.White;

            lblLicenseAccept.Location= new Point(3, 309);
            this.lblLicenseAccept.Name = "lblLicenseAccept";

            lblLicenseAccept.Size= new Size(390, 67);
            this.lblLicenseAccept.TabIndex = 6;
            this.lblLicenseAccept.TextAlign = ContentAlignment.MiddleLeft;
            this.btnContinue.BackColor = Color.Transparent;
            this.btnContinue.BackgroundImageLayout = ImageLayout.None;
            this.btnContinue.ButtonImage = null;
            this.btnContinue.CheckBox = false;
            this.btnContinue.Checked = false;
            this.btnContinue.DarkMode = false;
            this.btnContinue.Enabled = false;
            this.btnContinue.Font = new Font("Tahoma", 13f, FontStyle.Regular, GraphicsUnit.Pixel);

            btnContinue.Location= new Point(154, 384);

            btnContinue.Margin =  new Padding(0);

            btnContinue.MaximumSize =new Size(520, 520);

            btnContinue.MinimumSize= new Size(3, 3);
            this.btnContinue.Name = "btnContinue";

            btnContinue.Size= new Size(89, 40);
            this.btnContinue.TabIndex = 5;
            this.btnContinue.TextCaption = "Continue";
            this.lblLicense.AutoSize = true;
            this.lblLicense.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblLicense.ForeColor = Color.White;

            lblLicense.Location= new Point(13, 30);
            this.lblLicense.Name = "lblLicense";

            lblLicense.Size= new Size(64, 21);
            this.lblLicense.TabIndex = 4;
            this.lblLicense.Text = "License";
            this.timDoJob.Interval = 1000;
            this.timEnd.Interval = 5000;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = Color.FromArgb(253, 149, 0);
            this.ClientSize = new Size(420, 550);
            this.Controls.Add(this.pnl1);
            this.Controls.Add(this.pnlLicense);
            this.Controls.Add(this.lblProduct);
            this.Controls.Add(this.lblCompany);
            this.Controls.Add(this.lblTitle);
            this.Font = new Font("Segoe UI", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMain";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.pnl1.ResumeLayout(false);
            this.pnl1.PerformLayout();
            this.pnlLicense.ResumeLayout(false);
            this.pnlLicense.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.Panel pnl1;
        private System.Windows.Forms.Label lblLog;
        private Krc4Button btnOk;
        private System.Windows.Forms.Label lblMainMessage;
        private System.Windows.Forms.Panel pnlLicense;
        private System.Windows.Forms.Label lblLicenseAccept;
        private Krc4Button btnContinue;
        private System.Windows.Forms.Label lblLicense;
        private System.Windows.Forms.RichTextBox rtbLicense;
        private Krc4Button btnCancel;
        private Krc4Button btnAccept;
        private System.Windows.Forms.Timer timDoJob;
        private System.Windows.Forms.Timer timEnd;
        private Krc4Button btnReboot;
    }
}