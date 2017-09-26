using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdFigures;

using HEADSNeed.ASTRA.ASTRAClasses;

namespace HEADSNeed.ASTRA.ASTRAForms
{
    public partial class frmTextSize : Form
    {
        vdDocument doc = null;
        int selIndex = -1;
        public frmTextSize(vdDocument document,int selectIndex,ASTRADoc astDoc)
        {
            InitializeComponent();
            doc = document;
            selIndex = selectIndex;


            chkElements.Enabled = (astDoc.Elements.Count > 0);
            chkSupportFixed.Enabled = false;
            for (int i = 0; i < astDoc.Supports.Count; i++)
            {
                if (astDoc.Supports[i].Option == Support.SupportOption.FIXED)
                {
                    chkSupportFixed.Enabled = true;
                    break;
                }
            }
            chkSupportPinned.Enabled = false;
            for (int i = 0; i < astDoc.Supports.Count; i++)
            {
                if (astDoc.Supports[i].Option == Support.SupportOption.PINNED)
                {
                    chkSupportPinned.Enabled = true;
                    break;
                }
            }

            chkJointLoad.Enabled = (astDoc.JointLoads.Count > 0);
        }
        public void SetTextSize(double txtSize)
        {
            for (int i = 0; i < this.doc.ActiveLayOut.Entities.Count; i++)
            {
                vdText txt = this.doc.ActiveLayOut.Entities[i] as vdText;
                if (txt != null)
                {
                    txt.Height = txtSize/10.0d;
                    txt.Update();
                }
            }
            this.doc.Redraw(true);
        }

        private void frmTextSize_Load(object sender, EventArgs e)
        {
            cmbSize.SelectedIndex = (selIndex >= cmbSize.Items.Count ? 0 : selIndex);
        }

        private void cmbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                double d = double.Parse(cmbSize.Text);
                SetTextSize(d*0.50);
            }
            catch (Exception ex)
            { }

        }

        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.Location.ToString());
            MessageBox.Show(this.Size.ToString());
        }

        private void chkMember_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                doc.Layers.FindName("Nodes").Frozen = !chkJoint.Checked;
                doc.Layers.FindName("Members").Frozen = !chkMember.Checked;
                doc.Redraw(true);

            }
            catch (Exception exx)
            {
            }
        }

        private void chkSupportPinned_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = sender as CheckBox;

                doc.Layers.FindName(chk.Name.Substring(3)).Frozen = !chk.Checked;
                doc.Redraw(true);
            }
            catch (Exception ex)
            {
            }
        
        }

        private void chkJointLoad_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            doc.ShowUCSAxis = checkBox1.Checked;
            doc.Redraw(true);
        }
        
    }
}
