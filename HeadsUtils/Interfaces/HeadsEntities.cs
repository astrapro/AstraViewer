using System;
using System.Collections.Generic;
using System.Text;
using HeadsUtils;

namespace HeadsUtils.Interfaces
{
    

    public interface IHdEntity
    {
        eHEADS_COLOR color { get; set; }
        string EntityName { get; }
        string Handle { get; }
        string Layer { get; set; }
        string Label { get; set; }
        double LinetypeScale { get; set; }
        eHEADS_LWEIGHT Lineweight { get; set; }
        short ColorIndex{ get; set; }
        
        int ObjectID { get; }
        string ObjectName { get; }
        int OwnerID { get; }
        double Thickness { get; set; }
        bool Visible { get; set; }
        IHdEntity Copy();
        void Delete();
        void Erase();        
        void Move(CPoint3D FromPoint, CPoint3D ToPoint);
        void Rotate(CPoint3D BasePoint, double RotationAngle);
        void Rotate3D(CPoint3D Point1, CPoint3D Point2, double RotationAngle);
        void Update();

        void SetLayer(IHdLayer layer);
    }

    public interface IHdPoint : IHdEntity
    {
        CPoint3D[] Coordinates { get; set; }
        void set_Coordinate(int Index, CPoint3D pVal);
        CPoint3D get_Coordinate(int Index);
        //eHEADS_COLOR color { get; set; }
        //string Handle { get; }
        //int ObjectID { get; }
        //double Thickness { get; set; }
        //bool Visible { get; set; }
        //void Delete();
        //void Erase();
        //void Update();
    }
    public interface IHdLine : IHdEntity
    {
        double Angle { get; }
        //eHEADS_COLOR color { get; set; }
        //string Handle { get; }
        string Linetype { get; set; }
        CPoint3D StartPoint{ get; }
        CPoint3D EndPoint { get; }        
        //double LinetypeScale { get; set; }
        //eHEADS_LWEIGHT Lineweight { get; set; }
        //int ObjectID { get; }
        //double Thickness { get; set; }
        //bool Visible { get; set; }
    }
    public interface IHdArc : IHdEntity
    {
        CPoint3D CenterPoint{ get; set;}
        double StartAngle { get;}
        double EndAngle { get;}
        double Radius{ get;}
        //string Handle { get; }
        //int ObjectID { get; }
        //double Thickness
        //{
        //    get;
        //    set;
        //}
    }
    public interface IHdText: IHdEntity
    {
        CPoint3D InsertionPoint{ get; }
        double Height 
        { 
            get;
            set;
        }
        double Rotation
        {
            get;
            set;
        }
        string Text
        {
            get;
            set;
        }
        //int ObjectID { get; }
        //double Thickness
        //{
        //    get;
        //    set;
        //}        
    }
    public interface IHdEllipse : IHdEntity
    {
        CPoint3D CenterPoint { get; set;}
        double Radius { get;}
        //string Handle { get; }
        //int ObjectID { get; }
        //bool Visible
        //{
        //    get;
        //    set;
        //}        
    }
    public interface IHdPolyline3D : IHdEntity
    {
        bool Closed { get; set; }
        //eHEADS_COLOR color { get; set; }
        //string Handle { get; }
        //string Layer { get; set; }
        double Length { get; }
        CPoint3D[] Coordinates { get; set; }
        //eHEADS_LWEIGHT Lineweight { get; set; }
        //int ObjectID { get; }
        //bool Visible { get; set; }
        void AppendVertex(CPoint3D vertex);
        void RemoveVertex(int iIndex);
        CPoint3D get_Coordinate(int Index);
        void set_Coordinate(int Index, CPoint3D pVal);
    }
    public interface IHdLayer
    {
        eHEADS_COLOR color { get; set; }
        string Description { get; set; }
        bool Freeze { get; set; }
        string Handle { get; }
        bool LayerOn { get; set; }
        string Linetype { get; set; }
        eHEADS_LWEIGHT Lineweight { get; set; }
        bool Lock { get; set; }
        string Name { get; set; }
        bool Used { get; }

        void Delete();
        void Erase();        
    }
    public interface IHd3DFace : IHdEntity
    {
        CPoint3D[] Coordinates { get; set; }
        void set_Coordinate(int Index, CPoint3D pVal);
        CPoint3D get_Coordinate(int Index);
        //eHEADS_COLOR color { get; set; }
        //string Handle { get; }
        //string Layer { get; set; }
        //eHEADS_LWEIGHT Lineweight { get; set; }
        //int ObjectID { get; }
        //bool Visible { get; set; }

        //object Copy();
        //void Delete();
        //void Erase();
        //void Update();
    }
}
