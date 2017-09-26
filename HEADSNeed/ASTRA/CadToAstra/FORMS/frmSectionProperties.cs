using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdCollections;

using HEADSNeed.ASTRA.ASTRAClasses;
//using MovingLoadAnalysis;
namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    public partial class frmSectionProperties : Form
    {
        vdDocument doc = null;
        ASTRADoc astDoc = null;
        List<int> list_mem_No = null;
        IASTRACAD iACad = null;
        string kStr = "";
        public List<string> ASTRA_Data { get; set; }
        public MemberGroupCollection MGC { get; set; }
        public TreeView TRV { get; set; }



        public DataGridView DGV_Joints { get; set; }
        public DataGridView DGV_Members { get; set; }

        public frmSectionProperties(IASTRACAD iACAD)
        {
            InitializeComponent();
            list_mem_No = new List<int>();

            this.iACad = iACAD;
            this.doc = iACAD.Document;
            this.astDoc = iACAD.AstraDocument;
            ShowMemberInText();

            ASTRA_Data = new List<string>();
        }
        public vdDocument Document
        {
            get
            {
                return doc;
            }
        }
        private vdSelection GetGripSelection(bool Create)
        {
            VectorDraw.Professional.vdCollections.vdSelection gripset;
            string selsetname = "VDGRIPSET_" + doc.ActiveLayOut.Handle.ToStringValue() + (doc.ActiveLayOut.ActiveViewPort != null ? doc.ActiveLayOut.ActiveViewPort.Handle.ToStringValue() : "");
            gripset = doc.ActiveLayOut.Document.Selections.FindName(selsetname);
            if (Create)
            {
                if (gripset == null)
                {
                    gripset = doc.ActiveLayOut.Document.Selections.Add(selsetname);
                }
            }
            //vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.Document.Selections.FindName(selsetname).RemoveAll();
            return gripset;
        }
        public void ShowMemberInText()
        {
            List<vdLine> list_lines = new List<vdLine>();
            vdSelection gripset = GetGripSelection(false);

            //if (gripset.Count > 1) return;
            vdLine ln;
            foreach (vdFigure fig in gripset)
            {
                ln = fig as vdLine;
                if (ln != null)
                {
                    list_lines.Add(ln);
                }
            }
            foreach (vdLine line in list_lines)
            {
                list_mem_No.Add(GetMemberNo(line));
            }
            list_mem_No.Sort();
            
            for (int i = 0; i < list_mem_No.Count; i++)
            {
                if (i == list_mem_No.Count - 1)
                {
                    txt_member_nos.Text += list_mem_No[i].ToString();
                }
                else
                {
                    txt_member_nos.Text += list_mem_No[i].ToString() + ",";
                }
            }
        }
        public int GetMemberNo(vdLine line)
        {
            //int mNo = -1;
            for (int i = 0; i < astDoc.Members.Count; i++)
            {
                if (astDoc.Members[i].StartNode.Point == line.StartPoint &&
                    astDoc.Members[i].EndNode.Point == line.EndPoint)
                {
                    return astDoc.Members[i].MemberNo;
                }
                else if (astDoc.Members[i].StartNode.Point == line.EndPoint &&
                     astDoc.Members[i].EndNode.Point == line.StartPoint)
                {
                    return astDoc.Members[i].MemberNo;
                }
            }
            return -1;
        }
        public void setArea()
        {
            if (rbtn_dimension.Checked)
            {
                double b, d, ix, iy, iz, area, id, od;
                area = b = d = ix = iy = iz = id = od = 0;

                b = MyStrings.StringToDouble(txt_YD.Text, 0.0);
                d = MyStrings.StringToDouble(txt_ZD.Text, 0.0);

                id = 0.0d;
                od = 0.0d;

                if (b != 0.0 && d != 0.0)
                {
                    area = b * d;
                    ix = (b * d * d * d) / 12;
                    iy = (d * b * b * b) / 12;
                    iz = ix + iy;
                }
                else
                {
                    area = (((Math.PI * od * od) / 4) - ((Math.PI * id * id) / 4));
                    ix = ((Math.PI / 64) * ((od * od * od * od) - (id * id * id * id)));
                    iy = ix;
                    iz = ix + iy;
                }
                //txt_AX.Text = area.ToString("0.000");
                //txt_IX.Text = ix.ToString("0.000");
                //txt_IY.Text = iy.ToString("0.000");
                //txt_IZ.Text = iz.ToString("0.000"); 
                
                //txt_AX.Text = area.ToString("0.000");
                //txt_IX.Text = ix.ToString("0.000");
                //txt_IY.Text = iy.ToString("0.000");
                //txt_IZ.Text = iz.ToString("0.000");
            }
        }


        //Chiranjit [2012 04 29]
        public void ClearAll()
        {
            txt_member_nos.Text = "";
            txt_YD.Text = "";
            txt_ZD.Text = "";
            txt_YB.Text = "";
            txt_ZB.Text = "";
            txt_AX.Text = "";
            txt_IZ.Text = "";
            txt_IY.Text = "";
            txt_IX.Text = "";
            txt_member_nos.Focus();
        }

        private void cmb_range_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_range.SelectedIndex == 0)
            {
                txt_member_nos.Text = "ALL";
                txt_member_nos.Enabled = false;
            }
            else if (cmb_range.SelectedIndex == 1)
            {
                txt_member_nos.Enabled = true;
                txt_member_nos.Text = "";
            }
            else
            {
                txt_member_nos.Enabled = false;
                txt_member_nos.Text = MGC.GetMemberGroup(cmb_range.Text).MemberNosText;
            }
        }

        private void btn_add_data_Click(object sender, EventArgs e)
        {
            string kStr = "";

            if (txt_member_nos.Text == "") return;

            if (cmb_range.SelectedIndex == 0) // ALL
            {
                //kStr = string.Format("1 TO {0} PRISMATIC YD {1} ZD {2} IZ {3} IY {4} IX {5}",
                kStr = string.Format("1 TO {0} PRISMATIC ",
                    iACad.AstraDocument.Members.Count);
            }
            else if (cmb_range.SelectedIndex == 1) // NUMBERS
            {
                string num_text = "";

                num_text = txt_member_nos.Text.Replace(',', ' ');
                kStr = string.Format("{0} PRISMATIC ",
                    num_text);

            }
            else
            {
                string num_text = "";

                num_text = cmb_range.Text.Replace(',', ' ');
                kStr = string.Format("{0} PRISMATIC ",
                    num_text);

            }
            if (txt_YD.Text != "")
            {
                kStr += "YD " + txt_YD.Text;
            }
            if (txt_ZD.Text != "")
            {
                kStr += " ZD " + txt_ZD.Text;
            }
            if (txt_YB.Text != "")
            {
                kStr += " YB " + txt_YB.Text;
            }
            if (txt_ZB.Text != "")
            {
                kStr += " ZB " + txt_ZB.Text;
            }
            if (txt_AX.Text != "")
            {
                kStr += " AX " + txt_AX.Text;
            }
            if (txt_IX.Text != "")
            {
                kStr += " IX " + txt_IX.Text;
            }
            if (txt_IY.Text != "")
            {
                kStr += " IY " + txt_IY.Text;
            } 
            if (txt_IZ.Text != "")
            {
                kStr += " IZ " + txt_IZ.Text;
            }

            if (TRV != null)
            {
                if (ASTRA_Data.Count > 0)
                {
                    Update_Data(kStr);
                    //TRV.SelectedNode.Text = kStr;
                    this.Close();

                    //MemberProperty mp = new MemberProperty();
                    MemberPropertyCollection mp = new MemberPropertyCollection();

                    mp.AddTxt(kStr);



                    for (int i = 0; i < astDoc.Members.Count; i++)
                    {
                        
                    }


                }
                else
                {
                    Update_Data(kStr);
                    //TRV.Nodes.Add(kStr);
                }
            }
            //ASTRA_Data.Add(kStr);
            //ClearAll();

            //this.Close();

           
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.Cancel;
            //iACad.WriteFile("UNIT " + cmb_mass_unit.Text + " " + cmb_length_unit.Text);
            //iACad.Create_Data.Section_Unit = "UNIT " + cmb_mass_unit.Text + " " + cmb_length_unit.Text;
            //iACad.WriteFile("SECTION PROPERTIES");
            //foreach (string kStr in ASTRA_Data)
            //{

            //    if(iACad.Create_Data.Section_Data.Contains(kStr.Trim()))
            //        iACad.Create_Data.Section_Data.Remove(kStr.Trim());
            //    iACad.Create_Data.Section_Data.Add(kStr.Trim());
            //}
            this.Close();
        }

        private void rbtn_dimension_CheckedChanged(object sender, EventArgs e)
        {
            //RadioButton rbtn = sender as RadioButton;
            //switch (rbtn.Name)
            //{
            //    case "rbtn_dimension":
            //        grb_Dimension.Enabled = true;
            //        grb_Calculated_Values.Enabled = false;
            //        break;
            //    case "rbtn_calculated_values":
            //        grb_Dimension.Enabled = false;
            //        grb_Calculated_Values.Enabled = true;
            //        break;
            //}
        }

        private void frmSectionProperties_Load(object sender, EventArgs e)
        {
            //cmb_mass_unit.SelectedIndex = 0;
            //cmb_length_unit.SelectedIndex = 1;
            if (DGV_Joints == null ||
                DGV_Members == null)
            {
                gb1.Visible = false;
            }
            if (MGC != null)
            {
                foreach (var item in MGC)
                {
                    cmb_range.Items.Add(item.GroupName);

                }
            }
            if (ASTRA_Data.Count > 0)
            {
                MyStrings mlist = new MyStrings(MyStrings.RemoveAllSpaces(ASTRA_Data[0]), ' ');

                //kStr
                int r = -1;
                for (int i = 0; i < mlist.Count; i++)
                {
                    if (mlist.StringList[i].StartsWith("PR"))
                    {
                        r = i;
                        //break;
                    }
                    else if (mlist.StringList[i].StartsWith("YD"))
                    {
                        txt_YD.Text = mlist.StringList[i + 1];
                        rbtn_dimension.Checked = true;
                    }
                    else if (mlist.StringList[i].StartsWith("ZD"))
                    {
                        txt_ZD.Text = mlist.StringList[i + 1];
                        rbtn_dimension.Checked = true;
                    }
                    else if (mlist.StringList[i].StartsWith("YB"))
                    {
                        txt_YB.Text = mlist.StringList[i + 1];
                        rbtn_dimension.Checked = true;
                    }
                    else if (mlist.StringList[i].StartsWith("ZB"))
                    {
                        txt_ZB.Text = mlist.StringList[i + 1];
                        rbtn_dimension.Checked = true;
                    }
                    else if (mlist.StringList[i].StartsWith("AX"))
                    {
                        if (r == -1) r = i;
                        txt_AX.Text = mlist.StringList[i + 1];
                        rbtn_calculated_values.Checked = true;
                    }
                    else if (mlist.StringList[i].StartsWith("A"))
                    {
                        if (r == -1) r = i;
                        txt_AX.Text = mlist.StringList[i + 1];
                        rbtn_calculated_values.Checked = true;
                    }
                    else if (mlist.StringList[i].StartsWith("IX"))
                    {
                        txt_IX.Text = mlist.StringList[i + 1];
                        rbtn_calculated_values.Checked = true;
                    }
                    else if (mlist.StringList[i].StartsWith("IY"))
                    {
                        txt_IY.Text = mlist.StringList[i + 1];
                        rbtn_calculated_values.Checked = true;
                    }
                    else if (mlist.StringList[i].StartsWith("IZ"))
                    {
                        txt_IZ.Text = mlist.StringList[i + 1];
                        rbtn_calculated_values.Checked = true;
                    }
                }

                if (r != -1)
                {
                    kStr = mlist.GetString(0, r - 1).Trim();
                    if (cmb_range.Items.Contains(kStr))
                    {
                        cmb_range.SelectedItem = kStr;
                    }
                    else
                    {
                        cmb_range.SelectedIndex = 1;
                        txt_member_nos.Text = kStr;
                    }
                }

                btn_add_data.Text = "Change";
            }
            else
            {

                //cmb_mass_unit.SelectedIndex = (int)iACad.MassUnit;
                //cmb_length_unit.SelectedIndex = (int)iACad.LengthUnit; ;
                rbtn_dimension.Checked = true;

                kStr = iACad.GetSelectedMembersInText();

                rbtn_member_select.Checked = true;

                if (kStr == "ALL")
                {
                    cmb_range.SelectedIndex = 0;

                }
                else
                {
                    cmb_range.SelectedIndex = 1;
                }


                txt_member_nos.Text = kStr;
            }
            Load_Floor_Levels();

        }

        private void txt_YD_TextChanged(object sender, EventArgs e)
        {
            setArea();
        }


        #region Chiranjit [2015 04 01]

        MemberIncidenceCollection mic = new MemberIncidenceCollection();
        JointCoordinateCollection jcc = new JointCoordinateCollection();
        Hashtable BeamTable;
        List<int> AllColumn;


        List<int> selected_beams;
        List<int> selected_columns;
        public void Load_Floor_Levels()
        {
            if (DGV_Joints == null) return;

            BeamTable = new Hashtable();
            AllColumn = new List<int>();
            selected_beams = new List<int>();
            selected_columns = new List<int>();
            
            
            List<double> flvls = new List<double>();

            double d = 0.0;
            //JointCoordinateCollection 
            //MemberIncidenceCollection   
            MemberIncidence mi = new MemberIncidence();
            //MemberIncidenceCollection mic = new MemberIncidenceCollection();
            JointCoordinate jc = new JointCoordinate();
            //JointCoordinateCollection jcc = new JointCoordinateCollection();


            List<int> selected_mems = MyStrings.Get_Array_Intiger(kStr);

            for (int i = 0; i < DGV_Joints.RowCount; i++)
            {
                jc = new JointCoordinate();

                jc.NodeNo = MyStrings.StringToInt(DGV_Joints[0, i].Value.ToString(), 0);
                jc.X = MyStrings.StringToDouble(DGV_Joints[1, i].Value.ToString(), 0.0);
                jc.Y = MyStrings.StringToDouble(DGV_Joints[2, i].Value.ToString(), 0.0);
                jc.Z = MyStrings.StringToDouble(DGV_Joints[3, i].Value.ToString(), 0.0);
                jcc.Add(jc);

            }

            for (int i = 0; i < DGV_Members.RowCount; i++)
            {
                mi = new MemberIncidence();

                mi.MemberNo = MyStrings.StringToInt(DGV_Members[0, i].Value.ToString(), 0);
                //jc.X = MyStrings.StringToDouble(DGV_Members[1, i].Value.ToString(), 0.0);
                mi.StartNode.NodeNo = MyStrings.StringToInt(DGV_Members[2, i].Value.ToString(), 0);
                mi.EndNode.NodeNo = MyStrings.StringToInt(DGV_Members[3, i].Value.ToString(), 0);
                mic.Add(mi);
            }

            mic.CopyJointCoordinates(jcc);
            foreach (var item in mic)
            {
                if (item.StartNode.Y == item.EndNode.Y)
                {
                    if (selected_mems.Contains(item.MemberNo))
                        selected_beams.Add(item.MemberNo);
                    
                    d = item.StartNode.Y;
                    if (!flvls.Contains(d))
                    {
                        flvls.Add(d);
                        //cmb_floor_levels.Items.Add(d.ToString("f3"));
                    }
                }
                else if (item.StartNode.X == item.EndNode.X &&
                    item.StartNode.Z == item.EndNode.Z)
                {
                    if (selected_mems.Contains(item.MemberNo))
                        selected_columns.Add(item.MemberNo);
                
                    AllColumn.Add(item.MemberNo);
                }
            }


            flvls.Sort();
            List<int> mems = new List<int>();
            List<int> all_beams = new List<int>();
            foreach (var lvl in flvls)
            {
                cmb_floor_levels.Items.Add(lvl.ToString("f3"));
                 mems = new List<int>();
                foreach (var item in mic)
                {
                    if (item.StartNode.Y == item.EndNode.Y)
                    {
                        d = item.StartNode.Y;
                        if (lvl == d)
                        {
                            mems.Add(item.MemberNo);
                        }
                    }
                }
                BeamTable.Add(lvl.ToString("f3"), mems);

                all_beams.AddRange(mems.ToArray());
            }
            BeamTable.Add("all", all_beams);

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


            //MyStrings memGrp = new MyStrings(UpdText, ' ');

            string mat_str = kStr.Trim();

            for (int i = 0; i < TRV.Nodes.Count; i++)
            {
                //mems = MyStrings.Get_Array_Intiger(TRV.Nodes[i].Text);

                mmls = new MyStrings(TRV.Nodes[i].Text, ' ');
                if (Updmems.Count > 0)
                {
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

                    if (mems.Count > 0)
                    {
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
                }
                else
                {
                    if (mmls.StringList[0] == mat_str)
                    {
                        TRV.Nodes[i].Text = UpdText;
                        //TRV.Nodes.RemoveAt(i);
                        goto _ss;
                    }

                }
            }
            TRV.Nodes.Add(UpdText);

            _ss :

            iACad.AstraDocument.MemberProperties.AddTxt(UpdText);
            iACad.AstraDocument.MemberProperties.CopyMemberIncidence(iACad.AstraDocument.Members);

        }



        #endregion Chiranjit [2015 04 01]

        private void rbtn_beam_all_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbtn = sender as RadioButton;

            cmb_floor_levels.Enabled = false;
            if (rbtn.Name == rbtn_beam_all.Name)
            {

                List<int> mems = (List<int>)BeamTable["all"];
                if (mems != null)
                    txt_member_nos.Text = MyStrings.Get_Array_Text(mems);

            }
            else if (rbtn.Name == rbtn_beam_floor.Name)
            {
                cmb_floor_levels.Enabled = true;
                cmb_floor_levels.SelectedIndex = -1;
                cmb_floor_levels.SelectedIndex = 0;
            }
            else if (rbtn.Name == rbtn_column_all.Name)
            {
                if (AllColumn != null)
                    txt_member_nos.Text = MyStrings.Get_Array_Text(AllColumn);
            }
            else if (rbtn.Name == rbtn_beam_select.Name)
            {
                if (selected_beams != null)
                    txt_member_nos.Text = MyStrings.Get_Array_Text(selected_beams);
                else
                    txt_member_nos.Text = kStr;
            }
            else if (rbtn.Name == rbtn_column_select.Name)
            {
                if (selected_columns != null)
                    txt_member_nos.Text = MyStrings.Get_Array_Text(selected_columns);
                else
                    txt_member_nos.Text = kStr;
            }
            else if (rbtn.Name == rbtn_member_select.Name)
            {
                txt_member_nos.Text = kStr;
            }

        }

        private void cmb_floor_levels_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<int> mems = (List<int>)BeamTable[cmb_floor_levels.Text];
            if (mems != null)
                txt_member_nos.Text = MyStrings.Get_Array_Text(mems);

        }
    }
}
