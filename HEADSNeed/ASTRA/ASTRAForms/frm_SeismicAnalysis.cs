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
    public delegate void dWrite_Seismic_Data(List<string> LoadData, List<string> Load_Comb, string file_name);
    public delegate bool dRunAnalysis(string fName);

    public partial class frm_SeismicAnalysis : Form
    {
        public dWrite_Seismic_Data Write_Seismic_Data;
        public dRunAnalysis RunAnalysis;
        public frm_SeismicAnalysis()
        {
            InitializeComponent();
        }

        List<int> list_mem_Nos = null;
        IASTRACAD iACad = null;
        public List<string> ASTRA_Data { get; set; }
        public TreeNode Node { get; set; }

        public string File_Seismic_Load { get; set; }
        List<LoadCaseDefinition> ALL_Loads { get; set; }

        public LoadCombinations LDC { get; set; }
        public bool IsRepeatLoad { get; set; }


        public frm_SeismicAnalysis(IASTRACAD iACAD, List<LoadCaseDefinition> all_loads)
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

            txt_load_no.Text = (ALL_Loads.Count + 1).ToString();
            if (ALL_Loads.Count > 0)
            {
                lst_loadcases.Items.Clear();
                for (int i = 0; i < ALL_Loads.Count; i++)
                {
                    if (!ALL_Loads[i].Is_Load_Combination)
                    {
                        if (LDC.Count > 0)
                        {
                            if (!LDC.LoadCases.Contains(ALL_Loads[i].LoadNo))
                                lst_loadcases.Items.Add(ALL_Loads[i].ToString());
                        }
                        else
                            lst_loadcases.Items.Add(ALL_Loads[i].ToString());
                    }
                }
            }

            if (LDC.Count > 0)
            {
                txt_load_no.Text = LDC.LoadNo.ToString();
                txt_load_name.Text = LDC.Name;
                dgv_combinations.Rows.Clear();

                for (int i = 0; i < LDC.Count; i++)
                {
                    dgv_combinations.Rows.Add("Load Case " + LDC.LoadCases[i], LDC.Factors[i].ToString("f3"));
                }
                btn_process.Text = "Change";
            }
            else
            {
                txt_load_no.Text = (ALL_Loads.Count + 1).ToString();
                if (lst_loadcases.Items.Count > 0)
                    lst_loadcases.SelectedIndex = 0;
            }
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            MyStrings mlist = null;

            List<int> indices = new List<int>();
            List<int> loadNo = new List<int>();

            if (btn.Name == btn_insert.Name)
            {

                #region Chiranjit [2015 05 14]
                //Due to Load combination more than 3 is not acceptable
                if (dgv_combinations.RowCount > 0)
                {
                    MessageBox.Show(this, "More than one Load Definition is not acceptable.", "ASTRA");
                    return;
                }
                #endregion Chiranjit [2015 05 14]

                for (int i = 0; i < lst_loadcases.SelectedIndices.Count; i++)
                {
                    indices.Add(lst_loadcases.SelectedIndices[i]);
                    mlist = new MyStrings(lst_loadcases.Items[lst_loadcases.SelectedIndices[i]].ToString(), ':');

                    loadNo.Add(mlist.GetInt(0));

                }

                indices.Sort();
                indices.Reverse();

                for (int i = 0; i < indices.Count; i++)
                {
                    lst_loadcases.Items.RemoveAt(indices[i]);
                }
                

                for (int i = 0; i < ALL_Loads.Count; i++)
                {
                    if (loadNo.Contains(ALL_Loads[i].LoadNo))
                        dgv_combinations.Rows.Add("Load Case " + ALL_Loads[i].LoadNo, "1.0");
                }
            }
            else if (btn.Name == btn_insert_all.Name)
            {
                #region Chiranjit [2015 05 14]
                //Due to Load combination more than 3 is not acceptable
                if (dgv_combinations.RowCount > 0)
                {
                    MessageBox.Show(this, "More than one Load Definition is not acceptable.", "ASTRA");
                    return;
                }
                #endregion Chiranjit [2015 05 14]

                for (int i = 0; i < lst_loadcases.Items.Count; i++)
                {
                    indices.Add(i);
                    mlist = new MyStrings(lst_loadcases.Items[i].ToString(), ':');

                    loadNo.Add(mlist.GetInt(0));
                }

                indices.Sort();
                indices.Reverse();
                for (int i = 0; i < indices.Count; i++)
                {
                    lst_loadcases.Items.RemoveAt(indices[i]);
                }

                for (int i = 0; i < ALL_Loads.Count; i++)
                {
                    if (loadNo.Contains(ALL_Loads[i].LoadNo))
                        dgv_combinations.Rows.Add("Load Case " + ALL_Loads[i].LoadNo, "1.0");
                }

            }
            else if (btn.Name == btn_remove.Name)
            {
                int r = -1;
                for (int i = 0; i < dgv_combinations.SelectedCells.Count; i++)
                {
                    r = dgv_combinations.SelectedCells[i].RowIndex;
                    indices.Add(r);
                    mlist = new MyStrings(dgv_combinations[0, r].Value.ToString().ToUpper().Replace("LOAD CASE ", ""), ' ');
                    loadNo.Add(mlist.GetInt(0));
                }
                for (int i = 0; i < indices.Count; i++)
                {
                    dgv_combinations.Rows.RemoveAt(indices[i]);
                }


                for (int i = 0; i < ALL_Loads.Count; i++)
                {
                    if (loadNo.Contains(ALL_Loads[i].LoadNo))
                        lst_loadcases.Items.Add(ALL_Loads[i].ToString());
                }

            }
            else if (btn.Name == btn_remove_all.Name)
            {

                int r = -1;
                for (int i = 0; i < dgv_combinations.RowCount; i++)
                {
                    r = i;
                    indices.Add(r);
                    mlist = new MyStrings(dgv_combinations[0, r].Value.ToString().ToUpper().Replace("LOAD CASE ", ""), ' ');
                    loadNo.Add(mlist.GetInt(0));
                }
                dgv_combinations.Rows.Clear();



                for (int i = 0; i < ALL_Loads.Count; i++)
                {
                    if (loadNo.Contains(ALL_Loads[i].LoadNo))
                        lst_loadcases.Items.Add(ALL_Loads[i].ToString());
                }
            }

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
                if (btn_dir_1.Checked) return 1;
                if (btn_dir_2.Checked) return 2;
                if (btn_dir_3.Checked) return 3;
                if (btn_dir_4.Checked) return 4;
                if (btn_dir_5.Checked) return 5;
                if (btn_dir_6.Checked) return 6;

                return 6;

            }
        }
        StructureMemberAnalysis Str_Analysis;
        private void btn_create_Click(object sender, EventArgs e)
        {

            if (dgv_combinations.RowCount == 0)
            {
                MessageBox.Show("Add a Load Definition.", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //iACad.AstraDocument
            if (Str_Analysis == null)
                Str_Analysis = new StructureMemberAnalysis(iACad.AstraDocument.AnalysisFileName);

            List<JointForce> jn_Force = Str_Analysis.list_joints;

            int loadno = ALL_Loads.Count + 1;

            List<string> jloads = new List<string>();
            int i = 0;


            int load_eqx = loadno;
            int load_eqz = loadno;


            #region Chiranjit [2015 05 27]
            #endregion Chiranjit [2015 05 27]


            int sign_x = 1, sign_z = 1;


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
                          sign_x * Math.Abs(jn_Force[i].FY) * SC));
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
                           sign_z * Math.Abs(jn_Force[i].FY) * SC));
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
                          sign_x * Math.Abs(jn_Force[i].FY) * SC));
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
                           sign_z * Math.Abs(jn_Force[i].FY) * SC));
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
                          sign_x * Math.Abs(jn_Force[i].FY) * SC));
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
                           sign_z * Math.Abs(jn_Force[i].FY) * SC));
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
                          sign_x * Math.Abs(jn_Force[i].FY) * SC));
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
                           sign_z * Math.Abs(jn_Force[i].FY) * SC));
                    }
                    #endregion //+X/+Z

                    break;


                case 5: //  +X/-X

                    #region +X/-X
                    sign_x = 1;
                    sign_z = -1;

                    jloads.Add(string.Format("LOAD {0} SEISMIC LOAD +EQX", loadno));
                    jloads.Add(string.Format("JOINT LOAD"));
                    for (i = 0; i < jn_Force.Count; i++)
                    {
                        jloads.Add(string.Format("{0} {1} {2:f3}", jn_Force[i].NodeNo,
                            "FX",
                          sign_x * Math.Abs(jn_Force[i].FY) * SC));
                    }


                    loadno++;
                    load_eqz = loadno;

                    jloads.Add(string.Format("LOAD {0} SEISMIC LOAD -EQX", loadno));
                    jloads.Add(string.Format("JOINT LOAD"));
                    for (i = 0; i < jn_Force.Count; i++)
                    {
                        jloads.Add(string.Format("{0} {1} {2:f3}",
                            jn_Force[i].NodeNo,
                            "FX",
                           sign_z * Math.Abs(jn_Force[i].FY) * SC));
                    }
                    #endregion //+X/+Z

                    break;


                case 6: //  +Z/-Z

                    #region   +Z/-Z
                    sign_x = 1;
                    sign_z = -1;

                    jloads.Add(string.Format("LOAD {0} SEISMIC LOAD +EQZ", loadno));
                    jloads.Add(string.Format("JOINT LOAD"));
                    for (i = 0; i < jn_Force.Count; i++)
                    {
                        jloads.Add(string.Format("{0} {1} {2:f3}", jn_Force[i].NodeNo,
                            "FZ",
                          sign_x * Math.Abs(jn_Force[i].FY) * SC));
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
                           sign_z * Math.Abs(jn_Force[i].FY) * SC));
                    }
                    #endregion //+X/+Z

                    break;


            }

            //switch (Direction)
            //{
            //    case 1: //+X/+Z
            //        jloads.Add(string.Format("LOAD {0} SEISMIC LOAD EQX", loadno));
            //        jloads.Add(string.Format("JOINT LOAD"));
            //        for (i = 0; i < jn_Force.Count; i++)
            //        {
            //            jloads.Add(string.Format("{0} {1} {2:f3}", jn_Force[i].NodeNo,
            //                "FX",
            //               Math.Abs(jn_Force[i].FY) * SC));
            //        }


            //        loadno++;
            //        load_eqz = loadno;

            //        jloads.Add(string.Format("LOAD {0} SEISMIC LOAD EQZ", loadno));
            //        jloads.Add(string.Format("JOINT LOAD"));
            //        for (i = 0; i < jn_Force.Count; i++)
            //        {
            //            jloads.Add(string.Format("{0} {1} {2:f3}",
            //                jn_Force[i].NodeNo,
            //                "FZ",
            //                Math.Abs(jn_Force[i].FY) * SC));
            //        }
            //        break;
            //}







            //if (Direction != 6)
            //{
            //    jloads.Add(string.Format("LOAD {0} SEISMIC LOAD EQX", loadno));
            //    jloads.Add(string.Format("JOINT LOAD"));
            //    for (i = 0; i < jn_Force.Count; i++)
            //    {
            //        jloads.Add(string.Format("{0} {1} {2:f3}", jn_Force[i].NodeNo,
            //            "FX",
            //            jn_Force[i].FY * SC));
            //    }
            //}


            //if (Direction != 5)
            //{
            //    loadno++;
            //    load_eqz = loadno;

            //    jloads.Add(string.Format("LOAD {0} SEISMIC LOAD EQZ", loadno));
            //    jloads.Add(string.Format("JOINT LOAD"));
            //    for (i = 0; i < jn_Force.Count; i++)
            //    {
            //        jloads.Add(string.Format("{0} {1} {2:f3}",
            //            jn_Force[i].NodeNo,
            //            "FZ",
            //            jn_Force[i].FY * SC));
            //    }
            //}
            //jloads.Add(string.Format("{0} {1} {2:f3}", jn_Force[i].NodeNo, dir, jn_Force[i].FY * sc));

            //LDC.LoadNo = MyStrings.StringToInt(txt_load_no.Text, 0);
            //loadno++;
            LDC.LoadNo = loadno;
            LDC.Name = txt_load_name.Text;
            LDC.LoadCases.Clear();
            LDC.Factors.Clear();
            for (i = 0; i < dgv_combinations.RowCount; i++)
            {
                LDC.LoadCases.Add(MyStrings.StringToInt(dgv_combinations[0, i].Value.ToString().ToUpper().Replace("LOAD CASE ", ""), 0));
                LDC.Factors.Add(MyStrings.StringToDouble(dgv_combinations[1, i].Value.ToString(), 0));
            }

            //LDC.LoadCases.Add(LDC.LoadNo);
            //LDC.Factors.Add(MyStrings.StringToDouble(textBox1.Text, 0));

            LDC.Set_Combination();


            List<string> load_combs = new List<string>();

            //load_combs.Add(string.Format("LOAD COMB {0} SEISMIC COMB", loadno));
            if (Direction == 1 || Direction == 3 || Direction == 5)
            {
                loadno++;
                load_combs.Add(string.Format("LOAD COMB {0} {1} (DL+EQX)", loadno, textBox1.Text));
                load_combs.Add(string.Format("{0} {1} {2}", LDC.Data, load_eqx, textBox1.Text));
                //load_combs.Add(LDC.Data);
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


            btn_process.Enabled = true;
            btn_view.Enabled = true;
        }
        private void btn_create_Click_1(object sender, EventArgs e)
        {
            //iACad.AstraDocument
            StructureMemberAnalysis Str_Analysis = new StructureMemberAnalysis(iACad.AstraDocument.AnalysisFileName);

            List<JointForce> jn_Force = Str_Analysis.list_joints;

            int loadno = ALL_Loads.Count + 1;

            List<string> jloads = new List<string>();
            int i = 0;


            int load_eqx = loadno;
            int load_eqz = loadno;

            if (Direction != 6)
            {
                jloads.Add(string.Format("LOAD {0} SEISMIC LOAD EQX", loadno));
                jloads.Add(string.Format("JOINT LOAD"));
                for (i = 0; i < jn_Force.Count; i++)
                {
                    jloads.Add(string.Format("{0} {1} {2:f3}", jn_Force[i].NodeNo,
                        "FX",
                        jn_Force[i].FY * SC));
                }
            }


            if (Direction != 5)
            {
                loadno++;
                load_eqz = loadno;

                jloads.Add(string.Format("LOAD {0} SEISMIC LOAD EQZ", loadno));
                jloads.Add(string.Format("JOINT LOAD"));
                for (i = 0; i < jn_Force.Count; i++)
                {
                    jloads.Add(string.Format("{0} {1} {2:f3}",
                        jn_Force[i].NodeNo,
                        "FZ",
                        jn_Force[i].FY * SC));
                }
            }
            //jloads.Add(string.Format("{0} {1} {2:f3}", jn_Force[i].NodeNo, dir, jn_Force[i].FY * sc));

            //LDC.LoadNo = MyStrings.StringToInt(txt_load_no.Text, 0);
            //loadno++;
            LDC.LoadNo = loadno;
            LDC.Name = txt_load_name.Text;
            LDC.LoadCases.Clear();
            LDC.Factors.Clear();
            for (i = 0; i < dgv_combinations.RowCount; i++)
            {
                LDC.LoadCases.Add(MyStrings.StringToInt(dgv_combinations[0, i].Value.ToString().ToUpper().Replace("LOAD CASE ", ""), 0));
                LDC.Factors.Add(MyStrings.StringToDouble(dgv_combinations[1, i].Value.ToString(), 0));
            }

            //LDC.LoadCases.Add(LDC.LoadNo);
            //LDC.Factors.Add(MyStrings.StringToDouble(textBox1.Text, 0));

            LDC.Set_Combination();


            List<string> load_combs = new List<string>();

            //load_combs.Add(string.Format("LOAD COMB {0} SEISMIC COMB", loadno));
            if (Direction == 1 || Direction == 3 || Direction == 5)
            {
                loadno++;
                load_combs.Add(string.Format("LOAD COMB {0} {1} (DL+EQX)", loadno, textBox1.Text));
                load_combs.Add(string.Format("{0} {1} {2}", LDC.Data, load_eqx, textBox1.Text));
                //load_combs.Add(LDC.Data);
            }

            if (Direction == 2 || Direction == 4 || Direction == 5)
            {
                loadno++;
                //load_combs.Add(string.Format("LOAD COMB {0} SEISMIC COMB", loadno));
                load_combs.Add(string.Format("LOAD COMB {0} {1} (DL-EQX)", loadno, textBox1.Text));
                load_combs.Add(string.Format("{0} {1} -{2}", LDC.Data, load_eqx, textBox1.Text));
            }


            if (Direction == 1 || Direction == 4 || Direction == 6)
            {
                loadno++;
                //load_combs.Add(string.Format("LOAD COMB {0} SEISMIC COMB", loadno));
                load_combs.Add(string.Format("LOAD COMB {0} {1} (DL+EQZ)", loadno, textBox1.Text));
                load_combs.Add(string.Format("{0} {1} {2}", LDC.Data, load_eqz, textBox1.Text));
                //load_combs.Add(LDC.Data);
            }

            if (Direction == 2 || Direction == 3 || Direction == 6)
            {
                loadno++;
                //load_combs.Add(string.Format("LOAD COMB {0} SEISMIC COMB", loadno));
                load_combs.Add(string.Format("LOAD COMB {0} {1} (DL-EQZ)", loadno, textBox1.Text));
                load_combs.Add(string.Format("{0} {1} -{2}", LDC.Data, load_eqz, textBox1.Text));
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
                RunAnalysis(file_name);
            }

            this.DialogResult = DialogResult.OK;

            this.Close();
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


    }
}
