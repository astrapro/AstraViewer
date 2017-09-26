using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HeadsUtils.Interfaces;
using HeadsUtils;

namespace HeadsFunctions1
{
    internal class DrawingUtil
    {        
        private DrawingUtil()
        {

        }

        private static void XlateNmat(ref double x1, ref double y1, double XMetric, double YMetric)
        {
            x1 = x1 * XMetric;
            y1 = y1 * YMetric;
        } 

        private static void SetLayerToEntity(bool bEngFunc, IHdEntity hdEntity, IHeadsDocument doc, string strLayerName)
        {
            if (bEngFunc)
            {
                hdEntity.SetLayer(doc.GetActiveLayer());
            }
            else
            {
                if (strLayerName.Trim() != string.Empty)
                {
                    IHdLayer hdlayer = doc.AddLayer(strLayerName);
                    hdEntity.SetLayer(hdlayer);
                }                
            }
        }

        public static IHdPolyline3D DrawPolyline3DDx(IHeadsDocument doc, bool bEngFunc, CLinetype linetype)
        {
            List<CPoint3D> ptList = new List<CPoint3D>();
            ptList.Add(linetype.StartPoint);
            ptList.Add(linetype.EndPoint);

            IHdPolyline3D polyline = doc.DrawPolyline3D(ptList);

            DrawingUtil.SetLayerToEntity(bEngFunc, polyline, doc, linetype.Layer);
            polyline.Lineweight = eHEADS_LWEIGHT.LnWt000;
            polyline.Label = linetype.Label;

            return polyline;
        }

        public static IHdPolyline3D DrawRect(IHeadsDocument doc, CPoint3D ptBottomLeft, CPoint3D ptTopRight)
        {
            CPoint3D ptTopLeft = new CPoint3D();
            ptTopLeft.X = ptBottomLeft.X;
            ptTopLeft.Y = ptTopRight.Y;
            ptTopLeft.Z = ptTopRight.Z;

            CPoint3D ptBottomRight = new CPoint3D();
            ptBottomRight.X = ptTopRight.X;
            ptBottomRight.Y = ptBottomLeft.Y;
            ptBottomRight.Z = ptBottomLeft.Z;

            List<CPoint3D> listpt = new List<CPoint3D>();
            listpt.Add(ptBottomLeft);
            listpt.Add(ptTopLeft);
            listpt.Add(ptTopRight);
            listpt.Add(ptBottomRight);
            IHdPolyline3D polyrect = doc.DrawPolyline3D(listpt);
            polyrect.Closed = true;

            return polyrect;
        }

        public static IHdArc DrawArc(IHeadsDocument doc, double xs, double ys, double xe, double ye, double xc, double yc, double rad)
        {
            IHdArc hdarc = null;
            double PI = Math.Atan(1.0) * 4.0;
            double th1 = 0;
            double th2 = 0;

            double h = 0;
            double l = 0;

            if (xs > xe)
            {
                h = ys - yc;
                l = Math.Abs(xs - xc);
                th1 = Math.Atan(h / l);
                if (l == 0)
                {
                    return hdarc;
                }
                th1 = th1 * (180 / PI);

                l = Math.Abs(xc - xe);
                th2 = Math.Atan(h / l);
                th2 = th2 * (180 / PI);
                th2 = 180 - th2;
            }

            else if (xs <= xe) // turn = 1 left // = added to exclude everything
            {
                h = yc - ys;
                l = Math.Abs(xc - xs);
                if (l == 0)
                {
                    return hdarc;
                }
                th1 = Math.Atan(h / l);
                th1 = th1 * (180 / PI);
                th1 = 180 + th1;

                l = Math.Abs(xe - xc);
                if (l == 0)
                {
                    return hdarc;
                }

                th2 = Math.Atan(h / l);

                th2 = th2 * (180 / PI);
                th2 = 360 - th2;
            }

            th1 = th1 * PI / 180;
            th2 = th2 * PI / 180;
            hdarc = doc.DrawArc(new CPoint3D(xc, yc, 0), rad, th1, th2);

            return hdarc;
        }

        public static int DrawData(IHeadsApplication app, string szFileName, string sType, bool bEngFunc)
        {
            short eld_Code = 0;
            Linetype line;
            Boxtype box;
            Circletype circle1;
            Arctype arc;
            TEXTtype text;
            CCfgtype cfg;
            //	Cfgtype						cfg1;

            double last_x1 = 0;
            double last_y1 = 0;
            double last_x2 = 0;
            double last_y2 = 0;
            BinaryReader fhandle;	  //int fhandle;
            double LWidthDbl = 0;					//'/l->line
            double BWidthDbl = 0;					//'/b->box
            double AWidthDbl = 0;					//'/ a->text
            double CWidthDbl = 0;					//'/c->circle
            //double RotAngle = 0;
            IHdPolyline3D pLine = null;
            //	CvdSelectionSet	MySelSet; // d deb 30/aug/01
            //CVdView* drawing = NULL;				//frmDrawing drawing;
            //DrgReturn Retval;
            //int i = 0;
            CCfgtype tempcfg;
            //	CWaitCursor					wait;
            long NumPts = 0;



            //if (!bEngFunc)
            //{
            //    CDocument* pDocument = theApp.m_pDocTemplate->CreateNewDocument();

            //    BOOL bAutoDelete = pDocument->m_bAutoDelete;
            //    pDocument->m_bAutoDelete = FALSE;   // don't destroy if something goes wrong
            //    CFrameWnd* pFrame = theApp.m_pDocTemplate->CreateNewFrame(pDocument, NULL);
            //    pDocument->m_bAutoDelete = bAutoDelete;

            //    pDocument->SetPathName(szFileName);
            //    theApp.m_pDocTemplate->InitialUpdateFrame(pFrame, pDocument, TRUE);
            //}

            //drawing = (CVdView*)GetCurrentView();

            fhandle = new BinaryReader(new FileStream(szFileName, FileMode.Open), Encoding.Default);
            //fopen(szFileName, "rb");
            if (fhandle == null)
            {
                new Exception("Failed to open file : " + szFileName);
                return 0;
            }
            pLine = null;

            //	if(bEngFunc)
            cfg = app.ActiveDocument.ConfigParam;


            while (fhandle.BaseStream.Position < fhandle.BaseStream.Length)
            {
                eld_Code = fhandle.ReadInt16();

                if (eld_Code == 99)
                {
                    if (sType == "PVR" && (!bEngFunc))
                    {
                        cfg = CCfgtype.FromStream(fhandle);
                        app.ActiveDocument.ConfigParam = cfg;
                    }
                    else
                    {
                        tempcfg = CCfgtype.FromStream(fhandle);
                    }
                    //RotAngle = 0;
                    NumPts = 0;
                }
                else if (eld_Code == 1)
                {
                    line = Linetype.FromStream(fhandle);

                    DrawingUtil.XlateNmat(ref line.x1, ref line.y1, cfg.XMetric, cfg.YMetric);
                    DrawingUtil.XlateNmat(ref line.x2, ref line.y2, cfg.XMetric, cfg.YMetric);

                    LWidthDbl = (double)line.width;			//Convert result into double
                    NumPts = NumPts + 1;
                    if (line.scatt == 1)
                    {
                        line.style = (short)(line.style - 1); // to check this  d deb 30/aug/01
                        if (pLine == null)
                        {
                            List<CPoint3D> listpt = new List<CPoint3D>();
                            listpt.Add(new CPoint3D(line.x1, line.y1, line.z1));
                            listpt.Add(new CPoint3D(line.x2, line.y2, line.z2));

                            pLine = app.ActiveDocument.DrawPolyline3D(listpt);

                            DrawingUtil.SetLayerToEntity(bEngFunc, pLine, app.ActiveDocument, line.layer);

                        }
                        else
                        {
                            pLine.AppendVertex(new CPoint3D(line.x2, line.y2, line.z2));
                        }
                    }
                    last_x1 = line.x1;
                    last_y1 = line.y1;
                    last_x2 = line.x2;
                    last_y2 = line.y2;
                }
                else if (eld_Code == 2)
                {
                    box = Boxtype.FromStream(fhandle);

                    box.style = (short)(box.style - 1);

                    DrawingUtil.XlateNmat(ref box.x1, ref box.y1, cfg.XMetric, cfg.YMetric);
                    DrawingUtil.XlateNmat(ref box.x2, ref box.y2, cfg.XMetric, cfg.YMetric);


                    BWidthDbl = (double)(box.Width);
                    if (box.scatt == 1)
                    {
                        IHdPolyline3D polyRect = DrawingUtil.DrawRect(app.ActiveDocument
                            , new CPoint3D(box.x1, box.y1, 0)
                            , new CPoint3D(box.x2, box.y2, 0));

                        DrawingUtil.SetLayerToEntity(bEngFunc, polyRect, app.ActiveDocument, box.Layer);
                    }
                }
                else if (eld_Code == 3)
                {
                    circle1 = Circletype.FromStream(fhandle);

                    circle1.style = (short)(circle1.style - 1);

                    DrawingUtil.XlateNmat(ref circle1.xc, ref circle1.yc, cfg.XMetric, cfg.YMetric);


                    CWidthDbl = (double)(circle1.Width);

                    if (circle1.scatt == 1)
                    {
                        IHdEllipse hdellipse = app.ActiveDocument.DrawEllipse(new CPoint3D(circle1.xc, circle1.yc, 0), circle1.Radius);
                        DrawingUtil.SetLayerToEntity(bEngFunc, hdellipse, app.ActiveDocument, circle1.Layer);
                        //DrawCircle(bEngFunc, circle1.xc, circle1.yc, circle1.Radius, circle1.Color, circle1.style, CWidthDbl, circle1.Label, circle1.Layer, RotAngle, sType);
                    }
                }
                else if (eld_Code == 4)
                {
                    arc = Arctype.FromStream(fhandle);

                    arc.style = (short)(arc.style - 1);

                    DrawingUtil.XlateNmat(ref arc.xc, ref arc.yc, cfg.XMetric, cfg.YMetric);
                    DrawingUtil.XlateNmat(ref arc.xs, ref arc.ys, cfg.XMetric, cfg.YMetric);
                    DrawingUtil.XlateNmat(ref arc.xe, ref arc.ye, cfg.XMetric, cfg.YMetric);


                    AWidthDbl = (double)(arc.Width);
                    if (arc.scatt != 0)
                    {
                        IHdArc hdarc = DrawingUtil.DrawArc(app.ActiveDocument, arc.xs, arc.ys, arc.xe, arc.ye, arc.xc, arc.yc, arc.Radius);
                        DrawingUtil.SetLayerToEntity(bEngFunc, hdarc, app.ActiveDocument, arc.Layer);
                        //DrawArc(bEngFunc, arc.xs, arc.ys, arc.xe, arc.ye, arc.xc, arc.yc, arc.Radius, arc.Color, arc.style, AWidthDbl, arc.Label, arc.Layer, RotAngle, sType);
                    }
                }
                else if (eld_Code == 9)
                {
                    text = TEXTtype.FromStream(fhandle);

                    text.style = (short)(text.style - 1);

                    DrawingUtil.XlateNmat(ref text.x1, ref text.y1, cfg.XMetric, cfg.YMetric);


                    if (text.scatt != 0)
                    {
                        IHdText hdtext = app.ActiveDocument.DrawText(new CPoint3D(text.x1, text.y1, 0), text.text, text.Size);
                        DrawingUtil.SetLayerToEntity(bEngFunc, hdtext, app.ActiveDocument, text.Layer);

                    }
                }

            }
            fhandle.Close();

            return pLine.ObjectID;
        }

        public static void DrawChainage(IHeadsApplication app, string szFileName, string sType, bool bEngFunc)
        {
            // 11/09/01
            Eldtype eld = new Eldtype();
            Linetype line = new Linetype();
            Boxtype box = new Boxtype();
            Circletype circle1 = new Circletype();
            Arctype arc = new Arctype();
            TEXTtype text = new TEXTtype();
            CCfgtype cfg = new CCfgtype();


            BinaryReader fhandle;					//int fhandle;
            double LWidthDbl = 0;					//'/l->line
            double BWidthDbl;					//'/b->box
            double AWidthDbl;					//'/ a->text
            double CWidthDbl;					//'/c->circle
            //double						RotAngle = 0;

            //IHdPolyline3D		pLine;    // no plines req d deb 
            //CvdSelection	MySelSet; // renentry d deb 11/09/01
            //CVdView						*drawing = NULL;	//frmDrawing drawing;
            //DrgReturn					Retval;
            //int							i = 0;	
            CCfgtype tempcfg = new CCfgtype();
            long NumPts = 0;


            //if(!bEngFunc)
            //{
            //    CDocument* pDocument = theApp.m_pDocTemplate->CreateNewDocument();

            //    BOOL bAutoDelete = pDocument->m_bAutoDelete;
            //    pDocument->m_bAutoDelete = FALSE;   // don't destroy if something goes wrong
            //    CFrameWnd* pFrame = theApp.m_pDocTemplate->CreateNewFrame(pDocument, NULL);
            //    pDocument->m_bAutoDelete = bAutoDelete;

            //    pDocument->SetPathName(szFileName);
            //    theApp.m_pDocTemplate->InitialUpdateFrame(pFrame, pDocument, TRUE);
            //}

            //drawing = (CVdView*)GetCurrentView();

            if (File.Exists(szFileName) == false)
            {
                return;
            }
            fhandle = new BinaryReader(new FileStream(szFileName, FileMode.Open), Encoding.Default);

            //	if(bEngFunc)
            cfg = app.ActiveDocument.ConfigParam;

            while (fhandle.BaseStream.Position < fhandle.BaseStream.Length)
            {
                eld.Code = fhandle.ReadInt16();

                if (eld.Code == 99)
                {
                    if (sType == "PVR" && (!bEngFunc))
                    {
                        cfg = CCfgtype.FromStream(fhandle);
                        app.ActiveDocument.ConfigParam = cfg;
                    }
                    else
                    {
                        tempcfg = CCfgtype.FromStream(fhandle);
                    }
                    //RotAngle = 0;
                    NumPts = 0;
                }
                else if (eld.Code == 1) // line
                {
                    line = Linetype.FromStream(fhandle);

                    XlateNmat(ref line.x1, ref line.y1, cfg.XMetric, cfg.YMetric);
                    XlateNmat(ref line.x2, ref line.y2, cfg.XMetric, cfg.YMetric);


                    LWidthDbl = (double)(line.width);			//Convert to double
                    NumPts = NumPts + 1;

                    if (line.scatt == 1)
                    {
                        IHdLine Entity = app.ActiveDocument.DrawLine(new CPoint3D(line.x1, line.y1, line.z1), new CPoint3D(line.x2, line.y2, line.z2));

                        if (Entity != null)
                        {
                            if (!bEngFunc)
                            {
                                string layerName = line.layer;
                                DrawingUtil.SetLayerToEntity(bEngFunc, Entity, app.ActiveDocument, layerName);

                                //Entity.SetPenStyle(line.style);
                                //Entity.SetPenWidth(LWidthDbl);
                            }
                        }

                        Entity.Label = sType;
                    }
                }
                else if (eld.Code == 2)
                {
                    box = Boxtype.FromStream(fhandle);

                    box.style = (short)(box.style - 1);

                    XlateNmat(ref box.x1, ref box.y1, cfg.XMetric, cfg.YMetric);
                    XlateNmat(ref box.x2, ref box.y2, cfg.XMetric, cfg.YMetric);

                    BWidthDbl = (double)(box.Width);

                    if (box.scatt == 1)
                    {
                        IHdPolyline3D polyRect = DrawingUtil.DrawRect(app.ActiveDocument
                            , new CPoint3D(box.x1, box.y1, 0)
                            , new CPoint3D(box.x2, box.y2, 0));

                        DrawingUtil.SetLayerToEntity(bEngFunc, polyRect, app.ActiveDocument, box.Layer);

                        polyRect.Label = sType;
                    }
                }
                else if (eld.Code == 3)
                {
                    circle1 = Circletype.FromStream(fhandle);

                    circle1.style = (short)(circle1.style - 1);
                    XlateNmat(ref circle1.xc, ref circle1.yc, cfg.XMetric, cfg.YMetric);

                    CWidthDbl = (double)(circle1.Width);

                    if (circle1.scatt == 1)
                    {
                        IHdEllipse hdellipse = app.ActiveDocument.DrawEllipse(new CPoint3D(circle1.xc, circle1.yc, 0), circle1.Radius);
                        DrawingUtil.SetLayerToEntity(bEngFunc, hdellipse, app.ActiveDocument, circle1.Layer);

                        hdellipse.Label = sType;
                    }
                }
                else if (eld.Code == 4)
                {
                    arc = Arctype.FromStream(fhandle);

                    arc.style = (short)(arc.style - 1);

                    XlateNmat(ref arc.xc, ref arc.yc, cfg.XMetric, cfg.YMetric);
                    XlateNmat(ref arc.xs, ref arc.ys, cfg.XMetric, cfg.YMetric);
                    XlateNmat(ref arc.xe, ref arc.ye, cfg.XMetric, cfg.YMetric);

                    AWidthDbl = (double)(arc.Width);

                    if (arc.scatt != 0)
                    {
                        IHdArc hdarc = DrawingUtil.DrawArc(app.ActiveDocument, arc.xs, arc.ys, arc.xe, arc.ye, arc.xc, arc.yc, arc.Radius);
                        DrawingUtil.SetLayerToEntity(bEngFunc, hdarc, app.ActiveDocument, arc.Layer);

                        hdarc.Label = sType;
                    }
                }
                else if (eld.Code == 9)
                {
                    text = TEXTtype.FromStream(fhandle);

                    text.style = (short)(text.style - 1);

                    XlateNmat(ref text.x1, ref text.y1, cfg.XMetric, cfg.YMetric);

                    if (text.scatt != 0)
                    {
                        IHdText hdtext = app.ActiveDocument.DrawText(new CPoint3D(text.x1, text.y1, 0), text.text, text.Size);
                        DrawingUtil.SetLayerToEntity(bEngFunc, hdtext, app.ActiveDocument, text.Layer);

                        hdtext.Rotate(new CPoint3D(text.x1, text.y1, 0), ViewerUtils.DegreeToRadian(text.rotn));
                        hdtext.Label = sType;
                        //DrawText(bEngFunc, text.text, text.x1, text.y1, text.Size, text.Color, text.style, text.Label, text.Layer, text.rotn, sType);
                    }
                }
            }

            fhandle.Close();
        }
        
        public static void DrawDrg(IHeadsApplication app, string szFileName, string sType, bool bEngFunc)
        {
            if (File.Exists(szFileName) == false)
            {
                return;
            }

            Eldtype eld = new Eldtype();
            Linetype line = new Linetype();
            Boxtype box = new Boxtype();
            Circletype circle1 = new Circletype();
            Arctype arc = new Arctype();
            TEXTtype text = new TEXTtype();
            CCfgtype cfg = new CCfgtype();
            //	Cfgtype						cfg1;

            double last_x1;
            double last_y1;
            double last_x2;
            double last_y2;
            BinaryReader fhandle;					//int fhandle;
            double LWidthDbl = 0;					//'/l->line
            double BWidthDbl;					//'/b->box
            double AWidthDbl;					//'/ a->text
            double CWidthDbl;					//'/c->circle
            //double RotAngle = 0;
            IHdPolyline3D pLine;


            //DrgReturn Retval;
            //int i = 0;
            CCfgtype tempcfg = new CCfgtype();
            //	CWaitCursor					wait;
            long NumPts = 0;


            //if (!bEngFunc)
            //{
            //    CDocument* pDocument = theApp.m_pDocTemplate->CreateNewDocument();

            //    BOOL bAutoDelete = pDocument->m_bAutoDelete;
            //    pDocument->m_bAutoDelete = FALSE;   // don't destroy if something goes wrong
            //    CFrameWnd* pFrame = theApp.m_pDocTemplate->CreateNewFrame(pDocument, NULL);
            //    pDocument->m_bAutoDelete = bAutoDelete;

            //    pDocument->SetPathName(szFileName);
            //    theApp.m_pDocTemplate->InitialUpdateFrame(pFrame, pDocument, TRUE);
            //}


            fhandle = new BinaryReader(new FileStream(szFileName, FileMode.Open), Encoding.Default);

            pLine = null;

            cfg = app.ActiveDocument.ConfigParam;

            while (fhandle.BaseStream.Position < fhandle.BaseStream.Length)
            {
                eld.Code = 0;
                eld.Code = fhandle.ReadInt16();

                if (eld.Code == 99)
                {
                    if (sType == "PVR" && (!bEngFunc))
                    {
                        cfg = CCfgtype.FromStream(fhandle);
                        app.ActiveDocument.ConfigParam = cfg;
                    }
                    else
                    {
                        tempcfg = CCfgtype.FromStream(fhandle);
                    }
                    //RotAngle = 0;
                    NumPts = 0;
                }
                else if (eld.Code == 1)
                {
                    line = Linetype.FromStream(fhandle);

                    XlateNmat(ref line.x1, ref line.y1, cfg.XMetric, cfg.YMetric);
                    XlateNmat(ref line.x2, ref line.y2, cfg.XMetric, cfg.YMetric);

                    LWidthDbl = (double)(line.width);			//Convert result into double
                    NumPts = NumPts + 1;
                    if (line.scatt == 1)
                    {
                        line.style = (short)(line.style - 1); // to check this  d deb 30/aug/01
                        if (pLine == null)
                        {
                            List<CPoint3D> listpt = new List<CPoint3D>();
                            listpt.Add(new CPoint3D(line.x1, line.y1, line.z1));
                            listpt.Add(new CPoint3D(line.x2, line.y2, line.z2));

                            pLine = app.ActiveDocument.DrawPolyline3D(listpt);

                            DrawingUtil.SetLayerToEntity(bEngFunc, pLine, app.ActiveDocument, line.layer);

                            pLine.Label = sType;
                        }
                        else
                        {
                            pLine.AppendVertex(new CPoint3D(line.x2, line.y2, line.z2));
                        }

                    }
                    last_x1 = line.x1;
                    last_y1 = line.y1;
                    last_x2 = line.x2;
                    last_y2 = line.y2;
                }
                else if (eld.Code == 2)
                {
                    box = Boxtype.FromStream(fhandle);
                    box.style = (short)(box.style - 1);

                    XlateNmat(ref box.x1, ref box.y1, cfg.XMetric, cfg.YMetric);
                    XlateNmat(ref box.x2, ref box.y2, cfg.XMetric, cfg.YMetric);

                    BWidthDbl = (double)(box.Width);
                    if (box.scatt == 1)
                    {
                        IHdPolyline3D polyRect = DrawingUtil.DrawRect(app.ActiveDocument
                            , new CPoint3D(box.x1, box.y1, 0)
                            , new CPoint3D(box.x2, box.y2, 0));

                        DrawingUtil.SetLayerToEntity(bEngFunc, polyRect, app.ActiveDocument, box.Layer);

                        polyRect.Label = sType;
                    }
                }
                else if (eld.Code == 3)
                {
                    circle1 = Circletype.FromStream(fhandle);

                    circle1.style = (short)(circle1.style - 1);
                    XlateNmat(ref circle1.xc, ref circle1.yc, cfg.XMetric, cfg.YMetric);

                    CWidthDbl = (double)(circle1.Width);

                    if (circle1.scatt == 1)
                    {
                        IHdEllipse hdellipse = app.ActiveDocument.DrawEllipse(new CPoint3D(circle1.xc, circle1.yc, 0), circle1.Radius);
                        DrawingUtil.SetLayerToEntity(bEngFunc, hdellipse, app.ActiveDocument, circle1.Layer);

                        circle1.Label = sType;
                    }
                }
                else if (eld.Code == 4)
                {
                    arc = Arctype.FromStream(fhandle);

                    arc.style = (short)(arc.style - 1);

                    XlateNmat(ref arc.xc, ref arc.yc, cfg.XMetric, cfg.YMetric);
                    XlateNmat(ref arc.xs, ref arc.ys, cfg.XMetric, cfg.YMetric);
                    XlateNmat(ref arc.xe, ref arc.ye, cfg.XMetric, cfg.YMetric);

                    AWidthDbl = (double)(arc.Width);
                    if (arc.scatt != 0)
                    {
                        IHdArc hdarc = DrawingUtil.DrawArc(app.ActiveDocument, arc.xs, arc.ys, arc.xe, arc.ye, arc.xc, arc.yc, arc.Radius);
                        DrawingUtil.SetLayerToEntity(bEngFunc, hdarc, app.ActiveDocument, arc.Layer);
                        hdarc.Label = sType;
                    }
                }
                else if (eld.Code == 9)
                {
                    text = TEXTtype.FromStream(fhandle);
                    text.style = (short)(text.style - 1);

                    XlateNmat(ref text.x1, ref text.y1, cfg.XMetric, cfg.YMetric);

                    if (text.scatt != 0)
                    {
                        IHdText hdtext = app.ActiveDocument.DrawText(new CPoint3D(text.x1, text.y1, 0), text.text, text.Size);
                        DrawingUtil.SetLayerToEntity(bEngFunc, hdtext, app.ActiveDocument, text.Layer);

                        hdtext.Rotate(new CPoint3D(text.x1, text.y1, 0), text.rotn);

                        hdtext.Label = sType;
                    }
                }

            }
            fhandle.Close();
        }

        private static DrgEleType GetDrgEleType(short type)
        {
            DrgEleType eletype = DrgEleType.Unknown;

            //Chiranjit [2011 06 09]
            switch (type)
            {
                case 1:
                case 91:
                    {
                        eletype = DrgEleType.LINE;
                        break;
                    }
                case 3:
                case 93:
                    {
                        eletype = DrgEleType.ELLIPSE;
                        break;
                    }
                case 4:
                case 94:
                    {
                        eletype = DrgEleType.ARC;
                        break;
                    }
                case 9:
                case 90:
                    {
                        eletype = DrgEleType.TEXT;
                        break;
                    }
                case 99:
                    {
                        eletype = DrgEleType.CONFIG;
                        break;
                    }
            }

            //if (Constants.BuildType != eHEADS_RELEASE_TYPE.DEMO)
            //if (Constants.BuildType == eHEADS_RELEASE_TYPE.DEMO)
            //{
            //    switch (type)
            //    {
            //        case 1:
            //            {
            //                eletype = DrgEleType.LINE;
            //                break;
            //            }
            //        case 3:
            //            {
            //                eletype = DrgEleType.ELLIPSE;
            //                break;
            //            }
            //        case 4:
            //            {
            //                eletype = DrgEleType.ARC;
            //                break;
            //            }
            //        case 9:
            //            {
            //                eletype = DrgEleType.TEXT;
            //                break;
            //            }
            //        case 99:
            //            {
            //                eletype = DrgEleType.CONFIG;
            //                break;
            //            }
            //    }
            //}
            //else
            //{
            //    switch (type)
            //    {
            //        case 1:
            //        case 91:
            //            {
            //                eletype = DrgEleType.LINE;
            //                break;
            //            }
            //        case 3:
            //        case 93:
            //            {
            //                eletype = DrgEleType.ELLIPSE;
            //                break;
            //            }
            //        case 4:
            //        case 94:
            //            {
            //                eletype = DrgEleType.ARC;
            //                break;
            //            }
            //        case 9:
            //        case 90:
            //            {
            //                eletype = DrgEleType.TEXT;
            //                break;
            //            }
            //        case 99:
            //            {
            //                eletype = DrgEleType.CONFIG;
            //                break;
            //            }
            //    }
            //}


            return eletype;
        }

        public static void ExportToDRG(IHeadsApplication app, string strPath)
        {
            CCfgtype cfg = app.ActiveDocument.ConfigParam;

            CBox box = app.ActiveDocument.BoundingBox;
            double[] Arr = new double[] { box.BottomRight.X, box.BottomRight.Y, box.TopLeft.X, box.TopLeft.Y };

            cfg.XLate_X = Arr[0];
            cfg.XLate_Y = Arr[1];
            cfg.hs = (297 / (Arr[2] - cfg.XLate_X));

            BinaryWriter bw = new BinaryWriter(new FileStream(strPath, FileMode.Create), Encoding.Default);
            Eldtype eld = new Eldtype();
            eld.Code = 99;
            bw.Write(eld.Code);
            cfg.ToStream(bw);
            IHdEntity[] allEntities = app.ActiveDocument.Entities;
            foreach (IHdEntity entity in allEntities)
            {
                if (entity.EntityName == "LINE")
                {
                    IHdLine hdline = (IHdLine)entity;
                    eld.Code = (short)((Constants.BuildType == eHEADS_RELEASE_TYPE.DEMO) ? 91 : 1);
                    bw.Write(eld.Code);

                    Linetype line = new Linetype();

                    line.elatt = 0;
                    line.scatt = 1;
                    line.layer = hdline.Layer;
                    line.laatt = 1;

                    line.x1 = hdline.StartPoint.X;
                    line.y1 = hdline.StartPoint.Y;
                    line.z1 = hdline.StartPoint.Z;

                    line.x1 = line.x1 / cfg.XMetric;
                    line.y1 = line.y1 / cfg.YMetric;

                    line.x2 = hdline.EndPoint.X;
                    line.y2 = hdline.EndPoint.Y;
                    line.z2 = hdline.EndPoint.Z;

                    line.x2 = line.x2 / cfg.XMetric;
                    line.y2 = line.y2 / cfg.YMetric;

                    line.color = entity.ColorIndex;

                    if (hdline.Label == "")
                    {
                        line.label = "$$$$$$$$:****";
                    }
                    else
                    {
                        line.label = hdline.Label;
                    }

                    line.ToStream(bw);
                }
                else if (entity.EntityName == "ELLIPSE")
                {
                    IHdEllipse hdEllipse = (IHdEllipse)entity;
                    eld.Code = (short)((Constants.BuildType == eHEADS_RELEASE_TYPE.DEMO) ? 93 : 3);
                    bw.Write(eld.Code);

                    Circletype circle1 = new Circletype();

                    circle1.elatt = 0;
                    circle1.scatt = 1;
                    circle1.Layer = hdEllipse.Layer;
                    circle1.laatt = 1;

                    circle1.xc = hdEllipse.CenterPoint.X;
                    circle1.yc = hdEllipse.CenterPoint.Y;

                    circle1.xc = circle1.xc / cfg.XMetric;
                    circle1.yc = circle1.yc / cfg.YMetric;
                    circle1.Radius = hdEllipse.Radius;

                    circle1.color = entity.ColorIndex;

                    if (hdEllipse.Label == "")
                    {
                        circle1.Label = "$$$$$$$$:****";
                    }
                    else
                    {
                        circle1.Label = hdEllipse.Label;
                    }

                    circle1.ToStream(bw);
                }
                else if (entity.EntityName == "ARC")
                {
                    IHdArc hdArc = (IHdArc)entity;
                    eld.Code = (short)((Constants.BuildType == eHEADS_RELEASE_TYPE.DEMO) ? 94 : 4);
                    bw.Write(eld.Code);

                    Arctype arc = new Arctype();

                    arc.elatt = 0;
                    arc.scatt = 1;
                    arc.Layer = hdArc.Layer;
                    arc.laatt = 1;

                    arc.xc = hdArc.CenterPoint.X;
                    arc.yc = hdArc.CenterPoint.Y;
                    arc.xc = arc.xc / cfg.XMetric;
                    arc.yc = arc.yc / cfg.YMetric;

                    arc.xs = (arc.xc + hdArc.Radius * Math.Cos(hdArc.StartAngle)) / cfg.XMetric;
                    arc.ys = (arc.yc + hdArc.Radius * Math.Sin(hdArc.StartAngle)) / cfg.YMetric;
                    arc.xe = (arc.xc + hdArc.Radius * Math.Cos(hdArc.EndAngle)) / cfg.XMetric;
                    arc.ye = (arc.yc + hdArc.Radius * Math.Sin(hdArc.EndAngle)) / cfg.YMetric;
                    arc.Radius = hdArc.Radius;

                    arc.Color = entity.ColorIndex;
                    if (hdArc.Label == "")
                    {
                        arc.Label = "$$$$$$$$:****";
                    }
                    else
                    {
                        arc.Label = hdArc.Label;
                    }
                    arc.ToStream(bw);
                }
                else if (entity.EntityName == "POLYLINE3D")
                {
                    IHdPolyline3D hdPline = (IHdPolyline3D)entity;
                    CPoint3D[] Coord = hdPline.Coordinates;
                    int NumVerticies = Coord.Length - 2;
                    for (int j = 0; j <= NumVerticies; j++)
                    {
                        eld.Code = (short)((Constants.BuildType == eHEADS_RELEASE_TYPE.DEMO) ? 91 : 1);
                        bw.Write(eld.Code);

                        Linetype line = new Linetype();

                        if (j == 0)
                            line.elatt = 0;
                        else
                            line.elatt = 1;

                        //line.elatt = X;
                        line.scatt = 1;
                        line.layer = hdPline.Layer;
                        line.laatt = 1;

                        line.x1 = Coord[j].X;
                        line.y1 = Coord[j].Y;
                        line.z1 = Coord[j].Z;

                        line.x1 = line.x1 / cfg.XMetric;
                        line.y1 = line.y1 / cfg.YMetric;

                        line.x2 = Coord[j + 1].X;
                        line.y2 = Coord[j + 1].Y;
                        line.z2 = Coord[j + 1].Z;

                        line.x2 = line.x2 / cfg.XMetric;
                        line.y2 = line.y2 / cfg.YMetric;
                        line.color = entity.ColorIndex;

                        if (hdPline.Label == "")
                        {
                            line.label = "$$$$$$$$:****";
                        }
                        else
                        {
                            line.label = hdPline.Label;
                        }

                        line.ToStream(bw);
                    }
                }
                else if (entity.EntityName == "TEXT")
                {
                    IHdText hdTxt = (IHdText)entity;
                    eld.Code = (short)((Constants.BuildType == eHEADS_RELEASE_TYPE.DEMO) ? 90 : 9);
                    bw.Write(eld.Code);

                    TEXTtype text = new TEXTtype();

                    text.Length = 1;
                    text.scatt = 1;
                    text.Layer = hdTxt.Layer;
                    text.laatt = 1;

                    text.x1 = hdTxt.InsertionPoint.X;
                    text.y1 = hdTxt.InsertionPoint.Y;

                    text.x1 = text.x1 / cfg.XMetric;
                    text.y1 = text.y1 / cfg.YMetric;
                    text.Size = hdTxt.Height;
                    text.rotn = hdTxt.Rotation * 180 / Math.PI;
                    text.text = hdTxt.Text;
                    text.Color = entity.ColorIndex;
                    if (hdTxt.Label == "")
                    {
                        text.Label = "$$$$$$$$:****";
                    }
                    else
                    {
                        text.Label = hdTxt.Label;
                    }
                    text.ToStream(bw);
                }
            }

            bw.Close();
        }

        public static void ImportFromDRG(IHeadsApplication app, string strPath, string sType, bool bEngFunc)
        {
            app.ActiveDocument.DisableUndoRecording(true);

            CCfgtype cfg = app.ActiveDocument.ConfigParam;
            Linetype line = null;
            Circletype circle1 = null;
            TEXTtype text = null;
            Arctype arc = null;
            BinaryReader br = new BinaryReader(new FileStream(strPath, FileMode.Open, FileAccess.Read), Encoding.Default);
            Eldtype eld = new Eldtype();
            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                eld.Code = br.ReadInt16();
                if (GetDrgEleType(eld.Code) == DrgEleType.CONFIG)
                {
                    if (sType == "PVR" && (!bEngFunc))
                    {
                        cfg = CCfgtype.FromStream(br);
                        app.ActiveDocument.ConfigParam = cfg;
                    }
                    else
                    {
                        CCfgtype tempcfg = CCfgtype.FromStream(br);
                    }
                }
                else if (GetDrgEleType(eld.Code) == DrgEleType.LINE)
                {
                    line = Linetype.FromStream(br);


                    XlateNmat(ref line.x1, ref line.y1, cfg.XMetric, cfg.YMetric);
                    XlateNmat(ref line.x2, ref line.y2, cfg.XMetric, cfg.YMetric);

                    //LWidthDbl = (double)(line.Width);			//Convert result into double :::why?
                    //NumPts = NumPts + 1;
                    if (line.scatt == 1)
                    {
                        line.style = (short)(line.style - 1);
                        IHdLine aLine = app.ActiveDocument.DrawLine(line.StartPoint, line.EndPoint);
                        aLine.ColorIndex = line.color;
                        SetLayerToEntity(bEngFunc, aLine, app.ActiveDocument, line.layer);       

                    }//end if(line.scatt == 1)
                }
                else if (GetDrgEleType(eld.Code) == DrgEleType.ELLIPSE)
                {
                    circle1 = Circletype.FromStream(br);

                    circle1.style = (short)(circle1.style - 1);
                    //			circle1.Color = HeadsToVDraw(circle1.Color);
                    XlateNmat(ref circle1.xc, ref circle1.yc, cfg.XMetric, cfg.YMetric);

                    //CWidthDbl = (double)(circle1.Width);

                    if (circle1.scatt == 1)
                    {
                        IHdEllipse ellipse = app.ActiveDocument.DrawEllipse(new CPoint3D(circle1.xc, circle1.yc, 0), circle1.Radius);
                        //DrawCircle(bEngFunc, circle1.xc, circle1.yc, circle1.Radius, circle1.Color, circle1.style, CWidthDbl, circle1.Label, circle1.Layer, RotAngle, sType);

                        SetLayerToEntity(bEngFunc, ellipse, app.ActiveDocument, circle1.Layer);

                        ellipse.Label = circle1.Label;
                        ellipse.ColorIndex = circle1.color;
                    }
                }
                else if (GetDrgEleType(eld.Code) == DrgEleType.ARC)
                {
                    arc = Arctype.FromStream(br);

                    arc.style = (short)(arc.style - 1);
                    //			arc.Color = HeadsToVDraw(arc.Color);
                    XlateNmat(ref arc.xc, ref arc.yc, cfg.XMetric, cfg.YMetric);
                    XlateNmat(ref arc.xs, ref arc.ys, cfg.XMetric, cfg.YMetric);
                    XlateNmat(ref arc.xe, ref arc.ye, cfg.XMetric, cfg.YMetric);

                    //AWidthDbl = (double)(arc.Width);
                    if (arc.scatt == 1)
                    {
                        IHdArc hdarc = DrawArc(app.ActiveDocument, arc.xs, arc.ys, arc.xe, arc.ye, arc.xc, arc.yc, arc.Radius);
                        SetLayerToEntity(bEngFunc, hdarc, app.ActiveDocument, arc.Layer);

                        hdarc.Label = arc.Label; 
                        hdarc.ColorIndex = arc.Color;
                    }
                }
                else if (GetDrgEleType(eld.Code) == DrgEleType.TEXT)
                {
                    text = TEXTtype.FromStream(br);

                    text.style = (short)(text.style - 1);

                    XlateNmat(ref text.x1, ref text.y1, cfg.XMetric, cfg.YMetric);

                    if (text.scatt != 0)
                    {
                        IHdText hdtext = app.ActiveDocument.DrawText(new CPoint3D(text.x1, text.y1, 0), text.text, text.Size);
                        DrawingUtil.SetLayerToEntity(bEngFunc, hdtext, app.ActiveDocument, text.Layer);
                        hdtext.Label = text.Label;
                        hdtext.Rotate(new CPoint3D(text.x1, text.y1, 0), ViewerUtils.DegreeToRadian(text.rotn));
                        hdtext.ColorIndex = text.Color;
                    }
                }
            }
            br.Close();

            app.ActiveDocument.DisableUndoRecording(false);
            app.ZoomExtents();            
        }
    }
}
