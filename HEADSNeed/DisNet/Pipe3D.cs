using System;
using System.Drawing;
using System.Windows.Forms;

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ComponentModel;
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
using System.IO;

namespace HEADSNeed
{
    public class Pipe3D :vdShape
    {
        #region main variables

        private gPoint gpStartPoint = null;
        private gPoint gpEndPoint = null;
        private double dPipeHeight;
        private vdCircle cirStart = null;
        private vdCircle cirEnd = null;

        
        private vdTextstyle mTextStyle = null;
        private int mNumSides = 4;
        private string mText = "";
        private double mTextHeight = 1.0d;
        private double mRadius = 1.0d;





        #endregion
        #region ctor
        public Pipe3D()
        {

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
        #region properties
        //You should use the AddHistory command in your properties so any change will be writen to the history
        //The string in the AddHistory command is the name of the property exactly.
        public double PipeHeight
        {
            get
            {
                return dPipeHeight;
            }
            set
            {
                AddHistory("PipeHeight",value);
                dPipeHeight = value;
            }
        }
        
        public int NumSides
        {
            get { return mNumSides; }
            set
            {
                AddHistory("NumSides", value);
                mNumSides = value;
            }
        }
        public string TextString
        {
            get { return mText; }
            set
            {
                AddHistory("TextString", value);
                mText = value;
            }
        }
        public double Radius
        {
            get { return mRadius; }
            set
            {
                AddHistory("Radius", value);
                mRadius = value;
            }
        }
        public vdTextstyle TextStyle
        {
            get
            {
                return mTextStyle;
            }
            set
            {
                AddHistory("TextStyle", value);
                mTextStyle = value;
            }
        }
        public double TextHeight
        {
            get
            {
                return mTextHeight;
            }
            set
            {
                AddHistory("TextHeight", value);
                mTextHeight = value;
            }
        }

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
                cirStart.Center = value;
            }
        }
        public gPoint EndPoint
        {
            get
            {
                return gpEndPoint;
            }
            set
            {
                AddHistory("EndPoint", value);
                gpEndPoint = value;
                cirEnd.Center = value;
            }
        }
        public vdCircle StartCircle
        {
            get
            {
                return cirStart;
            }
            set
            {
                AddHistory("StartCircle", value);
                cirStart = value;
            }
        }
        public vdCircle EndCircle
        {
            get
            {
                return cirEnd;
            }
            set
            {
                AddHistory("EndCircle", value);
                cirEnd = value;
            }
        }

        #endregion

        #region serialization
        public override void Serialize(Serializer serializer)
        {
            base.Serialize(serializer);
            serializer.Serialize(mNumSides, "NumSides");
            serializer.Serialize(mRadius, "Radius");
            serializer.Serialize(mText, "TextString");
            serializer.Serialize(mTextStyle, "TextStyle");
            serializer.Serialize(mTextHeight, "TextHeight");
            serializer.Serialize(dPipeHeight, "PipeHeight");
            serializer.Serialize(gpStartPoint, "StartPoint");
            serializer.Serialize(gpEndPoint, "EndPoint");
            serializer.Serialize(cirStart, "StartCircle");
            serializer.Serialize(cirEnd, "EndCircle");
        }
        public override bool DeSerialize(DeSerializer deserializer, string fieldname, object value)
        {
            if (base.DeSerialize(deserializer, fieldname, value)) return true;
            else if (fieldname == "NumSides") mNumSides = (int)value;
            else if (fieldname == "Radius") mRadius = (double)value;
            else if (fieldname == "TextString") mText = (string)value;
            else if (fieldname == "TextStyle") mTextStyle = (vdTextstyle)value;
            else if (fieldname == "TextHeight") mTextHeight = (double)value;
            else if (fieldname == "PipeHeight") dPipeHeight = (double)value;
            else if (fieldname == "StartCircle") cirStart = value as vdCircle;
            else if (fieldname == "EndCircle") cirEnd = value as vdCircle;
            else if (fieldname == "StartPoint") gpStartPoint = value as gPoint;
            else if (fieldname == "EndPoint") gpEndPoint = value as gPoint;

            else return false;
            return true;
        }
        #endregion
        #region curve calulation
        public override void FillShapeEntities(ref vdEntities entities)
        {
            //calculate shape entities in ecs 
            
            gPoint cen = new gPoint();
            vdText text = new vdText();
            //entities.AddItem(text);
            //text.MatchProperties(this, Document);

            text.Style = TextStyle;
            text.TextString = TextString;
            text.VerJustify = VdConstVerJust.VdTextVerCen;
            text.HorJustify = VdConstHorJust.VdTextHorCenter;
            text.Height = TextHeight;
            text.InsertionPoint = cen;
            vdPolyline pl = new vdPolyline();

            entities.AddItem(cirStart);
            entities.AddItem(pl);
            //entities.AddItem(pl2);

            pl.MatchProperties(this, Document);
            cirStart.MatchProperties(this, Document);
            cirEnd.MatchProperties(this, Document);

            Vertexes verts = new Vertexes();

            double stepangle = Globals.VD_TWOPI / this.NumSides;
            double sang = 0.0d;

            for (int i = 0; i < NumSides; i++)
            {
                verts.Add(gPoint.Polar(cen, sang, Radius));
                sang += stepangle;
            }
            pl.VertexList = verts;
            pl.Flag = VdConstPlineFlag.PlFlagCLOSE;

            //entities.AddItem(cirEnd);

            cirStart.Center = cen;
            cirStart.Radius = mRadius;
            cirStart.Thickness = PipeHeight;
            //cirStart.Thickness = cirStart.Center.Distance3D(EndPoint);
            //cirStart.ExtrusionVector = new Vector(0.0, 1.0, 0.0);
            //cirStart.ExtrusionVector = Vector.CreateExtrusion(EndPoint, StartPoint);
            cirStart.ExtrusionVector = Vector.CreateExtrusion( StartPoint,EndPoint);


            cirEnd.Center = EndPoint;
            cirEnd.Radius = mRadius;
            //cirEnd.ExtrusionVector = Vector.CreateExtrusion(cen, EndPoint);
            //cirEnd.ExtrusionVector = cirStart.ExtrusionVector;




            // Draw DisNet Pipe
            gPoints gPts = GetDisNetPipePoints();

            //vdPolyline plDisNet = new vdPolyline();
            pl.VertexList = new Vertexes(gPts);
            //entities.AddItem(pl);

        }


        public gPoints GetDisNetPipePoints()
        {
            string fName = "C:\\DisNetPoints.txt";
            StreamReader sr = new StreamReader(new FileStream(fName, FileMode.Open, FileAccess.Read));
            gPoints pts = new gPoints();
            string[] values;
            string kStr = "";
            double dx = 0.0, dy, dz;
            dx = dy = dz = 0.0;
            while (!sr.EndOfStream)
            {
                try
                {
                    kStr = sr.ReadLine();
                    values = kStr.Split(new char[] { ',' });
                    dx = double.Parse(values[0]);
                    dy = double.Parse(values[1]);
                    dz = double.Parse(values[2]);
                    pts.Add(dx, dy, dz);
                }
                catch (Exception exx)
                {
                }
            }
            sr.Close();
            return pts;
        }
        #endregion

        #region overrides
        protected override void OnDocumentDefaults()
        {
            base.OnDocumentDefaults();
            mTextStyle = Document.ActiveTextStyle;
            if (mTextStyle == null) mTextStyle = Document.TextStyles.Standard;
        }
        public override void InitializeProperties()
        {
            gpStartPoint = new gPoint();
            gpEndPoint = new gPoint();
            dPipeHeight = 0.0;
            cirStart = new vdCircle();
            cirEnd = new vdCircle();


            mNumSides = 4;
            mText = "";
            mTextHeight = 1.0d;
            mTextStyle = null;
            if (Document != null) mTextStyle = Document.TextStyles.Standard;
            mRadius = 1.0d;
            base.InitializeProperties();
        }

        public override void MatchProperties(VectorDraw.Professional.vdObjects.vdPrimary _from, VectorDraw.Professional.vdObjects.vdDocument thisdocument)
        {
            base.MatchProperties(_from, thisdocument);
            Pipe3D from = _from as Pipe3D;
            if (from == null) return;
            gpStartPoint = from.gpStartPoint;
            gpEndPoint = from.gpEndPoint;
            dPipeHeight = from.dPipeHeight;
            cirStart = from.cirStart;
            cirEnd = from.cirEnd;


            TextString = from.TextString;
            NumSides = from.NumSides;
            Radius = from.Radius;
            TextStyle = from.TextStyle;
            TextHeight = from.TextHeight;
        }
        public override void Transformby(Matrix mat)
        {
            if (mat.IsUnitMatrix()) return;
            Matrix mult = (ECSMatrix * mat);
            MatrixProperties matprops = mult.Properties;
            Radius *= matprops.GetScaleXY();
            TextHeight *= matprops.GetScaleXY();
            base.Transformby(mat);
        }
        public override gPoints GetGripPoints()
        {
            gPoints ret = new gPoints();
            gPoint cen = new gPoint();
            ret.Add(cen);
            double stepangle = Globals.VD_TWOPI / this.NumSides;
            double sang = 0.0d;
            for (int i = 0; i < NumSides; i++)
            {
                ret.Add(gPoint.Polar(cen, sang, Radius));
                sang += stepangle;
            }
            ECSMatrix.Transform(ret);
            return ret;
        }
        public override void MoveGripPointsAt(Int32Array Indexes, double dx, double dy, double dz)
        {
            try
            {

                if (Indexes == null || Indexes.Count == 0 || (dx == 0.0 && dy == 0.0 && dz == 0.0)) return;
                Matrix mat = new Matrix();
                mat.TranslateMatrix(dx, dy, dz);
                gPoints grips = GetGripPoints();
                ECSMatrix.GetInvertion().Transform(grips);
                if (Indexes.Count == grips.Count)
                {
                    Transformby(mat);
                }
                else
                {
                    foreach (int index in Indexes)
                    {
                        switch (index)
                        {
                            case 0:
                                Transformby(mat);
                                break;
                            default:
                                gPoint grip = new gPoint(grips[index]);
                                grip = ECSMatrix.Transform(grip);
                                grip += new gPoint(dx, dy, dz);
                                this.Radius = gPoint.Distance2D(grip, this.Origin);
                                break;
                        }
                    }
                }
                Update();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }
        public override bool IsTableObjectDependOn(vdPrimary table)
        {
            if (Deleted) return false;//always check if this object is deleted
            if (base.IsTableObjectDependOn(table)) return true;//always check the base implamentation
            //check properties of object that reference the table object.
            if (TextStyle != null && object.ReferenceEquals(TextStyle, table)) return true;
            return false;
        }
        public override void GetTableDependecies(vdLayers layers, vdBlocks blocks, vdDimstyles dimstyles, vdLineTypes linetypes, vdTextstyles textstyles, vdImages images, vdHatchPatterns hatchpatterns, object breakOnObject, ref bool breakObjectFound)
        {
            if (breakOnObject != null && breakObjectFound) return;
            if (Deleted) return;
            //base.GetTableDependecies(layers, blocks, dimstyles, linetypes, textstyles, images, hatchpatterns, breakOnObject, ref breakObjectFound);


            vdTableDependeciesArgs vd = new vdTableDependeciesArgs();
            //base.GetTableDependecies(layers, blocks, dimstyles, linetypes, textstyles, images, hatchpatterns, breakOnObject, ref breakObjectFound);
            base.GetTableDependecies(vd);
            
            if (textstyles != null)
            {
                textstyles.AddItem(mTextStyle);
                if (breakOnObject != null && object.ReferenceEquals(breakOnObject, mTextStyle))
                {
                    breakObjectFound = true;
                    return;
                }
            }
            if (mTextStyle != null)
            {
                vdTableDependeciesArgs vt = new vdTableDependeciesArgs();
                //base.GetTableDependecies(layers, blocks, dimstyles, linetypes, textstyles, images, hatchpatterns, breakOnObject, ref breakObjectFound);
                
                base.GetTableDependecies(vt);

                //mTextStyle.GetTableDependecies(layers, blocks, dimstyles, linetypes, textstyles, images, hatchpatterns, breakOnObject, ref breakObjectFound);
            }
        }
        #endregion
     
    }
    public class ActionPipe3D : ActionEntityEx
    {
        private Pipe3D figure;
        //public ActionPipe3D(int NumSides, string text, double textheight, gPoint reference, vdLayout layout,gPoint endPoint)
        //    : base(reference, layout)
        public ActionPipe3D(int NumSides, double height, gPoint reference, vdLayout layout, gPoint endPoint)
            : base(reference, layout)
        {
            ValueTypeProp |= valueType.DISTANCE;
            figure = new Pipe3D();
            figure.SetUnRegisterDocument(layout.Document);
            figure.setDocumentDefaults();
            figure.Origin = reference;
            figure.Radius = 0.0d;
            //figure.TextHeight = textheight;
            //figure.TextString = text;
            figure.NumSides = NumSides;
            figure.PipeHeight = height;

            // Chiranjit 11/08/2009
            figure.StartPoint = reference;
            figure.EndPoint = endPoint;
            
        }
        protected override void OnMyPositionChanged(gPoint newPosition)
        {
            try
            {
                figure.Radius = newPosition.Distance3D(figure.Origin);
                figure.StartCircle.Radius = newPosition.Distance3D(figure.StartCircle.Center);
                //figure.EndCircle.Radius = figure.Radius;
            }
            catch (Exception exx)
            {
                System.Windows.Forms.MessageBox.Show(exx.ToString());
            }   
            
        }
        public override vdFigure Entity
        {
            get
            {
                return figure;
            }
        }

        static List<DisNetPipe> lstPipe = new List<DisNetPipe>();

        public static void CmdPipe3D(vdDocument doc)
        {
            Color pipeColor = Color.Green;
            double pipeSize = 100.0d;

            vdLayer pipeLay = new vdLayer();
            pipeLay.Name = "Pipes";
            pipeLay.SetUnRegisterDocument(doc);
            pipeLay.setDocumentDefaults();
            doc.Layers.AddItem(pipeLay);
            doc.ActiveLayer = pipeLay;


            //OpenFileDialog ofd = new OpenFileDialog();
            //string fName = "";
            
            //ofd.Title = "SELECT DisNet Pipe Details File";
            //ofd.Filter = "Text File|*.txt";

            

            //if (ofd.ShowDialog() != DialogResult.Cancel)
            //{
            //    fName = ofd.FileName;
            //    ReadPipes(fName);
            //    HEADSNeed.DisNet.frmDrawPipe fdp = new HEADSNeed.DisNet.frmDrawPipe();
            //    if (fdp.ShowDialog() == DialogResult.OK)
            //    {
            //        pipeColor = fdp.PipeColor;
            //        pipeSize = fdp.PipeSize;
            //        //doc.ActiveLayer.PenColor = new vdColor(pipeColor);
            //        pipeLay.PenColor = new vdColor(pipeColor);
            //    }
            //    else
            //        return;
            //}

            //ReadPipes("C:\\DisNetPoints.txt");

            for (int i = 0; i < lstPipe.Count; i++)
            {
                try
                {
                    
                    DisNetPipe dnPipe = lstPipe[i];
                    vdCircle cir = new vdCircle();
                    cir.Center = dnPipe.StartPoint;
                    cir.Radius = dnPipe.Diameter * pipeSize;
                    //cir.Radius = dnPipe.Diameter;
                    cir.ExtrusionVector = Vector.CreateExtrusion(dnPipe.StartPoint, dnPipe.EndPoint);
                    cir.SetUnRegisterDocument(doc);
                    cir.setDocumentDefaults();
                    cir.Layer = pipeLay;

                    doc.ActionLayout.Entities.AddItem(cir);
                    cir.Thickness = dnPipe.Length;
                    //cir.PenColor = new vdColor(cl);
                    doc.CommandAction.CmdSphere(dnPipe.EndPoint, cir.Radius, 10, 10);
                    doc.CommandAction.Zoom("E", 100, 100);



                    
                    //doc.Redraw(true);
                }
                catch (Exception exx)
                {
                }
            }
            doc.RenderMode = vdRender.Mode.Render;
            doc.CommandAction.RegenAll();
            doc.Redraw(true);
        }

        public static void CmdPipe3DFromText(vdDocument doc, string txtFileName)
        {
            Color pipeColor = Color.Green;
            double pipeSize = 100.0d;

            vdLayer pipeLay = new vdLayer();

            pipeLay.Name = "Pipes";
            pipeLay.SetUnRegisterDocument(doc);
            pipeLay.setDocumentDefaults();
            doc.Layers.AddItem(pipeLay);
            doc.ActiveLayer = pipeLay;


            vdLayer nodeBallLay = new vdLayer();
            nodeBallLay.Name = "NodalData";
            nodeBallLay.SetUnRegisterDocument(doc);
            nodeBallLay.setDocumentDefaults();
            doc.Layers.AddItem(nodeBallLay);
            nodeBallLay.PenColor = new vdColor(Color.Red);



            string fName = txtFileName;

            ReadPipes(fName);
            HEADSNeed.DisNet.frmDrawPipe fdp = new HEADSNeed.DisNet.frmDrawPipe();
            if (fdp.ShowDialog() == DialogResult.OK)
            {
                pipeColor = fdp.PipeColor;
                pipeSize = fdp.PipeSize;
                //doc.ActiveLayer.PenColor = new vdColor(pipeColor);
                pipeLay.PenColor = new vdColor(pipeColor);
            }
            else
            {
                return;
            }
            for (int i = 0; i < lstPipe.Count; i++)
            {
                try
                {

                    DisNetPipe dnPipe = lstPipe[i];
                    vdCircle cir = new vdCircle();
                    cir.Center = dnPipe.StartPoint;
                    cir.Radius = dnPipe.Diameter * pipeSize;
                    //cir.Radius = dnPipe.Diameter;
                    cir.ExtrusionVector = Vector.CreateExtrusion(dnPipe.StartPoint, dnPipe.EndPoint);
                    cir.SetUnRegisterDocument(doc);
                    cir.setDocumentDefaults();
                    cir.Layer = pipeLay;

                    doc.ActionLayout.Entities.AddItem(cir);
                    cir.Thickness = dnPipe.Length;
                    //cir.PenColor = new vdColor(cl);
                    doc.CommandAction.CmdSphere(dnPipe.EndPoint, cir.Radius, 10, 10);
                    doc.ActiveLayer = nodeBallLay;
                    doc.CommandAction.CmdSphere(dnPipe.EndPoint, cir.Radius + 5.0d, 15, 10);
                    doc.ActiveLayer = pipeLay;
                    doc.CommandAction.Zoom("E", 100, 100);

                    if (i == 0)
                    {
                        vdPolyline pline = new vdPolyline();
                        pline.VertexList.Add(new gPoint(cir.Center.x - 35.0d, cir.Center.y + 25.0d, cir.Center.z));
                        pline.VertexList.Add(new gPoint(cir.Center.x - 35.0d, cir.Center.y, cir.Center.z));
                        pline.VertexList.Add(new gPoint(cir.Center.x + 35.0d, cir.Center.y, cir.Center.z));
                        pline.VertexList.Add(new gPoint(cir.Center.x + 35.0d, cir.Center.y + 25.0d, cir.Center.z));
                        pline.SetUnRegisterDocument(doc);
                        pline.setDocumentDefaults();

                        pline.PenColor = new vdColor(Color.LightPink);
                        pline.PenWidth = 2.0d;
                        doc.ActionLayout.Entities.AddItem(pline);


                        vdLine ln = new vdLine();
                        ln.SetUnRegisterDocument(doc);
                        ln.setDocumentDefaults();

                        ln.StartPoint = (pline.VertexList[0] + pline.VertexList[1]) / 2;
                        ln.EndPoint = (pline.VertexList[2] + pline.VertexList[3]) / 2;
                        ln.PenColor = new vdColor(Color.LightPink);
                        ln.PenWidth = 2.0d;
                        doc.ActionLayout.Entities.AddItem(ln);

                        vdText tx = new vdText();
                        tx.SetUnRegisterDocument(doc);
                        tx.setDocumentDefaults();
                        tx.InsertionPoint = new gPoint(ln.StartPoint.x + 2.0d, ln.StartPoint.y + 3.0d, ln.StartPoint.z);
                        tx.TextString = "SOURCE";
                        tx.PenColor = new vdColor(Color.LightPink);
                        tx.Height = 10.0d;

                        doc.ActionLayout.Entities.AddItem(tx);
                    }
                    
                    //doc.Redraw(true);
                }
                catch (Exception exx)
                {
                }
            }
            doc.RenderMode = vdRender.Mode.Wire2dGdiPlus;
            doc.CommandAction.RegenAll();
            doc.Redraw(true);
        }
        public static void CmdSectionClip(vdDocument doc)
        {
            doc.ActiveLayOut.Sections.RemoveAll();
            gPoint cen = new gPoint();
            gPoint endPt = new gPoint();
            vdSectionClip secClip = new vdSectionClip();
            secClip.SetUnRegisterDocument(doc);
            secClip.Name = "CLIP1";
            secClip.Enable = true;

            doc.Prompt("Origin Point : ");
            object ret = doc.ActionUtility.getUserPoint();
            doc.Prompt(null);
            if (ret == null || !(ret is gPoint)) return;
            secClip.OriginPoint = ret as gPoint;
            secClip.Direction = new Vector(0, 0, 1);
            doc.ActiveLayOut.Sections.AddItem(secClip);
            doc.Redraw(true);
        }

        public static void ReadPipes(string fileName)
        {
            //System.IO.StreamReader sr = new System.IO.StreamReader("C:\\HareKrishna.txt");
            System.IO.StreamReader sr = new System.IO.StreamReader(fileName);
            lstPipe.Clear();
            while (sr.EndOfStream == false)
            {
                try
                {
                    //pts.Add(GetPoint(sr.ReadLine()));
                    lstPipe.Add(DisNetPipe.Parse(sr.ReadLine()));
                }
                catch (Exception exx)
                {
                }
            }
        }
    }
    public class DisNetPipe
    {
        int iNo;
        double dLength, dDiameter;
        gPoint gpStartPoint, gpEndPoint;
        string kStr;
        public DisNetPipe()
        {
            iNo = 0;
            dLength = 0.0;
            dDiameter = 0.0;
            gpStartPoint = new gPoint();
            gpEndPoint = new gPoint();
        }
        public int No
        {
            get
            {
                return iNo;
            }
            set
            {
                iNo = value;
            }
        }
        public double Length
        {
            get
            {
                return dLength;
            }
            set
            {
                dLength = value;
            }
        }
        public gPoint StartPoint
        {
            get
            {
                return gpStartPoint;
            }
            set
            {
                gpStartPoint = value;
            }
        }
        public gPoint EndPoint
        {
            get
            {
                return gpEndPoint;
            }
            set
            {
                gpEndPoint = value;
            }
        }
        public double Diameter
        {
            get
            {
                return dDiameter;
            }
            set
            {
                dDiameter = value;
            }
        }
        public static DisNetPipe Parse(string str)
        {
            DisNetPipe dnPipe = new DisNetPipe();
            str = str.Replace(",", " ");
            while (str.IndexOf("  ") != -1)
            {
                str = str.Replace("  ", " ");
            }
            string[] values = str.Split(new char[] { ' ' });

            //if (values.Length == 9)
            //{
            int i = 0;
            dnPipe.No = int.Parse(values[i]); i++;
            dnPipe.StartPoint.x = double.Parse(values[i]); i++;
            dnPipe.StartPoint.y = double.Parse(values[i]); i++;
            dnPipe.StartPoint.z = double.Parse(values[i]); i++;
            dnPipe.EndPoint.x = double.Parse(values[i]); i++;
            dnPipe.EndPoint.y = double.Parse(values[i]); i++;
            dnPipe.EndPoint.z = double.Parse(values[i]); i++;
            dnPipe.Length = double.Parse(values[i]); i++;
            dnPipe.Diameter = double.Parse(values[i]); i++;
            //}
            return dnPipe;

        }
    }
}
