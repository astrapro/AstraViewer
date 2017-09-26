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
    public partial class frmJointLoad : Form
    {
        List<int> list_joint_No = null;
        IASTRACAD iACad = null;
        public List<string> ASTRA_Data { get; set; }
        public TreeNode Node { get; set; }


        public frmJointLoad(IASTRACAD iACAD)
        {
            InitializeComponent();
            list_joint_No = new List<int>();
            this.iACad = iACAD;
            ASTRA_Data = new List<string>();
        }

        private void frmJointLoad_Load(object sender, EventArgs e)
        {

            if (ASTRA_Data.Count > 0)
            {
                string kStr = ASTRA_Data[0];



                MyStrings mlist = new MyStrings(MyStrings.RemoveAllSpaces(ASTRA_Data[0]), '*');

                if (mlist.Count > 1)
                {
                    kStr = mlist.StringList[0];

                    textBox1.Text = mlist.StringList[1];
                }
                
                mlist = new MyStrings(MyStrings.RemoveAllSpaces(kStr), ' ');



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
                        txt_fx.Text = mlist.StringList[i + 1];
                    }
                    else if (mlist.StringList[i] == "FY")
                    {
                        txt_fy.Text = mlist.StringList[i + 1];
                    }
                    else if (mlist.StringList[i] == "FZ")
                    {
                        txt_fz.Text = mlist.StringList[i + 1];
                    }
                    else if (mlist.StringList[i] == "MX")
                    {
                        txt_mx.Text = mlist.StringList[i + 1];
                    }
                    else if (mlist.StringList[i] == "MY")
                    {
                        txt_my.Text = mlist.StringList[i + 1];
                    }
                    else if (mlist.StringList[i] == "MZ")
                    {
                        txt_mz.Text = mlist.StringList[i + 1];
                    }
                }

                if (r != -1)
                {
                    txt_joint_number.Text = mlist.GetString(0, r - 1);
                }
                btn_jload_add.Text = "Change";
                ASTRA_Data.Clear();
            }
            else
                txt_joint_number.Text = iACad.GetSelectedJointsInText();
        }

        private void btn_jload_add_Click(object sender, EventArgs e)
        {
            string kStr = "";
            kStr = txt_joint_number.Text.Replace(',', ' ');

            double dval = 0.0;

            dval = MyStrings.StringToDouble(txt_fx.Text, 0.0);
            if (dval != 0.0)
            {
                kStr = kStr + " FX " + dval.ToString("f3");
            }
            dval = MyStrings.StringToDouble(txt_fy.Text, 0.0);
            if (dval != 0.0)
            {
                kStr = kStr + " FY " + dval.ToString("f3");
            }
            dval = MyStrings.StringToDouble(txt_fz.Text, 0.0);
            if (dval != 0.0)
            {
                kStr = kStr + " FZ " + dval.ToString("f3");
            }
            dval = MyStrings.StringToDouble(txt_mx.Text, 0.0);
            if (dval != 0.0)
            {
                kStr = kStr + " MX " + dval.ToString("f3");
            }
            dval = MyStrings.StringToDouble(txt_my.Text, 0.0);
            if (dval != 0.0)
            {
                kStr = kStr + " MY " + dval.ToString("f3");
            }
            dval = MyStrings.StringToDouble(txt_mz.Text, 0.0);
            if (dval != 0.0)
            {
                kStr = kStr + " MZ " + dval.ToString("f3");
            }

            if (textBox1.Text != "") kStr = kStr + " *" + textBox1.Text;
          
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
                    Node.Nodes.Add(kStr);
                    //Node.Expand();
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
