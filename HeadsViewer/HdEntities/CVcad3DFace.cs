using System;
using System.Collections.Generic;
using System.Text;
using HeadsUtils;
using HeadsUtils.Interfaces;
using VectorDraw.Professional.vdFigures;

namespace HeadsViewer.HdEntities
{
    internal class CVcad3DFace:IHd3DFace
    {
        vd3DFace vdobj;
        public CVcad3DFace(vd3DFace vd3dface)
        {
            this.vdobj = vd3dface;
        }

        #region IHd3DFace Members

        public HeadsUtils.CPoint3D[] Coordinates
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public void set_Coordinate(int Index, HeadsUtils.CPoint3D pVal)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public HeadsUtils.CPoint3D get_Coordinate(int Index)
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
            get { throw new Exception("The method or operation is not implemented."); }
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
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public double LinetypeScale
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
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
            get { return "FACE3D"; }
        }

        public int OwnerID
        {
            get { return this.vdobj.Owner.Id; }
        }

        public double Thickness
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
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
                throw new Exception("The method or operation is not implemented.");
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
