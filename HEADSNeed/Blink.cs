using System;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows.Forms;
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

namespace HEADSNeed
{
    public class VectorDrawBlink : vdShape
    {
        #region Main Variables
        private double mRadius = 1.0d;
        private vdHatchProperties mhatchprops = null;
        private System.Windows.Forms.Timer mtimer = null;
        private double mAngle = 0.0d;
        private double mAngleDifference = VectorDraw.Geometry.Globals.DegreesToRadians(5.0);
        private int mInterval = 500;
        private bool mShowLines = true;
        #endregion

        #region ctors + Timer
        public VectorDrawBlink()
        {
            mhatchprops = new vdHatchProperties();
            mhatchprops.SetUnRegisterDocument(Document);
            mhatchprops.FillMode = VdConstFill.VdFillModeSolid;
        }
        private bool IsTimerOn()
        {
            if ((mtimer != null) && (mtimer.Enabled)) return true;
            return false;
        }
        private void SetTimer()
        {
            ReleaseTimer();
            mtimer = new Timer();
            mtimer.Interval = mInterval;
            mtimer.Tick += new EventHandler(mtimer_Tick);
            mtimer.Start();
        }
        private void ReleaseTimer()
        {
            if (mtimer == null) return;
            mtimer.Tick -= new EventHandler(mtimer_Tick);
            mtimer.Stop();
            mtimer = null;
        }
        ~VectorDrawBlink()
        {
            ReleaseTimer();
        }
        void mtimer_Tick(object sender, EventArgs e)
        {
            if (!this.IsDocumentRegister) return;
            if (Document.ActiveLayOut.OverAllActiveActions.Count > 1) return;
            this.Invalidate();
            if (mhatchprops.FillMode == VdConstFill.VdFillModeSolid)
                mhatchprops.FillMode = VdConstFill.VdFillModeNone;
            else
            {
                mhatchprops.FillMode = VdConstFill.VdFillModeSolid;
                mAngle += mAngleDifference;
                if (mAngle > VectorDraw.Geometry.Globals.VD_TWOPI) mAngle = 0.0;
            }
            this.Update();
            this.Invalidate();
        }
        protected override void OnDocumentSelected(vdDocument document)
        {
            base.OnDocumentSelected(document);
            if (this.IsDocumentRegister)
                SetTimer();
            else
                ReleaseTimer();
        }
        public override bool Deleted
        {
            get
            {
                return base.Deleted;
            }
            set
            {
                base.Deleted = value;
                if (value)
                    ReleaseTimer();
                else
                    SetTimer();
            }
        }
        protected override void OnOwnerChanged()
        {
            base.OnOwnerChanged();
            if (this.Owner == null) ReleaseTimer();
        }
        #endregion

        #region not vdShape supported properties
        [Browsable(false)]
        public override double Rotation
        {//do not support vdShape Rotation
            get
            {
                //return base.Rotation;
                return 0.0d;
            }
            set
            {
                //base.Rotation = value;
            }
        }
        [Browsable(false)]
        public override Vector Scales
        {//do not support vdShape Scales
            get
            {
                //return base.Scales;
                return new Vector(1.0d, 1.0d, 1.0d);
            }
            set
            {
                //base.Scales = value;
            }
        }
        #endregion

        #region vdShape override properties
        public override gPoint Origin
        {
            get
            {
                return base.Origin;
            }
            set
            {
                base.Origin = value;
                Update();
            }
        }
        #endregion

        #region public Properties
        //You should use the AddHistory command in your properties so any change will be writen to the history
        //The string in the AddHistory command is the name of the property exactly.
        public double Radius
        {
            get { return mRadius; }
            set
            {
                AddHistory("Radius", value);
                mRadius = value;
            }
        }
        public int Interval
        {
            get { return mInterval; }
            set
            {
                AddHistory("Interval", value);
                mInterval = value;
                if (IsTimerOn()) mtimer.Interval = mInterval;
            }
        }
        public double LineRotAngleDifference
        {
            get { return VectorDraw.Geometry.Globals.RadiansToDegrees(mAngleDifference); }
            set
            {
                AddHistory("LineRotAngleDifference", value);
                mAngleDifference = VectorDraw.Geometry.Globals.DegreesToRadians(value);
            }
        }
        public bool ShowLines
        {
            get { return mShowLines; }
            set
            {
                AddHistory("ShowLines", value);
                mShowLines = value;
            }
        }
        #endregion

        #region Serialization
        public override void Serialize(Serializer serializer)
        {
            base.Serialize(serializer);
            serializer.Serialize(mRadius, "Radius");
            serializer.Serialize(mInterval, "Interval");
            serializer.Serialize(mAngleDifference, "LineRotAngleDifference");
            serializer.Serialize(mShowLines, "ShowLines");

        }
        public override bool DeSerialize(DeSerializer deserializer, string fieldname, object value)
        {
            if (base.DeSerialize(deserializer, fieldname, value)) return true;
            else if (fieldname == "Radius") mRadius = (double)value;
            else if (fieldname == "Interval") mInterval = (int)value;
            else if (fieldname == "LineRotAngleDifference") mAngleDifference = (double)value;
            else if (fieldname == "ShowLines") mShowLines = (bool)value;
            else return false;
            return true;
        }
        #endregion

        #region Object Figure Calculations
        public override void FillShapeEntities(ref vdEntities entities)
        {
            vdCircle circle = new vdCircle();
            entities.AddItem(circle);
            circle.MatchProperties(this, Document);
            circle.Radius = mRadius;
            circle.HatchProperties = mhatchprops;

            if (mShowLines)
            {
                gPoint cen = new gPoint();
                vdLine line1;
                double angle = 0.0;
                for (int i = 0; i < 4; i++)
                {
                    line1 = new vdLine();
                    line1.MatchProperties(this, Document);
                    if (mhatchprops.FillMode == VdConstFill.VdFillModeNone)
                    {
                        line1.LineType = Document.LineTypes.Invisible;
                    }
                    line1.StartPoint = new gPoint(gPoint.Polar(cen, angle + mAngle, 3.0d * mRadius / 2.0d));
                    line1.EndPoint = new gPoint(gPoint.Polar(cen, angle + mAngle, 2.0d * mRadius));
                    angle += VectorDraw.Geometry.Globals.HALF_PI;
                    entities.AddItem(line1);
                }
            }
        }
        #endregion

        #region Overrides
        public override void InitializeProperties()
        {
            mRadius = 1.0d;
            mhatchprops = null;
            mAngle = 0.0d;
            mAngleDifference = VectorDraw.Geometry.Globals.DegreesToRadians(5.0);
            mInterval = 500;
            mShowLines = true;
            base.InitializeProperties();
        }
        public override void MatchProperties(VectorDraw.Professional.vdObjects.vdPrimary _from, VectorDraw.Professional.vdObjects.vdDocument thisdocument)
        {
            base.MatchProperties(_from, thisdocument);
            VectorDrawBlink from = _from as VectorDrawBlink;
            if (from == null) return;
            Radius = from.Radius;
            LineRotAngleDifference = from.LineRotAngleDifference;
            Interval = from.Interval;
            ShowLines = from.ShowLines;
        }
        public override void Transformby(Matrix mat)
        {
            if (mat.IsUnitMatrix()) return;
            Matrix mult = (ECSMatrix * mat);
            MatrixProperties matprops = mult.Properties;
            Radius *= matprops.GetScaleXY();
            base.Transformby(mat);
        }
        public override gPoints GetGripPoints()
        {
            gPoints ret = new gPoints();
            gPoint cen = new gPoint();
            ret.Add(cen);
            ECSMatrix.Transform(ret);
            return ret;
        }
        public override void MoveGripPointsAt(Int32Array Indexes, double dx, double dy, double dz)
        {
            if (Indexes == null || Indexes.Count == 0 || (dx == 0.0 && dy == 0.0 && dz == 0.0)) return;
            Matrix mat = new Matrix();
            mat.TranslateMatrix(dx, dy, dz);
            gPoints grips = GetGripPoints();
            ECSMatrix.GetInvertion().Transform(grips);
            Transformby(mat);
            Update();
        }
        public override bool IsTableObjectDependOn(vdPrimary table)
        {
            if (Deleted) return false;//always check if this object is deleted
            if (base.IsTableObjectDependOn(table)) return true;//always check the base implamentation
            //check properties of object that reference the table object.
            if (mhatchprops != null && mhatchprops.IsTableObjectDependOn(table)) return true;
            return false;
        }
        public override void GetTableDependecies(vdLayers layers, vdBlocks blocks, vdDimstyles dimstyles, vdLineTypes linetypes, vdTextstyles textstyles, vdImages images, vdHatchPatterns hatchpatterns, object breakOnObject, ref bool breakObjectFound)
        {
            if (breakOnObject != null && breakObjectFound) return;
            if (Deleted) return;
            //base.GetTableDependecies(layers, blocks, dimstyles, linetypes, textstyles, images, hatchpatterns, breakOnObject, ref breakObjectFound);
            //if (this.mhatchprops != null)
            //{
            //    mhatchprops.GetTableDependecies(layers, blocks, dimstyles, linetypes, textstyles, images, hatchpatterns, breakOnObject, ref breakObjectFound);
            //}

        }
        #endregion
    }

    public class ActionBlink : ActionEntity
    {
        private VectorDrawBlink figure;
        public ActionBlink(gPoint reference, vdLayout layout)
            : base(reference, layout)
        {
            ValueTypeProp |= valueType.DISTANCE;
            figure = new VectorDrawBlink();
            figure.SetUnRegisterDocument(layout.Document);
            figure.setDocumentDefaults();
            figure.Origin = reference;
        }
        protected override void OnMyPositionChanged(gPoint newPosition)
        {
            figure.Radius = newPosition.Distance3D(figure.Origin);
        }
        public override vdFigure Entity
        {
            get
            {
                return figure;
            }
        }
        public static void CmdBlink(vdDocument doc)
        {
            gPoint cen = new gPoint();
            doc.Prompt("Origin-Center Point : ");
            object ret = doc.ActionUtility.getUserPoint();
            doc.Prompt(null);
            if (ret == null || !(ret is gPoint)) goto error;
            cen = ret as gPoint;
            doc.Prompt("Radius : ");
            ActionBlink aFig = new ActionBlink(cen, doc.ActiveLayOut);
            doc.ActionAdd(aFig);
            StatusCode scode = aFig.WaitToFinish();
            doc.Prompt(null);
            if (scode != VectorDraw.Actions.StatusCode.Success) goto error;
            aFig.Entity.Transformby(doc.User2WorldMatrix);
            doc.ActionLayout.Entities.AddItem(aFig.Entity);
            doc.ActionDrawFigure(aFig.Entity);
            return;
        error:
            return;
        }
    }
}
