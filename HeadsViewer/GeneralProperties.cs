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
using VectorDraw.Professional.vdCollections;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.Actions;
using VectorDraw.Render;
using System.IO;

namespace HeadsViewer
{
    /// <summary>
    /// This form is used in order to save and load general drawing/application properties.In this form we use a vdDocument(no graphics) component
    /// in order to load and save drawing properties and then copy these to any new opened vdScrollable control.
    /// </summary>
    public partial class GeneralProperties : Form
    {
        public enum Side { Left, Right }
        private vdDocument mDoc = null;
        private static VectorDraw.Professional.Components.vdDocumentComponent vdDocumentComponent1 = null;
        public DocumentProperties mDocumentProperties = null;
        private string PropsFilename = "GeneralProps.txt";
        private Side mside = Side.Left;
        /// <summary>
        /// Initialization of the form with tha passed parameters.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="ResourcesPath"></param>
        /// <param name="PropertiesPanel"></param>
        public void Initialize(vdDocument doc,string ResourcesPath, Panel PropertiesPanel)
        {
            if (PropertiesPanel.Dock == DockStyle.Left) mside = Side.Left;
            else mside = Side.Right;

            if (vdDocumentComponent1 == null)
            {
                vdDocumentComponent1 = new VectorDraw.Professional.Components.vdDocumentComponent();
                vdDocumentComponent1.Document.UndoHistory.PushEnable(false);
                LoadPropsFromFile(ResourcesPath);
            }
            mDoc = doc;

            mDocumentProperties = new DocumentProperties(vdDocumentComponent1.Document,mside);
            
        }
        public GeneralProperties()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Show the properties at the property list on startup of the dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeneralProperties_Load(object sender, EventArgs e)
        {
            vdPropertyGrid1.SelectedObject = mDocumentProperties;
        }
        /// <summary>
        /// Closes the dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// On closing the form, load the selected properties to the passed(at form load) active document.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeneralProperties_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadProps(vdDocumentComponent1.Document, mDoc,true);
            mDoc.Redraw(false);
        }
        #region Save/Load Functions
        /// <summary>
        /// Writes the properties to a text stream.
        /// </summary>
        /// <param name="Resourcesdirectory"></param>
        public void SaveProps(string Resourcesdirectory)
        {
            StreamWriter stream = new StreamWriter(Resourcesdirectory + PropsFilename, false);
            stream.WriteLine("AxisSize:" + vdDocumentComponent1.Document.GlobalRenderProperties.AxisSize.ToString());
            stream.WriteLine("CrossSize:" + vdDocumentComponent1.Document.GlobalRenderProperties.CrossSize.ToString());
            stream.WriteLine("CursorAxisColor:" + vdDocumentComponent1.Document.GlobalRenderProperties.CursorAxisColor.ToArgb().ToString());
            stream.WriteLine("CursorPickColor:" + vdDocumentComponent1.Document.GlobalRenderProperties.CursorPickColor.ToArgb().ToString());
            stream.WriteLine("CurveResolution:" + vdDocumentComponent1.Document.GlobalRenderProperties.CurveResolution.ToString());
            stream.WriteLine("GridColor:" + vdDocumentComponent1.Document.GlobalRenderProperties.GridColor.ToArgb().ToString());
            stream.WriteLine("GripColor:" + vdDocumentComponent1.Document.GlobalRenderProperties.GripColor.ToArgb().ToString());
            stream.WriteLine("GripSize:" + vdDocumentComponent1.Document.GlobalRenderProperties.GripSize.ToString());
            stream.WriteLine("OsnapColor:" + vdDocumentComponent1.Document.GlobalRenderProperties.OsnapColor.ToArgb().ToString());
            stream.WriteLine("OsnapSize:" + vdDocumentComponent1.Document.GlobalRenderProperties.OsnapSize.ToString());
            stream.WriteLine("PickSize:" + vdDocumentComponent1.Document.GlobalRenderProperties.PickSize.ToString());
            stream.WriteLine("ReferenceCrossColor:" + vdDocumentComponent1.Document.GlobalRenderProperties.ReferenceCrossColor.ToArgb().ToString());
            stream.WriteLine("RubberBandColor:" + vdDocumentComponent1.Document.GlobalRenderProperties.RubberBandColor.ToArgb().ToString());
            stream.WriteLine("SelectingCrossColor:" + vdDocumentComponent1.Document.GlobalRenderProperties.SelectingCrossColor.ToArgb().ToString());
            stream.WriteLine("SelectingWindowColor:" + vdDocumentComponent1.Document.GlobalRenderProperties.SelectingWindowColor.ToArgb().ToString());
            stream.WriteLine("ShowCursor:" + vdDocumentComponent1.Document.GlobalRenderProperties.ShowCursor.ToString());
            stream.WriteLine("TimerBreakForDraw:" + vdDocumentComponent1.Document.GlobalRenderProperties.TimerBreakForDraw.ToString());

            stream.WriteLine("BackupOnSave:" + vdDocumentComponent1.Document.BackupOnSave.ToString());
            stream.WriteLine("ShowUCSAxis:" + vdDocumentComponent1.Document.ShowUCSAxis.ToString());
            stream.WriteLine("SupportPath:" + vdDocumentComponent1.Document.SupportPath);

            stream.WriteLine("PolarBkColor:" + vdDocumentComponent1.Document.PolarTrackDispProps.BkColor.ToArgb().ToString());
            stream.WriteLine("PolarFrameColor:" + vdDocumentComponent1.Document.PolarTrackDispProps.FrameColor.ToArgb().ToString());
            stream.WriteLine("PolarTextColor:" + vdDocumentComponent1.Document.PolarTrackDispProps.TextColor.ToArgb().ToString());
            stream.WriteLine("PolarFontStyle:" + vdDocumentComponent1.Document.PolarTrackDispProps.FontStyle.ToString());

            stream.WriteLine("ToolBkColor:" + vdDocumentComponent1.Document.ToolTipDispProps.BkColor.ToArgb().ToString());
            stream.WriteLine("ToolFrameColor:" + vdDocumentComponent1.Document.ToolTipDispProps.FrameColor.ToArgb().ToString());
            stream.WriteLine("ToolTextColor:" + vdDocumentComponent1.Document.ToolTipDispProps.TextColor.ToArgb().ToString());
            stream.WriteLine("ToolFontStyle:" + vdDocumentComponent1.Document.ToolTipDispProps.FontStyle.ToString());

            stream.WriteLine("EnableToolTips:" + vdDocumentComponent1.Document.EnableToolTips.ToString());
            stream.WriteLine("EnableUrls:" + vdDocumentComponent1.Document.EnableUrls.ToString());
            stream.WriteLine("BackGround:" + vdDocumentComponent1.Document.Palette.Background.ToArgb().ToString());
            stream.WriteLine("ForeGround:" + vdDocumentComponent1.Document.Palette.Forground.ToArgb().ToString());

            stream.WriteLine("PropertiesListSide:" + this.mside.ToString());
            //stream.WriteLine("MirrorText:" + vdDocumentComponent1.Document.Document.MirrorText.ToString());

            stream.Close();
        }
        /// <summary>
        /// Get a value from a line using the split character.This method is used when reading the properties saved file.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="splitchar"></param>
        /// <returns></returns>
        string GetValue(string value,char splitchar)
        {
            string[] strs = value.Split(new char[] { splitchar }, 2);
            if (strs.Length == 2)
                return strs[1].Trim();
            else
                throw new Exception("Error in loading Default VDFCAD values");
        }
        /// <summary>
        /// Get the font's properties from an already saved line.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        Font GetFont(string value)
        {
            string font = GetValue(value, ':');
            string str = GetValue(font, ':');
            string[] strs = str.Split(',');
            int counter = 0;
            string name = "";
            float size = 12;
            int unit = 3;
            byte charset = 0;
            bool Vertical = false;
            foreach (string var in strs)
            {
                switch (counter)
                {
                    case 0: name = GetValue(var, '='); break;
                    case 1: size = float.Parse(GetValue(var, '='),VectorDraw.Serialize.Activator.GetNumberFormat()); break;
                    case 2: unit = int.Parse(GetValue(var, '=')); break;
                    case 3: charset = byte.Parse(GetValue(var, '=')); break;
                    case 4: Vertical = bool.Parse(GetValue(var.TrimEnd(']'), '=')); break;
                }
                counter++;
            }
            return new Font(name, size, 0, (GraphicsUnit)unit, charset, Vertical);
        }
        /// <summary>
        /// Reads the text properties file and loads properties to the temporary vdDocument component.
        /// </summary>
        /// <param name="Resourcesdirectory"></param>
        public void LoadPropsFromFile(string Resourcesdirectory)
        {
            
            if (VectorDraw.Professional.Utilities.vdGlobals.IsFileExist(Resourcesdirectory + PropsFilename))
            {
                StreamReader stream = new StreamReader(Resourcesdirectory + PropsFilename);
                string line = "";
                while (!stream.EndOfStream)
                {
                    line = stream.ReadLine().Trim();

                    if (line.StartsWith("AxisSize:")) vdDocumentComponent1.Document.GlobalRenderProperties.AxisSize = int.Parse(GetValue(line, ':'));
                    else if (line.StartsWith("CrossSize:")) vdDocumentComponent1.Document.GlobalRenderProperties.CrossSize = int.Parse(GetValue(line, ':'));
                    else if (line.StartsWith("CursorAxisColor:")) vdDocumentComponent1.Document.GlobalRenderProperties.CursorAxisColor = System.Drawing.Color.FromArgb(int.Parse(GetValue(line, ':')));
                    else if (line.StartsWith("CursorPickColor:")) vdDocumentComponent1.Document.GlobalRenderProperties.CursorPickColor = System.Drawing.Color.FromArgb(int.Parse(GetValue(line, ':')));
                    else if (line.StartsWith("CurveResolution:")) vdDocumentComponent1.Document.GlobalRenderProperties.CurveResolution = int.Parse(GetValue(line, ':'));
                    else if (line.StartsWith("GridColor:")) vdDocumentComponent1.Document.GlobalRenderProperties.GridColor = System.Drawing.Color.FromArgb(int.Parse(GetValue(line, ':')));
                    else if (line.StartsWith("GripColor:")) vdDocumentComponent1.Document.GlobalRenderProperties.GripColor = System.Drawing.Color.FromArgb(int.Parse(GetValue(line, ':')));
                    else if (line.StartsWith("GripSize:")) vdDocumentComponent1.Document.GlobalRenderProperties.GripSize = int.Parse(GetValue(line, ':'));
                    else if (line.StartsWith("OsnapColor:")) vdDocumentComponent1.Document.GlobalRenderProperties.OsnapColor = System.Drawing.Color.FromArgb(int.Parse(GetValue(line, ':')));
                    else if (line.StartsWith("OsnapSize:")) vdDocumentComponent1.Document.GlobalRenderProperties.OsnapSize = int.Parse(GetValue(line, ':'));
                    else if (line.StartsWith("PickSize:")) vdDocumentComponent1.Document.GlobalRenderProperties.PickSize = int.Parse(GetValue(line, ':'));
                    else if (line.StartsWith("ReferenceCrossColor:")) vdDocumentComponent1.Document.GlobalRenderProperties.ReferenceCrossColor = System.Drawing.Color.FromArgb(int.Parse(GetValue(line, ':')));
                    else if (line.StartsWith("RubberBandColor:")) vdDocumentComponent1.Document.GlobalRenderProperties.RubberBandColor = System.Drawing.Color.FromArgb(int.Parse(GetValue(line, ':')));
                    else if (line.StartsWith("SelectingCrossColor:")) vdDocumentComponent1.Document.GlobalRenderProperties.SelectingCrossColor = System.Drawing.Color.FromArgb(int.Parse(GetValue(line, ':')));
                    else if (line.StartsWith("SelectingWindowColor:")) vdDocumentComponent1.Document.GlobalRenderProperties.SelectingWindowColor = System.Drawing.Color.FromArgb(int.Parse(GetValue(line, ':')));
                    else if (line.StartsWith("ShowCursor:")) vdDocumentComponent1.Document.GlobalRenderProperties.ShowCursor = bool.Parse(GetValue(line, ':'));
                    else if (line.StartsWith("TimerBreakForDraw:")) vdDocumentComponent1.Document.GlobalRenderProperties.TimerBreakForDraw = int.Parse(GetValue(line, ':'));

                    else if (line.StartsWith("BackupOnSave:")) vdDocumentComponent1.Document.Document.BackupOnSave = bool.Parse(GetValue(line, ':'));
                    else if (line.StartsWith("ShowUCSAxis:")) vdDocumentComponent1.Document.Document.ShowUCSAxis = bool.Parse(GetValue(line, ':'));
                    else if (line.StartsWith("SupportPath:")) vdDocumentComponent1.Document.Document.SupportPath = GetValue(line, ':');

                    else if (line.StartsWith("PolarBkColor:")) vdDocumentComponent1.Document.PolarTrackDispProps.BkColor = System.Drawing.Color.FromArgb(int.Parse(GetValue(line, ':')));
                    else if (line.StartsWith("PolarFrameColor:")) vdDocumentComponent1.Document.Document.PolarTrackDispProps.FrameColor= System.Drawing.Color.FromArgb(int.Parse(GetValue(line, ':')));
                    else if (line.StartsWith("PolarTextColor:"))  vdDocumentComponent1.Document.Document.PolarTrackDispProps.TextColor= System.Drawing.Color.FromArgb(int.Parse(GetValue(line, ':')));
                    else if (line.StartsWith("PolarFontStyle:"))  vdDocumentComponent1.Document.Document.PolarTrackDispProps.FontStyle =GetFont(line);

                    else if (line.StartsWith("ToolBkColor:")) vdDocumentComponent1.Document.Document.ToolTipDispProps.BkColor = System.Drawing.Color.FromArgb(int.Parse(GetValue(line, ':')));
                    else if (line.StartsWith("ToolFrameColor:")) vdDocumentComponent1.Document.Document.ToolTipDispProps.FrameColor = System.Drawing.Color.FromArgb(int.Parse(GetValue(line, ':')));
                    else if (line.StartsWith("ToolTextColor:")) vdDocumentComponent1.Document.Document.ToolTipDispProps.TextColor = System.Drawing.Color.FromArgb(int.Parse(GetValue(line, ':')));
                    else if (line.StartsWith("ToolFontStyle:")) vdDocumentComponent1.Document.Document.ToolTipDispProps.FontStyle = GetFont(line);

                    else if (line.StartsWith("EnableToolTips:"))  vdDocumentComponent1.Document.Document.EnableToolTips =bool.Parse(GetValue(line, ':'));
                    else if (line.StartsWith("EnableUrls:"))  vdDocumentComponent1.Document.Document.EnableUrls = bool.Parse(GetValue(line, ':'));
                    
                    else if (line.StartsWith("BackGround:")) vdDocumentComponent1.Document.Document.Palette.Background = System.Drawing.Color.FromArgb(int.Parse(GetValue(line, ':')));
                    else if (line.StartsWith("ForeGround:")) vdDocumentComponent1.Document.Document.Palette.Forground = System.Drawing.Color.FromArgb(int.Parse(GetValue(line, ':')));

                    else if (line.StartsWith("PropertiesListSide:"))
                    {
                        string val = GetValue(line, ':');
                        if (val == "Left")
                            this.mside = Side.Left;
                        else
                            this.mside = Side.Right;
                    }
                    
                }                
                stream.Close();
            }
        }
        /// <summary>
        /// Copies the properties from a document to another. Thsi method is used to copy the properties from the vdDocument component used temporary in this form to another document.
        /// </summary>
        /// <param name="From"></param>
        /// <param name="ToDoc"></param>
        public void LoadProps(vdDocument From, vdDocument ToDoc,bool loadpalette)
        {
            if (From == null) From = vdDocumentComponent1.Document;
            ToDoc.UndoHistory.PushEnable(false);

            ToDoc.GlobalRenderProperties.AxisSize = From.Document.GlobalRenderProperties.AxisSize;
            ToDoc.GlobalRenderProperties.CrossSize = From.Document.GlobalRenderProperties.CrossSize;
            ToDoc.GlobalRenderProperties.CursorAxisColor = From.Document.GlobalRenderProperties.CursorAxisColor;
            ToDoc.GlobalRenderProperties.CursorPickColor = From.Document.GlobalRenderProperties.CursorPickColor;
            ToDoc.GlobalRenderProperties.CurveResolution = From.Document.GlobalRenderProperties.CurveResolution;
            ToDoc.GlobalRenderProperties.GridColor = From.Document.GlobalRenderProperties.GridColor;
            ToDoc.Document.GlobalRenderProperties.GripColor = From.Document.GlobalRenderProperties.GripColor;
            ToDoc.Document.GlobalRenderProperties.GripSize = From.Document.GlobalRenderProperties.GripSize;
            ToDoc.GlobalRenderProperties.OsnapColor = From.Document.GlobalRenderProperties.OsnapColor;
            ToDoc.GlobalRenderProperties.OsnapSize = From.Document.GlobalRenderProperties.OsnapSize;
            ToDoc.GlobalRenderProperties.PickSize = From.Document.GlobalRenderProperties.PickSize;
            ToDoc.Document.GlobalRenderProperties.ReferenceCrossColor = From.Document.GlobalRenderProperties.ReferenceCrossColor;
            ToDoc.GlobalRenderProperties.RubberBandColor = From.Document.GlobalRenderProperties.RubberBandColor;
            ToDoc.GlobalRenderProperties.SelectingCrossColor = From.Document.GlobalRenderProperties.SelectingCrossColor;
            ToDoc.GlobalRenderProperties.SelectingWindowColor = From.Document.GlobalRenderProperties.SelectingWindowColor;
            ToDoc.GlobalRenderProperties.ShowCursor = From.Document.GlobalRenderProperties.ShowCursor;
            ToDoc.GlobalRenderProperties.TimerBreakForDraw = From.Document.GlobalRenderProperties.TimerBreakForDraw;

            ToDoc.Document.BackupOnSave = From.Document.BackupOnSave;
            ToDoc.Document.ShowUCSAxis = From.Document.ShowUCSAxis;
            ToDoc.Document.SupportPath = From.Document.SupportPath;

            ToDoc.PolarTrackDispProps.BkColor = From.Document.PolarTrackDispProps.BkColor;
            ToDoc.PolarTrackDispProps.FontStyle =new Font (From.PolarTrackDispProps.FontStyle, From.PolarTrackDispProps.FontStyle.Style);
            ToDoc.PolarTrackDispProps.FrameColor = From.PolarTrackDispProps.FrameColor;
            ToDoc.PolarTrackDispProps.TextColor = From.PolarTrackDispProps.TextColor;

            ToDoc.ToolTipDispProps.BkColor = From.ToolTipDispProps.BkColor;
            ToDoc.Document.ToolTipDispProps.FrameColor = From.Document.PolarTrackDispProps.FrameColor;
            ToDoc.Document.ToolTipDispProps.TextColor = From.Document.PolarTrackDispProps.TextColor;
            ToDoc.ToolTipDispProps.FontStyle = new Font(From.Document.ToolTipDispProps.FontStyle, From.Document.ToolTipDispProps.FontStyle.Style);

            ToDoc.Document.EnableToolTips = From.Document.EnableToolTips;
            ToDoc.Document.EnableUrls = From.Document.EnableUrls;

            if (loadpalette)
            {
                ToDoc.Document.Palette.Background = From.Document.Palette.Background;
                ToDoc.Document.Palette.Forground = From.Document.Palette.Forground;
            }
            ToDoc.UndoHistory.PopEnable();
            //ToDoc.Document.MirrorText = From.Document.MirrorText;
        }
        /// <summary>
        /// This method is used in order to obtain the background when opening a new drawing.
        /// </summary>
        /// <returns></returns>
        public static Color GetBackGround()
        {
            return vdDocumentComponent1.Document.Palette.Background;
        }

        #endregion
    }
    /// <summary>
    /// This class is used in order to have all the necessary properties that are going to be shown and changed from the user.
    /// </summary>
    public class DocumentProperties
    {

        public GeneralProperties.Side mSide = GeneralProperties.Side.Left;
        vdDocument mDocument;
        public DocumentProperties(vdDocument document,GeneralProperties.Side side)
        {
            mDocument = document;
            mSide = side;
        }
        [Category("Misc")]
        [DisplayName()]
        [Description()]
        public vdRenderGlobalProperties GlobalRenderProperties { get { return mDocument.GlobalRenderProperties; } }
        [Category()]
        [DisplayName()]
        [Description()]
        public bool MirrorText { get { return mDocument.MirrorText; } set { mDocument.MirrorText = value; } }
        [Category("Misc")]
        [DisplayName()]
        [Description()]
        public bool BackupOnSave { get { return mDocument.BackupOnSave; } set { mDocument.BackupOnSave  = value;} }
        [Category("Misc")]
        [DisplayName()]
        [Description()]
        public bool ShowUCSAxis { get { return mDocument.ShowUCSAxis; } set { mDocument.ShowUCSAxis = value; } }
        [Category("Misc")]
        [DisplayName()]
        [Description()]
        public string SupportPath { get { return mDocument.SupportPath; } set { mDocument.SupportPath = value; } }
        [Category("Extra Display Properties")]
        [DisplayName()]
        [Description()]
        public ToolTipDisplayProps PolarTrackDispProps { get { return mDocument.PolarTrackDispProps; } }
        [Category("Extra Display Properties")]
        [DisplayName()]
        [Description()]
        public ToolTipDisplayProps ToolTipDispProps { get { return mDocument.ToolTipDispProps; } }
        [Category("Extra Display Properties")]
        [DisplayName()]
        [Description()]
        public bool EnableTooltips { get { return mDocument.EnableToolTips; } set { mDocument.EnableToolTips = value; } }
        [Category("Extra Display Properties")]
        [DisplayName()]
        [Description()]
        public bool EnableUrls { get { return mDocument.EnableUrls;} set { mDocument.EnableUrls = value; } }
        [Category("Application Properties")]
        [DisplayName()]
        [Description()]
        public Color ForeGround { get { return mDocument.Palette.Forground; } set { mDocument.Palette.Forground = value; } }
        [Category("Application Properties")]
        [DisplayName()]
        [Description()]
        public Color BackGround { get { return mDocument.Palette.Background; } set { mDocument.Palette.Background = value; } }
        [Category("Application Properties")]
        [DisplayName()]
        [Description()]
        public GeneralProperties.Side PropertiesListSide { get { return mSide; } set { mSide = value; } }

    }
    
}