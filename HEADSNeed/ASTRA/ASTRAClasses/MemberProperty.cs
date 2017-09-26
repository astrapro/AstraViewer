using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace HEADSNeed.ASTRA.ASTRAClasses
{

//1 2 PRISMATIC YD 12 ZD 12 IZ 509 IY 509 IX 1018
//3 TO 10 PR YD 12 ZD 12 IZ 864 IY 864 IX 1279
//11 TO 21 PR YD 21 ZD 16 IZ 5788 IY 2953 IX 6497
    [Serializable]
    public class MemberProperty
    {
        MemberIncidence miMember = null;
        private double dYD, dZD, dIX, dIY, dIZ, dArea,dPR;
        private string sPR = "", sDEN = "",dE = "";


        public MemberProperty()
        {
            miMember = new MemberIncidence();
            dYD = 0.0d;
            dZD = 0.0d;
            dIX = 0.0d;
            dIY = 0.0d;
            dIZ = 0.0d;
            dArea = 0.0d;
            dE = "";
            sDEN = "";
        }

        #region Public Property

        public MemberIncidence Member
        {
            get
            {
                return miMember;
            }
            set
            {
                miMember = value;
            }
        }
        public double YD
        {
            get
            {
                return dYD;
            }
            set
            {
                dYD = value;
            }
        }
        public double ZD
        {
            get
            {
                return dZD;
            }
            set
            {
                dZD = value;
            }
        }
        public double IX
        {
            get
            {
                return dIX;
            }
            set
            {
                dIX = value;
            }
        }
        public double IY
        {
            get
            {
                return dIY;
            }
            set
            {
                dIY = value;
            }
        }
        public double IZ
        {
            get
            {
                return dIZ;
            }
            set
            {
                dIZ = value;
            }
        }
        public string DEN
        {
            get
            {
                return sDEN;
            }
            set
            {
                sDEN = value;
            }
        }
        public string E
        {
            get
            {
                return dE;
            }
            set
            {
                dE = value;
            }
        }
        public string PR
        {
            get
            {
                return sPR;
            }
            set
            {
                sPR = value;
            }
        }
        public double Area
        {
            get
            {
                return dArea;
            }
            set
            {
                dArea = value;
            }
        }

        #endregion
    }
    [Serializable]
    public enum MemberType
    {
        Beam = 0,
        Truss = 1,
        PRISMATIC = 3,
    }
    [Serializable]
    public class MemberPropertyCollection : IList<MemberProperty>
    {
        List<MemberProperty> list = null;
        public ASTRAAnalysisData AnalysisData { get; set; }

        public List<string> ASTRA_Data { get; set; }
        public MemberPropertyCollection()
        {
            list = new List<MemberProperty>();
            ASTRA_Data = new List<string>();
        }

        #region IList<MemberProperty> Members

        public int IndexOf(MemberProperty item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Member.MemberNo == item.Member.MemberNo) return i;
            }
            return -1;
        }
        public int IndexOf(int MemberNo)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Member.MemberNo == MemberNo) return i;
            }
            return -1;
        }

        public void Insert(int index, MemberProperty item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public MemberProperty this[int index]
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

        #region ICollection<MemberProperty> Members

        public void Add(MemberProperty item)
        {
            foreach (var dd in list)
            {
                if (item.Member.MemberNo == dd.Member.MemberNo)
                {
                    dd.Area = item.Area;
                    dd.YD = item.YD;
                    dd.ZD = item.ZD;
                    dd.IX = item.IX;
                    dd.IY = item.IY;
                    dd.IZ = item.IZ;
                    return;
                }
            }
            list.Add(item);
        }
        public void AddAST(string s)
        {
            //
            //N003 UNIT 1.000 0.083 KIP FT KIP INCH member#, section_ID#,B, D, Do, Di, area*lfact*lfact, ix*lfact*lfact*lfact*lfact, iy*lfact*lfact*lfact*lfact, iy*lfact*lfact*lfact*lfact
            //N003	1	1	0	0	0	0	144	1018	509	509
            //N003	2	1	0	0	0	0	144	1018	509	509
            
            
            
            
            
            
            //N003 element#,section_ID#,B     , D,       Do,     Di,     area     , ix,         iy,          iz
            // 0       1     2          3       4        5       6         7         8           9           10
            //N003     1     1     0.000000 0.000000 0.000000 0.000000 144.000000 1018.000000 509.000000 509.000000
            //N003     2     1     0.000000 0.000000 0.000000 0.000000 144.000000 1018.000000 509.000000 509.000000


            //N004  element#,mat_ID#, emod     , pr,   mden,   wden   ,  alpha,   beta
            //0        1     2     3             4      5         6        7        8
            //N004     1     1   3150.000      0.150 0.000383 0.000383 0.000012 0.000000
            //N004     2     1   3150.000      0.150 0.000383 0.000383 0.000012 0.000000
            //N004     3     1   3150.000      0.150 0.000383 0.000383 0.000012 0.000000
            //N004     4     1   3150.000      0.150 0.000383 0.000383 0.000012 0.000000
            string temp = s.Trim().TrimEnd().TrimStart().Replace('\t', ' ');
            while (temp.Contains("  "))
            {
                temp = temp.Replace("  ", " ");
            }
            
            MemberProperty mProp = new MemberProperty();
            MyStrings mList = new MyStrings(temp, ' ');

            if (mList.StringList[0] == "N003")
            {
                mProp.Member.MemberNo = mList.GetInt(1);

                mProp.YD = mList.GetDouble(3);
                mProp.ZD = mList.GetDouble(4);

                mProp.Area = mList.GetDouble(7);
                mProp.IX = mList.GetDouble(8);
                mProp.IY = mList.GetDouble(9);
                mProp.IZ = mList.GetDouble(10);

                Add(mProp);
            }
            else if (mList.StringList[0] == "N004")
            {
                int indx = IndexOf(mList.GetInt(1));
                if (indx != -1)
                {
                    list[indx].E = mList.StringList[3];
                    list[indx].PR = mList.StringList[4];
                    list[indx].DEN = mList.StringList[5];
                }
                else
                {
                    //mProp = new MemberProperty();
                    //mProp.Member.MemberNo = mList.GetInt(1);
                    //mProp.Area = mList.GetDouble(7);
                    //mProp.IX = mList.GetDouble(8);
                    //mProp.IY = mList.GetDouble(9);
                    //mProp.IZ = mList.GetDouble(10);
                }
            }
        }
        public void AddTxt(string s)
        {
            //1 2 PRISMATIC YD 12 ZD 12 IZ 509 IY 509 IX 1018
            //3 TO 10 PR YD 12 ZD 12 IZ 864 IY 864 IX 1279
            //11 TO 21 PR YD 21 ZD 16 IZ 5788 IY 2953 IX 6497


            //1 TO 993 AX 0.54 IX 0.07390   IY 0.00911   IZ 0.06480   

            //1 3 4 A 28.11 IX 1509.4 IY 188.6 ; 2 A 21.67 IX 839.1 IY 94.8
            //5 6 7 A 43.24 IX 3920.5 IY 448.6 ; 8 TO 13 A 36.71 IX 2624.5 IY 328.8
            //14 TO 23 A 3.07 IX 4.5

            //1 2 3 13 14 15 16 18 94 95 100 TO 105 116 TO 129 165 167 172 173 174 184 TO 186 AX 0.54 IX 0.07390   IY 0.00911   IZ 0.06480   



            string temp = s.ToUpper().Trim().TrimEnd().TrimStart().Replace("PRISMATIC", "PR");

            List<string> lstStr = new List<string>(temp.Split(new char[] { ' ' }));
            MyStrings mList = new MyStrings(lstStr);

            double area, yd, zd, ix, iy, iz;
            int indx = -1;

            List<int> mems = MyStrings.Get_Array_Intiger(temp);

            if (AnalysisData != null)
            {
                if (mems.Count == 0)
                {
                    foreach (var item in AnalysisData.MemberGroups.GroupCollection)
                    {
                        if (item.GroupName == mList.StringList[0])
                        {
                            mems = MyStrings.Get_Array_Intiger(item.MemberNosText); break;
                        }
                    }
                }
            }


            area = yd = zd = ix = iy = iz = 0.0d;
            int toIndex = lstStr.IndexOf("TO");

            if (lstStr.Contains("YD"))
            {
                indx = lstStr.IndexOf("YD");
                yd = mList.GetDouble(indx + 1);
            }
            if (lstStr.Contains("ZD"))
            {
                indx = lstStr.IndexOf("ZD");
                zd = mList.GetDouble(indx + 1);
            }
            if (lstStr.Contains("IX"))
            {
                indx = lstStr.IndexOf("IX");
                ix = mList.GetDouble(indx + 1);
            }
            if (lstStr.Contains("IY"))
            {
                indx = lstStr.IndexOf("IY");
                iy = mList.GetDouble(indx + 1);
            }

            if (lstStr.Contains("IZ"))
            {
                indx = lstStr.IndexOf("IZ");
                iz = mList.GetDouble(indx + 1);
            }
            if (lstStr.Contains("AX"))
            {
                indx = lstStr.IndexOf("AX");
                area = mList.GetDouble(indx + 1);
            }
            if (lstStr.Contains("A"))
            {
                indx = lstStr.IndexOf("A");
                area = mList.GetDouble(indx + 1);
            }

            try
            {
                //1 2 3 13 14 15 16 18 94 95 100 TO 105 116 TO 129 165 167 172 173 174 184 TO 186 AX 0.54 IX 0.07390   IY 0.00911   IZ 0.06480   

                MemberProperty mp = null;
                for (int i = 0; i < mems.Count; i++)
                {
                    mp = new MemberProperty();
                    mp.Member.MemberNo = mems[i];
                    mp.YD = yd;
                    mp.ZD = zd;
                    mp.IX = ix;
                    mp.IY = iy;
                    mp.IZ = iz;
                    mp.Area = area;
                    Add(mp);
                }
            }
            catch (Exception exww) { }
        }

        public void AddTxt_2014_04_07(string s)
        {
            //1 2 PRISMATIC YD 12 ZD 12 IZ 509 IY 509 IX 1018
            //3 TO 10 PR YD 12 ZD 12 IZ 864 IY 864 IX 1279
            //11 TO 21 PR YD 21 ZD 16 IZ 5788 IY 2953 IX 6497


            //1 TO 993 AX 0.54 IX 0.07390   IY 0.00911   IZ 0.06480   

            //1 3 4 A 28.11 IX 1509.4 IY 188.6 ; 2 A 21.67 IX 839.1 IY 94.8
            //5 6 7 A 43.24 IX 3920.5 IY 448.6 ; 8 TO 13 A 36.71 IX 2624.5 IY 328.8
            //14 TO 23 A 3.07 IX 4.5

            //1 2 3 13 14 15 16 18 94 95 100 TO 105 116 TO 129 165 167 172 173 174 184 TO 186 AX 0.54 IX 0.07390   IY 0.00911   IZ 0.06480   



            string temp = s.ToUpper().Trim().TrimEnd().TrimStart().Replace("PRISMATIC", "PR");

            List<string> lstStr = new List<string>(temp.Split(new char[] { ' ' }));
            MyStrings mList = new MyStrings(lstStr);

            double area, yd, zd, ix, iy, iz;
            int indx = -1;

            List<int> mems = MyStrings.Get_Array_Intiger(temp);

            area = yd = zd = ix = iy = iz = 0.0d;
            int toIndex = lstStr.IndexOf("TO");

            if (lstStr.Contains("YD"))
            {
                indx = lstStr.IndexOf("YD");
                yd = mList.GetDouble(indx + 1);
            }
            if (lstStr.Contains("ZD"))
            {
                indx = lstStr.IndexOf("ZD");
                zd = mList.GetDouble(indx + 1);
            }
            if (lstStr.Contains("IX"))
            {
                indx = lstStr.IndexOf("IX");
                ix = mList.GetDouble(indx + 1);
            }
            if (lstStr.Contains("IY"))
            {
                indx = lstStr.IndexOf("IY");
                iy = mList.GetDouble(indx + 1);
            }

            if (lstStr.Contains("IZ"))
            {
                indx = lstStr.IndexOf("IZ");
                iz = mList.GetDouble(indx + 1);
            }
            if (lstStr.Contains("AX"))
            {
                indx = lstStr.IndexOf("AX");
                area = mList.GetDouble(indx + 1);
            }
            if (lstStr.Contains("A"))
            {
                indx = lstStr.IndexOf("A");
                area = mList.GetDouble(indx + 1);
            }

            try
            {
                //1 2 3 13 14 15 16 18 94 95 100 TO 105 116 TO 129 165 167 172 173 174 184 TO 186 AX 0.54 IX 0.07390   IY 0.00911   IZ 0.06480   
                if (lstStr.Contains("TO"))
                {
                    MyStrings tempLst = new MyStrings(temp, ' ');
                    toIndex = tempLst.StringList.IndexOf("TO", 0);
                    while (tempLst.StringList.Contains("TO"))
                    {
                        toIndex = tempLst.StringList.IndexOf("TO", 0);
                        if (toIndex > 1)
                        {
                            for (int i = 0; i < (toIndex - 1); i++)
                            {
                                MemberProperty mp = new MemberProperty();
                                mp.Member.MemberNo = tempLst.GetInt(i);
                                mp.YD = yd;
                                mp.ZD = zd;
                                mp.IX = ix;
                                mp.IY = iy;
                                mp.IZ = iz;
                                mp.Area = area;
                                Add(mp);
                            }
                            tempLst.StringList.RemoveRange(0, toIndex - 1);
                        }
                        toIndex = tempLst.StringList.IndexOf("TO", 0);
                        for (int i = tempLst.GetInt(toIndex - 1); i <= tempLst.GetInt(toIndex + 1); i++)
                        {
                            MemberProperty mp = new MemberProperty();
                            mp.Member.MemberNo = i;
                            mp.YD = yd;
                            mp.ZD = zd;
                            mp.IX = ix;
                            mp.IY = iy;
                            mp.IZ = iz;
                            mp.Area = area;
                            Add(mp);
                        }
                        tempLst.StringList.RemoveRange(0, toIndex + 2);
                    }
                    return;
                }
            }
            catch (Exception exx)
            {

            }



            //if (lstStr.Contains("TO"))
            //{
            //    toIndex = lstStr.IndexOf("TO");
            //    for (int i = mList.GetInt(toIndex - 1); i <= mList.GetInt(toIndex + 1); i++)
            //    {
            //        MemberProperty mp = new MemberProperty();
            //        mp.Member.MemberNo = i;
            //        mp.YD = yd;
            //        mp.ZD = zd;
            //        mp.IX = ix;
            //        mp.IY = iy;
            //        mp.IZ = iz;
            //        mp.Area = area;
            //        Add(mp);
            //    }
            //}
            //else
            //{
            //indx = lstStr.IndexOf("PR");
            if (indx == -1)
            {
                indx = lstStr.IndexOf("A");
                if (indx == -1)
                {
                    indx = lstStr.IndexOf("AX");
                }
            }
            for (int i = 0; i < indx; i++)
            {
                MemberProperty mp = new MemberProperty();
                mp.Member.MemberNo = mList.GetInt(i);
                mp.YD = yd;
                mp.ZD = zd;
                mp.IX = ix;
                mp.IY = iy;
                mp.IZ = iz;
                mp.Area = area;
                Add(mp);
            }
            //}
        }


        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(MemberProperty item)
        {
            return ((IndexOf(item) != -1) ? true : false);
        }

        public void CopyTo(MemberProperty[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(MemberProperty item)
        {
            if (IndexOf(item) != -1)
            {
                RemoveAt(IndexOf(item));
                return true;
            }
            return false;
        }

        #endregion

        #region IEnumerable<MemberProperty> Members

        public IEnumerator<MemberProperty> GetEnumerator()
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
        
        
        #region Public Methods

        public void CopyMemberIncidence(MemberIncidenceCollection mcCol)
        {
            int indx = -1;
            for (int i = 0; i < list.Count; i++)
            {
                indx = mcCol.IndexOf(list[i].Member);
                if (indx != -1)
                {
                    list[i].Member = mcCol[indx];
                    mcCol[indx].Property = list[i];
                }
            }
        }

        public void SetConstants(string s)
        {
            //CONSTANTS
            //E 3150 ALL
            //DEN 0.000383 ALL

            //3 CONSTANTS
            //E 200 1 TO 47
            //DEN 0.078 1 TO 47
            int indx = -1;

            string temp = s.Replace('\t', ' ').Trim().TrimEnd().TrimStart().ToUpper();
            while (temp.Contains("  "))
            {
                temp = temp.Replace("  ", " ");
            }
            MyStrings mList = new MyStrings(temp, ' ');


            if (list.Count == 0)
            {
                MemberProperty mp = null;
                for (int i = 0; i < AnalysisData.Members.Count; i++)
                {
                    mp = new MemberProperty();
                    mp.Member.MemberNo = AnalysisData.Members[i].MemberNo;
                    list.Add(mp);
                }
            }


            if (mList.StringList[0] == "E")
            {
                if (mList.StringList.Contains("ALL"))
                {

                    for (int i = 0; i < list.Count; i++)
                    {
                        list[i].E = mList.StringList[1];
                    }
                }
                else if (mList.StringList.Contains("TO"))
                {
                    indx = mList.StringList.IndexOf("TO");
                    //for (int i = mList.GetInt(indx - 1); i <= mList.GetInt(indx + 1); i++)
                    //{
                    //    list[i].E = mList.GetDouble(1);
                    //}
                    for (int i = mList.GetInt(indx - 1); i <= mList.GetInt(indx + 1); i++)
                    {
                        this[IndexOf(i)].E = mList.StringList[1];
                    }
                }
                else
                {
                    mList.StringList.RemoveAt(0);
                    //mList.StringList.RemoveAt(0);

                    double val = mList.GetDouble(0);
                    for (int i = 1; i < mList.Count; i++)
                    {
                        this[IndexOf(mList.GetInt(i))].E = mList.StringList[0];
                        
                    }
                }
            }
            if (mList.StringList[0].StartsWith("DEN"))
            {
                if (mList.StringList.Contains("ALL"))
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        list[i].DEN = mList.StringList[1];
                    }
                }
                else if (mList.StringList.Contains("TO"))
                {
                    indx = mList.StringList.IndexOf("TO");
                    //for (int i = mList.GetInt(indx - 1); i <= mList.GetInt(indx + 1); i++)
                    //{
                    //    list[i].DEN = mList.StringList[1];
                    //}
                    for (int i = mList.GetInt(indx - 1); i <= mList.GetInt(indx + 1); i++)
                    {
                        this[IndexOf(i)].DEN = mList.StringList[1];
                        //list[i].PR = mList.StringList[1];
                    }
                }
                else
                {
                    mList.StringList.RemoveAt(0);
                    //mList.StringList.RemoveAt(0);

                    //double val = mList.GetDouble(0);
                    for (int i = 1; i < mList.Count; i++)
                    {
                        this[IndexOf(mList.GetInt(i))].DEN = mList.StringList[0];

                    }
                }
            }
            if (mList.StringList[0] == "PR" || mList.StringList[0] == "POISSON")
            {
                if (mList.StringList.Contains("ALL"))
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        list[i].PR = mList.StringList[1];
                    }
                }
                else if (mList.StringList.Contains("TO"))
                {
                    indx = mList.StringList.IndexOf("TO");
                    for (int i = mList.GetInt(indx - 1); i <= mList.GetInt(indx + 1); i++)
                    {
                        this[IndexOf(i)].PR = mList.StringList[1];
                        //list[i].PR = mList.StringList[1];
                    }
                }
                else
                {
                    mList.StringList.RemoveAt(0);
                    for (int i = 1; i < mList.Count; i++)
                    {
                        this[IndexOf(mList.GetInt(i))].PR = mList.StringList[0];

                    }
                }
            }
        }
        #endregion
    }

    [Serializable]
    public class MyStrings
    {
        List<string> strList = null;
        char splitChar = ' ';
        public MyStrings(string s,char splitChar)
        {
            this.splitChar = splitChar;
            strList = new List<string>(s.Split(new char[] { splitChar }));
        }
        public MyStrings(List<string> lstStr)
        {
            strList = new List<string>();

            strList = lstStr;
        }

        public MyStrings(MyStrings obj)
        {
            splitChar = obj.splitChar;
            strList = new List<string>(obj.StringList);
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
            //catch (Exception exx)
            //{
            //}
            //return 0;

            //
        }
        public double GetDouble(int index)
        {
            try
            {
                return double.Parse(strList[index]);
            }
            catch (Exception exx) { }
            return 0.0;
        }
        public double GetDouble(int index, double default_value)
        {
            try
            {
                return double.Parse(strList[index]);
            }
            catch (Exception exx) { }
            return default_value;
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
        public string Text
        {
            get
            {
                string kStr = "";
                for (int i = 0; i < StringList.Count; i++)
                {
                    if(i == StringList.Count-1)
                    {
                        kStr += StringList[i];
                    }
                    else
                    {
                        kStr += StringList[i] + splitChar.ToString();
                    }
                }
                return kStr;
            }
        }
        public string GetString(int afterIndex)
        {
            string s = string.Empty;
            for (int i = afterIndex; i < strList.Count; i++)
            {
                s = s + splitChar + strList[i];
            }
            return s.Trim().TrimEnd().TrimStart();
        }
        public string GetString(int fromIndex, int toIndex)
        {
            string s = string.Empty;
            for (int i = fromIndex; i <= toIndex; i++)
            {
                s = s + strList[i] + splitChar;
            }
            return s;
        }

        public static string Get_Array_Text(List<int> list_int)
        {
            int val = 0;

            if (list_int.Count == 0) return "";

            List<int> temp_list = new List<int>();
            for (int i = 0; i < list_int.Count; i++)
            {
                if (!temp_list.Contains(list_int[i]))
                    temp_list.Add(list_int[i]);
            }

            list_int = temp_list;


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
                        kStr += " " + start_mbr_no + "   " + end_mbr_no;
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
            if ((end_mbr_no - start_mbr_no) == 1)
                kStr += "   " + start_mbr_no + "   " + end_mbr_no;
            else if (end_mbr_no != start_mbr_no)
                kStr += "   " + start_mbr_no + " TO " + (end_mbr_no);
            else
                kStr += "   " + start_mbr_no;
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

                MyStrings mlist = new MyStrings(MyStrings.RemoveAllSpaces(kStr), ' ');


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
                        kStr = mlist.GetString(0, i - 1);
                        break;
                    }
                }
                mlist = new MyStrings(MyStrings.RemoveAllSpaces(kStr), ' ');


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


        public static string Get_Analysis_Report_File(string input_path)
        {
            if (!File.Exists(input_path)) return "";
            return Path.Combine(Path.GetDirectoryName(input_path), "ANALYSIS_REP.TXT");
        }
        public static string Get_LL_TXT_File(string input_path)
        {
            if (!File.Exists(input_path)) return "";
            return Path.Combine(Path.GetDirectoryName(input_path), "LL.TXT");
        }

        public double SUM
        {
            get
            {
                double sum = 0;
                for (int i = 0; i < Count; i++)
                {
                    try
                    {
                        sum += GetDouble(i);
                    }
                    catch (Exception ex) { }
                }

                return sum;
            }
        }


        public static implicit operator MyStrings(string rhs)
        {
            MyStrings c = new MyStrings(rhs,' '); //Internally call Currency constructor
            return c;
        }
        public static implicit operator string(MyStrings rhs)
        {
            return rhs.Text;
        }

    }


    [Serializable]
    public class MaterialProperty
    {
        public string MemberNos { get; set; }
        public string Elastic_Modulus { get; set; }
        public string Density { get; set; }
        public string Possion_Ratio { get; set; }
        public string Alpha { get; set; }

        public MaterialProperty()
        {
            MemberNos = "";
            Elastic_Modulus = "";
            Density = "";
            Possion_Ratio = "";
            Alpha = "";
        }

        public List<string> ASTRA_Data
        {
            get
            {
                List<string> list = new List<string>();
                if (Elastic_Modulus != "")
                    list.Add(string.Format("E {0} {1}", Elastic_Modulus, MemberNos));
                if (Density != "")
                    list.Add(string.Format("DEN {0} {1}", Density, MemberNos));
                if (Possion_Ratio != "")
                    list.Add(string.Format("POISSON {0} {1}", Possion_Ratio, MemberNos));
                if (Alpha != "")
                    list.Add(string.Format("ALPHA {0} {1}", Alpha, MemberNos));
                return list;
            }
        }

    }

}
