using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using HEADSNeed.ASTRA;
using HEADSNeed.ASTRA.ASTRAClasses;
//using MovingLoadAnalysis;

using HEADSNeed.ASTRA.ASTRADrawingTools;
using HEADSNeed.ASTRA.CadToAstra;
using System.IO;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Geometry;


namespace HEADSNeed.ASTRA.ASTRAClasses
{
    public class ASTRADoc
    {
        private string projTitle = "";
        private ELengthUnits base_lUnit = ELengthUnits.FT;
        private EMassUnits base_mUnit = EMassUnits.KIP;

        private ELengthUnits prop_lUnit = ELengthUnits.FT;
        private ELengthUnits load_lUnit = ELengthUnits.FT;
        private EMassUnits load_mUnit = EMassUnits.KIP;

        private JointCoordinateCollection jccJointCollection = null;
        private MemberIncidenceCollection micMemberCollection = null;

        MemberGroupCollection Groups = null;

        private SupportCollection scSupportCollection = null;
        private JointLoadCollection jlcCollection = null;
        private MemberLoadCollection mloadCollection = null;
        private ElementCollection elmtCollection = null;
        private MemberPropertyCollection memProps = null;
        private BeamAnalysisCollection beamAnalysisCol = null;
        private BeamAnalysisCalculator beamAnalysisCalculator = null;
        private LoadGenerationCollection load_generation = null;
        private string fileName = "";
        private bool bIsMovingLoad = false;
        private bool bIsDynamic = false;
        double max_x = 0.0d;

        public double Seismic_Coeeficient { get; set; }

        public List<LoadDefine> LoadDefines { get; set; }
        public List<string> SeismicLoads { get; set; }

        public LoadDefine DefineLoad { get; set; }


        public List<MaterialProperty> Constants { get; set; }
        public List<string> JointSupports { get; set; }
        public List<string> MemberGroups { get; set; }
        public List<string> MemberProps { get; set; }
        public List<string> ElementProps { get; set; }
        public List<string> MemberCables { get; set; }
        public List<string> MemberTrusses { get; set; }
        public List<string> MemberReleases { get; set; }

        public string SelfWeight_Direction { get; set; }
        public string SelfWeight { get; set; }


        public List<MovingLoadData> MovingLoads { get; set; }
        public List<LiveLoad> LL_Definition { get; set; }


        public PrintAnalysis Analysis_Specification { get; set; }
        public List<string> DynamicAnalysis { get; set; }
        public string FREQUENCIES { get; set; }


        List<double> Floor_Level { get; set; }

        public ASTRADoc()
        {
            Initialize();

        }
        ~ASTRADoc()
        {
            jccJointCollection = null;
            micMemberCollection = null;
            scSupportCollection = null;
            jlcCollection = null;
            mloadCollection = null;
            elmtCollection = null;
            memProps = null;
            beamAnalysisCol = null;
            beamAnalysisCalculator = null;
            load_generation = null;
            fileName = "";
            bIsMovingLoad = false;
            bIsDynamic = false;
            max_x = 0.0d;

            AnalysisData = null;
            LoadDefines = null;
            DefineLoad = null;
        }

        public void Initialize()
        {
            projTitle = "ASTRA";
            jccJointCollection = new JointCoordinateCollection();
            micMemberCollection = new MemberIncidenceCollection();
            scSupportCollection = new SupportCollection();
            jlcCollection = new JointLoadCollection();
            mloadCollection = new MemberLoadCollection();
            elmtCollection = new ElementCollection();
            memProps = new MemberPropertyCollection();
            beamAnalysisCol = new BeamAnalysisCollection();
            beamAnalysisCalculator = new BeamAnalysisCalculator();
            load_generation = new LoadGenerationCollection();

            LoadDefines = new List<LoadDefine>();
            SeismicLoads = new List<string>();
            DefineLoad = new LoadDefine();


            Constants = new List<MaterialProperty>();
            MemberGroups = new List<string>();
            MemberProps = new List<string>();
            ElementProps = new List<string>();
            MemberCables = new List<string>();
            MemberTrusses = new List<string>();
            MemberReleases = new List<string>();

            JointSupports = new List<string>();
            SelfWeight_Direction = "";
            SelfWeight = "";
            FREQUENCIES = "";


            MovingLoads = new List<MovingLoadData>();
            LL_Definition = new List<LiveLoad>();

            Analysis_Specification = new PrintAnalysis();

            DynamicAnalysis = new List<string>();
            
            //Chiranjit [2015 04 01]
            Floor_Level = new List<double>();

            Seismic_Coeeficient = 0.0;
        }

        public ASTRAAnalysisData AnalysisData { get; set; }

        public double Skew_Angle
        {
            get
            {
                try
                {
                    if (jccJointCollection.Count > 1)
                    {
                        return (int)((180.0 / Math.PI) * Math.Atan((jccJointCollection[1].Point.x / jccJointCollection[1].Point.z)));
                    }
                }
                catch (Exception ex) { }
                return 0.0;
            }
        }

        public void Set_Floor_Level()
        {
            Floor_Level.Clear();

            foreach (var item in jccJointCollection)
            {
                if (!Floor_Level.Contains(item.Y))
                {
                    Floor_Level.Add(item.Y);
                }
            }

        }
        public ASTRADoc(ASTRADoc obj)
        {
            try
            {
                Initialize();


                //jccJointCollection = new JointCoordinateCollection();
                //micMemberCollection = new MemberIncidenceCollection();
                //scSupportCollection = new SupportCollection();
                //jlcCollection = new JointLoadCollection();
                //mloadCollection = new MemberLoadCollection();
                //elmtCollection = new ElementCollection();
                //memProps = new MemberPropertyCollection();
                AnalysisData = new ASTRAAnalysisData(obj.FileName);

                jccJointCollection = obj.jccJointCollection;

                micMemberCollection = obj.micMemberCollection;
                scSupportCollection = obj.scSupportCollection;
                jlcCollection = obj.jlcCollection;
                mloadCollection = obj.mloadCollection;
                elmtCollection = obj.elmtCollection;
                memProps = obj.memProps;





                //projTitle = "";
                //lengthUnit = ELengthUnits.FT;
                //massUnit = EMassUnits.KIP;
                //jccJointCollection = null;
                //micMemberCollection = null;

                //scSupportCollection = null;
                //jlcCollection = null;
                //mloadCollection = null;
                //elmtCollection = null;
                //memProps = null;
                //beamAnalysisCol = null;
                //beamAnalysisCalculator = null;
                //load_generation = null;
                //fileName = "";
                //bIsMovingLoad = false;
                //bIsDynamic = false;
                //max_x = 0.0d;


                //LoadDefines  = new List<LoadDefine>();
                //DefinesLoad  = new LoadDefine();

            }
            catch (Exception ex) { }
        }
        public ASTRADoc(string fullFileName)
        {
            Initialize();
            //jccJointCollection = new JointCoordinateCollection();
            //micMemberCollection = new MemberIncidenceCollection();
            //scSupportCollection = new SupportCollection();
            //jlcCollection = new JointLoadCollection();
            //mloadCollection = new MemberLoadCollection();
            //elmtCollection = new ElementCollection();
            //memProps = new MemberPropertyCollection();
            fileName = fullFileName;
            string ext = Path.GetExtension(fullFileName).ToLower();
            string ast_file = "";


            if (ext == ".ast")
                ast_file = Path.Combine(Path.GetDirectoryName(fileName), Path.GetFileNameWithoutExtension(fileName) + ".txt");
            if (File.Exists(ast_file))
            {
                fileName = ast_file;
                ext = ".txt";
            }


            try
            {

                AnalysisData = new ASTRAAnalysisData(fileName);


                if (!File.Exists(fileName))
                {
                    throw new FileNotFoundException();
                }
                else
                {
                    if (ext == ".txt")
                    {
                        try
                        {
                            SetASTRADocFromTXT();
                            //Members.Read_Member_Groups(fullFileName);
                        }
                        catch (Exception ex) { }
                        //}
                    }
                    else if (ext == ".ast")
                    {
                        SetASTRADocFromAST();
                    }
                    micMemberCollection.CopyJointCoordinates(jccJointCollection);
                    scSupportCollection.CopyFromCoordinateCollection(jccJointCollection);
                    JointLoads.CopyCoordinates(jccJointCollection);
                    MemberLoads.CopyMember(micMemberCollection);
                    Elements.CopyCoordinates(jccJointCollection);
                    MemberProperties.CopyMemberIncidence(micMemberCollection);

                    if (IsMovingLoad)
                    {
                        JointLoads.SetMovingJointLoad(FileName);
                        JointLoads.CopyCoordinates(Joints);
                    }
                }
            }
            catch (Exception ex) { }

            Set_Floor_Level();
        }
        public string FileName
        {
            get
            {
                return fileName;
            }
        }
        public string AnalysisFileName
        {
            get
            {
                string f = Path.Combine(Path.GetDirectoryName(fileName), "ANALYSIS_REP.TXT");
                if (!File.Exists(f))
                    f = Path.Combine(Path.GetDirectoryName(fileName), "analysis_rep.txt");
                return f;
            }
        }
        public string MemberForce_File
        {
            get
            {
                if (File.Exists(AnalysisFileName))
                    return Path.Combine(Path.GetDirectoryName(AnalysisFileName), "MemberForces.txt");
                return "";
            }
        }

        public string ProjectTitle
        {
            get
            {
                return projTitle;
            }
            set
            {
                projTitle = value;
            }
        }
        public string UserTitle
        {
            get
            {
                if (projTitle != "")
                {
                    MyStrings mlist = new MyStrings(MyStrings.RemoveAllSpaces(projTitle), ' ');

                    if (mlist.Count > 1)
                        return mlist.GetString(2);
                }
                return "";
            }
        }
        public string StructureType
        {
            get
            {
                if (projTitle != "")
                {
                    MyStrings mlist = new MyStrings(MyStrings.RemoveAllSpaces(projTitle), ' ');
                    if (mlist.Count > 1)
                        return mlist.StringList[1];

                }
                return "";
            }
        }

        public EMassUnits Base_MassUnit
        {
            get
            {
                return base_mUnit;
            }
            set
            {
                base_mUnit = value;
            }
        }
        public ELengthUnits Base_LengthUnit
        {
            get
            {
                return base_lUnit;
            }
            set
            {
                base_lUnit = value;
            }
        }

        public ELengthUnits Prop_LengthUnit
        {
            get
            {
                return prop_lUnit;
            }
            set
            {
                prop_lUnit = value;
            }
        }
        public EMassUnits Const_MassUnit { get; set; }
        public ELengthUnits Const_LengthUnit { get; set; }
        public EMassUnits Load_MassUnit
        {
            get
            {
                return load_mUnit;
            }
            set
            {
                load_mUnit = value;
            }
        }
        public ELengthUnits Load_LengthUnit
        {
            get
            {
                return load_lUnit;
            }
            set
            {
                load_lUnit = value;
            }
        }



        public JointLoadCollection JointLoads
        {
            get
            {
                return jlcCollection;
            }
            set
            {
                jlcCollection = value;
            }
        }
        public JointCoordinateCollection Joints
        {
            get
            {
                return jccJointCollection;
            }
            set
            {
                jccJointCollection = value;
            }
        }
        public MemberIncidenceCollection Members
        {
            get
            {
                return micMemberCollection;
            }
            set
            {
                micMemberCollection = value;
            }
        }
        public SupportCollection Supports
        {
            get
            {
                return scSupportCollection;
            }
            set
            {
                scSupportCollection = value;
            }
        }
        public MemberLoadCollection MemberLoads
        {
            get
            {
                return mloadCollection;
            }
            set
            {
                mloadCollection = value;
            }
        }
        public ElementCollection Elements
        {
            get
            {
                return elmtCollection;
            }
            set
            {
                elmtCollection = value;
            }
        }

        public LoadGenerationCollection Load_Geneartion
        {
            get
            {
                return load_generation;
            }
            set
            {
                load_generation = value;
            }
        }
        public MemberPropertyCollection MemberProperties
        {
            get
            {
                return memProps;
            }
            set
            {
                memProps = value;
            }
        }


        public bool IsMovingLoad
        {
            get
            {
                return bIsMovingLoad;
            }
        }
        public bool IsDynamicLoad
        {
            get
            {
                return bIsDynamic;
            }
            set
            {
                bIsDynamic = value;
            }
        }

        public BeamAnalysisCollection BeamAnalysisColl
        {
            get
            {
                return beamAnalysisCol;
            }
        }
        public BeamAnalysisCalculator BeamAnalysisCalc
        {
            get
            {
                return beamAnalysisCalculator;
            }
        }
        private void SetASTRADocFromAST()
        {

            StreamReader sr = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
            string kStr = "";
            string option = "";

            try
            {
                while (!sr.EndOfStream)
                {
                    kStr = sr.ReadLine().ToUpper();
                    if (ProjectTitle == "ASTRA")
                        ProjectTitle = kStr;

                    #region Option Strings

                    if (kStr.StartsWith("N001"))
                    {
                        option = "JOINT COORDINATE";
                    }
                    else if (kStr.StartsWith("N002"))
                    {
                        option = "MEMB INCI";
                    }

                    else if (kStr.StartsWith("N003"))
                    {
                        option = "MEMBER PROPERTY";
                    }
                    else if (kStr.StartsWith("N004"))
                    {
                        option = "MEMBER PROPERTY";
                    }

                    else if (kStr.StartsWith("N005"))
                    {
                        option = "SUPPORT";
                    }

                    else if (kStr.StartsWith("N006"))
                    {
                        option = "MEMBER LOAD";
                        // N007  UNIT 1.000 1.000
                        MyStrings mList = new MyStrings(MyStrings.RemoveAllSpaces(kStr), ' ');
                        if (mList.Count > 13)
                        {
                            loadUnitFac = mList.GetDouble(3);
                        }
                    }
                    else if (kStr.StartsWith("N007"))
                    {
                        option = "JOINT LOAD";
                    }
                    else if (kStr.StartsWith("N008"))
                    {
                        option = "TRUSS";
                    }
                    else if (kStr.StartsWith("N018"))
                    {
                        option = "ELEMENT";
                    }
                    else if (kStr.StartsWith("N012"))
                    {
                        option = "LOAD GENERATION";
                    }
                    else if (kStr.StartsWith("N013"))
                    {
                        option = "LOAD COMB";
                    }
                    else if (kStr.StartsWith("N099"))
                    {
                        MyStrings mLst = new MyStrings(MyStrings.RemoveAllSpaces(kStr), ' ');
                        if (mLst.Count == 5)
                        {
                            if (mLst.GetInt(2) > 0)
                                option = "DYNAMIC";
                        }

                    }

                    #region DUMP
                    //if (kStr.StartsWith("N001"))
                    //{
                    //    option = "N001";
                    //}
                    //else if (kStr.StartsWith("N002"))
                    //{
                    //    option = "N002";
                    //}
                    //else if (kStr.StartsWith("N003"))
                    //{
                    //    option = "N003";
                    //}
                    //else if (kStr.StartsWith("N004"))
                    //{
                    //    option = "N004";
                    //}
                    //else if (kStr.StartsWith("N005"))
                    //{
                    //    option = "N005";
                    //}
                    //else if (kStr.StartsWith("N006"))
                    //{
                    //    option = "N006";
                    //}
                    //else if (kStr.StartsWith("N007"))
                    //{
                    //    option = "N007";
                    //}
                    #endregion
                    else
                    {
                        option = "";
                    }

                    #endregion
                    switch (option)
                    {
                        case "JOINT COORDINATE":
                            try
                            {
                                Joints.Add(JointCoordinate.ParseAST(kStr));
                            }
                            catch (Exception exx) { }
                            break;
                        case "MEMB INCI":
                            try
                            {
                                Members.Add(MemberIncidence.ParseAST(kStr));
                            }
                            catch (Exception exx) { }
                            break;
                        case "SUPPORT":
                            try
                            {
                                Supports.Add(Support.ParseAST(kStr));
                            }
                            catch (Exception exx) { }
                            break;
                        case "JOINT LOAD":
                            try
                            {
                                JointLoads.AddAST(kStr);
                            }
                            catch (Exception exx) { }
                            break;
                        case "MEMBER LOAD":
                            try
                            {
                                MemberLoads.AddAST(kStr);
                            }
                            catch (Exception exx) { }
                            break;
                        case "MEMBER PROPERTY":
                            try
                            {
                                MemberProperties.AddAST(kStr);
                            }
                            catch (Exception exx) { }
                            break;
                        case "TRUSS":
                            try
                            {
                                Members.AddAST(kStr);
                            }
                            catch (Exception exx) { }
                            break;
                        case "ELEMENT":
                            try
                            {
                                Elements.AddAST(kStr);
                                //Elements.AddAST(kStr);
                            }
                            catch (Exception exx) { }
                            break;
                        case "LOAD GENERATION":
                            try
                            {
                                bIsMovingLoad = true;
                                load_generation.Read_Type_From_Text_File(fileName);

                            }
                            catch (Exception exx) { }
                            break;
                        case "LOAD COMB":
                            try
                            {
                                MyStrings mlist = new MyStrings(MyStrings.RemoveAllSpaces(kStr), ' ');

                                foreach (string str in mlist.StringList)
                                {
                                    MemberLoads.SetLoadCombination(mlist.GetInt(2), mlist.GetInt(1), mlist.GetDouble(3));
                                    JointLoads.SetLoadCombination(mlist.GetInt(2), mlist.GetInt(1), mlist.GetDouble(3));
                                    //MemberLoads.SetLoadCombination(str, LoadCaseNo);
                                    //JointLoads.SetLoadCombination(str, LoadCaseNo);
                                }

                            }
                            catch (Exception exx) { }
                            break;
                        case "DYNAMIC":
                            try
                            {
                                bIsDynamic = true;
                            }
                            catch (Exception exx) { }
                            break;
                        case "FINISH":
                            break;
                    }
                }
            }
            catch (Exception exx) { }
            finally
            {
                sr.Close();
            }
        }
        private void SetASTRADocFromTXT()
        {
            List<String> lstStr = new List<string>();
            //StreamReader sr = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
            string kStr = "";
            string option = "";

            int LoadCaseNo = 0;
            string LoadTitle = "";

            //int LoadCombination = -1;
            string comment = "";
            bool isComb = false;
            bool find_astra = false;
            List<string> fileLines = new List<string>(File.ReadAllLines(fileName));
            while (fileLines.Contains(""))
            {
                fileLines.Remove("");
            }

            //fileLines = fileLines;
            int lineNo = -1;

            MaterialProperty Mat_Prop = null;
            try
            {
                MyStrings mList = null;
                do
                {
                    //if (lineNo > 328)
                    //{
                    //    lineNo++;
                    //}
                    //else
                        lineNo++;

                    //lineNo = 330;

                    //kStr = "";
                    kStr = MyStrings.RemoveAllSpaces(fileLines[lineNo].ToUpper());


                    if (kStr.StartsWith("ASTRA")) find_astra = true;



                    if (!find_astra) continue;


                    if (kStr.StartsWith("FINI"))
                    {
                        find_astra = false;
                        break;
                    }

                    if (fileLines[lineNo].EndsWith("-"))
                    {
                        //kStr += fileLines[lineNo].ToUpper();
                        while (fileLines[lineNo].EndsWith("-"))
                        {
                            //kStr += MyStrings.RemoveAllSpaces(fileLines[lineNo].ToUpper()).Replace("-",
                            //    fileLines[++lineNo].ToUpper());


                            kStr +=  fileLines[++lineNo].ToUpper();
                            //lineNo++;
                        }
                        kStr = MyStrings.RemoveAllSpaces(kStr.ToUpper()).Replace("-"," ");

                    }
                    //else
                    //{
                    //    kStr = MyStrings.RemoveAllSpaces(fileLines[lineNo].ToUpper());
                    //}


                    if (kStr.StartsWith("*"))
                    {
                        comment = kStr; 
                        continue;
                    }
                    if (kStr.StartsWith("//")) continue;
                    //kStr = sr.ReadLine().ToUpper();

                    #region Option Strings

                    //if (lineNo > 246)
                    //    mList = new MyList(kStr.ToUpper(), ' ');


                    mList = new MyStrings(kStr.ToUpper(), ' ');
                    if (kStr.Contains("ASTRA"))
                    {
                        ProjectTitle = kStr;
                    }
                    #region UNIT
                    if (kStr.StartsWith("UNIT"))
                    {
                        option = "";
                        int c = 1;

                        int uflag = 0;
                        if (fileLines[lineNo + 1].ToUpper().StartsWith("JOI"))
                        {
                            uflag = 0; // Base unit
                        }
                        else if (fileLines[lineNo + 1].ToUpper().StartsWith("SEC") ||
                            fileLines[lineNo + 1].ToUpper().StartsWith("MEM"))
                        {
                            uflag = 1; // Property unit
                        }
                        else if (fileLines[lineNo + 1].ToUpper().StartsWith("LOAD"))
                        {
                            uflag = 2; // Load unit
                        }
                        else if (fileLines[lineNo + 1].ToUpper().StartsWith("MAT") ||
                            fileLines[lineNo + 1].ToUpper().StartsWith("CON"))
                        {
                            uflag = 3; // Material Constant
                        }
                        do
                        {
                            if (mList.StringList[c].StartsWith("MTO"))
                            {
                                if (uflag == 0)
                                    Base_MassUnit = EMassUnits.MTON;
                                else if (uflag == 2)
                                    Load_MassUnit = EMassUnits.MTON;
                                else if (uflag == 3)
                                    Const_MassUnit = EMassUnits.MTON;
                            }
                            else if (mList.StringList[c].StartsWith("TO"))
                            {
                                if (uflag == 0)
                                    Base_MassUnit = EMassUnits.MTON;
                                else if (uflag == 2)
                                    Load_MassUnit = EMassUnits.MTON;
                                else if (uflag == 3)
                                    Const_MassUnit = EMassUnits.MTON;
                            }
                            else if (mList.StringList[c].StartsWith("GM"))
                            {
                                if (uflag == 0)
                                    Base_MassUnit = EMassUnits.GMS;
                                else if (uflag == 2)
                                    Load_MassUnit = EMassUnits.GMS;
                                else if (uflag == 3)
                                    Const_MassUnit = EMassUnits.GMS;
                            }
                            else if (mList.StringList[c].StartsWith("KG"))
                            {
                                if (uflag == 0)
                                    Base_MassUnit = EMassUnits.KG;
                                else if (uflag == 2)
                                    Load_MassUnit = EMassUnits.KG;
                                else if (uflag == 3)
                                    Const_MassUnit = EMassUnits.KG;
                            }
                            else if (mList.StringList[c].StartsWith("KIP"))
                            {
                                if (uflag == 0)
                                    Base_MassUnit = EMassUnits.KIP;
                                else if (uflag == 2)
                                    Load_MassUnit = EMassUnits.KIP;
                                else if (uflag == 3)
                                    Const_MassUnit = EMassUnits.KIP;
                            }
                            else if (mList.StringList[c].StartsWith("KN"))
                            {
                                if (uflag == 0)
                                    Base_MassUnit = EMassUnits.KN;
                                else if (uflag == 2)
                                    Load_MassUnit = EMassUnits.KN;
                                else if (uflag == 3)
                                    Const_MassUnit = EMassUnits.KN;
                            }
                            else if (mList.StringList[c].StartsWith("LB"))
                            {
                                if (uflag == 0)
                                    Base_MassUnit = EMassUnits.LBS;
                                else if (uflag == 2)
                                    Load_MassUnit = EMassUnits.LBS;
                                else if (uflag == 3)
                                    Const_MassUnit = EMassUnits.LBS;
                            }
                            else if (mList.StringList[c].StartsWith("NEW"))
                            {
                                if (uflag == 0)
                                    Base_MassUnit = EMassUnits.NEW;
                                else if (uflag == 2)
                                    Load_MassUnit = EMassUnits.NEW;
                                else if (uflag == 3)
                                    Const_MassUnit = EMassUnits.NEW;
                            }
                            else if (mList.StringList[c].StartsWith("ME"))
                            {
                                if (uflag == 0)
                                    Base_LengthUnit = ELengthUnits.M;
                                else if (uflag == 1)
                                    Prop_LengthUnit = ELengthUnits.M;
                                else if (uflag == 2)
                                    Load_LengthUnit = ELengthUnits.M;
                                else if (uflag == 3)
                                    Const_LengthUnit = ELengthUnits.M;
                            }
                            else if (mList.StringList[c].StartsWith("CM"))
                            {
                                if (uflag == 0)
                                    Base_LengthUnit = ELengthUnits.CM;
                                else if (uflag == 1)
                                    Prop_LengthUnit = ELengthUnits.CM;
                                else if (uflag == 2)
                                    Load_LengthUnit = ELengthUnits.CM;
                                else if (uflag == 3)
                                    Const_LengthUnit = ELengthUnits.CM;
                            }
                            else if (mList.StringList[c].StartsWith("FT"))
                            {
                                if (uflag == 0)
                                    Base_LengthUnit = ELengthUnits.FT;
                                else if (uflag == 1)
                                    Prop_LengthUnit = ELengthUnits.FT;
                                else if (uflag == 2)
                                    Load_LengthUnit = ELengthUnits.FT;
                                else if (uflag == 3)
                                    Const_LengthUnit = ELengthUnits.FT;
                            }
                            else if (mList.StringList[c].StartsWith("IN"))
                            {
                                if (uflag == 0)
                                    Base_LengthUnit = ELengthUnits.INCH;
                                else if (uflag == 1)
                                    Prop_LengthUnit = ELengthUnits.INCH;
                                else if (uflag == 2)
                                    Load_LengthUnit = ELengthUnits.INCH;
                                else if (uflag == 3)
                                    Const_LengthUnit = ELengthUnits.INCH;
                            }
                            else if (mList.StringList[c].StartsWith("MM"))
                            {
                                if (uflag == 0)
                                    Base_LengthUnit = ELengthUnits.MM;
                                else if (uflag == 1)
                                    Prop_LengthUnit = ELengthUnits.MM;
                                else if (uflag == 2)
                                    Load_LengthUnit = ELengthUnits.MM;
                                else if (uflag == 3)
                                    Const_LengthUnit = ELengthUnits.MM;
                            }
                            else if (mList.StringList[c].StartsWith("YD"))
                            {
                                if (uflag == 0)
                                    Base_LengthUnit = ELengthUnits.YDS;
                                else if (uflag == 1)
                                    Prop_LengthUnit = ELengthUnits.YDS;
                                else if (uflag == 2)
                                    Load_LengthUnit = ELengthUnits.YDS;
                                else if (uflag == 3)
                                    Const_LengthUnit = ELengthUnits.YDS;
                            }

                            c++;
                        }
                        while (c < mList.Count);


                        if (uflag == 0)
                        {
                            Prop_LengthUnit = Base_LengthUnit;

                            Const_MassUnit = Base_MassUnit;
                            Const_LengthUnit = Base_LengthUnit;

                            Load_MassUnit = Base_MassUnit;
                            Load_LengthUnit = Base_LengthUnit;
                        }
                    }
                    #endregion UNIT

                    if (mList.Count == 2)
                    {
                        if (mList.StringList[0].Length > 3)
                            mList.StringList[0] = mList.StringList[0].Substring(0, 3);
                        if (mList.StringList[1].Length > 3)
                            mList.StringList[1] = mList.StringList[1].Substring(0, 3);

                        if (mList.StringList[0].ToUpper().Contains("JOI") &&
                            (mList.StringList[1].ToUpper().Contains("COO")))
                        {
                            option = "JOINT COORDINATE";
                            continue;
                        }
                        if (mList.StringList[0].ToUpper().Contains("MEM") ||
                            mList.StringList[0].ToUpper().Contains("SEC"))
                        {
                            if ((mList.StringList[1].ToUpper().Contains("INC")) || 
                                (mList.StringList[1].ToUpper().Contains("CON")))
                            {
                                option = "MEMB INCI";
                                continue;
                            }
                            else if (mList.StringList[1].ToUpper().Contains("PRO"))
                            {
                                option = "MEMBER PROPERTY"; continue;
                            }
                        }

                        if (mList.StringList[0].ToUpper().Contains("ELE"))
                        {
                            if ((mList.StringList[1].ToUpper().Contains("INC")) ||
                                (mList.StringList[1].ToUpper().Contains("CON")))
                            {
                                option = "ELEMENT INCIDENCE"; continue;
                            }
                            else if (mList.StringList[1].ToUpper().Contains("PRO"))
                            {
                                option = "ELEMENT PROP"; continue;
                            }
                        }
                        if (mList.StringList[0].ToUpper().Contains("MAT"))
                        {
                            if (mList.StringList[1].ToUpper().Contains("CON"))
                            {
                                if (Mat_Prop == null)
                                    Mat_Prop = new MaterialProperty();
                                else
                                {
                                    Mat_Prop = new MaterialProperty();
                                }
                                Constants.Add(Mat_Prop);
                                option = "CONSTANT"; continue;
                            }
                        }

                        //if (kStr.Contains("MEMB INCI") || kStr.Contains("MEMBER INCIDENCE") || kStr.Contains("MEMBER INCI"))
                        //{
                        //    option = "MEMB INCI";
                        //}
                    

                        //if (kStr.Contains("JOINT COORD") || kStr.Contains("JOINT COORDINATE"))
                        //{
                        //    option = "JOINT COORDINATE";
                        //    continue;
                        //}
                        //if (kStr.Contains("MEMB INCI") || kStr.Contains("MEMBER INCIDENCE") || kStr.Contains("MEMBER INCI"))
                        //{
                        //    option = "MEMB INCI";
                        //}
                    }
                    if (kStr.ToUpper().StartsWith("CONSTANT"))
                    {
                        if (Mat_Prop == null)
                            Mat_Prop = new MaterialProperty();
                        else
                        {
                            Mat_Prop = new MaterialProperty();
                        }
                        Constants.Add(Mat_Prop);


                        option = "CONSTANT"; continue;
                    }
                    if (kStr.StartsWith("START GROUP"))
                    {
                        option = "GROUP"; continue;
                    }
                    if (option == "GROUP" && kStr.StartsWith("END"))
                    {
                        option = ""; continue;
                    }

                    if (kStr.StartsWith("SUPPORT DISPLACEMENT"))
                    {
                        //[261]	"SUPPORT DISPLACEMENT LOAD"	string

                        option = "SUPPORT DISPACEMENT LOAD"; continue;
                    }
                    if (kStr.StartsWith("SUPPORT"))
                    {
                        option = "SUPPORT"; continue;
                    }

                    //if (kStr.Contains("CONSTANT"))
                    //{
                    //    option = "CONSTANT"; continue;
                    //}
                    if (kStr.Trim().TrimEnd().TrimStart().StartsWith("LOAD")
                        && (!kStr.Contains("GENERATION")))
                    {
                        lstStr.Clear();
                        lstStr.AddRange(kStr.Split(new char[] { ' ' }));
                        if(mList.Count > 2)
                         LoadTitle = mList.GetString(2);
                        else
                            LoadTitle = "";

                        if (lstStr.Count == 3)
                        {
                            if (int.TryParse(lstStr[1], out LoadCaseNo))
                            {
                                isComb = true;
                                option = "LOAD";
                                //continue;
                            }
                        }
                        else
                        {
                            if (int.TryParse(lstStr[1], out LoadCaseNo))
                            {
                                isComb = false;
                                option = "LOAD"; continue;
                            }
                        }
                    }
                    if (kStr.Contains("SELF"))
                    {
                        option = "SELF";

                        if (mList.Count > 2)
                        {
                            SelfWeight_Direction = mList.StringList[1];
                            SelfWeight = mList.StringList[2];
                        }
                        if (LoadCaseNo != 0)
                        {


                            if (DefineLoad.LoadCase != LoadCaseNo)
                            {
                                DefineLoad = new LoadDefine();
                                LoadDefines.Add(DefineLoad);
                            }
                            DefineLoad.LoadCase = LoadCaseNo;
                            DefineLoad.LoadTitle = LoadTitle;
                            DefineLoad.Selfweight = kStr;
                            //DefineLoad.ElementLoadList.Add(str);

                        }

                        continue;
                    }
                    if (kStr.Contains("MEMB TRUSS") || kStr.Contains("MEMBER TRUSS"))
                    {
                        option = "MEMBER TRUSS"; continue;
                    }
                    if (kStr.Contains("MEMB CABLE") || kStr.Contains("MEMBER CABLE"))
                    {
                        option = "MEMBER CABLE"; continue;
                    }
                    if (kStr.Contains("MEMB RELEASE") || kStr.Contains("MEMBER RELEASE"))
                    {
                        option = "MEMB RELEASE"; continue;
                    }
                    if (kStr.Contains("SELFWEIGHT"))
                    {
                        option = "SELFWEIGHT"; continue;
                    }
                    if (kStr.Contains("JOINT LOA"))
                    {
                        option = "JOINT LOAD"; continue;
                    }
                    if (kStr.Contains("JOINT WEI"))
                    {
                        option = "JOINT WEIGHT"; continue;
                    }
                    if (kStr.Contains("MEMB LOAD") || kStr.Contains("MEMBER LOAD"))
                    {
                        option = "MEMBER LOAD"; continue;
                    }
                    if (kStr.Contains("ELEM LOAD") || kStr.Contains("ELEMENT LOAD"))
                    {
                        option = "ELEMENT LOAD"; continue;
                    }
                    if (kStr.Contains("LOAD COMB"))
                    {
                        lstStr.Clear();
                        lstStr.AddRange(kStr.Split(new char[] { ' ' }));

                        if (mList.Count > 3)
                            LoadTitle = mList.GetString(3);
                        else
                            LoadTitle = "";
                        if (lstStr.Count >= 3)
                        {
                            if (int.TryParse(lstStr[2], out LoadCaseNo))
                            {
                                isComb = true;
                                //option = "LOAD"; continue;
                                option = "LOAD COMB"; continue;
                            }
                        }
                        else
                        {     
                            if (int.TryParse(lstStr[1], out LoadCaseNo))
                            {
                                isComb = false;
                                option = "LOAD COMB"; continue;
                                //option = "LOAD"; continue;
                            }
                        }

                        //option = "LOAD COMB"; continue;
                    }

                    if (kStr.StartsWith("SEISMIC COEEFICIENT") ||
                       kStr.StartsWith("SEIS COEEF") ||
                       kStr.StartsWith("SEIS COEF"))
                    {
                        //option = "SEISMIC COEEFICIENT"; continue;

                        mList = new MyStrings(kStr, ' ');
                        Seismic_Coeeficient = mList.GetDouble(2);
                        continue;

                    }
                    if (kStr.Contains("PRINT") && !kStr.Contains("INTERVAL"))
                    {
                        option = "PRINT";

                        if (kStr.Contains("PRINT ANALYSIS ALL"))
                            Analysis_Specification.Print_Analysis_All = true;
                        else if (kStr.StartsWith("PRINT SUPPORT REACTION"))
                        {
                            Analysis_Specification.Print_Support_Reaction = true;
                        }
                        else if (kStr.StartsWith("PRINT STATIC CHECK"))
                        {
                            Analysis_Specification.Print_Static_Check = true;
                        }
                        else if (kStr.StartsWith("PRINT LOAD DATA"))
                        {
                            Analysis_Specification.Print_Load_Data = true;
                        }
                        else if (kStr.StartsWith("PRINT MAX FORCE"))
                        {
                            Analysis_Specification.Print_Max_Force = true;
                            if(mList.StringList.Contains("LIST"))
                            {
                                Analysis_Specification.List_Maxforce.Add(mList.GetString(mList.StringList.IndexOf("LIST") + 1));
                            }
                        }
                        continue;
                    }
                    if (kStr.Contains("PERFORM ANALYSIS"))
                    {
                        Analysis_Specification.Perform_Analysis = true;
                        option = "PERFORM ANALYSIS"; continue;
                    }
                    if (kStr.Contains("FINISH"))
                    {
                        option = "FINISH"; continue;
                    }
                    if (kStr.Contains("LOADING"))
                    {
                        option = "LOADING"; continue;
                    }
                    if (kStr.Contains("TEMP LOAD"))
                    {
                        option = "TEMP LOAD"; continue;
                    }
                    if (kStr.Contains("AREA LOAD"))
                    {
                        option = "AREA LOAD"; continue;
                    }
                    if (kStr.Contains("FLOOR LOAD"))
                    {
                        option = "FLOOR LOAD"; continue;
                    }
                    if (kStr.Contains("REPEAT LOAD"))
                    {
                        //LoadCaseNo++;
                        option = "REPEAT LOAD"; continue;
                    }
                    if (kStr.Contains("ELEMENT INCIDENCE"))
                    {
                        option = "ELEMENT INCIDENCE"; continue;
                    }
                    if (kStr.Contains("ELEMENT PROP") ||
                        kStr.Contains("ELEMENT PROPERTIES") ||
                        kStr.Contains("ELEMENT PROPERTY"))
                    {
                        option = "ELEMENT PROP"; continue;
                    }
                    if (kStr.Contains("ELEMENT LOAD"))
                    {
                        option = "ELEMENT LOAD"; continue;
                    }
                    if (kStr.Contains("DEFINE MOVING"))
                    {
                        option = "DEFINE MOVING"; 
                        
                        string ll_txt = Path.Combine(Path.GetDirectoryName(fileName), "LL.TXT");



                        if (File.Exists(ll_txt))
                            MovingLoads = MovingLoadData.GetMovingLoads(ll_txt);

                        load_generation.Read_Type_From_Text_File(fileName);
                        
                        LiveLoad ll = new LiveLoad();
                        
                        for (var item = 0; item < load_generation.Count; item++)
                        {
                            ll = new LiveLoad();
                            ll.Type = load_generation[item].TypeNo;
                            ll.X_Distance = load_generation[item].X;
                            ll.Y_Distance = load_generation[item].Y;
                            ll.Z_Distance = load_generation[item].Z;
                            ll.X_Increment = load_generation[item].XINC;
                            LL_Definition.Add(ll);
                        }
                      

                        continue;
                    }
                    if (kStr.Contains("LOAD GENE"))
                    {       
                        bIsMovingLoad = true;
                        option = "LOAD GENERATION";
                        //load_generation.Read_Type_From_Text_File(fileName);
                        continue;
                    }
                    if (kStr.Contains("PRINT SUPPORT"))
                    {
                        option = "PRINT SUPPORT"; continue;
                    }
                    if (kStr.Contains("PRINT MAX"))
                    {
                        option = "PRINT MAX"; continue;
                    }
                    if (kStr.Contains("MEMBER PROPERTY") ||
                        kStr.Contains("MEMB PROP") ||
                        kStr.Contains("MEM PROP") ||
                        kStr.Contains("MEMBER PROPERTIES"))
                    {
                        option = "MEMBER PROPERTY"; continue;
                    }
                    if (kStr.Contains("DEAD"))
                    {
                        option = "DEAD"; continue;
                    }
                    if (kStr.Contains("PERFORM EIGEN"))
                    {
                        //option = "PERFORM EIGEN";
                        option = "DYNAMIC";
                        DynamicAnalysis.Add(kStr);
                        IsDynamicLoad = true;
                        continue;
                    }
                    if (kStr.Contains("PERFORM TIME HISTORY"))
                    {
                        option = "DYNAMIC";
                        //option = "TIME HISTORY";
                        DynamicAnalysis.Add(kStr);
                        IsDynamicLoad = true;
                        continue;
                    }
                    if (kStr.Contains("PERFORM RESPONSE"))
                    {
                        //option = "PERFORM RESPONSE";
                        option = "DYNAMIC";
                        DynamicAnalysis.Add(kStr);
                        IsDynamicLoad = true;
                        continue;
                    }
                    if (kStr.Contains("FREQUENCIES"))
                    {
                        FREQUENCIES = mList.GetString(1);
                        //option = "FREQUENCIES"; continue;
                    }
                    #region Dynamic Analysis
                    //if (kStr.Contains("TIME STEPS"))
                    //{
                    //    option = "TIME STEPS"; continue;
                    //}
                    //if (kStr.Contains("PRINT INTERVAL"))
                    //{
                    //    option = "PRINT INTERVAL"; continue;
                    //}
                    //if (kStr.Contains("STEP INTERVAL"))
                    //{
                    //    option = "STEP INTERVAL"; continue;
                    //}
                    //if (kStr.Contains("DAMPING FACTOR"))
                    //{
                    //    option = "DAMPING FACTOR"; continue;
                    //}
                    //if (kStr.Contains("GROUND MOTION"))
                    //{
                    //    option = "GROUND MOTION"; continue;
                    //}
                    //if (kStr.Contains("X DIVISION"))
                    //{
                    //    option = "X DIVISION"; continue;
                    //}
                    //if (kStr.Contains("SCALE FACTOR"))
                    //{
                    //    option = "SCALE FACTOR"; continue;
                    //}
                    //if (kStr.Contains("TIME VALUES"))
                    //{
                    //    option = "TIME VALUES"; continue;
                    //}
                    //if (kStr.Contains("TIME FUNCTION"))
                    //{
                    //    option = "TIME FUNCTION"; continue;
                    //}
                    //if (kStr.Contains("NODAL CONSTRAINT"))
                    //{
                    //    option = "NODAL CONSTRAINT"; continue;
                    //}
                    //if (kStr.Contains("MEMBER STRESS"))
                    //{
                    //    option = "MEMBER STRESS"; continue;
                    //}
                    //if (kStr.Contains("CUTOFF FREQUENCY"))
                    //{
                    //    option = "CUTOFF FREQUENCY"; continue;
                    //}
                    //if (kStr.Contains("PERFORM RESPONSE"))
                    //{
                    //    option = "PERFORM RESPONSE"; continue;
                    //}
                    //if (kStr.Contains("DIRECTION"))
                    //{
                    //    option = "DIRECTION"; continue;
                    //}
                    //if (kStr.Contains("SPECTRUM TYPE ACCELERATION"))
                    //{
                    //    option = "SPECTRUM TYPE ACCELERATION"; continue;
                    //}
                    //if (kStr.Contains("SPECTRUM TYPE DISPLACEMENT"))
                    //{
                    //    option = "SPECTRUM TYPE DISPLACEMENT"; continue;
                    //}
                    //if (kStr.Contains("SPECTRUM POINTS"))
                    //{
                    //    option = "SPECTRUM POINTS"; continue;
                    //}
                    //if (kStr.Contains("PERIOD ACCELERATION"))
                    //{
                    //    option = "PERIOD ACCELERATION"; continue;
                    //}
                    #endregion Dynamic Analysis

                    #endregion
                    switch (option)
                    {
                        case "JOINT COORDINATE":
                            try
                            {
                                Joints.AddTXT(kStr);
                            }
                            catch (Exception ex) 
                            {
                                kStr = "";
                            }
                            break;
                        case "MEMB INCI":
                            //Members.AddTXT(kStr);
                            try
                            {
                                Members.AddTXT(kStr);
                            }
                            catch (Exception ex)
                            {
                                kStr = "";
                            }
                            break;


                        case "GROUP":
                            //Members.AddTXT(kStr);
                            try
                            {
                                Members.Add_Group(kStr);
                                MemberGroups.Add(kStr);
                                
                            }
                            catch (Exception ex)
                            {
                                kStr = "";
                            }
                            break;


                        case "MEMBER TRUSS":
                            Members.AddMemberTruss(kStr);
                            MemberTrusses.Add(kStr);
                            break;

                        case "MEMBER CABLE":
                            //Members.AddMemberTruss(kStr);
                            MemberCables.Add(kStr);
                            break;
                        case "MEMBER RELEASE":
                            //Members.AddMemberTruss(kStr);
                            MemberReleases.Add(kStr);
                            break;
                        case "SUPPORT":
                            Supports.Add(kStr);
                            JointSupports.Add(kStr);
                            break;
                        case "JOINT WEIGHT":

                            if (DefineLoad.LoadCase != LoadCaseNo)
                            {
                                DefineLoad = new LoadDefine();
                                LoadDefines.Add(DefineLoad);
                            }
                            DefineLoad.LoadCase = LoadCaseNo;
                            DefineLoad.LoadTitle = LoadTitle;
                            DefineLoad.JointWeightList.Add(kStr);

                            break;
                        case "JOINT LOAD":
                            //JointLoads.AddTXT(kStr, LoadCaseNo);


                            JointLoads.AddTXT(kStr + comment, LoadCaseNo);


                            if (DefineLoad.LoadCase != LoadCaseNo)
                            {
                                DefineLoad = new LoadDefine();
                                LoadDefines.Add(DefineLoad);
                            }
                            DefineLoad.LoadCase = LoadCaseNo;
                            DefineLoad.LoadTitle = LoadTitle;

                            if (comment != "")
                            {
                                SLoad sl = new SLoad(kStr);
                                sl.Comment = comment;
                                DefineLoad.JointLoadList.Add(sl);
                                comment = "";
                            }
                            else
                            {
                                DefineLoad.JointLoadList.Add(kStr);
                            }

                            break;
                        case "MEMBER LOAD":
                            lstStr.Clear();
                            lstStr.AddRange(kStr.Trim().TrimEnd().TrimStart().Split(new char[] { ';' }));
                            foreach (string str in lstStr)
                            {
                                MemberLoads.AddTxt(str.Trim().TrimEnd().TrimStart(), LoadCaseNo);

                                if (DefineLoad.LoadCase != LoadCaseNo)
                                {
                                    DefineLoad = new LoadDefine();
                                    LoadDefines.Add(DefineLoad);
                                }
                                DefineLoad.LoadCase = LoadCaseNo;
                                DefineLoad.LoadTitle = LoadTitle;
                                DefineLoad.MemberLoadList.Add(str);
                            }
                            break;

                        case "AREA LOAD":
                            lstStr.Clear();
                            lstStr.AddRange(kStr.Trim().TrimEnd().TrimStart().Split(new char[] { ';' }));
                            foreach (string str in lstStr)
                            {
                                //AreaLoads.AddTxt(str.Trim().TrimEnd().TrimStart(), LoadCaseNo);
                                if (DefineLoad.LoadCase != LoadCaseNo)
                                {
                                    DefineLoad = new LoadDefine();
                                    LoadDefines.Add(DefineLoad);
                                }
                                DefineLoad.LoadCase = LoadCaseNo;
                                DefineLoad.LoadTitle = LoadTitle;
                                DefineLoad.AreaLoadList.Add(str);
                            }
                            break;

                        case "FLOOR LOAD":
                            lstStr.Clear();
                            lstStr.AddRange(kStr.Trim().TrimEnd().TrimStart().Split(new char[] { ';' }));
                            foreach (string str in lstStr)
                            {
                                //AreaLoads.AddTxt(str.Trim().TrimEnd().TrimStart(), LoadCaseNo);
                                if (DefineLoad.LoadCase != LoadCaseNo)
                                {
                                    DefineLoad = new LoadDefine();
                                    LoadDefines.Add(DefineLoad);
                                }
                                DefineLoad.LoadCase = LoadCaseNo;
                                DefineLoad.LoadTitle = LoadTitle;
                                if (!str.Contains("ONEWAY"))
                                    DefineLoad.FloorLoadList.Add(str);
                            }
                            break;


                        case "TEMP LOAD":
                            lstStr.Clear();
                            lstStr.AddRange(kStr.Trim().TrimEnd().TrimStart().Split(new char[] { ';' }));
                            foreach (string str in lstStr)
                            {
                                //AreaLoads.AddTxt(str.Trim().TrimEnd().TrimStart(), LoadCaseNo);
                                if (DefineLoad.LoadCase != LoadCaseNo)
                                {
                                    DefineLoad = new LoadDefine();
                                    LoadDefines.Add(DefineLoad);
                                }
                                DefineLoad.LoadCase = LoadCaseNo;
                                DefineLoad.LoadTitle = LoadTitle;
                                DefineLoad.TempLoadList.Add(str);
                            }
                            break;

                        case "REPEAT LOAD":
                            lstStr.Clear();
                            lstStr.AddRange(kStr.Trim().TrimEnd().TrimStart().Split(new char[] { ';' }));
                            foreach (string str in lstStr)
                            {
                                MemberLoads.SetLoadCombination(str, LoadCaseNo);
                                JointLoads.SetLoadCombination(str, LoadCaseNo);


                                if (DefineLoad.LoadCase != LoadCaseNo)
                                {
                                    DefineLoad = new LoadDefine();
                                    LoadDefines.Add(DefineLoad);
                                }
                                DefineLoad.LoadCase = LoadCaseNo;
                                DefineLoad.LoadTitle = LoadTitle;
                                DefineLoad.RepeatLoadList.Add(str);

                            }
                            break;


                        case "ELEMENT LOAD":
                            lstStr.Clear();
                            lstStr.AddRange(kStr.Trim().TrimEnd().TrimStart().Split(new char[] { ';' }));
                            foreach (string str in lstStr)
                            {
                                //MemberLoads.AddTxt(str.Trim().TrimEnd().TrimStart(), LoadCaseNo);

                                if (DefineLoad.LoadCase != LoadCaseNo)
                                {
                                    DefineLoad = new LoadDefine();
                                    LoadDefines.Add(DefineLoad);
                                }
                                DefineLoad.LoadCase = LoadCaseNo;
                                DefineLoad.LoadTitle = LoadTitle;
                                DefineLoad.ElementLoadList.Add(str);
                            }
                            break;
                        case "SUPPORT DISPACEMENT LOAD":
                            lstStr.Clear();
                            lstStr.AddRange(kStr.Trim().TrimEnd().TrimStart().Split(new char[] { ';' }));
                            foreach (string str in lstStr)
                            {
                                //MemberLoads.AddTxt(str.Trim().TrimEnd().TrimStart(), LoadCaseNo);

                                if (DefineLoad.LoadCase != LoadCaseNo)
                                {
                                    DefineLoad = new LoadDefine();
                                    LoadDefines.Add(DefineLoad);
                                }
                                DefineLoad.LoadCase = LoadCaseNo;
                                DefineLoad.LoadTitle = LoadTitle;
                                DefineLoad.SupportDisplacements.Add(str);
                            }
                            break;
                          
                        case "LOAD COMB":
                            lstStr.Clear();
                            lstStr.AddRange(kStr.Trim().TrimEnd().TrimStart().Split(new char[] { ';' }));
                            foreach (string str in lstStr)
                            {
                                MemberLoads.SetLoadCombination(str, LoadCaseNo);
                                JointLoads.SetLoadCombination(str, LoadCaseNo);


                                if (DefineLoad.LoadCase != LoadCaseNo)
                                {
                                    DefineLoad = new LoadDefine();
                                    LoadDefines.Add(DefineLoad);
                                }
                                DefineLoad.LoadCase = LoadCaseNo;
                                DefineLoad.LoadTitle = LoadTitle;
                                DefineLoad.CominationLoadList.Add(str);

                            }
                            break;

                        case "SEISMIC LOAD":
                            lstStr.Clear();
                            lstStr.AddRange(kStr.Trim().TrimEnd().TrimStart().Split(new char[] { ';' }));
                            foreach (string str in lstStr)
                            {
                                SeismicLoads.Add(str);
                            }
                            break;

                        case "ELEMENT INCIDENCE":
                            lstStr.Clear();
                            lstStr.AddRange(kStr.Trim().TrimEnd().TrimStart().Split(new char[] { ';' }));
                            foreach (string str in lstStr)
                            {
                                try
                                {
                                    Elements.Add(Element.Parse(str.Trim().TrimEnd().TrimStart()));
                                }
                                catch (Exception exx) { }
                            }
                            break;

                        case "MEMBER PROPERTY":
                            lstStr.Clear();
                            lstStr.AddRange(kStr.Trim().TrimEnd().TrimStart().Split(new char[] { ';' }));
                            MemberProperties.AnalysisData = AnalysisData;

                            //MemberProps.AddRange(lstStr.ToArray());

                            foreach (string str in lstStr)
                            {
                                try
                                {
                                    MemberProperties.AddTxt(str);
                                    MemberProps.Add(str.Trim().TrimStart());
                                }
                                catch (Exception exx) { }
                            }
                            break;

                        case "DEFINE MOVING":
                            try
                            {

                                int type = mList.GetInt(1);

                                if (mList.Count > 3)
                                {
                                    for (int j = 0; j < LL_Definition.Count; j++)
                                    {
                                        if (LL_Definition[j].Type == type)
                                            LL_Definition[j].Impact_Factor = mList.GetDouble(3);
                                    }
                                }
                            }
                            catch (Exception exx) { }

                            break;

                        case "LOAD GENERATION":
                            try
                            {
                                load_generation.Read_Type_From_Text_File(fileName);
                            }
                            catch (Exception exx) { }

                            break;

                        case "ELEMENT PROP":
                            try
                            {
                                Elements.SetElementProperty(kStr);
                                ElementProps.Add(kStr);
                            }
                            catch (Exception exx) { }

                            break;
                        
                        case "CONSTANT":
                            try
                            {
                                MemberProperties.AnalysisData = AnalysisData;
                                MemberProperties.SetConstants(kStr);

                                if (mList.StringList[0].StartsWith("D"))
                                {
                                    Mat_Prop.Density = mList.StringList[1];
                                }
                                else if (mList.StringList[0].StartsWith("E"))
                                {
                                    Mat_Prop.Elastic_Modulus = mList.StringList[1];
                                }
                                else if (mList.StringList[0].StartsWith("P"))
                                {
                                    Mat_Prop.Possion_Ratio = mList.StringList[1];
                                }
                                else if (mList.StringList[0].StartsWith("A"))
                                {
                                    Mat_Prop.Alpha = mList.StringList[1];
                                }
                                Mat_Prop.MemberNos = mList.GetString(2);
                                
                            }
                            catch (Exception exx) { }
                            break;
                        case "DYNAMIC":
                            DynamicAnalysis.Add(kStr);
                            break;
                        case "FINISH":
                            break;
                    }
                }
                while (lineNo != fileLines.Count);

            }
            catch (Exception exx) {}
            finally
            {
                //sr.Close();
                fileLines.Clear();
                fileLines = null;
            }
        }
        public void Draw_Main_MemberDetails(vdDocument doc, int LoadCase)
        {
            try
            {
                //Members.DrawMember(doc);
                //Supports.DrawSupport(doc);
                JointLoads.DrawPlanMovingJointLoads(doc, LoadCase);
                MemberLoads.DrawMemberLoad(doc, LoadCase);
                Elements.DrawElements(doc);

                //VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);

            }
            catch (Exception ex) { }
        }
        public void DrawMemberDetails(vdDocument doc, int LoadCase)
        {
            try
            {
                //Members.DrawMember(doc);
                Supports.DrawSupport(doc);
                JointLoads.DrawJointLoads(doc, LoadCase);
                MemberLoads.DrawMemberLoad(doc, LoadCase);
                Elements.DrawElements(doc);
                //VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);

            }
            catch (Exception ex) { }
        }
        public void SetLoadFactor()
        {
            double loadFac = 1.0d;
            string astFilePath = "";
            if ((Path.GetExtension(FileName).ToLower() == ".ast"))
            {
                astFilePath = FileName;
            }
            else if (Path.GetExtension(FileName).ToLower() == ".txt")
            {
                astFilePath = Path.Combine(Path.GetDirectoryName(FileName), Path.GetFileNameWithoutExtension(FileName) + ".ast");
            }

            List<string> fileContent = new List<string>(File.ReadAllLines(astFilePath));
            MyStrings mList;
            for (int i = 0; i < fileContent.Count; i++)
            {
                if (fileContent[i].StartsWith("N006"))
                {
                    while (fileContent[i].Contains("  "))
                    {
                        fileContent[i] = fileContent[i].Trim().TrimEnd().TrimStart().Replace("  ", " ");
                    }
                    mList = new MyStrings(fileContent[i], ' ');
                    loadFac = mList.GetDouble(3);
                    //LoadUnitFactor = loadFac;
                    //break;
                }
                if (fileContent[i].StartsWith("N099"))
                {
                    mList = new MyStrings(MyStrings.RemoveAllSpaces(fileContent[i]), ' ');
                    if (mList.Count == 5)
                    {
                        if (mList.GetInt(2) > 0)
                            this.IsDynamicLoad = true;
                        else
                            this.IsDynamicLoad = false;
                    }
                }

            }
            //return loadFac;
        }

        double loadUnitFac = 1.0d;
        public double LoadUnitFactor
        {
            get
            {
                return loadUnitFac;
            }
            //set
            //{
            //    loadUnitFac = value;
            //}
        }
    }
    [Serializable]
    public class ASTRAAnalysisData
    {
        JointNodeCollection joints;
        MemberCollection members;
        List<string> ASTRA_Data = null;
        public ASTRAAnalysisData(string analysis_file)
        {
            joints = new JointNodeCollection();
            members = new MemberCollection();
            AnalysisFileName = analysis_file;
            MemberGroups = new MemberGroupCollection();
            ReadDataFromFile();
            SetAstraStructure();

        }
        public string AnalysisFileName { get; set; }

        public MemberGroupCollection MemberGroups { get; set; }

        void ReadDataFromFile()
        {
            List<string> file_con = new List<string>(File.ReadAllLines(AnalysisFileName));
            string kStr = "";
            try
            {
                ASTRA_Data = new List<string>();
                bool flag = false;
                for (int i = 0; i < file_con.Count; i++)
                {

                    kStr = MyStrings.RemoveAllSpaces(file_con[i].ToUpper());

                    if (kStr.Contains("USER'S DATA") && kStr.Contains("END OF USER") == false)
                    {
                        flag = true; continue;

                    }
                    if (kStr.Contains("JOINT COO") && kStr.Contains("END OF USER") == false)
                    {
                        flag = true;

                    }
                    if (flag)
                        ASTRA_Data.Add(kStr);

                    //if (i == 440)
                    //    kStr = kStr.ToUpper();

                    if (kStr.Contains("FINIS") || kStr.Contains("END OF USER"))
                        break;
                }
            }
            catch (Exception ex) { }
            finally
            {
                file_con.Clear();
                file_con = null;
            }
        }


        void SetAstraStructure()
        {
            try
            {
                joints.ReadJointCoordinates(ASTRA_Data);
                members.ReadMembers(ASTRA_Data);
                members.SetCoordinates(joints);
                MemberGroups.ReadFromFile(ASTRA_Data, members);
            }
            catch (Exception ex) { }
        }
        public JointNodeCollection Joints
        {
            get
            {
                return joints;
            }
        }

        public MemberCollection Members
        {
            get
            {
                return members;
            }
        }

        public double Length
        {
            get
            {
                return joints.OuterLength;
            }
        }
        public double Width
        {
            get
            {
                return joints.Width;
            }
        }
        public double Height
        {
            get
            {
                return joints.Height;
            }
        }
        public double NoOfPanels
        {
            get
            {
                return joints.Get_NoOfPanels();
            }
        }

    }
    public class JointNode
    {
        public JointNode()
        {
            NodeNo = -1;
            XYZ = new gPoint();
        }
        public int NodeNo { get; set; }
        public double X { get { return XYZ.x; } set { XYZ.x = value; } }
        public double Y { get { return XYZ.y; } set { XYZ.y = value; } }
        public double Z { get { return XYZ.z; } set { XYZ.z = value; } }
        public gPoint XYZ { get; set; }
        public static JointNode Parse(string s)
        {
            string str = s.Replace(',', ' ').ToUpper();
            str = MyStrings.RemoveAllSpaces(str);
            MyStrings mList = new MyStrings(str, ' ');
            JointNode jNode = new JointNode();
            if (mList.Count == 4)
            {
                //1	0	0	8.43
                jNode.NodeNo = mList.GetInt(0);
                jNode.XYZ.x = mList.GetDouble(1);
                jNode.XYZ.y = mList.GetDouble(2);
                jNode.XYZ.z = mList.GetDouble(3);
            }
            else
                throw new Exception("Invalid Joint data : " + s);

            return jNode;

        }

        public override string ToString()
        {
            return string.Format("{0,-7} {1,10:f3} {2,10:f3} {3,10:f3} ", NodeNo, X, Y, Z);
        }
    }
    public class JointNodeCollection
    {
        List<JointNode> list = null;
        public JointNodeCollection()
        {
            list = new List<JointNode>();
        }
        public List<JointNode> JointNodes
        {
            get
            {
                return list;
            }
        }
        public JointNode this[int index]
        {
            get
            {
                return list[index];
            }
        }

        public void Add(JointNode jn)
        {
            list.Add(jn);
        }
        public void Clear()
        {
            list.Clear();
        }
        public int Count
        {
            get
            {
                return list.Count;
            }
        }
        public void ReadJointCoordinates(List<string> ASTRA_Data)
        {
            try
            {
                string kStr = "";
                bool flag = false;
                MyStrings ml = null;
                for (int i = 0; i < ASTRA_Data.Count; i++)
                {
                    kStr = ASTRA_Data[i].ToUpper();

                    kStr = MyStrings.RemoveAllSpaces(kStr);
                    if (kStr.Contains("JOINT C"))
                    {
                        flag = true; continue;
                    }
                    if (flag)
                    {
                        try
                        {
                            ml = new MyStrings(kStr, ';');
                            foreach (var item in ml.StringList)
                            {
                                list.Add(JointNode.Parse(item));
                            }
                            //list.Add(JointNode.Parse(kStr));
                        }
                        catch (Exception ex) { }
                    }
                    if (kStr.Contains("MEMBER")) break;
                }
                Set_Max_Min_Val();
            }
            catch (Exception ex) { }
        }
        public double MaxX { get; set; }
        public double MaxY { get; set; }
        public double MaxZ { get; set; }

        public double MinZ { get; set; }
        public double MinY { get; set; }
        public double MinX { get; set; }

        public void Set_Max_Min_Val()
        {
            MaxX = -99999999.9999;
            MaxY = -99999999.9999;
            MaxZ = -99999999.9999;
            MinX = 99999999.9999;
            MinY = 99999999.9999;
            MinZ = 99999999.9999;

            for (int i = 0; i < list.Count; i++)
            {
                if (MaxX < list[i].XYZ.x)
                    MaxX = list[i].XYZ.x;

                if (MaxY < list[i].XYZ.y)
                    MaxY = list[i].XYZ.y;

                if (MaxZ < list[i].XYZ.z)
                    MaxZ = list[i].XYZ.z;

                if (MinX > list[i].XYZ.x)
                    MinX = list[i].XYZ.x;

                if (MinY > list[i].XYZ.y)
                    MinY = list[i].XYZ.y;

                if (MinZ > list[i].XYZ.z)
                    MinZ = list[i].XYZ.z;
            }
        }

        public double Length
        {
            get
            {
                return (MaxX - MinX);
            }
        }

        public double OuterLength
        {
            get
            {
                double outer_length = 0.0;
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Z == MinZ)
                        if (outer_length < list[i].X)
                            outer_length = list[i].X;
                }

                return outer_length;
            }
        }

        public double Width
        {
            get
            {
                return (MaxZ - MinZ);
            }
        }
        public double Height
        {
            get
            {
                return (MaxY - MinY);
            }
        }

        public JointNode GetJoints(int JointNo)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].NodeNo == JointNo)
                {
                    return list[i];
                }
            }
            return null;
        }
        public int Get_NoOfPanels()
        {
            //List<double> list_x = new List<double>();
            double curr_val = MinX;
            int panel_count = 1;
            for (int i = 0; i < list.Count; i++)
            {

                if (curr_val < list[i].XYZ.x)
                {
                    curr_val = list[i].XYZ.x;
                    panel_count++;
                }
            }
            return panel_count;
        }

        public List<string> Get_Joints_Load_as_String(double inner_load, double outter_load)
        {
            int i = 0;
            string kStr = "";
            List<string> lst = new List<string>();

            List<int> inn_lst = new List<int>();
            List<int> out_lst = new List<int>();


            for (i = 0; i < list.Count; i++)
            {
                if (list[i].XYZ.x == MinX && list[i].XYZ.z == MinZ && list[i].XYZ.y == MinY)
                {
                    inn_lst.Add(list[i].NodeNo);
                }
                if (list[i].XYZ.x == MaxX && list[i].XYZ.z == MinZ && list[i].XYZ.y == MinY)
                {
                    inn_lst.Add(list[i].NodeNo);
                }
                if (list[i].XYZ.x == MinX && list[i].XYZ.z == MaxZ && list[i].XYZ.y == MinY)
                {
                    inn_lst.Add(list[i].NodeNo);
                }
                if (list[i].XYZ.x == MaxX && list[i].XYZ.z == MaxZ && list[i].XYZ.y == MinY)
                {
                    inn_lst.Add(list[i].NodeNo);
                }
            }
            for (i = 0; i < list.Count; i++)
            {
                if (list[i].XYZ.z == MinZ && list[i].XYZ.y == MinY)
                {
                    if (!inn_lst.Contains(list[i].NodeNo))
                        out_lst.Add(list[i].NodeNo);
                }

                if (list[i].XYZ.z == MaxZ && list[i].XYZ.y == MinY)
                {
                    if (!inn_lst.Contains(list[i].NodeNo))
                        out_lst.Add(list[i].NodeNo);
                }
            }
            kStr = "";
            int jnt1, jnt2, jnt3;
            int toIndx, fromIndx;
            bool flag = false;
            jnt1 = jnt2 = jnt3 = 0;
            for (i = 0; i < out_lst.Count; i++)
            {
                if (i == 0)
                {
                    jnt1 = out_lst[i];
                    jnt2 = jnt1;
                    continue;
                }
                jnt3 = out_lst[i];

                if (jnt3 == (jnt2 + 1))
                {
                    jnt2 = jnt3;
                    flag = true;
                    continue;
                }
                else
                {
                    if (flag)
                    {
                        kStr += jnt1 + "   TO  " + jnt2 + "  ";
                        jnt1 = jnt3;
                        jnt2 = jnt1;
                        flag = false;
                    }
                    else
                    {
                        kStr += jnt3 + " ";
                    }
                }
                //kStr += out_lst[i].ToString() + "  ";
            }
            if (flag)
            {
                kStr += jnt1 + "   TO  " + jnt2 + "  ";
                jnt1 = jnt3;
                //jnt2 = jnt1;
                //flag = false;
            }
            //else
            //{
            //    //kStr += jnt3 + " ";
            //}

            kStr += "    FY  -" + inner_load.ToString("0.000");
            lst.Add(kStr);
            kStr = "";
            for (i = 0; i < inn_lst.Count; i++)
            {
                kStr += inn_lst[i].ToString() + "  ";
            }
            kStr += "   FY  -" + outter_load.ToString("0.000");
            lst.Add(kStr);

            return lst;

        }

        public int Get_Total_Joints_At_Truss_Floor()
        {
            int i = 0;
            string kStr = "";
            List<string> lst = new List<string>();

            List<int> inn_lst = new List<int>();
            List<int> out_lst = new List<int>();


            for (i = 0; i < list.Count; i++)
            {
                if (list[i].XYZ.x == MinX && list[i].XYZ.z == MinZ && list[i].XYZ.y == MinY)
                {
                    inn_lst.Add(list[i].NodeNo);
                }
                if (list[i].XYZ.x == MaxX && list[i].XYZ.z == MinZ && list[i].XYZ.y == MinY)
                {
                    inn_lst.Add(list[i].NodeNo);
                }
                if (list[i].XYZ.x == MinX && list[i].XYZ.z == MaxZ && list[i].XYZ.y == MinY)
                {
                    inn_lst.Add(list[i].NodeNo);
                }
                if (list[i].XYZ.x == MaxX && list[i].XYZ.z == MaxZ && list[i].XYZ.y == MinY)
                {
                    inn_lst.Add(list[i].NodeNo);
                }
            }
            for (i = 0; i < list.Count; i++)
            {
                if (list[i].XYZ.z == MinZ && list[i].XYZ.y == MinY)
                {
                    if (!inn_lst.Contains(list[i].NodeNo))
                        out_lst.Add(list[i].NodeNo);
                }

                if (list[i].XYZ.z == MaxZ && list[i].XYZ.y == MinY)
                {
                    if (!inn_lst.Contains(list[i].NodeNo))
                        out_lst.Add(list[i].NodeNo);
                }
            }
            //kStr = "";
            //int jnt1, jnt2, jnt3;
            //int toIndx, fromIndx;
            //bool flag = false;
            //jnt1 = jnt2 = jnt3 = 0;
            //for (i = 0; i < out_lst.Count; i++)
            //{
            //    if (i == 0)
            //    {
            //        jnt1 = out_lst[i];
            //        jnt2 = jnt1;
            //        continue;
            //    }
            //    jnt3 = out_lst[i];

            //    if (jnt3 == (jnt2 + 1))
            //    {
            //        jnt2 = jnt3;
            //        flag = true;
            //        continue;
            //    }
            //    else
            //    {
            //        if (flag)
            //        {
            //            kStr += jnt1 + "   TO  " + jnt2 + "  ";
            //            jnt1 = jnt3;
            //            jnt2 = jnt1;
            //            flag = false;
            //        }
            //        else
            //        {
            //            kStr += jnt3 + " ";
            //        }
            //    }
            //    //kStr += out_lst[i].ToString() + "  ";
            //}
            //if (flag)
            //{
            //    kStr += jnt1 + "   TO  " + jnt2 + "  ";
            //    jnt1 = jnt3;
            //    //jnt2 = jnt1;
            //    //flag = false;
            //}
            //else
            //{
            //    //kStr += jnt3 + " ";
            //}

            //kStr += "    FY  -" + inner_load.ToString("0.000");
            //lst.Add(kStr);
            //kStr = "";
            //for (i = 0; i < inn_lst.Count; i++)
            //{
            //    kStr += inn_lst[i].ToString() + "  ";
            //}
            //kStr += "   FY  -" + outter_load.ToString("0.000");
            //lst.Add(kStr);

            return (inn_lst.Count + out_lst.Count);

        }

    }
    public class PrintAnalysis
    {
        public bool Perform_Analysis { get; set; }
        public bool Print_Analysis_All { get; set; }
        public bool Print_Load_Data { get; set; }
        public bool Print_Static_Check { get; set; }
        public bool Print_Support_Reaction { get; set; }
        public bool Print_Max_Force { get; set; }
        public List<string> List_Maxforce { get; set; }

        public PrintAnalysis()
        {
            Perform_Analysis = false;
            Print_Analysis_All = false;
            Print_Load_Data = false;
            Print_Static_Check = false;
            Print_Support_Reaction = false;
            Print_Max_Force = false;
            List_Maxforce = new List<string>();
        }
    }

    #region cc
    /*
     */
    public class Member
    {
        public Member()
        {
            MemberNo = -1;
            StartNode = new JointNode();
            EndNode = new JointNode();
        }
        public int MemberNo { get; set; }
        public JointNode StartNode { get; set; }
        public JointNode EndNode { get; set; }

        public double Length
        {
            get
            {
                return StartNode.XYZ.Distance3D(EndNode.XYZ);
            }
        }
        public static Member Parse(string s)
        {
            string str = s.Replace(",", " ").ToUpper();
            str = MyStrings.RemoveAllSpaces(s);

            MyStrings mList = new MyStrings(str, ' ');
            Member mem;
            if (mList.Count == 3)
            {
                mem = new Member();
                //1	1	2	
                mem.MemberNo = mList.GetInt(0);
                mem.StartNode.NodeNo = mList.GetInt(1);
                mem.EndNode.NodeNo = mList.GetInt(2);
            }
            else
            {
                throw new Exception("Invalid Member Data : " + s);
            }
            return mem;
        }

        public override string ToString()
        {
            return string.Format("{0,-7} {1,7} {2,7}", MemberNo, StartNode.NodeNo, EndNode.NodeNo);
        }
    }
    public class MemberCollection
    {
        List<Member> list = null;
        public MemberCollection()
        {
            list = new List<Member>();
        }
        //Chiranjit [2011 06 15]
        // Initialize Data from another object
        public MemberCollection(MemberCollection mem)
        {
            list = new List<Member>(mem.Members);
        }
        public List<Member> Members
        {
            get
            {
                return list;
            }
        }
        public bool Contains(Member item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].MemberNo == item.MemberNo)
                {
                    return true;
                }
            }
            return false;
        }
        public Member this[int index]
        {
            get
            {
                return list[index];
            }
        }
        public void Add(Member m)
        {
            list.Add(m);
        }
        public void Clear()
        {
            list.Clear();
        }
        public int Count
        {
            get
            {
                return list.Count;
            }
        }
        public void ReadMembers(List<string> ASTRA_Data)
        {
            try
            {
                list.Clear();

                string kStr = "";
                bool flag = false;
                for (int i = 0; i < ASTRA_Data.Count; i++)
                {
                    kStr = ASTRA_Data[i].ToUpper();
                    if (kStr.Contains("MEMBER"))
                    {
                        flag = true; continue;
                    }
                    if (flag)
                    {
                        try
                        {

                            MyStrings ml = new MyStrings(kStr, ';');
                            foreach (var item in ml.StringList)
                            {
                                list.Add(Member.Parse(item));
                            }
                            //list.Add(Member.Parse(kStr));
                        }
                        catch (Exception ex) { }
                    }
                    if (kStr.Contains("MEMBER TRUSS")) break;
                    if (kStr.Contains("SECTION")) break;
                    if (kStr.Contains("MATERIAL")) break;
                }
            }
            catch (Exception ex) { }
        }

        public void SetCoordinates(JointNodeCollection joints)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].EndNode = joints.GetJoints(list[i].EndNode.NodeNo);
                list[i].StartNode = joints.GetJoints(list[i].StartNode.NodeNo);
            }
        }
        public Member GetMember(int memNo)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].MemberNo == memNo)
                    return list[i];
            }
            return null;
        }

        public double Get_Member_Length(string mem_def)
        {
            string kStr = MyStrings.RemoveAllSpaces(mem_def);
            MyStrings mList = new MyStrings(kStr, ' ');

            Member mem = GetMember(mList.GetInt(0));

            if (mem != null) return mem.Length;

            return 0.0;
        }
    }

    public class MemberGroup
    {
        string memText = "";
        public MemberGroup()
        {
            GroupName = "";
            MemberNosText = "";
            MemberNos = new List<int>();
        }
        public string GroupName { get; set; }
        //public MemberCollection MemberColl { get; set; }
        public string MemberNosText
        {
            get
            {
                return memText;
            }
            set
            {
                memText = value;

            }
        }
        public static MemberGroup Parse(string str)
        {

            //START	GROUP	DEFINITION		
            //_L0L1	1	10	11	20
            //_L1L2	2	9	12	19
            //_L2L3	3	8	13	18
            //_L3L4	4	7	14	17
            //_L4L5	5	6	15	16
            //_U1U2	59	66	67	74
            //_U2U3	60	65	68	73
            //_U3U4	61	64	69	72
            //_U4U5	62	63	70	71
            //_L1U1	21	29	30	38
            //_L2U2	22	28	31	37
            //_L3U3	23	27	32	36
            //_L4U4	24	26	33	35
            //_L5U5	25	34		
            //_ER	39	43	49	53
            //_L2U1	40	44	50	54
            //_L3U2	41	45	51	55
            //_L4U3	42	46	52	56
            //_L5U4	47	48	57	58
            //_TCS_ST	170	TO	178	
            //_TCS_DIA	179	TO	194	
            //_BCB	195	TO	214	
            //_STRINGER	75	TO	114	
            //_XGIRDER	115	TO	169						
            //END	
            string kStr = "";
            kStr = str.Replace(',', ' ');
            kStr = MyStrings.RemoveAllSpaces(kStr);

            MyStrings mList = new MyStrings(kStr, ' ');

            MemberGroup mGrp = new MemberGroup();

            mGrp.GroupName = mList.StringList[0];
            for (int i = 1; i < mList.Count; i++)
            {
                mGrp.MemberNosText += mList.StringList[i] + " ";

            }
            //mGrp.SetMemNos();
            return mGrp;

        }

        public void SetMemNos()
        {
            if (memText == "") return;
            MemberNos = new List<int>();

            string str = MyStrings.RemoveAllSpaces(memText);
            MyStrings mList = new MyStrings(str, ' ');

            int indx = -1;
            int count = 0;
            int i = 0;
            do
            {
                indx = mList.StringList.IndexOf("TO");
                if (indx == -1)
                {
                    indx = mList.StringList.IndexOf("-");
                }
                if (indx != -1)
                {
                    for (i = mList.GetInt(indx - 1); i <= mList.GetInt(indx + 1); i++)
                    {
                        MemberNos.Add(i);
                    }
                    mList.StringList.RemoveRange(0, indx + 2);
                }
                else if (mList.Count > 0)
                {
                    for (i = 0; i < mList.Count; i++)
                    {
                        MemberNos.Add(mList.GetInt(i));
                    }
                    mList.StringList.Clear();
                }
            }
            while (mList.Count != 0 && mList.Count != 1);
        }
        public List<int> MemberNos { get; set; }
    }

    public class MemberGroupCollection : List<MemberGroup>
    {
        //Hashtable hash_ta;
        public List<MemberGroup> GroupCollection
        {
            get
            {
                return this;
            }
        }
        public MemberGroupCollection()
            : base()
        {
            //hash_ta = new Hashtable();
            //GroupCollection = new List<MemberGroup>();
        }
        public void ReadFromFile(List<string> astra_data, MemberCollection All_Members)
        {
            //START	GROUP	DEFINITION		
            //_L0L1	1	10	11	20
            //_L1L2	2	9	12	19
            //_L2L3	3	8	13	18
            //_L3L4	4	7	14	17
            //_L4L5	5	6	15	16
            //_U1U2	59	66	67	74
            //_U2U3	60	65	68	73
            //_U3U4	61	64	69	72
            //_U4U5	62	63	70	71
            //_L1U1	21	29	30	38
            //_L2U2	22	28	31	37
            //_L3U3	23	27	32	36
            //_L4U4	24	26	33	35
            //_L5U5	25	34		
            //_ER	39	43	49	53
            //_L2U1	40	44	50	54
            //_L3U2	41	45	51	55
            //_L4U3	42	46	52	56
            //_L5U4	47	48	57	58
            //_TCS_ST	170	TO	178	
            //_TCS_DIA	179	TO	194	
            //_BCB	195	TO	214	
            //_STRINGER	75	TO	114	
            //_XGIRDER	115	TO	169						
            //END	
            string kStr = "";
            bool flag = false;
            for (int i = 0; i < astra_data.Count; i++)
            {
                kStr = astra_data[i].Replace(',', ' ');
                kStr = MyStrings.RemoveAllSpaces(kStr);
                if (kStr.StartsWith("*")) continue;
                if (kStr.StartsWith("//")) continue;
                MyStrings mList = new MyStrings(kStr, ' ');

                if (kStr.Contains("START GROUP"))
                {
                    flag = true; continue;
                }
                else if (kStr.StartsWith("END ") && kStr.Contains("XGIRDER_END") == false)
                {
                    flag = false; break;
                }
                if (flag)
                {
                    //int toIndex = mList.StringList.IndexOf("T0"
                    GroupCollection.Add(MemberGroup.Parse(kStr));
                    //hash_ta.Add(mList.StringList[0], GroupCollection[GroupCollection.Count - 1]);
                }

            }
        }
        public MemberGroup GetMemberGroup(string memGroup)
        {
            //return (MemberGroup)hash_ta[memGroup];

            foreach (var item in this)
            {
                if (item.GroupName == memGroup)
                    return item;

            }
            return null;

        }

        public void Add(MemberGroup item)
        {
            base.Add(item);
            //hash_ta.Add(item.GroupName, item);
        }
        public void Remove(MemberGroup item)
        {
            base.Remove(item);
            //hash_ta.Remove(item.GroupName);
        }
        public void RemoveAt(int index)
        {
            //hash_ta.Remove(this[index].GroupName);
            base.RemoveAt(index);
        }
    }
    /**/
    #endregion cc


    public class LiveLoad
    {
        public LiveLoad()
        {
            Type = 0;
            X_Distance = 0.0;
            Y_Distance = 0.0;
            Z_Distance = 0.0;
            X_Increment = 0.0;
            Y_Increment = 0.0;
            Z_Increment = 0.0;
            Impact_Factor = 1.179;
        }
        public int Type { get; set; }
        public double X_Distance { get; set; }
        public double Y_Distance { get; set; }
        public double Z_Distance { get; set; }
        public double X_Increment { get; set; }
        public double Y_Increment { get; set; }
        public double Z_Increment { get; set; }
        public double Impact_Factor { get; set; }


        public override string ToString()
        {
            string kStr = string.Format("TYPE {0} {1:f3} {2:f3} {3:f3}", Type, X_Distance, Y_Distance, Z_Distance);

            if (X_Increment != 0.0)
                kStr += " XINCR " + X_Increment.ToString("f3");
            if (Y_Increment != 0.0)
                kStr += " YINCR " + Y_Increment.ToString("f3");
            if (Z_Increment != 0.0)
                kStr += " ZINCR " + Z_Increment.ToString("f3");
            return kStr;
        }

    }
    [Serializable]
    public class MovingLoadData
    {
        public MovingLoadData()
        {
            Type = 0;
            Name = "";
            Loads = "";
            Distances = "";
            LoadWidth = 0.0;
        }
        public int Type { get; set; }
        public string Name { get; set; }
        public string Loads { get; set; }
        public string Distances { get; set; }
        public double LoadWidth { get; set; }

        public static List<MovingLoadData> GetMovingLoads(string file_path)
        {

            if (!File.Exists(file_path)) return null;

            //TYPE1 IRCCLASSA
            //68 68 68 68 114 114 114 27
            //3.00 3.00 3.00 4.30 1.20 3.20 1.10
            //1.80
            List<MovingLoadData> LL_list = new List<MovingLoadData>();
            List<string> file_content = new List<string>(File.ReadAllLines(file_path));
            MyStrings mlist = null;
            string kStr = "";

            MovingLoadData mld = null;

            int icount = 0;
            for (int i = 0; i < file_content.Count; i++)
            {
                kStr = MyStrings.RemoveAllSpaces(file_content[i]);
                kStr = kStr.Replace(',', ' ');
                kStr = MyStrings.RemoveAllSpaces(kStr);
                mlist = new MyStrings(kStr, ' ');

                if (mlist.StringList[0].Contains("TYPE"))
                {
                    MovingLoadData ld = new MovingLoadData();
                    if (mlist.Count == 2)
                    {
                        kStr = mlist.StringList[0].Replace("TYPE", "");
                        ld.Type = MyStrings.StringToInt(kStr, 0);
                        ld.Name = mlist.StringList[1];
                        LL_list.Add(ld);
                    }
                    else if (mlist.Count == 3)
                    {
                        ld.Type = MyStrings.StringToInt(mlist.StringList[1], 0);
                        ld.Name = mlist.GetString(2);
                        LL_list.Add(ld);
                    }
                    if ((i + 3) < file_content.Count)
                    {
                        ld.Loads = file_content[i + 1].Replace(",", " ");
                        ld.Distances = file_content[i + 2].Replace(",", " ");
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
            return string.Format("TYPE {0} {1}", Type, Name);
        }
    }

}
