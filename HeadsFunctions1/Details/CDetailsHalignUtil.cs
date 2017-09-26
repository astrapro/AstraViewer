using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HeadsUtils;

namespace HeadsFunctions1.Details
{
    internal class CDetailsHalignUtil
    {
        #region MEMBER VARIABLES
        BinaryReader fp1;
        BinaryWriter fp2;
        StreamReader fp1SR;
        
        short turn1,  elemno, eltype, flag;
        
        double xlate_x, xlate_y, XMIN, YMIN, textsize, textrotn, rotn, xfa, xta, yta;
        double R, L, B1, B2, DEL,  xst, yst, xed, yed;
        double stchn, endchn, T1, T2, IPX, IPY, last_stchn;
        double last_endchn, xt, yt, last_IPX, last_IPY, xstor, ystor, ths, thc, qb1, qb2;
        double Ls, Lc, XST, YST, V, SE;
        double XL, YL, XR, YR;
        double Ls1, Ls2, Xc, Yc, HIPchainage, startchn, finchn, Brg1, Brg2;
        double ltc_x, ltc_y, rbc_x, rbc_y;
        double lastB2;

        int lasteltype;
        int elcount;
        int last_eltype;
        int start_index = 0;
        
        string path, pathfile;
        string modstr, stgstr, textstring;       
        #endregion

        public CDetailsHalignUtil()
        {

        }

        #region PUBLIC METHODS
        public void Funcmain(string argv1)
        {

            path = argv1;
            //err_code = 9; /* no error */
            V=130.0;
            
            read_input();
            
            pathfile = Path.Combine(path, "DETAILS2.TMP");

            fp2 = new BinaryWriter(new FileStream(pathfile , FileMode.Create), Encoding.Default);
	
            if(rotn != 0.0)
	        {
		        textstring = "DRG not by 'Draw String', use 'Minor option(s) 1103 / 1104'...";
		        error_msg();
		        return;
	        }

            pathfile = Path.Combine(path, "HALIGN.TMP");

            if (File.Exists(pathfile) == false)
            {
                pathfile = Path.Combine(path, "HALIGN.FIL");
            }

            if (File.Exists(pathfile) == false)
            {
                textstring = "File %s not found..." + pathfile;
                error_msg();
                return;
            }
            fp1SR = new StreamReader(pathfile);
            
            //index = false;
            elcount=0;
            int telemno = 0;
            HIPchainage = 0.0;
            string mdlnam, strnam;	// Sandipan 25082008 Kolkata

            while (fp1SR.EndOfStream == false)
            {
                Ls1=0.0; Ls2=0.0;

                CHalignFilData fildata = CHalignFilData.Parse(fp1SR.ReadLine());

                mdlnam = fildata.sMod;
                strnam = fildata.sString;
                telemno = fildata.iSlno;
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
                turn1 = fildata.iTurn;                
                
                if ((telemno > 0) && (mdlnam == modstr) && (strnam == stgstr))
                {
                    finchn = endchn;
                }
            }
            
            fp1SR.Close();

            fp1SR = new StreamReader(pathfile);

            while (fp1SR.EndOfStream == false)
            {
                init_param();
                des_curve_input();
                
                if( flag == 0)
                    continue;
                
                if((stchn == last_stchn) && (endchn == last_endchn))
                    break;
                
                //if(start_index == 0)
                //{
                //    XST = xst;
                //    YST = yst;
                //    start_index = 1;
                //}
                
                
                draw_hips();
                
                //index = true;
                
                if(endchn==finchn)
                    break;

            }	// end while(fp1SR.EndOfStream)
            
            end_HIP();

            fp1.Close();
            fp1SR.Close();
            fp2.Close();
        }
        #endregion

        #region PRIVATE METHODS
        void read_input()
        {
            HeadsUtils.Detail det;
            double r9;

            r9 = 57.29578;

            pathfile = Path.Combine(path, "DETAILS1.tmp");

            if (File.Exists(pathfile) == false)
            {
                textstring = "File %s not found..." + pathfile;
                error_msg();
            }

            fp1 = new BinaryReader(new FileStream(pathfile, FileMode.Open, FileAccess.Read), Encoding.Default);
            det = HeadsUtils.Detail.FromStream(fp1);


            modstr = det.ModelName;
            stgstr = det.StringName;
            textsize = det.TextSize;
            rotn = det.Rotation;
            XMIN = det.XMin;
            YMIN = det.YMin;

            fp1.Close();

            xfa = rotn / r9;
            xlate_x = XMIN;
            xlate_y = YMIN;
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

        void write_text()
        {
            Eldtype eld = new Eldtype();
            TEXTtype text = new TEXTtype();

            eld.Code = 9;

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
            text.Label = modstr + ":" + stgstr;

            fp2.Write(eld.Code);

            text.ToStream(fp2);
        }

        void init_param()
        {
            IPX = IPY = 0.0;
        }

        void des_curve_input()
        {
            string mdlnam;
            string strnam;
	        int telemno = 0, teltype = 0;
	        double tstchn = 0.0 , tendchn = 0.0, tIPX = 0.0, tIPY = 0.0;
	        double tT1 = 0.0 , tT2 = 0.0, tR = 0.0, tL = 0.0;
	        double txst = 0.0 , tyst = 0.0, txed = 0.0, tyed = 0.0;
	        double tB1 = 0.0 , tB2 = 0.0, tDEL = 0.0;
	        int tturn1 = 0;
            Ls1=0.0; Ls2=0.0;

            
            while(true)
            {
                string strLine = fp1SR.ReadLine();
                if (strLine == null)
                {
                    break;
                }
                CHalignFilData fildata = CHalignFilData.Parse(strLine);

                mdlnam = fildata.sMod;
                strnam = fildata.sString;
                telemno = fildata.iSlno;
                teltype = fildata.iEltype;
                tstchn = fildata.dStartchn;
                tendchn = fildata.dEndchn;
                tIPX = fildata.dHipx;
                tIPY = fildata.dHipy;
                tT1 = fildata.dT1;
                tT2 = fildata.dT2;
                tR = fildata.dRadius; 
                tL = fildata.dEllength;
                txst = fildata.dStartx;
                tyst = fildata.dStarty;
                txed = fildata.dEndx;
                tyed = fildata.dEndy;
                tB1 = fildata.dB1;
                tB2 = fildata.dB2;
                tDEL = fildata.dDeviation;
                tturn1 = fildata.iTurn;
		
                if(lasteltype == teltype && last_endchn == tendchn)	
                {
                    break;
                }
                
                if( (telemno > 0) && (mdlnam == modstr) && (strnam == stgstr) )
                {
                    flag=1; // final 13/09/01 DD
                    elemno = (short)telemno;
                    eltype = (short)teltype;
                    stchn = tstchn;
                    endchn = tendchn;
                    
                    //if(telemno==1)
                    if (eltype == 1)
                    {
                        xst = txst;
                        yst = tyst;
                        Xc = txst;
                        Yc = tyst;
                        startchn = tstchn;
                        HIPchainage = tstchn;
                        B1 = tB1;
                        B2 = tB2;
                    }
                    if(eltype==2)
                    {
                        Ls1 = tL;
                        Brg1 = tB1;
                    }
                    else if(eltype == 3)
                    {
                        Lc = tL;
                        elcount++;
                        IPX = tIPX;
                        IPY = tIPY;
                        T1 = tT1 ;
                        T2 = tT2;
                        R = tR;
                        L = tL;
                        xed = txed;
                        yed = tyed;
                        B1 = tB1;
                        B2 = tB2;
                        DEL = tDEL;
                        turn1 = (short)tturn1;
                        HIPchainage += Math.Sqrt((Xc-IPX)*(Xc-IPX)+(Yc-IPY)*(Yc-IPY));
                        Xc = IPX;
                        Yc = IPY;
                    }
                    if(eltype == 4)
                    {
                        Ls2=tL;
                        Brg2=tB2;
                    }

                    if (start_index == 0)
                    {
                        XST = xst;
                        YST = yst;
                        start_index = 1;
                    }

                    if (tIPX == 0.0 && tIPY == 0.0)
                    {
                        xed = txed;
                        yed = tyed;
                        break;
                    }
                    else
                    {
			            if(eltype == 1 && (lasteltype == 3 || lasteltype == 4))
			            {
                            xed = txed;
                            yed = tyed;
				            break;
			            }
                    }
                                   
                
                }
                else
                {
                    flag = 0;
                }
                
                lasteltype=teltype;
                last_stchn = stchn;
                last_endchn = endchn;
                last_eltype = eltype;
            }
        }

        void end_HIP()
        {
            xt = XST - xlate_x;
            yt = YST - xlate_y;

            xt += 4.0 * textsize;
            yt += 4.0 * textsize;

            plan_form(ref xt, ref yt);
            textstring = "Start Co-ordinates : E = " + XST.ToString("0.000")  + ", N = " + YST.ToString("0.000") + " m.";
            textwrite();

            if (IPX > 0 && IPY > 0)
            {
                yt -= (2 * textsize);
                textstring = "HIP Chainage = " + startchn.ToString("0.000") + " m.\n";
                xstor = xt;
                ystor = yt;
                textwrite();
                xt = xstor;
                yt = ystor;
            }

            xt = xed - xlate_x;
            yt = yed - xlate_y;

            xt += 4.0 * textsize;
            yt += 4.0 * textsize;

            plan_form(ref xt, ref yt);
            textstring = "End Co-ordinates : E = " + xed.ToString("0.000") + ", N = " + yed.ToString("0.000") + " m.";
            textwrite();

            if (IPX > 0 && IPY > 0)
            {
                yt -= (2 * textsize);
                HIPchainage += Math.Sqrt((Xc - xed) * (Xc - xed) + (Yc - yed) * (Yc - yed));
                textstring = "HIP Chainage = " + HIPchainage.ToString("0.000") + " m.\n";
                xstor = xt;
                ystor = yt;
                textwrite();
                xt = xstor;
                yt = ystor;
            }
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

        void wcb2qb()
        {
            if (B1 < 0) B1 += 360;
            else if (B1 > 360) B1 -= 360;

            if (B1 >= 0 && B1 < 90) qb1 = B1;
            else if (B1 >= 90 && B1 < 180) qb1 = 180 - B1;
            else if (B1 >= 180 && B1 < 270) qb1 = B1 - 180;
            else if (B1 >= 270 && B1 < 360) qb1 = 360 - B1;

            if (B2 < 0) B2 += 360;
            else if (B2 > 360) B2 -= 360;

            if (B2 >= 0 && B2 < 90) qb2 = B2;
            else if (B2 >= 90 && B2 < 180) qb2 = 180 - B2;
            else if (B2 >= 180 && B2 < 270) qb2 = B2 - 180;
            else if (B2 >= 270 && B2 < 360) qb2 = 360 - B2;

        }

        void draw_hips()
        {
            //zt = 0.0;
            textrotn = 0.0;
            xt = xed - xlate_x;
            yt = yed - xlate_y;

            if (IPX == 0 && IPY == 0) /* ELEMENT METHOD of Alignment, plan minor option 1103 */
            {
                //int Delta_deg = 0, Delta_min = 0, Delta_sec = 0, Dc_deg = 0, Dc_min = 0, Dc_sec = 0;
                //int Brg_deg1 = 0, Brg_min1 = 0; double Brg_sec1 = 0.0;
                //int Brg_deg2 = 0, Brg_min2 = 0; double Brg_sec2 = 0.0;

                int Delta_c_deg, Delta_c_min;
                //double Theta_s_deg, Theta_s_min, Theta_s_sec, PHI_c_sec;
                double Delta_c_sec, min;
                double Delta, Delta_c;

                //Brg_deg1 = (int)B1; min1 = Math.Abs(B1 - Brg_deg1) * 60.0; Brg_min1 = (int)min1; Brg_sec1 = Math.Abs(min1 - Brg_min1) * 60.0;

                Delta = Math.Abs(B1-lastB2);
                Delta_c = Delta;
                Delta_c_deg = (int)Delta_c; 
                min = (Delta_c - Delta_c_deg) * 60.0; 
                Delta_c_min = (int)min; 
                Delta_c_sec = (min - Delta_c_min) * 60.0;


                elcount++;

                if (elcount == 1)
                {
                    return;
                    //xt = xst - xlate_x;
                    //yt = yst - xlate_y;
                }
                else
                {
                    xt = xst - xlate_x;
                    yt = yst - xlate_y;
                }

                yt -= (2 * textsize);
                //textstring = "[ EL #: " + elcount.ToString("00000") + " ]";
                textstring = "[ EL #: " + elcount.ToString("") + " ]";
                xstor = xt;
                ystor = yt;
                textwrite();
                xt = xstor;
                yt = ystor;

                yt -= (2 * textsize);
                textstring = string.Format("HIP: E={0:f3}, N={1:f3} m.\n", xed, yed);
                xstor = xt;
                ystor = yt;
                textwrite();
                xt = xstor;
                yt = ystor;

                if (eltype == 1)
                {
                    if (elcount > 1)
                    {
                        yt -= (2 * textsize);
                        //textstring = string.Format("De={0,2:D}° {1,2:D}' {2,4:f2}\"", Delta_c_deg, Delta_c_min, Delta_c_sec);
                        textstring = "Deviation = " + Delta.ToString("0.000") + " Degree.";
                        xstor = xt;
                        ystor = yt;
                        textwrite();
                        xt = xstor;
                        yt = ystor;
                    }

                    yt -= (2 * textsize);
                    //textstring = string.Format("Bearings: B1={0,2:D}° {1,2:D}' {2,4:f2}\",  B2={3,2:D}° {4,2:D}' {5,4:f2}\"", Brg_deg1.ToString, Brg_min1.ToString, Brg_sec1.ToString, Brg_deg2, Brg_min2, Brg_sec2);
                    textstring = "W. C. Bearing = " + B1.ToString("0.000") + " Degree.";
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;
                      

                    yt -= (2 * textsize);
                    textstring = "STRAIGHT :  L = " + L.ToString("0.000") + " m.";
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;
                }

                else if (eltype == 2 || eltype == 4)
                {
                    yt -= (2 * textsize);
                    textstring = "L = "+ L.ToString("0.000") +"m.";
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    wcb2qb();
                    ths = qb2 - qb1;
                    ths = Math.Abs(ths);

                    yt -= (2 * textsize);
                    textstring = "Theta_s = " + ths.ToString("0.000") + " deg";
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    yt -= (2 * textsize);
                    textstring = "Curvature = " + R.ToString("0.000") + "m";
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    yt -= (2 * textsize);   // below 
                    textstring = "Start Brg = " + B1.ToString("0.000") + "Deg";
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    yt -= (2 * textsize);   // below 
                    textstring = "End Brg = " + B2.ToString("0.000") + "Deg";
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    yt -= (2 * textsize);

                    if (eltype == 2)
                        textstring = "Lead TRANSITION";
                    else if (eltype == 4)
                        textstring = "Trail TRANSITION";

                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;
                }

                else if (eltype == 3)
                {
                    yt -= (2 * textsize);
                    textstring = "Rc = " + R.ToString("0.000") + "m";
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    yt -= (2 * textsize);
                    textstring = "Lc = " + L.ToString("0.000") + "m";
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    wcb2qb();
                    thc = qb2 - qb1;
                    thc = Math.Abs(thc);

                    yt -= (2 * textsize);
                    textstring = "Theta_c = " + thc.ToString("0.000") + " deg";
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    yt -= (2 * textsize);   // below 
                    textstring = "Start Brg = " + B1.ToString("0.000") + " Degrees";
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;
                    
                    yt -= (2 * textsize);   // below 
                    textstring = "End Brg = " + B2.ToString("0.000") + "Degrees";
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    yt -= (2 * textsize);
                    textstring = "CIRCULAR";
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;
                }
                lastB2 = B2;
            }
            else      // IP METHOD of Alignment,
            {
                int Delta_c_deg, Delta_c_min;
                double Theta_s_deg, Theta_s_min, Theta_s_sec, PHI_c_sec, Delta_c_sec, min;
                double Delta, Delta_c;

                int Delta_deg = 0, Delta_min = 0;
                int Brg_deg1 = 0, Brg_min1 = 0; double Brg_sec1 = 0.0, Dc_sec = 0.0, Dc_min = 0.0;
                int Brg_deg2 = 0, Brg_min2 = 0; double Brg_sec2 = 0.0, Delta_sec = 0.0, Dc_deg = 0.0;

                int PHI_c_deg, PHI_c_min;
                               
                double PHI_c, Theta_s_rad, LC;
                double Rc, Dc, Theta_s, Ts, Es, Xs, Ys, LT, ST, Lc, p, k, Tc;
                double r9 = 57.29577951;

                
                double min1, Theta_s_rad1, Theta_s1 = 0, Xs1, Ys1, p1, k1, Ts1, Es1;
                int Theta_s_deg1, Theta_s_min1;
                double Theta_s_sec1;

                double min2, Theta_s_rad2, Theta_s2 = 0, Xs2, Ys2, p2, k2, Ts2, Es2;
                int Theta_s_deg2, Theta_s_min2;
                double Theta_s_sec2;

                double PHI_c1, PHI_c2;
                int PHI_c_deg1, PHI_c_min1; double PHI_c_sec1;
                int PHI_c_deg2, PHI_c_min2; double PHI_c_sec2;
                                

                double LC1, LT1, ST1;
                double LC2, LT2, ST2;



                if (Ls1 == 0 && Ls2 == 0)	//(eltype==3) // IP Method without Transition curves
                {
                    Ls = 0;

                    Delta = DEL;
                    //Delta_deg = (int)Delta; min = (Delta - Delta_deg) * 60.0; Delta_min = (int)min; Delta_sec = (min - Delta_min) * 60.0;
                    Delta_deg = (int)Delta; min = (Delta - Delta_deg) * 60.0; Delta_min = (int)min; //Delta_sec = (min - Delta_min) * 60.0;
                    Rc = R;

                    Dc = 5729.578 / Rc;
                    //Dc_deg = (int)Dc; min = (Dc - Dc_deg) * 60.0; Dc_min = (int)min; Dc_sec = (min - Dc_min) * 60.0;
                    Dc_deg = (int)Dc; min = (Dc - Dc_deg) * 60.0; Dc_min = (int)min; //Dc_sec = (min - Dc_min) * 60.0;

                    Theta_s_rad = Ls / (2 * Rc);
                    Theta_s = Theta_s_rad * 180.0 / 3.14159;
                    Theta_s_deg = (int)Theta_s; min = (Theta_s - Theta_s_deg) * 60.0; Theta_s_min = (int)min; Theta_s_sec = (min - Theta_s_min) * 60.0;

                    PHI_c = Theta_s / 3.0;
                    PHI_c_deg = (int)PHI_c; min = (PHI_c - PHI_c_deg) * 60.0; PHI_c_min = (int)min; PHI_c_sec = (min - PHI_c_min) * 60.0;

                    Delta_c = Delta - 2 * Theta_s;
                    Delta_c_deg = (int)Delta_c; min = (Delta_c - Delta_c_deg) * 60.0; Delta_c_min = (int)min; Delta_c_sec = (min - Delta_c_min) * 60.0;

                    Xs = Ls * (1 - Theta_s_rad * Theta_s_rad / 10 + Theta_s_rad * Theta_s_rad * Theta_s_rad * Theta_s_rad / 216);
                    Ys = Ls * (Theta_s_rad / 3.0);

                    p = Ls * Ls / (24 * Rc);
                    k = Xs - Rc * Math.Sin(Theta_s / r9);

                    Ts = (Ls / 2.0) + (Rc + p) * Math.Tan(Delta / 2.0 / r9);
                    Tc = Rc * (Math.Tan(Delta / 2.0 / r9));
                    if (Tc < 0.0)
                        Tc = 0.0 - Tc;
                    Es = (Rc + p) * (1 / Math.Cos(Delta / 2.0 / r9)) - Rc;
                    Lc = Rc * Delta / r9;
                    LC = Math.Sqrt(Xs * Xs + Ys * Ys);
                    LT = Xs - Ys * (1 / Math.Tan(Theta_s / r9));
                    ST = Ys / Math.Sin(Theta_s / r9);

                    Brg_deg1 = (int)B1; min1 = Math.Abs(B1 - Brg_deg1) * 60.0; Brg_min1 = (int)min1; Brg_sec1 = Math.Abs(min1 - Brg_min1) * 60.0;
                    Brg_deg2 = (int)B2; min2 = Math.Abs(B2 - Brg_deg2) * 60.0; Brg_min2 = (int)min2; Brg_sec2 = Math.Abs(min2 - Brg_min2) * 60.0;

                    //xt = IPX-xlate_x;
                    xt = Xc - xlate_x;
                    xt += 12.0 * textsize;

                    //yt = IPY-xlate_y;
                    yt = Yc - xlate_y;
                    plan_form(ref xt, ref yt);

                    ltc_x = xt - 2 * textsize;
                    ltc_y = yt + 2 * textsize;

                    yt -= (2 * textsize);
                    textstring = string.Format("[ IP #: {0,5:D} ]", elcount);
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    yt -= (2 * textsize);
                    textstring = string.Format("CIRCULAR: E={0:f3}, N={1:f3} m.\n", IPX, IPY);
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    yt -= (2 * textsize);
                    textstring = string.Format("HIP Chainage={0:f3} m.\n", HIPchainage);
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    rbc_x = xt + 30 * textsize;
                    rbc_y = yt - 1 * textsize;

                    //write_box();

                    yt -= (2 * textsize);
                    textstring =  "";
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    yt -= (2 * textsize);
                    textstring = string.Format("De={0,2:D}° {1,2:D}' {2,4:f2}\"", Delta_deg, Delta_min, Delta_sec);
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    yt -= (2 * textsize);
                    textstring = string.Format("Bearings: B1={0,2:D}° {1,2:D}' {2,4:f2}\",  B2={3,2:D}° {4,2:D}' {5,4:f2}\"", Brg_deg1, Brg_min1, Brg_sec1, Brg_deg2, Brg_min2, Brg_sec2);
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    yt -= (2 * textsize);
                    textstring = string.Format("Rc={0,10:f3}", Rc);
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    yt -= (2 * textsize);
                    textstring = string.Format("Tc={0,10:f3}", Tc);
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    yt -= (2 * textsize);
                    textstring = string.Format("Dc={0,2:d}° {1,2:d}' {2,4:f2}\"", int.Parse(Dc_deg.ToString()), int.Parse(Dc_min.ToString()), Dc_sec);
                    //textstring = string.Format("Dc={0,2:D}° {1,2:D}' {2,4:f2}\"", Dc_deg, Dc_min, Dc_sec);
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    yt -= (2 * textsize);
                    textstring = string.Format("Lc={0,6:f3}", Lc);
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    yt -= (2 * textsize);
                    textstring = string.Format("Es={0,10:f3}", Es);
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    rbc_x = xt + 30 * textsize;
                    rbc_y = yt - 1 * textsize;

                    //write_box();
                    /**/
                }
                else	// IP Method with Transition Curves
                {
                    Delta = DEL;
                    //Delta_deg = (int)Delta; min = (Delta - Delta_deg) * 60.0; Delta_min = (int)min; Delta_sec = (min - Delta_min) * 60.0;
                    Delta_deg = (int)Delta; min = (Delta - Delta_deg) * 60.0; Delta_min = (int)min; //Delta_sec = (min - Delta_min) * 60.0;
                   
                    Rc = R;

                    Dc = 5729.578 / Rc;
                    //Dc_deg = (int)Dc; min = (Dc - Dc_deg) * 60.0; Dc_min = (int)min; Dc_sec = (min - Dc_min) * 60.0;
                    Dc_deg = (int)Dc; min = (Dc - Dc_deg) * 60.0; Dc_min = (int)min; Dc_sec = (min - Dc_min) * 60.0;
                    //Chiranjit 12 /10 /2009

                    //Delta_c = Delta - (Theta_s1 + Theta_s2);
                    //Delta_c_deg = (int)Delta_c; min = (Delta_c - Delta_c_deg) * 60.0; Delta_c_min = (int)min; Delta_c_sec = (min - Delta_c_min) * 60.0;

                    //Lc = Rc * Delta_c / r9;


                    // Separate values for Ls1 & Ls2

                    Theta_s_rad1 = Ls1 / (2 * Rc);
                    Theta_s1 = Theta_s_rad1 * 180.0 / 3.14159;
                    Theta_s_deg1 = (int)Theta_s1; min1 = Math.Abs(Theta_s1 - Theta_s_deg1) * 60.0; Theta_s_min1 = (int)min1; Theta_s_sec1 = Math.Abs(min1 - Theta_s_min1) * 60.0;

                    Theta_s_rad2 = Ls2 / (2 * Rc);
                    Theta_s2 = Theta_s_rad2 * 180.0 / 3.14159;
                    Theta_s_deg2 = (int)Theta_s2; min2 = Math.Abs(Theta_s2 - Theta_s_deg2) * 60.0; Theta_s_min2 = (int)min2; Theta_s_sec2 = Math.Abs(min2 - Theta_s_min2) * 60.0;

                    //chiranjit
                    Delta_c = Delta - (Theta_s1 + Theta_s2);
                    Delta_c_deg = (int)Delta_c; min = (Delta_c - Delta_c_deg) * 60.0; Delta_c_min = (int)min; Delta_c_sec = (min - Delta_c_min) * 60.0;

                    Lc = Rc * Delta_c / r9;


                    PHI_c1 = Theta_s1 / 3.0;
                    PHI_c_deg1 = (int)PHI_c1; min1 = Math.Abs(PHI_c1 - PHI_c_deg1) * 60.0; PHI_c_min1 = (int)min1; PHI_c_sec1 = Math.Abs(min1 - PHI_c_min1) * 60.0;

                    PHI_c2 = Theta_s2 / 3.0;
                    PHI_c_deg2 = (int)PHI_c2; min2 = Math.Abs(PHI_c2 - PHI_c_deg2) * 60.0; PHI_c_min2 = (int)min2; PHI_c_sec2 = Math.Abs(min2 - PHI_c_min2) * 60.0;


                    Xs1 = Ls1 * (1 - Theta_s_rad1 * Theta_s_rad1 / 10 + Theta_s_rad1 * Theta_s_rad1 * Theta_s_rad1 * Theta_s_rad1 / 216);
                    Ys1 = Ls1 * (Theta_s_rad1 / 3.0);

                    Xs2 = Ls2 * (1 - Theta_s_rad2 * Theta_s_rad2 / 10 + Theta_s_rad2 * Theta_s_rad2 * Theta_s_rad2 * Theta_s_rad2 / 216);
                    Ys2 = Ls2 * (Theta_s_rad2 / 3.0);



                    Brg_deg1 = (int)Brg1; min1 = Math.Abs(B1 - Brg_deg1) * 60.0; Brg_min1 = (int)min1; Brg_sec1 = Math.Abs(min1 - Brg_min1) * 60.0;
                    Brg_deg2 = (int)Brg2; min2 = Math.Abs(B2 - Brg_deg2) * 60.0; Brg_min2 = (int)min2; Brg_sec2 = Math.Abs(min2 - Brg_min2) * 60.0;


                    p1 = Ls1 * Ls1 / (24 * Rc);
                    k1 = Xs1 - Rc * Math.Sin(Theta_s1 / r9);

                    p2 = Ls2 * Ls2 / (24 * Rc);
                    k2 = Xs2 - Rc * Math.Sin(Theta_s2 / r9);



                    Ts1 = (Ls1 / 2.0) + (Rc + p1) * Math.Tan(Delta / 2.0 / r9);
                    Es1 = (Rc + p1) * (1 / Math.Cos(Delta / 2.0 / r9)) - Rc;

                    Ts2 = (Ls2 / 2.0) + (Rc + p2) * Math.Tan(Delta / 2.0 / r9);
                    Es2 = (Rc + p2) * (1 / Math.Cos(Delta / 2.0 / r9)) - Rc;



                    LC1 = Math.Sqrt(Xs1 * Xs1 + Ys1 * Ys1);
                    LT1 = Xs1 - Ys1 * (1 / Math.Tan(Theta_s1 / r9));
                    ST1 = Ys1 / Math.Sin(Theta_s1 / r9);

                    LC2 = Math.Sqrt(Xs2 * Xs2 + Ys2 * Ys2);
                    LT2 = Xs2 - Ys2 * (1 / Math.Tan(Theta_s2 / r9));
                    ST2 = Ys2 / Math.Sin(Theta_s2 / r9);



                    SE = V * V / (225 * Rc);


                    xt = IPX - xlate_x;
                    xt += 12.0 * textsize;

                    yt = IPY - xlate_y;
                    plan_form(ref xt, ref yt);

                    ltc_x = xt - 2 * textsize;
                    ltc_y = yt + 2 * textsize;

                    rbc_x = xt + 30 * textsize;
                    rbc_y = yt + 2 * textsize;

                    XL = IPX + 5 * textsize;
                    YL = IPY;

                    XR = XL + 30 * textsize;
                    YR = YL;

                    //write_line();

                    /**/
                    yt -= (2 * textsize);
                    textstring = "[ IP #: " + elcount.ToString() + " ]";
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    yt -= (2 * textsize);
                    textstring = "SPIRAL";
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    yt -= (2 * textsize);
                    textstring = string.Format("CIRCULAR: E={0:f3}, N={1:f3} m.\n", IPX, IPY);
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    yt -= (2 * textsize);
                    textstring = "SPIRAL";
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    yt -= (2 * textsize);
                    textstring = string.Format("HIP Chainage={0:f3} m.\n", HIPchainage);
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;
                    
                    yt -= (2 * textsize);
                    textstring = "";
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;
                    
                    yt -= (2 * textsize);
                    textstring = string.Format("A={0,2:D}° {1,2:D}' {2,4:f2}\",  Xs1={3,6:f3}  Xs2={4,6:f3}", Delta_deg, Delta_min, Delta_sec, Xs1, Xs2);
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    yt -= (2 * textsize);
                    textstring = string.Format("Bearings: B1={0,2:D}° {1,2:D}' {2,4:f2}\",  B2={3,2:D}° {4,2:D}' {5,4:f2}\""
                        , Brg_deg1, Brg_min1, Brg_sec1, Brg_deg2, Brg_min2, Brg_sec2);
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    yt -= (2 * textsize);
                    textstring = string.Format("Rc={0:f3},  Ys1={1:f3}  Ys1={2:f3}", Rc, Ys1, Ys2);
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    //
                    yt -= (2 * textsize);
                    textstring = string.Format("Ls1={0:f3}                 LT1={1:f3}", Ls1, LT1);
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    yt -= (2 * textsize);
                    textstring = string.Format("Ls2={0:f3}                 LT2={1:f3}", Ls2, LT2);
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    yt -= (2 * textsize);
                    textstring = string.Format("Dc={0,2:D}° {1,2:D}' {2,4:f2}\",  ST1={3:f3}  ST2={4:f3}", (int)Dc_deg, (int)Dc_min, Dc_sec, ST1, ST2);
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    yt -= (2 * textsize);
                    textstring = string.Format("Os1={0,2:D}° {1,2:D}' {2,4:f2}\" ", Theta_s_deg1, Theta_s_min1, Theta_s_sec1);
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    yt -= (2 * textsize);
                    textstring = string.Format("Os2={0,2:D}° {1,2:D}' {2,4:f2}\"            Lc={3,6:f3}", Theta_s_deg2, Theta_s_min2, Theta_s_sec2, Lc);
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    yt -= (2 * textsize);
                    textstring = string.Format("Øc1={0,2:D}° {1,2:D}' {2,4:f2}\"          p1={3:f3}", PHI_c_deg1, PHI_c_min1, PHI_c_sec1, p1);
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    yt -= (2 * textsize);
                    textstring = string.Format("Øc2={0,2:D}° {1,2:D}' {2,4:f2}\"          p2={3:f3}", PHI_c_deg2, PHI_c_min2, PHI_c_sec2, p2);
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    /**/
                    yt -= (2 * textsize);
                    textstring = string.Format("Ac={0,2:D}° {1,2:D}' {2,4:f2}\"          k1={3:f3}  k2={4:f3}", Delta_c_deg, Delta_c_min, Delta_c_sec, k1, k2);
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;


                    yt -= (2 * textsize);
                    textstring = string.Format("Ts1={0:f3}  Ts2={1:f3}", Ts1, Ts2);
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    yt -= (2 * textsize);
                    textstring = string.Format("Es1={0:f3}  Es2={1:f3}", Es1, Es2);
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;


                    yt -= (2 * textsize);
                    textstring = string.Format("For V={0:f3}, SE={1:f3}", V, SE);
                    xstor = xt;
                    ystor = yt;
                    textwrite();
                    xt = xstor;
                    yt = ystor;

                    //yt -= (2*textsize);
                    //sprintf(textstring,"LC1=%.3f  LC2=%.3f", LC1, LC2);
                    xstor = xt;
                    ystor = yt;
                    //textwrite();
                    xt = xstor;
                    yt = ystor;


                    rbc_x = xt + 30 * textsize;
                    rbc_y = yt - 1 * textsize;

                    //write_box();
                }


                last_IPX = IPX;
                last_IPY = IPY;

            }

        }
        #endregion        
    }
}
