using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using VectorDraw.Geometry;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdFigures;


namespace MovingLoadAnalysis
{
    public class RKASTRA
    {
        Hashtable Joints = new Hashtable();
        //Hashtable Members = new Hashtable();
        Hashtable Translation = new Hashtable();
        Hashtable Forces = new Hashtable();

        //List<kJointNode> Joints = new List<kJointNode>();
        List<kMember> Members = new List<kMember>();
        //List<kTranslationRotation> Translation = new List<kTranslationRotation>();
        //List<kForceMoment> Forces = new List<kForceMoment>();

        vdDocument vdoc = null;
        public string AnalysisReportFile { get; set; }
        public RKASTRA(vdDocument doc, string file_name)
        {
            vdoc = doc;
            //ReadFromFile(Repofile);
            if (file_name.Contains("ANALYSIS_REP.TXT"))
                AnalysisReportFile = file_name;
            else
                AnalysisReportFile = Path.Combine(Path.GetDirectoryName(file_name), "ANALYSIS_REP.TXT");
            RunThread();
            //Thread thd = new Thread(new ThreadStart(RunThread));
            //thd.Priority = ThreadPriority.Highest;
            //thd.Start();
            //thd.Join();
        }
        public void ReadFromFile1(string file_name)
        {
            if (file_name.Contains("ANALYSIS_REP.TXT"))
                AnalysisReportFile = file_name;
            else
                AnalysisReportFile = Path.Combine(Path.GetDirectoryName(file_name), "ANALYSIS_REP.TXT");
            if (!File.Exists(AnalysisReportFile)) return;
            List<string> list = new List<string>(File.ReadAllLines(AnalysisReportFile));
            string kStr = "";
            bool joint_coordinate = false;
            bool member_incident = false;
            bool translation = false;
            bool forces = false;
            int last_node = 0;
            try
            {
                frm_ProgressBar.ON("Reading Data.....");
                for (int i = 0; i < list.Count; i++)
                {
                    kStr = MyList.RemoveAllSpaces(list[i].Trim().ToUpper());
                    //frm_ProgressBar.ON("Reading Data.....");
                    Console.WriteLine(kStr + " " + i + " " + list.Count);
                    //frm_ProgressBar.ON("Reading Data.....");
                    if (kStr.Contains("JOINT COORDINATES"))
                    {
                        joint_coordinate = true;
                        //joint_coordinate = false;
                        member_incident = false;
                        translation = false;
                        forces = false;
                        continue;
                    }
                    if (kStr.Contains("MEMBER INCIDENCE"))
                    {
                        member_incident = true;

                        joint_coordinate = false;
                        //member_incident = false;
                        translation = false;
                        forces = false;
                        continue;
                    }
                    if (kStr.Contains("NUMBER CASE FORCE FORCE FORCE MOMENT MOMENT MOMENT"))
                    {
                        forces = true;

                        joint_coordinate = false;
                        member_incident = false;
                        translation = false;
                        //forces = false;
                        continue;
                    }
                    if (kStr.Contains("NUMBER CASE TRANSLATION TRANSLATION TRANSLATION ROTATION ROTATION ROTATION"))
                    {
                        translation = true;

                        joint_coordinate = false;
                        member_incident = false;
                        //translation = false;
                        forces = false;
                        continue;
                    }


                    if (joint_coordinate)
                    {
                        try
                        {
                            kJointNode kn = kJointNode.Parse(kStr);
                            Joints.Add(kn.JointNo, kn);
                            //Joints.Add(kn);
                        }
                        catch (Exception ex) { }
                    }
                    if (member_incident)
                    {
                        try
                        {
                            kMember kn = kMember.Parse(kStr);
                            //Members.Add(kn.MemberNo, kn);
                            Members.Add(kn);
                        }
                        catch (Exception ex) { }
                    }
                    if (forces)
                    {
                        try
                        {
                            kForceMoment kn = kForceMoment.Parse(kStr);
                            if (kn.NodeNumber == 0 || kn.NodeNumber == -1)
                                kn.NodeNumber = last_node;
                            else
                                last_node = kn.NodeNumber;

                            Forces.Add(kn.ID, kn);
                            //Forces.Add(kn);
                        }
                        catch (Exception ex) { }
                    }

                    if (translation)
                    {
                        try
                        {
                            kTranslationRotation kn = kTranslationRotation.Parse(kStr);
                            if (kn.NodeNumber == 0 || kn.NodeNumber == -1)
                                kn.NodeNumber = last_node;
                            else
                                last_node = kn.NodeNumber;

                            Translation.Add(kn.ID, kn);
                            //Translation.Add(kn);
                        }
                        catch (Exception ex) { }
                    }
                    frm_ProgressBar.SetValue(i, list.Count);
                }
            }
            catch (Exception ex) { }
            finally
            {
                list.Clear();
                list = null;
                frm_ProgressBar.OFF();
            }
        }

        public void ReadFromFile(string file_name)
        {
            if (!File.Exists(AnalysisReportFile)) return;
            //List<string> list = new List<string>(File.ReadAllLines(AnalysisReportFile));
            List<string> list = new List<string>();
            string kStr = "";
            bool joint_coordinate = false;
            bool member_incident = false;
            bool translation = false;
            bool forces = false;
            int last_node = 0;

            StreamReader sw = new StreamReader(new FileStream(AnalysisReportFile, FileMode.Open));
            try
            {
                //frm_ProgressBar.ON("Reading Data.....");
                Console.WriteLine("START" + DateTime.Now.ToString());
                while (!sw.EndOfStream)
                {
                    kStr = MyList.RemoveAllSpaces(sw.ReadLine().Trim().ToUpper());
                    ////frm_ProgressBar.ON("Reading Data.....");
                    Console.WriteLine(kStr + " " + sw.BaseStream.Position + " " + sw.BaseStream.Length);
                    ////frm_ProgressBar.ON("Reading Data.....");
                    //if (kStr.Contains("JOINT COORDINATE") ||
                    //    kStr.Contains("JOINT COOR"))
                    //{
                    //    joint_coordinate = true;
                    //    //joint_coordinate = false;
                    //    member_incident = false;
                    //    translation = false;
                    //    forces = false;
                    //    continue;
                    //}
                    //if (kStr.Contains("MEMBER INCIDENCE") ||
                    //    kStr.Contains("MEM INCI") ||
                    //    kStr.Contains("MEMBER INCI") ||
                    //    kStr.Contains("MEMBER CONNECTIVITY") ||
                    //    kStr.Contains("MEMBER CON"))
                    //{
                    //    member_incident = true;

                    //    joint_coordinate = false;
                    //    //member_incident = false;
                    //    translation = false;
                    //    forces = false;
                    //    continue;
                    //}
                    //if (kStr.StartsWith("BEAM"))
                    //{
                    //    if (Forces.Count != 0 && Translation.Count != 0)
                    //        break;
                    //}

                    //if (kStr.Contains("SECTION") ||
                    //    kStr.Contains("SECTION PROPERTY") ||
                    //    kStr.Contains("MATERIAL") ||
                    //    kStr.Contains("SUPPORT") ||
                    //    kStr.Contains("MEMBER LOAD"))
                    //{

                    //    joint_coordinate = false;
                    //    member_incident = false;
                    //    translation = false;
                    //    forces = false;
                    //    continue;
                    //}
                    //if (kStr.Contains("NUMBER CASE FORCE FORCE FORCE MOMENT MOMENT MOMENT"))
                    //{
                    //    forces = true;

                    //    joint_coordinate = false;
                    //    member_incident = false;
                    //    translation = false;
                    //    //forces = false;
                    //    continue;
                    //}
                    //if (kStr.Contains("NUMBER CASE TRANSLATION TRANSLATION TRANSLATION ROTATION ROTATION ROTATION"))
                    //{
                    //    translation = true;

                    //    joint_coordinate = false;
                    //    member_incident = false;
                    //    //translation = false;
                    //    forces = false;
                    //    continue;
                    //}


                    //if (joint_coordinate)
                    //{
                    //    try
                    //    {
                    //        kJointNode kn = kJointNode.Parse(kStr);
                    //        Joints.Add(kn.JointNo, kn);
                    //        //Joints.Add(kn);
                    //    }
                    //    catch (Exception ex) { }
                    //}
                    //if (member_incident)
                    //{
                    //    try
                    //    {
                    //        kMember kn = kMember.Parse(kStr);
                    //        //Members.Add(kn.MemberNo, kn);
                    //        Members.Add(kn);
                    //    }
                    //    catch (Exception ex) { }
                    //}
                    //if (forces)
                    //{
                    //    try
                    //    {
                    //        kForceMoment kn = kForceMoment.Parse(kStr);
                    //        if (kn.NodeNumber == 0 || kn.NodeNumber == -1)
                    //            kn.NodeNumber = last_node;
                    //        else
                    //            last_node = kn.NodeNumber;

                    //        Forces.Add(kn.ID, kn);
                    //        //Forces.Add(kn);
                    //    }
                    //    catch (Exception ex) { }
                    //}

                    //if (translation)
                    //{
                    //    try
                    //    {
                    //        kTranslationRotation kn = kTranslationRotation.Parse(kStr);
                    //        if (kn.NodeNumber == 0 || kn.NodeNumber == -1)
                    //            kn.NodeNumber = last_node;
                    //        else
                    //            last_node = kn.NodeNumber;

                    //        Translation.Add(kn.ID, kn);
                    //        //Translation.Add(kn);
                    //    }
                    //    catch (Exception ex) { }
                    //}
                    //frm_ProgressBar.SetValue(sw.BaseStream.Position, sw.BaseStream.Length);
                }
                Console.WriteLine("END " + DateTime.Now.ToString());

            }
            catch (Exception ex) { }
            finally
            {
                list.Clear();
                list = null;
                frm_ProgressBar.OFF();
            }
        }
        public void RunThread()
        {
            ReadFromFile(AnalysisReportFile);
            DrawMember();

            UpdateLoadCase(3);
        }

        public void DrawMember()
        {
            vdLine ln = new vdLine();
            for (int i = 0; i < Members.Count; i++)
            {
                Members[i].StartNode = (kJointNode)Joints[Members[i].StartNode.JointNo];
                Members[i].EndNode = (kJointNode)Joints[Members[i].EndNode.JointNo];
                Members[i].DrawMember(vdoc);

            }
        }

        public void UpdateLoadCase(int Loadcase)
        {
            kTranslationRotation kTR = null;
            for (int i = 0; i < Members.Count; i++)
            {
                try
                {
                    Members[i].StartNode = (kJointNode)Joints[Members[i].StartNode.JointNo];
                    Members[i].EndNode = (kJointNode)Joints[Members[i].EndNode.JointNo];

                    kTR = (kTranslationRotation)Translation[Members[i].StartNode.JointNo + "," + Loadcase];

                    Members[i].Line.StartPoint.x = Members[i].StartNode.x + kTR.X_Translation + kTR.X_Rotation;
                    Members[i].Line.StartPoint.y = Members[i].StartNode.y + kTR.Y_Translation + kTR.Y_Rotation;
                    Members[i].Line.StartPoint.z = Members[i].StartNode.z + kTR.Z_Translation + kTR.Z_Rotation;



                    kTR = (kTranslationRotation)Translation[Members[i].EndNode.JointNo + "," + Loadcase];
                    Members[i].Line.EndPoint.x = Members[i].EndNode.x + kTR.X_Translation + kTR.X_Rotation;
                    Members[i].Line.EndPoint.y = Members[i].EndNode.y + kTR.Y_Translation + kTR.Y_Rotation;
                    Members[i].Line.EndPoint.z = Members[i].EndNode.z + kTR.Z_Translation + kTR.Z_Rotation;

                    if (Members[i].Line.EndPoint.y < -4)
                    {
                        Members[i].Line.EndPoint.y = Members[i].Line.EndPoint.y + 0.0;

                    }

                    if (Members[i].Line.StartPoint.y < -4)
                    {
                        Members[i].Line.StartPoint.y = Members[i].Line.StartPoint.y + 0.0;

                    }

                    Members[i].Line.Update();
                }
                catch (Exception ex) { }
            }
            vdoc.Redraw(true);
        }
    }
    public class kJointNode
    {
        public int JointNo { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }

        public kJointNode(double x)
        {
            JointNo = 0;
            x = y = z = 0;
            this.x = x;
        }
        public kJointNode(double x, double y)
        {
            JointNo = 0;
            x = y = z = 0;
            this.x = x;
            this.y = y;
        }
        public kJointNode(double x, double y, double z)
        {
            JointNo = 0;
            x = y = z = 0;
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public kJointNode()
        {
            JointNo = 0;
            x = y = z = 0;
        }

        public static kJointNode Parse(string txt)
        {
            string kStr = MyList.RemoveAllSpaces(txt.ToUpper());

            MyList mlist = new MyList(kStr, ' ');

            kJointNode kob = new kJointNode();
            kob.JointNo = mlist.GetInt(0);
            kob.x = mlist.GetDouble(1);
            kob.y = mlist.GetDouble(2);
            kob.z = mlist.GetDouble(3);

            return kob;
        }
    }
    public class kMember
    {
        vdLine ln;
        public int MemberNo { get; set; }
        public kJointNode StartNode { get; set; }
        public kJointNode EndNode { get; set; }
        public kMember()
        {
            MemberNo = 0;
            StartNode = new kJointNode();
            EndNode = new kJointNode();
        }
        public static kMember Parse(string txt)
        {
            string kStr = MyList.RemoveAllSpaces(txt.ToUpper());

            MyList mlist = new MyList(kStr, ' ');

            kMember kob = new kMember();
            kob.MemberNo = mlist.GetInt(0);
            kob.StartNode.JointNo = mlist.GetInt(1);
            kob.EndNode.JointNo = mlist.GetInt(2);
            return kob;
        }
        public vdLine Line
        {
            get
            {
                if (ln == null)
                {
                    ln = new vdLine();
                    ln.StartPoint.x = StartNode.x;
                    ln.StartPoint.y = StartNode.y;
                    ln.StartPoint.z = StartNode.z;
                    ln.EndPoint.x = EndNode.x;
                    ln.EndPoint.y = EndNode.y;
                    ln.EndPoint.z = EndNode.z;
                }
                return ln;
            }
            set
            {
                ln = value;
            }
        }
        public bool DrawMember(vdDocument doc)
        {
            Line.SetUnRegisterDocument(doc);
            Line.setDocumentDefaults();
            doc.ActiveLayOut.Entities.AddItem(Line);
            return true;
        }

    }
    public class kTranslationRotation
    {
        public int NodeNumber { get; set; }
        public int LoadCase { get; set; }
        public double X_Translation { get; set; }
        public double X_Rotation { get; set; }
        public double Y_Translation { get; set; }
        public double Y_Rotation { get; set; }
        public double Z_Translation { get; set; }
        public double Z_Rotation { get; set; }
        public string ID
        {
            get
            {
                return string.Format("{0},{1}", NodeNumber, LoadCase);
            }
        }
        public kTranslationRotation()
        {
            NodeNumber = -1;
            LoadCase = -1;
            X_Translation = -1;
            X_Rotation = -1;
            Y_Translation = -1;
            Y_Rotation = -1;
            Z_Translation = -1;
            Z_Rotation = -1;
        }
        //   NODE   LOAD            X-            Y-            Z-            X-            Y-            Z-
        //NUMBER   CASE    TRANSLATION   TRANSLATION   TRANSLATION      ROTATION      ROTATION      ROTATION
        //109       1   0.00000E+00  -0.59846E+00   0.00000E+00   0.28766E-02   0.00000E+00   0.50815E+00
        public static kTranslationRotation Parse(string txt)
        {
            string kStr = MyList.RemoveAllSpaces(txt.ToUpper());

            MyList mlist = new MyList(kStr, ' ');

            int index = 0;
            kTranslationRotation kob = new kTranslationRotation();
            if (mlist.Count == 8)
            {
                kob.NodeNumber = mlist.GetInt(index); index++;
                kob.LoadCase = mlist.GetInt(index); index++;
                kob.X_Translation = mlist.GetDouble(index); index++;
                kob.Y_Translation = mlist.GetDouble(index); index++;
                kob.Z_Translation = mlist.GetDouble(index); index++;
                kob.X_Rotation = mlist.GetDouble(index); index++;
                kob.Y_Rotation = mlist.GetDouble(index); index++;
                kob.Z_Rotation = mlist.GetDouble(index); index++;
            }
            else
            {
                //kob.NodeNumber = mlist.GetInt(index); index++;
                kob.LoadCase = mlist.GetInt(index); index++;
                kob.X_Translation = mlist.GetDouble(index); index++;
                kob.Y_Translation = mlist.GetDouble(index); index++;
                kob.Z_Translation = mlist.GetDouble(index); index++;
                kob.X_Rotation = mlist.GetDouble(index); index++;
                kob.Y_Rotation = mlist.GetDouble(index); index++;
                kob.Z_Rotation = mlist.GetDouble(index); index++;
            }
            return kob;
        }
    }
    public class kForceMoment
    {

        public int NodeNumber { get; set; }
        public int LoadCase { get; set; }
        public double X_AxisForce { get; set; }
        public double X_AxisMoment { get; set; }
        public double Y_AxisForce { get; set; }
        public double Y_AxisMoment { get; set; }
        public double Z_AxisForce { get; set; }
        public double Z_AxisMoment { get; set; }
        public string ID
        {
            get
            {
                return string.Format("{0},{1}", NodeNumber, LoadCase);
            }
        }
        //   NODE      LOAD         X-AXIS         Y-AXIS         Z-AXIS         X-AXIS         Y-AXIS         Z-AXIS
        //NUMBER      CASE          FORCE          FORCE          FORCE         MOMENT         MOMENT         MOMENT
        public kForceMoment()
        {
            NodeNumber = -1;
            LoadCase = -1;
            X_AxisForce = -1;
            X_AxisMoment = -1;
            Y_AxisForce = -1;
            Y_AxisMoment = -1;
            Z_AxisForce = -1;
            Z_AxisMoment = -1;
        }
        public static kForceMoment Parse(string txt)
        {
            string kStr = MyList.RemoveAllSpaces(txt.ToUpper());

            MyList mlist = new MyList(kStr, ' ');

            int index = 0;
            kForceMoment kob = new kForceMoment();
            if (mlist.Count == 8)
            {
                kob.NodeNumber = mlist.GetInt(index); index++;
                kob.LoadCase = mlist.GetInt(index); index++;
                kob.X_AxisForce = mlist.GetDouble(index); index++;
                kob.Y_AxisForce = mlist.GetDouble(index); index++;
                kob.Z_AxisForce = mlist.GetDouble(index); index++;
                kob.X_AxisMoment = mlist.GetDouble(index); index++;
                kob.Y_AxisMoment = mlist.GetDouble(index); index++;
                kob.Z_AxisMoment = mlist.GetDouble(index); index++;
            }
            else
            {
                //kob.NodeNumber = mlist.GetInt(index); index++;
                kob.LoadCase = mlist.GetInt(index); index++;
                kob.X_AxisForce = mlist.GetDouble(index); index++;
                kob.Y_AxisForce = mlist.GetDouble(index); index++;
                kob.Z_AxisForce = mlist.GetDouble(index); index++;
                kob.X_AxisMoment = mlist.GetDouble(index); index++;
                kob.Y_AxisMoment = mlist.GetDouble(index); index++;
                kob.Z_AxisMoment = mlist.GetDouble(index); index++;
            }
            return kob;
        }
    }


    [Serializable]
    public class MyList
    {
        List<string> strList = null;
        char sp_char;
        public MyList(string s, char splitChar)
        {
            strList = new List<string>(s.Split(new char[] { splitChar }));
            sp_char = splitChar;

            for (int i = 0; i < strList.Count; i++)
            {
                strList[i] = strList[i].Trim().TrimEnd().TrimStart();
            }
        }
        public MyList(List<string> lstStr)
        {
            strList = new List<string>();

            strList = lstStr;
        }

        public List<string> StringList
        {
            get
            {
                return strList;
            }
        }
        public int GetInt(int index)
        {
            //try
            //{
            return int.Parse(strList[index]);
            //}
            //catch(Exception exx)
            //{
            //}
            //return -1;
        }
        public decimal GetDecimal(int index)
        {
            //try
            //{
            return decimal.Parse(strList[index]);
            //}
            //catch(Exception exx)
            //{
            //}
            //return -1;
        }
        public double GetDouble(int index)
        {
            //try
            //{
            return double.Parse(strList[index]);
            //}
            //catch(Exception exx)
            //{
            //}
            //return -1;
        }
        public static string RemoveAllSpaces(string s)
        {
            s = s.Replace('\t', ' ').TrimStart().TrimEnd().Trim();
            while (s.Contains("  "))
            {
                s = s.Replace("  ", " ");
            }
            return s;
        }
        public static double StringToDouble(string s, double defaultValue)
        {
            s = s.Replace('\t', ' ').TrimStart().TrimEnd().Trim();
            while (s.Contains("  "))
            {
                s = s.Replace("  ", " ");
            }
            double d = 0.0d;
            try
            {
                d = double.Parse(s);
            }
            catch (Exception ex)
            {
                d = defaultValue;
            }
            return d;
        }
        public static decimal StringToDecimal(string s, decimal defaultValue)
        {
            s = s.Replace('\t', ' ').TrimStart().TrimEnd().Trim();
            while (s.Contains("  "))
            {
                s = s.Replace("  ", " ");
            }
            decimal d = 0.0m;
            try
            {
                d = decimal.Parse(s);
            }
            catch (Exception ex)
            {
                d = defaultValue;
            }
            return d;
        }
        public static int StringToInt(string s, int defaultValue)
        {
            s = s.Replace('\t', ' ').TrimStart().TrimEnd().Trim();
            while (s.Contains("  "))
            {
                s = s.Replace("  ", " ");
            }
            int i = 0;
            try
            {
                i = int.Parse(s);
            }
            catch (Exception ex)
            {
                i = defaultValue;
            }
            return i;
        }
        public int Count
        {
            get
            {
                return StringList.Count;
            }
        }

        public string GetString(int afterIndex)
        {
            string kStr = "";
            for (int i = afterIndex; i < strList.Count; i++)
            {
                kStr += strList[i] + sp_char;
            }
            return kStr;
        }
        public static string Get_Modified_Path(string full_path)
        {
            try
            {
                string tst1 = Path.GetFileName(full_path);
                string tst2 = Path.GetFileName(Path.GetDirectoryName(full_path));
                string tst3 = Path.GetPathRoot(full_path);
                if (tst2 != "")
                    full_path = tst3 + "....\\" + Path.Combine(tst2, tst1);
            }
            catch (Exception ex) { }
            return full_path;
        }
        public static string Get_Array_Text(List<int> list_int)
        {
            string kStr = "";
            int end_mbr_no, start_mbr_no;

            start_mbr_no = 0;
            end_mbr_no = 0;
            list_int.Sort();
            for (int i = 0; i < list_int.Count; i++)
            {
                if (i == 0)
                {
                    start_mbr_no = list_int[i];
                    end_mbr_no = list_int.Count == 1 ? start_mbr_no : start_mbr_no + 1;
                    continue;
                }
                if (end_mbr_no == list_int[i])
                    end_mbr_no++;
                else
                {
                    end_mbr_no = list_int[i - 1];

                    if ((end_mbr_no - start_mbr_no) == 1)
                        kStr += " " + start_mbr_no + " " + (end_mbr_no);
                    else if (end_mbr_no != start_mbr_no)
                        kStr += "   " + start_mbr_no + " TO " + (end_mbr_no);
                    else
                        kStr += " " + start_mbr_no;

                    start_mbr_no = list_int[i];
                    end_mbr_no = start_mbr_no + 1;
                }
                if (i == list_int.Count - 1)
                {
                    end_mbr_no = list_int[i];
                }
            }
            //if (end_mbr_no != start_mbr_no)
            //    kStr += "   " + start_mbr_no + " TO " + (end_mbr_no);
            //else
            //    kStr += " " + start_mbr_no;


            if ((end_mbr_no - start_mbr_no) == 1)
                kStr += " " + start_mbr_no + " " + (end_mbr_no);
            else if (end_mbr_no != start_mbr_no)
                kStr += "   " + start_mbr_no + " TO " + (end_mbr_no);
            else
                kStr += " " + start_mbr_no;
            return RemoveAllSpaces(kStr);
        }
        public static List<int> Get_Array_Intiger(string list_text)
        {
            //11 12 15 TO 21 43 89 98, 45 43  

            List<int> list = new List<int>();
            string kStr = RemoveAllSpaces(list_text.ToUpper().Trim());


            try
            {
                kStr = kStr.Replace("T0", "TO");
                kStr = kStr.Replace("-", "TO");
                kStr = kStr.Replace(",", " ");
                kStr = RemoveAllSpaces(kStr.ToUpper().Trim());

                MyList mlist = new MyList(MyList.RemoveAllSpaces(kStr), ' ');


                int start = 0;
                int end = 0;


                for (int i = 0; i < mlist.Count; i++)
                {
                    try
                    {
                        if (mlist.StringList[i] != "TO")
                            start = mlist.GetInt(i);
                    }
                    catch (Exception exx)
                    {
                        kStr = mlist.GetString(0);
                        break;
                    }
                }
                mlist = new MyList(MyList.RemoveAllSpaces(kStr), ' ');


                for (int i = 0; i < mlist.Count; i++)
                {
                    try
                    {
                        start = mlist.GetInt(i);
                        if (i < mlist.Count - 1)
                        {
                            if (mlist.StringList[i + 1] == "TO")
                            {
                                //start = mlist.GetInt(i);
                                end = mlist.GetInt(i + 2);
                                for (int j = start; j <= end; j++) list.Add(j);
                                i += 2;
                            }
                            else
                                list.Add(start);
                        }
                        else
                            list.Add(start);
                    }
                    catch (Exception ex) { }
                }
                mlist = null;
            }
            catch (Exception ex) { }
            list.Sort();
            return list;
        }

        public static double Get_Max_Value(List<double> list)
        {
            double d = list[0];
            for (int i = 0; i < list.Count; i++)
            {
                if (d < list[i]) d = list[i];
            }
            return d;
        }

        public static double Get_Min_Value(List<double> list)
        {
            double d = list[0];
            for (int i = 0; i < list.Count; i++)
            {
                if (d > list[i]) d = list[i];
            }
            return d;
        }


        public override string ToString()
        {
            return GetString(0);
        }


        public static string GetSectionSize(string s)
        {

            string kStr = s;

            if (kStr.ToUpper().Contains("X")) return kStr;

            if (kStr.Length % 2 != 0)
            {
                int i = kStr.Length / 2 + 1;
                kStr = s.Substring(0, i) + "X" + s.Substring(i, s.Length - (i));
            }
            else
            {
                int i = kStr.Length / 2;
                kStr = s.Substring(0, i) + "X" + s.Substring(i, s.Length - (i));
            }
            return kStr;
        }
        public static void Delete_Folder(string folder)
        {
            try
            {
                if (Directory.Exists(folder))
                {
                    foreach (var item in Directory.GetDirectories(folder))
                    {
                        Delete_Folder(item);
                    }
                    foreach (var item in Directory.GetFiles(folder))
                    {
                        File.Delete(item);
                    }
                    Directory.Delete(folder);
                }
            }
            catch (Exception exx) { }
        }

    }
    

    #region Chiranjit [2011 09 26] Get this data from ASTRA Steel Truss Analysis
    public class BeamMemberForce
    {
        public BeamMemberForce()
        {
            JointNo = -1;
            R1_Axial = 0.0;
            R2_Shear = 0.0;
            R3_Shear = 0.0;
            M1_Torsion = 0.0;
            M2_Bending = 0.0;
            M3_Bending = 0.0;
        }
        public int JointNo { get; set; }
        public double R1_Axial { get; set; }
        public double R2_Shear { get; set; }
        public double R3_Shear { get; set; }
        public double M1_Torsion { get; set; }
        public double M2_Bending { get; set; }
        public double M3_Bending { get; set; }

        public static BeamMemberForce Parse(string s)
        {
            string kStr = s.ToUpper();
            kStr = MyList.RemoveAllSpaces(kStr);
            MyList mList = new MyList(kStr, ' ');

            BeamMemberForce bf = new BeamMemberForce();
            if (mList.Count == 8)
            {

                bf.R1_Axial = mList.GetDouble(2);
                bf.R2_Shear = mList.GetDouble(3);
                bf.R3_Shear = mList.GetDouble(4);
                bf.M1_Torsion = mList.GetDouble(5);
                bf.M2_Bending = mList.GetDouble(6);
                bf.M3_Bending = mList.GetDouble(7);
            }
            else if (mList.Count == 6)
            {

                bf.R1_Axial = mList.GetDouble(0);
                bf.R2_Shear = mList.GetDouble(1);
                bf.R3_Shear = mList.GetDouble(2);
                bf.M1_Torsion = mList.GetDouble(3);
                bf.M2_Bending = mList.GetDouble(4);
                bf.M3_Bending = mList.GetDouble(5);
            }
            return bf;
        }

        public double MaxCompressionForce
        {
            get
            {
                double frc = 0.0;
                double comp_frc = 0.0;

                //frc = R1_Shear;
                //if (frc < 0)
                //{
                //    if (Math.Abs(frc) > Math.Abs(comp_frc))
                //        comp_frc = frc;
                //}
                //frc = R3_Shear;
                //if (frc < 0)
                //{
                //    if (Math.Abs(frc) > Math.Abs(comp_frc))
                //        comp_frc = frc;
                //}
                //frc = M2_Bending;
                //if (frc < 0)
                //{
                //    if (Math.Abs(frc) > Math.Abs(comp_frc))
                //        comp_frc = frc;
                //}
                //frc = M3_Bending;
                //if (frc < 0)
                //{
                //    if (Math.Abs(frc) > Math.Abs(comp_frc))
                //        comp_frc = frc;
                //}

                return comp_frc;
            }
        }
        public double MaxTensileForce
        {
            get
            {
                double frc = 0.0;
                double tens_frc = 0.0;

                //frc = R2_Shear;
                //if (frc > 0)
                //{
                //    if (Math.Abs(frc) > Math.Abs(tens_frc))
                //        tens_frc = frc;
                //}
                //frc = R3_Shear;
                //if (frc > 0)
                //{
                //    if (Math.Abs(frc) > Math.Abs(tens_frc))
                //        tens_frc = frc;
                //}
                //frc = M2_Bending;
                //if (frc > 0)
                //{
                //    if (Math.Abs(frc) > Math.Abs(tens_frc))
                //        tens_frc = frc;
                //}
                //frc = M3_Bending;
                //if (frc > 0)
                //{
                //    if (Math.Abs(frc) > Math.Abs(tens_frc))
                //        tens_frc = frc;
                //}

                return tens_frc;
            }
        }

        public double MaxAxialForce
        {
            get
            {
                double frc = 0.0;
                double comp_frc = 0.0;

                comp_frc = R1_Axial;
                //frc = R1_Shear;
                //if (frc < 0)
                //{
                //    if (Math.Abs(frc) > Math.Abs(comp_frc))
                //        comp_frc = frc;
                //}
                //frc = R3_Shear;
                //if (frc < 0)
                //{
                //    if (Math.Abs(frc) > Math.Abs(comp_frc))
                //        comp_frc = frc;
                //}
                //frc = M2_Bending;
                //if (frc < 0)
                //{
                //    if (Math.Abs(frc) > Math.Abs(comp_frc))
                //        comp_frc = frc;
                //}
                //frc = M3_Bending;
                //if (frc < 0)
                //{
                //    if (Math.Abs(frc) > Math.Abs(comp_frc))
                //        comp_frc = frc;
                //}

                return comp_frc;
            }
        }
        public double MaxTorsion
        {
            get
            {
                double frc = 0.0;
                double comp_frc = 0.0;

                frc = M1_Torsion;

                return frc;
            }
        }

        public double MaxBendingMoment
        {
            get
            {
                if (Math.Abs(M2_Bending) > Math.Abs(M3_Bending)) return M2_Bending;
                return M3_Bending;
            }
        }
        public double MaxShearForce
        {
            get
            {
                if (Math.Abs(R2_Shear) > Math.Abs(R3_Shear)) return R2_Shear;
                return R3_Shear;
            }
        }
    }

    public class StructureMemberAnalysis
    {
        Thread thd = null;
        public delegate void SetProgressValue(ProgressBar pbr, int val);

        SetProgressValue spv;

        ProgressBar pbr;
        public List<JointForce> list_joints = null;
        public List<AnalysisData> list_analysis = null;
        public List<AstraBeamMember> list_beams = null;
        public List<AstraTrussMember> list_trusses = null;
        public List<AstraTrussMember> list_cables = null;
        public List<AstraPlateMember> list_plates = null;

        public List<int> list_mem_no = null;
        public Hashtable hash_table = null;
        public List<AstraMember> lst_ast_mems = null;
        public List<JointDisplacement> lst_jnt_disps = null;

        public List<SupportReactions> lst_supp_reactns = null;

        public string MassUnit
        {
            get
            {

                if (truss_analysis != null)
                {
                    return truss_analysis.MassUnit;
                    if (truss_analysis.MassUnit.Length > 3)
                        return truss_analysis.MassUnit.Substring(0, 3);
                    else
                        return truss_analysis.MassUnit;
                }
                return "Ton";
            }
        }
        public string LengthUnit
        {
            get
            {
                if (truss_analysis != null)
                {
                    return truss_analysis.LengthUnit;
                    if (truss_analysis.MassUnit.Length > 2)
                        return truss_analysis.LengthUnit.Substring(0, 2);
                    else
                        return truss_analysis.LengthUnit;
                } return "m";
            }
        }

        CAnalysis truss_analysis = null;

        //public SteelTrussMemberAnalysis(string analysis_file)
        //{
        //    list_mem_no = new List<int>();
        //    //this.pbr = this_pbr;

        //    Analysis_File = analysis_file;
        //    if (ReadFromFile())
        //    {
        //        truss_analysis = new CTrussAnalysis(Analysis_File);


        //        for (int i = 0; i < truss_analysis.Members.Count; i++)
        //        {

        //            for (int j = 0; j < list_beams.Count; j++)
        //            {
        //                if (list_beams[j].BeamNo == truss_analysis.Members[i].MemberNo)
        //                {
        //                    list_beams[j].EndNodeForce.JointNo = truss_analysis.Members[i].EndNode.NodeNo;
        //                    list_beams[j].StartNodeForce.JointNo = truss_analysis.Members[i].StartNode.NodeNo;
        //                }
        //            }
        //        }
        //        SetAnalysisData();
        //    }
        //}
        public StructureMemberAnalysis(string analysis_file)
        {


            list_joints = new List<JointForce>();
            list_analysis = new List<AnalysisData>();
            list_beams = new List<AstraBeamMember>();
            list_trusses = new List<AstraTrussMember>();
            list_cables = new List<AstraTrussMember>();
            list_mem_no = new List<int>();
            hash_table = new Hashtable();
            lst_ast_mems = new List<AstraMember>();
            lst_jnt_disps = new List<JointDisplacement>();
            lst_supp_reactns = new List<SupportReactions>();
                                                                                                                               

            list_mem_no = new List<int>();
            //this.pbr = this_pbr;

            Analysis_File = analysis_file;
            frm_ProgressBar.ON("Read Truss/Beam Members..");
            if (File.Exists(analysis_file) && ReadFromFile())
            {
                truss_analysis = new CAnalysis(Analysis_File);
                try
                {
                    for (int i = 0; i < truss_analysis.Members.Count; i++)
                    {

                        for (int j = 0; j < list_beams.Count; j++)
                        {
                            if (list_beams[j].BeamNo == truss_analysis.Members[i].MemberNo)
                            {
                                list_beams[j].EndNodeForce.JointNo = truss_analysis.Members[i].EndNode.NodeNo;
                                list_beams[j].StartNodeForce.JointNo = truss_analysis.Members[i].StartNode.NodeNo;
                            }
                        }
                        frm_ProgressBar.SetValue(i, truss_analysis.Members.Count);
                    }
                }
                catch (Exception ex11) { }
                frm_ProgressBar.OFF();
                SetAnalysisData();
            }
        }

        public StructureMemberAnalysis(string analysis_file, ProgressBar this_pbr)
        {
            list_mem_no = new List<int>();
            this.pbr = this_pbr;

            Analysis_File = analysis_file;
            spv = new SetProgressValue(SetProgressBar);
            //RunThread();
        }
        public string Analysis_File { get; set; }

        public CAnalysis Analysis
        {
            get
            {
                return truss_analysis;
            }
        }
        bool ReadFromFile()
        {
            if (!File.Exists(Analysis_File)) return false;
            List<string> file_content = new List<string>(File.ReadAllLines(Analysis_File));

            List<string> list_mem_frc = new List<string>();

            list_mem_frc.Add("");
            list_mem_frc.Add("");
            list_mem_frc.Add("Taken from file ANALYSIS_REP.TXT");
            //list_mem_frc.Add("       Member   Load     Node    AXIAL       SHEAR       SHEAR     TORSION     BENDING     BENDING");
            //list_mem_frc.Add("       No:      Case     Nos:       R1          R2          R3          M1          M2          M3");
            list_mem_frc.Add("");
            list_mem_frc.Add("");



            try
            {
                bool mem_no_flag = false;
                bool joint_disp_flag = false;
                bool joint_force_flag = false;
                bool truss_force_flag = false;
                bool cable_force_flag = false;
                bool beam_force_flag = false;
                bool plate_force_flag = false;
                bool support_reaction_flag = false;
                                            
                string kStr = "";
                hash_table = new Hashtable();
                AstraMember ast_mem = new AstraMember();
                MyList mList = null;

                list_joints = new List<JointForce>();
                list_trusses = new List<AstraTrussMember>();
                list_cables = new List<AstraTrussMember>();
                list_beams = new List<AstraBeamMember>();
                list_plates = new List<AstraPlateMember>();
                lst_ast_mems = new List<AstraMember>();
                lst_jnt_disps = new List<JointDisplacement>();
                lst_supp_reactns = new List<SupportReactions>();

                AstraBeamMember beam_member = null;
                AstraTrussMember truss_member = null;

                SupportReactions supp_reac = new SupportReactions();

                frm_ProgressBar.ON("Reading Data...");
                bool flag = false;
                bool truss_title = false;
                bool beam_title = false;
                bool cable_title = false;

                for (int i = 0; i < file_content.Count; i++)
                {
                    #region Set Member Forces
                    kStr = file_content[i];
                    if (kStr.StartsWith("*") || kStr == "") 
                        continue;

                    kStr = MovingLoadAnalysis.MyList.RemoveAllSpaces(file_content[i]);


                    if (kStr.Contains("T I M E L O G"))
                    {
                        list_mem_frc.Add("");
                        list_mem_frc.Add("");
                        list_mem_frc.Add("********************         END OF REPORT      **********************");
                        list_mem_frc.Add("");
                        list_mem_frc.Add("");
                        flag = false;
                    }
                    if (kStr.Contains("TRUSS MEMBER ACTIONS"))
                    {
                        flag = true;
                        if (!truss_title)
                        {
                            list_mem_frc.Add("");
                            list_mem_frc.Add("TRUSS MEMBER ACTIONS");
                            list_mem_frc.Add("");
                            list_mem_frc.Add("    MEMBER      LOAD         STRESS              FORCE");
                            list_mem_frc.Add("");
                            truss_title = true;
                        }
                        kStr = "";
                    }
                    if (kStr.Contains("BEAM FORCES AND MOMENTS"))
                    {
                        flag = true;
                        if (!beam_title)
                        {
                            list_mem_frc.Add("");
                            list_mem_frc.Add("BEAM MEMBER FORCES AND MOMENTS");
                            list_mem_frc.Add("");
                            list_mem_frc.Add(" BEAM LOAD     AXIAL       SHEAR       SHEAR     TORSION     BENDING     BENDING");
                            list_mem_frc.Add("  NO.  NO.        R1          R2          R3          M1          M2          M3");
                            list_mem_frc.Add("");
                            beam_title = true;
                        }
                        kStr = "";
                    }
                    if (kStr.Contains("CABLE MEMBER ACTIONS"))
                    {
                        flag = true;
                        if (!cable_title)
                        {
                            list_mem_frc.Add("CABLE MEMBER ACTIONS");
                            list_mem_frc.Add("");
                            list_mem_frc.Add("    MEMBER      LOAD         STRESS              FORCE");
                            list_mem_frc.Add("");
                            cable_title = true;
                        }
                        kStr = "";
                    }

                    if (kStr == "BEAM LOAD AXIAL SHEAR SHEAR TORSION BENDING BENDING") kStr = "";
                    if (kStr == "NO. NO. R1 R2 R3 M1 M2 M3") kStr = "";
                    if (kStr == "MEMBER LOAD STRESS FORCE") kStr = "";


                    if (flag && !kStr.Contains("*") && kStr.Length > 9)
                    {
                        if ((kStr != "MEMBER LOAD STRESS FORCE") ||
                            (kStr != ".....BEAM FORCES AND MOMENTS"))
                            list_mem_frc.Add(file_content[i]);
                    }



                    #endregion Set Member Forces







                    kStr = file_content[i].ToUpper();
                    kStr = kStr.Replace(',', ' ');
                    kStr = MyList.RemoveAllSpaces(kStr);
                    kStr = kStr.Replace("MEMBER NUMBER", " ");

                    //if (i >= 729)
                    //kStr = kStr.Replace(',', ' ');


                    kStr = MyList.RemoveAllSpaces(kStr);
                    if (kStr.Contains("END OF USER'S DATA"))
                    {
                        mem_no_flag = true;
                    }
                    if (kStr.Contains("::") && mem_no_flag)
                    {
                        mem_no_flag = true;
                        mList = new MyList(kStr, ' ');
                        ast_mem = new AstraMember();
                        //0          1     2   3   4    5    6
                        //TRUSS:: User's TRUSS 1 ASTRA TRUSS 1
                        //TRUSS:: User's TRUSS 1 ASTRA TRUSS 1
                        if (kStr.Contains("TRUSS/CABLE"))
                        {
                            ast_mem.AstraMemberType = eAstraMemberType.CABLE;
                            ast_mem.UserNo = mList.GetInt(3);
                            ast_mem.AstraMemberNo = mList.GetInt(3);
                            ast_mem.AstraMemberNo = mList.GetInt(6);
                            hash_table.Add(ast_mem.UserNo, ast_mem);
                            lst_ast_mems.Add(ast_mem);
                        }
                        else if (kStr.StartsWith("TRUSS"))
                        {
                            ast_mem.AstraMemberType = eAstraMemberType.TRUSS;
                            ast_mem.UserNo = mList.GetInt(3);
                            ast_mem.AstraMemberNo = mList.GetInt(3);
                            ast_mem.AstraMemberNo = mList.GetInt(6);
                            hash_table.Add(ast_mem.UserNo, ast_mem);
                            lst_ast_mems.Add(ast_mem);
                        }
                        else if (kStr.StartsWith("CABLE")) // Chiranjit [2011 11 12] this is a new Item for ASTRA Members
                        {
                            try
                            {
                                ast_mem.AstraMemberType = eAstraMemberType.CABLE;
                                ast_mem.UserNo = mList.GetInt(3);
                                ast_mem.AstraMemberNo = mList.GetInt(3);
                                ast_mem.AstraMemberNo = mList.GetInt(6);
                                hash_table.Add(ast_mem.UserNo, ast_mem);
                                lst_ast_mems.Add(ast_mem);
                            }
                            catch (Exception ex) { }
                        }
                        else if (kStr.StartsWith("BEAM"))
                        {
                            ast_mem.AstraMemberType = eAstraMemberType.BEAM;
                            ast_mem.UserNo = mList.GetInt(4);
                            ast_mem.AstraMemberNo = mList.GetInt(7);
                            hash_table.Add(ast_mem.UserNo, ast_mem);
                            lst_ast_mems.Add(ast_mem);
                        }
                        list_mem_no.Add(ast_mem.UserNo);

                    }
                    else if (kStr.Contains("******************************") && mem_no_flag)
                    {
                        mem_no_flag = false;
                    }
                    else if (kStr.Contains("TRUSS MEMBER ACTIONS"))
                    {
                        joint_disp_flag = false;
                        joint_force_flag = false;
                        truss_force_flag = true;
                        cable_force_flag = false;
                        beam_force_flag = false;
                        plate_force_flag = false;
                    }
                    else if (kStr.Contains("CABLE MEMBER ACTIONS"))
                    {
                        joint_disp_flag = false;
                        joint_force_flag = false;
                        cable_force_flag = true;
                        truss_force_flag = false;
                        beam_force_flag = false;
                        plate_force_flag = false;
                    }
                    else if (kStr.Contains(".....BEAM FORCES AND MOMENTS"))
                    {
                        joint_disp_flag = false;
                        joint_force_flag = false;
                        beam_force_flag = true;
                        truss_force_flag = false;
                        cable_force_flag = false;
                        plate_force_flag = false;
                    }
                    else if (kStr.Contains("N O D A L L O A D S (S T A T I C) O R M A S S E S (D Y N A M I C)"))
                    {
                        joint_disp_flag = false;
                        joint_force_flag = true;
                        beam_force_flag = false;
                        truss_force_flag = false;
                        cable_force_flag = false;
                        plate_force_flag = false;
                    }

                    else if (kStr.Contains("N O D E D I S P L A C E M E N T S / R O T A T I O N S"))
                    {
                        joint_disp_flag = true;
                        joint_force_flag = false;
                        beam_force_flag = false;
                        truss_force_flag = false;
                        cable_force_flag = false;
                        plate_force_flag = false;
                    }
                    else if (kStr.Contains("SHELL ELEMENT STRESSES"))
                    {

                        joint_disp_flag = false;
                        joint_force_flag = false;
                        beam_force_flag = false;
                        truss_force_flag = false;
                        cable_force_flag = false;
                        plate_force_flag = true;
                    }
                    else if (kStr.Contains("SUMMARY OF MAXIMUM SUPPORT FORCES"))
                    {

                        joint_disp_flag = false;
                        joint_force_flag = false;
                        beam_force_flag = false;
                        truss_force_flag = false;
                        cable_force_flag = false;
                        plate_force_flag = false;
                        support_reaction_flag = true;
                        continue;
                    }

                    if (joint_force_flag)
                    {
                        try
                        {
                            mList = new MyList(kStr, ' ');

                            if (mList.Count == 8)
                            {
                                JointForce jf = new JointForce();
                                jf.NodeNo = mList.GetInt(0);
                                jf.LoadNo = mList.GetInt(1);
                                jf.FX = mList.GetDouble(2);
                                jf.FY = mList.GetDouble(3);
                                jf.FZ = mList.GetDouble(4);
                                jf.MX = mList.GetDouble(5);
                                jf.MY = mList.GetDouble(6);
                                jf.MZ = mList.GetDouble(7);
                                list_joints.Add(jf);
                            }
                        }
                        catch (Exception ex) { }
                    }
                    if (joint_disp_flag)
                    {
                        try
                        {
                            mList = new MyList(kStr, ' ');

                            if (mList.Count == 8)
                            {
                                JointDisplacement jf = new JointDisplacement();
                                jf.JointNo = mList.GetInt(0);
                                jf.LoadNo = mList.GetInt(1);
                                jf.Tx = mList.GetDouble(2);
                                jf.Ty = mList.GetDouble(3);
                                jf.Tz = mList.GetDouble(4);
                                jf.Rx = mList.GetDouble(5);
                                jf.Ry = mList.GetDouble(6);
                                jf.Rz = mList.GetDouble(7);
                                lst_jnt_disps.Add(jf);
                            }
                        }
                        catch (Exception ex) { }
                    }
                    if (truss_force_flag)
                    {
                        try
                        {
                            list_trusses.Add(AstraTrussMember.Parse(kStr));
                        }
                        catch (Exception ex) { }
                    }
                    if (cable_force_flag)
                    {
                        try
                        {
                            list_cables.Add(AstraTrussMember.Parse(kStr));
                        }
                        catch (Exception ex) { }
                    }
                    if (beam_force_flag)
                    {
                        try
                        {
                            mList = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
                            if (mList.Count == 8)
                            {
                                beam_member = new AstraBeamMember();
                                beam_member.BeamNo = mList.GetInt(0);
                                beam_member.LoadNo = mList.GetInt(1);
                                beam_member.StartNodeForce = BeamForce.Parse(kStr);
                            }
                            else if (mList.Count == 6)
                            {
                                beam_member.EndNodeForce = BeamForce.Parse(kStr);
                                list_beams.Add(beam_member);
                            }
                        }
                        catch (Exception ex) { }
                    }

                    if (plate_force_flag)
                    {
                        try
                        {
                            list_plates.Add(AstraPlateMember.Parse(kStr));
                        }
                        catch (Exception ex) { }
                    }
                    if (support_reaction_flag)
                    {
                        try
                        {

                            if (kStr.Contains("MAXIMUM REACTION"))
                            {
                                kStr = kStr.Replace("AT SUPPORT=", " ");
                                kStr = kStr.Replace("FOR LOAD CASE=", " ");
                                kStr = kStr.Replace("MAXIMUM REACTION=", " ");
                                kStr = kStr.Replace("=", " ");
                                kStr = MyList.RemoveAllSpaces(kStr);

                                mList = new MyList(kStr, ' ');
                                supp_reac = new SupportReactions();

                                supp_reac.Support_No = mList.GetInt(0);
                                supp_reac.Max_Reaction_Loadcase = mList.GetInt(1);
                                supp_reac.Max_Reaction = mList.GetDouble(2);
                                lst_supp_reactns.Add(supp_reac);
                            }
                            else if (kStr.Contains("MAXIMUM +VE MX"))
                            {
                                kStr = kStr.Replace("AT SUPPORT", " ");
                                kStr = kStr.Replace("FOR LOAD CASE", " ");
                                kStr = kStr.Replace("MAXIMUM +VE MX", " ");
                                kStr = kStr.Replace("MAXIMUM -VE MX", " ");
                                kStr = kStr.Replace("=", " ");
                                kStr = MyList.RemoveAllSpaces(kStr);

                                mList = new MyList(kStr, ' ');
                                //supp_reac = new SupportReactions();

                                supp_reac.Max_Positive_Mx_Loadcase = mList.GetInt(1);
                                supp_reac.Max_Positive_Mx = mList.GetDouble(2);



                                supp_reac.Max_Negative_Mx_Loadcase = mList.GetInt(3);
                                supp_reac.Max_Negative_Mx = mList.GetDouble(4);

                            }

                            else if (kStr.Contains("MAXIMUM +VE MZ"))
                            {
                                kStr = kStr.Replace("AT SUPPORT", " ");
                                kStr = kStr.Replace("FOR LOAD CASE", " ");
                                kStr = kStr.Replace("MAXIMUM +VE MZ", " ");
                                kStr = kStr.Replace("MAXIMUM -VE MZ", " ");
                                kStr = kStr.Replace("=", " ");
                                kStr = MyList.RemoveAllSpaces(kStr);

                                mList = new MyList(kStr, ' ');
                                //supp_reac = new SupportReactions();

                                supp_reac.Max_Positive_Mz_Loadcase = mList.GetInt(1);
                                supp_reac.Max_Positive_Mz = mList.GetDouble(2);



                                supp_reac.Max_Negative_Mz_Loadcase = mList.GetInt(3);
                                supp_reac.Max_Negative_Mz = mList.GetDouble(4);

                            }
                        }
                        catch (Exception ex) { }
                    }
                    frm_ProgressBar.SetValue(i, file_content.Count);
                }

                File.WriteAllLines(MemberForce_File, list_mem_frc.ToArray());
            }
            catch (Exception ex) { }
            finally
            {
                file_content.Clear();
                file_content = null;
                frm_ProgressBar.OFF();
                //SetMemberForces();
            }
            return true;
        }
        void SetMemberForces()
        {
            string temp_file = "";
            string src_file = "";

            temp_file = MemberForce_File;
            src_file = Path.Combine(Path.GetDirectoryName(Analysis_File), "ANALYSIS_REP.txt");
            if (!File.Exists(src_file)) return;

            StreamWriter sw = new StreamWriter(new FileStream(temp_file, FileMode.Create));
            StreamReader sr = new StreamReader(new FileStream(Analysis_File, FileMode.Open));

            string str = "";
            string kstr = "";
            bool flag = false;

            bool truss_title = false;
            bool beam_title = false;
            bool cable_title = false;
            try
            {
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Taken from file ANALYSIS_REP.TXT");
                //sw.WriteLine("       Member   Load     Node    AXIAL       SHEAR       SHEAR     TORSION     BENDING     BENDING");
                //sw.WriteLine("       No:      Case     Nos:       R1          R2          R3          M1          M2          M3");
                sw.WriteLine();
                sw.WriteLine();
                frm_ProgressBar.ON("Reading Member Forces...");
                while (!sr.EndOfStream)
                {
                    str = sr.ReadLine().ToUpper();
                    kstr = MovingLoadAnalysis.MyList.RemoveAllSpaces(str);

                    //str = str.Replace("\t\t", "\t    ");


                    if (kstr.Contains("T I M E L O G"))
                    {
                        //if (kstr.Contains("S T A T I C S O L U T I O N T I M E L O G")
                        sw.WriteLine();
                        sw.WriteLine();
                        sw.WriteLine("********************         END OF REPORT      **********************");
                        sw.WriteLine();
                        sw.WriteLine();

                        break;
                    }
                    if (str.Contains("TRUSS MEMBER ACTIONS"))
                    {
                        flag = true;
                        if (!truss_title)
                        {
                            sw.WriteLine();
                            sw.WriteLine("TRUSS MEMBER ACTIONS");
                            sw.WriteLine();
                            sw.WriteLine("    MEMBER      LOAD         STRESS              FORCE");
                            sw.WriteLine();
                            truss_title = true;
                        }
                        continue;
                    }
                    if (str.Contains("BEAM FORCES AND MOMENTS"))
                    {
                        flag = true;
                        if (!beam_title)
                        {
                            sw.WriteLine();
                            sw.WriteLine("BEAM MEMBER FORCES AND MOMENTS");
                            sw.WriteLine();
                            sw.WriteLine(" BEAM LOAD     AXIAL       SHEAR       SHEAR     TORSION     BENDING     BENDING");
                            sw.WriteLine("  NO.  NO.        R1          R2          R3          M1          M2          M3");
                            sw.WriteLine();
                            beam_title = true;
                        }
                        continue;
                    }
                    if (str.Contains("CABLE MEMBER ACTIONS"))
                    {
                        flag = true;
                        if (!cable_title)
                        {
                            sw.WriteLine("CABLE MEMBER ACTIONS");
                            sw.WriteLine();
                            sw.WriteLine("    MEMBER      LOAD         STRESS              FORCE");
                            sw.WriteLine();
                            cable_title = true;
                        }
                        continue;
                    }

                    if (kstr == "BEAM LOAD AXIAL SHEAR SHEAR TORSION BENDING BENDING") continue;
                    if (kstr == "NO. NO. R1 R2 R3 M1 M2 M3") continue;
                    if (kstr == "MEMBER LOAD STRESS FORCE") continue;


                    if (flag && !str.Contains("*") && str.Length > 9)
                    {
                        if ((kstr != "MEMBER LOAD STRESS FORCE") ||
                            (kstr != ".....BEAM FORCES AND MOMENTS"))
                            sw.WriteLine(str);
                    }
                    frm_ProgressBar.SetValue(sr.BaseStream.Position, sr.BaseStream.Length);
                }
                frm_ProgressBar.OFF();
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
                sr.Close();
            }
        }
        public string MemberForce_File
        {
            get
            {
                if (File.Exists(Analysis_File))
                    return Path.Combine(Path.GetDirectoryName(Analysis_File), "MemberForces.txt");
                return "";
            }
        }


        void SetAnalysisData()
        {
            MemberAnalysis = new Hashtable();
            list_analysis = new List<AnalysisData>();
            AnalysisData ana_data = null;
            AstraMember mem = null;
            //if (list_mem_no.Count > 1)
            //    frm_ProgressBar.ON("Read Member forces from the Analysis Report");
            for (int i = 0; i < list_mem_no.Count; i++)
            {
                mem = (AstraMember)hash_table[list_mem_no[i]];
                ana_data = new AnalysisData();
                ana_data.UserMemberNo = mem.UserNo;
                ana_data.AstraMemberNo = mem.AstraMemberNo;

                ana_data.CompressionForce = GetMaxCompressionForce(mem);
                ana_data.TensileForce = GetMaxTensileForce(mem);
                ana_data.MaxBendingMoment = GetMaxBendingMoment(mem);
                ana_data.MaxShearForce = GetMaxShearForce(mem);

                ana_data.MaxAxialForce = GetMaxAxialForce(mem);
                ana_data.MaxTorsion = GetMaxTorsion(mem);


                //if (ana_data.CompressionForce == 0.0)
                //    ana_data.CompressionForce = -ana_data.TensileForce;
                //if (ana_data.TensileForce == 0.0)
                //    ana_data.TensileForce = Math.Abs(ana_data.CompressionForce);

                ana_data.TensileForce = Math.Abs(ana_data.TensileForce);
                ana_data.CompressionForce = Math.Abs(ana_data.CompressionForce);

                list_analysis.Add(ana_data);
                ana_data.AstraMemberType = mem.AstraMemberType;
                MemberAnalysis.Add(ana_data.UserMemberNo, ana_data);
                //int v = (int)(((double)(i + 1) /(double) list_mem_no.Count) * 100.0);
                //pbr.Invoke(spv, pbr, v);

                //if (list_mem_no.Count > 1)
                //    frm_ProgressBar.SetValue(i, list_mem_no.Count);
            }
            //if (list_mem_no.Count > 1)
            //    frm_ProgressBar.OFF();

        }
        double GetMaxCompressionForce(AstraMember mem)
        {
            double comp_force = 0.0;
            double frc = 0.0;
            if (mem.AstraMemberType == eAstraMemberType.BEAM)
            {
                return 0.0;
                for (int i = 0; i < list_beams.Count; i++)
                {

                    if (list_beams[i].BeamNo == mem.AstraMemberNo)
                    {
                        frc = list_beams[i].MaxCompressionForce;
                        if (Math.Abs(frc) > Math.Abs(comp_force))
                            comp_force = frc;
                    }
                }
            }
            if (mem.AstraMemberType == eAstraMemberType.TRUSS)
            {
                for (int i = 0; i < list_trusses.Count; i++)
                {
                    if (list_trusses[i].TrussMemberNo == mem.AstraMemberNo)
                    {
                        frc = list_trusses[i].Force;
                        if (frc < 0)
                        {
                            if (Math.Abs(frc) > Math.Abs(comp_force))
                                comp_force = frc;
                        }
                    }
                }
            }
            return comp_force;
        }
        double GetMaxTensileForce(AstraMember mem)
        {
            double tens_force = 0.0;
            double frc = 0.0;
            if (mem.AstraMemberType == eAstraMemberType.BEAM)
            {
                return 0.0;
                for (int i = 0; i < list_beams.Count; i++)
                {
                    if (list_beams[i].BeamNo == mem.AstraMemberNo)
                    {
                        frc = list_beams[i].MaxTensileForce;
                        if (Math.Abs(frc) > Math.Abs(tens_force))
                            tens_force = frc;
                    }
                }
            }
            if (mem.AstraMemberType == eAstraMemberType.TRUSS)
            {
                for (int i = 0; i < list_trusses.Count; i++)
                {
                    if (list_trusses[i].TrussMemberNo == mem.AstraMemberNo)
                    {
                        frc = list_trusses[i].Force;
                        if (frc > 0)
                        {
                            if (Math.Abs(frc) > Math.Abs(tens_force))
                                tens_force = frc;
                        }
                    }
                }
            }
            if (mem.AstraMemberType == eAstraMemberType.CABLE)
            {
                for (int i = 0; i < list_cables.Count; i++)
                {
                    if (list_cables[i].TrussMemberNo == mem.AstraMemberNo)
                    {
                        frc = list_cables[i].Force;
                        if (frc > 0)
                        {
                            if (Math.Abs(frc) > Math.Abs(tens_force))
                                tens_force = frc;
                        }
                    }
                }
            }
            return tens_force;
        }

        double GetMaxBendingMoment(AstraMember mem)
        {
            double bend_frc = 0.0;
            double frc = 0.0;
            if (mem.AstraMemberType == eAstraMemberType.BEAM)
            {
                for (int i = 0; i < list_beams.Count; i++)
                {
                    if (list_beams[i].BeamNo == mem.AstraMemberNo)
                    {
                        frc = list_beams[i].MaxBendingMoment;
                        if (Math.Abs(frc) > Math.Abs(bend_frc))
                            bend_frc = frc;
                    }
                }
            }
            return Math.Abs(bend_frc);
        }
        double GetMaxShearForce(AstraMember mem)
        {
            double shr_frc = 0.0;
            double frc = 0.0;
            if (mem.AstraMemberType == eAstraMemberType.BEAM)
            {
                for (int i = 0; i < list_beams.Count; i++)
                {
                    if (list_beams[i].BeamNo == mem.AstraMemberNo)
                    {
                        frc = list_beams[i].MaxShearForce;
                        if (Math.Abs(frc) > Math.Abs(shr_frc))
                            shr_frc = frc;
                    }
                }
            }
            return Math.Abs(shr_frc);
        }
        double GetMaxAxialForce(AstraMember mem)
        {
            double shr_frc = 0.0;
            double frc = 0.0;
            if (mem.AstraMemberType == eAstraMemberType.BEAM)
            {
                for (int i = 0; i < list_beams.Count; i++)
                {
                    if (list_beams[i].BeamNo == mem.AstraMemberNo)
                    {
                        frc = list_beams[i].MaxAxialForce;
                        if (Math.Abs(frc) > Math.Abs(shr_frc))
                            shr_frc = frc;
                    }
                }
            }
            return Math.Abs(shr_frc);
        }
        double GetMaxTorsion(AstraMember mem)
        {
            double shr_frc = 0.0;
            double frc = 0.0;
            if (mem.AstraMemberType == eAstraMemberType.BEAM)
            {
                for (int i = 0; i < list_beams.Count; i++)
                {
                    if (list_beams[i].BeamNo == mem.AstraMemberNo)
                    {
                        frc = list_beams[i].MaxTorsion;
                        if (Math.Abs(frc) > Math.Abs(shr_frc))
                            shr_frc = frc;
                    }
                }
            }
            return Math.Abs(shr_frc);
        }


        public  double GetMaxReaction(int Support_No)
        {


            double shr_frc = 0.0;
            double frc = 0.0;


            foreach (var item in lst_supp_reactns)
            {
                if (item.Support_No == Support_No)
                {
                    shr_frc = item.Max_Reaction;
                }
            }
            
            return Math.Abs(shr_frc);
        }

        public string GetForce(ref CMember mem)
        {
            //string str = MyList.RemoveAllSpaces(memNos);
            //MyList mList = new MyList(str, ' ');

            List<int> list_memNo = new List<int>();
            string str = MyList.RemoveAllSpaces(mem.Group.MemberNosText);
            MyList mList = new MyList(str, ' ');

            int indx = -1;
            int count = 0;

            //do
            //{
            //    indx = mList.StringList.IndexOf("TO");
            //    if (indx == -1)
            //    {
            //        indx = mList.StringList.IndexOf("-");
            //    }
            //    if (indx != -1)
            //    {
            //        count += mList.GetInt(indx + 1) - mList.GetInt(indx - 1);
            //        count++;
            //        mList.StringList.RemoveRange(0, indx + 2);
            //    }
            //    else if (mList.Count > 1)
            //    {
            //        count += mList.Count;
            //        mList.StringList.Clear();
            //    }
            //}
            //while (mList.Count != 0);

            double max_frc = 0.00;

            double max_bend, max_shr, max_comp, max_tens;

            max_bend = -9999999990.0;
            max_shr = -9999999990.0;
            max_comp = -9999999990.0;
            max_tens = -9999999990.0;


            AnalysisData ana_data;
            mem.Group.SetMemNos();
            if (mem.Group.MemberNos.Count > 0)
            {
                ana_data = (AnalysisData)MemberAnalysis[mem.Group.MemberNos[0]];
                if (ana_data == null) return "";
                for (int i = 0; i < mem.Group.MemberNos.Count; i++)
                {
                    ana_data = (AnalysisData)MemberAnalysis[mem.Group.MemberNos[i]];
                    //if (mem.Force == "") continue;

                    if (ana_data.MaxBendingMoment > max_bend)
                        max_bend = ana_data.MaxBendingMoment;

                    //if (ana_data.MaxShearForce > max_shr)
                    //    max_shr = ana_data.MaxShearForce;

                    if (ana_data.MaxShearForce > max_shr)
                        max_shr = ana_data.MaxShearForce;

                    if (ana_data.CompressionForce > max_comp)
                        max_comp = ana_data.CompressionForce;

                    if (ana_data.TensileForce > max_tens)
                        max_tens = ana_data.TensileForce;
                }
                str = "";
                if (ana_data.AstraMemberType == eAstraMemberType.BEAM)
                {
                    if (max_bend != 0.0)
                    {
                        str = " Moment = " + max_bend.ToString("0.000") + " kN-m";
                    }
                    if (max_shr != 0.0)
                    {
                        if (str != "")
                            str += ", Shear = " + max_shr.ToString("0.000") + " kN";
                        else
                            str += " Shear = " + max_shr.ToString("0.000") + " kN";

                    }
                }
                else
                {
                    if (max_comp != 0.0)
                    {
                        str += " Compression = " + max_comp.ToString("0.000") + " kN";
                    }
                    if (max_tens != 0.0)
                    {
                        if (str != "")
                            str += ", Tension = " + max_tens.ToString("0.000") + " kN";
                        else
                            str += " Tension = " + max_tens.ToString("0.000") + " kN";
                    }
                }
            }

            //mem.MaxCompForce = (max_comp == -9999999990.0) ? 0.0 : max_comp;
            //mem.MaxTensionForce = max_tens;
            //mem.MaxMoment = max_bend;
            //mem.MaxShearForce = max_shr;
            mem.MaxCompForce = (max_comp == -9999999990.0) ? 0.0 : max_comp;
            mem.MaxTensionForce = (max_tens == -9999999990.0) ? 0.0 : max_tens;
            mem.MaxMoment = (max_bend == -9999999990.0) ? 0.0 : max_bend;
            mem.MaxShearForce = (max_shr == -9999999990.0) ? 0.0 : max_shr;


            return str;
        }


        public double GetJoint_Negative_Moment(List<int> joint_array)
        {

            double max_moment = double.MinValue;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    AstraBeamMember bf = new AstraBeamMember();
                    bf = list_beams[j];

                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {
                        if (list_beams[j].StartNodeForce.MaxBendingMoment < 0)
                        {

                            if (max_moment < Math.Abs(list_beams[j].StartNodeForce.M2_Bending))
                            {
                                max_moment = Math.Abs(list_beams[j].StartNodeForce.M2_Bending);
                            }

                            if (max_moment < Math.Abs(list_beams[j].StartNodeForce.M3_Bending))
                            {
                                max_moment = Math.Abs(list_beams[j].StartNodeForce.M3_Bending);
                            }
                        }
                    }
                    if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        if (list_beams[j].EndNodeForce.MaxBendingMoment < 0)
                        {
                            if (max_moment < Math.Abs(list_beams[j].EndNodeForce.M2_Bending))
                            {
                                max_moment = Math.Abs(list_beams[j].EndNodeForce.M2_Bending);
                            }

                            if (max_moment < Math.Abs(list_beams[j].EndNodeForce.M3_Bending))
                            {
                                max_moment = Math.Abs(list_beams[j].EndNodeForce.M3_Bending);
                            }
                        }
                    }
                }
            }

            return (max_moment == double.MinValue) ? 0.0d : max_moment;

        }
        public double GetJoint_Positive_Moment(List<int> joint_array)
        {

            double max_moment = double.MinValue;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    AstraBeamMember bf = new AstraBeamMember();
                    bf = list_beams[j];

                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {
                        if (list_beams[j].StartNodeForce.MaxBendingMoment > 0)
                        {

                            if (max_moment < Math.Abs(list_beams[j].StartNodeForce.M2_Bending))
                            {
                                max_moment = Math.Abs(list_beams[j].StartNodeForce.M2_Bending);
                            }

                            if (max_moment < Math.Abs(list_beams[j].StartNodeForce.M3_Bending))
                            {
                                max_moment = Math.Abs(list_beams[j].StartNodeForce.M3_Bending);
                            }
                        }
                    }
                    if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        if (list_beams[j].EndNodeForce.MaxBendingMoment > 0)
                        {
                            if (max_moment < Math.Abs(list_beams[j].EndNodeForce.M2_Bending))
                            {
                                max_moment = Math.Abs(list_beams[j].EndNodeForce.M2_Bending);
                            }

                            if (max_moment < Math.Abs(list_beams[j].EndNodeForce.M3_Bending))
                            {
                                max_moment = Math.Abs(list_beams[j].EndNodeForce.M3_Bending);
                            }
                        }
                    }
                }
            }

            return (max_moment == double.MinValue) ? 0.0d : max_moment;

        }


        public double GetJoint_MomentForce(List<int> joint_array)
        {
            double max_moment = double.MinValue;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    AstraBeamMember bf = new AstraBeamMember();
                    bf = list_beams[j];

                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {
                        if (max_moment < Math.Abs(list_beams[j].StartNodeForce.M2_Bending))
                        {
                            max_moment = Math.Abs(list_beams[j].StartNodeForce.M2_Bending);
                        }

                        if (max_moment < Math.Abs(list_beams[j].StartNodeForce.M3_Bending))
                        {
                            max_moment = Math.Abs(list_beams[j].StartNodeForce.M3_Bending);
                        }
                    }
                    if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        if (max_moment < Math.Abs(list_beams[j].EndNodeForce.M2_Bending))
                        {
                            max_moment = Math.Abs(list_beams[j].EndNodeForce.M2_Bending);
                        }

                        if (max_moment < Math.Abs(list_beams[j].EndNodeForce.M3_Bending))
                        {
                            max_moment = Math.Abs(list_beams[j].EndNodeForce.M3_Bending);
                        }
                    }
                }
            }

            return (max_moment == double.MinValue) ? 0.0d : max_moment;

        }

        public double GetJoint_M1_Torsion(List<int> joint_array)
        {
            double max_moment = double.MinValue;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    AstraBeamMember bf = new AstraBeamMember();
                    bf = list_beams[j];

                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {
                        if (max_moment < Math.Abs(list_beams[j].StartNodeForce.M1_Torsion))
                        {
                            max_moment = Math.Abs(list_beams[j].StartNodeForce.M1_Torsion);
                        }
                    }
                    if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        if (max_moment < Math.Abs(list_beams[j].EndNodeForce.M1_Torsion))
                        {
                            max_moment = Math.Abs(list_beams[j].EndNodeForce.M1_Torsion);
                        }
                    }
                }
            }

            return (max_moment == double.MinValue) ? 0.0d : max_moment;

        }

        public double GetJoint_M2_Bending(List<int> joint_array)
        {
            double max_moment = double.MinValue;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    AstraBeamMember bf = new AstraBeamMember();
                    bf = list_beams[j];

                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {
                        if (max_moment < Math.Abs(list_beams[j].StartNodeForce.M2_Bending))
                        {
                            max_moment = Math.Abs(list_beams[j].StartNodeForce.M2_Bending);
                        }
                    }
                    if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        if (max_moment < Math.Abs(list_beams[j].EndNodeForce.M2_Bending))
                        {
                            max_moment = Math.Abs(list_beams[j].EndNodeForce.M2_Bending);
                        }
                    }
                }
            }

            return (max_moment == double.MinValue) ? 0.0d : max_moment;

        }

        public double GetJoint_M3_Bending(List<int> joint_array)
        {
            double max_moment = double.MinValue;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    AstraBeamMember bf = new AstraBeamMember();
                    bf = list_beams[j];

                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {
                        if (max_moment < Math.Abs(list_beams[j].StartNodeForce.M3_Bending))
                        {
                            max_moment = Math.Abs(list_beams[j].StartNodeForce.M3_Bending);
                        }
                    }
                    if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        if (max_moment < Math.Abs(list_beams[j].EndNodeForce.M3_Bending))
                        {
                            max_moment = Math.Abs(list_beams[j].EndNodeForce.M3_Bending);
                        }
                    }
                }
            }

            return (max_moment == double.MinValue) ? 0.0d : max_moment;

        }

        public double GetJoint_R1_Axial(List<int> joint_array)
        {
            double max_moment = double.MinValue;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    AstraBeamMember bf = new AstraBeamMember();
                    bf = list_beams[j];

                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {
                        if (max_moment < Math.Abs(list_beams[j].StartNodeForce.R1_Axial))
                        {
                            max_moment = Math.Abs(list_beams[j].StartNodeForce.R1_Axial);
                        }
                    }
                    if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        if (max_moment < Math.Abs(list_beams[j].EndNodeForce.R1_Axial))
                        {
                            max_moment = Math.Abs(list_beams[j].EndNodeForce.R1_Axial);
                        }
                    }
                }
            }

            return (max_moment == double.MinValue) ? 0.0d : max_moment;

        }

        public double GetJoint_R2_Shear(List<int> joint_array)
        {
            double max_moment = double.MinValue;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    AstraBeamMember bf = new AstraBeamMember();
                    bf = list_beams[j];

                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {
                        if (max_moment < Math.Abs(list_beams[j].StartNodeForce.R2_Shear))
                        {
                            max_moment = Math.Abs(list_beams[j].StartNodeForce.R2_Shear);
                        }
                    }
                    if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        if (max_moment < Math.Abs(list_beams[j].EndNodeForce.R2_Shear))
                        {
                            max_moment = Math.Abs(list_beams[j].EndNodeForce.R2_Shear);
                        }
                    }
                }
            }

            return (max_moment == double.MinValue) ? 0.0d : max_moment;

        }

        public double GetJoint_R3_Shear(List<int> joint_array)
        {
            double max_moment = double.MinValue;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    AstraBeamMember bf = new AstraBeamMember();
                    bf = list_beams[j];

                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {
                        if (max_moment < Math.Abs(list_beams[j].StartNodeForce.R3_Shear))
                        {
                            max_moment = Math.Abs(list_beams[j].StartNodeForce.R3_Shear);
                        }
                    }
                    if (list_beams[j].EndNodeForce.JointNo == joint_array[i])
                    {
                        if (max_moment < Math.Abs(list_beams[j].EndNodeForce.R3_Shear))
                        {
                            max_moment = Math.Abs(list_beams[j].EndNodeForce.R3_Shear);
                        }
                    }
                }
            }

            return (max_moment == double.MinValue) ? 0.0d : max_moment;

        }


        public double GetJoint_ShearForce(List<int> joint_array)
        {
            double max_shear = double.MinValue;

            for (int i = 0; i < joint_array.Count; i++)
            {
                for (int j = 0; j < list_beams.Count; j++)
                {
                    if (list_beams[j].StartNodeForce.JointNo == joint_array[i])
                    {
                        if (max_shear < Math.Abs(list_beams[j].StartNodeForce.R2_Shear))
                        {
                            max_shear = Math.Abs(list_beams[j].StartNodeForce.R2_Shear);
                        }

                        if (max_shear < Math.Abs(list_beams[j].StartNodeForce.R3_Shear))
                        {
                            max_shear = Math.Abs(list_beams[j].StartNodeForce.R3_Shear);
                        }
                    }
                }
            }

            return (max_shear == double.MinValue) ? 0.0d : max_shear;

        }

        public Hashtable MemberAnalysis { get; set; }

        void LoadData()
        {
            if (ReadFromFile())
                SetAnalysisData();
        }
        void RunThread()
        {
            ThreadStart ths = new ThreadStart(LoadData);
            thd = new Thread(ths);
            thd.Start();
            //thd.Join();
            //thd.
        }
        //public delegate void SetProgressValue(ProgressBar pbr, int val);

        //SetProgressValue spv;
        public void SetProgressBar(ProgressBar pbr, int val)
        {
            pbr.Value = val;
        }

        public int Get_User_Member_No(int ast_mno, eAstraMemberType emtype)
        {
            foreach (var item in lst_ast_mems)
            {
                if (item.AstraMemberType == emtype && item.AstraMemberNo == ast_mno)
                    return item.UserNo;
            }
            return ast_mno;
        }
        public AstraBeamMember Get_Beam_Forces(int memNo, int loadCase)
        {
            foreach (var item in list_beams)
            {
                if (item.BeamNo == memNo && item.LoadNo == loadCase)
                    return item;
            }
            return null;
        }

    }
    public class TrussDesignResult
    {
        public TrussDesignResult()
        {
            CompressionForce = 0.0;
            TensileForce = 0.0;
            Moment = 0.0;
            Shear = 0.0;
            Result = "";
        }
        public double CompressionForce { get; set; }
        public double TensileForce { get; set; }
        public double Moment { get; set; }
        public double Shear { get; set; }
        public string Result { get; set; }
    }
    public class MaxForce
    {
        public MaxForce(double force, int loadcase, int mem_no)
        {
            Force = force;
            Loadcase = loadcase;
            MemberNo = mem_no;
            NodeNo = mem_no;
        }
        public MaxForce()
        {
            Force = 0.0;
            Loadcase = 0;
            MemberNo = 0;
            NodeNo = 0;
        }
        public double Force { get; set; }
        public int Loadcase { get; set; }
        public int MemberNo { get; set; }
        public int NodeNo { get; set; }
        public override string ToString()
        {
            return Force.ToString("E3");
            //return Force.ToString("f3") + " " + Loadcase + " " + MemberNo;
            //return Force.ToString("f3");
        }



        public List<string> GetDetails(string caption, List<int> joints, string forceUnit)
        {


            joints.Sort();
            List<string> list = new List<string>();
            MyList mlist = new MyList(caption, ':');
            string title = "", forc_title = "";

            if (mlist.Count > 0)
            {
                if (mlist.Count == 2)
                {
                    title = mlist.StringList[0];
                    forc_title = mlist.StringList[1];
                }
            }

            list.Add("----------------------------------------------------------------------------");
            string str = "";
            string str2 = string.Format("{0} : {1} = {2:E3} {3}", title, forc_title, Force, forceUnit);

            list.Add(str2);

            //L/2 :  MAX SHEAR FORCE = 15.12  Ton
            //------------
            //Joints at L/2 : 59 60 61 62 63 
            //Member No : 176
            //Load Case No : 30
            //Node No : 62
            //list.Add(caption);

            for (int i = 0; i < str2.Length; i++)
            {
                str += "-";
            }
            list.Add(str);
            str = MyList.Get_Array_Text(joints);
            //foreach (var item in joints)
            //{
            //    str += item.ToString() + " ";
            //}
            if (joints.Count > 0)
            {
                if (NodeNo != 0)
                    list.Add("Joints at " + title + " : " + str.Trim().TrimStart().TrimEnd());
                else
                    list.Add("Members at " + title + " : " + str.Trim().TrimStart().TrimEnd());
            }
            list.Add("Member No      : " + MemberNo);
            if (NodeNo != 0)
                list.Add("Joint No       : " + NodeNo);
            if (Loadcase != 0)
                list.Add("Load Case No   : " + Loadcase);
            list.Add("----------------------------------------------------------------------------");
            //list.Add("");
            return list;
        }
    }

    public enum eAstraMemberType
    {
        TRUSS = 0,
        BEAM = 1,
        CABLE = 2,
    }

    public class AstraMember
    {
        public AstraMember()
        {
            UserNo = -1;
            AstraMemberType = eAstraMemberType.TRUSS;
            AstraMemberNo = -1;
        }
        public int UserNo { get; set; }
        public eAstraMemberType AstraMemberType { get; set; }
        public int AstraMemberNo { get; set; }
    }
    public class AstraTrussMember
    {
        //MEMBER    LOAD              STRESS               FORCE
        //1         1	     0.1504968266E+05    0.3611923837E+03

        public AstraTrussMember()
        {
            TrussMemberNo = -1;
            LoadNo = -1;
            Stress = -1.0;
            Force = -1.0;
        }
        public int TrussMemberNo { get; set; }
        public int LoadNo { get; set; }
        public double Stress { get; set; }
        public double Force { get; set; }
        public static AstraTrussMember Parse(string s)
        {
            string kStr = s.ToUpper();
            kStr = MyList.RemoveAllSpaces(kStr);

            MyList mlist = new MyList(kStr, ' ');

            AstraTrussMember ast_trss = new AstraTrussMember();
            ast_trss.TrussMemberNo = mlist.GetInt(0);
            ast_trss.LoadNo = mlist.GetInt(1);
            ast_trss.Stress = mlist.GetDouble(2);
            ast_trss.Force = mlist.GetDouble(3);
            return ast_trss;

        }
    }
    public class AstraPlateMember
    {
        //   ELEMENT      LOAD             MEMBRANE STRESS COMPONENTS                      BENDING MOMENT COMPONENTS     
        //NUMBER      CASE        SXX             SYY             SXY            MXX             MYY             MXY  


        //     1         1 -0.32880141E+02 -0.21237268E+03 -0.13321289E+02  0.54702512E+02  0.67273299E+02 -0.12641519E+00
        //     1         2 -0.30895162E+02 -0.21237262E+03 -0.12241097E-10  0.54702514E+02  0.67273290E+02 -0.26702196E-10
        //     1         3 -0.34025843E+02 -0.21237274E+03 -0.17128905E+02  0.54702503E+02  0.67273301E+02 -0.94622247E-01

        public AstraPlateMember()
        {
            PlateNo = -1;
            LoadNo = -1;
            SXX = -1.0;
            SYY = -1.0;
            SXY = -1.0;
            MXX = -1.0;
            MYY = -1.0;
            MXY = -1.0;
        }
        public int PlateNo { get; set; }
        public int LoadNo { get; set; }
        public double SXX { get; set; }
        public double SYY { get; set; }
        public double SXY { get; set; }
        public double MXX { get; set; }
        public double MYY { get; set; }
        public double MXY { get; set; }
        public static AstraPlateMember Parse(string s)
        {
            string kStr = s.ToUpper();
            kStr = MyList.RemoveAllSpaces(kStr);

            MyList mlist = new MyList(kStr, ' ');

            AstraPlateMember ast_trss = new AstraPlateMember();

            int c = 0;
            ast_trss.PlateNo = mlist.GetInt(c++);

            ast_trss.LoadNo = mlist.GetInt(c++);
            ast_trss.SXX = mlist.GetDouble(c++);
            ast_trss.SYY = mlist.GetDouble(c++);
            ast_trss.SXY = mlist.GetDouble(c++);
            ast_trss.MXX = mlist.GetDouble(c++);
            ast_trss.MYY = mlist.GetDouble(c++);
            ast_trss.MXY = mlist.GetDouble(c++);
            return ast_trss;
        }
        static int Last_Member_No = -1;
    }

    public class AstraBeamMember
    {
        //BEAM LOAD     AXIAL       SHEAR       SHEAR     TORSION     BENDING     BENDING
        //NO.  NO.        R1          R2          R3          M1          M2          M3
        //  1   1 -1.503E-01   5.740E-01   2.074E-01  -3.828E-02   1.599E-01   6.521E-02
        //         1.503E-01  -5.740E-01  -2.074E-01   3.828E-02  -1.405E+00   3.379E+00

        public AstraBeamMember()
        {
            BeamNo = -1;
            LoadNo = -1;
            StartNodeForce = new BeamForce();
            EndNodeForce = new BeamForce();
        }
        public int BeamNo { get; set; }
        public int LoadNo { get; set; }
        public BeamForce StartNodeForce { get; set; }
        public BeamForce EndNodeForce { get; set; }

        public double MaxCompressionForce
        {
            get
            {
                //if (Math.Abs(StartNodeForce.MaxCompressionForce) > Math.Abs(EndNodeForce.MaxCompressionForce))
                //    return StartNodeForce.MaxCompressionForce;
                //return EndNodeForce.MaxCompressionForce;
                return 0.0;

            }
        }
        public double MaxTensileForce
        {
            get
            {
                //if (Math.Abs(StartNodeForce.MaxTensileForce) > Math.Abs(EndNodeForce.MaxTensileForce))
                //    return StartNodeForce.MaxTensileForce;
                //return EndNodeForce.MaxTensileForce;
                return 0.0;
            }
        }
        public double MaxBendingMoment
        {
            get
            {
                if (Math.Abs(StartNodeForce.MaxBendingMoment) > Math.Abs(EndNodeForce.MaxBendingMoment))
                    return StartNodeForce.MaxBendingMoment;
                return EndNodeForce.MaxBendingMoment;
            }
        }
        public double MaxShearForce
        {
            get
            {
                if (Math.Abs(StartNodeForce.MaxShearForce) > Math.Abs(EndNodeForce.MaxShearForce))
                    return StartNodeForce.MaxShearForce;
                return EndNodeForce.MaxShearForce;
            }
        }
        public double MaxAxialForce
        {
            get
            {
                if (Math.Abs(StartNodeForce.MaxAxialForce) > Math.Abs(EndNodeForce.MaxAxialForce))
                    return StartNodeForce.MaxAxialForce;
                return EndNodeForce.MaxAxialForce;
            }
        }
        public double MaxTorsion
        {
            get
            {
                if (Math.Abs(StartNodeForce.MaxTorsion) > Math.Abs(EndNodeForce.MaxTorsion))
                    return StartNodeForce.MaxTorsion;
                return EndNodeForce.MaxTorsion;
            }
        }

    }
    public class BeamForce
    {
        public BeamForce()
        {
            JointNo = -1;
            R1_Axial = 0.0;
            R2_Shear = 0.0;
            R3_Shear = 0.0;
            M1_Torsion = 0.0;
            M2_Bending = 0.0;
            M3_Bending = 0.0;
        }
        public int JointNo { get; set; }
        public double R1_Axial { get; set; }
        public double R2_Shear { get; set; }
        public double R3_Shear { get; set; }
        public double M1_Torsion { get; set; }
        public double M2_Bending { get; set; }
        public double M3_Bending { get; set; }

        public static BeamForce Parse(string s)
        {
            string kStr = s.ToUpper();
            kStr = MyList.RemoveAllSpaces(kStr);
            MyList mList = new MyList(kStr, ' ');

            BeamForce bf = new BeamForce();
            if (mList.Count == 8)
            {

                bf.R1_Axial = mList.GetDouble(2);
                bf.R2_Shear = mList.GetDouble(3);
                bf.R3_Shear = mList.GetDouble(4);
                bf.M1_Torsion = mList.GetDouble(5);
                bf.M2_Bending = mList.GetDouble(6);
                bf.M3_Bending = mList.GetDouble(7);
            }
            else if (mList.Count == 6)
            {

                bf.R1_Axial = mList.GetDouble(0);
                bf.R2_Shear = mList.GetDouble(1);
                bf.R3_Shear = mList.GetDouble(2);
                bf.M1_Torsion = mList.GetDouble(3);
                bf.M2_Bending = mList.GetDouble(4);
                bf.M3_Bending = mList.GetDouble(5);
            }
            return bf;
        }

        public double MaxCompressionForce
        {
            get
            {
                double frc = 0.0;
                double comp_frc = 0.0;

                frc = R1_Axial;
                //frc = R2_Shear;
                if (frc < 0)
                {
                    if (Math.Abs(frc) > Math.Abs(comp_frc))
                        comp_frc = frc;
                }
                //frc = R3_Shear;
                //if (frc < 0)
                //{
                //    if (Math.Abs(frc) > Math.Abs(comp_frc))
                //        comp_frc = frc;
                //}
                //frc = M2_Bending;
                //if (frc < 0)
                //{
                //    if (Math.Abs(frc) > Math.Abs(comp_frc))
                //        comp_frc = frc;
                //}
                //frc = M3_Bending;
                //if (frc < 0)
                //{
                //    if (Math.Abs(frc) > Math.Abs(comp_frc))
                //        comp_frc = frc;
                //}

                return comp_frc;
            }
        }
        public double MaxTensileForce
        {
            get
            {
                double frc = 0.0;
                double tens_frc = 0.0;

                frc = R1_Axial;
                //frc = R2_Shear;
                if (frc > 0)
                {
                    if (Math.Abs(frc) > Math.Abs(tens_frc))
                        tens_frc = frc;
                }
                //frc = R3_Shear;
                //if (frc > 0)
                //{
                //    if (Math.Abs(frc) > Math.Abs(tens_frc))
                //        tens_frc = frc;
                //}
                //frc = M2_Bending;
                //if (frc > 0)
                //{
                //    if (Math.Abs(frc) > Math.Abs(tens_frc))
                //        tens_frc = frc;
                //}
                //frc = M3_Bending;
                //if (frc > 0)
                //{
                //    if (Math.Abs(frc) > Math.Abs(tens_frc))
                //        tens_frc = frc;
                //}

                return tens_frc;
            }
        }

        public double MaxBendingMoment
        {
            get
            {
                if (Math.Abs(M2_Bending) > Math.Abs(M3_Bending)) return M2_Bending;
                return M3_Bending;
            }
        }
        public double MaxShearForce
        {
            get
            {
                if (Math.Abs(R2_Shear) > Math.Abs(R3_Shear)) return R2_Shear;
                return R3_Shear;
            }
        }
        public double MaxAxialForce
        {
            get
            {
                return R1_Axial;
            }
        }
        public double MaxTorsion
        {
            get
            {
                return M1_Torsion;
            }
        }
    }

    public class JointForce
    {
        public JointForce()
        {
            NodeNo = 0;
            LoadNo = 0;
            FX = 0.0;
            FY = 0.0;
            FZ = 0.0;
            MX = 0.0;
            MY = 0.0;
            MZ = 0.0;
        }

    //      NODE      LOAD         X-AXIS         Y-AXIS         Z-AXIS         X-AXIS         Y-AXIS         Z-AXIS
    //NUMBER      CASE          FORCE          FORCE          FORCE         MOMENT         MOMENT         MOMENT
        public int NodeNo { get; set; }
        public int LoadNo { get; set; }
        public double FX { get; set; }
        public double FY { get; set; }
        public double FZ { get; set; }
        public double MX { get; set; }
        public double MY { get; set; }
        public double MZ { get; set; }

    }

    public class JointDisplacement
    {
        public JointDisplacement()
        {
            JointNo = 0;
            LoadNo = 0;
            Tx = 0.0;
            Ty = 0.0;
            Tz = 0.0;
            Rx = 0.0;
            Ry = 0.0;
            Rz = 0.0;
        }

        public int JointNo { get; set; }
        public int LoadNo { get; set; }
        public double Tx { get; set; }
        public double Ty { get; set; }
        public double Tz { get; set; }
        public double Rx { get; set; }
        public double Ry { get; set; }
        public double Rz { get; set; }
    }
    #endregion Chiranjit [2011 09 26] Get this data from ASTRA Steel Truss Analysis

    #region Chiranjit [2011 09 26] Get this data from ASTRA Steel Truss Analysis

    #endregion Chiranjit [2011 09 26] Get this data from ASTRA Steel Truss Analysis


    #region Tables Data
    public class TableRolledSteelBeams
    {
        MyList mList = null;
        List<MyList> list = null;
        List<RolledSteelBeamsRow> list_record = null;
        string file_Name = "";
        /// <summary>
        /// Initialize Rolled Steel Beams Table
        /// </summary>
        /// <param name="tab_path">Table Folder Path</param>
        public TableRolledSteelBeams(string tab_path)
        {
            //tab_file = Path.Combine(tab_path, @"Steel Table\Rolled Steel Beams.txt");
            this.file_Name = Path.Combine(tab_path, @"Steel Table\Rolled Steel Beams.txt");
            list = new List<MyList>();
            SetRecords();
        }
        void SetRecords()
        {
            List<string> file_content = new List<string>(File.ReadAllLines(file_Name));
            list_record = new List<RolledSteelBeamsRow>();
            list = new List<MyList>();
            for (int i = 0; i < file_content.Count; i++)
            {
                mList = new MyList(MyList.RemoveAllSpaces(file_content[i].TrimEnd().TrimStart()), ' ');
                try
                {
                    list_record.Add(RolledSteelBeamsRow.Parse(file_content[i]));
                    list.Add(mList);
                }
                catch (Exception ex) { }
            }

        }
        public List<MyList> List
        {
            get
            {
                return list;
            }
        }
        public List<RolledSteelBeamsRow> List_Table
        {
            get
            {
                return list_record;
            }
        }

        public RolledSteelBeamsRow GetDataFromTable(string section_name, string section_code)
        {
            for (int i = 0; i < list_record.Count; i++)
            {
                if (list_record[i].SectionName == section_name && list_record[i].SectionCode == section_code)
                    return list_record[i];
            }
            return null;
        }


    }
    public class RolledSteelBeamsRow
    {
        public RolledSteelBeamsRow()
        {
            SectionName = "";
            SectionCode = "";
            Weight = 0.0;
            Area = 0.0;
            Depth = 0.0;
            FlangeWidth = 0.0;
            FlangeThickness = 0.0;
            WebThickness = 0.0;
            Ixx = 0.0;
            Iyy = 0.0;
            Rxx = 0.0;
            Ryy = 0.0;
            Zxx = 0.0;
            Zyy = 0.0;

        }

        public string SectionName { get; set; } //1
        public string SectionCode { get; set; }//2
        public double Weight { get; set; }//3
        public double Area { get; set; }//4
        public double Depth { get; set; }//5
        public double FlangeWidth { get; set; }//7
        public double FlangeThickness { get; set; }//8
        public double WebThickness { get; set; }//9
        public double Ixx { get; set; } //10
        public double Iyy { get; set; }//11
        public double Rxx { get; set; }//12
        public double Ryy { get; set; }//13
        public double Zxx { get; set; }//14
        public double Zyy { get; set; }//15



        public MyList Data { get; set; }
        public static RolledSteelBeamsRow Parse(string s)
        {

            string kStr = MyList.RemoveAllSpaces(s.ToUpper());

            kStr = kStr.Replace(',', ' ');
            kStr = MyList.RemoveAllSpaces(kStr.ToUpper());

            MyList mlist = new MyList(kStr, ' ');

            RolledSteelBeamsRow td = new RolledSteelBeamsRow();
            int index = 0;
            if (mlist.Count > 0)
            {
                td.SectionName = mlist.StringList[index]; index++;
                td.SectionCode = mlist.StringList[index]; index++;
                td.Weight = mlist.GetDouble(index); index++;
                td.Area = mlist.GetDouble(index); index++;
                td.Depth = mlist.GetDouble(index); index++;
                td.FlangeWidth = mlist.GetDouble(index); index++;
                td.FlangeThickness = mlist.GetDouble(index); index++;
                td.WebThickness = mlist.GetDouble(index); index++;
                td.Ixx = mlist.GetDouble(index); index++;
                td.Iyy = mlist.GetDouble(index); index++;
                td.Rxx = mlist.GetDouble(index); index++;
                td.Ryy = mlist.GetDouble(index); index++;
                td.Zxx = mlist.GetDouble(index); index++;
                td.Zyy = mlist.GetDouble(index); index++;
            }
            return td;

        }

    }

    public class TableRolledSteelChannels
    {
        MyList mList = null;
        List<MyList> list = null;
        List<RolledSteelChannelsRow> list_record = null;
        string file_Name = "";
        public TableRolledSteelChannels(string table_path)
        {
            //tab_file = Path.Combine(tab_path, @"Steel Table\Rolled Steel Channels.txt");
            this.file_Name = Path.Combine(table_path, @"Steel Table\Rolled Steel Channels.txt");
            list = new List<MyList>();
            SetRecords();
        }
        void SetRecords()
        {
            List<string> file_content = new List<string>(File.ReadAllLines(file_Name));
            list_record = new List<RolledSteelChannelsRow>();
            for (int i = 0; i < file_content.Count; i++)
            {
                mList = new MyList(MyList.RemoveAllSpaces(file_content[i].TrimEnd().TrimStart()), ' ');
                list.Add(mList);
                try
                {
                    list_record.Add(RolledSteelChannelsRow.Parse(file_content[i]));
                }
                catch (Exception ex) { }
            }

        }
        public List<MyList> List
        {
            get
            {
                return list;
            }
        }
        public List<RolledSteelChannelsRow> List_Table
        {
            get
            {
                return list_record;
            }
        }
        public List<string> Get_Channels()
        {
            List<string> Channels = new List<string>();

            for (int i = 0; i < List_Table.Count; i++)
            {
                string sec_name = List_Table[i].SectionName;
                if (!Channels.Contains(sec_name) && sec_name != "")
                {
                    Channels.Add(sec_name);
                }
            }
            return Channels;
        }
        public List<string> Get_SectionCodes(string section_name)
        {
            List<string> code1 = new List<string>();
            string sec_code, sec_name;
            for (int i = 0; i < List_Table.Count; i++)
            {
                sec_code = List_Table[i].SectionCode;
                sec_name = List_Table[i].SectionName;
                if (sec_name == section_name && sec_name != "")
                {
                    if (code1.Contains(sec_code) == false)
                    {
                        code1.Add(sec_code);
                    }
                }
            }
            return code1;
        }
        public RolledSteelChannelsRow GetDataFromTable(string section_name, string section_code)
        {
            for (int i = 0; i < list_record.Count; i++)
            {
                if (list_record[i].SectionName == section_name && list_record[i].SectionCode == section_code)
                    return list_record[i];
            }
            return null;
        }


    }
    public class RolledSteelChannelsRow
    {
        public RolledSteelChannelsRow()
        {
            SectionName = "";
            SectionCode = "";
            Weight = 0.0;
            Area = 0.0;
            Depth = 0.0;
            FlangeWidth = 0.0;
            FlangeThickness = 0.0;
            WebThickness = 0.0;
            CentreOfGravity = 0.0;
            Ixx = 0.0;
            Iyy = 0.0;
            Rxx = 0.0;
            Ryy = 0.0;
            Zxx = 0.0;
            Zyy = 0.0;
        }

        public string SectionName { get; set; } //1
        public string SectionCode { get; set; }//2
        public double Weight { get; set; }//3
        public double Area { get; set; }//4
        public double Depth { get; set; }//5
        public double FlangeWidth { get; set; }//7
        public double FlangeThickness { get; set; }//8
        public double WebThickness { get; set; }//9
        public double CentreOfGravity { get; set; }//9
        public double Ixx { get; set; } //10
        public double Iyy { get; set; }//11
        public double Rxx { get; set; }//12
        public double Ryy { get; set; }//13
        public double Zxx { get; set; }//14
        public double Zyy { get; set; }//15

        public MyList Data { get; set; }
        public static RolledSteelChannelsRow Parse(string s)
        {

            string kStr = MyList.RemoveAllSpaces(s.ToUpper());

            kStr = kStr.Replace(',', ' ');
            kStr = MyList.RemoveAllSpaces(kStr.ToUpper());

            MyList mlist = new MyList(kStr, ' ');

            RolledSteelChannelsRow td = new RolledSteelChannelsRow();
            int index = 0;
            if (mlist.Count >= 14)
            {
                td.SectionName = mlist.StringList[index]; index++;
                td.SectionCode = mlist.StringList[index]; index++;
                td.Weight = mlist.GetDouble(index); index++;
                td.Area = mlist.GetDouble(index); index++;
                td.Depth = mlist.GetDouble(index); index++;
                td.FlangeWidth = mlist.GetDouble(index); index++;
                td.FlangeThickness = mlist.GetDouble(index); index++;
                td.WebThickness = mlist.GetDouble(index); index++;
                td.CentreOfGravity = mlist.GetDouble(index); index++;
                td.Ixx = mlist.GetDouble(index); index++;
                td.Iyy = mlist.GetDouble(index); index++;
                td.Rxx = mlist.GetDouble(index); index++;
                td.Ryy = mlist.GetDouble(index); index++;
                td.Zxx = mlist.GetDouble(index); index++;
                td.Zyy = mlist.GetDouble(index); index++;
            }
            return td;

        }

    }


    public class TableRolledSteelAngles
    {
        MyList mList = null;
        List<MyList> list = null;
        List<RolledSteelAnglesRow> list_record = null;
        string equal_angle_file_name = "";
        string unequal_angle_file_name = "";
        public TableRolledSteelAngles(string file_path)
        {
            this.equal_angle_file_name = Path.Combine(file_path, @"Steel Table\Rolled Steel Equal Angles.txt");
            this.unequal_angle_file_name = Path.Combine(file_path, @"Steel Table\Rolled Steel Unequal Angles.txt");
            list = new List<MyList>();
            SetAngleTable(true);
            SetAngleTable(false);
        }
        void SetAngleTable(bool IsEqualAngles)
        {
            bool flag = false;
            List<string> file_content = null;

            if (IsEqualAngles) file_content = new List<string>(File.ReadAllLines(equal_angle_file_name));
            else file_content = new List<string>(File.ReadAllLines(unequal_angle_file_name));

            if (IsEqualAngles)
                list_record = new List<RolledSteelAnglesRow>();
            RolledSteelAnglesRow row = null;
            for (int i = 0; i < file_content.Count; i++)
            {
                mList = new MyList(MyList.RemoveAllSpaces(file_content[i].TrimEnd().TrimStart()), ' ');
                if (mList.StringList[0].StartsWith("ISA") || flag == true)
                {
                    flag = true;
                    list.Add(mList);
                    try
                    {
                        row = RolledSteelAnglesRow.Parse(file_content[i], IsEqualAngles);
                        if (row.SectionName == "")
                        {
                            row.SectionName = list_record[list_record.Count - 1].SectionName;
                            row.SectionCode = list_record[list_record.Count - 1].SectionCode;
                        }
                        list_record.Add(row);
                    }
                    catch (Exception ex) { }
                }

            }

        }

        public List<MyList> List
        {
            get
            {
                return list;
            }
        }
        public List<RolledSteelAnglesRow> List_Table
        {
            get
            {
                return list_record;
            }
        }

        public RolledSteelAnglesRow GetDataFromTable(string section_name, string section_code, double thickness)
        {
            for (int i = 0; i < list_record.Count; i++)
            {
                if (list_record[i].SectionName == section_name
                    && list_record[i].SectionCode == section_code
                    && list_record[i].Thickness == thickness)
                    return list_record[i];

                if (list_record[i].SectionName == section_name
                    && list_record[i].SectionSize == section_code
                     && list_record[i].Thickness == thickness)
                    return list_record[i];


            }
            return null;
        }


    }
    public class RolledSteelAnglesRow
    {
        public RolledSteelAnglesRow()
        {
            SectionName = "";
            SectionCode = "";

            Weight = 0.0;
            Area = 0.0;
            Ixx = 0.0;
            Iyy = 0.0;
            Cxx = 0.0;
            Cyy = 0.0;
            Exx = 0.0;
            Eyy = 0.0;

            /// doo
            double d;

        }
        public string SectionSize
        {
            get
            {
                string kStr = SectionCode;
                if (kStr.Length % 2 != 0)
                {
                    int i = kStr.Length / 2 + 1;
                    kStr = SectionCode.Substring(0, i) + "x" + SectionCode.Substring(i, SectionCode.Length - (i));
                }
                else
                {
                    int i = kStr.Length / 2;
                    kStr = SectionCode.Substring(0, i) + "x" + SectionCode.Substring(i, SectionCode.Length - (i));
                }
                return kStr;
            }
        }
        public string SectionName { get; set; }
        public string SectionCode { get; set; }
        public double Length_1 { get; set; }
        public double Length_2 { get; set; }
        public double Thickness { get; set; } //Weight per metre
        public double Weight { get; set; } //Weight per metre
        public double Area { get; set; } // Sectoinal Area

        public double Ixx { get; set; } //Moment of Inertia
        public double Iyy { get; set; } //Moment of Inertia
        public double Cxx { get; set; } //Centre of Gravity
        public double Cyy { get; set; } //Centre of Gravity
        public double Exx { get; set; } //Distance of Extreme Fibre
        public double Eyy { get; set; }

        public static RolledSteelAnglesRow Parse(string s, bool IsEqualAngle)
        {
            string kStr = MyList.RemoveAllSpaces(s.ToUpper());

            kStr = kStr.Replace(',', ' ');
            kStr = MyList.RemoveAllSpaces(kStr.ToUpper());

            MyList mlist = new MyList(kStr, ' ');

            RolledSteelAnglesRow td = new RolledSteelAnglesRow();
            int index = 0;
            if (mlist.Count >= 12)
            {
                if (mlist.StringList[0].StartsWith("ISA"))
                {
                    td.SectionName = mlist.StringList[index]; index++;
                    td.SectionCode = mlist.StringList[index]; index++;
                    //if (IsEqualAngle)
                    //{
                    index++; index++; index++;
                    //}
                }
                td.Thickness = mlist.GetDouble(index); index++;
                td.Area = mlist.GetDouble(index); index++;
                td.Weight = mlist.GetDouble(index); index++;
                td.Cxx = mlist.GetDouble(index);// index++;
                if (!IsEqualAngle) index++;
                td.Cyy = mlist.GetDouble(index); index++;
                td.Exx = mlist.GetDouble(index);// index++;
                if (!IsEqualAngle) index++;
                td.Eyy = mlist.GetDouble(index); index++;
                td.Ixx = mlist.GetDouble(index); // index++;
                if (!IsEqualAngle) index++;
                td.Iyy = mlist.GetDouble(index); index++;
            }
            return td;

        }

    }

    #endregion Tables Data


    public class AnalysisData
    {
        //eAstraMemberType mType;
        public AnalysisData()
        {
            UserMemberNo = -1;
            CompressionForce = 0.0;
            TensileForce = 0.0;
            AstraMemberNo = -1;
            LoadNo = -1;
            MaxAxialForce = 0.0;
            MaxTorsion = 0.0;
            MaxBendingMoment = 0.0;
            MaxShearForce = 0.0;
            AstraMemberType = eAstraMemberType.TRUSS;
            Result = "";

        }
        public int UserMemberNo { get; set; }
        public double CompressionForce { get; set; }
        public double TensileForce { get; set; }
        public int AstraMemberNo { get; set; }
        public int LoadNo { get; set; }
        public double MaxBendingMoment { get; set; }
        public double MaxShearForce { get; set; }
        public double MaxAxialForce { get; set; }
        public double MaxTorsion { get; set; }
        public eAstraMemberType AstraMemberType { get; set; }
        public string Result { get; set; }

    }

    public class CompleteDesign
    {
        public CompleteDesign()
        {
            Members = new MembersDesign();
            DeadLoads = new TotalDeadLoad();
            //TotalSteelWeight = 0.0;
            AddWeightPercent = 24.42;
            NoOfJointsAtTrussFloor = 0;
            NoOfJointsAtTrussFloor = 0;
            IsRailBridge = false;
            //NoOfEndJointsOnBothSideAtBottomChord = 0;
            //ForceEachInsideJoints = 0.0;
            //ForceEachEndJoint = 0.0;
            Is_Live_Load = Is_Super_Imposed_Dead_Load = Is_Dead_Load = false;

        }
        public MembersDesign Members { get; set; }
        public TotalDeadLoad DeadLoads { get; set; }
        public double TotalSteelWeight
        {
            get
            {
                return Members.Weight;
            }
        }
        public double AddWeightPercent { get; set; }
        public double TotalBridgeWeight
        {
            get
            {
                //return TotalSteelWeight + GussetAndLacingWeight + DeadLoads.Weight;



                //Chiranjit [2011 07 05]
                //Calculate Total Bridge weight
                double tot_wgt = 0;

                if (Is_Dead_Load)
                {
                    tot_wgt += TotalSteelWeight + GussetAndLacingWeight;
                }
                if (Is_Super_Imposed_Dead_Load)
                {
                    tot_wgt += DeadLoads.Weight;
                }
                return tot_wgt;
            }
        }
        public double GussetAndLacingWeight
        {
            get
            {
                return (TotalSteelWeight * AddWeightPercent / 100.0);
            }
        }
        //public int NoOfJointsAtTrussFloor { get; set; }
        public int NoOfInsideJointsOnBothSideAtBottomChord
        {
            get
            {
                return (NoOfJointsAtTrussFloor - NoOfEndJointsOnBothSideAtBottomChord);
            }
        }
        public int NoOfJointsAtTrussFloor { get; set; }
        public int NoOfEndJointsOnBothSideAtBottomChord
        {

            get
            {
                return 4;
            }
        }
        public double ForceEachInsideJoints
        {
            get
            {
                return (TotalBridgeWeight / (NoOfJointsAtTrussFloor - 2.0));
            }
        }
        public double ForceEachEndJoint
        {
            get
            {
                return ForceEachInsideJoints / 2.0;
            }

        }
        public bool IsRailBridge { get; set; }
        //Chiranjit[2011 07 05] 
        //For Defferent Loads like LL, SIDL, DL
        public bool Is_Live_Load { get; set; }
        public bool Is_Super_Imposed_Dead_Load { get; set; }
        public bool Is_Dead_Load { get; set; }

        public void ToStream(StreamWriter sw)
        {
            string kStr = "";
            try
            {
                //if(DeadLoads
                if (DeadLoads.Load_List.Count > 0 && DeadLoads.IsRailLoad == false)
                {
                    DeadLoads.ToStream(sw);
                }
                if (DeadLoads.IsRailLoad)
                {
                    DeadLoads.ToStream(sw);
                }
                sw.WriteLine();
                sw.WriteLine();
                Members.ToStream(sw);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Total Weight of Structural Steel  = {0:f3} kN = {1:f3} MTon",
                    TotalSteelWeight,
                    (TotalSteelWeight / 10.0),
                    (TotalSteelWeight / 0.00981));
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Adding {0}% for Gusset Plates, Lacing etc  = {1:f3} kN = {2:f3} MTon",
                    AddWeightPercent,
                    GussetAndLacingWeight,
                    (GussetAndLacingWeight / 10.0),
                    (GussetAndLacingWeight / 0.00981));
                sw.WriteLine();
                sw.WriteLine("Total Weight  = {0:f3} + {1:f3} = {2:f3} MTon",
                    (TotalSteelWeight / 10.0), (GussetAndLacingWeight / 10.0),
                    ((TotalSteelWeight / 10.0) + (GussetAndLacingWeight / 10.0)));

                sw.WriteLine();
                sw.WriteLine("Weight of Deck Slab + S.I.D.L  = {0:f3} kN = {1:f3} MTon",
                    DeadLoads.Weight,
                    (DeadLoads.Weight / 10.0),
                    (DeadLoads.Weight / 0.00981));
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Total Weight of Bridge = {0:f3} + {1:f3} + {2:f3} = {3:f3} kN = {4:f3} MTon",
                    TotalSteelWeight, GussetAndLacingWeight, DeadLoads.Weight, TotalBridgeWeight, (TotalBridgeWeight / 10.0), (TotalBridgeWeight / 0.00981));
                sw.WriteLine();
                sw.WriteLine("No of inside Joints on both sides of Truss at Bottom Chord = {0}", NoOfInsideJointsOnBothSideAtBottomChord);
                sw.WriteLine();
                sw.WriteLine("No of End Joints on both sides of Truss at Bottom Chord = 4");
                sw.WriteLine();
                sw.WriteLine("Force in each inside joints = {0:f3}/({1} + 2) = {2:f3} kN",
                    TotalBridgeWeight, NoOfInsideJointsOnBothSideAtBottomChord, ForceEachInsideJoints);
                sw.WriteLine("Force in each End joints = {0:f3}/2 = {1:f3} kN", ForceEachInsideJoints, ForceEachEndJoint);
            }
            catch (Exception ex) { }
        }
        public void WriteGroupSummery(string file_name)
        {
            StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));
            try
            {
                WriteGroupSummery(sw);
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }
        public void WriteGroupSummery(StreamWriter sw)
        {
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("==============================================================");
            sw.WriteLine("                        DESIGN SECTION SUMMARY");
            sw.WriteLine("==============================================================");
            sw.WriteLine();
            sw.WriteLine();
            string kStr = "";
            string format = "";

            //format = "{0,-18} {1,-25} {2,-15} {3,-15} {4,-15} {5,-20} {6,20:f2} {7,20:f2} {8,20:f2} {9,20:f2} {10,20:f2} {11,20:f2} {12,20:f2} {13,20:f2} {14,7}";
            format = "{0,-18} {1,-25} {2,-10} {3,-10} {4,-10} {5,-15} {6,15:f2} {7,15:f2} {8,15:f2} {9,15:f2} {10,10:f2} {11,10:f2} {12,15:f2} {13,15:f2}   {14,-7}";
            format = "{0,-9} {1,-20} {2,-10} {3,-10} {4,-10} {5,-10} {6,12:f2} {7,12:f2} {8,10:f2} {9,10:f2} {10,10:f2} {11,10:f2} {12,10:f2} {13,10:f2}   {14,-7}";
            format = "{0,-9} {1,-18} {2,-8} {3,-8} {4,-8} {5,-10} {6,12:f2} {7,12:f2} {8,10:f2} {9,10:f2} {10,10:f2} {11,10:f2} {12,10:e2} {13,10:e2}  {14,-7}";

            //sw.WriteLine(format, "Member Group", "Section", "Top Plate", "Side Plate", "Bottom Plate", "Vert. Stiff. Plate", "Req. Comp. Force", "Cap. Comp. Force", "Req. Tens. Force", "Cap. Tens. Force", "Req. Shear Stress", "Cap. Shear Stress", "Req. Sec. Modulus", "Cap. Sec. Modulus", "Remarks");

            sw.WriteLine(format, "      ", "      ", "     ", "     ", "      ", " Vertical", "  Member  ", " Capacity  ", " Member", "Capacity", " Member", "Capacity", "Member", "Required", "");
            sw.WriteLine(format, "Member", "      ", " Top ", "Side ", "Bottom", "Stiffener", "Compressive", "Compressive", "Tensile ", "Tensile ", " Shear  ", " Shear  ", "Section ", "Secion  ", "Remarks");
            sw.WriteLine(format, "Group", "Section", "Plate", "Plate", "Plate ", "  Plate  ", "  Force    ", "   Force   ", " Force  ", " Force  ", " Stress ", " Stress ", "Modulus ", "Modulus ", "       ");
            sw.WriteLine(format, "-----", "-------", "-----", "-----", "------", "---------", "-----------", "-----------", "--------", "--------", "--------", "--------", "--------", "--------", "-------");

            //sw.WriteLine(format, "      ", "      ", "     ", "     ", "      ", " Vertical", " Required  ", " Capacity  ", "Required", "Capacity", "Required", "Capacity", "Required", "Capacity", "");
            //sw.WriteLine(format, "Member", "      ", " Top ", "Side ", "Bottom", "Stiffener", "Compressive", "Compressive", "Tensile ", "Tensile ", " Shear  ", " Shear  ", "Section ", "Secion  ", "Remarks");
            //sw.WriteLine(format, "Group", "Section", "Plate", "Plate", "Plate ", "  Plate  ", "  Force    ", "   Force   ", " Force  ", " Force  ", " Stress ", " Stress ", "Modulus ", "Modulus ", "       ");
            //sw.WriteLine(format, "-----", "-------", "-----", "-----", "------", "---------", "-----------", "-----------", "--------", "--------", "--------", "--------", "--------", "--------", "-------");

            //sw.WriteLine();
            //sw.WriteLine();
            bool flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.BottomChord)
                {
                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("                        SUMMARY OF BOTTOM CHORDS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }
                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        item.MaxCompForce,
                        item.Capacity_CompForce,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.BottomChordBracings)
                {
                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("                        SUMMARY OF BOTTOM CHORD BRACINGS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }

                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        item.MaxCompForce,
                        item.Capacity_CompForce,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.CantileverBrackets)
                {
                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("                        SUMMARY OF CANTILEVER BRACKETS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }

                    kStr = string.Format(format,
                         item.Group.GroupName,
                         item.SectionDetails.ToString(),
                         item.SectionDetails.TopPlate.ToString(),
                         item.SectionDetails.SidePlate.ToString(),
                         item.SectionDetails.BottomPlate.ToString(),
                         item.SectionDetails.VerticalStiffenerPlate.ToString(),
                         item.MaxCompForce,
                         item.Capacity_CompForce,
                         item.MaxTensionForce,
                         item.Capacity_TensionForce,
                         item.Required_ShearStress,
                         item.Capacity_ShearStress,
                         item.Capacity_SectionModulus,
                         item.Required_SectionModulus,
                         item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }

            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.CrossGirder)
                {

                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("                        SUMMARY OF CROSS GIRDERS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }

                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        item.MaxCompForce,
                        item.Capacity_CompForce,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.DiagonalMember)
                {

                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("                        SUMMARY OF DIAGONAL MEMBERS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }
                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        item.MaxCompForce,
                        item.Capacity_CompForce,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.EndRakers)
                {

                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("                        SUMMARY OF END RAKERS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }

                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        item.MaxCompForce,
                        item.Capacity_CompForce,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.StringerBeam)
                {

                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("                        SUMMARY OF STRINGER BEAMS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }

                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        item.MaxCompForce,
                        item.Capacity_CompForce,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.TopChord)
                {


                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("                        SUMMARY OF TOP CHORDS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }
                    kStr = string.Format(format,
                         item.Group.GroupName,
                         item.SectionDetails.ToString(),
                         item.SectionDetails.TopPlate.ToString(),
                         item.SectionDetails.SidePlate.ToString(),
                         item.SectionDetails.BottomPlate.ToString(),
                         item.SectionDetails.VerticalStiffenerPlate.ToString(),
                         item.MaxCompForce,
                         item.Capacity_CompForce,
                         item.MaxTensionForce,
                         item.Capacity_TensionForce,
                         item.Required_ShearStress,
                         item.Capacity_ShearStress,
                         item.Capacity_SectionModulus,
                         item.Required_SectionModulus,
                         item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.TopChordBracings)
                {


                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("                        SUMMARY OF TOP CHORD BRACINGS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }
                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        item.MaxCompForce,
                        item.Capacity_CompForce,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            flag = true;
            foreach (var item in Members)
            {
                if (item.MemberType == eMemberType.VerticalMember)
                {


                    if (flag)
                    {
                        sw.WriteLine("-----------------------------------------------------------------------------");
                        sw.WriteLine("                        SUMMARY OF VERTICAL MEMBERS");
                        sw.WriteLine("-----------------------------------------------------------------------------");

                        flag = false;
                    }
                    kStr = string.Format(format,
                        item.Group.GroupName,
                        item.SectionDetails.ToString(),
                        item.SectionDetails.TopPlate.ToString(),
                        item.SectionDetails.SidePlate.ToString(),
                        item.SectionDetails.BottomPlate.ToString(),
                        item.SectionDetails.VerticalStiffenerPlate.ToString(),
                        item.MaxCompForce,
                        item.Capacity_CompForce,
                        item.MaxTensionForce,
                        item.Capacity_TensionForce,
                        item.Required_ShearStress,
                        item.Capacity_ShearStress,
                        item.Capacity_SectionModulus,
                        item.Required_SectionModulus,
                        item.Result);
                    sw.WriteLine(kStr);
                    sw.WriteLine();
                }
            }
            sw.WriteLine();
            sw.WriteLine("-----------------------------------------------------------------------------");
            sw.WriteLine("                        SUMMARY OF WEIGHTS");
            sw.WriteLine("-----------------------------------------------------------------------------");
            sw.WriteLine();
            sw.WriteLine("Total Weight of Structural Steel = {0:f3} kN  = {1:f3} MTon", TotalSteelWeight, (TotalSteelWeight / 10.0));
            sw.WriteLine();
            sw.WriteLine("Adding {0}% for Gusset Plates, Lacing etc = {1:f3} kN  = {2:f3} MTon", AddWeightPercent, GussetAndLacingWeight, GussetAndLacingWeight / 10.0);
            sw.WriteLine();
            sw.WriteLine("Weight of Deck Slab + Super Imposed Dead Load = {0:f3} kN  = {1:f3} MTon", DeadLoads.Weight, DeadLoads.Weight / 10.0);
            sw.WriteLine();
            sw.WriteLine("Total Weight of Bridge = {0:f3} + {1:f3} + {2:f3} = {3:f3} kN  = {4:f3} MTon",
                TotalSteelWeight, GussetAndLacingWeight, DeadLoads.Weight, TotalBridgeWeight, TotalBridgeWeight / 10.0);
            sw.WriteLine();
            sw.WriteLine("No of inside Joints on both sides of Truss at Bottom Chord = {0}", NoOfInsideJointsOnBothSideAtBottomChord);
            sw.WriteLine();
            sw.WriteLine("No of End Joints on both sides of Truss at Bottom Chord = 4");
            sw.WriteLine();
            sw.WriteLine("Force in each inside joints = {0:f3}/({1} + 2) = {2:f3} kN",
                TotalBridgeWeight, NoOfInsideJointsOnBothSideAtBottomChord, ForceEachInsideJoints);
            sw.WriteLine("Force in each End joints = {0:f3}/2 = {1:f3} kN", ForceEachInsideJoints, ForceEachEndJoint);

            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("==============================================================");
            sw.WriteLine("                      END DESIGN SECTION SUMMARY");
            sw.WriteLine("==============================================================");
            sw.WriteLine();

        }
        public void WriteForcesSummery(string file_name)
        {
            StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));
            try
            {
                WriteForcesSummery(sw);
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
            }


        }
        public void WriteForcesSummery(StreamWriter sw)
        {
            //StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));
            try
            {
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("==============================================================");
                sw.WriteLine("                        DESIGN FORCE SUMMARY");
                sw.WriteLine("==============================================================");
                sw.WriteLine();
                sw.WriteLine();

                string DL_SIDL_LL = "";

                DL_SIDL_LL = Is_Dead_Load ? "DL " : "";
                DL_SIDL_LL += Is_Super_Imposed_Dead_Load ? Is_Dead_Load ? " + SIDL " : "SIDL" : "";
                DL_SIDL_LL += Is_Live_Load ? (Is_Dead_Load || Is_Super_Imposed_Dead_Load) ? " + LL " : "LL" : "";

                sw.WriteLine("Applied force and Check for member section for " + DL_SIDL_LL);
                sw.WriteLine();
                string kStr = "";
                string format = "";

                //format = "{0,-18} {1,-25} {2,-15} {3,-15} {4,-15} {5,-20} {6,20:f2} {7,20:f2} {8,20:f2} {9,20:f2} {10,20:f2} {11,20:f2} {12,20:f2} {13,20:f2} {14,7}";
                //format = "{0,-18} {1,-25} {2,-10} {3,-10} {4,-10} {5,-15} {6,15:f2} {7,15:f2} {8,15:f2} {9,15:f2} {10,10:f2} {11,10:f2} {12,15:f2} {13,15:f2}   {14,-7}";
                //format = "{0,-9} {1,-20} {2,-10} {3,-10} {4,-10} {5,-10} {6,12:f2} {7,12:f2} {8,10:f2} {9,10:f2} {10,10:f2} {11,10:f2} {12,10:f2} {13,10:f2}   {14,-7}";



                format = "{0,-10} {1,10:f3} {2,15:f3} {3,10:f3} {4,10:f3} {5,10:f3} {6,15:f3} {7,15:f3} {8,10:f3}";
                //      0             1                 2                  3                 4           5                6                   7              8
                //MemberGroup    TensionForce    CaompressionForce    TensileStress   CapTensileStress Result     CompressiveStress   CapCompressiveStress Result  
                sw.WriteLine(format,
                    "Member",
                    "Tension",
                    "Compression",
                    "Tensile",
                    "Capacity",
                    "",
                    "Compressive",
                    "Capacity",
                    "");
                sw.WriteLine(format,
                   "Group",
                   "Force",
                   "Force",
                   "Stress",
                   "Tensile",
                   "Result",
                   "Stress",
                   "Compression",
                   "Result");
                sw.WriteLine(format,
                     "",
                     "",
                     "",
                     "",
                     "Stress",
                     "",
                     "",
                     "Stress",
                     "");
                sw.WriteLine(format,
                                    "",
                                    " (kN)",
                                    " (kN)",
                                    " (N/sq.mm)",
                                    " (N/sq.mm)",
                                    "  ",
                                    " (N/sq.mm)",
                                    " (N/sq.mm)",
                                    "");

                sw.WriteLine();
                bool flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.BottomChord)
                    {
                        if (flag)
                        {
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF BOTTOM CHORDS");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                            item.Group.GroupName,
                            item.MaxTensionForce,
                            item.MaxCompForce,
                            item.Tensile_Stress,
                            item.Capacity_Tensile_Stress,
                            item.Result_Tensile_Stress,
                            item.Compressive_Stress,
                            item.Capacity_Compressive_Stress,
                            item.Result_Compressive_Stress);
                        sw.WriteLine(kStr);
                    }
                }
                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.BottomChordBracings)
                    {
                        if (flag)
                        {
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF BOTTOM CHORD BRACINGS");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                            flag = false;
                        }

                        kStr = string.Format(format,
                           item.Group.GroupName,
                           item.MaxTensionForce,
                           item.MaxCompForce,
                           item.Tensile_Stress,
                           item.Capacity_Tensile_Stress,
                           item.Result_Tensile_Stress,
                           item.Compressive_Stress,
                           item.Capacity_Compressive_Stress,
                           item.Result_Compressive_Stress);
                        sw.WriteLine(kStr);
                    }
                }
                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.CantileverBrackets)
                    {
                        if (flag)
                        {
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF CANTILEVER BRACKETS");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                            flag = false;
                        }

                        kStr = string.Format(format,
                           item.Group.GroupName,
                           item.MaxTensionForce,
                           item.MaxCompForce,
                           item.Tensile_Stress,
                           item.Capacity_Tensile_Stress,
                           item.Result_Tensile_Stress,
                           item.Compressive_Stress,
                           item.Capacity_Compressive_Stress,
                           item.Result_Compressive_Stress);
                        sw.WriteLine(kStr);
                    }
                }


                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.DiagonalMember)
                    {

                        if (flag)
                        {
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF DIAGONAL MEMBERS");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                           item.Group.GroupName,
                           item.MaxTensionForce,
                           item.MaxCompForce,
                           item.Tensile_Stress,
                           item.Capacity_Tensile_Stress,
                           item.Result_Tensile_Stress,
                           item.Compressive_Stress,
                           item.Capacity_Compressive_Stress,
                           item.Result_Compressive_Stress);
                        sw.WriteLine(kStr);
                    }
                }
                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.EndRakers)
                    {

                        if (flag)
                        {
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF END RAKERS");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                            flag = false;
                        }

                        kStr = string.Format(format,
                           item.Group.GroupName,
                           item.MaxTensionForce,
                           item.MaxCompForce,
                           item.Tensile_Stress,
                           item.Capacity_Tensile_Stress,
                           item.Result_Tensile_Stress,
                           item.Compressive_Stress,
                           item.Capacity_Compressive_Stress,
                           item.Result_Compressive_Stress);
                        sw.WriteLine(kStr);
                    }
                }

                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.TopChord)
                    {


                        if (flag)
                        {
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF TOP CHORDS");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                           item.Group.GroupName,
                           item.MaxTensionForce,
                           item.MaxCompForce,
                           item.Tensile_Stress,
                           item.Capacity_Tensile_Stress,
                           item.Result_Tensile_Stress,
                           item.Compressive_Stress,
                           item.Capacity_Compressive_Stress,
                           item.Result_Compressive_Stress);
                        sw.WriteLine(kStr);
                    }
                }
                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.TopChordBracings)
                    {


                        if (flag)
                        {
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF TOP CHORD BRACINGS");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                           item.Group.GroupName,
                           item.MaxTensionForce,
                           item.MaxCompForce,
                           item.Tensile_Stress,
                           item.Capacity_Tensile_Stress,
                           item.Result_Tensile_Stress,
                           item.Compressive_Stress,
                           item.Capacity_Compressive_Stress,
                           item.Result_Compressive_Stress);
                        sw.WriteLine(kStr);
                    }
                }
                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.VerticalMember)
                    {


                        if (flag)
                        {
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF VERTICAL MEMBERS");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                            flag = false;
                        }
                        kStr = string.Format(format,
                           item.Group.GroupName,
                           item.MaxTensionForce,
                           item.MaxCompForce,
                           item.Tensile_Stress,
                           item.Capacity_Tensile_Stress,
                           item.Result_Tensile_Stress,
                           item.Compressive_Stress,
                           item.Capacity_Compressive_Stress,
                           item.Result_Compressive_Stress);
                        sw.WriteLine(kStr);
                    }
                }
                format = "{0,-10} {1,10:f3} {2,10:f3} {3,15:e3} {4,15:e3} {5,10:f3} {6,10:f3} {7,10:f3} {8,10:f3}";

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("-----------------------------------------------------------------------------");
                sw.WriteLine("                                 BEAM MEMBERS  ");
                sw.WriteLine("-----------------------------------------------------------------------------");
                sw.WriteLine();

                sw.WriteLine(format,
                    "Member",
                    "Bending",
                    "Shear",
                    "Section",
                    "Capacity",
                    "",
                    "Shear",
                    "Capacity",
                    "");
                sw.WriteLine(format,
                   "Group",
                   "Moment",
                   "Force",
                   "Modulas",
                   "Section",
                   "Result",
                   "Stress",
                   "Shear",
                   "Result");
                sw.WriteLine(format,
                     "",
                     "",
                     "",
                     "",
                     "",
                     "",
                     "",
                     "Stress",
                     "");
                sw.WriteLine(format,
                                    "",
                                    " (kN-m)",
                                    " (kN)",
                                    " (cu.mm)",
                                    " (cu.mm)",
                                    "  ",
                                    " (N/sq.mm)",
                                    " (N/sq.mm)",
                                    "");
                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.StringerBeam)
                    {

                        if (flag)
                        {
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF STRINGER BEAMS");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                            flag = false;
                        }

                        kStr = string.Format(format,
                           item.Group.GroupName,
                           item.MaxMoment,
                           item.MaxShearForce,
                           item.Required_SectionModulus,
                           item.Capacity_SectionModulus,
                           item.Result_Section_Modulas,
                           item.Capacity_ShearStress,
                           item.Required_ShearStress,
                           item.Result_Shear_Stress);
                        sw.WriteLine(kStr);
                    }
                }

                flag = true;
                foreach (var item in Members)
                {
                    if (item.MemberType == eMemberType.CrossGirder)
                    {

                        if (flag)
                        {
                            sw.WriteLine("-----------------------------------------------------------------------------");
                            sw.WriteLine("                        SUMMARY OF CROSS GIRDERS");
                            sw.WriteLine("-----------------------------------------------------------------------------");

                            flag = false;
                        }

                        kStr = string.Format(format,
                          item.Group.GroupName,
                          item.MaxMoment,
                          item.MaxShearForce,
                          item.Required_SectionModulus,
                          item.Capacity_SectionModulus,
                          item.Result_Section_Modulas,
                          item.Capacity_ShearStress,
                          item.Required_ShearStress,
                          item.Result_Shear_Stress);
                        sw.WriteLine(kStr);
                    }
                }
                //sw.WriteLine();
                //sw.WriteLine("-----------------------------------------------------------------------------");
                //sw.WriteLine("                        SUMMARY OF WEIGHTS");
                //sw.WriteLine("-----------------------------------------------------------------------------");
                //sw.WriteLine();
                //sw.WriteLine("Total Weight of Structural Steel = {0:f3} kN  = {1:f3} MTon", TotalSteelWeight, (TotalSteelWeight / 10.0));
                //sw.WriteLine();
                //sw.WriteLine("Adding {0}% for Gusset Plates, Lacing etc = {1:f3} kN  = {2:f3} MTon", AddWeightPercent, GussetAndLacingWeight, GussetAndLacingWeight / 10.0);
                //sw.WriteLine();
                //sw.WriteLine("Weight of Deck Slab + Super Imposed Dead Load = {0:f3} kN  = {1:f3} MTon", DeadLoads.Weight, DeadLoads.Weight / 10.0);
                //sw.WriteLine();
                //sw.WriteLine("Total Weight of Bridge = {0:f3} + {1:f3} + {2:f3} = {3:f3} kN  = {4:f3} MTon",
                //    TotalSteelWeight, GussetAndLacingWeight, DeadLoads.Weight, TotalBridgeWeight, TotalBridgeWeight / 10.0);
                //sw.WriteLine();
                //sw.WriteLine("No of inside Joints on both sides of Truss at Bottom Chord = {0}", NoOfInsideJointsOnBothSideAtBottomChord);
                //sw.WriteLine();
                //sw.WriteLine("No of End Joints on both sides of Truss at Bottom Chord = 4");
                //sw.WriteLine();
                //sw.WriteLine("Force in each inside joints = {0:f3}/({1} + 2) = {2:f3} kN",
                //    TotalBridgeWeight, NoOfInsideJointsOnBothSideAtBottomChord, ForceEachInsideJoints);
                //sw.WriteLine("Force in each End joints = {0:f3}/2 = {1:f3} kN", ForceEachInsideJoints, ForceEachEndJoint);

                sw.WriteLine();
                sw.WriteLine("==============================================================");
                sw.WriteLine("                      END DESIGN FORCE SUMMARY");
                sw.WriteLine("==============================================================");
                sw.WriteLine();
            }
            catch (Exception ex) { }
            finally
            {
                //sw.Flush();
                //sw.Close();
            }


        }

        public void ReadFromFile(string file_name)
        {
            this.DeadLoads.ReadFromFile(file_name);
            this.Members.ReadFromFile(file_name);
        }
    }
    [Serializable]
    public class MembersDesign : IList<CMember>
    {
        List<CMember> list = null;

        public MembersDesign()
        {
            list = new List<CMember>();
        }

        #region IList<Member> Members

        public int IndexOf(string MemberNo)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Group.GroupName == MemberNo)
                {
                    return i;
                }
            }
            return -1;
        }
        public int IndexOf(CMember item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Group.GroupName == item.Group.GroupName)
                {
                    return i;
                }
            }
            return -1;
        }


        public void Insert(int index, CMember item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public CMember this[int index]
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

        #region ICollection<Member> Members

        public void Add(CMember item)
        {
            try
            {
                if (IndexOf(item) != -1) throw new Exception("Member Group. " + item.Group.GroupName + " already exist.");
                if (item.Group.GroupName == "") throw new Exception("Invalid Member Information");
                list.Add(item);
            }
            catch (Exception ex) { }
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(CMember item)
        {
            return (IndexOf(item) != -1);
        }

        public void CopyTo(CMember[] array, int arrayIndex)
        {
            CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(CMember item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<Member> Members

        public IEnumerator<CMember> GetEnumerator()
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

        public void ReadFromFile(string file_name)
        {
            List<string> file_con = new List<string>(File.ReadAllLines(file_name));
            string kStr = "";
            MyList mLIst = null;
            CMember mem = null;
            int index = 0;
            bool flag = false;
            bool flag2 = false;
            eMemberType memType = eMemberType.NoSelection;

            for (int i = 0; i < file_con.Count; i++)
            {
                kStr = file_con[i];

                kStr = MyList.RemoveAllSpaces(kStr.ToUpper());
                mLIst = new MyList(kStr, ' ');
                //1      Section1     ISJB 150                              1.0000                1.0000     1.0000    
                //                    T PL 500 x 22   500        22         0.8628     1         

                //2      Section1     ISJB 150                              1.0000                1.0000     1.0000    
                //                    T PL 500 x 22   500        22         0.8628     1         

                //3      Section4     ISA 2020 x 3                          1.0000                1.0000     1.0000    
                //                    T PL 350 x 25   350        25         0.6864     1         
                //                    S PL 420 x 16   420        16         0.5271     2         
                //                    VS PL 120 x 25  120        25         0.2353     2         
                //                    S PL 320 x 10   320        10         0.2510    

                try
                {
                    if (mLIst.StringList[0].StartsWith("MEMBER"))
                    {
                        flag2 = true;
                        continue;
                    }
                    if (!flag2) continue;
                    if (mLIst.Count == 2 || mLIst.Count == 3)
                    {
                        kStr = kStr.ToUpper();
                        if (kStr == "TOP CHORD")
                        {
                            memType = eMemberType.TopChord;
                        }
                        else if (kStr == "BOTTOM CHORD")
                        {
                            memType = eMemberType.BottomChord;
                        }
                        else if (kStr == "STRINGER BEAM")
                        {
                            memType = eMemberType.StringerBeam;
                        }
                        else if (kStr == "CROSS GIRDER")
                        {
                            memType = eMemberType.CrossGirder;
                        }
                        else if (kStr == "END RAKERS")
                        {
                            memType = eMemberType.EndRakers;
                        }
                        else if (kStr == "DIAGONAL MEMBER")
                        {
                            memType = eMemberType.DiagonalMember;
                        }
                        else if (kStr == "VERTICAL MEMBER")
                        {
                            memType = eMemberType.VerticalMember;
                        }
                        else if (kStr == "TOP CHORD BRACINGS")
                        {
                            memType = eMemberType.TopChordBracings;
                        }
                        else if (kStr == "BOTTOM CHORD BRACINGS")
                        {
                            memType = eMemberType.BottomChordBracings;
                        }
                        else if (kStr == "CANTILEVER BRACKETS")
                        {
                            memType = eMemberType.CantileverBrackets;
                        }
                    }
                    if (mLIst.Count == 14 && mLIst.StringList[2] == "ISA")
                    {
                        if (flag)
                        {
                            Add(mem);
                            flag = false;
                        }
                        mem = new CMember();
                        index = 0;
                        mem.Group.GroupName = mLIst.StringList[index]; index++;
                        mem.SectionDetails.DefineSection = GetDefineSection(mLIst.StringList[index]); index++;
                        mem.SectionDetails.SectionName = mLIst.StringList[index]; index++;
                        mem.SectionDetails.SectionCode = mLIst.StringList[index]; index++; index++;
                        mem.SectionDetails.AngleThickness = mLIst.GetDouble(index); index++;
                        mem.WeightPerMetre = mLIst.GetDouble(index); index++;
                        mem.SectionDetails.NoOfElements = mLIst.GetDouble(index); index++; index++;
                        mem.SectionDetails.LateralSpacing = mLIst.GetDouble(index); index++;
                        mem.SectionDetails.BoltDia = mLIst.GetDouble(index); index++;
                        mem.SectionDetails.NoOfBolts = mLIst.GetInt(index); index++;
                        mem.Length = mLIst.GetDouble(index); index++;
                        //mem.Weight = mLIst.GetDouble(index); index++;

                        mem.MemberType = memType;
                        flag = true;
                    }
                    if (mLIst.Count == 12 && kStr.Contains("SECTION"))
                    {
                        if (flag)
                        {
                            Add(mem);
                            flag = false;
                        }
                        mem = new CMember();
                        index = 0;
                        mem.Group.GroupName = mLIst.StringList[index]; index++;
                        mem.SectionDetails.DefineSection = GetDefineSection(mLIst.StringList[index]); index++;
                        mem.SectionDetails.SectionName = mLIst.StringList[index]; index++;
                        mem.SectionDetails.SectionCode = mLIst.StringList[index]; index++;
                        //mem.SectionDetails.AngleThickness = mLIst.GetDouble(index); index++;
                        mem.WeightPerMetre = mLIst.GetDouble(index); index++;
                        mem.SectionDetails.NoOfElements = mLIst.GetDouble(index); index++; index++;
                        mem.SectionDetails.LateralSpacing = mLIst.GetDouble(index); index++;
                        mem.SectionDetails.BoltDia = mLIst.GetDouble(index); index++;
                        mem.SectionDetails.NoOfBolts = mLIst.GetInt(index); index++;
                        mem.Length = mLIst.GetDouble(index); index++;
                        //mem.Weight = mLIst.GetDouble(index); index++;
                        mem.MemberType = memType;
                        flag = true;
                    }
                    else if (mLIst.Count == 11 && mLIst.StringList[2] != "ISA")
                    {
                        switch (mLIst.StringList[0].ToUpper())
                        {
                            case "T":
                            case "C":
                                mem.SectionDetails.TopPlate.Width = mLIst.GetDouble(2);
                                mem.SectionDetails.TopPlate.Thickness = mLIst.GetDouble(4);
                                mem.SectionDetails.TopPlate.Length = mLIst.GetDouble(9);
                                mem.SectionDetails.TopPlate.TotalPlates = 1;
                                break;
                            case "B":
                                mem.SectionDetails.BottomPlate.Width = mLIst.GetDouble(2);
                                mem.SectionDetails.BottomPlate.Thickness = mLIst.GetDouble(4);
                                mem.SectionDetails.BottomPlate.Length = mLIst.GetDouble(9);
                                mem.SectionDetails.BottomPlate.TotalPlates = 1;
                                break;
                            case "S":
                                mem.SectionDetails.SidePlate.Width = mLIst.GetDouble(2);
                                mem.SectionDetails.SidePlate.Thickness = mLIst.GetDouble(4);
                                mem.SectionDetails.SidePlate.Length = mLIst.GetDouble(9);
                                mem.SectionDetails.SidePlate.TotalPlates = 2;
                                break;
                            case "VS":
                                mem.SectionDetails.VerticalStiffenerPlate.Width = mLIst.GetDouble(2);
                                mem.SectionDetails.VerticalStiffenerPlate.Thickness = mLIst.GetDouble(4);
                                mem.SectionDetails.VerticalStiffenerPlate.Length = mLIst.GetDouble(9);
                                mem.SectionDetails.VerticalStiffenerPlate.TotalPlates = 2;
                                break;
                        }
                    }

                }
                catch (Exception ex) { }
            }
            if (flag)
            {
                Add(mem);
                //flag = false;
            }
        }
        public void ToStream(StreamWriter sw)
        {
            string title_line1, title_line2;
            //StreamWriter sw = new StreamWriter(new FileStream(file_name, FileMode.Create));
            try
            {
                title_line1 = string.Format("{0, -16} {1, -12} {2, -15} {3, -10} {4, -10} {5, -10} {6, -10} {7, -10} {8, 10} {9, 10} {10, 10} {11, 10} {12, 10}",
                    "Member", //0
                    "Section",//1
                    "",//2
                    "Plate",//3
                    "Plate",//4
                    "Unit",//5
                    "No Of",//6
                    "No Of",//7
                    "Lateral",//8
                    "Bolt",//9
                    "No Of",//10
                    "Member",//11
                    "Member");//12
                sw.WriteLine(title_line1);
                //str = string.Format("{0, -6} {1, -15} {2, -12} {3, -15} {4, -10} {5, -10} {6, -10} {7, -10} {8, -10} {9, -10}",
                title_line2 = string.Format("{0, -16} {1, -12} {2, -15} {3, -10} {4, -10} {5, -10} {6, -10} {7, -10} {8, 10} {9, 10} {10, 10} {11, 10} {12, 10}",
                    "Group", //0
                    "Type",//1
                    "Element",//2
                    "Width",//3
                    "Thickness",//4
                    "Weight",//5
                    "Elements",//6
                    "Members",//7
                    "Spacing",//8
                    "Diameter",//9
                    "Bolt",//10
                    "Length",//11
                    "Weight");//12
                sw.WriteLine(title_line2);

                title_line2 = string.Format("{0, -16} {1, -12} {2, -15} {3, -10} {4, -10} {5, -10} {6, -10} {7, -10} {8, 10} {9, 10} {10, 10} {11, 10} {12, 10}",
                   "", //0
                   "",//1
                   "",//2
                   " (mm) ",//3
                   " (mm) ",//4
                   "(kN/m)",//5
                   "",//6
                   "",//7
                   " (mm) ",//8
                   " (mm) ",//9
                   "",//10
                   " (m) ",//11
                   "(kN)");//12
                sw.WriteLine(title_line2);

                sw.WriteLine();

                int i = 0;
                double total_weight, weight;
                #region CROSS GIRDER
                weight = 0.0;
                total_weight = 0.0;
                bool print_flag = true;
                for (i = 0; i < list.Count; i++)
                {

                    if (list[i].MemberType == eMemberType.CrossGirder)
                    {
                        if (print_flag)
                        {
                            sw.WriteLine("CROSS GIRDER");
                            sw.WriteLine("------------");
                            print_flag = false;
                        }
                        list[i].ToStream(sw);
                        weight += list[i].Weight;
                        //weight += list[i].SectionDetails.TopPlate.Weight;
                        //weight += list[i].SectionDetails.SidePlate.Weight;
                        //weight += list[i].SectionDetails.BottomPlate.Weight;
                        //weight += list[i].SectionDetails.VerticalStiffenerPlate.Weight;
                    }
                }
                if (weight > 0.0)
                {
                    sw.WriteLine("{0,135}{1,20:f4}", "TOTAL WEIGHT : ", weight);
                    total_weight += weight;
                }
                #endregion CROSS GIRDER
                weight = 0.0;
                print_flag = true;

                #region STRINGER BEAM
                for (i = 0; i < list.Count; i++)
                {
                    if (list[i].MemberType == eMemberType.StringerBeam)
                    {
                        if (print_flag)
                        {
                            sw.WriteLine("STRINGER BEAM");
                            sw.WriteLine("-------------");
                            print_flag = false;
                        }
                        list[i].ToStream(sw);
                        weight += list[i].Weight;
                        //weight += list[i].SectionDetails.TopPlate.Weight;
                        //weight += list[i].SectionDetails.SidePlate.Weight;
                        //weight += list[i].SectionDetails.BottomPlate.Weight;
                        //weight += list[i].SectionDetails.VerticalStiffenerPlate.Weight;
                    }
                }
                if (weight > 0.0)
                {
                    sw.WriteLine("{0,135}{1,20:f4}", "TOTAL WEIGHT : ", weight);
                    total_weight += weight;
                }
                #endregion STRINGER BEAM
                weight = 0.0;
                print_flag = true;

                #region BOTTOM CHORD
                for (i = 0; i < list.Count; i++)
                {
                    if (list[i].MemberType == eMemberType.BottomChord)
                    {
                        if (print_flag)
                        {
                            sw.WriteLine("BOTTOM CHORD");
                            sw.WriteLine("------------");
                            print_flag = false;
                        }
                        list[i].ToStream(sw);
                        weight += list[i].Weight;
                        //weight += list[i].SectionDetails.TopPlate.Weight;
                        //weight += list[i].SectionDetails.SidePlate.Weight;
                        //weight += list[i].SectionDetails.BottomPlate.Weight;
                        //weight += list[i].SectionDetails.VerticalStiffenerPlate.Weight;
                    }
                }
                if (weight > 0.0)
                {
                    sw.WriteLine("{0,135}{1,20:f4}", "TOTAL WEIGHT : ", weight);
                    total_weight += weight;
                }
                #endregion BOTTOM CHORD
                weight = 0.0;
                print_flag = true;

                #region TOP CHORD
                for (i = 0; i < list.Count; i++)
                {
                    if (list[i].MemberType == eMemberType.TopChord)
                    {
                        if (print_flag)
                        {
                            sw.WriteLine("TOP CHORD");
                            sw.WriteLine("---------");
                            print_flag = false;
                        }
                        list[i].ToStream(sw);
                        weight += list[i].Weight;
                        //weight += list[i].SectionDetails.TopPlate.Weight;
                        //weight += list[i].SectionDetails.SidePlate.Weight;
                        //weight += list[i].SectionDetails.BottomPlate.Weight;
                        //weight += list[i].SectionDetails.VerticalStiffenerPlate.Weight;
                    }
                }
                if (weight > 0.0)
                {
                    sw.WriteLine("{0,135}{1,20:f4}", "TOTAL WEIGHT : ", weight);
                    total_weight += weight;
                }
                #endregion TOP CHORD
                weight = 0.0;
                print_flag = true;

                #region END RAKERS
                for (i = 0; i < list.Count; i++)
                {
                    if (list[i].MemberType == eMemberType.EndRakers)
                    {
                        if (print_flag)
                        {
                            sw.WriteLine("END RAKERS");
                            sw.WriteLine("----------");
                            print_flag = false;
                        }
                        list[i].ToStream(sw);
                        weight += list[i].Weight;
                        //weight += list[i].SectionDetails.TopPlate.Weight;
                        //weight += list[i].SectionDetails.SidePlate.Weight;
                        //weight += list[i].SectionDetails.BottomPlate.Weight;
                        //weight += list[i].SectionDetails.VerticalStiffenerPlate.Weight;
                    }
                }
                if (weight > 0.0)
                {
                    sw.WriteLine("{0,135}{1,20:f4}", "TOTAL WEIGHT : ", weight);
                    total_weight += weight;
                }
                #endregion END RAKERS
                weight = 0.0;
                print_flag = true;

                #region DIAGONAL MEMBER
                for (i = 0; i < list.Count; i++)
                {
                    if (list[i].MemberType == eMemberType.DiagonalMember)
                    {
                        if (print_flag)
                        {
                            sw.WriteLine("DIAGONAL MEMBER");
                            sw.WriteLine("---------------");
                            print_flag = false;
                        }
                        list[i].ToStream(sw);
                        weight += list[i].Weight;
                        //weight += list[i].SectionDetails.TopPlate.Weight;
                        //weight += list[i].SectionDetails.SidePlate.Weight;
                        //weight += list[i].SectionDetails.BottomPlate.Weight;
                        //weight += list[i].SectionDetails.VerticalStiffenerPlate.Weight;
                    }
                }
                if (weight > 0.0)
                {
                    sw.WriteLine("{0,135}{1,20:f4}", "TOTAL WEIGHT : ", weight);
                    total_weight += weight;
                }
                #endregion DIAGONAL MEMBER
                weight = 0.0;
                print_flag = true;

                #region VERTICAL MEMBER
                for (i = 0; i < list.Count; i++)
                {
                    if (list[i].MemberType == eMemberType.VerticalMember)
                    {
                        if (print_flag)
                        {
                            sw.WriteLine("VERTICAL MEMBER");
                            sw.WriteLine("------------------");
                            print_flag = false;
                        }
                        list[i].ToStream(sw);
                        weight += list[i].Weight;
                        //weight += list[i].SectionDetails.TopPlate.Weight;
                        //weight += list[i].SectionDetails.SidePlate.Weight;
                        //weight += list[i].SectionDetails.BottomPlate.Weight;
                        //weight += list[i].SectionDetails.VerticalStiffenerPlate.Weight;
                    }
                }
                if (weight > 0.0)
                {
                    sw.WriteLine("{0,135}{1,20:f4}", "TOTAL WEIGHT : ", weight);
                    total_weight += weight;
                }
                #endregion VERTICAL MEMBER
                weight = 0.0;
                print_flag = true;

                #region TOP CHORD BRACINGS
                for (i = 0; i < list.Count; i++)
                {
                    if (list[i].MemberType == eMemberType.TopChordBracings)
                    {
                        if (print_flag)
                        {
                            sw.WriteLine("TOP CHORD BRACINGS");
                            sw.WriteLine("------------------");
                            print_flag = false;
                        }
                        list[i].ToStream(sw);
                        weight += list[i].Weight;
                        //weight += list[i].SectionDetails.TopPlate.Weight;
                        //weight += list[i].SectionDetails.SidePlate.Weight;
                        //weight += list[i].SectionDetails.BottomPlate.Weight;
                        //weight += list[i].SectionDetails.VerticalStiffenerPlate.Weight;
                    }
                }
                if (weight > 0.0)
                {
                    sw.WriteLine("{0,135}{1,20:f4}", "TOTAL WEIGHT : ", weight);
                    total_weight += weight;
                }
                #endregion TOP CHORD BRACINGS
                weight = 0.0;
                print_flag = true;

                #region BOTTOM CHORD BRACINGS
                for (i = 0; i < list.Count; i++)
                {
                    if (list[i].MemberType == eMemberType.BottomChordBracings)
                    {
                        if (print_flag)
                        {
                            sw.WriteLine("BOTTOM CHORD BRACINGS");
                            sw.WriteLine("---------------------");
                            print_flag = false;
                        }
                        list[i].ToStream(sw);
                        weight += list[i].Weight;
                        //weight += list[i].SectionDetails.TopPlate.Weight;
                        //weight += list[i].SectionDetails.SidePlate.Weight;
                        //weight += list[i].SectionDetails.BottomPlate.Weight;
                        //weight += list[i].SectionDetails.VerticalStiffenerPlate.Weight;
                    }
                }
                if (weight > 0.0)
                {
                    sw.WriteLine("{0,135}{1,20:f4}", "TOTAL WEIGHT : ", weight);
                    total_weight += weight;
                }
                #endregion BOTTOM CHORD BRACINGS
                weight = 0.0;
                print_flag = true;

                #region CANTILEVER BRACKETS
                for (i = 0; i < list.Count; i++)
                {
                    if (list[i].MemberType == eMemberType.CantileverBrackets)
                    {
                        if (print_flag)
                        {
                            sw.WriteLine("CANTILEVER BRACKETS");
                            sw.WriteLine("-------------------");
                            print_flag = false;
                        }
                        list[i].ToStream(sw);
                        weight += list[i].Weight;
                        //weight += list[i].SectionDetails.TopPlate.Weight;
                        //weight += list[i].SectionDetails.SidePlate.Weight;
                        //weight += list[i].SectionDetails.BottomPlate.Weight;
                        //weight += list[i].SectionDetails.VerticalStiffenerPlate.Weight;
                    }
                }
                if (weight > 0.0)
                {
                    sw.WriteLine("{0,135}{1,20:f4}", "TOTAL WEIGHT : ", weight);
                    total_weight += weight;
                }
                #endregion CANTILEVER BRACKETS
            }
            catch (Exception ex) { }
            //finally
            //{
            //    sw.Flush();
            //    sw.Close();
            //}

        }

        public eDefineSection GetDefineSection(string s)
        {
            eDefineSection ds = eDefineSection.NoSelection;
            switch (s.ToUpper())
            {
                case "SECTION1":
                    ds = eDefineSection.Section1;
                    break;
                case "SECTION2":
                    ds = eDefineSection.Section2;
                    break;
                case "SECTION3":
                    ds = eDefineSection.Section3;
                    break;
                case "SECTION4":
                    ds = eDefineSection.Section4;
                    break;
                case "SECTION5":
                    ds = eDefineSection.Section5;
                    break;
                case "SECTION6":
                    ds = eDefineSection.Section6;
                    break;
                case "SECTION7":
                    ds = eDefineSection.Section7;
                    break;
                case "SECTION8":
                    ds = eDefineSection.Section8;
                    break;
                case "SECTION9":
                    ds = eDefineSection.Section9;
                    break;
                case "SECTION10":
                    ds = eDefineSection.Section10;
                    break;
                case "SECTION11":
                    ds = eDefineSection.Section11;
                    break;
                case "SECTION12":
                    ds = eDefineSection.Section12;
                    break;
                case "SECTION13":
                    ds = eDefineSection.Section13;
                    break;
                case "SECTION14":
                    ds = eDefineSection.Section14;
                    break;
                case "SECTION15":
                    ds = eDefineSection.Section15;
                    break;
            }
            return ds;
        }
        public eMemberType GetMemberType(string s)
        {
            eMemberType ds = eMemberType.NoSelection;
            if (s.ToUpper() == eMemberType.BottomChord.ToString().ToUpper())
                ds = eMemberType.BottomChord;
            else if (s.ToUpper() == eMemberType.BottomChordBracings.ToString().ToUpper())
                ds = eMemberType.BottomChordBracings;
            else if (s.ToUpper() == eMemberType.CantileverBrackets.ToString().ToUpper())
                ds = eMemberType.CantileverBrackets;
            else if (s.ToUpper() == eMemberType.CrossGirder.ToString().ToUpper())
                ds = eMemberType.CrossGirder;
            else if (s.ToUpper() == eMemberType.DiagonalMember.ToString().ToUpper())
                ds = eMemberType.DiagonalMember;
            else if (s.ToUpper() == eMemberType.EndRakers.ToString().ToUpper())
                ds = eMemberType.EndRakers;
            else if (s.ToUpper() == eMemberType.StringerBeam.ToString().ToUpper())
                ds = eMemberType.StringerBeam;
            else if (s.ToUpper() == eMemberType.TopChord.ToString().ToUpper())
                ds = eMemberType.TopChord;
            else if (s.ToUpper() == eMemberType.TopChordBracings.ToString().ToUpper())
                ds = eMemberType.TopChordBracings;
            else if (s.ToUpper() == eMemberType.VerticalMember.ToString().ToUpper())
                ds = eMemberType.VerticalMember;

            return ds;

        }
        //public void Serialise(string file_name)
        //{
        //    Stream stream = new FileStream(file_name, FileMode.Create);
        //    BinaryFormatter bfor = new BinaryFormatter();
        //    bfor.Serialize(stream, this);
        //}
        //public static MembersDesign DeSerialise(string file_name)
        //{
        //    MembersDesign cmp_des = null;

        //    Stream stream = new FileStream(file_name, FileMode.Open);
        //    try
        //    {
        //        BinaryFormatter bfor = new BinaryFormatter();
        //        cmp_des = (MembersDesign)bfor.Deserialize(stream);
        //    }
        //    catch (Exception ex) { }
        //    finally
        //    {
        //        stream.Close();
        //    }
        //    return cmp_des;
        //}

        public double Weight
        {
            get
            {
                double wgt = 0.0;
                for (int i = 0; i < list.Count; i++)
                {
                    wgt += list[i].Weight;
                }
                return wgt;
            }
        }

    }
    [Serializable]
    public class CMember
    {
        public CMember()
        {

            Group = new kMemberGroup();
            MemberType = eMemberType.NoSelection;
            SectionDetails = new SectionData();
            Length = 0.0;
            //Weight = 0.0;
            WeightPerMetre = 0.0;
            Force = "";
            Result = "";

            Capacity_CompForce = 0.0;
            Capacity_TensionForce = 0.0;
            Capacity_SectionModulus = 0.0;
            Required_SectionModulus = 0.0;
            Required_ShearStress = 0.0;
            Capacity_ShearStress = 0.0;

            Tensile_Stress = 0.0;
            Capacity_Tensile_Stress = 0.0;
            Compressive_Stress = 0.0;
            Capacity_Compressive_Stress = 0.0;
        }
        public kMemberGroup Group { get; set; }
        //public string GroupName { get; set; }
        public eMemberType MemberType { get; set; }
        public SectionData SectionDetails { get; set; }
        public double Length { get; set; }
        public double Weight
        {
            get
            {
                //Chiranjit [2011 05 27]
                return NoOfMember * (WeightPerMetre * SectionDetails.NoOfElements * Length + SectionDetails.TopPlate.Weight + SectionDetails.BottomPlate.Weight + SectionDetails.SidePlate.Weight + SectionDetails.VerticalStiffenerPlate.Weight);
                //return NoOfMember * WeightPerMetre * SectionDetails.NoOfElements * Length + SectionDetails.TopPlate.Weight + SectionDetails.BottomPlate.Weight + SectionDetails.SidePlate.Weight + SectionDetails.VerticalStiffenerPlate.Weight;
            }
        }
        public double WeightPerMetre { get; set; }
        public void ToStream(StreamWriter sw)
        {
            string str = "";

            if (SectionDetails.DefineSection == eDefineSection.Section3 ||
                SectionDetails.DefineSection == eDefineSection.Section4 ||
                 SectionDetails.DefineSection == eDefineSection.Section7 ||
                 SectionDetails.DefineSection == eDefineSection.Section8 ||
                 SectionDetails.DefineSection == eDefineSection.Section9 ||
                 SectionDetails.DefineSection == eDefineSection.Section11 ||
                 SectionDetails.DefineSection == eDefineSection.Section12 ||
                 SectionDetails.DefineSection == eDefineSection.Section13 ||
                 SectionDetails.DefineSection == eDefineSection.Section14)
            {

                //str = string.Format("{0, -6} {1, -15} {2, -12} {3, -15} {4, -10} {5, -10} {6, -10:f4} {7, -10:f4} {8,10:f4} {9, -10:f4}",
                str = string.Format("{0,-16} {1,-12} {2,-15} {3,-10} {4,-10} {5,-10:f4} {6,-10} {7,-10:f0} {8,10:f4} {9,10:f4} {10,10:f0} {11,10:f4} {12,10:f4}",
                    Group.GroupName,//0
                    SectionDetails.DefineSection.ToString(),//1
                    SectionDetails.SectionName + " " + SectionDetails.SectionCode + " x " + SectionDetails.AngleThickness,//2
                    " ",//3
                    " ",//4
                    WeightPerMetre,//5
                    SectionDetails.NoOfElements,//6
                    Group.MemberNos.Count,//7
                    SectionDetails.LateralSpacing,//8
                    SectionDetails.BoltDia,//9
                    SectionDetails.NoOfBolts,//10
                    Length,//11
                    Weight);//12
            }
            else if (SectionDetails.DefineSection == eDefineSection.Section1 ||
                 SectionDetails.DefineSection == eDefineSection.Section2 ||
                 SectionDetails.DefineSection == eDefineSection.Section10 ||
                SectionDetails.DefineSection == eDefineSection.Section5 ||
                 SectionDetails.DefineSection == eDefineSection.Section6)
            {

                str = string.Format("{0,-16} {1,-12} {2,-15} {3,-10} {4,-10} {5,-10:f4} {6,-10} {7,-10:f0} {8,10:f4} {9,10:f4} {10,10:f0} {11,10:f4} {12,10:f4}",
                    Group.GroupName,//0
                    SectionDetails.DefineSection.ToString(),//1
                    SectionDetails.SectionName + " " + SectionDetails.SectionCode,//2
                    " ",//3
                    " ",//4
                    WeightPerMetre,//5
                    SectionDetails.NoOfElements,//6
                    Group.MemberNos.Count,//7
                    SectionDetails.LateralSpacing,//8
                    SectionDetails.BoltDia,//9
                    SectionDetails.NoOfBolts,//10
                    Length,//11
                    Weight);//12
            }
            sw.WriteLine(str);
            if (SectionDetails.TopPlate.Area > 0)
            {
                //str = string.Format("{0, -6} {1, -15} {2, -12} {3, -15} {4, -10} {5, -10} {6, -10:f4} {7, -10:f0}",
                str = string.Format("{0, -16} {1, -12} {2, -15} {3, -10} {4, -10} {5, -10:f4} {6, -10:f0} {7, -10:f0} {8,10:f4} {9,10:f0} {10,10:f4} {11,10:f4} {12,10:f4}",
                   "",
                    //"",
                   "",
                   (SectionDetails.DefineSection == eDefineSection.Section12) ? "C PL" : "T PL " + SectionDetails.TopPlate.Width + " x " + SectionDetails.TopPlate.Thickness,
                   SectionDetails.TopPlate.Width,
                   SectionDetails.TopPlate.Thickness,
                   SectionDetails.TopPlate.WeightPerMetre,
                   SectionDetails.TopPlate.TotalPlates,
                   "",
                   "",
                   "",
                   "",
                   SectionDetails.TopPlate.Length,
                   SectionDetails.TopPlate.Weight);
                sw.WriteLine(str);
            }
            if (SectionDetails.BottomPlate.Area > 0)
            {
                str = string.Format("{0, -16} {1, -12} {2, -15} {3, -10} {4, -10} {5, -10:f4} {6, -10:f0} {7, -10:f0} {8,10:f4} {9,10:f0} {10,10:f4} {11,10:f4} {12,10:f4}",
                   "",
                    //"",
                   "",
                   "B PL " + SectionDetails.BottomPlate.Width + " x " + SectionDetails.BottomPlate.Thickness,
                   SectionDetails.BottomPlate.Width,
                   SectionDetails.BottomPlate.Thickness,
                   SectionDetails.BottomPlate.WeightPerMetre,
                   SectionDetails.BottomPlate.TotalPlates,
                   "",
                   "",
                   "",
                   "",
                   SectionDetails.BottomPlate.Length,
                   SectionDetails.BottomPlate.Weight);
                sw.WriteLine(str);
            }
            if (SectionDetails.SidePlate.Area > 0)
            {
                str = string.Format("{0, -16} {1, -12} {2, -15} {3, -10} {4, -10} {5, -10:f4} {6, -10:f0} {7, -10:f0} {8,10:f4} {9,10:f0} {10,10:f4} {11,10:f4} {12,10:f4}",
                   "",
                    //"",
                   "",
                   "S PL " + SectionDetails.SidePlate.Width + " x " + SectionDetails.SidePlate.Thickness,
                   SectionDetails.SidePlate.Width,
                   SectionDetails.SidePlate.Thickness,
                   SectionDetails.SidePlate.WeightPerMetre,
                   SectionDetails.SidePlate.TotalPlates, "", "", "", "",
                   SectionDetails.SidePlate.Length,
                   SectionDetails.SidePlate.Weight);
                sw.WriteLine(str);
            }
            if (SectionDetails.VerticalStiffenerPlate.Area > 0)
            {
                str = string.Format("{0, -16} {1, -12} {2, -15} {3, -10} {4, -10} {5, -10:f4} {6, -10:f0} {7, -10:f0} {8,10:f4} {9,10:f0} {10,10:f4} {11,10:f4} {12,10:f4}",
                   "",
                    //"",
                   "",
                   "VS PL " + SectionDetails.VerticalStiffenerPlate.Width + " x " + SectionDetails.VerticalStiffenerPlate.Thickness,
                   SectionDetails.VerticalStiffenerPlate.Width,
                   SectionDetails.VerticalStiffenerPlate.Thickness,
                   SectionDetails.VerticalStiffenerPlate.WeightPerMetre,
                   SectionDetails.VerticalStiffenerPlate.TotalPlates, "", "", "", "",
                   SectionDetails.VerticalStiffenerPlate.Length,
                   SectionDetails.VerticalStiffenerPlate.Weight);
                sw.WriteLine(str);
            }
            sw.WriteLine();
        }
        public string Force { get; set; }
        public string Result { get; set; }
        public double MaxCompForce { get; set; }
        public double MaxTensionForce { get; set; }
        public double MaxShearForce { get; set; }
        public double MaxMoment { get; set; }

        public double Capacity_CompForce { get; set; }
        public double Capacity_TensionForce { get; set; }
        public double Capacity_ShearStress { get; set; }
        public double Required_ShearStress { get; set; }
        public double Capacity_SectionModulus { get; set; }
        public double Required_SectionModulus { get; set; }

        //Chiranjit [2011 07 01] for Tensile & Compressive stresses
        public double Tensile_Stress { get; set; }
        public double Capacity_Tensile_Stress { get; set; }
        public double Compressive_Stress { get; set; }
        public double Capacity_Compressive_Stress { get; set; }
        public string Result_Tensile_Stress
        {
            get
            {
                if (Capacity_Tensile_Stress > Tensile_Stress)
                    return "OK";
                else if (Capacity_Tensile_Stress < Tensile_Stress)
                    return "NOT OK";

                return "";
            }
        }
        public string Result_Compressive_Stress
        {
            get
            {
                if (Capacity_Compressive_Stress > Compressive_Stress)
                    return "OK";
                else if (Capacity_Compressive_Stress < Compressive_Stress)
                    return "NOT OK";
                return "";
            }
        }
        public string Result_Section_Modulas
        {
            get
            {
                if (Capacity_SectionModulus > Required_SectionModulus)
                    return "OK";
                else if (Capacity_SectionModulus < Required_SectionModulus)
                    return "NOT OK";

                return "";
            }
        }
        public string Result_Shear_Stress
        {
            get
            {
                if (Required_ShearStress > Capacity_ShearStress)
                    return "OK";
                else if (Required_ShearStress < Capacity_ShearStress)
                    return "NOT OK";

                return "";
            }
        }


        public int NoOfMember
        {
            get
            {
                return Group.MemberNos.Count;
            }
        }

    }
    [Serializable]
    public enum eMemberType
    {
        NoSelection = -1,
        StringerBeam = 0,
        CrossGirder = 1,
        BottomChord = 2,
        TopChord = 3,
        EndRakers = 4,
        DiagonalMember = 5,
        VerticalMember = 6,
        TopChordBracings = 7,
        BottomChordBracings = 8,
        CantileverBrackets = 9,
        AllMember = 10,
    }
    [Serializable]

    public enum eDefineSection
    {
        NoSelection = -1,
        Section1 = 0,
        Section2 = 1,
        Section3 = 2,
        Section4 = 3,
        Section5 = 4,
        Section6 = 5,
        Section7 = 6,
        Section8 = 7,
        Section9 = 8,
        Section10 = 9,
        Section11 = 10,
        Section12 = 11,
        Section13 = 12,
        Section14 = 13,
        Section15 = 14,
    }
    [Serializable]
    public class SectionData
    {
        public SectionData()
        {
            DefineSection = eDefineSection.NoSelection;
            SectionName = "";
            SectionCode = "";
            AngleThickness = 0.0;
            TopPlate = new Plate();
            BottomPlate = new Plate();
            SidePlate = new Plate();
            VerticalStiffenerPlate = new Plate();
            BoltDia = 20;
            LateralSpacing = 0.0;
            NoOfBolts = 2;
            //NoOfElements = 2;
        }
        public eDefineSection DefineSection { get; set; }
        public string SectionName { get; set; }
        public string SectionCode { get; set; }
        public double AngleThickness { get; set; }
        public Plate TopPlate { get; set; }
        public Plate BottomPlate { get; set; }
        public Plate SidePlate { get; set; }
        public Plate VerticalStiffenerPlate { get; set; }
        public double NoOfElements { get; set; }
        public double BoltDia { get; set; }
        public double LateralSpacing { get; set; }
        public int NoOfBolts { get; set; }

        public override string ToString()
        {
            string kStr = string.Format("{0} x {1} {2}", NoOfElements, SectionName, SectionCode);
            if (SectionName.Contains("ISA"))
            {
                kStr = string.Format("{0} x {1} {2}x{3}", NoOfElements, SectionName, SectionCode, AngleThickness);
            }
            return kStr;
        }

    }
    [Serializable]
    public class Plate
    {
        public Plate()
        {
            Width = 0.0;
            Thickness = 0.0;
            Length = 0.0;
            TotalPlates = 0;
        }
        public double Width { get; set; }
        public double Thickness { get; set; }
        public double Length { get; set; }
        public int TotalPlates { get; set; }

        public double Area
        {
            get
            {
                return Width * Thickness;
            }
        }

        public double Weight
        {
            get
            {
                return WeightPerMetre * Length * TotalPlates;
            }
        }
        public double WeightPerMetre
        {
            get
            {
                return Width * Thickness * 0.00007844d;
            }
        }
        public override string ToString()
        {
            if (Area == 0.0) return "";
            return string.Format("{0}x{1}", Width, Thickness);
        }
    }

    public class SuperInposedDeadLoad
    {
        public SuperInposedDeadLoad(string dead_load_name)
        {
            Name = dead_load_name;
            Length = 0.0;
            Breadth = 0.0;
            Depth = 1;
            TotalNo = 0;
            Gamma = 24.0;
        }
        public string Name { get; set; }
        public double Length { get; set; }
        public double Breadth { get; set; }
        public double Depth { get; set; }
        public int TotalNo { get; set; }
        public double Volume
        {
            get
            {
                if (Breadth == 0.0)
                {
                    return Length * TotalNo;
                }
                if (Depth == 0.0)
                {
                    return Length * Breadth * TotalNo;
                }
                return Length * Breadth * Depth * TotalNo;
            }
        }
        /// <summary>
        /// Unit Weight
        /// </summary>
        public double Gamma { get; set; }
        public double UnitWeight
        {
            get
            {
                return Gamma;
            }
            set
            {
                Gamma = value;
            }
        }
        public double Weight
        {
            get
            {
                return Volume * Gamma;
            }
        }
    }
    public class TotalDeadLoad
    {
        public TotalDeadLoad()
        {
            IsRailLoad = false;
            Load_List = new List<SuperInposedDeadLoad>();

            DeckSlab = new SuperInposedDeadLoad("DECK SLAB");
            Kerb = new SuperInposedDeadLoad("KERB");
            FootPathSlab = new SuperInposedDeadLoad("FOOTPATH SLAB");
            OuterBeam = new SuperInposedDeadLoad("OUTER BEAM");
            WearingCoat = new SuperInposedDeadLoad("WEARING COAT");
            Railing = new SuperInposedDeadLoad("RAILING");
            LiveLoadOnFootPath = new SuperInposedDeadLoad("LIVE LOAD FOOT PATH");
            Rail = new RailLoad();
            SetLoads();


        }
        public void SetLoads()
        {
            if (DeckSlab.Weight > 0)
                Load_List.Add(DeckSlab);
            if (Kerb.Weight > 0)
                Load_List.Add(Kerb);
            if (FootPathSlab.Weight > 0)
                Load_List.Add(FootPathSlab);
            if (OuterBeam.Weight > 0)
                Load_List.Add(OuterBeam);
            if (WearingCoat.Weight > 0)
                Load_List.Add(WearingCoat);
            if (Railing.Weight > 0)
                Load_List.Add(Railing);
            if (LiveLoadOnFootPath.Weight > 0)
                Load_List.Add(LiveLoadOnFootPath);
        }
        public List<SuperInposedDeadLoad> Load_List { get; set; }
        public RailLoad Rail { get; set; }
        public SuperInposedDeadLoad DeckSlab { get; set; }
        public SuperInposedDeadLoad Kerb { get; set; }
        public SuperInposedDeadLoad FootPathSlab { get; set; }
        public SuperInposedDeadLoad OuterBeam { get; set; }
        public SuperInposedDeadLoad WearingCoat { get; set; }
        public SuperInposedDeadLoad Railing { get; set; }
        public SuperInposedDeadLoad LiveLoadOnFootPath { get; set; }
        public SuperInposedDeadLoad RailPermanentLoadAsOpenFloor { get; set; }
        public SuperInposedDeadLoad RailBendingMoment { get; set; }
        public SuperInposedDeadLoad RailShearForce { get; set; }
        public bool IsRailLoad { get; set; }

        public double TotalWeight
        {
            get
            {
                if (IsRailLoad)
                    return RailPermanentLoadAsOpenFloor.Weight + RailShearForce.Weight + RailBendingMoment.Weight;

                return DeckSlab.Weight + Kerb.Weight + FootPathSlab.Weight + OuterBeam.Weight + WearingCoat.Weight + Railing.Weight + LiveLoadOnFootPath.Weight;
            }
        }

        public void ToStream(StreamWriter sw)
        {
            string kStr = "";
            try
            {
                if (IsRailLoad == false)
                {
                    sw.WriteLine("------------------------------------------------------------------------------------------------------");
                    kStr = string.Format("{0,-25} {1,10} {2,10} {3,10} {4,10} {5,10} {6,10} {7,10}",
                        "",
                        "",
                        "",
                        "",
                        "Total",
                        "",
                        "Unit",
                        "");
                    sw.WriteLine(kStr);
                    kStr = string.Format("{0,-25} {1,10} {2,10} {3,10} {4,10} {5,10} {6,10} {7,10}",
                        "Name",
                        "Length",
                        "Breadth",
                        "Depth",
                        "Nos",
                        "Volume",
                        "Weight",
                        "Weight");
                    sw.WriteLine(kStr);
                    kStr = string.Format("{0,-25} {1,10} {2,10} {3,10} {4,10} {5,10} {6,10} {7,10}",
                        "",
                        " (m)",
                        " (m)",
                        " (m)",
                        " Nos",
                        " (cu.m)",
                        " (kN/m) ",
                        "  (kN)");
                    sw.WriteLine(kStr);
                    sw.WriteLine("------------------------------------------------------------------------------------------------------");

                    double total_vol = 0.0;
                    double total_wgt = 0.0;
                    foreach (SuperInposedDeadLoad item in Load_List)
                    {
                        sw.WriteLine("{0,-25} {1,10:f3} {2,10:f3} {3,10:f3} {4,10} {5,10:f3} {6,10:f3} {7,10:f3}",
                        item.Name,
                        item.Length,
                        item.Breadth,
                        item.Depth,
                        item.TotalNo,
                        item.Volume,
                        item.Gamma,
                        item.Weight);

                        total_vol += item.Volume;
                        total_wgt += item.Weight;
                    }

                    sw.WriteLine("------------------------------------------------------------------------------------------------------");
                    sw.WriteLine("{0,-25} {1,10:f3} {2,10:f3} {3,10:f3} {4,10} {5,10:f3} {6,10:f3} {7,10:f3}",
                        "",
                        "",
                        "",
                        "",
                        "TOTAL",
                        total_vol,
                        "",
                        total_wgt);

                    sw.WriteLine("------------------------------------------------------------------------------------------------------");
                }
                else
                {
                    Rail.ToStream(sw);
                }
                sw.WriteLine();
                sw.WriteLine();
            }
            catch (Exception ex) { }
        }
        public void ToStream1(StreamWriter sw)
        {
            string kStr = "";
            try
            {
                if (IsRailLoad == false)
                {
                    sw.WriteLine("------------------------------------------------------------------------------------------------------");
                    kStr = string.Format("{0,-25} {1,10} {2,10} {3,10} {4,10} {5,10} {6,10} {7,10}",
                        "",
                        "",
                        "",
                        "",
                        "Total",
                        "",
                        "Unit",
                        "");
                    sw.WriteLine(kStr);
                    kStr = string.Format("{0,-25} {1,10} {2,10} {3,10} {4,10} {5,10} {6,10} {7,10}",
                        "Name",
                        "Length",
                        "Breadth",
                        "Depth",
                        "Nos",
                        "Volume",
                        "Weight",
                        "Weight");
                    sw.WriteLine(kStr);
                    sw.WriteLine("------------------------------------------------------------------------------------------------------");

                    sw.WriteLine("{0,-25} {1,10:f3} {2,10:f3} {3,10:f3} {4,10} {5,10:f3} {6,10:f3} {7,10:f3}",
                        DeckSlab.Name,
                        DeckSlab.Length,
                        DeckSlab.Breadth,
                        DeckSlab.Depth,
                        DeckSlab.TotalNo,
                        DeckSlab.Volume,
                        DeckSlab.Gamma,
                        DeckSlab.Weight);

                    sw.WriteLine("{0,-25} {1,10:f3} {2,10:f3} {3,10:f3} {4,10} {5,10:f3} {6,10:f3} {7,10:f3}",
                        Kerb.Name,
                        Kerb.Length,
                        Kerb.Breadth,
                        Kerb.Depth,
                        Kerb.TotalNo,
                        Kerb.Volume,
                        Kerb.Gamma,
                        Kerb.Weight);

                    sw.WriteLine("{0,-25} {1,10:f3} {2,10:f3} {3,10:f3} {4,10} {5,10:f3} {6,10:f3} {7,10:f3}",
                        FootPathSlab.Name,
                        FootPathSlab.Length,
                        FootPathSlab.Breadth,
                        FootPathSlab.Depth,
                        FootPathSlab.TotalNo,
                        FootPathSlab.Volume,
                        FootPathSlab.Gamma,
                        FootPathSlab.Weight);
                    sw.WriteLine("{0,-25} {1,10:f3} {2,10:f3} {3,10:f3} {4,10} {5,10:f3} {6,10:f3} {7,10:f3}",
                        OuterBeam.Name,
                        OuterBeam.Length,
                        OuterBeam.Breadth,
                        OuterBeam.Depth,
                        OuterBeam.TotalNo,
                        OuterBeam.Volume,
                        OuterBeam.Gamma,
                        OuterBeam.Weight);

                    double total_vol = DeckSlab.Volume + Kerb.Volume + FootPathSlab.Volume + OuterBeam.Volume;
                    double total_wgt = DeckSlab.Weight + Kerb.Weight + FootPathSlab.Weight + OuterBeam.Weight;
                    sw.WriteLine("------------------------------------------------------------------------------------------------------");
                    sw.WriteLine("{0,-25} {1,10:f3} {2,10:f3} {3,10:f3} {4,10} {5,10:f3} {6,10:f3} {7,10:f3}",
                        "",
                        "",
                        "",
                        "",
                        "TOTAL",
                        total_vol,
                        "",
                        total_wgt);

                    sw.WriteLine("------------------------------------------------------------------------------------------------------");
                    sw.WriteLine("{0,-25} {1,10:f3} {2,10:f3} {3,10:f3} {4,10} {5,10:f3} {6,10:f3} {7,10:f3}",
                        WearingCoat.Name,
                        WearingCoat.Length,
                        WearingCoat.Breadth,
                        "",
                        WearingCoat.TotalNo,
                        "",
                        WearingCoat.Gamma,
                        WearingCoat.Weight);
                    sw.WriteLine("{0,-25} {1,10:f3} {2,10:f3} {3,10:f3} {4,10} {5,10:f3} {6,10:f3} {7,10:f3}",
                        Railing.Name,
                        Railing.Length,
                        Railing.Breadth,
                        "",
                        Railing.TotalNo,
                        "",
                        Railing.Gamma,
                        Railing.Weight);
                    sw.WriteLine("{0,-25} {1,10:f3} {2,10:f3} {3,10:f3} {4,10} {5,10:f3} {6,10:f3} {7,10:f3}",
                        LiveLoadOnFootPath.Name,
                        LiveLoadOnFootPath.Length,
                        LiveLoadOnFootPath.Breadth,
                        "",
                        LiveLoadOnFootPath.TotalNo,
                        "",
                        LiveLoadOnFootPath.Gamma,
                        LiveLoadOnFootPath.Weight);

                    sw.WriteLine("------------------------------------------------------------------------------------------------------");
                    sw.WriteLine("{0,-25} {1,10:f3} {2,10:f3} {3,10:f3} {4,10} {5,10:f3} {6,10:f3} {7,10:f3}",
                       "",
                       "",
                       "",
                       "",
                       "TOTAL",
                       "WEIGHT",
                       "",
                       TotalWeight);
                    sw.WriteLine("------------------------------------------------------------------------------------------------------");
                }
                else
                {
                    Rail.ToStream(sw);
                }
                sw.WriteLine();
                sw.WriteLine();
            }
            catch (Exception ex) { }
        }
        public void ReadFromFile(string file_name)
        {
            List<string> file_content = new List<string>(File.ReadAllLines(file_name));
            try
            {
                string kStr = "";
                MyList mList = null;
                int index = 0;

                Load_List.Clear();
                foreach (string line in file_content)
                {
                    kStr = MyList.RemoveAllSpaces(line.ToUpper());
                    //Name                Length    Breadth      Depth  Total Nos     Volume Unit Weight  Weight
                    //DECK SLAB           61.000      3.750      0.230          2    105.225         24   2525.400
                    //KERB                61.000      0.230      0.510          2     14.311         24    343.454
                    //FOOTPATH SLAB       61.000      1.880      0.100          2     22.936         24    550.464
                    //OUTER BEAM          61.000      0.150      0.510          2      9.333         24    223.992
                    //WEARING COAT        61.000      3.750                     2                     2    915.000
                    //RAILING             61.000      0.000                     2                   1.6    195.200
                    //LIVE LOAD FOOT PATH 61.000      1.980                     2                  1.92    463.795
                    mList = new MyList(kStr, ' ');
                    if (mList.StringList[0].StartsWith("RAIL PER"))
                    {
                        mList = new MyList(kStr, '=');
                        kStr = MyList.RemoveAllSpaces(mList.StringList[1]);
                        mList = new MyList(kStr, ' ');
                        Rail.PermanentLoad = mList.GetDouble(0);
                        IsRailLoad = true;
                    }
                    if (kStr.StartsWith("EFFECTIVE SPAN"))
                    {
                        mList = new MyList(kStr, '=');
                        kStr = MyList.RemoveAllSpaces(mList.StringList[1]);
                        mList = new MyList(kStr, ' ');
                        Rail.EffectiveSpan = mList.GetDouble(0);
                        IsRailLoad = true;
                        continue;
                    }
                    if (kStr.Contains("BENDING MOMENT"))
                    {
                        mList = new MyList(kStr, '=');
                        kStr = MyList.RemoveAllSpaces(mList.StringList[1]);
                        mList = new MyList(kStr, ' ');
                        Rail.BendingMoment = mList.GetDouble(0);
                        IsRailLoad = true;
                        continue;
                    }
                    if (kStr.Contains("SHEAR FORCE"))
                    {
                        mList = new MyList(kStr, '=');
                        kStr = MyList.RemoveAllSpaces(mList.StringList[1]);
                        mList = new MyList(kStr, ' ');
                        Rail.ShearForce = mList.GetDouble(0);
                        IsRailLoad = true;
                        continue;
                    }
                    if (mList.StringList[0].StartsWith("DECK"))
                    {
                        index = 2;
                        DeckSlab.Length = mList.GetDouble(index); index++;
                        DeckSlab.Breadth = mList.GetDouble(index); index++;
                        DeckSlab.Depth = mList.GetDouble(index); index++;
                        DeckSlab.TotalNo = mList.GetInt(index); index++; index++;
                        DeckSlab.Gamma = mList.GetDouble(index); index++;
                        IsRailLoad = false;
                        continue;
                    }
                    if (mList.StringList[0].StartsWith("KERB"))
                    {
                        index = 1;
                        Kerb.Length = mList.GetDouble(index); index++;
                        Kerb.Breadth = mList.GetDouble(index); index++;
                        Kerb.Depth = mList.GetDouble(index); index++;
                        Kerb.TotalNo = mList.GetInt(index); index++; index++;
                        Kerb.Gamma = mList.GetDouble(index); index++;
                        IsRailLoad = false;
                        continue;
                    }
                    if (mList.StringList[0].StartsWith("FOOTPATH"))
                    {
                        index = 2;
                        FootPathSlab.Length = mList.GetDouble(index); index++;
                        FootPathSlab.Breadth = mList.GetDouble(index); index++;
                        FootPathSlab.Depth = mList.GetDouble(index); index++;
                        FootPathSlab.TotalNo = mList.GetInt(index); index++; index++;
                        FootPathSlab.Gamma = mList.GetDouble(index); index++;
                        IsRailLoad = false;
                        continue;
                    }
                    if (mList.StringList[0].StartsWith("OUTER"))
                    {
                        index = 2;
                        OuterBeam.Length = mList.GetDouble(index); index++;
                        OuterBeam.Breadth = mList.GetDouble(index); index++;
                        OuterBeam.Depth = mList.GetDouble(index); index++;
                        OuterBeam.TotalNo = mList.GetInt(index); index++; index++;
                        OuterBeam.Gamma = mList.GetDouble(index); index++;
                        IsRailLoad = false;
                        continue;
                    }
                    if (mList.StringList[0].StartsWith("WEARING"))
                    {
                        index = 2;
                        WearingCoat.Length = mList.GetDouble(index); index++;
                        WearingCoat.Breadth = mList.GetDouble(index); index++;
                        WearingCoat.Depth = mList.GetDouble(index); index++;
                        WearingCoat.TotalNo = mList.GetInt(index); index++; index++;
                        WearingCoat.Gamma = mList.GetDouble(index); index++;
                        IsRailLoad = false;
                        continue;
                    }
                    if (mList.StringList[0].StartsWith("RAILING"))
                    {
                        index = 1;
                        Railing.Length = mList.GetDouble(index); index++;
                        Railing.Breadth = mList.GetDouble(index); index++;
                        Railing.Depth = mList.GetDouble(index); index++;
                        Railing.TotalNo = mList.GetInt(index); index++; index++;
                        Railing.Gamma = mList.GetDouble(index); index++;
                        IsRailLoad = false;
                        continue;
                    }
                    if (mList.StringList[0].StartsWith("LIVE"))
                    {
                        index = 4;
                        LiveLoadOnFootPath.Length = mList.GetDouble(index); index++;
                        LiveLoadOnFootPath.Breadth = mList.GetDouble(index); index++;
                        LiveLoadOnFootPath.Depth = mList.GetDouble(index); index++;
                        LiveLoadOnFootPath.TotalNo = mList.GetInt(index); index++; index++;
                        LiveLoadOnFootPath.Gamma = mList.GetDouble(index); index++;
                        throw new Exception();
                        IsRailLoad = false;
                        continue;
                    }
                }
                //SetLoads();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                SetLoads();
                file_content.Clear();
                file_content = null;
            }

        }
        public double Weight
        {
            get
            {
                if (IsRailLoad)
                    return Rail.ShearForce + Rail.BendingMoment;
                double wgt = 0.0;

                for (int i = 0; i < Load_List.Count; i++)
                {
                    wgt += Load_List[i].Weight;
                }
                //return DeckSlab.Weight + Kerb.Weight + FootPathSlab.Weight + OuterBeam.Weight + WearingCoat.Weight + Railing.Weight + LiveLoadOnFootPath.Weight;
                return wgt;
            }
        }
    }
    public class RailLoad
    {
        public RailLoad()
        {
            EffectiveSpan = 0.0;
            PermanentLoad = 0.0;
            BendingMoment = 0.0;
            ShearForce = 0.0;
            //EffectiveSpan = 6.0;
            //PermanentLoad = 7.5;
            //BendingMoment = 2727.0;
            //ShearForce = 2927.0;
        }
        public double EffectiveSpan { get; set; }
        public double PermanentLoad { get; set; }
        public double BendingMoment { get; set; }
        public double ShearForce { get; set; }
        public double TotalLoad
        {
            get
            {
                return 0;
            }
        }
        public void ToStream(StreamWriter sw)
        {
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("Effective span of each stringer beam segment (c/c spacing of cross girder) = {0} m", EffectiveSpan);
            sw.WriteLine();
            sw.WriteLine("Rail Permanent Load as Open Floor = {0} kN/m", PermanentLoad);
            sw.WriteLine();
            sw.WriteLine("Equivalent Total moving load per track for Bending Moment Calculation = {0} kN", BendingMoment);
            sw.WriteLine();
            sw.WriteLine("Equivalent Total moving load per track for Shear force Calculation = {0} kN", ShearForce);
            sw.WriteLine();
        }
    }
    public static class MemberString
    {
        const string TopChord = "TOP CHORD";
        const string BottomChord = "BOTTOM CHORD";
        const string StringerBeam = "STRINGER BEAM";
        const string CrossGirder = "CROSS GIRDER";
        const string VerticalMember = "VERTICAL MEMBER";
        const string DiagonalMember = "DIAGONAL MEMBER";
        const string TopChordBracing = "TOP CHORD BRACING";
        const string BottomChordBracing = "BOTTOM CHORD BRACING";
        const string CantileverBracket = "CANTILEVER BRACKET";
        const string EndRakers = "END RAKERS";

        public const string Section1 = "SECTION-1, [FIG-1]";
        public const string Section2 = "SECTION-2, [FIG-2]";
        public const string Section3 = "SECTION-3, [FIG-3]";
        public const string Section4 = "SECTION-4, [FIG-4]";
        public const string Section5 = "SECTION-5, [FIG-5]";
        public const string Section6 = "SECTION-6, [FIG-6]";
        public const string Section7 = "SECTION-7, [FIG-7]";
        public const string Section8 = "SECTION-8, [FIG-8]";
        public const string Section9 = "SECTION-9, [FIG-9]";
        public const string Section10 = "SECTION-10, [FIG-10]";
        public const string Section11 = "SECTION-11, [FIG-11]";
        public const string Section12 = "SECTION-12, [FIG-12]";
        public const string Section13 = "SECTION-13, [FIG-13]";
        public const string Section14 = "SECTION-14, [FIG-14]";


        public static string GerMemberString(CMember mbr)
        {
            string str = "";
            switch (mbr.MemberType)
            {
                case eMemberType.BottomChord:
                    str = BottomChord;
                    break;
                case eMemberType.TopChord:
                    str = TopChord;
                    break;
                case eMemberType.CrossGirder:
                    str = CrossGirder;
                    break;
                case eMemberType.StringerBeam:
                    str = StringerBeam;
                    break;
                case eMemberType.BottomChordBracings:
                    str = BottomChordBracing;
                    break;
                case eMemberType.TopChordBracings:
                    str = TopChordBracing;
                    break;
                case eMemberType.CantileverBrackets:
                    str = CantileverBracket;
                    break;
                case eMemberType.DiagonalMember:
                    str = DiagonalMember;
                    break;
                case eMemberType.EndRakers:
                    str = EndRakers;
                    break;
                case eMemberType.VerticalMember:
                    str = VerticalMember;
                    break;
            }

            switch (mbr.SectionDetails.DefineSection)
            {
                case eDefineSection.Section1:
                    str = str + "    " + Section1;
                    break;
                case eDefineSection.Section2:
                    str = str + "    " + Section2;
                    break;
                case eDefineSection.Section3:
                    str = str + "    " + Section3;
                    break;
                case eDefineSection.Section4:
                    str = str + "    " + Section4;
                    break;
                case eDefineSection.Section5:
                    str = str + "    " + Section5;
                    break;
                case eDefineSection.Section6:
                    str = str + "    " + Section6;
                    break;
                case eDefineSection.Section7:
                    str = str + "    " + Section7;
                    break;
                case eDefineSection.Section8:
                    str = str + "    " + Section8;
                    break;
                case eDefineSection.Section9:
                    str = str + "    " + Section9;
                    break;
                case eDefineSection.Section10:
                    str = str + "    " + Section10;
                    break;
                case eDefineSection.Section11:
                    str = str + "    " + Section11;
                    break;
                case eDefineSection.Section12:
                    str = str + "    " + Section12;
                    break;
                case eDefineSection.Section13:
                    str = str + "    " + Section13;
                    break;
                case eDefineSection.Section14:
                    str = str + "    " + Section14;
                    break;
            }

            return str;
        }


    }
    public class CAnalysis
    {
        JointNodeCollection joints;
        kMemberCollection members;
        List<string> ASTRA_Data = null;
        public string MassUnit { get; set; }
        public string LengthUnit { get; set; }
        public CAnalysis(string analysis_file)
        {
            joints = new JointNodeCollection();
            members = new kMemberCollection();
            AnalysisFileName = analysis_file;
            MemberGroups = new kMemberGroupCollection();
            ReadDataFromFile();
            SetAstraStructure();
        }
        public string AnalysisFileName { get; set; }
        public kMemberGroupCollection MemberGroups { get; set; }

        void ReadDataFromFile()
        {
            List<string> file_con = new List<string>(File.ReadAllLines(AnalysisFileName));
            string kStr = "";
            try
            {
                MassUnit = "";
                LengthUnit = "";
                ASTRA_Data = new List<string>();
                bool flag = false;
                for (int i = 0; i < file_con.Count; i++)
                {

                    kStr = MyList.RemoveAllSpaces(file_con[i].ToUpper());

                    if (kStr.Contains("USER'S DATA") && kStr.Contains("END OF USER") == false)
                    {
                        flag = true; continue;

                    }
                    if (kStr.Contains("JOINT COO") && kStr.Contains("END OF USER") == false)
                    {
                        flag = true;

                    }
                    if (flag)
                    {
                        MyList ml = new MyList(MyList.RemoveAllSpaces(kStr), ' ');
                        if (ml.Count >= 3 && ml.StringList[0].Contains("UNIT"))
                        {
                            if (ml.StringList[2].StartsWith("T"))
                                MassUnit = "TON";
                            if (ml.StringList[2].StartsWith("MT"))
                                MassUnit = "TON";
                            if (ml.StringList[2].StartsWith("KN"))
                                MassUnit = "KN";

                            if (ml.StringList[2].StartsWith("ME"))
                                LengthUnit = "ME";
                            if (ml.StringList[2].StartsWith("KG"))
                                LengthUnit = "ME";

                            if (ml.StringList[1].StartsWith("T"))
                                MassUnit = "TON";
                            if (ml.StringList[1].StartsWith("MT"))
                                MassUnit = "TON";

                            if (ml.StringList[1].StartsWith("KN"))
                                MassUnit = "KN";

                            if (ml.StringList[1].StartsWith("ME"))
                                LengthUnit = "ME";
                            if (ml.StringList[1].StartsWith("KG"))
                                LengthUnit = "ME";

                        }
                        ASTRA_Data.Add(kStr);
                    }

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

        public kMemberCollection Members
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
            str = MyList.RemoveAllSpaces(str);
            MyList mList = new MyList(str, ' ');
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
                Clear();
                string kStr = "";
                bool flag = false;
                for (int i = 0; i < ASTRA_Data.Count; i++)
                {
                    kStr = ASTRA_Data[i].ToUpper();

                    kStr = MyList.RemoveAllSpaces(kStr);
                    if (kStr.Contains("JOINT C"))
                    {
                        flag = true; continue;
                    }
                    if (flag)
                    {
                        try
                        {

                            MyList ml = new MyList(kStr, ';');
                            foreach (var item in ml.StringList)
                            {
                                MyList ls = new MyList(item, ' ');
                                if (ls.Count == 4)
                                {
                                    list.Add(JointNode.Parse(item));
                                }
                                else if (ls.Count == 8)
                                {
                                    int sno = ls.GetInt(0);
                                    int eno = ls.GetInt(4);

                                    double dsx = ls.GetDouble(1);
                                    double dsy = ls.GetDouble(2);
                                    double dsz = ls.GetDouble(3);

                                    double dex = ls.GetDouble(5);
                                    double dey = ls.GetDouble(6);
                                    double dez = ls.GetDouble(7);

                                    //double dx = (dex - dsx) / (eno - sno + 1);
                                    //double dy = (dey - dsy) / (eno - sno + 1);
                                    //double dz = (dez - dsz) / (eno - sno + 1);

                                    double dx = (dex - dsx) / (eno - sno);
                                    double dy = (dey - dsy) / (eno - sno);
                                    double dz = (dez - dsz) / (eno - sno);

                                    list.Clear();

                                    for (int c = 0; c <= (eno - sno); c++)
                                    {
                                        JointNode jn = new JointNode();

                                        jn.NodeNo = sno + c;
                                        jn.X = dsx + c * dx;
                                        jn.Y = dsy + c * dy;
                                        jn.Z = dsz + c * dz;

                                        list.Add(jn);
                                    }


                                    //list.Add(JointNode.Parse(item));
                                }
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
            str = MyList.RemoveAllSpaces(s);

            MyList mList = new MyList(str, ' ');
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
    public class kMemberCollection
    {
        List<Member> list = null;
        public kMemberCollection()
        {
            list = new List<Member>();
        }
        //Chiranjit [2011 06 15]
        // Initialize Data from another object
        public kMemberCollection(kMemberCollection mem)
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
                string kStr = "";
                bool flag = false;
                list.Clear();

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

                            MyList ml = new MyList(kStr, ';');
                            foreach (var item in ml.StringList)
                            {
                                MyList ls = new MyList(MyList.RemoveAllSpaces(item), ' ');

                                if (ls.Count == 3)
                                    list.Add(Member.Parse(item));
                                else if (ls.Count == 4)
                                {
                                    int sno = ls.GetInt(0);
                                    int sno1 = ls.GetInt(1);
                                    int sno2 = ls.GetInt(2);
                                    int eno = ls.GetInt(3);

                                    for (int c = sno; c <= eno; c++)
                                    {
                                        Member mbb = new Member();
                                        mbb.MemberNo = c;
                                        mbb.StartNode.NodeNo = sno1++;
                                        mbb.EndNode.NodeNo = sno2++;
                                        list.Add(mbb);
                                    }
                                }

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
            string kStr = MyList.RemoveAllSpaces(mem_def);
            MyList mList = new MyList(kStr, ' ');

            Member mem = GetMember(mList.GetInt(0));

            if (mem != null) return mem.Length;

            return 0.0;
        }
    }

    public class kMemberGroup
    {
        string memText = "";
        public kMemberGroup()
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
        public static kMemberGroup Parse(string str)
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
            kStr = MyList.RemoveAllSpaces(kStr);

            MyList mList = new MyList(kStr, ' ');

            kMemberGroup mGrp = new kMemberGroup();

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

            string str = MyList.RemoveAllSpaces(memText);
            MyList mList = new MyList(str, ' ');

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





    #region   Memcoll
    /**/
    public class kMemberGroupCollection
    {
        Hashtable hash_ta;
        public List<kMemberGroup> GroupCollection { get; set; }
        public kMemberGroupCollection()
            : base()
        {
            hash_ta = new Hashtable();
            GroupCollection = new List<kMemberGroup>();
        }
        public void ReadFromFile(List<string> astra_data, kMemberCollection All_Members)
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
                kStr = MyList.RemoveAllSpaces(kStr);
                if (kStr.StartsWith("*")) continue;
                if (kStr.StartsWith("//")) continue;
                MyList mList = new MyList(kStr, ' ');

                if (kStr.Contains("START GROUP"))
                {
                    flag = true; continue;
                }
                else if (kStr.Contains("END") && kStr.Contains("XGIRDER_END") == false)
                {
                    flag = false; break;
                }
                if (flag)
                {
                    //int toIndex = mList.StringList.IndexOf("T0"
                    GroupCollection.Add(kMemberGroup.Parse(kStr));
                    hash_ta.Add(mList.StringList[0], GroupCollection[GroupCollection.Count - 1]);
                }

            }
        }
        public kMemberGroup GetMemberGroup(string memGroup)
        {
            return (kMemberGroup)hash_ta[memGroup];
        }
    }
//*/
    #endregion



    #region Support Reactions
    public class SupportReactions
    {
//        At Support=    1  For Load Case=    1  Maximum Reaction=-9.040E-001
//At Support=    1  For Load Case=    1  Maximum +ve Mx= 5.500E-002  For Load Case=    1  Maximum -ve Mx= 0.000E+000
//At Support=    1  For Load Case=    1  Maximum +ve Mz= 1.315E+000  For Load Case=    1  Maximum -ve Mz= 0.000E+000
        public int Support_No { get; set; }

        public double Max_Reaction { get; set; }
        public int Max_Reaction_Loadcase { get; set; }

        public double Max_Positive_Mx { get; set; }
        public int Max_Positive_Mx_Loadcase { get; set; }

        public double Max_Positive_Mz { get; set; }
        public int Max_Positive_Mz_Loadcase { get; set; }

        public double Max_Negative_Mx { get; set; }
        public int Max_Negative_Mx_Loadcase { get; set; }

        public double Max_Negative_Mz { get; set; }
        public int Max_Negative_Mz_Loadcase { get; set; }


        public SupportReactions()
        {
            Support_No = 0;
            Max_Reaction = 0.0;
            Max_Reaction_Loadcase = 0;


            Max_Positive_Mx = 0.0;
            Max_Positive_Mx_Loadcase = 0;

            Max_Positive_Mz = 0.0;
            Max_Positive_Mz_Loadcase = 0;

            Max_Negative_Mx = 0.0;
            Max_Negative_Mx_Loadcase = 0;


            Max_Negative_Mz = 0.0;
            Max_Negative_Mz_Loadcase = 0;
        }
    }
    #endregion Support Reactions
}
