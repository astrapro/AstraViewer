using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace HEADSNeed.ASTRA.ASTRAForms
{
    public partial class frm_Triangulation : Form
    {
        

        public frm_Triangulation(string working_path)
        {
            InitializeComponent();
            WorkingFolder = working_path;
        }
        public string WorkingFolder { get; set; }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            List<string> HEADS_Data = new List<string>();
            
            frm_GroundModeling ff = new frm_GroundModeling(WorkingFolder);
            ff.Owner = this;

            bool flag = !(HeadsUtils.Constants.BuildType == HeadsUtils.eHEADS_RELEASE_TYPE.DEMO);
            if (ff.ShowDialog() == DialogResult.OK)
            {
                string exe_file = flag ? "DTM_Pro.EXE" : "DTM_Demo.EXE";
                exe_file = Path.Combine(Application.StartupPath, exe_file);

                //HEADS
                HEADS_Data.Add("HEADS");
                //100,DGM
                HEADS_Data.Add("100,DGM");
                //101,BOU,MODEL=BOUND,STRING=BDRY
                HEADS_Data.Add("101,BOU,MODEL=" + txt_model.Text + ",STRING=" + txt_string.Text);
                //FINISH
                HEADS_Data.Add("FINISH");

                string Temp_File = Path.Combine(WorkingFolder, "inp.tmp");

                File.WriteAllLines(Temp_File, HEADS_Data.ToArray());
                try
                {
                    File.WriteAllText(Path.Combine(Application.StartupPath, "hds.001"), Temp_File);
                    File.WriteAllText(Path.Combine(Application.StartupPath, "hds.002"), WorkingFolder + "\\");
                    FileInfo finfo = new FileInfo(Temp_File);
                    if (finfo.IsReadOnly)
                        finfo.IsReadOnly = false;
                }
                catch (Exception exx)
                {
                }
                System.Environment.SetEnvironmentVariable("DEM0", Temp_File);
                System.Environment.SetEnvironmentVariable("SURVEY", Temp_File);
                System.Environment.SetEnvironmentVariable("HDSPATH", Application.StartupPath + "\\");

                if (File.Exists(exe_file))
                    System.Diagnostics.Process.Start(exe_file);
                else
                {
                    MessageBox.Show(exe_file + " file not found.", "ASTRA");
                }
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
