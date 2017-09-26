using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Collections;
using System.Threading;
using System.Windows.Forms;



using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Geometry;

using HEADSNeed.ASTRA.ASTRADrawingTools;
using HEADSNeed.ASTRA.ASTRAClasses;
using MovingLoadAnalysis;
using MovingLoadAnalysis.DataStructure;

namespace HEADSNeed.ASTRA.ASTRAForms
{
    public partial class frm_ASTRA_Analysis : Form
    {
        string file_name = "";
        ASTRADoc astDoc;
        StructureMemberAnalysis StructureAnalysis { get; set; }
        public ASTRADoc AST_DOC
        {
            
            get
            {
                if (tc4.SelectedIndex == 0 && AST_DOC_ORG != null)
                    return AST_DOC_ORG;
                return astDoc;
            }
            set
            {
                astDoc = value;
            }
        }

        public ASTRADoc  AST_DOC_ORG { get; set; }

        bool bIsNext = true;
        LoadDeflection ld = null;
        double max_x = 0.0;
        double max_y = 0.0;

        int lastId = -1;

        public vdDocument VDoc
        {
            get
            {
                return vdScrollableControl1.BaseControl.ActiveDocument;
            }
        }

        public vdDocument defDoc
        {
            get
            {
                return vdScrollableControl2.BaseControl.ActiveDocument;
            }
        }
        public vdDocument envDoc
        {
            get
            {
                return vdScrollableControl3.BaseControl.ActiveDocument;
            }
        }
        public vdDocument maxDoc
        {
            get
            {
                return vdScrollableControl5.BaseControl.ActiveDocument;
            }
        }


        public vdDocument movDoc
        {
            get
            {
                return vdScrollableControl4.BaseControl.ActiveDocument;
            }
        }




        public vdDocument ActiveDoc
        {
            get
            {
                if (tc4.SelectedTab == tab_org_doc)
                    return MainDoc;
                else if (tc4.SelectedTab == tab_defl_doc)
                    return defDoc;
                else if (tc4.SelectedTab == tab_max_doc)
                    return maxDoc;
                else if (tc4.SelectedTab == tab_mov_doc)
                    return movDoc;
                else if (tc4.SelectedTab == tab_evlp_doc)
                    return envDoc;

                return defDoc;
            }
        }
        public vdDocument doc
        {
            get
            {
                return defDoc;
            }
        }
        public vdDocument MainDoc
        {
            get
            {
                return VDoc;
            }
        }

        int iLoadCase;

        public string File_Name
        {
            get
            {
                return file_name;
            }
            set
            {
                this.Text = "Analysis Process & Results [" + MyStrings.Get_Modified_Path(value) + "]";
                file_name = value;
            }
        }
        public string Analysis_File_Name
        {
            get
            {
                if (!File.Exists(File_Name)) return "";
                return Path.Combine(Path.GetDirectoryName(File_Name), "ANALYSIS_REP.TXT");
            }
        }
        public string LL_TXT
        {
            get
            {
                if (!File.Exists(File_Name)) return "";
                return Path.Combine(Path.GetDirectoryName(File_Name), "LL.TXT");
            }
        }
        bool IsMovingLoad = true;


        public frm_ASTRA_Analysis(string file_name, bool moving_load)
        {
            InitializeComponent();
            Base_Control_MouseEvent();
            AST_DOC = new ASTRADoc(file_name);
            IsMovingLoad = moving_load;
        }
        public frm_ASTRA_Analysis()
        {

            InitializeComponent();
            Base_Control_MouseEvent();

            //AST_DOC = new ASTRADoc();
            //AST_DOC_ORG = new ASTRADoc();
        }

        private void Base_Control_MouseEvent()
        {

            vdScrollableControl1.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
            vdScrollableControl1.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);
            vdScrollableControl2.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
            vdScrollableControl2.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);

            vdScrollableControl3.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
            vdScrollableControl3.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);

            vdScrollableControl4.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
            vdScrollableControl4.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);

            vdScrollableControl5.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
            vdScrollableControl5.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);
        }

        private void frm_ASTRA_Analysis_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < tc_pp_panel.TabPages.Count;i++)
            {
                tc_pp_panel.TabPages[i].Text = "";
            }

            if (AST_DOC != null)
            {

                File_Name = AST_DOC.FileName;






                if (File.Exists(File_Name))
                    rtb_input_file.Lines = File.ReadAllLines(File_Name);
                tc_pp_panel.Visible = true;
                sc3.Panel2Collapsed = true;
                //astDoc = new ASTRADoc(File_Name);
                StructureAnalysis = null;
                ld = null;
                if (File.Exists(LL_TXT) && astDoc.IsMovingLoad)
                    rtb_ll_txt.Lines = File.ReadAllLines(LL_TXT);
                if (File.Exists(Analysis_File_Name))
                    rtb_ana_rep.Lines = File.ReadAllLines(Analysis_File_Name);

                Load_ASTRA_Data();

                cmbLengthUnit.SelectedIndex = (int)AST_DOC.Base_LengthUnit;
                cmbForceUnit.SelectedIndex = (int)AST_DOC.Base_MassUnit;

                MyStrings ml = new MyStrings(astDoc.ProjectTitle, ' ');
                txtUserTitle.Text = ml.GetString(2).Trim();
                cmb_structure_type.SelectedItem = ml.StringList[1];



                if (IsMovingLoad)
                {
                    tc5.SelectedIndex = 2;
                    tc1.SelectedIndex = 3;
                }
                else
                    tc6.SelectedIndex = 1;
            }



        }

        private void Load_ASTRA_Data()
        {

            if (defDoc != null)
            {
                defDoc.ActiveLayOut.Entities.RemoveAll();
                defDoc.Redraw(true);
            }
            if (envDoc != null)
            {
                envDoc.ActiveLayOut.Entities.RemoveAll();
                envDoc.Redraw(true);
            }
            if (maxDoc != null)
            {
                maxDoc.ActiveLayOut.Entities.RemoveAll();
                maxDoc.Redraw(true);
            }
            if (movDoc != null)
            {
                movDoc.ActiveLayOut.Entities.RemoveAll();
                movDoc.Redraw(true);
            }
            if (AST_DOC.Joints.Count == 0 ||
                AST_DOC.Members.Count == 0)
            {
                //MessageBox.Show("This file is not in correct format. Please select another file.", "ASTRA", MessageBoxButtons.OK);
                //this.Close();
                return;
            }



            VDoc.Palette.Background = Color.White;
            //DefDoc.Palette.Background = Color.White;
            AST_DOC.MemberProperties.CopyMemberIncidence(astDoc.Members);
            AST_DOC.Members.DrawMember(VDoc);
            AST_DOC.Elements.DrawElements(VDoc);
            AST_DOC.Supports.DrawSupport(VDoc);
            VDoc.Redraw(true);

            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Vrot(VDoc);
            SetGridWithNode();
            SetGridWithMember();
            SetGridWithElement();
            Set_Loadings();
            chk_joints.Checked = true;
            chk_mems.Checked = true;
            chk_elems.Checked = true;
            chk_suprts.Checked = true;

            if (AST_DOC.Elements.Count > 0)
                chk_suprts.Checked = true;

            for (int i = 1; i < 100; i++) cmb_text_size.Items.Add(i);

            cmb_text_size.SelectedIndex = 1;
            timer1.Start();
        }

        private void toolStripButtons_Click(object sender, EventArgs e)
        {

            vdDocument VD = VDoc;

            if (tc4.SelectedIndex == 1) VD = defDoc;
            else if (tc4.SelectedIndex == 2) VD = envDoc;


            VD =    ActiveDoc;

            ToolStripButton tsb = sender as ToolStripButton;
            if (tsb.Name == tsb_3D_rotate.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Vrot(VD);
            else if (tsb.Name == tsb_VTop.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(VD);
            else if (tsb.Name == tsb_VBot.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VBottom(VD);
            else if (tsb.Name == tsb_VLeft.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VLeft(VD);
            else if (tsb.Name == tsb_VRight.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VRight(VD);
            else if (tsb.Name == tsb_VFront.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VFront(VD);
            else if (tsb.Name == tsb_VBack.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VBack(VD);
            else if (tsb.Name == tsb_VNE.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VINE(VD);
            else if (tsb.Name == tsb_VNW.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VINW(VD);
            else if (tsb.Name == tsb_VSE.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VISE(VD);
            else if (tsb.Name == tsb_VSW.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VISW(VD);
            else if (tsb.Name == tsb_ZoomA.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomA_Ex(VD);
            else if (tsb.Name == tsb_ZoomE.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomE_Ex(VD);
            else if (tsb.Name == tsb_ZoomP.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomP_Ex(VD);

            else if (tsb.Name == tsb_ZoomW.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomW_Ex(VD);

            else if (tsb.Name == tsb_ZoomIn.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomIn_Ex(VD);
            else if (tsb.Name == tsb_ZoomOut.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomOut_Ex(VD);

            else if (tsb.Name == tsb_Pan.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.PanEx(VD);
            else if (tsb.Name == tsb_Save.Name)
            {
                SaveDrawing(VD);
            }
            else if (tsb.Name == tsb_ShadeOn.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_ShadeOn(VD);
            else if (tsb.Name == tsb_Wire.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Wire(VD);
         
        }

        private static void SaveDrawing(vdDocument VD)
        {

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "VDML File (*.vdml)|*.vdml|DXF File (*.dxf)|*.dxf|DWG File (*.dwg)|*.dwg";
                if (sfd.ShowDialog() != DialogResult.Cancel)
                {
                    if (VD.SaveAs(sfd.FileName))
                    {
                        MessageBox.Show("File Saved successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void frm_ASTRA_Analysis_SizeChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }

        #region CAD Methods
        void BaseControl_vdMouseDown(MouseEventArgs e, ref bool cancel)
        {

            if ((System.Windows.Forms.Control.ModifierKeys & Keys.Shift) != 0 && e.Button == MouseButtons.Left)
            {
                //gripset = GetGripSelection(false);
                vdSelection gripset = GetGripSelection(false);

                if (gripset == null) return;
                gPoint pt = this.vdScrollableControl1.BaseControl.ActiveDocument.CCS_CursorPos();
                //activeDragDropControl = this.vdScrollableControl1;
                int result = this.vdScrollableControl1.BaseControl.ActiveDocument.CommandAction.DoDragDrop(gripset, vdSelection.DragDropEffects.All, pt, 100, 100, 3);
                //activeDragDropControl = null;
                return;
            }
            if (e.Button != MouseButtons.Right) return;
            if ((System.Windows.Forms.Control.ModifierKeys & Keys.Control) != 0) return;

            if (vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.OverAllActiveActions.Count != 1) return;

            if ((System.Windows.Forms.Control.ModifierKeys & Keys.Shift) != 0)
            {
                //gripset = GetGripSelection(false);
                vdSelection gripset = GetGripSelection(false);
                if (gripset == null) return;
                gPoint pt = this.vdScrollableControl1.BaseControl.ActiveDocument.CCS_CursorPos();
                //activeDragDropControl = this.vdScrollableControl1;
                int result = this.vdScrollableControl1.BaseControl.ActiveDocument.CommandAction.DoDragDrop(gripset, vdSelection.DragDropEffects.All, pt, 100, 100, 3);
                //activeDragDropControl = null;
            }
            else
            {
                //if (BaseAction.PressedKeyCode != Keys.None) return; //6011 correction
                //MainForm parent = this.MdiParent as MainForm;
                //parent.commandLine.PostExecuteCommand("");
            }
        }
        private vdSelection GetGripSelection(bool Create)
        {
            VectorDraw.Professional.vdCollections.vdSelection gripset;

            vdDocument VD = VDoc;

            //if (tc4.SelectedIndex == 1)
            //    VD = defDoc;

            VD = ActiveDoc;


            string selsetname = "VDGRIPSET_" + VD.ActiveLayOut.Handle.ToStringValue() + (VD.ActiveLayOut.ActiveViewPort != null ? VD.ActiveLayOut.ActiveViewPort.Handle.ToStringValue() : "");
            gripset = VD.ActiveLayOut.Document.Selections.FindName(selsetname);
            if (Create)
            {
                if (gripset == null)
                {
                    gripset = VD.ActiveLayOut.Document.Selections.Add(selsetname);
                }
            }
            //vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Document.Selections.FindName(selsetname).RemoveAll();
            return gripset;
        }
        eWorkingMode Work = eWorkingMode.Structure;

        void BaseControl_vdMouseUp(MouseEventArgs e, ref bool cancel)
        {
            try
            {
                vdSelection gripset = GetGripSelection(false);

                //if (gripset.Count > 1) return;
                vd3DFace dFace;
                //vdPolyline dFace;
                foreach (vdFigure fig in gripset)
                {
                    switch (Work)
                    {
                        case eWorkingMode.Loading:

                            vdLine ln = fig as vdLine;

                            if (ln == null)
                            {
                                dFace = fig as vd3DFace;
                                ShowElementOnGrid(dFace);
                                this.vdScrollableControl1.BaseControl.ActiveDocument.Redraw(true);
                            }
                            else
                            {
                                //this.MemberLoadcase.ShowMember(ln);
                                this.vdScrollableControl1.BaseControl.ActiveDocument.Redraw(true);
                            }
                            break;
                        case eWorkingMode.Structure:


                            if (fig is vdText)
                            {
                                ShowMemberOnGrid(fig as vdText);
                                this.ActiveDoc.Redraw(true);
                                //fig.se
                                break;
                            }
                            if (fig is vdPolyline)
                            {
                                ShowElementOnGrid(fig as vdPolyline);
                                this.ActiveDoc.Redraw(true);
                                //fig.se
                                break;
                            }

                            if (fig is ASTRASupportFixed)
                            {
                                ASTRASupportFixed asf = fig as ASTRASupportFixed;

                                ShowNodeOnGrid(asf.Origin);
                                this.ActiveDoc.Redraw(true);
                                //fig.se
                                break;
                            }

                            if (fig is ASTRASupportPinned)
                            {
                                ASTRASupportPinned asp = fig as ASTRASupportPinned;

                                ShowNodeOnGrid(asp.Origin);
                                this.ActiveDoc.Redraw(true);
                                //fig.se
                                break;
                            }


                            ln = fig as vdLine;

                            if ((System.Windows.Forms.Control.ModifierKeys & Keys.Shift) != 0 && e.Button == MouseButtons.Left)
                            {
                            }
                            if ((System.Windows.Forms.Control.ModifierKeys & Keys.Control) != 0 && e.Button == MouseButtons.Left)
                            {
                            }
                            if (ln == null)
                            {
                                dFace = fig as vd3DFace;
                                ShowElementOnGrid(dFace);
                                this.ActiveDoc.Redraw(true);
                            }
                            else
                            {
                                ShowMemberOnGrid(ln);
                                //ln.PenColor = new vdColor(Color.Red);
                                this.ActiveDoc.Redraw(true);
                            }
                            //fig.ShowGrips = false;
                            break;
                        case eWorkingMode.PostProcess:
                            ln = fig as vdLine;
                            if (ln == null)
                            {
                                dFace = fig as vd3DFace;

                                //this.PostProcess.ShowMemberOnForm(dFace);
                                this.ActiveDoc.Redraw(true);
                            }
                            else
                            {
                                //this.PostProcess.ShowMemberOnForm(ln);
                                //ln.PenColor = new vdColor(Color.Red);
                                this.ActiveDoc.Redraw(true);
                            }
                            //fig.ShowGrips = false;
                            break;
                    }
                }

                //for (int i = 1; i < gripset.Count; i++)
                //{
                //    gripset.RemoveAt(i);
                //    i = 0;
                //}
                gripset.RemoveAll();

            }
            catch (Exception exx) { }
        }


        #endregion Cad Methods

        #region Joint Grid Methods
        public void SetGridWithNode()
        {
            string kStr = "";
            int indx = -1;
            dgv_joints.Rows.Clear();

            for (int i = 0; i < AST_DOC.Joints.Count; i++)
            {
                indx = AST_DOC.Supports.IndexOf(AST_DOC.Joints[i].NodeNo);
                kStr = "";
                if (indx != -1)
                {
                    kStr = AST_DOC.Supports[indx].Option.ToString();
                }

                dgv_joints.Rows.Add(AST_DOC.Joints[i].NodeNo,
                    AST_DOC.Joints[i].Point.x.ToString("f4"),
                    AST_DOC.Joints[i].Point.y.ToString("f4"),
                    AST_DOC.Joints[i].Point.z.ToString("f4"), kStr);
            }
        }
        private void dgv_joints_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            gPoint joint = new gPoint();
            joint.x = double.Parse(dgv_joints[1, e.RowIndex].Value.ToString());
            joint.y = double.Parse(dgv_joints[2, e.RowIndex].Value.ToString());
            joint.z = double.Parse(dgv_joints[3, e.RowIndex].Value.ToString());

            VectorDraw.Professional.vdPrimaries.vdFigure fg = null;
            if (lastId != -1)
            {
                for (int i = VDoc.ActiveLayOut.Entities.Count - 1; i >= 0; i--)
                {
                    fg = VDoc.ActiveLayOut.Entities[i];
                    if (fg.Id == lastId)
                    {
                        VDoc.ActiveLayOut.Entities.RemoveAt(i);
                        break;
                    }
                }
            }
            VDoc.CommandAction.CmdSphere(joint, 0.039, 10, 10);

            lastId = VDoc.ActiveLayOut.Entities.Count - 1;
            fg = VDoc.ActiveLayOut.Entities[lastId];
            lastId = fg.Id;
            fg.PenColor = new vdColor(Color.DarkViolet);

            VDoc.Redraw(true);

        }
        #endregion

        #region Member Grid Methods
        private void SetGridWithMember()
        {
            dgvMemberGrid.Rows.Clear();

            foreach (var item in AST_DOC.Members)
            {
                try
                {
                    dgvMemberGrid.Rows.Add(item.MemberNo, item.MemberType,
                        item.StartNode.NodeNo,
                        item.EndNode.NodeNo,
                        item.Property.YD,
                        item.Property.ZD,
                        item.Property.Area,
                        item.Property.IX,
                        item.Property.IY,
                        item.Property.IZ,
                        item.Property.E,
                        item.Property.DEN,
                        item.Property.PR,
                        item.Length.ToString("f3")
                        );
                }
                catch (Exception ex) 
                {
                }
            }
        }
        private void SetGridWithElement()
        {
            dgvElementGrid.Rows.Clear();
            foreach (var item in AST_DOC.Elements)
            {
                try
                {
                    dgvElementGrid.Rows.Add(item.ElementNo, "PLATE",
                        item.Node1.NodeNo,
                        item.Node2.NodeNo,
                        item.Node3.NodeNo,
                        item.Node4.NodeNo,
                        item.ThickNess.Replace("TH: ","")
                        );
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void dgvElementGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //dgvMemberGrid.CurrentCell = dgvMemberGrid.A
                //if (tssl_zoom.Text == "Zoom On")
                AST_DOC.Elements.ShowElement(e.RowIndex, VDoc, 0.093d);
            }
            catch (Exception ex) { }
        }
        private void dgvMemberGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //dgvMemberGrid.CurrentCell = dgvMemberGrid.A
                //if (tssl_zoom.Text == "Zoom On")
                AST_DOC.Members.ShowMember(e.RowIndex, VDoc);
            }
            catch (Exception ex) { }
        }
        public void ShowMemberOnGrid(vdLine vLine)
        {

            for (int i = 0; i < AST_DOC.Members.Count; i++)
            {
                if (AST_DOC.Members[i].StartNode.Point == vLine.StartPoint &&
                    AST_DOC.Members[i].EndNode.Point == vLine.EndPoint)
                {

                    DataGridView dgv = dgvMemberGrid;
                    if (tc1.SelectedTab == tab_max_force)
                    {
                        dgv = dgv_max_frc;
                    }

                    vdDocument VD = ActiveDoc;
                    //if (tc4.SelectedIndex == 1)
                    //    VD = defDoc;
                    
                    for (int j = 0; j < dgv.Rows.Count; j++)
                    {
                        int memNo = (int)dgv[0, j].Value;

                        if (memNo == AST_DOC.Members[i].MemberNo)
                        {
                            ClearSelect(dgv);
                            dgv.Rows[j].Selected = true;
                            dgv.FirstDisplayedScrollingRowIndex = j;
                            AST_DOC.Members.ShowMember(j, VD, 0.03d);
                            tc3.SelectedTab = tab2_members;
                            return;
                        }
                    }
                }
            }

            if (AST_DOC_ORG != null)
            {
                for (int i = 0; i < AST_DOC_ORG.Members.Count; i++)
                {
                    if (AST_DOC_ORG.Members[i].StartNode.Point == vLine.StartPoint &&
                        AST_DOC_ORG.Members[i].EndNode.Point == vLine.EndPoint)
                    {

                        DataGridView dgv = dgvMemberGrid;
                        if (tc1.SelectedTab == tab_max_force)
                        {
                            dgv = dgv_max_frc;
                        }

                        vdDocument VD = ActiveDoc;
                        //if (tc4.SelectedIndex == 1)
                        //    VD = defDoc;

                        for (int j = 0; j < dgv.Rows.Count; j++)
                        {
                            int memNo = (int)dgv[0, j].Value;

                            if (memNo == AST_DOC.Members[i].MemberNo)
                            {
                                ClearSelect(dgv);
                                dgv.Rows[j].Selected = true;
                                dgv.FirstDisplayedScrollingRowIndex = j;
                                AST_DOC_ORG.Members.ShowMember(j, VD, 0.03d);
                                tc3.SelectedTab = tab2_members;
                                return;
                            }
                        }
                    }
                }
            }
        }
        public void ShowNodeOnGrid(vdText vtxt)
        {

            if (!vtxt.Layer.Name.ToUpper().StartsWith("NODE")) return;
            int jNo = MyStrings.StringToInt(vtxt.TextString, -1);
            if (jNo == -1) return;

            int jntNo = 0;


            for (int j = 0; j < dgv_joints.Rows.Count; j++)
            {
                jntNo = (int)dgv_joints[0, j].Value;

                if (jntNo == jNo)
                {
                    ClearSelect(dgv_joints);
                    dgv_joints.Rows[j].Selected = true;
                    dgv_joints.FirstDisplayedScrollingRowIndex = j;
                    //ShowJ(j, VDoc, 0.03d);
                    //vtxt.
                    tc3.SelectedTab = tab2_joints;
                    return;
                }
            }
        }
        public void ShowNodeOnGrid(gPoint gp)
        {

            int jNo = 0;
            int jntNo = 0;

            for (int i = 0; i < astDoc.Joints.Count; i++)
            {
                if (astDoc.Joints[i].Point == gp)
                    jNo = astDoc.Joints[i].NodeNo;
            }

            for (int j = 0; j < dgv_joints.Rows.Count; j++)
            {
                jntNo = (int)dgv_joints[0, j].Value;

                if (jntNo == jNo)
                {
                    ClearSelect(dgv_joints);
                    dgv_joints.Rows[j].Selected = true;
                    dgv_joints.FirstDisplayedScrollingRowIndex = j;
                    //ShowJ(j, VDoc, 0.03d);
                    //vtxt.
                    tc3.SelectedTab = tab2_joints;
                    return;
                }
            }
        }

        public void ShowMemberOnGrid(vdText vtxt)
        {
            if (vtxt.Layer.Name.ToUpper().StartsWith("NODE"))
                ShowNodeOnGrid(vtxt);
                           
            if (!vtxt.Layer.Name.ToUpper().StartsWith("MEMBER")) return;
            int mNo = MyStrings.StringToInt(vtxt.TextString, -1);
            if (mNo == -1) return;


            DataGridView dgv = dgvMemberGrid;
            if (tc1.SelectedTab == tab_max_force)
            {
                     dgv = dgv_max_frc;
            }

            vdDocument VD = VDoc;
            if (tc4.SelectedIndex == 1)
                VD = defDoc;
                    
            for (int j = 0; j < dgv.Rows.Count; j++)
            {
                int memNo = (int)dgv[0, j].Value;

                if (memNo == mNo)
                {
                    ClearSelect(dgv);
                    dgv.Rows[j].Selected = true;
                    dgv.FirstDisplayedScrollingRowIndex = j;
                    astDoc.Members.ShowMember(j, VD, 0.03d);
                    //vtxt.
                    tc3.SelectedTab = tab2_members;
                    return;
                }
            }
        }
     
        public void ShowElementOnGrid(vd3DFace _3dFace)
        {

            int elmtNo = 0;
            string ss = "";

            for (int i = 0; i < astDoc.Elements.Count; i++)
            {
                if (AST_DOC.Elements[i].Node1.Point == _3dFace.VertexList[0] &&
                    astDoc.Elements[i].Node2.Point == _3dFace.VertexList[1] &&
                    astDoc.Elements[i].Node3.Point == _3dFace.VertexList[2] &&
                    astDoc.Elements[i].Node4.Point == _3dFace.VertexList[3])
                {
                    for (int j = 0; j < dgvMemberGrid.RowCount; j++)
                    {
                        elmtNo = (int)dgvElementGrid[0, j].Value;
                        ss = dgvElementGrid[1, j].Value.ToString().Trim().TrimEnd().TrimStart();
                        if (elmtNo == astDoc.Elements[i].ElementNo &&
                            ss == "PLATE")
                        {
                            ClearSelect(dgvElementGrid);
                            tc3.SelectedTab = tab2_elements;

                            dgvElementGrid.Rows[j].Selected = true;
                            dgvElementGrid.FirstDisplayedScrollingRowIndex = j;
                            AST_DOC.Elements.ShowElement(j, VDoc, 0.09d);
                            return;
                        }

                    }
                }
            }


        }

        public void ShowElementOnGrid(vdPolyline _pline)
        {
            int elmtNo = 0;
            string ss = "";

            for (int i = 0; i < astDoc.Elements.Count; i++)
            {
                if (AST_DOC.Elements[i].Node1.Point == _pline.VertexList[0] &&
                    astDoc.Elements[i].Node2.Point == _pline.VertexList[1] &&
                    astDoc.Elements[i].Node3.Point == _pline.VertexList[2] &&
                    astDoc.Elements[i].Node4.Point == _pline.VertexList[3])
                {
                    for (int j = 0; j < dgvMemberGrid.RowCount; j++)
                    {
                        elmtNo = (int)dgvElementGrid[0, j].Value;
                        ss = dgvElementGrid[1, j].Value.ToString().Trim().TrimEnd().TrimStart();
                        if (elmtNo == astDoc.Elements[i].ElementNo &&
                            ss == "PLATE")
                        {
                            ClearSelect(dgvElementGrid);
                            tc3.SelectedTab = tab2_elements;

                            dgvElementGrid.Rows[j].Selected = true;
                            dgvElementGrid.FirstDisplayedScrollingRowIndex = j;
                            AST_DOC.Elements.ShowElement(j, VDoc, 0.09d);
                            return;
                        }

                    }
                }
            }

        }

        public void ClearSelect(DataGridView dgv)
        {
            for (int i = 0; i < dgv.RowCount; i++)
            {
                dgv.Rows[i].Selected = false;
            }
        }
        public void ClearSelect()
        {
            ClearSelect(dgvMemberGrid);
        }
        public void SetTextSize(double txtSize)
        {
            vdDocument VD = VDoc;

            //if (tc4.SelectedIndex == 1) VD = defDoc;
            //if (tc4.SelectedIndex == 2) VD = maxDoc;
            //if (tc4.SelectedIndex == 3) VD = envDoc;



            if (tc4.SelectedTab == tab_org_doc) VD = VDoc;
            else if (tc4.SelectedTab == tab_defl_doc) VD = defDoc;
            else if (tc4.SelectedTab == tab_mov_doc) VD = movDoc;
            else if (tc4.SelectedTab == tab_max_doc) VD = maxDoc;
            else if (tc4.SelectedTab == tab_evlp_doc) VD = envDoc;


            for (int i = 0; i < VD.ActiveLayOut.Entities.Count; i++)
            {
                vdText txt = VD.ActiveLayOut.Entities[i] as vdText;
                if (txt != null)
                {
                    txt.Height = txtSize;
                    txt.Update();
                }
                else
                {

                    vdMText mtxt = VD.ActiveLayOut.Entities[i] as vdMText;
                    if (mtxt != null)
                    {
                        mtxt.Height = txtSize;
                        mtxt.Update();
                    }
                }
            }
            VD.Redraw(true);
        }
        #endregion

        #region Loading
        private void Set_Loadings()
        {

            TreeNode tn = null;
            TreeNode tjn = null;
            tv_loads.Nodes.Clear();
            foreach (var item in astDoc.LoadDefines)
            {
                tv_loads.Nodes.Add("LOAD " + item.LoadCase + " : " + item.LoadTitle);

                tn = tv_loads.Nodes[tv_loads.Nodes.Count - 1];


                if (item.Selfweight != "")
                {
                    tn.Nodes.Add(item.Selfweight);
                    //tn = tn.Nodes[tn.Nodes.Count - 1];
                    //foreach (var item1 in item.Combinations)
                    //{
                    //    tn.Nodes.Add(item1.ToString());
                    //}
                    //tn = tn.Parent;
                }
                if (item.SupportDisplacements.Count > 0)
                {
                    tn.Nodes.Add("SUPPORT DISPACEMENT LOAD");
                    tn = tn.Nodes[tn.Nodes.Count - 1];
                    foreach (var item1 in item.SupportDisplacements)
                    {
                        tn.Nodes.Add(item1.ToString());
                    }
                    tn = tn.Parent;
                }
                if (item.RepeatLoadList.Count > 0)
                {
                    item.Set_Combination();
                    tn.Nodes.Add("REPEAT LOAD");
                    tn = tn.Nodes[tn.Nodes.Count - 1];
                    foreach (var item1 in item.Combinations)
                    {
                        tn.Nodes.Add(item1.ToString());
                    }
                    tn = tn.Parent;
                }
                if (item.MemberLoadList.Count > 0)
                {
                    tn.Nodes.Add("MEMBER LOAD");
                    tn = tn.Nodes[tn.Nodes.Count - 1];
                    foreach (var item1 in item.MemberLoadList)
                    {
                        tn.Nodes.Add(item1 + " " + astDoc.Base_MassUnit + "/" + astDoc.Base_LengthUnit);
                    }
                    tn = tn.Parent;

                }
                if (item.JointLoadList.Count > 0)
                {
                    
                    tn.Nodes.Add("JOINT LOAD");
                    tn = tn.Nodes[tn.Nodes.Count - 1];
                    foreach (var item1 in item.JointLoadList)
                    {
                        tn.Nodes.Add(item1 + " " + astDoc.Base_MassUnit);
                    }
                    tn = tn.Parent;
                }
                if (item.AreaLoadList.Count > 0)
                {

                    tn.Nodes.Add("AREA LOAD");
                    tn = tn.Nodes[tn.Nodes.Count - 1];
                    foreach (var item1 in item.AreaLoadList)
                    {
                        tn.Nodes.Add(item1 + " " + astDoc.Base_MassUnit + "/Sq." + astDoc.Base_LengthUnit);
                    }
                    tn = tn.Parent;
                }
                if (item.TempLoadList.Count > 0)
                {

                    tn.Nodes.Add("TEMP LOAD");
                    tn = tn.Nodes[tn.Nodes.Count - 1];
                    foreach (var item1 in item.TempLoadList)
                    {
                        tn.Nodes.Add(item1);
                    }
                    tn = tn.Parent;
                }
                if (item.CominationLoadList.Count > 0)
                {
                    item.Set_Combination();
                    tn.Nodes.Add("COMBINATION");
                    if (tn.Nodes.Count > 0)
                        tn = tn.Nodes[tn.Nodes.Count - 1];
                    foreach (var item1 in item.Combinations)
                    {
                        tn.Nodes.Add(item1.ToString());
                    }
                    tn = tn.Parent;
                }
                if (item.ElementLoadList.Count > 0)
                {
                    tn.Nodes.Add("ELEMENT LOAD");
                    tn = tn.Nodes[0];
                    foreach (var item1 in item.ElementLoadList)
                    {
                        //tn.Nodes.Add(item1 + " " + astDoc.MassUnit + "/" + astDoc.LengthUnit);
                        tn.Nodes.Add(item1);
                    }
                    tn = tn.Parent;
                }
            }
            //End
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string kStr = "";

            TreeNode tn = tv_loads.SelectedNode;

            bool IsJointLoad = false;

            if (tn.Nodes.Count == 0)
            {
                IsJointLoad = (tn.Parent.Text.StartsWith("JOINT"));
                kStr = tn.Text;
            }

            AST_DOC.JointLoads.Delete_ASTRAArrowLine(VDoc);
            AST_DOC.MemberLoads.Delete_ASTRAMemberLoad(VDoc);

            if (tn.Parent != null)
            {
                tn = tn.Parent;
                if (tv_loads.SelectedNode.Text.StartsWith("JOINT"))
                {
                    astDoc.JointLoads.DrawJointLoads(VDoc, tn.Index + 1); goto _100;
                    //cmbLoadCase.SelectedIndex = tn.Index;
                }
                else if (tv_loads.SelectedNode.Text.StartsWith("MEMBER"))
                {
                    astDoc.MemberLoads.DrawMemberLoad(VDoc, tn.Index + 1); goto _100;
                    //cmbLoadCase.SelectedIndex = tn.Index;
                }
                if (tn.Text.StartsWith("COMB"))
                {
                    MyStrings mlist = new MyStrings(kStr, ' ');
                    AST_DOC.MemberLoads.DrawMemberLoad(VDoc, mlist.GetInt(3));
                    AST_DOC.JointLoads.DrawJointLoads(VDoc, mlist.GetInt(3)); goto _100;
                    //astDoc.MemberLoads.DrawC(Maindoc, tn.Index + 1); goto _100;
                    //cmbLoadCase.SelectedIndex = tn.Index;
                }
                if (tn.Text.StartsWith("REPEAT"))
                {
                    MyStrings mlist = new MyStrings(kStr, ' ');
                    AST_DOC.MemberLoads.DrawMemberLoad(VDoc, mlist.GetInt(3));
                    AST_DOC.JointLoads.DrawJointLoads(VDoc, mlist.GetInt(3)); goto _100;
                    //astDoc.MemberLoads.DrawC(Maindoc, tn.Index + 1); goto _100;
                    //cmbLoadCase.SelectedIndex = tn.Index;
                }
            }
            else
            {
                AST_DOC.JointLoads.DrawJointLoads(VDoc, tn.Index + 1);
                AST_DOC.MemberLoads.DrawMemberLoad(VDoc, tn.Index + 1);
                goto _100;
            }

            if (tn.Parent != null)
                tn = tn.Parent;
            if (tn.Parent != null)
                tn = tn.Parent;
            if (tn.Parent != null)
                tn = tn.Parent;
            if (tn.Parent != null)
                tn = tn.Parent;

            //int MemNo = MemberNo;
            //int mIndx = astDoc.Members.IndexOf(MemNo);

            //astDoc.Supports.DrawSupport(Maindoc);




            if (IsJointLoad)
            {
                //AST_DOC.JointLoads.DrawJointLoads(VDoc, tn.Index + 1, kStr);
                JointLoadCollection jlc = new JointLoadCollection();
                jlc.AddTXT(kStr, tn.Index + 1);


                for (int i = 0; i < jlc.Count; i++)
                {

                    try
                    {
                        jlc[i].Joint = AST_DOC.Joints[jlc[i].Joint.NodeNo - 1];
                    }
                    catch (Exception exx)
                    {
                        jlc.RemoveAt(i); i--;
                    }
                }

                jlc.DrawJointLoads(VDoc, tn.Index + 1);
            }
            else
            {
                MemberLoadCollection mlc = new MemberLoadCollection();
                mlc.AddTxt(kStr, tn.Index + 1);

                for (int i = 0; i < mlc.Count; i++)
                {
                    try
                    {
                        mlc[i].Member = AST_DOC.Members[mlc[i].Member.MemberNo - 1];
                    }
                    catch (Exception exx) 
                    {
                        mlc.RemoveAt(i); i--;
                    }
                }
                //AST_DOC.LoadDefines
                mlc.DrawMemberLoad(VDoc, tn.Index + 1);




                //AST_DOC.MemberLoads.DrawMemberLoad(VDoc, tn.Index + 1, kStr);
            }


        _100:
            if (tn.Index < cmbLoadCase.Items.Count)
                cmbLoadCase.SelectedIndex = tn.Index;
            VDoc.Redraw(true);
        }

        #endregion


        public bool Check_Coordinate(int JointNo, int MemberNo)
        {
            List<int> ListJoints = new List<int>();

            #region ListJoints
            ListJoints.Add(16);
            ListJoints.Add(16);
            ListJoints.Add(22);
            ListJoints.Add(16);
            ListJoints.Add(21);
            ListJoints.Add(15);
            ListJoints.Add(12);
            ListJoints.Add(79);
            ListJoints.Add(24);
            ListJoints.Add(16);
            ListJoints.Add(16);
            ListJoints.Add(16);
            ListJoints.Add(121);
            ListJoints.Add(121);
            ListJoints.Add(102);
            ListJoints.Add(1261);
            ListJoints.Add(153);
            ListJoints.Add(24);
            ListJoints.Add(392);
            ListJoints.Add(544);
            ListJoints.Add(166);
            #endregion ListJoints

            List<int> ListMembers = new List<int>();

            #region ListMembers
            ListMembers.Add(21);
            ListMembers.Add(21);
            ListMembers.Add(47);
            ListMembers.Add(23);
            ListMembers.Add(28);
            ListMembers.Add(21);
            ListMembers.Add(11);

            ListMembers.Add(78);
            ListMembers.Add(24);
            ListMembers.Add(21);
            ListMembers.Add(21);
            ListMembers.Add(21);
            ListMembers.Add(220);
            ListMembers.Add(220);

            ListMembers.Add(248);
            ListMembers.Add(2374);
            ListMembers.Add(280);
            ListMembers.Add(24);
            ListMembers.Add(899);
            ListMembers.Add(362);
            ListMembers.Add(0);
            #endregion ListMembers

            for (int i = 0; i < ListJoints.Count; i++)
            {
                if (ListJoints[i] == JointNo && ListMembers[i] == MemberNo)
                    return true;
            }
            return false;
        }

        public bool Check_Demo_Version()
        {
            //if (HeadsUtils.Constants.BuildType == HeadsUtils.eHEADS_RELEASE_TYPE.DEMO)
            if (HASP_Lock.IsDemoVersion())
            {
                //ASTRA USB Lock not found at any port.\n\nThis is Demo Version of ASTRA Pro
                //string str = "ASTRA USB Lock not found at any port.\n\nThis is Demo Version of ASTRA Pro. This will process only the default Data shown as sample.\n\n";
                //str += "For Professional Version of ASTRA Pro please contact : \n\nTechSOFT Engineering Services\n\n";
                //str += "Website : http://www.techsoftglobal.com, http://www.headsview.com\n\n";
                //str += "Email at: techsoft@consultant.com, dataflow@mail.com\n";

                //Chiranjit [2012 11 13]
                if (!Check_Coordinate(dgv_joints.RowCount, (dgvMemberGrid.RowCount)))
                {
                    //Chiranjit [2012 11 13]
                    string str = "This Facility is restricted in the Demo Version of ASTRA Pro.\n\nASTRA Pro USB Dongle not found at any port.\n\n";

                    str += "This Version can Process the Analysis Input Data from the Example Only. User has to select the input data file by using the menu option\n";
                    str += "\n \"File>>Open Analysis Example Data File\", and next can Process the Analysis file by using the menu option ";
                    str += "\"Process Analysis>>Analysis by Example Data File\" for Text Data, SAP Data and Drawings Files.\n\n";


                    str += "For Professional Version of ASTRA Pro please contact : \n\n";
                    str += "Email at : techsoft@consultant.com, dataflow@mail.com\n\n";
                    str += "Website : http://www.techsoftglobal.com, http://www.headsview.com\n\n";
                    str += "Tel. No  : +91 33 4008 3349,  +91 33 6526 1190\n\n";
                    str += "\nTechSOFT Engineering Services\n\n";
                    //MessageBox.Show(this, str, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //MessageBox.Show(str, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);


                    //Chiranjit [2014 12 10]
                    frm_Demo_Version.DemoDialog();
                    return true;
                }
            }

            return false;
        }

        //Chiranjit [2016 09 22] Disable Analysis
        public bool Is_Analysis
        {
            get
            {
                return btn_process_analysis.Text.StartsWith("Pro");
            }
            set
            {
                if (value)
                    btn_process_analysis.Text = "Process Analysis";
                else
                    btn_process_analysis.Text = "View Analysis Input Data";
            }
        }
        public string Process_Caption
        {
            get
            {
                return lbl_process_caption.Text;
            }
            set
            {
                lbl_process_caption.Text = value;
            }
        }
        
        private void btn_process_analysis_Click(object sender, EventArgs e)
        {
            if (Is_Analysis)
            {
                if (Check_Demo_Version()) return;
                RunAnalysis();
                if (File.Exists(Analysis_File_Name))
                {
                    rtb_ana_rep.Lines = File.ReadAllLines(Analysis_File_Name);
                    StructureAnalysis = null;
                    ld = null;
                }
            }
            else
            {
                System.Diagnostics.Process.Start(File_Name);
            }

        }
        public void Delete_Temporary_Files(string folder_path)
        {
            string tst = folder_path;

            if (File.Exists(tst))
                folder_path = Path.GetDirectoryName(tst);

            List<string> files = new List<string>();
            try
            {
                files.AddRange(Directory.GetFiles(folder_path));
            }
            catch (Exception ex) { }

            foreach (var item in files)
            {
                if (Path.GetExtension(item.ToLower()) == ".fil" ||
                    Path.GetExtension(item.ToLower()) == ".tmp")
                {
                    try
                    {
                        File.Delete(item);
                    }
                    catch (Exception ex) { }
                }
            }
        }

        public void RunAnalysis()
        {
            string patFile = Path.Combine(Application.StartupPath, "PAT001.tmp");

            string fName = astDoc.FileName;
            //Delete_FIL_Files(Path.GetDirectoryName(fName));
            Delete_Temporary_Files(fName);

            System.Environment.SetEnvironmentVariable("SURVEY", fName);
            System.Environment.SetEnvironmentVariable("ASTRA", fName);

            File.WriteAllText(patFile, Path.GetDirectoryName(fName) + "\\");

            string runExe = "", ext = "";
            ext = Path.GetExtension(fName).ToLower();
            if (ext == ".txt")
            {
                runExe = Path.Combine(Application.StartupPath, "AST001.exe");

                if (File.Exists(runExe) == false)
                {
                    runExe = Path.Combine(Application.StartupPath, "AST004.exe");
                }
            }
            else if (ext == ".ast")
                runExe = Path.Combine(Application.StartupPath, "AST003.exe");
            try
            {
                System.Diagnostics.Process.Start(runExe).WaitForExit();
            }
            catch (Exception exx)
            {
            }
        }

        private void btn_open_ana_rep_Click(object sender, EventArgs e)
        {
            string open_file = "";

            Button btn = sender as Button;

            if (btn.Name == btn_open_ana_rep.Name)
            {
                open_file = Path.Combine(Path.GetDirectoryName(AST_DOC.FileName), "ANALYSIS_REP.TXT");
            }

            if (File.Exists(open_file))
                System.Diagnostics.Process.Start(open_file);
        }

        private void chk_joints_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = sender as CheckBox;

                //VDoc.Layers.FindName(chk.Name.Substring(3)).Frozen = !chk.Checked;

                vdDocument VD = VDoc;

                if (tc4.SelectedIndex == 1)
                    VD = defDoc;


                for (int i = 0; i < VD.Layers.Count;i++)
                {
                    if (VD.Layers[i].Name.ToLower().StartsWith(chk.Text.Substring(0, 4).ToLower()))
                    {
                        VD.Layers[i].Frozen = !chk.Checked;
                    }
                    //if (VD.Layers[i].Name.StartsWith("Ele"))
                    //{
                    //    VD.Layers[i].Frozen = !chk_mems.Checked;
                    //}
                }
                VD.Redraw(true);
            }
            catch (Exception ex)
            {
            }

        }
        public double Text_Size
        {
            get
            {
                return (cmb_text_size.SelectedIndex + 1) * 0.1;
            }
        }

        private void cmb_text_size_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //double d = double.Parse(cmb_text_size.Text);
                SetTextSize(Text_Size);
            }
            catch (Exception ex)
            { }
        }

        void Show_Panel(TabPage tp)
        {
            tc_pp_panel.TabPages.Clear();
            tc_pp_panel.TabPages.Add(tp);
        }


        private void tc1_SelectedIndexChanged(object sender, EventArgs e)
        {

            //sc3.Panel2Collapsed = (tc6.SelectedTab == tab_file_open);

            if (tc4.TabPages.Count > 2)
            {
                tc4.TabPages.Remove(tab_evlp_doc);
            }


            tc_pp_panel.TabPages.Clear();
            if (tc1.SelectedTab == tab_forces)
                Show_Panel(tab1_forces);
            else if (tc1.SelectedTab == tab_load_deflection)
            {
                Show_Panel(tab1_load_deflection);
                tc4.SelectedTab = tab_defl_doc;
            }
            else if (tc1.SelectedTab == tab_max_force)
            {

                tc4.SelectedTab = tab_max_doc;
                Show_Panel(tab1_max_force);

                if (maxDoc != null)
                {
                    maxDoc.ActiveLayOut.Entities.RemoveAll();

                    if (maxDoc.ActiveLayOut.Entities.Count == 0)
                    {
                        if (AST_DOC_ORG != null)
                        {
                            AST_DOC_ORG.Members.DrawMember(maxDoc);
                            AST_DOC_ORG.Elements.DrawElements(maxDoc);
                            AST_DOC_ORG.Supports.DrawSupport(maxDoc);
                        }
                        else
                        {
                            AST_DOC.Members.DrawMember(maxDoc);
                            AST_DOC.Elements.DrawElements(maxDoc);
                            AST_DOC.Supports.DrawSupport(maxDoc);
              
                        }
                    }

                    maxDoc.Redraw(true);
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(maxDoc);
                }

            }
            else if (tc1.SelectedTab == tab_moving_load)
            {
                Show_Panel(tab1_moving_load);
                tc4.SelectedTab = tab_mov_doc;
            }
            else if (tc1.SelectedTab == tab_envelop)
            {
                Show_Panel(tab1_truss_env);
                tc4.TabPages.Add(tab_evlp_doc);
                tc4.SelectedTab = tab_evlp_doc;
            }

            //SetTextSize((cmb_text_size.SelectedIndex + 2) * 0.50);
            SetTextSize(Text_Size);

            //return;
            try
            {
                //tc2.SelectedIndex = tc1.SelectedIndex;
                Tab_Selection();
            }
            catch (Exception ex) { }
        }

        private void Tab_Selection()
        {

            if (tc1.SelectedTab == tab_load_deflection)
            {
                tc4.SelectedIndex = 1;
                if (ld == null || dgv_node_disp.RowCount == 0)
                {
                    AstrA_LOAD_Deflection();
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(defDoc);
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Vrot(defDoc);
                }
                Show_Panel(tab1_load_deflection);

            }
            else if (tc1.SelectedTab == tab_moving_load)
            {
                if (AST_DOC.IsMovingLoad)
                {
                    if (StructureAnalysis == null)
                    {
                        StructureAnalysis = new StructureMemberAnalysis(AST_DOC.AnalysisFileName);
                    }
                    tc4.SelectedTab = tab_mov_doc;
                    Set_Moving_Load_Init();
                    Show_Panel(tab1_moving_load);
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(movDoc);
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Vrot(movDoc);
                }
                else
                {
                    tc1.SelectedTab = tab_forces;
                    MessageBox.Show("Moving Load Data not found in the input file.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (tc1.SelectedTab == tab_forces ||
                tc1.SelectedTab == tab_max_force)
            {

                if (StructureAnalysis == null)
                {
                    StructureAnalysis = new StructureMemberAnalysis(AST_DOC.AnalysisFileName);

                    dgv_joint_frcs.Rows.Clear();
                    foreach (var item in StructureAnalysis.list_joints)
                    {
                        dgv_joint_frcs.Rows.Add(item.NodeNo, item.LoadNo, item.FX, item.FY, item.FZ, item.MX, item.MY, item.MZ);

                    }
                    dgv_beam_frcs.Rows.Clear();
                    foreach (var item in StructureAnalysis.list_beams)
                    {
                        dgv_beam_frcs.Rows.Add(StructureAnalysis.Get_User_Member_No(item.BeamNo, eAstraMemberType.BEAM), item.LoadNo,
                            item.StartNodeForce.JointNo,
                            item.StartNodeForce.R1_Axial,
                            item.StartNodeForce.R2_Shear,
                            item.StartNodeForce.R3_Shear,
                            item.StartNodeForce.M1_Torsion,
                            item.StartNodeForce.M2_Bending,
                            item.StartNodeForce.M3_Bending);

                        dgv_beam_frcs.Rows.Add("", "",
                            item.EndNodeForce.JointNo,
                            item.EndNodeForce.R1_Axial,
                            item.EndNodeForce.R2_Shear,
                            item.EndNodeForce.R3_Shear,
                            item.EndNodeForce.M1_Torsion,
                            item.EndNodeForce.M2_Bending,
                            item.EndNodeForce.M3_Bending);
                    }
                    dgv_truss_frcs.Rows.Clear();
                    foreach (var item in StructureAnalysis.list_trusses)
                    {
                        dgv_truss_frcs.Rows.Add(StructureAnalysis.Get_User_Member_No(item.TrussMemberNo, eAstraMemberType.TRUSS), item.LoadNo,
                            item.Stress,
                            item.Force);
                    }
                    foreach (var item in StructureAnalysis.list_cables)
                    {
                        dgv_truss_frcs.Rows.Add(StructureAnalysis.Get_User_Member_No(item.TrussMemberNo, eAstraMemberType.CABLE), item.LoadNo,
                            item.Stress,
                            item.Force);
                    }

                    dgv_max_frc.Rows.Clear();
                    foreach (var item in StructureAnalysis.list_analysis)
                    {
                        dgv_max_frc.Rows.Add(item.UserMemberNo, item.AstraMemberType,
                                    item.CompressionForce,
                                    item.TensileForce,
                            item.MaxAxialForce,
                            item.MaxTorsion,
                            item.MaxBendingMoment,
                            item.MaxShearForce);
                    }
                }
                if (tc1.SelectedTab == tab_max_force)
                    Show_Panel(tab1_max_force);
                else
                    Show_Panel(tab1_forces);

               
            }
            else if (tc1.SelectedTab == tab_envelop)
            {
                if (StructureAnalysis == null)
                    StructureAnalysis = new StructureMemberAnalysis(AST_DOC.AnalysisFileName);
                //tc4.SelectedIndex = 2;
            }
            else
                tc4.SelectedIndex = 0;
            this.Refresh();
        }

        private void tc2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (tc2.SelectedTab == tab1_moving_load)
            //{
            //    if (!AST_DOC.IsMovingLoad)
            //    {
            //        tc2.SelectedIndex = 0;
            //        tc1.SelectedIndex = 0;
            //        //MessageBox.Show("Moving Load Data not found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //        tc2.SelectedIndex = 0;
            //        tc1.SelectedIndex = 0;
            //    }
            //    else
            //        tc1.SelectedIndex = tc2.SelectedIndex;
            //}
            //else
            //if(tc2.SelectedTab != null)
            //tc2.SelectedTab.Text = "";
        }

        private void chkGlobal_CheckedChanged(object sender, EventArgs e)
        {
            ShowGlobalLocal();
        }
        private void ShowGlobalLocal()
        {
            vdDocument VD = VDoc;

            if (tc4.SelectedIndex == 1)
                VD = defDoc;

            try
            {
                VD.Layers.FindName("MemberLoadGlobal").Frozen = !chkGlobal.Checked;
                VD.Layers.FindName("MemberLoadLocal").Frozen = !chkLocal.Checked;
            }
            catch (Exception exx) { }
            {
            }

            try
            {

                VD.Layers.FindName("ConcentrateLocal").Frozen = !chkLocal.Checked;
                VD.Layers.FindName("ConcentrateGlobal").Frozen = !chkGlobal.Checked;
            }
            catch (Exception ex2) { }
            {
            }
            try
            {
                VD.Layers.FindName("JointLoad").Frozen = !chkJoint.Checked;
            }
            catch (Exception exx1) { }
            {
            }
            VD.Redraw(true);
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(VDoc);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Vrot(VDoc);
            //defDoc.Palette.Background = Color.White;
        }

        public void AstrA_LOAD_Deflection()
        {

            string analysisFileName = "";
            analysisFileName = Path.GetDirectoryName(astDoc.FileName);
            analysisFileName = astDoc.AnalysisFileName;

            if (!File.Exists(analysisFileName)) return;

            Set_Init();
            if (AST_DOC_ORG.IsDynamicLoad)
            {
                //this.Text = "EIGEN VALUE ANALYSIS";
                lblLoadCase.Text = "EIGEN VALUE";
            }
            else
            {
                //this.Text = "LOAD DEFLECTION ANALYSIS";
                lblLoadCase.Text = "LOAD CASE";
            }
            cmbInterval.SelectedIndex = 9;
            //if (AST_DOC_ORG.IsMovingLoad)
            //    AST_DOC_ORG.Draw_Main_MemberDetails(MainDoc, 1);

            cmbLoadCase.SelectedIndex = -1;
            cmbLoadCase.SelectedIndex = 0;

            string temp_file = "";

            temp_file = Path.Combine(Path.GetDirectoryName(astDoc.FileName), "rep.tmp");

            StreamWriter sw = new StreamWriter(new FileStream(temp_file, FileMode.Create));
            StreamReader sr = new StreamReader(new FileStream(analysisFileName, FileMode.Open));

            string str = "";
            string kstr = "";
            bool flag = false;

            bool truss_title = false;
            bool beam_title = false;
            bool cable_title = false;
            try
            {
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Taken from file ANALYSIS_REP.TXT");
                //sw.WriteLine("       Member   Load     Node    AXIAL       SHEAR       SHEAR     TORSION     BENDING     BENDING");
                //sw.WriteLine("       No:      Case     Nos:       R1          R2          R3          M1          M2          M3");
                sw.WriteLine();
                sw.WriteLine();
                while (!sr.EndOfStream)
                {
                    str = sr.ReadLine().ToUpper();
                    kstr = MovingLoadAnalysis.MyList.RemoveAllSpaces(str);

                    //str = str.Replace("\t\t", "\t    ");
                    if (str.Contains("TRUSS MEMBER ACTIONS"))
                    {
                        flag = true;
                        if (!truss_title)
                        {
                            sw.WriteLine("TRUSS MEMBER ACTIONS");
                            sw.WriteLine();
                            sw.WriteLine("    MEMBER      LOAD         STRESS              FORCE");
                            sw.WriteLine();
                            truss_title = true;
                        }
                        continue;
                    }
                    if (str.Contains("BEAM FORCES AND MOMENTS"))
                    {
                        flag = true;
                        if (!beam_title)
                        {
                            sw.WriteLine("BEAM FORCES AND MOMENTS");
                            sw.WriteLine();
                            sw.WriteLine(" BEAM LOAD     AXIAL       SHEAR       SHEAR     TORSION     BENDING     BENDING");
                            sw.WriteLine("  NO.  NO.        R1          R2          R3          M1          M2          M3");
                            sw.WriteLine();
                            beam_title = true;
                        }
                        continue;
                    }
                    if (str.Contains("CABLE MEMBER ACTIONS"))
                    {
                        flag = true;
                        if (!cable_title)
                        {
                            sw.WriteLine("CABLE MEMBER ACTIONS");
                            sw.WriteLine();
                            sw.WriteLine("    MEMBER      LOAD         STRESS              FORCE");
                            sw.WriteLine();
                            cable_title = true;
                        }
                        continue;
                    }
                    if (flag && !str.Contains("*") && str.Length > 9)
                    {
                        if ((kstr != "MEMBER LOAD STRESS FORCE") ||
                            (kstr != ".....BEAM FORCES AND MOMENTS"))
                            sw.WriteLine(str);
                    }
                }
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
                sr.Close();
            }

            return;
        }

        private void cmbLoadCase_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                iLoadCase = int.Parse(cmbLoadCase.Text);

                ShowData();

                if (AST_DOC.IsDynamicLoad)
                {
                    iLoadCase = 1;
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(VDoc);

                    VectorDraw.Professional.Memory.vdMemory.Collect();
                    GC.Collect();
                    VDoc.UndoHistory.PushEnable(false);
                    VDoc.CommandAction.Undo("BEGIN");

                    //mainDoc.UndoHistory.PushEnable(false);
                    //mainDoc.CommandAction.Undo("BEGIN");

                    doc.Redraw(true);
                    MainDoc.Redraw(true);

                    return;
                }
                //SetTextSize(Text_Size);
            }
            catch (Exception ex)
            {
                //ex = ex;
            }

            //if (AST_DOC_ORG.IsMovingLoad)
            //{
            //    try
            //    {
            //        GC.Collect();
            //        VectorDraw.Professional.Memory.vdMemory.Collect();


            //        MainDoc.UndoHistory.PushEnable(false);
            //        //doc.CommandAction.Undo("BEGIN");

            //        doc.UndoHistory.PushEnable(false);
            //        //mainDoc.CommandAction.Undo("BEGIN");

            //        AST_DOC_ORG.Draw_Main_MemberDetails(MainDoc, iLoadCase);
            //        AST_DOC.Load_Geneartion.Draw_Moving_Wheel(doc, 0.2, LoadCase, max_x, AST_DOC_ORG.Joints.BoundingBox.Bottom);
            //        AST_DOC.Load_Geneartion.Draw_Plan_Moving_Wheel(MainDoc, 0.2, LoadCase, max_x);
            //        VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VFront(MainDoc);
            //    }
            //    catch (Exception ex) { }
            //}
            //else
            //    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(MainDoc);

            doc.Redraw(true);
            MainDoc.Redraw(true);
        }
        public double Factor
        {
            get
            {
                return MyStrings.StringToDouble(txtDefFactor.Text, 1);
            }
        }
        private void ShowData()
        {

            VectorDraw.Professional.Memory.vdMemory.Collect();

            string sLoad = "LOAD_" + cmbLoadCase.Text;
            AST_DOC = (ASTRADoc)ld.HashDeflection[sLoad];
            //if (AST_DOC_ORG.IsMovingLoad == false)
            AST_DOC = ld.Get_ASTRADoc(LoadCase, Factor);

            if (AST_DOC != null)
            {

                defDoc.ActiveLayOut.Entities.EraseAll();
                AST_DOC.Joints.DrawJointsText(defDoc, Text_Size);
                AST_DOC.Members.DrawMember(defDoc, Text_Size);
                AST_DOC.Elements.DrawElements(defDoc);
                AST_DOC.Supports.DrawSupport(defDoc);

                //if (AST_DOC_ORG.IsMovingLoad == false)
                //    AST_DOC.Members.DrawMember(defDoc);
                if (AST_DOC.IsMovingLoad)
                {
                    //AST_DOC.JointLoads.DrawJointLoads_MovingLoad(doc, LoadCase);
                }
                else
                {
                   
                    AST_DOC_ORG.MemberLoads.Delete_ASTRAMemberLoad(MainDoc);
                    AST_DOC_ORG.MemberLoads.DrawMemberLoad(MainDoc, LoadCase);

                    AST_DOC_ORG.JointLoads.Delete_ASTRAArrowLine(MainDoc);
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
        public void Moving_Line()
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

        private void btnNext_Click(object sender, EventArgs e)
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

        private void ASTRALoadDeflections_Load()
        {
            //pbLoadDeflection.Visible = true;
         
        }

        private void tmrLoadDeflection_Tick(object sender, EventArgs e)
        {

            if (bIsNext)
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
            AST_DOC_ORG = new ASTRADoc(AST_DOC.FileName);
            if (ld == null)
                ld = new LoadDeflection(AST_DOC_ORG, pbLoadDeflection);



            NodeDisplacement nd = null;
            dgv_node_disp.Rows.Clear();

            double max_def = 0.0;
            int load_no = 0;
            int node_no = 0;

            for (int i = 0; i < ld.Node_Displacements.Count; i++)
            {
                nd = ld.Node_Displacements[i];
                dgv_node_disp.Rows.Add(nd.Node.NodeNo, nd.LoadCase, nd.Tx, nd.Ty, nd.Tz, nd.Rx, nd.Ry, nd.Rz);

                if (Math.Abs(nd.Ty) > Math.Abs(max_def))
                {
                    max_def = nd.Ty;
                    load_no = nd.LoadCase;
                    node_no = nd.Node.NodeNo;
                }
            }
            txt_max_deflection.Text = max_def.ToString();
            txt_max_deflection_load.Text = load_no.ToString();
            txt_max_deflection_node.Text = node_no.ToString();


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
                if (ld.Factor < 1)
                    txtDefFactor.Text = ld.Factor.ToString("0.000");
                else
                    txtDefFactor.Text = ld.Factor.ToString("0");
                cmbLoadCase.SelectedIndex = -1;
                cmbLoadCase.SelectedIndex = 0;
                //txtDefFactor.Enabled = !AST_DOC_ORG.IsMovingLoad;
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

        #region  Chiranjit [2014 04 10] New Moving load Animation
        List<MLoadData> load_data = null;
        List<VehicleLoad> list_vehicle = null;
        public void MovingLoad_Initialize(vdDocument vDoc, string file_name)
        {
            //InitializeComponent();
            if (movDoc != null)
            {
                AST_DOC_ORG.Members.DrawMember(movDoc);
                AST_DOC_ORG.Supports.DrawSupport(movDoc);
            }
            //if (MainDoc != null)
            //    AST_DOC_ORG.Members.DrawMember(MainDoc);

            //doc = vDoc;

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
                            vl1 = new VehicleLoad(movDoc, movDoc, load_data[j]);
                            vl1.TotalLength = AST_DOC_ORG.AnalysisData.Joints.MaxX;
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

            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(movDoc);
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
        public void Set_Moving_Load_Init()
        {
            AST_DOC_ORG = new ASTRADoc(AST_DOC.FileName);

            if (ld == null)
                ld = new LoadDeflection(AST_DOC_ORG, pbLoadDeflection);
            
            txtDefFactor.Text = ld.Factor.ToString("0");

            cmb_mov_loadcase.Items.Clear();
            for (int j = 0; j <= ld.MaxLoadCase; j++)
            {
                cmb_mov_loadcase.Items.Add(j);
            }
            //SetLoadIndex(1);


            //if (AST_DOC_ORG.IsDynamicLoad)
            //{
            //    AST_DOC_ORG.DrawMemberDetails(defDoc, 1);
            //}
            if (movDoc != null && AST_DOC_ORG.IsMovingLoad)
                MovingLoad_Initialize(movDoc, AST_DOC_ORG.FileName);

            List<int> lst_def_types = new List<int>();
            tv_ml_def.Nodes.Clear();
            for (int i = 0; i < AST_DOC_ORG.Load_Geneartion.Count; i++)
            {
                if (!lst_def_types.Contains(AST_DOC_ORG.Load_Geneartion[i].TypeNo))
                    lst_def_types.Add(AST_DOC_ORG.Load_Geneartion[i].TypeNo);
                tv_ml_def.Nodes.Add("TYPE " + AST_DOC_ORG.Load_Geneartion[i].TypeNo);
                tv_ml_def.Nodes[i].Nodes.Add("X : " + AST_DOC_ORG.Load_Geneartion[i].X.ToString("f3"));
                tv_ml_def.Nodes[i].Nodes.Add("Y : " + AST_DOC_ORG.Load_Geneartion[i].Y.ToString("f3"));
                tv_ml_def.Nodes[i].Nodes.Add("Z : " + AST_DOC_ORG.Load_Geneartion[i].Z.ToString("f3"));
                tv_ml_def.Nodes[i].Nodes.Add("XINC : " + AST_DOC_ORG.Load_Geneartion[i].XINC.ToString("f3"));
            }

            tv_ml_loads.Nodes.Clear();
            for (int i = 0; i < load_data.Count; i++)
            {
                if (lst_def_types.Contains(load_data[i].TypeNo))
                {
                    tv_ml_loads.Nodes.Add(load_data[i].TypeNoText + " : " + load_data[i].Code);
                    tv_ml_loads.Nodes[tv_ml_loads.Nodes.Count - 1].Nodes.Add("AXLE LOADS : " + load_data[i].LoadValues.ToString());
                    tv_ml_loads.Nodes[tv_ml_loads.Nodes.Count - 1].Nodes.Add("AXLE SPACINGS : " + load_data[i].Distances.ToString());
                    tv_ml_loads.Nodes[tv_ml_loads.Nodes.Count - 1].Nodes.Add("AXLE WIDTH : " + load_data[i].LoadWidth.ToString("f3"));
                }
            }

            if (AST_DOC_ORG.IsDynamicLoad)
            {
                //this.Text = "EIGEN VALUE ANALYSIS";
            }
            else
            {
                //this.Text = "MOVING LOAD DISPLAY";
            }
            cmb_mov_time.SelectedIndex = 0;
           
            cmb_mov_loadcase.SelectedIndex = 1;

            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(movDoc);


        }
        public void Moving_Stop()
        {
            tmr_moving_load.Stop();
            for (int i = 0; i < list_vehicle.Count; i++)
            {
                list_vehicle[i].Set_Wheel_Distance();
            }
            cmb_mov_loadcase.SelectedIndex = 1;
        }
        private void Start_Moving_Timer(bool flag)
        {

            int interval = 100;
            try
            {

                MyStrings  m = new MyStrings(cmbInterval.Text, ' ');

                double dd = m.GetDouble(0);
                if (dd < 0.3)
                    dd = dd * 100.0d;
                else
                    dd = dd * 1000.0d;

                if (dd == 0.0)
                    dd = 100.0;
                //else
                //    dd = dd * 100.0d;

                interval = int.Parse(dd.ToString());

            }
            catch (Exception xex)
            {
                interval = 100;
            }
            tmr_moving_load.Interval = interval;

            tmr_moving_load.Enabled = flag;
            btn_mov_pause.Enabled = flag;
            btn_mov_stop.Enabled = flag;
            cmb_mov_time.Enabled = !flag;
        }
        private void Set_Moving_LoadIndex(int indx)
        {
            int selIndex = cmb_mov_loadcase.SelectedIndex;
            selIndex += indx;

            if (selIndex == cmb_mov_loadcase.Items.Count)
            {
                selIndex = 0;
            }
            else if (selIndex == -1)
            {
                selIndex = cmb_mov_loadcase.Items.Count - 1;
            }
            try
            {
                cmb_mov_loadcase.SelectedIndex = selIndex;
            }
            catch (Exception ex) { }
            if (cmb_mov_loadcase.SelectedIndex == -1)
            {
                if (AST_DOC_ORG.IsMovingLoad)
                    AST_DOC_ORG.Members.DrawMember(defDoc);
                else
                    AST_DOC_ORG.Members.DrawMember(defDoc);
            }
            CleanMemory();
        }

        private void cmb_mov_loadcase_SelectedIndexChanged(object sender, EventArgs e)
        {

            #region Chiranjit [2011 09 24]   New Moving Load Analysis
            try
            {
                iLoadCase = int.Parse(cmb_mov_loadcase.Text);


                if (AST_DOC_ORG.IsMovingLoad)
                {
                    txt_dist.Text = (AST_DOC_ORG.Load_Geneartion[0].XINC * iLoadCase).ToString();
                    RunMovingLoad();
                }
                else
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(defDoc);


            }
            catch (Exception ex) { }
            #endregion Chiranjit [2011 09 24]   Commented for New Moving Load Analysis
        }
        private void btn_mov_run_Click(object sender, EventArgs e)
        {

            bIsNext = true;
            if (cmb_mov_loadcase.SelectedIndex == cmb_mov_loadcase.Items.Count - 1)
            {
                Moving_Stop();
            }
            if (cmb_mov_loadcase.SelectedIndex == 0)
            {
                Moving_Stop();
            }
            Start_Moving_Timer(true);
        }
        private void btn_mov_pause_Click(object sender, EventArgs e)
        {

            VectorDraw.Professional.Memory.vdMemory.Collect();
            GC.Collect();
            tmr_moving_load.Stop();
            Start_Moving_Timer(false);
        }
        private void btn_mov_stop_Click(object sender, EventArgs e)
        {
            Moving_Stop();
        }
        private void tmr_moving_load_Tick(object sender, EventArgs e)
        {
            if (bIsNext)
                Set_Moving_LoadIndex(+1);
            else
                Set_Moving_LoadIndex(-1);

            //Chiranjit [2011 12 14]
            //When Full load case shown timer should be stop
            if (cmb_mov_loadcase.SelectedIndex == cmb_mov_loadcase.Items.Count - 1)
                Start_Moving_Timer(false);

        }
        #endregion  Chiranjit [2014 04 10] New Moving load Animation

        private void tc5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tc5.SelectedTab == tab5_pre_prs)
            {
                sc3.Panel2Collapsed = (tc6.SelectedTab == tab_file_open);
            }
            else
            {
                if (!File.Exists(File_Name))
                {
                    MessageBox.Show("Please select a file.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    tc5.SelectedTab = tab5_pre_prs;
                    return;
                }
                if (tc5.SelectedTab == tab5_prs)
                {
                    sc3.Panel2Collapsed = true;
                }
                else if (tc5.SelectedTab == tab5_post_prs)
                {
                    if (!File.Exists(Analysis_File_Name))
                    {
                        MessageBox.Show("Process Analysis not done. This Panel enabled after Process Analysis.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //MessageBox.Show("Process Analysis not done. \"ANALYSIS_REP.TXT\"   file not found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        if (File.Exists(File_Name))
                            tc5.SelectedTab = tab5_prs;
                        else
                            tc5.SelectedTab = tab5_pre_prs;
                        return;
                    }
                    Tab_Selection();
                    //if (tc1.SelectedTab == tab_forces)
                    //    Show_Panel(tab1_forces);
                    //else
                    //    Show_Panel(tab1_max_force);
                    sc3.Panel2Collapsed = false;
                }
            }

        }

        private void btn_open_file_Click(object sender, EventArgs e)
        {

        }

        private void tc6_SelectedIndexChanged(object sender, EventArgs e)
        {
            tc_pp_panel.TabPages.Clear();
            if (tc6.SelectedTab == tab_geometry)
                tc_pp_panel.TabPages.Add(tab1_geometry);
            else if (tc6.SelectedTab == tab_loading)
            {
                tc_pp_panel.TabPages.Add(tab1_loading);
                tc4.SelectedTab = tab_org_doc;
            }
            sc3.Panel2Collapsed = (tc6.SelectedTab == tab_file_open);
        }

        private void txtDefFactor_EnabledChanged(object sender, EventArgs e)
        {
            txtDefFactor.Enabled = true;
        }

        private void btn_env_show_Click(object sender, EventArgs e)
        {
            //List<int> mem_list = MyList.Get_Array_Intiger(txt_env_mnos.Text);
            List<int> mem_list = new List<int>();

            List<AnalysisData> data = new List<AnalysisData>();
            List<double> tensile_forces = new List<double>();
            List<double> compressive_forces = new List<double>();




            List<int> top_chords = new List<int>();
            List<int> bottom_chords = new List<int>();

            foreach (var item in AST_DOC.AnalysisData.Members.Members)
            {
                if (item.StartNode.Z == 0.0 &&
                    item.EndNode.Z == 0.0 &&
                    item.EndNode.Y == AST_DOC.Joints.Max_Y_Positive &&
                    item.StartNode.Y == AST_DOC.Joints.Max_Y_Positive)
                    top_chords.Add(item.MemberNo);

                else if (item.StartNode.Z == 0.0 &&
                    item.EndNode.Z == 0.0 &&
                    item.EndNode.Y == AST_DOC.Joints.Max_Y_Negative &&
                    item.StartNode.Y == AST_DOC.Joints.Max_Y_Negative)
                    bottom_chords.Add(item.MemberNo);
            }

            AnalysisData ad = null;



            if (rbtn_env_top.Checked)
                mem_list = top_chords;
            if (rbtn_env_bottom.Checked)
                mem_list = bottom_chords;

            for (int i = 0; i < mem_list.Count; i++)
            {
                ad = (AnalysisData)StructureAnalysis.MemberAnalysis[mem_list[i]];
                if (ad != null)
                {
                    data.Add(ad);
                    if (ad.TensileForce != 0.0)
                        tensile_forces.Add(-ad.TensileForce);
                    else if (ad.CompressionForce != 0.0)
                        tensile_forces.Add(ad.CompressionForce);
                }
            }


            try
            {
                Envelope env = new Envelope(tensile_forces, AST_DOC);
                env.MembersNos = mem_list;

                envDoc.ActiveLayOut.Entities.RemoveAll();
                env.DrawEnvelop(envDoc);

                envDoc.Redraw(true);
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(envDoc);
            }
            catch (Exception ex)
            { }


        }

        private void tsmi_data_open_Click(object sender, EventArgs e)
        {
            string menu_name = "";


            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;

            if (tsmi != null) menu_name = tsmi.Name;
            else
            {
                ToolStripButton tsb = sender as ToolStripButton;

                if (tsb != null) menu_name = tsb.Name;
            }

            if (menu_name == tsmi_data_open.Name || menu_name == tsb_open_data.Name)
            {

                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Text Files (*.txt)|*.txt|ASTRA Files (*.ast)|*.ast";
                    if (ofd.ShowDialog() != DialogResult.Cancel)
                    {
                        File_Name = ofd.FileName;
                        rtb_input_file.Lines = File.ReadAllLines(File_Name);
                        tc_pp_panel.Visible = true;
                        sc3.Panel2Collapsed = true;
                        astDoc = new ASTRADoc(File_Name);
                        StructureAnalysis = null;
                        ld = null;
                        if (File.Exists(LL_TXT) && astDoc.IsMovingLoad)
                            rtb_ll_txt.Lines = File.ReadAllLines(LL_TXT);
                        if (File.Exists(Analysis_File_Name))
                            rtb_ana_rep.Lines = File.ReadAllLines(Analysis_File_Name);
                        Load_ASTRA_Data();

                        cmbLengthUnit.SelectedIndex = (int)AST_DOC.Base_LengthUnit;
                        cmbForceUnit.SelectedIndex = (int)AST_DOC.Base_MassUnit;

                        MyStrings ml = new MyStrings(astDoc.ProjectTitle, ' ');
                        txtUserTitle.Text = ml.GetString(2).Trim();
                        cmb_structure_type.SelectedItem = ml.StringList[1];
                    }
                }
            }
            else if (menu_name == tsmi_file_save.Name || menu_name == tsb_save_data.Name)
            {
                if (File.Exists(File_Name))
                    File.WriteAllLines(File_Name, rtb_input_file.Lines);
                if (File.Exists(LL_TXT))
                    File.WriteAllLines(LL_TXT, rtb_ll_txt.Lines);


                //File_Name = ofd.FileName;
                //rtb_input_file.Lines = File.ReadAllLines(File_Name);
                //tc2.Visible = true;
                //sc3.Panel2Collapsed = true;
                astDoc = new ASTRADoc(File_Name);
                StructureAnalysis = null;
                ld = null;
                //if (File.Exists(LL_TXT) && astDoc.IsMovingLoad)
                //    rtb_ll_txt.Lines = File.ReadAllLines(LL_TXT);
                //if (File.Exists(Analysis_File_Name))
                //    rtb_ana_rep.Lines = File.ReadAllLines(Analysis_File_Name);
               

                Load_ASTRA_Data();

                MessageBox.Show("Data Saved Sucessfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);



                cmbLengthUnit.SelectedIndex = (int)AST_DOC.Base_LengthUnit;
                cmbForceUnit.SelectedIndex = (int)AST_DOC.Base_MassUnit;

                MyStrings ml = new MyStrings(astDoc.ProjectTitle, ' ');
                txtUserTitle.Text = ml.GetString(2).Trim();
                cmb_structure_type.SelectedItem = ml.StringList[1];

            }
            else if (menu_name == tsmi_dwg_save.Name || menu_name == tsb_save_drawing.Name)
            {
                SaveDrawing(ActiveDoc);
            }
            else if (menu_name == tsmi_file_print.Name || menu_name == tsb_print_drawing.Name)
            {
                if (ActiveDoc != null)
                {
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.PrintEx(ActiveDoc);
                }
            }
            else if (menu_name == tsmi_close.Name || menu_name == tsb_close.Name)
            {
                this.Close();
            }
        }

        private void dgv_max_frc_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            Set_Max_Force_Column();

        }

        private void dgv_max_frc_SelectionChanged(object sender, EventArgs e)
        {
            Set_Max_Force_Column();
        }

        private void Set_Max_Force_Column()
        {

            try
            {
                bool flag = (dgv_max_frc[1, dgv_max_frc.SelectedCells[0].RowIndex].Value.ToString() == "BEAM");

                if (flag)
                {
                    col_cmf.Width = 0;
                    col_tf.Width = 0;
                    col_tor.Width = 99;
                    col_af.Width = 99;
                    col_bm.Width = 99;
                    col_sf.Width = 99;
                }
                else
                {
                    col_cmf.Width = 99;
                    col_tf.Width = 99;
                    col_tor.Width = 0;
                    col_af.Width = 0;
                    col_bm.Width = 0;
                    col_sf.Width = 0;
                }
            }
            catch (Exception ex) { }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            frm_ASTRA_Inputs frm = new frm_ASTRA_Inputs();
            frm.ShowDialog();
        }

        private void tsmi_astra_viewer_Click(object sender, EventArgs e)
        {

        }

    }
    public enum eWorkingMode
    {
        Structure = 0,
        Loading = 1,
        PostProcess = 2,
    }
}
