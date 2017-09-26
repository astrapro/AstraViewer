using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HEADSNeed.ASTRA.ASTRAClasses;
using System.IO;
using VectorDraw.Geometry;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.Dialogs;
using HEADSNeed.ASTRA.ASTRAClasses;
namespace HEADSNeed.ASTRA.ASTRAForms
{
    public partial class frmPostProcess : Form
    {
        ASTRADoc astDoc = null;
        ASTRADoc orgASTDoc = new ASTRADoc();
        ASTRADoc ASTDoc_Load1 = new ASTRADoc();
        ASTRADoc ASTDoc_Load2 = new ASTRADoc();
        ASTRADoc ASTDoc_Load3 = new ASTRADoc();

        vdDocument doc = null;
        vdDocument mainDoc;
        vdDocument postProcess1;
        vdDocument postProcess2;
        vdDocument postProcess3;

        Form frmMain = null;
        Form frmPP1 = null;
        Form frmPP2 = null;
        Form frmPP3 = null;
        string repFileName = "";

        BeamForceMomentCollection beamMomentCol = null;
        BeamForceMoment beamMoment = null;

        NodeDisplacementCollection nDispCol = null;
        NodeDisplacement ndisp = null;

        int nodeNo = 0;
        int loadCase = 0;

        public frmPostProcess(ASTRADoc AstDoc, vdDocument document)
        {
            InitializeComponent();
            astDoc = new ASTRADoc();
            astDoc = AstDoc;
            orgASTDoc = new ASTRADoc();
            orgASTDoc = AstDoc;

            doc = document;

            beamMomentCol = new BeamForceMomentCollection();
            beamMoment = new BeamForceMoment();
            repFileName = Path.Combine(Path.GetDirectoryName(astDoc.FileName), "ANALYSIS_REP.TXT");

            nDispCol = new NodeDisplacementCollection();
            ndisp = new NodeDisplacement();
        }
        public frmPostProcess(ASTRADoc AstDoc, vdDocument mainDoc, vdDocument postProcess1, vdDocument postProcess2, vdDocument postProcess3)
        {
            //InitializeComponent();
            //astDoc = new ASTRADoc();
            //astDoc = AstDoc;
            //orgASTDoc = AstDoc;
            //doc = mainDoc;

            ASTDoc_Load1 = new ASTRADoc(AstDoc.FileName);
            ASTDoc_Load2 = new ASTRADoc(AstDoc.FileName);
            ASTDoc_Load3 = new ASTRADoc(AstDoc.FileName);

            //ASTDoc_Load1 = AstDoc;
            //ASTDoc_Load2 = AstDoc;
            //ASTDoc_Load3 = AstDoc;

            InitializeComponent();
            astDoc = new ASTRADoc();
            astDoc = AstDoc;
            orgASTDoc = new ASTRADoc();
            orgASTDoc = AstDoc;

            doc = mainDoc;


            this.mainDoc = mainDoc;
            this.postProcess1 = postProcess1;
            this.postProcess2 = postProcess2;
            this.postProcess3 = postProcess3;

            beamMomentCol = new BeamForceMomentCollection();
            beamMoment = new BeamForceMoment();
            repFileName = Path.Combine(Path.GetDirectoryName(astDoc.FileName), "ANALYSIS_REP.TXT");

            nDispCol = new NodeDisplacementCollection();
            ndisp = new NodeDisplacement();
        }

        public frmPostProcess(ASTRADoc AstDoc, Form frmMain, Form frmPostProcess1, 
            Form frmPostProcess2, Form frmPostProcess3,vdDocument mainDoc, vdDocument postProcess1,
            vdDocument postProcess2, vdDocument postProcess3)
        {
            this.frmMain = frmMain;
            frmPP1 = frmPostProcess1;
            frmPP2 = frmPostProcess2;
            frmPP3 = frmPostProcess3;
            ASTDoc_Load1 = new ASTRADoc(AstDoc.FileName);
            ASTDoc_Load2 = new ASTRADoc(AstDoc.FileName);
            ASTDoc_Load3 = new ASTRADoc(AstDoc.FileName);

            //ASTDoc_Load1 = AstDoc;
            //ASTDoc_Load2 = AstDoc;
            //ASTDoc_Load3 = AstDoc;

            InitializeComponent();
            astDoc = new ASTRADoc();
            astDoc = AstDoc;
            orgASTDoc = new ASTRADoc();
            orgASTDoc = AstDoc;

            doc = mainDoc;


            this.mainDoc = mainDoc;
            this.postProcess1 = postProcess1;
            this.postProcess2 = postProcess2;
            this.postProcess3 = postProcess3;

            beamMomentCol = new BeamForceMomentCollection();
            beamMoment = new BeamForceMoment();
            repFileName = Path.Combine(Path.GetDirectoryName(astDoc.FileName), "ANALYSIS_REP.TXT");

            nDispCol = new NodeDisplacementCollection();
            ndisp = new NodeDisplacement();
        }

        private void frmPostProcess_Load(object sender, EventArgs e)
        {
            //if (astDoc.IsMovingLoad)
            //{
            //    SetPostProcessForMovingLoad();
            //}
            //else
                SetPostProcess(repFileName);

            beamMomentCol.CopyMembers(astDoc.Members);
            cmbLoadCase.Items.Clear();
            cmbMemberNo.Items.Clear();

            for (int i = 0; i < astDoc.Members.Count; i++)
            {
                if (!cmbMemberNo.Items.Contains(astDoc.Members[i].MemberNo))
                {
                    cmbMemberNo.Items.Add(astDoc.Members[i].MemberNo);
                }
            }
            for (int i = 0; i < beamMomentCol.Count; i++)
            {
                if (!cmbLoadCase.Items.Contains(beamMomentCol[i].LoadCase))
                {
                    cmbLoadCase.Items.Add(beamMomentCol[i].LoadCase);
                }
            }
            if (cmbLoadCase.Items.Count == 0)
            {
                cmbLoadCase.Items.Add(1);

            }
            
            if (cmbLoadCase.Items.Count > 0)
                cmbLoadCase.SelectedIndex = 0;
            if (cmbMemberNo.Items.Count > 0)
                cmbMemberNo.SelectedIndex = 0;
            if (cmbForce.Items.Count > 0)
                cmbForce.SelectedIndex = 0;
        }

        public void ShowMemberOnForm(vdLine vLine)
        {
            int mNo = MemberNo;
            for (int i = 0; i < astDoc.Members.Count; i++)
            {
                #region Switch Code
                //switch (cmbLoadCase.Text.Trim())
                //{
                //    case "1":
                //        if (ASTDoc_Load1.Members[i].StartNode.Point == vLine.StartPoint &&
                //   ASTDoc_Load1.Members[i].EndNode.Point == vLine.EndPoint)
                //        {


                //            cmbMemberNo.SelectedIndex = -1;
                //            cmbMemberNo.SelectedIndex = cmbMemberNo.Items.IndexOf(ASTDoc_Load1.Members[i].MemberNo);
                //        }
                //        break;
                //    case "2":
                //        if (ASTDoc_Load2.Members[i].StartNode.Point == vLine.StartPoint &&
                //   ASTDoc_Load2.Members[i].EndNode.Point == vLine.EndPoint)
                //        {
                //            cmbMemberNo.SelectedIndex = -1;
                //            cmbMemberNo.SelectedIndex = cmbMemberNo.Items.IndexOf(ASTDoc_Load2.Members[i].MemberNo);
                //        }
                //        break;
                //    case "3":
                //        if (ASTDoc_Load3.Members[i].StartNode.Point == vLine.StartPoint &&
                //   ASTDoc_Load3.Members[i].EndNode.Point == vLine.EndPoint)
                //        {
                //            cmbMemberNo.SelectedIndex = -1;
                //            cmbMemberNo.SelectedIndex = cmbMemberNo.Items.IndexOf(ASTDoc_Load3.Members[i].MemberNo);
                //            //cmbMemberNo.SelectedIndex = cmbMemberNo.Items.IndexOf(MemberNo);
                //        }
                //        break;

                //}
                #endregion

                //#region Switch Code

                if (astDoc.Members[i].StartNode.Point == vLine.StartPoint &&
                    astDoc.Members[i].EndNode.Point == vLine.EndPoint)
                {


                    cmbMemberNo.SelectedIndex = -1;
                    cmbMemberNo.SelectedIndex = cmbMemberNo.Items.IndexOf(astDoc.Members[i].MemberNo);
                }
                //if (astDoc.Members[i].StartNode.Point == vLine.StartPoint &&
                //    astDoc.Members[i].EndNode.Point == vLine.EndPoint)
                //{
                //    cmbMemberNo.SelectedIndex = cmbMemberNo.Items.IndexOf(MemberNo);
                //}
            }
        }

        public void SetPostProcessForMovingLoad()
        {
            string memberForceFile = Path.GetDirectoryName(astDoc.FileName);
            memberForceFile = Path.Combine(memberForceFile, "Memberforce.FIL");

            if (!File.Exists(memberForceFile)) return;

            List<string> lstStr = new List<string>(File.ReadAllLines(memberForceFile));


            // MemberForce.FIL
            // 0    1       2       3       4       5       6       7       8
            //MNO  LDCS     Node    R1      R2      R3      M1      M2      M3
            //  1   1        1       0.000   0.000   1.016   -0.157  -0.025  0.000
            //               2       0.000   0.000  -1.016    0.157  -1.029  0.000

            MyStrings mList = null;
            BeamForceMoment bfm = null;
            for (int i = 0; i < lstStr.Count; i++)
            {
                mList = new MyStrings(MyStrings.RemoveAllSpaces(lstStr[i]),' ');
                if (mList.Count == 9)
                {
                    bfm = new BeamForceMoment();
                    bfm.Member.MemberNo = mList.GetInt(0);
                    bfm.LoadCase = mList.GetInt(1);
                    bfm.StartForceMoment.R1 = mList.GetDouble(3);
                    bfm.StartForceMoment.R2 = mList.GetDouble(4);
                    bfm.StartForceMoment.R3 = mList.GetDouble(5);
                    bfm.StartForceMoment.M1 = mList.GetDouble(6);
                    bfm.StartForceMoment.M2 = mList.GetDouble(7);
                    bfm.StartForceMoment.M3 = mList.GetDouble(8);
                }
                else if (mList.Count == 7)
                {
                    //bfm = new BeamForceMoment();
                    //bfm.Member.MemberNo = mList.GetInt(0);
                    //bfm.LoadCase = mList.GetInt(1);
                    bfm.EndForceMoment.R1 = mList.GetDouble(1);
                    bfm.EndForceMoment.R2 = mList.GetDouble(2);
                    bfm.EndForceMoment.R3 = mList.GetDouble(3);
                    bfm.EndForceMoment.M1 = mList.GetDouble(4);
                    bfm.EndForceMoment.M2 = mList.GetDouble(5);
                    bfm.EndForceMoment.M3 = mList.GetDouble(6);
                    beamMomentCol.Add(bfm);
                }
            }
        }
        public void SetPostProcess(string repFileName)
        {
            string kStr = "";
            string option = "";

            try
            {
                StreamReader sr = new StreamReader(new FileStream(repFileName, FileMode.Open, FileAccess.Read));
                while (!sr.EndOfStream)
                {
                    kStr = sr.ReadLine().Trim().TrimEnd().TrimStart().ToUpper();
                    
                    if (kStr.Contains("P DELTA ANALYSIS FOR LOAD CASE")) break;


                    //   NODE   LOAD            X-            Y-            Z-            X-            Y-            Z-
                    if (kStr.Contains("NODE   LOAD            X-            Y-            Z-            X-            Y-            Z-"))
                    //if (kStr.Contains("N O D E   D I S P L A C E M E N T S / R O T A T I O N S"))
                    {
                        option = "NODE"; continue;
                    }
                    // BEAM LOAD     AXIAL       SHEAR       SHEAR     TORSION     BENDING     BENDING
                    if (kStr.Contains("BEAM LOAD     AXIAL       SHEAR       SHEAR     TORSION     BENDING     BENDING"))
                        //if (kStr.Contains(".....BEAM FORCES AND MOMENTS"))
                    {
                        option = "BEAM"; continue;
                    }
                    if (kStr.Contains("NO.  NO.    FORCE_X     FORCE_Y     FORCE_Z   MOMENT_X    MOMENT_Y    MOMENT_Z"))
                    {
                        option = "BEAM"; continue;
                    }
                    if (kStr.Contains("S T A T I C   S O L U T I O N   T I M E   L O G"))
                    {
                        option = ""; continue;
                    }
                    if (kStr.Contains("********************************"))
                    {
                        option = ""; continue;
                    }
                    switch (option)
                    {
                        case "NODE":
                            if (kStr != "")
                                SetProcessNode(option, kStr);
                            break;
                        case "BEAM":
                            if (kStr != "")
                                SetProcessBeam(option, kStr);
                            break;
                        default:
                            break;

                    }
                }
                sr.Close();

                SetASTDoc();
                ASTDoc_Load1.Members.CopyJointCoordinates(ASTDoc_Load1.Joints);
                ASTDoc_Load2.Members.CopyJointCoordinates(ASTDoc_Load2.Joints);
                ASTDoc_Load3.Members.CopyJointCoordinates(ASTDoc_Load3.Joints);
            }
            catch(Exception exx)
            {

            }
        }

        public void SetProcessNode(string op, string text)
        {
            //16       1  -0.64289E-02  -0.88362E-02   0.64941E-02   0.10075E-03  -0.16080E-03   0.75179E-02
            string temp = text.ToUpper().Trim().TrimEnd().TrimStart().Replace('\t', ' ');
            int indx = -1;

            while (temp.IndexOf("  ") != -1)
            {
                temp = temp.Replace("  ", " ");
            }
            MyStrings mList = new MyStrings(temp, ' ');

            double dx = 0.0d, dy = 0.0d, dz = 0.0d;
            try
            {
                // 0       1        2            3              4               5           6           7
                //16       1  -0.64289E-02  -0.88362E-02   0.64941E-02   0.10075E-03  -0.16080E-03   0.75179E-02
                ndisp = new NodeDisplacement();

                int index = 0;

                if (mList.StringList.Count == 8)
                {
                    nodeNo = mList.GetInt(index); index++;
                    ndisp.Node.NodeNo = nodeNo;
                    ndisp.LoadCase = mList.GetInt(index); index++;
                    ndisp.Tx = mList.GetDouble(index); index++;
                    ndisp.Ty = mList.GetDouble(index); index++;
                    ndisp.Tz = mList.GetDouble(index); index++;
                    ndisp.Rx = mList.GetDouble(index); index++;
                    ndisp.Ry = mList.GetDouble(index); index++;
                    ndisp.Rz = mList.GetDouble(index); index++;
                    nDispCol.Add(ndisp);
                }
                else if (mList.StringList.Count == 7)
                {
                    ndisp.Node.NodeNo = nodeNo;
                    ndisp.LoadCase = mList.GetInt(index); index++;
                    ndisp.Tx = mList.GetDouble(index); index++;
                    ndisp.Ty = mList.GetDouble(index); index++;
                    ndisp.Tz = mList.GetDouble(index); index++;
                    ndisp.Rx = mList.GetDouble(index); index++;
                    ndisp.Ry = mList.GetDouble(index); index++;
                    ndisp.Rz = mList.GetDouble(index); index++;
                    nDispCol.Add(ndisp);
                }
            }
            catch (Exception exx) { }
        }
        public void SetASTDoc()
        {
            int indx = 0;
            for (int i = 0; i < nDispCol.Count; i++)
            {
                if (nDispCol[i].LoadCase == 1)
                {
                    indx = ASTDoc_Load1.Joints.IndexOf(nDispCol[i].Node.NodeNo);

                    if (indx != -1)
                    {
                        ASTDoc_Load1.Joints[indx].Point.x += nDispCol[i].Tx;
                        ASTDoc_Load1.Joints[indx].Point.y += nDispCol[i].Ty;
                        ASTDoc_Load1.Joints[indx].Point.z += nDispCol[i].Tz;
                    }
                }
                else if (nDispCol[i].LoadCase == 2)
                {
                    indx = ASTDoc_Load2.Joints.IndexOf(nDispCol[i].Node.NodeNo);

                    if (indx != -1)
                    {
                        ASTDoc_Load2.Joints[indx].Point.x += nDispCol[i].Tx;
                        ASTDoc_Load2.Joints[indx].Point.y += nDispCol[i].Ty;
                        ASTDoc_Load2.Joints[indx].Point.z += nDispCol[i].Tz;
                    }
                }
                else if (nDispCol[i].LoadCase == 3)
                {
                    indx = ASTDoc_Load3.Joints.IndexOf(nDispCol[i].Node.NodeNo);

                    if (indx != -1)
                    {
                        ASTDoc_Load3.Joints[indx].Point.x += nDispCol[i].Tx;
                        ASTDoc_Load3.Joints[indx].Point.y += nDispCol[i].Ty;
                        ASTDoc_Load3.Joints[indx].Point.z += nDispCol[i].Tz;
                    }
                }
            }

        }
        public void SetProcessBeam(string op, string text)
        {
            // ANALYSIS_REP.TXT
            // 1   1  1.598E+02   1.178E+01  -6.206E-01  -4.740E-02  -4.551E+00   1.693E+01
            //       -1.598E+02  -1.178E+01   6.206E-01   4.740E-02   7.033E+00   3.017E+01


            string temp = text.ToUpper().Trim().TrimEnd().TrimStart().Replace('\t', ' ');
            while (temp.IndexOf("  ") != -1)
            {
                temp = temp.Replace("  ", " ");
            }
            MyStrings mList = new MyStrings(temp, ' ');


            try
            {
                if (mList.StringList.Count == 8)
                {

                    // 1   1  1.598E+02   1.178E+01  -6.206E-01  -4.740E-02  -4.551E+00   1.693E+01

                    beamMoment = new BeamForceMoment();

                    beamMoment.Member.MemberNo = mList.GetInt(0);
                    beamMoment.LoadCase = mList.GetInt(1);
                    beamMoment.StartForceMoment.R1 = mList.GetDouble(2);
                    beamMoment.StartForceMoment.R2 = mList.GetDouble(3);
                    beamMoment.StartForceMoment.R3 = mList.GetDouble(4);
                    beamMoment.StartForceMoment.M1 = mList.GetDouble(5);
                    beamMoment.StartForceMoment.M2 = mList.GetDouble(6);
                    beamMoment.StartForceMoment.M3 = mList.GetDouble(7);
                }
                if (mList.StringList.Count == 6)
                {

                    //       -1.598E+02  -1.178E+01   6.206E-01   4.740E-02   7.033E+00   3.017E+01
                   
                    beamMoment.EndForceMoment.R1 = mList.GetDouble(0);
                    beamMoment.EndForceMoment.R2 = mList.GetDouble(1);
                    beamMoment.EndForceMoment.R3 = mList.GetDouble(2);
                    beamMoment.EndForceMoment.M1 = mList.GetDouble(3);
                    beamMoment.EndForceMoment.M2 = mList.GetDouble(4);
                    beamMoment.EndForceMoment.M3 = mList.GetDouble(5);

                    beamMomentCol.Add(beamMoment);
                }
            }
            catch (Exception exx) { }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }

        private void cmbMemberNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetWork();
            astDoc.Members.ShowMember(astDoc.Members.IndexOf(MemberNo), doc);
            postProcess1.Redraw(true);
            postProcess2.Redraw(true);
            postProcess3.Redraw(true);
        }

        public int LoadCase
        {
            get
            {
                int loadcase = 0;
                try
                {
                    loadcase = int.Parse(cmbLoadCase.Text);
                }
                catch (Exception exx)
                {
                }
                return loadcase;
            }
        }
        public int MemberNo
        {
            get
            {
                int memNo = 0;
                try
                {
                    memNo = int.Parse(cmbMemberNo.Text);
                }
                catch (Exception exx)
                {
                }
                return memNo;
            }
        }
        private void SetWork()
        {
            int loadCase = 0;

            if (!int.TryParse(cmbLoadCase.Text, out loadCase))
            {
                loadCase = 0;
            }
            int indx = beamMomentCol.IndexOf(MemberNo, loadCase);

            try
            {
                try
                {
                        postProcess1.ActiveLayOut.Entities.RemoveAll();
                        astDoc.MemberLoads.ShowMember(postProcess1, astDoc.Members[astDoc.Members.IndexOf(MemberNo)],"MEMBER DIAGRAM");
                        astDoc.MemberLoads.ShowMember(postProcess1, astDoc.MemberLoads[astDoc.MemberLoads.IndexOf(MemberNo, LoadCase)]);
                }
                catch(Exception ex)
                {
                }
                try
                {
                    int mIndx = astDoc.MemberLoads.IndexOf(MemberNo, LoadCase);
                    if (mIndx != -1)
                    {
                        astDoc.MemberLoads.ShowMember(postProcess1, astDoc.MemberLoads[mIndx]);
                    }
                    else
                    {
                        postProcess2.ActiveLayOut.Entities.RemoveAll();
                        astDoc.MemberLoads.ShowMember(postProcess2, astDoc.Members[astDoc.Members.IndexOf(MemberNo)], "MEMBER DIAGRAM");
                        astDoc.MemberLoads.ShowMemberJointLoad(postProcess2, MemberNo, astDoc, LoadCase);

                        postProcess3.ActiveLayOut.Entities.RemoveAll();
                        astDoc.MemberLoads.ShowMember(postProcess3, astDoc.Members[astDoc.Members.IndexOf(MemberNo)], "MEMBER DIAGRAM");
                        astDoc.MemberLoads.ShowMemberJointLoad(postProcess3, MemberNo, astDoc, LoadCase);
                    }
                    astDoc.MemberLoads.ShowMemberJointLoad(postProcess1, MemberNo, astDoc, LoadCase);
                }
                catch (Exception exx)
                {

                }

                indx = beamMomentCol.IndexOf(MemberNo, loadCase);

                if (indx != -1)
                {
                    if (beamMomentCol[indx].StartForceMoment.M2 == 0.0)
                    {

                        if (astDoc.IsMovingLoad)
                        {
                            //beamMomentCol.DrawBeamForceMoment(postProcess2, MemberNo, BeamForceMomentCollection.eForce.M3, loadCase);
                            beamMomentCol.DrawBeamForceMoment(postProcess2, MemberNo, BeamForceMomentCollection.eForce.M1, loadCase);
                        }
                        else
                        {
                            DrawBeamMember(postProcess2, MemberNo, LoadCase, BeamForceMomentCollection.eForce.M3);
                        }
                    }
                    else
                    {
                        if (astDoc.IsMovingLoad)
                        {
                            beamMomentCol.DrawBeamForceMoment(postProcess2, MemberNo, BeamForceMomentCollection.eForce.M2, loadCase);
                            //beamMomentCol.DrawBeamForceMoment(postProcess2, MemberNo, BeamForceMomentCollection.eForce.M3, loadCase);
                        }
                        else
                        {
                            DrawBeamMember(postProcess2, MemberNo, LoadCase, BeamForceMomentCollection.eForce.M3);
                        }
                    }
                }

                indx = beamMomentCol.IndexOf(MemberNo, loadCase);
                if (indx != -1)
                {
                    if (beamMomentCol[indx].EndForceMoment.R3 == 0.0d &&
                        beamMomentCol[indx].StartForceMoment.R3 == 0.0d)
                    {
                        postProcess3.ActiveLayOut.Entities.RemoveAll();
                        astDoc.MemberLoads.ShowMember(postProcess3, astDoc.Members[astDoc.Members.IndexOf(MemberNo)], "MEMBER DIAGRAM");
                        astDoc.MemberLoads.ShowMemberJointLoad(postProcess3, MemberNo, astDoc, LoadCase);
                    }
                    else
                    {
                        beamMomentCol.DrawBeamForceMoment(postProcess3, MemberNo, BeamForceMomentCollection.eForce.R3, loadCase);
                    }
                }
                else
                {
                    postProcess2.ActiveLayOut.Entities.RemoveAll();
                    astDoc.MemberLoads.ShowMember(postProcess2, astDoc.Members[astDoc.Members.IndexOf(MemberNo)], "MEMBER DIAGRAM");
                    postProcess3.ActiveLayOut.Entities.RemoveAll();
                    astDoc.MemberLoads.ShowMember(postProcess3, astDoc.Members[astDoc.Members.IndexOf(MemberNo)], "MEMBER DIAGRAM");
                
                }

                //beamMomentCol.DrawBeamForceMoment(postProcess3, MemberNo, BeamForceMomentCollection.eForce.R2, loadCase);
            }
            catch (Exception ex)
            {
            }
           

        }

        private void cmbForce_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetWork();
        }

        private void cmbLoadCase_SelectedIndexChanged(object sender, EventArgs e)
        {
            //astDoc.Members.DrawMember(mainDoc);
            int loadCase = int.Parse(cmbLoadCase.Text);
            //if (!astDoc.IsMovingLoad)
                astDoc.DrawMemberDetails(mainDoc, LoadCase);

            //if (loadCase == 1)
            //{
            //    ASTDoc_Load1.Members.DrawMember(mainDoc);
                
            //}
            //if (loadCase == 2)
            //{
            //    ASTDoc_Load2.Members.DrawMember(mainDoc);
            //}
            //if (loadCase == 3)
            //{
            //    ASTDoc_Load3.Members.DrawMember(mainDoc);
            //}
            SetWork();
        }

        private void frmPostProcess_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                postProcess1.SaveAs(Path.Combine(Application.StartupPath, "test.vdml"));
                postProcess2.SaveAs(Path.Combine(Application.StartupPath, "test.vdml"));
                postProcess3.SaveAs(Path.Combine(Application.StartupPath, "test.vdml"));
                frmPP1.Close();
                frmPP2.Close();
                frmPP3.Close();
                frmMain.WindowState = FormWindowState.Maximized;
            }
            catch (Exception exx) { }
            //astDoc.Members.DrawMember(mainDoc);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(mainDoc);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(this.Location.ToString());
            //MessageBox.Show(this.Size.ToString());
        }

        public double MaxX(gPoints gps)
        {
            double x = 0.0d;
            foreach(gPoint gp in gps)
            {
                if(Math.Abs(gp.x) > x)
                    x = Math.Abs(gp.x);
            }
            return x;
        }
        public double MaxY(gPoints gps)
        {
            double y = 0.0d;
            foreach(gPoint gp in gps)
            {
                if(Math.Abs(gp.y) > y)
                    y = Math.Abs(gp.y);
            }
            return y;
        }
        public void Draw_StructureGraph(vdDocument document)
        {

            gPoints gps = astDoc.BeamAnalysisCalc.GetPoints(10,astDoc.LoadUnitFactor);

            //Jay Shree Krishna
            //Jay Shree Ganesh
            document.ActiveLayOut.Entities.RemoveAll();

            #region MainLIne
            vdLine mainLine = new vdLine();
            mainLine.SetUnRegisterDocument(document);
            mainLine.setDocumentDefaults();

            mainLine.StartPoint = new gPoint(10.0d, 10.0d);
            mainLine.EndPoint = new gPoint(30.0d, 10.0d);

            document.ActiveLayOut.Entities.AddItem(mainLine);
            #endregion

            #region Side Line 1
            vdLine sideLine1 = new vdLine();
            sideLine1.SetUnRegisterDocument(document);
            sideLine1.setDocumentDefaults();
            sideLine1.StartPoint = new gPoint(10, 5);
            sideLine1.EndPoint = new gPoint(10, 15);

            document.ActiveLayOut.Entities.AddItem(sideLine1);

            #endregion

            #region Side Line 2
            vdLine sideLine2 = new vdLine();
            sideLine2.SetUnRegisterDocument(document);
            sideLine2.setDocumentDefaults();
            sideLine2.StartPoint = new gPoint(30, 5);
            sideLine2.EndPoint = new gPoint(30, 15);
            document.ActiveLayOut.Entities.AddItem(sideLine2);
            #endregion

            #region Draw PolyLine

            vdPolyline pLine = new vdPolyline();
            pLine.SetUnRegisterDocument(document);
            pLine.setDocumentDefaults();
            //pLine.VertexList.Add(mainLine.StartPoint);

            double st, en, highest;

            st = en = highest = 0.0d;
            //if (IsShear)
            //{
                highest = MaxY(gps);
            //}
            //else
            //{
            //    highest = MaxMoment(SpanNo);
            //}


            double xIncr = 0.0d;
            
            for (int i = 0; i < gps.Count; i++)
            {
               
                    st = -gps[i].y;
                   
                    gPoint gp = new gPoint();

                    gp.x = mainLine.StartPoint.x + xIncr;
                    xIncr += 2.0d;
                    st = -st;
                    if (st > 0)
                    {
                        gp.y = mainLine.StartPoint.y + (sideLine1.EndPoint.y - mainLine.StartPoint.y) * (st / highest);
                    }
                    else
                    {
                        gp.y = mainLine.StartPoint.y - (sideLine1.StartPoint.y - mainLine.StartPoint.y) * (st / highest);
                    }
                    pLine.VertexList.Add(gp);


                    vdLine vLine = new vdLine();
                    vLine.SetUnRegisterDocument(document);
                    vLine.setDocumentDefaults();
                    vLine.StartPoint = gp;
                    vLine.EndPoint = new gPoint(gp.x, mainLine.StartPoint.y);
                    document.ActiveLayOut.Entities.AddItem(vLine);

                    vdText yValue = new vdText();
                    yValue.SetUnRegisterDocument(document);
                    yValue.setDocumentDefaults();
                    yValue.InsertionPoint = vLine.EndPoint;
                    yValue.TextString = st.ToString("0.00");
                    if (st > 0)
                    {

                        yValue.Rotation = 270.0d * Math.PI / 180.0d;
                        yValue.InsertionPoint.x -= 0.25d;
                        yValue.InsertionPoint.y -= 0.5d;
                    }
                    else
                    {
                        yValue.Rotation = 90.0d * Math.PI / 180.0d;
                        yValue.InsertionPoint.x += 0.25d;
                        yValue.InsertionPoint.y += 0.5d;
                    }
                    yValue.Height = 1.0d;
                    document.ActiveLayOut.Entities.AddItem(yValue);
            }
            

            document.ShowUCSAxis = false;
            document.ActiveLayOut.Entities.AddItem(pLine);

            #endregion

            #region Draw Rectangle
            vdRect rect = new vdRect();
            rect.SetUnRegisterDocument(document);
            rect.setDocumentDefaults();
            rect.InsertionPoint = new gPoint(sideLine1.StartPoint.x - 2, sideLine1.StartPoint.y - 2);
            rect.Width = 23.0d;
            rect.Height = 13.0d;

            document.ActiveLayOut.Entities.AddItem(rect);
            #endregion

            document.Redraw(true);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(document);
        }

        public void DrawBeamMember(vdDocument document, int MemberNo,int LoadCase,BeamForceMomentCollection.eForce ef)
        {
            astDoc.BeamAnalysisCalc.InitializeVariable();

            //return;
            int MemberIndex = -1;
            int MemberLoadIndex = -1;
            int MemberPropIndex = -1;
            int MemberPostProcIndex = -1;


            MemberIndex = astDoc.Members.IndexOf(MemberNo);


            MemberLoadCollection mLoadCol = astDoc.MemberLoads.GetAllMemberLoad(MemberNo,LoadCase);
            MemberLoadIndex = astDoc.MemberLoads.IndexOf(MemberNo,LoadCase);
            MemberPropIndex = astDoc.MemberProperties.IndexOf(MemberNo);
            MemberPostProcIndex = beamMomentCol.IndexOf(MemberNo, LoadCase);

            
            if (MemberIndex != -1)
            {
                astDoc.BeamAnalysisCalc.Length = astDoc.Members[MemberIndex].StartNode.Point.Distance3D(
                    astDoc.Members[MemberIndex].EndNode.Point);
            }
            if (MemberLoadIndex != -1)
            {
                //if (astDoc.MemberLoads[MemberLoadIndex].LoadType == MemberLoad.eLoadType.Concentrate)
                //{
                //    BeamAnalysisCalculator.ConcentrateLoad cLoad = new BeamAnalysisCalculator.ConcentrateLoad();
                //    cLoad.Distance = astDoc.MemberLoads[MemberLoadIndex].DistanceFromStartNode;
                //    cLoad.Force = astDoc.MemberLoads[MemberLoadIndex].Value;
                //    astDoc.BeamAnalysisCalc.ConsLoads.Add(cLoad);
                //}
                //else
                //{
                //    astDoc.BeamAnalysisCalc.UdlLoad = astDoc.MemberLoads[MemberLoadIndex].Value;
                //}

                astDoc.BeamAnalysisCalc.ConsLoads.Clear();

                for (int ii = 0; ii < mLoadCol.Count; ii++)
                {
                    if (mLoadCol[ii].LoadType == MemberLoad.eLoadType.Concentrate
                        && mLoadCol[ii].Member.MemberNo == MemberNo)
                    {
                        BeamAnalysisCalculator.ConcentrateLoad cLoad = new BeamAnalysisCalculator.ConcentrateLoad();
                        cLoad.Distance = mLoadCol[ii].DistanceFromStartNode;
                        cLoad.Force = mLoadCol[ii].Value;
                        astDoc.BeamAnalysisCalc.ConsLoads.Add(cLoad);
                    }
                    else
                    {
                        astDoc.BeamAnalysisCalc.UdlLoad = astDoc.MemberLoads[MemberLoadIndex].Value;
                    }
                }
            }
            if (MemberPropIndex != -1)
            {
            }
            if (MemberPostProcIndex != -1)
            {
                switch (ef)
                {
                    case BeamForceMomentCollection.eForce.M1:
                        astDoc.BeamAnalysisCalc.StartMoment = beamMomentCol[MemberPostProcIndex].StartForceMoment.M1;
                        astDoc.BeamAnalysisCalc.EndMoment = beamMomentCol[MemberPostProcIndex].EndForceMoment.M1;
                        break;
                    case BeamForceMomentCollection.eForce.M2:
                        astDoc.BeamAnalysisCalc.StartMoment = beamMomentCol[MemberPostProcIndex].StartForceMoment.M2;
                        astDoc.BeamAnalysisCalc.EndMoment = beamMomentCol[MemberPostProcIndex].EndForceMoment.M2;
                        break;
                    case BeamForceMomentCollection.eForce.M3:
                        astDoc.BeamAnalysisCalc.StartMoment = beamMomentCol[MemberPostProcIndex].StartForceMoment.M3;
                        astDoc.BeamAnalysisCalc.EndMoment = beamMomentCol[MemberPostProcIndex].EndForceMoment.M3;
                        break;
                }
            }
            try
            {
                gPoints gps = astDoc.BeamAnalysisCalc.GetPoints(10, astDoc.LoadUnitFactor);

                if (astDoc.Members[MemberIndex].StartNode.Point.y == astDoc.Members[MemberIndex].EndNode.Point.y)
                {
                    beamMomentCol.DrawBeamForceMoment_Bending(document,MemberNo, LoadCase, ef, gps);
                }
                else
                {
                    beamMomentCol.DrawBeamForceMoment(document, MemberNo, ef, LoadCase);
                }
            }
            catch (Exception exx)
            { }

            
        }

        private void cmbMemberNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetWork();
                astDoc.Members.ShowMember(astDoc.Members.IndexOf(MemberNo), doc);
                postProcess1.Redraw(true);
                postProcess2.Redraw(true);
                postProcess3.Redraw(true);
            }
        }

    }

    
}
