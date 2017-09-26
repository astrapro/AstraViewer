using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HEADSNeed.ASTRA.ASTRAForms
{
    public partial class frm_Modify_Beam_BOQ : Form
    {
        public DataGridView DGV_INPUTS { get; set; }
        public frm_Modify_Beam_BOQ()
        {
            InitializeComponent();
        }

        private void btn_Apply_Click(object sender, EventArgs e)
        {
            if (DGV_INPUTS != null)
            {
                bool flag = false;
                int c = 0;
                for (int i = 0; i < DGV_INPUTS.RowCount; i++)
                {
                    flag = (bool) DGV_INPUTS[1,i].Value;
                    if (flag)
                    {
                        c = 0;
                        DGV_INPUTS[c + 5, i].Value = dgv_beams[c, 0].Value; c++;
                        DGV_INPUTS[c + 5, i].Value = dgv_beams[c, 0].Value; c++;
                        DGV_INPUTS[c + 5, i].Value = dgv_beams[c, 0].Value; c++;
                        DGV_INPUTS[c + 5, i].Value = dgv_beams[c, 0].Value; c++;
                        DGV_INPUTS[c + 5, i].Value = dgv_beams[c, 0].Value; c++;
                        DGV_INPUTS[c + 5, i].Value = dgv_beams[c, 0].Value; c++;
                        DGV_INPUTS[c + 5, i].Value = dgv_beams[c, 0].Value; c++;
                        //DGV_INPUTS[c + 5, i].Value = dgv_beams[c, 0].Value; c++;
                        //DGV_INPUTS[c + 5, i].Value = dgv_beams[c, 0].Value; c++;
                    }
                }
            }
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_Modify_Beam_BOQ_Load(object sender, EventArgs e)
        {
            if(dgv_beams.RowCount == 0)
            {
                int c = 0;
                int i = 0;
                bool flag = false;
               
                if (DGV_INPUTS != null)
                {
                    if (DGV_INPUTS.SelectedCells.Count > 0)
                    {
                        i = DGV_INPUTS.SelectedCells[0].RowIndex;

                        dgv_beams.Rows.Add(DGV_INPUTS[5, i].Value,
                            DGV_INPUTS[6, i].Value,
                            DGV_INPUTS[7, i].Value,
                            DGV_INPUTS[8, i].Value,
                            DGV_INPUTS[9, i].Value,
                            DGV_INPUTS[10, i].Value,
                            DGV_INPUTS[11, i].Value,
                            DGV_INPUTS[12, i].Value,
                            DGV_INPUTS[13, i].Value);

                    }
                    else
                    {
                        for (i = 0; i < DGV_INPUTS.RowCount; i++)
                        {
                            flag = (bool)DGV_INPUTS[1, i].Value;
                            if (flag)
                            {

                                dgv_beams.Rows.Add(DGV_INPUTS[6, i].Value,
                                    DGV_INPUTS[7, i].Value,
                                    DGV_INPUTS[8, i].Value,
                                    DGV_INPUTS[9, i].Value,
                                    DGV_INPUTS[10, i].Value,
                                    DGV_INPUTS[11, i].Value,
                                    DGV_INPUTS[12, i].Value,
                                    DGV_INPUTS[13, i].Value,
                                    DGV_INPUTS[14, i].Value);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
