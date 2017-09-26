using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;


using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Geometry;

using HEADSNeed.ASTRA.ASTRADrawingTools;
using HEADSNeed.ASTRA.ASTRAClasses;
using HEADSNeed.ASTRA.ASTRAClasses.StructureDesign;
using HEADSNeed.ASTRA.CadToAstra.FORMS;
using HEADSNeed.ASTRA.CadToAstra;

using MovingLoadAnalysis;
using MovingLoadAnalysis.DataStructure;

using VDRAW = VectorDraw.Professional.ActionUtilities;




namespace HEADSNeed.ASTRA.ASTRAClasses.StructureDesign
{
    public partial class frm_InteractionDiagram : Form
    {
        vdDocument vdoc1
        {
            get
            {
                return vdScrollableControl1.BaseControl.ActiveDocument;
            }
        }
        public static int last_index = -1;
        public string DrawingPath { get { return Path.Combine(Application.StartupPath, "Interaction Diagrams"); } }
        string Chart2 = "", Chart3 = "";

        public string Tab_Heading
        {
            get
            {
                return tab_chart1.Text;
            }
            set
            {
                tab_chart1.Text = value;
            }
        }

        public string Title_1
        {
            get
            {
                return lbl_tab12.Text;
            }
            set
            {
                lbl_tab12.Text = value;
            }
        }

        public string Value_1
        {
            get
            {
                return txt_tab1.Text;
            }
            set
            {
                txt_tab1.Text = value;
            }
        }
        public string Value_TEXT
        {
            get
            {
                return lbl_tab13.Text;
            }
            set
            {
                lbl_tab13.Text = value;
            }
        }

        public frm_InteractionDiagram()
        {
            InitializeComponent();
        }
        public double Rebar_Nos { get; set; }
        public int side { get; set; }

        private void frm_InteractionDiagram_Load(object sender, EventArgs e)
        {
            //HEADSNeed.ASTRA.ASTRAForms.frm_Intact_Diag1 ffm = new HEADSNeed.ASTRA.ASTRAForms.frm_Intact_Diag1();

            //ffm.Rebar_Nos = (int) Rebar_Nos;
            //ffm.Fy = fy1;
            
            //side = 2;
            //if (ffm.ShowDialog() == DialogResult.Cancel)
            //{
            //    this.Close();
            //    return;
            //}
            //side = ffm.Side;




            MyList mlist = null;
            //int select_index = -1;
            int select_index = last_index;

            if (Directory.Exists(DrawingPath))
            {
                foreach (var item in Directory.GetFiles(DrawingPath))
                {
                    if (Path.GetFileName(item).StartsWith("37")) Chart2 = item;
                    else if (Path.GetFileName(item).StartsWith("38")) Chart3 = (item);
                    else
                    {
                        lst_drawings1.Items.Add(Path.GetFileNameWithoutExtension(item));
                    }

                    //mlist = new MyList(item.Replace("_", " ").Replace(".vdml", "").Replace("d'D", ""), '=');
                    mlist = new MyList(Path.GetFileNameWithoutExtension(item).Replace("_", " ").Replace(".vdml", "").Replace("d'D", "").Replace("=", " "), ' ');



                    //double da = d_dash / D;
                    if (da < 0.05) da = 0.05;
                    else if (da > 0.05 && da <= 0.10) da = 0.10;
                    else if (da > 0.10 && da <= 0.15) da = 0.15;
                    else if (da > 0.15 && da <= 0.20) da = 0.20;
                    if (fy1 == 240) fy1 = 250;
                    try
                    {
                        //if (fy1 == mlist.GetDouble(1) && (da.ToString("f2") == mlist.StringList[2]) && select_index == -1)

                        if (fy1 == mlist.GetDouble(5) && side == mlist.GetInt(2) && (da.ToString("f2") == mlist.StringList[7]) && select_index == -1)
                        {
                            select_index = lst_drawings1.Items.Count - 1;
                        }
                    }
                    catch (Exception ex) { }

                }
            }
            if (select_index != -1)
            {
                lst_drawings1.SelectedIndex = select_index;
                lst_drawings1.Visible = false;
            }
            else
            {
                if (lst_drawings1.Items.Count > 0)
                {
                    lst_drawings1.SelectedIndex = 0;
                    lst_drawings1.Visible = true;
                }
            }
            last_index = select_index;

            tabControl1.TabPages.Remove(tab_chart11);
            tabControl1.TabPages.Remove(tab_chart2);
            tabControl1.TabPages.Remove(tab_chart3);

            //btn_OK.Focus();
            txt_tab1.Focus();

            //lst_drawings1.Visible = false;

            lbl_tab11.Visible = true;
            lbl_tab12.Visible = true;
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Select_Chart1();
        }

        private void Select_Chart1()
        {

            string filename = "";
            try
            {


                if (lst_drawings1.SelectedItems.Count > 0)
                    filename = lst_drawings1.Items[lst_drawings1.SelectedIndex].ToString();

                filename = Path.Combine(DrawingPath, filename) + ".vdml";
                if (!File.Exists(filename)) filename = Path.Combine(DrawingPath, Path.GetFileNameWithoutExtension(filename)) + ".dwg";
                if (!File.Exists(filename)) filename = Path.Combine(DrawingPath, Path.GetFileNameWithoutExtension(filename)) + ".vdcl";
                if (File.Exists(filename))
                {
                    vdoc1.Open(filename);
                    vdoc1.GlobalRenderProperties.AxisSize = 1000;
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(vdoc1);

                }
            }
            catch (Exception ex) { }
        }
        private void btn_OK_Click(object sender, EventArgs e)
        {
            List<string> file_cont = new List<string>();
            //file_cont.Add(string.Format("InputValue1${0}", InputValue1));
            //file_cont.Add(string.Format("InputValue11${0}", InputValue11));
            //file_cont.Add(string.Format("InputValue2${0}", InputValue2));
            //file_cont.Add(string.Format("InputValue3${0}", InputValue3));
            //File.WriteAllLines(temp_file, file_cont.ToArray());
            this.DialogResult = DialogResult.OK;
            this.Close();
            //this.Dispose();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                switch (tabControl1.SelectedIndex)
                {
                    case 0:
                    case 1:
                        lst_drawings1.Visible = true;
                        Select_Chart1();
                        break;
                    case 2:
                        lst_drawings1.Visible = false;
                        if (File.Exists(Chart2))
                            vdoc1.Open(Chart2);
                        break;
                    case 3:
                        lst_drawings1.Visible = false;
                        if (File.Exists(Chart3))
                            vdoc1.Open(Chart3);
                        break;
                }
            }
            catch (Exception ex) { }
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(vdoc1);
        }

        public double fy1 = 415.0;
        public double fy2 = 415.0;
        public double fck1 = 0, b = 0, D = 0, d_dash = 0, fck2 = 0, Pu_top = 0;
        public double Muy = 0, Mux = 0;

        public double da = 0.0;
        public string SetText1_Tab1
        {
            get
            {
                return lbl_tab11.Text;
            }
            set
            {
                lbl_tab11.Text = value;
            }
        }
        public string SetText1_Tab11
        {
            get
            {
                return lbl_tab111.Text;
            }
            set
            {
                lbl_tab111.Text = value;
            }
        }
        public string SetText2_Tab1
        {
            get
            {
                return lbl_tab12.Text;
            }
            set
            {
                lbl_tab12.Text = value;
            }
        }
        public string SetText2_Tab11
        {
            get
            {
                return lbl_tab112.Text;
            }
            set
            {
                lbl_tab112.Text = value;
            }
        }
        public string SetText3_Tab1
        {
            get
            {
                return lbl_tab13.Text;
            }
            set
            {
                lbl_tab13.Text = value;
            }
        }
        public string SetText3_Tab11
        {
            get
            {
                return lbl_tab113.Text;
            }
            set
            {
                lbl_tab113.Text = value;
            }
        }


        public string SetText1_Tab2
        {
            get
            {
                return lbl_tab21.Text;
            }
            set
            {
                lbl_tab21.Text = value;
            }
        }
        public string SetText2_Tab2
        {
            get
            {
                return lbl_tab22.Text;
            }
            set
            {
                lbl_tab22.Text = value;
            }
        }


        public string SetText1_Tab3
        {
            get
            {
                return lbl_tab31.Text;
            }
            set
            {
                lbl_tab31.Text = value;
            }
        }
        public string SetText2_Tab3
        {
            get
            {
                return lbl_tab32.Text;
            }
            set
            {
                lbl_tab32.Text = value;
            }
        }

        public double InputValue1
        {
            get
            {
                try
                {
                    return Double.Parse(txt_tab1.Text);
                }
                catch (Exception ex) { }
                return 0.0;
            }
            set
            {
                txt_tab1.Text = value.ToString("0.0000");
            }
        }
        public double InputValue11
        {
            get
            {
                try
                {
                    return Double.Parse(txt_tab11.Text);
                }
                catch (Exception ex) { }
                return 0.0;
            }
            set
            {
                txt_tab11.Text = value.ToString("0.0000");
            }
        }
        public double InputValue2
        {
            get
            {
                try
                {
                    return Double.Parse(txt_tab2.Text);
                }
                catch (Exception ex) { }
                return 0.0;

            }
            set
            {
                txt_tab2.Text = value.ToString("0.0000");
            }
        }
        public double InputValue3
        {
            get
            {
                try
                {
                    return Double.Parse(txt_tab3.Text);
                }
                catch (Exception ex) { }
                return 0.0;
            }
            set
            {
                txt_tab3.Text = value.ToString("0.0000");
            }
        }

        private void txt_tab1_TextChanged(object sender, EventArgs e)
        {
            Calculate();
        }

        private void Calculate()
        {
            double inp_val1 = InputValue1;

            double Mux1 = inp_val1 * (fck1 * b * D * D);
            Mux1 = Mux1 / 1000000;

            double val4 = d_dash / b;
            double val5 = (Pu_top * 1000) / (fck2 * b * D);

            double Muy1 = inp_val1 * (fck1 * b * D * D);
            Muy1 = Muy1 / 10E5;
            inp_val1 = InputValue2;
            double Puz = inp_val1 * b * D;
            Puz = Puz / 1000.0;
            double val6 = Pu_top / Puz;
            double val7 = Mux / Mux1;
            double val8 = Muy / Muy1;
            double val9 = Pu_top / Puz;
            double val10 = Muy / Muy1;
            double val11 = Mux / Mux1;
            SetText1_Tab3 = string.Format("Refer to Interaction diagram chart for Pu / Puz = {0:f4} and Muy / Muy1 = {1:f4}", val9, val10);
        }
    }
}
