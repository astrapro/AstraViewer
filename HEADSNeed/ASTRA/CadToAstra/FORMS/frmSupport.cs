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
namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    public partial class frmSupport : Form
    {
        vdDocument doc = null;
        ASTRADoc astDoc = null;
        List<int> list_joint_No = null;
        IASTRACAD iACad = null;
        public string ASTRA_Data { get; set; }

        public TreeView TRV { get; set; }

        public frmSupport(IASTRACAD iACAD)
        {
            InitializeComponent();
            list_joint_No = new List<int>();
            this.iACad = iACAD;

            this.doc = iACad.Document;
            this.astDoc = iACad.AstraDocument;
            //ShowMemberInText();

            ASTRA_Data = "";
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

        public double kValue
        {
            get
            {
                return MyStrings.StringToDouble(txt_kVal.Text, 0.0);
            }
            set
            {
                txt_kVal.Text = value.ToString();
            }
        }

        public string kText
        {
            get
            {
                if (kValue == 0.0) return "";

                if (rbtn_kFX.Checked) return rbtn_kFX.Text + " " + kValue;
                if (rbtn_kFY.Checked) return rbtn_kFY.Text + " " + kValue;
                if (rbtn_kFZ.Checked) return rbtn_kFZ.Text + " " + kValue;
                return "";
            }
        }
        private void btnAddData_Click(object sender, EventArgs e)
        {
            //SUPPORT
            //1 TO 6 FIXED
            string kStr = "";
            kStr = txt_joint_nos.Text.Replace(',', ' ');
            if (cmb_support_type.SelectedIndex == 1)
                kStr = kStr + " " + cmb_support_type.Text;
            else
            {
                kStr = kStr + " " + cmb_support_type.Text;
                if (chk_fx.Checked || chk_fy.Checked || chk_fz.Checked
                    || chk_mx.Checked || chk_my.Checked || chk_mz.Checked)
                {
                    kStr = kStr + " BUT";
                }
                if (chk_fx.Checked)
                {
                    kStr = kStr + " FX";
                }
                if (chk_fy.Checked)
                {
                    kStr = kStr + " FY";
                }
                if (chk_fz.Checked)
                {
                    kStr = kStr + " FZ";
                }
                if (chk_mx.Checked)
                {
                    kStr = kStr + " MX";
                }
                if (chk_my.Checked)
                {
                    kStr = kStr + " MY";
                }
                if (chk_mz.Checked)
                {
                    kStr = kStr + " MZ";
                }
                if (kValue != 0.0)
                {
                    kStr = kStr + " " + kText.ToUpper();

                }

            }

            //ASTRA_Data = kStr;
            if (TRV != null)
            {
                if (ASTRA_Data != "")
                {
                    TRV.SelectedNode.Text = kStr;
                    this.Close();
                }
                else
                    TRV.Nodes.Add(kStr);
            }
            else
                this.Close();

            this.Close();

        }
       
        private void frmSupport_Load(object sender, EventArgs e)
        {
            if (ASTRA_Data != "")
            {
                chk_fx.Checked = false;
                chk_mx.Checked = false;

                chk_fy.Checked = false;
                chk_my.Checked = false;

                chk_fz.Checked = false;
                chk_mz.Checked = false;


                MyStrings mlist = new MyStrings(MyStrings.RemoveAllSpaces(ASTRA_Data), ' ');

                string kStr = "";
                int r = -1;
                for (int i = 0; i < mlist.Count; i++)
                {
                    if (mlist.StringList[i].StartsWith("FIXED"))
                    {
                        r = i;

                        cmb_support_type.SelectedIndex = 0;
                    }
                    else if (mlist.StringList[i].StartsWith("PINNED"))
                    {
                        r = i;
                        cmb_support_type.SelectedIndex = 1;
                        //break;
                    }
                    else if (mlist.StringList[i].StartsWith("FX"))
                    {
                        chk_fx.Checked = true;
                    }
                    else if (mlist.StringList[i].StartsWith("FY"))
                    {
                        chk_fy.Checked = true;
                    }
                    else if (mlist.StringList[i].StartsWith("FZ"))
                    {
                        chk_fz.Checked = true;
                    }
                    else if (mlist.StringList[i].StartsWith("MX"))
                    {
                        chk_mx.Checked = true;
                    }
                    else if (mlist.StringList[i].StartsWith("MY"))
                    {
                        chk_my.Checked = true;
                    }
                    else if (mlist.StringList[i].StartsWith("MZ"))
                    {
                        chk_mz.Checked = true;
                    }
                    //Chiranjit [2014 10 13]
                    else if (mlist.StringList[i].StartsWith("KFX"))
                    {
                        rbtn_kFX.Checked = true;
                        txt_kVal.Text = mlist.StringList[i + 1];
                    }
                    else if (mlist.StringList[i].StartsWith("KFY"))
                    {
                        rbtn_kFY.Checked = true;
                        txt_kVal.Text = mlist.StringList[i + 1];
                    }
                    else if (mlist.StringList[i].StartsWith("KFZ"))
                    {
                        rbtn_kFZ.Checked = true;
                        txt_kVal.Text = mlist.StringList[i + 1];
                    }
                }

                if (r != -1)
                {
                    txt_joint_nos.Text = mlist.GetString(0, r - 1).Trim();
                }
                btnAddData.Text = "Change";
            }
            else
            {
                cmb_support_type.SelectedIndex = 0;
                txt_joint_nos.Text = iACad.GetSelectedJointsInText();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmb_support_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            chk_fx.Enabled = cmb_support_type.SelectedIndex == 0;
            chk_fy.Enabled = cmb_support_type.SelectedIndex == 0;
            chk_fz.Enabled = cmb_support_type.SelectedIndex == 0;

            chk_mx.Enabled = cmb_support_type.SelectedIndex == 0;
            chk_my.Enabled = cmb_support_type.SelectedIndex == 0;
            chk_mz.Enabled = cmb_support_type.SelectedIndex == 0;
        }
    }
}
