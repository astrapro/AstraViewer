using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using HeadsUtils;
using HeadsUtils.Interfaces;
using HeadsFunctions1.Properties;
using System.Threading;
//using VectorDraw.Geometry;

namespace HeadsFunctions1.LSection
{
    internal partial class FormLSection : Form
    {
        # region private data
        
        //private bool isDrawLSec = false;
        private bool enableGrid = false;
        //private string modelName = string.Empty;
        //private string labelName = string.Empty;
        //private double xScale = 1.0;
        //private double yScale = 10.0;
        private LSTTypeItemCollection lstTypeItems = new LSTTypeItemCollection();
        private IHeadsApplication headsApp = null;
        System.Threading.Thread m_threadDraw;
        bool m_bKeepDrawThreadRunning = false;
        delegate void GenericFunCall();
        delegate void SetProgressValue(int iValue);
        delegate string RetStrFunCall();
        # endregion

        # region constructors
        public FormLSection(IHeadsApplication app)
        {
            InitializeComponent();
            this.headsApp = app;
            //string applicationDataPath = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            this.Init();
        }
        # endregion

        # region properties
        //public bool IsDrawSection
        //{
        //    get { return this.isDrawLSec; }
        //    set { this.isDrawLSec = value; }
        //}
        public bool EnableGrid
        {
            get { return this.enableGrid; }
            set { this.enableGrid = value; }
        }
        private bool IsRunning
        {
            get
            {
                return (m_threadDraw != null && m_threadDraw.IsAlive == true && m_bKeepDrawThreadRunning == true) ? true : false;
            }
        }
        # endregion

        # region private methods
        private bool InList(string name)
        {
            return this.lbStringLebels_.CheckedItems.Contains(name);
        }
        private void DrawStringSec()
        {
            
            double last_x2 = 0;
            double last_y2 = 0;
            double last_z2 = 0;
            bool isMod = false;
            bool isLabel = false;
            //BinaryReader filehandle = null;
            double x1 = 0;
            double y1 = 0;
            double z1 = 0;
            double x2 = 0;
            double y2 = 0;
            double z2 = 0;

            CLabtype lab = new CLabtype();
            CTXTtype txt = new CTXTtype();
            CStgType stg = new CStgType();
            CModType mod1 = new CModType();
            CPTStype pts = new CPTStype();

            CCfgtype cfg = this.headsApp.ActiveDocument.ConfigParam;
           
            int NumPts = 0;
            
            IHdPoint aPoint = null;
            IHdPolyline3D pLine = null;
            CLinetype uline = new CLinetype();
            string csTp = string.Empty;

            this.headsApp.ActiveDocument.DisableUndoRecording(true);
            //this.groupBoxLSectionParameters.Enabled = false;
            //this.buttonOK.Enabled = false;
            //this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

            BinaryReader filehandle = new BinaryReader(new FileStream(Path.Combine(this.headsApp.AppDataPath, "MODEL.FIL"), FileMode.Open, FileAccess.Read), Encoding.Default);
            NumPts = 0;
            SetProgressBarMaxValue(100);
            while((filehandle.BaseStream.Position < filehandle.BaseStream.Length) && (m_bKeepDrawThreadRunning == true))
            {
                lab = CLabtype.FromStream(filehandle);
                
                if (lab.attr == CLabtype.Type.Model)								// modelname
                {
                    mod1 = (CModType)lab.Tag;
                    if (pLine != null)
                    {
                        //pLine.Rotate(new CPoint3D(cfg.XLate_X, cfg.XLate_Y, 0)
                        //    , cfg.xfa * Math.PI / 180);
                    }
                    pLine = null;
                    NumPts = 0;

                    isMod = (mod1.Name == this.comboBoxModelName.SelectedItem.ToString()) ? true:false;
                }
                else if (lab.attr == CLabtype.Type.String)						//stringname
                {
                    stg = (CStgType)lab.Tag;
                    if (isMod)
                    {
                        isLabel = InList(stg.label);                        
                    }
                }
                else if (lab.attr == CLabtype.Type.Point)						//point
                {
                    pts = (CPTStype)lab.Tag;                    
                                        
                    x2 = pts.mc * cfg.XMetric;
                    y2 = pts.mz * cfg.YMetric;
                    z2 = pts.mz;

                    NumPts++;
                    x1 = last_x2;
                    y1 = last_y2;
                    z1 = last_z2;
                    last_x2 = x2;
                    last_y2 = y2;
                    last_z2 = z2;

                    if (isMod && isLabel)						//if model and string found
                    {
                        uline.x1 = x1;
                        uline.y1 = y1;
                        uline.z1 = z1;
                        uline.x2 = x2;
                        uline.y2 = y2;
                        uline.z2 = z2;

                        if (NumPts == 1)
                        {
                            aPoint = this.headsApp.ActiveDocument.DrawPoint(new CPoint3D(x2, y2, z2));                            
                        }
                        else if (NumPts == 2)
                        {
                            uline.Layer = this.headsApp.ActiveDocument.GetActiveLayer().Name;
                            uline.elatt = 1;
                            uline.laatt = 1;
                            uline.Label = mod1.Name;
                            uline.Label += ":";
                            uline.Label += stg.label;
                            uline.scatt = 1;
                            pLine = DrawingUtil.DrawPolyline3DDx(this.headsApp.ActiveDocument, true, uline);
                            aPoint.Erase();
                        }
                        else if (NumPts > 2)
                        {
                            pLine.AppendVertex(new CPoint3D(x2, y2, z2));
                        }                        
                    }
                }
                else if (lab.attr == CLabtype.Type.Text)						//text
                {
                    txt = (CTXTtype)lab.Tag;                                       
                }
                else if(lab.attr == CLabtype.Type.EndCode)						//end of polyline
		        {
			        pLine = null;
			        NumPts = 0;
		        }
                
                int iCurProgressInPercent = (int)(((double)filehandle.BaseStream.Position / (double)filehandle.BaseStream.Length) * 100.00);
                SetProgressBarValue(iCurProgressInPercent);
                System.Threading.Thread.Sleep(0);
            }

            filehandle.Close();
            this.headsApp.ActiveDocument.DisableUndoRecording(false);
            this.headsApp.ActiveDocument.RefreshDocument();
            this.headsApp.ZoomExtents();
            CloseFrom();
            m_bKeepDrawThreadRunning = false;
        }       
        private void Init()
        {
            this.lstTypeItems = new LSTTypeItemCollection();
            this.lstTypeItems.LoadFile(Path.Combine(this.headsApp.AppDataPath, "MODEL.LST"));
            this.PopulateComboboxItems();
            //this.textBoxScaleXDir.Text = "1.0";
            //this.textBoxScaleYDir.Text = "10.0";
        }
        private bool PopulateComboboxItems()
        {
            bool result = true;
            foreach (LSTTypeItem item in this.lstTypeItems)
            {
                if (this.comboBoxModelName.Items.Contains(item.Mod) == false)
                {
                    if (this.comboBoxModelName.Items.Contains(item.Mod) == false)
                    {
                        this.comboBoxModelName.Items.Add(item.Mod);
                    }
                }
            }
            if(this.comboBoxModelName.Items.Count > 0)
            {
                this.comboBoxModelName.SelectedIndex = 0;
            }
            return result;
        }
        private void UpdateDataSource()
        {
            string fillFilePath = Path.Combine(this.headsApp.AppDataPath, "MODEL.FIL");
            if (File.Exists(fillFilePath) == false)
            {
                System.Windows.Forms.MessageBox.Show("Fill File: " + fillFilePath + " can not be opened!", "L-Section Error");
                return;
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
            if ((iValue % 10) == 0)
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

        private string GetSelectedModelName()
        {
            string str = "";

            if (this.comboBoxModelName.InvokeRequired)
            {
                str = this.comboBoxModelName.Invoke(new RetStrFunCall(GetSelectedModelName)).ToString();
            }
            else
            {
                str = this.comboBoxModelName.SelectedItem.ToString();
                return str;

            }

            return str;
        }

        private void StartDraw()
        {
            if (this.IsRunning == false)
            {
                m_bKeepDrawThreadRunning = true;
                m_threadDraw = new Thread(new ThreadStart(DrawStringSec));
                m_threadDraw.IsBackground = false;
                m_threadDraw.Name = "LSecThread";
                m_threadDraw.Priority = ThreadPriority.Highest;
                m_threadDraw.Start();                
                //this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            }
        }

        private void StopDraw()
        {
            if (this.IsRunning == true)
            {
                m_bKeepDrawThreadRunning = false;
            }
        }      
        # endregion

        # region overrides
        protected override void OnClosing(CancelEventArgs e)
        {
            if ((e as FormClosingEventArgs).CloseReason == CloseReason.None)
            {
                e.Cancel = true;
                if (this.Visible == false)
                {
                    this.Visible = true;
                }
            }
            else
            {
                base.OnClosing(e);
            }
        }
        # endregion

        # region event-handlers
        private void comboBoxModelName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = this.comboBoxModelName.SelectedItem as string;
            if (string.IsNullOrEmpty(selectedItem) == true)
            {
                return;
            }
            
            LSTTypeItem item = this.lstTypeItems[selectedItem];
            this.lbStringLebels_.Items.Clear();
            foreach (string stgItem in item.Stg)
            {
                if (this.lbStringLebels_.Items.Contains(stgItem) == false)
                {
                    this.lbStringLebels_.Items.Add(stgItem);
                }
            }
            this.buttonOK.Enabled = false;
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.groupBoxLSectionParameters.Enabled = false;
            this.buttonOK.Enabled = false;
            this.headsApp.ActiveDocument.ConfigParam.XMetric = this.tbXScale_.Value;
            this.headsApp.ActiveDocument.ConfigParam.YMetric = this.tbYScale_.Value;
            this.StartDraw();
            
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();            
        }
        private void FormLSection_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.IsRunning)
            {
                StopDraw();
            }            
        }
        private void lbStringLebels__SelectedValueChanged(object sender, EventArgs e)
        {
            this.buttonOK.Enabled = (this.lbStringLebels_.CheckedItems.Count > 0) ? true : false;
        }
        # endregion        
        
    }
}
