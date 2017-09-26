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
    /*
    public class JointLoad
    {
        JointCoordinate jc = null;
        eDirection direction;
        double dValue = 0.0d;
        int iLoadCase = 0;
        public JointLoad()
        {
            jc = new JointCoordinate();
            direction = eDirection.FX;
            dValue = 0.0;

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

    }
    public class JointLoadCollection : IList<JointLoad>
    {
        List<JointLoad> list = new List<JointLoad>();
        vdLayer jointLoadsLay = new vdLayer();

        public JointLoadCollection()
        {
            list = new List<JointLoad>();
            jointLoadsLay = new vdLayer();
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
            list.Add(item);

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

            double d = double.Parse(values[3]);
            if (d != 0.0)
            {
                jld.Direction = JointLoad.eDirection.FX;
                jld.Value = d;
            }
            d = double.Parse(values[4]);
            if (d != 0.0)
            {
                jld.Direction = JointLoad.eDirection.FY;
                jld.Value = d;
            }
            d = double.Parse(values[5]);
            if (d != 0.0)
            {
                jld.Direction = JointLoad.eDirection.FZ;
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


            string[] values = data.Split(new char[] { ';' });
            List<string> lstStr = new List<string>();
            foreach (string s in values)
            {

                lstStr.Clear();
                lstStr.AddRange(s.Trim().TrimEnd().TrimStart().Split(new char[] { ' ' }));

                double val = 0.0d;
                int indx = 0;


                if (lstStr.Contains("FX"))
                {
                    try
                    {
                        indx = lstStr.IndexOf("FX");
                        val = double.Parse(lstStr[indx + 1]);
                        for (int i = 0; i < indx; i++)
                        {
                            jlds = new JointLoad();
                            jlds.Direction = JointLoad.eDirection.FX;
                            jlds.Value = val;
                            jlds.Joint.NodeNo = int.Parse(lstStr[i]);
                            jlds.LoadCase = loadCase;
                            Add(jlds);
                        }
                    }
                    catch (Exception ex) { }
                }
                else if (lstStr.Contains("FY"))
                {
                    try
                    {
                        indx = lstStr.IndexOf("FY");
                        val = double.Parse(lstStr[indx + 1]);
                        for (int i = 0; i < indx; i++)
                        {
                            jlds = new JointLoad();
                            jlds.Direction = JointLoad.eDirection.FY;
                            jlds.Value = val;
                            jlds.Joint.NodeNo = int.Parse(lstStr[i]);
                            jlds.LoadCase = loadCase;
                            Add(jlds);
                        }
                    }
                    catch (Exception ex) { }
                }
                else if (lstStr.Contains("FZ"))
                {
                    try
                    {
                        indx = lstStr.IndexOf("FZ");
                        val = double.Parse(lstStr[indx + 1]);
                        for (int i = 0; i < indx; i++)
                        {
                            jlds = new JointLoad();
                            jlds.Direction = JointLoad.eDirection.FZ;
                            jlds.Value = val;
                            jlds.Joint.NodeNo = int.Parse(lstStr[i]);
                            jlds.LoadCase = loadCase;
                            Add(jlds);
                        }
                    }
                    catch (Exception ex) { }
                }
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
            throw new NotImplementedException();
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

        public void DrawJointLoads(vdDocument doc, JointLoad jl)
        {
            jointLoadsLay = doc.Layers.FindName("JointLoad");
            if (jointLoadsLay == null)
            {
                jointLoadsLay = new vdLayer();
                jointLoadsLay.Name = "JointLoad";
                jointLoadsLay.SetUnRegisterDocument(doc);
                jointLoadsLay.setDocumentDefaults();
                doc.Layers.AddItem(jointLoadsLay);
                jointLoadsLay.PenColor = new vdColor(Color.Red);
            }
            //for (int i = 0; i < list.Count; i++)
            //{
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
                    arLine.EndPoint = new gPoint(jl.Joint.Point.x - 2.0d, jl.Joint.Point.y, jl.Joint.Point.z);
                else if (jl.Value > 0)
                    arLine.EndPoint = new gPoint(jl.Joint.Point.x + 2.0d, jl.Joint.Point.y, jl.Joint.Point.z);
            }
            else if (jl.Direction == JointLoad.eDirection.FY)
            {
                if (jl.Value < 0)
                    arLine.EndPoint = new gPoint(jl.Joint.Point.x, jl.Joint.Point.y - 2.0d, jl.Joint.Point.z);
                else if (jl.Value > 0)
                    arLine.EndPoint = new gPoint(jl.Joint.Point.x, jl.Joint.Point.y + 2.0d, jl.Joint.Point.z);
            }
            else if (jl.Direction == JointLoad.eDirection.FZ)
            {
                if (jl.Value < 0)
                {
                    arLine.EndPoint = new gPoint(jl.Joint.Point.x, jl.Joint.Point.y, jl.Joint.Point.z - 2.0d);
                    loadVal.InsertionPoint = new gPoint(arLine.EndPoint.x - 0.1d, arLine.EndPoint.y - 0.5, arLine.EndPoint.z);
                }
                else if (jl.Value > 0)
                {
                    arLine.EndPoint = new gPoint(jl.Joint.Point.x, jl.Joint.Point.y, jl.Joint.Point.z + 2.0d);
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
            doc.ActiveLayOut.Entities.AddItem(arLine);
            doc.ActiveLayOut.Entities.AddItem(loadVal);
            //}
            doc.Redraw(true);
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
                        arLine.EndPoint = new gPoint(list[i].Joint.Point.x - 2.0d, list[i].Joint.Point.y, list[i].Joint.Point.z);
                    else if (list[i].Value > 0)
                        arLine.EndPoint = new gPoint(list[i].Joint.Point.x + 2.0d, list[i].Joint.Point.y, list[i].Joint.Point.z);
                }
                else if (list[i].Direction == JointLoad.eDirection.FY)
                {
                    if (list[i].Value < 0)
                        arLine.EndPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y - 2.0d, list[i].Joint.Point.z);
                    else if (list[i].Value > 0)
                        arLine.EndPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y + 2.0d, list[i].Joint.Point.z);
                }
                else if (list[i].Direction == JointLoad.eDirection.FZ)
                {
                    if (list[i].Value < 0)
                    {
                        arLine.EndPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y, list[i].Joint.Point.z - 2.0d);
                        loadVal.InsertionPoint = new gPoint(arLine.EndPoint.x - 0.1d, arLine.EndPoint.y - 0.5, arLine.EndPoint.z);
                    }
                    else if (list[i].Value > 0)
                    {
                        arLine.EndPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y, list[i].Joint.Point.z + 2.0d);
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
                doc.ActiveLayOut.Entities.AddItem(loadVal);
            }
            doc.Redraw(true);
        }
        public void DrawJointLoads(vdDocument doc, int LoadCase)
        {
            jointLoadsLay = new vdLayer();
            jointLoadsLay.Name = "JointLoad";
            jointLoadsLay.SetUnRegisterDocument(doc);
            jointLoadsLay.setDocumentDefaults();

            doc.Layers.AddItem(jointLoadsLay);
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
                        arLine.EndPoint = new gPoint(list[i].Joint.Point.x - 2.0d, list[i].Joint.Point.y, list[i].Joint.Point.z);
                    else if (list[i].Value > 0)
                        arLine.EndPoint = new gPoint(list[i].Joint.Point.x + 2.0d, list[i].Joint.Point.y, list[i].Joint.Point.z);
                }
                else if (list[i].Direction == JointLoad.eDirection.FY)
                {
                    if (list[i].Value < 0)
                        arLine.EndPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y - 2.0d, list[i].Joint.Point.z);
                    else if (list[i].Value > 0)
                        arLine.EndPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y + 2.0d, list[i].Joint.Point.z);
                }
                else if (list[i].Direction == JointLoad.eDirection.FZ)
                {
                    if (list[i].Value < 0)
                    {
                        arLine.EndPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y, list[i].Joint.Point.z - 2.0d);
                        loadVal.InsertionPoint = new gPoint(arLine.EndPoint.x - 0.1d, arLine.EndPoint.y - 0.5, arLine.EndPoint.z);
                    }
                    else if (list[i].Value > 0)
                    {
                        arLine.EndPoint = new gPoint(list[i].Joint.Point.x, list[i].Joint.Point.y, list[i].Joint.Point.z + 2.0d);
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
                doc.ActiveLayOut.Entities.AddItem(loadVal);
            }
            doc.RenderMode = vdRender.Mode.Wire2d;
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);
            doc.Redraw(true);
        }
    }
    */
}
