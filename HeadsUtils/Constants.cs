using System;
using System.Collections.Generic;
using System.Text;

namespace HeadsUtils
{
    public enum DrgEleType
    {
        Unknown,
        LINE,
        ARC,
        ELLIPSE,
        TEXT,
        CONFIG,
    }

    public enum eHEADS_RELEASE_TYPE
    {
        DEMO = 0,
        ASTRA,
        PROFESSIONAL,
    }
    public enum eHEADS_LWEIGHT
    {
        LnWtByLwDefault = -3,
        LnWtByBlock = -2,
        LnWtByLayer = -1,
        LnWt000 = 0,
        LnWt005 = 5,
        LnWt009 = 9,
        LnWt013 = 13,
        LnWt015 = 15,
        LnWt018 = 18,
        LnWt020 = 20,
        LnWt025 = 25,
        LnWt030 = 30,
        LnWt035 = 35,
        LnWt040 = 40,
        LnWt050 = 50,
        LnWt053 = 53,
        LnWt060 = 60,
        LnWt070 = 70,
        LnWt080 = 80,
        LnWt090 = 90,
        LnWt100 = 100,
        LnWt106 = 106,
        LnWt120 = 120,
        LnWt140 = 140,
        LnWt158 = 158,
        LnWt200 = 200,
        LnWt211 = 211,
    }
    public enum eHEADS_COLOR
    {
        ByBlock = 0,
        Red = 1,
        Yellow = 2,
        Green = 3,
        Cyan = 4,
        Blue = 5,
        Magenta = 6,
        White = 7,
        DarkGray = 8,
        ByLayer = 256,
    }
    public class Constants
    {

#if DEMO
        //private const eHEADS_RELEASE_TYPE buildtype = eHEADS_RELEASE_TYPE.DEMO;
        private static eHEADS_RELEASE_TYPE buildtype = eHEADS_RELEASE_TYPE.DEMO;
#elif PRO
        private const eHEADS_RELEASE_TYPE buildtype = eHEADS_RELEASE_TYPE.PROFESSIONAL;
#elif ASTRA        
        //private const eHEADS_RELEASE_TYPE buildtype = eHEADS_RELEASE_TYPE.ASTRA;
        private static eHEADS_RELEASE_TYPE buildtype = eHEADS_RELEASE_TYPE.ASTRA;
#endif
        private Constants()
        {

        }
        public static eHEADS_RELEASE_TYPE BuildType
        {
            get
            {
                return buildtype;
            }
            set 
            {
                buildtype = value;
            }
        }
        public static string ProductTitle
        {
            get
            {
                string strTitle = string.Empty;
                if (Constants.BuildType == eHEADS_RELEASE_TYPE.PROFESSIONAL)
                {
                    strTitle = "Heads Viewer [Professional]";
                }
                else if( Constants.BuildType == eHEADS_RELEASE_TYPE.DEMO)
                {
                    strTitle = "ASTRA Viewer [Demo]";
                }
                else if (Constants.BuildType == eHEADS_RELEASE_TYPE.ASTRA)
                {
                    strTitle = "ASTRA Viewer";
                }
                else
                {
                    System.Diagnostics.Trace.Assert(false);
                }
                return strTitle;
            }
        }
        public static string ProductName
        {
            get
            {
                string strTitle = string.Empty;
                if (Constants.BuildType == eHEADS_RELEASE_TYPE.PROFESSIONAL)
                {
                    strTitle = "ASTRA Viewer";
                }
                else if (Constants.BuildType == eHEADS_RELEASE_TYPE.DEMO)
                {
                    strTitle = "ASTRA Viewer";
                }
                else if (Constants.BuildType == eHEADS_RELEASE_TYPE.ASTRA)
                {
                    strTitle = "ASTRA Viewer";
                }
                else
                {
                    System.Diagnostics.Trace.Assert(false);
                }
                return strTitle;
            }
        }
        public const string HIP_FILE_NAME = "HAL";
        public const string LABEL_DETAILS = "DETAILS";
        public const string LABEL_GRID = "GRID";
        public const string LABEL_CHAINAGE = "CHAIN";
    }
}
