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
    public class DisNetNode
    {
        int nodeNo;
        double elevation,head;
        gPoint pts;
        bool bIsPump = false;
        public DisNetNode()
        {
            nodeNo = 0;
            elevation = 0.0d;
            head = 0.0d;
            pts = new gPoint();
            bIsPump = false;
        }
        public int NodeNo
        {
            get
            {
                return nodeNo;
            }
            set
            {
                nodeNo = value;
            }
        }
        public double Elevation
        {
            get
            {
                return elevation;
            }
            set
            {
                elevation = value;
            }
        }
        public double Head
        {
            get
            {
                return head;
            }
            set
            {
                head = value;
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
        public bool IsPump
        {
            get
            {
                return bIsPump;
            }
            set
            {
                bIsPump = value;
            }
        }

        public void SetNodeNo(string data)
        {
            //Node = 1
            //Elevation = 99.228 m.
            //Head = 21.000 m.
            //POINTS = 54005.587,50772.821,100.228


            //Node = 2
            nodeNo = int.Parse(data.Split(new char[] { '=' })[1].Trim().TrimEnd().TrimStart());


        }
        public void SetElevation(string data)
        {
            //Node = 1
            //Elevation = 99.228 m.
            //Head = 21.000 m.
            //POINTS = 54005.587,50772.821,100.228


            //Node = 2
            elevation = double.Parse(data.Split(new char[] { ' ' })[2]);


        }
        public void SetHead(string data)
        {
            //Node = 1
            //Elevation = 99.228 m.
            //Head = 21.000 m.
            //POINTS = 54005.587,50772.821,100.228


            //Head = 21.000 m.
            head = double.Parse(data.Split(new char[] { ' ' })[2]);
        }
        public void SetNodePoint(string data)
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
    public class DisNetNodeCollection : IList<DisNetNode>
    {
        List<DisNetNode> list = null;
        vdLayer nodalDataLay = new vdLayer();
        public DisNetNodeCollection()
        {
            list = new List<DisNetNode>();
        }

        #region IList<DisNetNode> Members

        public int IndexOf(DisNetNode item)
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

        public void Insert(int index, DisNetNode item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public DisNetNode this[int index]
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

        #region ICollection<DisNetNode> Members

        public void Add(DisNetNode item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(DisNetNode item)
        {
            return (IndexOf(item) != -1 ? true : false);
        }

        public void CopyTo(DisNetNode[] array, int arrayIndex)
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

        public bool Remove(DisNetNode item)
        {
            int indx = IndexOf(item);
            if (indx != -1)
            {
                RemoveAt(indx);
                return true;
            }
            return false;
        }

        #endregion

        #region IEnumerable<DisNetNode> Members

        public IEnumerator<DisNetNode> GetEnumerator()
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

        vdLayer pumpLay =  new vdLayer();
        public void DrawNodes(vdDocument doc)
        {
            //nodalDataLay.Name = "NodalData";
            //nodalDataLay.SetUnRegisterDocument(doc);
            //nodalDataLay.setDocumentDefaults();
            //doc.Layers.AddItem(nodalDataLay);
            //nodalDataLay.PenColor = new vdColor(Color.Red);
            vdLayer pumpLay = new vdLayer();
            pumpLay.SetUnRegisterDocument(doc);
            pumpLay.setDocumentDefaults();

            pumpLay.PenColor = new vdColor(Color.LightSeaGreen);
            foreach (DisNetNode nd in list)
            {
                DrawNodes(doc, nd);
            }
            doc.Redraw(true);
        }
        public void DrawNodes(vdDocument doc,DisNetNode nd )
        {
            
            //Node = 1
            //Elevation = 99.228 m.
            //Head = 21.000 m.
            //POINTS = 54005.587,50772.821,100.228

            string txtStr = "";
            txtStr =  "Node = " + nd.NodeNo.ToString() + "\n\r" +
                "Elevation = " + nd.Elevation.ToString("0.000")+ " m." + "\n\r" +
                "Head = " + nd.Head.ToString("0.000")+ " m." + "\n\r" ;

            txtStr = "Node No: " + nd.NodeNo.ToString();
            if (nd.IsPump)
            {
                txtStr += "  [ PUMP INPUT ]";
            }
            vdText txt = new vdText();
            txt.TextString = txtStr;

            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.Height = 9.0d;
            txt.InsertionPoint = new gPoint(nd.Point.x,nd.Point.y,nd.Point.z + 15.0d);
            txt.PenColor = new vdColor(Color.White);
            txt.Layer = doc.Layers.FindName("NodalData");
            doc.ActiveLayOut.Entities.AddItem(txt);

            //if (nd.IsPump)
            //{

            //    vdText pump = new vdText();
            //    pump.TextString = " [ PUMP INPUT ]";

            //    pump.SetUnRegisterDocument(doc);
            //    pump.setDocumentDefaults();
            //    pump.Height = 4.0d;
            //    //pump.InsertionPoint = new gPoint(nd.Point.x + txt.WidthFactor, nd.Point.y, nd.Point.z + 15.0d);
            //    pump.InsertionPoint = new gPoint(nd.Point.x + txt.WidthFactor, nd.Point.y - txt.Height + 5.0d , nd.Point.z + 15.0d);
            //    pump.PenColor = new vdColor(Color.LightSkyBlue);
            //    pump.Layer = doc.Layers.FindName("NodalData");
            //    doc.ActiveLayOut.Entities.AddItem(pump);

            //}

            
        }

    }
}
