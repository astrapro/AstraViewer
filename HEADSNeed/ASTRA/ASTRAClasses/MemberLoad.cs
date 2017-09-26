using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using VectorDraw.Serialize;
using VectorDraw.Professional;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.Constants;
using VectorDraw.Geometry;
using VectorDraw.Render;
using VectorDraw.Professional.Actions;
using VectorDraw.Actions;

using HEADSNeed.ASTRA.ASTRADrawingTools;
namespace HEADSNeed.ASTRA.ASTRAClasses
{
    public class MemberLoad
    {
        #region private variables

        eDirection direction;
        eLoadType loadType;
        double dValue;
        MemberIncidence mincident = null;
        int iLoadCase = 0;
        double distStartNode = 0.0;

        
        #endregion

        #region ctor
        public MemberLoad()
        {
            direction = eDirection.X;
            loadType = eLoadType.Concentrate;
            dValue = 0.0d;
            W1 = 0.0d;
            W2 = 0.0d;
            D1 = 0.0d;
            D2 = 0.0d;
            D3 = 0.0d;
            mincident = new MemberIncidence();

        }
        #endregion

        #region public Property
        public MemberIncidence Member
        {
            get
            {
                return mincident;
            }
            set
            {
                mincident = value;
            }
        }
        public eDirection Direction
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
            }
        }
        public eLoadType LoadType
        {
            get
            {
                return loadType;
            }
            set
            {
                loadType = value;
            }
        }

        public double W1 { get; set; }
        public double W2 { get; set; }
        public double D1 { get; set; }
        public double D2 { get; set; }
        public double D3 { get; set; }

        public double Value
        {
            get
            {
                return dValue;
            }
            set
            {
                dValue = value;
            }
        }

        public int LoadCase
        {
            get
            {
                return iLoadCase;
            }
            set
            {
                iLoadCase = value;
            }
        }
        public double DistanceFromStartNode
        {
            get
            {
                return distStartNode;
            }
            set
            {
                distStartNode = value;
            }
        }
        #endregion

        #region public Enum
        public enum eDirection
        {
            X = 0,
            Y = 1,
            Z = 2,
            GX = 3,
            GY = 4,
            GZ = 5
        }
        public enum eLoadType
        {
            Concentrate = 1,
            UDL = 2,
            LINEAR = 3,
        }
        #endregion
    }
    public class MemberLoadCollection : IList<MemberLoad>
    {
        List<MemberLoad> list = null;
        //vdLayer membloadLay = null;
        vdLayer membload_GLOBAL_Lay = null;
        vdLayer membload_LOCAL_Lay = null;
        vdLayer conc_LOCAL_Lay = null;
        vdLayer conc_GLOBAL_Lay = null;
        double xtemp, ytemp, d1, d2, d3, d4, radFac, mx, my, theta;

        public MemberLoadCollection()
        {
            list = new List<MemberLoad>();
            //membloadLay = new vdLayer();
            membload_GLOBAL_Lay = new vdLayer();
            membload_LOCAL_Lay = new vdLayer();

            conc_GLOBAL_Lay = new vdLayer();
            conc_LOCAL_Lay = new vdLayer();


            //membloadLay.Name = "MemberLoad";
            membload_GLOBAL_Lay.Name = "MemberLoadGlobal";
            membload_LOCAL_Lay.Name = "MemberLoadLocal";

            conc_GLOBAL_Lay.Name = "ConcentrateGlobal";
            conc_LOCAL_Lay.Name = "ConcentrateLocal";

        }

        #region IList<MemberLoad> Members

        public int IndexOf(MemberLoad item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Member.MemberNo == item.Member.MemberNo && list[i].LoadCase == item.LoadCase
                    && list[i].LoadType == item.LoadType && list[i].Direction == item.Direction)
                {
                    return i;
                }
            }
            return -1;
        }
        public int IndexOf(int MemberNo,int LoadCase)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Member.MemberNo == MemberNo && list[i].LoadCase == LoadCase)
                {
                    return i;
                }
            }
            return -1;
        }
        public int IndexOf(int MemberNo)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Member.MemberNo == MemberNo)
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, MemberLoad item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public MemberLoad this[int index]
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

        #region ICollection<MemberLoad> Members

        public void Add(MemberLoad item)
        {
            int indx = IndexOf(item);
            if (indx != -1)
            {

                if (list[indx].D1 != item.D1)
                    list.Add(item);
                else
                    list[indx].Value += item.Value;
            }
            else
                list.Add(item);
        }

        public void AddConcentrateLoad(string s, int loadCase)
        {
            string temp = s.Trim().TrimEnd().TrimStart().Replace('\t', ' ').ToUpper();
            while (temp.Contains("  "))
            {
                temp = temp.Replace("  ", " ");
            }
            //11 TO 16 UNI Y -2.8 CON GY -2.0 D 2.0 CON GX -2.0 D 2.0
            //11 TO 16 UNI Y -2.8 CON Y -2.0 D 2.0 CON X -2.0 D 2.0

            //11 TO 16 UNI Y -5.1 CON Y -3.0 D 3.2
            //11 TO 16 CON Y -5.1 CON Y -3.0 D 3.2
            //11 TO 16 CON Y -5.1 D 3.2

            MyStrings mList = new MyStrings(temp, ' ');


            
            int toIndex = 0;
            int indx = 0;
            int toMember = 0, fromMember = 0;
            int conIndex = 0;
            int uniIndex = 0;
            double conValue = 0.0d;
            double uniValue = 0.0d;
            double conDist = 0.0d;
            MemberLoad.eDirection direction;

            
            conIndex = -1;
            for (int i = 0; i < mList.Count; i++)
            {
                if (mList.StringList[i].ToUpper().StartsWith("CON"))
                {
                    conIndex = i; break;
                }
            }
            if (conIndex == -1) return;

            List<int> mems = MyStrings.Get_Array_Intiger(mList.GetString(0, conIndex));

            //toIndex = mList.StringList.IndexOf("TO");

            //if (toIndex != -1)
            //{
            //    toMember = mList.GetInt(toIndex - 1);
            //    fromMember = mList.GetInt(toIndex + 1);

            //    mList.StringList.RemoveRange(0, toIndex + 2);
            //}
            MemberLoad mLoad = new MemberLoad();



            //conIndex = toIndex;
            //uniIndex = mList.StringList.IndexOf("UNI");
            direction = MemberLoad.eDirection.Y;

            if (conIndex != -1)
            {
                //11 TO 16 UNI Y -2.8 CON Y -2.0 D 2.0 CON X -2.0 D 2.0
                if (mList.StringList[conIndex + 1] == "X")
                {
                    direction = MemberLoad.eDirection.X;
                }
                else if (mList.StringList[conIndex + 1] == "Y")
                {
                    direction = MemberLoad.eDirection.Y;
                }
                else if (mList.StringList[conIndex + 1] == "Z")
                {
                    direction = MemberLoad.eDirection.Z;
                }
                else if (mList.StringList[conIndex + 1] == "GX")
                {
                    direction = MemberLoad.eDirection.GX;
                }
                else if (mList.StringList[conIndex + 1] == "GY")
                {
                    direction = MemberLoad.eDirection.GY;
                }
                else if (mList.StringList[conIndex + 1] == "GZ")
                {
                    direction = MemberLoad.eDirection.GZ;
                }
                direction = direction;
                conValue = mList.GetDouble(conIndex + 2);


                if (mList.StringList.Contains("D"))
                    conDist = mList.GetDouble(conIndex + 4);
                else
                    if (mList.Count > (conIndex + 3))
                        conDist = mList.GetDouble(conIndex + 3);




                //mList.StringList.RemoveRange(0, conIndex + 5);

                //List<int> mems = MyList.Get_Array_Intiger(temp);
                for (int i = 0; i < mems.Count; i++)
                {
                    mLoad = new MemberLoad();
                    mLoad.Member.MemberNo = mems[i];
                    mLoad.Direction = direction;
                    mLoad.LoadType = MemberLoad.eLoadType.Concentrate;
                    mLoad.Value = conValue;
                    mLoad.D1 = conDist;

                    if (mList.Count > (conIndex + 4))
                    {
                        try
                        {
                            mLoad.D2 = mList.GetDouble(conIndex + 4);
                        }
                        catch (Exception exx) { }
                    }

                    mLoad.DistanceFromStartNode = conDist;

                    mLoad.LoadCase = loadCase;
                    list.Add(mLoad);
                }
                //uniValue = uniValue;
                //conValue = conValue;
                //conDist = conDist;
            }
        }

        public void AddTxt(string data, int loadCase)
        {

            if (data.Contains("CON"))
            {
                AddConcentrateLoad(data, loadCase);
                return;
            }

            //11 TO 16 UNI Y -2.8
            //11 TO 16 UNI Y -5.1

            //11 TO 16 CON Y -2.8
            //11 TO 16 CON GY -5.1
            string temp = data.Trim().TrimEnd().TrimStart();

            MemberLoad.eDirection direction = MemberLoad.eDirection.Y;
            MemberLoad.eLoadType loadType = MemberLoad.eLoadType.UDL;
            int toIndex = -1;
            int toMemb, fromMemb;
            double val = 0.0d;

            List<string> lstStr = new List<string>();

            lstStr.AddRange(temp.Split(new char[] { ' ' }));

            MyStrings mlist = new MyStrings(lstStr);

            string kStr = "";
            if (lstStr.Contains("UNI"))
            {
                #region 127 TO 135 166 TO 174 UNI GY -1.6455


                toMemb = lstStr.IndexOf("UNI");

                kStr = mlist.GetString(0, toMemb - 1);

                if (true)
                {

                    if (true)
                    {

                        loadType = MemberLoad.eLoadType.UDL;
                        switch (lstStr[toMemb + 1].ToUpper())
                        {
                            case "X":
                                direction = MemberLoad.eDirection.X; break;
                            case "GX":
                                direction = MemberLoad.eDirection.GX; break;
                            case "Y":
                                direction = MemberLoad.eDirection.Y; break;
                            case "GY":
                                direction = MemberLoad.eDirection.GY; break;
                            case "Z":
                                direction = MemberLoad.eDirection.Z; break;
                            case "GZ":
                                direction = MemberLoad.eDirection.GZ; break;
                        }
                    }
                    val = mlist.GetDouble(toMemb + 2);

                    foreach (int ii in MyStrings.Get_Array_Intiger(kStr))
                    {
                        MemberLoad membLoad = new MemberLoad();
                        membLoad.Member.MemberNo = ii;
                        membLoad.Direction = direction;
                        membLoad.LoadType = loadType;
                        membLoad.Value = val;

                        if (mlist.Count > (toMemb + 3))
                        {
                            membLoad.D1 = MyStrings.StringToDouble(mlist.StringList[toMemb + 3], 0.0);
                        }
                        if (mlist.Count > (toMemb + 4))
                        {
                            membLoad.D2 = MyStrings.StringToDouble(mlist.StringList[toMemb + 4], 0.0);
                        }
                        if (mlist.Count > (toMemb + 5))
                        {
                            membLoad.D3 = MyStrings.StringToDouble(mlist.StringList[toMemb + 5], 0.0);
                        }
                        membLoad.LoadCase = loadCase;
                        Add(membLoad);
                    }
                }
                #endregion
            }
            else if (lstStr.Contains("LIN"))
            {
                #region 127 TO 135 166 TO 174 UNI GY -1.6455


                toMemb = lstStr.IndexOf("LIN");

                kStr = mlist.GetString(0, toMemb - 1);

                if (true)
                {

                    if (true)
                    {

                        loadType = MemberLoad.eLoadType.UDL;
                        switch (lstStr[toMemb + 1].ToUpper())
                        {
                            case "X":
                                direction = MemberLoad.eDirection.X; break;
                            case "GX":
                                direction = MemberLoad.eDirection.GX; break;
                            case "Y":
                                direction = MemberLoad.eDirection.Y; break;
                            case "GY":
                                direction = MemberLoad.eDirection.GY; break;
                            case "Z":
                                direction = MemberLoad.eDirection.Z; break;
                            case "GZ":
                                direction = MemberLoad.eDirection.GZ; break;
                        }
                    }
                    val = mlist.GetDouble(toMemb + 2);

                    foreach (int ii in MyStrings.Get_Array_Intiger(kStr))
                    {
                        MemberLoad membLoad = new MemberLoad();
                        membLoad.Member.MemberNo = ii;
                        membLoad.Direction = direction;
                        membLoad.LoadType = MemberLoad.eLoadType.LINEAR;
                        membLoad.Value = val;
                        membLoad.W1 = val;
                        if(mlist.Count > ( toMemb + 3))
                            membLoad.W2 = mlist.GetDouble(toMemb + 3); ;

                        

                        membLoad.LoadCase = loadCase;
                        Add(membLoad);
                    }
                }
                #endregion
            }
            return;

        }
        public void AddTxt_Old(string data, int loadCase)
        {
            if (data.Contains("CON"))
            {
                AddConcentrateLoad(data, loadCase);
                return;
            }

            //11 TO 16 UNI Y -2.8
            //11 TO 16 UNI Y -5.1

            //11 TO 16 CON Y -2.8
            //11 TO 16 CON GY -5.1
            string temp = data.Trim().TrimEnd().TrimStart();

            MemberLoad.eDirection direction = MemberLoad.eDirection.Y;
            MemberLoad.eLoadType loadType = MemberLoad.eLoadType.UDL;
            int toIndex = -1;
            int toMemb, fromMemb;
            double val = 0.0d;

            List<string> lstStr = new List<string>();

            lstStr.AddRange(temp.Split(new char[] { ' ' }));

            if (lstStr.Contains("UNI"))
            {

                #region 11 TO 16 UNI Y -2.8 And 11 TO 16 CON Y -2.8
                if (lstStr.Count == 6)
                {

                    if (lstStr.Contains("TO"))
                    {
                        toIndex = lstStr.IndexOf("TO");
                        fromMemb = int.Parse(lstStr[toIndex - 1].Trim().TrimStart().TrimEnd());
                        toMemb = int.Parse(lstStr[toIndex + 1].Trim().TrimStart().TrimEnd());

                        if (lstStr[toIndex + 2] == "UNI")
                        {
                            loadType = MemberLoad.eLoadType.UDL;
                        }
                        else if (lstStr[toIndex + 2] == "CON")
                        {
                            loadType = MemberLoad.eLoadType.Concentrate;
                        }
                        if (lstStr[toIndex + 3] == "X")
                        {
                            direction = MemberLoad.eDirection.X;
                        }
                        if (lstStr[toIndex + 3] == "GX")
                        {
                            direction = MemberLoad.eDirection.GX;
                        }
                        if (lstStr[toIndex + 3] == "Y")
                        {
                            direction = MemberLoad.eDirection.Y;
                        }
                        if (lstStr[toIndex + 3] == "GY")
                        {
                            direction = MemberLoad.eDirection.GY;
                        }
                        if (lstStr[toIndex + 3] == "Z")
                        {
                            direction = MemberLoad.eDirection.Z;
                        }
                        if (lstStr[toIndex + 3] == "GZ")
                        {
                            direction = MemberLoad.eDirection.GZ;
                        }
                        val = double.Parse(lstStr[toIndex + 4].Trim().TrimEnd().TrimStart());

                        for (int ii = fromMemb; ii <= toMemb; ii++)
                        {
                            MemberLoad membLoad = new MemberLoad();
                            membLoad.Member.MemberNo = ii;
                            membLoad.Direction = direction;
                            membLoad.LoadType = loadType;
                            membLoad.Value = val;
                            membLoad.LoadCase = loadCase;
                            Add(membLoad);
                        }
                    }
                }
                //list.Add(item);
                #endregion

                #region 8 TO 10 11 TO 13 UNI Y -0.85
                if (lstStr.Count == 9)
                {

                    if (lstStr.Contains("TO"))
                    {
                        toIndex = lstStr.IndexOf("TO");
                        fromMemb = int.Parse(lstStr[0].Trim().TrimStart().TrimEnd());
                        toMemb = int.Parse(lstStr[2].Trim().TrimStart().TrimEnd());

                        if (lstStr[6] == "UNI")
                        {
                            loadType = MemberLoad.eLoadType.UDL;
                        }
                        else if (lstStr[6] == "CON")
                        {
                            loadType = MemberLoad.eLoadType.Concentrate;
                        }
                        if (lstStr[7] == "X")
                        {
                            direction = MemberLoad.eDirection.X;
                        }
                        if (lstStr[7] == "GX")
                        {
                            direction = MemberLoad.eDirection.GX;
                        }
                        if (lstStr[7] == "Y")
                        {
                            direction = MemberLoad.eDirection.Y;
                        }
                        if (lstStr[7] == "GY")
                        {
                            direction = MemberLoad.eDirection.GY;
                        }
                        if (lstStr[7] == "Z")
                        {
                            direction = MemberLoad.eDirection.Z;
                        }
                        if (lstStr[7] == "GZ")
                        {
                            direction = MemberLoad.eDirection.GZ;
                        }
                        val = double.Parse(lstStr[8].Trim().TrimEnd().TrimStart());

                        for (int ii = fromMemb; ii <= toMemb; ii++)
                        {
                            MemberLoad membLoad = new MemberLoad();
                            membLoad.Member.MemberNo = ii;
                            membLoad.Direction = direction;
                            membLoad.LoadType = loadType;
                            membLoad.Value = val;
                            membLoad.LoadCase = loadCase;
                            Add(membLoad);
                        }
                        fromMemb = int.Parse(lstStr[3].Trim().TrimStart().TrimEnd());
                        toMemb = int.Parse(lstStr[5].Trim().TrimStart().TrimEnd());
                        for (int ii = fromMemb; ii <= toMemb; ii++)
                        {
                            MemberLoad membLoad = new MemberLoad();
                            membLoad.Member.MemberNo = ii;
                            membLoad.Direction = direction;
                            membLoad.LoadType = loadType;
                            membLoad.Value = val;
                            membLoad.LoadCase = loadCase;
                            Add(membLoad);
                        }
                    }
                }
                //list.Add(item);
                #endregion

                #region 6 UNI GY -1.1
                if (!lstStr.Contains("TO"))
                {

                    if (!lstStr.Contains("TO"))
                    {
                        toIndex = lstStr.IndexOf("UNI");
                        loadType = MemberLoad.eLoadType.UDL;
                        if (toIndex == -1)
                        {
                            toIndex = lstStr.IndexOf("CON");
                            loadType = MemberLoad.eLoadType.Concentrate;
                        }
                        //fromMemb = int.Parse(lstStr[0].Trim().TrimStart().TrimEnd());
                        //toMemb = int.Parse(lstStr[2].Trim().TrimStart().TrimEnd());

                        if (lstStr[toIndex + 1] == "X")
                        {
                            direction = MemberLoad.eDirection.X;
                        }
                        if (lstStr[toIndex + 1] == "GX")
                        {
                            direction = MemberLoad.eDirection.GX;
                        }
                        if (lstStr[toIndex + 1] == "Y")
                        {
                            direction = MemberLoad.eDirection.Y;
                        }
                        if (lstStr[toIndex + 1] == "GY")
                        {
                            direction = MemberLoad.eDirection.GY;
                        }
                        if (lstStr[toIndex + 1] == "Z")
                        {
                            direction = MemberLoad.eDirection.Z;
                        }
                        if (lstStr[toIndex + 1] == "GZ")
                        {
                            direction = MemberLoad.eDirection.GZ;
                        }
                        val = double.Parse(lstStr[toIndex + 2].Trim().TrimEnd().TrimStart());

                        for (int ii = 0; ii < toIndex; ii++)
                        {
                            MemberLoad membLoad = new MemberLoad();
                            membLoad.Member.MemberNo = int.Parse(lstStr[ii]);
                            membLoad.Direction = direction;
                            membLoad.LoadType = loadType;
                            membLoad.Value = val;
                            membLoad.LoadCase = loadCase;
                            Add(membLoad);
                        }
                    }
                }
                //list.Add(item);
                #endregion
            }
        }



        public void AddAST(string data)
        {
            //11 TO 16 UNI Y -2.8
            //11 TO 16 UNI Y -5.1

            //11 TO 16 CON Y -2.8
            //11 TO 16 CON GY -5.1

            //N006    Member# loadcase#,udlx     , udly       udlz   ,    px ,       d1,        py,        d2,         pz,       d3
            // 0    1      2     3        4          5          6          7          8          9          10        11        12    
            //N006 LCS    11     1      0.000     -2.800      0.000      0.000      0.000      0.000      0.000      0.000      0.000
            //N006 LCS    12     1      0.000     -2.800      0.000      0.000      0.000      0.000      0.000      0.000      0.000

            string temp = data.Trim().TrimEnd().TrimStart().Replace('\t', ' ');
            while (temp.Contains("  "))
            {
               temp = temp.Replace("  ", " ");
            }
            
            MyStrings mList = new MyStrings(temp, ' ');
            MemberLoad mLoad = new MemberLoad();
            mLoad.LoadType = MemberLoad.eLoadType.UDL;
            mLoad.LoadCase = mList.GetInt(3);
            mLoad.Member.MemberNo = mList.GetInt(2);

            if (mList.StringList[1] == "LCS")
            {
                if (mList.GetDouble(4) != 0.0d)
                {
                    mLoad.LoadType = MemberLoad.eLoadType.UDL;
                    mLoad.Direction = MemberLoad.eDirection.X;
                    mLoad.Value = mList.GetDouble(4);
                }
                if (mList.GetDouble(5) != 0.0d)
                {
                    mLoad.LoadType = MemberLoad.eLoadType.UDL;
                    mLoad.Direction = MemberLoad.eDirection.Y;
                    mLoad.Value = mList.GetDouble(5);
                }
                if (mList.GetDouble(6) != 0.0d)
                {
                    mLoad.LoadType = MemberLoad.eLoadType.UDL;
                    mLoad.Direction = MemberLoad.eDirection.Z;
                    mLoad.Value = mList.GetDouble(6);
                }
                if (mList.GetDouble(7) != 0.0d)
                {
                    mLoad.Direction = MemberLoad.eDirection.X;
                    mLoad.Value = mList.GetDouble(7);
                    mLoad.LoadType = MemberLoad.eLoadType.Concentrate;
                    mLoad.DistanceFromStartNode = mList.GetDouble(8);
                }
                if (mList.GetDouble(9) != 0.0d)
                {
                    mLoad.Direction = MemberLoad.eDirection.Y;
                    mLoad.Value = mList.GetDouble(9);
                    mLoad.LoadType = MemberLoad.eLoadType.Concentrate;
                    mLoad.DistanceFromStartNode = mList.GetDouble(10);
                }

                if (mList.GetDouble(11) != 0.0d)
                {
                    mLoad.Direction = MemberLoad.eDirection.Z;
                    mLoad.Value = mList.GetDouble(11);
                    mLoad.LoadType = MemberLoad.eLoadType.Concentrate;
                    mLoad.DistanceFromStartNode = mList.GetDouble(12);
                }
            }
            else if (mList.StringList[1] == "GCS")
            {
                mLoad.LoadCase = mList.GetInt(3);
                if (mList.GetDouble(4) != 0.0d)
                {
                    mLoad.Direction = MemberLoad.eDirection.GX;
                    mLoad.Value = mList.GetDouble(4);
                }
                if (mList.GetDouble(5) != 0.0d)
                {
                    mLoad.Direction = MemberLoad.eDirection.GY;
                    mLoad.Value = mList.GetDouble(5);
                }
                if (mList.GetDouble(6) != 0.0d)
                {
                    mLoad.Direction = MemberLoad.eDirection.GZ;
                    mLoad.Value = mList.GetDouble(6);
                }
                if (mList.GetDouble(7) != 0.0d)
                {
                    mLoad.Direction = MemberLoad.eDirection.GX;
                    mLoad.Value = mList.GetDouble(7);
                    mLoad.LoadType = MemberLoad.eLoadType.Concentrate;
                }
                if (mList.GetDouble(9) != 0.0d)
                {
                    mLoad.Direction = MemberLoad.eDirection.GY;
                    mLoad.Value = mList.GetDouble(9);
                    mLoad.LoadType = MemberLoad.eLoadType.Concentrate;
                }

                if (mList.GetDouble(11) != 0.0d)
                {
                    mLoad.Direction = MemberLoad.eDirection.GZ;
                    mLoad.Value = mList.GetDouble(11);
                    mLoad.LoadType = MemberLoad.eLoadType.Concentrate;
                }
            }
            Add(mLoad);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(MemberLoad item)
        {
            return ((IndexOf(item) != -1) ? true : false);
        }

        public void CopyTo(MemberLoad[] array, int arrayIndex)
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

        public bool Remove(MemberLoad item)
        {
            int indx = IndexOf(item);
            if (indx != -1)
            {
                list.RemoveAt(indx); return true;
            }
            return false;
        }

        #endregion

        #region IEnumerable<MemberLoad> Members

        public IEnumerator<MemberLoad> GetEnumerator()
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

        public void SetLoadCombination(int loadCase, int loadCombination, double loadValue)
        {
            for (int ii = 0; ii < list.Count; ii++)
            {
                if (list[ii].LoadCase == loadCase)
                {
                    MemberLoad mload = new MemberLoad();
                    mload.Direction = list[ii].Direction;
                    mload.DistanceFromStartNode = list[ii].DistanceFromStartNode;
                    mload.LoadCase = loadCombination;
                    mload.LoadType = list[ii].LoadType;
                    mload.Member = list[ii].Member;
                    mload.Value = list[ii].Value * loadValue;
                    Add(mload);
                }
            }
        }
        public void SetLoadCombination(string text,int LoadComb)
        {
            //1 0.50 2 0.75
            //1 0.74 2

            string temp = text.Trim().TrimEnd().TrimStart().Replace('\t', ' ');
            MyStrings mList = new MyStrings(temp, ' ');

            double value = 0.0d;
            int loadCase = -1;
            for (int i = 0; i < mList.Count; i += 2)
            {
                loadCase = mList.GetInt(i);
                value = mList.GetDouble(i + 1);
                SetLoadCombination(loadCase, LoadComb, value);
            }
        }

        public void CopyMember(MemberIncidenceCollection miCol)
        {
            int indx = -1;
            for (int i = 0; i < list.Count; i++)
            {
                try
                {
                    indx = miCol.IndexOf(list[i].Member);
                    list[i].Member = miCol[indx];
                }
                catch (Exception exx) { }
                //indx =  miCol.IndexOf(list[i].Member);
                //list[i].Member = miCol[indx];
            }
        }

        public void DrawMemberLoad(vdDocument document, MemberLoad mload)
        {
            //membloadLay = new vdLayer();
            //membloadLay.Name = "MemberLoad";
            //document.Layers.AddItem(membloadLay);
            //membloadLay.PenColor = new vdColor(Color.Blue);


            membload_GLOBAL_Lay.Name = "MemberLoadGlobal";
            if (document.Layers.FindName("MemberLoadGlobal") == null)
            {
                membload_GLOBAL_Lay = new vdLayer();
                membload_GLOBAL_Lay.Name = "MemberLoadGlobal";
                membload_GLOBAL_Lay.SetUnRegisterDocument(document);
                membload_GLOBAL_Lay.setDocumentDefaults();
                membload_GLOBAL_Lay.PenColor = new vdColor(Color.Green);
                document.Layers.AddItem(membload_GLOBAL_Lay);
            }
            else
            {
                membload_GLOBAL_Lay = document.Layers.FindName("MemberLoadGlobal");
            }

            membload_LOCAL_Lay.Name = "MemberLoadLocal";
            if (document.Layers.FindName("MemberLoadLocal") == null)
            {
                membload_LOCAL_Lay = new vdLayer();
                membload_LOCAL_Lay.Name = "MemberLoadLocal";
                membload_LOCAL_Lay.SetUnRegisterDocument(document);
                membload_LOCAL_Lay.setDocumentDefaults();
                membload_LOCAL_Lay.PenColor = new vdColor(Color.Blue);
                document.Layers.AddItem(membload_LOCAL_Lay);
            }
            else
            {
                membload_LOCAL_Lay = document.Layers.FindName("MemberLoadLocal");
            }


            if (mload.LoadType == MemberLoad.eLoadType.Concentrate)
            {
                membload_LOCAL_Lay.PenColor = new vdColor(Color.LightGray);
                membload_GLOBAL_Lay.PenColor = new vdColor(Color.Black);
            }
            else
            {
                membload_LOCAL_Lay.PenColor = new vdColor(Color.Blue);
                membload_GLOBAL_Lay.PenColor = new vdColor(Color.Green);
            }
            if (mload.Value > 0)
            {
                switch (mload.Direction)
                {
                    case MemberLoad.eDirection.X:
                        DrawMemberLoad_X_Positive_LOCAL(document, mload);
                        break;
                    case MemberLoad.eDirection.GX:
                        DrawMemberLoad_X_Positive_GLOBAL(document, mload);
                        break;
                    case MemberLoad.eDirection.GY:
                        DrawMemberLoad_Y_Positive_GLOBAL(document, mload);
                        break;
                    case MemberLoad.eDirection.Y:
                        DrawMemberLoad_Y_Positive_LOCAL(document, mload);
                        break;
                    case MemberLoad.eDirection.Z:
                        DrawMemberLoad_Z_Positive_LOCAL(document, mload);
                        break;
                    case MemberLoad.eDirection.GZ:
                        DrawMemberLoad_Z_Positive_GLOBAL(document, mload);
                        break;
                }
            }
            else
            {
                switch (mload.Direction)
                {
                    case MemberLoad.eDirection.X:
                        DrawMemberLoad_X_Negative_LOCAL(document, mload);
                        break;
                    case MemberLoad.eDirection.Y:
                        DrawMemberLoad_Y_Negative_LOCAL(document, mload);
                        break;
                    case MemberLoad.eDirection.Z:
                        DrawMemberLoad_Z_Negative_LOCAL(document, mload);
                        break;
                    case MemberLoad.eDirection.GX:
                        DrawMemberLoad_X_Negative_GLOBAL(document, mload);
                        break;
                    case MemberLoad.eDirection.GY:
                        DrawMemberLoad_Y_Negative_GLOBAL(document, mload);
                        break;
                    case MemberLoad.eDirection.GZ:
                        DrawMemberLoad_Z_Negative_GLOBAL(document, mload);
                        break;
                }
            }
            document.Redraw(true);
        }
        public void DrawMemberLoad(vdDocument document,int LoadCase)
        {
            membload_GLOBAL_Lay.Name = "MemberLoadGlobal";
            if (document.Layers.FindName("MemberLoadGlobal") == null)
            {
                membload_GLOBAL_Lay = new vdLayer();
                membload_GLOBAL_Lay.Name = "MemberLoadGlobal";
                membload_GLOBAL_Lay.SetUnRegisterDocument(document);
                membload_GLOBAL_Lay.setDocumentDefaults();
                membload_GLOBAL_Lay.PenColor = new vdColor(Color.Green);
                document.Layers.AddItem(membload_GLOBAL_Lay);
            }
            else
            {
                membload_GLOBAL_Lay = document.Layers.FindName("MemberLoadGlobal");
            }

            membload_LOCAL_Lay.Name = "MemberLoadLocal";
            if (document.Layers.FindName("MemberLoadLocal") == null)
            {
                membload_LOCAL_Lay = new vdLayer();
                membload_LOCAL_Lay.Name = "MemberLoadLocal";
                membload_LOCAL_Lay.SetUnRegisterDocument(document);
                membload_LOCAL_Lay.setDocumentDefaults();
                membload_LOCAL_Lay.PenColor = new vdColor(Color.Blue);
                document.Layers.AddItem(membload_LOCAL_Lay);
            }
            else
            {
                membload_LOCAL_Lay = document.Layers.FindName("MemberLoadLocal");
            }

            //membload_LOCAL_Lay = new vdLayer();
            //membload_LOCAL_Lay.Name = "MemberLoadLocal";
            //document.Layers.AddItem(membload_LOCAL_Lay);
            //membload_LOCAL_Lay.PenColor = new vdColor(Color.Blue);


            try
            {

                foreach (MemberLoad mload in list)
                {

                    if (mload.LoadCase != LoadCase) continue;

                    if (mload.LoadType == MemberLoad.eLoadType.UDL)
                    {
                        if (mload.Value > 0)
                        {
                            switch (mload.Direction)
                            {
                                case MemberLoad.eDirection.X:
                                    DrawMemberLoad_X_Positive_LOCAL(document, mload);
                                    break;
                                case MemberLoad.eDirection.GX:
                                    DrawMemberLoad_X_Positive_GLOBAL(document, mload);
                                    break;
                                case MemberLoad.eDirection.GY:
                                    DrawMemberLoad_Y_Positive_GLOBAL(document, mload);
                                    break;
                                case MemberLoad.eDirection.Y:
                                    DrawMemberLoad_Y_Positive_LOCAL(document, mload);
                                    break;
                                case MemberLoad.eDirection.Z:
                                    DrawMemberLoad_Z_Positive_LOCAL(document, mload);
                                    break;
                                case MemberLoad.eDirection.GZ:
                                    DrawMemberLoad_Z_Positive_GLOBAL(document, mload);
                                    break;
                            }
                        }
                        else
                        {
                            switch (mload.Direction)
                            {
                                case MemberLoad.eDirection.X:
                                    DrawMemberLoad_X_Negative_LOCAL(document, mload);
                                    break;
                                case MemberLoad.eDirection.Y:
                                    DrawMemberLoad_Y_Negative_LOCAL(document, mload);
                                    break;
                                case MemberLoad.eDirection.Z:
                                    DrawMemberLoad_Z_Negative_LOCAL(document, mload);
                                    break;
                                case MemberLoad.eDirection.GX:
                                    DrawMemberLoad_X_Negative_GLOBAL(document, mload);
                                    break;
                                case MemberLoad.eDirection.GY:
                                    DrawMemberLoad_Y_Negative_GLOBAL(document, mload);
                                    break;
                                case MemberLoad.eDirection.GZ:
                                    DrawMemberLoad_Z_Negative_GLOBAL(document, mload);
                                    break;
                            }
                        }
                    }
                    else if (mload.LoadType == MemberLoad.eLoadType.Concentrate)
                    {
                        DrawMemberLoad_CONS(document, mload);
                    }
                    else if (mload.LoadType == MemberLoad.eLoadType.LINEAR)
                    {

                        switch (mload.Direction)
                        {

                            case MemberLoad.eDirection.X:
                                DrawMemberLoad_X_Negative_LINEAR(document, mload);
                                break;
                            case MemberLoad.eDirection.Y:
                                DrawMemberLoad_Y_Negative_LINEAR(document, mload);
                                break;
                            case MemberLoad.eDirection.Z:
                                DrawMemberLoad_Z_Negative_LINEAR(document, mload);
                                break;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            document.Redraw(true);


        }
        public void Delete_ASTRAMemberLoad(vdDocument document)
        {
            //ASTRAArrowLine

            for (int i = 0; i < document.ActiveLayOut.Entities.Count; i++)
            {
                if (document.ActiveLayOut.Entities[i].Layer.Name.Contains("MemberLoad") ||
                    document.ActiveLayOut.Entities[i].Layer.Name.Contains("Concentr"))
                {
                    document.ActiveLayOut.Entities[i].Deleted = true;
                    document.ActiveLayOut.Entities.RemoveAt(i);
                    i = -1;
                }
            }


            //ASTRAMemberLoad aline = document.ActiveLayOut.Entities[i] as ASTRAMemberLoad;
            //if (aline != null)
            //{
            //    aline.Deleted = true;
            //    document.ActiveLayOut.Entities.RemoveAt(i);
            //    i = -1;
            //}
            //else
            //{
            //    vdText tx = document.ActiveLayOut.Entities[i] as vdText;
            //    if ((tx != null) && tx.Layer.Name.Contains("MemberLoad"))
            //    {
            //        tx.Deleted = true;
            //        document.ActiveLayOut.Entities.RemoveAt(i);
            //        i = -1;
            //    }
            //    else
            //    {
            //        vdCircle cir = document.ActiveLayOut.Entities[i] as vdCircle;
            //        if ((cir != null))
            //        {
            //            cir.Deleted = true;
            //            document.ActiveLayOut.Entities.RemoveAt(i);
            //            i = -1;
            //        }
            //        else
            //        {
            //            vdRect rect = document.ActiveLayOut.Entities[i] as vdRect;
            //            if ((rect != null) && rect.Layer.Name.Contains("MemberLoad"))
            //            {
            //                rect.Deleted = true;
            //                document.ActiveLayOut.Entities.RemoveAt(i);
            //                i = -1;
            //            }
            //        }
            //        //if ((cir != null) && cir.Layer.Name.Contains("JointLoad"))
            //        //{
            //        //    cir.Deleted = true;
            //        //    document.ActiveLayOut.Entities.RemoveAt(i);
            //        //    i = -1;
            //        //}
            //    }
            //}
            //}
        }

        public void DrawMemberLoad(vdDocument document, int LoadCase, string loadtext)
        {
            
            Delete_ASTRAMemberLoad(document);

            List<int> mems = MyStrings.Get_Array_Intiger(loadtext);

            //return;

            membload_GLOBAL_Lay.Name = "MemberLoadGlobal";
            if (document.Layers.FindName("MemberLoadGlobal") == null)
            {
                membload_GLOBAL_Lay = new vdLayer();
                membload_GLOBAL_Lay.Name = "MemberLoadGlobal";
                membload_GLOBAL_Lay.SetUnRegisterDocument(document);
                membload_GLOBAL_Lay.setDocumentDefaults();
                membload_GLOBAL_Lay.PenColor = new vdColor(Color.Green);
                document.Layers.AddItem(membload_GLOBAL_Lay);
            }
            else
            {
                membload_GLOBAL_Lay = document.Layers.FindName("MemberLoadGlobal");
            }

            membload_LOCAL_Lay.Name = "MemberLoadLocal";
            if (document.Layers.FindName("MemberLoadLocal") == null)
            {
                membload_LOCAL_Lay = new vdLayer();
                membload_LOCAL_Lay.Name = "MemberLoadLocal";
                membload_LOCAL_Lay.SetUnRegisterDocument(document);
                membload_LOCAL_Lay.setDocumentDefaults();
                membload_LOCAL_Lay.PenColor = new vdColor(Color.Blue);
                document.Layers.AddItem(membload_LOCAL_Lay);
            }
            else
            {
                membload_LOCAL_Lay = document.Layers.FindName("MemberLoadLocal");
            }

            //membload_LOCAL_Lay = new vdLayer();
            //membload_LOCAL_Lay.Name = "MemberLoadLocal";
            //document.Layers.AddItem(membload_LOCAL_Lay);
            //membload_LOCAL_Lay.PenColor = new vdColor(Color.Blue);


            MemberLoadCollection mlc = new MemberLoadCollection();
            mlc.AddTxt(loadtext, LoadCase);

            foreach (MemberLoad mload in list)
            {

                if (mload.LoadCase != LoadCase || mload.D1 != mlc[0].D1 || mload.D2 != mlc[0].D2) continue;
                if (!mems.Contains(mload.Member.MemberNo)) continue;

                if (mload.LoadType == MemberLoad.eLoadType.UDL)
                {
                    if (mload.Value > 0)
                    {
                        switch (mload.Direction)
                        {
                            case MemberLoad.eDirection.X:
                                DrawMemberLoad_X_Positive_LOCAL(document, mload);
                                break;
                            case MemberLoad.eDirection.GX:
                                DrawMemberLoad_X_Positive_GLOBAL(document, mload);
                                break;
                            case MemberLoad.eDirection.GY:
                                DrawMemberLoad_Y_Positive_GLOBAL(document, mload);
                                break;
                            case MemberLoad.eDirection.Y:
                                DrawMemberLoad_Y_Positive_LOCAL(document, mload);
                                break;
                            case MemberLoad.eDirection.Z:
                                DrawMemberLoad_Z_Positive_LOCAL(document, mload);
                                break;
                            case MemberLoad.eDirection.GZ:
                                DrawMemberLoad_Z_Positive_GLOBAL(document, mload);
                                break;
                        }
                    }
                    else
                    {
                        switch (mload.Direction)
                        {
                            case MemberLoad.eDirection.X:
                                DrawMemberLoad_X_Negative_LOCAL(document, mload);
                                break;
                            case MemberLoad.eDirection.Y:
                                DrawMemberLoad_Y_Negative_LOCAL(document, mload);
                                break;
                            case MemberLoad.eDirection.Z:
                                DrawMemberLoad_Z_Negative_LOCAL(document, mload);
                                break;
                            case MemberLoad.eDirection.GX:
                                DrawMemberLoad_X_Negative_GLOBAL(document, mload);
                                break;
                            case MemberLoad.eDirection.GY:
                                DrawMemberLoad_Y_Negative_GLOBAL(document, mload);
                                break;
                            case MemberLoad.eDirection.GZ:
                                DrawMemberLoad_Z_Negative_GLOBAL(document, mload);
                                break;
                        }
                    }
                }
                else if (mload.LoadType == MemberLoad.eLoadType.Concentrate)
                {
                    DrawMemberLoad_CONS(document, mload);
                }
                else if (mload.LoadType == MemberLoad.eLoadType.LINEAR)
                {
                    switch (mload.Direction)
                    {
                        case MemberLoad.eDirection.X:
                            DrawMemberLoad_X_Negative_LINEAR(document, mload);
                            break;
                        case MemberLoad.eDirection.Y:
                            DrawMemberLoad_Y_Negative_LINEAR(document, mload);
                            break;
                        case MemberLoad.eDirection.Z:
                            DrawMemberLoad_Z_Negative_LINEAR(document, mload);
                            break;
                    }
                }
            }
            document.Redraw(true);
        }

        public void DrawMemberLoad(vdDocument document)
        {
            //document.Layers.AddItem(membloadLay);
            //membloadLay.PenColor = new vdColor(Color.Blue);

            //foreach (MemberLoad mload in list)
            //{
            //    DrawMemberLoad(document, mload);
            //}
            //document.Redraw(true);

            //membload_GLOBAL_Lay = new vdLayer();
            //membload_GLOBAL_Lay.Name = "MemberLoadGlobal";
            //document.Layers.AddItem(membload_GLOBAL_Lay);
            //membload_GLOBAL_Lay.PenColor = new vdColor(Color.Green);

            //membload_LOCAL_Lay = new vdLayer();
            //membload_LOCAL_Lay.Name = "MemberLoadLocal";
            //document.Layers.AddItem(membload_LOCAL_Lay);
            //membload_LOCAL_Lay.PenColor = new vdColor(Color.Blue);

            membload_GLOBAL_Lay.Name = "MemberLoadGlobal";
            if (document.Layers.FindName("MemberLoadGlobal") == null)
            {
                membload_GLOBAL_Lay = new vdLayer();
                membload_GLOBAL_Lay.Name = "MemberLoadGlobal";
                membload_GLOBAL_Lay.SetUnRegisterDocument(document);
                membload_GLOBAL_Lay.setDocumentDefaults();
                membload_GLOBAL_Lay.PenColor = new vdColor(Color.Green);
                document.Layers.AddItem(membload_GLOBAL_Lay);
            }
            else
            {
                membload_GLOBAL_Lay = document.Layers.FindName("MemberLoadGlobal");
            }

            membload_LOCAL_Lay.Name = "MemberLoadLocal";
            if (document.Layers.FindName("MemberLoadLocal") == null)
            {
                membload_LOCAL_Lay = new vdLayer();
                membload_LOCAL_Lay.Name = "MemberLoadLocal";
                membload_LOCAL_Lay.SetUnRegisterDocument(document);
                membload_LOCAL_Lay.setDocumentDefaults();
                membload_LOCAL_Lay.PenColor = new vdColor(Color.Blue);
                document.Layers.AddItem(membload_LOCAL_Lay);
            }
            else
            {
                membload_LOCAL_Lay = document.Layers.FindName("MemberLoadLocal");
            }



            foreach (MemberLoad mload in list)
            {
                if (mload.Value > 0)
                {
                    switch (mload.Direction)
                    {
                        case MemberLoad.eDirection.X:
                            DrawMemberLoad_X_Positive_LOCAL(document, mload);
                            break;
                        case MemberLoad.eDirection.GX:
                            DrawMemberLoad_X_Positive_GLOBAL(document, mload);
                            break;
                        case MemberLoad.eDirection.GY:
                            DrawMemberLoad_Y_Positive_GLOBAL(document, mload);
                            break;
                        case MemberLoad.eDirection.Y:
                            DrawMemberLoad_Y_Positive_LOCAL(document, mload);
                            break;
                        case MemberLoad.eDirection.Z:
                            DrawMemberLoad_Z_Positive_LOCAL(document, mload);
                            break;
                        case MemberLoad.eDirection.GZ:
                            DrawMemberLoad_Z_Positive_GLOBAL(document, mload);
                            break;
                    }
                }
                else
                {
                    switch (mload.Direction)
                    {
                        case MemberLoad.eDirection.X:
                            DrawMemberLoad_X_Negative_LOCAL(document, mload);
                            break;
                        case MemberLoad.eDirection.Y:
                            DrawMemberLoad_Y_Negative_LOCAL(document, mload);
                            break;
                        case MemberLoad.eDirection.Z:
                            DrawMemberLoad_Z_Negative_LOCAL(document, mload);
                            break;
                        case MemberLoad.eDirection.GX:
                            DrawMemberLoad_X_Negative_GLOBAL(document, mload);
                            break;
                        case MemberLoad.eDirection.GY:
                            DrawMemberLoad_Y_Negative_GLOBAL(document, mload);
                            break;
                        case MemberLoad.eDirection.GZ:
                            DrawMemberLoad_Z_Negative_GLOBAL(document, mload);
                            break;
                    }
                }
            }
            document.Redraw(true);
        }

        #region BEARING
        public gPoint Bearing_XY(gPoint startPoint, gPoint endPoint, double ho)
        {
            double x1 = startPoint.x;
            double y1 = startPoint.y;
            double x2 = endPoint.x;
            double y2 = endPoint.y;

            double th, r9, brg = 0.0d;
            double ox, oy;

            r9 = 57.29577951;


            if (x2 > x1 && y2 == y1)
                brg = 90;
            else if (x2 < x1 && y2 == y1)
                brg = 270;
            else if (x2 == x1 && y2 > y1)
                brg = 0;
            else if (x2 == x1 && y2 < y1)
                brg = 180;
            else
            {
                th = Math.Atan((y2 - y1) / (x2 - x1));
                th = Math.Abs(th) * r9;

                if (x2 > x1 && y2 > y1)
                    brg = 90 - th;
                else if (x2 > x1 && y2 < y1)
                    brg = 90 + th;
                else if (x2 < x1 && y2 < y1)
                    brg = 270 - th;
                else if (x2 < x1 && y2 > y1)
                    brg = 270 + th;
            }
            ox = x2 + ho * Math.Cos((brg) / r9);
            oy = y2 - ho * Math.Sin((brg) / r9);

            return new gPoint(ox, oy, endPoint.z);
        }
        public gPoint Bearing_XZ(gPoint startPoint, gPoint endPoint, double ho)
        {
            double x1 = startPoint.x;
            double y1 = startPoint.z;
            double x2 = endPoint.x;
            double y2 = endPoint.z;

            double th, r9, brg = 0.0d;
            double ox, oy;

            r9 = 57.29577951;


            if (x2 > x1 && y2 == y1)
                brg = 90;
            else if (x2 < x1 && y2 == y1)
                brg = 270;
            else if (x2 == x1 && y2 > y1)
                brg = 0;
            else if (x2 == x1 && y2 < y1)
                brg = 180;
            else
            {
                th = Math.Atan((y2 - y1) / (x2 - x1));
                th = Math.Abs(th) * r9;

                if (x2 > x1 && y2 > y1)
                    brg = 90 - th;
                else if (x2 > x1 && y2 < y1)
                    brg = 90 + th;
                else if (x2 < x1 && y2 < y1)
                    brg = 270 - th;
                else if (x2 < x1 && y2 > y1)
                    brg = 270 + th;
            }
            ox = x2 + ho * Math.Cos((brg) / r9);
            oy = y2 - ho * Math.Sin((brg) / r9);

            return new gPoint(ox, endPoint.y, oy);
        }
        public gPoint Bearing_YZ(gPoint startPoint, gPoint endPoint, double ho)
        {
            double x1 = startPoint.y;
            double y1 = startPoint.z;
            double x2 = endPoint.y;
            double y2 = endPoint.z;

            double th, r9, brg = 0.0d;
            double ox, oy;

            r9 = 57.29577951;


            if (x2 > x1 && y2 == y1)
                brg = 90;
            else if (x2 < x1 && y2 == y1)
                brg = 270;
            else if (x2 == x1 && y2 > y1)
                brg = 0;
            else if (x2 == x1 && y2 < y1)
                brg = 180;
            else
            {
                th = Math.Atan((y2 - y1) / (x2 - x1));
                th = Math.Abs(th) * r9;

                if (x2 > x1 && y2 > y1)
                    brg = 90 - th;
                else if (x2 > x1 && y2 < y1)
                    brg = 90 + th;
                else if (x2 < x1 && y2 < y1)
                    brg = 270 - th;
                else if (x2 < x1 && y2 > y1)
                    brg = 270 + th;
            }
            ox = x2 + ho * Math.Cos((brg) / r9);
            oy = y2 - ho * Math.Sin((brg) / r9);

            return new gPoint(endPoint.z, oy,ox);
        }

        public gPoint Bearing(double x1, double y1, double x2, double y2, double ho)
        {
            double th, r9, brg = 0.0d;
            double ox, oy;
            
            r9 = 57.29577951;


            if (x2 > x1 && y2 == y1)
                brg = 90;
            else if (x2 < x1 && y2 == y1)
                brg = 270;
            else if (x2 == x1 && y2 > y1)
                brg = 0;
            else if (x2 == x1 && y2 < y1)
                brg = 180;
            else
            {
                th = Math.Atan((y2 - y1) / (x2 - x1));
                th = Math.Abs(th) * r9;

                if (x2 > x1 && y2 > y1)
                    brg = 90 - th;
                else if (x2 > x1 && y2 < y1)
                    brg = 90 + th;
                else if (x2 < x1 && y2 < y1)
                    brg = 270 - th;
                else if (x2 < x1 && y2 > y1)
                    brg = 270 + th;
            }
            ox = x2 + ho * Math.Cos((brg) / r9);
            oy = y2 - ho * Math.Sin((brg) / r9);

            return new gPoint(ox, oy);

        }
        #endregion

        #region Local Load

        // Local X
        public void DrawMemberLoad_X_Positive_LOCAL(vdDocument document, MemberLoad mLoad)
        {

            gPoint EPT, SPT;



            SPT = new gPoint(mLoad.Member.StartNode.Point);
            EPT = new gPoint(mLoad.Member.EndNode.Point);


            vdLine ln = new vdLine(SPT, EPT);

            SPT = ln.getPointAtDist(mLoad.D1);
            if (mLoad.D2 != 0.0)
                EPT = ln.getPointAtDist(mLoad.D1 + mLoad.D2);




            vdLine line2 = new vdLine();
            vdLine line1 = new vdLine();


            line2.SetUnRegisterDocument(document);
            line2.setDocumentDefaults();
            line2.StartPoint = SPT;
            line2.EndPoint = EPT;
            line2.Layer = membload_LOCAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line2);

            radFac = Math.PI / 180;
            xtemp = ytemp = d1 = d2 = d3 = d4 = mx = my = theta = 0.0d;

            line1.SetUnRegisterDocument(document);
            line1.setDocumentDefaults();




            double W1 = Math.Abs(mLoad.Value);

            while (Math.Abs(W1) > 2)
                W1 = W1 / 2;

            //while (Math.Abs(W1) < 1)
                //W1 = W1 * 10;

            W1 = W1 * 0.50;



            if ((EPT.y - SPT.y) > 0)
            {
                line1.EndPoint = Bearing_YZ(SPT, EPT, W1);
                line1.StartPoint = Bearing_YZ(EPT, SPT, -W1);
            }
            else
            {
                line1.EndPoint = Bearing_YZ(SPT, EPT, -W1);
                line1.StartPoint = Bearing_YZ(EPT, SPT, W1);
            }
            line2.Layer = membload_LOCAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line1);

            ASTRAMemberLoad aline = new ASTRAMemberLoad();
            document.ActionLayout.Entities.AddItem(aline);
            document.UndoHistory.PushEnable(false);
            aline.InitializeProperties();
            aline.setDocumentDefaults();
            aline.StartPoint = line1.StartPoint;
            aline.EndPoint = line2.StartPoint;
            aline.arrowSize = 0.1;
            aline.Layer = membload_LOCAL_Lay;
            aline.Transformby(document.User2WorldMatrix);
            document.UndoHistory.PopEnable();
            document.ActionDrawFigure(aline);


            double ll = ((line2.Length() / 8.0d) / line1.Length());
            double sx, sy, sz;
            double ex, ey, ez;
            sx = sy = sz = 0.0d;
            for (int j = 1; j <= 8; j++)
            {
                ASTRAMemberLoad aline1 = new ASTRAMemberLoad();
                document.ActionLayout.Entities.AddItem(aline1);
                document.UndoHistory.PushEnable(false);
                aline1.InitializeProperties();
                aline1.setDocumentDefaults();
                aline1.arrowSize = 0.1;

                sx = (j * ll) * (line1.EndPoint.x - line1.StartPoint.x) + line1.StartPoint.x;
                sy = (j * ll) * (line1.EndPoint.y - line1.StartPoint.y) + line1.StartPoint.y;
                sz = (j * ll) * (line1.EndPoint.z - line2.StartPoint.z) + line1.StartPoint.z;

                aline1.StartPoint = new gPoint(sx, sy, sz);

                ex = (j * ll) * (line2.EndPoint.x - line2.StartPoint.x) + line2.StartPoint.x;
                ey = (j * ll) * (line2.EndPoint.y - line2.StartPoint.y) + line2.StartPoint.y;
                ez = (j * ll) * (line2.EndPoint.z - line1.StartPoint.z) + line2.StartPoint.z;

                aline1.EndPoint = new gPoint(ex, ey, ez);
                aline1.Transformby(document.User2WorldMatrix);
                document.UndoHistory.PopEnable();
                document.ActionDrawFigure(aline);
                aline1.Layer = membload_LOCAL_Lay;
            }
            vdText txt = new vdText();
            txt.SetUnRegisterDocument(document);
            txt.setDocumentDefaults();
            txt.InsertionPoint = (line2.StartPoint + line2.EndPoint + line1.StartPoint + line1.EndPoint) / 4;
            txt.Layer = membload_LOCAL_Lay;
            txt.Height = 0.5d;
            txt.PenColor = new vdColor(Color.DarkGray);
            txt.TextString = Math.Abs(mLoad.Value).ToString();
            //document.ActiveLayOut.Entities.AddItem(txt);
        }
        public void DrawMemberLoad_X_Negative_LOCAL(vdDocument document, MemberLoad mLoad)
        {

            gPoint EPT, SPT;


            SPT = new gPoint(mLoad.Member.StartNode.Point);
            EPT = new gPoint(mLoad.Member.EndNode.Point);


            vdLine ln = new vdLine(SPT, EPT);

            SPT = ln.getPointAtDist(mLoad.D1);
            if (mLoad.D2 != 0.0)
                EPT = ln.getPointAtDist(mLoad.D1 + mLoad.D2);



            vdLine line2 = new vdLine();
            vdLine line1 = new vdLine();


            line1.SetUnRegisterDocument(document);
            line1.setDocumentDefaults();
            line1.StartPoint = SPT;
            line1.EndPoint = EPT;
            line1.Layer = membload_LOCAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line1);

            radFac = Math.PI / 180;
            xtemp = ytemp = d1 = d2 = d3 = d4 = mx = my = theta = 0.0d;


            line2.SetUnRegisterDocument(document);
            line2.setDocumentDefaults();



            double W1 = Math.Abs(mLoad.Value);

            while (Math.Abs(W1) > 2)
                W1 = W1 / 2;

            //while (Math.Abs(W1) < 1)
                //W1 = W1 * 10;

            W1 = W1 * 0.50;



            if ((EPT.y - SPT.y) > 0)
            {
                line2.EndPoint = Bearing_YZ(SPT, EPT, -W1);
                line2.StartPoint = Bearing_YZ(EPT, SPT, W1);
            }
            else
            {
                line2.EndPoint = Bearing_YZ(SPT, EPT, W1);
                line2.StartPoint = Bearing_YZ(EPT, SPT, -W1);
            }
            //line2.StartPoint = Bearing(SPT.x, SPT.y, EPT.x, EPT.y, -1);
            //line2.EndPoint = Bearing(EPT.x, EPT.y, SPT.x, SPT.y, 1);


            //line2.EndPoint = new gPoint(EPT.x, EPT.y + 1, EPT.z);
            line2.Layer = membload_LOCAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line2);

            ASTRAMemberLoad aline = new ASTRAMemberLoad();
            document.ActionLayout.Entities.AddItem(aline);
            document.UndoHistory.PushEnable(false);
            aline.InitializeProperties();
            aline.setDocumentDefaults();
            aline.StartPoint = line2.StartPoint;
            aline.EndPoint = line1.StartPoint;
            aline.arrowSize = 0.3;
            aline.Layer = membload_LOCAL_Lay;
            aline.Transformby(document.User2WorldMatrix);
            document.UndoHistory.PopEnable();
            document.ActionDrawFigure(aline);


            double ll = ((line1.Length() / 8.0d) / line1.Length());
            double sx, sy, sz;
            double ex, ey, ez;
            sx = sy = sz = 0.0d;
            for (int j = 1; j <= 8; j++)
            {
                ASTRAMemberLoad aline1 = new ASTRAMemberLoad();
                document.ActionLayout.Entities.AddItem(aline1);
                document.UndoHistory.PushEnable(false);
                aline1.InitializeProperties();
                aline1.setDocumentDefaults();
                aline1.arrowSize = 0.3;

                sx = (j * ll) * (line2.EndPoint.x - line2.StartPoint.x) + line2.StartPoint.x;
                sy = (j * ll) * (line2.EndPoint.y - line2.StartPoint.y) + line2.StartPoint.y;
                sz = (j * ll) * (line2.EndPoint.z - line2.StartPoint.z) + line2.StartPoint.z;

                aline1.StartPoint = new gPoint(sx, sy, sz);

                ex = (j * ll) * (line1.EndPoint.x - line1.StartPoint.x) + line1.StartPoint.x;
                ey = (j * ll) * (line1.EndPoint.y - line1.StartPoint.y) + line1.StartPoint.y;
                ez = (j * ll) * (line1.EndPoint.z - line1.StartPoint.z) + line1.StartPoint.z;

                aline1.EndPoint = new gPoint(ex, ey, ez);
                aline1.Transformby(document.User2WorldMatrix);
                document.UndoHistory.PopEnable();
                document.ActionDrawFigure(aline);
                aline1.Layer = membload_LOCAL_Lay;
            }
            vdText txt = new vdText();
            txt.SetUnRegisterDocument(document);
            txt.setDocumentDefaults();
            txt.InsertionPoint = (line1.StartPoint + line1.EndPoint + line2.StartPoint + line2.EndPoint) / 4;
            txt.Layer = membload_LOCAL_Lay;
            txt.Height = 0.5d;
            txt.PenColor = new vdColor(Color.DarkGray);
            txt.TextString = Math.Abs(mLoad.Value).ToString();
            //document.ActiveLayOut.Entities.AddItem(txt);
        }

        // Local Y
        public void DrawMemberLoad_Y_Positive_LOCAL(vdDocument document, MemberLoad mLoad)
        {

            gPoint EPT, SPT;


            SPT = new gPoint(mLoad.Member.StartNode.Point);
            EPT = new gPoint(mLoad.Member.EndNode.Point);


            vdLine ln = new vdLine(SPT, EPT);

            SPT = ln.getPointAtDist(mLoad.D1);
            if (mLoad.D2 != 0.0)
                EPT = ln.getPointAtDist(mLoad.D1 + mLoad.D2);



            vdLine line2 = new vdLine();
            vdLine line1 = new vdLine();


            line2.SetUnRegisterDocument(document);
            line2.setDocumentDefaults();
            line2.StartPoint = SPT;
            line2.EndPoint = EPT;
            line2.Layer = membload_LOCAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line2);

            radFac = Math.PI / 180;
            xtemp = ytemp = d1 = d2 = d3 = d4 = mx = my = theta = 0.0d;


            //d2 = line1.Length();
            //theta = Math.Atan(1.0 / line1.Length())/radFac;
            //theta = Math.Atan((EPT.y - SPT.y) / (EPT.x - SPT.x));

            xtemp = SPT.x * Math.Cos(theta) - SPT.y * Math.Sin(theta);
            ytemp = SPT.x * Math.Sin(theta) + SPT.y * Math.Cos(theta);


            d1 = 1.0d / Math.Sin(theta);

            line1.SetUnRegisterDocument(document);
            line1.setDocumentDefaults();


            double W1 = Math.Abs(mLoad.Value);

            while (Math.Abs(W1) > 2)
                W1 = W1 / 2;

            //while (Math.Abs(W1) < 1)
                //W1 = W1 * 10;

            W1 = W1 * 0.50;


            if ((EPT.x - SPT.x) > 0)
            {
                line1.EndPoint = Bearing_XY(SPT, EPT, W1);
                line1.StartPoint = Bearing_XY(EPT, SPT, -W1);
            }
            else
            {
                line1.EndPoint = Bearing_XY(SPT, EPT, -W1);
                line1.StartPoint = Bearing_XY(EPT, SPT, W1);
            }
            //line2.StartPoint = Bearing(SPT.x, SPT.y, EPT.x, EPT.y, -1);
            //line2.EndPoint = Bearing(EPT.x, EPT.y, SPT.x, SPT.y, 1);


            //line2.EndPoint = new gPoint(EPT.x, EPT.y + 1, EPT.z);
            line1.Layer = membload_LOCAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line1);

            ASTRAMemberLoad aline = new ASTRAMemberLoad();
            document.ActionLayout.Entities.AddItem(aline);
            document.UndoHistory.PushEnable(false);
            aline.InitializeProperties();
            aline.setDocumentDefaults();
            aline.StartPoint = line1.StartPoint;
            aline.EndPoint = line2.StartPoint;
            aline.arrowSize = 0.1;
            aline.Layer = membload_LOCAL_Lay;
            aline.Transformby(document.User2WorldMatrix);
            document.UndoHistory.PopEnable();
            document.ActionDrawFigure(aline);


            double ll = ((line2.Length() / 8.0d) / line1.Length());
            double sx, sy, sz;
            double ex, ey, ez;
            sx = sy = sz = 0.0d;
            for (int j = 1; j <= 8; j++)
            {
                ASTRAMemberLoad aline1 = new ASTRAMemberLoad();
                document.ActionLayout.Entities.AddItem(aline1);
                document.UndoHistory.PushEnable(false);
                aline1.InitializeProperties();
                aline1.setDocumentDefaults();
                aline1.arrowSize = 0.1;

                sx = (j * ll) * (line1.EndPoint.x - line1.StartPoint.x) + line1.StartPoint.x;
                sy = (j * ll) * (line1.EndPoint.y - line1.StartPoint.y) + line1.StartPoint.y;
                sz = (j * ll) * (line1.EndPoint.z - line2.StartPoint.z) + line1.StartPoint.z;

                aline1.StartPoint = new gPoint(sx, sy, sz);

                ex = (j * ll) * (line2.EndPoint.x - line2.StartPoint.x) + line2.StartPoint.x;
                ey = (j * ll) * (line2.EndPoint.y - line2.StartPoint.y) + line2.StartPoint.y;
                ez = (j * ll) * (line2.EndPoint.z - line1.StartPoint.z) + line2.StartPoint.z;

                aline1.EndPoint = new gPoint(ex, ey, ez);
                aline1.Transformby(document.User2WorldMatrix);
                document.UndoHistory.PopEnable();
                document.ActionDrawFigure(aline);
                aline1.Layer = membload_LOCAL_Lay;
            }
            vdText txt = new vdText();
            txt.SetUnRegisterDocument(document);
            txt.setDocumentDefaults();
            txt.InsertionPoint = (line2.StartPoint + line2.EndPoint + line1.StartPoint + line1.EndPoint) / 4;
            txt.Layer = membload_LOCAL_Lay;
            txt.Height = 0.5d;
            txt.PenColor = new vdColor(Color.DarkGray);
            txt.TextString = Math.Abs(mLoad.Value).ToString();
            //document.ActiveLayOut.Entities.AddItem(txt);
        }
        public void DrawMemberLoad_Y_Negative_LOCAL(vdDocument document, MemberLoad mLoad)
        {

            gPoint EPT, SPT;


            SPT = new gPoint(mLoad.Member.StartNode.Point);
            EPT = new gPoint(mLoad.Member.EndNode.Point);


            vdLine ln = new vdLine(SPT, EPT);

            SPT = ln.getPointAtDist(mLoad.D1);
            if (mLoad.D2 != 0.0)
                EPT = ln.getPointAtDist(mLoad.D1 + mLoad.D2);



            vdLine line2 = new vdLine();
            vdLine line1 = new vdLine();


            line1.SetUnRegisterDocument(document);
            line1.setDocumentDefaults();
            line1.StartPoint = SPT;
            line1.EndPoint = EPT;
            line1.Layer = membload_LOCAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line1);

            radFac = Math.PI / 180;
            xtemp = ytemp = d1 = d2 = d3 = d4 = mx = my = theta = 0.0d;

            xtemp = SPT.x * Math.Cos(theta) - SPT.y * Math.Sin(theta);
            ytemp = SPT.x * Math.Sin(theta) + SPT.y * Math.Cos(theta);


            d1 = 1.0d / Math.Sin(theta);

            line2.SetUnRegisterDocument(document);
            line2.setDocumentDefaults();


            double W1 = Math.Abs(mLoad.Value);

            //while (Math.Abs(W1) > 2)
            //    W1 = W1 / 2;

            //while (Math.Abs(W1) < 1)
                //W1 = W1 * 10;
            W1 = 1.50;

            W1 = W1 * 0.50;

            if ((EPT.x - SPT.x) > 0)
            {
                line2.EndPoint = Bearing_XY(SPT, EPT, -W1);
                line2.StartPoint = Bearing_XY(EPT, SPT, W1);
            }
            else
            {
                line2.EndPoint = Bearing_XY(SPT, EPT, W1);
                line2.StartPoint = Bearing_XY(EPT, SPT, -W1);
            }
            line2.Layer = membload_LOCAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line2);

            ASTRAMemberLoad aline = new ASTRAMemberLoad();
            document.ActionLayout.Entities.AddItem(aline);
            document.UndoHistory.PushEnable(false);
            aline.InitializeProperties();
            aline.setDocumentDefaults();
            aline.StartPoint = line2.StartPoint;
            aline.EndPoint = line1.StartPoint;
            aline.arrowSize = 0.1;
            aline.Layer = membload_LOCAL_Lay;
            aline.Transformby(document.User2WorldMatrix);
            document.UndoHistory.PopEnable();
            document.ActionDrawFigure(aline);


            double ll = ((line1.Length() / 8.0d) / line1.Length());
            double sx, sy, sz;
            double ex, ey, ez;
            sx = sy = sz = 0.0d;
            for (int j = 1; j <= 8; j++)
            {
                ASTRAMemberLoad aline1 = new ASTRAMemberLoad();
                document.ActionLayout.Entities.AddItem(aline1);
                document.UndoHistory.PushEnable(false);
                aline1.InitializeProperties();
                aline1.setDocumentDefaults();
                aline1.arrowSize = 0.1;

                sx = (j * ll) * (line2.EndPoint.x - line2.StartPoint.x) + line2.StartPoint.x;
                sy = (j * ll) * (line2.EndPoint.y - line2.StartPoint.y) + line2.StartPoint.y;
                sz = (j * ll) * (line2.EndPoint.z - line2.StartPoint.z) + line2.StartPoint.z;

                aline1.StartPoint = new gPoint(sx, sy, sz);

                ex = (j * ll) * (line1.EndPoint.x - line1.StartPoint.x) + line1.StartPoint.x;
                ey = (j * ll) * (line1.EndPoint.y - line1.StartPoint.y) + line1.StartPoint.y;
                ez = (j * ll) * (line1.EndPoint.z - line1.StartPoint.z) + line1.StartPoint.z;

                aline1.EndPoint = new gPoint(ex, ey, ez);
                aline1.Transformby(document.User2WorldMatrix);
                document.UndoHistory.PopEnable();
                document.ActionDrawFigure(aline);
                aline1.Layer = membload_LOCAL_Lay;
            }
            vdText txt = new vdText();
            txt.SetUnRegisterDocument(document);
            txt.setDocumentDefaults();
            txt.InsertionPoint = (line1.StartPoint + line1.EndPoint + line2.StartPoint + line2.EndPoint) / 4;
            txt.Layer = membload_LOCAL_Lay;
            txt.Height = 0.5d;
            txt.PenColor = new vdColor(Color.DarkGray);
            txt.TextString = Math.Abs(mLoad.Value).ToString();
            //document.ActiveLayOut.Entities.AddItem(txt);
        }

        // Local Z
        public void DrawMemberLoad_Z_Positive_LOCAL(vdDocument document, MemberLoad mLoad)
        {
            gPoint EPT, SPT;



            SPT = new gPoint(mLoad.Member.StartNode.Point);
            EPT = new gPoint(mLoad.Member.EndNode.Point);


            vdLine ln = new vdLine(SPT, EPT);

            SPT = ln.getPointAtDist(mLoad.D1);
            if (mLoad.D2 != 0.0)
                EPT = ln.getPointAtDist(mLoad.D1 + mLoad.D2);



            vdLine line2 = new vdLine();
            vdLine line1 = new vdLine();


            line2.SetUnRegisterDocument(document);
            line2.setDocumentDefaults();
            line2.StartPoint = SPT;
            line2.EndPoint = EPT;
            line2.Layer = membload_LOCAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line2);

            radFac = Math.PI / 180;
            xtemp = ytemp = d1 = d2 = d3 = d4 = mx = my = theta = 0.0d;


            ////d2 = line1.Length();
            ////theta = Math.Atan(1.0 / line1.Length())/radFac;
            ////theta = Math.Atan((EPT.y - SPT.y) / (EPT.x - SPT.x));

            //xtemp = SPT.x * Math.Cos(theta) - SPT.y * Math.Sin(theta);
            //ytemp = SPT.x * Math.Sin(theta) + SPT.y * Math.Cos(theta);


            //d1 = 1.0d / Math.Sin(theta);

            line1.SetUnRegisterDocument(document);
            line1.setDocumentDefaults();

            double W1 = Math.Abs(mLoad.Value);

            while (Math.Abs(W1) > 2)
                W1 = W1 / 2;

            //while (Math.Abs(W1) < 1)
                //W1 = W1 * 10;

            W1 = W1 * 0.50;


            if ((EPT.x - SPT.x) > 0)
            {


                line1.EndPoint = Bearing_XZ(SPT, EPT, W1);
                line1.StartPoint = Bearing_XZ(EPT, SPT, -W1);

            }
            else
            {
                line1.EndPoint = Bearing_XZ(SPT, EPT, -W1);
                line1.StartPoint = Bearing_XZ(EPT, SPT, W1);
            }
            //line2.StartPoint = Bearing(SPT.x, SPT.y, EPT.x, EPT.y, -1);
            //line2.EndPoint = Bearing(EPT.x, EPT.y, SPT.x, SPT.y, 1);


            //line2.EndPoint = new gPoint(EPT.x, EPT.y + 1, EPT.z);
            line1.Layer = membload_LOCAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line1);

            ASTRAMemberLoad aline = new ASTRAMemberLoad();
            document.ActionLayout.Entities.AddItem(aline);
            document.UndoHistory.PushEnable(false);
            aline.InitializeProperties();
            aline.setDocumentDefaults();
            aline.StartPoint = line1.StartPoint;
            aline.EndPoint = line2.StartPoint;
            aline.arrowSize = 0.3;
            aline.Layer = membload_LOCAL_Lay;
            aline.Transformby(document.User2WorldMatrix);
            document.UndoHistory.PopEnable();
            document.ActionDrawFigure(aline);


            double ll = ((line2.Length() / 8.0d) / line2.Length());
            double sx, sy, sz;
            double ex, ey, ez;
            sx = sy = sz = 0.0d;
            for (int j = 1; j <= 8; j++)
            {
                ASTRAMemberLoad aline1 = new ASTRAMemberLoad();
                document.ActionLayout.Entities.AddItem(aline1);
                document.UndoHistory.PushEnable(false);
                aline1.InitializeProperties();
                aline1.setDocumentDefaults();
                aline1.arrowSize = 0.3;

                sx = (j * ll) * (line1.EndPoint.x - line1.StartPoint.x) + line1.StartPoint.x;
                sy = (j * ll) * (line1.EndPoint.y - line1.StartPoint.y) + line1.StartPoint.y;
                sz = (j * ll) * (line1.EndPoint.z - line1.StartPoint.z) + line1.StartPoint.z;

                aline1.StartPoint = new gPoint(sx, sy, sz);

                ex = (j * ll) * (line2.EndPoint.x - line2.StartPoint.x) + line2.StartPoint.x;
                ey = (j * ll) * (line2.EndPoint.y - line2.StartPoint.y) + line2.StartPoint.y;
                ez = (j * ll) * (line2.EndPoint.z - line2.StartPoint.z) + line2.StartPoint.z;

                aline1.EndPoint = new gPoint(ex, ey, ez);
                aline1.Transformby(document.User2WorldMatrix);
                document.UndoHistory.PopEnable();
                document.ActionDrawFigure(aline);
                aline1.Layer = membload_LOCAL_Lay;
            }
            vdText txt = new vdText();
            txt.SetUnRegisterDocument(document);
            txt.setDocumentDefaults();
            txt.InsertionPoint = (line1.StartPoint + line1.EndPoint + line2.StartPoint + line2.EndPoint) / 4;
            txt.Layer = membload_LOCAL_Lay;
            txt.Height = 0.5d;
            txt.PenColor = new vdColor(Color.DarkGray);
            txt.TextString = Math.Abs(mLoad.Value).ToString();
            //document.ActiveLayOut.Entities.AddItem(txt);
        }
        public void DrawMemberLoad_Z_Negative_LOCAL(vdDocument document, MemberLoad mLoad)
        {
            gPoint EPT, SPT;

            SPT = new gPoint(mLoad.Member.StartNode.Point);
            EPT = new gPoint(mLoad.Member.EndNode.Point);

            vdLine ln = new vdLine(SPT, EPT);

            SPT = ln.getPointAtDist(mLoad.D1);
            if (mLoad.D2 != 0.0)
                EPT = ln.getPointAtDist(mLoad.D1 + mLoad.D2);


            vdLine line2 = new vdLine();
            vdLine line1 = new vdLine();


            line1.SetUnRegisterDocument(document);
            line1.setDocumentDefaults();
            line1.StartPoint = SPT;
            line1.EndPoint = EPT;
            line1.Layer = membload_LOCAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line1);

            radFac = Math.PI / 180;
            xtemp = ytemp = d1 = d2 = d3 = d4 = mx = my = theta = 0.0d;


            //d2 = line1.Length();
            //theta = Math.Atan(1.0 / line1.Length())/radFac;
            //theta = Math.Atan((EPT.y - SPT.y) / (EPT.x - SPT.x));

            xtemp = SPT.x * Math.Cos(theta) - SPT.y * Math.Sin(theta);
            ytemp = SPT.x * Math.Sin(theta) + SPT.y * Math.Cos(theta);


            d1 = 1.0d / Math.Sin(theta);

            line2.SetUnRegisterDocument(document);
            line2.setDocumentDefaults();


            double W1 = Math.Abs(mLoad.Value);

            while (Math.Abs(W1) > 2)
                W1 = W1 / 2;

            //while (Math.Abs(W1) < 1)
                //W1 = W1 * 10;

            W1 = W1 * 0.50;


            if ((EPT.x - SPT.x) > 0)
            {


                line2.EndPoint = Bearing_XZ(SPT, EPT, -W1);
                line2.StartPoint = Bearing_XZ(EPT, SPT, W1);

            }
            else
            {
                line2.EndPoint = Bearing_XZ(SPT, EPT, W1);
                line2.StartPoint = Bearing_XZ(EPT, SPT, -W1);
            }
            //line2.StartPoint = Bearing(SPT.x, SPT.y, EPT.x, EPT.y, -1);
            //line2.EndPoint = Bearing(EPT.x, EPT.y, SPT.x, SPT.y, 1);


            //line2.EndPoint = new gPoint(EPT.x, EPT.y + 1, EPT.z);
            line2.Layer = membload_LOCAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line2);

            ASTRAMemberLoad aline = new ASTRAMemberLoad();
            document.ActionLayout.Entities.AddItem(aline);
            document.UndoHistory.PushEnable(false);
            aline.InitializeProperties();
            aline.setDocumentDefaults();
            aline.StartPoint = line2.StartPoint;
            aline.EndPoint = line1.StartPoint;
            aline.arrowSize = 0.1;
            aline.Layer = membload_LOCAL_Lay;
            aline.Transformby(document.User2WorldMatrix);
            document.UndoHistory.PopEnable();
            document.ActionDrawFigure(aline);


            double ll = ((line1.Length() / 8.0d) / line1.Length());
            double sx, sy, sz;
            double ex, ey, ez;
            sx = sy = sz = 0.0d;
            for (int j = 1; j <= 8; j++)
            {
                ASTRAMemberLoad aline1 = new ASTRAMemberLoad();
                document.ActionLayout.Entities.AddItem(aline1);
                document.UndoHistory.PushEnable(false);
                aline1.InitializeProperties();
                aline1.setDocumentDefaults();
                aline1.arrowSize = 0.1;

                sx = (j * ll) * (line2.EndPoint.x - line2.StartPoint.x) + line2.StartPoint.x;
                sy = (j * ll) * (line2.EndPoint.y - line2.StartPoint.y) + line2.StartPoint.y;
                sz = (j * ll) * (line2.EndPoint.z - line2.StartPoint.z) + line2.StartPoint.z;

                aline1.StartPoint = new gPoint(sx, sy, sz);

                ex = (j * ll) * (line1.EndPoint.x - line1.StartPoint.x) + line1.StartPoint.x;
                ey = (j * ll) * (line1.EndPoint.y - line1.StartPoint.y) + line1.StartPoint.y;
                ez = (j * ll) * (line1.EndPoint.z - line1.StartPoint.z) + line1.StartPoint.z;

                aline1.EndPoint = new gPoint(ex, ey, ez);
                aline1.Transformby(document.User2WorldMatrix);
                document.UndoHistory.PopEnable();
                document.ActionDrawFigure(aline);
                aline1.Layer = membload_LOCAL_Lay;
            }
            vdText txt = new vdText();
            txt.SetUnRegisterDocument(document);
            txt.setDocumentDefaults();
            txt.InsertionPoint = (line1.StartPoint + line1.EndPoint + line2.StartPoint + line2.EndPoint) / 4;
            txt.Layer = membload_LOCAL_Lay;
            txt.Height = 0.5d;
            txt.PenColor = new vdColor(Color.DarkGray);
            txt.TextString = Math.Abs(mLoad.Value).ToString();
            //document.ActiveLayOut.Entities.AddItem(txt);
        }

       
        #endregion


        #region Linear Local Load

        // Local X
        public void DrawMemberLoad_X_Negative_LINEAR(vdDocument document, MemberLoad mLoad)
        {

            gPoint EPT, SPT;

            SPT = mLoad.Member.StartNode.Point;
            EPT = mLoad.Member.EndNode.Point;


            vdLine line2 = new vdLine();
            vdLine line1 = new vdLine();


            line1.SetUnRegisterDocument(document);
            line1.setDocumentDefaults();
            line1.StartPoint = SPT;
            line1.EndPoint = EPT;
            line1.Layer = membload_LOCAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line1);

            radFac = Math.PI / 180;
            xtemp = ytemp = d1 = d2 = d3 = d4 = mx = my = theta = 0.0d;


            line2.SetUnRegisterDocument(document);
            line2.setDocumentDefaults();


            double W1 = mLoad.W1;
            double W2 = mLoad.W2;

            while (Math.Abs(W1) > 1)
            {
                W1 = W1 / 10.0;
            }

            while (Math.Abs(W2) > 1)
            {
                W2 = W2 / 10.0;
            }


            if ((EPT.y - SPT.y) > 0)
            {
                line2.EndPoint = Bearing_YZ(SPT, EPT, -W1);
                line2.StartPoint = Bearing_YZ(EPT, SPT, W2);
            }
            else
            {
                line2.EndPoint = Bearing_YZ(SPT, EPT, W2);
                line2.StartPoint = Bearing_YZ(EPT, SPT, -W1);
            }
          


            //line2.EndPoint = new gPoint(EPT.x, EPT.y + 1, EPT.z);
            line2.Layer = membload_LOCAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line2);

            ASTRAMemberLoad aline = new ASTRAMemberLoad();
            document.ActionLayout.Entities.AddItem(aline);
            document.UndoHistory.PushEnable(false);
            aline.InitializeProperties();
            aline.setDocumentDefaults();
            aline.StartPoint = line2.StartPoint;
            aline.EndPoint = line1.StartPoint;
            aline.arrowSize = 0.03;
            aline.Layer = membload_LOCAL_Lay;
            aline.Transformby(document.User2WorldMatrix);
            document.UndoHistory.PopEnable();
            document.ActionDrawFigure(aline);


            double ll = ((line1.Length() / 8.0d) / line1.Length());
            double sx, sy, sz;
            double ex, ey, ez;
            sx = sy = sz = 0.0d;
            for (int j = 1; j <= 8; j++)
            {
                ASTRAMemberLoad aline1 = new ASTRAMemberLoad();
                document.ActionLayout.Entities.AddItem(aline1);
                document.UndoHistory.PushEnable(false);
                aline1.InitializeProperties();
                aline1.setDocumentDefaults();
                aline1.arrowSize = 0.03;

                sx = (j * ll) * (line2.EndPoint.x - line2.StartPoint.x) + line2.StartPoint.x;
                sy = (j * ll) * (line2.EndPoint.y - line2.StartPoint.y) + line2.StartPoint.y;
                sz = (j * ll) * (line2.EndPoint.z - line2.StartPoint.z) + line2.StartPoint.z;

                aline1.StartPoint = new gPoint(sx, sy, sz);

                ex = (j * ll) * (line1.EndPoint.x - line1.StartPoint.x) + line1.StartPoint.x;
                ey = (j * ll) * (line1.EndPoint.y - line1.StartPoint.y) + line1.StartPoint.y;
                ez = (j * ll) * (line1.EndPoint.z - line1.StartPoint.z) + line1.StartPoint.z;

                aline1.EndPoint = new gPoint(ex, ey, ez);
                aline1.Transformby(document.User2WorldMatrix);
                document.UndoHistory.PopEnable();
                document.ActionDrawFigure(aline);
                aline1.Layer = membload_LOCAL_Lay;
            }
            vdText txt = new vdText();
            txt.SetUnRegisterDocument(document);
            txt.setDocumentDefaults();
            txt.InsertionPoint = (line1.StartPoint + line1.EndPoint + line2.StartPoint + line2.EndPoint) / 4;
            txt.Layer = membload_LOCAL_Lay;
            txt.Height = 0.5d;
            txt.PenColor = new vdColor(Color.DarkGray);
            txt.TextString = Math.Abs(mLoad.Value).ToString();
            //document.ActiveLayOut.Entities.AddItem(txt);
        }

        // Local Y
         public void DrawMemberLoad_Y_Negative_LINEAR(vdDocument document, MemberLoad mLoad)
        {

            gPoint EPT, SPT;

            SPT = mLoad.Member.StartNode.Point;
            EPT = mLoad.Member.EndNode.Point;


            vdLine line2 = new vdLine();
            vdLine line1 = new vdLine();


            line1.SetUnRegisterDocument(document);
            line1.setDocumentDefaults();
            line1.StartPoint = SPT;
            line1.EndPoint = EPT;
            line1.Layer = membload_LOCAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line1);

            radFac = Math.PI / 180;
            xtemp = ytemp = d1 = d2 = d3 = d4 = mx = my = theta = 0.0d;

            xtemp = SPT.x * Math.Cos(theta) - SPT.y * Math.Sin(theta);
            ytemp = SPT.x * Math.Sin(theta) + SPT.y * Math.Cos(theta);


            d1 = 1.0d / Math.Sin(theta);

            line2.SetUnRegisterDocument(document);
            line2.setDocumentDefaults();


            double W1 = mLoad.W1;
            double W2 = mLoad.W2;

            while (Math.Abs(W1) > 1)
            {
                W1 = W1 / 10.0;
            }

            while (Math.Abs(W2) > 1)
            {
                W2 = W2 / 10.0;
            }


            if ((EPT.x - SPT.x) > 0)
            {

                //line2.EndPoint = Bearing_XY(SPT, EPT, -0.6);
                //line2.StartPoint = Bearing_XY(EPT, SPT, 0.6);
                line2.EndPoint = Bearing_XY(SPT, EPT, -W2);
                line2.StartPoint = Bearing_XY(EPT, SPT, W1);

            }
            else
            {

                //line2.EndPoint = Bearing_XY(SPT, EPT, 0.6);
                //line2.StartPoint = Bearing_XY(EPT, SPT, -0.6);

                line2.EndPoint = Bearing_XY(SPT, EPT, W2);
                line2.StartPoint = Bearing_XY(EPT, SPT, -W1);

            }
            line2.Layer = membload_LOCAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line2);

            ASTRAMemberLoad aline = new ASTRAMemberLoad();
            document.ActionLayout.Entities.AddItem(aline);
            document.UndoHistory.PushEnable(false);
            aline.InitializeProperties();
            aline.setDocumentDefaults();
            aline.StartPoint = line2.StartPoint;
            aline.EndPoint = line1.StartPoint;
            aline.arrowSize = 0.06;
            aline.Layer = membload_LOCAL_Lay;
            aline.Transformby(document.User2WorldMatrix);
            document.UndoHistory.PopEnable();
            document.ActionDrawFigure(aline);


            double ll = ((line1.Length() / 8.0d) / line1.Length());
            double sx, sy, sz;
            double ex, ey, ez;
            sx = sy = sz = 0.0d;
            for (int j = 1; j <= 8; j++)
            {
                ASTRAMemberLoad aline1 = new ASTRAMemberLoad();
                document.ActionLayout.Entities.AddItem(aline1);
                document.UndoHistory.PushEnable(false);
                aline1.InitializeProperties();
                aline1.setDocumentDefaults();
                aline1.arrowSize = 0.06;

                sx = (j * ll) * (line2.EndPoint.x - line2.StartPoint.x) + line2.StartPoint.x;
                sy = (j * ll) * (line2.EndPoint.y - line2.StartPoint.y) + line2.StartPoint.y;
                sz = (j * ll) * (line2.EndPoint.z - line2.StartPoint.z) + line2.StartPoint.z;

                aline1.StartPoint = new gPoint(sx, sy, sz);

                ex = (j * ll) * (line1.EndPoint.x - line1.StartPoint.x) + line1.StartPoint.x;
                ey = (j * ll) * (line1.EndPoint.y - line1.StartPoint.y) + line1.StartPoint.y;
                ez = (j * ll) * (line1.EndPoint.z - line1.StartPoint.z) + line1.StartPoint.z;

                aline1.EndPoint = new gPoint(ex, ey, ez);
                aline1.Transformby(document.User2WorldMatrix);
                document.UndoHistory.PopEnable();
                document.ActionDrawFigure(aline);
                aline1.Layer = membload_LOCAL_Lay;
            }
            vdText txt = new vdText();
            txt.SetUnRegisterDocument(document);
            txt.setDocumentDefaults();
            txt.InsertionPoint = (line1.StartPoint + line1.EndPoint + line2.StartPoint + line2.EndPoint) / 4;
            txt.Layer = membload_LOCAL_Lay;
            txt.Height = 0.5d;
            txt.PenColor = new vdColor(Color.DarkGray);
            txt.TextString = Math.Abs(mLoad.Value).ToString();
            //document.ActiveLayOut.Entities.AddItem(txt);
        }

        // Local Z
         public void DrawMemberLoad_Z_Negative_LINEAR(vdDocument document, MemberLoad mLoad)
        {
            gPoint EPT, SPT;

            SPT = mLoad.Member.StartNode.Point;
            EPT = mLoad.Member.EndNode.Point;


            vdLine line2 = new vdLine();
            vdLine line1 = new vdLine();


            line1.SetUnRegisterDocument(document);
            line1.setDocumentDefaults();
            line1.StartPoint = SPT;
            line1.EndPoint = EPT;
            line1.Layer = membload_LOCAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line1);

            radFac = Math.PI / 180;
            xtemp = ytemp = d1 = d2 = d3 = d4 = mx = my = theta = 0.0d;


            //d2 = line1.Length();
            //theta = Math.Atan(1.0 / line1.Length())/radFac;
            //theta = Math.Atan((EPT.y - SPT.y) / (EPT.x - SPT.x));

            xtemp = SPT.x * Math.Cos(theta) - SPT.y * Math.Sin(theta);
            ytemp = SPT.x * Math.Sin(theta) + SPT.y * Math.Cos(theta);


            d1 = 1.0d / Math.Sin(theta);

            line2.SetUnRegisterDocument(document);
            line2.setDocumentDefaults();


            double W1 = mLoad.W1;
            double W2 = mLoad.W2;
            while (Math.Abs(W1) > 1)
            {
                W1 = W1 / 10.0;
            }

            while (Math.Abs(W2) > 1)
            {
                W2 = W2 / 10.0;
            }

            if ((EPT.x - SPT.x) > 0)
            {


                line2.EndPoint = Bearing_XZ(SPT, EPT, -W2);
                line2.StartPoint = Bearing_XZ(EPT, SPT, W1);

            }
            else
            {
                line2.EndPoint = Bearing_XZ(SPT, EPT, W2);
                line2.StartPoint = Bearing_XZ(EPT, SPT, -W1);
            }
            //line2.StartPoint = Bearing(SPT.x, SPT.y, EPT.x, EPT.y, -1);
            //line2.EndPoint = Bearing(EPT.x, EPT.y, SPT.x, SPT.y, 1);


            //line2.EndPoint = new gPoint(EPT.x, EPT.y + 1, EPT.z);
            line2.Layer = membload_LOCAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line2);

            ASTRAMemberLoad aline = new ASTRAMemberLoad();
            document.ActionLayout.Entities.AddItem(aline);
            document.UndoHistory.PushEnable(false);
            aline.InitializeProperties();
            aline.setDocumentDefaults();
            aline.StartPoint = line2.StartPoint;
            aline.EndPoint = line1.StartPoint;
            aline.arrowSize = 0.06;
            aline.Layer = membload_LOCAL_Lay;
            aline.Transformby(document.User2WorldMatrix);
            document.UndoHistory.PopEnable();
            document.ActionDrawFigure(aline);


            double ll = ((line1.Length() / 8.0d) / line1.Length());
            double sx, sy, sz;
            double ex, ey, ez;
            sx = sy = sz = 0.0d;
            for (int j = 1; j <= 8; j++)
            {
                ASTRAMemberLoad aline1 = new ASTRAMemberLoad();
                document.ActionLayout.Entities.AddItem(aline1);
                document.UndoHistory.PushEnable(false);
                aline1.InitializeProperties();
                aline1.setDocumentDefaults();
                aline1.arrowSize = 0.06;

                sx = (j * ll) * (line2.EndPoint.x - line2.StartPoint.x) + line2.StartPoint.x;
                sy = (j * ll) * (line2.EndPoint.y - line2.StartPoint.y) + line2.StartPoint.y;
                sz = (j * ll) * (line2.EndPoint.z - line2.StartPoint.z) + line2.StartPoint.z;

                aline1.StartPoint = new gPoint(sx, sy, sz);

                ex = (j * ll) * (line1.EndPoint.x - line1.StartPoint.x) + line1.StartPoint.x;
                ey = (j * ll) * (line1.EndPoint.y - line1.StartPoint.y) + line1.StartPoint.y;
                ez = (j * ll) * (line1.EndPoint.z - line1.StartPoint.z) + line1.StartPoint.z;

                aline1.EndPoint = new gPoint(ex, ey, ez);
                aline1.Transformby(document.User2WorldMatrix);
                document.UndoHistory.PopEnable();
                document.ActionDrawFigure(aline);
                aline1.Layer = membload_LOCAL_Lay;
            }
            vdText txt = new vdText();
            txt.SetUnRegisterDocument(document);
            txt.setDocumentDefaults();
            txt.InsertionPoint = (line1.StartPoint + line1.EndPoint + line2.StartPoint + line2.EndPoint) / 4;
            txt.Layer = membload_LOCAL_Lay;
            txt.Height = 0.5d;
            txt.PenColor = new vdColor(Color.DarkGray);
            txt.TextString = Math.Abs(mLoad.Value).ToString();
            //document.ActiveLayOut.Entities.AddItem(txt);
        }


        #endregion

        #region Global Load
        public void DrawMemberLoad_X_Positive_GLOBAL(vdDocument document, MemberLoad mLoad)
        {
            membload_GLOBAL_Lay.Name = "MemberLoadGlobal";
            if (document.Layers.FindName("MemberLoadGlobal") == null)
            {
                membload_GLOBAL_Lay = new vdLayer();
                membload_GLOBAL_Lay.Name = "MemberLoadGlobal";
                membload_GLOBAL_Lay.SetUnRegisterDocument(document);
                membload_GLOBAL_Lay.setDocumentDefaults();
                membload_GLOBAL_Lay.PenColor = new vdColor(Color.Green);
                document.Layers.AddItem(membload_GLOBAL_Lay);
            }
            else
            {
                membload_GLOBAL_Lay = document.Layers.FindName("MemberLoadGlobal");
            }


            gPoint EPT, SPT;



            SPT = new gPoint(mLoad.Member.StartNode.Point);
            EPT = new gPoint(mLoad.Member.EndNode.Point);


            vdLine ln = new vdLine(SPT, EPT);

            SPT = ln.getPointAtDist(mLoad.D1);
            if (mLoad.D2 != 0.0)
                EPT = ln.getPointAtDist(mLoad.D2);



            vdLine line1 = new vdLine();
            line1.SetUnRegisterDocument(document);
            line1.setDocumentDefaults();
            line1.StartPoint = SPT;
            line1.EndPoint = EPT;
            line1.Layer = membload_GLOBAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line1);

            vdLine line2 = new vdLine();
            line2.SetUnRegisterDocument(document);
            line2.setDocumentDefaults();



            double W1 = Math.Abs(mLoad.Value);

            //while (Math.Abs(W1) > 2)
            //    W1 = W1 / 2;

            //while (Math.Abs(W1) < 1)
                //W1 = W1 * 10;

            //W1 = W1 * 0.70;

            W1 = 0.70;


            line2.StartPoint = new gPoint(SPT.x - W1, SPT.y, SPT.z);
            line2.EndPoint = new gPoint(EPT.x - W1, EPT.y, EPT.z);

            line2.Layer = membload_GLOBAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line2);


            ASTRAMemberLoad aline = new ASTRAMemberLoad();
            document.ActionLayout.Entities.AddItem(aline);
            document.UndoHistory.PushEnable(false);
            aline.InitializeProperties();
            aline.setDocumentDefaults();
            aline.StartPoint = line2.StartPoint;
            aline.EndPoint = line1.StartPoint;
            aline.arrowSize = 0.1;
            aline.Layer = membload_GLOBAL_Lay;

            aline.Transformby(document.User2WorldMatrix);
            document.UndoHistory.PopEnable();
            document.ActionDrawFigure(aline);


            double ll = ((line1.Length() / 5.0d) / line1.Length());
            double sx, sy, sz;
            double ex, ey, ez;
            sx = sy = sz = 0.0d;
            for (int j = 1; j <= 5; j++)
            {
                ASTRAMemberLoad aline1 = new ASTRAMemberLoad();
                document.ActionLayout.Entities.AddItem(aline1);
                document.UndoHistory.PushEnable(false);
                aline1.InitializeProperties();
                aline1.setDocumentDefaults();
                aline1.arrowSize = 0.1;

                sx = (j * ll) * (line2.EndPoint.x - line2.StartPoint.x) + line2.StartPoint.x;
                sy = (j * ll) * (line2.EndPoint.y - line2.StartPoint.y) + line2.StartPoint.y;
                sz = (j * ll) * (line2.EndPoint.z - line2.StartPoint.z) + line2.StartPoint.z;

                aline1.StartPoint = new gPoint(sx, sy, sz);

                ex = (j * ll) * (line1.EndPoint.x - line1.StartPoint.x) + line1.StartPoint.x;
                ey = (j * ll) * (line1.EndPoint.y - line1.StartPoint.y) + line1.StartPoint.y;
                ez = (j * ll) * (line1.EndPoint.z - line1.StartPoint.z) + line1.StartPoint.z;

                aline1.EndPoint = new gPoint(ex, ey, ez);
                aline1.Transformby(document.User2WorldMatrix);
                document.UndoHistory.PopEnable();
                aline1.Layer = membload_GLOBAL_Lay;
                document.ActionDrawFigure(aline);
            }
            vdText txt = new vdText();
            txt.SetUnRegisterDocument(document);
            txt.setDocumentDefaults();
            txt.InsertionPoint = (line1.StartPoint + line1.EndPoint + line2.StartPoint + line2.EndPoint) / 4;
            txt.InsertionPoint.y += 1.0;
            txt.Layer = membload_GLOBAL_Lay;
            txt.Height = 0.5d;
            txt.PenColor = new vdColor(Color.DarkGray);
            txt.TextString = Math.Abs(mLoad.Value).ToString();
            //document.ActiveLayOut.Entities.AddItem(txt);
        }
        public void DrawMemberLoad_X_Negative_GLOBAL(vdDocument document, MemberLoad mLoad)
        {
            gPoint EPT, SPT;


            SPT = new gPoint(mLoad.Member.StartNode.Point);
            EPT = new gPoint(mLoad.Member.EndNode.Point);


            vdLine ln = new vdLine(SPT, EPT);

            SPT = ln.getPointAtDist(mLoad.D1);
            if (mLoad.D2 != 0.0)
                EPT = ln.getPointAtDist(mLoad.D2);


            vdLine line2 = new vdLine();
            vdLine line1 = new vdLine();


            line1.SetUnRegisterDocument(document);
            line1.setDocumentDefaults();
            line1.StartPoint = SPT;
            line1.EndPoint = EPT;
            line1.Layer = membload_GLOBAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line1);

            line2.SetUnRegisterDocument(document);
            line2.setDocumentDefaults();



            double W1 = Math.Abs(mLoad.Value);

            //while (Math.Abs(W1) > 2)
            //    W1 = W1 / 2;

            //while (Math.Abs(W1) < 1 && W1 > 0)
                //W1 = W1 * 10;

            //W1 = W1 * 0.70;
            W1 = 0.70;



            line2.StartPoint = new gPoint(SPT.x + W1, SPT.y, SPT.z);
            line2.EndPoint = new gPoint(EPT.x + W1, EPT.y, EPT.z);
            line2.Layer = membload_GLOBAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line2);


            ASTRAMemberLoad aline = new ASTRAMemberLoad();
            document.ActionLayout.Entities.AddItem(aline);
            document.UndoHistory.PushEnable(false);
            aline.InitializeProperties();
            aline.setDocumentDefaults();
            aline.StartPoint = line2.StartPoint;
            aline.EndPoint = line1.StartPoint;
            aline.arrowSize = 0.1;
            aline.Layer = membload_GLOBAL_Lay;
            aline.Transformby(document.User2WorldMatrix);
            document.UndoHistory.PopEnable();
            document.ActionDrawFigure(aline);


            double ll = ((line1.Length() / 5.0d) / line1.Length());
            double sx, sy, sz;
            double ex, ey, ez;
            sx = sy = sz = 0.0d;
            for (int j = 1; j <= 5; j++)
            {
                ASTRAMemberLoad aline1 = new ASTRAMemberLoad();
                document.ActionLayout.Entities.AddItem(aline1);
                document.UndoHistory.PushEnable(false);
                aline1.InitializeProperties();
                aline1.setDocumentDefaults();
                aline1.arrowSize = 0.1;

                sx = (j * ll) * (line2.EndPoint.x - line2.StartPoint.x) + line2.StartPoint.x;
                sy = (j * ll) * (line2.EndPoint.y - line2.StartPoint.y) + line2.StartPoint.y;
                sz = (j * ll) * (line2.EndPoint.z - line2.StartPoint.z) + line2.StartPoint.z;

                aline1.StartPoint = new gPoint(sx, sy, sz);

                ex = (j * ll) * (line1.EndPoint.x - line1.StartPoint.x) + line1.StartPoint.x;
                ey = (j * ll) * (line1.EndPoint.y - line1.StartPoint.y) + line1.StartPoint.y;
                ez = (j * ll) * (line1.EndPoint.z - line1.StartPoint.z) + line1.StartPoint.z;

                aline1.EndPoint = new gPoint(ex, ey, ez);
                aline1.Transformby(document.User2WorldMatrix);
                document.UndoHistory.PopEnable();
                document.ActionDrawFigure(aline);
                aline1.Layer = membload_GLOBAL_Lay;
            }
            vdText txt = new vdText();
            txt.SetUnRegisterDocument(document);
            txt.setDocumentDefaults();
            txt.InsertionPoint = (line1.StartPoint + line1.EndPoint + line2.StartPoint + line2.EndPoint) / 4;
            txt.InsertionPoint.y += 1.0;
            txt.Layer = membload_GLOBAL_Lay;
            txt.Height = 0.5d;
            txt.PenColor = new vdColor(Color.DarkGray);
            txt.TextString = Math.Abs(mLoad.Value).ToString();
            //document.ActiveLayOut.Entities.AddItem(txt);
        }
        public void DrawMemberLoad_Y_Positive_GLOBAL(vdDocument document, MemberLoad mLoad)
        {
            //System.Windows.Forms.MessageBox.Show((Math.Atan(1.0) / (Math.PI / 180)).ToString());
            gPoint EPT, SPT;


            SPT = new gPoint(mLoad.Member.StartNode.Point);
            EPT = new gPoint(mLoad.Member.EndNode.Point);


            vdLine ln = new vdLine(SPT, EPT);

            SPT = ln.getPointAtDist(mLoad.D1);
            if (mLoad.D2 != 0.0)
                EPT = ln.getPointAtDist(mLoad.D2);



            vdLine line1 = new vdLine();
            line1.SetUnRegisterDocument(document);
            line1.setDocumentDefaults();
            line1.StartPoint = SPT;
            line1.EndPoint = EPT;
            line1.Layer = membload_GLOBAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line1);

            vdLine line2 = new vdLine();
            line2.SetUnRegisterDocument(document);
            line2.setDocumentDefaults();




            double W1 = Math.Abs(mLoad.Value);

            //while (Math.Abs(W1) > 2)
            //    W1 = W1 / 2;

            //while (Math.Abs(W1) < 1)
                //W1 = W1 * 10;

            //W1 = W1 * 0.70;
            W1 = 0.70;


            line2.StartPoint = new gPoint(SPT.x, SPT.y - W1, SPT.z);
            line2.EndPoint = new gPoint(EPT.x, EPT.y - W1, EPT.z);

            line2.Layer = membload_GLOBAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line2);


            ASTRAMemberLoad aline = new ASTRAMemberLoad();
            document.ActionLayout.Entities.AddItem(aline);
            document.UndoHistory.PushEnable(false);
            aline.InitializeProperties();
            aline.setDocumentDefaults();
            aline.StartPoint = line2.StartPoint;
            aline.EndPoint = line1.StartPoint;
            aline.arrowSize = 0.3;
            aline.Layer = membload_GLOBAL_Lay;

            aline.Transformby(document.User2WorldMatrix);
            document.UndoHistory.PopEnable();
            document.ActionDrawFigure(aline);


            double ll = ((line1.Length() / 5.0d) / line1.Length());
            double sx, sy, sz;
            double ex, ey, ez;
            sx = sy = sz = 0.0d;
            for (int j = 1; j <= 5; j++)
            {
                ASTRAMemberLoad aline1 = new ASTRAMemberLoad();
                document.ActionLayout.Entities.AddItem(aline1);
                document.UndoHistory.PushEnable(false);
                aline1.InitializeProperties();
                aline1.setDocumentDefaults();
                aline1.arrowSize = 0.3;

                sx = (j * ll) * (line2.EndPoint.x - line2.StartPoint.x) + line2.StartPoint.x;
                sy = (j * ll) * (line2.EndPoint.y - line2.StartPoint.y) + line2.StartPoint.y;
                sz = (j * ll) * (line2.EndPoint.z - line2.StartPoint.z) + line2.StartPoint.z;

                aline1.StartPoint = new gPoint(sx, sy, sz);

                ex = (j * ll) * (line1.EndPoint.x - line1.StartPoint.x) + line1.StartPoint.x;
                ey = (j * ll) * (line1.EndPoint.y - line1.StartPoint.y) + line1.StartPoint.y;
                ez = (j * ll) * (line1.EndPoint.z - line1.StartPoint.z) + line1.StartPoint.z;

                aline1.EndPoint = new gPoint(ex, ey, ez);
                aline1.Transformby(document.User2WorldMatrix);
                document.UndoHistory.PopEnable();
                aline1.Layer = membload_GLOBAL_Lay;
                document.ActionDrawFigure(aline);
            }

            vdText txt = new vdText();
            txt.SetUnRegisterDocument(document);
            txt.setDocumentDefaults();
            txt.InsertionPoint = (line1.StartPoint + line1.EndPoint + line2.StartPoint + line2.EndPoint) / 4;
            txt.InsertionPoint.y += 1.0;
            txt.Layer = membload_GLOBAL_Lay;
            txt.Height = 0.5d;
            txt.PenColor = new vdColor(Color.DarkGray);
            txt.TextString = Math.Abs(mLoad.Value).ToString();
            //document.ActiveLayOut.Entities.AddItem(txt);
            //document.Redraw(true);
        }
        public void DrawMemberLoad_Y_Negative_GLOBAL(vdDocument document, MemberLoad mLoad)
        {
            //System.Windows.Forms.MessageBox.Show((Math.Atan(1.0) / (Math.PI / 180)).ToString());
            gPoint EPT, SPT;



            SPT = new gPoint(mLoad.Member.StartNode.Point);
            EPT = new gPoint(mLoad.Member.EndNode.Point);


            vdLine ln = new vdLine(SPT, EPT);

            SPT = ln.getPointAtDist(mLoad.D1);
            if (mLoad.D2 != 0.0)
            {
                try
                {
                    //EPT = ln.getPointAtDist(mLoad.D1 + mLoad.D2);
                    EPT = ln.getPointAtDist(mLoad.D2);
                }
                catch (Exception ex11) { }
            }


            vdLine line2 = new vdLine();
            vdLine line1 = new vdLine();


            line1.SetUnRegisterDocument(document);
            line1.setDocumentDefaults();
            line1.StartPoint = SPT;
            line1.EndPoint = EPT;
            line1.Layer = membload_GLOBAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line1);

            line2.SetUnRegisterDocument(document);
            line2.setDocumentDefaults();



            double W1 = Math.Abs(mLoad.Value);

            //while (Math.Abs(W1) > 2)
            //    W1 = W1 / 2.0;

            ////while (Math.Abs(W1) < 1)
            //    //W1 = W1 * 10;

            //W1 = W1 * 0.70;

            W1 = 0.70;

            line2.StartPoint = new gPoint(SPT.x, SPT.y + W1, SPT.z);
            line2.EndPoint = new gPoint(EPT.x , EPT.y + W1, EPT.z);

            line2.Layer = membload_GLOBAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line2);


            ASTRAMemberLoad aline = new ASTRAMemberLoad();
            document.ActionLayout.Entities.AddItem(aline);
            document.UndoHistory.PushEnable(false);
            aline.InitializeProperties();
            aline.setDocumentDefaults();
            aline.StartPoint = line2.StartPoint;
            aline.EndPoint = line1.StartPoint;
            aline.arrowSize = 0.1;
            aline.Layer = membload_GLOBAL_Lay;
            aline.Transformby(document.User2WorldMatrix);
            document.UndoHistory.PopEnable();
            document.ActionDrawFigure(aline);


            double ll = ((line1.Length() / 5.0d) / line1.Length());
            double sx, sy, sz;
            double ex, ey, ez;
            sx = sy = sz = 0.0d;
            for (int j = 1; j <= 5; j++)
            {
                ASTRAMemberLoad aline1 = new ASTRAMemberLoad();
                document.ActionLayout.Entities.AddItem(aline1);
                document.UndoHistory.PushEnable(false);
                aline1.InitializeProperties();
                aline1.setDocumentDefaults();
                aline1.arrowSize = 0.1;

                sx = (j * ll) * (line2.EndPoint.x - line2.StartPoint.x) + line2.StartPoint.x;
                sy = (j * ll) * (line2.EndPoint.y - line2.StartPoint.y) + line2.StartPoint.y;
                sz = (j * ll) * (line2.EndPoint.z - line2.StartPoint.z) + line2.StartPoint.z;

                aline1.StartPoint = new gPoint(sx, sy, sz);

                ex = (j * ll) * (line1.EndPoint.x - line1.StartPoint.x) + line1.StartPoint.x;
                ey = (j * ll) * (line1.EndPoint.y - line1.StartPoint.y) + line1.StartPoint.y;
                ez = (j * ll) * (line1.EndPoint.z - line1.StartPoint.z) + line1.StartPoint.z;

                aline1.EndPoint = new gPoint(ex, ey, ez);
                aline1.Transformby(document.User2WorldMatrix);
                document.UndoHistory.PopEnable();
                document.ActionDrawFigure(aline);
                aline1.Layer = membload_GLOBAL_Lay;
            }
            vdText txt = new vdText();
            txt.SetUnRegisterDocument(document);
            txt.setDocumentDefaults();
            txt.InsertionPoint = (line1.StartPoint + line1.EndPoint + line2.StartPoint + line2.EndPoint) / 4;
            txt.InsertionPoint.y += 1.0;
            txt.Layer = membload_GLOBAL_Lay;
            txt.Height = 0.5d;
            txt.PenColor = new vdColor(Color.DarkGray);
            txt.TextString = Math.Abs(mLoad.Value).ToString();
            //document.ActiveLayOut.Entities.AddItem(txt);
        }
        public void DrawMemberLoad_Z_Positive_GLOBAL(vdDocument document, MemberLoad mLoad)
        {
            gPoint EPT, SPT;


            SPT = new gPoint(mLoad.Member.StartNode.Point);
            EPT = new gPoint(mLoad.Member.EndNode.Point);


            vdLine ln = new vdLine(SPT, EPT);

            SPT = ln.getPointAtDist(mLoad.D1);
            if (mLoad.D2 != 0.0)
                EPT = ln.getPointAtDist( mLoad.D2);



            vdLine line1 = new vdLine();
            line1.SetUnRegisterDocument(document);
            line1.setDocumentDefaults();
            line1.StartPoint = SPT;
            line1.EndPoint = EPT;
            line1.Layer = membload_GLOBAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line1);

            vdLine line2 = new vdLine();
            line2.SetUnRegisterDocument(document);
            line2.setDocumentDefaults();




            double W1 = Math.Abs(mLoad.Value);

            //while (Math.Abs(W1) > 2)
            //    W1 = W1 / 2;

            //while (Math.Abs(W1) < 1)
                //W1 = W1 * 10;

            //W1 = W1 * 0.70;
            W1 = 0.70;



            line2.StartPoint = new gPoint(SPT.x, SPT.y, SPT.z - W1);
            line2.EndPoint = new gPoint(EPT.x, EPT.y, EPT.z - W1);

            line2.Layer = membload_GLOBAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line2);


            ASTRAMemberLoad aline = new ASTRAMemberLoad();
            document.ActionLayout.Entities.AddItem(aline);
            document.UndoHistory.PushEnable(false);
            aline.InitializeProperties();
            aline.setDocumentDefaults();
            aline.StartPoint = line2.StartPoint;
            aline.EndPoint = line1.StartPoint;
            aline.arrowSize = 0.3;
            aline.Layer = membload_GLOBAL_Lay;

            aline.Transformby(document.User2WorldMatrix);
            document.UndoHistory.PopEnable();
            document.ActionDrawFigure(aline);


            double ll = ((line1.Length() / 5.0d) / line1.Length());
            double sx, sy, sz;
            double ex, ey, ez;
            sx = sy = sz = 0.0d;
            for (int j = 1; j <= 5; j++)
            {
                ASTRAMemberLoad aline1 = new ASTRAMemberLoad();
                document.ActionLayout.Entities.AddItem(aline1);
                document.UndoHistory.PushEnable(false);
                aline1.InitializeProperties();
                aline1.setDocumentDefaults();
                aline1.arrowSize = 0.3;

                sx = (j * ll) * (line2.EndPoint.x - line2.StartPoint.x) + line2.StartPoint.x;
                sy = (j * ll) * (line2.EndPoint.y - line2.StartPoint.y) + line2.StartPoint.y;
                sz = (j * ll) * (line2.EndPoint.z - line2.StartPoint.z) + line2.StartPoint.z;

                aline1.StartPoint = new gPoint(sx, sy, sz);

                ex = (j * ll) * (line1.EndPoint.x - line1.StartPoint.x) + line1.StartPoint.x;
                ey = (j * ll) * (line1.EndPoint.y - line1.StartPoint.y) + line1.StartPoint.y;
                ez = (j * ll) * (line1.EndPoint.z - line1.StartPoint.z) + line1.StartPoint.z;

                aline1.EndPoint = new gPoint(ex, ey, ez);
                aline1.Transformby(document.User2WorldMatrix);
                document.UndoHistory.PopEnable();
                aline1.Layer = membload_GLOBAL_Lay;
                document.ActionDrawFigure(aline);
            }
            vdText txt = new vdText();
            txt.SetUnRegisterDocument(document);
            txt.setDocumentDefaults();
            txt.InsertionPoint = (line1.StartPoint + line1.EndPoint + line2.StartPoint + line2.EndPoint) / 4;
            txt.InsertionPoint.y += 1.0;
            txt.Layer = membload_GLOBAL_Lay;
            txt.Height = 0.5d;
            txt.PenColor = new vdColor(Color.DarkGray);
            txt.TextString = Math.Abs(mLoad.Value).ToString();
            //document.ActiveLayOut.Entities.AddItem(txt);
        }
        public void DrawMemberLoad_Z_Negative_GLOBAL(vdDocument document, MemberLoad mLoad)
        {
            gPoint EPT, SPT;


            SPT = new gPoint(mLoad.Member.StartNode.Point);
            EPT = new gPoint(mLoad.Member.EndNode.Point);


            vdLine ln = new vdLine(SPT, EPT);

            SPT = ln.getPointAtDist(mLoad.D1);
            if (mLoad.D2 != 0.0)
                EPT = ln.getPointAtDist(mLoad.D2);




            vdLine line2 = new vdLine();
            vdLine line1 = new vdLine();


            line1.SetUnRegisterDocument(document);
            line1.setDocumentDefaults();
            line1.StartPoint = SPT;
            line1.EndPoint = EPT;
            line1.Layer = membload_GLOBAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line1);

            line2.SetUnRegisterDocument(document);
            line2.setDocumentDefaults();



            double W1 = Math.Abs(mLoad.Value);

            //while (Math.Abs(W1) > 2)
            //    W1 = W1 / 2;

            //while (Math.Abs(W1) < 1)
                //W1 = W1 * 10;

            //W1 = W1 * 0.70;
            W1 = 0.70;


            line2.StartPoint = new gPoint(SPT.x, SPT.y, SPT.z + W1);
            line2.EndPoint = new gPoint(EPT.x, EPT.y, EPT.z + W1);
            line2.Layer = membload_GLOBAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line2);


            ASTRAMemberLoad aline = new ASTRAMemberLoad();
            document.ActionLayout.Entities.AddItem(aline);
            document.UndoHistory.PushEnable(false);
            aline.InitializeProperties();
            aline.setDocumentDefaults();
            aline.StartPoint = line2.StartPoint;
            aline.EndPoint = line1.StartPoint;
            aline.arrowSize = 0.3;
            aline.Layer = membload_GLOBAL_Lay;
            aline.Transformby(document.User2WorldMatrix);
            document.UndoHistory.PopEnable();
            document.ActionDrawFigure(aline);


            double ll = ((line1.Length() / 5.0d) / line1.Length());
            double sx, sy, sz;
            double ex, ey, ez;
            sx = sy = sz = 0.0d;
            for (int j = 1; j <= 5; j++)
            {
                ASTRAMemberLoad aline1 = new ASTRAMemberLoad();
                document.ActionLayout.Entities.AddItem(aline1);
                document.UndoHistory.PushEnable(false);
                aline1.InitializeProperties();
                aline1.setDocumentDefaults();
                aline1.arrowSize = 0.3;

                sx = (j * ll) * (line2.EndPoint.x - line2.StartPoint.x) + line2.StartPoint.x;
                sy = (j * ll) * (line2.EndPoint.y - line2.StartPoint.y) + line2.StartPoint.y;
                sz = (j * ll) * (line2.EndPoint.z - line2.StartPoint.z) + line2.StartPoint.z;

                aline1.StartPoint = new gPoint(sx, sy, sz);

                ex = (j * ll) * (line1.EndPoint.x - line1.StartPoint.x) + line1.StartPoint.x;
                ey = (j * ll) * (line1.EndPoint.y - line1.StartPoint.y) + line1.StartPoint.y;
                ez = (j * ll) * (line1.EndPoint.z - line1.StartPoint.z) + line1.StartPoint.z;

                aline1.EndPoint = new gPoint(ex, ey, ez);
                aline1.Transformby(document.User2WorldMatrix);
                document.UndoHistory.PopEnable();
                document.ActionDrawFigure(aline);
                aline1.Layer = membload_GLOBAL_Lay;
            }
            vdText txt = new vdText();
            txt.SetUnRegisterDocument(document);
            txt.setDocumentDefaults();
            txt.InsertionPoint = (line1.StartPoint + line1.EndPoint + line2.StartPoint + line2.EndPoint) / 4;
            txt.InsertionPoint.y += 1.0;
            txt.Layer = membload_GLOBAL_Lay;
            txt.Height = 0.5d;
            txt.PenColor = new vdColor(Color.DarkGray);
            txt.TextString = Math.Abs(mLoad.Value).ToString();
            //document.ActiveLayOut.Entities.AddItem(txt);
        }
        public void DrawMemberLoad_CON_Negative_GLOBAL_Z(vdDocument document, MemberLoad mLoad)
        {
            gPoint EPT, SPT;

            SPT = mLoad.Member.StartNode.Point;
            EPT = mLoad.Member.EndNode.Point;



            ASTRAMemberLoad aline = new ASTRAMemberLoad();
            document.ActionLayout.Entities.AddItem(aline);
            document.UndoHistory.PushEnable(false);
            aline.InitializeProperties();
            aline.setDocumentDefaults();
            aline.StartPoint = mLoad.Member.StartNode.Point;
            aline.EndPoint = mLoad.Member.StartNode.Point;
            aline.arrowSize = 0.3;
            aline.Layer = membload_GLOBAL_Lay;
            aline.Transformby(document.User2WorldMatrix);
            document.UndoHistory.PopEnable();
            document.ActionDrawFigure(aline);
        }
        #endregion

        #region Concentrate Load
        public void DrawMemberLoad_CONS(vdDocument document, MemberLoad mLoad)
        {

            conc_LOCAL_Lay = document.Layers.FindName("ConcentrateLocal");
            if(conc_LOCAL_Lay == null)
            {
                conc_LOCAL_Lay = new vdLayer();
                conc_LOCAL_Lay.Name = "ConcentrateLocal";
                conc_LOCAL_Lay.SetUnRegisterDocument(document);
                conc_LOCAL_Lay.setDocumentDefaults();
                conc_LOCAL_Lay.PenColor = new vdColor(Color.Blue);
                document.Layers.AddItem(conc_LOCAL_Lay);
            }
            conc_GLOBAL_Lay = document.Layers.FindName("ConcentrateGlobal");
            if (conc_GLOBAL_Lay == null)
            {
                conc_GLOBAL_Lay = new vdLayer();
                conc_GLOBAL_Lay.Name = "ConcentrateGlobal";
                conc_GLOBAL_Lay.SetUnRegisterDocument(document);
                conc_GLOBAL_Lay.setDocumentDefaults();
                document.Layers.AddItem(conc_GLOBAL_Lay);
            }
            conc_LOCAL_Lay.PenColor = new vdColor(Color.Blue);
            conc_GLOBAL_Lay.PenColor = new vdColor(Color.Red);
        
            gPoint EPT, SPT,lineSPT,lineEPT;

            SPT = new gPoint( mLoad.Member.StartNode.Point);
            EPT =new gPoint( mLoad.Member.EndNode.Point);

            lineSPT = new gPoint();

            lineSPT.x = SPT.x + (EPT.x - SPT.x) * (mLoad.DistanceFromStartNode / gPoint.Distance3D(SPT, EPT));
            lineSPT.y = SPT.y + (EPT.y - SPT.y) * (mLoad.DistanceFromStartNode / gPoint.Distance3D(SPT, EPT));
            lineSPT.z = SPT.z + (EPT.z - SPT.z) * (mLoad.DistanceFromStartNode / gPoint.Distance3D(SPT, EPT));

            //lineEPT = new gPoint(lineSPT.x + 1, lineSPT.y, lineSPT.z);
            lineEPT = new gPoint();

            if (mLoad.Value > 0)
            {
                switch (mLoad.Direction)
                {
                    case MemberLoad.eDirection.GX:
                        lineEPT = new gPoint(lineSPT.x - 1, lineSPT.y, lineSPT.z);
                        break;
                    case MemberLoad.eDirection.GY:
                        lineEPT = new gPoint(lineSPT.x, lineSPT.y - 1, lineSPT.z);
                        break;
                    case MemberLoad.eDirection.GZ:
                        lineEPT = new gPoint(lineSPT.x, lineSPT.y, lineSPT.z - 1);
                        break;
                    case MemberLoad.eDirection.X:
                        if (SPT.x > EPT.x)
                            lineEPT = Bearing_YZ(lineSPT, EPT, -1);
                        else
                            lineEPT = Bearing_YZ(lineSPT, EPT, 1);
                        break;
                    case MemberLoad.eDirection.Y:
                        if (SPT.x < EPT.x)
                            lineEPT = Bearing_XY(EPT, lineSPT, -1);
                        else
                            lineEPT = Bearing_XY(EPT, lineSPT, 1);

                        break;
                    case MemberLoad.eDirection.Z:
                        if (SPT.x < EPT.x)
                            lineEPT = Bearing_XZ(EPT, lineSPT, -1);
                        else
                            lineEPT = Bearing_XZ(EPT, lineSPT, 1);
                        break;

                }
            }
            else
            {
                switch (mLoad.Direction)
                {
                    case MemberLoad.eDirection.GX:
                        lineEPT = new gPoint(lineSPT.x + 1, lineSPT.y, lineSPT.z);
                        break;
                    case MemberLoad.eDirection.GY:
                        lineEPT = new gPoint(lineSPT.x, lineSPT.y + 1, lineSPT.z);
                        break;
                    case MemberLoad.eDirection.GZ:
                        lineEPT = new gPoint(lineSPT.x, lineSPT.y, lineSPT.z + 1);
                        break;
                    case MemberLoad.eDirection.X:

                        if (lineSPT.x < EPT.x)
                            lineEPT = Bearing_XY(lineSPT, EPT, 1);
                        else
                            lineEPT = Bearing_XY(lineSPT, EPT, -1);

                        break;
                    case MemberLoad.eDirection.Y:
                        if (lineSPT.x < EPT.x)
                            lineEPT = Bearing_XY(EPT, lineSPT, 1);
                        else
                            lineEPT = Bearing_XY(EPT, lineSPT, -1);
                        break;
                    case MemberLoad.eDirection.Z:

                        if (lineSPT.x < EPT.x)
                            lineEPT = Bearing_XZ(EPT, lineSPT, 1);
                        else
                            lineEPT = Bearing_XZ(EPT, lineSPT, -1);
                        break;

                }
            }

            //SPT.x += mLoad.DistanceFromStartNode;

            //EPT.x += (mLoad.DistanceFromStartNode + 1);

            ASTRAMemberLoad aline = new ASTRAMemberLoad();
            aline.SetUnRegisterDocument(document);
            aline.setDocumentDefaults();
            //aline.StartPoint = lineSPT;
            //aline.EndPoint = lineEPT;
            aline.StartPoint = lineEPT;
            aline.EndPoint = lineSPT;

            aline.arrowSize = 0.3d;

            switch (mLoad.Direction)
            {
                case MemberLoad.eDirection.GX:
                    aline.Layer = conc_GLOBAL_Lay;
                    break;
                case MemberLoad.eDirection.GY:
                    aline.Layer = conc_GLOBAL_Lay;
                    break;
                case MemberLoad.eDirection.GZ:
                    aline.Layer = conc_GLOBAL_Lay;
                    break;
                case MemberLoad.eDirection.X:
                    aline.Layer = conc_LOCAL_Lay;
                    break;
                case MemberLoad.eDirection.Y:
                    aline.Layer = conc_LOCAL_Lay;
                    break;
                case MemberLoad.eDirection.Z:
                    aline.Layer = conc_LOCAL_Lay;
                    break;
            }
            document.ActiveLayOut.Entities.AddItem(aline);




            double factor, length,textSize;

            length = gPoint.Distance3D(EPT, SPT);

            factor = 7.0d / 0.3d;

            textSize = length / factor;



            vdText vTxt = new vdText();
            vTxt.SetUnRegisterDocument(document);
            vTxt.setDocumentDefaults();
            vTxt.InsertionPoint = aline.EndPoint;
            vTxt.TextString = mLoad.Value.ToString();
            //vTxt.Height = 0.3d;
            vTxt.Height = textSize;

            switch (mLoad.Direction)
            {
                case MemberLoad.eDirection.GX:
                case MemberLoad.eDirection.GY:
                case MemberLoad.eDirection.GZ:
                    vTxt.Layer = conc_GLOBAL_Lay;
                    break;
                case MemberLoad.eDirection.X:
                case MemberLoad.eDirection.Y:
                case MemberLoad.eDirection.Z:
                    vTxt.Layer = conc_LOCAL_Lay;
                    break;
            }
            //document.ActiveLayOut.Entities.AddItem(vTxt);
        }

        #endregion


        public void DrawMemberLoad_Y_Negative_LOCAL_Triangular(vdDocument document, MemberLoad mLoad)
        {

            gPoint EPT, SPT;

            SPT = mLoad.Member.StartNode.Point;
            EPT = mLoad.Member.EndNode.Point;


            vdLine line2 = new vdLine();
            vdLine line1 = new vdLine();


            line1.SetUnRegisterDocument(document);
            line1.setDocumentDefaults();
            line1.StartPoint = SPT;
            line1.EndPoint = EPT;
            line1.Layer = membload_LOCAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line1);
            
            radFac = Math.PI / 180;
            xtemp = ytemp = d1 = d2 = d3 = d4 = mx = my = theta = 0.0d;


            //d2 = line1.Length();
            //theta = Math.Atan(1.0 / line1.Length())/radFac;
            //theta = Math.Atan((EPT.y - SPT.y) / (EPT.x - SPT.x));

            xtemp = SPT.x * Math.Cos(theta) - SPT.y * Math.Sin(theta);
            ytemp = SPT.x * Math.Sin(theta) + SPT.y * Math.Cos(theta);


            d1 = 1.0d / Math.Sin(theta);

            line2.SetUnRegisterDocument(document);
            line2.setDocumentDefaults();
            line2.StartPoint = new gPoint(xtemp, ytemp, SPT.z);
            line2.EndPoint = new gPoint(EPT.x, EPT.y + 1, EPT.z);
            line2.Layer = membload_LOCAL_Lay;
            document.ActiveLayOut.Entities.AddItem(line2);

            ASTRAMemberLoad aline = new ASTRAMemberLoad();
            document.ActionLayout.Entities.AddItem(aline);
            document.UndoHistory.PushEnable(false);
            aline.InitializeProperties();
            aline.setDocumentDefaults();
            aline.StartPoint = line2.StartPoint;
            aline.EndPoint = line1.StartPoint;
            aline.arrowSize = 0.3;
            aline.Layer = membload_LOCAL_Lay;
            aline.Transformby(document.User2WorldMatrix);
            document.UndoHistory.PopEnable();
            document.ActionDrawFigure(aline);


            double ll = ((line1.Length() / 5.0d) / line1.Length());
            double sx, sy, sz;
            double ex, ey, ez;
            sx = sy = sz = 0.0d;
            for (int j = 1; j <= 5; j++)
            {
                ASTRAMemberLoad aline1 = new ASTRAMemberLoad();
                document.ActionLayout.Entities.AddItem(aline1);
                document.UndoHistory.PushEnable(false);
                aline1.InitializeProperties();
                aline1.setDocumentDefaults();
                aline1.arrowSize = 0.3;

                sx = (j * ll) * (line2.EndPoint.x - line2.StartPoint.x) + line2.StartPoint.x;
                sy = (j * ll) * (line2.EndPoint.y - line2.StartPoint.y) + line2.StartPoint.y;
                sz = (j * ll) * (line2.EndPoint.z - line2.StartPoint.z) + line2.StartPoint.z;

                aline1.StartPoint = new gPoint(sx, sy, sz);

                ex = (j * ll) * (line1.EndPoint.x - line1.StartPoint.x) + line1.StartPoint.x;
                ey = (j * ll) * (line1.EndPoint.y - line1.StartPoint.y) + line1.StartPoint.y;
                ez = (j * ll) * (line1.EndPoint.z - line1.StartPoint.z) + line1.StartPoint.z;

                aline1.EndPoint = new gPoint(ex, ey, ez);
                aline1.Transformby(document.User2WorldMatrix);
                document.UndoHistory.PopEnable();
                document.ActionDrawFigure(aline);
                aline1.Layer = membload_LOCAL_Lay;
            }
            vdText txt = new vdText();
            txt.SetUnRegisterDocument(document);
            txt.setDocumentDefaults();
            txt.InsertionPoint = (line1.StartPoint + line1.EndPoint + line2.StartPoint + line2.EndPoint) / 4;
            txt.Layer = membload_LOCAL_Lay;
            txt.Height = 0.5d;
            txt.PenColor = new vdColor(Color.DarkGray);
            txt.TextString = Math.Abs(mLoad.Value).ToString();
            //document.ActiveLayOut.Entities.AddItem(txt);
        }


        public void ShowMember(vdDocument document, MemberIncidence mInci,string HEADING_Text)
        {
            //gPoint gp1, gp2;

            #region Draw Member Line
            vdLine memberLine = new vdLine();
            memberLine.SetUnRegisterDocument(document);
            memberLine.setDocumentDefaults();
            memberLine.StartPoint = new gPoint(10, 12, 0);
            memberLine.EndPoint = new gPoint(20, 12, 0);
            document.ActiveLayOut.Entities.AddItem(memberLine);
            #endregion

            #region Draw Member Start Node Text

            vdText startNodeText = new vdText();
            startNodeText.SetUnRegisterDocument(document);
            startNodeText.setDocumentDefaults();
            startNodeText.InsertionPoint = new gPoint(9.2710d, 11.1111d, 0);
            startNodeText.Height = 0.7d;
            startNodeText.TextString = mInci.StartNode.NodeNo.ToString();
            startNodeText.PenColor = new vdColor(Color.Magenta);
            document.ActiveLayOut.Entities.AddItem(startNodeText);

            #endregion

            #region Draw Member No Text
            vdText memberNoText = new vdText();
            memberNoText.SetUnRegisterDocument(document);
            memberNoText.setDocumentDefaults();
            memberNoText.InsertionPoint = new gPoint(14.5224d, 11.1111d, 0);
            memberNoText.Height = 0.7d;
            memberNoText.TextString = mInci.MemberNo.ToString();
            memberNoText.PenColor = new vdColor(Color.Blue);
            document.ActiveLayOut.Entities.AddItem(memberNoText);
            #endregion

            #region Draw Member End Node Text
            vdText endNodeText = new vdText();
            endNodeText.SetUnRegisterDocument(document);
            endNodeText.setDocumentDefaults();
            endNodeText.InsertionPoint = new gPoint(20.18d, 11.1111d);
            endNodeText.Height = 0.7d;
            endNodeText.TextString = mInci.EndNode.NodeNo.ToString();
            endNodeText.PenColor = new vdColor(Color.Magenta);
            document.ActiveLayOut.Entities.AddItem(endNodeText);
            #endregion

            #region Draw Rectangle
            vdRect rect = new vdRect();
            rect.SetUnRegisterDocument(document);
            rect.setDocumentDefaults();
            rect.InsertionPoint = new gPoint(7.55d, 10.424d);
            rect.Width = 15.2406d;
            rect.Height = 6.2206d;
            document.ActiveLayOut.Entities.AddItem(rect);


            #endregion


            #region Draw Member Line 2
            /*
            vdLine memberLine2 = new vdLine();
            memberLine2.SetUnRegisterDocument(document);
            memberLine2.setDocumentDefaults();
            memberLine2.StartPoint = new gPoint(10, 13, 0);
            memberLine2.EndPoint = new gPoint(20, 13, 0);
            document.ActiveLayOut.Entities.AddItem(memberLine2);
            */
            #endregion

            #region Draw Member Load
            /*

            ASTRAMemberLoad aline = new ASTRAMemberLoad();
            document.ActionLayout.Entities.AddItem(aline);
            document.UndoHistory.PushEnable(false);
            aline.InitializeProperties();
            aline.setDocumentDefaults();
            aline.StartPoint = memberLine2.StartPoint;
            aline.EndPoint = memberLine.StartPoint;
            aline.arrowSize = 0.3;
            aline.Transformby(document.User2WorldMatrix);
            document.UndoHistory.PopEnable();
            document.ActionDrawFigure(aline);


            double sx, sy, sz, ex, ey, ez;
            sx = sy = sz = ex = ey = ez = 0.0d;
            double ll = ((memberLine2.Length() / 8.0d) / memberLine.Length());
            for (int j = 1; j <= 8; j++)
            {
                ASTRAMemberLoad aline1 = new ASTRAMemberLoad();
                document.ActionLayout.Entities.AddItem(aline1);
                document.UndoHistory.PushEnable(false);
                aline1.InitializeProperties();
                aline1.setDocumentDefaults();
                aline1.arrowSize = 0.4d;

                sx = (j * ll) * (memberLine.EndPoint.x - memberLine.StartPoint.x) + memberLine.StartPoint.x;
                sy = (j * ll) * (memberLine.EndPoint.y - memberLine.StartPoint.y) + memberLine.StartPoint.y;
                sz = (j * ll) * (memberLine.EndPoint.z - memberLine.StartPoint.z) + memberLine.StartPoint.z;


                ex = (j * ll) * (memberLine2.EndPoint.x - memberLine2.StartPoint.x) + memberLine2.StartPoint.x;
                ey = (j * ll) * (memberLine2.EndPoint.y - memberLine2.StartPoint.y) + memberLine2.StartPoint.y;
                ez = (j * ll) * (memberLine2.EndPoint.z - memberLine2.StartPoint.z) + memberLine2.StartPoint.z;

                aline1.StartPoint = new gPoint(ex, ey, ez); 
                aline1.EndPoint = new gPoint(sx, sy, sz);
                aline1.Transformby(document.User2WorldMatrix);
                document.UndoHistory.PopEnable();
                document.ActionDrawFigure(aline1);
            }
            */
            #endregion


            #region Show MEMBER DIAGRAM

            vdText memDiagram = new vdText();
            memDiagram.SetUnRegisterDocument(document);
            memDiagram.setDocumentDefaults();

            memDiagram.InsertionPoint = new gPoint(12.36d, 15.90d);
            memDiagram.Height = 0.5d;
            memDiagram.TextString = HEADING_Text;
            memDiagram.PenColor = new vdColor(Color.Red);

            document.ActiveLayOut.Entities.AddItem(memDiagram);

            #endregion
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(document);

            document.Redraw(true);
        }
        public void ShowMember(vdDocument document, MemberLoad mLoad)
        {
            double sx, sy, sz, ex, ey, ez;
            sx = sy = sz = ex = ey = ez = 0.0d;

            #region Draw Member Line
            vdLine memberLine = new vdLine();
            memberLine.SetUnRegisterDocument(document);
            memberLine.setDocumentDefaults();
            memberLine.StartPoint = new gPoint(10, 12, 0);
            memberLine.EndPoint = new gPoint(20, 12, 0);
            document.ActiveLayOut.Entities.AddItem(memberLine);
            #endregion

            #region Draw Member Start Node Text

            vdText startNodeText = new vdText();
            startNodeText.SetUnRegisterDocument(document);
            startNodeText.setDocumentDefaults();
            startNodeText.InsertionPoint = new gPoint(9.2710d, 11.1111d, 0);
            startNodeText.Height = 0.7d;
            startNodeText.TextString = mLoad.Member.StartNode.NodeNo.ToString();
            startNodeText.PenColor = new vdColor(Color.Magenta);
            document.ActiveLayOut.Entities.AddItem(startNodeText);

            #endregion

            #region Draw Member No Text
            vdText memberNoText = new vdText();
            memberNoText.SetUnRegisterDocument(document);
            memberNoText.setDocumentDefaults();
            memberNoText.InsertionPoint = new gPoint(14.5224d, 11.1111d, 0);
            memberNoText.Height = 0.7d;
            memberNoText.TextString = mLoad.Member.MemberNo.ToString();
            memberNoText.PenColor = new vdColor(Color.Blue);
            document.ActiveLayOut.Entities.AddItem(memberNoText);
            #endregion

            #region Draw Member End Node Text
            vdText endNodeText = new vdText();
            endNodeText.SetUnRegisterDocument(document);
            endNodeText.setDocumentDefaults();
            endNodeText.InsertionPoint = new gPoint(20.18d, 11.1111d);
            endNodeText.Height = 0.7d;
            endNodeText.TextString = mLoad.Member.EndNode.NodeNo.ToString();
            endNodeText.PenColor = new vdColor(Color.Magenta);
            document.ActiveLayOut.Entities.AddItem(endNodeText);
            #endregion

            //#region Draw Rectangle
            //vdRect rect = new vdRect();
            //rect.SetUnRegisterDocument(document);
            //rect.setDocumentDefaults();
            //rect.InsertionPoint = new gPoint(8.1085d, 9.030d);
            //rect.Width = 13.406d;
            //rect.Height = 6.5206d;
            //document.ActiveLayOut.Entities.AddItem(rect);


            //#endregion

            #region Draw Rectangle
            vdRect rect = new vdRect();
            rect.SetUnRegisterDocument(document);
            rect.setDocumentDefaults();
            rect.InsertionPoint = new gPoint(7.55d, 10.424d);
            rect.Width = 15.2406d;
            rect.Height = 6.2206d;
            document.ActiveLayOut.Entities.AddItem(rect);


            #endregion

            #region Show Member Details

            //vdText memDiagram = new vdText();
            //memDiagram.SetUnRegisterDocument(document);
            //memDiagram.setDocumentDefaults();

            //memDiagram.InsertionPoint = new gPoint(11.36d, 14.0d);
            //memDiagram.Height = 0.5d;
            //document.ActiveLayOut.Entities.AddItem(memDiagram);

            #endregion

            #region DRAW CONCENTRATE LOAD
            double dist = 0.0d;
            double len = 0.0d;
            if (mLoad.LoadType == MemberLoad.eLoadType.Concentrate)
            {
                dist = mLoad.DistanceFromStartNode;

                sx = 0.0d;
                sy = 0.0d;
                sz = 0.0d;
                len = dist / memberLine.Length();


                sx = len * (memberLine.EndPoint.x - memberLine.StartPoint.x) + memberLine.StartPoint.x;
                sy = len * (memberLine.EndPoint.y - memberLine.StartPoint.y) + memberLine.StartPoint.y;
                //sz = len * (memberLine.EndPoint.z - memberLine.StartPoint.z) + memberLine.StartPoint.z;

                ex = sx;
                ey = sy + 1.0d;
                //ex = sz;


                ASTRAMemberLoad ConcentrateLoad = new ASTRAMemberLoad();
                document.ActionLayout.Entities.AddItem(ConcentrateLoad);
                document.UndoHistory.PushEnable(false);
                ConcentrateLoad.InitializeProperties();
                ConcentrateLoad.setDocumentDefaults();
                ConcentrateLoad.arrowSize = 0.7d;
                ConcentrateLoad.PenColor = new vdColor(Color.Red);
                ConcentrateLoad.StartPoint =new gPoint(ex, ey, ez); 
                ConcentrateLoad.EndPoint = new gPoint(sx, sy, sz);
                ConcentrateLoad.Transformby(document.User2WorldMatrix);
                document.UndoHistory.PopEnable();
                document.ActionDrawFigure(ConcentrateLoad);


                vdText conVal = new vdText();
                conVal.SetUnRegisterDocument(document);
                conVal.setDocumentDefaults();
                conVal.TextString = "CON " + mLoad.DistanceFromStartNode + " " + mLoad.Value.ToString();
                conVal.InsertionPoint = new gPoint(ex - 0.7d, ey + 0.5d);
                conVal.Height = 0.56d;
                document.ActiveLayOut.Entities.AddItem(conVal);




            }
            else
            {
                #region Draw Member Line 2
                vdLine memberLine2 = new vdLine();
                memberLine2.SetUnRegisterDocument(document);
                memberLine2.setDocumentDefaults();
                memberLine2.StartPoint = new gPoint(10, 13, 0);
                memberLine2.EndPoint = new gPoint(20, 13, 0);
                document.ActiveLayOut.Entities.AddItem(memberLine2);
                #endregion

                #region Draw Member Load UDL

                ASTRAMemberLoad aline = new ASTRAMemberLoad();
                document.ActionLayout.Entities.AddItem(aline);
                document.UndoHistory.PushEnable(false);
                aline.InitializeProperties();
                aline.setDocumentDefaults();
                aline.StartPoint = memberLine2.StartPoint;
                aline.EndPoint = memberLine.StartPoint;
                aline.arrowSize = 0.3;
                aline.Transformby(document.User2WorldMatrix);
                document.UndoHistory.PopEnable();
                document.ActionDrawFigure(aline);


              
                double ll = ((memberLine2.Length() / 8.0d) / memberLine.Length());
                for (int j = 1; j <= 8; j++)
                {
                    ASTRAMemberLoad aline1 = new ASTRAMemberLoad();
                    document.ActionLayout.Entities.AddItem(aline1);
                    document.UndoHistory.PushEnable(false);
                    aline1.InitializeProperties();
                    aline1.setDocumentDefaults();
                    aline1.arrowSize = 0.4d;

                    if (((mLoad.Direction == MemberLoad.eDirection.GX) ||
                        (mLoad.Direction == MemberLoad.eDirection.GY) ||
                        (mLoad.Direction == MemberLoad.eDirection.GZ)))
                    {
                        aline1.PenColor = new vdColor(Color.Green);

                    }
                    else
                    {
                        aline1.PenColor = new vdColor(Color.Blue);
                    }

                    aline.PenColor = aline1.PenColor;

                    sx = (j * ll) * (memberLine.EndPoint.x - memberLine.StartPoint.x) + memberLine.StartPoint.x;
                    sy = (j * ll) * (memberLine.EndPoint.y - memberLine.StartPoint.y) + memberLine.StartPoint.y;
                    sz = (j * ll) * (memberLine.EndPoint.z - memberLine.StartPoint.z) + memberLine.StartPoint.z;

                    ex = (j * ll) * (memberLine2.EndPoint.x - memberLine2.StartPoint.x) + memberLine2.StartPoint.x;
                    ey = (j * ll) * (memberLine2.EndPoint.y - memberLine2.StartPoint.y) + memberLine2.StartPoint.y;
                    ez = (j * ll) * (memberLine2.EndPoint.z - memberLine2.StartPoint.z) + memberLine2.StartPoint.z;

                    aline1.StartPoint = new gPoint(ex, ey, ez);
                    aline1.EndPoint = new gPoint(sx, sy, sz);
                    aline1.Transformby(document.User2WorldMatrix);
                    document.UndoHistory.PopEnable();
                    document.ActionDrawFigure(aline1);
                }
                #endregion

                #region DRAW Load Value

                vdText loadVal = new vdText();
                loadVal.SetUnRegisterDocument(document);
                loadVal.setDocumentDefaults();
                loadVal.InsertionPoint = new gPoint(14.5d, 13.37d);
                loadVal.Height = 0.8d;
                loadVal.TextString = mLoad.Direction.ToString() + " " + mLoad.Value.ToString();
                document.ActiveLayOut.Entities.AddItem(loadVal);

                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(document);
                #endregion
            }
            #endregion
        }
        public void ShowMemberLoad(vdDocument document, int MemberNo, ASTRADoc astDoc)
        {

            MemberLoad mLoad = astDoc.MemberLoads[astDoc.MemberLoads.IndexOf(MemberNo)];

            #region Draw Member Line
            vdLine memberLine = new vdLine();
            memberLine.SetUnRegisterDocument(document);
            memberLine.setDocumentDefaults();
            memberLine.StartPoint = new gPoint(10, 12, 0);
            memberLine.EndPoint = new gPoint(20, 12, 0);
            document.ActiveLayOut.Entities.AddItem(memberLine);
            #endregion

            #region Draw Member Start Node Text

            vdText startNodeText = new vdText();
            startNodeText.SetUnRegisterDocument(document);
            startNodeText.setDocumentDefaults();
            startNodeText.InsertionPoint = new gPoint(9.2710d, 11.1111d, 0);
            startNodeText.Height = 0.7d;
            startNodeText.TextString = mLoad.Member.StartNode.NodeNo.ToString();
            startNodeText.PenColor = new vdColor(Color.Magenta);
            document.ActiveLayOut.Entities.AddItem(startNodeText);

            #endregion

            #region Draw Member No Text
            vdText memberNoText = new vdText();
            memberNoText.SetUnRegisterDocument(document);
            memberNoText.setDocumentDefaults();
            memberNoText.InsertionPoint = new gPoint(14.5224d, 11.1111d, 0);
            memberNoText.Height = 0.7d;
            memberNoText.TextString = mLoad.Member.MemberNo.ToString();
            memberNoText.PenColor = new vdColor(Color.Blue);
            document.ActiveLayOut.Entities.AddItem(memberNoText);
            #endregion

            #region Draw Member End Node Text
            vdText endNodeText = new vdText();
            endNodeText.SetUnRegisterDocument(document);
            endNodeText.setDocumentDefaults();
            endNodeText.InsertionPoint = new gPoint(20.18d, 11.1111d);
            endNodeText.Height = 0.7d;
            endNodeText.TextString = mLoad.Member.EndNode.NodeNo.ToString();
            endNodeText.PenColor = new vdColor(Color.Magenta);
            document.ActiveLayOut.Entities.AddItem(endNodeText);
            #endregion

            #region Draw Rectangle
            vdRect rect = new vdRect();
            rect.SetUnRegisterDocument(document);
            rect.setDocumentDefaults();
            rect.InsertionPoint = new gPoint(8.1085d, 9.030d);
            rect.Width = 13.406d;
            rect.Height = 6.5206d;
            document.ActiveLayOut.Entities.AddItem(rect);

            #endregion


            #region Draw Member Line
            vdLine memberLine2 = new vdLine();
            memberLine2.SetUnRegisterDocument(document);
            memberLine2.setDocumentDefaults();
            memberLine2.StartPoint = new gPoint(10, 13, 0);
            memberLine2.EndPoint = new gPoint(20, 13, 0);
            document.ActiveLayOut.Entities.AddItem(memberLine2);
            #endregion

            #region Draw Member Load

            //ASTRAMemberLoad aline = new ASTRAMemberLoad();
            //document.ActionLayout.Entities.AddItem(aline);
            //document.UndoHistory.PushEnable(false);
            //aline.InitializeProperties();
            //aline.setDocumentDefaults();
            //aline.StartPoint = memberLine2.StartPoint;
            //aline.EndPoint = memberLine.StartPoint;
            //aline.arrowSize = 0.3;
            //aline.Transformby(document.User2WorldMatrix);
            //document.UndoHistory.PopEnable();
            //document.ActionDrawFigure(aline);


            //double sx, sy, sz, ex, ey, ez;
            //sx = sy = sz = ex = ey = ez = 0.0d;
            //double ll = ((memberLine2.Length() / 8.0d) / memberLine.Length());
            //for (int j = 1; j <= 8; j++)
            //{
            //    ASTRAMemberLoad aline1 = new ASTRAMemberLoad();
            //    document.ActionLayout.Entities.AddItem(aline1);
            //    document.UndoHistory.PushEnable(false);
            //    aline1.InitializeProperties();
            //    aline1.setDocumentDefaults();
            //    aline1.arrowSize = 0.4d;

            //    sx = (j * ll) * (memberLine.EndPoint.x - memberLine.StartPoint.x) + memberLine.StartPoint.x;
            //    sy = (j * ll) * (memberLine.EndPoint.y - memberLine.StartPoint.y) + memberLine.StartPoint.y;
            //    sz = (j * ll) * (memberLine.EndPoint.z - memberLine.StartPoint.z) + memberLine.StartPoint.z;

            //    ex = (j * ll) * (memberLine2.EndPoint.x - memberLine2.StartPoint.x) + memberLine2.StartPoint.x;
            //    ey = (j * ll) * (memberLine2.EndPoint.y - memberLine2.StartPoint.y) + memberLine2.StartPoint.y;
            //    ez = (j * ll) * (memberLine2.EndPoint.z - memberLine2.StartPoint.z) + memberLine2.StartPoint.z;

            //    aline1.StartPoint = new gPoint(ex, ey, ez);
            //    aline1.EndPoint = new gPoint(sx, sy, sz);
            //    aline1.Transformby(document.User2WorldMatrix);
            //    document.UndoHistory.PopEnable();
            //    document.ActionDrawFigure(aline1);
            //}
            //#endregion

            //#region DRAW Load Value

            //vdText loadVal = new vdText();
            //loadVal.SetUnRegisterDocument(document);
            //loadVal.setDocumentDefaults();
            //loadVal.InsertionPoint = new gPoint(14.5d, 13.37d);
            //loadVal.Height = 0.8d;
            //loadVal.TextString = mLoad.Value.ToString();
            //document.ActiveLayOut.Entities.AddItem(loadVal);


            //VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(document);


            #endregion

            #region Draw Joint Load

            ASTRAMemberLoad StartJointLoad = new ASTRAMemberLoad();
            document.ActionLayout.Entities.AddItem(StartJointLoad);
            document.UndoHistory.PushEnable(false);
            StartJointLoad.InitializeProperties();
            StartJointLoad.setDocumentDefaults();
            StartJointLoad.arrowSize = 0.4d;

            StartJointLoad.StartPoint = memberLine.StartPoint;

            StartJointLoad.EndPoint = new gPoint(memberLine.StartPoint.x, memberLine.StartPoint.y - 2.0d, 0.0d);
            StartJointLoad.Transformby(document.User2WorldMatrix);
            document.UndoHistory.PopEnable();
            document.ActionDrawFigure(StartJointLoad);


            ASTRAMemberLoad EndJointLoad = new ASTRAMemberLoad();
            document.ActionLayout.Entities.AddItem(EndJointLoad);
            document.UndoHistory.PushEnable(false);
            EndJointLoad.InitializeProperties();
            EndJointLoad.setDocumentDefaults();
            EndJointLoad.arrowSize = 0.4d;

            EndJointLoad.StartPoint = memberLine.EndPoint;

            EndJointLoad.EndPoint = new gPoint(memberLine.EndPoint.x, memberLine.EndPoint.y - 2.0d, 0.0d);
            EndJointLoad.Transformby(document.User2WorldMatrix);
            document.UndoHistory.PopEnable();
            document.ActionDrawFigure(EndJointLoad);

            #endregion
        }
        public void ShowMemberJointLoad(vdDocument memberDoc, int MemberNo, ASTRADoc astDoc, int LoadCase)
        {

            MemberIncidence mInci = astDoc.Members[astDoc.Members.IndexOf(MemberNo)];

            #region Draw Member Line
            vdLine memberLine = new vdLine();
            memberLine.SetUnRegisterDocument(memberDoc);
            memberLine.setDocumentDefaults();
            memberLine.StartPoint = new gPoint(10, 12, 0);
            memberLine.EndPoint = new gPoint(20, 12, 0);
            memberDoc.ActiveLayOut.Entities.AddItem(memberLine);
            #endregion

            #region Draw Member Start Node Text

            vdText startNodeText = new vdText();
            startNodeText.SetUnRegisterDocument(memberDoc);
            startNodeText.setDocumentDefaults();
            startNodeText.InsertionPoint = new gPoint(9.2710d, 11.1111d, 0);
            startNodeText.Height = 0.7d;
            startNodeText.TextString = mInci.StartNode.NodeNo.ToString();
            startNodeText.PenColor = new vdColor(Color.Magenta);
            memberDoc.ActiveLayOut.Entities.AddItem(startNodeText);

            #endregion

            #region Draw Member No Text
            vdText memberNoText = new vdText();
            memberNoText.SetUnRegisterDocument(memberDoc);
            memberNoText.setDocumentDefaults();
            memberNoText.InsertionPoint = new gPoint(14.5224d, 11.1111d, 0);
            memberNoText.Height = 0.7d;
            memberNoText.TextString = mInci.MemberNo.ToString();
            memberNoText.PenColor = new vdColor(Color.Blue);
            memberDoc.ActiveLayOut.Entities.AddItem(memberNoText);
            #endregion

            #region Draw Member End Node Text
            vdText endNodeText = new vdText();
            endNodeText.SetUnRegisterDocument(memberDoc);
            endNodeText.setDocumentDefaults();
            endNodeText.InsertionPoint = new gPoint(20.18d, 11.1111d);
            endNodeText.Height = 0.7d;
            endNodeText.TextString = mInci.EndNode.NodeNo.ToString();
            endNodeText.PenColor = new vdColor(Color.Magenta);
            memberDoc.ActiveLayOut.Entities.AddItem(endNodeText);
            #endregion

            #region Draw Rectangle
            vdRect rect = new vdRect();
            rect.SetUnRegisterDocument(memberDoc);
            rect.setDocumentDefaults();
            rect.InsertionPoint = new gPoint(7.55d, 10.424d);
            rect.Width = 15.2406d;
            rect.Height = 6.2206d;
            memberDoc.ActiveLayOut.Entities.AddItem(rect);

            #endregion

            #region Draw Joint Load

            int jIndex = astDoc.JointLoads.IndexOf(mInci.StartNode.NodeNo, LoadCase);

            if (jIndex != -1)
            {
                ASTRAMemberLoad StartJointLoad = new ASTRAMemberLoad();
                memberDoc.ActionLayout.Entities.AddItem(StartJointLoad);
                memberDoc.UndoHistory.PushEnable(false);
                StartJointLoad.InitializeProperties();
                StartJointLoad.setDocumentDefaults();
                StartJointLoad.arrowSize = 0.7d;
                StartJointLoad.PenColor = new vdColor(Color.Red);
                StartJointLoad.StartPoint = new gPoint(memberLine.StartPoint.x, memberLine.StartPoint.y + 2.0d, 0.0d);
                StartJointLoad.EndPoint = memberLine.StartPoint;
                StartJointLoad.Transformby(memberDoc.User2WorldMatrix);
                memberDoc.UndoHistory.PopEnable();
                memberDoc.ActionDrawFigure(StartJointLoad);

                vdText startJointLoadVal = new vdText();
                startJointLoadVal.SetUnRegisterDocument(memberDoc);
                startJointLoadVal.setDocumentDefaults();
                startJointLoadVal.TextString = astDoc.JointLoads[jIndex].Direction.ToString() + " " + astDoc.JointLoads[jIndex].Value.ToString();
                startJointLoadVal.InsertionPoint = new gPoint(StartJointLoad.StartPoint.x - 0.5,
                    StartJointLoad.StartPoint.y + 0.5d);
                startJointLoadVal.Height = 0.6d;
                memberDoc.ActiveLayOut.Entities.AddItem(startJointLoadVal);
            }

            jIndex = astDoc.JointLoads.IndexOf(mInci.EndNode.NodeNo, LoadCase);
            if (jIndex != -1)
            {
                ASTRAMemberLoad EndJointLoad = new ASTRAMemberLoad();
                memberDoc.ActionLayout.Entities.AddItem(EndJointLoad);
                memberDoc.UndoHistory.PushEnable(false);
                EndJointLoad.InitializeProperties();
                EndJointLoad.setDocumentDefaults();
                EndJointLoad.arrowSize = 0.7d;
                EndJointLoad.PenColor = new vdColor(Color.Red);
                EndJointLoad.StartPoint = new gPoint(memberLine.EndPoint.x, memberLine.EndPoint.y + 2.0d, 0.0d);
                EndJointLoad.EndPoint = memberLine.EndPoint;
                EndJointLoad.Transformby(memberDoc.User2WorldMatrix);
                memberDoc.UndoHistory.PopEnable();
                memberDoc.ActionDrawFigure(EndJointLoad);


                vdText endJointLoadVal = new vdText();
                endJointLoadVal.SetUnRegisterDocument(memberDoc);
                endJointLoadVal.setDocumentDefaults();
                endJointLoadVal.TextString = astDoc.JointLoads[jIndex].Direction.ToString() + " " + astDoc.JointLoads[jIndex].Value.ToString();
                endJointLoadVal.InsertionPoint = new gPoint(EndJointLoad.StartPoint.x - 1.8,
                    EndJointLoad.StartPoint.y + 0.5d);
                endJointLoadVal.Height = 0.6d;
                memberDoc.ActiveLayOut.Entities.AddItem(endJointLoadVal);

            }
            memberDoc.Redraw(true);

            #endregion
            //if (IsShowText)
            //{
            //    for (int i = 0; i < memberDoc.ActiveLayOut.Entities.Count; i++)
            //    {
            //        vdFigure vFig = memberDoc.ActiveLayOut.Entities[i] as vdFigure;

            //        vdText vTxt = vFig as vdText;
            //        if (vTxt != null)
            //        {
            //            if (vTxt.TextString == "MEMBER DIAGRAM")
            //            {
            //                vTxt.TextString = "JOINT LOAD";
            //            }
            //        }
            //    }
            //}
        }

        public MemberLoadCollection GetAllMemberLoad(int MemberNo,int LoadCase)
        {
            MemberLoadCollection memCol = new MemberLoadCollection();
            MemberLoad mLoad = new MemberLoad();

            for (int i = 0; i < list.Count; i++)
            {
                if ((list[i].Member.MemberNo == MemberNo) && (list[i].LoadCase == LoadCase))
                {
                    memCol.Add(list[i]);
                }
            }
            return memCol;
        }
    }

    public class CombinationLoad
    {
        public CombinationLoad(int load_no, double factor)
        {
            Load_No = load_no;
            Load_Factor = factor;
        }
        public int Load_No { get; set; }
        public double Load_Factor { get; set; }

        public override string ToString()
        {
            return string.Format("({0}) X Load {1}", (Load_Factor > 10 ? Load_Factor.ToString("f3") : Load_Factor.ToString("f3")), Load_No);
        }
    }
    public class LoadDefine
    {
        public LoadDefine()
        {
            LoadCase = 0;
            LoadTitle = "";
            Selfweight = "";

            MemberLoadList = new List<string>();
            JointWeightList = new List<string>();
            //JointLoadList = new List<string>();
            JointLoadList = new List<SLoad>();
            AreaLoadList = new List<string>();
            FloorLoadList = new List<string>();
            TempLoadList = new List<string>();
            CominationLoadList = new List<string>();
            RepeatLoadList = new List<string>();
            ElementLoadList = new List<string>();
            Combinations = new List<CombinationLoad>();
            SupportDisplacements = new List<string>();
            
        }
        public int LoadCase { get; set; }
        public string LoadTitle { get; set; }
        public string Selfweight { get; set; }
        public List<string> MemberLoadList { get; set; }
        public List<string> JointWeightList { get; set; }
        //public List<string> JointLoadList { get; set; }
        public List<SLoad> JointLoadList { get; set; }
        public List<string> AreaLoadList { get; set; }
        public List<string> FloorLoadList { get; set; }
        public List<string> TempLoadList { get; set; }
        public List<string> CominationLoadList { get; set; }
        public List<string> RepeatLoadList { get; set; }
        public List<string> ElementLoadList { get; set; }
        public List<string> SupportDisplacements { get; set; }
             

        public List<CombinationLoad> Combinations { get; set; }
        public void Set_Combination()
        {
            Combinations.Clear();

            MyStrings mlist = null;
            CombinationLoad cl = null;
            foreach (var item in CominationLoadList)
            {
                mlist = new MyStrings(item, ' ');
                for (int i = 1; i < mlist.Count; i += 2)
                {
                    cl = new CombinationLoad(mlist.GetInt(i - 1), mlist.GetDouble(i));
                    Combinations.Add(cl);
                }
            }
            foreach (var item in RepeatLoadList)
            {
                mlist = new MyStrings(item, ' ');
                for (int i = 1; i < mlist.Count; i += 2)
                {
                    cl = new CombinationLoad(mlist.GetInt(i - 1), mlist.GetDouble(i));
                    Combinations.Add(cl);
                }
            }
        }
    }
    public class SLoad
    {
        public SLoad(string val)
        {
            Value = val;
            Comment = "";
        }
        public string Value { get; set; }
        public string Comment { get; set; }

        public static implicit operator string(SLoad d)
        {
            return d.Value;
        }
        //  User-defined conversion from double to Digit
        public static implicit operator SLoad(string val)
        {
            return new SLoad(val);
        }
    }


}
