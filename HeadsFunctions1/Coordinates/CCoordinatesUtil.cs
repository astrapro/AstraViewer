using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HeadsUtils;

namespace HeadsFunctions1.Coordinates
{
    internal class CCoordinatesUtil
    {
        short elatt = 0;
        double xlate_x, xlate_y, XMAX, YMAX, XMIN, YMIN;
        double rotn, xfa, textsize, textrotn, gsize, tsize;
        double xgridint, ygridint;

        string textstring;
        string modnam, stglbl, path, pathfile;

        //double  xstart, ystart, storextg, ang, lhg1, lha1, tx, ty, sheet_fact;
        //double lhg2, lha2, lbx, lby, rbx, rby, ltx, lty, rtx, rty, xoff, yoff;
        //double inchy, incvx, incvy, txl, tyl, txr, tyr, txb, tyb, txt;
        //double msize, last_hx, last_hy, last_vx, last_vy, xmfact, ymfact, pi;
        double x1, y1, x2, y2, r9, hs;

        //short xno, yno, cnt;

        BinaryWriter fp1;

        public CCoordinatesUtil()
        {

        }

        public void Funcmain(string argv1)
        {
            path = argv1;

            //cnt = 0;
            //err_code = 9;

            read_input();

            pathfile = Path.Combine(path, "GRID2.TMP");
            fp1 = new BinaryWriter(new FileStream(pathfile, FileMode.Create), Encoding.Default);

            if (rotn != 0.0)
            {
                textstring = "DRG not by 'Draw String', use 'Minor option 1102'...";
                error_msg();
            }

            grid();

            fp1.Close();
        }

        void read_input()
        {
            Grid grid;

            pathfile = Path.Combine(path, "GRID1.TMP");
            
            if (File.Exists(pathfile) == false)
            {
                textstring = "File %s not found..." + pathfile;
                error_msg();
            }
            BinaryReader br = new BinaryReader(new FileStream(pathfile, FileMode.Open, FileAccess.Read), Encoding.Default);

            grid = Grid.FromStream(br);

            modnam = grid.ModelName;
            stglbl = grid.StringName;
            XMIN = grid.MinX;
            YMIN = grid.MinY;
            XMAX = grid.MaxX;
            YMAX = grid.MaxY;
            xgridint = grid.XInterval;
            ygridint = grid.YInterval;
            tsize = grid.TextSize;
            rotn = grid.Rotation;

            br.Close();
            
            xlate_x = XMIN;
            xlate_y = YMIN;
            hs = (XMAX - XMIN) * 1000.0 / 297.0;
            gsize = tsize;
            r9 = 57.29578;
            //pi = 3.14159;
            xfa = rotn / r9;
            //gcol = 15;
            //tcol = 15;
        }

        void error_msg()
        {
            //err_code = -999;
            x2 = y2 = 100.0;
            textsize = 5.0;
            textrotn = 0.0;
            write_text();
            fp1.Close();
            return;
        }

        void write_text()
        {
            Eldtype eld = new Eldtype();
            TEXTtype text = new TEXTtype();

            text.text = textstring;
            text.Length = (short)textstring.Length;
            text.scatt = 1;
            text.Layer = "$$$$_14";
            text.laatt = 1;
            text.x1 = x2;
            text.y1 = y2;
            text.Size = textsize;
            text.rotn = textrotn;
            text.Color = 14;
            text.style = 1;
            text.Label = modnam + ":" + stglbl;

            eld.Code = 9;

            fp1.Write(eld.Code);
            text.ToStream(fp1);
        }

        void grid()
        {
            short i, j,  sl;
            long ival, idat, xno, yno;
            double xstart, ystart, xend, yend, dat,  xg;
            double yg, storexg, storeyg, xtg, ytg;
            
            string tt, str;            

            ival=(long)xgridint;
            tt = ival.ToString();
            sl = (short)tt.Length;
            
            dat=1;
            for(i=0;i<(sl-1);i++)dat *= 10;

            ival = (long)xlate_x;
            idat = (long)(ival / dat);
            xstart = idat * dat;
            ival = (long)XMAX;
            idat = (long)(ival / dat);
            xend = idat * dat;

            ival = (long)xstart;
            xg = ival;

            dat = 1;
            for (i = 0; i < (sl - 1); i++) dat *= 10;

            ival = (long)xlate_y; idat = (long)(ival / dat); ystart = idat * dat;
            ival = (long)YMAX; idat = (long)(ival / dat); yend = idat * dat;

            ival = (long)ystart;
            yg = ival;

            xno = (long)((XMAX - XMIN) / xgridint + 2); /* 1 for natural roundoff, 1 for <for> loop */
            yno = (long)((YMAX - YMIN) / ygridint + 2);

            storexg = xg;
            storeyg = yg;

            xtg = xstart;
            ytg = ystart;


            for(j=0; j<yno; j++)
            {
                for(i=0; i<xno; i++)
                {
                    x1 = xg - gsize; y1 = yg;
                    x2 = xg + gsize; y2 = yg;

                    write_line();

                    x1 = xg; y1 = yg - gsize;
                    x2 = xg; y2 = yg + gsize;

                    write_line();

                    if (j == 0)
                    {
                        x2 = xg + tsize / 2; y2 = ystart;

                        str = xtg.ToString("0");
                        textstring = "E " + str;
                        
                        textsize = tsize;
                        textrotn = 90.0;

                        write_text();
                    }

                    if (i == 0)
                    {
                        x2 = xstart; y2 = yg + tsize / 2;
                        str = ytg.ToString("0");
                        
                        textstring = "N " + str;
                        textsize = tsize;
                        textrotn = 0.0;

                        write_text();
                    }

                    if (j == yno - 1)
                    {
                        x2 = xg + tsize / 2; y2 = yg; /* yend*/

                        str = xtg.ToString("0");
                        textstring = "E " + str;
                        textsize = tsize;
                        textrotn = -90.0;
                        write_text();
                    }

                    if (i == xno - 1)
                    {
                        x2 = xg; /*xend*/ y2 = yg + tsize / 2;

                        str = ytg.ToString("0");
                        textstring = "N " + str;

                        textsize = tsize;
                        textrotn = 180.0;

                        write_text();
                    }
                    
                    xg += xgridint;
                    xtg += xgridint;
                }
                xg = storexg;
                yg = storeyg;

                yg += ygridint;
                storeyg = yg;

                ytg += ygridint;

                if (j == yno - 2)
                    xtg = xstart;
            }
        }

        void write_line()
        {
            Eldtype eld = new Eldtype();
            Linetype line = new Linetype();

            line.elatt = elatt;
            line.scatt = 1;
            line.layer = "$$$$_14";
            line.laatt = 1;
            line.x1 = x1;
            line.y1 = y1;
            line.z1 = 0;
            line.x2 = x2;
            line.y2 = y2;
            line.z2 = 0;
            line.color = 14;
            line.style = 1;
            line.width = 1;

            line.label =  modnam + ":" + stglbl;

            eld.Code = 1;
            fp1.Write(eld.Code);
            line.ToStream(fp1);
        }



    }
}
