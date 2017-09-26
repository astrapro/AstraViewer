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
    public partial class frmMemberLoad : Form
    {

        List<int> list_mem_Nos = null;
        IASTRACAD iACad = null;
        public List<string> ASTRA_Data { get; set; }
        public TreeNode Node { get; set; }


        public frmMemberLoad(IASTRACAD iACAD)
        {
            InitializeComponent();
            list_mem_Nos = new List<int>();
            this.iACad = iACAD;
            ASTRA_Data = new List<string>();
        }

        private void frmMemberLoad_Load(object sender, EventArgs e)
        {

            if (ASTRA_Data.Count > 0)
            {
                string kStr = ASTRA_Data[0];

                MyStrings mlist = new MyStrings(MyStrings.RemoveAllSpaces(kStr), ' ');

                int r = -1;

                bool Is_LINEAR = kStr.Contains("LIN");




                for (int i = 0; i < mlist.Count; i++)
                {
                    if (Is_LINEAR)
                    {

                        #region LINEAR Load
                        if (r == -1)
                        {
                            if (mlist.StringList[i] == "LIN")
                            {
                                r = i;
                            }
                        }
                        if (mlist.StringList[i] == "X")
                        {
                            cmb_lload_dir.SelectedItem = mlist.StringList[i];
                            txt_lload_start.Text = mlist.StringList[i + 1];
                        }
                        else if (mlist.StringList[i] == "Y")
                        {
                            cmb_lload_dir.SelectedItem = mlist.StringList[i];
                            txt_lload_start.Text = mlist.StringList[i + 1];
                        }
                        else if (mlist.StringList[i] == "Z")
                        {
                            cmb_lload_dir.SelectedItem = mlist.StringList[i];
                            txt_lload_start.Text = mlist.StringList[i + 1];
                        }
                        else if (mlist.StringList[i] == "GX")
                        {
                            cmb_lload_dir.SelectedItem = mlist.StringList[i];
                            txt_lload_start.Text = mlist.StringList[i + 1];
                        }
                        else if (mlist.StringList[i] == "GY")
                        {
                            cmb_lload_dir.SelectedItem = mlist.StringList[i];
                            txt_lload_start.Text = mlist.StringList[i + 1];
                        }
                        else if (mlist.StringList[i] == "GX")
                        {
                            cmb_lload_dir.SelectedItem = mlist.StringList[i];
                            txt_lload_start.Text = mlist.StringList[i + 1];
                        }
                       

                        if (mlist.StringList[i] == "X" ||
                            mlist.StringList[i] == "Y" ||
                            mlist.StringList[i] == "Z" ||
                            mlist.StringList[i] == "GX" ||
                            mlist.StringList[i] == "GY" ||
                            mlist.StringList[i] == "GZ")
                        {
                            if (mlist.Count > (i + 2))
                            {
                                txt_lload_end.Text = mlist.StringList[i + 2];
                            }
                        }
                        #endregion LINEAR Load
                    }
                    else
                    {
                        #region Member Load
                        if (r == -1)
                        {
                            if (mlist.StringList[i] == "UNI" || mlist.StringList[i].StartsWith("CON") ||
                                mlist.StringList[i].StartsWith("UMOM") || mlist.StringList[i].StartsWith("CMOM"))
                            {
                                r = i;

                                cmb_mload_type.SelectedItem = mlist.StringList[i];
                                continue;

                            }
                        }
                        if (mlist.StringList[i] == "X")
                        {
                            cmb_mload_dir.SelectedItem = mlist.StringList[i];
                            txt_mload_val.Text = mlist.StringList[i + 1];
                        }
                        else if (mlist.StringList[i] == "Y")
                        {
                            cmb_mload_dir.SelectedItem = mlist.StringList[i];
                            txt_mload_val.Text = mlist.StringList[i + 1];
                        }
                        else if (mlist.StringList[i] == "Z")
                        {
                            cmb_mload_dir.SelectedItem = mlist.StringList[i];
                            txt_mload_val.Text = mlist.StringList[i + 1];
                        }
                        else if (mlist.StringList[i] == "GX")
                        {
                            cmb_mload_dir.SelectedItem = mlist.StringList[i];
                            txt_mload_val.Text = mlist.StringList[i + 1];
                        }
                        else if (mlist.StringList[i] == "GY")
                        {
                            cmb_mload_dir.SelectedItem = mlist.StringList[i];
                            txt_mload_val.Text = mlist.StringList[i + 1];
                        }
                        else if (mlist.StringList[i] == "GX")
                        {
                            cmb_mload_dir.SelectedItem = mlist.StringList[i];
                            txt_mload_val.Text = mlist.StringList[i + 1];
                        }
                        else if (mlist.StringList[i] == "PX")
                        {
                            cmb_mload_dir.SelectedItem = mlist.StringList[i];
                            txt_mload_val.Text = mlist.StringList[i + 1];
                        }
                        else if (mlist.StringList[i] == "PY")
                        {
                            cmb_mload_dir.SelectedItem = mlist.StringList[i];
                            txt_mload_val.Text = mlist.StringList[i + 1];
                        }
                        else if (mlist.StringList[i] == "PZ")
                        {
                            cmb_mload_dir.SelectedItem = mlist.StringList[i];
                            txt_mload_val.Text = mlist.StringList[i + 1];
                        }

                        if (mlist.StringList[i] == "X" ||
                            mlist.StringList[i] == "Y" ||
                            mlist.StringList[i] == "Z" ||
                            mlist.StringList[i] == "PX" ||
                            mlist.StringList[i] == "PY" ||
                            mlist.StringList[i] == "PZ" ||
                            mlist.StringList[i] == "GX" ||
                            mlist.StringList[i] == "GY" ||
                            mlist.StringList[i] == "GZ")
                        {
                            if (mlist.Count > (i + 2))
                            {
                                txt_mload_d1.Text = mlist.StringList[i + 2];
                            }
                            if (mlist.Count > (i + 3))
                            {
                                txt_mload_d2.Text = mlist.StringList[i + 3];
                            }
                        }
                        #endregion Member Load
                    }
                }

                if (r != -1)
                {
                    if (Is_LINEAR)
                    {
                        txt_mnos_lin.Text = mlist.GetString(0, r - 1);
                        btn_lload_add.Text = "Change";
                        tabControl1.SelectedTab = tab_LIN;
                    }
                    else
                    {
                        txt_mnos_uni.Text = mlist.GetString(0, r - 1);
                        btn_mload_add.Text = "Change";
                    }
                }
                ASTRA_Data.Clear();
            }
            else
            {
                txt_mnos_uni.Text = iACad.GetSelectedMembersInText();
                txt_mnos_lin.Text = iACad.GetSelectedMembersInText();

                cmb_lload_dir.SelectedIndex = 1;
                cmb_mload_type.SelectedIndex = 0;
                cmb_mload_dir.SelectedIndex = 4;
            }

        }

        private void btn_mload_add_Click(object sender, EventArgs e)
        {

            string kStr = "";
            kStr = txt_mnos_uni.Text.Replace(',', ' ');


            kStr = kStr + " " + cmb_mload_type.Text;
            kStr = kStr + " " + cmb_mload_dir.Text;

            double dval = 0.0;

            dval = MyStrings.StringToDouble(txt_mload_val.Text, 0.0);
            if (dval != 0.0)
            {
                kStr = kStr + " " + dval.ToString("f3");
            }
            dval = MyStrings.StringToDouble(txt_mload_d1.Text, 0.0);
            if (dval != 0.0)
            {
                kStr = kStr + " " + dval.ToString("f3");
            }
            dval = MyStrings.StringToDouble(txt_mload_d2.Text, 0.0);
            if (dval != 0.0)
            {
                kStr = kStr + " " + dval.ToString("f3");
            }


            ASTRA_Data.Add(kStr);


            if (Node != null)
            {
                if (btn_mload_add.Text == "Change")
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

        private void btn_lload_add_Click(object sender, EventArgs e)
        {


            string kStr = "";
            kStr = txt_mnos_lin.Text.Replace(',', ' ');


            kStr = kStr + " LIN ";
            kStr = kStr + " " + cmb_lload_dir.Text;

            double dval = 0.0;

            dval = MyStrings.StringToDouble(txt_lload_start.Text, 0.0);
            if (dval != 0.0)
            {
                kStr = kStr + " " + dval.ToString("f3");
            }
            dval = MyStrings.StringToDouble(txt_lload_end.Text, 0.0);
            if (dval != 0.0)
            {
                kStr = kStr + " " + dval.ToString("f3");
            }
            
            if (ASTRA_Data.Contains(kStr))
                ASTRA_Data.Remove(kStr);


            ASTRA_Data.Add(kStr);

            if (btn_lload_add.Text == "Change")
            {
                Node.Text = kStr;
                this.Close();
            }
            else
            {
                if (Node != null)
                {
                    Node.Nodes.Add(kStr);
                    //Node.Expand();
                    Node.ExpandAll();
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbtn_UNI_CheckedChanged(object sender, EventArgs e)
        {
            //RadioButton rbtn = sender as RadioButton;
            //if (rbtn.Name == rbtn_UNI.Name)
            //{
            //}
            if (rbtn_UNI.Checked) cmb_mload_type.SelectedIndex = 0;
            if (rbtn_UMOM.Checked) cmb_mload_type.SelectedIndex = 1;
            if (rbtn_CON.Checked) cmb_mload_type.SelectedIndex = 2;
            if (rbtn_CMOM.Checked) cmb_mload_type.SelectedIndex = 3;
        }

        private void cmb_mload_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_mload_type.SelectedIndex == 0) rbtn_UNI.Checked = true;
            if (cmb_mload_type.SelectedIndex == 1) rbtn_UMOM.Checked = true;
            if (cmb_mload_type.SelectedIndex == 2) rbtn_CON.Checked = true;
            if (cmb_mload_type.SelectedIndex == 3) rbtn_CMOM.Checked = true;
     
        }


    }
}
