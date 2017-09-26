using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HeadsUtils;

namespace HeadsFunctions1.Details
{
    internal class CDetailsValignUtil
    {
        StreamReader fp1;
        BinaryWriter fp2;
	    bool index;
	    short turn1,  elemno, eltype, flag;
        
        double xlate_x, xlate_y, XMIN, YMIN, textsize, textrotn, rotn, xfa, xta, yta;
        
        double R, L, B1, B2, DEL, xst, yst, xed, yed;//RC, RCI, rep_chn, SL, 
        double stchn, endchn, T1, T2, IPX, IPY, last_stchn;//store_RC, curchn, brgi, ths, thc, 
        double last_endchn, xt, yt, last_IPX, last_IPY, xstor, ystor;
        //double qb1, qb2, zt;
        
        string path, pathfile;
        string modstr, stgstr, textstring;//, str1, str2, str3, str4, str5, filstr,;


        public CDetailsValignUtil()
        {

        }

        public void Funcmain(string argv1,CCfgtype cfg)
        {
            path = argv1;
            //err_code = 9; /* no error */

            read_input();

            pathfile = Path.Combine(path, "DETAILS2.TMP");

            fp2 = new BinaryWriter(new FileStream(pathfile, FileMode.Create), Encoding.Default);

            if (rotn != 0.0)
            {
                textstring = "DRG not by 'Draw String', use 'Minor option(s) 1103 / 1104'...";
                error_msg();
                return;
            }

            pathfile = Path.Combine(path, "VALIGN.TMP");
            if (File.Exists(pathfile) == false)
            {
                pathfile = Path.Combine(path, "VALIGN.FIL");
            }
            fp1 = new StreamReader(pathfile);
            

            index = false;

            while (fp1.EndOfStream == false)
            {
                init_param();
                des_curve_input();

                if (flag == 0) continue;

                if (stchn == last_stchn && endchn == last_endchn)
                    break;

                draw_hips(cfg);

                last_stchn = stchn;
                last_endchn = endchn;
                index = true;
            } // end while(fp1.EndOfStream == false)

            end_HIP();

            fp1.Close();
            fp2.Close();
        }

        void error_msg()
        {
            //err_code = -999;
            xta = yta = 100.0;
            textsize = 5;
            textrotn = 0;
            write_text();
            if (fp2 != null)
                fp2.Close();

            return;
        }

        void read_input()
        {
            HeadsUtils.Detail det;
            double r9;

            r9 = 57.29578;

            pathfile = Path.Combine(path, "DETAILS1.TMP");
            BinaryReader fp1Br = new BinaryReader(new FileStream(pathfile, FileMode.Open, FileAccess.Read), Encoding.Default);
            
            det = HeadsUtils.Detail.FromStream(fp1Br);

            modstr = det.ModelName;
            stgstr = det.StringName;
            textsize = det.TextSize;
            rotn = det.Rotation;
            XMIN = det.XMin;
            YMIN = det.YMin;

            fp1Br.Close();

            xfa = rotn / r9;
            xlate_x = XMIN;
            xlate_y = YMIN;
        }

        void init_param()
        {
            IPX = IPY = 0.0;
        }

        void des_curve_input()
        {
            string mdlnam, strnam;
            int telemno = 0, teltype = 0;
            double tstchn = 0.0 , tendchn = 0.0, tIPX = 0.0, tIPY = 0.0;
            double tT1 = 0.0 , tT2 = 0.0, tR = 0.0, tL = 0.0;
            double txst = 0.0 , tyst = 0.0, txed = 0.0, tyed = 0.0;
            double tB1 = 0.0 , tB2 = 0.0, tDEL = 0.0;
            int tturn1 = 0;

            CValignFilData fildata = CValignFilData.Parse(fp1.ReadLine());

            mdlnam = fildata.modnam;
            strnam = fildata.stglbl;
            telemno = fildata.elno;
            teltype = fildata.eltype;
            tstchn = fildata.chn1;
            tendchn = fildata.chn2;
            tIPX = fildata.hipx;
            tIPY = fildata.hipy;
            tT1 = fildata.t1;
            tT2 = fildata.t2;
            tR = fildata.rad;
            tL = fildata.l;
            txst = fildata.xs;
            tyst = fildata.ys;
            txed = fildata.xe;
            tyed = fildata.ye;
            tB1 = fildata.b1;
            tB2 = fildata.b2;
            tDEL = fildata.del;
            tturn1 = fildata.turn;

            if ((mdlnam == modstr) && (strnam == stgstr))
            {
                flag=1; // final 13/09/01 DD
                elemno = (short)telemno; 
                eltype = (short)teltype;
                stchn = tstchn ; endchn = tendchn; IPX = tIPX; IPY = tIPY;
                T1 = tT1 ; T2 = tT2; R = tR; L = tL;
                xst = txst ; yst = tyst; xed = txed; yed = tyed;
                B1 = tB1; B2 = tB2; DEL = tDEL;
                turn1 = (short)tturn1;
            }
            else
            {
                flag=0;
            }
        }

        void draw_hips(CCfgtype cfg)
        {
            //zt = 0.0;
            textrotn = 0.0;

            double dVcl, dK;
            
            double dLead = 1.2; // gap between lines
            
            if (IPX != last_IPX && IPY != last_IPY)
            {
                if (index == false) // Start HIP 
                {
                    xt = xst - xlate_x;
                    yt = yst - xlate_y;

                    
                    plan_form(ref xt, ref yt);
                    textstring = "Start chainage / Level: " + xst.ToString("0.000") + ", " + yst.ToString("0.000") + " m.";
                    textwrite();
                }               
            }
            if (eltype == 3)
            {
                xt = IPX - xlate_x;
                yt = IPY - xlate_y;
                plan_form(ref xt, ref yt);

                textstring = "VIP Chainage = " + IPX.ToString("0.000") + " m";
                xstor = xt;
                ystor = yt;
                textwrite();
                xt = xstor;
                yt = ystor;

                yt -= textsize / cfg.YMetric * dLead;

                textstring = "VIP Level = " + IPY.ToString("0.000") + " m";
                xstor = xt;
                ystor = yt;
                textwrite();
                xt = xstor;
                yt = ystor;

                yt -= textsize / cfg.YMetric * dLead;

                dVcl = R + L;
                if (Math.Abs(R - L) < 0.001)
                {
                    textstring = "VCL = " + dVcl.ToString("0.000") + " m";
                }
                else
                {
                    textstring =  "VCL1 = " + R.ToString("0.000") + " m, VCL2 = " + L.ToString("0.000") + " m";
                }

                xstor = xt;
                ystor = yt;
                textwrite();
                xt = xstor;
                yt = ystor;

                yt -= textsize / cfg.YMetric * dLead;

                textstring = "g1 = " + B1.ToString("0.000") + " %, g2 = " + B2.ToString("0.000") + " % ";
                xstor = xt;
                ystor = yt;
                textwrite();
                yt = ystor;
                xt = xstor;

                yt -= textsize / cfg.YMetric * dLead;

                if (turn1 == 1) R = 0.0 - R;
                textstring = "Grade Difference = " + DEL.ToString("0.000") + " %";

                xstor = xt;
                ystor = yt;
                textwrite();
                xt = xstor;
                yt = ystor;

                yt -= textsize / cfg.YMetric * dLead;

                dK = Math.Abs(dVcl / (B2 - B1));

                if (turn1 == 2)
                {
                    dK *= -1;
                }

                textstring =  "K = " + dK.ToString("0.000");
                xstor = xt;
                ystor = yt;
                textwrite();
                xt = xstor;
                yt = ystor;

                
            }
            last_IPX = IPX;
            last_IPY = IPY;
        }

        void end_HIP()
        {
            /* End HIP */
            xt = xed - xlate_x;
            yt = yed - xlate_y;

            xt += 4.0 * textsize;
            yt += 4.0 * textsize;

            plan_form(ref xt, ref yt);
            textstring = "End chainage / Level: " + xed.ToString("0.000") + ", " + yed.ToString("0.000") + " m.";		// Sandipan 12/02/2008	Kolkata
            textwrite();
        }

        void plan_form(ref double xfm, ref double yfm)
        {
            double x_temp, y_temp;

            /* PLANFORM transforms a point's location by rotation xfa +ve is anti-clock. */

            x_temp = xfm;
            y_temp = yfm;

            xfm = x_temp * Math.Cos(xfa) - y_temp * Math.Sin(xfa);
            yfm = y_temp * Math.Sin(xfa) + y_temp * Math.Cos(xfa);

        }

        void textwrite()
        {
            xta = xt;
            xta += xlate_x;

            yta = yt;
            yta += xlate_y;

            

            write_text();
        }

        void write_text()
        {
            Eldtype eld = new Eldtype();
            TEXTtype text = new TEXTtype();

            text.text = textstring;
            text.Length = (short)textstring.Length;
            text.scatt = 1;
            text.Layer = "$$$$_15";
            text.laatt = 1;
            text.x1 = xta;
            text.y1 = yta;
            text.Size = textsize;
            text.rotn = textrotn;
            text.Color = 15;
            text.style = 1;
            text.Label =  modstr + ":" + stgstr;

            eld.Code = 9;

            fp2.Write(eld.Code);
            text.ToStream(fp2);
        }


    }
}
