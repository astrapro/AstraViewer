using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HeadsUtils;

namespace HeadsFunctions1.Chainage
{
    internal class CChainageUtil
    {
        #region MEMBER VARIABLES
        double R, SL, L, LC, M, B1, B2, DEL, RC, rep_chn, xst, yst, pip_size;
        double b2, b3, store_RC, stchn, brgi, RX, RY, r9;
        double T1, T2, IPX, IPY, endchn, last_stchn, last_endchn, Xp, Yp, X7, Y7;
        double X8, Y8, X9, Y9, last_RC, last_xed, last_yed;
        double xed, yed, last_RX, last_RY, last_sc;
        double XMIN, YMIN;
        double inc, rotn, textsize, textrotn, xfa;
        double XL, YL, XR, YR, prevch;
        double sc = 0;
        double store_rep = 0;

        //double RCI, interval, radc, minsp, last_spchn, ZL, psi, tsi, val, curchn
        double[] spchn = new double[100];

        short  turn, eltype, last_lt;
		short last_dg, last_mn, sp, tsp, store_flag;
		short findflag, elatt, elno;
        short dg = 0;
        short mn = 0;

        //short opnum, run, ind, last_cno, new1, recnum, IN_flag, newrec, cno, pco, tco,  
        //short chnind, err_code, i, no, style; 
        int spchn_code, last_eltype;

        string str1, model, stglbl, modnam;
		string strnam, radcurve,last_curve, last_modnam;
		string last_strnam, path,pathfile;
        string textstring;
        //string uline, str2, PTs, filstr, 
		 
        int ibrg;
        
        BinaryReader fp1;
        BinaryWriter fp2;
        StreamReader fp1SR;
        #endregion

        public CChainageUtil()
        {

        }

        #region PUBLIC METHODS
        public void Funcmain(string argv1)
        {
            path = argv1;
            
            //err_code = 9; 
            
            read_input();
            
            pathfile = Path.Combine(path, "chain2.tmp");
            fp2 = new BinaryWriter(new FileStream(pathfile, FileMode.Create), Encoding.Default);
            
            if(rotn != 0.0)
            {
                textstring = "DRG not by 'Draw String', use 'Minor option 1105'...";
                error_msg();
                return;
            }
            
            constants();

            pathfile = Path.Combine(path, "HALIGN.TMP");

            if (File.Exists(pathfile) == false)
            {
                pathfile = Path.Combine(path, "HALIGN.FIL");
            }

            if (File.Exists(pathfile) == false)
            {
                textstring = "File %s not found..." + pathfile;
                error_msg();
            }
            else
            {
                fp1SR = new StreamReader(pathfile);
                last_eltype = 0;

                while (fp1SR.EndOfStream == false)
                {
                    designed_curve_input();

                    if (findflag == 0)
                        break;

                    element();

                    prev_curv_param();

                    //ind = 1;
                }

                RC = last_RC;

                fp1SR.Close();
            }            
            fp2.Close();
        }
        #endregion

        #region PRIVATE METHODS
        void constants()
        {
            RC = 0.0;
            tsp = 0;
            //minsp = 9999999.0;
            r9 = 57.29577951;
            if (rotn < 0) rotn += 360;
            else if (rotn > 360) rotn -= 360;
            xfa = rotn / r9;
            //ZL = -999.9;
            pip_size = textsize * 0.5;
            //new1 = 0;
            //ind = 0;
        }

        void read_input()
        {
            //IN_flag = 0;

            pathfile = Path.Combine(path, "chain1.tmp");

            if (File.Exists(pathfile) == false)
            {
                textstring = "File %s not found..." + pathfile;
                error_msg();
            }
            else
            {
                HeadsUtils.Chainage chain = new HeadsUtils.Chainage();

                fp1 = new BinaryReader(new FileStream(pathfile, FileMode.Open, FileAccess.Read), Encoding.Default);

                chain.ModelName = ViewerUtils.ConvertCharArrayToString(fp1.ReadBytes(30));
                chain.StringName = ViewerUtils.ConvertCharArrayToString(fp1.ReadBytes(20));
                chain.XMin = fp1.ReadDouble();
                chain.YMin = fp1.ReadDouble();
                chain.ChainageInterval = fp1.ReadDouble();
                chain.TextSize = fp1.ReadDouble();
                chain.Rotation = fp1.ReadDouble();

                model = chain.ModelName;
                stglbl = chain.StringName;

                inc = chain.ChainageInterval;
                textsize = chain.TextSize;
                rotn = chain.Rotation;
                XMIN = chain.XMin;
                YMIN = chain.YMin;

                fp1.Close();

            }          
        }
         
        void error_msg()
        {
            //err_code = -999;
            XR = YR = 100.0;
            textsize = 5;
            textrotn = 0;
            write_text();
            if (fp1 != null)
            {
                fp1.Close();
            }
            return;
        }

        void write_text()
        {
            Eldtype eld = new Eldtype();
            TEXTtype text = new TEXTtype();

            text.text = textstring;
            text.Length = (short)textstring.Length;
            text.scatt=1;
            text.Layer = "$$$$_15";

            text.laatt = 1;
            text.x1 = XR;
            text.y1 = YR;
            text.Size = textsize;
            text.rotn = textrotn;
            text.Color = 15;
            text.style = 1;
            text.Label = modnam + ":" + stglbl;
            eld.Code = 9;

            fp2.Write(eld.Code);
            text.ToStream(fp2);           
        }

        void designed_curve_input()
        {
            findflag = 0;

            while (fp1SR.EndOfStream == false)
            {
                CHalignFilData fildata = CHalignFilData.Parse(fp1SR.ReadLine());
                if (fildata == null)
                {
                    continue;
                }
                
                modnam = fildata.sMod;
                strnam = fildata.sString;
                elno = (short)fildata.iSlno;
                eltype = fildata.iEltype;
                stchn = fildata.dStartchn;
                endchn = fildata.dEndchn;
                IPX = fildata.dHipx;
                IPY = fildata.dHipy;
                T1 = fildata.dT1;
                T2 = fildata.dT2;
                R = fildata.dRadius;
                L = fildata.dEllength;
                xst = fildata.dStartx;
                yst = fildata.dStarty;
                xed = fildata.dEndx;
                yed = fildata.dEndy;
                B1 = fildata.dB1;
                B2 = fildata.dB2;
                DEL = fildata.dDeviation;
                turn = fildata.iTurn;              

                if (stchn == last_stchn && endchn == last_endchn)
                {
                    findflag = 0;
                    break;
                }

                if ((modnam == model) && (strnam == stglbl))
                {
                    findflag = 1;
                    break;
                }
            }

            if (findflag != 0)
            {
                initiate_param();

                start_param();

                if (eltype == 1) { SL = L; L = 0; LC = 0; M = 0; }
                else if (eltype == 2) { /*L = L;*/ SL = 0; LC = 0; M = 0; }
                else if (eltype == 3) { LC = L; SL = 0; L = 0; M = 0; }
                else if (eltype == 4) { M = L; SL = 0; LC = 0; L = 0; }
            }
        }

        void initiate_param()
        {
            last_RX = 0;
            last_RY = 0;
            last_stchn = 0;
            last_endchn = 0;

            last_xed = 0;
            last_yed = 0;

            last_modnam = string.Empty;
            last_strnam = string.Empty;
        }

        void start_param()
        {
            short n;

            RC = stchn;

            n = 0;
            b2 = 90;
            b3 = 90;
            if (turn == 1) b2 = b3 = -90;

            do
            {
                RC = inc * n;
                n++;
            } while (RC < stchn);

            n = 0;

            do
            {
                rep_chn = inc * n;
                n++;
            } while (RC < stchn);
        }

        void prev_curv_param()
        {
            last_stchn = stchn;
            last_endchn = endchn;

            last_xed = xed;
            last_yed = yed;

            last_modnam = modnam;
            last_strnam = strnam;

            last_lt = eltype;
            last_RX = RX;
            last_RY = RY;
            last_dg = dg;
            last_mn = mn;
            last_sc = sc;
            last_curve = radcurve;
        }

        void element()
        {
            //short chk = 0;

            store_RC = RC;
            RC = stchn;

            //if (RC >= 500.0)
            //    chk = 1;

            if (SL != 0)
                straight();
            else if (L != 0)
                lead_tran();
            else if (LC != 0)
                circular();
            else if (M != 0)
                trail_tran();

            pip_edges();
            draw_chain();

            RC = store_RC;

            if (RC > stchn)
                RC -= inc;

            last_RC = RC;

            do
            {
                RC += inc;

                if (RC >= endchn)
                    break;

            sppt1:

                for (sp = 0; sp < tsp; sp++)
                    if (spchn[sp] > stchn && RC > spchn[sp] && spchn[sp] > last_RC)
                    {
                        store_RC = RC;
                        RC = spchn[sp];
                        store_flag = 1;
                        break;
                    }
                    else store_flag = 0;

                //    sppt2:

                if (SL != 0)
                    straight();
                else if (L != 0)
                    lead_tran();
                else if (LC != 0)
                    circular();
                else if (M != 0)
                    trail_tran();

                pip_edges();
                draw_chain();

                last_RC = RC;

                if (store_flag == 1)
                {
                    RC = store_RC;
                    rep_chn = store_rep;
                    store_flag = 0;
                    goto sppt1;
                }

            } while (RC < endchn);

            store_RC = RC;
            RC = endchn;

            if (SL != 0)
                straight();
            else if (L != 0)
                lead_tran();
            else if (LC != 0)
                circular();
            else if (M != 0)
                trail_tran();

            pip_edges();
            draw_chain();

            last_RC = RC;

            RC = store_RC;

        }

        void pip_edges()
        {
            if (turn == 2)
            {
                XL = X8;
                YL = Y8;
                XR = X9;
                YR = Y9;
            }
            else if (turn == 1)
            {
                XR = X8;
                YR = Y8;
                XL = X9;
                YL = Y9;
            }
        }

        void draw_chain()
        {
            plan_form(ref XL, ref YL);

            plan_form(ref XR, ref YR);

            textrotn += rotn;

            if (textrotn > 360) textrotn -= 360;
            if (textrotn < 0) textrotn += 360;


            write_drg();

            prevch = RC;
        }

        void plan_form(ref double xfm, ref double yfm)
        {
            double x_temp, y_temp;

            x_temp = xfm - XMIN;
            y_temp = yfm - YMIN;

            xfm = XMIN + (x_temp * Math.Cos(xfa) - y_temp * Math.Sin(xfa));
            yfm = YMIN + (y_temp * Math.Sin(xfa) + y_temp * Math.Cos(xfa));
        }

        void write_drg()
        {
            elatt = 0;

            write_line();

           
            str1 = string.Empty;
            spchn_code = 0; // March 28, 2001

            if (eltype != last_eltype && last_eltype != 0)
            {
                spchn_code = 1; // March 28, 2001

                if (eltype == 1)
                {
                    if (last_eltype == 3)
                        str1 = "  CT";
                    else if (last_eltype == 4)
                        str1 = "  ST";
                }
                else if (eltype == 2)
                {
                    if (last_eltype == 1)
                        str1 = "  TS";
                    else if (last_eltype == 4)
                        str1 = "  SS";
                }
                else if (eltype == 3)
                {
                    if (last_eltype == 1)
                        str1 = "  TC";
                    else if (last_eltype == 2)
                        str1 = "  SC";
                }
                else if (eltype == 4)
                {
                    if (last_eltype == 3)
                        str1 = "  CS";
                    else if (last_eltype == 2)
                        str1 = "  SS";
                }
            }


            if (spchn_code != 0)
            {
                textstring = stchn.ToString("0.000") + " "  + str1;
            }
            else
            {
                textstring = RC.ToString("0.000");
            }
           
            write_text();
            last_eltype = eltype;
        }

        void write_line()
        {
            Eldtype eld = new Eldtype();
            Linetype line = new Linetype();

            line.elatt = elatt;
            line.scatt = 1;
            line.layer = "$$$$_15";
            line.laatt = 1;
            line.x1 = XL;
            line.y1 = YL;
            line.z1 = 0;
            line.x2 = XR;
            line.y2 = YR;
            line.z2 = 0;
            line.color = 15;
            line.style = 1;
            line.width = 1;

            line.label = modnam + ":" + stglbl;

            eld.Code = 1;

            fp2.Write(eld.Code);
            line.ToStream(fp2);                     
        }

        void inter_bearing(double BI, double ths)
        {
            if (eltype == 2 && turn == 1)
                brgi = BI + ths * r9;

            else if (eltype == 2 && turn == 2)
                brgi = BI - ths * r9;

            else if (eltype == 4 && turn == 1)
                brgi = BI - ths * r9;

            else if (eltype == 4 && turn == 2)
                brgi = BI + ths * r9;

            if (brgi < 0.0) brgi = brgi + 360.0;
            else if (brgi > 360.0) brgi = brgi - 360.0;
        }

        void straight()
        {
            brgi = B1;
            if (brgi < 0.0) brgi = brgi + 360.0;
            else if (brgi > 360.0) brgi = brgi - 360.0;
            radcurve = "INFINITY";

            Xp = xst + (RC - stchn) * Math.Sin(B1 / r9);
            Yp = yst + (RC - stchn) * Math.Cos(B1 / r9);

            if (turn == 1)
                b2 = b3 = -90;
            else
                b2 = b3 = 90;

            X8 = Xp + pip_size * Math.Sin((B1 - b3) / r9);
            Y8 = Yp + pip_size * Math.Cos((B1 - b3) / r9);

            X9 = Xp + pip_size * Math.Sin((B1 + b3) / r9);
            Y9 = Yp + pip_size * Math.Cos((B1 + b3) / r9);

            RX = Xp;
            RY = Yp;

            textrotn = 360 - B1;
            if (textrotn > 360) textrotn -= 360;
        }

        void lead_tran()
        {
            double ths, P1, P2, X6, Y6;

            ths = (RC - stchn) * (RC - stchn) / (2 * L * R);
            P1 = (1 - ths * ths / 10) * (RC - stchn);
            P2 = (ths / 3 - ths * ths * ths / 42) * (RC - stchn);

            if (turn == 1)
            {
                ths = 0.0 - ths;
                b2 = 0.0 - 90;
                b3 = 0.0 - 90;
            }

            else
            {
                b2 = 90;
                b3 = 90;
            }

            Y6 = yst + P1 * Math.Cos(B1 / r9);
            X6 = xst + P1 * Math.Sin(B1 / r9);
            Y7 = Y6 + Math.Abs(P2) * Math.Cos((B1 + b2) / r9);
            X7 = X6 + Math.Abs(P2) * Math.Sin((B1 + b2) / r9);
            Y8 = Y7 + pip_size * Math.Cos((B1 - b3) / r9 + ths);
            X8 = X7 + pip_size * Math.Sin((B1 - b3) / r9 + ths);
            Y9 = Y7 + pip_size * Math.Cos((B1 + b3) / r9 + ths);
            X9 = X7 + pip_size * Math.Sin((B1 + b3) / r9 + ths);

            RX = X7;
            RY = Y7;

            textrotn = 360 - (B1 + ths * r9);
            if (textrotn > 360) textrotn -= 360;

            if (RC != last_RC)
                inter_bearing(B1, ths);

            if (RC == endchn)
            {
                ibrg = (int)(brgi * 1000.0 + 1);
                brgi = (long)ibrg / 1000.0;
            }
        }

        void circular()
        {
            double X0, Y0, F, F1, BB1;

            if (turn == 1) b2 = 0.0 - 90;
            else b2 = 90;

            Y0 = yst + R * Math.Cos((B1 + b2) / r9);
            X0 = xst + R * Math.Sin((B1 + b2) / r9);

            F = (RC - stchn) * r9 / R;
            if (turn == 1) F = 0.0 - F;

            brgi = B1 + F;
            if (brgi < 0.0) brgi = brgi + 360.0;
            else if (brgi > 360.0) brgi = brgi - 360.0;

            F1 = B1 + b2 + F;

            Y7 = Y0 - R * Math.Cos(F1 / r9);
            X7 = X0 - R * Math.Sin(F1 / r9);

            Y8 = Y7 - pip_size * Math.Cos(F1 / r9);
            X8 = X7 - pip_size * Math.Sin(F1 / r9);
            Y9 = Y7 + pip_size * Math.Cos(F1 / r9);
            X9 = X7 + pip_size * Math.Sin(F1 / r9);

            RX = X7;
            RY = Y7;

            BB1 = B1 + F;

            textrotn = 360.0 - BB1;
            if (textrotn > 360) textrotn -= 360;

        }

        void trail_tran()
        {
            double ths, P1, P2, X6, Y6, BB;

            ths = (endchn - RC) * (endchn - RC) / (2 * M * R);
            P1 = (1 - ths * ths / 10) * (endchn - RC);
            P2 = (ths / 3 - ths * ths * ths / 42) * (endchn - RC);


            if (turn == 2)
            {
                ths = 0.0 - ths;
                b2 = -90;
                b3 = -90;
            }
            else
            {
                ths = Math.Abs(ths);
                b2 = 90;
                b3 = 90;
            }

            BB = B2 + 180;

            Y6 = yed + P1 * Math.Cos(BB / r9);
            X6 = xed + P1 * Math.Sin(BB / r9);
            Y7 = Y6 + P2 * Math.Cos((BB + b2) / r9);
            X7 = X6 + P2 * Math.Sin((BB + b2) / r9);

            Y8 = Y7 + pip_size * Math.Cos((BB - b3) / r9 + ths);
            X8 = X7 + pip_size * Math.Sin((BB - b3) / r9 + ths);
            Y9 = Y7 + pip_size * Math.Cos((BB + b3) / r9 + ths);
            X9 = X7 + pip_size * Math.Sin((BB + b3) / r9 + ths);

            RX = X7;
            RY = Y7;

            textrotn = 360 - (B2 + ths * r9);
            if (textrotn > 360) textrotn -= 360;



            brgi = B1;

            if (RC != last_RC)
                inter_bearing(B2, ths);

            if (RC == endchn)
            {
                ibrg = (int)(brgi * 1000.0 + 1);
                brgi = ibrg / 1000.0;
            }

        }
        #endregion
    }
}
