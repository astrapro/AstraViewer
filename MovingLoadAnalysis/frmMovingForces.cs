using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
//using System.Threading;


using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdPrimaries;

using MovingLoadAnalysis.DataStructure;
using AstraFunctionOne.BridgeDesign.SteelTruss;


namespace MovingLoadAnalysis
{
    public partial class frmMovingForces : Form
    {

        RKASTRA astra = null;
        vdDocument doc = null;
        Timer tmr = null;
        int load_case = 0;
        //Chiranjit [2011 09 26]  //This class is used to Get for forces of a member
        public static StructureMemberAnalysis AnalysisResult { get; set; }

        public frmMovingForces(vdDocument vDoc,string file_name, int selected_load_case)
        {
            InitializeComponent();
            doc = vDoc;
            load_case = selected_load_case;
            try
            {
                file_name = Path.Combine(Path.GetDirectoryName(file_name), "ANALYSIS_REP.TXT");
                //if (AnalysisResult == null)
                //    AnalysisResult = new SteelTrussMemberAnalysis(file_name);
                //else
                //{
                //    if (AnalysisResult.Analysis_File != file_name)
                //        AnalysisResult = new SteelTrussMemberAnalysis(file_name);
                //}
                AnalysisResult = new StructureMemberAnalysis(file_name);
            }
            catch (Exception ex) { AnalysisResult = null; }
        }

        private void frmMovingForces_Load(object sender, EventArgs e)
        {
            if (AnalysisResult != null)
            {
                btn_get_forces.Visible = true;
                btn_get_forces.Text = "Show Member Force Data";
                cmb_load_case.Items.Clear();
                cmb_member_no.Items.Clear();
                if (AnalysisResult.list_beams.Count != 0)
                    for (int i = 0; i < AnalysisResult.list_beams.Count; i++)
                    {
                        if (!cmb_load_case.Items.Contains(AnalysisResult.list_beams[i].LoadNo))
                            cmb_load_case.Items.Add(AnalysisResult.list_beams[i].LoadNo);

                        //if (!cmb_member_no.Items.Contains(AnalysisResult.list_beams[i].BeamNo))
                        //    cmb_member_no.Items.Add(AnalysisResult.list_beams[i].BeamNo);
                    }

                if (AnalysisResult.list_analysis.Count != 0)
                    for (int i = 0; i < AnalysisResult.list_analysis.Count; i++)
                    {
                        //if (!cmb_load_case.Items.Contains(AnalysisResult.list_beams[i].LoadNo))
                        //    cmb_load_case.Items.Add(AnalysisResult.list_beams[i].LoadNo);

                        if (!cmb_member_no.Items.Contains(AnalysisResult.list_analysis[i].UserMemberNo))
                        {
                            cmb_member_no.Items.Add(AnalysisResult.list_analysis[i].UserMemberNo + " : " + AnalysisResult.list_analysis[i].AstraMemberType.ToString());
                        }
                    }
            }

            

            if (cmb_member_no.Items.Count > 0)
                cmb_member_no.SelectedIndex = 0;
            if (cmb_load_case.Items.Count > 0)
                cmb_load_case.SelectedIndex = load_case;


            lbl_ton1.Text = AnalysisResult.MassUnit;
            lbl_ton2.Text = AnalysisResult.MassUnit;
            lbl_ton3.Text = AnalysisResult.MassUnit;
            lbl_ton4.Text = AnalysisResult.MassUnit;
            lbl_ton5.Text = AnalysisResult.MassUnit;
            lbl_ton6.Text = AnalysisResult.MassUnit;
            lbl_ton7.Text = AnalysisResult.MassUnit;
            lbl_ton8.Text = AnalysisResult.MassUnit;
            lbl_ton9.Text = AnalysisResult.MassUnit;
            lbl_ton10.Text = AnalysisResult.MassUnit;
            lbl_ton11.Text = AnalysisResult.MassUnit;

            lbl_ton_m1.Text = AnalysisResult.MassUnit + "-" + AnalysisResult.LengthUnit;
            lbl_ton_m2.Text = AnalysisResult.MassUnit + "-" + AnalysisResult.LengthUnit;
            lbl_ton_m3.Text = AnalysisResult.MassUnit + "-" + AnalysisResult.LengthUnit;
            lbl_ton_m4.Text = AnalysisResult.MassUnit + "-" + AnalysisResult.LengthUnit;
            lbl_ton_m5.Text = AnalysisResult.MassUnit + "-" + AnalysisResult.LengthUnit;
            lbl_ton_m6.Text = AnalysisResult.MassUnit + "-" + AnalysisResult.LengthUnit;
            lbl_ton_m7.Text = AnalysisResult.MassUnit + "-" + AnalysisResult.LengthUnit;
            lbl_ton_m8.Text = AnalysisResult.MassUnit + "-" + AnalysisResult.LengthUnit;
            lbl_ton_m9.Text = AnalysisResult.MassUnit + "-" + AnalysisResult.LengthUnit;
            
            lbl_ton_sqm1.Text = AnalysisResult.MassUnit + "/SQ." + AnalysisResult.LengthUnit;
            lbl_ton_sqm2.Text = AnalysisResult.MassUnit + "/SQ." + AnalysisResult.LengthUnit;

        }

        private void btn_get_forces_Click(object sender, EventArgs e)
        {
            string sf = Path.Combine(Path.GetDirectoryName(AnalysisResult.Analysis_File), "MemberForces.txt");
            if (File.Exists(sf))
            {
                System.Diagnostics.Process.Start(sf);
            }
        }

        void Show_forces()
        {

            double max_R1, max_R2, max_R3, max_M1, max_M2, max_M3;
            double max_loadcase_R1, max_loadcase_R2, max_loadcase_R3, max_loadcase_M1, max_loadcase_M2, max_loadcase_M3;

            double max_truss_stress, max_truss_force;
            double max_truss_stress_loadcase, max_truss_force_loadcase;
            double truss_stress, truss_force;


            max_truss_force = 0.0;
            max_truss_stress = 0.0;

            max_truss_stress_loadcase = 0.0;
            max_truss_force_loadcase = 0.0;

            truss_stress = 0.0;
            truss_force = 0.0;

            eAstraMemberType memType = eAstraMemberType.BEAM;
            //max_loadcase_M1 = double.MinValue;
            //max_loadcase_M2 = double.MinValue;
            //max_loadcase_M3 = double.MinValue;
            //max_loadcase_R1 = double.MinValue;
            //max_loadcase_R2 = double.MinValue;
            //max_loadcase_R3 = double.MinValue;



            //max_M1 = double.MinValue;
            //max_M2 = double.MinValue;
            //max_M3 = double.MinValue;
            //max_R1 = double.MinValue;
            //max_R2 = double.MinValue;
            //max_R3 = double.MinValue;


            max_loadcase_M1 = 0;
            max_loadcase_M2 = 0;
            max_loadcase_M3 = 0;
            max_loadcase_R1 = 0;
            max_loadcase_R2 = 0;
            max_loadcase_R3 = 0;



            max_M1 = 0;
            max_M2 = 0;
            max_M3 = 0;
            max_R1 = 0;
            max_R2 = 0;
            max_R3 = 0;

            bool truss = false;

            AnalysisData adata = AnalysisResult.MemberAnalysis[MemberNo] as AnalysisData;

            if (adata == null) return;
            memType = adata.AstraMemberType;

            txt_mem_type.Text = memType.ToString();

            if (memType == eAstraMemberType.CABLE ||
                memType == eAstraMemberType.TRUSS)
            {
                grb_truss.Text = memType.ToString() + " Member";
            }
            //for (int i = 0; i < AnalysisResult.list_analysis.Count; i++)
            //{
            //    if (AnalysisResult.list_analysis[i].UserMemberNo == MemberNo)
            //    {
            //        adata = AnalysisResult.list_analysis[i];
            //        memType = AnalysisResult.list_analysis[i].AstraMemberType;
            //        break;
            //    }
            //}

            //grb_beam.Visible = !truss;
            //grb_truss.Visible = truss;
            //grb_truss.Location = grb_beam.Location;
            try
            {
                Member mem = AnalysisResult.Analysis.Members.GetMember(MemberNo);
                grb_Start_Node.Text = "Start Node No : " + mem.StartNode.NodeNo;
                grb_End_Node.Text = "End Node No : " + mem.EndNode.NodeNo;
            }
            catch (Exception ex) { }
            ClearFields();
            if (memType == eAstraMemberType.TRUSS)
            {
                #region For Truss Member
                for (int i = 0; i < AnalysisResult.list_trusses.Count; i++)
                {
                    if (AnalysisResult.list_trusses[i].TrussMemberNo == adata.AstraMemberNo)
                    {

                        if (AnalysisResult.list_trusses[i].LoadNo == LoadCase)
                        {
                            //propertyGrid1.SelectedObject = AnalysisResult.list_trusses[i];
                            txt_mem_stress.Text = AnalysisResult.list_trusses[i].Stress.ToString("0.000");
                            txt_mem_force.Text = AnalysisResult.list_trusses[i].Force.ToString("0.000");
                        }

                        if (Math.Abs(AnalysisResult.list_trusses[i].Stress) >
                            Math.Abs(max_truss_stress))
                        {
                            max_truss_stress = AnalysisResult.list_trusses[i].Stress;
                            max_truss_stress_loadcase = AnalysisResult.list_trusses[i].LoadNo;
                        }
                        if (Math.Abs(AnalysisResult.list_trusses[i].Force) >
                            Math.Abs(max_truss_force))
                        {
                            max_truss_force = AnalysisResult.list_trusses[i].Force;
                            max_truss_force_loadcase = AnalysisResult.list_trusses[i].LoadNo;
                        }
                    }
                }

                txt_max_stress.Text = max_truss_stress.ToString("0.000");
                txt_max_stress_load_case.Text = max_truss_stress_loadcase.ToString("0");
                txt_max_force.Text = max_truss_force.ToString("0.000");
                txt_max_force_load_case.Text = max_truss_force_loadcase.ToString("0");



                #endregion  For Truss Member
            }
            else if (memType == eAstraMemberType.CABLE)
            {
                #region For Cable Member
                for (int i = 0; i < AnalysisResult.list_cables.Count; i++)
                {
                    if (AnalysisResult.list_cables[i].TrussMemberNo == adata.AstraMemberNo)
                    {

                        if (AnalysisResult.list_cables[i].LoadNo == LoadCase)
                        {
                            //propertyGrid1.SelectedObject = AnalysisResult.list_cables[i];
                            txt_mem_stress.Text = AnalysisResult.list_cables[i].Stress.ToString("E4");
                            txt_mem_force.Text = AnalysisResult.list_cables[i].Force.ToString("E4");
                        }

                        if (Math.Abs(AnalysisResult.list_cables[i].Stress) >
                            Math.Abs(max_truss_stress))
                        {
                            max_truss_stress = AnalysisResult.list_cables[i].Stress;
                            max_truss_stress_loadcase = AnalysisResult.list_cables[i].LoadNo;
                        }
                        if (Math.Abs(AnalysisResult.list_cables[i].Force) >
                            Math.Abs(max_truss_force))
                        {
                            max_truss_force = AnalysisResult.list_cables[i].Force;
                            max_truss_force_loadcase = AnalysisResult.list_cables[i].LoadNo;
                        }
                    }
                }

                txt_max_stress.Text = max_truss_stress.ToString("E4");
                txt_max_stress_load_case.Text = max_truss_stress_loadcase.ToString("0");
                txt_max_force.Text = max_truss_force.ToString("E4");
                txt_max_force_load_case.Text = max_truss_force_loadcase.ToString("0");



                #endregion  For Cable Member
            }
            else if (memType == eAstraMemberType.BEAM)
            {
                #region For Beam Member
                for (int i = 0; i < AnalysisResult.list_beams.Count; i++)
                {
                    if (AnalysisResult.list_beams[i].BeamNo == adata.AstraMemberNo)
                    {
                        if (AnalysisResult.list_beams[i].LoadNo == LoadCase)
                        {
                            //propertyGrid1.SelectedObject = AnalysisResult.list_beams[i];
                            txt_end_node_M1.Text = AnalysisResult.list_beams[i].EndNodeForce.M1_Torsion.ToString();
                            txt_end_node_M2.Text = AnalysisResult.list_beams[i].EndNodeForce.M2_Bending.ToString();
                            txt_end_node_M3.Text = AnalysisResult.list_beams[i].EndNodeForce.M3_Bending.ToString();

                            txt_end_node_R1.Text = AnalysisResult.list_beams[i].EndNodeForce.R1_Axial.ToString();
                            txt_end_node_R2.Text = AnalysisResult.list_beams[i].EndNodeForce.R2_Shear.ToString();
                            txt_end_node_R3.Text = AnalysisResult.list_beams[i].EndNodeForce.R3_Shear.ToString();

                            txt_start_node_M1.Text = AnalysisResult.list_beams[i].StartNodeForce.M1_Torsion.ToString();
                            txt_start_node_M2.Text = AnalysisResult.list_beams[i].StartNodeForce.M2_Bending.ToString();
                            txt_start_node_M3.Text = AnalysisResult.list_beams[i].StartNodeForce.M3_Bending.ToString();

                            txt_start_node_R1.Text = AnalysisResult.list_beams[i].StartNodeForce.R1_Axial.ToString();
                            txt_start_node_R2.Text = AnalysisResult.list_beams[i].StartNodeForce.R2_Shear.ToString();
                            txt_start_node_R3.Text = AnalysisResult.list_beams[i].StartNodeForce.R3_Shear.ToString();


                            //return;
                        }
                        if (Math.Abs(AnalysisResult.list_beams[i].StartNodeForce.R1_Axial) >
                            Math.Abs(max_R1))
                        {
                            max_R1 = AnalysisResult.list_beams[i].StartNodeForce.R1_Axial;
                            max_loadcase_R1 = AnalysisResult.list_beams[i].LoadNo;
                        }
                        if (Math.Abs(AnalysisResult.list_beams[i].StartNodeForce.R2_Shear) >
                            Math.Abs(max_R2))
                        {
                            max_R2 = AnalysisResult.list_beams[i].StartNodeForce.R2_Shear;
                            max_loadcase_R2 = AnalysisResult.list_beams[i].LoadNo;
                        }
                        if (Math.Abs(AnalysisResult.list_beams[i].StartNodeForce.R3_Shear) >
                            Math.Abs(max_R3))
                        {
                            max_R3 = AnalysisResult.list_beams[i].StartNodeForce.R3_Shear;
                            max_loadcase_R3 = AnalysisResult.list_beams[i].LoadNo;
                        }

                        if (Math.Abs(AnalysisResult.list_beams[i].StartNodeForce.M1_Torsion) >
                            Math.Abs(max_M1))
                        {
                            max_M1 = AnalysisResult.list_beams[i].StartNodeForce.M1_Torsion;
                            max_loadcase_M1 = AnalysisResult.list_beams[i].LoadNo;
                        }

                        if (Math.Abs(AnalysisResult.list_beams[i].StartNodeForce.M2_Bending) >
                            Math.Abs(max_M2))
                        {
                            max_M2 = AnalysisResult.list_beams[i].StartNodeForce.M2_Bending;
                            max_loadcase_M2 = AnalysisResult.list_beams[i].LoadNo;
                        }

                        if (Math.Abs(AnalysisResult.list_beams[i].StartNodeForce.M3_Bending) >
                            Math.Abs(max_M3))
                        {
                            max_M3 = AnalysisResult.list_beams[i].StartNodeForce.M3_Bending;
                            max_loadcase_M3 = AnalysisResult.list_beams[i].LoadNo;
                        }



                        if (Math.Abs(AnalysisResult.list_beams[i].EndNodeForce.R1_Axial) >
                            Math.Abs(max_R1))
                        {
                            max_R1 = AnalysisResult.list_beams[i].EndNodeForce.R1_Axial;
                            max_loadcase_R1 = AnalysisResult.list_beams[i].LoadNo;
                        }
                        if (Math.Abs(AnalysisResult.list_beams[i].EndNodeForce.R2_Shear) >
                            Math.Abs(max_R2))
                        {
                            max_R2 = AnalysisResult.list_beams[i].EndNodeForce.R2_Shear;
                            max_loadcase_R2 = AnalysisResult.list_beams[i].LoadNo;
                        }
                        if (Math.Abs(AnalysisResult.list_beams[i].EndNodeForce.R3_Shear) >
                            Math.Abs(max_R3))
                        {
                            max_R3 = AnalysisResult.list_beams[i].EndNodeForce.R3_Shear;
                            max_loadcase_R3 = AnalysisResult.list_beams[i].LoadNo;
                        }

                        if (Math.Abs(AnalysisResult.list_beams[i].EndNodeForce.M1_Torsion) >
                            Math.Abs(max_M1))
                        {
                            max_M1 = AnalysisResult.list_beams[i].EndNodeForce.M1_Torsion;
                            max_loadcase_M1 = AnalysisResult.list_beams[i].LoadNo;
                        }

                        if (Math.Abs(AnalysisResult.list_beams[i].EndNodeForce.M2_Bending) >
                            Math.Abs(max_M2))
                        {
                            max_M2 = AnalysisResult.list_beams[i].EndNodeForce.M2_Bending;
                            max_loadcase_M2 = AnalysisResult.list_beams[i].LoadNo;
                        }

                        if (Math.Abs(AnalysisResult.list_beams[i].EndNodeForce.M3_Bending) >
                            Math.Abs(max_M3))
                        {
                            max_M3 = AnalysisResult.list_beams[i].EndNodeForce.M3_Bending;
                            max_loadcase_M3 = AnalysisResult.list_beams[i].LoadNo;
                        }

                    }
                }



                txt_max_M1.Text = max_M1.ToString();
                txt_max_M2.Text = max_M2.ToString();
                txt_max_M3.Text = max_M3.ToString();
                txt_max_R1.Text = max_R1.ToString();
                txt_max_R2.Text = max_R2.ToString();
                txt_max_R3.Text = max_R3.ToString();

                txt_max_loadcase_M1.Text = max_loadcase_M1.ToString();
                txt_max_loadcase_M2.Text = max_loadcase_M2.ToString();
                txt_max_loadcase_M3.Text = max_loadcase_M3.ToString();
                txt_max_loadcase_R1.Text = max_loadcase_R1.ToString();
                txt_max_loadcase_R2.Text = max_loadcase_R2.ToString();
                txt_max_loadcase_R3.Text = max_loadcase_R3.ToString();
                //MessageBox.Show("Member No : " + cmb_member_no.Text + ", Load Case : " + cmb_load_case.Text + "  Not Found", "ASTRA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                #endregion  For Beam Member
            }
        }

        
        public int MemberNo
        {
            get
            {
                try
                {
                    MyList m = new MyList(cmb_member_no.Text, ':');
                    return m.GetInt(0);
                }
                catch (Exception ex) { }
                return -1;
                //return MyList.StringToInt( cmb_member_no.Text, -1);
            }
        }
        public int LoadCase
        {
            get
            {
                return MyList.StringToInt(cmb_load_case.Text, -1);
            }
        }
        public void ClearFields()
        {
            txt_max_M1.Text = "";
            txt_max_M2.Text = "";
            txt_max_M3.Text = "";
            txt_max_R1.Text = "";
            txt_max_R2.Text = "";
            txt_max_R3.Text = "";

            txt_max_loadcase_M1.Text = "";
            txt_max_loadcase_M2.Text = "";
            txt_max_loadcase_M3.Text = "";
            txt_max_loadcase_R1.Text = "";
            txt_max_loadcase_R2.Text = "";
            txt_max_loadcase_R3.Text = "";

            txt_end_node_M1.Text = "";
            txt_end_node_M2.Text = "";
            txt_end_node_M3.Text = "";

            txt_end_node_R1.Text = "";
            txt_end_node_R2.Text = "";
            txt_end_node_R3.Text = "";

            txt_start_node_M1.Text = "";
            txt_start_node_M2.Text = "";
            txt_start_node_M3.Text = "";

            txt_start_node_R1.Text = "";
            txt_start_node_R2.Text = "";
            txt_start_node_R3.Text = "";



            txt_max_stress.Text = "";
            txt_max_stress_load_case.Text = "";
            txt_max_force.Text = "";
            txt_max_force_load_case.Text = "";

            txt_mem_stress.Text = "";
            txt_mem_force.Text = "";
        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmb_member_no_SelectedIndexChanged(object sender, EventArgs e)
        {
            Show_forces();

        }
    }
    public class MLoadData
    {

        public MLoadData()
        {
            TypeNoText = "TYPE 6";
            Code = "IRC24RTRACK";
            X = -60.0;
            Y = 0.0d;
            Z = 1.0d;
            XINC = 0.5d;
            LoadWidth = 0d;
        }
        public string TypeNoText { get; set; }
        public int TypeNo
        {
            get
            {
                int i = 0;
                try
                {

                    string str = TypeNoText.ToUpper().Replace("TYPE", "");
                    if (TypeNoText.Length > 4)
                    {
                        return int.Parse(str);
                    }
                }
                catch (Exception ex) { }
                return 0;
            }
        }
        public string Code { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double XINC { get; set; }
        public double LoadWidth { get; set; }
        public MyList Distances { get; set; }
        public MyList LoadValues { get; set; }
        public double ImpactFactor
        {
            get
            {
                switch (TypeNoText)
                {
                    case "TYPE1":
                    case "TYPE 1":
                        return 0;
                        break;
                    case "TYPE2":
                    case "TYPE 2":
                        break;
                    case "TYPE3":
                    case "TYPE 3":
                        break;
                    case "TYPE4":
                    case "TYPE 4":
                        break;
                    case "TYPE5":
                    case "TYPE 5":
                        break;
                    case "TYPE6":
                    case "TYPE 6":
                        break;
                    case "TYPE7":
                    case "TYPE 7":
                        break;
                    case "TYPE8":
                    case "TYPE 8":
                        break;
                    case "TYPE9":
                    case "TYPE 9":
                        break;
                    case "TYPE10":
                    case "TYPE 10":
                        break;
                }
                return 0;
            }
        }

        public static List<MLoadData> GetLiveLoads(string file_path)
        {
            if (!File.Exists(file_path)) return null;

            //TYPE1 IRCCLASSA
            //68 68 68 68 114 114 114 27
            //3.00 3.00 3.00 4.30 1.20 3.20 1.10
            //1.80
            List<MLoadData> LL_list = new List<MLoadData>();
            List<string> file_content = new List<string>(File.ReadAllLines(file_path));
            MyList mlist = null;
            string kStr = "";

            int icount = 0;
            for (int i = 0; i < file_content.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(file_content[i]);
                //kStr = MyList.RemoveAllSpaces(file_content[i].Trim().TrimStart().TrimEnd());
                kStr = kStr.Replace(' ', ',');
                kStr = kStr.Replace(',', ' ');
                kStr = MyList.RemoveAllSpaces(kStr);
                mlist = new MyList(kStr, ' ');

                if (mlist.StringList[0].Contains("TYPE"))
                {
                    MLoadData ld = new MLoadData();
                    if (mlist.Count == 2)
                    {
                        kStr = mlist.StringList[0].Replace("TYPE", "");
                        ld.TypeNoText = "TYPE " + kStr;
                        ld.Code = mlist.StringList[1];
                        LL_list.Add(ld);
                    }
                    else if (mlist.Count == 3)
                    {
                        ld.TypeNoText = "TYPE " + mlist.StringList[1];
                        ld.Code = mlist.StringList[2];
                        LL_list.Add(ld);
                    }
                    if ((i + 3) < file_content.Count)
                        ld.LoadWidth = MyList.StringToDouble(file_content[i + 3], 0.0);

                    try
                    {
                        ld.LoadValues = new MyList(file_content[i + 1], ' ');
                        ld.Distances = new MyList(file_content[i + 2], ' ');
                    }
                    catch (Exception) { }
                }
            }
            return LL_list;

            //TYPE 2 IRCCLASSB
            //20.5 20.5 20.5 20.5 34.0 34.0 8.0 8.0
            //3.00 3.00 3.00 4.30 1.20 3.20 1.10
            //1.80

            //TYPE 3 IRC70RTRACK
            //70 70 70 70 70 70 70 70 70 70
            //0.457 0.457 0.457 0.457 0.457 0.457 0.457 0.457 0.457
            //0.84

            //TYPE 4 IRC70RWHEEL
            //85 85 85 85 60 60 40
            //1.37 3.05 1.37 2.13 1.52 3.96
            //0.450 1.480 0.450

            //TYPE 5 IRCCLASSAATRACK
            //70 70 70 70 70 70 70 70 70 70
            //0.360 0.360 0.360 0.360 0.360 0.360 0.360 0.360 0.360
            //0.85

            //TYPE 6 IRC24RTRACK
            //62.5 62.5 62.5 62.5 62.5 62.5 62.5 62.5 62.5 62.5
            //0.366 0.366 0.366 0.366 0.366 0.366 0.366 0.366 0.366
            //0.36
        }
    }
    public class LoadGenerationData
    {

        public LoadGenerationData()
        {
            LoadType = new MLoadData();
            X = Y = Z = XINC = 0.0;
        }
        //LOAD GENERATION 191
        //TYPE 1 -18.800 0 2.750 XINC 0.2
        //TYPE 1 -18.800 0 6.250 XINC 0.2
        //TYPE 1 -18.800 0 9.750 XINC 0.2

        public MLoadData LoadType { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double XINC { get; set; }
        
    }

}
//Chiranjit [2011 09 26]  //This class is used to Get for forces of a member
