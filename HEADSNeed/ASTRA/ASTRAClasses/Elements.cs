using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

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


namespace HEADSNeed.ASTRA.ASTRAClasses
{
    public class Element
    {
        private int elementNo;
        private JointCoordinate node1, node2, node3, node4;
        string sThickness, sDen;

        MemberProperty mp = new MemberProperty();
        

        #region ctor
        public Element()
        {
            elementNo = -1;
            node1 = new JointCoordinate();
            node2 = new JointCoordinate();
            node3 = new JointCoordinate();
            node4 = new JointCoordinate();
            sThickness = "";
            sDen = "";
            
        }
        public Element(Element elmt)
        {
            this.elementNo = elmt.elementNo;
            this.node1 = elmt.node1;
            this.node2 = elmt.node2;
            this.node2 = elmt.node2;
            this.node2 = elmt.node2;
        }
        #endregion

        #region Public Property
        public int ElementNo
        {
            get
            {
                return elementNo;
            }
            set
            {
                elementNo = value;
            }
        }
        public JointCoordinate Node1
        {
            get
            {
                return node1;
            }
            set
            {
                node1 = value;
            }
        }
        public JointCoordinate Node2
        {
            get
            {
                return node2;
            }
            set
            {
                node2 = value;
            }
        }
        public JointCoordinate Node3
        {
            get
            {
                return node3;
            }
            set
            {
                node3 = value;
            }
        }
        public JointCoordinate Node4
        {
            get
            {
                return node4;
            }
            set
            {
                node4 = value;
            }
        }
        public string ThickNess
        {
            get
            {
                return sThickness;
            }
            set
            {
                sThickness = "TH: " + value;
            }
        }
        public string Density
        {
            get
            {
                return sDen;
            }
            set
            {
                sDen = value;
            }
        }
        #endregion

        public static Element Parse(string s)
        {
            
            //1 5 6 8 7
            //1	24	246	247	166


            string temp = s.Trim().TrimEnd().TrimStart();

            temp = temp.Replace('\t', ' ');
            List<string> lstStr = new List<string>();
            lstStr.AddRange(temp.Split(new char[] { ' ' }));

            if (lstStr.Count != 5)
            {
                throw new Exception("Wrong data");
            }
            Element elmt = new Element();
            elmt.elementNo = int.Parse(lstStr[0]);
            elmt.Node1.NodeNo = int.Parse(lstStr[1]);
            elmt.Node2.NodeNo = int.Parse(lstStr[2]);
            elmt.Node3.NodeNo = int.Parse(lstStr[3]);
            elmt.Node4.NodeNo = int.Parse(lstStr[4]);

            //if (elmt.ElementNo == 337)
            //{
            //    System.Windows.Forms.MessageBox.Show(elmt.ToString());
            //}
            return elmt;


        }

    }
    public class ElementCollection:IList<Element>
    {
        List<Element> list = null;
        public ElementCollection()
        {
            list = new List<Element>();
        }

        #region IList<Element> Members

        public int IndexOf(Element item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ElementNo == item.ElementNo) return i;
            }
            return -1;

        }
        public int IndexOf(int ElementNo)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ElementNo == ElementNo) return i;
            }
            return -1;

        }

        public void Insert(int index, Element item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public Element this[int index]
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

        #region ICollection<Element> Members

        public void Add(Element item)
        {
            list.Add(item);
        }

        public void AddAST(string s)
        {
            //N018 UNIT 1.000 1.000 KIP FT KIP FT ELTYPE=6, Element#, Node1#, Node2#, Node3#, Node4#
            // 0      [1]   [2]   [3]   [4]  [5][6]    [7]      [8]       [9]   [10] [11]
            //N018     1     5     6     8     7 0      1      0.500     -1.000 0.00 0.00
            //N018     2     3     4     8     7 0      1      0.500     -1.000 0.00 0.00
            //N018     3     7     8    12    11 0      1      0.500      0.000 0.00 0.00

            string temp = s.Trim().TrimStart().TrimEnd().Replace('\t', ' ');
            while (temp.Contains("  "))
            {
                temp = temp.Replace("  ", " ");
            }
            MyStrings mList = new MyStrings(temp, ' ');

            Element elmt = new Element();
            elmt.ElementNo = mList.GetInt(1);
            elmt.Node1.NodeNo = mList.GetInt(2);
            elmt.Node2.NodeNo  = mList.GetInt(3);
            elmt.Node3.NodeNo  = mList.GetInt(4);
            elmt.Node4.NodeNo  = mList.GetInt(5);
            elmt.ThickNess = mList.StringList[8];
            //elmt.

            Add(elmt);

        }
        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(Element item)
        {
            return (IndexOf(item) != -1 ? true : false);
        }

        public void CopyTo(Element[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get {return false; }
        }

        public bool Remove(Element item)
        {
            if (IndexOf(item) == -1) return false;
            list.RemoveAt(IndexOf(item));
            return true;
        }

        #endregion

        #region IEnumerable<Element> Members

        public IEnumerator<Element> GetEnumerator()
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

        public void CopyCoordinates(JointCoordinateCollection jcol)
        {
            int indx = -1;
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Node1 = jcol[jcol.IndexOf(list[i].Node1)];
                    list[i].Node2 = jcol[jcol.IndexOf(list[i].Node2)];
                    list[i].Node3 = jcol[jcol.IndexOf(list[i].Node3)];
                    list[i].Node4 = jcol[jcol.IndexOf(list[i].Node4)];
                }
            }
            catch (Exception exx) { }
        }

        public void DrawElements(vdDocument doc)
        {
            vdLayer elementLay = new vdLayer();
            elementLay.Name = "Elements";
            elementLay.SetUnRegisterDocument(doc);
            elementLay.setDocumentDefaults();
            //elementLay.PenColor = new vdColor(Color.Gray);
            elementLay.PenColor = new vdColor(Color.DarkGreen);
            doc.Layers.AddItem(elementLay);

            foreach (Element elmt in list)
            {

                //We will create a vdPolyface object and add it to the Active Layout which is the basic Model Layout always existing in a Document.
                VectorDraw.Professional.vdFigures.vd3DFace one3dface = new VectorDraw.Professional.vdFigures.vd3DFace();
                //VectorDraw.Professional.vdFigures.vdPolyline one3dface = new vdPolyline();
                
                
                //We set the document where the polyface is going to be added.This is important for the vdPolyface in order to obtain initial properties with setDocumentDefaults.
                one3dface.SetUnRegisterDocument(doc);
                one3dface.setDocumentDefaults();


                //if ((elmt.Node1.Point.x == elmt.Node2.Point.x) &&
                //    (elmt.Node1.Point.x == elmt.Node3.Point.x) &&
                //    (elmt.Node1.Point.x == elmt.Node4.Point.x))
                //{
                //    one3dface.ExtrusionVector = new Vector(1.0, 0.0, 0.0);
                //}
                //else if ((elmt.Node1.Point.y == elmt.Node2.Point.y) &&
                //    (elmt.Node1.Point.y == elmt.Node3.Point.y) &&
                //    (elmt.Node1.Point.y == elmt.Node4.Point.y))
                //{
                //    one3dface.ExtrusionVector = new Vector(0.0, 1.0, 0.0);
                //}
               

                VectorDraw.Geometry.gPoints gpts = new VectorDraw.Geometry.gPoints();

                one3dface.VertexList.RemoveAll();
                one3dface.VertexList.Add(elmt.Node1.Point);
                one3dface.VertexList.Add(elmt.Node2.Point);
                one3dface.VertexList.Add(elmt.Node3.Point);
                one3dface.VertexList.Add(elmt.Node4.Point);
                one3dface.VertexList.Add(elmt.Node1.Point);




                //vdHatchProperties Properties = new vdHatchProperties();

                //Properties.SetUnRegisterDocument(doc);
                



                //Properties.FillMode = VdConstFill.VdFillModeSolid;
                //Properties.FillBkColor = new vdColor(Color.DarkGreen);
                //Properties.FillColor = new vdColor(Color.DarkGreen);


                //one3dface.HatchProperties = Properties;


                //one3dface.VertexList = gpts;
                one3dface.Layer = elementLay;
                //one3dface.visibility

                one3dface.Update();

                //Now we will add this object to the Entities collection of the Model Layout(ActiveLayout).
                doc.ActiveLayOut.Entities.AddItem(one3dface);

                //View3DShadeOn
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_ShadeOn(doc);
                one3dface.ToolTip = "Element No: " + elmt.ElementNo + " [" + elmt.Node1.NodeNo + "," +

                    elmt.Node2.NodeNo + "," + elmt.Node3.NodeNo + "," + elmt.Node4.NodeNo + "]";
            }
        }

        public void SetElementProperty(string propData)
        {
            //ELEMENT PROP
            //1 2 3 TH 0.5

            //ELEMENT PROPERTIES
            //1 2 3 TO 334 335 336 TH 0.07 DEN 0.0 Exx 3.00934e6  Exy 531061  Exg 0.0  Eyy 3.00934e6  Eyg 0.0  Gxy 1.23913e6   

            int thIndex = -1;
            int indx = -1;
            double th, den;
            th = den = 0.0d;

            string temp = propData.TrimStart().TrimEnd().Trim().ToUpper().Replace("THICKNESS","TH");

            MyStrings mLIst = new MyStrings(temp, ' ');

            thIndex = mLIst.StringList.IndexOf("TH");
            if (thIndex != -1)
            {
                th = mLIst.GetDouble(thIndex + 1);
            }
            indx = mLIst.StringList.IndexOf("DEN");
            if (indx != -1)
            {
                den = mLIst.GetDouble(indx + 1);
                mLIst.StringList.RemoveRange(indx + 2, (mLIst.StringList.Count - (indx + 2)));
            }

            //1 2 3 TO 334 335 336 TH 0.07 DEN 0.0 Exx 3.00934e6  Exy 531061  Exg 0.0  Eyy 3.00934e6  Eyg 0.0  Gxy 1.23913e6   

            int toIndex = -1;
            if (mLIst.StringList.Contains("TO"))
            {
                while (mLIst.StringList.Contains("TO"))
                {
                    toIndex = mLIst.StringList.IndexOf("TO");
                    if (toIndex > 1)
                    {
                        for (int i = 0; i < (toIndex - 1); i++)
                        {
                            indx = IndexOf(mLIst.GetInt(i));
                            if (indx != -1)
                            {
                                this[indx].ThickNess = th.ToString();
                                this[indx].Density = den.ToString();
                            }
                        }
                        mLIst.StringList.RemoveRange(0, toIndex - 2);
                    }
                    toIndex = mLIst.StringList.IndexOf("TO");
                    if (toIndex != -1)
                    {
                        for (int i = mLIst.GetInt(toIndex - 1); i <= mLIst.GetInt(toIndex + 1); i++)
                        {
                            this[IndexOf(i)].ThickNess = th.ToString();
                            this[IndexOf(i)].Density = den.ToString();
                        }
                        mLIst.StringList.RemoveRange(0, toIndex + 2);
                    }

                }
            }
            //else
            //{
            thIndex = mLIst.StringList.IndexOf("TH");

                for (int i = 0; i < thIndex; i++)
                {
                    indx = IndexOf(mLIst.GetInt(i));
                    if (indx != -1)
                    {
                        this[indx].ThickNess = th.ToString();
                        this[indx].Density = den.ToString();
                    }
                }
            //}


        }

        public void ShowElement(int index, vdDocument Maindoc, double cirRadius)
        {
            Element minc = new Element();
            try
            {
                minc = list[index];
            }
            catch (Exception exx) { return; }
            int mIndx = index;

            if (mIndx != -1)
            {
                try
                {
                    
                    for (int i = 0; i < Maindoc.ActiveLayOut.Entities.Count; i++)
                    {
                        if (Maindoc.ActiveLayOut.Entities[i] is vdCircle)
                        {
                            Maindoc.ActiveLayOut.Entities.RemoveAt(i); i = -1;
                        }
                    }

                }
                catch (Exception ex) { }


                List<gPoint> list = new List<gPoint>();

                list.Add(minc.Node1.Point);
                list.Add(minc.Node2.Point);
                list.Add(minc.Node3.Point);
                list.Add(minc.Node4.Point);
                list.Add(minc.Node1.Point);

                vdCircle cirMember;
                for (int i = 1; i < list.Count; i++)
                {
                    cirMember = new vdCircle();
                    cirMember.SetUnRegisterDocument(Maindoc);
                    cirMember.setDocumentDefaults();
                    cirMember.PenColor = new vdColor(Color.LightCoral);
                    cirMember.PenColor = new vdColor(Color.IndianRed);
                    cirMember.Center = list[i-1];
                    cirMember.Radius = cirRadius;
                    cirMember.Thickness = gPoint.Distance3D(list[i - 1], list[i]);

                    cirMember.ExtrusionVector = Vector.CreateExtrusion(list[i - 1], list[i]);
                    Maindoc.ActiveLayOut.Entities.AddItem(cirMember);
                }

                Maindoc.Redraw(true);
            }
        }

    }
}
