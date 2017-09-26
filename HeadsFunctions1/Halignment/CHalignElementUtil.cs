using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HeadsUtils;

namespace HeadsFunctions1.Halignment
{
    internal class CHalignElementUtil
    {
        //FILE* fp1;
        StreamWriter fp2;
        BinaryWriter fp3;

        //short AcceptCode;
        short eltype, ptsc; 
        short i, elno, turn, pts, code, hips, ip, last_turn;//, array_type;
        double xs, ys, b1;
        public double xe, ye, b2;
        double chn1, chn2, l, hipx, hipy, del; 
        double[] x = new double[500];
        double[] y = new double[500];
        double inc, r9, t1, t2, temp;
        double x1, y1, x2, y2, x3, y3, rad, ls1, ls2;
        double xc2, yc2;//, lc, xc1, yc1, lt1, lt2, B1, B2, last_rad, last_ls1, last_ls2;

        string modnam, stglbl; // Model, String
        string path, pathfile;

        public CHalignElementUtil()
        {

        }
        
        public void Funcmain(string argv1, short Ecode, short Acode
            , string cSmod, string cSstr, short nElno, short Combo
            , double C, double I, double pX, double pY, double B
            , double L, double R)
        {
            i = elno = eltype = turn = pts = code = hips = ip = last_turn = ptsc = 0;//array_type = 0;
            chn1 = chn2 = hipx = hipy = l = xs = ys = xe = ye = b1 = b2 = del = 0.0;
            inc = r9 = t1 = t2 = temp = 0.0;
            x1 = y1 = x2 = y2 = x3 = y3 = rad = ls1 = ls2 = xc2 = yc2 = 0.0;//lc = xc1 = yc1 =  lt1 = lt2 = B1 = B2 = last_rad  = last_ls1 = last_ls2 = 0.0;

            path = argv1;
            read_default();


            pathfile = Path.Combine(path, "halign.tmp");
            fp2 = new StreamWriter(pathfile, true);

            pathfile = Path.Combine(path, "hal2.tmp");
            fp3 = new BinaryWriter(new FileStream(pathfile, FileMode.Create), Encoding.Default);
            

            code = Ecode;
            switch (Ecode)
            {
                case 1:                    
                    break;
                case 2:
                {
                    modnam = cSmod;
                    stglbl = cSstr;
                    chn1 = C; inc = I; elno = nElno; eltype = Combo;
                    xs = pX; ys = pY; b1 = B; l = L; rad = R;
                    if (rad < 0)
                    { 
                        rad = 0.0 - rad; 
                        turn = 1; 
                    }
                    else
                        turn = 2;

                    ptsc = 0;
                    pts = 0;

                    switch (eltype)
                    {
                        case 1: 
                            straight(); // all fns calculate x[], y[]
                            break;
                        case 2: 
                            lead_tran();
                            break;
                        case 3: 
                            circular();
                            break;
                        case 4: 
                            trail_tran();
                            if (code == 1)
                            { 
                                xe = xc2; 
                                ye = yc2; 
                            }
                            break;
                    }
                    string strTemp = modnam + '\t' + stglbl + '\t' + elno.ToString() 
                        + '\t' + eltype.ToString() + '\t' + chn1.ToString() + '\t' + chn2.ToString() 
                        + '\t' + hipx.ToString() + '\t' + hipy.ToString() + '\t' + t1.ToString() 
                        + '\t' + t2.ToString() + '\t' + rad.ToString() + '\t' + l.ToString() 
                        + '\t' + xs.ToString() + '\t' + ys.ToString() + '\t' + xe.ToString() 
                        + '\t' + ye.ToString() + '\t' + b1.ToString() + '\t' + b2.ToString() 
                        + '\t' + del.ToString() + '\t' + turn.ToString();
                    fp2.WriteLine(strTemp);
                    //fprintf(fp2, "%8s %4s %4d %4d %10.5f %10.5f %10.5f %10.5f %10.5f %10.5f %10.5f %10.5f %10.5f %10.5f %10.5f %10.5f %10.5f %10.5f %10.5f %4d\n", modnam, stglbl, elno, eltype, chn1, chn2, hipx, hipy, t1, t2, rad, l, xs, ys, xe, ye, b1, b2, del, turn);
                    write_drg();
                    write_end_point();
                    break;
                }
                default:
                    break;
            }

            fp2.Close();
            fp3.Close();            
        }


        private void read_default()
        {
            r9 = 57.29577951;
            inc = 10.0;
            hipx = 0;
            hipy = 0;
            rad = 0;
            l = 0;
            del = 0;
            turn = 1;
        }

        private void verify_bearing(ref double b)
        {
            if (b < 0) b = b + 360;
            else if (b > 360) b = b - 360;
        }

        private void straight()
        {

            eltype = 1;

            verify_bearing(ref b1);

            chn2 = chn1 + l;
            xe = xs + l * Math.Sin(b1 / r9);
            ye = ys + l * Math.Cos(b1 / r9);
            b2 = b1;

            ptsc = 0;

            x[ptsc] = xs;
            y[ptsc] = ys;
            ptsc++;
            pts++;

            x[ptsc] = xe;
            y[ptsc] = ye;
            ptsc++;
            pts++;
        }

        private void lead_tran()
        {
            double F = 0.0; 
            double ab = 0.0; 
            double ths = 0.0; 
            double th = 0.0; 
            double th1 = 0.0; 
            double th2 = 0.0; 
            double l_inc = 0.0; 
            double last_l_inc = 0.0;

            eltype = 2;

            verify_bearing(ref b1);

            F = (l * r9) / (2.0 * rad);
            if (turn == 1) F = -F;

            b2 = b1 + F;
            verify_bearing(ref b2);

            ths = b2 - b1;
            verify_bearing(ref ths);

            chn2 = chn1 + l;

            if (turn == 1) ab = -90;
            else if (turn == 2) ab = 90;

            l_inc = 0;
            last_l_inc = -999;
            ptsc = 0;

            while (true)
            {
                th = (l_inc * l_inc) / (2.0 * l * rad);


                th1 = (1 - th * th / 10 + th * th * th * th / 216 - th * th * th * th * th * th / 9360 + th * th * th * th * th * th * th * th / 685440) * l_inc;
                th2 = (th / 3 - th * th * th / 42 + th * th * th * th * th / 1320 - th * th * th * th * th * th * th / 75600 + th * th * th * th * th * th * th * th * th / 66894720) * l_inc;

                x[ptsc] = xs + th1 * Math.Sin(b1 / r9) + th2 * Math.Sin((b1 + ab) / r9);
                y[ptsc] = ys + th1 * Math.Cos(b1 / r9) + th2 * Math.Cos((b1 + ab) / r9);
                ptsc++;
                pts++;

                l_inc += inc;

                if (l_inc > l)
                {
                    if (last_l_inc == l) 
                        break;
                    else 
                        l_inc = l;
                }

                last_l_inc = l_inc;

            }// while 

            xe = x[ptsc - 1];
            ye = y[ptsc - 1];

        }

        private void circular()
        {
            double F = 0.0;
            double ab = 0.0;
            double th = 0.0;
            double th1 = 0.0;
            double l_inc = 0.0;
            double last_l_inc = 0.0;

            eltype = 3;

            verify_bearing(ref b1);

            F = (l * r9) / rad;
            if (turn == 1) F = -F;

            b2 = b1 + F;
            verify_bearing(ref b2);

            chn2 = chn1 + l;

            if (turn == 1) ab = -90;
            else if (turn == 2) ab = 90;

            l_inc = 0;
            last_l_inc = -999;
            ptsc = 0;

            while (true)
            {
                th = (l_inc * r9) / rad;
                if (turn == 1) th = 0 - th;

                th1 = b1 + ab + th;

                x[ptsc] = xs + rad * (Math.Sin((b1 + ab) / r9) - Math.Sin(th1 / r9));
                y[ptsc] = ys + rad * (Math.Cos((b1 + ab) / r9) - Math.Cos(th1 / r9));
                ptsc++;
                pts++;

                l_inc += inc;

                if (l_inc > l)
                {
                    if (last_l_inc == l) break;
                    else l_inc = l;
                }

                last_l_inc = l_inc;

            }// while 

            xe = x[ptsc - 1];
            ye = y[ptsc - 1];

        }

        private void trail_tran()
        {
            short store_ptsc = 0;

            double F = 0.0;
            double ab = 0.0;
            double ths = 0.0;
            double th = 0.0;
            double th1 = 0.0;
            double th2 = 0.0;
            double l_inc = 0.0;
            double last_l_inc = 0.0;
            double tmp = 0.0;

            eltype = 4;

            verify_bearing(ref b1);

            F = (l * r9) / (2.0 * rad);
            if (turn == 1) F = -F;
            b2 = b1 + F;
            verify_bearing(ref b2);

            temp = b2;
            b2 = b1;
            b1 = temp;

            b1 += 180;
            verify_bearing(ref b1);

            b2 += 180;
            verify_bearing(ref b2);

            if (turn == 1) turn++;
            else if (turn == 2) turn--;

            ths = b2 - b1;

            verify_bearing(ref ths);

            chn2 = chn1 + l;

            if (turn == 1)
                ab = -90;
            else
                ab = 90;

            F = l / (2 * rad);

            th1 = (1 - F * F / 10 + F * F * F * F / 216 - F * F * F * F * F * F / 9360 + F * F * F * F * F * F * F * F / 685440) * l;
            th2 = (F / 3 - F * F * F / 42 + F * F * F * F * F / 1320 - F * F * F * F * F * F * F / 75600 + F * F * F * F * F * F * F * F * F / 66894720) * l;

            xe = xs - th1 * Math.Sin(b1 / r9) - th2 * Math.Sin((b1 + ab) / r9);
            ye = ys - th1 * Math.Cos(b1 / r9) - th2 * Math.Cos((b1 + ab) / r9);



            l_inc = 0;
            last_l_inc = -999;
            ptsc = (short)((l / inc) + 1);
            store_ptsc = ptsc;

            // NEW 
            if (turn == 1) turn = 2;
            else if (turn == 2) turn = 1;

            while (ptsc > 0)
            {
                th = (l_inc * l_inc) / (2.0 * l * rad);


                th1 = (1 - th * th / 10 + th * th * th * th / 216 - th * th * th * th * th * th / 9360 + th * th * th * th * th * th * th * th / 685440) * l_inc;
                th2 = (th / 3 - th * th * th / 42 + th * th * th * th * th / 1320 - th * th * th * th * th * th * th / 75600 + th * th * th * th * th * th * th * th * th / 66894720) * l_inc;

                tmp = x[ptsc - 1] = xe + th1 * Math.Sin(b1 / r9) + th2 * Math.Sin((b1 + ab) / r9);
                y[ptsc - 1] = ye + th1 * Math.Cos(b1 / r9) + th2 * Math.Cos((b1 + ab) / r9);
                ptsc--;
                pts++;

                l_inc += inc;

                if (l_inc > l)
                {
                    if (last_l_inc == l) break;
                    else l_inc = l;
                }

                last_l_inc = l_inc;

            }// while 

            temp = b2;
            b2 = b1;
            b1 = temp;

            b1 -= 180;
            verify_bearing(ref b1);
            b2 -= 180;
            verify_bearing(ref b2);

            x[0] = xs;
            y[0] = ys;
            ptsc = store_ptsc;

        }

        private void write_drg()
        {
            short i = 0;
            short eld_code = 0;
            Linetype line = new Linetype();

            for (i = 1; i < ptsc; i++)
            {
                if (i == 1) line.elatt = 0;
                else if (i > 1) line.elatt = 1;

                line.scatt = 1;
                line.layer = "$$$$_4";
                line.laatt = 1;
                line.x1 = x[i - 1];
                line.y1 = y[i - 1];
                line.z1 = 0.0;
                line.x2 = x[i];
                line.y2 = y[i];
                line.z2 = 0.0;
                line.color = 4;
                line.style = 1;
                line.width = 1;
                line.label = modnam + ":" + stglbl;

                eld_code = 1;
                fp3.Write(eld_code);

                line.ToStream(fp3);                               
            }
        }

        private void write_end_point()
        {
            short ELD_code = 0;
            string ENDPT_tcode = "";
            double ENDPT_tdval = 0;

            ELD_code = 9;
            ENDPT_tcode = "Last X:";
            ENDPT_tdval = xe;
            fp3.Write(ELD_code);
            fp3.Write(ENDPT_tcode);
            fp3.Write(ENDPT_tdval);           


            ELD_code = 9;
            ENDPT_tcode = "Last Y:";
            ENDPT_tdval = ye;
            fp3.Write(ELD_code);
            fp3.Write(ENDPT_tcode);
            fp3.Write(ENDPT_tdval);

            ELD_code = 9;
            ENDPT_tcode = "Last W.C.Bearing:";
            ENDPT_tdval = b2;
            fp3.Write(ELD_code);
            fp3.Write(ENDPT_tcode);
            fp3.Write(ENDPT_tdval);

            ELD_code = 9;
            ENDPT_tcode = "Last Chain:";
            ENDPT_tdval = chn2;
            fp3.Write(ELD_code);
            fp3.Write(ENDPT_tcode);
            fp3.Write(ENDPT_tdval);
        }

        public static void SaveHalign(string strWorkingDirPath)
        {
            CHalignHipUtil.SaveHalign(strWorkingDirPath);
        }
    }
}
