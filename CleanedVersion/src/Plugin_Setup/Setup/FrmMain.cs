using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Setup
{
    public partial class FrmMain : Form
    {
        #region Fields
        private string sCompany;
        private string sName;
        private string sVersion;
        private string sBuild;
        private string sDll;
        public JobType Job;
        private bool SmartHMIwasRuning;
        private IniFile myIni;
        private string strLogFile;
        private int iWarnings;
        private int iErrors;
        #endregion
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
        public FrmMain()
        {
            InitializeComponent();
        }
    }
}
