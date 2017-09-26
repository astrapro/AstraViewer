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
    public partial class frmTimeHistory : Form
    {
        IASTRACAD iACad = null;
        bool with_node = true;
        string kStr = "";
        public List<string> ASTRA_Data { get; set; }

        public frmTimeHistory(IASTRACAD iACAD, bool withNode)
        {
            InitializeComponent();
            this.iACad = iACAD;
            with_node = withNode;
            chkNodalConstraint.Checked = withNode;

            ASTRA_Data = new List<string>();
        }

        private void frmTimeHistory_Load(object sender, EventArgs e)
        {
            chkNodalConstraint.Checked = with_node;

            txtNodeNumbers.Text = iACad.GetSelectedJointsInText();
            txtMemberNumbers.Text = iACad.GetSelectedMembersInText();
        }
        private void chkNodalConstraint_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNodalConstraint.Checked)
            {
                grbNodalConstraint.Enabled = true;
            }
            else
                grbNodalConstraint.Enabled = false;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            ASTRA_Data.Clear();

            kStr = "PERFORM TIME HISTORY ANALYSIS";
            ASTRA_Data.Add(kStr);
            //iACad.WriteFile(kStr, true);
            kStr = string.Format("FREQUENCIES {0}", txtTotalFrequencies.Text);
            ASTRA_Data.Add(kStr);
            //iACad.WriteFile(kStr, true);
            kStr = string.Format("TIME STEPS {0}", txtTimeSteps.Text);
            ASTRA_Data.Add(kStr);
            //iACad.WriteFile(kStr, true);
            kStr = string.Format("PRINT INTERVAL {0}", txtPrintInterval.Text);
            ASTRA_Data.Add(kStr);
            //iACad.WriteFile(kStr, true);
            kStr = string.Format("STEP INTERVAL {0}", txtStepInterval.Text);
            ASTRA_Data.Add(kStr);
            //iACad.WriteFile(kStr, true);
            kStr = string.Format("DUMPING FACTOR {0}", txtDampingFactor.Text);
            ASTRA_Data.Add(kStr);
            //iACad.WriteFile(kStr, true);
            kStr = string.Format("GROUND MOTION {0}", cmbGroundMotion.Text);
            ASTRA_Data.Add(kStr);
            //iACad.WriteFile(kStr, true);
            kStr = string.Format("X DIVISION {0}", txtXDivision.Text);
            ASTRA_Data.Add(kStr);
            //iACad.WriteFile(kStr, true);
            kStr = string.Format("SCALE FACTOR {0}", txtScaleFactor.Text);
            ASTRA_Data.Add(kStr);
            //iACad.WriteFile(kStr, true);

            kStr = txtTimeValues.Text.Replace(',', ' ').Trim();
            kStr = string.Format("TIME VALUES {0}", kStr);
            ASTRA_Data.Add(kStr);
            //iACad.WriteFile(kStr, true);

            kStr = txtTimeFunction.Text.Replace(',', ' ').Trim();
            kStr = string.Format("TIME FUNCTION {0}", kStr);
            ASTRA_Data.Add(kStr);
            //iACad.WriteFile(kStr, true);

            if (chkNodalConstraint.Checked)
            {
                kStr = string.Format("NODAL CONSTRAINT DOF", txtTimeFunction.Text);
                ASTRA_Data.Add(kStr);
                //iACad.WriteFile(kStr, true);
                kStr = txtNodeNumbers.Text.Replace(',', ' ');

                MyStrings mList = new MyStrings(kStr, ' ');

                string movement = "";

                #region MOVEMENT
                if (chkTx.Checked)
                {
                    movement = "TX ";
                }
                if (chkTy.Checked)
                {
                    movement += "TY ";
                }
                if (chkTz.Checked)
                {
                    movement += "TZ ";
                }
                if (chkRx.Checked)
                {
                    movement += "RX ";
                }
                if (chkRy.Checked)
                {
                    movement += "RY ";
                }
                if (chkRz.Checked)
                {
                    movement += "RZ ";
                }
                #endregion
                foreach (var item in MyStrings.Get_Array_Intiger(kStr))
                {
                    kStr = string.Format("{0} {1}", item, movement);
                    ASTRA_Data.Add(kStr); 
                }
            }
            else
            {
            }
            kStr = "MEMBER STRESS COMPONENT";
            ASTRA_Data.Add(kStr);
            //iACad.WriteFile(kStr, true);
            kStr = string.Format("{0} {1}", txtMemberNumbers.Text, ((rbtnStart.Checked == true) ? "START" : "END"));
            ASTRA_Data.Add(kStr);
            //iACad.WriteFile(kStr, true);
            this.Close();
        }
    }
}
