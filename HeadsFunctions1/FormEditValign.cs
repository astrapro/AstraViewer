using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HeadsFunctions1.Valignment
{
    public partial class FormEditValign : Form
    {
        # region private data
        bool closingByOK = false;
        # endregion

        # region constructors
        public FormEditValign()
        {
            InitializeComponent();
            this.comboBoxSymmetrical.SelectedIndex = 0;
        }
        # endregion

        # region Properties
        public string ChainX
        {
            get { return this.textBoxVipChain.Text; }
            set { this.textBoxVipChain.Text = value; }
        }
        public string ChainY
        {
            get { return this.textBoxVipLevel.Text; }
            set { this.textBoxVipLevel.Text = value; }
        }
        public string Symmetrical
        {
            get { return this.comboBoxSymmetrical.SelectedItem.ToString(); }
            set
            {
                //this.comboBoxSymmetrical.SelectedItem = value;
                foreach (object item in this.comboBoxSymmetrical.Items)
                {
                    if (item.ToString() == value)
                    {
                        this.comboBoxSymmetrical.SelectedItem = item;
                    }
                }
            }
        }
        public string Vcl1
        {
            get { return this.textBoxVclVcl1.Text; }
            set { this.textBoxVclVcl1.Text = value; }
        }
        public string Vcl2
        {
            get
            {
                if (this.textBoxVcl2.Enabled)
                {
                    return this.textBoxVcl2.Text;
                }
                else
                {
                    return "0.0";
                }
            }
            set { this.textBoxVcl2.Text = value; }
        }
        # endregion

        # region event handlers
        private void comboBoxSymmetrical_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.Compare(this.comboBoxSymmetrical.SelectedItem.ToString(), "NO", true) == 0)
            {
                this.textBoxVcl2.Enabled = true;
            }
            else
            {
                this.textBoxVcl2.Enabled = false;
            }
        }

        private void FormEditValign_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.closingByOK == false)
            {
                return;
            }
            
            if (string.IsNullOrEmpty(this.textBoxVipChain.Text))
            {
                this.errorProvider1.SetError(this.textBoxVipChain, "A Valid value must be provided");
                e.Cancel = true;
                return;
            }
            else
            {
                this.errorProvider1.SetError(this.textBoxVipChain, string.Empty);
            }
            if (string.IsNullOrEmpty(this.textBoxVipLevel.Text))
            {
                this.errorProvider1.SetError(this.textBoxVipLevel, "A Valid value must be provided");
                e.Cancel = true;
                return;
            }
            else
            {
                this.errorProvider1.SetError(this.textBoxVipLevel, string.Empty);
            }
            if (string.IsNullOrEmpty(this.textBoxVclVcl1.Text))
            {
                this.errorProvider1.SetError(this.textBoxVclVcl1, "A Valid value must be provided");
                e.Cancel = true;
                return;
            }
            else
            {
                this.errorProvider1.SetError(this.textBoxVclVcl1, string.Empty);
            }
            if (this.textBoxVcl2.Enabled && string.IsNullOrEmpty(this.textBoxVcl2.Text))
            {
                this.errorProvider1.SetError(this.textBoxVcl2, "A Valid value must be provided");
                e.Cancel = true;
                return;
            }
            else
            {
                this.errorProvider1.SetError(this.textBoxVcl2, string.Empty);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.closingByOK = true;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.closingByOK = false;
        }
        # endregion
    }
}