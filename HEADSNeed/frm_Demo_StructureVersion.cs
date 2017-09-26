using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using HEADSNeed.ASTRA;

namespace HEADSNeed
{
    public partial class frm_Demo_StructureVersion : Form
    {
        public frm_Demo_StructureVersion()
        {
            InitializeComponent();
        }


        private void rtb_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_send_mail_Click(object sender, EventArgs e)
        {
            string flname = Path.Combine(Application.StartupPath, "sendmail.exe");
            if (File.Exists(flname))
                System.Diagnostics.Process.Start(flname);

            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (HASP_Lock.IsProfessional_StructuralVersion())
                this.Close();
        }
        public static bool DemoDialog()
        {
            frm_Demo_StructureVersion f = new frm_Demo_StructureVersion();
            return (f.ShowDialog() == DialogResult.OK);
        }
    }
}
