using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HeadsUtils;

namespace HeadsFunctions1.Valignment
{
    internal class VVIP
    {
        public VVIP()
        {

        }
        public double chainx = 0;
        public double chainy = 0;
        public bool sym = false;
        public double grade = 0;
        public double vcl1 = 0;
        public double vcl2 = 0;

        public static VVIP FromStream(System.IO.BinaryReader br)
        {
            VVIP obj = new VVIP();
            obj.chainx = br.ReadDouble();
            obj.chainy = br.ReadDouble();
            obj.sym = br.ReadBoolean();
            obj.grade = br.ReadDouble();
            obj.vcl1 = br.ReadDouble();
            obj.vcl2 = br.ReadDouble();

            return obj;
        }

        public void ToStream(System.IO.BinaryWriter bw)
        {
            bw.Write(this.chainx);
            bw.Write(this.chainy);
            bw.Write(this.sym);
            bw.Write(this.grade);
            bw.Write(this.vcl1);
            bw.Write(this.vcl2);
        }
    }

    internal class FirstLine
    {
        public FirstLine()
        {

        }
        public short Code;
        public short AcceptCode;
        public string ModelName; //[30];
        public string StringName; //[20];

        public static FirstLine FromStream(System.IO.BinaryReader br)
        {
            FirstLine data = new FirstLine();

            data.Code = br.ReadInt16();
            data.AcceptCode = br.ReadInt16();
            data.ModelName = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(30));
            data.StringName = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(20));

            return data;
        }

        public void ToStream(System.IO.BinaryWriter bw)
        {
            bw.Write(this.Code);
            bw.Write(this.AcceptCode);
            bw.Write(ViewerUtils.ConvertStringToByteArray(this.ModelName, 30));
            bw.Write(ViewerUtils.ConvertStringToByteArray(this.StringName, 20));
        }
    }

    internal class SecondLineVIP
    {
        public SecondLineVIP()
        {

        }
        public int TotalVIPs;
        public double StartChain;
        public double EndChain;
        public double Interval;

        public static SecondLineVIP FromStream(System.IO.BinaryReader br)
        {
            SecondLineVIP data = new SecondLineVIP();

            data.TotalVIPs = br.ReadInt32();
            data.StartChain = br.ReadDouble();
            data.EndChain = br.ReadDouble();
            data.Interval = br.ReadDouble();

            return data;
        }

        public void ToStream(System.IO.BinaryWriter bw)
        {
            bw.Write(this.TotalVIPs);
            bw.Write(this.StartChain);
            bw.Write(this.EndChain);
            bw.Write(this.Interval);
        }
    }

    internal class VIPData
    {
        public VIPData()
        {

        }
        public int VIPNo;
        public double VIP_Chain;
        public double VIP_level;
        public double VCL1;
        public double VCL2;

        public static VIPData FromStream(System.IO.BinaryReader br)
        {
            VIPData data = new VIPData();

            data.VIPNo = br.ReadInt32();
            data.VIP_Chain = br.ReadDouble();
            data.VIP_level = br.ReadDouble();
            data.VCL1 = br.ReadDouble();
            data.VCL2 = br.ReadDouble();

            return data;
        }

        public void ToStream(System.IO.BinaryWriter bw)
        {
            bw.Write(this.VIPNo);
            bw.Write(this.VIP_Chain);
            bw.Write(this.VIP_level);
            bw.Write(this.VCL1);
            bw.Write(this.VCL2);
        }
    }
    
    internal class CValignUtil
    {
        BinaryReader fp1;
        StreamWriter fp2;
        BinaryWriter fp3;
        StreamWriter fpm;
        
        int code, pts, vips, ip, index, vipno;
        double[] vip = new double[5000];
        double[] level = new double[5000];
        double[] vcl1 = new double[5000];
        double[] vcl2 = new double[5000];
        double[] grad = new double[5000];

        double sx = 0, sy = 0, inc = 5.0 , last_sx = 0, last_sy = 0, chn1 = 0, endchn = 0;
        string modnam, stglbl;
        long error_flag;
        string msgstr;
        int elemnum;

        string path, pathfile;
        //string drive, dir, file, ext;
        public CValignUtil()
        {

        }

        public bool Funcmain(string argv1)
        {
            path = argv1;
            //read_default();
            
            pathfile = Path.Combine(path, "VAL1.TMP");

            fp1 = new BinaryReader(new FileStream(pathfile, FileMode.Open, FileAccess.Read), Encoding.Default);
            FirstLine val1 = FirstLine.FromStream(fp1);

            fp1.Close();

            pathfile = Path.Combine(path, "VALIGN.TMP");
            fp2 = new StreamWriter(pathfile, false);
            
            pathfile = Path.Combine(path, "VAL2.TMP");
            fp3 = new BinaryWriter(new FileStream(pathfile, FileMode.Create), Encoding.Default);
            
            pathfile = Path.Combine(path, "ERRORMESSAGES.TXT");
            fpm = new StreamWriter(pathfile, true);
            fpm.WriteLine("");
            fpm.WriteLine("    *************************************************");
            fpm.WriteLine("    Program [HEADS:Design:VALIGNMENT] was run on  " + DateTime.Now.ToString());

            code = (int)val1.Code;
            switch (val1.Code)
            {
                case 1: 
                    vip_method();
                    break;

                default: break;
            }
            
            fpm.WriteLine("    *************************************************");

            fp2.Close();
            fp3.Close();
            fpm.Close();
            

            if (error_flag == 1)
                System.Windows.Forms.MessageBox.Show("Check the file ERRORMESSAGES.TXT for all the details", HeadsUtils.Constants.ProductName);                

            //if (val1.AcceptCode == 1)
            //    save_valign();

            return (error_flag == 1) ? false : true;
        }

        void vip_method()
        {
            vip_input();

            profile_array();

            //write_drg_text();

            write_valign();

        }

        void vip_input()
        {
            FirstLine val1;
            SecondLineVIP val2;
            VIPData val3;

            StreamWriter fptmp;

            pathfile = Path.Combine(path, "VAL1.TMP");
            fp1 = new BinaryReader(new FileStream(pathfile, FileMode.Open, FileAccess.Read), Encoding.Default);

            //strcpy(pathfile, path);
            //strcat(pathfile, "val1.tmp");

            //if ((fp1 = fopen(pathfile, "rb")) == NULL)
            //{
            //    sprintf(msgstr, " Data file %s not found...", pathfile);
            //    AfxMessageBox(msgstr);
            //    return;
            //    //no_file_msg();
            //}

            pathfile = Path.Combine(path, "VAL1.TXT");
            fptmp = new StreamWriter(pathfile, false);

            //strcpy(pathfile, path);
            //strcat(pathfile, "val1.txt");

            //if ((fptmp = fopen(pathfile, "w")) == NULL)
            //{
            //    sprintf(msgstr, " Data file %s not found...", pathfile);
            //    AfxMessageBox(msgstr);
            //    return;
            //    //no_file_msg();
            //}

            val1 = FirstLine.FromStream(fp1);

            modnam = val1.ModelName;
            stglbl = val1.StringName;

            val2 = SecondLineVIP.FromStream(fp1);
            
            vips = val2.TotalVIPs;
            chn1 = val2.StartChain;
            endchn = val2.EndChain;
            inc = val2.Interval;

            fptmp.WriteLine("VIP = " + vips.ToString());

            pts = 0;

            for (ip = 0; ip < vips; ip++)
            {
                val3 = VIPData.FromStream(fp1);

                vip[ip] = new double();
                level[ip] = new double();
                level[ip] = new double();
                level[ip] = new double();
                grad[ip] = new double();

                vip[ip] = 0.0;
                level[ip] = 0.0;
                vcl1[ip] = 0.0;
                vcl2[ip] = 0.0;

                vipno = val3.VIPNo;
                vip[ip] = val3.VIP_Chain;
                level[ip] = val3.VIP_level;
                vcl1[ip] = val3.VCL1;
                vcl2[ip] = val3.VCL2;

                fptmp.WriteLine("303\t" + vip[ip].ToString() + "\t" + level[ip].ToString() + "\t" + ((double)(vcl1[ip] + vcl2[ip])).ToString());
                
                if (ip > 0)
                {
                    if (vip[ip] <= vip[ip - 1])
                    {
                        //chain_error();
                        error_flag = 1;
                        msgstr = "Chainage data inconsistent at VIP " + ((int)(vipno-1)).ToString();
                        System.Windows.Forms.MessageBox.Show(msgstr, HeadsUtils.Constants.ProductName);
                        fpm.WriteLine("    In Process : " + msgstr);
                        fp3.Close();
                        fp1.Close();
                        return;
                    }

                    grad[ip] = (level[ip] - level[ip - 1]) / (vip[ip] - vip[ip - 1]) / 100.0;
                }

            }
            fp1.Close();
            fptmp.Close();
           
            if (endchn > vip[vips - 1])
                endchn = vip[vips - 1];/*NEW*/
        }

        void profile_array()
        {
            short	x, l, m, ok, no1, no2, endflag;
            double[] bvc = new double[10000];
            double[] evc = new double[10000];
            double[] ybvc = new double[10000];
            double[] yevc = new double[10000];
            double[] bvclev = new double[10000];
            double[] evclev = new double[10000];
            double[] mv = new double[10000];
            
            double modchn, delta, ftl, gdist, vdist,	ydist1, yval1, ydist2, yval2;
            double tmp1, tmp2;
            
            vcl1[0]=vcl2[0]=0.0;
            vcl1[vips-1] = vcl2[vips-1] = 0.0;

            string msgstr;
            
            for(l=0; l<vips; l++)
            {
                bvc[l] = new double();
                ybvc[l] = new double();
                evc[l] = new double();
                yevc[l] = new double();
                bvclev[l] = new double();
                evclev[l] = new double();
                mv[l] = new double();

                bvc[l]=vip[l]-vcl1[l];
                ybvc[l]=vcl1[l]*grad[l]*100;
                
                evc[l]=vip[l]+vcl2[l];
                yevc[l]=vcl2[l]*grad[l+1]*100;

                
                if(l > 0)
                {
                    if(bvc[l] < evc[l-1])
                    {
                        error_flag=1;
                        msgstr = "CURVE OVERLAPED, ERROR AT LINE "+ ((int)(l+1)).ToString("0000") +" : Ch." + vip[l].ToString("0.000");
                        System.Windows.Forms.MessageBox.Show(msgstr, HeadsUtils.Constants.ProductName);
                        fpm .WriteLine("    In Process : " + msgstr);
                    }
                }
                
                if(grad[l]>=0)
                    bvclev[l]=level[l]-ybvc[l];       /*****/
                else if(grad[l]<0)
                    bvclev[l]=level[l]-ybvc[l];
                
                if(grad[l+1]>=0)
                    evclev[l]=level[l]+yevc[l];       /*****/
                else if(grad[l+1]<0)
                    evclev[l]=level[l]+yevc[l];
                
                if(l > 0 && l < vips-1)
                {
                    if(bvc[l] < evc[l-1])
                    {                        
                        error_flag = 1;
                        msgstr = "Chainage data inconsistent at VIP " + vipno.ToString();
                        System.Windows.Forms.MessageBox.Show(msgstr, HeadsUtils.Constants.ProductName);
                        fpm.WriteLine("    In Process : " +  msgstr);
                        fp3.Close();
                        return;
                    }
                    
                    delta = grad[l+1] - grad[l];
                    if(delta == 0.0)
                        delta = 0.0001;
                    
                    tmp1 = vcl1[l];
                    tmp2 = vcl2[l];
                    
                    mv[l] = (vcl1[l] * vcl2[l] * delta) / (200.0 * (vcl1[l] + vcl2[l]));
                }
            }
            
            last_sx=-999.9;
            last_sy=-999.9;
            endflag=0;
            m=0;
            pts=0;
            modchn=chn1;
            index=0;
            
            /***************** fwrite drg at each sx[i], sy[i] ****************************/

            
            while(true)
            {
                ok=0;
                if(modchn == vip[0])
                {
                    ftl = level[m];
                    ok=1;
                    
                    if(pts > 0)
                    {
                        last_sx = sx;
                        last_sy = sy;
                    }
                    sx = modchn;
                    sy = ftl;
                    write_drg();
                    pts++;
                }                  
                else if(m > 0 && modchn >= evc[m-1] && modchn < bvc[m])//NEW
                {
                    //no1=(short)ceil((vip[m]-evc[m-1])/inc);
                    no1 = (short)Math.Ceiling((vip[m] - evc[m - 1]) / inc);
                    /*Numbers of x loop-Grade section*/
                    
                    for(x=0;x<=no1+1;x++)
                    //if (0 <= no1+1)
                    {
			            gdist=modchn-evc[m-1];      /*get exact distance*/

			            ftl=evclev[m-1]+(grad[m]*100.0*gdist);

			            ok=1;

			            if(pts > 0)
			            {
				            last_sx = sx;
				            last_sy = sy;
			            }
			            sx = modchn;
			            sy = ftl;
			            write_drg();
			            pts++;

			            //break;
			        }
                }
                else if(modchn >= bvc[m] && modchn < vip[m])
                {
                    no1=(short)( vcl1[m]/inc );  /*Numbers of x loop-Grade section*/

			        for(x=0;x<=no1+1;x++)
                    //if (0 <=no1+1)
			        {
				        vdist=modchn-bvc[m];
				        ydist1=mv[m]*(vdist/vcl1[m])*(vdist/vcl1[m]);
				        ydist1=ydist1*10000;
				        yval1=bvclev[m]+(grad[m]*100*(modchn-bvc[m]));
				        ftl=yval1+ydist1;

				        ok=1;

				        if(pts > 0)
				        {
					        last_sx = sx;
					        last_sy = sy;
				        }
				        sx = modchn;
				        sy = ftl;
				        write_drg();
				        pts++;

				        //break;
			        }
                }
                else if(modchn >= vip[m] && modchn <= evc[m] && m < vips-1)
                {
                    no2=(short)( vcl2[m]/inc );  /*Numbers of x loop-Grade section*/

			        for(x=0;x<=no2+1;x++)
                    //if (0 <= no2+1)
			        {
				        vdist=evc[m]-modchn;
				        ydist2=mv[m]*(vdist/vcl2[m])*(vdist/vcl2[m]);
				        ydist2=ydist2*10000;
				        yval2=evclev[m]-(grad[m+1]*100*(evc[m]-modchn));
				        ftl=yval2+ydist2;

				        if(modchn == evc[m])ok=0;
				        else ok=1;

				        if(pts > 0)
				        {
					        last_sx = sx;
					        last_sy = sy;
				        }
				        sx = modchn;
				        sy = ftl;
				        write_drg();
				        pts++;

				        //break;
			        }
                }
                else if(modchn >= evc[vips-2])
                {
                    if(modchn < vip[vips-1])     /*Numbers of x loop-Grade section*/
                        no2 = (short)Math.Ceiling((vip[vips - 1] - evc[vips - 2]) / inc);
			        else
                        no2 = (short)Math.Ceiling((modchn - evc[vips - 2]) / inc);

			        for(x=0;x<=no2+1;x++)
                    //if (0<=no2+1)
			        {
				        gdist=modchn-vip[m];               /*get exact distance*/

				        ftl=level[m]+(grad[m+1]*100*gdist);

				        ok=1;

				        if(pts > 0)
				        {
					        last_sx = sx;
					        last_sy = sy;
				        }
				        sx = modchn;
				        sy = ftl;
				        write_drg();
				        pts++;

				        //break;
			        }
                }
                if(ok==0)
		        m++;

		        if(endflag==1)
		        break;

		        if(ok==1)
		        modchn += inc;

		        if(modchn > endchn)
		        {
			        modchn=endchn;
			        endflag=1;
		        }
            } /*End of while 1 loop*/
        }

        void write_valign()
        {
            short elno, eltype, turn, gi;
            double sc, ec, t1, t2, ipx, ipy, l1, l2, xs, ys, xe, ye, g1, g2, del;
            
            elemnum = 0;
            elno = 0;
            eltype = 0;
            turn = 0;
            vcl1[0] = 0.0;
            vcl2[0] = 0.0;
            vcl1[vips - 1] = 0.0;
            vcl2[vips - 1] = 0.0;


            for (gi = 2; gi < vips; gi++)
            {
                elno++;
                eltype = 1;
                sc = vip[gi - 2] + vcl2[gi - 2];
                ec = vip[gi - 1] - vcl1[gi - 1];
                ipx = vip[gi - 1];
                ipy = level[gi - 1];
                t1 = vcl1[gi - 1];
                t2 = vcl2[gi - 1];
                l1 = (ec - sc) / 2.0; // only for straight grades
                l2 = (ec - sc) / 2.0;
                g1 = (level[gi - 1] - level[gi - 2]) / (vip[gi - 1] - vip[gi - 2]);
                g2 = (level[gi] - level[gi - 1]) / (vip[gi] - vip[gi - 1]);
                xs = sc;
                ys = level[gi - 2] + g1 * vcl2[gi - 2];
                xe = ec;
                ye = level[gi - 1] + (vcl1[gi - 1]) * (-1.0 * g1);
                del = (g2 - g1) * 100.0;

                if (g1 >= 0 && g2 <= 0)
                    turn = 2;
                else if (g1 <= 0 && g2 >= 0)
                    turn = 1;
                else if (g1 >= 0 && g2 >= 0 && g1 > g2)
                    turn = 2;
                else if (g1 >= 0 && g2 >= 0 && g1 < g2)
                    turn = 1;
                else if (g1 <= 0 && g2 <= 0 && Math.Abs(g1) > Math.Abs(g2))
                    turn = 1;
                else if (g1 <= 0 && g2 <= 0 && Math.Abs(g1) < Math.Abs(g2))
                    turn = 2;

                elemnum++;
                CValignFilData data = new CValignFilData();
                data.modnam = modnam;
                data.stglbl = stglbl;
                data.elno = elemnum;
                data.eltype = eltype;
                data.chn1 = sc;
                data.chn2 = ec;
                data.hipx = ipx;
                data.hipy = ipy;
                data.t1 = t1;
                data.t2 = t2;
                data.rad = l1;
                data.l = l2;
                data.xs = xs;
                data.ys = ys;
                data.xe = xe;
                data.ye = ye;
                data.b1 = (g1 * 100);
                data.b2 = (g2 * 100);
                data.del = del;
                data.turn = turn;


                fp2.WriteLine(data.ToString());
                //fprintf(fp2, "%s %s %d %d %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %d\n", modnam, stglbl, elemnum, eltype, sc, ec, ipx, ipy, t1, t2, l1, l2, xs, ys, xe, ye, g1 * 100, g2 * 100, del, turn);


                // Vertical Curve element

                elno++;
                eltype = 3;
                sc = vip[gi - 1] - vcl1[gi - 1];
                ec = vip[gi - 1] + vcl2[gi - 1];
                ipx = vip[gi - 1];
                ipy = level[gi - 1];
                t1 = vcl1[gi - 1];
                t2 = vcl2[gi - 1];
                if (vcl1[gi - 1] == 0.0)
                {
                    l1 = vcl2[gi - 1] / 2.0;
                    l2 = vcl2[gi - 1] / 2.0;
                }
                else
                {
                    l1 = vcl1[gi - 1];
                    l2 = vcl2[gi - 1];
                }
                g1 = (level[gi - 1] - level[gi - 2]) / (vip[gi - 1] - vip[gi - 2]);
                g2 = (level[gi] - level[gi - 1]) / (vip[gi] - vip[gi - 1]);
                xs = sc;
                ys = level[gi - 1] + (0 - g1) * vcl1[gi - 1];
                xe = ec;
                ye = level[gi - 1] + g2 * vcl2[gi - 1];
                del = (g2 - g1) * 100.0;

                if (g1 >= 0 && g2 <= 0)
                    turn = 2;
                else if (g1 <= 0 && g2 >= 0)
                    turn = 1;
                else if (g1 >= 0 && g2 >= 0 && g1 > g2)
                    turn = 2;
                else if (g1 >= 0 && g2 >= 0 && g1 < g2)
                    turn = 1;
                else if (g1 <= 0 && g2 <= 0 && Math.Abs(g1) > Math.Abs(g2))
                    turn = 1;
                else if (g1 <= 0 && g2 <= 0 && Math.Abs(g1) < Math.Abs(g2))
                    turn = 2;

                elemnum++;
                data = new CValignFilData();
                data.modnam = modnam;
                data.stglbl = stglbl;
                data.elno = elemnum;
                data.eltype = eltype;
                data.chn1 = sc;
                data.chn2 = ec;
                data.hipx = ipx;
                data.hipy = ipy;
                data.t1 = t1;
                data.t2 = t2;
                data.rad = l1;
                data.l = l2;
                data.xs = xs;
                data.ys = ys;
                data.xe = xe;
                data.ye = ye;
                data.b1 = (g1 * 100);
                data.b2 = (g2 * 100);
                data.del = del;
                data.turn = turn;

                fp2.WriteLine(data.ToString());
                //fprintf(fp2, "%s %s %d %d %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %d\n", modnam, stglbl, elemnum, eltype, sc, ec, ipx, ipy, t1, t2, l1, l2, xs, ys, xe, ye, g1 * 100, g2 * 100, del, turn);


                if (gi == vips - 1)
                {
                    // End straight grade element

                    //tmp1=vip[gi-1];
                    //tmp2=vcl2[gi-1];

                    elno++;
                    eltype = 1;
                    sc = vip[gi - 1] + vcl2[gi - 1];
                    ec = vip[gi];
                    ipx = vip[gi];
                    ipy = level[gi];
                    t1 = vcl1[gi - 1];
                    t2 = vcl2[gi - 1];
                    l1 = (ec - sc) / 2.0;
                    l2 = (ec - sc) / 2.0;
                    g1 = (level[gi - 1] - level[gi - 2]) / (vip[gi - 1] - vip[gi - 2]);
                    g2 = (level[gi] - level[gi - 1]) / (vip[gi] - vip[gi - 1]);
                    xs = sc;
                    ys = level[gi - 1] + g1 * vcl2[gi - 1];
                    xe = ec;
                    ye = level[gi];
                    del = (g2 - g1) * 100.0;

                    if (g1 >= 0 && g2 <= 0)
                        turn = 2;
                    else if (g1 <= 0 && g2 >= 0)
                        turn = 1;
                    else if (g1 >= 0 && g2 >= 0 && g1 > g2)
                        turn = 2;
                    else if (g1 >= 0 && g2 >= 0 && g1 < g2)
                        turn = 1;
                    else if (g1 <= 0 && g2 <= 0 && Math.Abs(g1) > Math.Abs(g2))
                        turn = 1;
                    else if (g1 <= 0 && g2 <= 0 && Math.Abs(g1) < Math.Abs(g2))
                        turn = 2;

                    elemnum++;
                    data = new CValignFilData();
                    data.modnam = modnam;
                    data.stglbl = stglbl;
                    data.elno = elemnum;
                    data.eltype = eltype;
                    data.chn1 = sc;
                    data.chn2 = ec;
                    data.hipx = ipx;
                    data.hipy = ipy;
                    data.t1 = t1;
                    data.t2 = t2;
                    data.rad = l1;
                    data.l = l2;
                    data.xs = xs;
                    data.ys = ys;
                    data.xe = xe;
                    data.ye = ye;
                    data.b1 = (g1 * 100);
                    data.b2 = (g2 * 100);
                    data.del = del;
                    data.turn = turn;

                    fp2.WriteLine(data.ToString());
                    //fprintf(fp2, "%s %s %d %d %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %d\n", modnam, stglbl, elemnum, eltype, sc, ec, ipx, ipy, t1, t2, l1, l2, xs, ys, xe, ye, g1 * 100, g2 * 100, del, turn);
                }//gi == item-1
            }// for loop
            fp2.Close();
        }

        void write_drg()
        {
            Eldtype eld = new Eldtype();
            Linetype line = new Linetype();

            if (pts != 0)
            {
                if (index == 0) 
                    line.elatt = 0;
                else 
                    line.elatt = 1;
                
                line.scatt = 1;
                line.layer = "$$$$_4";
                line.laatt = 1;
                line.x1 = last_sx;
                line.y1 = last_sy;
                line.z1 = 0.0;
                line.x2 = sx;
                line.y2 = sy;
                line.z2 = 0.0;
                line.color = 4;
                line.style = 1;
                line.width = 1;
                line.label =  modnam + ":" + stglbl;
             

                eld.Code = 1;
                fp3.Write(eld.Code);

                line.ToStream(fp3);
                
                index = 1;
            }
        }

        #region STATIC METHODS
        public static CValignInfo[] ReadValigns(string strValignFilePath)
        {
            List<CValignInfo> listvalign = new List<CValignInfo>();

            if (File.Exists(strValignFilePath))
            {
                StreamReader readerSrc = new StreamReader(strValignFilePath);

                string strLastModelName = "";
                string strLastStringLabel = "";
                CValignInfo valigninfo = null;
                while (readerSrc.EndOfStream == false)
                {
                    string strline = readerSrc.ReadLine();
                    CValignFilData fildata = CValignFilData.Parse(strline);

                    if (fildata != null)
                    {
                        if (strLastModelName != fildata.modnam || strLastStringLabel != fildata.stglbl)
                        {
                            valigninfo = new CValignInfo();
                            valigninfo.ModelName = fildata.modnam;
                            valigninfo.StringLabel = fildata.stglbl;
                            listvalign.Add(valigninfo);

                            strLastModelName = fildata.modnam;
                            strLastStringLabel = fildata.stglbl;
                        }
                        valigninfo.DataList.Add(fildata);
                    }                    
                }

                readerSrc.Close();
            }
            return listvalign.ToArray();
        }

        public static void SaveValign(string strWorkingDirPath)
        {
            StreamReader readerSrc = new StreamReader(Path.Combine(strWorkingDirPath, "VALIGN.TMP"));
            List<CValignFilData> listTempValign = new List<CValignFilData>();
            while (readerSrc.EndOfStream == false)
            {
                string strlineSrc = readerSrc.ReadLine();
                CValignFilData filDataTemp = CValignFilData.Parse(strlineSrc);
                if (filDataTemp != null)
                {
                    listTempValign.Add(filDataTemp);
                }
            }
            readerSrc.Close();

            //If any data present in temp file
            if (listTempValign.Count > 0)
            {
                string strValignFilePath = Path.Combine(strWorkingDirPath, "VALIGN.FIL");
                CValignInfo[] infoarr = CValignUtil.ReadValigns(strValignFilePath);
                StreamWriter writerDesti = new StreamWriter(strValignFilePath, false);
                foreach (CValignInfo info in infoarr)
                {
                    foreach (CValignFilData filData in info.DataList)
                    {
                        if (filData.modnam != listTempValign[0].modnam || filData.stglbl != listTempValign[0].stglbl)
                        {
                            writerDesti.WriteLine(filData.ToString());
                        }
                    }
                }

                //Now write the data from temp file
                foreach (CValignFilData filData in listTempValign)
                {
                    writerDesti.WriteLine(filData.ToString());
                }

                writerDesti.Close();
            }
        }
        #endregion

    }
}
