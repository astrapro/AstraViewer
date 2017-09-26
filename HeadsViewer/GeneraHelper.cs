using System;
using System.Collections.Generic;
using System.Text;
using HeadsUtils;
using VectorDraw.Geometry;
using VectorDraw.Professional.vdObjects;

namespace HeadsViewer
{
    internal class GeneraHelper
    {
        private GeneraHelper()
        {

        }

        public static gPoint GetVDPoint(CPoint3D pt)
        {
            return new gPoint(pt.X, pt.Y, pt.Z);
        }

        public static vdColor ToVdColor(eHEADS_COLOR hdcolor)
        {
            return new vdColor(System.Drawing.Color.FromName(hdcolor.ToString()));
        }

        public static eHEADS_COLOR ToHdColor(vdColor vdcolor)
        {
            //eHEADS_COLOR hdcolorSele = eHEADS_COLOR.ByBlock;

            //for (int i = (int)eHEADS_COLOR.ByBlock; i < (int)eHEADS_COLOR.ByLayer; i++)
            //{
            //    eHEADS_COLOR hdcolor = (eHEADS_COLOR)i;
            //    if (hdcolor.ToString().ToLower() == vdcolor.ToString().ToLower())
            //    {
            //        hdcolorSele = hdcolor;
            //        break;
            //    }
            //}
            return (eHEADS_COLOR)vdcolor.ColorIndex;
        }
    }
}
