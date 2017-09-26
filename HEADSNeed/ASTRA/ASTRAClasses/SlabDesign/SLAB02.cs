using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Drawing;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Geometry;

namespace HEADSNeed.ASTRA.ASTRAClasses.SlabDesign
{
    public class SLAB02:ISLAB02
    {
        #region Member Variable
        double l; // Floor Slab Length
        double b; // Floor Slab Breadth
        double ll; // Super imposed/ Live Load
        double sigma_ck; // Concrete Grade
        double sigma_y; // Steal Grade
        double alpha; // Constants 
        double beta; // Constants
        double gamma; // Constants
        double delta; // Constants
        double lamda; // Constants
        double d1; // Dia of Main Reinforcement
        double d2; // Dia of Distribution Reinforcement
        double h1; // Clear Cover
        double h2; // End Cover
        double ads; // Provide Distribution Reinforcements
        double tc; // Shear Strength of concrete as % of Steal
        double Slab_load; // Shear Strength of concrete as % of Steal


        KValueTable kValue = null;

        string DataPath = "";
        #endregion
        #region ctor
        public SLAB02()
        {
            l = 0.0; // Floor Slab Length
            b = 0.0; // Floor Slab Breadth
            ll = 0.0; // Super imposed/ Live Load
            sigma_ck = 0.0; // Concrete Grade
            sigma_y = 0.0; // Steal Grade
            alpha = 0.0; // Constants 
            beta = 0.0; // Constants
            gamma = 0.0; // Constants
            delta = 0.0; // Constants
            lamda = 0.0; // Constants
            d1 = 0.0; // Dia of Main Reinforcement
            d2 = 0.0; // Dia of Distribution Reinforcement
            h1 = 0.0; // Clear Cover
            h2 = 0.0; // End Cover
            ads = 0.0; // Provide Distribution Reinforcements
            tc = 0.0; // Shear Strength of concrete as % of Steal
            Slab_load = 0.0d;
            //kValue = new KValueTable(kValueFilePath);
            //if (File.Exists(dataPath))
            //{
            //    DataPath = Path.GetDirectoryName(dataPath);
            //}
            //else
            //    DataPath = dataPath;

            //DataPath = Path.Combine(DataPath, "DESIGN_SLAB02");
            //if (!Directory.Exists(DataPath))
            //    Directory.CreateDirectory(DataPath);
            //CalculateMethod();
        }
        public SLAB02(string kValueFilePath, string dataPath)
        {
            l = 0.0; // Floor Slab Length
            b = 0.0; // Floor Slab Breadth
            ll = 0.0; // Super imposed/ Live Load
            sigma_ck = 0.0; // Concrete Grade
            sigma_y = 0.0; // Steal Grade
            alpha = 0.0; // Constants 
            beta = 0.0; // Constants
            gamma = 0.0; // Constants
            delta = 0.0; // Constants
            lamda = 0.0; // Constants
            d1 = 0.0; // Dia of Main Reinforcement
            d2 = 0.0; // Dia of Distribution Reinforcement
            h1 = 0.0; // Clear Cover
            h2 = 0.0; // End Cover
            ads = 0.0; // Provide Distribution Reinforcements
            tc = 0.0; // Shear Strength of concrete as % of Steal
            Slab_load = 0.0d;
            kValue = new KValueTable(kValueFilePath);
            if (File.Exists(dataPath))
            {
                DataPath = Path.GetDirectoryName(dataPath);
            }
            else
                DataPath = dataPath;

            //DataPath = Path.Combine(DataPath, "DESIGN_SLAB02");
            //if (!Directory.Exists(DataPath))
            //    Directory.CreateDirectory(DataPath);
            //CalculateMethod();
        }
        #endregion
        #region Public Property
        #region ISLAB02 Members

        public double L
        {
            get
            {
                return l;
            }
            set
            {
                l = value;
            }
        }

        public double B
        {
            get
            {
                return b;
            }
            set
            {
                b = value;
            }
        }

        public double LL
        {
            get
            {
                return ll;
            }
            set
            {
                ll = value;
            }
        }

        public double Sigma_ck
        {
            get
            {
                return sigma_ck;
            }
            set
            {
                sigma_ck = value;
            }
        }

        public double Sigma_y
        {
            get
            {
                return sigma_y;
            }
            set
            {
                sigma_y = value;
            }
        }

        public double Alpha
        {
            get
            {
                return alpha;
            }
            set
            {
                alpha = value;
            }
        }

        public double Beta
        {
            get
            {
                return beta;
            }
            set
            {
                beta = value;
            }
        }

        public double Gamma
        {
            get
            {
                return gamma;
            }
            set
            {
                gamma = value;
            }
        }

        public double Delta
        {
            get
            {
                return delta;
            }
            set
            {
                delta = value;
            }
        }

        public double Lamda
        {
            get
            {
                return lamda;
            }
            set
            {
                lamda = value;
            }
        }

        public double D1
        {
            get
            {
                return d1;
            }
            set
            {
                d1 = value;
            }
        }

        public double D2
        {
            get
            {
                return d2;
            }
            set
            {
                d2 = value;
            }
        }

        public double Slab_Load
        {
            get
            {
                return Slab_load;
            }
            set
            {
                Slab_load = value;
            }
        }
        public double H1
        {
            get
            {
                return h1;
            }
            set
            {
                h1 = value;
            }
        }

        public double H2
        {
            get
            {
                return h2;
            }
            set
            {
                h2 = value;
            }
        }

        public double Ads
        {
            get
            {
                return ads;
            }
            set
            {
                ads = value;
            }
        }

        public double Tc
        {
            get
            {
                return tc;
            }
            set
            {
                tc = value;
            }
        }

        #endregion

        #endregion

        #region Public Method

        public void CalculateMethod()
        {
            string fName = Path.Combine(DataPath, "SLAB02.txt");
            StreamWriter sw = new StreamWriter(new FileStream(fName, FileMode.Create));

            try
            {
                sw.WriteLine("----------------------------------------------------------------------------------------------");
                sw.WriteLine("----------------------------------------------------------------------------------------------");
                sw.WriteLine("\t\t\t**********************************************");
                sw.WriteLine("\t\t\t*            ASTRA Pro Release 2009          *");
                sw.WriteLine("\t\t\t* TechSOFT Engineering Services (I) Pvt. Ltd.*");
                sw.WriteLine("\t\t\t*                                            *");
                sw.WriteLine("\t\t\t*           DESIGN OF SINGLE SPAN            *");
                sw.WriteLine("\t\t\t*  ONE WAY RCC SLAB BY LIMIT STATE METHOD    *");
                sw.WriteLine("\t\t\t**********************************************");
                sw.WriteLine("\t\t\t----------------------------------------------");
                sw.WriteLine("\t\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
                sw.WriteLine("\t\t\t----------------------------------------------");
                sw.WriteLine("----------------------------------------------------------------------------------------------");
                sw.WriteLine("----------------------------------------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("----------------------------------------------------------------------------------------------");
                sw.WriteLine("■ STEP 1:");
                sw.WriteLine(" Calculations for Overall and effective depth");
                sw.WriteLine("----------------------------------------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Since length of the slab is more than twice the width, it is a one way slab.");
                sw.WriteLine("Load will be transferred to the supports along the shorter span.");
                sw.WriteLine("Consider a 100 cm wide strip of the slab parallel to its shorter span.");

                double d, lowest_Span;
                lowest_Span = (L > B) ? B * 1000 : L * 1000;
                d = (lowest_Span / (alpha * beta * gamma * delta * lamda));

                sw.WriteLine("Minimum depth of slab d = L /(α * β * γ * δ * λ)");
                sw.WriteLine();
                sw.WriteLine("Let α = {0}, β = {1}, γ = {2}, δ = {3}, λ = {4})", alpha, beta, gamma, delta, lamda);
                sw.WriteLine("So, d = {0}/{1} = {2} mm", lowest_Span.ToString("0.0"), (alpha * beta * gamma * delta * lamda).ToString("0.00"), d.ToString("0.00"));

                double D = (d + h1);
                sw.WriteLine("Let us adopt overall depth   D = {0} mm.", D.ToString("0.00"));
                sw.WriteLine();
                sw.WriteLine("----------------------------------------------------------------------------------------------");
                sw.WriteLine("■ STEP 2:");
                sw.WriteLine("Calculations for Design Load, Moment and Shear");
                sw.WriteLine("----------------------------------------------------------------------------------------------");
                sw.WriteLine();
                double deadLoad_slab = (D / 1000) * 1.0 * Slab_load;
                sw.WriteLine("Dead Load of slab = {0} * {1} * {2} = {3} kN/m.", (D / 1000).ToString("0.00"), 1.0, Slab_load, deadLoad_slab.ToString("0.00"));
                sw.WriteLine("Superimposed load = {0} * 1 = {1} kN/m.", ll.ToString("0.00"), ll.ToString("0.00"));
                double totalLoad = deadLoad_slab + ll;
                sw.WriteLine("Total Load  = {0} kN/m.",totalLoad.ToString("0.00"));

                double factoredLoad, loadFactor;
                loadFactor = 1.5d;
                factoredLoad = totalLoad * loadFactor;
                sw.WriteLine("Factored load if the load factor is {0}", loadFactor.ToString("0.00"));
                sw.WriteLine("                               = {0} * {1} = {2} kN/m", loadFactor.ToString("0.00"), totalLoad.ToString("0.00"), factoredLoad.ToString("0.00"));
                sw.WriteLine("Maximum BM at center of shorter span");
                sw.WriteLine("                       = (Wu * l * l) / 8");
                sw.WriteLine("Assume steel consists of {0} mm bars with {1} mm clear cover.", d1, h1);

                double half_depth = d1 / 2;
                double eff_depth = D - h1 - half_depth;
                sw.WriteLine("Effective depth = {0} - {1} - {2} = {3} mm", D.ToString("0.0"), h1.ToString("0.0"), half_depth.ToString("0.00"), eff_depth.ToString("0.00"));

                lowest_Span = lowest_Span / 1000;
                
                double eff_span = (lowest_Span) + (eff_depth/1000);
                sw.WriteLine("Effective Span of Slab = {0} + d = {0} + {1} = {2} m", (lowest_Span).ToString("0.00"), (eff_depth / 1000).ToString("0.00"), eff_span.ToString("0.00"));
                double BM = factoredLoad * eff_span * eff_span / 8;
                    //sw.WriteLine("So, BM = ({0} * {1} * {1}) / 8 = {2} kNm", factoredLoad.ToString("0.00"), eff_span.ToString("0.00"), BM.ToString("0.00"));
                sw.WriteLine("So, BM = M = ({0} * {1} * {1}) / 8 = {2} kNm", factoredLoad.ToString("0.00"), eff_span.ToString("0.00"), BM.ToString("0.00"));
                sw.WriteLine("Max shear force = Vu = (Wn * lc) / 2");
                double max_shear_force = (factoredLoad*lowest_Span/2.0);
                sw.WriteLine("                = {0} * {1}/2 = {2} kN = {3} N", factoredLoad.ToString("0.00"), lowest_Span.ToString("0.0"), max_shear_force.ToString("0.0"), (max_shear_force*1000).ToString("0.00"));

                max_shear_force *= 1000;
                
                
                sw.WriteLine("Depth of the slab is given by");
                sw.WriteLine("              BM = 0.138 * σ_ck * b * d* d");

                double M = (BM * 10E+5);
                d = ((BM * 10E+5) / (0.138 * sigma_ck * 1000));
                d = Math.Sqrt(d);
                sw.WriteLine("or      d = √(({0} * 10E+5)/(0.138 * {1} * 1000)) = {2} mm", BM.ToString("0.00"), sigma_ck.ToString("0.00"), d.ToString("0.00"));

                d = (int)(d / 10);
                d += 5;
                d *= 10;
                sw.WriteLine("Adopt effective depth d = {0} mm and over all depth", d.ToString("0.0"));
                sw.WriteLine("                      D = {0} mm", eff_depth);


                sw.WriteLine("Adopt of tension steel is given by ");
                sw.WriteLine("             M = 0.87 * σ_y * A_t( d - ((σ_y * A_t)/(σ_ck * b))");

                double a, b, c,At;

                a = (0.87 * sigma_y * sigma_y) / (sigma_ck * 1000);
                b = 0.87 * sigma_y * d;
                c = M;
                double b_ac = (b * b) - 4 * a * c;
                At = (b) - Math.Sqrt(Math.Abs(b_ac));
                At = At / (2 * a);

                At = (int)At / 10;
                At += 1;
                At = At * 10;
                sw.WriteLine("   {0} * 10E+5 = 0.87 * {1} * At * ({2} - {3} * At / ({4} * 1000))",
                    BM.ToString("0.00"),sigma_y.ToString("0.00"),d.ToString("0.0"),sigma_y.ToString("0.00"),sigma_ck.ToString("0.00"));
                sw.WriteLine("or                     At = {0} sq.mm", At);
                sw.WriteLine("Use {0} mm bars @ {1} mm c/c giving total area ", d1, d.ToString("0.0"));

                double est_value = (Math.PI * (d1 * d1) / 4) * (1000 / d);
                sw.WriteLine("                         = {0} sq.mm. > {1} sq.mm      OK", est_value.ToString("0"), At.ToString("0"));
                double n_rod = At / ((Math.PI * (d1 * d1) / 4));

                n_rod = (int) n_rod;
                n_rod += 1;

                sw.WriteLine("  Bend alternate bars at L/{0} from the face of support where moment reduces to less",n_rod);
                sw.WriteLine("than half its maximum value. Temperature reinforcement equal to {0}% of the",ads);
                sw.WriteLine("gross concrete area will be provided in the longitudinal direction.");
                sw.WriteLine("gross concrete area will be provided in the longitudinal direction.");

                double dirArea = (ads / 100) * 1000 * eff_depth;
                sw.WriteLine("       = {0} * 1000 * {1} = {2} sq.mm.", (Ads / 100).ToString("0.0000"), eff_depth.ToString("0.00"), dirArea.ToString("0.00"));
                sw.WriteLine("Use {0} mm MS bars @ 100 mm c/c giving total area ",d2.ToString("0"));

                double a_st = Math.PI * d2 * d2 / 4;

                sw.WriteLine("   = {0} * (1000/100) = {1} sq.mm.  > {2} sq.mm            OK", a_st.ToString("0.00"), (a_st * 10).ToString("0.00"),dirArea.ToString("0.0"));
                sw.WriteLine();
                sw.WriteLine("----------------------------------------------------------------------------------------------");
                sw.WriteLine("■ STEP 3:");
                sw.WriteLine("Check for Shear");
                sw.WriteLine("----------------------------------------------------------------------------------------------");
                sw.WriteLine(); 
                sw.WriteLine("Percent tension steel = (100 * At)/ (b * d)");
                a_st = Math.PI * d1 * d1 / 4;



                double percent = (100 * (a_st * (1000 / 300))) / (1000 * d);
                sw.WriteLine("                      = (100 * ({0} * (1000/300)) / (1000 * {1}) = {2}%", a_st.ToString("0.0"), d.ToString("0.0"), percent.ToString("0.00"));
                sw.WriteLine("Shear strength of concrete for {0}% steel",percent.ToString("0.00"));
                ShearValue sh = new ShearValue();
                double tau_c = 0.0;

                tau_c = sh.Get_M15(percent);

                sw.WriteLine("     τ_c = {0} N/sq.mm.", tau_c);
                
                
                double k = kValue.Get_KValue(eff_depth);
                double tc_dash = k * tau_c;




              
                //tau_c = tau_c;
                sw.WriteLine("For {0} mm thick slab, k = {1}", eff_depth.ToString("0.00"), k.ToString("0.00"));
                sw.WriteLine("         So, τ_c` = k * Tc = {0} * {1} = {2} N/sq.mm", k, tau_c.ToString("0.00"), tc_dash.ToString("0.00"));
                double Vu = max_shear_force;
                double t_v = Vu / (1000 * d);

                sw.WriteLine("Nominal shear stress Tv = Vu / b * d = {0}/(1000 * {1}) = {2} N/sq.mm", Vu, d, t_v.ToString("0.00"));

                sw.WriteLine("The Slab is safe in shear.");
                sw.WriteLine();
                sw.WriteLine("----------------------------------------------------------------------------------------------");
                sw.WriteLine("■ STEP 4:");
                sw.WriteLine("Check for development length");
                sw.WriteLine("----------------------------------------------------------------------------------------------");
                sw.WriteLine();
                sw.WriteLine("Moment of resistance offered by {0} mm bars @ 300 mm c/c", d1);
                sw.WriteLine("M1 = 0.87 * σ_y * At * (d - (σ_y * At / σ_ck * b))");
                sw.WriteLine("   = 0.87 * {0} * {1} * (1000/300) * ({2} - ({3} * {4} * (1000 / 300)) / {5} * 1000)",
                    sigma_y.ToString("0.00"), a_st.ToString("0.00"), d.ToString("0.00"), sigma_y.ToString("0.00"), a_st.ToString("0.00"), Sigma_ck.ToString("0.00"));

                double M1 = 0.87 * sigma_y * a_st * (1000.0 / 300.0) * (d - (sigma_y * a_st * (1000.0d / 300.0d) / (sigma_ck * 1000.0)));





                sw.WriteLine("M1 = {0} Nmm", M1.ToString("0.00"));
                sw.WriteLine("Vu  = {0} N", Vu);
                sw.WriteLine("Let us assume anchorage length Lo = 0");
                sw.WriteLine("                  Ld <= 1.3 * (M1/Vu)");
                sw.WriteLine("                  56φ <= 1.3 * ({0}/{1})", M1.ToString("0.00"), Vu.ToString("0.00"));

                double phi = (1.3 * (M1 / Vu) / 56.0);
                sw.WriteLine("                  φ < {0}", phi.ToString("0.00"));
                sw.WriteLine("We have provided φ = {0} mm, So OK.",d1.ToString("0.00"));


                sw.WriteLine("The Code requires that bars must be carried into the supports by atleast Ld / 3 = 190 mm");
                sw.WriteLine("Step 5");
                sw.WriteLine("Check for deflection");
                sw.WriteLine("Percent tension steel at midspan");
                sw.WriteLine("               = (100 * As) / (b * d)");
                sw.WriteLine("               = (100 * {0} * 1000 / 150) / (1000 * 150)");
                double pps = (100 * 78.5 * 1000 / 150) / (1000 * 150);
                sw.WriteLine("               = {0}%", pps.ToString("0.00"));

                double gama = ModificationFactor.GetGamma(pps);
                sw.WriteLine("             γ = {0}%", gama.ToString("0.00"));
                //sw.WriteLine(" σσσγγβαδλ■  √");
                sw.WriteLine("  β = {0}, δ = {1} and λ = {2}", beta, delta, lamda);

                // Constant 20
                sw.WriteLine("Allowable L/d = 20 * {0} = {1}",gama.ToString("0.00"),(20*gama).ToString("0.00"));

                sw.WriteLine("Actual L/d = {0} / {1} = {2} < {3}       OK", (eff_span*1000).ToString("0.00"), d, (eff_span*1000 / d).ToString("0.00"), (20 * gama).ToString("0.00"));

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("************************************************************************");
                sw.WriteLine("***************************    END OF REPORT    ************************");
                sw.WriteLine("************************************************************************");
            }
            catch (Exception exx)
            {
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
        public void Slab_Drawing(vdDocument doc,string txtValueFile)
        {
            
            //double total_length;
            //double d1_space_dist;
            //double d2_space_dist;
            List<string> lstStr = new List<string>(File.ReadAllLines(txtValueFile));

            doc.Palette.Background = Color.White;

            #region Lines
            vdLine ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(1947.9958,3174.2928,0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(3724.6751, 3174.2928, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);

            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(1947.9958, 3152.4260, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(3724.6751, 3152.4260, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);

            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(3724.8270, 3211.7398, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(3724.8270, 3169.8891, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);

            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(3724.8270, 3169.8891, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(3712.5876, 3161.2031, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);

            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(3712.5876, 3161.2031, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(3734.6974, 3161.2031, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);

            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(3734.6974, 3161.2031, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(3724.8270, 3156.4653, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);

            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(3724.8270, 3156.4653, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(3724.8270, 3118.1680, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);



            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(3768.2569, 3213.3190, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(3768.2569, 3171.4684, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);





            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(3768.2569, 3171.4684, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(3756.0176, 3162.7824, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);





            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(3756.0176, 3162.7824, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(3778.1273, 3162.7824, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);





            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(3778.1273, 3162.7824, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(3768.2569, 3158.0446, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);





            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(3768.2569, 3158.0446, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(3768.2569, 3119.7473, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);





            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(3768.4088, 3174.2928, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(5627.0886, 3174.2928, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);





            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(3768.4088, 3152.4260, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(5627.0886, 3152.4260, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);





            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(2325.6507, 3146.3981, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(2325.6507, 1046.3981, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);





            //ln = new vdLine();
            //ln.SetUnRegisterDocument(doc);
            //ln.setDocumentDefaults();
            //ln.StartPoint = new VectorDraw.Geometry.gPoint(2439.9993, 3136.0259, 0.0000);
            //ln.EndPoint = new VectorDraw.Geometry.gPoint(2439.9993, 916.5434, 0.0000);
            //doc.ActiveLayOut.Entities.AddItem(ln);





            //ln = new vdLine();
            //ln.SetUnRegisterDocument(doc);
            //ln.setDocumentDefaults();
            //ln.StartPoint = new VectorDraw.Geometry.gPoint(2439.9993, 3136.0259, 0.0000);
            //ln.EndPoint = new VectorDraw.Geometry.gPoint(2439.9993, 916.5434, 0.0000);
            //doc.ActiveLayOut.Entities.AddItem(ln);




            //2632.0683,1171

            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(2442.3174, 3146.3981, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(2442.3174, 1146.3981, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);



            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(2509.0583, 1185.1093, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(2647.9472, 1185.1093, 0.0000);
            ln.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(ln);





            //ln = new vdLine();
            //ln.SetUnRegisterDocument(doc);
            //ln.setDocumentDefaults();
            //ln.StartPoint = new VectorDraw.Geometry.gPoint(2632.0683, 3120.7911, 0.0000);
            //ln.EndPoint = new VectorDraw.Geometry.gPoint(2632.0683, 1171.7171, 0.0000);
            //doc.ActiveLayOut.Entities.AddItem(ln);

            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(2565.7335, 3136.0259, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(2565.7335, 916.5434, 0.0000);
            ln.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(ln);


            

            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(2724.2680, 2720.5563, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(2357.9987, 2720.5563, 0.0000);
            ln.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(ln);




            //ln = new vdLine();
            //ln.SetUnRegisterDocument(doc);
            //ln.setDocumentDefaults();
            //ln.StartPoint = new VectorDraw.Geometry.gPoint(2461.3915, 2548.3042, 0.0000);
            //ln.EndPoint = new VectorDraw.Geometry.gPoint(5774.1558, 2548.3042, 0.0000);
            //doc.ActiveLayOut.Entities.AddItem(ln);




            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(2364.5396, 2368.6203, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(5208.9841, 2368.6203, 0.0000);
            ln.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(ln);




            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(2461.3915, 1815.7656, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(5774.1558, 1815.7656, 0.0000);
            ln.PenColor = new vdColor(Color.DarkGreen);
            doc.ActiveLayOut.Entities.AddItem(ln);



            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(2565.7335, 916.5434, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(3057.7370, 916.5434, 0.0000);
            ln.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(ln);



            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(2461.3915, 1846.0846, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(2461.3915, 1789.7780, 0.0000);
            ln.PenColor = new vdColor(Color.Green);
            doc.ActiveLayOut.Entities.AddItem(ln);




            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(2441.3309, 1523.3477, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(5131.1698, 1523.3209, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);




            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(5130.5029, 1523.5966, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(5026.4252, 1537.7306, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);




            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(5026.4252, 1537.7306, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(5027.0677, 1508.8202, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);




            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(5027.0677, 1508.8202, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(5130.5029, 1523.5966, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);




            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(2440.0460, 1524.4574, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(2543.4812, 1539.2339, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);



            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(2440.0460, 1524.4574, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(2544.1237, 1510.3234, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);

            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(2543.4812, 1539.2339, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(2544.1237, 1510.3234, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);


            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(2543.4812, 1539.2339, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(2440.0460, 1524.4574, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);




            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(3386.4737, 3120.7911, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(3386.4737, 1171.7171, 0.0000);
            ln.PenColor = new vdColor(Color.DarkGreen);
            doc.ActiveLayOut.Entities.AddItem(ln);




            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(4046.3576, 3118.6203, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(4046.3576, 907.5092, 0.0000);
            ln.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(ln);




            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(3974.1354, 1174.1759, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(4113.0243, 1174.1759, 0.0000);
            ln.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(ln);




            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(4046.3576, 907.5092, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(4444.4751, 907.5092, 0.0000);
            ln.PenColor = new vdColor(Color.Red);
            doc.ActiveLayOut.Entities.AddItem(ln);




            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(2442.3174, 1146.3981, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(5131.2063, 1146.3981, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);




            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(5247.8730, 1046.3981, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(2325.6507, 1046.3981, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);




            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(4932.8170, 3125.0925, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(4932.8170, 905.6100, 0.0000);
            ln.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(ln);




            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(5217.0857, 2709.6229, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(4850.8165, 2709.6229, 0.0000);
            ln.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(ln);




            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(4876.1418, 1174.1759, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(5015.0307, 1174.1759, 0.0000);
            ln.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(ln);




            //ln = new vdLine();
            //ln.SetUnRegisterDocument(doc);
            //ln.setDocumentDefaults();
            //ln.StartPoint = new VectorDraw.Geometry.gPoint(5048.3522, 1171.7171, 0.0000);
            //ln.EndPoint = new VectorDraw.Geometry.gPoint(5048.3522, 3120.7911, 0.0000);
            //doc.ActiveLayOut.Entities.AddItem(ln);




            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(5131.2063, 1146.3981, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(5131.2063, 3146.3981, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);




            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(5247.8730, 1046.3981, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(5247.8730, 3146.3981, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);




            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(4932.8170, 905.6100, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(5424.8205, 905.6100, 0.0000);
            ln.PenColor = new vdColor(Color.Blue);
            doc.ActiveLayOut.Entities.AddItem(ln);




            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new VectorDraw.Geometry.gPoint(5112.1322, 1846.0846, 0.0000);
            ln.EndPoint = new VectorDraw.Geometry.gPoint(5112.1322, 1789.7780, 0.0000);
            ln.PenColor = new vdColor(Color.Green);
            doc.ActiveLayOut.Entities.AddItem(ln);

            #endregion

            #region Circles
            vdCircle cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new VectorDraw.Geometry.gPoint(2565.6666, 2720.4304, 0.0000);
            cir.PenColor = new vdColor(Color.Blue);
            cir.Radius = 20.4470;

            doc.ActiveLayOut.Entities.AddItem(cir);

            //cir = new vdCircle();
            //cir.SetUnRegisterDocument(doc);
            //cir.setDocumentDefaults();
            //cir.Center = new VectorDraw.Geometry.gPoint(2631.3868,2548.4492,0.0000);
            //cir.Radius = 20.4470;
            //doc.ActiveLayOut.Entities.AddItem(cir);

            
            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new VectorDraw.Geometry.gPoint(3386.4580,1816.1968,0.0000);
            cir.PenColor = new vdColor(Color.Green);
            cir.Radius = 20.4470;

            doc.ActiveLayOut.Entities.AddItem(cir);

            
            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new VectorDraw.Geometry.gPoint(4046.3576,2368.6203,0.0000);
            cir.PenColor = new vdColor(Color.Red);
            cir.Radius = 20.4470;

            doc.ActiveLayOut.Entities.AddItem(cir);

            
            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new VectorDraw.Geometry.gPoint(4931.4906,2709.3588,0.0000);
            cir.PenColor = new vdColor(Color.Blue);
            cir.Radius = 20.4470;

            doc.ActiveLayOut.Entities.AddItem(cir);

            
            //cir = new vdCircle();
            //cir.SetUnRegisterDocument(doc);
            //cir.setDocumentDefaults();
            //cir.Center = new VectorDraw.Geometry.gPoint(5047.1744,2548.1977,0.0000);
            //cir.Radius = 20.4470;

            //doc.ActiveLayOut.Entities.AddItem(cir);

            #endregion

            #region Text
            vdText vtxt = new vdText();
            vtxt.SetUnRegisterDocument(doc);
            vtxt.setDocumentDefaults();
            vtxt.InsertionPoint = new VectorDraw.Geometry.gPoint(2721.1190, 938.6541, 0.0000);
            vtxt.TextString = lstStr[0];
            //vtxt.TextString = "35-T10-03-170-T1";
            vtxt.Height =39.990d;

            doc.ActiveLayOut.Entities.AddItem(vtxt);

            vtxt = new vdText();
            vtxt.SetUnRegisterDocument(doc);
            vtxt.setDocumentDefaults();
            vtxt.InsertionPoint = new VectorDraw.Geometry.gPoint(3459.5399, 1552.0480, 0.0000);
            //vtxt.TextString = "SPAN     3.5 M.";
            vtxt.TextString = lstStr[1];
            vtxt.Height =39.990d;
            doc.ActiveLayOut.Entities.AddItem(vtxt);

            vtxt = new vdText();
            vtxt.SetUnRegisterDocument(doc);
            vtxt.setDocumentDefaults();
            vtxt.InsertionPoint = new VectorDraw.Geometry.gPoint(4109.6622, 927.7206, 0.0000);
            vtxt.TextString = lstStr[2];
            //vtxt.TextString = "35-T10-01-170-B1";
            vtxt.Height =39.990d;
            doc.ActiveLayOut.Entities.AddItem(vtxt);

            //vtxt = new vdText();
            //vtxt.SetUnRegisterDocument(doc);
            //vtxt.setDocumentDefaults();
            //vtxt.InsertionPoint = new VectorDraw.Geometry.gPoint(5434.0767, 2578.6231, 0.0000);
            //vtxt.Height =39.990d;
            //vtxt.TextString = lstStr[3];
            //vtxt.TextString = "8-T6-04-220-T2";

            //doc.ActiveLayOut.Entities.AddItem(vtxt);

            vtxt = new vdText();
            vtxt.SetUnRegisterDocument(doc);
            vtxt.setDocumentDefaults();
            vtxt.InsertionPoint = new VectorDraw.Geometry.gPoint(5434.0767, 1846.0846, 0.0000);
            vtxt.TextString = lstStr[4];
            //vtxt.TextString = "20-T6-02-180-B2";
            vtxt.Height =39.990d;
            doc.ActiveLayOut.Entities.AddItem(vtxt);


            vtxt = new vdText();
            vtxt.SetUnRegisterDocument(doc);
            vtxt.setDocumentDefaults();
            vtxt.InsertionPoint = new VectorDraw.Geometry.gPoint(5088.2025, 927.7206, 0.0000);
            //vtxt.TextString = "35-T10-03-170-T1";
            vtxt.TextString = lstStr[0];
            vtxt.Height =39.990d;
            doc.ActiveLayOut.Entities.AddItem(vtxt);
            #endregion

            doc.ShowUCSAxis = false;
            doc.Redraw(true);
            Slab_Drawing_Plan(doc, txtValueFile);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);
        }
        public void SLAB02_View(vdDocument document, string design_file)
        {
            // A. K. Jain
            // Reinforce concrete LIMIT STATE DESIGN : 3rd Edition
            // Page - 356
         


            double total_length = 3500;
            double eff_length = 0.0;
            double b1, b2, h1, d1, d2, s1, s2, w1, w2;
            
            double width;
            double total_depth, eff_depth;
            d1 = 10.0; // 
            d2 = 6.0;
            h1 = 15;
            h2 = 5;
            b1 = 150 * 2;
            b2 = 100;
            total_depth = 170;
            double dep_center = 150;
            double fd = 500;

            //fd = 500;
            w1 = 250.0d;
            w2 = 250.0d;

           

            List<string> lstStr = new List<string>(File.ReadAllLines(design_file));
            MyStrings mList = null;

            for (int i = 0; i < lstStr.Count; i++)
            {

                //SLAB DESIGN 02
                //L = 3500
                //D = 170
                //b1 = 150
                //b2 = 100
                //h1 = 15
                //h2 = 15
                //d1 = 10
                //d2 = 6
                //fd = 500
                //w1 = 250
                //w2 = 250
                //END
                mList = new MyStrings(MyStrings.RemoveAllSpaces(lstStr[i].Trim().TrimEnd().TrimStart().ToUpper()), '=');
                if (mList.StringList[0] == "L")
                    total_length = mList.GetDouble(1);
                else if (mList.StringList[0].Trim() == "D")
                    total_depth = mList.GetDouble(1);
                else if (mList.StringList[0].Trim() == "B1")
                    b1 = mList.GetDouble(1);
                else if (mList.StringList[0].Trim() == "B2")
                    b2 = mList.GetDouble(1);
                else if (mList.StringList[0].Trim() == "H1")
                    h1 = mList.GetDouble(1);
                else if (mList.StringList[0].Trim() == "H2")
                    h2 = mList.GetDouble(1);
                else if (mList.StringList[0].Trim() == "D1")
                    d1 = mList.GetDouble(1);
                else if (mList.StringList[0].Trim() == "D2")
                    d2 = mList.GetDouble(1);
                else if (mList.StringList[0].Trim() == "FD")
                    fd = mList.GetDouble(1);
                else if (mList.StringList[0].Trim() == "W1")
                    w1 = mList.GetDouble(1);
                else if (mList.StringList[0].Trim() == "W2")
                    w2 = mList.GetDouble(1);
            }

           

            //total_length = 3500 + 170;
           
            int MainRein = (int)(1000 / (b1));
            MainRein++;
            eff_length = total_length + w1 + w2;
            int DistRein = (int)(eff_length / b2);

            #region Dist Reinforcement
            vdCircle cir = new vdCircle();

            for (int i = 1; i <= DistRein; i++)
            {
                cir = new vdCircle();
                cir.SetUnRegisterDocument(document);
                cir.setDocumentDefaults();
                cir.Center = new gPoint((i * b2) + h1,(d1/2 + d2/2), 0);
                cir.ExtrusionVector = Vector.CreateExtrusion(cir.Center, new gPoint(cir.Center.x, cir.Center.y, 1000));
                cir.Thickness = cir.Center.Distance3D(new gPoint(cir.Center.x, cir.Center.y, 1000));
                cir.Radius = (d2 / 2);
                cir.PenColor = new vdColor(Color.DarkGreen);
                document.ActiveLayOut.Entities.AddItem(cir);
            }
            #endregion

            #region Main Reinforcement



           
            vdPolyline pLine = null;
            eff_length = total_length + w1 + w2;
            double k_d = total_depth - (2 * ((h1 + (d1 / 2))));
            double c_length = w1 + fd;
            double _b1 = 1000;

            for (int i = 0; i < MainRein; i++)
            {
                pLine = new vdPolyline();
                pLine.SetUnRegisterDocument(document);
                pLine.setDocumentDefaults();
                pLine.VertexList.Add(new gPoint(0 + h1, k_d, _b1));
                pLine.VertexList.Add(new gPoint(c_length, k_d, _b1));
                pLine.VertexList.Add(new gPoint(c_length + k_d, 0, _b1));
                pLine.VertexList.Add(new gPoint(eff_length - h1, 0, _b1));
                document.ActiveLayOut.Entities.AddItem(pLine);
                #region Draw Circle
                cir = new vdCircle();
                cir.SetUnRegisterDocument(document);
                cir.setDocumentDefaults();
                cir.Center = pLine.VertexList[0];
                cir.ExtrusionVector = Vector.CreateExtrusion(cir.Center, pLine.VertexList[1]);
                cir.Thickness = cir.Center.Distance3D(pLine.VertexList[1]);
                cir.Radius = d1 / 2;
                cir.PenColor = new vdColor(Color.Red);
                document.ActiveLayOut.Entities.AddItem(cir);

                cir = new vdCircle();
                cir.SetUnRegisterDocument(document);
                cir.setDocumentDefaults();
                cir.Center = pLine.VertexList[1];
                cir.ExtrusionVector = Vector.CreateExtrusion(cir.Center, pLine.VertexList[2]);
                cir.Thickness = cir.Center.Distance3D(pLine.VertexList[2]);
                cir.Radius = d1 / 2;
                cir.PenColor = new vdColor(Color.Red);
                document.ActiveLayOut.Entities.AddItem(cir);


                cir = new vdCircle();
                cir.SetUnRegisterDocument(document);
                cir.setDocumentDefaults();
                cir.Center = pLine.VertexList[2];
                cir.ExtrusionVector = Vector.CreateExtrusion(cir.Center, pLine.VertexList[3]);
                cir.Thickness = cir.Center.Distance3D(pLine.VertexList[3]);
                cir.Radius = d1 / 2;
                cir.PenColor = new vdColor(Color.Red);
                document.ActiveLayOut.Entities.AddItem(cir);
                #endregion

                _b1 -= (b1);
            }

            c_length = eff_length - (w2 + fd);
            //c_length = 3500 + 250 + 250 - 500 - 250;
            _b1 = 1000 - (b1 / 2);
            for (int i = 0; i < MainRein; i++)
            {
                pLine = new vdPolyline();
                pLine.SetUnRegisterDocument(document);
                pLine.setDocumentDefaults();
                pLine.VertexList.Add(new gPoint(0 + h1, 0, _b1));
                pLine.VertexList.Add(new gPoint(c_length, 0, _b1 ));
                pLine.VertexList.Add(new gPoint(c_length + k_d, k_d, _b1));
                pLine.VertexList.Add(new gPoint(eff_length - h1, k_d, _b1));
                
                //pLine.VertexList.Add(new gPoint(0 + h1, 0, (double)(i * b1)));
                //pLine.VertexList.Add(new gPoint(c_length, 0, (double)(i * b1)));
                //pLine.VertexList.Add(new gPoint(c_length + k_d, k_d, (double)(i * b1)));
                //pLine.VertexList.Add(new gPoint(eff_length - h1, k_d, (double)(i * b1)));
                #region Draw Circle
                cir = new vdCircle();
                cir.SetUnRegisterDocument(document);
                cir.setDocumentDefaults();
                cir.Center = pLine.VertexList[0];
                cir.ExtrusionVector = Vector.CreateExtrusion(cir.Center, pLine.VertexList[1]);
                cir.Thickness = cir.Center.Distance3D(pLine.VertexList[1]);
                cir.Radius = d1 / 2;
                cir.PenColor = new vdColor(Color.Red);
                document.ActiveLayOut.Entities.AddItem(cir);

                cir = new vdCircle();
                cir.SetUnRegisterDocument(document);
                cir.setDocumentDefaults();
                cir.Center = pLine.VertexList[1];
                cir.ExtrusionVector = Vector.CreateExtrusion(cir.Center, pLine.VertexList[2]);
                cir.Thickness = cir.Center.Distance3D(pLine.VertexList[2]);
                cir.Radius = d1 / 2;
                cir.PenColor = new vdColor(Color.Red);
                document.ActiveLayOut.Entities.AddItem(cir);


                cir = new vdCircle();
                cir.SetUnRegisterDocument(document);
                cir.setDocumentDefaults();
                cir.Center = pLine.VertexList[2];
                cir.ExtrusionVector = Vector.CreateExtrusion(cir.Center, pLine.VertexList[3]);
                cir.Thickness = cir.Center.Distance3D(pLine.VertexList[3]);
                cir.Radius = d1 / 2;
                cir.PenColor = new vdColor(Color.Red);
                document.ActiveLayOut.Entities.AddItem(cir);
                #endregion
                _b1 -= (b1);
                document.ActiveLayOut.Entities.AddItem(pLine);
            }

            //k_d = 130;

            //for (int i = 1; i <= 10; i++)
            //{
            //    pLine = new vdPolyline();
            //    pLine.SetUnRegisterDocument(document);
            //    pLine.setDocumentDefaults();
            //    pLine.VertexList.Add(new gPoint(0, 130, (double)(i * b1) + (b1 / 2)));
            //    pLine.VertexList.Add(new gPoint(455, k_d, (double)(i * b1) + (b1 / 2)));
            //    pLine.VertexList.Add(new gPoint(455 + k_d, 0, (double)(i * b1) + (b1 / 2)));
            //    pLine.VertexList.Add(new gPoint(3670, 0, (double)i * b1 + (b1 / 2)));
            //    document.ActiveLayOut.Entities.AddItem(pLine);

            //}

            //for (int i = 1; i <= 10; i++)
            //{
            //    pLine = new vdPolyline();
            //    pLine.SetUnRegisterDocument(document);
            //    pLine.setDocumentDefaults();
            //    pLine.VertexList.Add(new gPoint(0, 0, (double)(i * b1)));
            //    pLine.VertexList.Add(new gPoint(3040, 0, (double)(i * b1)));
            //    pLine.VertexList.Add(new gPoint(3170, k_d, (double)(i * b1)));
            //    pLine.VertexList.Add(new gPoint(total_length, k_d, (double)(i * b1)));
            //    document.ActiveLayOut.Entities.AddItem(pLine);
            //}


            vdPolyline pLine1 = new vdPolyline();
            pLine1.SetUnRegisterDocument(document);
            pLine1.setDocumentDefaults();
            pLine1.VertexList.Add(new gPoint(0-h1, 0-h1, 0));
            pLine1.VertexList.Add(new gPoint(0 - h1, 0-h1, 1000));
            pLine1.VertexList.Add(new gPoint(eff_length + h1, 0-h1, 1000));
            pLine1.VertexList.Add(new gPoint(eff_length + h1, 0-h1, 0));
            pLine1.VertexList.Add(pLine1.VertexList[0]);
            document.ActiveLayOut.Entities.AddItem(pLine1);

            vdPolyline pLine2 = new vdPolyline();
            pLine2.SetUnRegisterDocument(document);
            pLine2.setDocumentDefaults();
            pLine2.VertexList.Add(new gPoint(0-h1, total_depth, 0));
            pLine2.VertexList.Add(new gPoint(0-h1, total_depth, 1000));
            pLine2.VertexList.Add(new gPoint(eff_length + h1, total_depth, 1000));
            pLine2.VertexList.Add(new gPoint(eff_length + h1, total_depth, 0));
            pLine2.VertexList.Add(pLine2.VertexList[0]);

            document.ActiveLayOut.Entities.AddItem(pLine2);

            for (int i = 0; i < pLine1.VertexList.Count; i++)
            {
                vdLine ln = new vdLine();
                ln.SetUnRegisterDocument(document);
                ln.setDocumentDefaults();
                ln.StartPoint = pLine1.VertexList[i];
                ln.EndPoint = pLine2.VertexList[i];
                document.ActiveLayOut.Entities.AddItem(ln);
            }
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(document);

            #endregion


            #region Write File
            string drawing_file = Path.Combine(Path.GetDirectoryName(design_file), "DRAWING.FIL");
            StreamWriter sww = new StreamWriter(new FileStream(drawing_file, FileMode.Create));
            string kStr = "";
            try
            {
               
                 //L = 3500
                //D = 170
                //b1 = 150
                //b2 = 100
                //h1 = 15
                //h2 = 15
                //d1 = 10
                //d2 = 6
                //fd = 500
                //w1 = 250
                //w2 = 250
                //35-T10-03-170-T1
                //SPAN 3.5 M
                //35-T10-01-170-B1
                //35-T10-03-170-T1
                //20-T6-02-180-B2


                kStr = MainRein + "-T" + d1 + "-03-" + b1 + "-T1";
                sww.WriteLine(kStr);

                kStr = "SPAN " + (total_length / 1000).ToString("0.00") + " M";
                sww.WriteLine(kStr);

                kStr = MainRein + "-T" + d1 + "-01-" + b1 + "-B1";
                sww.WriteLine(kStr);
                kStr = MainRein + "-T" + d1 + "-03-" + b1 + "-T1";
                sww.WriteLine(kStr);

                kStr = DistRein + "-T" + d2 + "-02-" + b2 + "-B2";
                sww.WriteLine(kStr);
            }
            catch (Exception exx)
            {
            }
            finally
            {
                sww.Flush();
                sww.Close();
            }
            #endregion

        }
        public void Slab_Drawing_Plan(vdDocument doc, string txtValueFile)
        {
            //SLAB DESIGN 02
            //L = 3500
            //D = 170
            //b1 = 150
            //b2 = 100
            //h1 = 15
            //h2 = 15
            //d1 = 10
            //d2 = 6
            //fd = 500
            //w1 = 250
            //w2 = 250
            //END

            #region Rectangles
            vdRect rec = new vdRect();
            rec.SetUnRegisterDocument(doc);
            rec.setDocumentDefaults();
            rec.InsertionPoint = new gPoint(2337.5579, 831.1796, 0.0000);
            rec.Width = 2908.7456;
            rec.Height = -418.3146;
            doc.ActiveLayOut.Entities.AddItem(rec);

            rec = new vdRect();
            rec.SetUnRegisterDocument(doc);
            rec.setDocumentDefaults();
            rec.InsertionPoint = new gPoint(2441.5908, 410.3683, 0.0000);
            rec.Width = -104.2120;
            rec.Height = -114.6333;
            doc.ActiveLayOut.Entities.AddItem(rec);

            rec = new vdRect();
            rec.SetUnRegisterDocument(doc);
            rec.setDocumentDefaults();
            rec.InsertionPoint = new gPoint(5245.4504, 413.8435, 0.0000);
            rec.Width = -119.8628;
            rec.Height = -119.0810;
            doc.ActiveLayOut.Entities.AddItem(rec);
            #endregion

            #region Circles
            vdCircle cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(2463.4921, 502.6282, 0.0000);
            cir.Radius = 24.7370;
            doc.ActiveLayOut.Entities.AddItem(cir);

            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(2571.5392, 502.6282, 0.0000);
            cir.Radius = 24.7370;
            doc.ActiveLayOut.Entities.AddItem(cir);

            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(2673.3636, 502.6282, 0.0000);
            cir.Radius = 24.7370;
            doc.ActiveLayOut.Entities.AddItem(cir);

            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(2781.4107, 502.6282, 0.0000);
            cir.Radius = 24.7370;
            doc.ActiveLayOut.Entities.AddItem(cir);

            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(2880.3156, 502.6282, 0.0000);
            cir.Radius = 24.7370;
            doc.ActiveLayOut.Entities.AddItem(cir);

            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(2989.3770, 502.6282, 0.0000);
            cir.Radius = 24.7370;
            doc.ActiveLayOut.Entities.AddItem(cir);

            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(3097.4241, 502.6282, 0.0000);
            cir.Radius = 24.7370;
            doc.ActiveLayOut.Entities.AddItem(cir);

            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(3199.2485, 502.6282, 0.0000);
            cir.Radius = 24.7370;
            doc.ActiveLayOut.Entities.AddItem(cir);

            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(3307.2956, 502.6282, 0.0000);
            cir.Radius = 24.7370;
            doc.ActiveLayOut.Entities.AddItem(cir);

            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(3406.2005, 502.6282, 0.0000);
            cir.Radius = 24.7370;
            doc.ActiveLayOut.Entities.AddItem(cir);

            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(3510.4373, 502.6282, 0.0000);
            cir.Radius = 24.7370;
            doc.ActiveLayOut.Entities.AddItem(cir);

            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(3618.4843, 502.6282, 0.0000);
            cir.Radius = 24.7370;
            doc.ActiveLayOut.Entities.AddItem(cir);

            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(3720.3087, 502.6282, 0.0000);
            cir.Radius = 24.7370;
            doc.ActiveLayOut.Entities.AddItem(cir);

            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(3828.3558, 502.6282, 0.0000);
            cir.Radius = 24.7370;
            doc.ActiveLayOut.Entities.AddItem(cir);

            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(3927.2608, 502.6282, 0.0000);
            cir.Radius = 24.7370;
            doc.ActiveLayOut.Entities.AddItem(cir);

            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(4021.0763, 502.6282, 0.0000);
            cir.Radius = 24.7370;
            doc.ActiveLayOut.Entities.AddItem(cir);

            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(4129.1234, 502.6282, 0.0000);
            cir.Radius = 24.7370;
            doc.ActiveLayOut.Entities.AddItem(cir);

            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(4230.9478, 502.6282, 0.0000);
            cir.Radius = 24.7370;
            doc.ActiveLayOut.Entities.AddItem(cir);

            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(4338.9948, 502.6282, 0.0000);
            cir.Radius = 24.7370;
            doc.ActiveLayOut.Entities.AddItem(cir);

            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(4437.8998, 502.6282, 0.0000);
            cir.Radius = 24.7370;
            doc.ActiveLayOut.Entities.AddItem(cir);

            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(4534.0798, 502.6282, 0.0000);
            cir.Radius = 24.7370;
            doc.ActiveLayOut.Entities.AddItem(cir);

            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(4632.9848, 502.6282, 0.0000);
            cir.Radius = 24.7370;
            doc.ActiveLayOut.Entities.AddItem(cir);

            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(4751.3943, 502.6282, 0.0000);
            cir.Radius = 24.7370;
            doc.ActiveLayOut.Entities.AddItem(cir);

            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(4859.4414, 502.6282, 0.0000);
            cir.Radius = 24.7370;
            doc.ActiveLayOut.Entities.AddItem(cir);

            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(4961.2658, 502.6282, 0.0000);
            cir.Radius = 24.7370;
            doc.ActiveLayOut.Entities.AddItem(cir);

            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(5069.3129, 502.6282, 0.0000);
            cir.Radius = 24.7370;
            doc.ActiveLayOut.Entities.AddItem(cir);

            cir = new vdCircle();
            cir.SetUnRegisterDocument(doc);
            cir.setDocumentDefaults();
            cir.Center = new gPoint(5168.2178, 502.6282, 0.0000);
            cir.Radius = 24.7370;
            doc.ActiveLayOut.Entities.AddItem(cir);

            //cir = new vdCircle();
            //cir.SetUnRegisterDocument(doc);
            //cir.setDocumentDefaults();
            //cir.Center = new gPoint();
            //cir.Radius = 24.7370;
            //doc.ActiveLayOut.Entities.AddItem(cir);


            #endregion

            #region Lines

            vdLine vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(2226.0549, 832.5539, 0.0000);
            vLIne.EndPoint = new gPoint(2226.1258, 419.7642, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(2183.1570, 832.5568, 0.0000);
            vLIne.EndPoint = new gPoint(2269.4646, 832.5509, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(2187.6522, 418.9995, 0.0000);
            vLIne.EndPoint = new gPoint(2273.9598, 418.9936, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(2405.4093, 750.5258, 0.0000);
            vLIne.EndPoint = new gPoint(2798.7285, 750.5258, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(2798.7285, 750.5258, 0.0000);
            vLIne.EndPoint = new gPoint(2946.2474, 477.6158, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(5210.0876, 477.6158, 0.0000);
            vLIne.EndPoint = new gPoint(2401.4014, 477.6158, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(4921.1675, 735.6835, 0.0000);
            vLIne.EndPoint = new gPoint(5210.5910, 735.6835, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(2442.5245, 224.6823, 0.0000);
            vLIne.EndPoint = new gPoint(2442.5245, 188.0015, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(2442.5245, 207.2821, 0.0000);
            vLIne.EndPoint = new gPoint(2935.1741, 207.2821, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(2936.9653, 188.2359, 0.0000);
            vLIne.EndPoint = new gPoint(2936.9653, 224.9166, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(2444.1116, 321.3254, 0.0000);
            vLIne.EndPoint = new gPoint(5122.1691, 324.3137, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(2567.3732, 348.1357, 0.0000);
            vLIne.EndPoint = new gPoint(2444.1116, 321.3254, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(2444.1116, 321.3254, 0.0000);
            vLIne.EndPoint = new gPoint(2567.3732, 293.1775, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(2567.3732, 293.1775, 0.0000);
            vLIne.EndPoint = new gPoint(2567.3732, 348.1357, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(2871.8924, 638.8776, 0.0000);
            vLIne.EndPoint = new gPoint(2908.9866, 665.0443, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(2893.2995, 681.3640, 0.0000);
            vLIne.EndPoint = new gPoint(2871.8924, 638.8776, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(2908.9866, 665.0443, 0.0000);
            vLIne.EndPoint = new gPoint(2893.2995, 681.3640, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(2871.8924, 638.8776, 0.0000);
            vLIne.EndPoint = new gPoint(2942.1048, 717.1914, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(2942.1048, 717.1914, 0.0000);
            vLIne.EndPoint = new gPoint(3004.2158, 717.1914, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(3330.4606, 476.7017, 0.0000);
            vLIne.EndPoint = new gPoint(3341.2210, 438.6124, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(3318.6240, 438.6124, 0.0000);
            vLIne.EndPoint = new gPoint(3340.8672, 438.6124, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(3330.4606, 476.7017, 0.0000);
            vLIne.EndPoint = new gPoint(3330.4606, 266.3269, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(3330.4606, 266.3269, 0.0000);
            vLIne.EndPoint = new gPoint(3449.1336, 266.3269, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(4321.3622, 559.0760, 0.0000);
            vLIne.EndPoint = new gPoint(4338.3038, 545.8704, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(4338.3038, 545.8704, 0.0000);
            vLIne.EndPoint = new gPoint(4333.2392, 568.3150, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(4333.2392, 568.3150, 0.0000);
            vLIne.EndPoint = new gPoint(4321.3622, 559.0760, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(4338.3038, 545.8704, 0.0000);
            vLIne.EndPoint = new gPoint(4228.7756, 726.5920, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(4228.7756, 726.5920, 0.0000);
            vLIne.EndPoint = new gPoint(4289.0161, 726.5920, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(4821.4591, 735.6835, 0.0000);
            vLIne.EndPoint = new gPoint(5210.5910, 735.6835, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(4687.3375, 477.6158, 0.0000);
            vLIne.EndPoint = new gPoint(4821.4591, 735.6835, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(4821.4591, 735.6835, 0.0000);
            vLIne.EndPoint = new gPoint(5210.5910, 735.6835, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(5243.4336, 206.2934, 0.0000);
            vLIne.EndPoint = new gPoint(5124.7477, 206.2934, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);


            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(5122.1691, 324.3137, 0.0000);
            vLIne.EndPoint = new gPoint(5036.4200, 302.6286, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(5036.4200,347.2235,0.0000);
            vLIne.EndPoint = new gPoint(5122.1691, 324.3137, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            vLIne = new vdLine();
            vLIne.SetUnRegisterDocument(doc);
            vLIne.setDocumentDefaults();
            vLIne.StartPoint = new gPoint(5036.4200, 347.2235, 0.0000);
            vLIne.EndPoint = new gPoint(5036.4200, 302.6286, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(vLIne);

            #endregion

        }
        public void Slab_Drawing_BoQ(vdDocument doc, string txtValueFile)
        {
            string boq_file = txtValueFile;

            doc.ActiveLayOut.Entities.RemoveAll();
            #region Draw Outer
            vdRect rec = new vdRect();
            rec.SetUnRegisterDocument(doc);
            rec.setDocumentDefaults();
            rec.InsertionPoint = new gPoint(354.8477, 349.2907, 0.0000);
            rec.Width = 289.9892;
            rec.Height = -200.3892;
            doc.ActiveLayOut.Entities.AddItem(rec);
            #endregion
            
            DrawLines(doc);
            DrawText(doc);

            //BoQ boq = new BoQ(@"D:\SOFTWARE TESTING\ASTRA\Astra Examples\TEST\Boq.txt");
            BoQ boq = new BoQ(txtValueFile);
            boq.ReadFromFile();
            boq.Draw_Boq(doc);
            doc.Redraw(true);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);

        }

        public void DrawLines(vdDocument doc)
        {
            #region Draw Lines
            vdLine ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new gPoint(359.5732, 343.8743, 0.0000);
            ln.EndPoint = new gPoint(639.5732, 343.8743, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);

            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new gPoint(359.5732, 333.3188, 0.0000);
            ln.EndPoint = new gPoint(639.5732, 333.3188, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);


            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new gPoint(359.5732, 153.8743, 0.0000);
            ln.EndPoint = new gPoint(359.5732, 343.8743, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);


       


            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new gPoint(359.5732, 323.6428, 0.0000);
            ln.EndPoint = new gPoint(639.5732, 323.6428, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);   
            
            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new gPoint(639.5732, 153.8743, 0.0000);
            ln.EndPoint = new gPoint(359.5732, 153.8743, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);

            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new gPoint(639.5732, 343.8743, 0.0000);
            ln.EndPoint = new gPoint(639.5732, 153.8743, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);

            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new gPoint(639.5732, 153.8743, 0.0000);
            ln.EndPoint = new gPoint(639.5732, 323.6428, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);

            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new gPoint(530.8337, 343.9386, 0.0000);
            ln.EndPoint = new gPoint(530.8337, 323.7308, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);

            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new gPoint(560.9554, 343.9386, 0.0000);
            ln.EndPoint = new gPoint(560.9554, 323.7308, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);

            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new gPoint(589.9764, 333.2206, 0.0000);
            ln.EndPoint = new gPoint(589.9764, 323.7308, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);

            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new gPoint(589.9348, 343.9386, 0.0000);
            ln.EndPoint = new gPoint(589.9348, 333.3323, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);

            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new gPoint(618.9965, 343.9386, 0.0000);
            ln.EndPoint = new gPoint(618.9965, 333.2206, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);

            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new gPoint(359.5732, 299.8928, 0.0000);
            ln.EndPoint = new gPoint(639.5732, 299.8928, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);


            // Vertical Lines
            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new gPoint(377.2083, 323.1503, 0.0000);
            ln.EndPoint = new gPoint(377.2083, 153.5810, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);

            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new gPoint(404.7061, 323.1503, 0.0000);
            ln.EndPoint = new gPoint(404.7061, 153.5810, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);

            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new gPoint(421.6630, 323.1503, 0.0000);
            ln.EndPoint = new gPoint(421.6630, 153.5810, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);

            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new gPoint(438.6199, 323.1503, 0.0000);
            ln.EndPoint = new gPoint(438.6199, 153.5810, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);

            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new gPoint(456.4934, 323.1503, 0.0000);
            ln.EndPoint = new gPoint(456.4934, 153.5810, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);

            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new gPoint(473.1486, 323.1503, 0.0000);
            ln.EndPoint = new gPoint(473.1486, 153.5810, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);

            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new gPoint(489.9490, 323.1503, 0.0000);
            ln.EndPoint = new gPoint(489.9490, 153.5810, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);

            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new gPoint(506.9059, 323.1503, 0.0000);
            ln.EndPoint = new gPoint(506.9059, 153.5810, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);

            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new gPoint(530.8791, 323.6527, 0.0000);
            ln.EndPoint = new gPoint(530.8791, 154.0834, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);

            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new gPoint(547.5151, 275.1984, 0.0000);
            ln.EndPoint = new gPoint(604.2699, 275.1984, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);

            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new gPoint(604.2699, 275.1984, 0.0000);
            ln.EndPoint = new gPoint(618.0179, 286.4788, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);

            ln = new vdLine();
            ln.SetUnRegisterDocument(doc);
            ln.setDocumentDefaults();
            ln.StartPoint = new gPoint(618.0179, 286.4788, 0.0000);
            ln.EndPoint = new gPoint(634.2336, 286.4788, 0.0000);
            doc.ActiveLayOut.Entities.AddItem(ln);

            #endregion

        }
        public void DrawText(vdDocument doc)
        {
            vdText txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(362.3510,335.5178,0.0000);
            txt.TextString = "REINFORCEMENT BAR SCHEDULE";
            txt.Height = 4.0000;
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(533.0058, 339.9160, 0.0000);
            txt.TextString = "Project No:";
            txt.Height = 3.0000;
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(461.9608, 304.8298, 0.0000);
            txt.TextString = "(mm)";
            txt.Height = 3.0000;
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(477.6359, 304.8298, 0.0000);
            txt.TextString = "(mm)";
            txt.Height = 3.0000;
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(513.8194, 304.8298, 0.0000);
            txt.TextString = "(Ton)";
            txt.Height = 3.0000;
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(533.0058,335.5178,0.0000);
            txt.TextString = "TES2009_68";
            txt.Height = 3.0000;
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(533.0058,329.7123,0.0000);
            txt.TextString = "Designed By:";
            txt.Height = 3.0000;
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(563.0898,339.9160,0.0000);
            txt.TextString = "Job No:";
            txt.Height =3.0000 ;
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(563.0898,335.5178,0.0000);
            txt.TextString = "9004_04";
            txt.Height = 3.0000;
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(563.3169,329.7123,0.0000);
            txt.TextString = "Checked By:";
            txt.Height = 3.0000;
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(591.1712,339.9160,0.0000);
            txt.TextString = "Schedule No:";
            txt.Height = 3.0000;
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(591.1712,335.0780,0.0000);
            txt.TextString = "SLAB02_01";
            txt.Height = 3.0000;
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(620.9621,339.9160,0.0000);
            txt.TextString = "Revision:";
            txt.Height = 3.0000;
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(620.9621,335.0780,0.0000);
            txt.TextString = "R0";
            txt.Height = 3.0000;
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(362.3510,327.1614,0.0000);
            txt.TextString = "SLAB One Way Single Span Limit State Design";
            txt.Height = 3.0000;
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(533.0058,329.7123,0.0000);
            txt.TextString = "Designed By:";
            txt.Height = 3.0000;
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(563.3169,329.7123,0.0000);
            txt.TextString = "Checked By:";
            txt.Height = 3.0000;
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(606.4685,329.7123,0.0000);
            txt.TextString = "Date:";
            txt.Height = 3.0;
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(363.1686,317.8333,0.0000);
            txt.TextString = "S. No:";
            txt.Height =3.0000 ;
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(379.6195,317.8333,0.0000);
            txt.TextString = "Member";
            txt.Height = 3.0000;
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(407.2933,317.8333,0.0000);
            txt.TextString = "Bar";
            txt.Height = 3.0000;
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(407.2933,310.5005,0.0000);
            txt.TextString = "Mark";
            txt.Height = 3.0;
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(425.6251,317.8333,0.0000);
            txt.TextString = "Bar";
            txt.Height = 3.0;
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(425.6251,310.5005,0.0000);
            txt.TextString = "Code";
            txt.Height = 3.0;
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(442.1238,317.8333,0.0000);
            txt.TextString = "Bar";
            txt.Height = 3.0;
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(442.1238,310.5005,0.0000);
            txt.TextString = "Grade";
            txt.Height = 3.0000;
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(459.9973,317.8333,0.0000);
            txt.TextString = "Bar";
            txt.Height = 3.0000;
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(459.9973,310.5005,0.0000);
            txt.TextString = "Dia.";
            txt.Height = 3.0000;
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(474.6628,317.8333,0.0000);
            txt.TextString = "Bar";
            txt.Height = 3.00;
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(474.6628,310.5005,0.0000);
            txt.TextString = "Length";
            txt.Height = 3.00;
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(494.3695,317.8333,0.0000);
            txt.TextString = "Bar";
            txt.Height = 3.00;
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(494.3695, 310.5005, 0.0000);
            txt.TextString = "Nos.";
            txt.Height = 3.00;
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(512.7013, 317.8333, 0.0000);
            txt.TextString = "Bar";
            txt.Height = 3.00;
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(512.7013, 310.5005, 0.0000);
            txt.TextString = "Weight";
            txt.Height = 3.00;
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(573.5826, 317.8333, 0.0000);
            txt.TextString = "Bar";
            txt.Height = 3.00;
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(573.5826, 310.5005, 0.0000);
            txt.TextString = "Shape";
            txt.Height = 3.00;
            doc.ActiveLayOut.Entities.AddItem(txt);
            
            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(381.6443, 247.8730, 0.0000);
            txt.TextString = "SLAB at";
            txt.Height = 3.00;
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(381.6443, 242.2328, 0.0000);
            txt.TextString = "First Floor";
            txt.Height = 3.00;
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(381.6443, 232.0098, 0.0000);
            txt.TextString = "Size:";
            txt.Height = 3.00;
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(381.6443, 219.3193, 0.0000);
            txt.TextString = "Sq. m.";
            txt.Height = 3.00;
            doc.ActiveLayOut.Entities.AddItem(txt);
        }
        public void Draw_UserText(vdDocument doc)
        {
            //1//T10_B1,T6_B1,T10_B1
            //2//Fe 415,Fe 415,Fe 415
            //3//10,6,10
            //4//3470,7970,3470
            //5//56,35,56
            //6//0.330,0.220,0.330
            //7//3040,130,1230
            string fName = @"D:\SOFTWARE TESTING\ASTRA\Astra Examples\[01] Static Analysis of Frame [Beam]\Data\DESIGN_SLAB02\BoQ_Text.txt";
            List<string> lstStr = new List<string>(File.ReadAllLines(fName, Encoding.ASCII));
            int index = 0;

//BoQ CODE
//S_N0 1,2
//Member 3.5 X 8.0
//Bar_Mark 01,02
//Bar_CODE T10_B1,T6_B2
//Bar_Grade Fe 415,Fe 415
//Bar_Dia 10,6
//Bar_Length 3470,7970
//Bar_Nos 56,35
//Bar_Weight 0.330,0.220
//Bar_Shape 3019,120,1209
//Bar_Shape 1209,120,3019
//END BoQ

            MyStrings mList = new MyStrings(MyStrings.RemoveAllSpaces(lstStr[index]),',');

            vdText txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(380.1654, 226.0354, 0.0000);
            txt.Height = 3.0d;
            txt.TextString = mList.StringList[0];
            doc.ActiveLayOut.Entities.AddItem(txt);
            index++;

            #region Bar Code

            mList = new MyStrings(MyStrings.RemoveAllSpaces(lstStr[index]), ',');
            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(421.9682, 285.7545, 0.0000);
            txt.Height = 3.0d;
            txt.TextString = mList.StringList[0];
            doc.ActiveLayOut.Entities.AddItem(txt);


            //txt = new vdText();
            //txt.SetUnRegisterDocument(doc);
            //txt.setDocumentDefaults();
            //txt.InsertionPoint = new gPoint(421.9682, 244.1578, 0.0000);
            //txt.TextString = mList.StringList[1];
            //txt.Height = 3.0d;
            //doc.ActiveLayOut.Entities.AddItem(txt);

            //txt = new vdText();
            //txt.SetUnRegisterDocument(doc);
            //txt.setDocumentDefaults();
            //txt.InsertionPoint = new gPoint(421.9682, 199.7411, 0.0000);
            //txt.Height = 3.0d;
            //txt.TextString = mList.StringList[2];
            //doc.ActiveLayOut.Entities.AddItem(txt);

            #endregion
            index++;
            #region Bar Grade

            mList = new MyStrings(MyStrings.RemoveAllSpaces(lstStr[index]), ',');

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(441.7254, 285.7545, 0.0000);
            txt.Height = 3.0d;
            txt.TextString = mList.StringList[0];
            doc.ActiveLayOut.Entities.AddItem(txt);

            //txt = new vdText();
            //txt.SetUnRegisterDocument(doc);
            //txt.setDocumentDefaults();
            //txt.InsertionPoint = new gPoint(441.7254, 244.1578, 0.0000);
            //txt.Height = 3.0d;
            //txt.TextString = mList.StringList[1];
            //doc.ActiveLayOut.Entities.AddItem(txt);

            //txt = new vdText();
            //txt.SetUnRegisterDocument(doc);
            //txt.setDocumentDefaults();
            //txt.InsertionPoint = new gPoint(441.7254, 199.7411, 0.0000);
            //txt.Height = 3.0d;
            //txt.TextString = mList.StringList[2];
            //doc.ActiveLayOut.Entities.AddItem(txt);

            #endregion
            index++;


            #region Bar Dia

            mList = new MyStrings(MyStrings.RemoveAllSpaces(lstStr[index]), ',');

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(458.2495, 285.7545, 0.0000);
            txt.Height = 3.0d;
            txt.TextString = mList.StringList[0];
            doc.ActiveLayOut.Entities.AddItem(txt);

            //txt = new vdText();
            //txt.SetUnRegisterDocument(doc);
            //txt.setDocumentDefaults();
            //txt.InsertionPoint = new gPoint(458.2495, 244.1578, 0.0000);
            //txt.Height = 3.0d;
            //txt.TextString = mList.StringList[1];
            //doc.ActiveLayOut.Entities.AddItem(txt);

            //txt = new vdText();
            //txt.SetUnRegisterDocument(doc);
            //txt.setDocumentDefaults();
            //txt.InsertionPoint = new gPoint(458.2495, 199.7411, 0.0000);
            //txt.Height = 3.0d;
            //txt.TextString = mList.StringList[2];
            //doc.ActiveLayOut.Entities.AddItem(txt);

            #endregion
            index++;

            #region Bar Length

            mList = new MyStrings(MyStrings.RemoveAllSpaces(lstStr[index]), ',');

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(475.0796, 285.7545, 0.0000);
            txt.Height = 3.0d;
            txt.TextString = mList.StringList[0];
            doc.ActiveLayOut.Entities.AddItem(txt);

            //txt = new vdText();
            //txt.SetUnRegisterDocument(doc);
            //txt.setDocumentDefaults();
            //txt.InsertionPoint = new gPoint(475.0796, 244.1578, 0.0000);
            //txt.Height = 3.0d;
            //txt.TextString = mList.StringList[1];
            //doc.ActiveLayOut.Entities.AddItem(txt);

            //txt = new vdText();
            //txt.SetUnRegisterDocument(doc);
            //txt.setDocumentDefaults();
            //txt.InsertionPoint = new gPoint(475.0796, 199.7411, 0.0000);
            //txt.Height = 3.0d;
            //txt.TextString = mList.StringList[2];
            //doc.ActiveLayOut.Entities.AddItem(txt);

            #endregion
            index++;

            #region Bar Nos

            mList = new MyStrings(MyStrings.RemoveAllSpaces(lstStr[index]), ',');

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(496.1937, 285.7545, 0.0000);
            txt.Height = 3.0d;
            txt.TextString = mList.StringList[0];
            doc.ActiveLayOut.Entities.AddItem(txt);

            //txt = new vdText();
            //txt.SetUnRegisterDocument(doc);
            //txt.setDocumentDefaults();
            //txt.InsertionPoint = new gPoint(496.1937, 244.1578, 0.0000);
            //txt.Height = 3.0d;
            //txt.TextString = mList.StringList[1];
            //doc.ActiveLayOut.Entities.AddItem(txt);

            //txt = new vdText();
            //txt.SetUnRegisterDocument(doc);
            //txt.setDocumentDefaults();
            //txt.InsertionPoint = new gPoint(496.1937, 199.7411, 0.0000);
            //txt.Height = 3.0d;
            //txt.TextString = mList.StringList[2];
            //doc.ActiveLayOut.Entities.AddItem(txt);

            #endregion
            index++;

            #region Bar Weight

            mList = new MyStrings(MyStrings.RemoveAllSpaces(lstStr[index]), ',');

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(513.8194, 285.7545, 0.0000);
            txt.Height = 3.0d;
            txt.TextString = mList.StringList[0];
            doc.ActiveLayOut.Entities.AddItem(txt);

            //txt = new vdText();
            //txt.SetUnRegisterDocument(doc);
            //txt.setDocumentDefaults();
            //txt.InsertionPoint = new gPoint(513.8194, 244.1578, 0.0000);
            //txt.Height = 3.0d;
            //txt.TextString = mList.StringList[1];
            //doc.ActiveLayOut.Entities.AddItem(txt);

            //txt = new vdText();
            //txt.SetUnRegisterDocument(doc);
            //txt.setDocumentDefaults();
            //txt.InsertionPoint = new gPoint(513.8194, 199.7411, 0.0000);
            //txt.Height = 3.0d;
            //txt.TextString = mList.StringList[2];
            //doc.ActiveLayOut.Entities.AddItem(txt);

            #endregion
            index++;

            #region Bar Shape

            mList = new MyStrings(MyStrings.RemoveAllSpaces(lstStr[index]), ',');

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(560.6099, 276.2003, 0.0000);
            txt.Height = 3.0d;
            txt.TextString = mList.StringList[0];
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(603.6153, 279.3135, 0.0000);
            txt.Rotation = (Math.PI / 180) * 41.0d;
            txt.Height = 3.0d;
            txt.TextString = mList.StringList[1];
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(621.4423, 287.7307, 0.0000);
            txt.Height = 3.0d;
            txt.TextString = mList.StringList[2];
            doc.ActiveLayOut.Entities.AddItem(txt);

            #endregion

            //#region Bar Shape 2

            //mList = new MyList(MyList.RemoveAllSpaces(lstStr[index]), ',');

            //txt = new vdText();
            //txt.SetUnRegisterDocument(doc);
            //txt.setDocumentDefaults();
            //txt.InsertionPoint = new gPoint(561.8027, 190.3192, 0.0000);
            //txt.Height = 3.0d;
            //txt.TextString = mList.StringList[0];
            //doc.ActiveLayOut.Entities.AddItem(txt);

            //txt = new vdText();
            //txt.SetUnRegisterDocument(doc);
            //txt.setDocumentDefaults();
            //txt.InsertionPoint = new gPoint(604.8081, 193.4324, 0.0000);
            //txt.Rotation = (Math.PI / 180) * 41.0d;
            //txt.Height = 3.0d;
            //txt.TextString = mList.StringList[1];
            //doc.ActiveLayOut.Entities.AddItem(txt);

            //txt = new vdText();
            //txt.SetUnRegisterDocument(doc);
            //txt.setDocumentDefaults();
            //txt.InsertionPoint = new gPoint(622.6351, 201.8496, 0.0000);
            //txt.Height = 3.0d;
            //txt.TextString = mList.StringList[2];
            //doc.ActiveLayOut.Entities.AddItem(txt);

            //#endregion
        }
        public void DrawBar2(vdDocument doc)
        {
        }
        public void DrawTextBar2(vdDocument doc)
        {
            

            #region BAR VIEW
            vdText txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(550.0926, 227.7606, 0.0000);
            txt.TextString = "1232";
            txt.Height = 3.0d;
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(568.2291, 225.2402, 0.0000);
            txt.TextString = "2212";
            txt.Rotation = (Math.PI / 180) * 313.0d;
            txt.Height = 3.0d;
            doc.ActiveLayOut.Entities.AddItem(txt);

            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(586.4907, 215.5688, 0.0000);
            txt.TextString = "322323333";
            txt.Height = 3.0d;
            doc.ActiveLayOut.Entities.AddItem(txt);

            #endregion


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(364.0394, 212.3722, 0.0000);
            txt.TextString = "2.";
            txt.Height = 3.0d;
            doc.ActiveLayOut.Entities.AddItem(txt);



            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(408.9850, 212.3722, 0.0000);
            txt.TextString = "02";
            txt.Height = 3.0d;
            doc.ActiveLayOut.Entities.AddItem(txt);



            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(422.8390, 212.3722, 0.0000);
            txt.TextString = "T6_B2";
            txt.Height = 3.0d;
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(442.5962, 212.3722, 0.0000);
            txt.TextString = "Fe 415";
            txt.Height = 3.0d;
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(459.1203, 212.3722, 0.0000);
            txt.TextString = "6";
            txt.Height = 3.0d;
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(475.9504, 212.3722, 0.0000);
            txt.TextString = "7970";
            txt.Height = 3.0d;
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(497.0645, 212.3722, 0.0000);
            txt.TextString = "35";
            txt.Height = 3.0d;
            doc.ActiveLayOut.Entities.AddItem(txt);


            txt = new vdText();
            txt.SetUnRegisterDocument(doc);
            txt.setDocumentDefaults();
            txt.InsertionPoint = new gPoint(514.6902, 212.3722, 0.0000);
            txt.TextString = "0.220";
            txt.Height = 3.0d;
            doc.ActiveLayOut.Entities.AddItem(txt);
        }
        #endregion


        class BoQ
        {
            #region Member Variable
            string member = "";
            string s_no = "";
            string bar_mark;
            string bar_code;
            string bar_grade;
            string bar_dia;
            string bar_length;
            string bar_nos;
            string bar_weight;
            string bar_spacing;
            List<MyStrings> lst_bar_shape = null;
            string file_name;
            #endregion 


            #region ctor
            public BoQ(string fileName)
            {
                member = "";
                s_no = "";
                bar_mark = "";
                bar_code = "";
                bar_grade = "";
                bar_dia = "";
                bar_length = "";
                bar_nos = "";
                bar_weight = "";
                bar_spacing = "";
                lst_bar_shape = new List<MyStrings>();
                file_name = fileName;
            }
            #endregion

            #region Public Property

            public MyStrings S_No
            {
                get
                {
                    MyStrings mList = new MyStrings(s_no, ',');
                    return mList;
                }
            }
            public MyStrings Member
            {
                get
                {
                    MyStrings mList = new MyStrings(member, 'X');
                    return mList;
                }
            }
            public MyStrings Bar_Spacing
            {
                get
                {
                    MyStrings mList = new MyStrings(bar_spacing, ',');
                    return mList;
                }
            }
            public MyStrings Bar_Mark
            {
                get
                {
                    MyStrings mList = new MyStrings(bar_mark, ',');
                    return mList;
                }
            }
            public MyStrings Bar_Code
            {
                get
                {
                    MyStrings mList = new MyStrings(bar_code, ',');
                    return mList;
                }
            }
            public MyStrings Bar_Grade
            {
                get
                {
                    MyStrings mList = new MyStrings(bar_grade, ',');
                    return mList;
                }
            }
            public MyStrings Bar_Dia
            {
                get
                {
                    MyStrings mList = new MyStrings(bar_dia, ',');
                    return mList;
                }
            }
            public MyStrings Bar_Length
            {
                get
                {
                    MyStrings mList = new MyStrings(bar_length, ',');
                    return mList;
                }
            }
            public MyStrings Bar_Nos
            {
                get
                {
                    MyStrings mList = new MyStrings(bar_nos, ',');
                    return mList;
                }
            }
            public MyStrings Bar_Weight
            {
                get
                {
                    MyStrings mList = new MyStrings(bar_weight, ',');
                    return mList;
                }
            }
            public List<MyStrings> LIST_BAR_SHAPE
            {
                get
                {
                    return lst_bar_shape;
                }

            }
            #endregion

            public bool ReadFromFile()
            {
                //file_name = @"D:\SOFTWARE TESTING\ASTRA\Astra Examples\TEST\boq.txt";


                string boq_file = file_name;
                if (!File.Exists(boq_file)) return false;

                List<string> lstStr = new List<string>(File.ReadAllLines(boq_file));
                MyStrings mList = null;
                string kStr = "";

                bool IsBoqFind = false;
                #region For Loop
                for (int i = 0; i < lstStr.Count; i++)
                {
                    kStr = lstStr[i].ToLower().Replace('\t', ' ').Replace("  ", " ").TrimEnd().TrimStart();
                    if (kStr.ToLower().Trim() == "boq code")
                    {
                        IsBoqFind = true;
                    }
                    else if (kStr.ToLower().Trim() == "end boq")
                    {
                        IsBoqFind = false;
                    }
                    if (!IsBoqFind) continue;

                    mList = new MyStrings(MyStrings.RemoveAllSpaces(kStr), ' ');
                    #region switch case
                    switch (mList.StringList[0])
                    {
                            
                        case "s_no":
                            s_no = mList.StringList[1].ToUpper();
                            break;
                        case "member":
                            member = mList.StringList[1] + " X " + mList.StringList[2];
                            break;
                        case "bar_spacing":
                            bar_spacing = mList.StringList[1];
                            break;
                        case "bar_mark":
                            bar_mark = mList.StringList[1].ToUpper();
                            break;
                        case "bar_code":
                            bar_code = mList.StringList[1].ToUpper();
                            break;
                        case "bar_grade":
                            bar_grade = mList.StringList[1].Replace("fe","Fe ");
                            break;
                        case "bar_dia":
                            bar_dia = mList.StringList[1].ToUpper();
                            break;
                        case "bar_length":
                            bar_length = mList.StringList[1].ToUpper();
                            break;
                        case "bar_nos":
                            bar_nos = mList.StringList[1].ToUpper();
                            break;
                        case "bar_weight":
                            bar_weight = mList.StringList[1].ToUpper();
                            break;
                        case "bar_shape":
                            lst_bar_shape.Add(new MyStrings(mList.StringList[1].ToUpper(), ','));
                            break;
                    }
                    #endregion
                }

                #endregion

                return true;
            }
            public void Draw_Boq(vdDocument doc)
            {
                vdLine ln = new vdLine();
                vdText txt = new vdText();

                #region S. No
                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = S_No.StringList[0];
                txt.InsertionPoint = new gPoint(363.1686, 285.7545, 0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);

                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = S_No.StringList[1];
                txt.InsertionPoint = new gPoint(363.2442, 249.7464, 0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);

                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = S_No.StringList[2];
                txt.InsertionPoint = new gPoint(361.9758, 200.6686, 0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);
                #endregion

                #region Member  
                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = Member.StringList[0] + " X " + Member.StringList[1];
                txt.InsertionPoint = new gPoint(380.1654,226.0354,0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);
                #endregion


                #region Bar Mark  
                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = Bar_Mark.StringList[0];
                txt.InsertionPoint = new gPoint(408.1142,285.7545,0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);

                
                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = Bar_Mark.StringList[1];
                txt.InsertionPoint = new gPoint(408.1898,249.7464,0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);
                
                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = Bar_Mark.StringList[2];
                txt.InsertionPoint = new gPoint(406.9214,200.6686,0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);

                #endregion

                #region Bar Code 
                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = Bar_Code.StringList[0];
                txt.InsertionPoint = new gPoint(421.9682,285.7545,0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);

                
                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = Bar_Code.StringList[1];
                txt.InsertionPoint = new gPoint(422.0438,249.7464,0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);
                
                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = Bar_Code.StringList[2];
                txt.InsertionPoint = new gPoint(421.9682,200.6686,0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);

                #endregion


                #region Bar Grade
                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = Bar_Grade.StringList[0];
                txt.InsertionPoint = new gPoint(441.7254,285.7545,0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);

                
                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = Bar_Grade.StringList[1];
                txt.InsertionPoint = new gPoint(441.8010,249.7464,0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);
                
                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = Bar_Grade.StringList[2];
                txt.InsertionPoint = new gPoint(440.5326,200.6686,0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);

                #endregion

                #region Bar Dia
                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = Bar_Dia.StringList[0];
                txt.InsertionPoint = new gPoint(458.2495,285.7545,0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);

                
                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = Bar_Dia.StringList[1];
                txt.InsertionPoint = new gPoint(458.3251,249.7464,0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);
                
                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = Bar_Dia.StringList[2];
                txt.InsertionPoint = new gPoint(458.6471,200.6686,0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);

                #endregion

                #region Bar Length
                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = Bar_Length.StringList[0];
                txt.InsertionPoint = new gPoint(475.0796,285.7545,0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);

                
                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = Bar_Length.StringList[1];
                txt.InsertionPoint = new gPoint(475.1552,249.7464,0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);
                
                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = Bar_Length.StringList[2];
                txt.InsertionPoint = new gPoint(477.0676,201.0662,0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);

                #endregion

                #region Bar Nos
                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = Bar_Nos.StringList[0];
                txt.InsertionPoint = new gPoint(496.1937,285.7545,0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);

                
                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = Bar_Nos.StringList[1];
                txt.InsertionPoint = new gPoint(496.2693,249.7464,0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);
                
                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = Bar_Nos.StringList[2];
                txt.InsertionPoint = new gPoint(495.0009,200.6686,0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);

                #endregion

                #region Bar Weight
                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = Bar_Weight.StringList[0];
                txt.InsertionPoint = new gPoint(513.8194,285.7545,0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);

                
                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = Bar_Weight.StringList[1];
                txt.InsertionPoint = new gPoint(513.8950,249.7464,0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);
                
                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = Bar_Weight.StringList[2];
                txt.InsertionPoint = new gPoint(512.6266,200.6686,0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);

                #endregion

                #region Bar Shape 1
                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = LIST_BAR_SHAPE[0].StringList[0];
                txt.InsertionPoint = new gPoint(560.6099, 276.2003, 0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);

                ln = new vdLine();
                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = new gPoint(547.5151, 275.1984, 0.0000);
                ln.EndPoint = new gPoint(604.2699, 275.1984, 0.0000);
                doc.ActiveLayOut.Entities.AddItem(ln);

                
                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = LIST_BAR_SHAPE[0].StringList[1];
                txt.InsertionPoint = new gPoint(603.6153, 279.3135, 0.0000);
                txt.Rotation = (Math.PI / 180) * 41.0d;
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);

                ln = new vdLine();
                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = new gPoint(604.2699, 275.1984, 0.0000);
                ln.EndPoint = new gPoint(618.0179, 286.4788, 0.0000);
                doc.ActiveLayOut.Entities.AddItem(ln);

                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = LIST_BAR_SHAPE[0].StringList[2];
                txt.InsertionPoint = new gPoint(621.4423,287.7307,0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);

                ln = new vdLine();
                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = new gPoint(618.0179, 286.4788, 0.0000);
                ln.EndPoint = new gPoint(634.2336, 286.4788, 0.0000);
                doc.ActiveLayOut.Entities.AddItem(ln);

                #endregion

                #region Bar Shape 2
                ln = new vdLine();
                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = new gPoint(547.9756, 233.5898, 0.0000);
                ln.EndPoint = new gPoint(634.2303, 233.5898, 0.0000);
                doc.ActiveLayOut.Entities.AddItem(ln);

                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = LIST_BAR_SHAPE[1].StringList[0];
                txt.InsertionPoint = new gPoint(570.1892,237.0391,0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);

                #endregion

                #region Bar Shape 3
                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = LIST_BAR_SHAPE[0].StringList[2];
                txt.InsertionPoint = new gPoint(549.6950,197.1456,0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);

                ln = new vdLine();
                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = new gPoint(563.7098, 194.6693, 0.0000);
                ln.EndPoint = new gPoint(547.4941, 194.6693, 0.0000);
                doc.ActiveLayOut.Entities.AddItem(ln);


                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = LIST_BAR_SHAPE[0].StringList[1];
                txt.InsertionPoint = new gPoint(567.8315,194.6252,0.0000);
                txt.Height = 3;
                txt.Rotation = (Math.PI/180) * 313;
                doc.ActiveLayOut.Entities.AddItem(txt);

                ln = new vdLine();
                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = new gPoint(577.4578, 183.3888, 0.0000);
                ln.EndPoint = new gPoint(563.7098, 194.6693, 0.0000);
                doc.ActiveLayOut.Entities.AddItem(ln);


                
                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = LIST_BAR_SHAPE[0].StringList[0];
                txt.InsertionPoint = new gPoint(586.0931,184.9538,0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);

                ln = new vdLine();
                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = new gPoint(634.2126, 183.3888, 0.0000);
                ln.EndPoint = new gPoint(577.4578, 183.3888, 0.0000);
                doc.ActiveLayOut.Entities.AddItem(ln);

                #endregion

                #region Lines

                ln = new vdLine();
                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = new gPoint(359.1756, 263.6461, 0.0000);
                ln.EndPoint = new gPoint(376.8107, 263.6492, 0.0000);
                doc.ActiveLayOut.Entities.AddItem(ln);

                ln = new vdLine();
                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = new gPoint(404.7061, 263.6540, 0.0000);
                ln.EndPoint = new gPoint(639.1756, 263.6947, 0.0000);
                doc.ActiveLayOut.Entities.AddItem(ln);

                ln = new vdLine();
                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = new gPoint(359.9708, 207.9824, 0.0000);
                ln.EndPoint = new gPoint(377.6059, 207.9855, 0.0000);
                doc.ActiveLayOut.Entities.AddItem(ln);

                ln = new vdLine();
                ln.SetUnRegisterDocument(doc);
                ln.setDocumentDefaults();
                ln.StartPoint = new gPoint(405.1037, 207.9902, 0.0000);
                ln.EndPoint = new gPoint(639.9708, 208.0310, 0.0000);
                doc.ActiveLayOut.Entities.AddItem(ln);

                

                #endregion


                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                txt.TextString = "@ " + Bar_Spacing.GetDouble(0)*2 + " c/c over length " + Member.GetDouble(1)*1000 + " mm";
                txt.InsertionPoint = new gPoint(551.8613, 268.7772, 0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);


                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                //txt.TextString = "@ 240 c/c over length 8000 mm";
                txt.TextString = "@ " + Bar_Spacing.StringList[1] + " c/c over length " + Member.GetDouble(0) * 1000 + " mm";
                txt.InsertionPoint = new gPoint(553.1866, 213.4448, 0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);


                txt = new vdText();
                txt.SetUnRegisterDocument(doc);
                txt.setDocumentDefaults();
                //txt.TextString = "@ 100 c/c over length 8000 mm";
                txt.TextString = "@ " + Bar_Spacing.GetDouble(0) * 2 + " c/c over length " + Member.GetDouble(1) * 1000 + " mm";
                txt.InsertionPoint = new gPoint(552.4339, 165.7199, 0.0000);
                txt.Height = 3;
                doc.ActiveLayOut.Entities.AddItem(txt);
            }
        }
    }
    public interface ISLAB02
    {
        double L { get; set; }
        double B { get; set; }
        double LL { get; set; }
        double Sigma_ck { get; set; }
        double Slab_Load { get; set; }
        double Sigma_y { get; set; }
        double Alpha { get; set; }
        double Beta { get; set; }
        double Gamma { get; set; }
        double Delta { get; set; }
        double Lamda { get; set; }
        double D1 { get; set; }
        double D2 { get; set; }
        double H1 { get; set; }
        double H2 { get; set; }
        double Ads { get; set; }
        double Tc { get; set; }
    }
    
    public class KValueTable
    {
        //Ashok Kumar Jain
        //Limit State Design
        //Page 353
        string fileName = "";

        MyStrings mList = null;
        List<double> lstD = null;
        List<double> lstK = null;
        public KValueTable(string fileName)
        {
            //hash_kTable = new Hashtable();
            this.fileName = fileName;
            lstD = new List<double>();
            lstK = new List<double>();
            SetTableValue();
        }
        void SetTableValue()
        {
            List<string> lstStr = new List<string>(File.ReadAllLines(fileName));
            string kStr = "";
            for (int i = 0; i < lstStr.Count; i++)
            {
                kStr = lstStr[i].Trim().TrimEnd().TrimStart().ToUpper();
                if (kStr == "K VALUES FOR SHEAR" || kStr == "") continue;
                mList = new MyStrings(MyStrings.RemoveAllSpaces(kStr), ' ');

                if (mList.StringList[0] == "D")
                {
                    for(int j = 1; j < mList.Count;j++)
                    {
                        lstD.Add(mList.GetDouble(j));
                    }
                }
                if (mList.StringList[0] == "K")
                {
                    for (int j = 1; j < mList.Count; j++)
                    {
                        lstK.Add(mList.GetDouble(j));
                    }
                }
            }
        }

        public double Get_KValue(double D)
        {
            double k = 0.0d;
            for (int i = 0; i < lstD.Count; i++)
            {
                if (D >= lstD[0])
                {
                    k = lstK[0]; break;
                }
                else if (D <= lstD[lstD.Count - 1])
                {
                    k = lstK[lstK.Count - 1]; break;
                }
                else if (D >= lstD[i])
                {
                    k = lstK[i - 1]; break;
                }
            }
            return k;
        }
    }
    public class ShearValue
    {
        //IS 456 :2000
        // Page 73
        List<double> lstPercent = null;
        List<double> lstM15 = null;
        //List<double> lstM20 = null;
        //List<double> lstM25 = null;
        //List<double> lstM30 = null;
        //List<double> lstM35 = null;
        //List<double> lstM40 = null;


        public ShearValue()
        {
            lstPercent = new List<double>();
             lstM15 = new List<double>();
             //lstM20 = List<double>();
             //lstM25 = List<double>();
             //lstM30 = List<double>();
             //lstM35 = List<double>();
             //lstM40 = List<double>();
             SetValue();
        }
        private void SetValue()
        {
            // 1
            lstPercent.Add(0.15);
            //lstM15.Add(0.28);
            //lstM20.Add(0.28);
            //lstM25.Add(0.29);
            //lstM30.Add(0.29);
            //lstM35.Add(0.29);
            //lstM40.Add(0.30);

            // 2
            lstPercent.Add(0.25);
            //lstM15.Add(0.35);
            //lstM20.Add(0.36);
            //lstM25.Add(0.36);
            //lstM30.Add(0.37);
            //lstM35.Add(0.37);
            //lstM40.Add(0.38);

            // 3
            lstPercent.Add(0.5);
            //lstM15.Add(0.46);
            //lstM20.Add(0.48);
            //lstM25.Add(0.49);
            //lstM30.Add(0.50);
            //lstM35.Add(0.50);
            //lstM40.Add(0.51);

            // 4
            lstPercent.Add(0.75);
            //lstM15.Add(0.54);
            //lstM20.Add(0.56);
            //lstM25.Add(0.57);
            //lstM30.Add(0.59);
            //lstM35.Add(0.59);
            //lstM40.Add(0.60);
            //lstM40.Add(0.68);
            //lstM40.Add(0.74);
            //lstM40.Add(0.79);
            //lstM40.Add(0.84);
            //lstM40.Add(0.88);
            //lstM40.Add(0.92);
            //lstM40.Add(0.95);
            //lstM40.Add(0.98);
            //lstM40.Add(1.01);

            // 5
            lstPercent.Add(1.00);
            //lstM15.Add(0.6);
            //lstM20.Add(0.62);
            //lstM25.Add(0.64);
            //lstM30.Add(0.66);
            //lstM35.Add(0.67);

            // 6
            lstPercent.Add(1.25);
            //lstM15.Add(0.64);
            //lstM20.Add(0.67);
            //lstM25.Add(0.7);
            //lstM30.Add(0.71);
            //lstM35.Add(0.73);

            // 7
            lstPercent.Add(1.50);
            //lstM15.Add(0.68);
            //lstM20.Add(0.72);
            //lstM25.Add(0.74);
            //lstM30.Add(0.76);
            //lstM35.Add(0.78);

            // 8
            lstPercent.Add(1.75);
            //lstM15.Add(0.71);
            //lstM20.Add(0.75);
            //lstM25.Add(0.78);
            //lstM30.Add(0.80);
            //lstM35.Add(0.82);

            // 9
            lstPercent.Add(2.0);
            //lstM15.Add(0.71);
            //lstM20.Add(0.79);
            //lstM25.Add(0.82);
            //lstM30.Add(0.84);
            //lstM35.Add(0.86);

            // 10
            lstPercent.Add(2.25);
            //lstM15.Add(0.71);
            //lstM20.Add(0.81);
            //lstM25.Add(0.85);
            //lstM30.Add(0.88);
            //lstM35.Add(0.90);

            // 11
            lstPercent.Add(2.5);
            //lstM15.Add(0.71);
            //lstM20.Add(0.82);
            //lstM25.Add(0.88);
            //lstM30.Add(0.91);
            //lstM35.Add(0.93);

            // 12
            lstPercent.Add(2.75);
            //lstM15.Add(0.71);
            //lstM20.Add(0.82);
            //lstM25.Add(0.90);
            //lstM30.Add(0.94);
            //lstM35.Add(0.96);


            // 13
            lstPercent.Add(3.0);
            //lstM15.Add(0.71);
            //lstM20.Add(0.82);
            //lstM25.Add(0.92);
            //lstM30.Add(0.96);
            //lstM35.Add(0.99);

        



        }


        //public double GetTau_c(double percent,MType tp)
        //{
        //    switch (tp)
        //    {
        //        case MType.M15:
        //            {
                        
        //            }
        //            break;
        //        case MType.M20:
        //            break;
        //    }
        //}
        public double Get_M15(double percent)
        {
            double tau = 0.0d;

            lstM15.Add(0.28);
            lstM15.Add(0.35);
            lstM15.Add(0.46);
            lstM15.Add(0.54);
            lstM15.Add(0.60);
            lstM15.Add(0.64);
            lstM15.Add(0.68);
            lstM15.Add(0.71);
            lstM15.Add(0.71);
            lstM15.Add(0.71);
            lstM15.Add(0.71);
            lstM15.Add(0.71);
            lstM15.Add(0.71);

            for (int i = 0; i < lstPercent.Count; i++)
            {
                if (percent <= lstPercent[0])
                {
                    tau = lstM15[0]; break;
                }
                else if (percent <= lstPercent[i])
                {
                    tau = lstM15[i];break;
                }
                else if (percent >= lstPercent[lstPercent.Count-1])
                {
                    tau = lstM15[i];break;
                }
            }
            return tau;
        }

        public enum MType
        {
            M15 = 0,
            M20 = 1,
            M25 = 2,
            M30 = 3,
            M35 = 4,
            M40 = 5,
        }
    }
    public class ModificationFactor
    {
        //double gamma;
        //double prcnt;
        static List<double> lstGamma = null;
        static List<double> lstPercent = null;

        public ModificationFactor()
        {
        }
        public static double GetGamma(double percent)
        {
            double p1, p2, g1, g2;
            double gamma = 0.0;
            
            #region Set Values
            lstGamma = new List<double>();
            lstPercent = new List<double>();

            lstPercent.Add(0.0);
            lstGamma.Add(0.0);

            lstPercent.Add(0.1);
            lstGamma.Add(2.0);

            lstPercent.Add(0.2);
            lstGamma.Add(1.6);

            lstPercent.Add(0.3);
            lstGamma.Add(1.4);

            lstPercent.Add(0.4);
            lstGamma.Add(1.28);

            lstPercent.Add(0.6);
            lstGamma.Add(1.1);

            lstPercent.Add(0.8);
            lstGamma.Add(1.05);

            lstPercent.Add(1.0);
            lstGamma.Add(0.95);

            lstPercent.Add(1.2);
            lstGamma.Add(0.91);

            lstPercent.Add(1.4);
            lstGamma.Add(0.9);

            lstPercent.Add(1.6);
            lstGamma.Add(0.84);

            lstPercent.Add(1.8);
            lstGamma.Add(0.82);

            lstPercent.Add(2.0);
            lstGamma.Add(0.805);

            lstPercent.Add(2.2);
            lstGamma.Add(0.81);

            lstPercent.Add(2.4);
            lstGamma.Add(0.8);

            lstPercent.Add(2.6);
            lstGamma.Add(0.795);

            lstPercent.Add(2.8);
            lstGamma.Add(0.79);

            lstPercent.Add(3.0);
            lstGamma.Add(0.78);

            #endregion

            if (percent >= lstPercent[0] && percent <= lstPercent[lstPercent.Count - 1])
            {
                for (int i = 0; i < lstPercent.Count; i++)
                {
                    if (percent == lstPercent[i])
                    {
                        gamma = lstGamma[i]; break;
                    }
                    if (lstPercent[i] > percent)
                    {
                        p1 = lstPercent[i - 1];
                        p2 = lstPercent[i];
                        g1 = lstGamma[i - 1];
                        g2 = lstGamma[i];

                        gamma = ((g2 - g1) / (p2 - p1)) * (percent - p1) + g1;
                        break;
                    }
                }
            }
            return gamma;
        }

       
    }

}
