using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HEADSNeed.ASTRA.CadToAstra;

using HEADSNeed.ASTRA.ASTRAClasses;


namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    public partial class frmResponse : Form
    {
        IASTRACAD iACad = null;

        public List<string> ASTRA_Data { get; set; }
        public frmResponse(IASTRACAD iACAD)
        {
            InitializeComponent();
            this.iACad = iACAD;
            ASTRA_Data = new List<string>();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string kStr = "";
            ASTRA_Data.Clear();

            kStr = "PERFORM RESPONSE SPECTRUM ANALYSIS";
            ASTRA_Data.Add(kStr);

            kStr = string.Format("FREQUENCIES {0}", txtTotalFrequencies.Text);
            ASTRA_Data.Add(kStr);
            kStr = string.Format("CUTOFF FREQUENCY {0}", txtCutOffFrequencies.Text);
            ASTRA_Data.Add(kStr);
            kStr = string.Format("DIRECTION FACTORS X {0} Y {1} Z {2}", txtX.Text, txtY.Text, txtZ.Text);
            ASTRA_Data.Add(kStr);

            if (rbtnDisplacement.Checked)
            {
                kStr = string.Format("SPECTRUM TYPE DISPLACEMENT");
                ASTRA_Data.Add(kStr);
            }
            if (rbtnAcceleration.Checked)
            {
                kStr = string.Format("SPECTRUM TYPE ACCELERATION");
                ASTRA_Data.Add(kStr);
            }
            kStr = string.Format("SPECTRUM POINTS {0}", txtSpectrumPoints.Text);
            ASTRA_Data.Add(kStr);
            
            kStr = string.Format("SCALE FACTOR {0}", txtScaleFactor.Text);
            ASTRA_Data.Add(kStr);
            


            MyStrings mList_Period, mList_Acce;

            if (rbtnAcceleration.Checked)
            {

                kStr = "PERIOD ACCELERATION";
                ASTRA_Data.Add(kStr);
            }
            else
            {
                kStr = "PERIOD DISPLACEMENT";
                ASTRA_Data.Add(kStr);
            }


            for (int i = 0; i < dgv_acceleration.RowCount - 1; i++)
            {
                kStr = string.Format("     {0}    {1}",
                    dgv_acceleration[0, i].Value, dgv_acceleration[1, i].Value);
                ASTRA_Data.Add(kStr);
            }

            DialogResult = DialogResult.OK;
            this.Close();
            #region Ex: 1 (Data)
            //PERFORM RESPONSE SPECTRUM ANALYSIS
            //FREQUENCIES 5
            //CUTOFF FREQUENCY 10.5
            //DIRECTION FACTORS X 1.0 Y 0.6667 Z 0.0 
            //*SPECTRUM TYPE DISPLACEMENT
            //SPECTRUM TYPE ACCELERATION
            //SPECTRUM POINTS 16
            //SCALE FACTOR 1.0
            //*PERIOD DISPLACEMENT
            //PERIOD ACCELERATION 
            //      0.00    46.328
            //      0.02    46.328
            //      0.10   149.023
            //      0.18   207.706
            //      0.20   211.566
            //      0.22   212.725
            //      0.26   210.408
            //      0.30   203.845
            //      0.56   134.352
            //      0.70   110.416
            //      0.90    86.094
            //      1.20    62.929
            //      1.40    52.892
            //      1.80    39.765
            //      2.00    35.518
            //      3.00    21.620\
            #endregion
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void rbtnAcceleration_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbtn = sender as RadioButton;

            if (rbtn.Name == rbtnAcceleration.Name)
            {
                col_acce_disp.HeaderText = "Accelerations";
                grbPeriods.Text = "Period and Accelerations";
            }
            else if (rbtn.Name == rbtnDisplacement.Name)
            {
                col_acce_disp.HeaderText = "Displacement";
                grbPeriods.Text = "Period and Displacement";
            }
        }

        private void frmResponse_Load(object sender, EventArgs e)
        {
            string periods = "0.00,0.02,0.10,0.18,0.20,0.22,0.26,0.30,0.56,0.70,0.90,1.20,1.40,1.80,2.00,3.00";
            string acce = "46.328,46.328,149.023,207.706,211.566,212.725,210.408,203.845,134.352,110.416,86.094,62.929,52.892,39.765,35.518,21.620";


            MyStrings mperiods = new MyStrings(periods, ',');
            MyStrings macce = new MyStrings(acce, ',');

            for (int i = 0; i < mperiods.Count; i++)
            {
                dgv_acceleration.Rows.Add(mperiods.StringList[i], macce.StringList[i]);
            }
            txtSpectrumPoints.Text = (dgv_acceleration.RowCount - 1) + "";
        }

        private void dgv_acceleration_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtSpectrumPoints.Text = (dgv_acceleration.RowCount - 1) + "";
        }

        private void btn_remove_all_Click(object sender, EventArgs e)
        {
            dgv_acceleration.Rows.Clear();
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_acceleration.CurrentCell.RowIndex > -1)
                    dgv_acceleration.Rows.RemoveAt(dgv_acceleration.CurrentCell.RowIndex);
            }
            catch (Exception ex) { }
        }
    }
}
