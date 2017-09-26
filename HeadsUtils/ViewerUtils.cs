using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HeadsUtils.Interfaces;

namespace HeadsUtils
{
    /// <summary>
    /// This class is used as an utility class.
    /// </summary>
    public class ViewerUtils
    {

        private ViewerUtils()
        {

        }
   
        public static void DeleteEntitiesByLabel(IHeadsDocument doc, string strLabel, bool bCompaireAll)
        {
            IHdEntity[] entitis = doc.Entities;
            foreach (IHdEntity ent in entitis)
            {
                bool bDel = false;
                if (bCompaireAll)
                {
                    if (ent.Label == strLabel)
                    {
                        bDel = true;
                    }                    
                }
                else
                {
                    if (ent.Label.StartsWith(strLabel))
                    {
                        bDel = true;
                    }
                }
                if (bDel)
                {
                    ent.Delete();
                }
            }
        }
        
        public static bool ReadModelLstFile(string strFilePath, ref List<ClSTtype> listData)
        {
            listData.Clear();
            if (File.Exists(strFilePath))
            {                
                BinaryReader br = new BinaryReader(new FileStream(strFilePath, FileMode.Open, FileAccess.Read), Encoding.Default);
                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    ClSTtype lstdata = ClSTtype.FromStream(br);                  
                    listData.Add(lstdata);                 
                }
                br.Close();
                return true;
            }      
            return false;
        }

        public static void WriteModelLstFile(string strFilePath, List<ClSTtype> listData)
        {
            BinaryWriter bw = new BinaryWriter(new FileStream(strFilePath, FileMode.Create), Encoding.Default);
            foreach (ClSTtype lstdata in listData)
            {
                lstdata.ToStream(bw);
            }
            bw.Close();
        }

        public static bool ReadModelFilFile(string strPath, ref List<CLabtype> listfil)
        {
            listfil.Clear();
            bool bSuccess = false;
            if (File.Exists(strPath))
            {
                BinaryReader br = new BinaryReader(new FileStream(strPath, FileMode.Open, FileAccess.Read), Encoding.Default);
                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    CLabtype lab = CLabtype.FromStream(br);
                    listfil.Add(lab);
                }
                br.Close();

                bSuccess = true;
            }

            return bSuccess;
        }

        public static void WriteModelFilFile(string strPath, List<CLabtype> listfil)
        {
            BinaryWriter bw = new BinaryWriter(new FileStream(strPath, FileMode.Create), Encoding.Default);
            for (int i = 0; i < listfil.Count; i++)
            {
                CLabtype lab = listfil[i];
                lab.ToStream(bw);                
            }
            bw.Close();
        }

        public static bool DeleteFileIfExists(string strFile)
        {
            bool bSuccess = false;
            if (System.IO.File.Exists(strFile))
            {
                try
                {
                    System.IO.File.Delete(strFile);
                    bSuccess = true;
                }
                catch
                {
                    //Do nothing
                }
            }
            return bSuccess;
        }

        public static void RenameFile(string strFileSrc, string strFileNameNew)
        {
            if (System.IO.File.Exists(strFileSrc))
            {
                FileInfo finfo = new FileInfo(strFileSrc);
                string strNewFilePath = finfo.DirectoryName;
                
                Path.Combine(strNewFilePath, strFileNameNew);

                System.IO.File.Copy(strFileSrc, strNewFilePath, true);
                System.IO.File.Delete(strFileSrc);
            }
        }

        public static string ConvertCharArrayToString(byte[] source)
        {
            string str = System.Text.Encoding.Default.GetString(source);
            if (str.IndexOf('\0') != -1)
            {
                str = str.Substring(0, str.IndexOf('\0'));
            }            
            return str;
        }

        public static byte[] ConvertStringToByteArray(string str, long lSize)
        {
            byte[] srcbytes  = System.Text.Encoding.Default.GetBytes(str);
            byte[] byteArrDesti = new byte[lSize];

            for (long lIndex = 0; lIndex < byteArrDesti.LongLength; lIndex++)
            {
                if (lIndex < srcbytes.LongLength)
                {
                    byteArrDesti[lIndex] = srcbytes[lIndex];
                }
                else
                {
                    byteArrDesti[lIndex] = 0;
                }
            }

            return byteArrDesti;
        }
          
        public static double GetAngle(CPoint3D pt1, CPoint3D pt2, bool BearingFlg /*= FALSE*/) //Optional 
        {
            double th;
            double temp;
            double B2 = 0;
            double x1 = pt1.X, y1 = pt1.Y;
            double x2 = pt2.X, y2 = pt2.Y;

            if (y1 == y2 && x2 > x1)
            {
                if (BearingFlg)
                    B2 = 90;
                else
                    B2 = 0;
            }
            else if (y1 == y2 && x2 < x1)
            {
                if (BearingFlg)
                    B2 = 270;
                else
                    B2 = 180;
            }
            else if (x1 == x2 && y2 > y1)
            {
                if (BearingFlg)
                    B2 = 0;
                else
                    B2 = 90;
            }
            else if (x1 == x2 && y2 < y1)
            {
                if (BearingFlg)
                    B2 = 180;
                else
                    B2 = 270;
            }
            else
            {
                temp = (y2 - y1) / (x2 - x1);
                th = Math.Atan(temp);
                th = Math.Abs((th * 180) / Math.PI);

                if (BearingFlg)
                {
                    if (x2 > x1 && y2 > y1)
                        B2 = 90 - th;
                    else if (x2 > x1 && y2 < y1)
                        B2 = 90 + th;
                    else if (x2 < x1 && y2 < y1)
                        B2 = 270 - th;
                    else if (x2 < x1 && y2 > y1)
                        B2 = 270 + th;
                }
                else
                {
                    if (x2 > x1 && y2 > y1)
                        B2 = th;
                    else if (x2 > x1 && y2 < y1)
                        B2 = 360 - th;
                    else if (x2 < x1 && y2 < y1)
                        B2 = 180 + th;
                    else if (x2 < x1 && y2 > y1)
                        B2 = 180 - th;
                }
            }


            if (BearingFlg)
            {
                if (B2 < 0)
                    B2 = B2 + 360;
                else if (B2 > 360)
                    B2 = B2 - 360;
            }

            return B2;
        }

        public static double DegreeToRadian(double degree)
        {
            return degree * Math.PI / 180;
        }        
    }
}
