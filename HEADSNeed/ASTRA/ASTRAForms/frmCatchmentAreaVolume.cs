using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Collections;
using System.Windows.Forms;

using VectorDraw.Geometry;
using VectorDraw.Actions;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdFigures;


namespace HEADSNeed.ASTRA.ASTRAForms
{
    public partial class frmCatchmentAreaVolume : Form
    {
        vdDocument vdoc;
        Box box = new Box();
        string working_folder = "";

        public frmCatchmentAreaVolume(vdDocument doc, string working_fol)
        {
            working_folder = working_fol;
            InitializeComponent();
            vdoc = doc;
        }
        public void Work()
        {
            vdFigure vFig;
            gPoint gp = new gPoint();
            gPoint gp1 = new gPoint();
            gPoint gp2 = new gPoint();



            //box.A
            MessageBox.Show(this, "Define Limits by clicking mouse button at two opposite diagonal corners.");
            vdoc.Prompt("Define Limits by clicking mouse button at two opposite diagonal corners:");
            //vdRect rect = vdoc.ActionUtility.getUserRect(gp) as vdRect;
            StatusCode sc = vdoc.ActionUtility.getUserPoint(out gp);
            sc = vdoc.ActionUtility.getUserRectViewCS(gp, out box);
            vdoc.Prompt("");

            //gp1 = rect.InsertionPoint;
            //gp2.x = gp1.x + rect.Width;
            //gp2.y = gp1.y + rect.Height;

            //MessageBox.Show(box.ToString());
        }

        private void btn_Proceed_Click(object sender, EventArgs e)
        {
            List<vdPolyline> list_p_lines = new List<vdPolyline>();
            List<vdPolyline> list_contour = new List<vdPolyline>();
            try
            {

                vdPolyline pl;
                int i = 0;
                for (i = 0; i < vdoc.ActiveLayOut.Entities.Count; i++)
                {
                    pl = vdoc.ActiveLayOut.Entities[i] as vdPolyline;
                    if (pl != null)
                    {
                        list_p_lines.Add(pl);
                    }
                }
                Hashtable hat = new Hashtable();


                vdPolyline pline1, pline2;

                for (i = 0; i < list_p_lines.Count; i++)
                {
                    pline1 = GetOptimizedPolyLine(list_p_lines[i]);
                    if (pline1 != null)
                    {
                        if (pline1.VertexList.Count != 0)
                        {
                            list_contour.Add(pline1);
                        }
                    }
                }
                List<double> list_ele = new List<double>();
                double elev = 0.0;
                for (i = 0; i < list_contour.Count; i++)
                {
                    elev = list_contour[i].VertexList[0].z;

                    pline2 = hat[elev] as vdPolyline;
                    if (pline2 != null)
                    {
                        //Chiranjit [2011 12 06]
                        //list_contour[i].VertexList.AddRange(pline2.VertexList);
                        if (list_contour[i].VertexList.Count > pline2.VertexList.Count)
                        {
                            hat.Remove(elev);
                            hat.Add(elev, list_contour[i]);
                        }
                    }
                    else
                    {
                        hat.Add(elev, list_contour[i]);
                        list_ele.Add(elev);
                    }
                }


                List<string> Data = new List<string>();

                list_ele.Sort();
                list_ele.Reverse();

                double level_area, last_level_area, level_volume, cumulative_vol;

                level_area = 0.0d;
                last_level_area = 0.0d;
                level_volume = 0.0d;
                cumulative_vol = 0.0d;

                Data.Add("");
                Data.Add("      **************************************************");
                Data.Add("      *                                                *");
                Data.Add("      *            ASTRA Pro Release 5.0               *");
                Data.Add("      *                                                *");
                Data.Add("      *           Storage Area and Volume              *");
                Data.Add("      *                                                *");
                Data.Add("      *         TechSOFT Engineering Services          *");
                Data.Add("      *                                                *");
                Data.Add("      *  The Program was run on " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + " *");
                Data.Add("      *                                                *");
                Data.Add("      **************************************************");
                Data.Add("");

                int j = 0;

                double a1 = 0.0;
                double a2 = 0.0;
                double e1 = 0.0, e2 = 0.0;
                //if isHill = true that means the data is hill road data
                //if isHill = false that means the data is Lake road data
                bool isHill = true;



                isHill = (cmb_storage_type.SelectedIndex == 0);




                for (i = 0; i < list_ele.Count; i++)
                {
                    elev = list_ele[i];
                    pline1 = hat[elev] as vdPolyline;
                    level_area = Math.Abs(pline1.Area());

                    a2 = level_area;
                    e1 = elev;

                    if (a1 == 0.0)
                    {
                        e2 = e1;
                        a1 = level_area;
                    }
                    if (isHill)
                    {
                        if (a1 > a2)
                        {
                            //e2 = e1;
                            continue;
                        }
                    }
                    else
                    {
                        if (a1 < a2)
                        {
                            //e2 = e1;
                            continue;
                        }
                    }
                    a1 = a2;

                    Data.Add("");
                    Data.Add("==================================================");
                    //Data.Add("");
                    Data.Add(string.Format("ELEVATION {0}", elev));
                    for (j = 0; j < pline1.VertexList.Count; j++)
                    {
                        Data.Add(string.Format("{0},{1},{2}", pline1.VertexList[j].x, pline1.VertexList[j].y, pline1.VertexList[j].z));
                    }

                    if (i > 0)
                    {
                        //level_volume = Math.Abs((list_ele[i] - list_ele[i - 1]) * (last_level_area + level_area) / 2.0);
                        level_volume = Math.Abs((e2 - e1) * (last_level_area + level_area) / 2.0);
                        e2 = e1;
                    }
                    cumulative_vol += level_volume;
                    Data.Add("==================================================");
                    Data.Add(string.Format("LEVEL AREA {0:f3} Sq.M., STORAGE VOLUME {1:f3} Cu.M., CUMULATIVE VOLUME {2:f3} Cu.M. ",
                        level_area, level_volume, cumulative_vol));
                    //Data.Add(string.Format("STORAGE VOLUME {0:f3} Cu.M.", level_volume));
                    //Data.Add(string.Format("CUMULATIVE VOLUME {0:f3} Cu.M.", cumulative_vol));
                    Data.Add("==================================================");
                    Data.Add("");
                    //Data.Add("");
                    //Data.Add("");

                    last_level_area = level_area;
                }
                string file_name = Path.Combine(working_folder, "Storage_Report.txt");
                File.WriteAllLines(file_name, Data.ToArray());



                #region
                //ELEVATION 565
                //181089.456,696175.104,491.519
                //181089.504,696175.040,491.517
                //181089.552,696175.040,491.517
                //181089.600,696175.040,491.518
                //181089.648,696175.040,491.519
                //181089.712,696175.040,491.518
                //181089.760,696175.040,491.516
                //181089.824,696175.040,491.516
                //181089.888,696175.040,491.519
                //181089.936,696175.040,491.521
                //181090.016,696175.040,491.518
                //181090.080,696175.040,491.520
                //181090.144,696175.040,491.518
                //181090.240,696175.040,491.509
                //181090.320,696175.040,491.508
                //181090.416,696174.976,491.497
                //181090.512,696174.976,491.492
                //181090.608,696174.976,491.491
                //181090.688,696174.976,491.499
                //181090.784,696174.976,491.501
                //181090.880,696174.976,491.506
                //181091.008,696174.976,491.499
                //181091.120,696174.976,491.506
                //181091.232,696174.912,491.511
                //181091.376,696174.912,491.514
                //181091.504,696174.912,491.521
                //181091.680,696174.912,491.514
                //LEVEL AREA 21.765 Sq.M.   
                //STORAGE VOLUME 0.000 Cu.M.


                //ELEVATION 566
                //181089.456,696175.104,491.519
                //181089.504,696175.040,491.517
                //181089.552,696175.040,491.517
                //181089.600,696175.040,491.518
                //181089.648,696175.040,491.519
                //181089.712,696175.040,491.518
                //181089.760,696175.040,491.516
                //181089.824,696175.040,491.516
                //181089.888,696175.040,491.519
                //181089.936,696175.040,491.521
                //181090.016,696175.040,491.518
                //181090.080,696175.040,491.520
                //181090.144,696175.040,491.518
                //181090.240,696175.040,491.509
                //181090.320,696175.040,491.508
                //181090.416,696174.976,491.497
                //181090.512,696174.976,491.492
                //181090.608,696174.976,491.491
                //181090.688,696174.976,491.499
                //181090.784,696174.976,491.501
                //181090.880,696174.976,491.506
                //181091.008,696174.976,491.499
                //181091.120,696174.976,491.506
                //181091.232,696174.912,491.511
                //181091.376,696174.912,491.514
                //181091.504,696174.912,491.521
                //181091.680,696174.912,491.514
                //LEVEL AREA 31.765 Sq.M.   
                //STORAGE VOLUME 41.569 Cu.M.
                #endregion
                Data.Clear();
                Data.Add(string.Format("{0,-5}{1}{2,14:f5}{1}{3,15:f5}{1}{4,15:f5}{1}  {5}",
                       "Serial", ' ',
                       "Easting",
                       "Northing",
                       "Elevation",
                       "Feature"));
                Data.Add(string.Format("{0,-5}{1}{2,15:f5}{1}{3,15:f5}{1}{4,15:f5}{1}  {5}",
                   "No:", ' ',
                   "  (Metres)",
                   "  (Metres)",
                   "  (Metres)",
                   "Code"));
                //sw.WriteLine("{0,-5}{1}{2,14:f5}{1}{3,15:f5}{1}{4,15:f5}{1}  {5}",
                //       "Serial", sp_char,
                //       "Easting",
                //       "Northing",
                //       "Elevation",
                //       "Feature");
                //sw.WriteLine("Serial{0}Easting{0}  Northing{0} Elevation{0} Feature", sp_char);
                //sw.WriteLine("{0,-5}{1}{2,15:f5}{1}{3,15:f5}{1}{4,15:f5}{1}  {5}",
                //   "No:", sp_char,
                //   "  (Metres)",
                //   "  (Metres)",
                //   "  (Metres)",
                //   "Code");
                string lay = "";
                int lay_indx1 = -1;
                int lay_indx2 = -1;

                for (i = 0; i < list_ele.Count; i++)
                {
                    elev = list_ele[i];
                    pline1 = hat[elev] as vdPolyline;
                    level_area = pline1.Area();

                    if (i > lay_indx2)
                    {
                        lay_indx1 = i;
                        lay_indx2 = lay_indx1 + 0;
                        if (lay_indx2 > list_ele.Count - 1)
                            lay_indx2 = list_ele.Count - 1;

                    }

                    lay = string.Format("OGL_{0}_{1}", list_ele[lay_indx1], list_ele[lay_indx2]);
                    if (list_ele[lay_indx1] == list_ele[lay_indx2])
                    {
                        lay = string.Format("OGL_{0}", list_ele[lay_indx1]);
                    }
                    for (j = 0; j < pline1.VertexList.Count; j++)
                    {
                        Data.Add(string.Format("{0,-5}{1}{2,15:f5}{1}{3,15:f5}{1}{4,15:f5}{1}  {5}",
                            (j + 1), ' ',
                            pline1.VertexList[j].x,
                            pline1.VertexList[j].y,
                            pline1.VertexList[j].z,
                            lay));

                    }

                    Data.Add(string.Format("{0,-5}{1}{2,15:f5}{1}{3,15:f5}{1}{4,15:f5}{1}  {5}",
                           0, ' ',
                           0,
                           0,
                           0,
                           lay));
                }

                File.WriteAllLines(Path.Combine(working_folder, "Storage_SurveyData.txt"), Data.ToArray());

                //file_name = @"C:\Documents and Settings\sandipan\My Documents\Downloads\INTAKE_ADIT.txt";
                //Data = new List<string>(File.ReadAllLines(file_name));
                //if (MessageBox.Show(this, "Reports Written in file " + file_name + "\n\nDo you want to open report file ?", "HEADS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //{
                System.Diagnostics.Process.Start(file_name);
                //}
                this.Close();
            }
            catch (Exception ex) { }
            finally
            {
                list_contour = null;
                list_p_lines = null;
            }
        }

        public vdPolyline GetOptimizedPolyLine(vdPolyline pl)
        {
            //vdPolyline vpl = new vdPolyline();
            //gPoint gp;
            //for (int i = 0; i < pl.VertexList.Count; i++)
            //{
            //    gp = pl.VertexList[i] as gPoint;
            //    if (box.PointInBox(gp))
            //    {
            //        vpl.VertexList.Add(gp);
            //    }
            //}
            //if (vpl.VertexList.Count == 1)
            //    vpl.VertexList.RemoveAll();
            ////if (vpl.VertexList.Count > 0)
            ////    vpl.VertexList.Add(vpl.VertexList[0]);
            //return vpl;

            //Chiranjit [2011 12 06] 
            //Ignore contour with 0, -999.0 elevations
            //Remove contour with any point in outside zone
            vdPolyline vpl = new vdPolyline();
            gPoint gp;
            for (int i = 0; i < pl.VertexList.Count; i++)
            {
                gp = pl.VertexList[i] as gPoint;
                if (box.PointInBox(gp) && gp.z != 0.0 && gp.z > -997.0)
                {
                    vpl.VertexList.Add(gp);
                }
                else
                {
                    vpl.VertexList.RemoveAll();
                    break;
                }
            }
            if (vpl.VertexList.Count == 1)
                vpl.VertexList.RemoveAll();
            //if (vpl.VertexList.Count > 0)
            //    vpl.VertexList.Add(vpl.VertexList[0]);
            return vpl;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCatchmentAreaVolume_Load(object sender, EventArgs e)
        {
            cmb_storage_type.SelectedIndex = 0;
            try
            {
                Work();
            }
            catch (Exception ex) { }
        }
    }
    public class CatchmentColorFill
    {
        vdDocument vdoc = null;
        public CatchmentColorFill(vdDocument doc)
        {
            vdoc = doc;
        }
        public void ColorFill()
        {
            List<vdPolyline> list_p_lines = new List<vdPolyline>();
            List<vdPolyline> list_contour = new List<vdPolyline>();
            try
            {

                vdPolyline pl;
                int i = 0;
                for (i = 0; i < vdoc.ActiveLayOut.Entities.Count; i++)
                {
                    pl = vdoc.ActiveLayOut.Entities[i] as vdPolyline;
                    if (pl != null)
                    {
                        if (!pl.Deleted)
                            list_p_lines.Add(pl);
                    }
                }
                Hashtable hat = new Hashtable();


                vdPolyline pline1, pline2;

                for (i = 0; i < list_p_lines.Count; i++)
                {
                    pline1 = list_p_lines[i];
                    if (pline1.VertexList.Count != 0)
                    {
                        list_contour.Add(pline1);
                    }
                }
                List<double> list_ele = new List<double>();
                double elev = 0.0;
                for (i = 0; i < list_contour.Count; i++)
                {
                    elev = list_contour[i].VertexList[0].z;

                    pline2 = hat[elev] as vdPolyline;
                    if (pline2 != null)
                    {
                        if (list_contour[i].VertexList.Count > pline2.VertexList.Count)
                        {
                            hat.Remove(elev);
                            hat.Add(elev, list_contour[i]);
                        }
                    }
                    else
                    {
                        hat.Add(elev, list_contour[i]);
                        list_ele.Add(elev);
                    }
                }
                double r, g, b;

                r = g = b = 0;
                Color cl;
                list_ele.Sort();
                for (i = 0; i < list_ele.Count; i++)
                {
                    r = 190;
                    g = 255.0d * ((double)i / (double)list_contour.Count);
                    b = 9;
                    cl = Color.FromArgb((int)r, (int)g, (int)b);
                    pline1 = hat[list_ele[i]] as vdPolyline;
                    pline1.PenColor = new vdColor(cl);
                    //pline1.HatchProperties = new vdHatchProperties(VectorDraw.Professional.Constants.VdConstFill.VdFillModeSolid);
                    //pline1.HatchProperties.FillColor = new vdColor(cl);
                    pline1.Update();
                    vdoc.Redraw(true);
                }
                //for (i = 0; i < list_contour.Count; i++)
                //{
                //    r = 190;
                //    g = 255.0d * ((double)i / (double)list_contour.Count);
                //    b = 9;
                //    cl = Color.FromArgb((int)r, (int)g, (int)b);
                //    pline1 = list_contour[i];
                //    pline1.PenColor = new vdColor(cl);
                //    //pline1.HatchProperties = new vdHatchProperties(VectorDraw.Professional.Constants.VdConstFill.VdFillModeSolid);
                //    //pline1.HatchProperties.FillColor = new vdColor(cl);
                //    pline1.Update();
                //    vdoc.Redraw(true);
                //}
            }
            catch (Exception ex) { }
        }
    }
}
//Chiranjit [2011 12 06] 
//Ignore contour with 0, -999.0 elevations
//Remove contour with any point in outside zone