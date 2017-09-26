using System;
using System.Collections.Generic;
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
using HEADSNeed.ASTRA.CadToAstra;


namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    public partial class frmMaterialProperties : Form
    {
        vdDocument doc = null;
        ASTRADoc astDoc = null;
        List<int> list_mem_No = null;
        IASTRACAD iACad = null;
        string kStr = "";

        List<string> EMOD { get; set; }
        List<string> DEN { get; set; }
        List<string> PR { get; set; }


        public List<string> ASTRA_Data { get; set; }

        public MaterialProperty Material { get; set; }

        public frmMaterialProperties(IASTRACAD iACAD)
        {
            InitializeComponent();
            list_mem_No = new List<int>();
            iACad = iACAD;
            this.doc = iACAD.Document;
            this.astDoc = iACAD.AstraDocument;
            EMOD = new List<string>();
            DEN = new List<string>();
            PR = new List<string>();

            ASTRA_Data = new List<string>();

            


        }

        private void frmMaterialProperties_Load(object sender, EventArgs e)
        {
            if (Material != null)
            {


                if (Material.MemberNos == "ALL")
                    cmb_range.SelectedIndex = 0;
                else
                    cmb_range.SelectedIndex = 1;


                if (Material.Elastic_Modulus.StartsWith("CON") &&
                     Material.Density.StartsWith("CON") &&
                     Material.Possion_Ratio.StartsWith("CON"))
                    cmb_mat_prop.SelectedIndex = 0;
                else if (Material.Elastic_Modulus.StartsWith("ST") &&
                     Material.Density.StartsWith("ST") &&
                     Material.Possion_Ratio.StartsWith("ST"))
                    cmb_mat_prop.SelectedIndex = 1;

                else
                    cmb_mat_prop.SelectedIndex = 2;

                txt_member_nos.Text = Material.MemberNos;
                txt_e_mod.Text = Material.Elastic_Modulus;
                txt_den_val.Text = Material.Density;
                txt_poisson_val.Text = Material.Possion_Ratio;
                txt_alpha.Text = Material.Alpha;
                btnAddData.Text = "Change";
            }
            else
                SetDefaultValue();
        }
        public void SetDefaultValue()
        {

            cmb_mat_prop.SelectedIndex = 0;
            //cmb_mat_prop.SelectedIndex = 0;

            kStr = iACad.GetSelectedMembersInText();
            if(kStr == "ALL" || kStr == "")
            {
                cmb_range.SelectedIndex = 0;
                kStr = "ALL";
            }
            else
                cmb_range.SelectedIndex = 1;

            txt_member_nos.Text = kStr;

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

        private void cmb_mat_prop_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CONCRETE
            //STEEL
            //USER'S VALUE
            if (cmb_mat_prop.SelectedIndex == 0) // CONCRETE
            {
                //txt_den_val.Text = "0.0024";
                //txt_e_mod.Text = "2.6E5";
                //txt_poisson_val.Text = "0.15";

                txt_den_val.Text = "CONCRETE";
                txt_e_mod.Text = "CONCRETE";
                txt_poisson_val.Text = "CONCRETE";
            }
            else if (cmb_mat_prop.SelectedIndex == 1) // STEEL
            {
                //txt_den_val.Text = "0.0078";
                //txt_e_mod.Text = "2.1E6";
                //txt_poisson_val.Text = "0.12";

                txt_den_val.Text = "STEEL";
                txt_e_mod.Text = "STEEL";
                txt_poisson_val.Text = "STEEL";
            }
            else if (cmb_mat_prop.SelectedIndex == 2)//USER'S VALUE
            {
                txt_e_mod.Text = "";
                txt_den_val.Text = "";
                txt_poisson_val.Text = "";
            }

            if (cmb_mat_prop.SelectedIndex == 2)//USER'S VALUE
            {
                //txt_e_mod.Enabled = true;
                //txt_den_val.Enabled = true;
                //txt_poisson_val.Enabled = true;
                //grb_unit.Enabled = true;
            }
            else
            {
                //txt_e_mod.Enabled = false;
                //txt_den_val.Enabled = false;
                //txt_poisson_val.Enabled = false;
                //grb_unit.Enabled = false;
                //cmbMassUnit.SelectedIndex = 0;
                //cmbLengthUnit.SelectedIndex = 1;
            }
        }

        private void cmb_poisson_ratio_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CONCRETE
            //STEEL
            //USER'S VALUE
            //if (cmb_poisson_ratio.SelectedIndex == 0)
            //{
            //    txt_poisson_val.Text = "0.15";
            //}
            //else if (cmb_poisson_ratio.SelectedIndex == 1)
            //{
            //    txt_poisson_val.Text = "0.12";

            //}
            //else if (cmb_poisson_ratio.SelectedIndex == 2)
            //{
            //    txt_poisson_val.Text = "";
            //}
        }

        private void btnAddData_Click(object sender, EventArgs e)
        {
            //MATERIAL CONSTANTS 
            //E 3150 ALL
            //DEN 0.000383 ALL
            string eMod = "";
            string den = "";
            string poi_rat = "";

            //if (txt_member_nos.Text == "") return;



            Material = new MaterialProperty();
            Material.MemberNos = txt_member_nos.Text;
            Material.Elastic_Modulus = txt_e_mod.Text;
            Material.Density = txt_den_val.Text;
            Material.Possion_Ratio = txt_poisson_val.Text;
            Material.Alpha = txt_alpha.Text;

            //if (cmb_range.SelectedIndex == 0) // ALL
            //{
            //    eMod = string.Format("E {0} ALL", txt_e_mod.Text);
            //    den = string.Format("DEN {0} ALL", txt_den_val.Text);
            //    poi_rat = string.Format("PR {0} ALL", txt_poisson_val.Text);
            //}
            //else if (cmb_range.SelectedIndex == 1) // NUMBERS
            //{
            //    string num_text = "";
            //    num_text = txt_member_nos.Text.Replace(',', ' ');

            //    eMod = string.Format("E {0} {1}",
            //        txt_e_mod.Text,
            //        num_text);

            //    den = string.Format("DEN {0} {1}", txt_den_val.Text, num_text);
            //    poi_rat = string.Format("PR {0} {1}", txt_poisson_val.Text, num_text);
            //}
            ////}

            //eMod = eMod.Trim();
            //den = den.Trim();
            //poi_rat = poi_rat.Trim();


            ASTRA_Data = Material.ASTRA_Data;
            //ASTRA_Data.AddRange(eMod);
            //ASTRA_Data.Add(den);
            //ASTRA_Data.Add(poi_rat);

            ClearAll();

            this.Close();
        }
        private void ClearAll()
        {

            txt_member_nos.Text = "";
            //txt_e_mod.Text = "";
            //txt_den_val.Text = "";
            //txt_poisson_val.Text = "";

            //txt_den_val.Text = "";
            //txt_poisson_val.Text = "";
            txt_member_nos.Focus();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            iACad.Create_Data.Material_Unit = "UNIT " + cmbMassUnit.Text + " " + cmbLengthUnit.Text;
            //iACad.WriteFile("UNIT " + cmbMassUnit.Text + " " + cmbLengthUnit.Text);
            //iACad.WriteFile("MATERIAL CONSTANT");
            foreach (var item in EMOD)
            {
                //iACad.WriteFile(item);
                if(iACad.Create_Data.Material_Data.Contains(item))
                    iACad.Create_Data.Material_Data.Remove(item);
                iACad.Create_Data.Material_Data.Add(item);
            }
            foreach (var item in DEN)
            {
                //iACad.WriteFile(item);       
                if (iACad.Create_Data.Material_Data.Contains(item))
                    iACad.Create_Data.Material_Data.Remove(item);
                iACad.Create_Data.Material_Data.Add(item);
            }
            foreach (var item in PR)
            {
                //iACad.WriteFile(item);              
                if (iACad.Create_Data.Material_Data.Contains(item))
                    iACad.Create_Data.Material_Data.Remove(item);
                iACad.Create_Data.Material_Data.Add(item);
            }
            //iACad.WriteFile(eMod);
            //iACad.WriteFile(den);
            //iACad.WriteFile(poi_rat);
            this.Close();
        }

        private void cmb_range_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_range.SelectedIndex == 0) // All
            {
                txt_member_nos.Text = "ALL";
                txt_member_nos.Enabled = false;
            }
            else if (cmb_range.SelectedIndex == 1) // NUMBERS
            {
                txt_member_nos.Text = "";
                txt_member_nos.Enabled = true;
            }
        }
    }
}
