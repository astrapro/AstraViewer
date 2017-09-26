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
    public partial class frmElements : Form
    {   
        //List<int> list_joint_No = null;
        IASTRACAD iACad = null;
        public List<string> ASTRA_Data { get; set; }
        public DataGridView DGV { get; set; }

        public frmElements()
        {
            InitializeComponent();
        }
        
        

        public int ElementNo
        {
            get
            {
                return MyStrings.StringToInt(txt_elmt_no.Text, 1);
            }
            set
            {
                txt_elmt_no.Text = value.ToString("f0");
            }
        }

        public int Node1
        {
            get
            {
                return MyStrings.StringToInt(txt_elmt_node1.Text, 0);
            }
            set
            {
                txt_elmt_node1.Text = value.ToString("f0");
            }
        }
        public int Node2
        {
            get
            {
                return MyStrings.StringToInt(txt_elmt_node2.Text, 0);
            }
            set
            {
                txt_elmt_node2.Text = value.ToString("f0");
            }
        }


        public int Node3
        {
            get
            {
                return MyStrings.StringToInt(txt_elmt_node3.Text, 0);
            }
            set
            {
                txt_elmt_node3.Text = value.ToString("f0");
            }
        }


        public int Node4
        {
            get
            {
                return MyStrings.StringToInt(txt_elmt_node4.Text, 0);
            }
            set
            {
                txt_elmt_node4.Text = value.ToString("f0");
            }
        }
        

        public frmElements(IASTRACAD iACAD)
        {
            InitializeComponent();
            this.iACad = iACAD;
            ASTRA_Data = new List<string>();
        }

        private void frmElements_Load(object sender, EventArgs e)
        {
            if (ASTRA_Data.Count > 0)
            {
                string kStr = ASTRA_Data[0];


                MyStrings mlist = new MyStrings(MyStrings.RemoveAllSpaces(kStr), ' ');


                int r = MyStrings.StringToInt(kStr, -1);
                if (r == -1)
                {
                    r = DGV.CurrentCell.RowIndex;
                    btn_elmt_add.Text = "Insert";
                    txt_elmt_no.Text = DGV[0, r].Value.ToString();
                }
                else
                {
                    txt_elmt_no.Text = DGV[0, r].Value.ToString();
                    txt_elmt_node1.Text = DGV[1, r].Value.ToString();
                    txt_elmt_node2.Text = DGV[2, r].Value.ToString();
                    txt_elmt_node3.Text = DGV[3, r].Value.ToString();
                    txt_elmt_node4.Text = DGV[4, r].Value.ToString();
                    btn_elmt_add.Text = "Change";
                }
            }
            else
            {
                txt_elmt_no.Text = DGV.RowCount + 1 + "";

                List<int> joints = MyStrings.Get_Array_Intiger(iACad.GetSelectedJointsInText());

                if (joints.Count > 3)
                {
                    txt_elmt_node1.Text = joints[0].ToString();
                    txt_elmt_node2.Text = joints[1].ToString();
                    txt_elmt_node3.Text = joints[2].ToString();
                    txt_elmt_node4.Text = joints[3].ToString();

                }

                //txt_elmt_node1.Text = "0";
                //txt_elmt_node2.Text = "0.0";
                //txt_jnt_start_z.Text = "0.0";
            }
            
        }
        private void btn_elmt_add_Click(object sender, EventArgs e)
        {
            string kStr = "";
           
          

            if (DGV != null)
            {
                if (btn_elmt_add.Text == "Change")
                {

                    if (ASTRA_Data.Count > 0)
                        kStr = ASTRA_Data[0];

                    int r = MyStrings.StringToInt(kStr, 0);

                    DGV[0, r].Value = ElementNo;
                    DGV[1, r].Value = Node1.ToString();
                    DGV[2, r].Value = Node2.ToString();
                    DGV[3, r].Value = Node3.ToString();
                    DGV[4, r].Value = Node4.ToString();

                    Draw_Elements();
                    this.Close();
                }
                else if (btn_elmt_add.Text == "Insert")
                {



                    int r = DGV.CurrentCell.RowIndex;

                    if (DGV.SelectedCells.Count > 0)
                        r = DGV.SelectedCells[0].RowIndex;

                    DGV.Rows.Insert(r, ElementNo,
                        Node1,
                        Node2,
                        Node3,
                        Node4);

                    ElementNo = ElementNo + 1;
                 
                    Draw_Elements();

                }
                else
                {
                    DGV.Rows.Add(ElementNo,
                        Node1,
                        Node2,
                        Node3,
                        Node4);

                    ElementNo = ElementNo + 1;
                   
                    Draw_Elements();

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
        public void Draw_Elements()
        {


            //Clear_All();
            ASTRADoc astdoc;
            //astdoc = new ASTRADoc(iACad.AstraDocument);
            astdoc = (iACad.AstraDocument);


            astdoc.Elements.Clear();

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

            foreach (var item in iACad.Document.ActionLayout.Entities)
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
            iACad.Document.Redraw(true);

            astdoc.Elements.CopyCoordinates(astdoc.Joints);
            astdoc.Elements.DrawElements(iACad.Document);

            //iACad.AstraDocument = astdoc;

            //AST_DOC.Members.DrawMember(VDoc);
            //AST_DOC.Elements.DrawElements(VDoc);
            //AST_DOC.Supports.DrawSupport(VDoc);

        }

    }
}
