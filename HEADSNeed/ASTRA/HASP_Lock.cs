using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Aladdin.Hasp;

namespace HEADSNeed.ASTRA
{
    public static class HASP_Lock
    {
        static int Service;
        static int LptNum = 0, SeedCode;
        static int Pass1, Pass2;
        static int p1, p2, p3, p4;
        static int lock_flag;
        static long IDNum;

        static string authorization_code = "864a3142b031011f";

        static string authorization_code_high = "982a3142c031b11f";
        static string authorization_code_structure = "326s3142b031011f";
        static string authorization_code_structure_only = "326s5698b012511f";
        static string authorization_code_bridge = "859b3142b031011f";
        static string authorization_code_all = "529a3142b031011f";

        static string authorization_code_structure_high = "254g8751r031011f";
        static string authorization_code_structure_low = "425s6325d012548r";
        static string authorization_code_bridge_high = "785e9854w023658q";
        static string authorization_code_bridge_low = "365z4127t031011f";

        #region Lock Activation

        #region ASTRA_Pro R18.0

        public static bool Check_ASTRA_Lock_18()
        {
            if (!CheckHasp()) return false;

            if (!IsActivate_18 && Get_Activation() <= 0) return false;



            //else if (Version_Type == eVersionType.High_Value_Version)
            //{
            //    if (!Is_High_Value()) return false;
            //}


            short lock_flag = 0;
            LptNum = 0;

            /**/
            // Memo Lock

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            // Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 18; //for heads
            //p2 == 23
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            if (p2 == 23)
                lock_flag = 1;
            else
            {
                lock_flag = 0;
                return false;
            }

            Service = 3;
            SeedCode = 0;

            p1 = 24; //for heads
            //p2 == 86
            param1 = (object)p1;
            param2 = (object)data;
            param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            if (p2 == 86)
            {
                lock_flag = 1;
            }
            else
            {
                lock_flag = 0;
                return false;
            }

            //if (Version_Type == eVersionType.High_Value_Version)
            //{
            //    if (!Is_High_Value_18()) return false;
            //}

            //if (Version_Type == eVersionType.Low_Value_Version)
            //{
            //    if (Is_High_Value_18()) return true;
            //}


            return true;
        }

        public static void MemoLock_18()
        {
            short lock_flag = 0;
            LptNum = 0;

            /**/
            // Memo Lock

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            //                Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 18; //for heads
            //p2 == 23
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            if (p2 == 23)
                lock_flag = 1;
            else
            {
                lock_flag = 0;
                //MessageBox.Show(this, "Lock not found at any port for ASTRA Release 6.0...!!!", "ASTRA");
                //Application.Exit();
            }

            Service = 3;
            SeedCode = 0;

            p1 = 24; //for heads
            //p2 == 86
            param1 = (object)p1;
            param2 = (object)data;
            param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            if (p2 == 86)
            {
                lock_flag = 1;

            }
            else
            {
                lock_flag = 0;
            }
        }

        public static bool Check_ASTRA_Structure_Lock_18()
        {
            short lock_flag = 0;
            LptNum = 0;

            /**/
            // Memo Lock

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            //	Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 27; // new address for Structural Analysis
            //p2 == 1 //Structure Analysis
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            if (p2 == 1 || p2 == 2)
                lock_flag = 1;
            else
            {
                lock_flag = 0;
                return false;
                //MessageBox.Show(this, "Lock not found at any port for ASTRA Release 6.0...!!!", "ASTRA");
                //Application.Exit();
            }



            //if (Version_Type == eVersionType.High_Value_Version)
            //{
            //    if (!Is_High_Value_18()) return false;
            //}

            //if (Version_Type == eVersionType.Low_Value_Version)
            //{
            //    if (Is_High_Value_18()) return true;
            //}


            return true;
        }
        public static bool Check_ASTRA_Bridge_Lock_18()
        {
            short lock_flag = 0;
            LptNum = 0;

            /**/
            // Memo Lock

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            //	Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 27; // new address for Structural Analysis
            //p2 == 1 //Structure Analysis
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            if (p2 == 0 || p2 == 2 || p2 == 65535) //for Bridge
                lock_flag = 1;
            else
            {
                lock_flag = 0;
                return false;
                //MessageBox.Show(this, "Lock not found at any port for ASTRA Release 6.0...!!!", "ASTRA");
                //Application.Exit();
            }

            return true;
        }
        public static bool IsProfessional_StructuralVersion_18()
        {
            try
            {
                int ps1, ps2, p1, p2, p3, p4;

                ps1 = 4561;
                ps2 = 9261;
                p1 = p2 = p3 = p4 = 0;
                //HASP_Lock f = new HASP_Lock();
                if (CheckHasp())
                {
                    //return f.Check_ASTRA_Lock();
                    if (Check_ASTRA_Lock_18())
                        return Check_ASTRA_Structure_Lock_18();
                }
            }
            catch (Exception ex) { }
            return false;
        }
        public static bool IsProfessional_BridgeVersion_18()
        {
            try
            {
                int ps1, ps2, p1, p2, p3, p4;

                ps1 = 4561;
                ps2 = 9261;
                p1 = p2 = p3 = p4 = 0;
                //HASP_Lock f = new HASP_Lock();
                if (CheckHasp())
                {
                    //return f.Check_ASTRA_Lock();
                    if (Check_ASTRA_Lock_18())
                        return Check_ASTRA_Bridge_Lock_18();
                }
            }
            catch (Exception ex) { }
            return false;
        }

        public static bool Get_Authorization_Code_18()
        {
            short lock_flag = 0;
            LptNum = 0;

            /**/
            // Memo Lock

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            //                Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 10; //for heads
            //p2 == 1 // If Authorizasion Code entered
            //p2 == 0 // If Authorizasion Code not entered
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            //p2 == 1 // If Authorizasion Code entered
            //p2 == 0 // If Authorizasion Code not entered
            if (p2 == 1)
            {
                lock_flag = 1;
            }
            else
            {
                lock_flag = 0;

            }
            return (lock_flag == 1);
        }

        public static bool IsActivate_18
        {
            get
            {
                return Get_Authorization_Code_18();
            }
        }
        public static bool Is_High_Value_18()
        {

            LptNum = 0;

            /**/
            // Memo Lock

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            //Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            //p1=17 //for heads
            p1 = 25;
            //p2 == 1
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;

            return (p2 == 1);
        }

        #endregion ASTRA_Pro R18.0


        #region ASTRA_Pro R19.0


        public static bool Write_ASTRA_Code_18()
        {
            WriteToLock(18, 23);
            WriteToLock(24, 86);
            return true;
        }

        public static bool Write_ASTRA_Code_19()
        {
            WriteToLock(15, 18);
            WriteToLock(14, 54);
            return true;
        }


        public static bool Write_ASTRA_AuthorisedCode_19()
        {
            WriteToLock(8, 1);
            return true;
        }


        public static bool Write_ASTRA_Bridge_Code_19()
        {
            WriteToLock(12, 1);
            return true;
        }
        public static bool Write_ASTRA_Structure_Code_19()
        {
            WriteToLock(7, 1);
            return true;
        }
        public static bool Write_ASTRA_Enterprise_Code_19()
        {
            WriteToLock(13, 1);
            return true;
        }
        public static bool Write_ASTRA_Professional_Code_19()
        {
            WriteToLock(13, 0);
            return true;
        }
        public static bool Check_ASTRA_Lock_19()
        {
            if (!CheckHasp()) return false;

            if (!IsActivate_19 && Get_Activation() <= 0) return false;

            short lock_flag = 0;
            LptNum = 0;

            /**/
            // Memo Lock

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            // Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 15; //for ASTRA Pro R19.0
            //p2 == 18
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            if (p2 == 18)
                lock_flag = 1;
            else
            {
                lock_flag = 0;
                return false;
            }

            Service = 3;
            SeedCode = 0;

            p1 = 14; //for heads
            //p2 == 54
            param1 = (object)p1;
            param2 = (object)data;
            param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            if (p2 == 54)
            {
                lock_flag = 1;
            }
            else
            {
                lock_flag = 0;
                return false;
            }

            //if (Version_Type == eVersionType.High_Value_Version)
            //{
            //    if (!Is_High_Value_19()) return false;
            //}

            //if (Version_Type == eVersionType.Low_Value_Version)
            //{
            //    if (Is_High_Value_19()) return true;
            //}


            return true;
        }
        public static bool Check_ASTRA_Structure_Lock_19()
        {
            short lock_flag = 0;
            LptNum = 0;

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            //	Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 7; // new address for Structural Analysis
            //p2 == 1 //Structure Analysis
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            if (p2 == 1)
                lock_flag = 1;
            else
            {
                lock_flag = 0;
                return false;
            }

            //if (Version_Type == eVersionType.High_Value_Version)
            //{
            //    if (!Is_High_Value_19()) return false;
            //}

            //if (Version_Type == eVersionType.Low_Value_Version)
            //{
            //    if (Is_High_Value_19()) return true;
            //}

            return true;
        }
        public static bool Check_ASTRA_Bridge_Lock_19()
        {
            short lock_flag = 0;
            LptNum = 0;

            /**/
            // Memo Lock

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            //	Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 12; // ASTRA Pro R 19.0
            //p2 == 1 //Structure Analysis
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            if (p2 == 1) //for Bridge
                lock_flag = 1;
            else
            {
                lock_flag = 0;
                return false;
            }


            //if (Version_Type == eVersionType.High_Value_Version)
            //{
            //    if (!Is_High_Value_19()) return false;
            //}

            //if (Version_Type == eVersionType.Low_Value_Version)
            //{
            //    if (Is_High_Value_19()) return true;
            //}

            return true;
        }
        public static bool IsProfessional_StructuralVersion_19()
        {
            try
            {
                int ps1, ps2, p1, p2, p3, p4;

                ps1 = 4561;
                ps2 = 9261;
                p1 = p2 = p3 = p4 = 0;
                //HASP_Lock f = new HASP_Lock();
                if (CheckHasp())
                {
                    //return f.Check_ASTRA_Lock();
                    if (Check_ASTRA_Lock_19())
                        return Check_ASTRA_Structure_Lock_19();
                }
            }
            catch (Exception ex) { }
            return false;
        }
        public static bool IsProfessional_BridgeVersion_19()
        {
            try
            {
                int ps1, ps2, p1, p2, p3, p4;

                ps1 = 4561;
                ps2 = 9261;
                p1 = p2 = p3 = p4 = 0;
                //HASP_Lock f = new HASP_Lock();
                if (CheckHasp())
                {
                    if (Check_ASTRA_Lock_19()) return Check_ASTRA_Bridge_Lock_19();
                }
            }
            catch (Exception ex) { }
            return false;
        }




        public static bool Get_Authorization_Code_19()
        {
            short lock_flag = 0;
            LptNum = 0;

            /**/
            // Memo Lock

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;

            // Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 8; //for heads
            //p2 == 1 // If Authorizasion Code entered
            //p2 == 0 // If Authorizasion Code not entered
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            //p2 == 1 // If Authorizasion Code entered
            //p2 == 0 // If Authorizasion Code not entered
            if (p2 == 1)
            {
                lock_flag = 1;
            }
            else
            {
                lock_flag = 0;

            }
            return (lock_flag == 1);
        }

        public static bool IsActivate_19
        {
            get
            {
                return Get_Authorization_Code_19();
            }
        }

        public static bool Is_High_Value_19()
        {
            LptNum = 0;

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            //Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            p1 = 13; //ASTRA Pro R 19.0
            //p2 == 1
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;

            return (p2 == 1);
        }

        #endregion ASTRA_Pro R19.0



        #region ASTRA_Pro R19.0


        public static bool Write_ASTRA_Code()
        {

            return Write_ASTRA_Code_19();
        }

        public static bool Check_ASTRA_Lock()
        {
            return Check_ASTRA_Lock_19();
        }
        public static bool Check_ASTRA_Structure_Lock()
        {
            return Check_ASTRA_Structure_Lock_19();
        }
        public static bool Check_ASTRA_Bridge_Lock()
        {
            return Check_ASTRA_Bridge_Lock_19();
        }
        public static bool IsProfessional_StructuralVersion()
        {
            return IsProfessional_StructuralVersion_19();
        }
        public static bool IsProfessional_BridgeVersion()
        {
            return IsProfessional_BridgeVersion_19();
        }

        public static bool Get_Authorization_Code()
        {
            return Get_Authorization_Code_19();
        }

        public static bool IsActivate
        {
            get
            {
                return IsActivate_19;
            }
        }

        public static bool Is_High_Value()
        {
            return Is_High_Value_19();
        }

        #endregion ASTRA_Pro R19.0


        #region General Functions

        public static bool Is_AASHTO()
        {
            p2 = 0;
            //if (p2 != 1)
            //{
            if (TimeZone.CurrentTimeZone.StandardName.ToUpper().StartsWith("INDIA")) return false;
            else p2 = 1;
            //}

            return (p2 == 1);
        }

        public static int Get_Activation()
        {

            LptNum = 0;

            /**/
            // Memo Lock

            Pass1 = 4651;
            Pass2 = 9261;


            LptNum = 0;


            //                Below is to READ Memo Lock For Release 12/14

            Service = 3;
            SeedCode = 0;

            //p1=17 //for heads
            p1 = 9;
            //p2 == 23
            int address = p1;
            int data = 0;
            int status = 0;

            object param1 = (object)address;
            object param2 = (object)data;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.ReadWord,
                SeedCode,
                LptNum,
                Pass1,
                Pass2,
                param1,
                param2,
                param3,
                null);
            status = (int)param3;

            p2 = (int)param2;
            return p2;
        }
        public static void Set_Activation(int actv)
        {
            WriteToLock(9, actv);
        }
        public static bool WriteToLock(int p1Val, int p2Val)
        {
            int service, seed, lptnum, passw1, passw2, p1, p2, p3, p4;
            service = 0;
            seed = 0;
            lptnum = 0;
            //passw1 = Pass1;
            //passw2 = Pass2;

            passw1 = 4651;
            passw2 = 9261;

            p1 = p1Val;
            p2 = p2Val;
            p3 = 0;
            p4 = 0;

            bool bSuccess = true;

            HaspKey.Hasp(HaspService.WriteWord, seed, lptnum, passw1, passw2, p1, p2, p3, p4);
            if (p3 != 0)
            {
                bSuccess = false;
                //MessageBox.Show(this, "Error.", "TechSOFT", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return bSuccess;
        }
        public static bool CheckHasp()
        {
            //int Service = 0;
            int LptNum = 0, SeedCode = 0;
            //int Pass1, Pass2;
            //int p1, p2, p3, p4;
            //int lock_flag;
            //long IDNum;

            int ps1, ps2, p1, p2, p3, p4;

            ps1 = 4561;
            ps2 = 9261;
            p1 = p2 = p3 = p4 = 0;

            bool findHasp = false;

            int result = 0;
            int status = 0;

            object param1 = (object)result;
            object param3 = (object)status;

            HaspKey.Hasp(HaspService.IsHasp,
                SeedCode,
                LptNum,
                0,
                0,
                param1,
                null,
                param3,
                null);


            result = (int)param1;
            status = (int)param3;

            //if (0 == status)
            //    findHasp = true;
            if (result == 1)
                findHasp = true;
            return findHasp;
        }
        public static int activations { get; set; }

        #endregion General Functions

        #endregion Lock Activation

        public static bool IsDemoVersion()
        {
            return !IsProfessional_StructuralVersion_19();
        }

        public static bool IsProfessionalVersion()
        {
            return CheckHasp();
            //return Check_ASTRA_Lock_19();
        }
    }

}
