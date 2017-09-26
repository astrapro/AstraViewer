using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.IO;
using System.Drawing;

//using VectorDraw.Serialize;
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

    public class JointCoordinate
    {
        int iNodeNo;
        gPoint gpPts;
        
        public JointCoordinate(int nodeNo, double x, double y, double z)
        {
            iNodeNo = nodeNo;
            gpPts.x = x;
            gpPts.y = y;
            gpPts.z = z;
        }
        public JointCoordinate(int nodeNo, gPoint Pts)
        {
            iNodeNo = nodeNo;
            gpPts = Pts;
        }
        public JointCoordinate(JointCoordinate objData)
        {
            this.iNodeNo = objData.iNodeNo;
            this.gpPts = objData.gpPts;
        }
        public JointCoordinate()
        {
            iNodeNo = 0;
            gpPts = new gPoint();
        }
        public int NodeNo
        {
            get
            {
                return iNodeNo;
            }
            set
            {
                iNodeNo = value;
            }
        }
        public gPoint Point
        {
            get
            {
                return gpPts;
            }
            set
            {
                gpPts = value;
            }
        }
        public static JointCoordinate ParseAST(string data)
        {
            //Example
            //N001 UNIT 1.000 1.000 KIP FT KIP FT NODE, x[NODE]*lfact, y[NODE]*lfact, z[NODE]*lfact, TX, TY, TZ, RX, RY, RZ
            //N001	1	0	0	0	0	0	0	0	0	0
            //N001	2	30	0	0	0	0	0	0	0	0
            //......
            //......
            string temp = data.Trim().TrimEnd().TrimStart();
            temp = temp.Replace('\t', ' ');

            while (temp.IndexOf("  ") != -1)
            {
                temp = temp.Replace("  ", " ");
            }

            string[] values = temp.Split(new char[] { ' ' });

            JointCoordinate obj = new JointCoordinate();
            if (values.Length != 11) throw new Exception("String Data is not correct format.");
            obj.NodeNo = int.Parse(values[1]);
            obj.Point.x = double.Parse(values[2]);
            obj.Point.y = double.Parse(values[3]);
            obj.Point.z = double.Parse(values[4]);
            return obj;
        }
        public static JointCoordinate ParseTXT(string data)
        {
            // JOINT COORDINATES
            // 1 0 0 0 ; 2 30 0 0
            // 3 0 20 0 ; 4 10 20 0 ; 5 20 20 0 ; 6 30 20 0
            // 7 0 35 0 ; 8 30 35 0 ; 9 7.5 35 0 ; 10 22.5 35 0 ; 11 15 35 0
            // 12 5 38 0 ; 13 25 38 0 ; 14 10 41 0 ; 15 20 41 0 ; 16 15 44 0
            string temp = data.Trim().TrimEnd().TrimStart();
            temp = temp.Replace('\t', ' ');

            while (temp.IndexOf("  ") != -1)
            {
                temp = temp.Replace("  ", " ");
            }

            string[] values = temp.Split(new char[] { ' ' });

            JointCoordinate obj = new JointCoordinate();
            if (values.Length != 4) throw new Exception("String Data is not correct format.");
            obj.NodeNo = int.Parse(values[0]);
            obj.Point.x = double.Parse(values[1]);
            obj.Point.y = double.Parse(values[2]);
            obj.Point.z = double.Parse(values[3]);
            return obj;
        }
        public override string ToString()
        {
            //N001     1     0.000     0.000     0.000    0    0    0    0    0    0

            string kStr = string.Format("N001 {0,7:f0} {1,10:f3} {2,10:f3}  {3,10:f3}   0    0    0    0    0    0",
                iNodeNo,
                gpPts.x,
                gpPts.y,
                gpPts.z);

            return kStr;
        }
        public double X
        {
            get
            {
                return Point.x;
            }
            set
            {
                Point.x = value;
            }
        }
        public double Y
        {
            get
            {
                return Point.y;
            }
            set
            {
                Point.y = value;
            }
        }
        public double Z
        {
            get
            {
                return Point.z;
            }
            set
            {
                Point.z = value;
            }
        }


        public void DrawJointsText(vdDocument doc, double txtSize)
        {

            vdLayer nodalLay = null;

            nodalLay = doc.Layers.FindName("Nodes");
            if (nodalLay == null)
            {
                nodalLay = new vdLayer();
                nodalLay.Name = "Nodes";
                nodalLay.SetUnRegisterDocument(doc);
                nodalLay.setDocumentDefaults();
                doc.Layers.AddItem(nodalLay);

            }
            nodalLay.PenColor = new vdColor(Color.Magenta);
            //nodalLay.PenColor = new vdColor(Color.Green);
            nodalLay.Update();


            vdMText vTxt = new vdMText();
            vTxt.SetUnRegisterDocument(doc);
            vTxt.setDocumentDefaults();
            vTxt.InsertionPoint = Point;
            vTxt.Height = txtSize;
            vTxt.HorJustify = VdConstHorJust.VdTextHorCenter;
            vTxt.VerJustify = VdConstVerJust.VdTextVerCen;
            vTxt.TextString = NodeNo.ToString();
            vTxt.Layer = nodalLay;
            doc.ActiveLayOut.Entities.AddItem(vTxt);


            vdCircle vcir = new vdCircle();
            vcir.SetUnRegisterDocument(doc);
            vcir.setDocumentDefaults();
            vcir.Center = Point;
            vcir.Radius = txtSize;
            //vcir.TextString = list[i].NodeNo.ToString();
            vcir.Layer = nodalLay;
            doc.ActiveLayOut.Entities.AddItem(vcir);


            doc.Update();
            doc.Redraw(true);

        }


    }
    public class JointCoordinateCollection : IList<JointCoordinate>
    {
        List<JointCoordinate> list;
        public SupportCollection sptCol = new SupportCollection();
        string kStr = "";
        double max_x_positive, max_x_negative;
        double max_y_positive, max_y_negative;
        double max_z_positive, max_z_negative;

        public JointCoordinateCollection()
        {
            BoundingBox = new Box();
            list = new List<JointCoordinate>();
            max_x_positive = 0.0d;
            max_x_negative = 0.0d;
            max_y_positive = 0.0d;
            max_y_negative = 0.0d;
            max_z_positive = 0.0d;
            max_z_negative = 0.0d;
        }
        #region MAX and MIN

        public double Max_X_Positive
        {
            get
            {
                return max_x_positive;
            }
            set
            {
                max_x_positive = value;
            }
        }
        public double Max_Y_Positive
        {
            get
            {
                return max_y_positive;
            }
            set
            {
                max_y_positive = value;
            }
        }
        public double Max_Z_Positive
        {
            get
            {
                return max_z_positive;
            }
            set
            {
                max_z_positive = value;
            }
        }

        public double Max_X_Negative
        {
            get
            {
                return max_x_negative;
            }
            set
            {
                max_x_negative = value;
            }
        }
        public double Max_Y_Negative
        {
            get
            {
                return max_y_negative;
            }
            set
            {
                max_y_negative = value;
            }
        }
        public double Max_Z_Negative
        {
            get
            {
                return max_z_negative;
            }
            set
            {
                max_z_negative = value;
            }
        }

        #endregion
        public JointCoordinateCollection(string FileName)
        {
            list = new List<JointCoordinate>();

            try
            {
                StreamReader sr = new StreamReader(new FileStream(FileName, FileMode.Open, FileAccess.Read));
                while (!sr.EndOfStream)
                {
                    //this.Add(sr.ReadLine());
                    kStr = sr.ReadLine().ToUpper();
                    AddTXT(kStr);

                    if (kStr.Contains("SUPPORT"))
                    {
                        kStr = sr.ReadLine();
                        sptCol.AddTXT(kStr);
                    }
                }
                sr.Close();
                sptCol.CopyFromCoordinateCollection(this);
                
            }
            catch (Exception exx)
            {
            }


        }


        //public AddP
        public Box BoundingBox { get; set; }

        #region IList<JointCoordinate> Members

        public int IndexOf(JointCoordinate item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].NodeNo == item.NodeNo)
                {
                    return i;
                }
            }
            return -1;
        }
        public int IndexOf(int NodeNo)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].NodeNo == NodeNo)
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, JointCoordinate item)
        {
            
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public JointCoordinate this[int index]
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

        #region ICollection<JointCoordinate> Members

        public void Add(JointCoordinate item)
        {
            BoundingBox.AddPoint(item.Point);
            list.Add(item);

            if (item.Point.x > 0)
            {
                if (max_x_positive < item.Point.x)
                    max_x_positive = item.Point.x;
            }
            else if (item.Point.x < 0)
            {
                if (max_x_negative < item.Point.x)
                    max_x_negative = item.Point.x;
            }
            if (item.Point.y > 0)
            {
                if (max_y_positive < item.Point.y)
                    max_y_positive = item.Point.y;
            }
            else if (item.Point.y < 0)
            {
                if (max_y_negative < item.Point.y)
                    max_y_negative = item.Point.y;
            }
            if (item.Point.z > 0)
            {
                if (max_z_positive < item.Point.z)
                    max_z_positive = item.Point.z;
            }
            else if (item.Point.z < 0)
            {
                if (max_z_negative < item.Point.z)
                    max_z_negative = item.Point.z;
            }
        }
        public void AddTXT(string itemCollection)
        {
            itemCollection = itemCollection.Replace('\t', ' ');
            string[] values = itemCollection.Split(new char[] { ';' });
            List<string> lstStr = new List<string>();

            int n1 = 0, n2 = 0, kdif = 0;
            double dx = 0.0d, dy = 0.0d, dz = 0.0d;
            double x = 0.0d, y = 0.0d, z = 0.0d;
            double kx = 0.0d, ky = 0.0d, kz = 0.0d;
            int addFactor = 0, nodeDif = 0;


            foreach (string str1 in values)
            {
                JointCoordinate jc;
                string str;
                str = str1.Trim().TrimEnd().TrimStart();
                try
                {
                    lstStr.Clear();
                    lstStr.AddRange(str.Split(new char[] { ' ' }));
                    // Example:
                    //1 0 0 0 ;
                    if (lstStr.Count == 4)
                    {
                        Add(JointCoordinate.ParseTXT(str));
                    }
                    // Example:
                    // 4	0	0	31.125	1
                    else if(lstStr.Count == 5)
                    {
                        n1 = list[list.Count - 1].NodeNo;
                        x = list[list.Count - 1].Point.x;
                        y = list[list.Count - 1].Point.y;
                        z = list[list.Count - 1].Point.z;
                        
                        n2 = int.Parse(lstStr[0]);
                        dx = double.Parse(lstStr[1]);
                        dy = double.Parse(lstStr[2]);
                        dz = double.Parse(lstStr[3]);

                        addFactor = int.Parse(lstStr[4]);

                        nodeDif = (n2 - n1) / addFactor;

                        //kx = (x + dx) / (nodeDif);
                        //ky = (y + dy) / (nodeDif);
                        //kz = (z + dz) / (nodeDif);
                        kx = (dx - x) / (nodeDif);
                        ky = (dy - y) / (nodeDif);
                        kz = (dz - z) / (nodeDif);

                        for (int i = 1; i < nodeDif; i++)
                        {
                            jc = new JointCoordinate();
                            jc.NodeNo = n1 + addFactor*i;
                            jc.Point = new gPoint(x + kx * i,y+ ky*i,z + kz*i);
                            Add(jc);
                        }
                        jc = new JointCoordinate();
                        jc.NodeNo = n2;
                        jc.Point = new gPoint(dx, dy, dz);
                        Add(jc);
                    }
                    // Example:
                    //1 0 0 0 5 20 0 0 ;
                    else if (lstStr.Count == 8)
                    {
                        try
                        {
                            int indx = 0;
                            n1 = int.Parse(lstStr[indx]); indx++;
                            x = double.Parse(lstStr[indx]); indx++;
                            y = double.Parse(lstStr[indx]); indx++;
                            z = double.Parse(lstStr[indx]); indx++;

                            jc = new JointCoordinate();
                            jc.NodeNo = n1;
                            jc.Point = new gPoint(x, y, z);
                            Add(jc);


                            n2 = int.Parse(lstStr[indx]); indx++;
                            dx = double.Parse(lstStr[indx]); indx++;
                            dy = double.Parse(lstStr[indx]); indx++;
                            dz = double.Parse(lstStr[indx]); indx++;

                            kdif = n2 - n1;

                            kx = (x + dx) / (kdif);
                            ky = (y + dy) / (kdif);
                            kz = (z + dz) / (kdif);

                            for (int i = 1; i < kdif; i++)
                            {
                                jc = new JointCoordinate();
                                jc.NodeNo = n1 + i;
                                jc.Point = new gPoint(x + kx * i, y + ky*i, z + kz*i);
                                Add(jc);
                            }
                            jc = new JointCoordinate();
                            jc.NodeNo = n2;
                            jc.Point = new gPoint(dx, dy, dz);
                            Add(jc);
                        }
                        catch (Exception exx)
                        {
                        }
                    }
                }
                catch (Exception exx)
                {
                }
            }
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(JointCoordinate item)
        {
            if (this.IndexOf(item) == -1) return false;
            return true;
        }

        public void CopyTo(JointCoordinate[] array, int arrayIndex)
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

        public bool Remove(JointCoordinate item)
        {
            return list.Remove(item);
        }

        #endregion

        #region IEnumerable<JointCoordinate> Members

        public IEnumerator<JointCoordinate> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
            //MyClass<string> nn = new MyClass<string>();

        }
        #endregion

        public List<double> Get_Floors()
        {
            List<double> list = new List<double>();
            foreach (var item in this)
            {
                if (!list.Contains(item.Y) && item.Y > 0)
                {
                    list.Add(item.Y);
                }
            }
            return list;
        }

        vdLayer nodalLay = null;
        public void DrawJointsText(vdDocument doc,double txtSize)
        {
            
            nodalLay = doc.Layers.FindName("Nodes");
            if (nodalLay == null)
            {
                nodalLay = new vdLayer();
                nodalLay.Name = "Nodes";
                nodalLay.SetUnRegisterDocument(doc);
                nodalLay.setDocumentDefaults();
                doc.Layers.AddItem(nodalLay);
               
            }
            nodalLay.PenColor = new vdColor(Color.Magenta);
            //nodalLay.PenColor = new vdColor(Color.Green);
            nodalLay.Update();

            for (int i = 0; i < list.Count; i++)
            {
                //vdMText vTxt = new vdMText();
                //vTxt.SetUnRegisterDocument(doc);
                //vTxt.setDocumentDefaults();
                //vTxt.InsertionPoint = list[i].Point;
                //vTxt.Height = txtSize;
                //vTxt.HorJustify = VdConstHorJust.VdTextHorCenter;
                //vTxt.VerJustify = VdConstVerJust.VdTextVerCen;
                //vTxt.TextString = list[i].NodeNo.ToString();
                //vTxt.Layer = nodalLay;
                //doc.ActiveLayOut.Entities.AddItem(vTxt);

                vdText vTxt = new vdText();
                vTxt.SetUnRegisterDocument(doc);
                vTxt.setDocumentDefaults();
                vTxt.InsertionPoint = list[i].Point;
                vTxt.Height = txtSize;
                //vTxt.HorJustify = VdConstHorJust.VdTextHorCenter;
                //vTxt.VerJustify = VdConstVerJust.VdTextVerCen;
                vTxt.TextString = list[i].NodeNo.ToString();

                vTxt.ToolTip = string.Format("Joint No : {0} [X:{1:f4}, Y:{2:f4}, Z:{3:f4}",
                    list[i].NodeNo, list[i].Point.x, list[i].Point.y, list[i].Point.z);

                vTxt.Layer = nodalLay;
                doc.ActiveLayOut.Entities.AddItem(vTxt);


                //vdCircle vcir = new vdCircle();
                //vcir.SetUnRegisterDocument(doc);
                //vcir.setDocumentDefaults();
                //vcir.Center = list[i].Point;
                //vcir.Radius = txtSize;
                //vcir.Layer = nodalLay;
                //doc.ActiveLayOut.Entities.AddItem(vcir);
            }
            doc.Update();
            doc.Redraw(true);

        }
    }

    //http://www.google.com
}
