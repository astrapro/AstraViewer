using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using HeadsFunctions1.Halignment;
using HeadsFunctions1.Valignment;
using System.Windows.Forms;
using System.IO;
using HeadsUtils;
using HeadsUtils.Interfaces;
using HeadsFunctions1.Properties;

namespace HeadsFunctions1
{
    public class CHeadsFunctionFactory : IHeadsFunctions
    {
        //static CHeadsFunctionFactory singleton;
        public CHeadsFunctionFactory()
        {

        }       

        #region WORKING DIRECTORY
        public IWorkingDirSelector CreateWkDirDialog(IHeadsApplication app)
        {
            IWorkingDirSelector workingdirselector = new HeadsFunctions1.WorkingFolder.FormWorkingFolderSelector();
            workingdirselector.WorkingFolderPath = app.AppDataPath;

            return workingdirselector;
        }
        #endregion

        #region DRAWSTRING
        public System.Windows.Forms.Form CreateDrawStringDialog(IHeadsApplication app)
        {
            HeadsFunctions1.DrawString.FormDrawString dlg = null;
            string strpathlistfile = Path.Combine(app.AppDataPath, "model.lst");
            string strpathfilfile = Path.Combine(app.AppDataPath, "model.fil");

            if (File.Exists(strpathlistfile) && File.Exists(strpathfilfile))
            {
                HeadsFunctions1.DrawString.FormDrawString formdrawstring = new HeadsFunctions1.DrawString.FormDrawString(app);
                dlg = formdrawstring;
            }
            else
            {
                MessageBox.Show(Resources.ST_PROMPT_LSTFIL_FILE_NOT_FOUND
                 , Resources.ST_ERROR_MSGBOX_CAPTION
                 , System.Windows.Forms.MessageBoxButtons.OK
                 , System.Windows.Forms.MessageBoxIcon.Error);
            }
            return dlg;
        }
        #endregion

        #region HALIGNMENT
        public System.Windows.Forms.Form CreateNewHalignHIPMethodDialog(IHeadsApplication app)
        {
            FormHipMethod formhipmethod = null;
            IHdEntity[] selections = app.ActiveDocument.GetUserSelection(Resources.ST_PROMPT_SELECT_POLYLINE);
            IHdPolyline3D polyline = null;
            if (selections.Length == 1)
            {
                IHdEntity entity = selections[0];
                //DictionaryEntry de = selections[0];
                if (entity.EntityName == "POLYLINE3D")
                {
                    polyline = (IHdPolyline3D)entity;
                }
            }       
            if (polyline != null)
            {
                formhipmethod = new FormHipMethod(app, polyline);
            }
            else
            {
                MessageBox.Show(Resources.ST_PROMPT_SELECT_POLYLINE, HeadsUtils.Constants.ProductName);

            }
            return formhipmethod;
        }

        public System.Windows.Forms.Form CreateNewHalignElementMethodDialog(IHeadsApplication app)
        {
            return new FormElementMethod(app);
        }
        
        public System.Windows.Forms.Form CreateOpenHIPMethodDialog(IHeadsApplication app, CHIPInfo info)
        {
            FormHipMethod formhipmethod = null;
            List<CHHipData> listHhip = CHalignHipUtil.GenerateHIPData(info);
            if (listHhip != null || listHhip.Count > 0)
            {
                formhipmethod = new FormHipMethod(app, info); 
            }
            else
            {
                MessageBox.Show("Error in Halign.fil file"
                    , Resources.ST_ERROR_MSGBOX_CAPTION
                    , System.Windows.Forms.MessageBoxButtons.OK
                    , System.Windows.Forms.MessageBoxIcon.Error);
            }
            return formhipmethod;
        }
        
        public IHalignFilSelector CreateHIPMethodSelectorDialog(IHeadsApplication app)
        {
            HeadsFunctions1.Halignment.FormOpenHipMethod formopenhipmethod = null;
            string strHalignFilePath = Path.Combine(app.AppDataPath, "HALIGN.FIL");
            if (File.Exists(strHalignFilePath))
            {
                formopenhipmethod = new FormOpenHipMethod(app, FormOpenHipMethod.OpenMode.HipMethod);
            }
            else
            {
                MessageBox.Show(Resources.ST_FILE_NOTFOUND + "\n" + strHalignFilePath
                 , Resources.ST_ERROR_MSGBOX_CAPTION
                 , System.Windows.Forms.MessageBoxButtons.OK
                 , System.Windows.Forms.MessageBoxIcon.Error);
            }

            return formopenhipmethod;
        }

        public IHalignFilSelector CreateElementMethodSelectorDialog(IHeadsApplication app)
        {
            HeadsFunctions1.Halignment.FormOpenHipMethod formopenhipmethod = null;
            string strHalignFilePath = Path.Combine(app.AppDataPath, "HALIGN.FIL");
            if (File.Exists(strHalignFilePath))
            {
                formopenhipmethod = new FormOpenHipMethod(app, FormOpenHipMethod.OpenMode.ElementMethod);
            }
            else
            {
                MessageBox.Show(Resources.ST_FILE_NOTFOUND + "\n" + strHalignFilePath
                 , Resources.ST_ERROR_MSGBOX_CAPTION
                 , System.Windows.Forms.MessageBoxButtons.OK
                 , System.Windows.Forms.MessageBoxIcon.Error);
            }
            return formopenhipmethod;
        }

        public System.Windows.Forms.Form CreateOpenElementMethodDialog(IHeadsApplication app, CHIPInfo info)
        {
            FormElementMethod formelementmethod = null;
            formelementmethod = new FormElementMethod(app, info);           
            return formelementmethod;
        }
        #endregion

        #region L-SECTION
        public System.Windows.Forms.Form CreateLSectionDialog(IHeadsApplication app)
        {
            HeadsFunctions1.LSection.FormLSection formlsection = null;
            string strpathlistfile = Path.Combine(app.AppDataPath, "model.lst");
            string strpathfilfile = Path.Combine(app.AppDataPath, "model.fil");

            if (File.Exists(strpathlistfile) && File.Exists(strpathfilfile))
            {
                formlsection = new HeadsFunctions1.LSection.FormLSection(app);
                //formlsection.IsDrawSection = true;
            }
            else
            {
                MessageBox.Show(Resources.ST_PROMPT_LSTFIL_FILE_NOT_FOUND
                 , Resources.ST_ERROR_MSGBOX_CAPTION
                 , System.Windows.Forms.MessageBoxButtons.OK
                 , System.Windows.Forms.MessageBoxIcon.Error);
            }            
            return formlsection;
        }
        
        public System.Windows.Forms.Form CreateLSecGridDialog(IHeadsApplication app)
        {
            return new LSection.FormLSectionGrid(app);
        }

        public void DeleteLSecGrid(IHeadsApplication app)
        {
            ViewerUtils.DeleteEntitiesByLabel(app.ActiveDocument, HeadsUtils.Constants.LABEL_GRID + ":LSec", true);
            app.ActiveDocument.RefreshDocument();
        }
        #endregion

        #region CONFIG PARAM
        public System.Windows.Forms.Form CreateConfigParamDialog(IHeadsApplication app)
        {
            return new ConfigParam.FormCfgParam(app);
        }
        #endregion

        #region CHAINAGE
        public System.Windows.Forms.Form CreateChainageDialog(IHeadsApplication app)
        {
            System.Windows.Forms.Form frmChainage = null;
            string strHalignTmpPath = Path.Combine(app.AppDataPath, "HALIGN.TMP");
            string strHalignFilPath = Path.Combine(app.AppDataPath, "HALIGN.FIL");
            if (File.Exists(strHalignTmpPath) || File.Exists(strHalignFilPath))
            {
                frmChainage = new Chainage.FormChainage(app);
            }
            else
            {
                MessageBox.Show(Properties.Resources.ST_FILE_NOTFOUND
                    , Properties.Resources.ST_ERROR_MSGBOX_CAPTION
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Information);
            }
            return frmChainage;
        }

        public void DeleteChainage(IHeadsApplication app)
        {
            ViewerUtils.DeleteEntitiesByLabel(app.ActiveDocument, HeadsUtils.Constants.LABEL_CHAINAGE, false);
            app.ActiveDocument.RefreshDocument();
        }
        #endregion

        #region DETAILS
        public System.Windows.Forms.Form CreateDetailsDialog(IHeadsApplication app)
        {
            System.Windows.Forms.Form frmDetails = null;

            string strHalignTmpPath = Path.Combine(app.AppDataPath, "HALIGN.TMP");
            string strHalignFilPath = Path.Combine(app.AppDataPath, "HALIGN.FIL");

            if (File.Exists(strHalignTmpPath) || File.Exists(strHalignFilPath))
            {
                frmDetails = new HeadsFunctions1.Details.FormDetails(app, false);
            }
            else
            {
                MessageBox.Show(Properties.Resources.ST_FILE_NOTFOUND
                    , Properties.Resources.ST_ERROR_MSGBOX_CAPTION
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Information);
            }
            return frmDetails;
        }

        public void DeleteDetails(IHeadsApplication app)
        {
            ViewerUtils.DeleteEntitiesByLabel(app.ActiveDocument, HeadsUtils.Constants.LABEL_DETAILS, false);
            app.ActiveDocument.RefreshDocument();
        }

        public System.Windows.Forms.Form CreateVaricalDetailsDialog(IHeadsApplication app)
        {
            System.Windows.Forms.Form frmDetails = null;

            string strTmpPath = Path.Combine(app.AppDataPath, "VALIGN.TMP");
            string strFilPath = Path.Combine(app.AppDataPath, "VALIGN.FIL");

            if (File.Exists(strTmpPath) || File.Exists(strFilPath))
            {
                frmDetails = new HeadsFunctions1.Details.FormDetails(app, true);
            }
            else
            {
                MessageBox.Show(Properties.Resources.ST_FILE_NOTFOUND
                    , Properties.Resources.ST_ERROR_MSGBOX_CAPTION
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Information);
            }
            return frmDetails;
        }
        #endregion

        #region COORDINATE
        public System.Windows.Forms.Form CreateCoordinateDialog(IHeadsApplication app)
        {
            System.Windows.Forms.Form frmCoordinate = null;

            if (app.ActiveDocument.Entities.Length > 0)
            {
                frmCoordinate = new HeadsFunctions1.Coordinates.FormCoordinates(app);
            }
            return frmCoordinate;
        }

        public void DeleteCoordinates(IHeadsApplication app)
        {
            ViewerUtils.DeleteEntitiesByLabel(app.ActiveDocument, HeadsUtils.Constants.LABEL_GRID, false);
            app.ActiveDocument.RefreshDocument();
        }
        #endregion

        #region VALIGNMENT
        public System.Windows.Forms.Form CreateNewValignDialog(IHeadsApplication app)
        {
            FormValignment formvalignment = null;
            //Only one polyline object is selected
            IHdEntity[] selections = app.ActiveDocument.GetUserSelection(Resources.ST_PROMPT_SELECT_POLYLINE);
            IHdPolyline3D polyline = null;
            if (selections.Length == 1)
            {
                IHdEntity entity = selections[0];
                //DictionaryEntry de = selections[0];
                if (entity.EntityName == "POLYLINE3D")
                {
                    polyline = (IHdPolyline3D)entity;
                }
            }               
            if (polyline != null)
            {
                formvalignment = new FormValignment(app, polyline);
            }
            else
            {
                MessageBox.Show(Resources.ST_PROMPT_SELECT_POLYLINE, HeadsUtils.Constants.ProductName);

            }
            return formvalignment;
        }

        public IValignFilSelector CreateValignSelectorDialog(IHeadsApplication app)
        {
            HeadsFunctions1.Valignment.FormOpenValignment formopenvalignmethod = null;
            string strValignFilePath = Path.Combine(app.AppDataPath, "VALIGN.FIL");
            if (File.Exists(strValignFilePath))
            {
                formopenvalignmethod = new FormOpenValignment(app);
            }
            else
            {
                MessageBox.Show(Resources.ST_FILE_NOTFOUND + "\n" + strValignFilePath
                 , Resources.ST_ERROR_MSGBOX_CAPTION
                 , System.Windows.Forms.MessageBoxButtons.OK
                 , System.Windows.Forms.MessageBoxIcon.Error);
            }

            return formopenvalignmethod;
        }

        public System.Windows.Forms.Form CreateOpenValignmentDialog(IHeadsApplication app, CValignInfo info)
        {
            FormValignment formvalignmethod = null;
            if (info != null || info.DataList.Count > 0)
            {
                formvalignmethod = new FormValignment(app, info);
            }
            else
            {
                MessageBox.Show("Error in Valign.fil file"
                    , Resources.ST_ERROR_MSGBOX_CAPTION
                    , System.Windows.Forms.MessageBoxButtons.OK
                    , System.Windows.Forms.MessageBoxIcon.Error);
            }
            return formvalignmethod;
        }
        #endregion

        #region MODELLING
        public System.Windows.Forms.Form CreateModellingHalignDialog(IHeadsApplication app)
        {
            HeadsFunctions1.Halignment.Modelling.FormModellingHalign form = null;
            string strHalignFilePath = Path.Combine(app.AppDataPath, "HALIGN.FIL");
            if (File.Exists(strHalignFilePath))
            {
                form = new HeadsFunctions1.Halignment.Modelling.FormModellingHalign(app);
            }
            else
            {
                MessageBox.Show(Resources.ST_FILE_NOTFOUND + "\n" + strHalignFilePath
                 , Resources.ST_ERROR_MSGBOX_CAPTION
                 , System.Windows.Forms.MessageBoxButtons.OK
                 , System.Windows.Forms.MessageBoxIcon.Error);
            }

            return form;
        }

        public System.Windows.Forms.Form CreateModellingValignDialog(IHeadsApplication app)
        {
            HeadsFunctions1.Valignment.Modelling.FormModellingValign form = null;
            string strValignFilePath = Path.Combine(app.AppDataPath, "VALIGN.FIL");
            if (File.Exists(strValignFilePath))
            {
                form = new HeadsFunctions1.Valignment.Modelling.FormModellingValign(app);
            }
            else
            {
                MessageBox.Show(Resources.ST_FILE_NOTFOUND + "\n" + strValignFilePath
                 , Resources.ST_ERROR_MSGBOX_CAPTION
                 , System.Windows.Forms.MessageBoxButtons.OK
                 , System.Windows.Forms.MessageBoxIcon.Error);
            }

            return form;
        }

        public System.Windows.Forms.Form CreateModellingOffsetDialog(IHeadsApplication app)
        {
            HeadsFunctions1.Offset.Modelling.FormModellingOffset form = null;
            string strFilePath = Path.Combine(app.AppDataPath, "OFFSET.FIL");
            if (File.Exists(strFilePath))
            {
                form = new HeadsFunctions1.Offset.Modelling.FormModellingOffset(app);
            }
            else
            {
                MessageBox.Show(Resources.ST_FILE_NOTFOUND + "\n" + strFilePath
                 , Resources.ST_ERROR_MSGBOX_CAPTION
                 , System.Windows.Forms.MessageBoxButtons.OK
                 , System.Windows.Forms.MessageBoxIcon.Error);
            }

            return form;
        }
        #endregion

        #region BOUNDARY
        public System.Windows.Forms.Form CreateBoundaryDialog(IHeadsApplication app, bool bMakeString)
        {
            MakeString.FormMakeString formmakestring = null;
            //Only one polyline object is selected
            IHdEntity[] selections = app.ActiveDocument.GetUserSelection(Resources.ST_PROMPT_SELECT_POLYLINE);
            IHdPolyline3D polyline = null;
            if (selections.Length == 1)
            {
                IHdEntity entity = selections[0];
                //DictionaryEntry de = selections[0];
                if (entity.EntityName == "POLYLINE3D")
                {
                    polyline = (IHdPolyline3D)entity;
                }
            }
            if (polyline != null)
            {
                MakeString.FormMakeString.OpenMode mode = (bMakeString == true) ? MakeString.FormMakeString.OpenMode.MakeString : HeadsFunctions1.MakeString.FormMakeString.OpenMode.Boundary;
                formmakestring = new MakeString.FormMakeString(app, polyline, mode);
            }
            else
            {
                MessageBox.Show(Resources.ST_PROMPT_SELECT_POLYLINE, HeadsUtils.Constants.ProductName);

            }
            return formmakestring;
        }
        #endregion

        #region LAYOUT
        CLayOut LastAppliedLayout = null;
        int LastAppliedLayoutID = 0;
        public void ApplyLayout(IHeadsApplication app)
        {
            if (this.LastAppliedLayoutID != 0)
            {
                if (MessageBox.Show("Last applied layout not accepted.\nDo you want to cancel the last applied layout?"
                    , "Layout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            //this.LastAppliedLayout = null;
            if (this.LastAppliedLayoutID != 0)
            {
                IHdEntity entity = app.ActiveDocument.GetObjectById(this.LastAppliedLayoutID);
                if (entity != null)
                {
                    entity.Erase();
                    app.ActiveDocument.RefreshDocument();
                }
                this.LastAppliedLayoutID = 0;
            }
           
            

            Layout.FormLayout formlayout = new Layout.FormLayout();

            if (this.LastAppliedLayout != null) formlayout.SelectedLayout = this.LastAppliedLayout;
            if (formlayout.ShowDialog() == DialogResult.OK)
            {
                CPoint3D ptBottomLeft  = app.ActiveDocument.GetUserPoint("Select the bottom-left edge to place the sheet");
                if (ptBottomLeft != null)
                {
                    this.LastAppliedLayout = formlayout.SelectedLayout;

                    CCfgtype cfg = app.ActiveDocument.ConfigParam;
                    double ActualHeight;
                    double ActualWidth;
                    double x2, y2, x11, y11, x22, y22;
                    double x = ptBottomLeft.X;
                    double y = ptBottomLeft.Y;

                    double Xpos = x * cfg.XMetric;
                    double Ypos = y * cfg.YMetric;
                    ActualHeight = this.LastAppliedLayout.sheetLength * (this.LastAppliedLayout.layOutScale / 1000.00);
                    ActualWidth = this.LastAppliedLayout.sheetWidth * (this.LastAppliedLayout.layOutScale / 1000.00);
                    x2 = Xpos + ActualWidth;
                    y2 = Ypos + ActualHeight;

                    x11 = Xpos + this.LastAppliedLayout.leftM * (this.LastAppliedLayout.layOutScale / 1000.00);
                    y11 = Ypos + this.LastAppliedLayout.bottomM * (this.LastAppliedLayout.layOutScale / 1000.00);
                    x22 = x2 - this.LastAppliedLayout.rightM * (this.LastAppliedLayout.layOutScale / 1000.00);
                    y22 = y2 - this.LastAppliedLayout.topM * (this.LastAppliedLayout.layOutScale / 1000.00);

                    this.LastAppliedLayoutID = AddPolyLineLayout(app, Xpos, Ypos, x2, y2, x11, y11, x22, y22);

                    app.ActiveDocument.RefreshDocument();
                }
            }
        }

        public void AcceptLayout(IHeadsApplication app)
        {
            if (this.LastAppliedLayout != null && this.LastAppliedLayoutID != 0)
            {
                IHdEntity entity = app.ActiveDocument.GetObjectById(this.LastAppliedLayoutID);
                if (entity != null && entity.EntityName == "POLYLINE3D")
                {
                    IHdPolyline3D polyline = (IHdPolyline3D)entity;
                    CPoint3D[] cords = polyline.Coordinates;
                    CPoint3D pt1 = null;
                    CPoint3D pt2 = null;

                    if (cords.Length > 7)
                    {
                        pt1 = cords[0];
                        pt2 = cords[1];
                        this.LastAppliedLayout.angle = ViewerUtils.GetAngle(pt1, pt2, false);
                        pt1 = cords[5];
                        this.LastAppliedLayout.startX = pt1.X;
                        this.LastAppliedLayout.startY = pt1.Y;

                        StreamWriter sw = new StreamWriter(Path.Combine(app.AppDataPath, "Layout.fil"), true);
                        string strTemp = string.Format("{0:f3} {1:f3} {2:f3} {3:f3} {4:f3} {5:f3} {6:f3} {7:f3} {8:f3} {9:f3} "
                            , this.LastAppliedLayout.startX
                            , this.LastAppliedLayout.startY
                            , this.LastAppliedLayout.angle
                            , this.LastAppliedLayout.sheetWidth
                            , this.LastAppliedLayout.sheetLength
                            , this.LastAppliedLayout.layOutScale
                            , this.LastAppliedLayout.bottomM
                            , this.LastAppliedLayout.rightM
                            , this.LastAppliedLayout.topM
                            , this.LastAppliedLayout.leftM);
                        sw.WriteLine(strTemp);
                        sw.Close();
                    }
                }

                //this.LastAppliedLayout = null;
                this.LastAppliedLayoutID = 0;
            }
        }

        private int AddPolyLineLayout(IHeadsApplication app, double x1, double y1, double x2, double y2, double x11, double y11, double x22, double y22)
        {
            IHdPolyline3D pLine;
            double z1 = 0.0;

            List<CPoint3D> listPts  = new List<CPoint3D>();
            listPts.Add(new CPoint3D(x1, y1, z1));
            listPts.Add(new CPoint3D(x2, y2, z1));
            pLine = app.ActiveDocument.DrawPolyline3D(listPts);

            pLine.RemoveVertex(1);

            CPoint3D pt = new CPoint3D(x2, y1, z1);
            pLine.AppendVertex(pt);

            pt = new CPoint3D(x2, y2, z1);
            pLine.AppendVertex (pt);
            pt = new CPoint3D(x1, y2, z1);
            pLine.AppendVertex(pt);
            pt = new CPoint3D(x1, y1, z1);
            pLine.AppendVertex(pt);
            pt = new CPoint3D(x11, y11, z1);
            pLine.AppendVertex(pt);
            pt = new CPoint3D(x22, y11, z1);
            pLine.AppendVertex(pt);
            pt = new CPoint3D(x22, y22, z1);
            pLine.AppendVertex(pt);
            pt = new CPoint3D(x11, y22, z1);
            pLine.AppendVertex(pt);
            pt = new CPoint3D(x11, y11, z1);
            pLine.AppendVertex(pt);

            return pLine.ObjectID;
        }
        #endregion

        #region OFFSET
        public System.Windows.Forms.Form CreateOffsetModelDialog(IHeadsApplication app)
        {
            Offset.FormOffset formoffset = null;
            //Only one polyline object is selected
            IHdEntity[] selections = app.ActiveDocument.GetUserSelection(Resources.ST_PROMPT_SELECT_POLYLINE);
            IHdPolyline3D polyline = null;
            if (selections.Length == 1)
            {
                IHdEntity entity = selections[0];
                //DictionaryEntry de = selections[0];
                if (entity.EntityName == "POLYLINE3D")
                {
                    polyline = (IHdPolyline3D)entity;
                }
            }
            if (polyline != null)
            {
                formoffset = new Offset.FormOffset(app, polyline);
            }
            else
            {
                MessageBox.Show(Resources.ST_PROMPT_SELECT_POLYLINE, HeadsUtils.Constants.ProductName);
            }
            return formoffset;
        }
        #endregion

        #region DRG FILE
        public void ExportToDrgFile(IHeadsApplication app, string strFilePath)
        {
            DrawingUtil.ExportToDRG(app, strFilePath);
        }
        public void ImportFromDrgFile(IHeadsApplication app, string strFilePath)
        {
            DrawingUtil.ImportFromDRG(app, strFilePath, "PVR", false);
        }
        #endregion


        #region LOAD DEFLECTION
        public Form CreateLoadDeflectionDialog(IHeadsApplication app)
        {
            LoadDeflection.FormLoadDeflection frmLoadDf = null;
            string strpathlistfile = Path.Combine(app.AppDataPath, "model.lst");
            string strpathfilfile = Path.Combine(app.AppDataPath, "model.fil");
            if (File.Exists(strpathlistfile) && File.Exists(strpathfilfile))
            {
                frmLoadDf =  new LoadDeflection.FormLoadDeflection(app);
            }
            else
            {
                MessageBox.Show(Resources.ST_PROMPT_LSTFIL_FILE_NOT_FOUND
                 , Resources.ST_ERROR_MSGBOX_CAPTION
                 , System.Windows.Forms.MessageBoxButtons.OK
                 , System.Windows.Forms.MessageBoxIcon.Error);
            }

            return frmLoadDf;
        }

        #endregion


        #region FIND OPTION
        public Form CreateFindOptionDialog(IHeadsApplication app)
        {
            return new FindOption.FormFindOption(app);
        }
        #endregion
    }
}
