using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;


using VectorDraw.Geometry;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Generics;
namespace HEADSNeed.ASTRA.ASTRAClasses.CodeSnap
{
    public class CopyCoordinates
    {

        public CopyCoordinates()
        {
        }
        public static bool ReadLineCoordinates(string fileName, vdDocument doc)
        {
            //vdArray<vdLine> line_arr = new vdArray<vdLine>();
            List<string> lstStr = new List<string>(File.ReadAllLines(fileName));
            string kStr = "";
            string option = "";
            string SP, EP, TEXT, IP, Height;
            SP = EP = IP = TEXT = Height = "";

            MyStrings mList = null;

            for (int i = 0; i < lstStr.Count; i++)
            {
                kStr = lstStr[i].Trim().TrimEnd().TrimStart();
                if (kStr == "VD_LINE")
                {
                    option = "VD_LINE"; i++;
                    SP = lstStr[i].Replace("SP=", ""); i++;
                    EP = lstStr[i].Replace("EP=", "");
                }
                else if (kStr == "VD_TEXT")
                {
                    option = "VD_TEXT"; i++;
                    IP = lstStr[i].Replace("IP=", ""); i++;
                    TEXT = lstStr[i].Replace("TEXT=", ""); i++;
                    Height = lstStr[i].Replace("HEIGHT=", "");
                }
                switch (option)
                {
                    case "VD_LINE":
                        mList = new MyStrings(MyStrings.RemoveAllSpaces(SP), ',');
                        vdLine ln = new vdLine();
                        ln.SetUnRegisterDocument(doc);
                        ln.setDocumentDefaults();
                        ln.StartPoint.x = mList.GetDouble(0);
                        ln.StartPoint.y = mList.GetDouble(1);
                        ln.StartPoint.z = mList.GetDouble(2);
                        mList = new MyStrings(MyStrings.RemoveAllSpaces(EP), ',');
                        ln.EndPoint.x = mList.GetDouble(0);
                        ln.EndPoint.y = mList.GetDouble(1);
                        ln.EndPoint.z = mList.GetDouble(2);
                        doc.ActiveLayOut.Entities.AddItem(ln);

                        break;
                    case "VD_TEXT":

                        mList = new MyStrings(MyStrings.RemoveAllSpaces(IP), ',');
                        vdText txt = new vdText();
                        txt.SetUnRegisterDocument(doc);
                        txt.setDocumentDefaults();
                        txt.InsertionPoint.x = mList.GetDouble(0);
                        txt.InsertionPoint.y = mList.GetDouble(1);
                        txt.InsertionPoint.z = mList.GetDouble(2);
                        txt.TextString = TEXT;
                        txt.Height = MyStrings.StringToDouble(Height, 0.01);
                        doc.ActiveLayOut.Entities.AddItem(txt);
                        break;
                }
            }
            lstStr.Clear();
            //doc.ShowUCSAxis = false;
            //VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);
            //doc.Redraw(true);
            return true;

        }
        public static bool WriteLineCoordinates(string fileName, vdDocument doc)
        {
            bool success = false;
            using (StreamWriter sw = new StreamWriter(new FileStream(fileName, FileMode.Create)))
            {
                try
                {
                    for (int i = 0; i < doc.ActiveLayOut.Entities.Count; i++)
                    {
                        vdLine ln = doc.ActiveLayOut.Entities[i] as vdLine;
                        if (ln != null)
                        {
                            sw.WriteLine("VD_LINE");
                            sw.WriteLine("SP={0}", ln.StartPoint.ToString());
                            sw.WriteLine("EP={0}", ln.EndPoint.ToString());
                        }
                        else
                        {
                            vdText txt = doc.ActiveLayOut.Entities[i] as vdText;
                            if (txt != null)
                            {
                                sw.WriteLine("VD_TEXT");
                                sw.WriteLine("IP={0}", txt.InsertionPoint.ToString());
                                sw.WriteLine("TEXT={0}", txt.TextString);
                                sw.WriteLine("HEIGHT={0}", txt.Height);
                            }
                        }
                    }
                    success = true;
                }
                catch (Exception ex) { success = false; }
                finally
                {
                    sw.Flush();
                    sw.Close();
                }
            }
            return success;
        }
        public static bool Draw_Boq_Text_Slab03(string file_name, vdDocument doc)
        {
            //file_name = Path.Combine(Application.StartupPath, "slab03.s03");
            SLAB03_BOQ sl_boq = new SLAB03_BOQ();
            sl_boq.ReadFromFile(file_name);
            sl_boq.Draw_Boq(doc);
            doc.ShowUCSAxis = false;
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);
            doc.Redraw(true);
            return true;
        }
        public static bool Draw_Boq_Text_Slab04(string file_name, vdDocument doc)
        {
            //file_name = Path.Combine(Application.StartupPath, "slab04.s04");
            SLAB04_BOQ sl_boq = new SLAB04_BOQ();
            sl_boq.ReadFromFile(file_name);
            sl_boq.Draw_Boq(doc);
            doc.ShowUCSAxis = false;
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);
            doc.Redraw(true);
            return true;
        }
        public static bool Draw_Box_Culvert(string file_name, vdDocument doc)
        {
            List<string> lst_content = new List<string>(File.ReadAllLines(file_name));
            MyStrings mList = null;
            Hashtable h_table = new Hashtable();
            string kStr = "";
            int i = 0;
            for (i = 0; i < lst_content.Count; i++)
            {
                kStr = lst_content[i].Trim().TrimEnd().TrimStart();
                mList = new MyStrings(kStr, '=');
                if (mList.Count == 2)
                    h_table.Add(mList.StringList[0], mList.StringList[1]);
            }

            for (i = 0; i < doc.ActiveLayOut.Entities.Count; i++)
            {
                vdText txt = doc.ActiveLayOut.Entities[i] as vdText;
                if (txt != null)
                {
                    if (h_table.Contains(txt.TextString))
                    {

                        txt.TextString = h_table[txt.TextString].ToString();
                        if (txt.TextString == "0")
                            txt.TextString = "-";
                    }
                }
            }
            doc.Palette.Background = Color.White;
            doc.Redraw(true);
            h_table.Clear();
            lst_content.Clear();
            return true;
        }

        public static bool Draw_Slab_Culvert(string file_name, vdDocument doc)
        {
            List<string> lst_content = new List<string>(File.ReadAllLines(file_name));
            MyStrings mList = null;
            Hashtable h_table = new Hashtable();
            string kStr = "";
            int i = 0;
            for (i = 0; i < lst_content.Count; i++)
            {
                kStr = lst_content[i].Trim().TrimEnd().TrimStart();
                mList = new MyStrings(kStr, '=');
                if (mList.Count == 2)
                    h_table.Add(mList.StringList[0], mList.StringList[1]);
            }

            for (i = 0; i < doc.ActiveLayOut.Entities.Count; i++)
            {
                vdText txt = doc.ActiveLayOut.Entities[i] as vdText;
                if (txt != null)
                {
                    if (h_table.Contains(txt.TextString))
                    {

                        txt.TextString = h_table[txt.TextString].ToString();
                        //if (txt.TextString == "0")
                        //    txt.TextString = "-";
                    }
                }
            }
            doc.Palette.Background = Color.White;
            doc.Redraw(true);
            h_table.Clear();
            lst_content.Clear();
            return true;
        }

        public static bool Draw_Deck_Slab(string file_name, vdDocument doc)
        {
            List<string> lst_content = new List<string>(File.ReadAllLines(file_name));
            MyStrings mList = null;
            Hashtable h_table = new Hashtable();
            string kStr = "";
            int i = 0;
            for (i = 0; i < lst_content.Count; i++)
            {
                kStr = lst_content[i].Trim().TrimEnd().TrimStart();
                mList = new MyStrings(kStr, '=');
                if (mList.Count == 2)
                    h_table.Add(mList.StringList[0], mList.StringList[1]);
            }

            for (i = 0; i < doc.ActiveLayOut.Entities.Count; i++)
            {
                vdText txt = doc.ActiveLayOut.Entities[i] as vdText;
                if (txt != null)
                {
                    if (h_table.Contains(txt.TextString))
                    {

                        txt.TextString = h_table[txt.TextString].ToString();
                        //if (txt.TextString == "0")
                        //    txt.TextString = "-";
                    }
                }
            }
            doc.Palette.Background = Color.White;
            doc.Redraw(true);
            h_table.Clear();
            lst_content.Clear();
            return true;
        }
        public static bool Set_Drawing_File(string file_name, vdDocument doc)
        {
            List<string> lst_content = new List<string>(File.ReadAllLines(file_name));
            MyStrings mList = null;
            Hashtable h_table = new Hashtable();
            string kStr = "";
            int i = 0;
            for (i = 0; i < lst_content.Count; i++)
            {
                kStr = lst_content[i].Trim().TrimEnd().TrimStart();
                mList = new MyStrings(kStr, '=');
                if (mList.Count == 2)
                {
                    h_table.Add(mList.StringList[0], mList.StringList[1]);
                }
                else if (mList.Count > 2)
                {
                    kStr = mList.StringList[1];
                    for (int K = 2; K < mList.Count; K++)
                    {
                        kStr += " = " + mList.StringList[K].TrimStart().TrimEnd();
                    }
                    h_table.Add(mList.StringList[0], kStr);

                }
            }

            for (i = 0; i < doc.ActiveLayOut.Entities.Count; i++)
            {
                vdText txt = doc.ActiveLayOut.Entities[i] as vdText;
                if (txt != null)
                {
                    if (h_table.Contains(txt.TextString))
                    {
                        
                        txt.TextString = h_table[txt.TextString].ToString();
                        //if (txt.TextString == "0")
                        //    txt.TextString = "-";
                    }
                }
            }
            //doc.Palette.Background = Color.White;
            doc.Redraw(true);
            h_table.Clear();
            lst_content.Clear();
            return true;
        }

    }
    internal class SLAB03_BOQ
    {
        string size1;
        string bgd;
        double d1;
        double d2;
        double dist1;
        double dist2;
        double dist3;
        double dist4;
        double bno1;
        double bno2;
        double bno3;
        double bno4;
        double bno5;
        double bwt1;
        double bwt2;
        double bwt3;
        double bwt4;
        double bwt5;
        double depth;
        double shape1;
        double shape2;
        double shape3;
        double shape4;

        public SLAB03_BOQ()
        {
            //size1 = "9.1 x 1.0";
            //bgd = "Fe 415";
            //d1 = 8;
            //d2 = 6;
            //dist1 = 2935;
            //dist2 = 2635;
            //dist3 = 1800;
            //dist4 = 1000;
            //bno1 = 9;
            //bno2 = 9;
            //bno3 = 16;
            //bno4 = 69;
            //bno5 = 38;
            //bwt1 = 0.154;
            //bwt2 = 0.154;
            //bwt3 = 0.154;
            //bwt4 = 0.154;
            //bwt5 = 0.154;
            //depth = 90;
            //shape1 = 2935;
            //shape2 = 2635;
            //shape3 = 1800;
            //shape4 = 1000;

            size1 = "";
            bgd = "";
            d1 = 0.0;
            d2 = 0.0;
            dist1 = 0.0;
            dist2 = 0.0;
            dist3 = 0.0;
            dist4 = 0.0;
            bno1 = 0.0;
            bno2 = 0.0;
            bno3 = 0.0;
            bno4 = 0.0;
            bno5 = 0.0;
            bwt1 = 0.0;
            bwt2 = 0.0;
            bwt3 = 0.0;
            bwt4 = 0.0;
            bwt5 = 0.0;
            depth = 0.0;
            shape1 = 0.0;
            shape2 = 0.0;
            shape3 = 0.0;
            shape4 = 0.0;

        }
        public void Draw_Boq(vdDocument doc)
        {
            for (int i = 0; i < doc.ActiveLayOut.Entities.Count; i++)
            {
                vdText txt = doc.ActiveLayOut.Entities[i] as vdText;
                if (txt != null)
                {
                    #region SWITCH
                    switch (txt.TextString)
                    {
                        case "size1":
                            txt.TextString = size1;
                            break;
                        case "bgd":
                            txt.TextString = bgd;
                            break;
                        case "d1":
                            txt.TextString = d1.ToString();
                            break;
                        case "d2":
                            txt.TextString = d2.ToString();
                            break;
                        case "dist1":
                            txt.TextString = dist1.ToString();
                            break;
                        case "dist2":
                            txt.TextString = dist2.ToString();
                            break;
                        case "dist3":
                            txt.TextString = dist3.ToString();
                            break;
                        case "dist4":
                            txt.TextString = dist4.ToString();
                            break;
                        case "bno1":
                            txt.TextString = bno1.ToString();
                            break;
                        case "bno2":
                            txt.TextString = bno2.ToString();
                            break;
                        case "bno3":
                            txt.TextString = bno3.ToString();
                            break;
                        case "bno4":
                            txt.TextString = bno4.ToString();
                            break;
                        case "bno5":
                            txt.TextString = bno5.ToString();
                            break;
                        case "bwt1":
                            txt.TextString = bwt1.ToString();
                            break;
                        case "bwt2":
                            txt.TextString = bwt2.ToString();
                            break;
                        case "bwt3":
                            txt.TextString = bwt3.ToString();
                            break;
                        case "bwt4":
                            txt.TextString = bwt4.ToString();
                            break;
                        case "bwt5":
                            txt.TextString = bwt5.ToString();
                            break;
                        case "depth":
                            txt.TextString = depth.ToString();
                            break;
                        case "shape1":
                            txt.TextString = shape1.ToString();
                            break;
                        case "shape2":
                            txt.TextString = shape2.ToString();
                            break;
                        case "shape3":
                            txt.TextString = shape3.ToString();
                            break;
                        case "shape4":
                            txt.TextString = shape4.ToString();
                            break;
                    }
                    #endregion
                    txt.Update();
                }
            }
        }
        public bool ReadFromFile(string file_name)
        {
            if (!File.Exists(file_name))
            {
                return false;
            }
            List<string> lstStr = new List<string>(File.ReadAllLines(file_name));
            string kStr = "";
            MyStrings mList = null;

            for (int i = 0; i < lstStr.Count; i++)
            {
                kStr = lstStr[i].Trim().TrimStart().TrimEnd();
                mList = new MyStrings(kStr, '=');

                #region SWITCH
                switch (mList.StringList[0])
                {
                    case "size1":
                        size1 = mList.StringList[1];
                        break;
                    case "bgd":
                        bgd = mList.StringList[1];
                        break;
                    case "d1":
                        d1 = mList.GetDouble(1);
                        break;
                    case "d2":
                        d2 = mList.GetDouble(1);
                        break;
                    case "dist1":
                        dist1 = mList.GetDouble(1); break;
                    case "dist2":
                        dist2 = mList.GetDouble(1); break;
                    case "dist3":
                        dist3 = mList.GetDouble(1); break;
                    case "dist4":
                        dist4 = mList.GetDouble(1); break;
                    case "bno1":
                        bno1 = mList.GetDouble(1); break;
                    case "bno2":
                        bno2 = mList.GetDouble(1); break;
                    case "bno3":
                        bno3 = mList.GetDouble(1); break;
                    case "bno4":
                        bno4 = mList.GetDouble(1); break;
                    case "bno5":
                        bno5 = mList.GetDouble(1); break;
                    case "bwt1":
                        bwt1 = mList.GetDouble(1); break;
                        break;
                    case "bwt2":
                        bwt2 = mList.GetDouble(1); break;
                    case "bwt3":
                        bwt3 = mList.GetDouble(1); break;
                        break;
                    case "bwt4":
                        bwt4 = mList.GetDouble(1); break;
                    case "bwt5":
                        bwt5 = mList.GetDouble(1); break;
                    case "depth":
                        depth = mList.GetDouble(1); break;
                    case "shape1":
                        shape1 = mList.GetDouble(1); break;
                    case "shape2":
                        shape2 = mList.GetDouble(1); break;
                    case "shape3":
                        shape3 = mList.GetDouble(1); break;
                    case "shape4":
                        shape4 = mList.GetDouble(1); break;
                }
                #endregion
            }

            lstStr.Clear();
            return true;
        }


    }
    internal class SLAB04_BOQ
    {
        string size1 = "4.2  x   5.8";
        string bcd1 = "T8_B1";
        string bcd2 = "T8_B2";
        string bcd3 = "T10_B3";
        string bcd4 = "T8_B4";
        string bcd5 = "T8_C1";
        string bcd6 = "";

        string bgd = "Fe 415";

        double d1 = 8;
        double d2 = 10;
        double bl1 = 66;
        double bl2 = 66;
        double bl3 = 66;
        double bl4 = 66;
        double bl5 = 66;
        double bl6 = 66;
        double bwt1 = 0.143;
        double bwt2 = 0.143;
        double bwt3 = 0.143;
        double bwt4 = 0.143;
        double bwt5 = 0.143;
        double bwt6 = 0.143;
        double shape1 = 665;
        double shape2 = 4250;
        double shape3 = 5750;
        double shape4 = 3250;
        double shape5 = 4250;
        double shape6 = 900;
        double shape7 = 840;

        double depth1 = 60;
        double depth2 = 50;

        double bno1 = 66;
        double bno2 = 66;
        double bno3 = 66;
        double bno4 = 66;
        double bno5 = 66;
        double bno6 = 66;


        public SLAB04_BOQ()
        {
            bcd2 = "T8_B2";
            bcd3 = "T10_B3";
            bcd4 = "T8_B4";
            bcd5 = "T8_C1";
            bgd = "Fe 415";
            d1 = 8;
            d2 = 10;

            depth1 = 60;
            depth2 = 50;

            bl1 = 66;
            bl2 = 66;
            bl3 = 66;
            bl4 = 66;
            bl5 = 66;
            bl6 = 66;
            bno1 = 66;
            bno2 = 66;
            bno3 = 66;
            bno4 = 66;
            bno5 = 66;
            bno6 = 66;
            bwt1 = 0.143;
            bwt2 = 0.143;
            bwt3 = 0.143;
            bwt4 = 0.143;
            bwt5 = 0.143;
            bwt6 = 0.143;
            shape1 = 665;
            shape2 = 4250;
            shape3 = 5750;
            shape4 = 3250;
            shape5 = 4250;
            shape6 = 900;
            shape7 = 840;
        }

        /**/

        public void Draw_Boq(vdDocument doc)
        {
            for (int i = 0; i < doc.ActiveLayOut.Entities.Count; i++)
            {
                vdText txt = doc.ActiveLayOut.Entities[i] as vdText;
                if (txt != null)
                {
                    #region SWITCH 1
                    switch (txt.TextString)
                    {
                        case "size1":
                            txt.TextString = size1;
                            break;
                        case "bgd":
                            txt.TextString = bgd;
                            break;
                        case "bcd1":
                            txt.TextString = bcd1;
                            break;
                        case "bcd2":
                            txt.TextString = bcd2;
                            break;
                        case "bcd3":
                            txt.TextString = bcd3;
                            break;
                        case "bcd4":
                            txt.TextString = bcd4;
                            break;
                        case "bcd5":
                            txt.TextString = bcd5;
                            break;
                        case "bcd6":
                            txt.TextString = bcd6;
                            break;
                        case "d1":
                            txt.TextString = d1.ToString();
                            break;
                        case "d2":
                            txt.TextString = d2.ToString();
                            break;
                        case "bl1":
                            txt.TextString = bl1.ToString(); break;
                        case "bl2":
                            txt.TextString = bl2.ToString(); break;
                        case "bl3":
                            txt.TextString = bl3.ToString(); break;
                        case "bl4":
                            txt.TextString = bl4.ToString(); break;
                        case "bl5":
                            txt.TextString = bl5.ToString(); break;
                        case "bl6":
                            txt.TextString = bl6.ToString(); break;
                        case "bno1":
                            txt.TextString = bno1.ToString(); break;
                        case "bno2":
                            txt.TextString = bno2.ToString(); break;
                        case "bno3":
                            txt.TextString = bno3.ToString(); break;
                        case "bno4":
                            txt.TextString = bno4.ToString(); break;
                        case "bno5":
                            txt.TextString = bno5.ToString(); break;
                        case "bno6":
                            txt.TextString = bno6.ToString(); break;
                        case "depth1":
                            txt.TextString = depth1.ToString(); break;
                        case "depth2":
                            txt.TextString = depth2.ToString(); break;
                        case "bwt1":
                            txt.TextString = bwt1.ToString(); break;
                        case "bwt2":
                            txt.TextString = bwt2.ToString(); break;
                        case "bwt3":
                            txt.TextString = bwt3.ToString(); break;
                        case "bwt4":
                            txt.TextString = bwt4.ToString(); break;
                        case "bwt5":
                            txt.TextString = bwt5.ToString(); break;
                        case "bwt6":
                            txt.TextString = bwt6.ToString(); break;
                        case "shape1":
                            txt.TextString = shape1.ToString(); break;
                        case "shape2":
                            txt.TextString = shape2.ToString(); break;
                        case "shape3":
                            txt.TextString = shape3.ToString(); break;
                        case "shape4":
                            txt.TextString = shape4.ToString(); break;
                        case "shape5":
                            txt.TextString = shape5.ToString(); break;
                        case "shape6":
                            txt.TextString = shape6.ToString(); break;
                        case "shape7":
                            txt.TextString = shape7.ToString(); break;
                    }
                    #endregion

                    txt.Update();
                }
            }
        }
        public bool ReadFromFile(string file_name)
        {
            if (!File.Exists(file_name))
            {
                return false;
            }
            List<string> lstStr = new List<string>(File.ReadAllLines(file_name));
            string kStr = "";
            MyStrings mList = null;

            for (int i = 0; i < lstStr.Count; i++)
            {
                kStr = lstStr[i].Trim().TrimStart().TrimEnd();
                mList = new MyStrings(kStr, '=');
                #region SWITCH
                switch (mList.StringList[0])
                {
                    case "size1":
                        size1 = mList.StringList[1];
                        break;
                    case "bgd":
                        bgd = mList.StringList[1];
                        break;
                    case "bcd1":
                        bcd1 = mList.StringList[1];
                        break;
                    case "bcd2":
                        bcd2 = mList.StringList[1];
                        break;
                    case "bcd3":
                        bcd3 = mList.StringList[1];
                        break;
                    case "bcd4":
                        bcd4 = mList.StringList[1];
                        break;
                    case "bcd5":
                        bcd5 = mList.StringList[1];
                        break;
                    case "bcd6":
                        bcd6 = mList.StringList[1];
                        break;
                    case "d1":
                        d1 = mList.GetDouble(1);
                        break;
                    case "d2":
                        d2 = mList.GetDouble(1);
                        break;
                    case "bl1":
                        bl1 = mList.GetDouble(1); break;
                    case "bl2":
                        bl2 = mList.GetDouble(1); break;
                    case "bl3":
                        bl3 = mList.GetDouble(1); break;
                    case "bl4":
                        bl4 = mList.GetDouble(1); break;
                    case "bl5":
                        bl5 = mList.GetDouble(1); break;
                    case "bl6":
                        bl6 = mList.GetDouble(1); break;
                    case "bno1":
                        bno1 = mList.GetDouble(1); break;
                    case "bno2":
                        bno2 = mList.GetDouble(1); break;
                    case "bno3":
                        bno3 = mList.GetDouble(1); break;
                    case "bno4":
                        bno4 = mList.GetDouble(1); break;
                    case "bno5":
                        bno5 = mList.GetDouble(1); break;
                    case "bno6":
                        bno6 = mList.GetDouble(1); break;
                    case "depth1":
                        depth1 = mList.GetDouble(1); break;
                    case "depth2":
                        depth2 = mList.GetDouble(1); break;
                    case "bwt1":
                        bwt1 = mList.GetDouble(1); break;
                    case "bwt2":
                        bwt2 = mList.GetDouble(1); break;
                    case "bwt3":
                        bwt3 = mList.GetDouble(1); break;
                    case "bwt4":
                        bwt4 = mList.GetDouble(1); break;
                    case "bwt5":
                        bwt5 = mList.GetDouble(1); break;
                    case "bwt6":
                        bwt6 = mList.GetDouble(1); break;
                    case "shape1":
                        shape1 = mList.GetDouble(1); break;
                    case "shape2":
                        shape2 = mList.GetDouble(1); break;
                    case "shape3":
                        shape3 = mList.GetDouble(1); break;
                    case "shape4":
                        shape4 = mList.GetDouble(1); break;
                    case "shape5":
                        shape5 = mList.GetDouble(1); break;
                    case "shape6":
                        shape6 = mList.GetDouble(1); break;
                    case "shape7":
                        shape7 = mList.GetDouble(1); break;
                }
                #endregion

            }
            lstStr.Clear();
            return true;
        }
        /**/
    }
}
