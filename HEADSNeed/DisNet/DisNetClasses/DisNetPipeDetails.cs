using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Drawing;

using VectorDraw.Serialize;
using VectorDraw.Professional;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Professional.Dialogs;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.Constants;
using VectorDraw.Geometry;
using VectorDraw.Render;
using VectorDraw.Professional.Actions;
using VectorDraw.Actions;


namespace HEADSNeed.DisNet.DisNetClasses
{
    public class DisNetPipeDetails
    {
        int pipeNo;
        double length, diameter, discharge;
        gPoint pts;
        public DisNetPipeDetails()
        {
            pipeNo = 0;
            length = diameter = discharge = 0.0d;
            pts = new gPoint();
        }
        public int PipeNo
        {
            get
            {
                return pipeNo;
            }
            set
            {
                pipeNo = value;
            }
        }
        public double Length
        {
            get
            {
                return length;
            }
            set
            {
                length = value;
            }
        }
        public double Diameter
        {
            get
            {
                return diameter;
            }
            set
            {
                diameter = value;
            }
        }
        public double Discharge
        {
            get
            {
                return discharge;
            }
            set
            {
                discharge = value;
            }
        }
        public gPoint Point
        {
            get
            {
                return pts;
            }
            set
            {
                pts = value;
            }
        }

        public void SetPipeNo(string data)
        {
            //Pipe = 1
            //Length = 195.185 m.
            //Diameter = 0.15 m.
            //Discharge = 0.0228 m3/s.
            //POINTS = 54020.713,50675.57,99.228

            //Pipe = 2

            pipeNo = int.Parse(data.Split(new char[] { '=' })[1].Trim().TrimEnd().TrimStart());

        }

        public void SetPipeLength(string data)
        {

            //Pipe = 1
            //Length = 195.185 m.
            //Diameter = 0.15 m.
            //Discharge = 0.0228 m3/s.
            //POINTS = 54020.713,50675.57,99.228

            //Length = 359.264 m.

            length = double.Parse(data.Split(new char[] { ' ' })[2]);
        }
        public void SetPipeDiameter(string data)
        {
            //Pipe = 1
            //Length = 195.185 m.
            //Diameter = 0.15 m.
            //Discharge = 0.0228 m3/s.
            //POINTS = 54020.713,50675.57,99.228

            //Diameter = 0.15 m.

            diameter = double.Parse(data.Split(new char[] { ' ' })[2]);
        }
        public void SetPipeDischarge(string data)
        {
            //Pipe = 1
            //Length = 195.185 m.
            //Diameter = 0.15 m.
            //Discharge = 0.0228 m3/s.
            //POINTS = 54020.713,50675.57,99.228

            //Discharge = 0.0090 m3/s.

            discharge = double.Parse(data.Split(new char[] { ' ' })[2]);

        }
        public void SetPipePoint(string data)
        {
            //Pipe = 1
            //Length = 195.185 m.
            //Diameter = 0.15 m.
            //Discharge = 0.0228 m3/s.
            //POINTS = 54020.713,50675.57,99.228

            //POINTS = 54020.713,50675.57,99.228

            data = data.Split(new char[] { '=' })[1].Trim().TrimEnd().TrimStart();

            pts.x = double.Parse(data.Split(new char[] { ',' })[0]);
            pts.y = double.Parse(data.Split(new char[] { ',' })[1]);
            pts.z = double.Parse(data.Split(new char[] { ',' })[2]);

        }
    }
    public class DisNetPipeDetailsCollection : IList<DisNetPipeDetails>
    {
        List<DisNetPipeDetails> list = null;
        vdLayer pipeDetailsLay = new vdLayer();
        public DisNetPipeDetailsCollection()
        {
            list = new List<DisNetPipeDetails>();
        }

        #region IList<DisNetPipeDetails> Members

        public int IndexOf(DisNetPipeDetails item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].PipeNo == item.PipeNo)
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, DisNetPipeDetails item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public DisNetPipeDetails this[int index]
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

        #region ICollection<DisNetPipeDetails> Members

        public void Add(DisNetPipeDetails item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(DisNetPipeDetails item)
        {
            return (IndexOf(item) != -1 ? true : false);
        }

        public void CopyTo(DisNetPipeDetails[] array, int arrayIndex)
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

        public bool Remove(DisNetPipeDetails item)
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

        #region IEnumerable<DisNetPipeDetails> Members

        public IEnumerator<DisNetPipeDetails> GetEnumerator()
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
        public void DrawPipeDetails(vdDocument doc)
        {
            pipeDetailsLay = new vdLayer();
            pipeDetailsLay.Name = "PipeData";
            pipeDetailsLay.SetUnRegisterDocument(doc);
            pipeDetailsLay.setDocumentDefaults();
            doc.Layers.AddItem(pipeDetailsLay);
            pipeDetailsLay.PenColor = new vdColor(Color.Magenta);
            foreach (DisNetPipeDetails pdtls in list)
            {
                DrawPipeDetails(doc, pdtls);
            }
            doc.Redraw(true);
        }
        public void DrawPipeDetails(vdDocument doc, DisNetPipeDetails pipeDetails)
        {


            //Pipe = 1
            //Length = 195.185 m.
            //Diameter = 0.15 m.
            //Discharge = 0.0228 m3/s.
            //POINTS = 54020.713,50675.57,99.228

            string txtStr = "";
            txtStr = "Pipe = " + pipeDetails.PipeNo.ToString() + "\n\r" +
                "Length = " + pipeDetails.Length.ToString("0.000") + " m." + "\n\r" +
                "Diameter = " + pipeDetails.Diameter.ToString("0.00") + " m3/s." + "\n\r" +
                "Discharge = " + pipeDetails.Discharge.ToString("0.0000") + " m." + "\n\r";

            txtStr = "Pipe No: " + pipeDetails.PipeNo.ToString();

            vdText txt = new vdText();

            txt.Style = new vdTextstyle();
            txt.TextString = "Pipe No: " + pipeDetails.PipeNo.ToString();

            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.Height = 5.0d;
            txt.InsertionPoint = new gPoint(pipeDetails.Point.x, pipeDetails.Point.y, pipeDetails.Point.z + 15.0d);
            //txt.PenColor = new vdColor(Color.Red);
            txt.Layer = doc.Layers.FindName("PipeData");

            doc.ActiveLayOut.Entities.AddItem(txt);
        }
    }
}
