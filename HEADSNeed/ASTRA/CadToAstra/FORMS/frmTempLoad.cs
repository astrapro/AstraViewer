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
    public partial class frmTempLoad : Form
    {
        List<int> list_joint_No = null;
        IASTRACAD iACad = null;
        public List<string> ASTRA_Data { get; set; }
        public TreeNode Node { get; set; }

        public frmTempLoad(IASTRACAD iACAD)
        {
            InitializeComponent();
            list_joint_No = new List<int>();
            this.iACad = iACAD;
            ASTRA_Data = new List<string>();
        }

        private void frmTempLoad_Load(object sender, EventArgs e)
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
                        if (mlist.StringList[i] == "TEMP")
                        {
                            r = i;
                            txt_joint_number.Text = mlist.GetString(0, r - 1);
                            txt_init_temp.Text = mlist.StringList[i + 1];
                            if (mlist.Count > (i + 2))
                            {
                                txt_final_temp.Text = mlist.StringList[i + 2];
                            }
                        }
                    }
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

            dval = MyStrings.StringToDouble(txt_init_temp.Text, 0.0);
            if (dval != 0.0)
            {
                kStr = kStr + " TEMP " + dval.ToString("f3");
            }
            dval = MyStrings.StringToDouble(txt_final_temp.Text, 0.0);
            if (dval != 0.0)
            {
                kStr = kStr + " " + dval.ToString("f3");
            }


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
