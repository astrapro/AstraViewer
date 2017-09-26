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
    public partial class frmAreaLoad : Form
    {

        List<int> list_mem_Nos = null;
        IASTRACAD iACad = null;
        public List<string> ASTRA_Data { get; set; }
        public TreeNode Node { get; set; }

        public frmAreaLoad(IASTRACAD iACAD)
        {
            InitializeComponent();
            list_mem_Nos = new List<int>();
            this.iACad = iACAD;
            ASTRA_Data = new List<string>();
        }

        private void btn_aload_Click(object sender, EventArgs e)
        {

            string kStr = "";
            kStr = txt_mnos.Text.Replace(',', ' ');


            kStr = kStr + " ALOAD ";

            double dval = 0.0;

            dval = MyStrings.StringToDouble(txt_aload_val.Text, 0.0);
            if (dval != 0.0)
            {
                kStr = kStr + " " + dval.ToString("f3");
            }

            ASTRA_Data.Add(kStr);

            if (Node != null)
            {
                if (btn_aload_add.Text == "Change")
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

        private void frmAreaLoad_Load(object sender, EventArgs e)
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
                        if (mlist.StringList[i] == "ALOAD")
                        {
                            r = i;
                            txt_mnos.Text = mlist.GetString(0, r - 1);
                            txt_aload_val.Text = mlist.StringList[i + 1];
                        }
                    }
                }


                btn_aload_add.Text = "Change";
                ASTRA_Data.Clear();
            }
            else
            {
                txt_mnos.Text = iACad.GetSelectedMembersInText();
            }

        }

    }
}
