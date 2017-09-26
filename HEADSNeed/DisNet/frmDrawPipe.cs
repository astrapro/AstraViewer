using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VectorDraw.Professional.Dialogs;

using HEADSNeed.DisNet.DisNetClasses;

namespace HEADSNeed.DisNet
{
    public partial class frmDrawPipe : Form
    {
        Color cc;
        public frmDrawPipe()
        {
            InitializeComponent();
            //cc = new Color();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            //DisNetDoc doc = new DisNetDoc(@"D:\TEST DISNET\New Folder\model.txt");
            //doc.ReadFromFile();


            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        public Color PipeColor
        {
            get
            {
                return lblColor.BackColor;
            }
            set
            {
                lblColor.BackColor = value;

            }
        }
        public double PipeSize
        {
            get
            {
                double dd = 100.0d;
                try
                {
                    dd = double.Parse(txtPipeSize.Text);
                }
                catch (Exception ex)
                {
                    dd = 100.0d;
                }
                return dd;
            }
            set
            {
                txtPipeSize.Text = value.ToString();
            }
        }

        private void lblColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() != DialogResult.Cancel)
            {
                PipeColor = colorDialog1.Color;
            }
        }

        private void frmDrawPipe_Load(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtPipeSize_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
