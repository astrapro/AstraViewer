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
    public partial class frmNodeGridBox : Form
    {
        IASTRACAD iACad = null;
        string kStr = "";
        public frmNodeGridBox(IASTRACAD iACAD)
        {
            InitializeComponent();
            this.iACad = iACAD;
        }
        void SetGrid()
        {
            kStr = iACad.GetSelectedJointsInText();

            if (kStr == "") kStr = "ALL";
            MyStrings mList = null;

            if (kStr == "ALL")
            {
                for (int i = 0; i < iACad.AstraDocument.Joints.Count; i++)
                {
                    dgvNodeGrid.Rows.Add(iACad.AstraDocument.Joints[i].NodeNo,
                        iACad.AstraDocument.Joints[i].Point.x,
                        iACad.AstraDocument.Joints[i].Point.y,
                        iACad.AstraDocument.Joints[i].Point.z, "");
                }
            }
            else if (kStr.Contains("TO"))
            {
                mList = new MyStrings(MyStrings.RemoveAllSpaces(kStr), ' ');
                int indx = -1;
                for (int i = mList.GetInt(0); i <= mList.GetInt(2); i++)
                {
                    indx = iACad.AstraDocument.Joints.IndexOf(i);

                    dgvNodeGrid.Rows.Add(iACad.AstraDocument.Joints[indx].NodeNo,
                        iACad.AstraDocument.Joints[indx].Point.x,
                        iACad.AstraDocument.Joints[indx].Point.y,
                        iACad.AstraDocument.Joints[indx].Point.z, "");
                }
            }
            else
            {
                //kStr = 
                mList = new MyStrings(MyStrings.RemoveAllSpaces(kStr), ',');
                int indx = -1;
                for (int i = 0; i < mList.Count; i++)
                {
                    indx = iACad.AstraDocument.Joints.IndexOf(mList.GetInt(i));
                    dgvNodeGrid.Rows.Add(iACad.AstraDocument.Joints[indx].NodeNo,
                        iACad.AstraDocument.Joints[indx].Point.x,
                        iACad.AstraDocument.Joints[indx].Point.y,
                        iACad.AstraDocument.Joints[indx].Point.z, "");
                }
            }
        }

        private void btn_add_data_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmNodeGridBox_Load(object sender, EventArgs e)
        {
            SetGrid();
        }


    }
}
