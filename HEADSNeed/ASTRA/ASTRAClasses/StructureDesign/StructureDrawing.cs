using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Geometry;

using HEADSNeed.ASTRA.ASTRADrawingTools;
using HEADSNeed.ASTRA.ASTRAClasses;
using HEADSNeed.ASTRA.ASTRAClasses.StructureDesign;
using HEADSNeed.ASTRA.CadToAstra.FORMS;
using HEADSNeed.ASTRA.CadToAstra;

using MovingLoadAnalysis;
using MovingLoadAnalysis.DataStructure;

using VDRAW = VectorDraw.Professional.ActionUtilities;



namespace HEADSNeed.ASTRA.ASTRAClasses.StructureDesign
{
    public class StructureDrawing
    {
        public ASTRADoc AstDoc { get; set; }
        public StructureMemberAnalysis StructureAnalysis { get; set; }
        public vdDocument doc { get; set; }


        public double Footing_B = 2.8;
        public double Footing_D = 2.8;

        public double Footing_level = 1.5;


        public StructureDrawing(ASTRADoc ast_doc, vdDocument basedoc)
        {
            AstDoc = ast_doc;
            doc = basedoc;
            All_Beam_Data = new List<BeamDwg>();
            All_Column_Data = new List<ColumnDwg>();
        }
        public List<BeamDwg> All_Beam_Data { get; set; }
        public List<ColumnDwg> All_Column_Data { get; set; }

        public List<Beam_Construction_Details> All_Beam_Construction_Details { get; set; }
        public List<Column_Construction_Details> All_Column_Construction_Details { get; set; }
        public List<Foundation_Construction_Details> All_Foundation_Construction_Details { get; set; }
        public List<Slab_Construction_Details> All_Slab_Construction_Details { get; set; }

        public List<string> Get_BeamNos(double floor_level)
        {
            List<string> lst = new List<string>();
            if (All_Beam_Data.Count > 0)
            {
                foreach (var item in All_Beam_Data)
                {
                    if (item.FloorLavel == floor_level)
                        lst.Add(item.BeamNo);
                }
            }
            return lst;
        }

        public void Load_All_Beam_Data(string fileName)
        {
            if (!File.Exists(fileName)) return;

            List<string> list = new List<string>(File.ReadAllLines(fileName));
            MyList mlist;
            BeamDwg bd = null;
            All_Beam_Data.Clear();
            foreach (var item in list)
            {
                mlist = new MyList(item, '$');
                try
                {
                    //1$True$3.000$B1$431 433 435 437 439 441 443 445 447 449$0.25$0.4$16$25$16$16$8$116.200$66.300$43.920$56.330$42.630$34.960$28.140$OK$
                    bd = new BeamDwg();
                    bd.FloorLavel = mlist.GetDouble(2);
                    bd.BeamNo = mlist.StringList[3];
                    bd.ContinuosMembers = mlist.StringList[4];
                    All_Beam_Data.Add(bd);
                }
                catch (Exception exx) { }

            }


        }
        public void Load_All_Column_Data(string fileName)
        {
            if (!File.Exists(fileName)) return;

            List<string> list = new List<string>(File.ReadAllLines(fileName));
            MyList mlist;
            ColumnDwg bd = null;
            All_Column_Data.Clear();
            foreach (var item in list)
            {
                mlist = new MyList(item, '$');
                try
                {
                    //False$C1$1 57 113 169 225 281$0.25$0.5$25$6$8$395.6$48.95$ OK $$
                    bd = new ColumnDwg();
                    //bd.FloorLavel = mlist.GetDouble(2);
                    bd.ColumnNo = mlist.StringList[1];
                    bd.ContinuosMembers = mlist.StringList[2];
                    All_Column_Data.Add(bd);
                }
                catch (Exception exx) { }

            }


        }

        public void Load_All_Beam_Construction_Details(string fileName)
        {

            if (!File.Exists(fileName)) return;

            List<string> list = new List<string>(File.ReadAllLines(fileName));
            MyList mlist;
            ColumnDwg bd = null;

            Beam_Construction_Details bcd = null;
            All_Beam_Construction_Details = new List<Beam_Construction_Details>();

            int i = 0;
            string kStr = "";
            for (i = 0; i < list.Count; i++)
            {

                //BEAM = B1, FLOOR LEVEL = 1.500 M
                //CONTINUOUS MEMBERS = 393 TO 402
                //BEAM SECTION = 250 mm. X 300 mm.

                //Provide 4 Nos 16 mm dia bars as Span Steel Bottom, (Ast = 804.248 Sq.mm)
                //Bar mark in drawing = B_Ast[1] as Span Steel (Bottom). 

                //Provide 2 Nos 16 mm dia bars as Span Steel Top.
                //Bar mark in drawing = B_Ast[2], as Span Steel (Top).

                //Provide 2 Nos. 16 mm dia and 2 Nos 25 mm dia bars as Support Steel Top (Ast = 1383.872 Sq.mm.)
                //Bar mark in drawing = B_Ast[3], as Supports Steel (Bottom).

                //Bar mark in drawing = B_Ast[4], as Supports Steel (Top).

                //Provide 2 Nos 16 mm dia bars as Support Steel Bottom.

                //Provide  Y'8 - (2 Legged) Vertical Stirrups @ 170 mm Centre to Centre (Ast = 591.359 Sq.mm. / metre)
                //Bar mark in drawing = B_Ast[5] as Stirrup Steel Shear Reinforcements,

                if (list[i].Contains("BEAM") && list[i].Contains("FLOOR LEVEL"))
                {
                    kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    mlist = new MyList(kStr, ' ');

                    bcd = new Beam_Construction_Details();
                    bcd.Beam_Nos = mlist.StringList[2];


                    bcd.Floor_Level = mlist.GetDouble(7);

                    i++;
                    kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    mlist = new MyList(kStr, '=');
                    bcd.Continuous_Beam_Nos = mlist.StringList[1];


                    i++;
                    kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    mlist = new MyList(kStr, ' ');

                    bcd.Breadth = mlist.GetDouble(3)/1000;
                    bcd.Depth = mlist.GetDouble(6) / 1000;


                    i++;
                    i++;
                    kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    mlist = new MyList(kStr, ' ');

                    bcd.Bar_nos_1 = mlist.GetDouble(1);
                    bcd.Bar_dia_1 = mlist.GetDouble(3);
                    bcd.BAst_1 = mlist.GetDouble(14);



                    i++;
                    i++;
                    i++;
                    kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    mlist = new MyList(kStr, ' ');

                    bcd.Bar_nos_2 = mlist.GetDouble(1);
                    bcd.Bar_dia_2 = mlist.GetDouble(3);
                    //bcd.BAst_2 = mlist.GetDouble(14);



                    i++;
                    i++;
                    i++;
                    kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    mlist = new MyList(kStr, ' ');

                    bcd.Bar_nos_3_1 = mlist.GetDouble(1);
                    bcd.Bar_dia_3_1 = mlist.GetDouble(3);
                    bcd.Bar_nos_3_2 = mlist.GetDouble(7);
                    bcd.Bar_dia_3_2 = mlist.GetDouble(9);
                    bcd.BAst_3 = mlist.GetDouble(19);



                    i++;
                    i++;
                    i++;
                    kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    mlist = new MyList(kStr, ' ');

                    bcd.Bar_nos_4 = mlist.GetDouble(1);
                    bcd.Bar_dia_4 = mlist.GetDouble(3);
                    //bcd.Bar_dia_3 = mlist.GetDouble(7);
                    //bcd.Bar_dia_3 = mlist.GetDouble(9);
                    //bcd.BAst_4 = mlist.GetDouble(19);



                    i++;
                    i++;
                    i++;
                    kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    mlist = new MyList(kStr.Replace("'", " "), ' ');

                    //bcd.Bar_nos_5 = mlist.GetDouble(3);
                    bcd.Bar_dia_5 = mlist.GetDouble(3);
                    bcd.Bar_Spacing_5 = mlist.GetDouble(10);
                    bcd.BAst_5 = mlist.GetDouble(17);
                    //bcd.Bar_dia_3 = mlist.GetDouble(7);
                    //bcd.Bar_dia_3 = mlist.GetDouble(9);
                    //bcd.BAst_3 = mlist.GetDouble(19);
                    All_Beam_Construction_Details.Add(bcd);
                }
            }
        }

        public void Load_All_Foundation_Construction_Details(string fileName)
        {

            if (!File.Exists(fileName)) return;

            List<string> list = new List<string>(File.ReadAllLines(fileName));
            MyList mlist;
            ColumnDwg bd = null;

            //Column_Construction_Details ccd = null;

            Foundation_Construction_Details ccd = null;

            All_Foundation_Construction_Details = new List<Foundation_Construction_Details>();

            int i = 0;
            string kStr = "";
            for (i = 0; i < list.Count; i++)
            {

                //COLUMN = C1
                //CONTINUOUS MEMBERS = 1 57 113 169 225 281 337
                //Column Section = 250 mm. x 250 mm.

                //Provide Main Steel Reinforcement Bars 6 Nos. 25 mm diameter
                //(Total Steel = Cast1 = 2945.243 Sq.mm.)
                //Bar Mark in Drawing = C_Ast[1]

                //Provide Y8 lateral tie @ 380 mm c/c.  (Steel per metre = C_Ast[2] = 132.278 Sq.mm. / metre)
                //Bar Mark in Drawing = C_Ast[2]


                if (list[i].Contains("FOUNDATION"))
                {
                    kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    mlist = new MyList(kStr, ' ');

                    ccd = new Foundation_Construction_Details();
                    ccd.Column_Nos = mlist.StringList[2];


                    //ccd.Floor_Level = mlist.GetDouble(7);

                    //i++;
                    //kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    //mlist = new MyList(kStr, '=');
                    //ccd.Continuous_Column_Nos = mlist.StringList[1];


                    i++;
                    kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    mlist = new MyList(kStr, ' ');

                    ccd.Footing_Length = mlist.GetDouble(8);
                    ccd.Footing_Breadth = mlist.GetDouble(10);


                    i++;
                    i++;
                    i++;
                    i++;
                    i++;
                    kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    mlist = new MyList(kStr, ' ');

                    ccd.Pedestal_Breadth = mlist.GetDouble(8);
                    ccd.Pedestal_Depth = mlist.GetDouble(10);




                    i++;
                    i++;
                    i++;


                    kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    mlist = new MyList(kStr.Replace("T", ""), ' ');

                    ccd.Column_Breadth = mlist.GetDouble(9);
                    ccd.Column_Depth = mlist.GetDouble(11);


                    i++;
                    i++;
                    i++;

                    kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    mlist = new MyList(kStr.Replace("T", ""), ' ');

                    ccd.Bar_spacing_1 = mlist.GetDouble(3);
                    ccd.Bar_dia_1 = mlist.GetDouble(1);
                    ccd.FAst_1 = mlist.GetDouble(14);



                    i++;
                    i++;
                    i++;
                    kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    mlist = new MyList(kStr.Replace("T", ""), ' ');

                    ccd.Bar_dia_2 = mlist.GetDouble(1);
                    ccd.Bar_spacing_2 = mlist.GetDouble(3);
                    ccd.FAst_2 = mlist.GetDouble(12);


                    All_Foundation_Construction_Details.Add(ccd);
                }
            }
        }

        public void Load_All_Slab_Construction_Details(string fileName)
        {

            if (!File.Exists(fileName)) return;

            List<string> list = new List<string>(File.ReadAllLines(fileName));
            MyList mlist;
            ColumnDwg bd = null;

            //Column_Construction_Details ccd = null;

            Slab_Construction_Details ccd = null;

            All_Slab_Construction_Details = new List<Slab_Construction_Details>();

            int i = 0;
            string kStr = "";
            for (i = 0; i < list.Count; i++)
            {

                //COLUMN = C1
                //CONTINUOUS MEMBERS = 1 57 113 169 225 281 337
                //Column Section = 250 mm. x 250 mm.

                //Provide Main Steel Reinforcement Bars 6 Nos. 25 mm diameter
                //(Total Steel = Cast1 = 2945.243 Sq.mm.)
                //Bar Mark in Drawing = C_Ast[1]

                //Provide Y8 lateral tie @ 380 mm c/c.  (Steel per metre = C_Ast[2] = 132.278 Sq.mm. / metre)
                //Bar Mark in Drawing = C_Ast[2]


                if (list[i].Contains("SLAB"))
                {
                    kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    mlist = new MyList(kStr, '=');

                    ccd = new Slab_Construction_Details();
                    ccd.Continuous_Beam_Nos = mlist.StringList[1];


                    //ccd.Floor_Level = mlist.GetDouble(7);

                    //i++;
                    //kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    //mlist = new MyList(kStr, '=');
                    //ccd.Continuous_Column_Nos = mlist.StringList[1];


                    i++;
                    kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    mlist = new MyList(kStr, ' ');

                    ccd.Floor_Level = mlist.GetDouble(4);



                    i++;
                    kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    mlist = new MyList(kStr, ' ');

                    ccd.Depth = mlist.GetDouble(7);




                    i++;
                    i++;
                    i++;
                    kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    mlist = new MyList(kStr.Replace("T", ""), ' ');
                    ccd.Bar_dia_1 = mlist.GetDouble(1);
                    ccd.Bar_spc_1 = mlist.GetDouble(3);
                    ccd.SAst_1 = mlist.GetDouble(9);



                    i++;
                    i++;
                    i++;
                    i++;
                    kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    mlist = new MyList(kStr.Replace("T", ""), ' ');
                    ccd.Bar_dia_2 = mlist.GetDouble(1);
                    ccd.Bar_spc_2 = mlist.GetDouble(3);
                    ccd.SAst_2 = mlist.GetDouble(9);



                    i++;
                    i++;
                    i++;
                    i++;
                    kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    mlist = new MyList(kStr.Replace("T", ""), ' ');
                    ccd.Bar_dia_3 = mlist.GetDouble(1);
                    ccd.Bar_spc_3 = mlist.GetDouble(3);
                    ccd.SAst_3 = mlist.GetDouble(9);


                    i++;
                    i++;
                    i++;
                    i++;
                    kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    mlist = new MyList(kStr.Replace("T", ""), ' ');
                    ccd.Bar_dia_4 = mlist.GetDouble(1);
                    ccd.Bar_spc_4 = mlist.GetDouble(3);
                    ccd.SAst_4 = mlist.GetDouble(9);



                    i++;
                    i++;
                    i++;
                    i++;
                    kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    mlist = new MyList(kStr.Replace("T", ""), ' ');
                    ccd.Bar_dia_5 = mlist.GetDouble(1);
                    ccd.Bar_spc_5 = mlist.GetDouble(3);
                    ccd.SAst_5 = mlist.GetDouble(9);



                    i++;
                    i++;
                    i++;
                    i++;
                    kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    mlist = new MyList(kStr.Replace("T", ""), ' ');
                    ccd.Bar_dia_6 = mlist.GetDouble(1);
                    ccd.Bar_spc_6 = mlist.GetDouble(3);
                    ccd.SAst_6 = mlist.GetDouble(9);

                    i++;
                    i++;
                    i++;
                    i++;
                    kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    mlist = new MyList(kStr.Replace("T", ""), ' ');
                    ccd.Bar_dia_7 = mlist.GetDouble(1);
                    ccd.Bar_spc_7 = mlist.GetDouble(3);
                    ccd.SAst_7 = mlist.GetDouble(9);



                    i++;
                    i++;
                    i++;
                    i++;
                    i++;
                    kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    mlist = new MyList(kStr.Replace("T", ""), ' ');
                    ccd.Bar_dia_8 = mlist.GetDouble(1);
                    ccd.Bar_spc_8 = mlist.GetDouble(3);
                    ccd.SAst_8 = mlist.GetDouble(9);



                    All_Slab_Construction_Details.Add(ccd);
                }
            }
        }

        public void Load_All_Column_Construction_Details(string fileName)
        {


            if (!File.Exists(fileName)) return;

            List<string> list = new List<string>(File.ReadAllLines(fileName));
            MyList mlist;
            ColumnDwg bd = null;

            Column_Construction_Details ccd = null;
            All_Column_Construction_Details = new List<Column_Construction_Details>();

            int i = 0;
            string kStr = "";
            for (i = 0; i < list.Count; i++)
            {

                //COLUMN = C1
                //CONTINUOUS MEMBERS = 1 57 113 169 225 281 337
                //Column Section = 250 mm. x 250 mm.

                //Provide Main Steel Reinforcement Bars 6 Nos. 25 mm diameter
                //(Total Steel = Cast1 = 2945.243 Sq.mm.)
                //Bar Mark in Drawing = C_Ast[1]

                //Provide Y8 lateral tie @ 380 mm c/c.  (Steel per metre = C_Ast[2] = 132.278 Sq.mm. / metre)
                //Bar Mark in Drawing = C_Ast[2]


                if (list[i].Contains("COLUMN"))
                {
                    kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    mlist = new MyList(kStr, ' ');

                    ccd = new Column_Construction_Details();
                    ccd.Column_Nos = mlist.StringList[2];


                    //ccd.Floor_Level = mlist.GetDouble(7);

                    i++;
                    kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    mlist = new MyList(kStr, '=');
                    ccd.Continuous_Column_Nos = mlist.StringList[1];


                    i++;
                    kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    mlist = new MyList(kStr, ' ');

                    ccd.Breadth = mlist.GetDouble(3) / 1000;
                    ccd.Depth = mlist.GetDouble(6) / 1000;


                    i++;
                    i++;
                    kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    mlist = new MyList(kStr, ' ');

                    ccd.Bar_nos_1 = mlist.GetDouble(5);
                    ccd.Bar_dia_1 = mlist.GetDouble(7);


                    i++;
                    kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    mlist = new MyList(kStr, ' ');
                    ccd.CAst_1 = mlist.GetDouble(5);



                    i++;
                    i++;
                    i++;
                    kStr = list[i].TrimStart().Trim().Replace(",", " ");
                    mlist = new MyList(kStr.Replace("Y", ""), ' ');

                    ccd.Bar_dia_2 = mlist.GetDouble(1);
                    ccd.Bar_spacing_2 = mlist.GetDouble(5);
                    ccd.CAst_2 = mlist.GetDouble(15);


                    All_Column_Construction_Details.Add(ccd);
                }
            }
        }

        public string Get_Beam_Data(int mem_no)
        {
            List<int> list = new List<int>();
            foreach (var item in All_Beam_Data)
            {
                list.Clear();

                list.AddRange(MyList.Get_Array_Intiger(item.ContinuosMembers));

                if (list.Contains(mem_no))
                    return item.BeamNo;
            }
            return "";

        }

        public string Get_Column_Data(int mem_no)
        {
            List<int> list = new List<int>();
            foreach (var item in All_Column_Data)
            {
                list.Clear();

                list.AddRange(MyList.Get_Array_Intiger(item.ContinuosMembers));

                if (list.Contains(mem_no))
                    return item.ColumnNo;
            }
            return "";

        }

        public void Draw_Floor_Layout(double flr_lvl, string file_name)
        {
            MemberIncidenceCollection beams = new MemberIncidenceCollection();
            MemberIncidenceCollection columns = new MemberIncidenceCollection();

            for (int i = 0; i < AstDoc.Members.Count; i++)
            {
                var item = AstDoc.Members[i];

                //if (item.StartNode.Y == flr_lvl || item.EndNode.Y == flr_lvl)
                if (item.EndNode.Y == flr_lvl)
                {
                    if (item.StartNode.Y == item.EndNode.Y)
                    {
                        beams.Add(item);
                    }
                    else
                    {
                        columns.Add(item);
                    }
                }
            }

            doc.ActiveLayOut.Entities.RemoveAll();

            doc.Layers.RemoveAll();

            vdLayer concLay = doc.Layers.FindName("Concrete");

            if (concLay == null)
            {
                concLay = new vdLayer(doc, "Concrete");
                //concLay.setDocumentDefaults(doc);
                //concLay.SetUnRegisterDocument();
                concLay.PenColor = new vdColor(Color.Red);

                doc.Layers.Add(concLay);
            }


            vdLayer beamsLay = doc.Layers.FindName("Beam");
            if (beamsLay == null)
            {
                beamsLay = new vdLayer(doc, "Beam");
                //concLay.setDocumentDefaults(doc);
                //concLay.SetUnRegisterDocument();
                beamsLay.PenColor = new vdColor(Color.Pink);

                doc.Layers.Add(beamsLay);
            }
            vdLayer columnLay = doc.Layers.FindName("Column");
            if (columnLay == null)
            {
                columnLay = new vdLayer(doc, "Column");
                //concLay.setDocumentDefaults(doc);
                //concLay.SetUnRegisterDocument();
                columnLay.PenColor = new vdColor(Color.Cyan);

                doc.Layers.Add(columnLay);
            }
            vdLayer foundLay = doc.Layers.FindName("Foundation");
            if (foundLay == null)
            {
                foundLay = new vdLayer(doc, "Foundation");
                //concLay.setDocumentDefaults(doc);
                //concLay.SetUnRegisterDocument();
                foundLay.PenColor = new vdColor(Color.Gray);

                doc.Layers.Add(foundLay);
            }

            vdLayer textLay = doc.Layers.FindName("Texts");
            if (textLay == null)
            {
                textLay = new vdLayer(doc, "Texts");
                //concLay.setDocumentDefaults(doc);
                //concLay.SetUnRegisterDocument();
                textLay.PenColor = new vdColor(Color.YellowGreen);

                doc.Layers.Add(textLay);
            }


            vdLine ln = new vdLine();



            vdMText vtxt = null;
            vdCircle vcle = null;
            vdLine ln2 = null;
            vdLine conln = null;

            double txt_dist = 0.05;
            double txt_hgt = 0.03;
            double cir_rds = 0.2;


            double max_z = AstDoc.Joints.Max_Z_Positive;
            foreach (var item in beams)
            {
                ln = new vdLine();

                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();

                ln.StartPoint.x = item.StartNode.X;
                ln.StartPoint.y = max_z - item.StartNode.Z;

                ln.EndPoint.x = item.EndNode.X;
                ln.EndPoint.y = max_z - item.EndNode.Z;

                ln.Layer = beamsLay;
                doc.ActiveLayOut.Entities.Add(ln);

                #region Draw Concrete Line 1

                conln = new vdLine();

                conln.SetUnRegisterDocument(doc);
                conln.setDocumentDefaults();


                conln.StartPoint.x = ln.StartPoint.x;
                conln.StartPoint.y = ln.StartPoint.y;


                conln.EndPoint.x = ln.EndPoint.x;
                conln.EndPoint.y = ln.EndPoint.y;


                if (item.Direction == eDirection.X_Direction)
                {
                    //conln.StartPoint.y = ln.StartPoint.y + item.Property.ZD / 2;
                    //conln.EndPoint.y = ln.EndPoint.y + item.Property.ZD / 2;

                    conln.StartPoint.y = ln.StartPoint.y + item.Property.YD / 8;
                    conln.EndPoint.y = ln.EndPoint.y + item.Property.YD / 8;

                }
                else if (item.Direction == eDirection.Z_Direction)
                {
                    //conln.StartPoint.x = ln.StartPoint.x + item.Property.ZD / 2;
                    //conln.EndPoint.x = ln.EndPoint.x + item.Property.ZD / 2;

                    conln.StartPoint.x = ln.StartPoint.x + item.Property.YD / 8;
                    conln.EndPoint.x = ln.EndPoint.x + item.Property.YD / 8;

                }
                conln.Layer = concLay;


                conln.LineType = doc.LineTypes.DPIDashDot;
                doc.ActiveLayOut.Entities.Add(conln);

                #endregion Draw Concrete Line


                #region Draw Concrete Line 2

                conln = new vdLine();

                conln.SetUnRegisterDocument(doc);
                conln.setDocumentDefaults();


                conln.StartPoint.x = ln.StartPoint.x;
                conln.StartPoint.y = ln.StartPoint.y;


                conln.EndPoint.x = ln.EndPoint.x;
                conln.EndPoint.y = ln.EndPoint.y;


                if (item.Direction == eDirection.X_Direction)
                {
                    //conln.StartPoint.y = ln.StartPoint.y - item.Property.ZD / 2;
                    //conln.EndPoint.y = ln.EndPoint.y - item.Property.ZD / 2;

                    conln.StartPoint.y = ln.StartPoint.y - item.Property.YD / 8;
                    conln.EndPoint.y = ln.EndPoint.y - item.Property.YD / 8;

                }
                else if (item.Direction == eDirection.Z_Direction)
                {
                    //conln.StartPoint.x = ln.StartPoint.x - item.Property.ZD / 2;
                    //conln.EndPoint.x = ln.EndPoint.x - item.Property.ZD / 2;

                    conln.StartPoint.x = ln.StartPoint.x - item.Property.YD / 8;
                    conln.EndPoint.x = ln.EndPoint.x - item.Property.YD / 8;

                }

                conln.Layer = concLay;
                conln.LineType = doc.LineTypes.DPIDashDot;
                doc.ActiveLayOut.Entities.Add(conln);

                #endregion Draw Concrete Line 2


                vtxt = new vdMText();

                vtxt.SetUnRegisterDocument(doc);
                vtxt.setDocumentDefaults();

                vtxt.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter;
                vtxt.VerJustify = VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerCen;
                vtxt.InsertionPoint = (ln.StartPoint + ln.EndPoint) / 2;

                if (item.Direction == eDirection.X_Direction)
                    vtxt.InsertionPoint.y = vtxt.InsertionPoint.y + txt_dist;
                else
                    vtxt.InsertionPoint.x = vtxt.InsertionPoint.x + txt_dist;

                vtxt.Height = txt_hgt;


                //vtxt.TextString = "B1";
                vtxt.TextString = Get_Beam_Data(item.MemberNo);



                //ln.ToolTip = vtxt.TextString;
                vtxt.Layer = textLay;

                doc.ActiveLayOut.Entities.Add(vtxt);



                vcle = new vdCircle();

                vcle.SetUnRegisterDocument(doc);
                vcle.setDocumentDefaults();

                vcle.Center = vtxt.InsertionPoint;
                vcle.Radius = cir_rds;

                //doc.ActiveLayOut.Entities.Add(vcle);



                ln2 = new vdLine();

                ln2.SetUnRegisterDocument(doc);
                ln2.setDocumentDefaults();


                ln2.StartPoint = (ln.StartPoint + ln.EndPoint) / 2;
                ln2.EndPoint = vtxt.InsertionPoint;
                //ln2.StartPoint = (ln.StartPoint + ln.EndPoint) / 2;

                if (item.Direction == eDirection.X_Direction)
                    ln2.EndPoint.y = ln2.EndPoint.y - vcle.Radius;

                else
                    ln2.EndPoint.x = ln2.EndPoint.x - vcle.Radius;

                ln2.Layer = concLay;

                doc.ActiveLayOut.Entities.Add(ln2);



            }

            vdPolyline pln = null;

            foreach (var item in columns)
            {
                pln = new vdPolyline();

                pln.SetUnRegisterDocument(doc);
                pln.setDocumentDefaults();


                //pln.VertexList.Add(new gPoint(item.StartNode.X - item.Property.YD / 2, item.StartNode.Z + item.Property.ZD / 2));
                //pln.VertexList.Add(new gPoint(item.StartNode.X + item.Property.YD / 2, item.StartNode.Z + item.Property.ZD / 2));
                //pln.VertexList.Add(new gPoint(item.StartNode.X + item.Property.YD / 2, item.StartNode.Z - item.Property.ZD / 2));
                //pln.VertexList.Add(new gPoint(item.StartNode.X - item.Property.YD / 2, item.StartNode.Z - item.Property.ZD / 2));
                //pln.VertexList.Add(new gPoint(item.StartNode.X - item.Property.YD / 2, item.StartNode.Z + item.Property.ZD / 2));


                pln.VertexList.Add(new gPoint(item.StartNode.X - item.Property.YD / 4, max_z - (item.StartNode.Z + item.Property.ZD / 4)));
                pln.VertexList.Add(new gPoint(item.StartNode.X + item.Property.YD / 4, max_z - (item.StartNode.Z + item.Property.ZD / 4)));
                pln.VertexList.Add(new gPoint(item.StartNode.X + item.Property.YD / 4, max_z - (item.StartNode.Z - item.Property.ZD / 4)));
                pln.VertexList.Add(new gPoint(item.StartNode.X - item.Property.YD / 4, max_z - (item.StartNode.Z - item.Property.ZD / 4)));
                pln.VertexList.Add(new gPoint(item.StartNode.X - item.Property.YD / 4, max_z - (item.StartNode.Z + item.Property.ZD / 4)));

                pln.Layer = columnLay;
                doc.ActiveLayOut.Entities.Add(pln);


                vtxt = new vdMText();

                vtxt.SetUnRegisterDocument(doc);
                vtxt.setDocumentDefaults();

                //vtxt.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter;
                //vtxt.VerJustify = VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerCen;

                //vtxt.InsertionPoint = new gPoint(item.StartNode.X, max_z - item.StartNode.Z);
                vtxt.InsertionPoint = pln.VertexList[1];

                //if (item.Direction == eDirection.X_Direction)
                //    vtxt.InsertionPoint.y = vtxt.InsertionPoint.y + 0.5;
                //else
                //vtxt.InsertionPoint.x = vtxt.InsertionPoint.x + item.Property.YD / 2;

                //vtxt.Height = 0.15;
                vtxt.Height = txt_hgt;


                //vtxt.TextString = "C1";
                vtxt.TextString = Get_Column_Data(item.MemberNo);

                vtxt.Layer = textLay;
                doc.ActiveLayOut.Entities.Add(vtxt);




                if (flr_lvl == Footing_level)
                {

                    pln = new vdPolyline();

                    pln.SetUnRegisterDocument(doc);
                    pln.setDocumentDefaults();

                    pln.VertexList.Add(new gPoint(item.StartNode.X - Footing_B / 4, max_z - (item.StartNode.Z + Footing_D / 4)));
                    pln.VertexList.Add(new gPoint(item.StartNode.X + Footing_B / 4, max_z - (item.StartNode.Z + Footing_D / 4)));
                    pln.VertexList.Add(new gPoint(item.StartNode.X + Footing_B / 4, max_z - (item.StartNode.Z - Footing_D / 4)));
                    pln.VertexList.Add(new gPoint(item.StartNode.X - Footing_B / 4, max_z - (item.StartNode.Z - Footing_D / 4)));
                    pln.VertexList.Add(new gPoint(item.StartNode.X - Footing_B / 4, max_z - (item.StartNode.Z + Footing_D / 4)));

                    pln.Layer = foundLay;
                    doc.ActiveLayOut.Entities.Add(pln);


                    ln = new vdLine();

                    ln.SetUnRegisterDocument(doc);
                    ln.setDocumentDefaults();


                    ln.StartPoint = (new gPoint(item.StartNode.X - Footing_B / 4, max_z - (item.StartNode.Z + Footing_D / 4)));
                    ln.EndPoint = new gPoint(item.StartNode.X + Footing_B / 4, max_z - (item.StartNode.Z - Footing_D / 4));

                    ln.Layer = foundLay;
                    doc.ActiveLayOut.Entities.Add(ln);



                    vtxt = new vdMText();

                    vtxt.SetUnRegisterDocument(doc);
                    vtxt.setDocumentDefaults();

                    //vtxt.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter;
                    //vtxt.VerJustify = VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerCen;

                    //vtxt.InsertionPoint = new gPoint(item.StartNode.X, max_z - item.StartNode.Z);
                    vtxt.InsertionPoint = pln.VertexList[1];

                    //if (item.Direction == eDirection.X_Direction)
                    //    vtxt.InsertionPoint.y = vtxt.InsertionPoint.y + 0.5;
                    //else
                    //vtxt.InsertionPoint.x = vtxt.InsertionPoint.x + item.Property.YD / 2;

                    //vtxt.Height = 0.15;
                    //vtxt.Height = item.Property.YD / 2;
                    vtxt.Height = txt_hgt;

                    //if (item.MemberNo > 56)
                    //    vtxt.TextString = "C1";

                    vtxt.TextString = "F" + item.MemberNo;

                    vtxt.Layer = textLay;
                    doc.ActiveLayOut.Entities.Add(vtxt);



                    ln = new vdLine();

                    ln.SetUnRegisterDocument(doc);
                    ln.setDocumentDefaults();


                    ln.StartPoint = (new gPoint(item.StartNode.X + Footing_B / 4, max_z - (item.StartNode.Z + Footing_D / 4)));
                    ln.EndPoint = (new gPoint(item.StartNode.X - Footing_B / 4, max_z - (item.StartNode.Z - Footing_D / 4)));
                    ln.Layer = foundLay;

                    doc.ActiveLayOut.Entities.Add(ln);
                }
            }



            doc.Palette.Background = Color.Black;
            doc.Redraw(true);

            if (file_name != "")
                doc.SaveAs(file_name);
        }

        public void Draw_Beam_Moment_Diagram_2015_05_03(double flr_lvl, string beam_no, string file_name)
        {
            MemberIncidenceCollection mic = new MemberIncidenceCollection();

            int i = 0;
            List<int> mems = new List<int>();
            for (i = 0; i < All_Beam_Data.Count; i++)
            {
                if (All_Beam_Data[i].BeamNo == beam_no && All_Beam_Data[i].FloorLavel == flr_lvl)
                {
                    mems = MyList.Get_Array_Intiger(All_Beam_Data[i].ContinuosMembers);
                    if (mems.Count > 0)
                    {
                        foreach (var item in mems)
                        {
                            mic.Add(AstDoc.Members.Get_Member(item));
                        }
                    }
                    break;
                }
            }

            if (mic.Count == 0) return;
            List<JointCoordinate> jnts = new List<JointCoordinate>();

            List<double> mem_length = new List<double>();
            double max_len = 0.0;

            foreach (var item in mic)
            {
                mem_length.Add(item.Length);

                if (max_len < item.Length)
                    max_len = item.Length;

                if (!jnts.Contains(item.StartNode))
                    jnts.Add(item.StartNode);
                if (!jnts.Contains(item.EndNode))
                    jnts.Add(item.EndNode);
            }





            List<int> jn = new List<int>();
            List<double> frcs = new List<double>();
            foreach (var item in jnts)
            {
                jn.Clear();

                jn.Add(item.NodeNo);

                //frcs.Add(StructureAnalysis.GetJoint_M3_Bending(jn));
                frcs.Add(StructureAnalysis.GetJoint_MomentForce(jn));


            }
            double max_frc = 0.0;
            foreach (var item in frcs)
            {
                if (max_frc < item)
                    max_frc = item;
            }
            doc.ActiveLayOut.Entities.RemoveAll();

            #region Draw Members
            vdPolyline vpl = new vdPolyline();
            vpl.SetUnRegisterDocument(doc);
            vpl.setDocumentDefaults();


            vpl.VertexList.Add(new gPoint());

            gPoint gp = new gPoint();
            vpl.VertexList.Add(gp);




            vdLine vln = new vdLine();

            double Y_Limit = max_len * 0.5;





            vdPolyline vfrc = new vdPolyline();
            vfrc.SetUnRegisterDocument(doc);
            vfrc.setDocumentDefaults();





            double fx = Y_Limit / 1.2;

            vdMText vmt = new vdMText();
            vmt.SetUnRegisterDocument(doc);
            vmt.setDocumentDefaults();

            vfrc.VertexList.Add(new gPoint(gp.x, 0 + (frcs[0] / max_frc) * fx));


            vmt.InsertionPoint = vfrc.VertexList[0];
            vmt.TextString = frcs[0].ToString("f3");

            vmt.Height = Y_Limit * 0.1;


            doc.ActiveLayOut.Entities.Add(vmt);


            #region Draw Verical Line
            //vln = new vdLine();
            vln.SetUnRegisterDocument(doc);
            vln.setDocumentDefaults();


            vln.StartPoint = new gPoint(gp.x, gp.y + Y_Limit);
            vln.EndPoint = new gPoint(gp.x, gp.y - Y_Limit);
            doc.ActiveLayOut.Entities.Add(vln);


            vmt = new vdMText();
            vmt.SetUnRegisterDocument(doc);
            vmt.setDocumentDefaults();

            //vfrc.VertexList.Add(new gPoint(gp.x, 0 + (frcs[0] / max_frc) * fx));


            vmt.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter;
            vmt.VerJustify = VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerBottom;

            vmt.InsertionPoint = new gPoint(vln.StartPoint.x, vln.StartPoint.y + 0.5);
            vmt.TextString = "Joint No : " + jnts[0].NodeNo;
            vmt.Height = Y_Limit * 0.1;
            doc.ActiveLayOut.Entities.Add(vmt);



            vmt = new vdMText();
            vmt.SetUnRegisterDocument(doc);
            vmt.setDocumentDefaults();

            //vfrc.VertexList.Add(new gPoint(gp.x, 0 + (frcs[0] / max_frc) * fx));


            vmt.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter;
            vmt.VerJustify = VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerBottom;

            vmt.InsertionPoint = new gPoint(vln.EndPoint.x, vln.EndPoint.y - 0.5);
            vmt.TextString = "Joint No : " + jnts[0].NodeNo;
            vmt.Height = Y_Limit * 0.1;
            doc.ActiveLayOut.Entities.Add(vmt);


            #endregion Draw Verical Line

            for (i = 0; i < mem_length.Count; i++)
            {
                gp = new gPoint(gp.x + mem_length[i], 0);
                vpl.VertexList.Add(gp);


                //gp = new gPoint(gp.x + frcs[i]/max_frc, 0);
                vfrc.VertexList.Add(new gPoint(gp.x - mem_length[i] / 2, -(((frcs[i + 1] + frcs[i]) / 2) / max_frc) * fx));


                #region Member No and Length
                vmt = new vdMText();
                vmt.SetUnRegisterDocument(doc);
                vmt.setDocumentDefaults();

                //vfrc.VertexList.Add(new gPoint(gp.x, 0 + (frcs[0] / max_frc) * fx));

                vmt.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter;
                vmt.VerJustify = VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerBottom;
                vmt.InsertionPoint = new gPoint((gp.x - mem_length[i] / 2), (0.5));
                vmt.TextString = "Member No : " + mic[i].MemberNo + "\n Length = " + mic[i].Length.ToString("f2") + " m.";
                vmt.Height = Y_Limit * 0.1;
                doc.ActiveLayOut.Entities.Add(vmt);

                #endregion Member No and Length




                vmt = new vdMText();
                vmt.SetUnRegisterDocument(doc);
                vmt.setDocumentDefaults();

                //vfrc.VertexList.Add(new gPoint(gp.x, 0 + (frcs[0] / max_frc) * fx));


                vmt.InsertionPoint = vfrc.VertexList[vfrc.VertexList.Count - 1];
                vmt.TextString = (((frcs[i + 1] + frcs[i]) / 2)).ToString("f3");
                vmt.Height = Y_Limit * 0.1;
                doc.ActiveLayOut.Entities.Add(vmt);




                vfrc.VertexList.Add(new gPoint(gp.x, (frcs[i + 1] / max_frc) * fx));

                vmt = new vdMText();
                vmt.SetUnRegisterDocument(doc);
                vmt.setDocumentDefaults();

                //vfrc.VertexList.Add(new gPoint(gp.x, 0 + (frcs[0] / max_frc) * fx));


                vmt.InsertionPoint = vfrc.VertexList[vfrc.VertexList.Count - 1];
                vmt.TextString = frcs[i + 1].ToString("f3");
                vmt.Height = Y_Limit * 0.1;
                doc.ActiveLayOut.Entities.Add(vmt);


                #region Draw Verical Line
                vln = new vdLine();
                vln.SetUnRegisterDocument(doc);
                vln.setDocumentDefaults();


                vln.StartPoint = new gPoint(gp.x, gp.y + Y_Limit);
                vln.EndPoint = new gPoint(gp.x, gp.y - Y_Limit);
                doc.ActiveLayOut.Entities.Add(vln);


                vmt = new vdMText();
                vmt.SetUnRegisterDocument(doc);
                vmt.setDocumentDefaults();

                //vfrc.VertexList.Add(new gPoint(gp.x, 0 + (frcs[0] / max_frc) * fx));


                vmt.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter;
                vmt.VerJustify = VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerBottom;

                vmt.InsertionPoint = new gPoint(vln.StartPoint.x, vln.StartPoint.y + 0.5);
                vmt.TextString = "Joint No : " + jnts[i + 1].NodeNo;
                vmt.Height = Y_Limit * 0.1;
                doc.ActiveLayOut.Entities.Add(vmt);



                vmt = new vdMText();
                vmt.SetUnRegisterDocument(doc);
                vmt.setDocumentDefaults();

                //vfrc.VertexList.Add(new gPoint(gp.x, 0 + (frcs[0] / max_frc) * fx));


                vmt.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter;
                vmt.VerJustify = VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerBottom;

                vmt.InsertionPoint = new gPoint(vln.EndPoint.x, vln.EndPoint.y - 0.5);
                vmt.TextString = "Joint No : " + jnts[i + 1].NodeNo;
                vmt.Height = Y_Limit * 0.1;
                doc.ActiveLayOut.Entities.Add(vmt);


                #endregion Draw Verical Line



            }
            doc.ActiveLayOut.Entities.Add(vpl);

            vfrc.SPlineFlag = VectorDraw.Professional.Constants.VdConstSplineFlag.SFlagFITTING;
            doc.ActiveLayOut.Entities.Add(vfrc);

            doc.ShowUCSAxis = false;
            doc.Redraw(true);

            if (file_name != "")
                doc.SaveAs(file_name);



            vmt = new vdMText();
            vmt.SetUnRegisterDocument(doc);
            vmt.setDocumentDefaults();


            vmt.VerJustify = VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerCen;
            vmt.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter;
            vmt.InsertionPoint = new gPoint(vpl.Length() / 2, -Y_Limit - 1.5);

            vmt.TextString = "BENDING MOMENT DIAGRAM \n FLOOR LEVEL = " + flr_lvl.ToString("f3")
                + "m.  and  BEAM = " + beam_no + ", Length = " + vpl.Length().ToString("f2") + " m";

            vmt.Height = Y_Limit * 0.1;


            doc.ActiveLayOut.Entities.Add(vmt);




            #endregion Draw Members


        }

        public void Draw_Beam_Moment_Diagram(double flr_lvl, string beam_no, string file_name)
        {
            MemberIncidenceCollection mic = new MemberIncidenceCollection();

            int i = 0;
            List<int> mems = new List<int>();
            for (i = 0; i < All_Beam_Data.Count; i++)
            {
                if (All_Beam_Data[i].BeamNo == beam_no && All_Beam_Data[i].FloorLavel == flr_lvl)
                {
                    mems = MyList.Get_Array_Intiger(All_Beam_Data[i].ContinuosMembers);
                    if (mems.Count > 0)
                    {
                        foreach (var item in mems)
                        {
                            mic.Add(AstDoc.Members.Get_Member(item));
                        }
                    }
                    break;
                }
            }

            if (mic.Count == 0) return;
            List<JointCoordinate> jnts = new List<JointCoordinate>();

            List<double> mem_length = new List<double>();
            double max_len = 0.0;

            foreach (var item in mic)
            {
                if (item != null)
                {
                    mem_length.Add(item.Length);

                    if (max_len < item.Length)
                        max_len = item.Length;

                    if (!jnts.Contains(item.StartNode))
                        jnts.Add(item.StartNode);
                    if (!jnts.Contains(item.EndNode))
                        jnts.Add(item.EndNode);
                }
            }





            List<int> jn = new List<int>();
            List<double> frcs = new List<double>();
            foreach (var item in jnts)
            {
                jn.Clear();

                jn.Add(item.NodeNo);

                //frcs.Add(StructureAnalysis.GetJoint_M3_Bending(jn));
                frcs.Add(StructureAnalysis.GetJoint_MomentForce(jn));


            }
            double max_frc = 0.0;
            foreach (var item in frcs)
            {
                if (max_frc < item)
                    max_frc = item;
            }
            doc.ActiveLayOut.Entities.RemoveAll();

            #region Draw Members
            vdPolyline vpl = new vdPolyline();
            vpl.SetUnRegisterDocument(doc);
            vpl.setDocumentDefaults();


            vpl.VertexList.Add(new gPoint());

            gPoint gp = new gPoint();
            vpl.VertexList.Add(gp);




            vdLine vln = new vdLine();

            double Y_Limit = max_len * 0.5;





            vdPolyline vfrc = new vdPolyline();
            vfrc.SetUnRegisterDocument(doc);
            vfrc.setDocumentDefaults();





            double fx = Y_Limit / 1.2;

            vdMText vmt = new vdMText();
            vmt.SetUnRegisterDocument(doc);
            vmt.setDocumentDefaults();

            vfrc.VertexList.Add(new gPoint(gp.x, 0 + (frcs[0] / max_frc) * fx));


            vmt.InsertionPoint = vfrc.VertexList[0];
            vmt.TextString = frcs[0].ToString("f3");

            vmt.Height = Y_Limit * 0.1;


            doc.ActiveLayOut.Entities.Add(vmt);


            #region Draw Verical Line
            //vln = new vdLine();
            vln.SetUnRegisterDocument(doc);
            vln.setDocumentDefaults();


            vln.StartPoint = new gPoint(gp.x, gp.y + Y_Limit);
            vln.EndPoint = new gPoint(gp.x, gp.y - Y_Limit);
            doc.ActiveLayOut.Entities.Add(vln);


            vmt = new vdMText();
            vmt.SetUnRegisterDocument(doc);
            vmt.setDocumentDefaults();

            //vfrc.VertexList.Add(new gPoint(gp.x, 0 + (frcs[0] / max_frc) * fx));


            vmt.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter;
            vmt.VerJustify = VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerBottom;

            vmt.InsertionPoint = new gPoint(vln.StartPoint.x, vln.StartPoint.y + 0.5);
            vmt.TextString = "Joint No : " + jnts[0].NodeNo;
            vmt.Height = Y_Limit * 0.1;
            doc.ActiveLayOut.Entities.Add(vmt);



            vmt = new vdMText();
            vmt.SetUnRegisterDocument(doc);
            vmt.setDocumentDefaults();

            //vfrc.VertexList.Add(new gPoint(gp.x, 0 + (frcs[0] / max_frc) * fx));


            vmt.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter;
            vmt.VerJustify = VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerBottom;

            vmt.InsertionPoint = new gPoint(vln.EndPoint.x, vln.EndPoint.y - 0.5);
            vmt.TextString = "Joint No : " + jnts[0].NodeNo;
            vmt.Height = Y_Limit * 0.1;
            doc.ActiveLayOut.Entities.Add(vmt);


            #endregion Draw Verical Line

            for (i = 0; i < mem_length.Count; i++)
            {
                gp = new gPoint(gp.x + mem_length[i], 0);
                vpl.VertexList.Add(gp);


                //gp = new gPoint(gp.x + frcs[i]/max_frc, 0);
                vfrc.VertexList.Add(new gPoint(gp.x - mem_length[i] / 2, -(((frcs[i + 1] + frcs[i]) / 2) / max_frc) * fx));


                #region Member No and Length
                vmt = new vdMText();
                vmt.SetUnRegisterDocument(doc);
                vmt.setDocumentDefaults();

                //vfrc.VertexList.Add(new gPoint(gp.x, 0 + (frcs[0] / max_frc) * fx));

                vmt.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter;
                vmt.VerJustify = VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerBottom;
                vmt.InsertionPoint = new gPoint((gp.x - mem_length[i] / 2), (0.5));
                vmt.TextString = "Member No : " + mic[i].MemberNo + "\n Length = " + mic[i].Length.ToString("f2") + " m.";
                vmt.Height = Y_Limit * 0.1;
                doc.ActiveLayOut.Entities.Add(vmt);

                #endregion Member No and Length




                vmt = new vdMText();
                vmt.SetUnRegisterDocument(doc);
                vmt.setDocumentDefaults();

                //vfrc.VertexList.Add(new gPoint(gp.x, 0 + (frcs[0] / max_frc) * fx));


                vmt.InsertionPoint = vfrc.VertexList[vfrc.VertexList.Count - 1];
                vmt.TextString = (((frcs[i + 1] + frcs[i]) / 2)).ToString("f3");
                vmt.Height = Y_Limit * 0.1;
                doc.ActiveLayOut.Entities.Add(vmt);




                vfrc.VertexList.Add(new gPoint(gp.x, (frcs[i + 1] / max_frc) * fx));

                #region Chiranjit [2015 05 03]

                vfrc.SPlineFlag = VectorDraw.Professional.Constants.VdConstSplineFlag.SFlagFITTING;
                doc.ActiveLayOut.Entities.Add(vfrc);

                vfrc = new vdPolyline();
                vfrc.SetUnRegisterDocument(doc);
                vfrc.setDocumentDefaults();
                vfrc.VertexList.Add(new gPoint(gp.x, (frcs[i + 1] / max_frc) * fx));


                #endregion Chiranjit [2015 05 03]



                vmt = new vdMText();
                vmt.SetUnRegisterDocument(doc);
                vmt.setDocumentDefaults();

                //vfrc.VertexList.Add(new gPoint(gp.x, 0 + (frcs[0] / max_frc) * fx));


                vmt.InsertionPoint = vfrc.VertexList[vfrc.VertexList.Count - 1];
                vmt.TextString = frcs[i + 1].ToString("f3");
                vmt.Height = Y_Limit * 0.1;
                doc.ActiveLayOut.Entities.Add(vmt);


                #region Draw Verical Line
                vln = new vdLine();
                vln.SetUnRegisterDocument(doc);
                vln.setDocumentDefaults();


                vln.StartPoint = new gPoint(gp.x, gp.y + Y_Limit);
                vln.EndPoint = new gPoint(gp.x, gp.y - Y_Limit);
                doc.ActiveLayOut.Entities.Add(vln);


                vmt = new vdMText();
                vmt.SetUnRegisterDocument(doc);
                vmt.setDocumentDefaults();

                //vfrc.VertexList.Add(new gPoint(gp.x, 0 + (frcs[0] / max_frc) * fx));


                vmt.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter;
                vmt.VerJustify = VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerBottom;

                vmt.InsertionPoint = new gPoint(vln.StartPoint.x, vln.StartPoint.y + 0.5);
                vmt.TextString = "Joint No : " + jnts[i + 1].NodeNo;
                vmt.Height = Y_Limit * 0.1;
                doc.ActiveLayOut.Entities.Add(vmt);



                vmt = new vdMText();
                vmt.SetUnRegisterDocument(doc);
                vmt.setDocumentDefaults();

                //vfrc.VertexList.Add(new gPoint(gp.x, 0 + (frcs[0] / max_frc) * fx));


                vmt.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter;
                vmt.VerJustify = VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerBottom;

                vmt.InsertionPoint = new gPoint(vln.EndPoint.x, vln.EndPoint.y - 0.5);
                vmt.TextString = "Joint No : " + jnts[i + 1].NodeNo;
                vmt.Height = Y_Limit * 0.1;
                doc.ActiveLayOut.Entities.Add(vmt);


                #endregion Draw Verical Line

            }
            doc.ActiveLayOut.Entities.Add(vpl);

            vfrc.SPlineFlag = VectorDraw.Professional.Constants.VdConstSplineFlag.SFlagFITTING;
            doc.ActiveLayOut.Entities.Add(vfrc);

            doc.ShowUCSAxis = false;
            doc.Redraw(true);

            if (file_name != "")
                doc.SaveAs(file_name);



            vmt = new vdMText();
            vmt.SetUnRegisterDocument(doc);
            vmt.setDocumentDefaults();


            vmt.VerJustify = VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerCen;
            vmt.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter;
            vmt.InsertionPoint = new gPoint(vpl.Length() / 2, -Y_Limit - 1.5);

            vmt.TextString = "BENDING MOMENT DIAGRAM \n FLOOR LEVEL = " + flr_lvl.ToString("f3")
                + "m.  and  BEAM = " + beam_no + ", Length = " + vpl.Length().ToString("f2") + " m";

            vmt.Height = Y_Limit * 0.1;


            doc.ActiveLayOut.Entities.Add(vmt);




            #endregion Draw Members


        }

        public void Draw_Beam_Force_Diagram(double flr_lvl, string beam_no, string file_name)
        {
            MemberIncidenceCollection mic = new MemberIncidenceCollection();

            int i = 0;
            List<int> mems = new List<int>();
            for (i = 0; i < All_Beam_Data.Count; i++)
            {
                if (All_Beam_Data[i].BeamNo == beam_no && All_Beam_Data[i].FloorLavel == flr_lvl)
                {
                    mems = MyList.Get_Array_Intiger(All_Beam_Data[i].ContinuosMembers);
                    if (mems.Count > 0)
                    {
                        foreach (var item in mems)
                        {
                            mic.Add(AstDoc.Members.Get_Member(item));
                        }
                    }
                    break;
                }
            }

            if (mic.Count == 0) return;
            List<JointCoordinate> jnts = new List<JointCoordinate>();

            List<double> mem_length = new List<double>();
            double max_len = 0.0;

            foreach (var item in mic)
            {
                mem_length.Add(item.Length);

                if (max_len < item.Length)
                    max_len = item.Length;

                if (!jnts.Contains(item.StartNode))
                    jnts.Add(item.StartNode);
                if (!jnts.Contains(item.EndNode))
                    jnts.Add(item.EndNode);
            }





            List<int> jn = new List<int>();
            List<double> frcs = new List<double>();
            foreach (var item in jnts)
            {
                jn.Clear();

                jn.Add(item.NodeNo);

                frcs.Add(StructureAnalysis.GetJoint_ShearForce(jn));


            }
            double max_frc = 0.0;
            foreach (var item in frcs)
            {
                if (max_frc < item)
                    max_frc = item;
            }
            doc.ActiveLayOut.Entities.RemoveAll();

            #region Draw Members
            vdPolyline vpl = new vdPolyline();
            vpl.SetUnRegisterDocument(doc);
            vpl.setDocumentDefaults();


            vpl.VertexList.Add(new gPoint());

            gPoint gp = new gPoint();
            vpl.VertexList.Add(gp);




            vdLine vln = new vdLine();

            double Y_Limit = max_len * 0.5;


            #region Draw Verical Line
            //vln = new vdLine();
            vln.SetUnRegisterDocument(doc);
            vln.setDocumentDefaults();


            vln.StartPoint = new gPoint(gp.x, gp.y + Y_Limit);
            vln.EndPoint = new gPoint(gp.x, gp.y - Y_Limit);
            doc.ActiveLayOut.Entities.Add(vln);
            #endregion Draw Verical Line



            vdLine vfrc = new vdLine();
            vfrc.SetUnRegisterDocument(doc);
            vfrc.setDocumentDefaults();





            double fx = Y_Limit / 1.2;

            vdMText vmt = new vdMText();
            vmt.SetUnRegisterDocument(doc);
            vmt.setDocumentDefaults();

            //vfrc.StartPoint = (new gPoint(gp.x, 0 + (frcs[0] / max_frc) * fx));
            //vfrc.VertexList.Add(new gPoint(gp.x, 0 + (frcs[0] / max_frc) * fx));


            //vmt.InsertionPoint = new gPoint(gp.x, 0 + (frcs[0] / max_frc) * fx);
            //vmt.TextString = frcs[0].ToString("f3");

            //vmt.Height = Y_Limit * 0.1;


            doc.ActiveLayOut.Entities.Add(vmt);

            for (i = 0; i < mem_length.Count; i++)
            {
                vfrc = new vdLine();
                vfrc.SetUnRegisterDocument(doc);
                vfrc.setDocumentDefaults();

                vfrc.StartPoint = (new gPoint(gp.x, 0 + (frcs[i] / max_frc) * fx));

                gp = new gPoint(gp.x + mem_length[i], 0);
                vpl.VertexList.Add(gp);


                vfrc.EndPoint = (new gPoint(gp.x, 0 - (frcs[i] / max_frc) * fx));
                doc.ActiveLayOut.Entities.Add(vfrc);




                vmt = new vdMText();
                vmt.SetUnRegisterDocument(doc);
                vmt.setDocumentDefaults();

                //vfrc.VertexList.Add(new gPoint(gp.x, 0 + (frcs[0] / max_frc) * fx));


                vmt.InsertionPoint = vfrc.StartPoint;
                vmt.TextString = (frcs[i]).ToString("f3");
                vmt.Height = Y_Limit * 0.1;
                doc.ActiveLayOut.Entities.Add(vmt);




                //vfrc.VertexList.Add(new gPoint(gp.x, (frcs[i + 1] / max_frc) * fx));

                vmt = new vdMText();
                vmt.SetUnRegisterDocument(doc);
                vmt.setDocumentDefaults();

                //vfrc.VertexList.Add(new gPoint(gp.x, 0 + (frcs[0] / max_frc) * fx));


                vmt.InsertionPoint = vfrc.EndPoint;
                vmt.TextString = (-frcs[i]).ToString("f3");
                vmt.Height = Y_Limit * 0.1;
                doc.ActiveLayOut.Entities.Add(vmt);


                #region Draw Verical Line
                vln = new vdLine();
                vln.SetUnRegisterDocument(doc);
                vln.setDocumentDefaults();


                vln.StartPoint = new gPoint(gp.x, gp.y + Y_Limit);
                vln.EndPoint = new gPoint(gp.x, gp.y - Y_Limit);
                doc.ActiveLayOut.Entities.Add(vln);
                #endregion Draw Verical Line



            }

            //vfrc.SPlineFlag = VectorDraw.Professional.Constants.VdConstSplineFlag.SFlagFITTING;
            doc.ActiveLayOut.Entities.Add(vpl);

            doc.ShowUCSAxis = false;
            doc.Redraw(true);

            if (file_name != "")
                doc.SaveAs(file_name);

            #endregion Draw Members


        }
        public void Draw_Beam_Force_Diagram(double flr_lvl, string beam_no, string file_name, gPoint origin)
        {
            MemberIncidenceCollection mic = new MemberIncidenceCollection();

            int i = 0;
            List<int> mems = new List<int>();
            for (i = 0; i < All_Beam_Data.Count; i++)
            {
                if (All_Beam_Data[i].BeamNo == beam_no && All_Beam_Data[i].FloorLavel == flr_lvl)
                {
                    mems = MyList.Get_Array_Intiger(All_Beam_Data[i].ContinuosMembers);
                    if (mems.Count > 0)
                    {
                        foreach (var item in mems)
                        {
                            mic.Add(AstDoc.Members.Get_Member(item));
                        }
                    }
                    break;
                }
            }

            if (mic.Count == 0) return;
            List<JointCoordinate> jnts = new List<JointCoordinate>();

            List<double> mem_length = new List<double>();
            double max_len = 0.0;

            foreach (var item in mic)
            {
                mem_length.Add(item.Length);

                if (max_len < item.Length)
                    max_len = item.Length;

                if (!jnts.Contains(item.StartNode))
                    jnts.Add(item.StartNode);
                if (!jnts.Contains(item.EndNode))
                    jnts.Add(item.EndNode);
            }





            List<int> jn = new List<int>();
            List<double> frcs = new List<double>();
            foreach (var item in jnts)
            {
                jn.Clear();

                jn.Add(item.NodeNo);

                frcs.Add(StructureAnalysis.GetJoint_ShearForce(jn));


            }
            double max_frc = 0.0;
            foreach (var item in frcs)
            {
                if (max_frc < item)
                    max_frc = item;
            }
            //doc.ActiveLayOut.Entities.RemoveAll();

            #region Draw Members
            vdPolyline vpl = new vdPolyline();
            vpl.SetUnRegisterDocument(doc);
            vpl.setDocumentDefaults();


            //vpl.VertexList.Add(new gPoint(0, origin.y));

            gPoint gp = new gPoint(0, origin.y);
            vpl.VertexList.Add(gp);




            vdLine vln = new vdLine();

            double Y_Limit = max_len * 0.5;





            vdLine vfrc = new vdLine();
            vfrc.SetUnRegisterDocument(doc);
            vfrc.setDocumentDefaults();





            double fx = Y_Limit / 1.2;

            vdMText vmt = new vdMText();
            vmt.SetUnRegisterDocument(doc);
            vmt.setDocumentDefaults();

            //vfrc.StartPoint = (new gPoint(gp.x, 0 + (frcs[0] / max_frc) * fx));
            //vfrc.VertexList.Add(new gPoint(gp.x, 0 + (frcs[0] / max_frc) * fx));


            //vmt.InsertionPoint = new gPoint(gp.x, 0 + (frcs[0] / max_frc) * fx);
            //vmt.TextString = frcs[0].ToString("f3");

            //vmt.Height = Y_Limit * 0.1;


            doc.ActiveLayOut.Entities.Add(vmt);

            #region Draw Verical Line
            //vln = new vdLine();
            vln.SetUnRegisterDocument(doc);
            vln.setDocumentDefaults();


            vln.StartPoint = new gPoint(gp.x, (gp.y + Y_Limit));
            vln.EndPoint = new gPoint(gp.x, (gp.y - Y_Limit));
            doc.ActiveLayOut.Entities.Add(vln);


            vmt = new vdMText();
            vmt.SetUnRegisterDocument(doc);
            vmt.setDocumentDefaults();

            //vfrc.VertexList.Add(new gPoint(gp.x, 0 + (frcs[0] / max_frc) * fx));


            vmt.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter;
            vmt.VerJustify = VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerBottom;

            vmt.InsertionPoint = new gPoint(vln.StartPoint.x, vln.StartPoint.y + 0.5);
            vmt.TextString = "Joint No : " + jnts[0].NodeNo;
            vmt.Height = Y_Limit * 0.1;
            doc.ActiveLayOut.Entities.Add(vmt);



            vmt = new vdMText();
            vmt.SetUnRegisterDocument(doc);
            vmt.setDocumentDefaults();

            //vfrc.VertexList.Add(new gPoint(gp.x, 0 + (frcs[0] / max_frc) * fx));


            vmt.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter;
            vmt.VerJustify = VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerBottom;

            vmt.InsertionPoint = new gPoint(vln.EndPoint.x, vln.EndPoint.y - 0.5);
            vmt.TextString = "Joint No : " + jnts[0].NodeNo;
            vmt.Height = Y_Limit * 0.1;
            doc.ActiveLayOut.Entities.Add(vmt);

            #endregion Draw Verical Line

            for (i = 0; i < mem_length.Count; i++)
            {
                vfrc = new vdLine();
                vfrc.SetUnRegisterDocument(doc);
                vfrc.setDocumentDefaults();

                vfrc.StartPoint = (new gPoint(gp.x, (origin.y) + ((frcs[i] / max_frc) * fx)));

                gp = new gPoint((gp.x + mem_length[i]), (origin.y));
                vpl.VertexList.Add(gp);


                vfrc.EndPoint = (new gPoint(gp.x, (origin.y) - ((frcs[i] / max_frc) * fx)));
                doc.ActiveLayOut.Entities.Add(vfrc);

                #region Member No and Length
                vmt = new vdMText();
                vmt.SetUnRegisterDocument(doc);
                vmt.setDocumentDefaults();

                //vfrc.VertexList.Add(new gPoint(gp.x, 0 + (frcs[0] / max_frc) * fx));

                vmt.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter;
                //vmt.VerJustify = VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerCen;
                vmt.InsertionPoint = new gPoint((gp.x - mem_length[i] / 2), (origin.y - 0.5));
                vmt.TextString = "Member No : " + mic[i].MemberNo + "\n Length = " + mic[i].Length.ToString("f2") + " m.";
                vmt.Height = Y_Limit * 0.1;
                doc.ActiveLayOut.Entities.Add(vmt);

                #endregion Member No and Length



                vmt = new vdMText();
                vmt.SetUnRegisterDocument(doc);
                vmt.setDocumentDefaults();

                //vfrc.VertexList.Add(new gPoint(gp.x, 0 + (frcs[0] / max_frc) * fx));


                vmt.InsertionPoint = vfrc.StartPoint;
                vmt.TextString = (frcs[i]).ToString("f3");
                vmt.Height = Y_Limit * 0.1;
                doc.ActiveLayOut.Entities.Add(vmt);




                //vfrc.VertexList.Add(new gPoint(gp.x, (frcs[i + 1] / max_frc) * fx));

                vmt = new vdMText();
                vmt.SetUnRegisterDocument(doc);
                vmt.setDocumentDefaults();

                //vfrc.VertexList.Add(new gPoint(gp.x, 0 + (frcs[0] / max_frc) * fx));


                vmt.InsertionPoint = vfrc.EndPoint;
                vmt.TextString = (-frcs[i]).ToString("f3");
                vmt.Height = Y_Limit * 0.1;
                doc.ActiveLayOut.Entities.Add(vmt);


                #region Draw Verical Line
                vln = new vdLine();
                vln.SetUnRegisterDocument(doc);
                vln.setDocumentDefaults();


                vln.StartPoint = new gPoint(gp.x, (gp.y + Y_Limit));
                vln.EndPoint = new gPoint(gp.x, (gp.y - Y_Limit));
                doc.ActiveLayOut.Entities.Add(vln);




                vmt = new vdMText();
                vmt.SetUnRegisterDocument(doc);
                vmt.setDocumentDefaults();

                //vfrc.VertexList.Add(new gPoint(gp.x, 0 + (frcs[0] / max_frc) * fx));


                vmt.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter;
                vmt.VerJustify = VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerBottom;

                vmt.InsertionPoint = new gPoint(vln.StartPoint.x, vln.StartPoint.y + 0.5);
                vmt.TextString = "Joint No : " + jnts[i + 1].NodeNo;
                vmt.Height = Y_Limit * 0.1;
                doc.ActiveLayOut.Entities.Add(vmt);



                vmt = new vdMText();
                vmt.SetUnRegisterDocument(doc);
                vmt.setDocumentDefaults();

                //vfrc.VertexList.Add(new gPoint(gp.x, 0 + (frcs[0] / max_frc) * fx));


                vmt.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter;
                vmt.VerJustify = VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerBottom;

                vmt.InsertionPoint = new gPoint(vln.EndPoint.x, vln.EndPoint.y - 0.5);
                vmt.TextString = "Joint No : " + jnts[i + 1].NodeNo;
                vmt.Height = Y_Limit * 0.1;
                doc.ActiveLayOut.Entities.Add(vmt);


                #endregion Draw Verical Line



            }

            //vfrc.SPlineFlag = VectorDraw.Professional.Constants.VdConstSplineFlag.SFlagFITTING;
            doc.ActiveLayOut.Entities.Add(vpl);




            vmt = new vdMText();
            vmt.SetUnRegisterDocument(doc);
            vmt.setDocumentDefaults();


            vmt.VerJustify = VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerCen;
            vmt.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter;
            vmt.InsertionPoint = new gPoint(vpl.Length() / 2, origin.y - Y_Limit - 1.5);

            vmt.TextString = "SHEAR FORCE DIAGRAM \n FLOOR LEVEL = " + flr_lvl.ToString("f3")
                + "m.  and  BEAM = " + beam_no + ", Length = " + vpl.Length().ToString("f2") + " m";

            vmt.Height = Y_Limit * 0.1;


            doc.ActiveLayOut.Entities.Add(vmt);




            doc.ShowUCSAxis = false;
            doc.Redraw(true);

            if (file_name != "")
                doc.SaveAs(file_name);

            #endregion Draw Members


        }


        public void Draw_Beam_Constuction_Table(double flr_lvl, string file_name)
        {
            vdPolyline vpl_top = new vdPolyline();
            vpl_top.SetUnRegisterDocument(doc);
            vpl_top.setDocumentDefaults();


            doc.ActiveLayOut.Entities.RemoveAll();

            double x = 0.0; double y = 0.0;
            double text_height = 1.1;

            double x_off = 1.5;
            double y_off = -1.7;

            //doc.ActiveTextStyle.Height = text_height;
            #region Draw Top Line
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 10;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 10;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 10;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 50;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 10;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 10;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 30;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 30;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 30;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 30;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 30;
            vpl_top.VertexList.Add(new gPoint(x, y));

            doc.ActiveLayOut.Entities.Add(vpl_top);
            #endregion Draw Top Line



            vdPolyline vpl = new vdPolyline();



            int i = 0;
            int j = 0;
            gPoint gp;
            //y = -10;
            y = -8;
            for (i = 0; i <= All_Beam_Construction_Details.Count; i++)
            {
                vpl = new vdPolyline();
                vpl.SetUnRegisterDocument(doc);
                vpl.setDocumentDefaults();
                for (j = 0; j < vpl_top.VertexList.Count; j++)
                {
                    gp = vpl_top.VertexList[j];
                    //if(i == 0)
                    //    vpl.VertexList.Add(new gPoint(gp.x, gp.y + (i + 2) * y));
                    //else
                    vpl.VertexList.Add(new gPoint(gp.x, gp.y + (i + 1) * y));
                }
                doc.ActiveLayOut.Entities.Add(vpl);
            }



            vdLine ln;
            for (j = 0; j < vpl_top.VertexList.Count; j++)
            {
                ln = new vdLine();
                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();

                ln.StartPoint = vpl_top.VertexList[j];
                ln.EndPoint = vpl.VertexList[j];
                doc.ActiveLayOut.Entities.Add(ln);
            }




            vdMText txt;
            //for (j = 0; j < vpl_top.VertexList.Count; j++)
            //{

            #region S.No
            j = 0;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "S.No";
            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion S.No

            #region Floor Level (M).
            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Floor \nLevel \n(M).";
            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion Floor Level (M).

            #region BeamNos
            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Beam Nos.";
            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion BeamNos

            #region Continuous Beams with   Member Nos. / Lengths (M).

            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Continuous Beams with \nMember Nos. / Lengths \n(M).";
            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion Continuous Beams with   Member Nos. / Lengths (M).

            #region Breadth[B] (M).
            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Breadth[B] \n(M).";
            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion Breadth[B] (M)

            #region Depth[D] (M).

            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Depth[D] \n(M).";
            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion Depth[D] (M).

            #region Span Reinf. Bott. Bar Dia & Nos. [Bar Mark BAst]
            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Span Reinf. Bott. \nBar Dia & Nos. \n[Bar Mark BAst]";

            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion Depth[D] (M).

            #region Span Reinf. Top Bar Dia & Nos.[Bar Mark BAst]
            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Span Reinf. Top \nBar Dia & Nos.\n[Bar Mark BAst]";

            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);

            #endregion Span Reinf. Top Bar Dia & Nos.[Bar Mark BAst]

            #region Supp. Reinf. Bott. Bar Dia & Nos. [Bar Mark BAst]

            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Supp. Reinf. Bott. \nBar Dia & Nos. \n[Bar Mark BAst]";

            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion Supp. Reinf. Bott. Bar Dia & Nos. [Bar Mark BAst]

            #region Supp. Reinf. Top Bar Dia & Nos. [Bar Mark BAst]
            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Supp. Reinf. Top \nBar Dia & Nos. \n[Bar Mark BAst]";

            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion Supp. Reinf. Top Bar Dia & Nos. [Bar Mark BAst]

            #region Stirrups Bar Dia & Spacing [Bar Mark BAst]
            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Stirrups \nBar Dia & Spacing \n[Bar Mark BAst]";

            txt.Height = text_height;
            #endregion Stirrups Bar Dia & Spacing [Bar Mark BAst]


            doc.ActiveLayOut.Entities.Add(txt);


            #region Print Beam Contruction
            for (i = 0; i < All_Beam_Construction_Details.Count; i++)
            {
                var item = All_Beam_Construction_Details[i];
                item.S_No = i + 1;
                #region S.No
                j = 0;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = item.S_No.ToString();
                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);
                #endregion S.No


                #region Floor Level (M).
                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = item.Floor_Level.ToString("f3");
                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);
                #endregion Floor Level (M).

                #region BeamNos
                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = item.Beam_Nos;
                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);
                #endregion BeamNos

                #region Continuous Beams with   Member Nos. / Lengths (M).

                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];


                List<int> mems = MyList.Get_Array_Intiger(item.Continuous_Beam_Nos);



                foreach (var m in mems)
                {

                    item.Continuous_Beam_Lengths = item.Continuous_Beam_Lengths +
                        AstDoc.Members.Get_Member(m).Length.ToString("f2") + ",";
                }


                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                //txt.TextString = "Continuous Beams with \nMember Nos. / Lengths \n(M).";
                txt.TextString = "Member Nos: " + item.Continuous_Beam_Nos + "\nLengths : " + item.Continuous_Beam_Lengths;
                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);
                #endregion Continuous Beams with   Member Nos. / Lengths (M).

                #region Breadth[B] (M).
                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = item.Breadth.ToString("f3");
                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);
                #endregion Breadth[B] (M)

                #region Depth[D] (M).

                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = item.Depth.ToString("f3");
                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);
                #endregion Depth[D] (M).

                #region Span Reinf. Bott. Bar Dia & Nos. [Bar Mark BAst]
                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                //txt.TextString = "Span Reinf. Bott. \nBar Dia & Nos. \n[Bar Mark BAst]";
                txt.TextString = string.Format("{0} nos {1} mm dia\nB_Ast[1] = {2:f3} Sq.mm", item.Bar_nos_1, item.Bar_dia_1, item.BAst_1);

                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);
                #endregion Depth[D] (M).

                #region Span Reinf. Top Bar Dia & Nos.[Bar Mark BAst]
                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                //txt.TextString = "Span Reinf. Bott. \nBar Dia & Nos. \n[Bar Mark BAst]";
                txt.TextString = string.Format("{0} nos {1} mm dia\nB_Ast[2] = {2:f3} Sq.mm",
                    item.Bar_nos_2, item.Bar_dia_2,
                    item.BAst_2 = item.Bar_nos_2 * Math.PI * Math.Pow(item.Bar_dia_2, 2) / 4.0);

                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);

                #endregion Span Reinf. Top Bar Dia & Nos.[Bar Mark BAst]

                #region Supp. Reinf. Bott. Bar Dia & Nos. [Bar Mark BAst]

                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                //txt.TextString = "Supp. Reinf. Bott. \nBar Dia & Nos. \n[Bar Mark BAst]";
                txt.TextString = string.Format("{0} nos {1} mm dia\n{2} nos {3} mm dia\nB_Ast[3] = {4:f3} Sq.mm", item.Bar_nos_3_1, item.Bar_dia_3_1, item.Bar_nos_3_2, item.Bar_dia_3_2, item.BAst_3);


                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);
                #endregion Supp. Reinf. Bott. Bar Dia & Nos. [Bar Mark BAst]

                #region Supp. Reinf. Top Bar Dia & Nos. [Bar Mark BAst]
                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];


                //txt.TextString = "Supp. Reinf. Top \nBar Dia & Nos. \n[Bar Mark BAst]";

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = string.Format("{0} nos {1} mm dia\nB_Ast[4] = {2:f3} Sq.mm",
                    item.Bar_nos_4, item.Bar_dia_4,
                    item.BAst_4 = item.Bar_nos_4 * Math.PI * Math.Pow(item.Bar_dia_4, 2) / 4.0);

                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);
                #endregion Supp. Reinf. Top Bar Dia & Nos. [Bar Mark BAst]

                #region Stirrups Bar Dia & Spacing [Bar Mark BAst]
                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                //txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
                //txt.TextString = "Stirrups \nBar Dia & Spacing \n[Bar Mark BAst]";

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = string.Format("{0} mm dia @{1}mm c/c\nB_Ast[5] = {2:f3} Sq.mm", item.Bar_dia_5, item.Bar_Spacing_5, item.BAst_5);

                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);
                #endregion Stirrups Bar Dia & Spacing [Bar Mark BAst]

            }

            #endregion

            doc.ShowUCSAxis = false;
            doc.Redraw(true);
            if (file_name != "")
                doc.SaveAs(file_name);


        }


        public void Draw_Column_Constuction_Table(double flr_lvl, string file_name)
        {

            vdPolyline vpl_top = new vdPolyline();
            vpl_top.SetUnRegisterDocument(doc);
            vpl_top.setDocumentDefaults();


            doc.ActiveLayOut.Entities.RemoveAll();

            double x = 0.0; double y = 0.0;
            double text_height = 1.1;

            double x_off = 1.5;
            double y_off = -1.7;

            //doc.ActiveTextStyle.Height = text_height;
            #region Draw Top Line
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 10;
            //vpl_top.VertexList.Add(new gPoint(x, y)); x += 10;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 10;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 50;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 10;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 10;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 30;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 30;
            //vpl_top.VertexList.Add(new gPoint(x, y)); x += 30;
            //vpl_top.VertexList.Add(new gPoint(x, y)); x += 30;
            //vpl_top.VertexList.Add(new gPoint(x, y)); x += 30;
            vpl_top.VertexList.Add(new gPoint(x, y));

            doc.ActiveLayOut.Entities.Add(vpl_top);
            #endregion Draw Top Line


            vdPolyline vpl = new vdPolyline();


            int i = 0;
            int j = 0;
            gPoint gp;
            //y = -10;
            y = -8;
            for (i = 0; i <= All_Column_Construction_Details.Count; i++)
            {
                vpl = new vdPolyline();
                vpl.SetUnRegisterDocument(doc);
                vpl.setDocumentDefaults();
                for (j = 0; j < vpl_top.VertexList.Count; j++)
                {
                    gp = vpl_top.VertexList[j];
                    //if(i == 0)
                    //    vpl.VertexList.Add(new gPoint(gp.x, gp.y + (i + 2) * y));
                    //else
                    vpl.VertexList.Add(new gPoint(gp.x, gp.y + (i + 1) * y));
                }
                doc.ActiveLayOut.Entities.Add(vpl);
            }



            vdLine ln;
            for (j = 0; j < vpl_top.VertexList.Count; j++)
            {
                ln = new vdLine();
                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();

                ln.StartPoint = vpl_top.VertexList[j];
                ln.EndPoint = vpl.VertexList[j];
                doc.ActiveLayOut.Entities.Add(ln);
            }




            vdMText txt;
            //for (j = 0; j < vpl_top.VertexList.Count; j++)
            //{

            #region S.No
            j = 0;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "S.No";
            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion S.No

            #region Floor Level (M).
            //j++;
            //txt = new vdMText();
            //txt.SetUnRegisterDocument(doc);
            //txt.setDocumentDefaults();

            //gp = vpl_top.VertexList[j];

            //txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            //txt.TextString = "Floor \nLevel \n(M).";
            //txt.Height = text_height;
            //doc.ActiveLayOut.Entities.Add(txt);
            #endregion Floor Level (M).

            #region Column Nos
            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Column Nos.";
            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion BeamNos

            #region Continuous Columns with   Member Nos. / Lengths (M).

            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Continuous Columns with \nMember Nos. / Lengths \n(M).";
            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion Continuous Beams with   Member Nos. / Lengths (M).

            #region Breadth[B] (M).
            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Breadth[B] \n(M).";
            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion Breadth[B] (M)

            #region Depth[D] (M).

            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Depth[D] \n(M).";
            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion Depth[D] (M).

            #region Main Reinf. Bars  Bar Dia & Nos. [Bar Mark BAst]
            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Main Reinf. Bars \nBar Dia & Nos. \n[Bar Mark CAst]";

            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion Depth[D] (M).

            #region Lateral Ties \nBar Dia & Spacing.\n[Bar Mark CAst]
            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Lateral Ties \nBar Dia & Spacing.\n[Bar Mark CAst]";

            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);

            #endregion Span Reinf. Top Bar Dia & Nos.[Bar Mark BAst]

            //doc.ActiveLayOut.Entities.Add(txt);

            #region Print Beam Contruction
            for (i = 0; i < All_Column_Construction_Details.Count; i++)
            {
                var item = All_Column_Construction_Details[i];
                item.S_No = i + 1;
                #region S.No
                j = 0;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = item.S_No.ToString();
                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);
                #endregion S.No


                #region Floor Level (M).
                //j++;
                //txt = new vdMText();
                //txt.SetUnRegisterDocument(doc);
                //txt.setDocumentDefaults();

                //gp = vpl_top.VertexList[j];

                //txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                //txt.TextString = item.Floor_Level.ToString("f3");
                //txt.Height = text_height;
                //doc.ActiveLayOut.Entities.Add(txt);
                #endregion Floor Level (M).

                #region Column Nos
                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = item.Column_Nos;
                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);
                #endregion BeamNos

                #region Continuous Beams with   Member Nos. / Lengths (M).

                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                List<int> mems = MyList.Get_Array_Intiger(item.Continuous_Column_Nos);
                string flvl = "";
                foreach (var m in mems)
                {
                    MemberIncidence mmi = AstDoc.Members.Get_Member(m);
                    item.Continuous_Column_Lengths = item.Continuous_Column_Lengths +
                        mmi.Length.ToString("f2") + ",";


                    flvl += mmi.EndNode.Y.ToString("f2") + ",";
                }


                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                //txt.TextString = "Continuous Beams with \nMember Nos. / Lengths \n(M).";
                txt.TextString = "Member Nos: " + item.Continuous_Column_Nos + "\nLengths : "
                    + item.Continuous_Column_Lengths
                     + "\nFloor Levels : " + flvl;
                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);
                #endregion Continuous Beams with   Member Nos. / Lengths (M).

                #region Breadth[B] (M).
                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = item.Breadth.ToString();
                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);
                #endregion Breadth[B] (M)

                #region Depth[D] (M).

                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = item.Depth.ToString();
                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);
                #endregion Depth[D] (M).

                #region Span Reinf. Bott. Bar Dia & Nos. [Bar Mark BAst]
                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                //txt.TextString = "Span Reinf. Bott. \nBar Dia & Nos. \n[Bar Mark BAst]";
                txt.TextString = string.Format("{0} nos {1} mm dia\nC_Ast[1] = {2:f3} Sq.mm", item.Bar_nos_1, item.Bar_dia_1, item.CAst_1);

                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);
                #endregion Depth[D] (M).

                #region Stirrups Bar Dia & Spacing [Bar Mark BAst]
                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                //txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
                //txt.TextString = "Stirrups \nBar Dia & Spacing \n[Bar Mark BAst]";

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = string.Format("{0} mm dia @{1}mm c/c\nC_Ast[2] = {2:f3} Sq.mm", item.Bar_dia_2, item.Bar_spacing_2, item.CAst_2);

                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);
                #endregion Stirrups Bar Dia & Spacing [Bar Mark BAst]

            }

            #endregion

            doc.ShowUCSAxis = false;
            doc.Redraw(true);
            if (file_name != "")
                doc.SaveAs(file_name);
        }


        public void Draw_Foundation_Constuction_Table(double flr_lvl, string file_name)
        {

            vdPolyline vpl_top = new vdPolyline();
            vpl_top.SetUnRegisterDocument(doc);
            vpl_top.setDocumentDefaults();


            doc.ActiveLayOut.Entities.RemoveAll();

            double x = 0.0; double y = 0.0;
            double text_height = 1.1;

            double x_off = 1.5;
            double y_off = -1.7;

            //doc.ActiveTextStyle.Height = text_height;
            #region Draw Top Line
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 10;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 10;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 10;
            //vpl_top.VertexList.Add(new gPoint(x, y)); x += 50;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 10;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 10;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 10;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 10;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 10;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 10;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 30;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 30;
            //vpl_top.VertexList.Add(new gPoint(x, y)); x += 30;
            //vpl_top.VertexList.Add(new gPoint(x, y)); x += 30;
            //vpl_top.VertexList.Add(new gPoint(x, y)); x += 30;
            vpl_top.VertexList.Add(new gPoint(x, y));

            doc.ActiveLayOut.Entities.Add(vpl_top);
            #endregion Draw Top Line



            vdPolyline vpl = new vdPolyline();



            int i = 0;
            int j = 0;
            gPoint gp;
            //y = -10;
            y = -8;
            for (i = 0; i <= All_Foundation_Construction_Details.Count; i++)
            {
                vpl = new vdPolyline();
                vpl.SetUnRegisterDocument(doc);
                vpl.setDocumentDefaults();
                for (j = 0; j < vpl_top.VertexList.Count; j++)
                {
                    gp = vpl_top.VertexList[j];
                    //if(i == 0)
                    //    vpl.VertexList.Add(new gPoint(gp.x, gp.y + (i + 2) * y));
                    //else
                    vpl.VertexList.Add(new gPoint(gp.x, gp.y + (i + 1) * y));
                }
                doc.ActiveLayOut.Entities.Add(vpl);
            }



            vdLine ln;
            for (j = 0; j < vpl_top.VertexList.Count; j++)
            {
                ln = new vdLine();
                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();

                ln.StartPoint = vpl_top.VertexList[j];
                ln.EndPoint = vpl.VertexList[j];
                doc.ActiveLayOut.Entities.Add(ln);
            }




            vdMText txt;
            //for (j = 0; j < vpl_top.VertexList.Count; j++)
            //{

            #region S.No
            j = 0;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "S.No";
            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion S.No

            #region Floor Level (M).
            //j++;
            //txt = new vdMText();
            //txt.SetUnRegisterDocument(doc);
            //txt.setDocumentDefaults();

            //gp = vpl_top.VertexList[j];

            //txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            //txt.TextString = "Floor \nLevel \n(M).";
            //txt.Height = text_height;
            //doc.ActiveLayOut.Entities.Add(txt);
            #endregion Floor Level (M).

            #region Column Nos
            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Elevation\n(M)";
            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion BeamNos

            #region Column Nos
            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Associated\nColumn\nNos.";
            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion BeamNos


            #region Continuous Columns with   Member Nos. / Lengths (M).

            //j++;
            //txt = new vdMText();
            //txt.SetUnRegisterDocument(doc);
            //txt.setDocumentDefaults();

            //gp = vpl_top.VertexList[j];

            //txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            //txt.TextString = "Continuous Columns with \nMember Nos. / Lengths \n(M).";
            //txt.Height = text_height;
            //doc.ActiveLayOut.Entities.Add(txt);
            #endregion Continuous Beams with   Member Nos. / Lengths (M).

            #region Footing \nLength
            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Footing \nLength[L] \n(M).";
            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion Breadth[B] (M)

            #region Footing Breadth[D] (M).

            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Footing \nBreadth[B] \n(M).";
            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion Depth[D] (M).



            #region Pedestal nBreadth
            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Pedestal \nBreadth [P1] \n(M).";
            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion Breadth[B] (M)

            #region Pedestal Depth
            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Pedestal \nDepth [P2] \n(M).";
            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion Breadth[B] (M)




            #region Column nBreadth
            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Column \nBreadth [C1] \n(M).";
            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion Breadth[B] (M)

            #region Column Depth
            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Column \nDepth [C2] \n(M).";
            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion Breadth[B] (M)



            #region Main Reinf. Bars  Bar Dia & Nos. [Bar Mark BAst]
            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Main Reinf. Bars \nBar Dia & Nos. \n[Bar Mark CAst]";

            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion Depth[D] (M).

            #region Lateral Ties \nBar Dia & Spacing.\n[Bar Mark CAst]
            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Transverse Reinf. Bars \nBar Dia & Spacing.\n[Bar Mark FAst]";

            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);

            #endregion Span Reinf. Top Bar Dia & Nos.[Bar Mark BAst]


            //doc.ActiveLayOut.Entities.Add(txt);


            #region Print Beam Contruction
            for (i = 0; i < All_Foundation_Construction_Details.Count; i++)
            {
                var item = All_Foundation_Construction_Details[i];
                item.S_No = i + 1;
                #region S.No
                j = 0;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = item.S_No.ToString();
                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);
                #endregion S.No


                #region Floor Level (M).
                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = item.Floor_Level.ToString("f3");
                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);
                #endregion Floor Level (M).

                #region Column Nos
                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = item.Column_Nos.Replace("F", "");
                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);
                #endregion BeamNos

                #region Continuous Beams with   Member Nos. / Lengths (M).

                //j++;
                //txt = new vdMText();
                //txt.SetUnRegisterDocument(doc);
                //txt.setDocumentDefaults();

                //gp = vpl_top.VertexList[j];

                //List<int> mems = MyList.Get_Array_Intiger(item.Continuous_Column_Nos);
                //string flvl = "";
                //foreach (var m in mems)
                //{
                //    MemberIncidence mmi = AstDoc.Members.Get_Member(m);
                //    item.Continuous_Column_Lengths = item.Continuous_Column_Lengths +
                //        mmi.Length.ToString("f2") + ",";


                //    flvl += mmi.EndNode.Y.ToString("f2") + ",";
                //}


                //txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                ////txt.TextString = "Continuous Beams with \nMember Nos. / Lengths \n(M).";
                //txt.TextString = "Member Nos: " + item.Continuous_Column_Nos + "\nLengths : "
                //    + item.Continuous_Column_Lengths
                //     + "\nFloor Levels : " + flvl;
                //txt.Height = text_height;
                //doc.ActiveLayOut.Entities.Add(txt);
                #endregion Continuous Beams with   Member Nos. / Lengths (M).

                #region Footing_Length
                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = item.Footing_Length.ToString();
                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);
                #endregion Breadth[B] (M)

                #region Footing_Breadth.

                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = item.Footing_Breadth.ToString();
                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);
                #endregion Depth[D] (M).




                #region Pedestal_Breadth
                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = item.Pedestal_Breadth.ToString();
                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);
                #endregion Breadth[B] (M)

                #region Pedestal_Depth.

                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = item.Pedestal_Depth.ToString();
                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);
                #endregion Depth[D] (M).




                #region Column_Breadth
                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = item.Column_Breadth.ToString();
                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);
                #endregion Breadth[B] (M)

                #region Column_Depth.

                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = item.Column_Depth.ToString();
                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);
                #endregion Depth[D] (M).




                #region Span Reinf. Bott. Bar Dia & Nos. [Bar Mark BAst]
                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                //txt.TextString = "Span Reinf. Bott. \nBar Dia & Nos. \n[Bar Mark BAst]";
                txt.TextString = string.Format("{0} mm dia @{1}mm c/c\nF_Ast[1] = {2:f3} Sq.mm", item.Bar_dia_1, item.Bar_spacing_1, item.FAst_1);
                //txt.TextString = string.Format("{0} nos {1} mm dia\nF_Ast[1] = {2:f3} Sq.mm", item.Bar_dia_1, item.Bar_spacing_1, item.FAst_1);

                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);
                #endregion Depth[D] (M).

                #region Stirrups Bar Dia & Spacing [Bar Mark BAst]
                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                //txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
                //txt.TextString = "Stirrups \nBar Dia & Spacing \n[Bar Mark BAst]";

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = string.Format("{0} mm dia @{1}mm c/c\nF_Ast[2] = {2:f3} Sq.mm", item.Bar_dia_2, item.Bar_spacing_2, item.FAst_2);

                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);
                #endregion Stirrups Bar Dia & Spacing [Bar Mark BAst]

            }

            #endregion

            doc.ShowUCSAxis = false;
            doc.Redraw(true);
            if (file_name != "")
                doc.SaveAs(file_name);


        }

        public void Draw_SLab_Constuction_Table(double flr_lvl, string file_name)
        {


            vdPolyline vpl_top = new vdPolyline();
            vpl_top.SetUnRegisterDocument(doc);
            vpl_top.setDocumentDefaults();


            doc.ActiveLayOut.Entities.RemoveAll();

            double x = 0.0; double y = 0.0;
            double text_height = 1.1;

            double x_off = 1.5;
            double y_off = -1.7;

            //doc.ActiveTextStyle.Height = text_height;
            #region Draw Top Line
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 10;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 10;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 50;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 30;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 30;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 30;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 30;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 30;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 30;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 30;
            vpl_top.VertexList.Add(new gPoint(x, y)); x += 30;
            vpl_top.VertexList.Add(new gPoint(x, y));

            doc.ActiveLayOut.Entities.Add(vpl_top);
            #endregion Draw Top Line

            vdPolyline vpl = new vdPolyline();



            int i = 0;
            int j = 0;
            gPoint gp;
            //y = -10;
            y = -10;
            for (i = 0; i <= All_Slab_Construction_Details.Count; i++)
            {
                vpl = new vdPolyline();
                vpl.SetUnRegisterDocument(doc);
                vpl.setDocumentDefaults();
                for (j = 0; j < vpl_top.VertexList.Count; j++)
                {
                    gp = vpl_top.VertexList[j];
                    //if(i == 0)
                    //    vpl.VertexList.Add(new gPoint(gp.x, gp.y + (i + 2) * y));
                    //else
                    vpl.VertexList.Add(new gPoint(gp.x, gp.y + (i + 1) * y));
                }
                doc.ActiveLayOut.Entities.Add(vpl);
            }



            vdLine ln;
            for (j = 0; j < vpl_top.VertexList.Count; j++)
            {
                ln = new vdLine();
                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();

                ln.StartPoint = vpl_top.VertexList[j];
                ln.EndPoint = vpl.VertexList[j];
                doc.ActiveLayOut.Entities.Add(ln);
            }




            vdMText txt;
            //for (j = 0; j < vpl_top.VertexList.Count; j++)
            //{

            #region S.No
            j = 0;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "S.No";
            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion S.No

            #region Floor Level (M).
            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Floor \nLevel \n(M).";
            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion Floor Level (M).


            #region Slab surrounded with Beams \nMember Nos. / Lengths (M).\n(M).
            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];



            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Slab surrounded with Beams \nMember Nos. / Lengths (M).\n(M).";
            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion Slab surrounded with Beams \nMember Nos. / Lengths (M).\n(M).


            #region Shorter Direction (Mid Span, at Bottom),
            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];



            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Shorter Direction \n(Mid Span, at Bottom),\nBar Dia & Spacing.\n[Bar Mark BAst]";
            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion Shorter Direction (Mid Span, at Bottom),


            #region Longer Direction (Mid Span, at Bottom)
            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];



            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Longer Direction \n(Mid Span, at Bottom),\nBar Dia & Spacing.\n[Bar Mark BAst]";
            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion Longer Direction (Mid Span, at Bottom),

            #region Shorter Direction (Continuous Edge, at Top)
            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];



            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Shorter Direction \n(Continuous Edge, at Top),\nBar Dia & Spacing.\n[Bar Mark BAst]";
            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion Shorter Direction (Continuous Edge, at Top)

            #region Shorter Direction (Discontinuous Edge, at Top)
            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];



            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Shorter Direction \n(Discontinuous Edge, at Top),\nBar Dia & Spacing.\n[Bar Mark BAst]";
            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion Shorter Direction (Discontinuous Edge, at Top)


            #region Longer Direction (Continuous Edge Edge, at Top)
            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];



            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Longer Direction \n(Continuous Edge Edge, at Top),\nBar Dia & Spacing.\n[Bar Mark BAst]";
            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion Longer Direction (Discontinuous Edge, at Top)


            #region Longer Direction (Discontinuous Edge, at Top)
            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];



            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Longer Direction \n(Discontinuous Edge, at Top),\nBar Dia & Spacing.\n[Bar Mark BAst]";
            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion Longer Direction (Discontinuous Edge, at Top)


            #region Distribution Steel in Longer Direction at Bottom,
            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];

            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Distribution Steel \n(in Longer Direction at Bottom),\nBar Dia & Spacing.\n[Bar Mark BAst]";
            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion Distribution Steel in Longer Direction at Bottom,

            #region Corner Reinforcements (Top & Bottom)
            j++;
            txt = new vdMText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();

            gp = vpl_top.VertexList[j];


            txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
            txt.TextString = "Corner Reinforcements\n(Top & Bottom),\nBar Dia & Spacing.\n[Bar Mark BAst]";
            txt.Height = text_height;
            doc.ActiveLayOut.Entities.Add(txt);
            #endregion Corner Reinforcements (Top & Bottom)

            //doc.ActiveLayOut.Entities.Add(txt);


            #region Print Beam Contruction
            for (i = 0; i < All_Slab_Construction_Details.Count; i++)
            {
                var item = All_Slab_Construction_Details[i];
                item.S_No = i + 1;
                #region S.No
                j = 0;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = item.S_No.ToString();
                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);
                #endregion S.No


                #region Floor Level (M).
                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = item.Floor_Level.ToString("f3");
                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);
                #endregion Floor Level (M).

                #region Continuous Beams with   Member Nos. / Lengths (M).

                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];


                List<int> mems = MyList.Get_Array_Intiger(item.Continuous_Beam_Nos);



                foreach (var m in mems)
                {

                    item.Continuous_Beam_Lengths = item.Continuous_Beam_Lengths +
                        AstDoc.Members.Get_Member(m).Length.ToString("f2") + ",";
                }


                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                //txt.TextString = "Continuous Beams with \nMember Nos. / Lengths \n(M).";
                txt.TextString = "Member Nos: " + item.Continuous_Beam_Nos + "\nLengths : " + item.Continuous_Beam_Lengths;
                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);
                #endregion Continuous Beams with   Member Nos. / Lengths (M).

                #region S_AST_1

                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                //txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
                //txt.TextString = "Stirrups \nBar Dia & Spacing \n[Bar Mark BAst]";

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = string.Format("{0} mm dia @{1}mm c/c\nS_Ast[1] = {2:f3} Sq.mm", item.Bar_dia_1, item.Bar_spc_1, item.SAst_1);

                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);

                #endregion S_AST_1

                #region S_AST_2

                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                //txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
                //txt.TextString = "Stirrups \nBar Dia & Spacing \n[Bar Mark BAst]";

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = string.Format("{0} mm dia @{1}mm c/c\nS_Ast[2] = {2:f3} Sq.mm",
                    item.Bar_dia_2, item.Bar_spc_2, item.SAst_2);

                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);

                #endregion S_AST_2


                #region S_AST_3

                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                //txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
                //txt.TextString = "Stirrups \nBar Dia & Spacing \n[Bar Mark BAst]";

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = string.Format("{0} mm dia @{1}mm c/c\nS_Ast[3] = {2:f3} Sq.mm",
                    item.Bar_dia_3, item.Bar_spc_3, item.SAst_3);

                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);

                #endregion S_AST_2


                #region S_AST_4

                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                //txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
                //txt.TextString = "Stirrups \nBar Dia & Spacing \n[Bar Mark BAst]";

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = string.Format("{0} mm dia @{1}mm c/c\nS_Ast[4] = {2:f3} Sq.mm",
                    item.Bar_dia_4, item.Bar_spc_4, item.SAst_4);

                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);

                #endregion S_AST_2

                #region S_AST_5

                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                //txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
                //txt.TextString = "Stirrups \nBar Dia & Spacing \n[Bar Mark BAst]";

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = string.Format("{0} mm dia @{1}mm c/c\nS_Ast[5] = {2:f3} Sq.mm",
                    item.Bar_dia_5, item.Bar_spc_5, item.SAst_5);

                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);

                #endregion S_AST_2

                #region S_AST_6

                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                //txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
                //txt.TextString = "Stirrups \nBar Dia & Spacing \n[Bar Mark BAst]";

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = string.Format("{0} mm dia @{1}mm c/c\nS_Ast[6] = {2:f3} Sq.mm",
                    item.Bar_dia_6, item.Bar_spc_6, item.SAst_6);

                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);

                #endregion S_AST_2

                #region S_AST_7

                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                //txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
                //txt.TextString = "Stirrups \nBar Dia & Spacing \n[Bar Mark BAst]";

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = string.Format("{0} mm dia @{1}mm c/c\nS_Ast[7] = {2:f3} Sq.mm",
                    item.Bar_dia_7, item.Bar_spc_7, item.SAst_7);

                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);

                #endregion S_AST_2

                #region S_AST_8

                j++;
                txt = new vdMText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();

                gp = vpl_top.VertexList[j];

                //txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + y_off);
                //txt.TextString = "Stirrups \nBar Dia & Spacing \n[Bar Mark BAst]";

                txt.InsertionPoint = new gPoint(gp.x + x_off, gp.y + ((i + 1) * y + y_off));
                txt.TextString = string.Format("{0} mm dia @{1}mm c/c\nS_Ast[8] = {2:f3} Sq.mm",
                    item.Bar_dia_8, item.Bar_spc_8, item.SAst_8);

                txt.Height = text_height;
                doc.ActiveLayOut.Entities.Add(txt);

                #endregion S_AST_2

            }

            #endregion

            doc.ShowUCSAxis = false;
            doc.Redraw(true);
            if (file_name != "")
                doc.SaveAs(file_name);


        }

    }
    public class FloorLayoutDrawing
    {
        public vdDocument doc { get; set; }
        public ASTRADoc aDoc { get; set; }
        public StructureDrawing StrDrawing { get; set; }
        public FloorLayoutDrawing(ASTRADoc ADoc, vdDocument basedoc)
        {
            aDoc = ADoc;
            doc = basedoc;
        }

        public void Draw_Floor_Layout(double flr_lvl, string file_name)
        {
            MemberIncidenceCollection beams = new MemberIncidenceCollection();
            MemberIncidenceCollection columns = new MemberIncidenceCollection();

            for (int i = 0; i < aDoc.Members.Count; i++)
            {
                var item = aDoc.Members[i];

                if (item.StartNode.Y == flr_lvl || item.EndNode.Y == flr_lvl)
                {
                    if (item.StartNode.Y == item.EndNode.Y)
                    {
                        beams.Add(item);
                    }
                    else
                    {
                        columns.Add(item);
                    }
                }
            }



            vdLine ln = new vdLine();

            doc.ActiveLayOut.Entities.RemoveAll();




            vdMText vtxt = null;
            vdCircle vcle = null;
            vdLine ln2 = null;


            foreach (var item in beams)
            {
                ln = new vdLine();

                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();


                ln.StartPoint.x = item.StartNode.X;
                ln.StartPoint.y = item.StartNode.Z;

                ln.EndPoint.x = item.EndNode.X;
                ln.EndPoint.y = item.EndNode.Z;

                doc.ActiveLayOut.Entities.Add(ln);


                vtxt = new vdMText();

                vtxt.SetUnRegisterDocument(doc);
                vtxt.setDocumentDefaults();

                vtxt.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter;
                vtxt.VerJustify = VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerCen;
                vtxt.InsertionPoint = (ln.StartPoint + ln.EndPoint) / 2;

                if (item.Direction == eDirection.X_Direction)
                    vtxt.InsertionPoint.y = vtxt.InsertionPoint.y + 0.5;
                else
                    vtxt.InsertionPoint.x = vtxt.InsertionPoint.x + 0.5;

                vtxt.Height = 0.2;


                //vtxt.TextString = "B1";
                vtxt.TextString = StrDrawing.Get_Beam_Data(item.MemberNo);

                doc.ActiveLayOut.Entities.Add(vtxt);



                vcle = new vdCircle();

                vcle.SetUnRegisterDocument(doc);
                vcle.setDocumentDefaults();

                vcle.Center = vtxt.InsertionPoint;
                vcle.Radius = 0.2;

                //doc.ActiveLayOut.Entities.Add(vcle);



                ln2 = new vdLine();

                ln2.SetUnRegisterDocument(doc);
                ln2.setDocumentDefaults();


                ln2.StartPoint = (ln.StartPoint + ln.EndPoint) / 2;
                ln2.EndPoint = vtxt.InsertionPoint;
                //ln2.StartPoint = (ln.StartPoint + ln.EndPoint) / 2;

                if (item.Direction == eDirection.X_Direction)
                    ln2.EndPoint.y = ln2.EndPoint.y - vcle.Radius;

                else
                    ln2.EndPoint.x = ln2.EndPoint.x - vcle.Radius;


                doc.ActiveLayOut.Entities.Add(ln2);



            }

            vdPolyline pln = null;

            double foot_B = 2.8;
            double foot_D = 2.8;
            foreach (var item in columns)
            {
                pln = new vdPolyline();

                pln.SetUnRegisterDocument(doc);
                pln.setDocumentDefaults();

                if (flr_lvl != 0)
                {

                    pln.VertexList.Add(new gPoint(item.StartNode.X - item.Property.YD / 2, item.StartNode.Z + item.Property.ZD / 2));
                    pln.VertexList.Add(new gPoint(item.StartNode.X + item.Property.YD / 2, item.StartNode.Z + item.Property.ZD / 2));
                    pln.VertexList.Add(new gPoint(item.StartNode.X + item.Property.YD / 2, item.StartNode.Z - item.Property.ZD / 2));
                    pln.VertexList.Add(new gPoint(item.StartNode.X - item.Property.YD / 2, item.StartNode.Z - item.Property.ZD / 2));
                    pln.VertexList.Add(new gPoint(item.StartNode.X - item.Property.YD / 2, item.StartNode.Z + item.Property.ZD / 2));



                    vtxt = new vdMText();

                    vtxt.SetUnRegisterDocument(doc);
                    vtxt.setDocumentDefaults();

                    vtxt.HorJustify = VectorDraw.Professional.Constants.VdConstHorJust.VdTextHorCenter;
                    vtxt.VerJustify = VectorDraw.Professional.Constants.VdConstVerJust.VdTextVerCen;
                    vtxt.InsertionPoint = new gPoint(item.StartNode.X, item.StartNode.Z);

                    //if (item.Direction == eDirection.X_Direction)
                    //    vtxt.InsertionPoint.y = vtxt.InsertionPoint.y + 0.5;
                    //else
                    //vtxt.InsertionPoint.x = vtxt.InsertionPoint.x + item.Property.YD / 2;

                    vtxt.Height = 0.15;


                    //vtxt.TextString = "C1";
                    vtxt.TextString = StrDrawing.Get_Column_Data(item.MemberNo);

                    doc.ActiveLayOut.Entities.Add(vtxt);



                }
                else
                {

                    pln.VertexList.Add(new gPoint(item.StartNode.X - foot_B / 2, item.StartNode.Z + foot_D / 2));
                    pln.VertexList.Add(new gPoint(item.StartNode.X + foot_B / 2, item.StartNode.Z + foot_D / 2));
                    pln.VertexList.Add(new gPoint(item.StartNode.X + foot_B / 2, item.StartNode.Z - foot_D / 2));
                    pln.VertexList.Add(new gPoint(item.StartNode.X - foot_B / 2, item.StartNode.Z - foot_D / 2));
                    pln.VertexList.Add(new gPoint(item.StartNode.X - foot_B / 2, item.StartNode.Z + foot_D / 2));




                    ln = new vdLine();

                    ln.SetUnRegisterDocument(doc);
                    ln.setDocumentDefaults();


                    ln.StartPoint = (new gPoint(item.StartNode.X - foot_B / 2, item.StartNode.Z + foot_D / 2));
                    ln.EndPoint = new gPoint(item.StartNode.X + foot_B / 2, item.StartNode.Z - foot_D / 2);

                    doc.ActiveLayOut.Entities.Add(ln);



                    ln = new vdLine();

                    ln.SetUnRegisterDocument(doc);
                    ln.setDocumentDefaults();


                    ln.StartPoint = (new gPoint(item.StartNode.X + foot_B / 2, item.StartNode.Z + foot_D / 2));
                    ln.EndPoint = (new gPoint(item.StartNode.X - foot_B / 2, item.StartNode.Z - foot_D / 2));

                    doc.ActiveLayOut.Entities.Add(ln);

                }
                doc.ActiveLayOut.Entities.Add(pln);
            }


            doc.Redraw(true);

            if (file_name != "")
                doc.SaveAs(file_name);

        }

    }
    public class BeamDwg
    {
        public string BeamNo { get; set; }
        public double FloorLavel { get; set; }
        public string ContinuosMembers { get; set; }
        public BeamDwg()
        {
            BeamNo = "";
            FloorLavel = 0.0;
            ContinuosMembers = "";
        }

    }
    public class ColumnDwg
    {
        public string ColumnNo { get; set; }
        //public double FloorLavel { get; set; }
        public string ContinuosMembers { get; set; }
        public ColumnDwg()
        {
            ColumnNo = "";
            //FloorLavel = 0.0;
            ContinuosMembers = "";
        }
    }

    public class Slab_Construction_Details
    {
        public int S_No { get; set; }
        public double Floor_Level { get; set; }
        public string Continuous_Beam_Nos { get; set; }
        public string Continuous_Beam_Lengths { get; set; }
        public double Breadth { get; set; }
        public double Depth { get; set; }
        /// <summary>
        /// Shorter Direction (Mid Span, at Bottom),
        /// </summary>
        public double Bar_dia_1 { get; set; }
        public double Bar_spc_1 { get; set; }
        public double SAst_1 { get; set; }
        /// <summary>
        /// Longer Direction (Mid Span, at Bottom).
        ///  </summary>
        public double Bar_dia_2 { get; set; }
        public double Bar_spc_2 { get; set; }
        public double SAst_2 { get; set; }

        /// <summary>
        /// Shorter Direction (Continuous Edge, at Top)
        /// </summary>
        public double Bar_dia_3 { get; set; }
        public double Bar_spc_3 { get; set; }
        public double SAst_3 { get; set; }

        /// <summary>
        /// Shorter Direction (Discontinuous Edge, at Top)
        /// </summary>
        public double Bar_dia_4 { get; set; }
        public double Bar_spc_4 { get; set; }
        public double SAst_4 { get; set; }

        /// <summary>
        /// Longer Direction (Continuous Edge, at Top),
        /// </summary>
        public double Bar_dia_5 { get; set; }
        public double Bar_spc_5 { get; set; }
        public double SAst_5 { get; set; }


        /// <summary>
        /// Longer Direction (Discontinuous Edge, at Top),
        /// </summary>
        public double Bar_dia_6 { get; set; }
        public double Bar_spc_6 { get; set; }
        public double SAst_6 { get; set; }

        /// <summary>
        /// Distribution Steel in Longer Direction at Bottom, 
        /// </summary>
        public double Bar_dia_7 { get; set; }
        public double Bar_spc_7 { get; set; }
        public double SAst_7 { get; set; }

        /// <summary>
        /// Corner Reinforcements (Top & Bottom),
        /// </summary>
        public double Bar_dia_8 { get; set; }
        public double Bar_spc_8 { get; set; }
        public double SAst_8 { get; set; }

        //S. No.	Floor
        //Level (M).	Beam
        //Nos.     	Continuous Beams with   
        //Member Nos. / Lengths (M).  	Breadth
        //[B] 
        //(M).     	Depth
        //[D] 
        //(M).  	Span Reinf. Bott.
        //Bar Dia & Nos.      
        //[Bar Mark BAst ]        	Span Reinf. Top
        //Bar Dia & Nos.   
        //[Bar Mark BAst]                    	Supp. Reinf. Bott. 
        //Bar Dia & Nos. 
        //[Bar Mark BAst]                     	Supp. Reinf. Top 
        //Bar Dia & Nos. 
        //[Bar Mark BAst]                     	Stirrups
        //Bar Dia & Spacing
        //[Bar Mark BAst]         



        public Slab_Construction_Details()
        {

            S_No = 0;
            Floor_Level = 0.0;
            //Beam_Nos = "";
            Continuous_Beam_Nos = "";
            Continuous_Beam_Lengths = "";
            Breadth = 0.0;
            Depth = 0.0;
            //Span Reinf. Bott.
            Bar_dia_1 = 0.0;
            Bar_spc_1 = 0.0;
            SAst_1 = 0.0;
            //Span Reinf. Top.
            Bar_dia_2 = 0.0;
            Bar_spc_2 = 0.0;
            SAst_2 = 0.0;
            //Supp Reinf. Bott.
            Bar_dia_3 = 0.0;
            Bar_spc_3 = 0.0;
            SAst_3 = 0.0;
            //Supp Reinf. Top.
            Bar_dia_4 = 0.0;
            Bar_spc_4 = 0.0;
            SAst_4 = 0.0;
            //Stirrups.
            Bar_dia_5 = 0.0;
            Bar_spc_5 = 0.0;
            SAst_5 = 0.0;

            Bar_dia_6 = 0.0;
            Bar_spc_6 = 0.0;
            SAst_6 = 0.0;

            Bar_dia_7 = 0.0;
            Bar_spc_7 = 0.0;
            SAst_7 = 0.0;

            Bar_dia_8 = 0.0;
            Bar_spc_8 = 0.0;
            SAst_8 = 0.0;
        }

    }

    public class Beam_Construction_Details
    {

        public int S_No { get; set; }
        public double Floor_Level { get; set; }
        public string Beam_Nos { get; set; }
        public string Continuous_Beam_Nos { get; set; }
        public string Continuous_Beam_Lengths { get; set; }
        public double Breadth { get; set; }
        public double Depth { get; set; }
        ////Span Reinf. Bott.
        public double Bar_dia_1 { get; set; }
        public double Bar_nos_1 { get; set; }
        public double BAst_1 { get; set; }
        ////Span Reinf. Top.
        public double Bar_dia_2 { get; set; }
        public double Bar_nos_2 { get; set; }
        public double BAst_2 { get; set; }
        ////Supp Reinf. Bott.
        public double Bar_dia_3_1 { get; set; }
        public double Bar_nos_3_1 { get; set; }
        public double Bar_dia_3_2 { get; set; }
        public double Bar_nos_3_2 { get; set; }
        public double BAst_3 { get; set; }
        ////Supp Reinf. Top.
        public double Bar_dia_4 { get; set; }
        public double Bar_nos_4 { get; set; }
        public double BAst_4 { get; set; }
        ////Stirrups.
        public double Bar_dia_5 { get; set; }
        public double Bar_Spacing_5 { get; set; }
        public double BAst_5 { get; set; }

        //S. No.	Floor
        //Level (M).	Beam
        //Nos.     	Continuous Beams with   
        //Member Nos. / Lengths (M).  	Breadth
        //[B] 
        //(M).     	Depth
        //[D] 
        //(M).  	Span Reinf. Bott.
        //Bar Dia & Nos.      
        //[Bar Mark BAst ]        	Span Reinf. Top
        //Bar Dia & Nos.   
        //[Bar Mark BAst]                    	Supp. Reinf. Bott. 
        //Bar Dia & Nos. 
        //[Bar Mark BAst]                     	Supp. Reinf. Top 
        //Bar Dia & Nos. 
        //[Bar Mark BAst]                     	Stirrups
        //Bar Dia & Spacing
        //[Bar Mark BAst]         



        public Beam_Construction_Details()
        {

            S_No = 0;
            Floor_Level = 0.0;
            Beam_Nos = "";
            Continuous_Beam_Nos = "";
            Continuous_Beam_Lengths = "";
            Breadth = 0.0;
            Depth = 0.0;
            //Span Reinf. Bott.
            Bar_dia_1 = 0.0;
            Bar_nos_1 = 0.0;
            BAst_1 = 0.0;
            //Span Reinf. Top.
            Bar_dia_2 = 0.0;
            Bar_nos_2 = 0.0;
            BAst_2 = 0.0;
            //Supp Reinf. Bott.
            Bar_dia_3_1 = 0.0;
            Bar_nos_3_1 = 0.0;
            BAst_3 = 0.0;
            //Supp Reinf. Top.
            Bar_dia_4 = 0.0;
            Bar_nos_4 = 0.0;
            BAst_4 = 0.0;
            //Stirrups.
            Bar_dia_5 = 0.0;
            Bar_Spacing_5 = 0.0;
            BAst_5 = 0.0;
        }

    }

    public class Column_Construction_Details
    {
        //S_No.	
        //Floor_Level
        //Column_Nos     	
        //Continuous_Columns with   
        //Member Nos. / Lengths (M).  	Breadth
        //[B] 
        //(M).     	Depth
        //[D] 
        //(M).  	Main Reinf. Bars
        //Bar Dia & Nos.      
        //[Bar Mark CAst ]        	Lateral Ties
        //Bar Dia & Spacing
        //[Bar Mark CAst]    


        public int S_No { get; set; }
        public double Floor_Level { get; set; }
        public string Column_Nos { get; set; }
        public string Continuous_Column_Nos { get; set; }
        public string Continuous_Column_Lengths { get; set; }
        public double Breadth { get; set; }
        public double Depth { get; set; }
        ////Main Reinf. Bars
        public double Bar_dia_1 { get; set; }
        public double Bar_nos_1 { get; set; }
        public double CAst_1 { get; set; }
        ////Lateral Ties
        public double Bar_dia_2 { get; set; }
        public double Bar_spacing_2 { get; set; }
        public double CAst_2 { get; set; }

    }

    public class Foundation_Construction_Details
    {
        //S. No. Elevation (M).	Associated
        //Column
        //Nos.     	
        //Footing Length [L](M). 	
        //Footing Breadth [B](M).
        //
        //
        //Pedestal Breadth [P1] 
        //
        //Pedestal Depth [P2] 
        //
        //(M).  	Main Reinf. Bars
        //Bar Dia & Spacing      
        //[Bar Mark FAst ]        	Transverse Reinf. Bars
        //Bar Dia & Spacing
        //[Bar Mark FAst]         


        public int S_No { get; set; }
        public double Floor_Level { get; set; }
        public string Column_Nos { get; set; }
        public string Continuous_Column_Nos { get; set; }
        public string Continuous_Column_Lengths { get; set; }
        public double Footing_Length { get; set; }
        public double Footing_Breadth { get; set; }
        public double Pedestal_Breadth { get; set; }
        public double Pedestal_Depth { get; set; }
        public double Column_Breadth { get; set; }
        public double Column_Depth { get; set; }
        ////Main Reinf. Bars
        public double Bar_dia_1 { get; set; }
        public double Bar_spacing_1 { get; set; }
        public double FAst_1 { get; set; }
        ////Transverse Reinf
        public double Bar_dia_2 { get; set; }
        public double Bar_spacing_2 { get; set; }
        public double FAst_2 { get; set; }

        public Foundation_Construction_Details()
        {
        }
    }

}
