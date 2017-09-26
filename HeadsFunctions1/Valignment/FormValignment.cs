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

namespace HeadsFunctions1.Valignment
{
    internal partial class FormValignment : Form
    {
        #region CONSTANTS
        const string strColIndex = "VIP#";
        const string strColVipChain = "VIP_CHAIN(M)";
        const string strColVipLevel = "VIP_LEVEL(M)";
        const string strColGrade = "Grade(%)";
        const string strColSymmetrical = "Symmetrical";
        const string strColVCL1 = "VCL / VCL1(M)";
        const string strColVCL2 = "VCL2(M)";

        const double DefaultVCurveLen = 150.0;
        #endregion

        DataTable dtParams;
        IHeadsApplication app;
        int LastAppliedPolyLineID = 0;
        int MainPolyLineID = 0;
        CValignInfo InfoData = null;

        public FormValignment(IHeadsApplication hdapp, IHdPolyline3D pline)
        {
            InitializeComponent();

            this.app = hdapp;

            this.InitVipParamGridData();

            List<VVIP> listVipData = new List<VVIP>();

            this.GetDataFromPolyLine(pline, ref listVipData);
            this.FillDetails(listVipData);

            this.MainPolyLineID = pline.ObjectID;

            this.checkBoxDVCL.Checked = true;
        }

        public FormValignment(IHeadsApplication hdapp, CValignInfo info)
        {
            InitializeComponent();

            this.app = hdapp;

            this.InitVipParamGridData();

            this.InfoData = info;

            this.tbModelName_.ReadOnly = true;
            this.tbStringlabel_.ReadOnly = true;
            
            List<VVIP> listVipData = new List<VVIP>();

            GetDetailsFromExistingData(info, ref listVipData);
            FillDetails(listVipData);

            this.ApplyValign();

            checkBoxDVCL.Checked = false;
            UpadteGradeValue();

            this.app.ActiveDocument.RefreshDocument();
            this.app.ZoomExtents();
        }

        bool IsEditMode
        {
            get
            {
                return (this.InfoData == null) ? false:true;
            }
        }

        void GetDetailsFromExistingData(CValignInfo info, ref List<VVIP> listVipData)
        {
            double tx = 0, ty = 0, tgrade = 0, tv1 = 0;//, tv2 = 0;
            bool tsym = true;
            listVipData.Clear();
            this.tbModelName_.Text = info.ModelName;
            this.tbStringlabel_.Text = info.StringLabel;
            VVIP vd = null;

            for (int nLN = 0; nLN < info.DataList.Count; nLN++)
            {
                CValignFilData data = info.DataList[nLN];

                if (nLN == 0)
                {
                    if (data.eltype == 1)
                    {
                        vd = new VVIP();
                        vd.chainx = data.xs;
                        vd.chainy = data.ys;
                        vd.grade = data.b1;
                        if (data.rad != data.l)
                        {
                            vd.sym = false;
                            vd.vcl1 = data.rad;
                            vd.vcl2 = data.l;
                        }
                        else
                        {
                            vd.sym = true;
                            vd.vcl1 = data.rad + data.l;
                            vd.vcl2 = 0.0;
                        }

                        vd.vcl1 = 0.0;
                        vd.vcl2 = 0.0;
                        listVipData.Add(vd);
                    }
                }
                else
                {
                    if (data.eltype == 3)
                    {
                        vd = new VVIP();

                        vd.chainx = data.hipx;
                        vd.chainy = data.hipy;
                        vd.grade = data.b1;
                        if (data.rad != data.l)
                        {
                            vd.sym = false;
                            vd.vcl1 = data.rad;
                            vd.vcl2 = data.l;

                        }
                        else
                        {
                            vd.sym = true;
                            vd.vcl1 = data.rad + data.l;
                            vd.vcl2 = 0.0;
                        }

                        listVipData.Add(vd);
                    }
                    else // if eltype == 1
                    { // for last line
                        tx = data.xe;
                        ty = data.ye;
                        tgrade = data.b2;
                        tsym = true;
                        tv1 = data.l;
                        //tv2 = 0;
                    }

                }
            }

            vd = new VVIP();

            vd.chainx = tx;
            vd.chainy = ty;
            vd.grade = tgrade;
            vd.sym = tsym;
            vd.vcl1 = 0;//tv1;
            vd.vcl2 = 0;//tv2;

            listVipData.Add(vd);
        }

        void InitVipParamGridData()
        {
            this.dtParams = new DataTable("VIP Params");

            //Prepare columns
            this.dtParams.Columns.Add(FormValignment.strColIndex, typeof(int));
            this.dtParams.Columns.Add(FormValignment.strColVipChain, typeof(double));
            this.dtParams.Columns.Add(FormValignment.strColVipLevel, typeof(double));
            this.dtParams.Columns.Add(FormValignment.strColGrade, typeof(double));
            this.dtParams.Columns.Add(FormValignment.strColSymmetrical, typeof(bool));
            this.dtParams.Columns.Add(FormValignment.strColVCL1, typeof(double));
            this.dtParams.Columns.Add(FormValignment.strColVCL2, typeof(double));

            this.dataGridParams_.DataSource = dtParams;

            this.dataGridParams_.Columns[FormValignment.strColIndex].ReadOnly = true;
            this.dataGridParams_.Columns[FormValignment.strColGrade].ReadOnly = true;
            this.dataGridParams_.Columns[FormValignment.strColIndex].DefaultCellStyle.BackColor = Color.LightGray;
            this.dataGridParams_.Columns[FormValignment.strColGrade].DefaultCellStyle.BackColor = Color.LightGray;
            
            this.dataGridParams_.Columns[FormValignment.strColIndex].Frozen = true;

            int iWidth = this.dataGridParams_.Width;
            this.dataGridParams_.Columns[FormValignment.strColIndex].Width = (int)(iWidth*0.07);
            this.dataGridParams_.Columns[FormValignment.strColVipChain].Width = (int)(iWidth * 0.16);
            this.dataGridParams_.Columns[FormValignment.strColVipLevel].Width = (int)(iWidth * 0.18);
            this.dataGridParams_.Columns[FormValignment.strColGrade].Width = (int)(iWidth * 0.20);
            this.dataGridParams_.Columns[FormValignment.strColSymmetrical].Width = (int)(iWidth * 0.12);
            this.dataGridParams_.Columns[FormValignment.strColVCL1].Width = (int)(iWidth * 0.16);
            this.dataGridParams_.Columns[FormValignment.strColVCL2].Width = (int)(iWidth * 0.12);

            this.dataGridParams_.Columns[FormValignment.strColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridParams_.Columns[FormValignment.strColVipChain].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridParams_.Columns[FormValignment.strColVipLevel].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridParams_.Columns[FormValignment.strColGrade].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridParams_.Columns[FormValignment.strColSymmetrical].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridParams_.Columns[FormValignment.strColVCL1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridParams_.Columns[FormValignment.strColVCL2].SortMode = DataGridViewColumnSortMode.NotSortable;             
        }

        void GetDataFromPolyLine(IHdPolyline3D polyline,ref List<VVIP> listVipData)
        {
            CCfgtype cfg = this.app.ActiveDocument.ConfigParam;
            listVipData.Clear();
            if (polyline != null)
            {
                CPoint3D[] ptCords = polyline.Coordinates;
                for (int iIndex = 0; iIndex < ptCords.Length; iIndex++)
                {
                    VVIP data = new VVIP();
                    data.chainx = ptCords[iIndex].X / cfg.XMetric;
                    data.chainy = ptCords[iIndex].Y / cfg.YMetric;

                    listVipData.Add(data);
                }
            }
        }

        List<VVIP> GetParamData()
        {
            List<VVIP> listVipData = new List<VVIP>();
            for (int i = 0; i < this.dtParams.Rows.Count; i++)
            {
                listVipData.Add(GetRow(i));                
            }

            return listVipData;
        }

        VVIP GetRow(int iIndex)
        {
            DataRow row = this.dtParams.Rows[iIndex];
            VVIP data = new VVIP();
            data.chainx = (double)row[FormValignment.strColVipChain];
            data.chainy = (double)row[FormValignment.strColVipLevel];
            data.grade = (double)row[FormValignment.strColGrade];
            data.sym = (bool)row[FormValignment.strColSymmetrical];
            data.vcl1 = (double)row[FormValignment.strColVCL1];
            data.vcl2 = (double)row[FormValignment.strColVCL2];
            return data;
        }

        void WriteDataFile()
        {
            List<VVIP> listparam = this.GetParamData();
            string TempFilePath = Path.Combine(this.app.AppDataPath, "VAL1.TMP");
            BinaryWriter bw = new BinaryWriter(new FileStream(TempFilePath, FileMode.Create), Encoding.Default);

            FirstLine firstline = new FirstLine();

            firstline.ModelName = this.tbModelName_.Text;
            firstline.StringName = this.tbStringlabel_.Text;
            firstline.Code = 1;
            firstline.AcceptCode = 0;
            firstline.ToStream(bw);

            SecondLineVIP secondline = new SecondLineVIP();
            secondline.TotalVIPs = this.dataGridParams_.Rows.Count;
            secondline.StartChain = this.tbStartChainage_.Value;
            secondline.EndChain = this.tbEndChainage_.Value;		
            secondline.Interval = this.tbChainageInterval_.Value;
            secondline.ToStream(bw);

            
            for(int i = 0; i < listparam.Count; i++)
            {
                VVIP data = listparam[i];
                VIPData vdata = new VIPData();
                vdata.VIPNo = i + 1;
                vdata.VIP_Chain = data.chainx;
                vdata.VIP_level = data.chainy;
                if (data.sym == true)
                {
                    vdata.VCL1 = data.vcl1 / 2;
                    vdata.VCL2 = data.vcl1 / 2;
                }
                else
                {
                    vdata.VCL1 = data.vcl1;
                    vdata.VCL2 = data.vcl2;
                }
                vdata.ToStream(bw);
            }
            bw.Close();
        }

        void FillDetails(List<VVIP> listVipData)
        {
            if (listVipData.Count > 1)
            {
                for (int iIndex = 0; iIndex < listVipData.Count; iIndex++)
                {
                    VVIP data = listVipData[iIndex];
                    data.sym = true;
                    this.AddRow(data);
                }

                UpadteGradeValue();

                this.tbStartChainage_.Value = listVipData[0].chainx;
                this.tbEndChainage_.Value = listVipData[listVipData.Count-1].chainx;
            }
        }

        void UpadteGradeValue()
        {
            for (int i = 0; i < this.dtParams.Rows.Count; i++)
            {
                DataRow row = this.dtParams.Rows[i];

                row[strColIndex] = i;
                if (i == 0)
                {
                    row[strColGrade] = 0.0;
                }
                else
                {
                    DataRow rowPrev = this.dtParams.Rows[i - 1];
                    row[strColGrade] = (((double)row[strColVipLevel] - (double)rowPrev[strColVipLevel])
                        / ((double)row[strColVipChain] - (double)rowPrev[strColVipChain])) * 100;
                }
            }
        }

        void AddRow(VVIP data)
        {
            int rows = dtParams.Rows.Count;

            DataRow row = dtParams.NewRow();
            row[strColIndex] = rows;
            row[strColVipChain] = data.chainx;
            row[strColVipLevel] = data.chainy;
            row[strColGrade] = data.grade;
            row[strColSymmetrical] = data.sym;
            row[strColVCL1] = data.vcl1;
            row[strColVCL2] = data.vcl2;

            dtParams.Rows.Add(row);
        }

        bool ValidateData()
        {
            if (this.tbModelName_.Text.Trim() == "") 
            {
                this.errorProvider.SetError(this.tbModelName_, Properties.Resources.ST_PROMPT_INVALID_INPUT);
                return false;
            }
            if (this.tbStringlabel_.Text.Trim() == "")
            {
                this.errorProvider.SetError(this.tbStringlabel_, Properties.Resources.ST_PROMPT_INVALID_INPUT);
                return false;
            }

            if (this.IsEditMode == false)
            {
                string strValignFilFilePath = Path.Combine(this.app.AppDataPath, "VALIGN.FIL");
                CValignInfo[] infoarr = CValignUtil.ReadValigns(strValignFilFilePath);
                foreach (CValignInfo info in infoarr)
                {
                    if (info.ModelName.Trim().ToLower() == this.tbModelName_.Text.Trim().ToLower()
                        && info.StringLabel.Trim().ToLower() == this.tbStringlabel_.Text.Trim().ToLower())
                    {
                        this.errorProvider.SetError(this.tbStringlabel_
                            , "Model Name and String Level already exists\nPlease specify unique Model Name and String Level");
                        this.tbStringlabel_.Focus();
                        return false;
                    }
                }
            }
            return true;
        }

        void ApplyValign()
        {
            if (this.ValidateData())
            {
                this.app.ActiveDocument.ConfigParam.XMetric = 1;
                this.app.ActiveDocument.ConfigParam.YMetric = 10;
                CCfgtype cfg = new CCfgtype();
                this.app.ActiveDocument.ConfigParam.CopyTo(cfg);

                IHdEntity entity = this.app.ActiveDocument.GetObjectById(this.LastAppliedPolyLineID);
                if (entity != null)
                {
                    entity.Erase();
                }

                //Erase the selected polyline on screen
                //this is because user may modify the X, Y 
                //co-ordinates using this dialog
                entity = this.app.ActiveDocument.GetObjectById(this.MainPolyLineID);
                if (entity != null)
                {
                    entity.Erase();
                }

                List<VVIP> listParams = GetParamData();
                List<CPoint3D> polypts = new List<CPoint3D>();

                foreach (VVIP vdata in listParams)
                {
                    polypts.Add(new CPoint3D(vdata.chainx * cfg.XMetric, vdata.chainy * cfg.YMetric, 0));
                }

                this.MainPolyLineID = this.app.ActiveDocument.DrawPolyline3D(polypts).ObjectID;

                this.WriteDataFile();

                ViewerUtils.DeleteFileIfExists(Path.Combine(this.app.AppDataPath, "VAL2.TMP"));

                CValignUtil util = new CValignUtil();
                if (util.Funcmain(this.app.AppDataPath))
                {
                    DrawingUtil.DrawData(this.app, Path.Combine(this.app.AppDataPath, "VAL2.TMP"), "PVR", true);
                }

                this.app.ActiveDocument.RefreshDocument();
            }
        }

        #region EVENT HANDLERS
        private void btnDelete__Click(object sender, EventArgs e)
        {
            if (this.dataGridParams_.SelectedRows.Count == 1)
            {
                int iIndex = this.dataGridParams_.SelectedRows[0].Index;
                this.dtParams.Rows.RemoveAt(iIndex);
                UpadteGradeValue();
            }
        }

        private void btnInsert__Click(object sender, EventArgs e)
        {
            if (this.dataGridParams_.SelectedRows.Count == 1)
            {
                VVIP vdata = new VVIP();
                vdata.sym = true;
                FormEditValign formedit = new FormEditValign(vdata);
                formedit.Owner = this;

                if (formedit.ShowDialog() == DialogResult.OK)
                {
                    int iIndex = this.dataGridParams_.SelectedRows[0].Index;

                    DataRow row = dtParams.NewRow();
                    row[strColIndex] = 0;
                    row[strColVipChain] = formedit.VVIPData.chainx;
                    row[strColVipLevel] = formedit.VVIPData.chainy;
                    row[strColGrade] = 0;
                    row[strColSymmetrical] = formedit.VVIPData.sym;
                    row[strColVCL1] = formedit.VVIPData.vcl1;
                    row[strColVCL2] = formedit.VVIPData.vcl2;

                    this.dtParams.Rows.InsertAt(row, iIndex);
                    this.UpadteGradeValue();
                }
               
            }
        }

        private void btnEdit__Click(object sender, EventArgs e)
        {
            if (this.dataGridParams_.SelectedRows.Count == 1)
            {
                int iIndex = this.dataGridParams_.SelectedRows[0].Index;
                VVIP vdata = this.GetRow(iIndex);
                FormEditValign formedit = new FormEditValign(vdata);
                formedit.Owner = this;
                if (formedit.ShowDialog() == DialogResult.OK)
                {
                    DataRow row = this.dtParams.Rows[iIndex];

                    //row[strColIndex] = 0;
                    row[strColVipChain] = formedit.VVIPData.chainx;
                    row[strColVipLevel] = formedit.VVIPData.chainy;
                    //row[strColGrade] = 0;
                    row[strColSymmetrical] = formedit.VVIPData.sym;
                    row[strColVCL1] = formedit.VVIPData.vcl1;
                    row[strColVCL2] = formedit.VVIPData.vcl2;
                    this.UpadteGradeValue();
                }
            }     
        }

        private void checkBoxDVCL_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxDVCL.Checked)
            {
                this.tbDefaultVertCurveLen_.Enabled = false;
            }
            else
            {
                //this.tbDefaultVertCurveLen_.Value = FormValignment.DefaultVCurveLen;
                this.tbDefaultVertCurveLen_.Enabled = true;

            }

            //if (!this.checkBoxDVCL.Checked)
            //{
            //    this.tbDefaultVertCurveLen_.Value = FormValignment.DefaultVCurveLen;
            //}
            for (int i = 2; i < this.dtParams.Rows.Count; i++)
            {
                DataRow row = this.dtParams.Rows[i - 1];
                row[strColVCL1] = this.tbDefaultVertCurveLen_.Value;
            }
        }       

        private void dataGridParams__DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            dataGridParams_.CancelEdit();         
        }

        private void btnApply__Click(object sender, EventArgs e)
        {
            this.ApplyValign();
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.errorProvider.Clear();
        }            

        private void dataGridParams__CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.dataGridParams_.Columns[FormValignment.strColVCL2].Index)
            {
                bool bVal = (bool)this.dtParams.Rows[e.RowIndex][FormValignment.strColSymmetrical];
                if (bVal)
                {
                    dataGridParams_.CancelEdit();
                }
            }
            else if (e.ColumnIndex == this.dataGridParams_.Columns[FormValignment.strColSymmetrical].Index)
            {
                bool bVal = (bool)this.dtParams.Rows[e.RowIndex][FormValignment.strColSymmetrical];
                if (bVal)
                {
                    this.dtParams.Rows[e.RowIndex][FormValignment.strColVCL2] = 0;                    
                }
            }
        }
       
        private void btnCancel__Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnFinish__Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            CValignUtil.SaveValign(this.app.AppDataPath);

            ViewerUtils.DeleteFileIfExists(Path.Combine(this.app.AppDataPath, "VALIGN.TMP"));
            this.Close();
        }        

        private void dataGridParams__CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.dataGridParams_.Columns[FormValignment.strColIndex].Index)
            {
                this.dataGridParams_.Rows[e.RowIndex].Selected = true;
            }
        }
        #endregion
    }
}