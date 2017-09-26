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
    public partial class frmMovingAnalysisForces : Form
    {
        ASTRADoc astDoc = null;
        LoadDeflection ld = null;
        public frmMovingAnalysisForces(ASTRADoc astDoc)
        {
            InitializeComponent();
            this.astDoc = astDoc;
            ld = new LoadDeflection(astDoc, pbr_forces);

            for (int i = 0; i < ld.MaxLoadCase; i++)
            {
                //ld.
            }
        }

        private void btn_get_forces_Click(object sender, EventArgs e)
        {
            if (astDoc != null)
            {
                //ld.
                //cmb_load_case.Items.Add(astDoc.Load_Geneartion.
            }
        }
    }
}
