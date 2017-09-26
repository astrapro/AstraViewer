using System;
using System.Collections.Generic;
using System.Text;

using VectorDraw.Geometry;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
namespace MovingLoadAnalysis.DataStructure
{
    public class VehicleLoad
    {
        public double Skew_Angle = 0.0;

        vdDocument vDoc_elevation = null;
        vdDocument vDoc_Plan = null;

        public VehicleLoad(vdDocument doc_elevation, vdDocument doc_plan, MLoadData ld_data)
        {
            //Skew_Angle = skew_ang;
            Loads = ld_data;
            XINC = 0.2;
            X = 0;
            Y = 0;
            Z = 0.0;
            LoadCase = 0;

            vDoc_elevation = doc_elevation;
            vDoc_Plan = doc_plan;

            WheelAxis = new List<Axial>();
            //WheelPlan = new List<PlanWheel>();

            Set_Wheel_Distance();
            //Axial ax = new Axial(doc_elevation);
            //ax.Width = Width;
            //ax.Load = Loads.LoadValues.StringList[0];
            //ax.WheelRadius = (Loads.LoadWidth == 0.0) ? 0.2d : Loads.LoadWidth / 9.0;

            //ax.Wheel1.ToolTip = Loads.Code + ", Axial Load = " + Loads.LoadValues.StringList[0];
            //ax.Wheel2.ToolTip = Loads.Code + ", Axial Load = " + Loads.LoadValues.StringList[0];
            //ax.Axis.ToolTip = Loads.Code + ", Load Width = " + Loads.LoadWidth + ", Distance = " + Loads.Distances.StringList[0];
            //WheelAxis.Add(ax);
            
            //double last_x = 0.0;
            //Loads.Distances.StringList.Remove("");
            //Loads.LoadValues.StringList.Remove("");
            //for (int i = 0; i < Loads.Distances.Count; i++)
            //{
            //    last_x += Loads.Distances.GetDouble(i);
            //    ax = new Axial(doc_elevation);
            //    ax.X = -last_x;
            //    ax.Width = Width;
            //    ax.Load = Loads.LoadValues.StringList[i];
            //    ax.WheelRadius = (Loads.LoadWidth == 0.0) ? 0.2d : Loads.LoadWidth / 9.0;

            //    ax.Wheel1.ToolTip = Loads.Code + ", Axial Load = " + Loads.LoadValues.StringList[i] + ", Distance = " + Loads.Distances.StringList[i];
            //    ax.Wheel2.ToolTip = Loads.Code + ", Axial Load = " + Loads.LoadValues.StringList[i] + ", Distance = " + Loads.Distances.StringList[i];
            //    ax.Axis.ToolTip = Loads.Code + ", Load Width = " + Loads.LoadWidth + ", Distance = " + Loads.Distances.StringList[i];
            //    WheelAxis.Add(ax);
            //}
        }
       

        public void Set_Wheel_Distance()
        {
            try
            {

                if (WheelAxis.Count == Loads.Distances.Count + 1)
                {
                    double last_x = 0.0;
                    WheelAxis[0].X = last_x;
                    for (int i = 0; i < Loads.Distances.Count; i++)
                    {
                        last_x += Loads.Distances.GetDouble(i);
                        WheelAxis[i + 1].X = -last_x;
                    }
                }
                else
                {

                    Axial ax = new Axial(vDoc_elevation);
                    ax.Width = Width;
                    ax.Load = Loads.LoadValues.StringList[0];
                    ax.WheelRadius = (Loads.LoadWidth == 0.0) ? 0.2d : Loads.LoadWidth / 9.0;

                    ax.Wheel1.ToolTip = Loads.Code + ", Axial Load = " + Loads.LoadValues.StringList[0];
                    ax.Wheel2.ToolTip = Loads.Code + ", Axial Load = " + Loads.LoadValues.StringList[0];
                    ax.Axis.ToolTip = Loads.Code + ", Load Width = " + Loads.LoadWidth + ", Distance = " + Loads.Distances.StringList[0];
                    WheelAxis.Add(ax);

                    double last_x = 0.0;
                    Loads.Distances.StringList.Remove("");
                    Loads.LoadValues.StringList.Remove("");
                    for (int i = 0; i < Loads.Distances.Count; i++)
                    {
                        last_x += Loads.Distances.GetDouble(i);
                        ax = new Axial(vDoc_elevation);
                        ax.X = -last_x;
                        ax.Width = Width;
                        ax.Load = Loads.LoadValues.StringList[i];
                        ax.WheelRadius = (Loads.LoadWidth == 0.0) ? 0.2d : Loads.LoadWidth / 9.0;

                        ax.Wheel1.ToolTip = Loads.Code + ", Axial Load = " + Loads.LoadValues.StringList[i] + ", Distance = " + Loads.Distances.StringList[i];
                        ax.Wheel2.ToolTip = Loads.Code + ", Axial Load = " + Loads.LoadValues.StringList[i] + ", Distance = " + Loads.Distances.StringList[i];
                        ax.Axis.ToolTip = Loads.Code + ", Load Width = " + Loads.LoadWidth + ", Distance = " + Loads.Distances.StringList[i];
                        WheelAxis.Add(ax);
                    }
                }
            }
            catch (Exception ex) { }
        }

        public void Update(double xinc)
        {
            foreach (var item in WheelAxis)
            {
                item.Z = Z;
                item.Y = Y + item.Wheel1.Radius;
                if (xinc >= 0.0)
                {
                    item.X += xinc;
                    item.X += Math.Tan(Skew_Angle * (Math.PI / 180.0)) * item.Z;
                    if (item.X > TotalLength)
                        item.X = 0.0;
                }
                else
                {
                    item.X = item.X + xinc;
                    item.X += Math.Tan(Skew_Angle * (Math.PI / 180.0)) * item.Z;
                    if (item.X <= 0)
                        item.X = TotalLength;

                }
                item.Update();
            }
            foreach (var item in WheelPlan)
            {
                item.Z = Z;
                item.Y = Y + item.Wheel1.Radius;
                if (xinc >= 0.0)
                {
                    item.X += xinc;
                    item.X += Math.Tan(Skew_Angle * (Math.PI / 180.0)) * item.Z;
                    if (item.X > TotalLength)
                        item.X = 0.0;
                }
                else
                {
                    item.X = item.X + xinc;
                    item.X += Math.Tan(Skew_Angle * (Math.PI / 180.0)) * item.Z;
                    if (item.X <= 0)
                        item.X = TotalLength;
                }
                item.Update();
            }
            vDoc_elevation.Redraw(true);
            vDoc_Plan.Redraw(true);
        }
        public void Update()
        {

            foreach (var item in WheelAxis)
            {
                item.Z = Z;
                item.Y = Y + item.Wheel1.Radius;

                item.X -= XINC;
                //item.X += XINC;
                item.X += Math.Tan(Skew_Angle * (Math.PI / 180.0)) * item.Z;

                if (item.X > TotalLength)
                    item.X = 0.0;

                item.Wheel1.visibility = (item.Wheel1.Center.x < 0) ? vdFigure.VisibilityEnum.Invisible : vdFigure.VisibilityEnum.Visible;
                item.Wheel2.visibility = item.Wheel1.visibility;
                item.Axis.visibility = item.Wheel1.visibility;
                item.Update();
            }
            //foreach (var item in WheelPlan)
            //{
            //    item.Z = Z;
            //    item.Y = Y + item.Wheel1.Radius;

            //    item.X += XINC;
            //    if (item.X > TotalLength)
            //        item.X = 0.0;

            //    item.Wheel1.visibility = (item.Wheel1.Center.x < 0) ? vdFigure.VisibilityEnum.Invisible : vdFigure.VisibilityEnum.Visible;
            //    item.Wheel2.visibility = item.Wheel1.visibility;
            //    item.Axis.visibility = item.Wheel1.visibility;

            //    item.Update();
            //}
            vDoc_elevation.Redraw(true);
            //vDoc_Plan.Redraw(true);
        }
        public void Update(bool isForward)
        {

            foreach (var item in WheelAxis)
            {
                item.Z = Z;
                item.Y = Y + item.Wheel1.Radius;

                if(isForward)
                    item.X += XINC;
                else
                    item.X -= XINC;


                item.X += Math.Tan(Skew_Angle * (Math.PI / 180.0)) * item.Z;

                if (item.X > TotalLength)
                    item.X = 0.0;

                item.Wheel1.visibility = (item.Wheel1.Center.x < 0) ? vdFigure.VisibilityEnum.Invisible : vdFigure.VisibilityEnum.Visible;
                item.Wheel2.visibility = item.Wheel1.visibility;
                item.Axis.visibility = item.Wheel1.visibility;
                item.Update();
            }
            //foreach (var item in WheelPlan)
            //{
            //    item.Z = Z;
            //    item.Y = Y + item.Wheel1.Radius;

            //    item.X += XINC;
            //    if (item.X > TotalLength)
            //        item.X = 0.0;

            //    item.Wheel1.visibility = (item.Wheel1.Center.x < 0) ? vdFigure.VisibilityEnum.Invisible : vdFigure.VisibilityEnum.Visible;
            //    item.Wheel2.visibility = item.Wheel1.visibility;
            //    item.Axis.visibility = item.Wheel1.visibility;

            //    item.Update();
            //}
            vDoc_elevation.Redraw(true);
            //vDoc_Plan.Redraw(true);
        }

        public double TotalLength { get; set; }
        public double XINC { get; set; }
        public double Width { get { return Loads.LoadWidth; } }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public int LoadCase { get; set; }
        public List<Axial> WheelAxis { get; set; }
        public List<PlanWheel> WheelPlan { get; set; }
        public MLoadData Loads { get; set; }
        ~VehicleLoad()
        {
            
            Loads = null;
            WheelAxis = null;
            WheelPlan = null;
        }
    }

    public class Axial
    {
        vdDocument vDoc = null;
        public Axial(vdDocument vdoc)
        {
            //X = Y = Z = 0.0;
            vDoc = vdoc;

            Wheel1 = new vdCircle();
            Wheel1.SetUnRegisterDocument(vdoc);
            Wheel1.setDocumentDefaults();
            Wheel1.Radius = WheelRadius;
            vdoc.ActiveLayOut.Entities.AddItem(Wheel1);

            Wheel2 = new vdCircle();
            Wheel2.SetUnRegisterDocument(vdoc);
            Wheel2.setDocumentDefaults();
            Wheel2.Radius = WheelRadius;
            vdoc.ActiveLayOut.Entities.AddItem(Wheel2);

            Axis = new vdLine();
            Axis.SetUnRegisterDocument(vdoc);
            Axis.setDocumentDefaults();
            Axis.StartPoint = Wheel1.Center;
            Axis.EndPoint = Wheel2.Center;
            vdoc.ActiveLayOut.Entities.AddItem(Axis);

            WheelRadius = (Width == 0.0) ? 0.2d : Width / 10.0;
        }
        public Axial(vdDocument vdoc, string loads)
        {
            //X = Y = Z = 0.0;
            vDoc = vdoc;

            Wheel1 = new vdCircle();
            Wheel1.SetUnRegisterDocument(vdoc);
            Wheel1.setDocumentDefaults();
            Wheel1.Radius = WheelRadius;
            vdoc.ActiveLayOut.Entities.AddItem(Wheel1);

            Wheel2 = new vdCircle();
            Wheel2.SetUnRegisterDocument(vdoc);
            Wheel2.setDocumentDefaults();
            Wheel2.Radius = WheelRadius;
            vdoc.ActiveLayOut.Entities.AddItem(Wheel2);

            Axis = new vdLine();
            Axis.SetUnRegisterDocument(vdoc);
            Axis.setDocumentDefaults();
            Axis.StartPoint = Wheel1.Center;
            Axis.EndPoint = Wheel2.Center;
            vdoc.ActiveLayOut.Entities.AddItem(Axis);


            WheelRadius = 0.3d;
            Load = (new MyList(loads, ' ')).StringList[0];
        }
        public double X
        {
            get
            {
                return Wheel1.Center.x;
            }
            set
            {
                Wheel1.Center.x = value;
                Wheel2.Center.x = value;
            }
        }
        public double Y
        {

            get
            {
                return Wheel1.Center.y;
            }
            set
            {
                Wheel1.Center.y = value;
                Wheel2.Center.y = value;
            }
        }
        public double Z
        {

            get
            {
                return Wheel1.Center.z;
            }
            set
            {
                Wheel1.Center.z = value;
                Wheel2.Center.z = value + Width;
            }
        }
        public double Width { get; set; }

        public vdCircle Wheel1 { get; set; }
        public vdCircle Wheel2 { get; set; }
        public vdLine Axis { get; set; }
        public double WheelRadius
        {
            get
            {
                try
                {
                    return Wheel1.Radius;
                }
                catch (Exception ex) { }
                return 0.2;
            }
            set
            {
                try
                {
                    Wheel1.Radius = value;
                    Wheel2.Radius = value;
                }
                catch (Exception ex) { }
            }
        }
        public string Load
        {
            get
            {
                try
                {
                    return Axis.ToolTip;
                }
                catch (Exception ex) { }
                return "";
            }
            set
            {
                try
                {
                    Axis.ToolTip = "Axial Load = " + value + ", Load Width = " + Width;
                }
                catch (Exception ex) { }

            }
        }

        public void Update()
        {
            Wheel1.Radius = WheelRadius;
            Wheel2.Radius = WheelRadius;
        
            Axis.StartPoint = Wheel1.Center;
            Axis.EndPoint = Wheel2.Center;
            Wheel1.Update();
            Wheel2.Update();
            Axis.Update();
        }

        
        ~Axial()
        {
            try
            {
                Wheel1.Deleted = true;
                Wheel2.Deleted = true;
                Axis.Deleted = true;
                Wheel1.Dispose();
                Wheel2.Dispose();
                Axis.Dispose();
            }
            catch (Exception ex) { }
        }
    }
    public class PlanWheel
    {
        vdDocument vDoc = null;
        public PlanWheel(vdDocument vdoc)
        {
            //X = Y = Z = 0.0;
            WheelRadius = 0.3d;
            vDoc = vdoc;

            Wheel1 = new vdCircle();
            Wheel1.SetUnRegisterDocument(vdoc);
            Wheel1.setDocumentDefaults();
            Wheel1.Radius = WheelRadius;
            Wheel1.ExtrusionVector = new Vector(0, 1, 0);
            vdoc.ActiveLayOut.Entities.AddItem(Wheel1);

            Wheel2 = new vdCircle();
            Wheel2.SetUnRegisterDocument(vdoc);
            Wheel2.setDocumentDefaults();
            Wheel2.Radius = WheelRadius;
            Wheel2.ExtrusionVector = new Vector(0, 1, 0);
            vdoc.ActiveLayOut.Entities.AddItem(Wheel2);

            Axis = new vdLine();
            Axis.SetUnRegisterDocument(vdoc);
            Axis.setDocumentDefaults();
            Axis.StartPoint = Wheel1.Center;
            Axis.EndPoint = Wheel2.Center;
            vdoc.ActiveLayOut.Entities.AddItem(Axis);
        }
        public double X
        {
            get
            {
                return Wheel1.Center.x;
            }
            set
            {
                Wheel1.Center.x = value;
                Wheel2.Center.x = value;
            }
        }
        public double Y
        {

            get
            {
                return Wheel1.Center.y;
            }
            set
            {
                Wheel1.Center.y = value;
                Wheel2.Center.y = value;
            }
        }
        public double Z
        {

            get
            {
                return Wheel1.Center.z;
            }
            set
            {
                Wheel1.Center.z = value;
                Wheel2.Center.z = value + Width;
            }
        }
        public double Width { get; set; }

        public vdCircle Wheel1 { get; set; }
        public vdCircle Wheel2 { get; set; }
        public vdLine Axis { get; set; }
        public double WheelRadius { get; set; }
        

        public void Update()
        {
            Axis.StartPoint = Wheel1.Center;
            Axis.EndPoint = Wheel2.Center;
            Wheel1.Radius = WheelRadius;
            Wheel2.Radius = WheelRadius;
            Wheel1.Update();
            Wheel2.Update();
            Axis.Update();
        }
        ~PlanWheel()
        {
            try
            {
                Wheel1.Deleted = true;
                Wheel2.Deleted = true;
                Axis.Deleted = true;
                Wheel1.Dispose();
                Wheel2.Dispose();
                Axis.Dispose();
            }
            catch (Exception ex) { }
        }
    }
}
