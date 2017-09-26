using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ComponentModel;
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
using System.IO;
using System.Drawing;
using HEADSNeed.ASTRA.ASTRAClasses;

namespace HEADSNeed
{
    public class SectionClip
    {
    }
    public class ActionSectionClip : ActionEntityEx
    {
        private vdLine figure;

        private vdSectionClip secClip;

        public ActionSectionClip(gPoint startPoint, vdLayout layout, gPoint endPoint)
            : base(startPoint, layout)
        {
            ValueTypeProp |= valueType.DISTANCE;
            figure = new vdLine();
            figure.SetUnRegisterDocument(layout.Document);
            figure.setDocumentDefaults();
            figure.StartPoint = startPoint;
            figure.EndPoint = startPoint;
            figure.ExtrusionVector = Vector.CreateExtrusion(startPoint, endPoint);
            figure.Thickness = startPoint.Distance3D(endPoint);
        }
        public static void CmdSectionClip_X(vdDocument doc)
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

            doc.Prompt("Direction Vector : ");
            ret = doc.ActionUtility.getUserPoint();
            doc.Prompt(null);
            if (ret == null || !(ret is gPoint)) return;
            endPt = ret as gPoint;

            secClip.Direction = new Vector(-1, 0, 0);
            doc.ActiveLayOut.Sections.AddItem(secClip);

            secClip = new vdSectionClip();
            secClip.SetUnRegisterDocument(doc);
            secClip.Name = "CLIP2";
            secClip.Enable = true;
            secClip.OriginPoint = endPt;
            secClip.Direction = new Vector(1, 0, 0);
            doc.ActiveLayOut.Sections.AddItem(secClip);
            doc.Redraw(true);
        }
        public static void CmdSectionClip_Y(vdDocument doc)
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

            doc.Prompt("Direction Vector : ");
            ret = doc.ActionUtility.getUserPoint();
            doc.Prompt(null);
            if (ret == null || !(ret is gPoint)) return;
            endPt = ret as gPoint;

            secClip.Direction = new Vector(0, -1, 0);
            doc.ActiveLayOut.Sections.AddItem(secClip);

            secClip = new vdSectionClip();
            secClip.SetUnRegisterDocument(doc);
            secClip.Name = "CLIP2";
            secClip.Enable = true;
            secClip.OriginPoint = endPt;
            secClip.Direction = new Vector(0, 1, 0);
            doc.ActiveLayOut.Sections.AddItem(secClip);
            doc.Redraw(true);
        }
        public static void CmdSectionClip_Z(vdDocument doc)
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

            doc.Prompt("Direction Vector : ");
            ret = doc.ActionUtility.getUserPoint();
            doc.Prompt(null);
            if (ret == null || !(ret is gPoint)) return;
            endPt = ret as gPoint;

            secClip.Direction = new Vector(0, 0, -1);
            doc.ActiveLayOut.Sections.AddItem(secClip);

            secClip = new vdSectionClip();
            secClip.SetUnRegisterDocument(doc);
            secClip.Name = "CLIP2";
            secClip.Enable = true;
            secClip.OriginPoint = endPt;
            secClip.Direction = new Vector(0, 0, 1);
            doc.ActiveLayOut.Sections.AddItem(secClip);
            doc.Redraw(true);
        }
        public static void CmdSectionClip_Clear(vdDocument doc)
        {
            doc.ActiveLayOut.Sections.RemoveAll();
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
            //secClip.Direction = new Vector(0, 0, 1);
            doc.Redraw(true);

            doc.Prompt("Direction Vector : ");
            ret = doc.ActionUtility.getUserPoint();
            doc.Prompt(null);
            if (ret == null || !(ret is gPoint)) return;
            endPt = ret as gPoint;

            secClip.Direction = new Vector(0, 0, -1);
            //secClip.Direction = new Vector(0, -1, 0);
            //secClip.Direction = Vector.CreateExtrusion(cen, endPt);
            doc.ActiveLayOut.Sections.AddItem(secClip);


            secClip = new vdSectionClip();
            secClip.SetUnRegisterDocument(doc);
            secClip.Name = "CLIP2";
            secClip.Enable = true;

            //doc.Prompt("Origin Point : ");
            //ret = doc.ActionUtility.getUserPoint();
            //doc.Prompt(null);
            //if (ret == null || !(ret is gPoint)) return;
            secClip.OriginPoint = endPt;
            //secClip.Direction = new Vector(0, 0, 1);
            //doc.Redraw(true);

            //doc.Prompt("Direction Vector : ");
            //ret = doc.ActionUtility.getUserPoint();
            //doc.Prompt(null);
            //if (ret == null || !(ret is gPoint)) return;
            //endPt = ret as gPoint;

            //secClip.Direction = new Vector(0, 1, 0);
            secClip.Direction = new Vector(0, 0, 1);


            //secClip.Direction = Vector.CreateExtrusion(endPt, cen);
            doc.ActiveLayOut.Sections.AddItem(secClip);
            doc.Redraw(true);

            //doc.ActiveLayOut.Sections.RemoveAll();
            //vdSectionClip clip = new vdSectionClip();
            //clip.SetUnRegisterDocument(doc);
            //clip.Name = "SLICE";
            //clip.Enable = true;
            //clip.OriginPoint = new gPoint(3.0, 3.5, 2.7);
            //clip.Direction = new Vector(0, 0, -1);  //This is the direction where we want the visible abjects to be.
            //doc.ActiveLayOut.Sections.AddItem(clip);

            ////We create another clip that will hide the front wall
            //clip = new vdSectionClip();
            //clip.SetUnRegisterDocument(doc);
            //clip.Name = "SLICE2";
            //clip.Enable = true;
            //clip.OriginPoint = new gPoint(3.0, 0.5, 0.0);
            //clip.Direction = new Vector(0, 1, 0);  //This is the direction where we want the visible abjects to be.
            //doc.ActiveLayOut.Sections.AddItem(clip);

            //doc.Redraw(true);

        }
        public static void CmdSectionClipRect(vdDocument doc)
        {
            //doc.ActiveLayOut.Sections.RemoveAll();
            //gPoint cen = new gPoint();
            //gPoint endPt = new gPoint();
            //vdSectionClip secClip = new vdSectionClip();
            //vdRect rect = new vdRect();

            //rect.SetUnRegisterDocument(doc);
            //rect.setDocumentDefaults();

            //doc.Prompt("Origin Point : ");
            //object ret = doc.ActionUtility.getUserPoint();
            //doc.Prompt(null);
            //if (ret == null || !(ret is gPoint)) return;
            //cen = ret as gPoint;

            //ret = doc.ActionUtility.getUserRect(cen);
            //if (ret == null || !(ret is vdRect)) return;
            //rect = ret as vdRect;
            //endPt = rect.getEndPoint();

            //secClip.SetUnRegisterDocument(doc);
            //secClip.Name = "CLIP1";
            //secClip.Enable = true;
            //secClip.OriginPoint = cen;
            //secClip.Direction = new Vector(0, -1, 0);
            //doc.ActiveLayOut.Sections.AddItem(secClip);

            //secClip = new vdSectionClip();
            //secClip.SetUnRegisterDocument(doc);
            //secClip.OriginPoint = endPt;
            //secClip.Name = "CLIP2";
            //secClip.Enable = true;
            //secClip.Direction = new Vector(0, 1, 0);
            //doc.ActiveLayOut.Sections.AddItem(secClip);



            //doc.Redraw(true);




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
            cen = ret as gPoint;
            secClip.OriginPoint = cen;

            doc.Prompt("Direction Vector : ");
            ret = doc.ActionUtility.getUserRect(secClip.OriginPoint);
            doc.Prompt(null);
            Vector v = ret as Vector;

            endPt = new gPoint((cen.x + v.y), (cen.y + v.z), (cen.z + v.x));

            secClip.Direction = new Vector(0, -1, 0);
            doc.ActiveLayOut.Sections.AddItem(secClip);

            secClip = new vdSectionClip();
            secClip.SetUnRegisterDocument(doc);
            secClip.Name = "CLIP2";
            secClip.Enable = true;
            secClip.OriginPoint = endPt;
            secClip.Direction = new Vector(0, 1, 0);
            doc.ActiveLayOut.Sections.AddItem(secClip);

            secClip = new vdSectionClip();
            secClip.SetUnRegisterDocument(doc);
            secClip.Name = "CLIP3";
            secClip.Enable = true;
            secClip.OriginPoint = cen;

            if (cen.x > endPt.x)
            {
                secClip.Direction = new Vector(-1, 0, 0);
            }
            else
            {
                secClip.Direction = new Vector(1, 0, 0);
            }
            doc.ActiveLayOut.Sections.AddItem(secClip);

            secClip = new vdSectionClip();
            secClip.SetUnRegisterDocument(doc);
            secClip.Name = "CLIP4";
            secClip.Enable = true;
            secClip.OriginPoint = endPt;
            if (cen.x > endPt.x)
            {
                secClip.Direction = new Vector(1, 0, 0);
            }
            else
            {
                secClip.Direction = new Vector(-1, 0, 0);
            }

            doc.ActiveLayOut.Sections.AddItem(secClip);


            doc.Redraw(true);
        }
        public static void CmdSectionClipAddAnother(vdDocument doc)
        {

            //doc.ActiveLayOut.Sections.RemoveAll();
            //gPoint cen = new gPoint();
            //gPoint endPt = new gPoint();
            //vdSectionClip secClip = new vdSectionClip();
           
            //secClip.SetUnRegisterDocument(doc);
            //secClip.Name = "CLIP1";
            //secClip.Enable = true;

            //doc.Prompt("Origin Point : ");
            //object ret = doc.ActionUtility.getUserPoint();
            //doc.Prompt(null);
            //if (ret == null || !(ret is gPoint)) return;
            //cen = ret as gPoint;
            //secClip.OriginPoint = cen;

            //doc.Prompt("Direction Vector : ");
            //ret = doc.ActionUtility.getUserRect(secClip.OriginPoint);
            //doc.Prompt(null);
            //Vector v = ret as Vector;

            //endPt = new gPoint((cen.x + v.y), (cen.y + v.z), (cen.z + v.x));

            //secClip.Direction = new Vector(0, -1, 0);
            //doc.ActiveLayOut.Sections.AddItem(secClip);

            //secClip = new vdSectionClip();
            //secClip.SetUnRegisterDocument(doc);
            //secClip.Name = "CLIP2";
            //secClip.Enable = true;
            //secClip.OriginPoint = endPt;
            //secClip.Direction = new Vector(0, 1, 0);
            //doc.ActiveLayOut.Sections.AddItem(secClip);

            //secClip = new vdSectionClip();
            //secClip.SetUnRegisterDocument(doc);
            //secClip.Name = "CLIP3";
            //secClip.Enable = true;
            //secClip.OriginPoint = cen;

            //if (cen.x > endPt.x)
            //{
            //    secClip.Direction = new Vector(-1, 0, 0);
            //}
            //else
            //{
            //    secClip.Direction = new Vector(1, 0, 0);
            //}
            //doc.ActiveLayOut.Sections.AddItem(secClip);

            //secClip = new vdSectionClip();
            //secClip.SetUnRegisterDocument(doc);
            //secClip.Name = "CLIP4";
            //secClip.Enable = true;
            //secClip.OriginPoint = endPt;
            //if (cen.x > endPt.x)
            //{
            //    secClip.Direction = new Vector(1, 0, 0);
            //}
            //else
            //{
            //    secClip.Direction = new Vector(-1, 0, 0);
            //} 
            
            //doc.ActiveLayOut.Sections.AddItem(secClip);


            //doc.Redraw(true);

            frmProperty frmProp = new frmProperty();
            frmProp.lay = doc.ActiveLayOut;
            frmProp.ShowDialog();
        }
        public static void CmdDrawMember(vdDocument doc)
        {
            //doc.Palette.Background = System.Drawing.Color.White;
            //doc.Palette.Forground = System.Drawing.Color.Black;
            doc.Redraw(true);

            JointCoordinateCollection jntCol;

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Text Files|*.txt";
                if (ofd.ShowDialog() != DialogResult.Cancel)
                {
                    MessageBox.Show(ofd.FileName);
                    jntCol = new JointCoordinateCollection(ofd.FileName);
                    
                }
            }

            //MessageBox.Show("Undert Construction");
        }


        //Hare Krishna Hare Krishna
        //Krishna Krishna Hare Hare

        //Hare Rama Hare Rama
        //Hare Rama Hare Rama
    }

}
