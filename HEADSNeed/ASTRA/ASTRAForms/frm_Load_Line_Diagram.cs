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
    public partial class frm_Load_Line_Diagram : Form
    {
        string Working_Folder { get; set; }
        public frm_Load_Line_Diagram(string working_folder)
        {
            InitializeComponent();
            Working_Folder = working_folder;
        }

        private void btn_Yes_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Name == btn_Yes.Name)
            {
                //    string src_path = Path.Combine(Application.StartupPath, "Example Line Diagram Model");
                //    src_path = Path.Combine(src_path, "Structure_drawing.dwg");
                //    string des_path = Path.Combine(Working_Folder, "Structure_drawing.dwg");
                //    if (File.Exists(src_path))
                //    {
                //        File.Copy(src_path, des_path, true);
                //    }
                DialogResult = DialogResult.Yes;
            }
            else if (btn.Name == btn_No.Name)
            {
                DialogResult = DialogResult.No;
            }
            this.Close();
        }
    }
}
