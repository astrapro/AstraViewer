using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using HEADSNeed.ASTRA.ASTRAClasses;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Geometry;

namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    public partial class frmMembers : Form
    {    
        List<int> list_joint_No = null;
        IASTRACAD iACad = null;
        public List<string> ASTRA_Data { get; set; }
        public DataGridView DGV { get; set; }


        public frmMembers()
        {
            InitializeComponent();
        }

        public frmMembers(IASTRACAD iACAD)
        {
            InitializeComponent();
            list_joint_No = new List<int>();
            this.iACad = iACAD;
            ASTRA_Data = new List<string>();
        }


        public int StartMemberNo
        {
            get
            {
                return MyStrings.StringToInt(txt_mbr_start_no.Text, 1);
            }
            set
            {
                txt_mbr_start_no.Text = value.ToString("f0");
            }
        }
        public int StartIncrNo
        {
            get
            {
                return MyStrings.StringToInt(txt_incr_start_no.Text, 1);
            }
            set
            {
                txt_incr_start_no.Text = value.ToString("f0");
            }
        }


        public int StartMemberJointNo
        {
            get
            {
                return MyStrings.StringToInt(txt_mbr_start_jnt.Text, 1);
            }
            set
            {
                txt_mbr_start_jnt.Text = value.ToString("f0");
            }
        }
        public int StartIncrJointNo
        {
            get
            {
                return MyStrings.StringToInt(txt_incr_start_jnt.Text, 1);
            }
            set
            {
                txt_incr_start_jnt.Text = value.ToString("f0");
            }
        }


        public int EndMemberJointNo
        {
            get
            {
                return MyStrings.StringToInt(txt_mbr_end_jnt.Text, 1);
            }
            set
            {
                txt_mbr_end_jnt.Text = value.ToString("f0");
            }
        }
        public int EndIncrJointNo
        {
            get
            {
                return MyStrings.StringToInt(txt_incr_end_jnt.Text, 1);
            }
            set
            {
                txt_incr_end_jnt.Text = value.ToString("f0");
            }
        }

        private void frmMembers_Load(object sender, EventArgs e)
        {
            cmb_memtype.SelectedIndex = 0;

            if (ASTRA_Data.Count > 0)
            {
                string kStr = ASTRA_Data[0];

                if (kStr == "INSERT")
                {

                    int r = DGV.CurrentCell.RowIndex;

                    txt_mbr_start_no.Text = DGV[0, r].Value.ToString();

                    if (DGV[1, r].Value.ToString().StartsWith("C"))
                        cmb_memtype.SelectedIndex = 2;
                    else if (DGV[1, r].Value.ToString().StartsWith("T"))
                        cmb_memtype.SelectedIndex = 1;
                    else
                        cmb_memtype.SelectedIndex = 0;

                    txt_mbr_start_jnt.Text = DGV[2, r].Value.ToString();
                    txt_mbr_end_jnt.Text = DGV[3, r].Value.ToString();

                    btn_add_mem.Text = "Insert";
                }
                else
                {

                    MyStrings mlist = new MyStrings(MyStrings.RemoveAllSpaces(kStr), ' ');

                    int r = MyStrings.StringToInt(kStr, 0);

                    txt_mbr_start_no.Text = DGV[0, r].Value.ToString();
                    if (DGV[1, r].Value.ToString().StartsWith("C"))
                        cmb_memtype.SelectedIndex = 2;
                    else if (DGV[1, r].Value.ToString().StartsWith("T"))
                        cmb_memtype.SelectedIndex = 1;
                    else
                        cmb_memtype.SelectedIndex = 0;

                    txt_mbr_start_jnt.Text = DGV[2, r].Value.ToString();
                    txt_mbr_end_jnt.Text = DGV[3, r].Value.ToString();

                    btn_add_mem.Text = "Change";
                }
            }
            else
            {
                txt_mbr_start_no.Text = (DGV.RowCount + 1) + "";
                if (DGV.RowCount == 0)
                {
                    txt_mbr_start_jnt.Text = "1";
                    txt_mbr_end_jnt.Text = "2";
                    txt_incr_start_no.Text = "1";
                    txt_incr_start_jnt.Text = "1";
                    txt_incr_end_jnt.Text = "1";
                }
            }
        }
        private void btn_mbr_add_Click(object sender, EventArgs e)
        {
            string kStr = "";

            if (DGV != null)
            {
                if (btn_add_mem.Text == "Change")
                {


                    if (ASTRA_Data.Count > 0)
                    {
                        kStr = ASTRA_Data[0];
                        int r = MyStrings.StringToInt(kStr, 0);

                        DGV[0, r].Value = StartMemberNo;
                        DGV[1, r].Value = cmb_memtype.Text;
                        DGV[2, r].Value = StartMemberJointNo.ToString("f0");
                        DGV[3, r].Value = EndMemberJointNo.ToString("f0");
                        Draw_Members();
                    }

                    this.Close();
                }
                else if (btn_add_mem.Text == "Insert")
                {


                    if (ASTRA_Data.Count > 0)
                    {
                        kStr = ASTRA_Data[0];
                        int r = DGV.CurrentCell.RowIndex;


                        if (DGV.SelectedCells.Count > 0)
                            r = DGV.SelectedCells[0].RowIndex;


                        DGV.Rows.Insert(r, StartMemberNo,
                            cmb_memtype.Text,
                            StartMemberJointNo.ToString("f0"),
                            EndMemberJointNo.ToString("f0"));

                        StartMemberNo = StartMemberNo + StartIncrNo;
                        StartMemberJointNo = StartMemberJointNo + StartIncrJointNo;
                        EndMemberJointNo = EndMemberJointNo + EndIncrJointNo;

                        Draw_Members();
                    }

                }
                else
                {

                    DGV.Rows.Add(StartMemberNo,
                        cmb_memtype.Text,
                        StartMemberJointNo.ToString("f0"),
                        EndMemberJointNo.ToString("f0"));

                    StartMemberNo = StartMemberNo + StartIncrNo;
                    StartMemberJointNo = StartMemberJointNo + StartIncrJointNo;
                    EndMemberJointNo = EndMemberJointNo + EndIncrJointNo;
                    Draw_Members();
                }
            }
            else
            {
                this.Close();
            }
        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void Draw_Members()
        {


            //Clear_All();
            ASTRADoc astdoc;
            //astdoc = new ASTRADoc(ACad.AstraDocument);
            astdoc = (iACad.AstraDocument);


            astdoc.Members.Clear();

            for (int i = 0; i < DGV.Rows.Count; i++)
            {
                MemberIncidence mbr = new MemberIncidence();

                DGV[0, i].Value = i + 1;

                mbr.MemberNo = MyStrings.StringToInt(DGV[0, i].Value.ToString(), 0);

                if (DGV[1, i].Value.ToString().StartsWith("C"))
                    mbr.MemberType = MembType.CABLE;
                else if (DGV[1, i].Value.ToString().StartsWith("T"))
                    mbr.MemberType = MembType.TRUSS;
                else
                    mbr.MemberType = MembType.BEAM;

                mbr.StartNode.NodeNo = MyStrings.StringToInt(DGV[2, i].Value.ToString(), 0);



                mbr.EndNode.NodeNo = MyStrings.StringToInt(DGV[3, i].Value.ToString(), 0);


                astdoc.Members.Add(mbr);
            }


            foreach (var item in iACad.Document.ActionLayout.Entities)
            {
                if (item is vdFigure)
                {
                    vdFigure vf = item as vdFigure;
                    if (vf.Layer.Name == "Members")
                    {
                        vf.Deleted = true;
                    }
                }

            }


            iACad.Document.Redraw(true);

            astdoc.Members.CopyJointCoordinates(astdoc.Joints);

            astdoc.MemberProperties.CopyMemberIncidence(astdoc.Members);

            astdoc.Members.DrawMember(iACad.Document, 0.3);
        }

       
    }
}
