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
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using VectorDraw.Actions;
using VectorDraw.Geometry;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Professional.vdObjects;
using HeadsUtils;
using HeadsUtils.Interfaces;
using HeadsViewer.HdEntities;
using VectorDraw.Professional.vdFigures;
using HEADSNeed.ASTRA.ASTRAForms;

namespace HeadsViewer
{
    /// <summary>
    /// This form is the MDI form that contains the vdScrollable control where all drawings are.
    /// </summary>
    public partial class Childform : Form, IHeadsDocument
    {
        #region Chiranjit 29 10 2009
        frmMemberGrid fMembGrid = null;
        frmMemberLoadCase fMembLoadCase = null;
        frmPostProcess fPostProc = null;
        #endregion


        private bool mRaiseGripEvent = true;
        HeadsUtils.CCfgtype configparam = new CCfgtype();
        /// <summary>
        /// Initializes the form.
        /// </summary>
        public Childform()
        {
            InitializeComponent();
            vdScrollableControl1.BaseControl.InitializeDocument += new VectorDraw.Professional.Control.InitializeDocumentEventHandler(BaseControl_InitializeDocument);
            vdScrollableControl1.BaseControl.UnInitializeDocument += new VectorDraw.Professional.Control.UnInitializeDocumentEventHandler(BaseControl_UnInitializeDocument);
           
             tmr.Tick += new EventHandler(tmr_Tick);
        
        }
        public vdScrollableControl.vdScrollableControl VectorDrawCtrl
        {
            get { return this.vdScrollableControl1; }
        }
        /// <summary>
        /// Unitializes VDF events.
        /// </summary>
        /// <param name="document"></param>
        void BaseControl_UnInitializeDocument(VectorDraw.Professional.vdObjects.vdDocument document)
        {
            this.vdScrollableControl1.BaseControl.vdMouseDown -= new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
            this.vdScrollableControl1.BaseControl.vdKeyDown -= new VectorDraw.Professional.Control.KeyDownEventHandler(BaseControl_vdKeyDown);
            this.vdScrollableControl1.BaseControl.GripSelectionModified -= new VectorDraw.Professional.Control.GripSelectionModifiedEventHandler(BaseControl_GripSelectionModified);
            this.vdScrollableControl1.BaseControl.ActionLayoutActivated -= new VectorDraw.Professional.Control.ActionLayoutActivatedEventHandler(BaseControl_ActionLayoutActivated);
            this.vdScrollableControl1.BaseControl.AfterNewDocument -= new VectorDraw.Professional.Control.AfterNewDocumentEventHandler(BaseControl_AfterNewDocument);
            this.vdScrollableControl1.BaseControl.AfterOpenDocument -= new VectorDraw.Professional.Control.AfterOpenDocumentEventHandler(BaseControl_AfterOpenDocument);
            this.vdScrollableControl1.BaseControl.Progress -= new VectorDraw.Professional.Control.ProgressEventHandler(BaseControl_Progress);
            this.vdScrollableControl1.BaseControl.vdDragEnter -= new VectorDraw.Professional.Control.DragEnterEventHandler(BaseControl_vdDragEnter);
            if (this.vdScrollableControl1.BaseControl.ActiveDocument != null)
            {
                this.vdScrollableControl1.BaseControl.ActiveDocument.OnGetOpenFileFilterFormat -= new VectorDraw.Professional.vdObjects.vdDocument.GetOpenFileFilterFormatEventHandler(ActiveDocument_OnGetOpenFileFilterFormat);
                this.vdScrollableControl1.BaseControl.ActiveDocument.OnGetSaveFileFilterFormat -= new VectorDraw.Professional.vdObjects.vdDocument.GetSaveFileFilterFormatEventHandler(ActiveDocument_OnGetSaveFileFilterFormat);
                this.vdScrollableControl1.BaseControl.ActiveDocument.OnActionJobLoop -= new vdDocument.ActionJobLoopEventHandler(ActiveDocument_OnActionJobLoop);
                this.vdScrollableControl1.BaseControl.ActiveDocument.OnAfterModifyObject -= new vdDocument.AfterModifyObjectEventHandler(ActiveDocument_OnAfterModifyObject);
            }
        }
        /// <summary>
        /// Initializes VDF events.
        /// </summary>
        /// <param name="document"></param>
        void BaseControl_InitializeDocument(VectorDraw.Professional.vdObjects.vdDocument document)
        {
            #region Chiranjit 30 10 2009
            this.vdScrollableControl1.BaseControl.vdMouseUp += new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);
            #endregion


            this.vdScrollableControl1.BaseControl.vdMouseDown += new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
            this.vdScrollableControl1.BaseControl.vdKeyDown += new VectorDraw.Professional.Control.KeyDownEventHandler(BaseControl_vdKeyDown);
            this.vdScrollableControl1.BaseControl.GripSelectionModified += new VectorDraw.Professional.Control.GripSelectionModifiedEventHandler(BaseControl_GripSelectionModified);
            this.vdScrollableControl1.BaseControl.ActionLayoutActivated += new VectorDraw.Professional.Control.ActionLayoutActivatedEventHandler(BaseControl_ActionLayoutActivated);
            this.vdScrollableControl1.BaseControl.AfterNewDocument += new VectorDraw.Professional.Control.AfterNewDocumentEventHandler(BaseControl_AfterNewDocument);
            this.vdScrollableControl1.BaseControl.AfterOpenDocument += new VectorDraw.Professional.Control.AfterOpenDocumentEventHandler(BaseControl_AfterOpenDocument);
            this.vdScrollableControl1.BaseControl.Progress += new VectorDraw.Professional.Control.ProgressEventHandler(BaseControl_Progress);
            this.vdScrollableControl1.BaseControl.MouseMoveAfter += new VectorDraw.Professional.Control.MouseMoveAfterEventHandler(BaseControl_MouseMoveAfter);
            this.vdScrollableControl1.BaseControl.vdDragEnter += new VectorDraw.Professional.Control.DragEnterEventHandler(BaseControl_vdDragEnter);
            if (this.vdScrollableControl1.BaseControl.ActiveDocument != null)
            {
                this.vdScrollableControl1.BaseControl.ActiveDocument.OnGetOpenFileFilterFormat += new VectorDraw.Professional.vdObjects.vdDocument.GetOpenFileFilterFormatEventHandler(ActiveDocument_OnGetOpenFileFilterFormat);
                this.vdScrollableControl1.BaseControl.ActiveDocument.OnGetSaveFileFilterFormat += new VectorDraw.Professional.vdObjects.vdDocument.GetSaveFileFilterFormatEventHandler(ActiveDocument_OnGetSaveFileFilterFormat);
                this.vdScrollableControl1.BaseControl.ActiveDocument.OnActionJobLoop += new vdDocument.ActionJobLoopEventHandler(ActiveDocument_OnActionJobLoop);
                this.vdScrollableControl1.BaseControl.ActiveDocument.OnAfterModifyObject += new vdDocument.AfterModifyObjectEventHandler(ActiveDocument_OnAfterModifyObject);
            }
        }

        #region drag & drop Implementation
        static vdScrollableControl.vdScrollableControl activeDragDropControl = null;
        void BaseControl_vdDragEnter(DragEventArgs drgevent, ref bool cancel)
        {
            if (object.ReferenceEquals(this.vdScrollableControl1, activeDragDropControl))
            {
                //Do nothing to the Document that the drag operation started
                drgevent.Effect = DragDropEffects.None;
            }
            else
            {
                //This is the Document other than the drag operation start document
                drgevent.Effect = DragDropEffects.Copy;
            }
        }
        #endregion

        #region Chiranjit 29 10 2009
        public void SetTextSize(double txtSize)
        {
            for (int i = 0; i < this.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Entities.Count; i++)
            {
                vdText txt = this.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Entities[i] as vdText;
                if (txt != null)
                {
                    txt.Height = txtSize;
                    txt.Update();
                }
            }
            this.vdScrollableControl1.BaseControl.ActiveDocument.Redraw(true);
        }
        WORK work = WORK.Structure;

        public WORK Work
        {
            get
            {
                return work;
            }
            set
            {
                work = value;
            }
        }
        void BaseControl_vdMouseUp(MouseEventArgs e, ref bool cancel)
        {
            try
            {
                vdSelection gripset = GetGripSelection(false);

                if (gripset.Count > 1) return;
                vd3DFace dFace;
                foreach (vdFigure fig in gripset)
                {
                    switch (Work)
                    {
                        case WORK.Loading:

                            vdLine ln = fig as vdLine;

                            if (ln == null)
                            {
                                dFace = fig as vd3DFace;
                                this.MemberGrid.ShowElementOnGrid(dFace);
                                this.vdScrollableControl1.BaseControl.ActiveDocument.Redraw(true);
                            }
                            else
                            {
                                this.MemberLoadcase.ShowMember(ln);
                                this.vdScrollableControl1.BaseControl.ActiveDocument.Redraw(true);
                            }
                            break;
                        case WORK.Structure:

                            ln = fig as vdLine;

                            if (ln == null)
                            {
                                dFace = fig as vd3DFace;
                                this.MemberGrid.ShowElementOnGrid(dFace);
                                this.vdScrollableControl1.BaseControl.ActiveDocument.Redraw(true);
                            }
                            else
                            {
                                this.MemberGrid.ShowMemberOnGrid(ln);
                                //ln.PenColor = new vdColor(Color.Red);
                                this.vdScrollableControl1.BaseControl.ActiveDocument.Redraw(true);
                            }
                            //fig.ShowGrips = false;
                            break;
                        case WORK.PostProcess:
                            ln = fig as vdLine;
                            if (ln == null)
                            {
                                dFace = fig as vd3DFace;

                                //this.PostProcess.ShowMemberOnForm(dFace);
                                this.vdScrollableControl1.BaseControl.ActiveDocument.Redraw(true);
                            }
                            else
                            {
                                this.PostProcess.ShowMemberOnForm(ln);
                                //ln.PenColor = new vdColor(Color.Red);
                                this.vdScrollableControl1.BaseControl.ActiveDocument.Redraw(true);
                            }
                            //fig.ShowGrips = false;
                            break;
                    }
                }

                //for (int i = 1; i < gripset.Count; i++)
                //{
                //    gripset.RemoveAt(i);
                //    i = 0;
                //}
                gripset.RemoveAll();

            }
            catch (Exception exx) { }
        }
        #endregion

        void BaseControl_vdMouseDown(MouseEventArgs e, ref bool cancel)
        {

            if ((System.Windows.Forms.Control.ModifierKeys & Keys.Shift) != 0 && e.Button == MouseButtons.Left)
            {
                //gripset = GetGripSelection(false);
                vdSelection gripset = GetGripSelection(false);

                if (gripset == null) return;
                gPoint pt = this.vdScrollableControl1.BaseControl.ActiveDocument.CCS_CursorPos();
                activeDragDropControl = this.vdScrollableControl1;
                int result = this.vdScrollableControl1.BaseControl.ActiveDocument.CommandAction.DoDragDrop(gripset, vdSelection.DragDropEffects.All, pt, 100, 100, 3);
                activeDragDropControl = null;
                return;
            }
            if (e.Button != MouseButtons.Right) return;
            if ((System.Windows.Forms.Control.ModifierKeys & Keys.Control) != 0) return;
            if (vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.OverAllActiveActions.Count != 1) return;

            if ((System.Windows.Forms.Control.ModifierKeys & Keys.Shift) != 0)
            {
                //gripset = GetGripSelection(false);
                vdSelection gripset = GetGripSelection(false);
                if (gripset == null) return;
                gPoint pt = this.vdScrollableControl1.BaseControl.ActiveDocument.CCS_CursorPos();
                activeDragDropControl = this.vdScrollableControl1;
                int result = this.vdScrollableControl1.BaseControl.ActiveDocument.CommandAction.DoDragDrop(gripset, vdSelection.DragDropEffects.All, pt, 100, 100, 3);
                activeDragDropControl = null;
            }
            else
            {
                if (BaseAction.PressedKeyCode != Keys.None) return; //6011 correction
                MainForm parent = this.MdiParent as MainForm;
                parent.commandLine.PostExecuteCommand("");
            }
        }
        /// <summary>
        /// In VDFCAD you can select entities and then click a command button and then the command will run for the selected entities.
        /// This is handled by this event and if there are selected items to the active MDI form then the select command is canceled and
        /// the already selected entities are used. Try to select 3 entities and press the Delete button you will notice that no select
        /// command will ask you to select which entities to delete.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="action"></param>
        /// <param name="cancel"></param>
        void ActiveDocument_OnActionJobLoop(object sender, object action, ref bool cancel)
        {
            BaseAction act = action as BaseAction;
            if ((act.ValueTypeProp == BaseAction.valueType.OBJECTSELECT))
            {
                vdDocument doc = sender as vdDocument;
                bool ret = doc.SendCommandActionString("grip");
                if (ret)
                {
                    ClearSelection();
                    act.FinishAction(act);
                }
            }
        }
        /// <summary>
        /// This event is used in order to specify the formats that are displayed in the save dialog at SaveAs command. Note that raster formats are missing since there is a dialog for raster export.
        /// </summary>
        /// <param name="saveFilter"></param>
        void ActiveDocument_OnGetSaveFileFilterFormat(ref string saveFilter)
        {
            saveFilter = "VDCL (*.vdcl)|*.vdcl|VDML (*.vdml)|*.vdml|DXF 2004 (*.dxf)|*.dxf|DXF 2000 (*.dxf)|*.dxf|DXF 12 (*.dxf)|*.dxf|DWG 2007 (*.dwg)|*.dwg|DWG 2004 (*.dwg)|*.dwg|DWG 2000 (*.dwg)|*.dwg|DWG 12 (*.dwg)|*.dwg|DGN 8 (*.dgn)|*.dgn|SVG (*.svg)|*.svg|DWF (*.dwf)|*.dwf||?VDML?VDCL?DXF2004?DXF2000?DXF12?DXF2007?DXF2004?DXF2000?DXF12?DGN8?SVG?DWF?";
            //saveFilter = "VDCL (*.vdcl)|*.vdcl|VDML (*.vdml)|*.vdml|DXF 2004 (*.dxf)|*.dxf|DXF 2000 (*.dxf)|*.dxf|DXF 12 (*.dxf)|*.dxf|DWG 2007 (*.dwg)|*.dwg|DWG 2004 (*.dwg)|*.dwg|DWG 2000 (*.dwg)|*.dwg|DWG 12 (*.dwg)|SVG (*.svg)|*.svg|DWF (*.dwf)|*.dwf||?VDML?VDCL?DXF2004?DXF2000?DXF12?DXF2007?DXF2004?DXF2000?DXF12?DGN8?SVG?DWF?";
        }
        /// <summary>
        /// This event is used in order to specify the valid open formats that are displayed to the open file dialog.
        /// </summary>
        /// <param name="openFilter"></param>
        void ActiveDocument_OnGetOpenFileFilterFormat(ref string openFilter)
        {
            openFilter = "All Drawing Files (*.vdml;*.vdcl;*.vdf;*.vdi;*.vdp;*.dwg;*.dxf;*.dgn;*.emf;*.wmf)|*.vdml;*.vdcl;*.vdf;*.vdi;*.vdp;*.dwg;*.dxf;*.dgn;*.emf;*.wmf;|VDML Document (*.vdml)|*.vdml|VDCL Document (*.vdcl)|*.vdcl|VectorDraw Files (*.DWG)|*.DWG|VectorDraw Files (*.DXF)|*.DXF|VectorDraw Files (*.DGN)|*.DGN|EMF Files (*.EMF)|*.EMF|WMF Files (*.WMF)|*.WMF|Images(*.bmp;*.gif;*.jpg;*.tif;*.ico;*.png)|*.bmp;*.gif;*.jpg;*.tif;*.ico;*.png|VectorDraw Files (*.VDF)|*.VDF|VectorDrawI Files (*.VDI)|*.VDI|VectorDraw Project Files (*.VDP)|*.VDP|All Files(*.*)|*.*||";
        }
        /// <summary>
        /// This event is used to ask the user when the form is closing if he wants to save or not.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            MainForm parent = this.MdiParent as MainForm;
            if (vdScrollableControl1.BaseControl.ActiveDocument.IsModified && parent.ReleaseType != eHEADS_RELEASE_TYPE.DEMO)
            {
                DialogResult res = MessageBox.Show("Save changes in Drawing \n" + this.Text + " ?", this.MdiParent.Text, MessageBoxButtons.YesNoCancel);
                if (res == DialogResult.Yes)
                {
                    string version = "";
                    string fname = vdScrollableControl1.BaseControl.ActiveDocument.GetSaveFileNameDlg(this.Text, out version);
                    if (fname != null)
                    {
                        bool success = vdScrollableControl1.BaseControl.ActiveDocument.SaveAs(fname, null, version);
                        if (success == false)
                        {
                            MessageBox.Show("Error saving \n" + fname, this.MdiParent.Text);
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else if (res == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }

            base.OnClosing(e);
        }
        /// <summary>
        /// Closing the MDI form.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {

            base.OnClosed(e);
            MainForm parent = this.MdiParent as MainForm;
            if (parent != null) parent.commandLine.SelectDocument(null);
            FillPropertyGrid(null);
            if (parent != null) parent.UpdateMenu(parent.MdiChildren.Length == 1);
            vdScrollableControl1.BaseControl.ActiveDocument.Dispose();
            vdScrollableControl1.BaseControl.Dispose();
        }
        /// <summary>
        /// This event is used in order to display the coords in the bottom of the application when the mouse is moving.
        /// </summary>
        /// <param name="e"></param>
        private void BaseControl_MouseMoveAfter(MouseEventArgs e)
        {
            MainForm parent = this.MdiParent as MainForm;
            VectorDraw.Geometry.gPoint ccspt = vdScrollableControl1.BaseControl.ActiveDocument.CCS_CursorPos();

            double x = ccspt.x;
            double y = ccspt.y;
            double z = ccspt.z;

            string str = vdScrollableControl1.BaseControl.ActiveDocument.lunits.FormatLength(x) + " , " + vdScrollableControl1.BaseControl.ActiveDocument.lunits.FormatLength(y) + " , " + vdScrollableControl1.BaseControl.ActiveDocument.lunits.FormatLength(z);
            if (parent != null && parent.mDisplayPolarCoord)
            {
                //if active action is user waiting reference point and mDisplayPolarCoord == true
                //then polar coord string
                if (vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.OverAllActiveActions.Count > 1)
                {
                    BaseAction action = vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.OverAllActiveAction;

                    if (action.WaitingPoint && action.ReferencePoint != null)
                    {
                        if ((action.ValueTypeProp & BaseAction.valueType.REFPOINT) == BaseAction.valueType.REFPOINT)
                        {
                            gPoint refpt = vdScrollableControl1.BaseControl.ActiveDocument.World2UserMatrix.Transform(action.ReferencePoint);
                            double angle = Globals.GetAngle(refpt, ccspt);
                            double dist = ccspt.Distance3D(refpt);
                            x = dist;
                            y = Globals.RadiansToDegrees(angle);
                            z = 0.0d;
                            str = vdScrollableControl1.BaseControl.ActiveDocument.lunits.FormatLength(dist) + "<" + vdScrollableControl1.BaseControl.ActiveDocument.aunits.FormatAngle(angle);
                        }
                    }
                }
            }
            //fix the width of the Coord Display.
            Graphics gr = Graphics.FromHwnd(IntPtr.Zero);
            Size sz2 = gr.MeasureString(str, parent.CoordDisplay.Font).ToSize();
            gr.Dispose();
            if (sz2.Width > parent.CoordDisplay.Size.Width) parent.CoordDisplay.Size = new Size((int)(sz2.Width * 1.2), parent.CoordDisplay.Size.Height);

            if (parent != null) parent.CoordDisplay.Text = str;
        }
        /// <summary>
        /// This event is used to display the progress bar at the bottom of the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="percent"></param>
        /// <param name="jobDescription"></param>
        private void BaseControl_Progress(object sender, long percent, string jobDescription)
        {
            MainForm parent = this.MdiParent as MainForm;
            if (parent == null) return;
            if (percent == 0 || percent == 100) parent.status.Text = "";
            else parent.status.Text = jobDescription;
            parent.ProgressBar.Value = (int)percent;
        }
        /// <summary>
        /// After opening a file some basic initializations like loading it's properties from the GeneralProperties form among others.
        /// </summary>
        /// <param name="sender"></param>
        private void BaseControl_AfterOpenDocument(object sender)
        {
            MainForm parent = this.MdiParent as MainForm;
            if (parent == null) return;
            VectorDraw.Professional.vdCommandLine.vdCommandLine cl = parent.commandLine;
            FillPropertyGrid(vdScrollableControl1.BaseControl.ActiveDocument);
            cl.SelectDocument(vdScrollableControl1.BaseControl.ActiveDocument);
            vdScrollableControl1.BaseControl.Focus();
            if (vdScrollableControl1.BaseControl.ActiveDocument.FileName != "")
            {
                this.Text = vdScrollableControl1.BaseControl.ActiveDocument.FileName;

            }
            LoadDocProps(vdScrollableControl1.BaseControl.ActiveDocument, false);
            parent.UpdatePictureBox(false);
        }
        /// <summary>
        /// Checks if a filename already exists opened as an MDI window.
        /// </summary>
        /// <param name="fname"></param>
        /// <returns></returns>
        private bool IsNewFileNameExist(string fname)
        {
            foreach (Childform frm in this.MdiParent.MdiChildren)
            {
                if (string.Compare(frm.Text, fname, true) == 0) return true;
            }
            return false;
        }
        /// <summary>
        /// After a new form some basic initializations. Asigns also a default filename.
        /// </summary>
        /// <param name="sender"></param>
        private void BaseControl_AfterNewDocument(object sender)
        {
            MainForm parent = this.MdiParent as MainForm;
            if (parent == null) return;
            VectorDraw.Professional.vdCommandLine.vdCommandLine cl = parent.commandLine;
            FillPropertyGrid(vdScrollableControl1.BaseControl.ActiveDocument);
            cl.SelectDocument(vdScrollableControl1.BaseControl.ActiveDocument);

            vdScrollableControl1.BaseControl.Focus();
            int i = 1;
            string fname = "Drawing" + i.ToString() + ".vdml";
            while (IsNewFileNameExist(fname))
            {
                i++;
                fname = "Drawing" + i.ToString() + ".vdml";
            }
            vdScrollableControl1.BaseControl.ActiveDocument.FileName = fname;


            //string full_path = fname;
            //try
            //{
            //    string tst1 = Path.GetFileName(full_path);
            //    string tst2 = Path.GetFileName(Path.GetDirectoryName(full_path));
            //    string tst3 = Path.GetPathRoot(full_path);
            //    if (tst2 != "")
            //        full_path = tst3 + "....\\" + Path.Combine(tst2, tst1);
            //}
            //catch (Exception ex) { }


            this.Text = fname;
            //this.Text = full_path;
            LoadDocProps(vdScrollableControl1.BaseControl.ActiveDocument, true);
            parent.UpdatePictureBox(true);
        }
        /// <summary>
        /// When a window is activated set the property list to reflect to this window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="deactivated"></param>
        /// <param name="activated"></param>
        private void BaseControl_ActionLayoutActivated(object sender, VectorDraw.Professional.vdPrimaries.vdLayout deactivated, VectorDraw.Professional.vdPrimaries.vdLayout activated)
        {
            VectorDraw.Professional.vdCollections.vdSelection gripset = vdScrollableControl1.BaseControl.ActiveDocument.Selections.FindName("VDGRIPSET_" + activated.Handle.ToStringValue());
            if (gripset != null)
            {
                foreach (VectorDraw.Professional.vdPrimaries.vdFigure var in gripset)
                {
                    var.ShowGrips = false;
                }
            }
            FillPropertyGrid(vdScrollableControl1.BaseControl.ActiveDocument);
            vdScrollableControl1.BaseControl.ReFresh();
        }
        /// <summary>
        /// This event is used to update the property grid when the grip selection of the document is changed.
        /// The mRaiseGripEvent boolean value is used not to use this event on the SelectAll Key Down event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="layout"></param>
        /// <param name="gripSelection"></param>
        private void BaseControl_GripSelectionModified(object sender, VectorDraw.Professional.vdPrimaries.vdLayout layout, VectorDraw.Professional.vdCollections.vdSelection gripSelection)
        {
            if (!mRaiseGripEvent) return;
            if (gripSelection.Count == 0)
            {
                FillPropertyGrid(vdScrollableControl1.BaseControl.ActiveDocument);
            }
            else
            {
                FillPropertyGrid(gripSelection);
            }
        }
        /// <summary>
        /// This method returns the default grip selection of the document.
        /// </summary>
        /// <param name="Create"></param>
        /// <returns></returns>
        private vdSelection GetGripSelection(bool Create)
        {
            VectorDraw.Professional.vdCollections.vdSelection gripset;
            string selsetname = "VDGRIPSET_" + vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Handle.ToStringValue() + (vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.ActiveViewPort != null ? vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.ActiveViewPort.Handle.ToStringValue() : "");
            gripset = vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Document.Selections.FindName(selsetname);
            if (Create)
            {
                if (gripset == null)
                {
                    gripset = vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Document.Selections.Add(selsetname);
                }
            }
            //vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Document.Selections.FindName(selsetname).RemoveAll();
            return gripset;
        }
        /// <summary>
        /// Key down event of the contro0l that implements basic key functions.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="cancel"></param>
        private void BaseControl_vdKeyDown(KeyEventArgs e, ref bool cancel)
        {
            MainForm parent = this.MdiParent as MainForm;
            if (e.Control)
            {
                if ((e.Shift) && (e.KeyCode == Keys.A)) { parent.commandLine.ExecuteCommand("saveas"); cancel = true; return; }
                else if (e.KeyCode == Keys.Z) { parent.commandLine.ExecuteCommand("undo"); cancel = true; return; }
                else if (e.KeyCode == Keys.Y) { parent.commandLine.ExecuteCommand("redo"); cancel = true; return; }
                else if (e.KeyCode == Keys.A)
                {
                    mRaiseGripEvent = false;
                    vdSelection gripset = GetGripSelection(true);

                    gripset.AddRange(vdScrollableControl1.BaseControl.ActiveDocument.ActionLayout.Entities, VectorDraw.Professional.vdCollections.vdSelection.AddItemCheck.RemoveInVisibleAndLockLayer);
                    foreach (vdFigure var in gripset)
                    {
                        var.ShowGrips = true;
                    }
                    vdScrollableControl1.BaseControl.Redraw();
                    cancel = true; mRaiseGripEvent = true;
                    FillPropertyGrid(gripset);
                    return;

                }

            }
            if (e.KeyCode == Keys.Delete) { if (GetGripSelection(false) != null) { parent.commandLine.ExecuteCommand("Erase"); cancel = true; return; } }

            BaseAction action = vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.OverAllActiveAction;
            if (!action.SendKeyEvents) return;

            if (parent == null) return;
            VectorDraw.Professional.vdCommandLine.vdCommandLine cl = parent.commandLine;
            if (cl.Visible == false) return;
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down) return;
            Message msg = new Message();
            msg.HWnd = cl.Handle;
            msg.Msg = (int)VectorDraw.WinMessages.MessageManager.Messages.WM_KEYDOWN;
            msg.WParam = (IntPtr)e.KeyCode;
            cl.vdProcessKeyMessage(ref msg);
        }
        /// <summary>
        /// Basic initializations when the window is activated.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            MainForm parent = this.MdiParent as MainForm;
            if (parent == null) return;
            parent.commandLine.SelectDocument(this.vdScrollableControl1.BaseControl.ActiveDocument);
            VectorDraw.Professional.vdCollections.vdSelection gripset = vdScrollableControl1.BaseControl.ActiveDocument.Selections.FindName("VDGRIPSET_" + vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Handle.ToStringValue());
            if (gripset != null && gripset.Count > 0) FillPropertyGrid(gripset);
            else FillPropertyGrid(this.vdScrollableControl1.BaseControl.ActiveDocument);

            if (this.vdScrollableControl1.BaseControl.ActiveDocument.GridMode)
            {
                parent.toolGrid.Checked = true; parent.toolGrid.Text = "GRID ON";
            }
            else
            {
                parent.toolGrid.Checked = false; parent.toolGrid.Text = "GRID OFF";
            }

            if (this.vdScrollableControl1.BaseControl.ActiveDocument.SnapMode)
            {
                parent.toolSnap.Checked = true; parent.toolSnap.Text = "SNAP ON";
            }
            else
            {
                parent.toolSnap.Checked = false; parent.toolSnap.Text = "SNAP OFF";
            }

            if (this.vdScrollableControl1.BaseControl.ActiveDocument.OrthoMode)
            {
                parent.toolOrtho.Checked = true; parent.toolOrtho.Text = "ORTHO ON";
            }
            else
            {
                parent.toolOrtho.Checked = false; parent.toolOrtho.Text = "ORTHO OFF";
            }
        }
        /// <summary>
        /// Basic uninitializations when the window is deactivated.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDeactivate(EventArgs e)
        {
            MainForm parent = this.MdiParent as MainForm;
            if (parent == null) return;
            parent.commandLine.SelectDocument(null);
            FillPropertyGrid(null);
            base.OnDeactivate(e);
        }
        /// <summary>
        /// This event is used to send all the keys to the commandline.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (vdScrollableControl1.BaseControl.Focused) return false;
            MainForm parent = this.MdiParent as MainForm;
            if (parent == null) return false;
            VectorDraw.Professional.vdCommandLine.vdCommandLine cl = parent.commandLine;
            if (cl.Visible == false) return false;

            if (keyData == Keys.Up || keyData == Keys.Down) return false;
            Message nmsg = new Message();
            nmsg.HWnd = cl.Handle;
            nmsg.Msg = msg.Msg;
            nmsg.WParam = msg.WParam;
            nmsg.LParam = msg.LParam;
            cl.vdProcessKeyMessage(ref nmsg);
            return false;
        }
        /// <summary>
        /// Support path initialization of the Scrollable control on loading the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Childform_Load(object sender, EventArgs e)
        {
            this.TextChanged += new EventHandler(Childform_TextChanged);
            MainForm parent = this.MdiParent as MainForm;

            if (parent == null) return;
            parent.UpdateMenu(false);
            string resources = parent.GetResourcesPath();
            if (!vdScrollableControl1.BaseControl.ActiveDocument.SupportPath.Contains(resources))
            {
                if (vdScrollableControl1.BaseControl.ActiveDocument.SupportPath == "") vdScrollableControl1.BaseControl.ActiveDocument.SupportPath = parent.GetResourcesPath();
                else vdScrollableControl1.BaseControl.ActiveDocument.SupportPath += ";" + parent.GetResourcesPath();
            }
            this.WindowState = FormWindowState.Maximized;
        }
        /// <summary>
        /// This event is used to update the Window menu when the form's text (open file, new file) is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Childform_TextChanged(object sender, EventArgs e)
        {
            Form activeChild = this.ActiveMdiChild;

            ActivateMdiChild(null);
            ActivateMdiChild(activeChild);
            //this.Text = vdScrollableControl1.BaseControl.ActiveDocument.FileName;

            string full_path = this.Text;
            try
            {
                if (File.Exists(full_path))
                {
                    string tst1 = Path.GetFileName(full_path);
                    string tst2 = Path.GetFileName(Path.GetDirectoryName(full_path));
                    string tst3 = Path.GetPathRoot(full_path);
                    if (tst2 != "")
                        full_path = tst3 + "....\\" + Path.Combine(tst2, tst1);
                }
            }
            catch (Exception ex) { full_path = this.Text; }
            this.Text = full_path;



        }
        /// <summary>
        /// This method will remove all selected entities from the grips collection and also hide their grips. The property list will also be updated.
        /// </summary>
        private void ClearSelection()
        {
            if (GetGripSelection(false) == null) return;
            FillPropertyGrid(vdScrollableControl1.BaseControl.ActiveDocument);

            vdSelection gripset = GetGripSelection(false);
            if (gripset != null)
            {
                foreach (vdFigure var in gripset)
                {
                    var.Invalidate();
                    var.ShowGrips = false;
                    var.Invalidate();

                }
                gripset.RemoveAll();
            }

        }
        /// <summary>
        /// Fills the property grid with the passed object.
        /// </summary>
        /// <param name="obj"></param>
        private void FillPropertyGrid(object obj)
        {
            MainForm parent = this.MdiParent as MainForm;
            if (parent == null) return;
            parent.vdgrid.SelectedObject = obj;
        }
        /// <summary>
        /// Loads the General properties to the passed document.Note that vdml,vdcl files do not load palette colors since they internally save the palette.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="IsNew"></param>
        private void LoadDocProps(VectorDraw.Professional.vdObjects.vdDocument doc, bool IsNew)
        {
            bool LoadPalette = true;
            if (doc.FileName.ToLower().EndsWith(".vdml") || doc.FileName.ToLower().EndsWith(".vdcl")) LoadPalette = false;
            if (IsNew) LoadPalette = true;

            MainForm parent = this.MdiParent as MainForm;

            if (parent == null) return;

            parent.Propertiesform.Initialize(doc, parent.GetResourcesPath(), parent.panel2);
            parent.Propertiesform.LoadProps(null, doc, LoadPalette);

        }

        void ActiveDocument_OnAfterModifyObject(object sender, string propertyname)
        {
            //If the ViewCenter is changed then the Aerial view is updated with the new center. The size does not change though.
            if (propertyname == "ViewCenter")
            {
                MainForm parent = this.MdiParent as MainForm;
                double pX = 0.0; double pY = 0.0;
                parent.CalculatePictureCenter(vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Entities.GetBoundingBox(true, false), parent.AerialView.Width, parent.AerialView.Height, ref pX, ref pY, vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.ViewCenter);
                parent.PrevX = (int)pX;
                parent.PrevY = (int)pY;
                parent.DrawRect();
            }
        }

        #region Private Members
        private VectorDraw.Professional.vdObjects.vdDocument ActiveVcadDoc
        {
            get
            {
                return this.VectorDrawCtrl.BaseControl.ActiveDocument; ;
            }
        }
        private IHdEntity CreateEntity(vdFigure vdfigure)
        {
            IHdEntity hdentity = null;
            if (vdfigure != null)
            {
                string strTypeName = vdfigure._TypeName;
                if (strTypeName == "vdPolyline")
                {
                    hdentity = new CVcadPolyLine((vdPolyline)vdfigure);
                }
                else if (strTypeName == "vdText")
                {
                    hdentity = new CVcadText((vdText)vdfigure);
                }
                else if (strTypeName == "vdLine")
                {
                    hdentity = new CVcadLine((vdLine)vdfigure);
                }
                else if (strTypeName == "vdArc")
                {
                    hdentity = new CVcadArc((vdArc)vdfigure);
                }
                else if (strTypeName == "vdPoint")
                {
                    hdentity = new CVcadPoint((vdPoint)vdfigure);
                }
                else if (strTypeName == "vd3DFace")
                {
                    hdentity = new CVcad3DFace((vd3DFace)vdfigure);
                }
            }

            return hdentity;
        }
        private vdFigure GeVDFigureByID(int iID)
        {
            vdFigure hdentity = null;
            foreach (vdFigure fig in this.ActiveVcadDoc.ActiveLayOut.Entities)
            {
                if (fig.Id == iID)
                {
                    hdentity = fig;
                    break;
                }
            }
            return hdentity;
        }
        #endregion

        #region IHeadsDocument Members

        public double ActiveTextHeight
        {
            get { return this.ActiveVcadDoc.ActiveTextStyle.Height; }
        }

        public IHdEntity[] Entities
        {
            get
            {
                List<IHdEntity> lstEnt = new List<IHdEntity>();
                foreach (vdFigure fig in this.ActiveVcadDoc.ActiveLayOut.Entities)
                {
                    if (fig.Deleted == false)
                    {
                        IHdEntity hdEnt = this.CreateEntity(fig);
                        if (hdEnt != null)
                        {
                            lstEnt.Add(hdEnt);
                        }
                    }
                }
                return lstEnt.ToArray();
            }
        }

        public void DisableUndoRecording(bool bDisable)
        {
            if (bDisable)
            {
                if (ActiveVcadDoc.UndoHistory.Enable == true)
                {
                    this.ActiveVcadDoc.UndoHistory.PushEnable(false);
                    this.ActiveVcadDoc.CommandAction.Undo("BEGIN");
                }
            }
            else
            {
                if (ActiveVcadDoc.UndoHistory.Enable == false)
                {
                    this.ActiveVcadDoc.CommandAction.Undo("END");
                    this.ActiveVcadDoc.UndoHistory.PushEnable(true);
                }
            }
        }

        public IHdLayer GetLayer(string strName)
        {
            IHdLayer hdlayer = null;
            if (this.ActiveVcadDoc != null)
            {
                foreach (vdLayer vdlayer in this.ActiveVcadDoc.Layers)
                {
                    if (vdlayer.Name == strName)
                    {
                        hdlayer = new CVcadLayer(vdlayer);
                        break;
                    }
                }
            }
            return hdlayer;
        }

        public IHdLayer AddLayer(string strName)
        {
            IHdLayer hdlayer = this.GetLayer(strName);
            if (hdlayer == null)
            {

                vdLayer vdlayer = this.ActiveVcadDoc.Layers.Add(strName);
                hdlayer = new CVcadLayer(vdlayer);
            }

            return hdlayer;
        }

        public IHdLayer GetActiveLayer()
        {
            IHdLayer hdlayer = null;
            if (this.ActiveVcadDoc != null)
            {
                hdlayer = new CVcadLayer(this.ActiveVcadDoc.ActiveLayer);
            }

            return hdlayer;
        }

        public IHdPoint DrawPoint(HeadsUtils.CPoint3D pt)
        {
            //We will create a vdPoint object and add it to the Active Layout which is the basic Model Layout always existing in a Document.
            VectorDraw.Professional.vdFigures.vdPoint onepoint = new VectorDraw.Professional.vdFigures.vdPoint();
            //We set the document where the point is going to be added.This is important for the vdPoint in order to obtain initial properties with setDocumentDefaults.
            onepoint.SetUnRegisterDocument(this.ActiveVcadDoc);
            onepoint.setDocumentDefaults();

            //The two previous steps are important if a vdFigure object is going to be added to a document.
            //Now we will change some properties of the point.
            onepoint.InsertionPoint = GeneraHelper.GetVDPoint(pt);

            //Now we will add this object to the Entities collection of the Model Layout(ActiveLayout).
            this.ActiveVcadDoc.ActiveLayOut.Entities.AddItem(onepoint);

            //Redraw the document to see the above changes.
            //this.ActiveVcadDoc.Redraw(true);

            return new CVcadPoint(onepoint);
        }

        public IHdLine DrawLine(HeadsUtils.CPoint3D ptFrom, HeadsUtils.CPoint3D ptTo)
        {
            //We will create a vdLine object and add it to the Active Layout which is the basic Model Layout always existing in a Document.
            VectorDraw.Professional.vdFigures.vdLine oneline = new VectorDraw.Professional.vdFigures.vdLine();
            //We set the document where the line is going to be added.This is important for the vdLine in order to obtain initial properties with setDocumentDefaults.
            oneline.SetUnRegisterDocument(this.ActiveVcadDoc);
            oneline.setDocumentDefaults();

            //The two previous steps are important if a vdFigure object is going to be added to a document.
            //Now we will change some properties of the line.
            oneline.StartPoint = GeneraHelper.GetVDPoint(ptFrom);
            oneline.EndPoint = GeneraHelper.GetVDPoint(ptTo);

            //Now we will add this object to the Entities collection of the Model Layout(ActiveLayout).
            this.ActiveVcadDoc.ActiveLayOut.Entities.AddItem(oneline);

            //Redraw the document to see the above changes.
            //this.ActiveVcadDoc.Redraw(true);

            return new CVcadLine(oneline);
        }

        public IHdArc DrawArc(HeadsUtils.CPoint3D ptCenter, double radius, double startAngle, double endAngle)
        {
            //We will create a vdArc object and add it to the Active Layout which is the basic Model Layout always existing in a Document.
            VectorDraw.Professional.vdFigures.vdArc onearc = new VectorDraw.Professional.vdFigures.vdArc();
            //We set the document where the arc is going to be added.This is important for the vdArc in order to obtain initial properties with setDocumentDefaults.
            onearc.SetUnRegisterDocument(this.ActiveVcadDoc);
            onearc.setDocumentDefaults();

            //The two previous steps are important if a vdFigure object is going to be added to a document.
            //Now we will change some properties of the arc.
            onearc.Center = GeneraHelper.GetVDPoint(ptCenter);
            onearc.Radius = radius;
            onearc.StartAngle = startAngle;
            onearc.EndAngle = endAngle;

            //Now we will add this object to the Entities collection of the Model Layout(ActiveLayout).
            this.ActiveVcadDoc.ActiveLayOut.Entities.AddItem(onearc);

            //Redraw the document to see the above changes.
            //this.ActiveVcadDoc.Redraw(true);
            return new CVcadArc(onearc);
        }

        public IHdText DrawText(HeadsUtils.CPoint3D pt, string strText, double height)
        {
            //We will create a vdText object and add it to the Active Layout which is the basic Model Layout always existing in a Document.
            VectorDraw.Professional.vdFigures.vdText onetext = new VectorDraw.Professional.vdFigures.vdText();
            //We set the document where the text is going to be added.This is important for the vdText in order to obtain initial properties with setDocumentDefaults.
            onetext.SetUnRegisterDocument(this.ActiveVcadDoc);
            onetext.setDocumentDefaults();

            //The two previous steps are important if a vdFigure object is going to be added to a document.
            //Now we will change some properties of the text.

            onetext.TextString = strText;
            //We set the insertion point depending the width of the Text from the vdFigure's BoundingBox
            onetext.InsertionPoint = GeneraHelper.GetVDPoint(pt);

            onetext.Height = height;

            //Now we will add this object to the Entities collection of the Model Layout(ActiveLayout).
            this.ActiveVcadDoc.ActiveLayOut.Entities.AddItem(onetext);

            //Redraw the document to see the above changes.
            //this.ActiveVcadDoc.Redraw(true);

            return new CVcadText(onetext);
        }

        public IHdEllipse DrawEllipse(CPoint3D center, double dRadius)
        {
            //We will create a vdCircle object and add it to the Active Layout which is the basic Model Layout always existing in a Document.
            VectorDraw.Professional.vdFigures.vdCircle onecircle = new VectorDraw.Professional.vdFigures.vdCircle();
            //We set the document where the circle is going to be added.This is important for the vdCircle in order to obtain initial properties with setDocumentDefaults.
            onecircle.SetUnRegisterDocument(this.ActiveVcadDoc);
            onecircle.setDocumentDefaults();

            //The two previous steps are important if a vdFigure object is going to be added to a document.
            //Now we will change some properties of the circle.
            onecircle.Center = GeneraHelper.GetVDPoint(center);
            onecircle.Radius = dRadius;

            //Now we will add this object to the Entities collection of the Model Layout(ActiveLayout).
            this.ActiveVcadDoc.ActiveLayOut.Entities.AddItem(onecircle);

            //Redraw the document to see the above changes.
            //this.ActiveVcadDoc.Redraw(true);

            return new CVcadEllipse(onecircle);
        }

        public IHdPolyline3D DrawPolyline3D(List<HeadsUtils.CPoint3D> ptList)
        {
            //We will create a vdPolyline object and add it to the Active Layout which is the basic Model Layout always existing in a Document.
            VectorDraw.Professional.vdFigures.vdPolyline onepoly = new VectorDraw.Professional.vdFigures.vdPolyline();
            //We set the document where the polyline is going to be added.This is important for the vdPolyline in order to obtain initial properties with setDocumentDefaults.
            onepoly.SetUnRegisterDocument(this.ActiveVcadDoc);
            onepoly.setDocumentDefaults();

            //The two previous steps are important if a vdFigure object is going to be added to a document.
            //Now we will change some properties of the polyline.
            foreach (CPoint3D pt in ptList)
            {
                onepoly.VertexList.Add(GeneraHelper.GetVDPoint(pt));
            }

            //Now we will add this object to the Entities collection of the Model Layout(ActiveLayout).
            this.ActiveVcadDoc.ActiveLayOut.Entities.AddItem(onepoly);

            //Redraw the document to see the above changes.
            //this.ActiveVcadDoc.Redraw(true);

            return new CVcadPolyLine(onepoly);
        }

        public IHd3DFace Draw3DFace(HeadsUtils.CPoint3D pt1, HeadsUtils.CPoint3D pt2, HeadsUtils.CPoint3D pt3, HeadsUtils.CPoint3D pt4)
        {
            //We will create a vdPolyface object and add it to the Active Layout which is the basic Model Layout always existing in a Document.
            VectorDraw.Professional.vdFigures.vd3DFace one3dface = new VectorDraw.Professional.vdFigures.vd3DFace();
            //We set the document where the polyface is going to be added.This is important for the vdPolyface in order to obtain initial properties with setDocumentDefaults.
            one3dface.SetUnRegisterDocument(this.ActiveVcadDoc);
            one3dface.setDocumentDefaults();

            VectorDraw.Geometry.gPoints gpts = new VectorDraw.Geometry.gPoints();
            gpts.Add(GeneraHelper.GetVDPoint(pt1));
            gpts.Add(GeneraHelper.GetVDPoint(pt2));
            gpts.Add(GeneraHelper.GetVDPoint(pt3));
            gpts.Add(GeneraHelper.GetVDPoint(pt4));

            one3dface.VertexList = gpts;

            //Now we will add this object to the Entities collection of the Model Layout(ActiveLayout).
            this.ActiveVcadDoc.ActiveLayOut.Entities.AddItem(one3dface);

            //Redraw the document to see the above changes.
            //this.ActiveVcadDoc.Redraw(true);

            return new CVcad3DFace(one3dface);

        }

        public IHdEntity[] GetSelectionSet()
        {
            List<IHdEntity> hdentities = new List<IHdEntity>();

            if (this.ActiveVcadDoc.Selections.Count > 0)
            {
                foreach (vdSelection selection in this.ActiveVcadDoc.Selections)
                {
                    foreach (vdFigure fig in selection)
                    {
                        if (fig.Deleted == false)
                        {
                            hdentities.Add(this.CreateEntity(fig));
                        }
                    }
                }
            }
            return hdentities.ToArray();
        }

        public IHdEntity GetObjectById(int iID)
        {
            vdFigure fig = GeVDFigureByID(iID);

            IHdEntity hdentity = null;
            if (fig != null)
            {
                hdentity = this.CreateEntity(fig);
            }
            return hdentity;
        }

        public void RefreshDocument()
        {
            this.ActiveVcadDoc.Redraw(true);
        }

        public void RefreshDocument(bool bInvalidate)
        {
            this.VectorDrawCtrl.BaseControl.Refresh();
            this.ActiveVcadDoc.Redraw(true);
        }

        public IHdEntity[] GetUserSelection(string strPrompt)
        {
            List<IHdEntity> hdentities = new List<IHdEntity>();


            if (this.ActiveVcadDoc != null)
            {
                this.ActiveVcadDoc.Prompt(strPrompt);
                vdSelection selset = this.ActiveVcadDoc.ActionUtility.getUserSelection();
                this.ActiveVcadDoc.Prompt(null);

                if (selset != null)
                {
                    foreach (vdFigure fig in selset)
                    {
                        if (fig.Deleted == false)
                        {
                            hdentities.Add(this.CreateEntity(fig));
                        }
                    }
                }

            }
            return hdentities.ToArray();
        }

        public CPoint3D GetUserPoint(string strPrompt)
        {
            CPoint3D pt = null;
            VectorDraw.Geometry.gPoint userpoint;
            this.ActiveVcadDoc.Prompt(strPrompt);
            //The user can either click a point or type at the command line a point like 5,5,2
            VectorDraw.Actions.StatusCode ret = this.ActiveVcadDoc.ActionUtility.getUserPoint(out userpoint);
            if (this.ActiveVcadDoc != null)
            {
                this.ActiveVcadDoc.Prompt(null);
                if (ret == VectorDraw.Actions.StatusCode.Success)
                {
                    pt = new CPoint3D(userpoint.x, userpoint.y, userpoint.z);
                }
            }
            return pt;
        }

        public void SetPrompt(string strPrompt)
        {
            this.ActiveVcadDoc.Prompt(strPrompt);
        }

        public CBox BoundingBox
        {
            get
            {
                vdSelection set = this.ActiveVcadDoc.Selections.Add("BoundingBox");
                set.RemoveAll();
                set.Select(VectorDraw.Render.RenderSelect.SelectingMode.All, null);
                VectorDraw.Geometry.Box vdbox = set.GetBoundingBox();
                CBox box = null;
                if (vdbox != null)
                {
                    box = new CBox(new CPoint3D(vdbox.Max.x, vdbox.Max.y, vdbox.Max.z)
                    , new CPoint3D(vdbox.Min.x, vdbox.Min.y, vdbox.Min.z));
                }
                set.RemoveAll();
                return box;
            }

        }

        public HeadsUtils.CCfgtype ConfigParam
        {
            get
            {
                return this.configparam;
            }
            set
            {
                this.configparam = value;
            }
        }

        public void RemoveAllEntities()
        {
            this.ActiveVcadDoc.ActiveLayOut.Entities.RemoveAll();
        }
        #endregion

        private void Childform_MouseClick(object sender, MouseEventArgs e)
        {
            //MessageBox.Show(vdScrollableControl1.BaseControl.ActiveDocument.
        }



        #region Chiranjit 29 10 2009

        public frmMemberGrid MemberGrid
        {
            get
            {
                return fMembGrid;
            }
            set
            {
                fMembGrid = value;

            }
        }

        public frmPostProcess PostProcess
        {
            get
            {
                return fPostProc;
            }
            set
            {
                fPostProc = value;
            }
        }
        public frmMemberLoadCase MemberLoadcase
        {
            get
            {
                return fMembLoadCase;
            }
            set
            {
                fMembLoadCase = value;
            }
        }

        #endregion

        public enum WORK
        {
            Structure = 0,
            Loading = 1,
            PostProcess = 2,
        }



        #region IHeadsDocument Members
        public bool SetCurrentLayer(string layer_name)
        {
            try
            {
                vdLayer lay = ActiveVcadDoc.Layers.FindName(layer_name);

                if (lay == null)
                {
                    lay = new vdLayer();
                    lay.Name = layer_name;
                    lay.SetUnRegisterDocument(ActiveVcadDoc);
                    lay.setDocumentDefaults();
                    ActiveVcadDoc.Layers.AddItem(lay);
                }
                else if (lay.Deleted) lay.Deleted = false;


                //ActiveVcadDoc.ActiveLayer = ActiveVcadDoc.Layers.FindName(layer_name);
                ActiveVcadDoc.ActiveLayer = lay;

                return true;
            }
            catch (Exception ex) { }
            return false;
        }
        public int Search_Text(string search_str, int zoom_factor, int afterIndex)
        {
            try
            {
                vdDocument doc = vdScrollableControl1.BaseControl.ActiveDocument;

                for (int i = afterIndex; i < doc.ActiveLayOut.Entities.Count; i++)
                {
                    if (doc.ActiveLayOut.Entities[i] is vdMText)
                    {
                        vdMText m_text = doc.ActiveLayOut.Entities[i] as vdMText;
                        if (m_text.TextString.Contains(search_str))
                        {
                            gPoint p1 = new gPoint(m_text.BoundingBox.UpperLeft);
                            p1.x -= zoom_factor;
                            p1.y += zoom_factor;

                            gPoint p2 = new gPoint(m_text.BoundingBox.LowerRight);
                            p2.x += zoom_factor;
                            p2.y -= zoom_factor;
                            doc.CommandAction.Zoom("W", p1, p2);
                            return i;
                        }
                    }
                    else if (doc.ActiveLayOut.Entities[i] is vdText)
                    {
                        vdText m_text = doc.ActiveLayOut.Entities[i] as vdText;
                        if (m_text.TextString.Contains(search_str))
                        {
                            gPoint p1 = new gPoint(m_text.BoundingBox.UpperLeft);
                            p1.x -= zoom_factor;
                            p1.y += zoom_factor;

                            gPoint p2 = new gPoint(m_text.BoundingBox.LowerRight);
                            p2.x += zoom_factor;
                            p2.y -= zoom_factor;
                            doc.CommandAction.Zoom("W", p1, p2);
                            return i;
                        }
                    }
                }
            }
            catch (Exception ex) { }
            return -1;
        }

        public int Search_Text_2017_07_20(string search_str, int zoom_factor, int afterIndex, bool match_case, bool match_whole_word, out string find_text)
        {
            try
            {
                find_text = "";
                vdDocument doc = vdScrollableControl1.BaseControl.ActiveDocument;
                bool flag = false;
                for (int i = afterIndex; i < doc.ActiveLayOut.Entities.Count; i++)
                {
                    if (doc.ActiveLayOut.Entities[i] is vdMText)
                    {
                        vdMText m_text = doc.ActiveLayOut.Entities[i] as vdMText;

                        if (match_case)
                        {
                            if (match_whole_word)
                                flag = m_text.TextString == search_str;
                            else
                                flag = m_text.TextString.Contains(search_str);
                        }
                        else
                        {
                            if (match_whole_word)
                                flag = m_text.TextString.ToLower() == search_str.ToLower();
                            else
                                flag = m_text.TextString.ToLower().Contains(search_str.ToLower());
                        }

                        if (flag)
                        {

                            find_text = m_text.TextString;
                            //find_text = string.Format("{0}, (x:{1:f4} y:{2:f4} z:{3:f4} ",
                            //    m_text.TextString,m_text.InsertionPoint.x,m_text.InsertionPoint.y,
                            //    m_text.InsertionPoint.z);
                            gPoint p1 = new gPoint(m_text.BoundingBox.UpperLeft);
                            p1.x -= zoom_factor;
                            p1.y += zoom_factor;

                            gPoint p2 = new gPoint(m_text.BoundingBox.LowerRight);
                            p2.x += zoom_factor;
                            p2.y -= zoom_factor;
                            doc.CommandAction.Zoom("W", p1, p2);
                            return i;
                        }
                    }
                    else if (doc.ActiveLayOut.Entities[i] is vdText)
                    {
                        vdText m_text = doc.ActiveLayOut.Entities[i] as vdText;

                        if (match_case)
                        {
                            if (match_whole_word)
                                flag = m_text.TextString == search_str;
                            else
                                flag = m_text.TextString.Contains(search_str);
                        }
                        else
                        {
                            if (match_whole_word)
                                flag = m_text.TextString.ToLower() == search_str.ToLower();
                            else
                                flag = m_text.TextString.ToLower().Contains(search_str.ToLower());
                        }

                        if (flag)
                        {

                            if (fig != null)
                            {
                                fig.PenColor = fg_clr;
                                fig.Update();
                                vdScrollableControl1.BaseControl.ActiveDocument.Redraw(true);
                            }
                            fig = m_text;
                            //fg_clr = new vdColor();
                            fg_clr = m_text.PenColor.Clone() as vdColor;
                            blink = false;
                            tmr.Start();



                            find_text = m_text.TextString;
                            //find_text = string.Format("{0}, (x:{1:f4} y:{2:f4} z:{3:f4} ",
                            //   m_text.TextString, m_text.InsertionPoint.x, m_text.InsertionPoint.y,
                            //   m_text.InsertionPoint.z);
                            gPoint p1 = new gPoint(m_text.BoundingBox.UpperLeft);
                            p1.x -= zoom_factor;
                            p1.y += zoom_factor;

                            gPoint p2 = new gPoint(m_text.BoundingBox.LowerRight);
                            p2.x += zoom_factor;
                            p2.y -= zoom_factor;
                            doc.CommandAction.Zoom("W", p1, p2);
                            return i;
                        }
                    }
                    else if (doc.ActiveLayOut.Entities[i] is vdDimension)
                    {
                        vdDimension m_text = doc.ActiveLayOut.Entities[i] as vdDimension;

                        if (match_case)
                        {
                            if (match_whole_word)
                                flag = m_text.Measurement.ToString() == search_str;
                            else
                                flag = m_text.Measurement.ToString().Contains(search_str);
                        }
                        else
                        {
                            if (match_whole_word)
                                flag = m_text.Measurement.ToString().ToLower() == search_str.ToLower();
                            else
                                flag = m_text.Measurement.ToString().ToLower().Contains(search_str.ToLower());
                        }

                        if (flag)
                        {
                            find_text = m_text.Measurement.ToString();
                            //find_text = string.Format("{0}, (x:{1:f4} y:{2:f4} z:{3:f4} ",
                            //   m_text.TextString, m_text.InsertionPoint.x, m_text.InsertionPoint.y,
                            //   m_text.InsertionPoint.z);
                            gPoint p1 = new gPoint(m_text.BoundingBox.UpperLeft);
                            p1.x -= zoom_factor;
                            p1.y += zoom_factor;

                            gPoint p2 = new gPoint(m_text.BoundingBox.LowerRight);
                            p2.x += zoom_factor;
                            p2.y -= zoom_factor;
                            doc.CommandAction.Zoom("W", p1, p2);
                            return i;
                        }
                    }
                }
            }
            catch (Exception ex) { }

            find_text = "";
            return -1;
        }

        public int Search_Text(string search_str, int zoom_factor, int afterIndex, bool match_case, bool match_whole_word, out string find_text)
        {
            try
            {
                find_text = "";
                vdDocument doc = vdScrollableControl1.BaseControl.ActiveDocument;
                bool flag = false;
                for (int i = afterIndex; i < doc.ActiveLayOut.Entities.Count; i++)
                {
                    if (doc.ActiveLayOut.Entities[i] is vdMText)
                    {
                        vdMText m_text = doc.ActiveLayOut.Entities[i] as vdMText;

                        if (match_case)
                        {
                            if (match_whole_word)
                                flag = m_text.TextString == search_str;
                            else
                                flag = m_text.TextString.Contains(search_str);
                        }
                        else
                        {
                            if (match_whole_word)
                                flag = m_text.TextString.ToLower() == search_str.ToLower();
                            else
                                flag = m_text.TextString.ToLower().Contains(search_str.ToLower());
                        }

                        if (flag)
                        {

                            if (fig != null)
                            {
                                fig.PenColor = fg_clr;
                                fig.Update();
                                vdScrollableControl1.BaseControl.ActiveDocument.Redraw(true);
                            }
                            fig = m_text;
                            //fg_clr = new vdColor();
                            fg_clr = m_text.PenColor.Clone() as vdColor;
                            blink = false;
                            tmr.Start();


                            find_text = m_text.TextString;
                            //find_text = string.Format("{0}, (x:{1:f4} y:{2:f4} z:{3:f4} ",
                            //    m_text.TextString,m_text.InsertionPoint.x,m_text.InsertionPoint.y,
                            //    m_text.InsertionPoint.z);
                            //gPoint p1 = new gPoint(m_text.BoundingBox.UpperLeft);
                            //p1.x -= zoom_factor;
                            //p1.y += zoom_factor;

                            //gPoint p2 = new gPoint(m_text.BoundingBox.LowerRight);
                            //p2.x += zoom_factor;
                            //p2.y -= zoom_factor;
                            //doc.CommandAction.Zoom("W", p1, p2);

                            doc.CommandAction.Zoom("W", m_text.InsertionPoint, m_text.InsertionPoint);

                            return i;
                        }
                    }
                    else if (doc.ActiveLayOut.Entities[i] is vdText)
                    {
                        vdText m_text = doc.ActiveLayOut.Entities[i] as vdText;

                        if (match_case)
                        {
                            if (match_whole_word)
                                flag = m_text.TextString == search_str;
                            else
                                flag = m_text.TextString.Contains(search_str);
                        }
                        else
                        {
                            if (match_whole_word)
                                flag = m_text.TextString.ToLower() == search_str.ToLower();
                            else
                                flag = m_text.TextString.ToLower().Contains(search_str.ToLower());
                        }

                        if (flag)
                        {

                            if (fig != null)
                            {
                                fig.PenColor = fg_clr;
                                fig.Update();
                                vdScrollableControl1.BaseControl.ActiveDocument.Redraw(true);
                            }
                            fig = m_text;
                            //fg_clr = new vdColor();
                            fg_clr = m_text.PenColor.Clone() as vdColor;
                            blink = false;
                            tmr.Start();



                            find_text = m_text.TextString;
                            //find_text = string.Format("{0}, (x:{1:f4} y:{2:f4} z:{3:f4} ",
                            //   m_text.TextString, m_text.InsertionPoint.x, m_text.InsertionPoint.y,
                            //   m_text.InsertionPoint.z);
                            //gPoint p1 = new gPoint(m_text.BoundingBox.UpperLeft);
                            //p1.x -= zoom_factor;
                            //p1.y += zoom_factor;

                            //gPoint p2 = new gPoint(m_text.BoundingBox.LowerRight);
                            //p2.x += zoom_factor;
                            //p2.y -= zoom_factor;
                            //doc.CommandAction.Zoom("W", p1, p2);


                            //doc.CommandAction.Zoom("W", p1, p2);
                            //doc.ViewCenter = m_text.InsertionPoint;

                            doc.CommandAction.Zoom("W", m_text.InsertionPoint, m_text.InsertionPoint);
                         
                            //doc.ZoomAll();


                            return i;
                        }
                    }
                    else if (doc.ActiveLayOut.Entities[i] is vdDimension)
                    {
                        vdDimension m_text = doc.ActiveLayOut.Entities[i] as vdDimension;

                        if (match_case)
                        {
                            if (match_whole_word)
                                flag = m_text.Measurement.ToString() == search_str;
                            else
                                flag = m_text.Measurement.ToString().Contains(search_str);
                        }
                        else
                        {
                            if (match_whole_word)
                                flag = m_text.Measurement.ToString().ToLower() == search_str.ToLower();
                            else
                                flag = m_text.Measurement.ToString().ToLower().Contains(search_str.ToLower());
                        }

                        if (flag)
                        {


                            if (fig != null)
                            {
                                fig.PenColor = fg_clr;
                                fig.Update();
                                vdScrollableControl1.BaseControl.ActiveDocument.Redraw(true);
                            }
                            fig = m_text;
                            //fg_clr = new vdColor();
                            fg_clr = m_text.PenColor.Clone() as vdColor;
                            blink = false;
                            tmr.Start();



                            find_text = m_text.Measurement.ToString();
                            //find_text = string.Format("{0}, (x:{1:f4} y:{2:f4} z:{3:f4} ",
                            //   m_text.TextString, m_text.InsertionPoint.x, m_text.InsertionPoint.y,
                            //   m_text.InsertionPoint.z);
                            //gPoint p1 = new gPoint(m_text.BoundingBox.UpperLeft);
                            //p1.x -= zoom_factor;
                            //p1.y += zoom_factor;

                            //gPoint p2 = new gPoint(m_text.BoundingBox.LowerRight);
                            //p2.x += zoom_factor;
                            //p2.y -= zoom_factor;
                            //doc.CommandAction.Zoom("W", p1, p2);

                            doc.CommandAction.Zoom("W", m_text.DefPoint1, m_text.DefPoint1);

                            return i;
                        }
                    }
                }
            }
            catch (Exception ex) { }

            find_text = "";
            return -1;
        }

        Timer tmr = new Timer();
        vdFigure fig = null;
        vdColor fg_clr = null;
        bool blink = false;
        public object Search_Entity(string search_str, int zoom_factor, int afterIndex, bool match_case, bool match_whole_word, out string find_text)
        {
            
            try
            {
                find_text = "";
                vdDocument doc = vdScrollableControl1.BaseControl.ActiveDocument;
                bool flag = false;
                for (int i = afterIndex; i < doc.ActiveLayOut.Entities.Count; i++)
                {
                    if (doc.ActiveLayOut.Entities[i] is vdMText)
                    {
                        vdMText m_text = doc.ActiveLayOut.Entities[i] as vdMText;

                        if (match_case)
                        {
                            if (match_whole_word)
                                flag = m_text.TextString == search_str;
                            else
                                flag = m_text.TextString.Contains(search_str);
                        }
                        else
                        {
                            if (match_whole_word)
                                flag = m_text.TextString.ToLower() == search_str.ToLower();
                            else
                                flag = m_text.TextString.ToLower().Contains(search_str.ToLower());
                        }

                        if (flag)
                        {

                            find_text = m_text.TextString;
                            //find_text = string.Format("{0}, (x:{1:f4} y:{2:f4} z:{3:f4} ",
                            //    m_text.TextString,m_text.InsertionPoint.x,m_text.InsertionPoint.y,
                            //    m_text.InsertionPoint.z);
                            gPoint p1 = new gPoint(m_text.BoundingBox.UpperLeft);
                            p1.x -= zoom_factor;
                            p1.y += zoom_factor;

                            gPoint p2 = new gPoint(m_text.BoundingBox.LowerRight);
                            p2.x += zoom_factor;
                            p2.y -= zoom_factor;
                            doc.CommandAction.Zoom("W", p1, p2);
                            return i;
                        }
                    }
                    else if (doc.ActiveLayOut.Entities[i] is vdText)
                    {
                        vdText m_text = doc.ActiveLayOut.Entities[i] as vdText;

                        if (match_case)
                        {
                            if (match_whole_word)
                                flag = m_text.TextString == search_str;
                            else
                                flag = m_text.TextString.Contains(search_str);
                        }
                        else
                        {
                            if (match_whole_word)
                                flag = m_text.TextString.ToLower() == search_str.ToLower();
                            else
                                flag = m_text.TextString.ToLower().Contains(search_str.ToLower());
                        }

                        if (flag)
                        {
                            if (fig != null)
                            {
                                fig.PenColor = fg_clr;
                            }
                            fig = m_text;
                            fg_clr = new vdColor();
                            fg_clr = m_text.PenColor.Clone() as vdColor;


                            blink = false;


                            tmr.Start();
                            find_text = m_text.TextString;
                            //find_text = string.Format("{0}, (x:{1:f4} y:{2:f4} z:{3:f4} ",
                            //   m_text.TextString, m_text.InsertionPoint.x, m_text.InsertionPoint.y,
                            //   m_text.InsertionPoint.z);
                            gPoint p1 = new gPoint(m_text.BoundingBox.UpperLeft);
                            p1.x -= zoom_factor;
                            p1.y += zoom_factor;

                            gPoint p2 = new gPoint(m_text.BoundingBox.LowerRight);
                            p2.x += zoom_factor;
                            p2.y -= zoom_factor;
                            doc.CommandAction.Zoom("W", p1, p2);
                            return i;
                        }
                    }
                    else if (doc.ActiveLayOut.Entities[i] is vdDimension)
                    {
                        vdDimension m_text = doc.ActiveLayOut.Entities[i] as vdDimension;

                        if (match_case)
                        {
                            if (match_whole_word)
                                flag = m_text.Measurement.ToString() == search_str;
                            else
                                flag = m_text.Measurement.ToString().Contains(search_str);
                        }
                        else
                        {
                            if (match_whole_word)
                                flag = m_text.Measurement.ToString().ToLower() == search_str.ToLower();
                            else
                                flag = m_text.Measurement.ToString().ToLower().Contains(search_str.ToLower());
                        }

                        if (flag)
                        {
                            find_text = m_text.Measurement.ToString();
                            //find_text = string.Format("{0}, (x:{1:f4} y:{2:f4} z:{3:f4} ",
                            //   m_text.TextString, m_text.InsertionPoint.x, m_text.InsertionPoint.y,
                            //   m_text.InsertionPoint.z);
                            gPoint p1 = new gPoint(m_text.BoundingBox.UpperLeft);
                            p1.x -= zoom_factor;
                            p1.y += zoom_factor;

                            gPoint p2 = new gPoint(m_text.BoundingBox.LowerRight);
                            p2.x += zoom_factor;
                            p2.y -= zoom_factor;
                            doc.CommandAction.Zoom("W", p1, p2);
                            return i;
                        }
                    }
                }
            }
            catch (Exception ex) { }

            find_text = "";
            return -1;
        }

        public void Stop_Blink()
        {
            try
            {
                if (fig != null)
                {
                    fig.PenColor = fg_clr;
                    fig.Update();
                    vdScrollableControl1.BaseControl.ActiveDocument.Redraw(true);
                }

                fig = null;
                fg_clr = null;
                tmr.Stop();
            }
            catch (Exception exx) { }
        }



        void tmr_Tick(object sender, EventArgs e)
        {
            tmr.Interval = 500;
            if (fig != null)
            {
                if (blink)
                {
                    fig.PenColor = new vdColor(Color.Red);


                }
                else
                {
                    //fg_clr = fig.PenColor.Clone() as vdColor;
                    //fig.PenColor = new vdColor(Color.DarkMagenta);
                    fig.PenColor = fg_clr;
                }
                blink = !blink;
                fig.Update();

                vdScrollableControl1.BaseControl.ActiveDocument.Redraw(true);
                vdScrollableControl1.BaseControl.ActiveDocument.Update();
            }
        }



        public int Search_Text(string search_str)
        {
            try
            {
                vdDocument doc = vdScrollableControl1.BaseControl.ActiveDocument;

                for (int i = 0; i < doc.ActiveLayOut.Entities.Count; i++)
                {
                    if (doc.ActiveLayOut.Entities[i] is vdMText)
                    {
                        vdMText m_text = doc.ActiveLayOut.Entities[i] as vdMText;
                        if (m_text.TextString == search_str)
                        {
                            gPoint p1 = new gPoint(m_text.BoundingBox.UpperLeft);
                            p1.x -= 100;
                            p1.y += 100;

                            gPoint p2 = new gPoint(m_text.BoundingBox.LowerRight);
                            p2.x += 100;
                            p2.y -= 100;
                            doc.CommandAction.Zoom("W", p1, p2);
                            return i;
                        }
                    }
                    else if (doc.ActiveLayOut.Entities[i] is vdText)
                    {
                        vdText m_text = doc.ActiveLayOut.Entities[i] as vdText;
                        if (m_text.TextString == search_str)
                        {
                            gPoint p1 = new gPoint(m_text.BoundingBox.UpperLeft);
                            p1.x -= 10;
                            p1.y += 10;

                            gPoint p2 = new gPoint(m_text.BoundingBox.LowerRight);
                            p2.x += 10;
                            p2.y -= 10;
                            doc.CommandAction.Zoom("W", p1, p2);
                            return i;
                        }
                    }
                }
            }
            catch (Exception ex) { }
            return -1;
        }


        public int Replace_Text(string search_str, string replace_str, int zoom_factor, int afterIndex, bool match_case, bool match_whole_word, out string find_text)
        {
            try
            {
                find_text = "";
                vdDocument doc = vdScrollableControl1.BaseControl.ActiveDocument;
                bool flag = false;
                for (int i = afterIndex; i < doc.ActiveLayOut.Entities.Count; i++)
                {
                    if (doc.ActiveLayOut.Entities[i] is vdMText)
                    {
                        vdMText m_text = doc.ActiveLayOut.Entities[i] as vdMText;

                        if (match_case)
                        {
                            if (match_whole_word)
                                flag = m_text.TextString == search_str;
                            else
                                flag = m_text.TextString.Contains(search_str);
                        }
                        else
                        {
                            if (match_whole_word)
                                flag = m_text.TextString.ToLower() == search_str.ToLower();
                            else
                                flag = m_text.TextString.ToLower().Contains(search_str.ToLower());
                        }

                        if (flag)
                        {

                            //m_text.TextString = m_text.TextString.Replace(search_str, replace_str);



                            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"" + search_str + "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                            if (match_case) reg = new System.Text.RegularExpressions.Regex(@"" + search_str + "", System.Text.RegularExpressions.RegexOptions.None);
                            m_text.TextString = reg.Replace(m_text.TextString, replace_str, 1);



                            find_text = m_text.TextString;
                            //find_text = string.Format("{0}, (x:{1:f4} y:{2:f4} z:{3:f4} ",
                            //    m_text.TextString,m_text.InsertionPoint.x,m_text.InsertionPoint.y,
                            //    m_text.InsertionPoint.z);
                            //gPoint p1 = new gPoint(m_text.BoundingBox.UpperLeft);
                            //p1.x -= zoom_factor;
                            //p1.y += zoom_factor;

                            //gPoint p2 = new gPoint(m_text.BoundingBox.LowerRight);
                            //p2.x += zoom_factor;
                            //p2.y -= zoom_factor;
                            //doc.CommandAction.Zoom("W", p1, p2);


                            m_text.Update();
                            doc.Redraw(true);

                            return i;
                        }
                    }
                    else if (doc.ActiveLayOut.Entities[i] is vdText)
                    {
                        vdText m_text = doc.ActiveLayOut.Entities[i] as vdText;

                        if (match_case)
                        {
                            if (match_whole_word)
                                flag = m_text.TextString == search_str;
                            else
                                flag = m_text.TextString.Contains(search_str);
                        }
                        else
                        {
                            if (match_whole_word)
                                flag = m_text.TextString.ToLower() == search_str.ToLower();
                            else
                                flag = m_text.TextString.ToLower().Contains(search_str.ToLower());
                        }

                        if (flag)
                        {
                            //m_text.TextString = m_text.TextString.Replace(search_str, replace_str);


                            //System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"[A-Za-z0-9\-]");
                            //System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"[" + search_str + "]", System.Text.RegularExpressions.RegexOptions.IgnoreCase);


                            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"" + search_str + "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                            if(match_case) reg = new System.Text.RegularExpressions.Regex(@"" + search_str + "", System.Text.RegularExpressions.RegexOptions.None);
                            m_text.TextString = reg.Replace(m_text.TextString, replace_str, 1);


                            m_text.Update();
                            doc.Redraw(true);
                            //if (match_case)
                            //{
                            //    m_text.TextString.Replace(search_str, replace_str);
                            //}
                            //else
                            //{
                            //    m_text.TextString.Replace(search_str, replace_str);
                            //}

                            find_text = m_text.TextString;
                            //find_text = string.Format("{0}, (x:{1:f4} y:{2:f4} z:{3:f4} ",
                            //   m_text.TextString, m_text.InsertionPoint.x, m_text.InsertionPoint.y,
                            //   m_text.InsertionPoint.z);
                            //gPoint p1 = new gPoint(m_text.BoundingBox.UpperLeft);
                            //p1.x -= zoom_factor;
                            //p1.y += zoom_factor;

                            //gPoint p2 = new gPoint(m_text.BoundingBox.LowerRight);
                            //p2.x += zoom_factor;
                            //p2.y -= zoom_factor;
                            //doc.CommandAction.Zoom("W", p1, p2);
                            return i;
                        }
                    }
                    else if (doc.ActiveLayOut.Entities[i] is vdDimension)
                    {
                        vdDimension m_text = doc.ActiveLayOut.Entities[i] as vdDimension;

                        if (match_case)
                        {
                            if (match_whole_word)
                                flag = m_text.Measurement.ToString() == search_str;
                            else
                                flag = m_text.Measurement.ToString().Contains(search_str);
                        }
                        else
                        {
                            if (match_whole_word)
                                flag = m_text.Measurement.ToString().ToLower() == search_str.ToLower();
                            else
                                flag = m_text.Measurement.ToString().ToLower().Contains(search_str.ToLower());
                        }

                        if (flag)
                        {
                            find_text = m_text.Measurement.ToString();



                            
                            //find_text = string.Format("{0}, (x:{1:f4} y:{2:f4} z:{3:f4} ",
                            //   m_text.TextString, m_text.InsertionPoint.x, m_text.InsertionPoint.y,
                            //   m_text.InsertionPoint.z);
                            //gPoint p1 = new gPoint(m_text.BoundingBox.UpperLeft);
                            //p1.x -= zoom_factor;
                            //p1.y += zoom_factor;

                            //gPoint p2 = new gPoint(m_text.BoundingBox.LowerRight);
                            //p2.x += zoom_factor;
                            //p2.y -= zoom_factor;
                            //doc.CommandAction.Zoom("W", p1, p2);
                            return i;
                        }
                    }
                }
            }
            catch (Exception ex) { }

            find_text = "";
            return -1;
        }
        public int Replace_All(string search_str, string replace_str, int zoom_factor, int afterIndex, bool match_case, bool match_whole_word, out string find_text)
        {
            int count = 0;
            try
            {
                Stop_Blink();

                bool find = false;
                find_text = "";
                vdDocument doc = vdScrollableControl1.BaseControl.ActiveDocument;
                bool flag = false;
                for (int i = afterIndex; i < doc.ActiveLayOut.Entities.Count; i++)
                {
                    if (doc.ActiveLayOut.Entities[i] is vdMText)
                    {
                        vdMText m_text = doc.ActiveLayOut.Entities[i] as vdMText;

                        if (match_case)
                        {
                            if (match_whole_word)
                                flag = m_text.TextString == search_str;
                            else
                                flag = m_text.TextString.Contains(search_str);
                        }
                        else
                        {
                            if (match_whole_word)
                                flag = m_text.TextString.ToLower() == search_str.ToLower();
                            else
                                flag = m_text.TextString.ToLower().Contains(search_str.ToLower());
                        }

                        if (flag)
                        {
                            count++;
                            find = true;
                            //m_text.TextString = m_text.TextString.Replace(search_str, replace_str);



                            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"" + search_str + "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                            if (match_case) reg = new System.Text.RegularExpressions.Regex(@"" + search_str + "", System.Text.RegularExpressions.RegexOptions.None);
                            m_text.TextString = reg.Replace(m_text.TextString, replace_str, 1);


                            m_text.Update();

                            //find_text = m_text.TextString;
                            ////find_text = string.Format("{0}, (x:{1:f4} y:{2:f4} z:{3:f4} ",
                            ////    m_text.TextString,m_text.InsertionPoint.x,m_text.InsertionPoint.y,
                            ////    m_text.InsertionPoint.z);
                            //gPoint p1 = new gPoint(m_text.BoundingBox.UpperLeft);
                            //p1.x -= zoom_factor;
                            //p1.y += zoom_factor;

                            //gPoint p2 = new gPoint(m_text.BoundingBox.LowerRight);
                            //p2.x += zoom_factor;
                            //p2.y -= zoom_factor;


                            //doc.CommandAction.Zoom("W", p1, p2);
                            //doc.CommandAction.Zoom("W", m_text.InsertionPoint, m_text.InsertionPoint);
                            //return i;
                        }
                    }
                    else if (doc.ActiveLayOut.Entities[i] is vdText)
                    {
                        vdText m_text = doc.ActiveLayOut.Entities[i] as vdText;

                        if (match_case)
                        {
                            if (match_whole_word)
                                flag = m_text.TextString == search_str;
                            else
                                flag = m_text.TextString.Contains(search_str);
                        }
                        else
                        {
                            if (match_whole_word)
                                flag = m_text.TextString.ToLower() == search_str.ToLower();
                            else
                                flag = m_text.TextString.ToLower().Contains(search_str.ToLower());
                        }

                        if (flag)
                        {
                            count++;
                            find = true;
                            //m_text.TextString = m_text.TextString.Replace(search_str, replace_str);

                            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"" + search_str + "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                            if (match_case) reg = new System.Text.RegularExpressions.Regex(@"" + search_str + "", System.Text.RegularExpressions.RegexOptions.None);
                            m_text.TextString = reg.Replace(m_text.TextString, replace_str, 1);


                            m_text.Update();

                            find_text = m_text.TextString;
                            //find_text = string.Format("{0}, (x:{1:f4} y:{2:f4} z:{3:f4} ",
                            //   m_text.TextString, m_text.InsertionPoint.x, m_text.InsertionPoint.y,
                            //   m_text.InsertionPoint.z);
                            //gPoint p1 = new gPoint(m_text.BoundingBox.UpperLeft);
                            //p1.x -= zoom_factor;
                            //p1.y += zoom_factor;

                            //gPoint p2 = new gPoint(m_text.BoundingBox.LowerRight);
                            //p2.x += zoom_factor;
                            //p2.y -= zoom_factor;

                            //doc.CommandAction.Zoom("W", p1, p2);
                            //doc.CommandAction.Zoom("W", m_text.InsertionPoint, m_text.InsertionPoint);
                            //return i;
                        }
                    }
                    else if (doc.ActiveLayOut.Entities[i] is vdDimension)
                    {
                        vdDimension m_text = doc.ActiveLayOut.Entities[i] as vdDimension;

                        if (match_case)
                        {
                            if (match_whole_word)
                                flag = m_text.Measurement.ToString() == search_str;
                            else
                                flag = m_text.Measurement.ToString().Contains(search_str);
                        }
                        else
                        {
                            if (match_whole_word)
                                flag = m_text.Measurement.ToString().ToLower() == search_str.ToLower();
                            else
                                flag = m_text.Measurement.ToString().ToLower().Contains(search_str.ToLower());
                        }

                        if (flag)
                        {
                            find = true;
                            find_text = m_text.Measurement.ToString();
                            //find_text = string.Format("{0}, (x:{1:f4} y:{2:f4} z:{3:f4} ",
                            //   m_text.TextString, m_text.InsertionPoint.x, m_text.InsertionPoint.y,
                            //   m_text.InsertionPoint.z);
                            //gPoint p1 = new gPoint(m_text.BoundingBox.UpperLeft);
                            //p1.x -= zoom_factor;
                            //p1.y += zoom_factor;

                            //gPoint p2 = new gPoint(m_text.BoundingBox.LowerRight);
                            //p2.x += zoom_factor;
                            //p2.y -= zoom_factor;
                            //doc.CommandAction.Zoom("W", p1, p2);
                            return i;
                        }
                    }
                }

                if (find)
                {
                    find_text = count + " items are Replaced.";

                    vdScrollableControl1.BaseControl.ActiveDocument.Redraw(true);

                    return 1;
                }
            }
            catch (Exception ex) { }

            find_text = "";

            return -1;
        }

        #endregion

    }

}