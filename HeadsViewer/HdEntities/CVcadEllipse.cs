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
    public class CVcadEllipse : IHdEllipse
    {
        vdCircle vdobj;
        public CVcadEllipse(vdCircle vdcircle)
        {
            this.vdobj = vdcircle;
        }

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
            get { return "ELLIPSE"; }
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

        #region IHdEllipse Members

        public CPoint3D CenterPoint
        {
            get
            {
                return new CPoint3D(this.vdobj.Center.x, this.vdobj.Center.y, this.vdobj.Center.z);
            }
            set
            {
                this.vdobj.Center = GeneraHelper.GetVDPoint(value);
            }
        }      

        public double Radius
        {
            get { return this.vdobj.Radius; }
        }

        #endregion
    }
}
