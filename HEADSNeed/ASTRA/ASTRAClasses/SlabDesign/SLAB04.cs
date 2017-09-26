using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Collections;
using VectorDraw.Geometry;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;

namespace HEADSNeed.ASTRA.ASTRAClasses.SlabDesign
{
    public class SLAB04
    {
        #region Variable Declaration
        double N1, S1, N2, S2, N3, S3, N4, S4, D1, D2, D3,D4, lx, ly,d;

        double long_span, short_span, clear_cover, height, start_long_dist, start_short_dist;
        double no_long_span_bar, no_short_span_bar, long_space, short_space;
        double short_span_Corner, long_span_Corner;
        double long_corner_space, short_corner_space, middle_bar_dist;

        double _short_span_Corner, _long_span_Corner;
        double _long_corner_space, _short_corner_space;
        PanelMoment_Type PanelMomentType = PanelMoment_Type.TWO_ADJACENT_EDGES_DISCONTINUOUS;

        double dz = 0.0;
        double dx = 0.0;
        int no_bar = 0;
        vdLayer top_lay = new vdLayer();
        vdLayer bottom_lay = new vdLayer();
        vdLayer edge_strip_lay = new vdLayer();
        vdLayer top_corner_lay = new vdLayer();
        vdLayer bottom_corner_lay = new vdLayer();
        vdLine line = new vdLine();


        Hashtable hash_T = null;
        string file_name = "";

        #endregion

        public SLAB04(string userDataFile)
        {
            file_name = userDataFile;
            hash_T = new Hashtable();
            if (!Read_Drawing_Data_From_File(file_name))
            {
                throw new Exception("File not found " + file_name);
            }

            N1 = S1 = N2 = S2 = N3 = S3 = N4 = S4 = 0.0d;
            D1 = D2 = D3 = 0.0d;

            lx = 4.2;
            ly = 5.9;
            d = 110;

            S1 = 140;
            N1 = 5;
            D1 = S1 * N1;

            S2 = 100;
            N2 = 5;
            D2 = S2 * N2;

            S3 = 140 / 2;
            N3 = 12;
            D3 = S3 * N3;

            S4 = 100 / 2;
            N4 = 12;
            D4 = S4 * N4;
        }
        public void Draw_View_Bottom(vdDocument doc)
        {
            Console.WriteLine("ENTER TIME : " + DateTime.Now.TimeOfDay.ToString());

            //Draw_View_Top(doc); return;

            double h = 25;
            double a = 500;
            double b = 3250;
            double c = 700;
            double d = 4250;
            double e = 840;
            double f = 900;
            double g = 2450;

            int i = 0;
            lx = c + d + e + 2 * h;
            ly = h + a + b + a + h;

            #region Poly Line
            vdPolyline pLine = new vdPolyline();
            pLine.SetUnRegisterDocument(doc);
            pLine.setDocumentDefaults();
            pLine.VertexList.Add(new gPoint(0, 0, 0));
            pLine.VertexList.Add(new gPoint(lx, 0, 0));
            pLine.VertexList.Add(new gPoint(lx, ly, 0));
            pLine.VertexList.Add(new gPoint(0, ly, 0));
            pLine.VertexList.Add(new gPoint(0, 0, 0));
            doc.ActiveLayOut.Entities.AddItem(pLine);

            pLine = new vdPolyline();
            pLine.SetUnRegisterDocument(doc);
            pLine.setDocumentDefaults();
            pLine.VertexList.Add(new gPoint(h, h, 0));
            pLine.VertexList.Add(new gPoint(lx - h, h, 0));
            pLine.VertexList.Add(new gPoint(lx - h, ly - h, 0));
            pLine.VertexList.Add(new gPoint(h, ly-h, 0));
            pLine.VertexList.Add(new gPoint(h, h, 0));
            doc.ActiveLayOut.Entities.AddItem(pLine);

            #endregion

            vdLine line = new vdLine();
            //line.SetUnRegisterDocument(doc);
            //line.setDocumentDefaults();
            //line.StartPoint = new gPoint(h,h,0);
            //line.EndPoint = new gPoint(lx - h,h,0);
            //doc.ActiveLayOut.Entities.AddItem(line);

            


            #region Main Reinforcement

            int main_rod_no = (int)(ly - 2 * h) / (140);
            double d_x, d_y;
            d_x = h;
            d_y = h;

            for (i = 0; i <= main_rod_no; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(d_x, d_y, 0);
                line.EndPoint = new gPoint(lx - h, d_y, 0);
                doc.ActiveLayOut.Entities.AddItem(line);

                d_y += 140;
            }
            #endregion

            #region Distribution Reinforcement
            int dist_rod_no = (int)(lx - 2 * h) / 100;
            
            d_x = h;
            d_y = h;
            
            for (i = 0; i <= dist_rod_no; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(d_x, d_y, 0);
                line.EndPoint = new gPoint(d_x,ly - h, 0);
                doc.ActiveLayOut.Entities.AddItem(line);

                d_x += 100;
            }
            #endregion

            #region Main Reinforcement Middle


            //double h = 25;
            //double a = 500;
            //double b = 3250;
            //double c = 700;
            //double d = 4250;
            //double e = 840;
            //double f = 900;
            //double g = 2450;

            //main_rod_no = (int)(ly - 2 * h) / (140);
            main_rod_no = (int)(ly - 2 * h - 2 * a) / (140);
            d_x = h + c;
            //d_y = h + 70;
            d_y = a + h;

            for (i = 0; i <= main_rod_no; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(d_x, d_y, 0);
                line.EndPoint = new gPoint(lx - h - c, d_y, 0);
                doc.ActiveLayOut.Entities.AddItem(line);

                d_y += 140;
            }

            dist_rod_no = (int)(lx - 2 * h - 2 * c) / (100);
            d_x = h + c + 50;
            d_y = a + h;

            for (i = 0; i <= dist_rod_no; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(d_x, d_y, 0);
                line.EndPoint = new gPoint(d_x, ly - h - a, 0);
                doc.ActiveLayOut.Entities.AddItem(line);

                d_x += 100;
            }
            #endregion

            #region Corner Bottom Left
            //double h = 25;
            //double a = 500;
            //double b = 3250;
            //double c = 700;
            //double d = 4250;
            //double e = 840;
            //double f = 900;
            //double g = 2450;

            main_rod_no = (int)(f / 140);

            d_x = h;
            d_y = h + 140 / 2;

            for (i = 0; i <= main_rod_no; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(d_x, d_y, 0);
                line.EndPoint = new gPoint(d_x + e, d_y, 0);
                doc.ActiveLayOut.Entities.AddItem(line);

                d_y += 140;
            }

            dist_rod_no = (int)(e / 100);

            d_x = h + 100 / 2;
            d_y = h ;

            for (i = 0; i <= dist_rod_no; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(d_x, d_y, 0);
                line.EndPoint = new gPoint(d_x , d_y + f, 0);
                doc.ActiveLayOut.Entities.AddItem(line);

                d_x += 100;
            }


            #endregion

            #region Corner Bottom Right
            //double h = 25;
            //double a = 500;
            //double b = 3250;
            //double c = 700;
            //double d = 4250;
            //double e = 840;
            //double f = 900;
            //double g = 2450;

            main_rod_no = (int)(f / 140);

            d_x = h + c + d;
            d_y = h + 140 / 2;

            for (i = 0; i <= main_rod_no; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(d_x, d_y, 0);
                line.EndPoint = new gPoint(d_x + e, d_y, 0);
                doc.ActiveLayOut.Entities.AddItem(line);

                d_y += 140;
            }

            dist_rod_no = (int)(e / 100);

            d_x = h + c + d;
            //d_x = h + 100 / 2 + c + d;
            d_y = h;

            for (i = 0; i <= dist_rod_no; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(d_x, d_y, 0);
                line.EndPoint = new gPoint(d_x, d_y + f, 0);
                doc.ActiveLayOut.Entities.AddItem(line);

                d_x += 100;
            }


            #endregion

            #region Corner Top Left
            //double h = 25;
            //double a = 500;
            //double b = 3250;
            //double c = 700;
            //double d = 4250;
            //double e = 840;
            //double f = 900;
            //double g = 2450;

            main_rod_no = (int)(f / 140);
            //d_x = h + c + d;
            //d_y = h + 140 / 2;

            d_x = h;
            d_y = ly - h;

            for (i = 0; i <= main_rod_no; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(d_x, d_y, 0);
                line.EndPoint = new gPoint(d_x + e, d_y, 0);
                doc.ActiveLayOut.Entities.AddItem(line);

                d_y -= 140;
            }

            dist_rod_no = (int)(e / 100);

            d_x = h  + 100 / 2;
            //d_x = h + 100 / 2 + c + d;
            d_y = h +  f + g;

            for (i = 0; i <= dist_rod_no; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(d_x, d_y, 0);
                line.EndPoint = new gPoint(d_x, d_y + f, 0);
                doc.ActiveLayOut.Entities.AddItem(line);

                d_x += 100;
            }


            #endregion

            #region Corner Top Right
            //double h = 25;
            //double a = 500;
            //double b = 3250;
            //double c = 700;
            //double d = 4250;
            //double e = 840;
            //double f = 900;
            //double g = 2450;

            main_rod_no = (int)(f / 140);

            d_x = h + c + d;
            //d_y = h + 140 / 2 + f + g;
            d_y = ly - h;


            for (i = 0; i <= main_rod_no; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(d_x, d_y, 0);
                line.EndPoint = new gPoint(d_x + e, d_y, 0);
                doc.ActiveLayOut.Entities.AddItem(line);

                d_y -= 140;
            }

            dist_rod_no = (int)(e / 100);
            //dist_rod_no++;

            d_x = h + c + d;
            //d_x = h + 100 / 2 + c + d;
            d_y = h + f + g;
            //d_y = h;

            for (i = 0; i <= dist_rod_no; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(d_x, d_y, 0);
                line.EndPoint = new gPoint(d_x, d_y + f, 0);
                doc.ActiveLayOut.Entities.AddItem(line);

                d_x += 100;
            }


            #endregion

            #region Edge Strip Bottom
            //double h = 25;
            //double a = 500;
            //double b = 3250;
            //double c = 700;
            //double d = 4250;
            //double e = 840;
            //double f = 900;
            //double g = 2450;

            main_rod_no = (int)N2;
            d_x = h;
            d_y = h + 70;
            for (i = 0; i < N2; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(d_x, d_y, 0);
                line.EndPoint = new gPoint(lx - h, d_y, 0);
                doc.ActiveLayOut.Entities.AddItem(line);

                d_y += 140;
            }

            #endregion

            #region Edge Strip Top
            //double h = 25;
            //double a = 500;
            //double b = 3250;
            //double c = 700;
            //double d = 4250;
            //double e = 840;
            //double f = 900;
            //double g = 2450;

            main_rod_no = (int)N2;
            d_x = h;
            d_y = ly - h;
            //d_y = ly - h - 70;
            //d_y = ly - h - 70;
            for (i = 0; i < N2; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(d_x, d_y, 0);
                line.EndPoint = new gPoint(lx - h, d_y, 0);
                doc.ActiveLayOut.Entities.AddItem(line);

                d_y -= 140;
            }

            #endregion
            doc.ShowUCSAxis = false;
            doc.Redraw(true);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);
            Console.WriteLine("EXIT TIME : " + DateTime.Now.TimeOfDay.ToString());


        }
        public void Draw_View_Top(vdDocument doc)
        {
            double h = 25;
            lx = 5800;
            ly = 4300;
            double corner_x_length, corner_y_length, corner_long_space, corner_short_space;
            double edge_strip_x_length, edge_strip_y_length, edge_long_space, edge_short_space;
            //double edge_strip_x_space, edge_strip_y_length;
            double main_dia = 10;
            double dist_dia = 8;

            double x, y;
            int i;
            x = 0;
            y = 0;


            corner_x_length = 840;
            corner_y_length = 900;
            corner_long_space = 100;
            corner_short_space = 70;

            edge_long_space = 220;
            edge_short_space = 200;

            edge_strip_x_length = 700;
            edge_strip_y_length = 570;
            
            int main_rod,dist_rod;

            #region All

            #region Corner Reinforcement Bottom Left



            x = h;
            y = h;

            vdLine line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(x, y);
            line.EndPoint = new gPoint(x,y + corner_y_length);
            doc.ActiveLayOut.Entities.AddItem(line);

            main_rod = (int)(corner_x_length / corner_short_space);
            // X Direction increse
            for (i = 0; i < main_rod; i++)
            {
                x += corner_short_space;
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(x, y, 0);
                line.EndPoint = new gPoint(x, y + corner_y_length, 0);
                doc.ActiveLayOut.Entities.AddItem(line);
            }


            x = h;
            y = h;

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(x, y);
            line.EndPoint = new gPoint(x + corner_x_length, y);
            doc.ActiveLayOut.Entities.AddItem(line);

            dist_rod = (int)(corner_y_length / corner_long_space);
            // Y Direction increse
            for (i = 0; i < dist_rod; i++)
            {
                y += corner_long_space;
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(x, y, 0);
                line.EndPoint = new gPoint(x + corner_x_length, y, 0);
                doc.ActiveLayOut.Entities.AddItem(line);
            }

            #endregion

            #region Corner Reinforcement Bottom Right


            x = lx - corner_x_length - h;
            y = h;

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(x, y + corner_y_length);
            line.EndPoint = new gPoint(x, y);
            doc.ActiveLayOut.Entities.AddItem(line);


            main_rod = (int)(corner_x_length / corner_short_space);
            // X Direction increse
            for (i = 0; i < main_rod; i++)
            {
                x += corner_short_space;
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(x, y, 0);
                line.EndPoint = new gPoint(x, y + corner_y_length, 0);
                doc.ActiveLayOut.Entities.AddItem(line);
            }


            x = lx - corner_x_length - h;
            y = h;

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(x, y);
            line.EndPoint = new gPoint(x + corner_x_length, y);
            doc.ActiveLayOut.Entities.AddItem(line);


            dist_rod = (int)(corner_y_length / corner_long_space);
            // Y Direction increse
            for (i = 0; i < dist_rod; i++)
            {
                y += corner_long_space;
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(x, y, 0);
                line.EndPoint = new gPoint(x + corner_x_length, y, 0);
                doc.ActiveLayOut.Entities.AddItem(line);
            }

            #endregion

            #region Corner Reinforcement Top Left

            x = h;
            y = ly - h - corner_y_length;

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(x, y + corner_y_length);
            line.EndPoint = new gPoint(x, y);
            doc.ActiveLayOut.Entities.AddItem(line);

            main_rod = (int)(corner_x_length / corner_short_space);
            // X Direction increse
            for (i = 0; i < main_rod; i++)
            {
                x += corner_short_space;
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(x, y, 0);
                line.EndPoint = new gPoint(x, y + corner_y_length, 0);
                doc.ActiveLayOut.Entities.AddItem(line);
            }


            x = h;
            //y = ly - h;
            y = ly - h - corner_y_length;

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(x, y);
            line.EndPoint = new gPoint(x + corner_x_length, y);
            doc.ActiveLayOut.Entities.AddItem(line);

            dist_rod = (int)(corner_y_length / corner_long_space);
            // Y Direction increse
            for (i = 0; i < dist_rod; i++)
            {
                y += corner_long_space;
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(x, y, 0);
                line.EndPoint = new gPoint(x + corner_x_length, y, 0);
                doc.ActiveLayOut.Entities.AddItem(line);
            }

            #endregion

            #region Corner Reinforcement Top Right

           

            x = lx - h - corner_x_length;
            y = ly - h - corner_y_length;
            //y = h;
    

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(x, y + corner_y_length);
            line.EndPoint = new gPoint(x, y);
            doc.ActiveLayOut.Entities.AddItem(line);

            main_rod = (int)(corner_x_length / corner_short_space);
            // X Direction increse
            for (i = 0; i < main_rod; i++)
            {
                x += corner_short_space;
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(x, y, 0);
                line.EndPoint = new gPoint(x, y + corner_y_length, 0);
                doc.ActiveLayOut.Entities.AddItem(line);
            }


            //x = h;
            x = lx - corner_x_length - h;
            y = ly - h - corner_y_length;
            //y = h;
            line = new vdLine();

            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(x, y );
            line.EndPoint = new gPoint(x + corner_x_length, y);
            doc.ActiveLayOut.Entities.AddItem(line);

            dist_rod = (int)(corner_y_length / corner_long_space);
            // Y Direction increse
            for (i = 0; i < dist_rod; i++)
            {
                y += corner_long_space;
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(x, y, 0);
                line.EndPoint = new gPoint(x + corner_x_length, y, 0);
                doc.ActiveLayOut.Entities.AddItem(line);
            }

            #endregion

            #region Draw Poly Line

            vdPolyline pline = new vdPolyline();
            pline.SetUnRegisterDocument(doc);
            pline.setDocumentDefaults();
            pline.VertexList.Add(new gPoint(h, h));
            pline.VertexList.Add(new gPoint(lx - h, h));
            pline.VertexList.Add(new gPoint(lx - h, ly - h));
            pline.VertexList.Add(new gPoint(h, ly - h));
            pline.VertexList.Add(new gPoint(h, h));
            doc.ActiveLayOut.Entities.AddItem(pline);

            pline = new vdPolyline();
            pline.SetUnRegisterDocument(doc);
            pline.setDocumentDefaults();
            pline.VertexList.Add(new gPoint(0, 0));
            pline.VertexList.Add(new gPoint(lx - 0, 0));
            pline.VertexList.Add(new gPoint(lx - 0, ly - 0));
            pline.VertexList.Add(new gPoint(0, ly - 0));
            pline.VertexList.Add(new gPoint(0, 0));
            doc.ActiveLayOut.Entities.AddItem(pline);


            #endregion

            #endregion

            #region Draw Bottom EDGE
            x = h + corner_x_length;
            y = h;

            main_rod = (int)((lx - 2 * corner_x_length) / edge_long_space);
            // X Direction
            for (i = 0; i <= main_rod; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(x, y);
                line.EndPoint = new gPoint(x, y + edge_strip_y_length);
                doc.ActiveLayOut.Entities.AddItem(line);
                x += edge_long_space;
            }


            #endregion

            #region Draw Top EDGE
            x = h + corner_x_length;
            y = ly - h - edge_strip_y_length;

            main_rod = (int)((lx - 2 * corner_x_length) / edge_long_space);
            // X Direction
            for (i = 0; i <= main_rod; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(x, y);
                line.EndPoint = new gPoint(x, y + edge_strip_y_length);
                doc.ActiveLayOut.Entities.AddItem(line);
                x += edge_long_space;
            }


            #endregion

            #region Draw Left EDGE
            x = h;
            y = h + corner_y_length;

            main_rod = (int)((ly - 2 * corner_y_length) / edge_short_space);
            // Y Direction
            for (i = 0; i <= main_rod; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(x, y);
                line.EndPoint = new gPoint(x + edge_strip_x_length, y);
                doc.ActiveLayOut.Entities.AddItem(line);
                y += edge_short_space;
            }


            #endregion

            #region Draw Right EDGE
            x = lx - h - edge_strip_x_length;
            y = h + corner_y_length;

            main_rod = (int)((ly - 2 * corner_y_length) / edge_short_space);
            // Y Direction
            for (i = 0; i <= main_rod; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(x, y);
                line.EndPoint = new gPoint(x + edge_strip_x_length, y);
                doc.ActiveLayOut.Entities.AddItem(line);
                y += edge_short_space;
            }


            #endregion

            doc.RenderMode = VectorDraw.Render.vdRender.Mode.ShadeOn;
            doc.Redraw(true);
            
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);
            
        }


        public void Draw_Section_Along_Long_Span(vdDocument doc)
        {
            // Jay Shree Hari
            //vdLine = new vdLine();
            //vdRect = new vdRect();

            PolyLine_Triagles(doc);
            Draw_Rects(doc);
            Draw_Lines(doc);
            Draw_Text(doc);
            Draw_U_Bar(doc);
            Draw_Circles(doc);
            doc.ShowUCSAxis = false;
            doc.Redraw(true);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);
        }
        private void PolyLine_Triagles(vdDocument doc)
        {
            vdHatchProperties hp = new vdHatchProperties();
            hp.SetUnRegisterDocument(doc);
            hp.FillMode = VectorDraw.Professional.Constants.VdConstFill.VdFillModeSolid;
            hp.FillColor = new vdColor(Color.Black);

            vdPolyline pl = new vdPolyline();
            pl.SetUnRegisterDocument(doc);
            pl.setDocumentDefaults();
            pl.VertexList.Add(new gPoint(-2.2220, 7.7840, 0.0000));
            pl.VertexList.Add(new gPoint(-2.0809, 7.8526, 0.0000));
            pl.VertexList.Add(new gPoint(-2.2220, 7.9288, 0.0000));
            pl.VertexList.Add(new gPoint(-2.2220, 7.7840, 0.0000));
            pl.HatchProperties = hp;
            doc.ActiveLayOut.Entities.AddItem(pl);

            pl = new vdPolyline();
            pl.SetUnRegisterDocument(doc);
            pl.setDocumentDefaults();
            pl.VertexList.Add(new gPoint(-1.6985, 7.8586, 0.0000));
            pl.VertexList.Add(new gPoint(-1.5703, 7.7862, 0.0000));
            pl.VertexList.Add(new gPoint(-1.5703, 7.9311, 0.0000));
            pl.VertexList.Add(new gPoint(-1.6985, 7.8586, 0.0000));
            pl.HatchProperties = hp;

            doc.ActiveLayOut.Entities.AddItem(pl);



            pl = new vdPolyline();
            pl.SetUnRegisterDocument(doc);
            pl.setDocumentDefaults();
            pl.VertexList.Add(new gPoint(-0.5573, 5.2666, 0.0000));
            pl.VertexList.Add(new gPoint(-0.6272, 5.4064, 0.0000));
            pl.VertexList.Add(new gPoint(-0.7084, 5.2666, 0.0000));
            pl.VertexList.Add(new gPoint(-0.5573, 5.2666, 0.0000));
            pl.HatchProperties = hp;

            doc.ActiveLayOut.Entities.AddItem(pl);




            pl = new vdPolyline();
            pl.SetUnRegisterDocument(doc);
            pl.setDocumentDefaults();
            pl.VertexList.Add(new gPoint(-1.1100, 7.8586, 0.0000));
            pl.VertexList.Add(new gPoint(-0.9818, 7.7862, 0.0000));
            pl.VertexList.Add(new gPoint(-0.9818, 7.9311, 0.0000));
            pl.VertexList.Add(new gPoint(-1.1100, 7.8586, 0.0000));
            pl.HatchProperties = hp;

            doc.ActiveLayOut.Entities.AddItem(pl);

            pl = new vdPolyline();
            pl.SetUnRegisterDocument(doc);
            pl.setDocumentDefaults();
            pl.VertexList.Add(new gPoint(-0.1797, 7.8526, 0.0000));
            pl.VertexList.Add(new gPoint(-0.3208, 7.9288, 0.0000));
            pl.VertexList.Add(new gPoint(-0.3208, 7.7840, 0.0000));
            pl.VertexList.Add(new gPoint(-0.1797, 7.8526, 0.0000));
            pl.HatchProperties = hp;

            doc.ActiveLayOut.Entities.AddItem(pl);

            pl = new vdPolyline();
            pl.SetUnRegisterDocument(doc);
            pl.setDocumentDefaults();
            pl.VertexList.Add(new gPoint(9.5440, 7.8578, 0.0000));
            pl.VertexList.Add(new gPoint(9.6622, 7.7831, 0.0000));
            pl.VertexList.Add(new gPoint(9.6622, 7.9280, 0.0000));
            pl.VertexList.Add(new gPoint(9.5440, 7.8578, 0.0000));
            pl.HatchProperties = hp;

            doc.ActiveLayOut.Entities.AddItem(pl);

            pl = new vdPolyline();
            pl.SetUnRegisterDocument(doc);
            pl.setDocumentDefaults();
            pl.VertexList.Add(new gPoint(11.4113, 7.8549, 0.0000));
            pl.VertexList.Add(new gPoint(11.2702, 7.9312, 0.0000));
            pl.VertexList.Add(new gPoint(11.2702, 7.7863, 0.0000));
            pl.VertexList.Add(new gPoint(11.4113, 7.8549, 0.0000));
            pl.HatchProperties = hp;

            doc.ActiveLayOut.Entities.AddItem(pl);

            pl = new vdPolyline();
            pl.SetUnRegisterDocument(doc);
            pl.setDocumentDefaults();
            pl.VertexList.Add(new gPoint(-2.6120, 7.1907, 0.0000));
            pl.VertexList.Add(new gPoint(-2.5163, 6.9887, 0.0000));
            pl.VertexList.Add(new gPoint(-2.7239, 6.9905, 0.0000));
            pl.VertexList.Add(new gPoint(-2.6120, 7.1907, 0.0000));
            pl.HatchProperties = hp;

            doc.ActiveLayOut.Entities.AddItem(pl);

            pl = new vdPolyline();
            pl.SetUnRegisterDocument(doc);
            pl.setDocumentDefaults();
            pl.VertexList.Add(new gPoint(9.7989, 6.6617, 0.0000));
            pl.VertexList.Add(new gPoint(9.7989, 6.8065, 0.0000));
            pl.VertexList.Add(new gPoint(9.6806, 6.7364, 0.0000));
            pl.VertexList.Add(new gPoint(9.7989, 6.6617, 0.0000));
            pl.HatchProperties = hp;

            doc.ActiveLayOut.Entities.AddItem(pl);

            pl = new vdPolyline();
            pl.SetUnRegisterDocument(doc);
            pl.setDocumentDefaults();
            pl.VertexList.Add(new gPoint(-2.6044, 5.1975, 0.0000));
            pl.VertexList.Add(new gPoint(-2.5105, 5.3780, 0.0000));
            pl.VertexList.Add(new gPoint(-2.7151, 5.3756, 0.0000));
            pl.VertexList.Add(new gPoint(-2.6044, 5.1975, 0.0000));
            pl.HatchProperties = hp;

            doc.ActiveLayOut.Entities.AddItem(pl);

            pl = new vdPolyline();
            pl.SetUnRegisterDocument(doc);
            pl.setDocumentDefaults();
            pl.VertexList.Add(new gPoint(2.4008, 5.2434, 0.0000));
            pl.VertexList.Add(new gPoint(2.3309, 5.3832, 0.0000));
            pl.VertexList.Add(new gPoint(2.2497, 5.2434, 0.0000));
            pl.VertexList.Add(new gPoint(2.4008, 5.2434, 0.0000));
            pl.HatchProperties = hp;

            doc.ActiveLayOut.Entities.AddItem(pl);

            pl = new vdPolyline();
            pl.SetUnRegisterDocument(doc);
            pl.setDocumentDefaults();
            pl.VertexList.Add(new gPoint(6.3314, 5.3534, 0.0000));
            pl.VertexList.Add(new gPoint(6.4725, 5.2772, 0.0000));
            pl.VertexList.Add(new gPoint(6.3314, 5.2086, 0.0000));
            pl.VertexList.Add(new gPoint(6.3314, 5.3534, 0.0000));
            pl.HatchProperties = hp;

            doc.ActiveLayOut.Entities.AddItem(pl);

            pl = new vdPolyline();
            pl.SetUnRegisterDocument(doc);
            pl.setDocumentDefaults();
            pl.VertexList.Add(new gPoint(7.1088, 5.3502, 0.0000));
            pl.VertexList.Add(new gPoint(6.9807, 5.2778, 0.0000));
            pl.VertexList.Add(new gPoint(7.1088, 5.2054, 0.0000));
            pl.VertexList.Add(new gPoint(7.1088, 5.3502, 0.0000));
            pl.HatchProperties = hp;

            doc.ActiveLayOut.Entities.AddItem(pl);

            pl = new vdPolyline();
            pl.SetUnRegisterDocument(doc);
            pl.setDocumentDefaults();
            pl.VertexList.Add(new gPoint(9.6011, 5.2337, 0.0000));
            pl.VertexList.Add(new gPoint(9.5751, 5.3786, 0.0000));
            pl.VertexList.Add(new gPoint(9.4644, 5.2816, 0.0000));
            pl.VertexList.Add(new gPoint(9.6011, 5.2337, 0.0000));
            pl.HatchProperties = hp;

            doc.ActiveLayOut.Entities.AddItem(pl);

            pl = new vdPolyline();
            pl.SetUnRegisterDocument(doc);
            pl.setDocumentDefaults();
            pl.VertexList.Add(new gPoint(10.9498, 5.2535, 0.0000));
            pl.VertexList.Add(new gPoint(10.9760, 5.3691, 0.0000));
            pl.VertexList.Add(new gPoint(10.8662, 5.3146, 0.0000));
            pl.VertexList.Add(new gPoint(10.9498, 5.2535, 0.0000));
            pl.HatchProperties = hp;

            doc.ActiveLayOut.Entities.AddItem(pl);

            pl = new vdPolyline();
            pl.SetUnRegisterDocument(doc);
            pl.setDocumentDefaults();
            pl.VertexList.Add(new gPoint(-0.9797, 4.7741, 0.0000));
            pl.VertexList.Add(new gPoint(-0.9797, 4.9189, 0.0000));
            pl.VertexList.Add(new gPoint(-1.1078, 4.8465, 0.0000));
            pl.VertexList.Add(new gPoint(-0.9797, 4.7741, 0.0000));
            pl.HatchProperties = hp;

            doc.ActiveLayOut.Entities.AddItem(pl);

            pl = new vdPolyline();
            pl.SetUnRegisterDocument(doc);
            pl.setDocumentDefaults();
            pl.VertexList.Add(new gPoint(10.4743, 4.8515, 0.0000));
            pl.VertexList.Add(new gPoint(10.3332, 4.9277, 0.0000));
            pl.VertexList.Add(new gPoint(10.3332, 4.7829, 0.0000));
            pl.VertexList.Add(new gPoint(10.4743, 4.8515, 0.0000));
            pl.HatchProperties = hp;

            doc.ActiveLayOut.Entities.AddItem(pl);

            pl = new vdPolyline();
            pl.SetUnRegisterDocument(doc);
            pl.setDocumentDefaults();
            pl.VertexList.Add(new gPoint(-2.0272, 4.0371, 0.0000));
            pl.VertexList.Add(new gPoint(-1.8991, 3.9647, 0.0000));
            pl.VertexList.Add(new gPoint(-1.8991, 4.1095, 0.0000));
            pl.VertexList.Add(new gPoint(-2.0272, 4.0371, 0.0000));
            pl.HatchProperties = hp;

            doc.ActiveLayOut.Entities.AddItem(pl);

            pl = new vdPolyline();
            pl.SetUnRegisterDocument(doc);
            pl.setDocumentDefaults();
            pl.VertexList.Add(new gPoint(-2.0272, 4.0290, 0.0000));
            pl.VertexList.Add(new gPoint(-1.8991, 3.9566, 0.0000));
            pl.VertexList.Add(new gPoint(-1.8991, 4.1014, 0.0000));
            pl.VertexList.Add(new gPoint(-2.0272, 4.0290, 0.0000));
            pl.HatchProperties = hp;

            doc.ActiveLayOut.Entities.AddItem(pl);

            pl = new vdPolyline();
            pl.SetUnRegisterDocument(doc);
            pl.setDocumentDefaults();
            pl.VertexList.Add(new gPoint(-1.3751, 4.0310, 0.0000));
            pl.VertexList.Add(new gPoint(-1.5162, 4.1072, 0.0000));
            pl.VertexList.Add(new gPoint(-1.5162, 3.9624, 0.0000));
            pl.VertexList.Add(new gPoint(-1.3751, 4.0310, 0.0000));
            pl.HatchProperties = hp;

            doc.ActiveLayOut.Entities.AddItem(pl);

            pl = new vdPolyline();
            pl.SetUnRegisterDocument(doc);
            pl.setDocumentDefaults();
            pl.VertexList.Add(new gPoint(-1.3741, 4.0245, 0.0000));
            pl.VertexList.Add(new gPoint(-1.2459, 3.9521, 0.0000));
            pl.VertexList.Add(new gPoint(-1.2459, 4.0970, 0.0000));
            pl.VertexList.Add(new gPoint(-1.3741, 4.0245, 0.0000));
            pl.HatchProperties = hp;

            doc.ActiveLayOut.Entities.AddItem(pl);

            pl = new vdPolyline();
            pl.SetUnRegisterDocument(doc);
            pl.setDocumentDefaults();
            pl.VertexList.Add(new gPoint(-0.4014, 3.9553, 0.0000));
            pl.VertexList.Add(new gPoint(-0.2603, 4.0239, 0.0000));
            pl.VertexList.Add(new gPoint(-0.4014, 4.1002, 0.0000));
            pl.VertexList.Add(new gPoint(-0.4014, 3.9553, 0.0000));
            pl.HatchProperties = hp;

            doc.ActiveLayOut.Entities.AddItem(pl);

            pl = new vdPolyline();
            pl.SetUnRegisterDocument(doc);
            pl.setDocumentDefaults();
            pl.VertexList.Add(new gPoint(9.6257, 4.0278, 0.0000));
            pl.VertexList.Add(new gPoint(9.7538, 3.9554, 0.0000));
            pl.VertexList.Add(new gPoint(9.7538, 4.1002, 0.0000));
            pl.VertexList.Add(new gPoint(9.6257, 4.0278, 0.0000));
            pl.HatchProperties = hp;

            doc.ActiveLayOut.Entities.AddItem(pl);

            pl = new vdPolyline();
            pl.SetUnRegisterDocument(doc);
            pl.setDocumentDefaults();
            pl.VertexList.Add(new gPoint(10.7447, 4.0255, 0.0000));
            pl.VertexList.Add(new gPoint(10.6036, 4.1017, 0.0000));
            pl.VertexList.Add(new gPoint(10.6036, 3.9569, 0.0000));
            pl.VertexList.Add(new gPoint(10.7447, 4.0255, 0.0000));
            pl.HatchProperties = hp;

            doc.ActiveLayOut.Entities.AddItem(pl);

            pl = new vdPolyline();
            pl.SetUnRegisterDocument(doc);
            pl.setDocumentDefaults();
            pl.VertexList.Add(new gPoint(-1.3741, 3.3916, 0.0000));
            pl.VertexList.Add(new gPoint(-1.2460, 3.4640, 0.0000));
            pl.VertexList.Add(new gPoint(-1.2460, 3.3192, 0.0000));
            pl.VertexList.Add(new gPoint(-1.3741, 3.3916, 0.0000));
            pl.HatchProperties = hp;

            doc.ActiveLayOut.Entities.AddItem(pl);

            pl = new vdPolyline();
            pl.SetUnRegisterDocument(doc);
            pl.setDocumentDefaults();
            pl.VertexList.Add(new gPoint(10.6065, 3.4599, 0.0000));
            pl.VertexList.Add(new gPoint(10.6065, 3.3150, 0.0000));
            pl.VertexList.Add(new gPoint(10.7476, 3.3836, 0.0000));
            pl.VertexList.Add(new gPoint(10.6065, 3.4599, 0.0000));
            pl.HatchProperties = hp;

            doc.ActiveLayOut.Entities.AddItem(pl);

            User_Text(doc);
        }
        private void Draw_Rects(vdDocument doc)
        {
            vdHatchProperties hp = new vdHatchProperties();
            hp.SetUnRegisterDocument(doc);
            hp.FillMode = VectorDraw.Professional.Constants.VdConstFill.VdFillModeHatchFDiagonal;

            vdRect rect = new vdRect();
            rect.SetUnRegisterDocument(doc);
            rect.setDocumentDefaults();
            rect.InsertionPoint = new gPoint(-2.0437, 7.1825, 0.0000);
            rect.Height = -2.0043;
            rect.Width = 13.4986;
            doc.ActiveLayOut.Entities.AddItem(rect);

            rect = new vdRect();
            rect.SetUnRegisterDocument(doc);
            rect.setDocumentDefaults();
            rect.InsertionPoint = new gPoint(-3.5944, 9.5029, 0.0000);
            rect.Height = -7.6742;
            rect.Width = 16.6133;
            doc.ActiveLayOut.Entities.AddItem(rect);

            rect = new vdRect();
            rect.SetUnRegisterDocument(doc);
            rect.setDocumentDefaults();
            rect.InsertionPoint = new gPoint(-1.1063, 4.4103, 0.0000);
            rect.HatchProperties = hp;
            rect.Height = 0.7722;
            rect.Width = -0.9374;
            doc.ActiveLayOut.Entities.AddItem(rect);

            rect = new vdRect();
            rect.SetUnRegisterDocument(doc);
            rect.setDocumentDefaults();
            rect.InsertionPoint = new gPoint(11.4549, 4.4103, 0.0000);
            rect.HatchProperties = hp;
            rect.Height = 0.7722;
            rect.Width = -0.9198;
            doc.ActiveLayOut.Entities.AddItem(rect);


            rect = new vdRect();
            rect.SetUnRegisterDocument(doc);
            rect.setDocumentDefaults();
            rect.InsertionPoint = new gPoint(13.2552, 8.4317, 0.0000);
            rect.Height = -5.3336;
            rect.Width = 3.6504;
            doc.ActiveLayOut.Entities.AddItem(rect);


            rect = new vdRect();
            rect.SetUnRegisterDocument(doc);
            rect.setDocumentDefaults();
            rect.InsertionPoint = new gPoint(-3.6223, 1.4053, 0.0000);
            rect.Height = -1.8098;
            rect.Width = 16.6234;
            doc.ActiveLayOut.Entities.AddItem(rect);

            

        }
        private void Draw_Circles(vdDocument doc)
        {
            vdHatchProperties hp = new vdHatchProperties();
            hp.SetUnRegisterDocument(doc);
            hp.FillMode = VectorDraw.Professional.Constants.VdConstFill.VdFillModeSolid;
            hp.FillColor = new vdColor(Color.Black);

            vdCircle StartCir = new vdCircle();
            StartCir.SetUnRegisterDocument(doc);
            StartCir.setDocumentDefaults();
            StartCir.Center = new gPoint(-1.6362, 5.4332, 0.0000);
            StartCir.Radius = 0.0500;
            StartCir.HatchProperties = hp;
            doc.ActiveLayOut.Entities.AddItem(StartCir);
        
            vdCircle EndCir = new vdCircle();
            EndCir.SetUnRegisterDocument(doc);
            EndCir.setDocumentDefaults();
            EndCir.Center = new gPoint(11.0173, 5.4332, 0.0000);
            EndCir.Radius = 0.0500;
            EndCir.HatchProperties = hp;
            doc.ActiveLayOut.Entities.AddItem(EndCir);


            double dx = 0.28d;

            double dist  = (EndCir.Center.x - StartCir.Center.x);

            int cnt = (int)(dist / dx);

            for (int i = 0; i < cnt; i++)
            {
                EndCir = new vdCircle();
                EndCir.SetUnRegisterDocument(doc);
                EndCir.setDocumentDefaults();
                EndCir.Center = new gPoint(StartCir.Center.x + (i * dx), 5.4332, 0.0000);
                EndCir.Radius = 0.0500;
                EndCir.HatchProperties = hp;
                doc.ActiveLayOut.Entities.AddItem(EndCir);


            }
            
        }
        private void Draw_Lines(vdDocument doc)
        {
            vdLine line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-2.3724, 7.8467, 0.0000);
            line.EndPoint = new gPoint(-2.1886, 7.8467, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-2.0775, 8.0952, 0.0000);
            line.EndPoint = new gPoint(-2.0775, 7.6065, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-1.7038, 8.0952, 0.0000);
            line.EndPoint = new gPoint(-1.7038, 7.6025, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-1.1153, 8.0952, 0.0000);
            line.EndPoint = new gPoint(-1.1153, 7.6025, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-0.1606, 6.8395, 0.0000);
            line.EndPoint = new gPoint(-1.7235, 6.8395, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-1.6094, 7.8521, 0.0000);
            line.EndPoint = new gPoint(-1.4256, 7.8521, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-1.0210, 7.8521, 0.0000);
            line.EndPoint = new gPoint(-0.8371, 7.8521, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-0.2873, 7.8467, 0.0000);
            line.EndPoint = new gPoint(-0.4712, 7.8467, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-0.1762, 8.0952, 0.0000);
            line.EndPoint = new gPoint(-0.1762, 7.6065, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(8.5126, 7.8233, 0.0000);
            line.EndPoint = new gPoint(7.0214, 7.8233, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(9.5396, 8.0976, 0.0000);
            line.EndPoint = new gPoint(9.5396, 7.6049, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(9.6231, 7.8491, 0.0000);
            line.EndPoint = new gPoint(11.2935, 7.8491, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(11.4148, 8.0976, 0.0000);
            line.EndPoint = new gPoint(11.4148, 7.6089, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-2.8736, 7.1906, 0.0000);
            line.EndPoint = new gPoint(-2.3558, 7.1906, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(9.6072, 6.4818, 0.0000);
            line.EndPoint = new gPoint(8.5126, 7.8233, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(11.1188, 6.8220, 0.0000);
            line.EndPoint = new gPoint(9.5559, 6.8220, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-2.6147, 7.1906, 0.0000);
            line.EndPoint = new gPoint(-2.6156, 6.3415, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-1.7235, 6.8395, 0.0000);
            line.EndPoint = new gPoint(-1.7235, 5.4837, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(11.1188, 5.4837, 0.0000);
            line.EndPoint = new gPoint(11.1188, 6.8206, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-2.6031, 5.9006, 0.0000);
            line.EndPoint = new gPoint(-2.6031, 5.1994, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-1.7235, 5.4837, 0.0000);
            line.EndPoint = new gPoint(11.1188, 5.4837, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-0.2359, 5.4837, 0.0000);
            line.EndPoint = new gPoint(-0.4054, 5.7096, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(9.6124, 5.4837, 0.0000);
            line.EndPoint = new gPoint(9.7631, 5.7285, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(9.7585, 6.7202, 0.0000);
            line.EndPoint = new gPoint(9.6072, 6.4818, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-2.8736, 5.1977, 0.0000);
            line.EndPoint = new gPoint(-2.3558, 5.1977, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-0.6297, 5.4719, 0.0000);
            line.EndPoint = new gPoint(-0.6297, 4.4768, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(2.3309,5.3832,0.0000);
            line.EndPoint = new gPoint(2.3309, 4.4946, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(6.3760, 5.2761, 0.0000);
            line.EndPoint = new gPoint(6.7119, 4.5997, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(6.7119, 4.5997, 0.0000);
            line.EndPoint = new gPoint(7.0661, 5.2670, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(9.5540, 5.2898, 0.0000);
            line.EndPoint = new gPoint(9.1085, 4.4053, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(10.9390, 5.2870, 0.0000);
            line.EndPoint = new gPoint(9.1085, 4.4053, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-0.6297, 4.4768, 0.0000);
            line.EndPoint = new gPoint(-0.2718, 4.4768, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(2.3309, 4.4946, 0.0000);
            line.EndPoint = new gPoint(2.7001, 4.4946, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(6.7119, 4.5997, 0.0000);
            line.EndPoint = new gPoint(6.2741, 4.5997, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(9.1085, 4.4053, 0.0000);
            line.EndPoint = new gPoint(8.5297, 4.4053, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(10.7483, 4.8382, 0.0000);
            line.EndPoint = new gPoint(10.7483, 3.0344, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-2.0325, 4.2900, 0.0000);
            line.EndPoint = new gPoint(-2.0325, 3.7973, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-1.9382, 4.0225, 0.0000);
            line.EndPoint = new gPoint(-1.7544, 4.0225, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-1.4827, 4.0251, 0.0000);
            line.EndPoint = new gPoint(-1.6665, 4.0251, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-1.3753, 4.2900, 0.0000);
            line.EndPoint = new gPoint(-1.3753, 3.8013, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-1.2932, 4.0180, 0.0000);
            line.EndPoint = new gPoint(-0.3542, 4.0183, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-0.2588, 4.2900, 0.0000);
            line.EndPoint = new gPoint(-0.2588, 3.8013, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(9.6251, 4.2745, 0.0000);
            line.EndPoint = new gPoint(9.6251, 3.7858, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(9.7147, 4.0213, 0.0000);
            line.EndPoint = new gPoint(10.7447, 4.0255, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-1.3766, 3.6106, 0.0000);
            line.EndPoint = new gPoint(-1.3766, 3.1179, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            //line = new vdLine();
            //line.SetUnRegisterDocument(doc);
            //line.setDocumentDefaults();
            //line.StartPoint = new gPoint(10.7476, 3.3836, 0.0000);
            //line.EndPoint = new gPoint(-1.3296, 3.3875, 0.0000);
            //doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(10.7483, 4.8382, 0.0000);
            line.EndPoint = new gPoint(10.7483, 3.0344, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-1.3746, 4.7928, 0.0000);
            line.EndPoint = new gPoint(-1.3784, 3.1047, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);

            //line = new vdLine();
            //line.SetUnRegisterDocument(doc);
            //line.setDocumentDefaults();
            //line.StartPoint = new gPoint(-1.1167, 4.8491, 0.0000);
            //line.EndPoint = new gPoint(10.4594, 4.8491, 0.0000);
            //doc.ActiveLayOut.Entities.AddItem(line);

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(-1.1167, 4.8491, 0.0000);
            line.EndPoint = new gPoint(4.1233, 4.8519, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(4.7865, 4.8491, 0.0000);
            line.EndPoint = new gPoint(10.4594, 4.8491, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(3.9424, 3.3858, 0.0000);
            line.EndPoint = new gPoint(-1.3296, 3.3875, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(10.7476, 3.3836, 0.0000);
            line.EndPoint = new gPoint(4.6056, 3.3856, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(line);
        }
        private void Draw_Text(vdDocument doc)
        {
            // Draw A
            vdText txt = new vdText();

            #region Text A
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(-2.0208, 7.7665, 0.0000);
            txt.TextString = "A";
            txt.PenColor = new vdColor(Color.Red);
            txt.Height = 0.3;
            doc.ActiveLayOut.Entities.AddItem(txt);



            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(13.4709, 8.0701, 0.0000);
            txt.TextString = "A";
            txt.PenColor = new vdColor(Color.Red);
            txt.Height = 0.3;
            doc.ActiveLayOut.Entities.AddItem(txt);



            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(14.0629, 8.1013, 0.0000);
            txt.TextString = "=";
            txt.PenColor = new vdColor(Color.Red);
            txt.Height = 0.3;
            doc.ActiveLayOut.Entities.AddItem(txt);

            #endregion

            #region Text B
            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(-0.7723, 7.7451, 0.0000);
            txt.TextString = "B";
            txt.Height = 0.3;
            txt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(13.4709, 7.6083, 0.0000);
            txt.TextString = "B";
            txt.Height = 0.3;
            txt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(-0.8796, 4.1371, 0.0000);
            txt.TextString = "B";
            txt.Height = 0.3;
            txt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(9.9165, 4.1683, 0.0000);
            txt.TextString = "B";
            txt.Height = 0.3;
            txt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(txt);



            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(14.0629, 7.5405, 0.0000);
            txt.TextString = "=";
            txt.PenColor = new vdColor(Color.Red);
            txt.Height = 0.3;
            doc.ActiveLayOut.Entities.AddItem(txt);

            #endregion

            #region Text C


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(10.3010, 7.9937, 0.0000);
            txt.TextString = "C";
            txt.Height = 0.3;
            txt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(13.4709, 7.0599, 0.0000);
            txt.TextString = "C";
            txt.Height = 0.3;
            txt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(14.0629, 7.1043, 0.0000);
            txt.TextString = "=";
            txt.PenColor = new vdColor(Color.Red);
            txt.Height = 0.3;
            doc.ActiveLayOut.Entities.AddItem(txt);

            #endregion

            #region Text D


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(-2.6877, 6.0168, 0.0000);
            txt.TextString  = "D";
            txt.Height = 0.3;
            txt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(13.4709, 6.5115, 0.0000);
            txt.TextString = "D";
            txt.Height = 0.3;
            txt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(txt);



            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(14.0629, 6.5124, 0.0000);
            txt.TextString = "=";
            txt.PenColor = new vdColor(Color.Red);
            txt.Height = 0.3;
            doc.ActiveLayOut.Entities.AddItem(txt);
            #endregion

            #region Text E


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(4.3544, 4.6841, 0.0000);
            txt.TextString  = "E";
            txt.Height = 0.3;
            txt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(13.4709, 6.0497, 0.0000);
            txt.TextString  = "E";
            txt.Height = 0.3;
            txt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(14.0629, 5.9828, 0.0000);
            txt.TextString = "=";
            txt.PenColor = new vdColor(Color.Red);
            txt.Height = 0.3;
            doc.ActiveLayOut.Entities.AddItem(txt);

            #endregion

            #region Text F


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(4.1735, 3.3000, 0.0000);
            txt.TextString = "F";
            txt.Height = 0.3;
            txt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(13.4709, 5.5013, 0.0000);
            txt.TextString = "F";
            txt.Height = 0.3;
            txt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(txt);




            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(14.0629, 5.5466, 0.0000);
            txt.TextString = "=";
            txt.PenColor = new vdColor(Color.Red);
            txt.Height = 0.3;
            doc.ActiveLayOut.Entities.AddItem(txt);
            #endregion

            #region Text G


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(-0.1330, 4.4399, 0.0000);
            txt.TextString = "G";
            txt.Height = 0.3;
            txt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(14.0629, 4.5206, 0.0000);
            txt.TextString = "=";
            txt.PenColor = new vdColor(Color.Red);
            txt.Height = 0.3;
            doc.ActiveLayOut.Entities.AddItem(txt);

            #endregion

            #region Text G

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(13.4709, 4.9241, 0.0000);
            txt.TextString = "G";
            txt.Height = 0.3;
            txt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(6.6353, 7.7296, 0.0000);
            txt.TextString = "G";
            txt.Height = 0.3;
            txt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(14.0629, 4.9494, 0.0000);
            txt.TextString = "=";
            txt.Height = 0.3;
            txt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(txt);

            #endregion

            #region Text H
            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(2.8095, 4.4399, 0.0000);
            txt.TextString = "H";
            txt.Height = 0.3;
            txt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(13.4709, 4.4623, 0.0000);
            txt.TextString = "H";
            txt.Height = 0.3;
            txt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(14.0629, 4.5206, 0.0000);
            txt.TextString = "=";
            txt.Height = 0.3;
            txt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(txt);
            #endregion

            #region  TEXT J

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(6.0751, 4.4399, 0.0000);
            txt.TextString = "J";
            txt.Height = 0.3;
            txt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(13.4709, 3.8570, 0.0000);
            txt.TextString = "J";
            txt.Height = 0.3;
            txt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(14.0629, 3.8850, 0.0000);
            txt.TextString = "=";
            txt.Height = 0.3;
            txt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(txt);
            #endregion

            #region  TEXT K

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(8.7227, 4.4399, 0.0000);
            txt.TextString = "K";
            txt.Height = 0.3;
            txt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(13.4816, 3.4048, 0.0000);
            txt.TextString = "K";
            txt.Height = 0.3;
            txt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(14.0629, 3.3657, 0.0000);
            txt.TextString = "=";
            txt.Height = 0.3;
            txt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(txt);
            #endregion 

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(0.5977, 2.0542, 0.0000);
            txt.TextString = "(c) SECTION ALONG LONG SPAN";
            txt.Height = 0.3;
            txt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(0.4706, -0.1702, 0.0000);
            txt.TextString = "(d) U - BAR";
            txt.Height = 0.3;
            txt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(txt);

        }
        private void User_Text(vdDocument doc)
        {
            List<string> list = new List<string>();
            //Hashtable hash_T = new Hashtable();
            //hash_T.Add("A", "25 mm");
            //hash_T.Add("B", "560 mm");
            //hash_T.Add("C", "710 mm");
            //hash_T.Add("D", "110 mm");
            //hash_T.Add("E", "5500 mm");
            //hash_T.Add("F", "5590 mm");
            //hash_T.Add("G", "8 - 200 c/c");
            //hash_T.Add("H", "8 - 100 c/c");
            //hash_T.Add("J", "10 - 110 c/c");
            //hash_T.Add("K", "8 - 140 c/c");

            #region Draw User Text
            vdText txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(13.2871, 8.6165, 0.0000);
            txt.Height = 0.3d;
            txt.TextString = "USER TEXT Table";
            txt.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(14.5302, 8.1094, 0.0000);
            txt.Height = 0.3d;
            txt.TextString = hash_T["A"].ToString();
            txt.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(14.5302, 7.6187, 0.0000);
            txt.Height = 0.3d;
            txt.TextString = hash_T["B"].ToString();
            txt.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(14.5302, 7.1735, 0.0000);
            txt.Height = 0.3d;
            txt.TextString = hash_T["C"].ToString();
            txt.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(txt);



            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(14.5302, 6.6209, 0.0000);
            txt.Height = 0.3d;
            txt.TextString = hash_T["D"].ToString();
            txt.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(14.5302, 6.0984, 0.0000);
            txt.Height = 0.3d;
            txt.TextString = hash_T["E"].ToString();
            txt.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(14.5302, 5.5554, 0.0000);
            txt.Height = 0.3d;
            txt.TextString = hash_T["F"].ToString();
            txt.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(14.5302, 5.0429, 0.0000);
            txt.Height = 0.3d;
            txt.TextString = hash_T["G"].ToString();
            txt.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(14.5302, 4.5292, 0.0000);
            txt.Height = 0.3d;
            txt.TextString = hash_T["H"].ToString();
            txt.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(14.5302, 3.9494, 0.0000);
            txt.Height = 0.3d;
            txt.TextString = hash_T["J"].ToString();
            txt.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(14.5302, 3.4324, 0.0000);
            txt.Height = 0.3d;
            txt.TextString = hash_T["K"].ToString();
            txt.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(txt);

            #endregion


        }
        private void Draw_U_Bar(vdDocument doc)
        {

            double radian = (Math.PI / 180);

            vdLine ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new gPoint(-0.4126, 1.1009, 0.0000);
            ln.EndPoint = new gPoint(-1.6880, 1.1009, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);

            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new gPoint(11.1639, 1.1009, 0.0000);
            ln.EndPoint = new gPoint(9.8885, 1.1009, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);


            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new gPoint(11.1226, 0.4063, 0.0000);
            ln.EndPoint = new gPoint(-1.5975, 0.4039, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);


            vdArc arc = new vdArc();
            arc.SetUnRegisterDocument(doc);
            arc.setDocumentDefaults();
            arc.Center = new gPoint(-1.6777, 0.7525, 0.0000);
            arc.ExtrusionVector = new Vector(0, 0, -1);
            arc.Radius = 0.3508;
            arc.StartAngle = radian * 260;
            arc.EndAngle = radian * 96;
            doc.ActiveLayOut.Entities.AddItem(arc);

            arc = new vdArc();
            arc.SetUnRegisterDocument(doc);
            arc.setDocumentDefaults();
            arc.Center = new gPoint(11.1325, 0.7543, 0.0000);
            arc.Radius = 0.3508;
            arc.StartAngle = radian * 268;
            arc.EndAngle = radian * 84;
            doc.ActiveLayOut.Entities.AddItem(arc);
        }

        public bool Read_Drawing_Data_From_File(string file_name)
        {
            if (!File.Exists(file_name)) return false;

            List<string> list = new List<string>(File.ReadAllLines(file_name));

            string kStr = "";
            MyStrings mList = null;

            for (int i = 0; i < list.Count; i++)
            {
                kStr = list[i].Trim().TrimStart().TrimEnd();

                mList = new MyStrings(kStr, '=');
                if (mList.Count > 1)
                {
                    kStr = mList.StringList[0].Trim().TrimEnd().TrimStart();
                    switch (kStr)
                    {
                        case "A":
                            hash_T.Add("A", mList.StringList[1]);
                            break;
                        case "B":
                            hash_T.Add("B", mList.StringList[1]);
                            break;
                        case "C":
                            hash_T.Add("C", mList.StringList[1]);
                            break;
                        case "D":
                            hash_T.Add("D", mList.StringList[1]);
                            break;
                        case "E":
                            hash_T.Add("E", mList.StringList[1]);
                            break;
                        case "F":
                            hash_T.Add("F", mList.StringList[1]);
                            break;
                        case "G":
                            hash_T.Add("G", mList.StringList[1]);
                            break;
                        case "H":
                            hash_T.Add("H", mList.StringList[1]);
                            break;
                        case "J":
                            hash_T.Add("J", mList.StringList[1]);
                            break;
                        case "K":
                            hash_T.Add("K", mList.StringList[1]);
                            break;
                    }
                }
            }
            return true;

        }


        public void Initialize_Layer(vdDocument doc)
        {
            
            //vdLayer top_lay = new vdLayer();
            top_lay.SetUnRegisterDocument(doc);
            top_lay.setDocumentDefaults();
            top_lay.Name = "TopView";
            doc.Layers.AddItem(top_lay);

            //vdLayer bottom_lay = new vdLayer();
            bottom_lay.SetUnRegisterDocument(doc);
            bottom_lay.setDocumentDefaults();
            bottom_lay.Name = "BottomView";
            doc.Layers.AddItem(bottom_lay);

            //vdLayer edge_strip_lay = new vdLayer();
            edge_strip_lay.SetUnRegisterDocument(doc);
            edge_strip_lay.setDocumentDefaults();
            edge_strip_lay.Name = "EdgeStrip";
            doc.Layers.AddItem(edge_strip_lay);


            //vdLayer top_corner_lay = new vdLayer();
            top_corner_lay.SetUnRegisterDocument(doc);
            top_corner_lay.setDocumentDefaults();
            top_corner_lay.Name = "TopCorners";
            doc.Layers.AddItem(top_corner_lay);

            //vdLayer bottom_corner_lay = new vdLayer();
            bottom_corner_lay.SetUnRegisterDocument(doc);
            bottom_corner_lay.setDocumentDefaults();
            bottom_corner_lay.Name = "BottomCorners";
            doc.Layers.AddItem(bottom_corner_lay);
        }
        public void Draw_View(vdDocument doc, string fileName)
        {
            PanelMomentType = PanelMoment_Type.FOUR_EDGES_DISCONTINUOUS;
            //PanelMomentType = PanelMoment_Type.INTERIOR_PANELS;
            //PanelMomentType = PanelMoment_Type.ONE_LONG_EDGE_DISCONTINUOUS;
            //PanelMomentType = PanelMoment_Type.ONE_SHORT_EDGE_DISCONTINUOUS;
            //PanelMomentType = PanelMoment_Type.THREE_EDGES_DISCONTINUOUS_ONE_LONG_EDGE_CONTINUOUS;
            //PanelMomentType = PanelMoment_Type.THREE_EDGES_DISCONTINUOUS_ONE_SHORT_EDGE_CONTINUOUS;
            //PanelMomentType = PanelMoment_Type.TWO_ADJACENT_EDGES_DISCONTINUOUS;
            //PanelMomentType = PanelMoment_Type.TWO_LONG_EDGES_DISCONTINUOUS;
            //PanelMomentType = PanelMoment_Type.TWO_SHORT_EDGES_DISCONTINUOUS;
            Initialize_Layer(doc);
            //if (!File.Exists(fileName))
            //    fileName = @"D:\SOFTWARE TESTING\design.fil";

            #region SWAP Condition
            if (Read_View_Data_From_File(fileName))
            {
                double val = 0.0d;
                val = long_span;
                long_span = short_span;
                short_span = val;


                //long_span = 5700;
                //short_span = 4300;
                clear_cover = 25;
                height = 1100;

                val = start_long_dist;
                start_long_dist = start_short_dist;
                start_short_dist = val;

                //start_long_dist = 700;
                //start_short_dist = 500;
                val = long_space;
                long_space = short_space;
                short_space = val;

                //long_space = 140;
                //short_space = 100;

                middle_bar_dist = 110;

                val = long_span_Corner;
                long_span_Corner = short_span_Corner;
                short_span_Corner = val;

                //long_span_Corner = 840;
                //short_span_Corner = 900;
                val = long_corner_space;
                long_corner_space = short_corner_space;
                short_corner_space = val;

                //long_corner_space = 70;
                //short_corner_space = 100;
            }
            #endregion
            #region Condition
            /**/
            if (!Read_View_Data_From_File(fileName))
            {

                long_span = 5700;
                short_span = 4300;
                clear_cover = 25;
                height = 1100;
                start_long_dist = 700;
                start_short_dist = 500;
                long_space = 140;
                short_space = 100;

                middle_bar_dist = 110;

                long_span_Corner = 840;
                short_span_Corner = 900;
                long_corner_space = 70;
                short_corner_space = 100;
            }
            /**/
            #endregion
            dz = short_space;
            dx = long_space;

          

            _long_span_Corner = long_span_Corner;
            _short_span_Corner = short_span_Corner;
            _long_corner_space = long_corner_space;
            _short_corner_space = short_corner_space;

            Along_Long_Span(doc);
            Along_Short_Span(doc);
            Middle_Along_Long_Span(doc);
            Middle_Along_Short_Span(doc);
            //Edge_Strip(doc);


            #region switch
            switch (PanelMomentType)
            {
                case PanelMoment_Type.FOUR_EDGES_DISCONTINUOUS:
                    {
                        Edge_Strip_TOP(doc);
                        Edge_Strip_RIGHT(doc);
                        Edge_Strip_BOTTOM(doc);
                        Edge_Strip_LEFT(doc);

                        Corners_BOTTOM_TOP_LEFT(doc);
                        Corners_TOP_TOP_LEFT(doc);
                        Corners_BOTTOM_TOP_RIGHT(doc);
                        Corners_TOP_TOP_RIGHT(doc);
                        Corners_BOTTOM_BOTTOM_LEFT(doc);
                        Corners_TOP_BOTTOM_LEFT(doc);
                        Corners_BOTTOM_BOTTOM_RIGHT(doc);
                        Corners_TOP_BOTTOM_RIGHT(doc);
                       

                    }
                    break;
                case PanelMoment_Type.INTERIOR_PANELS:
                    {
                    }
                    break;

                case PanelMoment_Type.ONE_LONG_EDGE_DISCONTINUOUS:
                    {
                        Edge_Strip_TOP(doc);

                        long_corner_space = _long_corner_space * 2;
                        short_corner_space = _short_corner_space * 2;

                        Corners_BOTTOM_TOP_LEFT(doc);
                        Corners_TOP_TOP_LEFT(doc);
                        Corners_BOTTOM_TOP_RIGHT(doc);
                        Corners_TOP_TOP_RIGHT(doc);
                        //Corners_BOTTOM_BOTTOM_LEFT(doc);
                        //Corners_TOP_BOTTOM_LEFT(doc);
                        //Corners_BOTTOM_BOTTOM_RIGHT(doc);
                        //Corners_TOP_BOTTOM_RIGHT(doc);
                    }
                    break;

                case PanelMoment_Type.ONE_SHORT_EDGE_DISCONTINUOUS:
                    {
                        Edge_Strip_LEFT(doc);

                        long_corner_space = _long_corner_space * 2;
                        short_corner_space = _short_corner_space * 2;


                        Corners_BOTTOM_TOP_LEFT(doc);
                        Corners_TOP_TOP_LEFT(doc);
                        //Corners_BOTTOM_TOP_RIGHT(doc);
                        //Corners_TOP_TOP_RIGHT(doc);
                        Corners_BOTTOM_BOTTOM_LEFT(doc);
                        Corners_TOP_BOTTOM_LEFT(doc);
                        //Corners_BOTTOM_BOTTOM_RIGHT(doc);
                        //Corners_TOP_BOTTOM_RIGHT(doc);
                    }
                    break;

                case PanelMoment_Type.THREE_EDGES_DISCONTINUOUS_ONE_LONG_EDGE_CONTINUOUS:
                    {
                        long_corner_space = _long_corner_space;
                        short_corner_space = _short_corner_space;

                        Edge_Strip_TOP(doc);
                        Edge_Strip_LEFT(doc);
                        Edge_Strip_RIGHT(doc);

                        Corners_BOTTOM_TOP_LEFT(doc);
                        Corners_TOP_TOP_LEFT(doc);
                        Corners_BOTTOM_TOP_RIGHT(doc);
                        Corners_TOP_TOP_RIGHT(doc);


                        long_corner_space = _long_corner_space * 2;
                        short_corner_space = short_corner_space * 2;

                        Corners_BOTTOM_BOTTOM_LEFT(doc);
                        Corners_TOP_BOTTOM_LEFT(doc);
                        Corners_BOTTOM_BOTTOM_RIGHT(doc);
                        Corners_TOP_BOTTOM_RIGHT(doc);
                    }
                    break;

                case PanelMoment_Type.THREE_EDGES_DISCONTINUOUS_ONE_SHORT_EDGE_CONTINUOUS:
                    {
                        Edge_Strip_TOP(doc);
                        Edge_Strip_RIGHT(doc);
                        Edge_Strip_BOTTOM(doc);


                        long_corner_space = _long_corner_space * 2;
                        short_corner_space = _short_corner_space * 2;

                        Corners_BOTTOM_TOP_LEFT(doc);
                        Corners_TOP_TOP_LEFT(doc);

                        Corners_BOTTOM_BOTTOM_LEFT(doc);
                        Corners_TOP_BOTTOM_LEFT(doc);

                        long_corner_space = _long_corner_space;
                        short_corner_space = _short_corner_space;

                        Corners_BOTTOM_TOP_RIGHT(doc);
                        Corners_TOP_TOP_RIGHT(doc);

                        Corners_BOTTOM_BOTTOM_RIGHT(doc);
                        Corners_TOP_BOTTOM_RIGHT(doc);
                    }
                    break;

                case PanelMoment_Type.TWO_ADJACENT_EDGES_DISCONTINUOUS:
                    {
                        Edge_Strip_TOP(doc);
                        //Edge_Strip_RIGHT(doc);
                        Edge_Strip_BOTTOM(doc);
                        Edge_Strip_LEFT(doc);

                        long_corner_space = _long_corner_space;
                        short_corner_space = _short_corner_space;
                       
                        Corners_BOTTOM_TOP_LEFT(doc);
                        Corners_TOP_TOP_LEFT(doc);

                        long_corner_space = _long_corner_space * 2;
                        short_corner_space = _short_corner_space * 2;

                        Corners_BOTTOM_TOP_RIGHT(doc);
                        Corners_TOP_TOP_RIGHT(doc);
                        Corners_BOTTOM_BOTTOM_LEFT(doc);
                        Corners_TOP_BOTTOM_LEFT(doc);
                        //Corners_BOTTOM_BOTTOM_RIGHT(doc);
                        //Corners_TOP_BOTTOM_RIGHT(doc);
                    }
                    break;

                case PanelMoment_Type.TWO_LONG_EDGES_DISCONTINUOUS:
                    {

                        Edge_Strip_TOP(doc);
                        //Edge_Strip_RIGHT(doc);
                        Edge_Strip_BOTTOM(doc);
                        //Edge_Strip_LEFT(doc);

                        
                        long_corner_space = _long_corner_space * 2;
                        short_corner_space = short_corner_space * 2;

                        Corners_BOTTOM_TOP_LEFT(doc);
                        Corners_TOP_TOP_LEFT(doc);
                        Corners_BOTTOM_TOP_RIGHT(doc);
                        Corners_TOP_TOP_RIGHT(doc);
                        Corners_BOTTOM_BOTTOM_LEFT(doc);
                        Corners_TOP_BOTTOM_LEFT(doc);
                        Corners_BOTTOM_BOTTOM_RIGHT(doc);
                        Corners_TOP_BOTTOM_RIGHT(doc);

                    }
                    break;

                case PanelMoment_Type.TWO_SHORT_EDGES_DISCONTINUOUS:
                    {

                        //Edge_Strip_TOP(doc);
                        Edge_Strip_RIGHT(doc);
                        //Edge_Strip_BOTTOM(doc);
                        Edge_Strip_LEFT(doc);

                        
                        long_corner_space = _long_corner_space * 2;
                        short_corner_space = short_corner_space * 2;

                        Corners_BOTTOM_TOP_LEFT(doc);
                        Corners_TOP_TOP_LEFT(doc);
                        Corners_BOTTOM_TOP_RIGHT(doc);
                        Corners_TOP_TOP_RIGHT(doc);
                        Corners_BOTTOM_BOTTOM_LEFT(doc);
                        Corners_TOP_BOTTOM_LEFT(doc);
                        Corners_BOTTOM_BOTTOM_RIGHT(doc);
                        Corners_TOP_BOTTOM_RIGHT(doc);
                    }
                    break;
            }
            #endregion

            //Corners_BOTTOM_TOP_LEFT(doc);
            //Corners_TOP_TOP_LEFT(doc);
            //Corners_BOTTOM_TOP_RIGHT(doc);
            //Corners_TOP_TOP_RIGHT(doc);
            //Corners_BOTTOM_BOTTOM_LEFT(doc);
            //Corners_TOP_BOTTOM_LEFT(doc);
            //Corners_BOTTOM_BOTTOM_RIGHT(doc);
            //Corners_TOP_BOTTOM_RIGHT(doc);
            //Line_To_Circle(doc);
            doc.Redraw(true);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);

        }
      
        #region DRAW_VIEW
        public bool Read_View_Data_From_File(string fileName)
        {
          
            if (!File.Exists(fileName)) return false;

            List<string> lstContent = new List<string>(File.ReadAllLines(fileName));
            MyStrings mList = null;

            string kStr = "";
            for (int i = 0; i < lstContent.Count; i++)
            {
                kStr = lstContent[i].Replace(';', ' ').Trim().TrimEnd().TrimStart();
                mList = new MyStrings(MyStrings.RemoveAllSpaces(kStr), '=');
                kStr = mList.StringList[0].Trim().TrimEnd().TrimStart().ToLower();
               
                #region SWITCH CASE
                //SLAB04 VIEW
                //long_span = 5700;
                //short_span = 4300;
                //clear_cover = 25;
                //height = 1100;
                //start_long_dist = 700;
                //start_short_dist = 500;
                //long_space = 140;
                //short_space = 100;
                //middle_bar_dist = 110;
                //long_span_Corner = 840;
                //short_span_Corner = 900;
                //long_corner_space = 70;
                //short_corner_space = 100;
                //panel_moment_type = 1;
                //FINISH
                switch (kStr)
                {
                    case "long_span":
                        long_span = mList.GetDouble(1);
                        break;
                    case "short_span":
                        short_span = mList.GetDouble(1);
                        break;
                    case "clear_cover":
                        clear_cover = mList.GetDouble(1);
                        break;
                    case "height":
                        height = mList.GetDouble(1);
                        break;
                    case "start_long_dist":
                        start_long_dist = mList.GetDouble(1);
                        break;
                    case "start_short_dist":
                        start_short_dist = mList.GetDouble(1);
                        break;
                    case "long_space":
                        long_space = mList.GetDouble(1);
                        break;
                    case "short_space":
                        short_space = mList.GetDouble(1);
                        break;
                    case "middle_bar_dist":
                        middle_bar_dist = mList.GetDouble(1);
                        break;
                    case "long_span_corner":
                        long_span_Corner = mList.GetDouble(1);
                        break;
                    case "short_span_corner":
                        short_span_Corner = mList.GetDouble(1);
                        break;
                    case "long_corner_space":
                        long_corner_space = mList.GetDouble(1);
                        break;
                    case "short_corner_space":
                        short_corner_space = mList.GetDouble(1);
                        break;
                    case "panel_moment_type":
                        PanelMomentType = (PanelMoment_Type) mList.GetInt(1);
                        break;
                }
                #endregion
            }
            return true;
        }

        private void Along_Long_Span(vdDocument doc)
        {
            #region Along Long Span
            dz = short_space;
            dx = long_space;
            no_long_span_bar = (short_span / short_space);
            //no_short_span_bar = (long_span / long_space);

            no_bar = (int)no_long_span_bar;
            for (int i = 0; i <= no_bar; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(0, 0, (i * dz));
                line.EndPoint = new gPoint(start_long_dist, 0, (i * dz));
                line.Layer = bottom_lay;
                doc.ActiveLayOut.Entities.AddItem(line);

                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(0, 0, (i * dz));
                line.EndPoint = new gPoint(0, height, (i * dz));
                line.Layer = top_lay;
                doc.ActiveLayOut.Entities.AddItem(line);

                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(0, height, (i * dz));
                line.EndPoint = new gPoint(start_long_dist, height, (i * dz));
                line.Layer = top_lay;
                doc.ActiveLayOut.Entities.AddItem(line);
            }

            for (int i = 0; i <= no_bar; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(long_span - start_long_dist, 0, (i * dz));
                line.EndPoint = new gPoint(long_span, 0, (i * dz));
                line.Layer = bottom_lay;
                doc.ActiveLayOut.Entities.AddItem(line);

                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(long_span, 0, (i * dz));
                line.EndPoint = new gPoint(long_span, height, (i * dz));
                line.Layer = top_lay;
                doc.ActiveLayOut.Entities.AddItem(line);

                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(long_span - start_long_dist, height, (i * dz));
                line.EndPoint = new gPoint(long_span, height, (i * dz));
                line.Layer = top_lay;
                doc.ActiveLayOut.Entities.AddItem(line);
            }
            #endregion
        }
        
        private void Along_Short_Span(vdDocument doc)
        {
            #region Short Span

            //dx = long_space;
            dx = middle_bar_dist * 2;
            //no_short_span_bar = (long_span / long_space);

            no_short_span_bar = ((long_span - 2 * start_short_dist) / dx);
            no_bar = (int)no_short_span_bar;


            for (int i = 0; i < no_bar; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_long_dist + (i * dx), 0, 0);
                line.EndPoint = new gPoint(start_long_dist + (i * dx), 0, short_span);
                line.Layer = bottom_lay;
                doc.ActiveLayOut.Entities.AddItem(line);

                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_long_dist + (i * dx), 0, 0);
                line.EndPoint = new gPoint(start_long_dist + (i * dx), height, 0);
                line.Layer = top_lay;
                doc.ActiveLayOut.Entities.AddItem(line);



                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_long_dist + (i * dx), height, 0);
                line.EndPoint = new gPoint(start_long_dist + (i * dx), height, start_short_dist);
                line.Layer = top_lay;
                doc.ActiveLayOut.Entities.AddItem(line);

                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_long_dist + (i * dx), 0, short_span);
                line.EndPoint = new gPoint(start_long_dist + (i * dx), height, short_span);
                line.Layer = top_lay;
                doc.ActiveLayOut.Entities.AddItem(line);


                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_long_dist + (i * dx), height, short_span);
                line.EndPoint = new gPoint(start_long_dist + (i * dx), height, short_span - start_short_dist);
                line.Layer = top_lay;
                doc.ActiveLayOut.Entities.AddItem(line);
            }
            #endregion
        }

        private void Middle_Along_Long_Span(vdDocument doc)
        {
            #region Middle Along Long span
            no_long_span_bar = ((short_span - (2 * start_short_dist)) / short_space);
            no_bar = (int)no_long_span_bar;
            dz = short_space;
            for (int i = 0; i <= no_bar; i++)
            {

                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_long_dist, 0, start_short_dist + (i * dz));
                line.EndPoint = new gPoint(long_span - start_long_dist, 0, start_short_dist + (i * dz));
                line.Layer = bottom_lay;
                doc.ActiveLayOut.Entities.AddItem(line);
            }

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(start_long_dist, 0, short_span - start_short_dist);
            line.EndPoint = new gPoint(long_span - start_long_dist, 0, short_span - start_short_dist);
            line.Layer = bottom_lay;
            doc.ActiveLayOut.Entities.AddItem(line);

            #endregion
        }
        private void Middle_Along_Short_Span(vdDocument doc)
        {
            #region Middle Along Short span
            dx = middle_bar_dist;

            no_short_span_bar = ((long_span - (2 * start_long_dist)) / dx);
            no_bar = (int)no_short_span_bar;

            for (int i = 0; i <= no_bar; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(start_long_dist + (i * dx), 0, start_short_dist);
                line.EndPoint = new gPoint(start_long_dist + (i * dx), 0, short_span - start_short_dist);
                line.PenColor = new vdColor(Color.Blue);
                line.Layer = bottom_lay;
                doc.ActiveLayOut.Entities.AddItem(line);
            }


            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(long_span - start_long_dist, 0, start_short_dist);
            line.EndPoint = new gPoint(long_span - start_long_dist, 0, short_span - start_short_dist);
            line.Layer = bottom_lay;
            doc.ActiveLayOut.Entities.AddItem(line);
            #endregion
        }

        private void Edge_Strip_TOP(vdDocument doc)
        {
            #region EDGE TOP
            no_long_span_bar = start_short_dist / 100.0;
            no_bar = (int)no_long_span_bar;
            dz = 100;
            for (int i = 0; i <= no_bar; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(0, 0, (i * dz));
                line.EndPoint = new gPoint(long_span, 0, (i * dz));
                line.Layer = edge_strip_lay;
                doc.ActiveLayOut.Entities.AddItem(line);
            }
            #endregion
        }
        private void Edge_Strip_BOTTOM(vdDocument doc)
        {
            #region EDGE BOTTOM

            no_long_span_bar = start_short_dist / 100.0;
            no_bar = (int)no_long_span_bar;
            dz = 100;
            for (int i = 0; i <= no_bar; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(0, 0, short_span - (i * dz));
                line.EndPoint = new gPoint(long_span, 0, short_span - (i * dz));
                line.Layer = edge_strip_lay;
                doc.ActiveLayOut.Entities.AddItem(line);
            }
            #endregion

        }
        private void Edge_Strip_LEFT(vdDocument doc)
        {
            #region EDGE LEFT

            dx = long_space;
            no_short_span_bar = start_long_dist / dx;
            no_bar = (int)no_short_span_bar;

            for (int i = 0; i <= no_bar; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint((i * dx), 0, 0);
                line.EndPoint = new gPoint((i * dx), 0, short_span);
                line.Layer = edge_strip_lay;
                doc.ActiveLayOut.Entities.AddItem(line);
            }
            #endregion

        }
        private void Edge_Strip_RIGHT(vdDocument doc)
        {
            #region EDGE RIGHT
            dx = long_space;
            no_short_span_bar = start_long_dist / dx;
            no_bar = (int)no_short_span_bar;

            for (int i = 0; i <= no_bar; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(long_span - (i * dx), 0, 0);
                line.EndPoint = new gPoint(long_span - (i * dx), 0, short_span);
                line.Layer = edge_strip_lay;
                doc.ActiveLayOut.Entities.AddItem(line);
            }

            #endregion

        }

        private void Edge_Strip(vdDocument doc)
        {
            #region EDGE TOP

            no_long_span_bar = start_short_dist / 100.0;
            no_bar = (int)no_long_span_bar;
            dz = 100;
            for (int i = 0; i <= no_bar; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(0, 0, (i * dz));
                line.EndPoint = new gPoint(long_span, 0, (i * dz));
                line.Layer = edge_strip_lay;
                doc.ActiveLayOut.Entities.AddItem(line);
            }
            #endregion

            #region EDGE LEFT

            dx = long_space;

            no_short_span_bar = start_long_dist / dx;

            no_bar = (int)no_short_span_bar;

            for (int i = 0; i <= no_bar; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint((i * dx), 0, 0);
                line.EndPoint = new gPoint((i * dx), 0, short_span);
                line.Layer = edge_strip_lay;
                doc.ActiveLayOut.Entities.AddItem(line);
            }
            #endregion
            #region EDGE LEFT

            dx = long_space;

            no_short_span_bar = start_long_dist / dx;

            no_bar = (int)no_short_span_bar;

            for (int i = 0; i <= no_bar; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint((i * dx), 0, 0);
                line.EndPoint = new gPoint((i * dx), 0, short_span);
                line.Layer = edge_strip_lay;
                doc.ActiveLayOut.Entities.AddItem(line);
            }
            #endregion
            #region EDGE RIGHT

            for (int i = 0; i <= no_bar; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(long_span - (i * dx), 0, 0);
                line.EndPoint = new gPoint(long_span - (i * dx), 0, short_span);
                line.Layer = edge_strip_lay;
                doc.ActiveLayOut.Entities.AddItem(line);
            }

            #endregion
        }
        
        private void Corners_BOTTOM_TOP_LEFT(vdDocument doc)
        {
            #region Corners BOTTOM UP LEFT

            no_long_span_bar = short_span_Corner / short_corner_space;
            no_bar = (int)no_long_span_bar;

            dz = short_corner_space;
            vdLine line = new vdLine();

            for (int i = 0; i < no_bar; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(0, 0, (i * dz));
                line.EndPoint = new gPoint(long_span_Corner, 0, (i * dz));
                line.Layer = bottom_corner_lay;
                doc.ActiveLayOut.Entities.AddItem(line);
            }

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(0, 0, short_span_Corner);
            line.EndPoint = new gPoint(long_span_Corner, 0, short_span_Corner);
            line.Layer = bottom_corner_lay;
            doc.ActiveLayOut.Entities.AddItem(line);



            dx = long_corner_space;
            no_short_span_bar = (long_span_Corner / dx);
            no_bar = (int)no_short_span_bar;
            for (int i = 0; i < no_bar; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint((i * dx), 0, 0);
                line.EndPoint = new gPoint((i * dx), 0, short_span_Corner);
                line.Layer = bottom_corner_lay;
                doc.ActiveLayOut.Entities.AddItem(line);
            }

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(long_span_Corner, 0, 0);
            line.EndPoint = new gPoint(long_span_Corner, 0, short_span_Corner);
            line.Layer = bottom_corner_lay;
            doc.ActiveLayOut.Entities.AddItem(line);


            #endregion

        }
        private void Corners_TOP_TOP_LEFT(vdDocument doc)
        {
            #region Corners BOTTOM DOWN LEFT

            no_long_span_bar = short_span_Corner / short_corner_space;
            no_bar = (int)no_long_span_bar;

            dz = short_corner_space;


            for (int i = 0; i < no_bar; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(0, height, (i * dz));
                line.EndPoint = new gPoint(long_span_Corner, height, (i * dz));
                line.Layer = top_corner_lay;

                doc.ActiveLayOut.Entities.AddItem(line);
            }

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(0, height, short_span_Corner);
            line.EndPoint = new gPoint(long_span_Corner, height, short_span_Corner);
            line.Layer = top_corner_lay;
            doc.ActiveLayOut.Entities.AddItem(line);



            dx = long_corner_space;
            no_short_span_bar = (long_span_Corner / dx);
            no_bar = (int)no_short_span_bar;
            for (int i = 0; i < no_bar; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint((i * dx), height, 0);
                line.EndPoint = new gPoint((i * dx), height, short_span_Corner);
                line.Layer = top_corner_lay;
                doc.ActiveLayOut.Entities.AddItem(line);
            }

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(long_span_Corner, height, 0);
            line.EndPoint = new gPoint(long_span_Corner, height, short_span_Corner);
            line.Layer = top_corner_lay;
            doc.ActiveLayOut.Entities.AddItem(line);


            #endregion
        }

        private void Corners_BOTTOM_TOP_RIGHT(vdDocument doc)
        {
            #region Corners BOTTOM UP RIGHT

            no_long_span_bar = short_span_Corner / short_corner_space;
            no_bar = (int)no_long_span_bar;

            dz = short_corner_space;

            for (int i = 0; i < no_bar; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(long_span, 0, (i * dz));
                line.EndPoint = new gPoint(long_span - long_span_Corner, 0, (i * dz));
                line.Layer = bottom_corner_lay;
                doc.ActiveLayOut.Entities.AddItem(line);
            }

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(long_span, 0, short_span_Corner);
            line.EndPoint = new gPoint(long_span - long_span_Corner, 0, short_span_Corner);
            line.Layer = bottom_corner_lay;
            doc.ActiveLayOut.Entities.AddItem(line);



            dx = long_corner_space;
            no_short_span_bar = (long_span_Corner / dx);
            no_bar = (int)no_short_span_bar;
            for (int i = 0; i < no_bar; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(long_span - (i * dx), 0, 0);
                line.EndPoint = new gPoint(long_span - (i * dx), 0, short_span_Corner);
                line.Layer = bottom_corner_lay;
                doc.ActiveLayOut.Entities.AddItem(line);
            }

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(long_span - long_span_Corner, 0, 0);
            line.EndPoint = new gPoint(long_span - long_span_Corner, 0, short_span_Corner);
            line.Layer = bottom_corner_lay;
            doc.ActiveLayOut.Entities.AddItem(line);


            #endregion
        }
        private void Corners_TOP_TOP_RIGHT(vdDocument doc)
        {
            #region Corners BOTTOM DOWN RIGHT

            no_long_span_bar = short_span_Corner / short_corner_space;
            no_bar = (int)no_long_span_bar;

            dz = short_corner_space;

            for (int i = 0; i < no_bar; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(long_span, height, (i * dz));
                line.EndPoint = new gPoint(long_span - long_span_Corner, height, (i * dz));
                line.Layer = top_corner_lay;
                doc.ActiveLayOut.Entities.AddItem(line);
            }

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(long_span, height, short_span_Corner);
            line.EndPoint = new gPoint(long_span - long_span_Corner, height, short_span_Corner);
            line.Layer = top_corner_lay;
            doc.ActiveLayOut.Entities.AddItem(line);



            dx = long_corner_space;
            no_short_span_bar = (long_span_Corner / dx);
            no_bar = (int)no_short_span_bar;
            for (int i = 0; i < no_bar; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(long_span - (i * dx), height, 0);
                line.EndPoint = new gPoint(long_span - (i * dx), height, short_span_Corner);
                line.Layer = top_corner_lay;
                doc.ActiveLayOut.Entities.AddItem(line);
            }

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(long_span - long_span_Corner, height, 0);
            line.EndPoint = new gPoint(long_span - long_span_Corner, height, short_span_Corner);
            line.Layer = top_corner_lay;
            doc.ActiveLayOut.Entities.AddItem(line);


            #endregion

        }

        private void Corners_BOTTOM_BOTTOM_LEFT(vdDocument doc)
        {
            #region Corners BOTTOM UP RIGHT

            no_long_span_bar = short_span_Corner / short_corner_space;
            no_bar = (int)no_long_span_bar;

            dz = short_corner_space;

            for (int i = 0; i < no_bar; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(0, 0, short_span - (i * dz));
                line.EndPoint = new gPoint(long_span_Corner, 0, short_span - (i * dz));
                line.Layer = bottom_corner_lay;
                doc.ActiveLayOut.Entities.AddItem(line);
            }

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(0, 0, short_span - short_span_Corner);
            line.EndPoint = new gPoint(long_span_Corner, 0, short_span - short_span_Corner);
            line.Layer = bottom_corner_lay;
            doc.ActiveLayOut.Entities.AddItem(line);



            dx = long_corner_space;
            no_short_span_bar = (long_span_Corner / dx);
            no_bar = (int)no_short_span_bar;
            for (int i = 0; i < no_bar; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint((i * dx), 0, short_span);
                line.EndPoint = new gPoint((i * dx), 0, short_span - short_span_Corner);
                line.Layer = bottom_corner_lay;
                doc.ActiveLayOut.Entities.AddItem(line);
            }

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(long_span_Corner, 0, short_span);
            line.EndPoint = new gPoint(long_span_Corner, 0, short_span - short_span_Corner);
            line.Layer = bottom_corner_lay;
            doc.ActiveLayOut.Entities.AddItem(line);


            #endregion
        }
        private void Corners_TOP_BOTTOM_LEFT(vdDocument doc)
        {
            #region Corners BOTTOM DOWN RIGHT

            no_long_span_bar = short_span_Corner / short_corner_space;
            no_bar = (int)no_long_span_bar;

            dz = short_corner_space;

            for (int i = 0; i < no_bar; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(0, height, short_span - (i * dz));
                line.EndPoint = new gPoint(long_span_Corner, height, short_span - (i * dz));
                line.Layer = top_corner_lay;
                doc.ActiveLayOut.Entities.AddItem(line);
            }

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(0, height, short_span - short_span_Corner);
            line.EndPoint = new gPoint(long_span_Corner, height, short_span - short_span_Corner);
            line.Layer = top_corner_lay;
            doc.ActiveLayOut.Entities.AddItem(line);



            dx = long_corner_space;
            no_short_span_bar = (long_span_Corner / dx);
            no_bar = (int)no_short_span_bar;
            for (int i = 0; i < no_bar; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint((i * dx), height, short_span);
                line.EndPoint = new gPoint((i * dx), height, short_span - short_span_Corner);
                line.Layer = top_corner_lay;
                doc.ActiveLayOut.Entities.AddItem(line);
            }

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(long_span_Corner, height, short_span);
            line.EndPoint = new gPoint(long_span_Corner, height, short_span - short_span_Corner);
            line.Layer = top_corner_lay;
            doc.ActiveLayOut.Entities.AddItem(line);


            #endregion

        }

        private void Corners_BOTTOM_BOTTOM_RIGHT(vdDocument doc)
        {
            #region Corners BOTTOM UP RIGHT

            no_long_span_bar = short_span_Corner / short_corner_space;
            no_bar = (int)no_long_span_bar;

            dz = short_corner_space;

            for (int i = 0; i < no_bar; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(long_span, 0, short_span - (i * dz));
                line.EndPoint = new gPoint(long_span - long_span_Corner, 0, short_span - (i * dz));
                line.Layer = bottom_corner_lay;
                doc.ActiveLayOut.Entities.AddItem(line);
            }

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(long_span, 0, short_span - short_span_Corner);
            line.EndPoint = new gPoint(long_span - long_span_Corner, 0, short_span - short_span_Corner);
            line.Layer = bottom_corner_lay;
            doc.ActiveLayOut.Entities.AddItem(line);



            dx = long_corner_space;
            no_short_span_bar = (long_span_Corner / dx);
            no_bar = (int)no_short_span_bar;
            for (int i = 0; i < no_bar; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(long_span - (i * dx), 0, short_span);
                line.EndPoint = new gPoint(long_span - (i * dx), 0, short_span - short_span_Corner);
                line.Layer = bottom_corner_lay;
                doc.ActiveLayOut.Entities.AddItem(line);
            }

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(long_span - long_span_Corner, 0, short_span);
            line.EndPoint = new gPoint(long_span - long_span_Corner, 0, short_span - short_span_Corner);
            line.Layer = bottom_corner_lay;
            doc.ActiveLayOut.Entities.AddItem(line);


            #endregion

        }
        private void Corners_TOP_BOTTOM_RIGHT(vdDocument doc)
        {
            #region Corners BOTTOM DOWN RIGHT

            no_long_span_bar = short_span_Corner / short_corner_space;
            no_bar = (int)no_long_span_bar;

            dz = short_corner_space;

            for (int i = 0; i < no_bar; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(long_span, height, short_span - (i * dz));
                line.EndPoint = new gPoint(long_span - long_span_Corner, height, short_span - (i * dz));
                line.Layer = top_corner_lay;
                doc.ActiveLayOut.Entities.AddItem(line);
            }

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(long_span, height, short_span - short_span_Corner);
            line.EndPoint = new gPoint(long_span - long_span_Corner, height, short_span - short_span_Corner);
            line.Layer = top_corner_lay;
            doc.ActiveLayOut.Entities.AddItem(line);



            dx = long_corner_space;
            no_short_span_bar = (long_span_Corner / dx);
            no_bar = (int)no_short_span_bar;
            for (int i = 0; i < no_bar; i++)
            {
                line = new vdLine();
                line.SetUnRegisterDocument(doc);
                line.setDocumentDefaults();
                line.StartPoint = new gPoint(long_span - (i * dx), height, short_span);
                line.EndPoint = new gPoint(long_span - (i * dx), height, short_span - short_span_Corner);
                line.Layer = top_corner_lay;
                doc.ActiveLayOut.Entities.AddItem(line);
            }

            line = new vdLine();
            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            line.StartPoint = new gPoint(long_span - long_span_Corner, height, short_span);
            line.EndPoint = new gPoint(long_span - long_span_Corner, height, short_span - short_span_Corner);
            line.Layer = top_corner_lay;
            doc.ActiveLayOut.Entities.AddItem(line);


            #endregion

        }
        #endregion

        public void Line_To_Circle(vdDocument doc)
        {
            vdCircle cir = new vdCircle();
            for (int i = 0; i < doc.ActiveLayOut.Entities.Count; i++)
            {
                vdLine ln = doc.ActiveLayOut.Entities[i] as vdLine;
                if (ln != null)
                {
                    cir = new vdCircle();
                    cir.SetUnRegisterDocument(doc);
                    cir.setDocumentDefaults();
                    cir.Center = ln.StartPoint;
                    cir.Thickness = ln.Length();
                    cir.ExtrusionVector = Vector.CreateExtrusion(ln.StartPoint, ln.EndPoint);
                    cir.PenColor = ln.PenColor;
                    cir.Radius = (ln.PenColor == (new vdColor(Color.Blue))) ? 10.0d : 8.0d;
                    doc.ActiveLayOut.Entities.AddItem(cir);
                }
            }
            
        }

        public enum PanelMoment_Type
        {
            INTERIOR_PANELS = 1,
            ONE_SHORT_EDGE_DISCONTINUOUS = 2,
            ONE_LONG_EDGE_DISCONTINUOUS = 3,
            TWO_ADJACENT_EDGES_DISCONTINUOUS = 4,
            TWO_SHORT_EDGES_DISCONTINUOUS = 5,
            TWO_LONG_EDGES_DISCONTINUOUS = 6,
            THREE_EDGES_DISCONTINUOUS_ONE_LONG_EDGE_CONTINUOUS = 7,
            THREE_EDGES_DISCONTINUOUS_ONE_SHORT_EDGE_CONTINUOUS = 8,
            FOUR_EDGES_DISCONTINUOUS = 9,
        }
    }
}
