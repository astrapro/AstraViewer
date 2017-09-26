using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HEADSNeed.ASTRA.ASTRAClasses;
using HEADSNeed.ASTRA.CadToAstra;
//using MovingLoadAnalysis;

namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    public partial class frmMemberTruss : Form
    {
        IASTRACAD iACad = null;
        public string ASTRA_Data { get; set; }
        public MemberGroupCollection MGC { get; set; }
        public TreeView TRV { get; set; }

        bool IsCable { get; set; }
        public frmMemberTruss(IASTRACAD iACAD)
        {
            InitializeComponent();
            this.iACad = iACAD;
            ASTRA_Data = "";
        }
        public frmMemberTruss(IASTRACAD iACAD, bool isCable)
        {
            InitializeComponent();
            this.iACad = iACAD;
            this.IsCable = isCable;
            ASTRA_Data = "";

            if (IsCable)
                this.Text = "MEMBER CABLE";
        }

        private void frmMemberTruss_Load(object sender, EventArgs e)
        {
            if (MGC != null)
            {
                foreach (var item in MGC.GroupCollection)
                {
                    cmb_range.Items.Add(item.GroupName);
                }
            }
            
            if (ASTRA_Data != "")
            {
                ASTRA_Data = ASTRA_Data.Trim();
                if (cmb_range.Items.Contains(ASTRA_Data))
                    cmb_range.SelectedItem = ASTRA_Data;
                else
                {
                    txt_mem_nos.Text = ASTRA_Data;
                    cmb_range.SelectedIndex = 1;
                }
                btn_add_data.Text = "Change";

            }
            else
            {
                if (cmb_range.Items.Count > 0)
                {
                    cmb_range.SelectedIndex = 1;
                }
                txt_mem_nos.Text = iACad.GetSelectedMembersInText();
            }
        }

        private void btn_add_data_Click(object sender, EventArgs e)
        {
            string kStr = "";


            if (txt_mem_nos.Text == "") return;

            if (cmb_range.SelectedIndex == 0) // ALL
            {
                kStr = string.Format("1 TO {0}", iACad.AstraDocument.Members.Count);
            }
            else if (cmb_range.SelectedIndex == 1) // NUMBERS
            {
                string num_text = "";

                num_text = txt_mem_nos.Text.Replace(',', ' ');
                kStr = string.Format("{0}",
                    num_text);

            }
            else
            {
                string num_text = "";

                num_text = cmb_range.Text.Replace(',', ' ');
                kStr = string.Format("{0} ",
                    num_text);

            }
            //kStr = txt_mem_nos.Text.Replace(',', ' ');

            if (TRV != null)
            {
                if (ASTRA_Data != "")
                {
                    TRV.SelectedNode.Text = (kStr);
                    this.Close();
                }
                else
                    TRV.Nodes.Add(kStr);
            }

            //this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cmb_range_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_range.SelectedIndex == 0)
            {
                txt_mem_nos.Text = "ALL";
                txt_mem_nos.Enabled = false;
            }
            else if (cmb_range.SelectedIndex == 1)
            {
                txt_mem_nos.Enabled = true;
                txt_mem_nos.Text = "";
            }
            else
            {
                txt_mem_nos.Enabled = false;
                txt_mem_nos.Text = MGC.GetMemberGroup(cmb_range.Text).MemberNosText;
            }
        }
    }
}
