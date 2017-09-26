using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
//using HEADSNeed.ProjectRoad;
using HEADSNeed.ASTRA.ASTRAForms;
using HeadsUtils.Interfaces;
using HEADSNeed.DrawingToData;

using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Professional.vdCommandLine;
using VectorDraw.Geometry;
using Excel = Microsoft.Office.Interop.Excel;

namespace HEADSNeed.ASTRA.ASTRAClasses
{
    public class CatchmentFunctions
    {
        static Form thisFrm;
        static IHeadsApplication ihapp;
        static string sample_data = "";
        public static void CatchmentMenu(vdDocument vdoc, string menu_name, vdCommandLine vcmd, Form thisForm, IHeadsApplication iHApp)
        {
            ihapp = iHApp;
            thisFrm = thisForm;
            switch (menu_name.ToUpper())
            {
                //case "POLY_JOINTWOPOLYLINE":
                //    Poly_JoinTwoPolyline(vdoc);
                //    break;
                //case "POLY_JOINLINESTOPOLYLINE":
                //    Poly_JoinLinesToPolyline(vdoc);
                //    break;
                //case "POLY_INSERTPOINTTOPOLYLINE":
                //    Poly_InsertPointToPolyline(vdoc);
                //    break;
                //case "POLY_BREAKPOLYLINE":
                //    Poly_BreakPolyline(vdoc);
                //    break;
                //case "POLY_SHIFTPOINTINPOLYLINE":
                //    Poly_ShiftPoinInPolyline(vdoc);
                //    break;
                //case "POLY_POLYLINELENGTH":
                //    Poly_PolylineLength(vdoc, vcmd);
                //    break;
                case "CATCHMENT_AREAVOLUME":
                    CATCHMENT_AREAVOLUME(vdoc);
                    break;


                case "CATCHMENT_PROCESSTERRAINSURVEYDATA":
                    CATCHMENT_ProcessTerrainSurveyData(vdoc);
                    break;

                case "CATCHMENT_SCOURDEPTHCALCULATION":
                    CATCHMENT_ScourDepthCalculation(vdoc);
                    break;

                case "CATCHMENT_SAMPLEDRAWINGTOPOGRAPHY":
                    CATCHMENT_SampleDrawingTopography(vdoc);
                    break;


                case "CATCHMENT_UG":
                    CATCHMENT_UG(vdoc);
                    break;
                case "CATCHMENT_TEXTMAPPING":
                    CATCHMENT_TEXTMAPPING(vdoc);
                    break;
                case "CATCHMENT_COLORFILL":
                    CATCHMENT_COLORFILL(vdoc);
                    break;
                case "CATCHMENT_HYDROGRAPH":
                    CATCHMENT_HYDROGRAPH(vdoc);
                    break;
                case "CATCHMENT_OPENHYDROGRAPH":
                    CATCHMENT_OPENHYDROGRAPH(vdoc);
                    break;
                case "CATCHMENT_SAMPLEDRAWINGHYDROGRAPH":
                    CATCHMENT_SampleDrawingHydrograph(vdoc);
                    break;
            }
        }

        private static bool CATCHMENT_ProcessTerrainSurveyData(vdDocument vdoc)
        {
            vdoc.Prompt("Click Left and Right mouse button on River Centre Line Polyline :");

            vdSelection sel = vdoc.ActionUtility.getUserSelection();

            vdFigure vfig = sel[0];

            vdPolyline pline = vfig as vdPolyline;
            if (pline != null)
            {
                frm_Process_Terrain_Survey_data frm = new frm_Process_Terrain_Survey_data(pline);
                frm.txt_select_survey_data.Text = sample_data;
                frm.ShowDialog();
            }
            return false;
        }
        static bool CATCHMENT_SampleDrawingHydrograph(vdDocument vdoc)
        {

            switch (frmOpenHydrologyDrawing.Get_DrawingType())
            {
                case frmOpenHydrologyDrawing.eHydroDrawing.SampleDrawing:
                    #region Sample Drawing
                    string sam_drawing_file = Path.Combine(Application.StartupPath, "DRAWINGS\\Catchment\\River Alignment.vdml");

                    if (!File.Exists(sam_drawing_file)) sam_drawing_file = Path.Combine(Application.StartupPath, "DRAWINGS\\Catchment\\River Alignment.dwg");
                    if (!File.Exists(sam_drawing_file)) sam_drawing_file = Path.Combine(Application.StartupPath, "DRAWINGS\\Bridges\\Catchment\\Terrain_River_Contour.vdml");
                    if (!File.Exists(sam_drawing_file)) sam_drawing_file = Path.Combine(Application.StartupPath, "DRAWINGS\\Bridges\\Catchment\\Terrain River Contour.vdml");
                    if (!File.Exists(sam_drawing_file)) sam_drawing_file = Path.Combine(Application.StartupPath, "DRAWINGS\\Catchment\\Terrain_River_Contour.vdml");
                    string sam_data_file = Path.Combine(Application.StartupPath, @"DESIGN\DefaultData\Catment_Data\Terrain Topo Data.TXT");
                    if(!File.Exists( sam_data_file)) Path.Combine(Application.StartupPath, @"DESIGN\DefaultData\Catment_Data\Terrain.TXT");

                    string drawing_file = "";
                    string data_file = "";
                    string work_folder = "";

                    using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                    {
                        if (fbd.ShowDialog() != DialogResult.Cancel)
                        {
                            work_folder = fbd.SelectedPath;
                        }
                    }

                    if (!Directory.Exists(work_folder)) return false;


                    drawing_file = Path.Combine(work_folder, Path.GetFileName(sam_drawing_file));
                    data_file = Path.Combine(work_folder, Path.GetFileName(sam_data_file));

                    if (File.Exists(sam_drawing_file))
                    {
                        File.Copy(sam_drawing_file, drawing_file, true);
                        if (File.Exists(sam_data_file))
                        {
                            File.Copy(sam_data_file, data_file, true);
                            sample_data = data_file;
                            System.Diagnostics.Process.Start(data_file);
                            //sam_drawing_file = Path.Combine(Application.StartupPath, "DRAWINGS\\Catchment\\Terrain+River+Contour.vdml");
                        }
                        return vdoc.Open(drawing_file);
                    }



                    if (!File.Exists(sam_drawing_file))
                    {
                        MessageBox.Show(sam_drawing_file + " not found.");
                    }
                #endregion Sample Drawing
                    break;
                case frmOpenHydrologyDrawing.eHydroDrawing.ProjectDrawing:
                    string proj_drawing_file = "";
                    using (OpenFileDialog ofd = new OpenFileDialog())
                    {
                        ofd.Filter = "Drawing Files (*.vdml;*.vdcl;*.dwg;*.dxf)|*.vdml;*.vdcl;*.dwg;*.dxf|All Files (*.*)|*.*";
                        if (ofd.ShowDialog() != DialogResult.Cancel)
                        {
                            sample_data = "";
                            proj_drawing_file = ofd.FileName;
                            vdoc.Open(proj_drawing_file);
                        }
                    }
                    break;
            }
            return false;
        }
        static bool CATCHMENT_COLORFILL(vdDocument vdoc)
        {
            CatchmentColorFill col = new CatchmentColorFill(vdoc);
            col.ColorFill();
            return false;

        }
        static bool CATCHMENT_OPENHYDROGRAPH(vdDocument vdoc)
        {
            string file_n = Path.Combine(Application.StartupPath, "Design\\Synthetic_Unit_Hydrograph.xls");
            if (!File.Exists(file_n))
                file_n = Path.Combine(Application.StartupPath, "Design\\SUH.xls");
            if (!File.Exists(file_n))
                file_n = Path.Combine(Application.StartupPath, "Design\\Synthetic_Unit_Hydrograph_[SUH].xls");
            if (!File.Exists(file_n))
                file_n = Path.Combine(Application.StartupPath, "Design\\Synthetic_Unit_Hydrograph_(SUH).xls");

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Worksheet Design File(*.xls)|*.xls;*.xlsx";
                if (ofd.ShowDialog() != DialogResult.Cancel)
                {
                    try
                    {
                        OpenExcelFile(ofd.FileName, "2011ap");
                        //System.Diagnostics.Process.Start(file_n);
                    }
                    catch (Exception ex) { }
                }
                else
                {
                    MessageBox.Show(file_n + " file not found.", "HEADS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return false;
        }
        static bool CATCHMENT_HYDROGRAPH(vdDocument vdoc)
        {
            string file_n = Path.Combine(Application.StartupPath, "Design\\Synthetic_Unit_Hydrograph.xls");
            if (!File.Exists(file_n))
                file_n = Path.Combine(Application.StartupPath, "Design\\SUH.xls");
            if (!File.Exists(file_n))
                file_n = Path.Combine(Application.StartupPath, "Design\\Synthetic_Unit_Hydrograph_[SUH].xls");
            if (!File.Exists(file_n))
                file_n = Path.Combine(Application.StartupPath, "Design\\Synthetic_Unit_Hydrograph_(SUH).xls");

            if (File.Exists(file_n))
            {
                try
                {
                    string work_folder = "";
                    using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                    {
                        if (fbd.ShowDialog() != DialogResult.Cancel)
                        {
                            work_folder = Path.Combine(fbd.SelectedPath, Path.GetFileName(file_n));
                            File.Copy(file_n, work_folder);
                            OpenExcelFile(file_n, "2011ap");
                        }
                    }

                }
                catch (Exception ex) { }
            }
            else
            {
                MessageBox.Show(file_n + " file not found.", "HEADS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        static bool CATCHMENT_TEXTMAPPING(vdDocument vdoc)
        {
            string  file_name = Path.Combine(ihapp.AppDataPath, "Storage_SurveyData.txt");
            if (File.Exists(file_name))
            {
                frmOptions frm = new frmOptions(vdoc, file_name);
                frm.Text = "Catchment Area/Volume User's Guide";
                frm.Owner = thisFrm;
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.Show();
            }
            else
            {
                MessageBox.Show("\"Storage_SurveyData.txt\" file not found in Working Folder.", "HEADS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;

        }
        static bool CATCHMENT_ScourDepthCalculation(vdDocument vdoc)
        {

            frmScourDepthCalculation frm = new frmScourDepthCalculation(vdoc);
            frm.Text = "Scour Depth Calculation";
            frm.Owner = thisFrm;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Show();
            return false;

        }
        static bool CATCHMENT_SampleDrawingTopography(vdDocument vdoc)
        {
            string file_p = Path.Combine(Application.StartupPath, "DRAWINGS\\Catchment\\Contour_Drawing.vdml");
            if (!File.Exists(file_p))
                file_p = Path.Combine(Application.StartupPath, "DRAWINGS\\Catchment\\Contour.vdml");
            if (!File.Exists(file_p))
                file_p = Path.Combine(Application.StartupPath, "DRAWINGS\\Catchment\\Contours.vdml");
           

            
            if (File.Exists(file_p))
            {
                return vdoc.Open(file_p);
            }
            else
            {
                MessageBox.Show(file_p + " not found.");
            }
            return false;

        }
        static bool CATCHMENT_UG(vdDocument vdoc)
        {
            string file_name = Path.Combine(Application.StartupPath, "CatchmentAreaVolume_user_guide.txt");
            if (!File.Exists(file_name))
            {
                file_name = Path.Combine(Application.StartupPath, @"AstraHelp\CatchmentAreaVolume_user_guide.txt");
            }
            frmASTRAReport frm = new frmASTRAReport(file_name);
            frm.Text = "Catchment Area/Volume User's Guide";
            frm.Owner = thisFrm;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Show();
            return false;

        }


        static bool CATCHMENT_AREAVOLUME(vdDocument vdoc)
        {
           //string file_name = @"C:\Documents and Settings\sandipan\Desktop\TEST\WORK 15 [Tunnel New Test]\Tunnel Data Test\HDS002A.FIL";
            
           frmCatchmentAreaVolume frm = new frmCatchmentAreaVolume(vdoc, ihapp.AppDataPath);
           frm.Owner = thisFrm;
           frm.Show();
           return false;

        }

        static bool Poly_JoinTwoPolyline(vdDocument vdoc)
        {
            //if (MessageBox.Show("For Joining Two Polylines both must be in the same direction" +
            //    ", otherwise, user has to reverse one of the two polylines.",
            //    "HEADS Viewer", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel) return false;


            vdFigure vFig;
            gPoint gp = new gPoint();
            gPoint gp1 = new gPoint();
            gPoint gp2 = new gPoint();



            vdPolyline pline1, pline2;

            vdoc.Prompt("Select a First Polyline:");
            vdoc.ActionUtility.getUserEntity(out vFig, out gp);


            pline1 = vFig as vdPolyline;
            gp1 = gp;

            vdoc.Prompt("Select a Second Polyline:");
            vdoc.ActionUtility.getUserEntity(out vFig, out gp);
            pline2 = vFig as vdPolyline;
            gp2 = gp;

            Vertexes vert = new Vertexes();
            double d1 = pline1.VertexList[0].Distance3D(pline2.VertexList[0]);
            double d2 = pline1.VertexList[0].Distance3D(pline2.VertexList[pline2.VertexList.Count - 1]);
            if (d1 > d2)
            {
                pline2.VertexList.Reverse();
            }

            vert.AddRange(pline1.VertexList);
            vert.AddRange(pline2.VertexList);



            if (pline1 is vdPolyline)
            {
                vdoc.Redraw(true);
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(vdoc);
            }

            //pline1.Deleted = true;
            pline2.Deleted = true;

            //vdoc.ActiveLayOut.Entities.RemoveItem(pline1);
            //vdoc.ActiveLayOut.Entities.RemoveItem(pline2);

            pline1.VertexList = vert;

            pline1.Update();
            vdoc.Redraw(true);




            return false;

        }
        static bool Poly_JoinLinesToPolyline(vdDocument vdoc)
        {
            vdoc.Prompt("Select Lines or Polylines to Join:");

            vdSelection sel = vdoc.ActionUtility.getUserSelection();

            vdFigure vfig;

            vdPolyline pline = new vdPolyline();

            vdLine ln = null;
            vdPolyline pln = null;

            double d1, d2;

            d1 = d2 = 0.0;
            foreach (vdFigure fig in sel)
            {
                if (fig is vdLine)
                {
                    ln = fig as vdLine;
                    if (pline.VertexList.Count > 0)
                    {
                        d1 = pline.VertexList[pline.VertexList.Count - 1].Distance3D(ln.StartPoint);
                        d2 = pline.VertexList[pline.VertexList.Count - 1].Distance3D(ln.EndPoint);
                    }
                    if (d2 >= d1)
                    {
                        if (pline.VertexList.FindVertexPoint(ln.StartPoint) == -1)
                        {
                            pline.VertexList.Add(ln.StartPoint);
                        }
                        if (pline.VertexList.FindVertexPoint(ln.EndPoint) == -1)
                        {
                            pline.VertexList.Add(ln.EndPoint);
                        }
                    }
                    else
                    {
                        if (pline.VertexList.FindVertexPoint(ln.EndPoint) == -1)
                        {
                            pline.VertexList.Add(ln.EndPoint);
                        }
                        if (pline.VertexList.FindVertexPoint(ln.StartPoint) == -1)
                        {
                            pline.VertexList.Add(ln.StartPoint);
                        }
                    }

                    ln.Deleted = true;
                    //vdoc.ActiveLayOut.Entities.RemoveItem(ln);
                } 
                else if (fig is vdPolyline)
                {
                    pln = fig as vdPolyline;
                    if (pline.VertexList.Count > 0)
                    {
                        d1 = pline.VertexList[pline.VertexList.Count - 1].Distance3D(pln.VertexList[0]);
                        d2 = pline.VertexList[pline.VertexList.Count - 1].Distance3D(pln.VertexList[pln.VertexList.Count - 1]);

                        if (d1 > d2)
                            pln.VertexList.Reverse();
                    }
                    pline.VertexList.AddRange(pln.VertexList);

                    pln.Deleted = true;
                    //vdoc.ActiveLayOut.Entities.RemoveItem(pln);
                }
            }

            if (pline.VertexList.Count > 1)
            {
                pline.SetUnRegisterDocument(vdoc);
                pline.setDocumentDefaults();
                vdoc.ActiveLayOut.Entities.AddItem(pline);
            }
            vdoc.Redraw(true);

            return true;
           
        }
        static bool Poly_InsertPointToPolyline(vdDocument vdoc)
        {
            vdFigure vFig;
            gPoint gp = new gPoint();
            //gPoint gp1 = new gPoint();
            //gPoint gp2 = new gPoint();



            vdPolyline pline1, pline2;

            vdoc.Prompt("Select a First Polyline:");
            vdoc.ActionUtility.getUserEntity(out vFig, out gp);
            vdoc.CommandAction.CmdBreak(vFig, gp, gp);


            vdoc.Redraw(true);




            return false;

        }
        static bool Poly_BreakPolyline(vdDocument vdoc)
        {
            vdFigure vFig;
            gPoint gp = new gPoint();
            //gPoint gp1 = new gPoint();
            //gPoint gp2 = new gPoint();



            vdPolyline pline1, pline2;

            vdoc.Prompt("Select a First Polyline:");
            vdoc.ActionUtility.getUserEntity(out vFig, out gp);
            vdoc.CommandAction.CmdBreak(vFig, gp, gp);


            vdoc.Redraw(true);

            return false;

        }
        static bool Poly_ShiftPoinInPolyline(vdDocument vdoc)
        {
            vdFigure vFig;
            gPoint gp = new gPoint();
            //gPoint gp1 = new gPoint();
            //gPoint gp2 = new gPoint();



            vdPolyline pline1, pline2;

            vdoc.Prompt("Select a Polyline:");
            //vdoc.ActionUtility.getUserEntity(out vFig, out gp);
            //vdoc.CommandAction.CmdBreak(vFig, gp, gp);


            //vdoc.Redraw(true);




            return false;

        }
        static bool Poly_PolylineLength(vdDocument vdoc, vdCommandLine vcmd)
        {
            //if (MessageBox.Show("For Joining Two Polylines both must be in the same direction" +
            //    ", otherwise, user has to reverse one of the two polylines.",
            //    "HEADS Viewer", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel) return false;


            vdFigure vFig;
            gPoint gp = new gPoint();
            //gPoint gp1 = new gPoint();
            //gPoint gp2 = new gPoint();



            vdPolyline pline1, pline2;

            vdoc.Prompt("Select a Polyline:");
            vdoc.ActionUtility.getUserEntity(out vFig, out gp);


            pline1 = vFig as vdPolyline;
            //gp1 = gp;

            
            vcmd.History.Text = "Polyline Length : " + pline1.Length().ToString("0.0000");
            //vdoc.Prompt("Polyline Length : " + pline1.Length().ToString("0.0000"));

            //if (pline1 is vdPolyline)
            //{
            //    vdoc.Redraw(true);
            //    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(vdoc);
            //}

            //pline1.Update();
            vdoc.Redraw(true);

            return false;

        }
       
        public static bool OpenExcelFile(string ExcelFileName, string password)
        {
            try
            {
                {
                    Excel.Application xlApp;
                    Excel.Workbook xlWorkBook;
                    Excel.Worksheet xlWorkSheet;

                    object misValue;
                    misValue = System.Reflection.Missing.Value;

                    xlApp = new Excel.ApplicationClass();
                    //xlWorkBook = xlApp.Workbooks.Open("csharp.net-informations.xls", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                    //xlWorkBook = xlApp.Workbooks.Open(txt_file.Text, 0, true, 5, "2011ap", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                    xlWorkBook = xlApp.Workbooks.Open(ExcelFileName, 0, true, 5, password, "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                    //xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                    //Excel.Range rng = xlWorkSheet.get_Range("B2", "B3");
                    xlApp.Visible = true;
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            return false;
        }
        

    }
}
