using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HEADSNeed.ASTRA.ASTRAClasses;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdFigures;


namespace HEADSNeed.ASTRA.ASTRAForms
{
    public partial class frmMemberGrid : Form
    {
        ASTRADoc astDoc = null;
        MemberGridCollection mGridCollection = null;
        vdDocument doc = null;

        public frmMemberGrid(ASTRADoc astDoc,vdDocument doc)
        {
            InitializeComponent();
            this.astDoc = astDoc;
            mGridCollection = new MemberGridCollection(astDoc);
            this.doc = doc;
        }

        private void frmMemberGrid_Load(object sender, EventArgs e)
        {
            mGridCollection.SetGridView(ref dgvMemberGrid);
        }
        public void SetGrid()
        {
            for (int i = 0; i < astDoc.Members.Count; i++)
            {
                dgvMemberGrid.Rows.Add(astDoc.Members[i].MemberNo,
                    "BEAM", astDoc.Members[i].StartNode.NodeNo,
                    astDoc.Members[i].EndNode.NodeNo,"","",
                    astDoc.MemberProperties.IndexOf(astDoc.Members[i].MemberNo )                 );
            }
            for (int i = 0; i < astDoc.Elements.Count; i++)
            {
                dgvMemberGrid.Rows.Add(astDoc.Elements[i].ElementNo,
                    "PLATE", astDoc.Elements[i].Node1.NodeNo,
                    astDoc.Elements[i].Node2.NodeNo,
                    astDoc.Elements[i].Node3.NodeNo,
                    astDoc.Elements[i].Node4.NodeNo);
            }
        }
        private void dgvMemberGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //dgvMemberGrid.CurrentCell = dgvMemberGrid.A
                if (tssl_zoom.Text == "Zoom On")
                    astDoc.Members.ShowMember(e.RowIndex, doc);
            }
            catch (Exception ex) { }
        }
        private void frmMemberGrid_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                astDoc.Members.RemoveMemberLine(doc);
            }
            catch (Exception ex) { }
        }
        public void ShowMemberOnGrid(vdLine vLine)
        {
            //dgvMemberGrid.FirstDisplayedScrollingRowIndex = 6;

            //DataGridViewCell cell;
            //cell.ColumnIndex = 0;
            //cell.RowIndex = 2;
            
            for (int i = 0; i < astDoc.Members.Count; i++)
            {
                if (astDoc.Members[i].StartNode.Point == vLine.StartPoint &&
                    astDoc.Members[i].EndNode.Point == vLine.EndPoint)
                {

                    for (int j = 0; j < dgvMemberGrid.Rows.Count; j++)
                    {
                        int memNo = (int)dgvMemberGrid[0, j].Value;

                        if (memNo == astDoc.Members[i].MemberNo)
                        {
                            ClearSelect();
                            dgvMemberGrid.Rows[j].Selected = true;
                            dgvMemberGrid.FirstDisplayedScrollingRowIndex = j;
                            astDoc.Members.ShowMember(j, doc, 0.03d);
                            return;
                        }
                    }
                }
            }
        }
        public void ShowElementOnGrid(vd3DFace _3dFace)
        {
            int elmtNo = 0;
            string ss = "";

            for (int i = 0; i < astDoc.Elements.Count; i++)
            {
                if (astDoc.Elements[i].Node1.Point == _3dFace.VertexList[0] &&
                    astDoc.Elements[i].Node2.Point == _3dFace.VertexList[1] &&
                    astDoc.Elements[i].Node3.Point == _3dFace.VertexList[2] &&
                    astDoc.Elements[i].Node4.Point == _3dFace.VertexList[3])
                {
                    for (int j = 0; j < dgvMemberGrid.RowCount; j++)
                    {
                        elmtNo = (int)dgvMemberGrid[0,j].Value;
                        ss = dgvMemberGrid[1,j].Value.ToString().Trim().TrimEnd().TrimStart();
                        if (elmtNo == astDoc.Elements[i].ElementNo &&
                            ss == "PLATE")
                        {
                            ClearSelect();

                            dgvMemberGrid.Rows[j].Selected = true;
                            dgvMemberGrid.FirstDisplayedScrollingRowIndex = j;
                            return;
                        }

                    }
                }
            }

        }
        /// <summary>
        /// This method is used to Clear all Selected rows in DataGridView
        /// </summary>
        public void ClearSelect()
        {
            for (int i = 0; i < dgvMemberGrid.RowCount; i++)
            {
                dgvMemberGrid.Rows[i].Selected = false;
            }
        }
        private void dgvMemberGrid_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void btn_add_data_Click(object sender, EventArgs e)
        {

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tssl_zoom_Click(object sender, EventArgs e)
        {
            tssl_zoom.Text = (tssl_zoom.Text == "Zoom Off") ? "Zoom On" : "Zoom Off";
            tssl_zoom.BackColor = (tssl_zoom.Text == "Zoom Off") ? Color.Cyan : Color.Red;
        }
    }
    [Serializable]
    public class MemberGrid
    {

        #region Member Variables
        int iMemberNo;
        MemberType mbType;
        int node1, node2, node3, node4;
        double yd, zd, ix, iy, iz,   material;
        string pr = "", area = "", e = "";
        string den = "";
        #endregion

        public MemberGrid()
        {
            iMemberNo = 0;
            mbType = MemberType.BEAM;
            node1 = node2 = node3 = node4 = -1;    
            yd = zd = ix = iy = iz  =  material =  0.0d;
            pr = ""; den = area = e = "";
        }

        #region PUBLIC PROPERTY
        public int MemberNo
        {
            get
            {
                return iMemberNo;
            }
            set
            {
                iMemberNo = value;
            }
        }
        public MemberType Type
        {
            get
            {
                return mbType;
            }
            set
            {
                mbType = value;
            }
        }
        public int Node1
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
        public int Node2
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
        public int Node3
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
        public int Node4
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
        public double YD
        {
            get
            {
                return yd;
            }
            set
            {
                yd = value;
            }
        }
        public double ZD
        {
            get
            {
                return zd;
            }
            set
            {
                zd = value;
            }
        }
        public double IX
        {
            get
            {
                return ix;
            }
            set
            {
                ix = value;
            }
        }
        public double IY
        {
            get
            {
                return iy;
            }
            set
            {
                iy = value;
            }
        }
        public double IZ
        {
            get
            {
                return iz;
            }
            set
            {
                iz = value;
            }
        }
        public string Area
        {
            get
            {
                return area;
            }
            set
            {
                area = value;
            }
        }
        public string E
        {
            get
            {
                return e;
            }
            set
            {
                e = value;
            }
        }
        public string DEN
        {
            get
            {
                return den;
            }
            set
            {
                den = value;
            }
        }
        public double Material
        {
            get
            {
                return material;
            }
            set
            {
                material = value;
            }
        }
        public string PoissionRatio
        {
            get
            {
                return pr;
            }
            set
            {
                pr = value;
            }
        }
        #endregion

        #region Enumeration
        public enum MemberType
        {
            BEAM = 0,
            TRUSS = 1,
            PLATE = 2,
        }
        #endregion

    }
    [Serializable]
    public class MemberGridCollection : IList<MemberGrid>
    {
        List<MemberGrid> list = null;
        ASTRADoc astDoc = null;
        public MemberGridCollection(ASTRADoc astDoc)
        {
            list = new List<MemberGrid>();
            this.astDoc = astDoc;
            SetGrid();
        }
        public void SetGrid()
        {
            if (astDoc.MemberProperties.Count == 0)
            {
                for (int i = 0; i < astDoc.Members.Count; i++)
                {
                    if (astDoc.Members[i].Property != null)
                    {
                        //astDoc.MemberProperties.Add(astDoc.Members[i].Property);
                        astDoc.MemberProperties.Add(astDoc.Members[i].Property);
                    }
                }
            }
            // Members
            int indx = -1;
            for (int i = 0; i < astDoc.Members.Count; i++)
            {
                MemberGrid mgrid = new MemberGrid();
                mgrid.MemberNo = astDoc.Members[i].MemberNo;
                mgrid.Type = (MemberGrid.MemberType)astDoc.Members[i].MemberType;
                mgrid.Node1 = astDoc.Members[i].StartNode.NodeNo;
                mgrid.Node2 = astDoc.Members[i].EndNode.NodeNo;

                indx = astDoc.MemberProperties.IndexOf(astDoc.Members[i].MemberNo);
                if (indx != -1)
                {
                    mgrid.YD = astDoc.MemberProperties[indx].YD;
                    mgrid.ZD = astDoc.MemberProperties[indx].ZD;
                    mgrid.IX = astDoc.MemberProperties[indx].IX;
                    mgrid.IY = astDoc.MemberProperties[indx].IY;
                    mgrid.IZ = astDoc.MemberProperties[indx].IZ;
                    mgrid.Area = astDoc.MemberProperties[indx].Area.ToString();
                    mgrid.E = astDoc.MemberProperties[indx].E;
                    mgrid.DEN = astDoc.MemberProperties[indx].DEN;
                    mgrid.PoissionRatio = astDoc.MemberProperties[indx].PR;
                }
                else if (astDoc.Members[i].Property != null)
                {
                    mgrid.YD = astDoc.Members[i].Property.YD;
                    mgrid.ZD = astDoc.Members[i].Property.ZD;
                    mgrid.IX = astDoc.Members[i].Property.IX;
                    mgrid.IY = astDoc.Members[i].Property.IY;
                    mgrid.IZ = astDoc.Members[i].Property.IZ;
                    mgrid.Area = astDoc.Members[i].Property.Area.ToString();
                    mgrid.E = astDoc.Members[i].Property.E;
                    mgrid.DEN = astDoc.Members[i].Property.DEN;
                    mgrid.PoissionRatio = astDoc.Members[i].Property.PR;
                }
                Add(mgrid);
            }

            // Elements

            for (int i = 0; i < astDoc.Elements.Count; i++)
            {
                MemberGrid mgrid = new MemberGrid();
                mgrid.MemberNo = astDoc.Elements[i].ElementNo;
                mgrid.Type = MemberGrid.MemberType.PLATE;
                mgrid.Node1 = astDoc.Elements[i].Node1.NodeNo;
                mgrid.Node2 = astDoc.Elements[i].Node2.NodeNo;
                mgrid.Node3 = astDoc.Elements[i].Node3.NodeNo;
                mgrid.Node4 = astDoc.Elements[i].Node4.NodeNo;
                mgrid.Area = astDoc.Elements[i].ThickNess;
                mgrid.DEN = astDoc.Elements[i].Density;


                //indx = astDoc.MemberProperties.IndexOf(astDoc.Members[i].MemberNo);
                //if (indx != -1)
                //{
                //    mgrid.YD = astDoc.MemberProperties[indx].YD;
                //    mgrid.ZD = astDoc.MemberProperties[indx].ZD;
                //    mgrid.IX = astDoc.MemberProperties[indx].IX;
                //    mgrid.IY = astDoc.MemberProperties[indx].IY;
                //    mgrid.IZ = astDoc.MemberProperties[indx].IZ;
                //    mgrid.Area = astDoc.MemberProperties[indx].Area;
                //    mgrid.E = astDoc.MemberProperties[indx].E;
                //    mgrid.DEN = astDoc.MemberProperties[indx].DEN;
                //    mgrid.PoissionRatio = astDoc.MemberProperties[indx].PR;
                //}
                Add(mgrid);
            }


        }

        public void SetGridView(ref DataGridView dgv)
        {
            for (int i = 0; i < list.Count; i++)
            {
                dgv.Rows.Add(list[i].MemberNo, 
                    list[i].Type.ToString(),
                    list[i].Node1, 
                    list[i].Node2, 
                    (list[i].Node3 == -1?"":list[i].Node3.ToString()) , 
                    (list[i].Node4 == -1?"":list[i].Node3.ToString()),
                    list[i].YD, 
                    list[i].ZD, 
                    list[i].Area, 
                    list[i].IX,
                    list[i].IY,
                    list[i].IZ,
                    list[i].E, 
                    list[i].DEN,
                    list[i].Material,
                    list[i].PoissionRatio);
            }

        }


        #region IList<MemberGrid> Members

        public int IndexOf(MemberGrid item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].MemberNo == item.MemberNo && list[i].Type == item.Type)
                    return i;
            }
            return -1;
        }

        public void Insert(int index, MemberGrid item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public MemberGrid this[int index]
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

        #region ICollection<MemberGrid> Members

        public void Add(MemberGrid item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(MemberGrid item)
        {
            return (IndexOf(item) != -1 ? true : false);
        }

        public void CopyTo(MemberGrid[] array, int arrayIndex)
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

        public bool Remove(MemberGrid item)
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

        #region IEnumerable<MemberGrid> Members

        public IEnumerator<MemberGrid> GetEnumerator()
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
}
