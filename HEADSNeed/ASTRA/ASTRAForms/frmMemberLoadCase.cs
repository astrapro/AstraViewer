using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

using HEADSNeed.ASTRA.ASTRAClasses;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdFigures;
using VectorDraw.Geometry;




namespace HEADSNeed.ASTRA.ASTRAForms
{

    public partial class frmMemberLoadCase : Form
    {
        vdDocument Maindoc = null;
        vdDocument MemberDoc = null;
        vdDocument MemberLoadDoc = null;
        vdDocument MemberJointLoadDoc = null;


        string formText = "";
        Form frmMain = null;
        Form frmMember = null;
        Form frmMemberLoad = null;
        Form frmMemberJointLoad = null;


        ASTRADoc astDoc = null;
        vdLine memberLine = null;
        vdCircle cirMember = null;
        public frmMemberLoadCase(vdDocument thisDoc, vdDocument memberDoc, vdDocument memberLoadDoc,
            vdDocument memberJointLoad, ASTRADoc thisAstDoc,
            Form thisForm, Form memberForm, Form memberLoadForm, Form memberJointLoadForm)
        {

            InitializeComponent();
            Maindoc = thisDoc;
            MemberDoc = memberDoc;
            MemberLoadDoc = memberLoadDoc;
            MemberJointLoadDoc = memberJointLoad;


            astDoc = thisAstDoc;
            SetMember();

            memberLine = new vdLine();
            cirMember = new vdCircle();
            cirMember.SetUnRegisterDocument(Maindoc);
            cirMember.setDocumentDefaults();

            memberLine.SetUnRegisterDocument(Maindoc);
            memberLine.setDocumentDefaults();
            Maindoc.ActiveLayOut.Entities.AddItem(memberLine);
            Maindoc.ActiveLayOut.Entities.AddItem(cirMember);

            formText = thisForm.Text;

            frmMain = thisForm;
            frmMember = memberForm;
            frmMemberLoad = memberLoadForm;
            frmMemberJointLoad = memberJointLoadForm;
        }
        public frmMemberLoadCase(vdDocument thisDoc, vdDocument memberDoc, vdDocument memberLoadDoc, vdDocument memberJointLoad, ASTRADoc thisAstDoc)
        {

            InitializeComponent();
            Maindoc = thisDoc;
            MemberDoc = memberDoc;
            MemberLoadDoc = memberLoadDoc;
            MemberJointLoadDoc = memberJointLoad;


            astDoc = thisAstDoc;
            SetMember();

            memberLine = new vdLine();
            cirMember = new vdCircle();
            cirMember.SetUnRegisterDocument(Maindoc);
            cirMember.setDocumentDefaults();

            memberLine.SetUnRegisterDocument(Maindoc);
            memberLine.setDocumentDefaults();
            Maindoc.ActiveLayOut.Entities.AddItem(memberLine);
            Maindoc.ActiveLayOut.Entities.AddItem(cirMember);
        }
        public frmMemberLoadCase(vdDocument thisDoc, ASTRADoc thisAstDoc)
        {
            InitializeComponent();
            Maindoc = thisDoc;
            astDoc = thisAstDoc;
            SetMember();

            memberLine = new vdLine();
            cirMember = new vdCircle();
            cirMember.SetUnRegisterDocument(Maindoc);
            cirMember.setDocumentDefaults();

            memberLine.SetUnRegisterDocument(Maindoc);
            memberLine.setDocumentDefaults();
            Maindoc.ActiveLayOut.Entities.AddItem(memberLine);
            Maindoc.ActiveLayOut.Entities.AddItem(cirMember);
        }

        public int MemberNo
        {
            get
            {
                return int.Parse(cmbMembers.Text);
            }
        }
        public int LoadCase
        {
            get
            {
                return int.Parse(cmbLoadCase.Text);
            }
        }

        public void ShowMember(vdLine ln)
        {
            for (int i = 0; i < astDoc.Members.Count; i++)
            {
                if (ln.StartPoint == astDoc.Members[i].StartNode.Point &&
                    ln.EndPoint == astDoc.Members[i].EndNode.Point)
                {
                    astDoc.Members.ShowMember(i, Maindoc);
                    cmbMembers.SelectedIndex = i;
                    return;
                }
            }
        }

        private void frmMemberLoadCase_Load(object sender, EventArgs e)
        {
            frmMain.Text = formText;
            frmMain.WindowState = FormWindowState.Normal;
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(Maindoc);
            cmbMembers.SelectedIndex = 0;
            cmbLoadCase.SelectedIndex = 0;
            ShowMember();
            Set_Loadings();

        }

        private void Set_Loadings()
        {

            TreeNode tn = null;
            TreeNode tjn = null;
            tv_loads.Nodes.Clear();
            foreach (var item in astDoc.LoadDefines)
            {
                tv_loads.Nodes.Add("LOAD " + item.LoadCase + " : " + item.LoadTitle);

                tn = tv_loads.Nodes[tv_loads.Nodes.Count - 1];

                if (item.MemberLoadList.Count > 0)
                {
                    tn.Nodes.Add("MEMBER LOAD");
                    tn = tn.Nodes[0];
                    foreach (var item1 in item.MemberLoadList)
                    {
                        tn.Nodes.Add(item1 + " " + astDoc.Base_MassUnit + "/" + astDoc.Base_LengthUnit);
                    }
                }
                if (item.JointLoadList.Count > 0)
                {
                    if (item.MemberLoadList.Count > 0)
                    {
                        tn = tn.Parent;
                    }
                    tn.Nodes.Add("JOINT LOAD");
                    if (tn.Nodes.Count > 1)
                        tn = tn.Nodes[1];
                    else
                        tn = tn.Nodes[0];
                    foreach (var item1 in item.JointLoadList)
                    {
                        tn.Nodes.Add(item1 + " " + astDoc.Base_MassUnit + "," + astDoc.Base_LengthUnit);
                    }
                }
                if (item.CominationLoadList.Count > 0)
                {
                    item.Set_Combination();
                    tn.Nodes.Add("COMBINATION");
                    if (tn.Nodes.Count > 0)
                        tn = tn.Nodes[tn.Nodes.Count - 1];
                    foreach (var item1 in item.Combinations)
                    {
                        tn.Nodes.Add(item1.ToString());
                    }
                }
            }
            //End
        }

        public void SetMember()
        {
            cmbMembers.Items.Clear();
            for (int i = 0; i < astDoc.Members.Count; i++)
            {
                cmbMembers.Items.Add(astDoc.Members[i].MemberNo);
            }

            cmbLoadCase.Items.Clear();
            foreach (var item in astDoc.LoadDefines)
            {
                cmbLoadCase.Items.Add(item.LoadCase);
                
            }

            //for (int i = 0; i < astDoc.MemberLoads.Count; i++)
            //{
            //    if (!cmbLoadCase.Items.Contains(astDoc.MemberLoads[i].LoadCase))
            //    {
            //        cmbLoadCase.Items.Add(astDoc.MemberLoads[i].LoadCase);
            //    }
            //}
            //for (int i = 0; i < astDoc.JointLoads.Count; i++)
            //{
            //    if (!cmbLoadCase.Items.Contains(astDoc.JointLoads[i].LoadCase))
            //    {
            //        cmbLoadCase.Items.Add(astDoc.JointLoads[i].LoadCase);
            //    }
            //}
            //if (cmbLoadCase.Items.Count == 0)
            //    cmbLoadCase.Items.Add(1);
        }

        private void btnMemberForword_Click(object sender, EventArgs e)
        {
            try
            {
                cmbMembers.SelectedIndex += 1;

                BeamAnalysisCollection beamCol = new BeamAnalysisCollection();

                beamCol.ReadFromFile(@"D:\SOFTWARE TESTING\ASTRA\Astra Examples\[01] Static Analysis of Frame [Beam]\Data\test.rep");
                //ShowMember(cmbMembers.Text);
            }
            catch (Exception exx)
            {
            }
        }

        private void btnMemberBack_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbMembers.SelectedIndex == 0) return;

                cmbMembers.SelectedIndex -= 1;
                //ShowMember(cmbMembers.Text);
            }
            catch (Exception exx)
            {
            }
        }

        private void btnLoadCaseForword_Click(object sender, EventArgs e)
        {
            try
            {
                //if(cmbLoadCase.SelectedIndex >= 0)
                cmbLoadCase.SelectedIndex += 1;
                //ShowMember(cmbMembers.Text);
            }
            catch (Exception exx)
            {
            }
        }

        private void btnLoadCaseBack_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbLoadCase.SelectedIndex == 0) return;

                cmbLoadCase.SelectedIndex -= 1;
                //ShowMember(cmbMembers.Text);
            }
            catch (Exception exx)
            {
            }

        }

        public void ShowMemberLoadCase()
        {
            int MemNo = MemberNo;
            int mIndx = astDoc.Members.IndexOf(MemNo);

            astDoc.DrawMemberDetails(Maindoc, LoadCase);

            if (mIndx != -1)
            {
                if (Maindoc.ActiveLayOut.Entities.FindItem(cirMember))
                {
                    Maindoc.ActiveLayOut.Entities.RemoveItem(cirMember);
                }

                //cirMember = new vdCircle();
                //cirMember.SetUnRegisterDocument(Maindoc);
                //cirMember.setDocumentDefaults();
                //cirMember.PenColor = new vdColor(Color.Orange);
                //cirMember.Center = astDoc.Members[mIndx].StartNode.Point;
                //cirMember.Radius = 0.18d;
                //cirMember.Thickness = gPoint.Distance3D(astDoc.Members[mIndx].StartNode.Point,
                //    astDoc.Members[mIndx].EndNode.Point);
                //cirMember.ExtrusionVector = Vector.CreateExtrusion(astDoc.Members[mIndx].StartNode.Point,
                //    astDoc.Members[mIndx].EndNode.Point);
                //Maindoc.ActiveLayOut.Entities.AddItem(cirMember);



                MemberDoc.ActiveLayOut.Entities.RemoveAll();
                MemberLoadDoc.ActiveLayOut.Entities.RemoveAll();
                MemberJointLoadDoc.ActiveLayOut.Entities.RemoveAll();

                astDoc.Members.DrawMember(astDoc.Members[mIndx], MemberDoc);
                astDoc.Members.DrawMember(astDoc.Members[mIndx], MemberLoadDoc);
                astDoc.Members.DrawMember(astDoc.Members[mIndx], MemberJointLoadDoc);


                mIndx = astDoc.MemberLoads.IndexOf(MemberNo, LoadCase);
                if (mIndx != -1)
                {
                    astDoc.MemberLoads.DrawMemberLoad(MemberLoadDoc, astDoc.MemberLoads[mIndx]);
                }

                mIndx = astDoc.JointLoads.IndexOf(astDoc.Members[astDoc.Members.IndexOf(MemberNo)].StartNode.NodeNo, LoadCase);
                if (mIndx != -1)
                {
                    astDoc.JointLoads.DrawJointLoads(MemberJointLoadDoc, astDoc.JointLoads[mIndx]);
                }

                mIndx = astDoc.JointLoads.IndexOf(astDoc.Members[astDoc.Members.IndexOf(MemberNo)].EndNode.NodeNo, LoadCase);
                if (mIndx != -1)
                {
                    astDoc.JointLoads.DrawJointLoads(MemberJointLoadDoc, astDoc.JointLoads[mIndx]);
                }
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(MemberDoc);
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(MemberLoadDoc);
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(MemberJointLoadDoc);

                Maindoc.Redraw(true);
            }
        }

        public void ShowMember_1()
        {
            int mIndx = astDoc.Members.IndexOf(MemberNo);

            try
            {
                astDoc.Members.ShowMember(mIndx, Maindoc, 0.03d);
            }
            catch (Exception exx)
            {
            }
            #region   EXTRA

            if (mIndx != -1)
            {

                try
                {
                    MemberDoc.ActiveLayOut.Entities.RemoveAll();
                    MemberLoadDoc.ActiveLayOut.Entities.RemoveAll();
                    MemberJointLoadDoc.ActiveLayOut.Entities.RemoveAll();

                    astDoc.MemberLoads.ShowMember(MemberDoc, astDoc.Members[mIndx],"MEMBER DIAGRAM");
                    astDoc.MemberLoads.ShowMember(MemberLoadDoc, astDoc.Members[mIndx], "    MEMBER LOAD");
                    astDoc.MemberLoads.ShowMember(MemberJointLoadDoc, astDoc.Members[mIndx], "    JOINT LOADS");

                    mIndx = astDoc.MemberLoads.IndexOf(MemberNo, LoadCase);
                    if (mIndx != -1)
                    {
                        astDoc.MemberLoads.ShowMember(MemberLoadDoc, astDoc.MemberLoads[mIndx]);
                    }
                    astDoc.MemberLoads.ShowMemberJointLoad(MemberJointLoadDoc, MemberNo, astDoc,LoadCase);
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(MemberDoc);
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(MemberLoadDoc);
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(MemberJointLoadDoc);

                    Maindoc.Redraw(true);
                }
                catch (Exception ex)
                { }
            }
            #endregion
        }

        public void ShowMember()
        {
            int mIndx = astDoc.Members.IndexOf(MemberNo);

            if (mIndx != -1)
            {
               ShowMember_1();
            }
        }
        public void ShowMember_2()
        {
            int mIndx = astDoc.Members.IndexOf(MemberNo);

            try
            {
                astDoc.Members.ShowMember(mIndx, Maindoc, 0.03d);
            }
            catch (Exception exx)
            {
            }
            #region   EXTRA

            if (mIndx != -1)
            {
                //if (Maindoc.ActiveLayOut.Entities.FindItem(cirMember))
                //{
                //    Maindoc.ActiveLayOut.Entities.RemoveItem(cirMember);
                //}

                //cirMember = new vdCircle();
                //cirMember.SetUnRegisterDocument(Maindoc);
                //cirMember.setDocumentDefaults();
                //cirMember.PenColor = new vdColor(Color.Orange);
                //cirMember.Center = astDoc.Members[mIndx].StartNode.Point;
                //cirMember.Radius = 0.3d;
                //cirMember.Thickness = gPoint.Distance3D(astDoc.Members[mIndx].StartNode.Point,
                //    astDoc.Members[mIndx].EndNode.Point);

                //cirMember.ExtrusionVector = Vector.CreateExtrusion(astDoc.Members[mIndx].StartNode.Point,
                //    astDoc.Members[mIndx].EndNode.Point);


                //Maindoc.ActiveLayOut.Entities.AddItem(cirMember);


                try
                {
                    MemberDoc.ActiveLayOut.Entities.RemoveAll();
                    MemberLoadDoc.ActiveLayOut.Entities.RemoveAll();
                    MemberJointLoadDoc.ActiveLayOut.Entities.RemoveAll();

                    astDoc.Members.DrawMember(astDoc.Members[mIndx], MemberDoc);
                    astDoc.Members.DrawMember(astDoc.Members[mIndx], MemberLoadDoc);
                    astDoc.Members.DrawMember(astDoc.Members[mIndx], MemberJointLoadDoc);


                    mIndx = astDoc.MemberLoads.IndexOf(MemberNo, LoadCase);
                    if (mIndx != -1)
                    {
                        astDoc.MemberLoads.DrawMemberLoad(MemberLoadDoc, astDoc.MemberLoads[mIndx]);
                    }

                    mIndx = astDoc.JointLoads.IndexOf(astDoc.Members[astDoc.Members.IndexOf(MemberNo)].StartNode.NodeNo, LoadCase);
                    if (mIndx != -1)
                    {
                        astDoc.JointLoads.DrawJointLoads(MemberJointLoadDoc, astDoc.JointLoads[mIndx]);
                    }

                    mIndx = astDoc.JointLoads.IndexOf(astDoc.Members[astDoc.Members.IndexOf(MemberNo)].EndNode.NodeNo, LoadCase);
                    if (mIndx != -1)
                    {
                        astDoc.JointLoads.DrawJointLoads(MemberJointLoadDoc, astDoc.JointLoads[mIndx]);
                    }
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(MemberDoc);
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(MemberLoadDoc);
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(MemberJointLoadDoc);

                    Maindoc.Redraw(true);
                }
                catch (Exception ex)
                { }
            }
            #endregion
        }

        public void CloseAllWindow()
        {
            try
            {
                string delete_file = Path.Combine(Application.StartupPath, "test.vdml");

                MemberDoc.SaveAs(delete_file);
                MemberLoadDoc.SaveAs(delete_file);
                MemberJointLoadDoc.SaveAs(delete_file);
                
                frmMember.Close();
                frmMemberLoad.Close();
                frmMemberJointLoad.Close();
                frmMain.WindowState = FormWindowState.Maximized;
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(Maindoc);

                File.Delete(delete_file);
            }
            catch (Exception ex)
            {
            }
        }



        private void cmbLoadCase_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (!astDoc.IsMovingLoad)
            //ShowMemberLoadCase();
            //ShowGlobalLocal();
            ShowMember();
        }

        private void cmbMembers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowMember();
        }

        private void frmMemberLoadCase_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Maindoc.ActiveLayOut.Entities.FindItem(cirMember))
            {
                Maindoc.ActiveLayOut.Entities.RemoveItem(cirMember);
                Maindoc.Redraw(true);
            }
            //frmMain.WindowState = FormWindowState.Maximized;
        }
        private void ShowGlobalLocal()
        {
            try
            {
                Maindoc.Layers.FindName("MemberLoadGlobal").Frozen = !chkGlobal.Checked;
                Maindoc.Layers.FindName("MemberLoadLocal").Frozen = !chkLocal.Checked;
            }
            catch (Exception exx) { }
            {
            }

            try
            {

                Maindoc.Layers.FindName("ConcentrateLocal").Frozen = !chkLocal.Checked;
                Maindoc.Layers.FindName("ConcentrateGlobal").Frozen = !chkGlobal.Checked;
            }
            catch (Exception ex2) { }
            {
            }
            try
            {
                Maindoc.Layers.FindName("JointLoad").Frozen = !chkJoint.Checked;
            }
            catch (Exception exx1) { }
            {
            }
            Maindoc.Redraw(true);
        }
        private void chkGlobal_CheckedChanged(object sender, EventArgs e)
        {
            ShowGlobalLocal();
        }

        private void chkLocal_CheckedChanged(object sender, EventArgs e)
        {
            ShowGlobalLocal();
        }

        private void cmbLoadCase_Click(object sender, EventArgs e)
        {
            ShowMember();
        }

        private void cmbMembers_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int iVal = 1;
                try
                {
                    if (int.TryParse(cmbMembers.Text, out iVal))
                    {
                        cmbMembers.Text = iVal.ToString();
                    }
                    else
                        cmbMembers.Text = "1";

                    if (int.TryParse(cmbLoadCase.Text, out iVal))
                    {
                        cmbLoadCase.Text = iVal.ToString();
                    }
                    else
                        cmbLoadCase.Text = "0";

                }
                catch (Exception ex) { }

                ShowMember();
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string kStr = "";



            TreeNode tn = tv_loads.SelectedNode;

            bool IsJointLoad = false;


            if (tn.Nodes.Count == 0)
            {
                IsJointLoad = (tn.Parent.Text.StartsWith("JOINT"));
                kStr = tn.Text;
            }



            astDoc.JointLoads.Delete_ASTRAArrowLine(Maindoc);
            astDoc.MemberLoads.Delete_ASTRAMemberLoad(Maindoc);

            if (tn.Parent != null)
            {
                tn = tn.Parent;
                if (tv_loads.SelectedNode.Text.StartsWith("JOINT"))
                {
                    astDoc.JointLoads.DrawJointLoads(Maindoc, tn.Index + 1); goto _100;
                    cmbLoadCase.SelectedIndex = tn.Index;
                }
                else if (tv_loads.SelectedNode.Text.StartsWith("MEMBER"))
                {
                    astDoc.MemberLoads.DrawMemberLoad(Maindoc, tn.Index + 1); goto _100;
                    cmbLoadCase.SelectedIndex = tn.Index;
                }
                if (tn.Text.StartsWith("COMB"))
                {
                    MyStrings mlist = new MyStrings(kStr, ' ');
                    astDoc.MemberLoads.DrawMemberLoad(Maindoc, mlist.GetInt(3)); 
                    astDoc.JointLoads.DrawJointLoads(Maindoc, mlist.GetInt(3)); goto _100;
                    //astDoc.MemberLoads.DrawC(Maindoc, tn.Index + 1); goto _100;
                    cmbLoadCase.SelectedIndex = tn.Index;
                }
            }
            else
            {

                astDoc.JointLoads.DrawJointLoads(Maindoc, tn.Index + 1);
                astDoc.MemberLoads.DrawMemberLoad(Maindoc, tn.Index + 1);
                goto _100;
            }

            if (tn.Parent != null)
                tn = tn.Parent;
            if (tn.Parent != null)
                tn = tn.Parent;
            if (tn.Parent != null)
                tn = tn.Parent;
            if (tn.Parent != null)
                tn = tn.Parent;




            int MemNo = MemberNo;
            int mIndx = astDoc.Members.IndexOf(MemNo);

           
            astDoc.Supports.DrawSupport(Maindoc);
            if (IsJointLoad)
                astDoc.JointLoads.DrawJointLoads(Maindoc, tn.Index + 1, kStr);
            else
                astDoc.MemberLoads.DrawMemberLoad(Maindoc, tn.Index + 1, kStr);
            
            _100 :
            if(tn.Index < cmbLoadCase.Items.Count)
                cmbLoadCase.SelectedIndex = tn.Index;
            Maindoc.Redraw(true);

        }
    }
}
