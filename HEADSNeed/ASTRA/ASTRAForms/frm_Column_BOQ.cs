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
    public delegate void sAdd_Column_BOQ(Column_BOQ bboq);

    public partial class frm_Column_BOQ : Form
    {

        Thread thd;

        public sAdd_Column_BOQ Add_Column_BOQ;


        ASTRADoc AST_DOC { get; set; }
        public StructureMemberAnalysis StructureAnalysis { get; set; }
        public double Main_Bar_Dia { get; set; }
        public double Tie_Bar_Dia { get; set; }

        public ColumnDesign col_design { get; set; }


        //Chiranjit [2015 04 01]
        public TreeView TRV { get; set; }


        public List<ColumnData> All_Column_Data { get; set; }

        public frm_Column_BOQ(ASTRADoc ast_doc)
        {
            InitializeComponent();

            AST_DOC = ast_doc;

        }

        private void frm_Column_BOQ_Load(object sender, EventArgs e)
        {
            Select_Members();
            Load_Column_Data();
            chk_sele_all.Checked = true;
        }


        private void btn_get_beams_Click(object sender, EventArgs e)
        {
            Select_Members();
        }

        private void Select_Members()
        {
            dgv_columns.Rows.Clear();

            chk_sele_all.Checked = true;

            List<MemberIncidence> list_mem = new List<MemberIncidence>();
            List<int> list_mem1 = new List<int>();
            List<List<int>> list_conts = new List<List<int>>();
            for (int i = 0; i < AST_DOC.Members.Count; i++)
            {
                var item = AST_DOC.Members[i];

                if (item.EndNode.X == item.StartNode.X &&
                    item.EndNode.Y != item.StartNode.Y &&
                    item.EndNode.Z == item.StartNode.Z)
                {
                    list_mem.Add(item);
                }
            }

            bool flag = false;


            List<int> lst_jnt = new List<int>();
            List<List<int>> all_jnt = new List<List<int>>();

            MovingLoadAnalysis.frm_ProgressBar.ON("Reading Members......");
            for (int i = 0; i < list_mem.Count; i++)
            {
                MovingLoadAnalysis.frm_ProgressBar.SetValue(i, list_mem.Count); 
                flag = false;
                foreach (var item in list_conts)
                {
                    if (item.Contains(list_mem[i].MemberNo))
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    JointCoordinateCollection cont_jcc = new JointCoordinateCollection();


                    list_mem1 = Get_Continuous_Members(list_mem[i], ref cont_jcc);
                    list_conts.Add(list_mem1);

                    lst_jnt = new List<int>();
                    foreach (var item in cont_jcc)
                    {
                        lst_jnt.Add(item.NodeNo);
                    }
                    all_jnt.Add(lst_jnt);
                }
            }
            MovingLoadAnalysis.frm_ProgressBar.OFF();

            
            MemberIncidence mi = null;


            double Pu = 0.0;
            double Mux = 0.0;
            double Muy = 0.0;
            for (int i = 0; i < list_conts.Count; i++)
            {
                var item = list_conts[i];
                mi = AST_DOC.Members.Get_Member(item[0]);

                Pu = StructureAnalysis.GetJoint_R1_Axial(all_jnt[i]);
                Mux = StructureAnalysis.GetJoint_M2_Bending(all_jnt[i]);
                Muy = StructureAnalysis.GetJoint_M3_Bending(all_jnt[i]);

                dgv_columns.Rows.Add(true, "C" + (i + 1), MyStrings.Get_Array_Text(item), mi.Property.YD, mi.Property.ZD, Main_Bar_Dia, col_design.bar_nos, Tie_Bar_Dia, Pu, Mux, Muy, "");
            }
        }

        public List<int> Get_Continuous_Members(MemberIncidence b1, ref  JointCoordinateCollection cont_jcc)
        {
            //JointCoordinateCollection cont_jcc = new JointCoordinateCollection();
            MemberIncidenceCollection mbr_coll = new MemberIncidenceCollection();

            if (b1 == null) return new List<int>();

            List<MemberIncidence> list = new List<MemberIncidence>();


            JointCoordinateCollection jcc = new JointCoordinateCollection();


            jcc.Add(b1.StartNode);
            jcc.Add(b1.EndNode);


            MemberIncidenceCollection mic1 = new MemberIncidenceCollection();
            MemberIncidenceCollection mic2 = new MemberIncidenceCollection();
            //MemberIncidenceCollection mic3 = new MemberIncidenceCollection();
            MemberIncidenceCollection mic4 = new MemberIncidenceCollection();


            foreach (var item in AST_DOC.Members)
            {
                if (item.EndNode.NodeNo == jcc[0].NodeNo || item.StartNode.NodeNo == jcc[0].NodeNo)
                    mic1.Add(item);
                if (item.EndNode.NodeNo == jcc[1].NodeNo || item.StartNode.NodeNo == jcc[1].NodeNo)
                    mic2.Add(item);
            }




            //if (jcc[0].NodeNo < jcc[1].NodeNo)
            //{
            //    if (!cont_jcc.Contains(jcc[1]))
            //    {
            //        cont_jcc.Add(jcc[1]);
            //    }
            //}
            cont_jcc.Add(jcc[0]);
            cont_jcc.Add(jcc[1]);

            int flag = 0;



            mbr_coll.Add(b1);
            while (flag <= 1)
            {
                for (int i = 0; i < AST_DOC.Members.Count; i++)
                {
                    foreach (var item in AST_DOC.Members)
                    {
                        if (b1.Direction == item.Direction)
                        {
                            if (item.StartNode.NodeNo == cont_jcc[cont_jcc.Count - 1].NodeNo)
                            {
                                if (!cont_jcc.Contains(item.EndNode))
                                {
                                    mbr_coll.Add(item);
                                    cont_jcc.Add(item.EndNode);
                                    i = 0; break;
                                }
                            }
                            if (item.EndNode.NodeNo == cont_jcc[cont_jcc.Count - 1].NodeNo)
                            {
                                if (!cont_jcc.Contains(item.StartNode))
                                {
                                    mbr_coll.Add(item);
                                    cont_jcc.Add(item.StartNode);
                                    i = 0; break;
                                }
                            }
                        }
                    }
                }

                if (mbr_coll.Count == 1)
                {
                    cont_jcc.Clear();
                    cont_jcc.Add(jcc[1]);
                    cont_jcc.Add(jcc[0]);
                }
                else
                    break;
                flag++;
            }



            List<int> mls = new List<int>();

            foreach (var item in mbr_coll)
            {
                mls.Add(item.MemberNo);
            }

            return mls;
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
            //for (int i = 0; i < dgv_columns.RowCount; i++)
            //{
            //    try
            //    {
            //        c = 0;
            //        ColumnData cd = new ColumnData();
            //        cd.IsSelect = (bool)dgv_columns[c++, i].Value;
            //        if (cd.IsSelect)
            //        {
            //            cd.ColumnNos = dgv_columns[c++, i].Value.ToString();
            //            cd.Continuous_ColumnMembers = dgv_columns[c++, i].Value.ToString();
            //            cd.Breadth = MyStrings.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0.0);
            //            cd.Depth = MyStrings.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0.0);
            //            cd.Main_Bar_dia = MyStrings.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0.0);
            //            cd.Tie_Bar_Dia = MyStrings.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0.0);
            //            cd.Pu = MyStrings.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0.0);
            //            cd.Mux = MyStrings.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0.0);
            //            cd.Muy = MyStrings.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0.0);
            //            All_Column_Data.Add(cd);
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


        public List<string> Design_Summary { get; set; }

        public void RunThread()
        {
            List<string> list = new List<string>();

            #region TechSOFT Banner
            list.Add("");
            list.Add("");
            list.Add("\t\t**********************************************");
            list.Add("\t\t*          ASTRA Pro Release 18.0            *");
            list.Add("\t\t*      TechSOFT Engineering Services         *");
            list.Add("\t\t*                                            *");
            list.Add("\t\t*           DESIGN OF RCC COLUMN             *");
            list.Add("\t\t*                                            *");
            list.Add("\t\t**********************************************");
            list.Add("\t\t----------------------------------------------");
            list.Add("\t\tTHIS RESULT CREATED ON " + System.DateTime.Now.ToString("dd.MM.yyyy  AT HH:mm:ss") + " ");
            list.Add("\t\t----------------------------------------------");
            list.Add("");

            #endregion

          

            int c = 0;
            int step = 1;


            string ColumnNos = "";

            Design_Summary = new List<string>();
            //int c = 0;
            for (int i = 0; i < dgv_columns.RowCount; i++)
            {
                try
                {
                    c = 0;
                    ColumnData cd = new ColumnData();
                    cd.IsSelect = (bool)dgv_columns[c++, i].Value;
                    if (cd.IsSelect)
                    {
                        Set_Grid_Color(i);

                        cd.ColumnNos = dgv_columns[c++, i].Value.ToString();
                        if (MessageBox.Show("Process Design Column " + cd.ColumnNos + " \n\nDo you want to continue ?", "ASTRA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            break;
                        }
                        cd.Continuous_ColumnMembers = dgv_columns[c, i].Value.ToString(); c++;
                        cd.Breadth = MyStrings.StringToDouble(dgv_columns[c, i].Value.ToString(), 0.0); c++;
                        cd.Depth = MyStrings.StringToDouble(dgv_columns[c, i].Value.ToString(), 0.0); c++;
                        cd.Main_Bar_dia = MyStrings.StringToDouble(dgv_columns[c, i].Value.ToString(), 0.0); c++;
                        cd.Bar_Nos = MyStrings.StringToDouble(dgv_columns[c, i].Value.ToString(), 0.0); c++;
                        cd.Tie_Bar_Dia = MyStrings.StringToDouble(dgv_columns[c, i].Value.ToString(), 0.0); c++;
                        cd.Pu = MyStrings.StringToDouble(dgv_columns[c, i].Value.ToString(), 0.0); c++;
                        cd.Mux = MyStrings.StringToDouble(dgv_columns[c, i].Value.ToString(), 0.0); c++;
                        cd.Muy = MyStrings.StringToDouble(dgv_columns[c, i].Value.ToString(), 0.0); c++;



                        //col_design.ColumnNos = All_Column_Data[i].Continuous_ColumnMembers;

                        col_design.Beam_Nos = cd.Continuous_ColumnMembers;
                        col_design.D = cd.Depth * 1000;
                        col_design.b = cd.Breadth * 1000;
                        col_design.bar_dia = cd.Main_Bar_dia;
                        col_design.bar_nos = cd.Bar_Nos;
                        col_design.tie_dia = cd.Tie_Bar_Dia;
                        col_design.Pu = cd.Pu;
                        col_design.Mux = cd.Mux;
                        col_design.Muy = cd.Muy;

                        //col_design.All_Column_Data.Clear();
                        col_design.Col_Data = cd;

                        if (Design_Summary == null)
                            Design_Summary = new List<string>();

                        Design_Summary.Add(string.Format("")); 
                        list.AddRange(col_design.Design_Program_Loop(i + 1).ToArray());

                        Design_Summary.Add(string.Format("------------------------------------------------"));
                        Design_Summary.Add(string.Format("COLUMN = {0}", cd.ColumnNos));
                        Design_Summary.Add(string.Format("CONTINUOUS MEMBERS = {0}", cd.Continuous_ColumnMembers));
                        Design_Summary.AddRange(col_design.Design_Summary.ToArray());
                        Design_Summary.Add(string.Format("------------------------------------------------"));
                       



                        Set_Grid_Color(i, true, col_design.IS_DESIGN_OK);

                        dgv_columns[c, i].Value = col_design.IS_DESIGN_OK ? " OK " : "NOT OK";

                        Add_Column_BOQ(col_design.BOQ);

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


            string des_sum = Path.Combine(Path.GetDirectoryName(col_design.Get_Report_File(1)), "COLUMN_DESIGN_SUMMARY.TXT");
            File.WriteAllLines(des_sum, Design_Summary.ToArray());



            
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
                dgv_columns.Rows[row_index].DefaultCellStyle.BackColor = Color.Yellow;

            if(row_index > 3 )
                dgv_columns.FirstDisplayedScrollingRowIndex = row_index - 3;
            else
                dgv_columns.FirstDisplayedScrollingRowIndex = 0;
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
            if (list_rows.Count > 0)
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
                    MessageBox.Show(this, string.Format("Design is not done for the selected Column {0}.", dgv[1, r].Value));
                    return;
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
                    MessageBox.Show(this, string.Format("Design is not done for the selected Column(s).", dgv[1, r].Value));
                }
            }
        }

        #region Save Data Function
        //Hashtable Beam_Data = new Hashtable();
        public void Save_Data()
        {
            string fname = col_design.Get_Report_File(1);
            fname = Path.Combine(Path.GetDirectoryName(fname), "COLUMN_DATA.TXT");

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
            fname = Path.Combine(Path.GetDirectoryName(fname), "COLUMN_DATA.TXT");


            if (File.Exists(fname))
            {
                List<string> list = new List<string>(File.ReadAllLines(fname));

                int c = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    MyStrings mli = new MyStrings(list[i], '$');
                    try
                    {
                        c = 3;
                        dgv_columns[c, i].Value = mli.StringList[c]; c++;
                        dgv_columns[c, i].Value = mli.StringList[c]; c++;
                        dgv_columns[c, i].Value = mli.StringList[c]; c++;
                        dgv_columns[c, i].Value = mli.StringList[c]; c++;
                        //dgv_columns[c, i].Value = mli.StringList[c]; 
                        c++;
                        //dgv_columns[c, i].Value = mli.StringList[c]; 
                        c++;
                        //dgv_columns[c, i].Value = mli.StringList[c]; 
                        c++;
                        c++;
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

            if (btn.Name == btn_update_ana_input.Name)
            {
                //thd = new Thread(new ThreadStart(Update_Data));
                //thd.Start();
                //thd.Join();
                Update_Data();
            }
            else if (btn.Name == btn_Modify.Name)
            {
                frm_Modify_Column_BOQ fmboq = new frm_Modify_Column_BOQ();
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
