using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using HEADSNeed.ASTRA.ASTRAClasses;

namespace HEADSNeed.ASTRA.ASTRAForms
{
    public partial class frm_ContourModeling : Form
   {
        string str = "";

        List<string> HEADS_Data = new List<string>();

        public frm_ContourModeling(string working_path)
        {
            InitializeComponent();
            WorkingFolder = working_path;
        }
        string WorkingFolder { get; set; }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                HEADS_Data.Clear();
                //HEADS
                HEADS_Data.Add("HEADS");
                //100,DGM
                HEADS_Data.Add("100,DGM");
                //104,CON,MODEL=CONTOUR,STRING=C001,INC=1.0
                str = string.Format("104,CON,MODEL={0},STRING={1},INC={2}",
                    txt_pri_model.Text,
                    txt_pri_string.Text,
                    txt_pri_inc.Text);
                HEADS_Data.Add(str);
                //104,CON,MODEL=CONTOUR,STRING=C005,INC=5.0
                str = string.Format("104,CON,MODEL={0},STRING={1},INC={2}",
                    txt_sec_model.Text,
                    txt_sec_string.Text,
                    txt_sec_inc.Text);
                HEADS_Data.Add(str);
                //105,TXT,MODEL=CONTOUR,STRING=ELE2,INC=5.0,TSI=5
                str = string.Format("105,TXT,MODEL={0},STRING={1},INC={2}",
                    txt_ele_model.Text,
                    txt_ele_string.Text,
                    txt_ele_inc.Text);
                HEADS_Data.Add(str);
                //FINISH
                str = "FINISH";
                HEADS_Data.Add(str);

                str = Path.Combine(WorkingFolder, "inp.tmp");
                File.WriteAllLines(str, HEADS_Data.ToArray());
                try
                {
                    File.WriteAllText(Path.Combine(Application.StartupPath, "hds.001"), str);
                    File.WriteAllText(Path.Combine(Application.StartupPath, "hds.002"), WorkingFolder + "\\");
                    FileInfo finfo = new FileInfo(str);
                    if (finfo.IsReadOnly)
                        finfo.IsReadOnly = false;
                }
                catch (Exception exx)
                {
                }
                System.Environment.SetEnvironmentVariable("DEM0", str);
                System.Environment.SetEnvironmentVariable("SURVEY", str);
                System.Environment.SetEnvironmentVariable("HDSPATH", Application.StartupPath + "\\");

                if (HeadsUtils.Constants.BuildType == HeadsUtils.eHEADS_RELEASE_TYPE.DEMO)
                    str = Path.Combine(Application.StartupPath, "dgcont_demo.exe");
                else
                    str = Path.Combine(Application.StartupPath, "dgcont_pro.exe");
                
                if (File.Exists(str))
                    System.Diagnostics.Process.Start(str);
                else
                    MessageBox.Show(str + " file not found.", "ASTRA");
            }
            catch (Exception ex) { }
            this.Close();
        }

        private void txt_pri_inc_TextChanged(object sender, EventArgs e)
        {
            txt_ele_inc.Text = (MyStrings.StringToDouble(txt_pri_inc.Text, 0.0) * 5).ToString("0.00");
            txt_sec_inc.Text = (MyStrings.StringToDouble(txt_pri_inc.Text, 0.0) * 5).ToString("0.00");
        }

        private void frmContour_Load(object sender, EventArgs e)
        {

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
