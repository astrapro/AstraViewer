using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HeadsUtils;
using HeadsUtils.Interfaces;
using System.Threading;
using System.IO;

namespace HeadsFunctions1.LoadDeflection
{
    public partial class FormLoadDeflection : Form
    {
        IHeadsApplication App;        

        Thread ThreadLoadFile;
        bool bKeepLoadFileThreadRunning;

        Thread ThreadAnimator;
        bool bKeepAnimatorThreadRunning;
        
        int iCurrentItem = 0;
        const eHEADS_COLOR MemberColor = eHEADS_COLOR.Yellow;
        const eHEADS_COLOR LoadColor = eHEADS_COLOR.Red;
        const int iAnimationDelayMS = 500;
        //// Chiranjit 24/11/2009
        //const int iAnimationDelayMS = 50;

        Hashtable hashLabels = new Hashtable();

        delegate void SetProgressValue(int iValue);
        delegate void EnableCtrlDelegate(Control ctrl, bool bEnable);
        delegate void SetTextDelegate(string strText);
        delegate void GenericFunCall();

        public FormLoadDeflection(IHeadsApplication headsapp)
        {
            InitializeComponent();
            App = headsapp;
            ShowPlayerCtrls(false);

            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            
        }

        #region LOAD FILE THREAD
        private void FormLoadDeflection_Load(object sender, EventArgs e)
        {
            this.StartLoadFileThread();
        }
        void StartLoadFileThread()
        {
            if (ThreadLoadFile == null || ThreadLoadFile.IsAlive == false)
            {
                bKeepLoadFileThreadRunning = true;

                ThreadLoadFile = new Thread(new ThreadStart(LoadFileThreadProc));
                ThreadLoadFile.IsBackground = true;
                ThreadLoadFile.Name = "ThreadLoadFile";
                ThreadLoadFile.Start();
            }
        }

        void StopLoadFileThread()
        {
            if (ThreadLoadFile != null && ThreadLoadFile.IsAlive)
            {
                bKeepLoadFileThreadRunning = false;
            }
        }

        void LoadFileThreadProc()
        {
            BinaryReader filehandle = new BinaryReader(new FileStream(Path.Combine(this.App.AppDataPath, "MODEL.FIL"), FileMode.Open, FileAccess.Read), Encoding.Default);
            SetProgressBarMaxValue(100);
            string strModelName = string.Empty;
            //string stgLabel = string.Empty;
            string stgLabelLast = string.Empty;

            while ((filehandle.BaseStream.Position < filehandle.BaseStream.Length) && (bKeepLoadFileThreadRunning == true))
            {
                CLabtype lab = CLabtype.FromStream(filehandle);
                if (lab.attr == CLabtype.Type.Model)
                {
                    strModelName = ((CModType)lab.Tag).Name.ToLower().Trim();
                }
                else if (lab.attr == CLabtype.Type.String)
                {
                    stgLabelLast = ((CStgType)lab.Tag).label.ToLower().Trim();
                    
                }
                else
                {
                    if (strModelName == "animation" && stgLabelLast.IndexOf('#') == -1)
                    {
                        List<CLabtype> listlbs = null;
                        if (hashLabels.ContainsKey(stgLabelLast) == false)
                        {
                            listlbs = new List<CLabtype>();
                            hashLabels.Add(stgLabelLast, listlbs);
                        }
                        else
                        {
                            listlbs = (List<CLabtype>)hashLabels[stgLabelLast];
                        }
                        listlbs.Add(lab);
                    }
                }

                int iCurProgressInPercent = (int)(((double)filehandle.BaseStream.Position / (double)filehandle.BaseStream.Length) * 100.00);
                SetProgressBarValue(iCurProgressInPercent);
                System.Threading.Thread.Sleep(0);
            }
            
            if (hashLabels.Count == 0)
            {
                MessageBox.Show("Model 'ANIMATION' is not found in model file"
                 , HeadsFunctions1.Properties.Resources.ST_ERROR_MSGBOX_CAPTION
                 , System.Windows.Forms.MessageBoxButtons.OK
                 , System.Windows.Forms.MessageBoxIcon.Error);

                this.CloseFrom();
            }
            else
            {
                this.SetProgressBarValue(100);
                this.ShowPlayerCtrls(true);

                this.ShowFirstItem();

                this.App.ZoomExtents();

                this.EnableCtrl(this.buttonStop_, false);
            }
            bKeepLoadFileThreadRunning = false;
        }
        #endregion

        #region EVENTS
        private void FormLoadDeflection_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.StopLoadFileThread();

            this.StopAnimatorThread();

            this.App.ActiveDocument.RemoveAllEntities();
            this.App.ActiveDocument.RefreshDocument();
        }

        private void buttonNext__Click(object sender, EventArgs e)
        {
            ShowNextItem();
        }

        private void buttonPrev__Click(object sender, EventArgs e)
        {
            ShowPreviousItem();
        }

        private void buttonLast__Click(object sender, EventArgs e)
        {
            this.ShowLastItem();
        }

        private void buttonFirst__Click(object sender, EventArgs e)
        {
            this.ShowFirstItem();
        }

        private void buttonPlay__Click(object sender, EventArgs e)
        {
            this.StartAnimatorThread();
        }

        private void buttonStop__Click(object sender, EventArgs e)
        {
            this.StopAnimatorThread();
        }
        #endregion

        #region THREAD SAFE UI UIPDATE
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

        private void ShowCtrl(Control ctrl, bool bShow)
        {
            if (ctrl.InvokeRequired)
            {
                ctrl.Invoke(new EnableCtrlDelegate(ShowCtrl), new object[] { ctrl, bShow });
            }
            else
            {
                ctrl.Visible = bShow;
            }
        }

        private void EnableCtrl(Control ctrl, bool bEnable)
        {
            if (ctrl.InvokeRequired)
            {
                ctrl.Invoke(new EnableCtrlDelegate(EnableCtrl), new object[] { ctrl, bEnable });
            }
            else
            {
                ctrl.Enabled = bEnable;
            }
        }

        void SetText(string strText)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetTextDelegate(SetText), new object[] { strText });
            }
            else
            {
                this.Text = strText;
            }
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
        #endregion

        #region PRIVATE METHODS
        void ShowPlayerCtrls( bool bShow)
        {
            ShowCtrl(buttonFirst_, bShow);
            ShowCtrl(buttonPrev_, bShow);
            ShowCtrl(buttonLast_, bShow);
            ShowCtrl(buttonNext_, bShow);
            ShowCtrl(buttonStop_, bShow);
            ShowCtrl(buttonPlay_, bShow);

            ShowCtrl(progressBar_, !bShow);
        }

        void ShowItemEntities(List<CLabtype> entities, string strModelName, string stgLabel)
        {
            int NumPts = 0;
            CPoint3D ptStart = new CPoint3D();
            CPoint3D ptEnd = new CPoint3D();
            CPoint3D ptLast = new CPoint3D();
            CTXTtype txt = new CTXTtype();
            CLinetype uline = new CLinetype();
            IHdPolyline3D polyline = null;
            string str1 = "";

            IHdPoint aPoint = null;
            CPTStype pts = new CPTStype();

            this.App.ActiveDocument.ConfigParam.XMetric = 1.0;
            this.App.ActiveDocument.ConfigParam.YMetric = 1.0;

            CCfgtype cfg = this.App.ActiveDocument.ConfigParam;

            eHEADS_COLOR entitycolor = (stgLabel.StartsWith("member")) ? FormLoadDeflection.MemberColor : FormLoadDeflection.LoadColor;
            
            double dTextHeight = (this.App.ActiveDocument.ActiveTextHeight > 0) ? this.App.ActiveDocument.ActiveTextHeight : 1.0;

            for (int iIndex = 0; iIndex < entities.Count; iIndex++)
            {
                CLabtype labtype = entities[iIndex];

                if (labtype.attr == CLabtype.Type.Point)
                {
                    pts = (CPTStype)labtype.Tag;

                    ptEnd = new CPoint3D(pts.mx * cfg.XMetric, pts.my * cfg.YMetric, pts.mz);
                    NumPts++;

                    ptStart = ptLast;
                    ptLast = ptEnd;

                    uline.StartPoint = new CPoint3D(ptStart);
                    uline.EndPoint = new CPoint3D(ptEnd);

                    str1 = stgLabel;

                    if (NumPts == 1)
                    {
                        aPoint = this.App.ActiveDocument.DrawPoint(ptEnd);

                        aPoint.color = entitycolor;
                        
                    }
                    else if (NumPts == 2)
                    {
                        uline.Layer = this.App.ActiveDocument.GetActiveLayer().Name;
                        uline.elatt = 1;
                        uline.laatt = 1;
                        uline.Label = strModelName;
                        uline.Label += ":";
                        uline.Label += stgLabel;
                        uline.scatt = 1;

                        polyline = DrawingUtil.DrawPolyline3DDx(this.App.ActiveDocument, true, uline);

                        polyline.color = entitycolor;

                        aPoint.Erase();
                    }
                    else if (NumPts > 2)
                    {
                        polyline.AppendVertex(ptEnd);
                    }
                }
                else if (labtype.attr == CLabtype.Type.Text)
                {
                    txt = (CTXTtype)labtype.Tag;

                    ptEnd = new CPoint3D(txt.tx * cfg.XMetric, txt.ty * cfg.YMetric, txt.tz);
                    str1 = txt.tg;
                    IHdText textobj =  this.App.ActiveDocument.DrawText(ptEnd, str1, dTextHeight);
                    textobj.color = entitycolor;
                    aPoint = this.App.ActiveDocument.DrawPoint(ptEnd);
                    aPoint.color = entitycolor;
                }
                else if (labtype.attr == CLabtype.Type.EndCode)
                {
                    NumPts = 0;
                }

            }
            this.App.ActiveDocument.RefreshDocument(true);
        }

        bool ShowItemEntities(string strItemName)
        {
            bool bSuccess = false;
            object entities = this.hashLabels[strItemName];
            if (entities != null)
            {
                try
                {
                    ShowItemEntities((List<CLabtype>)entities, "Animation", strItemName);
                    bSuccess = true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Followig exception occur : " + ex.ToString());
                }
            }
            return bSuccess;
        }

        void UpdateNavigationButton()
        {

            bool bLast = (this.hashLabels["member_" + ((int)(this.iCurrentItem + 1)).ToString()] == null) ? true : false;
            EnableCtrl(this.buttonNext_, !bLast);
            EnableCtrl(this.buttonLast_, !bLast);

            bool bFirst = (this.hashLabels["member_" + ((int)(this.iCurrentItem - 1)).ToString()] == null) ? true : false;
            EnableCtrl(this.buttonPrev_, !bFirst);
            EnableCtrl(this.buttonFirst_, !bFirst);
        }

        string GetCaption(int iItem)
        {
            string str;
            if (iItem == 0)
            {
                str = "Load Deflection [ No load case ]";
            }
            else
            {
                str = "Load Deflection [ Load case : " + this.iCurrentItem.ToString() + "]";
            }
            return str;
        }

        bool ShowNextItem()
        {
            bool bSuccess = false;
            int iNextItem = this.iCurrentItem + 1;
            if (this.hashLabels["member_" + iNextItem.ToString()] != null)
            {
                
                this.App.ActiveDocument.RemoveAllEntities();

                ShowItemEntities("member_" + iNextItem.ToString());
                ShowItemEntities("load_" + iNextItem.ToString());

                this.iCurrentItem = iNextItem;

                UpdateNavigationButton();

                SetText(GetCaption(this.iCurrentItem));                
                this.App.ActiveDocument.DisableUndoRecording(false);

                bSuccess = true;
            }
            return bSuccess;
        }

        bool ShowPreviousItem()
        {
            bool bSuccess = false;
            int iPrevItem = this.iCurrentItem - 1;
            if (this.hashLabels["member_" + iPrevItem.ToString()] != null)
            {
                this.App.ActiveDocument.DisableUndoRecording(true);
                this.App.ActiveDocument.RemoveAllEntities();


                ShowItemEntities("member_" + iPrevItem.ToString());
                ShowItemEntities("load_" + iPrevItem.ToString());

                this.iCurrentItem = iPrevItem;

                UpdateNavigationButton();

                SetText(GetCaption(this.iCurrentItem)); 
                this.App.ActiveDocument.DisableUndoRecording(false);
                bSuccess = true;
            }
            return bSuccess;
        }

        void ShowFirstItem()
        {
            int iFirstItem = 0;
            if (this.hashLabels["member_" + iFirstItem.ToString()] != null)
            {
                this.App.ActiveDocument.DisableUndoRecording(true);
                this.App.ActiveDocument.RemoveAllEntities();

                ShowItemEntities("member_" + iFirstItem.ToString());
                ShowItemEntities("load_" + iFirstItem.ToString());

                this.iCurrentItem = iFirstItem;

                UpdateNavigationButton();

                SetText(GetCaption(this.iCurrentItem));
                this.App.ActiveDocument.DisableUndoRecording(false);
            }
        }

        void ShowLastItem()
        {
            int iIndex = this.iCurrentItem;
            while (this.hashLabels["member_" + iIndex.ToString()] != null)
            {
                iIndex++;
            }

            int iLastItem = iIndex -1;
            if (this.hashLabels["member_" + iLastItem.ToString()] != null)
            {
                this.App.ActiveDocument.DisableUndoRecording(true);
                this.App.ActiveDocument.RemoveAllEntities();

                ShowItemEntities("member_" + iLastItem.ToString());
                ShowItemEntities("load_" + iLastItem.ToString());

                this.iCurrentItem = iLastItem;

                UpdateNavigationButton();

                SetText(GetCaption(this.iCurrentItem)); 
                this.App.ActiveDocument.DisableUndoRecording(false);
            }
        }
        #endregion

        #region animator thread
        void StartAnimatorThread()
        {
            if (ThreadAnimator == null || ThreadAnimator.IsAlive == false)
            {
                bKeepAnimatorThreadRunning = true;
                ThreadAnimator = new Thread(new ThreadStart(AnimatorThreadProc));
                ThreadAnimator.IsBackground = true;
                ThreadAnimator.Name = "ThreadAnimator";
                ThreadAnimator.Start();
            }
        }

        void StopAnimatorThread()
        {
            if (ThreadAnimator != null && ThreadAnimator.IsAlive == true)
            {
                if (ThreadAnimator.ThreadState == ThreadState.Suspended
                    || ThreadAnimator.ThreadState == ThreadState.SuspendRequested)
                {
                    ThreadAnimator.Resume();
                }
                bKeepAnimatorThreadRunning = false;
                //ThreadAnimator.Abort();
            }
        }

        void AnimatorThreadProc()
        {
            this.EnableCtrl(this.buttonStop_, true);
            this.EnableCtrl(this.buttonPlay_, false);
            this.ShowFirstItem();
            DateTime dtLast = DateTime.Parse("01/01/2000");
            while (bKeepAnimatorThreadRunning)
            {
                TimeSpan span = DateTime.Now - dtLast;
                if (span.Milliseconds >= iAnimationDelayMS)
                {
                    if (this.ShowNextItem() == false)
                    {
                        this.ShowFirstItem();
                    }
                    dtLast = DateTime.Now;
                }

                //Thread.Sleep(FormLoadDeflection.iAnimationDelay);
            }
            this.EnableCtrl(this.buttonStop_, false);
            this.EnableCtrl(this.buttonPlay_, true);
            bKeepAnimatorThreadRunning = false;
        }
        #endregion       

    }
}