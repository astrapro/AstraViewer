using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HeadsUtils;
using HeadsUtils.Interfaces;
using HeadsFunctions1.Halignment;

namespace HeadsFunctions1.Halignment.Modelling
{
    internal partial class FormHalignModelViewer : Form
    {
        IHeadsApplication app;
        CHalignModelUtil util;
        public FormHalignModelViewer(IHeadsApplication headsapp)
        {
            InitializeComponent();

            this.app = headsapp;
            util = new CHalignModelUtil();
            util.OnShowModelInfo += new CHalignModelUtil.ShowDataEvent(util_OnShowModelInfo);
        }

        void util_OnShowModelInfo(string strText)
        {
            this.listBoxView.Items.Add(strText);
            this.listBoxView.SelectedIndex = this.listBoxView.Items.Count - 1;
            System.Threading.Thread.Sleep(1);
            //this.richTextBoxView.Text += strText + "\n"; ;
        }

        private void FormHalignModelViewer_Load(object sender, EventArgs e)
        {
            
        }

        private void btnQuit__Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnProceed__Click(object sender, EventArgs e)
        {
            util.Funcmain(this.app.AppDataPath);
        }


    }
}