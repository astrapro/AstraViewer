using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using HeadsUtils.Interfaces;
namespace HEADSNeed.ASTRA.ASTRAForms
{
    public partial class frm_ModelEdit : Form
    {
        IHeadsApplication iHDapp = null;
        string temp_file = "";
        public frm_ModelEdit(IHeadsApplication this_app)
        {
            InitializeComponent();
            iHDapp = this_app;
        }

        private void btn_Proceed_Click(object sender, EventArgs e)
        {
            try
            {
                temp_file = Path.Combine(iHDapp.AppDataPath, "inp.tmp");
                File.WriteAllLines(temp_file, txt_HEADS_Data.Lines);
                SetEnvironmentVar(temp_file);
                string exe_file = Path.Combine(Application.StartupPath, "modeledit_pro.exe");

                if (HeadsUtils.Constants.BuildType == HeadsUtils.eHEADS_RELEASE_TYPE.DEMO)
                    exe_file = Path.Combine(Application.StartupPath, "modeledit_demo.exe");
                
                if (!File.Exists(exe_file))
                {
                    exe_file = Path.Combine(Application.StartupPath, "modeledit.exe");
                }
                if (File.Exists(exe_file))
                {

                    System.Diagnostics.Process prs = new System.Diagnostics.Process();

                    prs.StartInfo.FileName = exe_file;

                    if (prs.Start())
                        prs.WaitForExit();

                    temp_file = Path.Combine(iHDapp.AppDataPath, "MODEL.REP");

                    if (File.Exists(temp_file))
                    {
                        temp_file = Path.Combine(iHDapp.AppDataPath, "MODEL.txt");
                        File.Copy(Path.Combine(iHDapp.AppDataPath, "MODEL.REP"), temp_file, true);
                        System.Diagnostics.Process.Start(temp_file);
                    }
                }
            }
            catch (Exception ex) { }
            this.Close();

        }
        void SetEnvironmentVar(string env_file_name)
        {
            try
            {
                File.WriteAllText(Path.Combine(Application.StartupPath, "hds.001"), env_file_name);
                File.WriteAllText(Path.Combine(Application.StartupPath, "hds.002"), iHDapp.AppDataPath + "\\");
                FileInfo finfo = new FileInfo(temp_file);
                if (finfo.IsReadOnly)
                    finfo.IsReadOnly = false;
            }
            catch (Exception exx)
            {
            }
            System.Environment.SetEnvironmentVariable("DEM0", env_file_name);
            System.Environment.SetEnvironmentVariable("SURVEY", env_file_name);
            System.Environment.SetEnvironmentVariable("HDSPATH", Application.StartupPath + "\\");
        }

    }
}
