using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Geometry;

namespace HEADSNeed.ASTRA.ASTRAClasses
{
    //public class PostProcessHelper
    //{
    //    // This class is not implemented
    //}
    public class NodeDisplacement
    {
        JointCoordinate jcNode;
        int iLoadCase;
        double dTx, dTy, dTz, dRx, dRy, dRz;

        public NodeDisplacement()
        {
            jcNode = new JointCoordinate();
            iLoadCase = 0;
            dTx = dTy = dTz = dRx = dRy = dRz = 0.0d;
        }

        public JointCoordinate Node { get { return jcNode; } set { jcNode = value; } }
        public int LoadCase { get { return iLoadCase; } set { iLoadCase = value; } }
        public double Tx { get { return dTx; } set { dTx = value; } }
        public double Ty { get { return dTy; } set { dTy = value; } }
        public double Tz { get { return dTz; } set { dTz = value; } }
        public double Rx { get { return dRx; } set { dRx = value; } }
        public double Ry { get { return dRy; } set { dRy = value; } }
        public double Rz { get { return dRz; } set { dRz = value; } }
    }
    public class NodeDisplacementCollection : IList<NodeDisplacement>
    {
        List<NodeDisplacement> list = new List<NodeDisplacement>();
        public NodeDisplacementCollection()
        {
            list = new List<NodeDisplacement>();
        }

        #region IList<NodeDisplacement> Members

        public int IndexOf(NodeDisplacement item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if ((list[i].Node.NodeNo == item.Node.NodeNo) ||
                    (list[i].LoadCase == item.LoadCase))
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
                if ((list[i].Node.NodeNo == NodeNo) ||
                    (list[i].LoadCase == LoadCase))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, NodeDisplacement item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public NodeDisplacement this[int index]
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

        #region ICollection<NodeDisplacement> Members

        public void Add(NodeDisplacement item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(NodeDisplacement item)
        {
            return ((IndexOf(item) != -1) ? true : false);
        }

        public void CopyTo(NodeDisplacement[] array, int arrayIndex)
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

        public bool Remove(NodeDisplacement item)
        {
            int indx = IndexOf(item);
            if (indx != -1)
            {
                list.RemoveAt(indx);
            }
            return false;
        }

        #endregion

        #region IEnumerable<NodeDisplacement> Members

        public IEnumerator<NodeDisplacement> GetEnumerator()
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
    }
    public class ForceMoment
    {
        double dR1, dR2, dR3, dM1, dM2, dM3;
        public ForceMoment()
        {
            dR1 = dR2 = dR3 = dM1 = dM2 = dM3 = 0.0d;
        }
        public double R1
        {
            get
            {
                return dR1;
            }
            set
            {
                dR1 = value;
            }
        }
        public double R2
        {
            get
            {
                return dR2;
            }
            set
            {
                dR2 = value;
            }
        }
        public double R3
        {
            get
            {
                return dR3;
            }
            set
            {
                dR3 = value;
            }
        }
        public double M1
        {
            get
            {
                return dM1;
            }
            set
            {
                dM1 = value;
            }
        }
        public double M2
        {
            get
            {
                return dM2;
            }
            set
            {
                dM2 = value;
            }
        }
        public double M3
        {
            get
            {
                return dM3;
            }
            set
            {
                dM3 = value;
            }
        }
    }
    public class BeamForceMoment
    {
        MemberIncidence mi = null;
        int iLoadCase = 0;
        ForceMoment startForceMoment = null;
        ForceMoment endForceMoment = null;

        public BeamForceMoment()
        {
            mi = new MemberIncidence();
            iLoadCase = 0;
            startForceMoment = new ForceMoment();
            endForceMoment = new ForceMoment();
        }
        public MemberIncidence Member
        {
            get
            {
                return mi;
            }
            set
            {
                mi = value;
            }
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
        public ForceMoment StartForceMoment
        {
            get
            {
                return startForceMoment;
            }
            set
            {
                startForceMoment = value;
            }
        }
        public ForceMoment EndForceMoment
        {
            get
            {
                return endForceMoment;
            }
            set
            {
                endForceMoment = value;
            }
        }

    }
    public class BeamForceMomentCollection : IList<BeamForceMoment>
    {
        List<BeamForceMoment> list = new List<BeamForceMoment>();
        public BeamForceMomentCollection()
        {
            list = new List<BeamForceMoment>();
        }
        public void CopyMembers(MemberIncidenceCollection mic)
        {
            int indx = 0;
            for (int i = 0; i < list.Count; i++)
            {
                indx = mic.IndexOf(list[i].Member.MemberNo);
                if (indx != -1)
                {
                    list[i].Member = mic[indx];
                }
            }
        }
        #region IList<BeamForceMoment> Members

        public int IndexOf(BeamForceMoment item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if ((list[i].Member.MemberNo == item.Member.MemberNo) ||
                    list[i].LoadCase == item.LoadCase)
                {
                    return i;
                }
            }
            return -1;
        }
        public int IndexOf(int MemberNo, int LoadCase)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if ((list[i].Member.MemberNo == MemberNo) &&
                    list[i].LoadCase == LoadCase)
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, BeamForceMoment item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public BeamForceMoment this[int index]
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

        #region ICollection<BeamForceMoment> Members

        public void Add(BeamForceMoment item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(BeamForceMoment item)
        {
            return (IndexOf(item) != -1 ? true : false);
        }

        public void CopyTo(BeamForceMoment[] array, int arrayIndex)
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

        public bool Remove(BeamForceMoment item)
        {
            int indx = IndexOf(item);
            if (indx != -1)
            {
                list.RemoveAt(indx);
                return true;
            }
            return false;
        }

        #endregion

        #region IEnumerable<BeamForceMoment> Members

        public IEnumerator<BeamForceMoment> GetEnumerator()
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

        double st, en, max, highest;

        public double MaxY(gPoints gps)
        {
            double y = 0.0d;
            foreach (gPoint gp in gps)
            {
                if (Math.Abs(gp.y) > y)
                    y = Math.Abs(gp.y);
            }
            return y;
        }

        public void DrawBeamForceMoment_Bending(vdDocument doc, int MemberNo, int loadCase, eForce force, gPoints gps)
        {
            
            #region Variables


            doc.ActionLayout.Entities.RemoveAll();
            int indx = -1;
            int cnt = 2;
            double shortLineCount = 16;
            int textMarkCount = 4;
            int tMark = textMarkCount;
            int beamIndex = 0;

            st = en = 0.0d;

            indx = 0;
            #endregion

            beamIndex = IndexOf(MemberNo, loadCase);
            //mNo = IndexOf(list[beamIndex].Member.MemberNo, loadCase);
            #region switch case
            switch (force)
            {
                case eForce.R1:
                    indx = IndexOf(list[beamIndex].Member.MemberNo, loadCase);
                    if (indx != -1)
                    {
                        st = list[indx].StartForceMoment.R1;
                        en = list[indx].EndForceMoment.R1;
                    }
                    break;

                case eForce.R2:
                    indx = IndexOf(list[beamIndex].Member.MemberNo, loadCase);
                    if (indx != -1)
                    {
                        st = list[indx].StartForceMoment.R2;
                        en = list[indx].EndForceMoment.R2;
                    }
                    break;

                case eForce.R3:
                    indx = IndexOf(list[beamIndex].Member.MemberNo, loadCase);
                    if (indx != -1)
                    {
                        st = list[indx].StartForceMoment.R3;
                        en = list[indx].EndForceMoment.R3;
                    }
                    break;

                case eForce.M1:
                    indx = IndexOf(list[beamIndex].Member.MemberNo, loadCase);
                    if (indx != -1)
                    {
                        st = list[indx].StartForceMoment.M1;
                        en = list[indx].EndForceMoment.M1;
                    }
                    break;

                case eForce.M2:
                    indx = IndexOf(list[beamIndex].Member.MemberNo, loadCase);
                    if (indx != -1)
                    {
                        st = list[indx].StartForceMoment.M2;
                        en = list[indx].EndForceMoment.M2;
                    }
                    break;
                case eForce.M3:
                    indx = IndexOf(list[beamIndex].Member.MemberNo, loadCase);
                    if (indx != -1)
                    {
                        st = list[indx].StartForceMoment.M3;
                        en = list[indx].EndForceMoment.M3;
                    }
                    break;
            }
            #endregion

            indx = 0;
            max = MaxY(gps);

            #region Start Node Line
            vdLine line1 = new vdLine();
            line1.SetUnRegisterDocument(doc);
            line1.setDocumentDefaults();
            line1.StartPoint = new VectorDraw.Geometry.gPoint(2.0d, 5.0d, 0);
            line1.EndPoint = new VectorDraw.Geometry.gPoint(2.0d, 26.0d, 0);

            doc.ActiveLayOut.Entities.AddItem(line1);
            doc.Redraw(true);
            #endregion

            #region First Short Liles
            for (int i = 0; i <= (int)shortLineCount; i++)
            {
                vdLine ln = new vdLine();
                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();

                ln.StartPoint.x = line1.StartPoint.x + (line1.EndPoint.x - line1.StartPoint.x) * (((double)(i) / shortLineCount));
                ln.StartPoint.y = line1.StartPoint.y + (line1.EndPoint.y - line1.StartPoint.y) * (((double)(i) / shortLineCount));
                ln.StartPoint.z = line1.StartPoint.z + (line1.EndPoint.z - line1.StartPoint.z) * (((double)(i) / shortLineCount));

                ln.EndPoint = new VectorDraw.Geometry.gPoint(ln.StartPoint.x - 1.2d, ln.StartPoint.y, ln.StartPoint.z);
                if (tMark == textMarkCount)
                {
                    ln.EndPoint = new VectorDraw.Geometry.gPoint(ln.StartPoint.x - 1.8d, ln.StartPoint.y, ln.StartPoint.z);
                }
                doc.ActiveLayOut.Entities.AddItem(ln);


                vdText vTxt = new vdText();
                vTxt.SetUnRegisterDocument(doc);
                vTxt.setDocumentDefaults();

                //double dd = 
                vTxt.InsertionPoint = new VectorDraw.Geometry.gPoint(ln.EndPoint.x - 12.0d, ln.EndPoint.y - 1.3d, ln.EndPoint.z);

                if (tMark == textMarkCount)
                {
                    if (i < 8)
                    {
                        double d = max * (((double)cnt) / 2.0d);
                        vTxt.TextString = "-" + d.ToString("0.0000");
                        cnt--;
                    }
                    else
                    {
                        double d = max * (((double)cnt) / 2.0d);
                        vTxt.TextString = d.ToString("0.0000");
                        cnt++;
                        highest = d;
                    }
                    vTxt.Height = 2.5d;
                    doc.ActiveLayOut.Entities.AddItem(vTxt);
                    tMark = 0;
                }


                double dd = (int)max.ToString("0.0000").Length;
                vTxt.InsertionPoint = new VectorDraw.Geometry.gPoint(ln.EndPoint.x - 2.0d * dd, ln.EndPoint.y - 1.3d, ln.EndPoint.z);


                tMark++;
            }
            #endregion

            #region End Node Line
            vdLine line2 = new vdLine();
            line2.SetUnRegisterDocument(doc);
            line2.setDocumentDefaults();
            line2.StartPoint = new VectorDraw.Geometry.gPoint(55.0d, 5.0d, 0);
            line2.EndPoint = new VectorDraw.Geometry.gPoint(55.0d, 26.0d, 0);

            doc.ActiveLayOut.Entities.AddItem(line2);
            doc.Redraw(true);
            #endregion

            #region End Short Line

            cnt = 2;
            //Chiranjit 2009 11 23 
            //cnt = 1;
            tMark = textMarkCount;
            for (int i = 0; i <= (int)shortLineCount; i++)
            {
                vdLine ln = new vdLine();
                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();

                ln.StartPoint.x = line2.StartPoint.x + (line2.EndPoint.x - line2.StartPoint.x) * (((double)(i) / shortLineCount));
                ln.StartPoint.y = line2.StartPoint.y + (line2.EndPoint.y - line2.StartPoint.y) * (((double)(i) / shortLineCount));
                ln.StartPoint.z = line2.StartPoint.z + (line2.EndPoint.z - line2.StartPoint.z) * (((double)(i) / shortLineCount));

                ln.EndPoint = new VectorDraw.Geometry.gPoint(ln.StartPoint.x + 1.2d, ln.StartPoint.y, ln.StartPoint.z);
                if (tMark == textMarkCount)
                {
                    ln.EndPoint = new VectorDraw.Geometry.gPoint(ln.StartPoint.x + 1.8d, ln.StartPoint.y, ln.StartPoint.z);
                }
                doc.ActiveLayOut.Entities.AddItem(ln);


                vdText vTxt = new vdText();
                vTxt.SetUnRegisterDocument(doc);
                vTxt.setDocumentDefaults();
                vTxt.InsertionPoint = new VectorDraw.Geometry.gPoint(ln.EndPoint.x + 1.5d, ln.EndPoint.y - 0.4d, ln.EndPoint.z);

                if (tMark == textMarkCount)
                {
                    if (i < 8)
                    {
                        double d = max * (((double)cnt) / 2.0d);
                        vTxt.TextString = "-" + d.ToString("0.0000");
                        cnt--;
                    }
                    else
                    {
                        double d = max * (((double)cnt) / 2.0d);
                        vTxt.TextString = d.ToString("0.0000");
                        cnt++;
                        highest = d;
                    }
                    vTxt.Height = 2.5d;
                    doc.ActiveLayOut.Entities.AddItem(vTxt);
                    tMark = 0;
                }
                tMark++;
            }
            #endregion

            #region Middle Line
            vdLine midLine = new vdLine();
            midLine.StartPoint = (line1.StartPoint + line1.EndPoint) / 2;
            midLine.EndPoint = (line2.StartPoint + line2.EndPoint) / 2;
            midLine.SetUnRegisterDocument(doc);
            midLine.setDocumentDefaults();
            doc.ActiveLayOut.Entities.AddItem(midLine);
            #endregion

            #region Draw OUTER Rectangle
            vdRect rect = new vdRect();
            rect.SetUnRegisterDocument(doc);
            rect.setDocumentDefaults();
            rect.InsertionPoint = new VectorDraw.Geometry.gPoint(-22.48d, -0.7d, 0.0d);
            rect.Width = 100.0d;
            rect.Height = 38.0d;
            rect.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(rect);

            doc.RenderMode = VectorDraw.Render.vdRender.Mode.Wire2d;
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);
            #endregion

            #region BEAM LINE

            vdLine beamLine = new vdLine();

            if (st > 0)
            {
                beamLine.StartPoint.x = midLine.StartPoint.x + (line1.EndPoint.x - midLine.StartPoint.x) * (st / highest);
                beamLine.StartPoint.y = midLine.StartPoint.y + (line1.EndPoint.y - midLine.StartPoint.y) * (st / highest);
                beamLine.StartPoint.z = midLine.StartPoint.z + (line1.EndPoint.z - midLine.StartPoint.z) * (st / highest);
            }
            else
            {
                beamLine.StartPoint.x = midLine.StartPoint.x - (line1.StartPoint.x - midLine.StartPoint.x) * (st / highest);
                beamLine.StartPoint.y = midLine.StartPoint.y - (line1.StartPoint.y - midLine.StartPoint.y) * (st / highest);
                beamLine.StartPoint.z = midLine.StartPoint.z - (line1.StartPoint.z - midLine.StartPoint.z) * (st / highest);
            }
            if (en > 0)
            {
                beamLine.EndPoint.x = midLine.EndPoint.x + (line2.EndPoint.x - midLine.EndPoint.x) * (en / highest);
                beamLine.EndPoint.y = midLine.EndPoint.y + (line2.EndPoint.y - midLine.EndPoint.y) * (en / highest);
                beamLine.EndPoint.z = midLine.EndPoint.z + (line2.EndPoint.z - midLine.EndPoint.z) * (en / highest);
            }
            else
            {
                beamLine.EndPoint.x = midLine.EndPoint.x - (line2.StartPoint.x - midLine.EndPoint.x) * (en / highest);
                beamLine.EndPoint.y = midLine.EndPoint.y - (line2.StartPoint.y - midLine.EndPoint.y) * (en / highest);
                beamLine.EndPoint.z = midLine.EndPoint.z - (line2.StartPoint.z - midLine.EndPoint.z) * (en / highest);
            }
            beamLine.SetUnRegisterDocument(doc);
            beamLine.setDocumentDefaults();
            beamLine.PenColor = new vdColor(Color.Blue);
            //doc.ActiveLayOut.Entities.AddItem(beamLine);
            #endregion

            #region Poly Line
            vdPolyline vPline = new vdPolyline();
            vPline.SetUnRegisterDocument(doc);
            vPline.setDocumentDefaults();

            gPoint gp = new gPoint();
            double x = midLine.StartPoint.x;
            double xIncr = (midLine.Length() / 10.0d);
            for (int i = 0; i < gps.Count; i++)
            {
                gp = new gPoint();
                gp.x = x;
                gp.y = midLine.StartPoint.y + (line1.EndPoint.y - midLine.StartPoint.y) * (-gps[i].y / highest);
                vPline.VertexList.Add(gp);
                x += xIncr;

                vdLine vLine = new vdLine();
                vLine.SetUnRegisterDocument(doc);
                vLine.setDocumentDefaults();
                vLine.StartPoint = gp;
                vLine.EndPoint = new gPoint(gp.x, midLine.StartPoint.y);
                doc.ActiveLayOut.Entities.AddItem(vLine);

                vdText yValue = new vdText();
                yValue.SetUnRegisterDocument(doc);
                yValue.setDocumentDefaults();
                yValue.InsertionPoint = vLine.EndPoint;
                yValue.TextString = (-gps[i].y).ToString("0.00");
                if (-gps[i].y > 0)
                {

                    yValue.Rotation = 270.0d * Math.PI / 180.0d;
                    yValue.InsertionPoint.x -= 1.25d;
                    yValue.InsertionPoint.y -= 1.5d;
                }
                else
                {
                    yValue.Rotation = 90.0d * Math.PI / 180.0d;
                    yValue.InsertionPoint.x += 1.25d;
                    yValue.InsertionPoint.y += 1.5d;
                }

                if (i == 0)
                {
                    yValue.InsertionPoint.x += 1.25d;
                }
                else if (i == (gps.Count-1))
                {
                    yValue.InsertionPoint.x -= 1.25d;
                }
                yValue.Height = 2.4d;
                yValue.ToolTip ="Distance = " +  (gps[i].x).ToString("0.00") + ", Load = " + yValue.TextString;
                //yValue.ToolTip = yValue.TextString + ", " + (x - midLine.StartPoint.x).ToString("0.00");
                yValue.PenColor = new vdColor(Color.DarkBlue);
                doc.ActiveLayOut.Entities.AddItem(yValue);
            }
            doc.ActiveLayOut.Entities.AddItem(vPline);
            #endregion

            #region START BOX

            vdRect stBox = new vdRect();
            stBox.SetUnRegisterDocument(doc);
            stBox.setDocumentDefaults();
            //stBox.InsertionPoint = new
            //    VectorDraw.Geometry.gPoint(beamLine.StartPoint.x - 0.2d, beamLine.StartPoint.y - 0.2d, beamLine.StartPoint.z);

            stBox.InsertionPoint = new
                 VectorDraw.Geometry.gPoint(vPline.VertexList[0].x - 0.2d, vPline.VertexList[0].y - 0.2d, vPline.VertexList[0].z);

            stBox.Width = 0.4;
            stBox.Height = 0.4;
            stBox.PenColor = new vdColor(Color.Red);
            stBox.ToolTip = (-gps[0].y).ToString("0.0000");
            doc.ActiveLayOut.Entities.AddItem(stBox);

            #endregion

            #region START TEXT
            vdText stTxt = new vdText();
            stTxt.SetUnRegisterDocument(doc);
            stTxt.setDocumentDefaults();
            stTxt.InsertionPoint = new
                VectorDraw.Geometry.gPoint(beamLine.StartPoint.x + 0.2d, beamLine.StartPoint.y + 0.2d, beamLine.StartPoint.z);
            stTxt.TextString = st.ToString("0.0000");
            stTxt.Height = 2.4;
            stTxt.PenColor = new vdColor(Color.Red);
            stTxt.ToolTip = st.ToString("0.0000");
            //doc.ActiveLayOut.Entities.AddItem(stTxt);
            #endregion

            #region Draw Fixed End Moment

            stTxt = new vdText();
            stTxt.SetUnRegisterDocument(doc);
            stTxt.setDocumentDefaults();
            switch (force)
            {
                case eForce.M1:
                    stTxt.TextString = "MOMENT DIAGRAM";
                    stTxt.InsertionPoint = new
                        VectorDraw.Geometry.gPoint(11.72d, 32.818, beamLine.StartPoint.z);
                    break;
                case eForce.M2:
                    stTxt.TextString = "MOMENT DIAGRAM";
                    stTxt.InsertionPoint = new
                        VectorDraw.Geometry.gPoint(11.82d, 32.318, beamLine.StartPoint.z);
                    break;
                case eForce.M3:
                    stTxt.TextString = "MOMENT DIAGRAM";
                    stTxt.InsertionPoint = new
                        VectorDraw.Geometry.gPoint(11.72d, 32.818, beamLine.StartPoint.z);
                    break;
                case eForce.R1:
                    stTxt.TextString = "END SHEAR DIAGRAM";
                    stTxt.InsertionPoint = new
                      VectorDraw.Geometry.gPoint(7.0902d, 32.818, beamLine.StartPoint.z);
                    break;
                case eForce.R2:
                    stTxt.TextString = "END SHEAR DIAGRAM";
                    stTxt.InsertionPoint = new
                        //VectorDraw.Geometry.gPoint(3.9225d, 31.48, beamLine.StartPoint.z);
                      VectorDraw.Geometry.gPoint(7.0902d, 32.818, beamLine.StartPoint.z);
                    break;
                case eForce.R3:
                    stTxt.TextString = "END SHEAR DIAGRAM";
                    stTxt.InsertionPoint = new
                        //VectorDraw.Geometry.gPoint(3.9225d, 31.48, beamLine.StartPoint.z);
                      VectorDraw.Geometry.gPoint(7.0902d, 32.818, beamLine.StartPoint.z);
                    break;
            }
            stTxt.Height = 3.0d;
            stTxt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(stTxt);

            #endregion

            #region Draw Member No.

            stTxt = new vdText();
            stTxt.SetUnRegisterDocument(doc);
            stTxt.setDocumentDefaults();
            stTxt.InsertionPoint = new
                VectorDraw.Geometry.gPoint(3.70d, 1.05d, beamLine.StartPoint.z);
            stTxt.TextString = "Member : " + MemberNo + " , Load Case : " + loadCase;
            //stTxt.TextString = "Member No: " + list[beamIndex].Member.MemberNo + ";
            stTxt.Height = 3.0d;
            stTxt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(stTxt);

            #endregion

            #region Draw Mx [TORSON]

            stTxt = new vdText();
            stTxt.SetUnRegisterDocument(doc);
            stTxt.setDocumentDefaults();
            stTxt.InsertionPoint = new VectorDraw.Geometry.gPoint(15.0d, 27.35d, beamLine.StartPoint.z);
            switch (force)
            {
                case eForce.M1:
                    stTxt.TextString = "Mx [TORSON]";
                    break;
                case eForce.M2:
                    stTxt.TextString = "My [FEM]";
                    stTxt.InsertionPoint = new VectorDraw.Geometry.gPoint(20.0d, 27.35d, beamLine.StartPoint.z);
                    break;
                case eForce.M3:
                    stTxt.TextString = "Mz [FEM]";
                    stTxt.InsertionPoint = new VectorDraw.Geometry.gPoint(20.0d, 27.35d, beamLine.StartPoint.z);
                    break;
                case eForce.R1:
                    stTxt.TextString = "Fx [AXIAL]";
                    break;
                case eForce.R2:
                    stTxt.TextString = "Fy [SHEAR]";
                    break;
                case eForce.R3:
                    stTxt.TextString = "Fz [SHEAR]";
                    break;

            }
            stTxt.Height = 3.0d;
            stTxt.PenColor = new vdColor(Color.Red);
            //doc.ActiveLayOut.Entities.AddItem(stTxt);

            #endregion

            #region End Box
            vdRect endBox = new vdRect();
            endBox.SetUnRegisterDocument(doc);
            endBox.setDocumentDefaults();
            endBox.InsertionPoint = new
                VectorDraw.Geometry.gPoint(vPline.VertexList[vPline.VertexList.Count - 1].x - 0.2d, vPline.VertexList[vPline.VertexList.Count - 1].y - 0.2d, vPline.VertexList[vPline.VertexList.Count - 1].z);
            endBox.Width = 0.4;
            endBox.Height = 0.4;
            endBox.PenColor = new vdColor(Color.Red);
            endBox.ToolTip = (-gps[gps.Count -1].y).ToString("0.0000");
            doc.ActiveLayOut.Entities.AddItem(endBox);
            #endregion

            #region Start Node

            vdText endTxt = new vdText();
            endTxt.SetUnRegisterDocument(doc);
            endTxt.setDocumentDefaults();
            endTxt.InsertionPoint = new
                VectorDraw.Geometry.gPoint(-16.7169, 29.1850, beamLine.EndPoint.z);
            //endTxt.TextString = "START";
            endTxt.TextString = "NODE " + list[beamIndex].Member.StartNode.NodeNo;
            endTxt.Height = 2.4d;
            endTxt.PenColor = new vdColor(Color.Red);
            endTxt.ToolTip = en.ToString("0.0000");
            doc.ActiveLayOut.Entities.AddItem(endTxt);
            #endregion

            #region End Node

            endTxt = new vdText();
            endTxt.SetUnRegisterDocument(doc);
            endTxt.setDocumentDefaults();
            endTxt.InsertionPoint = new
                VectorDraw.Geometry.gPoint(57.6218d, 29.1850d, beamLine.EndPoint.z);
            //endTxt.TextString = "END";
            endTxt.TextString = "NODE " + list[beamIndex].Member.EndNode.NodeNo;
            endTxt.Height = 2.4d;
            endTxt.PenColor = new vdColor(Color.Red);
            //endTxt.ToolTip = en.ToString("0.0000");
            doc.ActiveLayOut.Entities.AddItem(endTxt);
            #endregion

            doc.ShowUCSAxis = false;
            doc.Redraw(true);
        }
        public void DrawBeamForceMoment(vdDocument doc, int beamIndex)
        {
            #region Variables
            eForce force = eForce.M1;
            doc.ActionLayout.Entities.RemoveAll();
            int indx = -1;
            int cnt = 2;
            double shortLineCount = 16;
            int textMarkCount = 4;
            int tMark = textMarkCount;

            st = en = 0.0d;
            #endregion

            #region switch case
            switch (force)
            {
                case eForce.R1:
                    st = list[beamIndex].StartForceMoment.R1;
                    en = list[beamIndex].EndForceMoment.R1;
                    break;

                case eForce.R2:
                    st = list[beamIndex].StartForceMoment.R2;
                    en = list[beamIndex].EndForceMoment.R2;
                    break;

                case eForce.R3:
                    st = list[beamIndex].StartForceMoment.R3;
                    en = list[beamIndex].EndForceMoment.R3;
                    break;


                case eForce.M1:
                    st = list[beamIndex].StartForceMoment.M1;
                    en = list[beamIndex].EndForceMoment.M1;
                    break;


                case eForce.M2:
                    st = list[beamIndex].StartForceMoment.M2;
                    en = list[beamIndex].EndForceMoment.M2;
                    break;


                case eForce.M3:
                    st = list[beamIndex].StartForceMoment.M3;
                    en = list[beamIndex].EndForceMoment.M3;
                    break;
            }
            #endregion

            indx = 0;

            if (Math.Abs(st) > Math.Abs(en))
            {
                max = Math.Abs(st);
            }
            else
            {
                max = Math.Abs(en);
            }


            #region Start Node Line
            vdLine line1 = new vdLine();
            line1.SetUnRegisterDocument(doc);
            line1.setDocumentDefaults();
            line1.StartPoint = new VectorDraw.Geometry.gPoint(2.0d, 5.0d, 0);
            line1.EndPoint = new VectorDraw.Geometry.gPoint(2.0d, 26.0d, 0);

            doc.ActiveLayOut.Entities.AddItem(line1);
            doc.Redraw(true);
            #endregion


            for (int i = 0; i <= (int)shortLineCount; i++)
            {
                vdLine ln = new vdLine();
                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();

                ln.StartPoint.x = line1.StartPoint.x + (line1.EndPoint.x - line1.StartPoint.x) * (((double)(i) / shortLineCount));
                ln.StartPoint.y = line1.StartPoint.y + (line1.EndPoint.y - line1.StartPoint.y) * (((double)(i) / shortLineCount));
                ln.StartPoint.z = line1.StartPoint.z + (line1.EndPoint.z - line1.StartPoint.z) * (((double)(i) / shortLineCount));

                ln.EndPoint = new VectorDraw.Geometry.gPoint(ln.StartPoint.x - 1.2d, ln.StartPoint.y, ln.StartPoint.z);
                if (tMark == textMarkCount)
                {
                    ln.EndPoint = new VectorDraw.Geometry.gPoint(ln.StartPoint.x - 1.8d, ln.StartPoint.y, ln.StartPoint.z);
                    //tMark = 0;
                }

                //ln.EndPoint = new VectorDraw.Geometry.gPoint(ln.StartPoint.x - 1.2d, ln.StartPoint.y, ln.StartPoint.z);
                doc.ActiveLayOut.Entities.AddItem(ln);

                vdText vTxt = new vdText();
                vTxt.SetUnRegisterDocument(doc);
                vTxt.setDocumentDefaults();
                vTxt.InsertionPoint = new VectorDraw.Geometry.gPoint(ln.EndPoint.x - 12.0d, ln.EndPoint.y - 1.3d, ln.EndPoint.z);

                if (tMark == textMarkCount)
                {
                    if (i < 8)
                    {
                        double d = max * (((double)cnt) / 1.0d);
                        vTxt.TextString = "-" + d.ToString("0.0000");
                        cnt--;
                    }
                    else
                    {
                        double d = max * (((double)cnt) / 1.0d);
                        vTxt.TextString = d.ToString("0.0000");
                        cnt++;
                        highest = d;
                    }
                    vTxt.Height = 2.5d;
                    doc.ActiveLayOut.Entities.AddItem(vTxt);
                    tMark = 0;
                }
                tMark++;

            }



            vdLine line2 = new vdLine();
            line2.SetUnRegisterDocument(doc);
            line2.setDocumentDefaults();
            line2.StartPoint = new VectorDraw.Geometry.gPoint(55.0d, 5.0d, 0);
            line2.EndPoint = new VectorDraw.Geometry.gPoint(55.0d, 26.0d, 0);

            doc.ActiveLayOut.Entities.AddItem(line2);
            doc.Redraw(true);


            cnt = 2;
            tMark = textMarkCount;
            for (int i = 0; i <= (int)shortLineCount; i++)
            {
                vdLine ln = new vdLine();
                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();

                ln.StartPoint.x = line2.StartPoint.x + (line2.EndPoint.x - line2.StartPoint.x) * (((double)(i) / shortLineCount));
                ln.StartPoint.y = line2.StartPoint.y + (line2.EndPoint.y - line2.StartPoint.y) * (((double)(i) / shortLineCount));
                ln.StartPoint.z = line2.StartPoint.z + (line2.EndPoint.z - line2.StartPoint.z) * (((double)(i) / shortLineCount));

                //ln.StartPoint.x = dx;
                //ln.StartPoint.y = dy;
                //ln.StartPoint.z = dz;
                ln.EndPoint = new VectorDraw.Geometry.gPoint(ln.StartPoint.x + 1.2d, ln.StartPoint.y, ln.StartPoint.z);
                if (tMark == textMarkCount)
                {
                    ln.EndPoint = new VectorDraw.Geometry.gPoint(ln.StartPoint.x + 1.8d, ln.StartPoint.y, ln.StartPoint.z);
                }
                doc.ActiveLayOut.Entities.AddItem(ln);


                vdText vTxt = new vdText();
                vTxt.SetUnRegisterDocument(doc);
                vTxt.setDocumentDefaults();
                vTxt.InsertionPoint = new VectorDraw.Geometry.gPoint(ln.EndPoint.x + 0.5d, ln.EndPoint.y - 0.4d, ln.EndPoint.z);

                //if (i < 5)
                //{
                //    double d = max * (((double)cnt) / 4);
                //    vTxt.TextString = "-" + d.ToString("0.0000");
                //    cnt--;
                //}
                //else
                //{
                //    double d = max * (((double)cnt) / 4);
                //    vTxt.TextString = d.ToString("0.0000");
                //    cnt++;
                //}
                //vTxt.Height = 2.5d;
                //if (tMark == textMarkCount)
                //{
                //    doc.ActiveLayOut.Entities.AddItem(vTxt);
                //    tMark = 0;
                //}
                //tMark++;

                if (tMark == textMarkCount)
                {
                    if (i < 8)
                    {
                        double d = max * (((double)cnt) / 1.0d);
                        vTxt.TextString = "-" + d.ToString("0.0000");
                        cnt--;
                    }
                    else
                    {
                        double d = max * (((double)cnt) / 1.0d);
                        vTxt.TextString = d.ToString("0.0000");
                        cnt++;
                        highest = d;
                    }
                    vTxt.Height = 2.5d;
                    doc.ActiveLayOut.Entities.AddItem(vTxt);
                    tMark = 0;
                }
                tMark++;
            }

            vdLine midLine = new vdLine();
            midLine.StartPoint = (line1.StartPoint + line1.EndPoint) / 2;
            midLine.EndPoint = (line2.StartPoint + line2.EndPoint) / 2;
            midLine.SetUnRegisterDocument(doc);
            midLine.setDocumentDefaults();
            doc.ActiveLayOut.Entities.AddItem(midLine);


            vdRect rect = new vdRect();
            rect.SetUnRegisterDocument(doc);
            rect.setDocumentDefaults();
            rect.InsertionPoint = new VectorDraw.Geometry.gPoint(-17.0d, -0.3d, 0.0d);
            rect.Width = 88.0d;
            rect.Height = 38.0d;
            rect.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(rect);

            doc.RenderMode = VectorDraw.Render.vdRender.Mode.Wire2d;
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);



            vdLine beamLine = new vdLine();

            if (st > 0)
            {
                beamLine.StartPoint.x = midLine.StartPoint.x + (line1.EndPoint.x - midLine.StartPoint.x) * (st / highest);
                beamLine.StartPoint.y = midLine.StartPoint.y + (line1.EndPoint.y - midLine.StartPoint.y) * (st / highest);
                beamLine.StartPoint.z = midLine.StartPoint.z + (line1.EndPoint.z - midLine.StartPoint.z) * (st / highest);
            }
            else
            {
                beamLine.StartPoint.x = midLine.StartPoint.x - (line1.StartPoint.x - midLine.StartPoint.x) * (st / highest);
                beamLine.StartPoint.y = midLine.StartPoint.y - (line1.StartPoint.y - midLine.StartPoint.y) * (st / highest);
                beamLine.StartPoint.z = midLine.StartPoint.z - (line1.StartPoint.z - midLine.StartPoint.z) * (st / highest);
            }
            if (en > 0)
            {
                beamLine.EndPoint.x = midLine.EndPoint.x + (line2.EndPoint.x - midLine.EndPoint.x) * (en / highest);
                beamLine.EndPoint.y = midLine.EndPoint.y + (line2.EndPoint.y - midLine.EndPoint.y) * (en / highest);
                beamLine.EndPoint.z = midLine.EndPoint.z + (line2.EndPoint.z - midLine.EndPoint.z) * (en / highest);
            }
            else
            {
                beamLine.EndPoint.x = midLine.EndPoint.x - (line2.StartPoint.x - midLine.EndPoint.x) * (en / highest);
                beamLine.EndPoint.y = midLine.EndPoint.y - (line2.StartPoint.y - midLine.EndPoint.y) * (en / highest);
                beamLine.EndPoint.z = midLine.EndPoint.z - (line2.StartPoint.z - midLine.EndPoint.z) * (en / highest);
            }
            beamLine.SetUnRegisterDocument(doc);
            beamLine.setDocumentDefaults();
            doc.ActiveLayOut.Entities.AddItem(beamLine);

            vdRect stBox = new vdRect();
            stBox.SetUnRegisterDocument(doc);
            stBox.setDocumentDefaults();
            stBox.InsertionPoint = new
                VectorDraw.Geometry.gPoint(beamLine.StartPoint.x - 0.2d, beamLine.StartPoint.y - 0.2d, beamLine.StartPoint.z);
            stBox.Width = 0.4;
            stBox.Height = 0.4;
            stBox.PenColor = new vdColor(Color.Red);
            stBox.ToolTip = st.ToString("0.0000");
            doc.ActiveLayOut.Entities.AddItem(stBox);

            vdText stTxt = new vdText();
            stTxt.SetUnRegisterDocument(doc);
            stTxt.setDocumentDefaults();
            stTxt.InsertionPoint = new
                VectorDraw.Geometry.gPoint(beamLine.StartPoint.x + 0.2d, beamLine.StartPoint.y + 0.2d, beamLine.StartPoint.z);
            stTxt.TextString = st.ToString("0.0000");
            stTxt.Height = 2.4;
            stTxt.PenColor = new vdColor(Color.Red);
            stTxt.ToolTip = st.ToString("0.0000");
            doc.ActiveLayOut.Entities.AddItem(stTxt);



            #region Draw Fixed End Moment

            stTxt = new vdText();
            stTxt.SetUnRegisterDocument(doc);
            stTxt.setDocumentDefaults();
            stTxt.InsertionPoint = new
                VectorDraw.Geometry.gPoint(9.3d, 31.24, beamLine.StartPoint.z);
            stTxt.TextString = "FIXED END MOMENT";
            stTxt.Height = 3.0d;
            stTxt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(stTxt);

            #endregion

            #region Draw Member No.

            stTxt = new vdText();
            stTxt.SetUnRegisterDocument(doc);
            stTxt.setDocumentDefaults();
            stTxt.InsertionPoint = new
                VectorDraw.Geometry.gPoint(16.0d, 1.05d, beamLine.StartPoint.z);
            stTxt.TextString = "Member No: " + list[beamIndex].Member.MemberNo;
            stTxt.Height = 3.0d;
            stTxt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(stTxt);

            #endregion

            #region Draw Mx [TORSON]

            stTxt = new vdText();
            stTxt.SetUnRegisterDocument(doc);
            stTxt.setDocumentDefaults();
            stTxt.InsertionPoint = new
                VectorDraw.Geometry.gPoint(15.0d, 27.35d, beamLine.StartPoint.z);
            stTxt.TextString = "Mx [TORSON]";
            stTxt.Height = 3.0d;
            stTxt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(stTxt);

            #endregion

            #region Draw Rectangle
            vdRect endBox = new vdRect();
            endBox.SetUnRegisterDocument(doc);
            endBox.setDocumentDefaults();
            endBox.InsertionPoint = new
                VectorDraw.Geometry.gPoint(beamLine.EndPoint.x - 0.2d, beamLine.EndPoint.y - 0.2d, beamLine.EndPoint.z);
            endBox.Width = 0.4;
            endBox.Height = 0.4;
            endBox.PenColor = new vdColor(Color.Red);
            endBox.ToolTip = en.ToString("0.0000");
            doc.ActiveLayOut.Entities.AddItem(endBox);
            #endregion

            vdText endTxt = new vdText();
            endTxt.SetUnRegisterDocument(doc);
            endTxt.setDocumentDefaults();
            endTxt.InsertionPoint = new
                VectorDraw.Geometry.gPoint(beamLine.EndPoint.x - (1.8d * ((double)en.ToString().Length)), beamLine.EndPoint.y - 0.2d, beamLine.EndPoint.z);
            endTxt.TextString = en.ToString("0.0000");
            endTxt.Height = 2.4;
            endTxt.PenColor = new vdColor(Color.Red);
            endTxt.ToolTip = en.ToString("0.0000");
            doc.ActiveLayOut.Entities.AddItem(endTxt);

            doc.ShowUCSAxis = false;
            doc.Redraw(true);
        }
        public void DrawMemberDiagram(int MemberNo,ASTRADoc Doc)
        {

            

        }
        public void DrawBeamForceMoment(vdDocument doc, int MemberNo, eForce force, int loadCase)
        {

            #region Variables
            doc.ActionLayout.Entities.RemoveAll();
            int indx = -1;
            int cnt = 2;
            double shortLineCount = 16;
            int textMarkCount = 4;
            int tMark = textMarkCount;
            int beamIndex = IndexOf(MemberNo, loadCase);
            st = en = 0.0d;

            indx = 0;
            #endregion

            #region switch case
            switch (force)
            {
                case eForce.R1:
                    indx = IndexOf(list[beamIndex].Member.MemberNo, loadCase);
                    if (indx != -1)
                    {
                        st = list[indx].StartForceMoment.R1;
                        en = list[indx].EndForceMoment.R1;
                    }
                    break;

                case eForce.R2:
                    indx = IndexOf(list[beamIndex].Member.MemberNo, loadCase);
                    if (indx != -1)
                    {
                        st = list[indx].StartForceMoment.R2;
                        en = list[indx].EndForceMoment.R2;
                    }
                    break;

                case eForce.R3:
                    indx = IndexOf(list[beamIndex].Member.MemberNo, loadCase);
                    if (indx != -1)
                    {
                        st = list[indx].StartForceMoment.R3;
                        en = list[indx].EndForceMoment.R3;
                    }
                    break;

                case eForce.M1:
                    indx = IndexOf(list[beamIndex].Member.MemberNo, loadCase);
                    if (indx != -1)
                    {
                        st = list[indx].StartForceMoment.M1;
                        en = list[indx].EndForceMoment.M1;
                    }
                    break;

                case eForce.M2:
                    indx = IndexOf(list[beamIndex].Member.MemberNo, loadCase);
                    if (indx != -1)
                    {
                        st = list[indx].StartForceMoment.M2;
                        en = list[indx].EndForceMoment.M2;
                    }
                    break;
                case eForce.M3:
                    indx = IndexOf(list[beamIndex].Member.MemberNo, loadCase);
                    if (indx != -1)
                    {
                        st = list[indx].StartForceMoment.M3;
                        en = list[indx].EndForceMoment.M3;
                    }
                    break;
            }
            #endregion

            indx = 0;

            #region Check Maximum And Minimum
            if (Math.Abs(st) > Math.Abs(en))
            {
                max = Math.Abs(st);
            }
            else
            {
                max = Math.Abs(en);
            }
            #endregion


            #region Start Node Line
            vdLine line1 = new vdLine();
            line1.SetUnRegisterDocument(doc);
            line1.setDocumentDefaults();
            line1.StartPoint = new VectorDraw.Geometry.gPoint(2.0d, 5.0d, 0);
            line1.EndPoint = new VectorDraw.Geometry.gPoint(2.0d, 26.0d, 0);

            doc.ActiveLayOut.Entities.AddItem(line1);
            doc.Redraw(true);
            #endregion

            
            #region First Short Liles
            for (int i = 0; i <= (int)shortLineCount; i++)
            {
                vdLine ln = new vdLine();
                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();

                ln.StartPoint.x = line1.StartPoint.x + (line1.EndPoint.x - line1.StartPoint.x) * (((double)(i) / shortLineCount));
                ln.StartPoint.y = line1.StartPoint.y + (line1.EndPoint.y - line1.StartPoint.y) * (((double)(i) / shortLineCount));
                ln.StartPoint.z = line1.StartPoint.z + (line1.EndPoint.z - line1.StartPoint.z) * (((double)(i) / shortLineCount));

                ln.EndPoint = new VectorDraw.Geometry.gPoint(ln.StartPoint.x - 1.2d, ln.StartPoint.y, ln.StartPoint.z);
                if (tMark == textMarkCount)
                {
                    ln.EndPoint = new VectorDraw.Geometry.gPoint(ln.StartPoint.x - 1.8d, ln.StartPoint.y, ln.StartPoint.z);
                }
                doc.ActiveLayOut.Entities.AddItem(ln);


                vdText vTxt = new vdText();
                vTxt.SetUnRegisterDocument(doc);
                vTxt.setDocumentDefaults();

                //double dd = 
                vTxt.InsertionPoint = new VectorDraw.Geometry.gPoint(ln.EndPoint.x - 12.0d, ln.EndPoint.y - 1.3d, ln.EndPoint.z);

                if (tMark == textMarkCount)
                {
                    if (i < 8)
                    {
                        double d = max * (((double)cnt) / 1.0d);
                        vTxt.TextString = "-" + d.ToString("0.0000");
                        cnt--;
                    }
                    else
                    {
                        double d = max * (((double)cnt) / 1.0d);
                        vTxt.TextString = d.ToString("0.0000");
                        cnt++;
                        highest = d;
                    }
                    vTxt.Height = 2.5d;
                    doc.ActiveLayOut.Entities.AddItem(vTxt);
                    tMark = 0;
                }


                double dd = (int)max.ToString("0.0000").Length;
                vTxt.InsertionPoint = new VectorDraw.Geometry.gPoint(ln.EndPoint.x - 2.0d * dd, ln.EndPoint.y - 1.3d, ln.EndPoint.z);


                tMark++;
            }
            #endregion



            #region End Node Line
            vdLine line2 = new vdLine();
            line2.SetUnRegisterDocument(doc);
            line2.setDocumentDefaults();
            line2.StartPoint = new VectorDraw.Geometry.gPoint(55.0d, 5.0d, 0);
            line2.EndPoint = new VectorDraw.Geometry.gPoint(55.0d, 26.0d, 0);

            doc.ActiveLayOut.Entities.AddItem(line2);
            doc.Redraw(true);
            #endregion


            #region End Short Line

            cnt = 2;
            tMark = textMarkCount;
            for (int i = 0; i <= (int)shortLineCount; i++)
            {
                vdLine ln = new vdLine();
                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();

                ln.StartPoint.x = line2.StartPoint.x + (line2.EndPoint.x - line2.StartPoint.x) * (((double)(i) / shortLineCount));
                ln.StartPoint.y = line2.StartPoint.y + (line2.EndPoint.y - line2.StartPoint.y) * (((double)(i) / shortLineCount));
                ln.StartPoint.z = line2.StartPoint.z + (line2.EndPoint.z - line2.StartPoint.z) * (((double)(i) / shortLineCount));

                ln.EndPoint = new VectorDraw.Geometry.gPoint(ln.StartPoint.x + 1.2d, ln.StartPoint.y, ln.StartPoint.z);
                if (tMark == textMarkCount)
                {
                    ln.EndPoint = new VectorDraw.Geometry.gPoint(ln.StartPoint.x + 1.8d, ln.StartPoint.y, ln.StartPoint.z);
                }
                doc.ActiveLayOut.Entities.AddItem(ln);


                vdText vTxt = new vdText();
                vTxt.SetUnRegisterDocument(doc);
                vTxt.setDocumentDefaults();
                vTxt.InsertionPoint = new VectorDraw.Geometry.gPoint(ln.EndPoint.x + 1.5d, ln.EndPoint.y - 0.4d, ln.EndPoint.z);

                if (tMark == textMarkCount)
                {
                    if (i < 8)
                    {
                        double d = max * (((double)cnt) / 1.0d);
                        vTxt.TextString = "-" + d.ToString("0.0000");
                        cnt--;
                    }
                    else
                    {
                        double d = max * (((double)cnt) / 1.0d);
                        vTxt.TextString = d.ToString("0.0000");
                        cnt++;
                        highest = d;
                    }
                    vTxt.Height = 2.5d;
                    doc.ActiveLayOut.Entities.AddItem(vTxt);
                    tMark = 0;
                }
                tMark++;
            }
            #endregion


            #region Middle Line
            vdLine midLine = new vdLine();
            midLine.StartPoint = (line1.StartPoint + line1.EndPoint) / 2;
            midLine.EndPoint = (line2.StartPoint + line2.EndPoint) / 2;
            midLine.SetUnRegisterDocument(doc);
            midLine.setDocumentDefaults();
            doc.ActiveLayOut.Entities.AddItem(midLine);
            #endregion


            #region Draw OUTER Rectangle
            vdRect rect = new vdRect();
            rect.SetUnRegisterDocument(doc);
            rect.setDocumentDefaults();
            rect.InsertionPoint = new VectorDraw.Geometry.gPoint(-22.48d, -0.7d, 0.0d);
            rect.Width = 100.0d;
            rect.Height = 38.0d;
            rect.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(rect);

            doc.RenderMode = VectorDraw.Render.vdRender.Mode.Wire2d;
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);
            #endregion

            #region BEAM LINE

            vdLine beamLine = new vdLine();

            if (st > 0)
            {
                beamLine.StartPoint.x = midLine.StartPoint.x + (line1.EndPoint.x - midLine.StartPoint.x) * (st / highest);
                beamLine.StartPoint.y = midLine.StartPoint.y + (line1.EndPoint.y - midLine.StartPoint.y) * (st / highest);
                beamLine.StartPoint.z = midLine.StartPoint.z + (line1.EndPoint.z - midLine.StartPoint.z) * (st / highest);
            }
            else
            {
                beamLine.StartPoint.x = midLine.StartPoint.x - (line1.StartPoint.x - midLine.StartPoint.x) * (st / highest);
                beamLine.StartPoint.y = midLine.StartPoint.y - (line1.StartPoint.y - midLine.StartPoint.y) * (st / highest);
                beamLine.StartPoint.z = midLine.StartPoint.z - (line1.StartPoint.z - midLine.StartPoint.z) * (st / highest);
            }
            if (en > 0)
            {
                beamLine.EndPoint.x = midLine.EndPoint.x + (line2.EndPoint.x - midLine.EndPoint.x) * (en / highest);
                beamLine.EndPoint.y = midLine.EndPoint.y + (line2.EndPoint.y - midLine.EndPoint.y) * (en / highest);
                beamLine.EndPoint.z = midLine.EndPoint.z + (line2.EndPoint.z - midLine.EndPoint.z) * (en / highest);
            }
            else
            {
                beamLine.EndPoint.x = midLine.EndPoint.x - (line2.StartPoint.x - midLine.EndPoint.x) * (en / highest);
                beamLine.EndPoint.y = midLine.EndPoint.y - (line2.StartPoint.y - midLine.EndPoint.y) * (en / highest);
                beamLine.EndPoint.z = midLine.EndPoint.z - (line2.StartPoint.z - midLine.EndPoint.z) * (en / highest);
            }
            beamLine.SetUnRegisterDocument(doc);
            beamLine.setDocumentDefaults();
            beamLine.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(beamLine);





            if (force == eForce.M2 || force == eForce.M3)
            {
                vdPolyline pln = new vdPolyline();
                pln.SetUnRegisterDocument(doc);
                pln.setDocumentDefaults();
                pln.SPlineFlag = VectorDraw.Professional.Constants.VdConstSplineFlag.SFlagFITTING;


                pln.VertexList.Add(beamLine.StartPoint);


                pln.VertexList.Add((beamLine.StartPoint + beamLine.EndPoint) / 2);

                //pln.VertexList[1].y = (-(beamLine.StartPoint.y * (st / highest) + beamLine.EndPoint.y * (en / highest)) / 2.0) * (en / highest);



                pln.VertexList[1].y = midLine.StartPoint.y  - ((beamLine.StartPoint.y * (st / highest) + beamLine.EndPoint.y * (en / highest)) / 2.0) * (en / highest);


                //if (st > 0)
                //{
                //    pln.VertexList[1].y = midLine.StartPoint.y + (line1.EndPoint.y - midLine.StartPoint.y) * (st / highest);
                //}
                //else
                //{
                //    beamLine.StartPoint.y = midLine.StartPoint.y - (line1.StartPoint.y - midLine.StartPoint.y) * (st / highest);
                //}
                //if (en > 0)
                //{
                //    beamLine.EndPoint.y = midLine.EndPoint.y + (line2.EndPoint.y - midLine.EndPoint.y) * (en / highest);
                //}
                //else
                //{
                //    beamLine.EndPoint.y = midLine.EndPoint.y - (line2.StartPoint.y - midLine.EndPoint.y) * (en / highest);
                //}




                pln.VertexList.Add(beamLine.EndPoint);
                doc.ActiveLayOut.Entities.Add(pln);

            }





            #endregion

            #region START BOX

            vdRect stBox = new vdRect();
            stBox.SetUnRegisterDocument(doc);
            stBox.setDocumentDefaults();
            stBox.InsertionPoint = new
                VectorDraw.Geometry.gPoint(beamLine.StartPoint.x - 0.2d, beamLine.StartPoint.y - 0.2d, beamLine.StartPoint.z);
            stBox.Width = 0.4;
            stBox.Height = 0.4;
            stBox.PenColor = new vdColor(Color.Red);
            stBox.ToolTip = st.ToString("0.0000");
            doc.ActiveLayOut.Entities.AddItem(stBox);
            #endregion

            #region START TEXT
            vdText stTxt = new vdText();
            stTxt.SetUnRegisterDocument(doc);
            stTxt.setDocumentDefaults();
            stTxt.InsertionPoint = new
                VectorDraw.Geometry.gPoint(beamLine.StartPoint.x + 0.2d, beamLine.StartPoint.y + 0.2d, beamLine.StartPoint.z);
            stTxt.TextString = st.ToString("0.0000");
            stTxt.Height = 2.4;
            stTxt.PenColor = new vdColor(Color.Red);
            stTxt.ToolTip = st.ToString("0.0000");
            doc.ActiveLayOut.Entities.AddItem(stTxt);
            #endregion


            #region Draw Fixed End Moment

            stTxt = new vdText();
            stTxt.SetUnRegisterDocument(doc);
            stTxt.setDocumentDefaults();
            switch (force)
            {
                case eForce.M1:
                    stTxt.TextString = "MOMENT DIAGRAM";
                    stTxt.InsertionPoint = new
                        VectorDraw.Geometry.gPoint(11.72d, 32.818, beamLine.StartPoint.z);
                    break;
                case eForce.M2:
                    stTxt.TextString = "MOMENT DIAGRAM";
                    stTxt.InsertionPoint = new
                        VectorDraw.Geometry.gPoint(11.72d, 32.818, beamLine.StartPoint.z);
                    break;
                case eForce.M3:
                    stTxt.TextString = "MOMENT DIAGRAM";
                    stTxt.InsertionPoint = new
                        VectorDraw.Geometry.gPoint(11.72d, 32.818, beamLine.StartPoint.z);
                    break;
                case eForce.R1:
                    stTxt.TextString = "END SHEAR DIAGRAM";
                    stTxt.InsertionPoint = new
                      VectorDraw.Geometry.gPoint(7.0902d, 32.818, beamLine.StartPoint.z);
                    break;
                case eForce.R2:
                    stTxt.TextString = "END SHEAR DIAGRAM";
                    stTxt.InsertionPoint = new
                        //VectorDraw.Geometry.gPoint(3.9225d, 31.48, beamLine.StartPoint.z);
                      VectorDraw.Geometry.gPoint(7.0902d, 32.818, beamLine.StartPoint.z);
                    break;
                case eForce.R3:
                    stTxt.TextString = "END SHEAR DIAGRAM";
                    stTxt.InsertionPoint = new
                        //VectorDraw.Geometry.gPoint(3.9225d, 31.48, beamLine.StartPoint.z);
                      VectorDraw.Geometry.gPoint(7.0902d, 32.818, beamLine.StartPoint.z);
                    break;
            }
            stTxt.Height = 3.0d;
            stTxt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(stTxt);

            #endregion

            #region Draw Member No.

            stTxt = new vdText();
            stTxt.SetUnRegisterDocument(doc);
            stTxt.setDocumentDefaults();
            stTxt.InsertionPoint = new
                VectorDraw.Geometry.gPoint(3.70d, 1.05d, beamLine.StartPoint.z);
            stTxt.TextString = "Member : " + list[beamIndex].Member.MemberNo + " , Load Case : " + list[beamIndex].LoadCase.ToString();
            //stTxt.TextString = "Member No: " + list[beamIndex].Member.MemberNo + ";
            stTxt.Height = 3.0d;
            stTxt.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(stTxt);

            #endregion

            #region Draw Mx [TORSON]

            //stTxt = new vdText();
            //stTxt.SetUnRegisterDocument(doc);
            //stTxt.setDocumentDefaults();
            //stTxt.InsertionPoint = new VectorDraw.Geometry.gPoint(15.0d, 27.35d, beamLine.StartPoint.z);
            //switch (force)
            //{
            //    case eForce.M1:
            //        stTxt.TextString = "Mx [TORSON]";
            //        break;
            //    case eForce.M2:
            //        stTxt.TextString = "My [FEM]";
            //        stTxt.InsertionPoint = new VectorDraw.Geometry.gPoint(20.0d, 27.35d, beamLine.StartPoint.z);
            //        break;
            //    case eForce.M3:
            //        stTxt.TextString = "Mz [FEM]";
            //        stTxt.InsertionPoint = new VectorDraw.Geometry.gPoint(20.0d, 27.35d, beamLine.StartPoint.z);
            //        break;
            //    case eForce.R1:
            //        stTxt.TextString = "Fx [AXIAL]";
            //        break;
            //    case eForce.R2:
            //        stTxt.TextString = "Fy [SHEAR]";
            //        break;
            //    case eForce.R3:
            //        stTxt.TextString = "Fz [SHEAR]";
            //        break;

            //}
            //stTxt.Height = 3.0d;
            //stTxt.PenColor = new vdColor(Color.Red);
            //doc.ActiveLayOut.Entities.AddItem(stTxt);

            #endregion

            #region Draw Rectangle
            vdRect endBox = new vdRect();
            endBox.SetUnRegisterDocument(doc);
            endBox.setDocumentDefaults();
            endBox.InsertionPoint = new
                VectorDraw.Geometry.gPoint(beamLine.EndPoint.x - 0.2d, beamLine.EndPoint.y - 0.2d, beamLine.EndPoint.z);
            endBox.Width = 0.4;
            endBox.Height = 0.4;
            endBox.PenColor = new vdColor(Color.Red);
            endBox.ToolTip = en.ToString("0.0000");
            doc.ActiveLayOut.Entities.AddItem(endBox);
            #endregion

            vdText endTxt = new vdText();
            endTxt.SetUnRegisterDocument(doc);
            endTxt.setDocumentDefaults();
            endTxt.InsertionPoint = new
                VectorDraw.Geometry.gPoint(beamLine.EndPoint.x - (1.6d * ((double)en.ToString("0.0000").Length)), beamLine.EndPoint.y - 0.2d, beamLine.EndPoint.z);
            endTxt.TextString = en.ToString("0.0000");
            endTxt.Height = 2.2d;
            endTxt.PenColor = new vdColor(Color.Red);
            endTxt.ToolTip = en.ToString("0.0000");
            doc.ActiveLayOut.Entities.AddItem(endTxt);


            #region Start Node

            endTxt = new vdText();
            endTxt.SetUnRegisterDocument(doc);
            endTxt.setDocumentDefaults();
            endTxt.InsertionPoint = new
                VectorDraw.Geometry.gPoint(-16.7169, 29.1850, beamLine.EndPoint.z);
            //endTxt.TextString = "START";
            endTxt.TextString = "NODE " + list[beamIndex].Member.StartNode.NodeNo;
            endTxt.Height = 2.4d;
            endTxt.PenColor = new vdColor(Color.Red);
            endTxt.ToolTip = en.ToString("0.0000");
            doc.ActiveLayOut.Entities.AddItem(endTxt);
            #endregion

            #region End Node

            endTxt = new vdText();
            endTxt.SetUnRegisterDocument(doc);
            endTxt.setDocumentDefaults();
            endTxt.InsertionPoint = new
                VectorDraw.Geometry.gPoint(57.6218d, 29.1850d, beamLine.EndPoint.z);
            //endTxt.TextString = "END";
            endTxt.TextString = "NODE " + list[beamIndex].Member.EndNode.NodeNo;
            endTxt.Height = 2.4d;
            endTxt.PenColor = new vdColor(Color.Red);
            //endTxt.ToolTip = en.ToString("0.0000");
            doc.ActiveLayOut.Entities.AddItem(endTxt);
            #endregion

            doc.ShowUCSAxis = false;
            doc.Redraw(true);
        }
        public void DrawBeamForceMoment_R1(vdDocument doc, int beamIndex)
        {
            doc.ActionLayout.Entities.RemoveAll();

            vdLine line1 = new vdLine();
            line1.SetUnRegisterDocument(doc);
            line1.setDocumentDefaults();

            line1.StartPoint = new VectorDraw.Geometry.gPoint(2.0d, 5.0d, 0.0d);
            line1.EndPoint = new VectorDraw.Geometry.gPoint(2.0d, 19.0d, 0.0d);
            doc.ActionLayout.Entities.AddItem(line1);

            int cnt = -7;

            for (int i = 0; i <= 14; i++)
            {
                vdLine line3 = new vdLine();
                line3.SetUnRegisterDocument(doc);
                line3.setDocumentDefaults();

                line3.StartPoint = new VectorDraw.Geometry.gPoint(2.0d, (i + 5.0d), 0.0d);
                line3.EndPoint = new VectorDraw.Geometry.gPoint(1.75d, (i + 5.0d), 0.0d);
                doc.ActionLayout.Entities.AddItem(line3);

                vdText vTxt = new vdText();
                vTxt.SetUnRegisterDocument(doc);
                vTxt.setDocumentDefaults();
                //if (i < 8)
                //{
                //    cnt--;
                //}
                //else
                //{
                //    cnt++;
                //}
                vTxt.InsertionPoint = new VectorDraw.Geometry.gPoint(line3.EndPoint.x - 3.0d, line3.EndPoint.y - 0.15d);
                vTxt.TextString = (list[beamIndex].StartForceMoment.R1 * ((double)(i + 1) / 15.0d)).ToString("0.0000");
                //vTxt.TextString = cnt.ToString();

                vTxt.Height = 0.5d;
                doc.ActionLayout.Entities.AddItem(vTxt);
                cnt++;
            }


            vdLine line2 = new vdLine();
            line2.SetUnRegisterDocument(doc);
            line2.setDocumentDefaults();

            line2.StartPoint = new VectorDraw.Geometry.gPoint(32.0d, 5.0d, 0.0d);
            line2.EndPoint = new VectorDraw.Geometry.gPoint(32.0d, 19.0d, 0.0d);
            doc.ActionLayout.Entities.AddItem(line2);


            cnt = -7;
            for (int i = 0; i <= 14; i++)
            {
                vdLine line3 = new vdLine();
                line3.SetUnRegisterDocument(doc);
                line3.setDocumentDefaults();

                line3.StartPoint = new VectorDraw.Geometry.gPoint(32.0d, (i + 5.0d), 0.0d);
                line3.EndPoint = new VectorDraw.Geometry.gPoint(32.25d, (i + 5.0d), 0.0d);
                doc.ActionLayout.Entities.AddItem(line3);


                vdText vTxt = new vdText();
                vTxt.SetUnRegisterDocument(doc);
                vTxt.setDocumentDefaults();
                vTxt.InsertionPoint = new VectorDraw.Geometry.gPoint(line3.EndPoint.x + 0.2d, line3.EndPoint.y - 0.15d);
                //vTxt.InsertionPoint = line3.EndPoint;

                //if (i < 8)
                //{
                //    cnt--;
                //}
                //else if (i > 8)
                //{
                //    cnt++;
                //}
                vTxt.TextString = (list[beamIndex].StartForceMoment.R1 * ((double)(i + 1) / 15.0d)).ToString("0.0000");

                //vTxt.TextString = cnt.ToString();
                vTxt.Height = 0.5d;
                doc.ActionLayout.Entities.AddItem(vTxt);
                cnt++;
            }

            vdLine midLine = new vdLine();
            midLine.SetUnRegisterDocument(doc);
            midLine.setDocumentDefaults();

            midLine.StartPoint = (line1.StartPoint + line1.EndPoint) / 2;
            midLine.EndPoint = (line2.StartPoint + line2.EndPoint) / 2;
            //midLine.EndPoint = new VectorDraw.Geometry.gPoint(32.25d, (i + 1.0d), 0.0d);
            doc.ActionLayout.Entities.AddItem(midLine);


            vdRect rect = new vdRect();
            rect.SetUnRegisterDocument(doc);
            rect.setDocumentDefaults();

            rect.InsertionPoint = new VectorDraw.Geometry.gPoint(-2, 4.0d, 0);
            rect.Width = 38.50d;
            rect.Height = 16.0d;
            doc.ActionLayout.Entities.AddItem(rect);

            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);

            doc.RenderMode = VectorDraw.Render.vdRender.Mode.Wire2d;
            doc.Redraw(true);
        }
        public void DrawBeamForceMoment_R2(vdDocument doc, int beamIndex)
        {
            doc.ActionLayout.Entities.RemoveAll();

            vdLine line1 = new vdLine();
            line1.SetUnRegisterDocument(doc);
            line1.setDocumentDefaults();

            line1.StartPoint = new VectorDraw.Geometry.gPoint(2.0d, 5.0d, 0.0d);
            line1.EndPoint = new VectorDraw.Geometry.gPoint(2.0d, 19.0d, 0.0d);
            doc.ActionLayout.Entities.AddItem(line1);

            int cnt = -7;

            for (int i = 0; i <= 14; i++)
            {
                vdLine line3 = new vdLine();
                line3.SetUnRegisterDocument(doc);
                line3.setDocumentDefaults();

                line3.StartPoint = new VectorDraw.Geometry.gPoint(2.0d, (i + 5.0d), 0.0d);
                line3.EndPoint = new VectorDraw.Geometry.gPoint(1.75d, (i + 5.0d), 0.0d);
                doc.ActionLayout.Entities.AddItem(line3);

                vdText vTxt = new vdText();
                vTxt.SetUnRegisterDocument(doc);
                vTxt.setDocumentDefaults();
                //if (i < 8)
                //{
                //    cnt--;
                //}
                //else
                //{
                //    cnt++;
                //}
                vTxt.InsertionPoint = new VectorDraw.Geometry.gPoint(line3.EndPoint.x - 3.0d, line3.EndPoint.y - 0.15d);
                vTxt.TextString = (list[beamIndex].StartForceMoment.R2 * ((double)(i + 1) / 15.0d)).ToString("0.0000");
                //vTxt.TextString = cnt.ToString();

                vTxt.Height = 0.5d;
                doc.ActionLayout.Entities.AddItem(vTxt);
                cnt++;
            }


            vdLine line2 = new vdLine();
            line2.SetUnRegisterDocument(doc);
            line2.setDocumentDefaults();

            line2.StartPoint = new VectorDraw.Geometry.gPoint(32.0d, 5.0d, 0.0d);
            line2.EndPoint = new VectorDraw.Geometry.gPoint(32.0d, 19.0d, 0.0d);
            doc.ActionLayout.Entities.AddItem(line2);


            cnt = -7;
            for (int i = 0; i <= 14; i++)
            {
                vdLine line3 = new vdLine();
                line3.SetUnRegisterDocument(doc);
                line3.setDocumentDefaults();

                line3.StartPoint = new VectorDraw.Geometry.gPoint(32.0d, (i + 5.0d), 0.0d);
                line3.EndPoint = new VectorDraw.Geometry.gPoint(32.25d, (i + 5.0d), 0.0d);
                doc.ActionLayout.Entities.AddItem(line3);


                vdText vTxt = new vdText();
                vTxt.SetUnRegisterDocument(doc);
                vTxt.setDocumentDefaults();
                vTxt.InsertionPoint = new VectorDraw.Geometry.gPoint(line3.EndPoint.x + 0.2d, line3.EndPoint.y - 0.15d);
                //vTxt.InsertionPoint = line3.EndPoint;

                //if (i < 8)
                //{
                //    cnt--;
                //}
                //else if (i > 8)
                //{
                //    cnt++;
                //}
                vTxt.TextString = (list[beamIndex].StartForceMoment.R2 * ((double)(i + 1) / 15.0d)).ToString("0.0000");

                //vTxt.TextString = cnt.ToString();
                vTxt.Height = 0.5d;
                doc.ActionLayout.Entities.AddItem(vTxt);
                cnt++;
            }

            vdLine midLine = new vdLine();
            midLine.SetUnRegisterDocument(doc);
            midLine.setDocumentDefaults();

            midLine.StartPoint = (line1.StartPoint + line1.EndPoint) / 2;
            midLine.EndPoint = (line2.StartPoint + line2.EndPoint) / 2;
            //midLine.EndPoint = new VectorDraw.Geometry.gPoint(32.25d, (i + 1.0d), 0.0d);
            doc.ActionLayout.Entities.AddItem(midLine);



            vdRect rect = new vdRect();
            rect.SetUnRegisterDocument(doc);
            rect.setDocumentDefaults();

            rect.InsertionPoint = new VectorDraw.Geometry.gPoint(-2, 4.0d, 0);
            rect.Width = 38.50d;
            rect.Height = 16.0d;
            doc.ActionLayout.Entities.AddItem(rect);

            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);

            doc.RenderMode = VectorDraw.Render.vdRender.Mode.Wire2d;
            doc.Redraw(true);
        }
        public void DrawBeamForceMoment_R3(vdDocument doc, int beamIndex)
        {
            doc.ActionLayout.Entities.RemoveAll();

            vdLine line1 = new vdLine();
            line1.SetUnRegisterDocument(doc);
            line1.setDocumentDefaults();

            line1.StartPoint = new VectorDraw.Geometry.gPoint(2.0d, 5.0d, 0.0d);
            line1.EndPoint = new VectorDraw.Geometry.gPoint(2.0d, 19.0d, 0.0d);
            doc.ActionLayout.Entities.AddItem(line1);

            int cnt = -7;

            for (int i = 0; i <= 14; i++)
            {
                vdLine line3 = new vdLine();
                line3.SetUnRegisterDocument(doc);
                line3.setDocumentDefaults();

                line3.StartPoint = new VectorDraw.Geometry.gPoint(2.0d, (i + 5.0d), 0.0d);
                line3.EndPoint = new VectorDraw.Geometry.gPoint(1.75d, (i + 5.0d), 0.0d);
                doc.ActionLayout.Entities.AddItem(line3);

                vdText vTxt = new vdText();
                vTxt.SetUnRegisterDocument(doc);
                vTxt.setDocumentDefaults();
                //if (i < 8)
                //{
                //    cnt--;
                //}
                //else
                //{
                //    cnt++;
                //}
                vTxt.InsertionPoint = new VectorDraw.Geometry.gPoint(line3.EndPoint.x - 3.0d, line3.EndPoint.y - 0.15d);
                vTxt.TextString = (list[beamIndex].StartForceMoment.R3 * ((double)(i + 1) / 15.0d)).ToString("0.0000");
                //vTxt.TextString = cnt.ToString();

                vTxt.Height = 0.5d;
                doc.ActionLayout.Entities.AddItem(vTxt);
                cnt++;
            }


            vdLine line2 = new vdLine();
            line2.SetUnRegisterDocument(doc);
            line2.setDocumentDefaults();

            line2.StartPoint = new VectorDraw.Geometry.gPoint(32.0d, 5.0d, 0.0d);
            line2.EndPoint = new VectorDraw.Geometry.gPoint(32.0d, 19.0d, 0.0d);
            doc.ActionLayout.Entities.AddItem(line2);


            cnt = -7;
            for (int i = 0; i <= 14; i++)
            {
                vdLine line3 = new vdLine();
                line3.SetUnRegisterDocument(doc);
                line3.setDocumentDefaults();

                line3.StartPoint = new VectorDraw.Geometry.gPoint(32.0d, (i + 5.0d), 0.0d);
                line3.EndPoint = new VectorDraw.Geometry.gPoint(32.25d, (i + 5.0d), 0.0d);
                doc.ActionLayout.Entities.AddItem(line3);


                vdText vTxt = new vdText();
                vTxt.SetUnRegisterDocument(doc);
                vTxt.setDocumentDefaults();
                vTxt.InsertionPoint = new VectorDraw.Geometry.gPoint(line3.EndPoint.x + 0.2d, line3.EndPoint.y - 0.15d);
                //vTxt.InsertionPoint = line3.EndPoint;

                //if (i < 8)
                //{
                //    cnt--;
                //}
                //else if (i > 8)
                //{
                //    cnt++;
                //}
                vTxt.TextString = (list[beamIndex].StartForceMoment.R3 * ((double)(i + 1) / 15.0d)).ToString("0.0000");

                //vTxt.TextString = cnt.ToString();
                vTxt.Height = 0.5d;
                doc.ActionLayout.Entities.AddItem(vTxt);
                cnt++;
            }

            vdLine midLine = new vdLine();
            midLine.SetUnRegisterDocument(doc);
            midLine.setDocumentDefaults();

            midLine.StartPoint = (line1.StartPoint + line1.EndPoint) / 2;
            midLine.EndPoint = (line2.StartPoint + line2.EndPoint) / 2;
            //midLine.EndPoint = new VectorDraw.Geometry.gPoint(32.25d, (i + 1.0d), 0.0d);
            doc.ActionLayout.Entities.AddItem(midLine);



            vdRect rect = new vdRect();
            rect.SetUnRegisterDocument(doc);
            rect.setDocumentDefaults();

            rect.InsertionPoint = new VectorDraw.Geometry.gPoint(-2, 4.0d, 0);
            rect.Width = 38.50d;
            rect.Height = 16.0d;
            doc.ActionLayout.Entities.AddItem(rect);

            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);

            doc.RenderMode = VectorDraw.Render.vdRender.Mode.Wire2d;
            doc.Redraw(true);
        }
        public void DrawBeamForceMoment_M1(vdDocument doc, int beamIndex)
        {
            doc.ActionLayout.Entities.RemoveAll();

            vdLine line1 = new vdLine();
            line1.SetUnRegisterDocument(doc);
            line1.setDocumentDefaults();

            line1.StartPoint = new VectorDraw.Geometry.gPoint(2.0d, 5.0d, 0.0d);
            line1.EndPoint = new VectorDraw.Geometry.gPoint(2.0d, 19.0d, 0.0d);
            doc.ActionLayout.Entities.AddItem(line1);

            int cnt = -7;

            for (int i = 0; i <= 14; i++)
            {
                vdLine line3 = new vdLine();
                line3.SetUnRegisterDocument(doc);
                line3.setDocumentDefaults();

                line3.StartPoint = new VectorDraw.Geometry.gPoint(2.0d, (i + 5.0d), 0.0d);
                line3.EndPoint = new VectorDraw.Geometry.gPoint(1.75d, (i + 5.0d), 0.0d);
                doc.ActionLayout.Entities.AddItem(line3);

                vdText vTxt = new vdText();
                vTxt.SetUnRegisterDocument(doc);
                vTxt.setDocumentDefaults();
                //if (i < 8)
                //{
                //    cnt--;
                //}
                //else
                //{
                //    cnt++;
                //}
                vTxt.InsertionPoint = new VectorDraw.Geometry.gPoint(line3.EndPoint.x - 3.0d, line3.EndPoint.y - 0.15d);
                vTxt.TextString = (list[beamIndex].StartForceMoment.M1 * ((double)(i + 1) / 15.0d)).ToString("0.0000");
                //vTxt.TextString = cnt.ToString();

                vTxt.Height = 0.5d;
                doc.ActionLayout.Entities.AddItem(vTxt);
                cnt++;
            }


            vdLine line2 = new vdLine();
            line2.SetUnRegisterDocument(doc);
            line2.setDocumentDefaults();

            line2.StartPoint = new VectorDraw.Geometry.gPoint(32.0d, 5.0d, 0.0d);
            line2.EndPoint = new VectorDraw.Geometry.gPoint(32.0d, 19.0d, 0.0d);
            doc.ActionLayout.Entities.AddItem(line2);


            cnt = -7;
            for (int i = 0; i <= 14; i++)
            {
                vdLine line3 = new vdLine();
                line3.SetUnRegisterDocument(doc);
                line3.setDocumentDefaults();

                line3.StartPoint = new VectorDraw.Geometry.gPoint(32.0d, (i + 5.0d), 0.0d);
                line3.EndPoint = new VectorDraw.Geometry.gPoint(32.25d, (i + 5.0d), 0.0d);
                doc.ActionLayout.Entities.AddItem(line3);


                vdText vTxt = new vdText();
                vTxt.SetUnRegisterDocument(doc);
                vTxt.setDocumentDefaults();
                vTxt.InsertionPoint = new VectorDraw.Geometry.gPoint(line3.EndPoint.x + 0.2d, line3.EndPoint.y - 0.15d);
                //vTxt.InsertionPoint = line3.EndPoint;

                //if (i < 8)
                //{
                //    cnt--;
                //}
                //else if (i > 8)
                //{
                //    cnt++;
                //}
                vTxt.TextString = (list[beamIndex].StartForceMoment.M1 * ((double)(i + 1) / 15.0d)).ToString("0.0000");

                //vTxt.TextString = cnt.ToString();
                vTxt.Height = 0.5d;
                doc.ActionLayout.Entities.AddItem(vTxt);
                cnt++;
            }

            vdLine midLine = new vdLine();
            midLine.SetUnRegisterDocument(doc);
            midLine.setDocumentDefaults();

            midLine.StartPoint = (line1.StartPoint + line1.EndPoint) / 2;
            midLine.EndPoint = (line2.StartPoint + line2.EndPoint) / 2;
            //midLine.EndPoint = new VectorDraw.Geometry.gPoint(32.25d, (i + 1.0d), 0.0d);
            doc.ActionLayout.Entities.AddItem(midLine);



            vdRect rect = new vdRect();
            rect.SetUnRegisterDocument(doc);
            rect.setDocumentDefaults();

            rect.InsertionPoint = new VectorDraw.Geometry.gPoint(-2, 4.0d, 0);
            rect.Width = 38.50d;
            rect.Height = 16.0d;
            doc.ActionLayout.Entities.AddItem(rect);

            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);

            doc.RenderMode = VectorDraw.Render.vdRender.Mode.Wire2d;
            doc.Redraw(true);
        }
        public void DrawBeamForceMoment_M2(vdDocument doc, int beamIndex)
        {
            doc.ActionLayout.Entities.RemoveAll();

            vdLine line1 = new vdLine();
            line1.SetUnRegisterDocument(doc);
            line1.setDocumentDefaults();

            line1.StartPoint = new VectorDraw.Geometry.gPoint(2.0d, 5.0d, 0.0d);
            line1.EndPoint = new VectorDraw.Geometry.gPoint(2.0d, 19.0d, 0.0d);
            doc.ActionLayout.Entities.AddItem(line1);

            int cnt = -7;

            for (int i = 0; i <= 14; i++)
            {
                vdLine line3 = new vdLine();
                line3.SetUnRegisterDocument(doc);
                line3.setDocumentDefaults();

                line3.StartPoint = new VectorDraw.Geometry.gPoint(2.0d, (i + 5.0d), 0.0d);
                line3.EndPoint = new VectorDraw.Geometry.gPoint(1.75d, (i + 5.0d), 0.0d);
                doc.ActionLayout.Entities.AddItem(line3);

                vdText vTxt = new vdText();
                vTxt.SetUnRegisterDocument(doc);
                vTxt.setDocumentDefaults();
                //if (i < 8)
                //{
                //    cnt--;
                //}
                //else
                //{
                //    cnt++;
                //}
                vTxt.InsertionPoint = new VectorDraw.Geometry.gPoint(line3.EndPoint.x - 3.0d, line3.EndPoint.y - 0.15d);
                vTxt.TextString = (list[beamIndex].StartForceMoment.M2 * ((double)(i + 1) / 15.0d)).ToString("0.0000");
                //vTxt.TextString = cnt.ToString();

                vTxt.Height = 0.5d;
                doc.ActionLayout.Entities.AddItem(vTxt);
                cnt++;
            }


            vdLine line2 = new vdLine();
            line2.SetUnRegisterDocument(doc);
            line2.setDocumentDefaults();

            line2.StartPoint = new VectorDraw.Geometry.gPoint(32.0d, 5.0d, 0.0d);
            line2.EndPoint = new VectorDraw.Geometry.gPoint(32.0d, 19.0d, 0.0d);
            doc.ActionLayout.Entities.AddItem(line2);


            cnt = -7;
            for (int i = 0; i <= 14; i++)
            {
                vdLine line3 = new vdLine();
                line3.SetUnRegisterDocument(doc);
                line3.setDocumentDefaults();

                line3.StartPoint = new VectorDraw.Geometry.gPoint(32.0d, (i + 5.0d), 0.0d);
                line3.EndPoint = new VectorDraw.Geometry.gPoint(32.25d, (i + 5.0d), 0.0d);
                doc.ActionLayout.Entities.AddItem(line3);


                vdText vTxt = new vdText();
                vTxt.SetUnRegisterDocument(doc);
                vTxt.setDocumentDefaults();
                vTxt.InsertionPoint = new VectorDraw.Geometry.gPoint(line3.EndPoint.x + 0.2d, line3.EndPoint.y - 0.15d);
                //vTxt.InsertionPoint = line3.EndPoint;

                //if (i < 8)
                //{
                //    cnt--;
                //}
                //else if (i > 8)
                //{
                //    cnt++;
                //}
                vTxt.TextString = (list[beamIndex].StartForceMoment.M2 * ((double)(i + 1) / 15.0d)).ToString("0.0000");

                //vTxt.TextString = cnt.ToString();
                vTxt.Height = 0.5d;
                doc.ActionLayout.Entities.AddItem(vTxt);
                cnt++;
            }

            vdLine midLine = new vdLine();
            midLine.SetUnRegisterDocument(doc);
            midLine.setDocumentDefaults();

            midLine.StartPoint = (line1.StartPoint + line1.EndPoint) / 2;
            midLine.EndPoint = (line2.StartPoint + line2.EndPoint) / 2;
            //midLine.EndPoint = new VectorDraw.Geometry.gPoint(32.25d, (i + 1.0d), 0.0d);
            doc.ActionLayout.Entities.AddItem(midLine);



            vdRect rect = new vdRect();
            rect.SetUnRegisterDocument(doc);
            rect.setDocumentDefaults();

            rect.InsertionPoint = new VectorDraw.Geometry.gPoint(-2, 4.0d, 0);
            rect.Width = 38.50d;
            rect.Height = 16.0d;
            doc.ActionLayout.Entities.AddItem(rect);

            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);

            doc.RenderMode = VectorDraw.Render.vdRender.Mode.Wire2d;
            doc.Redraw(true);
        }
        public void DrawBeamForceMoment_M3(vdDocument doc, int beamIndex)
        {
            doc.ActionLayout.Entities.RemoveAll();

            vdLine line1 = new vdLine();
            line1.SetUnRegisterDocument(doc);
            line1.setDocumentDefaults();

            line1.StartPoint = new VectorDraw.Geometry.gPoint(2.0d, 5.0d, 0.0d);
            line1.EndPoint = new VectorDraw.Geometry.gPoint(2.0d, 19.0d, 0.0d);
            doc.ActionLayout.Entities.AddItem(line1);

            int cnt = -7;

            for (int i = 0; i <= 14; i++)
            {
                vdLine line3 = new vdLine();
                line3.SetUnRegisterDocument(doc);
                line3.setDocumentDefaults();

                line3.StartPoint = new VectorDraw.Geometry.gPoint(2.0d, (i + 5.0d), 0.0d);
                line3.EndPoint = new VectorDraw.Geometry.gPoint(1.75d, (i + 5.0d), 0.0d);
                doc.ActionLayout.Entities.AddItem(line3);

                vdText vTxt = new vdText();
                vTxt.SetUnRegisterDocument(doc);
                vTxt.setDocumentDefaults();
                //if (i < 8)
                //{
                //    cnt--;
                //}
                //else
                //{
                //    cnt++;
                //}
                vTxt.InsertionPoint = new VectorDraw.Geometry.gPoint(line3.EndPoint.x - 3.0d, line3.EndPoint.y - 0.15d);
                vTxt.TextString = (list[beamIndex].StartForceMoment.M3 * ((double)(i + 1) / 15.0d)).ToString("0.0000");
                //vTxt.TextString = cnt.ToString();

                vTxt.Height = 0.5d;
                doc.ActionLayout.Entities.AddItem(vTxt);
                cnt++;
            }


            vdLine line2 = new vdLine();
            line2.SetUnRegisterDocument(doc);
            line2.setDocumentDefaults();

            line2.StartPoint = new VectorDraw.Geometry.gPoint(32.0d, 5.0d, 0.0d);
            line2.EndPoint = new VectorDraw.Geometry.gPoint(32.0d, 19.0d, 0.0d);
            doc.ActionLayout.Entities.AddItem(line2);


            cnt = -7;
            for (int i = 0; i <= 14; i++)
            {
                vdLine line3 = new vdLine();
                line3.SetUnRegisterDocument(doc);
                line3.setDocumentDefaults();

                line3.StartPoint = new VectorDraw.Geometry.gPoint(32.0d, (i + 5.0d), 0.0d);
                line3.EndPoint = new VectorDraw.Geometry.gPoint(32.25d, (i + 5.0d), 0.0d);
                doc.ActionLayout.Entities.AddItem(line3);


                vdText vTxt = new vdText();
                vTxt.SetUnRegisterDocument(doc);
                vTxt.setDocumentDefaults();
                vTxt.InsertionPoint = new VectorDraw.Geometry.gPoint(line3.EndPoint.x + 0.2d, line3.EndPoint.y - 0.15d);
                //vTxt.InsertionPoint = line3.EndPoint;

                //if (i < 8)
                //{
                //    cnt--;
                //}
                //else if (i > 8)
                //{
                //    cnt++;
                //}
                vTxt.TextString = (list[beamIndex].StartForceMoment.M3 * ((double)(i + 1) / 15.0d)).ToString("0.0000");

                //vTxt.TextString = cnt.ToString();
                vTxt.Height = 0.5d;
                doc.ActionLayout.Entities.AddItem(vTxt);
                cnt++;
            }

            vdLine midLine = new vdLine();
            midLine.SetUnRegisterDocument(doc);
            midLine.setDocumentDefaults();

            midLine.StartPoint = (line1.StartPoint + line1.EndPoint) / 2;
            midLine.EndPoint = (line2.StartPoint + line2.EndPoint) / 2;
            //midLine.EndPoint = new VectorDraw.Geometry.gPoint(32.25d, (i + 1.0d), 0.0d);
            doc.ActionLayout.Entities.AddItem(midLine);



            vdRect rect = new vdRect();
            rect.SetUnRegisterDocument(doc);
            rect.setDocumentDefaults();

            rect.InsertionPoint = new VectorDraw.Geometry.gPoint(-2, 4.0d, 0);
            rect.Width = 38.50d;
            rect.Height = 16.0d;
            doc.ActionLayout.Entities.AddItem(rect);

            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);

            doc.RenderMode = VectorDraw.Render.vdRender.Mode.Wire2d;
            doc.Redraw(true);
        }

        public enum eForce
        {
            R1 = 0,
            R2 = 1,
            R3 = 2,
            M1 = 3,
            M2 = 4,
            M3 = 5,
        }
    }
}
