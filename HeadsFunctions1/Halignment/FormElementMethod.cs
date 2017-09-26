using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HeadsUtils.Interfaces;
using HeadsUtils;
using HeadsFunctions1.Properties;
using System.IO;

namespace HeadsFunctions1.Halignment
{
    internal partial class FormElementMethod : Form
    {
        enum ElementType
        {
            Straight = 1,
            Lead_Transition,
            Circular,
            Trail_Transition,
        }

        int nTotED = 0;
        int ElementNo = 1;
        CElementData lastED = new CElementData();
        CElementData[] carED = new CElementData[500];
        IHeadsApplication m_app;
        private int m_iLastAppliedPolyLine = 0;
        public FormElementMethod(IHeadsApplication app)
        {
            InitializeComponent();

            PopulateElementTypes();            

            this.m_app = app;
        }

        public FormElementMethod(IHeadsApplication app, CHIPInfo info)
        {
            InitializeComponent();
            
            this.m_app = app;
            this.PopulateElementTypes();
            
            this.FillFromExistingData(info);

            this.txtModelName_.ReadOnly = true;
            this.txtStringlabel_.ReadOnly = true;

            
        }
                
        void PopulateElementTypes()
        {
            comboType_.Items.Add(ElementType.Straight);
            comboType_.Items.Add(ElementType.Lead_Transition);
            comboType_.Items.Add(ElementType.Circular);
            comboType_.Items.Add(ElementType.Trail_Transition);

            this.comboType_.SelectedItem = ElementType.Straight;
        }

        void FillFromExistingData(CHIPInfo info)
        {
            if (info.IsHipData == false)
            {
                ViewerUtils.DeleteFileIfExists(System.IO.Path.Combine(this.m_app.AppDataPath, "halign.tmp"));

                int nidxED = 0;

                this.txtModelName_.Text = info.ModelName;
                this.txtStringlabel_.Text = info.StringLabel;
                for (int iCnt = 0; iCnt < info.DataList.Count; iCnt++ )
                {
                    CHalignFilData fildata = info.DataList[iCnt];
                    if (carED[nidxED] == null)
                    {
                        carED[nidxED] = new CElementData();
                    }
                    carED[nidxED].dBearings = fildata.dB1;
                    carED[nidxED].dChainInterval = 10;
                    carED[nidxED].dLength = fildata.dEllength;
                    if (fildata.iTurn == 1)
                    {
                        fildata.dRadius = -1 * fildata.dRadius;
                    }
                    carED[nidxED].dRadius = fildata.dRadius;
                    carED[nidxED].dStartChain = fildata.dStartchn;
                    carED[nidxED].dXvalue = fildata.dStartx;
                    carED[nidxED].dYvalue = fildata.dStarty;
                    carED[nidxED].nElementNo = nidxED + 1;
                    carED[nidxED].nType = fildata.iEltype;

                    CHalignElementUtil code = new CHalignElementUtil();

                    code.Funcmain(this.m_app.AppDataPath, 2, 1, info.ModelName, info.StringLabel
                        , (short)(nidxED + 1), fildata.iEltype, fildata.dStartchn
                        , 10.0, fildata.dStartx, fildata.dStarty, fildata.dB1
                        , fildata.dEllength, fildata.dRadius);

                    carED[nidxED].npLineID = DrawingUtil.DrawData(this.m_app, System.IO.Path.Combine(this.m_app.AppDataPath, "HAL2.TMP"), "ELE", true);

                    nidxED++;

                    this.SelectedElementType = fildata.iEltype;
                    this.txtChainageInterval_.Value = 10.0;
                    this.txtLength_.Value = fildata.dEllength;
                    this.txtModelName_.Text = fildata.sMod;
                    this.txtRadius_.Value = fildata.dRadius;
                    this.txtStartInterval_.Value = fildata.dEndchn;
                    this.txtStringlabel_.Text = fildata.sString;
                    this.txtWCBearings_.Value = fildata.dB2;
                    this.txtXVal_.Value = fildata.dEndx;
                    this.txtYVal_.Value = fildata.dEndy;
                    this.StartEleNo = nidxED + 1;
                    ElementNo = nidxED + 1;
                    nTotED = ElementNo - 1;
                    TotalEleDrawn = nidxED;

                    lastED.dStartChain = this.txtStartInterval_.Value;
                    lastED.dChainInterval = this.txtChainageInterval_.Value;
                    lastED.dXvalue = this.txtXVal_.Value;
                    lastED.dYvalue = this.txtYVal_.Value;
                    lastED.nType = this.SelectedElementType;
                    lastED.dBearings = this.txtWCBearings_.Value;
                    lastED.dLength = this.txtLength_.Value;
                    lastED.dRadius = this.txtRadius_.Value;
                    lastED.nElementNo = this.StartEleNo;
                }
                this.m_app.ActiveDocument.RefreshDocument();
                this.m_app.ZoomExtents();
                //ViewerUtils.DeleteFileIfExists(this.m_app.AppDataPath + @"\~halign.fil");
            }            
        }

        private short SelectedElementType
        {
            set
            {
                this.comboType_.SelectedItem = (ElementType)value;
            }
            get
            {
                return (short)((ElementType)this.comboType_.SelectedItem);
            }
        }

        private int StartEleNo
        {
            set
            {
                this.txtStartEleNo_.Text = value.ToString();
            }
            get
            {
                return int.Parse(this.txtStartEleNo_.Text);
            }
        }

        private int TotalEleDrawn
        {
            set
            {
                this.txtTotalEleDrawn_.Text = value.ToString();
            }
            get
            {
                return int.Parse(this.txtTotalEleDrawn_.Text);
            }
        }

        void SetDlgED(int nED)
        {
            txtStartInterval_.Value = carED[nED].dStartChain;
            txtChainageInterval_.Value = carED[nED].dChainInterval;
            txtXVal_.Value = carED[nED].dXvalue;
            txtYVal_.Value = carED[nED].dYvalue;
            SelectedElementType = (short)carED[nED].nType;

            txtWCBearings_.Value = carED[nED].dBearings;
            txtLength_.Value = carED[nED].dLength;
            txtRadius_.Value = carED[nED].dRadius;
            this.StartEleNo = carED[nED].nElementNo;
        }

        void GetDlgED(int nED, int ncombo, int nID)
        {
            if (carED[nED] == null)
            {
                carED[nED] = new CElementData();
            }
	        carED[nED].dStartChain = this.txtStartInterval_.Value;
	        carED[nED].dChainInterval = this.txtChainageInterval_.Value;
	        carED[nED].dXvalue = this.txtXVal_.Value;
	        carED[nED].dYvalue = this.txtYVal_.Value;
	        carED[nED].nType = ncombo;

	        carED[nED].dBearings = this.txtWCBearings_.Value;
            carED[nED].dLength = this.txtLength_.Value;
            carED[nED].dRadius = this.txtRadius_.Value;
	        carED[nED].npLineID = nID;
            carED[nED].nElementNo = this.StartEleNo;
        }

        void SetNext()
        {
            if (ElementNo <= nTotED)
            {
                ElementNo++;
            }
            else
            {
                return;
            }

            if (ElementNo > nTotED)
            {
                txtStartInterval_.Value = lastED.dStartChain;
                txtChainageInterval_.Value = lastED.dChainInterval;
                txtXVal_.Value = lastED.dXvalue;
                txtYVal_.Value = lastED.dYvalue;
                SelectedElementType = (short)lastED.nType;
                txtWCBearings_.Value = lastED.dBearings;
                txtLength_.Value = lastED.dLength;
                txtRadius_.Value = lastED.dRadius;
                this.StartEleNo = lastED.nElementNo;
            }
            else
            {
                SetDlgED(ElementNo - 1);
            }
        }

        void SetLast()
        {
            if (ElementNo <= nTotED)
            {
                ElementNo = nTotED + 1;
            }
            else
            {
                return;
            }

            txtStartInterval_.Value = lastED.dStartChain;
            txtChainageInterval_.Value = lastED.dChainInterval;
            txtXVal_.Value = lastED.dXvalue;
            txtYVal_.Value = lastED.dYvalue;
            SelectedElementType = (short)lastED.nType;
            txtWCBearings_.Value = lastED.dBearings;
            txtLength_.Value = lastED.dLength;
            txtRadius_.Value = lastED.dRadius;
            this.StartEleNo = lastED.nElementNo;
        }

        void SetFirst()
        {
            if (ElementNo == 1) return;

            ElementNo = 1;
            SetDlgED(ElementNo - 1);
        }

        void SetPrevious()
        {
            if (ElementNo == 1) return;

            ElementNo--;
            SetDlgED(ElementNo - 1);
        }

        void ApplyData()
        {
            if (this.txtModelName_.Text.Trim() == "" || this.txtStringlabel_.Text.Trim() == "")
            {
                MessageBox.Show("Input Model Name and String Level"
                    , Resources.ST_ERROR_MSGBOX_CAPTION
                    , System.Windows.Forms.MessageBoxButtons.OK
                    , System.Windows.Forms.MessageBoxIcon.Information);

                return;
            }
            else
            {
                if (this.txtModelName_.ReadOnly == false)
                {
                    string strHalignFilFilePath = System.IO.Path.Combine(this.m_app.AppDataPath, "HALIGN.FIL");
                    CHIPInfo[] infoarr = CHalignHipUtil.ReadHaligns(strHalignFilFilePath);
                    foreach (CHIPInfo info in infoarr)
                    {
                        if (info.ModelName.Trim().ToLower() == this.txtModelName_.Text.Trim().ToLower()
                            && info.StringLabel.Trim().ToLower() == this.txtStringlabel_.Text.Trim().ToLower())
                        {
                            MessageBox.Show("Model Name and String Level already exists\nPlease specify unique Model Name and String Level"
                                , Resources.ST_ERROR_MSGBOX_CAPTION
                                , System.Windows.Forms.MessageBoxButtons.OK
                                , System.Windows.Forms.MessageBoxIcon.Information);

                            return;
                        }
                    }
                }
            }

            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.btnSaveFinish_.Enabled = false;            

            ViewerUtils.DeleteFileIfExists(System.IO.Path.Combine(this.m_app.AppDataPath, "halign.tmp"));

            int loop = nTotED;
            while (loop > 0)
            {
                loop--;
                //Delete existing hip line
                IHdEntity entity = this.m_app.ActiveDocument.GetObjectById(carED[loop].npLineID);
                if (entity != null)
                {
                    entity.Erase();
                }
            }
            CHalignElementUtil code = new CHalignElementUtil();
            for (loop = 0; loop < ElementNo - 1; loop++)
            {
                ViewerUtils.DeleteFileIfExists(System.IO.Path.Combine(this.m_app.AppDataPath, "HAL2.TMP"));

                code.Funcmain(this.m_app.AppDataPath, 2, 1, this.txtModelName_.Text, this.txtStringlabel_.Text
                    , (short)carED[loop].nElementNo, (short)carED[loop].nType, carED[loop].dStartChain
                    , carED[loop].dChainInterval, carED[loop].dXvalue, carED[loop].dYvalue
                    , carED[loop].dBearings, carED[loop].dLength, carED[loop].dRadius);
                this.m_iLastAppliedPolyLine = DrawingUtil.DrawData(this.m_app, System.IO.Path.Combine(this.m_app.AppDataPath, "HAL2.TMP"), "ELE", true);
                carED[loop].npLineID = this.m_iLastAppliedPolyLine;

            } //end for

            ViewerUtils.DeleteFileIfExists(Path.Combine(this.m_app.AppDataPath, "HAL2.TMP"));

            //writes hal2.tmp, appends halign.tmp
            code.Funcmain(this.m_app.AppDataPath, 2, 1, this.txtModelName_.Text, this.txtStringlabel_.Text
                , (short)ElementNo, this.SelectedElementType, this.txtStartInterval_.Value
                , this.txtChainageInterval_.Value, this.txtXVal_.Value, this.txtYVal_.Value
                , this.txtWCBearings_.Value, this.txtLength_.Value, this.txtRadius_.Value);

            this.m_iLastAppliedPolyLine = DrawingUtil.DrawData(this.m_app, Path.Combine(this.m_app.AppDataPath, "HAL2.TMP"), "ELE", true);

            GetDlgED((ElementNo - 1), (int)this.SelectedElementType, this.m_iLastAppliedPolyLine);
            nTotED = ElementNo;

            //	AcceptCode = 1;
            ElementNo = ElementNo + 1;
            this.StartEleNo = ElementNo;
            this.txtXVal_.Value = code.xe;
            this.txtYVal_.Value = code.ye;
            this.txtWCBearings_.Value = code.b2;
            this.txtStartInterval_.Value += this.txtLength_.Value;
            this.TotalEleDrawn = nTotED;

            lastED.dXvalue = code.xe;
            lastED.dYvalue = code.ye;
            lastED.dBearings = code.b2;
            lastED.dChainInterval = this.txtChainageInterval_.Value;
            lastED.dStartChain = this.txtStartInterval_.Value;
            lastED.nType = (int)this.SelectedElementType;
            lastED.dLength = this.txtLength_.Value;
            lastED.dRadius = this.txtRadius_.Value;
            lastED.nElementNo = ElementNo;

            this.m_app.ActiveDocument.RefreshDocument();
            this.btnSaveFinish_.Enabled = true;
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }

        void Cancel()
        {
            int loop = nTotED;
            while (loop > 0)
            {
                loop--;
                IHdEntity entity = this.m_app.ActiveDocument.GetObjectById(carED[loop].npLineID);
                if (entity != null)
                {
                    entity.Erase();
                }
            }
            // delete halign.tmp
            ViewerUtils.DeleteFileIfExists(Path.Combine(this.m_app.AppDataPath, "halign.tmp"));
            if (System.IO.File.Exists(Path.Combine(this.m_app.AppDataPath, "~halign.fil")))
            {
                // delete truncated halign.fil
                ViewerUtils.DeleteFileIfExists(Path.Combine(this.m_app.AppDataPath, "halign.fil"));

                ViewerUtils.RenameFile(Path.Combine(this.m_app.AppDataPath, "~halign.fil"), "halign.fil");
            }
        }

        void SaveAndFinish()
        {
            CHalignElementUtil.SaveHalign(this.m_app.AppDataPath);

            // delete halign.tmp
            ViewerUtils.DeleteFileIfExists(System.IO.Path.Combine(this.m_app.AppDataPath , "halign.tmp"));
            ViewerUtils.RenameFile(System.IO.Path.Combine(this.m_app.AppDataPath , "~halign.fil"), "~*halign.fil");
        }

        #region EVENTS
        private void btnNext__Click(object sender, EventArgs e)
        {
            this.SetNext();
        }

        private void btnLast__Click(object sender, EventArgs e)
        {
            this.SetLast();            
        }

        private void btnFirst__Click(object sender, EventArgs e)
        {
            this.SetFirst();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            this.SetPrevious();
        }

        private void btnDelTrailing__Click(object sender, EventArgs e)
        {
            this.SetPrevious();
            this.ApplyData();
        }        

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.ApplyData();
        }        

        private void btnCancel__Click(object sender, EventArgs e)
        {
            this.Cancel();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        
        private void btnSaveFinish__Click(object sender, EventArgs e)
        {
            SaveAndFinish();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        #endregion

       
    }
}