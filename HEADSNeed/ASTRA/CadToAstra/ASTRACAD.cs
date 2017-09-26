using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Windows.Forms;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Geometry;

using HEADSNeed.ASTRA.ASTRAClasses;
using HEADSNeed.ASTRA.ASTRAForms;
using HEADSNeed.ASTRA.CadToAstra.FORMS;


namespace HEADSNeed.ASTRA.CadToAstra
{
    public class ASTRACAD : IASTRACAD
    {
        //Chiranjit [2010 04 13], TechSOFT, Kolkata
        //Jay Shree Nath, Bholenath, Neel puja

        vdDocument doc;
        ASTRADoc astDoc = null;
        EMassUnits massUnit;
        ELengthUnits lenUnit;
        DrawingMenu d_menu = null;
        Form frm_main = null;
        bool is_moving_load = false;
        string user_file = "";
        bool is_open_file = false;

        string file_moving_load = "";

        string file_mem_inci = "";
        string file_sec_prop = "";
        string file_mat_prop = "";
        string file_support = "";
        string file_load = "";

        MenuStrip mStrip = null;

        string kStr = "";

        List<string> list_file_write = new List<string>();
        List<string> list_file_read = new List<string>();
        string proj_title = "";

        SaveFileDialog sfd = null;

        public ASTRACAD()
        {
            astDoc = new ASTRADoc();
            massUnit = EMassUnits.KIP;
            lenUnit = ELengthUnits.FT;
            d_menu = new DrawingMenu();

            Create_Data = new CREATE_ASTRA_Data();
        }
        public void SetProjectTitle()
        {
            frmProjectTitle frm = new frmProjectTitle(this);
            frm.ShowDialog();
        }


        #region Static Members
        public static bool WriteAstraCode(vdDocument doc, string file_name)
        {
            bool isPolyLine = false;

            List<vdLine> list_line = new List<vdLine>();
            int i = 0;
            for (i = 0; i < doc.ActiveLayOut.Entities.Count; i++)
            {
                if (doc.ActiveLayOut.Entities[i].Layer.Frozen) continue;

                if (doc.ActiveLayOut.Entities[i]._TypeName.StartsWith("ASTRA")) 
                    continue;

                vdLine ln = doc.ActiveLayOut.Entities[i] as vdLine;


                if (ln != null)
                {
                    ln.StartPoint.x = double.Parse(ln.StartPoint.x.ToString("0.000"));
                    ln.StartPoint.y = double.Parse(ln.StartPoint.y.ToString("0.000"));
                    ln.StartPoint.z = double.Parse(ln.StartPoint.z.ToString("0.000"));
                    ln.EndPoint.x = double.Parse(ln.EndPoint.x.ToString("0.000"));
                    ln.EndPoint.y = double.Parse(ln.EndPoint.y.ToString("0.000"));
                    ln.EndPoint.z = double.Parse(ln.EndPoint.z.ToString("0.000"));
                    if (!ln.Deleted)
                        list_line.Add(ln);
                }
                else
                {
                    vdPolyline pline = doc.ActiveLayOut.Entities[i] as vdPolyline;
                    if (pline != null)
                    {
                        if (!isPolyLine)
                        {
                            if (MessageBox.Show("Explode All Polyline?", "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                return false;
                            }
                            else
                                isPolyLine = true;
                        }

                        vdEntities ents = pline.Explode();
                        for (int kk = 0; kk < ents.Count; kk++)
                        {
                            ln = ents[kk] as vdLine;
                            if (ln != null)
                            {
                                ln.StartPoint.x = double.Parse(ln.StartPoint.x.ToString("0.000"));
                                ln.StartPoint.y = double.Parse(ln.StartPoint.y.ToString("0.000"));
                                ln.StartPoint.z = double.Parse(ln.StartPoint.z.ToString("0.000"));
                                ln.EndPoint.x = double.Parse(ln.EndPoint.x.ToString("0.000"));
                                ln.EndPoint.y = double.Parse(ln.EndPoint.y.ToString("0.000"));
                                ln.EndPoint.z = double.Parse(ln.EndPoint.z.ToString("0.000"));
                                if (!ln.Deleted)
                                    list_line.Add(ln);
                            }
                        }
                    }
                    else
                    {
                        vd3DFace _3df = doc.ActiveLayOut.Entities[i] as vd3DFace;
                        if (_3df != null)
                        {
                            if (!isPolyLine)
                            {
                                if (MessageBox.Show("Explode All Polyline?", "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                {
                                    return false;
                                }
                                else
                                    isPolyLine = true;
                            }

                            vdEntities ents = _3df.Explode();




                            for (int kk = 0; kk < _3df.VertexList.Count -1; kk++)
                            {
                                ln = new vdLine();
                                ln.StartPoint = _3df.VertexList[kk];
                                ln.EndPoint = _3df.VertexList[kk + 1];
                                if (ln != null)
                                {
                                    ln.StartPoint.x = double.Parse(ln.StartPoint.x.ToString("0.000"));
                                    ln.StartPoint.y = double.Parse(ln.StartPoint.y.ToString("0.000"));
                                    ln.StartPoint.z = double.Parse(ln.StartPoint.z.ToString("0.000"));
                                    ln.EndPoint.x = double.Parse(ln.EndPoint.x.ToString("0.000"));
                                    ln.EndPoint.y = double.Parse(ln.EndPoint.y.ToString("0.000"));
                                    ln.EndPoint.z = double.Parse(ln.EndPoint.z.ToString("0.000"));
                                    if (!ln.Deleted)
                                        list_line.Add(ln);
                                }
                            }
                        }

                    }

                }

            }

            List<gPoint> list_pts = new List<gPoint>();
            for (i = 0; i < list_line.Count; i++)
            {
                if (!list_pts.Contains(list_line[i].StartPoint))
                    list_pts.Add(list_line[i].StartPoint);
                if (!list_pts.Contains(list_line[i].EndPoint))
                    list_pts.Add(list_line[i].EndPoint);
            }
            List<gPoint> list_sort_pts = new List<gPoint>();


            double x = 0;
            double y = 0;
            double z = 0;

            double max_x = 0.0;
            double min_x = 0.0;
            double max_y = 0.0;
            double min_y = 0.0;
            double max_z = 0.0;
            double min_z = 0.0;

            x = 0;

            int indx = -1;
            int j = 0;


            //list_pts.Sort();


            list_sort_pts.Clear();


            #region Y Sort

            list_pts.AddRange(list_sort_pts.ToArray());
            list_sort_pts.Clear();

            for (i = 0; i < list_pts.Count; i++)
            {

                for (j = 0; j < list_pts.Count; j++)
                {
                    if (j == 0)
                    {
                        y = list_pts[0].y;
                        indx = 0;
                    }

                    if (y > list_pts[j].y)
                    {
                        indx = j;
                        y = list_pts[j].y;
                    }
                }
                list_sort_pts.Add(list_pts[indx]);
                list_pts.RemoveAt(indx);
                i = -1;
            }
            #endregion Y Sort


            #region Z Sort

            list_pts.AddRange(list_sort_pts.ToArray());
            list_sort_pts.Clear();

            for (i = 0; i < list_pts.Count; i++)
            {

                for ( j = 0; j < list_pts.Count; j++)
                {
                    if (j == 0)
                    {
                        z = list_pts[0].z;
                        indx = 0;
                    }
                    else
                    {
                        if (z > list_pts[j].z)
                        {
                            indx = j;
                            z = list_pts[j].z;
                        }
                    }
                }
                list_sort_pts.Add(list_pts[indx]);
                list_pts.RemoveAt(indx);
                i = -1;
            }
            #endregion Z Sort


            #region X Sort

            list_pts.AddRange(list_sort_pts.ToArray());
            list_sort_pts.Clear();

            for (i = 0; i < list_pts.Count; i++)
            {

                for ( j = 0; j < list_pts.Count; j++)
                {
                    if (j == 0)
                    {
                        x = list_pts[0].x;
                        indx = 0;
                    }

                    if (x > list_pts[j].x)
                    {
                        indx = j;
                        x = list_pts[j].x;
                    }
                }
                list_sort_pts.Add(list_pts[indx]);
                list_pts.RemoveAt(indx);
                i = -1;
            }
            #endregion X Sort





            list_pts.AddRange(list_sort_pts.ToArray());
            list_sort_pts.Clear();
            // Sorting Solution (Y,X,Z)
            for (i = 0; i < list_pts.Count; i++)
            {
                x = list_pts[i].x;
                y = list_pts[i].y;
                z = list_pts[i].z;


                if(min_x > x)
                    min_x = x;
                if(min_y > y)
                    min_y = y;
                if(min_z > z)
                    min_x = z;
                
                if(max_x < x)
                    max_x = x;
                if(max_y < y)
                    max_y = y;
                if(max_z < z)
                    max_z = z;
                

                indx = i;
                for (j = 0; j < list_pts.Count; j++)
                {
                    if (y > list_pts[j].y)
                    {
                        y = list_pts[j].y;
                        indx = j;

                        x = list_pts[j].x;
                        z = list_pts[j].z;
                    }
                    else if (y == list_pts[j].y)
                    {
                        if (z > list_pts[j].z)
                        {
                            z = list_pts[j].z;
                            indx = j;
                        }
                        else if (z == list_pts[j].z)
                        {
                            //indx = j;
                            if (x > list_pts[j].x)
                            {
                                x = list_pts[j].x;
                                indx = j;
                            }
                        }
                    }
                }
                list_sort_pts.Add(list_pts[indx]);
                list_pts.RemoveAt(indx);
                i = -1;
            }

            list_pts = new List<gPoint>(list_sort_pts);
            list_sort_pts.Clear();

            JointCoordinateCollection jc_col = new JointCoordinateCollection();
            MemberIncidenceCollection mi_col = new MemberIncidenceCollection();

            JointCoordinate jc = null;
            for (i = 0; i < list_pts.Count; i++)
            {
                jc = new JointCoordinate();
                jc.NodeNo = i + 1;
                jc.Point = list_pts[i];
                jc_col.Add(jc);
            }

            MemberIncidence mi = null;

            List<MemberIncidence> list_mi = new List<MemberIncidence>();

            indx = 0;
            for (i = 0; i < list_line.Count; i++)
            {
                mi = new MemberIncidence();
                mi.MemberNo = i + 1;
                indx = Get_Joint_Coor_Index(list_line[i].StartPoint, jc_col);
                if (indx != -1)
                {
                    mi.StartNode = jc_col[indx];
                }

                indx = Get_Joint_Coor_Index(list_line[i].EndPoint, jc_col);
                if (indx != -1)
                {
                    mi.EndNode = jc_col[indx];
                }
                indx = Get_Member_Index(list_line[i].StartPoint, list_line[i].EndPoint, list_mi);

                if (indx == -1)
                    list_mi.Add(mi);
            }

            indx = 0;

            int find_indx = -1;
            for (i = 0; i < list_mi.Count; i++)
            {
                list_mi[i].MemberNo = i + 1;
                if (list_mi[i].StartNode.NodeNo > list_mi[i].EndNode.NodeNo)
                {
                    jc = new JointCoordinate(list_mi[i].StartNode);
                    list_mi[i].StartNode = list_mi[i].EndNode;
                    list_mi[i].EndNode = jc;
                }
            }

            //int y = 0;
            for (i = 0; i < list_mi.Count; i++)
            {
                x = list_mi[i].StartNode.NodeNo;
                y = list_mi[i].EndNode.NodeNo;
                indx = i;
                for (j = 0; j < list_mi.Count; j++)
                {
                    if (x > list_mi[j].StartNode.NodeNo)
                    {
                        x = list_mi[j].StartNode.NodeNo;
                        y = list_mi[j].EndNode.NodeNo;
                        indx = j;
                    }
                    else if (x == list_mi[j].StartNode.NodeNo)
                    {
                        if (y > list_mi[j].EndNode.NodeNo)
                        {
                            y = list_mi[j].EndNode.NodeNo;
                            indx = j;
                        }
                    }
                }
                find_indx = Get_Member_Index(list_mi[i].StartNode.Point, list_mi[i].EndNode.Point, mi_col);
                if (find_indx == -1)
                    mi_col.Add(list_mi[indx]);
                list_mi.RemoveAt(indx);
                i = -1;
            }


            //Chiranjit [2014 03 19]
            List<MemberIncidence> top_mi = new List<MemberIncidence>();
            List<MemberIncidence> bottom_mi = new List<MemberIncidence>();
            List<MemberIncidence> Vert_mi = new List<MemberIncidence>();
            List<MemberIncidence> Dia_mi = new List<MemberIncidence>();
            List<MemberIncidence> bc_mi = new List<MemberIncidence>();
            List<MemberIncidence> tc_mi = new List<MemberIncidence>();
            List<MemberIncidence> bc_dia_mi = new List<MemberIncidence>();
            List<MemberIncidence> tc_dia_mi = new List<MemberIncidence>();
            List<MemberIncidence> others = new List<MemberIncidence>();


            for (i = 0; i < mi_col.Count; i++)
            {
                if ((mi_col[i].StartNode.Point.z == mi_col[i].EndNode.Point.z) &&
                    (mi_col[i].StartNode.Point.y == mi_col[i].EndNode.Point.y) &&
                    (mi_col[i].StartNode.Point.x != mi_col[i].EndNode.Point.x))
                {
                    //if (mi_col[i].StartNode.Point.y == max_y)
                    //{
                    //    top_mi.Add(mi_col[i]);
                    //}
                    //else if (mi_col[i].StartNode.Point.y == min_y)
                    //{
                    //    bottom_mi.Add(mi_col[i]);
                    //}
                    //else
                    //    others.Add(mi_col[i]);

                    if (mi_col[i].StartNode.Point.y == min_y)
                    {
                        bottom_mi.Add(mi_col[i]);
                    }
                    else
                        top_mi.Add(mi_col[i]);
           
                }
                else if ((mi_col[i].StartNode.Point.x == mi_col[i].EndNode.Point.x) &&
                    (mi_col[i].StartNode.Point.y != mi_col[i].EndNode.Point.y) &&
                    (mi_col[i].StartNode.Point.z == mi_col[i].EndNode.Point.z))
                {

                    Vert_mi.Add(mi_col[i]);
                }
                else if ((mi_col[i].StartNode.Point.z == mi_col[i].EndNode.Point.z) &&
                    (mi_col[i].StartNode.Point.y != mi_col[i].EndNode.Point.y) &&
                    (mi_col[i].StartNode.Point.x != mi_col[i].EndNode.Point.x))
                {
                    Dia_mi.Add(mi_col[i]);
                }
                else if ((mi_col[i].StartNode.Point.z != mi_col[i].EndNode.Point.z) &&
                    (mi_col[i].StartNode.Point.y == mi_col[i].EndNode.Point.y) &&
                    (mi_col[i].StartNode.Point.x == mi_col[i].EndNode.Point.x))
                {
                    //Chiranjit Comment on this line
                    //if (mi_col[i].StartNode.Point.y == max_y)
                    //{
                    //    tc_mi.Add(mi_col[i]);
                    //}
                    //else if (mi_col[i].StartNode.Point.y == min_y)
                    //{
                    //    bc_mi.Add(mi_col[i]);
                    //}
                    //else
                    //    others.Add(mi_col[i]);




                    if (mi_col[i].StartNode.Point.y == min_y)
                    {
                        bc_mi.Add(mi_col[i]);
                    }
                    else
                        tc_mi.Add(mi_col[i]);
                       
                }
                else if ((mi_col[i].StartNode.Point.z != mi_col[i].EndNode.Point.z) &&
                    (mi_col[i].StartNode.Point.y == mi_col[i].EndNode.Point.y) &&
                    (mi_col[i].StartNode.Point.x != mi_col[i].EndNode.Point.x))
                {

                    if (mi_col[i].StartNode.Point.y == max_y)
                    {
                        tc_dia_mi.Add(mi_col[i]);
                    }
                    else if (mi_col[i].StartNode.Point.y == min_y)
                    {
                        bc_dia_mi.Add(mi_col[i]);
                    }

                    else
                        others.Add(mi_col[i]);
                }
                else
                    others.Add(mi_col[i]);

            }

            mi_col.Clear();
            list_mi.Clear();
            //list_mi.AddRange(others.ToArray());
            list_mi.AddRange(bottom_mi.ToArray());
            list_mi.AddRange(Vert_mi.ToArray());
            list_mi.AddRange(Dia_mi.ToArray());
            list_mi.AddRange(bc_mi.ToArray());
            list_mi.AddRange(bc_dia_mi.ToArray());
            list_mi.AddRange(top_mi.ToArray());
            list_mi.AddRange(tc_mi.ToArray());
            list_mi.AddRange(tc_dia_mi.ToArray());
            list_mi.AddRange(others.ToArray());

            //if (find_indx == -1)
            //list_mi.RemoveAt(indx);

            for (i = 0; i < list_mi.Count; i++)
            {
                list_mi[i].MemberNo = i + 1;
                mi_col.Add(list_mi[i]);
            }

            WriteToFile(file_name, jc_col, mi_col);


            return true; 
        }
        private static int Get_Member_Index(gPoint startPt, gPoint endPt, List<MemberIncidence> list_mi)
        {

            for (int i = 0; i < list_mi.Count; i++)
            {
                if ((list_mi[i].StartNode.Point == startPt && list_mi[i].EndNode.Point == endPt) ||
                    (list_mi[i].StartNode.Point == endPt && list_mi[i].EndNode.Point == startPt))
                    return i;
            }
            return -1;
        }
        private static int Get_Member_Index(gPoint startPt, gPoint endPt, MemberIncidenceCollection mi_col)
        {

            for (int i = 0; i < mi_col.Count; i++)
            {
                if ((mi_col[i].StartNode.Point == startPt && mi_col[i].EndNode.Point == endPt) ||
                    (mi_col[i].StartNode.Point == endPt && mi_col[i].EndNode.Point == startPt))
                    return i;
            }
            return -1;
        }
        private static int Get_Joint_Coor_Index(gPoint gp, JointCoordinateCollection jc_col)
        {
            for (int i = 0; i < jc_col.Count; i++)
            {
                if (jc_col[i].Point == gp)
                    return i;
            }
            return -1;
        }
        private static void WriteToFile(string file_name, JointCoordinateCollection jc_col, MemberIncidenceCollection mi_col)
        {
            //file_name = "C:\\KrishnaCodeAstra.ast";

            if (file_name == "") return;
            //MessageBox.Show(file_name );
            StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));
            int i = 0;
            try
            {
                WriteTextFormat(sw, jc_col, mi_col);
                //return;

                //sw.WriteLine("ASTRA FLOOR DESIGN FROM ASTRA VIEWER");
                //sw.WriteLine("STRUCTURE      1     0");
                //sw.WriteLine("N000 KIP FT");
                //sw.WriteLine("N001 UNIT 1.000 1.000 KIP FT KIP FT NODE, x[NODE]*lfact, y[NODE]*lfact, z[NODE]*lfact, TX, TY, TZ, RX, RY, RZ");
                //for (i = 0; i < jc_col.Count; i++)
                //{
                //    sw.WriteLine(jc_col[i].ToString());
                //}
                //sw.WriteLine("N002 UNIT 1.000 1.000 KIP FT KIP FT ELTYPE=2, Beam#, NODE1#, x[NODE1]*lfact, y[NODE1]*lfact, z[NODE1]*lfact, NODE2#, x[NODE2]*lfact, y[NODE2]*lfact, z[NODE2]*lfact");
                //for (i = 0; i < mi_col.Count; i++)
                //{
                //    sw.WriteLine(mi_col[i].ToString());
                //}
                //sw.WriteLine("N099 NDYN NF");
                //sw.WriteLine("N099 NDYN     0 NF     0");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
        private static string Get_file_name()
        {
            string file_name = "";
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.DefaultExt = ".txt";
                sfd.Filter = "TEXT Files|*.txt";

                if (sfd.ShowDialog() != DialogResult.Cancel)
                {
                    file_name = sfd.FileName;
                }
            }
            return file_name;

        }
        private static void WriteTextFormat(StreamWriter sw, JointCoordinateCollection jc_col, MemberIncidenceCollection mi_col)
        {
            int cnt = 3;
            int incr = 0;
            try
            {
                


                sw.WriteLine("ASTRA SPACE ANALYSIS");
                sw.WriteLine("UNIT KN ME");
                sw.WriteLine("JOINT COORDINATES");
                for (int i = 0; i < jc_col.Count; i++)
                {
                    sw.WriteLine("{0} {1:f3} {2:f3} {3:f3}",
                           jc_col[i].NodeNo,
                           jc_col[i].Point.x, jc_col[i].Point.y, jc_col[i].Point.z);
                }

                sw.WriteLine("MEMBER INCIDENCES");
                incr = 0;
                for (int i = 0; i < mi_col.Count; i++)
                {
                    sw.WriteLine("{0} {1} {2}",
                        mi_col[i].MemberNo,
                        mi_col[i].StartNode.NodeNo, mi_col[i].EndNode.NodeNo);

                }
                sw.WriteLine("FINISH");
            }
            catch (Exception ex) { }
        }
        #endregion
        public void EnableAllMenu(bool isEnable)
        {
            string mName = "Project Title";
            //MenuEnable(mName, isEnable);

            mName = "Member Truss";
            MenuEnable(mName, isEnable);


            mName = "Member Release";
            MenuEnable(mName, isEnable);


            mName = "Section Properties";
            MenuEnable(mName, isEnable);


            mName = "Material Properties";
            MenuEnable(mName, isEnable);

            mName = "Support";
            MenuEnable(mName, isEnable);

            mName = "Load";
            MenuEnable(mName, isEnable);

            mName = "Moving Load";
            MenuEnable(mName, isEnable);

            mName = "Analysis Type";
            MenuEnable(mName, isEnable);

            mName = "Show Data";
            MenuEnable(mName, isEnable);

            //mName = "Whole Data";
            //MenuEnable(mName, isEnable);

            mName = "Finish Statement";
            MenuEnable(mName, isEnable);

            mName = "Save Data File";
            MenuEnable(mName, isEnable);

            mName = "Refresh";
            MenuEnable(mName, isEnable);

        }

        #region Drawing Menu
        public bool OpenDataFile(vdDocument doc)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Text Files|*.txt";
            if (ofd.ShowDialog() != DialogResult.Cancel)
            {
                user_file = ofd.FileName;
            }
            else
                return false;

            string cad_file = Path.GetFileNameWithoutExtension(user_file);
            cad_file = cad_file + ".vdml";

            cad_file = Path.Combine(Path.GetDirectoryName(user_file), cad_file);

            astDoc = new ASTRADoc(user_file);
            //MemberIncidence(doc, false);
            is_open_file = Document.Open(cad_file);
            Document.Redraw(true);
            if (!is_open_file)
            {
                EnableAllMenu(false);
                MessageBox.Show("Drawing File Not Found!", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                EnableAllMenu(true);
                NewWork();
            }
            return is_open_file;
        }

        public void NewWork()
        {
            try
            {
                list_file_write.Clear();
                //this.ProjectTitle = "";
            }
            catch (Exception ex) { }

        }
        public void SaveDataFile1(vdDocument doc)
        {
            sfd = new SaveFileDialog();
            sfd.Filter = "Text Files|*.txt";

            string cad_file = "";

            if (sfd.ShowDialog() != DialogResult.Cancel)
            {
                user_file = sfd.FileName;
            }
            else
                return;

            cad_file = Path.GetFileNameWithoutExtension(user_file);
            cad_file = cad_file + ".vdml";

            cad_file = Path.Combine(Path.GetDirectoryName(user_file), cad_file);

            StreamWriter sw = null;

            if (is_open_file)
            {
                sw = new StreamWriter(new FileStream(user_file, FileMode.Append));
            }
            else
            {
                sw = new StreamWriter(new FileStream(user_file, FileMode.Create));
            }

            //List<string> file_content = new List<string>(File.ReadAllLines(File_Name));
            List<string> file_mem_conn = new List<string>(File.ReadAllLines(File_Mem_Inci));


            try
            {
               
                if (!IsOpenFile)
                {
                    sw.WriteLine();
                    sw.WriteLine();
                    sw.WriteLine(ProjectTitle);
                    sw.WriteLine("UNIT " + MassUnit.ToString() + " " + LengthUnit.ToString());
                    for (int i = 0; i < file_mem_conn.Count; i++)
                    {
                        sw.WriteLine(file_mem_conn[i]);
                    }
                }
                if (list_file_write.Count > 0)
                {
                    //sw.WriteLine("*****************************************");
                    //sw.WriteLine("**     {0}", DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
                    //sw.WriteLine("*****************************************");
                }
                for (int i = 0; i < list_file_write.Count; i++)
                {
                    sw.WriteLine(list_file_write[i]);
                }
                list_file_write.Clear();
                Document.SaveAs(cad_file);

                if (IsMovingLoad)
                {
                    File.Copy(MovingLoadFile, Path.Combine(Path.GetDirectoryName(cad_file), Path.GetFileName(MovingLoadFile)));
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
        public void SaveDataFile(vdDocument doc)
        {
            sfd = new SaveFileDialog();
            sfd.Filter = "Text Files|*.txt";

            string cad_file = "";

            if (sfd.ShowDialog() != DialogResult.Cancel)
            {
                user_file = sfd.FileName;
            }
            else
                return;

            cad_file = Path.GetFileNameWithoutExtension(user_file);
            cad_file = cad_file + ".vdml";

            cad_file = Path.Combine(Path.GetDirectoryName(user_file), cad_file);

            StreamWriter sw = null;

            if (is_open_file)
            {
                sw = new StreamWriter(new FileStream(user_file, FileMode.Append));
            }
            else
            {
                sw = new StreamWriter(new FileStream(user_file, FileMode.Create));
            }

            //List<string> file_content = new List<string>(File.ReadAllLines(File_Name));
            List<string> file_mem_conn = new List<string>(File.ReadAllLines(File_Mem_Inci));
            list_file_write = Create_Data.Get_ASTRA_Data();

            try
            {

                if (!IsOpenFile)
                {
                    //sw.WriteLine();
                    //sw.WriteLine();
                    //sw.WriteLine(ProjectTitle);
                    //sw.WriteLine("UNIT " + MassUnit.ToString() + " " + LengthUnit.ToString());
                    //for (int i = 0; i < file_mem_conn.Count; i++)
                    //{
                    //    sw.WriteLine(file_mem_conn[i]);
                    //}
                }
                for (int i = 0; i < list_file_write.Count; i++)
                {
                    sw.WriteLine(list_file_write[i]);
                }
                list_file_write.Clear();
                Document.SaveAs(cad_file);

                if (IsMovingLoad)
                {
                    File.Copy(MovingLoadFile, Path.Combine(Path.GetDirectoryName(cad_file), Path.GetFileName(MovingLoadFile)));
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
        public void SaveDataFile(vdDocument doc, string file_name)
        {
            sfd = new SaveFileDialog();
            sfd.Filter = "Text Files|*.txt";

            string cad_file = "";

            //if (sfd.ShowDialog() != DialogResult.Cancel)
            //{
            user_file = file_name;
            //}
            //else
            //    return;

            cad_file = Path.GetFileNameWithoutExtension(user_file);
            cad_file = cad_file + ".vdml";

            cad_file = Path.Combine(Path.GetDirectoryName(user_file), cad_file);

            StreamWriter sw = null;

            if (is_open_file)
            {
                sw = new StreamWriter(new FileStream(user_file, FileMode.Append));
            }
            else
            {
                sw = new StreamWriter(new FileStream(user_file, FileMode.Create));
            }

            //List<string> file_content = new List<string>(File.ReadAllLines(File_Name));
            List<string> file_mem_conn = new List<string>(File.ReadAllLines(File_Mem_Inci));
            list_file_write = Create_Data.Get_ASTRA_Data();

            try
            {

                if (!IsOpenFile)
                {
                    //sw.WriteLine();
                    //sw.WriteLine();
                    //sw.WriteLine(ProjectTitle);
                    //sw.WriteLine("UNIT " + MassUnit.ToString() + " " + LengthUnit.ToString());
                    //for (int i = 0; i < file_mem_conn.Count; i++)
                    //{
                    //    sw.WriteLine(file_mem_conn[i]);
                    //}
                }
                for (int i = 0; i < list_file_write.Count; i++)
                {
                    sw.WriteLine(list_file_write[i]);
                }
                list_file_write.Clear();
                //Document.SaveAs(cad_file);

                //if (IsMovingLoad)
                //{
                //    File.Copy(MovingLoadFile, Path.Combine(Path.GetDirectoryName(cad_file), Path.GetFileName(MovingLoadFile)));
                //}
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        public void StructuralGeometry(vdDocument doc)
        {
            #region Menu Enable
            string mName = "Project Title";
            //MenuEnable(mName, false);

            //mName = "Joint Coordinates";
            //MenuEnable(mName, false);

            //mName = "Member Connectivity / Incidence";
            //MenuEnable(mName, false);


            //mName = "Member Truss";
            //MenuEnable(mName, false);


            //mName = "Member Release";
            //MenuEnable(mName, false);


            //mName = "Section Properties";
            //MenuEnable(mName, false);


            //mName = "Material Properties";
            //MenuEnable(mName, false);

            //mName = "Support";
            //MenuEnable(mName, false);

            //mName = "Load";
            //MenuEnable(mName, false);

            //mName = "Moving Load";
            //MenuEnable(mName, false);

            //mName = "Analysis Type";
            //MenuEnable(mName, false);

            //mName = "Show Data";
            //MenuEnable(mName, false);

            //mName = "Finish Statement";
            //MenuEnable(mName, false);


            #endregion

            try
            {
                File.WriteAllText(File_Mem_Inci, "");
                File.WriteAllText(File_Name, "");
                if (ASTRACAD.WriteAstraCode(doc, File_Mem_Inci))
                {
                    Create_Data.Geometry_Data.Clear();
                    Create_Data.Geometry_Data.AddRange(File.ReadAllLines(File_Mem_Inci));
                    if (doc.Layers.FindName("Members") != null)
                    {
                        doc.Layers.FindName("Members").Frozen = false;
                    }
                    astDoc = new ASTRADoc(File_Mem_Inci);
                    astDoc.Joints.DrawJointsText(doc, 0.1);
                    astDoc.Members.DrawMember(doc);
                    //VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);
                    doc.Redraw(true);
                    //D_Menu.File_Save_Open(doc);

                    kStr = "Structural Geometry is Created.";
                    kStr = "Data for Joint Coordinates and Member Connectivity / Incidence will be created.\n\n";
                    kStr += "                              Do you want to Save this Data?";

                    //if (MessageBox.Show(kStr, "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    //{
                    //    File.Delete(File_Mem_Inci);
                    //}
                }
                UserFile = "";
                NewWork();
                doc.Redraw(true);
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }


            EnableAllMenu(true);


            //mName = "Project Title";
            //MenuEnable(mName, true);

            //mName = "Show Data";
            //MenuEnable(mName, true);


            //mName = "Member Truss";
            //MenuEnable(mName, true);


            //mName = "Member Release";
            //MenuEnable(mName, true);


            //mName = "Section Properties";
            //MenuEnable(mName, true);


            //mName = "Material Properties";
            //MenuEnable(mName, true);

            //mName = "Support";
            //MenuEnable(mName, false);

            //mName = "Load";
            //MenuEnable(mName, true);

            //mName = "Moving Load";
            //MenuEnable(mName, true);

            //mName = "Analysis Type";
            //MenuEnable(mName, true);

            //mName = "Show Data";
            //MenuEnable(mName, true);

            //mName = "Finish Statement";
            //MenuEnable(mName, true);

        }

        public void JointCoordinates(vdDocument doc)
        {
            file_mem_inci = Path.Combine(Path.GetDirectoryName(File_Name), "MemInci.txt");

            frmNodeGridBox ndGrd = new frmNodeGridBox(this);
            ndGrd.Owner = MainForm;
            ndGrd.Show();
        }
        public void MemberIncidence(vdDocument doc, bool showDialog)
        {
            frmMemberGridBox miGrd = new frmMemberGridBox(this);
            miGrd.FormBorderStyle = FormBorderStyle.Sizable;
            //miGrd.MinimizeBox = true;
            //miGrd.ShowIcon = true;
            //miGrd.ShowInTaskbar = true;
            miGrd.Owner = MainForm;
            miGrd.StartPosition = FormStartPosition.CenterScreen;
            miGrd.Show();

        }

        public void MemberTruss(vdDocument doc)
        {
            frmMemberTruss f_mem_truss = new frmMemberTruss(this);
            f_mem_truss.Owner = MainForm;
            f_mem_truss.Show();
        }
        public void MemberRelease(vdDocument doc)
        {
            frmMemberRelease f_mem_release = new frmMemberRelease(this);
            f_mem_release.Owner = MainForm;
            f_mem_release.Show();
        }
        public void SectionProperties(vdDocument doc)
        {
            ReadData();
            frmSectionProperties f_sec_prop = new frmSectionProperties(this);
            f_sec_prop.Owner = MainForm;
            f_sec_prop.Show();
        }
        public void MaterialProperties(vdDocument doc)
        {
            ReadData();
            frmMaterialProperties f_mat_prop = new frmMaterialProperties(this);
            f_mat_prop.Owner = MainForm;
            f_mat_prop.Show();
        }
        public void Support(vdDocument doc)
        {
            ReadData();
            frmSupport f_support = new frmSupport(this);
            f_support.Owner = MainForm;
            f_support.Show();
        }

        public void Load(vdDocument doc)
        {
            ReadData();
            frmLoad f_Load = new frmLoad(this);
            f_Load.Owner = MainForm;
            f_Load.Show();
        }
        public void MovingLoad(vdDocument doc)
        {
            ReadData();
            frmMovingLoad f_MovingLoad = new frmMovingLoad(this);
            f_MovingLoad.Owner = MainForm;
            f_MovingLoad.Show();
        }

        public void AnalysisType(vdDocument doc)
        {
            ReadData();
            frmAnalysisType f_AnalysisType = new frmAnalysisType(this);
            f_AnalysisType.Owner = MainForm;
            f_AnalysisType.Show();
        }
        public void Finish(vdDocument doc)
        {
            ReadData();
            frmFinish f_Finish = new frmFinish(this);
            f_Finish.Owner = MainForm;
            f_Finish.Show();
        }
        public void Refresh(vdDocument doc)
        {
            kStr = "Do You want to Delete all Saved and Unsaved Data ?";

            if (MessageBox.Show(kStr, "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                list_file_write.Clear();
                File.Delete(File_Name);
                File.Delete(File_Mem_Inci);
                EnableAllMenu(false);
            }
        }

        public vdSelection GetGripSelection(bool Create)
        {
            VectorDraw.Professional.vdCollections.vdSelection gripset;
            string selsetname = "VDGRIPSET_" + doc.ActiveLayOut.Handle.ToStringValue() + (doc.ActiveLayOut.ActiveViewPort != null ? doc.ActiveLayOut.ActiveViewPort.Handle.ToStringValue() : "");
            gripset = doc.ActiveLayOut.Document.Selections.FindName(selsetname);
            if (Create)
            {
                if (gripset == null)
                {
                    gripset = doc.ActiveLayOut.Document.Selections.Add(selsetname);
                }
            }
            //vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Document.Selections.FindName(selsetname).RemoveAll();
            return gripset;
        }
        public void ReadData()
        {
            //ASTRACAD.WriteAstraCode(doc, File_Name);
            //astDoc = new ASTRADoc(File_Name);
            //astDoc.Members.DrawMember(doc);
        }
        public void Moving(vdDocument vdoc)
        {
            //ASTRACAD.WriteAstraCode(doc, File_Name);
            //astDoc = new ASTRADoc(File_Name);
            //astDoc.Members.DrawMember(doc);

            frmMovingLoadRun fmr = new frmMovingLoadRun(vdoc);
            fmr.Show();
        }


        #endregion

        #region IASTRACAD Members

        public string ProjectTitle
        {
            get
            {
                return proj_title;
            }
            set
            {
                proj_title = value;
            }
        }

        public ELengthUnits LengthUnit
        {
            get
            {
                return lenUnit;
            }
            set
            {
                lenUnit = value;
            }
        }

        public EMassUnits MassUnit
        {
            get
            {
                return massUnit;
            }
            set
            {
                massUnit = value;
            }
        }

        public ASTRADoc AstraDocument
        {
            get { return astDoc; }
            set { astDoc = value; }
        }

        #endregion

        #region IASTRACAD Members


        public DrawingMenu D_Menu
        {
            get
            {
                return d_menu;
            }
        }

        #endregion

        #region IASTRACAD Members


        public void JointCoordinates()
        {
        }

        #endregion

        #region IASTRACAD Members


        public Form MainForm
        {
            get
            {
                return frm_main;
            }
            set
            {
                frm_main = value;
                mStrip = frm_main.MainMenuStrip;
            }
        }

        #endregion

        #region IASTRACAD Members SetMenu
        public void SetMenu(string command_name, vdDocument doc)
        {
            //D_Menu.Menu(command_name, doc);
            Document = doc;
            string _menu = command_name.Replace("DD_", "").Trim().TrimEnd().TrimStart();
            try
            {
                #region Switch Case
                switch (_menu)
                {
                    case "OpenDataFile":
                        OpenDataFile(doc);
                        break;
                    case "ProjectTitle":
                        SetProjectTitle();
                        break;
                    case "SaveDataFile":
                        SaveDataFile(doc);
                        break;
                    case "StructuralGeometry":
                        StructuralGeometry(doc);
                        break;
                    case "JointCoordinates":
                        JointCoordinates(doc);
                        break;
                    case "MemberIncidence":
                        MemberIncidence(doc, true);
                        break;
                    case "MemberTruss":
                        MemberTruss(doc);
                        break;
                    case "MemberRelease":
                        MemberRelease(doc);
                        break;
                    case "SectionProperties":
                        SectionProperties(doc);
                        break;
                    case "MaterialProperties":
                        MaterialProperties(doc);
                        break;
                    case "Support":
                        Support(doc);
                        break;
                    case "Load":
                        Load(doc);
                        break;
                    case "MovingLoad":
                        MovingLoad(doc);
                        break;
                    case "AnalysisType":
                        AnalysisType(doc);
                        break;
                    case "ShowData":
                        //System.Diagnostics.Process.Start(File_Name);
                        ShowData();
                        break;
                    case "Finish":
                        Finish(doc);
                        break;
                    case "Refresh":
                        Refresh(doc);
                        break;
                    case "Moving":
                        Moving(doc);
                        break;
                }
                #endregion
            }
            catch (Exception ex) { }
        }
        #endregion

        public void ShowData()
        {
            string file_path = Path.GetDirectoryName(File_Mem_Inci);

            //if (UserFile != "")
            //{
            //    file_path = UserFile;
            //}
            //else
            //{
            try
            {
                file_path = Path.GetDirectoryName(File_Mem_Inci);

                file_path = Path.Combine(file_path, "Whole_Data.txt");

                File.WriteAllLines(file_path, Create_Data.Get_ASTRA_Data().ToArray());
                System.Diagnostics.Process.Start(file_path);
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }
            //}
        }

        #region IASTRACAD Members

        public vdDocument Document
        {
            get
            {
                return doc;
            }
            set
            {
                doc = value;
            }
        }
        public string File_Name
        {
            get
            {
                //Environment.SpecialFolder.

                //Environment.SpecialFolder.ApplicationData


                string kStr = "";
                //kStr = Environment.CurrentDirectory;
                //kStr = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                kStr = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                //string file_name = Path.Combine(Application.StartupPath, "DrawCAD");
                string file_name = Path.Combine(kStr, "DrawCAD");
                if (!Directory.Exists(file_name))
                {
                    Directory.CreateDirectory(file_name);
                }

                file_name = Path.Combine(file_name, "DrawCAD.txt");
                return file_name;
            }
        }

        public bool IsMovingLoad
        {
            get
            {
                return is_moving_load;
            }
            set
            {
                is_moving_load = value;
            }
        }
        public string UserFile
        {
            get
            {
                return user_file;
            }
            set
            {
                user_file = value;
            }
        }

        #endregion

        #region IASTRACAD Members

        public void SaveUserFile()
        {
        }
        public bool OpenUserFile()
        {
            return false;
        }
        public void WriteFile(string txt)
        {
            WriteFile(txt, true);
        }
        public void WriteFile(string txt, bool isAppend)
        {
            StreamWriter sw = null;

            if (isAppend)
                sw = new StreamWriter(new FileStream(File_Name, FileMode.Append));
            else
                sw = new StreamWriter(new FileStream(File_Name, FileMode.Create));

            if(!File.Exists(File_Name))
                sw = new StreamWriter(new FileStream(File_Name, FileMode.Create));
            try
            {
                sw.WriteLine(txt);
                list_file_write.Add(txt);
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        public string File_Mem_Inci
        {
            get
            {
                file_mem_inci = Path.Combine(Path.GetDirectoryName(File_Name), "MemInci.txt");
                return file_mem_inci;
            }
        }

        
        #endregion

        #region IASTRACAD Members


        public bool IsOpenFile
        {
            get
            {
                return is_open_file;
            }
            set
            {
                is_open_file = value;
            }
        }

        #endregion


        #region IASTRACAD Members


        public string MovingLoadFile
        {
            get
            {
                return file_moving_load;
            }
            set
            {
                file_moving_load = value;
            }
        }

        #endregion

        #region Selected Joint Numbers
        public string GetSelectedJointsInText()
        {
            string kStr = "";
            List<vdText> list_joints = new List<vdText>();
            List<int> list_joint_No = new List<int>();
            vdSelection gripset = GetGripSelection(false);


            //if (gripset.Count > 1) return;
            vdText vTxt;
            int jnt_no = -1;
            foreach (vdFigure fig in gripset)
            {
                vTxt = fig as vdText;
                if (vTxt != null)
                {
                    if (vTxt.Layer.Name == "Nodes")
                        list_joints.Add(vTxt);
                }
            }
            foreach (vdText line in list_joints)
            {
                jnt_no = GetJointNo(line);
                if (jnt_no != -1)
                    list_joint_No.Add(jnt_no);
            }
            list_joint_No.Sort();

            //for (int i = 0; i < list_joint_No.Count; i++)
            //{
            //    if (i == list_joint_No.Count - 1)
            //    {
            //        kStr += list_joint_No[i].ToString();
            //    }
            //    else
            //    {
            //        kStr += list_joint_No[i].ToString() + ",";
            //    }
            //}


            int prev_no = -1;

            //for (int i = 0; i < list_joint_No.Count; i++)
            //{
            //    if (i == 0)
            //    {
            //        prev_no = list_joint_No[0];
            //        continue;
            //    }
            //    prev_no++;
            //    try
            //    {
            //        if (prev_no != list_joint_No[i])
            //        {
            //            prev_no = -1;
            //            break;
            //        }
            //    }
            //    catch (Exception ex) { }
            //}

            //if (prev_no != -1 && list_joint_No.Count != 2)
            //{
            //    kStr = list_joint_No[0] + " TO " + list_joint_No[list_joint_No.Count - 1].ToString();
            //}

            //if (list_joint_No.Count == astDoc.Joints.Count) kStr = "ALL";

            kStr = MyStrings.Get_Array_Text(list_joint_No);

            list_joints.Clear();
            list_joint_No.Clear();
            //gripset.RemoveAll();

            list_joints = null;
            list_joint_No = null;
            gripset = null;
            doc.Redraw(true);

            return kStr;

        }
        public int GetJointNo(vdText txt)
        {
            //int mNo = -1;
            for (int i = 0; i < astDoc.Joints.Count; i++)
            {
                if (txt.InsertionPoint == astDoc.Joints[i].Point)
                {
                    return astDoc.Joints[i].NodeNo;
                }
            }
            return MyStrings.StringToInt(txt.TextString, -1);

            return -1;
        }
        #endregion
        #region Selected Member Number

        public string GetSelectedMembersInText()
        {
            string mem_text = "";
            List<vdLine> list_lines = new List<vdLine>();
            List<vdText> list_text = new List<vdText>();
            vdSelection gripset = GetGripSelection(false);
            List<int> list_mem_No = new List<int>();
            vdLine ln;
            vdText txt;

            if (gripset == null) return "";
            if (gripset.Count == 0) return "";
            int memNo = -1;

            foreach (vdFigure fig in gripset)
            {
                ln = fig as vdLine;
                if (ln != null)
                {
                    list_lines.Add(ln);
                }
                else
                {
                    txt = fig as vdText;
                    if (txt != null)
                    {
                        if (txt.Layer.Name.ToLower() == "members")
                        {
                            memNo = MyStrings.StringToInt(txt.TextString, -1);
                            if (memNo != -1)
                            {
                                if (list_mem_No.Contains(memNo))
                                    list_mem_No.Remove(memNo);
                                list_mem_No.Add(memNo);
                            }
                        }
                    }
                }
            }
            while (list_mem_No.Contains(-1)) list_mem_No.Remove(-1);

            int mno = 0;
            foreach (vdLine line in list_lines)
            {
                mno = GetMemberNo(line);
                if (mno != -1)
                {
                    if (list_mem_No.Contains(mno))
                        list_mem_No.Remove(mno);
                    list_mem_No.Add(mno);
                }
            }

            list_mem_No.Sort();


            return MyStrings.Get_Array_Text(list_mem_No);


            if (list_mem_No.Count == astDoc.Members.Count)
            {
                return "ALL";
            }

            for (int i = 0; i < list_mem_No.Count; i++)
            {
                if (i == list_mem_No.Count - 1)
                {
                    mem_text += list_mem_No[i].ToString();
                }
                else
                {
                    mem_text += list_mem_No[i].ToString() + ",";
                }
            }

            int prev_no = -1;

            for (int i = 0; i < list_mem_No.Count; i++)
            {
                if (i == 0)
                {
                    prev_no = list_mem_No[0];
                    continue;
                }
                prev_no++;
                try
                {
                    if (prev_no != list_mem_No[i])
                    {
                        prev_no = -1;
                        break;
                    }
                }
                catch (Exception ex) { }
            }

            if (prev_no != -1 && list_mem_No.Count > 2)
            {
                mem_text = list_mem_No[0] + " TO " + list_mem_No[list_mem_No.Count - 1].ToString();
            }

            list_lines.Clear();
            list_mem_No.Clear();
            //gripset.RemoveAll();


            doc.Redraw(true);
            return mem_text;
        }
        public int GetMemberNo(vdLine line)
        {
            //int mNo = -1;
            for (int i = 0; i < astDoc.Members.Count; i++)
            {
                if (astDoc.Members[i].StartNode.Point == line.StartPoint &&
                    astDoc.Members[i].EndNode.Point == line.EndPoint)
                {
                    return astDoc.Members[i].MemberNo;
                }
                else if (astDoc.Members[i].StartNode.Point == line.EndPoint &&
                     astDoc.Members[i].EndNode.Point == line.StartPoint)
                {
                    return astDoc.Members[i].MemberNo;
                }
            }
            return -1;
        }


        #endregion


        public void MenuEnable(string menuName, bool isEnable)
        {
            try
            {
                ToolStripMenuItem tsmi = mStrip.Items["Drawing To Data"] as ToolStripMenuItem;
                tsmi = tsmi.DropDown.Items[menuName] as ToolStripMenuItem;
                //tsmi = tsmi.DropDown.Items["Joint Coordinates"] as ToolStripMenuItem;
                tsmi.Enabled = isEnable;
            }
            catch (Exception x) { }
        }
        public MenuStrip Menu
        {
            get
            {
                return mStrip;
            }
            set 
            {
                mStrip = value;
            }
        }


        #region IASTRACAD Members


        public CREATE_ASTRA_Data Create_Data { get; set; }

        #endregion

        #region IASTRACAD Members


        public void SaveUserFile(vdDocument doc, string file_name)
        {
            SaveDataFile(doc, file_name);
        }

        #endregion
    }
    
    #region Public Variables
    public enum CompareField
    {
        X,
        Y,
        Z
    }
    #endregion
    class CompareClass : IComparer
    {
        #region Private Variables
        private CompareField sortBy = CompareField.X;
        #endregion

        #region Properties
        public CompareField SortBy
        {
            get
            {
                return sortBy;
            }
            set
            {
                sortBy = value;
            }
        }
        #endregion

        #region Constructor

        public CompareClass()
        {
            //default constructor
        }

        public CompareClass(CompareField pSortBy)
        {
            sortBy = pSortBy;
        }

        #endregion

        #region Methods
        public Int32 Compare(Object pFirstObject, Object pObjectToCompare)
        {
            if (pFirstObject is gPoint)
            {
                switch (this.sortBy)
                {
                    case CompareField.X:
                        return Comparer.DefaultInvariant.Compare(((gPoint)pFirstObject).x, ((gPoint)pObjectToCompare).x);
                        break;
                    case CompareField.Y:
                        return Comparer.DefaultInvariant.Compare(((gPoint)pFirstObject).y, ((gPoint)pObjectToCompare).y);
                        break;
                    case CompareField.Z:
                        return Comparer.DefaultInvariant.Compare(((gPoint)pFirstObject).z, ((gPoint)pObjectToCompare).z);
                        break;
                    default:
                        return 0;
                        break;
                }
            }
            else
                return 0;
        }
        #endregion
    }

    
}
