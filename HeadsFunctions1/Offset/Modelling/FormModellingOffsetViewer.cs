using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HeadsUtils.Interfaces;
using HeadsUtils;

namespace HeadsFunctions1.Offset.Modelling
{
    internal partial class FormModellingOffsetViewer : Form
    {
        IHeadsApplication App;
        string RefMod, RefStr, OffMod, OffStr;
        public FormModellingOffsetViewer(IHeadsApplication hdApp, string strRefMod, string strRefStr, string strOffMod, string strOffStr)
        {
            InitializeComponent();

            this.App = hdApp;
            this.RefMod = strRefMod;
            this.RefStr = strRefStr;
            this.OffMod = strOffMod;
            this.OffStr = strOffStr;
        }
        

        private void btnProceed__Click(object sender, EventArgs e)
        {
            COffsetModelUtil modelutil = new COffsetModelUtil();
            modelutil.OnShowModelInfo += new COffsetModelUtil.ShowDataEvent(modelutil_OnShowModelInfo);
            modelutil.FuncMainPostOff(App.AppDataPath, RefMod, RefStr, OffMod, OffStr);

            this.Close();
        }

        void modelutil_OnShowModelInfo(string strText)
        {
            this.listModelCtrl_.Items.Add(strText);
            this.listModelCtrl_.SelectedIndex = this.listModelCtrl_.Items.Count - 1;
            System.Threading.Thread.Sleep(1); 
        }

        private void btnQuit__Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}