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
    /// This is a class that produces a dll for extra methods that are called from the command line.
    /// </summary>
    public class Methods
    {
        /// <summary>
        /// This is a method that is used to diplay a custom message to the commandline.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="description"></param>
        /// <param name="value"></param>
        private static void AppendToHistory(vdDocument doc, string description, string value)
        {
            doc.Prompt(description);
            doc.PostCommandActionString(doc.ActionControl, value);
            doc.ActionUtility.getUserString();
            doc.Prompt(null);
        }

        #region General
        /// <summary>
        /// This method shows a messagebox when About command is called.
        /// </summary>
        /// <param name="doc"></param>
        public static void About(vdDocument doc)
        {
            System.Reflection.Assembly asm = typeof(VectorDraw.Professional.Control.VectorDrawBaseControl).Assembly;
            System.Reflection.AssemblyName asmname = new System.Reflection.AssemblyName(asm.FullName);
            System.Windows.Forms.MessageBox.Show("Vectordraw Developer Freamework CAD version : " + asmname.Version.ToString() + "\nNot for commercial purposes.\nThis application's source code is provided to all our VDF subscribers.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Calls the Pan method.
        /// </summary>
        /// <param name="doc"></param>
        public static void Pan(vdDocument doc)
        {
            doc.CommandAction.Pan();
        }
        #endregion

        #region FORMAT Menu List (dialogs)
        /// <summary>
        /// Shows the layers dialog.
        /// </summary>
        /// <param name="doc"></param>
        public static void ShowLayersDialog(vdDocument doc)
        {
            VectorDraw.Professional.Dialogs.LayersDialog.Show(doc);
        }
        /// <summary>
        /// Shows the Textstyle dialog.
        /// </summary>
        /// <param name="doc"></param>
        public static void ShowTextStylesDialog(vdDocument doc)
        {
            VectorDraw.Professional.Dialogs.frmTextStyle.Show(doc);
        }
        /// <summary>
        /// Shows the Dimension style dialog.
        /// </summary>
        /// <param name="doc"></param>
        public static void ShowDimensionStyle(vdDocument doc)
        {
            VectorDraw.Professional.Dialogs.frmDimStyle.Show(doc);
        }
        /// <summary>
        /// Shows the point style dialog.
        /// </summary>
        /// <param name="doc"></param>
        public static void ShowPointStyle(vdDocument doc)
        {
            VectorDraw.Professional.Dialogs.frmPointStyleDialog.Show(doc);
        }
        /// <summary>
        /// Shows the External references dialog.
        /// </summary>
        /// <param name="doc"></param>
        public static void ShowExternalReferencesDialog(vdDocument doc)
        {
            VectorDraw.Professional.Dialogs.frmXrefManager.Show(doc);
        }
        /// <summary>
        /// Shows the Images definitians dialog.
        /// </summary>
        /// <param name="doc"></param>
        public static void ShowImageDefinitionsDialog(vdDocument doc)
        {
            VectorDraw.Professional.Dialogs.FrmImageDefs.Show(doc);
        }
        /// <summary>
        /// Shows the light manager dialog.
        /// </summary>
        /// <param name="doc"></param>
        public static void ShowLightsDialog(vdDocument doc)
        {
            VectorDraw.Professional.Dialogs.frmLightManager.Show(doc);
        }
        /// <summary>
        /// Shows the osnap dialog.
        /// </summary>
        /// <param name="doc"></param>
        public static void ShowOsnapDialog(vdDocument doc)
        {
            VectorDraw.Professional.Dialogs.OSnapDialog.Show(doc, doc.ActionControl);
        }
        #endregion

        #region Utilities
        /// <summary>
        /// Implements the BringToFront command.
        /// </summary>
        /// <param name="doc"></param>
        public static void BringToFront(vdDocument doc)
        {
            doc.Prompt("Select a Figure");
            vdFigure fig;
            gPoint userpt;
            //This command waits until thew user clicks an entity.
            StatusCode code = doc.ActionUtility.getUserEntity(out fig, out userpt);
            doc.Prompt(null);
            if (code == StatusCode.Success)
            {
                if (fig != null)
                {
                    fig.Invalidate();
                    doc.ActiveLayOut.Entities.ChangeOrder(fig, false);
                    fig.Update();
                    fig.Invalidate();

                }
            }
        }
        /// <summary>
        /// Implements the SendToBack command.
        /// </summary>
        /// <param name="doc"></param>
        public static void SendToBack(vdDocument doc)
        {
            doc.Prompt("Select a Figure");
            vdFigure fig;
            gPoint userpt;
            //This command waits until thew user clicks an entity.
            StatusCode code = doc.ActionUtility.getUserEntity(out fig, out userpt);
            doc.Prompt(null);
            if (code == StatusCode.Success)
            {
                if (fig != null)
                {
                    fig.Invalidate();
                    doc.ActiveLayOut.Entities.ChangeOrder(fig, true);
                    fig.Update();
                    fig.Invalidate();
                }
            }
        }
        /// <summary>
        /// Implemets a command to get distances.
        /// </summary>
        /// <param name="doc"></param>
        public static void Distance(vdDocument doc)
        {
            doc.Prompt("Select Distance:");
            object end = doc.ActionUtility.getUserDist(null);
            doc.Prompt(null);
            if (end != null)
            {
                double distance = (double)end;
                AppendToHistory(doc ,"Distance : ", doc.lunits.FormatLength(distance));       
            }
        }
        /// <summary>
        /// Implements the ID command which shows the coordinates of a clicked point.
        /// </summary>
        /// <param name="doc"></param>
        public static void ID(vdDocument doc)
        {
            doc.Prompt("Pick a Point");
            gPoint pt;
            StatusCode code = doc.ActionUtility.getUserPoint(out pt);
            doc.Prompt(null);
            if (code == StatusCode.Success)
            {
                AppendToHistory(doc, "ID of Point:", "x:" + doc.lunits.FormatLength(pt.x) + " y:" + doc.lunits.FormatLength(pt.y) + " z:" + doc.lunits.FormatLength(pt.z));
            }
        }
        /// <summary>
        /// Implements the area command.
        /// </summary>
        /// <param name="doc"></param>
        public static void Area(vdDocument doc)
        {
            double area = 0.0;
            double length = 0.0;
            bool suc = doc.CommandAction.CmdArea(null, null,out area,out length);
            if (suc)
            {
                AppendToHistory(doc, "Area:", doc.lunits.FormatLength(area));
                AppendToHistory(doc, "Length:", doc.lunits.FormatLength(length));
            }
        }
        /// <summary>
        /// Shows the Purge dialog.
        /// </summary>
        /// <param name="doc"></param>
        public static void Purge(vdDocument doc)
        {
            frmPurge purge = new frmPurge();
            purge.InitializeDialog(doc);
            purge.ShowDialog(doc.ActionControl);
        }
        /// <summary>
        /// Implementation of the Slice command.
        /// </summary>
        /// <param name="doc"></param>
        public static void Slice(vdDocument doc)
        {
            doc.Prompt("Select a vdPolyface Figure");
            vdFigure fig;
            gPoint userpt;
            //This command waits until thew user clicks an entity.
            StatusCode code = doc.ActionUtility.getUserEntity(out fig, out userpt);
            doc.Prompt(null);
            if (code == StatusCode.Success)
            {
                vdPolyface face = fig as vdPolyface;
                if (face != null)
                {
                    doc.Prompt("Pick the Origin Point");
                    gPoint origin;
                    code = doc.ActionUtility.getUserPoint(out origin);
                    doc.Prompt(null);
                    if (code != StatusCode.Success) return;
                    doc.Prompt("Give the Slice Direction Vector");
                    gPoint vector;
                    code = doc.ActionUtility.getUserPoint(out vector);
                    doc.Prompt(null);
                    if (code != StatusCode.Success) return;
                    Vector v = new Vector(vector);
                    face.Invalidate();
                    face.Slice(origin, v);
                    face.Update();
                    face.Invalidate();
                }
            }
        }
        #endregion

        #region Osnaps
        /// <summary>
        /// Enables/Disables Osnap END.
        /// </summary>
        /// <param name="doc"></param>
        public static void OsnapEnd(vdDocument doc){doc.osnapMode |= OsnapMode.END;}
        /// <summary>
        /// Enables/Disables Osnap CENter.
        /// </summary>
        /// <param name="doc"></param>
        public static void OsnapCen(vdDocument doc){doc.osnapMode |= OsnapMode.CEN;}
        /// <summary>
        /// Enables/Disables Osnap MIDdle.
        /// </summary>
        /// <param name="doc"></param>
        public static void OsnapMid(vdDocument doc) { doc.osnapMode |= OsnapMode.MID; }
        /// <summary>
        /// Enables/Disables Osnap INSert.
        /// </summary>
        /// <param name="doc"></param>
        public static void OsnapIns(vdDocument doc) { doc.osnapMode |= OsnapMode.INS; }
        /// <summary>
        /// Enables/Disables Osnap PERpedicular.
        /// </summary>
        /// <param name="doc"></param>
        public static void OsnapPer(vdDocument doc) { doc.osnapMode |= OsnapMode.PER; }
        /// <summary>
        /// Enables/Disables Osnap NEArest.
        /// </summary>
        /// <param name="doc"></param>
        public static void OsnapNea(vdDocument doc) { doc.osnapMode |= OsnapMode.NEA; }
        /// <summary>
        /// Enables/Disables Osnap INTERSection.
        /// </summary>
        /// <param name="doc"></param>
        public static void OsnapInters(vdDocument doc) { doc.osnapMode |= OsnapMode.INTERS; }
        /// <summary>
        /// Enables/Disables Osnap NODE for points.
        /// </summary>
        /// <param name="doc"></param>
        public static void OsnapNode(vdDocument doc) { doc.osnapMode |= OsnapMode.NODE; }
        /// <summary>
        /// Enables/Disables Osnap QUArant.
        /// </summary>
        /// <param name="doc"></param>
        public static void OsnapQua(vdDocument doc) { doc.osnapMode |= OsnapMode.QUA; }
        /// <summary>
        /// Enables/Disables Osnap TANGent.
        /// </summary>
        /// <param name="doc"></param>
        public static void OsnapTang(vdDocument doc) { doc.osnapMode |= OsnapMode.TANG; }
        /// <summary>
        /// Enables/Disables Osnap APARENTINTersection.
        /// </summary>
        /// <param name="doc"></param>
        public static void OsnapApparentint(vdDocument doc) { doc.osnapMode |= OsnapMode.APPARENTINT; }
        /// <summary>
        /// Removes all osnaps.
        /// </summary>
        /// <param name="doc"></param>
        public static void OsnapCancel(vdDocument doc)
        {
            if (doc.ActionLayout.OverAllActiveActions != null && doc.ActionLayout.OverAllActiveActions.Count > 1)
                doc.osnapMode = OsnapMode.DISABLE;
            else
                doc.osnapMode = OsnapMode.NONE;
        }
        #endregion
        /// <summary>
        /// Shows the printer dialog in order to be used for PDF export.
        /// </summary>
        /// <param name="doc"></param>
        //public static void PDFExport(vdDocument doc)
        //{
        //    //doc.FileProperties.PDFExportProperties = vdFileProperties.PDFExportPropertiesFlags.UsePrinterPropertiesWithOutlineText;
        //    vdPrint printer = new vdPrint(doc.ActiveLayOut.Printer);
        //    printer.PrinterName = VectorDraw.Professional.Utilities.vdGlobals.GetFileNameWithoutExtension(doc.FileName) + ".pdf";
        //    printer.InitializePreviewFormProperties(true, true, false, false);
        //    printer.DialogPreview();
        //}

        #region Block Library
        /// <summary>
        /// Shows the blocklibrary Form.
        /// </summary>
        /// <param name="doc"></param>
        public static void LoadBlockLibrary(vdDocument doc)
        {
            BlockLibrary lib = new BlockLibrary();
            lib.InitializeDialog(doc);
            lib.ShowDialog(doc.ActionControl);
        }
        #endregion
    }
}
