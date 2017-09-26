using System;
using System.Collections.Generic;
using System.Text;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Geometry;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Generics;
using VectorDraw.Render;
using VectorDraw.Professional.Actions;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Serialize;

//namespace HEADSNeed
//{
//    public class HEADSEntities
//    {
//    }
//    class VectorDrawSimpleRect : vdCurve, IvdProxyFigure, IvdProxySerializer, IVDSerialise
//    {
//        #region variables
//        private gPoint mInsertionPoint = null;
//        private double mHeight = 0.1;
//        private double mWidth = 0.1;
//        private double mRotation = 0.0;
//        private gPoints mRectSamplePoints = null;
//        private MyObject mobj1 = new MyObject();
//        private MyObject2 mobj2 = new MyObject2();
//        #endregion

//        #region ctors
//        public VectorDrawSimpleRect()
//            : base()
//        {
//        }
//        #endregion

//        #region Properties
//        //Please note that in CustomObjects project at the Load of the Form(Form1.cs) these types are being added to the ActiveDocument.ProxyClasses collection
//        public MyObject obj1
//        {
//            get { return mobj1; }
//            set { mobj1 = value; }
//        }
//        //Please note that in CustomObjects project at the Load of the Form these types are being added to the ActiveDocument.ProxyClasses collection
//        public MyObject2 obj2
//        {
//            get { return mobj2; }
//            set { mobj2 = value; }
//        }

//        /// <summary>
//        /// Gets the Bounding Box of the VectorDrawSimpleRect object in WorldCoordinate System.Used in draw,Invalidate calculations.
//        /// </summary>
//        public override Box BoundingBox
//        {
//            get
//            {
//                if (mBoundBox.IsEmpty)
//                {
//                    mBoundBox = new Box();
//                    mBoundBox.AddPoint(new gPoint());
//                    mBoundBox.AddPoint(new gPoint(this.Width, this.Height, 0.0d));
//                    mBoundBox.AddWidth(System.Math.Max(this.PenWidth, 0.0) / 2.0d);
//                    mBoundBox.ExpandBy(Thickness);
//                    mBoundBox.TransformBy(ECSMatrix);
//                }
//                return mBoundBox;
//            }
//        }
//        /// <summary>
//        /// Get/Set the lower left point of the VectorDrawSimpleRect in Woorld Coordinate System(WCS).
//        /// </summary>
//        /// <remarks>
//        /// Default value (0.0,0.0,0.0).
//        /// </remarks>
//        public gPoint InsertionPoint
//        {
//            get { if (mInsertionPoint == null)mInsertionPoint = new gPoint(); return mInsertionPoint; }
//            set
//            {
//                if (!RaiseOnBeforeModify("InsertionPoint", value)) return;
//                AddHistory("InsertionPoint", value);
//                InsertionPoint.CopyFrom(value);
//                RaiseOnAfterModify("InsertionPoint");
//            }
//        }

//        /// <summary>
//        /// Get/Set the width of the VectorDrawSimpleRect in drawing units.
//        /// </summary>
//        /// <remarks>
//        /// Default value 0.1.
//        /// </remarks>
//        public double Width
//        {
//            get { return mWidth; }
//            set
//            {
//                if (!RaiseOnBeforeModify("Width", value)) return;
//                AddHistory("Width", value);
//                mWidth = value;
//                RaiseOnAfterModify("Width");
//            }
//        }

//        /// <summary>
//        /// Get/Set the height of the VectorDrawSimpleRect in drawing units.
//        /// </summary>
//        /// <remarks>
//        /// Default value 0.1.
//        /// </remarks>
//        public double Height
//        {
//            get { return mHeight; }
//            set
//            {
//                if (!RaiseOnBeforeModify("Height", value)) return;
//                AddHistory("Height", value);
//                mHeight = value;
//                RaiseOnAfterModify("Height");
//            }
//        }

//        /// <summary>
//        /// Get/Set the vertical rotation angle in radians of the VectorDrawSimpleRect.
//        /// </summary>
//        /// <remarks>
//        /// Default value 0.0.
//        /// </remarks>
//        public double Rotation
//        {
//            get { return mRotation; }
//            set
//            {
//                if (!RaiseOnBeforeModify("Rotation", value)) return;
//                AddHistory("Rotation", value);
//                mRotation = value;
//                RaiseOnAfterModify("Rotation");
//            }
//        }
//        #endregion

//        #region Methods
//        /// <summary>
//        /// Indicates if at least a segment of the object is visible after applying the SectionClips in 3dRender mode.
//        /// </summary>
//        /// <param name="object2World">Represents the object to world coordinate system matrix.</param>
//        /// <returns>True if the object is visible after applying the sections</returns>
//        public override bool Is3dSectionVisible(Matrix object2World)
//        {
//            if (!IsVisible()) return false;
//            if (Document == null || !Document.ActiveRender.SupportSectionClips) return true;
//            vdSectionClips sclips = Document.GetActiveSections();
//            vdRender render = Document.ActiveRender;
//            mSamplePoints = GetSamplePoints(render.CurveResolution, render.CurveResPixelSize);//6009  //60000254
//            gPoints pts = new gPoints();
//            pts.AddRange(mSamplePoints);
//            return sclips.IsSectionVisible(pts, (object2World == null ? ECSMatrix : (ECSMatrix * object2World)));
//        }
//        /// <summary>
//        /// Tests if a point is inside a VectorDrawSimpleRect or not.
//        /// </summary>
//        /// <param name="pt">The point with which the test will be commited in World Coordinate System(WCS).</param>
//        /// <returns>Returns -1.0 if the point is inside the VectorDrawSimpleRect and 1.0 if it is outside.</returns>
//        public override double TestOffsetSide(gPoint pt)
//        {
//            if (this.BoundingBox.PointInBox(pt))
//                return -1.0;
//            else
//                return 1.0;
//        }
//        /// <summary>
//        /// Creates a new VectorDrawSimpleRect object at a specified distance from the existing rectangle.
//        /// </summary>
//        /// <param name="offsetDist">The distance where the new VectorDrawSimpleRect object will be created.</param>
//        /// <returns>The new created VectorDrawSimpleRect object.An empty collection is returned if the height and width of the new object is less or equal to 0.0 .</returns>
//        public override VectorDraw.Professional.vdCollections.vdCurves getOffsetCurve(double offsetDist)
//        {
//            vdCurves ret = new vdCurves();
//            if (!((Math.Abs(offsetDist) < Math.Abs(this.Height)) && (Math.Abs(offsetDist) < Math.Abs(this.Width)))) return ret;

//            VectorDrawSimpleRect newRect = new VectorDrawSimpleRect();
//            newRect.SetUnRegisterDocument(this.Document);
//            newRect.MatchProperties(this, null);

//            Box box = new Box();
//            box.AddPoint(new gPoint());
//            box.AddPoint(new gPoint(Width, Height));
//            newRect.InsertionPoint = box.Min;
//            newRect.Width = box.Width;
//            newRect.Height = box.Height;
//            newRect.Rotation = 0.0d;

//            newRect.InsertionPoint.x += (-1.0) * offsetDist;
//            newRect.InsertionPoint.y += (-1.0) * offsetDist;
//            newRect.Height += 2.0 * offsetDist;
//            newRect.Width += 2.0 * offsetDist;

//            newRect.Transformby(this.ECSMatrix);

//            ret.AddItem(newRect);
//            return ret;
//        }
//        public override void GetTableDependecies(vdLayers layers, vdBlocks blocks, vdDimstyles dimstyles, vdLineTypes linetypes, vdTextstyles textstyles, vdImages images, vdHatchPatterns hatchpatterns, object breakOnObject, ref bool breakObjectFound)
//        {
//            //Since there no extra dependencies the base call is enough.
//            base.GetTableDependecies(layers, blocks, dimstyles, linetypes, textstyles, images, hatchpatterns, breakOnObject, ref breakObjectFound);
//        }

//        /// <summary>
//        /// Calculates the enclosed area of the VectorDrawSimpleRect object in Drawing Units.
//        /// </summary>
//        /// <returns>Returns the enclosed area.</returns>
//        public override double Area()
//        {
//            return Width * Height;
//        }

//        /// <summary>
//        /// Calculates the length of the VectorDrawSimpleRect object.
//        /// </summary>
//        /// <returns>Returns the length of the object.</returns>
//        public override double Length()
//        {
//            return 2.0d * Width + 2.0d * Height;
//        }
//        #endregion

//        //------------------------------------------About PARAM------------------------------------------------------------------------
//        //The Param methods are used in Trim,Break ,Devide , Measure. The param has to be relative to the length of the object in this case (rect). We could
//        //have set the param as relative to something else that could divide the object into equal (relative) pieces like we have done in our vdArc object where 
//        //we used as param not the length but the angle. In most cases the length is used.

//        //-----------------------------------------------------------------------------------------------------------------------------

//        /// <summary>
//        /// Get a value representing the start of the VectorDrawSimpleRect object.
//        /// </summary>
//        /// <returns>Returns 0.0.</returns>
//        public override double getFirstOffsetParam()
//        {
//            return 0.0;
//        }
//        /// <summary>
//        /// Returns the start parameter for the VectorDrawSimpleRect object.
//        /// </summary>
//        /// <returns>Returns the start parameter for the VectorDrawSimpleRect object equal to 0.0.</returns>
//        public override double getStartParam()
//        {
//            return 0.0d;
//        }
//        /// <summary>
//        /// Return the end parameter for the VectorDrawSimpleRect object.
//        /// </summary>
//        /// <returns>Return the end parameter for the VectorDrawSimpleRect object equal to it's length.</returns>
//        public override double getEndParam()
//        {
//            return Length();
//        }

//        /// <summary>
//        /// Get the first point of the VectorDrawSimpleRect object(lower left) in World Coordinate System(WCS).
//        /// </summary>
//        /// <returns>The start point of the VectorDrawSimpleRect object in World Coordinate System(WCS).</returns>
//        public override gPoint getStartPoint()
//        {
//            gPoint pt = new gPoint();
//            return ECSMatrix.Transform(pt);//return point in WCS
//        }

//        /// <summary>
//        /// Get the last point of the VectorDrawSimpleRect object(lower left) in World Coordinate System(WCS).
//        /// </summary>
//        /// <returns>The last point of the VectorDrawSimpleRect object in World Coordinate System(WCS) equals to the start point.</returns>
//        public override gPoint getEndPoint()
//        {
//            gPoint pt = new gPoint();
//            return ECSMatrix.Transform(pt);//return point in WCS
//        }

//        /// <summary>
//        /// Get the length of the VectorDrawSimpleRect’s segment from the VectorDrawSimpleRect’s beginning to the point specified by param.
//        /// </summary>
//        /// <param name="param">A value that represents a point on the VectorDrawSimpleRect.</param>
//        /// <returns>The distance from the getStartParam.</returns>
//        public override double getDistAtParam(double param)
//        {
//            return param;
//        }

//        /// <summary>
//        /// Gets the parameter of the object at the specific distance.
//        /// </summary>
//        /// <param name="dist">The distance that the param is needed.</param>
//        /// <returns>Returns parameter at the location specified by dist.</returns>
//        /// <remarks>
//        /// If the distance is 0.0 then the <see cref="getStartParam"/> is returned.
//        /// If the distance is equal to the length of the object then the <see cref="getEndParam"/> is returned.
//        /// </remarks>
//        public override double getParamAtDist(double dist)
//        {
//            return dist;
//        }

//        /// <summary>
//        /// Get the length of the VectorDrawSimpleRect’s segment between the VectorDrawSimpleRect’s start point and point pt.
//        /// </summary>
//        /// <param name="pt">The point of the VectorDrawSimpleRect in World Coordinate System(WCS).</param>
//        /// <returns>Returns the length of the VectorDrawSimpleRect’s segment between the VectorDrawSimpleRect’s start point and point.</returns>
//        /// <seealso cref="getEndParam"/>
//        /// <seealso cref="getStartParam"/>
//        public override double getDistAtPoint(gPoint pt)
//        {
//            return getParamAtPoint(pt);
//        }

//        /// <summary>
//        /// Gets a point belonging to the VectorDrawSimpleRect at the specified distance.
//        /// </summary>
//        /// <param name="dist">The distance along the VectorDrawSimpleRect.</param>
//        /// <returns>Returns the point located at the location specified by dist</returns>
//        /// <seealso cref="getEndParam"/>
//        /// <seealso cref="getStartParam"/>
//        public override gPoint getPointAtDist(double dist)
//        {
//            return getPointAtParam(dist);
//        }
//        /// <summary>
//        /// Returns a point on the VectorDrawSimpleRect representing a param in WCS.
//        /// </summary>
//        /// <param name="param">The param of the VectorDrawSimpleRect object.</param>
//        /// <returns>Returns the point on the VectorDrawSimpleRect specified by the param in World Coordinate System(WCS).</returns>
//        /// <seealso cref="getEndParam"/>
//        /// <seealso cref="getStartParam"/>
//        public override gPoint getPointAtParam(double param)
//        {
//            if (!IsParamValid(param)) throw new Exception("Wrong input param.");
//            if (Globals.AreEqual(param, getStartParam(), Globals.VD_ZERO8)) return getStartPoint();
//            if (Globals.AreEqual(param, getEndParam(), Globals.VD_ZERO8)) return getEndPoint();
//            gPoints pts = GetSamplePoints(128, 0.0d);
//            gPoint pt = pts.getPointAtDist2d(param);
//            return ECSMatrix.Transform(pt);//return point in WCS
//        }

//        /// <summary>
//        /// Checks if the param is between the getStartParam and the getEndParam.
//        /// </summary>
//        /// <param name="param">The parameter used for the check.</param>
//        /// <returns>true if the param is between these two values.</returns>
//        public override bool IsParamValid(double param)
//        {
//            return (Globals.AreEqual(param, getStartParam(), Globals.VD_ZERO8) ||
//                Globals.AreEqual(param, getEndParam(), Globals.VD_ZERO8) ||
//                (param >= getStartParam() && param <= getEndParam())
//            );
//        }

//        /// <summary>
//        /// Get the parameter of the VectorDrawSimpleRect at a specified point given in WCS.
//        /// </summary>
//        /// <param name="pt">The point in World Coordinate System(WCS) belonging to the VectorDrawSimpleRect.</param>
//        /// <returns>Returns the parameter of the VectorDrawSimpleRect at point pt.</returns>
//        /// <seealso cref="getEndParam"/>
//        /// <seealso cref="getStartParam"/>
//        public override double getParamAtPoint(gPoint pt)
//        {
//            gPoint p = ECSMatrix.GetInvertion().Transform(pt);
//            if (!PointOnCurve(p, true)) throw new Exception("Wrong input param.");
//            gPoints pts = GetSamplePoints(128, 0.0d);
//            double param = pts.getDistAtPoint2d(p);
//            if (!IsParamValid(param)) throw new Exception("Wrong input param.");
//            return param;
//        }

//        /// <summary>
//        /// Returns if a point belongs to a VectorDrawSimpleRect object.
//        /// </summary>
//        /// <param name="pt">A point in WCS or ECS.</param>
//        /// <param name="IsInECS">A boolean value indicating if the point is in ECS or WCS.</param>
//        /// <returns>Returns true if the point belongs to the VectorDrawSimpleRect object.</returns>
//        public override bool PointOnCurve(gPoint pt, bool IsInECS)
//        {
//            if (IsInECS)
//            {
//                gPoints pts = GetSamplePoints(128, 0.0d);
//                return pts.PointOnThis2d(pt);
//            }
//            else
//            {
//                gPoint p = ECSMatrix.GetInvertion().Transform(pt);
//                return PointOnCurve(p, true);
//            }
//        }

//        /// <summary>
//        /// Matches all the common properties from an object and sets them to this object.
//        /// </summary>
//        /// <param name="_from">The object from which the properties values will be read.</param>
//        /// <param name="thisdocument">The vdDocument object where this object belongs.</param>
//        /// <remarks >
//        /// If thisdocument parameter is null then the from's object Document is used instead.
//        /// This is a very important method that is used internally by VectorDraw engine to many methods of the object 
//        /// and it should contain all the properties of the object to be matched.
//        /// </remarks>
//        public override void MatchProperties(VectorDraw.Professional.vdObjects.vdPrimary _from, VectorDraw.Professional.vdObjects.vdDocument thisdocument)
//        {
//            base.MatchProperties(_from, thisdocument);
//            VectorDrawSimpleRect from = _from as VectorDrawSimpleRect;
//            if (from == null) return;
//            InsertionPoint = from.InsertionPoint;
//            Width = from.Width;
//            Height = from.Height;
//            Rotation = from.Rotation;
//        }
//        /// <summary>
//        /// This function is intended to be called during a grip edit of the object. 
//        /// </summary>
//        /// <param name="Indexes">Represents a zero based array of the grip indexes that will be moved.</param>
//        /// <param name="dx">The dx distance that the grip is moved.</param>
//        /// <param name="dy">The dy distance that the grip is moved.</param>
//        /// <param name="dz">The dz distance that the grip is moved.</param>
//        public override void MoveGripPointsAt(Int32Array Indexes, double dx, double dy, double dz)
//        {
//            if (Indexes == null || Indexes.Count == 0 || (dx == 0.0 && dy == 0.0 && dz == 0.0)) return;
//            if (RaiseOnMoveGripPointsAt(Indexes, dx, dy, dz)) return;
//            Matrix mat = new Matrix();
//            mat.TranslateMatrix(dx, dy, dz);
//            gPoints grips = GetGripPoints();
//            ECSMatrix.GetInvertion().Transform(grips);
//            if (Indexes.Count == grips.Count)
//            {

//                Transformby(mat);
//            }
//            else
//            {
//                foreach (int index in Indexes)
//                {
//                    switch (index)
//                    {
//                        case 0:
//                            Transformby(mat);
//                            break;
//                        case 1:
//                            gPoint grip = new gPoint(grips[index]);
//                            mat.IdentityMatrix();
//                            mat.RotateZMatrix(-this.Rotation);
//                            gPoint offset = new gPoint(dx, dy, dz);
//                            offset = mat.Transform(offset);
//                            grip += offset;
//                            this.Width = grip.x;
//                            this.Height = grip.y;
//                            break;
//                        default:
//                            break;
//                    }
//                }
//            }
//            Update();
//        }

//        /// <summary>
//        /// A function returning the grip points of the object.
//        /// </summary>
//        /// <returns>Returns a new instance of gpoints object containing the grip points of the object.</returns>
//        /// <remarks>
//        /// The returning points are in World coordinate system(WCS).
//        /// </remarks>
//        public override gPoints GetGripPoints()
//        {
//            gPoints ret = new gPoints();
//            if (RaiseOnGetGripPoints(ret)) return ret;
//            gPoint ins = new gPoint();
//            ret.Add(ins);
//            ret.Add(new gPoint(ins.x + Width, ins.y + Height, ins.z));
//            ECSMatrix.Transform(ret);
//            return ret;
//        }
//        /// <summary>
//        /// Resets the properties of the VectorDrawSimpleRect object to the Default values.
//        /// </summary>
//        /// <remarks>
//        /// Default values: InsertionPoint set to (0.0,0.0,0.0) , Width = Height = 1, Rotation = 0.
//        /// If the object is Document Registered then it's common properties with the Active Document's properties
//        /// will be overriden.
//        /// </remarks>
//        public override void InitializeProperties()
//        {
//            InsertionPoint.CopyFrom(new gPoint());
//            mWidth = 1.0d;
//            mHeight = 1.0d;
//            mRotation = 0.0d;
//            base.InitializeProperties();
//        }
//        /// <summary>
//        /// Gets a System.String that represents the type of the VectorDrawSimpleRect Object.
//        /// </summary>
//        public override string ToString()
//        {
//            return this.GetType().Name;
//        }
//        /// <summary>
//        /// Returns a list of points on the VectorDrawSimpleRect object from the start to the end point of the curve.
//        /// </summary>
//        /// <param name="curveResolution">valid values (1 to 20000)</param>
//        /// <param name="pixelSize">valid values possitive decimal ( >=0)</param>
//        /// <returns>A collection of points belonging to the VectorDrawSimpleRect in Entity Coordinate System(ECS).</returns>
//        /// <seealso cref="vdCurve.GetSamplePoints"/>
//        public override gPoints GetSamplePoints(int CurveResolution, double PixelSize)
//        {
//            if (mRectSamplePoints != null) return mRectSamplePoints;
//            else
//            {
//                mRectSamplePoints = new gPoints();
//                mRectSamplePoints.Add(new gPoint());
//                mRectSamplePoints.Add(new gPoint(Width, 0.0d));
//                mRectSamplePoints.Add(new gPoint(Width, Height));
//                mRectSamplePoints.Add(new gPoint(0.0d, Height));
//                mRectSamplePoints.Add(new gPoint());
//                mRectSamplePoints.Reverse();

//            }
//            return mRectSamplePoints;
//        }
//        /// <summary>
//        /// Gets the Top Left point of the VectorDrawSimpleRect in ECS Coordinate System.
//        /// This method is to help the calculations in Draw Method.
//        /// </summary>
//        /// <returns></returns>
//        public gPoint GetTopLeftPoint()
//        {
//            return new gPoint(Width, 0.0);
//        }

//        /// <summary>
//        /// Gets the Top Right point of the VectorDrawSimpleRect in ECS Coordinate System.
//        /// This method is to help the calculations in Draw Method.
//        /// </summary>
//        /// <returns></returns>
//        public gPoint GetTopRightPoint()
//        {
//            return new gPoint(Width, Height);
//        }

//        /// <summary>
//        /// Gets the Bottom Right point of the VectorDrawSimpleRect in ECS Coordinate System.
//        /// This method is to help the calculations in Draw Method.
//        /// </summary>
//        /// <returns></returns>
//        public gPoint GetBottomRightPoint()
//        {
//            return new gPoint(0.0d, Height);
//        }

//        /// <summary>
//        /// Gets the Middle point of the VectorDrawSimpleRect in ECS Coordinate System.
//        /// This method is to help the calculations in Draw Method.
//        /// </summary>
//        /// <returns></returns>
//        public gPoint GetMiddlePoint()
//        {
//            return new gPoint(Width / 2.0, Height / 2.0);
//        }

//        /// <summary>
//        /// Updates the object after change of properties.
//        /// </summary>
//        /// <remarks >
//        /// Call this method after changing properties that influences the geometry of the object. The update method
//        /// will force other properties to be recalculated (e.g Bounding Box).
//        /// </remarks>
//        public override void Update()
//        {
//            mRectSamplePoints = null;
//            base.Update();
//        }
//        /// <summary>
//        /// Gets the matrix which is used to transform vectors or points from the 
//        /// object's object coordinate system (OCS) to the World Coordinate System (WCS).
//        /// </summary>
//        public override Matrix ECSMatrix
//        {
//            get
//            {
//                if (mEcsMatrix != null) return mEcsMatrix;
//                mEcsMatrix = new Matrix();
//                mEcsMatrix.RotateZMatrix(this.Rotation);
//                mEcsMatrix.ApplyECS2WCS(ExtrusionVector);
//                mEcsMatrix.TranslateMatrix(this.InsertionPoint.x, this.InsertionPoint.y, this.InsertionPoint.z);
//                return mEcsMatrix;
//            }
//        }
//        /// <summary>
//        /// Transforms all the geometrical properties of the object with the specific Matrix mat.
//        /// </summary>
//        /// <param name="mat">The Matrix with which the transformation will be done.</param>
//        public override void Transformby(Matrix mat)
//        {
//            if (mat.IsUnitMatrix()) return;
//            Matrix mult = (ECSMatrix * mat);
//            MatrixProperties matprops = mult.Properties;
//            InsertionPoint = mat.Transform(InsertionPoint);
//            Width *= matprops.Scales.x;
//            Height *= matprops.Scales.y;
//            Rotation = matprops.Zangle;
//            base.Transformby(mat);
//        }

//        /// <summary>
//        /// Explodes the VectorDrawSimpleRect object into a vdPolyline object.
//        /// </summary>
//        /// <returns>A collection containing the vdPolyline object created from the explode method.</returns>
//        public override vdEntities Explode()
//        {
//            vdEntities Entities = new vdEntities();
//            Entities.SetUnRegisterDocument(Document);
//            if (Document != null) Document.UndoHistory.PushEnable(false);

//            vdPolyline pl = new vdPolyline();
//            pl.MatchProperties(this, null);
//            pl.VertexList = new Vertexes();
//            gPoints samplePoints = GetSamplePoints(0, 0.0d);
//            foreach (gPoint var in samplePoints)
//            {
//                pl.VertexList.Add(new Vertex(var));
//            }
//            pl.VertexList.RemoveAt(pl.VertexList.Count - 1);
//            pl.Flag = VectorDraw.Professional.Constants.VdConstPlineFlag.PlFlagCLOSE;
//            pl.Transformby(ECSMatrix);
//            Entities.AddItem(pl);

//            vdLine line1 = new vdLine();
//            line1.MatchProperties(this, null);
//            line1.StartPoint = new gPoint();
//            line1.EndPoint = GetTopRightPoint();
//            line1.Transformby(ECSMatrix);
//            Entities.AddItem(line1);

//            vdLine line2 = new vdLine();
//            line2.MatchProperties(this, null);
//            line2.StartPoint = GetTopLeftPoint();
//            line2.EndPoint = GetBottomRightPoint();
//            line2.Transformby(ECSMatrix);
//            Entities.AddItem(line2);

//            if (Document != null) Document.UndoHistory.PopEnable();
//            return Entities;
//        }

//        /// <summary>
//        /// Finds the intersection points of an object with another object.
//        /// </summary>
//        /// <param name="pEntity">The entity with which the intersection of the object will be done.</param>
//        /// <param name="intersectOption"><see cref="VdConstInters"/> constant</param>
//        /// <param name="intPoints">A precreated gPoints collection in which the instersection points will be added.The points are returned in WCS.</param>
//        /// <returns>True if at least one intersection point is found.</returns>
//        public override bool IntersectWith(VectorDraw.Professional.vdPrimaries.vdFigure pEntity, VectorDraw.Professional.Constants.VdConstInters intersectOption, gPoints intPoints)
//        {
//            vdPolyline pl = new vdPolyline();
//            pl.MatchProperties(this, null);
//            pl.VertexList = new Vertexes();
//            gPoints samplePoints = GetSamplePoints(0, 0.0d);
//            foreach (gPoint var in samplePoints)
//            {
//                pl.VertexList.Add(new Vertex(var));
//            }
//            pl.VertexList.RemoveAt(pl.VertexList.Count - 1);
//            pl.Flag = VectorDraw.Professional.Constants.VdConstPlineFlag.PlFlagCLOSE;
//            pl.Transformby(ECSMatrix);

//            bool ret = pl.IntersectWith(pEntity, intersectOption, intPoints);
//            pl = null;
//            return ret;
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="object2viewcs"></param>
//        /// <param name="mode"></param>
//        /// <param name="pickPoi"></param>
//        /// <param name="LastPoi"></param>
//        /// <param name="SegCount"></param>
//        /// <param name="osnaps"></param>
//        /// <returns></returns>
//        public override bool getOsnapPoints(Matrix object2viewcs, OsnapMode mode, gPoint pickPoi, gPoint LastPoi, int SegCount, OsnapPoints osnaps)
//        {
//            if (mode == OsnapMode.NONE) return false;
//            int count = osnaps.Count;
//            if (SegCount == -1 || SegCount > 3) return false;
//            gPoint p1 = null;
//            gPoint p2 = null;
//            mRectSamplePoints = GetSamplePoints(0, 0.0);
//            switch (SegCount)
//            {
//                case 0:
//                    p1 = new gPoint(mRectSamplePoints[0]);
//                    p2 = new gPoint(mRectSamplePoints[1]);
//                    break;
//                case 1:
//                    p1 = new gPoint(mRectSamplePoints[1]);
//                    p2 = new gPoint(mRectSamplePoints[2]);
//                    break;
//                case 2:
//                    p1 = new gPoint(mRectSamplePoints[2]);
//                    p2 = new gPoint(mRectSamplePoints[3]);
//                    break;
//                case 3:
//                    p1 = new gPoint(mRectSamplePoints[3]);
//                    p2 = new gPoint(mRectSamplePoints[4]);
//                    break;
//                default:
//                    return false;
//            }
//            if (Document != null) Document.UndoHistory.PushEnable(false);
//            vdLine line = new vdLine();
//            line.StartPoint = ECSMatrix.Transform(p1);
//            line.EndPoint = ECSMatrix.Transform(p2);
//            Matrix mat = new Matrix(ECSMatrix.GetInvertion());
//            mat.Multiply(object2viewcs);
//            if (Document != null) Document.UndoHistory.PopEnable();
//            line.getOsnapPoints(mat, mode, pickPoi, LastPoi, -1, osnaps);
//            line = null;

//            return (osnaps.Count != count);
//        }

//        /// <summary>
//        /// Converts the VectorDrawSimpleRect into a vdPolyface object.
//        /// </summary>
//        /// <param name="CurveResolution">Determines the number of the segments that the rect will be devided.</param>
//        /// <returns>The created vdPolyface object which is not being added to the Document.</returns>
//        public override vdPolyface ToMesh(int CurveResolution)
//        {
//            gPoints pts = this.GetSamplePoints(CurveResolution, 0.0);
//            vdPolyface ret = new vdPolyface();
//            ret.SetUnRegisterDocument(this.Document);
//            ret.setDocumentDefaults();
//            vdArray<gPoints> tmp = new vdArray<gPoints>();
//            tmp.AddItem(pts);
//            VectorDraw.Geometry.GpcWrapper.ClippingPolyFace pFace = Globals.ConvertToMesh(tmp, this.Thickness, false, false);
//            if (pFace == null) return null;
//            foreach (VectorDraw.Geometry.Vertex vert in pFace.vertices)
//                ret.VertexList.Add(vert.VertexPoint);
//            ret.FaceList.AddRange(pFace.facelist);
//            ret.ClearVerticies();
//            ret.Transformby(this.ECSMatrix);
//            ret.MatchProperties(this, null);
//            return ret;
//        }

//        /// <summary>
//        /// Draw of the object.
//        /// </summary>
//        /// <param name="render"></param>
//        /// <returns></returns>
//        public override vdRender.DrawStatus Draw(vdRender render)
//        {
//            vdRender.DrawStatus doDraw = base.Draw(render);
//            if (doDraw == vdRender.DrawStatus.Successed)
//            {
//                mRectSamplePoints = GetSamplePoints(render.CurveResolution, render.CurveResPixelSize);//6009 //60000254
                
//                render.PushMatrix(ECSMatrix);
//                render.DrawPLine(this, mRectSamplePoints, Thickness);
//                render.DrawLine(this, GetTopLeftPoint(), GetBottomRightPoint());
//                render.DrawLine(this, new gPoint(), GetTopRightPoint());
//                render.PopMatrix();
//            }
//            AfterDraw(render);
//            return doDraw;
//        }

//        #region IVDSerialise Members
//        /// <summary>
//        /// This Function is called when saving the VectorDrawSimpleRect object to vdml format.
//        /// </summary>
//        /// <param name="serializer">The Serializer object.</param>
//        public override void Serialize(VectorDraw.Serialize.Serializer serializer)
//        {
//            base.Serialize(serializer);
//            serializer.Serialize(mInsertionPoint, "InsertionPoint");
//            serializer.Serialize(mWidth, "Width");
//            serializer.Serialize(mHeight, "Height");
//            if (mRotation != 0.0d) serializer.Serialize(mRotation, "Rotation");
//            serializer.Serialize(obj1, "obj1");
//            serializer.Serialize(obj2, "obj2");
//        }
//        /// <summary>
//        /// This Function is called foreach field name of the VectorDrawSimpleRect object when opening in vdml format.
//        /// </summary>
//        /// <param name="deserializer">The DeSerializer object.</param>
//        /// <param name="fieldname">The name of the property of the object.</param>
//        /// <param name="value">the value of the property.</param>
//        /// <returns>Returns False if the fieldname does not correspond to a property of the object.</returns>
//        public override bool DeSerialize(VectorDraw.Serialize.DeSerializer deserializer, string fieldname, object value)
//        {
//            if (base.DeSerialize(deserializer, fieldname, value)) return true;
//            else if (fieldname == "InsertionPoint") mInsertionPoint = (gPoint)value;
//            else if (fieldname == "Width") mWidth = (double)value;
//            else if (fieldname == "Height") mHeight = (double)value;
//            else if (fieldname == "Rotation") mRotation = (double)value;
//            else if (fieldname == "obj1") obj1 = (MyObject)value;
//            else if (fieldname == "obj2") obj2 = (MyObject2)value;
//            else return false;
//            return true;
//        }
//        #endregion
//    }
//    public class ActionVectorDrawSimpleRect : ActionEntity
//    {
//        private VectorDrawSimpleRect figure;
//        public ActionVectorDrawSimpleRect(gPoint reference, vdLayout layout)
//            : base(reference, layout)
//        {
//            ValueTypeProp |= valueType.DISTANCE;
//            figure = new VectorDrawSimpleRect();
//            figure.SetUnRegisterDocument(layout.Document);
//            figure.setDocumentDefaults();
//            figure.InsertionPoint = reference;
//        }
//        protected override void OnMyPositionChanged(gPoint newPosition)
//        {
//            gPoint pt = figure.ECSMatrix.GetInvertion().Transform(newPosition);
//            figure.Width = pt.x;
//            figure.Height = pt.y;
//        }
//        public override vdFigure Entity
//        {
//            get
//            {
//                return figure;
//            }
//        }

//        public override bool HideRubberLine
//        {
//            get
//            {
//                return true;
//            }
//        }

//        public static void CmdVectorDrawSimpleRect(vdDocument doc)
//        {
//            gPoint inspt = new gPoint();
//            doc.Prompt("Origin-Center Point : ");
//            object ret = doc.ActionUtility.getUserPoint();
//            doc.Prompt(null);
//            if (ret == null || !(ret is gPoint)) goto error;
//            inspt = ret as gPoint;
//            ActionVectorDrawSimpleRect aFig = new ActionVectorDrawSimpleRect(inspt, doc.ActiveLayOut);
//            doc.ActionAdd(aFig);
//            VectorDraw.Actions.StatusCode scode = aFig.WaitToFinish();
//            doc.Prompt(null);
//            if (scode != VectorDraw.Actions.StatusCode.Success) goto error;
//            aFig.Entity.Transformby(doc.User2WorldMatrix);

//            VectorDrawSimpleRect rect = aFig.Entity as VectorDrawSimpleRect;

//            //At the beggining we add an Xproperty to the object containing MyObject2 object
//            VectorDraw.Professional.vdObjects.vdXProperty xprop = new VectorDraw.Professional.vdObjects.vdXProperty();
//            xprop.Name = "My_Object2";
//            MyObject2 xpropobj = new MyObject2();
//            xpropobj.Double1 = 3.0d;
//            xpropobj.Text1 = "three";
//            xprop.PropValue = xpropobj;
//            rect.XProperties.AddItem(xprop);

//            //Also set the obj1 and obj2 values
//            rect.obj1 = new MyObject(2.0, "Two");

//            rect.obj2 = new MyObject2();
//            rect.obj2.Double1 = 6.0;
//            rect.obj2.Text1 = "Six";

            

//            doc.ActionLayout.Entities.AddItem(aFig.Entity);
//            doc.ActionDrawFigure(aFig.Entity);
//            return;
//        error:
//            return;
//        }
//    }
//    #region My_Object
//    public class MyObject2 : MyObject
//    {
//        public MyObject2() { }
//        public override void Serialize(Serializer serializer)
//        {
//            base.Serialize(serializer);
//        }
//        public override bool DeSerialize(DeSerializer deserializer, string fieldname, object value)
//        {
//            return base.DeSerialize(deserializer, fieldname, value);
//        }
//    }
//    public class MyObject : IvdProxySerializer
//    {
//        private double mDouble1 = 1.0d;
//        private string mText1 = "one";
//        public MyObject() { } // Need an EMPTY CONSTRUCTOR

//        public MyObject(double dbl, string str)
//        {
//            mDouble1 = dbl;
//            mText1 = str;
//        }
//        public double Double1
//        {
//            get
//            {
//                return mDouble1;
//            }
//            set
//            {
//                mDouble1 = value;
//            }
//        }
//        public string Text1
//        {
//            get
//            {
//                return mText1;
//            }
//            set
//            {
//                mText1 = value;
//            }
//        }
//        public override string ToString()
//        {
//            return "MyObject with value " + mDouble1.ToString() + " " + mText1;
//        }
//        public virtual bool DeSerialize(DeSerializer deserializer, string fieldname, object value)
//        {
//            if (fieldname == "Double1") mDouble1 = (double)value;
//            else if (fieldname == "Text1") mText1 = (string)value;
//            else return false;
//            return true;
//        }
//        public virtual void Serialize(Serializer serializer)
//        {
//            serializer.Serialize(mText1, "Text1");
//            serializer.Serialize(mDouble1, "Double1");
//        }
//    }
//    #endregion
//}
