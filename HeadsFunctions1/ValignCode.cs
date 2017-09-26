using System;
using System.IO;
using System.Collections;
using System.Windows.Forms;

using HeadsUtils;

namespace HeadsFunctions1.Valignment
{
    class ValignCode
    {
        # region private data
        private BinaryReader reader1 = null;
        private StreamReader reader2 = null;
        private StreamWriter writer2 = null;
        private StreamWriter sWriter3 = null;
        private BinaryWriter writer3 = null;
        private StreamWriter writerM = null;
        //FILE *fp1;
        //FILE *fp2;
        //FILE *fp3;
        //FILE *fpm;
        # endregion

        # region public data
        public short i, code, pts, vips, ip, index, vipno;
        public double[] vip = new double[5000];
        public double[] level = new double[5000];
        public double[] vcl1 = new double[5000];
        public double[] vcl2 = new double[5000];
        public double[] grad = new double[5000];
        public double chn1, endchn;
        public double sx, sy, inc, last_sx, last_sy, temp;
        public string modnam; //[20]
        public string stglbl; //[20];
        public long error_flag;
        public string msgstr; //[255];
        public long elemnum;
        public string filstr; //[MAXPATH]
        public string path; //[MAXPATH]
        public string pathfile; //[MAXPATH]
        public string drive; //[MAXPATH]
        public string dir; //[MAXPATH]
        public string file; //[MAXPATH]
        public string ext; //[MAXPATH]
        # endregion

        # region constructors
        public ValignCode()
        {
            // intentionally empty
        }
        # endregion

        # region APIs
        public void Funcmain(string argv1)
        {
            Val1type val1 = new Val1type();

            this.path = argv1;

            this.read_default();
            this.pathfile = Path.Combine(this.path, "val1.tmp");
            if (File.Exists(this.pathfile) == false)
            {
                string mssg = string.Format("Data file {0} not found...", pathfile);
                MessageBox.Show(mssg);
                return;
            }

            //if ((fp1 = fopen(pathfile, "rb")) == NULL)
            this.reader1 = new BinaryReader(new FileStream(this.pathfile, FileMode.Open));
            val1.Code = this.reader1.ReadInt16();
            val1.AcceptCode = this.reader1.ReadInt16();
            val1.ModelName = ViewerUtils.ConvertCharArrayToString(this.reader1.ReadBytes(30));
            val1.StringName = ViewerUtils.ConvertCharArrayToString(this.reader1.ReadBytes(20));
            this.reader1.Close();

            pathfile = Path.Combine(this.path, "valign.tmp");

            //fp2 = fopen(pathfile, "w");
            this.writer2 = new StreamWriter(this.pathfile);

            //strcpy(pathfile, path);
            //strcat(pathfile, "val2.tmp");
            //fp3 = fopen(pathfile, "wb");
            this.pathfile = Path.Combine(this.path, "val2.tmp");
            this.writer3 = new BinaryWriter(new FileStream(this.pathfile, FileMode.Create));

            //time_t ts;
            //char* ptm;
            //time(&ts);
            //ptm = ctime(&ts);
            DateTime dateTime = DateTime.Now;

            //strcpy(pathfile, path);
            //strcat(pathfile, "ERRORMESSAGES.TXT");
            //fpm = fopen(pathfile, "a+");
            this.pathfile = Path.Combine(this.path, "ERRORMESSAGES.TXT");
            this.writerM = new StreamWriter(this.pathfile, true);

            //fprintf(fpm, "\n\n    *************************************************");
            //fprintf(fpm, "\n    Program [HEADS:Design:VALIGNMENT] was run on  %s", ptm);
            this.writerM.Write("\n\n    *************************************************");
            this.writerM.Write(string.Format("\n    Program [HEADS:Design:VALIGNMENT] was run on  {0}", dateTime.ToString()));

            code = val1.Code;
            switch (val1.Code)
            {
                case 1:
                    vip_method();
                    break;
                default:
                    break;
            }

            //fclose(fp2);
            //fclose(fp3);
            this.writer2.Close();
            this.writer3.Close();

            //fprintf(fpm, "\n    *************************************************");
            //fclose(fpm);
            this.writerM.Write("\n    *************************************************");
            this.writerM.Close();

            if (error_flag == 1)
            {
                //AfxMessageBox("Check the file ERRORMESSAGES.TXT for all the details");
                MessageBox.Show("Check the file ERRORMESSAGES.TXT for all the details");
            }

            if (val1.AcceptCode == 1)
            {
                save_valign();
            }
        }

        public void read_default()
        {
            this.inc = 5.0;
        }

        public void vip_method()
        {
            vip_input();

            profile_array();

                                   write_drg_text();

            write_valign();
        }

        public void vip_input()
        {
            Val1type val1 = new Val1type();
            Val2type val2 = new Val2type();
            Val3type val3 = new Val3type();

            //FILE* fptmp;
            StreamWriter writerTmp = null;

            //strcpy(pathfile, path);
            //strcat(pathfile, "val1.tmp");
            this.pathfile = Path.Combine(this.path, "val1.tmp");

            //if ((fp1 = fopen(pathfile, "rb")) == NULL)
            //{
            //    sprintf(msgstr, " Data file %s not found...", pathfile);
            //    AfxMessageBox(msgstr);
            //    return;
            //    //no_file_msg();
            //}
            if (File.Exists(this.pathfile) == false)
            {
                string mssg = string.Format("Data file \"{0}\" not found...", this.pathfile);
                MessageBox.Show(mssg);
                return;
            }
            this.reader1 = new BinaryReader(new FileStream(this.pathfile, FileMode.Open));

            //strcpy(pathfile, path);
            //strcat(pathfile, "val1.txt");
            this.pathfile = Path.Combine(this.path, "val1.txt");

            //if ((fptmp = fopen(pathfile, "w")) == NULL)
            //{
            //    sprintf(msgstr, " Data file %s not found...", pathfile);
            //    AfxMessageBox(msgstr);
            //    return;
            //    //no_file_msg();
            //}

            writerTmp = new StreamWriter(this.pathfile);

            //fread(&val1.Code, sizeof(char), sizeof(val1.Code), fp1);
            //fread(&val1.AcceptCode, sizeof(char), sizeof(val1.AcceptCode), fp1);
            //fread(val1.ModelName, sizeof(char), sizeof(val1.ModelName), fp1);
            //fread(val1.StringName, sizeof(char), sizeof(val1.StringName), fp1);
            val1.Code = this.reader1.ReadInt16();
            val1.AcceptCode = this.reader1.ReadInt16();
            val1.ModelName = ViewerUtils.ConvertCharArrayToString(this.reader1.ReadBytes(30));
            val1.StringName = ViewerUtils.ConvertCharArrayToString(this.reader1.ReadBytes(20));


            //val1.ModelName[8] = NULL;
            //val1.StringName[4] = NULL;
            //strcpy(modnam, val1.ModelName);
            //strcpy(stglbl, val1.StringName);
            //val1.ModelName = val1.ModelName.Substring(0, 7);
            //val1.StringName = val1.StringName.Substring(0, 3);
            this.modnam = val1.ModelName;
            this.stglbl = val1.StringName;

            //fread(&val2.TotalVIPs, sizeof(char), sizeof(val2.TotalVIPs), fp1);
            //fread(&val2.StartChain, sizeof(char), sizeof(val2.StartChain), fp1);
            //fread(&val2.EndChain, sizeof(char), sizeof(val2.EndChain), fp1);
            //fread(&val2.Interval, sizeof(char), sizeof(val2.Interval), fp1);
            val2.TotalVIPs = this.reader1.ReadInt16();
            val2.StartChain = this.reader1.ReadDouble();
            val2.EndChain = this.reader1.ReadDouble();
            val2.Interval = this.reader1.ReadDouble();

            vips = val2.TotalVIPs;
            chn1 = val2.StartChain;
            endchn = val2.EndChain;
            inc = val2.Interval;

            //fprintf(fptmp, "VIP=%8d\n", vips);
            writerTmp.Write(string.Format("VIP={0}\n", this.vips));

            pts = 0;

            for (ip = 0; ip < vips; ip++)
            {
                //fread(&val3.VIPNo, sizeof(char), sizeof(val3.VIPNo), fp1);
                //fread(&val3.VIPChain, sizeof(char), sizeof(val3.VIPChain), fp1);
                //fread(&val3.VIPLevel, sizeof(char), sizeof(val3.VIPLevel), fp1);
                //fread(&val3.vcl1, sizeof(char), sizeof(val3.vcl1), fp1);
                //fread(&val3.vcl2, sizeof(char), sizeof(val3.vcl2), fp1);
                val3.VIPNo = this.reader1.ReadInt16();
                val3.VIPChain = this.reader1.ReadDouble();
                val3.VIPLevel = this.reader1.ReadDouble();
                val3.vcl1 = this.reader1.ReadDouble();
                val3.vcl2 = this.reader1.ReadDouble();

                vip[ip] = 0.0;
                level[ip] = 0.0;
                vcl1[ip] = 0.0;
                vcl2[ip] = 0.0;

                vipno = val3.VIPNo;
                vip[ip] = val3.VIPChain;
                level[ip] = val3.VIPLevel;
                vcl1[ip] = val3.vcl1;
                vcl2[ip] = val3.vcl2;

                //fprintf(fptmp, "303 %10.5f %10.3f %5.3f\n", vip[ip], level[ip], vcl1[ip] + vcl2[ip]);
                writerTmp.Write(string.Format("303 {0} {1} {2}\n", vip[ip], level[ip], vcl1[ip] + vcl2[ip]));

                if (ip > 0)
                {
                    if (vip[ip] <= vip[ip - 1])
                    {
                        //chain_error();
                        error_flag = 1;
                        //sprintf(msgstr, "Chainage data inconsistent at VIP %d", vipno);
                        //AfxMessageBox(msgstr);
                        //fprintf(fpm, "\n    In Process : %s", msgstr);
                        //fclose(fp3);
                        this.msgstr = string.Format("Chainage data inconsistent at VIP {0}", this.vipno);
                        MessageBox.Show(this.msgstr);
                        writerTmp.Write(string.Format("\n    In Process : {0}", msgstr));
                        this.writer3.Close();
                        return;
                    }

                    grad[ip] = (level[ip] - level[ip - 1]) / (vip[ip] - vip[ip - 1]) / 100.0;
                }

            }

            //fclose(fp1);
            //fclose(fptmp);
            this.reader1.Close();
            writerTmp.Close();

            if (endchn > vip[vips - 1])
            {
                endchn = vip[vips - 1];/*NEW*/
            }
        }

        public void profile_array()
        {
            short l, m, ok, no1, no2, endflag;
            double[] bvc = new double[10000];
            double[] evc = new double[10000];
            double[] ybvc = new double[10000];
            double[] yevc = new double[10000];
            double[] bvclev = new double[10000];
            double[] evclev = new double[10000];
            double[] mv = new double[10000];
            double x, modchn, delta, ftl, gdist, vdist, ydist1, yval1, ydist2, yval2;
            double tmp1, tmp2;

            vcl1[0] = vcl2[0] = 0.0;
            vcl1[vips - 1] = vcl2[vips - 1] = 0.0;

            //char msgstr[255];
            string msgstr;

            for (l = 0; l < vips; l++)
            {
                bvc[l] = vip[l] - vcl1[l];
                ybvc[l] = vcl1[l] * grad[l] * 100;

                evc[l] = vip[l] + vcl2[l];
                yevc[l] = vcl2[l] * grad[l + 1] * 100;


                if (l > 0)
                {
                    if (bvc[l] < evc[l - 1])
                    {
                        error_flag = 1;
                        //sprintf(msgstr, "CURVE OVERLAPED, ERROR AT LINE %4d : Ch.%.3f",l+1,vip[l]);
                        //AfxMessageBox(msgstr);
                        //fprintf(fpm, "\n    In Process : %s", msgstr);
                        ////error_msg();
                        msgstr = string.Format("CURVE OVERLAPED, ERROR AT LINE {0} : Ch.{1}", l + 1, vip[l]);
                        MessageBox.Show(msgstr);
                        this.writerM.Write(string.Format("\n    In Process : {0}", msgstr));
                    }
                }

                if (grad[l] >= 0)
                {
                    bvclev[l] = level[l] - ybvc[l];       /*****/
                }
                else if (grad[l] < 0)
                {
                    bvclev[l] = level[l] - ybvc[l];
                }
                if (grad[l + 1] >= 0)
                {
                    evclev[l] = level[l] + yevc[l];       /*****/
                }
                else if (grad[l + 1] < 0)
                {
                    evclev[l] = level[l] + yevc[l];
                }

                if (l > 0 && l < vips - 1)
                {
                    if (bvc[l] < evc[l - 1])
                    {
                        //chain_error();
                        error_flag = 1;
                        //sprintf(msgstr, "Chainage data inconsistent at VIP %d", vipno);
                        //AfxMessageBox(msgstr);
                        //fprintf(fpm, "\n    In Process : %s", msgstr);
                        //fclose(fp3);
                        msgstr = string.Format("Chainage data inconsistent at VIP {0}", vipno);
                        MessageBox.Show(msgstr);
                        this.writerM.Write(string.Format("\n    In Process : {0}", msgstr));
                        this.writer3.Close();
                        return;
                    }

                    delta = grad[l + 1] - grad[l];

                    if (delta == 0.0)
                    {
                        delta = 0.0001;
                    }

                    tmp1 = vcl1[l];
                    tmp2 = vcl2[l];

                    mv[l] = (vcl1[l] * vcl2[l] * delta) / (200.0 * (vcl1[l] + vcl2[l]));
                }
            }

            last_sx = -999.9;
            last_sy = -999.9;
            endflag = 0;
            m = 0;
            pts = 0;
            modchn = chn1;
            index = 0;

            /***************** fwrite drg at each sx[i], sy[i] ****************************/
            while (true)
            {
                ok = 0;

                if (modchn == vip[0])
                {
                    ftl = level[m];

                    ok = 1;

                    if (pts > 0)
                    {
                        last_sx = sx;
                        last_sy = sy;
                    }
                    sx = modchn;
                    sy = ftl;
                    write_drg();
                    pts++;
                }

                /*NEW*/
                else if (m > 0 && modchn >= evc[m - 1] && modchn < bvc[m])
                {
                    no1 = (short)Math.Ceiling((vip[m] - evc[m - 1]) / inc);
                    /*Numbers of x loop-Grade section*/

                    for (x = 0; x <= no1 + 1; x++)
                    {
                        gdist = modchn - evc[m - 1];      /*get exact distance*/

                        ftl = evclev[m - 1] + (grad[m] * 100.0 * gdist);

                        ok = 1;

                        if (pts > 0)
                        {
                            last_sx = sx;
                            last_sy = sy;
                        }
                        sx = modchn;
                        sy = ftl;
                        write_drg();
                        pts++;

                        break;
                    }

                }
                else if (modchn >= bvc[m] && modchn < vip[m])
                {
                    no1 = (short)(vcl1[m] / inc);  /*Numbers of x loop-Grade section*/

                    for (x = 0; x <= no1 + 1; x++)
                    {
                        vdist = modchn - bvc[m];
                        ydist1 = mv[m] * (vdist / vcl1[m]) * (vdist / vcl1[m]);
                        ydist1 = ydist1 * 10000;
                        yval1 = bvclev[m] + (grad[m] * 100 * (modchn - bvc[m]));
                        ftl = yval1 + ydist1;

                        ok = 1;

                        if (pts > 0)
                        {
                            last_sx = sx;
                            last_sy = sy;
                        }
                        sx = modchn;
                        sy = ftl;
                        write_drg();
                        pts++;

                        break;
                    }

                }

                else if (modchn >= vip[m] && modchn <= evc[m] && m < vips - 1)
                {
                    no2 = (short)(vcl2[m] / inc);  /*Numbers of x loop-Grade section*/

                    for (x = 0; x <= no2 + 1; x++)
                    {
                        vdist = evc[m] - modchn;
                        ydist2 = mv[m] * (vdist / vcl2[m]) * (vdist / vcl2[m]);
                        ydist2 = ydist2 * 10000;
                        yval2 = evclev[m] - (grad[m + 1] * 100 * (evc[m] - modchn));
                        ftl = yval2 + ydist2;

                        if (modchn == evc[m]) ok = 0;
                        else ok = 1;

                        if (pts > 0)
                        {
                            last_sx = sx;
                            last_sy = sy;
                        }
                        sx = modchn;
                        sy = ftl;
                        write_drg();
                        pts++;

                        break;
                    }

                }

                else if (modchn >= evc[vips - 2])
                {
                    if (modchn < vip[vips - 1])     /*Numbers of x loop-Grade section*/
                        no2 = (short)Math.Ceiling((vip[vips - 1] - evc[vips - 2]) / inc);
                    else
                        no2 = (short)Math.Ceiling((modchn - evc[vips - 2]) / inc);

                    for (x = 0; x <= no2 + 1; x++)
                    {
                        gdist = modchn - vip[m];               /*get exact distance*/

                        ftl = level[m] + (grad[m + 1] * 100 * gdist);

                        ok = 1;

                        if (pts > 0)
                        {
                            last_sx = sx;
                            last_sy = sy;
                        }
                        sx = modchn;
                        sy = ftl;
                        write_drg();
                        pts++;

                        break;
                    }

                }

                if (ok == 0)
                    m++;

                if (endflag == 1)
                    break;

                if (ok == 1)
                    modchn += inc;

                if (modchn > endchn)
                {
                    modchn = endchn;
                    endflag = 1;
                }
            } /*End of while 1 loop*/
        }

        public void write_valign()
        {
            // February 23, 2002 by Sandipan Goswami at Kolkata.

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
                //fprintf(fp2, "%s %s %d %d %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %d\n", modnam, stglbl, elemnum, eltype, sc, ec, ipx, ipy, t1, t2, l1, l2, xs, ys, xe, ye, g1 * 100, g2 * 100, del, turn);
                this.writer2.Write(string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12} {13} {14} {15} {16} {17} {18} {19}\n", modnam, stglbl, elemnum, eltype, sc, ec, ipx, ipy, t1, t2, l1, l2, xs, ys, xe, ye, g1 * 100, g2 * 100, del, turn));

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
                //fprintf(fp2, "%s %s %d %d %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %d\n", modnam, stglbl, elemnum, eltype, sc, ec, ipx, ipy, t1, t2, l1, l2, xs, ys, xe, ye, g1 * 100, g2 * 100, del, turn);
                this.writer2.Write( string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12} {13} {14} {15} {16} {17} {18} {19}\n", modnam, stglbl, elemnum, eltype, sc, ec, ipx, ipy, t1, t2, l1, l2, xs, ys, xe, ye, g1 * 100, g2 * 100, del, turn));

                if (gi == vips - 1)
                {
                    // End straight grade element

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

                    //elemnum;
                    //fprintf(fp2, "%s %s %d %d %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %d\n", modnam, stglbl, elemnum, eltype, sc, ec, ipx, ipy, t1, t2, l1, l2, xs, ys, xe, ye, g1 * 100, g2 * 100, del, turn);
                    this.writer2.Write(string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12} {13} {14} {15} {16} {17} {18} {19}\n", modnam, stglbl, elemnum, eltype, sc, ec, ipx, ipy, t1, t2, l1, l2, xs, ys, xe, ye, g1 * 100, g2 * 100, del, turn));

                }	//gi == item-1


            }		// for loop

            //fclose(fp2);
            this.writer2.Close();
        }

        public void write_drg()
        {
            EldType eld = new EldType();
            Linetype line = new Linetype();

            if (pts == 0)
            {
                return;
            }

            //if (index == 0) line.elatt = 0;
            //else line.elatt = 1;
            line.elatt = (short)((index == 0) ? 0 : 1);

            line.scatt = 1;
            //strcpy(line.Layer, "$$$$_4");
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
            line.width = (short)0.35;
            //strcpy(line.Label, modnam);
            //strcat(line.Label, ":");
            //strcat(line.Label, stglbl);
            line.label = modnam + ":" + stglbl;

            eld.Code = 1;
            //fwrite(&eld.Code, sizeof(char), sizeof(eld.Code), fp3);
            this.writer3.Write(eld.Code);

            //fwrite(&line.elatt, sizeof(line.elatt), 1, fp3);
            //fwrite(&line.scatt, sizeof(line.scatt), 1, fp3);
            //fwrite(line.Layer, sizeof(line.Layer), 1, fp3);
            //fwrite(&line.laatt, sizeof(line.laatt), 1, fp3);
            //fwrite(&line.x1, sizeof(line.x1), 1, fp3);
            //fwrite(&line.y1, sizeof(line.y1), 1, fp3);
            //fwrite(&line.z1, sizeof(line.z1), 1, fp3);
            //fwrite(&line.x2, sizeof(line.x2), 1, fp3);
            //fwrite(&line.y2, sizeof(line.y2), 1, fp3);
            //fwrite(&line.z2, sizeof(line.z2), 1, fp3);
            //fwrite(&line.Color, sizeof(line.Color), 1, fp3);
            //fwrite(&line.style, sizeof(line.style), 1, fp3);
            //fwrite(&line.Width, sizeof(line.Width), 1, fp3);
            //fwrite(line.Label, sizeof(line.Label), 1, fp3);

            this.writer3.Write(line.elatt);
            this.writer3.Write(line.scatt);
            this.writer3.Write(line.layer);
            this.writer3.Write(line.laatt);
            this.writer3.Write(line.x1);
            this.writer3.Write(line.y1);
            this.writer3.Write(line.z1);
            this.writer3.Write(line.x2);
            this.writer3.Write(line.y2);
            this.writer3.Write(line.z2);
            this.writer3.Write(line.color);
            this.writer3.Write(line.style);
            this.writer3.Write(line.width);
            this.writer3.Write(line.label);

            index = 1;
        }

        public void write_drg_text()
        {
        }

        public void write_text(double xt, double yt, double size, double rotn, short color, string str)
        {
            EldType eld = new EldType();
            TEXTtype text = new TEXTtype();

            //strcpy(text.text, str);
            text.text = str;
            text.Length = (short)str.Length;
            text.scatt = 1;
            //strcpy(text.Layer, "$$$$_5");
            text.layer = "$$$$_5";
            text.laatt = 1;
            text.x1 = xt;
            text.y1 = yt;
            text.Size = size;
            text.rotn = rotn;
            text.Color = color;
            text.style = 1;
            //strcpy(text.Label, modnam);
            //strcat(text.Label, ":");
            //strcat(text.Label, stglbl);
            text.label = this.modnam = ":" + this.stglbl;

            eld.Code = 9;
            //fwrite(&eld.Code, sizeof(char), sizeof(eld.Code), fp3);
            this.writer3.Write(eld.Code);

            //fwrite(&text.Length, sizeof(text.Length), 1, fp3);
            //fwrite(&text.scatt, sizeof(text.scatt), 1, fp3);
            //fwrite(text.Layer, sizeof(text.Layer), 1, fp3);
            //fwrite(&text.laatt, sizeof(text.laatt), 1, fp3);
            //fwrite(&text.x1, sizeof(text.x1), 1, fp3);
            //fwrite(&text.y1, sizeof(text.y1), 1, fp3);
            //fwrite(&text.Size, sizeof(text.Size), 1, fp3);
            //fwrite(&text.rotn, sizeof(text.rotn), 1, fp3);
            //fwrite(&text.Color, sizeof(text.Color), 1, fp3);
            //fwrite(&text.style, sizeof(text.style), 1, fp3);
            //fwrite(text.text, sizeof(text.text), 1, fp3);
            //fwrite(text.Label, sizeof(text.Label), 1, fp3);

            this.writer3.Write(text.Length);
            this.writer3.Write(text.scatt);
            this.writer3.Write(text.Layer);
            this.writer3.Write(text.laatt);
            this.writer3.Write(text.x1);
            this.writer3.Write(text.y1);
            this.writer3.Write(text.Size);
            this.writer3.Write(text.rotn);
            this.writer3.Write(text.Color);
            this.writer3.Write(text.style);
            this.writer3.Write(text.text);
            this.writer3.Write(text.Label);

        }

        public void save_valign()
        {
            short elno, eltype, turn;
            double sc, ec, t1, t2, ipx, ipy, l1, l2, xs, ys, xe, ye, g1, g2, del;
            double last_sc, last_ec;

            //strcpy(pathfile, path);
            //strcat(pathfile, "valign.tmp");
            //if ((fp2 = fopen(pathfile, "r")) == NULL)
            //{
            //    sprintf(msgstr, " Data file %s not found...", pathfile);
            //    AfxMessageBox(msgstr);
            //    return;
            //    //no_file_msg();
            //}
            this.pathfile = Path.Combine(this.path, "valign.tmp");
            if (File.Exists(this.pathfile) == false)
            {
                this.msgstr = string.Format("Data file \"{0}\" not found...", this.pathfile);
                MessageBox.Show(this.msgstr);
                return;
            }
            this.reader2 = new StreamReader(this.pathfile);

            //strcpy(pathfile, path);
            //strcat(pathfile, "VALIGN.FIL");
            //fp3 = fopen(pathfile, "a+");
            this.pathfile = Path.Combine(this.path, "VALIGN.FIL");
            this.sWriter3 = new StreamWriter(this.pathfile, true);

            last_sc = -999.999;
            last_ec = -999.999;

            //while (!feof(fp3))
            //{
            //    fscanf(fp2, "%s %s %i %i %lf %lf %lf %lf %lf %lf %lf %lf %lf %lf %lf %lf %lf %lf %lf %i", modnam, stglbl, &elno, &eltype, &sc, &ec, &ipx, &ipy, &t1, &t2, &l1, &l2, &xs, &ys, &xe, &ye, &g1, &g2, &del, &turn);
            //    if (sc == last_sc && ec == last_ec)
            //        break;

            //    fprintf(fp3, "%s %s %d %d %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %d\n", modnam, stglbl, elno, eltype, sc, ec, ipx, ipy, t1, t2, l1, l2, xs, ys, xe, ye, g1, g2, del, turn);
            //    last_sc = sc;
            //    last_ec = ec;
            //}
            while (this.reader2.EndOfStream == false)
            {
                string line = this.reader2.ReadLine();
                string[] components = line.Split(' ');

                modnam = components[0];
                stglbl = components[1];
                elno = short.Parse(components[2]);
                eltype = short.Parse(components[3]);
                sc = double.Parse(components[4]);
                ec = double.Parse(components[5]);
                ipx = double.Parse(components[6]);
                ipy = double.Parse(components[7]);
                t1 = double.Parse(components[8]);
                t2 = double.Parse(components[9]);
                l1 = double.Parse(components[10]);
                l2 = double.Parse(components[11]);
                xs = double.Parse(components[12]);
                ys = double.Parse(components[13]);
                xe = double.Parse(components[14]);
                ye = double.Parse(components[15]);
                g1 = double.Parse(components[16]);
                g2 = double.Parse(components[17]);
                del = double.Parse(components[18]);
                turn = short.Parse(components[19]);

                if (sc == last_sc && ec == last_ec)
                {
                    break;
                }

                //fprintf(fp3, "%s %s %d %d %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %d\n", modnam, stglbl, elno, eltype, sc, ec, ipx, ipy, t1, t2, l1, l2, xs, ys, xe, ye, g1, g2, del, turn);
                string dataToWrite = string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11} {12} {13} {14} {15} {16} {17} {18} {19}\n", modnam, stglbl, elno, eltype, sc, ec, ipx, ipy, t1, t2, l1, l2, xs, ys, xe, ye, g1, g2, del, turn);
                this.sWriter3.Write(dataToWrite);

                last_sc = sc;
                last_ec = ec;
            }

            //fclose(fp2);
            //fclose(fp3);
            this.reader2.Close();
            this.sWriter3.Close();
        }

        public void no_file_msg()
        {
            string textstr; //[60]
            EldType eld = new EldType();
            TEXTtype text = new TEXTtype();
            
            textstr = " Data file not found...";
            text.text = textstr;
            text.Length = (short)textstr.Length;
            text.scatt = 1;
            text.layer = "$$$$_15";
            text.laatt = 1;
            text.x1 = 10.0;
            text.y1 = 10.0;
            text.size = 5;
            text.rotn = 0;
            text.Color = 15;
            text.style = 1;
            text.label = this.modnam + ":" + this.stglbl;

            eld.Code = 9;
            this.writer3.Write(eld.Code);
            this.writer3.Write(text.Length);
            this.writer3.Write(text.scatt);
            this.writer3.Write(text.Layer);
            this.writer3.Write(text.laatt);
            this.writer3.Write(text.x1);
            this.writer3.Write(text.y1);
            this.writer3.Write(text.Size);
            this.writer3.Write(text.rotn);
            this.writer3.Write(text.Color);
            this.writer3.Write(text.style);
            this.writer3.Write(text.text);
            this.writer3.Write(text.Label);

            this.writer3.Close();
        }

        public void chain_error()
        {
            string textstr; //[200];

            EldType eld = new EldType();
            TEXTtype text = new TEXTtype();

            //sprintf(textstr, "Chainage data inconsistent at VIP %d", vipno);
            //strcpy(text.text, textstr);
            //text.Length = strlen(textstr);
            textstr = string.Format("Chainage data inconsistent at VIP {0}", vipno);
            text.scatt = 1;
            text.Layer = "$$$$_15";
            text.laatt = 1;
            text.x1 = 10.0;
            text.y1 = 10.0;
            text.Size = 5;
            text.rotn = 0;
            text.Color = 15;
            text.style = 1;
            //strcpy(text.Label, modnam);
            //strcat(text.Label, ":");
            //strcat(text.Label, stglbl);
            text.label = this.modnam + ":" + this.stglbl;

            eld.Code = 9;
            //fwrite(&eld.Code, sizeof(char), sizeof(eld.Code), fp3);
            //fwrite(&text.Length, sizeof(text.Length), 1, fp3);
            //fwrite(&text.scatt, sizeof(text.scatt), 1, fp3);
            //fwrite(text.Layer, sizeof(text.Layer), 1, fp3);
            //fwrite(&text.laatt, sizeof(text.laatt), 1, fp3);
            //fwrite(&text.x1, sizeof(text.x1), 1, fp3);
            //fwrite(&text.y1, sizeof(text.y1), 1, fp3);
            //fwrite(&text.Size, sizeof(text.Size), 1, fp3);
            //fwrite(&text.rotn, sizeof(text.rotn), 1, fp3);
            //fwrite(&text.Color, sizeof(text.Color), 1, fp3);
            //fwrite(&text.style, sizeof(text.style), 1, fp3);
            //fwrite(text.text, sizeof(text.text), 1, fp3);
            //fwrite(text.Label, sizeof(text.Label), 1, fp3);
            this.writer3.Write(eld.Code);
            this.writer3.Write(text.Length);
            this.writer3.Write(text.scatt);
            this.writer3.Write(text.Layer);
            this.writer3.Write(text.laatt);
            this.writer3.Write(text.x1);
            this.writer3.Write(text.y1);
            this.writer3.Write(text.Size);
            this.writer3.Write(text.rotn);
            this.writer3.Write(text.Color);
            this.writer3.Write(text.style);
            this.writer3.Write(text.text);
            this.writer3.Write(text.Label);

            //fclose(fp3);
            this.writer3.Close();
        }

        public void del_tmp()
        {
            this.pathfile = Path.Combine(this.path, "val1.tmp");
            File.Delete(this.pathfile);
        }
        # endregion
    }
}
