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
using VectorDraw.Professional;
using VectorDraw.Generics;
using VectorDraw.Professional.vdCollections;

namespace HeadsViewer
{
    /// <summary>
    /// This is a simple form that implements the publish command.
    /// </summary>
    public partial class Publish : Form
    {
        private vdDocument mDoc = null;
        private string mPath = "";
        private string mVersion = "";
        private string mExtension = "";
        public Publish()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Initialization of the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Publish_Load(object sender, EventArgs e)
        {
            comboVersion.Items.Clear();
            comboVersion.Items.AddRange(new string[] { "VDML", "VDCL", "DXF2004", "DXF2000", "DWG2007", "DWG2004", "DWG2000", "DGN8" });
            comboVersion.SelectedIndex = 0;
        }
        public void Initialize(vdDocument doc)
        {
            mDoc = doc;
            butBrowse.Text = "Browse";
        }
        /// <summary>
        /// Closes the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Opens a dialog in order for the user to select a directory where the output files are going to be saved.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butBrowse_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderbrowser = new FolderBrowserDialog();
            folderbrowser.RootFolder = Environment.SpecialFolder.MyComputer;
            DialogResult res = folderbrowser.ShowDialog();
            if (res == DialogResult.OK)
            {
                string strPath = folderbrowser.SelectedPath;
                textPath.Text = strPath;
                mPath = strPath;
            }   
        }
        /// <summary>
        /// Starts the publish command. All dependencies of the document(external references etc...) are going to be saved in the directory that the user selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butPublish_Click(object sender, EventArgs e)
        {

            if (mPath == "") { MessageBox.Show("Path not selected", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            if (mExtension == "" || mVersion == "") { MessageBox.Show("Version not selected", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            StringArray arr = new StringArray();
            mDoc.ExportDistributionFiles(ref arr, mPath, mExtension, mVersion, true);
            MessageBox.Show(arr.Count.ToString() + " files exported", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
        /// <summary>
        /// This event is used in order to inform the version and the extension parameters of the form depending the combo selection of the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((string)comboVersion.SelectedItem)
            {
                case "VDML": mVersion = "VDML"; mExtension = ".vdml"; break;
                case "VDCL": mVersion = "VDML"; mExtension = ".vdcl"; break;
                case "DXF2004": mVersion = "DXF2004"; mExtension = ".dxf"; break;
                case "DXF2000": mVersion = "DXF2000"; mExtension = ".dxf"; break;
                case "DWG2007": mVersion = "DXF2007"; mExtension = ".dwg"; break;
                case "DWG2004": mVersion = "DXF2004"; mExtension = ".dwg"; break;
                case "DWG2000": mVersion = "DXF2000"; mExtension = ".dwg"; break;
                case "DGN8": mVersion = "DGN8"; mExtension = ".dgn"; break;
            }   
        }
    }
}