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
    public partial class frmMemberGridBox : Form
    {
        IASTRACAD iACad = null;
        string kStr = "";
        public frmMemberGridBox(IASTRACAD iACAD)
        {
            InitializeComponent();
            this.iACad = iACAD;
        }
        void SetGrid()
        {
            kStr = iACad.GetSelectedMembersInText();

            if (kStr == "") kStr = "ALL";
            MyStrings mList = null;

            if (kStr == "ALL")
            {
                for (int i = 0; i < iACad.AstraDocument.Members.Count; i++)
                {
                    dgvMemberGrid.Rows.Add(iACad.AstraDocument.Members[i].MemberNo,
                        "BEAM",
                        iACad.AstraDocument.Members[i].StartNode.NodeNo,
                        iACad.AstraDocument.Members[i].EndNode.NodeNo,
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "");
                }
               
            }
            else if (kStr.Contains("TO"))
            {
                mList = new MyStrings(MyStrings.RemoveAllSpaces(kStr), ' ');
                int indx = -1;

                for (int i = mList.GetInt(0); i <= mList.GetInt(2); i++)
                {
                    indx = iACad.AstraDocument.Members.IndexOf(i);
                    dgvMemberGrid.Rows.Add(iACad.AstraDocument.Members[indx].MemberNo,
                        "BEAM",
                        iACad.AstraDocument.Members[indx].StartNode.NodeNo,
                        iACad.AstraDocument.Members[indx].EndNode.NodeNo,
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "");
                }
            }
            else
            {
                kStr = kStr.Replace(',', ' ');

                mList = new MyStrings(MyStrings.RemoveAllSpaces(kStr), ' ');
                int indx = -1;

                for (int i = 0; i < mList.Count; i++)
                {
                    indx = iACad.AstraDocument.Members.IndexOf(mList.GetInt(i));
                 
                    dgvMemberGrid.Rows.Add(iACad.AstraDocument.Members[indx].MemberNo,
                        "BEAM",
                        iACad.AstraDocument.Members[indx].StartNode.NodeNo,
                        iACad.AstraDocument.Members[indx].EndNode.NodeNo,
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "");
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

        private void frmMemberGridBox_Load(object sender, EventArgs e)
        {
            SetGrid();
        }
    }
}
