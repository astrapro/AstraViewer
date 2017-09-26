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
    public partial class frmLoadCombination : Form
    {

        List<int> list_mem_Nos = null;
        IASTRACAD iACad = null;
        public List<string> ASTRA_Data { get; set; }
        public TreeNode Node { get; set; }

        List<LoadCaseDefinition> ALL_Loads { get; set; }

        public LoadCombinations LDC { get; set; }
        public bool IsRepeatLoad { get; set; }


        public frmLoadCombination(IASTRACAD iACAD, List<LoadCaseDefinition> all_loads)
        {
            InitializeComponent();
            list_mem_Nos = new List<int>();
            this.iACad = iACAD;
            ASTRA_Data = new List<string>();
            ALL_Loads = all_loads;
            LDC = new LoadCombinations();
            IsRepeatLoad = false;

        }

        private void frmLoadCombination_Load(object sender, EventArgs e)
        {
            if (IsRepeatLoad)
            {
                this.Text = "REPEAT LOAD";
                txt_load_no.Text = LDC.LoadNo.ToString();
                txt_load_name.Text = LDC.Name;
                txt_load_no.Enabled = false;
                txt_load_name.Enabled = false;
            }

            if (ALL_Loads.Count > 0)
            {
                lst_loadcases.Items.Clear();
                for (int i = 0; i < ALL_Loads.Count; i++)
                {
                    if (!ALL_Loads[i].Is_Load_Combination)
                    {
                        if (LDC.Count > 0)
                        {
                            if (!LDC.LoadCases.Contains(ALL_Loads[i].LoadNo))
                                lst_loadcases.Items.Add(ALL_Loads[i].ToString());
                        }
                        else
                            lst_loadcases.Items.Add(ALL_Loads[i].ToString());
                    }
                }
            }

            if (LDC.Count > 0)
            {
                txt_load_no.Text = LDC.LoadNo.ToString();
                txt_load_name.Text = LDC.Name;
                dgv_combinations.Rows.Clear();

                for (int i = 0; i < LDC.Count; i++)
                {
                    dgv_combinations.Rows.Add("Load Case " + LDC.LoadCases[i], LDC.Factors[i].ToString("f3"));
                }
                btn_add.Text = "Change";
            }
            else
            {
                if (lst_loadcases.Items.Count > 0)
                    lst_loadcases.SelectedIndex = 0;
            }
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            MyStrings mlist = null;

            List<int> indices = new List<int>();
            List<int> loadNo = new List<int>();

            if (btn.Name == btn_insert.Name)
            {
                for (int i = 0; i < lst_loadcases.SelectedIndices.Count; i++)
                {
                    indices.Add(lst_loadcases.SelectedIndices[i]);
                    mlist = new MyStrings(lst_loadcases.Items[lst_loadcases.SelectedIndices[i]].ToString(), ':');

                    loadNo.Add(mlist.GetInt(0));

                }

                indices.Sort();
                indices.Reverse();

                for (int i = 0; i < indices.Count; i++)
                {
                    lst_loadcases.Items.RemoveAt(indices[i]);
                }


                for (int i = 0; i < ALL_Loads.Count; i++)
                {
                    if (loadNo.Contains(ALL_Loads[i].LoadNo))
                        dgv_combinations.Rows.Add("Load Case " + ALL_Loads[i].LoadNo, "1.0");
                }
            }
            else if (btn.Name == btn_insert_all.Name)
            {

                for (int i = 0; i < lst_loadcases.Items.Count; i++)
                {
                    indices.Add(i);
                    mlist = new MyStrings(lst_loadcases.Items[i].ToString(), ':');

                    loadNo.Add(mlist.GetInt(0));

                }

                indices.Sort();
                indices.Reverse();
                for (int i = 0; i < indices.Count; i++)
                {
                    lst_loadcases.Items.RemoveAt(indices[i]);
                }


                for (int i = 0; i < ALL_Loads.Count; i++)
                {
                    if (loadNo.Contains(ALL_Loads[i].LoadNo))
                        dgv_combinations.Rows.Add("Load Case " + ALL_Loads[i].LoadNo, "1.0");
                }

            }
            else if (btn.Name == btn_remove.Name)
            {
                int r = -1;
                for (int i = 0; i < dgv_combinations.SelectedCells.Count; i++)
                {
                    r = dgv_combinations.SelectedCells[i].RowIndex;
                    indices.Add(r);
                    mlist = new MyStrings(dgv_combinations[0,r].Value.ToString().ToUpper().Replace("LOAD CASE ", ""), ' ');
                    loadNo.Add(mlist.GetInt(0));
                }
                for (int i = 0; i < indices.Count; i++)
                {
                    dgv_combinations.Rows.RemoveAt(indices[i]);
                }


                for (int i = 0; i < ALL_Loads.Count; i++)
                {
                    if (loadNo.Contains(ALL_Loads[i].LoadNo))
                        lst_loadcases.Items.Add(ALL_Loads[i].ToString());
                }
    
            }
            else if (btn.Name == btn_remove_all.Name)
            {

                int r = -1;
                for (int i = 0; i < dgv_combinations.RowCount; i++)
                {
                    r = i;
                    indices.Add(r);
                    mlist = new MyStrings(dgv_combinations[0, r].Value.ToString().ToUpper().Replace("LOAD CASE ", ""), ' ');
                    loadNo.Add(mlist.GetInt(0));
                }
                dgv_combinations.Rows.Clear();
               


                for (int i = 0; i < ALL_Loads.Count; i++)
                {
                    if (loadNo.Contains(ALL_Loads[i].LoadNo))
                        lst_loadcases.Items.Add(ALL_Loads[i].ToString());
                }
            }

        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            LDC.LoadNo = MyStrings.StringToInt(txt_load_no.Text, 0);
            LDC.Name = txt_load_name.Text;
            LDC.LoadCases.Clear();
            LDC.Factors.Clear();
            for (int i = 0; i < dgv_combinations.RowCount; i++)
            {
                LDC.LoadCases.Add(MyStrings.StringToInt(dgv_combinations[0,i].Value.ToString().ToUpper().Replace("LOAD CASE ",""), 0)); 
                LDC.Factors.Add(MyStrings.StringToDouble(dgv_combinations[1,i].Value.ToString(), 0)); 
            }
            LDC.Set_Combination();
            this.Close();
        }


    }
}
