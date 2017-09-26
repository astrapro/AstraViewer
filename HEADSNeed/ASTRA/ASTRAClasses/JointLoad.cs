using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

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

using HEADSNeed.ASTRA.ASTRADrawingTools;


namespace HEADSNeed.ASTRA.ASTRAClasses
{
    public class JointLoad
    {
        JointCoordinate jc = null;
        eDirection direction;
        double dValue = 0.0d;
        int iLoadCase = 0;
        double dFx, dFy, dFz, dMx, dMy, dMz;

        public string Tag { get; set; }


        public JointLoad()
        {
            jc = new JointCoordinate();
            direction = eDirection.FX;
            dValue = 0.0;

            Tag = "";
        }

        public JointCoordinate Joint
        {
            get
            {
                return jc;
            }
            set
            {
                jc = value;
            }
        }
        public eDirection Direction
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
            }
        }
        public double Value
        {
            get
            {
                if (dValue == 0.0d)
                {
                    if (dFx != 0.0d)
                    {
                        dValue = dFx;
                    }
                    if (dFy != 0.0d)
                    {
                        dValue = dFy;
                    }
                    if (dFz != 0.0d)
                    {
                        dValue = dFz;
                    }
                }
                return dValue;
            }
            set
            {
                dValue = value;
            }
        }
        public enum eDirection
        {
            FX = 0,
            FY = 1,
            FZ = 2,
            MX = 3,
            MY = 4,
            MZ = 5,
        }
        public int LoadCase
        {
            get
            {
                return iLoadCase;
            }
            set
            {
                iLoadCase = value;
            }
        }
        public double Fx
        {
            get
            {
                return dFx;
            }
            set
            {
                dFx = value;
            }
        }
        public double Fy
        {
            get
            {
                return dFy;
            }
            set
            {
                dFy = value;
            }
        }
        public double Fz
        {
            get
            {
                return dFz;
            }
            set
            {
                dFz = value;
            }
        }
        public double Mx
        {
            get
            {
                return dMx;
            }
            set
            {
                dMx = value;
            }
        }
        public double My
        {
            get
            {
                return dMy;
            }
            set
            {
                dMy = value;
            }
        }
        public double Mz
        {
            get
            {
                return dMz;
            }
            set
            {
                dMz = value;
            }
        }
        public void AddLoad(string data)
        {
            // JOINT LOAD
            // 4 5 FY -15 ; 11 FY -30
            //JOINT LOAD
            //10 FZ 8.5
            //11 FZ 20.0
            //12 FZ 16.0
            //15 16 FZ 8.5
            string[] values = data.Split(new char[] { ';' });
            List<string> lstStr = new List<string>();
            foreach (string s in values)
            {
                lstStr.Clear();
                lstStr.AddRange(s.Split(new char[] { ' ' }));
            }
        }
        public static JointLoad ParseAST(string s, bool IsMovindLoad)
        {
            // Example 01
            //N007  UNIT 1.000 0.083 KIP FT KIP IN NODE#, loadcase#, fx*wfact, fy*wfact, fz*wfact, mx*wfact*lfact, my*wfact*lfact, mz*wfact*lfact

            //N007    10     1      0.000      0.000      8.500      0.000      0.000      0.000
            //N007    11     1      0.000      0.000     20.000      0.000      0.000      0.000
            //N007    12     1      0.000      0.000     16.000      0.000      0.000      0.000
            //N007    15     1      0.000      0.000      8.500      0.000      0.000      0.000
            //N007    16     1      0.000      0.000      8.500      0.000      0.000      0.000

            string temp = MyStrings.RemoveAllSpaces(s);
            MyStrings mList = new MyStrings(temp, ' ');
            JointLoad jld = new JointLoad();
            if (mList.StringList[0].Contains("N007") || IsMovindLoad)
            {
                int i = 1;

                if (IsMovindLoad)
                    i = 0;

                jld.Joint.NodeNo = mList.GetInt(i); i++;
                jld.LoadCase = mList.GetInt(i); i++;
                jld.Fx = mList.GetDouble(i); i++;
                jld.Fy = mList.GetDouble(i); i++;
                jld.Fz = mList.GetDouble(i); i++;
                jld.Mx = mList.GetDouble(i); i++;
                jld.My = mList.GetDouble(i); i++;
                jld.Mz = mList.GetDouble(i); i++;

                if (jld.Fx != 0.0d)
                {
                    jld.Direction = eDirection.FX;
                }
                if (jld.Fy != 0.0d)
                    jld.Direction = eDirection.FY;
                if (jld.Fz != 0.0d)
                    jld.Direction = eDirection.FZ;


                if (jld.Direction == eDirection.FX && jld.LoadCase == 121)
                {
                    jld.Direction = eDirection.FY;
                }
            }
            else
            {
                throw new Exception("N007 not found.");
            }
            return jld;
        }

    }
    public class JointLoadCollection : IList<JointLoad>
    {
        List<JointLoad> list = new List<JointLoad>();



        public Hashtable hashJointLoad = new Hashtable();


        vdLayer jointLoadsLay = new vdLayer();
        bool bIsWork = false;
        public JointLoadCollection()
        {
            list = new List<JointLoad>();
            jointLoadsLay = new vdLayer();
            bIsWork = false;
            //jointLoadsLay.PenColor = new vdColor(Color.Red);
        }

        #region IList<JointLoad> Members

        public int IndexOf(JointLoad item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Joint.NodeNo == item.Joint.NodeNo)
                {
                    return i;
                }
            }
            return -1;
        }
        public int IndexOf(int NodeNo, int LoadCase)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Joint.NodeNo == NodeNo && list[i].LoadCase == LoadCase)
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
                if (list[i].Joint.NodeNo == NodeNo)
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, JointLoad item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public JointLoad this[int index]
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

        #region ICollection<JointLoad> Members

        public void Add(JointLoad item)
        {
            if (item.Fx == 0.0 && item.Fy == 0.0 && item.Fz == 0.0 &&
                item.Mx == 0.0 && item.My == 0.0 && item.Mz == 0.0)
            {
                return;
            }
            list.Add(item);

            //string ss = item.Joint.NodeNo + " " + item.LoadCase;

            // Chiranjit [2010 05 18]
            try
            {
                List<JointLoad> ll = hashJointLoad[item.LoadCase] as List<JointLoad>;

                if (ll == null)
                {
                    ll = new List<JointLoad>();
                    ll.Add(item);
                    hashJointLoad.Add(item.LoadCase, ll);
                }
                else
                {
                    hashJointLoad.Remove(item.LoadCase);
                    ll.Add(item);
                    hashJointLoad.Add(item.LoadCase, ll);
                }
            }
            catch (Exception ex) { }
        }

        public void AddAST(string data)
        {
            //Example

            //N007  UNIT 1.000 1.000 KIP FT KIP FT NODE#, loadcase#, fx*wfact, fy*wfact, fz*wfact, mx*wfact*lfact, my*wfact*lfact, mz*wfact*lfact
            //N007     4     1      0.000    -15.000      0.000      0.000      0.000      0.000
            //N007     5     1      0.000    -15.000      0.000      0.000      0.000      0.000
            //N007    11     1      0.000    -30.000      0.000      0.000      0.000      0.000
            string temp = data.Trim().TrimEnd().TrimStart();
            temp = temp.Replace('\t', ' ');
            while (temp.IndexOf("  ") != -1)
            {
                temp = temp.Replace("  ", " ");
            }
            string[] values = temp.Split(new char[] { ' ' });
            if (values.Length != 9) throw new Exception("String Data is not correct format.");

            JointLoad jld = new JointLoad();
            jld.Joint.NodeNo = int.Parse(values[1]);
            jld.LoadCase = int.Parse(values[2]);

            double d = double.Parse(values[3]);
            if (d != 0.0)
            {
                jld.Direction = JointLoad.eDirection.FX;
                jld.Fx = d;
                jld.Value = d;
            }
            d = double.Parse(values[4]);
            if (d != 0.0)
            {
                jld.Direction = JointLoad.eDirection.FY;
                jld.Fy = d;
                jld.Value = d;
            }
            d = double.Parse(values[5]);
            if (d != 0.0)
            {
                jld.Direction = JointLoad.eDirection.FZ;
                jld.Fz = d;
                jld.Value = d;
            }
            Add(jld);
        }
        public void AddTXT(string data, int loadCase)
        {
            string temp = data;
            data = data.Replace('\t', ' ');
            // JOINT LOAD
            // 4 5 FY -15 ; 11 FY -30
            //JOINT LOAD
            //10 FZ 8.5
            //11 FZ 20.0
            //12 FZ 16.0
            //15 16 FZ 8.5

            JointLoad jlds;


            MyStrings ss = new MyStrings(data, '*');

            string coment = "";

            if (ss.Count > 1)
            {
                data = ss.StringList[0];
                coment = ss.StringList[1];
            }


            string[] values = data.Split(new char[] { ';' });
            List<string> lstStr = new List<string>();
            foreach (string s in values)
            {

                lstStr.Clear();
                lstStr.AddRange(s.Trim().TrimEnd().TrimStart().Split(new char[] { ' ' }));

                MyStrings mlist = new MyStrings(MyStrings.RemoveAllSpaces(s), ' ');
                double val = 0.0d;
                int indx = 0;

                #region FX
                if (lstStr.Contains("FX"))
                {
                    try
                    {
                        indx = lstStr.IndexOf("FX");
                        List<int> lst = MyStrings.Get_Array_Intiger(mlist.GetString(0, indx - 1));
                        val = double.Parse(lstStr[indx + 1]);
                        for (int i = 0; i < lst.Count; i++)
                        {
                            jlds = new JointLoad();
                            jlds.Direction = JointLoad.eDirection.FX;
                            jlds.Value = val;
                            jlds.Fx = val;
                            jlds.Joint.NodeNo = lst[i];
                            jlds.LoadCase = loadCase;

                            jlds.Tag = coment;

                            Add(jlds);
                        }
                    }
                    catch (Exception ex) { }
                }
                #endregion FX
                #region FY
                if (lstStr.Contains("FY"))
                {
                    try
                    {
                        indx = lstStr.IndexOf("FY");
                        List<int> lst = MyStrings.Get_Array_Intiger(mlist.GetString(0, indx - 1));
                        val = double.Parse(lstStr[indx + 1]);
                        for (int i = 0; i < lst.Count; i++)
                        {
                            jlds = new JointLoad();
                            jlds.Direction = JointLoad.eDirection.FY;
                            jlds.Value = val;
                            jlds.Fy = val;
                            jlds.Joint.NodeNo = lst[i];
                            jlds.LoadCase = loadCase;
                            jlds.Tag = coment;
                            Add(jlds);
                        }
                    }
                    catch (Exception ex) { }
                }
                #endregion FY
                #region FZ
                if (lstStr.Contains("FZ"))
                {
                    try
                    {
                        indx = lstStr.IndexOf("FZ");
                        List<int> lst = MyStrings.Get_Array_Intiger(mlist.GetString(0, indx - 1));
                        val = double.Parse(lstStr[indx + 1]);
                        for (int i = 0; i < lst.Count; i++)
                        {
                            jlds = new JointLoad();
                            jlds.Direction = JointLoad.eDirection.FZ;
                            jlds.Value = val;
                            jlds.Fz = val;
                            jlds.Joint.NodeNo = lst[i];
                            jlds.LoadCase = loadCase;
                            jlds.Tag = coment;
                            Add(jlds);
                        }
                    }
                    catch (Exception ex) { }
                }
                #endregion FZ

                #region MX
                if (lstStr.Contains("MX"))
                {
                    try
                    {
                        indx = lstStr.IndexOf("MX");
                        List<int> lst = MyStrings.Get_Array_Intiger(mlist.GetString(0, indx - 1));
                        val = double.Parse(lstStr[indx + 1]);
                        for (int i = 0; i < lst.Count; i++)
                        {
                            jlds = new JointLoad();
                            jlds.Direction = JointLoad.eDirection.MX;
                            jlds.Value = val;
                            jlds.Mx = val;
                            jlds.Joint.NodeNo = lst[i];
                            jlds.LoadCase = loadCase;
                            jlds.Tag = coment;
                            Add(jlds);
                        }
                    }
                    catch (Exception ex) { }
                }
                #endregion MX
                #region MY
                if (lstStr.Contains("MY"))
                {
                    try
                    {
                        indx = lstStr.IndexOf("MY");
                        List<int> lst = MyStrings.Get_Array_Intiger(mlist.GetString(0, indx - 1));
                        val = double.Parse(lstStr[indx + 1]);
                        for (int i = 0; i < lst.Count; i++)
                        {
                            jlds = new JointLoad();
                            jlds.Direction = JointLoad.eDirection.MY;
                            jlds.Value = val;
                            jlds.My = val;
                            jlds.Joint.NodeNo = lst[i];
                            jlds.LoadCase = loadCase;
                            jlds.Tag = coment;
                            Add(jlds);
                        }
                    }
                    catch (Exception ex) { }
                }
                #endregion MY
                #region MZ
                if (lstStr.Contains("MZ"))
                {
                    try
                    {
                        indx = lstStr.IndexOf("MZ");
                        List<int> lst = MyStrings.Get_Array_Intiger(mlist.GetString(0, indx - 1));
                        val = double.Parse(lstStr[indx + 1]);
                        for (int i = 0; i < lst.Count; i++)
                        {
                            jlds = new JointLoad();
                            jlds.Direction = JointLoad.eDirection.MZ;
                            jlds.Value = val;
                            jlds.Mz = val;
                            jlds.Joint.NodeNo = lst[i];
                            jlds.LoadCase = loadCase;
                            jlds.Tag = coment;
                            Add(jlds);
                        }
                    }
                    catch (Exception ex) { }
                }
                #endregion MZ
            }
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(JointLoad item)
        {
            return (IndexOf(item) == -1 ? false : true);
        }

        public void CopyTo(JointLoad[] array, int arrayIndex)
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

        public bool Remove(JointLoad item)
        {
            int indx = 0;
            return false;
        }

        #endregion

        #region IEnumerable<JointLoad> Members

        public IEnumerator<JointLoad> GetEnumerator()
        {
            return list.GetEnumerator();
            //throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion


        public void CopyCoordinates(JointCoordinateCollection jcCol)
        {
            int indx = 0;
            for (int i = 0; i < list.Count; i++)
            {
                indx = jcCol.IndexOf(list[i].Joint);
                if (indx != -1)
                {
                    list[i].Joint = jcCol[indx];
                }
            }
        }

        public void SetLoadCombination(int loadCase, int loadCombination, double loadValue)
        {
            for (int ii = 0; ii < list.Count; ii++)
            {
                if (list[ii].LoadCase == loadCase)
                {
                    JointLoad jload = new JointLoad();
                    jload.Direction = list[ii].Direction;
                    jload.Joint = list[ii].Joint;
                    jload.LoadCase = loadCombination;
                    jload.Value = list[ii].Value * loadValue;
                    Add(jload);
                }
            }
        }
        public void SetLoadCombination(string text, int LoadComb)
        {
            //1 0.50 2 0.75
            //1 0.74 2

            string temp = text.Trim().TrimEnd().TrimStart().Replace('\t', ' ');
            MyStrings mList = new MyStrings(temp, ' ');

            double value = 0.0d;
            int loadCase = -1;
            for (int i = 0; i < mList.StringList.Count; i += 2)
            {
                loadCase = mList.GetInt(i);
                value = mList.GetDouble(i + 1);
                SetLoadCombination(loadCase, LoadComb, value);
            }
        }

        public void DrawJointLoads(vdDocument doc)
        {
            jointLoadsLay = new vdLayer();
            jointLoadsLay.Name = "JointLoad";
            jointLoadsLay.SetUnRegisterDocument(doc);
            jointLoadsLay.setDocumentDefaults();

            doc.Layers.AddItem(jointLoadsLay);
            jointLoadsLay.PenColor = new vdColor(Color.Red);
            for (int i = 0; i < list.Count; i++)
            {
                ASTRAArrowLine arLine = new ASTRAArrowLine();
                arLine.SetUnRegisterDocument(doc);
                arLine.setDocumentDefaults();
                arLine.Layer = jointLoadsLay;

                arLine.arrowSize = 0.5;
                arLine.StartPoint = list[i].Joint.Point;


                vdText loadVal = new vdText();
                loadVal.SetUnRegisterDocument(doc);
                loadVal.setDocumentDefaults();



                if (list[i].Direction == JointLoad.eDirection.FX)
                {
                    if (list[i].Value < 0)
                        arLine.EndPoint = new gPoint(list[i].Joint.Point.x - 1.0d, list[i].Joint.Point.y, list[i].Joint.Point.z);
                    else if (list[i].Value > 0)
                        arLine.EndPoint = new gPoint(list[i].Joint.Point.x + 1.0d, list[i].Joint.Point.y, list[i].Joint.Point.z);
                }
                else if (list[i].Direction == JointLoad.eDirection.FY)
                {
                    if (list[i].Value < 0)
                    {
                        arLine.StartPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y + 1.0d, list[i].Joint.Point.z);
                        arLine.EndPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y, list[i].Joint.Point.z);
                    }
                    else if (list[i].Value > 0)
                    {
                        arLine.StartPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y - 1.0d, list[i].Joint.Point.z);
                        arLine.EndPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y, list[i].Joint.Point.z);
                    }
                }
                else if (list[i].Direction == JointLoad.eDirection.FZ)
                {
                    if (list[i].Value < 0)
                    {
                        arLine.EndPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y, list[i].Joint.Point.z - 1.0d);
                        loadVal.InsertionPoint = new gPoint(arLine.EndPoint.x - 0.1d, arLine.EndPoint.y - 0.5, arLine.EndPoint.z);
                    }
                    else if (list[i].Value > 0)
                    {
                        arLine.EndPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y, list[i].Joint.Point.z + 1.0d);
                        loadVal.InsertionPoint = new gPoint(arLine.EndPoint.x - 0.1d, arLine.EndPoint.y + 0.5, arLine.EndPoint.z);
                    }
                }

                loadVal.Layer = jointLoadsLay;
                if (list[i].Value < 0)
                {
                    loadVal.TextString = Math.Abs(list[i].Value).ToString();
                    loadVal.InsertionPoint = new gPoint(arLine.EndPoint.x - 0.2d, arLine.EndPoint.y - 0.5, arLine.EndPoint.z);
                }
                else if (list[i].Value > 0)
                {
                    loadVal.TextString = Math.Abs(list[i].Value).ToString();
                    loadVal.InsertionPoint = new gPoint(arLine.EndPoint.x - 0.2d, arLine.EndPoint.y, arLine.EndPoint.z);
                }

                loadVal.Height = 0.2d;
                doc.ActiveLayOut.Entities.AddItem(arLine);
                //doc.ActiveLayOut.Entities.AddItem(loadVal);
            }
            doc.Redraw(true);
        }
        public void Delete_ASTRAArrowLine(vdDocument document)
        {
            //ASTRAArrowLine

            for (int i = 0; i < document.ActiveLayOut.Entities.Count; i++)
            {

                if (document.ActiveLayOut.Entities[i].Layer.Name.StartsWith("JointLoad"))
                {
                    document.ActiveLayOut.Entities[i].Deleted = true;
                    document.ActiveLayOut.Entities.RemoveAt(i);
                    i = -1;
                    continue;
                }

                //ASTRAArrowLine aline = document.ActiveLayOut.Entities[i] as ASTRAArrowLine;
                //if (aline != null)
                //{
                //    aline.Deleted = true;
                //    document.ActiveLayOut.Entities.RemoveAt(i);
                //    i = -1;
                //}
                //else
                //{
                //    vdText tx = document.ActiveLayOut.Entities[i] as vdText;
                //    if ((tx != null) && tx.Layer.Name.Contains("JointLoad"))
                //    {
                //        tx.Deleted = true;
                //        document.ActiveLayOut.Entities.RemoveAt(i);
                //        i = -1;
                //    }
                //    else
                //    {
                //        vdCircle cir = document.ActiveLayOut.Entities[i] as vdCircle;
                //        if ((cir != null))
                //        {
                //            cir.Deleted = true;
                //            document.ActiveLayOut.Entities.RemoveAt(i);
                //            i = -1;
                //        }
                //        else
                //        {
                //            vdRect rect = document.ActiveLayOut.Entities[i] as vdRect;
                //            if ((rect != null) && rect.Layer.Name.Contains("JointLoad"))
                //            {
                //                rect.Deleted = true;
                //                document.ActiveLayOut.Entities.RemoveAt(i);
                //                i = -1;
                //            }
                //        }
                //        //if ((cir != null) && cir.Layer.Name.Contains("JointLoad"))
                //        //{
                //        //    cir.Deleted = true;
                //        //    document.ActiveLayOut.Entities.RemoveAt(i);
                //        //    i = -1;
                //        //}
                //    }
                //}
            }
        }
        public void DrawJointLoads_MovingLoad(vdDocument doc, int LoadCase)
        {
            Delete_ASTRAArrowLine(doc);

            jointLoadsLay.Name = "JointLoad";
            if (doc.Layers.FindName("JointLoad") == null)
            {
                jointLoadsLay = new vdLayer();
                jointLoadsLay.Name = "JointLoad";
                jointLoadsLay.SetUnRegisterDocument(doc);
                jointLoadsLay.setDocumentDefaults();

                doc.Layers.AddItem(jointLoadsLay);
            }
            else
            {
                jointLoadsLay = doc.Layers.FindName("JointLoad");
            }
            jointLoadsLay.PenColor = new vdColor(Color.Red);

            List<JointLoad> list_j_load = hashJointLoad[LoadCase] as List<JointLoad>;
            if (list_j_load != null)
            {
                #region Work

                for (int i = 0; i < list_j_load.Count; i++)
                {
                    //if (list_j_load[i].LoadCase != LoadCase) continue;

                    ASTRAArrowLine arLine = new ASTRAArrowLine();
                    arLine.SetUnRegisterDocument(doc);
                    arLine.setDocumentDefaults();
                    arLine.Layer = jointLoadsLay;

                    arLine.arrowSize = 0.5;
                    arLine.StartPoint = list_j_load[i].Joint.Point;


                    vdText loadVal = new vdText();
                    loadVal.SetUnRegisterDocument(doc);
                    loadVal.setDocumentDefaults();

                    if (list_j_load[i].Direction == JointLoad.eDirection.FX)
                    {
                        if (list_j_load[i].Value < 0)
                            arLine.EndPoint = new gPoint(list_j_load[i].Joint.Point.x + 2.0d, list_j_load[i].Joint.Point.y, list_j_load[i].Joint.Point.z);
                        else if (list_j_load[i].Value > 0)
                            arLine.EndPoint = new gPoint(list_j_load[i].Joint.Point.x - 2.0d, list_j_load[i].Joint.Point.y, list_j_load[i].Joint.Point.z);
                    }
                    else if (list_j_load[i].Direction == JointLoad.eDirection.FY)
                    {
                        if (list_j_load[i].Value < 0)
                            arLine.EndPoint = new gPoint(list_j_load[i].Joint.Point.x, list_j_load[i].Joint.Point.y + 2.0d, list_j_load[i].Joint.Point.z);
                        else if (list_j_load[i].Value > 0)
                            arLine.EndPoint = new gPoint(list_j_load[i].Joint.Point.x, list_j_load[i].Joint.Point.y - 2.0d, list_j_load[i].Joint.Point.z);
                    }
                    else if (list_j_load[i].Direction == JointLoad.eDirection.FZ)
                    {
                        if (list_j_load[i].Value < 0)
                        {
                            arLine.EndPoint = new gPoint(list_j_load[i].Joint.Point.x, list_j_load[i].Joint.Point.y, list_j_load[i].Joint.Point.z + 2.0d);
                            loadVal.InsertionPoint = new gPoint(arLine.EndPoint.x - 0.1d, arLine.EndPoint.y - 0.5, arLine.EndPoint.z);
                        }
                        else if (list_j_load[i].Value > 0)
                        {
                            arLine.EndPoint = new gPoint(list_j_load[i].Joint.Point.x, list_j_load[i].Joint.Point.y, list_j_load[i].Joint.Point.z - 2.0d);
                            loadVal.InsertionPoint = new gPoint(arLine.EndPoint.x - 0.1d, arLine.EndPoint.y + 0.5, arLine.EndPoint.z);
                        }
                    }


                    loadVal.Layer = jointLoadsLay;
                    if (list_j_load[i].Value < 0)
                    {
                        loadVal.TextString = Math.Abs(list_j_load[i].Value).ToString();
                        loadVal.InsertionPoint = new gPoint(arLine.EndPoint.x - 0.2d, arLine.EndPoint.y - 0.5, arLine.EndPoint.z);
                    }
                    else if (list_j_load[i].Value > 0)
                    {
                        loadVal.TextString = Math.Abs(list_j_load[i].Value).ToString();
                        loadVal.InsertionPoint = new gPoint(arLine.EndPoint.x - 0.2d, arLine.EndPoint.y, arLine.EndPoint.z);
                    }

                    loadVal.Height = 0.02d;

                    gPoint p = new gPoint(arLine.StartPoint);
                    arLine.StartPoint = new gPoint(arLine.EndPoint);
                    arLine.EndPoint = p;



                    #region Chiranjit Modified [2010 01 01]


                    double radius = 0.1;
                    vdCircle cir = new vdCircle();
                    cir.SetUnRegisterDocument(doc);
                    cir.setDocumentDefaults();
                    cir.Center = new gPoint(arLine.EndPoint.x, arLine.EndPoint.y + radius, arLine.EndPoint.z);
                    cir.Radius = radius;

                    vdHatchProperties hp = new vdHatchProperties();
                    hp.SetUnRegisterDocument(doc);
                    hp.FillMode = VdConstFill.VdFillModeSolid;
                    hp.FillColor = new vdColor(Color.Red);
                    try
                    {
                        cir.HatchProperties = hp;
                    }
                    catch (Exception ex)
                    {

                    }
                    //cir.HatchProperties.FillColor = new vdColor(Color.Red);

                    //cir.Layer = jointLoadsLay;
                    doc.ActiveLayOut.Entities.AddItem(cir);

                    #endregion

                    arLine.EndPoint.y += radius;
                    doc.ActiveLayOut.Entities.AddItem(arLine);
                    doc.ActiveLayOut.Entities.AddItem(loadVal);
                }
                #endregion
            }
            doc.RenderMode = vdRender.Mode.Wire2d;
            //VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);
            //doc.Redraw(true);
        }

        public void DrawJointLoads(vdDocument doc, int LoadCase)
        {
            Delete_ASTRAArrowLine(doc);

            jointLoadsLay.Name = "JointLoad";
            if (doc.Layers.FindName("JointLoad") == null)
            {
                jointLoadsLay = new vdLayer();
                jointLoadsLay.Name = "JointLoad";
                jointLoadsLay.SetUnRegisterDocument(doc);
                jointLoadsLay.setDocumentDefaults();

                doc.Layers.AddItem(jointLoadsLay);
            }
            else
            {
                jointLoadsLay = doc.Layers.FindName("JointLoad");
            }
            jointLoadsLay.PenColor = new vdColor(Color.Red);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].LoadCase != LoadCase) continue;



                if (list[i].Direction == JointLoad.eDirection.FX ||
                    list[i].Direction == JointLoad.eDirection.FY ||
                    list[i].Direction == JointLoad.eDirection.FZ)
                {

                    ASTRAArrowLine arLine = new ASTRAArrowLine();
                    arLine.SetUnRegisterDocument(doc);
                    arLine.setDocumentDefaults();
                    arLine.Layer = jointLoadsLay;

                    //arLine.PenColor = new vdColor(Color.Blue);
                    //arLine.PenColor = new vdColor(Color.Green);
                    //arLine.arrowSize = 0.3;
                   
                    //arLine.arrowSize = 0.5;
                    arLine.StartPoint = list[i].Joint.Point;
                    //arLine.ToolTip = "Joint No :" + list[i].Joint.NodeNo + ", Joint Load = " + list[i].Value + " " + list[i].Direction.ToString() + "\n\r" + list[i].Tag;


                    if (list[i].Tag != "")
                    {
                        arLine.ToolTip = list[i].Tag  + "\n\r" + "Joint No :" + list[i].Joint.NodeNo + ", Joint Load = " + list[i].Value + " " + list[i].Direction.ToString();
                    }
                    else
                    {
                        arLine.ToolTip = "Joint No :" + list[i].Joint.NodeNo + ", Joint Load = " + list[i].Value + " " + list[i].Direction.ToString();
                    }

                    vdText loadVal = new vdText();
                    loadVal.SetUnRegisterDocument(doc);
                    loadVal.setDocumentDefaults();


                    if (list[i].Tag.ToLower().Contains("wind"))
                    {
                        arLine.PenColor = new vdColor(Color.Blue);
                    }
                    else if (list[i].Tag.ToLower().Contains("seismic"))
                    {
                        arLine.PenColor = new vdColor(Color.Green);
                    }
                    else
                    {
                        arLine.PenColor = new vdColor(Color.Red);
                    }

                    if (list[i].Direction == JointLoad.eDirection.FX)
                    {
                        //arLine.PenColor = new vdColor(Color.Green);
                        arLine.arrowSize = 0.3;
                   
                        //if (list[i].Value < 0)
                        //    arLine.EndPoint = new gPoint(list[i].Joint.Point.x + 2.0d, list[i].Joint.Point.y, list[i].Joint.Point.z);
                        //else if (list[i].Value > 0)
                        //    arLine.EndPoint = new gPoint(list[i].Joint.Point.x - 2.0d, list[i].Joint.Point.y, list[i].Joint.Point.z);



                        //if (list[i].Value < 0)
                        //    arLine.EndPoint = new gPoint(list[i].Joint.Point.x - 2.0d, list[i].Joint.Point.y, list[i].Joint.Point.z);
                        //else if (list[i].Value > 0)
                        //    arLine.EndPoint = new gPoint(list[i].Joint.Point.x + 2.0d, list[i].Joint.Point.y, list[i].Joint.Point.z);




                        if (list[i].Value < 0)
                        {
                            arLine.StartPoint = new gPoint(list[i].Joint.Point.x - 2.0d, list[i].Joint.Point.y, list[i].Joint.Point.z);
                            arLine.EndPoint = list[i].Joint.Point;
                        }
                        else if (list[i].Value > 0)
                        {
                            arLine.StartPoint = new gPoint(list[i].Joint.Point.x + 2.0d, list[i].Joint.Point.y, list[i].Joint.Point.z);
                            arLine.EndPoint = list[i].Joint.Point;
                        }


                    }
                    else if (list[i].Direction == JointLoad.eDirection.FY)
                    {

                        //arLine.PenColor = new vdColor(Color.Red);
                        arLine.arrowSize = 0.5;

                        //if (list[i].Value < 0)
                        //    arLine.EndPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y + 2.0d, list[i].Joint.Point.z);
                        //else if (list[i].Value > 0)
                        //    arLine.EndPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y - 2.0d, list[i].Joint.Point.z);



                        //if (list[i].Value < 0)
                        //    arLine.EndPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y - 2.0d, list[i].Joint.Point.z);
                        //else if (list[i].Value > 0)
                        //    arLine.EndPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y + 2.0d, list[i].Joint.Point.z);





                        if (list[i].Value < 0)
                        {
                            arLine.StartPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y - 2.0d, list[i].Joint.Point.z);
                            arLine.EndPoint = list[i].Joint.Point;
                        }
                        else if (list[i].Value > 0)
                        {
                            arLine.StartPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y + 2.0d, list[i].Joint.Point.z);
                            arLine.EndPoint = list[i].Joint.Point;
                        }


                    }
                    else if (list[i].Direction == JointLoad.eDirection.FZ)
                    {

                        //arLine.PenColor = new vdColor(Color.Blue);
                        arLine.arrowSize = 0.4;

                        //if (list[i].Value < 0)
                        //    arLine.EndPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y, list[i].Joint.Point.z + 2.0d);
                        //else if (list[i].Value > 0)
                        //    arLine.EndPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y, list[i].Joint.Point.z - 2.0d);

                        //if (list[i].Value < 0)
                        //    arLine.EndPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y, list[i].Joint.Point.z - 2.0d);
                        //else if (list[i].Value > 0)
                        //    arLine.EndPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y, list[i].Joint.Point.z + 2.0d);



                        if (list[i].Value < 0)
                        {
                            arLine.StartPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y, list[i].Joint.Point.z - 2.0d);
                            arLine.EndPoint = list[i].Joint.Point;
                        }
                        else if (list[i].Value > 0)
                        {
                            arLine.StartPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y, list[i].Joint.Point.z + 2.0d);
                            arLine.EndPoint = list[i].Joint.Point;
                        }
                    }

                    


                    //loadVal.InsertionPoint = arLine.EndPoint;

                    loadVal.InsertionPoint = arLine.StartPoint;

                    loadVal.Layer = jointLoadsLay;

                    loadVal.PenColor = arLine.PenColor;


                    loadVal.TextString = list[i].Value.ToString();
                    //if (list[i].Value < 0)
                    //{
                    //    //loadVal.TextString = Math.Abs(list[i].Value).ToString();
                    //    //loadVal.InsertionPoint = new gPoint(arLine.EndPoint.x - 0.2d, arLine.EndPoint.y - 0.5, arLine.EndPoint.z);


                    //    gPoint gp = new gPoint(arLine.EndPoint);


                    //    arLine.EndPoint = arLine.StartPoint;
                    //    arLine.StartPoint = gp;
                    //    loadVal.InsertionPoint = arLine.StartPoint;
                    //}
                    //else if (list[i].Value > 0)
                    //{
                    //    //loadVal.TextString = Math.Abs(list[i].Value).ToString();
                    //    //loadVal.InsertionPoint = new gPoint(arLine.EndPoint.x - 0.2d, arLine.EndPoint.y, arLine.EndPoint.z);
                    //}

                    loadVal.Height = 0.02d;

                    gPoint p = new gPoint(arLine.StartPoint);
                    arLine.StartPoint = new gPoint(arLine.EndPoint);
                    arLine.EndPoint = p;
                    doc.ActiveLayOut.Entities.AddItem(arLine);
                    doc.ActiveLayOut.Entities.AddItem(loadVal);
                }
                else
                {

                    string fn_jnl_neg = Path.Combine(Application.StartupPath, @"DrawCAD\joint_mement_neg.vdml");
                    string fn_jnl_pos = Path.Combine(Application.StartupPath, @"DrawCAD\joint_mement_pos.vdml");

                    if (!File.Exists(fn_jnl_neg)) return;
                    if (!File.Exists(fn_jnl_pos)) return;

                    if (doc.Blocks.FindItem(doc.Blocks.FindName(Path.GetFileNameWithoutExtension(fn_jnl_neg))) == false)
                        doc.Blocks.AddFromFile(fn_jnl_neg, false);

                    if (doc.Blocks.FindItem(doc.Blocks.FindName(Path.GetFileNameWithoutExtension(fn_jnl_pos))) == false)
                        doc.Blocks.AddFromFile(fn_jnl_pos, false);

                    fn_jnl_neg = Path.GetFileNameWithoutExtension(fn_jnl_neg);
                    fn_jnl_pos = Path.GetFileNameWithoutExtension(fn_jnl_pos);

                    vdInsert vdIns = new vdInsert();
                    vdIns.SetUnRegisterDocument(doc);
                    vdIns.setDocumentDefaults();

                    if (list[i].Value < 0)
                        vdIns.Block = doc.Blocks.FindName(fn_jnl_neg);
                    else
                        vdIns.Block = doc.Blocks.FindName(fn_jnl_pos);
                    //vdIns.Block = doc.Blocks[doc.Blocks.Count - 1];
                    //vdIns.InsertionPoint = new gPoint(list[i].X, list[i].Y, list[i].Z);
                    vdIns.InsertionPoint = list[i].Joint.Point;


                    vdIns.Layer = jointLoadsLay;


                    vdIns.ToolTip = "Joint No :" + list[i].Joint.NodeNo + ", Joint Load = " + list[i].Value + " " + list[i].Direction.ToString();



                    if (list[i].Direction == JointLoad.eDirection.MX)
                    {
                        vdIns.ExtrusionVector = new Vector(1.0, 0, 0);
                    }
                    else if (list[i].Direction == JointLoad.eDirection.MY)
                    {
                        vdIns.ExtrusionVector = new Vector(0, 1.0, 0);
                    }
                    else if (list[i].Direction == JointLoad.eDirection.MZ)
                    {
                        vdIns.ExtrusionVector = new Vector(0, 0, 1.0);
                    }


                    //loadVal.Layer = jointLoadsLay;
                    //if (list[i].Value < 0)
                    //{
                    //    loadVal.TextString = Math.Abs(list[i].Value).ToString();
                    //    loadVal.InsertionPoint = new gPoint(arLine.EndPoint.x - 0.2d, arLine.EndPoint.y - 0.5, arLine.EndPoint.z);
                    //}
                    //else if (list[i].Value > 0)
                    //{
                    //    loadVal.TextString = Math.Abs(list[i].Value).ToString();
                    //    loadVal.InsertionPoint = new gPoint(arLine.EndPoint.x - 0.2d, arLine.EndPoint.y, arLine.EndPoint.z);
                    //}

                    //loadVal.Height = 0.02d;

                    //gPoint p = new gPoint(arLine.StartPoint);
                    //arLine.StartPoint = new gPoint(arLine.EndPoint);
                    //arLine.EndPoint = p;
                    doc.ActiveLayOut.Entities.AddItem(vdIns);
                    //doc.ActiveLayOut.Entities.AddItem(loadVal);
                }
            }
            doc.RenderMode = vdRender.Mode.Wire2d;
            //VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);
            //doc.Redraw(true);
        }
        public void DrawJointLoads(vdDocument doc, int LoadCase, string loadtext)
        {
            Delete_ASTRAArrowLine(doc);

            MyStrings mlist = new MyStrings(MyStrings.RemoveAllSpaces(loadtext), ' ');

            List<int> mems = MyStrings.Get_Array_Intiger(loadtext);

            

            jointLoadsLay.Name = "JointLoad";
            if (doc.Layers.FindName("JointLoad") == null)
            {
                jointLoadsLay = new vdLayer();
                jointLoadsLay.Name = "JointLoad";
                jointLoadsLay.SetUnRegisterDocument(doc);
                jointLoadsLay.setDocumentDefaults();

                doc.Layers.AddItem(jointLoadsLay);
            }
            else
            {
                jointLoadsLay = doc.Layers.FindName("JointLoad");
            }
            jointLoadsLay.PenColor = new vdColor(Color.Red);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].LoadCase != LoadCase) continue;
                if (!mems.Contains(list[i].Joint.NodeNo)) continue;



                if (list[i].Direction == JointLoad.eDirection.FX ||
                    list[i].Direction == JointLoad.eDirection.FY ||
                    list[i].Direction == JointLoad.eDirection.FZ)
                {

                    ASTRAArrowLine arLine = new ASTRAArrowLine();
                    arLine.SetUnRegisterDocument(doc);
                    arLine.setDocumentDefaults();
                    arLine.Layer = jointLoadsLay;

                    arLine.arrowSize = 0.5;
                    arLine.StartPoint = list[i].Joint.Point;
                    arLine.ToolTip = "Joint No :" + list[i].Joint.NodeNo + ", Joint Load = " + list[i].Value + " " + list[i].Direction.ToString();

                    if (list[i].Direction == JointLoad.eDirection.FX)
                    {
                        if (list[i].Value < 0)
                            arLine.EndPoint = new gPoint(list[i].Joint.Point.x + 2.0d, list[i].Joint.Point.y, list[i].Joint.Point.z);
                        else if (list[i].Value > 0)
                            arLine.EndPoint = new gPoint(list[i].Joint.Point.x - 2.0d, list[i].Joint.Point.y, list[i].Joint.Point.z);
                    }
                    else if (list[i].Direction == JointLoad.eDirection.FY)
                    {
                        if (list[i].Value < 0)
                            arLine.EndPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y + 2.0d, list[i].Joint.Point.z);
                        else if (list[i].Value > 0)
                            arLine.EndPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y - 2.0d, list[i].Joint.Point.z);
                    }
                    else if (list[i].Direction == JointLoad.eDirection.FZ)
                    {
                        if (list[i].Value < 0)
                        {
                            arLine.EndPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y, list[i].Joint.Point.z + 2.0d);
                        }
                        else if (list[i].Value > 0)
                        {
                            arLine.EndPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y, list[i].Joint.Point.z - 2.0d);
                        }
                    }


                    gPoint p = new gPoint(arLine.StartPoint);
                    arLine.StartPoint = new gPoint(arLine.EndPoint);
                    arLine.EndPoint = p;
                    doc.ActiveLayOut.Entities.AddItem(arLine);
                }
                else
                {

                    string fn_jnl_neg = Path.Combine(Application.StartupPath, @"DrawCAD\joint_mement_neg.vdml");
                    string fn_jnl_pos = Path.Combine(Application.StartupPath, @"DrawCAD\joint_mement_pos.vdml");

                    if (!File.Exists(fn_jnl_neg)) return;
                    if (!File.Exists(fn_jnl_pos)) return;

                    if (doc.Blocks.FindItem(doc.Blocks.FindName(Path.GetFileNameWithoutExtension(fn_jnl_neg))) == false)
                        doc.Blocks.AddFromFile(fn_jnl_neg, false);

                    if (doc.Blocks.FindItem(doc.Blocks.FindName(Path.GetFileNameWithoutExtension(fn_jnl_pos))) == false)
                        doc.Blocks.AddFromFile(fn_jnl_pos, false);

                    fn_jnl_neg = Path.GetFileNameWithoutExtension(fn_jnl_neg);
                    fn_jnl_pos = Path.GetFileNameWithoutExtension(fn_jnl_pos);

                    vdInsert vdIns = new vdInsert();
                    vdIns.SetUnRegisterDocument(doc);
                    vdIns.setDocumentDefaults();

                    if (list[i].Value < 0)
                        vdIns.Block = doc.Blocks.FindName(fn_jnl_neg);
                    else
                        vdIns.Block = doc.Blocks.FindName(fn_jnl_pos);
                    //vdIns.Block = doc.Blocks[doc.Blocks.Count - 1];
                    //vdIns.InsertionPoint = new gPoint(list[i].X, list[i].Y, list[i].Z);
                    vdIns.InsertionPoint = list[i].Joint.Point;


                    vdIns.Layer = jointLoadsLay;


                    vdIns.ToolTip = "Joint No :" + list[i].Joint.NodeNo + ", Joint Load = " + list[i].Value + " " + list[i].Direction.ToString();



                    if (list[i].Direction == JointLoad.eDirection.MX)
                    {
                        vdIns.ExtrusionVector = new Vector(1.0, 0, 0);
                    }
                    else if (list[i].Direction == JointLoad.eDirection.MY)
                    {
                        vdIns.ExtrusionVector = new Vector(0, 1.0, 0);
                    }
                    else if (list[i].Direction == JointLoad.eDirection.MZ)
                    {
                        vdIns.ExtrusionVector = new Vector(0, 0, 1.0);
                    }


                    //loadVal.Layer = jointLoadsLay;
                    //if (list[i].Value < 0)
                    //{
                    //    loadVal.TextString = Math.Abs(list[i].Value).ToString();
                    //    loadVal.InsertionPoint = new gPoint(arLine.EndPoint.x - 0.2d, arLine.EndPoint.y - 0.5, arLine.EndPoint.z);
                    //}
                    //else if (list[i].Value > 0)
                    //{
                    //    loadVal.TextString = Math.Abs(list[i].Value).ToString();
                    //    loadVal.InsertionPoint = new gPoint(arLine.EndPoint.x - 0.2d, arLine.EndPoint.y, arLine.EndPoint.z);
                    //}

                    //loadVal.Height = 0.02d;

                    //gPoint p = new gPoint(arLine.StartPoint);
                    //arLine.StartPoint = new gPoint(arLine.EndPoint);
                    //arLine.EndPoint = p;
                    doc.ActiveLayOut.Entities.AddItem(vdIns);
                    //doc.ActiveLayOut.Entities.AddItem(loadVal);
                }
            }
            doc.RenderMode = vdRender.Mode.Wire2d;
            //VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);
            //doc.Redraw(true);
        }

        public void DrawPlanMovingJointLoads(vdDocument doc, int LoadCase)
        {
            Delete_ASTRAArrowLine(doc);

            //jointLoadsLay = new vdLayer();
            //jointLoadsLay.Name = "JointLoad";
            //jointLoadsLay.SetUnRegisterDocument(doc);
            //jointLoadsLay.setDocumentDefaults();

            //doc.Layers.AddItem(jointLoadsLay);
            jointLoadsLay.Name = "JointLoad";
            if (doc.Layers.FindName("JointLoad") == null)
            {
                jointLoadsLay = new vdLayer();
                jointLoadsLay.Name = "JointLoad";
                jointLoadsLay.SetUnRegisterDocument(doc);
                jointLoadsLay.setDocumentDefaults();

                doc.Layers.AddItem(jointLoadsLay);
            }
            else
            {
                jointLoadsLay = doc.Layers.FindName("JointLoad");
            }

            jointLoadsLay.PenColor = new vdColor(Color.Red);
            Vertexes vertex = new Vertexes();


            vdHatchProperties hp = new vdHatchProperties();
            hp.SetUnRegisterDocument(doc);
            hp.FillMode = VdConstFill.VdFillModeSolid;
            hp.FillColor = new vdColor(Color.Red);

            for (int i = 0; i < list.Count; i++)
            {

                #region Chiranjit Modified [2010 01 02]
                double radius = 0.15;

                if (list[i].LoadCase != LoadCase) continue;
                if (vertex.FindVertexPoint(list[i].Joint.Point) != -1) continue;
                vertex.Add(list[i].Joint.Point);

                vdCircle cir = new vdCircle();
                cir.SetUnRegisterDocument(doc);
                cir.setDocumentDefaults();


                cir.Center = list[i].Joint.Point;
                cir.Radius = radius;
                cir.ExtrusionVector = new Vector(0, 1, 0);
                cir.HatchProperties = hp;
                doc.ToolTipDispProps.ToolTipAligment = StringAlignment.Near;
                cir.ToolTip = string.Format(@"Joint Force Details
Joint No = {0}
Load Case = {1}
FX = {2:f6}
FY = {3:f6}
FZ = {4:f6}
MX = {5:f6}
MY = {6:f6}
MZ = {7:f6}", list[i].Joint.NodeNo, list[i].LoadCase, list[i].Fx, list[i].Fy, list[i].Fz, list[i].Mx, list[i].My, list[i].Mz);

                //cir.ToolTip = "Joint No : " + list[i].Joint.NodeNo + " " + list[i].Direction.ToString() + " " + list[i].Value.ToString();

                doc.ActiveLayOut.Entities.AddItem(cir);
                #endregion

            }
            doc.RenderMode = vdRender.Mode.Wire2d;
            //VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);
            doc.Redraw(true);
        }
        public void DrawMovingJointLoads(vdDocument doc, int LoadCase)
        {
            Delete_ASTRAArrowLine(doc);

            //jointLoadsLay = new vdLayer();
            //jointLoadsLay.Name = "JointLoad";
            //jointLoadsLay.SetUnRegisterDocument(doc);
            //jointLoadsLay.setDocumentDefaults();

            //doc.Layers.AddItem(jointLoadsLay);
            jointLoadsLay.Name = "JointLoad";
            if (doc.Layers.FindName("JointLoad") == null)
            {
                jointLoadsLay = new vdLayer();
                jointLoadsLay.Name = "JointLoad";
                jointLoadsLay.SetUnRegisterDocument(doc);
                jointLoadsLay.setDocumentDefaults();

                doc.Layers.AddItem(jointLoadsLay);
            }
            else
            {
                jointLoadsLay = doc.Layers.FindName("JointLoad");
            }

            jointLoadsLay.PenColor = new vdColor(Color.Red);




            List<JointLoad> list_j_load = hashJointLoad[LoadCase] as List<JointLoad>;
            if (list_j_load != null)
            {
                #region Work

                for (int i = 0; i < list_j_load.Count; i++)
                {
                    //if (list_j_load[i].LoadCase != LoadCase) continue;


                    ASTRAArrowLine arLine = new ASTRAArrowLine();
                    arLine.SetUnRegisterDocument(doc);
                    arLine.setDocumentDefaults();
                    arLine.Layer = jointLoadsLay;

                    arLine.arrowSize = 0.5;
                    arLine.StartPoint = list_j_load[i].Joint.Point;


                    vdText loadVal = new vdText();
                    loadVal.SetUnRegisterDocument(doc);
                    loadVal.setDocumentDefaults();

                    if (list_j_load[i].Direction == JointLoad.eDirection.FX)
                    {
                        if (list_j_load[i].Value < 0)
                            arLine.EndPoint = new gPoint(list_j_load[i].Joint.Point.x + 2.0d, list_j_load[i].Joint.Point.y, list_j_load[i].Joint.Point.z);
                        else if (list_j_load[i].Value > 0)
                            arLine.EndPoint = new gPoint(list_j_load[i].Joint.Point.x - 2.0d, list_j_load[i].Joint.Point.y, list_j_load[i].Joint.Point.z);
                    }
                    else if (list_j_load[i].Direction == JointLoad.eDirection.FY)
                    {
                        if (list_j_load[i].Value < 0)
                            arLine.EndPoint = new gPoint(list_j_load[i].Joint.Point.x, list_j_load[i].Joint.Point.y + 2.0d, list_j_load[i].Joint.Point.z);
                        else if (list_j_load[i].Value > 0)
                            arLine.EndPoint = new gPoint(list_j_load[i].Joint.Point.x, list_j_load[i].Joint.Point.y - 2.0d, list_j_load[i].Joint.Point.z);
                    }
                    else if (list_j_load[i].Direction == JointLoad.eDirection.FZ)
                    {
                        if (list_j_load[i].Value < 0)
                        {
                            arLine.EndPoint = new gPoint(list_j_load[i].Joint.Point.x, list_j_load[i].Joint.Point.y, list_j_load[i].Joint.Point.z + 2.0d);
                            loadVal.InsertionPoint = new gPoint(arLine.EndPoint.x - 0.1d, arLine.EndPoint.y - 0.5, arLine.EndPoint.z);
                        }
                        else if (list_j_load[i].Value > 0)
                        {
                            arLine.EndPoint = new gPoint(list_j_load[i].Joint.Point.x, list_j_load[i].Joint.Point.y, list_j_load[i].Joint.Point.z - 2.0d);
                            loadVal.InsertionPoint = new gPoint(arLine.EndPoint.x - 0.1d, arLine.EndPoint.y + 0.5, arLine.EndPoint.z);
                        }
                    }


                    loadVal.Layer = jointLoadsLay;
                    if (list_j_load[i].Value < 0)
                    {
                        loadVal.TextString = Math.Abs(list_j_load[i].Value).ToString();
                        loadVal.InsertionPoint = new gPoint(arLine.EndPoint.x - 0.2d, arLine.EndPoint.y - 0.5, arLine.EndPoint.z);
                    }
                    else if (list_j_load[i].Value > 0)
                    {
                        loadVal.TextString = Math.Abs(list_j_load[i].Value).ToString();
                        loadVal.InsertionPoint = new gPoint(arLine.EndPoint.x - 0.2d, arLine.EndPoint.y, arLine.EndPoint.z);
                    }

                    loadVal.Height = 0.02d;

                    gPoint p = new gPoint(arLine.StartPoint);
                    arLine.StartPoint = new gPoint(arLine.EndPoint);
                    arLine.EndPoint = p;



                    #region Chiranjit Modified [2010 01 01]


                    double radius = 0.1;
                    vdCircle cir = new vdCircle();
                    cir.SetUnRegisterDocument(doc);
                    cir.setDocumentDefaults();
                    cir.Center = new gPoint(arLine.EndPoint.x, arLine.EndPoint.y + radius, arLine.EndPoint.z);
                    cir.Radius = radius;

                    vdHatchProperties hp = new vdHatchProperties();
                    hp.SetUnRegisterDocument(doc);
                    hp.FillMode = VdConstFill.VdFillModeSolid;
                    hp.FillColor = new vdColor(Color.Red);
                    try
                    {
                        cir.HatchProperties = hp;
                    }
                    catch (Exception ex)
                    {

                    }
                    //cir.HatchProperties.FillColor = new vdColor(Color.Red);

                    //cir.Layer = jointLoadsLay;
                    doc.ActiveLayOut.Entities.AddItem(cir);

                    #endregion

                    arLine.EndPoint.y += radius;
                    doc.ActiveLayOut.Entities.AddItem(arLine);


                    doc.ActiveLayOut.Entities.AddItem(loadVal);
                }
                #endregion
            }



            doc.RenderMode = vdRender.Mode.Wire2d;
            //VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);
            doc.Redraw(true);
        }

        public void DrawMovingJointLoads_OLD(vdDocument doc, int LoadCase)
        {
            Delete_ASTRAArrowLine(doc);

            //jointLoadsLay = new vdLayer();
            //jointLoadsLay.Name = "JointLoad";
            //jointLoadsLay.SetUnRegisterDocument(doc);
            //jointLoadsLay.setDocumentDefaults();

            //doc.Layers.AddItem(jointLoadsLay);
            jointLoadsLay.Name = "JointLoad";
            if (doc.Layers.FindName("JointLoad") == null)
            {
                jointLoadsLay = new vdLayer();
                jointLoadsLay.Name = "JointLoad";
                jointLoadsLay.SetUnRegisterDocument(doc);
                jointLoadsLay.setDocumentDefaults();

                doc.Layers.AddItem(jointLoadsLay);
            }
            else
            {
                jointLoadsLay = doc.Layers.FindName("JointLoad");
            }

            jointLoadsLay.PenColor = new vdColor(Color.Red);



            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].LoadCase != LoadCase) continue;


                ASTRAArrowLine arLine = new ASTRAArrowLine();
                arLine.SetUnRegisterDocument(doc);
                arLine.setDocumentDefaults();
                arLine.Layer = jointLoadsLay;

                arLine.arrowSize = 0.5;
                arLine.StartPoint = list[i].Joint.Point;


                vdText loadVal = new vdText();
                loadVal.SetUnRegisterDocument(doc);
                loadVal.setDocumentDefaults();

                if (list[i].Direction == JointLoad.eDirection.FX)
                {
                    if (list[i].Value < 0)
                        arLine.EndPoint = new gPoint(list[i].Joint.Point.x + 2.0d, list[i].Joint.Point.y, list[i].Joint.Point.z);
                    else if (list[i].Value > 0)
                        arLine.EndPoint = new gPoint(list[i].Joint.Point.x - 2.0d, list[i].Joint.Point.y, list[i].Joint.Point.z);
                }
                else if (list[i].Direction == JointLoad.eDirection.FY)
                {
                    if (list[i].Value < 0)
                        arLine.EndPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y + 2.0d, list[i].Joint.Point.z);
                    else if (list[i].Value > 0)
                        arLine.EndPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y - 2.0d, list[i].Joint.Point.z);
                }
                else if (list[i].Direction == JointLoad.eDirection.FZ)
                {
                    if (list[i].Value < 0)
                    {
                        arLine.EndPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y, list[i].Joint.Point.z + 2.0d);
                        loadVal.InsertionPoint = new gPoint(arLine.EndPoint.x - 0.1d, arLine.EndPoint.y - 0.5, arLine.EndPoint.z);
                    }
                    else if (list[i].Value > 0)
                    {
                        arLine.EndPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y, list[i].Joint.Point.z - 2.0d);
                        loadVal.InsertionPoint = new gPoint(arLine.EndPoint.x - 0.1d, arLine.EndPoint.y + 0.5, arLine.EndPoint.z);
                    }
                }


                loadVal.Layer = jointLoadsLay;
                if (list[i].Value < 0)
                {
                    loadVal.TextString = Math.Abs(list[i].Value).ToString();
                    loadVal.InsertionPoint = new gPoint(arLine.EndPoint.x - 0.2d, arLine.EndPoint.y - 0.5, arLine.EndPoint.z);
                }
                else if (list[i].Value > 0)
                {
                    loadVal.TextString = Math.Abs(list[i].Value).ToString();
                    loadVal.InsertionPoint = new gPoint(arLine.EndPoint.x - 0.2d, arLine.EndPoint.y, arLine.EndPoint.z);
                }

                loadVal.Height = 0.02d;

                gPoint p = new gPoint(arLine.StartPoint);
                arLine.StartPoint = new gPoint(arLine.EndPoint);
                arLine.EndPoint = p;



                #region Chiranjit Modified [2010 01 01]


                double radius = 0.1;
                vdCircle cir = new vdCircle();
                cir.SetUnRegisterDocument(doc);
                cir.setDocumentDefaults();
                cir.Center = new gPoint(arLine.EndPoint.x, arLine.EndPoint.y + radius, arLine.EndPoint.z);
                cir.Radius = radius;

                vdHatchProperties hp = new vdHatchProperties();
                hp.SetUnRegisterDocument(doc);
                hp.FillMode = VdConstFill.VdFillModeSolid;
                hp.FillColor = new vdColor(Color.Red);
                try
                {
                    cir.HatchProperties = hp;
                }
                catch (Exception ex)
                {

                }
                //cir.HatchProperties.FillColor = new vdColor(Color.Red);

                //cir.Layer = jointLoadsLay;
                doc.ActiveLayOut.Entities.AddItem(cir);

                #endregion

                arLine.EndPoint.y += radius;
                doc.ActiveLayOut.Entities.AddItem(arLine);


                doc.ActiveLayOut.Entities.AddItem(loadVal);
            }
            doc.RenderMode = vdRender.Mode.Wire2d;
            //VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);
            doc.Redraw(true);
        }

        public void DrawJointLoads(vdDocument doc, JointLoad jl)
        {
            //jointLoadsLay = new vdLayer();
            //jointLoadsLay.Name = "JointLoad";
            //jointLoadsLay.SetUnRegisterDocument(doc);
            //jointLoadsLay.setDocumentDefaults();

            //doc.Layers.AddItem(jointLoadsLay);
            jointLoadsLay.Name = "JointLoad";
            if (doc.Layers.FindName("JointLoad") == null)
            {
                jointLoadsLay = new vdLayer();
                jointLoadsLay.Name = "JointLoad";
                jointLoadsLay.SetUnRegisterDocument(doc);
                jointLoadsLay.setDocumentDefaults();

                doc.Layers.AddItem(jointLoadsLay);
            }
            else
            {
                jointLoadsLay = doc.Layers.FindName("JointLoad");
            }

            jointLoadsLay.PenColor = new vdColor(Color.Red);

            ASTRAArrowLine arLine = new ASTRAArrowLine();
            arLine.SetUnRegisterDocument(doc);
            arLine.setDocumentDefaults();
            arLine.Layer = jointLoadsLay;

            arLine.arrowSize = 0.5;
            arLine.StartPoint = jl.Joint.Point;


            vdText loadVal = new vdText();
            loadVal.SetUnRegisterDocument(doc);
            loadVal.setDocumentDefaults();

            if (jl.Direction == JointLoad.eDirection.FX)
            {
                if (jl.Value < 0)
                    arLine.EndPoint = new gPoint(jl.Joint.Point.x + 2.0d, jl.Joint.Point.y, jl.Joint.Point.z);
                else if (jl.Value > 0)
                    arLine.EndPoint = new gPoint(jl.Joint.Point.x - 2.0d, jl.Joint.Point.y, jl.Joint.Point.z);
            }
            else if (jl.Direction == JointLoad.eDirection.FY)
            {
                if (jl.Value < 0)
                    arLine.EndPoint = new gPoint(jl.Joint.Point.x, jl.Joint.Point.y + 2.0d, jl.Joint.Point.z);
                else if (jl.Value > 0)
                    arLine.EndPoint = new gPoint(jl.Joint.Point.x, jl.Joint.Point.y - 2.0d, jl.Joint.Point.z);
            }
            else if (jl.Direction == JointLoad.eDirection.FZ)
            {
                if (jl.Value < 0)
                {
                    arLine.EndPoint = new gPoint(jl.Joint.Point.x, jl.Joint.Point.y, jl.Joint.Point.z + 2.0d);
                    loadVal.InsertionPoint = new gPoint(arLine.EndPoint.x - 0.1d, arLine.EndPoint.y - 0.5, arLine.EndPoint.z);
                }
                else if (jl.Value > 0)
                {
                    arLine.EndPoint = new gPoint(jl.Joint.Point.x, jl.Joint.Point.y, jl.Joint.Point.z - 2.0d);
                    loadVal.InsertionPoint = new gPoint(arLine.EndPoint.x - 0.1d, arLine.EndPoint.y + 0.5, arLine.EndPoint.z);
                }
            }


            loadVal.Layer = jointLoadsLay;
            if (jl.Value < 0)
            {
                loadVal.TextString = Math.Abs(jl.Value).ToString();
                loadVal.InsertionPoint = new gPoint(arLine.EndPoint.x - 0.2d, arLine.EndPoint.y - 0.5, arLine.EndPoint.z);
            }
            else if (jl.Value > 0)
            {
                loadVal.TextString = Math.Abs(jl.Value).ToString();
                loadVal.InsertionPoint = new gPoint(arLine.EndPoint.x - 0.2d, arLine.EndPoint.y, arLine.EndPoint.z);
            }

            loadVal.Height = 0.2d;

            gPoint p = new gPoint(arLine.StartPoint);
            arLine.StartPoint = new gPoint(arLine.EndPoint);
            arLine.EndPoint = p;


            doc.ActiveLayOut.Entities.AddItem(arLine);
            doc.ActiveLayOut.Entities.AddItem(loadVal);
            //}
            doc.RenderMode = vdRender.Mode.Wire2d;
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);
            doc.Redraw(true);
        }

        public bool SetMovingJointLoad(string astFileName)
        {
            if (bIsWork) return false;
            if (!File.Exists(astFileName)) return false;
            string filFilePath = Path.GetDirectoryName(astFileName);
            filFilePath = Path.Combine(filFilePath, "moveload3.fil");
            if (!File.Exists(filFilePath)) return false;

            List<string> lstStr = new List<string>(File.ReadAllLines(filFilePath));

            string kStr = "";

            #region For Loop
            for (int i = 0; i < lstStr.Count; i++)
            {
                Add(JointLoad.ParseAST(lstStr[i], true));
            }
            lstStr.Clear();
            bIsWork = true;
            #endregion

            return true;
        }

        ~JointLoadCollection()
        {
            list.Clear();
            list = null;
            hashJointLoad = null;
        }

    }
    public class JointWeight
    {
        public int JointNo { get; set; }
        public double Weight { get; set; }

        public JointWeight()
        {
            JointNo = 0;
            Weight = 0;
        }

        public JointWeight(int jointNo, double weight)
        {
            JointNo = jointNo;
            Weight = weight;
        }
        public override string ToString()
        {
            return string.Format("{0} WEIGHT {1:f4}", JointNo, Weight);
        }

    }
}
