using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using HeadsUtils;
using System.IO;
using HeadsFunctions1.Halignment;

namespace HeadsFunctions1.Halignment.Modelling
{
    internal class HalMod1type
    {
        public HalMod1type()
        {
        }
	    public string	ModelName;//[30];
        public string	StringName;//[20];
        public double	inc;
        public int	TotalSpChn;

        public void ToStream(System.IO.BinaryWriter bw)
        {
            bw.Write(ViewerUtils.ConvertStringToByteArray(this.ModelName, 30));
            bw.Write(ViewerUtils.ConvertStringToByteArray(this.StringName, 20));
            bw.Write(this.inc);
            bw.Write(this.TotalSpChn);
        }

        public static HalMod1type FromStream(System.IO.BinaryReader br)
        {
            HalMod1type obj = new HalMod1type();
            obj.ModelName = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(30));
            obj.StringName = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(20));
            obj.inc = br.ReadDouble();
            obj.TotalSpChn = br.ReadInt32();
            return obj;
        }
    };

    internal class CHalignModelUtil
    {
        #region DATA MEMBERS
        StreamReader fp1;
        StreamWriter fp2;
        StreamReader fp3;
        //FILE* fp4;
        //FILE* fp5;
        int tsp;
        short i, elno, eltype, turn, dg, mn, last_eltype;//, ok;
        short spchn_flag, ival;
        //short IP_flag, store_flag, SS, run, len, IRC, findFlag, last_turn, ind1, chk;

        double stchn, endchn, IPX, IPY, T1, T2, rad, L, xst, yst, xed, yed;
        double B1, B2, DEL, r9;//store_RC, totpts, sx, sy,, MC, RC, last_RC, last_MC, store_MC;
        double  inc, sc, radc, xc, yc;
        double last_stchn, last_endchn, last_chn;
        double last_IPX, last_IPY;//, schn, last_schn, LRC, store_ll, last_radc, num, brgi,  RB, SC, ;

        string modnam, stglbl, PTS, str1;//, date, hour;
        //string day, mon, year, uline, cal, last_modnam;//last_stglbl, radcurve, tt, 
        string usermod, userstg;
        string str2;//userreq, strmod, str2, msgstr;

        long al_index;//, rep_flag;
        double st_chn;

        List<double> splchn = new List<double>();
        //double[] splchn = new double[100];
        int cnt, spchn_ind;//opnum, 
        double lastchn, chn1, chn2, X2, Y2, lt1, lt2, RA, xend, yend, spchn;
        double L1, Lc, L2, del, last_RA, chn, ex, ey, ez, brg;//X1, Y1, X3, Y3, 
        string modnam1, stglbl1;

        //long ibrg;

        string path, pathfile;
        //string filstr, drive, dir, file, ext;

        public delegate void ShowDataEvent(string strText);
        public event ShowDataEvent OnShowModelInfo = null;
        #endregion

        public CHalignModelUtil()
        {

        }

        public void Funcmain(string argv1)
        {
            HalMod1type halmod1;
            //HalMod2type halmod2;
            tsp = 0;
            //MC = 0.0;

            //last_radc = 0.0;
            path = argv1;

            pathfile = Path.Combine(path, "HALMOD1.TMP");

            BinaryReader br = new BinaryReader(new FileStream(pathfile, FileMode.Open, FileAccess.Read), Encoding.Default);
            halmod1 = HalMod1type.FromStream(br);            

            usermod = halmod1.ModelName;
            userstg = halmod1.StringName;

            inc = halmod1.inc;
            r9 = 57.29577951;

            if (halmod1.TotalSpChn > 0)
            {
                if (halmod1.TotalSpChn > 100)
                {
                    halmod1.TotalSpChn = 100;
                }

                spchn_flag = 1;

                tsp = halmod1.TotalSpChn;

                for (i = 0; i < halmod1.TotalSpChn; i++)
                {
                    double dSpChn  = br.ReadDouble();
                    splchn.Add(dSpChn);
                }
            }


            br.Close();

            //overwrite_modelfile();

            //if (ok != 0)
            //    overwrite_halignfile();

            process_haligmod();


            System.Windows.Forms.MessageBox.Show("Design Report is written in file HALIGNMENT.REP", HeadsUtils.Constants.ProductName);

            //string[] tmpfilesarr = Directory.GetFiles(path, "*.TMP");
            //foreach (string strTmpFile in tmpfilesarr)
            //{
            //    ViewerUtils.DeleteFileIfExists(strTmpFile);
            //}
            
        }

        #region PRIVATE MEMBERS
        void overwrite_modelfile()
        {
            pathfile = Path.Combine(path, "MODEL.FIL");
            //ok = 0;
            if (File.Exists(pathfile))
            {
                List<CLabtype> listfil = new List<CLabtype>();
                ViewerUtils.ReadModelFilFile(pathfile, ref listfil);


                //Overwrite the existing model and string
                for (int i = 0; i < listfil.Count; i++)
                {
                    //DictionaryEntry ent = listfil[i];
                    CLabtype ent = listfil[i];
                    if (ent.attr == CLabtype.Type.Model)
                    {
                        CModType mod = (CModType)ent.Tag;
                        str1 = mod.Name;
                    }
                    else if (ent.attr == CLabtype.Type.String)
                    {
                        CStgType stg = (CStgType)ent.Tag;
                        str2 = stg.label;

                        if (str1 == modnam && str2 == stglbl)
                        {
                            CModType mod = new CModType();
                            mod.Name = "999";
                            CLabtype entPrev = listfil[i - 1];
                            entPrev.Tag = mod;

                            //CStgType stg = new CStgType();
                            stg.label = "999";
                            ent.Tag = stg;

                            //ok = 1;
                        }
                    }
                }

                ViewerUtils.WriteModelFilFile(pathfile, listfil);
            }
        }

        void overwrite_halignfile()
        {
            pathfile = Path.Combine(path, "HALIGN.FIL"); 
            if (File.Exists(pathfile))
            {
                StreamReader sr = new StreamReader(pathfile);
                List<CHalignFilData> listdata = new List<CHalignFilData>();
                while(sr.EndOfStream == false)
                {
                    string line = sr.ReadLine();
                    CHalignFilData fildata = CHalignFilData.Parse(line);
                    if(fildata.sMod == modnam && fildata.sString == stglbl)
                    {
                        fildata.sMod = "999";
                        fildata.sString  = "999";
                    }
                    listdata.Add(fildata);
                }
                sr.Close();

                StreamWriter sw = new StreamWriter(pathfile, false);
                foreach(CHalignFilData data in listdata)
                {
                    sw.WriteLine(data.ToString());
                }
                sw.Close();
            }
        }

        void process_haligmod()
        {
            fp1 = new StreamReader(Path.Combine(path, "HALIGN.FIL"));            
            fp2 = new StreamWriter(Path.Combine(path, "MODEL.TMP"));
            
            spchn_ind = 0;
            if(File.Exists(Path.Combine(path, "SPCHN.TMP")) == true)
            {
                fp3 = new StreamReader(Path.Combine(path, "SPCHN.TMP"));
                spchn_ind = 1;
            }
            
            lastchn = -999.999;

            while (fp1.EndOfStream == false)
            {
                CHalignFilData fildata = CHalignFilData.Parse(fp1.ReadLine());
                modnam1 = fildata.sMod;
                stglbl1 = fildata.sString;
                elno = (short)fildata.iSlno;
                eltype = fildata.iEltype;
                chn1 = fildata.dStartchn;
                chn2 = fildata.dEndchn;
                X2 = fildata.dHipx;
                Y2 = fildata.dHipy;
                lt1 = fildata.dT1;
                lt2 = fildata.dT2;
                RA = fildata.dRadius;
                L = fildata.dEllength;
                xst = fildata.dStartx;
                yst = fildata.dStarty;
                xend = fildata.dEndx;
                yend = fildata.dEndy;
                B1 = fildata.dB1;
                B2 = fildata.dB2;
                del = fildata.dDeviation;
                turn = (short)fildata.iTurn;

                //fscanf(fp1, "%s %s %i %i %lf %lf %lf %lf %lf %lf %lf %lf %lf %lf %lf %lf %lf %lf %lf %i\n", modnam1, stglbl1, &elno, &eltype, &chn1, &chn2, &X2, &Y2, &lt1, &lt2, &RA, &L, &xst, &yst, &xend, &yend, &B1, &B2, &del, &turn);

                cnt++;
                //if (chn2 > 2637.0)
                //    chk = 1;

                //int ll = modnam1.Length;
                //modnam1[ll] = '\0';


                if (modnam1 != usermod || stglbl1 != userstg)
                    continue;

                //if(L<=0.0)
                //continue;

                switch (eltype)
                {
                    case 1: 
                        modelStraight();
                        break;
                    case 2: 
                        modelLeadTransition();
                        break;
                    case 3: 
                        modelCircular();
                        break;
                    case 4: 
                        modelTrailTransition();
                        break;
                    default:
                        break;
                }
            }

            fp1.Close();
            fp2.Close();

            if (spchn_ind == 1)
                fp3.Close();

            writeModel();
            modelStraights();

            FuncMainHalignRep();
        }

        void modelStraight()
        {
	        long ival;
	        double chn, sx, sy, ex, ey, ez, l1, storechn, storel1;
	        string spmod, spstg;
            string strTmp;

            sx = xst;
            sy = yst;
            chn = chn1;
            storechn = chn;
            storel1 = 0.0;
            eltype = 1;
            ez = -999.999;
            l1 = 0.0;
            spchn_flag = 0;

            while(true)
            {
                if(chn == stchn)
                {
                    ex = sx;
                    ey = sy;
                    //goto pt1;
                }
                else
                {
                    if (spchn_ind == 1)
                    {
                        fp3.Close();
                        fp3 = new StreamReader(Path.Combine(path, "SPCHN.TMP"));
                        while (fp3.EndOfStream == false)
                        {
                            chn = storechn;
                            l1 = storel1;
                            spchn_flag = 0;

                            strTmp = fp3.ReadLine();
                            string[] arrtmp = strTmp.Split(new char[] { '\t' });
                            spmod = arrtmp[0];
                            spstg = arrtmp[1];
                            spchn = double.Parse(arrtmp[2]);


                            if (lastchn < spchn && spchn < chn && modnam == spmod && stglbl == spstg)
                            {
                                chn = spchn;
                                l1 = spchn - stchn;
                                spchn_flag = 1;
                                break;
                            }
                        }
                    }
                    
                    ex = sx + l1 * Math.Sin(B1 / r9);  /*  E  */
                    ey = sy + l1 * Math.Cos(B1 / r9);  /*  N  */
                }

                strTmp = modnam1 + "\t" + stglbl1 + "\t" 
                    + eltype.ToString() + "\t" 
                    + chn.ToString() + "\t" 
                    + ex.ToString() + "\t" 
                    + ey.ToString() + "\t" 
                    + ez.ToString() + "\t" 
                    + RA.ToString() + "\t"
                    + B1.ToString() + "\t" 
                    + turn.ToString() + "\t" 
                    + spchn_flag.ToString();

                fp2.WriteLine(strTmp);
                

		        if(chn == chn2)
			        break;

		        lastchn=chn;

		        if(spchn_flag==1)
			        continue;

		        chn = chn + inc;
		        ival = (long)(chn/inc);
		        chn = ival*inc;
		        l1 = chn-chn1;
                //if (chn == 3000.0)
                //{
                //    int ok = 1;
                //}
		        if(chn > chn2)
		        {
			        l1 = L;
			        chn = chn2;
		        }

                storechn=chn;
		        storel1=l1;
            }
        }

        void modelLeadTransition()
        {
            long ival;
	        double chn, sx, sy;
	        double brg, rad, storechn, storel1 = 0;
	        double b1 = 0, q1, p1, p2, l1, ex, ey, ez;//, th, th1, BB2
	        string spmod, spstg;
            string strTmp;

	        L1=L;
	        sx = xst;
	        sy = yst;
	        chn = chn1;
	        storechn=chn;
        	//storel1=l1;
	        eltype = 2;
	        brg=B1;
	        q1=0.0;
	        spchn_flag=0;

	        if(turn==1)
		        b1=-90.0;
	        else if(turn==2)
		        b1=90.0;

	        l1=0.0;


	        while(true)
	        {
		        if(l1 == 0.0)
		        {
			        ex = sx;
			        ey = sy;
			        ez = -999.999;
			        //goto pt2;
		        }
                else
                {
                    if(spchn_ind == 1)
		            {
                        fp3.Close();
                        fp3 = new StreamReader(Path.Combine(path, "SPCHN.TMP"));
                        while (fp3.EndOfStream == false)
			            {
                            chn = storechn;
                            l1 = storel1;
                            spchn_flag = 0;

                            strTmp = fp3.ReadLine();
                            string[] arrtmp = strTmp.Split(new char[] { '\t' });
                            spmod = arrtmp[0];
                            spstg = arrtmp[1];
                            spchn = double.Parse(arrtmp[2]);

                            if (lastchn < spchn && spchn < chn && modnam1 == spmod && stglbl1 == spstg)
				            {
                                chn = spchn;
                                l1 = spchn - chn1;
                                spchn_flag = 1;
                                break;
				            }
			            }
		            }

                    q1 = l1 * l1 / (2 * L1 * RA);

                    p1 = (1 - q1 * q1 / 10
                        + q1 * q1 * q1 * q1 / 216
                        - q1 * q1 * q1 * q1 * q1 * q1 / 9360
                        + q1 * q1 * q1 * q1 * q1 * q1 * q1 * q1 / 685440) * l1;

                    p2 = (q1 / 3 - q1 * q1 * q1 / 42
                            + q1 * q1 * q1 * q1 * q1 / 1320
                            - q1 * q1 * q1 * q1 * q1 * q1 * q1 / 75600
                            + q1 * q1 * q1 * q1 * q1 * q1 * q1 * q1 * q1 / 6894720) * l1;

                    ex = sx + p1 * Math.Sin(B1 / r9) + p2 * Math.Sin((B1 + b1) / r9);
                    ey = sy + p1 * Math.Cos(B1 / r9) + p2 * Math.Cos((B1 + b1) / r9);
                    ez = -999.999;
                }
		        //pt2:

		        if(turn == 1)
      			        brg = B1 - q1*r9;
   		        else if(turn == 2)
      			        brg = B1 + q1*r9;

		        if(brg<0.0)
			        brg=brg+360.0;
   		        else if(brg>360.0)
			        brg=brg-360.0;

		        if(l1 == 0.0)
			        rad = RA*L1;
		        else
			        rad = RA*L1/l1;

                strTmp = modnam1 + "\t" + stglbl1 + "\t"
                    + eltype.ToString() + "\t"
                    + chn.ToString() + "\t"
                    + ex.ToString() + "\t"
                    + ey.ToString() + "\t"
                    + ez.ToString() + "\t"
                    + rad.ToString() + "\t"
                    + brg.ToString() + "\t"
                    + turn.ToString() + "\t"
                    + spchn_flag.ToString();

                fp2.WriteLine(strTmp);

                //fprintf(fp2, "%s %s %4d %.5f %.5f %.5f %.5f %.5f %.5f %4d %d\n"
                //    , modnam1, stglbl1, eltype, chn, ex, ey, ez, rad, brg, turn, spchn_flag);

		        if(chn == chn2)
			        break;

        	        lastchn=chn;

		        if(spchn_flag==1)
			        continue;

		        chn = chn + inc;
		        ival = (long)(chn/inc);
		        chn = ival*inc;
		        l1 = chn-chn1;

		        if(chn > chn2)
		        {
			        l1 = L1;
			        chn = chn2;
		        }

		        storechn=chn;
		        storel1=l1;

	        }
        }

        void modelCircular()
        {
            long ival;
	        double chn, sx, sy, brg, storechn, storel1 = 0;//sz, q1, p1, p2, BB2, 
	        double b1 = 0, th, th1, l1, ex, ey, ez;
	        string spmod, spstg;
            string strTmp;

	        Lc = L;
	        sx = xst;
	        sy = yst;
	        chn = chn1;
	        storechn=chn;
	        brg=B1;
	        th=0.0;
        //	storel1=l1;
	        eltype=3;
	        spchn_flag=0;


	        if(turn==1)
		        b1=-90.0;
	        else if(turn==2)
		        b1=90.0;

	        l1 = 0.0;
                ez = -999.999;
            //if(RA==1500.0)
            //    chk=1;

	        while(true)
	        {
                if(l1 == 0.0)
		        {
			        ex = sx;
			        ey = sy;
			        //goto pt3;
		        }
                else
                {
                    if (spchn_ind == 1)
                    {
                        fp3.Close();
                        fp3 = new StreamReader(Path.Combine(path, "SPCHN.TMP"));
                        while (fp3.EndOfStream == false)
                        {
                            chn = storechn;
                            l1 = storel1;
                            spchn_flag = 0;

                            strTmp = fp3.ReadLine();
                            string[] arrtmp = strTmp.Split(new char[] { '\t' });
                            spmod = arrtmp[0];
                            spstg = arrtmp[1];
                            spchn = double.Parse(arrtmp[2]);
                            if (lastchn < spchn && spchn < chn && modnam1 == spmod && stglbl1 == spstg)
                            {
                                chn = spchn;
                                l1 = spchn - chn1;
                                spchn_flag = 1;
                                break;
                            }
                        }
                    }
                    th = l1 * r9 / RA;
                    if (turn == 1) th = 0.0 - th;

                    th1 = B1 + b1 + th;

                    ex = sx + RA * Math.Sin((B1 + b1) / r9) - RA * Math.Sin(th1 / r9);
                    ey = sy + RA * Math.Cos((B1 + b1) / r9) - RA * Math.Cos(th1 / r9);
                    ez = -999.999;
                }		        

		        brg = B1 + th;

                if (brg < 0.0)
                    brg = brg + 360.0;
                else if (brg > 360.0)
                    brg = brg - 360.0;

                strTmp = modnam1 + "\t" + stglbl1 + "\t"
                    + eltype.ToString() + "\t"
                    + chn.ToString() + "\t"
                    + ex.ToString() + "\t"
                    + ey.ToString() + "\t"
                    + ez.ToString() + "\t"
                    + RA.ToString() + "\t"
                    + brg.ToString() + "\t"
                    + turn.ToString() + "\t"
                    + spchn_flag.ToString();

                fp2.WriteLine(strTmp);
                //fprintf(fp2, "%s %s %4d %.5f %.5f %.5f %.5f %.5f %.5f %4d %d\n"
                //    , modnam1, stglbl1, eltype, chn, ex, ey, ez, RA, brg, turn, spchn_flag);

		        if(chn == chn2)
			        break;

                        lastchn=chn;

		        if(spchn_flag==1)
			        continue;

		        chn = chn + inc;
		        ival = (long)(chn/inc);
		        chn = ival*inc;
		        l1 = chn-chn1;

		        if(chn > chn2)
		        {
			        l1 = Lc;
			        chn = chn2;
		        }

                storechn=chn;
		        storel1=l1;

	        }
        }

        void modelTrailTransition()
        {
	        long ival;
	        //long chk;
	        double chn, sx, sy, brg, rad, storechn, storel1;//sz, th, th1, 
	        double b1 = 0, q1, p1, p2, BB2 = 0, l1, ex, ey, ez;
	        string spmod, spstg;
            string strTmp;
	        L2 = L;
	        sx = xend;
	        sy = yend;
	        chn = chn1;
	        storechn=chn;
	        storel1=L2;
	        eltype = 4;
	        brg=B1;
	        q1 = L2*L2/(2*L2*RA);
	        spchn_flag=0;


	        if(turn==1)
		        b1=90.0;
	        else if(turn==2)
		        b1=-90.0;

	        l1=L2;

            //if(RA==280)
            //    chk=1;

		    while(true)
		    {
			    if(l1 == 0.0)
			    {
				    ex = sx;
				    ey = sy;
				    ez = -999.999;
				    //goto pt4;
			    }
                else
                {
                    if (spchn_ind == 1)
                    {
                        fp3.Close();
                        fp3 = new StreamReader(Path.Combine(path, "SPCHN.TMP"));
                        while (fp3.EndOfStream == false)
                        {
                            chn = storechn;
                            l1 = storel1;
                            spchn_flag = 0;

                            strTmp = fp3.ReadLine();
                            string[] arrtmp = strTmp.Split(new char[] { '\t' });
                            spmod = arrtmp[0];
                            spstg = arrtmp[1];
                            spchn = double.Parse(arrtmp[2]);

                            if (lastchn < spchn && spchn < chn && modnam1 == spmod && stglbl1 == spstg)
                            {
                                chn = spchn;
                                l1 = spchn - chn1;
                                spchn_flag = 1;
                                break;
                            }
                        }
                    }

                    q1 = l1 * l1 / (2 * L2 * RA);
                    p1 = (1 - q1 * q1 / 10
                        + q1 * q1 * q1 * q1 / 216
                        - q1 * q1 * q1 * q1 * q1 * q1 / 9360
                        + q1 * q1 * q1 * q1 * q1 * q1 * q1 * q1 / 685440) * l1;

                    p2 = (q1 / 3 - q1 * q1 * q1 / 42
                            + q1 * q1 * q1 * q1 * q1 / 1320
                            - q1 * q1 * q1 * q1 * q1 * q1 * q1 / 75600
                            + q1 * q1 * q1 * q1 * q1 * q1 * q1 * q1 * q1 / 6894720) * l1;

                    BB2 = B2 + 180;
                    if (BB2 > 360) BB2 = BB2 - 360;
                    else if (BB2 < 0) BB2 = BB2 + 360;

                    ex = sx + p1 * Math.Sin(BB2 / r9) + p2 * Math.Sin((BB2 + b1) / r9);
                    ey = sy + p1 * Math.Cos(BB2 / r9) + p2 * Math.Cos((BB2 + b1) / r9);
                    ez = -999.999;
                }


		        if(turn == 1)
      			        brg = BB2 + q1*r9;
   		        else if(turn == 2)
      			        brg = BB2 - q1*r9;

		        if(brg<0.0)
			        brg=brg+360.0;
   		        else if(brg>360.0)
			        brg=brg-360.0;

		        brg = brg-180.0;

		        if(l1> 0.0)
			        rad = RA*L2/l1;
		        else
			        rad = RA*L2;

                strTmp = modnam1 + "\t" + stglbl1 + "\t"
                    + eltype.ToString() + "\t"
                    + chn.ToString() + "\t"
                    + ex.ToString() + "\t"
                    + ey.ToString() + "\t"
                    + ez.ToString() + "\t"
                    + rad.ToString() + "\t"
                    + brg.ToString() + "\t"
                    + turn.ToString() + "\t"
                    + spchn_flag.ToString();

                fp2.WriteLine(strTmp);

                //fprintf(fp2, "%s %s %4d %.5f %.5f %.5f %.5f %.5f %.5f %4d %d\n"
                //    , modnam1, stglbl1, eltype, chn, ex, ey, ez, rad, brg, turn, spchn_flag);

                if (chn == chn2)
                    break;

                lastchn = chn;

                if (spchn_flag == 1)
                    continue;

                chn = chn + inc;
                ival = (long)(chn / inc);
                chn = ival * inc;
                l1 = L2 - (chn - chn1);

                if (chn > chn2)
                {
                    l1 = 0.0;
                    chn = chn2;
                }

                storechn = chn;
                storel1 = l1;
            }       /* while */
        }

        void writeModel()
        {
            short match = 0;//, chk=0;
            double chn, ex, ey, ez, brg, lastchn;
            string lastmod = string.Empty, laststg = string.Empty, smodnam = string.Empty;
            CLabtype lab;
            CPTStype pts;
            //CTXTtype txt;
            CModType mod;
            CStgType stg;
            ClSTtype lst;
            //long loc1 = 0;
            string strTemp;
            string[] arrtmp = null;
            int iPos = 0;
        
            pathfile = Path.Combine(path, "MODEL.TMP");

            if (File.Exists(pathfile) == false)
            {
                System.Windows.Forms.MessageBox.Show("File model.tmp not found...!", HeadsUtils.Constants.ProductName);
                return;
            }


            fp1 = new StreamReader(pathfile); 
            strTemp = fp1.ReadLine();
            arrtmp = strTemp.Split(new char[] { '\t' });
            iPos = 0;
            modnam = arrtmp[iPos]; iPos++;
            stglbl = arrtmp[iPos]; iPos++;
            eltype = short.Parse(arrtmp[iPos]); iPos++;
            chn = double.Parse(arrtmp[iPos]); iPos++;
            ex = double.Parse(arrtmp[iPos]); iPos++;
            ey = double.Parse(arrtmp[iPos]); iPos++;
            ez = double.Parse(arrtmp[iPos]); iPos++;
            RA = double.Parse(arrtmp[iPos]); iPos++;
            brg = double.Parse(arrtmp[iPos]); iPos++;
            turn = short.Parse(arrtmp[iPos]); iPos++;
            spchn_flag = short.Parse(arrtmp[iPos]);

            fp1.Close();

            pathfile = Path.Combine(path, "MODEL.LST");
            List<ClSTtype> listLst = new List<ClSTtype>();
            if (File.Exists(pathfile))
            {
                ViewerUtils.ReadModelLstFile(pathfile, ref listLst);
                match = 0;
                foreach (ClSTtype lstele in listLst)
                {
                    if (modnam.ToLower() == lstele.strMod1.ToLower() && stglbl.ToLower() == lstele.strStg.ToLower())
                    {
                        match = 1;
                        break;
                    }
                }

                if (match == 0)
                {
                    lst = new ClSTtype();
                    lst.strMod1 = modnam;
                    lst.strStg = stglbl;
                    listLst.Add(lst);
                }
            }
            else//else file not exists
            {
                lst = new ClSTtype();
                lst.strMod1 = modnam;
                lst.strStg = stglbl;
                listLst.Add(lst);
            }
            ViewerUtils.WriteModelLstFile(pathfile, listLst);

            if (match == 1)
            {
                //Data organize
                pathfile = Path.Combine(path, "MODEL.FIL");
                ///////////
                List<CLabtype> listlabs = new List<CLabtype>();
                ViewerUtils.ReadModelFilFile(pathfile, ref listlabs);
                mod = null;
                for (int icnt = 0; icnt < listlabs.Count; icnt++)
                {
                    lab = listlabs[icnt];
                    if (lab.attr == CLabtype.Type.Model)
                    {
                        mod = (CModType)lab.Tag;
                    }
                    else if (lab.attr == CLabtype.Type.String)
                    {
                        stg = (CStgType)lab.Tag;
                        if (mod != null)
                        {
                            if ((mod.name.ToLower() == modnam.ToLower() || mod.name.ToLower() == modnam.ToLower() + "$")
                                && (stg.label.ToLower() == stglbl.ToLower()))
                            {
                                int iDelStartIndex = icnt - 1;
                                listlabs.RemoveAt(iDelStartIndex);
                                while (listlabs.Count > iDelStartIndex
                                    && listlabs[iDelStartIndex].attr != CLabtype.Type.Model)
                                {
                                    listlabs.RemoveAt(iDelStartIndex);
                                }

                            }
                        }
                        
                    }
                }
                ViewerUtils.WriteModelFilFile(pathfile, listlabs);               
                
            } /* match=1 */

            pathfile = Path.Combine(path, "MODEL.FIL");
            List<CLabtype> listfil = new List<CLabtype>();
            ViewerUtils.ReadModelFilFile(pathfile, ref listfil);

            lab = new CLabtype();
            lab.attr = CLabtype.Type.Model;
            mod = new CModType();
            mod.Name = modnam;
            lab.Tag = mod;
            listfil.Add(lab);

            lab = new CLabtype();
            lab.attr = CLabtype.Type.String;
            stg = new CStgType();
            stg.label = stglbl;
            lab.Tag = stg;
            listfil.Add(lab);
            
            match = 0;
            lastchn = -999.999;

            
            fp1 = new StreamReader(Path.Combine(path, "MODEL.TMP"));
            while (fp1.EndOfStream == false)
            {
                //loc1 = fp1.BaseStream.Position;
                strTemp = fp1.ReadLine();
                arrtmp = strTemp.Split(new char[] { '\t' });
                iPos = 0;
                modnam = arrtmp[iPos]; iPos++;
                stglbl = arrtmp[iPos]; iPos++;
                eltype = short.Parse(arrtmp[iPos]); iPos++;
                chn = double.Parse(arrtmp[iPos]); iPos++;
                ex = double.Parse(arrtmp[iPos]); iPos++;
                ey = double.Parse(arrtmp[iPos]); iPos++;
                ez = double.Parse(arrtmp[iPos]); iPos++;
                RA = double.Parse(arrtmp[iPos]); iPos++;
                brg = double.Parse(arrtmp[iPos]); iPos++;
                turn = short.Parse(arrtmp[iPos]); iPos++;
                spchn_flag = short.Parse(arrtmp[iPos]);

                string tmpFileName = Path.Combine(path, "SCROLL.TMP");
                StreamWriter tmpFile = new StreamWriter(tmpFileName, true);
                string strTmp = modnam + '\t' + stglbl + '\t'
                    + chn.ToString() + '\t'
                    + ex.ToString() + '\t'
                    + ey.ToString() + '\t'
                    + ez.ToString() + '\t'
                    + RA.ToString() + '\t'
                    + brg.ToString();
                //fprintf(tmpFile, "%8s %s %.3f %.3f %.3f %.3f %.3f %.3f\n", modnam, stglbl, chn, ex, ey, ez, RA, brg);
                tmpFile.WriteLine(strTmp);
                tmpFile.Close();

                // Display scroll
                //CString buffer;
                //buffer.Format("%s %s %.3f  %.3f  %.3f  %.3f", modnam, stglbl, chn, ex, ey, ez);

                //int i = m_ModelCtrl.AddString(buffer);
                //m_ModelCtrl.SetCurSel(i);
                strTmp = string.Format("{0} {1} {2:f3}  {3:f3}  {4:f3}  {5:f3}", modnam, stglbl, chn, ex, ey, ez);
                this.DisplayScroll(strTmp);

                if(modnam.ToLower() != lastmod.ToLower() || stglbl.ToLower() != laststg.ToLower())
                {
                    match++;
                    if(match == 2)
                    {
                        break;
                    }
                }

		        if(chn == lastchn)
			        continue;

                lab = new CLabtype();
                lab.attr = CLabtype.Type.Point;
                pts = new CPTStype();
                pts.mc = chn;
                pts.mx = ex;
                pts.my = ey;
                pts.mz = ez;
                lab.Tag = pts;
                listfil.Add(lab);

                lastmod = modnam;
                laststg = stglbl;
                lastchn = chn;
                match = 1;

                
            }
            lab = new CLabtype();
            lab.attr = CLabtype.Type.EndCode;
            listfil.Add(lab);


            ViewerUtils.WriteModelFilFile(Path.Combine(path, "MODEL.FIL"), listfil);

            //if (match == 2)
            //{
            //    fp1.BaseStream.Position = loc1;
            //    goto pt1;
            //}
            fp1.Close();
            //////////////////////////////////////////////////////////////////////////
            
            //short match=0;//, chk=0;
            //double chn, ex, ey, ez, brg, lastchn;
            //string lastmod = string.Empty, laststg = string.Empty, smodnam = string.Empty;
            //List<CLabtype> listfil = new List<CLabtype>();
            //string strTemp;
            //string[] arrtmp = null;
            //int iPos = 0;
            //long loc1 = 0;

            //pt1:
            //pathfile = Path.Combine(path, "MODEL.TMP");

            //if (File.Exists(pathfile) == false)
            //{
            //    System.Windows.Forms.MessageBox.Show("File model.tmp not found...!");
            //    return;
            //}

            //fp1 = new StreamReader(pathfile);

            //strTemp = fp1.ReadLine();
            //arrtmp = strTemp.Split(new char[] { '\t' });
            //iPos = 0;
            //modnam = arrtmp[iPos]; iPos++;
            //stglbl = arrtmp[iPos]; iPos++;
            //eltype = short.Parse(arrtmp[iPos]); iPos++;
            //chn = double.Parse(arrtmp[iPos]); iPos++;
            //ex = double.Parse(arrtmp[iPos]); iPos++;
            //ey = double.Parse(arrtmp[iPos]); iPos++;
            //ez = double.Parse(arrtmp[iPos]); iPos++;
            //RA = double.Parse(arrtmp[iPos]); iPos++;
            //brg = double.Parse(arrtmp[iPos]); iPos++;
            //turn = short.Parse(arrtmp[iPos]); iPos++;
            //spchn_flag = short.Parse(arrtmp[iPos]);

            
            //fp1.BaseStream.Position = 0;


            //pathfile = Path.Combine(path, "MODEL.LST");
            //List<ClSTtype> listLst = new List<ClSTtype>();
            //if (File.Exists(pathfile))
            //{
            //    ViewerUtils.ReadModelLstFile(pathfile, ref listLst);
            //    match = 0;
            //    foreach (ClSTtype lstele in listLst)
            //    {
            //        if (modnam.ToLower() == lstele.strMod1.ToLower() && stglbl.ToLower() == lstele.strStg.ToLower())
            //        {
            //            match = 1;
            //            break;
            //        }
            //    }

            //    if (match == 0)
            //    {
            //        ClSTtype lstele = new ClSTtype();
            //        lstele.strMod1 = modnam;
            //        lstele.strStg = stglbl;
            //        listLst.Add(lstele);
            //    }
            //}
            //else
            //{
            //    ClSTtype lstele = new ClSTtype();
            //    lstele.strMod1 = modnam;
            //    lstele.strStg = stglbl;
            //    listLst.Add(lstele);
            //}

            //BinaryWriter bw = new BinaryWriter(new FileStream(pathfile, FileMode.Create), Encoding.Default);
            //foreach (ClSTtype lstdata in listLst)
            //{
            //    lstdata.ToStream(bw);
            //}
            //bw.Close();

            //if (match == 1)
            //{
            //    //Data organize
            //    pathfile = Path.Combine(path, "MODEL.FIL");
            //    listfil = new List<CLabtype>();
            //    ViewerUtils.ReadModelFilFile(pathfile, ref listfil);

            //    smodnam = modnam + "$";
            //    CModType mod = null;
            //    for (int iCnt = 0; iCnt < listfil.Count; iCnt++)
            //    {
            //        CLabtype labEle = listfil[iCnt];
            //        if (labEle.attr == 101)
            //        {
            //            mod = (CModType)labEle.Tag;
            //        }
            //        else if (labEle.attr == 102)
            //        {
            //            CStgType stgdata = (CStgType)labEle.Tag;
            //            if (modnam.ToLower() == mod.Name.ToLower() && stglbl.ToLower() == stgdata.label.ToLower())
            //            {
            //                stgdata.label = "-999";
            //                labEle.Tag = stgdata;
            //            }
            //        }
            //    }
            //    ViewerUtils.WriteModelFilFile(pathfile, listfil);
            //} /* match=1 */

            //pathfile = Path.Combine(path, "MODEL.FIL");
            //listfil = new List<CLabtype>();
            //ViewerUtils.ReadModelFilFile(pathfile, ref listfil);

            //CLabtype labtmp = new CLabtype();
            //labtmp.attr = 101;
            //CModType modtmp = new CModType();
            //modtmp.Name = modnam;
            //labtmp.Tag = modtmp;
            //listfil.Add(labtmp);

            //labtmp = new CLabtype();
            //labtmp.attr = 102;
            //CStgType stg = new CStgType();
            //stg.label = stglbl;
            //labtmp.Tag = stg;
            //listfil.Add(labtmp);


            ////FILE *tmpFile;

            //match = 0;
            //lastchn = -999.999;

            //while (fp1.EndOfStream == false)
            //{
            //    loc1 = fp1.BaseStream.Position;

            //    strTemp = fp1.ReadLine();
            //    arrtmp = strTemp.Split(new char[] { '\t' });
            //    iPos = 0;
            //    modnam = arrtmp[iPos]; iPos++;
            //    stglbl = arrtmp[iPos]; iPos++;
            //    eltype = short.Parse(arrtmp[iPos]); iPos++;
            //    chn = double.Parse(arrtmp[iPos]); iPos++;
            //    ex = double.Parse(arrtmp[iPos]); iPos++;
            //    ey = double.Parse(arrtmp[iPos]); iPos++;
            //    ez = double.Parse(arrtmp[iPos]); iPos++;
            //    RA = double.Parse(arrtmp[iPos]); iPos++;
            //    brg = double.Parse(arrtmp[iPos]); iPos++;
            //    turn = short.Parse(arrtmp[iPos]); iPos++;
            //    spchn_flag = short.Parse(arrtmp[iPos]);

            //    //fgetpos(fp1, &loc1);
            //    //fscanf(fp1, "%s %s %i %lf %lf %lf %lf %lf %lf %i %i", modnam, stglbl, &eltype, &chn, &ex, &ey, &ez, &RA, &brg, &turn, &spchn_flag);

            //    string tmpFileName = Path.Combine(path, "SCROLL.TMP");
            //    StreamWriter tmpFile = new StreamWriter(tmpFileName, true);
            //    string strTmp = modnam + '\t' + stglbl + '\t'
            //        + chn.ToString() + '\t'
            //        + ex.ToString() + '\t'
            //        + ey.ToString() + '\t'
            //        + ez.ToString() + '\t'
            //        + RA.ToString() + '\t'
            //        + brg.ToString();
            //    tmpFile.WriteLine(strTmp);
            //    tmpFile.Close();

            //    // Display scroll
            //    //CString buffer;
            //    //buffer.Format("%s %s %.3f  %.3f  %.3f  %.3f", modnam, stglbl, chn, ex, ey, ez);

            //    //int i = m_ModelCtrl.AddString(buffer);
            //    //m_ModelCtrl.SetCurSel(i);
            //    strTmp = string.Format("{0} {1} {2:f3}  {3:f3}  {4:f3}  {5:f3}", modnam, stglbl, chn, ex, ey, ez);
            //    this.DisplayScroll(strTmp);

            //    if (modnam.ToLower() == lastmod.ToLower() || stglbl.ToLower() == laststg.ToLower())
            //    {
            //        match++;
            //        if (match == 2)
            //        {
            //            break;
            //        }
            //    }

            //    if (chn == lastchn)
            //        continue;

            //    labtmp = new CLabtype();
            //    labtmp.attr = 103;
            //    CPTStype pts = new CPTStype();
            //    pts.mc = chn;
            //    pts.mx = ex;
            //    pts.my = ey;
            //    pts.mz = ez;
            //    labtmp.Tag = pts;
            //    listfil.Add(labtmp);

            //    lastmod = modnam;
            //    laststg = stglbl;
            //    lastchn = chn;
            //    match = 1;

            //    //if (chn > 3336.0)
            //    //    chk = 1;
            //}

            //labtmp = new CLabtype();
            //labtmp.attr = 999;
            //listfil.Add(labtmp);


            //ViewerUtils.WriteModelFilFile(Path.Combine(path, "MODEL.FIL"), listfil);

            //if (match == 2)
            //{
            //    fp1.BaseStream.Position = loc1;
            //    goto pt1;
            //}
            //fp1.Close();       
        }

        void modelStraights()
        {
            CLabtype lab = new CLabtype();
            CPTStype pts = new CPTStype();
            CTXTtype txt = new CTXTtype();
            CModType mod = new CModType();
            CStgType stg = new CStgType();
            ClSTtype lst = new ClSTtype();

            List<CLabtype> listfil = new List<CLabtype>();
            ViewerUtils.ReadModelFilFile(Path.Combine(path, "MODEL.FIL"), ref listfil);

            lab = new CLabtype();
            lab.attr = CLabtype.Type.Model;
            mod = new CModType();
            mod.name = modnam+"$";
            lab.Tag = mod;
            listfil.Add(lab);

            lab = new CLabtype();
            lab.attr = CLabtype.Type.String;
            stg = new CStgType();
            stg.label = stglbl;
            lab.Tag = stg;
            listfil.Add(lab);

            List<ClSTtype> listmodlst = new List<ClSTtype>();
            pathfile = Path.Combine(path, "MODEL.LST");
            if (File.Exists(pathfile))
            {
                int match = 0;
                ViewerUtils.ReadModelLstFile(pathfile, ref listmodlst);
                match = 0;

                for(int iC = 0; iC < listmodlst.Count; iC++)
                {
                    lst = listmodlst[iC];
                    if(mod.name.ToLower() == lst.strMod1.ToLower() && stg.label.ToLower() == lst.strStg.ToLower())
			        {
				        match=1;
				        break;
			        }
                }

                if(match==0)
		        {
                    lst = new ClSTtype();
			        lst.strMod1 = mod.name;
			        lst.strStg = stg.label;listmodlst.Add(lst);
		        }
            }
            else
            {
                lst = new ClSTtype();
                lst.strMod1 = mod.name;
                lst.strStg = stg.label;
                listmodlst.Add(lst);
            }
            ViewerUtils.WriteModelLstFile(pathfile, listmodlst);

            string mdlnam = string.Empty, strnam = string.Empty;
	        int telemno = 0, teltype = 0;
	        double tstchn = 0.0 , tendchn = 0.0, tIPX = 0.0, tIPY = 0.0;
	        double tT1 = 0.0 , tT2 = 0.0, tR = 0.0, tL = 0.0;
	        double txst = 0.0 , tyst = 0.0, txed = 0.0, tyed = 0.0;
	        double tB1 = 0.0 , tB2 = 0.0, tDEL = 0.0;
	        int tturn1 = 0;

	        int index, ok;
	        double cumchn = 0;//lastxed, lastyed, 
	        double last_pts_mx = 0, last_pts_my = 0;

            StreamWriter tmpFile = null;
            string slTmp = string.Empty;

	        index=0;
	        lastchn=-999.999;
	        //fpos_t loc1;
	        ok=0;
	
            fp1 = new StreamReader(Path.Combine(path, "HALIGN.FIL"));

            while(fp1.EndOfStream == false)
            {
                string strLine = fp1.ReadLine();
                CHalignFilData fildata = CHalignFilData.Parse(strLine);
                mdlnam 	= fildata.sMod;
                strnam 	= fildata.sString;
                telemno = fildata.iSlno;
                teltype = fildata.iEltype;
                tstchn 	= fildata.dStartchn;
                tendchn = fildata.dEndchn;
                tIPX 	= fildata.dHipx;
                tIPY 	= fildata.dHipy;
                tT1 	= fildata.dT1;
                tT2 	= fildata.dT2;
                tR 	= fildata.dRadius;
                tL 	= fildata.dEllength;
                txst 	= fildata.dStartx;
                tyst 	= fildata.dStarty;
                txed 	= fildata.dEndx;
                tyed 	= fildata.dEndy;
                tB1 	= fildata.dB1;
                tB2 	= fildata.dB2;
                tDEL 	= fildata.dDeviation;
                tturn1 	= (short)fildata.iTurn; 
                
                if(tendchn == lastchn)
                    continue;
                       

                if (txst > 98000.0)
                {
                    ok=1;
                }

                if(modnam.ToLower() == mdlnam.ToLower() && stglbl.ToLower() == strnam.ToLower())
                {
                    if(teltype==1)
                    {
                        ok=1;
				        if(index==0)
				        {
                            cumchn=tstchn;
                            lab = new CLabtype();
					        lab.attr = CLabtype.Type.Point;					        
                            pts = new CPTStype();
					        pts.mc=cumchn;
					        pts.mx=txst;
					        pts.my=tyst;
					        pts.mz=0.0;
                            lab.Tag = pts;
                            listfil.Add(lab); 


					        tmpFile = new StreamWriter(Path.Combine(path, "SCROLL.TMP"), true);
                            
                            slTmp = mod.name + '\t' + strnam + '\t' + telemno.ToString("0000") + '\t' 
                                + "STRAIGHT" + '\t' + tL.ToString("0.000") + '\t' + tB1.ToString("0.000");

                            tmpFile.WriteLine(slTmp);
					        tmpFile.Close();       					
					        
        			
					        // Display scroll
                            //CString buffer;
                            //buffer.Format("%8s %8s ELEM# = %4d     STRAIGHT     LENGTH=%.3f(M)     BEARING=%.3f(D) CHN=%.3f", mod.name, strnam, telemno, tL, tB1, cumchn);
        					
                            //int i = m_ModelCtrl.AddString(buffer);
                            //m_ModelCtrl.SetCurSel(i);

                            slTmp = string.Format("{0,8} {1,8} ELEM# = {2,4:D}     STRAIGHT     LENGTH={3:f3}(M)     BEARING={4:f3}(D) CHN={5:f3}", mod.name, strnam, telemno, tL, tB1, cumchn);
                            this.DisplayScroll(slTmp);

                            last_pts_mx = pts.mx;
                            last_pts_my = pts.my;
                            lastchn = tstchn;
				        }
                        else{

                        cumchn = cumchn + Math.Sqrt((pts.mx - last_pts_mx) * (pts.mx - last_pts_mx) + (pts.mx - last_pts_mx) * (pts.mx - last_pts_mx));

                        lab = new CLabtype();
				        lab.attr = CLabtype.Type.Point;					        
                        pts = new CPTStype();
				        pts.mc=cumchn;
                        if (tIPX == 0.0 && tIPY == 0.0)
                        {
                            pts.mx = txst;
                            pts.my = tyst;
                        }
                        else
                        {
                            pts.mx = tIPX;
                            pts.my = tIPY;
                        }
				        pts.mz=0.0;
                        lab.Tag = pts;
                        listfil.Add(lab);

                        //cumchn = cumchn + Math.Sqrt((pts.mx - last_pts_mx) * (pts.mx - last_pts_mx) + (pts.mx - last_pts_mx) * (pts.mx - last_pts_mx));

				
                        tmpFile = new StreamWriter(Path.Combine(path, "SCROLL.TMP"), true);
                            
                        slTmp = mod.name + '\t' + strnam + '\t' + telemno.ToString("0000") + '\t' 
                            + "STRAIGHT" + '\t' + tL.ToString("0.000") + '\t' + tB1.ToString("0.000");

                        tmpFile.WriteLine(slTmp);
				        tmpFile.Close(); 				

		
				        // Display scroll
                        //CString buffer;
                        //buffer.Format("%8s %8s ELEM# = %4d     STRAIGHT     LENGTH=%.3f(M)     BEARING=%.3f(D) CHN=%.3f", mod.name, strnam, telemno, tL, tB1, cumchn);
        				
                        //int i = m_ModelCtrl.AddString(buffer);
                        //m_ModelCtrl.SetCurSel(i);
                        slTmp = string.Format("{0,8} {1,8} ELEM# = {2,4:D}     STRAIGHT     LENGTH={3:f3}(M)     BEARING={4:f3}(D) CHN={5:f3}", mod.name, strnam, telemno, tL, tB1, cumchn);
                        this.DisplayScroll(slTmp);
				
				        last_pts_mx = pts.mx;
				        last_pts_my = pts.my;
				        lastchn=tendchn;
                    }
				        index=1;
                    }// teltype=1
                }// model string found
            }//while halign.fil

            if(ok == 1)
            {
                lab = new CLabtype();
		        lab.attr = CLabtype.Type.Point;					        
                pts = new CPTStype();
		        pts.mc = cumchn;
		        pts.mx = txed;
		        pts.my = tyed;
		        pts.mz=0.0;
                lab.Tag = pts;
                listfil.Add(lab);

                cumchn = cumchn + Math.Sqrt((pts.mx-last_pts_mx)*(pts.mx-last_pts_mx) + (pts.mx-last_pts_mx)*(pts.mx-last_pts_mx));


                tmpFile = new StreamWriter(Path.Combine(path, "SCROLL.TMP"), true);
                            
                slTmp = mod.name + '\t' + strnam + '\t' + telemno.ToString("0000") + '\t' 
                    + "STRAIGHT" + '\t' + tL.ToString("0.000") + '\t' + tB1.ToString("0.000");

                tmpFile.WriteLine(slTmp);
		        tmpFile.Close();

                lab = new CLabtype();
                lab.attr = CLabtype.Type.EndCode;
                listfil.Add(lab);
            }
            fp1.Close();
            ViewerUtils.WriteModelFilFile(Path.Combine(path, "MODEL.FIL"), listfil);
        }

        void FuncMainHalignRep()
        {
            //string filestr;
            //double pi;

            //pi=3.141592654;
	        r9=57.29577951;     /*   radian to degree 180/pi   */

	        //Data organize

            pathfile = Path.Combine(path, "HALIGNMENT.REP");

            fp2 = new StreamWriter(pathfile, true);

	        header1();;
	        element_details();

            header2();
	        element_summary_report();

	        header2();
	        alignmentDetails();

	        writeBatchInput();

            fp2.Close();
        }

        void header1()
        {
            fp2.Write("\n\n\n\t\t\t\t\t\t*********************************************");
            fp2.Write("\n\n\t\t\t\t\t              DESIGN OF HORIZONTAL ALIGNMENT");
            fp2.Write("\n\n\t\t\t			   	       H E A D S");
            fp2.Write("\n\n\t\t\t				       Release 14.0");
            fp2.Write("\n\n\t\t\t				         By");
            fp2.Write("\n\n\t\t\t\t		        TechSOFT  Engineering  Services");
            fp2.Write("\n\n\t\t\t\t\t              Program was run on  " + DateTime.Now.ToString());
            fp2.Write("\n\n\n\t\t\t\t\t\t*********************************************");
            fp2.Write("\n\n\n");
        }

        void header2()
        {
            fp2.Write("\n\n\n");


            fp2.Write("  H E A D S  ** Highway Engineering And Design System.\n\n");
            fp2.Write("  By TechSOFT Engineering Services.\n\n");            

            fp2.Write("  DATE : " + DateTime.Now.ToString("dd,MM,yyyy [dddd]") + "\n");
            fp2.Write("  TIME : " + DateTime.Now.ToString("T") + "\n\n");
            fp2.Write("  HORIZONTAL ALIGNMENT DESIGN REPORT .\n");
            fp2.Write("  ---------------------------------- .\n\n\n");
        }

        void footer()
        {
            fp2.Write("\n\n");
            //fprintf(fp2," END OF DESIGN ------------------------------------------------------------------------------ .\n\n\n");
            fp2.Write(" End of Horizontal Alignment---------------------------------------------------------------- .\n\n\n");
        }

        void element_summary_report()
        {
            fp2.Write("  TABLE 3:\n");
            fp2.Write("  ELEMENT SUMMARY --------------------------------------------------------------------------\n\n\n");
            fp2.Write("  ELEMENT    CHAINAGE      C O O R D I N A T E S         BEARING       RADIUS OF     LENGTH\n");
            fp2.Write("	                -----X----      -----Y-----      DEG MIN SEC   CURVATURE \n\n\n");

            element_summary();
        }

        void element_details()
        {
            int dg1, mn1, dg2, mn2, cnt = 0, last_eltype = 0, hipcnt;
            double last_stchn=-999.999, last_endchn=-999.999, sc1, sc2;
            double gchn1 = 0, gchn2, val, user_V, user_e, V, Vc = 0, emax = 0, e = 0;
            string side, elem = string.Empty, devstr;
            string modnam1, stglbl1;

            //FILE *fp6;
            fp1 = new StreamReader(Path.Combine(path, "HALIGN.FIL"));
            
            StreamWriter fp6 = new StreamWriter(Path.Combine(path,"HDS006.FIL"), true);
            fp2.Write(" Please note: Data assumed for 'Design Speed [V]', 'Maximum Super elevation [e]' and Coeff. of Friction [F] in \"200 HALIGN\" Input data\n");
            fp2.Write(" Data assumed as: \"202,XC=0.0,YC=0.0,SC=0.00000,IN=5.000, [EC=...optional], V=100,e=5.0,F=0.0\" in file HDS006.FIL\n");
			
            fp2.Write("\n");

            hipcnt=2;

            while(fp1.EndOfStream == false)
            {
                string strLine = fp1.ReadLine();
                CHalignFilData fildata = CHalignFilData.Parse(strLine);
                modnam1 = fildata.sMod;
                stglbl1 = fildata.sString;
                elno = (short)fildata.iSlno;
                eltype = fildata.iEltype;
                stchn = fildata.dStartchn;
                endchn = fildata.dEndchn;
                IPX = fildata.dHipx;
                IPY = fildata.dHipy;
                T1 = fildata.dT1;
                T2 = fildata.dT2;
                rad = fildata.dRadius;
                L = fildata.dEllength;
                xst = fildata.dStartx;
                yst = fildata.dStarty;
                xed = fildata.dEndx;
                yed = fildata.dEndy;
                B1 = fildata.dB1;
                B2 = fildata.dB2;
                del = fildata.dDeviation;
                turn = (short)fildata.iTurn;

		        if(modnam1 != modnam || stglbl1 != stglbl)
                    continue;

		        if(endchn == last_chn)
                    continue;

		        user_V = 100.0;
		        user_e = 5.0;
		        //user_F = 0.0;
        	
		        if(eltype==1)
		        {
			        gchn1=stchn;
			        gchn2=endchn;
			        val=0.0;
                    strLine = modnam + '\t' + stglbl + '\t' 
                        + gchn1.ToString() + '\t' 
                        + gchn2.ToString() + '\t' 
                        + user_V.ToString() + '\t' 
                        + ((double)(user_e / 100.0)).ToString() + '\t' 
                        + val.ToString();
                    fp6.WriteLine(strLine);
			        //fprintf(fp6,"%8s %8s %15.3f %15.3f %10.1f %10.2f %10.3f\n", modnam, stglbl, gchn1, gchn2, user_V, user_e/100.0, val);
		        }
		        else if(eltype==2)
		        {
                    L1 = L;
                    gchn1 = stchn;
		        }
		        else if(eltype==3)
		        {
                    Lc = L;

                    V = user_V;
                    emax = user_e / 100.0;

                    e = V * V / (225 * rad);

			        ///////////////////

			        if(e>emax)
			        {
                        Vc = Math.Sqrt(emax * 225 * rad);
                        ival = (short)(Vc / 10);

                        if (Vc - ival * 10 > 5)
                        {
                            Vc = ival * 10 + 5;
                        }
                        else
                        {
                            Vc = ival * 10;
                        }        				
			        }
			        else
			        {
				        Vc = V;
			        }

			        e=Vc*Vc/(225*rad);

			        ///////////////////

			        if(last_eltype != 2)
			        {
				        gchn1=stchn;
				        gchn2=endchn;

                        strLine = modnam + '\t' 
                            + stglbl + '\t' 
                            + gchn1.ToString() + '\t' 
                            + gchn2.ToString() + '\t' 
                            + Vc.ToString() + '\t' 
                            + emax.ToString() + '\t' 
                            + e.ToString();

                        fp6.WriteLine(strLine);
				        //fprintf(fp6,"%8s %8s %15.3f %15.3f %10.1f %10.2f %10.3f\n", modnam, stglbl, gchn1, gchn2, Vc, emax, e);

				        hipcnt++;
			        }
        			
		        }
		        else if(eltype==4)
		        {
                    gchn2 = endchn;

                    strLine = modnam + '\t'
                            + stglbl + '\t'
                            + gchn1.ToString() + '\t'
                            + gchn2.ToString() + '\t'
                            + Vc.ToString() + '\t'
                            + emax.ToString() + '\t'
                            + e.ToString();

                    fp6.WriteLine(strLine);        		
			        //fprintf(fp6,"%8s %8s %15.3f %15.3f %10.1f %10.2f %10.3f\n", modnam, stglbl, gchn1, gchn2, Vc, emax, e);

			        hipcnt++;
		        }                
		        //fprintf(fp2,"\n");
		        cnt++;

                last_chn = endchn;
                last_eltype = eltype;
            }      /* while feof */


            fp6.Close();

	        fp1.Close();
            fp1 = new StreamReader(Path.Combine(path, "HALIGN.FIL"));


            fp2.Write("  TABLE 2: ELEMENT DATA\n");
            fp2.Write("  ------------\n\n");

            fp2.Write("EL-#     ELEMENT    LENGTH    START-CHN      END-CHN    START-BRG   END-BRG        TURN        DEVIATION\n");
            fp2.Write("                                                        DEG MIN S   DEG MIN SEC                ANGLE\n\n");


            while (fp1.EndOfStream == false)
            {
                string strLine = fp1.ReadLine();
                CHalignFilData fildata = CHalignFilData.Parse(strLine);
                modnam1 = fildata.sMod;
                stglbl1 = fildata.sString;
                elno = (short)fildata.iSlno;
                eltype = fildata.iEltype;
                stchn = fildata.dStartchn;
                endchn = fildata.dEndchn;
                IPX = fildata.dHipx;
                IPY = fildata.dHipy;
                T1 = fildata.dT1;
                T2 = fildata.dT2;
                RA = fildata.dRadius;
                L = fildata.dEllength;
                xst = fildata.dStartx;
                yst = fildata.dStarty;
                xed = fildata.dEndx;
                yed = fildata.dEndy;
                B1 = fildata.dB1;
                B2 = fildata.dB2;
                del = fildata.dDeviation;
                turn = (short)fildata.iTurn;


                //fscanf(fp1,"%s %s %i %i", modnam1, stglbl1, &elno, &eltype);
                //fscanf(fp1,"%lf %lf %lf %lf", &stchn, &endchn, &IPX, &IPY);
                //fscanf(fp1,"%lf %lf %lf %lf", &T1, &T2, &RA, &L);
                //fscanf(fp1,"%lf %lf %lf %lf", &xst, &yst, &xed, &yed);
                //fscanf(fp1,"%lf %lf %lf %i", &B1, &B2, &del, &turn);
                
                if(modnam1.ToLower() != modnam.ToLower() || stglbl1.ToLower() != stglbl.ToLower())
                    continue;
                
                if(stchn == last_stchn && endchn == last_endchn)
                    break;

                if (eltype == 1)
                {
                    elem = "STRAIGHT";
                }
                else if (eltype == 2)
                {
                    elem = "LEAD-TRANS";
                }
                else if (eltype == 3)
                {
                    elem = "CIRCULAR";
                }
                else if (eltype == 4)
                {
                    elem = "TRAIL-TRANS";
                }
                
                brg=B1;
	            deg_min_sec();
	            dg1=dg; mn1=mn; sc1=sc;

	            brg=B2;
	            deg_min_sec();
	            dg2=dg; mn2=mn; sc2=sc;

	            devstr = del.ToString();

                if (turn == 1)
                    side = "LEFT";
                else if (turn == 2)
                    side = "RIGHT";
                else
                    side = "STRAIGHT";

                strLine = string.Format("{0,3:D}  {1,11} {2,9:f5} {3,12:f5} {4,12:f5} {5,3:D} {6,2:D} {7,5:f2} {8,3:D} {9,2:D} {10,5:f2} {11,8}     {12,8}\n\n", elno, elem, L, stchn, endchn, dg1, mn1, sc1, dg2, mn2, sc2, side, devstr);
                fp2.Write(strLine);


	            last_stchn = stchn;
	            last_endchn = endchn;

            }

            fp2.Write("\n\n");
            fp1.Close();

        }

        void deg_min_sec()
        {
            long iang;
            double val;
            /* rounded off at 3rd decimal place */

            if (brg < 0)
                brg += 360;
            else if (brg > 360)
                brg -= 360;

            iang = (long)brg;
            dg = (short)iang;

            val = (brg - iang) * 60;
            mn = (short)val;

            sc = (val - mn) * 60;
        }

        static List<CHalignFilData> ReadHalignFilFile(string strHalignFilePath)
        {
            List<CHalignFilData> filDataList = null;
            if (File.Exists(strHalignFilePath))
            {
                filDataList = new List<CHalignFilData>();
                StreamReader readerSrc = new StreamReader(strHalignFilePath);                
                while (readerSrc.EndOfStream == false)
                {
                    string strline = readerSrc.ReadLine();
                    CHalignFilData fildata = CHalignFilData.Parse(strline);

                    if (fildata != null)
                    {
                        filDataList.Add(fildata);                       
                    }                    
                }
                readerSrc.Close();
            }
            return filDataList;
        }

        void writeBatchInput()
        {
            double X1, Y1, X2, Y2, L1, L2;
            string strLine = string.Empty;
            CHalignFilData fildata = null;           

            fp2.Write("\n\n\nTABLE 6:\n");
            fp2.Write("Input data for Major Option 200 HALIGN\n\n\n");


            fp2.Write("HEADS\n");
            fp2.Write("200,HALIGN\n");
            fp2.Write("201,MODEL=" + usermod + ",STRING=" + userstg + "\n");

            X1 = Y1 = X2 = Y2 = rad = L1 = L2 = 0.0;
            last_stchn = 0.0;
            last_endchn = 0.0;


            int max_el = -9999;
            int tot_el = 0;
            long ind = 0;

            int iHIndex = 0;
            List<CHalignFilData> HfilList = ReadHalignFilFile(Path.Combine(path, "HALIGN.FIL"));

            for(iHIndex = 0; iHIndex < HfilList.Count; iHIndex++)
            {
                fildata = HfilList[iHIndex];
                modnam = fildata.sMod;
                stglbl = fildata.sString;
                ival = (short)fildata.iSlno;
                eltype = fildata.iEltype;
                stchn = fildata.dStartchn;
                endchn = fildata.dEndchn;
                IPX = fildata.dHipx;
                IPY = fildata.dHipy;
                T1 = fildata.dT1;
                T2 = fildata.dT2;
                rad = fildata.dRadius;
                L = fildata.dEllength;
                xst = fildata.dStartx;
                yst = fildata.dStarty;
                xed = fildata.dEndx;
                yed = fildata.dEndy;
                B1 = fildata.dB1;
                B2 = fildata.dB2;
                DEL = fildata.dDeviation;
                turn = (short)fildata.iTurn;

                if (modnam.ToLower() == usermod.ToLower() && stglbl.ToLower() == userstg.ToLower())
                {
                    if (ind == 0)
                    {
                        strLine = string.Format("202,XC=0.0,YC=0.0,SC={0:f5},IN={1:f3}, V=100,e=5.0,F=0.0\n", stchn, inc);
                        fp2.Write(strLine);
                        //fprintf(fp2, "202,XC=0.0,YC=0.0,SC=%.5f,IN=%.3f, V=100,e=5.0,F=0.0\n", stchn, inc);
                        ind = 1;
                    }

                    if (max_el < ival)
                    {
                        max_el = ival;
                    }

                    if (stchn == last_stchn && endchn == last_endchn)
                        break;

                    tot_el++;
                    last_stchn = stchn;
                    last_endchn = endchn;
                }
            }

            long last_ival = 0;
            int opnum = 0;
            int last_opnum = 0;
            int last_cnt;
            last_stchn = -999.999;
            int elcnt = 0;

            last_cnt = 0;
            last_IPX = -999.999;
            last_IPY = -999.999;
            ind = 0;

            for (iHIndex = 0; iHIndex < HfilList.Count; iHIndex++)
            {
                fildata = HfilList[iHIndex];

                modnam = fildata.sMod;
                stglbl = fildata.sString;
                ival = (short)fildata.iSlno;
                eltype = fildata.iEltype;
                stchn = fildata.dStartchn;
                endchn = fildata.dEndchn;
                IPX = fildata.dHipx;
                IPY = fildata.dHipy;
                T1 = fildata.dT1;
                T2 = fildata.dT2;
                rad = fildata.dRadius;
                L = fildata.dEllength;
                xst = fildata.dStartx;
                yst = fildata.dStarty;
                xed = fildata.dEndx;
                yed = fildata.dEndy;
                B1 = fildata.dB1;
                B2 = fildata.dB2;
                DEL = fildata.dDeviation;
                turn = (short)fildata.iTurn;

                if (modnam != usermod || stglbl != userstg)
                {
                    continue;
                }

                if (modnam.ToLower() == usermod.ToLower() && stglbl.ToLower() == userstg.ToLower())
                {
                    if (stchn == last_stchn && endchn == last_endchn)
                        break;

                    elcnt++;

                    if (stchn == last_stchn)
                        last_cnt++;
                    if (last_cnt == 2)
                    {
                        //AfxMessageBox("In Halign.fil Start Chainage is repeating in 4th Column...quiting...");
                        break;
                    }
                }
                if (ival == last_ival)
                    break;
                if (IPX != 0.0 || IPY != 0.0)
                {
                    opnum = 203;

                    switch (eltype)
                    {
                        case 1:
                            {
                                rad = L1 = L2 = 0.0;
                                if (last_opnum == 203)
                                {
                                    if (last_IPX == IPX && last_IPY == IPY)
                                    {
                                        strLine = string.Format("203,SX,X1={0:f5},Y1={1:f5},X2={2:f5},Y2={3:f5}\n", last_IPX, last_IPY, xed, yed);
                                        fp2.Write(strLine);
                                        //fprintf(fp2, "203,SX,X1=%.5f,Y1=%.5f,X2=%.5f,Y2=%.5f\n", last_IPX, last_IPY, xed, yed);
                                    }
                                    else
                                    {
                                        if (elcnt == tot_el)
                                        {
                                            strLine = string.Format("203,SX,X1={0:f5},Y1={1:f5},X2={2:f5},Y2={3:f5}\n", last_IPX, last_IPY, xed, yed);
                                            fp2.Write(strLine);

                                            //fprintf(fp2, "203,SX,X1=%.5f,Y1=%.5f,X2=%.5f,Y2=%.5f\n", last_IPX, last_IPY, xed, yed);
                                        }
                                        else
                                        {

                                            //if (IPY < 937009.0)
                                            //    chk = 1;
                                            strLine = string.Format("203,SX,X1={0:f5},Y1={1:f5},X2={2:f5},Y2={3:f5}\n", last_IPX, last_IPY, IPX, IPY);
                                            fp2.Write(strLine);
                                            //fprintf(fp2, "203,SX,X1=%.5f,Y1=%.5f,X2=%.5f,Y2=%.5f\n", last_IPX, last_IPY, IPX, IPY);
                                        }
                                    }
                                }
                                else
                                {
                                    if (last_IPX == IPX && last_IPY == IPY)
                                        continue;

                                    strLine = string.Format("203,SX,X1={0:f5},Y1={1:f5},X2={2:f5},Y2={3:f5}\n", xst, yst, IPX, IPY);
                                    fp2.Write(strLine);
                                    //fprintf(fp2, "203,SX,X1=%.5f,Y1=%.5f,X2=%.5f,Y2=%.5f\n", xst, yst, IPX, IPY);
                                }
                                break;
                            }
                        case 2:
                            L1 = L;
                            break;
                        case 3:	
                        {
                            //	Sandipan	03/04/2004	below		
                            if (iHIndex < HfilList.Count)
                            {
                                iHIndex++;
                            }
                            else 
                            { 
                                break; 
                            }
                            
                            fildata = HfilList[iHIndex];
                            //strLine = fp1.ReadLine();
                            //strStartLine = strLine;
                            //fildata = CHalignFilData.Parse(strLine);
                            modnam = fildata.sMod;
                            stglbl = fildata.sString;
                            ival = (short)fildata.iSlno;
                            eltype = fildata.iEltype;
                            stchn = fildata.dStartchn;
                            endchn = fildata.dEndchn;
                            IPX = fildata.dHipx;
                            IPY = fildata.dHipy;
                            T1 = fildata.dT1;
                            T2 = fildata.dT2;
                            rad = fildata.dRadius;
                            L = fildata.dEllength;
                            xst = fildata.dStartx;
                            yst = fildata.dStarty;
                            xed = fildata.dEndx;
                            yed = fildata.dEndy;
                            B1 = fildata.dB1;
                            B2 = fildata.dB2;
                            DEL = fildata.dDeviation;
                            turn = (short)fildata.iTurn;

                            if (eltype == 4)
                            {
                                L2 = L;                                
                            }
                            else // eltype==1
                            {
                                L2 = 0.0;
                                iHIndex = iHIndex - 1;

                                fildata = HfilList[iHIndex];
                                //strLine = fp1.ReadLine();
                                //strStartLine = strLine;
                                //fildata = CHalignFilData.Parse(strLine);
                                modnam = fildata.sMod;
                                stglbl = fildata.sString;
                                ival = (short)fildata.iSlno;
                                eltype = fildata.iEltype;
                                stchn = fildata.dStartchn;
                                endchn = fildata.dEndchn;
                                IPX = fildata.dHipx;
                                IPY = fildata.dHipy;
                                T1 = fildata.dT1;
                                T2 = fildata.dT2;
                                rad = fildata.dRadius;
                                L = fildata.dEllength;
                                xst = fildata.dStartx;
                                yst = fildata.dStarty;
                                xed = fildata.dEndx;
                                yed = fildata.dEndy;
                                B1 = fildata.dB1;
                                B2 = fildata.dB2;
                                DEL = fildata.dDeviation;
                                turn = (short)fildata.iTurn;
                                
                            }       
                                                   

                            //	Sandipan	11/01/2009	below
                            //fp1.BaseStream.Position = loc9;                             
                            if (turn == 1)
                            {
                                if (L1 > 0.0 || L2 > 0.0)	// Modified on 18/10/2001
                                {
                                    strLine = string.Format("203,CLE,RA={0:f5},L1={1:f5},L2={2:f5}\n", rad, L1, L2);
                                    fp2.Write(strLine);
                                    //fprintf(fp2, "203,CLE,RA=%.5f,L1=%.5f,L2=%.5f\n", rad, L1, L2);
                                }
                                else
                                {
                                    strLine = string.Format("203,CLE,RA={0:f5}\n", rad);
                                    fp2.Write(strLine);
                                    //fprintf(fp2, "203,CLE,RA=%.5f\n", rad);
                                }

                            }
                            else
                            {
                                if (L1 > 0.0 || L2 > 0.0)	// Modified on 18/10/2001
                                {
                                    strLine = string.Format("203,CRE,RA={0:f5},L1={1:f5},L2={2:f5}\n", rad, L1, L2);
                                    fp2.Write(strLine);

                                    //fprintf(fp2, "203,CRE,RA=%.5f,L1=%.5f,L2=%.5f\n", rad, L1, L2);
                                }
                                else
                                {
                                    strLine = string.Format("203,CRE,RA={0:f5}\n", rad);
                                    fp2.Write(strLine);
                                    //fprintf(fp2, "203,CRE,RA=%.5f\n", rad);
                                }
                            }                           
                            
                            break;
                        }
                        case 4:
                            L2 = L;
                            break;

                        default:
                            break;
                    }

                }
                else
                {
                    opnum = 204;

                    switch (eltype)
                    {
                        case 1:
                            rad = 0.0;
                            strLine = string.Format("204,SX,X1={0:f5},Y1={1:f5},L={2:f5},B1={3:f5},RA={4:f5}\n", xst, yst, L, B1, rad);
                            fp2.Write(strLine);
                            //fprintf(fp2, "204,SX,X1=%.5f,Y1=%.5f,L=%.5f,B1=%.5f,RA=%.5f\n", xst, yst, L, B1, rad);
                            break;
                        case 2:
                            if (turn == 1)
                            {
                                strLine = string.Format("204,LLX,X1={0:f5},Y1={1:f5},L={2:f5},B1={3:f5},RA={4:f5}\n", xst, yst, L, B1, rad);
                                fp2.Write(strLine);
                                //fprintf(fp2, "204,LLX,X1=%.5f,Y1=%.5f,L=%.5f,B1=%.5f,RA=%.5f\n", xst, yst, L, B1, rad);
                            }
                            else
                            {
                                strLine = string.Format("204,LRX,X1={0:f5},Y1={1:f5},L={2:f5},B1={3:f5},RA={4:f5}\n", xst, yst, L, B1, rad);
                                fp2.Write(strLine);
                                //fprintf(fp2, "204,LRX,X1=%.5f,Y1=%.5f,L=%.5f,B1=%.5f,RA=%.5f\n", xst, yst, L, B1, rad);
                            }

                            L1 = L;
                            break;
                        case 3:
                            if (turn == 1)
                            {
                                strLine = string.Format("204,CLX,X1={0:f5},Y1={1:f5},L={2:f5},B1={3:f5},RA={4:f5}\n", xst, yst, L, B1, rad);
                                fp2.Write(strLine);
                                //fprintf(fp2, "204,CLX,X1=%.5f,Y1=%.5f,L=%.5f,B1=%.5f,RA=%.5f\n", xst, yst, L, B1, rad);
                            }
                            else
                            {
                                strLine = string.Format("204,CRX,X1={0:f5},Y1={1:f5},L={2:f5},B1={3:f5},RA={4:f5}\n", xst, yst, L, B1, rad);
                                fp2.Write(strLine);
                                //fprintf(fp2, "204,CRX,X1=%.5f,Y1=%.5f,L=%.5f,B1=%.5f,RA=%.5f\n", xst, yst, L, B1, rad);
                            }
                            break;
                        case 4:
                            if (turn == 1)
                            {
                                strLine = string.Format("204,TLX,X1={0:f5},Y1={1:f5},L={2:f5},B1={3:f5},RA={4:f5}\n", xst, yst, L, B1, rad);
                                fp2.Write(strLine);
                                //fprintf(fp2, "204,TLX,X1=%.5f,Y1=%.5f,L=%.5f,B1=%.5f,RA=%.5f\n", xst, yst, L, B1, rad);
                            }
                            else
                            {
                                strLine = string.Format("204,TRX,X1={0:f5},Y1={1:f5},L={2:f5},B1={3:f5},RA={4:f5}\n", xst, yst, L, B1, rad);
                                fp2.Write(strLine);
                                //fprintf(fp2, "204,TRX,X1=%.5f,Y1=%.5f,L=%.5f,B1=%.5f,RA=%.5f\n", xst, yst, L, B1, rad);
                            }

                            L2 = L;
                            break;
                        default:
                            break;
                    }
                }
                //	Sandipan	03/04/2004
                ind++;
                if (opnum == 203)
                {
                    last_IPX = IPX;
                    last_IPY = IPY;
                }

                last_opnum = opnum;
                last_stchn = stchn;
                last_endchn = endchn;

            }//End for each line in halign fill file

            fp2.Write("FINISH\n\n\n\n");

            fp2.Write("     *************\n");
            fp2.Write("     END OF DESIGN\n");
            fp2.Write("     *************\n");

        }

        void alignmentDetails()
        {
            string strLine;
            //double chk=0;
            string radcurve;
            double lastRA=0.0;
            
            //fpos_t loc1;
            pathfile = Path.Combine(path, "MODEL.TMP");
            if (File.Exists(pathfile) == false)
            {
                System.Windows.Forms.MessageBox.Show("File HALIGN.FIL not found...!", HeadsUtils.Constants.ProductName);
                return;
            }
            fp1 = new StreamReader(pathfile);
 
            fp2.Write("\n\n");

            fp2.Write("  TABLE 4:\n");
            fp2.Write("  ALIGNMENT DETAILS ------------------------------------------------------------------------\n\n\n");
            fp2.Write("  ELEMENT             CHAINAGE        C O O R D I N A T E S        BEARING          RADIUS OF \n");
            fp2.Write("	                           -----X----      -----Y-----     DEG MIN SEC      CURVATURE \n\n\n");

            elno=0;
            eltype=0;
            chn=0.0;
            ex=ey=ez=0.0;
            RA=0.0;
            brg=0.0;
            turn=0;
            
            spchn_flag=0;
            last_chn = 0.0;
            last_RA = 0.0;
            last_eltype=0;
            
            PTS = "START POINT - SA";

	        al_index=0;
	        //rep_flag=0;
            //long loc1 = 0;
            
            while(fp1.EndOfStream == false)
            {
                //loc1 = fp1.BaseStream.Position;
                strLine = fp1.ReadLine();
                string[] arrs = strLine.Split(new char[]{'\t'});
                int iPos = 0;
                modnam = arrs[iPos];iPos++;
                stglbl = arrs[iPos];iPos++;
                eltype = short.Parse(arrs[iPos]);iPos++;
                chn = double.Parse(arrs[iPos]);iPos++;
                ex = double.Parse(arrs[iPos]);iPos++;
                ey = double.Parse(arrs[iPos]);iPos++;
                ez = double.Parse(arrs[iPos]);iPos++;
                RA = double.Parse(arrs[iPos]);iPos++;
                brg = double.Parse(arrs[iPos]);iPos++;
                turn = short.Parse(arrs[iPos]);iPos++;
                spchn_flag = short.Parse(arrs[iPos]);

                //fscanf(fp1, "%s %s %i %lf %lf %lf %lf %lf %lf %i %i", modnam, stglbl, &eltype, &chn, &ex, &ey, &ez, &RA, &brg, &turn, &spchn_flag);

                //if(chn>53.0)
                //    chk=1;
                
                if(elno >= 1 && eltype != last_eltype)
                    elno++;
                
                if(turn==1)
                    RA = 0.0-RA;
                
                deg_min_sec();

                if(al_index==0)
                {
                    st_chn=chn;
                    al_index=1;
                }

                writeAlignment();

		        last_eltype = eltype;
		        last_chn = chn;
		        last_RA = RA;
		        spchn_flag=0;
            }
            PTS = "END   POINT - EA";
            
            if(eltype == 1)
                radcurve = "INFINITY";
             else
                radcurve = RA.ToString("0.000");

            strLine = string.Format(" {0}  {1,10:f3} {2,15:f3} {3,15:f3}  {4,6:D} {5,2:D} {6,6:f2} {7,12}\n", PTS, chn, ex, ey, dg, mn, sc, radcurve);
            fp2.Write(strLine);
            //sprintf(fp2," %s  %10.3f %15.3f %15.3f  %6d %2d %6.2f %12s\n",PTS, chn, ex, ey, dg, mn, sc, radcurve);
            footer();

            fp1.Close();


            // Write HIPs

	        int ipindex, lastelno = 0, lasteltype;
	        double lastipx=-999.999, lastipy=-999.999;
	        double lastxed=-999.999, lastyed=-999.999;
	        string modnam1, stglbl1;
            pathfile = Path.Combine(path, "HALIGN.FIL");
            if (File.Exists(pathfile) == false)
            {
                System.Windows.Forms.MessageBox.Show("File HALIGN.FIL not found...!", HeadsUtils.Constants.ProductName);
                return;
            }
            fp1 = new StreamReader(pathfile);
            
            ipindex=0;
	        cnt=1;
	        lasteltype=0;
	        L1=0.0;
	        L2=0.0;

            fp2.Write("  TABLE 5:\n");
            fp2.Write("  List of Horizontal Alignment Intersection Points [HIPs]\n\n");
            fp2.Write("    X_IP (Eastings M.)  Y_IP (Northings M.)\n");

            while (fp1.EndOfStream == false)
            {
                strLine = fp1.ReadLine();
                CHalignFilData fildata = CHalignFilData.Parse(strLine);

                modnam1 = fildata.sMod;
                stglbl1 = fildata.sString;
                elno = (short)fildata.iSlno;
                eltype = fildata.iEltype;
                stchn = fildata.dStartchn;
                endchn = fildata.dEndchn;
                IPX = fildata.dHipx;
                IPY = fildata.dHipy;
                T1 = fildata.dT1;
                T2 = fildata.dT2;
                RA = fildata.dRadius;
                L = fildata.dEllength;
                xst = fildata.dStartx;
                yst = fildata.dStarty;
                xed = fildata.dEndx;
                yed = fildata.dEndy;
                B1 = fildata.dB1;
                B2 = fildata.dB2;
                del = fildata.dDeviation;
                turn = (short)fildata.iTurn;
                
                                
                if(modnam1.ToLower() == modnam.ToLower() &&  stglbl1.ToLower() == stglbl.ToLower())
                {
                    if(ipindex == 0)
                    {
                        strLine = string.Format("    {0,4:D} {1,10:f5}         {2,10:f5}\n", cnt, xst, yst);
                        fp2.Write(strLine);
                        //sprintf(fp2, "    %4d %10.5f         %10.5f\n", cnt, xst, yst);
                    }
                    
                    if(elno == lastelno)
                    {
                        continue;
                    }

			        if(IPX == 0.0 && IPY == 0.0)
			        {
				        if(xed == lastxed && yed == lastyed)
				        {
					        continue;
				        }
				        else
				        {
					        cnt++;
                            strLine = string.Format("    {0,4:D} {1,10:f5}         {2,10:f5}\n", cnt, xed, yed);
                            fp2.Write(strLine);
					        //sprintf(fp2, "    %4d %10.5f         %10.5f\n", cnt, xed, yed);
				        }

				        lastxed = xed;
				        lastyed = yed;
        				
			        }
			        else
			        {

				        if(eltype==2)
				        {
					        L1=L;
				        }
				        else if(eltype==4)
				        {
					        L2=L;
				        }

				        if(lasteltype==3)
				        {
					        cnt++;
					        if(L1==0 && L2==0)
					        {
                                strLine = string.Format("    {0,4:D} {1,10:f5}         {2,10:f5}  {3,10:f5}\n", cnt, lastipx, lastipy, lastRA);
                                fp2.Write(strLine);
						        //sprintf(fp2, "    %4d %10.5f         %10.5f  %10.5f\n", cnt, IPX, IPY, lastRA);
					        }
					        else if(L1 == 0 && L2 > 0)
					        {
                                strLine = string.Format("    {0,4:D} {1,10:f5}         {2,10:f5}  {3,10:f5}   {4,10:f5}\n", cnt, IPX, IPY, lastRA, L2);
                                fp2.Write(strLine);
						        //sprintf(fp2, "    %4d %10.5f         %10.5f  %10.5f   %10.5f\n", cnt, IPX, IPY, lastRA, L2);
					        }
					        else if(L1 > 0 && L2 == 0)
					        {
                                strLine = string.Format("    {0,4:D} {1,10:f5}         {2,10:f5}  {3,10:f5}   {4,10:f5}\n", cnt, lastipx, lastipy, lastRA, L1);
                                fp2.Write(strLine);
						        //fprintf(fp2, "    %4d %10.5f         %10.5f  %10.5f   %10.5f\n", cnt, IPX, IPY, lastRA, L1);
					        }
					        else if(L1 > 0 && L2 > 0)
					        {
                                strLine = string.Format("    {0,4:D} {1,10:f5}         {2,10:f5}  {3,10:f5}   {4,10:f5}   {5,10:f5}\n", cnt, IPX, IPY, lastRA, L1, L2);
                                fp2.Write(strLine);
						        //fprintf(fp2, "    %4d %10.5f         %10.5f  %10.5f   %10.5f   %10.5f\n", cnt, IPX, IPY, lastRA, L1, L2);
					        }

					        L1=0.0;
					        L2=0.0;

				        }
        				

				        lastipx = IPX;
				        lastipy = IPY;
				        lasteltype=eltype;
				        lastRA = RA;
			        }
                    ipindex=1;
                    lastelno=elno;
                }
            }

            if (IPX != 0.0 && IPY != 0.0)
            {
                cnt++;
                strLine = string.Format("    {0,4:D} {1,10:f5}         {2,10:f5}\n\n\n", cnt, xed, yed);
                fp2.Write(strLine);
                //fprintf(fp2, "    %4d %10.5f         %10.5f\n\n\n", cnt, xed, yed);
            }

            fp2.Write(" END OF HORIZONTAL ALIGNMENT DESIGN REPORT--------------------------------------------------- .\n");
            fp1.Close();
        }

        void writeAlignment()
        {
            int ival;
            //double tb;
            double val;
            string radcurve;
            string PTS = string.Empty;
            string strLine;

            if (elno == 0)
            {
                PTS = "START POINT - SA";
                if (eltype == 1)
                    radcurve = "INFINITY";
                else
                {
                    if (turn == 1)
                        radcurve = string.Format("-{0:f3}", RA);
                    else
                        radcurve = string.Format("{0:f3}", RA);
                }

                strLine = string.Format(" {0}  {1,10:f3} {2,15:f3} {3,15:f3}  {4,6:D} {5,2:D} {6,6:f2} {7,12}\n"
                    , PTS, chn, ex, ey, dg, mn, sc, radcurve);
                fp2.Write(strLine);
                //fprintf(fp2, " %s  %10.3f %15.3f %15.3f  %6d %2d %6.2f %12s\n", PTS, chn, ex, ey, dg, mn, sc, radcurve);
                elno = 1;                
            }
            else
            {
                if (eltype != last_eltype)
                {
                    if (last_eltype == 1)
                        radcurve = "INFINITY";
                    else
                        radcurve = string.Format("{0:f3}", last_RA);
                    if (eltype == 1)
                    {
                        if (last_eltype == 4)
                            PTS = "TANGENT POINT-ST";
                        else if (last_eltype == 3)
                            PTS = "TANGENT POINT-CT";
                        else if (last_eltype == 1)
                        {
                            PTS = "TANGENT POINT-SS";
                            radcurve = "INFINITY";
                        }
                        else if (last_eltype == 2)
                            PTS = "LEAD-TO-TANGENT";
                        else if (last_eltype == 0)
                            PTS = "START POINT - SA";

                        radcurve = "INFINITY";
                        strLine = string.Format(" {0}  {1,10:f3} {2,15:f3} {3,15:f3}  {4,6:D} {5,2:D} {6,6:f2} {7,12}\n", PTS, chn, ex, ey, dg, mn, sc, radcurve);
                        fp2.Write(strLine);
                    }
                    else if (eltype == 2)
                    {
                        if (last_eltype == 1)
                        {
                            PTS = "TANGENT POINT-TS";
                            radcurve = "INFINITY";
                        }
                        else if (last_eltype == 3)
                            PTS = "CIRCULAR-TO-LEAD";
                        else if (last_eltype == 4)
                            PTS = "TANGENT POINT-SS";
                        else if (last_eltype == 0)
                            PTS = "START POINT - SA";

                        if (last_eltype != -999)
                        {
                            strLine = string.Format(" {0}  {1,10:f3} {2,15:f3} {3,15:f3}  {4,6:D} {5,2:D} {6,6:f2} {7,12}\n", PTS, chn, ex, ey, dg, mn, sc, radcurve);                        
                            fp2.Write(strLine);
                        }

                        radcurve = string.Format("{0:f3}", RA);
                        strLine = string.Format(" {0}  {1,10:f3} {2,15:f3} {3,15:f3}  {4,6:D} {5,2:D} {6,6:f2} {7,12}\n", PTS, chn, ex, ey, dg, mn, sc, radcurve);
                        fp2.Write(strLine);

                    }
                    else if (eltype == 3)
                    {
                        if (last_eltype == 1)
                        {
                            PTS = "TANGENT POINT-TC";
                            radcurve = "INFINITY";
                        }
                        else if (last_eltype == 2)
                            PTS = "TANGENT POINT-SC";
                        else if (last_eltype == 3)
                            PTS = "TANGENT POINT-CC";
                        else if (last_eltype == 4)
                            PTS = "TRAIL-TO-CIRCULAR";
                        else if (last_eltype == 0)
                            PTS = "START POINT - SA";

                        if (last_eltype != -999)
                        {
                            //fprintf(fp2, " %s  %10.3f %15.3f %15.3f  %6d %2d %6.2f %12s\n", PTS, chn, ex, ey, dg, mn, sc, radcurve);
                            strLine = string.Format(" {0}  {1,10:f3} {2,15:f3} {3,15:f3}  {4,6:D} {5,2:D} {6,6:f2} {7,12}\n", PTS, chn, ex, ey, dg, mn, sc, radcurve);
                            fp2.Write(strLine);
                        }

                        radcurve = string.Format("{0:f3}", last_RA);
                        //fprintf(fp2, " %s  %10.3f %15.3f %15.3f  %6d %2d %6.2f %12s\n", PTS, chn, ex, ey, dg, mn, sc, radcurve);
                        strLine = string.Format(" {0}  {1,10:f3} {2,15:f3} {3,15:f3}  {4,6:D} {5,2:D} {6,6:f2} {7,12}\n", PTS, chn, ex, ey, dg, mn, sc, radcurve);
                        fp2.Write(strLine);
                    }

                    else if (eltype == 4)
                    {
                        if (last_eltype == 1)
                        {
                            PTS = "TANGENT-TO-TRAIL";
                            radcurve = "INFINITY";
                        }
                        else if (last_eltype == 2)
                            PTS = "TANGENT POINT-SS";
                        else if (last_eltype == 3)
                            PTS = "TANGENT POINT-CS";
                        else if (last_eltype == 0)
                            PTS = "START POINT - SA";

                        if (last_eltype != -999)
                        {
                            //fprintf(fp2, " %s  %10.3f %15.3f %15.3f  %6d %2d %6.2f %12s\n", PTS, chn, ex, ey, dg, mn, sc, radcurve);
                            strLine = string.Format(" {0}  {1,10:f3} {2,15:f3} {3,15:f3}  {4,6:D} {5,2:D} {6,6:f2} {7,12}\n", PTS, chn, ex, ey, dg, mn, sc, radcurve);
                            fp2.Write(strLine);
                        }

                        radcurve = string.Format("{0:f3}", last_RA);
                        strLine = string.Format(" {0}  {1,10:f3} {2,15:f3} {3,15:f3}  {4,6:D} {5,2:D} {6,6:f2} {7,12}\n", PTS, chn, ex, ey, dg, mn, sc, radcurve);
                        fp2.Write(strLine);
                        //fprintf(fp2, " %s  %10.3f %15.3f %15.3f  %6d %2d %6.2f %12s\n", PTS, chn, ex, ey, dg, mn, sc, radcurve);

                    }                   

                }
                else
                {

                    if (spchn_flag == 1)
                        PTS = "SPECIAL CHAINAGE";
                    else
                        PTS = string.Format("{0:D}", elno);


                    if (eltype == 1)
                        radcurve = "INFINITY";
                    else
                        radcurve = string.Format("{0:f3}", RA);


                    ival = (int)chn;
                    val = chn - ival;
                    if (chn != last_chn)
                    {
                        if (spchn_flag == 0 && val == 0)
                        {
                            strLine = string.Format("             {0,4}  {1,10:f3} {2,15:f3} {3,15:f3}  {4,6:D} {5,2:D} {6,6:f2} {7,12}\n", PTS, chn, ex, ey, dg, mn, sc, radcurve);
                            fp2.Write(strLine);
                            //fprintf(fp2, "             %4s  %10.3f %15.3f %15.3f  %6d %2d %6.2f %12s\n", PTS, chn, ex, ey, dg, mn, sc, radcurve);
                        }

                        else if (spchn_flag == 1)
                        {
                            strLine = string.Format(" {0}  {1,10:f3} {2,15:f3} {3,15:f3}  {4,6:D} {5,2:D} {6,6:f2} {7,12}\n", PTS, chn, ex, ey, dg, mn, sc, radcurve);
                            fp2.Write(strLine);
                            //fprintf(fp2, " %s  %10.3f %15.3f %15.3f  %6d %2d %6.2f %12s\n", PTS, chn, ex, ey, dg, mn, sc, radcurve);
                        }
                    }               

                }
            }
        }

        void element_summary()
        {
            int cnt;
            string radcurve1 = string.Empty, radcurve2 = string.Empty, last_modnam, last_stglbl;
            string modnam1, stglbl1;
            string strLine = string.Empty;

            pathfile = Path.Combine(path, "HALIGN.FIL");

            if (File.Exists(pathfile) == false)
            {
                System.Windows.Forms.MessageBox.Show("File HALIGN.FIL not found...!", HeadsUtils.Constants.ProductName);
                return;
            }
            fp1 = new StreamReader(pathfile);

            cnt = 0;
            last_chn = 0;
            last_modnam = "";
            last_stglbl = "";

            while (fp1.EndOfStream == false)
            {
                strLine = fp1.ReadLine();
                CHalignFilData fildata = CHalignFilData.Parse(strLine);

                modnam1 = fildata.sMod;
                stglbl1 = fildata.sString;
                elno = (short)fildata.iSlno;
                eltype = fildata.iEltype;
                stchn = fildata.dStartchn;
                endchn = fildata.dEndchn;
                IPX = fildata.dHipx;
                IPY = fildata.dHipy;
                T1 = fildata.dT1;
                T2 = fildata.dT2;
                rad = fildata.dRadius;
                L = fildata.dEllength;
                xst = fildata.dStartx;
                yst = fildata.dStarty;
                xed = fildata.dEndx;
                yed = fildata.dEndy;
                B1 = fildata.dB1;
                B2 = fildata.dB2;
                del = fildata.dDeviation;
                turn = (short)fildata.iTurn;

                // fscanf(fp1,"%s %s %i %i", modnam1, stglbl1, &elno, &eltype);
                //fscanf(fp1,"%lf %lf %lf %lf", &stchn, &endchn, &IPX, &IPY);
                //fscanf(fp1,"%lf %lf %lf %lf", &T1, &T2, &rad, &L);
                //fscanf(fp1,"%lf %lf %lf %lf", &xst, &yst, &xed, &yed);
                //fscanf(fp1,"%lf %lf %lf %i", &B1, &B2, &del, &turn);

                if (modnam1.ToLower() != modnam.ToLower() || stglbl1.ToLower() != stglbl.ToLower())
                    continue;

                if (endchn == last_chn)
                    continue;

                if (cnt == 0)
                {
                    fp2.Write(string.Format("  MODEL     {0}\n", modnam));
                    fp2.Write(string.Format("  STRING    {0}\n", stglbl));
                    fp2.Write("\n");
                }


                if (turn == 1) 
                    radc = -rad;
                else 
                    radc = rad;

                switch (eltype)
                {
                    case 1:
                        radcurve1 = "INFINITY";
                        radcurve2 = "INFINITY";
                        break;
                    case 2:
                        radcurve1 = "INFINITY";
                        radcurve2 = string.Format("{0:f3}", radc);
                        break;
                    case 3:
                        radcurve1 = string.Format("{0:f3}", radc);
                        radcurve2 = string.Format("{0:f3}", radc);
                        break;
                    case 4:
                        radcurve1 = string.Format("{0:f3}", radc);
                        radcurve2 = "INFINITY";
                        break;
                    default:
                        break;
                }
                if(eltype==3)
	            {
		            brg=B1;
		            deg_min_sec();
                    strLine = string.Format("  START  {0,10:f3} {1,15:f3} {2,15:f3}  {3,4:D} {4,2:D} {5,6:f2}\n", stchn, xst, yst, dg, mn, sc);
                    fp2.Write(strLine);
		            //fprintf(fp2,"  START  %10.3f %15.3f %15.3f  %4d %2d %6.2f\n", stchn, xst, yst, dg, mn, sc);

		            brg=B2;
		            deg_min_sec();
                    strLine = string.Format("  END    {0,10:f3} {1,15:f3} {2,15:f3}  {3,4:D} {4,2:D} {5,6:f2}\n", endchn, xed, yed, dg, mn, sc);
		            fp2.Write(strLine);
                    //fprintf(fp2,"  END    %10.3f %15.3f %15.3f  %4d %2d %6.2f\n", endchn, xed, yed, dg, mn, sc);

                    get_center();
                    strLine = string.Format("  CENTER    {0,23:f3} {1,15:f3}  {2,27:f3}  {3,8:f3}\n", xc, yc, radc, L);
                    fp2.Write(strLine);
		            //fprintf(fp2,"  CENTER    %23.3f %15.3f  %27.3f  %8.3f\n", xc, yc, radc, L);
	            }
	            else
	            {
		            brg=B1;
		            deg_min_sec();

                    strLine = string.Format("  START  {0,10:f3} {1,15:f3} {2,15:f3}  {3,4:D} {4,2:D} {5,6:f2} {6,12}\n", stchn, xst, yst, dg, mn, sc, radcurve1);
                    fp2.Write(strLine);
		            //fprintf(fp2,"  START  %10.3f %15.3f %15.3f  %4d %2d %6.2f %12s\n", stchn, xst, yst, dg, mn, sc, radcurve1);

		            brg=B2;
		            deg_min_sec();
                    strLine = string.Format("  END    {0,10:f3} {1,15:f3} {2,15:f3}  {3,4:D} {4,2:D} {5,6:f2} {6,12}  {7,8:f3}\n", endchn, xed, yed, dg, mn, sc, radcurve2, L);
                    fp2.Write(strLine);
		            //fprintf(fp2,"  END    %10.3f %15.3f %15.3f  %4d %2d %6.2f %12s  %8.3f\n", endchn, xed, yed, dg, mn, sc, radcurve2, L);
	            }

	            fp2.Write("\n\n");

	            cnt++;
                last_modnam = modnam;
                last_stglbl = stglbl;
	            last_chn=endchn;
            }      /* while feof */
            fp1.Close();
        }

        void get_center()
        {
            double xm, ym, th1, th2 = 0, d, d2, a;

            xm = (xst + xed) / 2.0;
            ym = (yst + yed) / 2.0;

            if (xst == xed) xed += 0.001;

            th1 = Math.Atan((yed - yst) / (xed - xst));
            th1 *= r9;

            if (turn == 1) th2 = th1 + 90;
            else if (turn == 2) th2 = th1 - 90;

            d = Math.Sqrt((xst - xed) * (xst - xed) + (yst - yed) * (yst - yed));
            d2 = d / 2.0;

            a = Math.Sqrt(radc * radc - d2 * d2);

            xc = xm + a * Math.Cos(th2 / r9);
            yc = ym + a * Math.Sin(th2 / r9);

        }

        void DisplayScroll(string str)
        {
            if (this.OnShowModelInfo != null)
            {
                this.OnShowModelInfo(str);
            }
        }
        #endregion
    }
}
