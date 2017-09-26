using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using HEADSNeed.ASTRA.ASTRAClasses;

namespace HEADSNeed.ASTRA.ASTRAForms
{
    public partial class frmBeamAnalysis : Form
    {
        string filePath = "";
        vdDocument docmnt = null;
        vdDocument shearDoc = null;
        vdDocument momentDoc = null;
        
        BeamAnalysisCollection beamAnaCol = null;
        int PrevBeamHeight = 0;
        bool IsShearMoment = false;
        public frmBeamAnalysis(string fileName, vdDocument ShearDocument,vdDocument MomentDocument)
        {
            InitializeComponent();
            FilePath = fileName;
            beamAnaCol = new BeamAnalysisCollection();
            beamAnaCol.ReadFromFile(Path.Combine(FilePath, "TEST.REP"));
            shearDoc = ShearDocument;
            momentDoc = MomentDocument;


            cmbSpanNo.Items.Clear();
            for (int i = 0; i < BeamAnalysisCol.Count; i++)
            {
                if (!cmbSpanNo.Items.Contains(BeamAnalysisCol[i].SpanNo))
                {
                    cmbSpanNo.Items.Add(BeamAnalysisCol[i].SpanNo);
                }
            }
            IsShearMoment = true;

        }
        public frmBeamAnalysis(string fileName,vdDocument document)
        {
            InitializeComponent();
            FilePath = fileName;
            beamAnaCol = new BeamAnalysisCollection();
            beamAnaCol.ReadFromFile(Path.Combine(FilePath, "TEST.REP"));
            docmnt = document;
            cmbSpanNo.Items.Clear();
            for (int i = 0; i < BeamAnalysisCol.Count; i++)
            {
                if (!cmbSpanNo.Items.Contains(BeamAnalysisCol[i].SpanNo))
                {
                    cmbSpanNo.Items.Add(BeamAnalysisCol[i].SpanNo);
                }
            }
            IsShearMoment = false;
        }
        public vdDocument ShearDoc
        {
            get
            {
                return shearDoc;
            }
            set
            {
                shearDoc = value;
            }
        }
        public vdDocument MomentDoc
        {
            get
            {
                return momentDoc;
            }
            set
            {
                momentDoc = value;
            }
        }
        public vdDocument Doc
        {
            get
            {
                return docmnt;
            }
            set
            {
                docmnt = value;
            }
        }
        public string FilePath
        {
            get
            {
                return filePath;
            }
            set
            {
                if (File.Exists(value))
                    filePath = Path.GetDirectoryName(value);
                else
                    filePath = value;
            }
        }
        public BeamAnalysisCollection BeamAnalysisCol
        {
            get
            {
                return beamAnaCol;
            }
        }
        public void ShowData()
        {
            
            try
            {
                rtbBeamAnalysis.Lines = File.ReadAllLines(Path.Combine(FilePath,"test.rep"));
            }
            catch (Exception exx)
            {
            }
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog cdlg = new ColorDialog())
            {
                if (cdlg.ShowDialog() != DialogResult.Cancel)
                {
                    rtbBeamAnalysis.SelectionColor = cdlg.Color;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //using (FontDialog cdlg = new FontDialog())
            //{
            //    cdlg.Font = rtbBeamAnalysis.Font;
            //    if (cdlg.ShowDialog() != DialogResult.Cancel)
            //    {
            //        rtbBeamAnalysis.Font = cdlg.Font;
            //    }
            //}
        }

        private void frmBeamAnalysis_Load(object sender, EventArgs e)
        {
            ShowData();
            cmbSpanNo.SelectedIndex = 0;
            cmbLoadCase.SelectedIndex = 0;
        }

        private void cmbSpanNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowGraph();
            ShowSpanNo(cmbSpanNo.Text);
        }

        public void ShowSpanNo(string SpanNo)
        {
            string ss = "SPAN NO. " + SpanNo.ToString();
            try
            {
                int i = rtbBeamAnalysis.Text.IndexOf(ss);
                rtbBeamAnalysis.SelectionStart = i;
                rtbBeamAnalysis.SelectionLength = ss.Length;
                rtbBeamAnalysis.ShowSelectionMargin = true;
                rtbBeamAnalysis.ScrollToCaret();
                //rtbBeamAnalysis.

            }
            catch (Exception ex) { }
        }
        public void ShowGraph()
        {
            int SpanNo = 0;
            int LoadCaseNo = 0;

            if(int.TryParse(cmbSpanNo.Text,out SpanNo) && int.TryParse(cmbLoadCase.Text,out LoadCaseNo))
            {
                if (IsShearMoment)
                {
                    BeamAnalysisCol.Draw_StructureGraph(ShearDoc, SpanNo, LoadCaseNo, true);
                    BeamAnalysisCol.Draw_StructureGraph(MomentDoc, SpanNo, LoadCaseNo, false);

                }
                else
                    BeamAnalysisCol.Draw_StructureGraph(Doc, SpanNo, LoadCaseNo, rbtnShear.Checked);
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            ShowGraph();
        }

        private void chkHide_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHide.Checked)
            {
                PrevBeamHeight = rtbBeamAnalysis.Height;
                this.Height -= PrevBeamHeight;
                this.WindowState = FormWindowState.Normal;
                chkHide.Text = "Show Details";
            }
            else
            {
                this.Height += PrevBeamHeight;
                this.WindowState = FormWindowState.Normal;
                chkHide.Text = "Hide Details";
            }

            //BeamAnalysisCalculator beamCalculator = new BeamAnalysisCalculator();
            //beamCalculator.MainFunction(Path.Combine(FilePath,"TEST.REP"), 10, 122, 1200,100.0d,200.0d);
        }


    }
}
