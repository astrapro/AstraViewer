using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HEADSNeed.ASTRA.CadToAstra;

using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdCollections;
namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    public partial class frmLoad : Form
    {
        IASTRACAD iACad = null;
        string kStr = "";
        List<string> list_mem_load = null;
        List<string> list_load_comb = null;
        List<string> list_area_load = null;
        List<string> list_joint_load = null;
        public frmLoad(IASTRACAD iACAD)
        {
            InitializeComponent();
            this.iACad = iACAD;
            list_mem_load = new List<string>();
            list_load_comb = new List<string>();
            list_area_load = new List<string>();
            list_joint_load = new List<string>();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btn_add_data_Click(object sender, EventArgs e)
        {

            kStr = "UNIT " + cmbMassUnit.Text + " " + cmbLengthUnit.Text;
            iACad.Create_Data.Load_Unit = kStr;
            
            //iACad.WriteFile(kStr);


            kStr = string.Format("LOAD {0} {1}",
                txt_Load_Number.Text,
                txt_remarks.Text);

            //iACad.WriteFile(kStr, true);
            iACad.Create_Data.Load_Data.Add(kStr);

            if (chk_selfweight.Checked)
            {
                kStr = string.Format("SELF {0} {1}",
                    cmb_Self_Weight.Text,
                    txt_seft_wt_Value.Text);
            }

            iACad.Create_Data.Load_Data.Add(kStr);
            //iACad.WriteFile(kStr, true);


            if (list_mem_load.Count > 0)
            {
                kStr = "MEMB LOAD";
                iACad.WriteFile(kStr, true);
                foreach (string str in list_mem_load)
                {
                    //iACad.WriteFile(str, true);
                    iACad.Create_Data.MemberLoad_Data.Add(str);
                }
            }

            if (list_joint_load.Count > 0)
            {
                kStr = "JOINT LOAD";
                iACad.WriteFile(kStr, true);
                foreach (string str in list_joint_load)
                {
                    //iACad.WriteFile(str, true);
                    iACad.Create_Data.JointLoad_Data.Add(str);
                }
            }
            if (list_area_load.Count > 0)
            {
                kStr = "AREA LOAD";
                //iACad.WriteFile(kStr, true);
                //kStr = string.Format("LOAD {0} {1}",
                //    txt_Load_Number.Text,
                //    txt_remarks.Text);
                //iACad.WriteFile(kStr, true);

                foreach (string str in list_area_load)
                {
                    //iACad.WriteFile(str, true);
                    iACad.Create_Data.AreaLoad_Data.Add(str);
                }
            }


            if (txt_repeat_load_number.Text != "" && txt_repeat_load_factor.Text != "")
            {
                kStr = "REPEAT LOAD";
                //iACad.WriteFile(kStr, true);

                kStr = string.Format("LOAD {0} {1}",
                   txt_repeat_load_number.Text,
                   txt_repeat_load_remarks.Text);
                //iACad.WriteFile(kStr, true);
                iACad.Create_Data.RepeatLoad_Data.Add(kStr);


                kStr = txt_repeat_load_number.Text + " " + txt_repeat_load_factor.Text;
                iACad.Create_Data.RepeatLoad_Data.Add(kStr);
                //iACad.WriteFile(kStr, true);
            }

            if (list_load_comb.Count > 0)
            {
                kStr = "LOAD COMB " + txt_load_comb_load_number.Text;

                iACad.Create_Data.CombinationLoad_Data.Add(kStr);
                //iACad.WriteFile(kStr, true);

                kStr = "";
                foreach (string str in list_load_comb)
                {
                    kStr += str + " ";
                }
                iACad.Create_Data.CombinationLoad_Data.Add(kStr );
               
                //iACad.WriteFile(kStr, true);
            }
            this.Close();
        }

        private void btn_mem_next_load_Click(object sender, EventArgs e)
        {
            if (txt_mem_val.Text == "") return;

            if (cmb_member_load.SelectedIndex == 0)
            {
                kStr = string.Format("{0} UNI {1} {2}",
                   txt_member_numbers.Text,
                   cmb_load_dir.Text,
                   txt_mem_val.Text);
                //iACad.Create_Data.MemberLoad_Data.Add(kStr);
            }
            else if (cmb_member_load.SelectedIndex == 1)
            {
                kStr = string.Format("{0} CONC {1} {2} DIST {3}",
                   txt_member_numbers.Text,
                   cmb_load_dir.Text,
                   txt_mem_val.Text,
                   txt_distance.Text);
                //iACad.Create_Data.MemberLoad_Data.Add(kStr);
            }
            list_mem_load.Add(kStr);
            txt_mem_val.Text = "";
            txt_distance.Text = "0.0";

        }

        private void btn_joint_next_load_Click(object sender, EventArgs e)
        {
            if (txt_joint_val.Text == "") return;

            kStr = string.Format("{0} {1} {2}",
                  txt_joint_number.Text,
                  cmb_joint_dir.Text,
                  txt_joint_val.Text);
            list_joint_load.Add(kStr);

            //iACad.Create_Data.JointLoad_Data.Add(kStr);

            
            txt_joint_val.Text = "";
            txt_joint_number.Text = "";
        }
        public void ShowMemberInText()
        {
            List<int> list_mem_No = new List<int>();
            List<vdLine> list_lines = new List<vdLine>();
            vdSelection gripset = iACad.GetGripSelection(false);

            //if (gripset.Count > 1) return;
            vdLine ln;
            foreach (vdFigure fig in gripset)
            {
                ln = fig as vdLine;
                if (ln != null)
                {
                    list_lines.Add(ln);
                }
            }
            foreach (vdLine line in list_lines)
            {
                list_mem_No.Add(GetMemberNo(line));
            }
            list_mem_No.Sort();


            
            for (int i = 0; i < list_mem_No.Count; i++)
            {
                if (i == list_mem_No.Count - 1)
                {
                    txt_member_numbers.Text += list_mem_No[i].ToString();
                }
                else
                {
                    txt_member_numbers.Text += list_mem_No[i].ToString() + ",";
                }
            }
        }
        public int GetMemberNo(vdLine line)
        {
            //int mNo = -1;
            for (int i = 0; i < iACad.AstraDocument.Members.Count; i++)
            {
                if (iACad.AstraDocument.Members[i].StartNode.Point == line.StartPoint &&
                    iACad.AstraDocument.Members[i].EndNode.Point == line.EndPoint)
                {
                    return iACad.AstraDocument.Members[i].MemberNo;
                }
                else if (iACad.AstraDocument.Members[i].StartNode.Point == line.EndPoint &&
                     iACad.AstraDocument.Members[i].EndNode.Point == line.StartPoint)
                {
                    return iACad.AstraDocument.Members[i].MemberNo;
                }
            }
            return -1;
        }
        private void btn_area_next_load_Click(object sender, EventArgs e)
        {
            kStr = string.Format("{0} ALOAD {1}",
                  txt_area_mem_number.Text,
                  txt_area_load.Text);
            list_area_load.Add(kStr);
            //iACad.Create_Data.AreaLoad_Data.Add(kStr);
        }

        private void btn_load_comb_next_load_Click(object sender, EventArgs e)
        {
            //kStr = string.Format("LOAD COMB {0}",
            //      txt_load_comb_load_number.Text);

            kStr = string.Format("{0} {1}",
                  txt_load_comb_prev_load_number.Text,
                  txt_load_comb_load_Factor.Text);

            list_load_comb.Add(kStr);

            txt_load_comb_prev_load_number.Text = "";
            txt_load_comb_load_Factor.Text = "";
        }

        private void cmb_member_load_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_member_load.SelectedIndex == 0)
            {
                txt_distance.Enabled = false;
            } 
            if (cmb_member_load.SelectedIndex == 1)
            {
                txt_distance.Enabled = true;
            }
        }

        private void frmLoad_Load(object sender, EventArgs e)
        {
            cmb_member_load.SelectedIndex = 0;
            cmb_load_dir.SelectedIndex = 1;
            cmb_Self_Weight.SelectedIndex = 1;
            cmb_joint_dir.SelectedIndex = 2;


            cmbMassUnit.SelectedIndex = (int)iACad.MassUnit;
            cmbLengthUnit.SelectedIndex = (int)iACad.LengthUnit; ;

            //cmbMassUnit.SelectedIndex = 2;
            //cmbLengthUnit.SelectedIndex = 0;



            txt_member_numbers.Text = iACad.GetSelectedMembersInText();
            txt_area_mem_number.Text = txt_member_numbers.Text;
            txt_joint_number.Text = iACad.GetSelectedJointsInText();
        }

        private void chk_selfweight_CheckedChanged(object sender, EventArgs e)
        {
            cmb_Self_Weight.Enabled = chk_selfweight.Checked;
            txt_seft_wt_Value.Enabled = chk_selfweight.Checked;
        }
    }
}
