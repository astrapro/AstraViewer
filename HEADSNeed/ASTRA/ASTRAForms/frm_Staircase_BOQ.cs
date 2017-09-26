using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using HEADSNeed.ASTRA.ASTRAClasses;
using HEADSNeed.ASTRA.ASTRAClasses.StructureDesign;

using MovingLoadAnalysis;
using MovingLoadAnalysis.DataStructure;

namespace HEADSNeed.ASTRA.ASTRAForms
{
    public delegate void sAdd_Staircase_BOQ(Staircase_BOQ sboq);

    public partial class frm_Staircase_BOQ : Form
    {
        public frm_Staircase_BOQ()
        {
            InitializeComponent();
        }


        

        Thread thd;

        public sAdd_Staircase_BOQ Add_Staircase_BOQ;


        ASTRADoc AST_DOC { get; set; }
        public StructureMemberAnalysis StructureAnalysis { get; set; }
        public double Main_Bar_Dia { get; set; }
        public double Tie_Bar_Dia { get; set; }

        public StaircaseDesign col_design { get; set; }


        //Chiranjit [2015 04 01]
        public TreeView TRV { get; set; }


        public List<ColumnData> All_Column_Data { get; set; }

        public frm_Staircase_BOQ(ASTRADoc ast_doc)
        {
            InitializeComponent();

            AST_DOC = ast_doc;

        }

        private void frm_Staircase_BOQ_Load(object sender, EventArgs e)
        {
            Select_Members();
            Load_Column_Data();
            //chk_sele_all.Checked = false;
        }


        private void btn_get_beams_Click(object sender, EventArgs e)
        {
            Select_Members();
        }
        List<double> Floors { get; set; }
        private void Select_Members()
        {
            dgv_columns.Rows.Clear();
            Floors = AST_DOC.Joints.Get_Floors();


            for (int i = 0; i < Floors.Count - 1; i++)
            {
                dgv_columns.Rows.Add(true, Floors[i].ToString("f4"),
                    "2",
                    ((Floors[i + 1] - Floors[i])*1000).ToString("f4"),
                    col_design.wf.ToString(),
                    col_design.wl.ToString(),
                    col_design.tw.ToString(),
                    col_design.bar_dia1.ToString(),
                    col_design.bar_dia2.ToString(),
                    "");
            }

        }


        private void cmb_sele_all_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgv_columns.RowCount; i++)
            {
                dgv_columns[0, i].Value = chk_sele_all.Checked;
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {

            //All_Column_Data = new List<ColumnData>();
            //int c = 0;
            //bool flag = false;
            //for (int i = 0; i < dgv_columns.RowCount; i++)
            //{
            //    try
            //    {
            //        c = 0;
            //        flag = (bool)dgv_columns[c++, i].Value;
            //        if (flag)
            //        {
            //            col_design.floor_level = MyList.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0.0);
            //            col_design.FlightNos = MyList.StringToInt(dgv_columns[c++, i].Value.ToString(), 2);
            //            col_design.lf = MyList.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0.0);
            //            col_design.wl = MyList.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0.0);
            //            col_design.tw = MyList.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0.0);
            //        }
            //    }
            //    catch (Exception exx) { }
            //}


            //this.DialogResult = DialogResult.OK;
            //this.Close();
            RunThread();
            //thd = new Thread(new ThreadStart(RunThread));
            //thd.Start();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        int lr = 0;

        public int Last_Row
        {
            set
            {
                bool chk = (bool)dgv_columns[0, value].Value;

                if (lr != value)
                {
                    if (chk)
                    {
                        dgv_columns[0, lr].Value = false;
                        lr = value;
                        dgv_columns[0, lr].Value = true;
                    }
                }

            }
            get
            {
                return lr;
            }
        }

        private void dgv_columns_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            return;
            bool chk = (bool)dgv_columns[0, e.RowIndex].Value;

            if (chk)
            {
                Last_Row = e.RowIndex;
            }
            //for (int i = 0; i < dgv_columns.RowCount; i++)
            //{
            //    if(i == e.RowIndex) continue;

            //    if ((bool)dgv_columns[0, i].Value)
            //        dgv_columns[0, i].Value = false;
            //}

            dgv_columns.Refresh();

        }



        #region Thread Functions

        public void RunThread()
        {
            List<string> list = new List<string>();

            #region TechSOFT Banner
            list.Add("");

            //list.Add("");
            //list.Add("\t\t**********************************************");
            //list.Add("\t\t*          ASTRA Pro Release 18.0            *");
            //list.Add("\t\t*      TechSOFT Engineering Services         *");
            //list.Add("\t\t*                                            *");
            //list.Add("\t\t*           DESIGN OF RCC COLUMN             *");
            //list.Add("\t\t*                                            *");
            //list.Add("\t\t**********************************************");
            //list.Add("\t\t----------------------------------------------");
            //list.Add("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            //list.Add("\t\t----------------------------------------------");
            //list.Add("");

            #endregion

          

            int c = 0;
            int step = 1;


            string ColumnNos = "";

            bool flag = false;
            //int c = 0;
            for (int i = 0; i < dgv_columns.RowCount; i++)
            {
                try
                {
                    c = 0;
                    flag = (bool)dgv_columns[c++, i].Value;
                    
                    if (flag)
                    {
                        Set_Grid_Color(i);

                        ////cd.ColumnNos = dgv_columns[c++, i].Value.ToString();
                        //if (MessageBox.Show("Process Design Column " + cd.ColumnNos + " \n\nDou you want to continue ?", 
                        //    "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        //{
                        //    break;
                        //}
                        //col_design.hf = (Floors[i + 1] - Floors[i]) * 1000;
                        col_design.floor_level = MyStrings.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0.0);
                        col_design.FlightNos = MyStrings.StringToInt(dgv_columns[c++, i].Value.ToString(), 0);
                        col_design.hf = MyStrings.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0);
                        col_design.wf = MyStrings.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0);
                        col_design.wl = MyStrings.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0);
                        col_design.tw = MyStrings.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0);
                        col_design.bar_dia1 = MyStrings.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0);
                        col_design.bar_dia2 = MyStrings.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0);


                        list.AddRange(col_design.Design_Program_Loop(i + 1).ToArray());
                        Set_Grid_Color(i, true, col_design.IS_DESIGN_OK);

                        dgv_columns[9, i].Value = col_design.IS_DESIGN_OK ? " OK " : "NOT OK";

                        Add_Staircase_BOQ(col_design.BOQ);

                    }

                }
                catch (Exception exx) { }
            }



           
            #region End of Report
            list.Add("");
            list.Add("---------------------------------------------------------------------------");
            list.Add("---------------------       END OF REPORT        --------------------------");
            list.Add("---------------------------------------------------------------------------");
            #endregion End of Report

            Save_Data();

            File.WriteAllLines(col_design.Report_File, list.ToArray());

            MessageBox.Show(this, "Report file created in file " + col_design.Report_File);

            frmASTRAReport fap = new frmASTRAReport(col_design.Report_File);
            fap.Owner = this;
            fap.ShowDialog();

        }

        public void Set_Grid_Color(int row_index, bool isdone, bool Is_Design_OK)
        {
            if (isdone)
            {
                if (Is_Design_OK)
                    dgv_columns.Rows[row_index].DefaultCellStyle.BackColor = Color.LawnGreen;
                else
                    dgv_columns.Rows[row_index].DefaultCellStyle.BackColor = Color.LightCoral;
            }
            else
                dgv_columns.Rows[row_index].DefaultCellStyle.BackColor = Color.White;
            dgv_columns.FirstDisplayedScrollingRowIndex = row_index;
        }

        public void Set_Grid_Color(int row_index)
        {
            Set_Grid_Color(row_index, false, false);
        }

        #endregion Thread Functions

        private void btn_oprn_report_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dgv_columns;

            int r = -1;
            if (dgv.SelectedCells.Count == 0) return;

            List<int> list_rows = new List<int>();

            int i = 0;
            bool fl = false;
            for (i = 0; i < dgv.RowCount; i++)
            {
                fl = (bool)dgv[0, i].Value;
                if (fl)
                    list_rows.Add(i);
            }

            list_rows.Sort();
            string f = "";


            List<string> list = new List<string>();
            list.AddRange(ColumnDesign.Get_Banner());

            foreach (var item in list_rows)
            {
                r = item;

                f = col_design.Get_Report_File(r + 1);

                if (File.Exists(f))
                {
                    list.AddRange(File.ReadAllLines(f));
                }
                else
                {
                    MessageBox.Show(this, string.Format("Design is not done for the selected Staircase {0}.", dgv[1, r].Value));
                }
            }
            if (list.Count > 0)
            {
                File.WriteAllLines(col_design.Report_File, list.ToArray());

                f = col_design.Report_File;

                if (File.Exists(f))
                {
                    frmASTRAReport.OpenReport(f, this);
                }
                else
                {
                    MessageBox.Show(this, "Design is not done for the selected Staircase(s).");
                }
            }
        }

        #region Save Data Function
        //Hashtable Beam_Data = new Hashtable();
        public void Save_Data()
        {
            string fname = col_design.Get_Report_File(1);
            fname = Path.Combine(Path.GetDirectoryName(fname), "STAIRCASE_DATA.TXT");

            List<string> list = new List<string>();

            string kStr = "";
            int i = 0;
            for (i = 0; i < dgv_columns.RowCount; i++)
            {
                kStr = "";
                for (int c = 0; c < dgv_columns.ColumnCount; c++)
                {
                    kStr += string.Format("{0}$", dgv_columns[c, i].Value.ToString());
                }
                list.Add(kStr);
            }

            File.WriteAllLines(fname, list.ToArray());


        }

        void Load_Column_Data()
        {
            string fname = col_design.Get_Report_File(1);
            fname = Path.Combine(Path.GetDirectoryName(fname), "STAIRCASE_DATA.TXT");


            if (File.Exists(fname))
            {
                List<string> list = new List<string>(File.ReadAllLines(fname));

                int c = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    MyStrings mli = new MyStrings(list[i], '$');
                    try
                    {
                        c = 2;
                        dgv_columns[c, i].Value = mli.StringList[c]; c++;
                        dgv_columns[c, i].Value = mli.StringList[c]; c++;
                        dgv_columns[c, i].Value = mli.StringList[c]; c++;
                        dgv_columns[c, i].Value = mli.StringList[c]; c++;
                        dgv_columns[c, i].Value = mli.StringList[c]; c++;
                        dgv_columns[c, i].Value = mli.StringList[c]; c++;
                        dgv_columns[c, i].Value = mli.StringList[c]; c++;
                        dgv_columns[c, i].Value = mli.StringList[c];

                        if (mli.StringList[c] != "")
                            Set_Grid_Color(i, true, !mli.StringList[c].Contains("NOT"));

                    }
                    catch (Exception exc)
                    {

                    }
                }
            }

        }
        #endregion Save Data Function

        private void btn_Modify_Click(object sender, EventArgs e)
        {
            
            Button btn = sender as Button;
            if (btn.Name == btn_Modify.Name)
            {
                frm_Modify_Staircase_BOQ fmboq = new frm_Modify_Staircase_BOQ();
                fmboq.DGV_INPUTS = dgv_columns;
                fmboq.ShowDialog();
            }
        }

        private void Update_Data()
        {
            MovingLoadAnalysis.frm_ProgressBar.ON("Update Analysis Input Data....");
            for (int i = 0; i < dgv_columns.RowCount; i++)
            {
                MovingLoadAnalysis.frm_ProgressBar.SetValue(i, dgv_columns.RowCount);
                //Set_Grid_Color(i);
                Update_Data(string.Format("{0} PRISMATIC YD {1} ZD {2}", dgv_columns[2, i].Value
                    , dgv_columns[3, i].Value
                    , dgv_columns[4, i].Value));
                //Set_Grid_Color(i);

            }
            MovingLoadAnalysis.frm_ProgressBar.OFF();
            MessageBox.Show("Data Sucessfully Updated.", "ASTRA", MessageBoxButtons.OK);

        }
        public void Update_Data(string UpdText)
        {
            if (TRV == null) return;
            List<List<int>> list1 = new List<List<int>>();

            List<int> mems = new List<int>();
            List<int> Updmems = new List<int>();

            //string UpdText = "1 TO 10 PR YD 0.410 ZD 0.32";




            MyStrings mmls = new MyStrings(UpdText, ' ');

            int intx = 0;
            string kStr = "";
            string PR = "";
            for (intx = 0; intx < mmls.Count; intx++)
            {
                if (mmls.StringList[intx].StartsWith("PR"))
                {
                    kStr = mmls.GetString(0, intx - 1);
                    PR = mmls.GetString(intx);
                    break;
                }
            }
            Updmems = MyStrings.Get_Array_Intiger(kStr);
            string PR_Upd = PR;


            for (int i = 0; i < TRV.Nodes.Count; i++)
            {
                //mems = MyStrings.Get_Array_Intiger(TRV.Nodes[i].Text);

                mmls = new MyStrings(TRV.Nodes[i].Text, ' ');

                intx = 0;
                kStr = "";
                PR = "";
                for (intx = 0; intx < mmls.Count; intx++)
                {
                    if (mmls.StringList[intx].StartsWith("PR"))
                    {
                        kStr = mmls.GetString(0, intx - 1);
                        PR = mmls.GetString(intx);
                        break;
                    }
                }
                mems = MyStrings.Get_Array_Intiger(kStr);


                foreach (var item in Updmems)
                {
                    if (mems.Contains(item)) mems.Remove(item);
                }
                if (mems.Count > 0)
                    TRV.Nodes[i].Text = MyStrings.Get_Array_Text(mems) + " " + PR;
                else
                {
                    TRV.Nodes.RemoveAt(i);
                    i--;
                }
                list1.Add(mems);
            }
            TRV.Nodes.Add(UpdText);

            //string kStr = "";

        }

       

    }
}
