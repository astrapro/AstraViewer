using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HEADSNeed.ASTRA.ASTRAForms
{
    public partial class frm_LoadExample : Form
    {
        public int Project_Example 
        {
            get
            {
                if (rbtn2.Checked) return 2;
                return 1;
            }
        }
        public frm_LoadExample()
        {
            InitializeComponent();
            //Project_Example = 1;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            
            if (btn.Name == btn_OK.Name)
                this.DialogResult = DialogResult.OK;
            else
                this.DialogResult = DialogResult.Cancel;

            this.Close();
        }
    }
}
