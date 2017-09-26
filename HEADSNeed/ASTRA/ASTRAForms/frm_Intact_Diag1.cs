using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HEADSNeed.ASTRA.ASTRAClasses;

namespace HEADSNeed.ASTRA.ASTRAForms
{
    public partial class frm_Intact_Diag1 : Form
    {
        public frm_Intact_Diag1()
        {
            InitializeComponent();
        }
        public int Rebar_Nos
        {
            get
            {
                return MyStrings.StringToInt(txt_rebar_nos.Text, 0);
            }
            set
            {
                txt_rebar_nos.Text = value.ToString(); ;
            }
        }
        public double Fy
        {
            get
            {
                return MyStrings.StringToDouble(txt_rebar_fck.Text, 0);
            }
            set
            {
                txt_rebar_fck.Text = value.ToString(); ;
            }
        }

        public int Side
        {
            get
            {
                if (rbtn_2_side.Checked) return 2;
                return 4;
            }
        }

        public string d_dash_by_D
        {
            get
            {
                return txt_d_D.Text;
            }
            set
            {
                txt_d_D.Text = value;
            }
        }

        private void frm_Intact_Diag1_Load(object sender, EventArgs e)
        {
            if (Rebar_Nos % 4 == 0) rbtn_4_side.Checked = true;
            else rbtn_2_side.Checked = true;
        }

        private void rbtn_side_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_2_side.Checked)
                pcb.BackgroundImage = HEADSNeed.Properties.Resources.Reinf_2_side;
            else
                pcb.BackgroundImage = HEADSNeed.Properties.Resources.Reinf_4_side;
        }

        private void btn_proceed_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Name == btn_proceed.Name)
            {
                if (Side == 4)
                {
                    if (Rebar_Nos % 4 != 0)
                    {
                        MessageBox.Show("Total Bars = " + Rebar_Nos + " nos. "
                            + "This is not equally distributable on Four Sides So, "
                            + "selected Interaction Diagram is not applied.Either "
                            + "select Interaction for Rebars 2 Sides or For Rebars 4 sides enter "
                            + "Bar Nos (see input screen) divisible by 4.");

                        rbtn_2_side.Checked = true;
                        return;
                    }
                }
                this.DialogResult = DialogResult.OK;
            }
            else if (btn.Name == btn_cancel.Name)
            {
                this.DialogResult = DialogResult.Cancel;
            }
            this.Close();
        }
    }
}
