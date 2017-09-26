using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using HEADSNeed.ASTRA.ASTRAClasses;
using MovingLoadAnalysis;

using HEADSNeed.ASTRA.CadToAstra;


namespace HEADSNeed.ASTRA.ASTRAForms
{
    public partial class frm_Seismic_Suspension : Form
    {
        public frm_Seismic_Suspension()
        {
            InitializeComponent();
        }
        public dWrite_Seismic_Data Write_Seismic_Data;
        public dRunAnalysis RunAnalysis;
         
        List<int> list_mem_Nos = null;
        IASTRACAD iACad = null;
        public List<string> ASTRA_Data { get; set; }
        public TreeNode Node { get; set; }

        public string File_Seismic_Load { get; set; }
        List<LoadCaseDefinition> ALL_Loads { get; set; }

        public LoadCombinations LDC { get; set; }
        public bool IsRepeatLoad { get; set; }


        public frm_Seismic_Suspension(IASTRACAD iACAD, List<LoadCaseDefinition> all_loads)
        {
            InitializeComponent();
            list_mem_Nos = new List<int>();
            this.iACad = iACAD;
            ASTRA_Data = new List<string>();
            ALL_Loads = all_loads;
            LDC = new LoadCombinations();
            IsRepeatLoad = false;

            File_Seismic_Load = "";

        }

        private void frm_SeismicAnalysis_Load(object sender, EventArgs e)
        {
            cmb_FX.SelectedIndex = 0;
            cmb_FZ.SelectedIndex = 0;
            try
            {

                txt_smcs_jnts_1.Text = "1,17,33,49";
                txt_smcs_jnts_2.Text = "65,81,97,113";



                txt_sup_ract_jnts_1.Text = "1,17,33,49";
                txt_sup_ract_jnts_2.Text = "65,81,97,113";
                //Str_Analysis = new StructureMemberAnalysis(iACad.AstraDocument.AnalysisFileName);
                Get_Reactions();

            }
            catch (Exception exx) { }

        }

        private void Get_Reactions()
        {
            if(Str_Analysis == null)
                Str_Analysis = new StructureMemberAnalysis(iACad.AstraDocument.AnalysisFileName);

            //txt_smcs_jnts_1.Text = "8,24,40,56";
            //txt_smcs_jnts_2.Text = "72,88,104,120";

            string jnts1 = txt_sup_ract_jnts_1.Text.Replace(",", " ");
            MyList jnts = new MyList(jnts1, ' ');


            double vl = Str_Analysis.GetMaxReaction(1);
            string kStr = "";


            if (jnts.Count > 1)
            {
                vl = Str_Analysis.GetMaxReaction(jnts.GetInt(0));
                kStr = vl.ToString();

                for (int i = 1; i < jnts.Count; i++)
                {
                    vl = Str_Analysis.GetMaxReaction(jnts.GetInt(i));
                    kStr = kStr + "," + vl.ToString();

                }
            }
            txt_sup_ract_1.Text = kStr;


            //kStr = vl.ToString();

            //vl = Str_Analysis.GetMaxReaction(17);

            //kStr = kStr + "," + vl.ToString();

            //vl = Str_Analysis.GetMaxReaction(33);

            //kStr = kStr + "," + vl.ToString();

            //vl = Str_Analysis.GetMaxReaction(49);

            //kStr = kStr + "," + vl.ToString();

            //txt_sup_ract_1.Text = kStr;


            kStr = "";


            jnts1 = txt_sup_ract_jnts_2.Text.Replace(",", " ");
            jnts = new MyList(jnts1, ' ');


            if (jnts.Count > 1)
            {
                vl = Str_Analysis.GetMaxReaction(jnts.GetInt(0));
                kStr = vl.ToString();

                for (int i = 1; i < jnts.Count; i++)
                {
                    vl = Str_Analysis.GetMaxReaction(jnts.GetInt(i));
                    kStr = kStr + "," + vl.ToString();

                }
            }


            //vl = Str_Analysis.GetMaxReaction(65);

            //kStr = vl.ToString();


            //vl = Str_Analysis.GetMaxReaction(81);

            //kStr = kStr + "," + vl.ToString();

            //vl = Str_Analysis.GetMaxReaction(97);

            //kStr = kStr + "," + vl.ToString();


            //vl = Str_Analysis.GetMaxReaction(113);

            //kStr = kStr + "," + vl.ToString();
            txt_sup_ract_2.Text = kStr;



            Calculate_Forces();
        }



        public double SC
        {
            get
            {
                return MyList.StringToDouble(txt_SC.Text, 0.18);
            }
            set
            {
                txt_SC.Text = value.ToString();
            }
        }

        int Direction
        {
            get
            {

                //case 1: //  +X / +Z
                //case 2: //  -X / -Z
                //case 3: //  +X / -Z
                //case 4: //  -X / +Z

                if (cmb_FX.SelectedIndex == 0 && cmb_FZ.SelectedIndex == 0) return 1; // X+ / Z+
                if (cmb_FX.SelectedIndex == 1 && cmb_FZ.SelectedIndex == 1) return 2; // X- / Z-
                if (cmb_FX.SelectedIndex == 0 && cmb_FZ.SelectedIndex == 1) return 3; // X+ / Z-
                if (cmb_FX.SelectedIndex == 1 && cmb_FZ.SelectedIndex == 0) return 4; // X- / Z+

                return 1;

            }
        }
        StructureMemberAnalysis Str_Analysis;
        private void btn_create_Click(object sender, EventArgs e)
        {

            #region Chiranjit [2016 05 01
            /**/

            //if (dgv_combinations.RowCount == 0)
            //{
            //    MessageBox.Show("Add a Load Definition.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            //iACad.AstraDocument
            if (Str_Analysis == null)
                Str_Analysis = new StructureMemberAnalysis(iACad.AstraDocument.AnalysisFileName);

            //List<JointForce> jn_Force = Str_Analysis.list_joints;
            //List<double> jn_Force = new List<double>();
            List<JointForce> jn_Force = new List<JointForce>();


            MyList mlst = new MyList(txt_smcs_frcs_1.Text, ',');
            MyList mlst_jnt = new MyList(txt_smcs_jnts_1.Text, ',');

            int i = 0;
            JointForce jf = null;
            for (i = 0; i < mlst.Count; i++)
            {
                jf = new JointForce();
                jf.NodeNo = mlst_jnt.GetInt(i);
                jf.FY = mlst.GetDouble(i);
                jn_Force.Add(jf);
            }

            mlst = new MyList(txt_smcs_frcs_2.Text, ',');
            mlst_jnt = new MyList(txt_smcs_jnts_2.Text, ',');

            for (i = 0; i < mlst.Count; i++)
            {
                jf = new JointForce();
                jf.NodeNo = mlst_jnt.GetInt(i);
                jf.FY = mlst.GetDouble(i);
                jn_Force.Add(jf);
            }


            int loadno = ALL_Loads.Count + 1;

            List<string> jloads = new List<string>();


            int load_eqx = loadno;
            int load_eqz = loadno;

             

            int sign_x = 1, sign_z = 1;

                //case 1: //  +X / +Z
                //case 2: //  -X / -Z
                //case 3: //  +X / -Z
                //case 4: //  -X / +Z

            switch (Direction)
            {
                case 1: //+X/+Z

                    #region //+X/+Z
                    sign_x = 1;
                    sign_z = 1;

                    jloads.Add(string.Format("LOAD {0} SEISMIC LOAD +EQX", loadno));
                    jloads.Add(string.Format("JOINT LOAD"));
                    for (i = 0; i < jn_Force.Count; i++)
                    {
                        jloads.Add(string.Format("{0} {1} {2:f3}", jn_Force[i].NodeNo,
                            "FX",
                          sign_x * Math.Abs(jn_Force[i].FY)));
                    }


                    loadno++;
                    load_eqz = loadno;

                    jloads.Add(string.Format("LOAD {0} SEISMIC LOAD +EQZ", loadno));
                    jloads.Add(string.Format("JOINT LOAD"));
                    for (i = 0; i < jn_Force.Count; i++)
                    {
                        jloads.Add(string.Format("{0} {1} {2:f3}",
                            jn_Force[i].NodeNo,
                            "FZ",
                           sign_z * Math.Abs(jn_Force[i].FY)));
                    }
                    #endregion //+X/+Z

                    break;

                case 2: //-X/-Z

                    #region -X/-Z
                    sign_x = -1;
                    sign_z = -1;

                    jloads.Add(string.Format("LOAD {0} SEISMIC LOAD -EQX", loadno));
                    jloads.Add(string.Format("JOINT LOAD"));
                    for (i = 0; i < jn_Force.Count; i++)
                    {
                        jloads.Add(string.Format("{0} {1} {2:f3}", jn_Force[i].NodeNo,
                            "FX",
                          sign_x * Math.Abs(jn_Force[i].FY)));
                    }


                    loadno++;
                    load_eqz = loadno;

                    jloads.Add(string.Format("LOAD {0} SEISMIC LOAD -EQZ", loadno));
                    jloads.Add(string.Format("JOINT LOAD"));
                    for (i = 0; i < jn_Force.Count; i++)
                    {
                        jloads.Add(string.Format("{0} {1} {2:f3}",
                            jn_Force[i].NodeNo,
                            "FZ",
                           sign_z * Math.Abs(jn_Force[i].FY)));
                    }
                    #endregion //+X/+Z

                    break;


                case 3: //  +X/-Z

                    #region +X/-Z
                    sign_x = 1;
                    sign_z = -1;

                    jloads.Add(string.Format("LOAD {0} SEISMIC LOAD +EQX", loadno));
                    jloads.Add(string.Format("JOINT LOAD"));
                    for (i = 0; i < jn_Force.Count; i++)
                    {
                        jloads.Add(string.Format("{0} {1} {2:f3}", jn_Force[i].NodeNo,
                            "FX",
                          sign_x * Math.Abs(jn_Force[i].FY)));
                    }


                    loadno++;
                    load_eqz = loadno;

                    jloads.Add(string.Format("LOAD {0} SEISMIC LOAD -EQZ", loadno));
                    jloads.Add(string.Format("JOINT LOAD"));
                    for (i = 0; i < jn_Force.Count; i++)
                    {
                        jloads.Add(string.Format("{0} {1} {2:f3}",
                            jn_Force[i].NodeNo,
                            "FZ",
                           sign_z * Math.Abs(jn_Force[i].FY)));
                    }
                    #endregion //+X/+Z

                    break;

                case 4: //  -X/+Z

                    #region -X/+Z
                    sign_x = -1;
                    sign_z = 1;

                    jloads.Add(string.Format("LOAD {0} SEISMIC LOAD -EQX", loadno));
                    jloads.Add(string.Format("JOINT LOAD"));
                    for (i = 0; i < jn_Force.Count; i++)
                    {
                        jloads.Add(string.Format("{0} {1} {2:f3}", jn_Force[i].NodeNo,
                            "FX",
                          sign_x * Math.Abs(jn_Force[i].FY)));
                    }


                    loadno++;
                    load_eqz = loadno;

                    jloads.Add(string.Format("LOAD {0} SEISMIC LOAD +EQZ", loadno));
                    jloads.Add(string.Format("JOINT LOAD"));
                    for (i = 0; i < jn_Force.Count; i++)
                    {
                        jloads.Add(string.Format("{0} {1} {2:f3}",
                            jn_Force[i].NodeNo,
                            "FZ",
                           sign_z * Math.Abs(jn_Force[i].FY)));
                    }
                    #endregion //+X/+Z

                    break;

            }
             
            LDC.LoadNo = loadno;
            LDC.Name = "SEISMIC LOAD";
            LDC.LoadCases.Clear();
            LDC.Factors.Clear();
            //for (i = 0; i < dgv_combinations.RowCount; i++)
            //{
            //    LDC.LoadCases.Add(MyStrings.StringToInt(dgv_combinations[0, i].Value.ToString().ToUpper().Replace("LOAD CASE ", ""), 0));
            //    LDC.Factors.Add(MyStrings.StringToDouble(dgv_combinations[1, i].Value.ToString(), 0));


            //}


            LDC.LoadCases.Add(1);
            LDC.Factors.Add(1.0);
        
            LDC.Set_Combination();


            List<string> load_combs = new List<string>();

            if (Direction == 1 || Direction == 3 || Direction == 5)
            {
                loadno++;
                load_combs.Add(string.Format("LOAD COMB {0} {1} (DL+EQX)", loadno, textBox1.Text));
                load_combs.Add(string.Format("{0} {1} {2}", LDC.Data, load_eqx, textBox1.Text));
            }

            if (Direction == 2 || Direction == 4)
            {
                loadno++;
                //load_combs.Add(string.Format("LOAD COMB {0} SEISMIC COMB", loadno));
                load_combs.Add(string.Format("LOAD COMB {0} {1} (DL-EQX)", loadno, textBox1.Text));
                //load_combs.Add(string.Format("{0} {1} -{2}", LDC.Data, load_eqx, textBox1.Text));
                load_combs.Add(string.Format("{0} {1} {2}", LDC.Data, load_eqx, textBox1.Text));
            }


            if (Direction == 5)
            {
                loadno++;
                //load_combs.Add(string.Format("LOAD COMB {0} SEISMIC COMB", loadno));
                load_combs.Add(string.Format("LOAD COMB {0} {1} (DL-EQX)", loadno, textBox1.Text));
                load_combs.Add(string.Format("{0} {1} {2}", LDC.Data, load_eqz, textBox1.Text));
            }


            if (Direction == 1 || Direction == 4)
            {
                loadno++;
                //load_combs.Add(string.Format("LOAD COMB {0} SEISMIC COMB", loadno));
                load_combs.Add(string.Format("LOAD COMB {0} {1} (DL+EQZ)", loadno, textBox1.Text));
                load_combs.Add(string.Format("{0} {1} {2}", LDC.Data, load_eqz, textBox1.Text));
                //load_combs.Add(LDC.Data);
            }

            if (Direction == 2 || Direction == 3)
            {
                loadno++;
                //load_combs.Add(string.Format("LOAD COMB {0} SEISMIC COMB", loadno));
                load_combs.Add(string.Format("LOAD COMB {0} {1} (DL-EQZ)", loadno, textBox1.Text));
                //load_combs.Add(string.Format("{0} {1} -{2}", LDC.Data, load_eqz, textBox1.Text));
                load_combs.Add(string.Format("{0} {1} {2}", LDC.Data, load_eqz, textBox1.Text));
                //load_combs.Add(LDC.Data);
            }
            if (Direction == 6)
            {
                loadno++;
                //load_combs.Add(string.Format("LOAD COMB {0} SEISMIC COMB", loadno));
                load_combs.Add(string.Format("LOAD COMB {0} {1} (DL+EQZ)", loadno, textBox1.Text));
                load_combs.Add(string.Format("{0} {1} {2}", LDC.Data, load_eqx, textBox1.Text));

                loadno++;
                //load_combs.Add(string.Format("LOAD COMB {0} SEISMIC COMB", loadno));
                load_combs.Add(string.Format("LOAD COMB {0} {1} (DL-EQZ)", loadno, textBox1.Text));
                load_combs.Add(string.Format("{0} {1} {2}", LDC.Data, load_eqz, textBox1.Text));
                //load_combs.Add(LDC.Data);

            }

            string file_name = File_Seismic_Load;


            if (file_name == "")
            {
                file_name = Path.Combine(Path.GetDirectoryName(Str_Analysis.Analysis_File), "Seismic_Analysis");
                if (!Directory.Exists(file_name))
                    Directory.CreateDirectory(file_name);
                file_name = Path.Combine(file_name, "Seismic_Analysis.TXT");
            }
            File.WriteAllText(file_name, "Write Data");

            if (Write_Seismic_Data != null)
            {
                Write_Seismic_Data(jloads, load_combs, file_name);
            }
            /**/
            #endregion Chiranjit [2016 05 01

            btn_process.Enabled = true;
            btn_view.Enabled = true;
        }

        private void btn_process_Click(object sender, EventArgs e)
        {
            if (File.Exists(File_Seismic_Load))
            {
                RunAnalysis(File_Seismic_Load);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btn_view_Click(object sender, EventArgs e)
        {
            if (File.Exists(File_Seismic_Load))
            {
                System.Diagnostics.Process.Start(File_Seismic_Load);
            }
        }

        private void txt_SC_TextChanged(object sender, EventArgs e)
        {
            Calculate_Forces();
        }

        private void Calculate_Forces()
        {

            MyList mlist = new MyList(txt_sup_ract_1.Text, ',');

            try
            {
                string kStr = "";

                for (int i = 0; i < mlist.Count; i++)
                {
                    if (i == 0)
                    {
                        kStr = (mlist.GetDouble(i) * SC).ToString("f3");
                    }
                    else
                    {
                        kStr = kStr + "," + (mlist.GetDouble(i) * SC).ToString("f3");
                    }
                }
                txt_smcs_frcs_1.Text = kStr;

                kStr = "";
                mlist = new MyList(txt_sup_ract_2.Text, ',');

                for (int i = 0; i < mlist.Count; i++)
                {
                    if (i == 0)
                    {
                        kStr = (mlist.GetDouble(i) * SC).ToString("f3");
                    }
                    else
                    {
                        kStr = kStr + "," + (mlist.GetDouble(i) * SC).ToString("f3");
                    }
                }
                txt_smcs_frcs_2.Text = kStr;

            }
            catch (Exception exx) { }
        }

        private void btn_help_Click(object sender, EventArgs e)
        {
            string seismic_excel = Path.Combine(Application.StartupPath, @"DESIGN\Siesmic Coefficient\Calculation for Siesmic Coefficient.xls");
            if (File.Exists(seismic_excel))
            {
                System.Diagnostics.Process.Start(seismic_excel);
            }
        }

        private void btn_get_jnt_reac_Click(object sender, EventArgs e)
        {
            Get_Reactions();
            
        }


    }

}
