using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HeadsUtils;

namespace HeadsFunctions1.Offset.Modelling
{
    internal class COffsetModelUtil
    {
        
        StreamReader fp2;        
        StreamWriter fp4;
        StreamWriter fp5;
        //FILE *fpt;//FILE *fp1;//FILE *fp3;
        CLabtype.Type label = CLabtype.Type.Unknown;
        short ok1, ok2, opnum, ind,  find = 0,type;//i, ,hit,chk,   sno, last_hit;
		short sign_d, offind;//, flag1, last_cnt, index2, quadrant, Q1, Qi, SameString;
	    long ss_found;//rec, rep_flag;//, offsiz,  flag_101;
	    double brg, ho, vo, ch1, ch2, ho1, ho2, vo1, vo2, last_ox;//, chn, last_chn; 
        double last_oy, last_oz, ox, oy, oz;//, last_x, last_y, last_z, offchn, sv_chn;
        double se1, se2, ochn;//, sv_x, sv_y, sv_z, x, y, z ;
	    double chn1, x1, y1, z1, chn2, x2, y2, z2, se;//, x3, y3, x4, y4, last_brg,last_label, last_ochn, last_mch;

        //double schn, sx, sy, sz, last_schn, last_sx, last_sy, last_sz, brg1, brg2;
        //double xi, yi, zi, last_last_schn, last_last_sx, last_last_sy, last_last_sz;
        //double last_ho, last_vo, absho, last_absho;

    	
	    string uline, str1, str2, refmod, refstr, offmod;
		string offstr;//, day, mon, date, hour, year, cal, offstg;
        string last_submod, last_substr, substr = string.Empty, submod = string.Empty; 
	    //string argstr, dum1;
	    //char *months[12];
	    //short ival, aval, ki;
	    double val, chndiff, last_chndiff;

        string path, pathfile;//, filestr, headspath;
	    //string drive, dir, file, ext;
	    string msgstr;
	    string  last_offmod, last_offstr;//last_refmod, last_refstr,

        List<CLabtype> listmodelfilOld = null;
        List<CLabtype> listmodelfilNew = null;

	    //fpos_t loc1, loc2, loc3, loc4, last_loc3, last_last_loc3;
        string m_RefMod, m_RefStr, m_OffMod, m_OffStr;

        public delegate void ShowDataEvent(string strText);
        public event ShowDataEvent OnShowModelInfo = null;

        public COffsetModelUtil()
        {

        }

        public void FuncMainPostOff(string argv, string strRefMod, string strRefStr, string strOffMod, string strOffStr)
        {
            path = 	argv;

            this.m_RefMod = strRefMod;
            this.m_RefStr = strRefStr;
            this.m_OffMod = strOffMod;
            this.m_OffStr = strOffStr;
		
            pathfile = Path.Combine(path, "MODEL.FIL");
            listmodelfilOld = new List<CLabtype>();
            listmodelfilNew = new List<CLabtype>();
            ViewerUtils.ReadModelFilFile(pathfile, ref listmodelfilOld);
	
            pathfile = Path.Combine(path, "OFFSET.FIL");		
            if (File.Exists(pathfile) == false)
            {
                return;
            }

            fp2 = new StreamReader(pathfile);
            fp4 = new StreamWriter(Path.Combine(path, "OFFSETS.REP"), true);
	
            fp5 = new StreamWriter(Path.Combine(path, "OFFDAT.FIL"), true);
	

	
	        header();

	        write_input_rep();

            fp4.WriteLine("");

            //int fp1Pos = 0;
            
            while(fp2.EndOfStream == false)                      /* Input file */
            {
                string strCurfp2Line = fp2.ReadLine();
                string[] tok = strCurfp2Line.Split(new char[]{'\t'});
                str1 = tok[0];

                opnum = short.Parse(str1);

		        if(opnum == 401)
		        {
                    str1 = tok[1];
                    refmod = tok[2];
                    str2 = tok[3];
                    refstr = tok[4];
			        offind=0;
        			
			        chndiff=0; 
			        last_chndiff=0;
			        chn1=0;
		            //rewind(fp1);
		        }
		        else if(opnum==402)
		        {
			        type=1;
                    str1 = tok[1];
                    offmod = tok[2];
                    str2 = tok[3];
                    offstr = tok[4];

                    str1 = tok[5];
                    ch1 = double.Parse(tok[6]);
                    str2 = tok[7];
                    ch2 = double.Parse(tok[8]);

                    str1 = tok[9];
                    ho1 = double.Parse(tok[10]);
                    str2 = tok[11];
                    ho2 = double.Parse(tok[12]);

                    str1 = tok[13];
                    vo1 = double.Parse(tok[14]);
                    str2 = tok[15];
                    vo2 = double.Parse(tok[16]);

                    //fscanf(fp2,"%s %s %s %s",str1, offmod, str2, offstr);
                    //fscanf(fp2,"%s %lf %s %lf",str1, &ch1, str2, &ch2);
                    //fscanf(fp2,"%s %lf %s %lf",str1, &ho1, str2, &ho2);
                    //fscanf(fp2,"%s %lf %s %lf",str1, &vo1, str2, &vo2);

		            //rec=0;
		            //rewind(fp1);
			        if(last_offstr != offstr)
                        offind=0;

			        /*
			        buffer.Format("CREATING OFFSET STRING TYPE 1");
			        */
                    try
                    {
                        StreamWriter tmpFile = new StreamWriter(Path.Combine(path, "SCROLL.TMP"), true);
                        tmpFile.WriteLine("CREATING OFFSET STRING TYPE 1");
                        tmpFile.Close();
                    }
                    catch
                    {
                        msgstr = "Failed to write display file";
                        error_msg();
                        return;
                    }
                    
			        fp4.Write(string.Format("\n  MODEL = {0}  REFERENCE-STRING LABEL = {1}\n", refmod, refstr));
                    fp4.Write(string.Format("\n  MODEL = {0}  OFFSET-STRING LABEL = {1}\n\n", offmod, offstr));
                    fp4.Write("  CHAINAGE(M)     X (M)             Y(M)            Z(M)   H-OFF(M)  V-OFF(M)\n\n");

                    //fprintf(fp4,"\n  MODEL = %s  REFERENCE-STRING LABEL = %s\n", refmod, refstr);
                    //fprintf(fp4,"\n  MODEL = %s  OFFSET-STRING LABEL = %s\n\n", offmod, offstr);
                    //fprintf(fp4, "  CHAINAGE(M)     X (M)             Y(M)            Z(M)   H-OFF(M)  V-OFF(M)\n\n");

                    fp5.Write(string.Format("{0} {1} {2} {3} CH1 {4:f3} CH2 {5:f3} HO1 {6:f3} HO2 {7:f3} VO1 {8:f3} VO2 {9:f3}\n", refmod, refstr, offmod, offstr, ch1, ch2, ho1, ho2, vo1, vo2));
			        //fprintf(fp5,"%s %s %s %s CH1 %.3f CH2 %.3f HO1 %.3f HO2 %.3f VO1 %.3f VO2 %.3f\n", refmod, refstr, offmod, offstr, ch1, ch2, ho1, ho2, vo1, vo2);

                    reference_string();

                    //if(find != 1 && label != 999)
                    //{
                    //    //fprintf(fp1,"999\n");
                    //}
        			
		        }
		        else if(opnum==403)
		        {
			        type=2;
                    str1 = tok[1];
                    offmod = tok[2];
                    str2 = tok[3];
                    offstr = tok[4];

                    str1 = tok[5];
                    ch1 = double.Parse(tok[6]);
                    str2 = tok[7];
                    ch2 = double.Parse(tok[8]);

                    str1 = tok[9];
                    ho1 = double.Parse(tok[10]);
                    str2 = tok[11];
                    ho2 = double.Parse(tok[12]);

                    str1 = tok[13];
                    se1 = double.Parse(tok[14]);
                    str2 = tok[15];
                    se2 = double.Parse(tok[16]);


                    //fscanf(fp2,"%s %s %s %s",str1, offmod, str2, offstr);
                    //fscanf(fp2,"%s %lf %s %lf",str1, &ch1, str2, &ch2);
                    //fscanf(fp2,"%s %lf %s %lf",str1, &ho1, str2, &ho2);
                    //fscanf(fp2,"%s %lf %s %lf",str1, &se1, str2, &se2);

		            //rec=0;
		            //rewind(fp1);
			        if(last_offstr != offstr)
                        offind=0;
        		
			        try
                    {
                        StreamWriter tmpFile = new StreamWriter(Path.Combine(path, "SCROLL.TMP"), true);
                        tmpFile.WriteLine("CREATING OFFSET STRING TYPE 2");
                        tmpFile.Close();
                    }
                    catch
                    {
                        msgstr = "Failed to write display file";
                        error_msg();
                        return;
                    }

			       
                    fp4.Write(string.Format("\n  MODEL = {0}  REFERENCE-STRING LABEL = {1}\n", refmod, refstr));
					fp4.Write(string.Format("\n  MODEL = {0}  OFFSET-STRING LABEL = {1}\n", offmod, offstr));
					fp4.Write("  CHAINAGE(M)     X (M)             Y(M)            Z(M)   H-OFF(M)  V-OFF(M)\n\n");
        			
                    //fprintf(fp4,"\n  MODEL = %s  REFERENCE-STRING LABEL = %s\n", refmod, refstr);
                    //fprintf(fp4,"\n  MODEL = %s  OFFSET-STRING LABEL = %s\n", offmod, offstr);
                    //fprintf(fp4, "  CHAINAGE(M)     X (M)             Y(M)            Z(M)   H-OFF(M)  V-OFF(M)\n\n");

			        vo1=Math.Abs(ho1)*se1/100.0;
			        vo2=Math.Abs(ho2)*se2/100.0;
                    fp5.Write(string.Format("{0} {1} {2} {3} CH1 {4:f3} CH2 {5:f3} HO1 {6:f3} HO2 {7:f3} VO1 {8:f3} VO2 {9:f3}\n"
                        , refmod, refstr, offmod, offstr, ch1, ch2, ho1, ho2, vo1, vo2));
			        //fprintf(fp5,"%s %s %s %s CH1 %.3f CH2 %.3f HO1 %.3f HO2 %.3f VO1 %.3f VO2 %.3f\n", refmod, refstr, offmod, offstr, ch1, ch2, ho1, ho2, vo1, vo2);

                    reference_string();

                    //if(find != 1 && label != 999)
                    //{
                    //    //fprintf(fp1,"999\n");
                    //}
		        }
                else if (opnum == 404)
                {
                    type = 3;

                    str1 = tok[1];
                    offmod = tok[2];
                    str2 = tok[3];
                    offstr = tok[4];

                    str1 = tok[5];
                    ch1 = double.Parse(tok[6]);
                    str2 = tok[7];
                    ch2 = double.Parse(tok[8]);

                    str1 = tok[9];
                    se1 = double.Parse(tok[10]);
                    str2 = tok[11];
                    se2 = double.Parse(tok[12]);

                    //fscanf(fp2,"%s %s %s %s",str1, submod, str2, substr);
                    //fscanf(fp2,"%s %lf %s %lf",str1, &ch1, str2, &ch2);
                    //fscanf(fp2,"%s %lf %s %lf",str1, &se1, str2, &se2);

                    //rec = 0;
                    //rewind(fp1);
                    if (last_substr != substr)
                        offind = 0;

                    try
                    {
                        StreamWriter tmpFile = new StreamWriter(Path.Combine(path, "SCROLL.TMP"), true);
                        tmpFile.WriteLine("CREATING OFFSET STRING TYPE 3");
                        tmpFile.Close();
                    }
                    catch
                    {
                        msgstr = "Failed to write display file";
                        error_msg();
                        return;
                    }

                    fp4.Write(string.Format("\n  MODEL = {0}  REFERENCE-STRING LABEL = {1}\n", refmod, refstr));
                    fp4.Write(string.Format("\n  MODEL = {0}  OFFSET-STRING LABEL = {1}\n", submod, substr));
                    fp4.Write("  CHAINAGE(M)     CHAINAGE(M)      X (M)             Y(M)            Z(M)   H-OFF(M)  V-OFF(M)\n");
                    fp4.Write("  REF. STR.       SUB. STR.\n\n");

                    //fprintf(fp4,"\n  MODEL = %s  REFERENCE-STRING LABEL = %s\n", refmod, refstr);
                    //fprintf(fp4,"\n  MODEL = %s  OFFSET-STRING LABEL = %s\n", submod, substr);
                    //fprintf(fp4, "  CHAINAGE(M)     CHAINAGE(M)      X (M)             Y(M)            Z(M)   H-OFF(M)  V-OFF(M)\n");	
                    //fprintf(fp4, "  REF. STR.       SUB. STR.\n\n");	

                    ok2 = 0;
                    //hit = 0;
                    ss_found = 0;

                    vo1 = Math.Abs(ho1) * se1 / 100.0;
                    vo2 = Math.Abs(ho2) * se2 / 100.0;
                    fp5.Write(string.Format("{0} {1} {2} {3} CH1 {4:f3} CH2 {5:f3} HO1 {6:f3} HO2 {7:f3} VO1 {8:f3} VO2 {9:f3}\n", refmod, refstr, offmod, offstr, ch1, ch2, ho1, ho2, vo1, vo2));
                    //fprintf(fp5,"%s %s %s %s CH1 %.3f CH2 %.3f HO1 %.3f HO2 %.3f VO1 %.3f VO2 %.3f\n", refmod, refstr, offmod, offstr, ch1, ch2, ho1, ho2, vo1, vo2);


                    reference_string();

                    if (ss_found == 0)
                    {
                        fp4.Write(string.Format("Master Sub-String {0} {1} not found in {2}...!!!"
                            , submod, substr, Path.Combine(path, "MODEL.FIL")));
                        msgstr = string.Format("Master Sub-String {0} {1} not found in {2}...!!!", submod, substr, Path.Combine(path, "MODEL.FIL"));
                        System.Windows.Forms.MessageBox.Show(msgstr, HeadsUtils.Constants.ProductName);
                        break;
                    }

                    if (find != 1 && label != CLabtype.Type.EndCode)
                    {
                        CLabtype lab = new CLabtype();
                        lab.attr = CLabtype.Type.EndCode;
                        listmodelfilOld.Add(lab);
                    }

                    last_submod = submod;
                    last_substr = substr;

                    //fclose(fp5);
                }
                else
                {
                    break;
                }

                last_offmod = offmod;
                last_offstr = offstr;
            }  /* while feof */
            
            footer();

            listmodelfilOld.AddRange(listmodelfilNew);
            ViewerUtils.WriteModelFilFile(Path.Combine(path, "MODEL.FIL"), listmodelfilOld);
            
            //fclose(fp1);
            fp2.Close();
            fp4.Close();
            fp5.Close();

            System.Windows.Forms.MessageBox.Show("Design Report is written in file OFFSETS.REP", HeadsUtils.Constants.ProductName);
        }

        void error_msg()
        {
            System.Windows.Forms.MessageBox.Show(msgstr);           

            StreamWriter fpm = new StreamWriter(Path.Combine(path, "ERRORMESSAGES.TXT"), true);
            fpm.Write("\n\n    *********************************************");
            fpm.Write(string.Format("\n    Program [HEADS:OFFSET] was run on  {0}", DateTime.Now.ToString()));
            fpm.Write(string.Format("\n    {0}", msgstr));
            fpm.Write("\n    *********************************************");

            fpm.Close();
        }

        void footer()
        {
            fp4.Write("\n\n");
            fp4.Write("  END OF DESIGN ------------------------------------------------------------------ .\n\n\n");
        }
        
        void header()
        {
            fp4.Write("\t\n\n\n\n");
            fp4.Write("\t\t\t\t*******************************************\n\n");
            fp4.Write("\t\t\t\t     HEADS Release 14\n\n");
            fp4.Write("\t\t\t\tReport on Design of Offset String Alignment\n\n");
            fp4.Write("\t\t\t\tProgram was run on  " + DateTime.Now.ToString() + "\n");
            fp4.Write("\t\t\t\tTechSOFT Engineering Services (I) Pvt. Ltd.\n\n");
            fp4.Write("\t\t\t\t*******************************************\n");

            fp4.Write("\n\n\n");
        }

        void write_input_rep()
        {
            int rec;
            string recstr;

            fp2.Close();
            fp2 = new StreamReader(Path.Combine(path, "OFFSET.FIL"));
            
            fp4.Write(" USER INPUT DATA :\n\n\n");

            while (fp2.EndOfStream == false)
            {
                uline = fp2.ReadLine();
                string[] tok = uline.Split(new char[] {'\t'});
                rec = int.Parse(tok[0]);
                str1 = tok[1];
                recstr = tok[2];

                fp4.WriteLine(" " + uline);
                if(str1.StartsWith("FIN") == true)
                    break;
            }

            fp4.Write("\n\n\n");
            fp4.Write(" OFFSET STRING-WISE CHAINAGE AND OFFSET DISTANCE DETAILS :\n");
            fp4.Write("\n\n");

            fp2.Close();
            fp2 = new StreamReader(Path.Combine(path, "OFFSET.FIL"));
        }

        void reference_string()
        {
            if (type == 1 || type == 2)
            {
                offset_sub_string();
                return;
            }
            else if (type == 3)
            {
                master_sub_string();
                return;
            }        	
        }

        void bearing()
        {
            double th, r9;

            r9 = 57.29577951;

            if (x2 > x1 && y2 == y1)
                brg = 90;
            else if (x2 < x1 && y2 == y1)
                brg = 270;
            else if (x2 == x1 && y2 > y1)
                brg = 0;
            else if (x2 == x1 && y2 < y1)
                brg = 180;
            else
            {
                th = Math.Atan((y2 - y1) / (x2 - x1));
                th = Math.Abs(th) * r9;

                if (x2 > x1 && y2 > y1)
                    brg = 90 - th;
                else if (x2 > x1 && y2 < y1)
                    brg = 90 + th;
                else if (x2 < x1 && y2 < y1)
                    brg = 270 - th;
                else if (x2 < x1 && y2 > y1)
                    brg = 270 + th;
            }


            //if (0.0 <= brg && brg <= 90)
            //    quadrant = 1;
            //else if (90.0 <= brg && brg <= 180)
            //    quadrant = 2;
            //else if (180.0 <= brg && brg <= 270)
            //    quadrant = 3;
            //else if (270.0 <= brg && brg <= 360)
            //    quadrant = 4;
        }

        void offset_sub_string()
        {
            CLabtype lab;
	        CModType mod = null;
	        CStgType stg;
	        CPTStype pts;
	        CTXTtype txt;

	        short ok;
	        double chkchn1;//, chkchn2;
	        int ichn1, chkchn_flag;
        	

	        ok=0;
	        ind=0;
	        //rep_flag=0;
        	

	        double r9;
	        r9=57.29577951;
            
            //while(! feof(fp1))
            for (int iPos = 0; iPos < listmodelfilOld.Count; iPos++)
            {
                lab = listmodelfilOld[iPos];
                label = lab.attr;

		        if(label == CLabtype.Type.Model)
		        {
                    mod = (CModType)lab.Tag;			
			        ok=0;
		        }
                else if (label == CLabtype.Type.String)
                {
                    stg = (CStgType)lab.Tag;
			        if(refmod.ToLower() == mod.name.ToLower() && refstr.ToLower() == stg.label.ToLower())
			        {
				        ok=1;
			        }
                }
                else if (label == CLabtype.Type.Point)
                {
                    pts = (CPTStype)lab.Tag;
                    val = pts.mc;
                    
                    if(ok>0 && ch1 <= pts.mc && pts.mc <= ch2)
                    {
                        ok=2;

				        chn2 = pts.mc;
				        x2=pts.mx;
				        y2=pts.my;
				        z2=pts.mz;

				        chndiff=Math.Abs(chn2-chn1);

				        chkchn_flag=0;
				        chkchn1=chn2/5.0;
				        ichn1=(int)chkchn1;
				        ichn1=(int)(ichn1*5.0);

				        if(chn2!=ichn1)
				        {
					        chkchn_flag=1;
					        if(chn2 < ch2 && chndiff < last_chndiff)
					        {
						        continue;
					        }
				        }
                        //fgetpos(fp1, &loc1);
                        
                        if(ind==0)
                        {
					        chn1=chn2; 
					        x1=x2; 
					        y1=y2; 
					        z1=z2;

                            lab = listmodelfilOld[iPos+1];
                            pts = (CPTStype)lab.Tag;
                            //fread(&lab, sizeof(char), sizeof(lab), fp1);
                            //fread(&pts, sizeof(char), sizeof(pts), fp1);

					        chn2 = pts.mc;
					        x2=pts.mx;
					        y2=pts.my;
					        z2=pts.mz;

					        bearing();

					        chn2 = chn1;
					        x2=x1;
					        y2=y1;
					        z2=z1;
                            
                            //fsetpos(fp1, &loc1);
                        }
                        else
                        {
                            bearing();
                        }
                        
                        if(type==1)
				        {
					        ho=ho1+(chn2-ch1)*(ho2-ho1)/(ch2-ch1);
					        vo=vo1+(chn2-ch1)*(vo2-vo1)/(ch2-ch1);

					        ochn = chn2;

        				
					        if(chn2 == chn1 && ind == 1)	// Sandipan 28/09/2007
					        {
						        ox = last_ox;
						        oy = last_oy; 
						        oz = last_oz;
					        }
					        else
					        {
						        ox = x2 + ho*Math.Cos((brg)/r9);
						        oy = y2 - ho*Math.Sin((brg)/r9); 
						        oz = z2 + vo;
						        last_ox = ox;
						        last_oy = oy;
						        last_oz = oz;
					        }
        					

					        write_offset_string();
					        //fsetpos(fp1, &loc1);
				        }
                        else if(type==2)
                        {
                            ho=ho1+(chn2-ch1)*(ho2-ho1)/(ch2-ch1);
					        se=se1+(chn2-ch1)*(se2-se1)/(ch2-ch1);
					        vo=Math.Abs(ho)*se/100.0;

					        ochn = chn2;

					        if(chn2 == chn1 && ind == 1)	// Sandipan 28/09/2007
					        {
						        ox = last_ox;
						        oy = last_oy; 
						        oz = last_oz;
					        }
					        else
					        {
                                ox = x2 + ho * Math.Cos((brg) / r9);
                                oy = y2 - ho * Math.Sin((brg) / r9); 
						        oz = z2 + vo;
						        last_ox = ox;
						        last_oy = oy;
						        last_oz = oz;
					        }

					        write_offset_string();
					        //fsetpos(fp1, &loc1);
                        }
                        else if(type==3)
                        {
                            //Do nothing
                        }
                        
                        if(chkchn_flag==0)
				        {
					        last_chndiff=chn2-chn1;
					        ind=1;
					        chn1=chn2; 
					        x1=x2; 
					        y1=y2; 
					        z1=z2;
				        }
                    }	//ok==1
                }	//103
                else if (label == CLabtype.Type.Text)
                {
                    txt = (CTXTtype)lab.Tag;                    
                }
                else if (label == CLabtype.Type.EndCode)
                {
                    if (ochn == ch2 && ok == 2)
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            if(ok==2)
            {
                lab = new CLabtype();
                lab.attr = CLabtype.Type.EndCode;
                listmodelfilNew.Add(lab);
                
                //fwrite(&lab, sizeof(char), sizeof(lab), fp1);
                ok=0;
            }
        }

        void write_offset_string()
        {
            CLabtype lab;
	        CModType mod;
	        CStgType stg;
	        CPTStype pts;
	        //CTXTtype txt;
            
            //fsetpos(fp1, &loc2);
	
            if(offind==0)
	        {
                lab = new CLabtype();
                lab.attr = CLabtype.Type.Model;
                mod = new CModType();
                mod.name = offmod;
                lab.Tag = mod;
                listmodelfilNew.Add(lab);

                lab = new CLabtype();
                lab.attr = CLabtype.Type.String;
                stg = new CStgType();
                stg.label = offstr;
                lab.Tag = stg;
                listmodelfilNew.Add(lab);

		        write_list();

		        offind=1;
	        }

            lab = new CLabtype();
            lab.attr = CLabtype.Type.Point;
            pts = new CPTStype();
            pts.mc = ochn;
	        pts.mx = ox;
	        pts.my = oy;
	        pts.mz = oz;
            lab.Tag = pts;
            listmodelfilNew.Add(lab);
            
            //fgetpos(fp1, &loc2);

            fp4.Write(string.Format(" {0,10:f5}  {1,15:f5}  {2,15:f5}  {3,10:f3} {4,8:f3} {5,8:f3}\n", ochn, ox, oy, oz, ho, vo));
            //fprintf(fp4," %10.5f  %15.5f  %15.5f  %10.3f %8.3f %8.3f\n", ochn, ox, oy, oz, ho, vo);

            try
            {
                StreamWriter tmpFile = new StreamWriter(Path.Combine(path, "SCROLL.TMP"), true);
                tmpFile.WriteLine(string.Format("{0} {1} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3}", offmod, offstr, ochn, ox, oy, oz));                
                tmpFile.Close();
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Failed to write display file", HeadsUtils.Constants.ProductName);
                return;
            }
        }

        void write_list()
        {
            BinaryWriter bw = new BinaryWriter(new FileStream(Path.Combine(path, "MODEL.LST"), FileMode.Append), Encoding.Default);
            ClSTtype lst = new ClSTtype();
            lst.strMod1 = offmod;
            lst.strStg = offstr;
            lst.ToStream(bw);
            bw.Close();
        }

        void master_sub_string()
        {
            CLabtype lab;
	        CModType mod = null;
	        CStgType stg;
	        CPTStype pts;
	        CTXTtype txt;

		    // Redeveloped Sandipan 23/03/2007
		    // Redeveloped  14/06/2004
		    // Redeveloped by Sandipan 04 & 05/12/2003
    		
		    //rewind(fp1);
		
	        double[] rchn = new double[10000];
	        double[] rx = new double[10000];
	        double[] ry = new double[10000];
	        double[] rz = new double[10000];
	        double[] schn = new double[10000];
	        double[] sx = new double[10000];
	        double[] sy = new double[10000];
	        double[] sz = new double[10000];
	        int i, j;
	        int totref;
	        int totsub;
	        //string substrFile;

	        double valrc, valrx, valry, valrz;
	        double valsc1, valsx1, valsy1, valsz1;
	        double valsc2, valsx2, valsy2, valsz2;
	        //double ic, valix, valiy, valiz;
	        double m1, m2, c1, c2;
	        double x4, y4, z4, d, id1, id2, id3;
	        double xdiff1, ydiff1, xdiff2, ydiff2, B1 = 0, B2 = 0, theta, r9, ichn, checkd;
	        int direction;
	        string curmod = string.Empty, curstr;

            StreamWriter ftmp;
            try
            {
                ftmp = new StreamWriter(Path.Combine(path, "SUBSTR.TMP"), true);                
            }
            catch
            {
                msgstr = "Failed to write display file " + Path.Combine(path, "SCROLL.TMP");
                error_msg();
                return;
            }

            //FILE *ftmp;
            //strcpy(substrFile, path);
            //strcat(substrFile, "\\substr.tmp");

            //if((ftmp=fopen(substrFile, "w")) == NULL)
            //{
            //    fclose(fp1);
            //    sprintf(msgstr, "Failed to open file %s", substrFile);
            //    error_msg();
            //}

            i=0; j=0;
            r9=57.29577951;
            
            for (int iPos = 0; iPos < listmodelfilOld.Count; iPos++)
            {
                lab = listmodelfilOld[iPos];
                // model file for offset string 
                label = lab.attr;


                if (label == CLabtype.Type.Model)
                {
                    mod = (CModType)lab.Tag;
                    str1 = mod.name;
                    ok1=0;
                    ok2=0;
                }
                else if (label == CLabtype.Type.String)
			    {
                    stg = (CStgType)lab.Tag;
                    str2 = stg.label;
    				
				    if(refmod.ToLower() == str1.ToLower() && refstr.ToLower() == str2.ToLower())
				    {
					    ok1=1;			
				    }
				    else if(submod.ToLower() == str1.ToLower() && substr.ToLower() == str2.ToLower())
				    {
					    ok2=1;
					    ss_found=1;
				    }

			    }
                else if (label == CLabtype.Type.Point)
			    {
                    pts = (CPTStype)lab.Tag;
				    if(ok1==1)
				    {
                        rchn[i] = pts.mc;
					    rx[i] = pts.mx;
					    ry[i] = pts.my;
					    rz[i] = pts.mz;
					    i++;
				    }
				    else if(ok2==1)
				    {
					    schn[j] = pts.mc;
					    sx[j] = pts.mx;
					    sy[j] = pts.my;
					    sz[j] = pts.mz;
					    j++;
				    }

			    }
                else if (label == CLabtype.Type.Text)
			    {
                    txt = (CTXTtype)lab.Tag;
				    //fread(&txt, sizeof(char), sizeof(txt), fp1);
			    }
                else if (label == CLabtype.Type.EndCode)
			    {
				    continue;
			    }
            }	// while ! feof 

	        totref=i;
	        totsub=j;

	        if(ss_found==0)
		        return;

            for(i=0; i<totref; i++)
            {
                valrc=rchn[i];
                valrx=rx[i]; valry=ry[i]; valrz=rz[i];
		
		        for(j=1; j<totsub; j++)
		        {
			        valsc1=schn[j-1];
			        valsx1=sx[j-1]; valsy1=sy[j-1]; valsz1=sz[j-1];
			        valsc2=schn[j];
			        valsx2=sx[j]; valsy2=sy[j]; valsz2=sz[j];

			        if(valsc1==valsc2)
				        continue;

			        xdiff1=Math.Abs(sx[j]-sx[j-1]);
			        if(xdiff1 == 0.0)
				        xdiff1=0.001;

			        ydiff1=Math.Abs(sy[j]-sy[j-1]);
			        if(ydiff1 == 0.0)
				        ydiff1=0.001;

        			
			        m1=(sy[j]-sy[j-1])/(sx[j]-sx[j-1]);
			        c1=sy[j-1] - ((sy[j]-sy[j-1])/(sx[j]-sx[j-1]))*sx[j-1];
			        m2=0.0-(1/m1);
			        c2=ry[i] - m2*rx[i];
			        x4=(c2-c1)/(m1-m2);
			        y4=m1*x4 + c1;

			        id1=Math.Sqrt((x4-sx[j-1])*(x4-sx[j-1]) + (y4-sy[j-1])*(y4-sy[j-1]));
			        id2=Math.Sqrt((x4-sx[j])*(x4-sx[j]) + (y4-sy[j])*(y4-sy[j]));
			        id3=Math.Sqrt((sx[j-1]-sx[j])*(sx[j-1]-sx[j]) + (sy[j-1]-sy[j])*(sy[j-1]-sy[j]));
			        checkd=Math.Abs(id1+id2-id3);

			        if(checkd > 0.001)
				        continue;

			        d=Math.Sqrt((rx[i]-x4)*(rx[i]-x4) + (ry[i]-y4)*(ry[i]-y4));
        			

			        ////
			        theta=Math.Atan(ydiff1/xdiff1);
			        theta=Math.Abs(theta*r9);

			        if(sx[j]>=sx[j-1] && sy[j]>=sy[j-1])
				        B1 = 90-theta;
			        else if(sx[j]>=sx[j-1] && sy[j]<=sy[j-1])
				        B1 = 90 + theta;
			        else if(sx[j]<=sx[j-1] && sy[j]<=sy[j-1])
				        B1 = 270 -  theta;
			        else if(sx[j]<=sx[j-1] && sy[j]>=sy[j-1])
				        B1 = 270 + theta;

			        if(B1 < 0.0)
				         B1 = B1 + 360;
			        if(B1 > 360)
				         B1 = B1 - 360;

			        xdiff2=Math.Abs(rx[i]-x4);
			        if(xdiff2 == 0.0)
				        xdiff2=0.001;

			        ydiff2=Math.Abs(ry[i]-y4);
			        if(ydiff2 == 0.0)
				        ydiff2=0.001;

			        theta=Math.Atan(ydiff2/xdiff2);
			        theta=Math.Abs(theta*r9);

			        if(rx[i]>=x4 && ry[i]>=y4)
				        B2 = 90 - theta;
			        else if(rx[i]>=x4 && ry[i]<=y4)
				        B2 = 90 + theta;
			        else if(rx[i]<=x4 && ry[i]<=y4)
				        B2 = 270 - theta;
			        else if(rx[i]<=x4 && ry[i]>=y4)
				        B2 = 270 + theta;

			        if(B2 < 0.0)
				        B2 = B2 + 360;
			        if(B2 > 360)
				        B2 = B2 - 360;


			        // Ref Halignrep.cpp
			        // determine Actual direction of Turn
			        // 16/01/2003	Sandipan
			        // 30/05/2003	Sandipan
			        // 31/07/2003	Sandipan
			        // 17/08/2003	Sandipan
			        // Opened & modified on 10/02/2004  on Constell's Meghalaya work, Sandipan


			        if(B2 > B1)		
			        {
				        if(0 <= B1 && B1 <= 90 && 270 <= B2 && B2 <= 360)
					        direction=1;
				        else if(0 <= B1 && B1 <= 90 && 180 <= B2 && B2 <= 270)
				        {
					        if(B2 > B1+180)
						        direction=1;
					        else
						        direction=2;
				        }

				        else if(90 <= B1 && B1 <= 180 && 270 <= B2 && B2 <= 360)
				        {
					        if(B2 > B1+180)
						        direction=1;
					        else
						        direction=2;
				        }

				        else
					        direction=2;
			        }
			        else
			        {	 
				        if(270 <= B1 && B1 <= 360 && 0 <= B2 && B2 <= 90)
					        direction=2;
				        else if(180 <= B1 && B1 <= 270 && 0 <= B2 && B2 <= 90)
				        {
					        if(B1 > B2+180)
						        direction=2;
					        else
						        direction=1;
				        }
				        else if(270 <= B1 && B1 <= 360 && 90 <= B2 && B2 <= 180)
				        {
					        if(B1 > B2+180)
						        direction=2;
					        else
						        direction=1;
				        }
				        else
					        direction=1;
			        }
         
			        // Done for halignrep.cpp on 10/02/2004  Sandipan
         
			        if(direction==1)
				        sign_d=1;
			        else if(direction==2)
				        sign_d=-1;

			        ichn=schn[j-1] + Math.Sqrt((sx[j-1]-x4)*(sx[j-1]-x4) + (sy[j-1]-y4)*(sy[j-1]-y4));
			        se=se1 + (se2-se1)*(ichn-ch1)/(ch2-ch1);
			        ho=d*sign_d;
			        vo=Math.Abs(ho)*se/100.0;
			        z4=rz[i] + vo;
			        ////

                    ftmp.WriteLine(valrc.ToString() + '\t' 
                        + ichn.ToString() + '\t'
                        + x4.ToString() + '\t'
                        + y4.ToString() + '\t' 
                        + z4.ToString() + '\t'
                        + ho.ToString() + '\t'
                        + vo.ToString());
        			//ftmp.Write(string.Format("{0:f5} {1:f5} {2:f5} {3:f5} {4:f5} {5:f3} {6:f3}\n", valrc, ichn, x4, y4, z4, ho, vo));
			        //fprintf(ftmp, "%.5f %.5f %.5f %.5f %.5f %.3f %.3f\n", valrc, ichn, x4, y4, z4, ho, vo);
			        break;
        			
		        }
            }
            
            //fclose(fp1);
            ftmp.Close();


            for (int iPos = 0; iPos < listmodelfilOld.Count; iPos++)
            {
                lab = listmodelfilOld[iPos];
                if(label == CLabtype.Type.Model)
		        {
                    mod = (CModType)lab.Tag;
                    curmod = mod.name;
		        }
                else if (label == CLabtype.Type.String)
		        {
                    stg = (CStgType)lab.Tag;
                    curstr = stg.label;

			        if(curmod.ToLower() == submod.ToLower() && curstr.ToLower()  == substr.ToLower() )
			        {
                        mod.name = "";
                        stg.label = "";	
	                    listmodelfilOld[iPos-1].Tag = mod;
		                listmodelfilOld[iPos].Tag = stg;
			        }
		        }
                else if (label == CLabtype.Type.Point)
                {
			        pts = (CPTStype)lab.Tag;
                }
                else if (label == CLabtype.Type.Text)
                {
			        txt = (CTXTtype)lab.Tag;
                }

            }

            StreamReader ftmpR = new StreamReader(Path.Combine(path, "SUBSTR.TMP"));

            fp5 = new StreamWriter(Path.Combine(path, "SHIFT.FIL"), false);

            StreamWriter tmpFile = null;
            int scroll = 1;
            try
            {
                tmpFile  = new StreamWriter(Path.Combine(path, "SCROLL.TMP"), true);
            }
            catch
            {
                scroll = 0;
            }



            lab = new CLabtype();
            lab.attr = CLabtype.Type.Model;
            mod = new CModType();
            mod.name = submod;
            lab.Tag = mod;
            listmodelfilNew.Add(lab);


            lab = new CLabtype();
            lab.attr = CLabtype.Type.String;
            stg = new CStgType();
            stg.label = substr;
            lab.Tag = stg;
            listmodelfilNew.Add(lab);
		
            while(ftmpR.EndOfStream == false)
            {
                string[] tok = ftmpR.ReadLine().Split(new char[] {'\t'});
                valrc = double.Parse(tok[0]);
                ichn = double.Parse(tok[1]);
                x4 = double.Parse(tok[2]);
                y4 = double.Parse(tok[3]);
                z4 = double.Parse(tok[4]);
                ho = double.Parse(tok[5]);
                vo = double.Parse(tok[6]);
                //fscanf(ftmp, "%lf %lf %lf %lf %lf %lf %lf", &valrc, &ichn, &x4, &y4, &z4, &ho, &vo);

                lab = new CLabtype();
                lab.attr = CLabtype.Type.Point;
                pts = new CPTStype();
                pts.mc = ichn;
                pts.mx = x4;
                pts.my = y4;
                pts.mz = z4;
                lab.Tag = pts;
                listmodelfilNew.Add(lab);


                //Write Report File
                fp4.Write(string.Format(" {0,10:f3}       {1,10:f5}  {2,15:f5}  {3,15:f5}  {4,10:f3} {5,8:f3} {6,8:f3}\n", valrc, ichn, x4, y4, z4, ho, vo));

                //Write Shift File
                fp5.Write(string.Format(" {0,10:f5}  {1,35:f3}\n", valrc, ho));

                //Write Display Scroll File
                if (scroll == 1)
                {
                    tmpFile.Write(string.Format("{0} {1} {2,10:f3} {3,10:f3} {4,10:f3} {5,10:f3} {6,8:f3} {7,5:f3} {8,5:f3}\n", submod, substr, valrc, ichn, x4, y4, z4, ho, vo));
                }
            }

            lab = new CLabtype();
            lab.attr = CLabtype.Type.EndCode;
            listmodelfilNew.Add(lab);
            
            //fclose(fp1);
            ftmpR.Close();
            fp5.Close();
        }

        void DisplayScroll(string str)
        {
            if (this.OnShowModelInfo != null)
            {
                this.OnShowModelInfo(str);
            }
        }
    }
}
