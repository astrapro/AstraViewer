using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using HEADSNeed.ASTRA.CadToAstra;

namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    public partial class frmMovingLoad : Form
    {
        IASTRACAD iACad = null;
        public frmMovingLoad(IASTRACAD iACAD)
        {
            InitializeComponent();
            this.iACad = iACAD;
        }

        private void frmMovingLoad_Load(object sender, EventArgs e)
        {
            cmbMassUnit.SelectedIndex = 2;
            cmbLengthUnit.SelectedIndex = 0;

            //TYPE 1 -18.8 0 2.75 XINC 0.2
            //TYPE 1 -18.8 0 6.25 XINC 0.2
            //TYPE 1 -18.8 0 9.75 XINC 0.2
            dgv.Rows.Add(1, -18.8, 0, 2.75, 0.2);
            dgv.Rows.Add(1, -18.8, 0, 6.25, 0.2);
            dgv.Rows.Add(1, -18.8, 0, 9.75, 0.2);
        }

        private void btn_add_data_Click(object sender, EventArgs e)
        {
            string kStr = "UNIT " + cmbMassUnit.Text + " " + cmbLengthUnit.Text;
            //iACad.WriteFile(kStr);

            iACad.MovingLoadFile = MovingFile;
            iACad.IsMovingLoad = true;
            iACad.Create_Data.Moving_Load_Data.Clear();

            kStr = "DEFINE MOVING LOAD FILE " + Path.GetFileName(MovingFile);
            //iACad.WriteFile(kStr);
            iACad.Create_Data.Moving_Load_Data.Add(kStr);
            kStr = string.Format("TYPE 1 {0} {1}", txt_load_1.Text, txt_type_1.Text);
            iACad.Create_Data.Moving_Load_Data.Add(kStr);
            //iACad.WriteFile(kStr);
            kStr = string.Format("TYPE 2 {0} {1}", txt_load_2.Text, txt_type_2.Text);
            iACad.Create_Data.Moving_Load_Data.Add(kStr);
            //iACad.WriteFile(kStr);
            kStr = string.Format("TYPE 3 {0} {1}", txt_load_3.Text, txt_type_3.Text);
            iACad.Create_Data.Moving_Load_Data.Add(kStr);
            //iACad.WriteFile(kStr);
            kStr = string.Format("TYPE 4 {0} {1}", txt_load_4.Text, txt_type_4.Text);
            iACad.Create_Data.Moving_Load_Data.Add(kStr);
            //iACad.WriteFile(kStr);
            kStr = string.Format("TYPE 5 {0} {1}", txt_load_5.Text, txt_type_5.Text);
            iACad.Create_Data.Moving_Load_Data.Add(kStr);
            //iACad.WriteFile(kStr);
            //iACad.WriteFile("**** 3 LANE CLASS A *****");

            kStr = "LOAD GENERATION " + txt_load_gen.Text;
            //iACad.WriteFile(kStr);
            iACad.Create_Data.Moving_Load_Data.Add(kStr);

            try
            {
                if (dgv.RowCount > 1)
                {
                    for (int i = 0; i < dgv.RowCount - 1; i++)
                    {
                        kStr = string.Format("TYPE {0} {1} {2} {3} XINC {4}",
                            dgv[0, i].Value.ToString(),
                            dgv[1, i].Value.ToString(),
                            dgv[2, i].Value.ToString(),
                            dgv[3, i].Value.ToString(),
                            dgv[4, i].Value.ToString());
                        iACad.Create_Data.Moving_Load_Data.Add(kStr);
                        //iACad.WriteFile(kStr);
                    }
                }
                WriteMovingLoad();
            }
            catch (Exception ex) { }
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public string MovingFile
        {
            get
            {
                string file_name = "";
                file_name = txt_load_file.Text + ".TXT";
                file_name = Path.Combine(Path.GetDirectoryName(iACad.File_Name), file_name);
                return file_name;
            }
        }
        void WriteMovingLoad()
        {
            StreamWriter sw = new StreamWriter(new FileStream(MovingFile, FileMode.Create));
            try
            {
                sw.WriteLine("FILE {0}", Path.GetFileName(MovingFile));
                //sw.WriteLine("TYPE1 IRCCLASSA");
                //sw.WriteLine("3.4 3.4 3.4 3.4 5.7 5.7 1.35 1.35");
                //sw.WriteLine("3.00 3.00 3.00 4.30 1.20 3.20 1.10");
                //sw.WriteLine("1.80");

                //sw.WriteLine("TYPE 2 IRCCLASSB");
                //sw.WriteLine("2.05 2.05 2.05 2.05 3.4 3.4 0.8 0.8");
                //sw.WriteLine("3.00 3.00 3.00 4.30 1.20 3.20 1.10");
                //sw.WriteLine("1.80");

                //sw.WriteLine("TYPE 3 IRC70RTRACK");
                //sw.WriteLine("3.5 3.5 3.5 3.5 3.5 3.5 3.5 3.5 3.5 3.5");
                //sw.WriteLine("0.457 0.457 0.457 0.457 0.457 0.457 0.457 0.457 0.457");
                //sw.WriteLine("2.06");

                //sw.WriteLine("TYPE 4 IRC70RWHEEL");
                //sw.WriteLine("4.25 4.25 4.25 4.25 3.0 3.0 2.0");
                //sw.WriteLine("1.37 3.05 1.37 2.13 1.52 3.96");
                //sw.WriteLine("0.450 1.480 0.450");

                //sw.WriteLine("TYPE 5 IRCCLASSAATRACK");
                //sw.WriteLine("3.5 3.5 3.5 3.5 3.5 3.5 3.5 3.5 3.5 3.5");
                //sw.WriteLine("0.360 0.360 0.360 0.360 0.360 0.360 0.360 0.360 0.360");
                //sw.WriteLine("2.05");


                //sw.WriteLine(string.Format("FILE LL.TXT"));
                sw.WriteLine(string.Format(""));
                sw.WriteLine(string.Format("TYPE1 IRCCLASSA"));
                sw.WriteLine(string.Format("2.7 2.7 11.4 11.4 6.8 6.8 6.8 6.8"));
                sw.WriteLine(string.Format("1.10 3.20 1.20 4.30 3.00 3.00 3.00"));
                sw.WriteLine(string.Format("1.80"));
                sw.WriteLine(string.Format(""));
                sw.WriteLine(string.Format("TYPE 2 IRCCLASSB"));
                sw.WriteLine(string.Format("1.6 1.6 6.8 6.8 4.1 4.1 4.1 4.1"));
                sw.WriteLine(string.Format("1.10 3.20 1.20 4.30 3.00 3.00 3.00"));
                sw.WriteLine(string.Format("1.80"));
                sw.WriteLine(string.Format(""));
                sw.WriteLine(string.Format("TYPE 3 IRC70RTRACK"));
                sw.WriteLine(string.Format("7.0 7.0 7.0 7.0 7.0 7.0 7.0 7.0 7.0 7.0"));
                sw.WriteLine(string.Format("0.457 0.457 0.457 0.457 0.457 0.457 0.457 0.457 0.457"));
                sw.WriteLine(string.Format("0.84"));
                sw.WriteLine(string.Format(""));
                sw.WriteLine(string.Format("TYPE 4 IRC70RWHEEL"));
                sw.WriteLine(string.Format("8.0 12.0 12.0 17.0 17.0 17.0 17.0"));
                sw.WriteLine(string.Format("1.37 3.05 1.37 2.13 1.52 3.96"));
                sw.WriteLine(string.Format("0.450 1.480 0.450"));
                sw.WriteLine(string.Format(""));
                sw.WriteLine(string.Format("TYPE 5 IRCCLASSAATRACK"));
                sw.WriteLine(string.Format("7.0 7.0 7.0 7.0 7.0 7.0 7.0 7.0 7.0 7.0"));
                sw.WriteLine(string.Format("0.360 0.360 0.360 0.360 0.360 0.360 0.360 0.360 0.360"));
                sw.WriteLine(string.Format("0.85"));
                sw.WriteLine(string.Format(""));
                sw.WriteLine(string.Format("TYPE 6 IRC24RTRACK"));
                sw.WriteLine(string.Format("2.5 2.5 2.5 2.5 2.5 2.5 2.5 2.5 2.5 2.5"));
                sw.WriteLine(string.Format("0.366 0.366 0.366 0.366 0.366 0.366 0.366 0.366 0.366"));
                sw.WriteLine(string.Format("0.36"));
                sw.WriteLine(string.Format(""));
                sw.WriteLine(string.Format("TYPE 7 BG_RAIL_1"));
                sw.WriteLine(string.Format("24.52 24.52 24.52 24.52 24.52 24.52 24.52 24.52 24.52 24.52 24.52 24.52"));
                sw.WriteLine(string.Format("2.05 1.95 5.56 1.95 2.05 5.94 2.05 1.95 5.56 1.95 2.05 2.97"));
                sw.WriteLine(string.Format("1.676"));
                sw.WriteLine(string.Format(""));
                sw.WriteLine(string.Format("TYPE 8 BG_RAIL_2"));
                sw.WriteLine(string.Format("22.06 22.06 22.06 22.06 22.06 22.06 22.06 22.06 22.06 22.06 22.06 22.06"));
                sw.WriteLine(string.Format("1.65 1.65 6.4 1.65 1.65 3.0 1.65 1.65 6.4 1.65 1.65 1.5"));
                sw.WriteLine(string.Format("1.676"));
                sw.WriteLine(string.Format(""));
                sw.WriteLine(string.Format("TYPE 9 MG_RAIL_1"));
                sw.WriteLine(string.Format("11.87 11.87 11.87 11.87 11.28 12.94 12.94 12.94 12.94 7.94"));
                sw.WriteLine(string.Format("1.372 2.286 1.372 2.680 2.133 1.346 1.346 1.397 2.197 1.778"));
                sw.WriteLine(string.Format("1.676"));
                sw.WriteLine(string.Format(""));
                sw.WriteLine(string.Format("TYPE 10 MG_RAIL_2"));
                sw.WriteLine(string.Format("9.61 9.61 9.61 9.61 9.12 10.49 10.49 10.49 10.49 6.47"));
                sw.WriteLine(string.Format("1.372 1.372 1.372 2.806 1.829 1.346 1.346 1.397 2.197 1.778"));
                sw.WriteLine(string.Format("1.676"));

            }
            catch (Exception ex)
            {
            }
            finally
            {
                sw.Flush();
                sw.Close();
            }
        }

    }
}
