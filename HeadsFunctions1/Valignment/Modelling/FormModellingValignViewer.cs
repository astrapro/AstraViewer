using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HeadsUtils.Interfaces;
using HeadsUtils;
using System.IO;

namespace HeadsFunctions1.Valignment.Modelling
{
    public partial class FormModellingValignViewer : Form
    {
        CValignInfo valInfo;
        IHeadsApplication app;
        double chainageVal;

        public FormModellingValignViewer(CValignInfo valInfo, double chainageVal, IHeadsApplication app)
        {
            this.app = app;
            this.chainageVal = chainageVal;
            this.valInfo = valInfo;

            InitializeComponent();
        }

        private void btnProceed__Click(object sender, EventArgs e)
        {
            CValignModelUtil ModelUtil = new CValignModelUtil();
            ModelUtil.OnShowModelInfo += new CValignModelUtil.ShowDataEvent(ModelUtil_OnShowModelInfo);
            ModelUtil.Funcmain(this.app.AppDataPath, this.valInfo, this.chainageVal);
        }

        void ModelUtil_OnShowModelInfo(string strText)
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