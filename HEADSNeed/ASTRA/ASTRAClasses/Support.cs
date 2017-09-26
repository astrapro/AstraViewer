using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ComponentModel;
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
using HEADSNeed.ASTRA.ASTRADrawingTools;

namespace HEADSNeed.ASTRA.ASTRAClasses
{
    public class Support
    {
        //private int iNo;
        SupportOption soOption;

        private JointCoordinate jcNode = null;
        public Support()
        {
            jcNode = new JointCoordinate();
            soOption = SupportOption.FIXED;

            FX = true;
            FY = true;
            FZ = true;
            MX = true;
            MY = true;
            MZ = true;
        }
        public JointCoordinate Node
        {
            get
            {
                return jcNode;
            }
            set
            {
                jcNode = value;
            }
        }
        public SupportOption Option
        {
            get
            {
                return soOption;
            }
            set
            {
                soOption = value;
            }
        }
        public enum SupportOption
        {
            FIXED = 0,
            PINNED = 1,
        }
        public bool FX { get; set; }
        public bool FY { get; set; }
        public bool FZ { get; set; }
        public bool MX { get; set; }
        public bool MY { get; set; }
        public bool MZ { get; set; }

        public static Support ParseAST(string data)
        {
            // Example

            // N005 element#, dof[0], dof[1], dof[2], dof[3], dof[4], dof[5]
            // N005     1     1     1     1     1     1     1
            // N005     2     1     1     1     0     0     0

            string temp = data.Trim().TrimEnd().TrimStart();
            temp = temp.Replace('\t', ' ');

            while (temp.IndexOf("  ") != -1)
            {
                temp = temp.Replace("  ", " ");
            }

            string[] values = temp.Split(new char[] { ' ' });
            
            

            Support obj = new Support();
            if (values.Length != 8) throw new Exception("String Data is not correct format.");

            if (values[2] == "1" && values[3] == "1" && values[4] == "1" &&
                values[5] == "1" && values[6] == "1" && values[7] == "1")
            {
                obj.Option = SupportOption.FIXED;
            }
            else
            {
                obj.Option = SupportOption.PINNED;
            }
            obj.Node.NodeNo = int.Parse(values[1]);
            return obj;
        }

       

    }

    public class SupportCollection : IList<Support>
    {
        List<Support> list;
        vdLayer supportPinnedLay = new vdLayer();
        vdLayer supportFixedLay = new vdLayer();

        public List<string> ASTRA_Data { get; set; }
        public SupportCollection()
        {
            list = new List<Support>();
            ASTRA_Data = new List<string>();
            
        }

        #region IList<Support> Members

        public int IndexOf(Support item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Node.NodeNo == item.Node.NodeNo)
                    return i;
            }
            return -1;
        }
        public int IndexOf(int NodeNo)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Node.NodeNo == NodeNo)
                    return i;
            }
            return -1;
        }

        public void Insert(int index, Support item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public Support this[int index]
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

        #region ICollection<Support> Members

        public void Add(Support item)
        {
            list.Add(item);
        }
        public void Add(string  itemData)
        {
            AddTXT(itemData);

        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(Support item)
        {
            return ((IndexOf(item) == -1) ? false : true);
        }

        public void CopyTo(Support[] array, int arrayIndex)
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

        public bool Remove(Support item)
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

        #region IEnumerable<Support> Members

        public IEnumerator<Support> GetEnumerator()
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

        public void AddTXT(string data)
        {
            //1 2 4 5 104 TO 108 161 TO 165 538 TO 544 FIXED

            string orgStr = data.Trim().TrimEnd().TrimStart();


            string[] values = orgStr.Split(new char[] { ';' });
            List<string> lstStr;

            Support spt;
            foreach (string st in values)
            {

                //1 2 4 5 104 TO 108 161 TO 165 538 TO 544 FIXED
                int indx = -1;
                try
                {

                    List<int> jnts = MyStrings.Get_Array_Intiger(st);
                    MyStrings mLIst = new MyStrings(st, ' ');

                    for (int i = 0; i < jnts.Count; i++)
                    {
                        spt = new Support();
                        spt.Node.NodeNo = jnts[i];
                        spt.Option = st.Contains("FIXED") ? Support.SupportOption.FIXED : Support.SupportOption.PINNED;
                        Add(spt);
                    }
                }
                catch (Exception exx)
                {
                }
            }
        }
        public void CopyFromCoordinateCollection(JointCoordinateCollection jcCol)
        {
            int indx = 0;
            for (int i = 0; i < list.Count; i++)
            {
                indx = jcCol.IndexOf(list[i].Node);
                if (indx != -1)
                {
                    list[i].Node = jcCol[indx];
                }
            }
        }

        public void DrawSupport_Dump(vdDocument doc)
        {

            supportFixedLay.Name = "SupportFixed";
            supportPinnedLay.Name = "SupportPinned";
            if (doc.Layers.FindName("SupportFixed") == null)
            {
                supportFixedLay = new vdLayer();
                supportFixedLay.Name = "SupportFixed";
                supportFixedLay.SetUnRegisterDocument(doc);
                supportFixedLay.setDocumentDefaults();
                doc.Layers.AddItem(supportFixedLay);
            }
            if (doc.Layers.FindName("SupportPinned") == null)
            {
                supportPinnedLay = new vdLayer();
                supportPinnedLay.Name = "SupportPinned";
                supportPinnedLay.SetUnRegisterDocument(doc);
                supportPinnedLay.setDocumentDefaults();
                doc.Layers.AddItem(supportPinnedLay);
            }
            try
            {
                doc.Layers.AddItem(supportFixedLay);
                doc.Layers.AddItem(supportPinnedLay);
            }
            catch (Exception ex) { }
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Option == Support.SupportOption.PINNED)
                    DrawSupportPinned(doc, list[i].Node.Point);
                if (list[i].Option == Support.SupportOption.FIXED)
                    DrawSupportFixed(doc, list[i].Node.Point);
            }
        }
        public void DrawSupport(vdDocument doc)
        {
            for (int i = 0; i < doc.ActionLayout.Entities.Count; i++)
            {
                if (doc.ActionLayout.Entities[i].Layer.Name.StartsWith("Support"))
                {
                    doc.ActionLayout.Entities[i].Deleted = true;
                    doc.ActionLayout.Entities[i].Update();
                }
            }
            doc.Redraw(true);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Option == Support.SupportOption.PINNED)
                    DrawSupportPinned(doc, list[i].Node.Point);
                if (list[i].Option == Support.SupportOption.FIXED)
                    DrawSupportFixed(doc, list[i].Node.Point);
            }
        }

        public void DrawSupportPinned(vdDocument doc, gPoint pt)
        {
            supportPinnedLay.Name = "SupportPinned";
            if (doc.Layers.FindName("SupportPinned") == null)
            {
                supportPinnedLay = new vdLayer();
                supportPinnedLay.Name = "SupportPinned";
                supportPinnedLay.SetUnRegisterDocument(doc);
                supportPinnedLay.setDocumentDefaults();
                doc.Layers.AddItem(supportPinnedLay);
            }
            else
            {
                supportPinnedLay = doc.Layers.FindName("SupportPinned");
            }
            ASTRASupportPinned asFix = new ASTRASupportPinned();
            asFix.SetUnRegisterDocument(doc);
            asFix.setDocumentDefaults();

            asFix.Origin = pt;
            asFix.Radius = 0.1d;
            asFix.Layer = supportPinnedLay;
            doc.ActiveLayOut.Entities.AddItem(asFix);
        }
        public void DrawSupportFixed(vdDocument doc, gPoint pt)
        {
            supportFixedLay.Name = "SupportFixed";
            if (doc.Layers.FindName("SupportFixed") == null)
            {
                supportFixedLay = new vdLayer();
                supportFixedLay.Name = "SupportFixed";
                supportFixedLay.SetUnRegisterDocument(doc);
                supportFixedLay.setDocumentDefaults();
                doc.Layers.AddItem(supportFixedLay);
            }
            else
            {
                supportFixedLay = doc.Layers.FindName("SupportFixed");
            }

            ASTRASupportFixed asFix = new ASTRASupportFixed();
            asFix.SetUnRegisterDocument(doc);
            asFix.setDocumentDefaults();

            asFix.Origin = pt;
            asFix.Radius = 0.1d;
            asFix.Layer = supportFixedLay;
            doc.ActiveLayOut.Entities.AddItem(asFix);
        }

    }
}
