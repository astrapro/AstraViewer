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
    public partial class frmJoints : Form
    {

        List<int> list_joint_No = null;
        IASTRACAD iACad = null;
        public List<string> ASTRA_Data { get; set; }
        public DataGridView DGV { get; set; }


        public frmJoints()
        {
            InitializeComponent();
        }

        public int StartJointNo
        {
            get
            {
                return MyStrings.StringToInt(txt_jnt_start_no.Text, 1);
            }
            set
            {
                txt_jnt_start_no.Text = value.ToString("f0");
            }
        }
        public double StartJoint_X
        {
            get
            {
                return MyStrings.StringToDouble(txt_jnt_start_x.Text, 0.0);
            }
            set
            {
                txt_jnt_start_x.Text = value.ToString("f4");
            }
        }
        public double StartJoint_Y
        {
            get
            {
                return MyStrings.StringToDouble(txt_jnt_start_y.Text, 0.0);
            }
            set
            {
                txt_jnt_start_y.Text = value.ToString("f4");
            }
        }
        public double StartJoint_Z
        {
            get
            {
                return MyStrings.StringToDouble(txt_jnt_start_z.Text, 0.0);
            }
            set
            {
                txt_jnt_start_z.Text = value.ToString("f4");
            }
        }

        public int EndJointNo
        {
            get
            {
                return MyStrings.StringToInt(txt_jnt_end_no.Text, 1);
            }
            set
            {
                txt_jnt_end_no.Text = value.ToString("f0");
            }
        }
        public double EndJoint_X
        {
            get
            {
                return MyStrings.StringToDouble(txt_jnt_end_x.Text, 0.0);
            }
            set
            {
                txt_jnt_end_x.Text = value.ToString("f4");
            }
        }
        public double EndJoint_Y
        {
            get
            {
                return MyStrings.StringToDouble(txt_jnt_end_y.Text,0.0);
            }
            set
            {
                txt_jnt_end_y.Text = value.ToString("f4");
            }
        }
        public double EndJoint_Z
        {
            get
            {
                return MyStrings.StringToDouble(txt_jnt_end_z.Text, 0.0);
            }
            set
            {
                txt_jnt_end_z.Text = value.ToString("f4");
            }
        }



        public frmJoints(IASTRACAD iACAD)
        {
            InitializeComponent();
            list_joint_No = new List<int>();
            this.iACad = iACAD;
            ASTRA_Data = new List<string>();
        }

        private void frmJoints_Load(object sender, EventArgs e)
        {
            if (ASTRA_Data.Count > 0)
            {
                string kStr = ASTRA_Data[0];


                MyStrings mlist = new MyStrings(MyStrings.RemoveAllSpaces(kStr), ' ');


                int r = MyStrings.StringToInt(kStr, -1);
                if (r == -1)
                {
                    r = DGV.CurrentCell.RowIndex;
                    btn_jnt_add.Text = "Insert";
                    txt_jnt_start_no.Text = DGV[0, r].Value.ToString();
                }
                else
                {
                    txt_jnt_start_no.Text = DGV[0, r].Value.ToString();
                    txt_jnt_start_x.Text = DGV[1, r].Value.ToString();
                    txt_jnt_start_y.Text = DGV[2, r].Value.ToString();
                    txt_jnt_start_z.Text = DGV[3, r].Value.ToString();
                    btn_jnt_add.Text = "Change";
                }
            }
            else
            {
                txt_jnt_start_no.Text = DGV.RowCount + 1 + "";

                txt_jnt_start_x.Text = "0.0";
                txt_jnt_start_y.Text = "0.0";
                txt_jnt_start_z.Text = "0.0";
            }
            
        }
        private void btn_jnt_add_Click(object sender, EventArgs e)
        {
            string kStr = "";
           
          

            if (DGV != null)
            {
                if (btn_jnt_add.Text == "Change")
                {

                    if (ASTRA_Data.Count > 0)
                        kStr = ASTRA_Data[0];

                    int r = MyStrings.StringToInt(kStr, 0);

                    DGV[0, r].Value = StartJointNo;
                    DGV[1, r].Value = StartJoint_X.ToString("f4");
                    DGV[2, r].Value = StartJoint_Y.ToString("f4");
                    DGV[3, r].Value = StartJoint_Z.ToString("f4");

                    Draw_Joints();
                    this.Close();
                }
                else if (btn_jnt_add.Text == "Insert")
                {



                    int r = DGV.CurrentCell.RowIndex;


                    if (DGV.SelectedCells.Count > 0)
                        r = DGV.SelectedCells[0].RowIndex;

                    DGV.Rows.Insert(r, StartJointNo,
                        StartJoint_X.ToString("f4"),
                        StartJoint_Y.ToString("f4"),
                        StartJoint_Z.ToString("f4"));

                    StartJointNo = StartJointNo + EndJointNo;
                    StartJoint_X = StartJoint_X + EndJoint_X;
                    StartJoint_Y = StartJoint_Y + EndJoint_Y;
                    StartJoint_Z = StartJoint_Z + EndJoint_Z;
                    Draw_Joints();

                }
                else
                {
                    DGV.Rows.Add(StartJointNo,
                        StartJoint_X.ToString("f4"),
                        StartJoint_Y.ToString("f4"),
                        StartJoint_Z.ToString("f4"));

                    StartJointNo = StartJointNo + EndJointNo;
                    StartJoint_X = StartJoint_X + EndJoint_X;
                    StartJoint_Y = StartJoint_Y + EndJoint_Y;
                    StartJoint_Z = StartJoint_Z + EndJoint_Z;
                    Draw_Joints();

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
        public void Draw_Joints()
        {


            //Clear_All();
            ASTRADoc astdoc;
            //astdoc = new ASTRADoc(iACad.AstraDocument);
            astdoc = (iACad.AstraDocument);


            astdoc.Joints.Clear();

            for (int i = 0; i < DGV.Rows.Count; i++)
            {
                JointCoordinate jn = new JointCoordinate();
                DGV[0, i].Value = i + 1;

                //jn.NodeNo = MyList.StringToInt(DGV[0, i].Value.ToString(), 0);
                jn.NodeNo = MyStrings.StringToInt(DGV[0, i].Value.ToString(), 0);
                jn.Point.x = MyStrings.StringToDouble(DGV[1, i].Value.ToString(), 0.0);
                jn.Point.y = MyStrings.StringToDouble(DGV[2, i].Value.ToString(), 0.0);
                jn.Point.z = MyStrings.StringToDouble(DGV[3, i].Value.ToString(), 0.0);
                astdoc.Joints.Add(jn);
            }
            //iACad.Document.ActiveLayOut.Entities.EraseAll();

            foreach (var item in iACad.Document.ActionLayout.Entities)
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
            iACad.Document.Redraw(true);

            astdoc.Joints.DrawJointsText(iACad.Document, 0.3);

            //iACad.AstraDocument = astdoc;

            //AST_DOC.Members.DrawMember(VDoc);
            //AST_DOC.Elements.DrawElements(VDoc);
            //AST_DOC.Supports.DrawSupport(VDoc);

        }

    }
}
