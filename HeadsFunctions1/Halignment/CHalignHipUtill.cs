using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HeadsUtils.Interfaces;
using HeadsUtils;

namespace HeadsFunctions1.Halignment
{
    //internal class CHalignHipUtil
    public class CHalignHipUtil
    {
        #region INIT DATA
        string m_strWorkingDirPath = "";
        
        string modnam = "";// Model
        string stglbl = "";// String

        int error_flag = 0;

        short turn = 0;
        short last_turn = 0;
        short elno = 0;
        short eltype = 1;
        short ip = 1;
        short code = 0;
        short hips = 0;
        short ptsc = 0;
        short pts = 0;

        double[] x = new double[500];
        double[] y = new double[500];

        double r9 = 0;
        double x1 = 0;
        double y1 = 0;
        double x2 = 0;
        double y2 = 0;
        double x3 = 0;
        double y3 = 0;
        double rad = 0;
        double ls1 = 0;
        double ls2 = 0;
        double last_ls1 = 0;
        double last_ls2 = 0;
        double last_rad = 0;



        double B1 = 0;
        double B2 = 0;
        double del = 0;
        double inc = 10.0;

        double t1 = 0;
        double t2 = 0;
        double lc = 0;
        double lt1 = 0;
        double lt2 = 0;
        double xc1 = 0;
        double xc2 = 0;
        double yc1 = 0;
        double yc2 = 0;

        double temp = 0;

        
        double chn1 = 0;
        double chn2 = 0;
        double hipx = 0;
        double hipy = 0;
        double l = 0;
        double b1 = 0;
        double b2 = 0;
        double xs = 0;
        double ys = 0;
        double xe = 0;
        double ye = 0;
        double xm = 0;
        double ym = 0;

        BinaryReader fp1 = null;
        StreamWriter fpm = null;
        StreamWriter fp2 = null;
        BinaryWriter fp3 = null;

        #endregion
        

        public CHalignHipUtil()
        {

        }
        //const double r9 = 57.29577951;
        public bool WriteHIPFile(string strPath, CHalFile filedata)
        {
            bool bSuccess = false;
            BinaryWriter bw = new BinaryWriter(new FileStream(strPath, FileMode.Create), Encoding.Default);
            if (bw != null)
            {
                short sTotalHips = (short)filedata.ListHip.Count;
                bw.Write(filedata.Code);//Line code
                bw.Write(filedata.AcceptCode);//Accept code
                bw.Write(ViewerUtils.ConvertStringToByteArray(filedata.ModelName, 30));//Model name
                bw.Write(ViewerUtils.ConvertStringToByteArray(filedata.StringLevel, 20));//String level

                bw.Write(sTotalHips);//Total HIPs
                bw.Write(filedata.StartChainage);//Start Chain
                bw.Write(filedata.ChainageInterval);//Chainage Interval

                for (short iIndex = 0; iIndex < sTotalHips; iIndex++)
                {
                    CHHipData hhipObj = filedata.ListHip[iIndex];

                    short sTotalHIP = (short)(iIndex + 1);
                    bw.Write(sTotalHIP);//Hip no
                    bw.Write(hhipObj.xvalue);
                    bw.Write(hhipObj.yvalue);
                    bw.Write(hhipObj.radius);
                    bw.Write(hhipObj.leadtrans);
                    bw.Write(hhipObj.trailtrans);
                }
                bw.Close();
                bSuccess = true;
            }
            return bSuccess;
        }

        public bool ReadHIPFile(string strPath, out CHalFile filedata)
        {
            bool bSuccess = false;
            filedata = new CHalFile();
            BinaryReader br = new BinaryReader(new FileStream(strPath, FileMode.Open, FileAccess.Read), Encoding.Default);
            if (br != null)
            {
                filedata.Code = br.ReadInt16();//Line code
                filedata.AcceptCode = br.ReadInt16();//Accept code
                filedata.ModelName = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(30));//Model name
                filedata.StringLevel = ViewerUtils.ConvertCharArrayToString(br.ReadBytes(20));//String level

                short sTotalHIP = br.ReadInt16();//Total HIPs
                filedata.StartChainage = br.ReadDouble();//Start Chain
                filedata.ChainageInterval = br.ReadDouble();//Chainage Interval

                for (short sCnt = 0; sCnt < sTotalHIP; sCnt++)
                {
                    CHHipData hhipObj = new CHHipData();
                    short iHipNo = br.ReadInt16();
                    hhipObj.xvalue = br.ReadDouble();
                    hhipObj.yvalue = br.ReadDouble();
                    hhipObj.radius = br.ReadDouble();
                    hhipObj.leadtrans = br.ReadDouble();
                    hhipObj.trailtrans = br.ReadDouble();

                    filedata.ListHip.Add(hhipObj);
                }

                br.Close();
                bSuccess = true;
            }
            return bSuccess;
        }
        
        public bool FuncMain(string strWorkingDirPath)
        {
            bool bSuccess = false;

            short HAL1_code = 0;
            short HAL1_acceptcode = 0;

            this.read_default();

            this.m_strWorkingDirPath = strWorkingDirPath;
            string strHAL1TmpPath = Path.Combine(strWorkingDirPath, "HAL1.tmp");
            if (File.Exists(strHAL1TmpPath))
            {
                fp1 = new BinaryReader(new FileStream(strHAL1TmpPath, FileMode.Open, FileAccess.Read), Encoding.Default);
                HAL1_code = fp1.ReadInt16();//Line code;
                HAL1_acceptcode = fp1.ReadInt16();//Accept code;
                fp1.Close();

                fp2 = new StreamWriter(Path.Combine(strWorkingDirPath,"halign.tmp"));
                fp3 = new BinaryWriter(new FileStream(Path.Combine(strWorkingDirPath, "HAL2.tmp"), FileMode.Create), Encoding.Default);
                code = HAL1_code;
                switch (HAL1_code)
                {
                    case 1:
                    {
                        if (hip_method() == true)
                        {
                            write_end_point();
                            bSuccess = true;
                        }                        
                        break;
                    }
                    case 2:
                    {
                        //element_method();
                        //write_end_point();
                        break;
                    }
                }
                fp2.Close();
                fp3.Close();

                if (HAL1_acceptcode == 1)
                {
                    save_halign();
                }                
            }
            else
            {
                throw new Exception("Failed to open file : " + strHAL1TmpPath);
            }

            return bSuccess;
        }

        #region PRIVATE MEMBERS
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

        private bool hip_method()
        {
            bool bSuccess = true;

            fp1 = new BinaryReader(new FileStream(Path.Combine(this.m_strWorkingDirPath, "HAL1.tmp"), FileMode.Open, FileAccess.Read), Encoding.Default);

            CHalFile filedata = new CHalFile();
            filedata.Code = fp1.ReadInt16();//Line code
            filedata.AcceptCode = fp1.ReadInt16();//Accept code
            filedata.ModelName = ViewerUtils.ConvertCharArrayToString(fp1.ReadBytes(30));//Model name
            filedata.StringLevel = ViewerUtils.ConvertCharArrayToString(fp1.ReadBytes(20));//String level
            short sTotalHIP = fp1.ReadInt16();//Total HIPs
            filedata.StartChainage = fp1.ReadDouble();//Start Chain
            filedata.ChainageInterval = fp1.ReadDouble();//Chainage Interval



            modnam = filedata.ModelName;
            stglbl = filedata.StringLevel;
            hips = sTotalHIP;
            chn1 = filedata.StartChainage;
            inc = filedata.ChainageInterval;
            ptsc = 0;
            pts = 0;

            fpm = new StreamWriter(Path.Combine(this.m_strWorkingDirPath, "ERRORMESSAGES.TXT"), true);
            fpm.WriteLine();
            fpm.WriteLine("    *************************************************");
            fpm.WriteLine("    Program [HEADS:Design:HALIGNMENT] was run on : " + DateTime.Now.ToString());

            for (ip = 0; ip < hips - 2; ip++)
            {
                hip_input();

                bearings();

                deflection();

                tangent_points();

                if (error_flag != 0)
                {
                    break;
                }

                if (lt1 > 0)
                {
                    elno++;
                    eltype = 1;
                    //chn1 = chn1;
                    hipx = x2;
                    hipy = y2;
                    l = lt1;

                    if (ip == 0)
                    { 
                        xs = x1; 
                        ys = y1; 
                    }
                    else
                    { 
                        xs = xe; 
                        ys = ye; 
                    }
                    b1 = B1;

                    element();
                }

                if (ls1 > 0)
                {
                    elno++;
                    eltype = 2;
                    chn1 = chn2;
                    hipx = x2;
                    hipy = y2;
                    l = ls1;
                    xs = xe; ys = ye;
                    b1 = b2;

                    element();
                }


                if (lc > 0)
                {
                    elno++;
                    eltype = 3;
                    chn1 = chn2;
                    hipx = x2;
                    hipy = y2;
                    l = lc;
                    xs = xe; ys = ye;
                    b1 = b2;

                    element();
                }

                if (ls2 > 0)
                {
                    elno++;
                    eltype = 4;
                    chn1 = chn2;
                    hipx = x2;
                    hipy = y2;
                    l = ls2;
                    xs = xe; ys = ye;
                    b1 = b2;

                    element();
                }

                x1 = xe; y1 = ye;
                xm = x2; ym = y2;
                x2 = x3; y2 = y3;

                rad = last_rad;
                ls1 = last_ls1;
                ls2 = last_ls2;
                turn = last_turn;

                chn1 = chn2;
                B1 = b2;
            }

            if (error_flag == 0)
            {
                if (lt2 > 0)
                {
                    elno++;
                    eltype = 1;
                    chn1 = chn2;
                    hipx = xm;
                    hipy = ym;
                    l = lt2;
                    xs = xe; ys = ye;
                    b1 = B2;

                    element();
                }
            }
            
            
            fpm.WriteLine("    *************************************************");
            fpm.Close();
            fp1.Close();
            
            if (error_flag == 1)
            {
                System.Windows.Forms.MessageBox.Show("Check the file ERRORMESSAGES.TXT for all the details", HeadsUtils.Constants.ProductName);
                bSuccess = false;
            }
            else
            {
                bSuccess = true;
            }

            return bSuccess;
        }

        private void hip_input()
        {
            CHHipData hhipObj = null;
            short sHipNoRaw = 0;
            short hipno;
            if(ip == 0)
            {
                hhipObj = new CHHipData();
                sHipNoRaw = fp1.ReadInt16();
                hhipObj.xvalue = fp1.ReadDouble();
                hhipObj.yvalue = fp1.ReadDouble();
                hhipObj.radius = fp1.ReadDouble();
                hhipObj.leadtrans = fp1.ReadDouble();
                hhipObj.trailtrans = fp1.ReadDouble();

                hipno = sHipNoRaw;
                x1 = hhipObj.xvalue;
                y1 = hhipObj.yvalue;
                rad = hhipObj.radius;
                ls1 = hhipObj.leadtrans;
                ls2 = hhipObj.trailtrans;

                hhipObj = new CHHipData();
                sHipNoRaw = fp1.ReadInt16();
                hhipObj.xvalue = fp1.ReadDouble();
                hhipObj.yvalue = fp1.ReadDouble();
                hhipObj.radius = fp1.ReadDouble();
                hhipObj.leadtrans = fp1.ReadDouble();
                hhipObj.trailtrans = fp1.ReadDouble();

                hipno = sHipNoRaw;
                x2 = hhipObj.xvalue;
                y2 = hhipObj.yvalue;
                rad = hhipObj.radius;
                ls1 = hhipObj.leadtrans;
                ls2 = hhipObj.trailtrans;

                if (rad < 0)
                {
                    rad = 0.0 - rad;
                    turn = 1;
                }
                else
                {
                    turn = 2;
                }
            }

            hhipObj = new CHHipData();
            sHipNoRaw = fp1.ReadInt16();
            hhipObj.xvalue = fp1.ReadDouble();
            hhipObj.yvalue = fp1.ReadDouble();
            hhipObj.radius = fp1.ReadDouble();
            hhipObj.leadtrans = fp1.ReadDouble();
            hhipObj.trailtrans = fp1.ReadDouble();

            hipno = sHipNoRaw;
            x3 = hhipObj.xvalue;
            y3 = hhipObj.yvalue;   
            last_rad = hhipObj.radius;        
            last_ls1 = hhipObj.leadtrans;
            last_ls2 = hhipObj.trailtrans;

            if (last_rad < 0)
            {
                last_rad = 0.0 - last_rad;
                last_turn = 1;
            }
            else
            {
                last_turn = 2;
            }
        }

        private void bearings()
        {
            double th = 0;
            int direction = 0;

            if (x1 == x2) x2 += 0.001;
            if (y1 == y2) y2 += 0.001;

            th = (Math.Atan((y2 - y1) / (x2 - x1))) * r9;
            if (th < 0) th = 0.0 - th;

            if (x2 > x1 && y2 > y1) B1 = 90 - th;
            else if (x2 > x1 && y2 < y1) B1 = 90 + th;
            else if (x2 < x1 && y2 < y1) B1 = 270 - th;
            else if (x2 < x1 && y2 > y1) B1 = 270 + th;

            if (x2 == x3) x3 += 0.001;
            if (y2 == y3) y3 += 0.001;

            th = (Math.Atan((y3 - y2) / (x3 - x2))) * r9;
            if (th < 0) th = 0.0 - th;

            if (x3 > x2 && y3 > y2) B2 = 90 - th;
            else if (x3 > x2 && y3 < y2) B2 = 90 + th;
            else if (x3 < x2 && y3 < y2) B2 = 270 - th;
            else if (x3 < x2 && y3 > y2) B2 = 270 + th;



            // Incorporated from prehalign.cpp on 10/10/2005 Sandipan

            if (B2 > B1)
            {
                if (0 <= B1 && B1 <= 90 && 270 <= B2 && B2 <= 360)
                    direction = 1;
                else if (0 <= B1 && B1 <= 90 && 180 <= B2 && B2 <= 270 && B2 > B1 + 180)
                    direction = 1;
                else if (90 <= B1 && B1 <= 180 && 270 <= B2 && B2 <= 360 && B2 > B1 + 180)
                    direction = 1;
                else
                    direction = 2;
            }
            else
            {
                if (270 <= B1 && B1 <= 360 && 0 <= B2 && B2 <= 90)
                    direction = 2;
                else if (180 <= B1 && B1 <= 270 && 0 <= B2 && B2 <= 90 && B1 > B2 + 180)
                    direction = 2;
                else if (270 <= B1 && B1 <= 360 && 90 <= B2 && B2 <= 180 && B1 > B2 + 180)
                    direction = 2;
                else
                    direction = 1;
            }

            if (direction == 1)
            {
                turn = 1;
                rad = Math.Abs(rad);
            }
            else
            {
                turn = 2;
                rad = Math.Abs(rad);
            }
        }

        private void deflection()
        {
            del = B1 - B2;

            if (del < 0.0)
            {
                del = 0.0 - del;
            }
            if (del > 180.0)
            {
                del = 360.0 - del;
            }
        }

        private void tangent_points()
        {
            double th = 0;
            double  th1 = 0;
            double  th2 = 0;
            double  r1 = 0;
            double  k1 = 0;
            double  r2 = 0;
            double  k2 = 0;
            double  d1 = 0;
            double  d2 = 0;
            double  d = 0;
            double  l1 = 0;
            double  l2 = 0;
            double  tb = 0;
            double  q1 = 0;
            double  q2 = 0;

            //char textstr[200];
            //struct ELD eld;
            //struct TEXT text;

            if (ls1 > 0)
            {
                th = ls1 / (2.0 * rad);


                q1 = th;

                th1 = (1 - th * th / 10 + th * th * th * th / 216 - th * th * th * th * th * th / 9360 + th * th * th * th * th * th * th * th / 685440) * ls1;
                th2 = (th / 3 - th * th * th / 42 + th * th * th * th * th / 1320 - th * th * th * th * th * th * th / 75600 + th * th * th * th * th * th * th * th / 66894720) * ls1;

                k1 = th1 - rad * Math.Abs(Math.Sin(th));
                r1 = th2 + rad * (Math.Abs(Math.Cos(th)) - 1);
            }
            else
            {
                k1 = r1 = th = q1 = 0.0;
            }

            if (ls2 > 0)
            {
                th = ls2 / (2.0 * rad);


                q2 = th;

                th1 = (1 - th * th / 10 + th * th * th * th / 216 - th * th * th * th * th * th / 9360 + th * th * th * th * th * th * th * th / 685440) * ls2;
                th2 = (th / 3 - th * th * th / 42 + th * th * th * th * th / 1320 - th * th * th * th * th * th * th / 75600 + th * th * th * th * th * th * th * th / 66894720) * ls2;

                k2 = th1 - rad * Math.Abs(Math.Sin(th));
                r2 = th2 + rad * (Math.Abs(Math.Cos(th)) - 1);
            }
            else
            {
                k2 = r2 = th = q2 = 0.0;
            }

            t1 = (rad + r1) * Math.Abs(Math.Tan(del / (2.0 * r9))) + k1 + (r2 - r1) / (Math.Sin(del / r9));
            t2 = (rad + r2) * Math.Abs(Math.Tan(del / (2.0 * r9))) + k2 - (r2 - r1) / (Math.Sin(del / r9));

            d1 = del / 2.0 - q1 * r9;
            d2 = del / 2.0 - q2 * r9;
            d = d1 + d2;
            lc = (d * rad) / r9;

            l1 = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
            l2 = Math.Sqrt((x3 - x2) * (x3 - x2) + (y3 - y2) * (y3 - y2));

            lt1 = l1 - t1;
            lt2 = l2 - t2;

            error_flag = 0;

            if (t1 > l1 || t2 > l2)
            {
                string strMsg  = "Curve is large at HIP : " 
                    + x2.ToString()
                    + "," + y2.ToString();
                System.Windows.Forms.MessageBox.Show(strMsg, HeadsUtils.Constants.ProductName);
                fpm.WriteLine("    In Process : " + strMsg);

                error_flag = 1;
            }

            if (error_flag == 1)
            {
                //fp3.Close();
                
            }
            else
            {
                tb = B1 + 180;

                xc1 = x2 + t1 * Math.Sin(tb / r9);
                yc1 = y2 + t1 * Math.Cos(tb / r9);

                xc2 = x2 + t2 * Math.Sin(B2 / r9);
                yc2 = y2 + t2 * Math.Cos(B2 / r9);
            }            
        }       
        
        private void element()
        {
            switch (eltype)
            {
                case 1:
                    straight();
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
            write_halign();
            write_drg();
        }

        private void verify_bearing(ref double b)
        {
            if (b < 0)
            {
                b = b + 360;
            } 
            else if (b > 360) 
            {
                b = b - 360;
            }
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
            double F = 0;
            double ab = 0;
            double ths = 0;
            double th = 0;
            double th1 = 0;
            double th2 = 0;
            double l_inc = 0;
            double last_l_inc = 0;

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
                    {
                        break;
                    }
                    else
                    {
                        l_inc = l;
                    }
                }

                last_l_inc = l_inc;

            }// while 

            xe = x[ptsc - 1];
            ye = y[ptsc - 1];
        }

        private void circular()
        {
            double F = 0;
            double ab = 0;
            double th = 0;
            double th1 = 0;
            double l_inc = 0;
            double last_l_inc = 0;

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
            double F = 0;
            double ab = 0;
            double ths = 0;
            double th = 0;
            double th1 = 0;
            double th2 = 0;
            double l_inc = 0;
            double last_l_inc = 0;
            //double tmp = 0;

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
            ptsc = (short)((l / inc)+ 1);
            store_ptsc = ptsc;

            // NEW 
            if (turn == 1) turn = 2;
            else if (turn == 2) turn = 1;

            while (ptsc > 0)
            {
                th = (l_inc * l_inc) / (2.0 * l * rad);


                th1 = (1 - th * th / 10 + th * th * th * th / 216 - th * th * th * th * th * th / 9360 + th * th * th * th * th * th * th * th / 685440) * l_inc;
                th2 = (th / 3 - th * th * th / 42 + th * th * th * th * th / 1320 - th * th * th * th * th * th * th / 75600 + th * th * th * th * th * th * th * th * th / 66894720) * l_inc;

                x[ptsc - 1] = xe + th1 * Math.Sin(b1 / r9) + th2 * Math.Sin((b1 + ab) / r9);
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

        private void write_halign()
        {
            del = b2 - b1;
            if (del < 0) del = 0.0 - del;
            if (del > 360) del = del - 360;
            //string strTemp = "";
            string strModelName = (modnam.Length > 8)? modnam.Substring(0, 8) : modnam;
            string strLabelStr = (stglbl.Length > 4) ? stglbl.Substring(0, 4) : stglbl;

            CHalignFilData FilDataObj = new CHalignFilData();
            FilDataObj.sMod = strModelName;
            FilDataObj.sString = strLabelStr;
            FilDataObj.iSlno = elno;
            FilDataObj.iEltype = eltype;
            FilDataObj.dStartchn = chn1;
            FilDataObj.dEndchn = chn2;
            FilDataObj.dHipx = hipx;
            FilDataObj.dHipy = hipy;
            FilDataObj.dT1 = t1;
            FilDataObj.dT2 = t2;
            FilDataObj.dRadius = rad;
            FilDataObj.dEllength = l;
            FilDataObj.dStartx = xs;
            FilDataObj.dStarty = ys;
            FilDataObj.dEndx = xe;
            FilDataObj.dEndy = ye;
            FilDataObj.dB1 = b1;
            FilDataObj.dB2 = b2;
            FilDataObj.dDeviation = del;
            FilDataObj.iTurn = turn;
           

            fp2.WriteLine(FilDataObj.ToString());
            //fprintf(fp2, "%8s %4s %4d %4d %10.5f %10.5f %10.5f %10.5f %10.5f %10.5f %10.5f %10.5f %10.5f %10.5f %10.5f %10.5f %10.5f %10.5f %10.5f %4d\n", modnam, stglbl, elno, eltype, chn1, chn2, hipx, hipy, t1, t2, rad, l, xs, ys, xe, ye, b1, b2, del, turn);
        }

        private void write_drg()
        {
            short i = 0;
            short ELD_code = 0;
            Linetype line = new Linetype();
            for (i = 1; i < ptsc; i++)
            {
                line.elatt = 0;
                if (i == 1) { line.elatt = 0; }
                else if (i > 1) { line.elatt = 1; }


                line.scatt = 1;
                line.layer = "$$_4";
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
                line.label = modnam;
                line.label = ":";
                line.label = stglbl;

                ELD_code = 1;

                fp3.Write(ELD_code);

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

        /// <summary>
        /// Unreachable code segment
        /// </summary>
        private void save_halign()
        {
            double last_chn1 = 0.0;
            double last_chn2 = 0.0;
            string last_mod = "NULL";
            string last_stg = "NULL";

            string strhalignPath = Path.Combine(this.m_strWorkingDirPath, "halign.tmp");
            if (File.Exists(strhalignPath) == false)
            {
                throw new Exception("Failed to open file : " + strhalignPath );
            }
            
            StreamReader readerfp2 = new StreamReader(strhalignPath);
            StreamWriter writerfp3 = new StreamWriter(Path.Combine(this.m_strWorkingDirPath, "HALIGN.FIL"), true);
            while(readerfp2.EndOfStream == false)
            {
                //int iIndex = 0;
                string strValTSV = readerfp2.ReadLine();
                CHalignFilData FilDataObj = CHalignFilData.Parse(strValTSV);
                //string[] Values = strValTSV.Split(new char[] { '\t' });
                modnam = FilDataObj.sMod;
                stglbl = FilDataObj.sString;
                

                if (last_mod.CompareTo(modnam) == 0 && last_stg.CompareTo(stglbl) == 0)
                {
                    break;
                }

                elno = (short)FilDataObj.iSlno;
                eltype = FilDataObj.iEltype;
                chn1 = FilDataObj.dStartchn;
                chn2 = FilDataObj.dEndchn;
                hipx = FilDataObj.dHipx;
                hipy = FilDataObj.dHipy;
                t1 = FilDataObj.dT1;
                t2 = FilDataObj.dT2;
                rad = FilDataObj.dRadius;
                l = FilDataObj.dEllength;
                xs = FilDataObj.dStartx;
                ys = FilDataObj.dStarty;
                xe = FilDataObj.dEndx;
                ye = FilDataObj.dEndy;
                b1 = FilDataObj.dB1;
                b2 = FilDataObj.dB2;
                del = FilDataObj.dDeviation;
                turn = FilDataObj.iTurn;

                del = b2 - b1;
                if (del < 0) del = del + 360;
                if (del > 360) del = del - 360;

                FilDataObj.sMod = modnam;
                FilDataObj.sString = stglbl;
                FilDataObj.iSlno = elno;
                FilDataObj.iEltype = eltype;
                FilDataObj.dStartchn = chn1;
                FilDataObj.dEndchn = chn2;
                FilDataObj.dHipx = hipx;
                FilDataObj.dHipy = hipy;
                FilDataObj.dT1 = t1;
                FilDataObj.dT2 = t2;
                FilDataObj.dRadius = rad;
                FilDataObj.dEllength = l;
                FilDataObj.dStartx = xs;
                FilDataObj.dStarty = ys;
                FilDataObj.dEndx = xe;
                FilDataObj.dEndy = ye;
                FilDataObj.dB1 = b1;
                FilDataObj.dB2 = b2;
                FilDataObj.dDeviation = del;
                FilDataObj.iTurn = turn;

                writerfp3.WriteLine(FilDataObj.ToString());

                last_chn1 = chn1;
                last_chn2 = chn2;

                last_mod = modnam;
                last_stg = stglbl;

            }

            readerfp2.Close();
            writerfp3.Close();
        }
        #endregion

        #region STATIC MEMBERS
        /// <summary>
        /// This function copied from CHalignDxCode::save_halign()
        /// </summary>
        /// <param name="strWorkingDirPath"></param>
        public static void SaveHalign(string strWorkingDirPath)
        {
            StreamReader readerSrc = new StreamReader(Path.Combine(strWorkingDirPath , "halign.tmp"));
            List<CHalignFilData> listTempHalign = new List<CHalignFilData>();
            while (readerSrc.EndOfStream == false)
            {
                string strlineSrc = readerSrc.ReadLine();
                CHalignFilData filDataTemp = CHalignFilData.Parse(strlineSrc);
                if (filDataTemp != null)
                {
                    listTempHalign.Add(filDataTemp);
                }                
            }
            readerSrc.Close();
            
            //If any data present in temp file
            if (listTempHalign.Count > 0)
            {
                string strHalignFilFilePath = Path.Combine(strWorkingDirPath, "HALIGN.FIL");
                CHIPInfo[] infoarr = CHalignHipUtil.ReadHaligns(strHalignFilFilePath);
                StreamWriter writerDesti = new StreamWriter(strHalignFilFilePath, false);
                foreach (CHIPInfo info in infoarr)
                {
                    foreach (CHalignFilData filData in info.DataList)
                    {
                        if (filData.sMod != listTempHalign[0].sMod || filData.sString != listTempHalign[0].sString)
                        {
                            writerDesti.WriteLine(filData.ToString());
                        }
                    }
                }

                //Now write the data from temp file
                foreach (CHalignFilData filData in listTempHalign)
                {
                    writerDesti.WriteLine(filData.ToString());
                }

                writerDesti.Close();
            }            
        }

        public static CHIPInfo[] ReadHaligns(string strHalignFilePath)
        {
            List<CHIPInfo> listhips = new List<CHIPInfo>();

            if (File.Exists(strHalignFilePath))
            {
                StreamReader readerSrc = new StreamReader(strHalignFilePath);

                string strLastModelName = "";
                string strLastStringLabel = "";
                CHIPInfo hipinfo = null;
                while (readerSrc.EndOfStream == false)
                {                    
                    string strline = readerSrc.ReadLine();
                    CHalignFilData fildata = CHalignFilData.Parse(strline);

                    if (fildata != null)
                    {
                        if (strLastModelName != fildata.sMod || strLastStringLabel != fildata.sString)
                        {
                            hipinfo = new CHIPInfo();
                            hipinfo.ModelName = fildata.sMod;
                            hipinfo.StringLabel = fildata.sString;
                            listhips.Add(hipinfo);

                            strLastModelName = fildata.sMod;
                            strLastStringLabel = fildata.sString;
                        }
                        hipinfo.DataList.Add(fildata);
                    }                    

                    //string[] arrStr = strline.Split(new char[] { '\t' });
                    //if (arrStr.Length == 20)
                    //{
                    //    if (strLastModelName != arrStr[0] || strLastStringLabel != arrStr[1])
                    //    {
                    //        hipinfo = new CHIPInfo();
                    //        hipinfo.ModelName = arrStr[0];
                    //        hipinfo.StringLabel = arrStr[1];
                    //        listhips.Add(hipinfo);

                    //        strLastModelName = arrStr[0];
                    //        strLastStringLabel = arrStr[1];
                    //    }
                    //    hipinfo.DataList.Add(CHalignFilData.Parse(strline));
                    //}
                }

                readerSrc.Close();
            }
            return listhips.ToArray();
        }

        public static List<CHHipData> GenerateHIPData(CHIPInfo hipinfo)
        {
            List<CHHipData> listHhip = null;
            int ln = 0;
            int iLine = 0;
            int iHipArrIndex = 0;
            double nSign = 0;
            CHalignFilData filData = null;

            CHHipData[] hipArr = new CHHipData[hipinfo.DataList.Count];

            for (int i = 0; i < hipinfo.DataList.Count; i++)
            {
                hipArr[i] = new CHHipData();
            }

            ln++;
            filData = hipinfo.DataList[iLine]; iLine++;
            nSign = (filData.iTurn == 1) ? -1 : 1;

            hipArr[iHipArrIndex].xvalue = filData.dStartx;
            hipArr[iHipArrIndex].yvalue = filData.dStarty;
            hipArr[iHipArrIndex].delta = filData.dDeviation * nSign;
            hipArr[iHipArrIndex].radius = 0.0;
            hipArr[iHipArrIndex].leadtrans = 0.0;
            hipArr[iHipArrIndex].trailtrans = 0.0;

            double dLastHIPx = filData.dHipx;
            double dLastHIPy = filData.dHipy;

            while (ln < (hipinfo.DataList.Count - 1))
            {
                ln++;
                filData = hipinfo.DataList[iLine]; iLine++;

                if (ln == 2)
                {
                    if (filData.iEltype != 1)
                    {
                        iHipArrIndex++;

                        nSign = (filData.iTurn == 1) ? -1 : 1;

                        hipArr[iHipArrIndex].xvalue = filData.dHipx;
                        hipArr[iHipArrIndex].yvalue = filData.dHipy;
                        hipArr[iHipArrIndex].delta = filData.dDeviation * nSign;
                        hipArr[iHipArrIndex].radius = 0.0;
                        hipArr[iHipArrIndex].leadtrans = 0.0;
                        hipArr[iHipArrIndex].trailtrans = 0.0;

                    }
                }

                if ((Math.Abs(dLastHIPx - filData.dHipx) < 0.00001) && (Math.Abs(dLastHIPy - filData.dHipy) < 0.00001))
                {
                    // same HIP set
                    if (filData.iEltype == 2) // lead transition
                    {
                        hipArr[iHipArrIndex].leadtrans = filData.dEllength;
                    }
                    else if (filData.iEltype == 3) // circular transition
                    {
                        hipArr[iHipArrIndex].radius = filData.dRadius * nSign;
                    }
                    else if (filData.iEltype == 4) // trail transition
                    {
                        hipArr[iHipArrIndex].trailtrans = filData.dEllength;
                    }
                    else
                    {
                        //Error
                        return listHhip;
                    }

                }
                else
                {
                    // next HIP set
                    iHipArrIndex++;

                    nSign = (filData.iTurn == 1) ? -1 : 1;

                    hipArr[iHipArrIndex].xvalue = filData.dHipx;
                    hipArr[iHipArrIndex].yvalue = filData.dHipy;
                    hipArr[iHipArrIndex].delta = filData.dDeviation * nSign;
                    hipArr[iHipArrIndex].radius = 0.0;
                    hipArr[iHipArrIndex].leadtrans = 0.0;
                    hipArr[iHipArrIndex].trailtrans = 0.0;

                    dLastHIPx = filData.dHipx;
                    dLastHIPy = filData.dHipy;
                }
            }

            iHipArrIndex++;
            filData = hipinfo.DataList[iLine]; iLine++;
            ln++;
            nSign = (filData.iTurn == 1) ? -1 : 1;
            hipArr[iHipArrIndex].xvalue = filData.dEndx;
            hipArr[iHipArrIndex].yvalue = filData.dEndy;
            hipArr[iHipArrIndex].delta = filData.dDeviation * nSign;
            hipArr[iHipArrIndex].radius = 0.0;
            hipArr[iHipArrIndex].leadtrans = 0.0;
            hipArr[iHipArrIndex].trailtrans = 0.0;

            listHhip = new List<CHHipData>();
            for (int ii = 0; ii < (iHipArrIndex + 1); ii++)
            {
                listHhip.Add(hipArr[ii]);
            }

            return listHhip;
        }
        #endregion
    }
}
