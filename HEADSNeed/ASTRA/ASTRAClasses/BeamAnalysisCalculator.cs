using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using VectorDraw.Geometry;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;

namespace HEADSNeed.ASTRA.ASTRAClasses
{
    #region BEAM Analysis Calculator from From Formula
    public class BeamAnalysisCalculator : IBeamAnalysisCalculator
    {
        #region Member Variable

        double dStartMoment; // Start Moment / M1
        double dEndMoment; // End Moment / M2
        double dLength; // Length / l
        double dUdlLoad; // w
        double dStartReAction; // RA
        double dEndReAction; // RB
        double dMoment; // M
        double dStartDistance; // x
        MemberLoad MemberLoad;
        List<ConcentrateLoad> lstConLoad = null;
        #endregion

        #region ctor

        public BeamAnalysisCalculator()
        {
            InitializeVariable();
        }
        public void InitializeVariable()
        {
            dStartMoment = 0.0d; // Start Moment / M1
            dEndMoment = 0.0d; // End Moment / M2
            dLength = 0.0d; // Length / l
            dUdlLoad = 0.0d; // w
            dStartReAction = 0.0d; // RA
            dEndReAction = 0.0d; // RB
            dMoment = 0.0d; // M
            dStartDistance = 0.0d; // x
            lstConLoad = new List<ConcentrateLoad>();
        }
        #endregion

        #region Test
        public void Test()
        {
            Length = 5.0;
            UdlLoad = 3.0d;
            StartMoment = 11.13d;
            EndMoment = 13.77d;
            ConcentrateLoad cload = new ConcentrateLoad();

            cload.Distance = 2.0d;
            cload.Force = 5.0d;
            ConsLoads.Add(cload);

            cload = new ConcentrateLoad();
            cload.Distance = 4.0d;
            cload.Force = 8.0d;
            ConsLoads.Add(cload);

            //MessageBox.Show(CalculateMoment(1.5).ToString());
            //MessageBox.Show(CalculateMoment(2.5).ToString());
            //MessageBox.Show(CalculateMoment(3.5).ToString());
            //MessageBox.Show(CalculateMoment(4.5).ToString());
        }
        #endregion

        public double CalculateMoment_B(double x)
        {
            double M1, M2, w;
            M1 = M2 = w = 0.0d;
            int i = 0;
            // Calculate Start Reaction


            M1 = Math.Abs(StartMoment);
            M2 = Math.Abs(EndMoment);
            w = Math.Abs(UdlLoad);

            //M1 = StartMoment;
            //M2 = EndMoment;
            //w = UdlLoad;

            StartReaction = M1 - M2 - (w * Length * Length / 2.0);
            for (i = 0; i < ConsLoads.Count; i++)
            {
                StartReaction -= ConsLoads[i].Force * (Length - ConsLoads[i].Distance);
            }
            StartReaction = Math.Abs(StartReaction) / Length;
            Moment = StartReaction * x - w * x * x / 2.0d;
            for (i = 0; i < ConsLoads.Count; i++)
            {
                if (x >= ConsLoads[i].Distance)
                    Moment -= ConsLoads[i].Force * (x - ConsLoads[i].Distance);
            }
            return Moment;
        }

        public double CalculateMoment_A(double unitFactor)
        {
            int i = 0;
            double RB_l, l, M1, M2, w, p1, d1, p2, d2, MA;
            M1 = Math.Abs(StartMoment);
            M2 = Math.Abs(EndMoment);
            l = Length;
            w = Math.Abs(UdlLoad);
            //M1 = StartMoment;
            //M2 = EndMoment;
            //l = Length;
            //w = UdlLoad;


            RB_l = (-M1) + M2 + w * l * l / 2;
            for (i = 0; i < ConsLoads.Count; i++)
            {
                RB_l += ConsLoads[i].Force * ConsLoads[i].Distance * unitFactor;
            }

            RB_l = Math.Abs(RB_l);
            EndReaction = Math.Abs(RB_l) / l;

            MA = RB_l - w * l * l / 2;
            for (i = 0; i < ConsLoads.Count; i++)
            {
                MA -= ConsLoads[i].Force * ConsLoads[i].Distance * unitFactor;
            }
            return MA;
        }

        public double CalculateMomentWithConsLoad(double x)
        {
            // VAZIRANI & RATWANI Page 20-21
            // Chiranjit 29/11/2009
            int i = 0;
            double Mx, w, l, Ra, M;
            w = Math.Abs(UdlLoad);
            l = Length;

            M = 0;

            double minMoment = 0.0;
            minMoment = Math.Abs(StartMoment);
            if (Math.Abs(StartMoment) > Math.Abs(EndMoment))
                minMoment = Math.Abs(EndMoment);

            Mx = (w * l * x / 2.0) - (w * x * x / 2.0) - minMoment;
            //Mx = (w * l * x / 2.0) - (w * x * x / 2.0) - (w * l * l / 12.0d);


            //Ra = w * l / 2.0d;
            Ra = 0.0d;
            if (ConsLoads.Count > 0)
            {
                for (i = 0; i < ConsLoads.Count; i++)
                {
                    if (x > ConsLoads[i].Distance)
                        Ra -= (ConsLoads[i].Force * (Length - ConsLoads[i].Distance)) / Length;
                }
                M = Ra * x;
            }
            return Mx + M;
        }
        public double CalculateMoment(double x)
        {
            // VAZIRANI & RATWANI Page 20-21
            // Chiranjit 29/11/2009
            int i = 0;
            double Mx, w, l,Ra,M;
            w = Math.Abs(UdlLoad);
            l = Length;

            M = 0;
            //Mx = (w * l * x / 2.0) - (w * x * x / 2.0) - StartMoment;
            Mx = (w * l * x / 2.0) - (w * x * x / 2.0) - (w * l * l / 12.0d);

            //Ra = w * l / 2.0d;

            //if (ConsLoads.Count > 0)
            //{
            //    for (i = 0; i < ConsLoads.Count; i++)
            //    {
            //        if (x >= ConsLoads[i].Distance)
            //            Ra -= (ConsLoads[i].Force * ConsLoads[i].Distance * (Math.Pow((Length - ConsLoads[i].Distance), 2.0))) / Math.Pow(Length, 2.0);
            //    }

            //    M = Ra * x;
            //    for (i = 0; i < ConsLoads.Count; i++)
            //    {
            //        if (x >= ConsLoads[i].Distance)
            //            M -= ConsLoads[i].Force * (x - ConsLoads[i].Distance); 
            //    }
            //}

            return Mx + M;
        }
        public struct ConcentrateLoad
        {
            double dDistance;
            double dForce;
            public ConcentrateLoad(double Dist,double Foc)
            {
                dDistance = Dist;
                dForce = Foc;
            }
            public double Distance
            {
                get
                {
                    return dDistance;
                }
                set
                {
                    dDistance = value;
                }
            }
            public double Force
            {
                get
                {
                    return dForce;
                }
                set
                {
                    dForce = value;
                }
            }
        }

        #region IBeamAnalysisCalculator Members
        public double StartMoment
        {
            get
            {
                return dStartMoment;
            }
            set
            {
                dStartMoment = value;
            }
        }
        public double EndMoment
        {
            get
            {
                return dEndMoment;
            }
            set
            {
                dEndMoment = value;
            }
        }
        public double Length
        {
            get
            {
                return dLength;
            }
            set
            {
                dLength = value;
            }
        }
        public double UdlLoad
        {
            get
            {
                return dUdlLoad;
            }
            set
            {
                dUdlLoad = value;
            }
        }
        public double StartReaction
        {
            get
            {
                return dStartReAction;
            }
            set
            {
                dStartReAction = value;
            }
        }
        public double EndReaction
        {
            get
            {
                return dEndReAction;
            }
            set
            {
                dEndReAction = value;
            }
        }
        public double Moment
        {
            get
            {
                return dMoment;
            }
            set
            {
                dMoment = value;
            }
        }
        public double StartDistance
        {
            get
            {
                return dStartDistance;
            }
            set
            {
                dStartDistance = value;
            }
        }
        #endregion

        public List<ConcentrateLoad> ConsLoads
        {
            get
            {
                return lstConLoad;
            }
        }

        public gPoints GetPoints(int div,double unitFactor)
        {
            gPoints gps = new gPoints();
            div = 10;
            double x, y,xIncr;
            x = y = xIncr = 0.0d;
            xIncr = Length / div;

            UdlLoad = UdlLoad / unitFactor;
            for (int i = 0; i <= div; i++)
            {
                y = CalculateMomentWithConsLoad(x);
                if (i == 0)
                    y = -(Math.Abs(StartMoment));
                else if (i == div)
                    y = -(Math.Abs(EndMoment));
                gPoint gp = new gPoint(x, y);
                gps.Add(gp);
                x += xIncr;
            }
            return gps;
        }
    }
    public interface IBeamAnalysisCalculator
    {
        double StartMoment { get; set; } // Start Moment / M1
        double EndMoment { get; set; }  // End Moment / M2
        double Length { get; set; }  // Length / l
        double UdlLoad { get; set; }  // w
        double StartReaction { get; set; }  // RA
        double EndReaction { get; set; }  // RB
        double Moment { get; set; }  // M
        double StartDistance { get; set; }

    }
    #endregion
    #region Calculator Test from DESIGN Report File
    public class BeamAnalysisCalculator_Test
    {
        StreamReader sr;
        List<CLoadDetails> srchLDetails = new List<CLoadDetails>();
        List<CLoadDetails> lstLoad = new List<CLoadDetails>();
        List<CSpanDetails> lstSpan = new List<CSpanDetails>();
        string EnvVerName = "", AppDataPath = "", FilePath = "";
        string title = "";
        int loadNext = 0;
        int indx = -1;
        int loadNo = 0;

        #region Variable Declaration

        List<double> lstF = new List<double>();
        List<double> lstML = new List<double>();
        List<double> lstMR = new List<double>();
        List<double> lstI2 = new List<double>();
        List<double> lstR = new List<double>();
        List<double> lstL = new List<double>();
        List<double> lstFL = new List<double>();
        List<double> lstFR = new List<double>();
        List<int> lstNL = new List<int>();
        List<double> lstD = new List<double>();
        List<double> lstE = new List<double>();
        List<double> lstK2 = new List<double>();
        List<double> lstMM = new List<double>();
        List<double> lstSM = new List<double>();
        double[,] VS = new double[100, 12];
        double[,] MS = new double[100, 22];

        double Z1 = 0;
        double CL = 0.0;
        double CR = 0.0;
        double T, FL1, FR1;
        double RL = 0.0;
        double S = 0.0;
        double RR = 0.0;
        double VK = 0.0;
        double Z = 0.0;
        double MK = 0.0;
        double D1 = 0.0;
        double F1 = 0.0;
        double WZ = 0;

        int segEnv = 21;
        int NS = 0;
        int i, j, k, kk;

        #endregion


        public void SetDefaultValues(double SpanLength, double loadVal, double MomentOfInertia)
        {
            CLoadDetails cld = new CLoadDetails();
            cld.iSpanNo = 1;
            cld.dLoadWeight = loadVal;
            //cld.dLoadWeight = 306.0;
            cld.dLoadStartDistance = 0.0;
            cld.dLoadCoverDistance = SpanLength;
            cld.iLoadNo = 1;
            lstLoad.Add(cld);


            //cld = new CLoadDetails();
            //cld.iSpanNo = 2;
            //cld.dLoadWeight = 100.0;
            //cld.dLoadStartDistance = 0.0;
            //cld.dLoadCoverDistance = 4.0;
            //cld.iLoadNo = 1;
            //lstLoad.Add(cld);

            //cld = new CLoadDetails();
            //cld.iSpanNo = 3;
            //cld.dLoadWeight = 306.0;
            //cld.dLoadStartDistance = 0.0;
            //cld.dLoadCoverDistance = 6.0;
            //cld.iLoadNo = 1;
            //lstLoad.Add(cld);

            CSpanDetails csd = new CSpanDetails();
            csd.iSpanNo = 1;
            csd.dSpanLength = SpanLength;
            csd.dMomentOfInertia = MomentOfInertia;
            lstSpan.Add(csd);

            //csd = new CSpanDetails();
            //csd.iSpanNo = 2;
            //csd.dSpanLength = 4;
            //csd.dMomentOfInertia = 1200;
            //lstSpan.Add(csd);

            //csd = new CSpanDetails();
            //csd.iSpanNo = 3;
            //csd.dSpanLength = 6;
            //csd.dMomentOfInertia = 1200;
            //lstSpan.Add(csd);

            lstNL.Add(1);
            lstNL.Add(1);
            lstNL.Add(1);

            NS = 1;

        }
        public void SetDefaultValues()
        {
            CLoadDetails cld = new CLoadDetails();
            cld.iSpanNo = 1;
            cld.dLoadWeight = 306.0;
            cld.dLoadStartDistance = 0.0;
            cld.dLoadCoverDistance = 6.0;
            cld.iLoadNo = 1;
            lstLoad.Add(cld);


            cld = new CLoadDetails();
            cld.iSpanNo = 2;
            cld.dLoadWeight = 100.0;
            cld.dLoadStartDistance = 0.0;
            cld.dLoadCoverDistance = 4.0;
            cld.iLoadNo = 1;
            lstLoad.Add(cld);

            cld = new CLoadDetails();
            cld.iSpanNo = 3;
            cld.dLoadWeight = 306.0;
            cld.dLoadStartDistance = 0.0;
            cld.dLoadCoverDistance = 6.0;
            cld.iLoadNo = 1;
            lstLoad.Add(cld);

            CSpanDetails csd = new CSpanDetails();
            csd.iSpanNo = 1;
            csd.dSpanLength = 6;
            csd.dMomentOfInertia = 1200;
            lstSpan.Add(csd);

            csd = new CSpanDetails();
            csd.iSpanNo = 2;
            csd.dSpanLength = 4;
            csd.dMomentOfInertia = 1200;
            lstSpan.Add(csd);

            csd = new CSpanDetails();
            csd.iSpanNo = 3;
            csd.dSpanLength = 6;
            csd.dMomentOfInertia = 1200;
            lstSpan.Add(csd);
            lstNL.Add(1);
            lstNL.Add(1);
            lstNL.Add(1);

            NS = 3;

        }
        public void InitialisingVariables()
        {
            lstMM.Clear();
            lstSM.Clear();

            lstF.Clear();
            lstML.Clear();
            lstMR.Clear();
            lstI2.Clear();
            lstR.Clear();
            lstL.Clear();
            lstFL.Clear();
            lstFR.Clear();
            lstD.Clear();
            lstE.Clear();
            lstK2.Clear();

            for (i = 0; i <= NS + 2; i++)
            {
                lstMM.Add(0);
                lstSM.Add(0);

                lstF.Add(0.0);
                lstML.Add(0.0);
                lstMR.Add(0.0);
                lstI2.Add(0.0);
                lstR.Add(0.0);
                lstL.Add(0.0);
                lstFL.Add(0.0);
                lstFR.Add(0.0);
                lstD.Add(0.0);
                lstE.Add(0.0);
                lstK2.Add(0.0);
            }
        }
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }

        }
        public bool MainFunction(string fileName, int MemberNo, double SpanLength, double LoadValue, double MomentOfInertia, double StartMoment, double EndMoment)
        {
            bool success = false;
            try
            {
                CL = StartMoment;
                CR = EndMoment;
                lstLoad.Clear();
                lstSpan.Clear();
                lstLoad.Add(new CLoadDetails());
                lstSpan.Add(new CSpanDetails());
                lstNL.Add(0);
                InitialisingVariables();
                //InitialisingVariables();
                FilePath = fileName;
                SetDefaultValues(SpanLength, LoadValue, MomentOfInertia);
                MainFunction();
                success = WriteToFile();
            }
            catch (Exception ex)
            {
                success = false;
            }
            return success;
        }
        public void MainFunction()
        {
            // Beam Stiffness
            for (i = 1; i <= NS; i++)
            {
                //K2[i] = I2[i] / L[i];
                lstK2[i] = (lstSpan[i].dMomentOfInertia / lstSpan[i].dSpanLength);
                //K2[i] = I2[i] / lstSpan[i].dSpanLength;
            }
            SlopDeflectionEqUnLHS();
            FixedAndMoments();
            SlopDEflectionEqUnRHS();
            EndMomentCalc();
            CalcSpanShearsAndMoments();

            // Sort for maximum span moments and their positions
            for (i = 1; i <= NS; i++)
            {
                for (k = 1; k <= 21; k++)
                {
                    if (MS[i, k] > lstMM[i])
                    {
                        lstMM[i] = MS[i, k];
                        lstSM[i] = lstSpan[i].dSpanLength * (k - 1) / 20;
                    }
                }
            }

            // Print out Data and Results
            //FileStream fis = new FileStream("ANALYSIS_REP.TXT", FileMode.Create);

            //if (WriteToFile())
            //{
            //    MessageBox.Show(" Design output is written in " + FilePath + "", "Analysis Continuous Beam", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
            //else
            //    MessageBox.Show("Writing is not completing.", "Analysis Continuous Beam", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        void GetWorkingDirFromEnvVariable()
        {
            //this.EnvVerName = @"C:\Test\INP.txt";
            string strFileName = System.Environment.GetEnvironmentVariable("SURVEY");

            if (strFileName != null)
            {
                if (strFileName != "" && File.Exists(strFileName))
                {
                    this.AppDataPath = Path.GetDirectoryName(strFileName);
                }
            }
        }
        public bool WriteToFile()
        {
            //try
            //{
            //    FilePath = "";
            //    sr = new StreamReader(Path.Combine(Application.StartupPath, "PAT001.tmp"));
            //    FilePath = sr.ReadLine();
            //    sr.Close();

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("The path of user's working folder was not found...", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    //this.Close();
            //    return false;
            //}
            if (Directory.Exists(FilePath))
            {
                FilePath = Path.Combine(FilePath, "DESIGN.REP");
            }
            else
                FilePath = Path.Combine(Path.GetDirectoryName(FilePath), "DESIGN.REP");

            WriteToFile(FilePath);

            if (Directory.Exists(FilePath))
            {
                FilePath = Path.Combine(FilePath, "TEST.REP");
            }
            else
                FilePath = Path.Combine(Path.GetDirectoryName(FilePath), "TEST.REP");

            //FilePath = Path.Combine(Path.GetDirectoryName(FilePath), "TEST.REP");
            if (File.Exists(FilePath)) File.Delete(FilePath);

            return WriteToFile(FilePath);

            //return true;
        }
        public bool WriteToFile(string filePath)
        {

            // Patch File

            FilePath = filePath;
            FileStream fis = new FileStream(FilePath, FileMode.Append);
            //FileStream fis = new FileStream("ANALYSIS_REP.TXT", FileMode.Append);
            StreamWriter sw = new StreamWriter(fis);

            sw.WriteLine("----------------------------------------------------------------------------------------------");
            sw.WriteLine("----------------------------------------------------------------------------------------------");
            sw.WriteLine("\t\t\t**********************************************");
            sw.WriteLine("\t\t\t*             ASTRA Pro Release 2.0          *");
            sw.WriteLine("\t\t\t*         TechSOFT Engineering Services.     *");
            sw.WriteLine("\t\t\t*                                            *");
            sw.WriteLine("\t\t\t*              ASTRA DESIGN SUIT             *");
            sw.WriteLine("\t\t\t*   ANALYSIS AND DESIGN OF CONTINUOUS BEAM   *");
            sw.WriteLine("\t\t\t**********************************************");
            sw.WriteLine("\t\t\t----------------------------------------------");
            sw.WriteLine("\t\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            sw.WriteLine("\t\t\t----------------------------------------------");

            try
            {
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("----------------------------------------------------------------------------------------------");
                if (Title == "")
                {
                    Title = "BEAM EXAMPLE-FLOOR 2-MEMBER ID 38";
                }
                string str = Title;
                sw.WriteLine("TITLE " + Title);

                sw.WriteLine("STRUCTURE INFORMATION");
                sw.WriteLine("---------------------");
                sw.WriteLine("NUMBER OF SPANS " + NS);
                sw.WriteLine("{0,20} {1,30} {2,30}", "SPAN NO", "SPAN LENGTH(m)", "2ND MOMENT OF AREA(mm^4)");
                for (i = 1; i <= NS; i++)
                {
                    sw.WriteLine("{0,15} {1,25} {2,30}", i, lstSpan[i].dSpanLength.ToString("0.00"), lstSpan[i].dMomentOfInertia.ToString("0.00"));
                    //sw.WriteLine("{0,20} {1,30} {2,30}", i, lstSpan[i].dSpanLength.ToString("0.00"), I2[i].ToString("0.00"));
                }

                sw.WriteLine("\n\n\n");
                sw.WriteLine("LOADING INFORMATION");
                sw.WriteLine("-------------------");
                sw.WriteLine("{0,20} {1,20} {2,20} {3,15}", "SPAN NO", "LOAD WEIGHT(kN)", "START DISTANCE(m)", "COVER");
                for (i = 1; i <= NS; i++)
                {
                    if (lstNL[i] == 0) continue;
                    this.SetLoadBySpan(i);
                    //for (j = 1; j <= lstNL[i]; j++)
                    //{
                    //    sw.WriteLine("{0,20} {1,30} {2,30} {3,30}", i, W[i, j].ToString("0.00"), A[i, j].ToString("0.00"), C[i, j].ToString("0.00"));
                    //}
                    for (j = 1; j < srchLDetails.Count; j++)
                    {
                        sw.WriteLine("{0,15} {1,22} {2,20} {3,18}", i, srchLDetails[j].dLoadWeight.ToString("0.00"), srchLDetails[j].dLoadStartDistance.ToString("0.00"), srchLDetails[j].dLoadCoverDistance.ToString("0.00"));
                    }

                }

                sw.WriteLine("\nCANTILEVER MOMENT AT L.H.S. = " + CL + " kN.m");
                sw.WriteLine("CANTILEVER MOMENT AT R.H.S. = " + CR + " kN.m");
                sw.WriteLine();

                sw.WriteLine("\n\n SPAN SHEARS AND MOMENTS");
                sw.WriteLine(" -----------------------");
                sw.WriteLine("SHEARS,kN AND MOMENTS,kN.m AT 10TH INTERVALS ALONG SPANS");
                for (i = 1; i <= NS; i++)
                {
                    sw.WriteLine("\n SPAN NO. " + (i));
                    sw.WriteLine("{0,20} {3,11} {1,24} {2,30}", "SECTION NO.", "SHEAR", "MOMENT", "DISTANCE(m)");
                    int cc = 1;
                    for (k = 1; k <= 21; k += 2)
                    {
                        cc = ((k + 1) / 2);

                        double dd = (lstSpan[i].dSpanLength * (cc - 1)) / 10.0d;
                        sw.WriteLine("{0,15} {3,12} {1,28} {2,30}", ((k + 1) / 2), VS[i, cc].ToString("0.00"), MS[i, k].ToString("0.00"), dd.ToString("0.00"));
                    }
                    sw.WriteLine("\n\n MAXIMUM SAGGING MOMENT = " + lstMM[i].ToString("0.00") + " kN.m");
                    sw.WriteLine(" AT A DISTANCE = " + lstSM[i] + " metres");
                }

                sw.WriteLine("----------------------------------------   END OF DESIGN  ------------------------------------");
                sw.WriteLine("----------------------------------------------------------------------------------------------");
                sw.Flush();
                sw.Close();
                return true;
            }
            catch (Exception ex)
            {
                sw.Flush();
                sw.Close();
            }
            return false;

        }
        private void btnProceed_Click(object sender, EventArgs e)
        {
            //CL = double.Parse(txtCL.Text);
            //CR = double.Parse(txtCR.Text);
            //InitialisingVariables();
            //MainFunction();
        }

        private void SlopDeflectionEqUnLHS()
        {
            lstD[1] = 4 * lstK2[1];
            lstE[1] = 2 * lstK2[1];

            //lstD.Add(4 * lstK2[1]);
            //lstE.Add(2 * lstK2[1]);
            for (i = 2; i <= NS; i++)
            {
                lstD[i] = (4 * (lstK2[i - 1] + lstK2[i]));
                lstE[i] = (2 * lstK2[i]);
                //D[i] = 4 * (K2[i - 1] + K2[i]);
                //E[i] = 2 * K2[i];
            }
            lstD[NS + 1] = (4 * lstK2[NS]);
            lstE[NS + 1] = 0.0;
            //D[NS + 1] = 4 * K2[NS];
            //E[NS + 1] = 0.0;
            return;
        }
        private void FixedAndMoments()
        {
            for (i = 1; i <= NS; i++)
            {
                //FL[i] = 0;
                //FR[i] = 0;
                lstFL[i] = 0.0;
                lstFR[i] = 0.0;

                if (lstNL[i] == 0) continue;

                this.SetLoadBySpan(i);
                for (j = 1; j <= lstNL[i]; j++)
                {
                    S = srchLDetails[j].dLoadStartDistance + (srchLDetails[j].dLoadCoverDistance / 2);
                    T = lstSpan[i].dSpanLength - S;
                    FL1 = srchLDetails[j].dLoadWeight * (S * T * T + (S - 2 * T) * srchLDetails[j].dLoadCoverDistance * srchLDetails[j].dLoadCoverDistance / 12.0) / (lstSpan[i].dSpanLength * lstSpan[i].dSpanLength);
                    FR1 = srchLDetails[j].dLoadWeight * (T * S * S + (T - 2 * S) * srchLDetails[j].dLoadCoverDistance * srchLDetails[j].dLoadCoverDistance / 12.0) / (lstSpan[i].dSpanLength * lstSpan[i].dSpanLength);
                    lstFL[i] += FL1;
                    lstFR[i] += FR1;
                }

                //for (j = 1; j <= lstNL[i]; j++)
                //{
                //    S = A[i, j] + (C[i, j] / 2);
                //    T = lstSpan[i].dSpanLength - S;
                //    FL1 = W[i, j] * (S * T * T + (S - 2 * T) * C[i, j] * C[i, j] / 12.0) / (lstSpan[i].dSpanLength * lstSpan[i].dSpanLength);
                //    FR1 = W[i, j] * (T * S * S + (T - 2 * S) * C[i, j] * C[i, j] / 12.0) / (lstSpan[i].dSpanLength * lstSpan[i].dSpanLength);
                //    FL[i] += FL1;
                //    FR[i] += FR1;
                //}
            }
            return;
        }
        private void SlopDEflectionEqUnRHS()
        {
            lstF[1] = (-lstFL[1] + CL);
            //F[1] = -FL[1] + CL;
            for (i = 2; i <= NS; i++)
            {
                //double val1 = FR[i - 1];
                //double val2 = FL[i];
                lstF[i] = (lstFR[i - 1] - lstFL[i]);
                //F[i] = FR[i - 1] - FL[i];
            }

            lstF[NS + 1] = (lstFR[NS] - CR);
            return;
        }
        private void EndMomentCalc()
        {
            D1 = lstD[1];
            F1 = lstF[1];

            for (i = 2; i <= NS + 1; i++)
            {
                F1 = lstF[i] - lstE[i - 1] * F1 / D1;
                D1 = lstD[i] - (lstE[i - 1] * lstE[i - 1]) / D1;
            }
            lstR[NS + 1] = F1 / D1;
            lstR[NS] = ((lstF[NS + 1] - lstD[NS + 1] * lstR[NS + 1]) / lstE[NS]);

            for (i = NS; i >= 2; i--)
            {
                lstR[i - 1] = (lstF[i] - lstD[i] * lstR[i] - lstE[i] * lstR[i + 1]) / lstE[i - 1];
            }

            for (i = 1; i <= NS; i++)
            {
                lstML[i] = ((4 * lstR[i] + 2 * lstR[i + 1]) * lstK2[i] + lstFL[i]);
                lstMR[i] = ((2 * lstR[i] + 4 * lstR[i + 1]) * lstK2[i] - lstFR[i]);
            }
            return;
        }
        private void CalcSpanShearsAndMoments()
        {

            for (i = 1; i <= NS; i++)
            {
                //Calculation of Span shears and moments due to end moments

                this.SetLoadBySpan(i);
                RL = (lstML[i] + lstMR[i]) / lstSpan[i].dSpanLength;
                for (k = 1; k <= 11; k++)
                {
                    VS[i, k] = RL;
                }
                for (k = 1; k <= 21; k++)
                {
                    MS[i, k] = -lstML[i] + RL * lstSpan[i].dSpanLength * (k - 1) / 20;
                }
                if (lstNL[i] == 0) continue;

                for (j = 1; j <= lstNL[i]; j++)
                {
                    S = lstSpan[i].dSpanLength - srchLDetails[j].dLoadStartDistance - srchLDetails[j].dLoadCoverDistance / 2.0;
                    //S = lstSpan[i].dSpanLength - A[i, j] - C[i, j] / 2.0;
                    RL = srchLDetails[j].dLoadWeight * S / lstSpan[i].dSpanLength;
                    RR = srchLDetails[j].dLoadWeight - RL;
                    // Span Shears 10th intervals
                    for (k = 1; k <= 11; k++)
                    {
                        Z = (k - 1) * lstSpan[i].dSpanLength / 10;
                        //if(k == 1) z
                        if (Z <= srchLDetails[j].dLoadStartDistance)
                        {
                            VK = RL;
                        }
                        else if (Z > (srchLDetails[j].dLoadStartDistance + srchLDetails[j].dLoadCoverDistance))
                        {
                            VK = -RR;
                        }
                        else
                        {
                            Z1 = Z - srchLDetails[j].dLoadStartDistance;
                            VK = RL - srchLDetails[j].dLoadWeight * Z1 / srchLDetails[j].dLoadCoverDistance;
                        }
                        VS[i, k] += VK;
                    }
                    for (k = 1; k <= segEnv; k++)
                    {
                        Z = (k - 1) * lstSpan[i].dSpanLength / 20;
                        if (Z <= srchLDetails[j].dLoadStartDistance)
                        {
                            MK = RL * Z;
                        }
                        else if (Z > (srchLDetails[j].dLoadStartDistance + srchLDetails[j].dLoadCoverDistance))
                        {
                            MK = RR * (lstSpan[i].dSpanLength - Z);
                        }
                        else
                        {
                            Z1 = Z - srchLDetails[j].dLoadStartDistance;

                            WZ = srchLDetails[j].dLoadWeight * Z1 / srchLDetails[j].dLoadCoverDistance;
                            MK = RL * Z - WZ * Z1 / 2;
                            MS[i, k] += MK;
                        }
                    } // k
                } // j
            }//i
        }

        private void frmAnalysisContBeam_Load(object sender, EventArgs e)
        {
            lstLoad.Clear();
            lstSpan.Clear();
            lstLoad.Add(new CLoadDetails());
            lstSpan.Add(new CSpanDetails());
            lstNL.Add(0);
            InitialisingVariables();
            SetDefaultValues();

            //SetWorkingDirFromEnvVariable();
            //GetWorkingDirFromEnvVariable();
        }

        public void SetLoadBySpan(int SpanNo)
        {
            srchLDetails.Clear();
            srchLDetails.Add(new CLoadDetails());
            for (kk = 0; kk < lstLoad.Count; kk++)
            {
                if (lstLoad[kk].iSpanNo == SpanNo) srchLDetails.Add(lstLoad[i]);
            }
        }
        public void WorkSpanNext()
        {
        }
        public void WorkLoadNext()
        {

        }
        public void WorkSpanPrevious()
        {
        }
        public void WorkLoadPrevious()
        {

        }
        public int GetLoadIndex(int SpanNo)
        {
            return -1;
        }
        public int GetLoadIndex(int SpanNo, int LoadNo)
        {
            int n = 0;
            for (n = 0; n < lstLoad.Count; n++)
            {
                if (lstLoad[n].iSpanNo == SpanNo && lstLoad[n].iLoadNo == LoadNo)
                {
                    return n;
                }
            }
            return -1;
        }
        public int GetSpanIndex(int SpanNo)
        {
            int n = 0;
            for (n = 0; n < lstSpan.Count; n++)
            {
                if (lstSpan[n].iSpanNo == SpanNo)
                {
                    return n;
                }
            }
            return -1;
        }
    }
    public class CLoadDetails
    {
        public CLoadDetails() { }
        public int iSpanNo;
        public int iLoadNo;
        public double dLoadWeight;
        public double dLoadStartDistance;
        public double dLoadCoverDistance;
    }
    public class CSpanDetails
    {
        public CSpanDetails() { }
        public int iSpanNo;
        public double dSpanLength;
        public double dMomentOfInertia;
    }
    #endregion
}
