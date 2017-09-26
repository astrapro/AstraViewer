using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Text;
using System.Windows.Forms;
using HEADSNeed.ASTRA.ASTRAClasses;
using HEADSNeed.ASTRA.ASTRAClasses.StructureDesign;


using MovingLoadAnalysis;

namespace HEADSNeed.ASTRA.ASTRAForms
{
    public delegate void sAdd_ISO_Foundation_BOQ(ISO_Foundation_BOQ fboq);

    public partial class frm_Footing_BOQ : Form
    {
        
        ASTRADoc AST_DOC { get; set; }
        public StructureMemberAnalysis StructureAnalysis { get; set; }


        public sAdd_ISO_Foundation_BOQ Add_Foundation_BOQ;
        //public List<BeamData> All_Beam_Data { get; set; }

        public IsolatedFoundationDesign Foot_Des { get; set; }


        public frm_Footing_BOQ(ASTRADoc ast_doc)
        {
            InitializeComponent();

            AST_DOC = ast_doc;
        }
        public frm_Footing_BOQ()
        {
            InitializeComponent();
        }

        private void frm_Footing_BOQ_Load(object sender, EventArgs e)
        {
            Select_Members();
            Load_Footing_Data();

        }


        private void Select_Members()
        {

            double min_y = 999;
            for (int i = 0; i < AST_DOC.Members.Count; i++)
            {
                var item = AST_DOC.Members[i];

                if (item.StartNode.Y < min_y)
                    min_y = item.StartNode.Y;
                if (item.EndNode.Y < min_y)
                    min_y = item.EndNode.Y;

            }

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
                    if(item.EndNode.Y == min_y ||
                        item.StartNode.Y == min_y)
                    list_mem.Add(item);
                }
            }

            bool flag = false;


            MemberIncidence mi = null;

            double P = 0.0;
            double Mx = 0.0;

            List<int> all_jnt = new List<int>();

            for (int i = 0; i < list_mem.Count; i++)
            {
                var item = list_mem[i];

                all_jnt.Clear();
                all_jnt.Add(item.StartNode.NodeNo);
                all_jnt.Add(item.EndNode.NodeNo);

                P = StructureAnalysis.GetJoint_R1_Axial(all_jnt);
                //Mx = StructureAnalysis.GetJoint_M1_Torsion(all_jnt);
                Mx = StructureAnalysis.GetJoint_MomentForce(all_jnt);
                mi = item;
                dgv_columns.Rows.Add((i + 1), true, "F" + (i + 1), item.MemberNo, 
                    mi.Property.YD, 
                    mi.Property.ZD, 
                    Foot_Des.l,
                    Foot_Des.b,
                    Foot_Des.P1,
                    Foot_Des.P2,
                    Foot_Des.ph, 
                    Foot_Des.bar_dia, 
                    P, Mx, "");
            }
        }

        public List<int> Get_Continuous_Members(MemberIncidence b1)
        {

            JointCoordinateCollection cont_jcc = new JointCoordinateCollection();
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
                dgv_columns[0, i].Value = i + 1;
                dgv_columns[1, i].Value = chk_sele_all.Checked;
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            //All_Beam_Data = new List<BeamData>();
            //int c = 0;
            //for (int i = 0; i < dgv_beams.RowCount; i++)
            //{
            //    try
            //    {
            //        c = 0;
            //        BeamData bd = new BeamData();
            //        bd.IsSelect = (bool)dgv_beams[c++, i].Value;
            //        if (bd.IsSelect)
            //        {
            //            bd.BeamNos = dgv_beams[c++, i].Value.ToString();
            //            bd.Continuous_BeamMembers = dgv_beams[c++, i].Value.ToString();
            //            bd.Breadth = MyStrings.StringToDouble(dgv_beams[c++, i].Value.ToString(), 0.0);
            //            bd.Depth = MyStrings.StringToDouble(dgv_beams[c++, i].Value.ToString(), 0.0);
            //            bd.d1 = MyStrings.StringToDouble(dgv_beams[c++, i].Value.ToString(), 0.0);
            //            bd.d2 = MyStrings.StringToDouble(dgv_beams[c++, i].Value.ToString(), 0.0);
            //            bd.d3 = MyStrings.StringToDouble(dgv_beams[c++, i].Value.ToString(), 0.0);
            //            bd.d4 = MyStrings.StringToDouble(dgv_beams[c++, i].Value.ToString(), 0.0);
            //            bd.d5 = MyStrings.StringToDouble(dgv_beams[c++, i].Value.ToString(), 0.0);
            //            bd.d6 = MyStrings.StringToDouble(dgv_beams[c++, i].Value.ToString(), 0.0);
            //            All_Beam_Data.Add(bd);
            //        }

            //    }
            //    catch (Exception exx) { }
            //}

            //this.DialogResult = DialogResult.OK;
            //this.Close();

            //RunThread();

            Thread thd = new Thread(new ThreadStart(RunThread));
            thd.Start();
            thd.Join();

            Save_Data();
            MessageBox.Show(this, "Report file created in file " + Foot_Des.Report_File);
            frmASTRAReport.OpenReport(Foot_Des.Report_File, this);
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


        #region Thread Functions


        List<string> Design_Summary { get; set; }
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
            list.Add("\t\t*      DESIGN OF ISOLATED FOUNDATION         *");
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
                    c = 1;
                    ColumnData cd = new ColumnData();
                    cd.IsSelect = (bool)dgv_columns[c++, i].Value;
                    if (cd.IsSelect)
                    {
                        Set_Grid_Color(i, false, Foot_Des.IS_DESIGN_OK);

                        Foot_Des.ColumnNo = dgv_columns[c++, i].Value.ToString(); c++;
                        //Foot_Des.Continuous_ColumnMembers = dgv_columns[c++, i].Value.ToString();
                        Foot_Des.C1 = MyStrings.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0.0);
                        Foot_Des.C2 = MyStrings.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0.0);
                       
                        Foot_Des.l = MyStrings.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0.0);
                        Foot_Des.b = MyStrings.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0.0);
                       
                        Foot_Des.P1 = MyStrings.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0.0);
                        Foot_Des.P2 = MyStrings.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0.0);
                        Foot_Des.ph = MyStrings.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0.0);
                        
                        Foot_Des.bar_dia = MyStrings.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0.0);
                        Foot_Des.P = MyStrings.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0.0);
                        Foot_Des.Mx = MyStrings.StringToDouble(dgv_columns[c++, i].Value.ToString(), 0.0);

                        //Foot_Des.Mx = MyStrings.StringToDouble(dgv_columns[c, i].Value.ToString(), 0.0);

                        list.AddRange(Foot_Des.Design_Program_Loop(i + 1).ToArray());
                        Design_Summary.Add(string.Format("------------------------------------------------"));
                        //Design_Summary.Add(string.Format("FOUNDATION = {0}, ELEVATION = {1} M", Foot_Des.F));
                        Design_Summary.Add(string.Format("FOUNDATION = {0}", Foot_Des.ColumnNo));
                        //Design_Summary.Add(string.Format("CONTINUOUS MEMBERS = {0}", cd.Continuous_ColumnMembers));
                        Design_Summary.AddRange(Foot_Des.Design_Summary.ToArray());
                        Design_Summary.Add(string.Format("------------------------------------------------"));
                       

                        #region End of Report
                        list.Add("");
                        #endregion End of Report

                        dgv_columns[c, i].Value = Foot_Des.IS_DESIGN_OK ? " OK" : "NOT OK";
                        Set_Grid_Color(i, true, Foot_Des.IS_DESIGN_OK);

                        Add_Foundation_BOQ(Foot_Des.BOQ);
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



            File.WriteAllLines(Foot_Des.Report_File, list.ToArray());

            string des_sum = Path.Combine(Path.GetDirectoryName(Foot_Des.Get_Report_File(1)), "FOUNDATION_DESIGN_SUMMARY.TXT");
            File.WriteAllLines(des_sum, Design_Summary.ToArray());

        }

        public void Set_Grid_Color(int row_index, bool isdone, bool Is_Design_OK)
        {
            if (isdone)
            {
                if (Is_Design_OK)
                    dgv_columns.Rows[row_index].DefaultCellStyle.BackColor = Color.LawnGreen;
                else
                    dgv_columns.Rows[row_index].DefaultCellStyle.BackColor = Color.LightCoral;

                if(row_index > 6)
                    dgv_columns.FirstDisplayedScrollingRowIndex = row_index - 4;
                else
                    dgv_columns.FirstDisplayedScrollingRowIndex = 0;
            }
            else
                dgv_columns.Rows[row_index].DefaultCellStyle.BackColor = Color.Yellow;
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
                fl = (bool)dgv[1, i].Value;
                if (fl)
                    list_rows.Add(i);
            }



            list_rows.Sort();
            string f = "";


            List<string> list = new List<string>();
            list.AddRange(IsolatedFoundationDesign.Get_Banner());
            foreach (var item in list_rows)
            {
                r = item;

                f = Foot_Des.Get_Report_File(r + 1);

                if (File.Exists(f))
                {
                    list.AddRange(File.ReadAllLines(f));
                }
                else
                {
                    MessageBox.Show(this, string.Format("Design is not done for the selected Isolated Footing {0}.", dgv[1, r].Value));
                }
            }
            if (list.Count > 0)
            {
                f = Foot_Des.Report_File;
                File.WriteAllLines(f, list.ToArray());

                if (File.Exists(f))
                {
                    frmASTRAReport.OpenReport(f, this);
                }
                else
                {
                    MessageBox.Show(this, "Design is not done for the selected Column(s).");
                }
            }
        }

        #region Save Data Function
        //Hashtable Beam_Data = new Hashtable();
        public void Save_Data()
        {
            string fname = Foot_Des.Get_Report_File(1);
            fname = Path.Combine(Path.GetDirectoryName(fname), "FOOTING_DATA.TXT");

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

        void Load_Footing_Data()
        {
            string fname = Foot_Des.Get_Report_File(1);
            fname = Path.Combine(Path.GetDirectoryName(fname), "FOOTING_DATA.TXT");


            if (File.Exists(fname))
            {
                List<string> list = new List<string>(File.ReadAllLines(fname));

                int c = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    MyStrings mli = new MyStrings(list[i], '$');
                    try
                    {
                        c = 4;
                        dgv_columns[c, i].Value = mli.StringList[c]; c++;
                        dgv_columns[c, i].Value = mli.StringList[c]; c++;
                        dgv_columns[c, i].Value = mli.StringList[c]; c++;
                        dgv_columns[c, i].Value = mli.StringList[c]; c++;
                        dgv_columns[c, i].Value = mli.StringList[c]; c++;
                        dgv_columns[c, i].Value = mli.StringList[c]; c++;
                        dgv_columns[c, i].Value = mli.StringList[c]; c++;
                        dgv_columns[c, i].Value = mli.StringList[c]; c++;

                        //dgv_columns[c, i].Value = mli.StringList[c]; 
                        c++;
                        //dgv_columns[c, i].Value = mli.StringList[c]; 
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
            frm_Modify_Footing_BOQ fmboq = new frm_Modify_Footing_BOQ();
            fmboq.DGV_INPUTS = dgv_columns;
            fmboq.ShowDialog();
        }
    }
}
