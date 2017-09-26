using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using HeadsUtils.Interfaces;
using HeadsUtils;


namespace HeadsFunctions1.Coordinates
{
    internal partial class FormCoordinates : Form
    {
        IHeadsApplication headsApp = null;
        public FormCoordinates(IHeadsApplication app)
        {
            InitializeComponent();

            this.headsApp = app;
        }

        private bool ValidateData()
        {
            if (this.tbTextSize_.Value <= 0)
            {
                this.errorProvider.SetError(this.tbTextSize_, Properties.Resources.ST_PROMPT_INVALID_INPUT);
                this.tbTextSize_.Focus();
                return false;
            }

            if (this.tbXInterval_.Value <= 0)
            {
                this.errorProvider.SetError(this.tbXInterval_, Properties.Resources.ST_PROMPT_INVALID_INPUT);
                this.tbXInterval_.Focus();
                return false;
            }

            if (this.tbYInterval_.Value <= 0)
            {
                this.errorProvider.SetError(this.tbYInterval_, Properties.Resources.ST_PROMPT_INVALID_INPUT);
                this.tbYInterval_.Focus();
                return false;
            }
            return true;
        }

        private void TextBox__TextChanged(object sender, EventArgs e)
        {
            this.errorProvider.Clear();
        }

        private void btnOk__Click(object sender, EventArgs e)
        {
            if (this.ValidateData())
            {
                //string PathName;
                Grid Grd = new Grid();
                //CCfgtype cfg;

                CBox box = this.headsApp.ActiveDocument.BoundingBox;
                if (box != null)
                {
                    //Delete Existing
                    ViewerUtils.DeleteEntitiesByLabel(this.headsApp.ActiveDocument, HeadsUtils.Constants.LABEL_GRID, false);

                    double[] xy = new double[] { box.BottomRight.X, box.BottomRight.Y, box.TopLeft.X, box.TopLeft.Y };

                    Grd.ModelName = "$$$$$$$$";
                    Grd.StringName = "****";
                    Grd.MinX = xy[0];
                    Grd.MinY = xy[1];
                    Grd.MaxX = xy[2];
                    Grd.MaxY = xy[3];

                    Grd.XInterval = this.tbXInterval_.Value;
                    Grd.YInterval = this.tbYInterval_.Value;
                    Grd.Rotation = 0; 
                    Grd.TextSize = this.tbTextSize_.Value;

                    string FileName = Path.Combine(this.headsApp.AppDataPath, "GRID1.TMP");

                    BinaryWriter bw = new BinaryWriter(new FileStream(FileName, FileMode.Create), Encoding.Default);
                    Grd.ToStream(bw);
                    bw.Close();

                    FileName = Path.Combine(this.headsApp.AppDataPath, "GRID2.TMP");

                    ViewerUtils.DeleteFileIfExists(FileName);

                    CCoordinatesUtil util = new CCoordinatesUtil();
                    util.Funcmain(this.headsApp.AppDataPath);

                    DrawingUtil.DrawChainage(this.headsApp, FileName, HeadsUtils.Constants.LABEL_GRID, true);

                    this.headsApp.ActiveDocument.RefreshDocument();
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }            
        }

        private void btnCancel__Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}