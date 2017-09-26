using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HeadsUtils.Interfaces;

namespace HeadsFunctions1.WorkingFolder
{
    internal partial class FormWorkingFolderSelector : Form, IWorkingDirSelector
    {
        
        public FormWorkingFolderSelector()
        {
            InitializeComponent();
            this.txtWkDirDetail_.Text = string.Format(HeadsFunctions1.Properties.Resources.ST_ABOUT_WORKING_DIR, HeadsUtils.Constants.ProductName);
        }

        private bool IsValidFolderPath(string strPath)
        {
            strPath = strPath.Trim();
            return (strPath != string.Empty && System.IO.Directory.Exists(strPath));            
        }

        private void btnFolderSelector__Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            if (this.IsValidFolderPath(this.txtPath_.Text))
            {
                folderDlg.SelectedPath = this.txtPath_.Text;
            }
            //folderDlg.ShowNewFolderButton = false;
            if (folderDlg.ShowDialog() == DialogResult.OK)
            {
                this.txtPath_.Text = folderDlg.SelectedPath;
            }
        }
        
        #region IWorkingDirSelector Members

        public string WorkingFolderPath
        {
            set
            {
                if (this.IsValidFolderPath(value))
                {
                    this.txtPath_.Text = value;
                }
                
            }
            get
            {
                string strPath = string.Empty;
                if (this.IsValidFolderPath(this.txtPath_.Text))
                {
                    strPath = this.txtPath_.Text;
                }
                return strPath;
            }
        }

        public Form DialogInstance
        {
            get { return this; }
        }

        #endregion

        private void btnOk__Click(object sender, EventArgs e)
        {
            if (this.IsValidFolderPath(this.txtPath_.Text))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }           
        }

        private void txtPath__TextChanged(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists(this.txtPath_.Text))
            {
                this.btnOk_.Enabled = true;
            }
            else
            {
                this.btnOk_.Enabled = false;
            }
        }

        private void btnCancel___Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}