using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using HEADSNeed.ASTRA.ASTRAClasses;
using HEADSNeed.ASTRA.ASTRAClasses.SlabDesign;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;

namespace HEADSNeed.ASTRA.ASTRAForms
{
    public partial class frmSLAB01 : Form
    {
        SLAB01 slb01 = null;
        string FilePath = "";
        public frmSLAB01(string filePath,vdDocument doc)
        {
            InitializeComponent();
            slb01 = new SLAB01(doc);
            if (File.Exists(filePath))
            {
                FilePath = Path.GetDirectoryName(filePath);
            }
            else
            {
                if (Directory.Exists(filePath))
                    FilePath = filePath;
                else
                    throw new Exception("INVALID Path " + filePath);
            }
        }
        public bool Set_SLAB01()
        {
            bool success = false;
            try
            {
                slb01.D1 = double.Parse(txt_d1.Text);
                slb01.D2 = double.Parse(txt_d2.Text);
                slb01.H = double.Parse(txt_h.Text);
                slb01.Gamma_C = double.Parse(txt_gamma_c.Text);
                slb01.Gc = double.Parse(txt_Gc.Text);
                slb01.L = double.Parse(txt_L.Text);
                slb01.B1 = double.Parse(txt_b1.Text);
                slb01.B2 = double.Parse(txt_b2.Text);
                slb01.W1 = double.Parse(txt_w1.Text);
                slb01.W2 = double.Parse(txt_w2.Text);
                slb01.Dst = double.Parse(txt_Dst.Text);
                slb01.Sigma_St = double.Parse(txt_sigma_st.Text);
                success = true;
            }
            catch (Exception ex)
            {
                success = false;
            }
            return success;
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (Set_SLAB01())
            {
                FilePath = Path.Combine(FilePath,"DESIGN_SLAB01");
                DirectoryInfo dInfo =  Directory.CreateDirectory(FilePath);
                string fName = Path.Combine(FilePath, "SLAB01.txt");
                //if (slb01.CalculateProgram(fName))
                //{
                //    MessageBox.Show("Report written to file " + fName,"ASTRA",MessageBoxButtons.OK,MessageBoxIcon.Information);
                //}
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSLAB01_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
