using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ComponentModel;

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
    public class DisNetLoop
    {
        int loopNo = 0;
        gPoint pts;
        public DisNetLoop()
        {
            loopNo = 0;
            pts = new gPoint();
        }
        public int LoopNo
        {
            get
            {
                return loopNo;
            }
            set
            {
                loopNo = value;
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

        public void SetLoopNo(string data)
        {
            //Loop = 4
            //POINTS = 54297.8457125,49966.6582175,54297.8457125

            loopNo = int.Parse(data.Split(new char[] { '=' })[1].Trim().TrimEnd().TrimStart());


        }
        public void SetLoopPoint(string data)
        {

            //Loop = 4
            //POINTS = 54297.8457125,49966.6582175,54297.8457125

            data = data.Split(new char[] { '=' })[1].Trim().TrimEnd().TrimStart();

            pts.x = double.Parse(data.Split(new char[] { ',' })[0]);
            pts.y = double.Parse(data.Split(new char[] { ',' })[1]);
            pts.z = double.Parse(data.Split(new char[] { ',' })[2]);

        }

    }

    public class DisNetLoopCollection : IList<DisNetLoop>
    {
        List<DisNetLoop> list = null;
        vdLayer loopLay = new vdLayer();
        public DisNetLoopCollection()
        {
            list = new List<DisNetLoop>();
        }

        #region IList<DisNetLoop> Members

        public int IndexOf(DisNetLoop item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].LoopNo == item.LoopNo) return i;
            }
            return -1;
        }

        public void Insert(int index, DisNetLoop item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public DisNetLoop this[int index]
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

        #region ICollection<DisNetLoop> Members

        public void Add(DisNetLoop item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(DisNetLoop item)
        {
            return (IndexOf(item) != -1 ? true : false);
        }

        public void CopyTo(DisNetLoop[] array, int arrayIndex)
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

        public bool Remove(DisNetLoop item)
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

        #region IEnumerable<DisNetLoop> Members

        public IEnumerator<DisNetLoop> GetEnumerator()
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

        public void DrawLoops(vdDocument doc)
        {
            loopLay.Name = "Loops";
            loopLay.SetUnRegisterDocument(doc);
            loopLay.setDocumentDefaults();
            doc.Layers.AddItem(loopLay);
            loopLay.PenColor = new vdColor(Color.LightCoral);
            foreach (DisNetLoop lp in list)
            {
                DrawLoops(doc, lp);
            }
            doc.Redraw(true);
        }

        public void DrawLoops(vdDocument doc, DisNetLoop loop)
        {

            //Loop = 1
            //POINTS = 54098.236595,50363.406415,54098.236595

            string txtStr = "Loop No: " + loop.LoopNo.ToString();

            vdText txt = new vdText();
            txt.TextString = txtStr;

            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.Height = 15.0d;
            txt.InsertionPoint = loop.Point;
            //txt.PenColor = new vdColor(Color.Red);
            txt.Layer = loopLay;

            doc.ActiveLayOut.Entities.AddItem(txt);
        }
   
    }
}
