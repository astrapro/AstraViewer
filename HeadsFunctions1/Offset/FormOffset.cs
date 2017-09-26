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

namespace HeadsFunctions1.Offset
{
    internal partial class FormOffset : Form
    {
        const string STR_402 = "402 Vertical Elevated Offset";
        const string STR_403 = "403 Super Elevated Offset";
        const string STR_404 = "404 Super Elevated Offset";

        IHeadsApplication app;

        public FormOffset(IHeadsApplication hdapp, IHdPolyline3D pline)
        {
            InitializeComponent();

            this.app = hdapp;

            this.comboOffsetType_.Items.Add(STR_402);
            this.comboOffsetType_.Items.Add(STR_403);
            this.comboOffsetType_.Items.Add(STR_404);

            this.InitDataFromPolyline(pline);
            this.comboOffsetType_.SelectedItem = STR_402;
        }

        void InitDataFromPolyline(IHdPolyline3D pline)
        {
            string strLbl = pline.Label;
            if (strLbl.Trim() == "")
            {
                strLbl = "tMod:tStr";
                pline.Label = strLbl;
            }
            string[] tok = strLbl.Split(new char[] { ':' });
            if (tok.Length == 2)
            {
                this.tbRefModel_.Text = tok[0].Trim();
                this.tbRefString_.Text = tok[1].Trim();
            }
        }

        void WriteDataToFileOFF()
        {
            OffsetRec data = new OffsetRec();
           
            data.OffsetType = short.Parse(this.comboOffsetType_.SelectedItem.ToString().Substring(0, 3));
            data.AcceptCode = 0;
            data.ModelName = this.tbRefModel_.Text;
            data.StringName = this.tbRefString_.Text;
            data.OffsetModel = this.tbOffsetModel_.Text;
            data.OffsetString = this.tbOffsetString_.Text;
            data.StartChainage = this.tbStartChainage_.Value;
            data.EndChainage = this.tbEndChainage_.Value;
            data.StartHOffset = this.tbStartHorzOffset_.Value;
            data.EndHOffset = this.tbEndHorzOffset_.Value;
            data.StartVOffset = this.tbStartVertOffset_.Value;
            data.EndVOffset = this.tbEndVertOffset_.Value;
            data.StartSuper = this.tbStartSuperElv_.Value;
            data.EndSuper = this.tbEndSuperElv_.Value;


            string strPath = Path.Combine(this.app.AppDataPath, "OFF1.TMP");
            BinaryWriter bw = new BinaryWriter(new FileStream(strPath, FileMode.Create), Encoding.Default);
            data.ToStream(bw);
            bw.Close();

        }

        void SaveOffset()
        {
            short off_opnum=0;
            double ch1=0.0, ch2=0.0, ho1=0.0, ho2=0.0, vo1=0.0, vo2=0.0, se1=0.0, se2=0.0;
            double last_ch1=0.0, last_ch2=0.0;
	        string ostr1, ostr2, ostr3, ostr4, offmod, offstr;
	        string last_offmod = string.Empty, last_offstr = string.Empty;

            if (File.Exists(Path.Combine(this.app.AppDataPath, "OFFSET.TMP")) == false)
            {
                return;
            }
            StreamReader san1 = new StreamReader(Path.Combine(this.app.AppDataPath, "OFFSET.TMP"));
            StreamWriter san2 = new StreamWriter(Path.Combine(this.app.AppDataPath, "OFFSET.FIL"), true);
            while (san1.EndOfStream == false)
            {
                string strLine = san1.ReadLine();
                string[] tok = strLine.Split(new char[]{'\t'});
                off_opnum = short.Parse(tok[0]);
                if (off_opnum == 401)
                {
                    ostr1 = tok[1];
                    ostr2 = tok[2];
                    ostr3 = tok[3];
                    ostr4 = tok[4];
                    san2.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}", off_opnum, ostr1, ostr2, ostr3, ostr4));
                    

                }
                else if (off_opnum == 402)
                {
                    ostr1 = tok[1];
                    offmod = tok[2];
                    ostr3 = tok[3];
                    offstr = tok[4];

                    ostr1 = tok[5];
                    ch1 = double.Parse(tok[6]);
                    ostr3 = tok[7];
                    ch2 = double.Parse(tok[8]);

                    ostr1 = tok[9];
                    ho1 = double.Parse(tok[10]);
                    ostr3 = tok[11];
                    ho2 = double.Parse(tok[12]);

                    ostr1 = tok[13];
                    vo1 = double.Parse(tok[14]);
                    ostr3 = tok[15];
                    vo2 = double.Parse(tok[16]);



                    if (offmod.ToLower() == last_offmod.ToLower()
                        && offstr.ToLower() == last_offstr.ToLower()
                        && ch1 == last_ch1 && ch2 == last_ch2)
                    {
                        continue;
                    }
                    else
                    {
                        string strTemp = string.Format("{0}\tMODEL\t{1}\tSTRING\t{2}\t", off_opnum, offmod, offstr);
                        strTemp += string.Format("CH1\t{0:f3}\tCH2\t{1:f3}\t", ch1, ch2);
                        strTemp += string.Format("HO1\t{0:f}\tHO2\t{1:f}\t", ho1, ho2);
                        strTemp += string.Format("VO1\t{0:f}\tVO2\t{1:f}", vo1, vo2);

                        san2.WriteLine(strTemp);                     
                    }

                    last_offmod = offmod;
                    last_offstr = offstr;
                    last_ch1 = ch1;
                    last_ch2 = ch2;

                }
                else if (off_opnum == 403)
                {
                    ostr1 = tok[1];
                    offmod = tok[2];
                    ostr3 = tok[3];
                    offstr = tok[4];

                    ostr1 = tok[5];
                    ch1 = double.Parse(tok[6]);
                    ostr3 = tok[7];
                    ch2 = double.Parse(tok[8]);

                    ostr1 = tok[9];
                    ho1 = double.Parse(tok[10]);
                    ostr3 = tok[11];
                    ho2 = double.Parse(tok[12]);

                    ostr1 = tok[13];
                    se1 = double.Parse(tok[14]);
                    ostr3 = tok[15];
                    se2 = double.Parse(tok[16]);


                    if (offmod.ToLower() == last_offmod.ToLower()
                        && offstr.ToLower() == last_offstr.ToLower()
                        && ch1 == last_ch1 && ch2 == last_ch2)
                    {
                        continue;
                    }
                    else
                    {
                        string strTemp = string.Format("{0}\tMODEL\t{1}\tSTRING\t{2}\t", off_opnum, offmod, offstr);
                        strTemp += string.Format("CH1\t{0:f3}\tCH2\t{1:f3}\t", ch1, ch2);
                        strTemp += string.Format("HO1\t{0:f}\tHO2\t{1:f}\t", ho1, ho2);
                        strTemp += string.Format("SE1\t{0:f}\tSE2\t{1:f}", se1, se2);
                        san2.WriteLine(strTemp);  
                    }

                    last_offmod = offmod;
                    last_offstr = offstr;
                    last_ch1 = ch1;
                    last_ch2 = ch2;

                }
                else if (off_opnum == 404)
                {
                    ostr1 = tok[1];
                    offmod = tok[2];
                    ostr3 = tok[3];
                    offstr = tok[4];

                    ostr1 = tok[5];
                    ch1 = double.Parse(tok[6]);
                    ostr3 = tok[7];
                    ch2 = double.Parse(tok[8]);

                    ostr1 = tok[9];
                    se1 = double.Parse(tok[10]);
                    ostr3 = tok[11];
                    se2 = double.Parse(tok[12]);

                    if (offmod.ToLower() == last_offmod.ToLower()
                       && offstr.ToLower() == last_offstr.ToLower()
                       && ch1 == last_ch1 && ch2 == last_ch2)
                    {
                        continue;
                    }
                    else
                    {
                        string strTemp = string.Format("{0}\tMODEL\t{1}\tSTRING\t{2}\t", off_opnum, offmod, offstr);
                        strTemp += string.Format("CH1\t{0:f3}\tCH2\t{1:f3}\t", ch1, ch2);
                        strTemp += string.Format("SE1\t{0:f}\tSE2\t{1:f}", se1, se2);

                        san2.WriteLine(strTemp); 
                    }

                    last_offmod = offmod;
                    last_offstr = offstr;
                    last_ch1 = ch1;
                    last_ch2 = ch2;

                }
            }

            san1.Close();
            san2.Close();
        }

        private void comboOffsetType__SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tbStartVertOffset_.Enabled = true;
            this.tbEndVertOffset_.Enabled = true;
            this.tbStartHorzOffset_.Enabled = true;
            this.tbEndHorzOffset_.Enabled = true;
            this.tbStartSuperElv_.Enabled = true;
            this.tbEndSuperElv_.Enabled = true;

            if(this.comboOffsetType_.SelectedItem.ToString() == STR_402)
            {
                this.tbStartSuperElv_.Enabled = false;
                this.tbEndSuperElv_.Enabled = false;
            }
            else if (this.comboOffsetType_.SelectedItem.ToString() == STR_403)
            {
                this.tbStartVertOffset_.Enabled = false;
                this.tbEndVertOffset_.Enabled = false;
            }
            else if (this.comboOffsetType_.SelectedItem.ToString() == STR_404)
            {
                this.tbStartVertOffset_.Enabled = false;
                this.tbEndVertOffset_.Enabled = false;
                this.tbStartHorzOffset_.Enabled = false;
                this.tbEndHorzOffset_.Enabled = false;
            }

        }

        private void btnApply__Click(object sender, EventArgs e)
        {
            WriteDataToFileOFF();

            if (File.Exists(Path.Combine(this.app.AppDataPath, "OFF1.TMP")) == true)
            {
                ViewerUtils.DeleteFileIfExists(Path.Combine(this.app.AppDataPath, "OFF2.TMP"));

                COffsetUtil util = new COffsetUtil();
                util.Funcmain(this.app.AppDataPath);

                if (File.Exists(Path.Combine(this.app.AppDataPath, "OFF2.TMP")))
                {
                    DrawingUtil.DrawData(this.app, Path.Combine(this.app.AppDataPath, "OFF2.TMP"), "PVR", true);
                    this.app.ActiveDocument.RefreshDocument();
                }
            }
        }

        private void btnCancel__Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFinish__Click(object sender, EventArgs e)
        {
            SaveOffset();
            this.Close();
        }
    }
}