using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

using HeadsUtils;
using System.Threading;
using HeadsUtils.Interfaces;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Geometry;

namespace HEADSNeed.StringDraw
{
    public partial class frm_StringDraw : Form
    {
         private string m_strpathlistfile = "";
        private string m_strpathfilfile = "";
        private IHeadsApplication m_app;
        private Thread m_threadDraw;

        delegate void GenericFunCall();
        delegate void SetProgressValue(int iValue, ProgressBar pbr);
        delegate string RetStrFunCall();
        public string Working_Folder { get; set; }
        vdDocument vdoc = null;

        vdText vtxt = new vdText();
        vdLine ln = new vdLine();
        vdPolyline p_ln = new vdPolyline();
        vd3DFace v3D_face = new vd3DFace();
        vdPoint pt = new vdPoint();


        public frm_StringDraw(IHeadsApplication app, vdDocument doc)
        {
            InitializeComponent();

            vdoc = doc;
            Working_Folder = app.AppDataPath;
            this.progressBar_.Minimum = 0;
            this.progressBar_.Maximum = 100;

            m_app = app;

            //set the test data path    
            m_strpathlistfile = Path.Combine(Working_Folder, "model.lst");
            m_strpathfilfile = Path.Combine(Working_Folder, "model.fil");
                        
            //m_app = app;

            //this.tbScaleX_.Value = this.m_app.ActiveDocument.ConfigParam.XMetric;
            //this.tbScaleY_.Value = this.m_app.ActiveDocument.ConfigParam.YMetric;

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
                //if (listmodels.Contains(data.strMod1) == false)
                if (listmodels.Contains(data.strMod1) == false && data.strMod1 != "")
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
                //m_threadDraw.IsBackground = true;
                m_threadDraw.SetApartmentState(ApartmentState.STA);
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
            try
            {
                ExecDrawString();
            }
            catch (ThreadAbortException)
            {
                System.Diagnostics.Trace.WriteLine("ThreadAbortException Catched in : " + Thread.CurrentThread.Name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), HeadsUtils.Constants.ProductName);
            }
            //br.Close();
            this.m_app.ActiveDocument.DisableUndoRecording(false);
            vdoc.Redraw(true);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(vdoc);
            //this.m_app.ActiveDocument.RefreshDocument();
            //this.m_app.ZoomExtents();
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
        private void SetProgressBarMaxValue(int iMaxValue, ProgressBar pbr)
        {
            if (this.progressBar_.InvokeRequired)
            {
                this.progressBar_.Invoke(new SetProgressValue(SetProgressBarMaxValue), new object[] { iMaxValue, progressBar_ });
            }
            else
            {
                pbr.Maximum = iMaxValue;
            }
        }
        private void SetProgressBarValue(int iValue, ProgressBar pbr)
        {
            pbr.Value = iValue;
            //if ((iValue % 2) == 0)
            //{
            //    if (this.progressBar_.InvokeRequired)
            //    {
            //        this.progressBar_.Invoke(new SetProgressValue(SetProgressBarValue), new object[] { iValue });
            //    }
            //    else
            //    {
            //        this.progressBar_.Value = iValue;
            //    }
            //}
           
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
            //Chiranjit [2011 09 06] 
            //Add 4 buttons
            else if (this.rbtn_X_Y_Values.Checked)
            {
                iLineType = 5;
            }
            else if (this.rbtn_with_z_values.Checked)
            {
                iLineType = 6;
            }
            else if (this.rbtn_with_string_label.Checked)
            {
                iLineType = 7;
            }
            else if (this.rbtn_with_model_string_labels.Checked)
            {
                iLineType = 8;
            }
            return iLineType;
        }


        //Chiranjit [2011 08 10]
        //This DrawString method created by Tirtha Chaterjee
        private void ExecDrawString()
        {

            SetProgressValue s_pbr_val = new SetProgressValue(SetProgressBarValue);
            int iCurProgressInPercent =0;
            BinaryReader br = new BinaryReader(new FileStream(m_strpathfilfile, FileMode.Open, FileAccess.Read), Encoding.Default);
            List<vdPolyline> list_pLine = new List<vdPolyline>();
            vdPolyline p_line = new vdPolyline();
            try
            {

                ExecDrawString(br);
                vtxt.Dispose();
                ln.Dispose();
                p_ln.Dispose();
                v3D_face.Dispose();
                pt.Dispose();

                throw new Exception("");

                bool bIsModel = false;
                bool bIsLabel = false;

                CPTStype pts = new CPTStype();
                CTXTtype txt = new CTXTtype();




                string strSeleModel = GetSelectedModelName();

                string strModelName = "";
                int NumPts = 0;
                string str1 = "";
                string stgLabel = "";

                this.m_app.ActiveDocument.ConfigParam.XMetric = 1.0;
                this.m_app.ActiveDocument.ConfigParam.YMetric = 1.0;

                CCfgtype cfg = this.m_app.ActiveDocument.ConfigParam;
                int iSelectedLineType = this.GetSelectedLineType();
                double dTextHeight = (this.m_app.ActiveDocument.ActiveTextHeight > 0) ? this.m_app.ActiveDocument.ActiveTextHeight : 1.0;

                string strFilePath = m_strpathfilfile;
                
                //set the progress bar max value
                SetProgressBarMaxValue(100, progressBar_);
                //Chiranjit [2011 08 10]
                //Add Progress Bar and Time
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
                        if (bIsLabel)
                        {
                            pts = (CPTStype)labtype.Tag;
                            p_line.VertexList.Add(new Vertex(pts.mx, pts.my, ((pts.mz < -998) ? 0.0 : pts.mz)));
                        }
                    }
                    else if (labtype.attr == CLabtype.Type.Text)
                    {

                    }
                    else if (labtype.attr == CLabtype.Type.EndCode)
                    {
                        list_pLine.Add(p_line);
                        p_line.SetUnRegisterDocument(vdoc);
                        p_line.setDocumentDefaults();
                        vdoc.ActiveLayOut.Entities.AddItem(p_line);

                        p_line = new vdPolyline();
                    }
                    iCurProgressInPercent = (int)(((double)br.BaseStream.Position / (double)br.BaseStream.Length) * 100.00);

                    progressBar_.Invoke(s_pbr_val, iCurProgressInPercent, progressBar_);
                    //Thread.Sleep(0);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                br.Close();
                vdoc.Redraw(true);
            }


          
            //AutoProgressBar.frm_ProgressBar.OFF();
        }
        private void ExecDrawString(BinaryReader br)
        {
            SetProgressValue s_pbr_val = new SetProgressValue(SetProgressBarValue);
            int iCurProgressInPercent = 0;

            bool bIsModel = false;
            bool bIsLabel = false;

            gPoint ptStart = new gPoint();
            gPoint ptEnd = new gPoint();
            gPoint ptLast = new gPoint();
            gPoint ptFirst3dFac = new gPoint();
            string strSeleModel = GetSelectedModelName();

            CTXTtype txt = new CTXTtype();
            vdLine uline = new vdLine();




            string strModelName = "";
            int NumPts = 0;
            string str1 = "";
            string stgLabel = "";
            gPoint aPoint = null;
            IHdPolyline3D polyline = null;
            CPTStype pts = new CPTStype();
            List<gPoint> pFArray = new List<gPoint>();

            this.m_app.ActiveDocument.ConfigParam.XMetric = 1.0;
            this.m_app.ActiveDocument.ConfigParam.YMetric = 1.0;

            CCfgtype cfg = this.m_app.ActiveDocument.ConfigParam;

            int iSelectedLineType = this.GetSelectedLineType();

            double dTextHeight = (this.m_app.ActiveDocument.ActiveTextHeight > 0) ? this.m_app.ActiveDocument.ActiveTextHeight : 1.0;

            int p_count = 0;

            string strFilePath = m_strpathfilfile;
            //if (File.Exists(strFilePath))
            {
                //BinaryReader br = new BinaryReader(new FileStream(strFilePath, FileMode.Open, FileAccess.Read), Encoding.Default);

                //set the progress bar max value
                SetProgressBarMaxValue(100, progressBar_);
                //Chiranjit [2011 08 10]
                //Add Progress Bar and Time
                //AutoProgressBar.frm_ProgressBar.ON("Reading Model file....");
                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    try
                    {
                        CLabtype labtype = CLabtype.FromStream(br);
                        //AutoProgressBar.frm_ProgressBar.SetValue(br.BaseStream.Position, br.BaseStream.Length);
                        #region Model
                        if (labtype.attr == CLabtype.Type.Model) // Model Name
                        {
                            NumPts = 0;
                            strModelName = ((CModType)labtype.Tag).Name;
                            bIsModel = (strModelName == strSeleModel) ? true : false;
                        }
                        #endregion Model

                        #region String
                        else if (labtype.attr == CLabtype.Type.String)// String Label
                        {
                            stgLabel = ((CStgType)labtype.Tag).label;
                            if (bIsModel)
                            {
                                bIsLabel = this.lbStringLebels_.CheckedItems.Contains(stgLabel);

                                if (bIsLabel)
                                {
                                    if (p_ln.VertexList.Count > 1)
                                    {
                                        p_ln.Label = strModelName + ":" + stgLabel + "     : " + p_count++;
                                        DrawEntity(p_ln);
                                    }
                                }
                            }
                        }
                        #endregion String

                        #region Point
                        else if (labtype.attr == CLabtype.Type.Point)
                        {
                            pts = (CPTStype)labtype.Tag;
                            try
                            {
                                ptEnd = new gPoint(pts.mx * cfg.XMetric, pts.my * cfg.YMetric, ((pts.mz < -898.0) ? 0.0 : pts.mz));
                            }
                            catch (Exception ex)
                            {
                                ptEnd = new gPoint();
                            }
                            NumPts++;

                            ptStart = ptLast;
                            ptLast = ptEnd;

                            if (bIsModel && bIsLabel)
                            {
                                uline.StartPoint = new gPoint(ptStart);
                                uline.EndPoint = new gPoint(ptEnd);

                                str1 = stgLabel;
                                if (iSelectedLineType == 5)
                                {
                                    if (true)
                                    {
                                        str1 = "X = " + ptEnd.x.ToString("0.000");

                                        vtxt.InsertionPoint = ptEnd;
                                        vtxt.TextString = str1;
                                        vtxt.Height = dTextHeight;
                                        //double _bt = vtxt.BoundingBox.Bottom;
                                        DrawEntity(vtxt);

                                        str1 = "Y = " + ptEnd.y.ToString("0.000");

                                        vtxt.InsertionPoint = ptEnd;
                                        vtxt.InsertionPoint.y -= (1.2 * dTextHeight);
                                        vtxt.TextString = str1;
                                        vtxt.Height = dTextHeight;
                                        DrawEntity(vtxt);


                                        pt.InsertionPoint = ptEnd;
                                        DrawEntity(pt);

                                    }
                                    pt.InsertionPoint = ptEnd;
                                    DrawEntity(pt);
                                }
                                else if (iSelectedLineType == 6)
                                {
                                    if (true)
                                    {
                                        str1 = "Z = " + ptEnd.z.ToString("0.000");

                                        vtxt.InsertionPoint = ptEnd;
                                        vtxt.TextString = str1;
                                        vtxt.Height = dTextHeight;
                                        DrawEntity(vtxt);

                                        pt.InsertionPoint = ptEnd;
                                        DrawEntity(pt);
                                    }
                                    pt.InsertionPoint = ptEnd;
                                    DrawEntity(pt);
                                }
                                else if (iSelectedLineType == 7)
                                {
                                    if (true)
                                    {
                                        str1 = stgLabel;

                                        vtxt.InsertionPoint = ptEnd;
                                        vtxt.TextString = str1;
                                        vtxt.Height = dTextHeight;
                                        DrawEntity(vtxt);

                                        pt.InsertionPoint = ptEnd;
                                        DrawEntity(pt);
                                    }
                                    pt.InsertionPoint = ptEnd;
                                    DrawEntity(pt);
                                }
                                else if (iSelectedLineType == 8)
                                {
                                    if (true)
                                    {
                                        str1 = strModelName;

                                        vtxt.InsertionPoint = ptEnd;
                                        vtxt.TextString = str1;
                                        vtxt.Height = dTextHeight;
                                        DrawEntity(vtxt);

                                        str1 = stgLabel;

                                        vtxt.InsertionPoint = ptEnd;
                                        vtxt.InsertionPoint.y -= (dTextHeight * 1.5);
                                        vtxt.TextString = str1;
                                        vtxt.Height = dTextHeight;
                                        DrawEntity(vtxt);

                                        pt.InsertionPoint = ptEnd;
                                        DrawEntity(pt);
                                        //ln.StartPoint = new gPoint(ptEnd.x - 0.5, ptEnd.y, ptEnd.z);
                                        //ln.EndPoint = new gPoint(ptEnd.x + 0.5, ptEnd.y, ptEnd.z);
                                        //DrawEntity(ln);

                                        //ln.StartPoint = new gPoint(ptEnd.x, ptEnd.y - 0.5, ptEnd.z);
                                        //ln.EndPoint = new gPoint(ptEnd.x, ptEnd.y + 0.5, ptEnd.z);
                                        //DrawEntity(ln);
                                    }
                                    pt.InsertionPoint = ptEnd;
                                    DrawEntity(pt);
                                }
                                else if (str1.ToUpper().StartsWith("P") == true)
                                {
                                    if (str1.ToUpper().StartsWith("P0") == true)
                                    {
                                        //txt.Point = ptEnd;
                                        //double[] res = ptEnd.GetCordArr();

                                        txt.tr = 0.0;

                                        str1 = ptEnd.z.ToString("0.000");

                                        vtxt.InsertionPoint = ptEnd;
                                        vtxt.TextString = str1;
                                        vtxt.Height = dTextHeight;
                                        DrawEntity(vtxt);

                                        ln.StartPoint = new gPoint(ptEnd.x - 0.5, ptEnd.y, ptEnd.z);
                                        ln.EndPoint = new gPoint(ptEnd.x + 0.5, ptEnd.y, ptEnd.z);
                                        DrawEntity(ln);

                                        ln.StartPoint = new gPoint(ptEnd.x, ptEnd.y - 0.5, ptEnd.z);
                                        ln.EndPoint = new gPoint(ptEnd.x, ptEnd.y + 0.5, ptEnd.z);
                                        DrawEntity(ln);



                                        //this.m_app.ActiveDocument.DrawText(new CPoint3D(ptEnd.x, ptEnd.y, ptEnd.z), str1, dTextHeight);

                                        //this.m_app.ActiveDocument.DrawLine(new CPoint3D(ptEnd.x - 0.5, ptEnd.y, ptEnd.z), new CPoint3D(ptEnd.x + 0.5, ptEnd.y, ptEnd.z));
                                        //this.m_app.ActiveDocument.DrawLine(new CPoint3D(ptEnd.x, ptEnd.y - 0.5, ptEnd.z), new CPoint3D(ptEnd.x, ptEnd.y + 0.5, ptEnd.z));

                                    }
                                    else
                                    {
                                        vtxt.InsertionPoint = ptEnd;
                                        vtxt.TextString = str1;
                                        vtxt.Height = dTextHeight;
                                        DrawEntity(vtxt);

                                        ln.StartPoint = new gPoint(ptEnd.x - 0.5, ptEnd.y, ptEnd.z);
                                        ln.EndPoint = new gPoint(ptEnd.x + 0.5, ptEnd.y, ptEnd.z);
                                        DrawEntity(ln);

                                        ln.StartPoint = new gPoint(ptEnd.x, ptEnd.y - 0.5, ptEnd.z);
                                        ln.EndPoint = new gPoint(ptEnd.x, ptEnd.y + 0.5, ptEnd.z);
                                        DrawEntity(ln);

                                        //txt.Point = ptEnd;
                                        //txt.tr = 0.0;
                                        //double[] res = ptEnd.GetCordArr();

                                        //this.m_app.ActiveDocument.DrawText(new CPoint3D(ptEnd.x, ptEnd.y, dTextHeight), str1, dTextHeight);

                                        //this.m_app.ActiveDocument.DrawLine(new CPoint3D(ptEnd.x - 0.5, ptEnd.y, ptEnd.z), new CPoint3D(ptEnd.x + 0.5, ptEnd.y, ptEnd.z));
                                        //this.m_app.ActiveDocument.DrawLine(new CPoint3D(ptEnd.x, ptEnd.y - 0.5, ptEnd.z), new CPoint3D(ptEnd.x, ptEnd.y + 0.5, ptEnd.z));
                                    }
                                }
                                else
                                {
                                    if (NumPts == 1)
                                    {
                                        //aPoint = this.m_app.ActiveDocument.DrawPoint(ptEnd);
                                        if (iSelectedLineType == 1)
                                        {
                                            p_ln.VertexList.Add(ptEnd);
                                        }
                                        else if (iSelectedLineType == 4)
                                        {
                                            //aPoint.Erase();
                                            ptFirst3dFac = new gPoint(ptEnd);
                                            pFArray.Add(ptEnd);
                                        }
                                        else if (iSelectedLineType == 3)
                                        {
                                            pt.InsertionPoint = ptEnd;
                                            DrawEntity(pt);
                                        }


                                    }
                                    else if (NumPts == 2)
                                    {
                                        //uline.Layer = this.m_app.ActiveDocument.GetActiveLayer().Name;
                                        //uline.elatt = 1;
                                        //uline.laatt = 1;
                                        uline.Label = strModelName;
                                        uline.Label += ":";
                                        uline.Label += stgLabel;
                                        //uline.scatt = 1;

                                        if (iSelectedLineType == 1)
                                        {
                                            //ln.StartPoint = uline.StartPoint;
                                            //ln.EndPoint = uline.EndPoint;
                                            //ln.Label = uline.Label;
                                            //DrawEntity(ln);

                                            p_ln.VertexList.Add(uline.EndPoint);
                                            //polyline = DrawingUtil.DrawPolyline3DDx(this.m_app.ActiveDocument, true, uline);
                                        }
                                        else if (iSelectedLineType == 2)
                                        {
                                            ln.StartPoint = uline.StartPoint;
                                            ln.EndPoint = uline.EndPoint;
                                            ln.Label = uline.Label;
                                            DrawEntity(ln);
                                            //polyline = DrawingUtil.DrawPolyline3DDx(this.m_app.ActiveDocument, true, uline);
                                        }
                                        else if (iSelectedLineType == 3)
                                        {
                                            pt.InsertionPoint = ptEnd;
                                            DrawEntity(pt);
                                            //aPoint = this.m_app.ActiveDocument.DrawPoint(ptEnd);
                                        }
                                        else if (iSelectedLineType == 4)
                                        {
                                            pFArray.Add(ptEnd);
                                        }

                                        //aPoint.Erase();
                                    }
                                    else if (NumPts > 2)
                                    {
                                        if (iSelectedLineType == 1)
                                        {
                                            p_ln.VertexList.Add(ptEnd);
                                            //polyline.AppendVertex(ptEnd);
                                        }
                                        else if (iSelectedLineType == 2)
                                        {
                                            ln.StartPoint = uline.StartPoint;
                                            ln.EndPoint = uline.EndPoint;
                                            ln.Label = uline.Label;
                                            DrawEntity(ln);
                                            //polyline = DrawingUtil.DrawPolyline3DDx(this.m_app.ActiveDocument, true, uline);
                                        }
                                        else if (iSelectedLineType == 3)
                                        {
                                            pt.InsertionPoint = ptEnd;
                                            DrawEntity(pt);
                                            //aPoint = this.m_app.ActiveDocument.DrawPoint(ptEnd);
                                        }
                                        else if (iSelectedLineType == 4)
                                        {
                                            pFArray.Add(ptEnd);
                                        }
                                    }
                                }
                            }
                        }
                        #endregion Point

                        #region Text

                        else if (labtype.attr == CLabtype.Type.Text)
                        {
                            txt = (CTXTtype)labtype.Tag;

                            ptEnd = new gPoint(txt.tx * cfg.XMetric, txt.ty * cfg.YMetric, txt.tz);
                            if (bIsLabel && bIsModel)
                            {
                                str1 = txt.tg;
                                //Display an Elevation
                                //TO DO:

                                vtxt.InsertionPoint = ptEnd;
                                vtxt.TextString = str1;
                                vtxt.Height = dTextHeight;
                                vtxt.Rotation = txt.tr;
                                DrawEntity(vtxt);
                                //IHdText xt = this.m_app.ActiveDocument.DrawText(ptEnd, str1, dTextHeight);
                                //xt.Rotation = txt.tr;
                                //xt.Height = txt.ts;
                                //xt.VerJustify = eVerticalJustify.Bottom;
                                //aPoint = this.m_app.ActiveDocument.DrawPoint(ptEnd);
                            }
                        }
                        #endregion Text

                        #region EndCode

                        else if (labtype.attr == CLabtype.Type.EndCode)
                        {
                            if ((iSelectedLineType == 4) && (bIsLabel && bIsModel))
                            {
                                if ((System.Math.Abs(ptEnd.x - ptFirst3dFac.x) > 0.00001) || (System.Math.Abs(ptEnd.y - ptFirst3dFac.y) > 0.00001))
                                {
                                    if (NumPts < 4)
                                    {
                                        pFArray.Add(ptFirst3dFac);	 //close the face
                                    }
                                }

                                if (pFArray.Count > 3)
                                {
                                    //v3D_face.VertexList.Add(pFArray[0]);
                                    //v3D_face.VertexList.Add(pFArray[1]);
                                    //v3D_face.VertexList.Add(pFArray[2]);
                                    //v3D_face.VertexList.Add(pFArray[3]);


                                    v3D_face.VertexList[0] = (pFArray[0]);
                                    v3D_face.VertexList[1] = (pFArray[1]);
                                    v3D_face.VertexList[2] = (pFArray[2]);
                                    v3D_face.VertexList[3] = (pFArray[3]);
                                    pFArray.Clear();

                                }
                                DrawEntity(v3D_face);


                            }
                            if ((iSelectedLineType == 1) && (bIsLabel && bIsModel))
                            {
                                p_ln.Label = strModelName + ":" + stgLabel + "     : " + p_count++;
                                DrawEntity(p_ln);
                            }
                            NumPts = 0;
                        }
                        #endregion EndCode
                        iCurProgressInPercent = (int)(((double)br.BaseStream.Position / (double)br.BaseStream.Length) * 100.00);
                        progressBar_.Invoke(s_pbr_val, iCurProgressInPercent, progressBar_);
                        //Thread.Sleep(0);
                    }
                    catch (Exception ex) { }
                }
                if ((iSelectedLineType == 1))
                {
                    if (p_ln.VertexList.Count > 1)
                    {
                        p_ln.Label = strModelName + ":" + stgLabel + "     : " + p_count++;
                        DrawEntity(p_ln);
                    }
                }
                //AutoProgressBar.frm_ProgressBar.OFF();
                //br.Close();
            }
        }


        private void ExecDrawString1(BinaryReader br)
        {
            SetProgressValue s_pbr_val = new SetProgressValue(SetProgressBarValue);
            int iCurProgressInPercent = 0;

            bool bIsModel = false;
            bool bIsLabel = false;

            gPoint ptStart = new gPoint();
            gPoint ptEnd = new gPoint();
            gPoint ptLast = new gPoint();
            gPoint ptFirst3dFac = new gPoint();
            string strSeleModel = GetSelectedModelName();

            CTXTtype txt = new CTXTtype();
            vdLine uline = new vdLine();



            
            string strModelName = "";
            int NumPts = 0;
            string str1 = "";
            string stgLabel = "";
            gPoint aPoint = null;
            IHdPolyline3D polyline = null;
            CPTStype pts = new CPTStype();
            List<gPoint> pFArray = new List<gPoint>();

            this.m_app.ActiveDocument.ConfigParam.XMetric = 1.0;
            this.m_app.ActiveDocument.ConfigParam.YMetric = 1.0;

            CCfgtype cfg = this.m_app.ActiveDocument.ConfigParam;

            int iSelectedLineType = this.GetSelectedLineType();

            double dTextHeight = (this.m_app.ActiveDocument.ActiveTextHeight > 0) ? this.m_app.ActiveDocument.ActiveTextHeight : 1.0;

            int p_count = 0;

            string strFilePath = m_strpathfilfile;
            //if (File.Exists(strFilePath))
            {
                //BinaryReader br = new BinaryReader(new FileStream(strFilePath, FileMode.Open, FileAccess.Read), Encoding.Default);

                //set the progress bar max value
                SetProgressBarMaxValue(100, progressBar_);
                //Chiranjit [2011 08 10]
                //Add Progress Bar and Time
                //AutoProgressBar.frm_ProgressBar.ON("Reading Model file....");
                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    CLabtype labtype = CLabtype.FromStream(br);
                    //AutoProgressBar.frm_ProgressBar.SetValue(br.BaseStream.Position, br.BaseStream.Length);
                    #region Model
                    if (labtype.attr == CLabtype.Type.Model) // Model Name
                    {
                        NumPts = 0;
                        strModelName = ((CModType)labtype.Tag).Name;
                        bIsModel = (strModelName == strSeleModel) ? true : false;
                    }
                    #endregion Model

                    #region String
                    else if (labtype.attr == CLabtype.Type.String)// String Label
                    {
                        stgLabel = ((CStgType)labtype.Tag).label;
                        if (bIsModel)
                        {
                            bIsLabel = this.lbStringLebels_.CheckedItems.Contains(stgLabel);

                            if (bIsLabel)
                            {
                                if (p_ln.VertexList.Count > 1)
                                {
                                    p_ln.Label = strModelName + ":" + stgLabel + "     : " + p_count++;
                                    DrawEntity(p_ln);
                                }
                            }
                        }
                    }
                    #endregion String

                    #region Point
                    else if (labtype.attr == CLabtype.Type.Point)
                    {
                        pts = (CPTStype)labtype.Tag;

                        ptEnd = new gPoint(pts.mx * cfg.XMetric, pts.my * cfg.YMetric, ((pts.mz < -998.0) ? 0.0 : pts.mz));
                        NumPts++;

                        ptStart = ptLast;
                        ptLast = ptEnd;

                        if (bIsModel && bIsLabel)
                        {
                            uline.StartPoint = new gPoint(ptStart);
                            uline.EndPoint = new gPoint(ptEnd);

                            str1 = stgLabel;
                            if (str1.ToUpper().StartsWith("P") == true)
                            {
                                if (str1.ToUpper().StartsWith("P0") == true)
                                {
                                    //txt.Point = ptEnd;
                                    //double[] res = ptEnd.GetCordArr();

                                    txt.tr = 0.0;

                                    str1 = ptEnd.z.ToString("0.000");

                                    vtxt.InsertionPoint = ptEnd;
                                    vtxt.TextString = str1;
                                    vtxt.Height = dTextHeight;
                                    DrawEntity(vtxt);

                                    ln.StartPoint = new gPoint(ptEnd.x - 0.5, ptEnd.y, ptEnd.z);
                                    ln.EndPoint = new gPoint(ptEnd.x + 0.5, ptEnd.y, ptEnd.z);
                                    DrawEntity(ln);

                                    ln.StartPoint = new gPoint(ptEnd.x, ptEnd.y - 0.5, ptEnd.z);
                                    ln.EndPoint = new gPoint(ptEnd.x, ptEnd.y + 0.5, ptEnd.z);
                                    DrawEntity(ln);



                                    //this.m_app.ActiveDocument.DrawText(new CPoint3D(ptEnd.x, ptEnd.y, ptEnd.z), str1, dTextHeight);
                                    
                                    //this.m_app.ActiveDocument.DrawLine(new CPoint3D(ptEnd.x - 0.5, ptEnd.y, ptEnd.z), new CPoint3D(ptEnd.x + 0.5, ptEnd.y, ptEnd.z));
                                    //this.m_app.ActiveDocument.DrawLine(new CPoint3D(ptEnd.x, ptEnd.y - 0.5, ptEnd.z), new CPoint3D(ptEnd.x, ptEnd.y + 0.5, ptEnd.z));

                                }
                                else
                                {
                                    vtxt.InsertionPoint = ptEnd;
                                    vtxt.TextString = str1;
                                    vtxt.Height = dTextHeight;
                                    DrawEntity(vtxt);

                                    ln.StartPoint = new gPoint(ptEnd.x - 0.5, ptEnd.y, ptEnd.z);
                                    ln.EndPoint = new gPoint(ptEnd.x + 0.5, ptEnd.y, ptEnd.z);
                                    DrawEntity(ln);

                                    ln.StartPoint = new gPoint(ptEnd.x, ptEnd.y - 0.5, ptEnd.z);
                                    ln.EndPoint = new gPoint(ptEnd.x, ptEnd.y + 0.5, ptEnd.z);
                                    DrawEntity(ln);

                                    //txt.Point = ptEnd;
                                    //txt.tr = 0.0;
                                    //double[] res = ptEnd.GetCordArr();

                                    //this.m_app.ActiveDocument.DrawText(new CPoint3D(ptEnd.x, ptEnd.y, dTextHeight), str1, dTextHeight);

                                    //this.m_app.ActiveDocument.DrawLine(new CPoint3D(ptEnd.x - 0.5, ptEnd.y, ptEnd.z), new CPoint3D(ptEnd.x + 0.5, ptEnd.y, ptEnd.z));
                                    //this.m_app.ActiveDocument.DrawLine(new CPoint3D(ptEnd.x, ptEnd.y - 0.5, ptEnd.z), new CPoint3D(ptEnd.x, ptEnd.y + 0.5, ptEnd.z));
                                }
                            }
                            else
                            {
                                if (NumPts == 1)
                                {
                                    //aPoint = this.m_app.ActiveDocument.DrawPoint(ptEnd);
                                    if (iSelectedLineType == 1)
                                    {
                                        p_ln.VertexList.Add(ptEnd);
                                    }
                                    else  if (iSelectedLineType == 4)
                                    {
                                        //aPoint.Erase();
                                        ptFirst3dFac = new gPoint(ptEnd);
                                        pFArray.Add(ptEnd);
                                    }
                                    else if (iSelectedLineType == 3)
                                    {
                                        pt.InsertionPoint = ptEnd;
                                        DrawEntity(pt);
                                    }
                                }
                                else if (NumPts == 2)
                                {
                                    //uline.Layer = this.m_app.ActiveDocument.GetActiveLayer().Name;
                                    //uline.elatt = 1;
                                    //uline.laatt = 1;
                                    uline.Label = strModelName;
                                    uline.Label += ":";
                                    uline.Label += stgLabel;
                                    //uline.scatt = 1;

                                    if (iSelectedLineType == 1)
                                    {
                                        //ln.StartPoint = uline.StartPoint;
                                        //ln.EndPoint = uline.EndPoint;
                                        //ln.Label = uline.Label;
                                        //DrawEntity(ln);

                                        p_ln.VertexList.Add(uline.EndPoint);
                                        //polyline = DrawingUtil.DrawPolyline3DDx(this.m_app.ActiveDocument, true, uline);
                                    }
                                    else if (iSelectedLineType == 2)
                                    {
                                        ln.StartPoint = uline.StartPoint;
                                        ln.EndPoint = uline.EndPoint;
                                        ln.Label = uline.Label;
                                        DrawEntity(ln);
                                        //polyline = DrawingUtil.DrawPolyline3DDx(this.m_app.ActiveDocument, true, uline);
                                    }
                                    else if (iSelectedLineType == 3)
                                    {
                                        pt.InsertionPoint = ptEnd;
                                        DrawEntity(pt);
                                        //aPoint = this.m_app.ActiveDocument.DrawPoint(ptEnd);
                                    }
                                    else if (iSelectedLineType == 4)
                                    {
                                        pFArray.Add(ptEnd);
                                    }

                                    //aPoint.Erase();
                                }
                                else if (NumPts > 2)
                                {
                                    if (iSelectedLineType == 1)
                                    {
                                        p_ln.VertexList.Add(ptEnd);
                                        //polyline.AppendVertex(ptEnd);
                                    }
                                    else if (iSelectedLineType == 2)
                                    {
                                        ln.StartPoint = uline.StartPoint;
                                        ln.EndPoint = uline.EndPoint;
                                        ln.Label = uline.Label;
                                        DrawEntity(ln);
                                        //polyline = DrawingUtil.DrawPolyline3DDx(this.m_app.ActiveDocument, true, uline);
                                    }
                                    else if (iSelectedLineType == 3)
                                    {
                                        pt.InsertionPoint = ptEnd;
                                        DrawEntity(pt);
                                        //aPoint = this.m_app.ActiveDocument.DrawPoint(ptEnd);
                                    }
                                    else if (iSelectedLineType == 4)
                                    {
                                        pFArray.Add(ptEnd);
                                    }
                                }
                            }
                        }
                    }
                    #endregion Point

                    #region Text

                    else if (labtype.attr == CLabtype.Type.Text)
                    {
                        txt = (CTXTtype)labtype.Tag;

                        ptEnd = new gPoint(txt.tx * cfg.XMetric, txt.ty * cfg.YMetric, txt.tz);
                        if (bIsLabel && bIsModel)
                        {
                            str1 = txt.tg;
                            //Display an Elevation
                            //TO DO:

                            vtxt.InsertionPoint = ptEnd;
                            vtxt.TextString = str1;
                            vtxt.Height = dTextHeight;
                            vtxt.Rotation = txt.tr;
                            DrawEntity(vtxt);
                            //IHdText xt = this.m_app.ActiveDocument.DrawText(ptEnd, str1, dTextHeight);
                            //xt.Rotation = txt.tr;
                            //xt.Height = txt.ts;
                            //xt.VerJustify = eVerticalJustify.Bottom;
                            //aPoint = this.m_app.ActiveDocument.DrawPoint(ptEnd);
                        }
                    }
                    #endregion Text

                    #region EndCode

                    else if (labtype.attr == CLabtype.Type.EndCode)
                    {
                        if ((iSelectedLineType == 4) && (bIsLabel && bIsModel))
                        {
                            if ((System.Math.Abs(ptEnd.x - ptFirst3dFac.x) > 0.00001) || (System.Math.Abs(ptEnd.y - ptFirst3dFac.y) > 0.00001))
                            {
                                if (NumPts < 4)
                                {
                                    pFArray.Add(ptFirst3dFac);	 //close the face
                                }
                            }

                            if (pFArray.Count > 3)
                            {
                                v3D_face.VertexList.Add(pFArray[0]);
                                v3D_face.VertexList.Add(pFArray[1]);
                                v3D_face.VertexList.Add(pFArray[2]);
                                v3D_face.VertexList.Add(pFArray[3]);
                            }
                            
                        }
                        if ((iSelectedLineType == 1) && (bIsLabel && bIsModel))
                        {
                            p_ln.Label = strModelName + ":" + stgLabel + "     : " + p_count++;
                            DrawEntity(p_ln);
                        }
                        NumPts = 0;
                    }
                    #endregion EndCode
                    iCurProgressInPercent = (int)(((double)br.BaseStream.Position / (double)br.BaseStream.Length) * 100.00);
                    progressBar_.Invoke(s_pbr_val, iCurProgressInPercent, progressBar_);
                    //Thread.Sleep(0);
                }
                if ((iSelectedLineType == 1))
                {
                    if (p_ln.VertexList.Count > 1)
                    {
                        p_ln.Label = strModelName + ":" + stgLabel + "     : " + p_count++;
                        DrawEntity(p_ln);
                    }
                }
                //AutoProgressBar.frm_ProgressBar.OFF();
                //br.Close();
            }
        }
        public void DrawEntity(vdFigure fig)
        {

            //vdText vtxt = new vdText();
            //vdLine ln = new vdLine();
            //vdPolyline p_ln = new vdPolyline();
            //vd3DFace v3D_face = new vd3DFace();
            //vdPoint pt = new vdPoint();
            if (fig is vdText)
            {
                vtxt.SetUnRegisterDocument(vdoc);
                vtxt.setDocumentDefaults();
                vdoc.ActiveLayOut.Entities.AddItem(vtxt);
                vtxt = new vdText();
                return;
            }
            if (fig is vdLine)
            {
                ln.SetUnRegisterDocument(vdoc);
                ln.setDocumentDefaults();
                vdoc.ActiveLayOut.Entities.AddItem(ln);
                ln = new vdLine();
                return;
            }
            if (fig is vdPolyline)
            {
                p_ln.SetUnRegisterDocument(vdoc);
                p_ln.setDocumentDefaults();
                vdoc.ActiveLayOut.Entities.AddItem(p_ln);
                p_ln = new vdPolyline();
                return;
            }
            if (fig is vd3DFace)
            {
                v3D_face.SetUnRegisterDocument(vdoc);
                v3D_face.setDocumentDefaults();
                vdoc.ActiveLayOut.Entities.AddItem(v3D_face);
                v3D_face = new vd3DFace();
                return;
            }
            if (fig is vdPoint)
            {
                pt.SetUnRegisterDocument(vdoc);
                pt.setDocumentDefaults();
                vdoc.ActiveLayOut.Entities.AddItem(pt);
                pt = new vdPoint();
                return;
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
                    //MessageBox.Show("HEADS" , MessageBoxButtons.OK , MessageBoxIcon.Information);
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
                //this.m_app.ActiveDocument.ConfigParam.XMetric = this.tbScaleX_.Value;
                //this.m_app.ActiveDocument.ConfigParam.YMetric = this.tbScaleY_.Value;

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

        private void frm_StringDraw_Load(object sender, EventArgs e)
        {
            if (File.Exists(m_strpathfilfile) == false)
            {
                MessageBox.Show("MODEL.FIL file not found in the Current Working Folder.", "HEADS", MessageBoxButtons.OK);
                this.Close();
                return;
            }
            if (File.Exists(m_strpathlistfile) == false)
            {
                MessageBox.Show("MODEL.LST file not found in the Current Working Folder.", "HEADS", MessageBoxButtons.OK);
                this.Close();
                return;
            }
        }
    }
}
