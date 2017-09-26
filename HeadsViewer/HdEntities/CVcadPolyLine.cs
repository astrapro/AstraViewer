using System;
using System.Collections.Generic;
using System.Text;
using HeadsUtils;
using HeadsUtils.Interfaces;
using HeadsViewer;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.Constants;

namespace HeadsViewer.HdEntities
{
    internal class CVcadPolyLine:IHdPolyline3D
    {
        vdPolyline vdobj;
        public CVcadPolyLine(vdPolyline vdpolyline)
        {
            this.vdobj = vdpolyline;
        }

        #region IHdPolyline3D Members

        public bool Closed
        {
            get
            {
                return (this.vdobj.Flag == VdConstPlineFlag.PlFlagCLOSE) ? true : false;
            }
            set
            {
                if (value == true)
                {
                    this.vdobj.Flag = VdConstPlineFlag.PlFlagCLOSE;
                }
                else
                {
                    this.vdobj.Flag = VdConstPlineFlag.PlFlagOPEN;
                }
                
            }
        }

        public double Length
        {
            get { return this.vdobj.Length();}
        }

        public HeadsUtils.CPoint3D[] Coordinates
        {
            get
            {
                List<HeadsUtils.CPoint3D> vlist = new List<CPoint3D>();
                foreach (VectorDraw.Geometry.Vertex ver in this.vdobj.VertexList)
                {
                    VectorDraw.Geometry.gPoint pt = ver.AsgPoint();
                    vlist.Add(new CPoint3D(pt.x, pt.y, pt.z));
                }
                return vlist.ToArray();
            }
            set
            {
                VectorDraw.Geometry.Vertexes vers = new VectorDraw.Geometry.Vertexes();
                foreach(HeadsUtils.CPoint3D pt in value)
                {
                    vers.Add(GeneraHelper.GetVDPoint(pt));
                }
                this.vdobj.VertexList = vers;
            }
        }

        public void AppendVertex(HeadsUtils.CPoint3D vertex)
        {
            VectorDraw.Geometry.Vertex ver = new VectorDraw.Geometry.Vertex(GeneraHelper.GetVDPoint(vertex));
            this.vdobj.AppendVertex(ver);
        }

        public void RemoveVertex(int iIndex)
        {
            this.vdobj.VertexList.RemoveAt(iIndex);
        }

        public HeadsUtils.CPoint3D get_Coordinate(int Index)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void set_Coordinate(int Index, HeadsUtils.CPoint3D pVal)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region IHdEntity Members

        public eHEADS_COLOR color
        {
            get
            {
                return GeneraHelper.ToHdColor(this.vdobj.PenColor);
            }
            set
            {
                this.vdobj.PenColor = GeneraHelper.ToVdColor(value);
            }
        }

        public string EntityName
        {
            get { return this.ObjectName; }
        }

        public string Handle
        {
            get { return this.vdobj.Label; }
        }
        public string Label
        {
            get
            {
                return this.vdobj.Label;
            }
            set
            {
                this.vdobj.Label = value;
            }
        }

        public string Layer
        {
            get
            {
                return this.vdobj.Layer.Name;
            }
            set
            {
                this.vdobj.Layer.Name = value;
            }
        }

        public double LinetypeScale
        {
            get
            {
                throw new Exception("LinetypeScale - The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("LinetypeScale -The method or operation is not implemented.");
            }
        }

        public eHEADS_LWEIGHT Lineweight
        {
            get
            {
                return (eHEADS_LWEIGHT)this.vdobj.LineWeight;
            }
            set
            {
                this.vdobj.LineWeight = (VectorDraw.Professional.Constants.VdConstLineWeight)value;
            }
        }

        public int ObjectID
        {
            get { return this.vdobj.Id; }
        }

        public string ObjectName
        {
            get { return "POLYLINE3D"; }
        }

        public int OwnerID
        {
            get { return this.vdobj.Owner.Id; }
        }

        public double Thickness
        {
            get
            {
                throw new Exception("Thickness - The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("Thickness - The method or operation is not implemented.");
            }
        }

        public bool Visible
        {
            get
            {
                return this.vdobj.IsVisible();
            }
            set
            {
                throw new Exception("Visible - The method or operation is not implemented.");
            }
        }

        public IHdEntity Copy()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Delete()
        {
            this.Erase();
        }

        public void Erase()
        {
            if (this.vdobj.Deleted == false)
            {
                this.vdobj.Invalidate();
                this.vdobj.Deleted = true;
                this.vdobj.Update();
            }
        }

        public void Move(HeadsUtils.CPoint3D FromPoint, HeadsUtils.CPoint3D ToPoint)
        {
            
            throw new Exception("The method or operation is not implemented.");
        }

        public void Rotate(HeadsUtils.CPoint3D BasePoint, double RotationAngle)
        {
            
            throw new Exception("The method or operation is not implemented.");
        }

        public void Rotate3D(HeadsUtils.CPoint3D Point1, HeadsUtils.CPoint3D Point2, double RotationAngle)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Update()
        {
            this.vdobj.Update();
        }
        public void SetLayer(IHdLayer layer)
        {
            this.vdobj.Layer = (VectorDraw.Professional.vdPrimaries.vdLayer)((CVcadLayer)layer).Instance;
        }

        public short ColorIndex
        {
            get
            {
                return this.vdobj.PenColor.ColorIndex;
            }
            set
            {
                this.vdobj.PenColor.ColorIndex = value;
            }
        }
        #endregion

        
    }
}
