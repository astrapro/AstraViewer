using System;
using System.Collections.Generic;
using System.Text;

namespace HEADSNeed.ASTRA.ASTRAClasses
{

    public class Axis_Direction
    {
        public bool X_Positive { get; set; }
        public bool X_Negative { get; set; }
        public bool Y_Positive { get; set; }
        public bool Y_Negative { get; set; }
        public bool Z_Positive { get; set; }
        public bool Z_Negative { get; set; }
        public int JointNo { get; set; }

        public MemberIncidence X_Positive_Member { get; set; }
        public MemberIncidence X_Negative_Member { get; set; }
        public MemberIncidence Y_Positive_Member { get; set; }
        public MemberIncidence Y_Negative_Member { get; set; }
        public MemberIncidence Z_Positive_Member { get; set; }
        public MemberIncidence Z_Negative_Member { get; set; }



        public Axis_Direction()
        {
            JointNo = 0;

            X_Positive = false;
            X_Negative = false;
            Y_Positive = false;
            Y_Negative = false;
            Z_Positive = false;
            Z_Negative = false;
        }
        public override string ToString()
        {
            string kStr = JointNo + "  ";

            if (X_Positive) kStr = kStr + " [X+] ";
            if (X_Negative) kStr = kStr + " [X-] ";
            if (Y_Positive) kStr = kStr + " [Y+] ";
            if (Y_Negative) kStr = kStr + " [Y-] ";
            if (Z_Positive) kStr = kStr + " [Z+] ";
            if (Z_Negative) kStr = kStr + " [Z-] ";
            return kStr;
        }
        public MemberIncidence Lx_Member
        {
            get
            {
                double L1 = 0;
                if (X_Positive_Member != null)
                {
                    L1 = X_Positive_Member.Length;
                }
                if (X_Negative_Member != null)
                {
                    if (X_Negative_Member.Length > L1)
                    {
                        return X_Negative_Member;
                    }
                }
                return X_Positive_Member;
            }
        }

        public MemberIncidence Ly_Member
        {
            get
            {
                double L1 = 0;
                if (Y_Positive_Member != null)
                {
                    L1 = Y_Positive_Member.Length;
                }
                if (Y_Negative_Member != null)
                {
                    if (Y_Negative_Member.Length > L1)
                    {
                        return Y_Negative_Member;
                    }
                }
                return Y_Positive_Member;
            }
        }

        public MemberIncidence Lz_Member
        {
            get
            {
                double L1 = 0;
                if (Z_Positive_Member != null)
                {
                    L1 = Z_Positive_Member.Length;
                }
                if (Z_Negative_Member != null)
                {
                    if (Z_Negative_Member.Length > L1)
                    {
                        return Z_Negative_Member;
                    }
                }
                return Z_Positive_Member;
            }
        }


    }

    public class DirecctionCollection : List<Axis_Direction>
    {
        public DirecctionCollection()
            : base()
        {
        }
    }
}
