using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HEADSNeed.ASTRA.ASTRAClasses;
using HEADSNeed.ASTRA.ASTRADrawingTools;
//using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Geometry;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using HEADSNeed.ASTRA.ASTRAClasses.SlabDesign;
namespace HEADSNeed.ASTRA.ASTRAForms
{
    public partial class frmASTRALoadDeflections : Form
    {
        int iMemberNo, iLoadCase;
        ASTRADoc AST_DOC = null;
        ASTRADoc AST_DOC_ORG = null;
        vdDocument doc = null;
        vdDocument MainDoc = null;
        Form[] frms = null;
        Form report_frm = null;
        bool bIsNext = true;
        LoadDeflection ld = null;
        double max_x = 0.0;
        double max_y = 0.0;
        public frmASTRALoadDeflections(ASTRADoc astDoc, vdDocument vDoc, vdDocument mainDoc, Form[] frms)
        {
            //vDoc.DisableUndoRecording(false);

            
            vDoc.UndoHistory.PushEnable(false);
            vDoc.CommandAction.Undo("BEGIN");

            mainDoc.UndoHistory.PushEnable(false);
            mainDoc.CommandAction.Undo("BEGIN");

            //if (vDoc.UndoHistory.Enable == true)
            //{
            //    this.ActiveVcadDoc.UndoHistory.PushEnable(false);
            //    this.ActiveVcadDoc.CommandAction.Undo("BEGIN");
            //}



            InitializeComponent();
            iMemberNo = 0;
            iLoadCase = -1;
            doc = vDoc;
            AST_DOC = new ASTRADoc();
            AST_DOC_ORG = astDoc;
            MainDoc = mainDoc;

            this.frms = frms;
            max_x = astDoc.Joints.Max_X_Positive;
            max_y = astDoc.Joints.Max_Y_Positive;
            //groupBox1.Visible = groupBox2.Visible = false;
            //ld = new LoadDeflection(astDoc,pbLoadDeflection);

            //cmbLoadCase.Items.Clear();
            //for (int j = 0; j <= ld.MaxLoadCase; j++)
            //{
            //    cmbLoadCase.Items.Add(j);
            //}
            //SetLoadIndex(1);

            //this.frms = frms;

            //if (astDoc.IsDynamicLoad)
            //{
            //    AST_DOC_ORG.DrawMemberDetails(MainDoc, 1);
            //}

            btnPause.Enabled = false;
            btnStop.Enabled = false;
            Moving_Line();

        }
        public frmASTRALoadDeflections(ASTRADoc astDoc, vdDocument vDoc, vdDocument mainDoc, Form[] frms, Form report_form)
        {
            //vDoc.DisableUndoRecording(false);

            report_frm = report_form;

            vDoc.UndoHistory.PushEnable(false);
            vDoc.CommandAction.Undo("BEGIN");

            mainDoc.UndoHistory.PushEnable(false);
            mainDoc.CommandAction.Undo("BEGIN");

            //if (vDoc.UndoHistory.Enable == true)
            //{
            //    this.ActiveVcadDoc.UndoHistory.PushEnable(false);
            //    this.ActiveVcadDoc.CommandAction.Undo("BEGIN");
            //}



            InitializeComponent();
            iMemberNo = 0;
            iLoadCase = -1;
            doc = vDoc;
            AST_DOC = new ASTRADoc();
            AST_DOC_ORG = astDoc;
            MainDoc = mainDoc;

            this.frms = frms;
            max_x = astDoc.Joints.Max_X_Positive;
            max_y = astDoc.Joints.Max_Y_Positive;
            //groupBox1.Visible = groupBox2.Visible = false;
            //ld = new LoadDeflection(astDoc,pbLoadDeflection);

            //cmbLoadCase.Items.Clear();
            //for (int j = 0; j <= ld.MaxLoadCase; j++)
            //{
            //    cmbLoadCase.Items.Add(j);
            //}
            //SetLoadIndex(1);

            //this.frms = frms;

            //if (astDoc.IsDynamicLoad)
            //{
            //    AST_DOC_ORG.DrawMemberDetails(MainDoc, 1);
            //}

            btnPause.Enabled = false;
            btnStop.Enabled = false;
            Moving_Line();

        }

        public Form Report_Form
        {
            set
            {
                report_frm = value; 
            }
        }
        public void Moving_Line()
        {
            #region ASTRA

            if (AST_DOC_ORG.IsMovingLoad)
            {
                AST_DOC.Members.DrawMember(doc);
                //Draw_ASTRA_Joint_Load();

                vdLine ln = new vdLine();
                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();
                //ln.StartPoint = new gPoint(0, max_y + 3);
                //ln.EndPoint = new gPoint(max_x, max_y + 3);
                ln.StartPoint.x = AST_DOC_ORG.Joints.BoundingBox.Left;
                ln.StartPoint.y = AST_DOC_ORG.Joints.BoundingBox.Top + 2;
                ln.EndPoint.x = AST_DOC_ORG.Joints.BoundingBox.Right;
                ln.EndPoint.y = AST_DOC_ORG.Joints.BoundingBox.Top + 2;

                doc.ActiveLayOut.Entities.AddItem(ln);

                double xinc = AST_DOC_ORG.Load_Geneartion[0].XINC;



                vdMText wheelPos = new vdMText();
                wheelPos.SetUnRegisterDocument(doc);
                wheelPos.setDocumentDefaults();
                //wheelPos.TextString = "WHEEL POSITION [ INCREMENT = " + xinc + " m. ], DECK SPAN LENGTH = " + max_x + " m.";
                wheelPos.TextString = "WHEEL POSITION [ INCREMENT = " + xinc + " m. ], DECK SPAN LENGTH = " + AST_DOC_ORG.Joints.BoundingBox.Width + " m.";
                wheelPos.InsertionPoint.x = AST_DOC_ORG.Joints.BoundingBox.MidPoint.x;
                wheelPos.InsertionPoint.y = AST_DOC_ORG.Joints.BoundingBox.Top + 2.5;
                wheelPos.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter;
                wheelPos.VerJustify = VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerCen;
                //wheelPos.InsertionPoint.x -= 2.4;
                //wheelPos.InsertionPoint.y -= 0.4;
                wheelPos.Height = 0.24;
                doc.ActiveLayOut.Entities.AddItem(wheelPos);




                vdMText nodalLoad = new vdMText();
                nodalLoad.SetUnRegisterDocument(doc);
                nodalLoad.setDocumentDefaults();
                nodalLoad.TextString = "NODAL LOADS";
                nodalLoad.InsertionPoint.x = AST_DOC_ORG.Joints.BoundingBox.MidPoint.x;
                nodalLoad.InsertionPoint.y = AST_DOC.Joints.BoundingBox.Bottom - 2.0;

                nodalLoad.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter;
                nodalLoad.VerJustify = VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerCen;
              
                nodalLoad.Height = 0.25;
                nodalLoad.PenColor = new vdColor(Color.Red);
                doc.ActiveLayOut.Entities.AddItem(nodalLoad);


                ln = new vdLine();
                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();
                ln.StartPoint.x = AST_DOC_ORG.Joints.BoundingBox.Left;
                ln.StartPoint.y = AST_DOC_ORG.Joints.BoundingBox.Top + 3.0;

                ln.EndPoint.x = AST_DOC_ORG.Joints.BoundingBox.Right;
                ln.EndPoint.y = AST_DOC_ORG.Joints.BoundingBox.Top + 3.0;
                doc.ActiveLayOut.Entities.AddItem(ln);



                double dx, dy, dz;

                int no = (int)(max_x / xinc);

                for (int n = 0; n < no; n++)
                {
                    dx = AST_DOC_ORG.Joints.BoundingBox.Left + n * xinc;
                    dy = AST_DOC_ORG.Joints.BoundingBox.Bottom;
                    ln = new vdLine();
                    ln.SetUnRegisterDocument(doc);
                    ln.setDocumentDefaults();
                    ln.StartPoint = new gPoint(dx, dy);
                    ln.EndPoint = new gPoint(dx, dy - 0.05);
                    doc.ActiveLayOut.Entities.AddItem(ln);
                }
            }
            #endregion
        }

        private void cmbLoadCase_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                iLoadCase = int.Parse(cmbLoadCase.Text);
               
                ShowData();
               
                if (AST_DOC_ORG.IsDynamicLoad)
                {
                    iLoadCase = 1;
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);

                    VectorDraw.Professional.Memory.vdMemory.Collect();
                    GC.Collect();
                    doc.UndoHistory.PushEnable(false);
                    doc.CommandAction.Undo("BEGIN");

                    //mainDoc.UndoHistory.PushEnable(false);
                    //mainDoc.CommandAction.Undo("BEGIN");

                    doc.Redraw(true);
                    MainDoc.Redraw(true);
                    return;
                }
            }
            catch (Exception ex)
            {
                //ex = ex;
            }

            if (AST_DOC_ORG.IsMovingLoad)
            {
                if (iLoadCase == 1)
                {
                    Moving_Line();
                }
                try
                {
                    GC.Collect();
                    VectorDraw.Professional.Memory.vdMemory.Collect();
                   
                    
                    MainDoc.UndoHistory.PushEnable(false);
                    //doc.CommandAction.Undo("BEGIN");

                    doc.UndoHistory.PushEnable(false);
                    //mainDoc.CommandAction.Undo("BEGIN");
                    
                    AST_DOC_ORG.Draw_Main_MemberDetails(MainDoc, iLoadCase);
                    AST_DOC.Load_Geneartion.Draw_Moving_Wheel(doc, 0.2, LoadCase, max_x, AST_DOC_ORG.Joints.BoundingBox.Bottom);
                    AST_DOC.Load_Geneartion.Draw_Plan_Moving_Wheel(MainDoc, 0.2, LoadCase, max_x);
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VFront(MainDoc);
                }
                catch (Exception ex) { }
            }
            else
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(MainDoc);


            //doc.Redraw(true);
            //MainDoc.Redraw(true);
            //AST_DOC.Load_Geneartion.Draw_Moving_Wheel(doc, 0.2, LoadCase, AST_DOC.Joints.Max_X_Positive);
            

            //VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VFront(MainDoc);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);

            doc.Redraw(true);
            MainDoc.Redraw(true);
        }
        public double Factor
        {
            get
            {
                return MyStrings.StringToDouble(txtDefFactor.Text, 1)*500;
            }
        }
        private void ShowData()
        {
            
            VectorDraw.Professional.Memory.vdMemory.Collect();

            string sLoad = "LOAD_" + cmbLoadCase.Text;
            AST_DOC = (ASTRADoc)ld.HashDeflection[sLoad];
            if (AST_DOC_ORG.IsMovingLoad == false)
                AST_DOC = ld.Get_ASTRADoc(LoadCase, Factor);

            if (AST_DOC != null)
            {
                if (AST_DOC_ORG.IsMovingLoad == false)
                    AST_DOC.Members.DrawMember(doc);
                //AST_DOC.Supports.DrawSupport(doc);
                
                if (AST_DOC.IsDynamicLoad)
                {
                    //AST_DOC.JointLoads.DrawJointLoads(doc, 1);
                    AST_DOC.JointLoads.DrawJointLoads(doc, 1);

                    AST_DOC_ORG.Members.DrawMember_Test(MainDoc);
                    if (AST_DOC_ORG.MemberLoads.Count > 0)
                        AST_DOC_ORG.MemberLoads.DrawMemberLoad(MainDoc, LoadCase);

                    if (AST_DOC_ORG.Supports.Count > 0)
                    {
                        AST_DOC_ORG.Supports.DrawSupport(MainDoc);
                        AST_DOC_ORG.Supports.DrawSupport(doc);
                    }
                    if (AST_DOC_ORG.JointLoads.Count > 0)
                        AST_DOC_ORG.JointLoads.DrawJointLoads(MainDoc, LoadCase);
                }
                else if (AST_DOC.IsMovingLoad)
                {
                    AST_DOC.JointLoads.DrawJointLoads_MovingLoad(doc, LoadCase);

                    //// Chiranjit [2010 05 20]

                    //int i = 0;

                    //for (i = 0; i < list_joint_index.Count; i++)
                    //{
                    //    doc.ActiveLayOut.Entities[list_joint_index[i]].visibility = vdFigure.VisibilityEnum.Invisible;
                    //}
                    
                    //int indx = -1;

                    //List<JointLoad> jload = AST_DOC.JointLoads.hashJointLoad[LoadCase] as List<JointLoad>;
                    //if (jload != null)
                    //{
                    //    for (i = 0; i < jload.Count; i++)
                    //    {

                    //        indx = list_joint_index[jload[i].Joint.NodeNo-1];
                    //        doc.ActiveLayOut.Entities[indx].visibility = vdFigure.VisibilityEnum.Visible;
                    //    }
                    //}
                    //doc.Redraw(true);

                }
                else
                {
                    //AST_DOC_ORG.JointLoads.CopyCoordinates(AST_DOC_ORG.Joints);

                    AST_DOC.JointLoads.DrawJointLoads(doc, LoadCase);

                    AST_DOC_ORG.Members.DrawMember_Test(MainDoc);
                    if (AST_DOC_ORG.MemberLoads.Count > 0)
                        AST_DOC_ORG.MemberLoads.DrawMemberLoad(MainDoc, LoadCase);

                    if (AST_DOC_ORG.Supports.Count > 0)
                    {
                        AST_DOC_ORG.Supports.DrawSupport(MainDoc);
                        AST_DOC_ORG.Supports.DrawSupport(doc);
                    }
                    if (AST_DOC_ORG.JointLoads.Count > 0)
                        AST_DOC_ORG.JointLoads.DrawJointLoads(MainDoc, LoadCase);
                }
                AST_DOC_ORG.JointLoads.CopyCoordinates(AST_DOC_ORG.Joints);
            }
            VectorDraw.Professional.Memory.vdMemory.Collect();

        }

        List<int> list_joint_index = new List<int>();
        private void Draw_ASTRA_Joint_Load()
        {
            ASTRAArrowLine aline = null;
            for (int i = 0; i < AST_DOC.Joints.Count; i++)
            {
                aline = new ASTRAArrowLine();
                aline.SetUnRegisterDocument(doc);
                aline.setDocumentDefaults();

                list_joint_index.Add(doc.ActiveLayOut.Entities.Count);
                doc.ActiveLayOut.Entities.AddItem(aline);

                aline.EndPoint = AST_DOC.Joints[i].Point;

                aline.StartPoint.x = aline.EndPoint.x;
                aline.StartPoint.y = aline.EndPoint.y + 2.0d;
                aline.StartPoint.z = aline.EndPoint.z;


                if (i % 2 == 0)
                {
                    aline.visibility = vdFigure.VisibilityEnum.Invisible;
                }
                else
                {
                    aline.visibility = vdFigure.VisibilityEnum.Visible;
                }
            }
            doc.Redraw(true);
        }

        private void ShowMovingLoad()
        {
        }
        public int LoadCase
        {
            get
            {
                try
                {
                    iLoadCase = int.Parse(cmbLoadCase.Text);
                }
                catch (Exception exx)
                {
                }
                return iLoadCase;
            }

        }
       
        private void btnPrev_Click(object sender, EventArgs e)
        {
            SetLoadIndex(-1);
        }

        private void btnNext_Click_1(object sender, EventArgs e)
        {
            SetLoadIndex(+1);
        }
        private void SetLoadIndex(int indx)
        {
            int selIndex = cmbLoadCase.SelectedIndex;
            selIndex += indx;

            if (selIndex == cmbLoadCase.Items.Count)
            {
                selIndex = 0;
            }
            else if (selIndex == -1)
            {
                selIndex = cmbLoadCase.Items.Count - 1;
            }
            try
            {
                cmbLoadCase.SelectedIndex = selIndex;
            }
            catch (Exception ex) { }
            if (cmbLoadCase.SelectedIndex == -1)
            {
                AST_DOC_ORG.Members.DrawMember(doc);
            }
            CleanMemory();
        }

        private void frmASTRALoadDeflections_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                doc.SaveAs(Path.Combine(Application.StartupPath, "test.vdml"));
                this.frms[1].Close();
                this.frms[0].WindowState = FormWindowState.Maximized;
                report_frm.Close();
                ld.ThisThread.Abort();
            }
            catch (Exception ex)
            {
            }
        }

        private void frmASTRALoadDeflections_Load(object sender, EventArgs e)
        {
            pbLoadDeflection.Visible = true;
            Set_Init();
            if (AST_DOC_ORG.IsDynamicLoad)
            {
                this.Text = "EIGEN VALUE ANALYSIS";
                lblLoadCase.Text = "EIGEN VALUE";
            }
            else
            {
                this.Text = "LOAD DEFLECTION ANALYSIS";
                lblLoadCase.Text = "LOAD CASE";
            }
            cmbInterval.SelectedIndex = 9;
            if (AST_DOC_ORG.IsMovingLoad)
                AST_DOC_ORG.Draw_Main_MemberDetails(MainDoc, 1);

            cmbLoadCase.SelectedIndex = -1;
            cmbLoadCase.SelectedIndex = 0;
        }

        private void tmrLoadDeflection_Tick(object sender, EventArgs e)
        {
            if(bIsNext)
                SetLoadIndex(+1);
            else
                SetLoadIndex(-1);

        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            VectorDraw.Professional.Memory.vdMemory.Collect();
            GC.Collect();
            tmrLoadDeflection.Stop();
            StartTimer(false);
            //SLAB01 slb01 = new SLAB01();
        }

        private void btnNextAuto_Click(object sender, EventArgs e)
        {
            bIsNext = true;
            StartTimer(true);
        }

        private void btnAutoPrev_Click(object sender, EventArgs e)
        {
            bIsNext = false;
            StartTimer(true);
            CleanMemory();
        }


        public void Set_Init()
        {
            ld = new LoadDeflection(AST_DOC_ORG, pbLoadDeflection);

            cmbLoadCase.Items.Clear();
            for (int j = 0; j <= ld.MaxLoadCase; j++)
            {
                cmbLoadCase.Items.Add(j);
            }
            SetLoadIndex(1);


            if (AST_DOC_ORG.IsDynamicLoad)
            {
                AST_DOC_ORG.DrawMemberDetails(MainDoc, 1);
            }
        }


        private void StartTimer(bool flag)
        {
            int interval = 1000;
            try
            {
                double dd = double.Parse(cmbInterval.Text.Replace("Sec", " ").Trim().TrimEnd().TrimStart());
                dd = dd * 1000.0d;
                interval = int.Parse(dd.ToString());
            }
            catch (Exception xex)
            {
            }
            tmrLoadDeflection.Interval = interval;

            tmrLoadDeflection.Enabled = flag;
            btnPause.Enabled = flag;
            btnStop.Enabled = flag;
            cmbInterval.Enabled = !flag;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            CleanMemory();
            tmrLoadDeflection.Stop();
            StartTimer(false);
            cmbLoadCase.SelectedIndex = 0;
            //System.GC.Collect();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            try
            {
                cmbLoadCase.SelectedIndex = cmbLoadCase.Items.Count - 1;
                CleanMemory();
            }
            catch (Exception ex)
            {
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            try
            {
                cmbLoadCase.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            ld = new LoadDeflection(AST_DOC_ORG);
        }

        private void pbLoadDeflection_VisibleChanged(object sender, EventArgs e)
        {
            grb_manual.Visible = !pbLoadDeflection.Visible;
            grb_Auto.Visible = !pbLoadDeflection.Visible;
            lblPleaseWait.Visible = pbLoadDeflection.Visible;
         
            //lblFactor.Enabled = !AST_DOC_ORG.IsMovingLoad;
            
            txtDefFactor.Visible = !pbLoadDeflection.Visible;
            lblFactor.Visible = !pbLoadDeflection.Visible;
            try
            {
                txtDefFactor.Text = ld.Factor.ToString("0.00");
                cmbLoadCase.SelectedIndex = -1;
                cmbLoadCase.SelectedIndex = 0;
                txtDefFactor.Enabled = !AST_DOC_ORG.IsMovingLoad;
            }
            catch (Exception ex)
            {
            }
        }
        public void CleanMemory()
        {
            try
            {
                VectorDraw.Professional.Memory.vdMemory.Collect();
                GC.Collect();
            }
            catch (Exception ex) { }
        }
       
    }
}
