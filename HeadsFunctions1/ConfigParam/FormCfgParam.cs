using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HeadsUtils.Interfaces;
using HeadsUtils;

namespace HeadsFunctions1.ConfigParam
{
    internal partial class FormCfgParam : Form
    {
        public FormCfgParam(IHeadsApplication app)
        {
            InitializeComponent();

            this.PopulateData(app.ActiveDocument.ConfigParam);
        }

        private void PopulateData(CCfgtype cfgparam)
        {
            this.tbXFactor_.Text = cfgparam.XMetric.ToString();
            this.tbYFactor_.Text = cfgparam.YMetric.ToString();
            this.tbRotatonAngle_.Text = cfgparam.xfa.ToString();

            this.tbXOrigin_.Text = cfgparam.XLate_X.ToString();
            this.tbYOrigin_.Text = cfgparam.XLate_Y.ToString();
            this.tbHorizontalScale_.Text = cfgparam.hs.ToString();

            this.tbMarginTop_.Text = cfgparam.tm.ToString();
            this.tbMarginBottom_.Text = cfgparam.bm.ToString();
            this.tbMarginLeft_.Text = cfgparam.lm.ToString();
            this.tbMarginRight_.Text = cfgparam.rm.ToString();

            this.tbSheetLength_.Text = cfgparam.sl.ToString();
            this.tbSheetWidth_.Text = cfgparam.sw.ToString();            
        }
    }
}