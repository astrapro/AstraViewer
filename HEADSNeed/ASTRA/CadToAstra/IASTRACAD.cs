using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using HEADSNeed.ASTRA.ASTRAClasses;
using HEADSNeed.ASTRA.CadToAstra;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdCollections;

namespace HEADSNeed.ASTRA.CadToAstra
{
    public interface IASTRACAD
    {
        string ProjectTitle { get; set; }
        ELengthUnits LengthUnit { get; set; }
        EMassUnits MassUnit { get; set; }
        ASTRADoc AstraDocument { get; }
        void SetProjectTitle();
        DrawingMenu D_Menu { get; }
        Form MainForm { get; set; }
        void SetMenu(string command_name, vdDocument doc);
        vdDocument Document { get; set; }
        string File_Name { get; }
        void WriteFile(string txt);
        void WriteFile(string txt, bool isAppend);
        bool IsMovingLoad { get; set; }
        string MovingLoadFile { get; set; }
        string UserFile { get; set; }
        void SaveUserFile();
        void SaveUserFile(vdDocument doc, string file_name);
        bool OpenUserFile();
        vdSelection GetGripSelection(bool Create);
        bool IsOpenFile { get; set; }

        MenuStrip Menu { get; set; }
        
        void EnableAllMenu(bool isEnable);
        string GetSelectedMembersInText();
        string GetSelectedJointsInText();

        CREATE_ASTRA_Data Create_Data { get; set; }

    }
    //Chiranjit [2012 04 29]
    public class CREATE_ASTRA_Data
    {
        public CREATE_ASTRA_Data()
        {
            Project_Title = "";
            Geometry_Data = new List<string>();
            Member_Truss_Data = new List<string>();
            Member_Release_Data = new List<string>();
            Section_Data = new List<string>();
            Material_Data = new List<string>();
            Support_Data = new List<string>();
            Load_Data = new List<string>();
            JointLoad_Data = new List<string>();
            SupportDisplacementLoad_Data = new List<string>();
            MemberLoad_Data = new List<string>();
            AreaLoad_Data = new List<string>();
            FloorLoad_Data = new List<string>();
            RepeatLoad_Data = new List<string>();
            CombinationLoad_Data = new List<string>();
            Moving_Load_Data = new List<string>();
            Analysis_Data = new List<string>();
            Finish_Statement_Data = new List<string>();

            Load_Cases = new List<LoadCaseDefinition>();
            Load_Combinations = new List<LoadCombinations>();


            Base_Unit = "";
            Section_Unit = "";
            Material_Unit = "";
            Load_Unit = "";
        }

        public string Project_Title { get; set; }
        public List<string> Geometry_Data { get; set; }
        public List<string> Member_Truss_Data { get; set; }
        public List<string> Member_Release_Data { get; set; }
        public List<string> Section_Data { get; set; }
        public List<string> Material_Data { get; set; }
        public List<string> Support_Data { get; set; }

        public List<LoadCaseDefinition> Load_Cases { get; set; }
        public List<LoadCombinations> Load_Combinations { get; set; }




        public List<string> Load_Data { get; set; }
        public List<string> JointLoad_Data { get; set; }
        public List<string> SupportDisplacementLoad_Data { get; set; }
        public List<string> MemberLoad_Data { get; set; }
        public List<string> AreaLoad_Data { get; set; }
        public List<string> FloorLoad_Data { get; set; }
        public List<string> RepeatLoad_Data { get; set; }
        public List<string> CombinationLoad_Data { get; set; }
        public List<string> Moving_Load_Data { get; set; }
        public List<string> Analysis_Data { get; set; }
        public List<string> Finish_Statement_Data { get; set; }


        public string Base_Unit { get; set; }
        public string Section_Unit { get; set; }
        public string Material_Unit { get; set; }
        public string Load_Unit { get; set; }

        public List<string> Get_ASTRA_Data()
        {
            List<string> ASTRA_Data = new List<string>();

            ASTRA_Data.Add(string.Format(""));
            ASTRA_Data.Add(string.Format(""));

            if (Project_Title == "")
                ASTRA_Data.Add(string.Format("ASTRA SPACE "));
            else
                ASTRA_Data.Add(string.Format(Project_Title));

            if (Project_Title == "")
                ASTRA_Data.Add(string.Format("UNIT KN MET"));
            else
                ASTRA_Data.Add(string.Format(Base_Unit));

            ASTRA_Data.AddRange((Geometry_Data));
            if (Section_Data.Count > 0)
            {
                ASTRA_Data.Add(string.Format(Section_Unit));
                ASTRA_Data.Add("SECTION PROPERTY");
                ASTRA_Data.AddRange((Section_Data));
            }
            if (Member_Truss_Data.Count > 0)
            {

                ASTRA_Data.Add("MEMBER TRUSS");
                ASTRA_Data.AddRange((Member_Truss_Data));
            }
            if (Member_Release_Data.Count > 0)
            {
                ASTRA_Data.Add("MEMBER RELEASE");
                ASTRA_Data.AddRange((Member_Release_Data));
            }
            if (Material_Data.Count > 0)
            {
                ASTRA_Data.Add(string.Format(Material_Unit));
                ASTRA_Data.Add("MATERIAL CONSTANTS");
                ASTRA_Data.AddRange((Material_Data));
            }
            if (Support_Data.Count > 0)
            {
                ASTRA_Data.Add("SUPPORTS");
                ASTRA_Data.AddRange((Support_Data));
            }

            if (Load_Unit != "")
            {
                ASTRA_Data.Add(Load_Unit);
            }
            if (Load_Data.Count > 0)
            {
                ASTRA_Data.AddRange(Load_Data);
            }
            if (JointLoad_Data.Count > 0)
            {
                ASTRA_Data.Add("JOINT LOAD");
                ASTRA_Data.AddRange((JointLoad_Data));
            }

            if (SupportDisplacementLoad_Data.Count > 0)
            {
                ASTRA_Data.Add("SUPPORT DISPLACEMENT LOAD");
                ASTRA_Data.AddRange((SupportDisplacementLoad_Data));
            }

            if (MemberLoad_Data.Count > 0)
            {
                ASTRA_Data.Add("MEMBER LOAD");
                ASTRA_Data.AddRange((MemberLoad_Data));
            }
            if (FloorLoad_Data.Count > 0)
            {
                ASTRA_Data.Add("FLOOR LOAD");
                ASTRA_Data.AddRange((FloorLoad_Data));
            }
            if (AreaLoad_Data.Count > 0)
            {
                ASTRA_Data.Add("AREA LOAD");
                ASTRA_Data.AddRange((AreaLoad_Data));
            }
            if (RepeatLoad_Data.Count > 0)
            {
                ASTRA_Data.Add("REPEAT LOAD");
                ASTRA_Data.AddRange((RepeatLoad_Data));
            }
            if (CombinationLoad_Data.Count > 0)
            {
                ASTRA_Data.Add("LOAD COMB");
                ASTRA_Data.AddRange((CombinationLoad_Data));
            }
            if (Moving_Load_Data.Count > 0)
            {
                //ASTRA_Data.Add("DEFINE MOVING LOAD FILE LL.TXT");
                ASTRA_Data.AddRange((Moving_Load_Data));
            }
            if (Analysis_Data.Count > 0)
            {
                ASTRA_Data.AddRange((Analysis_Data));
            }
            if (Finish_Statement_Data.Count > 0)
            {
                ASTRA_Data.AddRange((Finish_Statement_Data));
            }
            //ASTRA_Data.Add("PERFORM ANALYSIS");
            ASTRA_Data.Add("FINISH");

            return ASTRA_Data;
        }
    }
    public class LoadCaseDefinition
    {
        public LoadCaseDefinition()
        {
            LoadNo = 0;
            Title = "";
            //JointLoads = new List<string>();
            JointLoads = new List<SLoad>();
            JointWeights = new List<string>();
            SupportDisplacementLoads = new List<string>();
            MemberLoads = new List<string>();
            ElementLoads = new List<string>();
            AreaLoads = new List<string>();
            FloorLoads = new List<string>();
            TemperatureLoads = new List<string>();
            Comb_Loads = new LoadCombinations();
            RepeatLoads = new LoadRepeats();
            //SeismicLoads = new List<string>();
            
        }
        public int LoadNo { get; set; }
        public string Title { get; set; }
        public bool Is_Load_Combination
        {
            get
            {
                if (Comb_Loads == null) return false;

                if (Comb_Loads.Count == 0) return false;

                return true;
            }
        }
        //public List<string> JointLoads { get; set; }
        public List<SLoad> JointLoads { get; set; }
        public List<string> JointWeights { get; set; }

        public List<string> SupportDisplacementLoads { get; set; }
        public List<string> MemberLoads { get; set; }
        public List<string> ElementLoads { get; set; }
        public List<string> AreaLoads { get; set; }
        public List<string> FloorLoads { get; set; }
        public List<string> TemperatureLoads { get; set; }

        public LoadCombinations Comb_Loads { get; set; }
        public LoadCombinations RepeatLoads { get; set; }

        public override string ToString()
        {
            return string.Format("{0}:{1}", LoadNo, Title);
        }
    }
    public class LoadCombinations : List<string>
    {
        public LoadCombinations()
            : base()
        {
            LoadNo = 0;
            Name = "";
            //Combinations = new List<string>();
            LoadCases = new List<int>();
            Factors = new List<double>();
        }
        public int LoadNo { get; set; }
        public string Name { get; set; }
        //public List<string> Combinations { get; set; }
        public List<int> LoadCases { get; set; }
        public List<double> Factors { get; set; }

        public string Data { get; set; }

        public void Set_Combination()
        {
            string kStr = "";
            Clear();
            for (int i = 0; i < LoadCases.Count; i++)
            {

                //kStr += LoadCases[i] + " " + Factors[i].ToString("F3") + " ";
                kStr += LoadCases[i] + " " + Factors[i].ToString("F2") + " ";
                Add(string.Format("({0:f3}) x Load {1}", Factors[i], LoadCases[i]));
            }
            Data = kStr;
        }
    }

    public class LoadRepeats : LoadCombinations
    {
        public LoadRepeats()
            : base()
        {

        }
    }

}
