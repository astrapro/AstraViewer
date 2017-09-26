using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HEADSNeed.ASTRA.ASTRAClasses;
using HEADSNeed.ASTRA.CadToAstra;
namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    public partial class frmProjectTitle : Form
    {
        string structType, userTitle;
        ELengthUnits lenUnit;
        EMassUnits massUnit;
        IASTRACAD iACAD = null;
        public frmProjectTitle(IASTRACAD astCad)
        {
            InitializeComponent();
            this.iACAD = astCad;
        }

        private void frmProjectTitle_Load(object sender, EventArgs e)
        {
            if (iACAD.ProjectTitle != "")
            {
                MyStrings mList = new MyStrings(iACAD.ProjectTitle, ' ');

                mList.StringList.RemoveAt(0);

                switch (mList.StringList[0])
                {
                    case "SPACE":
                        lbStructureType.SelectedIndex = 0;
                        break;
                    case "FLOOR":
                        lbStructureType.SelectedIndex = 1;
                        break;
                    case "PLANE":
                        lbStructureType.SelectedIndex = 2;
                        break;
                }
                mList.StringList.RemoveAt(0);
                txtUserTitle.Text = mList.Text;
                cmbLengthUnit.SelectedIndex = (int)iACAD.LengthUnit;
                cmbMassUnit.SelectedIndex = (int)iACAD.MassUnit;
                return;
            }

            lenUnit = ELengthUnits.M;
            massUnit = EMassUnits.KN;
            cmbLengthUnit.SelectedIndex = (int)lenUnit;
            cmbMassUnit.SelectedIndex = (int)massUnit;
            lbStructureType.SelectedIndex = 0;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {

            iACAD.ProjectTitle = "ASTRA " + lbStructureType.SelectedItem.ToString() + " " + txtUserTitle.Text;
            iACAD.LengthUnit = (ELengthUnits)cmbLengthUnit.SelectedIndex;
            iACAD.MassUnit = (EMassUnits)cmbMassUnit.SelectedIndex;
            this.DialogResult = DialogResult.OK;
            this.Close();


            iACAD.Create_Data.Project_Title = iACAD.ProjectTitle;
            iACAD.Create_Data.Base_Unit = "UNIT " + iACAD.LengthUnit.ToString() + " " + iACAD.MassUnit.ToString();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void lbStructureType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAst.Text = "ASTRA " + lbStructureType.SelectedItem.ToString();
        }

    }

}
