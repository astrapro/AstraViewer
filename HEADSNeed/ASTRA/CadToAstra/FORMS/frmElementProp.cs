using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HEADSNeed.ASTRA.ASTRAClasses;

namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    public partial class frmElementProp : Form
    {

        List<int> list_mem_Nos = null;
        IASTRACAD iACad = null;
        public List<string> ASTRA_Data { get; set; }
        public TreeView TRV { get; set; }

        public frmElementProp(IASTRACAD iACAD)
        {
            InitializeComponent();
            list_mem_Nos = new List<int>();
            this.iACad = iACAD;
            ASTRA_Data = new List<string>();
        }

        private void frmElementProp_Load(object sender, EventArgs e)
        {

            txt_DEN_val.Text = "";
            txt_EXX_val.Text = "";
            txt_EXY_val.Text = "";
            txt_EXG_val.Text = "";
            txt_EYY_val.Text = "";
            txt_EYG_val.Text = "";
            txt_GXY_val.Text = "";

            if (ASTRA_Data.Count > 0)
            {
                string kStr = ASTRA_Data[0].ToUpper();

                MyStrings mlist = new MyStrings(MyStrings.RemoveAllSpaces(kStr), ' ');

                int r = -1;
                for (int i = 0; i < mlist.Count; i++)
                {
                    if (r == -1)
                    {
                        if (mlist.StringList[i].StartsWith("TH"))
                        {
                            r = i;
                            txt_mnos.Text = mlist.GetString(0, r - 1);
                            txt_thk_val.Text = mlist.StringList[i + 1];
                        }
                    }
                    else
                    {
                        if (mlist.StringList[i].StartsWith("DEN"))
                        {
                            //1 TO 234 TH 0.07 DEN 0.0 EXX 3.00934E6  EXY 531061  EXG 0.0  EYY 3.00934E6  EYG 0.0  GXY 1.23913E6
                            //r = i;
                            //txt_mnos.Text = mlist.GetString(0, r - 1);
                            txt_DEN_val.Text = mlist.StringList[i + 1];
                        }
                        if (mlist.StringList[i].StartsWith("EXX"))
                        {
                            txt_EXX_val.Text = mlist.StringList[i + 1];
                        }
                        if (mlist.StringList[i].StartsWith("EXY"))
                        {
                            txt_EXY_val.Text = mlist.StringList[i + 1];
                        }
                        if (mlist.StringList[i].StartsWith("EXG"))
                        {
                            txt_EXG_val.Text = mlist.StringList[i + 1];
                        }
                        if (mlist.StringList[i].StartsWith("EYY"))
                        {
                            txt_EYY_val.Text = mlist.StringList[i + 1];
                        }
                        if (mlist.StringList[i].StartsWith("EYG"))
                        {
                            txt_EYG_val.Text = mlist.StringList[i + 1];
                        }
                        if (mlist.StringList[i].StartsWith("GXY"))
                        {
                            txt_GXY_val.Text = mlist.StringList[i + 1];
                        }
                    }
                }


                btn_aload_add.Text = "Change";
                //ASTRA_Data.Clear();
            }
            else
            {
                //txt_mnos.Text = iACad.GetSelectedE();

            }
        }
        private void btn_aload_Click(object sender, EventArgs e)
        {

            string kStr = "";
            kStr = txt_mnos.Text.Replace(',', ' ');


            kStr = kStr + " TH ";

            double dval = 0.0;

            dval = MyStrings.StringToDouble(txt_thk_val.Text, 0.0);
            if (dval != 0.0)
            {
                kStr = kStr + " " + dval.ToString("f3");
            }
            //else
            //    return;
            //1 TO 234 TH 0.07 DEN 0.0 EXX 3.00934E6  EXY 531061  EXG 0.0  EYY 3.00934E6  EYG 0.0  GXY 1.23913E6

            //dval = MyList.StringToDouble(txt_DEN_val.Text, 0.0);
            if (txt_DEN_val.Text != "")
            {
                kStr = kStr + " DEN " + txt_DEN_val.Text;
            }
            if (txt_EXX_val.Text != "")
            {
                kStr = kStr + " EXX " + txt_EXX_val.Text;
            }
            if (txt_EXY_val.Text != "")
            {
                kStr = kStr + " EXY " + txt_EXY_val.Text;
            }
            if (txt_EXG_val.Text != "")
            {
                kStr = kStr + " EXG " + txt_EXG_val.Text;
            }
            if (txt_EYY_val.Text != "")
            {
                kStr = kStr + " EYY " + txt_EYY_val.Text;
            }
            dval = MyStrings.StringToDouble(txt_EYG_val.Text, 0.0);
            if (txt_EYG_val.Text != "")
            {
                kStr = kStr + " EYG " + txt_EYG_val.Text;
            }
            if (txt_GXY_val.Text != "")
            {
                kStr = kStr + " GXY " + txt_GXY_val.Text;
            }




            if (TRV != null)
            {
                if (ASTRA_Data.Count > 0)
                {
                    TRV.SelectedNode.Text = kStr;
                    this.Close();
                }
                else
                {
                    ASTRA_Data.Add(kStr);
                    TRV.Nodes.Add(kStr);
                }
            }
            else
            {
                this.Close();
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
