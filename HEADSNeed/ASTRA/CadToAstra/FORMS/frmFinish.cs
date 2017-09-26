using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HEADSNeed.ASTRA.CadToAstra;

namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    public partial class frmFinish : Form
    {
        IASTRACAD iACad = null;
        public frmFinish(IASTRACAD iACAD)
        {
            InitializeComponent();
            iACad = iACAD;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btn_add_data_Click(object sender, EventArgs e)
        {
            string kStr = "";
            iACad.Create_Data.Finish_Statement_Data.Clear();
            if (chk_print_support.Checked)
            {
                kStr = chk_print_support.Text;
                //iACad.WriteFile(kStr);
                iACad.Create_Data.Finish_Statement_Data.Add(kStr);
            } 
            if (chk_print_max_force.Checked)
            {
                kStr = chk_print_max_force.Text + " " + txt_max.Text;
                //iACad.WriteFile(kStr);
                iACad.Create_Data.Finish_Statement_Data.Add(kStr);
            }
            kStr = "FINISH";
            //iACad.WriteFile(kStr);
            this.Close();
        }

        private void frmFinish_Load(object sender, EventArgs e)
        {

        }
    }
}
