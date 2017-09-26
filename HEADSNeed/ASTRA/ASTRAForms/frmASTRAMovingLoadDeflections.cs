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
using MovingLoadAnalysis;
using MovingLoadAnalysis.DataStructure;
namespace HEADSNeed.ASTRA.ASTRAForms
{
    public partial class frmASTRAMovingLoadDeflections : Form
    {
        int iMemberNo, iLoadCase;
        ASTRADoc AST_DOC = null;
        ASTRADoc AST_DOC_ORG = null;
        LoadDeflection ld = null;
        vdDocument doc = null;
        vdDocument MainDoc = null;
        Form[] frms = null;
        Form report_frm = null;
        bool bIsNext = true;
        double max_x = 0.0;
        double max_y = 0.0;
        public frmASTRAMovingLoadDeflections(ASTRADoc astDoc, vdDocument vDoc, vdDocument mainDoc, Form[] frms)
        {
            //vDoc.DisableUndoRecording(false);
            if (vDoc != null)
            {
                vDoc.UndoHistory.PushEnable(false);
                vDoc.CommandAction.Undo("BEGIN");
            }
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
            if (doc != null)
                Moving_Line();
        }
        public frmASTRAMovingLoadDeflections(ASTRADoc astDoc, vdDocument vDoc, vdDocument mainDoc, Form[] frms, Form report_form)
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
            if (doc != null)
                Moving_Line();

        }

        public bool IsMovingLoad { get { return AST_DOC_ORG.IsMovingLoad; } }
        
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

        public double Factor
        {
            get
            {
                return MovingLoadAnalysis.MyList.StringToDouble(txtDefFactor.Text, 1);
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
                if (AST_DOC_ORG.IsMovingLoad)
                    AST_DOC_ORG.Members.DrawMember(MainDoc);
                else
                    AST_DOC_ORG.Members.DrawMember(doc);
            }
            CleanMemory();
        }
        public void Stop()
        {
            StartTimer(false);
            for (int i = 0; i < list_vehicle.Count; i++)
            {
                list_vehicle[i].Set_Wheel_Distance();
            }
            cmbLoadCase.SelectedIndex = 1;
        }
        public void Set_Init()
        {
            ld = new LoadDeflection(AST_DOC_ORG, pbLoadDeflection);

            cmbLoadCase.Items.Clear();
            for (int j = 0; j <= ld.MaxLoadCase; j++)
            {
                cmbLoadCase.Items.Add(j);
            }
            //SetLoadIndex(1);


            if (AST_DOC_ORG.IsDynamicLoad)
            {
                AST_DOC_ORG.DrawMemberDetails(MainDoc, 1);
            }
            if (MainDoc != null && AST_DOC_ORG.IsMovingLoad)
                MovingLoad_Initialize(MainDoc, AST_DOC_ORG.FileName);


            if (AST_DOC_ORG.IsDynamicLoad)
            {
                this.Text = "EIGEN VALUE ANALYSIS";
                //lblLoadCase.Text = "EIGEN VALUE";
            }
            else
            {
                this.Text = "MOVING LOAD DISPLAY";
                //lblLoadCase.Text = "LOAD CASE";
            }
            cmbInterval.SelectedIndex = 0;
            ////if (AST_DOC_ORG.IsMovingLoad)
            ////    AST_DOC_ORG.Draw_Main_MemberDetails(MainDoc, 1);

            //cmbLoadCase.SelectedIndex = -1;
            //cmbLoadCase.SelectedIndex = 0;

            //SetLoadIndex(1);
            cmbLoadCase.SelectedIndex = 1;
            

            //frmMovingForces f = new frmMovingForces(doc, AST_DOC_ORG.FileName);
            //f.Owner = this;
            //f.Show();

            if (AST_DOC_ORG.IsMovingLoad)
                this.Height = 107;
            else
                this.Height = 79;



            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(MainDoc);


        }

        private void StartTimer(bool flag)
        {
            int interval = 100;
            try
            {

                MovingLoadAnalysis.MyList m = new MovingLoadAnalysis.MyList(cmbInterval.Text, ' ');

                double dd = m.GetDouble(0);
                if (dd < 0.3)
                    dd = dd * 100.0d;
                else
                    dd = dd * 1000.0d;

                //if (dd < 0.3)
                //    dd = dd * 1.0d;
                //else
                //    dd = dd * 100.0d;

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

        public void CleanMemory()
        {
            try
            {
                VectorDraw.Professional.Memory.vdMemory.Collect();
                GC.Collect();
            }
            catch (Exception ex) { }
        }
        //Chiranjit [2011 12 14]
        void ResizeText(double text_size)
        {
            for (int i = 0; i < MainDoc.ActiveLayOut.Entities.Count; i++)
            {
                vdText t = MainDoc.ActiveLayOut.Entities[i] as vdText;
                vdMText m = MainDoc.ActiveLayOut.Entities[i] as vdMText;

                if (t != null)
                {
                    t.Height = text_size;
                    t.Update();
                }
                else if (m != null)
                {
                    m.Height = text_size;
                    m.Update();
                }
            }
            MainDoc.Redraw(true);
            MainDoc.Update();
        }

        #region  Chiranjit [2011 09 24] New Moving load Animation
        List<MLoadData> load_data = null;
        List<VehicleLoad> list_vehicle = null;
        public void MovingLoad_Initialize(vdDocument vDoc, string file_name)
        {
            //InitializeComponent();
            if (doc != null)
                AST_DOC_ORG.Members.DrawMember(doc);
            if (MainDoc != null)
                AST_DOC_ORG.Members.DrawMember(MainDoc);

            doc = vDoc;

            //AST_DOC_ORG.Members.DrawMember(MainDoc);
            //AST_DOC_ORG.Joints.DrawJointsText(MainDoc, 0.1);


            string load_file = Path.Combine(Path.GetDirectoryName(file_name), "LL.fil");
            if (!File.Exists(load_file)) load_file = Path.Combine(Path.GetDirectoryName(file_name), "LL.txt");

            if (File.Exists(load_file))
                load_data = MLoadData.GetLiveLoads(load_file);
            else
            {
                MessageBox.Show(this, load_file + " not found.");
                return;
            }

            VehicleLoad vl1 = null;

            for (int i = 0; i < AST_DOC_ORG.Load_Geneartion.Count; i++)
            {
                for (int j = 0; j < load_data.Count; j++)
                {
                    if (AST_DOC_ORG.Load_Geneartion[i].TypeNo == load_data[j].TypeNo)
                    {
                        try
                        {
                            //vl1 = new VehicleLoad(doc, MainDoc, load_data[j].Distances.Text);
                            vl1 = new VehicleLoad(MainDoc, MainDoc, load_data[j]);
                            vl1.TotalLength = AST_DOC_ORG.AnalysisData.Length;
                            vl1.XINC = AST_DOC_ORG.Load_Geneartion[i].XINC;
                            vl1.X = AST_DOC_ORG.Load_Geneartion[i].X;
                            vl1.Y = AST_DOC_ORG.Load_Geneartion[i].Y;
                            vl1.Z = AST_DOC_ORG.Load_Geneartion[i].Z;
                            if (list_vehicle == null)
                                list_vehicle = new List<VehicleLoad>();
                            list_vehicle.Add(vl1);
                        }
                        catch (Exception ex) { }
                    }
                }
            }

            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(MainDoc);
            //VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VFront(MainDoc);
        }
        void RunMovingLoad()
        {
            pbLoadDeflection.Visible = false;
            foreach (var item in list_vehicle)
            {
                item.Update(bIsNext);
            }
        }
        #endregion  Chiranjit [2011 09 24] New Moving load Animation

        #region Form Events
        private void cmbLoadCase_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region Chiranjit [2011 09 24]   New Moving Load Analysis
            try
            {

                //if (cmbLoadCase.SelectedIndex == 0)
                //{
                //    Stop();
                //}

                iLoadCase = int.Parse(cmbLoadCase.Text);
                txt_dist.Text = (AST_DOC_ORG.Load_Geneartion[0].XINC * iLoadCase).ToString();

                if (AST_DOC_ORG.IsDynamicLoad)
                {
                    ShowData();
                    iLoadCase = 1;
                    //VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);

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
                RunMovingLoad();
               
                //if (iLoadCase == 1)
                //{
                //    Moving_Line();
                //}
                //try
                //{
                //    GC.Collect();
                //    VectorDraw.Professional.Memory.vdMemory.Collect();


                //    MainDoc.UndoHistory.PushEnable(false);
                //    //doc.CommandAction.Undo("BEGIN");

                //    doc.UndoHistory.PushEnable(false);
                //    //mainDoc.CommandAction.Undo("BEGIN");

                //    AST_DOC_ORG.Draw_Main_MemberDetails(MainDoc, iLoadCase);
                //    AST_DOC.Load_Geneartion.Draw_Moving_Wheel(doc, 0.2, LoadCase, max_x, AST_DOC_ORG.Joints.BoundingBox.Bottom);
                //    AST_DOC.Load_Geneartion.Draw_Plan_Moving_Wheel(MainDoc, 0.2, LoadCase, max_x);
                //    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VFront(MainDoc);
                //}
                //catch (Exception ex) { }
            }
            else
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(MainDoc);

            //doc.Redraw(true);
            //MainDoc.Redraw(true);
            //AST_DOC.Load_Geneartion.Draw_Moving_Wheel(doc, 0.2, LoadCase, AST_DOC.Joints.Max_X_Positive);

            //VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VFront(MainDoc);
            //VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);

            //doc.Redraw(true);
            //MainDoc.Redraw(true);
            #endregion Chiranjit [2011 09 24]   Commented for New Moving Load Analysis

            //if (IsMovingLoad)
            //{
            //    RunMovingLoad();
            //}
        }

        private void btn_member_force_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(AST_DOC_ORG.MemberForce_File))
                    System.Diagnostics.Process.Start(AST_DOC_ORG.MemberForce_File);
            }
            catch (Exception exc) { }
        }
        private void btn_Pan_Click_1(object sender, EventArgs e)
        {
            MainDoc.CommandAction.Pan();
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
            try
            {
                string file_name = AST_DOC_ORG.FileName;


                file_name = Path.Combine(Path.GetDirectoryName(file_name), "ANALYSIS_REP.TXT");
                if (frmMovingForces.AnalysisResult == null)
                    frmMovingForces.AnalysisResult = new StructureMemberAnalysis(file_name);
                else
                {
                    if (frmMovingForces.AnalysisResult.Analysis_File != file_name)
                        frmMovingForces.AnalysisResult = new StructureMemberAnalysis(file_name);
                }
            }
            catch (Exception ex) { frmMovingForces.AnalysisResult = null; }



            pbLoadDeflection.Visible = true;
            Set_Init();
            //if (MainDoc != null && AST_DOC_ORG.IsMovingLoad)
            //    MovingLoad_Initialize(MainDoc, AST_DOC_ORG.FileName);


            //if (AST_DOC_ORG.IsDynamicLoad)
            //{
            //    this.Text = "EIGEN VALUE ANALYSIS";
            //    //lblLoadCase.Text = "EIGEN VALUE";
            //}
            //else
            //{
            //    this.Text = "MOVING LOAD DISPLAY";
            //    //lblLoadCase.Text = "LOAD CASE";
            //}
            //cmbInterval.SelectedIndex = 1;
            ////if (AST_DOC_ORG.IsMovingLoad)
            ////    AST_DOC_ORG.Draw_Main_MemberDetails(MainDoc, 1);

            //cmbLoadCase.SelectedIndex = -1;
            //cmbLoadCase.SelectedIndex = 0;


            ////frmMovingForces f = new frmMovingForces(doc, AST_DOC_ORG.FileName);
            ////f.Owner = this;
            ////f.Show();

            //if (AST_DOC_ORG.IsMovingLoad)
            //    this.Height = 107;
            //else
            //    this.Height = 79;



            //VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(MainDoc);



            ResizeText(0.2);

            Stop();

        }
        private void tmrLoadDeflection_Tick(object sender, EventArgs e)
        {
            if (bIsNext)
                SetLoadIndex(+1);
            else
                SetLoadIndex(-1);

            //Chiranjit [2011 12 14]
            //When Full load case shown timer should be stop
            if (cmbLoadCase.SelectedIndex == cmbLoadCase.Items.Count - 1)
                StartTimer(false);

            //try
            //{
                
            //    if (bIsNext)
            //        cmbLoadCase.SelectedIndex++;
            //    else
            //        cmbLoadCase.SelectedIndex--;

            //}
            //catch (Exception ex) { cmbLoadCase.SelectedIndex = 0; }
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
            if (cmbLoadCase.SelectedIndex == cmbLoadCase.Items.Count - 1)
            {
                Stop();
            }
            if (cmbLoadCase.SelectedIndex == 0)
            {
                Stop();
            }
            StartTimer(true);
        }
        private void btnAutoPrev_Click(object sender, EventArgs e)
        {
            bIsNext = false;
            StartTimer(false);
            CleanMemory();
            if (cmbLoadCase.SelectedIndex != 0)
                SetLoadIndex(-1);
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            Stop();

            //StartTimer(false);
            //for (int i = 0; i < list_vehicle.Count; i++)
            //{
            //    list_vehicle[i].Set_Wheel_Distance();
            //}
            //cmbLoadCase.SelectedIndex = 1;
            //CleanMemory();
            ////tmrLoadDeflection.Stop();
            //StartTimer(false);
            //Set_Init();
            //Set_Init();
            //if (MainDoc != null && AST_DOC_ORG.IsMovingLoad)
            //    MovingLoad_Initialize(MainDoc, AST_DOC_ORG.FileName);
            ////cmbLoadCase.SelectedIndex = 1;
            //cmbLoadCase.SelectedIndex = 0;
            //VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(MainDoc);

        }
        private void btn_ZoomOut_Click(object sender, EventArgs e)
        {
            try
            {
                //cmbLoadCase.SelectedIndex = cmbLoadCase.Items.Count - 1;
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomOut_Ex(MainDoc);
                CleanMemory();
            }
            catch (Exception ex)
            {
            }
        }
        private void btn_ZoomIn_Click(object sender, EventArgs e)
        {
            try
            {
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomIn_Ex(MainDoc);
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
            //Chiranjit [2011 12 13]
            //grb_manual.Visible = !pbLoadDeflection.Visible;
            //grb_Auto.Visible = !pbLoadDeflection.Visible;
            //lblPleaseWait.Visible = pbLoadDeflection.Visible;


            grb_moving_controls.Visible = !pbLoadDeflection.Visible;
            //grb_Auto.Visible = !pbLoadDeflection.Visible;
            //lblPleaseWait.Visible = pbLoadDeflection.Visible;




            //lblFactor.Enabled = !AST_DOC_ORG.IsMovingLoad;

            //txtDefFactor.Visible = !pbLoadDeflection.Visible;
            //lblFactor.Visible = !pbLoadDeflection.Visible;
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
        #endregion Form Events

        private void btn_top_view_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Name == btn_top_view.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VFront(MainDoc);
            if (btn.Name == btn_side_view.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(MainDoc);
        }
        private void btn_force_loadcase_Click(object sender, EventArgs e)
        {
            if (AST_DOC_ORG.IsMovingLoad)
            {
                StartTimer(false);
                frmMovingForces frm1 = new frmMovingForces(MainDoc, AST_DOC_ORG.FileName, cmbLoadCase.SelectedIndex-1);
                frm1.Owner = this;
                //frm1.Size = new Size(242, 602);
                //frm.Location = new Point(vdPropertyGrid1.Width + 5, 70 + toolStrip1.Height + this.MdiChildren[0].Height);
                //this.MdiChildren[0].Width -= frm1.Width;

                //frm1.Location = new Point(vdPropertyGrid1.Width + this.MdiChildren[0].Width, toolStrip1.Height);
                //frm1.Location = new Point(vdPropertyGrid1.Width + this.MdiChildren[0].Width, this.Height - frmASTRA_MovingLoadDef.Height - toolStrip1.Height - this.MdiChildren[0].Height);
                //frmASTRA_MovingLoadDef.Report_Form = frm1;

                frm1.ShowDialog();
                StartTimer(true);
                
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(MainDoc);
            }
        }
        private void txt_text_size_TextChanged(object sender, EventArgs e)
        {


            double txt_size = MovingLoadAnalysis.MyList.StringToDouble(txt_text_size.Text, 0.2);

            ResizeText(txt_size);
            
        }
    }
}

// Chiranjit [2011 09 24] New Moving load Animation