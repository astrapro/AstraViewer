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
    /// This form is used to Add/Edit blocks to the library(vlib files).The component in this form uses different Menu 
    /// and Commands from the main application named BlockLibraryCommands.txt and BlockLibraryMenu.txt.
    /// </summary>
    public partial class EditCreateBlock : Form
    {
        #region properties
        private vdDocument mDoc = null;
        private vdBlock mblk = null;
        public vdBlock ExportedBlock = null;
        #endregion

        #region Initialize
        /// <summary>
        /// Initializes the dialog.
        /// </summary>
        public EditCreateBlock()
        {
            InitializeComponent();
        }
        /// <summary>
        /// This method is called before showing this dialog in order to initialize important variables.
        /// </summary>
        /// <param name="blk">The block to be edited or null if a new block is going to be created.</param>
        /// <param name="doc">A vdDocument object needed for initial values.</param>
        public void InitializeDialog(vdBlock blk, vdDocument doc)
        {
            mDoc = doc;
            mblk = blk;
        }
        /// <summary>
        /// This method is used when the resources directory is required.
        /// </summary>
        /// <returns></returns>
        public string GetResourcesPath()
        {
            //The same function in implemented also in EditCreateBlock.cs
            return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\vdRes\\";
        }
        /// <summary>
        /// Initializes the dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditCreateBlock_Load(object sender, EventArgs e)
        {
            vdFramedControl1.BaseControl.ActiveDocument.New();
            if (mblk == null)
            {
                textName.Text = "";
                butAddInsertionPoint.Enabled = true;
            }
            else
            {
                textName.Text = mblk.Name;
                butAddInsertionPoint.Enabled = false;

                vdBlock block = new vdBlock();
                block.SetUnRegisterDocument(vdFramedControl1.BaseControl.ActiveDocument);
                block.setDocumentDefaults();
                block.MatchProperties(mblk, vdFramedControl1.BaseControl.ActiveDocument);
                vdFramedControl1.BaseControl.ActiveDocument.Blocks.AddItem(block);

                vdInsert ins = new vdInsert();
                ins.SetUnRegisterDocument(vdFramedControl1.BaseControl.ActiveDocument);
                ins.setDocumentDefaults();
                ins.Block = block;
                vdFramedControl1.BaseControl.ActiveDocument.Model.Entities.AddItem(ins);
                vdFramedControl1.BaseControl.ActiveDocument.CommandAction.CmdExplode(ins);
                ins.Deleted = true;

                vdPoint pt = new vdPoint();
                pt.SetUnRegisterDocument(vdFramedControl1.BaseControl.ActiveDocument);
                pt.setDocumentDefaults();
                pt.InsertionPoint = new gPoint();
                pt.PenColor.SystemColor = Color.Red;
                pt.Label = "VD_TMP_ORIGIN";
                vdFramedControl1.BaseControl.ActiveDocument.Model.Entities.AddItem(pt);

                vdFramedControl1.BaseControl.ActiveDocument.CommandAction.Zoom("E", 0, 0);
                vdFramedControl1.BaseControl.ActiveDocument.CommandAction.Zoom("S", 0.95, 0);
                vdFramedControl1.BaseControl.ActiveDocument.Redraw(true);
            }
            vdFramedControl1.BaseControl.ActiveDocument.OnUndoStoreValue += new vdDocument.UndoStoreValueEventHandler(ActiveDocument_OnUndoStoreValue);
            //LOAD EditBlocksCOMMANDS / EditBlocksMENU 
            vdFramedControl1.UnLoadCommands();
            vdFramedControl1.UnLoadMenu();
            vdFramedControl1.LoadCommands(GetResourcesPath(), "BlockLibraryCommands.txt");
            vdFramedControl1.LoadMenu(GetResourcesPath(), "BlockLibraryMenu.txt");
            vdFramedControl1.ShowMenu(true);
        }
        /// <summary>
        /// This event is used in order to specify if the insertion point of the block is already set.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="isRedo"></param>
        /// <param name="propObject"></param>
        /// <param name="propName"></param>
        /// <param name="value"></param>
        /// <param name="Cancel"></param>
        void ActiveDocument_OnUndoStoreValue(object sender, bool isRedo, object propObject, string propName, object value, ref bool Cancel)
        {
            if (propObject is vdPoint && propName == "Deleted" && value is bool && (bool)value == false)
            {
                vdPoint pt = propObject as vdPoint;
                if (pt.Label == "VD_TMP_ORIGIN")
                {
                    butAddInsertionPoint.Enabled = true;
                }
            }
        }

        #endregion
        #region Buttons
        /// <summary>
        /// Closes the dialog with a cancel return message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            ExportedBlock = null;
            this.Close();
        }
        /// <summary>
        /// Closes the dialog with an ok return message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butOk_Click(object sender, EventArgs e)
        {
            if (textName.Text == "") { MessageBox.Show("Please set a name for the block!!!", "", MessageBoxButtons.OK, MessageBoxIcon.Question); ExportedBlock = null; return; }

            bool OriginExists = false;
            ExportedBlock = new vdBlock();
            ExportedBlock.SetUnRegisterDocument(mDoc);
            ExportedBlock.setDocumentDefaults();
            foreach (vdFigure  var in vdFramedControl1.BaseControl.ActiveDocument.Model.Entities)
            {
                if (var.Deleted) continue;
                if (var is vdPoint && var.Label == "VD_TMP_ORIGIN")
                {
                    vdPoint vdPt = var as vdPoint;
                    gPoint pt = vdFramedControl1.BaseControl.ActiveDocument.User2WorldMatrix.Transform(vdPt.InsertionPoint);
                    ExportedBlock.Origin = new gPoint(pt);
                    OriginExists = true;
                }
                else
                {
                    ExportedBlock.Entities.AddItem(var.Clone(mDoc) as vdFigure);
                }
            }
            if (!OriginExists) { MessageBox.Show("Please set an Origin Point for the block!!!", "", MessageBoxButtons.OK, MessageBoxIcon.Question); ExportedBlock = null; return; }
            ExportedBlock.Name = textName.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        /// <summary>
        /// This button specifies the insertion point of the block.The insertion point is added as a vdpoint with label "VD_TMP_ORIGIN" and this vdpoint is excluded when the block is saved and returned.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butAddInsertionPoint_Click(object sender, EventArgs e)
        {
            gPoint userpoint;
            vdFramedControl1.BaseControl.ActiveDocument.Prompt("Select a Point:");
            StatusCode ret = vdFramedControl1.BaseControl.ActiveDocument.ActionUtility.getUserPoint(out userpoint);
            vdFramedControl1.BaseControl.ActiveDocument.Prompt(null);
            if (ret == StatusCode.Success)
            {
                vdPoint pt = new vdPoint();
                pt.SetUnRegisterDocument(vdFramedControl1.BaseControl.ActiveDocument);
                pt.setDocumentDefaults();
                pt.InsertionPoint = new gPoint(userpoint);
                pt.PenColor.SystemColor = Color.Red;
                pt.Label = "VD_TMP_ORIGIN";

                vdFramedControl1.BaseControl.ActiveDocument.Model.Entities.AddItem(pt);
                vdFramedControl1.BaseControl.ActiveDocument.Redraw(true);
                butAddInsertionPoint.Enabled = false;
            }
        }
        #endregion

    }
}