using System;
using System.Collections.Generic;
using System.Text;
using HeadsUtils;
using HeadsUtils.Interfaces;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdObjects;

namespace HeadsViewer.HdEntities
{
    internal class CVcadLayer:IHdLayer
    {
        vdLayer vdobj;
        public CVcadLayer(vdLayer vdlayer)
        {
            this.vdobj = vdlayer;
        }

        #region IHdLayer Members

        public eHEADS_COLOR color
        {
            get
            {
                return  GeneraHelper.ToHdColor(this.vdobj.PenColor);
            }
            set
            {
                this.vdobj.PenColor = GeneraHelper.ToVdColor(value);
            }
        }

        public string Description
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

        public bool Freeze
        {
            get
            {
                return this.vdobj.Frozen;
            }
            set
            {
                this.vdobj.Frozen = value;
            }
        }

        public string Handle
        {
            get { return this.vdobj.HandleId.ToString(); }
        }

        public bool LayerOn
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

        public string Linetype
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

        public bool Lock
        {
            get
            {
                return this.vdobj.Lock;
            }
            set
            {
                this.vdobj.Lock = value;
            }
        }

        public string Name
        {
            get
            {
                return this.vdobj.Name;
            }
            set
            {
                this.vdobj.Name = value;
            }
        }

        public bool Used
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public void Delete()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Erase()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public object Instance
        {
            get { return vdobj; }
        }

        #endregion
    }
}
