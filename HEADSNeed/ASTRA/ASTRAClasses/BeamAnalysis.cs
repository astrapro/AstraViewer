using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using VectorDraw.Geometry;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using MovingLoadAnalysis;

namespace HEADSNeed.ASTRA.ASTRAClasses
{

    public class MemberAnalysis : StructureMemberAnalysis
    {
        public MemberAnalysis(string ana_file)
            : base(ana_file)
        {

        }

        public List<AnalysisData>  Analysis_Data
        {
            get
            {
                return this.list_analysis;
            }
        }

        public List<string> Data
        {
            get
            {
                return new List<string>();
            }
        }
    }
    public class BeamAnalysis
    {
        #region private variables
        int iSectionNo, iSpanNo;
        double dDistance, dShear, dMoment;
        #endregion

        #region ctor
        public BeamAnalysis(int spanNo, int sectionNo, double distance, double shear, double moment)
        {
            iSpanNo = spanNo;
            iSectionNo = sectionNo;
            dDistance = distance;
            dShear = shear;
            dMoment = moment;
        }
        public BeamAnalysis()
        {
            iSectionNo = 0;
            iSpanNo = 0;
            dDistance = 0.0d;
            dShear = 0.0d;
            dMoment = 0.0d;
        }
        #endregion

        #region Public Property
        public int SectionNo
        {
            get
            {
                return iSectionNo;
            }
            set
            {
            }
        }
        public int SpanNo
        {
            get
            {
                return iSpanNo;
            }
            set
            {
                iSpanNo = value;
            }
        }
        public double Distance
        {
            get
            {
                return dDistance;
            }
            set
            {
                dDistance = value;
            }
        }
        public double Shear
        {
            get
            {
                return dShear;
            }
            set
            {
                dShear = value;
            }
        }
        public double Moment
        {
            get
            {
                return dMoment;
            }
            set
            {
                dMoment = value;
            }
        }
        #endregion
    }

    public class BeamAnalysisCollection : IList<BeamAnalysis>
    {
        #region private variables
        List<BeamAnalysis> list = null;
        #endregion

        #region ctor
        public BeamAnalysisCollection()
        {
            list = new List<BeamAnalysis>();
        }
        #endregion

        #region IList<BeamAnalysis> Members

        public int IndexOf(BeamAnalysis item)
        {
            return IndexOf(item.SpanNo, item.SectionNo);
        }

        public int IndexOf(int SpanNo, int SectionNo)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if ((list[i].SpanNo == SpanNo) && (list[i].SectionNo == SectionNo))
                {
                    return i;
                }
            }
            return -1;
        }
        public void Insert(int index, BeamAnalysis item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public BeamAnalysis this[int index]
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

        #region ICollection<BeamAnalysis> Members

        public void Add(BeamAnalysis item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(BeamAnalysis item)
        {
            return (IndexOf(item) != -1 ? true : false);
        }

        public void CopyTo(BeamAnalysis[] array, int arrayIndex)
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

        public bool Remove(BeamAnalysis item)
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

        #region IEnumerable<BeamAnalysis> Members

        public IEnumerator<BeamAnalysis> GetEnumerator()
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

        public void ReadFromFile(string fileName)
        {
            if (!File.Exists(fileName)) return;

            string kStr = "";
            int SpanNo = 0;
            bool work = false;
            StreamReader sr = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
            try
            {
                while (sr.EndOfStream == false)
                {
                    kStr = sr.ReadLine().Trim().TrimEnd().TrimStart();

                    if (kStr.Contains("SPAN NO."))
                    {
                        MyStrings mL = new MyStrings(kStr, ' ');
                        try
                        {
                            SpanNo = mL.GetInt(2);
                            work = true;
                        }
                        catch (Exception exx)
                        {
                        }
                    }
                    if (work)
                    {
                        AddToList(kStr, SpanNo);
                    }
                }
            }
            catch (Exception ex) { }
            finally
            {
                sr.Close();
            }
        }
        public void AddToList(string str, int SpanNo)
        {
            str = str.Replace('\t', ' ');

            while (str.Contains("  "))
            {
                str = str.Replace("  ", " ");
            }
            MyStrings mList = new MyStrings(str, ' ');

            /*
             *  
    SPAN NO. 1
         SECTION NO. DISTANCE(m)                    SHEAR                         MOMENT
              1         0.00                       131.10                           0.00
              2         0.60                       100.50                          69.48
              3         1.20                        69.90                         120.60
             
             * 
             */

            try
            {
                BeamAnalysis beamAna = new BeamAnalysis();
                beamAna.SpanNo = SpanNo;
                beamAna.SectionNo = mList.GetInt(0);
                beamAna.Distance = mList.GetDouble(1);
                beamAna.Shear = mList.GetDouble(2);
                beamAna.Moment = mList.GetDouble(3);
                Add(beamAna);
            }
            catch (Exception exx)
            {
            }

        }

        public void Draw_StructureGraph(vdDocument document, int SpanNo, int LoadCase, bool IsShear)
        {
            //Jay Shree Krishna
            //Jay Shree Ganesh
            document.ActiveLayOut.Entities.RemoveAll();

            #region MainLIne
            vdLine mainLine = new vdLine();
            mainLine.SetUnRegisterDocument(document);
            mainLine.setDocumentDefaults();

            mainLine.StartPoint = new gPoint(10.0d, 10.0d);
            mainLine.EndPoint = new gPoint(30.0d, 10.0d);

            document.ActiveLayOut.Entities.AddItem(mainLine);
            #endregion

            #region Side Line 1
            vdLine sideLine1 = new vdLine();
            sideLine1.SetUnRegisterDocument(document);
            sideLine1.setDocumentDefaults();
            sideLine1.StartPoint = new gPoint(10, 5);
            sideLine1.EndPoint = new gPoint(10, 15);

            document.ActiveLayOut.Entities.AddItem(sideLine1);

            #endregion

            #region Side Line 2
            vdLine sideLine2 = new vdLine();
            sideLine2.SetUnRegisterDocument(document);
            sideLine2.setDocumentDefaults();
            sideLine2.StartPoint = new gPoint(30, 5);
            sideLine2.EndPoint = new gPoint(30, 15);
            document.ActiveLayOut.Entities.AddItem(sideLine2);
            #endregion

            #region Draw PolyLine

            vdPolyline pLine = new vdPolyline();
            pLine.SetUnRegisterDocument(document);
            pLine.setDocumentDefaults();
            //pLine.VertexList.Add(mainLine.StartPoint);

            double st, en, highest;

            st = en = highest = 0.0d;
            if (IsShear)
            {
                highest = MaxShear(SpanNo);
            }
            else
            {
                highest = MaxMoment(SpanNo);

            }

            
            double xIncr = 0.0d;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].SpanNo == SpanNo)
                {
                    if (IsShear)
                    {
                        st = list[i].Shear;
                    }
                    else
                        st = list[i].Moment;


                    gPoint gp = new gPoint();

                    gp.x = mainLine.StartPoint.x + xIncr;
                    xIncr += 2.0d;
                    st = -st;
                    if (st > 0)
                    {
                        gp.y = mainLine.StartPoint.y + (sideLine1.EndPoint.y - mainLine.StartPoint.y) * (st / highest);
                    }
                    else
                    {
                        gp.y = mainLine.StartPoint.y - (sideLine1.StartPoint.y - mainLine.StartPoint.y) * (st / highest);
                    }
                    pLine.VertexList.Add(gp);


                    vdLine vLine = new vdLine();
                    vLine.SetUnRegisterDocument(document);
                    vLine.setDocumentDefaults();
                    vLine.StartPoint = gp;
                    vLine.EndPoint = new gPoint(gp.x, mainLine.StartPoint.y);
                    document.ActiveLayOut.Entities.AddItem(vLine);

                    vdText yValue = new vdText();
                    yValue.SetUnRegisterDocument(document);
                    yValue.setDocumentDefaults();
                    yValue.InsertionPoint = vLine.EndPoint;
                    yValue.TextString = st.ToString();
                    if (st > 0)
                    {

                        yValue.Rotation = 270.0d * Math.PI / 180.0d;
                        yValue.InsertionPoint.x -= 0.25d;
                        yValue.InsertionPoint.y -= 0.5d;
                    }
                    else
                    {
                        yValue.Rotation = 90.0d * Math.PI / 180.0d;
                        yValue.InsertionPoint.x += 0.25d;
                        yValue.InsertionPoint.y += 0.5d;
                    }
                    yValue.Height = 1.0d;

                    document.ActiveLayOut.Entities.AddItem(yValue);

                }
            }

            document.ShowUCSAxis = false;
            document.ActiveLayOut.Entities.AddItem(pLine);

            //if (st > 0)
            //{
            //    beamLine.StartPoint.x = midLine.StartPoint.x + (line1.EndPoint.x - midLine.StartPoint.x) * (st / highest);
            //    beamLine.StartPoint.y = midLine.StartPoint.y + (line1.EndPoint.y - midLine.StartPoint.y) * (st / highest);
            //    beamLine.StartPoint.z = midLine.StartPoint.z + (line1.EndPoint.z - midLine.StartPoint.z) * (st / highest);
            //}
            //else
            //{
            //    beamLine.StartPoint.x = midLine.StartPoint.x - (line1.StartPoint.x - midLine.StartPoint.x) * (st / highest);
            //    beamLine.StartPoint.y = midLine.StartPoint.y - (line1.StartPoint.y - midLine.StartPoint.y) * (st / highest);
            //    beamLine.StartPoint.z = midLine.StartPoint.z - (line1.StartPoint.z - midLine.StartPoint.z) * (st / highest);
            //}
            //if (en > 0)
            //{
            //    beamLine.EndPoint.x = midLine.EndPoint.x + (line2.EndPoint.x - midLine.EndPoint.x) * (en / highest);
            //    beamLine.EndPoint.y = midLine.EndPoint.y + (line2.EndPoint.y - midLine.EndPoint.y) * (en / highest);
            //    beamLine.EndPoint.z = midLine.EndPoint.z + (line2.EndPoint.z - midLine.EndPoint.z) * (en / highest);
            //}
            //else
            //{
            //    beamLine.EndPoint.x = midLine.EndPoint.x - (line2.StartPoint.x - midLine.EndPoint.x) * (en / highest);
            //    beamLine.EndPoint.y = midLine.EndPoint.y - (line2.StartPoint.y - midLine.EndPoint.y) * (en / highest);
            //    beamLine.EndPoint.z = midLine.EndPoint.z - (line2.StartPoint.z - midLine.EndPoint.z) * (en / highest);
            //}

            #endregion

            #region Draw Rectangle
            vdRect rect = new vdRect();
            rect.SetUnRegisterDocument(document);
            rect.setDocumentDefaults();
            rect.InsertionPoint = new gPoint(sideLine1.StartPoint.x - 2, sideLine1.StartPoint.y - 2);
            rect.Width = 23.0d;
            rect.Height = 13.0d;

            document.ActiveLayOut.Entities.AddItem(rect);
            #endregion

            document.Redraw(true);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(document);
        }
        #region Public Method
        public double MaxShear(int SpanNo)
        {
            double dd = 0.0d;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].SpanNo == SpanNo)
                {
                    if (Math.Abs(list[i].Shear) > dd)
                    {
                        dd = Math.Abs(list[i].Shear);
                    }
                }
            }
            return dd;
        }
        public double MaxMoment(int SpanNo)
        {
            double dd = 0.0d;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].SpanNo == SpanNo)
                {
                    if (Math.Abs(list[i].Moment) > dd)
                    {
                        dd = Math.Abs(list[i].Moment);
                    }
                }
            }
            return dd;
        }
        #endregion
        
    }



}

   

