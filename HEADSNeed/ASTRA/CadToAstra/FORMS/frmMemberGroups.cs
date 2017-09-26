using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using HEADSNeed.ASTRA.ASTRAClasses;
//using MovingLoadAnalysis;
using HEADSNeed.ASTRA.CadToAstra;

namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    public partial class frmMemberGroups : Form
    {
        IASTRACAD iACad = null;

        public string ASTRA_Data { get; set; }
        public MemberGroup MGroup { get; set; }
        public frmMemberGroups(IASTRACAD iACAD)
        {
            InitializeComponent();
            this.iACad = iACAD;
        }

        private void frmMemberGroups_Load(object sender, EventArgs e)
        {
            if (MGroup != null)
            {
                txt_group_name.Text = MGroup.GroupName;
                txt_mem_nos.Text = MGroup.MemberNosText;
                btn_add_data.Text = "Change";
            }
            else
                txt_mem_nos.Text = iACad.GetSelectedMembersInText();

            string kStr = "";
            kStr = txt_group_name.Text;

            if (!kStr.StartsWith("_"))
                kStr = "_" + kStr;

            ASTRA_Data = kStr + " " + txt_mem_nos.Text.Replace(',', ' ');
        }
        private void btn_add_data_Click(object sender, EventArgs e)
        {
            string kStr = "";
            kStr = txt_group_name.Text;

            if (!kStr.StartsWith("_"))
                kStr = "_" + kStr;

            ASTRA_Data = kStr + " " + txt_mem_nos.Text.Replace(',', ' ');
            //ASTRA_Data = kStr;

            MGroup = new MemberGroup();
            MGroup.GroupName = kStr;
            MGroup.MemberNosText = txt_mem_nos.Text;

            DialogResult = DialogResult.OK;
            this.Close();
            
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {

            DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
