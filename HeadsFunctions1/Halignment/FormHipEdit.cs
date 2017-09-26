using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HeadsUtils;

namespace HeadsFunctions1.Halignment
{
    internal partial class FormHipEdit : Form
    {
        double dInitialRadiusValue = 0;
        public FormHipEdit(CHHipData hhip, string strCaption)
        {
            InitializeComponent();
            //set the data
            this.txtXValue_.Value = hhip.xvalue;
            this.txtYValue_.Value = hhip.yvalue;
            this.txtRadius_.Value = hhip.radius;
            this.txtLeadTrans_.Value = hhip.leadtrans;
            this.txtTrailTrans_.Value = hhip.trailtrans;

            this.Text = strCaption;

            this.dInitialRadiusValue = hhip.radius;
        }

        public CHHipData HipData
        {
            get
            {
                CHHipData hhip = new CHHipData();

                hhip.xvalue = this.txtXValue_.Value;
                hhip.yvalue = this.txtYValue_.Value;
                hhip.radius = this.txtRadius_.Value;
                hhip.leadtrans = this.txtLeadTrans_.Value;
                hhip.trailtrans = this.txtTrailTrans_.Value;

                
                if (this.dInitialRadiusValue < 0)
                {
                    if (hhip.radius > 0)
                        hhip.radius  = hhip.radius * -1;
                }
                else
                {
                    if (hhip.radius < 0)
                        hhip.radius = hhip.radius * -1;
                }
                
                return hhip;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}