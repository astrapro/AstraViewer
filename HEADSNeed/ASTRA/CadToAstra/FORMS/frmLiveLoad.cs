using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using HEADSNeed.ASTRA.ASTRAClasses;

namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    public partial class frmLiveLoad : Form
    {
        IASTRACAD iACad = null;
        public List<string> ASTRA_Data { get; set; }
        public LiveLoad LLD { get; set; }
        public List<MovingLoadData> MovingLoads { get; set; }

        public frmLiveLoad(IASTRACAD iACAD, List<MovingLoadData> MLD)
        {
            InitializeComponent();
            this.iACad = iACAD;
            ASTRA_Data = new List<string>();
            this.MovingLoads = MLD;
        }

        private void frmLiveLoad_Load(object sender, EventArgs e)
        {
            
                cmb_Ana_load_type.Items.Clear();
                foreach (var item in MovingLoads)
                {
                    cmb_Ana_load_type.Items.Add("TYPE " + item.Type + ":" + item.Name);
                }

                if (cmb_Ana_load_type.Items.Count > 0)
                {
                    cmb_Ana_load_type.DropDownStyle = ComboBoxStyle.DropDownList;
                    cmb_Ana_load_type.SelectedIndex = 0;
                }


                if (LLD != null)
                {
                    for (int i = 0; i < cmb_Ana_load_type.Items.Count; i++)
                    {
                        if (cmb_Ana_load_type.Items[i].ToString().StartsWith("TYPE " + LLD.Type + ":"))
                        {
                            cmb_Ana_load_type.SelectedIndex = i;
                            break;
                        }
                    }
                    txt_Ana_X.Text = LLD.X_Distance.ToString("f3");
                    txt_Ana_DL_Y.Text = LLD.Y_Distance.ToString("f3");
                    txt_Ana_DL_Z.Text = LLD.Z_Distance.ToString("f3");
                    txt_XINCR.Text = LLD.X_Increment.ToString("f3");
                    txt_Load_Impact.Text = LLD.Impact_Factor.ToString("f3");

                    btn_add.Text = "Change";
                }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            LLD = new LiveLoad();

            MyStrings mlist = new MyStrings(cmb_Ana_load_type.Text,':');
            LLD.Type = MyStrings.StringToInt(mlist.StringList[0].Replace("TYPE ", ""), 0);
            LLD.X_Distance = MyStrings.StringToDouble(txt_Ana_X.Text, 0.0);
            LLD.Y_Distance = MyStrings.StringToDouble(txt_Ana_DL_Y.Text, 0.0);
            LLD.Z_Distance = MyStrings.StringToDouble(txt_Ana_DL_Z.Text, 0.0);
            LLD.X_Increment = MyStrings.StringToDouble(txt_XINCR.Text, 0.0);
            LLD.Impact_Factor = MyStrings.StringToDouble(txt_Load_Impact.Text, 1.0);

            this.Close();
        }
    }

}
