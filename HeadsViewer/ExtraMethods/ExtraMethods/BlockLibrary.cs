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
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Geometry;
using VectorDraw.Actions;
using VectorDraw.Professional.vdCollections;

namespace ExtraMethods
{
    /// <summary>
    /// This form will show a block library. The block library can be saved in a specified format named .vlib
    /// and can be loaded again to obtain blocks and add them to the current Document.
    /// </summary>
    public partial class BlockLibrary : Form
    {
        #region Properties
        private vdDocument mdoc = null;
        private static string Libraryname = "";
        #endregion

        #region INITIALIZE
        /// <summary>
        /// Initialization of the form.
        /// </summary>
        public BlockLibrary()
        {
            InitializeComponent();
            this.vectorDrawBaseControl1.GetOpenFileFilterFormat += new VectorDraw.Professional.Control.GetOpenFileFilterFormatEventHandler(vectorDrawBaseControl1_GetOpenFileFilterFormat);
            this.vectorDrawBaseControl1.GetSaveFileFilterFormat += new VectorDraw.Professional.Control.GetSaveFileFilterFormatEventHandler(vectorDrawBaseControl1_GetSaveFileFilterFormat);
            this.vectorDrawBaseControl1.ActionStart += new VectorDraw.Professional.Control.ActionStartEventHandler(vectorDrawBaseControl1_ActionStart);
            this.vectorDrawBaseControl1.vdMouseEnter += new VectorDraw.Professional.Control.MouseEnterEventHandler(vectorDrawBaseControl1_vdMouseEnter);
            this.vectorDrawBaseControl1.IsValidOpenFormat += new VectorDraw.Professional.Control.IsValidOpenFormatEventHandler(vectorDrawBaseControl1_IsValidOpenFormat);
            this.vectorDrawBaseControl1.SaveUnknownFileName += new VectorDraw.Professional.Control.SaveUnknownFileNameEventHandler(vectorDrawBaseControl1_SaveUnknownFileName);
            this.vectorDrawBaseControl1.LoadUnknownFileName += new VectorDraw.Professional.Control.LoadUnknownFileNameEventHandler(vectorDrawBaseControl1_LoadUnknownFileName);
        }
        /// <summary>
        /// This event is used so the vlib files(which are saved in VDML format but with different extensions) can be handled and opened by VDF.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="fileName"></param>
        /// <param name="success"></param>
        void vectorDrawBaseControl1_LoadUnknownFileName(object sender, string fileName, out bool success)
        {
            success = false;
            vdDocument doc = sender as vdDocument;
            if (fileName.EndsWith(".vlib", StringComparison.CurrentCultureIgnoreCase))
            {
                //This is the way to open an already saved as VDML file with a different extension.
                System.IO.Stream stream;
                try
                {
                    stream = System.IO.File.Open(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                doc.LoadFromMemory(stream);
                stream.Close();
                success = true;
            }
        }
        /// <summary>
        /// This event is used so the vlib files(which are saved in VDML format but with different extension) can be handled and saved by VDF.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="fileName"></param>
        /// <param name="success"></param>
        void vectorDrawBaseControl1_SaveUnknownFileName(object sender, string fileName, out bool success)
        {
            success = false;
            vdDocument doc = sender as vdDocument;
            if (fileName.EndsWith(".vlib", StringComparison.CurrentCultureIgnoreCase))
            {
                //This is the way to save a file as VDML with a different extension (vlib).
                System.IO.FileStream fl;
                try
                {
                   fl = System.IO.File.Open(fileName, System.IO.FileMode.Create);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                System.IO.MemoryStream VDFStreem;
                VDFStreem = doc.ToStream();
                
                byte[] bytearray = VDFStreem.ToArray();
                fl.Write(bytearray, 0, bytearray.Length);
                fl.Close();
                VDFStreem.Close();
                success = true;
            }
        }
        /// <summary>
        /// This event is used so the Save dialog displays only vlib files in order to save the library.
        /// </summary>
        /// <param name="saveFilter"></param>
        void vectorDrawBaseControl1_GetSaveFileFilterFormat(ref string saveFilter)
        {
            saveFilter = "VectorDraw Library Files (*.vlib)|*.vlib||?VLIB";
        }
        /// <summary>
        /// This event is used so the .vlib files are valid for VDF while open and save commands are executed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="extension"></param>
        /// <param name="success"></param>
        void vectorDrawBaseControl1_IsValidOpenFormat(object sender, string extension, ref bool success)
        {
            success = (extension.EndsWith(".vlib", StringComparison.CurrentCultureIgnoreCase));
        }
        /// <summary>
        /// This event is used in order to change the cursor of the VDF into a different taken from the resources.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="cancel"></param>
        void vectorDrawBaseControl1_vdMouseEnter(EventArgs e, ref bool cancel)
        {
            vectorDrawBaseControl1.SetCustomMousePointer(new System.Windows.Forms.Cursor(Properties.Resources.blkform.Handle));
        }
        /// <summary>
        /// This event is used to cancel the Pan command to the viewing control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="actionName"></param>
        /// <param name="cancel"></param>
        void vectorDrawBaseControl1_ActionStart(object sender, string actionName, ref bool cancel)
        {
            if (actionName == "BaseAction_ActionPan") cancel = true;
        }
        /// <summary>
        /// This event is used in order to show the .vlib files to the open dialog while opening blocks library files.
        /// </summary>
        /// <param name="openFilter"></param>
        void vectorDrawBaseControl1_GetOpenFileFilterFormat(ref string openFilter)
        {
            openFilter = "VectorDraw Library Files(*.vlib)|*.vlib|";
        }
        private void BlockLibrary_Load(object sender, EventArgs e)
        {
            InitializeControls();
        }
        /// <summary>
        /// Initialized the Document for the dialog.This method is used before showing the dialog in order to initialize the Document variable.
        /// </summary>
        /// <param name="doc"></param>
        public void InitializeDialog(vdDocument doc)
        {
            mdoc = doc;
        }
        /// <summary>
        /// Closing the dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BlockLibrary_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.vectorDrawBaseControl1.GetOpenFileFilterFormat -= new VectorDraw.Professional.Control.GetOpenFileFilterFormatEventHandler(vectorDrawBaseControl1_GetOpenFileFilterFormat);
            this.vectorDrawBaseControl1.GetSaveFileFilterFormat -= new VectorDraw.Professional.Control.GetSaveFileFilterFormatEventHandler(vectorDrawBaseControl1_GetSaveFileFilterFormat);
            this.vectorDrawBaseControl1.ActionStart -= new VectorDraw.Professional.Control.ActionStartEventHandler(vectorDrawBaseControl1_ActionStart);
            this.vectorDrawBaseControl1.vdMouseEnter -= new VectorDraw.Professional.Control.MouseEnterEventHandler(vectorDrawBaseControl1_vdMouseEnter);
            this.vectorDrawBaseControl1.IsValidOpenFormat -= new VectorDraw.Professional.Control.IsValidOpenFormatEventHandler(vectorDrawBaseControl1_IsValidOpenFormat);
            this.vectorDrawBaseControl1.SaveUnknownFileName -= new VectorDraw.Professional.Control.SaveUnknownFileNameEventHandler(vectorDrawBaseControl1_SaveUnknownFileName);
            this.vectorDrawBaseControl1.LoadUnknownFileName -= new VectorDraw.Professional.Control.LoadUnknownFileNameEventHandler(vectorDrawBaseControl1_LoadUnknownFileName);
        }
        /// <summary>
        /// Initializes properties for the dialog.
        /// </summary>
        private void InitializeControls()
        {
            
            vectorDrawBaseControl1.ActiveDocument.ShowUCSAxis = false;
            vectorDrawBaseControl1.ActiveDocument.EnableAutoGripOn = false;
            vectorDrawBaseControl1.ActiveDocument.MouseWheelZoomScale = 1.0;
            this.vectorDrawBaseControl1.ActiveDocument.Palette.Background = mdoc.Palette.Background;
            groupPreview.Enabled = false;

            LoadMainBlocks(0,"");

            if (Libraryname != null && Libraryname != "")
            {
                LoadLibrary(Libraryname);
            }
            
        }
        #endregion

        #region LoadBlocks
        /// <summary>
        /// Fills the combo tha contains the Main document's blocks.
        /// </summary>
        /// <param name="index">The index to have as selected for the combo if the second parameter is "".</param>
        /// <param name="blocknameForIndex">The name of the block to be set as selected.If this parameter is "" then the first parameter is set as selected for the combo.</param>
        private void LoadMainBlocks(int index,string blocknameForIndex)
        {
            combMainBlocks.Items.Clear();
            int i = 0;
            int foundindex = 0;
            foreach (vdBlock  var in mdoc.Blocks)
            {
                combMainBlocks.Items.Add(var.Name);
                if (blocknameForIndex != "")
                {
                    if (blocknameForIndex == var.Name) foundindex = i;
                }
                i++;
            }

            if (blocknameForIndex == "") combMainBlocks.SelectedIndex = index;
            else 
            {
                if (foundindex > combMainBlocks.Items.Count) foundindex = 0;
                if (foundindex != 0) { combMainBlocks.SelectedIndex = foundindex; return; }
                if (foundindex == 0 && combMainBlocks.Items.Count != 0) combMainBlocks.SelectedIndex = foundindex;
                else combMainBlocks.SelectedItem = null;
            }
        }
        /// <summary>
        /// Loads to the combo the library's blocks.
        /// </summary>
        /// <param name="name">The name of the block to be set as selected for the combo.This parameter is usually used when editing or adding
        /// a block to the library and is set as selected when the add/edit form is closed.</param>
        private void LoadLibraryBlocks(string name)
        {
            int index = 0;
            int i = 0;
            listBlocks.Items.Clear();
            //We exclude from the combo box the default "VDDIM_DEFAULT","VDDIM_NONE" blocks.
            foreach (vdBlock  var in vectorDrawBaseControl1.ActiveDocument.Blocks)
            {
                if (var.Name.Equals("VDDIM_DEFAULT", StringComparison.CurrentCultureIgnoreCase) || var.Name.Equals("VDDIM_NONE", StringComparison.CurrentCultureIgnoreCase)) continue;
                listBlocks.Items.Add(var.Name);
                if (name != null && var.Name == name) index = i;
                i++;
            }
            if (index > listBlocks.Items.Count) index = 0;
            if (index != 0) { listBlocks.SelectedIndex = index; return; }
            if (index == 0 && listBlocks.Items.Count != 0) listBlocks.SelectedIndex = index;
            else listBlocks.SelectedItem = null;
        }
        #endregion
        /// <summary>
        /// This event is used when the library's selected block is changed in order to update the preview component.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBlocks_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedname = (string)listBlocks.SelectedItem;
            if (selectedname != null && selectedname != "")
            {
                DrawPreview(selectedname);
            }
        }
        /// <summary>
        /// Draws to the preview component the block indicated by the parameter.
        /// </summary>
        /// <param name="selectedname">The name of the block to be displayed.</param>
        private void DrawPreview(string selectedname)
        {
            this.vectorDrawBaseControl1.ActiveDocument.Model.Entities.RemoveAll();
            if (selectedname != null && selectedname != "")
            {
                this.vectorDrawBaseControl1.ActiveDocument.Palette.Background = mdoc.Palette.Background;
                vdInsert ins = new vdInsert();
                ins.SetUnRegisterDocument(vectorDrawBaseControl1.ActiveDocument);
                ins.setDocumentDefaults();
                ins.Block = vectorDrawBaseControl1.ActiveDocument.Blocks.FindName(selectedname);

                vectorDrawBaseControl1.ActiveDocument.Model.Entities.AddItem(ins);
                vectorDrawBaseControl1.ActiveDocument.CommandAction.Zoom("E", 0, 0);
                vectorDrawBaseControl1.ActiveDocument.CommandAction.Zoom("S", 0.95 , 0);
            }
            vectorDrawBaseControl1.ActiveDocument.Redraw(true);
        }
        /// <summary>
        /// Loads the library with the specified filename.
        /// </summary>
        /// <param name="fname">The filename of the library.</param>
        private void LoadLibrary(string fname)
        {
            bool successopen = vectorDrawBaseControl1.ActiveDocument.Open(fname);
            if (!successopen)
            {
                System.Windows.Forms.MessageBox.Show("Error openning " + fname, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                groupPreview.Enabled = false;
                return;
            }
            groupPreview.Enabled = true;
            vectorDrawBaseControl1.ActiveDocument.EnableAutoGripOn = false;
            this.butSave.Enabled = false;
            LoadLibraryBlocks(null);
            textLibraryfilename.Text = fname;
            Libraryname = fname;
        }

        #region Buttons
        /// <summary>
        /// This button displays the dialog to open a library file(vlib).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butSelect_Click(object sender, EventArgs e)
        {

            object ret = vectorDrawBaseControl1.ActiveDocument.GetOpenFileNameDlg(0, System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\vdRes\\*.vlib", 0, this);
            if (ret == null)
            { groupPreview.Enabled = false; return; }
            string fname = (string)ret;
            LoadLibrary(fname);
        }
        /// <summary>
        /// This button adds the selected block from the library to the current Document.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butAddMain_Click(object sender, EventArgs e)
        {
           string selectedname = (string)listBlocks.SelectedItem;
           if (selectedname != null && selectedname != "")
           {
               vdBlock blk = mdoc.Blocks.FindName(selectedname);
               if (blk != null)
               {
                   DialogResult res = MessageBox.Show("A block already exists in the document with this name do you want to ovewrite?", "Info", MessageBoxButtons.YesNo);
                   if (res == DialogResult.No) return;
               }
               vdBlock blkFromLibrary = vectorDrawBaseControl1.ActiveDocument.Blocks.FindName(selectedname);
               if (blkFromLibrary != null)
               {
                   vdBlock blkTogo = new vdBlock();
                   blkTogo.SetUnRegisterDocument(mdoc);
                   blkTogo.setDocumentDefaults();
                   blkTogo.MatchProperties(blkFromLibrary, mdoc);

                   mdoc.Blocks.AddItem(blkTogo);
                   LoadMainBlocks(0, blkTogo.Name);
               }
               else
               {
                   MessageBox.Show("An error occured", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
               }
           }
        }
        /// <summary>
        /// This dialog saves the library with it's current name.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butSave_Click(object sender, EventArgs e)
        {
            if (Libraryname != "")
            {
                bool suc = vectorDrawBaseControl1.ActiveDocument.SaveAs(Libraryname);
                if (suc)
                {
                    MessageBox.Show("Library " + Libraryname + " saved!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.butSave.Enabled = false;
                }
            }
            else
                MessageBox.Show("Please select a library!", "", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
        /// <summary>
        /// This button shows a dialog to create a library.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butCreate_Click(object sender, EventArgs e)
        {
            string ver = "";
            string fname = vectorDrawBaseControl1.ActiveDocument.GetSaveFileNameDlg("",out ver);
            if (fname != null && fname != "")
            {
                vectorDrawBaseControl1.ActiveDocument.SaveAs (fname);
                Libraryname = fname;
                MessageBox.Show("Library " + Libraryname + " Created!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadLibrary(Libraryname);
                this.butSave.Enabled = false;
            }
        }
        /// <summary>
        /// This button removes the selected block from the library's blocks.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butRemoveBlock_Click(object sender, EventArgs e)
        {
            string selectedname = (string)listBlocks.SelectedItem;
            if (selectedname != null && selectedname != "")
            {
                vdBlock blk = vectorDrawBaseControl1.ActiveDocument.Blocks.FindName(selectedname);
                if (blk != null)
                {
                    DrawPreview(null);
                    vectorDrawBaseControl1.ActiveDocument.Blocks.RemoveItem(blk);
                    this.butSave.Enabled = true;
                }
                LoadLibraryBlocks(null);
                
            }
        }
        /// <summary>
        /// This button opens the other form(EditCreateBlock.cs) in order to Create a new block to the library.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butCreateBlock_Click(object sender, EventArgs e)
        {
            EditCreateBlock frm = new EditCreateBlock();
            frm.InitializeDialog(null, vectorDrawBaseControl1.ActiveDocument);
            frm.ShowDialog(this);

            vdBlock exported = frm.ExportedBlock;
            if (exported != null)
            {
                this.butSave.Enabled = true;
                vdBlock blk = new vdBlock();
                blk.SetUnRegisterDocument(vectorDrawBaseControl1.ActiveDocument);
                blk.setDocumentDefaults();
                blk.MatchProperties(exported,vectorDrawBaseControl1.ActiveDocument);
                vectorDrawBaseControl1.ActiveDocument.Blocks.AddItem(blk);
                LoadLibraryBlocks(exported.Name);
                DrawPreview(exported.Name);
            }
        }
        /// <summary>
        /// This button opens the other form(EditCreateBlock.cs) in order to edit the selected block from the library.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butEdit_Click(object sender, EventArgs e)
        {
            string selectedname = (string)listBlocks.SelectedItem;
            if (selectedname != null && selectedname != "")
            {
                vdBlock blk = vectorDrawBaseControl1.ActiveDocument.Blocks.FindName(selectedname);
                if (blk != null)
                {
                    EditCreateBlock frm = new EditCreateBlock();
                    frm.InitializeDialog(blk, vectorDrawBaseControl1.ActiveDocument);
                    frm.ShowDialog(this);
                    
                    vdBlock exported = frm.ExportedBlock;
                    if (exported !=null)
                    {
                        this.butSave.Enabled = true;
                        blk.MatchProperties(exported,vectorDrawBaseControl1.ActiveDocument);
                        LoadLibraryBlocks(exported.Name);
                        DrawPreview(exported.Name);
                    }
                }
            }
        }
        /// <summary>
        /// This button closes the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        
    }
}