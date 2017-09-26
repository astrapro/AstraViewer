using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using HEADSNeed.ASTRA.ASTRAClasses;

namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    public partial class frmDefineMovingLoad : Form
    {
        IASTRACAD iACad = null;
        public List<string> ASTRA_Data { get; set; }
        public MovingLoadData MLD { get; set; }

        LiveLoadCollections LL_Collections { get; set; }

        public EMassUnits ForceUnit { get; set; }
        public frmDefineMovingLoad(IASTRACAD iACAD)
        {
            InitializeComponent();
            this.iACad = iACAD;
            ASTRA_Data = new List<string>();
            ForceUnit = iACAD.MassUnit;
            string ll_txt = Path.Combine(Application.StartupPath, "LL.TXT");
            if (File.Exists(ll_txt))
                LL_Collections = new LiveLoadCollections(ll_txt);

        }


        private void txt_loads_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //MyList ml = new MyList(MyList.RemoveAllSpaces(txt_distances.Text), ',');
                ////txt_total_dist.Text = ml.SUM.ToString("f3");
                //txt_tot_dis.Text = ml.Count.ToString("f0");

                //ml = new MyList(MyList.RemoveAllSpaces(txt_loads.Text), ',');
                ////txt_total_dist.Text = ml.SUM.ToString("f3");
                //txt_tot_ld.Text = ml.Count.ToString("f0");

            }
            catch (Exception ex) { }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            string txt_loads = "";
            string txt_distances = "";

            for (int i = 0; i < dgv_loads.RowCount - 1; i++)
            {
                txt_loads += dgv_loads[1, i].Value + " ";
                txt_distances += dgv_loads[2, i].Value + " ";
            }

            MLD = new MovingLoadData();
            MLD.Type = MyStrings.StringToInt(txt_type.Text, 0);
            MLD.Name = txt_lm.Text;
            MLD.Loads = txt_loads;
            MLD.Distances = txt_distances;
            MLD.LoadWidth = MyStrings.StringToDouble(txt_load_width.Text, 0);
            //Load
            this.Close();
        }

        private void dgv_loads_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            
            Set_Auto_Serial();
        }

        private void Set_Auto_Serial()
        {

            double dd = 0.0;
            try
            {
                if (dgv_loads.RowCount > 1)
                {

                    for (int i = 0; i < dgv_loads.RowCount - 1; i++)
                    {
                        if (i == 0)
                        {

                            dgv_loads[2, 0].ReadOnly = true;
                            dgv_loads[2, 0].Style.BackColor = Color.DarkKhaki;
                            dgv_loads[2, 0].Value = "";
                        }
                        else
                        {
                            dgv_loads[2, i].ReadOnly = false;
                            dgv_loads[2, i].Style.BackColor = Color.White;
                            //dgv_loads[2, i].Value = "";
                        }
                        dgv_loads[0, i].Value = (i + 1);
                        dd += MyStrings.StringToDouble(dgv_loads[2, i].Value.ToString(), 0.0);
                    }
                    txt_load_distance.Text = dd.ToString("f3");
                }
            }
            catch (Exception ex) { }
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            try
            {
                dgv_loads.Rows.Insert(dgv_loads.CurrentCell.RowIndex, "", "", "");
            }
            catch (Exception ex) { }
        }

        private void btn_ld_delete_Click(object sender, EventArgs e)
        {
            try
            {
                dgv_loads.Rows.RemoveAt(dgv_loads.CurrentCell.RowIndex);
            }
            catch (Exception ex) { }

        }

        private void frmDefineMovingLoad_Load(object sender, EventArgs e)
        {
            if (LL_Collections != null)
            {
                foreach (var item in LL_Collections)
                {
                    lst_pdl.Items.Add(item.Code);
                }
            }

            if (MLD != null)
            {
                txt_type.Text = MLD.Type.ToString();
                txt_lm.Text = MLD.Name;
                txt_load_width.Text = MLD.LoadWidth.ToString("f3");

                MyStrings ml_loads = new MyStrings(MLD.Loads.Trim(), ' ');
                MyStrings ml_dists = new MyStrings(MLD.Distances.Trim(), ' ');
                dgv_loads.Rows.Clear();
                for (int i = 0; i < ml_loads.Count; i++)
                {
                    if (i == 0)
                        dgv_loads.Rows.Add((i + 1), ml_loads.StringList[i], "");
                    else
                        dgv_loads.Rows.Add((i + 1), ml_loads.StringList[i], ml_dists.StringList[i - 1]);
                }
                btn_new.Text = "Change";

            }
            Set_Auto_Serial();
        }

        private void btn_apply_Click(object sender, EventArgs e)
        {
            int r = lst_pdl.SelectedIndex;
            if (r > -1)
            {
                txt_lm.Text = LL_Collections[r].Code;
                txt_load_width.Text = LL_Collections[r].LoadWidth.ToString("f3");

                dgv_loads.Rows.Clear();

                if (ForceUnit == EMassUnits.KN)
                {
                    for (int i = 0; i < LL_Collections[r].Loads_In_KN.Count; i++)
                    {
                        if (i == 0)

                            dgv_loads.Rows.Add((i + 1), LL_Collections[r].Loads_In_KN.StringList[i], "");
                        else
                            dgv_loads.Rows.Add((i + 1), LL_Collections[r].Loads_In_KN.StringList[i], LL_Collections[r].Distances.StringList[i - 1]);
                    }
                }
                else
                {
                    for (int i = 0; i < LL_Collections[r].Loads.Count; i++)
                    {
                        if (i == 0)

                            dgv_loads.Rows.Add((i + 1), LL_Collections[r].Loads.StringList[i], "");
                        else
                            dgv_loads.Rows.Add((i + 1), LL_Collections[r].Loads.StringList[i], LL_Collections[r].Distances.StringList[i - 1]);
                    }
                }
            }

            Set_Auto_Serial();

        }
    }

    public class LoadData
    {
        public LoadData()
        {
            TypeNo = "TYPE 6";
            Code = "IRC24RTRACK";
            X = -60.0;
            Y = 0.0d;
            Z = 1.0d;
            XINC = 0.5d;
            YINC = 0.0d;
            ZINC = 0.0d;
            LoadWidth = 0d;

            ImpactFactor = -1.0d;
        }
        public LoadData(LoadData obj)
        {
            TypeNo = obj.TypeNo;
            Code = obj.Code;
            X = obj.X;
            Y = obj.Y;
            Z = obj.Z;
            XINC = obj.XINC;
            YINC = obj.YINC;
            ZINC = obj.ZINC;
            LoadWidth = obj.LoadWidth;

            ImpactFactor = obj.ImpactFactor;

            Loads = new MyStrings(obj.Loads);
            Distances = new MyStrings(obj.Distances);

        }
        public string TypeNo { get; set; }
        public string Code { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double XINC { get; set; }
        public double YINC { get; set; }
        public double ZINC { get; set; }
        public double LoadWidth { get; set; }
        public MyStrings Loads { get; set; }
        public MyStrings Loads_In_KN
        {
            get
            {
                if (Loads == null) return null;
                string str = "";
                for (int i = 0; i < Loads.Count; i++)
                {
                    try
                    {
                        //str += " " + (Loads.GetDouble(i) * 10).ToString("f3");
                        str += " " + (Loads.GetDouble(i) * 10).ToString();
                    }
                    catch (Exception ex) { }
                }

                str = MyStrings.RemoveAllSpaces(str.Replace(",", ""));
                return new MyStrings(str, ' ');
            }
        }
        public MyStrings Distances { get; set; }
        public double Distance
        {
            get
            {
                double d = 0.0;

                for (int i = 0; i < Distances.Count; i++)
                {
                    d += MyStrings.StringToDouble(Distances.StringList[i], 0.0);
                }
                return -d;
            }
        }

        public double Total_Loads
        {
            get
            {
                double d = 0.0;

                for (int i = 0; i < Loads.Count; i++)
                {
                    d += MyStrings.StringToDouble(Loads.StringList[i], 0.0);
                }
                return d;
            }
        }

        public double Default_ImpactFactor
        {
            get
            {
                switch (Code)
                {
                    case "IRCCLASSA":
                        return 1.179;
                        break;
                    case "IRCCLASSB":
                        return 1.188;
                        break;
                    case "LRFD_HTL57":
                        return 1.25;
                        break;
                    case "LRFD_HL93_HS20":
                        return 1.25;
                        break;
                    case "LRFD_HL93_H20":
                        return 1.25;
                        break;
                    case "IRC70RTRACK":
                        return 1.10;
                        break;
                    case "IRC70RWHEEL":
                        return 1.179;
                        break;
                    case "IRCCLASSAATRACK":
                        return 1.188;
                        break;
                    case "IRC24RTRACK":
                        return 1.188;
                        break;
                    case "BG_RAIL_1":
                        return 1.90;
                        break;
                    case "BG_RAIL_2":
                        return 1.90;
                        break;
                    case "MG_RAIL_1":
                        return 1.90;
                        break;
                    case "MG_RAIL_2":
                        return 1.90;
                        break;
                }
                return 1.25;
            }
            /*
            get
            {
                switch (TypeNo)
                {
                    case "TYPE1":
                    case "TYPE 1":
                        return 1.179;
                        break;
                    case "TYPE2":
                    case "TYPE 2":
                        return 1.188;
                        break;
                    case "TYPE3":
                    case "TYPE 3":
                        return 1.25;
                        break;
                    case "TYPE4":
                    case "TYPE 4":
                        return 1.25;
                        break;
                    case "TYPE5":
                    case "TYPE 5":
                        return 1.25;
                        break;
                    case "TYPE6":
                    case "TYPE 6":
                        return 1.10;
                        break;
                    case "TYPE7":
                    case "TYPE 7":
                        return 1.179;
                        break;
                    case "TYPE8":
                    case "TYPE 8":
                        return 1.188;
                        break;
                    case "TYPE9":
                    case "TYPE 9":
                        return 1.188;
                        break;
                    case "TYPE10":
                    case "TYPE11":
                    case "TYPE12":
                    case "TYPE13":
                    case "TYPE14":
                    case "TYPE 10":
                    case "TYPE 11":
                    case "TYPE 12":
                    case "TYPE 13":
                    case "TYPE 14":
                        return 1.90;
                        break;
                }
                return 1.25;
            }
            /**/
        }
        double im_fact = -1;
        public double ImpactFactor
        {
            get { if (im_fact == -1) im_fact = Default_ImpactFactor; return im_fact; }
            set { im_fact = value; }
        }
        public static LoadData Parse(string txt)
        {
            //TYPE 2 -48.750 0 0.500 XINC 0.2

            txt = txt.ToUpper().Replace("TYPE", "");
            txt = txt.Replace(",", " ");
            txt = MyStrings.RemoveAllSpaces(txt);
            MyStrings mlist = new MyStrings(txt, ' ');
            LoadData ld = new LoadData();

            ld.TypeNo = "TYPE " + mlist.GetInt(0);
            ld.X = mlist.GetDouble(1);
            ld.Y = mlist.GetDouble(2);
            ld.Z = mlist.GetDouble(3);

            if (mlist.StringList[4] == "XINC")
                ld.XINC = mlist.GetDouble(5);
            if (mlist.StringList[4] == "YINC")
                ld.YINC = mlist.GetDouble(5);
            if (mlist.StringList[4] == "ZINC")
                ld.ZINC = mlist.GetDouble(5);

            return ld;

        }
        public static List<LoadData> GetLiveLoads(string file_path)
        {
            if (!File.Exists(file_path)) return null;

            //TYPE1 IRCCLASSA
            //68 68 68 68 114 114 114 27
            //3.00 3.00 3.00 4.30 1.20 3.20 1.10
            //1.80
            List<LoadData> LL_list = new List<LoadData>();
            List<string> file_content = new List<string>(File.ReadAllLines(file_path));
            MyStrings mlist = null;
            string kStr = "";

            int icount = 0;
            for (int i = 0; i < file_content.Count; i++)
            {
                kStr = MyStrings.RemoveAllSpaces(file_content[i]);
                kStr = kStr.Replace(',', ' ');
                kStr = MyStrings.RemoveAllSpaces(kStr);
                mlist = new MyStrings(kStr, ' ');

                if (mlist.StringList[0].Contains("TYPE"))
                {
                    LoadData ld = new LoadData();
                    if (mlist.Count == 2)
                    {
                        kStr = mlist.StringList[0].Replace("TYPE", "");
                        ld.TypeNo = "TYPE " + kStr;
                        ld.Code = mlist.StringList[1];
                        LL_list.Add(ld);
                    }
                    else if (mlist.Count == 3)
                    {
                        ld.TypeNo = "TYPE " + mlist.StringList[1];
                        ld.Code = mlist.StringList[2];
                        LL_list.Add(ld);
                    }
                    if ((i + 3) < file_content.Count)
                    {
                        ld.Loads = new MyStrings(MyStrings.RemoveAllSpaces(file_content[i + 1].Replace(",", " ")), ' ');
                        ld.Distances = new MyStrings(MyStrings.RemoveAllSpaces(file_content[i + 2].Replace(",", " ")), ' ');
                        ld.LoadWidth = MyStrings.StringToDouble(file_content[i + 3], 0.0);
                    }
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

        public override string ToString()
        {
            return string.Format("{0} {1} {2:f3} ", TypeNo, Code, ((ImpactFactor != -1.0) ? ImpactFactor : Default_ImpactFactor));
        }
        public void ToStream(StreamWriter sw)
        {
            sw.WriteLine();
            sw.WriteLine("{0} {1}", TypeNo, Code);
            sw.WriteLine("{0}", Loads.ToString());
            sw.WriteLine("{0}", Distances.ToString());
            sw.WriteLine("{0:f3}", LoadWidth);
            sw.WriteLine();
        }
        public void ToStream_In_KN(StreamWriter sw)
        {
            sw.WriteLine();
            sw.WriteLine("{0} {1}", TypeNo, Code);
            sw.WriteLine("{0}", Loads_In_KN.ToString());
            sw.WriteLine("{0}", Distances.ToString());
            sw.WriteLine("{0:f3}", LoadWidth);
            sw.WriteLine();
        }
        public string Data
        {
            get
            {
                string str = "";
                str += string.Format("{0}  {1}  {2:f3}", TypeNo, Code, ImpactFactor) + "\n\r";
                str += string.Format("{0}", Loads.ToString()) + "\n\r"; ;
                str += string.Format("{0}", Distances.ToString()) + "\n\r"; ;
                str += string.Format("{0:f3}", LoadWidth);
                str += "";

                return str;
            }
        }
        public string Data_KN
        {
            get
            {
                string str = "";
                str += string.Format("{0}  {1}  {2:f3}", TypeNo, Code, ImpactFactor) + "\n\r";
                str += string.Format("{0}", Loads_In_KN.ToString()) + "\n\r"; ;
                str += string.Format("{0}", Distances.ToString()) + "\n\r"; ;
                str += string.Format("{0:f3}", LoadWidth);
                str += "";

                return str;
            }
        }

        public double Get_Maximum_Load()
        {
            double max = 0;
            for (int i = 0; i < Loads.Count; i++)
            {
                try
                {
                    if (max <= Loads.GetDouble(i))
                        max = Loads.GetDouble(i);
                }
                catch (Exception ex) { }
            }
            return max;
        }
    }
    public class LiveLoadCollections : List<LoadData>
    {
        public LiveLoadCollections(string file_name)
        {
            try
            {
                if (File.Exists(file_name))
                {

                    AddRange(LoadData.GetLiveLoads(file_name));
                }
            }
            catch (Exception ex) { }
        }
        public void Fill_Combo(ref ComboBox cmb)
        {
            try
            {
                cmb.Items.Clear();

                foreach (var item in this)
                {
                    cmb.Items.Add(item.TypeNo + " : " + item.Code);
                }
                if (cmb.Items.Count > 0)
                    cmb.SelectedIndex = 0;

            }
            catch (Exception ex) { }
        }
        public void Impact_Factor_2012_02_09(ref List<string> list, bool IsAASHTO)
        {
            if (!list.Contains("DEFINE MOVING LOAD FILE LL.TXT"))
                list.Add("DEFINE MOVING LOAD FILE LL.TXT");
            if (IsAASHTO)
            {

                if (!list.Contains("TYPE 1 LRFD_HTL57 1.25"))
                    list.Add("TYPE 1 LRFD_HTL57 1.25");
                if (!list.Contains("TYPE 2 LRFD_HL93_HS20 1.25"))
                    list.Add("TYPE 2 LRFD_HL93_HS20 1.25");
                if (!list.Contains("TYPE 3 LRFD_HL93_H20 1.25"))
                    list.Add("TYPE 3 LRFD_HL93_H20 1.25");
                if (!list.Contains("TYPE 4 CLA 1.179"))
                    list.Add("TYPE 4 CLA 1.179");
                if (!list.Contains("TYPE 5 CLB 1.188"))
                    list.Add("TYPE 5 CLB 1.188");
                if (!list.Contains("TYPE 6 A70RT 1.10"))
                    list.Add("TYPE 6 A70RT 1.10");
                if (!list.Contains("TYPE 7 CLAR 1.179"))
                    list.Add("TYPE 7 CLAR 1.179");
                if (!list.Contains("TYPE 8 A70RR 1.188"))
                    list.Add("TYPE 8 A70RR 1.188");
                if (!list.Contains("TYPE 9 IRC24RTRACK 1.188"))
                    list.Add("TYPE 9 IRC24RTRACK 1.188");
                if (!list.Contains("TYPE 10 BG_RAIL_1 1.9"))
                    list.Add("TYPE 10 BG_RAIL_1 1.9");
                if (!list.Contains("TYPE 11 BG_RAIL_2 1.9"))
                    list.Add("TYPE 11 BG_RAIL_2 1.90");
                if (!list.Contains("TYPE 12 MG_RAIL_1 1.9"))
                    list.Add("TYPE 12 MG_RAIL_1 1.90");
                if (!list.Contains("TYPE 13 MG_RAIL_2 1.9"))
                    list.Add("TYPE 13 MG_RAIL_2 1.90");
            }
            else
            {

                if (!list.Contains("TYPE 1 CLA 1.179"))
                    list.Add("TYPE 1 CLA 1.179");
                if (!list.Contains("TYPE 2 CLB 1.188"))
                    list.Add("TYPE 2 CLB 1.188");
                if (!list.Contains("TYPE 3 A70RT 1.10"))
                    list.Add("TYPE 3 A70RT 1.10");
                if (!list.Contains("TYPE 4 CLAR 1.179"))
                    list.Add("TYPE 4 CLAR 1.179");
                if (!list.Contains("TYPE 5 A70RR 1.188"))
                    list.Add("TYPE 5 A70RR 1.188");
                if (!list.Contains("TYPE 6 IRC24RTRACK 1.188"))
                    list.Add("TYPE 6 IRC24RTRACK 1.188");
                if (!list.Contains("TYPE 7 LRFD_HTL57 1.25"))
                    list.Add("TYPE 7 LRFD_HTL57 1.25");
                if (!list.Contains("TYPE 8 LRFD_HL93_HS20 1.25"))
                    list.Add("TYPE 8 LRFD_HL93_HS20 1.25");
                if (!list.Contains("TYPE 9 LRFD_HL93_H20 1.25"))
                    list.Add("TYPE 9 LRFD_HL93_H20 1.25");
                if (!list.Contains("TYPE 10 BG_RAIL_1 1.9"))
                    list.Add("TYPE 10 BG_RAIL_1 1.9");
                if (!list.Contains("TYPE 11 BG_RAIL_2 1.9"))
                    list.Add("TYPE 11 BG_RAIL_2 1.90");
                if (!list.Contains("TYPE 12 MG_RAIL_1 1.9"))
                    list.Add("TYPE 12 MG_RAIL_1 1.90");
                if (!list.Contains("TYPE 13 MG_RAIL_2 1.9"))
                    list.Add("TYPE 13 MG_RAIL_2 1.90");
            }
        }
        //Chiranjit [2012 02 09]
        public void Impact_Factor(ref List<string> list)
        {
            bool IsAASHTO = false;

            if (!list.Contains("DEFINE MOVING LOAD FILE LL.TXT"))
                list.Add("DEFINE MOVING LOAD FILE LL.TXT");

            foreach (var item in this)
            {
                list.Add(item.ToString());
            }
            if (false)
            {
                #region set Impact Factor


                if (IsAASHTO)
                {

                    if (!list.Contains("TYPE 1 LRFD_HTL57 1.25"))
                        list.Add("TYPE 1 LRFD_HTL57 1.25");
                    if (!list.Contains("TYPE 2 LRFD_HL93_HS20 1.25"))
                        list.Add("TYPE 2 LRFD_HL93_HS20 1.25");
                    if (!list.Contains("TYPE 3 LRFD_HL93_H20 1.25"))
                        list.Add("TYPE 3 LRFD_HL93_H20 1.25");
                    if (!list.Contains("TYPE 4 CLA 1.179"))
                        list.Add("TYPE 4 CLA 1.179");
                    if (!list.Contains("TYPE 5 CLB 1.188"))
                        list.Add("TYPE 5 CLB 1.188");
                    if (!list.Contains("TYPE 6 A70RT 1.10"))
                        list.Add("TYPE 6 A70RT 1.10");
                    if (!list.Contains("TYPE 7 CLAR 1.179"))
                        list.Add("TYPE 7 CLAR 1.179");
                    if (!list.Contains("TYPE 8 A70RR 1.188"))
                        list.Add("TYPE 8 A70RR 1.188");
                    if (!list.Contains("TYPE 9 IRC24RTRACK 1.188"))
                        list.Add("TYPE 9 IRC24RTRACK 1.188");
                    if (!list.Contains("TYPE 10 BG_RAIL_1 1.9"))
                        list.Add("TYPE 10 BG_RAIL_1 1.9");
                    if (!list.Contains("TYPE 11 BG_RAIL_2 1.90"))
                        list.Add("TYPE 11 BG_RAIL_2 1.90");
                    if (!list.Contains("TYPE 12 MG_RAIL_1 1.90"))
                        list.Add("TYPE 12 MG_RAIL_1 1.90");
                    if (!list.Contains("TYPE 13 MG_RAIL_2 1.90"))
                        list.Add("TYPE 13 MG_RAIL_2 1.90");
                }

                else
                {

                    if (!list.Contains("TYPE 1 CLA 1.179"))
                        list.Add("TYPE 1 CLA 1.179");
                    if (!list.Contains("TYPE 2 CLB 1.188"))
                        list.Add("TYPE 2 CLB 1.188");
                    if (!list.Contains("TYPE 3 A70RT 1.10"))
                        list.Add("TYPE 3 A70RT 1.10");
                    if (!list.Contains("TYPE 4 CLAR 1.179"))
                        list.Add("TYPE 4 CLAR 1.179");
                    if (!list.Contains("TYPE 5 A70RR 1.188"))
                        list.Add("TYPE 5 A70RR 1.188");
                    if (!list.Contains("TYPE 6 IRC24RTRACK 1.188"))
                        list.Add("TYPE 6 IRC24RTRACK 1.188");
                    if (!list.Contains("TYPE 7 LRFD_HTL57 1.25"))
                        list.Add("TYPE 7 LRFD_HTL57 1.25");
                    if (!list.Contains("TYPE 8 LRFD_HL93_HS20 1.25"))
                        list.Add("TYPE 8 LRFD_HL93_HS20 1.25");
                    if (!list.Contains("TYPE 9 LRFD_HL93_H20 1.25"))
                        list.Add("TYPE 9 LRFD_HL93_H20 1.25");
                    if (!list.Contains("TYPE 10 BG_RAIL_1 1.9"))
                        list.Add("TYPE 10 BG_RAIL_1 1.9");
                    if (!list.Contains("TYPE 11 BG_RAIL_2 1.90"))
                        list.Add("TYPE 11 BG_RAIL_2 1.90");
                    if (!list.Contains("TYPE 12 MG_RAIL_1 1.90"))
                        list.Add("TYPE 12 MG_RAIL_1 1.90");
                    if (!list.Contains("TYPE 13 MG_RAIL_2 1.90"))
                        list.Add("TYPE 13 MG_RAIL_2 1.90");
                }
                #endregion set Impact Factor
            }
        }

        public void Save_LL_TXT(string folder, bool IsMTon)
        {

            StreamWriter sw = new StreamWriter(Path.Combine(folder, "LL.TXT"));
            try
            {
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("FILE LL.TXT");
                sw.WriteLine();
                sw.WriteLine();
                if (IsMTon)
                {
                    foreach (var item in this)
                    {
                        item.ToStream(sw);
                    }
                }
                else
                {
                    foreach (var item in this)
                    {
                        item.ToStream_In_KN(sw);
                    }
                }
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

        public double Get_Distance(string type_no)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].TypeNo == type_no)
                    return this[i].Distance;
            }
            return 0.0;
        }
    }
}
