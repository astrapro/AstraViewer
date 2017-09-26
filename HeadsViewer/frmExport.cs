//
// (C) Copyright 1998-2007 by VectorDraw, Ltd. 
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted, 
// provided that the above copyright notice appears in all copies and 
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting 
// documentation.
//
// VectorDraw PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS. 
// VectorDraw SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  VectorDraw, Ltd. 
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Geometry;

namespace HeadsViewer
{
    /// <summary>
    /// This form is used in order to export the drawing into raster formats(jpg,bmp etc...).
    /// </summary>
    public partial class frmExport : Form
    {
        #region Properties
        private vdDocument mDoc = null;
        private Box mRenderBox = new Box();
        private Image mImg = null;
        #endregion

        #region Initialize
        public frmExport()
        {
            InitializeComponent();
        }
        /// <summary>
        /// This method is used in order to show the dialog.Note thaty using a while the dialog is continuasly loading until it exits
        /// with OK or Cancel. Using the Ignore value of the form's result we are able to hide the form and ask the user to select
        /// a box that will be used as window render for the raster export.
        /// </summary>
        /// <param name="doc"></param>
        public void Show(vdDocument doc)
        {
            mDoc = doc;
            mRenderBox.Empty();
            mRenderBox.AddBox (mDoc.ActiveLayOut.Entities.GetBoundingBox(false, true));
            mRenderBox.TransformBy(mDoc.World2ViewMatrix);
            
            if (checkAspect.Checked) CalculateAspect(false);

            DialogResult res = DialogResult.OK;
            do
            {
                this.ShowDialog(mDoc.ActionControl);
                Application.DoEvents();
                res = this.DialogResult;
                switch (res)
                {
                    case DialogResult.Ignore:
                        {
                            Box UserBox = null;
                            mDoc.ActionUtility.getUserRectViewCS(null, out UserBox);
                            if (UserBox != null)
                            {
                                mRenderBox.Empty();
                                mRenderBox.AddBox(UserBox);
                            }
                            if (checkAspect.Checked) CalculateAspect(false);
                        } break;
                }

            } while (res != DialogResult.OK && res != DialogResult.Cancel);
        }
        /// <summary>
        /// Calculates the Height or the width(depending the parameter) keeping the aspect of the drawing.
        /// </summary>
        /// <param name="calculateHeight">A boolean value representing if the Height will be calculated or the width.</param>
        private void CalculateAspect(bool calculateHeight)
        {
            double l1 = 1.0d;
            if(!mRenderBox.IsEmpty) 
                l1 = mRenderBox.Width / mRenderBox.Height;
            if (calculateHeight)
            {
                int width = 200;
                try
                {
                    width = int.Parse(textWidth.Text);
                }
                catch { width = 200; }
                double height = (double)width / l1;
                textHeight.Text = ((int)height).ToString();
            }
            else
            {
                int height = 200;
                try
                {
                    height = int.Parse(textHeight.Text);
                }
                catch { height = 200; }
                double width = (double)height * l1;
                textWidth.Text = ((int)width).ToString();
            }
        }
        /// <summary>
        /// Fills the combo with all the supported raster formats.
        /// </summary>
        private void FillCombo()
        {
            comboExport.Items.Clear();
            comboExport.Items.Add("BMP");
            comboExport.Items.Add("GIF");
            comboExport.Items.Add("JPG");
            comboExport.Items.Add("TIF");
            comboExport.Items.Add("ICO");
            comboExport.Items.Add("PNG");
            comboExport.Items.Add("EMF");
            comboExport.Items.Add("WMF");
        }
        /// <summary>
        /// Initializes the dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmExport_Load(object sender, EventArgs e)
        {
            if (comboExport.Text != "") UpdatePreview();
            FillCombo();
        }
        
        #endregion

        #region Buttons & COMBO
        /// <summary>
        /// This method is used to return the filter that is going to be used at the save format depending the selection that the user has made at the combo.
        /// </summary>
        /// <returns></returns>
        private string GetSaveFilter()
        {
            string ret = comboExport.Text.ToUpper() + " Files (*." + comboExport.Text.ToLower() + ")|*." + comboExport.Text.ToLower();
            return ret;
        }
        /// <summary>
        /// Saves the image using the user's selections.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butExport_Click(object sender, EventArgs e)
        {
            //EXPORT.
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = GetSaveFilter();
            
            saveFileDialog1.FileName = VectorDraw.Professional.Utilities.vdGlobals.GetFileNameWithoutExtension(mDoc.FileName);
            saveFileDialog1.InitialDirectory = VectorDraw.Professional.Utilities.vdGlobals.GetDirectoryName(mDoc.FileName);
            
            saveFileDialog1.RestoreDirectory = true;
            DialogResult res = saveFileDialog1.ShowDialog(this);
            Application.DoEvents();
            if (res == DialogResult.OK)
            {
                string filename = saveFileDialog1.FileName;

                if ((filename != "") && (filename != null))
                {
                    if (filename.EndsWith(".emf", StringComparison.CurrentCultureIgnoreCase) || filename.EndsWith(".wmf", StringComparison.CurrentCultureIgnoreCase))
                    {

                        IntPtr referencedc = VectorDraw.Render.GDIDraw.GDI.IntCreateDC("DISPLAY", "", "", IntPtr.Zero);
                        System.Drawing.Imaging.Metafile emf = new System.Drawing.Imaging.Metafile(filename, referencedc, System.Drawing.Imaging.EmfType.EmfOnly, "FromVectorDraw6");
                        Color bkcolor = mDoc.ActiveRender.BkColor;
                        System.Drawing.Size renderingSize = new System.Drawing.Size(mImg.Width, mImg.Height);
                        System.Drawing.Graphics gr = Graphics.FromImage(emf);
                        gr.FillRectangle(new SolidBrush(bkcolor), new Rectangle(new Point(0, 0), renderingSize));
                        mDoc.ActiveLayOut.RenderToGraphics(gr, mRenderBox, mImg.Width, mImg.Height);
                        gr.Dispose();
                        emf.Dispose();
                        VectorDraw.Render.GDIDraw.GDI.DeleteDC(referencedc);
                    }
                    else
                    {
                        mImg.Save(filename);
                    }
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }
        /// <summary>
        /// Updates the dialog when the user selects the raster format at the comb.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboExport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboExport.Text != "")
            {
                panel1.Enabled = true;
                butExport.Enabled = true;
                UpdatePreview();
            }
        }
        /// <summary>
        /// Calculates the extends of the drawing as render window for the export.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butAll_Click(object sender, EventArgs e)
        {
            mRenderBox.Empty();
            mRenderBox.AddBox (mDoc.ActiveLayOut.Entities.GetBoundingBox(false, false));
            mRenderBox.TransformBy(mDoc.World2ViewMatrix);
            UpdatePreview();
        }
        /// <summary>
        /// Closes the dialog returning Cancel value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        /// <summary>
        /// Closes the dialog returning Ignore value so the user can select the bounding window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butSelect_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
            this.Close();
        }
        /// <summary>
        /// When the user presses ENTER at the width textbox then the aspect is calculated for the preview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (checkAspect.Checked) CalculateAspect(true);
                UpdatePreview();
            }
        }
        /// <summary>
        /// When the user presses ENTER at the height textbox then the aspect is calculated for the preview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (checkAspect.Checked) CalculateAspect(false);
                UpdatePreview();
            }
        }
        /// <summary>
        /// This event is used when the user changes the aspect checkbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkAspect_CheckedChanged(object sender, EventArgs e)
        {
            if (checkAspect.Checked)
            {
                CalculateAspect(true);
            }
            UpdatePreview();
        }

        #endregion
        /// <summary>
        /// Updates the preview window depending the user's values.
        /// </summary>
        private void UpdatePreview()
        {
            int width = 200;
            int height = 200;
            try
            {
                width = int.Parse(textWidth.Text);
            }
            catch { width = 200; }
            try
            {
                height = int.Parse(textHeight.Text);
            }
            catch { height = 200; }

            mImg = new Bitmap(width, height);
            System.Drawing.Graphics graph = Graphics.FromImage(mImg);

            mDoc.ActiveLayOut.RenderToGraphics(graph, mRenderBox, width, height);

            int previewWidth = pictureBox1.Width;
            int previewHeight = pictureBox1.Height;
            Padding pad = new Padding(0, 0, 0, 0);
            if ((width < previewWidth) && (height < previewHeight))
            {
                pictureBox1.Image = mImg;
                int left = (int)((previewWidth - width) / 2.0);
                int top = (int)((previewHeight - height) / 2.0);
                pad = new Padding(left, top, left, top);
            }
            else
            {
                Image Preview = new Bitmap(mImg);
                double l1 = (double)width / (double)height;
                if ((width > previewWidth) && (height > previewHeight))
                {
                    if (l1 > 1.0)
                    {
                        int prev = previewHeight;
                        previewHeight = (int)(previewWidth / l1);
                        int left = (int)((prev - previewHeight) / 2.0);
                        pad = new Padding(0, left, 0, left);
                    }
                    else
                    {
                        int prev = previewWidth;
                        previewWidth = (int)(l1 * previewHeight);
                        int top = (int)((prev - previewWidth) / 2.0);
                        pad = new Padding(top, 0, top, 0);
                    }
                        
                }
                else
                {
                    if (width > previewWidth)
                    {
                        int prev = previewHeight;
                        previewHeight = (int)(previewWidth / l1);
                        int left = (int)((prev - previewHeight) / 2.0);
                        pad = new Padding(0, left, 0, left);
                    }
                    else
                    {
                        int prev = previewWidth;
                        previewWidth = (int)(l1 * previewHeight);
                        int top = (int)((prev - previewWidth) / 2.0);
                        pad = new Padding(top, 0, top, 0);
                    }
                }
                
                Preview = Preview.GetThumbnailImage(previewWidth, previewHeight, null, System.IntPtr.Zero);

                pictureBox1.Image = Preview;
            }

            pictureBox1.BackColor = Color.Gray;
            pictureBox1.Padding = pad;                
            pictureBox1.Update();
            graph.Dispose();
        }
    }
}