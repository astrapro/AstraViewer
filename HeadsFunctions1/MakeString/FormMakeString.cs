using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HeadsUtils.Interfaces;
using HeadsUtils;
using System.IO;
using HeadsFunctions1.Halignment;

namespace HeadsFunctions1.MakeString
{
    internal partial class FormMakeString : Form
    {
        internal enum OpenMode
        {
            MakeString = 0,
            Boundary,
        }
        IHeadsApplication app;
        CPoint3D[] ptCords = null;

        double sx, sy, sz, ex, ey, ez;
        double B1, B2;
	    string modnam, stglbl, tt;
        const double r9 = 57.29577951;

        public FormMakeString(IHeadsApplication hdapp, IHdPolyline3D polyline, OpenMode mode)
        {
            InitializeComponent();

            this.app = hdapp;
            this.ptCords = polyline.Coordinates;

            if (mode == OpenMode.Boundary)
            {
                this.Text = "Boundary";
                this.checkMasterString_.Checked = false;
                this.checkMasterString_.Visible = false;
                this.tbChainageInterval_.Visible = false;
                this.labelChainageInterval_.Visible = false;
                this.tbChainageInterval_.Value = 10000000;

                this.Height -= (int)((double)this.tbChainageInterval_.Height * 1.5);
            }            
        }

        void determine_bearing()
        {
            double th, temp;

            if (sy == ey && ex > sx) B2 = 90;
            else if (sy == ey && ex < sx) B2 = 270;
            else if (sx == ex && ey > sy) B2 = 0;
            else if (sx == ex && ey < sy) B2 = 180;
            else
            {
                temp = (ey - sy) / (ex - sx);
                th = Math.Atan(temp);
                th = Math.Abs(th) * r9;

                if (ex > sx && ey > sy) B2 = 90 - th;
                else if (ex > sx && ey < sy) B2 = 90 + th;	/* NEW */
                else if (ex < sx && ey < sy) B2 = 270 - th;	/* NEW */
                else if (ex < sx && ey > sy) B2 = 270 + th;	/* NEW */
            }

            if (B2 < 0)
                B2 += 360;
            else if (B2 > 360)
                B2 -= 360;

        }

        void ProcessData()
        {
            BinaryWriter fp2 = null;
            StreamWriter fp3 = null;
            
            string nam;
            string lbl;

	        int ind=0, len, ival;
	        int slno, eltype, turn;

	        double x, y, z;
	        double stchn, RC, sc = 0, inc, seglen, segend;//, seginc;
	        double RCI, num, val;
	        double T1, T2, R, D, lastRC=-999.999;

	        /* Select a Polyline from screen */
	        /* User Input data in dialog box */
            modnam = this.tbModelName_.Text;
            stglbl = this.tbStringlabel_.Text;
            
            stchn = this.tbStartChainage_.Value;
        
	        inc = this.tbChainageInterval_.Value;

            string csTemp = Path.Combine(this.app.AppDataPath, "MODEL.FIL");
            fp2 = new BinaryWriter(new FileStream(csTemp, FileMode.Append), Encoding.Default);

	        if(this.checkMasterString_.Checked == true)
	        {
                csTemp = Path.Combine(this.app.AppDataPath, "HALIGN.FIL");
                fp3 = new StreamWriter(csTemp, true);		        
	        }
            CLabtype lab  = null;
            CPTStype pts = null;
            CModType mod = null;
            CStgType stg = null;
            ClSTtype lst = null;

            lab = new CLabtype();
            lab.attr = CLabtype.Type.Model;
            mod = new CModType();
            mod.name = modnam;
            lab.Tag = mod;
            lab.ToStream(fp2);

            lab = new CLabtype();
            lab.attr = CLabtype.Type.String;
            stg = new CStgType();
            stg.label = stglbl;
            lab.Tag = stg;
            lab.ToStream(fp2);
            

            RC = stchn;
	        //seginc=0.0;
	        sx=sy=sz=-999.999;
	        T1=T2=R=D=0;
            turn = 0;
	        B1=B2=0.0;
	        slno=1;
	        eltype=1;
	        turn=1;
	        ind=0;
	        //ind1=0;
            

            for (int vx = 0; vx < this.ptCords.Length; vx++)
            {
                /*JBLN*/
                ex = this.ptCords[vx].X;
                ey = this.ptCords[vx].Y;
                ez = this.ptCords[vx].Z;
                //		fscanf(fp1, "%lf %lf %lf", &ex, &ey, &ez);  /* close in vc++    */
                
                if(ind == 0)
                {
                    lab = new CLabtype();
                    lab.attr = CLabtype.Type.Point;
                    pts = new CPTStype();
                    pts.mc = RC;
                    pts.mx = ex;
                    pts.my = ey;
                    pts.mz = ez;
                    lab.Tag = pts;
                    lab.ToStream(fp2);
                    
                    sc=RC;
                    sx = ex;
                    sy = ey;
                    sz = ez;
                    B1 = B2;

                    ind = 1;
                    continue;
                }


                if (ex == sx && ey == sy)
                {
                    continue;
                }

                tt = string.Format("{0:f0}", inc);//sprintf(tt,"%.0f", inc);

                len = tt.Length;

                RCI = RC;
                num = Math.Pow(10, len);
                RCI = RCI / num;
                ival = (int)RCI;
                RCI = ival * num;
                while (RCI < RC)
                    RCI += inc;
                RC = RCI;

                if (RC == sc)
                    RC += inc;	/* NEW */
                
                determine_bearing();

                seglen = Math.Sqrt((ex - sx) * (ex - sx) + (ey - sy) * (ey - sy));	/* NEW */

                segend = sc + seglen;								/* NEW */

                if (this.checkMasterString_.Checked == true)
                {
                    if (B1 == 0) B1 = B2;
                    D = B1 - B2; if (D < 0) D += 360; if (D > 360) D -= 360;
                    
                    CHalignFilData fildata = new CHalignFilData();
                    fildata.sMod = modnam;
                    fildata.sString = stglbl;
                    fildata.iSlno = slno;
                    fildata.iEltype = (short)eltype;
                    fildata.dStartchn = sc;
                    fildata.dEndchn = segend;
                    fildata.dHipx = ex;
                    fildata.dHipy = ey;
                    fildata.dT1 = T1;
                    fildata.dT2 = T2;
                    fildata.dRadius = R;
                    fildata.dEllength = seglen;
                    fildata.dStartx = sx;
                    fildata.dStarty = sy;
                    fildata.dEndx = ex;
                    fildata.dEndy = ey;
                    fildata.dB1 = B2;
                    fildata.dB2 = B2;
                    fildata.dDeviation = D;
                    fildata.iTurn = (short)turn;
                    fp3.WriteLine(fildata.ToString());

                    //fprintf(fp3, "%s %s %d %d %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.3f %.3f %.3f %d\n", modnam, stglbl, slno, eltype, sc, segend, ex, ey, T1, T2, R, seglen, sx, sy, ex, ey, B2, B2, D, turn);
                    slno++;	/* NEW */
                }

                
                val = Math.Abs(RC - segend);/* fabs will be abs in VC++*/
                if (val < 0.001)
                    segend = RC;

		
                while(true)
                {
                    if (segend < RC)
                    {
                        if (segend + inc != RC)
                        {
                            val = Math.Abs(segend - lastRC);
                            if (val > 0.001)
                            {
                                lab = new CLabtype();
                                lab.attr = CLabtype.Type.Point;
                                pts = new CPTStype();
                                pts.mc = segend;
                                pts.mx = ex;
                                pts.my = ey;
                                pts.mz = ez;
                                lab.Tag = pts;
                                lab.ToStream(fp2);                                                    

                                lastRC = segend;
                            }
                        }
                        RC = segend;

                        ind = 0;
                        break;
                    }
                    else
                    {
                        if ((RC - sc) > 0.001)
                        {
                            x = sx + (RC - sc) * Math.Sin(B2 / r9);  	/*  E  */
                            y = sy + (RC - sc) * Math.Cos(B2 / r9);  	/*  N  */
                            z = sz + (RC - sc) * (ez - sz) / (segend - sc);

                            if (RC != lastRC)
                            {
                                lab = new CLabtype();
                                lab.attr = CLabtype.Type.Point;
                                pts = new CPTStype();
                                pts.mc = RC;
                                pts.mx = x;
                                pts.my = y;
                                pts.mz = z;
                                lab.Tag = pts;
                                lab.ToStream(fp2); 
                                
                                lastRC = RC;
                            }
                        }
                        RC += inc;
                    }
                }

                sc = RC;
                sx = ex;
                sy = ey;
                sz = ez;
                B1 = B2;

                ind = 1;
            }// end for previously while

            fp2.Close();

            if (this.checkMasterString_.Checked == true)
                fp3.Close();

            csTemp = Path.Combine(this.app.AppDataPath, "MODEL.LST");
            fp2 = new BinaryWriter(new FileStream(csTemp, FileMode.Append), Encoding.Default);
            lst = new ClSTtype();
            lst.strMod1 = modnam;
            lst.strStg = stglbl;
            lst.ToStream(fp2);
            fp2.Close();

            csTemp = Path.Combine(this.app.AppDataPath, "MODEL.FIL");
            fp2 = new BinaryWriter(new FileStream(csTemp, FileMode.Append), Encoding.Default);

            lab = new CLabtype();
            lab.attr = CLabtype.Type.Model;
            mod = new CModType();
            nam  = modnam + "%%";
            mod.name = nam;
            lab.Tag = mod;
            lab.ToStream(fp2);

            lab = new CLabtype();
            lab.attr = CLabtype.Type.String;
            stg = new CStgType();
            lbl = stglbl;
            stg.label = lbl;
            lab.Tag = stg;
            lab.ToStream(fp2);


            for (int vx = 0; vx < this.ptCords.Length; vx++)
            {
                ex = this.ptCords[vx].X;
                ey = this.ptCords[vx].Y;
                ez = this.ptCords[vx].Z;

                lab = new CLabtype();
                lab.attr = CLabtype.Type.Point;
                pts = new CPTStype();
                pts.mc = RC;
                pts.mx = ex;
                pts.my = ey;
                pts.mz = ez;
                lab.Tag = pts;
                lab.ToStream(fp2);
            }

            fp2.Close();

            csTemp = Path.Combine(this.app.AppDataPath, "MODEL.LST");
            fp2 = new BinaryWriter(new FileStream(csTemp, FileMode.Append), Encoding.Default);
            lst = new ClSTtype();
            lst.strMod1 = nam;
            lst.strStg = lbl;
            lst.ToStream(fp2);
            fp2.Close();
        }

        bool ValidateData()
        {
            if (this.tbModelName_.Text.Trim() == "")
            {
                this.errorProvider.SetError(this.tbModelName_, Properties.Resources.ST_PROMPT_INVALID_INPUT);
                this.tbModelName_.Focus();
                return false;
            }

            if (this.tbStringlabel_.Text.Trim() == "")
            {
                this.errorProvider.SetError(this.tbStringlabel_, Properties.Resources.ST_PROMPT_INVALID_INPUT);
                this.tbStringlabel_.Focus();
                return false;
            }

            string strHalignFilFilePath = Path.Combine(this.app.AppDataPath, "HALIGN.FIL");
            CHIPInfo[] infoarr = CHalignHipUtil.ReadHaligns(strHalignFilFilePath);
            foreach (CHIPInfo info in infoarr)
            {
                if (info.ModelName.Trim().ToLower() == this.tbModelName_.Text.Trim().ToLower()
                    && info.StringLabel.Trim().ToLower() == this.tbStringlabel_.Text.Trim().ToLower())
                {
                    this.errorProvider.SetError(this.tbStringlabel_
                        , "Model Name and String Level already exists\nPlease specify unique Model Name and String Level");
                    this.tbStringlabel_.Focus();
                    return false;
                }
            }

            return true;
        }

        private void btnOk__Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                ProcessData();
                this.Close();
            }
        }

        private void btnCancel__Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.errorProvider.Clear();
        }      
    }
}