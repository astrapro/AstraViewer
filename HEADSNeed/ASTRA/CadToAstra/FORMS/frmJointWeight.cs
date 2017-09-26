using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HEADSNeed.ASTRA.ASTRAClasses;

namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    public partial class frmJointWeight : Form
    {
        public frmJointWeight()
        {
            InitializeComponent();
        }

        
        List<int> list_joint_No = null;
        IASTRACAD iACad = null;
        public List<string> ASTRA_Data { get; set; }
        public TreeNode Node { get; set; }



        public frmJointWeight(IASTRACAD iACAD)
        {
            InitializeComponent();
            list_joint_No = new List<int>();
            this.iACad = iACAD;
            ASTRA_Data = new List<string>();
        }

        public void Add_Structure_Joint_Load()
        {
            MemberIncidenceCollection all_beams = new MemberIncidenceCollection();
            List<double> floors = new List<double>();
            if (iACad.AstraDocument.Members.Count > 0)
            {
                foreach (var item in iACad.AstraDocument.Members)
                {
                    if (item.StartNode.Y == item.EndNode.Y)
                    {
                        all_beams.Add(item);
                        if (!floors.Contains(item.EndNode.Y))
                            floors.Add(item.EndNode.Y);
                    }
                }

            }
            List<JointWeight> JointWeights = new List<JointWeight>();

            List<int> joints = new List<int>();

            floors.Sort();
            double wght = 0.0;
            int indx = 0;

            double factor = MyStrings.StringToDouble(txt_factor.Text, 0.8);
            double unit_wt_wall = MyStrings.StringToDouble(txt_wall_uwgt.Text, 0.8);
            foreach (var item in all_beams)
            {
                JointWeight jw = new JointWeight();
                jw.JointNo = item.StartNode.NodeNo;
                indx = floors.IndexOf(item.StartNode.Y);

                wght = 0.0;
                //double unit_wt_wall = 19.2;
                if (indx != -1)
                {
                    if (indx + 1 < floors.Count)
                    {
                        if (item.Property == null)
                        {
                            MessageBox.Show("Member Property is not defined for Member No " 
                                + item.MemberNo, "ASTRA", MessageBoxButtons.OK);
                            return;

                        }
                        else
                        {
                            wght = item.Property.YD * item.Length * (floors[indx + 1] - floors[indx]) * unit_wt_wall;
                        }
                    }
                    //wght = item.Property.ZD * item.Length * (floors[indx + 1] - floors[indx]) * 24.0;
                }



                if (wght != 0.0)
                {
                    wght = wght * factor;
                    jw = new JointWeight();
                    jw.JointNo = item.StartNode.NodeNo;
                    jw.Weight = wght / 2;
                    JointWeights.Add(jw);



                    if (!joints.Contains(jw.JointNo))
                        joints.Add(jw.JointNo);

                    jw = new JointWeight();
                    jw.JointNo = item.EndNode.NodeNo;
                    jw.Weight = wght / 2;
                    JointWeights.Add(jw);


                    if (!joints.Contains(jw.JointNo))
                        joints.Add(jw.JointNo);
                }

            }

            //List<JointWeight> FinalWeights = new List<JointWeight>();
            JointWeight jwt = null;


            jwt = null;

            dgv_str_jnt_wgt.Rows.Clear();
            for (int j = 0; j < joints.Count; j++)
            {
                jwt = new JointWeight(joints[j], 0.0);
                //FinalWeights.Add(jwt);
                for (int i = 0; i < JointWeights.Count; i++)
                {
                    if (jwt.JointNo == JointWeights[i].JointNo)
                    {
                        jwt.Weight += JointWeights[i].Weight;
                    }
                }
                dgv_str_jnt_wgt.Rows.Add(jwt.JointNo, jwt.Weight.ToString("f3"));
                //Node.Nodes.Add(jwt.ToString());
                ASTRA_Data.Add(jwt.ToString());

            }

            txt_joint_number.Text = "";
            //FinalWeights.Add(jwt);

            //Node.Nodes.Add(jwt.ToString());
        }

        private void frmJointWeight_Load(object sender, EventArgs e)
        {

            if (ASTRA_Data.Count > 0)
            {
                string kStr = ASTRA_Data[0];

                MyStrings mlist = new MyStrings(MyStrings.RemoveAllSpaces(kStr), ' ');

                int r = -1;
                for (int i = 0; i < mlist.Count; i++)
                {
                    if (r == -1)
                    {
                        if (mlist.StringList[i].StartsWith("WEI"))
                        {
                            r = i;
                        }
                    }
                    if (mlist.StringList[i].StartsWith("WEI"))
                    {
                        txt_fy.Text = mlist.StringList[i + 1];
                    }
                }

                if (r != -1)
                {
                    txt_joint_number.Text = mlist.GetString(0, r - 1);
                }
                btn_jload_add.Text = "Change";
                ASTRA_Data.Clear();

                this.Height = 140;
                groupBox1.Visible = false;
            }
            else
            {
                txt_joint_number.Text = iACad.GetSelectedJointsInText();

                this.Height = 447;

            }
        }

        private void btn_jload_add_Click(object sender, EventArgs e)
        {
            string kStr = "";
            kStr = txt_joint_number.Text.Replace(',', ' ');

            double dval = 0.0;
           
            dval = MyStrings.StringToDouble(txt_fy.Text, 0.0);
            if (dval != 0.0)
            {
                kStr = kStr.Trim() + " WEIGHT " + dval.ToString("f3").Trim();
            }

            if (kStr != "")
                ASTRA_Data.Add(kStr);

            if (Node != null)
            {
                if (btn_jload_add.Text == "Change")
                {
                    Node.Text = kStr;
                    this.Close();
                }
                else
                {
                    if (kStr == "")
                    {
                        Node.Nodes.Clear();
                        ASTRA_Data.Clear();
                        if (dgv_str_jnt_wgt.Rows.Count > 1)
                        {
                            for (int i = 0; i < dgv_str_jnt_wgt.Rows.Count - 1; i++)
                            {
                                kStr = dgv_str_jnt_wgt[0, i].Value.ToString() +
                                    " WEIGHT " + dgv_str_jnt_wgt[1, i].Value.ToString();

                                ASTRA_Data.Add(kStr);

                            }
                            foreach (var item in ASTRA_Data)
                            {
                                Node.Nodes.Add(item);
                            }
                        }
                    }
                    else
                    {
                        Node.Nodes.Add(kStr);
                    }

                    //Node.Nodes.Add(kStr);
                    Node.ExpandAll();
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

        private void btn_str_wgt_Click(object sender, EventArgs e)
        {
            Add_Structure_Joint_Load();
        }
    }
}
