using System;
using System.Collections.Generic;
using System.Text;
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
using HEADSNeed.ASTRA.ASTRADrawingTools;
namespace HEADSNeed.ALIGN
{
    public class Alignment
    {
        vdDocument document = null;
        public Alignment(vdDocument vDoc)
        {
            document = vDoc;
        }
        public static double GetAngle(gPoint pt1, gPoint pt2, bool BearingFlg /*= FALSE*/) //Optional 
        {
            double th;
            double temp;
            double B2 = 0;
            double x1 = pt1.x, y1 = pt1.y;
            double x2 = pt2.x, y2 = pt2.y;

            if (y1 == y2 && x2 > x1)
            {
                if (BearingFlg)
                    B2 = 90;
                else
                    B2 = 0;
            }
            else if (y1 == y2 && x2 < x1)
            {
                if (BearingFlg)
                    B2 = 270;
                else
                    B2 = 180;
            }
            else if (x1 == x2 && y2 > y1)
            {
                if (BearingFlg)
                    B2 = 0;
                else
                    B2 = 90;
            }
            else if (x1 == x2 && y2 < y1)
            {
                if (BearingFlg)
                    B2 = 180;
                else
                    B2 = 270;
            }
            else
            {
                temp = (y2 - y1) / (x2 - x1);
                th = Math.Atan(temp);
                th = Math.Abs((th * 180) / Math.PI);

                if (BearingFlg)
                {
                    if (x2 > x1 && y2 > y1)
                        B2 = 90 - th;
                    else if (x2 > x1 && y2 < y1)
                        B2 = 90 + th;
                    else if (x2 < x1 && y2 < y1)
                        B2 = 270 - th;
                    else if (x2 < x1 && y2 > y1)
                        B2 = 270 + th;
                }
                else
                {
                    if (x2 > x1 && y2 > y1)
                        B2 = th;
                    else if (x2 > x1 && y2 < y1)
                        B2 = 360 - th;
                    else if (x2 < x1 && y2 < y1)
                        B2 = 180 + th;
                    else if (x2 < x1 && y2 > y1)
                        B2 = 180 - th;
                }
            }


            if (BearingFlg)
            {
                if (B2 < 0)
                    B2 = B2 + 360;
                else if (B2 > 360)
                    B2 = B2 - 360;
            }

            return B2;
        }

        public void SetAlign()
        {
            vdFigure vdFig = null;
            vdLine stln = null;
            vdLine endln = null;
            object ret = null;
            gPoint EPT, SPT;
            gPoint FSP; // First Start Point
            gPoint FDP; // First Destination Point
            gPoint SSP; // Second Start Point
            gPoint SDP; // Second Destination Point

            vdSelection vdSel = null;

            document.Prompt("Select Entities:");
            ret = document.ActionUtility.getUserSelection();
            document.Prompt(null);
            if (ret == null || !(ret is vdSelection)) return;
            vdSel = ret as vdSelection;

            document.Prompt("Enter First Source Point:");
            ret = document.ActionUtility.getUserPoint();
            document.Prompt(null);
            if (ret == null || !(ret is gPoint)) return;
            FSP = ret as gPoint;

            document.Prompt("Enter First Destination Point:");
            ret = document.ActionUtility.getUserPoint();
            document.Prompt(null);
            if (ret == null || !(ret is gPoint)) return;
            FDP = ret as gPoint;

            document.Prompt("Enter Second Source Point:");
            ret = document.ActionUtility.getUserPoint();
            document.Prompt(null);
            if (ret == null || !(ret is gPoint)) return;
            SSP = ret as gPoint;

            document.Prompt("Enter Second Destination Point:");
            ret = document.ActionUtility.getUserPoint();
            document.Prompt(null);
            if (ret == null || !(ret is gPoint)) return;
            SDP = ret as gPoint;

            //endln.StartPoint = FSP
            //stln.StartPoint = FDP
            //endln.EndPoint = SSP
            //stln.EndPoint = SDP


            FSP.z = 0.0;
            SSP.z = 0.0;
            FDP.z = 0.0;
            SDP.z = 0.0;
            
            stln = new vdLine(FDP, SDP);
            endln = new vdLine(FSP, SSP);

            double scaleFactor = stln.Length() / endln.Length();
            document.CommandAction.CmdScale(vdSel, endln.StartPoint, scaleFactor);
            document.CommandAction.CmdMove(vdSel, endln.StartPoint, stln.StartPoint);
            double ang = stln.StartPoint.GetAngle(stln.EndPoint);
            double ang1 = endln.StartPoint.GetAngle(endln.EndPoint);
            ang = ang - ang1;
            document.CommandAction.CmdRotate(vdSel, stln.StartPoint, ang);
        }
    }
}
