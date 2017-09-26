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
    public partial class frmFloorLoad : Form
    {

        List<int> list_mem_Nos = null;
        IASTRACAD iACad = null;
        public List<string> ASTRA_Data { get; set; }
        public TreeNode Node { get; set; }

        public frmFloorLoad(IASTRACAD iACAD)
        {
            InitializeComponent();

            list_mem_Nos = new List<int>();
            this.iACad = iACAD;
            ASTRA_Data = new List<string>();
        }

        private void frmFloorLoad_Load(object sender, EventArgs e)
        {

            if (ASTRA_Data.Count > 0)
            {
                string kStr = ASTRA_Data[0];

                MyStrings mlist = new MyStrings(MyStrings.RemoveAllSpaces(kStr), ' ');

                int r = -1;
                for (int i = 0; i < mlist.Count; i++)
                {

                    if (i == 0)
                    {
                        if (mlist.StringList[i].StartsWith("XR") ||
                            mlist.StringList[i].StartsWith("XL"))
                        {
                            rbtn_XLimit.Checked = true;
                        }
                        if (mlist.StringList[i].StartsWith("YR") ||
                            mlist.StringList[i].StartsWith("YL"))
                        {
                            rbtn_YLimit.Checked = true;
                        }
                        if (mlist.StringList[i].StartsWith("ZR") ||
                            mlist.StringList[i].StartsWith("ZL"))
                        {
                            rbtn_ZLimit.Checked = true;
                        }
                    }
                    if (mlist.StringList[i].StartsWith("FL"))
                    {
                        txt_pressure.Text = mlist.StringList[++i];
                        chk_One.Checked = false;
                    }

                    if (mlist.StringList[i].StartsWith("ON"))
                    {
                        txt_pressure.Text = mlist.StringList[++i];
                        chk_One.Checked = true;
                    }
                    if (mlist.StringList[i].StartsWith("GX"))
                    {
                        rbtn_GX.Checked = true;
                    }
                    if (mlist.StringList[i].StartsWith("GY"))
                    {
                        rbtn_GY.Checked = true;
                       
                    }
                    if (mlist.StringList[i].StartsWith("GZ"))
                    {
                        rbtn_GZ.Checked = true;
                    }

                    if (mlist.StringList[i].StartsWith("XR") ||
                        mlist.StringList[i].StartsWith("XL"))
                    {
                        //rbtn_XLimit.Checked = true;
                        txt_X_min.Text = mlist.StringList[++i];
                        txt_X_max.Text = mlist.StringList[++i];
                    }
                    else if (mlist.StringList[i].StartsWith("YR") ||
                        mlist.StringList[i].StartsWith("YL"))
                    {
                        //rbtn_YLimit.Checked = true;
                        txt_Y_min.Text = mlist.StringList[++i];
                        txt_Y_max.Text = mlist.StringList[++i];
                    }
                    else if (mlist.StringList[i].StartsWith("ZR") ||
                        mlist.StringList[i].StartsWith("ZL"))
                    {
                        //rbtn_ZLimit.Checked = true;
                        txt_Z_min.Text = mlist.StringList[++i];
                        txt_Z_max.Text = mlist.StringList[++i];
                    }


                }


                btn_fload_add.Text = "Change";
                ASTRA_Data.Clear();
            }
            else
            {
                //txt_mnos.Text = iACad.GetSelectedMembersInText();
            }

        }


        private void btn_fload_Click(object sender, EventArgs e)
        {

            string kStr = "";
            //XRANGE 4 18 FLOAD -0.15 YRANGE 0 10.43  ZRANGE -1 5.4  GY
            //YRANGE 4 18 FLOAD -0.15 XRANGE 0 10.43  ZRANGE -1 5.4  GY
            //ZRANGE 4 18 FLOAD -0.15 XRANGE 0 10.43  YRANGE -1 5.4  GY


            double dval = 0.0;

            dval = MyStrings.StringToDouble(txt_pressure.Text, 0.0);




            if (rbtn_XLimit.Checked)
            {
                if (chk_One.Checked)
                {
                    kStr = string.Format("XRANGE {0} {1} ONE {2} YRANGE {3} {4}  ZRANGE {5} {6}",
                        txt_X_min.Text, txt_X_max.Text, txt_pressure.Text,
                        txt_Y_min.Text, txt_Y_max.Text,
                        txt_Z_min.Text, txt_Z_max.Text);
                }
                else
                {
                    kStr = string.Format("XRANGE {0} {1} FLOAD {2} YRANGE {3} {4}  ZRANGE {5} {6}",
                        txt_X_min.Text, txt_X_max.Text, txt_pressure.Text,
                        txt_Y_min.Text, txt_Y_max.Text,
                        txt_Z_min.Text, txt_Z_max.Text);
                }
            }
            else if (rbtn_YLimit.Checked)
            {
                if (chk_One.Checked)
                {
                    kStr = string.Format("YRANGE {0} {1} ONE {2} XRANGE {3} {4}  ZRANGE {5} {6}",
                       txt_Y_min.Text, txt_Y_max.Text, txt_pressure.Text,
                       txt_X_min.Text, txt_X_max.Text,
                       txt_Z_min.Text, txt_Z_max.Text);
                }
                else
                {
                    kStr = string.Format("YRANGE {0} {1} FLOAD {2} XRANGE {3} {4}  ZRANGE {5} {6}",
                       txt_Y_min.Text, txt_Y_max.Text, txt_pressure.Text,
                       txt_X_min.Text, txt_X_max.Text,
                       txt_Z_min.Text, txt_Z_max.Text);
                }
            }
            else if (rbtn_ZLimit.Checked)
            {
                if (chk_One.Checked)
                {
                    kStr = string.Format("ZRANGE {0} {1} ONE {2} XRANGE {3} {4}  YRANGE {5} {6}",
                        txt_Z_min.Text, txt_Z_max.Text, txt_pressure.Text,
                        txt_X_min.Text, txt_X_max.Text,
                        txt_Y_min.Text, txt_Y_max.Text);
                }
                else
                {
                    kStr = string.Format("ZRANGE {0} {1} FLOAD {2} XRANGE {3} {4}  YRANGE {5} {6}",
                        txt_Z_min.Text, txt_Z_max.Text, txt_pressure.Text,
                        txt_X_min.Text, txt_X_max.Text,
                        txt_Y_min.Text, txt_Y_max.Text);
                }
            } 
            


            if (rbtn_GX.Checked) kStr = kStr + " GX";
            else if (rbtn_GY.Checked) kStr = kStr + " GY";
            else if (rbtn_GZ.Checked) kStr = kStr + " GZ";

            ASTRA_Data.Add(kStr);

            if (Node != null)
            {
                if (btn_fload_add.Text == "Change")
                {
                    Node.Text = kStr;
                    this.Close();
                }
                else
                {
                    Node.Nodes.Add(kStr);
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

        

    }
}
