using System;
using System.Collections.Generic;
using System.Text;

namespace HeadsUtils
{
    public class CPoint3D
    {
        double m_dx = 0;
        double m_dy = 0;
        double m_dz = 0;
        public CPoint3D(double X, double Y, double Z)
        {
            m_dx = X;
            m_dy = Y;
            m_dz = Z;
        }
        public CPoint3D(double[] arr)
        {
            if (arr.Length == 3)
            {
                m_dx = arr[0];
                m_dy = arr[1];
                m_dz = arr[2];
            }            
        }
        public CPoint3D()
        {
            m_dx = 0;
            m_dy = 0;
            m_dz = 0;
        }
        public CPoint3D(CPoint3D pt)
        {
            m_dx = pt.m_dx;
            m_dy = pt.m_dy;
            m_dz = pt.m_dz;
        }
        public double[] GetCordArr()
        {
            double[] Points = new double[3];

            Points[0] = X;
            Points[1] = Y;
            Points[2] = Z;
            return Points;
        }
        public void CopyTo(CPoint3D obj)
        {
            obj.m_dx = m_dx;
            obj.m_dy = m_dy;
            obj.m_dz = m_dz;
        }
        

        public double X
        {
            get
            {
                return m_dx;
            }
            set
            {
                m_dx = value;
            }
        }
        public double Y
        {
            get
            {
                return m_dy;
            }
            set
            {
                m_dy = value;
            }
        }
        public double Z
        {
            get
            {
                if (m_dz < -999.0)
                    m_dz = 0.0;
                return m_dz;
            }
            set
            {
                m_dz = value;
            }
        }
    }

    public class CBox
    {
        public CBox(CPoint3D ptTopLeft, CPoint3D ptBottomRight)
        {
            this.TopLeft = ptTopLeft;
            this.BottomRight = ptBottomRight;
        }
        public CPoint3D TopLeft = new CPoint3D();
        public CPoint3D BottomRight = new CPoint3D();
        public CPoint3D TopRight
        {
            get
            {
                return new CPoint3D(BottomRight.X, TopLeft.Y, TopLeft.Z);
            }
        }
        public CPoint3D BottomLeft
        {
            get
            {
                return new CPoint3D(TopLeft.X, BottomRight.Y, BottomRight.Z);
            }
        }
    };

}
