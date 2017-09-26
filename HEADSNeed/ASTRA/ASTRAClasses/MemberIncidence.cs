using System;
using System.Windows.Forms;
using System.Drawing;

using System.Collections.Generic;
using System.Collections;
using System.Runtime.InteropServices;
using System.ComponentModel;
using VectorDraw.Serialize;
using VectorDraw.Professional;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.Constants;
using VectorDraw.Geometry;
using VectorDraw.Render;
using VectorDraw.Professional.Actions;
using VectorDraw.Actions;
using System.IO;

namespace HEADSNeed.ASTRA.ASTRAClasses
{
    public enum MembType
    {
        BEAM = 0,
        TRUSS = 1,
        CABLE = 2,
    }

    public class MemberIncidence
    {
        int iMemberNo;
        JointCoordinate gpStartNode, gpEndNode;
        MembType mType;
        vdLine ln = null;
        public MemberIncidence()
        {
            iMemberNo = 0;
            gpStartNode = new JointCoordinate();
            gpEndNode = new JointCoordinate();
            mType = MembType.BEAM;
            //Property = new MemberProperty();
        }
        public int MemberNo
        {
            get
            {
                return iMemberNo;
            }
            set
            {
                iMemberNo = value;
            }
        }
        public JointCoordinate StartNode
        {
            get
            {
                return gpStartNode;
            }
            set
            {
                gpStartNode = value;
            }
        }
        public JointCoordinate EndNode
        {
            get
            {
                return gpEndNode;
            }
            set
            {
                gpEndNode = value;
            }
        }

        public eDirection Direction
        {
            get
            {
                if (StartNode.Point.x != EndNode.Point.x)
                    return eDirection.X_Direction;
                else if (StartNode.Point.y != EndNode.Point.y)
                    return eDirection.Y_Direction;
                else if (StartNode.Point.z != EndNode.Point.z)
                    return eDirection.Z_Direction;

                return eDirection.Z_Direction;

            }
        }
        public MembType MemberType
        {
            get
            {
                return mType;
            }
            set
            {
                mType = value;
            }
        }
        //Chiranjit [2012 02 20]
        public MemberProperty Property { get; set; }

        public double Length
        {
            get
            {
                try
                {
                    return (StartNode.Point.Distance3D(EndNode.Point));
                }
                catch (Exception ex) { }
                return 0;
            }
        }




        public static MemberIncidence ParseAST(string data)
        {
            //Example

            // N002 UNIT 1.000 1.000 KIP FT KIP FT ELTYPE=2, MEMBER#, NODE1#, x[NODE1]*lfact, y[NODE1]*lfact, z[NODE1]*lfact, NODE2#, x[NODE2]*lfact, y[NODE2]*lfact, z[NODE2]*lfact
            // N002	2	1	1	0	0	0	3	0	20	0
            // N002	2	2	3	0	20	0	7	0	35	0
            // N002	2	3	2	30	0	0	6	30	20	0
            // N002	2	4	6	30	20	0	8	30	35	0

            string temp = data.Trim().TrimEnd().TrimStart();
            temp = temp.Replace('\t', ' ');
            while (temp.IndexOf("  ") != -1)
            {
                temp = temp.Replace("  ", " ");
            }
            string[] values = temp.Split(new char[] { ' ' });
            if (values.Length != 11) throw new Exception("String Data is not correct format.");

            MemberIncidence membInci = new MemberIncidence();

            membInci.iMemberNo = int.Parse(values[2]);
            membInci.StartNode.NodeNo = int.Parse(values[3]);
            membInci.EndNode.NodeNo = int.Parse(values[7]);

            return membInci;
        }
        public static MemberIncidence ParseTXT(string data)
        {
            //Example

            // MEMBER INCIDENCE
            // 1 1 3 ; 2 3 7 ; 3 2 6 ; 4 6 8 ; 5 3 4
            // 6 4 5 ; 7 5 6 ; 8 7 12 ; 9 12 14
            // 10 14 16 ; 11 15 16 ; 12 13 15 ; 13 8 13
            // 14 9 12 ; 15 9 14 ; 16 11 14 ; 17 11 15
            // 18 10 15 ; 19 10 13 ; 20 7 9
            // 21 9 11 ; 22 10 11 ; 23 8 10

            string temp = data.Trim().TrimEnd().TrimStart();
            temp = temp.Replace('\t', ' ');
            while (temp.IndexOf("  ") != -1)
            {
                temp = temp.Replace("  ", " ");
            }
            string[] values = temp.Split(new char[] { ' ' });
            if (values.Length != 3) throw new Exception("String Data is not correct format.");

            MemberIncidence membInci = new MemberIncidence();

            membInci.iMemberNo = int.Parse(values[0]);
            membInci.StartNode.NodeNo = int.Parse(values[1]);
            membInci.EndNode.NodeNo = int.Parse(values[2]);
            return membInci;
        }
        public override string ToString()
        {
            string kStr = string.Format("N002    2 {0,6:f0} {1,10:f0} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f0} {6,10:f3} {7,10:f3} {8,10:f3}",
                iMemberNo,
                gpStartNode.NodeNo,
                gpStartNode.Point.x,
                gpStartNode.Point.y,
                gpStartNode.Point.z,
                gpEndNode.NodeNo,
                gpEndNode.Point.x,
                gpEndNode.Point.y,
                gpEndNode.Point.z);
            return kStr;
        }

        //Chiranjit [2011 09 20]
        //Get a vdLine Object

        public vdDocument vdoc { get; set; }
        public vdLine Line
        {
            get
            {
                if (ln == null && vdoc != null)
                {
                    ln = new vdLine();
                    ln.SetUnRegisterDocument(vdoc);
                    ln.setDocumentDefaults();
                    vdoc.ActiveLayOut.Entities.AddItem(ln);
                }
                ln.StartPoint.x = StartNode.Point.x;
                ln.StartPoint.y = StartNode.Point.x;
                ln.StartPoint.z = StartNode.Point.x;
                ln.EndPoint.x = EndNode.Point.x;
                ln.EndPoint.y = EndNode.Point.y;
                ln.EndPoint.z = EndNode.Point.z;
                return ln;
            }
            set
            {
                ln = value;
            }
        }
    }

    /// <summary>
    /// This class is a collection of MemberIncidence.
    /// And provides Parsing from Text data And AST data.
    /// </summary>
    public class MemberIncidenceCollection : IList<MemberIncidence>
    {
        List<MemberIncidence> list;
        public  _MemberGroupCollection Groups { get; set; }

        bool bDrawNodeMember;
        string kStr = "";
        vdCircle cirMember = null;
        Hashtable hash_joint_no = new Hashtable();

        public MemberIncidence Get_Member(int memNo)
        {
            foreach (var item in this)
            {
                if (item.MemberNo == memNo) return item;
            }
            return null;
        }

        public MemberIncidenceCollection(string fileName)
        {
            list = new List<MemberIncidence>();
            StreamReader sr = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
            try
            {
                while (!sr.EndOfStream)
                {
                    kStr = sr.ReadLine().ToUpper();
                    AddTXT(kStr);

                    //if(kStr.Contains("SUPPORT"))
                    //{
                    //    kStr = sr.ReadLine();
                    //    sptCol.SetCollection(kStr);
                    //}
                }
                sr.Close();
            }
            catch (Exception exx)
            {
                sr.Close();
            }
        }
        //Chiranjit [2012 02 20]
        public void Read_Member_Groups(string file_name)
        {
            Groups.Read_File_File(file_name);
            //Groups
            for (int i = 0; i < list.Count; i++)
            {
                list[i].MemberType = Groups.GetMemberType(list[i].MemberNo);
                list[i].Property = Groups.GetProperty(list[i].MemberNo);
            }

        }

        public void Add_Group(string text)
        {
            _MemberGroup mg = new _MemberGroup();

            text = MyStrings.RemoveAllSpaces(text);
            MyStrings mlist = new MyStrings( text, ' ');

            mg.Name = mlist.StringList[0];
            mg.MemberNosText = mlist.GetString(1);
            mg.SetMemNos();

            Groups.Add(mg);
            Groups.Table.Add(mg.Name, mg);
        }

        public MemberIncidenceCollection()
        {
            list = new List<MemberIncidence>();
            cirMember = new vdCircle();
            Groups = new _MemberGroupCollection();
        }

        public void CopyJointCoordinates(JointCoordinateCollection JntCol)
        {
            int iNode = 0, indx = -1;
            for (int i = 0; i < list.Count; i++)
            {
                try
                {
                    indx = JntCol.IndexOf(list[i].StartNode);
                    list[i].StartNode = JntCol[indx];

                    indx = JntCol.IndexOf(list[i].EndNode);
                    list[i].EndNode = JntCol[indx];
                }
                catch (Exception exx)
                {
                }
            }
        }

        public bool DrawNodeMember
        {
            get
            {
                return bDrawNodeMember;
            }
            set
            {
                bDrawNodeMember = value;
            }
        }

        vdLayer selectLay = new vdLayer();
        vdLayer nodesLay = new vdLayer();
        vdLayer membersLay = new vdLayer();

        public void DrawMember_2011_09_22(vdDocument doc, ASTRADoc astdoc)
        {
            doc.ActiveLayOut.Entities.RemoveAll();
            SetLayers(doc);
            foreach (MemberIncidence mi in astdoc.Members)
            {
                DrawMember(mi, doc);
            }
            VectorDraw.Professional.Memory.vdMemory.Collect();
            System.GC.Collect();
        }
        public void DrawMember(vdDocument doc)
        {
            doc.ActiveLayOut.Entities.RemoveAll();
            SetLayers(doc);
            foreach (MemberIncidence mi in list)
            {
                DrawMember(mi, doc);
            }
            VectorDraw.Professional.Memory.vdMemory.Collect();
            System.GC.Collect();

        }
        public void DrawMember(vdDocument doc, double txtSize)
        {
            //doc.ActiveLayOut.Entities.RemoveAll();
            SetLayers(doc);
            foreach (MemberIncidence mi in list)
            {
                DrawMember(mi, doc, txtSize);
            }
            doc.Redraw(true);
            VectorDraw.Professional.Memory.vdMemory.Collect();
            System.GC.Collect();

           
        }

        public void SetLayers(vdDocument doc)
        {

            //Chiranjit [2014 10 31]
            selectLay = doc.Layers.FindName("Selection");
            if (selectLay == null)
            {
                selectLay = new vdLayer();
                selectLay.Name = "Selection";
                selectLay.SetUnRegisterDocument(doc);
                selectLay.setDocumentDefaults();
                selectLay.PenColor = new vdColor(Color.Red);
                doc.Layers.AddItem(selectLay);
            }


            nodesLay = doc.Layers.FindName("Nodes");
            if (nodesLay == null)
            {
                nodesLay = new vdLayer();
                nodesLay.Name = "Nodes";
                nodesLay.SetUnRegisterDocument(doc);
                nodesLay.setDocumentDefaults();
                nodesLay.PenColor = new vdColor(Color.Magenta);
                doc.Layers.AddItem(nodesLay);
            }


            membersLay = doc.Layers.FindName("Members");
            if (membersLay == null)
            {
                membersLay = new vdLayer();
                membersLay.Name = "Members";
                membersLay.SetUnRegisterDocument(doc);
                membersLay.setDocumentDefaults();
                doc.Layers.AddItem(membersLay);
            }
            VectorDraw.Professional.Memory.vdMemory.Collect();
            System.GC.Collect();

        }


        public void DrawMember_Test(vdDocument doc)
        {
            doc.ActiveLayOut.Entities.RemoveAll();
            //doc.Redraw(true);
            //doc.ClearEraseItems();
            //nodesLay = new vdLayer();
            //membersLay = new vdLayer();

            //nodesLay.Name = "Nodes";
            //membersLay.Name = "Members";


            //nodesLay = doc.Layers.FindName("Nodes")


            //doc.Layers.AddItem(nodesLay);
            //doc.Layers.AddItem(membersLay);

            SetLayers(doc);

            foreach (MemberIncidence mi in list)
            {
                nodesLay.SetUnRegisterDocument(doc);
                nodesLay.setDocumentDefaults();
                doc.Layers.AddItem(nodesLay);
                DrawMember(mi, doc);
                //doc.New();
            }
        }

        public void UpdateMember(MemberIncidence mi, vdDocument doc)
        {

            double length, factor, txtSize;


            doc.Palette.Background = Color.White;

            mi.vdoc = doc;

            vdText vtxtStartNode = new vdText();
            vdText vtxtEndNode = new vdText();
            vdText vtxtMemberNo = new vdText();

            vtxtStartNode.SetUnRegisterDocument(doc);
            vtxtStartNode.setDocumentDefaults();
            vtxtEndNode.SetUnRegisterDocument(doc);
            vtxtEndNode.setDocumentDefaults();
            vtxtMemberNo.SetUnRegisterDocument(doc);
            vtxtMemberNo.setDocumentDefaults();


            //line.PenColor = new vdColor(Color.Black);
            mi.Line.Update();


            //line.StartPoint = mi.StartNode.Point;
            //line.EndPoint = mi.EndNode.Point;
            //line.ToolTip = string.Format("Member No : {0} \nStart Node = {1} [X={2}, Y={3}, Z={4}]\nEnd Node = {5} [X={6}, Y={7}, Z={8}]",
            //    mi.MemberNo,
            //    mi.StartNode.NodeNo,
            //    mi.StartNode.Point.x,
            //    mi.StartNode.Point.y,
            //    mi.StartNode.Point.z,

            //    mi.EndNode.NodeNo,
            //    mi.EndNode.Point.x,
            //    mi.EndNode.Point.y,
            //    mi.EndNode.Point.z); 

            mi.Line.ToolTip = string.Format("Member No : {0} [Nodes ({1}, {2})]",
                 mi.MemberNo,
                 mi.StartNode.NodeNo,
                 mi.EndNode.NodeNo);
            mi.Line.Update();

            //doc.ActionLayout.Entities.AddItem(line);



            // Chiranjit Modified [2010 04 16], Kolkata, TechSOFT
            object jc_no = hash_joint_no[mi.StartNode.NodeNo];

            if (jc_no == null)
            {
                hash_joint_no.Add(mi.StartNode.NodeNo, mi.StartNode.NodeNo);
                vtxtEndNode.ToolTip = string.Format("Node No : {0} [X={1}, Y={2}, Z={3}]",
                    mi.StartNode.NodeNo,
                    mi.StartNode.Point.x,
                    mi.StartNode.Point.y,
                    mi.StartNode.Point.z);


                doc.ActionLayout.Entities.AddItem(vtxtStartNode);
            }


            jc_no = hash_joint_no[mi.EndNode.NodeNo];

            if (jc_no == null)
            {
                hash_joint_no.Add(mi.EndNode.NodeNo, mi.EndNode.NodeNo);
                vtxtEndNode.ToolTip = string.Format("Node No : {0} [X={1}, Y={2}, Z={3}]",
                    mi.EndNode.NodeNo,
                    mi.EndNode.Point.x,
                    mi.EndNode.Point.y,
                    mi.EndNode.Point.z);

                doc.ActionLayout.Entities.AddItem(vtxtEndNode);

            }


            //doc.ActionLayout.Entities.AddItem(vtxtStartNode);
            //doc.ActionLayout.Entities.AddItem(vtxtEndNode);
            doc.ActionLayout.Entities.AddItem(vtxtMemberNo);



            //length = line.Length();
            //factor = 22.2222d;
            //factor = length / 0.9d;

            //txtSize = length / factor;

            txtSize = GetTextSize();

            vtxtStartNode.TextString = mi.StartNode.NodeNo.ToString();
            vtxtStartNode.Height = txtSize;
            //vtxtStartNode.Height = 0.9d;
            //vtxtStartNode.PenColor = new vdColor(Color.Green);
            vtxtStartNode.InsertionPoint = mi.StartNode.Point;
            vtxtStartNode.Layer = nodesLay;

            vtxtEndNode.TextString = mi.EndNode.NodeNo.ToString();
            vtxtEndNode.Height = txtSize;
            //vtxtEndNode.Height = 0.9d;
            //vtxtEndNode.PenColor = new vdColor(Color.Green);
            vtxtEndNode.InsertionPoint = mi.EndNode.Point;
            vtxtEndNode.Layer = nodesLay;

            vtxtMemberNo.TextString = mi.MemberNo.ToString();
            //vtxtMemberNo.Height = 0.9d;
            vtxtMemberNo.Height = txtSize;
            vtxtMemberNo.PenColor = new vdColor(Color.Blue);
            vtxtMemberNo.InsertionPoint = (mi.StartNode.Point + mi.EndNode.Point) / 2;
            vtxtMemberNo.Layer = membersLay;
            vtxtMemberNo.ToolTip = string.Format("Member No : {0} [Nodes ({1}, {2})]",
                mi.MemberNo,
                mi.StartNode.NodeNo,
                mi.EndNode.NodeNo);
            //doc.ZoomAll();
            //doc.Redraw(true);
        }

        public void DrawMember(MemberIncidence mi, vdDocument doc)
        {

            double length, factor, txtSize;


            doc.Palette.Background = Color.White;
            vdLine line = new vdLine();
            vdText vtxtStartNode = new vdText();
            vdText vtxtEndNode = new vdText();
            vdText vtxtMemberNo = new vdText();

            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            vtxtStartNode.SetUnRegisterDocument(doc);
            vtxtStartNode.setDocumentDefaults();
            vtxtEndNode.SetUnRegisterDocument(doc);
            vtxtEndNode.setDocumentDefaults();
            vtxtMemberNo.SetUnRegisterDocument(doc);
            vtxtMemberNo.setDocumentDefaults();


            line.PenColor = new vdColor(Color.Black);
            line.StartPoint = mi.StartNode.Point;
            line.EndPoint = mi.EndNode.Point;
            //line.ToolTip = string.Format("Member No : {0} \nStart Node = {1} [X={2}, Y={3}, Z={4}]\nEnd Node = {5} [X={6}, Y={7}, Z={8}]",
            //    mi.MemberNo,
            //    mi.StartNode.NodeNo,
            //    mi.StartNode.Point.x,
            //    mi.StartNode.Point.y,
            //    mi.StartNode.Point.z,

            //    mi.EndNode.NodeNo,
            //    mi.EndNode.Point.x,
            //    mi.EndNode.Point.y,
            //    mi.EndNode.Point.z); 

            line.ToolTip = string.Format("Member No : {0} [Nodes ({1}, {2})]",
                 mi.MemberNo,
                 mi.StartNode.NodeNo,
                 mi.EndNode.NodeNo);

            doc.ActionLayout.Entities.AddItem(line);



            // Chiranjit Modified [2010 04 16], Kolkata, TechSOFT
            object jc_no = hash_joint_no[mi.StartNode.NodeNo];

            if (jc_no == null)
            {
                hash_joint_no.Add(mi.StartNode.NodeNo, mi.StartNode.NodeNo);
                vtxtEndNode.ToolTip = string.Format("Node No : {0} [X={1}, Y={2}, Z={3}]",
                    mi.StartNode.NodeNo,
                    mi.StartNode.Point.x,
                    mi.StartNode.Point.y,
                    mi.StartNode.Point.z);


                doc.ActionLayout.Entities.AddItem(vtxtStartNode);
            }


            jc_no = hash_joint_no[mi.EndNode.NodeNo];

            if (jc_no == null)
            {
                hash_joint_no.Add(mi.EndNode.NodeNo, mi.EndNode.NodeNo);
                vtxtEndNode.ToolTip = string.Format("Node No : {0} [X={1}, Y={2}, Z={3}]",
                    mi.EndNode.NodeNo,
                    mi.EndNode.Point.x,
                    mi.EndNode.Point.y,
                    mi.EndNode.Point.z);

                doc.ActionLayout.Entities.AddItem(vtxtEndNode);

            }


            //doc.ActionLayout.Entities.AddItem(vtxtStartNode);
            //doc.ActionLayout.Entities.AddItem(vtxtEndNode);
            doc.ActionLayout.Entities.AddItem(vtxtMemberNo);



            length = line.Length();
            //factor = 22.2222d;
            //factor = length / 0.9d;

            //txtSize = length / factor;

            txtSize = GetTextSize();

            vtxtStartNode.TextString = mi.StartNode.NodeNo.ToString();
            vtxtStartNode.Height = txtSize;
            //vtxtStartNode.Height = 0.9d;
            //vtxtStartNode.PenColor = new vdColor(Color.Green);
            vtxtStartNode.InsertionPoint = mi.StartNode.Point;
            vtxtStartNode.Layer = nodesLay;

            vtxtEndNode.TextString = mi.EndNode.NodeNo.ToString();
            vtxtEndNode.Height = txtSize;
            //vtxtEndNode.Height = 0.9d;
            //vtxtEndNode.PenColor = new vdColor(Color.Green);
            vtxtEndNode.InsertionPoint = mi.EndNode.Point;
            vtxtEndNode.Layer = nodesLay;

            vtxtMemberNo.TextString = mi.MemberNo.ToString();
            //vtxtMemberNo.Height = 0.9d;
            vtxtMemberNo.Height = txtSize;
            vtxtMemberNo.PenColor = new vdColor(Color.Blue);
            vtxtMemberNo.InsertionPoint = (mi.StartNode.Point + mi.EndNode.Point) / 2;
            vtxtMemberNo.Layer = membersLay;
            vtxtMemberNo.ToolTip = string.Format("Member No : {0} [Nodes ({1}, {2})]",
                mi.MemberNo,
                mi.StartNode.NodeNo,
                mi.EndNode.NodeNo);
            //doc.ZoomAll();
            //doc.Redraw(true);
        }

        public void DrawMember(MemberIncidence mi, vdDocument doc, double txtSize)
        {

            double length, factor;


            doc.Palette.Background = Color.White;
            vdLine line = new vdLine();
            vdText vtxtMemberNo = new vdText();

            line.SetUnRegisterDocument(doc);
            line.setDocumentDefaults();
            vtxtMemberNo.SetUnRegisterDocument(doc);
            vtxtMemberNo.setDocumentDefaults();


            line.PenColor = new vdColor(Color.Black);

            line.Layer = doc.Layers[0];
            doc.Layers[0].Frozen = false;

            line.StartPoint = mi.StartNode.Point;
            line.EndPoint = mi.EndNode.Point;
          

            line.ToolTip = string.Format("Member No : {0} [Nodes ({1}, {2})]",
                 mi.MemberNo,
                 mi.StartNode.NodeNo,
                 mi.EndNode.NodeNo);

            doc.ActionLayout.Entities.AddItem(line);




            length = line.Length();
            //factor = 22.2222d;
            //factor = length / 0.9d;

            //txtSize = length / factor;

            //txtSize = GetTextSize();

           
            vtxtMemberNo.TextString = mi.MemberNo.ToString();
            vtxtMemberNo.Layer = membersLay;
            vtxtMemberNo.Height = txtSize;
            vtxtMemberNo.PenColor = new vdColor(Color.Blue);
            vtxtMemberNo.InsertionPoint = (mi.StartNode.Point + mi.EndNode.Point) / 2;
            vtxtMemberNo.Layer = membersLay;
            vtxtMemberNo.ToolTip = string.Format("Member No : {0} [Nodes ({1}, {2})]",
                mi.MemberNo,
                mi.StartNode.NodeNo,
                mi.EndNode.NodeNo);

            doc.ActionLayout.Entities.AddItem(vtxtMemberNo);

            //doc.ZoomAll();
            //doc.Redraw(true);
        }

        public void RemoveMemberLine(vdDocument Maindoc)
        {
            try
            {
                Maindoc.ActiveLayOut.Entities.RemoveItem(cirMember);
            }
            catch (Exception ex) { }
            Maindoc.Redraw(true);
        }
        public void ShowMember(int index, vdDocument Maindoc)
        {
            ShowMember(index, Maindoc, 0.03d);

            Maindoc.Redraw(true);
        }
        public void ShowMember(int index, vdDocument Maindoc, double cirRadius)
        {
            MemberIncidence minc = new MemberIncidence();
            try
            {
                minc = list[index];
            }
            catch (Exception exx) { return; }
            int mIndx = index;

            if (mIndx != -1)
            {
                RemoveCircle(Maindoc);

                cirMember = new vdCircle();
                cirMember.SetUnRegisterDocument(Maindoc);
                cirMember.setDocumentDefaults();
                cirMember.PenColor = new vdColor(Color.LightCoral);
                cirMember.PenColor = new vdColor(Color.IndianRed);
                cirMember.Center = minc.StartNode.Point;
                cirMember.Radius = cirRadius;
                cirMember.Thickness = gPoint.Distance3D(minc.StartNode.Point,
                    minc.EndNode.Point);

                cirMember.ExtrusionVector = Vector.CreateExtrusion(minc.StartNode.Point,
                    minc.EndNode.Point);
                Maindoc.ActiveLayOut.Entities.AddItem(cirMember);
                cirMember.Update();

                //Maindoc.CommandAction.Zoom("W",
                //    new gPoint(minc.StartNode.Point.x + minc.Length, minc.StartNode.Point.y + minc.Length, minc.StartNode.Point.z + minc.Length),
                //new gPoint(minc.EndNode.Point.x - minc.Length, minc.EndNode.Point.y - minc.Length, minc.EndNode.Point.z - minc.Length));


                Maindoc.Redraw(true);
            }
        }

        public void RemoveCircle(vdDocument Maindoc)
        {
            try
            {
                for (int i = 0; i < Maindoc.ActiveLayOut.Entities.Count; i++)
                {
                    vdCircle cir = Maindoc.ActiveLayOut.Entities[i] as vdCircle;
                    if (cir != null)
                    {
                        cir.Deleted = true;
                    }
                }

                Maindoc.ClearEraseItems();


                //if (Maindoc.ActiveLayOut.Entities.FindItem(cirMember))
                //{
                //    Maindoc.ActiveLayOut.Entities.RemoveItem(cirMember);
                //}
                //for (int i = 0; i < Maindoc.ActiveLayOut.Entities.Count; i++)
                //{
                //    if (Maindoc.ActiveLayOut.Entities[i] is vdCircle)
                //    {
                //        if (Maindoc.ActiveLayOut.Entities[i].Layer.Name == "0")
                //        {
                //            Maindoc.ActiveLayOut.Entities.RemoveAt(i); i = -1;
                //        }
                //    }
                //}
                Maindoc.Redraw(true);
            }
            catch (Exception ex) { }
        }

        public double GetTextSize()
        {
            double d = 0.0d, length = -1.0;
            double factor = 36.99d;
            for (int i = 0; i < list.Count; i++)
            {
                d = gPoint.Distance3D(list[i].StartNode.Point, list[i].EndNode.Point);
                if (d > length)
                    length = d;
            }
            return length / 10;
            //return length / factor;
        }

        #region IList<MemberIncidence> Members

        public int IndexOf(MemberIncidence item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].MemberNo == item.MemberNo)
                {
                    return i;
                }
            }
            return -1;
        }
        public int IndexOf(int MemberNo)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].MemberNo == MemberNo)
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, MemberIncidence item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public MemberIncidence this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        #endregion

        #region ICollection<MemberIncidence> Members

        public void Add(MemberIncidence item)
        {
            list.Add(item);
        }
        public void AddAST(string itemData)
        {
            //N008 ELTYPE=1, TRUSS_MEMB #
            //N008     1   14
            //N008     1   15
            //N008     1   16
            //N008     1   17
            string temp = itemData.TrimEnd().Trim().TrimStart().Replace('\t', ' ');
            while (temp.Contains("  "))
            {
                temp = temp.Replace("  ", " ");
            }
            MyStrings mList = new MyStrings(temp, ' ');

            int indx = IndexOf(mList.GetInt(2));
            if (indx != -1)
            {
                list[indx].MemberType = MembType.TRUSS;
            }
        }
        public void AddMemberTruss(string trussData)
        {
            //EX:2 MEMB TRUSS
            //14 TO 23 ;

            //EX:3 MEMBER TRUSS
            //1 TO 47
            List<int> mems = new List<int>();

            if (Groups.Table.Count > 0)
            {
                _MemberGroup mg = (_MemberGroup)Groups.Table[trussData];
                if (mg != null)
                {
                    mems = mg.MemberNos;

                    for (int i = 0; i < list.Count; i++)
                    {
                        if (mems.Contains(list[i].MemberNo))
                            list[i].MemberType = MembType.TRUSS;

                    }
                    return;
                }
            }




            mems = MyStrings.Get_Array_Intiger(trussData);

            for (int i = 0; i < list.Count; i++)
            {
                if (mems.Contains(list[i].MemberNo))
                    list[i].MemberType = MembType.TRUSS;

            }

        }
        public void AddTXT(string itemData)
        {
            string[] values = itemData.Split(new char[] { ';' });
            MemberIncidence mIncnd1, mIncnd2;

            List<string> lstStr = new List<string>();
            string kStr = "";
            int n1 = 0, sNode = 0, eNode = 0, toNode = 0, indx = 0;

            foreach (string str in values)
            {
                kStr = str.Trim().TrimEnd().TrimStart();
                lstStr.Clear();
                lstStr.AddRange(kStr.Split(new char[] { ' ' }));
                try
                {
                    if (lstStr.Count == 4)
                    {
                        indx = 0;
                        n1 = int.Parse(lstStr[indx]); indx++;
                        sNode = int.Parse(lstStr[indx]); indx++;
                        eNode = int.Parse(lstStr[indx]); indx++;
                        toNode = int.Parse(lstStr[indx]); indx++;

                        mIncnd1 = new MemberIncidence();
                        mIncnd1.MemberNo = n1;
                        mIncnd1.StartNode.NodeNo = sNode;
                        mIncnd1.EndNode.NodeNo = eNode;
                        list.Add(mIncnd1);

                        for (int i = n1 + 1; i <= toNode; i++)
                        {
                            sNode++; eNode++;
                            mIncnd2 = new MemberIncidence();
                            mIncnd2.MemberNo = i;
                            mIncnd2.StartNode.NodeNo = sNode;
                            mIncnd2.EndNode.NodeNo = eNode;
                            list.Add(mIncnd2);
                        }

                    }
                    else if (lstStr.Count == 6)
                    {
                        indx = 0;
                        n1 = int.Parse(lstStr[indx]); indx++;
                        sNode = int.Parse(lstStr[indx]); indx++;
                        eNode = int.Parse(lstStr[indx]); indx++;
                        toNode = int.Parse(lstStr[indx]); indx++;

                        int sni = int.Parse(lstStr[indx]); indx++;
                        int eni = int.Parse(lstStr[indx]); indx++;


                        mIncnd1 = new MemberIncidence();
                        mIncnd1.MemberNo = n1;
                        mIncnd1.StartNode.NodeNo = sNode;
                        mIncnd1.EndNode.NodeNo = eNode;
                        list.Add(mIncnd1);

                        for (int i = n1 + 1; i <= toNode; i++)
                        {
                            sNode+= sni; eNode += eni;
                            mIncnd2 = new MemberIncidence();
                            mIncnd2.MemberNo = i;
                            mIncnd2.StartNode.NodeNo = sNode;
                            mIncnd2.EndNode.NodeNo = eNode;
                            list.Add(mIncnd2);
                        }
                    }
                    else
                    {
                        list.Add(MemberIncidence.ParseTXT(str));
                    }
                }
                catch (Exception exx)
                {
                }
                lstStr.Clear();
            }
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(MemberIncidence item)
        {
            if (this.IndexOf(item) != -1)
                return true;
            return false;
        }

        public void CopyTo(MemberIncidence[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(MemberIncidence item)
        {
            int indx = this.IndexOf(item);
            if (indx == -1) return false;

            list.RemoveAt(indx);
            return true;
        }

        #endregion

        #region IEnumerable<MemberIncidence> Members

        public IEnumerator<MemberIncidence> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion


        public class _MemberGroup
        {
            public _MemberGroup()
            {
                Name = "";
                GroupType = MembType.BEAM;
                Properties = new MemberProperty();
                MemberNos = new List<int>();
            }
            public string Name { get; set; }
            public List<int> MemberNos { get; set; }
            public string MemberNosText { get; set; }
            public MembType GroupType { get; set; }
            public MemberProperty Properties { get; set; }
            public void SetMemNos()
            {
                string memText = MemberNosText;
                if (memText == "") return;
                MemberNos = MyStrings.Get_Array_Intiger(MemberNosText);

               

               
            }
        }
        public class _MemberGroupCollection : List<_MemberGroup>
        {
            public Hashtable Table { get; set; }
            public _MemberGroupCollection()
                : base()
            {
                Table = new Hashtable();
            }
            public _MemberGroupCollection(_MemberGroupCollection obj)
                : base(obj)
            {
                Table = new Hashtable();
            }
            public void Read_File_File(string file_name)
            {
                if (!File.Exists(file_name)) return;

                Table = new Hashtable();
                #region SAMPLE
                //START GROUP DEFINITION
                //_L0L1         1          6          7         12
                //_L1L2         2          5          8         11
                //_L2L3         3          4          9         10
                //_U1U2        35         38         39         42
                //_U2U3        36         37         40         41
                //_L1U1        13         17         18         22
                //_L2U2        14         16         19         21
                //_L3U3        15         20
                //_ER         23         28         29         34
                //_L2U1        24         27         30         33
                //_L3U2        25         26         31         32
                //_TCS_ST            102         TO        106 
                //_TCS_DIA           107         TO        114 
                //_BCB               115         TO        126 
                //_STRINGER           43         TO         66 
                //_XGIRDER            67         TO        101 
                //END
                //MEMBER PROPERTY
                //_L0L1      	PRI	AX	0.024	IX	0.00001	IY	0.000741	IZ	0.001
                //_L1L2      	PRI	AX	0.024	IX	0.00001	IY	0.000741	IZ	0.001
                //_L2L3      	PRI	AX	0.030	IX	0.00001	IY	0.000946	IZ	0.001
                //_U1U2      	PRI	AX	0.027	IX	0.00001	IY	0.000807	IZ	0.000693
                //_U2U3      	PRI	AX	0.033	IX	0.00001	IY	0.000864	IZ	0.000922
                //_L1U1      	PRI	AX	0.006	IX	0.00001	IY	0.000182	IZ	0.000036
                //_L2U2      	PRI	AX	0.013	IX	0.00001	IY	0.000399	IZ	0.000302
                //_L3U3      	PRI	AX	0.009	IX	0.00001	IY	0.000290	IZ	0.000127
                //_ER           	PRI	AX	0.022	IX	0.00001	IY	0.000666	IZ	0.000579
                //_L2U1      	PRI	AX	0.019	IX	0.00001	IY	0.000621	IZ	0.000356
                //_L3U2      	PRI	AX	0.014	IX	0.00001	IY	0.000474	IZ	0.000149
                //_TCS_ST	        PRI	AX	0.006	IX	0.00001	IY	0.000027	IZ	0.000225
                //_TCS_DIA	PRI	AX	0.006	IX	0.00001	IY	0.000027	IZ	0.000225
                //_BCB	        PRI	AX	0.002	IX	0.00001	IY	0.000001	IZ	0.000001
                //_STRINGER	PRI	AX	0.048	IX	0.00001	IY	0.008	        IZ	0.002
                //_XGIRDER	PRI	AX	0.059	IX	0.00001	IY	0.009	        IZ	0.005
                //MEMBER TRUSS
                //_L0L1
                //MEMBER TRUSS
                //_L1L2
                //MEMBER TRUSS
                //_L2L3
                //MEMBER TRUSS
                //_U1U2
                //MEMBER TRUSS
                //_U2U3
                //MEMBER TRUSS
                //_L1U1
                //MEMBER TRUSS
                //_L2U2
                //MEMBER TRUSS
                //_L3U3
                //MEMBER TRUSS
                //_ER
                //MEMBER TRUSS
                //_L2U1
                //MEMBER TRUSS
                //_L3U2
                //MEMBER TRUSS
                //_TCS_ST
                //MEMBER TRUSS
                //_TCS_DIA
                //MEMBER TRUSS
                //_BCB
                #endregion
                List<string> list = new List<string>(File.ReadAllLines(file_name));

                bool flag1 = false;
                bool flag2 = false;
                MyStrings mlist = null;
                string kStr = "";
                _MemberGroup mgr = new _MemberGroup();
                for (int i = 0; i < list.Count; i++)
                {
                    kStr = list[i].Trim().TrimEnd().TrimStart().ToUpper();
                    kStr = MyStrings.RemoveAllSpaces(kStr);

                    mlist = new MyStrings(MyStrings.RemoveAllSpaces(kStr), ' ');

                    if (kStr.StartsWith("START GROUP"))
                    {
                        flag1 = true;
                        continue;
                    }
                    if (kStr.StartsWith("MEMBER PROPERTY"))
                    {
                        flag2 = true;
                        flag1 = false;
                        continue;
                    }
                    if (kStr.StartsWith("END") && !kStr.StartsWith("END_RA"))
                    {
                        flag1 = false;
                        continue;
                    }
                    else if (kStr.StartsWith("FINISH"))
                    {
                        break;
                    }

                    if (flag1)
                    {
                        try
                        {
                            mgr = Table[mlist.StringList[0]] as _MemberGroup;
                            if (mgr == null)
                            {
                                mgr = new _MemberGroup();
                                mgr.Name = mlist.StringList[0];
                                mgr.MemberNosText = mlist.GetString(1);
                                mgr.SetMemNos();
                                Table.Add(mgr.Name, mgr);
                                Add(mgr);
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                    if (flag2)
                    {

                        try
                        {

                            mgr = Table[mlist.StringList[0]] as _MemberGroup;

                            if (mgr != null)
                            {
                                if (mlist.Count == 1)
                                {
                                    kStr = list[i - 1];
                                    kStr = MyStrings.RemoveAllSpaces(kStr);
                                    mlist = new MyStrings(kStr, ' ');

                                    if (mlist.Count > 1)
                                    {
                                        if (mlist.StringList[1].StartsWith("TRUSS"))
                                        {
                                            mgr.GroupType = MembType.TRUSS;
                                        }
                                        else if (mlist.StringList[1].StartsWith("BEAM"))
                                        {
                                            mgr.GroupType = MembType.BEAM;
                                        }
                                        else if (mlist.StringList[1].StartsWith("CABLE"))
                                        {
                                            mgr.GroupType = MembType.CABLE;
                                        }
                                    }
                                    continue;
                                }
                                else
                                {
                                    for (int j = 1; j < mlist.Count; j++)
                                    {
                                        if (mlist.StringList[j].StartsWith("PR"))
                                            mgr.Properties.PR = mlist.StringList[j];
                                        else if (mlist.StringList[j].StartsWith("IX"))
                                            mgr.Properties.IX = mlist.GetDouble(j + 1);
                                        else if (mlist.StringList[j].StartsWith("IY"))
                                            mgr.Properties.IY = mlist.GetDouble(j + 1);
                                        else if (mlist.StringList[j].StartsWith("IZ"))
                                            mgr.Properties.IZ = mlist.GetDouble(j + 1);
                                        else if (mlist.StringList[j].StartsWith("AX"))
                                            mgr.Properties.Area = mlist.GetDouble(j + 1);

                                    }
                                }
                            }
                        }
                        catch (Exception ex) { }
                    }

                }
                //this
            }

            public MemberProperty GetProperty(int memNo)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i].MemberNos.Contains(memNo))
                    {
                        return this[i].Properties;
                    }
                }
                //return MembType.TRUSS;
                return new MemberProperty();

            }
            public MembType GetMemberType(int memNo)
            {
                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i].MemberNos.Contains(memNo))
                        return this[i].GroupType;
                }
                return MembType.BEAM;

            } 
        }
   }

    public enum eDirection
    {
        X_Direction = 0,
        Y_Direction = 1,
        Z_Direction = 2,
    }

  
}
