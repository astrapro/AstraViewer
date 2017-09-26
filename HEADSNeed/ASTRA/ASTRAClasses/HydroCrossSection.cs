using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;

using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
namespace HEADSNeed.ASTRA.ASTRAClasses
{
    public class HydroCrossSection
    {
        public HydroCrossSection(string data_file_name)
        {
            //data_file = data_file_name;
            //list_hydro_coll = new List<HydroData>();
        }
        public static void DrawHrdrographFromDataFile(string data_file, vdDocument vdDoc)
        {
            List<HydroData> list_hydro_coll = new List<HydroData>();
            MyStrings mlist = null;
            List<string> file_content = new List<string>(File.ReadAllLines(data_file));
            string kStr = "";
            HydroData hdr = null;
            vdMText mtext = null;
            vdPolyline pl = null;
            vdCircle cir = null;
            vdRect rect = null;

            double min_x, min_y;
            double max_x, max_y;


            min_x = double.MaxValue;
            min_y = min_x;
            max_x = double.MinValue;
            max_y = max_x;
            bool flag = false;
            double min_foundation_Level = 0.0, Max_Scour_Depth = 0.0;
            foreach (var item in file_content)
            {
                kStr = item.Replace(",", " ");
                kStr = item.Replace("=", " ");
                kStr = MyStrings.RemoveAllSpaces(kStr.ToUpper());
                mlist = new MyStrings(kStr, ' ');

                if (kStr.ToUpper().Contains("DISTANCE")) flag = true;
                if (mlist.StringList[0] == "MIN_FOUNDATION_LEVEL")
                {
                    min_foundation_Level = mlist.GetDouble(1);
                }
                if (mlist.StringList[0] == "MAXIMUM_SCOUR_DEPTH")
                {
                    Max_Scour_Depth = mlist.GetDouble(1);
                }

                //sw.WriteLine("MIN_FOUNDATION_LEVEL = {0}", min_foundation_Level);
                //sw.WriteLine("MAXIMUM_SCOUR_DEPTH = {0}", Max_Scour_Depth);

                if (flag == false) continue;
                try
                {
                    hdr = new HydroData();
                    hdr.SerialNo = list_hydro_coll.Count + 1;

                    
                    hdr.Distance = mlist.GetDouble(0, -999.0);
                    if (hdr.Distance == -999.0)
                        throw (new Exception());

                    hdr.WaterLevel = mlist.GetDouble(1);



                    list_hydro_coll.Add(hdr);

                    if (min_x > hdr.Distance)
                        min_x = hdr.Distance;
                    if (min_y > hdr.WaterLevel)
                        min_y = hdr.WaterLevel;

                    if (max_x < hdr.Distance)
                        max_x = hdr.Distance;
                    if (max_y < hdr.WaterLevel)
                        max_y = hdr.WaterLevel;

                }
                catch (Exception ex) { }
            }

            pl = new vdPolyline();
            pl.SetUnRegisterDocument(vdDoc);
            pl.setDocumentDefaults();


           
            foreach (var item in list_hydro_coll)
            {
                cir = new vdCircle();
                cir.SetUnRegisterDocument(vdDoc);
                cir.setDocumentDefaults();
                cir.Center = new VectorDraw.Geometry.gPoint(item.Distance, item.WaterLevel);
                cir.Radius = 0.09;
                cir.PenColor = new vdColor(Color.GreenYellow);
                vdDoc.ActiveLayOut.Entities.AddItem(cir);

                pl.VertexList.Add(cir.Center);

            }
            pl.PenColor = new vdColor(Color.Red);
            vdDoc.ActiveLayOut.Entities.AddItem(pl);


            vdLine ln_min_found = new vdLine();
            ln_min_found.SetUnRegisterDocument(vdDoc);
            ln_min_found.setDocumentDefaults();
            ln_min_found.StartPoint.x = pl.BoundingBox.Left;
            ln_min_found.StartPoint.y = pl.BoundingBox.Bottom - min_foundation_Level;

            ln_min_found.EndPoint.x = pl.BoundingBox.Right;
            ln_min_found.EndPoint.y = pl.BoundingBox.Bottom - min_foundation_Level;
            vdDoc.ActiveLayOut.Entities.AddItem(ln_min_found);


            mtext = new vdMText();
            mtext.SetUnRegisterDocument(vdDoc);
            mtext.setDocumentDefaults();
            mtext.InsertionPoint.x = ln_min_found.StartPoint.x;
            mtext.InsertionPoint.y = ln_min_found.StartPoint.y-0.2;
            mtext.Height = 0.3;
            //mtext.Rotation = 90 * Math.PI / 180.0;

            mtext.TextString = "Depth from LBL to minimum Foundation Level = " + min_foundation_Level.ToString("0.0000");
            mtext.PenColor = new vdColor(Color.Cyan);
            vdDoc.ActiveLayOut.Entities.AddItem(mtext);




            vdLine ln_max_scour = new vdLine();
            ln_max_scour.SetUnRegisterDocument(vdDoc);
            ln_max_scour.setDocumentDefaults();
            ln_max_scour.StartPoint.x = pl.BoundingBox.Left;
            ln_max_scour.StartPoint.y = pl.BoundingBox.Bottom - Max_Scour_Depth;

            ln_max_scour.EndPoint.x = pl.BoundingBox.Right;
            ln_max_scour.EndPoint.y = pl.BoundingBox.Bottom - Max_Scour_Depth;
            vdDoc.ActiveLayOut.Entities.AddItem(ln_max_scour);



            mtext = new vdMText();
            mtext.SetUnRegisterDocument(vdDoc);
            mtext.setDocumentDefaults();
            mtext.InsertionPoint = ln_max_scour.StartPoint;
            mtext.InsertionPoint.y -= 0.2 ;
            mtext.Height = 0.3;
            //mtext.Rotation = 90 * Math.PI / 180.0;

            mtext.TextString = "Maximum Scour Depth obtained = " + Max_Scour_Depth.ToString("0.0000");
            mtext.PenColor = new vdColor(Color.Cyan);
            vdDoc.ActiveLayOut.Entities.AddItem(mtext);

            //Maximum Scour Depth adopted = 

            //Depth from LBL to minimum Foundation Level


            //vdRect rect = null;
            rect = new vdRect();
            rect.SetUnRegisterDocument(vdDoc);
            rect.setDocumentDefaults();

            rect.InsertionPoint.x = pl.BoundingBox.Left-2;
            if (min_foundation_Level > Max_Scour_Depth)
                rect.InsertionPoint.y = pl.BoundingBox.Bottom - 2 - min_foundation_Level;
            else
                rect.InsertionPoint.y = pl.BoundingBox.Bottom - 2 - Max_Scour_Depth;

            rect.Width = pl.BoundingBox.Width+4;
            //rect.Height = -pl.BoundingBox.Height;
            rect.Height = -2;

            vdDoc.ActiveLayOut.Entities.AddItem(rect);


            vdLine ln = new vdLine();
            ln.SetUnRegisterDocument(vdDoc);
            ln.setDocumentDefaults();
            ln.StartPoint.x = rect.BoundingBox.Left;
            ln.StartPoint.y = rect.BoundingBox.Bottom + 1;
            ln.EndPoint.x = rect.BoundingBox.Right;
            ln.EndPoint.y = rect.BoundingBox.Bottom + 1;

            vdDoc.ActiveLayOut.Entities.AddItem(ln);

            foreach (var item in list_hydro_coll)
            {
                mtext = new vdMText();
                mtext.SetUnRegisterDocument(vdDoc);
                mtext.setDocumentDefaults();
                mtext.InsertionPoint.x = item.Distance;
                mtext.InsertionPoint.y = ln.BoundingBox.Bottom + 0.2;
                mtext.Height = 0.1;
                mtext.Rotation = 90 * Math.PI / 180.0;

                mtext.TextString = item.WaterLevel.ToString("0.0000");
                mtext.PenColor = new vdColor(Color.Cyan);
                vdDoc.ActiveLayOut.Entities.AddItem(mtext);


                mtext = new vdMText();
                mtext.SetUnRegisterDocument(vdDoc);
                mtext.setDocumentDefaults();
                mtext.InsertionPoint.x = item.Distance;
                mtext.InsertionPoint.y = rect.BoundingBox.Bottom + 0.2;
                mtext.Height = 0.1;
                mtext.Rotation = 90 * Math.PI / 180.0;

                mtext.TextString = item.Distance.ToString("0.0000");
                mtext.PenColor = new vdColor(Color.LightGreen);
                vdDoc.ActiveLayOut.Entities.AddItem(mtext);

            }


            mtext = new vdMText();
            mtext.SetUnRegisterDocument(vdDoc);
            mtext.setDocumentDefaults();
            mtext.InsertionPoint.x = rect.BoundingBox.Left + 0.2;
            mtext.InsertionPoint.y = rect.BoundingBox.Top + 0.2;
            mtext.Height = 0.11;
            //mtext.Rotation = 90 * Math.PI / 180.0;

            mtext.TextString = "DATUM = " + rect.BoundingBox.Top.ToString("0.00");
            vdDoc.ActiveLayOut.Entities.AddItem(mtext);

            mtext = new vdMText();
            mtext.SetUnRegisterDocument(vdDoc);
            mtext.setDocumentDefaults();
            mtext.InsertionPoint.x = rect.BoundingBox.Left + 0.2;
            mtext.InsertionPoint.y = ln.BoundingBox.Bottom + 0.5;
            mtext.Height = 0.1;
            //mtext.Rotation = 90 * Math.PI / 180.0;

            mtext.TextString = "RIVER BED LEVEL (m)";
            mtext.PenColor = new vdColor(Color.Cyan);
            vdDoc.ActiveLayOut.Entities.AddItem(mtext);


            mtext = new vdMText();
            mtext.SetUnRegisterDocument(vdDoc);
            mtext.setDocumentDefaults();
            mtext.InsertionPoint.x = rect.BoundingBox.Left + 0.2;
            mtext.InsertionPoint.y = rect.BoundingBox.Bottom + 0.5;
            mtext.Height = 0.1;
            //mtext.Rotation = 90 * Math.PI / 180.0;

            mtext.TextString = "DISTANCE (m)";
            mtext.PenColor = new vdColor(Color.LightGreen);
            vdDoc.ActiveLayOut.Entities.AddItem(mtext);


            ln = new vdLine();
            ln.SetUnRegisterDocument(vdDoc);
            ln.setDocumentDefaults();
            ln.StartPoint.x = rect.BoundingBox.Left + 1.8;
            ln.StartPoint.y = rect.BoundingBox.Bottom;
            ln.EndPoint.x = rect.BoundingBox.Left + 1.8;
            ln.EndPoint.y = rect.BoundingBox.Top;


            vdDoc.ActiveLayOut.Entities.AddItem(ln);

            vdRect over_box = new vdRect();
            over_box.SetUnRegisterDocument(vdDoc);
            over_box.setDocumentDefaults();

            over_box.InsertionPoint.x = rect.BoundingBox.Left - 2;
            over_box.InsertionPoint.y = rect.BoundingBox.Bottom - 1;

            over_box.Width = rect.BoundingBox.Width + 4;
            over_box.Height = (pl.BoundingBox.Top - rect.BoundingBox.Bottom) + 2;

            vdDoc.ActiveLayOut.Entities.AddItem(over_box);

            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(vdDoc);
            vdDoc.Redraw(true);
        }

    }
    public class HydroData
    {
        public HydroData()
        {
            SerialNo =-1;
            Distance  = 0.0;
            WaterLevel  = 0.0;
        }
        public int SerialNo { get; set; }
        public double Distance { get; set; }
        public double WaterLevel { get; set; }
    }
}
