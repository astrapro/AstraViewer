//Jay Shree Krishna
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using HeadsUtils;
using System.Threading;
using HeadsUtils.Interfaces;

namespace HeadsFunctions1.DrawString
{
    internal partial class FormDrawString : Form
    {
        private string m_strpathlistfile = "";
        private string m_strpathfilfile = "";
        private IHeadsApplication m_app;
        private Thread m_threadDraw;

        delegate void GenericFunCall();
        delegate void SetProgressValue(int iValue);
        delegate string RetStrFunCall();

        public FormDrawString(IHeadsApplication app)
        {
            InitializeComponent();

            this.UpdateUIWithProductType();

            this.progressBar_.Minimum = 0;
            this.progressBar_.Maximum = 100;



            //set the test data path    
            m_strpathlistfile = Path.Combine(app.AppDataPath, "model.lst");
            m_strpathfilfile = Path.Combine(app.AppDataPath, "model.fil");
                        
            m_app = app;

            this.tbScaleX_.Value = this.m_app.ActiveDocument.ConfigParam.XMetric;
            this.tbScaleY_.Value = this.m_app.ActiveDocument.ConfigParam.YMetric;

            //Fill available models from file
            FillModels();
        }        
        
        #region Private Member
        private void UpdateUIWithProductType()
        {
            if (HeadsUtils.Constants.BuildType == eHEADS_RELEASE_TYPE.ASTRA)
            {
                this.Text = "Draw Model";
                UpdateUIWithProductType(this);
            }
        }
        private void UpdateUIWithProductType(Control ctrl)
        {
            foreach (Control ctrlitem in ctrl.Controls)
            {
                if (ctrlitem.Text.IndexOf("String Labels") > -1)
                {
                    ctrlitem.Text = ctrlitem.Text.Replace("String Labels", "Features");
                }
                else if (ctrlitem.Text.IndexOf("string labels") > -1)
                {
                    ctrlitem.Text = ctrlitem.Text.Replace("string labels", "features");
                }
                else if (ctrlitem.Text.IndexOf("String Label") > -1)
                {
                    ctrlitem.Text = ctrlitem.Text.Replace("String Label", "Feature");
                }
                else if (ctrlitem.Text.IndexOf("string labels") > -1)
                {
                    ctrlitem.Text = ctrlitem.Text.Replace("string label", "feature");
                }

                if (ctrlitem.Controls.Count > 0)
                {
                    UpdateUIWithProductType(ctrlitem);
                }
            }
        }
        private void FillModels()
        {
            List<string> listmodels = new List<string>();
            List<ClSTtype> listmodeldata = new List<ClSTtype>();

            ViewerUtils.ReadModelLstFile(m_strpathlistfile, ref listmodeldata);

            foreach (ClSTtype data in listmodeldata)
            {
                if (listmodels.Contains(data.strMod1) == false)
                {
                    listmodels.Add(data.strMod1);
                }
            }
            foreach (string strModel in listmodels)
            {
                if (this.cbModelName_.Items.Contains(strModel) == false)
                {
                    this.cbModelName_.Items.Add(strModel);
                }
            }

            if (this.cbModelName_.Items.Count > 0)
            {
                this.cbModelName_.SelectedIndex = 0;
            }

            
        }
        private bool IsRunning
        {
            get
            {
                return (m_threadDraw != null && m_threadDraw.IsAlive == true) ? true : false;
            }
        }
        private void StartDraw()
        {
            if (m_threadDraw == null || m_threadDraw.IsAlive == false)
            {
                m_threadDraw = new Thread(new ThreadStart(DrawThreadProc));
                m_threadDraw.IsBackground = true;
                m_threadDraw.Name = "DrawStringThread";
                m_threadDraw.Priority = ThreadPriority.Highest;
                m_threadDraw.Start();
            }
        }
        private void StopDraw()
        {
            if (this.IsRunning)
            {
                m_threadDraw.Abort();
                m_threadDraw.Join();
            }

        }
        private void DrawThreadProc()
        {
            this.m_app.ActiveDocument.DisableUndoRecording(true);
            BinaryReader br = new BinaryReader(new FileStream(m_strpathfilfile, FileMode.Open, FileAccess.Read), Encoding.Default);
            try
            {
                ExecDrawString(br);
            }
            catch (ThreadAbortException)
            {
                br.Close();
                this.m_app.ActiveDocument.DisableUndoRecording(false);
                this.m_app.ActiveDocument.RefreshDocument();
                this.m_app.ZoomExtents();
                System.Diagnostics.Trace.WriteLine("ThreadAbortException Catched in : " + Thread.CurrentThread.Name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), HeadsUtils.Constants.ProductName);
            }
            br.Close();
            this.m_app.ActiveDocument.DisableUndoRecording(false);
            this.m_app.ActiveDocument.RefreshDocument();
            this.m_app.ZoomExtents();
            CloseFrom();
        }
        
        private string GetSelectedModelName()
        {
            string str = "";
            
            if (this.cbModelName_.InvokeRequired)
            {
                str = this.cbModelName_.Invoke(new RetStrFunCall(GetSelectedModelName)).ToString();
            }
            else 
            {
                str = this.cbModelName_.SelectedItem.ToString();
                return str;
               
            }
            
            return str;
        }
        private void CloseFrom()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new GenericFunCall(CloseFrom));
            }
            else
            {
                this.Close();
            }
        }
        private void SetProgressBarMaxValue(int iMaxValue)
        {
            if (this.progressBar_.InvokeRequired)
            {
                this.progressBar_.Invoke(new SetProgressValue(SetProgressBarMaxValue), new object[] { iMaxValue });
            }
            else
            {
                this.progressBar_.Maximum = iMaxValue;
            }
        }
        private void SetProgressBarValue(int iValue)
        {
            if ((iValue % 2) == 0)
            {
                if (this.progressBar_.InvokeRequired)
                {
                    this.progressBar_.Invoke(new SetProgressValue(SetProgressBarValue), new object[] { iValue });
                }
                else
                {
                    this.progressBar_.Value = iValue;
                }
            }
           
        }
        private int GetSelectedLineType()
        {
            int iLineType = 0;
            if (this.rb3dPolyLine_.Checked)
            {
                iLineType = 1;
            }
            else if  (this.rb3DLine_.Checked)
            {
                iLineType = 2;
            }
            else if (this.rbPoints_.Checked)
            {
                iLineType = 3;
            }
            else if (this.rb3DFace_.Checked)
            {
                iLineType = 4;
            }
            return iLineType;
        }

        private void ExecDrawString(BinaryReader br)
        {            
            bool bIsModel = false;
            bool bIsLabel = false;

            CPoint3D ptStart = new CPoint3D();
            CPoint3D ptEnd = new CPoint3D();
            CPoint3D ptLast= new CPoint3D();
            CPoint3D ptFirst3dFac = new CPoint3D();
            string strSeleModel = GetSelectedModelName();            

            CTXTtype txt = new CTXTtype();
            CLinetype uline = new CLinetype();
            string strModelName = "";
            int NumPts = 0;
            string str1 = "";
            string stgLabel = "";
            IHdPoint aPoint = null;
            IHdPolyline3D polyline = null;
            CPTStype pts = new CPTStype();
            List<CPoint3D> pFArray = new List<CPoint3D>();

            this.m_app.ActiveDocument.ConfigParam.XMetric = 1.0;
            this.m_app.ActiveDocument.ConfigParam.YMetric = 1.0;

            CCfgtype cfg = this.m_app.ActiveDocument.ConfigParam;

            int iSelectedLineType = this.GetSelectedLineType();

            double dTextHeight = (this.m_app.ActiveDocument.ActiveTextHeight > 0) ? this.m_app.ActiveDocument.ActiveTextHeight : 1.0;
            
            
            string strFilePath = m_strpathfilfile;
            //if (File.Exists(strFilePath))
            {
                //BinaryReader br = new BinaryReader(new FileStream(strFilePath, FileMode.Open, FileAccess.Read), Encoding.Default);

                //set the progress bar max value
                SetProgressBarMaxValue(100);                

                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    CLabtype labtype = CLabtype.FromStream(br);

                    if (labtype.attr == CLabtype.Type.Model) // Model Name
                    {
                        NumPts = 0;
                        strModelName = ((CModType)labtype.Tag).Name;                       
                        bIsModel = (strModelName == strSeleModel) ? true : false;                        
                    }
                    else if (labtype.attr == CLabtype.Type.String)// String Label
                    {
                        stgLabel = ((CStgType)labtype.Tag).label; 
                        if (bIsModel)
                        {                            
                            bIsLabel = this.lbStringLebels_.CheckedItems.Contains(stgLabel);                                                  
                        }
                    }
                    else if (labtype.attr == CLabtype.Type.Point)
                    {
                        pts = (CPTStype)labtype.Tag;

                        ptEnd = new CPoint3D(pts.mx * cfg.XMetric, pts.my * cfg.YMetric, pts.mz);
                        NumPts++;

                        ptStart = ptLast;
                        ptLast = ptEnd;
                       
                        if (bIsModel && bIsLabel)
                        {
                            uline.StartPoint = new CPoint3D(ptStart);
                            uline.EndPoint = new CPoint3D(ptEnd);

                            str1 = stgLabel;
                            if (str1.ToUpper().StartsWith("P") == true)
                            {
                                if (str1.ToUpper().StartsWith("P0") == true)
                                {
                                    txt.Point = ptEnd;
                                    //double[] res = ptEnd.GetCordArr();

                                    txt.tr = 0.0;
                                    
                                    str1 = ptEnd.Z.ToString("0.000");
                                    this.m_app.ActiveDocument.DrawText(new CPoint3D(ptEnd.X, ptEnd.Y, dTextHeight), str1, dTextHeight);
                                   
                                    this.m_app.ActiveDocument.DrawLine(new CPoint3D(ptEnd.X-0.5, ptEnd.Y, ptEnd.Z), new CPoint3D(ptEnd.X + 0.5, ptEnd.Y, ptEnd.Z));
                                    this.m_app.ActiveDocument.DrawLine(new CPoint3D(ptEnd.X , ptEnd.Y - 0.5, ptEnd.Z), new CPoint3D(ptEnd.X, ptEnd.Y + 0.5, ptEnd.Z));
                                                                       
                                }
                                else
                                {
                                    txt.Point = ptEnd;
                                    txt.tr = 0.0;
                                    double[] res = ptEnd.GetCordArr();

                                    this.m_app.ActiveDocument.DrawText(new CPoint3D(ptEnd.X, ptEnd.Y, dTextHeight), str1, dTextHeight);
                                    
                                    this.m_app.ActiveDocument.DrawLine(new CPoint3D(ptEnd.X - 0.5, ptEnd.Y, ptEnd.Z), new CPoint3D(ptEnd.X + 0.5, ptEnd.Y, ptEnd.Z));
                                    this.m_app.ActiveDocument.DrawLine(new CPoint3D(ptEnd.X, ptEnd.Y - 0.5, ptEnd.Z), new CPoint3D(ptEnd.X, ptEnd.Y + 0.5, ptEnd.Z));
                                }
                            }
                            else
                            {
                                if (NumPts == 1)
                                {
                                    aPoint = this.m_app.ActiveDocument.DrawPoint(ptEnd);
                                    if (iSelectedLineType == 4)
                                    {
                                        aPoint.Erase();
                                        ptFirst3dFac = new CPoint3D(ptEnd);
                                        pFArray.Add(ptEnd);	
                                    }

                                }
                                else if (NumPts == 2)
                                {
                                    uline.Layer = this.m_app.ActiveDocument.GetActiveLayer().Name;
                                    uline.elatt = 1;
                                    uline.laatt = 1;
                                    uline.Label = strModelName;
                                    uline.Label += ":";
                                    uline.Label += stgLabel;
                                    uline.scatt = 1;

                                    if (iSelectedLineType == 1)
                                    {
                                        polyline = DrawingUtil.DrawPolyline3DDx(this.m_app.ActiveDocument, true, uline);
                                    }
                                    else if (iSelectedLineType == 2)
                                    {
                                        polyline = DrawingUtil.DrawPolyline3DDx(this.m_app.ActiveDocument, true, uline);                                        
                                    }
                                    else if (iSelectedLineType == 3)
                                    {
                                        aPoint = this.m_app.ActiveDocument.DrawPoint(ptEnd);
                                    }
                                    else if (iSelectedLineType == 4)
                                    {                                        
                                        pFArray.Add(ptEnd);   
                                    }

                                    aPoint.Erase();
                                }
                                else if (NumPts > 2)
                                {
                                    if (iSelectedLineType == 1)
                                    {
                                        polyline.AppendVertex(ptEnd);
                                    }
                                    else if (iSelectedLineType == 2)
                                    {
                                        polyline = DrawingUtil.DrawPolyline3DDx(this.m_app.ActiveDocument, true, uline);
                                    }
                                    else if (iSelectedLineType == 3)
                                    {
                                        aPoint = this.m_app.ActiveDocument.DrawPoint(ptEnd);
                                    }
                                    else if (iSelectedLineType == 4)
                                    {
                                        pFArray.Add(ptEnd);
                                    }
                                }
                            }
                        }
                    }
                    else if (labtype.attr == CLabtype.Type.Text)
                    {
                        txt = (CTXTtype)labtype.Tag;

                        ptEnd = new CPoint3D(txt.tx * cfg.XMetric, txt.ty * cfg.YMetric, txt.tz);
                        if (bIsLabel && bIsModel)
                        {
                            str1 = txt.tg;
                            //Display an Elevation
                            //TO DO:
                            this.m_app.ActiveDocument.DrawText(ptEnd, str1, dTextHeight);
                            aPoint = this.m_app.ActiveDocument.DrawPoint(ptEnd);
                        }
                    }
                    else if (labtype.attr == CLabtype.Type.EndCode)
                    {
                        if ((iSelectedLineType == 4) && (bIsLabel && bIsModel))
                        {
                            if ((System.Math.Abs(ptEnd.X - ptFirst3dFac.X) > 0.00001) || (System.Math.Abs(ptEnd.Y - ptFirst3dFac.Y) > 0.00001))
                            {
                                if (NumPts < 4)
                                {
                                    pFArray.Add(ptFirst3dFac);	 //close the face
                                }
                            }

                            if (pFArray.Count > 3)
                            {
                                IHd3DFace face3d = this.m_app.ActiveDocument.Draw3DFace(pFArray[0], pFArray[1], pFArray[2], pFArray[3]);
                                face3d.Update();
                            }
                            pFArray.Clear();                            
                        }
                        NumPts = 0;
                    }
                    int iCurProgressInPercent = (int)(((double)br.BaseStream.Position / (double)br.BaseStream.Length) * 100.00);

                    SetProgressBarValue(iCurProgressInPercent);
                    Thread.Sleep(0);
                }
                //br.Close();
            }
        }


        private void FillLabel(string strModelName)
        {
            List<string> listLabels = new List<string>();
            List<ClSTtype> listmodeldata = new List<ClSTtype>();

            ViewerUtils.ReadModelLstFile(m_strpathlistfile, ref listmodeldata);

            foreach (ClSTtype data in listmodeldata)
            {
                if (data.strMod1.Trim() ==  strModelName.Trim())
                {
                    listLabels.Add(data.strStg);
                }
            }
            this.lbStringLebels_.Items.Clear();
            foreach (string strLabel in listLabels)
            {
                if (strLabel.Trim() != "")
                {
                    if (this.lbStringLebels_.Items.Contains(strLabel) == false)
                    {
                        this.lbStringLebels_.Items.Add(strLabel);
                    }
                }
                
            }
        }        
        private bool ValidateData(bool bValidate)
        {
            bool bSuccess = false;
            if (bValidate)
            {
                if (this.lbStringLebels_.CheckedItems.Count > 0)  
                {
                    bSuccess = true;
                }
                else
                {
                    MessageBox.Show(this, HeadsFunctions1.Properties.Resources.ST_PROMPT_DS_SELECT_STRING_LABEL
                        , HeadsFunctions1.Properties.Resources.ST_ERROR_MSGBOX_CAPTION
                        , MessageBoxButtons.OK
                        , MessageBoxIcon.Information);
                }
            }
            return bSuccess;
        }
        #endregion

        #region UI EVENTS
        private void cbModelName__SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbModelName_.SelectedIndex > -1)
            {
                this.FillLabel(this.cbModelName_.SelectedItem.ToString());
            }
            
        }
        private void btnSelAllStringLabels__Click(object sender, EventArgs e)
        {
            for (int iIndex = 0; iIndex < this.lbStringLebels_.Items.Count; iIndex++)
            {
                this.lbStringLebels_.SetItemChecked(iIndex, true);
            }            
        }
        private void btnDeSelAllStringLabels__Click(object sender, EventArgs e)
        {
            for (int iIndex = 0; iIndex < this.lbStringLebels_.Items.Count; iIndex++)
            {
                this.lbStringLebels_.SetItemChecked(iIndex, false);
            }  
        }
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (this.IsRunning)
            {
                StopDraw();
            }
            else
            {
                this.Close();
            }          
            
        }
        private void OkBtn_Click(object sender, EventArgs e)
        {
            if (this.ValidateData(true))
            {
                this.groupBoxSelection1.Enabled = false;
                this.groupBoxSelection2.Enabled = false;
                this.groupBoxParameters.Enabled = false;
                this.OkBtn.Enabled = false;
                //Set the configuration parameter
                this.m_app.ActiveDocument.ConfigParam.XMetric = this.tbScaleX_.Value;
                this.m_app.ActiveDocument.ConfigParam.YMetric = this.tbScaleY_.Value;

                StartDraw();                       
            }            

        }
        private void btnSelStringLabels__Click(object sender, EventArgs e)
        {
            string strSerach = this.tbSelStringLebel_.Text.Trim();
            if (strSerach != "")
            {
                int iQuesMarkIndex = strSerach.IndexOf('?');
                if (iQuesMarkIndex > 0)
                {
                    strSerach = strSerach.Substring(0, iQuesMarkIndex);
                }

                for (int iIndex = 0; iIndex < this.lbStringLebels_.Items.Count; iIndex++)
                {
                    if (this.lbStringLebels_.Items[iIndex].ToString().StartsWith(strSerach))
                    {
                        this.lbStringLebels_.SetItemChecked(iIndex, true);
                    }
                }

            }

        }
        private void btnDeSelStringLabels__Click(object sender, EventArgs e)
        {
            string strSerach = this.tbDeSelStringLebel_.Text.Trim();
            if (strSerach != "")
            {
                int iQuesMarkIndex = strSerach.IndexOf('?');
                if (iQuesMarkIndex > 0)
                {
                    strSerach = strSerach.Substring(0, iQuesMarkIndex);
                }

                for (int iIndex = 0; iIndex < this.lbStringLebels_.Items.Count; iIndex++)
                {
                    if (this.lbStringLebels_.Items[iIndex].ToString().StartsWith(strSerach))
                    {
                        this.lbStringLebels_.SetItemChecked(iIndex, false);
                    }
                }

            }
        }
        #endregion

        
     }
}