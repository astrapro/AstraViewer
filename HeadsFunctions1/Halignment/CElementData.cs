using System;
using System.Collections.Generic;
using System.Text;

namespace HeadsFunctions1.Halignment
{
    internal class CElementData
    {
        public CElementData()
        {
            this.npLineID = 0;
            this.nElementNo = 0;
            this.nType = 0;				// for combobox 
            this.dChainInterval = 0;
            this.dLength = 0;
            this.dRadius = 0;
            this.dStartChain = 0;
            this.dBearings = 0;
            this.dXvalue = 0;
            this.dYvalue = 0;

        }
        public int npLineID;
        public int nElementNo;
        public int nType;				// for combobox 
        public double dChainInterval;
        public double dLength;
        public double dRadius;
        public double dStartChain;
        public double dBearings;
        public double dXvalue;
        public double dYvalue;
    }
}
