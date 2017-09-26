using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using HEADSNeed.ASTRA.ASTRAClasses;


namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    public partial class frmWindLoad : Form
    {
        List<int> list_joint_No = null;
        IASTRACAD iACad = null;
        public List<string> ASTRA_Data { get; set; }
        public TreeNode Node { get; set; }

        public frmWindLoad(IASTRACAD iACAD)
        {
            InitializeComponent();
            list_joint_No = new List<int>();
            this.iACad = iACAD;
            ASTRA_Data = new List<string>();
        }

        private void frmWindLoad_Load(object sender, EventArgs e)
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
                        if (mlist.StringList[i] == "FX" || mlist.StringList[i] == "FY" || mlist.StringList[i] == "FZ" ||
                            mlist.StringList[i] == "MX" || mlist.StringList[i] == "MY" || mlist.StringList[i] == "MZ")
                        {
                            r = i;
                        }
                    }

                    if (mlist.StringList[i] == "FX")
                    {
                        rbtn_fx_positive.Checked = true;
                    }
                    else if (mlist.StringList[i] == "FZ")
                    {
                        rbtn_fz_positive.Checked = true;
                    }
                }

                //if (r != -1)
                //{
                //    txt_sc.Text = mlist.GetString(1, r - 1).Trim().TrimEnd().TrimStart();
                //}
                btn_add.Text = "Change";
                ASTRA_Data.Clear();
            }
            //else
            //    txt_joint_number.Text = iACad.GetSelectedJointsInText();
        }

        private void btn_jload_add_Click(object sender, EventArgs e)
        {


            double Area_Factor = MyStrings.StringToDouble(txt_area_factor.Text, 0.6);
            double Velocity = MyStrings.StringToDouble(txt_velocity.Text, 0.6);
            double Pressure = MyStrings.StringToDouble(txt_pressure.Text, 0.6);

            double Width = 0.0;
            double Height = 0.0;







            string kStr = "SC ";


            Seismic_Calculations ss = new Seismic_Calculations(iACad);
            ss.Set_AS();



            List<int> ll = ss.Get_FX_Joints(false);



            if (rbtn_fx_negative.Checked)
            {
                //ll = ss.Get_FX_Joints(false);

                Width = ss.list_z[0] - ss.list_z[ss.list_z.Count - 1];
                Height = ss.list_y[1] - ss.list_y[ss.list_y.Count - 1]; 
            }
            else if (rbtn_fx_positive.Checked)
            {
                ll = ss.Get_FX_Joints(true);


                Width = ss.list_z[0] - ss.list_z[ss.list_z.Count - 1];
                Height = ss.list_y[1] - ss.list_y[ss.list_y.Count - 1]; 
            }
            else if (rbtn_fz_positive.Checked)
            {
                ll = ss.Get_FZ_Joints(false);

                Width = ss.list_x[0] - ss.list_x[ss.list_x.Count - 1];
                Height = ss.list_y[1] - ss.list_y[ss.list_y.Count - 1]; 
            }
            else if (rbtn_fz_negative.Checked)
            {
                ll = ss.Get_FZ_Joints(true);
                Width = ss.list_x[0] - ss.list_x[ss.list_x.Count - 1];
                Height = ss.list_y[1] - ss.list_y[ss.list_y.Count - 1]; 
            }

            Width = Math.Abs(Width);
            Height = Math.Abs(Height); 
     

            //Wind Rectangle Width = 13.432 m
            //Wind Rectangle Height = 26.810 m
            //Effective Area Factor = 0.60 (User Input)
            //Wind Rectangle Effective Area = 13.432 x 26.810 x 0.60 =216.067 Sq.m
            //Total Wind Force = 216.067 x 540 N. = 116676 N = 116.676 kN
            //Total Wind Force is to be applied on 30 Nodes as shown
            //Wind Force per Node = 116.676 / 30 = 3.89 kN in FX or FZ directions.

            double Wind_Eff_Area = Width * Height * Area_Factor;

            double total_force = Wind_Eff_Area * Pressure/1000;
            double wind_force_per_node = total_force / ll.Count;



            //ll = ss.Get_FX_Joints(true);

            //ll = ss.Get_FZ_Joints(false);

            //ll = ss.Get_FZ_Joints(true);


            //for (int i = 1; i < ss.list_y.Count; i++)
            //{
                //var jntLst = ss.Get_Floor_Joints(ss.list_y[i]);

                foreach (var item in ll)
                {
                    if (rbtn_fz_positive.Checked)
                    {
                        kStr = item + " FZ " + (wind_force_per_node).ToString("f3");
                    }
                    else if (rbtn_fz_negative.Checked)
                    {
                        kStr = item + " FZ -" + (wind_force_per_node).ToString("f3");
                    }
                    else if (rbtn_fx_positive.Checked)
                    {
                        kStr = item + " FX " + (wind_force_per_node).ToString("f3");
                    }
                    else if (rbtn_fx_negative.Checked)
                    {
                        kStr = item + " FX -" + (wind_force_per_node).ToString("f3");
                    }
                    ASTRA_Data.Add(kStr);
                }
            //}




            //if (rbtn_fz_positive.Checked)
            //{
            //    kStr = kStr + " FZ ";
            //}

            ASTRA_Data.Add(kStr);

            if (Node != null)
            {
                if (btn_add.Text == "Change")
                {

                    //Node = Node.Parent;

                    //Node.Parent.Nodes.Clear();

                    Node.Text = "JOINT LOAD";

                    //var nd = Node.Nodes.Add("JOINT LOAD");

                    foreach (var item in ASTRA_Data)
                    {
                        Node.Nodes.Add(item);

                    }
                    //nd.Nodes.Add(
                    this.Close();
                }
                else
                {

                    //Node.Text = "JOINT LOAD";

                    var nd = Node.Nodes.Add("JOINT LOAD");

                    foreach (var item in ASTRA_Data)
                    {
                        nd.Nodes.Add(item);

                    }


                    //Node.Nodes.Add(kStr);
                    //Node.Expand();
                    Node.ExpandAll();
                }
            }
            //else
            //{
            this.Close();

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    }
}
