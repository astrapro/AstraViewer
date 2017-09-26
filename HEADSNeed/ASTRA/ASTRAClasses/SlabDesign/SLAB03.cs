using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Geometry;


namespace HEADSNeed.ASTRA.ASTRAClasses.SlabDesign
{
    public class SLAB03
    {
        double main_dia;
        double dist_dia;
        double dist_y;
        double end_span_reinforcement;
        double span_bredth;
        double clear_cover;
        double start_center_distance;
        double start_wall_distance;
        double interior_reinforcement;
        double support_reinforcement;
        double upper_distance;
        double lower_distance;
        double total_span;
        double start_blueline_X1;
        double start_blueline_X2;
        double start_redline_X1;
        double start_redline_X2;
        double wall_thickness;
        double depth;
        double start_span;
        double mid_span;
        double bar_hill;
        double pillar_length;
        double dist_bar_spacing;
        double start_greenline_X1;
        double start_greenline_X2;
        Drawing drw = null;

        List<double> lengths = null;
        vdLine line = null;
        vdCircle cir = null;
        string filename = "";
        public SLAB03(string file_name)
        {
          
            filename = file_name;

            end_span_reinforcement = 0.0;
            span_bredth = 0.0;
            clear_cover = 0.0;
            start_center_distance = 0.0;
            start_wall_distance = 0.0;
            total_span = 0.0;
            wall_thickness = 0.0;
            lengths = new List<double>();
            line = new vdLine();
            cir = new vdCircle();
            start_blueline_X1 = 0.0;
            start_blueline_X2 = 0.0;
            start_redline_X1 = 0.0;
            start_redline_X2 = 0.0;
            bar_hill = 0.0;
            dist_bar_spacing = 170;

            main_dia = 8.0d; // 8 mm.
            dist_dia = 6.0d; // 6 mm.
            dist_y = 0.0;
            ReadFromFile(file_name);

        }
        private void InitializeVariable()
        {
            //main_dia = 8.0d;
            //dist_dia = 6.0d;
            //total_span = 9000;
            //start_span = 3000;
            //mid_span = 3000;
            //span_bredth = 8000;
            //end_span_reinforcement = 340;
            ////end_span_reinforcement = 130;
            //support_reinforcement = 140;
            //interior_reinforcement = 480;

            //start_center_distance = 300;
            //start_wall_distance = 450;
            //clear_cover = 15;
            //upper_distance = 900;
            //lower_distance = 600;
            //depth = 500;
            //wall_thickness = 250;
            //bar_hill = 25.4;

            //pillar_length = 700;
            ////dist_y = main_dia + dist_dia / 2 + clear_cover;
            ////dist_y = main_dia / 2 + dist_dia + clear_cover;
            ////dist_y = main_dia / 2 + dist_dia / 2 + clear_cover;
            ////dist_y = clear_cover - (main_dia / 2 + dist_dia / 2);
            //dist_y = (main_dia / 2 + dist_dia / 2);

            ////lengths = new List<double>() { 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000, 3000 };
            //lengths = new List<double>() { 3000, 3000, 3000, 3000, 3000 };
            //lengths = new List<double>() { 3000, 3000 };
        }
        public void ReadFromFile(string file_name)
        {
            //ASTRA SLAB03

            //lengths = 3000, 3000, 3000, 3000, 3000
            //span_bredth = 8000;
            //main_dia = 8.0d;
            //dist_dia = 6.0d;
            //end_span_reinforcement = 340;
            //support_reinforcement = 140;
            //interior_reinforcement = 480;
            //start_center_distance = 300;
            //start_wall_distance = 450;
            //clear_cover = 15;
            //upper_distance = 900;
            //lower_distance = 600;
            //height = 500;
            //wall_thickness = 250;
            //bar_hill = 25.4;
            //pillar_length = 700;

            //FINISH

            //file_name = @"D:\SOFTWARE TESTING\slab03.txt";
            List<string> lstStr = new List<string>(File.ReadAllLines(file_name));
            string kStr = "";
            MyStrings mList = null;
            bool isSlab03 = false;
            for (int i = 0; i < lstStr.Count; i++)
            {
                kStr = lstStr[i].Trim().TrimEnd().TrimStart().ToLower().Replace(';', ' ').Replace('=', ' ').Replace(',', ' ');
                kStr = MyStrings.RemoveAllSpaces(kStr);
                mList = new MyStrings(kStr, ' ');
                if (kStr == "slab design 03")
                    isSlab03 = true;
                else if (kStr == "finish")
                {
                    isSlab03 = false;
                    return;
                }
                if (!isSlab03) continue;

                switch (mList.StringList[0])
                {
                    case "lengths":
                        {
                            for (int indx = 1; indx < mList.Count; indx++)
                            {
                                lengths.Add(mList.GetDouble(indx)*1000);
                            }
                        }
                        break;
                    case "span_bredth":
                        {
                            span_bredth = mList.GetDouble(1)*1000;
                        }
                        break;
                    case "main_dia":
                        {
                            main_dia = mList.GetDouble(1);
                        }
                        break;
                    case "dist_dia":
                        {
                            dist_dia = mList.GetDouble(1);
                        }
                        break;
                    case "end_span_reinforcement":
                        {
                            end_span_reinforcement = mList.GetDouble(1);
                        }
                        break;
                    case "support_reinforcement":
                        {
                            support_reinforcement = mList.GetDouble(1);
                        }
                        break;
                    case "interior_reinforcement":
                        {
                            interior_reinforcement = mList.GetDouble(1);
                        }
                        break;
                    case "start_center_distance":
                        {
                            start_center_distance = mList.GetDouble(1);
                        }
                        break;
                    case "start_wall_distance":
                        {
                            start_wall_distance = mList.GetDouble(1);
                        }
                        break;
                    case "clear_cover":
                        {
                            clear_cover = mList.GetDouble(1);
                        }
                        break;
                    case "upper_distance":
                        {
                            upper_distance = mList.GetDouble(1);
                        }
                        break;
                    case "lower_distance":
                        {
                            lower_distance = mList.GetDouble(1);
                        }
                        break;
                    case "depth":
                        {
                            depth = mList.GetDouble(1);
                        }
                        break;
                    case "wall_thickness":
                        {
                            wall_thickness = mList.GetDouble(1);
                        }
                        break;
                    case "bar_hill":
                        {
                            bar_hill = mList.GetDouble(1);
                        }
                        break;
                    case "pillar_length":
                        {
                            pillar_length = mList.GetDouble(1);
                        }
                        break;
                }

                dist_y = main_dia / 2 + dist_dia / 2;
                
            }
        }

        public void Draw_View_Single_Span(vdDocument doc)
        {
            vdLine line = new vdLine();

            total_span = 9000;
            double start_span = 3000;
            double mid_span = 3000;

            end_span_reinforcement = 340;
            support_reinforcement = 140;
            interior_reinforcement = 480;

            start_center_distance = 300;
            start_wall_distance = 450;
            clear_cover = 15;
            upper_distance = 900;
            lower_distance = 600;
            double height = 500;

            bar_hill = 25.4;

            double start_blueline_dist = 0.0;
            double start_blueline_length = 0.0;
            double start_redline_dist = 0.0;
            double start_redline_length = 0.0;


            for (int i = 0; i < span_bredth; i += 100)
            {

                start_blueline_dist = clear_cover;
                start_blueline_length = start_span - 600 - clear_cover;
                #region Blue End Span 1

                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_blueline_dist, clear_cover, i);
                line.EndPoint = new gPoint(start_blueline_length, clear_cover, i);
                line.PenColor = new vdColor(Color.Blue);
                doc.ActiveLayOut.Entities.AddItem(line);




                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_blueline_length, clear_cover, i);
                line.EndPoint = new gPoint(start_blueline_length + bar_hill / 2, clear_cover + bar_hill, i);
                line.PenColor = new vdColor(Color.Blue);
                doc.ActiveLayOut.Entities.AddItem(line);


                //line = new vdLine();
                //line.SetUnRegisterDocument(doc);
                //line.setDocumentDefaults();
                //line.StartPoint = new gPoint(start_blueline_dist, clear_cover, i);
                //line.EndPoint = new gPoint(start_blueline_dist - bar_hill / 2, clear_cover + bar_hill, i);
                //line.PenColor = new vdColor(Color.Blue);
                //doc.ActiveLayOut.Entities.AddItem(line);

                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_blueline_dist, clear_cover, i);
                line.EndPoint = new gPoint(start_blueline_dist, height - clear_cover, i);
                line.PenColor = new vdColor(Color.Blue);
                doc.ActiveLayOut.Entities.AddItem(line);
                #endregion

                start_redline_dist = clear_cover + 300;
                start_redline_length = start_span;

                #region Red End Span 1

                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_redline_dist, clear_cover, i + 50);
                line.EndPoint = new gPoint(start_redline_length, clear_cover, i + 50);
                line.PenColor = new vdColor(Color.Red);
                doc.ActiveLayOut.Entities.AddItem(line);


                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_redline_dist, clear_cover, i + 50);
                line.EndPoint = new gPoint(start_redline_dist - bar_hill / 2, clear_cover + bar_hill, i + 50);
                line.PenColor = new vdColor(Color.Red);
                doc.ActiveLayOut.Entities.AddItem(line);

                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_redline_length, clear_cover, i + 50);
                line.EndPoint = new gPoint(start_redline_length + bar_hill / 2, clear_cover + bar_hill, i + 50);
                line.PenColor = new vdColor(Color.Red);
                doc.ActiveLayOut.Entities.AddItem(line);


                #endregion

                start_blueline_dist = start_span + 600;
                start_blueline_length = start_span * 2 - bar_hill / 2;
                #region Blue End Span 2

                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_blueline_dist, clear_cover, i);
                line.EndPoint = new gPoint(start_blueline_length, clear_cover, i);
                line.PenColor = new vdColor(Color.Blue);
                doc.ActiveLayOut.Entities.AddItem(line);



                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_blueline_dist, clear_cover, i);
                line.EndPoint = new gPoint(start_blueline_dist - bar_hill / 2, clear_cover + bar_hill, i);
                line.PenColor = new vdColor(Color.Blue);
                doc.ActiveLayOut.Entities.AddItem(line);



                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_blueline_length, clear_cover, i);
                line.EndPoint = new gPoint(start_blueline_length + bar_hill / 2, clear_cover + bar_hill, i);
                line.PenColor = new vdColor(Color.Blue);
                doc.ActiveLayOut.Entities.AddItem(line);

                #endregion

                start_redline_dist = start_span;
                start_redline_length = 2 * start_span - 600;
                #region Red End Span 2

                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_redline_dist + bar_hill, clear_cover, i + 50);
                line.EndPoint = new gPoint(start_redline_length, clear_cover, i + 50);
                line.PenColor = new vdColor(Color.Red);
                doc.ActiveLayOut.Entities.AddItem(line);


                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_redline_dist + bar_hill, clear_cover, i + 50);
                line.EndPoint = new gPoint(start_redline_dist + bar_hill - bar_hill / 2, clear_cover + bar_hill, i + 50);
                line.PenColor = new vdColor(Color.Red);
                doc.ActiveLayOut.Entities.AddItem(line);


                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_redline_length, clear_cover, i + 50);
                line.EndPoint = new gPoint(start_redline_length + bar_hill / 2, clear_cover + bar_hill, i + 50);
                line.PenColor = new vdColor(Color.Red);
                doc.ActiveLayOut.Entities.AddItem(line);



                #endregion


                start_blueline_dist = 2 * start_span + 600;
                start_blueline_length = total_span - clear_cover;

                #region Blue End Span 3

                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_blueline_dist, clear_cover, i);
                line.EndPoint = new gPoint(start_blueline_length, clear_cover, i);
                line.PenColor = new vdColor(Color.Blue);
                doc.ActiveLayOut.Entities.AddItem(line);


                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_blueline_dist, clear_cover, i);
                line.EndPoint = new gPoint(start_blueline_dist - bar_hill / 2, clear_cover + bar_hill, i);
                line.PenColor = new vdColor(Color.Blue);
                doc.ActiveLayOut.Entities.AddItem(line);

                //line = new vdLine();
                //line.SetUnRegisterDocument(doc);
                //line.setDocumentDefaults();
                //line.StartPoint = new gPoint(start_blueline_length, clear_cover, i);
                //line.EndPoint = new gPoint(start_blueline_length + bar_hill / 2, clear_cover + bar_hill, i);
                //line.PenColor = new vdColor(Color.Blue);
                //doc.ActiveLayOut.Entities.AddItem(line);


                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_blueline_length, clear_cover, i);
                line.EndPoint = new gPoint(start_blueline_length, height - clear_cover, i);
                line.PenColor = new vdColor(Color.Blue);
                doc.ActiveLayOut.Entities.AddItem(line);
                #endregion

                start_redline_dist = start_span * 2 + bar_hill / 2;
                start_redline_length = total_span - 300 - clear_cover;
                #region Red End Span 3

                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_redline_dist, clear_cover, i + 50);
                line.EndPoint = new gPoint(start_redline_length, clear_cover, i + 50);
                line.PenColor = new vdColor(Color.Red);
                doc.ActiveLayOut.Entities.AddItem(line);

                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_redline_dist, clear_cover, i + 50);
                line.EndPoint = new gPoint(start_redline_dist - bar_hill / 2, clear_cover + bar_hill, i + 50);
                line.PenColor = new vdColor(Color.Red);
                doc.ActiveLayOut.Entities.AddItem(line);


                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_redline_length, clear_cover, i + 50);
                line.EndPoint = new gPoint(start_redline_length + bar_hill / 2, clear_cover + bar_hill, i + 50);
                line.PenColor = new vdColor(Color.Red);
                doc.ActiveLayOut.Entities.AddItem(line);

                #endregion



                #region TOP Bars 1

                start_blueline_dist = clear_cover;
                start_blueline_length = 450 + 300 / 2;

                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_blueline_dist, height - clear_cover, i);
                line.EndPoint = new gPoint(start_blueline_length, height - clear_cover, i);
                doc.ActiveLayOut.Entities.AddItem(line);



                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_blueline_length, height - clear_cover, i);
                line.EndPoint = new gPoint(start_blueline_length + bar_hill / 2, height - clear_cover - bar_hill, i);
                doc.ActiveLayOut.Entities.AddItem(line);

                #endregion


                #region TOP Bars 2

                start_blueline_dist = start_span - 900;
                start_blueline_length = start_span + 900;

                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_blueline_dist, height - clear_cover, i);
                line.EndPoint = new gPoint(start_blueline_length, height - clear_cover, i);
                doc.ActiveLayOut.Entities.AddItem(line);


                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_blueline_dist, height - clear_cover, i);
                line.EndPoint = new gPoint(start_blueline_dist - bar_hill / 2, height - clear_cover - bar_hill, i);
                doc.ActiveLayOut.Entities.AddItem(line);

                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_blueline_length, height - clear_cover, i);
                line.EndPoint = new gPoint(start_blueline_length + bar_hill / 2, height - clear_cover - bar_hill, i);
                doc.ActiveLayOut.Entities.AddItem(line);

                #endregion

                #region TOP Bars 3
                start_blueline_dist = 2 * start_span - 900;
                start_blueline_length = 2 * start_span + 900;

                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_blueline_dist, height - clear_cover, i);
                line.EndPoint = new gPoint(start_blueline_length, height - clear_cover, i);
                doc.ActiveLayOut.Entities.AddItem(line);

                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_blueline_dist, height - clear_cover, i);
                line.EndPoint = new gPoint(start_blueline_dist - bar_hill / 2, height - clear_cover - bar_hill, i);
                doc.ActiveLayOut.Entities.AddItem(line);

                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_blueline_length, height - clear_cover, i);
                line.EndPoint = new gPoint(start_blueline_length + bar_hill / 2, height - clear_cover - bar_hill, i);
                doc.ActiveLayOut.Entities.AddItem(line);
                #endregion


                #region TOP Bars 4
                start_blueline_dist = total_span - clear_cover - 450;
                start_blueline_length = total_span - clear_cover;

                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_blueline_dist, height - clear_cover, i);
                line.EndPoint = new gPoint(start_blueline_length, height - clear_cover, i);
                doc.ActiveLayOut.Entities.AddItem(line);



                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_blueline_dist, height - clear_cover, i);
                line.EndPoint = new gPoint(start_blueline_dist - bar_hill / 2, height - clear_cover - bar_hill, i);
                doc.ActiveLayOut.Entities.AddItem(line);
                #endregion

            }

            doc.Redraw(true);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);


        }

        public void Draw_View_Multy_Span(vdDocument doc)
        {
            //InitializeVariable();
            for (int z = 0; z <= span_bredth; z += (int)end_span_reinforcement)
            {
                Draw_Start(doc,z);
                for (int i = 1; i < lengths.Count; i++)
                {
                    Draw_Mid_Face(doc,i-1);
                    Draw_Distribution_Bars(doc, i - 1);
                    Draw_Top_Pillar_Distribution_Bars(doc, i - 1);
                }

                Draw_End(doc,z);
            }
            // DRAW SUPPORT
            for (int z = 0; z <= span_bredth; z += (int)support_reinforcement)
            {
                for (int i = 1; i < lengths.Count; i++)
                {
                    Draw_Support(doc, i, z);
                }
            }

            //
            for (int z = 0; z <= span_bredth; z += (int)interior_reinforcement)
            {
                for (int i = 1; i < lengths.Count - 1; i++)
                {
                    Draw_Middle(doc, i, z);
                }
            }


            //Draw_Start_Face(doc);
            Draw_Face(doc);
            Draw_Start_Distribution_Bars(doc);
            Draw_End_Distribution_Bars(doc);
            Draw_Top_Distribution_Bars(doc);
            Draw_Circle(doc);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);
            doc.Redraw(true);
        }
        public void Draw_Start_Distribution_Bars(vdDocument doc)
        {
            start_greenline_X1 = wall_thickness;
            start_greenline_X2 = lengths[0] - wall_thickness / 2;

            int bar_nos = (int)((start_greenline_X2 - start_greenline_X1) / dist_bar_spacing);

            for (int i = 0; i <= bar_nos; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(wall_thickness + (i * dist_bar_spacing), clear_cover + dist_y, 0);
                line.EndPoint = new gPoint(wall_thickness + (i * dist_bar_spacing), clear_cover + dist_y, span_bredth);
                line.PenColor = new vdColor(Color.Green);
                doc.ActiveLayOut.Entities.AddItem(line);
            }
        }
        public void Draw_End_Distribution_Bars(vdDocument doc)
        {
            start_greenline_X1 = TotalLength - wall_thickness - 450;
            start_greenline_X2 = TotalLength - wall_thickness;

            int bar_nos = (int)((start_greenline_X2 - start_greenline_X1) / dist_bar_spacing);

            for (int i = 0; i <= bar_nos; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_greenline_X2 - (i * dist_bar_spacing), depth - clear_cover - dist_y, 0);
                line.EndPoint = new gPoint(start_greenline_X2 - (i * dist_bar_spacing), depth - clear_cover - dist_y, span_bredth);
                line.PenColor = new vdColor(Color.Green);
                doc.ActiveLayOut.Entities.AddItem(line);
            }
        }
        public void Draw_Distribution_Bars(vdDocument doc, int span_no)
        {

            start_greenline_X1 = GetLengthSum(0, span_no) + wall_thickness / 2;
            start_greenline_X2 = GetLengthSum(0, span_no + 1) - wall_thickness / 2;

            int bar_nos = (int)((start_greenline_X2 - start_greenline_X1) / dist_bar_spacing);

            for (int i = 0; i <= bar_nos; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_greenline_X1 + (i * dist_bar_spacing), clear_cover + dist_y, 0);
                line.EndPoint = new gPoint(start_greenline_X1 + (i * dist_bar_spacing), clear_cover + dist_y, span_bredth);
                line.PenColor = new vdColor(Color.Green);
                doc.ActiveLayOut.Entities.AddItem(line);
            }
        }
        public void Draw_Top_Pillar_Distribution_Bars(vdDocument doc, int span_no)
        {
            start_greenline_X1 = GetLengthSum(0, span_no) - 900;
            start_greenline_X2 = GetLengthSum(0, span_no) + 900;

            int bar_nos = (int)((start_greenline_X2 - start_greenline_X1) / dist_bar_spacing);

            for (int i = 0; i <= bar_nos; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_greenline_X1 + (i * dist_bar_spacing), depth - clear_cover - dist_y, 0);
                line.EndPoint = new gPoint(start_greenline_X1 + (i * dist_bar_spacing), depth - clear_cover - dist_y, span_bredth);
                line.PenColor = new vdColor(Color.Green);
                doc.ActiveLayOut.Entities.AddItem(line);
            }
        }
        public void Draw_Top_Distribution_Bars(vdDocument doc)
        {

            start_greenline_X1 = wall_thickness;
            start_greenline_X2 = wall_thickness + 450;

            int bar_nos = (int)((start_greenline_X2 - start_greenline_X1) / dist_bar_spacing);

            for (int i = 0; i <= bar_nos; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_greenline_X1 + (i * dist_bar_spacing), depth - clear_cover - dist_y, 0);
                line.EndPoint = new gPoint(start_greenline_X1 + (i * dist_bar_spacing), depth - clear_cover + dist_y, span_bredth);
                line.PenColor = new vdColor(Color.Green);
                doc.ActiveLayOut.Entities.AddItem(line);
            }
        }

        private void Draw_Start(vdDocument doc, double z)
        {
            start_blueline_X1 = clear_cover;
            start_blueline_X2 = lengths[0] - 600;

            #region Blue Line
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(start_blueline_X1, clear_cover, z);
            line.EndPoint = new gPoint(start_blueline_X2 - bar_hill / 2, clear_cover, z);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(start_blueline_X2 - bar_hill / 2, clear_cover, z);
            line.EndPoint = new gPoint(start_blueline_X2, clear_cover + bar_hill, z);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(start_blueline_X1, clear_cover, z);
            line.EndPoint = new gPoint(start_blueline_X1, depth - clear_cover, z);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);


            start_blueline_X2 = start_blueline_X1 + wall_thickness + 450 - clear_cover;
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(start_blueline_X1, depth - clear_cover, z);
            line.EndPoint = new gPoint(start_blueline_X2, depth - clear_cover, z);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(start_blueline_X2, depth - clear_cover, z);
            line.EndPoint = new gPoint(start_blueline_X2 + bar_hill/2, depth - clear_cover - bar_hill, z);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);

            #endregion

            start_redline_X1 = clear_cover + 300 + bar_hill/2;
            start_redline_X2 = lengths[0] - bar_hill/2;

            #region Red Line

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(start_redline_X1, clear_cover, z + (end_span_reinforcement / 2));
            line.EndPoint = new gPoint(start_redline_X2, clear_cover, z + (end_span_reinforcement / 2));
            line.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(line);



            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(start_redline_X1, clear_cover, z + (end_span_reinforcement / 2));
            line.EndPoint = new gPoint(start_redline_X1 - bar_hill / 2, clear_cover + bar_hill, z + (end_span_reinforcement / 2));
            line.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(line);

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(start_redline_X2, clear_cover, z + (end_span_reinforcement / 2));
            line.EndPoint = new gPoint(start_redline_X2 + bar_hill/2, clear_cover + bar_hill, z + (end_span_reinforcement / 2));
            line.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(line);


            #endregion
        }
        private void Draw_Middle(vdDocument doc, int span_no, double z)
        {
            //int span_no = 0;

            //start_blueline_X1 = lengths[span_no] + 600;
            //start_blueline_X2 = lengths[span_no + 1];


            start_blueline_X1 = GetLengthSum(0,span_no - 1) + 600 + bar_hill/2;
            start_blueline_X2 = GetLengthSum(0, span_no) - bar_hill/2;

            #region Blue Line
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(start_blueline_X1, clear_cover, z);
            line.EndPoint = new gPoint(start_blueline_X2, clear_cover, z);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(start_blueline_X1, clear_cover, z);
            line.EndPoint = new gPoint(start_blueline_X1 - bar_hill/2, clear_cover + bar_hill, z);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(start_blueline_X2, clear_cover, z);
            line.EndPoint = new gPoint(start_blueline_X2 + bar_hill / 2, clear_cover + bar_hill, z);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);

            #endregion

            start_redline_X1 = GetLengthSum(0, span_no - 1) + bar_hill/2;
            start_redline_X2 = GetLengthSum(0, span_no) - 600 - bar_hill/2;

            #region Red Line

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            if ((z + end_span_reinforcement / 2) > span_bredth) return;
            line.StartPoint = new gPoint(start_redline_X1, clear_cover, z + end_span_reinforcement / 2);
            line.EndPoint = new gPoint(start_redline_X2, clear_cover, z + end_span_reinforcement / 2);
            line.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(line);

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(start_redline_X1, clear_cover, z + end_span_reinforcement / 2);
            line.EndPoint = new gPoint(start_redline_X1 - bar_hill / 2, clear_cover + bar_hill, z + end_span_reinforcement / 2);
            line.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(line);

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            if (z > span_bredth) z = span_bredth - clear_cover;
            line.StartPoint = new gPoint(start_redline_X2, clear_cover, z + end_span_reinforcement / 2);
            line.EndPoint = new gPoint(start_redline_X2 + bar_hill / 2, clear_cover + bar_hill, z + end_span_reinforcement / 2);
            line.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(line);

            #endregion
        }
        private void Draw_End(vdDocument doc, double z)
        {
            start_blueline_X1 = TotalLength - lengths[lengths.Count - 1] + 600 - bar_hill/2;
            start_blueline_X2 = TotalLength - clear_cover;
            
            #region Blue Line
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(start_blueline_X1, clear_cover, z);
            line.EndPoint = new gPoint(start_blueline_X2, clear_cover, z);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(start_blueline_X1, clear_cover, z);
            line.EndPoint = new gPoint(start_blueline_X1 - bar_hill / 2, clear_cover + bar_hill, z);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(start_blueline_X2, clear_cover, z);
            line.EndPoint = new gPoint(start_blueline_X2, depth - clear_cover, z);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);

            start_blueline_X1 = start_blueline_X2;
            start_blueline_X2 = TotalLength - wall_thickness - 450;

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(start_blueline_X1, depth - clear_cover, z);
            line.EndPoint = new gPoint(start_blueline_X2, depth - clear_cover, z);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(start_blueline_X2, depth - clear_cover, z);
            line.EndPoint = new gPoint(start_blueline_X2 - bar_hill/2, depth - clear_cover - bar_hill, z);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);

            #endregion

            start_redline_X1 = TotalLength - lengths[lengths.Count - 1] + bar_hill/2;
            start_redline_X2 = TotalLength - clear_cover - 300 - bar_hill/2;

            #region Red Line

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(start_redline_X1, clear_cover, z + end_span_reinforcement / 2);
            line.EndPoint = new gPoint(start_redline_X2, clear_cover, z + end_span_reinforcement / 2);
            line.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(line);

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(start_redline_X1, clear_cover, z + end_span_reinforcement / 2);
            line.EndPoint = new gPoint(start_redline_X1 - bar_hill / 2, clear_cover + bar_hill, z + end_span_reinforcement / 2);
            line.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(line);

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(start_redline_X2, clear_cover, z);
            line.EndPoint = new gPoint(start_redline_X2 + bar_hill / 2, clear_cover + bar_hill, z);
            line.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(line);


            #endregion
        }

        public void Draw_Support(vdDocument doc,int span_no,double z)
        {
            start_blueline_X1 = GetLengthSum(0, span_no - 1) - 900;
            start_blueline_X2 = GetLengthSum(0, span_no - 1) + 900;

            #region Blue Line
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(start_blueline_X1, depth - clear_cover, z);
            line.EndPoint = new gPoint(start_blueline_X2, depth - clear_cover, z);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(start_blueline_X1, depth - clear_cover, z);
            line.EndPoint = new gPoint(start_blueline_X1 - bar_hill/2, depth - clear_cover - bar_hill, z);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(start_blueline_X2, depth - clear_cover, z);
            line.EndPoint = new gPoint(start_blueline_X2 + bar_hill/2, depth - clear_cover - bar_hill, z);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);

            #endregion
        }

        public void Draw_Start_Face(vdDocument doc)
        {
            // Top Line
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(0, depth, 0);
            line.EndPoint = new gPoint(TotalLength, depth, 0);
            line.PenColor = new vdColor(Color.BlueViolet);
            doc.ActiveLayOut.Entities.AddItem(line);


            // Bottom Line
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(0, 0, 0);
            line.EndPoint = new gPoint(TotalLength, 0, 0);
            line.PenColor = new vdColor(Color.BlueViolet);
            doc.ActiveLayOut.Entities.AddItem(line);

            // Side 1
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(0, depth, 0);
            line.EndPoint = new gPoint(0, - depth - 100, 0);
            line.PenColor = new vdColor(Color.BlueViolet);
            doc.ActiveLayOut.Entities.AddItem(line);

            // Wall Thickness
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(0, -depth - 100, 0);
            line.EndPoint = new gPoint(wall_thickness, -depth - 100, 0);
            line.PenColor = new vdColor(Color.BlueViolet);
            doc.ActiveLayOut.Entities.AddItem(line);

            // Wall Thickness
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(wall_thickness, -depth - 100, 0);
            line.EndPoint = new gPoint(wall_thickness, 0, 0);
            line.PenColor = new vdColor(Color.BlueViolet);
            doc.ActiveLayOut.Entities.AddItem(line);

            // Wall Thickness
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(wall_thickness, 0, 0);
            line.EndPoint = new gPoint(lengths[0] - wall_thickness/2, 0, 0);
            line.PenColor = new vdColor(Color.BlueViolet);
            doc.ActiveLayOut.Entities.AddItem(line);

        }
        public void Draw_Face(vdDocument doc)
        {
            vdPolyline pline1 = new vdPolyline();
            pline1.SetUnRegisterDocument(doc);
            pline1.setDocumentDefaults();
            pline1.VertexList.Add(new gPoint(0, 0, 0 - clear_cover));
            pline1.VertexList.Add(new gPoint(TotalLength, 0, 0 - clear_cover));
            pline1.VertexList.Add(new gPoint(TotalLength, 0, span_bredth + clear_cover));
            pline1.VertexList.Add(new gPoint(0, 0, span_bredth + clear_cover));
            pline1.VertexList.Add(pline1.VertexList[0]);
            doc.ActiveLayOut.Entities.AddItem(pline1);



            vdPolyline pline2 = new vdPolyline();
            pline2.SetUnRegisterDocument(doc);
            pline2.setDocumentDefaults();
            pline2.VertexList.Add(new gPoint(0, depth, 0 - clear_cover));
            pline2.VertexList.Add(new gPoint(TotalLength, depth, 0 - clear_cover));
            pline2.VertexList.Add(new gPoint(TotalLength, depth, span_bredth + clear_cover));
            pline2.VertexList.Add(new gPoint(0, depth, span_bredth + clear_cover));
            pline2.VertexList.Add(pline2.VertexList[0]);
            doc.ActiveLayOut.Entities.AddItem(pline2);

            for (int i = 0; i < pline1.VertexList.Count; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = pline1.VertexList[i];
                line.EndPoint = pline2.VertexList[i];
                doc.ActiveLayOut.Entities.AddItem(line);
            }

           

            start_blueline_X1 = 0;
            start_blueline_X2 = wall_thickness;

            pline1 = new vdPolyline();
            pline1.SetUnRegisterDocument(doc);
            pline1.setDocumentDefaults();
            pline1.VertexList.Add(new gPoint(start_blueline_X1, 0, 0));
            pline1.VertexList.Add(new gPoint(start_blueline_X2, 0, 0));
            pline1.VertexList.Add(new gPoint(start_blueline_X2, -pillar_length, 0));
            pline1.VertexList.Add(new gPoint(start_blueline_X1, -pillar_length, 0));
            pline1.VertexList.Add(pline1.VertexList[0]);
            doc.ActiveLayOut.Entities.AddItem(pline1);



            start_blueline_X1 = 0;
            start_blueline_X2 = wall_thickness;

            pline2 = new vdPolyline();
            pline2.SetUnRegisterDocument(doc);
            pline2.setDocumentDefaults();
            pline2.VertexList.Add(new gPoint(start_blueline_X1, 0, span_bredth));
            pline2.VertexList.Add(new gPoint(start_blueline_X2, 0, span_bredth));
            pline2.VertexList.Add(new gPoint(start_blueline_X2, -pillar_length, span_bredth));
            pline2.VertexList.Add(new gPoint(start_blueline_X1, -pillar_length, span_bredth));
            pline2.VertexList.Add(pline2.VertexList[0]);
            doc.ActiveLayOut.Entities.AddItem(pline2);

            for (int i = 0; i < pline1.VertexList.Count; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = pline1.VertexList[i];
                line.EndPoint = pline2.VertexList[i];
                doc.ActiveLayOut.Entities.AddItem(line);
            }


            start_blueline_X1 = TotalLength - wall_thickness;
            start_blueline_X2 = TotalLength;
            pline1 = new vdPolyline();
            pline1.SetUnRegisterDocument(doc);
            pline1.setDocumentDefaults();
            pline1.VertexList.Add(new gPoint(start_blueline_X1, 0, 0));
            pline1.VertexList.Add(new gPoint(start_blueline_X2, 0, 0));
            pline1.VertexList.Add(new gPoint(start_blueline_X2, -pillar_length, 0));
            pline1.VertexList.Add(new gPoint(start_blueline_X1, -pillar_length, 0));
            pline1.VertexList.Add(pline1.VertexList[0]);
            doc.ActiveLayOut.Entities.AddItem(pline1);



            start_blueline_X1 = TotalLength - wall_thickness;
            start_blueline_X2 = TotalLength;
            pline2 = new vdPolyline();
            pline2.SetUnRegisterDocument(doc);
            pline2.setDocumentDefaults();
            pline2.VertexList.Add(new gPoint(start_blueline_X1, 0, span_bredth));
            pline2.VertexList.Add(new gPoint(start_blueline_X2, 0, span_bredth));
            pline2.VertexList.Add(new gPoint(start_blueline_X2, -pillar_length, span_bredth));
            pline2.VertexList.Add(new gPoint(start_blueline_X1, -pillar_length, span_bredth));
            pline2.VertexList.Add(pline2.VertexList[0]);
            doc.ActiveLayOut.Entities.AddItem(pline2);

            for (int i = 0; i < pline1.VertexList.Count; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = pline1.VertexList[i];
                line.EndPoint = pline2.VertexList[i];
                doc.ActiveLayOut.Entities.AddItem(line);
            }
        }
        public void Draw_Mid_Face(vdDocument doc, int span_no)
        {
            vdPolyline pline1 = new vdPolyline();
            pline1.SetUnRegisterDocument(doc);
            pline1.setDocumentDefaults();

            start_blueline_X1 = GetLengthSum(0, span_no) - wall_thickness / 2;
            start_blueline_X2 = GetLengthSum(0, span_no) + wall_thickness / 2;
            pline1.VertexList.Add(new gPoint(start_blueline_X1, 0, 0));
            pline1.VertexList.Add(new gPoint(start_blueline_X2, 0, 0));
            pline1.VertexList.Add(new gPoint(start_blueline_X2, -pillar_length, 0));
            pline1.VertexList.Add(new gPoint(start_blueline_X1, -pillar_length, 0));
            pline1.VertexList.Add(pline1.VertexList[0]);
            doc.ActiveLayOut.Entities.AddItem(pline1);

            vdPolyline pline2 = new vdPolyline();
            pline2.SetUnRegisterDocument(doc);
            pline2.setDocumentDefaults();

            start_blueline_X1 = GetLengthSum(0, span_no) - wall_thickness / 2;
            start_blueline_X2 = GetLengthSum(0, span_no) + wall_thickness / 2;
            pline2.VertexList.Add(new gPoint(start_blueline_X1, 0, span_bredth));
            pline2.VertexList.Add(new gPoint(start_blueline_X2, 0, span_bredth));
            pline2.VertexList.Add(new gPoint(start_blueline_X2, -pillar_length, span_bredth));
            pline2.VertexList.Add(new gPoint(start_blueline_X1, -pillar_length, span_bredth));
            pline2.VertexList.Add(pline2.VertexList[0]);
            doc.ActiveLayOut.Entities.AddItem(pline2);


            for (int i = 0; i < pline1.VertexList.Count; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = pline1.VertexList[i];
                line.EndPoint = pline2.VertexList[i];
                doc.ActiveLayOut.Entities.AddItem(line);
            }
        }
        public void Draw_Circle(vdDocument doc)
        {
            for (int i = 0; i < doc.ActiveLayOut.Entities.Count; i++)
            {
                line = doc.ActiveLayOut.Entities[i] as vdLine;
                if (line != null)
                {
                    cir = new vdCircle();
                    cir.SetUnRegisterDocument(doc);
                    cir.setDocumentDefaults();
                    cir.Center = line.StartPoint;
                    cir.Thickness = line.Length();
                    cir.ExtrusionVector = Vector.CreateExtrusion(line.StartPoint, line.EndPoint);
                    if (line.StartPoint.x != line.EndPoint.x)
                    {
                        //cir.PenColor = new vdColor(Color.Red);
                        cir.Radius = main_dia/2;
                    }
                    else
                    {
                        //cir.PenColor = new vdColor(Color.Green);
                        cir.Radius = dist_dia/2;
                    }
                    cir.PenColor = line.PenColor;
                    doc.ActiveLayOut.Entities.AddItem(cir);
                }
            }
        }
        public double TotalLength
        {
            get
            {
                double d = 0.0;
                for (int i = 0; i < lengths.Count; i++)
                {
                    d += lengths[i];
                }
                return d;
            }
        }
        public double GetLengthSum(int fromIndex, int toIndex)
        {
            double sum = 0.0;
            for (int i = fromIndex; i <= toIndex; i++)
            {
                sum += lengths[i];
            }
            return sum;
        }


        #region Drawing

        public void Draw_Drawing(vdDocument doc)
        {
            doc.ShowUCSAxis = false;
            Draw_Rect(doc);
            Draw_Circles(doc);
            Draw_Text(doc);
            Draw_Green_Lines(doc);
            Draw_Blue_Lines(doc);
            Draw_Red_Lines(doc);
            Draw_Black_Lines(doc);
            Draw_Green_Text(doc);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);
        }
        public void Draw_Green_Lines(vdDocument doc)
        {
            vdLine line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-0.7733, 7.9646, 0.0000);
            line.EndPoint = new gPoint(-4.0345, 7.9646, 0.0000);
            line.PenColor = new vdColor(Color.Green);
            doc.ActiveLayOut.Entities.AddItem(line);

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(4.0073, 3.8539, 0.0000);
            line.EndPoint = new gPoint(-4.0345, 3.8539, 0.0000);
            line.PenColor = new vdColor(Color.Green);
            doc.ActiveLayOut.Entities.AddItem(line);

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-0.9587, 8.3170, 0.0000);
            line.EndPoint = new gPoint(-0.9587, 2.7761, 0.0000);
            line.PenColor = new vdColor(Color.Green);
            doc.ActiveLayOut.Entities.AddItem(line);

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-0.5804, 8.3170, 0.0000);
            line.EndPoint = new gPoint(-0.5804, 2.7761, 0.0000);
            line.PenColor = new vdColor(Color.Green);
            doc.ActiveLayOut.Entities.AddItem(line);

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(3.8701, 8.2725, 0.0000);
            line.EndPoint = new gPoint(3.8701, 2.7539, 0.0000);
            line.PenColor = new vdColor(Color.Green);
            doc.ActiveLayOut.Entities.AddItem(line);

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(3.6114, 8.0194, 0.0000);
            line.EndPoint = new gPoint(13.6963, 8.0194, 0.0000);
            line.PenColor = new vdColor(Color.Green);
            doc.ActiveLayOut.Entities.AddItem(line);

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(5.3105, 8.2935, 0.0000);
            line.EndPoint = new gPoint(5.3105, 2.7851, 0.0000);
            line.PenColor = new vdColor(Color.Green);
            doc.ActiveLayOut.Entities.AddItem(line);

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(5.0296, 4.4764, 0.0000);
            line.EndPoint = new gPoint(13.7609, 4.4973, 0.0000);
            line.PenColor = new vdColor(Color.Green);
            doc.ActiveLayOut.Entities.AddItem(line);

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(6.6613, 8.2286, 0.0000);
            line.EndPoint = new gPoint(6.6613, 2.7901, 0.0000);
            line.PenColor = new vdColor(Color.Green);
            doc.ActiveLayOut.Entities.AddItem(line);

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(9.4242, 8.2039, 0.0000);
            line.EndPoint = new gPoint(9.4242, 2.5213, 0.0000);
            line.PenColor = new vdColor(Color.Green);
            doc.ActiveLayOut.Entities.AddItem(line);

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(10.6205, 8.2057, 0.0000);
            line.EndPoint = new gPoint(10.6205, 2.5480, 0.0000);
            line.PenColor = new vdColor(Color.Green);
            doc.ActiveLayOut.Entities.AddItem(line);


            //line = new vdLine();
            //line.SetUnRegisterDocument(doc);
            //line.setDocumentDefaults();
            //line.StartPoint = new gPoint();
            //line.EndPoint = new gPoint();
            //line.PenColor = new vdColor(Color.Green);
            //doc.ActiveLayOut.Entities.AddItem(line);
        }
        public void Draw_Blue_Lines(vdDocument doc)
        {
            //line 1
            vdLine line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-1.1815, 2.1666, 0.0000);
            line.EndPoint = new gPoint(-1.1815, 10.5527, 0.0000);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);

            //line 2
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-1.1841, 10.5527, 0.0000);
            line.EndPoint = new gPoint(0.0274, 10.5527, 0.0000);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);


            //line 3
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-0.7944, 6.9929, 0.0000);
            line.EndPoint = new gPoint(-1.5591, 6.9929, 0.0000);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);


            //line 4
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(3.6388, 5.6078, 0.0000);
            line.EndPoint = new gPoint(-1.5955, 5.6078, 0.0000);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);


            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(2.7345, 8.7319, 0.0000);
            line.EndPoint = new gPoint(2.7345, 0.1883, 0.0000);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);

            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(3.0936, 7.5618, 0.0000);
            line.EndPoint = new gPoint(5.8103, 7.5618, 0.0000);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);
            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(3.0936, 3.0550, 0.0000);
            line.EndPoint = new gPoint(5.8103, 3.0550, 0.0000);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);

            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(2.7345, 0.1883, 0.0000);
            line.EndPoint = new gPoint(4.2476, 0.1883, 0.0000);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);


            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(3.3922, 2.2096, 0.0000);
            line.EndPoint = new gPoint(3.3922, 10.5406, 0.0000);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);


            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(3.3922, 10.5406, 0.0000);
            line.EndPoint = new gPoint(5.5846, 10.5406, 0.0000);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);
            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(4.4849, 10.0411, 0.0000);
            line.EndPoint = new gPoint(4.4848, 0.9293, 0.0000);
            line.LineType = doc.LineTypes.DPIDash;
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);


            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(6.1981, 6.5978, 0.0000);
            line.EndPoint = new gPoint(10.0613, 6.5978, 0.0000);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);
            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(8.7648, 8.7525, 0.0000);
            line.EndPoint = new gPoint(8.7648, 0.1883, 0.0000);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);


            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(8.7648, 0.1883, 0.0000);
            line.EndPoint = new gPoint(9.9113, 0.1883, 0.0000);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);
            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(9.1002, 2.1724, 0.0000);
            line.EndPoint = new gPoint(9.1002, 10.4969, 0.0000);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);


            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(9.1002, 10.4969, 0.0000);
            line.EndPoint = new gPoint(10.8199, 10.4969, 0.0000);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);
            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(8.6560, 7.5618, 0.0000);
            line.EndPoint = new gPoint(11.3727, 7.5618, 0.0000);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);


            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(10.8199, 8.7540, 0.0000);
            line.EndPoint = new gPoint(10.8199, 0.1883, 0.0000);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);



            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(10.8199, 0.1883, 0.0000);
            line.EndPoint = new gPoint(12.4399, 0.1883, 0.0000);
            line.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(line);
        }
        public void Draw_Red_Lines(vdDocument doc)
        {
            //line 1
            vdLine line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-0.9587, 3.2371, 0.0000);
            line.EndPoint = new gPoint(4.5158, 3.2371, 0.0000);
            line.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(line);

            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(0.6791, 8.7319, 0.0000);
            line.EndPoint = new gPoint(0.6791, 1.1438, 0.0000);
            line.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(line);

            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(0.6791, 1.1438, 0.0000);
            line.EndPoint = new gPoint(2.3703, 1.1438, 0.0000);
            line.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(line);

            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(4.5158, 4.1108, 0.0000);
            line.EndPoint = new gPoint(8.8650, 4.1108, 0.0000);
            line.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(line);

            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(6.9952, 8.7775, 0.0000);
            line.EndPoint = new gPoint(6.9952, 1.1509, 0.0000);
            line.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(line);
            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(6.9952, 1.1509, 0.0000);
            line.EndPoint = new gPoint(8.5405, 1.1509, 0.0000);
            line.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(line);

            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(11.0692, 8.7540, 0.0000);
            line.EndPoint = new gPoint(11.0692, 1.1772, 0.0000);
            line.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(line);

            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(11.0692, 1.1772, 0.0000);
            line.EndPoint = new gPoint(12.5895, 1.1772, 0.0000);
            line.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(line);

            //line 
            //line = new vdLine();
            //line.SetUnRegisterDocument(doc);
            //line.setDocumentDefaults();
            //line.StartPoint = new gPoint();
            //line.EndPoint = new gPoint();
            //line.PenColor = new vdColor(Color.Red);
            //doc.ActiveLayOut.Entities.AddItem(line);
           

        }
        public void Draw_Black_Lines(vdDocument doc)
        {
            //line 1
            vdLine line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-1.7857, 8.9484, 0.0000);
            line.EndPoint = new gPoint(-1.7857, 1.9444, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);

            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(11.8254, 8.9484, 0.0000);
            line.EndPoint = new gPoint(-1.7857, 8.9484, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);

            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-1.7857, 1.9444, 0.0000);
            line.EndPoint = new gPoint(11.8254, 1.9444, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);

            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(8.6560, 7.5618, 0.0000);
            line.EndPoint = new gPoint(11.3727, 7.5618, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);

            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(8.5748, 3.0550, 0.0000);
            line.EndPoint = new gPoint(11.2915, 3.0550, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);

            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(11.8253, 5.7231, 0.0000);
            line.EndPoint = new gPoint(11.8254, 8.9484, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);

            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(11.8253, 5.7231, 0.0000);
            line.EndPoint = new gPoint(11.9602, 5.6814, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);

            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(11.9602, 5.6814, 0.0000);
            line.EndPoint = new gPoint(11.6637, 5.5196, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);

            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(11.6637, 5.5196, 0.0000);
            line.EndPoint = new gPoint(11.8254, 5.4713, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);

            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(11.5284, 5.3094, 0.0000);
            line.EndPoint = new gPoint(12.1830, 5.3094, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);

            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(11.8254, 1.9444, 0.0000);
            line.EndPoint = new gPoint(11.8254, 5.4713, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(11.5308, 5.9586, 0.0000);
            line.EndPoint = new gPoint(12.1717, 5.9586, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(10.3849, 8.5603, 0.0000);
            line.EndPoint = new gPoint(10.3849, 2.3889, 0.0000);
            line.LineType = doc.LineTypes.DPIDash;
            doc.ActiveLayOut.Entities.AddItem(line);

            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(10.3849, 2.3889, 0.0000);
            line.EndPoint = new gPoint(11.3563, 2.3889, 0.0000);
            line.LineType = doc.LineTypes.DPIDash;
            doc.ActiveLayOut.Entities.AddItem(line);


            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(10.3849, 8.5603, 0.0000);
            line.EndPoint = new gPoint(11.3849, 8.5603, 0.0000);
            line.LineType = doc.LineTypes.DPIDash;
            doc.ActiveLayOut.Entities.AddItem(line);

        }
        public void Draw_Lines(vdDocument doc)
        {
            //line 1
            vdLine line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-0.7733, 7.9646, 0.0000);
            line.EndPoint = new gPoint(-4.0345, 7.9646, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);

            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint();
            line.EndPoint = new gPoint();
            doc.ActiveLayOut.Entities.AddItem(line);

            //line 
            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint();
            line.EndPoint = new gPoint();
            doc.ActiveLayOut.Entities.AddItem(line);

        }
        public void Draw_Circles(vdDocument doc)
        {
            // cir 1
            vdCircle cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(-0.9545, 7.9608, 0.0000);
            cir.Radius = 0.1000;
            doc.ActiveLayOut.Entities.AddItem(cir);

            // cir 2
            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(-1.1823, 6.9928, 0.0000);
            cir.Radius = 0.1000;
            doc.ActiveLayOut.Entities.AddItem(cir);

            // cir 3
            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(-0.5898, 3.8528, 0.0000);
            cir.Radius = 0.1000;
            doc.ActiveLayOut.Entities.AddItem(cir);

            // cir 4
            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(0.6723, 3.2420, 0.0000);
            cir.Radius = 0.1000;
            doc.ActiveLayOut.Entities.AddItem(cir);

            // cir 5
            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(2.7383, 5.6040, 0.0000);
            cir.Radius = 0.1000;
            doc.ActiveLayOut.Entities.AddItem(cir);

            // cir 6
            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(3.3908, 7.5608, 0.0000);
            cir.Radius = 0.1000;
            doc.ActiveLayOut.Entities.AddItem(cir);

            // cir 7
            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(3.3912, 3.0511, 0.0000);
            cir.Radius = 0.1000;
            doc.ActiveLayOut.Entities.AddItem(cir);

             // cir 8
            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(3.8685, 8.0225, 0.0000);
            cir.Radius = 0.1000;
            doc.ActiveLayOut.Entities.AddItem(cir);


            // cir 9
            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(5.3070, 8.0138, 0.0000);
            cir.Radius = 0.1000;
            doc.ActiveLayOut.Entities.AddItem(cir);

            // cir 10
            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(6.6543, 4.4905, 0.0000);
            cir.Radius = 0.1000;
            doc.ActiveLayOut.Entities.AddItem(cir);

            // cir 11
            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(6.9921, 4.1160, 0.0000);
            cir.Radius = 0.1000;
            doc.ActiveLayOut.Entities.AddItem(cir);

            // cir 12
            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(8.7602, 6.5875, 0.0000);
            cir.Radius = 0.1000;
            doc.ActiveLayOut.Entities.AddItem(cir);

            // cir 13
            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(9.0981, 7.5635, 0.0000);
            cir.Radius = 0.1000;
            doc.ActiveLayOut.Entities.AddItem(cir);

            // cir 14
            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(9.0970, 3.0801, 0.0000);
            cir.Radius = 0.1000;
            doc.ActiveLayOut.Entities.AddItem(cir);

            // cir 15
            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(9.4221, 8.0196, 0.0000);
            cir.Radius = 0.1000;
            doc.ActiveLayOut.Entities.AddItem(cir);

            // cir 16
            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(10.6230, 8.0245, 0.0000);
            cir.Radius = 0.1000;
            doc.ActiveLayOut.Entities.AddItem(cir);

            // cir 17
            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(10.8173, 6.6101, 0.0000);
            cir.Radius = 0.1000;
            doc.ActiveLayOut.Entities.AddItem(cir);
        }
        public void Draw_Rect(vdDocument doc)
        {
            //rect 01
            vdRect rect = new vdRect();
            rect.SetUnRegisterDocument(doc);
            rect.setDocumentDefaults();
            rect.InsertionPoint = new gPoint(-4.3705, 11.6031, 0.0000);
            rect.Height = -12.5732;
            rect.Width = 18.7781;
            doc.ActiveLayOut.Entities.AddItem(rect);

            //rect 02
            rect = new vdRect();
            rect.SetUnRegisterDocument(doc);
            rect.setDocumentDefaults();
            rect.InsertionPoint = new gPoint(-1.3690,8.5516,0.0000);
            rect.Height = -6.1508;
            rect.Width = 5.5754;
            rect.LineType = doc.LineTypes.DPIDash;
            doc.ActiveLayOut.Entities.AddItem(rect);

            //rect 03
            rect = new vdRect();
            rect.SetUnRegisterDocument(doc);
            rect.setDocumentDefaults();
            rect.InsertionPoint = new gPoint(4.7222,8.5516,0.0000);
            rect.Height = -6.1706;
            rect.Width = 5.0595;
            rect.LineType = doc.LineTypes.DPIDash;
            doc.ActiveLayOut.Entities.AddItem(rect);

        }


        List<string> lstText = new List<string>();

        public void Draw_Text(vdDocument doc)
        {
            SetList();
            //Text 1
            vdText txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(-2.1861, 11.9145, 0.0000);
            txt.TextString = "ONE WAY CONTINUOUS RCC SLAB DRAWING";
            txt.Height = 0.5000;
            txt.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(txt);

            ////Text 
            //txt = new vdText();
            //txt.SetUnRegisterDocument(doc);
            //txt.setDocumentDefaults();
            //txt.InsertionPoint = new gPoint();
            //txt.TextString = "";
            //txt.Height = 0.252;
            //txt.PenColor = new vdColor(Color.Blue);
            //doc.ActiveLayOut.Entities.AddItem(txt);
            Draw_Red_Text(doc);
            Draw_Green_Text(doc);
            Draw_Blue_Text(doc);

        }
        private void Draw_Red_Text(vdDocument doc)
        {

            //List<string> lstStr = new List<string>();
            //lstStr.Add("T8-340 c/c - B2");

            int indx = 0;

            //Text 1
            vdText txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(-0.1067, 0.6872, 0.0000);
            txt.TextString = lstText[4];
            txt.Height = 0.2133;
            txt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(txt);

            //Text 
            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(6.2853, 0.8357, 0.0000);
            txt.TextString = lstText[4];
            txt.Height = 0.2133;
            txt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(txt);


            //Text 
            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(11.0355, 0.8208, 0.0000);
            txt.TextString = lstText[4];
            txt.Height = 0.2133;
            txt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(txt);


        }
        private void Draw_Blue_Text(vdDocument doc)
        {

            //List<string> lstStr = new List<string>();
            //lstStr.Add("T8-340 c/c -T1");
            //lstStr.Add("T8-140 c/c -T1");
            //lstStr.Add("T8-340 c/c -B1");

            int indx = 0;


            //Text 1
            vdText txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(-1.1755, 10.6804, 0.0000);
            txt.TextString = lstText[1];
            txt.PenColor = new vdColor(Color.Blue);
            txt.Height = 0.2133;
            txt.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(txt);

            //Text 
            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(3.2867, 10.7072, 0.0000);
            txt.TextString = lstText[2];
            txt.PenColor = new vdColor(Color.Blue);
            txt.Height = 0.2133;
            txt.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(txt);

            //Text 
            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(8.9781, 10.7072, 0.0000);
            txt.TextString = lstText[2];
            txt.PenColor = new vdColor(Color.Blue);
            txt.Height = 0.2133;
            txt.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(txt);






            //Text 
            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(2.7523, -0.2479, 0.0000);
            txt.TextString = lstText[3];
            txt.PenColor = new vdColor(Color.Blue);
            txt.Height = 0.2133;
            txt.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(txt);

            //Text 
            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(8.4866, -0.1252, 0.0000);
            txt.TextString = lstText[3];
            txt.PenColor = new vdColor(Color.Blue);
            txt.Height = 0.2133;
            txt.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(txt);

            //Text 
            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(10.7683, -0.2212, 0.0000);
            txt.TextString = lstText[3];
            txt.PenColor = new vdColor(Color.Blue);
            txt.Height = 0.2133;
            txt.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(txt);
        }
        private void Draw_Green_Text(vdDocument doc)
        {

            //Text 1
            vdText txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(-3.9810, 7.6344, 0.0000);
            txt.TextString = "DISTRIBUTION";
            txt.Height = 0.2133;
            txt.PenColor = new vdColor(Color.Green);
            doc.ActiveLayOut.Entities.AddItem(txt);

            //Text 
            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(-3.8742, 7.2870, 0.0000);
            txt.TextString = lstText[0];
            //txt.TextString = "T6";
            txt.Height = 0.2133;
            txt.PenColor = new vdColor(Color.Green );
            doc.ActiveLayOut.Entities.AddItem(txt);

            //Text 
            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(-2.3779, 7.2870, 0.0000);
            txt.TextString = "T2";
            txt.Height = 0.2133;
            txt.PenColor = new vdColor(Color.Green);
            doc.ActiveLayOut.Entities.AddItem(txt);

            //Text 
            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(-4.0612, 3.4928, 0.0000);
            txt.TextString = "DISTRIBUTION";
            txt.Height = 0.2133;
            txt.PenColor = new vdColor(Color.Green);
            doc.ActiveLayOut.Entities.AddItem(txt);

            //Text 
            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(-3.9810, 3.1455, 0.0000);
            txt.TextString = lstText[0];
            //txt.TextString = "T6";
            txt.Height = 0.2133;
            txt.PenColor = new vdColor(Color.Green);
            doc.ActiveLayOut.Entities.AddItem(txt);

            //Text 
            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(-2.4847, 3.1455, 0.0000);
            txt.TextString = "B3";
            txt.Height = 0.2133;
            txt.PenColor = new vdColor(Color.Green);
            doc.ActiveLayOut.Entities.AddItem(txt);

            //Text 
            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(11.8905, 7.6878, 0.0000);
            txt.TextString = "DISTRIBUTION";
            txt.Height = 0.2133;
            txt.PenColor = new vdColor(Color.Green);
            doc.ActiveLayOut.Entities.AddItem(txt);

            //Text 
            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(11.9258, 7.3358, 0.0000);
            txt.TextString = lstText[0];
            //txt.TextString = "T6";
            txt.Height = 0.2133;
            txt.PenColor = new vdColor(Color.Green);
            doc.ActiveLayOut.Entities.AddItem(txt);

            //Text 
            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(13.4221, 7.3358, 0.0000);
            txt.TextString = "T2";
            txt.Height = 0.2133;
            txt.PenColor = new vdColor(Color.Green);
            doc.ActiveLayOut.Entities.AddItem(txt);

            //Text 
            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(11.9707, 4.1074, 0.0000);
            txt.TextString = "DISTRIBUTION";
            txt.Height = 0.2133;
            txt.PenColor = new vdColor(Color.Green);
            doc.ActiveLayOut.Entities.AddItem(txt);

            //Text 
            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(12.0861, 3.7287, 0.0000);
            txt.TextString = lstText[0];
            //txt.TextString = "T6";
            txt.Height = 0.2133;
            txt.PenColor = new vdColor(Color.Green);
            doc.ActiveLayOut.Entities.AddItem(txt);

            //Text 
            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(13.5824, 3.7287, 0.0000);
            txt.TextString = "B3";
            txt.Height = 0.2133;
            txt.PenColor = new vdColor(Color.Green);
            doc.ActiveLayOut.Entities.AddItem(txt);

         
        }
        private void SetList()
        {
            //string fileName = @"C:\Documents and Settings\TECHSOFT\Desktop\Slab_Boq\DESIGN_SLAB03\DESIGN.FIL";
            ReadFromFile(filename);
            
            //SLAB DESIGN 03

            //lengths = 3, 3, 3
            //span_bredth = 1
            //main_dia = 8
            //dist_dia = 6
            //end_span_reinforcement = 340
            //support_reinforcement = 140
            //interior_reinforcement = 398
            //start_center_distance = 300
            //start_wall_distance = 450
            //clear_cover = 15
            //upper_distance = 900
            //lower_distance = 600
            //depth = 120
            //wall_thickness = 250
            //bar_hill = 25.4
            //pillar_length = 500

            //FINISH

            
            //List<string> lstContent = new List<string>(File.ReadAllText(filename));
            string kStr = "";

            kStr = "T" + dist_dia; // T6
            lstText.Add(kStr);


            kStr = "T" + main_dia + " - " + end_span_reinforcement + " c/c - T1";
            lstText.Add(kStr);

            kStr = "T" + main_dia + " - " + support_reinforcement + " c/c - T1";
            lstText.Add(kStr);

            kStr = "T" + main_dia + " - " + end_span_reinforcement + " c/c - B1";
            lstText.Add(kStr);

            kStr = "T" + main_dia + " - " + end_span_reinforcement+ " c/c - B2";
            lstText.Add(kStr);

        }

        #endregion
        class Drawing
        {
            vdLine ln;
            vdPolyline pline;

            public Drawing()
            {
                ln = new vdLine();
                pline = new vdPolyline();
               
            }
            public void Draw_Drawing_1(vdDocument doc)
            {
                #region OUTER FACE

                ln = new vdLine();
                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = new gPoint(0, 0, 0);
                ln.EndPoint = new gPoint(0, 100, 0);
                doc.ActiveLayOut.Entities.AddItem(ln);


                ln = new vdLine();
                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = new gPoint(0, 100, 0);
                ln.EndPoint = new gPoint(300, 100, 0);
                doc.ActiveLayOut.Entities.AddItem(ln);

                ln = new vdLine();
                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = new gPoint(0, 0, 0);
                ln.EndPoint = new gPoint(300, 0, 0);
                doc.ActiveLayOut.Entities.AddItem(ln);

                #endregion

                pline = new vdPolyline();
                pline.SetUnRegisterDocument(doc);
                pline.setDocumentDefaults();
                pline.VertexList.Add(new gPoint(10, 10, 0));
                pline.VertexList.Add(new gPoint(90, 10, 0));
                pline.VertexList.Add(new gPoint(90, 90, 0));
                pline.VertexList.Add(new gPoint(10, 90, 0));
                pline.VertexList.Add(pline.VertexList[0]);
                pline.LineType = doc.LineTypes.DPIDash;
                doc.ActiveLayOut.Entities.AddItem(pline);


                pline = new vdPolyline();
                pline.SetUnRegisterDocument(doc);
                pline.setDocumentDefaults();
                pline.VertexList.Add(new gPoint(110, 10, 0));
                pline.VertexList.Add(new gPoint(190, 10, 0));
                pline.VertexList.Add(new gPoint(190, 90, 0));
                pline.VertexList.Add(new gPoint(110, 90, 0));
                pline.VertexList.Add(pline.VertexList[0]);
                pline.LineType = doc.LineTypes.DPIDash;
                doc.ActiveLayOut.Entities.AddItem(pline);

                pline = new vdPolyline();
                pline.SetUnRegisterDocument(doc);
                pline.setDocumentDefaults();
                pline.VertexList.Add(new gPoint(210, 10, 0));
                pline.VertexList.Add(new gPoint(290, 10, 0));
                pline.VertexList.Add(new gPoint(290, 90, 0));
                pline.VertexList.Add(new gPoint(210, 90, 0));
                pline.VertexList.Add(pline.VertexList[0]);
                pline.LineType = doc.LineTypes.DPIDash;
                doc.ActiveLayOut.Entities.AddItem(pline);



                ln = new vdLine();
                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = new gPoint();
                ln.EndPoint = new gPoint();
                doc.ActiveLayOut.Entities.AddItem(ln);

                ln = new vdLine();
                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = new gPoint();
                ln.EndPoint = new gPoint();
                doc.ActiveLayOut.Entities.AddItem(ln);

                ln = new vdLine();
                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = new gPoint();
                ln.EndPoint = new gPoint();
                doc.ActiveLayOut.Entities.AddItem(ln);

                ln = new vdLine();
                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = new gPoint();
                ln.EndPoint = new gPoint();
                doc.ActiveLayOut.Entities.AddItem(ln);


                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);
                doc.Redraw(true);
            }
            //public void Draw_Drawing(vdDocument doc)
            //{
            //    Draw_Rect(doc);
            //    Draw_Circles(doc);
            //}
            //public void Draw_Lines(vdDocument doc)
            //{
            //    vdLine line = new vdLine();
            //    line.SetUnRegisterDocument(doc);
            //    line.setDocumentDefaults();
            //    line.StartPoint = new gPoint();
            //    line.EndPoint = new gPoint();

            //    doc.ActiveLayOut.Entities.AddItem(line);

            //    line = new vdLine();
            //    line.SetUnRegisterDocument(doc);
            //    line.setDocumentDefaults();
            //    line.StartPoint = new gPoint();
            //    line.EndPoint = new gPoint();

            //    doc.ActiveLayOut.Entities.AddItem(line);

            //    line = new vdLine();
            //    line.SetUnRegisterDocument(doc);
            //    line.setDocumentDefaults();
            //    line.StartPoint = new gPoint();
            //    line.EndPoint = new gPoint();

            //    doc.ActiveLayOut.Entities.AddItem(line);

            //}
            //public void Draw_Circles(vdDocument doc)
            //{
            //    // cir 1
            //    vdCircle cir = new vdCircle();
            //    cir.SetUnRegisterDocument(doc);
            //    cir.setDocumentDefaults();
            //    cir.Center = new gPoint(-0.9545,7.9608,0.0000);
            //    cir.Radius = 0.1000;
            //    doc.ActiveLayOut.Entities.AddItem(cir);

            //    // cir 2
            //    vdCircle cir = new vdCircle();
            //    cir.SetUnRegisterDocument(doc);
            //    cir.setDocumentDefaults();
            //    cir.Center = new gPoint(-1.1823,6.9928,0.0000);
            //    cir.Radius = 0.1000;
            //    doc.ActiveLayOut.Entities.AddItem(cir);

            //    // cir 3
            //    vdCircle cir = new vdCircle();
            //    cir.SetUnRegisterDocument(doc);
            //    cir.setDocumentDefaults();
            //    cir.Center = new gPoint(-0.5898,3.8528,0.0000);
            //    cir.Radius = 0.1000;
            //    doc.ActiveLayOut.Entities.AddItem(cir);

            //    // cir 4
            //    vdCircle cir = new vdCircle();
            //    cir.SetUnRegisterDocument(doc);
            //    cir.setDocumentDefaults();
            //    cir.Center = new gPoint(0.6723,3.2420,0.0000);
            //    cir.Radius = 0.1000;
            //    doc.ActiveLayOut.Entities.AddItem(cir);

            //    // cir 5
            //    vdCircle cir = new vdCircle();
            //    cir.SetUnRegisterDocument(doc);
            //    cir.setDocumentDefaults();
            //    cir.Center = new gPoint(2.7383,5.6040,0.0000);
            //    cir.Radius = 0.1000;
            //    doc.ActiveLayOut.Entities.AddItem(cir);

            //    // cir 6
            //    vdCircle cir = new vdCircle();
            //    cir.SetUnRegisterDocument(doc);
            //    cir.setDocumentDefaults();
            //    cir.Center = new gPoint(3.3908,7.5608,0.0000);
            //    cir.Radius = 0.1000;
            //    doc.ActiveLayOut.Entities.AddItem(cir);

            //    // cir 7
            //    vdCircle cir = new vdCircle();
            //    cir.SetUnRegisterDocument(doc);
            //    cir.setDocumentDefaults();
            //    cir.Center = new gPoint(3.3912, 3.0511, 0.0000);
            //    cir.Radius = 0.1000;
            //    doc.ActiveLayOut.Entities.AddItem(cir);

            //    // cir 8
            //    vdCircle cir = new vdCircle();
            //    cir.SetUnRegisterDocument(doc);
            //    cir.setDocumentDefaults();
            //    cir.Center = new gPoint(5.3070, 8.0138, 0.0000);
            //    cir.Radius = 0.1000;
            //    doc.ActiveLayOut.Entities.AddItem(cir);

            //    // cir 9
            //    vdCircle cir = new vdCircle();
            //    cir.SetUnRegisterDocument(doc);
            //    cir.setDocumentDefaults();
            //    cir.Center = new gPoint(6.6543, 4.4905, 0.0000);
            //    cir.Radius = 0.1000;
            //    doc.ActiveLayOut.Entities.AddItem(cir);

            //    // cir 10
            //    vdCircle cir = new vdCircle();
            //    cir.SetUnRegisterDocument(doc);
            //    cir.setDocumentDefaults();
            //    cir.Center = new gPoint(6.9921, 4.1160, 0.0000);
            //    cir.Radius = 0.1000;
            //    doc.ActiveLayOut.Entities.AddItem(cir);

            //    // cir 11
            //    vdCircle cir = new vdCircle();
            //    cir.SetUnRegisterDocument(doc);
            //    cir.setDocumentDefaults();
            //    cir.Center = new gPoint(8.7602, 6.5875, 0.0000);
            //    cir.Radius = 0.1000;
            //    doc.ActiveLayOut.Entities.AddItem(cir);

            //    // cir 12
            //    vdCircle cir = new vdCircle();
            //    cir.SetUnRegisterDocument(doc);
            //    cir.setDocumentDefaults();
            //    cir.Center = new gPoint(9.0981, 7.5635, 0.0000);
            //    cir.Radius = 0.1000;
            //    doc.ActiveLayOut.Entities.AddItem(cir);

            //    // cir 13
            //    vdCircle cir = new vdCircle();
            //    cir.SetUnRegisterDocument(doc);
            //    cir.setDocumentDefaults();
            //    cir.Center = new gPoint(9.0970, 3.0801, 0.0000);
            //    cir.Radius = 0.1000;
            //    doc.ActiveLayOut.Entities.AddItem(cir);

            //    // cir 14
            //    vdCircle cir = new vdCircle();
            //    cir.SetUnRegisterDocument(doc);
            //    cir.setDocumentDefaults();
            //    cir.Center = new gPoint(9.4221, 8.0196, 0.0000);
            //    cir.Radius = 0.1000;
            //    doc.ActiveLayOut.Entities.AddItem(cir);

            //    // cir 15
            //    vdCircle cir = new vdCircle();
            //    cir.SetUnRegisterDocument(doc);
            //    cir.setDocumentDefaults();
            //    cir.Center = new gPoint(10.6230, 8.0245, 0.0000);
            //    cir.Radius = 0.1000;
            //    doc.ActiveLayOut.Entities.AddItem(cir);

            //    // cir 16
            //    vdCircle cir = new vdCircle();
            //    cir.SetUnRegisterDocument(doc);
            //    cir.setDocumentDefaults();
            //    cir.Center = new gPoint(10.8173, 6.6101, 0.0000);
            //    cir.Radius = 0.1000;
            //    doc.ActiveLayOut.Entities.AddItem(cir);

            //    // cir 17
            //    vdCircle cir = new vdCircle();
            //    cir.SetUnRegisterDocument(doc);
            //    cir.setDocumentDefaults();
            //    cir.Center = new gPoint(10.8173, 6.6101, 0.0000);
            //    cir.Radius = 0.1000;
            //    doc.ActiveLayOut.Entities.AddItem(cir);

            //}
            //public void Draw_Rect(vdDocument doc)
            //{
            //    vdRect rect = new vdRect();
            //    rect.SetUnRegisterDocument(doc);
            //    rect.setDocumentDefaults();
            //    rect.InsertionPoint = new gPoint(-4.3705,11.6031,0.0000);
            //    rect.Height = -12.5732;
            //    rect.Width = 18.7781;
            //    doc.ActiveLayOut.Entities.AddItem(rect);

                
            
            //}
            
        }
       
    }
}
