using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace HEADSNeed.ASTRA.ASTRAClasses
{
    //Jay Shree Hari
    //Jay Shree Ganesh
    public class LoadDeflection
    {
        #region Member Variable

        Thread thd = null;
        ASTRADoc AST_DOC = null;
        string reportFilePath = "";
        int lastNodeNo = 0;
        int maxLoadCase = 0;
        bool IsFind = false;
        double fact = 0.0d;
        NodeDisplacementCollection ndispCol = null;
        Hashtable hashDeflections = new Hashtable();
        ProgressBar pbLoadingData = null;
        double maxTx, maxTy, maxTz;
        bool withFact = true;

        #endregion


        #region ctor
        public LoadDeflection(ASTRADoc astDoc)
        {
            withFact = true;
            maxTx = maxTy = maxTz = 0.0d;
            this.AST_DOC = astDoc;
            ndispCol = new NodeDisplacementCollection();
            reportFilePath = Path.Combine(Path.GetDirectoryName(astDoc.FileName), "ANALYSIS_REP.TXT");
            this.pbLoadingData = new ProgressBar();
            if (File.Exists(reportFilePath))
                ReadFromFile();
            //SetHashTable();
            RunThread();
        }
        public LoadDeflection(ASTRADoc astDoc, ProgressBar pb)
        {
            withFact = true;
            maxTx = maxTy = maxTz = 0.0d;
            this.AST_DOC = astDoc;
            ndispCol = new NodeDisplacementCollection();
            reportFilePath = Path.Combine(Path.GetDirectoryName(astDoc.FileName), "ANALYSIS_REP.TXT");
            this.pbLoadingData = pb;
            if (File.Exists(reportFilePath))
                ReadFromFile();
            RunThread();
            //SetHashTable();
        }
        public LoadDeflection(ASTRADoc astDoc, ProgressBar pb, bool isWithFact)
        {
            withFact = isWithFact;
            maxTx = maxTy = maxTz = 0.0d;
            this.AST_DOC = astDoc;
            ndispCol = new NodeDisplacementCollection();
            reportFilePath = astDoc.AnalysisFileName;
            this.pbLoadingData = pb;
            if (File.Exists(reportFilePath))
                ReadFromFile();
            RunThread();
            //SetHashTable();
        }
        #endregion

        #region Private Methods
        private void ReadFromFile()
        {
            reportFilePath = AST_DOC.AnalysisFileName;
            if (!File.Exists(reportFilePath))
                return;

            MyStrings mList = null;
            List<string> lst = new List<string>(File.ReadAllLines(reportFilePath));
            string ss = "";

            for (int i = 0; i < lst.Count; i++)
            {
                ss = MyStrings.RemoveAllSpaces(lst[i]);
                if (ss.Contains("P DELTA ANALYSIS FOR LOAD CASE"))
                {
                    ss = MyStrings.RemoveAllSpaces(lst[i]);
                    break;
                }

                if (ss.Contains("NUMBER VECTOR TRANSLATION TRANSLATION TRANSLATION ROTATION ROTATION ROTATION"))
                {
                    IsFind = true; continue;
                }
                if (ss.Contains("NUMBER CASE TRANSLATION TRANSLATION TRANSLATION ROTATION ROTATION ROTATION"))
                {
                    IsFind = true; continue;
                }
                if (ss.Contains("*******")) IsFind = false;

                if (IsFind && ss != "")
                {
                    SET_NodalDisplacement(ss);
                }
            }
            mList = null;
            lst.Clear();
            GC.Collect();

            if (MaxLoadCase > 0)
                SetDeflectionFactor(1);
        }
        private void SET_NodalDisplacement(string text)
        {
            NodeDisplacement nd = new NodeDisplacement();
            MyStrings mList = new MyStrings(MyStrings.RemoveAllSpaces(text), ' ');

            int indx = 0;
            if (mList.Count == 8)
            {
                indx = 0;
                nd.Node.NodeNo = mList.GetInt(indx); indx++;
                lastNodeNo = nd.Node.NodeNo;

                nd.LoadCase = mList.GetInt(indx); indx++;
                maxLoadCase = nd.LoadCase;

                nd.Tx = mList.GetDouble(indx); indx++;
                nd.Ty = mList.GetDouble(indx); indx++;
                nd.Tz = mList.GetDouble(indx); indx++;
                nd.Rx = mList.GetDouble(indx); indx++;
                nd.Ry = mList.GetDouble(indx); indx++;
                nd.Rz = mList.GetDouble(indx); indx++;
            }
            else if (mList.Count == 7)
            {
                indx = 0;
                nd.Node.NodeNo = lastNodeNo;
                nd.LoadCase = mList.GetInt(indx); indx++;
                maxLoadCase = nd.LoadCase;

                nd.Tx = mList.GetDouble(indx); indx++;
                nd.Ty = mList.GetDouble(indx); indx++;
                nd.Tz = mList.GetDouble(indx); indx++;
                nd.Rx = mList.GetDouble(indx); indx++;
                nd.Ry = mList.GetDouble(indx); indx++;
                nd.Rz = mList.GetDouble(indx); indx++;
            }

            #region Calculate Proportionate
            /**/
            double x, y, z, prp, dif;

            x =  y =  z =  prp = dif = 0.0;

            if (nd.Tx < nd.Ty && nd.Tx < nd.Tz)
            {
                x = nd.Tx;
                if (x < 1.0d)
                {
                    dif = 1.0d - x;
                    prp = x / dif;
                    x = 1.0d;
                    y = y * prp;
                    z = z * prp;

                    //nd.Tx = x;
                    //nd.Ty += y;
                    //nd.Tz += z;
                }
            }
            else if (nd.Ty < nd.Tx && nd.Ty < nd.Tz)
            {
                y = nd.Ty;
                if (y < 1.0)
                {
                    dif = 1.0d - y;
                    prp = y / dif;
                    y = 1.0d;
                    x = x * prp;
                    z = z * prp;


                    //nd.Tx += x;
                    //nd.Ty = y;
                    //nd.Tz += z;
                }
            }
            else if (nd.Tz < nd.Tx && nd.Tz < nd.Ty)
            {
                z = nd.Tz;
                if (z < 1.0d)
                {
                    dif = 1.0d - z;
                    prp = z / dif;
                    z = 1.0d;
                    y = y * prp;
                    x = x * prp;

                    //nd.Tx += x;
                    //nd.Ty += y;
                    //nd.Tz = z;
                }
            }
            /*
            if (nd.Tx > 0.0)
            {
                nd.Tx += x;
            }
            else
            {
                nd.Tx -= x;
            }

            if (nd.Ty > 0.0)
            {
                nd.Ty += y;
            }
            else
            {
                nd.Tx -= y;
            }

            if (nd.Tz > 0.0)
            {
                nd.Tz += z;
            }
            else
            {
                nd.Tz -= z;
            }
            /**/
            double factor = 1d;

            nd.Tx = nd.Tx * factor;
            nd.Ty = nd.Ty * factor;
            nd.Tz = nd.Tz * factor;
            #endregion

            //Chiranjit [2014 08 14]
            if(nd.Ty > 0)
                nd.Ty = nd.Ty * -1;


            
            ndispCol.Add(nd);
        }
        
        void SetDeflectionFactor(int loadCase)
        {
            double max = 0.0d;
            int i =0;
            maxTx = 0.0d;
            maxTy = 0.0d;
            maxTz = 0.0d;
            for (i = 0; i < ndispCol.Count; i++)
            {
                if (ndispCol[i].LoadCase == loadCase)
                {
                    if (maxTx < Math.Abs(ndispCol[i].Tx))
                    {
                        maxTx = Math.Abs(ndispCol[i].Tx);
                    }
                    if (maxTy < Math.Abs(ndispCol[i].Ty))
                    {
                        maxTy = Math.Abs(ndispCol[i].Ty);
                    }
                    if (maxTz < Math.Abs(ndispCol[i].Tz))
                    {
                        maxTz = Math.Abs(ndispCol[i].Tz);
                    }
                }
            }
            if (maxTx > maxTy && maxTx > maxTz)
                max = maxTx;
            else if (maxTy > maxTx && maxTy > maxTz)
                max = maxTy;
            else if (maxTz > maxTx && maxTz > maxTy)
                max = maxTz;
            fact = 1 / max;

            //for (i = 0; i < ndispCol.Count; i++)
            //{
            //    if (ndispCol[i].LoadCase == loadCase)
            //    {
            //        ndispCol[i].Tx *= fact;
            //        ndispCol[i].Ty *= fact;
            //        ndispCol[i].Tz *= fact;
            //    }
            //}
            //return fact;

        }

        void RunThread()
        {
            //MyList
            ThreadStart ths = new ThreadStart(SetHashTable);
            thd = new Thread(ths);
            thd.Start();
            //thd.Join();
        }
        private void SetHashTable()
        {
            string load = "";
            pbLoadingData.Maximum = 100;
            pbLoadingData.Minimum = 0;
            SetProgress sp = new SetProgress(Set_Progress_Bar);

            int val = 0;

            try
            {
                for (int i = 0; i <= maxLoadCase; i++)
                {
                    load = "LOAD_" + i;
                    hashDeflections.Add(load, Get_ASTRADoc(i));
                    val = (int)(((double)i / (double)maxLoadCase) * 100.0);

                    pbLoadingData.Invoke(sp, pbLoadingData, val);
                    //pbLoadingData.Value = (int)(i / maxLoadCase) * 100;
                }
            }
            catch (Exception ex)
            {
            }
        }
        public delegate void SetProgress(ProgressBar pb, int val);
        public void Set_Progress_Bar(ProgressBar pb, int val)
        {
            if (val >= pb.Minimum && val <= pb.Maximum)
            {
                pb.Value = val;
            }
            if (val == pb.Maximum)
            {
                pb.Visible = false;
            }
        }
        private ASTRADoc Get_ASTRADoc(int loadCase)
        {
            ASTRADoc astDoc = new ASTRADoc(AST_DOC.FileName);

            if (loadCase == 0) return astDoc;

            int indx = 0;
            SetDeflectionFactor(loadCase);
            for (int i = 0; i < ndispCol.Count; i++)
            {
                if (ndispCol[i].LoadCase == loadCase)
                {
                    indx = astDoc.Joints.IndexOf(ndispCol[i].Node);
                    if (indx != -1)
                    {
                        if (withFact)
                        {
                            if (fact == 0) fact = 1;
                            //fact = 1;
                            astDoc.Joints[indx].Point.x += ndispCol[i].Tx * fact;
                            astDoc.Joints[indx].Point.y += ndispCol[i].Ty * fact;
                            astDoc.Joints[indx].Point.z += ndispCol[i].Tz * fact;
                        }
                        else
                        {
                            astDoc.Joints[indx].Point.x += ndispCol[i].Tx;
                            astDoc.Joints[indx].Point.y += ndispCol[i].Ty;
                            astDoc.Joints[indx].Point.z += ndispCol[i].Tz;
                        }
                    }
                }
            }
            //AST_DOC.JointLoads = new JointLoadCollection();
            //AST_DOC.JointLoads = AST_DOC_ORG.JointLoads;
            //AST_DOC.JointLoads.CopyCoordinates(AST_DOC.Joints);


            astDoc.Members.CopyJointCoordinates(astDoc.Joints);
            return astDoc;
        }
        public ASTRADoc Get_ASTRADoc(int loadCase, double factor)
        {
            ASTRADoc astDoc = new ASTRADoc(AST_DOC.FileName);

            if (loadCase == 0) return astDoc;
            int indx = 0;
            Factor = factor;
            for (int i = 0; i < ndispCol.Count; i++)
            {
                if (ndispCol[i].LoadCase == loadCase)
                //if (ndispCol[i].LoadCase <= loadCase)
                {
                    indx = astDoc.Joints.IndexOf(ndispCol[i].Node);
                    if (indx != -1)
                    {
                        astDoc.Joints[indx].Point.x += ndispCol[i].Tx * fact;
                        astDoc.Joints[indx].Point.y += ndispCol[i].Ty * fact;
                        astDoc.Joints[indx].Point.z += ndispCol[i].Tz * fact;
                    }
                }
            }
            //AST_DOC.JointLoads = new JointLoadCollection();
            //AST_DOC.JointLoads = AST_DOC_ORG.JointLoads;
            //AST_DOC.JointLoads.CopyCoordinates(AST_DOC.Joints);


            astDoc.Members.CopyJointCoordinates(astDoc.Joints);
            return astDoc;
        }
        #endregion

        #region Public Methods

        #endregion
        #region Public Property

        public NodeDisplacementCollection Node_Displacements
        {
            get
            {
                return ndispCol;
            }

        }

        public Thread ThisThread
        {
            get
            {
                return thd;
            }
        }
        public Hashtable HashDeflection
        {
            get
            {
                return hashDeflections;
            }
        }
        public int MaxLoadCase
        {
            get
            {
                return maxLoadCase;

            }
        }
        public double Factor
        {
            get
            {
                return fact;
            }
            set
            {
                if (value > 0.0)
                    fact = value;
            }
        }
        public bool IsWithFact
        {
            get
            {
                return withFact;
            }
            set
            {
                withFact = value;
            }

        }
        #endregion
        ~LoadDeflection()
        {
            try
            {
                thd.Abort();
            }
            catch (Exception ex)
            {
            }
            try
            {
                thd = null;
                reportFilePath = null;
                //lastNodeNo = null;
                //maxLoadCase = null;
                //IsFind = null;
                //fact = null;
                ndispCol = null; 
                hashDeflections = null;
                pbLoadingData.Visible = false;
                //maxTx = null;
                //maxTy = null;
                //maxTz = null;
            }
            catch (Exception ex)
            {
            }
        }
    }

}
