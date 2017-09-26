using System;
using System.IO;
using HeadsUtils;
using HeadsUtils.Interfaces;

namespace HeadsFunctions1.Valignment
{
    class DetailsCodeVAL
    {
        # region private data
        IHeadsApplication headsApp = null;

        bool index;
	    
        short turn1,  elemno, eltype, flag;

	    double xlate_x, xlate_y, XMIN, YMIN, textsize, textrotn, rotn, xfa, xta, yta;
	    double R, L, B1, B2, DEL, xst, yst, xed, yed;
        double stchn, endchn, T1, T2, IPX, IPY, last_stchn;
        double last_endchn, xt, yt, last_IPX, last_IPY, xstor, ystor;
        double qb1, qb2;

	    //string filstr; //char[255]
        string path; //char[255]
        //string pathfile; // char[255]
	    string modstr; //char[30]
        string stgstr; //char[20]
        //string textstring; //char[60]
        //string str1; //char[20]
        //string str2; //char[20]
        //string str3; //char[20]
        //string str4; //char[20]
        //string str5; //char[20]
        # endregion

        # region construction / destruction
        public DetailsCodeVAL(IHeadsApplication headsApp)
        {
            this.headsApp = headsApp;
        }
        # endregion

        # region APIs
        void FuncMain(string argv1)
        {
            this.path = argv1;
            //this.errCode = 9; //no error

            this.ReadInput(null);

            string filePath = Path.Combine(this.path, "details2.tmp");
            
            BinaryWriter details2Writer = new BinaryWriter(new FileStream(filePath, FileMode.Create));

            if (this.rotn != 0.0)
            {
                string error = "DRG not by 'Draw String', use 'Minor option(s) 1103 / 1104'...";
                this.ErrorMsg(error, details2Writer);
                details2Writer.Close();
                return;
            }

            filePath = Path.Combine(this.path, "VALIGN.TMP");
            if (File.Exists(filePath) == false)
            {
                filePath = Path.Combine(this.path, "VALIGN.FIL");
            }

            if(File.Exists(filePath) == false)
            {
                string error = string.Format("File {0} not found...", filePath);
                this.ErrorMsg(error, details2Writer);
                details2Writer.Close();
                return;
            }

            StreamReader valignReader = new StreamReader(filePath);

            index = false;
            while (valignReader.EndOfStream == false)
            {
                this.InitParam();
                
                this.DesCurveInput(valignReader);

                if (flag == 0)
                {
                    continue;
                }

                if (stchn == last_stchn && endchn == last_endchn)
                {
                    break;
                }

                this.DrawHips(details2Writer);

                last_stchn = stchn;
                last_endchn = endchn;
                index = true;

            } // end while()

            this.EndHIP(details2Writer);

            details2Writer.Close();
            valignReader.Close();
        }

        bool ReadInput(BinaryWriter writer)
        {
            Detail det = new Detail();
            double r9 = 57.29578;

            string filePath = Path.Combine(this.path, "details1.tmp");

            if(File.Exists(filePath) == false)
            {
                string mssg = string.Format("File {0} not found...", filePath);
                this.ErrorMsg(mssg, writer);
                return false;
            }

            BinaryReader details1TempReader = new BinaryReader(new FileStream(filePath, FileMode.Open));
            det.ModelName = ViewerUtils.ConvertCharArrayToString(details1TempReader.ReadBytes(30));
            det.StringName = ViewerUtils.ConvertCharArrayToString(details1TempReader.ReadBytes(20));
            det.XMin = details1TempReader.ReadDouble();
            det.YMin = details1TempReader.ReadDouble();
            det.TextSize = details1TempReader.ReadDouble();
            det.Rotation = details1TempReader.ReadDouble();

            det.ModelName = det.ModelName.Substring(0, 7);
            det.StringName = det.StringName.Substring(0, 3);

            this.modstr = det.ModelName;
            this.stgstr = det.StringName;
            this.textsize = det.TextSize;
            this.rotn = det.Rotation;
            this.XMIN = det.XMin;
            this.YMIN = det.YMin;

            details1TempReader.Close();

            xfa = rotn / r9;
            xlate_x = XMIN;
            xlate_y = YMIN;

            return true;
        }
        
        void InitParam()
        {
            this.IPX = this.IPY = 0.0;
        }

        void DesCurveInput(StreamReader reader)
        {
            string mdlnam; //char[30];
            string strnam; //char[20];
            int telemno = 0, teltype = 0;
            double tstchn = 0.0, tendchn = 0.0, tIPX = 0.0, tIPY = 0.0;
            double tT1 = 0.0, tT2 = 0.0, tR = 0.0, tL = 0.0;
            double txst = 0.0, tyst = 0.0, txed = 0.0, tyed = 0.0;
            double tB1 = 0.0, tB2 = 0.0, tDEL = 0.0;
            int tturn1 = 0;

            string line = reader.ReadLine();
            string[] components = line.Split(' ');

            mdlnam = components[0];
            strnam = components[1];
            telemno = int.Parse(components[2]);
            teltype = int.Parse(components[3]);
            tstchn = double.Parse(components[4]);
            tendchn = double.Parse(components[5]);
            tIPX = double.Parse(components[6]);
            tIPY = double.Parse(components[7]);
            tT1 = double.Parse(components[8]);
            tT2 = double.Parse(components[9]);
            tR = double.Parse(components[10]);
            tL = double.Parse(components[11]);
            txst = double.Parse(components[12]);
            tyst = double.Parse(components[13]);
            txed = double.Parse(components[14]);
            tyed = double.Parse(components[15]);
            tB1 = double.Parse(components[16]);
            tB2 = double.Parse(components[17]);
            tDEL = double.Parse(components[18]);
            tturn1 = int.Parse(components[19]);

            if ((string.Compare(mdlnam, modstr, true) == 0) && (string.Compare(strnam, stgstr, true) == 0))
            {
                flag = 1; // final 13/09/01 DD
                elemno = (short)telemno;
                eltype = (short)teltype;
                stchn = tstchn;
                endchn = tendchn;
                IPX = tIPX;
                IPY = tIPY;
                T1 = tT1;
                T2 = tT2;
                R = tR;
                L = tL;
                xst = txst;
                yst = tyst;
                xed = txed;
                yed = tyed;
                B1 = tB1;
                B2 = tB2;
                DEL = tDEL;
                turn1 = (short)tturn1;
            }
            else
            {
                flag = 0;
            }
        }
        
        void DrawHips(BinaryWriter writer)
        {
            //zt = 0.0;
            textrotn = 0.0;

            double dVcl, dK;

            CCfgtype cfg = this.headsApp.ConfigParam;

            double dLead = 1.2; // gap between lines
            if (IPX != last_IPX && IPY != last_IPY)
            {
                if (index == false) // Start HIP 
                {
                    xt = xst - xlate_x;
                    yt = yst - xlate_y;

                    this.PlanForm(ref xt, ref yt);

                    string mssg = string.Format("Start chainage / Level: {0}, {1} m", xst, yst);
                    this.TextWrite(mssg, writer);
                }
            }

            if (eltype == 3)
            {
                xt = IPX - xlate_x;
                yt = IPY - xlate_y;
                this.PlanForm(ref xt, ref yt);
                string mssg = string.Format("VIP Chainage = {0} m", IPX);
                xstor = xt;
                ystor = yt;
                this.TextWrite(mssg, writer);
                xt = xstor;
                yt = ystor;

                yt -= textsize / cfg.YMetric * dLead;

                mssg = string.Format("VIP Level = {0} m", IPY);
                xstor = xt;
                ystor = yt;
                this.TextWrite(mssg, writer);
                xt = xstor;
                yt = ystor;

                yt -= textsize / cfg.YMetric * dLead;

                dVcl = R + L;
                if (Math.Abs(R - L) < 0.001)
                {
                    mssg = string.Format("VCL = {0} m", dVcl);
                }
                else
                {
                    mssg = string.Format("VCL1 = {0} m, VCL2 = {1} m", R, L);
                }

                xstor = xt;
                ystor = yt;
                this.TextWrite(mssg, writer);
                xt = xstor;
                yt = ystor;

                yt -= textsize / cfg.YMetric * dLead;

                mssg = string.Format("g1 = {0} %%, g2 = {1} %% ", B1, B2);
                xstor = xt;
                ystor = yt;
                this.TextWrite(mssg, writer);
                yt = ystor;
                xt = xstor;

                yt -= textsize / cfg.YMetric * dLead;

                if (turn1 == 1)
                {
                    R = 0.0 - R;
                }

                mssg = string.Format("Grade Difference = {0} %%", DEL);

                xstor = xt;
                ystor = yt;
                this.TextWrite(mssg, writer);
                xt = xstor;
                yt = ystor;

                yt -= textsize / cfg.YMetric * dLead;

                dK = Math.Abs(dVcl / (B2 - B1));

                if (turn1 == 2)
                {
                    dK *= -1;
                }

                mssg = string.Format("K = {0}", dK);
                xstor = xt;
                ystor = yt;
                this.TextWrite(mssg, writer);
                xt = xstor;
                yt = ystor;
            }

            last_IPX = IPX;
            last_IPY = IPY;
        }
        
        void EndHIP(BinaryWriter writer)
        {
            xt = xed - xlate_x;
            yt = yed - xlate_y;

            xt += 4.0 * textsize;
            yt += 4.0 * textsize;

            this.PlanForm(ref xt, ref yt);
            string mssg = string.Format("End chainage / Level: {0}, {1} m.", xed, yed);		// Sandipan 12/02/2008	Kolkata
            this.TextWrite(mssg, writer);
        }

        void TextWrite(string message, BinaryWriter writer)
        {
            xta = xt;
            xta += xlate_x;

            yta = yt;
            yta += xlate_y;

            this.WriteText(message, writer);
        }
        
        void WCB2QB()
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
        
        private void ErrorMsg(string mssg, BinaryWriter writer)
        {
            //this.errCode = -999;
            this.xta = this.yta = 100.0;
            this.textsize = 5;
            this.textrotn = 0;
            this.WriteText(mssg, writer);
        }

        private void WriteText(string message, BinaryWriter writer)
        {
            if (writer == null)
            {
                return;
            }

            Eldtype eld = new Eldtype();
            TEXTtype text = new TEXTtype();

            text.text = message;
            text.Length = (short)message.Length;
            text.scatt=1;
            text.Layer = "$$$$_15";
            text.laatt=1;
            text.x1=xta;
            text.y1=yta;
            text.Size= this.textsize;
            text.rotn=this.textrotn;
            text.Color=15;
            text.style=1;
            text.Label = this.modstr;
            text.Label += ":" + this.stgstr;
            eld.Code = 9;

            writer.Write(eld.Code);
            writer.Write(text.Length);
            writer.Write(text.scatt);
            writer.Write(text.Layer);
            writer.Write(text.laatt);
            writer.Write(text.x1);
            writer.Write(text.y1);
            writer.Write(text.Size);
            writer.Write(text.rotn);
            writer.Write(text.Color);
            writer.Write(text.style);
            writer.Write(text.text);
            writer.Write(text.Label);
        }

        void PlanForm(ref double xfm, ref double yfm)
        {
            double x_temp, y_temp;

            /* PLANFORM transforms a point's location by rotation xfa +ve is anti-clock. */

            x_temp = xfm;
            y_temp = yfm;

            xfm = x_temp * Math.Cos(xfa) - y_temp * Math.Sin(xfa);
            yfm = y_temp * Math.Sin(xfa) + y_temp * Math.Cos(xfa);

        }
        
        void WriteCFG(BinaryWriter writer)
        {
            Eldtype eld = new Eldtype();
            CCfgtype cfg = new CCfgtype();

            cfg.XMetric = 1;
            cfg.YMetric = 1;
            cfg.XLate_X = 0;
            cfg.XLate_Y = 0;

            cfg.xfa = 0.0;
            cfg.sl = 297.0;
            cfg.sw = 210.0;
            cfg.hs = 1;

            cfg.lm = 10.0;
            cfg.bm = 10.0;
            cfg.rm = 10.0;
            cfg.tm = 10.0;

            eld.Code = 99;
            writer.Write(eld.Code);
            writer.Write(cfg.XMetric);
            writer.Write(cfg.YMetric);
            writer.Write(cfg.XLate_X);
            writer.Write(cfg.XLate_Y);
            writer.Write(cfg.xfa);
            writer.Write(cfg.sl);
            writer.Write(cfg.sw);
            writer.Write(cfg.hs);
            writer.Write(cfg.bm);
            writer.Write(cfg.rm);
            writer.Write(cfg.tm);
            writer.Write(cfg.lm);
        }
        # endregion
    }
}
