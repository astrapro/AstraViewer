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
        enum Symmetrical
        {
            No = 0,
            Yes = 1,
        }
        # endregion

        # region constructors
        internal FormEditValign(VVIP data)
        {
            InitializeComponent();
            this.comboSymmetrical_.Items.Add(Symmetrical.Yes);
            this.comboSymmetrical_.Items.Add(Symmetrical.No);

            this.comboSymmetrical_.SelectedItem = Symmetrical.Yes;

            this.tbVipChain_.Value = data.chainx;
            this.tbVipLevel_.Value = data.chainy;
            this.comboSymmetrical_.SelectedItem = (data.sym) ? Symmetrical.Yes : Symmetrical.No;
            this.tbVcl1_.Value = data.vcl1;
            this.tbVcl2_.Value = data.vcl2;

        }
        # endregion

        # region Properties
        internal VVIP VVIPData
        {
            get
            {
                VVIP data = new VVIP();
                data.chainx = this.tbVipChain_.Value;
                data.chainy = this.tbVipLevel_.Value;
                data.sym = ((Symmetrical)this.comboSymmetrical_.SelectedItem == Symmetrical.Yes) ? true:false;
                data.vcl1 = this.tbVcl1_.Value;
                data.vcl2 = this.tbVcl2_.Value;
                return data;
            }
        }        
        # endregion

        # region event handlers
        private void comboBoxSymmetrical_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((Symmetrical)this.comboSymmetrical_.SelectedItem == Symmetrical.No)
            {
                this.tbVcl2_.Enabled = true;
            }
            else
            {
                this.tbVcl2_.Enabled = false;
                this.tbVcl2_.Value = 0;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        # endregion
    }
}