using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HEADSNeed.ASTRA.ASTRAClasses;

namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    public partial class frmLoadCase : Form
    {

        List<int> list_joint_No = null;
        IASTRACAD iACad = null;
        public string ASTRA_Data { get; set; }

        public LoadCaseDefinition Ld { get; set; }

        public frmLoadCase(IASTRACAD iACAD)
        {
            InitializeComponent();
            list_joint_No = new List<int>();
            this.iACad = iACAD;
            ASTRA_Data = "";

        }

        private void btn_jload_add_Click(object sender, EventArgs e)
        {

            string kStr = "";
            //kStr = "LOAD " + txt_load_case.Text + " : " + txt_load_title.Text.Replace(',', ' ');
            kStr = txt_load_case.Text + " : " + txt_load_title.Text.Replace(',', ' ');

            ASTRA_Data = kStr;




            Ld = new LoadCaseDefinition();
            Ld.LoadNo = MyStrings.StringToInt(txt_load_case.Text, 0);
            Ld.Title = txt_load_title.Text;



            this.Close();
        }

        private void frmLoadCase_Load(object sender, EventArgs e)
        {
            if (ASTRA_Data != "")
            {
                MyStrings mlist = new MyStrings(ASTRA_Data, ':');

                if (mlist.StringList.Count > 1)
                {
                    txt_load_case.Text = mlist.StringList[0].Trim();
                    txt_load_title.Text = mlist.StringList[1].Trim();
                }
                btn_jload_add.Text = "Change";
            }
            else
            {
                txt_load_title.Text = "LOAD " + txt_load_case.Text;
            }

        }

    }
}
