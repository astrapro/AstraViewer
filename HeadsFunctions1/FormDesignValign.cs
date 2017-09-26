using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using HeadsUtils;
using HeadsUtils.Interfaces;

namespace HeadsFunctions1.Valignment
{
    public partial class FormDesignValign : Form
    {
        # region internal class
        public class VVIP
        {
            public double chainx;
            public double chainy;
            public string sym; //[3]
            public double grade;
            public double vcl1;
            public double vcl2;
        }
        # endregion

        # region private data
        IHeadsApplication headsApp = null;
        # endregion

        # region public data
        public long id;
        public long id2;
        public bool isOpen;
        public string szpath;
        public string HIPSetName;
        public short AcceptCode;
        public short MaxRowValign;
        public short MaxColValign;
        public VVIP[] GridArray;
        # endregion

        # region construction
        public FormDesignValign(IHeadsApplication headsApp)
        {
            InitializeComponent();
            this.headsApp = headsApp;
            this.isOpen = false;
            this.id2 = 0;
        }
        # endregion

        # region APIs
        public void setvariables(IHdPolyline3D pLine)
        {
            //CvdPolyline SPLine;			//Selected Polyline for which VIP has to be done
            //COleVariant pt;
            IHdPolyline3D SPLine;
            CPoint3D pt = new CPoint3D();
            short iVertex = 0;

            //CVdView* drawing;

            //CChildFrame* child = NULL;
            //CMainFrame* frm = (CMainFrame*)AfxGetApp()->GetMainWnd();
            //child = (CChildFrame*)frm->MDIGetActive();
            //drawing = (CVdView*)child->GetActiveView();

            CCfgtype cfg = this.headsApp.ConfigParam;

            SPLine = pLine;
            this.MaxColValign = 5;
            this.MaxRowValign = (short)SPLine.Coordinates.Length;

            this.GridArray = new VVIP[this.MaxRowValign];

            for (int i = 0; i < MaxRowValign; i++)
            {
                GridArray[i] = new VVIP();
                GridArray[i].chainx = 0.0;
                GridArray[i].chainy = 0.0;
                //strcpy(GridArray[i].sym, "Yes");
                this.GridArray[i].sym = "Yes";
                GridArray[i].vcl1 = 0.0;
                GridArray[i].vcl2 = 0.0;
            }

            string buffer;
            double grade = 0.0;
            double x, y, z;
            for (iVertex = 0; iVertex < MaxRowValign; iVertex++)
            {
                pt = SPLine.Coordinates[iVertex];
                //x = y = z = 0.0;
                //VariantToXYZ(pt, x, y, z);
                x = pt.X;
                y = pt.Y;
                z = pt.Z;

                GridArray[iVertex].chainx = x / cfg.XMetric;
                GridArray[iVertex].chainy = y / cfg.YMetric;

                if (iVertex == 0)
                {
                    GridArray[iVertex].grade = 0.0;
                }
                else
                {
                    GridArray[iVertex].grade = ((GridArray[iVertex].chainy - GridArray[iVertex - 1].chainy)
                                                    / (GridArray[iVertex].chainx - GridArray[iVertex - 1].chainx)) * 100;
                }
            }
        }
        
        public bool setvarfromopen(string sMod, string sStr, int nLines)
        {
            CCfgtype cfg = this.headsApp.ConfigParam;

            //char modnam[30], stglbl[20];
            string modnam; //[30]
            string stglbl; //[20]

            string CSmodnam, CSstglbl;
            int elno, eltype;
            double chn1, chn2, hipx, hipy, t1, t2, rad, l, xs, ys, xe, ye, b1, b2, del;
            int turn;

            bool bcut = false;
            int nidxED = 0;

            string sPath, sHfil, sHcut, sHtemp;
            //sPath = getpath();
            //sHfil = getpath();
            //sHtemp = getpath();
            //sHcut = getpath();
            sPath = this.headsApp.AppDataPath;
            sHfil = Path.Combine(sPath, "VALIGN.FIL");
            sHtemp = Path.Combine(sPath, "VALIGN.TMP");
            sHcut = Path.Combine(sPath, "VALIGN.CUT");

            //sHfil += "VALIGN.FIL";
            //sHtemp += "VALIGN.TMP";
            //sHcut += "VALIGN.CUT";

            StreamReader hf = null;
            StreamWriter hc = null;
            //FILE *hf, *hc; //, *ht;

            //CFileFind temp;
            //BOOL IsThere = temp.FindFile(sHtemp);
            //if(IsThere)  DeleteFile(sHtemp); // delete halign.tmp
            ViewerUtils.DeleteFileIfExists(sHtemp);

            int nLN = 0;
            double tx = 0, ty = 0, tgrade = 0, tv1 = 0, tv2 = 0;
            string tsym = "Yes";

            //if(( hf=fopen( (LPCSTR) sHfil, "r")) == NULL)
            //{
            //    AfxMessageBox( "Valign.Fil not found at the path");
            //    return FALSE;
            //}
            if (File.Exists(sHfil) == false)
            {
                MessageBox.Show("Valign.Fil not found at the path");
                return false;
            }
            hf = new StreamReader(sHfil);

            //if(( hc=fopen( (LPCSTR) sHcut, "w")) == NULL)
            //{
            //    AfxMessageBox( "Could not open valign.cut (truncated valign.fil) for writing at the path");
            //    return FALSE;
            //}
            hc = new StreamWriter(sHcut);

            GridArray = new VVIP[nLines];
            for (int i = 0; i < nLines; i++)
            {
                GridArray[i] = new VVIP();
            }

            while (hf.EndOfStream == false)
            {
                //fscanf( hf, "%s %s %i %4i %lf %lf %lf %lf %lf %lf %lf %lf %lf %lf %lf %lf %lf %lf %lf %i\n", modnam, stglbl, &elno, &eltype, &chn1, &chn2, &hipx, &hipy, &t1, &t2, &rad, &l, &xs, &ys, &xe, &ye, &b1, &b2, &del, &turn);
                string line = hf.ReadLine();
                string[] components = line.Split('\t');
                modnam = components[0];
                stglbl = components[1];
                elno = int.Parse(components[2]);
                eltype = int.Parse(components[3]);
                chn1 = double.Parse(components[4]);
                chn2 = double.Parse(components[5]);
                hipx = double.Parse(components[6]);
                hipy = double.Parse(components[7]);
                t1 = double.Parse(components[8]);
                t2 = double.Parse(components[9]);
                rad = double.Parse(components[10]);
                l = double.Parse(components[11]);
                xs = double.Parse(components[12]);
                ys = double.Parse(components[13]);
                xe = double.Parse(components[14]);
                ye = double.Parse(components[15]);
                b1 = double.Parse(components[16]);
                b2 = double.Parse(components[17]);
                del = double.Parse(components[18]);
                turn = int.Parse(components[19]);

                CSmodnam = modnam;
                CSstglbl = stglbl;

                if (sMod != CSmodnam)
                {
                    bcut = true;
                }
                if (sStr != CSstglbl)
                {
                    bcut = true;
                }

                if (bcut == true)
                {
                    //fprintf( hc,"%8s %4s %4d %4d %10.5f %10.5f %10.5f %10.5f %10.5f %10.5f %10.5f %10.5f %10.5f %10.5f %10.5f %10.5f %10.5f %10.5f %10.5f %4d\n", modnam, stglbl, elno, eltype, chn1, chn2, hipx, hipy, t1, t2, rad, l, xs, ys, xe, ye, b1, b2, del,turn);
                    hc.Write("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12} {13} {14} {15} {16} {17} {18} {19}\n", modnam, stglbl, elno, eltype, chn1, chn2, hipx, hipy, t1, t2, rad, l, xs, ys, xe, ye, b1, b2, del, turn);
                    bcut = false;
                }
                else // mod and str have matched therefore fill gridarray
                {
                    if (nLN == 0)
                    {
                        if (eltype == 1)
                        {
                            GridArray[0].chainx = xs;
                            GridArray[0].chainy = ys;
                            GridArray[0].grade = b1;
                            if (rad != l)
                            {
                                //strcpy(GridArray[0].sym,"No");
                                GridArray[0].sym = "No";
                                GridArray[0].vcl1 = rad;
                                GridArray[0].vcl2 = l;
                            }
                            else
                            {
                                //strcpy(GridArray[0].sym,"Yes");
                                GridArray[0].sym = "Yes";
                                GridArray[0].vcl1 = rad + l;
                                GridArray[0].vcl2 = 0.0;
                            }
                            nLN++;
                        }
                    }
                    else
                    {   // if nLN > 0
                        if (eltype == 3)
                        {
                            GridArray[nLN].chainx = hipx;
                            GridArray[nLN].chainy = hipy;
                            GridArray[nLN].grade = b1;
                            if (rad != l)
                            {
                                //strcpy(GridArray[nLN].sym,"No");
                                GridArray[nLN].sym = "No";
                                GridArray[nLN].vcl1 = rad;
                                GridArray[nLN].vcl2 = l;

                            }
                            else
                            {
                                //strcpy(GridArray[nLN].sym, "Yes");
                                GridArray[nLN].sym = "Yes";
                                GridArray[nLN].vcl1 = rad + l;
                                GridArray[nLN].vcl2 = 0.0;
                            }
                            nLN++;
                        }
                        else // if eltype == 1
                        { // for last line
                            tx = xe;
                            ty = ye;
                            tgrade = b2;
                            tsym = "Yes";
                            tv1 = l;
                            tv2 = 0;
                        }

                    }//end else  if(nLN==0)

                }// end else bcut

            }//end while(!feof( hf))

            GridArray[nLN].chainx = tx;
            GridArray[nLN].chainy = ty;
            GridArray[nLN].grade = tgrade;
            //strcpy(GridArray[nLN].sym, (LPCSTR) tsym );
            GridArray[nLN].sym = tsym;
            GridArray[nLN].vcl1 = tv1;
            GridArray[nLN].vcl2 = tv2;

            MaxRowValign = (short)(nLN + 1);

            //fclose( hf );
            //fclose( hc );
            hf.Close();
            hc.Close();

            //IsThere = temp.FindFile( (sPath+"~valign.fil") );
            //if(IsThere)  
            //    DeleteFile( (sPath+"~valign.fil") ); // delete valign.fil
            ViewerUtils.DeleteFileIfExists(Path.Combine(sPath, "~valign.fil"));

            //rename(sHfil, sPath+"~valign.fil");
            //rename(sHcut, sPath+"valign.fil");
            ViewerUtils.RenameFile(sHfil, Path.Combine(sPath, "~valign.fil"));
            ViewerUtils.RenameFile(sHcut, Path.Combine(sPath, "valign.fil"));

            return true;

            //m_StartChainage = GridArray[0].chainx;
            //m_EndChainage = GridArray[MaxRowValign-1].chainx;
        }
        
        public bool WriteDataToFileHIP()
        {
            string szFileName;
            FirstLine Line1 = new FirstLine();
            SecondLineVIP line2 = new SecondLineVIP();

            VIPData Data = new VIPData();
            //FILE* fNumber;
            BinaryWriter fNumber = null;

            //CVdView* drawing = NULL;
            //CChildFrame* child = NULL;
            //CMainFrame* frm = (CMainFrame*)AfxGetApp()->GetMainWnd();
            //child = (CChildFrame*)frm->MDIGetActive();
            //drawing = (CVdView*)child->GetActiveView();

            //Cfgtype cfg;
            //cfg = drawing->GetCFGParam();

            szpath = this.headsApp.AppDataPath;
            //szFileName = szpath + "VAL1.tmp";
            //fNumber = fopen(szFileName, "wb");
            //if (fNumber == NULL)
            //{
            //    AfxMessageBox(szFileName + " Can't Be Opened");
            //    return FALSE;
            //}
            szFileName = Path.Combine(szpath, "VAL1.tmp");
            fNumber = new BinaryWriter(new FileStream(szFileName, FileMode.Create));

            //UpdateData();
            Line1.Code = 1;
            Line1.AcceptCode = AcceptCode;
            //strcpy(Line1.ModelName, m_ModelName);
            //strcpy(Line1.StringName, m_StringLabel);
            Line1.ModelName = this.textBoxModelName.Text;
            Line1.StringName = this.textBoxStringLabel.Text;

            //fwrite(&Line1.Code, 1, sizeof(Line1.Code), fNumber);
            //fwrite(&Line1.AcceptCode, 1, sizeof(Line1.AcceptCode), fNumber);
            //fwrite(Line1.ModelName, 1, sizeof(Line1.ModelName), fNumber);
            //fwrite(Line1.StringName, 1, sizeof(Line1.StringName), fNumber);
            fNumber.Write(Line1.Code);
            fNumber.Write(Line1.AcceptCode);
            fNumber.Write(ViewerUtils.ConvertStringToCharArray(Line1.ModelName, 30));
            fNumber.Write(ViewerUtils.ConvertStringToCharArray(Line1.StringName, 20));

            //if (m_StartChainage < 0.0)
            //{
            //    m_StartChainage = 0.0;
            //    UpdateData(FALSE);
            //    CString buffer;
            //    GridArray[0].chainx = m_StartChainage;
            //    buffer.Format("%lf", m_StartChainage);
            //    m_ListControl.SetItem(0, 0, LVIF_TEXT, buffer, 0, 0, 0, 0L);
            //}
            if (double.Parse(this.textBoxStartChainageM.Text) < 0.0)
            {
                this.textBoxStartChainageM.Text = "0.0";
                GridArray[0].chainx = double.Parse(this.textBoxStartChainageM.Text);
                this.listView1.Items.Add(new ListViewItem(this.textBoxStartChainageM.Text));
            }

            line2.TotalVIPs = MaxRowValign;
            line2.StartChain = double.Parse(this.textBoxStartChainageM.Text);		// Removed cfg.xmetric
            line2.EndChain = double.Parse(this.textBoxEndChainageM.Text);			//Removed cfg.xmetric
            line2.Interval = double.Parse(this.textBoxChainageIntervalM.Text);

            //fwrite(&line2.TotalVIPs, 1, sizeof(line2.TotalVIPs), fNumber);
            //fwrite(&line2.StartChain, 1, sizeof(line2.StartChain), fNumber);
            //fwrite(&line2.EndChain, 1, sizeof(line2.EndChain), fNumber);
            //fwrite(&line2.Interval, 1, sizeof(line2.Interval), fNumber);
            fNumber.Write(line2.TotalVIPs);
            fNumber.Write(line2.StartChain);
            fNumber.Write(line2.EndChain);
            fNumber.Write(line2.Interval);

            for (int i = 0; i < MaxRowValign; i++)
            {
                Data.VIPNo = (short)(i + 1);
                Data.VIP_Chain = (double)GridArray[i].chainx;
                Data.VIP_level = (double)GridArray[i].chainy;

                if (GridArray[i].sym == "Yes")
                {
                    Data.VCL1 = GridArray[i].vcl1 / 2;
                    Data.VCL2 = GridArray[i].vcl1 / 2;
                }
                else
                {
                    Data.VCL1 = GridArray[i].vcl1;
                    Data.VCL2 = GridArray[i].vcl2;
                }

                //fwrite(&Data.VIPNo, 1, sizeof(Data.VIPNo), fNumber);
                //fwrite(&Data.VIP_Chain, 1, sizeof(Data.VIP_Chain), fNumber);
                //fwrite(&Data.VIP_level, 1, sizeof(Data.VIP_level), fNumber);
                //fwrite(&Data.VCL1, 1, sizeof(Data.VCL1), fNumber);
                //fwrite(&Data.VCL2, 1, sizeof(Data.VCL2), fNumber);
                fNumber.Write(Data.VIPNo);
                fNumber.Write(Data.VIP_Chain);
                fNumber.Write(Data.VIP_level);
                fNumber.Write(Data.VCL1);
                fNumber.Write(Data.VCL2);
            }
            //fclose(fNumber);
            fNumber.Close();
            return true;
        }
        
        public void FillSelectedPolyLineDetails()
        {
            short iVertex;
            //string buffer;
            //double grade = 0.0;

            for (iVertex = 0; iVertex < MaxRowValign; iVertex++)
            {
                string[] listViewItemData = new string[] {
                    GridArray[iVertex].chainx.ToString(),
                    GridArray[iVertex].chainy.ToString(),
                    GridArray[iVertex].grade.ToString(),
                    GridArray[iVertex].sym.ToString(),
                    GridArray[iVertex].vcl1.ToString(),
                    GridArray[iVertex].vcl2.ToString()
                };
                this.listView1.Items.Add(new ListViewItem(listViewItemData));

                ////buffer.Format("%lf", GridArray[iVertex].chainx);
                ////m_ListControl.InsertItem(LVIF_TEXT, iVertex, buffer, 0, 0, 0, 0L);
                //this.listView1.Items.Add(new ListViewItem(GridArray[iVertex].chainx.ToString()));

                ////buffer.Format("%lf", GridArray[iVertex].chainy);
                ////m_ListControl.SetItem(iVertex, 1, LVIF_TEXT, buffer, 0, 0, 0, 0L);
                //this.listView1.Items.Add(new ListViewItem(GridArray[iVertex].chainy.ToString()));

                ////buffer.Format("%lf", GridArray[iVertex].grade);
                ////m_ListControl.SetItem(iVertex, 2, LVIF_TEXT, buffer, 0, 0, 0, 0L);
                //this.listView1.Items.Add(new ListViewItem(GridArray[iVertex].grade.ToString()));

                ////buffer.Format("%s", GridArray[iVertex].sym);
                ////m_ListControl.SetItem(iVertex, 3, LVIF_TEXT, buffer, 0, 0, 0, 0L);
                //this.listView1.Items.Add(new ListViewItem(GridArray[iVertex].sym.ToString()));

                ////buffer.Format("%lf", GridArray[iVertex].vcl1);
                ////m_ListControl.SetItem(iVertex, 4, LVIF_TEXT, buffer, 0, 0, 0, 0L);
                //this.listView1.Items.Add(new ListViewItem(GridArray[iVertex].vcl1.ToString()));

                ////buffer.Format("%lf", GridArray[iVertex].vcl2);
                ////m_ListControl.SetItem(iVertex, 5, LVIF_TEXT, buffer, 0, 0, 0, 0L);
                //this.listView1.Items.Add(new ListViewItem(GridArray[iVertex].vcl2.ToString()));
            }

            //m_StartChainage = GridArray[0].chainx;
            //m_EndChainage = GridArray[MaxRowValign - 1].chainx;
            this.textBoxStartChainageM.Text = GridArray[0].chainx.ToString();
            this.textBoxEndChainageM.Text = GridArray[MaxRowValign - 1].chainx.ToString();
        }
        # endregion

        # region private methods
        private void UpdateVIPParamButtonsState()
        {
            this.buttonEdit.Enabled = (this.listView1.Focused && this.listView1.SelectedItems.Count > 0);
            this.buttonDelete.Enabled = (this.listView1.Focused && this.listView1.SelectedItems.Count > 0);
            this.buttonInsert.Enabled = this.listView1.Focused;
        }
        # endregion

        # region overrides
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AcceptCode = 0;
            FillSelectedPolyLineDetails();
        }
        # endregion

        # region event handlers
        private void buttonSaveFinish_Click(object sender, EventArgs e)
        {
            ValignCode vc = new ValignCode();
            vc.path = this.headsApp.AppDataPath;
            vc.save_valign();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            this.buttonApply.Enabled = false;
            this.buttonSaveFinish.Enabled = false;
            this.buttonCanel.Enabled = false;
            this.listView1.Enabled = false;

            Cursor.Current = Cursors.WaitCursor;

            IHdEntity ent;
            double x1;
            double y1;
            double x2;
            double y2;
            IHdPolyline3D pLineNew, pLineOld;
            CPoint3D pt;

            CCfgtype cfg = this.headsApp.ConfigParam;

            cfg.XMetric = 1;
            cfg.YMetric = 10;

            this.headsApp.ConfigParam = cfg;

            if (id2 != 0L) // erase applied pline from val2.tmp
            {
                //ent = (CvdFigure)drawing->m_Vdraw.GetActiveDocument().GetFromID(id2);
                //pLineOld = (CvdPolyline) ent;
                //pLineOld.Erase();
                ent = this.headsApp.ActiveDocument.GetObjectById((int)this.id2);
                if (ent != null)
                {
                    ent.Erase();
                }
            }

            // now use same variables for main pline
            if (id != 0) // erase pline from screen click
            {
                //ent = (CvdFigure)drawing->m_Vdraw.GetActiveDocument().GetFromID(id);
                //pLineOld = (CvdPolyline) ent;
                //pLineOld.Erase();
                ent = this.headsApp.ActiveDocument.GetObjectById((int)this.id);
                if (ent != null)
                {
                    ent.Erase();
                }
            }

            x1 = GridArray[0].chainx * cfg.XMetric;
            y1 = GridArray[0].chainy * cfg.YMetric;
            x2 = GridArray[1].chainx * cfg.XMetric;
            y2 = GridArray[1].chainy * cfg.YMetric;

            List<CPoint3D> polypt = new List<CPoint3D>();
            long[] ix = new long[2];

            //SAFEARRAYBOUND rgsabound[2];
            //rgsabound[0].lLbound = 0;
            //rgsabound[0].cElements = 2;
            //rgsabound[1].lLbound = 0;
            //rgsabound[1].cElements = 2;

            //COleSafeArray array;
            //array.Create(VT_R8,2,rgsabound);

            //ix[0] = 0; ix[1] = 0;
            //array.PutElement(ix, &x1);
            //ix[0] = 0; ix[1] = 1;
            //array.PutElement(ix, &y1);
            //ix[0] = 1; ix[1] = 0;
            //array.PutElement(ix, &x2);
            //ix[0] = 1; ix[1] = 1;
            //array.PutElement(ix, &y2);
            //polypt = array.Detach();
            polypt.Add(new CPoint3D(0, 0, 0));
            polypt.Add(new CPoint3D(0, 1, 0));
            polypt.Add(new CPoint3D(1, 0, 0));
            polypt.Add(new CPoint3D(1, 1, 0));

            //pLineNew = drawing->m_Vdraw.GetActiveDocument().GetEntities().AddPolyLine(polypt);
            pLineNew = this.headsApp.ActiveDocument.DrawPolyline3D(polypt);

            double q1, q2, q3;
            q1 = q2 = q3 = 0;
            for (int i = 2; i < MaxRowValign; i++)
            {
                q1 = GridArray[i].chainx * cfg.XMetric;
                q2 = GridArray[i].chainy * cfg.YMetric;
                pt = new CPoint3D(q1, q2, q3);
                pLineNew.AppendVertex(pt);
            }

            pLineNew.Update();
            //id = pLineNew.GetId();
            id = pLineNew.ObjectID;

            if (WriteDataToFileHIP())
            {
                //szpath = getpath();
                szpath = this.headsApp.AppDataPath;

                //CFileFind temp;
                //BOOL IsThere = temp.FindFile(szpath + "VAL2.TMP");
                //if(IsThere)
                //    DeleteFile(szpath + "VAL2.TMP");
                ViewerUtils.DeleteFileIfExists(Path.Combine(szpath, "VAL2.TMP"));

                ValignCode vl = new ValignCode();
                vl.Funcmain(szpath);

                //id2 = DrawData(szpath + "VAL2.TMP", "PVR", TRUE);
                ViewerUtils.DrawData(this.headsApp, Path.Combine(szpath, "VAL2.TMP"), "PVR", true);
                // only for drawing pline from hal2.tmp
            }

            Cursor.Current = Cursors.Default;

            this.buttonApply.Enabled = true;
            this.buttonSaveFinish.Enabled = true;
            this.buttonCanel.Enabled = true;
            this.listView1.Enabled = true;
        }

        private void textBoxStartChainageM_TextChanged(object sender, EventArgs e)
        {
            string data = this.textBoxStartChainageM.Text;
            if (this.listView1.Items.Count > 0)
            {
                this.listView1.Items[0].SubItems[0].Text = data;
            }
        }

        private void textBoxEndChainageM_TextChanged(object sender, EventArgs e)
        {
            string data = this.textBoxEndChainageM.Text;
            int endIndex = this.listView1.Items.Count - 1;
            if (endIndex > 0)
            {
                this.listView1.Items[endIndex].SubItems[0].Text = data;
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            this.UpdateVIPParamButtonsState();

            ListView.SelectedListViewItemCollection selectedItems = this.listView1.SelectedItems;
            if (selectedItems != null && selectedItems.Count > 0)
            {
                int nItem = selectedItems.Count;
                VVIP[] TempArray = new VVIP[MaxRowValign];
                for (int i = 0; i < MaxRowValign; i++)
                {
                    TempArray[i] = new VVIP();
                }

                for (int i = 0; i < MaxRowValign; i++)
                {
                    TempArray[i].chainx = GridArray[i].chainx;
                    TempArray[i].chainy = GridArray[i].chainy;
                    TempArray[i].grade = GridArray[i].grade;
                    TempArray[i].sym = GridArray[i].sym;
                    TempArray[i].vcl1 = GridArray[i].vcl1;
                    TempArray[i].vcl2 = GridArray[i].vcl2;
                }

                GridArray = null;
                MaxRowValign = (short)(MaxRowValign - 1);
                if (MaxRowValign == 0)
                {
                    this.listView1.Items.Clear();
                    this.buttonInsert.Enabled = true;
                    return;
                }
                GridArray = new VVIP[MaxRowValign];
                for (int i = 0; i < MaxRowValign; i++)
                {
                    GridArray[i] = new VVIP();
                }

                for (int i = 0; i < nItem; i++)
                {
                    GridArray[i].chainx = TempArray[i].chainx;
                    GridArray[i].chainy = TempArray[i].chainy;
                    GridArray[i].grade = TempArray[i].grade;
                    GridArray[i].sym = TempArray[i].sym;
                    GridArray[i].vcl1 = TempArray[i].vcl1;
                    GridArray[i].vcl2 = TempArray[i].vcl2;
                }

                for (int i = nItem; i < MaxRowValign; i++)
                {
                    GridArray[i].chainx = TempArray[i + 1].chainx;
                    GridArray[i].chainy = TempArray[i + 1].chainy;
                    GridArray[i].grade = TempArray[i + 1].grade;
                    GridArray[i].sym = TempArray[i + 1].sym;
                    GridArray[i].vcl1 = TempArray[i + 1].vcl1;
                    GridArray[i].vcl2 = TempArray[i + 1].vcl2;
                }

                this.listView1.Items.Clear();
                string buffer;
                for (int i = 0; i < MaxRowValign; i++)
                {
                    string[] gridData = new string[] {
                        GridArray[i].chainx.ToString(),
                        GridArray[i].chainy.ToString(),
                        GridArray[i].grade.ToString(),
                        GridArray[i].sym,
                        GridArray[i].vcl1.ToString(),
                        GridArray[i].vcl2.ToString()
                    };
                    this.listView1.Items.Add(new ListViewItem(gridData));
                }

                double grade = 0.0;
                for (int i = 0; i < MaxRowValign; i++)
                {
                    if (i == 0)
                    {
                        GridArray[i].grade = 0.0;
                    }
                    else
                    {
                        GridArray[i].grade = ((GridArray[i].chainy - GridArray[i - 1].chainy)
                                                    / (GridArray[i].chainx - GridArray[i - 1].chainx)) * 100;
                    }

                    this.listView1.Items[i].SubItems[2].Text = GridArray[i].grade.ToString();
                }
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            //POSITION pos = m_ListControl.GetFirstSelectedItemPosition();
            ListView.SelectedListViewItemCollection selectedItems = this.listView1.SelectedItems;
            
            //if (pos != NULL)
            if (selectedItems != null && selectedItems.Count > 0)
            {
                //CEditValign dlg;
                FormEditValign dlg = new FormEditValign();
                int nItem = this.listView1.SelectedIndices[0];

                dlg.ChainX = selectedItems[0].SubItems[0].Text;
                dlg.ChainY = selectedItems[0].SubItems[1].Text;
                dlg.Symmetrical = selectedItems[0].SubItems[3].Text;
                dlg.Vcl1 = selectedItems[0].SubItems[4].Text;
                dlg.Vcl2 = selectedItems[0].SubItems[5].Text;
                dlg.ShowDialog();

                selectedItems[0].SubItems[0].Text = dlg.ChainX;
                selectedItems[0].SubItems[1].Text = dlg.ChainY;
                selectedItems[0].SubItems[3].Text = dlg.Symmetrical;
                selectedItems[0].SubItems[4].Text = dlg.Vcl1;
                selectedItems[0].SubItems[5].Text = dlg.Vcl2;

                GridArray[nItem].chainx = double.Parse(dlg.ChainX);
                GridArray[nItem].chainy = double.Parse(dlg.ChainY);
                GridArray[nItem].sym = dlg.Symmetrical;
                GridArray[nItem].vcl1 = double.Parse(dlg.Vcl1);
                GridArray[nItem].vcl2 = double.Parse(dlg.Vcl2);

                string buffer = string.Empty;
                double grade = 0.0;
                for (int i = 0; i < MaxRowValign; i++)
                {
                    if (i == 0)
                        GridArray[i].grade = 0.0;
                    else
                        GridArray[i].grade = ((GridArray[i].chainy - GridArray[i - 1].chainy)
                                                    / (GridArray[i].chainx - GridArray[i - 1].chainx)) * 100;
                    //buffer.Format("%lf", GridArray[i].grade);
                    //m_ListControl.SetItem(i, 2, LVIF_TEXT, buffer, 0, 0, 0, 0L);
                    this.listView1.Items[i].SubItems[2].Text = GridArray[i].grade.ToString();
                }
            }
            selectedItems[0].Selected = true;

            this.UpdateVIPParamButtonsState();
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            //POSITION pos = m_ListControl.GetFirstSelectedItemPosition();
            ListView.SelectedListViewItemCollection selectedItems = this.listView1.SelectedItems;

            //if (pos != NULL)
            //if (selectedItems != null && selectedItems.Count > 0)
            //{
                string buffer = string.Empty;
                //int nItem = m_ListControl.GetNextSelectedItem(pos);
                int nItem = selectedItems.Count;

                FormEditValign dlg = new FormEditValign();
                DialogResult result = dlg.ShowDialog();
                if (result == DialogResult.Cancel)
                {
                    return;
                }

                VVIP[] TempArray = new VVIP[MaxRowValign];
                for (int i = 0; i < MaxRowValign; i++)
                {
                    TempArray[i] = new VVIP();
                }

                for (int i = 0; i < MaxRowValign; i++)
                {
                    TempArray[i].chainx = GridArray[i].chainx;
                    TempArray[i].chainy = GridArray[i].chainy;
                    TempArray[i].grade = GridArray[i].grade;
                    TempArray[i].sym = GridArray[i].sym;
                    TempArray[i].vcl1 = GridArray[i].vcl1;
                    TempArray[i].vcl2 = GridArray[i].vcl2;
                }

                MaxRowValign = (short)(MaxRowValign + 1);
                GridArray = new VVIP[MaxRowValign];
                for (int i = 0; i < MaxRowValign; i++)
                {
                    GridArray[i] = new VVIP();
                }

                for (int i = 0; i < nItem; i++)
                {
                    GridArray[i].chainx = TempArray[i].chainx;
                    GridArray[i].chainy = TempArray[i].chainy;
                    GridArray[i].grade = TempArray[i].grade;
                    GridArray[i].sym = TempArray[i].sym;
                    GridArray[i].vcl1 = TempArray[i].vcl1;
                    GridArray[i].vcl2 = TempArray[i].vcl2;
                }

                GridArray[nItem].chainx = double.Parse(dlg.ChainX);
                GridArray[nItem].chainy = double.Parse(dlg.ChainY);
                GridArray[nItem].sym = dlg.Symmetrical;
                GridArray[nItem].vcl1 = double.Parse(dlg.Vcl1);
                GridArray[nItem].vcl2 = double.Parse(dlg.Vcl2);

                for (int i = nItem + 1; i < MaxRowValign; i++)
                {
                    GridArray[i].chainx = TempArray[i - 1].chainx;
                    GridArray[i].chainy = TempArray[i - 1].chainy;
                    GridArray[i].grade = TempArray[i - 1].grade;
                    GridArray[i].sym = TempArray[i - 1].sym;
                    GridArray[i].vcl1 = TempArray[i - 1].vcl1;
                    GridArray[i].vcl2 = TempArray[i - 1].vcl2;
                }

                this.listView1.Items.Clear();

                for (int i = 0; i < MaxRowValign; i++)
                {
                    string[] gridData = new string[] {
                        GridArray[i].chainx.ToString(),
                        GridArray[i].chainy.ToString(),
                        GridArray[i].grade.ToString(),
                        GridArray[i].sym,
                        GridArray[i].vcl1.ToString(),
                        GridArray[i].vcl2.ToString()
                    };
                    this.listView1.Items.Add(new ListViewItem(gridData));
                }

                double grade = 0.0;
                for (int i = 0; i < MaxRowValign; i++)
                {
                    if (i == 0)
                    {
                        GridArray[i].grade = 0.0;
                    }
                    else
                    {
                        GridArray[i].grade = ((GridArray[i].chainy - GridArray[i - 1].chainy)
                                                    / (GridArray[i].chainx - GridArray[i - 1].chainx)) * 100;
                    }

                    this.listView1.Items[i].SubItems[2].Text = GridArray[i].grade.ToString();
                }
            //}

            this.UpdateVIPParamButtonsState();
        }

        private void buttonCanel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            this.UpdateVIPParamButtonsState();
        }

        private void FormDesignValign_Click(object sender, EventArgs e)
        {
            this.UpdateVIPParamButtonsState();
        }

        private void checkBoxDVCL_CheckStateChanged(object sender, EventArgs e)
        {
            double delta = 0.0;
            double dVCL = 0.0;
            string buffer = string.Empty;

            if (this.checkBoxDVCL.Checked == false)
            {
                this.textBoxDVCL.Enabled = true;

                dVCL = 0.0;
                for (int i = 2; i < MaxRowValign; i++)
                {
                    GridArray[i - 1].vcl1 = dVCL;
                    this.listView1.Items[i - 1].SubItems[4].Text = dVCL.ToString();
                }
            }
            else
            {
                this.textBoxDVCL.Enabled = false;

                dVCL = double.Parse(this.textBoxDVCL.Text);
                for (int i = 2; i < MaxRowValign; i++)
                {
                    GridArray[i - 1].vcl1 = dVCL;
                    this.listView1.Items[i - 1].SubItems[4].Text = dVCL.ToString();
                }
            }
        }
        # endregion
    }
}