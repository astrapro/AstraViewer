using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HEADSNeed.ASTRA.ASTRAClasses;
using HEADSNeed.ASTRA.CadToAstra;

namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    public partial class frmMemberRelease : Form
    {
        IASTRACAD iACad = null;
        public string ASTRA_Data { get; set; }
        public TreeView TRV { get; set; }
        public frmMemberRelease(IASTRACAD iACAD)
        {
            InitializeComponent();
            this.iACad = iACAD;
            ASTRA_Data = "";
        }

        private void frmMemberRelease_Load(object sender, EventArgs e)
        {
            if (ASTRA_Data != "")
            {
                chk_Fx.Checked = false;
                chk_Mx.Checked = false;

                chk_Fy.Checked = false;
                chk_My.Checked = false;

                chk_Fz.Checked = false;
                chk_Mz.Checked = false;


                MyStrings mlist = new MyStrings(MyStrings.RemoveAllSpaces(ASTRA_Data), ' ');

                string kStr = "";
                int r = -1;
                for (int i = 0; i < mlist.Count; i++)
                {
                    if (mlist.StringList[i].StartsWith("START"))
                    {
                        r = i;

                        cmb_node.SelectedIndex = 0;
                    }
                    else if (mlist.StringList[i].StartsWith("END"))
                    {
                        r = i;
                        cmb_node.SelectedIndex = 1;
                        //break;
                    }
                    else if (mlist.StringList[i].StartsWith("FX"))
                    {
                        chk_Fx.Checked = true;
                    }
                    else if (mlist.StringList[i].StartsWith("FY"))
                    {
                        chk_Fy.Checked = true;
                    }
                    else if (mlist.StringList[i].StartsWith("FZ"))
                    {
                        chk_Fz.Checked = true;
                    }
                    else if (mlist.StringList[i].StartsWith("MX"))
                    {
                        chk_Mx.Checked = true;
                    }
                    else if (mlist.StringList[i].StartsWith("MY"))
                    {
                        chk_My.Checked = true;
                    }
                    else if (mlist.StringList[i].StartsWith("MZ"))
                    {
                        chk_Mz.Checked = true;
                    }
                }

                if (r != -1)
                {
                    txt_mem_nos.Text = mlist.GetString(0, r - 1).Trim();
                }
                btn_add_data.Text = "Change";
            }
            else
            {
                cmb_node.SelectedIndex = 0;
                txt_mem_nos.Text = iACad.GetSelectedMembersInText();
            }
        }

        private void btn_add_data_Click(object sender, EventArgs e)
        {
            string kStr = "MEMBER RELEASE";
            //iACad.WriteFile(kStr);

            kStr = txt_mem_nos.Text.Replace(',', ' ');

            kStr += " " + cmb_node.Text + " ";

            if (chk_Fx.Checked)
            {
                kStr += "FX ";
            }
            if (chk_Fy.Checked)
            {
                kStr += "FY ";
            }
            if (chk_Fz.Checked)
            {
                kStr += "FZ ";
            }
            if (chk_Mx.Checked)
            {
                kStr += "MX ";
            }
            if (chk_My.Checked)
            {
                kStr += "MY ";
            }
            if (chk_Mz.Checked)
            {
                kStr += "MZ ";
            }



            //ASTRA_Data = kStr;
            if (TRV != null)
            {
                if (ASTRA_Data != "")
                {
                    TRV.SelectedNode.Text = kStr;
                    this.Close();
                }
                else
                    TRV.Nodes.Add(kStr);
            }
            else
                this.Close();
            //iACad.WriteFile(kStr);
            //iACad.Create_Data.Member_Release_Data.Add(kStr);
            //this.Close();

            //if (kStr != "")
            //{
            //    if (iACad.Create_Data.Member_Release_Data.Contains(kStr))
            //        iACad.Create_Data.Member_Release_Data.Remove(kStr);
            //    iACad.Create_Data.Member_Release_Data.Add(kStr);
            //    txt_mem_nos.Text = "";
            //}
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            //DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
