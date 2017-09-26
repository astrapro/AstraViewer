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
    public partial class frmAnalysisType : Form
    {
        IASTRACAD iACad = null;
        public frmAnalysisType(IASTRACAD iACAD)
        {
            InitializeComponent();
            this.iACad = iACAD;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void rbtn_static_CheckedChanged(object sender, EventArgs e)
        {
            string txt = "";
            List<string> list_str = new List<string>();


            RadioButton rbtn = sender as RadioButton;
            if (rbtn.Checked)
            {
                switch (rbtn.Name.ToLower())
                {
                    case "rbtn_static":
                        txt = "PERFORM ANALYSIS";
                        grb_dyna.Enabled = false;
                        break;
                    case "rbtn_dynamic":
                        grb_dyna.Enabled = true;
                        rbtn_perform_eigen.Checked = true;
                        break;
                    case "rbtn_perform_eigen":
                        txt_frequencies.Enabled = true;
                        grb_time.Enabled = false;
                        break;
                    case "rbtn_perform_time_history":
                        txt_frequencies.Enabled = false;
                        grb_time.Enabled = true;
                        rbtn_with_nodal_cons.Checked = true;
                        break;
                    case "rbtn_with_nodal_cons":
                        break;
                    case "rbtn_without_nodal_cons":
                        break;
                    case "rbtn_response_spectrum":
                        grb_time.Enabled = false;
                        break;
                }
            }
            list_str.Add(txt);
        }

        private void btn_add_data_Click(object sender, EventArgs e)
        {
            if (rbtn_static.Checked)
            {
                //iACad.WriteFile("PERFORM ANALYSIS", true);
                iACad.Create_Data.Analysis_Data.Add("PERFORM ANALYSIS");
            }
            else if (rbtn_dynamic.Checked)
            {
                if (rbtn_perform_time_history.Checked)
                {
                    frmTimeHistory f_th = new frmTimeHistory(iACad, rbtn_with_nodal_cons.Checked);
                    f_th.Owner = iACad.MainForm;
                    f_th.Show();
                }
                else if (rbtn_response_spectrum.Checked)
                {
                    frmResponse f_res = new frmResponse(iACad);
                    f_res.Owner = iACad.MainForm;
                    f_res.Show();
                }
            }
           
            this.Close();
        }
    }
}
