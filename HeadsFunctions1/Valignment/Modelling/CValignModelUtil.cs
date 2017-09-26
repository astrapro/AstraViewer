using System;
using System.Collections.Generic;
using System.Text;
using HeadsUtils.Interfaces;
using HeadsUtils;
using System.IO;
using System.Windows.Forms;
namespace HeadsFunctions1.Valignment.Modelling
{
    partial class CValignModelUtil
    {
        StreamReader fp1;
        //BinaryReader fp11;
        BinaryWriter fp12;
  

        //FILE* fp2;
        StreamWriter fp3;
        //FILE* fp4;
        //FILE* fpt;
        //FILE* tmpFile;

        double[] vip = new double[5000];

        double[] level = new double[5000];
        double[] vcl1 = new double[5000];
        double[] vcl2 = new double[5000];
        double[] grad = new double[5000];
        double[] bvc = new double[5000];
        double[] evc = new double[5000];
        double[] bvclev = new double[5000];
        double[] evclev = new double[5000];
        double[] ybvc = new double[5000];
        double[] yevc = new double[5000];
        double[] acumch = new double[5000];
        double[] acumdist = new double[5000];
        double[] lev = new double[5000];
        double[] clev = new double[5000];
        double[] mv = new double[5000];
        double[] delta = new double[5000];
        double gdist, glevel, yval1, yval2, vchain;
        double modchn, modx, mody, modz, lastchn, ftl, cint, grad_diff;
        short recno, opnum, fincode, no1, no2, endflag, len;
        short item, i, j, k, l, m, x, y, z, q, ind, ok;//,ls1, ls2, count, 
        CLabtype.Type label;
        short[] xend = new short[5000];
        short[] yend = new short[5000];
        short[] zend = new short[5000];
        short[] qend = new short[5000];
        short a, b, c, d;//, u, page;
        double aa, bb, cc, dd, gchain, aglevel, next, ydist1, ydist2, vdist, diff;
        //string fname, outfil, tt, te, uline, rec, valfil;
        string day, mon, date, hour, year, gradmod;//cal, 
        string str1, str2, tmod, tstr;
        string modnam, stglbl, usermod, userstg;
        //string msgstr;
        double cgr, rad, kval, kv1, kv2, SC, EC, endchn, endlev;//, l1, l2, last_chn;
        long ufact;

        double dUserInc;
        double st_chn;

        //long loc1, loc2;

        string  path, pathfile;
        //string filstr,drive, dir, file, ext;
        
        public delegate void ShowDataEvent(string strText);
        public event ShowDataEvent OnShowModelInfo = null;

        public CValignModelUtil()
        {
        }

        void DisplayScroll(string str)
        {
            if (this.OnShowModelInfo != null)
            {
                this.OnShowModelInfo(str);
            }
        }

        string[] RemoveEmptyElements(string[] arrsrc)
        {
            List<string> desti = new List<string>();
            foreach(string str in arrsrc)
            {
                if (str.Trim() != "")
                    desti.Add(str);
            }
            return desti.ToArray();
        }

        public void Funcmain(string argv1, CValignInfo info, double dInterval)
        {
            //ValMod1type valmod1;
            endflag = 0;

            path = argv1;

            usermod = info.ModelName;
            userstg = info.StringLabel;
            dUserInc = dInterval;

            process_valmod();

            report_message();

            //string[] tmpfilesarr = Directory.GetFiles(path, "*.TMP");
            //foreach (string strTmpFile in tmpfilesarr)
            //{
            //    ViewerUtils.DeleteFileIfExists(strTmpFile);
            //}
        }
        
        void process_valmod()
        {
            string[] paramarr = null; 
            short elno, eltype, turn;
            double sc, ec, t1, t2, ipx = 0, ipy = 0, l1 = 0, l2 = 0, xs = 0, ys = 0, xe = 0, ye = 0, g1 = 0, g2 = 0, del = 0;
            double last_sc, last_ec;//last_ipx, last_ipy,	
        	
            pathfile = Path.Combine(path, "VALIGN.FIL");
            if(File.Exists(pathfile) == false)
            {
                System.Windows.Forms.MessageBox.Show("INPUT FILE NOT FOUND", HeadsUtils.Constants.ProductName);
                return;
            }
            fp1 = new StreamReader(pathfile);
           
            // New read VALIGN.FIL database file and make arrays 27/2/02
            // Modified on 27(28)/02/2002
        	
            //last_ipx = -999.999;
            //last_ipy = -999.999;
            last_sc = -999.999;
            last_ec = -999.999;

            item=0;
            SC=999999999999.999;
            EC=-999999999999.999;
            cint = dUserInc;

            string storeModel = usermod, storeString = userstg;
            
            
            //System.Windows.Forms.MessageBox.Show(("Locating...5");
            while(! fp1.EndOfStream)
            {
                CValignFilData vfildata = CValignFilData.Parse(fp1.ReadLine());
                modnam = vfildata.modnam;
                stglbl = vfildata.stglbl;
                elno = 	 (short)vfildata.elno;
                eltype = (short)vfildata.eltype;
                sc = 	 vfildata.chn1;
                ec = 	 vfildata.chn2;
                ipx = 	 vfildata.hipx;
                ipy = 	 vfildata.hipy;
                t1 = 	 vfildata.t1;
                t2 = 	 vfildata.t2;
                l1 = 	 vfildata.rad;
                l2 = 	 vfildata.l;
                xs = 	 vfildata.xs;
                ys = 	 vfildata.ys;
                xe = 	 vfildata.xe;
                ye = 	 vfildata.ye;
                g1 = 	 vfildata.b1;
                g2 = 	 vfildata.b2;
                del = 	 vfildata.del;
                turn = (short)vfildata.turn;
                //fscanf(fp1, "%s %s %i %i %lf %lf %lf %lf %lf %lf %lf %lf %lf %lf %lf %lf %lf %lf %lf %i", modnam, stglbl, &elno, &eltype, &sc, &ec, &ipx, &ipy, &t1, &t2, &l1, &l2, &xs, &ys, &xe, &ye, &g1, &g2, &del, &turn);
                if(sc==last_sc && ec == last_ec)
                    break;

                if(modnam == usermod && stglbl == userstg)
                {
                    if(SC>sc)
                        SC=sc;
        			
                    if(EC<ec)
                        EC=ec;

                    if(item==0)
                    {
                        vip[item] = xs;
                        level[item] = ys;
                        vcl1[item]=0.0;
                        vcl2[item]=0.0;
                        item++;
                    }

                    if(eltype == 3) // take only curve elements
                    {
                        vip[item] = ipx;
                        level[item] = ipy;
                        vcl1[item]=l1;
                        vcl2[item]=l2;
                        item++;
                    }
        			
                }
        		
                last_sc = sc;
                last_ec = ec;
            }

            fp1.Close();// Sandipan 11/06/2004 close VALIGN.FIL

            // last straight element
            vip[item] = ipx;
            level[item] = ipy;
            vcl1[item]=l1;
            vcl2[item]=l2;
            item++;

            vip[item] = xe;
            level[item] = ye;
            vcl1[item]=0.0;
            vcl2[item]=0.0;
            //item++;
            // value of "item" not to be incremented
	
            // Use the arrays for checking Curve overlaps, Grade calculations & VCL splittings
        		
            string msgstr;

            for (i=0;i<item;i++)
            {
                if(i>0)
                {
                    if (vip[i]<=vip[i-1])
                    {
                        msgstr = string.Format("CHAINAGE OUT OF ORDER, ERROR AT LINE {0,4:D} : Ch.{1:f3}", i, vip[i]);
                       // sprintf(msgstr, "CHAINAGE OUT OF ORDER, ERROR AT LINE %4d : Ch.%.3f",i,vip[i]);
                        System.Windows.Forms.MessageBox.Show(msgstr, HeadsUtils.Constants.ProductName);
                        return;
                    }
                    grad[i]=((level[i]-level[i-1])/(vip[i]-vip[i-1]))/100;
                }

                if(vcl2[i]==0) 
                    vcl2[i] = vcl1[i] = vcl1[i]/2.0;               /*****/

                if(i==0 || i==item-1)
                { 
                    vcl1[i]=0.0;
                    vcl2[i]=0.0; 
                    grad[i]=0.0;
                }
        	
            }

	
            for (l=0;l<item;l++)
            {
                bvc[l]=vip[l]-vcl1[l];         
                evc[l]=vip[l]+vcl2[l];
                ybvc[l]=vcl1[l]*grad[l]*100;   
                yevc[l]=vcl2[l]*grad[l+1]*100;

                if(grad[l]>=0)  
                    bvclev[l]=level[l]-ybvc[l];         /*****/
                else if(grad[l]<0)  
                    bvclev[l]=level[l]-ybvc[l];

                if(grad[l+1]>=0)  
                    evclev[l]=level[l]+yevc[l];       /*****/
                else if(grad[l+1]<0)  
                    evclev[l]=level[l]+yevc[l];
            }


            //	Sandipan 11/06/2004
            fp3 = new StreamWriter(Path.Combine(path , "INP_VALIGN.TMP"), false);

            //time(&ts);
            //ptm=ctime(&ts);
            tmp_time();
            ////	Sandipan 11/06/2004


		
            double vcl;
            double length;
            modnam = storeModel;
            stglbl = storeString;

            //fprintf(fp3,"\n\n\n");
            //fprintf(fp3,"Input Data for Major Option 300 VALIGN\n\n\n");
            string strTemp;
            strTemp = "HEADS\n";
            fp3.Write(strTemp);
            strTemp = "300 VALIGN\n";
            fp3.Write(strTemp);
            strTemp = string.Format("301 MODEL {0} STRING {1}\n", modnam, stglbl);
            fp3.Write(strTemp);
            strTemp = string.Format("302 VIP {0:D} SC {1:f5} EC {2:f5} IN {3:f3}\n", item, vip[0], vip[item-1], cint);
            fp3.Write(strTemp);
            strTemp = string.Format("303 {0,15:f5} {1,15:f5}\n", vip[0], level[0]);
            fp3.Write(strTemp);


            for (i=1;i<item-1;i++)
            {
                //fprintf(fp3, "303 %15.5f %12.5f %10.3f %10.3f\n", vip[i], level[i], vcl1[i], vcl2[i]);
                strTemp = string.Format("303 {0,15:f5} {1,12:f5} {2,10:f3} {3,10:f3}\n"
                    , vip[i], level[i], vcl1[i], vcl2[i]);
                fp3.Write(strTemp);
            }

             //fprintf(fp3, "303 %15.5f %15.5f\n", vip[item-1], level[item-1]);
            strTemp = string.Format("303 {0,15:f5} {1,15:f5}\n", vip[item-1], level[item-1]);
            fp3.Write(strTemp);

            fp3.Write("FINISH\n");
            fp3.Close();

            pathfile = Path.Combine(path, "INP_VALIGN.TMP");

            if (File.Exists(pathfile) == false)
            {
                msgstr = string.Format("INPUT FILE  {0}  NOT FOUND", pathfile);
                System.Windows.Forms.MessageBox.Show(msgstr, HeadsUtils.Constants.ProductName);
                return;
            }

            fp1 = new StreamReader(pathfile);
            pathfile = Path.Combine(path, "VALIGNMENT.REP");

            fp3 = new StreamWriter(pathfile);

            //strcpy(pathfile, path);
            ////strcat(pathfile, "VALIGN.FIL");
            ////strcat(pathfile, "VALDAT.FIL");			// Modified on 17&18/02/2002
            ////fp2=fopen(pathfile, "w");

            //strcpy(pathfile, path);
            //strcat(pathfile, "VALIGNMENT.REP");
            //fp3=fopen(pathfile, "a+");

            //time(&ts);
            //ptm=ctime(&ts);

            //tmp_time();
        
            strTemp = fp1.ReadLine();

            if(strTemp.Trim() != "HEADS")
            {
                System.Windows.Forms.MessageBox.Show("User input data is not in format", HeadsUtils.Constants.ProductName);
                return;
            }

            strTemp = fp1.ReadLine();
            paramarr = strTemp.Split(new char[]{' '});
            opnum = short.Parse(paramarr[0]);
            str1 = paramarr[1]; 


            //fscanf(fp1,"%i %i %s", &recno, &opnum, str1);
        //	fscanf(fp1,"%i %s", &opnum, str1);

            if(opnum != 300)
            {
                System.Windows.Forms.MessageBox.Show("User input data is not in format", HeadsUtils.Constants.ProductName);
                return;
            }
        	
            if(str1.StartsWith("VAL") == false)
            {
                System.Windows.Forms.MessageBox.Show("User input data is not in format");
                return;
            }

            strTemp = fp1.ReadLine();
            paramarr = strTemp.Split(new char[]{' '});
            opnum = short.Parse(paramarr[0]);
            str1 = paramarr[1]; 
            modnam = paramarr[2];
            str2 = paramarr[3];
            stglbl = paramarr[4];

            ////fscanf(fp1,"%i %i %s %s %s %s", &recno, &opnum, str1, modnam, str2, stglbl);
            //fscanf(fp1,"%i %s %s %s %s", &opnum, str1, modnam, str2, stglbl);
        	
            if(opnum != 301)
            {
                System.Windows.Forms.MessageBox.Show("User input data is not in format", HeadsUtils.Constants.ProductName);
                return ;
            }

            //*if(strncmpi(str1,"MOD",3))
            if(str1.StartsWith("MOD") == false)
            {
                System.Windows.Forms.MessageBox.Show("User input data is not in format");
                return;
            }

            //*if(strncmpi(str2,"STR",3))
            if(str2.StartsWith("STR") == false)
            {
                System.Windows.Forms.MessageBox.Show("User input data is not in format", HeadsUtils.Constants.ProductName);
                return ;
            }

            if(modnam.Length > 8)
            {
                modnam = modnam.Substring(0, 8);
            }

            //302 VIP 5 SC -1.80303 EC 9.22727 IN 5.000
            strTemp = fp1.ReadLine();
            paramarr = strTemp.Split(new char[]{' '});
            opnum = short.Parse(paramarr[0]);

            //fscanf(fp1,"%i", &opnum);

            if(opnum != 302)
            {
                System.Windows.Forms.MessageBox.Show("User input data is not in format", HeadsUtils.Constants.ProductName);
                return;
            }

            
            str1 = paramarr[1];
            item = short.Parse(paramarr[2]);

            //fscanf(fp1,"%s %i", str1, &item);
            if( str1.StartsWith("VIP") == false)
            {
                System.Windows.Forms.MessageBox.Show("User input data is not in format", HeadsUtils.Constants.ProductName);
                return;
            }

            str1 = paramarr[3];
            SC = double.Parse(paramarr[4]);
            //fscanf(fp1,"%s %lf", str1, &SC);

            if(str1.StartsWith("SC") == false)
            {
                System.Windows.Forms.MessageBox.Show("User input data is not in format", HeadsUtils.Constants.ProductName);
                return;
            }

            //fscanf(fp1,"%s %lf", str1, &EC);
            str1 = paramarr[5];
            EC = double.Parse(paramarr[6]);


            //*if(strcmpi(str1,"EC"))
            if( str1.StartsWith("EC") == false)
            {
                System.Windows.Forms.MessageBox.Show("User input data is not in format", HeadsUtils.Constants.ProductName);
                return ;
            }

            str1 = paramarr[7];
            cint = double.Parse(paramarr[8]);

            //fscanf(fp1,"%s %lf", str1, &cint);

            if(str1.StartsWith("IN") == false)
            {
                System.Windows.Forms.MessageBox.Show("User input data is not in format", HeadsUtils.Constants.ProductName);
                return;
            }

            //fgets(uline,100,fp1);
            //fp1.ReadLine();
            j=0;
            fincode=0;


            while(fp1.EndOfStream == false)
            {
                str1 = fp1.ReadLine();
		        
                //if(! strncmpi(uline," FIN",3))
                if(str1.StartsWith("FIN") == true)
                {
                    fincode=1;
                    break;
                }
                j++;
            }


            if(j < item)
            {
                msgstr = string.Format("TOTAL No. OF VIPs LESS THAN SPECIFIED  {0:D}..", item);
                System.Windows.Forms.MessageBox.Show(msgstr, HeadsUtils.Constants.ProductName);
                //error_msg();
            }

            if(j > item)
            {
                msgstr = string.Format("TOTAL No. OF VIPs MORE THAN SPECIFIED  %d..", item);
                System.Windows.Forms.MessageBox.Show(msgstr, HeadsUtils.Constants.ProductName);
                //error_msg();
            }

            if(fincode == 0)
            {
                //*clrscr();
                msgstr = string.Format("In User Input Data FINish  statement is   not  found..");
                System.Windows.Forms.MessageBox.Show(msgstr, HeadsUtils.Constants.ProductName);
                //error_msg();
            }

            fp1.Close();
            fp1 = new StreamReader(Path.Combine(path, "INP_VALIGN.TMP"));

            fp1.ReadLine();
            while(true)
            {
                strTemp = fp1.ReadLine();
                paramarr = strTemp.Split(new char[]{' '});
                //fscanf(fp1,"%s", str1);
                opnum = short.Parse(paramarr[0]);
                if(opnum == 302)
                {
                    break;
                }
             }

            for (i=0 ; i< item ; i++)
            {
                //fgets(uline,100,fp1);

                strTemp = fp1.ReadLine();
                //*if(! strncmpi(uline,"FIN",3))
                if(strTemp.StartsWith("FIN") == true)
                {
                    break;
                }
                vip[i] = new double(); 
                level[i]= new double();
                vcl1[i]= new double();
                vcl2[i]= new double();

                recno = opnum = 0;
                vip[i] = level[i] = vcl1[i] = vcl2[i] = 0;

                paramarr = this.RemoveEmptyElements(strTemp.Split(new char[]{' '}));
                opnum = short.Parse(paramarr[0]); 
                vip[i] = double.Parse(paramarr[1]); 
                level[i]= double.Parse(paramarr[2]);
                if (paramarr.Length == 5)
                {
                    vcl1[i] = double.Parse(paramarr[3]);
                    vcl2[i] = double.Parse(paramarr[4]);
                }
                

                //sscanf(uline,"%d %d %lf %lf %lf %lf",&recno, &opnum, &vip[i],&level[i],&vcl1[i],&vcl2[i]);
                //sscanf(uline,"%d %lf %lf %lf %lf",&opnum, &vip[i],&level[i],&vcl1[i],&vcl2[i]);

                if(i>0)
                {
                    if (vip[i]<=vip[i-1])
                    {
                        msgstr = string.Format("CHAINAGE OUT OF ORDER, ERROR AT LINE {0,4:D} : Ch.{1:f3}", i, vip[i]);
                        System.Windows.Forms.MessageBox.Show(msgstr, HeadsUtils.Constants.ProductName);
                        //error_msg();
                    }
                    grad[i]=((level[i]-level[i-1])/(vip[i]-vip[i-1]))/100;
                }

                if(vcl2[i]==0) 
                {
                    vcl2[i] = vcl1[i] = vcl1[i]/2.0;               /*****/
                }
                if(i==0 || i==item-1)
                { 
                    vcl1[i]=0.0;
                    vcl2[i]=0.0; 
                }
                if(i==0)
                { 
                    grad[i]=0.0;
                }
            }

	
            for (l = 0; l < item; l++)
            {
                bvc[l]=new double();         
                evc[l]=new double();
                ybvc[l]=new double();   
                yevc[l]=new double();
                bvclev[l]=new double();
                evclev[l]=new double();

                bvc[l]=vip[l]-vcl1[l];         
                evc[l]=vip[l]+vcl2[l];
                ybvc[l]=vcl1[l]*grad[l]*100;   
                yevc[l]=vcl2[l]*grad[l+1]*100;

                if(grad[l]>=0)  
                    bvclev[l]=level[l]-ybvc[l];         /*****/
                else if(grad[l]<0)  
                    bvclev[l]=level[l]-ybvc[l];

                if(grad[l+1]>=0)  
                    evclev[l]=level[l]+yevc[l];       /*****/
                else if(grad[l+1]<0)  
                    evclev[l]=level[l]+yevc[l];
            }


            header();

            //write_input_rep();

            elno=1;

            for (k=0;k<item;k++)
            {
                if(k==0)
                {
                    strTemp = string.Format("VIP{0,3:D}   {1,12:f3} {2,7:f3}\n\n", k+1, bvc[k], bvclev[k]);
                    fp3.Write(strTemp);
                    /* straight grade */
                    turn=0;

                    eltype = 1; /* crest or sag curve */

                    length = bvc[k+1]-bvc[k];

                    // Modified on 18/02/2002
                    //fprintf(fp2,"%s %s %4d %4d %8.3f %15.3f %15.3f %4.1f\n", modnam, stglbl, elno, eltype, length, bvc[k], bvc[k+1], grad[k+1]*10000.0);
                    elno++;
                }

                if( k > 0 && k < item-1)
                {
                    if(bvc[k] < evc[k-1])
                    {
                        msgstr = string.Format("CURVE OVERLAPED, ERROR AT LINE {0,4:D} : Ch.{1:f3}", k+1, vip[k]);
                        System.Windows.Forms.MessageBox.Show(msgstr, HeadsUtils.Constants.ProductName);
                        //error_msg();
                    }

                    delta[k]=(grad[k+1])-(grad[k]);
                    if(delta[k] == 0.0)
                    {
                        delta[k] = 0.001;
                    }
                    mv[k]=(vcl1[k]*vcl2[k]*delta[k]) / (200*(vcl1[k]+vcl2[k]));

                    strTemp = string.Format("      GRADIENT = {0:f4} %\n\n", grad[k]*10000);
                    fp3.Write(strTemp);
                    strTemp = string.Format("BVC      {0,12:f3} {1,8:f3}\n", bvc[k], bvclev[k]);
                    fp3.Write(strTemp);

                    if(vcl1[k]!=vcl2[k])
                    {
                        kv1=(vcl1[k]*2)/(delta[k]*10000.0);
                        kv2=(vcl2[k]*2)/(delta[k]*10000.0);
                        //fprintf(fp3,"VIP%3d   %12.3f %8.3f     VCL1=%.3f, VCL2=%.3f, Mo=%.3f, k1=%.3f, k2=%.3f\n",k+1,vip[k],level[k],vcl1[k],vcl2[k],mv[k]*10000.0,kv1,kv2);
                        strTemp = string.Format("VIP{0,3:D}   {1,12:f3} {2,8:f3}     VCL1={3:f3}, VCL2={4:f3}, Mo={5:f3}, k1={6:f3}, k2={7:f3}\n", 
                            k+1, vip[k], level[k], vcl1[k], vcl2[k], mv[k]*10000.0, kv1, kv2);

                        fp3.Write(strTemp);

                        length=vcl1[k]+vcl2[k];

                        if(delta[k] < 0)/* crest curve */
                        {
                            turn=1;
                            eltype = 2;
                        }
                        else		/* sag curve */
                        {
                            turn=2;
                            eltype = 3;
                        }

                        // Modified on 18/02/2002
                        //fprintf(fp2,"%s %s %4d %4d %8.3f %15.3f %15.3f %4d\n", modnam, stglbl, elno, eltype, length, bvc[k], evc[k], turn);
                        elno++;
                    }

                    if(vcl1[k]==vcl2[k])
                    {
                        kval=(vcl1[k]+vcl2[k])/(delta[k]*10000.0);
                        //fprintf(fp3,"VIP%3d   %12.3f %8.3f      VCL=%.3f, Mo=%.3f, k=%.3f\n",k+1,vip[k],level[k],vcl1[k]*2,mv[k]*10000.0,kval);
                        strTemp = string.Format("VIP{0,3:D}   {1,12:f3} {2,8:f3}      VCL={3:f3}, Mo={4:f3}, k={5:f3}\n", 
                            k+1, vip[k], level[k], vcl1[k]*2, mv[k]*10000.0, kval);

                        fp3.Write(strTemp);

                        length=vcl1[k]+vcl2[k];

                        if(delta[k] < 0)/* crest curve */
                        {
                            turn=1;
                            eltype = 2;
                        }
                        else		/* sag curve */
                        {
                            turn=2;
                            eltype = 3;
                        }

                        eltype = 2; /* crest or sag curve */

                        // Modified on 18/02/2002
                        //fprintf(fp2,"%s %s %4d %4d %8.3f %15.3f %15.3f %4d\n", modnam, stglbl, elno, eltype, length, bvc[k], evc[k], turn);
                        elno++;
                    }

                   //fprintf(fp3,"EVC      %12.3f %8.3f\n\n",evc[k],evclev[k]);
                    strTemp = string.Format("EVC      {0,12:f3} {1,8:f3}\n\n", evc[k], evclev[k]);
                    fp3.Write(strTemp);
                }

                if(k == item-1)
                {
                    //fprintf(fp3,"      GRADIENT = %.4f %\n\n",grad[k]*10000);
                    //fprintf(fp3,"VIP%3d   %12.3f %7.3f\n",k+1,bvc[k],bvclev[k]);
                    strTemp = string.Format("      GRADIENT = {0:f4} %\n\n", grad[k]*10000);
                    fp3.Write(strTemp);
                    strTemp = string.Format("VIP{0,3:D}   {1,12:f3} {2,7:f3}\n", k+1, bvc[k], bvclev[k]);
                    fp3.Write(strTemp);
                }

                /* straight grade */
                turn=0;

                eltype = 1; /* crest or sag curve */

                if(k > 0 && bvc[k+1] > 0)
                {
                    // Modified on 18/02/2002
                    //fprintf(fp2,"%s %s %4d %4d %8.3f %15.3f %15.3f %4.1f\n", modnam, stglbl, elno, eltype, length, evc[k], bvc[k+1], grad[k+1]*10000.0);
                    elno++;
                }

            }

            //fclose(fp2);  // Modified on 18/02/2002


            write_screen();

            header3();

            st_chn=vip[0];

            for (m=0;m<item;m++)   /*Start m- loop*/
            {
                if(m==0)
                {
                    if(evc[m]>=0 && evc[m]<10) 
                        ufact=10;
                    else if(evc[m]>=10 && evc[m]<100) 
                        ufact=10;
                    else if(evc[m]>=100 && evc[m]<1000) 
                        ufact=100;
                    else if(evc[m]>=1000 && evc[m]<10000) 
                        ufact=1000;
                    else if(evc[m]>=10000 && evc[m]<100000) 
                        ufact=10000;
                    else if(evc[m]>=100000 && evc[m]<1000000) 
                        ufact=100000;
                    else if(evc[m]>=1000000 && evc[m]<10000000) 
                        ufact=100000;

                    aa=evc[m]/ufact;
                    a= (short)Math.Floor(aa);       /*Always round floating down*/
                    gchain= a * ufact;

                    //fprintf(fp3," START CHAINAGE-%12.3f %12.3f %12.3f      0.000     INFINITY\n",vip[m],level[m],grad[m+1]*10000);
                    strTemp = string.Format(" START CHAINAGE-{0,12:f3} {1,12:f3} {2,12:f3}      0.000     INFINITY\n", vip[m], level[m], grad[m+1]*10000);
                    fp3.Write(strTemp);
                }

                if(m>0 && m<item-1)
                {
                    xend[m] = (short)Math.Ceiling((vip[m] - gchain) / cint);  /*Numbers of x loop-Grade section*/

                    for(x=0;x<=xend[m]+1;x++)
                    {
                        if(x==0)
                            aglevel=evclev[m-1]-(grad[m]*100*(evc[m-1]-gchain));
                        else if(x>0)
                        {
                            next=gchain+(cint*x);   /*get next chainage*/
                            gdist=next-gchain;      /*get exact distance*/
                            glevel=aglevel+(grad[m]*100*gdist);

        					
                            if(next>evc[m-1] && next<bvc[m])
                            {
                                //fprintf(fp3,"                %12.3f %12.3f %12.3f      0.000     INFINITY\n",next,glevel,grad[m]*10000);
                                strTemp = string.Format("                {0,12:f3} {1,12:f3} {2,12:f3}      0.000     INFINITY\n", next, glevel, grad[m]*10000);
                                fp3.Write(strTemp);
                            }
                        }
                    }

                    if(bvc[m] != evc[m])
                    {
                        if((grad[m] - grad[m+1]) == 0.0)
                            grad_diff = 0.0001;
                        else
                            grad_diff = grad[m] - grad[m+1];

                        rad=(200*vcl1[m])/(grad_diff*10000);
                        //fprintf(fp3,"            BVC-%12.3f %12.3f %12.3f      0.000 %12.1f\n",bvc[m],bvclev[m],grad[m]*10000,rad*-1);
                        strTemp = string.Format("            BVC-{0,12:f3} {1,12:f3} {2,12:f3}      0.000 {3,12:f1}\n", bvc[m], bvclev[m], grad[m]*10000, rad*-1);
                        fp3.Write(strTemp);
                    }

                    if(bvc[m]>=0 && bvc[m]<10) 
                        ufact=10;
                    else if(bvc[m]>=10 && bvc[m]<100) 
                        ufact=10;
                    else if(bvc[m]>=100 && bvc[m]<1000) 
                        ufact=100;
                    else if(bvc[m]>=1000 && bvc[m]<10000) 
                        ufact=1000;
                    else if(bvc[m]>=10000 && bvc[m]<100000) 
                        ufact=10000;
                    else if(bvc[m]>=100000 && bvc[m]<1000000) 
                        ufact=100000;
                    else if(bvc[m]>=1000000 && bvc[m]<10000000) 
                        ufact=100000;

                    bb=evc[m-1]/ufact;
                    b = (short)Math.Floor(bb);          /*Always round floating down*/
                    vchain= b * ufact;    /*to get start chainage for vcl1*/

                    yend[m] = (short)Math.Ceiling((vip[m] - vchain) / cint);    /*Numbers of y loop-VCL1 section*/

                    for(y=0;y<=yend[m];y++)          /*Start of y loop*/
                    {
                        if(y==0)
                            aglevel=bvclev[m]-(grad[m]*100*(bvc[m]-vchain));
                        else if(y>0)
                        {
                            next=vchain+(cint*y);      /*get next chainage*/
                            vdist=next-bvc[m];         /*get exact distance*/
                            ydist1=mv[m]*(vdist/vcl1[m])*(vdist/vcl1[m]); /*Vert. distance*/
                            ydist1=ydist1*10000;
                            yval1=aglevel+(grad[m]*100*(next-vchain)); /*level on grade*/
                            diff=yval1+ydist1;

                            l1=next-bvc[m]; l2=evc[m]-next;
                            cgr=((grad[m]*l2)+(grad[m+1]*l1))/(l1+l2);

                            if((grad[m] - grad[m+1]) == 0.0)
                                grad_diff = 0.0001;
                            else
                                grad_diff = grad[m] - grad[m+1];

                            rad=(200*vcl1[m])/(grad_diff*10000);

					
                            if(next>bvc[m] && next<=vip[m])
                            {
                                strTemp = string.Format("                {0,12:f3} {1,12:f3} {2,12:f3} {3,10:f3} {4,12:f1}\n", next, diff, cgr * 10000, ydist1, rad * -1);
                                fp3.Write(strTemp);

                                //fprintf(fp3,"                %12.3f %12.3f %12.3f %10.3f %12.1f\n",next,diff,cgr*10000,ydist1,rad*-1);
                            }
                        }
                    }

                    if(vip[m]>=0 && vip[m]<10) 
                        ufact=10;
                    else if(vip[m]>=10 && vip[m]<100) 
                        ufact=10;
                    else if(vip[m]>=100 && vip[m]<1000) 
                        ufact=100;
                    else if(vip[m]>=1000 && vip[m]<10000) 
                        ufact=1000;
                    else if(vip[m]>=10000 && vip[m]<100000) 
                        ufact=10000;
                    else if(vip[m]>=100000 && vip[m]<1000000) 
                        ufact=100000;
                    else if(vip[m]>=1000000 && vip[m]<10000000) 
                        ufact=100000;
        			
                    cc=bvc[m]/ufact;
                    c = (short)Math.Floor(cc);          /*Always round floating down*/
                    vchain= c * ufact;    /*get start chainage for vcl2*/

                    zend[m] = (short)Math.Ceiling((evc[m] - vchain) / cint);    /*Numbers of z loop-VCL2 section*/
                    for(z=0;z<=zend[m];z++)             /*Start of z loop*/
                    {
                        if(z==0)
                        {
                            aglevel=level[m]-(grad[m+1]*100*(vip[m]-vchain));
                        }
                        else if(z>0)
                        {
                            next=vchain+(cint*z);      /*get next chainage*/
                            vdist=evc[m]-next;         /*get exact distance*/
                            ydist2=mv[m]*(vdist/vcl2[m])*(vdist/vcl2[m]); /*Vert. distance*/
                            ydist2=ydist2*10000;
                            yval2=aglevel+(grad[m+1]*100*(next-vchain)); /*level on grade*/
                            diff=yval2+ydist2;

                            l1=next-bvc[m]; l2=evc[m]-next;
                            cgr=((grad[m]*l2)+(grad[m+1]*l1))/(l1+l2);

                            if((grad[m] - grad[m+1]) == 0.0)
                                grad_diff = 0.0001;
                            else
                                grad_diff = grad[m] - grad[m+1];

                            rad=(200*vcl2[m])/(grad_diff*10000);

        					
                            if(next>vip[m] && next<evc[m])
                            {
                                //fprintf(fp3,"                %12.3f %12.3f %12.3f %10.3f %12.1f\n",next,diff,cgr*10000,ydist2,rad*-1);
                                strTemp = string.Format("                {0,12:f3} {1,12:f3} {2,12:f3} {3,10:f3} {4,12:f1}\n", next, diff, cgr *10000, ydist2, rad*-1);
                                fp3.Write(strTemp);
                            }
                        }
                    }

                    if(bvc[m]!=evc[m])
                    {
                        strTemp = string.Format("            EVC-{0,12:f3} {1,12:f3} {2,12:f3}      0.000     INFINITY\n", evc[m], evclev[m], grad[m+1]*10000);
                        fp3.Write(strTemp);
                        //fprintf(fp3,"            EVC-%12.3f %12.3f %12.3f      0.000     INFINITY\n",evc[m],evclev[m],grad[m+1]*10000);
                    }
                }

                if(m==item-1) /* Last grade calculation */
                {
                    if(evc[m-1]>=0 && evc[m-1]<10) 
                        ufact=10;
                    else if(evc[m-1]>=10 && evc[m-1]<100) 
                        ufact=10;
                    else if(evc[m-1]>=100 && evc[m-1]<1000) 
                        ufact=100;
                    else if(evc[m-1]>=1000 && evc[m-1]<10000) 
                        ufact=1000;
                    else if(evc[m-1]>=10000 && evc[m-1]<100000) 
                        ufact=10000;
                    else if(evc[m-1]>=100000 && evc[m-1]<1000000) 
                        ufact=100000;
                    else if(evc[m-1]>=1000000 && evc[m-1]<10000000) 
                        ufact=100000;

                    dd=evc[m-1]/ufact;
                    d = (short)Math.Floor(dd);       /*Always round floating down*/
                    gchain= d * ufact;

                    qend[m] = (short)Math.Ceiling((bvc[m] - gchain) / cint);    /*Numbers of q end-grade section*/
                    for(q=0;q<=qend[m];q++)             /*Start of q loop*/
                    {
                        if(q==0)
                        {
                            aglevel=evclev[m-1]-(grad[m]*100*(evc[m-1]-gchain));
                        }
                        else if(q>0)
                        {
                            next=gchain+(cint*q);   /*get next chainage*/
                            gdist=next-gchain;      /*get exact distance*/
                            glevel=aglevel+(grad[m]*100*gdist);
					
				
                            if(next>evc[m-1] && next<bvc[m])
                            {
                                strTemp = string.Format("                {0,12:f3} {1,12:f3} {2,12:f3}      0.000     INFINITY\n", next, glevel, grad[m]*10000);
                                fp3.Write(strTemp);
                                //fprintf(fp3,"                %12.3f %12.3f %12.3f      0.000     INFINITY\n",next,glevel,grad[m]*10000);
                            }
                        }
                    }

                    strTemp = string.Format("   END CHAINAGE-{0,12:f3} {1,12:f3} {2,12:f3}      0.000     INFINITY\n", bvc[m], bvclev[m], grad[m]*10000);
                    fp3.Write(strTemp);

                    //fprintf(fp3,"   END CHAINAGE-%12.3f %12.3f %12.3f      0.000     INFINITY\n",bvc[m],bvclev[m],grad[m]*10000);
                }

            } /*End of m loop*/



            //exit_rep:
            
            fp3.WriteLine("");
            fp3.WriteLine("");
            fp3.Write("-------------------- END OF DESIGN VERTICAL PROFILE ----------------------------\n");
	        
	
            //Write 300 VALIGN data

            //fprintf(fp3,"\n\n\n");
            //fprintf(fp3,"Input Data for Major Option 300 VALIGN\n\n\n");

            //fprintf(fp3,"HEADS\n");
            //fprintf(fp3,"300 VALIGN\n");
            //fprintf(fp3,"301 MODEL %s STRING %s\n", modnam, stglbl);
            //fprintf(fp3,"302 VIP %d SC %.5f EC %.5f IN %.3f\n", item, vip[0], vip[item-1], cint);

            //fprintf(fp3, "303 %15.5f %15.5f\n", vip[0], level[0]);


            strTemp = "\n\n\n";
            fp3.Write(strTemp);
            strTemp = "Input Data for Major Option 300 VALIGN\n\n\n";
            fp3.Write(strTemp);
            strTemp = "HEADS\n";
            fp3.Write(strTemp);
            strTemp = "300 VALIGN\n";
            fp3.Write(strTemp);
            strTemp = string.Format("301 MODEL {0} STRING {1}\n", modnam, stglbl);
            fp3.Write(strTemp);
            strTemp = string.Format("302 VIP {0:D} SC {1:f5} EC {2:f5} IN {3:f3}\n", item, vip[0], vip[item-1], cint);
            fp3.Write(strTemp);
            strTemp = string.Format("303 {0,15:f5} {1,15:f5}\n", vip[0], level[0]);
            fp3.Write(strTemp);

            for (i =1;i<item-1;i++)
            {

                if(vcl1[i] == vcl2[i])
                {
                    vcl = vcl1[i] + vcl2[i];
                    strTemp = string.Format("303 {0,15:f5} {1,15:f5} {2,10:f3}\n", vip[i], level[i], vcl);
                    fp3.Write(strTemp);
                }
                else
                {
                    strTemp = string.Format("303 {0,15:f5} {1,12:f5} {2,10:f3} {3,10:f3}\n", vip[i], level[i], vcl1[i], vcl2[i]);
                    fp3.Write(strTemp);
                }
            }

            strTemp = string.Format("303 {0,15:f5} {1,15:f5}\n", vip[item-1], level[item-1]);
            fp3.Write(strTemp);

            strTemp = "FINISH\n";
            fp3.Write(strTemp);            

            footer();
            fp1.Close();
            fp3.Close();

            /*If model.fil exists then*/
            write_vmodel();
        } 
        /*-------------------- END OF MAIN BLOCK ----------------------------*/
                        
        void tmp_time()
        {
            day = DateTime.Now.DayOfWeek.ToString();
            mon = DateTime.Now.Month.ToString();
            date = DateTime.Now.Day.ToString();
            hour = DateTime.Now.Hour.ToString();
            year = DateTime.Now.Year.ToString();
        }

        void write_screen()
        {
            string buffer;
            //int l=0;

            for (j=0;j<=item;j++)
            {
                if(j==0) 
                {
                    buffer = string.Format("VIP{0,3:D}   {1,12:f3} {2,8:f3}",j+1,bvc[j],bvclev[j]);
                    //sptintf(buffer , "VIP%3d   %12.3f %8.3f",j+1,bvc[j],bvclev[j]);
                    this.DisplayScroll(buffer);
                    //l = m_ModelCtrl.Items.Add(buffer);
                    //m_ModelCtrl.SelectedIndex = l;
                }


                if(j>0&&j<item-1)
                {
                    double temp;

                    temp = grad[j]*10000;

                    //sptintf(buffer , "             GRADIENT = %.4f %%",temp);
                    buffer = string.Format("             GRADIENT = {0:f4} %", temp);
                    this.DisplayScroll(buffer);
                    //l = m_ModelCtrl.Items.Add(buffer);
                    //m_ModelCtrl.SelectedIndex = l;

                    //sprintf(buffer , "BVC      %12.3f %7.3f",bvc[j],bvclev[j]);
                    buffer = string.Format("BVC      {0,12:f3} {1,7:f3}", bvc[j], bvclev[j]);
                    this.DisplayScroll(buffer);
                    //l = m_ModelCtrl.Items.Add(buffer);
                    //m_ModelCtrl.SelectedIndex = l;

                    if(vcl1[j]!=vcl2[j])
                    {
                        //sprintf(buffer , "VIP%3d   %12.3f %8.3f      VCL1=%.3f, VCL2=%.3f",j+1,vip[j],level[j],vcl1[j],vcl2[j]);
                        buffer = string.Format("VIP{0,3:D}   {1,12:f3} {2,8:f3}      VCL1={3:f3}, VCL2={4:f3}"
                            , j+1, vip[j], level[j], vcl1[j], vcl2[j]);
                        this.DisplayScroll(buffer);
                        //l = m_ModelCtrl.Items.Add(buffer);
                        //m_ModelCtrl.SelectedIndex = l;
                    }

                    if(vcl1[j]==vcl2[j])
                    {
                        //sprintf(buffer , "VIP%3d   %12.3f %8.3f      VCL =%.3f",j+1,vip[j],level[j],vcl1[j]*2);
                        buffer = string.Format("VIP{0,3:D}   {1,12:f3} {2,8:f3}      VCL ={3:f3}", j+1, vip[j], level[j], vcl1[j]*2);
                        this.DisplayScroll(buffer);
                        //l = m_ModelCtrl.Items.Add(buffer);
                        //m_ModelCtrl.SelectedIndex = l;
                    }

                    //sprintf(buffer , "EVC      %12.3f %7.3f",evc[j],evclev[j]);
                    buffer = string.Format("EVC      {0,12:f3} {1,7:f3}", evc[j], evclev[j]);
                    this.DisplayScroll(buffer);
                    //l = m_ModelCtrl.Items.Add(buffer);
                    //m_ModelCtrl.SelectedIndex = l;
                }

                if(j==item-1)
                {
                    double temp = grad[j]*1000;
                    //sprintf(buffer , "             GRADIENT = %.4f %%",temp);
                    buffer = string.Format("             GRADIENT = {0:f4} %", temp);
                    this.DisplayScroll(buffer);
                    //l = m_ModelCtrl.Items.Add(buffer);
                    //m_ModelCtrl.SelectedIndex = l;
                    //sprintf(buffer , "VIP%3d   %12.3f %8.3f",j+1,bvc[j],bvclev[j]);
                    buffer = string.Format("VIP{0,3:D}   {1,12:f3} {2,8:f3}", j+1, bvc[j], bvclev[j]);
                    this.DisplayScroll(buffer);
                    //l = m_ModelCtrl.Items.Add(buffer);
                    //m_ModelCtrl.SelectedIndex = l;
                }

            }
        }

        void chain_error()
        {
            string buffer;

            //sprintf(buffer ," ERROR LINE %4d : Ch.%.3f                   CHAINAGE OUT OF ORDER",i,vip[i]);
            buffer = string.Format(" ERROR LINE {0,4:D} : Ch.{1:f3}                   CHAINAGE OUT OF ORDER", i, vip[i]);

            MessageBox.Show(buffer, HeadsUtils.Constants.ProductName);
            throw new Exception();
        }  
      
        void curve_error()
        {
            string buffer;
            //sprintf(buffer ," ERROR LINE %4d : Ch.%.3f                   CHAINAGE OUT OF ORDER",i,vip[i]);
            buffer = string.Format(" ERROR LINE {0,4:D} : Ch.{1:f3}                   CHAINAGE OUT OF ORDER", i, vip[i]);

            MessageBox.Show(buffer, HeadsUtils.Constants.ProductName);
            throw new Exception();

        }

        void write_vmodel()
        {
            pathfile = Path.Combine(path, "MODEL.FIL");
            
            if(File.Exists(pathfile) == false)
            { 
                write_model_null();
                write_model_list();
            }
            else
            {
                List<CLabtype> listModelFil = new List<CLabtype>();
                ViewerUtils.ReadModelFilFile(Path.Combine(path, "MODEL.FIL"), ref listModelFil);

                int iPosition = -1;
                check_profile_string_exists(listModelFil, ref iPosition);
                //fp11 = new BinaryReader(new FileStream(pathfile, FileMode.Open, FileAccess.Read), Encoding.Default);

                if(ind==0)
                {
                    write_model_null();
                    write_model_list();
                }
                else
                {
                    write_profile_model(ref listModelFil, iPosition);
                    ViewerUtils.WriteModelFilFile(Path.Combine(path, "MODEL.FIL"), listModelFil);
                }

                
            }
         	
            write_model_list();
            write_grade_model();            
        }

        void check_profile_string_exists(List<CLabtype> listModelFil, ref int iPos)
        {
            //CLabtype lab = new CLabtype();
            CModType mod = new CModType();
            CStgType stg = new CStgType();
            //CPTStype pts = new CPTStype();
            //CTXTtype txt = new CTXTtype();

            ind=0;

            for (int iIndex = 0; iIndex < listModelFil.Count; iIndex++ )
            {
                
                if (listModelFil[iIndex].attr == CLabtype.Type.Model)
                {
                    mod = (CModType)listModelFil[iIndex].Tag;
                    tmod = mod.name;

                    stg = (CStgType)listModelFil[iIndex+1].Tag;
                    tstr = stg.label;

                    if ((tmod.ToLower() == modnam.ToLower()) && (tstr.ToLower() == stglbl.ToLower()))
                    {
                        ind = 1;
                        iPos = iIndex+ 2;
                        break;
                    }
                }
            }            
        }
        
        //void check_profile_string_exists()
        //{
        //    CLabtype lab = new CLabtype();
        //    CModType mod = new CModType();
        //    CStgType stg = new CStgType();
        //    CPTStype pts = new CPTStype();
        //    CTXTtype txt = new CTXTtype();

        //    ind=0;

        //    while(! feof(fp1))
        //    {
        //        fread(&lab, sizeof(char), sizeof(lab), fp1);
        //        label = lab.attr;

        //        if(label == 101)
        //        {
        //            fread(&mod, sizeof(char), sizeof(mod), fp1);
        //            strcpy(tmod,mod.nam);

        //            fread(&lab, sizeof(char), sizeof(lab), fp1);
        //            label = lab.attr;

        //            fread(&stg, sizeof(char), sizeof(stg), fp1);
        //            strcpy(tstr,stg.lbl);
        //        }
        //        else if(label == 103)
        //            fread(&pts, sizeof(char), sizeof(pts), fp1);
        //        else if(label == 104)
        //            fread(&txt, sizeof(char), sizeof(txt), fp1);
        //        else if(label == 999)
        //            continue;

        //        if((! strcmpi(tmod,modnam)) && (! strcmpi(tstr,stglbl)))
        //        {
        //            ind=1;
        //            fgetpos(fp1, &loc1);
        //            break;
        //        }
        //    }

        //}

        void write_profile_model(ref List<CLabtype> listModelFil, int iStartPos)
        {
            short end_chk;//, chk;

            CLabtype lab = new CLabtype();
            CModType mod = new CModType();
            CStgType stg = new CStgType();
            CPTStype pts = new CPTStype();
            CTXTtype txt = new CTXTtype();

            //string buffer;

            m = 0;
            ind = 0;
            lastchn = -999.999;
            end_chk = 0;

            StreamWriter tmpFile = new StreamWriter(Path.Combine(path, "SCROLL.TMP"), true);
            string strTemp = string.Format("ALIGNMENT MODEL  =  {0}   STRING  =  {1}\n", modnam, stglbl);
            tmpFile.Write(strTemp);
            tmpFile.Close();

            for(int iIndex = iStartPos; iIndex < listModelFil.Count; iIndex++)
            {
                ok = 0;
                lab = listModelFil[iIndex];
                label = lab.attr;
                ind = 1;

                if(label == CLabtype.Type.EndCode)
                    break;
                else if (label == CLabtype.Type.Point)
                {
                    pts = (CPTStype)lab.Tag;
                    modchn = pts.mc;
                    modx = pts.mx;
                    mody = pts.my;
                    modz = pts.mz;

                    if (m > 0 && modchn == lastchn)
                    {
                        if (end_chk == 3)
                            break;
                        end_chk++;
                        continue;
                    }
                    else
                        end_chk = 0;

                    while (modchn > evc[m])
                    {
                        if (modchn > EC)
                        { 
                            modchn = EC;
                            endflag = 1; 
                        }
                        m++;
                    }
                    //chk = (short)evc[m - 1];
                    //chk = (short)bvc[m];

                    if (modchn == vip[0])
                    {
                        ftl = level[m];
                        write_model_rec(ref lab);
                    }
                    else if (modchn >= evc[m - 1] && modchn < bvc[m])
                    {
                        no1 = (short)Math.Ceiling((vip[m] - evc[m - 1]) / cint);  //Numbers of x loop-Grade section

                        for (x = 0; x <= no1 + 1; x++)
                        {
                            next = modchn;              //get next chainage
                            gdist = next - evc[m - 1];      //get exact distance
                            ftl = evclev[m - 1] + (grad[m] * 100 * gdist);

                            ok = 1;
                            write_model_rec(ref lab);

                            break;
                        }

                    }
                    else if (modchn >= bvc[m] && modchn < vip[m])
                    {
                        no1 = (short)(vcl1[m] / cint);  //Numbers of x loop-Grade section

                        for (x = 0; x <= no1 + 1; x++)
                        {
                            next = modchn;
                            vdist = next - bvc[m];
                            ydist1 = mv[m] * (vdist / vcl1[m]) * (vdist / vcl1[m]);
                            ydist1 = ydist1 * 10000;
                            yval1 = bvclev[m] + (grad[m] * 100 * (next - bvc[m]));
                            ftl = yval1 + ydist1;

                            ok = 1;
                            write_model_rec(ref lab);

                            break;
                        }

                    }
                    else if (modchn >= vip[m] && modchn <= evc[m] && m < item - 1)
                    {
                        no2 = (short)(vcl2[m] / cint);  //Numbers of x loop-Grade section

                        for (x = 0; x <= no2 + 1; x++)
                        {
                            next = modchn;
                            vdist = evc[m] - next;
                            ydist2 = mv[m] * (vdist / vcl2[m]) * (vdist / vcl2[m]);
                            ydist2 = ydist2 * 10000;
                            yval2 = evclev[m] - (grad[m + 1] * 100 * (evc[m] - next));
                            ftl = yval2 + ydist2;

                            ok = 1;
                            if (modchn == evc[m])
                                ok = 0;

                            write_model_rec(ref lab);

                            break;
                        }

                    }
                    else if (modchn >= evc[item - 2])
                    {
                        if (modchn < vip[item - 1])     //Numbers of x loop-Grade section
                            no2 = (short)Math.Ceiling((vip[item - 1] - evc[item - 2]) / cint);
                        else
                            no2 = (short)Math.Ceiling((modchn - evc[item - 2]) / cint);

                        for (x = 0; x <= no2 + 1; x++)
                        {
                            m--;
                            next = modchn;                     //get next chainage
                            gdist = next - vip[m];               //get exact distance
                            ftl = level[m] + (grad[m + 1] * 100 * gdist);
                            endchn = next;
                            endlev = ftl;
                            ok = 1;
                            write_model_rec(ref lab);

                            break;
                        }

                    }
                    
                    if (modchn == EC)
                        break;

                    if (ok == 0)
                    {
                        m++;
                    }

                    if (m == item)
                        break;

                    lastchn = modchn;
                }// 103
            }//end for
        }

        //void write_profile_model()
        //{
        	
        //    short end_chk, chk;

        //    CLabtype lab = new CLabtype();
        //    CModType mod = new CModType();
        //    CStgType stg = new CStgType();
        //    CPTStype pts = new CPTStype();
        //    CTXTtype txt = new CTXTtype();

        //    string buffer;

        //    m=0;
        //    ind=0;
        //    lastchn=-999.999;
        //    end_chk=0;

        //    //buffer.Format("ALIGNMENT MODEL  =  %s   STRING  =  %s\n", modnam, stglbl);
        //    //m_ModelCtrlArray = (char**)malloc(sizeof(char));
        //    //*m_ModelCtrlArray = (char*)malloc(sizeof(buffer));	

        //    /**/
        //    StreamWriter tmpFile = new StreamWriter(Path.Combine(path, "SCROLL.TMP"), true); 
        //    string strTemp = string.Format("ALIGNMENT MODEL  =  {0}   STRING  =  {1}\n", modnam, stglbl);
        //    tmpFile.Write(strTemp);
        //    tmpFile.Close();

        //    // printf("\tALIGNMENT MODEL  =  %s   STRING  =  %s\n", modnam, stglbl);
        //    fsetpos(fp1, &loc1);

        //    while(! feof(fp1))
        //    {
        //        ok=0;

        //        if(ind)
        //            fsetpos(fp1, &loc2);

        //        fread(&lab, sizeof(char), sizeof(lab), fp1);

        //        fgetpos(fp1, &loc2);
        //        label = lab.attr;
        //        ind=1;

        //        if(label == 999)
        //            break;
        //        else if(label == 103)
        //        {
        //            fgetpos(fp1, &loc1);
        //            fread(&pts, sizeof(char), sizeof(pts), fp1);
        //            fgetpos(fp1, &loc2);

        //            modchn = pts.mc;
        //            modx   = pts.mx;
        //            mody   = pts.my;
        //            modz   = pts.mz;

        //            if(modchn==5600.0)
        //                chk=1;

        //        //  18/12/2000 


        //            if(m > 0 && modchn==lastchn)
        //            {

        //                if(end_chk==3)
        //                    break;
        //                end_chk++;
        //                continue;
        //            }
        //            else
        //                end_chk=0;

        //            grade_start:
        //            if(modchn > evc[m])
        //            {
        //                if(modchn > EC)
        //                    {modchn=EC; endflag=1;}
        //                m++;
        //                goto grade_start;
        //            }

        //            chk=evc[m-1];
        //            chk=bvc[m];

        //            if(modchn == vip[0])
        //            {
        //                ftl=level[m];
        //                write_model_rec();
        //            }
        //            else if(modchn >= evc[m-1] && modchn < bvc[m])
        //            {
        //                no1=Math.Ceiling((vip[m]-evc[m-1])/cint);  //Numbers of x loop-Grade section

        //                for(x=0;x<=no1+1;x++)
        //                {
        //                    next=modchn;              //get next chainage
        //                    gdist=next-evc[m-1];      //get exact distance
        //                    ftl=evclev[m-1]+(grad[m]*100*gdist);

        //                    ok=1;
        //                    write_model_rec();

        //                    break;
        //                }

        //            }
        //            else if(modchn >= bvc[m] && modchn < vip[m])
        //            {
        //                no1=vcl1[m]/cint;  //Numbers of x loop-Grade section

        //                for(x=0;x<=no1+1;x++)
        //                {
        //                    next=modchn;
        //                    vdist=next-bvc[m];
        //                    ydist1=mv[m]*(vdist/vcl1[m])*(vdist/vcl1[m]);
        //                    ydist1=ydist1*10000;
        //                    yval1=bvclev[m]+(grad[m]*100*(next-bvc[m]));
        //                    ftl=yval1+ydist1;

        //                    ok=1;
        //                    write_model_rec();

        //                    break;
        //                }

        //            }
        //            else if(modchn >= vip[m] && modchn <= evc[m] && m < item-1)
        //            {
        //                no2=vcl2[m]/cint;  //Numbers of x loop-Grade section

        //                for(x=0;x<=no2+1;x++)
        //                {
        //                    next=modchn;
        //                    vdist=evc[m]-next;
        //                    ydist2=mv[m]*(vdist/vcl2[m])*(vdist/vcl2[m]);
        //                    ydist2=ydist2*10000;
        //                    yval2=evclev[m]-(grad[m+1]*100*(evc[m]-next));
        //                    ftl=yval2+ydist2;
        	
        //                    ok=1;
        //                    if(modchn == evc[m])
        //                        ok=0;

        //                    write_model_rec();

        //                    break;
        //                }

        //            }
        //            else if(modchn >= evc[item-2])
        //            {
        //                if(modchn < vip[item-1])     //Numbers of x loop-Grade section
        //                    no2=Math.Ceiling((vip[item-1]-evc[item-2])/cint);
        //                else
        //                    no2=Math.Ceiling((modchn-evc[item-2])/cint);

        //                for(x=0;x<=no2+1;x++)
        //                {
        //                    m--;
        //                    next=modchn;                     //get next chainage
        //                    gdist=next-vip[m];               //get exact distance
        //                    ftl=level[m]+(grad[m+1]*100*gdist);
        //                    endchn=next;
        //                    endlev=ftl;
        //                    ok=1;
        //                    write_model_rec();

        //                    break;
        //                }

        //            }

        //            modpt:

        //            if(modchn == EC)
        //                break;

        //            if(ok==0)
        //            {
        //                m++;
        //            }

        //            if(m==item)
        //                break;

        //            lastchn=modchn;

        //        }// 103 
        //    } //End of while feof loop

          
        //}

        void write_model_rec(ref CLabtype lab)
        {
            
            if (lab.attr != CLabtype.Type.Point)
            {
                System.Diagnostics.Trace.Assert(false, "Not a " + CLabtype.Type.Point.ToString());
                return;
            }
            CPTStype pts = new CPTStype();
            pts.mc = modchn;
            pts.mx = modx;
            pts.my = mody;
            pts.mz = ftl;
            lab.Tag = pts;

            StreamWriter tmpFile = new StreamWriter(Path.Combine(path, "SCROLL.TMP"), true);
            string strTemp = string.Format("{0,10:f3} {1,12:f3} {2,12:f3} {3,12:f3}\n", modchn, modx, mody, ftl);
            tmpFile.Write(strTemp);
            tmpFile.Close();

        }
        
        //void write_model_rec()
        //{
        //    CLabtype lab = new CLabtype();
        //    CModType mod = new CModType();
        //    CStgType stg = new CStgType();
        //    CPTStype pts = new CPTStype();
        //    //CTXTtype txt = new CTXTtype();

        //    CString buffer;

        //    fsetpos(fp1,&loc1);

        //    pts.mc = modchn;
        //    pts.mx = modx;
        //    pts.my = mody;
        //    pts.mz = ftl;
        //    fwrite(&pts, sizeof(char), sizeof(pts), fp1);

        //    /*buffer.Format("%10.3f % 12.3f %12.3f %12.3f\n", modchn, modx, mody, ftl);
        //    m_ModelCtrlArray = (char**)malloc(sizeof(char));
        //    *m_ModelCtrlArray = (char*)malloc(sizeof(buffer));	
        //    */

        //    /**/
        //    char tmpFileName[512] ;
        //    strcpy(tmpFileName, path);
        //    strcat(tmpFileName, "\\scroll.tmp");
        //    if((tmpFile=fopen(tmpFileName, "a")) != NULL)
        //    {
        //        fprintf(tmpFile, "%10.3f % 12.3f %12.3f %12.3f\n", modchn, modx, mody, ftl);
        //        fclose(tmpFile);
        //    }
        //    else
        //    {
        //        sprintf(msgstr, "Failed to write display file");
        //        AfxMessageBox(msgstr);
        //    }
        //    /**/

        //    //*printf("\t%10.3f % 12.3f %12.3f %12.3f\n", modchn, modx, mody, ftl);

        //}
        
        void write_grade_model()
        {
            CLabtype lab = new CLabtype();
            CModType mod = new CModType();
            CStgType stg = new CStgType();
            CPTStype pts = new CPTStype();
            //CTXTtype txt = new CTXTtype();

            //string buffer;

            int gi;

            pathfile = Path.Combine(path, "MODEL.FIL");
            fp12 = new BinaryWriter(new FileStream(pathfile, FileMode.Append), Encoding.Default);
                    	
            gradmod = modnam;
            len = (short)gradmod.Length;
            if (len > 7)
            {
                gradmod = gradmod.Substring(0, 7);
            }

            gradmod += "#";

            modnam = gradmod;
        	

            write_model_list();			/* 17/12/2000 */

            lab = new CLabtype();
            lab.attr = CLabtype.Type.Model;
            mod = new CModType();
            mod.name = gradmod;
            lab.Tag = mod;
            lab.ToStream(fp12);
           

            lab = new CLabtype();
            lab.attr = CLabtype.Type.String;
            stg = new CStgType();
            stg.label = stglbl;
            lab.Tag = stg;
            lab.ToStream(fp12);

            
            StreamWriter tmpFile = new StreamWriter(Path.Combine(path, "SCROLL.TMP"), true);
            string strTemp = string.Format("GRADE LINE MODEL  =  {0}   STRING  =  {1}\n", gradmod, stglbl);
            tmpFile.Write(strTemp);
            tmpFile.Close();

            

            vip[item]=endchn;
            level[item]=endlev;
            item++;

            if(vip[item-2] == vip[item-1])
                item = (short)(item-1);

            //strtok(modnam, "#");

            for(gi=0; gi<item; gi++)
            {
                modchn=vip[gi];
                modx=vcl1[gi];
                mody=vcl2[gi];
                ftl=level[gi];

                if(modx == mody) /* symmetrical vert curve */
                {
                    modx += mody;
                    mody = 0.0;
                }

                lab = new CLabtype();
                lab.attr = CLabtype.Type.Point;
                pts = new CPTStype();
                pts.mc = modchn;
                pts.mx = modx;
                pts.my = mody;
                pts.mz = ftl;
                lab.Tag = pts;
                lab.ToStream(fp12);
                

                tmpFile = new StreamWriter(Path.Combine(path, "SCROLL.TMP"), true);
                strTemp = string.Format("{0,10:f3} {1,12:f3} {2,12:f3} {3,12:f3}\n", modchn, modx, mody, ftl);
                tmpFile.Write(strTemp);
                tmpFile.Close();                

            }

            lab = new CLabtype();
            lab.attr = CLabtype.Type.EndCode;
            lab.ToStream(fp12);

            fp12.Close();

            short  eltype, turn;//, elno,store_elno;
            double sc, ec, t1, t2, ipx, ipy, l1, l2, xs, ys, xe, ye, g1, g2, del;//, tmp1, tmp2, tmp3, tmp4;


            //elno=0;
            eltype=0;
            turn=0;


            //string modnam1, stglbl1;
            StreamReader fil = new StreamReader(Path.Combine(path, "VALIGN.FIL"));           

            /*
            Compress Design Database VALIGN.FIL	by removing '########'	//11/12/2002 Sandipan
            */

            StreamWriter til = new StreamWriter(Path.Combine(path, "VALIGNFIL.TMP"), false);            

            short elnum, last_elnum;
            last_elnum=-999;

            while(fil.EndOfStream == false)
            {
                string strLine = fil.ReadLine();
                CValignFilData fildata = CValignFilData.Parse(strLine);
                //fscanf(fil, "%s %s %i %i %lf %lf %lf %lf %lf %lf %lf %lf %lf %lf %lf %lf %lf %lf %lf %i", modnam, stglbl, &elnum, &eltype, &sc, &ec, &ipx, &ipy, &t1, &t2, &l1, &l2, &xs, &ys, &xe, &ye, &g1, &g2, &del, &turn);
                modnam = fildata.modnam;
                stglbl = fildata.stglbl;
                elnum = (short)fildata.elno;
                eltype = (short)fildata.eltype;
                sc = fildata.chn1;
                ec = fildata.chn2;
                ipx = fildata.hipx;
                ipy = fildata.hipy;
                t1 = fildata.t1;
                t2 = fildata.t2;
                l1 = fildata.rad;
                l2 = fildata.l;
                xs = fildata.xs;
                ys = fildata.ys;
                xe = fildata.xe;
                ye = fildata.ye;
                g1 = fildata.b1;
                g2 = fildata.b2;
                del = fildata.del;
                turn = (short)fildata.turn;


        	
                if(modnam == "########" && stglbl == "########")
                {
                    continue;
                }
                else
                {
                    if(elnum != last_elnum)
                    {
                        strLine  = fildata.ToString();
                        til.WriteLine(strLine);
                        //fprintf(til,"%8s %8s %4d %4d %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.6f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %d\n"
                        //, modnam, stglbl, elnum, eltype, sc, ec, ipx, ipy, t1, t2, l1, l2, xs, ys, xe, ye, g1, g2, del, turn);
                    }
                    
                    last_elnum = elnum;
                }

            }

            fil.Close();
            til.Close();

            //char tmpFileNameA[255], tmpFileNameB[255];
        }
        
        void write_model_null()
        {
            double val1, val2;//, val;
            //double last_m, last_val;
            short i;

            CLabtype lab = new CLabtype();
            CModType mod = new CModType();
            CStgType stg = new CStgType();
            CPTStype pts = new CPTStype();
            CTXTtype txt = new CTXTtype();

            // writes null X-Y model 

            //fclose(fp1);
            List<CLabtype> listModelFil = new List<CLabtype>();
            ViewerUtils.ReadModelFilFile(Path.Combine(path, "MODEL.FIL"), ref listModelFil);

            lab = new CLabtype();
            lab.attr = CLabtype.Type.Model;
            mod = new CModType();
            mod.name = modnam;
            lab.Tag = mod;
            listModelFil.Add(lab);
            
            lab = new CLabtype();
            lab.attr = CLabtype.Type.String;
            stg = new CStgType();
            stg.label = stglbl;
            lab.Tag = stg;
            listModelFil.Add(lab);

            m=0;
            modx=mody=-999.9;
            modchn=SC;

            StreamWriter tmpFile = new StreamWriter(Path.Combine(path, "SCROLL.TMP"), true); 
            string strTemp = string.Format("NULL MODEL  =  {0}   STRING  =  {1}\n", modnam, stglbl);
            tmpFile.Write(strTemp);
            tmpFile.Close();
            

            //fgetpos(fp1,&loc1);

            //short chk = 0;

            while(true)
            {
                ok=0;

                if(m > 0 && modchn==lastchn)
                    break;

                //if(modchn==3250.0)
                //    chk = 1;

                if(modchn > evc[m])
                {
                    if(modchn > EC){modchn=EC; endflag=1;}
                        m++;
                }

                if(modchn == vip[0])
                {
                    ftl=level[m];
                    write_null_rec(ref listModelFil);
                }
                else if(modchn >= evc[m-1] && modchn < bvc[m])
                {
                    if(m != 0)
                    {
                        no1=(short)((vip[m]-evc[m-1])/cint);  //Numbers of x loop-Grade section

                        for(x=0;x<=no1+1;x++)
                        {
                            next=modchn;              //get next chainage
                            gdist=next-evc[m-1];      //get exact distance

                            val1=evclev[m-1];
                            val2=grad[m]*100*gdist;
                            ftl=val1+val2;


                            //ftl=evclev[m-1]+(grad[m]*100*gdist);

                            ok=1;
                            write_null_rec(ref listModelFil);

                            break;
                        }
                    }
                }
                else if(modchn >= bvc[m] && modchn < vip[m])
                {
                    no1=(short)(vcl1[m]/cint);  //Numbers of x loop-Grade section

                    for(x=0;x<=no1+1;x++)
                    {
                        next=modchn;
                        vdist=next-bvc[m];
                        ydist1=mv[m]*(vdist/vcl1[m])*(vdist/vcl1[m]);
                        ydist1=ydist1*10000;
                        yval1=bvclev[m]+(grad[m]*100*(next-bvc[m]));
                        ftl=yval1+ydist1;

                        ok=1;
                        write_null_rec(ref listModelFil);

                        break;
                    }
                }
                else if(modchn >= vip[m] && modchn <= evc[m] && m < item-1)
                {
                    no2 = (short)(vcl2[m] / cint);  //Numbers of x loop-Grade section

                    for(x=0;x<=no2+1;x++)
                    {
                        next=modchn;
                        vdist=evc[m]-next;
                        ydist2=mv[m]*(vdist/vcl2[m])*(vdist/vcl2[m]);
                        ydist2=ydist2*10000;
                        yval2=evclev[m]-(grad[m+1]*100*(evc[m]-next));
                        ftl=yval2+ydist2;

                        ok=1;
                        if(modchn == evc[m])
                            ok=0;

                        write_null_rec(ref listModelFil);

                        break;
                    }
                }
                else if(modchn >= evc[item-2])
                {
                    if(modchn < vip[item-1])     //Numbers of x loop-Grade section
                        no2 = (short)Math.Ceiling((vip[item - 1] - evc[item - 2]) / cint);
                    else
                        no2 = (short)Math.Ceiling((modchn - evc[item - 2]) / cint);

                    for(i=0; i<no2+1; i++)
                    {
                        m--;

                        //get next chainage
                        next=modchn;

                       //get exact distance
                        gdist=next-vip[m];

                        //17/12/2000
                        ftl=level[m]+(grad[m+1]*100*gdist);

                        endchn=next;
                        endlev=ftl;
                        ok=1;
                        write_null_rec(ref listModelFil);

                        break;
                    }

                }

                if(modchn == EC)
                    break;

                if(ok==0)
                {
                    m++;
                }

                if(m==item)
                    break;

                if(modchn > EC && endflag==0)
                {
                    modchn=EC; 
                    endflag=1;
                }
                else if(modchn > EC && endflag==1)
                    break;


                if(modchn==vip[item-1])
                    break; 			// 17/12/2000 

                lastchn=modchn;
                modchn += cint;

                if(modchn > vip[item-1])
                    modchn = vip[item-1];     	// 17/12/2000


            } //End of while 1 loop

            lab = new CLabtype();
            lab.attr = CLabtype.Type.EndCode;
            listModelFil.Add(lab);
            
            ViewerUtils.WriteModelFilFile(Path.Combine(path, "MODEL.FIL"), listModelFil);
        }

        void write_null_rec(ref List<CLabtype> listModelFil)
        {
            CLabtype lab = new CLabtype();
            CPTStype pts = new CPTStype();

            lab = new CLabtype();
            lab.attr = CLabtype.Type.Point;  
            pts = new CPTStype();
            pts.mc = modchn;
            pts.mx = 0.0;
            pts.my = 0.0;
            pts.mz = ftl;
            lab.Tag = pts;
            listModelFil.Add(lab);

            StreamWriter tmpFile = new StreamWriter(Path.Combine(path, "SCROLL.TMP"), true); 
            string strTemp = string.Format("{0,10:f3} {1,12:f3} {2,12:f3} {3,12:f3}\n", modchn, modx, mody, ftl);
            tmpFile.Write(strTemp);
            tmpFile.Close();
            
        }

        void header()
        {
            //time(&ts);
            //ptm=ctime(&ts);  

            /**/
            fp3.Write("\n\n\n\t\t\t\t\t\t*********************************************");
            fp3.Write("\n\n\t\t\t\t\t              DESIGN OF VERTICAL ALIGNMENT");
            fp3.Write("\n\n\t\t\t			   	     H E A D S");
            fp3.Write("\n\n\t\t\t			           Release 14.0");
            fp3.Write("\n\n\t\t\t				         By");
            fp3.Write("\n\n\t\t\t\t			TechSOFT  Engineering  Services");
            fp3.Write("\n\n\t\t\t\t\t          Program was run on  " + DateTime.Now.ToString());
            fp3.Write("\n\n\n\t\t\t\t\t\t*********************************************");
            /**/
            

            fp3.Write("\n\n\n\n");
            fp3.Write("VERTICAL CURVE DETAILS:\n");
            fp3.Write("----------------------------------------------------------------------------\n");
            fp3.Write("\n");  
        }

        void header3()
        {
            fp3.Write("\n\n\n");
            fp3.Write("VERTICAL ALIGNMENT WITH CHAINAGE STATIONS AT USER REQUESTED INTERVAL :\n");
            fp3.Write("------------------------------------------------------------------------------\n");
            fp3.Write("                   -CHAINAGE-      -LEVEL-  -GRADE*100-  -V. DIST-  -V. RADIUS-\n");
            fp3.Write("\n");
        }

        void footer()
        {
            fp3.Write("\n\n\n\n");

            fp3.Write("\t\t--------------------------------\n");
            fp3.Write("\t\t   E N D   O F   D E S I G N.\n");
            fp3.Write("\t\t--------------------------------\n");
        }

        void report_message()
        {
            MessageBox.Show("Design Report is written in file VALIGNMENT.REP", HeadsUtils.Constants.ProductName);
        }

        void write_model_list()
        {
            ClSTtype lst = new ClSTtype();
            lst.strMod1 = modnam;
            lst.strStg = stglbl;

            BinaryWriter bw = new BinaryWriter(new FileStream(Path.Combine(path, "MODEL.LST"), FileMode.Append), Encoding.Default);
            lst.ToStream(bw);
            bw.Close();
        }

        void no_file_msg()
        {
            string buffer = "User Input Data file not found... path : ?";
            MessageBox.Show(buffer, HeadsUtils.Constants.ProductName);
        }

        void no_string_msg()
        {
            string buffer = string.Format("User specified Model [{0}] String [{1}] not found...", usermod, userstg);
            MessageBox.Show(buffer, HeadsUtils.Constants.ProductName);
        }

    }
}
