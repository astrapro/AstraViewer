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
    public partial class frmOpenHydrologyDrawing : Form
    {
        public enum eHydroDrawing
        {
            Cancel = 0,
            SampleDrawing = 1,
            ProjectDrawing = 2,
        }
        public eHydroDrawing DrawingType { get; set; }
        public frmOpenHydrologyDrawing()
        {
            InitializeComponent();
            DrawingType = eHydroDrawing.Cancel;
        }
        private void btn_sample_drawing_data_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;

            if (b.Name == btn_cancel.Name)
                DrawingType = eHydroDrawing.Cancel;
         
            else if (b.Name == btn_project_drawing.Name)
                DrawingType = eHydroDrawing.ProjectDrawing;

            else if (b.Name == btn_sample_drawing_data.Name)
                DrawingType = eHydroDrawing.SampleDrawing;

            this.Close();
        }

        public static eHydroDrawing Get_DrawingType()
        {
            frmOpenHydrologyDrawing frm = new frmOpenHydrologyDrawing();
            frm.ShowDialog();
            return frm.DrawingType;
        }
    }
}
