using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HeadsUtils.Interfaces;
using HeadsUtils;
using System.IO;

namespace HeadsFunctions1.Offset.Modelling
{
    internal partial class FormModellingOffset : Form
    {
        IHeadsApplication app;
        Hashtable hashReferenceModel = new Hashtable();
        Hashtable hashOffsetModel = new Hashtable();
        public FormModellingOffset(IHeadsApplication hdapp)
        {
            InitializeComponent();
            this.app = hdapp;

            this.PopulateModels(ref hashReferenceModel, ref hashOffsetModel);
        }

        void PopulateModels(ref Hashtable hashRefModel, ref Hashtable hashOffModel)
        {
            StreamReader sr = new StreamReader(Path.Combine(this.app.AppDataPath, "OFFSET.FIL"));
            while (sr.EndOfStream == false)
            {
                string strLine = sr.ReadLine();
                string[] tok = strLine.Split(new char[] { '\t' });
                short opnum = short.Parse(tok[0]);
                if (opnum == 401)
                {
                    string strRefMod = tok[2];
                    string strRefStr = tok[4];
                    if (hashRefModel.ContainsKey(strRefMod) == false)
                    {
                        hashRefModel.Add(strRefMod, new List<string>());
                        this.comboRefModel_.Items.Add(strRefMod);
                    }

                    ((List<string>)hashRefModel[strRefMod]).Add(strRefStr);                   
                }
                if (opnum == 402 || opnum == 403 || opnum == 404)
                {
                    string strOffMod = tok[2];
                    string strOffStr = tok[4];
                    if (hashOffModel.ContainsKey(strOffMod) == false)
                    {
                        hashOffModel.Add(strOffMod, new List<string>());
                        this.comboOffModel_.Items.Add(strOffMod);
                    }

                    ((List<string>)hashOffModel[strOffMod]).Add(strOffStr);    
                }
            }
            sr.Close();

            if (this.comboRefModel_.Items.Count > 0)
            {
                this.comboRefModel_.SelectedIndex = 0;
            }

            if (this.comboOffModel_.Items.Count > 0)
            {
                this.comboOffModel_.SelectedIndex = 0;
            }
        }

        private void comboRefModel__SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> listdata = (List<string>)this.hashReferenceModel[comboRefModel_.SelectedItem.ToString()];
            this.listBoxRefStr_.Items.Clear();
            if (listdata != null)
            {
                foreach(string str in listdata)
                {
                    this.listBoxRefStr_.Items.Add(str);
                }

                this.listBoxRefStr_.SelectedIndex = 0;
            }

        }

        private void comboOffModel__SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> listdata = (List<string>)this.hashOffsetModel[comboOffModel_.SelectedItem.ToString()];
            this.listBoxOffsetStr_.Items.Clear();
            if (listdata != null)
            {
                foreach (string str in listdata)
                {
                    this.listBoxOffsetStr_.Items.Add(str);
                }
                this.listBoxOffsetStr_.SelectedIndex = 0;
            }
        }

        private void btnOk__Click(object sender, EventArgs e)
        {
            FormModellingOffsetViewer form = new FormModellingOffsetViewer(this.app, this.comboRefModel_.SelectedItem.ToString()
                , this.listBoxRefStr_.SelectedItem.ToString(), this.comboOffModel_.SelectedItem.ToString()
                , this.listBoxOffsetStr_.SelectedItem.ToString());
            form.ShowDialog();
            this.Close();
        }

        private void btnCancel__Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}