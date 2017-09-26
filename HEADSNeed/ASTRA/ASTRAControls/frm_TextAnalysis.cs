using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Geometry;

using HEADSNeed.ASTRA.ASTRADrawingTools;
using HEADSNeed.ASTRA.ASTRAClasses;
using HEADSNeed.ASTRA.CadToAstra.FORMS;
using HEADSNeed.ASTRA.CadToAstra;
using HEADSNeed.ASTRA.ASTRAForms;

using MovingLoadAnalysis;
using MovingLoadAnalysis.DataStructure;

using VDRAW = VectorDraw.Professional.ActionUtilities;

namespace HEADSNeed.ASTRA.ASTRAControls
{
    public partial class frm_TextAnalysis : Form
    {

        //public frm_ASTRA_Inputs()
        //{
        //    InitializeComponent();
        //}
        #region Pre Process

        public vdDocument VDoc
        {
            get
            {
                return vdScrollableControl1.BaseControl.ActiveDocument;
            }
        }
        public ASTRADoc AST_DOC
        {
            get
            {
                if (ACad != null)
                    return ACad.AstraDocument;
                return null;
            }
            set
            {
                if (ACad != null)
                    ACad.AstraDocument = value;
            }
        }
        ASTRACAD ACad;

        int lastId = 0;

        public int CurrentLoadIndex { get; set; }

        bool IsDrawingFileOpen { get; set; }

        public LoadCaseDefinition Current_LoadCase
        {
            get
            {
                try
                {
                    return LoadCases[CurrentLoadIndex];
                }
                catch (Exception ex) { }
                return null;

            }
            set
            {
                try
                {
                    LoadCases[CurrentLoadIndex] = value;
                }
                catch (Exception ex) { }
            }
        }
        public List<LoadCaseDefinition> LoadCases { get; set; }
        public List<MovingLoadData> MovingLoads { get; set; }
        public List<LiveLoad> LL_Definition { get; set; }
        public List<MaterialProperty> Materials { get; set; }
        MemberGroupCollection Groups { get; set; }

        string Drawing_File { get; set; }
        //string File_Name { get; set; }
        string DataFileName
        {
            get
            {
                return File_Name;
            }
            set
            {
                File_Name = value;
            }
        }

        public frm_TextAnalysis(string input_file)
        {
            //Member m;
            //MemberIncidence mi;
            //MemberProperty mp;

            try
            {
                InitializeComponent();
                //vdScrollableControl1.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
                //vdScrollableControl1.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);
                Base_Control_MouseEvent();

                Drawing_File = "";
                File_Name = input_file;


                ACad = new ASTRACAD();
                LoadCases = new List<LoadCaseDefinition>();
                //Combinations = new List<LoadCombinations>();
                MovingLoads = new List<MovingLoadData>();
                LL_Definition = new List<LiveLoad>();
                Groups = new MemberGroupCollection();
                Materials = new List<MaterialProperty>();

                IsDrawingFileOpen = false;
            }
            catch (Exception ex) { }

        }

        public frm_TextAnalysis(string drawing_file, bool IsDrawingFile)
        {
            //Member m;
            //MemberIncidence mi;
            //MemberProperty mp;

            try
            {
                InitializeComponent();
                //vdScrollableControl1.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
                //vdScrollableControl1.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);
                Base_Control_MouseEvent();

                IsDrawingFileOpen = IsDrawingFile;

                if (IsDrawingFile)
                {
                    Drawing_File = drawing_file;
                    File_Name = "";
                }
                else
                {
                    Drawing_File = "";
                    File_Name = drawing_file;
                }


                ACad = new ASTRACAD();
                LoadCases = new List<LoadCaseDefinition>();
                //Combinations = new List<LoadCombinations>();
                MovingLoads = new List<MovingLoadData>();
                LL_Definition = new List<LiveLoad>();
                Groups = new MemberGroupCollection();
                Materials = new List<MaterialProperty>();

            }
            catch (Exception ex) { }

        }


        public frm_TextAnalysis()
        {
            //this.Show
            try
            {
                InitializeComponent();
                //vdScrollableControl1.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
                //vdScrollableControl1.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);
                Base_Control_MouseEvent();
                Drawing_File = "";
                File_Name = "";
                ACad = new ASTRACAD();

                LoadCases = new List<LoadCaseDefinition>();
                //Combinations = new List<LoadCombinations>();
                MovingLoads = new List<MovingLoadData>();
                LL_Definition = new List<LiveLoad>();
                Groups = new MemberGroupCollection();
                Materials = new List<MaterialProperty>();

                IsDrawingFileOpen = false;
            }
            catch (Exception ex) { }

        }

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

            //ACad.Create_Data
        }
        private void SetGridWithMember()
        {
            dgv_members.Rows.Clear();

            foreach (var item in AST_DOC.Members)
            {
                try
                {
                    if (item.Property == null)
                        item.Property = new MemberProperty();

                    dgv_members.Rows.Add(item.MemberNo, item.MemberType,
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
            dgv_elements.Rows.Clear();
            foreach (var item in AST_DOC.Elements)
            {
                try
                {
                    dgv_elements.Rows.Add(item.ElementNo,
                        item.Node1.NodeNo,
                        item.Node2.NodeNo,
                        item.Node3.NodeNo,
                        item.Node4.NodeNo,
                        item.ThickNess.Replace("TH: ", "")
                        );
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void Tab_Selection()
        {
            sc2.Panel2Collapsed = (tc_prp_main.SelectedTab == tab_file_open);
            sc1.Panel2Collapsed = (tc_prp_main.SelectedTab == tab_file_open);

            if (tc_prp_main.SelectedTab == tab_geom)
                Show_Panel(tab1_geom);
            else if (tc_prp_main.SelectedTab == tab_props)
                Show_Panel(tab1_props);
            else if (tc_prp_main.SelectedTab == tab_constants)
                Show_Panel(tab1_const);
            else if (tc_prp_main.SelectedTab == tab_supports)
                Show_Panel(tab1_supports);
            else if (tc_prp_main.SelectedTab == tab_loads)
                Show_Panel(tab1_loads);
            else if (tc_prp_main.SelectedTab == tab_mov_load)
                Show_Panel(tab1_moving);
            else if (tc_prp_main.SelectedTab == tab_dynamic)
                Show_Panel(tab1_dynamic);
            else if (tc_prp_main.SelectedTab == tab_ana_spec)
                Show_Panel(tab1_ana_spec);
        }
        public void Show_Panel(TabPage tp)
        {
            tc_prp_panel.TabPages.Clear();
            tc_prp_panel.TabPages.Add(tp);
        }

        public void ShowMemberOnGrid(vdLine vLine)
        {
            for (int i = 0; i < AST_DOC.Members.Count; i++)
            {
                if (AST_DOC.Members[i].StartNode.Point == vLine.StartPoint &&
                    AST_DOC.Members[i].EndNode.Point == vLine.EndPoint)
                {

                    DataGridView dgv = dgv_members;

                    vdDocument VD = ActiveDoc;

                    //if (VD == VDoc) dgv = dgv_members;
                    if (VD == maxDoc) dgv = dgv_max_frc;


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

            for (int i = 0; i < AST_DOC.Joints.Count; i++)
            {
                if (AST_DOC.Joints[i].Point == gp)
                    jNo = AST_DOC.Joints[i].NodeNo;
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


            DataGridView dgv = dgv_members;

            vdDocument VD = ActiveDoc;


            for (int j = 0; j < dgv.Rows.Count; j++)
            {
                int memNo = (int)dgv[0, j].Value;

                if (memNo == mNo)
                {
                    ClearSelect(dgv);
                    dgv.Rows[j].Selected = true;
                    dgv.FirstDisplayedScrollingRowIndex = j;
                    AST_DOC.Members.ShowMember(j, VD, 0.03d);
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

            for (int i = 0; i < AST_DOC.Elements.Count; i++)
            {
                if (AST_DOC.Elements[i].Node1.Point == _3dFace.VertexList[0] &&
                    AST_DOC.Elements[i].Node2.Point == _3dFace.VertexList[1] &&
                    AST_DOC.Elements[i].Node3.Point == _3dFace.VertexList[2] &&
                    AST_DOC.Elements[i].Node4.Point == _3dFace.VertexList[3])
                {
                    for (int j = 0; j < dgv_members.RowCount; j++)
                    {
                        elmtNo = (int)dgv_members[0, j].Value;
                        ss = dgv_members[1, j].Value.ToString().Trim().TrimEnd().TrimStart();
                        if (elmtNo == AST_DOC.Elements[i].ElementNo &&
                            ss == "PLATE")
                        {
                            //ClearSelect();

                            dgv_members.Rows[j].Selected = true;
                            dgv_members.FirstDisplayedScrollingRowIndex = j;
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

            for (int i = 0; i < AST_DOC.Elements.Count; i++)
            {
                if (AST_DOC.Elements[i].Node1.Point == _pline.VertexList[0] &&
                    AST_DOC.Elements[i].Node2.Point == _pline.VertexList[1] &&
                    AST_DOC.Elements[i].Node3.Point == _pline.VertexList[2] &&
                    AST_DOC.Elements[i].Node4.Point == _pline.VertexList[3])
                {
                    for (int j = 0; j < dgv_members.RowCount; j++)
                    {
                        elmtNo = (int)dgv_elements[0, j].Value;
                        ss = dgv_elements[1, j].Value.ToString().Trim().TrimEnd().TrimStart();
                        if (elmtNo == AST_DOC.Elements[i].ElementNo &&
                            ss == "PLATE")
                        {
                            ClearSelect(dgv_elements);
                            tc3.SelectedTab = tab2_elements;

                            dgv_elements.Rows[j].Selected = true;
                            dgv_elements.FirstDisplayedScrollingRowIndex = j;
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
            ClearSelect(dgv_members);
        }
        public void SetTextSize(double txtSize)
        {
            vdDocument VD = ActiveDoc;

            if (VD == null) return;
            foreach (var item in VD.ActionLayout.Entities)
            {
                if (item is vdMText)
                {
                    vdMText t = item as vdMText;
                    t.Height = txtSize;
                    t.Update();
                }
                if (item is vdText)
                {
                    vdText t = item as vdText;
                    t.Height = txtSize;
                    t.Update();
                }
                if (item is vdCircle)
                {
                    vdCircle t = item as vdCircle;
                    t.Radius = txtSize * 0.3;
                    t.Update();
                }

            }
            VD.Redraw(true);

            //for (int i = 0; i < VD.ActiveLayOut.Entities.Count; i++)
            //{
            //    vdText txt = VD.ActiveLayOut.Entities[i] as vdText;
            //    if (txt != null)
            //    {
            //        txt.Height = txtSize / 10.0d;
            //        txt.Update();
            //    }
            //}
            VD.Redraw(true);
        }


        private void frm_ASTRA_Inputs_Load(object sender, EventArgs e)
        {
            //Base_Control_MouseEvent();

            sc1.Panel2Collapsed = true;
            //MyList
            for (int i = 1; i < 100; i++)
            {
                cmb_text_size.Items.Add(i.ToString());
                cmb_pp_text_size.Items.Add(i.ToString());
            }
            //MyList

            VDoc.Palette.Background = Color.White;
            chk_selfweight.Checked = false;
            cmb_Self_Weight.SelectedIndex = 1;

            cmb_structure_type.SelectedIndex = 0;
            cmb_Base_LUnit.SelectedIndex = 0;
            cmb_Base_MUnit.SelectedIndex = 1;
            cmb_Const_LUnit.SelectedIndex = 0;
            cmb_Const_MUnit.SelectedIndex = 1;
            cmb_Load_LUnit.SelectedIndex = 0;
            cmb_Load_MUnit.SelectedIndex = 1;

            cmb_Prop_LUnit.SelectedIndex = 0;


            for (int i = 0; i < tc_prp_panel.TabPages.Count; i++)
            {
                tc_prp_panel.TabPages[i].Text = "";
            }


            ACad.Document = VDoc;



            if (IsDrawingFileOpen)
            {
                if (File.Exists(Drawing_File))
                {
                    Open_Drawing_File();
                    tc_prp_main.SelectedTab = tab_geom;
                }
                else
                    sc2.Panel1Collapsed = true;
            }
            else
            {
                if (File.Exists(File_Name))
                {
                    Open_Data_File(File_Name);
                    //VDRAW.vdCommandAction.View3D_VTop(VDoc);

                }
                else
                    sc2.Panel1Collapsed = true;
                tc_prp_main.SelectedTab = tab_geom;
                tc_prp_main.SelectedTab = tab_file_open;
            }


            Button_Enable_Disable();

            Count_Geometry();

            timer1.Start();

            cmb_text_size.SelectedIndex = 0;
            cmb_pp_text_size.SelectedIndex = 0;
        }

        private void Open_Drawing_File()
        {

            if (File.Exists(Drawing_File))
            {
                if (!VDoc.Open(Drawing_File))
                    MessageBox.Show(Drawing_File + " could not be opened.");

                //VDoc.Palette.Background = Color.White;


                VDoc.Palette.Background = Color.White;

                ACad.Document = VDoc;
                ACad.StructuralGeometry(VDoc);

                Clear_All();


                //Draw_Joints();
                //Draw_Members();

                SetGridWithNode();
                SetGridWithMember();

                Tab_Selection();


                cmb_structure_type.SelectedIndex = 0;
                cmb_Base_MUnit.SelectedIndex = 1;
                cmb_Base_LUnit.SelectedIndex = 0;

                cmb_Prop_LUnit.SelectedIndex = 0;


                cmb_Const_MUnit.SelectedIndex = 1;
                cmb_Const_LUnit.SelectedIndex = 0;

                cmb_Load_MUnit.SelectedIndex = 1;
                cmb_Load_LUnit.SelectedIndex = 0;






                txtUserTitle.Text = "Analysis Input Data File";

                Button_Enable_Disable();


                Draw_Joints();
                Draw_Members();
                VDRAW.vdCommandAction.View3D_VTop(VDoc);

                tc_prp_main.SelectedTab = tab_geom;

            }
        }

        private void Clear_All()
        {

            VDoc.ActiveLayOut.Entities.EraseAll();

            dgv_elements.Rows.Clear();
            dgv_members.Rows.Clear();
            dgv_joints.Rows.Clear();

            tv_mem_props.Nodes.Clear();
            tv_elem_props.Nodes.Clear();
            tv_mem_spec_truss.Nodes.Clear();
            tv_mem_spec_cable.Nodes.Clear();
            tv_mem_grps.Nodes.Clear();
            tv_mem_release.Nodes.Clear();
            tv_constants.Nodes.Clear();
            tv_supports.Nodes.Clear();
            tv_loads.Nodes.Clear();
            tv_mov_def_load.Nodes.Clear();
            tv_mov_loads.Nodes.Clear();
            tv_max_frc.Nodes.Clear();

        }
        private void toolStripButtons_Click(object sender, EventArgs e)
        {
            vdDocument VD = VDoc;

            //if (tc4.SelectedIndex == 1) VD = defDoc;
            //if (tc4.SelectedIndex == 1) VD = defDoc;

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
            else if (tsb.Name == tsb_ShadeOn.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_ShadeOn(VD);
            else if (tsb.Name == tsb_Wire.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Wire(VD);
            else if (tsb.Name == tsb_Save.Name)
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "VDML File (*.vdml)|*.vdml|DXF File (*.dxf)|*.dxf|DWG File (*.dwg)|*.dwg";
                    if (sfd.ShowDialog() != DialogResult.Cancel)
                    {
                        if (HASP_Lock.IsDemoVersion())
                        {
                            MessageBox.Show("This function is not available in Demo version.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (VD.SaveAs(sfd.FileName))
                            {
                                MessageBox.Show("File Saved successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
        }
        private void tc1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tab_Selection();
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

            vdDocument VD = ActiveDoc;

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
        void BaseControl_vdMouseUp(MouseEventArgs e, ref bool cancel)
        {
            try
            {

                if (tc_parrent.SelectedTab == tab_pre_process)
                {
                    if (tc_prp_main.SelectedTab != tab_props || e.Button == MouseButtons.Right)
                    {
                        Delete_Layer_Items("Selection");
                        //return;
                    }

                    if (tc_prp_main.SelectedTab != tab_geom)
                    {
                        for (int i = 0; i < VDoc.ActiveLayOut.Entities.Count; i++)
                        {
                            if (VDoc.ActiveLayOut.Entities[i] is vdCircle)
                            {
                                if (VDoc.ActiveLayOut.Entities[i].Layer.Name != "Selection")
                                {
                                    VDoc.ActiveLayOut.Entities.RemoveAt(i); i = -1;
                                }
                            }
                        }
                        VDoc.Redraw(true);
                        return;
                    }
                    else
                    {

                        if (tc3.SelectedTab == tab2_elements)
                        {
                            return;
                        }
                    }
                }


                vdSelection gripset = GetGripSelection(false);
                vd3DFace dFace;
                vdLine ln;

                vdDocument VD = ActiveDoc;
                foreach (vdFigure fig in gripset)
                {
                    if (fig is vdText)
                    {
                        ShowMemberOnGrid(fig as vdText);
                        VD.Redraw(true);
                        break;
                    }
                    if (fig is vdPolyline)
                    {
                        ShowElementOnGrid(fig as vdPolyline);
                        VD.Redraw(true);
                        break;
                    }

                    if (fig is ASTRASupportFixed)
                    {
                        ASTRASupportFixed asf = fig as ASTRASupportFixed;

                        ShowNodeOnGrid(asf.Origin);
                        VD.Redraw(true);
                        break;
                    }

                    if (fig is ASTRASupportPinned)
                    {
                        ASTRASupportPinned asp = fig as ASTRASupportPinned;

                        ShowNodeOnGrid(asp.Origin);
                        VD.Redraw(true);
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
                        VD.Redraw(true);
                    }
                    else
                    {
                        ShowMemberOnGrid(ln);
                        VD.Redraw(true);
                    }
                }

                gripset.RemoveAll();
            }
            catch (Exception exx) { }
        }
        #endregion Cad Methods



        private void dgvElementGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                AST_DOC.Elements.ShowElement(e.RowIndex, VDoc, 0.093d);
            }
            catch (Exception ex) { }
        }
        private void dgvMemberGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                AST_DOC.Members.ShowMember(e.RowIndex, VDoc);
            }
            catch (Exception ex) { }
        }
        private void dgv_joints_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;


            gPoint joint = new gPoint();

            try
            {
                //joint.x = double.Parse(dgv_joints[1, e.RowIndex].Value.ToString());
                //joint.y = double.Parse(dgv_joints[2, e.RowIndex].Value.ToString());
                //joint.z = double.Parse(dgv_joints[3, e.RowIndex].Value.ToString());

                //VectorDraw.Professional.vdPrimaries.vdFigure fg = null;
                //if (lastId != -1)
                //{
                //    for (int i = VDoc.ActiveLayOut.Entities.Count - 1; i >= 0; i--)
                //    {
                //        fg = VDoc.ActiveLayOut.Entities[i];
                //        if (fg.Id == lastId)
                //        {
                //            VDoc.ActiveLayOut.Entities.RemoveAt(i);
                //            break;
                //        }
                //    }
                //}
                //VDoc.CommandAction.CmdSphere(joint, 0.039, 10, 10);

                //lastId = VDoc.ActiveLayOut.Entities.Count - 1;
                //fg = VDoc.ActiveLayOut.Entities[lastId];
                //lastId = fg.Id;
                //fg.PenColor = new vdColor(Color.DarkViolet);

                //VDoc.Redraw(true);
            }
            catch (Exception ex) { }
        }

        private void btn_props_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_props_add.Name)
            {
                frmSectionProperties fs = new frmSectionProperties(ACad);
                fs.DGV_Joints = dgv_joints;
                fs.DGV_Members = dgv_members;
                fs.Owner = this;
                fs.MGC = Groups;
                fs.TRV = tv_mem_props;
                fs.ShowDialog();
                //if (fs.ASTRA_Data.Count > 0)
                //    tv_mem_props.Nodes.Add(fs.ASTRA_Data[0]);
            }
            else if (btn.Name == btn_props_del.Name)
            {
                tv_mem_props.Nodes.Remove(tv_mem_props.SelectedNode);
            }
            else if (btn.Name == btn_props_edit.Name)
            {
                frmSectionProperties fs = new frmSectionProperties(ACad);
                fs.Owner = this;
                fs.MGC = Groups;
                fs.TRV = tv_mem_props;
                fs.ASTRA_Data.Add(tv_mem_props.SelectedNode.Text);
                fs.ShowDialog();
            }
            if (tv_mem_props.SelectedNode != null)
                ShowMember(tv_mem_props.SelectedNode.Text, Text_Size * 0.3);
            else
                Delete_Layer_Items("Selection");

        }

        private void btn_grps_add_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Name == btn_grps_add.Name)
            {
                frmMemberGroups fs = new frmMemberGroups(ACad);
                fs.Owner = this;
                fs.ShowDialog();

                if (fs.MGroup != null)
                {
                    Groups.Add(fs.MGroup);
                    tv_mem_grps.Nodes.Add(fs.ASTRA_Data);
                }

            }
            else if (btn.Name == btn_grps_del.Name)
            {
                try
                {
                    Groups.RemoveAt(tv_mem_grps.SelectedNode.Index);

                    tv_mem_grps.Nodes.Remove(tv_mem_grps.SelectedNode);
                }
                catch (Exception ex) { }

            }
            else if (btn.Name == btn_grps_edit.Name)
            {
                try
                {
                    frmMemberGroups fs = new frmMemberGroups(ACad);
                    fs.Owner = this;
                    fs.MGroup = Groups[tv_mem_grps.SelectedNode.Index];
                    fs.ShowDialog();
                    Groups[tv_mem_grps.SelectedNode.Index] = fs.MGroup;
                    tv_mem_grps.Nodes[tv_mem_grps.SelectedNode.Index].Text = fs.ASTRA_Data;

                }
                catch (Exception ex) { }

            }
        }

        private void btn_spec_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;

            if (btn.Name == btn_spec_add_cable.Name)
            {
                frmMemberTruss fs = new frmMemberTruss(ACad, true);
                fs.Owner = this;

                fs.MGC = Groups;
                fs.TRV = tv_mem_spec_cable;
                fs.ShowDialog();
                //tv_mem_spec_cable.Nodes.Add(fs.ASTRA_Data);
            }
            else if (btn.Name == btn_spec_del_cable.Name)
            {
                tv_mem_spec_cable.Nodes.Remove(tv_mem_spec_cable.SelectedNode);
            }
            else if (btn.Name == btn_spec_edit_cable.Name)
            {

                frmMemberTruss fs = new frmMemberTruss(ACad);
                fs.Owner = this;
                fs.MGC = Groups;
                fs.TRV = tv_mem_spec_cable;
                fs.ASTRA_Data = (tv_mem_spec_cable.SelectedNode.Text);
                fs.ShowDialog();
            }

            else if (btn.Name == btn_spec_add_truss.Name)
            {

                frmMemberTruss fs = new frmMemberTruss(ACad);
                fs.Owner = this;
                fs.MGC = Groups;
                fs.TRV = tv_mem_spec_truss;
                fs.ShowDialog();
                //tv_mem_spec_truss.Nodes.Add(fs.ASTRA_Data);
            }
            else if (btn.Name == btn_spec_del_truss.Name)
            {
                tv_mem_spec_truss.Nodes.Remove(tv_mem_spec_truss.SelectedNode);
            }
            else if (btn.Name == btn_spec_edit_truss.Name)
            {

                frmMemberTruss fs = new frmMemberTruss(ACad);
                fs.Owner = this;
                fs.MGC = Groups;
                fs.TRV = tv_mem_spec_truss;
                fs.ASTRA_Data = (tv_mem_spec_truss.SelectedNode.Text);
                fs.ShowDialog();
            }

        }

        private void btn_release_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;
            if (btn.Name == btn_release_add.Name)
            {
                frmMemberRelease fs = new frmMemberRelease(ACad);
                fs.Owner = this;
                fs.TRV = tv_mem_release;
                fs.ShowDialog();
            }
            else if (btn.Name == btn_release_del.Name)
            {
                tv_mem_release.Nodes.Remove(tv_mem_release.SelectedNode);
            }
            else if (btn.Name == btn_release_edit.Name)
            {

                frmMemberRelease fs = new frmMemberRelease(ACad);
                fs.Owner = this;
                fs.TRV = tv_mem_release;
                fs.ASTRA_Data = tv_mem_release.SelectedNode.Text;
                fs.ShowDialog();
            }
        }

        private void btn_constant_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;
            if (btn.Name == btn_cons_add.Name)
            {
                frmMaterialProperties fs = new frmMaterialProperties(ACad);
                fs.Owner = this;
                fs.ShowDialog();

                if (fs.ASTRA_Data.Count > 0)
                {

                    TreeNode tn = new TreeNode("MATERIAL_" + (tv_constants.Nodes.Count + 1));
                    foreach (var item in fs.ASTRA_Data)
                    {
                        tn.Nodes.Add(item);
                    }
                    tv_constants.Nodes.Add(tn);

                    Materials.Add(fs.Material);

                }
            }
            else if (btn.Name == btn_cons_edit.Name)
            {
                TreeNode tn = tv_constants.SelectedNode;

                while (tn.Parent != null) tn = tn.Parent;

                frmMaterialProperties fs = new frmMaterialProperties(ACad);
                fs.Owner = this;
                fs.Material = Materials[tn.Index];

                fs.ShowDialog();

                if (fs.ASTRA_Data.Count > 0)
                {

                    //tn.Text = new TreeNode("MATERIAL_" + (tv_constants.Nodes.Count + 1));
                    tn.Nodes.Clear();
                    foreach (var item in fs.ASTRA_Data)
                    {
                        tn.Nodes.Add(item);
                    }
                    //tv_constants.Nodes.Add(tn);

                    Materials[tn.Index] = (fs.Material);

                }
            }
            else if (btn.Name == btn_cons_del.Name)
            {

                TreeNode tn = tv_constants.SelectedNode;
                while (tn.Parent != null) tn = tn.Parent;

                Materials.RemoveAt(tn.Index);

                tv_constants.Nodes.RemoveAt(tn.Index);
                //tv_constants.Nodes.RemoveAt(tv_constants.SelectedNode);
                //tv_constants.s
            }
        }

        private void btn_supp_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Name == btn_supp_add.Name)
            {
                frmSupport fs = new frmSupport(ACad);
                fs.Owner = this;
                fs.TRV = tv_supports;
                fs.ShowDialog();
            }
            else if (btn.Name == btn_supp_del.Name)
            {
                tv_supports.Nodes.Remove(tv_supports.SelectedNode);
                //tv_constants.s
            }
            else if (btn.Name == btn_supp_edit.Name)
            {
                frmSupport fs = new frmSupport(ACad);
                fs.Owner = this;
                fs.TRV = tv_supports;
                fs.ASTRA_Data = tv_supports.SelectedNode.Text;
                fs.ShowDialog();
            }
            Draw_Supports();
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            TreeNode tn = tv_loads.SelectedNode;


            bool flag = false;
            if (tn != null)
            {
                try
                {
                    while (tn.Parent != null) tn = tn.Parent;
                    CurrentLoadIndex = tn.Index;
                    tn.ExpandAll();
                }
                catch (Exception ex) { }
            }
            else
            {
                if (tv_loads.Nodes.Count > 0)
                    tn = tv_loads.Nodes[0];
            }
            #region Define Load Case
            if (btn.Name == btn_ldc_add.Name)
            {
                frmLoadCase fs = new frmLoadCase(ACad);
                fs.txt_load_case.Text = (tv_loads.Nodes.Count + 1) + "";
                fs.Owner = this;
                fs.ShowDialog();

                if (fs.ASTRA_Data != "")
                {
                    tv_loads.Nodes.Add(fs.ASTRA_Data);
                    LoadCases.Add(fs.Ld);
                }
            }
            else if (btn.Name == btn_ldc_edit.Name)
            {

                frmLoadCase fs = new frmLoadCase(ACad);
                fs.txt_load_case.Text = (tv_loads.Nodes.Count + 1) + "";
                fs.Owner = this;
                fs.ASTRA_Data = tv_loads.Nodes[CurrentLoadIndex].Text;
                fs.ShowDialog();

                if (fs.ASTRA_Data != "")
                {
                    tv_loads.Nodes[CurrentLoadIndex].Text = fs.ASTRA_Data;
                    Current_LoadCase.LoadNo = fs.Ld.LoadNo;
                    Current_LoadCase.Title = fs.Ld.Title;
                }
            }
            else if (btn.Name == btn_ldc_del.Name)
            {
                try
                {
                    tv_loads.Nodes.RemoveAt(CurrentLoadIndex);
                    LoadCases.RemoveAt(CurrentLoadIndex);
                }
                catch (Exception ex) { }
            }
            #endregion Define Load Case

            #region Define Joint Load
            else if (btn.Name == btn_jload_add.Name)
            {

                for (int i = 0; i < tn.Nodes.Count; i++)
                {
                    if (tn.Nodes[i].Text.StartsWith("JOINT"))
                    {
                        tn = tn.Nodes[i];
                        flag = true;
                        break;
                    }
                }
                tn.ExpandAll();

                if (!flag)
                {
                    tn.Nodes.Add("JOINT LOAD");

                    tn.ExpandAll();
                    tn = tn.Nodes[tn.Nodes.Count - 1];
                }


                frmJointLoad fs = new frmJointLoad(ACad);
                if (tn != null)
                    fs.Node = tn;
                fs.Owner = this;
                fs.ShowDialog();

                foreach (var item in fs.ASTRA_Data)
                {
                    LoadCases[CurrentLoadIndex].JointLoads.Add(item);
                }
            }
            else if (btn.Name == btn_jload_edit.Name)
            {

                int r = LoadCases[CurrentLoadIndex].JointLoads.IndexOf(tv_loads.SelectedNode.Text);
                frmJointLoad fs = new frmJointLoad(ACad);
                if (tn != null)
                    fs.Node = tv_loads.SelectedNode;
                fs.Owner = this;
                fs.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
                fs.ShowDialog();


                if (r != -1 && fs.ASTRA_Data.Count > 0)
                {
                    LoadCases[CurrentLoadIndex].JointLoads[r] = fs.ASTRA_Data[0];
                }

            }
            else if (btn.Name == btn_jload_del.Name)
            {
                //LoadCases[CurrentLoadIndex].JointLoads.Add(tv_loadings.SelectedNode);
                tv_loads.SelectedNode.Parent.Nodes.Remove(tv_loads.SelectedNode);
            }
            #endregion Define Joint Load

            #region Define SINK Load
            else if (btn.Name == btn_sdload_add.Name)
            {

                for (int i = 0; i < tn.Nodes.Count; i++)
                {
                    if (tn.Nodes[i].Text.StartsWith("SUPPORT"))
                    {
                        tn = tn.Nodes[i];
                        flag = true;
                        break;
                    }
                }
                tn.ExpandAll();

                if (!flag)
                {
                    tn.Nodes.Add("SUPPORT DISPLACEMENT LOAD");

                    tn.ExpandAll();
                    tn = tn.Nodes[tn.Nodes.Count - 1];
                }


                frmSupportDisplacements fs = new frmSupportDisplacements(ACad);
                if (tn != null)
                    fs.Node = tn;
                fs.Owner = this;
                fs.ShowDialog();

                foreach (var item in fs.ASTRA_Data)
                {
                    LoadCases[CurrentLoadIndex].SupportDisplacementLoads.Add(item);
                }
            }
            else if (btn.Name == btn_sdload_edit.Name)
            {

                int r = LoadCases[CurrentLoadIndex].SupportDisplacementLoads.IndexOf(tv_loads.SelectedNode.Text);
                frmSupportDisplacements fs = new frmSupportDisplacements(ACad);
                if (tn != null)
                    fs.Node = tv_loads.SelectedNode;
                fs.Owner = this;
                fs.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
                fs.ShowDialog();


                if (r != -1 && fs.ASTRA_Data.Count > 0)
                {
                    LoadCases[CurrentLoadIndex].SupportDisplacementLoads[r] = fs.ASTRA_Data[0];
                }

            }
            else if (btn.Name == btn_sdload_del.Name)
            {
                //LoadCases[CurrentLoadIndex].JointLoads.Add(tv_loadings.SelectedNode);
                tv_loads.SelectedNode.Parent.Nodes.Remove(tv_loads.SelectedNode);
            }
            #endregion Define SINK Load


            #region Define Member Load
            else if (btn.Name == btn_mload_add.Name)
            {

                for (int i = 0; i < tn.Nodes.Count; i++)
                {
                    if (tn.Nodes[i].Text.StartsWith("MEMBER"))
                    {
                        tn = tn.Nodes[i];
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {

                    tn.Nodes.Add("MEMBER LOAD");

                    tn.ExpandAll();
                    tn = tn.Nodes[tn.Nodes.Count - 1];
                    tn.ExpandAll();


                }



                frmMemberLoad fs = new frmMemberLoad(ACad);

                if (tn != null)
                    fs.Node = tn;
                fs.Owner = this;
                fs.ShowDialog();

                foreach (var item in fs.ASTRA_Data)
                {
                    LoadCases[CurrentLoadIndex].MemberLoads.Add(item);
                    //tn.Nodes.Add(item);
                }
            }
            else if (btn.Name == btn_mload_edit.Name)
            {

                int r = LoadCases[CurrentLoadIndex].MemberLoads.IndexOf(tv_loads.SelectedNode.Text);
                frmMemberLoad fs = new frmMemberLoad(ACad);
                if (tn != null)
                    fs.Node = tv_loads.SelectedNode;
                fs.Owner = this;
                fs.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
                fs.ShowDialog();


                if (r != -1 && fs.ASTRA_Data.Count > 0)
                {
                    LoadCases[CurrentLoadIndex].MemberLoads[r] = fs.ASTRA_Data[0];
                }
            }
            else if (btn.Name == btn_mload_del.Name)
            {
                tv_loads.SelectedNode.Parent.Nodes.Remove(tv_loads.SelectedNode);
            }
            #endregion Define Member Load


            #region Define Element Load
            else if (btn.Name == btn_eload_add.Name)
            {

                for (int i = 0; i < tn.Nodes.Count; i++)
                {
                    if (tn.Nodes[i].Text.StartsWith("ELEMENT"))
                    {
                        tn = tn.Nodes[i];
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {
                    tn.Nodes.Add("ELEMENT LOAD");
                    tn.ExpandAll();
                    tn = tn.Nodes[tn.Nodes.Count - 1];
                    tn.ExpandAll();
                }

                frmElementLoad fs = new frmElementLoad(ACad);

                if (tn != null)
                    fs.Node = tn;
                fs.Owner = this;
                fs.ShowDialog();

                foreach (var item in fs.ASTRA_Data)
                {
                    LoadCases[CurrentLoadIndex].ElementLoads.Add(item);
                    //tn.Nodes.Add(item);
                }
            }
            else if (btn.Name == btn_eload_edit.Name)
            {

                int r = LoadCases[CurrentLoadIndex].ElementLoads.IndexOf(tv_loads.SelectedNode.Text);
                frmElementLoad fs = new frmElementLoad(ACad);
                if (tn != null)
                    fs.Node = tv_loads.SelectedNode;
                fs.Owner = this;
                fs.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
                fs.ShowDialog();


                if (r != -1 && fs.ASTRA_Data.Count > 0)
                {
                    LoadCases[CurrentLoadIndex].ElementLoads[r] = fs.ASTRA_Data[0];
                }
            }
            else if (btn.Name == btn_eload_del.Name)
            {
                tv_loads.SelectedNode.Parent.Nodes.Remove(tv_loads.SelectedNode);
            }
            #endregion Define Member Load

            #region Define Area Load
            else if (btn.Name == btn_aload_add.Name)
            {
                for (int i = 0; i < tn.Nodes.Count; i++)
                {
                    if (tn.Nodes[i].Text.StartsWith("AREA"))
                    {
                        tn = tn.Nodes[i];
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {
                    tn.Nodes.Add("AREA LOAD");

                    tn.ExpandAll();
                    tn = tn.Nodes[tn.Nodes.Count - 1];
                }


                frmAreaLoad fs = new frmAreaLoad(ACad);
                if (tn != null)
                    fs.Node = tn;
                fs.Owner = this;
                fs.ShowDialog();


                foreach (var item in fs.ASTRA_Data)
                {
                    LoadCases[CurrentLoadIndex].AreaLoads.Add(item);
                }
            }
            else if (btn.Name == btn_aload_edit.Name)
            {

                int r = LoadCases[CurrentLoadIndex].AreaLoads.IndexOf(tv_loads.SelectedNode.Text);
                frmAreaLoad fs = new frmAreaLoad(ACad);
                if (tn != null)
                    fs.Node = tv_loads.SelectedNode;
                fs.Owner = this;
                fs.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
                fs.ShowDialog();

                if (r != -1 && fs.ASTRA_Data.Count > 0)
                {
                    LoadCases[CurrentLoadIndex].JointLoads[r] = fs.ASTRA_Data[0];
                }
            }
            else if (btn.Name == btn_aload_del.Name)
            {
                tv_loads.SelectedNode.Parent.Nodes.Remove(tv_loads.SelectedNode);
            }
            #endregion Define Area Load

            #region Define Floor Load
            else if (btn.Name == btn_fload_add.Name)
            {
                for (int i = 0; i < tn.Nodes.Count; i++)
                {
                    if (tn.Nodes[i].Text.StartsWith("FLOOR"))
                    {
                        tn = tn.Nodes[i];
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {
                    tn.Nodes.Add("FLOOR LOAD");

                    tn.ExpandAll();
                    tn = tn.Nodes[tn.Nodes.Count - 1];
                }


                frmFloorLoad fs = new frmFloorLoad(ACad);
                if (tn != null)
                    fs.Node = tn;
                fs.Owner = this;
                fs.ShowDialog();


                foreach (var item in fs.ASTRA_Data)
                {
                    LoadCases[CurrentLoadIndex].AreaLoads.Add(item);
                }
            }
            else if (btn.Name == btn_fload_edit.Name)
            {

                int r = LoadCases[CurrentLoadIndex].AreaLoads.IndexOf(tv_loads.SelectedNode.Text);
                frmFloorLoad fs = new frmFloorLoad(ACad);
                if (tn != null)
                    fs.Node = tv_loads.SelectedNode;
                fs.Owner = this;
                fs.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
                fs.ShowDialog();

                if (r != -1 && fs.ASTRA_Data.Count > 0)
                {
                    LoadCases[CurrentLoadIndex].JointLoads[r] = fs.ASTRA_Data[0];
                }
            }
            else if (btn.Name == btn_fload_del.Name)
            {
                tv_loads.SelectedNode.Parent.Nodes.Remove(tv_loads.SelectedNode);
            }
            #endregion Define Floor Load


            #region Define Temperature Load
            else if (btn.Name == btn_tload_add.Name)
            {
                for (int i = 0; i < tn.Nodes.Count; i++)
                {
                    if (tn.Nodes[i].Text.StartsWith("TEMP"))
                    {
                        tn = tn.Nodes[i];
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {
                    tn.Nodes.Add("TEMP LOAD");

                    tn.ExpandAll();
                    tn = tn.Nodes[tn.Nodes.Count - 1];
                }


                frmTempLoad fs = new frmTempLoad(ACad);
                if (tn != null)
                    fs.Node = tn;
                fs.Owner = this;
                fs.ShowDialog();

                foreach (var item in fs.ASTRA_Data)
                {
                    LoadCases[CurrentLoadIndex].TemperatureLoads.Add(item);
                    //tn.Nodes.Add(item);
                }

            }
            else if (btn.Name == btn_tload_edit.Name)
            {

                int r = LoadCases[CurrentLoadIndex].TemperatureLoads.IndexOf(tv_loads.SelectedNode.Text);
                frmTempLoad fs = new frmTempLoad(ACad);
                if (tn != null)
                    fs.Node = tv_loads.SelectedNode;
                fs.Owner = this;
                fs.ASTRA_Data.Add(tv_loads.SelectedNode.Text);
                fs.ShowDialog();

                if (r != -1 && fs.ASTRA_Data.Count > 0)
                {
                    LoadCases[CurrentLoadIndex].TemperatureLoads[r] = fs.ASTRA_Data[0];
                }
            }
            else if (btn.Name == btn_tload_del.Name)
            {
                tv_loads.SelectedNode.Parent.Nodes.Remove(tv_loads.SelectedNode);
            }
            #endregion Define Temperature Load

            #region Define Repeat Load
            else if (btn.Name == btn_rload_add.Name)
            {
                for (int i = 0; i < tn.Nodes.Count; i++)
                {
                    if (tn.Nodes[i].Text.StartsWith("REPEAT"))
                    {
                        tn = tn.Nodes[i];
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {
                    tn.Nodes.Add("REPEAT LOAD");

                    tn.ExpandAll();
                    tn = tn.Nodes[tn.Nodes.Count - 1];
                    tn.ExpandAll();
                }


                frmLoadCombination fs = new frmLoadCombination(ACad, LoadCases);
                fs.IsRepeatLoad = true;
                fs.txt_load_no.Text = (tv_loads.Nodes.Count + 1) + "";
                fs.Owner = this;
                fs.LDC.LoadNo = Current_LoadCase.LoadNo;
                fs.LDC.Name = Current_LoadCase.Title;
                fs.ShowDialog();

                if (fs.LDC.Count > 0)
                {
                    LoadCaseDefinition Ld = new LoadCaseDefinition();
                    Ld.LoadNo = fs.LDC.LoadNo;
                    Ld.Title = fs.LDC.Name;
                    Ld.RepeatLoads = fs.LDC;
                    LoadCases[CurrentLoadIndex].RepeatLoads = fs.LDC;

                    Ld.RepeatLoads.Set_Combination();
                    for (int i = 0; i < Ld.RepeatLoads.Count; i++)
                    {
                        tn.Nodes.Add(Ld.RepeatLoads[i]);
                    }
                }

            }
            else if (btn.Name == btn_rload_edit.Name)
            {

                frmLoadCombination fs = new frmLoadCombination(ACad, LoadCases);
                //fs.txt_load_no.Text = (tv_loadings.Nodes.Count + 1) + "";
                fs.Owner = this;
                fs.IsRepeatLoad = true;
                fs.LDC = Current_LoadCase.RepeatLoads;
                fs.ShowDialog();

                if (fs.LDC.Count > 0)
                {

                    tn = tv_loads.SelectedNode.Parent;


                    Current_LoadCase.RepeatLoads = fs.LDC;

                    tn.Nodes.Clear();

                    fs.LDC.Set_Combination();
                    for (int i = 0; i < fs.LDC.Count; i++)
                    {
                        tn.Nodes.Add(fs.LDC[i]);
                    }
                }
            }
            else if (btn.Name == btn_rload_del.Name)
            {
                try
                {
                    tv_loads.Nodes.RemoveAt(CurrentLoadIndex);
                    LoadCases.RemoveAt(CurrentLoadIndex);
                }
                catch (Exception ex) { }
            }
            #endregion Define Repeat Load

            #region Define Combination Load
            else if (btn.Name == btn_cload_add.Name)
            {

                frmLoadCombination fs = new frmLoadCombination(ACad, LoadCases);
                fs.txt_load_no.Text = (tv_loads.Nodes.Count + 1) + "";
                fs.Owner = this;
                fs.ShowDialog();

                if (fs.LDC.Count > 0)
                {
                    LoadCaseDefinition Ld = new LoadCaseDefinition();
                    Ld.LoadNo = fs.LDC.LoadNo;
                    Ld.Title = fs.LDC.Name;
                    Ld.Comb_Loads = fs.LDC;

                    LoadCases.Add(Ld);

                    tv_loads.Nodes.Add(Ld.LoadNo + " : " + Ld.Title);

                    Ld.Comb_Loads.Set_Combination();
                    for (int i = 0; i < Ld.Comb_Loads.Count; i++)
                    {
                        tv_loads.Nodes[tv_loads.Nodes.Count - 1].Nodes.Add(Ld.Comb_Loads[i]);
                    }
                }

            }
            else if (btn.Name == btn_cload_edit.Name)
            {

                frmLoadCombination fs = new frmLoadCombination(ACad, LoadCases);
                //fs.txt_load_no.Text = (tv_loadings.Nodes.Count + 1) + "";
                fs.Owner = this;
                fs.LDC = Current_LoadCase.Comb_Loads;
                fs.ShowDialog();

                if (fs.LDC.Count > 0)
                {
                    LoadCaseDefinition Ld = new LoadCaseDefinition();
                    Ld.LoadNo = fs.LDC.LoadNo;
                    Ld.Title = fs.LDC.Name;
                    Ld.Comb_Loads = fs.LDC;

                    Current_LoadCase = (Ld);



                    tv_loads.Nodes[CurrentLoadIndex].Text = (Ld.LoadNo + " : " + Ld.Title);

                    tv_loads.Nodes[CurrentLoadIndex].Nodes.Clear();
                    Ld.Comb_Loads.Set_Combination();
                    for (int i = 0; i < Ld.Comb_Loads.Count; i++)
                    {
                        tv_loads.Nodes[CurrentLoadIndex].Nodes.Add(Ld.Comb_Loads[i]);
                    }
                }
            }
            else if (btn.Name == btn_cload_del.Name)
            {
                try
                {
                    tv_loads.Nodes.RemoveAt(CurrentLoadIndex);
                    LoadCases.RemoveAt(CurrentLoadIndex);
                }
                catch (Exception ex) { }
            }
            #endregion Define Combination Load

            grb_cload.Enabled = (tv_loads.Nodes.Count > 0);

            grb_jload.Enabled = (tv_loads.Nodes.Count > 0);
            grb_mload.Enabled = (tv_loads.Nodes.Count > 0);
            grb_aload.Enabled = (tv_loads.Nodes.Count > 0);
            grb_tload.Enabled = (tv_loads.Nodes.Count > 0);
            grb_rload.Enabled = (tv_loads.Nodes.Count > 0);

            Button_Enable_Disable();

        }

        private void rbtn_perform_eigen_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbtn = sender as RadioButton;
            txt_frequencies.Enabled = rbtn_perform_eigen.Checked;
            grb_dynamic.Enabled = !rbtn_perform_eigen.Checked;
        }

        private void chk_print_max_force_CheckedChanged(object sender, EventArgs e)
        {
            grb_max_frc.Enabled = chk_print_max_force.Checked;
        }

        private void btn_dyn_add_Click(object sender, EventArgs e)
        {

            if (rbtn_perform_time_history.Checked)
            {
                frmTimeHistory fs = new frmTimeHistory(ACad, true);
                fs.Owner = this;
                fs.ShowDialog();

                txt_dynamic.Lines = fs.ASTRA_Data.ToArray();
            }
            else if (rbtn_response_spectrum.Checked)
            {
                frmResponse fs = new frmResponse(ACad);
                fs.Owner = this;
                //fs.ShowDialog();
                if (fs.ShowDialog() != DialogResult.Cancel)
                    txt_dynamic.Lines = fs.ASTRA_Data.ToArray();
            }
        }

        private void btn_dyn_del_Click(object sender, EventArgs e)
        {
            txt_dynamic.Text = "";
        }
        private void btn_file_open_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                #region Drawing Open
                //if (btn.Name == btn_file_open.Name)
                //{
                //    ofd.Filter = "All Drawing Files (*.vdml,*.dxf,*.dwg)|*.vdml;*.dxf;*.dwg";
                //    if (ofd.ShowDialog() != DialogResult.Cancel)
                //    {
                //        txt_file_name.Text = ofd.FileName;

                //        Drawing_File = txt_file_name.Text;

                //        string fn = Path.Combine(Path.GetDirectoryName(Drawing_File), Path.GetFileNameWithoutExtension(Drawing_File) + ".txt");

                //        ASTRADoc astdoc;
                //        if (File.Exists(fn))
                //        {
                //            astdoc = new ASTRADoc(fn);
                //            ACad.AstraDocument = astdoc;

                //            AST_DOC.Members.DrawMember(VDoc);
                //            SetGridWithNode();
                //            SetGridWithMember();
                //        }
                //        else
                //            Open_Drawing_File();

                //    }
                //}
                #endregion Drawing Open
                #region Data Open
                //else if (btn.Name == btn_data_open.Name)
                //{
                //    ofd.Filter = "ASTRA Data Files (*.txt)|*.txt";
                //    if (ofd.ShowDialog() != DialogResult.Cancel)
                //    {
                //        txt_file_name.Text = ofd.FileName;

                //        Drawing_File = txt_file_name.Text;

                //        string fn = Path.Combine(Path.GetDirectoryName(Drawing_File), Path.GetFileNameWithoutExtension(Drawing_File) + ".txt");

                //        ASTRADoc astdoc;
                //        if (File.Exists(fn))
                //        {
                //            astdoc = new ASTRADoc(fn);
                //            ACad.AstraDocument = astdoc;

                //            AST_DOC.Members.DrawMember(VDoc);
                //            AST_DOC.Elements.DrawElements(VDoc);
                //            AST_DOC.Supports.DrawSupport(VDoc);

                //            chk_dynamic_analysis.Checked = false;
                //            rbtn_perform_eigen.Checked = true;
                //            txt_dynamic.Text = "";

                //            cmb_structure_type.SelectedItem = AST_DOC.StructureType;
                //            txtUserTitle.Text = AST_DOC.UserTitle;


                //            cmb_Base_MUnit.SelectedIndex = (int)AST_DOC.Base_MassUnit;
                //            cmb_Base_LUnit.SelectedIndex = (int)AST_DOC.Base_LengthUnit;


                //            cmb_Prop_LUnit.SelectedIndex = (int)AST_DOC.Prop_LengthUnit;
                //            cmb_Load_MUnit.SelectedIndex = (int)AST_DOC.Load_MassUnit;
                //            cmb_Load_LUnit.SelectedIndex = (int)AST_DOC.Load_LengthUnit;




                //            SetGridWithNode();
                //            SetGridWithMember();
                //            //SetGridWithElement();

                //            Groups.Clear();
                //            tv_mem_grps.Nodes.Clear();
                //            foreach (var item in AST_DOC.MemberGroups)
                //            {
                //                tv_mem_grps.Nodes.Add(item);

                //                MemberGroup mg = new MemberGroup();
                //                MyStrings ml = new MyStrings(item, ' ');

                //                mg.GroupName = ml.StringList[0];
                //                mg.MemberNosText = ml.GetString(1);

                //                Groups.Add(mg);
                //            }

                //            tv_mem_props.Nodes.Clear();
                //            foreach (var item in AST_DOC.MemberProps)
                //            {
                //                tv_mem_props.Nodes.Add(item);
                //            }
                //            tv_mem_spec_cable.Nodes.Clear();
                //            foreach (var item in AST_DOC.MemberCables)
                //            {
                //                tv_mem_spec_cable.Nodes.Add(item);
                //            }
                //            tv_mem_spec_truss.Nodes.Clear();
                //            foreach (var item in AST_DOC.MemberTrusses)
                //            {
                //                tv_mem_spec_truss.Nodes.Add(item);
                //            }

                //            tv_constants.Nodes.Clear();
                //            Materials.Clear();
                //            for (int i = 0; i < AST_DOC.Constants.Count; i++)
                //            {
                //                tv_constants.Nodes.Add("MATERIAL_" + (i + 1));
                //                foreach (var item in AST_DOC.Constants[i].ASTRA_Data)
                //                {
                //                    tv_constants.Nodes[i].Nodes.Add(item);
                //                }
                //                Materials.Add(AST_DOC.Constants[i]);
                //            }

                //            tv_supports.Nodes.Clear();
                //            foreach (var item in AST_DOC.JointSupports)
                //            {
                //                tv_supports.Nodes.Add(item);
                //            }

                //            if (AST_DOC.SelfWeight != "")
                //            {
                //                chk_selfweight.Checked = true;
                //                cmb_Self_Weight.SelectedItem = AST_DOC.SelfWeight_Direction;
                //                txt_seft_wt_Value.Text = AST_DOC.SelfWeight;
                //            }
                //            tv_loads.Nodes.Clear();
                //            LoadCases.Clear();
                //            foreach (var item in AST_DOC.LoadDefines)
                //            {
                //                LoadCaseDefinition lcd = new LoadCaseDefinition();
                //                lcd.LoadNo = item.LoadCase;
                //                lcd.Title = item.LoadTitle;
                //                lcd.JointLoads = item.JointLoadList;
                //                lcd.MemberLoads = item.MemberLoadList;
                //                lcd.ElementLoads = item.ElementLoadList;
                //                lcd.AreaLoads = item.AreaLoadList;
                //                lcd.TemperatureLoads = item.TempLoadList;


                //                if (item.CominationLoadList.Count > 0)
                //                {
                //                    item.Set_Combination();
                //                    lcd.Comb_Loads.LoadNo = item.LoadCase;
                //                    lcd.Comb_Loads.Name = item.LoadTitle;
                //                    foreach (var cm in item.Combinations)
                //                    {
                //                        lcd.Comb_Loads.LoadCases.Add(cm.Load_No);
                //                        lcd.Comb_Loads.Factors.Add(cm.Load_Factor);
                //                    }
                //                    lcd.Comb_Loads.Set_Combination();
                //                }

                //                if (item.RepeatLoadList.Count > 0)
                //                {
                //                    item.Set_Combination();
                //                    lcd.RepeatLoads.LoadNo = item.LoadCase;
                //                    lcd.RepeatLoads.Name = item.LoadTitle;

                //                    foreach (var cm in item.Combinations)
                //                    {
                //                        lcd.RepeatLoads.LoadCases.Add(cm.Load_No);
                //                        lcd.RepeatLoads.Factors.Add(cm.Load_Factor);
                //                    }
                //                    lcd.RepeatLoads.Set_Combination();
                //                }
                //                //lcd.Comb_Loads = item.CominationLoadList;
                //                //lcd.RepeatLoads = item.RepeatLoadList;



                //                LoadCases.Add(lcd);
                //            }
                //            #region   LoadCases
                //            if (LoadCases.Count > 0)
                //            {
                //                TreeNode tn = null;
                //                TreeNode tjn = null;
                //                tv_loads.Nodes.Clear();
                //                foreach (var item in LoadCases)
                //                {
                //                    tv_loads.Nodes.Add(item.ToString());

                //                    tn = tv_loads.Nodes[tv_loads.Nodes.Count - 1];


                //                    if (item.RepeatLoads.Count > 0)
                //                    {
                //                        //item.Set_Combination();
                //                        tn.Nodes.Add("REPEAT LOAD");
                //                        tn = tn.Nodes[tn.Nodes.Count - 1];
                //                        foreach (var item1 in item.RepeatLoads)
                //                        {
                //                            tn.Nodes.Add(item1.ToString());
                //                        }
                //                        tn = tn.Parent;
                //                    }
                //                    if (item.MemberLoads.Count > 0)
                //                    {
                //                        tn.Nodes.Add("MEMBER LOAD");
                //                        tn = tn.Nodes[tn.Nodes.Count - 1];
                //                        foreach (var item1 in item.MemberLoads)
                //                        {
                //                            tn.Nodes.Add(item1);
                //                        }
                //                        tn = tn.Parent;

                //                    }
                //                    if (item.ElementLoads.Count > 0)
                //                    {
                //                        tn.Nodes.Add("ELEMENT LOAD");
                //                        tn = tn.Nodes[tn.Nodes.Count - 1];
                //                        foreach (var item1 in item.ElementLoads)
                //                        {
                //                            tn.Nodes.Add(item1);
                //                        }
                //                        tn = tn.Parent;

                //                    }
                //                    if (item.JointLoads.Count > 0)
                //                    {

                //                        tn.Nodes.Add("JOINT LOAD");
                //                        tn = tn.Nodes[tn.Nodes.Count - 1];
                //                        foreach (var item1 in item.JointLoads)
                //                        {
                //                            tn.Nodes.Add(item1);
                //                        }
                //                        tn = tn.Parent;
                //                    }
                //                    if (item.AreaLoads.Count > 0)
                //                    {

                //                        tn.Nodes.Add("AREA LOAD");
                //                        tn = tn.Nodes[tn.Nodes.Count - 1];
                //                        foreach (var item1 in item.AreaLoads)
                //                        {
                //                            tn.Nodes.Add(item1);
                //                        }
                //                        tn = tn.Parent;
                //                    }
                //                    if (item.TemperatureLoads.Count > 0)
                //                    {

                //                        tn.Nodes.Add("TEMP LOAD");
                //                        tn = tn.Nodes[tn.Nodes.Count - 1];
                //                        foreach (var item1 in item.TemperatureLoads)
                //                        {
                //                            tn.Nodes.Add(item1);
                //                        }
                //                        tn = tn.Parent;
                //                    }
                //                    if (item.Comb_Loads.Count > 0)
                //                    {
                //                        //item.Set_Combination();
                //                        //tn.Nodes.Add("COMBINATION");
                //                        //if (tn.Nodes.Count > 0)
                //                        //    tn = tn.Nodes[tn.Nodes.Count - 1];
                //                        foreach (var item1 in item.Comb_Loads)
                //                        {
                //                            tn.Nodes.Add(item1.ToString());
                //                        }
                //                        //tn = tn.Parent;
                //                    }
                //                    //if (item.ElementLoad.Count > 0)
                //                    //{
                //                    //    tn.Nodes.Add("ELEMENT LOAD");
                //                    //    tn = tn.Nodes[0];
                //                    //    foreach (var item1 in item.ElementLoadList)
                //                    //    {
                //                    //        //tn.Nodes.Add(item1 + " " + astDoc.MassUnit + "/" + astDoc.LengthUnit);
                //                    //        tn.Nodes.Add(item1);
                //                    //    }
                //                    //    tn = tn.Parent;
                //                    //}
                //                }
                //            }
                //            #endregion

                //            #region Moving Load
                //            if (AST_DOC.MovingLoads.Count > 0)
                //            {
                //                TreeNode tn;
                //                tv_mov_loads.Nodes.Clear();
                //                MovingLoads.Clear();

                //                foreach (var item in AST_DOC.MovingLoads)
                //                {
                //                    MovingLoads.Add(item);
                //                    tn = new TreeNode(item.ToString());
                //                    tv_mov_loads.Nodes.Add(tn);
                //                    tn.Nodes.Add("LOADS : " + item.Loads);
                //                    tn.Nodes.Add("DISTANCES : " + item.Distances);
                //                    tn.Nodes.Add("LOAD WIDTH : " + item.LoadWidth);
                //                }


                //            }
                //            if (AST_DOC.LL_Definition.Count > 0)
                //            {


                //                TreeNode tn;
                //                tv_mov_def_load.Nodes.Clear();
                //                LL_Definition.Clear();

                //                foreach (var item in AST_DOC.LL_Definition)
                //                {

                //                    LL_Definition.Add(item);

                //                    tn = new TreeNode(item.ToString());

                //                    tv_mov_def_load.Nodes.Add(tn);
                //                    tn.Nodes.Add("X-Distance : " + item.X_Distance);
                //                    tn.Nodes.Add("Y-Distance : " + item.Y_Distance);
                //                    tn.Nodes.Add("Z-Distance : " + item.Z_Distance);
                //                    if (item.X_Increment != 0.0)
                //                        tn.Nodes.Add("X_Increment : " + item.X_Increment);
                //                    if (item.Y_Increment != 0.0)
                //                        tn.Nodes.Add("Y_Increment : " + item.Y_Increment);
                //                    if (item.Z_Increment != 0.0)
                //                        tn.Nodes.Add("Z_Increment : " + item.Z_Increment);
                //                    tn.Nodes.Add("Impact_Factor : " + item.Impact_Factor);

                //                }
                //            }
                //            #endregion Moving Load
                //            chk_perform_ana.Checked = AST_DOC.Analysis_Specification.Perform_Analysis;
                //            chk_print_ana_all.Checked = AST_DOC.Analysis_Specification.Print_Analysis_All;
                //            chk_print_load_data.Checked = AST_DOC.Analysis_Specification.Print_Load_Data;
                //            chk_print_static_check.Checked = AST_DOC.Analysis_Specification.Print_Static_Check;
                //            chk_print_supp_reac.Checked = AST_DOC.Analysis_Specification.Print_Support_Reaction;
                //            chk_print_max_force.Checked = AST_DOC.Analysis_Specification.Print_Max_Force;
                //            txt_max_frc_list.Enabled = true;
                //            tv_max_frc.Nodes.Clear();
                //            foreach (var item in AST_DOC.Analysis_Specification.List_Maxforce)
                //            {
                //                tv_max_frc.Nodes.Add(item);
                //            }

                //            if (AST_DOC.DynamicAnalysis.Count > 0)
                //            {
                //                chk_dynamic_analysis.Checked = true;

                //                if (AST_DOC.DynamicAnalysis[0].StartsWith("PERFORM EIGEN"))
                //                {
                //                    rbtn_perform_eigen.Checked = true;
                //                    txt_frequencies.Text = AST_DOC.FREQUENCIES;
                //                }
                //                else if (AST_DOC.DynamicAnalysis[0].StartsWith("PERFORM TIME HISTORY"))
                //                {
                //                    rbtn_perform_time_history.Checked = true;
                //                    txt_dynamic.Lines = AST_DOC.DynamicAnalysis.ToArray();
                //                }
                //                else if (AST_DOC.DynamicAnalysis[0].StartsWith("PERFORM RESPON"))
                //                {
                //                    txt_dynamic.Lines = AST_DOC.DynamicAnalysis.ToArray();
                //                    rbtn_response_spectrum.Checked = true;
                //                }

                //            }
                //            //AST_DOC.DynamicAnalysis
                //        }
                //    }
                //}
                #endregion Data Open
            }

            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(VDoc);
        }

        private void btn_file_save_Click(object sender, EventArgs e)
        {
            if (File.Exists(Drawing_File))
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    //sfd.Filter = "All Drawing Files (*.vdml,*.dxf,*.dwg)|*.vdml;*.dxf;*.dwg";
                    sfd.Filter = "VDML Files (*.vdml)|*.vdml|DXF Files (*.dxf)|*.dxf|DWG Files (*.dwg)|*.dwg";

                    if (sfd.ShowDialog() != DialogResult.Cancel)
                    {
                        if (VDoc.SaveAs(sfd.FileName))
                        {
                            MessageBox.Show("File saved successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private void chk_dynamic_analysis_CheckedChanged(object sender, EventArgs e)
        {
            grb_dyna_analysis.Enabled = chk_dynamic_analysis.Checked;

            //Mem
        }

        private void btn_mov_load_add_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            TreeNode tn = tv_mov_loads.SelectedNode;


            int type = 0;
            int indx = -1;
            MovingLoadData MLD = null;
            if (tn != null)
            {
                while (tn.Parent != null) tn = tn.Parent;


                MyStrings mlist = new MyStrings(tn.Text, ' ');


                type = mlist.GetInt(1);


                for (int i = 0; i < MovingLoads.Count; i++)
                {
                    if (MovingLoads[i].Type == type)
                    {
                        indx = i;
                        MLD = MovingLoads[i];
                        break;
                    }
                }
            }


            ACad.MassUnit = (EMassUnits)cmb_Base_MUnit.SelectedIndex;
            #region Define Load Case
            if (btn.Name == btn_mov_load_add.Name)
            {
                frmDefineMovingLoad fs = new frmDefineMovingLoad(ACad);
                //fs.txt_load_case.Text = (tv_loadings.Nodes.Count + 1) + "";
                fs.txt_type.Text = (tv_mov_loads.Nodes.Count + 1) + "";
                fs.Owner = this;
                fs.ShowDialog();

                if (fs.MLD != null)
                {
                    MovingLoads.Add(fs.MLD);
                    tn = new TreeNode(fs.MLD.ToString());

                    tv_mov_loads.Nodes.Add(tn);
                    tn.Nodes.Add("LOADS : " + fs.MLD.Loads);
                    tn.Nodes.Add("DISTANCES : " + fs.MLD.Distances);
                    tn.Nodes.Add("LOAD WIDTH : " + fs.MLD.LoadWidth);
                }
            }
            else if (btn.Name == btn_mov_load_edit.Name)
            {


                frmDefineMovingLoad fs = new frmDefineMovingLoad(ACad);
                //fs.txt_load_case.Text = (tv_loadings.Nodes.Count + 1) + "";
                //fs.txt_type.Text = (tv_mov_loads.Nodes.Count + 1) + "";
                fs.Owner = this;
                fs.MLD = MLD;
                fs.ShowDialog();

                if (fs.MLD != null)
                {
                    MovingLoads[indx] = (fs.MLD);

                    tv_mov_loads.Nodes[indx].Text = fs.MLD.ToString();
                    tv_mov_loads.Nodes[indx].Nodes.Clear();
                    tv_mov_loads.Nodes[indx].Nodes.Add("LOADS : " + fs.MLD.Loads);
                    tv_mov_loads.Nodes[indx].Nodes.Add("DISTANCES : " + fs.MLD.Distances);
                    tv_mov_loads.Nodes[indx].Nodes.Add("LOAD WIDTH : " + fs.MLD.LoadWidth);
                }

            }
            else if (btn.Name == btn_mov_load_del.Name)
            {
                try
                {

                    MovingLoads.RemoveAt(indx);

                    tv_mov_loads.Nodes.Remove(tn);
                }
                catch (Exception ex) { }
            }
            #endregion Define Load Case
        }
        string Load_Type = "";


        private void tv_loadings_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Load_Selection(e.Node);
            Draw_Loadings();
        }

        private void Load_Selection(TreeNode tn)
        {
            //tv_loadings.SelectedNode
            Load_Type = "";
            if (tn.Nodes.Count == 0)
            {
                if (tn.Parent != null)
                {
                    if (tn.Parent.Text.StartsWith("JOINT"))
                        Load_Type = "JOINT";
                    if (tn.Parent.Text.StartsWith("MEMBER"))
                        Load_Type = "MEMBER";
                    if (tn.Parent.Text.StartsWith("AREA"))
                        Load_Type = "AREA";
                    if (tn.Parent.Text.StartsWith("FLOOR"))
                        Load_Type = "FLOOR";
                    if (tn.Parent.Text.StartsWith("TEMP"))
                        Load_Type = "TEMP";
                    if (tn.Parent.Text.StartsWith("REPEAT"))
                        Load_Type = "REPEAT";
                    if (tn.Parent.Text.StartsWith("ELEM"))
                        Load_Type = "ELEMENT";
                    if (tn.Parent.Text.StartsWith("SUPPORT"))
                        Load_Type = "SUPPORT";
                }

            }

            if (tn.Parent == null)
            {
                grb_jload.Enabled = true;
                grb_mload.Enabled = true;
                grb_eload.Enabled = true;
                grb_aload.Enabled = true;
                grb_fload.Enabled = true;
                grb_tload.Enabled = true;
                grb_cload.Enabled = true;
                grb_sdload.Enabled = true;


            }
            else
            {
                if (tn.Parent.Text.StartsWith("JOINT") ||
                    tn.Parent.Text.StartsWith("MEMBER") ||
                    tn.Parent.Text.StartsWith("ELEMENT") ||
                    tn.Parent.Text.StartsWith("AREA") ||
                    tn.Parent.Text.StartsWith("FLOOR") ||
                    tn.Parent.Text.StartsWith("TEMP") ||
                    tn.Parent.Text.StartsWith("SUPPORT") ||
                    tn.Parent.Text.StartsWith("REPEAT"))
                    tn = tn.Parent;


                grb_jload.Enabled = false;
                grb_mload.Enabled = false;
                grb_eload.Enabled = false;
                grb_aload.Enabled = false;
                grb_fload.Enabled = false;
                grb_tload.Enabled = false;
                grb_rload.Enabled = false;
                grb_sdload.Enabled = false;

                if (tn.Text.StartsWith("JOINT"))
                {
                    grb_jload.Enabled = true;
                    //grb_mload.Enabled = false;
                    //grb_aload.Enabled = false;
                    //grb_tload.Enabled = false;
                }
                else if (tn.Text.StartsWith("MEMBER"))
                {
                    //grb_jload.Enabled = false;
                    grb_mload.Enabled = true;
                    //grb_aload.Enabled = false;
                    //grb_tload.Enabled = false;
                }
                else if (tn.Text.StartsWith("SUPPORT"))
                {
                    //grb_jload.Enabled = false;
                    grb_sdload.Enabled = true;
                    //grb_aload.Enabled = false;
                    //grb_tload.Enabled = false;
                }
                else if (tn.Text.StartsWith("ELEMENT"))
                {
                    //grb_jload.Enabled = false;
                    grb_eload.Enabled = true;
                    //grb_aload.Enabled = false;
                    //grb_tload.Enabled = false;
                }
                else if (tn.Text.StartsWith("AREA"))
                {
                    //grb_jload.Enabled = false;
                    //grb_mload.Enabled = false;
                    grb_aload.Enabled = true;
                    //grb_tload.Enabled = false;
                }
                else if (tn.Text.StartsWith("FLOOR"))
                {
                    //grb_jload.Enabled = false;
                    //grb_mload.Enabled = false;
                    grb_fload.Enabled = true;
                    //grb_tload.Enabled = false;
                }
                else if (tn.Text.StartsWith("TEMP"))
                {
                    //grb_jload.Enabled = false;
                    //grb_mload.Enabled = false;
                    //grb_aload.Enabled = false;
                    grb_tload.Enabled = true;
                }
                else if (tn.Text.StartsWith("REP"))
                {
                    //grb_jload.Enabled = false;
                    //grb_mload.Enabled = false;
                    //grb_aload.Enabled = false;
                    grb_rload.Enabled = true;
                }

            }


            while (tn.Parent != null)
            {
                tn = tn.Parent;
            }

            MyStrings mlist = new MyStrings(tn.Text, ':');

            if (mlist.Count > 0)
            {
                //CurrentLoadIndex = mlist.GetInt(0) - 1;
                CurrentLoadIndex = tn.Index;
            }


            grb_jload.Enabled = !LoadCases[CurrentLoadIndex].Is_Load_Combination;
            grb_mload.Enabled = !LoadCases[CurrentLoadIndex].Is_Load_Combination;
            grb_eload.Enabled = !LoadCases[CurrentLoadIndex].Is_Load_Combination;
            grb_aload.Enabled = !LoadCases[CurrentLoadIndex].Is_Load_Combination;
            grb_fload.Enabled = !LoadCases[CurrentLoadIndex].Is_Load_Combination;
            grb_tload.Enabled = !LoadCases[CurrentLoadIndex].Is_Load_Combination;
            grb_rload.Enabled = !LoadCases[CurrentLoadIndex].Is_Load_Combination;
            grb_sdload.Enabled = !LoadCases[CurrentLoadIndex].Is_Load_Combination;


            //btn_jload_edit.Enabled = (Current_LoadCase.JointLoads.Count > 0);
            //btn_jload_del.Enabled = (Current_LoadCase.JointLoads.Count > 0);

            //btn_mload_edit.Enabled = (Current_LoadCase.MemberLoads.Count > 0);
            //btn_mload_del.Enabled = (Current_LoadCase.MemberLoads.Count > 0);

            //btn_aload_edit.Enabled = (Current_LoadCase.AreaLoads.Count > 0);
            //btn_aload_del.Enabled = (Current_LoadCase.AreaLoads.Count > 0);

            //btn_tload_edit.Enabled = (Current_LoadCase.TemperatureLoads.Count > 0);
            //btn_tload_del.Enabled = (Current_LoadCase.TemperatureLoads.Count > 0);


            btn_cload_edit.Enabled = (Current_LoadCase.Comb_Loads.Count > 0);
            btn_cload_del.Enabled = (Current_LoadCase.Comb_Loads.Count > 0);

            if (Load_Type == "")
            {
                btn_jload_edit.Enabled = false;
                btn_jload_del.Enabled = false;
                btn_mload_edit.Enabled = false;
                btn_mload_del.Enabled = false;
                btn_eload_edit.Enabled = false;
                btn_eload_del.Enabled = false;
                btn_aload_edit.Enabled = false;
                btn_aload_del.Enabled = false;
                btn_fload_edit.Enabled = false;
                btn_fload_del.Enabled = false;
                btn_tload_edit.Enabled = false;
                btn_tload_del.Enabled = false;

                btn_rload_edit.Enabled = false;
                btn_rload_del.Enabled = false;

                btn_sdload_edit.Enabled = false;
                btn_sdload_del.Enabled = false;



                btn_jload_add.Enabled = true;
                btn_mload_add.Enabled = true;
                btn_eload_add.Enabled = true;
                btn_aload_add.Enabled = true;
                btn_fload_add.Enabled = true;
                btn_tload_add.Enabled = true;
                btn_cload_add.Enabled = true;
                btn_rload_add.Enabled = true;

                btn_sdload_add.Enabled = true;



            }

            if (Load_Type == "JOINT")
            {

                btn_jload_edit.Enabled = true;
                btn_jload_del.Enabled = true;

                grb_jload.Enabled = true;
                grb_mload.Enabled = false;
                grb_eload.Enabled = false;
                grb_aload.Enabled = false;
                grb_fload.Enabled = false;
                grb_tload.Enabled = false;
                grb_rload.Enabled = false;
                grb_sdload.Enabled = false;
            }
            else if (Load_Type == "MEMBER")
            {

                btn_mload_edit.Enabled = true;
                btn_mload_del.Enabled = true;

                grb_jload.Enabled = false;
                grb_mload.Enabled = true;
                grb_eload.Enabled = false;
                grb_fload.Enabled = false;
                grb_aload.Enabled = false;
                grb_tload.Enabled = false;
                grb_rload.Enabled = false;
                grb_sdload.Enabled = false;
            }
            else if (Load_Type == "ELEMENT")
            {

                btn_eload_edit.Enabled = true;
                btn_eload_del.Enabled = true;

                grb_jload.Enabled = false;
                grb_mload.Enabled = false;
                grb_eload.Enabled = true;
                grb_aload.Enabled = false;
                grb_fload.Enabled = false;
                grb_tload.Enabled = false;
                grb_rload.Enabled = false;
                grb_sdload.Enabled = false;
            }
            else if (Load_Type == "AREA")
            {

                btn_aload_edit.Enabled = true;
                btn_aload_del.Enabled = true;

                grb_jload.Enabled = false;
                grb_mload.Enabled = false;
                grb_eload.Enabled = false;
                grb_fload.Enabled = false;
                grb_aload.Enabled = true;
                grb_tload.Enabled = false;
                grb_rload.Enabled = false;
                grb_sdload.Enabled = false;
            }
            else if (Load_Type == "FLOOR")
            {

                btn_fload_edit.Enabled = true;
                btn_fload_del.Enabled = true;

                grb_jload.Enabled = false;
                grb_mload.Enabled = false;
                grb_eload.Enabled = false;
                grb_fload.Enabled = false;
                grb_fload.Enabled = true;
                grb_aload.Enabled = false;
                grb_tload.Enabled = false;
                grb_rload.Enabled = false;
                grb_sdload.Enabled = false;
            }
            else if (Load_Type == "TEMP")
            {

                btn_tload_edit.Enabled = true;
                btn_tload_del.Enabled = true;

                grb_jload.Enabled = false;
                grb_mload.Enabled = false;
                grb_eload.Enabled = false;
                grb_aload.Enabled = false;
                grb_fload.Enabled = false;
                grb_tload.Enabled = true;
                grb_rload.Enabled = false;
                grb_sdload.Enabled = false;
            }
            else if (Load_Type == "REPEAT")
            {

                btn_rload_edit.Enabled = true;
                btn_rload_del.Enabled = true;

                grb_jload.Enabled = false;
                grb_mload.Enabled = false;
                grb_eload.Enabled = false;
                grb_aload.Enabled = false;
                grb_fload.Enabled = false;
                grb_tload.Enabled = false;
                grb_rload.Enabled = true;
                grb_sdload.Enabled = false;
            }
            else if (Load_Type == "SUPPORT")
            {

                btn_sdload_edit.Enabled = true;
                btn_sdload_del.Enabled = true;

                grb_jload.Enabled = false;
                grb_mload.Enabled = false;
                grb_eload.Enabled = false;
                grb_aload.Enabled = false;
                grb_fload.Enabled = false;
                grb_tload.Enabled = false;
                grb_rload.Enabled = false;
                grb_sdload.Enabled = true;
            }
        }

        private void frm_DrawingToData_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void btn_mov_def_load_add_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;

            TreeNode tn = tv_mov_def_load.SelectedNode;


            int type = 0;
            int indx = -1;
            LiveLoad LL = null;

            if (tn != null)
            {
                while (tn.Parent != null) tn = tn.Parent;
                LL = LL_Definition[tn.Index];
                indx = tn.Index;
            }







            #region Define Load Case
            if (btn.Name == btn_mov_def_load_add.Name)
            {
                frmLiveLoad fs = new frmLiveLoad(ACad, MovingLoads);
                //fs.txt_load_case.Text = (tv_loadings.Nodes.Count + 1) + "";
                //fs.txt_type.Text = (tv_mov_loads.Nodes.Count + 1) + "";
                fs.Owner = this;
                fs.ShowDialog();

                if (fs.LLD != null)
                {
                    tn = new TreeNode(fs.LLD.ToString());

                    LL_Definition.Add(fs.LLD);
                    tv_mov_def_load.Nodes.Add(tn);
                    tn.Nodes.Add("X-Distance : " + fs.LLD.X_Distance);
                    tn.Nodes.Add("Y-Distance : " + fs.LLD.Y_Distance);
                    tn.Nodes.Add("Z-Distance : " + fs.LLD.Z_Distance);
                    if (fs.LLD.X_Increment != 0.0)
                        tn.Nodes.Add("X_Increment : " + fs.LLD.X_Increment);
                    if (fs.LLD.Y_Increment != 0.0)
                        tn.Nodes.Add("Y_Increment : " + fs.LLD.Y_Increment);
                    if (fs.LLD.Z_Increment != 0.0)
                        tn.Nodes.Add("Z_Increment : " + fs.LLD.Z_Increment);
                    tn.Nodes.Add("Impact_Factor : " + fs.LLD.Impact_Factor);
                }
            }
            else if (btn.Name == btn_mov_def_load_edit.Name)
            {

                frmLiveLoad fs = new frmLiveLoad(ACad, MovingLoads);
                //fs.txt_load_case.Text = (tv_loadings.Nodes.Count + 1) + "";
                fs.LLD = LL;
                fs.Owner = this;
                fs.ShowDialog();

                if (fs.LLD != null)
                {
                    tn = tv_mov_def_load.Nodes[indx];

                    LL_Definition[indx] = (fs.LLD);
                    tn.Text = fs.LLD.ToString();

                    tn.Nodes.Clear();

                    tn.Nodes.Add("X-Distance : " + fs.LLD.X_Distance);
                    tn.Nodes.Add("Y-Distance : " + fs.LLD.Y_Distance);
                    tn.Nodes.Add("Z-Distance : " + fs.LLD.Z_Distance);
                    if (fs.LLD.X_Increment != 0.0)
                        tn.Nodes.Add("X_Increment : " + fs.LLD.X_Increment);
                    if (fs.LLD.Y_Increment != 0.0)
                        tn.Nodes.Add("Y_Increment : " + fs.LLD.Y_Increment);
                    if (fs.LLD.Z_Increment != 0.0)
                        tn.Nodes.Add("Z_Increment : " + fs.LLD.Z_Increment);
                    tn.Nodes.Add("Impact_Factor : " + fs.LLD.Impact_Factor);
                }
            }
            else if (btn.Name == btn_mov_def_load_del.Name)
            {
                if (indx != -1)
                    tv_mov_def_load.Nodes.RemoveAt(indx);
            }
            #endregion Define Load Case
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            Save_Data();
        }

        private void Save_Data()
        {

            //string fname = Path.Combine(Path.GetDirectoryName(txt_file_name.Text),
            //               Path.GetFileNameWithoutExtension(txt_file_name.Text) + ".TXT");
            string fname = "";

            //if (IsDrawingFileOpen)


            if (!File.Exists(File_Name))
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "TEXT Files (*.txt)|*.txt";
                    if (sfd.ShowDialog() != DialogResult.Cancel)
                        File_Name = sfd.FileName;
                    else
                        return;
                }
            }

            fname = File_Name;



            //using (SaveFileDialog sfd = new SaveFileDialog())
            //{
            //    sfd.Filter = "TEXT Files (*.txt)|*.txt";
            //    if (sfd.ShowDialog() != DialogResult.Cancel)
            //    {
            //        fname = sfd.FileName;
            //    }
            //    else
            //        return;
            //}


            List<string> list = new List<string>();
            string kStr = "";

            int i = 0;
            list.Add(string.Format("ASTRA {0} {1}", cmb_structure_type.Text, txtUserTitle.Text));
            list.Add(string.Format("UNIT {0} {1}", cmb_Base_MUnit.Text, cmb_Base_LUnit.Text));

            list.Add(string.Format("JOINT COORDINATES"));

            for (i = 0; i < dgv_joints.RowCount; i++)
            {
                list.Add(string.Format("{0,-5} {1,10} {2,10} {3,10}", dgv_joints[0, i].Value,
                    dgv_joints[1, i].Value, dgv_joints[2, i].Value, dgv_joints[3, i].Value));

            }
            list.Add(string.Format("MEMBER INCIDENCES"));
            for (i = 0; i < dgv_members.RowCount; i++)
            {
                list.Add(string.Format("{0,-5} {1,10} {2,10}",
                    dgv_members[0, i].Value,
                    dgv_members[2, i].Value,
                    dgv_members[3, i].Value));

            }

            if (dgv_elements.RowCount > 1)
            {
                list.Add(string.Format("ELEMENT CONNECTIVITY"));
                for (i = 0; i < dgv_elements.RowCount; i++)
                {
                    list.Add(string.Format("{0,-5} {1,4} {2,4} {3,4} {4,4}",
                        dgv_elements[0, i].Value,
                        dgv_elements[1, i].Value,
                        dgv_elements[2, i].Value,
                        dgv_elements[3, i].Value,
                        dgv_elements[4, i].Value));
                }
            }
            if (tv_mem_grps.Nodes.Count > 0)
            {
                list.Add(string.Format("START GROUP DEFINITION"));
                for (i = 0; i < tv_mem_grps.Nodes.Count; i++)
                {
                    list.Add(string.Format("{0}", tv_mem_grps.Nodes[i].Text));

                }
                list.Add(string.Format("END GROUP DEFINITION"));
            }
            if (tv_mem_props.Nodes.Count > 0)
            {

                if (cmb_Prop_LUnit.SelectedIndex != cmb_Base_LUnit.SelectedIndex)
                {
                    list.Add(string.Format("UNIT {0}", cmb_Prop_LUnit.Text));
                }

                list.Add(string.Format("MEMBER PROPERTY"));
                for (i = 0; i < tv_mem_props.Nodes.Count; i++)
                {
                    list.Add(string.Format("{0}", tv_mem_props.Nodes[i].Text));
                }
            }
            if (tv_elem_props.Nodes.Count > 0)
            {
                list.Add(string.Format("ELEMENT PROPERTY"));
                for (i = 0; i < tv_elem_props.Nodes.Count; i++)
                {
                    list.Add(string.Format("{0}", tv_elem_props.Nodes[i].Text));
                }
            }
            if (tv_mem_spec_truss.Nodes.Count > 0)
            {
                for (i = 0; i < tv_mem_spec_truss.Nodes.Count; i++)
                {
                    list.Add(string.Format("MEMBER TRUSS"));
                    list.Add(string.Format("{0}", tv_mem_spec_truss.Nodes[i].Text));
                }
            }
            if (tv_mem_spec_cable.Nodes.Count > 0)
            {
                for (i = 0; i < tv_mem_spec_cable.Nodes.Count; i++)
                {
                    list.Add(string.Format("MEMBER CABLE"));
                    list.Add(string.Format("{0}", tv_mem_spec_cable.Nodes[i].Text));
                }
            }
            if (tv_mem_release.Nodes.Count > 0)
            {
                list.Add(string.Format("MEMBER RELEASE"));
                for (i = 0; i < tv_mem_release.Nodes.Count; i++)
                {
                    list.Add(string.Format("{0}", tv_mem_release.Nodes[i].Text));
                }
            }
            if (tv_constants.Nodes.Count > 0)
            {
                if (cmb_Const_LUnit.SelectedIndex != cmb_Base_LUnit.SelectedIndex ||
                  cmb_Const_MUnit.SelectedIndex != cmb_Base_MUnit.SelectedIndex)
                {
                    string s1 = "";
                    string s2 = "";

                    if (cmb_Const_MUnit.SelectedIndex != cmb_Base_MUnit.SelectedIndex)
                        s1 = cmb_Const_MUnit.Text;
                    if (cmb_Const_LUnit.SelectedIndex != cmb_Base_LUnit.SelectedIndex)
                        s1 = cmb_Const_LUnit.Text;

                    if(s1 != "" || s2 != "")
                        list.Add(string.Format("UNIT {0} {1}", s1, s2));
                }

                list.Add(string.Format("MATERIAL CONSTANT"));
                for (i = 0; i < tv_constants.Nodes.Count; i++)
                {
                    for (int j = 0; j < tv_constants.Nodes[i].Nodes.Count; j++)
                    {
                        list.Add(string.Format("{0}", tv_constants.Nodes[i].Nodes[j].Text));
                    }
                }
            }
            if (tv_supports.Nodes.Count > 0)
            {
                list.Add(string.Format("SUPPORTS"));
                for (i = 0; i < tv_supports.Nodes.Count; i++)
                {
                    list.Add(string.Format("{0}", tv_supports.Nodes[i].Text));
                }
            }
            if (chk_selfweight.Checked)
            {
                list.Add(string.Format("SELFWEIGHT {0} {1}", cmb_Self_Weight.Text, txt_seft_wt_Value.Text));
            }
            if (tv_loads.Nodes.Count > 0)
            {
                if (cmb_Load_LUnit.SelectedIndex != cmb_Base_LUnit.SelectedIndex ||
                    cmb_Load_MUnit.SelectedIndex != cmb_Base_MUnit.SelectedIndex)
                {
                    list.Add(string.Format("UNIT {0} {1}", cmb_Load_MUnit.Text, cmb_Load_LUnit.Text));
                }


                for (i = 0; i < LoadCases.Count; i++)
                {

                    if (LoadCases[i].Comb_Loads.Count > 0)
                    {
                        list.Add(string.Format("LOAD COMB {0} {1}", LoadCases[i].Comb_Loads.LoadNo, LoadCases[i].Comb_Loads.Name));
                        LoadCases[i].Comb_Loads.Set_Combination();
                        list.Add(string.Format("{0}", LoadCases[i].Comb_Loads.Data));
                        continue;
                    }
                    else
                        list.Add(string.Format("LOAD {0} {1}", LoadCases[i].LoadNo, LoadCases[i].Title));


                    if (LoadCases[i].RepeatLoads.Count > 0)
                    {
                        LoadCases[i].RepeatLoads.Set_Combination();
                        list.Add(string.Format("REPEAT LOAD"));
                        list.Add(string.Format("{0}", LoadCases[i].RepeatLoads.Data));
                    //foreach (var item in LoadCases[i].RepeatLoads)
                    //{

                    //     ;
                    //    list.Add(string.Format("{0}", LoadCases[i].RepeatLoads.Data));

                    //}
                    }

                    //for (int v = 0; v < LoadCases[i].RepeatLoads.Count;v++)
                    //{

                    //    list.Add(string.Format("{0}", LoadCases[i].RepeatLoads.G

                    //}

                    if (LoadCases[i].JointLoads.Count > 0)
                        list.Add(string.Format("JOINT LOAD"));
                    foreach (var item in LoadCases[i].JointLoads)
                    {
                        list.Add(string.Format("{0}", item));

                    }

                    if (LoadCases[i].MemberLoads.Count > 0)
                        list.Add(string.Format("MEMBER LOAD"));
                    foreach (var item in LoadCases[i].MemberLoads)
                    {
                        list.Add(string.Format("{0}", item));

                    }
                    if (LoadCases[i].ElementLoads.Count > 0)
                        list.Add(string.Format("ELEMENT LOAD"));
                    foreach (var item in LoadCases[i].ElementLoads)
                    {
                        list.Add(string.Format("{0}", item));

                    }
                    if (LoadCases[i].AreaLoads.Count > 0)
                        list.Add(string.Format("AREA LOAD"));
                    foreach (var item in LoadCases[i].AreaLoads)
                    {
                        list.Add(string.Format("{0}", item));

                    }
                    if (LoadCases[i].TemperatureLoads.Count > 0)
                        list.Add(string.Format("TEMP LOAD"));
                    foreach (var item in LoadCases[i].TemperatureLoads)
                    {
                        list.Add(string.Format("{0}", item));
                    }

                    if (LoadCases[i].SupportDisplacementLoads.Count > 0)
                        list.Add(string.Format("SUPPORT DISPLACEMENT LOAD"));
                    foreach (var item in LoadCases[i].SupportDisplacementLoads)
                    {
                        list.Add(string.Format("{0}", item));
                    }


                }


                //for (i = 0; i < Combinations.Count; i++)
                //{
                //    list.Add(string.Format("LOAD COMB {0} {1}", Combinations[i].LoadNo, Combinations[i].Name));

                //    foreach (var item in Combinations)
                //    {
                //        item.Set_Combination();
                //        list.Add(string.Format("{0}", item.Data));
                //    }
                //}

            }

            if (chk_dynamic_analysis.Checked)
            {
                if (rbtn_perform_eigen.Checked)
                {
                    list.Add(string.Format("PERFORM EIGEN VALUES ANALYSIS"));
                    list.Add(string.Format("FREQUENCIES {0}", txt_frequencies.Text));
                }
                else
                {
                    list.AddRange(txt_dynamic.Lines);
                }
                //list.Add(string.Format(""));
            }
            else
            {

                if (tv_mov_def_load.Nodes.Count > 0)
                {
                    List<string> ll = new List<string>();
                    ll.Add("");
                    ll.Add("FILE LL.TXT");
                    ll.Add("");
                    for (i = 0; i < MovingLoads.Count; i++)
                    {
                        ll.Add("");
                        ll.Add(MovingLoads[i].ToString());
                        ll.Add(MovingLoads[i].Loads.Replace(",", " "));
                        ll.Add(MovingLoads[i].Distances.Replace(",", " "));
                        ll.Add(MovingLoads[i].LoadWidth.ToString("f3"));
                        ll.Add("");
                        ll.Add("");

                    }
                    File.WriteAllLines(Path.Combine(Path.GetDirectoryName(fname), "LL.TXT"), ll.ToArray());

                    rtb_ll_txt.Lines = ll.ToArray();

                    list.Add(string.Format("DEFINE MOVING LOAD FILE LL.TXT"));
                    for (i = 0; i < LL_Definition.Count; i++)
                    {
                        list.Add(string.Format("TYPE {0} {1} {2:f3}", LL_Definition[i].Type,
                            Get_Load_Name(LL_Definition[i].Type),
                            LL_Definition[i].Impact_Factor));
                    }
                    list.Add(string.Format("LOAD GENERATION {0}", txt_load_gen.Text));
                    for (i = 0; i < LL_Definition.Count; i++)
                    {
                        list.Add(string.Format("{0}", LL_Definition[i].ToString()));
                    }
                }
            }
            if (chk_perform_ana.Checked)
                list.Add(string.Format(chk_perform_ana.Text));
            if (chk_print_supp_reac.Checked)
                list.Add(string.Format(chk_print_supp_reac.Text));
            if (chk_print_static_check.Checked)
                list.Add(string.Format(chk_print_static_check.Text));
            if (chk_print_load_data.Checked)
                list.Add(string.Format(chk_print_load_data.Text));
            if (chk_print_ana_all.Checked)
                list.Add(string.Format(chk_print_ana_all.Text));
            if (chk_print_max_force.Checked)
            {
                for (i = 0; i < tv_max_frc.Nodes.Count; i++)
                {
                    list.Add(string.Format("PRINT MAX FORCE ENVELOPE LIST {0}", tv_max_frc.Nodes[i].Text));
                }
            }

            list.Add(string.Format("FINISH"));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));
            list.Add(string.Format(""));

            rtb_input_file.Lines = list.ToArray();

            AST_DOC = new ASTRADoc(fname);
            File.WriteAllLines(fname, list.ToArray());

            if (MessageBox.Show("ASTRA Data file created as \n\r" + fname, "ASTRA", MessageBoxButtons.OK) == DialogResult.OK)
            {
                //System.Diagnostics.Process.Start(fname);
            }

        }
        public string Get_Load_Name(int type)
        {
            foreach (var item in MovingLoads)
            {
                if (item.Type == type)
                    return item.Name;
            }
            return "";
        }

        private void chk_selfweight_CheckedChanged(object sender, EventArgs e)
        {
            cmb_Self_Weight.Enabled = chk_selfweight.Checked;
            txt_seft_wt_Value.Enabled = chk_selfweight.Checked;
        }

        private void tv_mem_grps_MouseMove(object sender, MouseEventArgs e)
        {
            TreeView tv = sender as TreeView;
            if (tv.Name == tv_mem_grps.Name)
            {
                btn_grps_edit.Enabled = (tv_mem_grps.SelectedNode != null);
                btn_grps_del.Enabled = (tv_mem_grps.SelectedNode != null);
            }
            else if (tv.Name == tv_elem_props.Name)
            {
                btn_elem_props_edit.Enabled = (tv_elem_props.SelectedNode != null);
                btn_elem_props_del.Enabled = (tv_elem_props.SelectedNode != null);
            }
            else if (tv.Name == tv_mem_props.Name)
            {
                btn_props_edit.Enabled = (tv_mem_props.SelectedNode != null);
                btn_props_del.Enabled = (tv_mem_props.SelectedNode != null);
            }
            else if (tv.Name == tv_mem_spec_truss.Name)
            {
                btn_spec_edit_truss.Enabled = (tv_mem_spec_truss.SelectedNode != null);
                btn_spec_del_truss.Enabled = (tv_mem_spec_truss.SelectedNode != null);
            }
            else if (tv.Name == tv_mem_spec_cable.Name)
            {
                btn_spec_edit_cable.Enabled = (tv_mem_spec_cable.SelectedNode != null);
                btn_spec_del_cable.Enabled = (tv_mem_spec_cable.SelectedNode != null);
            }
            else if (tv.Name == tv_mem_release.Name)
            {
                btn_release_edit.Enabled = (tv_mem_release.SelectedNode != null);
                btn_release_del.Enabled = (tv_mem_release.SelectedNode != null);
            }
            else if (tv.Name == tv_mem_release.Name)
            {
                btn_release_edit.Enabled = (tv_mem_release.SelectedNode != null);
                btn_release_del.Enabled = (tv_mem_release.SelectedNode != null);
            }
            else if (tv.Name == tv_constants.Name)
            {
                btn_cons_edit.Enabled = (tv_constants.SelectedNode != null);
                btn_cons_del.Enabled = (tv_constants.SelectedNode != null);
            }
            else if (tv.Name == tv_supports.Name)
            {
                btn_supp_edit.Enabled = (tv_supports.SelectedNode != null);
                btn_supp_del.Enabled = (tv_supports.SelectedNode != null);
            }
            else if (tv.Name == tv_loads.Name)
            {
                btn_ldc_edit.Enabled = (tv_loads.SelectedNode != null);
                btn_ldc_del.Enabled = (tv_loads.SelectedNode != null);
            }
            else if (tv.Name == tv_mov_loads.Name)
            {
                btn_mov_load_edit.Enabled = (tv_mov_loads.SelectedNode != null);
                btn_mov_load_del.Enabled = (tv_mov_loads.SelectedNode != null);
            }
            else if (tv.Name == tv_mov_def_load.Name)
            {
                btn_mov_def_load_edit.Enabled = (tv_mov_def_load.SelectedNode != null);
                btn_mov_def_load_del.Enabled = (tv_mov_def_load.SelectedNode != null);
            }
        }

        private void Button_Enable_Disable()
        {

            btn_grps_edit.Enabled = (tv_mem_grps.SelectedNode != null);
            btn_grps_del.Enabled = (tv_mem_grps.SelectedNode != null);

            btn_props_edit.Enabled = (tv_mem_props.SelectedNode != null);
            btn_props_del.Enabled = (tv_mem_props.SelectedNode != null);

            btn_elem_props_edit.Enabled = (tv_elem_props.SelectedNode != null);
            btn_elem_props_del.Enabled = (tv_elem_props.SelectedNode != null);


            btn_spec_edit_truss.Enabled = (tv_mem_spec_truss.SelectedNode != null);
            btn_spec_del_truss.Enabled = (tv_mem_spec_truss.SelectedNode != null);

            btn_spec_edit_cable.Enabled = (tv_mem_spec_cable.SelectedNode != null);
            btn_spec_del_cable.Enabled = (tv_mem_spec_cable.SelectedNode != null);

            btn_release_edit.Enabled = (tv_mem_release.SelectedNode != null);
            btn_release_del.Enabled = (tv_mem_release.SelectedNode != null);

            btn_release_edit.Enabled = (tv_mem_release.SelectedNode != null);
            btn_release_del.Enabled = (tv_mem_release.SelectedNode != null);

            btn_cons_edit.Enabled = (tv_constants.SelectedNode != null);
            btn_cons_del.Enabled = (tv_constants.SelectedNode != null);

            btn_supp_edit.Enabled = (tv_supports.SelectedNode != null);
            btn_supp_del.Enabled = (tv_supports.SelectedNode != null);

            btn_ldc_edit.Enabled = (tv_loads.SelectedNode != null);
            btn_ldc_del.Enabled = (tv_loads.SelectedNode != null);

            if (tv_loads.SelectedNode == null)
            {
                //btn_jload_add.Enabled = (tv_loadings.SelectedNode != null);
                //btn_jload_edit.Enabled = (tv_loadings.SelectedNode != null);
                //btn_jload_del.Enabled = (tv_loadings.SelectedNode != null);


                //btn_mload_add.Enabled = (tv_loadings.SelectedNode != null);
                //btn_mload_edit.Enabled = (tv_loadings.SelectedNode != null);
                //btn_mload_del.Enabled = (tv_loadings.SelectedNode != null);


                //btn_aload_add.Enabled = (tv_loadings.SelectedNode != null);
                //btn_aload_edit.Enabled = (tv_loadings.SelectedNode != null);
                //btn_aload_del.Enabled = (tv_loadings.SelectedNode != null);


                //btn_tload_add.Enabled = (tv_loadings.SelectedNode != null);
                //btn_tload_edit.Enabled = (tv_loadings.SelectedNode != null);
                //btn_tload_del.Enabled = (tv_loadings.SelectedNode != null);


                //btn_rload_add.Enabled = (tv_loadings.SelectedNode != null);
                //btn_rload_edit.Enabled = (tv_loadings.SelectedNode != null);
                //btn_rload_del.Enabled = (tv_loadings.SelectedNode != null);
            }
            else
                Load_Selection(tv_loads.SelectedNode);

            btn_cload_edit.Enabled = (tv_loads.SelectedNode != null);
            btn_cload_del.Enabled = (tv_loads.SelectedNode != null);


            btn_mov_load_edit.Enabled = (tv_loads.SelectedNode != null);
            btn_mov_load_del.Enabled = (tv_loads.SelectedNode != null);

            btn_mov_def_load_edit.Enabled = (tv_mov_def_load.SelectedNode != null);
            btn_mov_def_load_del.Enabled = (tv_mov_def_load.SelectedNode != null);



        }

        private void btn_max_frc_add_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_max_frc_add.Name)
            {
                if (txt_max_frc_list.Text != "")
                    tv_max_frc.Nodes.Add(txt_max_frc_list.Text);
            }
            else if (btn.Name == btn_max_frc_del.Name)
            {
                if (tv_max_frc.SelectedNode != null)
                    tv_max_frc.Nodes.Remove(tv_max_frc.SelectedNode);
            }
        }

        private void tsmi_data_open_Click(object sender, EventArgs e)
        {
            //ToolStripMenuItem tsmi = sender as ToolStripMenuItem;

            ToolStripItem tsmi = sender as ToolStripItem;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                #region Drawing Open
                if (tsmi.Name == tsmi_file_open.Name || tsmi.Name == tsb_file_open.Name)
                {
                    ofd.Filter = "All Drawing Files (*.vdml,*.dxf,*.dwg)|*.vdml;*.dxf;*.dwg";
                    if (ofd.ShowDialog() != DialogResult.Cancel)
                    {

                        Drawing_File = ofd.FileName;

                        IsDrawingFileOpen = true;
                        this.Text = "Analysis Input Data File [" + MyStrings.Get_Modified_Path(Drawing_File) + "]";

                        File_Name = Path.Combine(Path.GetDirectoryName(Drawing_File), Path.GetFileNameWithoutExtension(Drawing_File) + ".txt");

                        if (File.Exists(File_Name))
                        {
                            Open_Data_File(File_Name);
                        }
                        else
                            Open_Drawing_File();

                    }
                }
                #endregion Drawing Open

                #region Data Open
                else if (tsmi.Name == tsmi_data_open.Name || tsmi.Name == tsb_open_data.Name)
                {
                    ofd.Filter = "ASTRA Data Files (*.txt)|*.txt";
                    if (ofd.ShowDialog() != DialogResult.Cancel)
                    {
                        File_Name = ofd.FileName;
                        Drawing_File = "";
                        IsDrawingFileOpen = false;
                        this.Text = "Drawing To ASTRA Data [" + MyStrings.Get_Modified_Path(File_Name) + "]";
                        Open_Data_File(File_Name);
                    }
                }
                #endregion Data Open

                #region Drawing Open
                else if (tsmi.Name == tsb_open_data.Name)
                {
                    ofd.Filter = "All Drawing Files (*.vdml,*.dxf,*.dwg)|*.vdml;*.dxf;*.dwg|ASTRA Data Files (*.txt)|*.txt";
                    if (ofd.ShowDialog() != DialogResult.Cancel)
                    {

                        if (Path.GetExtension(ofd.FileName).ToUpper() != ".TXT")
                        {
                            Drawing_File = ofd.FileName;
                            IsDrawingFileOpen = true;
                            File_Name = Path.Combine(Path.GetDirectoryName(Drawing_File), Path.GetFileNameWithoutExtension(Drawing_File) + ".txt");
                            this.Text = "Drawing To ASTRA Data [" + MyStrings.Get_Modified_Path(ofd.FileName) + "]";


                            if (File.Exists(File_Name))
                            {
                                Open_Data_File(File_Name);
                            }
                            else
                                Open_Drawing_File();
                        }
                        else
                        {
                            File_Name = ofd.FileName;
                            Drawing_File = "";
                            IsDrawingFileOpen = false;
                            this.Text = "Drawing To ASTRA Data [" + MyStrings.Get_Modified_Path(File_Name) + "]";
                            Open_Data_File(File_Name);
                        }

                    }
                }
                #endregion Drawing Open
            }

            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(VDoc);

        }

        private void Open_Data_File(string fn)
        {

            Clear_All();
            ASTRADoc astdoc;
            astdoc = new ASTRADoc(fn);
            ACad.AstraDocument = astdoc;

            //AST_DOC.Members.DrawMember(VDoc);
            AST_DOC.Joints.DrawJointsText(VDoc, Text_Size);
            AST_DOC.Members.DrawMember(VDoc, Text_Size);
            AST_DOC.Elements.DrawElements(VDoc);
            AST_DOC.Supports.DrawSupport(VDoc);

            chk_dynamic_analysis.Checked = false;
            rbtn_perform_eigen.Checked = true;
            txt_dynamic.Text = "";

            cmb_structure_type.SelectedItem = AST_DOC.StructureType;
            txtUserTitle.Text = AST_DOC.UserTitle;


            cmb_Base_MUnit.SelectedIndex = (int)AST_DOC.Base_MassUnit;
            cmb_Base_LUnit.SelectedIndex = (int)AST_DOC.Base_LengthUnit;





            cmb_Const_MUnit.SelectedIndex = (int)AST_DOC.Const_MassUnit;
            cmb_Const_LUnit.SelectedIndex = (int)AST_DOC.Const_LengthUnit;


            cmb_Prop_LUnit.SelectedIndex = (int)AST_DOC.Prop_LengthUnit;
            cmb_Load_MUnit.SelectedIndex = (int)AST_DOC.Load_MassUnit;
            cmb_Load_LUnit.SelectedIndex = (int)AST_DOC.Load_LengthUnit;




            SetGridWithNode();
            SetGridWithMember();
            SetGridWithElement();

            Groups.Clear();
            tv_mem_grps.Nodes.Clear();
            foreach (var item in AST_DOC.MemberGroups)
            {
                tv_mem_grps.Nodes.Add(item);

                MemberGroup mg = new MemberGroup();
                MyStrings ml = new MyStrings(item, ' ');

                mg.GroupName = ml.StringList[0];
                mg.MemberNosText = ml.GetString(1);

                Groups.Add(mg);
            }

            tv_mem_props.Nodes.Clear();
            foreach (var item in AST_DOC.MemberProps)
            {
                tv_mem_props.Nodes.Add(item);
            }
            tv_elem_props.Nodes.Clear();
            foreach (var item in AST_DOC.ElementProps)
            {
                tv_elem_props.Nodes.Add(item);
            }
            tv_mem_spec_cable.Nodes.Clear();
            foreach (var item in AST_DOC.MemberCables)
            {
                tv_mem_spec_cable.Nodes.Add(item);
            }
            tv_mem_spec_truss.Nodes.Clear();
            foreach (var item in AST_DOC.MemberTrusses)
            {
                tv_mem_spec_truss.Nodes.Add(item);
            }

            tv_constants.Nodes.Clear();
            Materials.Clear();
            for (int i = 0; i < AST_DOC.Constants.Count; i++)
            {
                tv_constants.Nodes.Add("MATERIAL_" + (i + 1));
                foreach (var item in AST_DOC.Constants[i].ASTRA_Data)
                {
                    tv_constants.Nodes[i].Nodes.Add(item);
                }
                Materials.Add(AST_DOC.Constants[i]);
            }

            tv_supports.Nodes.Clear();
            foreach (var item in AST_DOC.JointSupports)
            {
                tv_supports.Nodes.Add(item);
            }

            if (AST_DOC.SelfWeight != "")
            {
                chk_selfweight.Checked = true;
                cmb_Self_Weight.SelectedItem = AST_DOC.SelfWeight_Direction;
                txt_seft_wt_Value.Text = AST_DOC.SelfWeight;
            }
            tv_loads.Nodes.Clear();
            LoadCases.Clear();
            foreach (var item in AST_DOC.LoadDefines)
            {
                LoadCaseDefinition lcd = new LoadCaseDefinition();
                lcd.LoadNo = item.LoadCase;
                lcd.Title = item.LoadTitle;
                lcd.JointLoads = item.JointLoadList;
                lcd.MemberLoads = item.MemberLoadList;
                lcd.ElementLoads = item.ElementLoadList;
                lcd.AreaLoads = item.AreaLoadList;
                lcd.FloorLoads = item.FloorLoadList;
                lcd.TemperatureLoads = item.TempLoadList;
                lcd.SupportDisplacementLoads = item.SupportDisplacements;


                if (item.CominationLoadList.Count > 0)
                {
                    item.Set_Combination();
                    lcd.Comb_Loads.LoadNo = item.LoadCase;
                    lcd.Comb_Loads.Name = item.LoadTitle;
                    foreach (var cm in item.Combinations)
                    {
                        lcd.Comb_Loads.LoadCases.Add(cm.Load_No);
                        lcd.Comb_Loads.Factors.Add(cm.Load_Factor);
                    }
                    lcd.Comb_Loads.Set_Combination();
                }

                if (item.RepeatLoadList.Count > 0)
                {
                    item.Set_Combination();
                    lcd.RepeatLoads.LoadNo = item.LoadCase;
                    lcd.RepeatLoads.Name = item.LoadTitle;

                    foreach (var cm in item.Combinations)
                    {
                        lcd.RepeatLoads.LoadCases.Add(cm.Load_No);
                        lcd.RepeatLoads.Factors.Add(cm.Load_Factor);
                    }
                    lcd.RepeatLoads.Set_Combination();
                }
                //lcd.Comb_Loads = item.CominationLoadList;
                //lcd.RepeatLoads = item.RepeatLoadList;

                LoadCases.Add(lcd);

            }
            #region   LoadCases
            if (LoadCases.Count > 0)
            {
                TreeNode tn = null;
                TreeNode tjn = null;
                tv_loads.Nodes.Clear();
                foreach (var item in LoadCases)
                {
                    tv_loads.Nodes.Add(item.ToString());

                    tn = tv_loads.Nodes[tv_loads.Nodes.Count - 1];


                    if (item.RepeatLoads.Count > 0)
                    {
                        //item.Set_Combination();
                        tn.Nodes.Add("REPEAT LOAD");
                        tn = tn.Nodes[tn.Nodes.Count - 1];
                        foreach (var item1 in item.RepeatLoads)
                        {
                            tn.Nodes.Add(item1.ToString());
                        }
                        tn = tn.Parent;
                    }
                    if (item.MemberLoads.Count > 0)
                    {
                        tn.Nodes.Add("MEMBER LOAD");
                        tn = tn.Nodes[tn.Nodes.Count - 1];
                        foreach (var item1 in item.MemberLoads)
                        {
                            tn.Nodes.Add(item1);
                        }
                        tn = tn.Parent;

                    }
                    if (item.ElementLoads.Count > 0)
                    {
                        tn.Nodes.Add("ELEMENT LOAD");
                        tn = tn.Nodes[tn.Nodes.Count - 1];
                        foreach (var item1 in item.ElementLoads)
                        {
                            tn.Nodes.Add(item1);
                        }
                        tn = tn.Parent;

                    }
                    if (item.JointLoads.Count > 0)
                    {

                        tn.Nodes.Add("JOINT LOAD");
                        tn = tn.Nodes[tn.Nodes.Count - 1];
                        foreach (var item1 in item.JointLoads)
                        {
                            tn.Nodes.Add(item1);
                        }
                        tn = tn.Parent;
                    }
                    if (item.SupportDisplacementLoads.Count > 0)
                    {

                        tn.Nodes.Add("SUPPORT DISPLACEMENT LOAD");
                        tn = tn.Nodes[tn.Nodes.Count - 1];
                        foreach (var item1 in item.SupportDisplacementLoads)
                        {
                            tn.Nodes.Add(item1);
                        }
                        tn = tn.Parent;
                    }
                    if (item.AreaLoads.Count > 0)
                    {

                        tn.Nodes.Add("AREA LOAD");
                        tn = tn.Nodes[tn.Nodes.Count - 1];
                        foreach (var item1 in item.AreaLoads)
                        {
                            tn.Nodes.Add(item1);
                        }
                        tn = tn.Parent;
                    }

                    if (item.FloorLoads.Count > 0)
                    {

                        tn.Nodes.Add("FLOOR LOAD");
                        tn = tn.Nodes[tn.Nodes.Count - 1];
                        foreach (var item1 in item.FloorLoads)
                        {
                            tn.Nodes.Add(item1);
                        }
                        tn = tn.Parent;
                    }
                    if (item.TemperatureLoads.Count > 0)
                    {

                        tn.Nodes.Add("TEMP LOAD");
                        tn = tn.Nodes[tn.Nodes.Count - 1];
                        foreach (var item1 in item.TemperatureLoads)
                        {
                            tn.Nodes.Add(item1);
                        }
                        tn = tn.Parent;
                    }
                    if (item.Comb_Loads.Count > 0)
                    {
                        //item.Set_Combination();
                        //tn.Nodes.Add("COMBINATION");
                        //if (tn.Nodes.Count > 0)
                        //    tn = tn.Nodes[tn.Nodes.Count - 1];
                        foreach (var item1 in item.Comb_Loads)
                        {
                            tn.Nodes.Add(item1.ToString());
                        }
                        //tn = tn.Parent;
                    }
                    //if (item.ElementLoad.Count > 0)
                    //{
                    //    tn.Nodes.Add("ELEMENT LOAD");
                    //    tn = tn.Nodes[0];
                    //    foreach (var item1 in item.ElementLoadList)
                    //    {
                    //        //tn.Nodes.Add(item1 + " " + astDoc.MassUnit + "/" + astDoc.LengthUnit);
                    //        tn.Nodes.Add(item1);
                    //    }
                    //    tn = tn.Parent;
                    //}
                }
            }
            #endregion

            #region Moving Load
            if (AST_DOC.MovingLoads.Count > 0)
            {
                TreeNode tn;
                tv_mov_loads.Nodes.Clear();
                MovingLoads.Clear();

                foreach (var item in AST_DOC.MovingLoads)
                {
                    MovingLoads.Add(item);
                    tn = new TreeNode(item.ToString());
                    tv_mov_loads.Nodes.Add(tn);
                    tn.Nodes.Add("LOADS : " + item.Loads);
                    tn.Nodes.Add("DISTANCES : " + item.Distances);
                    tn.Nodes.Add("LOAD WIDTH : " + item.LoadWidth);
                }
            }
            if (AST_DOC.LL_Definition.Count > 0)
            {


                TreeNode tn;
                tv_mov_def_load.Nodes.Clear();
                LL_Definition.Clear();

                foreach (var item in AST_DOC.LL_Definition)
                {

                    LL_Definition.Add(item);

                    tn = new TreeNode(item.ToString());

                    tv_mov_def_load.Nodes.Add(tn);
                    tn.Nodes.Add("X-Distance : " + item.X_Distance);
                    tn.Nodes.Add("Y-Distance : " + item.Y_Distance);
                    tn.Nodes.Add("Z-Distance : " + item.Z_Distance);
                    if (item.X_Increment != 0.0)
                        tn.Nodes.Add("X_Increment : " + item.X_Increment);
                    if (item.Y_Increment != 0.0)
                        tn.Nodes.Add("Y_Increment : " + item.Y_Increment);
                    if (item.Z_Increment != 0.0)
                        tn.Nodes.Add("Z_Increment : " + item.Z_Increment);
                    tn.Nodes.Add("Impact_Factor : " + item.Impact_Factor);

                }
            }
            #endregion Moving Load
            chk_perform_ana.Checked = AST_DOC.Analysis_Specification.Perform_Analysis;
            chk_print_ana_all.Checked = AST_DOC.Analysis_Specification.Print_Analysis_All;
            chk_print_load_data.Checked = AST_DOC.Analysis_Specification.Print_Load_Data;
            chk_print_static_check.Checked = AST_DOC.Analysis_Specification.Print_Static_Check;
            chk_print_supp_reac.Checked = AST_DOC.Analysis_Specification.Print_Support_Reaction;
            chk_print_max_force.Checked = AST_DOC.Analysis_Specification.Print_Max_Force;
            txt_max_frc_list.Enabled = true;
            tv_max_frc.Nodes.Clear();
            foreach (var item in AST_DOC.Analysis_Specification.List_Maxforce)
            {
                tv_max_frc.Nodes.Add(item);
            }

            if (AST_DOC.DynamicAnalysis.Count > 0)
            {
                chk_dynamic_analysis.Checked = true;

                if (AST_DOC.DynamicAnalysis[0].StartsWith("PERFORM EIGEN"))
                {
                    rbtn_perform_eigen.Checked = true;
                    txt_frequencies.Text = AST_DOC.FREQUENCIES;
                }
                else if (AST_DOC.DynamicAnalysis[0].StartsWith("PERFORM TIME HISTORY"))
                {
                    rbtn_perform_time_history.Checked = true;
                    txt_dynamic.Lines = AST_DOC.DynamicAnalysis.ToArray();
                }
                else if (AST_DOC.DynamicAnalysis[0].StartsWith("PERFORM RESPON"))
                {
                    txt_dynamic.Lines = AST_DOC.DynamicAnalysis.ToArray();
                    rbtn_response_spectrum.Checked = true;
                }
            }
            //AST_DOC.DynamicAnalysis
            //return astdoc;
            Count_Geometry();

            this.Text = "Analysis Input Data File [ " + MyStrings.Get_Modified_Path(fn) + " ]";

            rtb_input_file.Lines = File.ReadAllLines(fn);

            if (File.Exists(MyStrings.Get_LL_TXT_File(fn)))
            {
                rtb_ll_txt.Lines = File.ReadAllLines(MyStrings.Get_LL_TXT_File(fn));
            }


            if (File.Exists(Analysis_File_Name))
            {
                rtb_ana_rep.Lines = File.ReadAllLines(Analysis_File_Name);
                StructureAnalysis = null;
                ld = null;
                Select_Steps();
            }

            Tab_Selection();
        }

        private void tsmi_file_save_Click(object sender, EventArgs e)
        {
            ToolStripItem tsmi = sender as ToolStripItem;

            if (tsmi.Name == tsmi_file_save_as.Name)
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Text Data File(*.txt)|*.txt";
                    if (sfd.ShowDialog() != DialogResult.Cancel)
                    {
                        DataFileName = sfd.FileName;
                        File.WriteAllLines(DataFileName, rtb_input_file.Lines);
                    }
                    else
                        return;
                }
            }
            Save_Data();
        }

        private void tsmi_file_print_Click(object sender, EventArgs e)
        {
            if (HASP_Lock.IsDemoVersion())
            {
                MessageBox.Show("This feature is not available in demo version.", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (this.VDoc != null)
                {
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.PrintEx(VDoc);
                }
            }
        }

        private void tsmi_show_data_Click(object sender, EventArgs e)
        {
            if (File.Exists(File_Name))
            {
                System.Diagnostics.Process.Start(File_Name);
            }
        }

        private void tsmi_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_elem_props_add_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_elem_props_add.Name)
            {
                frmElementProp fs = new frmElementProp(ACad);
                fs.Owner = this;
                fs.TRV = tv_elem_props;
                fs.ShowDialog();
                //if (fs.ASTRA_Data.Count > 0)
                //    tv_mem_props.Nodes.Add(fs.ASTRA_Data[0]);
            }
            else if (btn.Name == btn_elem_props_del.Name)
            {
                tv_elem_props.Nodes.Remove(tv_elem_props.SelectedNode);
            }
            else if (btn.Name == btn_elem_props_edit.Name)
            {
                frmElementProp fs = new frmElementProp(ACad);
                fs.Owner = this;
                fs.TRV = tv_elem_props;
                fs.ASTRA_Data.Add(tv_elem_props.SelectedNode.Text);
                fs.ShowDialog();
            }

        }

        private void btn_mbr_add_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;

            if (btn.Name == btn_mbr_add.Name)
            {
                frmMembers fs = new frmMembers(ACad);
                fs.Owner = this;
                fs.DGV = dgv_members;
                fs.ShowDialog();

                //Draw_Members();
            }
            else if (btn.Name == btn_mbr_delete.Name)
            {
                if (dgv_members.SelectedCells.Count > 0)
                    dgv_members.Rows.RemoveAt(dgv_members.SelectedCells[0].RowIndex);
                else if (dgv_members.CurrentCell.RowIndex > -1)
                    dgv_members.Rows.RemoveAt(dgv_members.CurrentCell.RowIndex);

                //Draw_Members();
                //tv_elem_props.Nodes.Remove(tv_elem_props.SelectedNode);
            }
            else if (btn.Name == btn_mbr_edit.Name)
            {
                frmMembers fs = new frmMembers(ACad);
                fs.Owner = this;
                fs.DGV = dgv_members;
                if (dgv_members.SelectedCells.Count > 0)
                    fs.ASTRA_Data.Add(dgv_members.SelectedCells[0].RowIndex.ToString());
                else
                    fs.ASTRA_Data.Add(dgv_members.CurrentCell.RowIndex.ToString());
                fs.ShowDialog();
            }
            else if (btn.Name == btn_mbr_insert.Name)
            {
                frmMembers fs = new frmMembers(ACad);
                fs.Owner = this;
                fs.DGV = dgv_members;
                fs.ASTRA_Data.Add("INSERT");
                fs.ShowDialog();
            }
            Draw_Members();

        }

        private void btn_jnt_add_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;

            if (btn.Name == btn_jnt_add.Name)
            {
                frmJoints fs = new frmJoints(ACad);
                fs.Owner = this;
                fs.DGV = dgv_joints;
                fs.ShowDialog();
                //if (fs.ASTRA_Data.Count > 0)
                //    tv_mem_props.Nodes.Add(fs.ASTRA_Data[0]);
            }
            else if (btn.Name == btn_jnt_delete.Name)
            {
                //tv_elem_props.Nodes.Remove(tv_elem_props.SelectedNode);

                if (dgv_joints.SelectedCells.Count > 0)
                    dgv_joints.Rows.RemoveAt(dgv_joints.SelectedCells[0].RowIndex);
                else if (dgv_joints.CurrentCell.RowIndex > -1)
                    dgv_joints.Rows.RemoveAt(dgv_joints.CurrentCell.RowIndex);

            }
            else if (btn.Name == btn_jnt_edit.Name)
            {
                frmJoints fs = new frmJoints(ACad);
                fs.Owner = this;
                fs.DGV = dgv_joints;
                if (dgv_joints.SelectedCells.Count > 0)
                    fs.ASTRA_Data.Add(dgv_joints.SelectedCells[0].RowIndex.ToString());
                else
                    fs.ASTRA_Data.Add(dgv_joints.CurrentCell.RowIndex.ToString());
                fs.ShowDialog();
            }
            else if (btn.Name == btn_jnt_insert.Name)
            {
                frmJoints fs = new frmJoints(ACad);
                fs.Owner = this;
                fs.DGV = dgv_joints;
                fs.ASTRA_Data.Add("INSERT");
                fs.ShowDialog();
            }
            Draw_Joints();
            Draw_Members();
            Draw_Supports();
        }

        public void Draw_Joints()
        {
            //Clear_All();
            ASTRADoc astdoc;
            //astdoc = new ASTRADoc(ACad.AstraDocument);
            astdoc = (ACad.AstraDocument);


            astdoc.Joints.Clear();

            for (int i = 0; i < dgv_joints.Rows.Count; i++)
            {
                JointCoordinate jn = new JointCoordinate();

                dgv_joints[0, i].Value = i + 1;

                jn.NodeNo = MyStrings.StringToInt(dgv_joints[0, i].Value.ToString(), 0);
                jn.Point.x = MyStrings.StringToDouble(dgv_joints[1, i].Value.ToString(), 0.0);
                jn.Point.y = MyStrings.StringToDouble(dgv_joints[2, i].Value.ToString(), 0.0);
                jn.Point.z = MyStrings.StringToDouble(dgv_joints[3, i].Value.ToString(), 0.0);
                astdoc.Joints.Add(jn);
            }

            foreach (var item in VDoc.ActionLayout.Entities)
            {
                if (item is vdFigure)
                {
                    vdFigure vf = item as vdFigure;
                    if (vf.Layer.Name == "Nodes")
                    {
                        vf.Deleted = true;
                    }
                }

            }
            VDoc.Redraw(true);
            astdoc.Joints.DrawJointsText(VDoc, Text_Size);
            Count_Geometry();
        }
        public void Draw_Supports()
        {

            #region Draw_Support
            AST_DOC.Supports.Clear();

            if (tv_supports.Nodes.Count > 0)
            {
                for (int i = 0; i < tv_supports.Nodes.Count; i++)
                {
                    AST_DOC.Supports.Add(tv_supports.Nodes[i].Text);
                }
            }

            AST_DOC.Supports.CopyFromCoordinateCollection(AST_DOC.Joints);
            AST_DOC.Supports.DrawSupport(VDoc);
            VDoc.Redraw(true);
            #endregion DrawSupport
            Count_Geometry();
        }
        public void Draw_Members()
        {


            ClearSelect();
            ASTRADoc astdoc;
            //astdoc = new ASTRADoc(ACad.AstraDocument);
            astdoc = (ACad.AstraDocument);


            astdoc.Members.Clear();

            for (int i = 0; i < dgv_members.Rows.Count; i++)
            {
                MemberIncidence mbr = new MemberIncidence();
                dgv_members[0, i].Value = i + 1;

                mbr.MemberNo = MyStrings.StringToInt(dgv_members[0, i].Value.ToString(), 0);

                if (dgv_members[1, i].Value.ToString().StartsWith("C"))
                    mbr.MemberType = MembType.CABLE;
                else if (dgv_members[1, i].Value.ToString().StartsWith("T"))
                    mbr.MemberType = MembType.TRUSS;
                else
                    mbr.MemberType = MembType.BEAM;

                mbr.StartNode.NodeNo = MyStrings.StringToInt(dgv_members[2, i].Value.ToString(), 0);



                mbr.EndNode.NodeNo = MyStrings.StringToInt(dgv_members[3, i].Value.ToString(), 0);


                astdoc.Members.Add(mbr);
            }
            astdoc.Members.CopyJointCoordinates(astdoc.Joints);



            foreach (var item in VDoc.ActionLayout.Entities)
            {
                if (item is vdFigure)
                {
                    vdFigure vf = item as vdFigure;
                    if (vf.Layer.Name == "Members" || vf.Layer.Name == "0")
                    {
                        vf.Deleted = true;
                    }
                }

            }
            VDoc.Redraw(true);

            astdoc.Members.DrawMember(VDoc, Text_Size);

            Count_Geometry();
        }
        private void Draw_Loadings()
        {
            string kStr = "";

            TreeNode tn = tv_loads.SelectedNode;

            bool IsJointLoad = false;

            if (tn.Nodes.Count == 0)
            {
                if (tn.Parent != null)
                    IsJointLoad = (tn.Parent.Text.StartsWith("JOINT"));
                kStr = tn.Text;
            }

            JointLoad jn = new JointLoad();




            AST_DOC.JointLoads.Delete_ASTRAArrowLine(VDoc);
            AST_DOC.MemberLoads.Delete_ASTRAMemberLoad(VDoc);

            if (tn.Parent != null)
            {
                tn = tn.Parent;
                if (tv_loads.SelectedNode.Text.StartsWith("JOINT"))
                {
                    AST_DOC.JointLoads.DrawJointLoads(VDoc, tn.Index + 1); goto _100;
                    //cmbLoadCase.SelectedIndex = tn.Index;
                }
                else if (tv_loads.SelectedNode.Text.StartsWith("MEMBER"))
                {
                    AST_DOC.MemberLoads.DrawMemberLoad(VDoc, tn.Index + 1); goto _100;
                    //cmbLoadCase.SelectedIndex = tn.Index;
                }
                if (tn.Text.StartsWith("COMB"))
                {
                    MyStrings mlist = new MyStrings(kStr, ' ');
                    AST_DOC.MemberLoads.DrawMemberLoad(VDoc, mlist.GetInt(3));
                    AST_DOC.JointLoads.DrawJointLoads(VDoc, mlist.GetInt(3)); goto _100;
                }
                if (tn.Text.StartsWith("REPEAT"))
                {
                    MyStrings mlist = new MyStrings(kStr, ' ');
                    AST_DOC.MemberLoads.DrawMemberLoad(VDoc, mlist.GetInt(3));
                    AST_DOC.JointLoads.DrawJointLoads(VDoc, mlist.GetInt(3));
                    goto _100;
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

            if (IsJointLoad)
            {
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
                mlc.DrawMemberLoad(VDoc, tn.Index + 1);
            }
        _100:
            VDoc.Redraw(true);
        }
        public void Draw_Elements()
        {


            //Clear_All();
            ASTRADoc astdoc;
            //astdoc = new ASTRADoc(iACad.AstraDocument);
            astdoc = (ACad.AstraDocument);


            astdoc.Elements.Clear();

            DataGridView DGV = dgv_elements;

            for (int i = 0; i < DGV.Rows.Count; i++)
            {
                Element elm = new Element();
                DGV[0, i].Value = i + 1;

                //jn.NodeNo = MyList.StringToInt(DGV[0, i].Value.ToString(), 0);
                elm.ElementNo = MyStrings.StringToInt(DGV[0, i].Value.ToString(), 0);
                elm.Node1.NodeNo = MyStrings.StringToInt(DGV[1, i].Value.ToString(), 0);
                elm.Node2.NodeNo = MyStrings.StringToInt(DGV[2, i].Value.ToString(), 0);
                elm.Node3.NodeNo = MyStrings.StringToInt(DGV[3, i].Value.ToString(), 0);
                elm.Node4.NodeNo = MyStrings.StringToInt(DGV[4, i].Value.ToString(), 0);

                astdoc.Elements.Add(elm);
            }
            //iACad.Document.ActiveLayOut.Entities.EraseAll();

            foreach (var item in ACad.Document.ActionLayout.Entities)
            {
                if (item is vdFigure)
                {
                    vdFigure vf = item as vdFigure;
                    if (vf.Layer.Name == "Elements")
                    {
                        vf.Deleted = true;
                    }
                }

            }
            ACad.Document.Redraw(true);

            astdoc.Elements.CopyCoordinates(astdoc.Joints);
            astdoc.Elements.DrawElements(ACad.Document);
            Count_Geometry();

        }
        public void Count_Geometry()
        {
            txt_total_jnts.Text = AST_DOC.Joints.Count.ToString();
            txt_total_mbrs.Text = AST_DOC.Members.Count.ToString();
            txt_total_elmts.Text = AST_DOC.Elements.Count.ToString();
            txt_total_supps.Text = AST_DOC.Supports.Count.ToString();
        }

        public void Delete_Layer_Items(string LayerName)
        {
            foreach (var item in ACad.Document.ActionLayout.Entities)
            {
                if (item is vdFigure)
                {
                    vdFigure vf = item as vdFigure;
                    if (vf.Layer.Name == LayerName)
                    {
                        vf.Deleted = true;
                    }
                }

            }
            ACad.Document.Redraw(true);
        }
        public double Text_Size
        {
            get
            {
                return (cmb_text_size.SelectedIndex + 1) * 0.1;
            }
        }

        private void cmb_size_Click(object sender, EventArgs e)
        {

            foreach (var item in VDoc.ActionLayout.Entities)
            {
                if (item is vdMText)
                {
                    vdMText t = item as vdMText;
                    t.Height = Text_Size;
                    t.Update();
                }
                if (item is vdText)
                {
                    vdText t = item as vdText;
                    t.Height = Text_Size;
                    t.Update();
                }
                if (item is vdCircle)
                {
                    //vdCircle t = item as vdCircle;
                    //t.Radius = Text_Size * 0.3;
                    //t.Update();
                }

            }
            VDoc.Redraw(true);
        }
        private void chk_joints_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = sender as CheckBox;

                //VDoc.Layers.FindName(chk.Name.Substring(3)).Frozen = !chk.Checked;


                vdDocument VD = VDoc;

                if (tc_parrent.SelectedTab == tab_post_procees)
                    VD = ActiveDoc;
                //if (tc4.SelectedIndex == 1)
                //    VD = defDoc;


                for (int i = 0; i < VD.Layers.Count; i++)
                {
                    if (VD.Layers[i].Name.ToLower().StartsWith(chk.Text.Substring(0, 4).ToLower()))
                    {
                        VD.Layers[i].On = chk.Checked;
                        if (chk.Text.StartsWith("E"))
                        {
                            if (chk.Checked)
                                VDRAW.vdCommandAction.View3D_ShadeOn(VD);
                            else
                                VDRAW.vdCommandAction.View3D_Wire2d(VD);
                        }

                        //VD.Layers[i].Frozen = !chk.Checked;
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
        private void cmb_text_size_SelectedIndexChanged(object sender, EventArgs e)
        {

            vdDocument VD = ActiveDoc;

            foreach (var item in VD.ActionLayout.Entities)
            {
                if (item is vdMText)
                {
                    vdMText t = item as vdMText;
                    t.Height = PP_Text_Size;
                    t.Update();
                }
                if (item is vdText)
                {
                    vdText t = item as vdText;
                    t.Height = PP_Text_Size;
                    t.Update();
                }
                if (item is vdCircle)
                {
                    vdCircle t = item as vdCircle;
                    t.Radius = PP_Text_Size * 0.3;
                    t.Update();
                }

            }
            VD.Redraw(true);
        }
        private void btn_elmt_add_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;

            if (btn.Name == btn_elmt_add.Name)
            {
                frmElements fs = new frmElements(ACad);
                fs.Owner = this;
                fs.DGV = dgv_elements;
                fs.ShowDialog();
                //if (fs.ASTRA_Data.Count > 0)
                //    tv_mem_props.Nodes.Add(fs.ASTRA_Data[0]);
            }
            else if (btn.Name == btn_elmt_delete.Name)
            {
                //tv_elem_props.Nodes.Remove(tv_elem_props.SelectedNode);

                if (dgv_elements.SelectedCells.Count > 0)
                    dgv_elements.Rows.RemoveAt(dgv_elements.SelectedCells[0].RowIndex);
                else if (dgv_elements.CurrentCell.RowIndex > -1)
                    dgv_elements.Rows.RemoveAt(dgv_elements.CurrentCell.RowIndex);

                Draw_Joints();

            }
            else if (btn.Name == btn_elmt_edit.Name)
            {
                frmElements fs = new frmElements(ACad);
                fs.Owner = this;
                fs.DGV = dgv_elements;
                if (dgv_elements.SelectedCells.Count > 0)
                    fs.ASTRA_Data.Add(dgv_elements.SelectedCells[0].RowIndex.ToString());
                else
                    fs.ASTRA_Data.Add(dgv_elements.CurrentCell.RowIndex.ToString());
                fs.ShowDialog();
            }
            else if (btn.Name == btn_elmt_insert.Name)
            {
                frmElements fs = new frmElements(ACad);
                fs.Owner = this;
                fs.DGV = dgv_elements;
                fs.ASTRA_Data.Add("INSERT");
                fs.ShowDialog();
            }
            Draw_Elements();
        }

        public void ShowMember(string memText, double cirRadius)
        {

            List<int> mbrs = new List<int>();

            MyStrings MS = new MyStrings(memText, ';');

            foreach (var item in MS.StringList)
            {
                mbrs.AddRange(MyStrings.Get_Array_Intiger(item).ToArray());
            }


            if (mbrs.Count == 0)
            {
                MyStrings ml = new MyStrings(memText, ' ');
                string mm = "";
                for (int i = 0; i < tv_mem_grps.Nodes.Count; i++)
                {
                    mm = tv_mem_grps.Nodes[i].Text;

                    if (mm.StartsWith(ml.StringList[0]))
                    {
                        mm = mm.Replace(ml.StringList[0], "");
                        mbrs = MyStrings.Get_Array_Intiger(mm);
                    }
                }
            }





            AST_DOC.Members.SetLayers(VDoc);

            Delete_Layer_Items("Selection");

            MemberIncidence minc = new MemberIncidence();

            foreach (var item in mbrs)
            {
                try
                {
                    minc = AST_DOC.Members.Get_Member(item);
                    if (minc != null)
                    {
                        vdCircle cirMember = new vdCircle();
                        cirMember.SetUnRegisterDocument(VDoc);
                        cirMember.setDocumentDefaults();

                        cirMember.Layer = VDoc.Layers.FindName("Selection");

                        cirMember.PenColor = new vdColor(Color.LightCoral);
                        cirMember.PenColor = new vdColor(Color.IndianRed);
                        cirMember.Center = minc.StartNode.Point;
                        cirMember.Radius = cirRadius;
                        cirMember.Thickness = gPoint.Distance3D(minc.StartNode.Point,
                            minc.EndNode.Point);

                        cirMember.ExtrusionVector = Vector.CreateExtrusion(minc.StartNode.Point,
                            minc.EndNode.Point);
                        VDoc.ActiveLayOut.Entities.AddItem(cirMember);
                    }

                }
                catch (Exception exx) { return; }


                //cirMember.Update();
                VDoc.Redraw(true);
            }
        }

        private void tsmi_openAnalysisTXTDataFile_Click(object sender, EventArgs e)
        {

        }

        private void tv_mem_props_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //if (AST_DOC.MemberGroups.Count > 0)
            //{
            //    //MyList mli = new MyList(tv_mem_props.SelectedNode.Text, ' ');

            //    //AST_DOC.Members.Groups.Table[
            //}

            ShowMember(e.Node.Text, Text_Size * 0.3);
        }

        private void tsmi_saveDXFDrawingFile_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                //sfd.Filter = "VDML File (*.vdml)|*.vdml|DXF File (*.dxf)|*.dxf";
                sfd.Filter = "VDML File (*.vdml)|*.vdml|DXF File (*.dxf)|*.dxf";
                if (sfd.ShowDialog() != DialogResult.Cancel)
                {
                    if (HASP_Lock.IsDemoVersion())
                    {
                        MessageBox.Show("This function is not available in Demo version.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (VDoc.SaveAs(sfd.FileName))
                        {
                            MessageBox.Show("File Saved successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }
        //bool FirstOpen = false;
        private void tv_mem_props_Click(object sender, EventArgs e)
        {
            TreeView tv = sender as TreeView;
            if (tv != null)
            {
                if (tv.SelectedNode != null)
                    ShowMember(tv.SelectedNode.Text, Text_Size * 0.3);
            }
        }
        private void tsmi_newAnalysisTXTDataFile_Click(object sender, EventArgs e)
        {

        }

        #endregion Pre Process

        #region Process
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

        public bool Check_Coordinate(int JointNo, int MemberNo)
        {
            List<int> ListJoints = new List<int>();

            #region ListJoints
            ListJoints.Add(16);
            ListJoints.Add(16);
            ListJoints.Add(22);
            ListJoints.Add(16);
            ListJoints.Add(20);
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
            ListJoints.Add(441);
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
            ListMembers.Add(774);
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
            //MessageBox.Show(HeadsUtils.Constants.BuildType.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

            //if (HASP_Lock.IsDemoVersion())
            if (HASP_Lock.IsDemoVersion())
            {
                if (!Check_Coordinate(dgv_joints.RowCount, (dgv_members.RowCount)))
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

        private void btn_process_analysis_Click(object sender, EventArgs e)
        {
            if (Check_Demo_Version()) return;
            RunAnalysis();
            AST_DOC = new ASTRADoc(DataFileName);
            AST_DOC_ORG = new ASTRADoc(DataFileName);
            Load_ASTRA_Data();
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
            if (!File.Exists(File_Name)) return;
            string patFile = Path.Combine(Application.StartupPath, "PAT001.tmp");

            string fName = File_Name;
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

                if (File.Exists(Analysis_File_Name))
                {
                    rtb_ana_rep.Lines = File.ReadAllLines(Analysis_File_Name);
                    StructureAnalysis = null;
                    ld = null;
                    Select_Steps();
                }
            }
            catch (Exception exx)
            {
            }
        }

        public void Select_Steps()
        {
            List<int> Step_Lines = new List<int>();
            Hashtable hash_index = new Hashtable();
            //Items.Clear();
            //Items.Add("Select...Step.....");

            List<string> Items = new List<string>();

            List<string> analysis_list = new List<string>();
            #region analysis result list
            analysis_list.Add(string.Format("User's data"));
            analysis_list.Add(string.Format("JOINT COORDINATE"));
            analysis_list.Add(string.Format("MEMBER INCIDENCES"));
            analysis_list.Add(string.Format("JOINT COORD"));
            analysis_list.Add(string.Format("MEMBER INCI"));
            analysis_list.Add(string.Format("MEMB INCI"));
            analysis_list.Add(string.Format("START GROUP DEFINITION"));
            analysis_list.Add(string.Format("MEMBER PROPERTY"));
            analysis_list.Add(string.Format("CONSTANT"));
            analysis_list.Add(string.Format("SUPPORT"));
            //analysis_list.Add(string.Format("LOAD"));
            analysis_list.Add(string.Format("DEFINE MOVING LOAD FILE LL.TXT"));
            analysis_list.Add(string.Format("LOAD GENERATION"));
            analysis_list.Add(string.Format("C O N T R O L   I N F O R M A T I O N"));
            analysis_list.Add(string.Format("NODAL POINT INPUT DATA"));
            analysis_list.Add(string.Format("GENERATED NODAL DATA"));
            analysis_list.Add(string.Format("EQUATION NUMBERS"));
            analysis_list.Add(string.Format("ELEMENT LOAD MULTIPLIERS"));
            analysis_list.Add(string.Format("TRUSS ELEMENT DATA"));
            analysis_list.Add(string.Format("3 / D   B E A M   E L E M E N T S"));
            analysis_list.Add(string.Format("MATERIAL PROPERTIES"));
            analysis_list.Add(string.Format("BEAM GEOMETRIC PROPERTIES"));
            analysis_list.Add(string.Format("ELEMENT LOAD MULTIPLIERS"));
            analysis_list.Add(string.Format("3/D BEAM ELEMENT DATA"));
            analysis_list.Add(string.Format("E Q U A T I O N   P A R A M E T E R S"));
            analysis_list.Add(string.Format("N O D A L   L O A D S   (S T A T I C)   O R   M A S S E S   (D Y N A M I C)"));
            analysis_list.Add(string.Format("N O D E   D I S P L A C E M E N T S / R O T A T I O N S"));
            analysis_list.Add(string.Format("TRUSS MEMBER ACTIONS"));
            analysis_list.Add(string.Format(".....BEAM FORCES AND MOMENTS"));
            analysis_list.Add(string.Format("  SHELL ELEMENT STRESSES"));
            analysis_list.Add(string.Format("....8 NODE SOLID ELEMENT DATA"));
            analysis_list.Add(string.Format(".....8-NODE SOLID ELEMENT STRESSES"));
         
            analysis_list.Add(string.Format(" THIN  PLATE/SHELL  ELEMENT DATA"));
            analysis_list.Add(string.Format(" T H I N   P L A T E / S H E L L   E L E M E N T S"));
            analysis_list.Add(string.Format("S T A T I C   S O L U T I O N   T I M E   L O G"));
            analysis_list.Add(string.Format("O V E R A L L   T I M E   L O G"));
            analysis_list.Add(string.Format("SUMMARY OF MAXIMUM SUPPORT FORCES"));



            analysis_list.Add(string.Format("WEIGHT CALCULATION OF SUPER IMPOSED DEAD LOAD"));
            analysis_list.Add(string.Format("WEIGHT CALCULATION OF STEEL STRUCTURE LOAD"));
            //analysis_list.Add(string.Format("CROSS GIRDER"));
            //analysis_list.Add(string.Format("STRINGER BEAM"));
            //analysis_list.Add(string.Format("BOTTOM CHORD"));
            //analysis_list.Add(string.Format("TOP CHORD"));
            //analysis_list.Add(string.Format("END RAKERS"));
            //analysis_list.Add(string.Format("DIAGONAL MEMBER"));
            //analysis_list.Add(string.Format("VERTICAL MEMBER"));
            //analysis_list.Add(string.Format("TOP CHORD BRACINGS"));
            //analysis_list.Add(string.Format("BOTTOM CHORD BRACINGS"));

            analysis_list.Add(string.Format("STRINGER BEAM"));
            analysis_list.Add(string.Format("CROSS GIRDER"));
            analysis_list.Add(string.Format("BOTTOM CHORD"));
            analysis_list.Add(string.Format("TOP CHORD"));
            analysis_list.Add(string.Format("END RAKERS"));
            analysis_list.Add(string.Format("DIAGONAL MEMBER"));
            analysis_list.Add(string.Format("VERTICAL MEMBER"));
            analysis_list.Add(string.Format("TOP CHORD BRACINGS"));
            analysis_list.Add(string.Format("BOTTOM CHORD BRACINGS"));
            analysis_list.Add(string.Format("CANTILEVER BRACKETS"));
            analysis_list.Add(string.Format("SHORT VERTICAL MEMBER"));
            analysis_list.Add(string.Format("TOP DIAGONAL MEMBER"));
            analysis_list.Add(string.Format("BOTTOM DIAGONAL MEMBER"));
            analysis_list.Add(string.Format("TOP VERTICAL MEMBER"));
            analysis_list.Add(string.Format("BOTTOM VERTICAL MEMBER"));
            analysis_list.Add(string.Format("SHORT DIAGONAL MEMBER"));
            analysis_list.Add(string.Format("ARCH MEMBERS"));
            analysis_list.Add(string.Format("SUSPENSION CABLES"));
            analysis_list.Add(string.Format("TRANSVERSE MEMBER"));

            analysis_list.Add(string.Format("MEMBER GROUP"));



            #endregion


            List<string> list = new List<string>(rtb_ana_rep.Lines);
            //list.Sort();
            int indx = 0;

            for (int i = 0; i < list.Count; i++)
            {

                var item = list[i];

                if (item.StartsWith("*")) continue;
                //indx += item.Length + 1;
                indx += item.Length;
                if (item.ToUpper().StartsWith("STEP") ||
                   item.ToUpper().StartsWith("TABLE") ||
                   item.ToUpper().StartsWith("STAGE") ||
                    //item.ToUpper().StartsWith("DESIGN") ||
                    item.ToUpper().StartsWith("USER"))
                {
                    if (!Items.Contains(item))
                    {
                        Step_Lines.Add(i);
                        Items.Add(item);
                        hash_index.Add(Items.Count - 1, indx);
                    }
                }
                else
                {
                    foreach (var l in analysis_list)
                    {
                        //if (item.ToUpper().Contains(l.ToUpper()))
                        if (item.Contains(l.ToUpper()))
                        {
                            if (!Items.Contains(item))
                            {
                                Step_Lines.Add(i);
                                Items.Add(item);
                                hash_index.Add(Items.Count - 1, indx);
                            }
                        }
                    }
                }
            }
            list.Clear();
            lsv_steps.Items.Clear();
            foreach (var item in Items)
            {
                lsv_steps.Items.Add(item.Trim().TrimStart().ToString());
            }
            //if (lsv_steps.Items.Count > 0)
            //{
            //    lsv_steps.Items.RemoveAt(0);
            //    //cmb_step.SelectedIndex = 0;
            //}
        }
        private void select_text(string txt)
        {
            try
            {
                RichTextBox rtbData = rtb_ana_rep;
                int indx = rtbData.Find(txt);
                //if (hash_index[cmb_step.SelectedIndex] != null)
                if (indx != -1)
                {
                    //rtbData.SelectedText = cmb_step.Text;
                    //rtbData.Select((int)hash_index[cmb_step.SelectedIndex], cmb_step.Text.Length);
                    rtbData.Select(indx, txt.Length);
                    rtbData.ScrollToCaret();
                    //rtbData.SelectionBackColor = Color.Red;
                    rtbData.SelectionBackColor = Color.YellowGreen;

                    //rtbData.SelectionLength = cmb_step.Text.Length;

                    //Lines.Remove(cmb_step.Items[0].ToString());
                    //if (Lines.Contains(txt)) Lines.Remove(txt);
                    //Lines.Add(txt);
                    //Show_Next_Previous_Text();
                }
            }
            catch (Exception ex) { }
        }
        private void lstb_steps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsv_steps.SelectedItems.Count > 0)
            {
                select_text(lsv_steps.SelectedItems[0].Text.ToString());
                //CurrentPosition = Lines.Count - 1;
            }
        }

        private void tc_pp_main_SelectedIndexChanged(object sender, EventArgs e)
        {
            //sc3.Panel2Collapsed = (tc6.SelectedTab == tab_file_open);

            //if (tc4.TabPages.Count > 2)
            //{
            //    tc4.TabPages.Remove(tab_evlp_doc);
            //}

            //if (tc_parrent.SelectedIndex != 2) return;


            if (tc_parrent.SelectedTab != tab_post_procees) return;


            if (!File.Exists(Analysis_File_Name))
            {
                MessageBox.Show("Process Analysis not done. This Panel enabled after Process Analysis.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //MessageBox.Show("Process Analysis not done. \"ANALYSIS_REP.TXT\"   file not found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (File.Exists(File_Name))
                    tc_parrent.SelectedTab = tab_procees;
                else
                    tc_parrent.SelectedTab = tab_pre_process;
                return;
            }


            tc1_pp.TabPages.Clear();


            if (tc_pp_main.SelectedTab == tab_forces)
            {
                PP_Show_Panel(tab1_forces);
                if (frcsDoc != null)
                {
                    //frcsDoc.ActiveLayOut.Entities.RemoveAll();

                    if (frcsDoc.ActiveLayOut.Entities.Count == 0)
                    {
                        if (AST_DOC_ORG != null)
                        {
                            AST_DOC_ORG.Joints.DrawJointsText(frcsDoc, PP_Text_Size);
                            AST_DOC_ORG.Members.DrawMember(frcsDoc, PP_Text_Size);
                            AST_DOC_ORG.Elements.DrawElements(frcsDoc);
                            AST_DOC_ORG.Supports.DrawSupport(frcsDoc);
                        }
                        else
                        {
                            AST_DOC.Joints.DrawJointsText(frcsDoc, PP_Text_Size);
                            AST_DOC.Members.DrawMember(frcsDoc, PP_Text_Size);
                            AST_DOC.Elements.DrawElements(frcsDoc);
                            AST_DOC.Supports.DrawSupport(frcsDoc);
                        }
                        ActiveDoc.Redraw(true);
                        VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(frcsDoc);
                    }
                }
            }
            else if (tc_pp_main.SelectedTab == tab_load_deflection)
            {
                PP_Show_Panel(tab1_load_deflection);
            }
            else if (tc_pp_main.SelectedTab == tab_max_force)
            {
                //tc_docs.SelectedTab = tab_max_doc;
                PP_Show_Panel(tab1_max_force);

                if (maxDoc != null)
                {
                    //maxDoc.ActiveLayOut.Entities.RemoveAll();

                    if (maxDoc.ActiveLayOut.Entities.Count == 0)
                    {
                        if (AST_DOC_ORG != null)
                        {
                            AST_DOC_ORG.Joints.DrawJointsText(maxDoc, PP_Text_Size);
                            AST_DOC_ORG.Members.DrawMember(maxDoc, PP_Text_Size);
                            AST_DOC_ORG.Elements.DrawElements(maxDoc);
                            AST_DOC_ORG.Supports.DrawSupport(maxDoc);
                        }
                        else
                        {
                            AST_DOC.Joints.DrawJointsText(maxDoc, PP_Text_Size);
                            AST_DOC.Members.DrawMember(maxDoc, PP_Text_Size);
                            AST_DOC.Elements.DrawElements(maxDoc);
                            AST_DOC.Supports.DrawSupport(maxDoc);

                        }

                        maxDoc.Redraw(true);
                        VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(maxDoc);
                    }
                }

            }
            else if (tc_pp_main.SelectedTab == tab_moving_load)
            {
                PP_Show_Panel(tab1_moving_load);
            }
            else if (tc_pp_main.SelectedTab == tab_envelop)
            {
                PP_Show_Panel(tab1_truss_env);
            }
            else if (tc_pp_main.SelectedTab == tab_diagram)
            {
                PP_Show_Panel(tab1_diag);
            }

            //SetTextSize((cmb_text_size.SelectedIndex + 2) * 0.50);
            SetTextSize(PP_Text_Size);

            //return;
            try
            {
                //tc2.SelectedIndex = tc1.SelectedIndex;
                PP_Tab_Selection();
            }
            catch (Exception ex) { }
        }

        #endregion Process

        #region Post Process

        string file_name = "";
        ASTRADoc astDoc;
        StructureMemberAnalysis StructureAnalysis { get; set; }
        //public ASTRADoc AST_DOC
        //{

        //    get
        //    {
        //        if (tc4.SelectedIndex == 0 && AST_DOC_ORG != null)
        //            return AST_DOC_ORG;
        //        return astDoc;
        //    }
        //    set
        //    {
        //        astDoc = value;
        //    }
        //}

        public ASTRADoc AST_DOC_ORG { get; set; }

        bool bIsNext = true;
        LoadDeflection ld = null;
        double max_x = 0.0;
        double max_y = 0.0;

        //int lastId = -1;

        //public vdDocument VDoc
        //{
        //    get
        //    {
        //        return vdScrollableControl1.BaseControl.ActiveDocument;
        //    }
        //}

        public vdDocument frcsDoc
        {
            get
            {
                return vdSC_frcs.BaseControl.ActiveDocument;
            }
        }

        public vdDocument defDoc
        {
            get
            {
                return vdSC_ldef.BaseControl.ActiveDocument;
            }
        }
        public vdDocument envDoc
        {
            get
            {
                return vdSC_env.BaseControl.ActiveDocument;
            }
        }
        public vdDocument maxDoc
        {
            get
            {
                return vdSC_maxf.BaseControl.ActiveDocument;
            }
        }


        public vdDocument movDoc
        {
            get
            {
                return vdSC_mov.BaseControl.ActiveDocument;
            }
        }

        public vdDocument diagDoc
        {
            get
            {
                return vdSC_diag.BaseControl.ActiveDocument;
            }
        }



        public vdDocument ActiveDoc
        {
            get
            {

                if (tc_parrent.SelectedTab == tab_pre_process)
                    return VDoc;
                if (tc_docs.SelectedTab == tab_frcs_doc)
                    return frcsDoc;
                else if (tc_docs.SelectedTab == tab_defl_doc)
                    return defDoc;
                else if (tc_docs.SelectedTab == tab_max_doc)
                    return maxDoc;
                else if (tc_docs.SelectedTab == tab_mov_doc)
                    return movDoc;
                else if (tc_docs.SelectedTab == tab_evlp_doc)
                    return envDoc;
                else if (tc_docs.SelectedTab == tab_diagram)
                    return diagDoc;

                return VDoc;

                //if(
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

        bool IsMovingLoad = true;


        public void Set_Moving_Load_File(string file_name, bool moving_load)
        {
            InitializeComponent();
            Base_Control_MouseEvent();
            AST_DOC = new ASTRADoc(file_name);
            IsMovingLoad = moving_load;
        }
        public void Set_Moving_Load_File()
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
            vdSC_frcs.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
            vdSC_frcs.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);

            vdSC_ldef.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
            vdSC_ldef.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);

            vdSC_mov.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
            vdSC_mov.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);

            vdSC_maxf.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
            vdSC_maxf.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);
        }

        private void frm_ASTRA_Analysis_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < tc1_pp.TabPages.Count; i++)
            {
                tc1_pp.TabPages[i].Text = "";
            }

            if (AST_DOC != null)
            {

                File_Name = AST_DOC.FileName;

                if (File.Exists(File_Name))
                    rtb_input_file.Lines = File.ReadAllLines(File_Name);
                tc1_pp.Visible = true;
                StructureAnalysis = null;
                ld = null;
                if (File.Exists(LL_TXT) && astDoc.IsMovingLoad)
                    rtb_ll_txt.Lines = File.ReadAllLines(LL_TXT);
                if (File.Exists(Analysis_File_Name))
                    rtb_ana_rep.Lines = File.ReadAllLines(Analysis_File_Name);

                Load_ASTRA_Data();

                cmb_Base_LUnit.SelectedIndex = (int)AST_DOC.Base_LengthUnit;
                cmb_Base_MUnit.SelectedIndex = (int)AST_DOC.Base_MassUnit;

                MyStrings ml = new MyStrings(astDoc.ProjectTitle, ' ');
                txtUserTitle.Text = ml.GetString(2).Trim();
                cmb_structure_type.SelectedItem = ml.StringList[1];
            }
        }

        private void Load_ASTRA_Data()
        {

            if (frcsDoc != null)
            {
                frcsDoc.ActiveLayOut.Entities.RemoveAll();
                frcsDoc.Redraw(true);
            }
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
            if (diagDoc != null)
            {
                diagDoc.ActiveLayOut.Entities.RemoveAll();
                diagDoc.Redraw(true);
            }
            if (AST_DOC.Joints.Count == 0 ||
                AST_DOC.Members.Count == 0)
            {
                //MessageBox.Show("This file is not in correct format. Please select another file.", "ASTRA", MessageBoxButtons.OK);
                //this.Close();
                return;
            }



            VDoc.Palette.Background = Color.White;
            //DefDoc.Palette.Background = Color.White;   +
            astDoc = AST_DOC;
            AST_DOC.MemberProperties.CopyMemberIncidence(astDoc.Members);
            AST_DOC.Members.DrawMember(VDoc, Text_Size);
            //AST_DOC.Members.DrawMember(VDoc);
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
                chk_elems.Checked = true;

            //for (int i = 1; i < 100; i++) cmb_text_size.Items.Add(i);

            //cmb_text_size.SelectedIndex = 1;
            timer1.Start();
        }

        private void PP_toolStripButtons_Click(object sender, EventArgs e)
        {

            vdDocument VD = ActiveDoc;

            //if (tc4.SelectedIndex == 1) VD = defDoc;
            //else if (tc4.SelectedIndex == 2) VD = envDoc;


            VD = ActiveDoc;

            ToolStripButton tsb = sender as ToolStripButton;
            if (tsb.Name == tsb_pp_3D_rotate.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Vrot(VD);
            else if (tsb.Name == tsb_pp_VTop.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(VD);
            else if (tsb.Name == tsb_pp_VBot.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VBottom(VD);
            else if (tsb.Name == tsb_pp_VLeft.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VLeft(VD);
            else if (tsb.Name == tsb_pp_VRight.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VRight(VD);
            else if (tsb.Name == tsb_pp_VFront.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VFront(VD);
            else if (tsb.Name == tsb_pp_VBack.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VBack(VD);
            else if (tsb.Name == tsb_pp_VNE.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VINE(VD);
            else if (tsb.Name == tsb_pp_VNW.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VINW(VD);
            else if (tsb.Name == tsb_pp_VSE.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VISE(VD);
            else if (tsb.Name == tsb_pp_VSW.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VISW(VD);
            else if (tsb.Name == tsb_pp_ZoomA.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomA_Ex(VD);
            else if (tsb.Name == tsb_pp_ZoomE.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomE_Ex(VD);
            else if (tsb.Name == tsb_pp_ZoomP.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomP_Ex(VD);

            else if (tsb.Name == tsb_pp_ZoomW.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomW_Ex(VD);

            else if (tsb.Name == tsb_pp_ZoomIn.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomIn_Ex(VD);
            else if (tsb.Name == tsb_pp_ZoomOut.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomOut_Ex(VD);

            else if (tsb.Name == tsb_pp_Pan.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.PanEx(VD);
            else if (tsb.Name == tsb_pp_Save.Name)
            {
                SaveDrawing(VD);
            }
            else if (tsb.Name == tsb_pp_ShadeOn.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_ShadeOn(VD);
            else if (tsb.Name == tsb_pp_Wire.Name)
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Wire(VD);

        }

        private static void SaveDrawing(vdDocument VD)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "VDML File (*.vdml)|*.vdml|DXF File (*.dxf)|*.dxf|DWG File (*.dwg)|*.dwg";
                if (sfd.ShowDialog() != DialogResult.Cancel)
                {
                    if (HASP_Lock.IsDemoVersion())
                    {
                        MessageBox.Show("This function is not available in Demo version.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (VD.SaveAs(sfd.FileName))
                        {
                            MessageBox.Show("File Saved successfully.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private void frm_ASTRA_Analysis_SizeChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }




        #region Loading
        private void Set_Loadings()
        {

            TreeNode tn = null;
            TreeNode tjn = null;

            //TreeView tv_loads = tv_loads;
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


        private void btn_open_ana_rep_Click(object sender, EventArgs e)
        {
            string open_file = "";

            Button btn = sender as Button;

            //if (btn.Name == btn_open_ana_rep.Name)
            //{
            //    open_file = Path.Combine(Path.GetDirectoryName(AST_DOC.FileName), "ANALYSIS_REP.TXT");
            //}

            if (File.Exists(open_file))
                System.Diagnostics.Process.Start(open_file);
        }

        private void PP_chk_joints_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = sender as CheckBox;

                //VDoc.Layers.FindName(chk.Name.Substring(3)).Frozen = !chk.Checked;

                vdDocument VD = VDoc;

                if (tc_docs.SelectedIndex == 1)
                    VD = defDoc;


                for (int i = 0; i < VD.Layers.Count; i++)
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
        public double PP_Text_Size
        {
            get
            {
                return (cmb_pp_text_size.SelectedIndex + 1) * 0.1;
            }
        }

        private void PP_cmb_text_size_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //double d = double.Parse(cmb_text_size.Text);
                SetTextSize(Text_Size);
            }
            catch (Exception ex)
            { }
        }

        void PP_Show_Panel(TabPage tp)
        {
            tc_docs.TabPages.Clear();
            if (tp == tab1_forces)
            {
                tc_docs.TabPages.Add(tab_frcs_doc);
            }
            else if (tp == tab1_load_deflection)
            {
                tc_docs.TabPages.Add(tab_defl_doc);
            }
            else if (tp == tab1_max_force)
            {
                tc_docs.TabPages.Add(tab_max_doc);
            }
            else if (tp == tab1_moving_load)
            {
                tc_docs.TabPages.Add(tab_mov_doc);
            }
            else if (tp == tab1_truss_env)
            {
                tc_docs.TabPages.Add(tab_evlp_doc);
            }
            else if (tp == tab1_diag)
            {
                tc_docs.TabPages.Add(tab_diag_doc);
            }

            tc1_pp.TabPages.Clear();
            tc1_pp.TabPages.Add(tp);
        }


        private void PP_tc1_SelectedIndexChanged(object sender, EventArgs e)
        {

            //sc3.Panel2Collapsed = (tc6.SelectedTab == tab_file_open);

            if (tc_docs.TabPages.Count > 2)
            {
                tc_docs.TabPages.Remove(tab_evlp_doc);
            }


            tc_prp_panel.TabPages.Clear();
            if (tc_prp_main.SelectedTab == tab_forces)
                Show_Panel(tab1_forces);
            else if (tc_prp_main.SelectedTab == tab_load_deflection)
            {
                Show_Panel(tab1_load_deflection);
                tc_docs.SelectedTab = tab_defl_doc;
            }
            else if (tc_prp_main.SelectedTab == tab_max_force)
            {

                tc_docs.SelectedTab = tab_max_doc;
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
            else if (tc_prp_main.SelectedTab == tab_moving_load)
            {
                Show_Panel(tab1_moving_load);
                tc_docs.SelectedTab = tab_mov_doc;
            }
            else if (tc_prp_main.SelectedTab == tab_envelop)
            {
                Show_Panel(tab1_truss_env);
                tc_docs.TabPages.Add(tab_evlp_doc);
                tc_docs.SelectedTab = tab_evlp_doc;
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

        private void PP_Tab_Selection()
        {

            if (tc_pp_main.SelectedTab == tab_load_deflection)
            {
                if (ld == null || dgv_node_disp.RowCount == 0)
                {
                    AstrA_LOAD_Deflection();
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(defDoc);
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Vrot(defDoc);
                }
                PP_Show_Panel(tab1_load_deflection);

            }
            else if (tc_pp_main.SelectedTab == tab_moving_load)
            {
                if (AST_DOC.IsMovingLoad)
                {
                    if (StructureAnalysis == null)
                    {
                        StructureAnalysis = new MovingLoadAnalysis.StructureMemberAnalysis(AST_DOC.AnalysisFileName);
                    }
                    //tc_docs.SelectedTab = tab_mov_doc;
                    Set_Moving_Load_Init();
                    PP_Show_Panel(tab1_moving_load);
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(movDoc);
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Vrot(movDoc);
                }
                else
                {
                    tc_pp_main.SelectedTab = tab_forces;
                    MessageBox.Show("Moving Load Data not found in the input file.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (tc_pp_main.SelectedTab == tab_forces ||
                tc_pp_main.SelectedTab == tab_max_force)
            {

                if (StructureAnalysis == null)
                {
                    StructureAnalysis = new MovingLoadAnalysis.StructureMemberAnalysis(AST_DOC.AnalysisFileName);

                    dgv_joint_frcs.Rows.Clear();
                    foreach (var item in StructureAnalysis.list_joints)
                    {
                        dgv_joint_frcs.Rows.Add(item.NodeNo, item.LoadNo, item.FX, item.FY, item.FZ, item.MX, item.MY, item.MZ);

                    }
                    dgv_beam_frcs.Rows.Clear();

                    cmb_diag_mem_no.Items.Clear();
                    cmb_diag_ld_no.Items.Clear();
                    foreach (var item in StructureAnalysis.list_beams)
                    {
                        if (!cmb_diag_ld_no.Items.Contains(item.LoadNo))
                            cmb_diag_ld_no.Items.Add(item.LoadNo);
                        if (!cmb_diag_mem_no.Items.Contains(item.BeamNo))
                            cmb_diag_mem_no.Items.Add(item.BeamNo);

                        dgv_beam_frcs.Rows.Add(StructureAnalysis.Get_User_Member_No(item.BeamNo, MovingLoadAnalysis.eAstraMemberType.BEAM), item.LoadNo,
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
                        dgv_truss_frcs.Rows.Add(StructureAnalysis.Get_User_Member_No(item.TrussMemberNo, MovingLoadAnalysis.eAstraMemberType.TRUSS), item.LoadNo,
                            item.Stress,
                            item.Force);
                    }
                    foreach (var item in StructureAnalysis.list_cables)
                    {
                        dgv_truss_frcs.Rows.Add(StructureAnalysis.Get_User_Member_No(item.TrussMemberNo, MovingLoadAnalysis.eAstraMemberType.CABLE), item.LoadNo,
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

                    dgv_plate_frcs.Rows.Clear();
                    foreach (var item in StructureAnalysis.list_plates)
                    {
                        dgv_plate_frcs.Rows.Add(item.PlateNo, item.LoadNo,
                            item.SXX.ToString("f4"),
                            item.SYY.ToString("f4"),
                            item.SXY.ToString("f4"),
                            item.MXX.ToString("f4"),
                            item.MXY.ToString("f4"),
                            item.MXY.ToString("f4"));
                    }

                }
                if (tc_pp_main.SelectedTab == tab_max_force)
                    PP_Show_Panel(tab1_max_force);
                else
                    PP_Show_Panel(tab1_forces);


            }
            else if (tc_pp_main.SelectedTab == tab_envelop)
            {
                if (StructureAnalysis == null)
                    StructureAnalysis = new MovingLoadAnalysis.StructureMemberAnalysis(AST_DOC.AnalysisFileName);
                //tc4.SelectedIndex = 2;
            }
            else if (tc_pp_main.SelectedTab == tab_diagram)
            {
                if (StructureAnalysis == null)
                    StructureAnalysis = new MovingLoadAnalysis.StructureMemberAnalysis(AST_DOC.AnalysisFileName);


                if (cmb_diag_ld_no.Items.Count > 0)
                {
                    if (cmb_diag_ld_no.SelectedIndex == -1)
                        cmb_diag_ld_no.SelectedIndex = 0;
                }
                if (cmb_diag_mem_no.Items.Count > 0)
                {
                    if (cmb_diag_mem_no.SelectedIndex == -1)
                        cmb_diag_mem_no.SelectedIndex = 0;
                }
                Select_Diagram();
            }
            else
                tc_docs.SelectedIndex = 0;
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

            if (tc_docs.SelectedIndex == 1)
                VD = defDoc;

            try
            {
                //VD.Layers.FindName("MemberLoadGlobal").Frozen = !chkGlobal.Checked;
                //VD.Layers.FindName("MemberLoadLocal").Frozen = !chkLocal.Checked;
            }
            catch (Exception exx) { }
            {
            }

            try
            {

                //VD.Layers.FindName("ConcentrateLocal").Frozen = !chkLocal.Checked;
                //VD.Layers.FindName("ConcentrateGlobal").Frozen = !chkGlobal.Checked;
            }
            catch (Exception ex2) { }
            {
            }
            try
            {
                //VD.Layers.FindName("JointLoad").Frozen = !chkJoint.Checked;
            }
            catch (Exception exx1) { }
            {
            }
            VD.Redraw(true);
        }

        private void PP_timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(VDoc);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Vrot(VDoc);
            //defDoc.Palette.Background = Color.White;
        }

        public void AstrA_LOAD_Deflection()
        {

            string analysisFileName = "";
            //if (astDoc == null)
                astDoc = AST_DOC;
            analysisFileName = Path.GetDirectoryName(astDoc.FileName);
            analysisFileName = astDoc.AnalysisFileName;

            if (!File.Exists(analysisFileName)) return;

            Set_Init();
            if (AST_DOC_ORG.IsDynamicLoad)
            {
                //this.Text = "EIGEN VALUE ANALYSIS";
                //lblLoadCase.Text = "EIGEN VALUE";
                lblLoadCase.Text = "Frequencies : Mode Shapes";
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

                MyStrings m = new MyStrings(cmbInterval.Text, ' ');

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
                    AST_DOC_ORG.Members.DrawMember(movDoc);
                else
                    AST_DOC_ORG.Members.DrawMember(movDoc);
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
        #endregion  Chiranjit [2014 04 10] New Moving load Animation

        private void tc5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tc_parrent.SelectedTab == tab_pre_process)
            {
                sc3.Panel2Collapsed = (tc_prp_main.SelectedTab == tab_file_open);
            }
            else
            {
                if (!File.Exists(File_Name))
                {
                    MessageBox.Show("Please select a file.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    tc_parrent.SelectedTab = tab_pre_process;
                    return;
                }
                //if (tc_parrent.SelectedTab == pa)
                //{
                //    sc3.Panel2Collapsed = true;
                //}
                else if (tc_parrent.SelectedTab == tab_post_procees)
                {
                    if (!File.Exists(Analysis_File_Name))
                    {
                        MessageBox.Show("Process Analysis not done. This Panel enabled after Process Analysis.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //MessageBox.Show("Process Analysis not done. \"ANALYSIS_REP.TXT\"   file not found.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        if (File.Exists(File_Name))
                            tc_parrent.SelectedTab = tab_procees;
                        else
                            tc_parrent.SelectedTab = tab_pre_process;
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
            //tc2.TabPages.Clear();
            //if (tc6.SelectedTab == tab_geometry)
            //    tc2.TabPages.Add(tab1_geometry);
            //else if (tc6.SelectedTab == tab_loading)
            //{
            //    tc2.TabPages.Add(tab1_loading);
            //    tc4.SelectedTab = tab_org_doc;
            //}
            //sc3.Panel2Collapsed = (tc6.SelectedTab == tab_file_open);
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

        private void PP_tsmi_data_open_Click(object sender, EventArgs e)
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
                        tc_prp_panel.Visible = true;
                        sc3.Panel2Collapsed = true;
                        astDoc = new ASTRADoc(File_Name);
                        StructureAnalysis = null;
                        ld = null;
                        if (File.Exists(LL_TXT) && astDoc.IsMovingLoad)
                            rtb_ll_txt.Lines = File.ReadAllLines(LL_TXT);
                        if (File.Exists(Analysis_File_Name))
                            rtb_ana_rep.Lines = File.ReadAllLines(Analysis_File_Name);
                        Load_ASTRA_Data();

                        cmb_Base_LUnit.SelectedIndex = (int)AST_DOC.Base_LengthUnit;
                        cmb_Base_MUnit.SelectedIndex = (int)AST_DOC.Base_MassUnit;

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



                cmb_Base_LUnit.SelectedIndex = (int)AST_DOC.Base_LengthUnit;
                cmb_Base_MUnit.SelectedIndex = (int)AST_DOC.Base_MassUnit;

                MyStrings ml = new MyStrings(astDoc.ProjectTitle, ' ');
                txtUserTitle.Text = ml.GetString(2).Trim();
                cmb_structure_type.SelectedItem = ml.StringList[1];

            }
            //else if (menu_name == tsmi_dwg_save.Name || menu_name == tsb_save_drawing.Name)
            //{
            //    SaveDrawing(ActiveDoc);
            //}
            //else if (menu_name == tsmi_file_print.Name || menu_name == tsb_print_drawing.Name)
            //{
            //    if (ActiveDoc != null)
            //    {
            //        VectorDraw.Professional.ActionUtilities.vdCommandAction.PrintEx(ActiveDoc);
            //    }
            //}
            //else if (menu_name == tsmi_close.Name || menu_name == tsb_close.Name)
            //{
            //    this.Close();
            //}
        }

        private void dgv_max_frc_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgv_max_frc_SelectionChanged(object sender, EventArgs e)
        {
            Set_Max_Force_Column();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void tmrLoadDeflection_Tick(object sender, EventArgs e)
        {

            if (bIsNext)
                SetLoadIndex(+1);
            else
                SetLoadIndex(-1);

        }

        private void PP_tmr_moving_load_Tick(object sender, EventArgs e)
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

        private void cmb_diag_mem_no_SelectedIndexChanged(object sender, EventArgs e)
        {
            Select_Diagram();
        }
        public void Select_Diagram()
        {

            int memNo = MyStrings.StringToInt(cmb_diag_mem_no.Text, 0);
            int loadNo = MyStrings.StringToInt(cmb_diag_ld_no.Text, 0);

            BeamForceMoment bfm = new BeamForceMoment();
            BeamForceMomentCollection bfc = new BeamForceMomentCollection();
            diagDoc.Palette.Background = Color.White;
            //bfc.

            if (StructureAnalysis != null)
            {

                AstraBeamMember bm = null;
                if (memNo != 0 && loadNo != 0)
                {
                    bm = StructureAnalysis.Get_Beam_Forces(memNo, loadNo);

                    bfm.Member.MemberNo = bm.BeamNo;
                    bfm.LoadCase = loadNo;

                    bfm.StartForceMoment.R1 = bm.StartNodeForce.R1_Axial;
                    bfm.StartForceMoment.R2 = bm.StartNodeForce.R2_Shear;
                    bfm.StartForceMoment.R3 = bm.StartNodeForce.R3_Shear;


                    bfm.StartForceMoment.M1 = bm.StartNodeForce.M1_Torsion;
                    bfm.StartForceMoment.M2 = bm.StartNodeForce.M2_Bending;
                    bfm.StartForceMoment.M3 = bm.StartNodeForce.M3_Bending;


                    bfm.EndForceMoment.R1 = bm.EndNodeForce.R1_Axial;
                    bfm.EndForceMoment.R2 = bm.EndNodeForce.R2_Shear;
                    bfm.EndForceMoment.R3 = bm.EndNodeForce.R3_Shear;


                    bfm.EndForceMoment.M1 = bm.EndNodeForce.M1_Torsion;
                    bfm.EndForceMoment.M2 = bm.EndNodeForce.M2_Bending;
                    bfm.EndForceMoment.M3 = bm.EndNodeForce.M3_Bending;

                    bfc.Add(bfm);

                    grb_diag_start.Text = "START NODE : " + bm.StartNodeForce.JointNo;
                    txt_diag_start_FX.Text = bm.StartNodeForce.R1_Axial.ToString("f3");
                    txt_diag_start_FY.Text = bm.StartNodeForce.R2_Shear.ToString("f3");
                    txt_diag_start_FZ.Text = bm.StartNodeForce.R3_Shear.ToString("f3");
                    txt_diag_start_MX.Text = bm.StartNodeForce.M1_Torsion.ToString("f3");
                    txt_diag_start_MY.Text = bm.StartNodeForce.M2_Bending.ToString("f3");
                    txt_diag_start_MZ.Text = bm.StartNodeForce.M3_Bending.ToString("f3");



                    grb_diag_end.Text = "END NODE : " + bm.EndNodeForce.JointNo;
                    txt_diag_end_FX.Text = bm.EndNodeForce.R1_Axial.ToString("f3");
                    txt_diag_end_FY.Text = bm.EndNodeForce.R2_Shear.ToString("f3");
                    txt_diag_end_FZ.Text = bm.EndNodeForce.R3_Shear.ToString("f3");
                    txt_diag_end_MX.Text = bm.EndNodeForce.M1_Torsion.ToString("f3");
                    txt_diag_end_MY.Text = bm.EndNodeForce.M2_Bending.ToString("f3");
                    txt_diag_end_MZ.Text = bm.EndNodeForce.M3_Bending.ToString("f3");


                    bfc.CopyMembers(AST_DOC.Members);


                    BeamForceMomentCollection.eForce fc = BeamForceMomentCollection.eForce.M1;
                    if (rbtn_diag_MX.Checked)
                    {
                        //bfc.DrawBeamForceMoment_M1(PP_ActiveDoc, 0);
                        //fc = BeamForceMomentCollection.eForce.R1;
                        fc = BeamForceMomentCollection.eForce.M1;
                    }
                    if (rbtn_diag_MY.Checked)
                    {
                        //bfc.DrawBeamForceMoment_M2(PP_ActiveDoc, 0);
                        fc = BeamForceMomentCollection.eForce.M2;
                    }
                    if (rbtn_diag_MZ.Checked)
                    {
                        //bfc.DrawBeamForceMoment_M3(PP_ActiveDoc, 0);
                        fc = BeamForceMomentCollection.eForce.M3;
                    }

                    if (rbtn_diag_FX.Checked)
                    {
                        //bfc.DrawBeamForceMoment_R1(PP_ActiveDoc, 0);
                        fc = BeamForceMomentCollection.eForce.R1;
                    }
                    if (rbtn_diag_FY.Checked)
                    {
                        //bfc.DrawBeamForceMoment_R2(PP_ActiveDoc, 0);
                        fc = BeamForceMomentCollection.eForce.R2;
                    }
                    if (rbtn_diag_FZ.Checked)
                    {
                        //bfc.DrawBeamForceMoment_R3(PP_ActiveDoc, 0);
                        fc = BeamForceMomentCollection.eForce.R3;
                    }

                    //bfc.DrawBeamForceMoment_Bending(PP_ActiveDoc, memNo, loadNo, BeamForceMomentCollection.eForce.M1, )

                    bfc.DrawBeamForceMoment(diagDoc, memNo, fc, loadNo);
                }

            }
        }

        private void dgv_max_frc_SelectionChanged(object sender, DataGridViewCellEventArgs e)
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

        private void tv_mem_grps_MouseMove(object sender, EventArgs e)
        {

        }

        private void chk_show_steps_CheckedChanged(object sender, EventArgs e)
        {
            spc_results.Panel1Collapsed = !chk_show_steps.Checked;
        }

        private void btn_update_file_Click(object sender, EventArgs e)
        {
            if (File.Exists(DataFileName))
            {
                File.WriteAllLines(DataFileName, rtb_input_file.Lines);
                Open_Data_File(DataFileName);
                MessageBox.Show("Data File Updated.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        #endregion Post Process

        private void tsmi_viewer_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "VDML Drawing File(*.vdml)|*.vdml|DXF Drawing File(*.dxf)|*.dxf|DWG Drawing File(*.dwg)|*.dwg";
                if (sfd.ShowDialog() != DialogResult.Cancel)
                {
                    if (HASP_Lock.IsDemoVersion())
                    {
                        MessageBox.Show("This function is not available in Demo version.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        VDoc.SaveAs(sfd.FileName);
                        Drawing_File = sfd.FileName;

                        System.Environment.SetEnvironmentVariable("OPENFILE", Drawing_File);
                        //System.Diagnostics.Process.Start("viewer.exe", Drawing_File);
                        System.Diagnostics.Process.Start(Path.Combine(Application.StartupPath, "viewer.exe"));
                    }
                }
            }
        }

        private void lblLoadCase_TextChanged(object sender, EventArgs e)
        {
            lbl_mode_shape.Visible = lblLoadCase.Text.ToLower().StartsWith("e");
        }

        private void btn_view_analysis_Click(object sender, EventArgs e)
        {

            if (File.Exists(Analysis_File_Name))
            {
                System.Diagnostics.Process.Start(Analysis_File_Name);
            }
            else
            {
                MessageBox.Show("Analysis not done.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }

}
