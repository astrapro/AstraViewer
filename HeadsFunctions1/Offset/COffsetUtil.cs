using System;
using System.Collections.Generic;
using System.Text;
using HeadsUtils;
using System.IO;

namespace HeadsFunctions1.Offset
{
    internal class COffsetUtil
    {
        BinaryReader fp1;
        StreamWriter fp2;
        BinaryWriter fp3;
        

        short code, pnts, index, offtyp, cont_ind;//, i, vips, ip,  vipno;
        double chn1, chn2, ho1, ho2, vo1, vo2, se1, se2, chn, mx, my, mz;
        double last_sx, last_sy, last_sz;//, sx, sy, inc, last_mx, last_my, last_mz, temp;
        double offchn, brg, last_x, last_y, last_z, x2, y2, z2, r9, last_chn2;
        string refmod, refstg, offmod, offstg;

        string  path, pathfile;
        //string filstr, drive, dir, file, ext;

        public COffsetUtil()
        {

        }

        public void Funcmain(string argv1)
        {
            
            index = 0;
            path = argv1;

            read_default();

            pathfile = Path.Combine(path, "OFF1.TMP");
            fp1 = new BinaryReader(new FileStream(pathfile, FileMode.Open, FileAccess.Read), Encoding.Default);
            OffsetRec off1 = OffsetRec.FromStream(fp1);
            fp1.Close();


            pathfile = Path.Combine(path, "OFFSET.TMP");
            fp2 = new StreamWriter(pathfile, false);

            pathfile = Path.Combine(path, "OFF2.TMP");
            fp3 = new BinaryWriter(new FileStream(pathfile, FileMode.Create), Encoding.Default);
            
            code = off1.OffsetType;
            switch (off1.OffsetType)
            {
                case 402:
                case 403:
                case 404: 
                    offtyp = off1.OffsetType;
                    offset_method();
                    break;
                default: 
                    break;
            }

            fp2.Close();
            fp3.Close();

            if (off1.AcceptCode == 1)
            {
                save_offset();
            }
        }

        void read_default()
        {
            r9=57.29577951;
            //inc=10.0;
        }

        void offset_method()
        {
            offset_input();

            offset_alignment();

            write_offset();

        }

        void offset_input()
        {
            pathfile = Path.Combine(path, "OFF1.TMP");
            fp1 = new BinaryReader(new FileStream(pathfile, FileMode.Open, FileAccess.Read), Encoding.Default);
            OffsetRec off1 = OffsetRec.FromStream(fp1);
            fp1.Close();


            refmod = off1.ModelName;
            refstg = off1.StringName;
            offmod = off1.OffsetModel;
            offstg = off1.OffsetString;
            chn1 = off1.StartChainage;
            chn2 = off1.EndChainage;
            ho1 = off1.StartHOffset;
            ho2 = off1.EndHOffset;
            vo1 = off1.StartVOffset;
            vo2 = off1.EndVOffset;
            se1 = off1.StartSuper;
            se2 = off1.EndSuper;

            pnts = 0;
        }

        void offset_alignment()
        {
            CLabtype.Type label = CLabtype.Type.Unknown;
            short ok;
            double temp;

            CLabtype lab;
            CPTStype pts = new CPTStype();
            //CTXTtype txt;
            CModType mod = null;
            CStgType stg;

            ok = 0;
            cont_ind = 0;

            if (chn1 > chn2)
            {
                temp = chn2;
                chn2 = chn1;
                chn1 = temp;
            }

            pathfile = Path.Combine(path, "MODEL.FIL");

            if (File.Exists(pathfile) == false) return;
            List<CLabtype> listmodelfil = new List<CLabtype>();

            ViewerUtils.ReadModelFilFile(pathfile, ref listmodelfil);
            for (int iIndex = 0; iIndex < listmodelfil.Count; iIndex++)
            {
                lab = listmodelfil[iIndex];
                label = lab.attr;

                if (label == CLabtype.Type.EndCode) ok = 0;

                else if (label == CLabtype.Type.Model)
                {
                    mod = (CModType)lab.Tag;
                }

                else if (label == CLabtype.Type.String)
                {
                    stg = (CStgType)lab.Tag;

                    if (refmod.ToLower() == mod.name.ToLower() && refstg.ToLower() == stg.label.ToLower())
                        ok = 1;
                }
                else if (label == CLabtype.Type.Point)
                {
                    pts = (CPTStype)lab.Tag;
                    chn = pts.mc;
                    mx = pts.mx;
                    my = pts.my;
                    mz = pts.mz;

                    if (ok == 1)
                    {
                        if (cont_ind == 0)
                        {
                            store_last();

                            lab = listmodelfil[iIndex+1];
                            pts = (CPTStype)lab.Tag;
                            
                            
                            chn = pts.mc;
                            mx = pts.mx;
                            my = pts.my;
                            mz = pts.mz;

                            //fsetpos(fp1, &loc1);
                        }

                        if (last_chn2 == chn)
                            continue;

                        if (last_x == mx)
                            mx += 0.001;

                        if (last_y == my)
                            my += 0.001;

                        if ((chn > chn1 && chn < chn2) && chn != last_chn2)
                        {
                            calc_offset();

                            write_drg();
                        }

                        if (chn >= chn2)
                            break;

                        if (cont_ind == 1)
                            store_last();

                        cont_ind = 1;

                    }	// OK=1

                }	//103
               
            }
            if (ok == 1)
            {
                store_last();
                chn = pts.mc + 0.001;
                mx = pts.mx + 0.001;
                my = pts.my + 0.001;
                mz = pts.mz;
                calc_offset();
                write_drg();
            }
        }

        void store_last()
        {
            last_chn2 = chn;
            last_x = mx;
            last_y = my;
            last_z = mz;
        }

        void calc_offset()
        {
            double ho, vo = 0, se;

            offset_bearing();



            if (cont_ind == 0)
            {
                ho = ho1 + (last_chn2 - chn1) * (ho2 - ho1) / (chn2 - chn1); /* equals to ho1 */
                x2 = last_x + ho * Math.Cos((brg) / r9);
                y2 = last_y - ho * Math.Sin((brg) / r9);
                offchn = last_chn2;
            }
            else
            {
                ho = ho1 + (chn - chn1) * (ho2 - ho1) / (chn2 - chn1);
                x2 = mx + ho * Math.Cos((brg) / r9);
                y2 = my - ho * Math.Sin((brg) / r9);
                offchn = chn;
            }

            if (cont_ind == 0)
            {
                if (offtyp == 402)
                    vo = vo1 + (last_chn2 - chn1) * (vo2 - vo1) / (chn2 - chn1);
                else if (offtyp == 403)
                {
                    se = se1 + (last_chn2 - chn1) * (se2 - se1) / (chn2 - chn1);
                    vo = ho * se;
                }
                if (last_z == -999.999) vo = 0.0;
                z2 = last_z + vo;
            }
            else
            {
                if (offtyp == 402)
                    vo = vo1 + (chn - chn1) * (vo2 - vo1) / (chn2 - chn1);
                else if (offtyp == 403)
                {
                    se = se1 + (chn - chn1) * (se2 - se1) / (chn2 - chn1);
                    vo = ho * se;
                }
                if (mz == -999.999) vo = 0.0;
                z2 = mz + vo;
            }

        }

        void write_drg()
        {
            Eldtype eld = new Eldtype();
            Linetype line = new Linetype();

            if (pnts != 0)
            {
                if (index == 0) line.elatt = 0;
                else line.elatt = 1;

                line.scatt = 1;
                line.layer = "$$$$_2";
                line.laatt = 1;
                line.x1 = last_sx;
                line.y1 = last_sy;
                line.z1 = last_sz;
                line.x2 = x2;
                line.y2 = y2;
                line.z2 = z2;
                line.color = 2;
                line.style = 1;
                line.width = (short)0.35;
                line.label = offmod + ":" + offstg;

                eld.Code = 1;
                fp3.Write(eld.Code);
                line.ToStream(fp3);               

                index = 1;
            }

            last_sx = x2;
            last_sy = y2;
            last_sz = z2;

            pnts++;
        }

        void offset_bearing()
        {
            double th;

            if (last_y == my && mx > last_x) brg = 90;
            else if (last_y == my && mx < last_x) brg = 270;
            else if (last_x == mx && my > last_y) brg = 0;
            else if (last_x == mx && my < last_y) brg = 180;
            else
            {
                th = Math.Atan((my - last_y) / (mx - last_x));
                th = Math.Abs(th) * r9;
                if (mx > last_x && my > last_y) brg = 90 - th;
                else if (mx > last_x && my < last_y) brg = 90 + th;
                else if (mx < last_x && my < last_y) brg = 270 - th;
                else if (mx < last_x && my > last_y) brg = 270 + th;
            }

        }

        void write_offset()
        {
            string strTemp = string.Format("401\tMODEL\t{0}\tSTRING\t{1}\n", refmod, refstg);
            fp2.Write(strTemp);
            strTemp = offtyp.ToString() + '\t'
                + "MODEL" + '\t'
                + offmod + '\t'
                + "STRING" + '\t'
                + offstg + '\t'
                + "CH1" + '\t'
                + chn1.ToString() + '\t'
                + "CH2" + '\t'
                + chn2.ToString() + '\t';           
            
            if (offtyp == 402)
            {
                strTemp += "HO1" + '\t'
                + ho1.ToString() + '\t'
                + "HO2" + '\t'
                + ho2.ToString() + '\t'
                + "VO1" + '\t'
                + vo1.ToString() + '\t'
                + "VO2" + '\t'
                + vo2.ToString();
                fp2.WriteLine(strTemp);
            }
            else if (offtyp == 403)
            {
                strTemp += "HO1" + '\t'
                + ho1.ToString() + '\t'
                + "HO2" + '\t'
                + ho2.ToString() + '\t'
                + "VO1" + '\t'
                + se1.ToString() + '\t'
                + "VO2" + '\t'
                + se2.ToString();
                fp2.WriteLine(strTemp);
            }
            else if (offtyp == 404)
            {
                strTemp += "SE1" + '\t'
                + se1.ToString() + '\t'
                + "SE2" + '\t'
                + se2.ToString();

                fp2.WriteLine(strTemp);
            }
        }

        void save_offset()
        {
            throw new Exception("The method or operation is not implemented.");
            //strcpy(pathfile, path);
            //strcat(pathfile, "offset.tmp");
            //if ((fp2 = fopen(pathfile, "r")) == NULL) no_file_msg();

            //strcpy(pathfile, path);
            //strcat(pathfile, "OFFSET.FIL");
            //fp3 = fopen(pathfile, "a+");

            //save_header();

            //copy_offset();

            //save_footer();

            //fclose(fp2);

            //fclose(fp3);

        }


    }
}
