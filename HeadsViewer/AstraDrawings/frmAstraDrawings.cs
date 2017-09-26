using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace HeadsViewer.AstraDrawings
{
    public partial class frmAstraDrawings : Form
    {
        MainForm m_form = null;
        Hashtable hashForm = null;
        IOpenDrawings irp = null;
        public frmAstraDrawings(IOpenDrawings irp)
        {
            InitializeComponent();
            this.irp = irp;
        }
        
        public Hashtable HashForm
        {
            get
            {
                return hashForm;
            }
        }

        private void frmAstraDrawings_Load(object sender, EventArgs e)
        {
            Load_All_Drawings();
        }

        private void Load_All_Drawings()
        {

            irp.OpenDesignDrawings(ref lst_drawings);
            if (lst_drawings.Items.Count > 0)
            {
                pb.Visible = false;
                lst_drawings.SelectedIndex = 0;
                Text = "Multiple Drawings (" + (lst_drawings.Items.Count) + " files)";
            }
            if (lst_drawings.Items.Count == 1 || lst_drawings.Items.Count == 0)
                this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(btn_conv_dwg.Visible)
            if (lst_drawings.SelectedIndex != -1)
                irp.ChildActivate(lst_drawings.SelectedIndex);
        }

        private void btn_conv_dwg_Click(object sender, EventArgs e)
        {

            if (HeadsUtils.Constants.BuildType == HeadsUtils.eHEADS_RELEASE_TYPE.DEMO)
            {
                MessageBox.Show(this, "This feature is not available in Unauthorize Version of ASTRA Pro", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return ;
            }

            //string ext = rbtn_dxf.Checked ? "DXF" : "DWG";
            string ext = "DXF";
            int count = 0;
            pb.Visible = true;
            count = irp.Convert_All_Drawings(ext, pb, ref lst_drawings);
            pb.Visible = false;

            
            if (count > 0)
            {
                irp.CloseAllDrawings();
                this.Opacity = 0.0;
                if (MessageBox.Show(this, "Do you want to Open all " + ext + " Drawings ?", "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Load_All_Drawings();
                    this.Opacity = 1.0;
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void pb_VisibleChanged(object sender, EventArgs e)
        {
            btn_conv_dwg.Visible = !pb.Visible;
        }
    }
    public interface IOpenDrawings
    {
        MainForm MDIParentForm { get; }
        void ChildActivate(int mdiChildIndex);
        void OpenDesignDrawings(ref ListBox lstbox);
        string DrawingCode { get; }
        void CloseAllDrawings();
        int Convert_All_Drawings(string ext, ProgressBar pb, ref ListBox lst_drawings);
    }
}

