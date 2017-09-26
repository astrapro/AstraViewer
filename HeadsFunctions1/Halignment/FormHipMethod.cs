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
using HeadsFunctions1.Properties;
using System.IO;
using HeadsFunctions1;

namespace HeadsFunctions1.Halignment
{    

    internal partial class FormHipMethod : Form
    {
        
        //private List<CHHipData> m_listHhip = new List<CHHipData>();
        private const double m_DefaultRadious = 2000;
        //It stores the object id of the polyline drawn on click of apply button 
        private int m_iLastAppliedPolyLine = 0;
        //It stores the object id of the main source polyline on screen  
        private int m_iMainPolyLine = 0;

        private IHeadsApplication m_app;

        DataTable dtParams = null;
        const string ColNameIndex = "HIP #";
        const string ColNameXValue = "X Value";
        const string ColNameYValue = "Y Value";
        const string ColNameDeviation = "Deviation";
        const string ColNameRadius = "Radius";
        const string ColNameLeadTrans = "Lead Trans";
        const string ColNameTrailTrans = "Trail Trans";

        public FormHipMethod(IHeadsApplication app, IHdPolyline3D polyline)
        {
            InitializeComponent();

            this.m_app = app;
            this.InitGrid();
            List<CHHipData> listHhip = new List<CHHipData>();
            this.m_iMainPolyLine = this.GetPolylineDetails(polyline, ref listHhip);

            foreach(CHHipData hdata in listHhip)
            {
                this.AddRow(hdata);
            }
            this.UpdateDeltaValue();

            checkBoxDefaultRead_.Checked = true;
        }

        public FormHipMethod(IHeadsApplication app, CHIPInfo hipinfo)
        {
            InitializeComponent();

            this.m_app = app;
            this.InitGrid();
            List<CHHipData> listHhip = new List<CHHipData>();            
            GetDetailsFromExistingData(hipinfo, ref listHhip);
            foreach (CHHipData hdata in listHhip)
            {
                this.AddRow(hdata);
            }

            this.txtModelName_.ReadOnly = true;
            this.txtStringlabel_.ReadOnly = true;            

            //draw the polyline
            ApplyHIP();
            checkBoxDefaultRead_.Checked = false;
            this.UpdateRadiousValue();
            this.UpdateDeltaValue();

            this.m_app.ActiveDocument.RefreshDocument();
            this.m_app.ZoomExtents();
        }

        private void EnableHipParamButtons(bool bEnable)
        {
            if (bEnable == true && this.dtParams.Rows.Count == 0)
            {
                bEnable = false;
            }
            this.btnInsert_.Enabled = bEnable;
            this.btnEdit_.Enabled = bEnable;
            this.btnDelete_.Enabled = bEnable;
        }

        void InitGrid()
        {
            this.dtParams = new DataTable("HIP Params");

            //Prepare columns
            this.dtParams.Columns.Add(FormHipMethod.ColNameIndex, typeof(int));
            this.dtParams.Columns.Add(FormHipMethod.ColNameXValue, typeof(double));
            this.dtParams.Columns.Add(FormHipMethod.ColNameYValue, typeof(double));
            this.dtParams.Columns.Add(FormHipMethod.ColNameDeviation, typeof(double));
            this.dtParams.Columns.Add(FormHipMethod.ColNameRadius, typeof(double));
            this.dtParams.Columns.Add(FormHipMethod.ColNameLeadTrans, typeof(double));
            this.dtParams.Columns.Add(FormHipMethod.ColNameTrailTrans, typeof(double));


            this.dataGridParams_.DataSource = dtParams;

            this.dataGridParams_.Columns[FormHipMethod.ColNameIndex].ReadOnly = true;
            this.dataGridParams_.Columns[FormHipMethod.ColNameDeviation].ReadOnly = true;
            this.dataGridParams_.Columns[FormHipMethod.ColNameXValue].ReadOnly = true;
            this.dataGridParams_.Columns[FormHipMethod.ColNameYValue].ReadOnly = true;

            this.dataGridParams_.Columns[FormHipMethod.ColNameXValue].DefaultCellStyle.Format = "f5";
            this.dataGridParams_.Columns[FormHipMethod.ColNameYValue].DefaultCellStyle.Format = "f5";

            this.dataGridParams_.Columns[FormHipMethod.ColNameIndex].DefaultCellStyle.BackColor = Color.LightGray;
            this.dataGridParams_.Columns[FormHipMethod.ColNameDeviation].DefaultCellStyle.BackColor = Color.LightGray;
            //this.dataGridParams_.Columns[FormHipMethod.ColNameXValue].DefaultCellStyle.BackColor = Color.LightGray;
            //this.dataGridParams_.Columns[FormHipMethod.ColNameYValue].DefaultCellStyle.BackColor = Color.LightGray;

            this.dataGridParams_.Columns[FormHipMethod.ColNameIndex].Frozen = true;

            this.dataGridParams_.Columns[FormHipMethod.ColNameIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridParams_.Columns[FormHipMethod.ColNameXValue].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridParams_.Columns[FormHipMethod.ColNameYValue].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridParams_.Columns[FormHipMethod.ColNameDeviation].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridParams_.Columns[FormHipMethod.ColNameRadius].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridParams_.Columns[FormHipMethod.ColNameLeadTrans].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridParams_.Columns[FormHipMethod.ColNameTrailTrans].SortMode = DataGridViewColumnSortMode.NotSortable;

            int iWidth = this.dataGridParams_.Width;
            this.dataGridParams_.Columns[FormHipMethod.ColNameIndex].Width = (int)(iWidth * 0.08);
            this.dataGridParams_.Columns[FormHipMethod.ColNameXValue].Width = (int)(iWidth * 0.14);
            this.dataGridParams_.Columns[FormHipMethod.ColNameYValue].Width = (int)(iWidth * 0.14);
            this.dataGridParams_.Columns[FormHipMethod.ColNameDeviation].Width = (int)(iWidth * 0.18);
            this.dataGridParams_.Columns[FormHipMethod.ColNameRadius].Width = (int)(iWidth * 0.15);
            this.dataGridParams_.Columns[FormHipMethod.ColNameLeadTrans].Width = (int)(iWidth * 0.14);
            this.dataGridParams_.Columns[FormHipMethod.ColNameTrailTrans].Width = (int)(iWidth * 0.14);

        }
        void AddRow(CHHipData data)
        {
            int rows = dtParams.Rows.Count;

            DataRow row = dtParams.NewRow();
            row[ColNameIndex] = rows;
            row[ColNameXValue] = data.xvalue;
            row[ColNameYValue] = data.yvalue;
            row[ColNameDeviation] = data.delta;
            row[ColNameRadius] = data.radius;
            row[ColNameLeadTrans] = data.leadtrans;
            row[ColNameTrailTrans] = data.trailtrans;

            dtParams.Rows.Add(row);
        }
        void UpdateRadiousValue()
        {
            if (checkBoxDefaultRead_.Checked)
            {
                this.txtDefaultRadious_.Enabled = false;
                double dRadius = this.txtDefaultRadious_.Value;
                for (int iIndex = 2; iIndex < this.dtParams.Rows.Count; iIndex++)
                {
                    GetDelta((double)this.dtParams.Rows[iIndex - 2][FormHipMethod.ColNameXValue]
                        , (double)this.dtParams.Rows[iIndex - 2][FormHipMethod.ColNameYValue]
                        , (double)this.dtParams.Rows[iIndex - 1][FormHipMethod.ColNameXValue]
                        , (double)this.dtParams.Rows[iIndex - 1][FormHipMethod.ColNameYValue]
                        , (double)this.dtParams.Rows[iIndex][FormHipMethod.ColNameXValue]
                        , (double)this.dtParams.Rows[iIndex][FormHipMethod.ColNameYValue]
                        , ref dRadius);

                    this.dtParams.Rows[iIndex - 1][FormHipMethod.ColNameRadius] = dRadius;
                }
            }
            else
            {
                //this.txtDefaultRadious_.Value = m_DefaultRadious;
                this.txtDefaultRadious_.Enabled = true;
            }

            //if (checkBoxDefaultRead_.Checked)
            //{
            //    this.txtDefaultRadious_.Enabled = false;                
            //}
            //else
            //{
            //    //this.txtDefaultRadious_.Value = m_DefaultRadious;
            //    this.txtDefaultRadious_.Enabled = true;
            //}

            //double dRadius = this.txtDefaultRadious_.Value;
            //for (int iIndex = 2; iIndex < this.dtParams.Rows.Count; iIndex++)
            //{
            //    GetDelta((double)this.dtParams.Rows[iIndex - 2][FormHipMethod.ColNameXValue]
            //        , (double)this.dtParams.Rows[iIndex - 2][FormHipMethod.ColNameYValue]
            //        , (double)this.dtParams.Rows[iIndex - 1][FormHipMethod.ColNameXValue]
            //        , (double)this.dtParams.Rows[iIndex - 1][FormHipMethod.ColNameYValue]
            //        , (double)this.dtParams.Rows[iIndex][FormHipMethod.ColNameXValue]
            //        , (double)this.dtParams.Rows[iIndex][FormHipMethod.ColNameYValue]
            //        , ref dRadius);

            //    this.dtParams.Rows[iIndex - 1][FormHipMethod.ColNameRadius] = dRadius;
            //}            
            ////UpdateParamList();
        }
        void UpdateDeltaValue()
        {
            for (int iIndex = 2; iIndex < this.dtParams.Rows.Count; iIndex++)
            {
                double dRadius = 0;

                double dDelta = GetDelta((double)this.dtParams.Rows[iIndex - 2][FormHipMethod.ColNameXValue]
                    , (double)this.dtParams.Rows[iIndex - 2][FormHipMethod.ColNameYValue]
                    , (double)this.dtParams.Rows[iIndex - 1][FormHipMethod.ColNameXValue]
                    , (double)this.dtParams.Rows[iIndex - 1][FormHipMethod.ColNameYValue]
                    , (double)this.dtParams.Rows[iIndex][FormHipMethod.ColNameXValue]
                    , (double)this.dtParams.Rows[iIndex][FormHipMethod.ColNameYValue]
                    , ref dRadius);

                this.dtParams.Rows[iIndex - 1][FormHipMethod.ColNameDeviation] = dDelta;
            } 
        }
        void UpdateIndexValue()
        {
            for (int iIndex = 0; iIndex < this.dtParams.Rows.Count; iIndex++)
            {
                this.dtParams.Rows[iIndex][FormHipMethod.ColNameIndex] = iIndex;
            } 
        }
        CHHipData GetRow(int iIndex)
        {
            CHHipData data = new CHHipData();
            data.xvalue = (double)this.dtParams.Rows[iIndex][FormHipMethod.ColNameXValue];
            data.yvalue = (double)this.dtParams.Rows[iIndex][FormHipMethod.ColNameYValue];
            data.delta = (double)this.dtParams.Rows[iIndex][FormHipMethod.ColNameDeviation];
            data.radius = (double)this.dtParams.Rows[iIndex][FormHipMethod.ColNameRadius];
            data.leadtrans = (double)this.dtParams.Rows[iIndex][FormHipMethod.ColNameLeadTrans];
            data.trailtrans = (double)this.dtParams.Rows[iIndex][FormHipMethod.ColNameTrailTrans];
            return data;
        }


        private void GetDetailsFromExistingData(CHIPInfo hipinfo, ref List<CHHipData> listHhip)
        {
            if (hipinfo.DataList.Count > 0)
            {
                this.txtModelName_.Text = hipinfo.ModelName;
                this.txtStringlabel_.Text = hipinfo.StringLabel;
                this.txtStartInterval_.Value = hipinfo.DataList[0].dStartchn;
                this.txtDefaultRadious_.Value = hipinfo.DataList[0].dRadius;
                listHhip = CHalignHipUtil.GenerateHIPData(hipinfo);
            }            
        }

        private int GetPolylineDetails(IHdPolyline3D polyline, ref List<CHHipData> listHhip)
        {
            int iPolyLineObjID = 0;
            listHhip.Clear();
            if (polyline != null)
            {
                //Store the main source polyline
                iPolyLineObjID = polyline.ObjectID;

                CPoint3D[] ptCords = polyline.Coordinates;
                for (int iIndex = 0; iIndex < ptCords.Length; iIndex++)
                {
                    CHHipData hhipObj = new CHHipData();
                    hhipObj.xvalue = ptCords[iIndex].X;
                    hhipObj.yvalue = ptCords[iIndex].Y;
                    listHhip.Add(hhipObj);
                }
            }

            return iPolyLineObjID;
        }
        
        private double GetDelta(double x1, double y1, double x2, double y2, double x3, double y3, ref double radius)
        {
	        double theta, delta, B1, B2, r9;

	        theta = 0.0;
	        delta = 0.0;
	        B1=0.0;
	        B2=0.0;
	        r9 = 57.297854;
        	

	        //FIRST Straight 

	        if(x1==x2)x2+=0.000001;

            //theta= atan((y2-y1)/(x2-x1));
            //theta=fabs(theta*r9);
            theta = System.Math.Atan((y2 - y1) / (x2 - x1));
            theta = System.Math.Abs(theta * r9);
        	
	        if(x2>=x1 && y2>=y1)
		        B1 = 90.0-theta;
	        else if(x2>=x1 && y2<=y1)
		        B1 = 90.0 + theta;
	        else if(x2<=x1 && y2<=y1)
		        B1 = 270.0 -  theta;
	        else if(x2<=x1 && y2>=y1)
		        B1 = 270.0 + theta;

	        if(B1 < 0.0)
		        B1 = B1 + 360.0;
	        if(B1 > 360.0)
		        B1 = B1 - 360.0;

	        //SECOND Straight 

	        if(x2==x3)x3+=0.000001;

            //theta=atan((y3-y2)/(x3-x2));
            //theta=fabs(theta*r9);
            theta = System.Math.Atan((y3 - y2) / (x3 - x2));
            theta = System.Math.Abs(theta * r9);

	        if(x3>=x2 && y3>=y2)
		        B2 = 90.0 - theta;
	        else if(x3>=x2 && y3<=y2)
		        B2 = 90.0 + theta;
	        else if(x3<=x2 && y3<=y2)
		        B2 = 270.0 - theta;
	        else if(x3<=x2 && y3>=y2)
		        B2 = 270.0 + theta;

	        if(B2 < 0.0)
		        B2 = B2 + 360.0;
	        if(B2 > 360.0)
		        B2 = B2 - 360.0;

	        //  APPLY SIGN ON TURN  
        		

	        //  Calculate Deviation Angle delta 

            //delta = fabs(B2 - B1);
            //radius = fabs(radius);
            delta = System.Math.Abs(B2 - B1);
            radius = System.Math.Abs(radius);

	        double BSign=1.0;

            if (B2 >= 0.0 && B2 <= 90.0 && B1 >= 90.0 && B1 <= 180.0)
                BSign = -1.0;
            else if (B2 >= 270.0 && B2 <= 360.0 && B1 >= 0.0 && B1 <= 90.0)
                BSign = -1.0;
            else if (B2 >= 180.0 && B2 <= 270.0 && B1 >= 270.0 && B1 <= 360.0)
                BSign = -1.0;
            else if (B2 >= 90.0 && B2 <= 180.0 && B1 >= 180.0 && B1 <= 270.0)
                BSign = -1.0;
            // New as on SPM
            else if (B1 >= 0.0 && B1 <= 90.0 && B2 >= 0.0 && B2 <= 90.0 && B1 > B2)
                BSign = -1.0;
            else if (B1 >= 90.0 && B1 <= 180.0 && B2 >= 90.0 && B2 <= 180.0 && B1 > B2)
                BSign = -1.0;
            else if (B1 >= 180.0 && B1 <= 270.0 && B2 >= 180.0 && B2 <= 270.0 && B1 > B2)
                BSign = -1.0;
            else if (B1 >= 270.0 && B1 <= 360.0 && B2 >= 270.0 && B2 <= 360.0 && B1 > B2)
                BSign = -1.0;

	        delta = delta * BSign;
	        radius = radius * BSign;

	        return delta;
        }

        private bool ValidateData()
        {
            if (this.txtModelName_.Text.Trim() == "")
            {
                this.errorProvider.SetError(this.txtModelName_, Properties.Resources.ST_PROMPT_INVALID_INPUT);
                this.txtModelName_.Focus();
                return false;
            }

            if (this.txtStringlabel_.Text.Trim() == "")
            {
                this.errorProvider.SetError(this.txtStringlabel_, Properties.Resources.ST_PROMPT_INVALID_INPUT);
                this.txtStringlabel_.Focus();
                return false;
            }

            if (this.txtModelName_.ReadOnly == false)
            {
                string strHalignFilFilePath = Path.Combine(this.m_app.AppDataPath, "HALIGN.FIL");
                CHIPInfo[] infoarr = CHalignHipUtil.ReadHaligns(strHalignFilFilePath);
                foreach (CHIPInfo info in infoarr)
                {
                    if (info.ModelName.Trim().ToLower() == this.txtModelName_.Text.Trim().ToLower()
                        && info.StringLabel.Trim().ToLower() == this.txtStringlabel_.Text.Trim().ToLower())
                    {
                        this.errorProvider.SetError(this.txtStringlabel_
                            , "Model Name and String Level already exists\nPlease specify unique Model Name and String Level");
                        this.txtStringlabel_.Focus();
                        return false;
                    }
                }
            }
            return true;
        }

        private void ApplyHIP()
        {
            this.m_app.ActiveDocument.ConfigParam.XMetric = 1;
            this.m_app.ActiveDocument.ConfigParam.YMetric = 1;

            //Delete existing hip line
            IHdEntity entity = this.m_app.ActiveDocument.GetObjectById(this.m_iLastAppliedPolyLine);
            if (entity != null)
            {
                entity.Erase();
            }

            //Erase the selected polyline on screen
            //this is because user may modify the X, Y 
            //co-ordinates using this dialog
            entity = this.m_app.ActiveDocument.GetObjectById(this.m_iMainPolyLine);
            if (entity != null)
            {
                List<CHHipData> listLinedata = new List<CHHipData>();
                GetPolylineDetails((IHdPolyline3D)entity, ref listLinedata);
                int temCount = this.dtParams.Rows.Count;
                for(int iCnt = 0; iCnt < temCount; iCnt++)
                {
                    CHHipData hdata = listLinedata[iCnt];

                    this.dtParams.Rows[iCnt][FormHipMethod.ColNameXValue] = hdata.xvalue;
                    this.dtParams.Rows[iCnt][FormHipMethod.ColNameYValue] = hdata.yvalue;                    
                }
                UpdateDeltaValue();

                entity.Erase();                
            }
            //Redraw the polyline
            List<CPoint3D> listPt = new List<CPoint3D>();
            for (int iCnt = 0; iCnt < this.dtParams.Rows.Count; iCnt++)
            {
                CHHipData data = this.GetRow(iCnt);
                listPt.Add(new CPoint3D(data.xvalue, data.yvalue, 0));
            }
            this.m_iMainPolyLine = this.m_app.ActiveDocument.DrawPolyline3D(listPt).ObjectID;

                        
            //Refresh the document
            this.m_app.ActiveDocument.RefreshDocument();


            List<CHHipData> listHips = new List<CHHipData>();
            for (int iCnt = 0; iCnt < this.dtParams.Rows.Count; iCnt++)
            {
                listHips.Add(this.GetRow(iCnt));
            }

            CHalFile filedata = new CHalFile();
            filedata.Code = 1;
            filedata.AcceptCode = 0;
            filedata.ModelName = this.txtModelName_.Text.Trim();
            filedata.StringLevel = this.txtStringlabel_.Text.Trim();
            filedata.StartChainage = this.txtStartInterval_.Value;
            filedata.ChainageInterval = this.txtChainageInterval_.Value;
            filedata.ListHip = listHips;

            string strHipFilePath = Path.Combine(m_app.AppDataPath, "HAL1.tmp");
            CHalignHipUtil util = new CHalignHipUtil();
            if (util.WriteHIPFile(strHipFilePath, filedata))
            {
                //Delete HAL2.TEMP
                string strHAL2TmpPath = Path.Combine(m_app.AppDataPath, "HAL2.tmp");
                ViewerUtils.DeleteFileIfExists(strHAL2TmpPath);
                
                //try
                {
                    if (util.FuncMain(this.m_app.AppDataPath) == true)
                    {
                        this.m_iLastAppliedPolyLine = DrawingUtil.DrawData(this.m_app, strHAL2TmpPath, "PVR", true);
                        this.m_app.ActiveDocument.RefreshDocument();

                        //this.m_app.ZoomExtents();
                    }                    
                }
                //catch (Exception ex)
                //{
                //    MessageBox.Show(this, ex.ToString(), "ERROR!"
                //        , MessageBoxButtons.OK
                //        , MessageBoxIcon.Error);
                //    System.Diagnostics.Trace.Write(ex.ToString());
                //}
            }
            else
            {
                MessageBox.Show(this
                    , "Failed to generate HIP file"
                    , Resources.ST_ERROR_MSGBOX_CAPTION
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
            }

            this.ShowDetails(checkBoxShowDetails_.Checked);
        }

        private void ShowDetails(bool bShow)
        {
            string strHipLabel = HeadsUtils.Constants.LABEL_DETAILS + ":HIP";
            ViewerUtils.DeleteEntitiesByLabel(this.m_app.ActiveDocument, strHipLabel, true);
            if (bShow)
            {
                double dTextHeight = 5.0;
                double dZVal = 5.0;
                double dYPos = 0;
                double dXPos = 0;
                for (int iCnt = 0; iCnt < this.dtParams.Rows.Count; iCnt++)
                {
                    CHHipData hipdata = this.GetRow(iCnt);

                    dYPos = hipdata.yvalue - (dTextHeight*1.5);
                    dXPos = hipdata.xvalue + 2.5;

                    string strLine = "IP# " + iCnt.ToString();
                    CPoint3D pt = new CPoint3D(dXPos, dYPos, dZVal);
                    IHdText textObj = this.m_app.ActiveDocument.DrawText(pt, strLine, dTextHeight);
                    textObj.Label = strHipLabel;

                    dYPos = dYPos - (dTextHeight * 1.5);

                    strLine = "X : " + hipdata.xvalue.ToString("0.000");
                    pt = new CPoint3D(dXPos, dYPos, dZVal);
                    textObj = this.m_app.ActiveDocument.DrawText(pt, strLine, dTextHeight);
                    textObj.Label = strHipLabel;

                    dYPos = dYPos - (dTextHeight * 1.5);

                    strLine = "Y : " + hipdata.yvalue.ToString("0.000");
                    pt = new CPoint3D(dXPos, dYPos, dZVal);
                    textObj = this.m_app.ActiveDocument.DrawText(pt, strLine, dTextHeight);
                    textObj.Label = strHipLabel;
                }              
            }

            this.m_app.ActiveDocument.RefreshDocument();
        }        

        private void checkBoxDefaultRead__CheckedChanged(object sender, EventArgs e)
        {
            UpdateRadiousValue();
        }
        
        private void btnInsert__Click(object sender, EventArgs e)
        {
            //EnableHipParamButtons(false);
            if (this.dataGridParams_.SelectedRows.Count == 1)
            {                
                int iIndex = this.dataGridParams_.SelectedRows[0].Index;
                CHHipData hhipdata = new CHHipData();
                if (iIndex == 0 || iIndex == (this.dtParams.Rows.Count - 1))
                {
                    hhipdata.radius = 0;
                }
                else
                {
                    hhipdata.radius = (double)this.dtParams.Rows[iIndex][ColNameRadius];
                }
                FormHipEdit formhipedit = new FormHipEdit(hhipdata, Resources.ST_HIP_INSERT);
                formhipedit.Owner = this;
                if (formhipedit.ShowDialog() == DialogResult.OK)
                {
                    DataRow row = dtParams.NewRow();
                    row[ColNameIndex] = 0;
                    row[ColNameXValue] = formhipedit.HipData.xvalue;
                    row[ColNameYValue] = formhipedit.HipData.yvalue;
                    row[ColNameDeviation] = formhipedit.HipData.delta;
                    row[ColNameRadius] = formhipedit.HipData.radius;
                    row[ColNameLeadTrans] = formhipedit.HipData.leadtrans;
                    row[ColNameTrailTrans] = formhipedit.HipData.trailtrans;

                    this.dtParams.Rows.InsertAt(row, iIndex);

                    this.UpdateIndexValue();
                    this.UpdateDeltaValue();

                }                
            }            
        }

        private void btnEdit__Click(object sender, EventArgs e)
        {
            //EnableHipParamButtons(false);
            if (this.dataGridParams_.SelectedRows.Count == 1)
            {
                int iIndex = this.dataGridParams_.SelectedRows[0].Index;
                CHHipData hhipdata = this.GetRow(iIndex);
                FormHipEdit formhipedit = new FormHipEdit(hhipdata, Resources.ST_HIP_EDIT);
                formhipedit.Owner = this;
                if (formhipedit.ShowDialog() == DialogResult.OK)
                {
                    DataRow row = this.dtParams.Rows[iIndex];

                    row[ColNameXValue] = formhipedit.HipData.xvalue;
                    row[ColNameYValue] = formhipedit.HipData.yvalue;
                    row[ColNameDeviation] = formhipedit.HipData.delta;
                    row[ColNameRadius] = formhipedit.HipData.radius;
                    row[ColNameLeadTrans] = formhipedit.HipData.leadtrans;
                    row[ColNameTrailTrans] = formhipedit.HipData.trailtrans;
                    this.UpdateDeltaValue();
                }
            }          
        }        

        private void btnDelete__Click(object sender, EventArgs e)
        {
            //EnableHipParamButtons(false);

            if (this.dataGridParams_.SelectedRows.Count == 1)
            {
                int iIndex = this.dataGridParams_.SelectedRows[0].Index;
                this.dtParams.Rows.RemoveAt(iIndex);
                this.UpdateIndexValue();
                this.UpdateDeltaValue();
            }                 
        }

        private void btnSave__Click(object sender, EventArgs e)
        {
            string strHalignTmpFilePath = Path.Combine(this.m_app.AppDataPath, "halign.tmp");
            if (File.Exists(strHalignTmpFilePath) == true)
            {
                CHalignHipUtil.SaveHalign(this.m_app.AppDataPath);
                //throw new FileNotFoundException("File not found : " + strHalignTmpFilePath);
            }

            

            //File.Delete(strHalignTmpFilePath);

            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (this.ValidateData())
            {
                this.ApplyHIP();
            }            
        }

        private void btnCancel__Click(object sender, EventArgs e)
        {
            this.Close();
        }
           

        private void checkBoxShowDetails__CheckedChanged(object sender, EventArgs e)
        {
            this.ShowDetails(checkBoxShowDetails_.Checked);
        }

        private void txtModelName__TextChanged(object sender, EventArgs e)
        {
            this.errorProvider.Clear();
        }

        private void txtStringlabel__TextChanged(object sender, EventArgs e)
        {
            this.errorProvider.Clear();
        }

        private void FormHipMethod_FormClosed(object sender, FormClosedEventArgs e)
        {
            string strHalignTmpFilePath = Path.Combine(this.m_app.AppDataPath, "halign.tmp");
            ViewerUtils.DeleteFileIfExists(strHalignTmpFilePath);
        }

        private void dataGridParams__DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            this.dataGridParams_.CancelEdit();
        }

        private void dataGridParams__CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.dataGridParams_.Columns[FormHipMethod.ColNameRadius].Index
                || e.ColumnIndex == this.dataGridParams_.Columns[FormHipMethod.ColNameLeadTrans].Index
                || e.ColumnIndex == this.dataGridParams_.Columns[FormHipMethod.ColNameTrailTrans].Index)
            {
                if (e.RowIndex == 0 || e.RowIndex == this.dtParams.Rows.Count -1)
                {
                    dataGridParams_.CancelEdit();
                }
            }
        }

        private void dataGridParams__CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.dataGridParams_.Columns[FormHipMethod.ColNameIndex].Index)
            {
                this.dataGridParams_.Rows[e.RowIndex].Selected = true;
            }
        }      
                    
    }
}