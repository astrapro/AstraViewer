using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

using System.IO;
using System.Windows.Forms;
using HEADSNeed.ASTRA.ASTRAClasses;
using MovingLoadAnalysis.DataStructure;
using MovingLoadAnalysis;


namespace HEADSNeed.ASTRA.ASTRAClasses.StructureDesign
{
    public class Tables
    {
        public Tables()
        {
        }

        public static Rebar_Weights Rebars;
        public static Rebar_Weights Get_Rebar_Weights()
        {
            string table_file = ASTRA_Table_Path;
            table_file = Path.Combine(Path.GetDirectoryName(table_file), "Rebar_Weights.txt");

            Rebar_Weights list = new Rebar_Weights();
            if (File.Exists(table_file))
            {
                List<string> content = new List<string>(File.ReadAllLines(table_file));
                MyList mlist;
                for (int i = 0; i < content.Count; i++)
                {
                    try
                    {
                        mlist = new MyList(MyList.RemoveAllSpaces(content[i]), ' ');
                        if (mlist.Count == 2)
                        {
                            list.Add(new Rebar_Weight(mlist.GetInt(0), mlist.GetDouble(1)));
                        }
                    }
                    catch (Exception ex) { }
                }
            }
            Rebars = list;
            return list;
        }
        public static void Write_Rebar_Weights(Rebar_Weights list)
        {
            Rebars = list;
            string table_file = ASTRA_Table_Path;
            table_file = Path.Combine(Path.GetDirectoryName(table_file), "Rebar_Weights.txt");
            //table_file = Path.Combine(table_file, "Rebar_Weights.txt");

            if (File.Exists(table_file))
            {
                List<string> content = new List<string>();
                MyList mlist ;

                content.Add(string.Format(""));
                content.Add(string.Format("---------------------"));
                content.Add(string.Format("Bar Dia      WEIGHT"));
                content.Add(string.Format(" (mm)        (Ton/m)"));
                content.Add(string.Format("---------------------"));
                //list.Add(string.Format("   6       0.000222"));
                //list.Add(string.Format("   8       0.000395"));
                for (int i = 0; i < list.Count; i++)
                {
                    try
                    {
                        content.Add(string.Format("{0,4} {1,15:f6}", list[i].Size, list[i].Weight));
                    }
                    catch (Exception ex) { }
                }
                content.Add(string.Format("---------------------"));
                File.WriteAllLines(table_file, content.ToArray());
            }
        }


        public static eDesignStandard DesignStandard { get; set; }


        public static eDesignStandard Get_DesignStandard()
        {
            string pp = Environment.GetEnvironmentVariable("DesignStandard");

            if (pp == null) pp = "";

            if (pp.StartsWith("British"))
                return eDesignStandard.BritishStandard;

            return eDesignStandard.IndianStandard;

        }

        public static string ASTRA_Table_Path
        {
            get
            {
                string pp = "";

                DesignStandard = Get_DesignStandard();


                if (DesignStandard == eDesignStandard.IndianStandard)
                {
                    pp = Path.Combine(Application.StartupPath, "TABLES\\Indian Standard");
                }
                if (DesignStandard == eDesignStandard.BritishStandard)
                    pp = Path.Combine(Application.StartupPath, "TABLES\\British Standard");

                return Directory.Exists(pp) ? pp : Path.GetFileName(pp) + " Folder not found in Application folder";
            }
        }

        public static double Permissible_Shear_Stress(double percent, int con_grade, ref string ref_string)
        {
            int indx = -1;
            percent = Double.Parse(percent.ToString("0.00"));
            string table_file = ASTRA_Table_Path;
            //table_file = Path.Combine(table_file, "Box_Culvert_Table_1.txt");
            table_file = Path.Combine(table_file, "Permissible_Shear_Stress.txt");
            //Permissible_Shear_Stress

            List<string> lst_content = new List<string>(File.ReadAllLines(table_file));
            string kStr = "";
            MyList mList = null;

            bool find = false;

            double a1, b1, a2, b2, returned_value;

            a1 = 0.0;
            b1 = 0.0;
            a2 = 0.0;
            b2 = 0.0;
            returned_value = 0.0;

            List<MyList> lst_list = new List<MyList>();


            #region Swith Case
            switch (con_grade)
            {
                case 15:
                    indx = 1;
                    break;
                case 20:
                    indx = 2;
                    break;
                case  25:
                    indx = 3;
                    break;

                case  30:
                    indx = 4;
                    break;

                case  35:
                    indx = 5;
                    break;

                case  40:
                    indx = 6;
                    break;
                default:
                    indx = 6; con_grade =  40;
                    break;
            }
            #endregion


            for (int i = 0; i < lst_content.Count; i++)
            {
                if (i == 0)
                {
                    ref_string = lst_content[0];
                    find = false;
                }
                kStr = MyList.RemoveAllSpaces(lst_content[i]);
                //find = ((double.TryParse(mList.StringList[0], out a2)) && (mList.Count == 7));
                kStr = kStr.ToUpper().Replace("AND ABOVE", "").Trim().TrimEnd().TrimStart();
                kStr = kStr.Replace("<=", "");
                mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
                if (kStr.ToLower().Contains("m"))
                {
                    kStr = MyList.RemoveAllSpaces(lst_content[i]).ToUpper();

                    kStr = kStr.Substring(kStr.IndexOf('M'), (kStr.Length - kStr.IndexOf('M')));


                    MyList ml = new MyList(kStr, 'M');
                    indx = ml.StringList.IndexOf(((int)con_grade).ToString());

                    if (DesignStandard == eDesignStandard.BritishStandard)
                    {
                        if (con_grade ==  15 || con_grade ==  20)
                            indx = 1;
                        else if (con_grade ==  25)
                            indx = 2;
                        else if (con_grade ==  30)
                            indx = 3;
                        else if (con_grade ==  35 || con_grade ==  40)
                            indx = 4;
                        else
                            indx = 4;
                    }

                    if (indx != -1)
                    {
                        find = true; continue;
                    }
                }
                if (find)
                {
                    try
                    {
                        if (mList.GetDouble(0) != 0.0000001111111)
                        {
                            lst_list.Add(mList);
                        }
                    }
                    catch (Exception ex) { }
                }
            }

            for (int i = 0; i < lst_list.Count; i++)
            {
                a1 = lst_list[i].GetDouble(0);
                if (percent < lst_list[0].GetDouble(0))
                {
                    returned_value = lst_list[0].GetDouble(indx);
                    break;
                }
                else if (percent > (lst_list[lst_list.Count - 1].GetDouble(0)))
                {
                    returned_value = lst_list[lst_list.Count - 1].GetDouble(indx);
                    break;
                }

                if (a1 == percent)
                {
                    returned_value = lst_list[i].GetDouble(indx);
                    break;
                }
                else if (a1 > percent)
                {
                    a2 = a1;
                    b2 = lst_list[i].GetDouble(indx);

                    a1 = lst_list[i - 1].GetDouble(0);
                    b1 = lst_list[i - 1].GetDouble(indx);

                    returned_value = b1 + ((b2 - b1) / (a2 - a1)) * (percent - a1);
                    break;
                }
            }

            lst_list.Clear();
            lst_content.Clear();


            returned_value = Double.Parse(returned_value.ToString("0.000"));
            return returned_value;
        }
        public static List<string> Get_Tables_Permissible_Shear_Stress()
        {
            List<string> list = new List<string>();
            string table_file = ASTRA_Table_Path;
            table_file = Path.Combine(table_file, "Permissible_Shear_Stress.txt");

            if (File.Exists(table_file))
                return new List<string>(File.ReadAllLines(table_file));
            else
                list.Add("Table File Not found in application folder.");
            return list;
        }

        public static void Terzaghi_Bearing_Capacity_Factors(double phi, ref double Nc, ref double Nq, ref double Nr, ref string ref_string)
        {

            phi = Double.Parse(phi.ToString("0.000"));
            int indx = 0;

            string table_file = ASTRA_Table_Path;
            //table_file = Path.Combine(table_file, "Steel_Plate_Tab_4.txt");
            table_file = Path.Combine(table_file, "Terzaghi_Bearing_Capacity_Factors.txt");

            List<string> lst_content = new List<string>(File.ReadAllLines(table_file));
            string kStr = "";
            MyList mList = null;

            bool find = false;

            double a1, b1, a2, b2, returned_value;

            a1 = 0.0;
            b1 = 0.0;
            a2 = 0.0;
            b2 = 0.0;
            returned_value = 0.0;

            List<MyList> lst_list = new List<MyList>();

            for (int i = 0; i < lst_content.Count; i++)
            {
                kStr = MyList.RemoveAllSpaces(lst_content[i]);
                kStr = kStr.Replace("<=", "");
                if (i == 0)
                {
                    ref_string = kStr;
                }

                mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
                if (mList.Count == 4)
                {
                    try
                    {
                        a1 = mList.GetDouble(0);

                        lst_list.Add(mList);
                    }
                    catch (Exception ex) { }
                }
            }

            for (int i = 0; i < lst_list.Count; i++)
            {
                a1 = lst_list[i].GetDouble(0);
                if (phi < lst_list[0].GetDouble(0))
                {
                    Nc = lst_list[0].GetDouble(1);
                    Nq = lst_list[0].GetDouble(2);
                    Nr = lst_list[0].GetDouble(3);

                    break;
                }
                else if (phi > (lst_list[lst_list.Count - 1].GetDouble(0)))
                {
                    //returned_value = lst_list[lst_list.Count - 1].GetDouble(indx);

                    Nc = lst_list[lst_list.Count - 1].GetDouble(1);
                    Nq = lst_list[lst_list.Count - 1].GetDouble(2);
                    Nr = lst_list[lst_list.Count - 1].GetDouble(3);

                    break;
                }

                if (a1 == phi)
                {
                    //returned_value = lst_list[i].GetDouble(indx);

                    Nc = lst_list[i].GetDouble(1);
                    Nq = lst_list[i].GetDouble(2);
                    Nr = lst_list[i].GetDouble(3);

                    break;
                }
                else if (a1 > phi)
                {
                    //a2 = a1;
                    //b2 = lst_list[i].GetDouble(indx);

                    //a1 = lst_list[i - 1].GetDouble(0);
                    //b1 = lst_list[i - 1].GetDouble(indx);

                    //returned_value = b1 + ((b2 - b1) / (a2 - a1)) * (phi - a1);

                    Nc = lst_list[i].GetDouble(1);
                    Nq = lst_list[i].GetDouble(2);
                    Nr = lst_list[i].GetDouble(3);

                    break;
                }
            }

            lst_list.Clear();
            lst_content.Clear();
        }

        public static List<string> Get_Tables_Terzaghi_Bearing_Capacity_Factors()
        {
            List<string> list = new List<string>();
            string table_file = ASTRA_Table_Path;
            table_file = Path.Combine(table_file, "Terzaghi_Bearing_Capacity_Factors.txt");

            if (File.Exists(table_file))
                return new List<string>(File.ReadAllLines(table_file));
            else
                list.Add("Table File Not found in application folder.");
            return list;
        }
        //Chiranjit [2013 07 18]



        public static void Get_Bending_Moment_Coefficients(out double beta_x, out double beta_y, out double alpha_x, out double alpha_y, double Ly_by_Lx, int case_index)
        {

            beta_x = 0.084;
            beta_y = 0.063;
            alpha_x = 0.047;
            alpha_y = 0.035;


            List<double> list = new List<double>();

            List<List<double>> list_1 = new List<List<double>>();

            List<double> list_2 = new List<double>();





            string tab_file = Path.Combine(Application.StartupPath, @"TABLES\Bending_Moment_Coefficients.txt");

            if (File.Exists(tab_file))
            {
                List<string> file_content = new List<string>(File.ReadAllLines(tab_file));

                string kStr = "";
                MyList mlist = null;

                int flag = 0;
                double dd = 0.0;

                int i = 0;
                for (i = 0; i < file_content.Count; i++)
                {

                    kStr = MyList.RemoveAllSpaces(file_content[i].ToUpper().TrimEnd().TrimStart().Trim());

                    if (kStr.StartsWith("------"))
                    {
                        flag++;
                        continue;
                    }

                    if (flag == 1)
                    {
                        mlist = new MyList(kStr, ' ');

                        for (int j = 0; j < mlist.Count; j++)
                        {
                            try
                            {
                                list_2.Add(mlist.GetDouble(j));
                            }
                            catch (Exception ex) { }
                        }
                    }
                    else if (flag == 2)
                    {
                        mlist = new MyList(kStr, ' ');
                        for (int j = mlist.Count - 1; j > 0; j--)
                        {
                            try
                            {
                                list.Add(mlist.GetDouble(j));
                            }
                            catch (Exception ex) { break; }
                        }

                        if (list.Count > 0)
                        {
                            list.Reverse();
                            list_1.Add(list);
                            list = new List<double>();
                        }
                    }
                }

                //Ly_by_Lx = 1.75;
                for (i = 1; i < list_2.Count; i++)
                {
                    if (Ly_by_Lx >= list_2[i - 1] && Ly_by_Lx <= list_2[i])
                    {
                        int indx = case_index;

                        indx = indx * 2;

                        beta_x = list_1[indx][i];
                        beta_y = list_1[indx + 1][i];

                        alpha_x = list_1[indx][list_1[indx].Count - 1];
                        alpha_y = list_1[indx + 1][list_1[indx].Count - 1];
                        break;
                    }
                }
            }
        }



        public static void Get_Shear_Force_Coefficients(out double gamma_x1, out double gamma_x2, out double gamma_x3, out double gamma_x4, double Ly_by_Lx, int case_index)
        {

            gamma_x1 = 0.57;
            gamma_x2 = 0.38;

            gamma_x3 = 0.57;
            gamma_x4 = 0.38;


            List<double> list = new List<double>();

            List<List<double>> list_1 = new List<List<double>>();

            List<double> list_2 = new List<double>();





            string tab_file = Path.Combine(Application.StartupPath, @"TABLES\Shear_Force_Coefficients.txt");

            if (File.Exists(tab_file))
            {
                List<string> file_content = new List<string>(File.ReadAllLines(tab_file));

                string kStr = "";
                MyList mlist = null;

                int flag = 0;
                double dd = 0.0;

                int i = 0;
                for (i = 0; i < file_content.Count; i++)
                {

                    kStr = MyList.RemoveAllSpaces(file_content[i].ToUpper().TrimEnd().TrimStart().Trim());

                    if (kStr.StartsWith("------"))
                    {
                        flag++;
                        continue;
                    }

                    if (flag == 1)
                    {
                        mlist = new MyList(kStr, ' ');

                        for (int j = 0; j < mlist.Count; j++)
                        {
                            try
                            {
                                list_2.Add(mlist.GetDouble(j));
                            }
                            catch (Exception ex) { }
                        }
                    }
                    else if (flag == 2)
                    {
                        mlist = new MyList(kStr, ' ');
                        for (int j = mlist.Count - 1; j > 0; j--)
                        {
                            try
                            {
                                list.Add(mlist.GetDouble(j));
                            }
                            catch (Exception ex) { break; }
                        }

                        if (list.Count > 2)
                        {
                            list.Reverse();
                            list_1.Add(list);
                        }
                        list = new List<double>();
                    }
                }

                //Ly_by_Lx = 1.75;
                for (i = 1; i < list_2.Count; i++)
                {
                    if (Ly_by_Lx >= list_2[i - 1] && Ly_by_Lx <= list_2[i])
                    {
                        int indx = case_index;
                        if (indx == 8 || indx == 0 || indx == 1)
                            indx = 0;
                        else
                            indx = indx - 1;


                        indx = indx * 2;

                        gamma_x1 = list_1[indx][i];
                        gamma_x2 = list_1[indx + 1][i];

                        gamma_x3 = list_1[indx][list_1[indx].Count - 1];
                        gamma_x4 = list_1[indx + 1][list_1[indx].Count - 1];

                        break;
                    }
                }
            }
        }
        public static List<string> Get_File_Bending_Moment_Coefficients()
        {
            string tab_file = Path.Combine(Application.StartupPath, @"TABLES\Bending_Moment_Coefficients.txt");

            if (File.Exists(tab_file))
                return new List<string>(File.ReadAllLines(tab_file));

            return new List<string>();
        }

        public static List<string> Get_File_Shear_Force_Coefficients()
        {
            string tab_file = Path.Combine(Application.StartupPath, @"TABLES\Shear_Force_Coefficients.txt");

            if (File.Exists(tab_file))
                return new List<string>(File.ReadAllLines(tab_file));

            return new List<string>();
        }
        public static List<string> Get_File_Permissible_Shear_Stress()
        {
            string tab_file = Path.Combine(Application.StartupPath, @"TABLES\Permissible_Shear_Stress.txt");

            if (File.Exists(tab_file))
                return new List<string>(File.ReadAllLines(tab_file));

            return new List<string>();
        }


    }

    public enum eDesignStandard
    {
        IndianStandard = 0,
        BritishStandard = 1,
    }

    public class ASTRAGrade
    {
        public int ConcreteGrade { get; set; }
        public int SteelGrade { get; set; }
        public double Modular_Ratio { get; set; }

        public double Allowable_Stress_Concrete_kg_sq_cm
        {
            get
            {
                return double.Parse((((double)(int)ConcreteGrade) * 10.0 / 3.0).ToString("f2"));
            }
        }
        public double sigma_c_kg_sq_cm
        {
            get
            {
                return Allowable_Stress_Concrete_kg_sq_cm;
            }
        }
        public double sigma_c_N_sq_mm
        {
            get
            {
                return fcb;
            }
        }

        public double Permissible_Stress_Steel
        {
            get
            {
                switch (SteelGrade)
                {
                    case 240:
                        return 125.0;
                    case 415:
                        return 200.0;
                    case 500:
                        return 240.0;
                }
                return 240.0;
            }
        }
        public double sigma_sv_N_sq_mm
        {
            get
            {
                return Permissible_Stress_Steel;
            }
        }
        public double sigma_st_N_sq_mm
        {
            get
            {
                return Permissible_Stress_Steel;
            }
        }
        public double fst
        {
            get
            {
                return Permissible_Stress_Steel;
            }
        }
        public double fck
        {
            get
            {
                return (double)(int)ConcreteGrade;
            }
        }
        public double fcb
        {
            get
            {
                return double.Parse((fck / 3).ToString("f2"));
            }

        }
        public double fcc
        {
            get
            {
                return double.Parse((fck / 4).ToString("f2"));
            }

        }
        public double fy
        {
            get
            {
                return (double)(int)SteelGrade;
            }
        }
        public double m
        {
            get
            {
                return Modular_Ratio;
            }
        }
        public double n
        {
            get
            {
                return ((m * fcb) / (m * fcb + fst));
            }
        }
        public double Lever_Arm_Factor
        {
            get
            {
                return (1 - n / 3);
            }
        }
        public double j
        {
            get
            {
                return Lever_Arm_Factor;
            }
        }
        public double Q
        {
            get
            {
                return (n * j * fcb / 2);
            }
        }
        public double Moment_Factor
        {
            get
            {
                return Q;
            }

        }
        public ASTRAGrade()
        {
            ConcreteGrade =  25;
            SteelGrade =  415;
        }
        public ASTRAGrade(string conc_grade, string steel_grade)
        {
            ConcreteGrade = ( 25);
            SteelGrade = ( 415);

            try
            {

                ConcreteGrade = (int.Parse(conc_grade));
                SteelGrade = (int.Parse(steel_grade));
                //Modular_Ratio = 10;
            }
            catch (Exception ex) { }
        }
        public ASTRAGrade(double conc_grade, double steel_grade)
        {
            ConcreteGrade =  ((int)(conc_grade));
            SteelGrade = ((int)(steel_grade));
        }
        public ASTRAGrade(int conc_grade, int steel_grade)
        {
            ConcreteGrade = conc_grade;
            SteelGrade = steel_grade;
        }

    }

    public class Rebar_Weight
    {
        public int Size { get; set; }
        public double Weight { get; set; }

        public Rebar_Weight()
        {
            Size = 0;
            Weight = 0;
        }
        public Rebar_Weight(int size, double weight)
        {
            Size = size;
            Weight = weight;
        }
    }

    public class Rebar_Weights : List<Rebar_Weight>
    {
        public Rebar_Weights()
            : base()
        {

        }

        public double Get_Rebar_Weight(double bar_dia)
        {
            return Get_Rebar_Weight((int)bar_dia);
        }
        public double Get_Rebar_Weight(int bar_dia)
        {
            foreach (var item in this)
            {
                if (item.Size == bar_dia) return item.Weight;
            }
            return 0.0;
        }

    }


}
