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

namespace HEADSNeed.ASTRA.ASTRADrawingTools
{
    public class ASTRAMemberLoad : vdFigure, IvdProxyFigure
    {

        #region private fields
        private gPoints mArrowPoints = new gPoints();
        private double mArrowSize = 1.0d;
        private gPoint mStartPoint = new gPoint();
        private gPoint mEndPoint = new gPoint();

        #endregion
        #region ctor
        public ASTRAMemberLoad()
        {
            mArrowPoints.Add(new gPoint(0.0d, 0.0d, 0.0d));
            mArrowPoints.Add(new gPoint(-1.0d, -0.1665d, 0.0d));
            mArrowPoints.Add(new gPoint(-1.0d, 0.1665d, 0.0d));
            mArrowPoints.Add(new gPoint(0.0d, 0.0d, 0.0d));
        }
        #endregion
        #region public properties
        //You should use the AddHistory command in your properties so any change will be writen to the history
        //The string in the AddHistory command is the name of the property exactly.
        public double arrowSize
        {
            get
            {
                return mArrowSize;
            }
            set
            {
                AddHistory("arrowSize", value);
                mArrowSize = value;
            }
        }
        public gPoint StartPoint
        {
            get
            {
                return mStartPoint;
            }
            set
            {
                AddHistory("StartPoint", value);
                mStartPoint.CopyFrom(value);
            }
        }
        public gPoint EndPoint
        {
            get
            {
                return mEndPoint;
            }
            set
            {
                AddHistory("EndPoint", value);
                mEndPoint.CopyFrom(value);
            }
        }
        #endregion
        #region private methods


        Matrix ArrowEcsMatrix(Vector ViewDir)
        {
            Matrix mArrowMatrix = new Matrix();
            mArrowMatrix.ScaleMatrix(mArrowSize, mArrowSize, 1.0d);
            mArrowMatrix.ApplyECS2WCS(ViewDir, new Vector(StartPoint, EndPoint));
            mArrowMatrix.TranslateMatrix(EndPoint);
            return mArrowMatrix;

        }
        #endregion
        #region pline overrides

        public override void Serialize(Serializer serializer)
        {
            base.Serialize(serializer);
            serializer.Serialize(mArrowSize, "arrowSize");
            serializer.Serialize(mStartPoint, "StartPoint");
            serializer.Serialize(mEndPoint, "EndPoint");
        }
        public override bool DeSerialize(DeSerializer deserializer, string fieldname, object value)
        {
            if (base.DeSerialize(deserializer, fieldname, value)) return true;
            else if (fieldname == "arrowSize") mArrowSize = (double)value;
            else if (fieldname == "StartPoint") mStartPoint = (gPoint)value;
            else if (fieldname == "EndPoint") mEndPoint = (gPoint)value;
            else return false;
            return true;
        }
        public override Box BoundingBox
        {
            get
            {
                if (!mBoundBox.IsEmpty) return mBoundBox;
                mBoundBox = new Box();
                mBoundBox.AddPoint(StartPoint);
                mBoundBox.AddPoint(EndPoint);
                gPoints pts = new gPoints();
                pts.AddRange(mArrowPoints);
                ArrowEcsMatrix(new Vector(0, 0, 1)).Transform(pts);
                mBoundBox.AddPoints(pts);
                return mBoundBox;
            }
        }
        public override void Transformby(Matrix mat)
        {
            StartPoint = mat.Transform(StartPoint);
            EndPoint = mat.Transform(EndPoint);
            base.Transformby(mat);
        }
        public override vdRender.DrawStatus Draw(vdRender render)
        {
            vdRender.DrawStatus doDraw = base.Draw(render);
            if (doDraw == vdRender.DrawStatus.Successed)
            {

                render.DrawLine(this, StartPoint, EndPoint);

                render.PushMatrix(ArrowEcsMatrix(render.ViewDir));
                render.DrawSolidPolygon(this, mArrowPoints, vdRender.PolygonType.Simple);
                render.PopMatrix();
            }
            AfterDraw(render);
            return doDraw;
        }
        public override gPoints GetGripPoints()
        {
            gPoints ret = new gPoints();
            ret.Add(StartPoint);
            ret.Add(EndPoint);
            return ret;
        }
        public override void MoveGripPointsAt(Int32Array Indexes, double dx, double dy, double dz)
        {
            if (Indexes == null || Indexes.Count == 0 || (dx == 0.0 && dy == 0.0 && dz == 0.0)) return;
            gPoints grips = GetGripPoints();
            if (Indexes.Count == grips.Count)
            {
                Matrix mat = new Matrix();
                mat.TranslateMatrix(dx, dy, dz);
                Transformby(mat);
            }
            else
            {
                foreach (int index in Indexes)
                {
                    switch (index)
                    {
                        case 0:
                            StartPoint += new gPoint(dx, dy, dz);
                            break;
                        case 1:
                            EndPoint += new gPoint(dx, dy, dz);
                            break;
                        default:
                            break;
                    }
                }
            }
            Update();
        }
        public override void MatchProperties(VectorDraw.Professional.vdObjects.vdPrimary _from, VectorDraw.Professional.vdObjects.vdDocument thisdocument)
        {
            base.MatchProperties(_from, thisdocument);
            ASTRAMemberLoad from = _from as ASTRAMemberLoad;
            if (from == null) return;
            StartPoint = from.StartPoint;
            EndPoint = from.EndPoint;
            arrowSize = from.arrowSize;
        }
        public override vdEntities Explode()
        {
            vdEntities Entities = new vdEntities();
            Entities.SetUnRegisterDocument(Document);
            if (Document != null) Document.UndoHistory.PushEnable(false);
            vdLine line = new vdLine();
            line.StartPoint = StartPoint;
            line.EndPoint = EndPoint;
            line.MatchProperties(this, Document);
            Entities.AddItem(line);

            vdPolyline pl = new vdPolyline();
            pl.VertexList.AddRange(this.mArrowPoints);
            pl.Flag = VdConstPlineFlag.PlFlagCLOSE;
            pl.HatchProperties = new vdHatchProperties();
            pl.HatchProperties.FillMode = VdConstFill.VdFillModeSolid;
            pl.HatchProperties.FillColor.ByBlock = true;
            pl.Transformby(ArrowEcsMatrix(new Vector(0, 0, 1)));
            pl.MatchProperties(this, Document);
            Entities.AddItem(pl);
            if (Document != null) Document.UndoHistory.PopEnable();
            return Entities;
        }
        public override bool IsTableObjectDependOn(vdPrimary table)
        {
            //we call only the base check because there are not tables objects reference to this object properties.
            if (Deleted) return false;
            if (base.IsTableObjectDependOn(table)) return true;
            return false;
        }
        public override void GetTableDependecies(vdLayers layers, vdBlocks blocks, vdDimstyles dimstyles, vdLineTypes linetypes, vdTextstyles textstyles, vdImages images, vdHatchPatterns hatchpatterns, object breakOnObject, ref bool breakObjectFound)
        {
            //Since there no extra dependencies the base call is enough.

            vdTableDependeciesArgs vd = new vdTableDependeciesArgs();
            //base.GetTableDependecies(layers, blocks, dimstyles, linetypes, textstyles, images, hatchpatterns, breakOnObject, ref breakObjectFound);
            base.GetTableDependecies(vd);
        }
        #endregion
    }
    public class ActionASTRAMemberLoad : ActionEntity
    {
        private ASTRAMemberLoad line;
        public ActionASTRAMemberLoad(gPoint reference, vdLayout layout)
            : base(reference, layout)
        {
            line = new ASTRAMemberLoad();
            line.SetUnRegisterDocument(layout.Document);
            line.setDocumentDefaults();
            line.StartPoint = reference;
            line.EndPoint = reference;
        }
        public override bool HideRubberLine
        {
            get
            {
                return true;
            }
        }
        public override vdFigure Entity
        {
            get
            {
                return line;
            }
        }
        protected override void OnMyPositionChanged(gPoint newPosition)
        {
            line.EndPoint = newPosition;
        }
        public override bool needUpdate
        {
            get
            {
                return true;
            }
        }

        public static void CmdASTRAMemberLoad1(vdDocument document)
        {
            gPoint EPT, SPT;
            document.Prompt("Start Point:");
            object ret = document.ActionUtility.getUserPoint();
            document.Prompt(null);
            if (ret == null || !(ret is gPoint)) return;
            SPT = ret as gPoint;

            ActionASTRAMemberLoad aFig = new ActionASTRAMemberLoad(SPT, document.ActiveLayOut);
            document.Prompt("End Point :");

            document.ActionAdd(aFig);
            StatusCode scode = aFig.WaitToFinish();
            document.Prompt(null);
            if (scode != VectorDraw.Actions.StatusCode.Success) return;

            EPT = aFig.Value as gPoint;



            vdLine line1 = new vdLine();
            line1.SetUnRegisterDocument(document);
            line1.setDocumentDefaults();
            line1.StartPoint = SPT;
            line1.EndPoint = EPT;
            document.ActiveLayOut.Entities.AddItem(line1);

            vdLine line2 = new vdLine();
            line2.SetUnRegisterDocument(document);
            line2.setDocumentDefaults();
            line2.StartPoint = new gPoint(SPT.x, SPT.y - 1, SPT.z);
            line2.EndPoint = new gPoint(EPT.x, EPT.y - 1, EPT.z);
            document.ActiveLayOut.Entities.AddItem(line2);


            ASTRAMemberLoad aline = new ASTRAMemberLoad();
            document.ActionLayout.Entities.AddItem(aline);
            document.UndoHistory.PushEnable(false);
            aline.InitializeProperties();
            aline.setDocumentDefaults();
            aline.StartPoint = line1.StartPoint;
            aline.EndPoint = line2.StartPoint;
            aline.arrowSize = 0.3;
            aline.Transformby(document.User2WorldMatrix);
            document.UndoHistory.PopEnable();
            document.ActionDrawFigure(aline);

            ASTRAMemberLoad aline1 = new ASTRAMemberLoad();
            document.ActionLayout.Entities.AddItem(aline1);
            document.UndoHistory.PushEnable(false);
            aline1.InitializeProperties();
            aline1.setDocumentDefaults();
            aline1.arrowSize = 0.3;
            aline1.StartPoint = line1.EndPoint;
            aline1.EndPoint = line2.EndPoint;
            aline1.Transformby(document.User2WorldMatrix);
            document.UndoHistory.PopEnable();
            document.ActionDrawFigure(aline);

            document.Redraw(true);






        }
        public static void CmdASTRAMemberLoad2(vdDocument document)
        {
            gPoint EPT, SPT;
            document.Prompt("Start Point:");
            object ret = document.ActionUtility.getUserPoint();
            document.Prompt(null);
            if (ret == null || !(ret is gPoint)) return;
            SPT = ret as gPoint;

            ActionASTRAMemberLoad aFig = new ActionASTRAMemberLoad(SPT, document.ActiveLayOut);
            document.Prompt("End Point :");

            document.ActionAdd(aFig);
            StatusCode scode = aFig.WaitToFinish();
            document.Prompt(null);
            if (scode != VectorDraw.Actions.StatusCode.Success) return;

            EPT = aFig.Value as gPoint;



            vdLine line1 = new vdLine();
            line1.SetUnRegisterDocument(document);
            line1.setDocumentDefaults();
            line1.StartPoint = SPT;
            line1.EndPoint = EPT;
            document.ActiveLayOut.Entities.AddItem(line1);

            vdLine line2 = new vdLine();
            line2.SetUnRegisterDocument(document);
            line2.setDocumentDefaults();
            line2.StartPoint = new gPoint(SPT.x, SPT.y - 1, SPT.z);
            line2.EndPoint = new gPoint(EPT.x, EPT.y - 1, EPT.z);
            document.ActiveLayOut.Entities.AddItem(line2);


            ASTRAMemberLoad aline = new ASTRAMemberLoad();
            document.ActionLayout.Entities.AddItem(aline);
            document.UndoHistory.PushEnable(false);
            aline.InitializeProperties();
            aline.setDocumentDefaults();
            aline.StartPoint = line1.StartPoint;
            aline.EndPoint = line2.StartPoint;
            aline.arrowSize = 0.3;
            aline.Transformby(document.User2WorldMatrix);
            document.UndoHistory.PopEnable();
            document.ActionDrawFigure(aline);


            double ll = ((line1.Length() / 5.0d) / line1.Length());
            double sx, sy, sz;
            double ex, ey, ez;
            sx = sy = sz = 0.0d;
            for (int j = 1; j <= 5; j++)
            {
                ASTRAMemberLoad aline1 = new ASTRAMemberLoad();
                document.ActionLayout.Entities.AddItem(aline1);
                document.UndoHistory.PushEnable(false);
                aline1.InitializeProperties();
                aline1.setDocumentDefaults();
                aline1.arrowSize = 0.3;

                sx = (j * ll) * (line1.EndPoint.x - line1.StartPoint.x) + line1.StartPoint.x;
                sy = (j * ll) * (line1.EndPoint.y - line1.StartPoint.y) + line1.StartPoint.y;
                sz = (j * ll) * (line1.EndPoint.z - line1.StartPoint.z) + line1.StartPoint.z;

                aline1.StartPoint = new gPoint(sx, sy, sz);

                ex = (j * ll) * (line2.EndPoint.x - line2.StartPoint.x) + line2.StartPoint.x;
                ey = (j * ll) * (line2.EndPoint.y - line2.StartPoint.y) + line2.StartPoint.y;
                ez = (j * ll) * (line2.EndPoint.z - line2.StartPoint.z) + line2.StartPoint.z;

                aline1.EndPoint = new gPoint(ex, ey, ez);
                aline1.Transformby(document.User2WorldMatrix);
                document.UndoHistory.PopEnable();
                document.ActionDrawFigure(aline);
            }
            document.Redraw(true);
        }
        public static void CmdASTRAMemberLoad3(vdDocument document,gPoint StartPoint,gPoint EndPoint)
        {

            gPoint EPT, SPT;

            SPT = StartPoint;

            EPT = EndPoint;



            vdLine line1 = new vdLine();
            line1.SetUnRegisterDocument(document);
            line1.setDocumentDefaults();
            line1.StartPoint = SPT;
            line1.EndPoint = EPT;
            document.ActiveLayOut.Entities.AddItem(line1);

            vdLine line2 = new vdLine();
            line2.SetUnRegisterDocument(document);
            line2.setDocumentDefaults();
            line2.StartPoint = new gPoint(SPT.x, SPT.y - 1, SPT.z);
            line2.EndPoint = new gPoint(EPT.x, EPT.y - 1, EPT.z);
            document.ActiveLayOut.Entities.AddItem(line2);


            ASTRAMemberLoad aline = new ASTRAMemberLoad();
            document.ActionLayout.Entities.AddItem(aline);
            document.UndoHistory.PushEnable(false);
            aline.InitializeProperties();
            aline.setDocumentDefaults();
            aline.StartPoint = line2.StartPoint;
            aline.EndPoint = line1.StartPoint;
            aline.arrowSize = 0.3;
            aline.Transformby(document.User2WorldMatrix);
            document.UndoHistory.PopEnable();
            document.ActionDrawFigure(aline);


            double ll = ((line1.Length() / 5.0d) / line1.Length());
            double sx, sy, sz;
            double ex, ey, ez;
            sx = sy = sz = 0.0d;
            for (int j = 1; j <= 5; j++)
            {
                ASTRAMemberLoad aline1 = new ASTRAMemberLoad();
                document.ActionLayout.Entities.AddItem(aline1);
                document.UndoHistory.PushEnable(false);
                aline1.InitializeProperties();
                aline1.setDocumentDefaults();
                aline1.arrowSize = 0.3;

                sx = (j * ll) * (line2.EndPoint.x - line2.StartPoint.x) + line2.StartPoint.x;
                sy = (j * ll) * (line2.EndPoint.y - line2.StartPoint.y) + line2.StartPoint.y;
                sz = (j * ll) * (line2.EndPoint.z - line2.StartPoint.z) + line2.StartPoint.z;

                aline1.StartPoint = new gPoint(sx, sy, sz);

                ex = (j * ll) * (line1.EndPoint.x - line1.StartPoint.x) + line1.StartPoint.x;
                ey = (j * ll) * (line1.EndPoint.y - line1.StartPoint.y) + line1.StartPoint.y;
                ez = (j * ll) * (line1.EndPoint.z - line1.StartPoint.z) + line1.StartPoint.z;

                aline1.EndPoint = new gPoint(ex, ey, ez);
                aline1.Transformby(document.User2WorldMatrix);
                document.UndoHistory.PopEnable();
                document.ActionDrawFigure(aline);
            }
            document.Redraw(true);
        }
        public static void CmdASTRAMemberLoad(vdDocument document)
        {
            gPoint EPT, SPT;
            document.Prompt("Start Point:");
            object ret = document.ActionUtility.getUserPoint();
            document.Prompt(null);
            if (ret == null || !(ret is gPoint)) return;
            SPT = ret as gPoint;

            ActionASTRAMemberLoad aFig = new ActionASTRAMemberLoad(SPT, document.ActiveLayOut);
            document.Prompt("End Point :");

            document.ActionAdd(aFig);
            StatusCode scode = aFig.WaitToFinish();
            document.Prompt(null);
            if (scode != VectorDraw.Actions.StatusCode.Success) return;

            EPT = aFig.Value as gPoint;



            vdLine line1 = new vdLine();
            line1.SetUnRegisterDocument(document);
            line1.setDocumentDefaults();
            line1.StartPoint = SPT;
            line1.EndPoint = EPT;
            document.ActiveLayOut.Entities.AddItem(line1);

            vdLine line2 = new vdLine();
            line2.SetUnRegisterDocument(document);
            line2.setDocumentDefaults();
            line2.StartPoint = new gPoint(SPT.x, SPT.y - 1, SPT.z);
            line2.EndPoint = new gPoint(EPT.x, EPT.y - 1, EPT.z);
            document.ActiveLayOut.Entities.AddItem(line2);


            ASTRAMemberLoad aline = new ASTRAMemberLoad();
            document.ActionLayout.Entities.AddItem(aline);
            document.UndoHistory.PushEnable(false);
            aline.InitializeProperties();
            aline.setDocumentDefaults();
            aline.StartPoint = line2.StartPoint;
            aline.EndPoint = line1.StartPoint;
            aline.arrowSize = 0.3;
            aline.Transformby(document.User2WorldMatrix);
            document.UndoHistory.PopEnable();
            document.ActionDrawFigure(aline);


            double ll = ((line1.Length() / 5.0d) / line1.Length());
            double sx, sy, sz;
            double ex, ey, ez;
            sx = sy = sz = 0.0d;
            for (int j = 1; j <= 5; j++)
            {
                ASTRAMemberLoad aline1 = new ASTRAMemberLoad();
                document.ActionLayout.Entities.AddItem(aline1);
                document.UndoHistory.PushEnable(false);
                aline1.InitializeProperties();
                aline1.setDocumentDefaults();
                aline1.arrowSize = 0.3;

                sx = (j * ll) * (line2.EndPoint.x - line2.StartPoint.x) + line2.StartPoint.x;
                sy = (j * ll) * (line2.EndPoint.y - line2.StartPoint.y) + line2.StartPoint.y;
                sz = (j * ll) * (line2.EndPoint.z - line2.StartPoint.z) + line2.StartPoint.z;

                aline1.StartPoint = new gPoint(sx, sy, sz);

                ex = (j * ll) * (line1.EndPoint.x - line1.StartPoint.x) + line1.StartPoint.x;
                ey = (j * ll) * (line1.EndPoint.y - line1.StartPoint.y) + line1.StartPoint.y;
                ez = (j * ll) * (line1.EndPoint.z - line1.StartPoint.z) + line1.StartPoint.z;

                aline1.EndPoint = new gPoint(ex, ey, ez);
                aline1.Transformby(document.User2WorldMatrix);
                document.UndoHistory.PopEnable();
                document.ActionDrawFigure(aline);
            }
            document.Redraw(true);
        }
    }


    public class ASTRAMemberLoad_UDL : vdShape
    {
        #region Main Variables
        private double mRadius = 1.0d;
        private vdHatchProperties mhatchprops = null;
        //private System.Windows.Forms.Timer mtimer = null;
        private double mAngle = 0.0d;
        private double mAngleDifference = VectorDraw.Geometry.Globals.DegreesToRadians(5.0);
        private int mInterval = 500;
        private bool mShowLines = true;

        private gPoint gpStartPoint = null;
        private gPoint gpEndPoint = null;

        
        #endregion

        #region ctors + Timer
        public ASTRAMemberLoad_UDL()
        {
            mhatchprops = new vdHatchProperties();
            mhatchprops.SetUnRegisterDocument(Document);
            mhatchprops.FillMode = VdConstFill.VdFillModeSolid;

            gpEndPoint = new gPoint();
            gpStartPoint = new gPoint();
        }
        private bool IsTimerOn()
        {
            //if ((mtimer != null) && (mtimer.Enabled)) return true;
            return false;
        }
        private void SetTimer()
        {
            //ReleaseTimer();
            //mtimer = new Timer();
            //mtimer.Interval = mInterval;
            //mtimer.Tick += new EventHandler(mtimer_Tick);
            //mtimer.Start();
        }
        private void ReleaseTimer()
        {
            //if (mtimer == null) return;
            //mtimer.Tick -= new EventHandler(mtimer_Tick);
            //mtimer.Stop();
            //mtimer = null;
        }
        ~ASTRAMemberLoad_UDL()
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
        public gPoint StartPoint
        {
            get
            {
                return gpStartPoint;
            }
            set
            {
                AddHistory("StartPoint", value);
                gpStartPoint = value;
            }
        }
        [Browsable(false)]
        public gPoint EndPoint
        {
            get
            {
                return gpEndPoint;
            }
            set
            {
                AddHistory("EndPoint", value);
                gpStartPoint = value;
            }
        }



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
                //if (IsTimerOn()) mtimer.Interval = mInterval;
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
            serializer.Serialize(gpStartPoint, "StartPoint");
            serializer.Serialize(gpEndPoint, "EndPoint");

        }
        public override bool DeSerialize(DeSerializer deserializer, string fieldname, object value)
        {
            if (base.DeSerialize(deserializer, fieldname, value)) return true;
            else if (fieldname == "Radius") mRadius = (double)value;
            else if (fieldname == "Interval") mInterval = (int)value;
            else if (fieldname == "LineRotAngleDifference") mAngleDifference = (double)value;
            else if (fieldname == "ShowLines") mShowLines = (bool)value;
            else if (fieldname == "StartPoint") gpStartPoint = value as gPoint;
            else if (fieldname == "EndPoint") gpEndPoint = value as gPoint;
            else return false;
            return true;
        }
        #endregion

        #region Object Figure Calculations
        public override void FillShapeEntities(ref vdEntities entities)
        {
            //vdDocument document = entities.Document;
            //vdCircle circle = new vdCircle();
            //entities.AddItem(circle);
            //circle.MatchProperties(this, Document);
            //circle.Radius = mRadius;
            //circle.HatchProperties = mhatchprops;

            //if (mShowLines)
            //{
            //    gPoint cen = new gPoint();
            //    vdLine line1;
            //    double angle = 0.0;
            //    for (int i = 0; i < 4; i++)
            //    {
            //        line1 = new vdLine();
            //        line1.MatchProperties(this, Document);
            //        if (mhatchprops.FillMode == VdConstFill.VdFillModeNone)
            //        {
            //            line1.LineType = Document.LineTypes.Invisible;
            //        }
            //        line1.StartPoint = new gPoint(gPoint.Polar(cen, angle + mAngle, 3.0d * mRadius / 2.0d));
            //        line1.EndPoint = new gPoint(gPoint.Polar(cen, angle + mAngle, 2.0d * mRadius));
            //        angle += VectorDraw.Geometry.Globals.HALF_PI;
            //        entities.AddItem(line1);
            //    }
            //}

            gPoint EPT, SPT;

            SPT = StartPoint;
            EPT = EndPoint;

            vdLine line1 = new vdLine();
            //line1.SetUnRegisterDocument(document);
            //line1.setDocumentDefaults();
            line1.StartPoint = SPT;
            line1.EndPoint = EPT;
            entities.AddItem(line1);

            vdLine line2 = new vdLine();
            //line2.SetUnRegisterDocument(document);
            //line2.setDocumentDefaults();
            line2.StartPoint = new gPoint(SPT.x, SPT.y - 1, SPT.z);
            line2.EndPoint = new gPoint(EPT.x, EPT.y - 1, EPT.z);
            entities.AddItem(line2);


            ASTRAMemberLoad aline = new ASTRAMemberLoad();
            entities.AddItem(aline);
            aline.InitializeProperties();
            aline.setDocumentDefaults();
            aline.StartPoint = line2.StartPoint;
            aline.EndPoint = line1.StartPoint;
            aline.arrowSize = 0.3;


            double ll = ((line1.Length() / 5.0d) / line1.Length());
            double sx, sy, sz;
            double ex, ey, ez;
            sx = sy = sz = 0.0d;
            for (int j = 1; j <= 5; j++)
            {
                ASTRAMemberLoad aline1 = new ASTRAMemberLoad();
                entities.AddItem(aline1);
                aline1.InitializeProperties();
                aline1.setDocumentDefaults();
                aline1.arrowSize = 0.3;

                sx = (j * ll) * (line2.EndPoint.x - line2.StartPoint.x) + line2.StartPoint.x;
                sy = (j * ll) * (line2.EndPoint.y - line2.StartPoint.y) + line2.StartPoint.y;
                sz = (j * ll) * (line2.EndPoint.z - line2.StartPoint.z) + line2.StartPoint.z;

                aline1.StartPoint = new gPoint(sx, sy, sz);

                ex = (j * ll) * (line1.EndPoint.x - line1.StartPoint.x) + line1.StartPoint.x;
                ey = (j * ll) * (line1.EndPoint.y - line1.StartPoint.y) + line1.StartPoint.y;
                ez = (j * ll) * (line1.EndPoint.z - line1.StartPoint.z) + line1.StartPoint.z;
                aline1.EndPoint = new gPoint(ex, ey, ez);
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
            gpStartPoint = null;
            gpEndPoint = null;
            base.InitializeProperties();
        }
        public override void MatchProperties(VectorDraw.Professional.vdObjects.vdPrimary _from, VectorDraw.Professional.vdObjects.vdDocument thisdocument)
        {
            base.MatchProperties(_from, thisdocument);
            ASTRAMemberLoad_UDL from = _from as ASTRAMemberLoad_UDL;
            if (from == null) return;
            Radius = from.Radius;
            LineRotAngleDifference = from.LineRotAngleDifference;
            Interval = from.Interval;
            ShowLines = from.ShowLines;
            StartPoint = from.StartPoint;
            EndPoint = from.EndPoint;

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
    public class ActionASTRAMemberLoad_UDL : ActionEntity
    {
        private ASTRAMemberLoad_UDL figure;
        public ActionASTRAMemberLoad_UDL(gPoint reference, vdLayout layout)
            : base(reference, layout)
        {
            ValueTypeProp |= valueType.DISTANCE;
            figure = new ASTRAMemberLoad_UDL();
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
        public static void CmdASTRAMemberLoad_UDL(vdDocument doc)
        {
            gPoint cen = new gPoint();
            doc.Prompt("Start Point : ");
            object ret = doc.ActionUtility.getUserPoint();
            doc.Prompt(null);
            if (ret == null || !(ret is gPoint)) goto error;
            cen = ret as gPoint;
            
            gPoint cen1 = new gPoint();
            doc.Prompt("End Point : ");
            ret = doc.ActionUtility.getUserPoint();
            doc.Prompt(null);
            if (ret == null || !(ret is gPoint)) goto error;
            cen1 = ret as gPoint;

            ASTRAMemberLoad_UDL udl = new ASTRAMemberLoad_UDL();
            udl.StartPoint = cen;
            udl.EndPoint = cen1;
            udl.SetUnRegisterDocument(doc);
            udl.setDocumentDefaults();
            doc.ActionLayout.Entities.AddItem(udl);
            doc.Redraw(true);
        error:
            return;
        }
    }
}
