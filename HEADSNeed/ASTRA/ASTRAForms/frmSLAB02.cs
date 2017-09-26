using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HEADSNeed.ASTRA.ASTRAClasses.SlabDesign;
using HEADSNeed.ASTRA.ASTRAClasses;
using System.IO;

namespace HEADSNeed.ASTRA.ASTRAForms
{
    public partial class frmSLAB02 : Form
    {
        SLAB02 slb02 = null;
        public frmSLAB02(string dataPath)
        {
            string fName = Path.Combine(Application.StartupPath,"vdres");
            fName = Path.Combine(fName,"kValue.txt");
            InitializeComponent();
            slb02 = new SLAB02(fName,dataPath);
        }

        private void frmSLAB02_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SetValues();
        }
        private void SetValues()
        {
            try
            {
                slb02.D1 = MyStrings.StringToDouble(txt_d1.Text, 0);
                slb02.D2 = MyStrings.StringToDouble(txt_d2.Text, 0);
                slb02.Ads = MyStrings.StringToDouble(txt_Ads.Text, 0);
                slb02.Alpha = MyStrings.StringToDouble(txt_alpha.Text, 0);
                slb02.B = MyStrings.StringToDouble(txt_B.Text, 0);
                slb02.Beta = MyStrings.StringToDouble(txt_beta.Text, 0);
                slb02.Delta = MyStrings.StringToDouble(txt_delta.Text, 0);
                slb02.Gamma = MyStrings.StringToDouble(txt_gamma.Text, 0);
                slb02.H1 = MyStrings.StringToDouble(txt_h1.Text, 0);
                slb02.H2 = MyStrings.StringToDouble(txt_h2.Text, 0);
                slb02.L = MyStrings.StringToDouble(txt_L.Text, 0);
                slb02.Lamda = MyStrings.StringToDouble(txt_lamda.Text, 0);
                slb02.LL = MyStrings.StringToDouble(txt_LL.Text, 0);
                slb02.Sigma_ck = MyStrings.StringToDouble(txt_sigma_ck.Text, 0);
                slb02.Sigma_y = MyStrings.StringToDouble(txt_sigma_y.Text, 0);
                slb02.Tc = MyStrings.StringToDouble(txt_Tc.Text, 0);
                slb02.Slab_Load = MyStrings.StringToDouble(txt_slab_load.Text, 0);

                slb02.CalculateMethod();
            }
            catch (Exception ex) { }
        }
    }
}
