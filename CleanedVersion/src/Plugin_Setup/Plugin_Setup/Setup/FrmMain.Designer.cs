using Ionic.Zip;
using Microsoft.VisualBasic;
using Microsoft.Win32;

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Plugin_Setup.Controls;
using Plugin_Setup.Setup;

namespace Plugin_Setup.Setup
{

    public partial class FrmMain : Form
    {
        public enum JobType
        {
            Install1,
            Install2,
            Update1,
            Update2,
            Reinstall1,
            Reinstall2,
            UnInstall1,
            Uninstall2,
            NoJob
        }
        private enum LogType
        {
            Info,
            Warning,
            Exception
        }
        private IContainer components;
        [AccessedThroughProperty("lblTitle")]
        private Label _lblTitle;
        [AccessedThroughProperty("lblCompany")]
        private Label _lblCompany;
        [AccessedThroughProperty("txtLog")]
        private TextBox _txtLog;
        [AccessedThroughProperty("lblProduct")]
        private Label _lblProduct;
        [AccessedThroughProperty("pnl1")]
        private Panel _pnl1;
        [AccessedThroughProperty("lblLog")]
        private Label _lblLog;
        [AccessedThroughProperty("btnOk")]
        private Krc4Button _btnOk;
        [AccessedThroughProperty("lblMainMessage")]
        private Label _lblMainMessage;
        [AccessedThroughProperty("pnlLicense")]
        private Panel _pnlLicense;
        [AccessedThroughProperty("lblLicenseAccept")]
        private Label _lblLicenseAccept;
        [AccessedThroughProperty("btnContinue")]
        private Krc4Button _btnContinue;
        [AccessedThroughProperty("lblLicense")]
        private Label _lblLicense;
        [AccessedThroughProperty("rtbLicense")]
        private RichTextBox _rtbLicense;
        [AccessedThroughProperty("btnCancel")]
        private Krc4Button _btnCancel;
        [AccessedThroughProperty("btnAccept")]
        private Krc4Button _btnAccept;
        [AccessedThroughProperty("timDoJob")]
        private Timer _timDoJob;
        [AccessedThroughProperty("timEnd")]
        private Timer _timEnd;
        [AccessedThroughProperty("btnReboot")]
        private Krc4Button _btnReboot;
        private string sCompany;
        private string sName;
        private string sVersion;
        private string sBuild;
        private string sDll;
        public frmMain.JobType Job;
        private bool SmartHMIwasRuning;
        private IniFile myIni;
        private string strLogFile;
        private int iWarnings;
        private int iErrors;
        internal virtual Label lblTitle
        {
            get
            {
                return this._lblTitle;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                this._lblTitle = value;
            }
        }
        internal virtual Label lblCompany
        {
            get
            {
                return this._lblCompany;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                this._lblCompany = value;
            }
        }
        internal virtual TextBox txtLog
        {
            get
            {
                return this._txtLog;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                this._txtLog = value;
            }
        }
        internal virtual Label lblProduct
        {
            get
            {
                return this._lblProduct;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                this._lblProduct = value;
            }
        }
        internal virtual Panel pnl1
        {
            get
            {
                return this._pnl1;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                this._pnl1 = value;
            }
        }
        internal virtual Label lblLog
        {
            get
            {
                return this._lblLog;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                this._lblLog = value;
            }
        }
        internal virtual Krc4Button btnOk
        {
            get
            {
                return this._btnOk;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                Krc4Button.ButtonClickHandler value2 = new Krc4Button.ButtonClickHandler(this.btnOk_ButtonClick);
                if (this._btnOk != null)
                {
                    this._btnOk.ButtonClick -= value2;
                }
                this._btnOk = value;
                if (this._btnOk != null)
                {
                    this._btnOk.ButtonClick += value2;
                }
            }
        }
        internal virtual Label lblMainMessage
        {
            get
            {
                return this._lblMainMessage;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                this._lblMainMessage = value;
            }
        }
        internal virtual Panel pnlLicense
        {
            get
            {
                return this._pnlLicense;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                this._pnlLicense = value;
            }
        }
        internal virtual Label lblLicenseAccept
        {
            get
            {
                return this._lblLicenseAccept;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                this._lblLicenseAccept = value;
            }
        }
        internal virtual Krc4Button btnContinue
        {
            get
            {
                return this._btnContinue;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                Krc4Button.ButtonClickHandler value2 = new Krc4Button.ButtonClickHandler(this.btnContinue_ButtonClick);
                if (this._btnContinue != null)
                {
                    this._btnContinue.ButtonClick -= value2;
                }
                this._btnContinue = value;
                if (this._btnContinue != null)
                {
                    this._btnContinue.ButtonClick += value2;
                }
            }
        }
        internal virtual Label lblLicense
        {
            get
            {
                return this._lblLicense;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                this._lblLicense = value;
            }
        }
        internal virtual RichTextBox rtbLicense
        {
            get
            {
                return this._rtbLicense;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                this._rtbLicense = value;
            }
        }
        internal virtual Krc4Button btnCancel
        {
            get
            {
                return this._btnCancel;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                Krc4Button.ButtonClickHandler value2 = new Krc4Button.ButtonClickHandler(this.btnCancel_ButtonClick);
                if (this._btnCancel != null)
                {
                    this._btnCancel.ButtonClick -= value2;
                }
                this._btnCancel = value;
                if (this._btnCancel != null)
                {
                    this._btnCancel.ButtonClick += value2;
                }
            }
        }
        internal virtual Krc4Button btnAccept
        {
            get
            {
                return this._btnAccept;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                Krc4Button.ButtonClickHandler value2 = new Krc4Button.ButtonClickHandler(this.btnAccept_ButtonCheckedChanged);
                if (this._btnAccept != null)
                {
                    this._btnAccept.ButtonCheckedChanged -= value2;
                }
                this._btnAccept = value;
                if (this._btnAccept != null)
                {
                    this._btnAccept.ButtonCheckedChanged += value2;
                }
            }
        }
        internal virtual Timer timDoJob
        {
            get
            {
                return this._timDoJob;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler value2 = new EventHandler(this.timDoJob_Tick);
                if (this._timDoJob != null)
                {
                    this._timDoJob.Tick -= value2;
                }
                this._timDoJob = value;
                if (this._timDoJob != null)
                {
                    this._timDoJob.Tick += value2;
                }
            }
        }
        internal virtual Timer timEnd
        {
            get
            {
                return this._timEnd;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                EventHandler value2 = new EventHandler(this.timEnd_Tick);
                if (this._timEnd != null)
                {
                    this._timEnd.Tick -= value2;
                }
                this._timEnd = value;
                if (this._timEnd != null)
                {
                    this._timEnd.Tick += value2;
                }
            }
        }
        internal virtual Krc4Button btnReboot
        {
            get
            {
                return this._btnReboot;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                Krc4Button.ButtonClickHandler value2 = new Krc4Button.ButtonClickHandler(this.btnReboot_ButtonClick);
                if (this._btnReboot != null)
                {
                    this._btnReboot.ButtonClick -= value2;
                }
                this._btnReboot = value;
                if (this._btnReboot != null)
                {
                    this._btnReboot.ButtonClick += value2;
                }
            }
        }
        public FrmMain()
        {
            base.Load += new EventHandler(this.frmMain_Load);
            this.sCompany = "";
            this.sName = "";
            this.sVersion = "";
            this.sBuild = "";
            this.sDll = "";
            this.myIni = new IniFile();
            this.iWarnings = 0;
            this.iErrors = 0;
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
            this.components = new Container();
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmMain));
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
            Control arg_153_0 = this.lblTitle;
            Point location = new Point(7, 29);
            arg_153_0.Location = location;
            this.lblTitle.Name = "lblTitle";
            Control arg_17D_0 = this.lblTitle;
            Size size = new Size(303, 50);
            arg_17D_0.Size = size;
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Technology Setup";
            this.lblCompany.AutoSize = true;
            this.lblCompany.Font = new Font("Segoe UI Light", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblCompany.ForeColor = Color.White;
            Control arg_1E9_0 = this.lblCompany;
            location = new Point(14, 9);
            arg_1E9_0.Location = location;
            this.lblCompany.Name = "lblCompany";
            Control arg_210_0 = this.lblCompany;
            size = new Size(95, 21);
            arg_210_0.Size = size;
            this.lblCompany.TabIndex = 1;
            this.lblCompany.Text = "OrangeApps";
            this.txtLog.BackColor = Color.FromArgb(253, 149, 0);
            this.txtLog.BorderStyle = BorderStyle.None;
            this.txtLog.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.txtLog.ForeColor = Color.White;
            Control arg_296_0 = this.txtLog;
            location = new Point(4, 54);
            arg_296_0.Location = location;
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            Control arg_2DB_0 = this.txtLog;
            size = new Size(389, 203);
            arg_2DB_0.Size = size;
            this.txtLog.TabIndex = 2;
            this.lblProduct.AutoSize = true;
            this.lblProduct.Font = new Font("Segoe UI Light", 18f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblProduct.ForeColor = Color.White;
            Control arg_337_0 = this.lblProduct;
            location = new Point(12, 79);
            arg_337_0.Location = location;
            this.lblProduct.Name = "lblProduct";
            Control arg_35D_0 = this.lblProduct;
            size = new Size(0, 32);
            arg_35D_0.Size = size;
            this.lblProduct.TabIndex = 3;
            this.pnl1.Controls.Add(this.btnReboot);
            this.pnl1.Controls.Add(this.lblMainMessage);
            this.pnl1.Controls.Add(this.btnOk);
            this.pnl1.Controls.Add(this.lblLog);
            this.pnl1.Controls.Add(this.txtLog);
            Control arg_3EE_0 = this.pnl1;
            location = new Point(12, 114);
            arg_3EE_0.Location = location;
            this.pnl1.Name = "pnl1";
            Control arg_41B_0 = this.pnl1;
            size = new Size(396, 424);
            arg_41B_0.Size = size;
            this.pnl1.TabIndex = 4;
            this.btnReboot.BackColor = Color.Transparent;
            this.btnReboot.BackgroundImageLayout = ImageLayout.None;
            this.btnReboot.ButtonImage = null;
            this.btnReboot.CheckBox = false;
            this.btnReboot.Checked = false;
            this.btnReboot.DarkMode = false;
            this.btnReboot.Font = new Font("Tahoma", 13f, FontStyle.Regular, GraphicsUnit.Pixel);
            Control arg_4AC_0 = this.btnReboot;
            location = new Point(286, 384);
            arg_4AC_0.Location = location;
            Control arg_4C0_0 = this.btnReboot;
            Padding margin = new Padding(0);
            arg_4C0_0.Margin = margin;
            Control arg_4DD_0 = this.btnReboot;
            size = new Size(520, 520);
            arg_4DD_0.MaximumSize = size;
            Control arg_4F2_0 = this.btnReboot;
            size = new Size(3, 3);
            arg_4F2_0.MinimumSize = size;
            this.btnReboot.Name = "btnReboot";
            Control arg_519_0 = this.btnReboot;
            size = new Size(110, 40);
            arg_519_0.Size = size;
            this.btnReboot.TabIndex = 7;
            this.btnReboot.TextCaption = "Steuerungs-PC jetzt neu starten";
            this.btnReboot.Visible = false;
            this.lblMainMessage.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblMainMessage.ForeColor = Color.White;
            Control arg_587_0 = this.lblMainMessage;
            location = new Point(3, 291);
            arg_587_0.Location = location;
            this.lblMainMessage.Name = "lblMainMessage";
            Control arg_5B1_0 = this.lblMainMessage;
            size = new Size(390, 74);
            arg_5B1_0.Size = size;
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
            Control arg_65B_0 = this.btnOk;
            location = new Point(154, 384);
            arg_65B_0.Location = location;
            Control arg_66F_0 = this.btnOk;
            margin = new Padding(0);
            arg_66F_0.Margin = margin;
            Control arg_68C_0 = this.btnOk;
            size = new Size(520, 520);
            arg_68C_0.MaximumSize = size;
            Control arg_6A1_0 = this.btnOk;
            size = new Size(3, 3);
            arg_6A1_0.MinimumSize = size;
            this.btnOk.Name = "btnOk";
            Control arg_6C8_0 = this.btnOk;
            size = new Size(89, 40);
            arg_6C8_0.Size = size;
            this.btnOk.TabIndex = 5;
            this.btnOk.TextCaption = "Ok";
            this.lblLog.AutoSize = true;
            this.lblLog.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblLog.ForeColor = Color.White;
            Control arg_733_0 = this.lblLog;
            location = new Point(3, 30);
            arg_733_0.Location = location;
            this.lblLog.Name = "lblLog";
            Control arg_75A_0 = this.lblLog;
            size = new Size(38, 21);
            arg_75A_0.Size = size;
            this.lblLog.TabIndex = 4;
            this.lblLog.Text = "Log";
            this.pnlLicense.Controls.Add(this.btnCancel);
            this.pnlLicense.Controls.Add(this.btnAccept);
            this.pnlLicense.Controls.Add(this.rtbLicense);
            this.pnlLicense.Controls.Add(this.lblLicenseAccept);
            this.pnlLicense.Controls.Add(this.btnContinue);
            this.pnlLicense.Controls.Add(this.lblLicense);
            Control arg_811_0 = this.pnlLicense;
            location = new Point(12, 114);
            arg_811_0.Location = location;
            this.pnlLicense.Name = "pnlLicense";
            Control arg_83E_0 = this.pnlLicense;
            size = new Size(396, 424);
            arg_83E_0.Size = size;
            this.pnlLicense.TabIndex = 5;
            this.btnCancel.Anchor = AnchorStyles.Bottom;
            this.btnCancel.BackColor = Color.Transparent;
            this.btnCancel.BackgroundImageLayout = ImageLayout.None;
            this.btnCancel.ButtonImage = null;
            this.btnCancel.CheckBox = false;
            this.btnCancel.Checked = false;
            this.btnCancel.DarkMode = false;
            this.btnCancel.Font = new Font("Tahoma", 13f, FontStyle.Regular, GraphicsUnit.Pixel);
            Control arg_8DB_0 = this.btnCancel;
            location = new Point(306, 384);
            arg_8DB_0.Location = location;
            Control arg_8EF_0 = this.btnCancel;
            margin = new Padding(0);
            arg_8EF_0.Margin = margin;
            Control arg_90C_0 = this.btnCancel;
            size = new Size(520, 520);
            arg_90C_0.MaximumSize = size;
            Control arg_921_0 = this.btnCancel;
            size = new Size(3, 3);
            arg_921_0.MinimumSize = size;
            this.btnCancel.Name = "btnCancel";
            Control arg_948_0 = this.btnCancel;
            size = new Size(90, 40);
            arg_948_0.Size = size;
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
            Control arg_9F2_0 = this.btnAccept;
            location = new Point(0, 384);
            arg_9F2_0.Location = location;
            Control arg_A06_0 = this.btnAccept;
            margin = new Padding(0);
            arg_A06_0.Margin = margin;
            Control arg_A23_0 = this.btnAccept;
            size = new Size(520, 520);
            arg_A23_0.MaximumSize = size;
            Control arg_A38_0 = this.btnAccept;
            size = new Size(3, 3);
            arg_A38_0.MinimumSize = size;
            this.btnAccept.Name = "btnAccept";
            Control arg_A5F_0 = this.btnAccept;
            size = new Size(90, 40);
            arg_A5F_0.Size = size;
            this.btnAccept.TabIndex = 8;
            this.btnAccept.TextCaption = "I Accept";
            this.rtbLicense.BackColor = Color.FromArgb(253, 149, 0);
            this.rtbLicense.BorderStyle = BorderStyle.None;
            this.rtbLicense.ForeColor = Color.White;
            Control arg_AC9_0 = this.rtbLicense;
            location = new Point(17, 54);
            arg_AC9_0.Location = location;
            Control arg_ADD_0 = this.rtbLicense;
            margin = new Padding(2);
            arg_ADD_0.Margin = margin;
            this.rtbLicense.Name = "rtbLicense";
            this.rtbLicense.ReadOnly = true;
            Control arg_B16_0 = this.rtbLicense;
            size = new Size(376, 247);
            arg_B16_0.Size = size;
            this.rtbLicense.TabIndex = 7;
            this.rtbLicense.Text = "";
            this.lblLicenseAccept.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblLicenseAccept.ForeColor = Color.White;
            Control arg_B78_0 = this.lblLicenseAccept;
            location = new Point(3, 309);
            arg_B78_0.Location = location;
            this.lblLicenseAccept.Name = "lblLicenseAccept";
            Control arg_BA2_0 = this.lblLicenseAccept;
            size = new Size(390, 67);
            arg_BA2_0.Size = size;
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
            Control arg_C4C_0 = this.btnContinue;
            location = new Point(154, 384);
            arg_C4C_0.Location = location;
            Control arg_C60_0 = this.btnContinue;
            margin = new Padding(0);
            arg_C60_0.Margin = margin;
            Control arg_C7D_0 = this.btnContinue;
            size = new Size(520, 520);
            arg_C7D_0.MaximumSize = size;
            Control arg_C92_0 = this.btnContinue;
            size = new Size(3, 3);
            arg_C92_0.MinimumSize = size;
            this.btnContinue.Name = "btnContinue";
            Control arg_CB9_0 = this.btnContinue;
            size = new Size(89, 40);
            arg_CB9_0.Size = size;
            this.btnContinue.TabIndex = 5;
            this.btnContinue.TextCaption = "Continue";
            this.lblLicense.AutoSize = true;
            this.lblLicense.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblLicense.ForeColor = Color.White;
            Control arg_D25_0 = this.lblLicense;
            location = new Point(13, 30);
            arg_D25_0.Location = location;
            this.lblLicense.Name = "lblLicense";
            Control arg_D4C_0 = this.lblLicense;
            size = new Size(64, 21);
            arg_D4C_0.Size = size;
            this.lblLicense.TabIndex = 4;
            this.lblLicense.Text = "License";
            this.timDoJob.Interval = 1000;
            this.timEnd.Interval = 5000;
            this.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.FromArgb(253, 149, 0);
            size = new Size(420, 550);
            this.ClientSize = size;
            this.Controls.Add(this.pnl1);
            this.Controls.Add(this.pnlLicense);
            this.Controls.Add(this.lblProduct);
            this.Controls.Add(this.lblCompany);
            this.Controls.Add(this.lblTitle);
            this.Font = new Font("Segoe UI", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
            this.Name = "frmMain";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.pnl1.ResumeLayout(false);
            this.pnl1.PerformLayout();
            this.pnlLicense.ResumeLayout(false);
            this.pnlLicense.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            var frm = new FrmBack().Show();

            Application.DoEvents();
            this.TopMost = true;
            Language.LangInit();
            this.LoadSetupIni();
            this.LogInit();
            Language.LanguageAct = this.GetLanguageReg();
            this.lblLicense.Text = Language.s("License").ToString();
            this.btnOk.TextCaption = Language.s("Ok").ToString();
             this.btnCancel.TextCaption = Language.s("Cancel").ToString();
            this.btnAccept.TextCaption = Language.s("I Accept").ToString();
            this.btnContinue.TextCaption = Language.s("Continue").ToString();
            this.btnReboot.TextCaption = Language.s("Reboot control PC now").ToString();
            if (this.Job == frmMain.JobType.NoJob)
            {
                this.Job = frmMain.JobType.Install1;
            }
            if (!this.SmartHMIExist())
            {
                this.Job = frmMain.JobType.NoJob;
                this.LogLine(Language.s("not a KRC4 environment").ToString(), frmMain.LogType.Info);
                Label arg_18F_0 = this.lblMainMessage;
                object arg_15B_0 = null;
                Type arg_15B_1 = typeof(string);
                string arg_15B_2 = "Format";
                object[] array = new object[]
				{
					RuntimeHelpers.GetObjectValue(Language.s("This is not a KRC4 environment! Setup canceled...")),
					this.sName
				};
                object[] arg_15B_3 = array;
                string[] arg_15B_4 = null;
                Type[] arg_15B_5 = null;
                bool[] array2 = new bool[]
				{
					false,
					true
				};
                object arg_18A_0 = NewLateBinding.LateGet(arg_15B_0, arg_15B_1, arg_15B_2, arg_15B_3, arg_15B_4, arg_15B_5, array2);
                if (array2[1])
                {
                    this.sName = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array[1]), typeof(string));
                }
                arg_18F_0.Text = Conversions.ToString(arg_18A_0);
            }
            if (this.Job != frmMain.JobType.Uninstall2 && Strings.InStr(Application.ExecutablePath, "uninstall.exe", CompareMethod.Text) > 0)
            {
                this.Job = frmMain.JobType.UnInstall1;
            }
            if (this.Job != frmMain.JobType.Reinstall2 && Strings.InStr(Application.ExecutablePath, "reinstall.exe", CompareMethod.Text) > 0)
            {
                this.Job = frmMain.JobType.Reinstall1;
            }
            string text = this.GetKrcVersionReg();
            checked
            {
                if ((this.Job == frmMain.JobType.Install1 || this.Job == frmMain.JobType.Reinstall1) && Operators.CompareString(this.myIni.GetKeyValue("Info", "SupportedKRC"), "", false) != 0)
                {
                    string[] array3 = Strings.Split(this.myIni.GetKeyValue("Info", "SupportedKRC"), ",", -1, CompareMethod.Binary);
                    bool flag = false;
                    string[] array4 = array3;
                    for (int i = 0; i < array4.Length; i++)
                    {
                        string value = array4[i];
                        if (text.StartsWith(value, StringComparison.OrdinalIgnoreCase))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        this.Job = frmMain.JobType.NoJob;
                        this.LogLine(Conversions.ToString(Language.s("KRC Version not supported")), frmMain.LogType.Info);
                        Label arg_327_0 = this.lblMainMessage;
                        object arg_2F8_0 = null;
                        Type arg_2F8_1 = typeof(string);
                        string arg_2F8_2 = "Format";
                        object[] array5 = new object[]
						{
							RuntimeHelpers.GetObjectValue(Language.s("KRC Version {0} is not supported")),
							text
						};
                        object[] arg_2F8_3 = array5;
                        string[] arg_2F8_4 = null;
                        Type[] arg_2F8_5 = null;
                        bool[] array2 = new bool[]
						{
							false,
							true
						};
                        object arg_322_0 = NewLateBinding.LateGet(arg_2F8_0, arg_2F8_1, arg_2F8_2, arg_2F8_3, arg_2F8_4, arg_2F8_5, array2);
                        if (array2[1])
                        {
                            text = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array5[1]), typeof(string));
                        }
                        arg_327_0.Text = arg_322_0.ToString();
                    }
                }
                if (this.Job == frmMain.JobType.Install1 || this.Job == frmMain.JobType.Reinstall1)
                {
                    switch (this.CheckVersionInstalled())
                    {
                        case -1:
                            {
                                this.Job = frmMain.JobType.NoJob;
                                this.LogLine(Language.s("newer version already installed").ToString(), frmMain.LogType.Info);
                                Label arg_400_0 = this.lblMainMessage;
                                object arg_3CC_0 = null;
                                Type arg_3CC_1 = typeof(string);
                                string arg_3CC_2 = "Format";
                                object[] array5 = new object[]
						{
							RuntimeHelpers.GetObjectValue(Language.s("Newer Version of {0} already installed")),
							this.sName
						};
                                object[] arg_3CC_3 = array5;
                                string[] arg_3CC_4 = null;
                                Type[] arg_3CC_5 = null;
                                bool[] array2 = new bool[]
						{
							false,
							true
						};
                                object arg_3FB_0 = NewLateBinding.LateGet(arg_3CC_0, arg_3CC_1, arg_3CC_2, arg_3CC_3, arg_3CC_4, arg_3CC_5, array2);
                                if (array2[1])
                                {
                                    this.sName = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array5[1]), typeof(string));
                                }
                                arg_400_0.Text = Conversions.ToString(arg_3FB_0);
                                break;
                            }
                        case 0:
                            {
                                this.Job = frmMain.JobType.NoJob;
                                this.LogLine(Conversions.ToString(Language.s("same version already installed")), frmMain.LogType.Info);
                                Label arg_4A8_0 = this.lblMainMessage;
                                object arg_474_0 = null;
                                Type arg_474_1 = typeof(string);
                                string arg_474_2 = "Format";
                                object[] array5 = new object[]
						{
							RuntimeHelpers.GetObjectValue(Language.s("Same Version of {0} already installed")),
							this.sName
						};
                                object[] arg_474_3 = array5;
                                string[] arg_474_4 = null;
                                Type[] arg_474_5 = null;
                                bool[] array2 = new bool[]
						{
							false,
							true
						};
                                object arg_4A3_0 = NewLateBinding.LateGet(arg_474_0, arg_474_1, arg_474_2, arg_474_3, arg_474_4, arg_474_5, array2);
                                if (array2[1])
                                {
                                    this.sName = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array5[1]), typeof(string));
                                }
                                arg_4A8_0.Text = Conversions.ToString(arg_4A3_0);
                                break;
                            }
                        case 1:
                            this.Job = frmMain.JobType.Update1;
                            this.LogLine(Conversions.ToString(Language.s("older version already installed, run update")), frmMain.LogType.Info);
                            break;
                    }
                }
                if ((this.Job == frmMain.JobType.Install1 || this.Job == frmMain.JobType.Update1) && Operators.CompareString(this.myIni.GetKeyValue("Info", "ShowLicense"), "1", false) == 0)
                {
                    this.pnlLicense.BringToFront();
                    this.lblLicenseAccept.Text = Conversions.ToString(Language.s("You must accept the terms of this License Agreement before continuing with the installation."));
                    this.rtbLicense.Rtf = this.GetLicenseText();
                }
                else
                {
                    this.pnl1.BringToFront();
                    this.timDoJob.Start();
                }
            }
        }
        private void DoJob()
        {
            switch (this.Job)
            {
                case frmMain.JobType.Install1:
                    this.JobExecuteScript("Install1");
                    if (Operators.CompareString(this.myIni.GetKeyValue("Info", "UseInstall2"), "1", false) == 0)
                    {
                        if (this.Cross3Runing())
                        {
                            this.MkDir("D:\\" + this.sCompany);
                            this.MkDir("D:\\" + this.sCompany + "\\" + this.sName);
                            this.CopyFileHd(Application.ExecutablePath, string.Concat(new string[]
						{
							"D:\\",
							this.sCompany,
							"\\",
							this.sName,
							"\\Setup.exe"
						}));
                            this.CopyFileHd(Application.StartupPath + "\\Version.ini", string.Concat(new string[]
						{
							"D:\\",
							this.sCompany,
							"\\",
							this.sName,
							"\\Version.ini"
						}));
                            this.SetRunOnce(string.Concat(new string[]
						{
							"D:\\",
							this.sCompany,
							"\\",
							this.sName,
							"\\Setup.exe -install"
						}));
                            this.SetColdBoot();
                            this.LogLine(Conversions.ToString(Language.s("installation prepared")), frmMain.LogType.Info);
                            this.lblMainMessage.Text = Conversions.ToString(Language.s("Installation prepared! Please restart control PC."));
                            this.btnReboot.Visible = true;
                            this.btnOk.TextCaption = Conversions.ToString(Language.s("Later"));
                        }
                        else
                        {
                            this.Job = frmMain.JobType.Install2;
                            this.DoJob();
                        }
                    }
                    else
                    {
                        this.CreateUnReinstall();
                        this.CreateOptionRegKey();
                        if (this.iErrors == 0)
                        {
                            object arg_27D_0 = null;
                            Type arg_27D_1 = typeof(string);
                            string arg_27D_2 = "Format";
                            object[] array = new object[]
						{
							RuntimeHelpers.GetObjectValue(Language.s("{0} successfully installed")),
							this.sName
						};
                            object[] arg_27D_3 = array;
                            string[] arg_27D_4 = null;
                            Type[] arg_27D_5 = null;
                            bool[] array2 = new bool[]
						{
							false,
							true
						};
                            object arg_2AC_0 = NewLateBinding.LateGet(arg_27D_0, arg_27D_1, arg_27D_2, arg_27D_3, arg_27D_4, arg_27D_5, array2);
                            if (array2[1])
                            {
                                this.sName = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array[1]), typeof(string));
                            }
                            this.LogLine(Conversions.ToString(arg_2AC_0), frmMain.LogType.Info);
                            Label arg_338_0 = this.lblMainMessage;
                            object arg_304_0 = null;
                            Type arg_304_1 = typeof(string);
                            string arg_304_2 = "Format";
                            object[] array3 = new object[]
						{
							RuntimeHelpers.GetObjectValue(Language.s("{0} successfully installed")),
							this.sName
						};
                            object[] arg_304_3 = array3;
                            string[] arg_304_4 = null;
                            Type[] arg_304_5 = null;
                            array2 = new bool[]
						{
							false,
							true
						};
                            object arg_333_0 = NewLateBinding.LateGet(arg_304_0, arg_304_1, arg_304_2, arg_304_3, arg_304_4, arg_304_5, array2);
                            if (array2[1])
                            {
                                this.sName = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array3[1]), typeof(string));
                            }
                            arg_338_0.Text = Conversions.ToString(arg_333_0);
                            this.timEnd.Start();
                        }
                    }
                    break;
                case frmMain.JobType.Install2:
                    this.JobExecuteScript("Install2");
                    this.CreateUnReinstall();
                    this.CreateOptionRegKey();
                    if (this.iErrors == 0)
                    {
                        object arg_3B8_0 = null;
                        Type arg_3B8_1 = typeof(string);
                        string arg_3B8_2 = "Format";
                        object[] array3 = new object[]
					{
						RuntimeHelpers.GetObjectValue(Language.s("{0} successfully installed")),
						this.sName
					};
                        object[] arg_3B8_3 = array3;
                        string[] arg_3B8_4 = null;
                        Type[] arg_3B8_5 = null;
                        bool[] array2 = new bool[]
					{
						false,
						true
					};
                        object arg_3E7_0 = NewLateBinding.LateGet(arg_3B8_0, arg_3B8_1, arg_3B8_2, arg_3B8_3, arg_3B8_4, arg_3B8_5, array2);
                        if (array2[1])
                        {
                            this.sName = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array3[1]), typeof(string));
                        }
                        this.LogLine(Conversions.ToString(arg_3E7_0), frmMain.LogType.Info);
                        Label arg_473_0 = this.lblMainMessage;
                        object arg_43F_0 = null;
                        Type arg_43F_1 = typeof(string);
                        string arg_43F_2 = "Format";
                        array3 = new object[]
					{
						RuntimeHelpers.GetObjectValue(Language.s("{0} successfully installed")),
						this.sName
					};
                        object[] arg_43F_3 = array3;
                        string[] arg_43F_4 = null;
                        Type[] arg_43F_5 = null;
                        array2 = new bool[]
					{
						false,
						true
					};
                        object arg_46E_0 = NewLateBinding.LateGet(arg_43F_0, arg_43F_1, arg_43F_2, arg_43F_3, arg_43F_4, arg_43F_5, array2);
                        if (array2[1])
                        {
                            this.sName = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array3[1]), typeof(string));
                        }
                        arg_473_0.Text = Conversions.ToString(arg_46E_0);
                    }
                    this.timEnd.Start();
                    break;
                case frmMain.JobType.Update1:
                    this.JobExecuteScript("Update1");
                    if (Operators.CompareString(this.myIni.GetKeyValue("Info", "UseUpdate2"), "1", false) == 0)
                    {
                        if (this.Cross3Runing())
                        {
                            this.MkDir("D:\\" + this.sCompany);
                            this.MkDir("D:\\" + this.sCompany + "\\" + this.sName);
                            this.CopyFileHd(Application.ExecutablePath, string.Concat(new string[]
						{
							"D:\\",
							this.sCompany,
							"\\",
							this.sName,
							"\\Setup.exe"
						}));
                            this.CopyFileHd(Application.StartupPath + "\\Version.ini", string.Concat(new string[]
						{
							"D:\\",
							this.sCompany,
							"\\",
							this.sName,
							"\\Version.ini"
						}));
                            this.SetRunOnce(string.Concat(new string[]
						{
							"D:\\",
							this.sCompany,
							"\\",
							this.sName,
							"\\Setup.exe -install"
						}));
                            this.SetColdBoot();
                            this.LogLine(Conversions.ToString(Language.s("installation prepared")), frmMain.LogType.Info);
                            this.lblMainMessage.Text = Conversions.ToString(Language.s("Installation prepared! Please restart control PC."));
                            this.btnReboot.Visible = true;
                            this.btnOk.TextCaption = Conversions.ToString(Language.s("Later"));
                        }
                        else
                        {
                            this.Job = frmMain.JobType.Update2;
                            this.DoJob();
                        }
                    }
                    else
                    {
                        this.CreateUnReinstall();
                        this.CreateOptionRegKey();
                        if (this.iErrors == 0)
                        {
                            object arg_6D5_0 = null;
                            Type arg_6D5_1 = typeof(string);
                            string arg_6D5_2 = "Format";
                            object[] array3 = new object[]
						{
							RuntimeHelpers.GetObjectValue(Language.s("{0} successfully installed")),
							this.sName
						};
                            object[] arg_6D5_3 = array3;
                            string[] arg_6D5_4 = null;
                            Type[] arg_6D5_5 = null;
                            bool[] array2 = new bool[]
						{
							false,
							true
						};
                            object arg_704_0 = NewLateBinding.LateGet(arg_6D5_0, arg_6D5_1, arg_6D5_2, arg_6D5_3, arg_6D5_4, arg_6D5_5, array2);
                            if (array2[1])
                            {
                                this.sName = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array3[1]), typeof(string));
                            }
                            this.LogLine(Conversions.ToString(arg_704_0), frmMain.LogType.Info);
                            Label arg_790_0 = this.lblMainMessage;
                            object arg_75C_0 = null;
                            Type arg_75C_1 = typeof(string);
                            string arg_75C_2 = "Format";
                            array3 = new object[]
						{
							RuntimeHelpers.GetObjectValue(Language.s("{0} successfully installed")),
							this.sName
						};
                            object[] arg_75C_3 = array3;
                            string[] arg_75C_4 = null;
                            Type[] arg_75C_5 = null;
                            array2 = new bool[]
						{
							false,
							true
						};
                            object arg_78B_0 = NewLateBinding.LateGet(arg_75C_0, arg_75C_1, arg_75C_2, arg_75C_3, arg_75C_4, arg_75C_5, array2);
                            if (array2[1])
                            {
                                this.sName = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array3[1]), typeof(string));
                            }
                            arg_790_0.Text = Conversions.ToString(arg_78B_0);
                            this.timEnd.Start();
                        }
                    }
                    break;
                case frmMain.JobType.Update2:
                    this.JobExecuteScript("Update2");
                    this.CreateUnReinstall();
                    this.CreateOptionRegKey();
                    if (this.iErrors == 0)
                    {
                        object arg_810_0 = null;
                        Type arg_810_1 = typeof(string);
                        string arg_810_2 = "Format";
                        object[] array3 = new object[]
					{
						RuntimeHelpers.GetObjectValue(Language.s("{0} successfully installed")),
						this.sName
					};
                        object[] arg_810_3 = array3;
                        string[] arg_810_4 = null;
                        Type[] arg_810_5 = null;
                        bool[] array2 = new bool[]
					{
						false,
						true
					};
                        object arg_83F_0 = NewLateBinding.LateGet(arg_810_0, arg_810_1, arg_810_2, arg_810_3, arg_810_4, arg_810_5, array2);
                        if (array2[1])
                        {
                            this.sName = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array3[1]), typeof(string));
                        }
                        this.LogLine(Conversions.ToString(arg_83F_0), frmMain.LogType.Info);
                        Label arg_8CB_0 = this.lblMainMessage;
                        object arg_897_0 = null;
                        Type arg_897_1 = typeof(string);
                        string arg_897_2 = "Format";
                        array3 = new object[]
					{
						RuntimeHelpers.GetObjectValue(Language.s("{0} successfully installed")),
						this.sName
					};
                        object[] arg_897_3 = array3;
                        string[] arg_897_4 = null;
                        Type[] arg_897_5 = null;
                        array2 = new bool[]
					{
						false,
						true
					};
                        object arg_8C6_0 = NewLateBinding.LateGet(arg_897_0, arg_897_1, arg_897_2, arg_897_3, arg_897_4, arg_897_5, array2);
                        if (array2[1])
                        {
                            this.sName = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array3[1]), typeof(string));
                        }
                        arg_8CB_0.Text = Conversions.ToString(arg_8C6_0);
                    }
                    this.timEnd.Start();
                    break;
                case frmMain.JobType.Reinstall1:
                    this.JobExecuteScript("Reinstall1");
                    if (Operators.CompareString(this.myIni.GetKeyValue("Info", "UseReinstall2"), "1", false) == 0)
                    {
                        if (this.Cross3Runing())
                        {
                            this.MkDir("D:\\" + this.sCompany);
                            this.MkDir("D:\\" + this.sCompany + "\\" + this.sName);
                            this.CopyFileHd(Application.ExecutablePath, string.Concat(new string[]
						{
							"D:\\",
							this.sCompany,
							"\\",
							this.sName,
							"\\Setup.exe"
						}));
                            this.CopyFileHd(Application.StartupPath + "\\Version.ini", string.Concat(new string[]
						{
							"D:\\",
							this.sCompany,
							"\\",
							this.sName,
							"\\Version.ini"
						}));
                            this.SetRunOnce(string.Concat(new string[]
						{
							"D:\\",
							this.sCompany,
							"\\",
							this.sName,
							"\\Setup.exe -install"
						}));
                            this.SetColdBoot();
                            this.LogLine(Conversions.ToString(Language.s("installation prepared")), frmMain.LogType.Info);
                            this.lblMainMessage.Text = Conversions.ToString(Language.s("Installation prepared! Please restart control PC."));
                            this.btnReboot.Visible = true;
                            this.btnOk.TextCaption = Conversions.ToString(Language.s("Later"));
                        }
                        else
                        {
                            this.Job = frmMain.JobType.Reinstall2;
                            this.DoJob();
                        }
                    }
                    else
                    {
                        this.CreateOptionRegKey();
                        if (this.iErrors == 0)
                        {
                            object arg_B27_0 = null;
                            Type arg_B27_1 = typeof(string);
                            string arg_B27_2 = "Format";
                            object[] array3 = new object[]
						{
							RuntimeHelpers.GetObjectValue(Language.s("{0} successfully installed")),
							this.sName
						};
                            object[] arg_B27_3 = array3;
                            string[] arg_B27_4 = null;
                            Type[] arg_B27_5 = null;
                            bool[] array2 = new bool[]
						{
							false,
							true
						};
                            object arg_B56_0 = NewLateBinding.LateGet(arg_B27_0, arg_B27_1, arg_B27_2, arg_B27_3, arg_B27_4, arg_B27_5, array2);
                            if (array2[1])
                            {
                                this.sName = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array3[1]), typeof(string));
                            }
                            this.LogLine(Conversions.ToString(arg_B56_0), frmMain.LogType.Info);
                            Label arg_BE2_0 = this.lblMainMessage;
                            object arg_BAE_0 = null;
                            Type arg_BAE_1 = typeof(string);
                            string arg_BAE_2 = "Format";
                            array3 = new object[]
						{
							RuntimeHelpers.GetObjectValue(Language.s("{0} successfully installed")),
							this.sName
						};
                            object[] arg_BAE_3 = array3;
                            string[] arg_BAE_4 = null;
                            Type[] arg_BAE_5 = null;
                            array2 = new bool[]
						{
							false,
							true
						};
                            object arg_BDD_0 = NewLateBinding.LateGet(arg_BAE_0, arg_BAE_1, arg_BAE_2, arg_BAE_3, arg_BAE_4, arg_BAE_5, array2);
                            if (array2[1])
                            {
                                this.sName = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array3[1]), typeof(string));
                            }
                            arg_BE2_0.Text = Conversions.ToString(arg_BDD_0);
                            this.timEnd.Start();
                        }
                    }
                    break;
                case frmMain.JobType.Reinstall2:
                    this.JobExecuteScript("Reinstall2");
                    this.CreateOptionRegKey();
                    if (this.iErrors == 0)
                    {
                        object arg_C5C_0 = null;
                        Type arg_C5C_1 = typeof(string);
                        string arg_C5C_2 = "Format";
                        object[] array3 = new object[]
					{
						RuntimeHelpers.GetObjectValue(Language.s("{0} successfully installed")),
						this.sName
					};
                        object[] arg_C5C_3 = array3;
                        string[] arg_C5C_4 = null;
                        Type[] arg_C5C_5 = null;
                        bool[] array2 = new bool[]
					{
						false,
						true
					};
                        object arg_C8B_0 = NewLateBinding.LateGet(arg_C5C_0, arg_C5C_1, arg_C5C_2, arg_C5C_3, arg_C5C_4, arg_C5C_5, array2);
                        if (array2[1])
                        {
                            this.sName = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array3[1]), typeof(string));
                        }
                        this.LogLine(Conversions.ToString(arg_C8B_0), frmMain.LogType.Info);
                        Label arg_D17_0 = this.lblMainMessage;
                        object arg_CE3_0 = null;
                        Type arg_CE3_1 = typeof(string);
                        string arg_CE3_2 = "Format";
                        array3 = new object[]
					{
						RuntimeHelpers.GetObjectValue(Language.s("{0} successfully installed")),
						this.sName
					};
                        object[] arg_CE3_3 = array3;
                        string[] arg_CE3_4 = null;
                        Type[] arg_CE3_5 = null;
                        array2 = new bool[]
					{
						false,
						true
					};
                        object arg_D12_0 = NewLateBinding.LateGet(arg_CE3_0, arg_CE3_1, arg_CE3_2, arg_CE3_3, arg_CE3_4, arg_CE3_5, array2);
                        if (array2[1])
                        {
                            this.sName = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array3[1]), typeof(string));
                        }
                        arg_D17_0.Text = Conversions.ToString(arg_D12_0);
                    }
                    this.timEnd.Start();
                    break;
                case frmMain.JobType.UnInstall1:
                    this.JobExecuteScript("Uninstall1");
                    if (Operators.CompareString(this.myIni.GetKeyValue("Info", "UseUninstall2"), "1", false) == 0)
                    {
                        if (this.Cross3Runing())
                        {
                            this.SetRunOnce("C:\\KRC_OPTION\\" + this.sName + "\\UNINST\\UnInstall.exe -uninstall");
                            this.SetColdBoot();
                            this.LogLine(Conversions.ToString(Language.s("uninstallation prepared")), frmMain.LogType.Info);
                            this.lblMainMessage.Text = Conversions.ToString(Language.s("Uninstallation prepared! Please restart control PC."));
                            this.btnReboot.Visible = true;
                            this.btnOk.TextCaption = Conversions.ToString(Language.s("Later"));
                        }
                        else
                        {
                            this.Job = frmMain.JobType.Uninstall2;
                            this.DoJob();
                        }
                    }
                    else
                    {
                        this.DeleteOptionRegKey();
                        if (this.iErrors == 0)
                        {
                            object arg_E50_0 = null;
                            Type arg_E50_1 = typeof(string);
                            string arg_E50_2 = "Format";
                            object[] array3 = new object[]
						{
							RuntimeHelpers.GetObjectValue(Language.s("{0} successfully uninstalled")),
							this.sName
						};
                            object[] arg_E50_3 = array3;
                            string[] arg_E50_4 = null;
                            Type[] arg_E50_5 = null;
                            bool[] array2 = new bool[]
						{
							false,
							true
						};
                            object arg_E7F_0 = NewLateBinding.LateGet(arg_E50_0, arg_E50_1, arg_E50_2, arg_E50_3, arg_E50_4, arg_E50_5, array2);
                            if (array2[1])
                            {
                                this.sName = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array3[1]), typeof(string));
                            }
                            this.LogLine(Conversions.ToString(arg_E7F_0), frmMain.LogType.Info);
                            Label arg_F0B_0 = this.lblMainMessage;
                            object arg_ED7_0 = null;
                            Type arg_ED7_1 = typeof(string);
                            string arg_ED7_2 = "Format";
                            array3 = new object[]
						{
							RuntimeHelpers.GetObjectValue(Language.s("{0} successfully uninstalled")),
							this.sName
						};
                            object[] arg_ED7_3 = array3;
                            string[] arg_ED7_4 = null;
                            Type[] arg_ED7_5 = null;
                            array2 = new bool[]
						{
							false,
							true
						};
                            object arg_F06_0 = NewLateBinding.LateGet(arg_ED7_0, arg_ED7_1, arg_ED7_2, arg_ED7_3, arg_ED7_4, arg_ED7_5, array2);
                            if (array2[1])
                            {
                                this.sName = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array3[1]), typeof(string));
                            }
                            arg_F0B_0.Text = Conversions.ToString(arg_F06_0);
                            this.timEnd.Start();
                        }
                    }
                    break;
                case frmMain.JobType.Uninstall2:
                    this.JobExecuteScript("Uninstall2");
                    this.DeleteOptionRegKey();
                    if (this.iErrors == 0)
                    {
                        object arg_F85_0 = null;
                        Type arg_F85_1 = typeof(string);
                        string arg_F85_2 = "Format";
                        object[] array3 = new object[]
					{
						RuntimeHelpers.GetObjectValue(Language.s("{0} successfully uninstalled")),
						this.sName
					};
                        object[] arg_F85_3 = array3;
                        string[] arg_F85_4 = null;
                        Type[] arg_F85_5 = null;
                        bool[] array2 = new bool[]
					{
						false,
						true
					};
                        object arg_FB4_0 = NewLateBinding.LateGet(arg_F85_0, arg_F85_1, arg_F85_2, arg_F85_3, arg_F85_4, arg_F85_5, array2);
                        if (array2[1])
                        {
                            this.sName = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array3[1]), typeof(string));
                        }
                        this.LogLine(Conversions.ToString(arg_FB4_0), frmMain.LogType.Info);
                        Label arg_1040_0 = this.lblMainMessage;
                        object arg_100C_0 = null;
                        Type arg_100C_1 = typeof(string);
                        string arg_100C_2 = "Format";
                        array3 = new object[]
					{
						RuntimeHelpers.GetObjectValue(Language.s("{0} successfully uninstalled")),
						this.sName
					};
                        object[] arg_100C_3 = array3;
                        string[] arg_100C_4 = null;
                        Type[] arg_100C_5 = null;
                        array2 = new bool[]
					{
						false,
						true
					};
                        object arg_103B_0 = NewLateBinding.LateGet(arg_100C_0, arg_100C_1, arg_100C_2, arg_100C_3, arg_100C_4, arg_100C_5, array2);
                        if (array2[1])
                        {
                            this.sName = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array3[1]), typeof(string));
                        }
                        arg_1040_0.Text = Conversions.ToString(arg_103B_0);
                    }
                    this.timEnd.Start();
                    break;
            }
        }
        private void JobExecuteScript(string sJob)
        {
            object arg_43_0 = null;
            Type arg_43_1 = typeof(string);
            string arg_43_2 = "Format";
            object[] array = new object[]
			{
				RuntimeHelpers.GetObjectValue(Language.s("execute {0}")),
				sJob
			};
            object[] arg_43_3 = array;
            string[] arg_43_4 = null;
            Type[] arg_43_5 = null;
            bool[] array2 = new bool[]
			{
				false,
				true
			};
            object arg_6E_0 = NewLateBinding.LateGet(arg_43_0, arg_43_1, arg_43_2, arg_43_3, arg_43_4, arg_43_5, array2);
            if (array2[1])
            {
                sJob = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array[1]), typeof(string));
            }
            this.LogLine(Conversions.ToString(arg_6E_0), frmMain.LogType.Info);
            int arg_91_0 = 1;
            int count = this.myIni.GetSection(sJob).Keys.Count;
            checked
            {
                for (int i = arg_91_0; i <= count; i++)
                {
                    string[] array3 = Strings.Split(this.myIni.GetKeyValue(sJob, i.ToString()), ",", -1, CompareMethod.Binary);
                    string left = array3[0].ToLower();
                    if (Operators.CompareString(left, "copy", false) == 0)
                    {
                        this.CopyFile(array3[1], array3[2]);
                    }
                    else
                    {
                        if (Operators.CompareString(left, "copyhd", false) == 0)
                        {
                            this.CopyFileHd(array3[1], array3[2]);
                        }
                        else
                        {
                            if (Operators.CompareString(left, "delete", false) == 0)
                            {
                                this.DeleteFile(array3[1]);
                            }
                            else
                            {
                                if (Operators.CompareString(left, "stopsmarthmi", false) == 0)
                                {
                                    this.SmartHMIwasRuning = this.SmartHMIRuning();
                                    this.SmartHMIKill();
                                }
                                else
                                {
                                    if (Operators.CompareString(left, "startsmarthmi", false) == 0)
                                    {
                                        if (this.SmartHMIwasRuning)
                                        {
                                            this.SmartHMIStart();
                                        }
                                    }
                                    else
                                    {
                                        if (Operators.CompareString(left, "mkdir", false) == 0)
                                        {
                                            this.MkDir(array3[1]);
                                        }
                                        else
                                        {
                                            if (Operators.CompareString(left, "deldir", false) == 0)
                                            {
                                                this.DelDir(array3[1]);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void LoadSetupIni()
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                using (ZipFile zipFile = ZipFile.Read(Resources.setup))
                {
                    zipFile["setup.ini"].Extract(memoryStream);
                }
                memoryStream.Position = 0L;
                this.myIni.Load(memoryStream);
                this.sCompany = this.myIni.GetKeyValue("Info", "Company");
                this.sName = this.myIni.GetKeyValue("Info", "Name");
                this.sVersion = this.myIni.GetKeyValue("Info", "Version");
                this.sBuild = this.myIni.GetKeyValue("Info", "Build");
                this.sDll = "C:\\KRC_Option\\" + this.sName + "\\KrcTech.dll";
                this.lblProduct.Text = this.sName + " " + this.sVersion;
                this.lblCompany.Text = this.sCompany;
            }
            catch (Exception expr_106)
            {
                ProjectData.SetProjectError(expr_106);
                this.LogExeption();
                ProjectData.ClearProjectError();
            }
        }
        private string GetLicenseText()
        {
            string result;
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                using (ZipFile zipFile = ZipFile.Read(Resources.setup))
                {
                    zipFile["license_" + Language.LanguageAct + ".rtf"].Extract(memoryStream);
                }
                memoryStream.Position = 0L;
                StreamReader streamReader = new StreamReader(memoryStream);
                result = streamReader.ReadToEnd();
            }
            catch (Exception expr_5E)
            {
                ProjectData.SetProjectError(expr_5E);
                this.LogExeption();
                result = "";
                ProjectData.ClearProjectError();
            }
            return result;
        }
        private void LogInit()
        {
            try
            {
                this.strLogFile = string.Concat(new string[]
				{
					"C:\\KRC\\ROBOTER\\LOG\\Setup_",
					this.sCompany,
					this.sName,
					this.sVersion,
					".log"
				});
                File.WriteAllText(this.ProductName, "");
            }
            catch (Exception expr_67)
            {
                ProjectData.SetProjectError(expr_67);
                this.LogLine("error while creating Log file", frmMain.LogType.Info);
                ProjectData.ClearProjectError();
            }
        }
        private void LogLine(string strMsg, frmMain.LogType LogType = frmMain.LogType.Info)
        {
            checked
            {
                try
                {
                    string text = Conversions.ToString(DateAndTime.TimeOfDay) + " ";
                    switch (LogType)
                    {
                        case frmMain.LogType.Info:
                            text += ".";
                            break;
                        case frmMain.LogType.Warning:
                            text += "!";
                            this.iWarnings++;
                            break;
                        case frmMain.LogType.Exception:
                            text += "#";
                            this.iErrors++;
                            break;
                    }
                    text += strMsg;
                    TextBox txtLog = this.txtLog;
                    txtLog.Text = txtLog.Text + text + "\r\n";
                    this.txtLog.Select(this.txtLog.TextLength, 0);
                    this.txtLog.ScrollToCaret();
                    Application.DoEvents();
                    StreamWriter streamWriter = File.AppendText(this.strLogFile);
                    streamWriter.WriteLine(text);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                catch (Exception expr_E7)
                {
                    ProjectData.SetProjectError(expr_E7);
                    ProjectData.ClearProjectError();
                }
            }
        }
        private void LogExeption()
        {
            try
            {
                StackTrace stackTrace = new StackTrace();
                this.LogLine(Information.Err().Description + "\" in " + stackTrace.GetFrame(1).GetMethod().Name, frmMain.LogType.Exception);
            }
            catch (Exception expr_37)
            {
                ProjectData.SetProjectError(expr_37);
                ProjectData.ClearProjectError();
            }
        }
        private bool Cross3Runing()
        {
            bool result;
            try
            {
                Process[] processesByName = Process.GetProcessesByName("Cross3");
                int num = 0;
                if (num >= processesByName.Length)
                {
                    result = false;
                }
                else
                {
                    Process process = processesByName[num];
                    result = true;
                }
            }
            catch (Exception expr_2E)
            {
                ProjectData.SetProjectError(expr_2E);
                result = false;
                ProjectData.ClearProjectError();
            }
            return result;
        }
        private bool SmartHMIRuning()
        {
            bool result;
            try
            {
                Process[] processesByName = Process.GetProcessesByName("SmartHMI");
                int num = 0;
                if (num >= processesByName.Length)
                {
                    result = false;
                }
                else
                {
                    Process process = processesByName[num];
                    result = true;
                }
            }
            catch (Exception expr_2E)
            {
                ProjectData.SetProjectError(expr_2E);
                result = false;
                ProjectData.ClearProjectError();
            }
            return result;
        }
        private void SmartHMIKill()
        {
            checked
            {
                try
                {
                    this.LogLine(Conversions.ToString(Language.s("stopping SmartHMI")), frmMain.LogType.Info);
                    Process[] processesByName = Process.GetProcessesByName("SmartHMI");
                    for (int i = 0; i < processesByName.Length; i++)
                    {
                        Process process = processesByName[i];
                        process.Kill();
                        process.WaitForExit(10000);
                    }
                }
                catch (Exception expr_51)
                {
                    ProjectData.SetProjectError(expr_51);
                    this.LogExeption();
                    ProjectData.ClearProjectError();
                }
            }
        }
        private void SmartHMIStart()
        {
            try
            {
                this.LogLine(Conversions.ToString(Language.s("starting SmartHMI")), frmMain.LogType.Info);
                ProcessStartInfo startInfo = new ProcessStartInfo("C:\\KRC\\SmartHmi\\SmartHMI.exe");
                Process.Start(startInfo);
            }
            catch (Exception expr_2D)
            {
                ProjectData.SetProjectError(expr_2D);
                this.LogExeption();
                ProjectData.ClearProjectError();
            }
        }
        private bool SmartHMIExist()
        {
            bool result;
            try
            {
                result = File.Exists("C:\\KRC\\SmartHMI\\SmartHMI.exe");
            }
            catch (Exception expr_10)
            {
                ProjectData.SetProjectError(expr_10);
                result = false;
                ProjectData.ClearProjectError();
            }
            return result;
        }
        private void CreateOptionRegKey()
        {
            checked
            {
                try
                {
                    this.LogLine(Conversions.ToString(Language.s("updating registry information")), frmMain.LogType.Info);
                    try
                    {
                        RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("KUKA Roboter GmbH").OpenSubKey("Options", true);
                        registryKey.DeleteSubKey(this.sName, false);
                        registryKey.Close();
                    }
                    catch (Exception expr_53)
                    {
                        ProjectData.SetProjectError(expr_53);
                        ProjectData.ClearProjectError();
                    }
                    RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\KUKA Roboter GmbH");
                    string[] subKeyNames = registryKey2.GetSubKeyNames();
                    bool flag = false;
                    string[] array = subKeyNames;
                    for (int i = 0; i < array.Length; i++)
                    {
                        string text = array[i];
                        if (Operators.CompareString(text.ToUpper(), "OPTIONS", false) == 0)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        registryKey2 = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("KUKA Roboter GmbH", true);
                        registryKey2.CreateSubKey("Options");
                    }
                    registryKey2 = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("KUKA Roboter GmbH").OpenSubKey("Options", true);
                    registryKey2.CreateSubKey(this.sName);
                    registryKey2 = registryKey2.OpenSubKey(this.sName, true);
                    registryKey2.SetValue("Name", this.sCompany + "." + this.sName.ToString());
                    registryKey2.SetValue("Version", this.sVersion.ToString());
                    registryKey2.SetValue("Build", this.sBuild.ToString());
                    registryKey2.SetValue("DLL", this.sDll.ToString());
                    registryKey2.Close();
                }
                catch (Exception expr_1A3)
                {
                    ProjectData.SetProjectError(expr_1A3);
                    this.LogExeption();
                    ProjectData.ClearProjectError();
                }
            }
        }
        private void DeleteOptionRegKey()
        {
            try
            {
                this.LogLine(Conversions.ToString(Language.s("updating registry information")), frmMain.LogType.Info);
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("KUKA Roboter GmbH").OpenSubKey("Options", true);
                registryKey.DeleteSubKey(this.sName, false);
                registryKey.Close();
            }
            catch (Exception expr_53)
            {
                ProjectData.SetProjectError(expr_53);
                this.LogExeption();
                ProjectData.ClearProjectError();
            }
        }
        private string GetLanguageReg()
        {
            string result;
            try
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("KUKA Roboter GmbH").OpenSubKey("RobotData", false);
                string text = registryKey.GetValue("LanguageID").ToString();
                registryKey.Close();
                string left = text;
                if (Operators.CompareString(left, "1033", false) == 0)
                {
                    result = "en";
                }
                else
                {
                    if (Operators.CompareString(left, "1031", false) == 0)
                    {
                        result = "de";
                    }
                    else
                    {
                        result = "en";
                    }
                }
            }
            catch (Exception expr_83)
            {
                ProjectData.SetProjectError(expr_83);
                result = "en";
                ProjectData.ClearProjectError();
            }
            return result;
        }
        private string GetKrcVersionReg()
        {
            string result;
            try
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("KUKA Roboter GmbH").OpenSubKey("Version", false);
                string text = registryKey.GetValue("Version").ToString();
                registryKey.Close();
                object arg_83_0 = null;
                Type arg_83_1 = typeof(string);
                string arg_83_2 = "Format";
                object[] array = new object[]
				{
					RuntimeHelpers.GetObjectValue(Language.s("current KRC version is {0}")),
					text
				};
                object[] arg_83_3 = array;
                string[] arg_83_4 = null;
                Type[] arg_83_5 = null;
                bool[] array2 = new bool[]
				{
					false,
					true
				};
                object arg_AE_0 = NewLateBinding.LateGet(arg_83_0, arg_83_1, arg_83_2, arg_83_3, arg_83_4, arg_83_5, array2);
                if (array2[1])
                {
                    text = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array[1]), typeof(string));
                }
                this.LogLine(Conversions.ToString(arg_AE_0), frmMain.LogType.Info);
                result = text;
            }
            catch (Exception expr_C1)
            {
                ProjectData.SetProjectError(expr_C1);
                this.LogExeption();
                result = "";
                ProjectData.ClearProjectError();
            }
            return result;
        }
        private void DeleteFile(string File1)
        {
            checked
            {
                try
                {
                    if (File1.Contains("*"))
                    {
                        string fileName = Path.GetFileName(File1);
                        string[] files = Directory.GetFiles(Path.GetDirectoryName(File1), fileName, SearchOption.TopDirectoryOnly);
                        for (int i = 0; i < files.Length; i++)
                        {
                            string path = files[i];
                            this.LogLine(Conversions.ToString(NewLateBinding.LateGet(null, typeof(string), "Format", new object[]
							{
								RuntimeHelpers.GetObjectValue(Language.s("delete file {0}")),
								Path.GetFileName(path)
							}, null, null, null)), frmMain.LogType.Info);
                            File.Delete(path);
                        }
                    }
                    else
                    {
                        this.LogLine(Conversions.ToString(NewLateBinding.LateGet(null, typeof(string), "Format", new object[]
						{
							RuntimeHelpers.GetObjectValue(Language.s("delete file {0}")),
							Path.GetFileName(File1)
						}, null, null, null)), frmMain.LogType.Info);
                        if (!File.Exists(File1))
                        {
                            this.LogLine(Conversions.ToString(NewLateBinding.LateGet(null, typeof(string), "Format", new object[]
							{
								RuntimeHelpers.GetObjectValue(Language.s("file {0} does not exist")),
								Path.GetFileName(File1)
							}, null, null, null)), frmMain.LogType.Warning);
                        }
                        else
                        {
                            File.Delete(File1);
                        }
                    }
                }
                catch (Exception expr_145)
                {
                    ProjectData.SetProjectError(expr_145);
                    this.LogExeption();
                    ProjectData.ClearProjectError();
                }
            }
        }
        private void CopyFile(string File1, string File2)
        {
            try
            {
                object arg_43_0 = null;
                Type arg_43_1 = typeof(string);
                string arg_43_2 = "Format";
                object[] array = new object[]
				{
					RuntimeHelpers.GetObjectValue(Language.s("copy file {0}")),
					File1
				};
                object[] arg_43_3 = array;
                string[] arg_43_4 = null;
                Type[] arg_43_5 = null;
                bool[] array2 = new bool[]
				{
					false,
					true
				};
                object arg_6E_0 = NewLateBinding.LateGet(arg_43_0, arg_43_1, arg_43_2, arg_43_3, arg_43_4, arg_43_5, array2);
                if (array2[1])
                {
                    File1 = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array[1]), typeof(string));
                }
                this.LogLine(Conversions.ToString(arg_6E_0), frmMain.LogType.Info);
                MemoryStream memoryStream = new MemoryStream();
                using (ZipFile zipFile = ZipFile.Read(Resources.setup))
                {
                    zipFile[File1].Extract(memoryStream);
                }
                memoryStream.Position = 0L;
                StreamWriter streamWriter = new StreamWriter(File2, false);
                memoryStream.WriteTo(streamWriter.BaseStream);
                streamWriter.Close();
            }
            catch (Exception expr_D7)
            {
                ProjectData.SetProjectError(expr_D7);
                this.LogExeption();
                ProjectData.ClearProjectError();
            }
        }
        private void CopyFileHd(string File1, string File2)
        {
            checked
            {
                try
                {
                    if (File1.Contains("*"))
                    {
                        string fileName = Path.GetFileName(File1);
                        object arg_5A_0 = null;
                        Type arg_5A_1 = typeof(string);
                        string arg_5A_2 = "Format";
                        object[] array = new object[]
						{
							RuntimeHelpers.GetObjectValue(Language.s("copy files of directory {0}")),
							File1
						};
                        object[] arg_5A_3 = array;
                        string[] arg_5A_4 = null;
                        Type[] arg_5A_5 = null;
                        bool[] array2 = new bool[]
						{
							false,
							true
						};
                        object arg_85_0 = NewLateBinding.LateGet(arg_5A_0, arg_5A_1, arg_5A_2, arg_5A_3, arg_5A_4, arg_5A_5, array2);
                        if (array2[1])
                        {
                            File1 = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array[1]), typeof(string));
                        }
                        this.LogLine(Conversions.ToString(arg_85_0), frmMain.LogType.Info);
                        if (Directory.Exists(Path.GetDirectoryName(File1)) && Directory.Exists(Path.GetDirectoryName(File2)))
                        {
                            string[] files = Directory.GetFiles(Path.GetDirectoryName(File1), fileName, SearchOption.TopDirectoryOnly);
                            for (int i = 0; i < files.Length; i++)
                            {
                                string text = files[i];
                                this.CopyFileHd(text, File2 + Path.GetFileName(text));
                            }
                        }
                        else
                        {
                            this.LogLine(string.Format(Conversions.ToString(Language.s("directory does not exist")), new object[0]), frmMain.LogType.Info);
                        }
                    }
                    else
                    {
                        if (Operators.CompareString(File1.ToLower(), File2.ToLower(), false) != 0)
                        {
                            this.LogLine(Conversions.ToString(NewLateBinding.LateGet(null, typeof(string), "Format", new object[]
							{
								RuntimeHelpers.GetObjectValue(Language.s("copy file {0}")),
								Path.GetFileName(File1)
							}, null, null, null)), frmMain.LogType.Info);
                            if (!File.Exists(File1))
                            {
                                this.LogLine(Conversions.ToString(NewLateBinding.LateGet(null, typeof(string), "Format", new object[]
								{
									RuntimeHelpers.GetObjectValue(Language.s("file {0} does not exist")),
									Path.GetFileName(File1)
								}, null, null, null)), frmMain.LogType.Warning);
                            }
                            else
                            {
                                File.Copy(File1, File2, true);
                            }
                        }
                    }
                }
                catch (Exception expr_1E8)
                {
                    ProjectData.SetProjectError(expr_1E8);
                    this.LogExeption();
                    ProjectData.ClearProjectError();
                }
            }
        }
        private void MkDir(string Dir)
        {
            try
            {
                object arg_43_0 = null;
                Type arg_43_1 = typeof(string);
                string arg_43_2 = "Format";
                object[] array = new object[]
				{
					RuntimeHelpers.GetObjectValue(Language.s("make directory {0}")),
					Dir
				};
                object[] arg_43_3 = array;
                string[] arg_43_4 = null;
                Type[] arg_43_5 = null;
                bool[] array2 = new bool[]
				{
					false,
					true
				};
                object arg_6E_0 = NewLateBinding.LateGet(arg_43_0, arg_43_1, arg_43_2, arg_43_3, arg_43_4, arg_43_5, array2);
                if (array2[1])
                {
                    Dir = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array[1]), typeof(string));
                }
                this.LogLine(Conversions.ToString(arg_6E_0), frmMain.LogType.Info);
                if (Directory.Exists(Dir))
                {
                    this.LogLine(Conversions.ToString(NewLateBinding.LateGet(null, typeof(string), "Format", new object[]
					{
						RuntimeHelpers.GetObjectValue(Language.s("directory {0} exists already")),
						Path.GetFileName(Dir)
					}, null, null, null)), frmMain.LogType.Info);
                }
                else
                {
                    Directory.CreateDirectory(Dir);
                }
            }
            catch (Exception expr_DC)
            {
                ProjectData.SetProjectError(expr_DC);
                this.LogExeption();
                ProjectData.ClearProjectError();
            }
        }
        private void DelDir(string Dir)
        {
            try
            {
                object arg_43_0 = null;
                Type arg_43_1 = typeof(string);
                string arg_43_2 = "Format";
                object[] array = new object[]
				{
					RuntimeHelpers.GetObjectValue(Language.s("delete directory {0}")),
					Dir
				};
                object[] arg_43_3 = array;
                string[] arg_43_4 = null;
                Type[] arg_43_5 = null;
                bool[] array2 = new bool[]
				{
					false,
					true
				};
                object arg_6E_0 = NewLateBinding.LateGet(arg_43_0, arg_43_1, arg_43_2, arg_43_3, arg_43_4, arg_43_5, array2);
                if (array2[1])
                {
                    Dir = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array[1]), typeof(string));
                }
                this.LogLine(Conversions.ToString(arg_6E_0), frmMain.LogType.Info);
                if (!Directory.Exists(Dir))
                {
                    this.LogLine(Conversions.ToString(NewLateBinding.LateGet(null, typeof(string), "Format", new object[]
					{
						RuntimeHelpers.GetObjectValue(Language.s("directory {0} does not exist")),
						Path.GetFileName(Dir)
					}, null, null, null)), frmMain.LogType.Info);
                }
                else
                {
                    Directory.Delete(Dir, true);
                }
            }
            catch (Exception expr_DC)
            {
                ProjectData.SetProjectError(expr_DC);
                this.LogExeption();
                ProjectData.ClearProjectError();
            }
        }
        private void SetColdBoot()
        {
            try
            {
                this.LogLine(Conversions.ToString(Language.s("set reload files")), frmMain.LogType.Info);
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM").OpenSubKey("CurrentControlSet").OpenSubKey("Enum").OpenSubKey("Root").OpenSubKey("SYSTEM").OpenSubKey("0003").OpenSubKey("Device Parameters").OpenSubKey("FastStart", true);
                registryKey.SetValue("Options", 0);
                registryKey.Close();
            }
            catch (Exception expr_89)
            {
                ProjectData.SetProjectError(expr_89);
                this.LogExeption();
                ProjectData.ClearProjectError();
            }
        }
        private void SetRunOnce(string Path)
        {
            try
            {
                this.LogLine(Conversions.ToString(Language.s("set auto run for nex system start")), frmMain.LogType.Info);
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("Microsoft").OpenSubKey("Windows").OpenSubKey("CurrentVersion").OpenSubKey("RunOnce", true);
                registryKey.SetValue("OrangeApps" + this.sName, Path);
                registryKey.Close();
            }
            catch (Exception expr_71)
            {
                ProjectData.SetProjectError(expr_71);
                this.LogExeption();
                ProjectData.ClearProjectError();
            }
        }
        private int CheckVersionInstalled()
        {
            int result;
            try
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("KUKA Roboter GmbH").OpenSubKey("Options").OpenSubKey(this.sName, false);
                string text = registryKey.GetValue("Version").ToString();
                registryKey.Close();
                string[] array = text.Replace("V", "").Split(new char[]
				{
					'.'
				});
                string[] array2 = this.sVersion.Replace("V", "").Split(new char[]
				{
					'.'
				});
                object arg_E0_0 = null;
                Type arg_E0_1 = typeof(string);
                string arg_E0_2 = "Format";
                object[] array3 = new object[]
				{
					RuntimeHelpers.GetObjectValue(Language.s("version {0} already installed")),
					text
				};
                object[] arg_E0_3 = array3;
                string[] arg_E0_4 = null;
                Type[] arg_E0_5 = null;
                bool[] array4 = new bool[]
				{
					false,
					true
				};
                object arg_10C_0 = NewLateBinding.LateGet(arg_E0_0, arg_E0_1, arg_E0_2, arg_E0_3, arg_E0_4, arg_E0_5, array4);
                if (array4[1])
                {
                    text = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array3[1]), typeof(string));
                }
                this.LogLine(Conversions.ToString(arg_10C_0), frmMain.LogType.Info);
                if (Conversions.ToInteger(array[0]) > Conversions.ToInteger(array2[0]))
                {
                    result = -1;
                }
                else
                {
                    if (Conversions.ToInteger(array[0]) < Conversions.ToInteger(array2[0]))
                    {
                        result = 1;
                    }
                    else
                    {
                        if (Conversions.ToInteger(array[1]) > Conversions.ToInteger(array2[1]))
                        {
                            result = -1;
                        }
                        else
                        {
                            if (Conversions.ToInteger(array[1]) < Conversions.ToInteger(array2[1]))
                            {
                                result = 1;
                            }
                            else
                            {
                                if (Conversions.ToInteger(array[2]) > Conversions.ToInteger(array2[2]))
                                {
                                    result = -1;
                                }
                                else
                                {
                                    if (Conversions.ToInteger(array[2]) < Conversions.ToInteger(array2[2]))
                                    {
                                        result = 1;
                                    }
                                    else
                                    {
                                        result = 0;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception expr_203)
            {
                ProjectData.SetProjectError(expr_203);
                result = -2;
                ProjectData.ClearProjectError();
            }
            return result;
        }
        private void CreateUnReinstall()
        {
            this.LogLine(Conversions.ToString(Language.s("creating uninstall and reinstall information")), frmMain.LogType.Info);
            this.MkDir("C:\\KRC_OPTION\\" + this.sName);
            this.MkDir("C:\\KRC_OPTION\\" + this.sName + "\\UNINST");
            this.MkDir("C:\\KRC_OPTION\\" + this.sName + "\\REINST");
            this.CopyFileHd(Application.ExecutablePath, "C:\\KRC_OPTION\\" + this.sName + "\\UNINST\\UnInstall.exe");
            this.CopyFileHd(Application.StartupPath + "\\Version.ini", "C:\\KRC_OPTION\\" + this.sName + "\\UNINST\\Version.ini");
            this.CopyFileHd(Application.ExecutablePath, "C:\\KRC_OPTION\\" + this.sName + "\\REINST\\ReInstall.exe");
            this.CopyFileHd(Application.StartupPath + "\\Version.ini", "C:\\KRC_OPTION\\" + this.sName + "\\REINST\\Version.ini");
            byte[] krcTech = Resources.KrcTech;
            this.CopyResourceFile(ref krcTech, "C:\\KRC_OPTION\\" + this.sName + "\\KrcTech.dll");
        }
        private void CopyResourceFile(ref byte[] Res, string Path)
        {
            try
            {
                FileStream fileStream = new FileStream(Path, FileMode.Create);
                fileStream.Write(Res, 0, Res.Length);
                fileStream.Close();
            }
            catch (Exception expr_20)
            {
                ProjectData.SetProjectError(expr_20);
                this.LogExeption();
                ProjectData.ClearProjectError();
            }
        }
        private void btnAccept_ButtonCheckedChanged()
        {
            this.btnContinue.Enabled = this.btnAccept.Checked;
        }
        private void btnCancel_ButtonClick()
        {
            this.Close();
            ProjectData.EndApp();
        }
        private void btnContinue_ButtonClick()
        {
            this.pnl1.BringToFront();
            this.timDoJob.Start();
        }
        private void timDoJob_Tick(object sender, EventArgs e)
        {
            this.timDoJob.Stop();
            this.DoJob();
            if (this.iWarnings > 0)
            {
                this.LogLine(string.Format(Conversions.ToString(Language.s("{0} warnings")), this.iWarnings), frmMain.LogType.Info);
            }
            if (this.iErrors > 0)
            {
                this.LogLine(string.Format(Conversions.ToString(Language.s("{0} errors")), this.iErrors), frmMain.LogType.Info);
                this.LogLine(Conversions.ToString(Language.s("see LOG file for more information")), frmMain.LogType.Info);
            }
            this.btnOk.Enabled = true;
        }
        private void timEnd_Tick(object sender, EventArgs e)
        {
            this.Close();
            ProjectData.EndApp();
        }
        private void btnOk_ButtonClick()
        {
            this.Close();
            ProjectData.EndApp();
        }
        private void btnReboot_ButtonClick()
        {
            Application.DoEvents();
            Cmd.RunCommandCom("START /WAIT C:\\KRC\\StartKrc.exe /x", "", false);
            Cmd.RunCommandCom("START /B /WAIT C:\\KRC\\VxWin\\UploadRTOS.exe -faststart -disable -nosleep -nowait", "", false);
            Process.Start("shutdown", "-r -t 00");
            this.Close();
            ProjectData.EndApp();
        }
    }
}
