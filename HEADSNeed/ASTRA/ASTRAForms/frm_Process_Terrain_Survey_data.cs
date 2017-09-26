using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

using VectorDraw.Professional.vdCollections;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Geometry;
using HEADSNeed.ASTRA.ASTRAClasses;
using HeadsUtils;
using HeadsUtils.Interfaces;
using HeadsFunctions1.Halignment;

namespace HEADSNeed.ASTRA.ASTRAForms
{
    public partial class frm_Process_Terrain_Survey_data : Form
    {
        public delegate bool DShowGroundModel();
        Process prs = null;
        Thread thd = null;
        string exe_path = "";
        CheckBox checkMasterString_ = new CheckBox();
        vdPolyline vd_pl = null;
        Vertexes ptCords = null;
        IHeadsApplication iApp = null;
        List<CPTStype> chainage_points = null; //Store all Chainage Points
        public frm_Process_Terrain_Survey_data( vdPolyline pl)
        {
            InitializeComponent();
            exe_path = Application.StartupPath;
            this.vd_pl = pl;

            this.ptCords = vd_pl.VertexList;
            this.checkMasterString_.Checked = false;
            this.checkMasterString_.Visible = false;
        }

        public string Temp_File
        {
            get
            {
                if (File.Exists(Survey_File))
                    return Path.Combine(Path.GetDirectoryName(Survey_File), "inp.prj");
                return "";
            }
        }
        public string Survey_File
        {
            get
            {
                return txt_select_survey_data.Text;
            }
        }
        public string Survey_GEN_File
        {
            get
            {
                if (File.Exists(Survey_File))
                    return Path.GetFileNameWithoutExtension(Survey_File) + ".GEN";
                return "";
            }
        }
        public string WorkingPath
        {
            get
            {
                if (File.Exists(Survey_File))
                    return Path.GetDirectoryName(Survey_File);
                return "";
            }
        }
        public string AppDataPath
        {
            get
            {
                if (File.Exists(Survey_File))
                    return Path.GetDirectoryName(Survey_File);
                return "";
            }
        }

        public double Right_Offset
        {
            get
            {
                return Math.Abs(MyStrings.StringToDouble(txt_right_ho.Text, 0.0));
            }
        }
        public double Left_Offset
        {
            get
            {
                return (-Math.Abs(MyStrings.StringToDouble(txt_left_ho.Text, 0.0)));
            }
        }
        public double Chainage_Interval
        {
            get
            {
                return (Math.Abs(MyStrings.StringToDouble(txt_chn_interval.Text, 0.0)));
            }
        }
        public double Start_Chainage
        {
            get
            {
                return (Math.Abs(MyStrings.StringToDouble(txt_sc.Text, 0.0)));
            }
        }

        
        public bool RunProgram()
        {
            //vd_pl.
            try
            {
                List<string> HEADS_Data = new List<string>();
                string str = "";
                string exe_file = "DGM2.EXE";
                this.checkMasterString_.Checked = true;
                if (chainage_points == null)
                    chainage_points = new List<CPTStype>();
                chainage_points.Clear();
                ProcessData();
                Vertexes vtx = new Vertexes(ptCords);
                this.checkMasterString_.Checked = false;
                ptCords.RemoveAll();
                for (int i = 0; i < chainage_points.Count; i++)
                {
                    ptCords.Add(new gPoint(chainage_points[i].mx, chainage_points[i].my, chainage_points[i].mz));
                }
                ptCords = Get_OffSet_Points(ptCords);


                SurveyDataCollection s_data_col = new SurveyDataCollection(txt_select_survey_data.Text);
                s_data_col.Write_Model("");

                s_data_col.Write_Model(ptCords, txt_model.Text, txt_string.Text, WorkingPath);
                s_data_col.Write_Model(ptCords, txt_model.Text + "%%", txt_string.Text, WorkingPath);


                frm_GroundModeling ff = new frm_GroundModeling(Survey_File);
                ff.Owner = this;

                bool flag = !(HeadsUtils.Constants.BuildType == eHEADS_RELEASE_TYPE.DEMO);
                if (ff.ShowDialog() == DialogResult.OK)
                {

                    exe_file = flag ? "DTM_Pro.EXE" : "DTM_Demo.EXE";
                    //HEADS
                    HEADS_Data.Add("HEADS");
                    //100,DGM
                    HEADS_Data.Add("100,DGM");
                    //101,BOU,MODEL=BOUND,STRING=BDRY
                    HEADS_Data.Add("101,BOU,MODEL=" + txt_model.Text + ",STRING=" + txt_string.Text);
                    //FINISH
                    HEADS_Data.Add("FINISH");
                    File.WriteAllLines(Temp_File, HEADS_Data.ToArray());
                    SetEnvironmentVar(Temp_File);
                    //MessageBox.Show("3");
                    RunExe(exe_file);
                }
                exe_file = "DGCONT.EXE";
                exe_file = flag ? "DGCONT_Pro.EXE" : "DGCONT_Demo.EXE";

                HEADS_Data.Clear();
                //HEADS
                HEADS_Data.Add("HEADS");
                //100,DGM
                HEADS_Data.Add("100,DGM");
                //104,CON,MODEL=CONTOUR,STRING=C001,INC=1.0
                str = string.Format("104,CON,MODEL={0},STRING={1},INC={2}",
                    txt_pri_model.Text,
                    txt_pri_string.Text,
                    txt_pri_inc.Text);
                HEADS_Data.Add(str);
                //104,CON,MODEL=CONTOUR,STRING=C005,INC=5.0
                str = string.Format("104,CON,MODEL={0},STRING={1},INC={2}",
                    txt_sec_model.Text,
                    txt_sec_string.Text,
                    txt_sec_inc.Text);
                HEADS_Data.Add(str);
                //105,TXT,MODEL=CONTOUR,STRING=ELE2,INC=5.0,TSI=5
                str = string.Format("105,TXT,MODEL={0},STRING={1},INC={2}",
                    txt_ele_model.Text,
                    txt_ele_string.Text,
                    txt_ele_inc.Text);
                HEADS_Data.Add(str);
                //FINISH
                str = "FINISH";
                HEADS_Data.Add(str);
                File.WriteAllLines(Temp_File, HEADS_Data.ToArray());
                SetEnvironmentVar(Temp_File);
                RunExe(exe_file);
            }
            catch (Exception ex) { }
            return true;
           
        }
        public void RunExe(string exe_file)
        {
            try
            {
                string kStr = "";
                System.GC.Collect();

                exe_path = Application.StartupPath;
                exe_file = Path.Combine(exe_path, exe_file);
                if (File.Exists(exe_file))
                {
                    prs = new System.Diagnostics.Process();
                    prs.EnableRaisingEvents = true;
                    prs.StartInfo.UseShellExecute = true;
                    //prs.StartInfo.RedirectStandardOutput = true;
                    //prs.StartInfo.RedirectStandardError = true;

                    prs.StartInfo.FileName = exe_file;
                    prs.StartInfo.CreateNoWindow = true;

                    //prs.StartInfo.Verb = "runas";

                    if (prs.Start())
                    {
                        prs.WaitForExit();
                    }

                }
                else
                {
                    MessageBox.Show(exe_file + " file not found.");
                }

            }
            catch (Exception ex) { }
            finally
            {
                System.GC.Collect();
            }
        }

        void SetEnvironmentVar(string env_file_name)
        {
            try
            {
                File.WriteAllText(Path.Combine(exe_path, "hds.001"), env_file_name);
                File.WriteAllText(Path.Combine(exe_path, "hds.002"), WorkingPath + "\\");
                FileInfo finfo = new FileInfo(env_file_name);
                if (finfo.IsReadOnly)
                    finfo.IsReadOnly = false;
            }
            catch (Exception exx)
            {
            }
            System.Environment.SetEnvironmentVariable("DEM0", env_file_name);
            System.Environment.SetEnvironmentVariable("SURVEY", env_file_name);
            System.Environment.SetEnvironmentVariable("HDSPATH", Application.StartupPath + "\\");
        }

        public void AbortThread()
        {
            
            try
            {
                prs.Kill();

            }
            catch (Exception ex) { }
            try
            {
                thd.Abort();
            }
            catch (Exception ex) { }
            try
            {
                File.Delete(Temp_File);
            }
            catch (Exception ex) { }
        }
        public void WriteModelFile()
        {

        }

        double sx, sy, sz, ex, ey, ez;
        double B1, B2;
	    string modnam, stglbl, tt;
        const double r9 = 57.29577951;


        void determine_bearing()
        {
            double th, temp;

            if (sy == ey && ex > sx) B2 = 90;
            else if (sy == ey && ex < sx) B2 = 270;
            else if (sx == ex && ey > sy) B2 = 0;
            else if (sx == ex && ey < sy) B2 = 180;
            else
            {
                temp = (ey - sy) / (ex - sx);
                th = Math.Atan(temp);
                th = Math.Abs(th) * r9;

                if (ex > sx && ey > sy) B2 = 90 - th;
                else if (ex > sx && ey < sy) B2 = 90 + th;	/* NEW */
                else if (ex < sx && ey < sy) B2 = 270 - th;	/* NEW */
                else if (ex < sx && ey > sy) B2 = 270 + th;	/* NEW */
            }

            if (B2 < 0)
                B2 += 360;
            else if (B2 > 360)
                B2 -= 360;

        }

        void ProcessData()
        {
            BinaryWriter fp2 = null;
            StreamWriter fp3 = null;
            
            string nam;
            string lbl;

	        int ind=0, len, ival;
	        int slno, eltype, turn;

	        double x, y, z;
	        double stchn, RC, sc = 0, inc, seglen, segend;//, seginc;
	        double RCI, num, val;
	        double T1, T2, R, D, lastRC=-999.999;

	        /* Select a Polyline from screen */
	        /* User Input data in dialog box */
            if (this.checkMasterString_.Checked)
            {
                modnam = this.txt_make_str_Model.Text;
                stglbl = this.txt_make_str_String.Text;
            }
            else
            {
                modnam = this.txt_model.Text;
                stglbl = this.txt_string.Text;
            }



            stchn = Start_Chainage;

            inc = Chainage_Interval;

            string csTemp = Path.Combine(AppDataPath, "MODEL.FIL");
            fp2 = new BinaryWriter(new FileStream(csTemp, FileMode.Append), Encoding.Default);

	        if(this.checkMasterString_.Checked == true)
	        {
                csTemp = Path.Combine(AppDataPath, "HALIGN.FIL");
                fp3 = new StreamWriter(csTemp, true);		        
	        }
            CLabtype lab  = null;
            CPTStype pts = null;
            CModType mod = null;
            CStgType stg = null;
            ClSTtype lst = null;

            lab = new CLabtype();
            lab.attr = CLabtype.Type.Model;
            mod = new CModType();
            mod.name = modnam;
            lab.Tag = mod;
            lab.ToStream(fp2);

            lab = new CLabtype();
            lab.attr = CLabtype.Type.String;
            stg = new CStgType();
            stg.label = stglbl;
            lab.Tag = stg;
            lab.ToStream(fp2);
            

            RC = stchn;
	        //seginc=0.0;
	        sx=sy=sz=-999.999;
	        T1=T2=R=D=0;
            turn = 0;
	        B1=B2=0.0;
	        slno=1;
	        eltype=1;
	        turn=1;
	        ind=0;
	        //ind1=0;
            

            for (int vx = 0; vx < this.ptCords.Count; vx++)
            {
                /*JBLN*/
                ex = this.ptCords[vx].x;
                ey = this.ptCords[vx].y;
                ez = this.ptCords[vx].z;
                //		fscanf(fp1, "%lf %lf %lf", &ex, &ey, &ez);  /* close in vc++    */
                
                if(ind == 0)
                {
                    lab = new CLabtype();
                    lab.attr = CLabtype.Type.Point;
                    pts = new CPTStype();
                    pts.mc = RC;
                    pts.mx = ex;
                    pts.my = ey;
                    pts.mz = ez;
                    lab.Tag = pts;
                    lab.ToStream(fp2);
                    chainage_points.Add(pts); // Store Chainage Points
                    
                    sc=RC;
                    sx = ex;
                    sy = ey;
                    sz = ez;
                    B1 = B2;

                    ind = 1;
                    continue;
                }


                if (ex == sx && ey == sy)
                {
                    continue;
                }

                tt = string.Format("{0:f0}", inc);//sprintf(tt,"%.0f", inc);

                len = tt.Length;

                RCI = RC;
                num = Math.Pow(10, len);
                RCI = RCI / num;
                ival = (int)RCI;
                RCI = ival * num;
                while (RCI < RC)
                    RCI += inc;
                RC = RCI;

                if (RC == sc)
                    RC += inc;	/* NEW */
                
                determine_bearing();

                seglen = Math.Sqrt((ex - sx) * (ex - sx) + (ey - sy) * (ey - sy));	/* NEW */

                segend = sc + seglen;								/* NEW */

                if (this.checkMasterString_.Checked == true)
                {
                    if (B1 == 0) B1 = B2;
                    D = B1 - B2; if (D < 0) D += 360; if (D > 360) D -= 360;
                    
                    CHalignFilData fildata = new CHalignFilData();
                    fildata.sMod = modnam;
                    fildata.sString = stglbl;
                    fildata.iSlno = slno;
                    fildata.iEltype = (short)eltype;
                    fildata.dStartchn = sc;
                    fildata.dEndchn = segend;
                    fildata.dHipx = ex;
                    fildata.dHipy = ey;
                    fildata.dT1 = T1;
                    fildata.dT2 = T2;
                    fildata.dRadius = R;
                    fildata.dEllength = seglen;
                    fildata.dStartx = sx;
                    fildata.dStarty = sy;
                    fildata.dEndx = ex;
                    fildata.dEndy = ey;
                    fildata.dB1 = B2;
                    fildata.dB2 = B2;
                    fildata.dDeviation = D;
                    fildata.iTurn = (short)turn;
                    fp3.WriteLine(fildata.ToString());

                    //fprintf(fp3, "%s %s %d %d %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.5f %.3f %.3f %.3f %d\n", modnam, stglbl, slno, eltype, sc, segend, ex, ey, T1, T2, R, seglen, sx, sy, ex, ey, B2, B2, D, turn);
                    slno++;	/* NEW */
                }

                
                val = Math.Abs(RC - segend);/* fabs will be abs in VC++*/
                if (val < 0.001)
                    segend = RC;

		
                while(true)
                {
                    if (segend < RC)
                    {
                        if (segend + inc != RC)
                        {
                            val = Math.Abs(segend - lastRC);
                            if (val > 0.001)
                            {
                                lab = new CLabtype();
                                lab.attr = CLabtype.Type.Point;
                                pts = new CPTStype();
                                pts.mc = segend;
                                pts.mx = ex;
                                pts.my = ey;
                                pts.mz = ez;
                                lab.Tag = pts;
                                lab.ToStream(fp2);                                                    

                                lastRC = segend;
                                chainage_points.Add(pts); // Store Chainage Points
                            }
                        }
                        RC = segend;

                        ind = 0;
                        break;
                    }
                    else
                    {
                        if ((RC - sc) > 0.001)
                        {
                            x = sx + (RC - sc) * Math.Sin(B2 / r9);  	/*  E  */
                            y = sy + (RC - sc) * Math.Cos(B2 / r9);  	/*  N  */
                            z = sz + (RC - sc) * (ez - sz) / (segend - sc);

                            if (RC != lastRC)
                            {
                                lab = new CLabtype();
                                lab.attr = CLabtype.Type.Point;
                                pts = new CPTStype();
                                pts.mc = RC;
                                pts.mx = x;
                                pts.my = y;
                                pts.mz = z;
                                lab.Tag = pts;
                                lab.ToStream(fp2); 
                                
                                lastRC = RC;

                                chainage_points.Add(pts); // Store Chainage Points
                            }
                        }
                        RC += inc;
                    }
                }

                sc = RC;
                sx = ex;
                sy = ey;
                sz = ez;
                B1 = B2;

                ind = 1;
            }// end for previously while

            fp2.Close();

            if (this.checkMasterString_.Checked == true)
                fp3.Close();

            csTemp = Path.Combine(this.AppDataPath, "MODEL.LST");
            fp2 = new BinaryWriter(new FileStream(csTemp, FileMode.Append), Encoding.Default);
            lst = new ClSTtype();
            lst.strMod1 = modnam;
            lst.strStg = stglbl;
            lst.ToStream(fp2);
            fp2.Close();

            csTemp = Path.Combine(this.AppDataPath, "MODEL.FIL");
            fp2 = new BinaryWriter(new FileStream(csTemp, FileMode.Append), Encoding.Default);

            lab = new CLabtype();
            lab.attr = CLabtype.Type.Model;
            mod = new CModType();
            nam  = modnam + "%%";
            mod.name = nam;
            lab.Tag = mod;
            lab.ToStream(fp2);

            lab = new CLabtype();
            lab.attr = CLabtype.Type.String;
            stg = new CStgType();
            lbl = stglbl;
            stg.label = lbl;
            lab.Tag = stg;
            lab.ToStream(fp2);


            for (int vx = 0; vx < this.ptCords.Count; vx++)
            {
                ex = this.ptCords[vx].x;
                ey = this.ptCords[vx].y;
                ez = this.ptCords[vx].z;

                lab = new CLabtype();
                lab.attr = CLabtype.Type.Point;
                pts = new CPTStype();
                pts.mc = RC;
                pts.mx = ex;
                pts.my = ey;
                pts.mz = ez;
                lab.Tag = pts;
                lab.ToStream(fp2);
            }

            fp2.Close();

            csTemp = Path.Combine(this.AppDataPath, "MODEL.LST");
            fp2 = new BinaryWriter(new FileStream(csTemp, FileMode.Append), Encoding.Default);
            lst = new ClSTtype();
            lst.strMod1 = nam;
            lst.strStg = lbl;
            lst.ToStream(fp2);
            fp2.Close();
        }
        void Write_Boundary_Offset_in_Model_File()
        {
            BinaryWriter fp2 = null;
            StreamWriter fp3 = null;

            string nam;
            string lbl;

            int ind = 0, len, ival;
            int slno, eltype, turn;

            double x, y, z;
            double stchn, RC, sc = 0, inc, seglen, segend;//, seginc;
            double RCI, num, val;
            double T1, T2, R, D, lastRC = -999.999;

            /* Select a Polyline from screen */
            /* User Input data in dialog box */
            //if (this.checkMasterString_.Checked)
            //{
            //    modnam = this.txt_make_str_Model.Text;
            //    stglbl = this.txt_make_str_String.Text;
            //}
            //else
            //{
            modnam = this.txt_model.Text;
            stglbl = this.txt_string.Text;
            //}



            stchn = Start_Chainage;

            inc = Chainage_Interval;

            string csTemp = Path.Combine(AppDataPath, "MODEL.FIL");
            fp2 = new BinaryWriter(new FileStream(csTemp, FileMode.Append), Encoding.Default);
            
            CLabtype lab = null;
            CPTStype pts = null;
            CModType mod = null;
            CStgType stg = null;
            ClSTtype lst = null;

            lab = new CLabtype();
            lab.attr = CLabtype.Type.Model;
            mod = new CModType();
            mod.name = modnam;
            lab.Tag = mod;
            lab.ToStream(fp2);

            lab = new CLabtype();
            lab.attr = CLabtype.Type.String;
            stg = new CStgType();
            stg.label = stglbl;
            lab.Tag = stg;
            lab.ToStream(fp2);


            RC = stchn;
            //seginc=0.0;
            sx = sy = sz = -999.999;
            T1 = T2 = R = D = 0;
            turn = 0;
            B1 = B2 = 0.0;
            slno = 1;
            eltype = 1;
            turn = 1;
            ind = 0;
            //ind1=0;


            for (int vx = 0; vx < this.ptCords.Count; vx++)
            {
                /*JBLN*/
                ex = this.ptCords[vx].x;
                ey = this.ptCords[vx].y;
                ez = this.ptCords[vx].z;

                if (ind == 0)
                {
                    lab = new CLabtype();
                    lab.attr = CLabtype.Type.Point;
                    pts = new CPTStype();
                    pts.mc = RC;
                    pts.mx = ex;
                    pts.my = ey;
                    pts.mz = ez;
                    lab.Tag = pts;
                    lab.ToStream(fp2);
                    chainage_points.Add(pts); // Store Chainage Points

                    sc = RC;
                    sx = ex;
                    sy = ey;
                    sz = ez;
                    B1 = B2;

                    ind = 1;
                    continue;
                }


                if (ex == sx && ey == sy)
                {
                    continue;
                }

                tt = string.Format("{0:f0}", inc);//sprintf(tt,"%.0f", inc);

                len = tt.Length;

                RCI = RC;
                num = Math.Pow(10, len);
                RCI = RCI / num;
                ival = (int)RCI;
                RCI = ival * num;
                while (RCI < RC)
                    RCI += inc;
                RC = RCI;

                if (RC == sc)
                    RC += inc;	/* NEW */

                determine_bearing();

                seglen = Math.Sqrt((ex - sx) * (ex - sx) + (ey - sy) * (ey - sy));	/* NEW */

                segend = sc + seglen;								/* NEW */


                val = Math.Abs(RC - segend);/* fabs will be abs in VC++*/
                if (val < 0.001)
                    segend = RC;

                sc = RC;
                sx = ex;
                sy = ey;
                sz = ez;
                B1 = B2;

                ind = 1;
            }// end for previously while

            fp2.Close();

            csTemp = Path.Combine(this.AppDataPath, "MODEL.LST");
            fp2 = new BinaryWriter(new FileStream(csTemp, FileMode.Append), Encoding.Default);
            lst = new ClSTtype();
            lst.strMod1 = modnam;
            lst.strStg = stglbl;
            lst.ToStream(fp2);
            fp2.Close();
        }
        bool ValidateData()
        {
            if (this.txt_make_str_Model.Text.Trim() == "")
            {
                this.txt_make_str_Model.Focus();
                return false;
            }

            if (this.txt_make_str_String.Text.Trim() == "")
            {
                this.txt_make_str_String.Focus();
                return false;
            }

            string strHalignFilFilePath = Path.Combine(this.AppDataPath, "HALIGN.FIL");
            CHIPInfo[] infoarr = CHalignHipUtil.ReadHaligns(strHalignFilFilePath);
            foreach (CHIPInfo info in infoarr)
            {
                if (info.ModelName.Trim().ToLower() == this.txt_make_str_Model.Text.Trim().ToLower()
                    && info.StringLabel.Trim().ToLower() == this.txt_make_str_String.Text.Trim().ToLower())
                {
                    this.txt_make_str_String.Focus();
                    return false;
                }
            }

            return true;
        }
        
        /// <summary>
        /// Get Offset coordinate 
        /// if   ho is less than 0, then ho is left side
        /// if   ho is greater than 0, then ho is right side
        /// </summary>
        /// <param name="ho"></param>
        /// Offset Distance
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public gPoint Get_Offset_Coordinate(double ho, double x1, double y1, double x2, double y2)
        {
            ////double x1, y1, x2, y2, brg;

            double ho_left, ho_right, brg;
            double ox, oy;


            double th, r9;

            r9 = 57.29577951;
            brg = 0;
            //ho_left=-1000.0
            //ho_right=1000.0

            //ho=ho_left;
            //ho=ho_right

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
            ox = x2 + ho * Math.Cos((brg) / r9);
            oy = y2 - ho * Math.Sin((brg) / r9);

            return new gPoint(ox, oy);
        }
        public Vertexes Get_OffSet_Points(Vertexes vertx)
        {
            Vertexes off_vertx = new Vertexes();

            off_vertx.Add(vertx[0]);
            int i = 1;
            int D = 50;
            gPoint gp = null;
            gPoint gp1 = null;
            double dist = 0.0;

            for (i = D; i < vertx.Count; i += D)
            {
                gp = Get_Offset_Coordinate(Left_Offset, vertx[i - D].x, vertx[i - D].y, vertx[i].x, vertx[i].y);

                gp1 = vd_pl.getClosestPointTo(gp);
                dist = Math.Abs(gp.Distance3D(gp1));
                if (dist >= (Right_Offset/3.0))
                    off_vertx.Add(gp);
            }
            off_vertx.Add(vertx[vertx.Count - 1]);

            off_vertx.Reverse();
            for (i = D; i < vertx.Count; i += D)
            {
                gp = Get_Offset_Coordinate(Right_Offset, vertx[i - D].x, vertx[i - D].y, vertx[i].x, vertx[i].y);

                gp1 = vd_pl.getClosestPointTo(gp);
                dist = Math.Abs(gp.Distance3D(gp1));
                if (dist >= (Right_Offset / 3.0))
                    off_vertx.Add(gp);
            }
            off_vertx.Add(off_vertx[0]);

            return off_vertx;
        }

        private void btnOk__Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                ProcessData();
                this.Close();
            }
        }

        private void btn_process_Click(object sender, EventArgs e)
        {
            //this.checkMasterString_.Checked = true;
            //if (chainage_points == null)
            //    chainage_points = new List<CPTStype>();
            //chainage_points.Clear();
            //ProcessData();
            //Vertexes vtx = new Vertexes(ptCords);
            //this.checkMasterString_.Checked = false;
            //ptCords.RemoveAll();
            //for (int i = 0; i < chainage_points.Count; i++)
            //{
            //    ptCords.Add(new gPoint(chainage_points[i].mx, chainage_points[i].my, chainage_points[i].mz));
            //}
            //ptCords = Get_OffSet_Points(ptCords);

            //if (File.Exists(Survey_File))
            //{
            //    ThreadStart thds = new ThreadStart(RunThread);
            //    thd = new Thread(thds);
            //    thd.SetApartmentState(ApartmentState.STA);
            //    thd.Priority = ThreadPriority.Highest;
            //    thd.Start();
            //}
            //else
            //{
            //    MessageBox.Show("");
            //}

            //Write_Boundary_Offset_in_Model_File();

            if (MessageBox.Show("This process might take few minutes for Proceesing Data.", "ASTRA", MessageBoxButtons.OK) == DialogResult.OK)
            {
                if (RunProgram())
                    this.Close();
            }
        }

        private void btn_browse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Text Files(*.txt)|*.txt|All Files(*.*)|*.*";
                if (ofd.ShowDialog() != DialogResult.Cancel)
                {
                    txt_select_survey_data.Text = ofd.FileName;
                    btn_process.Enabled = File.Exists(Survey_File);
                }
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            AbortThread();
            this.Close();
        }

        private void txt_select_survey_data_TextChanged(object sender, EventArgs e)
        {
            btn_process.Enabled = File.Exists(Survey_File);
        }
       
    }
    public class SurveyData
    {
        public SurveyData()
        {
            SerialNo = -1;
            X = -999.0;
            Y = -999.0;
            Z = -999.0;
            FeatureCode = "OGL";
        }
        public int SerialNo { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public string FeatureCode { get; set; }

        public static SurveyData Parse(string line)
        {
            string kStr = MyStrings.RemoveAllSpaces(line.Replace(",", " "));
            MyStrings mlist = new MyStrings(kStr,' ');
            SurveyData sdata = new SurveyData();
            if (mlist.Count == 5)
            {
                sdata.SerialNo = mlist.GetInt(0);
                sdata.X = mlist.GetDouble(1);
                sdata.Y = mlist.GetDouble(2);
                sdata.Z = mlist.GetDouble(3);
                sdata.FeatureCode = mlist.StringList[4];
            }
            else if (mlist.Count == 4)
            {
                sdata.SerialNo = mlist.GetInt(0);
                sdata.X = mlist.GetDouble(1);
                sdata.Y = mlist.GetDouble(2);
                sdata.Z = mlist.GetDouble(3);
                sdata.FeatureCode = "OGL";
            }
            else if (mlist.Count == 3)
            {
                //sdata.SerialNo = mlist.GetInt(0);
                sdata.X = mlist.GetDouble(0);
                sdata.Y = mlist.GetDouble(1);
                sdata.Z = mlist.GetDouble(2);
                sdata.FeatureCode = "P0";
            }
            return sdata;
        }
        public override string ToString()
        {
            if (X == 0.0 && Y == 0.0)
                return string.Format("     0      0.000      0.000   -999.000 {0}", FeatureCode);

            return string.Format("{0} {1:f5} {2:f5} {3:f5} {4}", SerialNo, X, Y, Z, FeatureCode);
        }
        //public static operator ==(SurveyData s1, SurveyData s2)
        //{
        //}
        
    }
    public class SurveyDataCollection : IList<SurveyData>
    {
        List<SurveyData> list = null;
        public SurveyDataCollection(string survey_file)
        {
            list = new List<SurveyData>();
            Survey_Data_file = survey_file;
            ReadFromFile();


        }
        public string Survey_Data_file { get; set; }
        public string WorkingFolder
        {
            get
            {
                if (File.Exists(Survey_Data_file))
                    return Path.GetDirectoryName(Survey_Data_file);
                return "";
            }
        }
        public string Model
        {
            get
            {
                string  m = "SURVEY";
                if (File.Exists(Survey_Data_file))
                {
                    m = Path.GetFileNameWithoutExtension(Survey_Data_file);
                }
                if (m.Length > 8)
                    m = m.Substring(0, 8);
                return m.ToUpper().Trim();
            }
        }


        #region IList<SurveyData> Members

        public int IndexOf(SurveyData item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (item.X == list[i].X &&
                    item.Y == list[i].Y &&
                    item.Z == list[i].Z &&
                    item.FeatureCode == list[i].FeatureCode)
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, SurveyData item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public SurveyData this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        #endregion

        #region ICollection<SurveyData> Members

        public void Add(SurveyData item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(SurveyData item)
        {
            return (IndexOf(item) != -1);
        }

        public void CopyTo(SurveyData[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(SurveyData item)
        {
            int indx = IndexOf(item);

            if (indx != -1)
            {
                RemoveAt(indx);
                return true;
            }
            return false;
        }

        #endregion

        #region IEnumerable<SurveyData> Members

        public IEnumerator<SurveyData> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
            
        }

        #endregion

        void WriteModelLstFile(string strFilePath, List<ClSTtype> listData)
        {
            BinaryWriter bw = new BinaryWriter(new FileStream(strFilePath, FileMode.Append), Encoding.Default);
            foreach (ClSTtype lstdata in listData)
            {
                lstdata.ToStream(bw);
            }
            bw.Close();
        }
        void WriteModelFilFile(string strPath, List<CLabtype> listfil)
        {
            BinaryWriter bw = new BinaryWriter(new FileStream(strPath, FileMode.Append), Encoding.Default);
            for (int i = 0; i < listfil.Count; i++)
            {
                CLabtype lab = listfil[i];
                lab.ToStream(bw);
            }
            bw.Close();
        }

        public void ReadFromFile()
        {
            List<string> file_content = new List<string>(File.ReadAllLines(Survey_Data_file));

            string kStr = "";
            Clear();
            for (int i = 0; i < file_content.Count; i++)
            {
                try
                {
                    kStr = file_content[i];
                    Add(SurveyData.Parse(kStr));
                }
                catch (Exception ex) { }
            }
        }
        public bool Write_Model(string model)
        {

            string last_str = "";

            CLabtype lab;
            CModType mod;
            CStgType stg;
            CPTStype pts;
            CTXTtype txt;
            ClSTtype lst;

            List<CLabtype> listlabs = new List<CLabtype>();
            List<ClSTtype> listLst = new List<ClSTtype>();

            for (int i = 0; i < list.Count; i++)
            {
                if ((list[i].SerialNo == 0) ||
                    ((list[i].X <= -999 || list[i].X == 0.0) &&
                    (list[i].Y <= -999 || list[i].Y == 0.0)))
                {
                    lab = new CLabtype();
                    lab.attr = CLabtype.Type.EndCode;
                    listlabs.Add(lab);
                }
                else
                {
                    if (list[i].FeatureCode != last_str)
                    {
                        last_str = list[i].FeatureCode;

                        mod = new CModType();
                        mod.Name = Model;

                        lab = new CLabtype();
                        lab.attr = CLabtype.Type.Model;
                        lab.Tag = mod;
                        listlabs.Add(lab);

                        stg = new CStgType();
                        stg.label = last_str;

                        lab = new CLabtype();
                        lab.attr = CLabtype.Type.String;
                        lab.Tag = stg;
                        listlabs.Add(lab);


                        lst = new ClSTtype();
                        lst.strMod1 = Model;
                        lst.strStg = last_str;
                        listLst.Add(lst);
                    }

                    lab = new CLabtype();
                    lab.attr = CLabtype.Type.Point;

                    pts = new CPTStype();

                    pts.mx = list[i].X;
                    pts.my = list[i].Y;
                    pts.mz = (list[i].Z == 0.0) ? -999.0 : list[i].Z;
                    //pts.mc = 123.00;
                    pts.mc = (i + 1);
                    lab.Tag = pts;
                    listlabs.Add(lab);
                }
            }
            lab = new CLabtype();
            lab.attr = CLabtype.Type.EndCode;
            listlabs.Add(lab);

            string pth = Path.Combine(WorkingFolder, "MODEL.LST");
            WriteModelLstFile(pth, listLst);
            pth = Path.Combine(WorkingFolder, "MODEL.FIL");
            WriteModelFilFile(pth, listlabs);

            return true;
        }
        public bool Write_Model(Vertexes vertx, string model, string strlbl, string work_folder)
        {

            string last_str = "";

            CLabtype lab;
            CModType mod;
            CStgType stg;
            CPTStype pts;
            CTXTtype txt;
            ClSTtype lst;

            List<CLabtype> listlabs = new List<CLabtype>();
            List<ClSTtype> listLst = new List<ClSTtype>();

            mod = new CModType();
            mod.Name = model;

            lab = new CLabtype();
            lab.attr = CLabtype.Type.Model;
            lab.Tag = mod;
            listlabs.Add(lab);

            stg = new CStgType();
            stg.label = strlbl;

            lab = new CLabtype();
            lab.attr = CLabtype.Type.String;
            lab.Tag = stg;
            listlabs.Add(lab);


            lst = new ClSTtype();
            lst.strMod1 = model;
            lst.strStg = strlbl;
            listLst.Add(lst);
            for (int i = 0; i < vertx.Count; i++)
            {

                lab = new CLabtype();
                lab.attr = CLabtype.Type.Point;

                pts = new CPTStype();

                pts.mx = vertx[i].x;
                pts.my = vertx[i].y;
                pts.mz = (vertx[i].z == 0.0) ? -999.0 : vertx[i].z;
                pts.mc = (i+1);
                lab.Tag = pts;
                listlabs.Add(lab);
            }
            lab = new CLabtype();
            lab.attr = CLabtype.Type.EndCode;
            listlabs.Add(lab);

            string pth = Path.Combine(work_folder, "MODEL.LST");
            WriteModelLstFile(pth, listLst);
            pth = Path.Combine(work_folder, "MODEL.FIL");
            WriteModelFilFile(pth, listlabs);

            return true;
        }

    }
}
