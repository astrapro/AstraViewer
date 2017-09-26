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
    public partial class frm_Open_Project : Form
    {
        public string Project_Type { get; set; }

        public string Wokring_Folder
        {
            get
            {
                return txt_working_folder.Text;
            }
            set
            {
                txt_working_folder.Text = value;
            }
        }
        public frm_Open_Project(string ProjectType, string WorkingFolder)
        {
            InitializeComponent();
            Wokring_Folder = WorkingFolder;
            Project_Type = ProjectType;
        }

        private void frm_Open_Project_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(Wokring_Folder))
            {
                string kStr = "";
                foreach (var item in Directory.GetDirectories(Wokring_Folder))
                {
                    kStr = Path.Combine(item, Project_Type + ".apr");
                    if (File.Exists(kStr))
                        lst_proj_folders.Items.Add(Path.GetFileName(item));
                }
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            //iproj.Project_Folder = lst_proj_folders.SelectedItem.ToString();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        public  string Example_Path
        {
            get
            {
                try
                {
                    return Path.Combine(Wokring_Folder, lst_proj_folders.SelectedItem.ToString());
                }
                catch (Exception exx) { }

                return "";
            }
        }


        private void lst_proj_folders_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

    }
}
