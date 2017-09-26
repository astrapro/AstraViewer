using System;
using System.Collections.Generic;
using System.Text;

namespace HeadsUtils.Interfaces
{
    public interface IHeadsApplication
    {
        IHeadsDocument ActiveDocument{ get;}
        //string AppFolderPath { get;}
        string AppDataPath { get;set;}
        //void ZoomCentre(CPoint3D pt, double dMagnification);
        void ZoomExtents();
        void ZoomWindow(CPoint3D ptLowerLeft, CPoint3D ptUpperRight);
        eHEADS_RELEASE_TYPE ReleaseType{ get;} 
        string ApplicationName{ get;}
        string ApplicationTitle{ get;}
    }
}
