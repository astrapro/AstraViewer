using System;
using System.Collections;
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
using VectorDraw.Professional.vdCollections;
using VectorDraw.Geometry;

using HEADSNeed.ASTRA.ASTRADrawingTools;
using HEADSNeed.ASTRA.ASTRAForms;
using HEADSNeed.ASTRA.ASTRAClasses;
using HEADSNeed.ASTRA.ASTRAClasses.StructureDesign;
using HEADSNeed.ASTRA.CadToAstra.FORMS;
using HEADSNeed.ASTRA.CadToAstra;


namespace HEADSNeed.ASTRA.ASTRAForms
{
    public delegate void sAdd_Slab_BOQ(Slab_BOQ sboq);

    public partial class frm_Slab_BOQ : Form
    {

        public sAdd_Slab_BOQ Add_Slab_Boq;
        ASTRADoc AST_DOC { get; set; }

        public double Main_Bar_Dia_Top { get; set; }
        public double Main_Bar_Dia_Bottom { get; set; }
        public double Stirrup_Bar_Dia { get; set; }

        public SlabDesign Slab_des { get; set; }
        public BillOfQuantity BOQ { get; set; }


        public string BeamNos
        {
            get
            {
                return txt_beam_nos.Text;
            }
            set
            {
                txt_beam_nos.Text = value;
            }
        }
        public frm_Slab_BOQ(ASTRADoc ast_doc)
        {
            InitializeComponent();
            AST_DOC = ast_doc;
            //Main_Bar_Dia_Top = 16;
            //Main_Bar_Dia_Bottom = 20;
            //Stirrup_Bar_Dia = 8;
        }

        public vdDocument VDoc
        {
            get
            {
                return vdScrollableControl1.BaseControl.ActiveDocument;
            }
        }
        private void toolStrip_deign_Click(object sender, EventArgs e)
        {
        
            vdDocument VD = VDoc;

            //if (tc4.SelectedIndex == 1) VD = defDoc;
            //if (tc4.SelectedIndex == 1) VD = defDoc;

            ToolStripButton tsb = sender as ToolStripButton;
            if (tsb.Name.EndsWith("3D_rotate"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Vrot(VD);
            else if (tsb.Name.EndsWith("VTop"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(VD);
            else if (tsb.Name.EndsWith("VBot"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VBottom(VD);
            else if (tsb.Name.EndsWith("VLeft"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VLeft(VD);
            else if (tsb.Name.EndsWith("VRight"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VRight(VD);
            else if (tsb.Name.EndsWith("VFront"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VFront(VD);
            else if (tsb.Name.EndsWith("VBack"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VBack(VD);
            else if (tsb.Name.EndsWith("VNE"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VINE(VD);
            else if (tsb.Name.EndsWith("VNW"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VINW(VD);
            else if (tsb.Name.EndsWith("VSE"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VISE(VD);
            else if (tsb.Name.EndsWith("VSW"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VISW(VD);
            else if (tsb.Name.EndsWith("ZoomA"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomA_Ex(VD);
            else if (tsb.Name.EndsWith("ZoomE"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomE_Ex(VD);
            else if (tsb.Name.EndsWith("ZoomP"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomP_Ex(VD);

            else if (tsb.Name.EndsWith("ZoomW"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomW_Ex(VD);

            else if (tsb.Name.EndsWith("ZoomIn"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomIn_Ex(VD);
            else if (tsb.Name.EndsWith("ZoomOut"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.ZoomOut_Ex(VD);

            else if (tsb.Name.EndsWith("Pan"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.PanEx(VD);
            else if (tsb.Name.EndsWith("ShadeOn"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_ShadeOn(VD);
            else if (tsb.Name.EndsWith("Wire"))
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Wire(VD);
            else if (tsb.Name.EndsWith("Save"))
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "VDML File (*.vdml)|*.vdml|DXF File (*.dxf)|*.dxf|DWG File (*.dwg)|*.dwg";
                    if (sfd.ShowDialog() != DialogResult.Cancel)
                    {
                        if (!HASP_Lock.IsProfessional_StructuralVersion_19())
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

         

        
        private void frm_Slab_BOQ_Load(object sender, EventArgs e)
        {
            vdScrollableControl1.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
            vdScrollableControl1.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);
            vdScrollableControl1.BaseControl.vdKeyUp += new VectorDraw.Professional.Control.KeyUpEventHandler(BaseControl_vdKeyUp);

            List<double> list = new List<double>();
            for (int i = 0; i < AST_DOC.Members.Count; i++)
            {
                var item = AST_DOC.Members[i];

                if (item.EndNode.Y == item.StartNode.Y)
                {
                    if (!list.Contains(item.StartNode.Y))
                        list.Add(item.StartNode.Y);
                    //if (!cmb_flr_lvl.Items.Contains(item.StartNode.Y.ToString("f3")))
                    //    cmb_flr_lvl.Items.Add(item.StartNode.Y.ToString("f3"));
                }

            }
            list.Sort();
            foreach (var item in list)
            {

                if (!cmb_flr_lvl.Items.Contains(item.ToString("f4")))
                    cmb_flr_lvl.Items.Add(item.ToString("f4"));
            }
            if (cmb_flr_lvl.Items.Count > 0)
            {
                cmb_flr_lvl.SelectedIndex = 0;
                Draw_Members();
            }

            for (int i = 1; i < 100; i++)
            {
                cmb_textsize.Items.Add(i);
            }
            cmb_textsize.SelectedIndex = 1;
            Load_Slab_Data();
            //Get_Beams();

            Draw_Floor_Layout();

            cmb_slab_type.SelectedIndex = 1;




            txt_slab_D.Text = Slab_des.D.ToString(); ;
            cmb_slab_fck.SelectedItem = "M" + Slab_des.fck;
            txt_slab_gamma.Text = Slab_des.gamma.ToString(); 
            cmb_slab_fy.SelectedItem = "Fe" + Slab_des.fy;
            txt_slab_wll.Text = Slab_des.wll.ToString(); 
            txt_slab_d1.Text = Slab_des.d1.ToString(); 
            txt_slab_s1.Text = Slab_des.s1.ToString(); 
            txt_slab_d2.Text = Slab_des.d2.ToString(); 
            txt_slab_s2.Text = Slab_des.s2.ToString(); 
            txt_slab_d3.Text = Slab_des.d3.ToString(); 
            txt_slab_s3.Text = Slab_des.s3.ToString(); 
            txt_slab_d4.Text = Slab_des.d4.ToString(); 
            txt_slab_s4.Text = Slab_des.s4.ToString(); 
            txt_slab_c.Text = Slab_des.cover.ToString(); 
            txt_slab_DLF.Text = Slab_des.dlf.ToString(); 
            txt_slab_LLF.Text = Slab_des.llf.ToString(); 


            for (int r = 0; r < Slab_des.dgv_dls.Rows.Count - 1; r++)
            {
                dgv_dls.Rows.Add(Slab_des.dgv_dls[0, r].Value, Slab_des.dgv_dls[1, r].Value);
            }

            if (cmb_flr_lvl.Items.Count > 0)
            {
                double d = MyStrings.StringToDouble(cmb_flr_lvl.Text, 0.0);
                Draw_Floor_Layout(d);
                //Get_Beams();
                panel1.Visible = true;
            }

            //dgv_dls.Rows.Add("Fixed Load", "4.0");

        }



        private void Draw_Members()
        {
            //throw new NotImplementedException();
        }

        #region CAD Methods
        void BaseControl_vdMouseDown(MouseEventArgs e, ref bool cancel)
        {
            Get_Selected_Beams();

            //if ((System.Windows.Forms.Control.ModifierKeys & Keys.Shift) != 0 && e.Button == MouseButtons.Left)
            //{
            //    vdSelection gripset = GetGripSelection(false);

            //    if (gripset == null) return;
            //    gPoint pt = this.vdScrollableControl1.BaseControl.ActiveDocument.CCS_CursorPos();
            //    int result = this.vdScrollableControl1.BaseControl.ActiveDocument.CommandAction.DoDragDrop(gripset, vdSelection.DragDropEffects.All, pt, 100, 100, 3);
            //    return;
            //}
            //if (e.Button != MouseButtons.Right) return;
            //if ((System.Windows.Forms.Control.ModifierKeys & Keys.Control) != 0) return;
            //if (vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.OverAllActiveActions.Count != 1) return;

            //if ((System.Windows.Forms.Control.ModifierKeys & Keys.Shift) != 0)
            //{
            //    vdSelection gripset = GetGripSelection(false);
            //    if (gripset == null) return;
            //    gPoint pt = this.vdScrollableControl1.BaseControl.ActiveDocument.CCS_CursorPos();
            //    int result = this.vdScrollableControl1.BaseControl.ActiveDocument.CommandAction.DoDragDrop(gripset, vdSelection.DragDropEffects.All, pt, 100, 100, 3);
            //}
            //else
            //{
            //    //if (BaseAction.PressedKeyCode != Keys.None) return; //6011 correction
            //    //MainForm parent = this.MdiParent as MainForm;
            //    //parent.commandLine.PostExecuteCommand("");
            //}
        }
        private vdSelection GetGripSelection(bool Create)
        {
            VectorDraw.Professional.vdCollections.vdSelection gripset;

            vdDocument VD = VDoc;



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
            Get_Selected_Beams();
        }
        void BaseControl_vdKeyUp(KeyEventArgs e, ref bool cancel)
        {
            Get_Selected_Beams();
        }

        #endregion Cad Methods

        public void Get_Selected_Beams()
        {

            foreach (vdFigure fig in VDoc.ActiveLayOut.Entities)
            {
                fig.LineWeight = VectorDraw.Professional.Constants.VdConstLineWeight.LW_BYLAYER;
                fig.Update();
            }

            vdSelection gripset = GetGripSelection(false);
            if (gripset == null) return;

            vdDocument VD = VDoc;

            List<int> members = new List<int>();

            string kStr = "";


            foreach (vdFigure fig in gripset)
            {
                if (fig is vdPolyline)
                {
                    //if (gripset.Count > 1)
                    //    gripset.RemoveAt(0);
                    fig.LineWeight = VectorDraw.Professional.Constants.VdConstLineWeight.LW_158;
                    MyStrings ml = new MyStrings(fig.ToolTip.ToUpper(), ' ');
                    if (ml.StringList[0].StartsWith("MEMBER"))
                    {
                        if(kStr == "")
                            kStr = ml.GetString(3);
                        else
                            kStr += " , " + ml.GetString(3);

                        Fill_Slab(ml.GetString(3));
                        //gripset.RemoveItem(fig);
                    }
                    fig.Update();
                }
                else
                {
                    fig.LineWeight = VectorDraw.Professional.Constants.VdConstLineWeight.LW_158;
                    MyStrings ml = new MyStrings(fig.ToolTip.ToUpper(), ' ');
                    if (ml.StringList[0].StartsWith("MEMBER"))
                    {
                        kStr += ml.GetInt(3) + " ";
                        //if (!members.Contains(ml.GetInt(3)))
                        //    members.Add(ml.GetInt(3));
                    }
                    fig.Update();
                }
            }

            txt_beam_nos.Text = kStr;

            Show_Data();

            VDoc.Redraw(true);
        }
        void HighLight_Member(int BeamNo)
        {
            MyStrings ml = null;
            string kStr = "";
            foreach (vdFigure fig in VDoc.ActiveLayOut.Entities)
            {
                try
                {
                    ml = new MyStrings(fig.ToolTip.ToUpper(), ' ');
                    if (ml.StringList[0].StartsWith("MEMBER"))
                    {
                        if (BeamNo == ml.GetInt(3))
                        {
                            fig.LineWeight = VectorDraw.Professional.Constants.VdConstLineWeight.LW_158;
                            fig.Update();
                        }

                    }
                }
                catch (Exception exx) { }
            }

        }
        private void btn_get_beams_Click(object sender, EventArgs e)
        {
            double d = MyStrings.StringToDouble(cmb_flr_lvl.Text, 0.0);
            Draw_Floor_Layout(d);
            //Get_Beams();
            panel1.Visible = true;

        }

        private void Fill_All_Slab()
        {

            foreach (vdFigure fig in VDoc.ActiveLayOut.Entities)
            {
                if (fig is vdPolyline)
                {
                    fig.Deleted = true;

                }
            }



            string ff = Slab_des.Get_Report_File(txt_beam_nos.Text, cmb_flr_lvl.Text);


            foreach (var item in Directory.GetFiles(Path.GetDirectoryName(ff)))
            {
                ff = Path.GetFileNameWithoutExtension(item);
                if (ff.StartsWith("S"))
                {
                    try
                    {
                        string beams = ff.Replace("S", "");

                        beams = beams.Replace("_", " ").Trim();
                        Fill_Slab(beams);
                    }
                    catch (Exception eex) { }
                }
            }
        }

        private void Get_Beams()
        {
            double d = MyStrings.StringToDouble(cmb_flr_lvl.Text, 0.0);

            VDoc.ActiveLayOut.Entities.RemoveAll();

            panel1.Visible = true;

            MemberIncidenceCollection mic = new MemberIncidenceCollection();
            JointCoordinateCollection jcc = new JointCoordinateCollection();


            MemberIncidence mi;
            foreach (var item in AST_DOC.Members)
            {
                if (item.StartNode.Y == d && item.EndNode.Y == d)
                {

                    //mi = new MemberIncidence();
                    //mi.StartNode = new JointCoordinate(item.StartNode);
                    //mi.EndNode = new JointCoordinate(item.EndNode);

                    //mi.MemberNo = item.MemberNo;
                    //mi.StartNode.Y = mi.StartNode.Z;
                    //mi.EndNode.Y = mi.EndNode.Z;

                    AST_DOC.Members.DrawMember(item, VDoc, TXT_SIZE);
                    //AST_DOC.Members.DrawMember(mi, VDoc, TXT_SIZE);
                    mic.Add(item);

                    if (!jcc.Contains(item.StartNode))
                        jcc.Add(item.StartNode);
                    if (!jcc.Contains(item.EndNode))
                        jcc.Add(item.EndNode);
                }
            }
            jcc.DrawJointsText(VDoc, TXT_SIZE);



            VDoc.Layers.FindName("Members").Lock = true;
            VDoc.Layers.FindName("Nodes").Lock = true;
            //VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(VDoc);

            Set_TextSize();



            //Get_Slabs(mic);
            Fill_All_Slab();
            Load_Slab_Data();



            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VBack(VDoc);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_Vrot(VDoc);
        }
        void Get_Slabs(MemberIncidenceCollection mic)
        {

            List<int> slab_mbrs = new List<int>();


            MemberIncidence mi = mic[0];
            JointCoordinate jc1, jc2;


            jc1 = mi.StartNode;
            jc2 = mi.EndNode;

            MemberIncidenceCollection mc2 = new MemberIncidenceCollection();
            for (int i = 1; i < mic.Count; i++)
            {
                var item = mic[i];

                if (item.StartNode == jc1 || item.EndNode == jc1)
                {
                    mc2.Add(item);
                }
                if (item.StartNode == jc2 || item.EndNode == jc2)
                {
                    mc2.Add(item);
                }
            }

            for (int i = 0; i < mc2.Count; i++)
            {
                var item = mc2[i];
                if (item.Direction == mi.Direction)
                {
                    mc2.RemoveAt(i); i = 0;
                }


            }

            List<int> jnts = new List<int>();

            for (int i = 0; i < mc2.Count; i++)
            {
                var item = mc2[i];

                if (!jnts.Contains(item.StartNode.NodeNo))
                    jnts.Add(item.StartNode.NodeNo);
                if (!jnts.Contains(item.EndNode.NodeNo))
                    jnts.Add(item.EndNode.NodeNo);
            }

            jnts.Remove(jc1.NodeNo);
            jnts.Remove(jc2.NodeNo);

            MemberIncidence mc4 = new MemberIncidence();
            for (int i = 1; i < mic.Count; i++)
            {
                var item = mic[i];

                if (item.StartNode.NodeNo == jnts[0] &&
                    item.EndNode.NodeNo == jnts[1])
                {
                    mc4 = item; break;
                }
                else if (item.EndNode.NodeNo == jnts[0] &&
                    item.StartNode.NodeNo == jnts[1])
                {
                    mc4 = item;
                    break;
                }
            }

            jnts.Remove(jc1.NodeNo);
            jnts.Remove(jc2.NodeNo);

        }

        private void cmb_textsize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Set_TextSize();
        }
        double TXT_SIZE
        {
            get
            {
                return (cmb_textsize.SelectedIndex + 1) * 0.03;
            }
        }

        private void Set_TextSize()
        {

            foreach (var item in VDoc.ActionLayout.Entities)
            {
                if (item is vdMText)
                {
                    vdMText t = item as vdMText;
                    t.Height = TXT_SIZE;
                    t.Update();
                }
                if (item is vdText)
                {
                    vdText t = item as vdText;
                    t.Height = TXT_SIZE;
                    t.Update();
                }
            }
            VDoc.Redraw(true);
        }

        private void cmb_flr_lvl_SelectedIndexChanged(object sender, EventArgs e)
        {
            VDoc.ActiveLayOut.Entities.RemoveAll();
            VDoc.Redraw(true);

            panel1.Visible = false;
            txt_beam_nos.Text = "";
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {

            MyStrings ml = new MyStrings(txt_beam_nos.Text.Trim(), ',');

            if (ml.Count > 0)
                Slab_des.Beam_Nos = ml.StringList[0].Trim();
            else
            {
                MessageBox.Show("Slab Members is not defined.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //Slab_des.Beam_Nos = txt_beam_nos.Text;
            Slab_des.case_index = cmb_slab_type.SelectedIndex;

            Slab_des.D = MyStrings.StringToDouble(txt_slab_D.Text, 0.0);
            Slab_des.fck = MyStrings.StringToDouble(cmb_slab_fck.Text.Replace("M", ""), 0.0);
            Slab_des.gamma = MyStrings.StringToDouble(txt_slab_gamma.Text, 0.0);
            Slab_des.fy = MyStrings.StringToDouble(cmb_slab_fy.Text.Replace("Fe", ""), 0.0);
            Slab_des.wll = MyStrings.StringToDouble(txt_slab_wll.Text, 0.0);
            Slab_des.d1 = MyStrings.StringToDouble(txt_slab_d1.Text, 0.0);
            Slab_des.s1 = MyStrings.StringToDouble(txt_slab_s1.Text, 0.0);
            Slab_des.d2 = MyStrings.StringToDouble(txt_slab_d2.Text, 0.0);
            Slab_des.s2 = MyStrings.StringToDouble(txt_slab_s2.Text, 0.0);
            Slab_des.d3 = MyStrings.StringToDouble(txt_slab_d3.Text, 0.0);
            Slab_des.s3 = MyStrings.StringToDouble(txt_slab_s3.Text, 0.0);
            Slab_des.d4 = MyStrings.StringToDouble(txt_slab_d4.Text, 0.0);
            Slab_des.s4 = MyStrings.StringToDouble(txt_slab_s4.Text, 0.0);
            Slab_des.cover = MyStrings.StringToDouble(txt_slab_c.Text, 0.0);
            Slab_des.dlf = MyStrings.StringToDouble(txt_slab_DLF.Text, 0.0);
            Slab_des.llf = MyStrings.StringToDouble(txt_slab_LLF.Text, 0.0);

            Slab_des.dgv_dls = dgv_dls;
            #region Check Beam Members


            MyStrings mlist = new MyStrings(txt_beam_nos.Text.Trim(), ' ');

            MemberIncidenceCollection mic = new MemberIncidenceCollection();

            for (int i = 0; i < mlist.Count; i++)
            {
                mic.Add(AST_DOC.Members.Get_Member(mlist.GetInt(i)));
            }


            JointCoordinateCollection jntc = new JointCoordinateCollection();

            for (int i = 1; i < mic.Count; i++)
            {
                if (mic[i - 1].StartNode == mic[i].StartNode)
                {
                    if (!jntc.Contains(mic[i].StartNode))
                        jntc.Add(mic[i].StartNode);
                }
                else if (mic[i - 1].EndNode == mic[i].StartNode)
                {
                    if (!jntc.Contains(mic[i].StartNode))
                        jntc.Add(mic[i].StartNode);
                }
                else if (mic[i - 1].StartNode == mic[i].EndNode)
                {
                    if (!jntc.Contains(mic[i].EndNode))
                        jntc.Add(mic[i].EndNode);
                }
                else if (mic[i - 1].EndNode == mic[i].EndNode)
                {
                    if (!jntc.Contains(mic[i].EndNode))
                        jntc.Add(mic[i].EndNode);
                }
                else
                {
                    //string ss = Slab_Data[beam_nos] as string;
                    //if (ss != null)
                    //    Slab_Data.Remove(beam_nos);
                    //Save_Data();
                    MessageBox.Show("Beam " + mic[i].MemberNo + " and Beam " + mic[i - 1].MemberNo + " Joints are mismatch..", "ASTRA", MessageBoxButtons.OK);
                    return;
                }
            }

            #endregion  Check Beam Members
            //Chiranjit [2015 04 26]
            //Slab_des.Calculate_Program();
            Slab_des.Design_Program();


            List<string> Design_Summary = new List<string>();

            Design_Summary.Add(string.Format("-------------------------------------"));
            Design_Summary.Add(string.Format("SLAB MEMBERS = {0}", Slab_des.Beam_Nos));
            Design_Summary.Add(string.Format("SLAB FLOOR LEVEL = {0} M", cmb_flr_lvl.Text));
            Design_Summary.AddRange(Slab_des.Design_Summary.ToArray());
            Design_Summary.Add(string.Format("-------------------------------------"));


            string fnm = Slab_des.Get_Design_Summary_File(Slab_des.Beam_Nos, cmb_flr_lvl.Text);
            //fnm = Path.Combine(Path.GetDirectoryName(fnm), "SLAB_DESIGN_SUMMARY.TXT");

            //if (File.Exists(fnm))
            //    Design_Summary.AddRange(File.ReadAllLines(fnm));

            File.WriteAllLines(fnm, Design_Summary.ToArray());

            Save_Data();
            //Fill_Slab(txt_beam_nos.Text, Slab_des.IS_DESIGN_OK);
            Fill_All_Slab();

            Add_Slab_Boq(Slab_des.BOQ);



            GetGripSelection(false).RemoveAll();

            if (File.Exists(Slab_des.Report_File))
            {
                MessageBox.Show(this, "Report file created in file " + Slab_des.Report_File, "ASTRA", MessageBoxButtons.OK);
                frmASTRAReport.OpenReport(Slab_des.Report_File, this, false);
            }
            VDoc.Redraw(true);

            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {

            string fn = Slab_des.Report_File;

            List<string> list = new List<string>();
            foreach (var item in Directory.GetDirectories(Path.GetDirectoryName(fn)))
            {

                foreach (var im1 in Directory.GetDirectories(item))
                {

                    //list.Add(string.Format(""));
                    //list.Add(string.Format("{0}", Path.GetFileName(im1)));
                    //list.Add(string.Format(""));
                    //list.Add(string.Format(""));

                    foreach (var im2 in Directory.GetFiles(im1))
                    {
                        list.AddRange(File.ReadAllLines(im2));
                    }
                }
            }

            File.WriteAllLines(Slab_des.Report_File, list.ToArray());


            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            MyStrings mlist = new MyStrings(txt_beam_nos.Text, ',');


            List<string> list = new List<string>();
            string fn = "";

            //list.AddRange(SlabDesign.Get_Banner());
            for (int i = 0; i < mlist.Count; i++)
            {
                list.Add(string.Format(""));
                list.Add(string.Format("----------------------------------------------------------------------------"));
                list.Add(string.Format("SLAB_DESIGN {0} : SLAB MEMBERS [{1}] , FLOOR LEVEL : {2}", (i + 1), mlist.StringList[i], cmb_flr_lvl.Text));
                list.Add(string.Format("----------------------------------------------------------------------------"));
                list.Add(string.Format(""));
                list.Add(string.Format(""));
                fn = Slab_des.Get_Report_File(mlist.StringList[i].Trim(), cmb_flr_lvl.Text);
                if (File.Exists(fn))
                {
                    list.AddRange(File.ReadAllLines(fn));
                }
                else
                {
                    MessageBox.Show(this, "Design is not done for the selected Slab(s).", "ASTRA");
                    return;
                }


                
            }

            list.Add(string.Format(""));
            list.Add(string.Format(""));


            //fn = Slab_des.Get_Report_File(txt_beam_nos.Text, cmb_flr_lvl.Text);
            fn = Slab_des.Report_File;

            File.WriteAllLines(fn , list.ToArray());
            if (File.Exists(fn))
            {
                frmASTRAReport.OpenReport(fn, this, mlist.Count > 1);
            }
            else
            {
                MessageBox.Show(this, "Design is not done for the selected Slab(s).", "ASTRA");
            }

        }

        private void Fill_Slab()
        {
            Fill_Slab(txt_beam_nos.Text);
            //return;

            //MyStrings mlist = new MyStrings(txt_beam_nos.Text.Trim(), ' ');

            //MemberIncidenceCollection mic = new MemberIncidenceCollection();


            //for (int i = 0; i < mlist.Count; i++)
            //{

            //    mic.Add(AST_DOC.Members.Get_Member(mlist.GetInt(i)));


            //}



            //JointCoordinateCollection jntc = new JointCoordinateCollection();

            //vdPolyline vdpl = new vdPolyline();

            //vdpl.SetUnRegisterDocument(VDoc);
            //vdpl.setDocumentDefaults();

            //for (int i = 1; i < mic.Count; i++)
            //{
            //    if (mic[i - 1].StartNode == mic[i].StartNode)
            //    {
            //        if (!jntc.Contains(mic[i].StartNode))
            //            jntc.Add(mic[i].StartNode);
            //    }
            //    else if (mic[i - 1].EndNode == mic[i].StartNode)
            //    {
            //        if (!jntc.Contains(mic[i].StartNode))
            //            jntc.Add(mic[i].StartNode);
            //    }
            //    else if (mic[i - 1].StartNode == mic[i].EndNode)
            //    {
            //        if (!jntc.Contains(mic[i].EndNode))
            //            jntc.Add(mic[i].EndNode);
            //    }
            //    else if (mic[i - 1].EndNode == mic[i].EndNode)
            //    {
            //        if (!jntc.Contains(mic[i].StartNode))
            //            jntc.Add(mic[i].EndNode);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Beam " + mic[i].MemberNo + " and Beam " + mic[i - 1].MemberNo + " Joints are mismatch..", "ASTRA", MessageBoxButtons.OK);
            //        return;
                
            //    }
            //}

            //if (!jntc.Contains(mic[0].StartNode))
            //{
            //    jntc.Add(mic[0].StartNode);
            //}
            //if (!jntc.Contains(mic[0].EndNode))
            //{
            //    jntc.Add(mic[0].EndNode);
            //}

            //jntc.Add(jntc[0]);

            //foreach (var item in jntc)
            //{
            //    vdpl.VertexList.Add(item.Point);

            //}



            //vdLayer vlay = VDoc.Layers.FindName("slab");

            //if (vlay == null)
            //{
            //    vlay = new vdLayer(VDoc, "slab");
            //    vlay.SetUnRegisterDocument(VDoc);
            //    vlay.setDocumentDefaults();


            //    VDoc.Layers.Add(vlay);
            //}

            ////vlay.Frozen = false;
            //vlay.Lock = true;
            //vdpl.Layer = vlay;


            //vdpl.PenColor = new vdColor(Color.DarkGray);
            ////vdpl.PenColor = new vdColor(Color.Red);
            //vdpl.HatchProperties = new vdHatchProperties(VectorDraw.Professional.Constants.VdConstFill.VdFillModeHatchFDiagonal);


            //vdpl.ExtrusionVector = new Vector(0.0, 1.0, 0.0);


            //VDoc.ActiveLayOut.Entities.Add(vdpl);


            //VDoc.Redraw(true);
        }
        private void Fill_Slab(string beam_nos)
        {
            string sa = Slab_Data[beam_nos] as string;

            if (sa != null)
            {
                MyStrings mlist = new MyStrings(sa, ',');

                if (mlist.Count > 19)
                    Fill_Slab(beam_nos, !mlist.StringList[19].Contains("NOT"));

            }
            else
                Fill_Slab(beam_nos, true);
        }
        private void Fill_Slab(string beam_nos, bool isOk)
        {

            MyStrings mlist = new MyStrings(beam_nos.Trim(), ' ');

            MemberIncidenceCollection mic = new MemberIncidenceCollection();


            for (int i = 0; i < mlist.Count; i++)
            {

                mic.Add(AST_DOC.Members.Get_Member(mlist.GetInt(i)));
            }



            JointCoordinateCollection jntc = new JointCoordinateCollection();

            vdPolyline vdpl = new vdPolyline();

            vdpl.SetUnRegisterDocument(VDoc);
            vdpl.setDocumentDefaults();

            for (int i = 1; i < mic.Count; i++)
            {
                if (mic[i - 1].StartNode == mic[i].StartNode)
                {
                    if (!jntc.Contains(mic[i].StartNode))
                        jntc.Add(mic[i].StartNode);
                }
                else if (mic[i - 1].EndNode == mic[i].StartNode)
                {
                    if (!jntc.Contains(mic[i].StartNode))
                        jntc.Add(mic[i].StartNode);
                }
                else if (mic[i - 1].StartNode == mic[i].EndNode)
                {
                    if (!jntc.Contains(mic[i].EndNode))
                        jntc.Add(mic[i].EndNode);
                }
                else if (mic[i - 1].EndNode == mic[i].EndNode)
                {
                    if (!jntc.Contains(mic[i].EndNode))
                        jntc.Add(mic[i].EndNode);
                }
                else
                {
                    //string ss = Slab_Data[beam_nos] as string;
                    //if (ss != null)
                    //    Slab_Data.Remove(beam_nos);

                    //Save_Data();
                    MessageBox.Show("Beam " + mic[i].MemberNo + " and Beam " + mic[i - 1].MemberNo + " Joints are mismatch..", "ASTRA", MessageBoxButtons.OK);
                    return;
                }
            }

            if (!jntc.Contains(mic[0].StartNode))
            {
                jntc.Add(mic[0].StartNode);
            }
            if (!jntc.Contains(mic[0].EndNode))
            {
                jntc.Add(mic[0].EndNode);
            }

            jntc.Add(jntc[0]);
            double max_z = AST_DOC.Joints.Max_Z_Positive;

            foreach (var item in jntc)
            {
                //vdpl.VertexList.Add(item.Point);
                vdpl.VertexList.Add(new gPoint(item.X, max_z - item.Z));

            }


            //if (File.Exists(Slab_des.Get_Report_File(beam_nos, cmb_flr_lvl.Text)))
            //{
            //    vdpl.ToolTip = File.ReadAllText(Slab_des.Get_Report_File(beam_nos, cmb_flr_lvl.Text));
            //    vdpl.ToolTip = "



            //}

            vdpl.ToolTip = "Member surrounded : " + beam_nos;

            vdLayer vlay = VDoc.Layers.FindName("slab");

            
            if (vlay == null)
            {
                vlay = new vdLayer(VDoc, "slab");
                vlay.SetUnRegisterDocument(VDoc);
                vlay.setDocumentDefaults();


                VDoc.Layers.Add(vlay);
            }

            //vlay.Frozen = false;
            //vlay.Lock = true;
            vdpl.Layer = vlay;


            //vdpl.PenColor = new vdColor(Color.DarkGray);
            if (isOk)
                vdpl.PenColor = new vdColor(Color.Green);
            else
                vdpl.PenColor = new vdColor(Color.Red);
            vdpl.HatchProperties = new vdHatchProperties(VectorDraw.Professional.Constants.VdConstFill.VdFillModeHatchFDiagonal);


            //vdpl.ExtrusionVector = new Vector(0.0, 1.0, 0.0);


            VDoc.ActiveLayOut.Entities.Add(vdpl);

            VDoc.ActiveLayOut.Entities.ChangeOrder(vdpl, true);
            
            VDoc.Redraw(true);
        }

        private void btn_delete_slab_Click(object sender, EventArgs e)
        {

            MyStrings mfls = new MyStrings(txt_beam_nos.Text, ',');
            foreach (var item in mfls.StringList)
            {

                string fn = Slab_des.Get_Report_File(item.Trim(), cmb_flr_lvl.Text);
                if (File.Exists(fn))
                {
                    if (MessageBox.Show("Do you want to Delete this Slab Design [" + item.Trim() + "] ?", "ASTRA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        File.Delete(fn);
                    }

                }
            }
            Fill_All_Slab();

            //string fn = Slab_des.Get_Report_File(txt_beam_nos.Text, cmb_flr_lvl.Text);
            //if (File.Exists(fn))
            //{
            //    if (MessageBox.Show("Do you want to Delete this Slab Design ?", "ASTRA", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //    {
            //        File.Delete(fn);
            //        Fill_All_Slab();
            //    }

            //}
        }

        #region Save Data Function
        Hashtable Slab_Data = new Hashtable();
        public void Save_Data()
        {
            string fname = Slab_des.Get_Report_File(txt_beam_nos.Text, cmb_flr_lvl.Text);
            fname = Path.Combine(Path.GetDirectoryName(fname), "SLAB_DATA.TXT");

            string kStr = "";
            string def_loads = "";
            int i = 0;
            for (i = 0; i < dgv_dls.RowCount - 1; i++)
            {
                def_loads += string.Format("{0}${1}$", dgv_dls[0, i].Value.ToString(), dgv_dls[1, i].Value.ToString());
            }



            kStr = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19}",
                txt_beam_nos.Text
                , txt_slab_D.Text
                , cmb_slab_fck.Text
                , txt_slab_gamma.Text
                , cmb_slab_fy.Text
                , txt_slab_wll.Text
                , txt_slab_DLF.Text
                , txt_slab_LLF.Text
                , txt_slab_c.Text
                , txt_slab_d1.Text
                , txt_slab_s1.Text
                , txt_slab_d2.Text
                , txt_slab_s2.Text
                , txt_slab_d3.Text
                , txt_slab_s3.Text
                , txt_slab_d4.Text
                , txt_slab_s4.Text
                , cmb_slab_type.Text
                , def_loads, Slab_des.IS_DESIGN_OK ? "OK" : "NOT OK"
                );

            List<string> list = new List<string>();

            string dta = (string)Slab_Data[txt_beam_nos.Text.Trim()];


            //list.AddRange(File.ReadAllLines(fname));
            if (dta == null)
            {
                Slab_Data.Add(txt_beam_nos.Text.Trim(), kStr.Trim());
            }
            else
            {
                Slab_Data.Remove(txt_beam_nos.Text);
                Slab_Data.Add(txt_beam_nos.Text, kStr);
            }


            list.Clear();

            foreach (DictionaryEntry de in Slab_Data)
            {
                list.Add(de.Value.ToString());
            }
            File.WriteAllLines(fname, list.ToArray());


        }

        void Load_Slab_Data()
        {
            Slab_Data = new Hashtable();
            string fname = Slab_des.Get_Report_File(txt_beam_nos.Text, cmb_flr_lvl.Text);
            fname = Path.Combine(Path.GetDirectoryName(fname), "SLAB_DATA.TXT");


            if (File.Exists(fname))
            {
                List<string> list = new List<string>(File.ReadAllLines(fname));

                for (int i = 0; i < list.Count; i++)
                {
                    MyStrings mli = new MyStrings(list[i], ',');

                   object obj = Slab_Data[mli.StringList[0].Trim()];
                   if (obj != null)
                       Slab_Data.Remove(mli.StringList[0].Trim());

                    Slab_Data.Add(mli.StringList[0].Trim(), list[i]);
                    
                }
            }

        }
        public void Show_Data()
        {
            string kStr = "";
            string def_loads = "";
            int i = 0;

            //kStr = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17}",
            //    txt_beam_nos.Text
            //    , txt_slab_D.Text
            //    , cmb_slab_fck.Text
            //    , txt_slab_gamma.Text
            //    , cmb_slab_fy.Text
            //    , txt_slab_wll.Text
            //    , txt_slab_DLF.Text
            //    , txt_slab_LLF.Text
            //    , txt_slab_c.Text
            //    , txt_slab_d1.Text
            //    , txt_slab_s1.Text
            //    , txt_slab_d2.Text
            //    , txt_slab_s2.Text
            //    , txt_slab_d3.Text
            //    , txt_slab_s3.Text
            //    , txt_slab_d4.Text
            //    , txt_slab_s4.Text
            //    , def_loads
            //    );

            if (txt_beam_nos.Text == "") return;

            List<string> list = new List<string>();

            string dta = (string)Slab_Data[txt_beam_nos.Text.Trim()];


            //list.AddRange(File.ReadAllLines(fname));
            if (dta != null)
            {
                MyStrings mlist = new MyStrings(dta, ',');
                i = 1;
                //txt_beam_nos.Text  = mlist.StringList[i++];
                txt_slab_D.Text  = mlist.StringList[i++];
                cmb_slab_fck.Text  = mlist.StringList[i++];
                txt_slab_gamma.Text   = mlist.StringList[i++];
                cmb_slab_fy.Text   = mlist.StringList[i++];
                txt_slab_wll.Text   = mlist.StringList[i++];
                txt_slab_DLF.Text   = mlist.StringList[i++];
                txt_slab_LLF.Text   = mlist.StringList[i++];
                txt_slab_c.Text   = mlist.StringList[i++];
                txt_slab_d1.Text   = mlist.StringList[i++];
                txt_slab_s1.Text  = mlist.StringList[i++];
                txt_slab_d2.Text    = mlist.StringList[i++];
                txt_slab_s2.Text   = mlist.StringList[i++];
                txt_slab_d3.Text   = mlist.StringList[i++];
                txt_slab_s3.Text    = mlist.StringList[i++];
                txt_slab_d4.Text   = mlist.StringList[i++];
                txt_slab_s4.Text = mlist.StringList[i++];
                cmb_slab_type.SelectedItem = mlist.StringList[i++];
                def_loads = mlist.StringList[i++];
                dgv_dls.Rows.Clear();
                if (def_loads != "")
                {

                    mlist = new MyStrings(def_loads, '$');
                    for (i = 1; i < mlist.Count-1; i += 2)
                    {
                        dgv_dls.Rows.Add(mlist.StringList[i - 1], mlist.StringList[i]);
                    }
                }

            }


            //list.Clear();

            //foreach (DictionaryEntry de in Slab_Data)
            //{
            //    list.Add(de.Value.ToString());
            //}
            //File.WriteAllLines(fname, list.ToArray());


        }
        #endregion Save Data Function



        public void Draw_Floor_Layout()
        {
            double flr_lvl = MyStrings.StringToDouble(cmb_flr_lvl.Text, 0.0);
            Draw_Floor_Layout(flr_lvl);

        }
        public void Draw_Floor_Layout(double flr_lvl)
        {
            MemberIncidenceCollection beams = new MemberIncidenceCollection();

            for (int i = 0; i < AST_DOC.Members.Count; i++)
            {
                var item = AST_DOC.Members[i];

                if (item.EndNode.Y == flr_lvl)
                {
                    if (item.StartNode.Y == item.EndNode.Y)
                    {
                        beams.Add(item);
                    }
                }
            }




            VDoc.ShowUCSAxis = false;

            VDoc.ActiveLayOut.Entities.RemoveAll();

            double max_z = AST_DOC.Joints.Max_Z_Positive;
            vdLine ln = new vdLine();

            foreach (var item in beams)
            {
                ln = new vdLine();

                ln.SetUnRegisterDocument(VDoc);
                ln.setDocumentDefaults();

                ln.StartPoint.x = item.StartNode.X;
                ln.StartPoint.y = max_z - item.StartNode.Z;

                ln.EndPoint.x = item.EndNode.X;
                ln.EndPoint.y = max_z - item.EndNode.Z;

                ln.ToolTip = "Member No : " + item.MemberNo;
                VDoc.ActiveLayOut.Entities.Add(ln);

            }

            //Get_Slabs(mic);
            Fill_All_Slab();
            Load_Slab_Data();

            VDoc.Palette.Background = Color.White;
            VDoc.Redraw(true);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(VDoc);
        }


    }


}
