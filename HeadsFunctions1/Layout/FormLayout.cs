using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HeadsUtils.Interfaces;
using HeadsUtils;

namespace HeadsFunctions1.Layout
{
    internal partial class FormLayout : Form
    {
        internal enum PaperType
        {
            A0 = 0,
            A1,
            A2,
            A3,
            A4,
            Custom,
        }
        
        public FormLayout()
        {
            InitializeComponent();

            this.comboPaperSize_.Items.Add(PaperType.A0);
            this.comboPaperSize_.Items.Add(PaperType.A1);
            this.comboPaperSize_.Items.Add(PaperType.A2);
            this.comboPaperSize_.Items.Add(PaperType.A3);
            this.comboPaperSize_.Items.Add(PaperType.A4);
            this.comboPaperSize_.Items.Add(PaperType.Custom);

            this.comboPaperSize_.SelectedItem = PaperType.A2;
        }

        public CLayOut SelectedLayout
        {
            get
            {
                CLayOut layout = new CLayOut();
                layout.sheetWidth = this.tbPageWidth_.Value;
                layout.sheetLength = this.tbPageHeight_.Value;

                layout.topM = this.tbMarginTop_.Value;
                layout.bottomM= this.tbMarginBottom_.Value;
                layout.leftM = this.tbMarginLeft_.Value;
                layout.rightM = this.tbMarginRight_.Value;

                layout.layOutScale = this.tbScale_.Value;

                return layout;
            }
            set
            {
                this.comboPaperSize_.SelectedItem  = GetPaperType(value.sheetLength, value.sheetWidth);
                this.tbPageWidth_.Value = value.sheetWidth;
                this.tbPageHeight_.Value = value.sheetLength;

                this.tbMarginTop_.Value = value.topM ;
                this.tbMarginBottom_.Value = value.bottomM;
                this.tbMarginLeft_.Value = value.leftM ;
                this.tbMarginRight_.Value = value.rightM;

                this.tbScale_.Value = value.layOutScale;

                
            }
        }

        PaperType GetPaperType(double dHeight, double dWidth)
        {
            PaperType type  = PaperType.Custom;
            if (dHeight == 841 && dWidth == 1189)
            {
                type = PaperType.A0;
            }
            else if (dHeight == 594 && dWidth == 841)
            {
                type = PaperType.A1;
            }
            else if (dHeight == 420 && dWidth == 594)
            {
                type = PaperType.A2;
            }
            else if (dHeight == 297 && dWidth == 420)
            {
                type = PaperType.A3;
            }
            else if (dHeight == 210 && dWidth == 297)
            {
                type = PaperType.A4;
            }

            return type;
        }

        void UpadtePaperSize(PaperType type)
        {
            double dHeight = 0;
            double dWidth = 0;

            this.tbPageHeight_.Enabled = false;
            this.tbPageWidth_.Enabled = false;

            switch (type)
            {

                case PaperType.A0:	//Paper Type
                    dHeight = 841;
                    dWidth = 1189;
                    break;
                case PaperType.A1:
                    dHeight = 594;
                    dWidth = 841;
                    break;
                case PaperType.A2:
                    dHeight = 420;
                    dWidth = 594;
                    break;
                case PaperType.A3:
                    dHeight = 297;
                    dWidth = 420;
                    break;
                case PaperType.A4:
                    dHeight = 210;
                    dWidth = 297;
                    break;

                case PaperType.Custom:
                    this.tbPageHeight_.Enabled = true;
                    this.tbPageWidth_.Enabled = true;
                    break;
            }
            this.tbPageHeight_.Value = dHeight;
            this.tbPageWidth_.Value = dWidth;
        }

        private void comboPaperSize__SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpadtePaperSize((PaperType)this.comboPaperSize_.SelectedItem);
        }

        private void btnOk__Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel__Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}