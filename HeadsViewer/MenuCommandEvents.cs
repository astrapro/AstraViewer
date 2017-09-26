using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using VectorDraw.Professional;
using VectorDraw.Generics;
using VectorDraw.Professional.vdCollections;
using VectorDraw.Professional.vdObjects;
using VectorDraw.Professional.vdPrimaries;
using VectorDraw.Professional.vdFigures;
using HeadsUtils.Interfaces;
using System.Reflection;
using HEADSNeed.DisNet.DisNetClasses;
using HEADSNeed.ASTRA.ASTRAForms;
using HEADSNeed.ASTRA.ASTRAClasses;
using HEADSNeed.ASTRA.ASTRAClasses.SlabDesign;
using HEADSNeed.ASTRA.ASTRAClasses.CodeSnap;
using HEADSNeed.ASTRA.CadToAstra;
using HEADSNeed.ASTRA.CadToAstra.FORMS;
using HEADSNeed.ASTRA;

using HeadsViewer.AstraDrawings;
using MovingLoadAnalysis;
namespace HeadsViewer
{
    public partial class MainForm   : IASTRA_Control
    {
        Hashtable hashHeadsCommands = null;
        Hashtable hashAstraCommands = null;
        delegate void HeadsCommnds();

        System.Windows.Forms.Form frmHalignValign = null;
        System.Windows.Forms.Form frmLoadDef = null;
        System.Windows.Forms.Form frmFind = null;

        //frmMemberLoadCase frmMemLoad = null;

        // Chiranjit
        string LastStructureFormName = "";
        int StructureFormCount = 0;

        vdDocument memberDoc = null;
        vdDocument memberLoadDoc = null;
        vdDocument memberJointLoadDoc = null;
        vdDocument mainDoc = null;

        vdDocument postProcess1 = null;
        vdDocument postProcess2 = null;
        vdDocument postProcess3 = null;

        Childform frmPostProcess1 = null;
        Childform frmPostProcess2 = null;
        Childform frmPostProcess3 = null;


        //
        //Chiranjit 30/09/2009
        frmMemberGrid memGrid = null;
        frmNodeGrid nodeGrid = null;
        frmMemberLoadCase frmMemLoadCase = null;
        frmPostProcess frmPostProc = null;

        //Chiranjit 02/12/2009
        frmASTRAMovingLoadDeflections frmASTRA_MovingLoadDef = null;
        frmASTRALoadDeflections frmASTRA_LoadDef = null;

        // Chiranjit 2010 02 19
        string file_path, system_path;



        private void CloseASTRADocs()
        {
            try
            {
                string sPth = Path.Combine(Application.StartupPath, "test.vdml");
                memberDoc.SaveAs(sPth);
                memberLoadDoc.SaveAs(sPth);
                memberJointLoadDoc.SaveAs(sPth);
                postProcess1.SaveAs(sPth);
                postProcess2.SaveAs(sPth);
                postProcess3.SaveAs(sPth);
            }
            catch (Exception edx)
            {
            }
            try
            {
                frm.Close();
            }
            catch (Exception ex) { }
            try
            {
                frmASTRA_LoadDef.Close();
            }
            catch (Exception ex) { }
            try
            {
                frmASTRA_MovingLoadDef.Close();
            }
            catch (Exception ex) { }
        }
        public vdDocument VDoc
        {
            get
            {
                Childform form = this.ActiveMdiChild as Childform;
                return form.vdScrollableControl1.BaseControl.ActiveDocument;
            }
        }
        private void RegisterHeadsCommands()
        {
            hashHeadsCommands = new Hashtable();
            hashAstraCommands = new Hashtable();
            //Chiranjit 29/07/2010
            this.hashHeadsCommands.Add("Align", new HeadsCommnds(OnAlignment));

            this.hashHeadsCommands.Add("HelpAbout", new HeadsCommnds(OnHelpAbout));
            this.hashHeadsCommands.Add("Close", new HeadsCommnds(OnCloseCurrentDoc));

            this.hashHeadsCommands.Add("WorkingDir", new HeadsCommnds(OnWorkingDir));
            //Draw String
            this.hashHeadsCommands.Add("DrawString", new HeadsCommnds(OnDrawString));
            //Halign
            this.hashHeadsCommands.Add("DesignNewHIPMethod", new HeadsCommnds(OnDesignNewHalignHIPMethod));
            this.hashHeadsCommands.Add("DesignOpenHIPMethod", new HeadsCommnds(OnDesignOpenHalignHIPMethod));
            this.hashHeadsCommands.Add("DesignNewElementMethod", new HeadsCommnds(OnDesignNewHalignElementMethod));
            this.hashHeadsCommands.Add("DesignOpenElementMethod", new HeadsCommnds(OnDesignOpenHalignElementMethod));
            //Valign
            this.hashHeadsCommands.Add("DesignNewValignment", new HeadsCommnds(OnDesignNewValignment));
            this.hashHeadsCommands.Add("DesignOpenValignment", new HeadsCommnds(OnDesignOpenValignment));
            //LSec
            this.hashHeadsCommands.Add("DesignLSecDrawLSection", new HeadsCommnds(OnDesignLSecDrawLSection));
            this.hashHeadsCommands.Add("DesignLSecGridOn", new HeadsCommnds(OnDesignLSecGridOn));
            this.hashHeadsCommands.Add("DesignLSecGridOff", new HeadsCommnds(OnDesignLSecGridOff));
            this.hashHeadsCommands.Add("DesignLSecnVerticalDetailsOn", new HeadsCommnds(OnDesignLSecnVerticalDetailsOn));
            this.hashHeadsCommands.Add("DesignLSecnVerticalDetailsOff", new HeadsCommnds(OnDesignDetailsOff));
            //Configurations
            this.hashHeadsCommands.Add("DesignConfigurations", new HeadsCommnds(OnDesignConfigParam));
            //Chainage
            this.hashHeadsCommands.Add("DesignChainageOn", new HeadsCommnds(OnDesignChainageOn));
            this.hashHeadsCommands.Add("DesignChainageOff", new HeadsCommnds(OnDesignChainageOff));
            //Details
            this.hashHeadsCommands.Add("DesignDetailsOn", new HeadsCommnds(OnDesignDetailsOn));
            this.hashHeadsCommands.Add("DesignDetailsOff", new HeadsCommnds(OnDesignDetailsOff));
            //Coordinate
            this.hashHeadsCommands.Add("DesignCoOrdOn", new HeadsCommnds(OnDesignCoordinatesOn));
            this.hashHeadsCommands.Add("DesignCoOrdOff", new HeadsCommnds(OnDesignCoordinatesOff));
            //Boundary
            this.hashHeadsCommands.Add("DesignBoundary", new HeadsCommnds(OnDesignBoundary));
            //Modelling
            this.hashHeadsCommands.Add("ModellingHalignment", new HeadsCommnds(OnModellingHalignment));
            this.hashHeadsCommands.Add("ModellingValignment", new HeadsCommnds(OnModellingValignment));
            this.hashHeadsCommands.Add("ModellingOffsets", new HeadsCommnds(OnModellingOffsets));
            //Layout
            this.hashHeadsCommands.Add("DesignLayoutApply", new HeadsCommnds(OnDesignLayoutApply));
            this.hashHeadsCommands.Add("DesignLayoutAccept", new HeadsCommnds(OnDesignLayoutAccept));
            //MakeString
            this.hashHeadsCommands.Add("DesignMakeString", new HeadsCommnds(OnDesignMakeString));
            //Offset
            this.hashHeadsCommands.Add("DesignOffsets", new HeadsCommnds(OnDesignOffsets));
            //Help
            this.hashHeadsCommands.Add("HelpContent", new HeadsCommnds(OnHelpContent));
            //File Export
            this.hashHeadsCommands.Add("DRGExport", new HeadsCommnds(OnDRGExport));
            this.hashHeadsCommands.Add("DRGImport", new HeadsCommnds(OnDRGImport));
            //Load Deflection
            this.hashHeadsCommands.Add("LoadDeflection", new HeadsCommnds(OnLoadDeflection));
            this.hashHeadsCommands.Add("ViewMovingLoad", new HeadsCommnds(OnViewMovingLoad));



            //Chiranjit [2017 07 04]  Find Text
            this.hashHeadsCommands.Add("Find", new HeadsCommnds(OnFind));

        }

        private bool ValidateWorkingDir()
        {
            bool bSuccess = false;
            if (this.AppDataPath.Trim() == ""
                || System.IO.Directory.Exists(this.AppDataPath) == false)
            {
                IWorkingDirSelector wrkdirselector = HeadsFunctions.CreateWkDirDialog(this);
                wrkdirselector.DialogInstance.Owner = this;

                if (wrkdirselector.DialogInstance.ShowDialog() == DialogResult.OK)
                {
                    this.AppDataPath = wrkdirselector.WorkingFolderPath;
                    bSuccess = true;
                }
            }
            else
            {
                bSuccess = true;
            }

            return bSuccess;
        }

        string fName = "";
        private HEADSNeed.ASTRA.ASTRAClasses.ASTRADoc astDoc = new HEADSNeed.ASTRA.ASTRAClasses.ASTRADoc();
        int membNo = 0;
        private void tmrZoomMember_Tick(object sender, EventArgs e)
        {
            try
            {
                return;
                Childform form1 = this.ActiveMdiChild as Childform;
                if (form1 != null)
                {
                    VectorDraw.Professional.vdObjects.vdDocument doc = form1.vdScrollableControl1.BaseControl.ActiveDocument;
                    VectorDraw.Geometry.gPoint midPoint = new VectorDraw.Geometry.gPoint();
                    VectorDraw.Geometry.gPoint fCorner = new VectorDraw.Geometry.gPoint();
                    VectorDraw.Geometry.gPoint otherCorner = new VectorDraw.Geometry.gPoint();

                    //midPoint = (astDoc.Members[membNo].StartNode.Point + astDoc.Members[membNo].EndNode.Point) / 2;
                    midPoint = (astDoc.Members[membNo].StartNode.Point + astDoc.Members[membNo].EndNode.Point) / 2.0;
                    membNo++;
                    fCorner.x = midPoint.x - 3.0d;
                    fCorner.y = midPoint.y + 3.0d;
                    fCorner.z = midPoint.z;
                    otherCorner.x = midPoint.x + 3.0d;
                    otherCorner.y = midPoint.y - 3.0d;
                    otherCorner.z = midPoint.z;
                    //fCorner.x = fCorner.x - 3.0d;
                    //fCorner.y = fCorner.y + 3.0d;
                    //otherCorner.x = otherCorner.x + 3.0d;
                    //otherCorner.y = otherCorner.y - 3.0d;

                    doc.CommandAction.Zoom("W", fCorner, otherCorner);
                    //doc.CommandAction.Zoom("E", fCorner, otherCorner);
                    if (membNo >= astDoc.Members.Count)
                        membNo = 0;

                    doc.Redraw(true);
                }
            }
            catch (Exception exx)
            {
                membNo = 0;
            }
        }

        /// <summary>
        /// This event is called when a command is going to be executed either from the menu(the command string is sent to the commandline) either 
        /// from a toolbar button(the command string is sent to the commandline) either directly from the commandline(type a command). For commands
        /// that are not present here the default vectordraw implementation is executed and this is controled with the success ref parameter.
        /// </summary>
        /// <param name="commandname"></param>
        /// <param name="isDefaultImplemented"></param>
        /// <param name="success"></param>
        /// 

        void CommandExecute(string commandname, bool isDefaultImplemented, ref bool success)
        {
            try
            {
                if (string.Compare(commandname, "open", true) == 0)
                {
                    success = true;
                    createForm();
                    Childform form = this.ActiveMdiChild as Childform;
                    VectorDraw.Professional.vdObjects.vdDocument doc = form.vdScrollableControl1.BaseControl.ActiveDocument;
                    object ret = doc.GetOpenFileNameDlg(0, "", 0);
                    if (ret == null)
                    {
                        form.Close();
                        return;
                    }
                    string fname = (string)ret;
                    this.AppDataPath = System.IO.Path.GetDirectoryName(fname);
                    bool successopen = doc.Open(fname);
                    if (!successopen)
                    {
                        System.Windows.Forms.MessageBox.Show("Error openning " + fname, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        form.Close();
                        return;
                    }
                    else
                    {
                        AddToRecentFiles(doc.FileName);
                    }

                    doc.Redraw(false);
                    //Updates the window list
                    ActivateMdiChild(null);
                    ActivateMdiChild(this.ActiveMdiChild);
                    iACAD.IsOpenFile = false;

                }
                else if (string.Compare(commandname, "new", true) == 0)
                {
                    OnFileNew();
                    success = true;

                    //createForm();
                    //success = true;

                    //Childform form1 = this.ActiveMdiChild as Childform;
                    //if (form1 != null)
                    //{
                    //    VectorDraw.Professional.vdObjects.vdDocument doc = form1.vdScrollableControl1.BaseControl.ActiveDocument;

                    //    Propertiesform.Initialize(doc, GetResourcesPath(), this.panel2);
                    //    Propertiesform.LoadProps(null, doc, true);
                    //    FixSide(Propertiesform.mDocumentProperties.mSide);
                    //}

                    //
                    try
                    {
                        frmMemLoadCase.Close();
                    }
                    catch (Exception exx) { }
                    try
                    {
                        memGrid.Close();
                        nodeGrid.Close();
                    }
                    catch (Exception exx)
                    {
                    }
                }

                #region Chiranjit On 01/09/2009
                #region ASTRA

                #region Drawing To Data Menu

                else if (string.Compare(commandname, "DD_OpenDrawing", true) == 0)
                {


                    //frm_DrawingToData fdt = new frm_DrawingToData();
                    frm_ASTRA_Inputs fdt = new frm_ASTRA_Inputs(VDoc.FileName, true);
                    fdt.Owner = this;
                    fdt.Show();
                    //using (OpenFileDialog ofd = new OpenFileDialog())
                    //{
                    //    //ofd.Filter = "TEXT Files, AST Files|*.txt;*.ast";
                    //    ofd.Filter = "Drawing Files|*.vdml;*.vdcl;*.dwg;*.dxf";
                    //    ofd.Title = "SELECT Drawing File";
                    //    if (ofd.ShowDialog() != DialogResult.Cancel)
                    //    {
                    //        //fName = ofd.FileName;
                    //        frm_DrawingToData fdt = new frm_DrawingToData(ofd.FileName);
                    //        fdt.Owner = this;
                    //        fdt.Show();
                    //    }
                    //}

                    return;










                    success = true;
                    createForm();
                    Childform form = this.ActiveMdiChild as Childform;
                    VectorDraw.Professional.vdObjects.vdDocument doc = form.vdScrollableControl1.BaseControl.ActiveDocument;
                    object ret = doc.GetOpenFileNameDlg(0, "", 0);
                    if (ret == null)
                    {
                        form.Close();
                        return;
                    }
                    string fname = (string)ret;
                    this.AppDataPath = System.IO.Path.GetDirectoryName(fname);
                    bool successopen = doc.Open(fname);
                    if (!successopen)
                    {
                        System.Windows.Forms.MessageBox.Show("Error openning " + fname, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        form.Close();
                        return;
                    }
                    else
                    {
                        AddToRecentFiles(doc.FileName);
                    }

                    doc.Redraw(false);
                    //Updates the window list
                    ActivateMdiChild(null);
                    ActivateMdiChild(this.ActiveMdiChild);
                    iACAD.IsOpenFile = false;

                    //vdDocument doc = ((Childform)this.ActiveMdiChild).vdScrollableControl1.BaseControl.ActiveDocument;
                    iACAD.Menu = this.MainMenuStrip;
                    iACAD.SetMenu("StructuralGeometry", doc);

                    //string file_nm = Path.Combine(Path.GetDirectoryName(doc.FileName), Path.GetFileNameWithoutExtension(doc.FileName).ToUpper() + "_INPUT.txt");
                    fName = Path.Combine(Path.GetDirectoryName(doc.FileName), Path.GetFileNameWithoutExtension(doc.FileName).ToUpper() + "_INPUT.txt");

                    iACAD.SaveUserFile(doc, fName);
                    commandLine.ExecuteCommand("drawStructure");
                    
                }
                else if (string.Compare(commandname, "DD_StructureDesign", true) == 0)
                {
                    //frm_DrawingToData fdt = new frm_DrawingToData();
                    if (fName != null)
                    {
                        frm_StructureDesign fdt = new frm_StructureDesign(fName);
                        fdt.Owner = this;
                        fdt.Show();
                    } 
                    else
                    {
                        if(Directory.Exists(AppDataPath) == false)
                            OnWorkingDir();
                        if (Directory.Exists(AppDataPath) == false) return;

                        frm_StructureDesign fdt = new frm_StructureDesign();

                        fdt.LastDesignWorkingFolder = AppDataPath;
                        fdt.Owner = this;
                        fdt.Show();
                    }
                    return;
                }
                else if (commandname.StartsWith("DD_"))
                {
                    vdDocument doc = ((Childform)this.ActiveMdiChild).vdScrollableControl1.BaseControl.ActiveDocument;
                    iACAD.Menu = this.MainMenuStrip;
                    iACAD.SetMenu(commandname, doc);
                }
                #endregion

                #region Joints / ON / OFF
                else if (string.Compare(commandname, "JointsOn", true) == 0)
                {
                    //fName = System.Environment.GetEnvironmentVariable("ASTRA");
                    Childform form = this.ActiveMdiChild as Childform;
                    for (int i = 0; i < form.vdScrollableControl1.BaseControl.ActiveDocument.Layers.Count; i++)
                    {
                        if (form.vdScrollableControl1.BaseControl.ActiveDocument.Layers[i].Name == "Nodes")
                        {
                            form.vdScrollableControl1.BaseControl.ActiveDocument.Layers[i].Frozen = false;
                            try
                            {
                                fTxtSize.chkJoint.Checked = true;
                            }
                            catch (Exception ex) { }
                            form.vdScrollableControl1.BaseControl.ActiveDocument.Redraw(true);
                            return;
                        }
                    }
                }
                else if (string.Compare(commandname, "JointsOff", true) == 0)
                {
                    //fName = System.Environment.GetEnvironmentVariable("ASTRA");
                    Childform form = this.ActiveMdiChild as Childform;
                    for (int i = 0; i < form.vdScrollableControl1.BaseControl.ActiveDocument.Layers.Count; i++)
                    {
                        if (form.vdScrollableControl1.BaseControl.ActiveDocument.Layers[i].Name == "Nodes")
                        {
                            form.vdScrollableControl1.BaseControl.ActiveDocument.Layers[i].Frozen = true;
                            try
                            {
                                fTxtSize.chkJoint.Checked = false;
                            }
                            catch (Exception ex) { }
                            form.vdScrollableControl1.BaseControl.ActiveDocument.Redraw(true);
                            return;
                        }
                    }
                }
                #endregion

                #region Support / On / OFF
                else if (string.Compare(commandname, "SupportPinnedOn", true) == 0)
                {
                    //fName = System.Environment.GetEnvironmentVariable("ASTRA");
                    Childform form = this.ActiveMdiChild as Childform;
                    for (int i = 0; i < form.vdScrollableControl1.BaseControl.ActiveDocument.Layers.Count; i++)
                    {
                        if (form.vdScrollableControl1.BaseControl.ActiveDocument.Layers[i].Name == "SupportPinned")
                        {
                            form.vdScrollableControl1.BaseControl.ActiveDocument.Layers[i].Frozen = false;
                            try
                            {
                                fTxtSize.chkSupportPinned.Checked = true;
                            }
                            catch (Exception ex) { }
                            form.vdScrollableControl1.BaseControl.ActiveDocument.Redraw(true);
                            return;
                        }
                    }
                }
                else if (string.Compare(commandname, "SupportPinnedOff", true) == 0)
                {
                    //fName = System.Environment.GetEnvironmentVariable("ASTRA");
                    Childform form = this.ActiveMdiChild as Childform;
                    for (int i = 0; i < form.vdScrollableControl1.BaseControl.ActiveDocument.Layers.Count; i++)
                    {
                        if (form.vdScrollableControl1.BaseControl.ActiveDocument.Layers[i].Name == "SupportPinned")
                        {
                            form.vdScrollableControl1.BaseControl.ActiveDocument.Layers[i].Frozen = true;
                            try
                            {
                                fTxtSize.chkSupportPinned.Checked = false;
                            }
                            catch (Exception ex) { }

                            form.vdScrollableControl1.BaseControl.ActiveDocument.Redraw(true);
                            return;
                        }
                    }
                }
                else if (string.Compare(commandname, "SupportFixedOn", true) == 0)
                {
                    //fName = System.Environment.GetEnvironmentVariable("ASTRA");
                    Childform form = this.ActiveMdiChild as Childform;
                    for (int i = 0; i < form.vdScrollableControl1.BaseControl.ActiveDocument.Layers.Count; i++)
                    {
                        if (form.vdScrollableControl1.BaseControl.ActiveDocument.Layers[i].Name == "SupportFixed")
                        {
                            form.vdScrollableControl1.BaseControl.ActiveDocument.Layers[i].Frozen = false;
                            try
                            {
                                fTxtSize.chkSupportFixed.Checked = true;
                            }
                            catch (Exception ex) { }
                            form.vdScrollableControl1.BaseControl.ActiveDocument.Redraw(true);
                            return;
                        }
                    }
                }
                else if (string.Compare(commandname, "SupportFixedOff", true) == 0)
                {
                    //fName = System.Environment.GetEnvironmentVariable("ASTRA");
                    Childform form = this.ActiveMdiChild as Childform;
                    for (int i = 0; i < form.vdScrollableControl1.BaseControl.ActiveDocument.Layers.Count; i++)
                    {
                        if (form.vdScrollableControl1.BaseControl.ActiveDocument.Layers[i].Name == "SupportFixed")
                        {
                            form.vdScrollableControl1.BaseControl.ActiveDocument.Layers[i].Frozen = true;
                            try
                            {
                                fTxtSize.chkSupportFixed.Checked = false;
                            }
                            catch (Exception ex) { }
                            form.vdScrollableControl1.BaseControl.ActiveDocument.Redraw(true);
                            return;
                        }
                    }
                }
                #endregion

                #region Members / On / OFF
                else if (string.Compare(commandname, "MembersOn", true) == 0)
                {
                    //fName = System.Environment.GetEnvironmentVariable("ASTRA");
                    Childform form = this.ActiveMdiChild as Childform;

                    for (int i = 0; i < form.vdScrollableControl1.BaseControl.ActiveDocument.Layers.Count; i++)
                    {
                        if (form.vdScrollableControl1.BaseControl.ActiveDocument.Layers[i].Name == "Members")
                        {
                            form.vdScrollableControl1.BaseControl.ActiveDocument.Layers[i].Frozen = false;
                            try
                            {
                                fTxtSize.chkMember.Checked = true;
                            }
                            catch (Exception ex) { }
                            form.vdScrollableControl1.BaseControl.ActiveDocument.Redraw(true);
                            return;
                        }
                    }
                }
                else if (string.Compare(commandname, "MembersOff", true) == 0)
                {
                    //fName = System.Environment.GetEnvironmentVariable("ASTRA");
                    Childform form = this.ActiveMdiChild as Childform;
                    for (int i = 0; i < form.vdScrollableControl1.BaseControl.ActiveDocument.Layers.Count; i++)
                    {
                        if (form.vdScrollableControl1.BaseControl.ActiveDocument.Layers[i].Name == "Members")
                        {
                            form.vdScrollableControl1.BaseControl.ActiveDocument.Layers[i].Frozen = true;
                            try
                            {
                                fTxtSize.chkMember.Checked = false;
                            }
                            catch (Exception ex) { }
                            form.vdScrollableControl1.BaseControl.ActiveDocument.Redraw(true);
                            return;
                        }
                    }
                }
                #endregion

                #region Joint Load / On / OFF
                else if (string.Compare(commandname, "JointLoadOn", true) == 0)
                {
                    //fName = System.Environment.GetEnvironmentVariable("ASTRA");
                    Childform form = this.ActiveMdiChild as Childform;


                    //vdLayer vd = form.vdScrollableControl1.BaseControl.ActiveDocument.Layers.FindName("JointLoad");
                    vdLayer vd = mainDoc.Layers.FindName("JointLoad");

                    if (vd != null)
                    {
                        vd.Frozen = false;
                        try
                        {
                            fTxtSize.chkJointLoad.Checked = true;
                        }
                        catch (Exception ex) { }

                    }

                }
                else if (string.Compare(commandname, "JointLoadOff", true) == 0)
                {
                    //fName = System.Environment.GetEnvironmentVariable("ASTRA");
                    Childform form = this.ActiveMdiChild as Childform;


                    //vdLayer vd = form.vdScrollableControl1.BaseControl.ActiveDocument.Layers.FindName("JointLoad");
                    vdLayer vd = mainDoc.Layers.FindName("JointLoad");

                    if (vd != null)
                    {
                        vd.Frozen = true;
                        try
                        {
                            fTxtSize.chkJointLoad.Checked = false;
                        }
                        catch (Exception ex) { }
                    }

                    //for (int i = 0; i < form.vdScrollableControl1.BaseControl.ActiveDocument.Layers.Count; i++)
                    //{
                    //    if (form.vdScrollableControl1.BaseControl.ActiveDocument.Layers[i].Name == "JointLoad")
                    //    {
                    //        form.vdScrollableControl1.BaseControl.ActiveDocument.Layers[i].Frozen = true;
                    //        try
                    //        {
                    //            fTxtSize.chkJointLoad.Checked = false;
                    //        }
                    //        catch (Exception ex) { }
                    //        form.vdScrollableControl1.BaseControl.ActiveDocument.Redraw(true);
                    //        return;
                    //    }
                    //}
                }
                #endregion


                #endregion
                #region ASTRA Draw Member


                else if (string.Compare(commandname, "drawmember", true) == 0)
                {
                    DrawMember();
                }

                else if (string.Compare(commandname, "eigenTest", true) == 0)
                {
                    AstrA_Moving_LOAD_Deflection();
                }
                else if (string.Compare(commandname, "RccSlab01", true) == 0)
                {
                    //RccSlab01_View();
                }
                else if (string.Compare(commandname, "RccSlab02", true) == 0)
                {
                    //RccSlab02_Drawing();
                }
                else if (string.Compare(commandname, "LoadingOn", true) == 0)
                {
                    LoadingOn();
                }

                else if (string.Compare(commandname, "LoadingOff", true) == 0)
                {
                    LoadingOff();
                }

                #endregion

                #region Draw DisNet Pipe
                else if (string.Compare(commandname, "drawdisnetpipe", true) == 0)
                {

                    //System.Environment.SetEnvironmentVariable("DISNET",@"D:\TEST DISNET\DisNetPipeDetails.txt");
                    //fName = System.Environment.GetEnvironmentVariable("DISNET");
                    if (fName == null)
                    {
                        //OpenFileDialog ofd = new OpenFileDialog();
                        //ofd.Filter = "TEXT Files|*.txt";
                        //ofd.Title = "SELECT DisNewPipeDetails File";

                        fName = Path.Combine(AppDataPath, "DisNetPipeDetails.txt");
                        if (File.Exists(fName))
                        {
                            commandLine.ExecuteCommand("new");
                        }
                        else
                        {
                            MessageBox.Show(fName + " not found.", "DisNet");
                            fName = null;
                            return;
                        }
                    }

                    string ext = Path.GetExtension(fName).ToLower();
                    if (ext != ".txt") return;

                    Childform form1 = this.ActiveMdiChild as Childform;
                    if (form1 != null)
                    {
                        VectorDraw.Professional.vdObjects.vdDocument doc = form1.vdScrollableControl1.BaseControl.ActiveDocument;

                        Propertiesform.Initialize(doc, GetResourcesPath(), this.panel2);
                        Propertiesform.LoadProps(null, doc, true);
                        FixSide(Propertiesform.mDocumentProperties.mSide);
                        HEADSNeed.ActionPipe3D.CmdPipe3DFromText(doc, fName);


                        fName = Path.Combine(AppDataPath, "model.txt");
                        DisNetDoc disDoc = new DisNetDoc(fName);
                        if (disDoc.ReadFromFile())
                        {
                            disDoc.Nodes.DrawNodes(doc);
                            disDoc.Pipes.DrawPipeDetails(doc);
                            disDoc.Loops.DrawLoops(doc);
                            doc.RenderMode = VectorDraw.Render.vdRender.Mode.Render;
                            doc.Redraw(true);
                        }
                        fName = null;
                    }
                }

                #endregion

                #region Draw DisNet Pipe Details
                else if (string.Compare(commandname, "disnetdetails", true) == 0)
                {

                    //System.Environment.SetEnvironmentVariable("DISNET",@"D:\TEST DISNET\DisNetPipeDetails.txt");
                    //fName = System.Environment.GetEnvironmentVariable("DISNET");
                    if (fName == null)
                    {
                        //OpenFileDialog ofd = new OpenFileDialog();
                        //ofd.Filter = "TEXT Files|*.txt";
                        //ofd.Title = "SELECT DisNewPipeDetails File";

                        fName = Path.Combine(AppDataPath, "Model.dtls");
                        if (File.Exists(fName))
                        {
                            HEADSNeed.DisNet.frmDisNetDetails frm = new HEADSNeed.DisNet.frmDisNetDetails(fName);
                            frm.Owner = this;
                            frm.Show();
                            fName = null;
                            return;
                        }
                    }
                }

                #endregion

                #region Structure Form [ Chiranjit 22/09/2009 ] Hare Krishna

                #region ASTRA Report
                else if (string.Compare(commandname, "AstraReport", true) == 0)
                {
                    ASTRA_Report();
                }
                #endregion
                #region AstraPostProcess


                else if (string.Compare(commandname, "AstraPostProcess", true) == 0)
                {
                    PostProcess();
                }
                #endregion
                #region Drawing To Data
                //else if (string.Compare(commandname, "ProjectTitle", true) == 0)
                //{
                //    //ProjectTitle();
                //}
                #endregion

                #region textsize
                else if (string.Compare(commandname, "textsize", true) == 0)
                {
                    try
                    {
                        ShowTextSize();
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else if (string.Compare(commandname, "writecoorinatestofile", true) == 0)
                {
                    try
                    {
                        WriteCoorinatesToFile();
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else if (string.Compare(commandname, "readcoorinatesfromfile", true) == 0)
                {
                    try
                    {
                        ReadCoorinatesFromFile();
                    }
                    catch (Exception ex)
                    {
                    }
                }
                #endregion
                #region explorer
                else if (string.Compare(commandname, "explorer", true) == 0)
                {
                    try
                    {
                        System.Diagnostics.Process.Start("Explorer", "/e," + Path.GetDirectoryName(astDoc.FileName));
                    }
                    catch (Exception ex)
                    {
                    }
                }

                #endregion
                #region RunAnalysis
                else if (string.Compare(commandname, "RunAnalysis", true) == 0)
                {
                    RunAnalysis();
                    astDoc = new HEADSNeed.ASTRA.ASTRAClasses.ASTRADoc(fName);
                    ASTRAMenuSetup();
                    commandLine.ExecuteCommand("new");
                    DrawStructure();
                    Show_ASTRA_Controls();
                    OnFileNew();
                    this.MdiChildren[this.MdiChildren.Length - 1].Close();
                    this.MdiChildren[0].WindowState = FormWindowState.Normal;

                    try
                    {
                        VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(mainDoc);
                    }
                    catch (Exception ex) { }
                }
                else if (string.Compare(commandname, "MaxForce", true) == 0)
                {
                    try
                    {
                        MovingLoadAnalysis.frmMovingForces f = new frmMovingForces(mainDoc, fName, 0);
                        f.Owner = this;
                        f.Show();
                    }
                    catch (Exception ex) { }
                }
                     
                #endregion
                #region drawStructure

                else if (string.Compare(commandname, "drawStructure", true) == 0)
                {



                    //Chiranjit [2014 11 09] START
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Filter = "TEXT Files|*.txt;";
                    ofd.Title = "SELECT ASTRA INPUT File";
                    if (ofd.ShowDialog() != DialogResult.Cancel)
                    {
                        fName = ofd.FileName;
                        astDoc = new HEADSNeed.ASTRA.ASTRAClasses.ASTRADoc(fName);

                        AppDataPath = Path.GetDirectoryName(fName);


                        VDoc.ActiveLayOut.Entities.EraseAll();

                        astDoc.Joints.DrawJointsText(VDoc, 0.09);
                        astDoc.Members.DrawMember(VDoc, 0.09);
                        astDoc.Supports.DrawSupport(VDoc);

                        ShowTextSize();
                        //VDoc.Redraw(true);
                        //VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(VDoc);
                        VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(VDoc);
                        VDoc.Redraw(true);

                    }
                    else
                    {
                        return;
                    }
                   

                    //Chiranjit [2014 11 09] END


                    return;

                    //Chiranjit [2014 04 12]
                    frm_ASTRA_Analysis fa = new frm_ASTRA_Analysis(fName, false);
                    fa.Owner = this;
                    fa.Show();

                    return;

                    astDoc = new HEADSNeed.ASTRA.ASTRAClasses.ASTRADoc(fName);
                    ASTRAMenuSetup();
                    commandLine.ExecuteCommand("new");
                    DrawStructure();
                    Show_ASTRA_Controls();
                    OnFileNew();
                    this.MdiChildren[this.MdiChildren.Length - 1].Close();
                    this.MdiChildren[0].WindowState = FormWindowState.Normal;
                }
                else if (string.Compare(commandname, "astraInput", true) == 0)
                {
                    //Chiranjit [2014 10 28]
                    frm_DrawingToData fa = new frm_DrawingToData(fName);
                    fa.Owner = this;
                    fa.Show();
                    return;

                }
                else if (string.Compare(commandname, "SLAB", true) == 0)
                {
                    //RccSlab01_View();
                }

                else if (string.Compare(commandname, "BeamAnalysis", true) == 0)
                {

                    BeamAnalysisCalculator calculator = new BeamAnalysisCalculator();
                    calculator.Test();
                    return;


                    //AstraDataFileOpen();

                    //astDoc = new HEADSNeed.ASTRA.ASTRAClasses.ASTRADoc(fName);
                    //commandLine.ExecuteCommand("new");
                    //DrawStructure();
                    //OnFileNew();
                    //this.MdiChildren[this.MdiChildren.Length - 1].Close();
                    //this.MdiChildren[0].WindowState = FormWindowState.Normal;


                    HEADSNeed.ASTRA.ASTRAClasses.BeamAnalysisCollection beamAna = new HEADSNeed.ASTRA.ASTRAClasses.BeamAnalysisCollection();

                    beamAna.ReadFromFile(Path.Combine(Path.GetDirectoryName(fName), "test.rep"));

                    Childform cld = this.MdiChildren[0] as Childform;
                    vdDocument doc = cld.vdScrollableControl1.BaseControl.ActiveDocument;
                    beamAna.Draw_StructureGraph(doc, 3, 1, true);

                    if (this.MdiChildren.Length == 2)
                    {

                        Childform fcld = this.MdiChildren[1] as Childform;
                        vdDocument momentDoc = fcld.vdScrollableControl1.BaseControl.ActiveDocument;
                        //beamAna.Draw_StructureGraph(doc, 3, 1, true);

                        frmBeamAnalysis fbeamAna = new frmBeamAnalysis(fName, doc, momentDoc);
                        fbeamAna.Owner = this;
                        fbeamAna.Show();

                    }
                    else
                    {
                        frmBeamAnalysis fbeamAna = new frmBeamAnalysis(fName, doc);
                        fbeamAna.Owner = this;
                        fbeamAna.Show();
                    }
                }


                else if (string.Compare(commandname, "AstraDataFileOpen", true) == 0)
                {
                    //OpenFileDialog ofd = new OpenFileDialog();
                    //ofd.Filter = "TEXT Files, AST Files|*.txt;*.ast";
                    //ofd.Title = "SELECT ASTRA INPUT File";
                    //if (ofd.ShowDialog() != DialogResult.Cancel)
                    //{
                    //    fName = ofd.FileName;
                    //    astDoc = new HEADSNeed.ASTRA.ASTRAClasses.ASTRADoc(fName);
                    //    commandLine.ExecuteCommand("new");
                    //}
                    //else
                    //{
                    //    return;
                    //}
                    //DrawStructure();
                    //OnFileNew();
                    //this.MdiChildren[this.MdiChildren.Length - 1].Close();
                    //this.MdiChildren[0].WindowState = FormWindowState.Normal;
                    AstraDataFileOpen();
                }
                #endregion
                #endregion



                #endregion

                #region Chiranjit [2011 06 09]
                else if (string.Compare(commandname, "OpenMultiDrawings", true) == 0)
                {
                    //fName = System.Environment.GetEnvironmentVariable("HEADSPROJECT");
                    using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                    {
                        fbd.SelectedPath = this.drawingCode;
                        if (fbd.ShowDialog() != DialogResult.Cancel)
                        {
                            fName = fbd.SelectedPath;
                            //commandLine.ExecuteCommand("new");
                            this.drawingCode = fbd.SelectedPath;
                            OpenMultipleDrawings();
                        }
                    }
                }
                #endregion Chiranjit [2011 06 09]

                #region Chitanjit On 29/07/2010 Alignment
                else if (string.Compare(commandname, "Align", true) == 0)
                {
                    OnAlignment();
                }
                #endregion

                else if (commandname.ToUpper().StartsWith("CATCHMENT_"))
                {
                    //if (!Directory.Exists(this.AppDataPath))
                    //{
                    //    OnWorkingDir();
                    //}
                    //if (Directory.Exists(this.AppDataPath))
                    //{
                    try
                    {
                        Childform form = this.ActiveMdiChild as Childform;
                        VectorDraw.Professional.vdObjects.vdDocument doc = form.vdScrollableControl1.BaseControl.ActiveDocument;
                        HEADSNeed.ASTRA.ASTRAClasses.CatchmentFunctions.CatchmentMenu(doc, commandname, commandLine, this, this);
                    }
                    catch (Exception ex) { }
                    //}
                    //HEADSNeed.ProjectTunnel.ProjectClass.Instance.OpenMenu(commandname, Menu);
                    //this.
                }
                
                else if (string.Compare(commandname, "Save", true) == 0)
                {

                    //MessageBox.Show("CheckHasp " + "=" + HASP_LOCK.Check_Lock.CheckHasp().ToString());
                    //MessageBox.Show("Check_ASTRA_Lock_19 " + "=" + HASP_LOCK.Check_Lock.Check_ASTRA_Lock_19().ToString());
                    //MessageBox.Show("Check_ASTRA_Lock_18 " + "=" + HASP_LOCK.Check_Lock.Check_ASTRA_Lock_18().ToString());
                    //MessageBox.Show("Check_ASTRA_Structure_Lock " + "=" + HASP_LOCK.Check_Lock.Check_ASTRA_Structure_Lock().ToString());

                    //MessageBox.Show("Check_ASTRA_Lock_19 " + "=" + HASP_Lock.Check_ASTRA_Lock_19().ToString());
                    //MessageBox.Show("Check_ASTRA_Lock_18 " + "=" + HASP_Lock.Check_ASTRA_Lock_18().ToString());
                    //MessageBox.Show("Check_ASTRA_Structure_Lock " + "=" + HASP_Lock.Check_ASTRA_Structure_Lock().ToString());


                    if (this.ReleaseType == HeadsUtils.eHEADS_RELEASE_TYPE.DEMO)
                    {
                        MessageBox.Show("This feature is not available in demo version.", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        Childform form = this.ActiveMdiChild as Childform;
                        VectorDraw.Professional.vdObjects.vdDocument doc = form.vdScrollableControl1.BaseControl.ActiveDocument;

                        string fname = doc.FileName;
                        string ver = "";
                        if (Path.GetDirectoryName(fname) == "")
                        {

                            fname = doc.GetSaveFileNameDlg(fname, out ver);
                            if (fname != null)
                            {
                                doc.SaveAs(fname, null, ver);
                                AddToRecentFiles(fname);
                                //Updates the window list
                                Form frm = this.ActiveMdiChild;
                                frm.Text = fname;
                                ActivateMdiChild(null);
                                ActivateMdiChild(frm);
                            }
                        }
                        else
                        {
                            doc.SaveAs(doc.FileName);
                            AddToRecentFiles(doc.FileName);
                        }
                    }
                    success = true;
                }
                else if (string.Compare(commandname, "SaveAs", true) == 0)
                {
                    Childform form = this.ActiveMdiChild as Childform;
                    VectorDraw.Professional.vdObjects.vdDocument doc = form.vdScrollableControl1.BaseControl.ActiveDocument;
                    string fname = "";
                    string ver = "";
                    if (this.ReleaseType == HeadsUtils.eHEADS_RELEASE_TYPE.DEMO)
                    {
                        fname = doc.GetSaveFileNameDlg(doc.FileName, out ver);
                        MessageBox.Show("This feature is not available in demo version.", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        fname = doc.GetSaveFileNameDlg(doc.FileName, out ver);
                        if (fname != null)
                        {
                            doc.SaveAs(fname, null, ver);
                            AddToRecentFiles(fname);

                            //Updates the window list
                            Form frm = this.ActiveMdiChild;
                            frm.Text = fname;
                            ActivateMdiChild(null);
                            ActivateMdiChild(frm);
                        }
                    }
                    success = true;
                }
                else if (string.Compare(commandname, "Publish", true) == 0)
                {
                    if (this.ReleaseType == HeadsUtils.eHEADS_RELEASE_TYPE.DEMO)
                    {
                        MessageBox.Show("This feature is not available in demo version.", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        Childform form = this.ActiveMdiChild as Childform;
                        VectorDraw.Professional.vdObjects.vdDocument doc = form.vdScrollableControl1.BaseControl.ActiveDocument;

                        Publish frm = new Publish();
                        frm.Initialize(doc);
                        frm.ShowDialog(this);
                    }
                    success = true;
                }
                else if (string.Compare(commandname, "Print", true) == 0)
                {
                    if (this.ReleaseType == HeadsUtils.eHEADS_RELEASE_TYPE.DEMO)
                    {
                        MessageBox.Show("This feature is not available in demo version.", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        if (this.ActiveMdiChild != null)
                        {
                            Childform form = this.ActiveMdiChild as Childform;
                            VectorDraw.Professional.vdObjects.vdDocument doc = form.vdScrollableControl1.BaseControl.ActiveDocument;
                            VectorDraw.Professional.ActionUtilities.vdCommandAction.PrintEx(doc);
                        }
                    }
                    success = true;
                }
                else if (string.Compare(commandname, "PDFExport", true) == 0)
                {
                    if (this.ReleaseType == HeadsUtils.eHEADS_RELEASE_TYPE.DEMO)
                    {
                        MessageBox.Show("This feature is not available in demo version.", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        Childform form = this.ActiveMdiChild as Childform;
                        VectorDraw.Professional.vdObjects.vdDocument doc = form.vdScrollableControl1.BaseControl.ActiveDocument;
                        //doc.FileProperties.PDFExportProperties = vdFileProperties.PDFExportPropertiesFlags.UsePrinterPropertiesWithOutlineText;
                        VectorDraw.Professional.vdObjects.vdPrint printer = new VectorDraw.Professional.vdObjects.vdPrint(doc.ActiveLayOut.Printer);
                        printer.PrinterName = VectorDraw.Professional.Utilities.vdGlobals.GetFileNameWithoutExtension(doc.FileName) + ".pdf";
                        printer.InitializePreviewFormProperties(true, true, false, false);
                        printer.DialogPreview();
                    }
                    success = true;
                }
                else if (string.Compare(commandname, "ModelingFromTextData", true) == 0)
                {
                    OnModelingFromTextData();
                }
                else if (string.Compare(commandname, "ModelEdit", true) == 0)
                {
                    OnModelEdit();
                }
                else if (string.Compare(commandname, "GroundModeling", true) == 0)
                {
                    OnGroundModeling();
                }
                else if (string.Compare(commandname, "ContourModeling", true) == 0)
                {
                    OnContourModeling();
                }
                else if (string.Compare(commandname, "DrawPolyLineFromText", true) == 0)
                {
                    Childform form = this.ActiveMdiChild as Childform;
                    HEADSNeed.DrawingToData.frmOptions f = new HEADSNeed.DrawingToData.frmOptions(form.vdScrollableControl1.BaseControl.ActiveDocument);
                    f.Owner = this;
                    f.Show();
                }
                else if (string.Compare(commandname, "ReversePolyline", true) == 0)
                {
                    Childform form = this.ActiveMdiChild as Childform;
                    HEADSNeed.DrawingToData.frmDrawPolylineFromFile.ReversePolyline(form.vdScrollableControl1.BaseControl.ActiveDocument);
                }
                else if (string.Compare(commandname, "DrawingToDataFile", true) == 0)
                {
                    Childform form = this.ActiveMdiChild as Childform;
                    HEADSNeed.DrawingToData.frmDrawPolylineFromFile f = new HEADSNeed.DrawingToData.frmDrawPolylineFromFile(form.vdScrollableControl1.BaseControl.ActiveDocument, false);
                    f.IsModeling = false;
                    f.Owner = this;
                    f.Show();
                }
                else if (string.Compare(commandname, "Export", true) == 0)
                {
                    if (this.ReleaseType == HeadsUtils.eHEADS_RELEASE_TYPE.DEMO)
                    {
                        MessageBox.Show("This feature is not available in demo version.", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        Childform form = this.ActiveMdiChild as Childform;
                        VectorDraw.Professional.vdObjects.vdDocument doc = form.vdScrollableControl1.BaseControl.ActiveDocument;
                        frmExport frm = new frmExport();
                        frm.Show(doc);
                    }
                    success = true;
                }
                else if (string.Compare(commandname, "Cascade", true) == 0) { success = true; this.LayoutMdi(MdiLayout.Cascade); }
                else if (string.Compare(commandname, "TileHorizontal", true) == 0) { this.LayoutMdi(MdiLayout.TileHorizontal); success = true; }
                else if (string.Compare(commandname, "TileVertical", true) == 0) { this.LayoutMdi(MdiLayout.TileVertical); success = true; }
                else if (string.Compare(commandname, "ArangeIcons", true) == 0) { this.LayoutMdi(MdiLayout.ArrangeIcons); success = true; }
                else if (string.Compare(commandname, "CloseAll", true) == 0)
                {
                    foreach (Form frm in this.MdiChildren)
                        frm.Close();
                }
                else if (string.Compare(commandname, "Exit", true) == 0) { success = true; this.Close(); }
                else if (string.Compare(commandname, "Properties", true) == 0)
                {
                    Childform form = this.ActiveMdiChild as Childform;
                    VectorDraw.Professional.vdObjects.vdDocument doc = form.vdScrollableControl1.BaseControl.ActiveDocument;

                    Propertiesform.Initialize(doc, GetResourcesPath(), this.panel2);
                    Propertiesform.ShowDialog(this);
                    GeneralProperties.Side s = Propertiesform.mDocumentProperties.mSide;
                    FixSide(s);
                }
                else
                {
                        
                //Chiranjit [2015 04 12]
                    if (string.Compare(commandname, "WriteBlock", true) == 0)
                    {
                        if (this.ReleaseType == HeadsUtils.eHEADS_RELEASE_TYPE.DEMO)
                        {
                            MessageBox.Show("This feature is not available in demo version.", this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        //else
                        //{
                        //}
                    }
                 


                    HeadsCommnds hdCommand = (HeadsCommnds)this.hashHeadsCommands[commandname];
                    if (hdCommand != null)
                    {
                        hdCommand();
                        success = true;
                    }
                }
            }
            catch (Exception ex) { }
        }
        private void OnAlignment()
        {
            HEADSNeed.ALIGN.Alignment align = null;
            Childform form1 = this.ActiveMdiChild as Childform;
            if (form1 != null)
            {
                VectorDraw.Professional.vdObjects.vdDocument doc = form1.vdScrollableControl1.BaseControl.ActiveDocument;
                align = new HEADSNeed.ALIGN.Alignment(doc);
                align.SetAlign();
            }
        }
       
        #region ASTRA Drawing To Data [2010 04 14]
        //private void ProjectTitle()
        //{
        //    iACAD.SetProjectTitle();
        //}
        #endregion

        #region ASTRA HELP FUNCTIONS [Chiranjit 23/09/2009, Panchamee Tithee]
        bool memDetailsOn = false;

        public ToolStripMenuItem ASTRALoadingMenu
        {
            get
            {
                MenuStrip ms = this.MainMenuStrip;
                ToolStripItem tsi = ms.Items["ASTRA"];

                tsi.Name = "ASTRA";
                ToolStripMenuItem tsmi = ms.Items["ASTRA"] as ToolStripMenuItem;
                tsmi = tsmi.DropDown.Items["Loading"] as ToolStripMenuItem;
                return tsmi;
            }
            //set
            //{

            //    MenuStrip ms = this.MainMenuStrip;
            //    ToolStripItem tsi = ms.Items["ASTRA"];

            //    tsi.Name = "ASTRA";
            //    ToolStripMenuItem tsmi = ms.Items["ASTRA"] as ToolStripMenuItem;
            //    tsmi = tsmi.DropDown.Items["Loading"] as ToolStripMenuItem;
            //    tsmi.Checked = value;
            //}
        }
        public ToolStripMenuItem ASTRAAnalysisMenu
        {
            get
            {
                MenuStrip ms = this.MainMenuStrip;
                ToolStripItem tsi = ms.Items["ASTRA"];

                tsi.Name = "ASTRA";
                ToolStripMenuItem tsmi = ms.Items["ASTRA"] as ToolStripMenuItem;
                tsmi = tsmi.DropDown.Items["Analysis"] as ToolStripMenuItem;
                return tsmi;
            }
        }
        public ToolStripMenuItem ASTRABeamAnalysisMenu
        {
            get
            {
                MenuStrip ms = this.MainMenuStrip;
                ToolStripItem tsi = ms.Items["ASTRA"];

                tsi.Name = "ASTRA";
                ToolStripMenuItem tsmi = ms.Items["ASTRA"] as ToolStripMenuItem;
                tsmi = tsmi.DropDown.Items["Beam Analysis"] as ToolStripMenuItem;
                return tsmi;
            }
        }
        public ToolStripMenuItem ASTRATextSizeMenu
        {
            get
            {
                MenuStrip ms = this.MainMenuStrip;
                ToolStripItem tsi = ms.Items["ASTRA"];

                tsi.Name = "ASTRA";
                ToolStripMenuItem tsmi = ms.Items["ASTRA"] as ToolStripMenuItem;
                tsmi = tsmi.DropDown.Items["Text Size"] as ToolStripMenuItem;
                return tsmi;
            }
        }
        public ToolStripMenuItem ASTRAStructureMenu
        {
            get
            {
                MenuStrip ms = this.MainMenuStrip;
                ToolStripItem tsi = ms.Items["ASTRA"];

                tsi.Name = "ASTRA";
                ToolStripMenuItem tsmi = ms.Items["ASTRA"] as ToolStripMenuItem;
                tsmi = tsmi.DropDown.Items["Structure"] as ToolStripMenuItem;
                return tsmi;
            }
        }
        public ToolStripMenuItem ASTRAReportMenu
        {
            get
            {
                MenuStrip ms = this.MainMenuStrip;
                ToolStripItem tsi = ms.Items["ASTRA"];

                tsi.Name = "ASTRA";
                ToolStripMenuItem tsmi = ms.Items["ASTRA"] as ToolStripMenuItem;
                tsmi = tsmi.DropDown.Items["Report"] as ToolStripMenuItem;

                

                return tsmi;

            }
        }
        public ToolStripMenuItem ASTRALoadDeflectionMenu
        {
            get
            {
                MenuStrip ms = this.MainMenuStrip;
                ToolStripItem tsi = ms.Items["ASTRA"];

                tsi.Name = "ASTRA";
                ToolStripMenuItem tsmi = ms.Items["ASTRA"] as ToolStripMenuItem;
                tsmi = tsmi.DropDown.Items["Load Deflection"] as ToolStripMenuItem;
                return tsmi;
            }
            //set
            //{

            //    MenuStrip ms = this.MainMenuStrip;
            //    ToolStripItem tsi = ms.Items["ASTRA"];

            //    tsi.Name = "ASTRA";
            //    ToolStripMenuItem tsmi = ms.Items["ASTRA"] as ToolStripMenuItem;
            //    tsmi = tsmi.DropDown.Items["PostProcess"] as ToolStripMenuItem;
            //    tsmi.Checked = value;
            //}
        }
        public ToolStripMenuItem ASTRAPostProcessMenu
        {
            get
            {
                MenuStrip ms = this.MainMenuStrip;
                ToolStripItem tsi = ms.Items["ASTRA"];

                tsi.Name = "ASTRA";
                ToolStripMenuItem tsmi = ms.Items["ASTRA"] as ToolStripMenuItem;
                tsmi = tsmi.DropDown.Items["Post Process"] as ToolStripMenuItem;
                return tsmi;
            }
            //set
            //{

            //    MenuStrip ms = this.MainMenuStrip;
            //    ToolStripItem tsi = ms.Items["ASTRA"];

            //    tsi.Name = "ASTRA";
            //    ToolStripMenuItem tsmi = ms.Items["ASTRA"] as ToolStripMenuItem;
            //    tsmi = tsmi.DropDown.Items["PostProcess"] as ToolStripMenuItem;
            //    tsmi.Checked = value;
            //}
        }

        public void AstraDataFileOpen()
        {
            try
            {

                //frm_ASTRA_Analysis fa = new frm_ASTRA_Analysis();
                frm_ASTRA_Inputs fa = null;

                //HEADSNeed.ASTRA.ASTRAControls.frm_TextAnalysis fa = null;


                if (File.Exists(fName)) fa = new frm_ASTRA_Inputs(fName);
                else fa = new frm_ASTRA_Inputs();
                //if (File.Exists(fName)) fa = new HEADSNeed.ASTRA.ASTRAControls.frm_TextAnalysis(fName);
                //else fa = new HEADSNeed.ASTRA.ASTRAControls.frm_TextAnalysis();

                //frm_ASTRA_Inputs fa = new frm_ASTRA_Inputs(VDoc.FileName, true);
                fa.Owner = this;
                fa.Show();
                return;

                OpenFileDialog ofd = new OpenFileDialog();
                //ofd.Filter = "TEXT Files, AST Files|*.txt;*.ast";
                ofd.Filter = "TEXT Files|*.txt;";
                ofd.Title = "SELECT ASTRA INPUT File";
                if (ofd.ShowDialog() != DialogResult.Cancel)
                {
                    fName = ofd.FileName;

                    //frm_ASTRA_Analysis fa = new frm_ASTRA_Analysis(fName);
                    //fa.Owner = this;
                    //fa.Show();
                    //return;

                    astDoc = new HEADSNeed.ASTRA.ASTRAClasses.ASTRADoc(fName);

                    AppDataPath = Path.GetDirectoryName(fName);
                    commandLine.ExecuteCommand("new");
                }
                else
                {
                    return;
                }
                IS_ASTRA_Control = false;
                DrawStructure();
                Show_ASTRA_Controls();

                OnFileNew();
                this.MdiChildren[this.MdiChildren.Length - 1].Close();
                this.MdiChildren[0].WindowState = FormWindowState.Normal;
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(VDoc);
            }
            catch (Exception ex) { MessageBox.Show("Input Data file is not correct format.", "ASTRA", MessageBoxButtons.OK); }
        }

        frm_ASTRA_Controls fa = null;
        private void Show_ASTRA_Controls()
        {
            if (fa != null)
            {
                try
                {
                    fa.Close();
                }
                catch (Exception ex) { }
            }
            fa = new frm_ASTRA_Controls(this);
            fa.Owner = this;
            double x, y, w, h;
            x = y = w = h = 0.0d;
            x = panel2.Location.X + panel2.Width;
            y = panel2.Location.Y;
            //w = this.Width - RightToolbarsPanel.Width * 1 - panel2.Width;
            w = panel2.Width;
            h = panel2.Height;

            int nx, ny;
            //nx = panel2.Width + (int)w * 2 / 3;
            nx = panel2.Width - fa.Width;
            ny = panel2.Location.Y + 25;


            fa.Height = panel2.Height;
            fa.Location = new Point(nx, ny);
            fa.Show();
        }
        public void ASTRA_Structure()
        {
            if (fName == null)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "TEXT Files, AST Files|*.txt;*.ast";
                ofd.Title = "SELECT ASTRA INPUT File";
                if (ofd.ShowDialog() != DialogResult.Cancel)
                {
                    fName = ofd.FileName;
                    commandLine.ExecuteCommand("new");
                }
                else
                {
                    fName = null;
                    return;
                }
            }
            //astDoc = new HEADSNeed.ASTRA.ASTRAClasses.ASTRADoc(fName);

            //frm = new HEADSNeed.ASTRA.ASTRAForms.frmASTRAMemberDetails(astDoc);
            //memDetailsOn = true;


            Childform form = this.ActiveMdiChild as Childform;
            VectorDraw.Professional.vdObjects.vdDocument doc = form.vdScrollableControl1.BaseControl.ActiveDocument;



            //if (frm.ShowDialog() != DialogResult.OK)
            //{
            //    return;
            //}
            DrawMemberWithLoadCase(1);
            StructureForm();

            ASTRA_StructureForm();
            HEADSNeed.ASTRA.ASTRAForms.frmMemberLoadCase frmMemLoad = new
               HEADSNeed.ASTRA.ASTRAForms.frmMemberLoadCase(doc, memberDoc, memberLoadDoc, memberJointLoadDoc, astDoc);
            frmMemLoad.Owner = this;

            frmMemLoad.Location = new Point(this.Width / 2 - frmMemLoad.Width / 2, this.Height - frmMemLoad.Height - toolStrip1.Height);
            frmMemLoad.Show();



        }
        public void ASTRA_Loading()
        {
            if (fName == null)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "TEXT Files, AST Files|*.txt;*.ast";
                ofd.Title = "SELECT ASTRA INPUT File";
                if (ofd.ShowDialog() != DialogResult.Cancel)
                {
                    fName = ofd.FileName;
                    commandLine.ExecuteCommand("new");
                }
                else
                {
                    fName = null;
                    return;
                }
            }
            //astDoc = new HEADSNeed.ASTRA.ASTRAClasses.ASTRADoc(fName);

            Childform form = this.ActiveMdiChild as Childform;
            VectorDraw.Professional.vdObjects.vdDocument doc = form.vdScrollableControl1.BaseControl.ActiveDocument;

            DrawMemberWithLoadCase(1);
            StructureForm();

            HEADSNeed.ASTRA.ASTRAForms.frmMemberLoadCase frmMemLoad = new
               HEADSNeed.ASTRA.ASTRAForms.frmMemberLoadCase(doc, memberDoc, memberLoadDoc, memberJointLoadDoc, astDoc);
            frmMemLoad.Owner = this;
            frmMemLoad.Location = new Point(this.Width / 2 - frmMemLoad.Width / 2, this.Height - frmMemLoad.Height - toolStrip1.Height);
            frmMemLoad.Show();
        }

        public void ASTRA_StructureForm()
        {
            string save_file = Path.Combine(Application.StartupPath, "test.vdml");

            #region Close All Window
            for (int ic = 0; ic < this.MdiChildren.Length; ic++)
            {
                if (this.MdiChildren[ic].Name == LastStructureFormName)
                {
                    continue;
                }
                else
                {
                    Childform chld = this.MdiChildren[ic] as Childform;
                    try
                    {
                        chld.vdScrollableControl1.BaseControl.ActiveDocument.SaveAs(save_file);
                    }
                    catch (Exception ex) { }
                    this.MdiChildren[ic].Close();
                    ic = -1;
                }
            }
            try
            {
                File.Delete(save_file);
            }
            catch (Exception ex) { }

            #endregion
            int memberIndex = 3;
            int memberLoadIndex = 3;

            memberIndex = astDoc.Members.IndexOf(1);
            memberLoadIndex = astDoc.MemberLoads.IndexOf(1);

            #region Calculate Windows Location And Size

            panel2.Width = 210; // Left Side Property Box
            double x, y, w, h;
            x = y = w = h = 0.0d;
            x = panel2.Location.X + panel2.Width;
            y = panel2.Location.Y;
            w = this.Width - RightToolbarsPanel.Width * 1 - panel2.Width;
            h = panel2.Height;

            this.MdiChildren[0].Location = new Point(0, 0);
            this.MdiChildren[0].Size = new Size((int)w * 2 / 3, (int)h - 50);
            this.MdiChildren[0].WindowState = FormWindowState.Normal;

            int nx, ny;
            nx = panel2.Width + (int)w * 2 / 3;
            ny = panel2.Location.Y + 25;

            try
            {
                nodeGrid.Close();
                frmMemLoadCase.Close();
            }
            catch (Exception exx) { }
            nodeGrid = new HEADSNeed.ASTRA.ASTRAForms.frmNodeGrid(astDoc, mainDoc);
            nodeGrid.Owner = this;
            nodeGrid.Location = new Point(nx, ny);
            nodeGrid.Size = new Size((int)w / 3, (int)h / 2);
            nodeGrid.Show();


            ny = (ny + (int)h / 2);
            try
            {
                memGrid.Close();
            }
            catch (Exception exx) { }
            memGrid = new HEADSNeed.ASTRA.ASTRAForms.frmMemberGrid(astDoc, mainDoc);
            Childform chform = this.MdiChildren[0] as Childform;

            chform.MemberGrid = memGrid;
            memGrid.Owner = this;
            memGrid.Location = new Point(nx, ny);
            memGrid.Size = new Size((int)w / 3, (int)h / 2);
            memGrid.Show();

            #endregion

            ShowTextSize();
            
        }

        frmTextSize fTxtSize = null;
        public void ShowTextSize()
        {
            int indx = -1;
            try
            {
                ////try
                ////{
                ////}
                ////catch (Exception exx) { }
                fTxtSize.Close();
                //fTxtSize.Visible = true;
            }
            catch (Exception exx)
            {
            }
            this.MdiChildren[0].WindowState = FormWindowState.Normal;
            //fTxtSize = new frmTextSize(mainDoc, (int)(astDoc.Members.GetTextSize() * 10), astDoc);
            //Chiranjit [2011 11 10] Set Default Test size 1
            //VDoc
            fTxtSize = new frmTextSize(VDoc, 1, astDoc);
            fTxtSize.Owner = this;
            fTxtSize.Size = new Size(this.MdiChildren[0].Size.Width, 74);



            //fTxtSize.Location = new Point(217, nodeGrid.Location.Y + MdiChildren[0].Size.Height);
            fTxtSize.Location = new Point(217, 579);
            fTxtSize.chkJointLoad.Enabled = false;
            fTxtSize.Show();
        }
        public void ShowPostProcess()
        {
            try
            {
                frmPostProc.Close();
            }
            catch (Exception exx)
            {
            }

        }
        public void StructureForm()
        {
            try
            {
                if (!File.Exists(astDoc.FileName))
                {
                    return;
                }
                #region Close All Window
                for (int ic = 0; ic < this.MdiChildren.Length; ic++)
                {
                    if (this.MdiChildren[ic].Name == LastStructureFormName)
                    {

                        continue;
                    }
                    else
                    {
                        this.MdiChildren[ic].Close();
                        ic = -1;
                    }
                }
                #endregion

                int memberIndex = -1;
                int memberLoadIndex = -1;
                memberIndex = astDoc.Members.IndexOf(1);
                memberLoadIndex = astDoc.MemberLoads.IndexOf(1);

                Childform form = this.ActiveMdiChild as Childform;
                VectorDraw.Professional.vdObjects.vdDocument doc = form.vdScrollableControl1.BaseControl.ActiveDocument;
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);

                #region Open Member Form

                OnFileNew();
                form = this.ActiveMdiChild as Childform;
                form.Name = "frmMember";
                form.Text = "Member.vdml";
                doc = form.vdScrollableControl1.BaseControl.ActiveDocument;
                astDoc.Members.DrawMember(astDoc.Members[memberIndex], doc);
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);
                memberDoc = doc;
                memberDoc.ShowUCSAxis = false;
                #endregion

                #region Open Member Load Form


                OnFileNew();
                form = this.ActiveMdiChild as Childform;
                form.Text = "MemberLoad.vdml";
                form.Name = "frmMemberLoad";
                doc = form.vdScrollableControl1.BaseControl.ActiveDocument;
                memberLoadDoc = doc;
                memberLoadDoc.ShowUCSAxis = false;
                try
                {
                    astDoc.Members.DrawMember(astDoc.Members[memberIndex], doc);
                    astDoc.MemberLoads.DrawMemberLoad(doc, astDoc.MemberLoads[memberLoadIndex]);
                }
                catch (Exception exx)
                {
                }
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);
                #endregion

                #region Open Member Joint Load Form
                OnFileNew();

                form = this.ActiveMdiChild as Childform;
                form.Name = "frmMemberJointLoad";
                form.Text = "MemberJointLoad.vdml";
                doc = form.vdScrollableControl1.BaseControl.ActiveDocument;
                memberJointLoadDoc = doc;
                memberJointLoadDoc.ShowUCSAxis = false;
                int jlIndex = -1;
                astDoc.Members.DrawMember(astDoc.Members[memberIndex], doc);
                jlIndex = astDoc.JointLoads.IndexOf(astDoc.Members[memberIndex].EndNode.NodeNo);
                try
                {
                    astDoc.JointLoads.DrawJointLoads(doc, astDoc.JointLoads[jlIndex]);
                }
                catch (Exception ex) { }

                jlIndex = astDoc.JointLoads.IndexOf(astDoc.Members[memberIndex].StartNode.NodeNo);
                try
                {
                    astDoc.JointLoads.DrawJointLoads(doc, astDoc.JointLoads[jlIndex]);
                }
                catch (Exception ex) { }


                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);
                #endregion

                #region Calculate Windows Location And Size

                panel2.Width = 210; // Left Side Property Box
                double x, y, w, h;
                x = y = w = h = 0.0d;
                x = panel2.Location.X + panel2.Width;
                y = panel2.Location.Y;
                w = this.Width - RightToolbarsPanel.Width * 1.5 - panel2.Width;
                h = panel2.Height;

                this.MdiChildren[0].Location = new Point(0, 0);
                this.MdiChildren[0].Size = new Size((int)w * 2 / 3, (int)h - 60);
                this.MdiChildren[0].WindowState = FormWindowState.Normal;

                form = this.MdiChildren[0] as Childform;
                doc = form.vdScrollableControl1.BaseControl.ActiveDocument;
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);


                this.MdiChildren[1].Location = new Point((int)w * 2 / 3, 0);
                this.MdiChildren[1].Size = new Size((int)w / 3, (int)h / 3);
                this.MdiChildren[1].WindowState = FormWindowState.Normal;

                this.MdiChildren[2].Location = new Point((int)w * 2 / 3, (int)(h / 3));
                this.MdiChildren[2].Size = new Size((int)w / 3, (int)(h / 3));
                this.MdiChildren[2].WindowState = FormWindowState.Normal;

                this.MdiChildren[3].Location = new Point((int)w * 2 / 3, (int)(h / 3) * 2);
                this.MdiChildren[3].Size = new Size((int)w / 3, (int)h / 3);
                this.MdiChildren[3].WindowState = FormWindowState.Normal;

                #endregion

                for (int k = 0; k < this.MdiChildren.Length; k++)
                {
                    form = this.MdiChildren[k] as Childform;
                    doc = form.vdScrollableControl1.BaseControl.ActiveDocument;
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);
                }
            }
            catch (Exception exx)
            {
            }
        }
        public void DrawMemberWithOutLoadCase(vdDocument doc, string fName)
        {
            if (fName == null) return;
            string ext = Path.GetExtension(fName).ToLower();
            if (ext != ".txt" && ext != ".ast") return;

            Childform form1 = this.ActiveMdiChild as Childform;
            StructureFormCount++;

            LastStructureFormName = "frmStructure" + StructureFormCount;
            form1.Name = LastStructureFormName;
            form1.Text = "ASTRA Structure Model" + StructureFormCount + ".vdml";
            if (form1 != null)
            {
                astDoc.Members.DrawMember(doc);
                astDoc.Supports.DrawSupport(doc);
                //astDoc.Joints.DrawJointsText(doc,astDoc.Members.GetTextSize());

                astDoc.Elements.DrawElements(doc);
                form1.Text = "ASTRA Structure Model" + StructureFormCount + ".vdml";

                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);
                fName = null;
            }
        }
        public void DrawMemberWithOutLoadCase()
        {
            //fName = System.Environment.GetEnvironmentVariable("ASTRA");
            if (fName == null)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "TEXT Files, AST Files|*.txt;*.ast";
                ofd.Title = "SELECT ASTRA INPUT File";
                if (ofd.ShowDialog() != DialogResult.Cancel)
                {
                    fName = ofd.FileName;
                    astDoc = new HEADSNeed.ASTRA.ASTRAClasses.ASTRADoc(fName);
                    commandLine.ExecuteCommand("new");
                }
                else
                {
                    fName = null;
                    return;
                }
            }

            string ext = Path.GetExtension(fName).ToLower();
            if (ext != ".txt" && ext != ".ast") return;

            Childform form1 = this.ActiveMdiChild as Childform;
            StructureFormCount++;

            LastStructureFormName = "frmStructure" + StructureFormCount;
            form1.Name = LastStructureFormName;
            form1.Text = "ASTRA Structure Model" + StructureFormCount + ".vdml";
            if (form1 != null)
            {
                VectorDraw.Professional.vdObjects.vdDocument doc = form1.vdScrollableControl1.BaseControl.ActiveDocument;

                Propertiesform.Initialize(doc, GetResourcesPath(), this.panel2);
                Propertiesform.LoadProps(null, doc, true);
                FixSide(Propertiesform.mDocumentProperties.mSide);

                //astDoc = new HEADSNeed.ASTRA.ASTRAClasses.ASTRADoc(fName);
                astDoc.Members.DrawMember(doc);
                astDoc.Supports.DrawSupport(doc);
                astDoc.Elements.DrawElements(doc);
                form1.Text = "ASTRA Structure Model" + StructureFormCount + ".vdml";

                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);
                //fName = null;
            }
        }
        public void DrawMember()
        {
            //fName = System.Environment.GetEnvironmentVariable("ASTRA");
            if (fName == null)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "TEXT Files, AST Files|*.txt;*.ast";
                ofd.Title = "SELECT ASTRA INPUT File";
                if (ofd.ShowDialog() != DialogResult.Cancel)
                {
                    fName = ofd.FileName;
                    commandLine.ExecuteCommand("new");
                }
                else
                {
                    fName = null;
                    return;
                }
            }

            string ext = Path.GetExtension(fName).ToLower();
            if (ext != ".txt" && ext != ".ast") return;

            Childform form1 = this.ActiveMdiChild as Childform;
            StructureFormCount++;

            LastStructureFormName = "frmStructure" + StructureFormCount;
            form1.Name = LastStructureFormName;
            form1.Text = "ASTRA Structure Model" + StructureFormCount + ".vdml";
            if (form1 != null)
            {
                VectorDraw.Professional.vdObjects.vdDocument doc = form1.vdScrollableControl1.BaseControl.ActiveDocument;

                Propertiesform.Initialize(doc, GetResourcesPath(), this.panel2);
                Propertiesform.LoadProps(null, doc, true);
                FixSide(Propertiesform.mDocumentProperties.mSide);

                //astDoc = new HEADSNeed.ASTRA.ASTRAClasses.ASTRADoc(fName);
                astDoc.Members.DrawMember(doc);
                astDoc.Supports.DrawSupport(doc);
                astDoc.JointLoads.DrawJointLoads(doc);
                astDoc.MemberLoads.DrawMemberLoad(doc);
                astDoc.Elements.DrawElements(doc);
                form1.Text = "ASTRA Structure Model" + StructureFormCount + ".vdml";

                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);
                fName = null;
            }
        }
        public void DrawMemberWithLoadCase(int LoadCase)
        {
            DrawASTRAStructureLoad(mainDoc, LoadCase);
        }
        public void DrawASTRAStructureLoad(vdDocument doc, int LoadCase)
        {
            try
            {
                mainDoc = doc;
                astDoc.JointLoads.DrawJointLoads(doc, LoadCase);
                astDoc.MemberLoads.DrawMemberLoad(doc, LoadCase);
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);
            }
            catch (Exception exx) { }
        }

        public void DrawStructure()
        {
            CloseAstraWindow();
            Childform form = this.ActiveMdiChild as Childform;
            form.Work = Childform.WORK.Structure;
            VectorDraw.Professional.vdObjects.vdDocument doc = form.vdScrollableControl1.BaseControl.ActiveDocument;

            DrawMemberWithOutLoadCase(doc, fName);
            try
            {
                frmPostProc.Close();
            }
            catch (Exception ex)
            {
            }
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(doc);
            mainDoc = doc;
            ASTRA_StructureForm();


            if (astDoc.Elements.Count > 0)
                mainDoc.RenderMode = VectorDraw.Render.vdRender.Mode.Render;
            ASTRAMenuSetup();
            ASTRALoadingMenu.Checked = false;
            ASTRALoadingMenu.Enabled = true;
            ASTRAAnalysisMenu.Enabled = true;

            mainDoc.Redraw(true);

            //mainDoc.CommandAction.View3D("vrot");
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(mainDoc);
            //commandLine.ExecuteCommand("View3DVrot");
            //MessageBox.Show("Pressing Left Mouse button Rotate the Structure.", "ASTRA");
        }
        public void ASTRAMenuSetup()
        {
            string ana_file = "";
            string dir = "";
            try
            {
                dir = Path.GetDirectoryName(astDoc.FileName);
                ana_file = Path.Combine(dir, "ANALYSIS_REP.TXT");
                if(!File.Exists(ana_file))
                    ana_file = Path.Combine(dir, "analysis_rep.txt");

                ASTRALoadingMenu.Enabled = (astDoc.MemberLoads.Count > 0 || astDoc.JointLoads.Count > 0);
                ASTRAPostProcessMenu.Enabled = ((File.Exists(ana_file) && !astDoc.IsDynamicLoad));
                ASTRAReportMenu.Enabled = (File.Exists(ana_file));
                ASTRAAnalysisMenu.Enabled = (File.Exists(Path.Combine(Application.StartupPath, "ast001.exe")));
                ASTRAStructureMenu.Enabled = (fName != null || fName != "");
                ASTRATextSizeMenu.Enabled = (fName != null || fName != "");
                //ASTRABeamAnalysisMenu.Enabled = File.Exists(Path.Combine(Path.GetDirectoryName(fName), "test.rep"));
                //ASTRALoadDeflectionMenu.Enabled = File.Exists(Path.Combine(Path.GetDirectoryName(astDoc.FileName), "MODEL.FIL"));
                //ASTRALoadDeflectionMenu.Enabled = File.Exists(Path.Combine(Path.GetDirectoryName(astDoc.FileName), "ANALYSIS_REP.TXT"));
                ASTRALoadDeflectionMenu.Enabled = File.Exists(ana_file);

            }
            catch (Exception exx) { }
        }
        public void AstrA_Moving_LOAD_Deflection()
        {

            //Chiranjit [2011 09 16]


            //MovingLoadAnalysis.frmMoving f_mo = new MovingLoadAnalysis.frmMoving(VDoc, astDoc.FileName);
            //f_mo.Owner = this;
            //f_mo.Show();
            //return;

            //LoadDeflection ld = new LoadDeflection(astDoc);

            vdDocument defDoc = null;
            CloseAstraWindow();

            if (this.MdiChildren.Length > 1)
            {
                #region Close All Window

                for (int ic = 0; ic < this.MdiChildren.Length; ic++)
                {
                    if (this.MdiChildren[ic].Name == LastStructureFormName)
                    {
                        continue;
                    }
                    else
                    {
                        this.MdiChildren[ic].Close();
                        ic = -1;
                    }
                }
                #endregion
            }

            if (this.MdiChildren.Length == 1 && !astDoc.IsMovingLoad)
            {
                OnFileNew();
                Childform chld = this.MdiChildren[1] as Childform;
                defDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            }

            if (!astDoc.IsMovingLoad)
            {
                #region Calculate Windows Location And Size

                panel2.Width = 210; // Left Side Property Box
                double x, y, w, h;
                x = y = w = h = 0.0d;
                x = panel2.Location.X + panel2.Width;
                y = panel2.Location.Y;
                w = this.Width - RightToolbarsPanel.Width * 1.5 - panel2.Width;
                h = panel2.Height;

                this.MdiChildren[0].Location = new Point(0, 0);
                this.MdiChildren[0].Size = new Size((int)w * 1 / 2, (int)h);
                this.MdiChildren[0].WindowState = FormWindowState.Normal;

                Childform form = this.MdiChildren[0] as Childform;
                mainDoc = form.vdScrollableControl1.BaseControl.ActiveDocument;
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(mainDoc);


                this.MdiChildren[1].Location = new Point((int)w * 1 / 2, 0);
                this.MdiChildren[1].Size = new Size((int)w * 1 / 2, (int)h);
                this.MdiChildren[1].WindowState = FormWindowState.Normal;



                #endregion
            }
            else
            {
                #region Calculate Windows Location And Size

                panel2.Width = 210; // Left Side Property Box
                double x, y, w, h;
                x = y = w = h = 0.0d;
                x = panel2.Location.X + panel2.Width;
                y = panel2.Location.Y;
                w = this.Width - RightToolbarsPanel.Width * 1.5 - panel2.Width;
                h = panel2.Height;

                this.MdiChildren[0].Location = new Point(0, 0);
                this.MdiChildren[0].Size = new Size((int)w * 1 / 2, (int)h);
                this.MdiChildren[0].WindowState = FormWindowState.Normal;

                Childform form = this.MdiChildren[0] as Childform;
                mainDoc = form.vdScrollableControl1.BaseControl.ActiveDocument;
                VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(mainDoc);


                //this.MdiChildren[1].Location = new Point((int)w * 1 / 2, 0);
                //this.MdiChildren[1].Size = new Size((int)w * 1 / 2, (int)h);
                //this.MdiChildren[1].WindowState = FormWindowState.Normal;



                #endregion
            }

            astDoc.Members.DrawMember(mainDoc);
            astDoc.Supports.DrawSupport(mainDoc);

            //if (astDoc.IsMovingLoad)
            //{
            //    for (int i = 0; i < mainDoc.ActiveLayOut.Entities.Count; i++)
            //    {
            //        vdText vtxt = mainDoc.ActiveLayOut.Entities[i] as vdText;
            //        if (vtxt != null)
            //        {
            //            vtxt.ExtrusionVector = new VectorDraw.Geometry.Vector(0, -1, 0);
            //        }
            //    }
            //    this.MdiChildren[0].WindowState = FormWindowState.Maximized;
            //}


            this.LayoutMdi(MdiLayout.TileVertical);
            if (astDoc.IsMovingLoad)
            {
                this.MdiChildren[0].Text = "Moving Load Analysis";
                this.MdiChildren[0].WindowState = FormWindowState.Maximized;
            }

            if (astDoc.IsMovingLoad == false)
                astDoc.MemberLoads.DrawMemberLoad(mainDoc);

            frmASTRA_MovingLoadDef = new frmASTRAMovingLoadDeflections(astDoc, defDoc, mainDoc, this.MdiChildren);
            frmASTRA_MovingLoadDef.Owner = this;
            frmASTRA_MovingLoadDef.StartPosition = FormStartPosition.Manual;
            //frmASTRA_LoadDef.Location = new Point(this.Width / 2 - frmASTRA_LoadDef.Width / 2, this.Height - frmASTRA_LoadDef.Height - toolStrip1.Height);
            //frmASTRA_MovingLoadDef.Location = new Point(this.panel2.Width, this.Height - frmASTRA_MovingLoadDef.Height - toolStrip1.Height);

            double ix = (Width - frmASTRA_MovingLoadDef.Width) / 2.0;
            
            frmASTRA_MovingLoadDef.Location = new Point((int) ix, this.Height - frmASTRA_MovingLoadDef.Height - toolStrip1.Height);
            frmASTRA_MovingLoadDef.Show();


            string analysisFileName = "";
            analysisFileName = Path.GetDirectoryName(astDoc.FileName);
            analysisFileName = Path.Combine(analysisFileName, "Memberforce.txt");

            //if (!File.Exists(analysisFileName)) return;
            if (this.MdiChildren.Length == 2)
            {
                this.MdiChildren[0].Height -= 180;
                this.MdiChildren[1].Height -= 180;
            }

            //if (astDoc.IsMovingLoad)
            //{
            //    frmMovingForces frm1 = new frmMovingForces(mainDoc, astDoc.FileName);
            //    frm1.Owner = this;
            //    frm1.Size = new Size(242, 602);
            //    //frm.Location = new Point(vdPropertyGrid1.Width + 5, 70 + toolStrip1.Height + this.MdiChildren[0].Height);
            //    this.MdiChildren[0].Width -= frm1.Width;
                
            //    //frm1.Location = new Point(vdPropertyGrid1.Width + this.MdiChildren[0].Width, toolStrip1.Height);
            //    frm1.Location = new Point(vdPropertyGrid1.Width + this.MdiChildren[0].Width, this.Height - frmASTRA_MovingLoadDef.Height - toolStrip1.Height - this.MdiChildren[0].Height);
            //    frmASTRA_MovingLoadDef.Report_Form = frm1;
            //    frm1.Show();
            //    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(mainDoc);
            //}
        }

        //Chiranjit [2011 09 26]  New Load deflection Model
        public void AstrA_LOAD_Deflection()
        {
            //LoadDeflection ld = new LoadDeflection(astDoc);

            vdDocument defDoc = null;
            CloseAstraWindow();

            if (this.MdiChildren.Length > 2)
            {
                #region Close All Window

                for (int ic = 0; ic < this.MdiChildren.Length; ic++)
                {
                    if (this.MdiChildren[ic].Name == LastStructureFormName)
                    {
                        continue;
                    }
                    else
                    {
                        this.MdiChildren[ic].Close();
                        ic = -1;
                    }
                }
                #endregion
            }

            if (this.MdiChildren.Length == 1)
            {
                OnFileNew();
                Childform chld = this.MdiChildren[1] as Childform;
                defDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            }

            #region Calculate Windows Location And Size

            panel2.Width = 210; // Left Side Property Box
            double x, y, w, h;
            x = y = w = h = 0.0d;
            x = panel2.Location.X + panel2.Width;
            y = panel2.Location.Y;
            w = this.Width - RightToolbarsPanel.Width * 1.5 - panel2.Width;
            h = panel2.Height;

            this.MdiChildren[0].Location = new Point(0, 0);
            this.MdiChildren[0].Size = new Size((int)w * 1 / 2, (int)h);
            this.MdiChildren[0].WindowState = FormWindowState.Normal;

            Childform form = this.MdiChildren[0] as Childform;
            mainDoc = form.vdScrollableControl1.BaseControl.ActiveDocument;
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(mainDoc);


            this.MdiChildren[1].Location = new Point((int)w * 1 / 2, 0);
            this.MdiChildren[1].Size = new Size((int)w * 1 / 2, (int)h);
            this.MdiChildren[1].WindowState = FormWindowState.Normal;

            //this.MdiChildren[2].Location = new Point((int)w * 3 / 5, (int)(h / 3));
            //this.MdiChildren[2].Size = new Size((int)w * 2 / 5, (int)(h / 3));
            //this.MdiChildren[2].WindowState = FormWindowState.Normal;

            //this.MdiChildren[3].Location = new Point((int)w * 3 / 5, (int)(h / 3) * 2);
            //this.MdiChildren[3].Size = new Size((int)w * 2 / 5, (int)h / 3);
            //this.MdiChildren[3].WindowState = FormWindowState.Normal;

            #endregion

            astDoc.Members.DrawMember(mainDoc);
            astDoc.Supports.DrawSupport(mainDoc);

            if (astDoc.IsMovingLoad)
            {
                for (int i = 0; i < mainDoc.ActiveLayOut.Entities.Count; i++)
                {
                    vdText vtxt = mainDoc.ActiveLayOut.Entities[i] as vdText;
                    if (vtxt != null)
                    {
                        vtxt.ExtrusionVector = new VectorDraw.Geometry.Vector(0, -1, 0);
                    }

                }
            }


            //astDoc.JointLoads.DrawJointLoads(mainDoc);

            //if (astDoc.IsMovingLoad)
            //    this.LayoutMdi(MdiLayout.TileHorizontal);
            //else
            //    this.LayoutMdi(MdiLayout.TileVertical);
            this.LayoutMdi(MdiLayout.TileVertical);
            if (astDoc.IsMovingLoad)
            {
                this.MdiChildren[0].Text = "PLAN View";
                this.MdiChildren[1].Text = "ELEVATION";
            }


            astDoc.MemberLoads.DrawMemberLoad(mainDoc);
            frmASTRA_LoadDef = new frmASTRALoadDeflections(astDoc, defDoc, mainDoc, this.MdiChildren);
            frmASTRA_LoadDef.Owner = this;
            frmASTRA_LoadDef.StartPosition = FormStartPosition.Manual;
            frmASTRA_LoadDef.Location = new Point(this.Width / 2 - frmASTRA_LoadDef.Width / 2, this.Height - frmASTRA_LoadDef.Height - toolStrip1.Height);
            frmASTRA_LoadDef.Show();


            string analysisFileName = "";
            analysisFileName = Path.GetDirectoryName(astDoc.FileName);
            //analysisFileName = Path.Combine(analysisFileName, "Memberforce.FIL");
            analysisFileName = astDoc.AnalysisFileName;

            if (!File.Exists(analysisFileName)) return;
          

            string temp_file = "";

            temp_file = Path.Combine(Path.GetDirectoryName(astDoc.FileName), "rep.tmp");

            StreamWriter sw = new StreamWriter(new FileStream(temp_file, FileMode.Create));
            StreamReader sr = new StreamReader(new FileStream(analysisFileName, FileMode.Open));

            string str = "";
            string kstr = "";
            bool flag = false;

            bool truss_title = false;
            bool beam_title = false;
            bool cable_title = false;
            try
            {
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("Taken from file ANALYSIS_REP.TXT");
                //sw.WriteLine("       Member   Load     Node    AXIAL       SHEAR       SHEAR     TORSION     BENDING     BENDING");
                //sw.WriteLine("       No:      Case     Nos:       R1          R2          R3          M1          M2          M3");
                sw.WriteLine();
                sw.WriteLine();
                while (!sr.EndOfStream)
                {
                    str = sr.ReadLine().ToUpper();
                    kstr = MovingLoadAnalysis.MyList.RemoveAllSpaces(str);

                    //str = str.Replace("\t\t", "\t    ");
                    if (str.Contains("TRUSS MEMBER ACTIONS") )
                    {
                        flag = true;
                        if (!truss_title)
                        {
                            sw.WriteLine("TRUSS MEMBER ACTIONS");
                            sw.WriteLine();
                            sw.WriteLine("    MEMBER      LOAD         STRESS              FORCE");
                            sw.WriteLine();
                            truss_title = true;
                        }
                        continue;
                    }
                    if (str.Contains("BEAM FORCES AND MOMENTS"))
                    {
                        flag = true;
                        if (!beam_title)
                        {
                            sw.WriteLine("BEAM FORCES AND MOMENTS");
                            sw.WriteLine();
                            sw.WriteLine(" BEAM LOAD     AXIAL       SHEAR       SHEAR     TORSION     BENDING     BENDING");
                            sw.WriteLine("  NO.  NO.        R1          R2          R3          M1          M2          M3");
                            sw.WriteLine();
                            beam_title = true;
                        }
                        continue;
                    }
                    if (str.Contains("CABLE MEMBER ACTIONS"))
                    {
                        flag = true;
                        if (!cable_title)
                        {
                            sw.WriteLine("CABLE MEMBER ACTIONS");
                            sw.WriteLine();
                            sw.WriteLine("    MEMBER      LOAD         STRESS              FORCE");
                            sw.WriteLine();
                            cable_title = true;
                        }
                        continue;
                    }
                    if (flag && !str.Contains("*") && str.Length > 9)
                    {
                        if ((kstr != "MEMBER LOAD STRESS FORCE") ||
                            (kstr != ".....BEAM FORCES AND MOMENTS"))
                        sw.WriteLine(str);
                    }
                }
            }
            catch (Exception ex) { }
            finally
            {
                sw.Flush();
                sw.Close();
                sr.Close();
            }

            return;
            //list.Clear();
            if (File.Exists(temp_file))
            {
                List<string> ll = new List<string>(File.ReadAllLines(temp_file));


                if (ll.Count < 6) return;

                try
                {
                    frm.Close();
                }
                catch (Exception exx)
                {
                }
                frm = new frmASTRAReport(temp_file);
                frm.Owner = this;
                frm.Size = new Size(this.MdiChildren[0].Width * 2, 220);
                //frm.Size = new Size(this.Width, this.Height - 130);
                frm.Location = new Point(vdPropertyGrid1.Width + 5, 70 + toolStrip1.Height + this.MdiChildren[0].Height);
                //frm.Location = new Point(vdPropertyGrid1.Width + 5, this.Height - frm.Height - toolStrip1.Height);
                //frm.Size = new Size(this.MdiChildren[0].Size.Width, 60);


                frm.Location = new Point(217, nodeGrid.Location.Y + MdiChildren[0].Size.Height - 18);
                frmASTRA_LoadDef.Report_Form = frm;
                frm.Show();

                if (this.MdiChildren.Length == 2)
                {
                    this.MdiChildren[0].Height -= 180;
                    this.MdiChildren[1].Height -= 180;
                }
            }
            else
            {
                //MessageBox.Show("'ANALYSIS_REP.TXT' file not found in working folder.", "ASTRA",
                //    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
       
        public void CloseAstraWindow()
        {
            try
            {
                fTxtSize.Close();
            }
            catch (Exception ex)
            {
            }
            try
            {
                nodeGrid.Close();
            }
            catch (Exception ex)
            {
            }
            try
            {
                memGrid.Close();
            }
            catch (Exception ex)
            {
            }
            try
            {
                frm.Close();
            }
            catch (Exception ex)
            {
            }
            try
            {
                frmPostProc.Close();
            }
            catch (Exception ex)
            {
            }
            try
            {
                frmMemLoadCase.Close();
            }
            catch (Exception ex)
            {
            }
            try
            {
                frmASTRA_MovingLoadDef.Close();
            }
            catch (Exception ex)
            {
            }
            try
            {
                frm.Close();
            }
            catch (Exception ex)
            {
            }

        }


        #region SLAB01
        public void RccSlab01_View(string design_file)
        {

            //frmSLAB01 fSlb01 = new frmSLAB01(astDoc.FileName, mainDoc);
            //fSlb01.Owner = this;
            //fSlb01.Show();

            Childform chld = this.MdiChildren[0] as Childform;

            SLAB01 sl01 = new SLAB01(chld.vdScrollableControl1.BaseControl.ActiveDocument);

            sl01.ReadFromFilFile(design_file);

            sl01.DrawSlab01(sl01.TotalLength, sl01.B, sl01.MainSpace, sl01.DistributedSpace, sl01.H, sl01.H);
        }
        public void RccSlab01_Drawing(string design_file)
        {

        }
        public void RccSlab01_BoQ(string design_file)
        {

        }
        #endregion

        #region ONE_WAY_RCC_SLAB
        public void Rcc_Slab_One_Way_View(string design_file)
        {
            //frmSLAB02 fSlb02 = new frmSLAB02(astDoc.FileName);
            //fSlb02.Owner = this;
            //fSlb02.Show();

            Childform chld = this.MdiChildren[0] as Childform;
            SLAB02 sl02 = new SLAB02();


            system_path = Path.GetDirectoryName(design_file);
            file_path = Path.GetDirectoryName(system_path);

            sl02.SLAB02_View(chld.vdScrollableControl1.BaseControl.ActiveDocument, design_file);
            string save_file_name = Path.Combine(file_path, "View_Rcc_Slab_One_Way.VDML");

            chld.vdScrollableControl1.BaseControl.ActiveDocument.SaveAs(save_file_name);
            chld.vdScrollableControl1.BaseControl.ActiveDocument.ClearAll();
            chld.vdScrollableControl1.BaseControl.ActiveDocument.Open(save_file_name);


        }
        public void Rcc_Slab_One_Way_Drawing(string design_file)
        {
            //frmSLAB02 fSlb02 = new frmSLAB02(astDoc.FileName);
            //fSlb02.Owner = this;
            //fSlb02.Show();

            Childform chld = this.MdiChildren[0] as Childform;
            SLAB02 sl02 = new SLAB02();

            system_path = Path.GetDirectoryName(design_file);
            file_path = Path.GetDirectoryName(system_path);
            string file_name = Path.Combine(system_path, "DRAWING.FIL");
            sl02.Slab_Drawing(chld.vdScrollableControl1.BaseControl.ActiveDocument, file_name);
            file_name = Path.Combine(file_path, "Drawing_Rcc_Slab_One_Way.VDML");
            chld.vdScrollableControl1.BaseControl.ActiveDocument.SaveAs(file_name);
            chld.vdScrollableControl1.BaseControl.ActiveDocument.ClearEraseItems();
            chld.vdScrollableControl1.BaseControl.ActiveDocument.Open(file_name);

        }
        public void Rcc_Slab_One_Way_BoQ(string design_file)
        {
            //frmSLAB02 fSlb02 = new frmSLAB02(astDoc.FileName);
            //fSlb02.Owner = this;
            //fSlb02.Show();

            Childform chld = this.MdiChildren[0] as Childform;
            SLAB02 sl02 = new SLAB02();


            system_path = Path.GetDirectoryName(design_file);
            file_path = Path.GetDirectoryName(system_path);
            string boq_file = Path.Combine(system_path, "BoQ.FIL");
            //sl02.Slab_Drawing(chld.vdScrollableControl1.BaseControl.ActiveDocument, str);
            sl02.Slab_Drawing_BoQ(chld.vdScrollableControl1.BaseControl.ActiveDocument, boq_file);

            boq_file = Path.Combine(file_path, "BoQ_Rcc_Slab_One_Way.VDML");
            chld.vdScrollableControl1.BaseControl.ActiveDocument.SaveAs(boq_file);
            chld.vdScrollableControl1.BaseControl.ActiveDocument.ClearEraseItems();
            chld.vdScrollableControl1.BaseControl.ActiveDocument.Open(boq_file);

        }
        #endregion

        #region Rcc_Slab_One_Way_Continuous
        public void Rcc_Slab_One_Way_Continuous_View(string design_file)
        {

            Childform chld = this.MdiChildren[0] as Childform;
            SLAB03 sl03 = new SLAB03(design_file);


            system_path = Path.GetDirectoryName(design_file);
            file_path = Path.GetDirectoryName(system_path);


            sl03.Draw_View_Multy_Span(chld.vdScrollableControl1.BaseControl.ActiveDocument);

            design_file = Path.Combine(file_path, "View_Rcc_Slab_One_Way_Continuous.VDML");
            chld.vdScrollableControl1.BaseControl.ActiveDocument.SaveAs(design_file);
            chld.vdScrollableControl1.BaseControl.ActiveDocument.ClearEraseItems();
            chld.vdScrollableControl1.BaseControl.ActiveDocument.Open(design_file);

        }
        public void Rcc_Slab_One_Way_Continuous_Drawing(string design_file)
        {
            //frmSLAB02 fSlb02 = new frmSLAB02(astDoc.FileName);
            //fSlb02.Owner = this;
            //fSlb02.Show();


            system_path = Path.GetDirectoryName(design_file);
            file_path = Path.GetDirectoryName(system_path);

            Childform chld = this.MdiChildren[0] as Childform;
            SLAB03 sl03 = new SLAB03(design_file);

            sl03.Draw_Drawing(chld.vdScrollableControl1.BaseControl.ActiveDocument);

            design_file = Path.Combine(file_path, "Drawing_Rcc_Slab_One_Way_Continuous.VDML");
            chld.vdScrollableControl1.BaseControl.ActiveDocument.SaveAs(design_file);
            chld.vdScrollableControl1.BaseControl.ActiveDocument.ClearEraseItems();
            chld.vdScrollableControl1.BaseControl.ActiveDocument.Open(design_file);

        }
        public void Rcc_Slab_One_Way_Continuous_BoQ(string design_file)
        {
            //string file = "C:\\slab03.s03";

            system_path = Path.GetDirectoryName(design_file);
            file_path = Path.GetDirectoryName(system_path);

            string file = Path.Combine(Application.StartupPath, @"DRAWINGS\Slab\BoQ_Rcc_Slab_One_Way_Continuous.VDML");
            Childform chld = this.ActiveMdiChild as Childform;
         
            chld.vdScrollableControl1.BaseControl.ActiveDocument.ClearAll();
            chld.vdScrollableControl1.BaseControl.ActiveDocument.ClearEraseItems();
            chld.vdScrollableControl1.BaseControl.ActiveDocument.Open(file);


            file = Path.Combine(system_path, "BoQ.FIL");
            HEADSNeed.ASTRA.ASTRAClasses.CodeSnap.CopyCoordinates.Set_Drawing_File(file,
             chld.vdScrollableControl1.BaseControl.ActiveDocument);


            design_file = Path.Combine(file_path, "BoQ_Rcc_Slab_One_Way_Continuous.VDML");
            chld.vdScrollableControl1.BaseControl.ActiveDocument.SaveAs(design_file);
            chld.vdScrollableControl1.BaseControl.ActiveDocument.ClearEraseItems();
            chld.vdScrollableControl1.BaseControl.ActiveDocument.Open(design_file);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(chld.vdScrollableControl1.BaseControl.ActiveDocument);
            chld.vdScrollableControl1.BaseControl.ActiveDocument.Redraw(true);

        }
        #endregion

        #region Rcc_Slab_Two_Way
        public void Rcc_Slab_Two_Way_View(string design_file)
        {
            try
            {
                CloseAllDocs();
            }
            catch (Exception ex)
            {
            }
            system_path = Path.GetDirectoryName(design_file);
            file_path = Path.GetDirectoryName(system_path);
            string k_file = Path.Combine(system_path, "DRAWING.FIL");
            SLAB04 slab04 = new SLAB04(k_file);

            Childform chld = this.MdiChildren[0] as Childform;
            chld = this.MdiChildren[0] as Childform;
            //chld.Text = "SLAB_03 VIEW";

           
            slab04.Draw_View(chld.vdScrollableControl1.BaseControl.ActiveDocument, design_file);

            design_file = Path.Combine(file_path, "View_Rcc_Slab_Two_Way.VDML");

            chld.vdScrollableControl1.BaseControl.ActiveDocument.SaveAs(design_file);
            chld.vdScrollableControl1.BaseControl.ActiveDocument.ClearEraseItems();
            chld.vdScrollableControl1.BaseControl.ActiveDocument.Open(design_file);




            //slab04.Draw_View(chld.vdScrollableControl1.BaseControl.ActiveDocument, SLAB04.PanelMoment_Type.FOUR_EDGES_DISCONTINUOUS);
            //this.LayoutMdi(MdiLayout.TileVertical);

        }
        public void Rcc_Slab_Two_Way_Drawing(string design_file)
        {
            system_path = Path.GetDirectoryName(design_file);
            file_path = Path.GetDirectoryName(system_path);

            //string file_save = Path.Combine(file_path, "TWO_WAY_RCC_SLAB_TOP_VIEW.VDML");
            string file_save = Path.Combine(file_path, "Drawing_Rcc_Slab_Two_Way_Top_View.VDML");
            try
            {
                CloseAllDocs();
            }
            catch (Exception ex)
            {
            }
            //string k_file = Path.Combine(Application.StartupPath, "slab04.drw");
            string k_file = Path.Combine(system_path, "DRAWING.FIL");

            //file_save = Path.Combine(file_path, "TWO_WAY_RCC_SLAB_SECTION_VIEW.VDML");
            file_save = Path.Combine(file_path, "Drawing_Rcc_Slab_Two_Way_Section_View.VDML");
            SLAB04 slab04 = new SLAB04(k_file);
            Childform chld = this.MdiChildren[0] as Childform;
            chld = this.MdiChildren[0] as Childform;
            chld.Text = "Section_Along_Long_Span";
            slab04.Draw_Section_Along_Long_Span(chld.vdScrollableControl1.BaseControl.ActiveDocument);
            chld.vdScrollableControl1.BaseControl.ActiveDocument.SaveAs(file_save);



            file_save = Path.Combine(file_path, "Drawing_Rcc_Slab_Two_Way_Top_View.VDML");
            OnFileNew();
            chld = this.MdiChildren[1] as Childform;
            chld.Text = "TOP VIEW";
            slab04.Draw_View_Top(chld.vdScrollableControl1.BaseControl.ActiveDocument);
            chld.vdScrollableControl1.BaseControl.ActiveDocument.SaveAs(file_save);

            file_save = Path.Combine(file_path, "Drawing_Rcc_Slab_Two_Way_Bottom_View.VDML");
            OnFileNew();
            chld = this.MdiChildren[2] as Childform;
            chld.Text = "BOTTOM VIEW";
            slab04.Draw_View_Bottom(chld.vdScrollableControl1.BaseControl.ActiveDocument);
            chld.vdScrollableControl1.BaseControl.ActiveDocument.SaveAs(file_save);

            this.LayoutMdi(MdiLayout.TileHorizontal);
        }
        public void Rcc_Slab_Two_Way_BoQ(string design_file)
        {

            system_path = Path.GetDirectoryName(design_file);
            file_path = Path.GetDirectoryName(system_path);

            //string file = "C:\\slab04.s04";
            string file = Path.Combine(Application.StartupPath, @"DRAWINGS\Slab\BoQ_Rcc_Slab_Two_Way.VDML");
            Childform chld = this.ActiveMdiChild as Childform;
            chld.vdScrollableControl1.BaseControl.ActiveDocument.ClearEraseItems();
            chld.vdScrollableControl1.BaseControl.ActiveDocument.ClearAll();
            chld.vdScrollableControl1.BaseControl.ActiveDocument.Open(file);

            file = Path.Combine(system_path, "BoQ.FIL");
            HEADSNeed.ASTRA.ASTRAClasses.CodeSnap.CopyCoordinates.Set_Drawing_File(file,
             chld.vdScrollableControl1.BaseControl.ActiveDocument);

            design_file = Path.Combine(file_path, "BoQ_Rcc_Slab_Two_Way.VDML");
            chld.vdScrollableControl1.BaseControl.ActiveDocument.SaveAs(design_file);
            chld.vdScrollableControl1.BaseControl.ActiveDocument.ClearEraseItems();
            chld.vdScrollableControl1.BaseControl.ActiveDocument.Open(design_file);

        }
        #endregion

        #region Copy Coorinates
        public void WriteCoorinatesToFile()
        {
            //string file = "C:\\slab03.s03";
            string file = "C:\\LinePoints.txt";

            Childform chld = this.ActiveMdiChild as Childform;
            HEADSNeed.ASTRA.ASTRAClasses.CodeSnap.CopyCoordinates.WriteLineCoordinates(file,
                chld.vdScrollableControl1.BaseControl.ActiveDocument);
        }
        public void ReadCoorinatesFromFile()
        {
            //string file = "C:\\slab04.s04";
            ////string file = Path.Combine(Application.StartupPath, "slab03.s03");
            //Childform chld = this.ActiveMdiChild as Childform;
            //HEADSNeed.ASTRA.ASTRAClasses.CodeSnap.CopyCoordinates.ReadLineCoordinates(file,
            //    chld.vdScrollableControl1.BaseControl.ActiveDocument);

            //file = @"C:\Documents and Settings\TECHSOFT\Desktop\ASTRA TEST\DESIGN_SLAB04\slab04.boq";
            //HEADSNeed.ASTRA.ASTRAClasses.CodeSnap.CopyCoordinates.Draw_Boq_Text_Slab04(file,
            // chld.vdScrollableControl1.BaseControl.ActiveDocument);
        }
        #endregion

        public void LoadingOn()
        {
            CloseAstraWindow();
            if (astDoc.IsMovingLoad)
            {
                if (astDoc.JointLoads.SetMovingJointLoad(astDoc.FileName))
                    astDoc.JointLoads.CopyCoordinates(astDoc.Joints);
            }

            if (!File.Exists(astDoc.FileName)) return;
            VectorDraw.Professional.vdObjects.vdDocument doc = mainDoc;

            Childform chf = this.MdiChildren[0] as Childform;
            chf.Work = Childform.WORK.Loading;


            DrawMemberWithLoadCase(1);
            StructureForm();
            try
            {
                frmMemLoadCase = new
                       HEADSNeed.ASTRA.ASTRAForms.frmMemberLoadCase(doc, memberDoc, memberLoadDoc, memberJointLoadDoc, astDoc,
                       this.MdiChildren[0], this.MdiChildren[1], this.MdiChildren[2], this.MdiChildren[3]);
                frmMemLoadCase.Owner = this;
                //frmMemLoadCase.Location = new Point(this.Width / 2 - frmMemLoadCase.Width / 2, this.Height - frmMemLoadCase.Height - toolStrip1.Height);
                frmMemLoadCase.Location = new Point(panel2.Width + this.MdiChildren[0].Width, toolStrip1.Height);

                frmMemLoadCase.Size = new Size(this.Width - frmMemLoadCase.Location.X - 10, this.Height - vdCommandLine1.Height);
                //frmMemLoadCase.Size = new Size(this.Width - frmMemLoadCase.Location.X - 10, this.Height - vdCommandLine1.Height - this.MdiChildren[3].Height * 2 - 39);
                //frmMemLoadCase.Size = new Size(this.Width - frmMemLoadCase.Location.X - 10, frmMemLoadCase.Size.Height - this.MdiChildren[2].Height - this.MdiChildren[3].Height);

                chf.MemberLoadcase = frmMemLoadCase;


                frmMemLoadCase.Show();
            }
            catch (Exception exx) { }
            ShowTextSize();
            try
            {
                fTxtSize.chkJointLoad.Enabled = true;
            }
            catch (Exception ex)
            { }

        }
        public void LoadingOff()
        {
            #region Close All Window

            for (int ic = 0; ic < this.MdiChildren.Length; ic++)
            {
                if (this.MdiChildren[ic].Name == LastStructureFormName)
                {
                    continue;
                }
                else
                {
                    this.MdiChildren[ic].Close();
                    ic = -1;
                }
            }

            try
            {
                this.MdiChildren[0].WindowState = FormWindowState.Maximized;
                frmMemLoadCase.Close();
            }
            catch (Exception exx)
            {
            }
            #endregion
            //commandLine.ExecuteCommand("new");
        }
        public void PostProcess()
        {
            //if (!(Path.GetExtension(astDoc.FileName).ToLower() == ".ast"))
            //{
            //    string fn = Path.GetFileNameWithoutExtension(astDoc.FileName);
            //    fn = fn + ".ast";
            //    fn = Path.Combine(Path.GetDirectoryName(astDoc.FileName), fn);
            //    if (File.Exists(fn))
            //        astDoc = new ASTRADoc(fn);
            //}

            if (astDoc.IsDynamicLoad)
            {
                AstrA_Moving_LOAD_Deflection();
                return;
            }
            //if (astDoc.IsMovingLoad)
            //{
            //    astDoc.JointLoads.SetMovingJointLoad(astDoc.FileName);
            //    astDoc.JointLoads.CopyCoordinates(astDoc.Joints);
            //}
            ASTRALoadingMenu.Checked = false;

            CloseAstraWindow();

            #region Close All Window
            for (int ic = 0; ic < this.MdiChildren.Length; ic++)
            {
                if (this.MdiChildren[ic].Name == LastStructureFormName)
                {
                    continue;
                }
                else
                {
                    this.MdiChildren[ic].Close();
                    ic = -1;
                }
            }
            #endregion

            CalculateWindow();
            //frmPostProc = new frmPostProcess(astDoc, mainDoc, postProcess1, postProcess2, postProcess3);
            frmPostProc = new frmPostProcess(astDoc, this.MdiChildren[0], frmPostProcess1, frmPostProcess2, frmPostProcess3, mainDoc, postProcess1, postProcess2, postProcess3);
            frmPostProc.Owner = this;
            Childform chld = this.MdiChildren[0] as Childform;
            chld.Work = Childform.WORK.PostProcess;
            chld.PostProcess = frmPostProc;

            //frmPostProc.Location = new Point(216, 566);

            //frmPostProc.Location = new Point(fTxtSize.Location.X, fTxtSize.Size.Height + fTxtSize.Location.Y);
            frmPostProc.Location = new Point(fTxtSize.Location.X, fTxtSize.Location.Y);
            frmPostProc.Width = this.MdiChildren[0].Width;
            fTxtSize.Width = frmPostProc.Width;
            fTxtSize.Refresh();
            frmPostProc.Show();
            //astDoc.SetLoadFactor();


        }
        public void CalculateWindow()
        {
            OnFileNewPostProcess();
            postProcess1.ShowUCSAxis = false;
            postProcess2.ShowUCSAxis = false;
            postProcess3.ShowUCSAxis = false;
        }
        void Delete_FIL_Files(string dir_path)
        {
            if (Directory.Exists(dir_path))
            {
                try
                {
                    foreach(string s in Directory.GetFiles(dir_path))
                    {
                        if (Path.GetExtension(s).ToLower() == ".fil")
                            File.Delete(s);
                    }
                }
                catch (Exception ex) { }
            }
        }
        public void RunAnalysis()
        {
            string patFile = Path.Combine(Application.StartupPath, "PAT001.tmp");

            fName = astDoc.FileName;
            //Delete_FIL_Files(Path.GetDirectoryName(fName));

            System.Environment.SetEnvironmentVariable("SURVEY", fName);
            System.Environment.SetEnvironmentVariable("ASTRA", fName);

            File.WriteAllText(patFile, Path.GetDirectoryName(fName) + "\\");

            string runExe = "", ext = "";
            ext = Path.GetExtension(fName).ToLower();
            if (ext == ".txt")
            {
                runExe = Path.Combine(Application.StartupPath, "AST001.exe");

                if (File.Exists(runExe) == false)
                {
                    runExe = Path.Combine(Application.StartupPath, "AST004.exe");
                }
            }
            else if (ext == ".ast")
                runExe = Path.Combine(Application.StartupPath, "AST003.exe");
            try
            {
                System.Diagnostics.Process.Start(runExe).WaitForExit();
            }
            catch (Exception exx)
            {
            }
            ASTRAMenuSetup();
        }
        HEADSNeed.ASTRA.ASTRAForms.frmASTRAReport frm = null;
        frmMovingForces frm_mmm = null;
        public void ASTRA_Report()
        {
            string analysisFileName = "";
            analysisFileName = Path.GetDirectoryName(astDoc.FileName);
            analysisFileName = Path.Combine(analysisFileName, "ANALYSIS_REP.TXT");

            if (!File.Exists(analysisFileName))
                analysisFileName = Path.Combine(analysisFileName, "ANALYSIS_REP.TXT");

            if (File.Exists(analysisFileName))
            {
                try
                {
                    frm.Close();
                }
                catch (Exception exx)
                {
                }
                frm = new frmASTRAReport(analysisFileName);
                frm.Owner = this;
                frm.Size = new Size(this.Width, this.Height - 130);
                frm.Location = new Point(0, 130);
                frm.Show();
            }
            else
            {
                MessageBox.Show("'ANALYSIS_REP.TXT' file not found in working folder.", "ASTRA",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion

        #region T-BEAM & Culvert Chiranjit [2009 12 30]
        public void Open_Interactive_Drawing(string _code, string file_name)
        {
            if (!File.Exists(file_name)) return;
            system_path = Path.GetDirectoryName(file_name);
            file_path = Path.GetDirectoryName(system_path);


            string f_name = Des_Drawngs.Get_Interactive_Drawing_Path(_code);
            if (f_name == null) return;


            Childform chld = this.MdiChildren[0] as Childform;
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            
            bool success = false;
            if(File.Exists(f_name))
                success = mainDoc.Open(f_name);
            mainDoc.Palette.Background = Color.Black;

            CopyCoordinates.Set_Drawing_File(file_name, mainDoc);

            f_name = Path.Combine(file_path, Path.GetFileName(f_name));
            mainDoc.SaveAs(f_name);
            mainDoc.ClearEraseItems();
            mainDoc.Open(f_name);
        }

        public void SET_DESIGN_REPORT(string code, string file_name)
        {
            bool success = false;

            Childform chld = this.MdiChildren[0] as Childform;
            string f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\RCC TBeam Bridge");



            //Open_Interactive_Drawing(string _code, string file_name)

            #region
            switch (code)
            {
                case "PRELIMINARY_DIMENSIONS":
                    f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\RCC TBeam Bridge");
                    f_name = Path.Combine(f_name, "Preliminary Dimensions.dwg");
                    mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
                    success = mainDoc.Open(f_name);
                    mainDoc.Palette.Background = Color.Black;
                    break;
                case "CANTILEVER_SLAB":
                    Cantilever_Slab(file_name);
                    break;
                case "REINFORCEMENT_DETAILS_DECK_SLAB":
                    Deck_SLAB(file_name);
                    break;
                case "REINFORCEMENT_DETAILS_OF_LONG_GIRDERS":
                    LONG_GIRDERS(file_name);
                    //success = success;
                    break;
                case "REINFORCEMENT_PLAN_OF_DECK_SLAB":
                    f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\RCC TBeam Bridge");
                    f_name = Path.Combine(f_name, "Reinforcement Plan of Deck Slab.dwg");
                    mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
                    success = mainDoc.Open(f_name);
                    mainDoc.Palette.Background = Color.Black;
                    break;


                case "BRIDGE_GENERAL_ARRANGEMENT_DRAWING":
                    f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\RCC TBeam Bridge");
                    f_name = Path.Combine(f_name, "BRIDGE GENERAL ARRANGEMENT DRAWING.dwg");
                    mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
                    success = mainDoc.Open(f_name);
                    mainDoc.Palette.Background = Color.Black;
                    break;


                case "BRIDGE_SLAB_SUPER_STRUCTURE":
                    f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\RCC TBeam Bridge");
                    f_name = Path.Combine(f_name, "BRIDGE SLAB SUPER STRUCTURE.dwg");
                    mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
                    success = mainDoc.Open(f_name);
                    mainDoc.Palette.Background = Color.Black;
                    break;


                case "BRIDGE_ABUTMENT_WITH_FOUNDATION01":
                    f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\RCC TBeam Bridge");
                    f_name = Path.Combine(f_name, "BRIDGE ABUTMENT WITH FOUNDATION01.dwg");
                    mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
                    success = mainDoc.Open(f_name);
                    mainDoc.Palette.Background = Color.Black;
                    break;

                case "BRIDGE_ABUTMENT_WITH_FOUNDATION02":
                    f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\RCC TBeam Bridge");
                    f_name = Path.Combine(f_name, "BRIDGE ABUTMENT WITH FOUNDATION02.dwg");
                    mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
                    success = mainDoc.Open(f_name);
                    mainDoc.Palette.Background = Color.Black;
                    break;

                case "REINFORCEMENT_DETAILS_CROSS_GIRDERS":
                    Cross_GIRDERS(file_name);
                    break;
                case "ABUTMENT_COUNTERFORT":
                    ABUTMENT_COUNTERFORT(file_name);
                    break;
                case "ABUTMENT":
                    ABUTMENT(file_name);
                    break;
                case "RCC_ABUTMENT_UNDERPASS":
                    RCC_ABUTMENT_UNDERPASS(file_name);
                    break;
                case "ABUTMENT_SAMPLE":
                case "RCC_ABUTMENT_SAMPLE":
                    f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\RCC TBeam Bridge");
                    f_name = Path.Combine(f_name, "SAMPLE DRAWING BRIDGE ABUTMENT WITH FOUNDATION.dwg");
                    mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
                    success = mainDoc.Open(f_name);
                    mainDoc.Palette.Background = Color.Black;
                    break;

                case "PIER":
                    Pier(file_name);
                    break;

                case "BOX_CULVERT":
                    Box_Culvert(file_name);
                    break;
                case "RCC_BOX_STRUCTURE":
                    RCC_Box_Culvert(file_name);
                    break;

                case "PIPE_CULVERT":
                    Pipe_Culvert(file_name);
                    break;
                case "TOP_RCC_SLAB_UNDERPASS":
                    TOP_RCC_SLAB_UNDERPASS(file_name);
                    break;
                case "SLAB_CULVERTS":
                    SLAB_Culverts(file_name);
                    break;

                case "VEHICULAR_UP":
                    VEHICULAR_UP();
                    break;
                case "PEDESTRIAN_UP":
                    PEDESTRIAN_UP();
                    break;
                case "PRESTRESSED_POST_TENSIONED_RCC_GIRDER":
                    Prestressed_Post_Tensioned_RCC_Girder(file_name);
                    break;
                case "RCC_DECK_SLAB":
                    Rcc_Deck_Slab(file_name);
                    break;
                case "COMPOSITE_BRIDGE":
                    Composite_Bridge(file_name);
                    break;
                case "RCC_WELL_FOUNDATION":
                    RCC_WELL_FOUNDATION(file_name);
                    break;
                case "RCC_PILE_FOUNDATION":
                    RCC_PILE_FOUNDATION(file_name);
                    break;
                case "STEEL_PLATE_GIRDER_RAILWAY_BRIDGE":
                    STEEL_PLATE_GIRDER_RAILWAY_BRIDGE(file_name);
                    break;
                case "HYDROGRAPH":
                    HYDROGRAPH(file_name);
                    break;
            }
            #endregion

        }

        public void Pier(string file_path)
        {
            string f_name = "";

            system_path = Path.GetDirectoryName(file_path);
            string drawing_file = Path.Combine(system_path, "PIER_DRAWING.FIL");
            bool success = true;

            Childform chld = this.MdiChildren[0] as Childform;
            f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\Bridge\Abutment & Pier");
            f_name = Path.Combine(f_name, "PIER_DRAWING.dwg");
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            success = mainDoc.Open(f_name);
            success = success;
            mainDoc.Palette.Background = Color.Black;
            mainDoc.Redraw(true);

            f_name = Path.GetDirectoryName(system_path); // User Path
            f_name = Path.Combine(f_name, "Drawing_Bridge_Pier.VDML");
            CopyCoordinates.Set_Drawing_File(drawing_file, mainDoc);
            mainDoc.SaveAs(f_name);
            mainDoc.ClearEraseItems();
            mainDoc.Open(f_name);
            return;
        }
        public void STEEL_PLATE_GIRDER_RAILWAY_BRIDGE(string file_path)
        {
            string f_name = "";

            system_path = Path.GetDirectoryName(file_path);
            string drawing_file = Path.Combine(system_path, "STEEL_PLATE_GIRDER_RAILWAY_BRIDGE_DRAWING.FIL");
            
            bool success = true;

            Childform chld = this.MdiChildren[0] as Childform;
            f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\Bridge\Rail");
            f_name = Path.Combine(f_name, "STEEL_PLATE_GIRDER_RAILWAY_BRIDGE_DRAWING.dwg");
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            success = mainDoc.Open(f_name);
            mainDoc.Palette.Background = Color.Black;
            mainDoc.Redraw(true);

            f_name = Path.GetDirectoryName(system_path); // User Path
            f_name = Path.Combine(f_name, "Drawing_Bridge_Steel_PG_Rail_Girder.VDML");
            CopyCoordinates.Set_Drawing_File(drawing_file, mainDoc);
            mainDoc.SaveAs(f_name);
            mainDoc.ClearEraseItems();
            mainDoc.Open(f_name);
            return;
        }
        public void HYDROGRAPH(string file_path)
        {
            string f_name = "";

            system_path = Path.GetDirectoryName(file_path);
            //string drawing_file = Path.Combine(system_path, "HYDROGRAPH.FIL");
            string drawing_file = Path.Combine(system_path, "HYDRAULIC_CALCULATIONS.FIL");

            bool success = true;

            Childform chld = this.MdiChildren[0] as Childform;
            
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            HydroCrossSection.DrawHrdrographFromDataFile(drawing_file, mainDoc);
            //success = mainDoc.Open(f_name);
            mainDoc.Palette.Background = Color.Black;
            mainDoc.Redraw(true);

            f_name = Path.GetDirectoryName(system_path); // User Path
            f_name = Path.Combine(f_name, "HYDROGRAPH.VDML");
            CopyCoordinates.Set_Drawing_File(drawing_file, mainDoc);
            mainDoc.SaveAs(f_name);
            mainDoc.ClearEraseItems();
            mainDoc.Open(f_name);
            return;
        }

        public void RCC_PILE_FOUNDATION(string file_path)
        {
            string f_name = "";

            system_path = Path.GetDirectoryName(file_path);
            string drawing_file = Path.Combine(system_path, "RCC_PILE_FOUNDATION_DRAWING.FIL");
            bool success = true;

            Childform chld = this.MdiChildren[0] as Childform;
            f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\Bridge\Foundation");
            f_name = Path.Combine(f_name, "RCC_PILE_FOUNDATION_DRAWING.VDML");
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            success = mainDoc.Open(f_name);
            //mainDoc.Palette.Background = Color.Black;
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(mainDoc);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.RegenEx(mainDoc);

            mainDoc.Redraw(true);

            f_name = Path.GetDirectoryName(system_path);  // User Path
            f_name = Path.Combine(f_name, "Drawing_Bridge_Found_Pile_Found.VDML");
            CopyCoordinates.Set_Drawing_File(drawing_file, mainDoc);
            mainDoc.SaveAs(f_name);
            mainDoc.ClearEraseItems();
            mainDoc.Open(f_name);
            return;
        }
        public void RCC_WELL_FOUNDATION(string file_path)
        {
            string f_name = "";
            string drawing_file = Path.GetDirectoryName(file_path);
            drawing_file = Path.Combine(drawing_file, "RCC_WELL_FOUNDATION_DRAWING.FIL");
            bool success = true;

            Childform chld = this.MdiChildren[0] as Childform;
            f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\Bridge\Foundation");
            f_name = Path.Combine(f_name, "RCC_WELL_FOUNDATION_DRAWING.VDML");
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            success = mainDoc.Open(f_name);
            mainDoc.Palette.Background = Color.Black;
            mainDoc.Redraw(true);

            f_name = Path.GetDirectoryName(file_path); // Astra System
            f_name = Path.GetDirectoryName(f_name);  // User Path
            f_name = Path.Combine(f_name, "Drawing_Bridge_Found_Well_Found.VDML");
            CopyCoordinates.Set_Drawing_File(drawing_file, mainDoc);
            mainDoc.SaveAs(f_name);
            mainDoc.ClearEraseItems();
            mainDoc.Open(f_name);
            return;
        }

        public void Prestressed_Post_Tensioned_RCC_Girder(string file_path)
        {
            string f_name = "";
            system_path = Path.GetDirectoryName(file_path);
            string drawing_file = Path.Combine(system_path, "PRESTRESSED_POST_TENSIONED_RCC_GIRDER_DRAWING.FIL");

            bool success = true;

            Childform chld = this.MdiChildren[0] as Childform;
            f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\Bridge\PreStressed");
            f_name = Path.Combine(f_name, "PRESTRESSED_BRIDGE_DRAWING.dwg");
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            success = mainDoc.Open(f_name);
            CopyCoordinates.Set_Drawing_File(drawing_file, mainDoc);
            mainDoc.Palette.Background = Color.Black;
            mainDoc.Redraw(true);

            drawing_file = Path.GetDirectoryName(system_path);
            drawing_file = Path.Combine(drawing_file, "Drawing_Bridge_Rcc_PS_Girder.VDML");
            mainDoc.SaveAs(drawing_file);
            mainDoc.ClearAll();
            mainDoc.Open(drawing_file);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(mainDoc);
        }
        public void Rcc_Deck_Slab(string file_path)
        {
            string f_name = "";
            system_path = Path.GetDirectoryName(file_path);
            string drawing_file = Path.Combine(system_path, "RCC_DECK_SLAB_DRAWING.FIL");

            bool success = true;

            Childform chld = this.MdiChildren[0] as Childform;
            f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\Bridge\PreStressed");
            f_name = Path.Combine(f_name, "Rcc_Deck_Slab.dwg");
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            success = mainDoc.Open(f_name);

            success = success;
            CopyCoordinates.Set_Drawing_File(drawing_file, mainDoc);
            mainDoc.Palette.Background = Color.White;
            mainDoc.Redraw(true);

            drawing_file = Path.GetDirectoryName(system_path);
            drawing_file = Path.Combine(drawing_file, "Drawing_Bridge_Rcc_PS_Slab.VDML");
            mainDoc.SaveAs(drawing_file);
            mainDoc.ClearAll();
            mainDoc.Open(drawing_file);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(mainDoc);

            OnFileNew();
            chld = this.MdiChildren[1] as Childform;
            f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\RCC TBeam Bridge");
            f_name = Path.Combine(f_name, "Reinforcement Plan of Deck Slab.dwg");
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            mainDoc.Open(f_name);
            mainDoc.Palette.Background = Color.Black;
            //mainDoc.SaveAs(f_name);
            mainDoc.Redraw(true);

            file_path = Path.GetDirectoryName(system_path);
            drawing_file = Path.Combine(file_path, "Plan_Bridge_Rcc_PS_Slab.VDML");
            mainDoc.SaveAs(drawing_file);
            mainDoc.ClearAll();
            mainDoc.Open(drawing_file);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(mainDoc);
            this.LayoutMdi(MdiLayout.TileVertical);
            

        }
        public void Composite_Bridge(string drawing_path)
        {
            string f_name = "";

            system_path = Path.GetDirectoryName(drawing_path);
            file_path = Path.GetDirectoryName(system_path);

            string drawing_file = Path.Combine(system_path, "COMPOSITE_BRIDGE_DRAWING.FIL");
            bool success = true;

            Childform chld = this.MdiChildren[0] as Childform;
            f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\Bridge\Composit");
            f_name = Path.Combine(f_name, "COMPOSITE_BRIDGE_DRAWING.dwg");
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            success = mainDoc.Open(f_name);
            mainDoc.Palette.Background = Color.Black;
            CopyCoordinates.Set_Drawing_File(drawing_file, mainDoc);
            //CopyCoordinates.Draw_Box_Culvert(@"D:\SOFTWARE DEVELOPMENT\ASTRA\CODE\BOX_CULVERT\BOX_CULVERT_DRAWING.FIL", mainDoc);
            mainDoc.Redraw(true);

            drawing_file = Path.Combine(file_path, "Drawing_Bridge_Rcc_Comp_Slab_Girder.VDML");
            mainDoc.SaveAs(drawing_file);
            mainDoc.ClearAll();
            mainDoc.Open(drawing_file);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(mainDoc);


            OnFileNew();
            chld = this.MdiChildren[1] as Childform;
            f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\RCC TBeam Bridge");
            f_name = Path.Combine(f_name, "Reinforcement Plan of Deck Slab.dwg");
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            mainDoc.Open(f_name);
            mainDoc.Palette.Background = Color.Black;
            mainDoc.SaveAs(f_name);
            mainDoc.Redraw(true);

            drawing_file = Path.Combine(file_path, "Plan_Bridge_Rcc_Comp_Slab_Girder.VDML");
            mainDoc.SaveAs(drawing_file);
            mainDoc.ClearAll();
            mainDoc.Open(drawing_file);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(mainDoc);
            this.LayoutMdi(MdiLayout.TileVertical);

        }
        public void Box_Culvert_DUMP()
        {
            string f_name = "";
            bool success = true;

            Childform chld = this.MdiChildren[0] as Childform;
            f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\\Culvert\Box culvert");
            f_name = Path.Combine(f_name, "1CELL Box Culvert.dwg");
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            success = mainDoc.Open(f_name);
            mainDoc.Palette.Background = Color.Black;
            mainDoc.Redraw(true);
            mainDoc.SaveAs(f_name);

            OnFileNew();
            chld = this.MdiChildren[1] as Childform;
            f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\\Culvert\Box culvert");
            f_name = Path.Combine(f_name, "2CELL Box Culvert.dwg");
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            success = mainDoc.Open(f_name);
            mainDoc.Palette.Background = Color.Black;
            mainDoc.Redraw(true);
            mainDoc.SaveAs(f_name);
            this.LayoutMdi(MdiLayout.TileVertical);
            //this.LayoutMdi(MdiLayout.TileHorizontal);
        }
        public void RCC_Box_Culvert(string file_path)
        {
            string f_name = "";
            string drawing_file = Path.GetDirectoryName(file_path);
            drawing_file = Path.Combine(drawing_file, "RCC_BOX_STRUCTURE_DRAWING.FIL");
            bool success = true;

            Childform chld = this.MdiChildren[0] as Childform;
            f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\Bridge\Culvert\Box culvert");
            f_name = Path.Combine(f_name, "BOX_CULVERT.vdml");
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            success = mainDoc.Open(f_name);
            mainDoc.Palette.Background = Color.White;
            CopyCoordinates.Draw_Box_Culvert(drawing_file, mainDoc);
            //CopyCoordinates.Draw_Box_Culvert(@"D:\SOFTWARE DEVELOPMENT\ASTRA\CODE\BOX_CULVERT\BOX_CULVERT_DRAWING.FIL", mainDoc);
            mainDoc.Redraw(true);

            drawing_file = Path.GetDirectoryName(file_path);
            drawing_file = Path.GetDirectoryName(drawing_file);
            drawing_file = Path.Combine(drawing_file, "Drawing_Underpass_Ped_Box.VDML");
            mainDoc.SaveAs(drawing_file);
            mainDoc.ClearAll();
            mainDoc.Open(drawing_file);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(mainDoc);
        }
       
        public void Box_Culvert(string file_path)
        {
            string f_name = "";
            string drawing_file = Path.GetDirectoryName(file_path);
            drawing_file = Path.Combine(drawing_file, "BOX_CULVERT_DRAWING.FIL");
            bool success = true;

            Childform chld = this.MdiChildren[0] as Childform;
            f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\Bridge\Culvert\Box culvert");
            f_name = Path.Combine(f_name, "BOX_CULVERT.vdml");
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            success = mainDoc.Open(f_name);
            mainDoc.Palette.Background = Color.White;
            CopyCoordinates.Draw_Box_Culvert(drawing_file, mainDoc);
            //CopyCoordinates.Draw_Box_Culvert(@"D:\SOFTWARE DEVELOPMENT\ASTRA\CODE\BOX_CULVERT\BOX_CULVERT_DRAWING.FIL", mainDoc);
            mainDoc.Redraw(true);

            drawing_file = Path.GetDirectoryName(file_path);
            drawing_file = Path.GetDirectoryName(drawing_file);
            drawing_file = Path.Combine(drawing_file, "Drawing_Culvert_Rcc_Box.VDML");
            mainDoc.SaveAs(drawing_file);
            mainDoc.ClearAll();
            mainDoc.Open(drawing_file);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(mainDoc);
        }
        public void Pipe_Culvert(string file_path)
        {
            string f_name = "";
            Childform chld = this.MdiChildren[0] as Childform;
            f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\Bridge\Culvert\Pipe culvert");
            f_name = Path.Combine(f_name, "PIPE Culvert.dwg");
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            mainDoc.Open(f_name);
            mainDoc.Palette.Background = Color.Black;
            mainDoc.SaveAs(f_name);
            mainDoc.Redraw(true);


            f_name = Path.GetDirectoryName(file_path);
            f_name = Path.Combine(f_name, "Drawing_Culvert_Rcc_Pipe.VDML");
            mainDoc.SaveAs(f_name);
            mainDoc.ClearAll();
            mainDoc.Open(f_name);
            
        }
        public void SLAB_Culverts(string file_path)
        {
            string f_name = "";

            system_path = Path.GetDirectoryName(file_path);
            string drawing_file = Path.Combine(system_path, "SLAB_CULVERT_DRAWING.FIL");
            bool success = true;

            Childform chld = this.MdiChildren[0] as Childform;
            f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\Bridge\Culvert\Slab Culverts");
            f_name = Path.Combine(f_name, "SLAB_CULVERT.vdml");
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            success = mainDoc.Open(f_name);
            mainDoc.Palette.Background = Color.White;
            CopyCoordinates.Draw_Slab_Culvert(drawing_file, mainDoc);
            //CopyCoordinates.Draw_Box_Culvert(@"D:\SOFTWARE DEVELOPMENT\ASTRA\CODE\BOX_CULVERT\BOX_CULVERT_DRAWING.FIL", mainDoc);
            mainDoc.Redraw(true);

            drawing_file = Path.GetDirectoryName(system_path);
            drawing_file = Path.Combine(drawing_file, "Drawing_Culvert_Rcc_Slab.VDML");
            mainDoc.SaveAs(drawing_file);
            mainDoc.ClearAll();
            mainDoc.Open(drawing_file);
        }


        public void LONG_GIRDERS(string drawing_file)
        {
            string f_name = "";

            system_path = Path.GetDirectoryName(drawing_file);
            file_path = Path.GetDirectoryName(system_path);


            drawing_file = Path.Combine(system_path, "LONG_GIRDERS_DRAWING.FIL");
            bool success = true;


            Childform chld = this.MdiChildren[0] as Childform;
            f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\RCC TBeam Bridge");
            f_name = Path.Combine(f_name, "Reinforcement Details of Long Girders.dwg");
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            success = mainDoc.Open(f_name);
            mainDoc.Palette.Background = Color.White;
            CopyCoordinates.Set_Drawing_File(drawing_file, mainDoc);
            //CopyCoordinates.Draw_Box_Culvert(@"D:\SOFTWARE DEVELOPMENT\ASTRA\CODE\BOX_CULVERT\BOX_CULVERT_DRAWING.FIL", mainDoc);
            drawing_file = Path.Combine(file_path, "Drawing_Bridge_Rcc_T_Beam_Long_Girder.VDML");
            mainDoc.SaveAs(drawing_file);
            mainDoc.ClearAll();
            mainDoc.Open(drawing_file);
            mainDoc.Redraw(true);
        }

        public void Cross_GIRDERS(string file_name)
        {
            system_path = Path.GetDirectoryName(file_name);

            file_path = Path.GetDirectoryName(system_path);
            string f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\RCC TBeam Bridge");
            f_name = Path.Combine(f_name, "Reinforcement Details Cross Girders.dwg");
            Childform chld = this.MdiChildren[0] as Childform;
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            bool success = mainDoc.Open(f_name);
            mainDoc.Palette.Background = Color.Black;

            f_name = Path.Combine(system_path, "CROSS_GIRDERS_DRAWING.FIL");
            CopyCoordinates.Set_Drawing_File(f_name, mainDoc);

            f_name = Path.Combine(file_path, "Drawing_Bridge_Rcc_T_Beam_Cross_Girder.VDML");
            mainDoc.SaveAs(f_name);
            mainDoc.ClearEraseItems();
            mainDoc.Open(f_name);
        }

        public void ABUTMENT_COUNTERFORT(string file_path)
        {
            string f_name = "";
            system_path = Path.GetDirectoryName(file_path);
            string drawing_file = Path.Combine(system_path, "ABUTMENT_COUNTERFORT_DRAWING.FIL");
            bool success = true;

            Childform chld = this.MdiChildren[0] as Childform;
            f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\Bridge\Abutment & Pier");
            f_name = Path.Combine(f_name, "Abutment_Counterfort_Drawings.dwg");
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            success = mainDoc.Open(f_name);
            mainDoc.Palette.Background = Color.White;
            CopyCoordinates.Set_Drawing_File(drawing_file, mainDoc);
            mainDoc.Redraw(true);

            drawing_file = Path.GetDirectoryName(system_path);
            drawing_file = Path.Combine(drawing_file, "Drawing_Bridge_Rcc_Abutment_As_Counterfort_Wall.VDML");
            mainDoc.SaveAs(drawing_file);
            mainDoc.ClearAll();
            mainDoc.Open(drawing_file);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(mainDoc);
        }
        public void ABUTMENT(string file_path)
        {
            string f_name = "";
            system_path = Path.GetDirectoryName(file_path);
            string drawing_file = Path.Combine(system_path, "ABUTMENT_DRAWING.FIL");
            bool success = true;

            Childform chld = this.MdiChildren[0] as Childform;
            f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\Bridge\Abutment & Pier");
            f_name = Path.Combine(f_name, "Abutment Reinforcements.dwg");
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            success = mainDoc.Open(f_name);
            mainDoc.Palette.Background = Color.Black;
            CopyCoordinates.Set_Drawing_File(drawing_file, mainDoc);
            mainDoc.Redraw(true);

            drawing_file = Path.GetDirectoryName(system_path);
            drawing_file = Path.Combine(drawing_file, "Drawing_Bridge_Rcc_Abutment.VDML");
            mainDoc.SaveAs(drawing_file);
            mainDoc.ClearAll();
            mainDoc.Open(drawing_file);
        }
        public void RCC_ABUTMENT_UNDERPASS(string file_path)
        {
            string f_name = "";
            string drawing_file = Path.GetDirectoryName(file_path);
            drawing_file = Path.Combine(drawing_file, "RCC_ABUTMENT_UNDERPASS_DRAWING.FIL");
            bool success = true;


            Childform chld = this.MdiChildren[0] as Childform;
            f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\Bridge\Abutment & Pier");
            f_name = Path.Combine(f_name, "Abutment Reinforcements.dwg");
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            success = mainDoc.Open(f_name);
            mainDoc.Palette.Background = Color.Black;
            CopyCoordinates.Set_Drawing_File(drawing_file, mainDoc);
            mainDoc.Redraw(true);

            drawing_file = Path.GetDirectoryName(file_path);
            drawing_file = Path.GetDirectoryName(drawing_file);
            drawing_file = Path.Combine(drawing_file, "Drawing_Underpass_Veh_Abutment.VDML");
            mainDoc.SaveAs(drawing_file);
            mainDoc.ClearAll();
            mainDoc.Open(drawing_file);
        }
        public void Cantilever_Slab(string file_path)
        {
            string f_name = "";
            string drawing_file = Path.GetDirectoryName(file_path);
            drawing_file = Path.Combine(drawing_file, "CANTILEVER_SLAB_DRAWING.FIL");
            bool success = true;

            Childform chld = this.MdiChildren[0] as Childform;
            f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\RCC TBeam Bridge");
            f_name = Path.Combine(f_name, "Cantilever Slab.dwg");
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            success = mainDoc.Open(f_name);
            mainDoc.Palette.Background = Color.White;
            CopyCoordinates.Set_Drawing_File(drawing_file, mainDoc);
            //CopyCoordinates.Draw_Box_Culvert(@"D:\SOFTWARE DEVELOPMENT\ASTRA\CODE\BOX_CULVERT\BOX_CULVERT_DRAWING.FIL", mainDoc);
            mainDoc.Redraw(true);
            drawing_file = Path.GetDirectoryName(file_path);
            drawing_file = Path.GetDirectoryName(drawing_file);
            drawing_file = Path.Combine(drawing_file, "Drawing_Bridge_Rcc_T_Beam_Cant_Slab.VDML");
            mainDoc.SaveAs(drawing_file);
            mainDoc.ClearAll();
            mainDoc.Open(drawing_file);
        }
        public void Deck_SLAB(string drawing_path)
        {
            string f_name = "";

            system_path = Path.GetDirectoryName(drawing_path);
            file_path = Path.GetDirectoryName(system_path);

            string drawing_file = Path.Combine(system_path, "DECK_SLAB_DRAWING.FIL");
            bool success = true;


            Childform chld = this.MdiChildren[0] as Childform;
            f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\RCC TBeam Bridge");
            f_name = Path.Combine(f_name, "Reinforcement Details Deck Slab.dwg");
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            success = mainDoc.Open(f_name);
            mainDoc.Palette.Background = Color.White;
            CopyCoordinates.Draw_Deck_Slab(drawing_file, mainDoc);
            //CopyCoordinates.Draw_Box_Culvert(@"D:\SOFTWARE DEVELOPMENT\ASTRA\CODE\BOX_CULVERT\BOX_CULVERT_DRAWING.FIL", mainDoc);
            mainDoc.Redraw(true);

            drawing_file = Path.Combine(file_path, "Drawing_Bridge_Rcc_T_Beam_Deck_Slab.VDML");
            mainDoc.SaveAs(drawing_file);
            mainDoc.ClearAll();
            mainDoc.Open(drawing_file);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(mainDoc);

            OnFileNew();
            chld = this.MdiChildren[1] as Childform;
            f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\RCC TBeam Bridge");
            f_name = Path.Combine(f_name, "Reinforcement Plan of Deck Slab.dwg");
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            mainDoc.Open(f_name);
            mainDoc.Palette.Background = Color.Black;
            mainDoc.SaveAs(f_name);
            mainDoc.Redraw(true);

            drawing_file = Path.Combine(file_path, "Plan_Bridge_Rcc_T_Beam_Deck_Slab.VDML");
            mainDoc.SaveAs(drawing_file);
            mainDoc.ClearAll();
            mainDoc.Open(drawing_file);
            this.LayoutMdi(MdiLayout.TileVertical);
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(mainDoc);

        }
        public void TOP_RCC_SLAB_UNDERPASS(string file_path)
        {
            string f_name = "";
            string drawing_file = Path.GetDirectoryName(file_path);
            drawing_file = Path.Combine(drawing_file, "TOP_RCC_SLAB_UNDERPASS_DRAWING.FIL");
            bool success = true;

            Childform chld = this.MdiChildren[0] as Childform;
            f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\Bridge\Culvert\Slab Culverts");
            f_name = Path.Combine(f_name, "SLAB_CULVERT.vdml");
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            success = mainDoc.Open(f_name);
            mainDoc.Palette.Background = Color.White;
            CopyCoordinates.Draw_Slab_Culvert(drawing_file, mainDoc);
            //CopyCoordinates.Draw_Box_Culvert(@"D:\SOFTWARE DEVELOPMENT\ASTRA\CODE\BOX_CULVERT\BOX_CULVERT_DRAWING.FIL", mainDoc);
            mainDoc.Redraw(true);

            drawing_file = Path.GetDirectoryName(file_path);
            drawing_file = Path.GetDirectoryName(drawing_file);
            drawing_file = Path.Combine(drawing_file, "Drawing_Underpass_Veh_Slab.VDML");
            mainDoc.SaveAs(drawing_file);
            mainDoc.ClearAll();
            mainDoc.Open(drawing_file);
        }
        public void VEHICULAR_UP()
        {
            string f_name = "";
            bool success = true;

            Childform chld = this.MdiChildren[0] as Childform;
            f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\Bridge\Underpass\VEHICULAR");
            f_name = Path.Combine(f_name, "VUP-001.dwg");

            if (File.Exists(fName))
            {
                mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
                success = mainDoc.Open(f_name);
                mainDoc.Palette.Background = Color.Black;
                mainDoc.Redraw(true);
                mainDoc.SaveAs(f_name);
            }


            OnFileNew();
            chld = this.MdiChildren[1] as Childform;
            f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\Bridge\Underpass\VEHICULAR");
            f_name = Path.Combine(f_name, "VUP-002.dwg");
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            success = mainDoc.Open(f_name);
            mainDoc.Palette.Background = Color.Black;
            mainDoc.Redraw(true);
            mainDoc.SaveAs(f_name);

            OnFileNew();
            chld = this.MdiChildren[2] as Childform;
            f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\Bridge\Underpass\VEHICULAR");
            f_name = Path.Combine(f_name, "VUP-003.dwg");
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            success = mainDoc.Open(f_name);
            mainDoc.Palette.Background = Color.Black;
            mainDoc.Redraw(true);
            mainDoc.SaveAs(f_name);

            OnFileNew();
            chld = this.MdiChildren[3] as Childform;
            f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\Bridge\Underpass\VEHICULAR");
            f_name = Path.Combine(f_name, "VUP-004.dwg");
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            success = mainDoc.Open(f_name);
            mainDoc.Palette.Background = Color.Black;
            mainDoc.Redraw(true);
            mainDoc.SaveAs(f_name);

            OnFileNew();
            chld = this.MdiChildren[4] as Childform;
            f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\Bridge\Underpass\VEHICULAR");
            f_name = Path.Combine(f_name, "VUP-005.dwg");
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            success = mainDoc.Open(f_name);
            mainDoc.Palette.Background = Color.Black;
            mainDoc.Redraw(true);
            mainDoc.SaveAs(f_name);

            OnFileNew();
            chld = this.MdiChildren[5] as Childform;
            f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\Bridge\Underpass\VEHICULAR");
            f_name = Path.Combine(f_name, "VUP-006.dwg");
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            success = mainDoc.Open(f_name);
            mainDoc.Palette.Background = Color.Black;
            mainDoc.Redraw(true);
            mainDoc.SaveAs(f_name);

            OnFileNew();
            chld = this.MdiChildren[6] as Childform;
            f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\Bridge\Underpass\VEHICULAR");
            f_name = Path.Combine(f_name, "VUP-007.dwg");
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            success = mainDoc.Open(f_name);
            mainDoc.Palette.Background = Color.Black;
            mainDoc.Redraw(true);
            mainDoc.SaveAs(f_name);

            this.LayoutMdi(MdiLayout.TileVertical);
        }
        public void PEDESTRIAN_UP()
        {
            string f_name = "";
            bool success = true;

            Childform chld = this.MdiChildren[0] as Childform;
            f_name = Path.Combine(Application.StartupPath, @"DRAWINGS\Bridge\Underpass\PEDESTRIAN");
            f_name = Path.Combine(f_name, "PEDESTRIAN UP.dwg");
            mainDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
            success = mainDoc.Open(f_name);
            mainDoc.Palette.Background = Color.Black;
            mainDoc.Redraw(true);
            mainDoc.SaveAs(f_name);
        }

        #endregion

        private void OnFileNew()
        {
            createForm();

            Childform form1 = this.ActiveMdiChild as Childform;
            if (form1 != null)
            {
                VectorDraw.Professional.vdObjects.vdDocument doc = form1.vdScrollableControl1.BaseControl.ActiveDocument;

                Propertiesform.Initialize(doc, GetResourcesPath(), this.panel2);
                Propertiesform.LoadProps(null, doc, true);
                FixSide(Propertiesform.mDocumentProperties.mSide);
            }
        }
        private void OnFileNew(Childform.WORK work)
        {
            createForm();

            Childform form1 = this.ActiveMdiChild as Childform;
            form1.Work = work;
            if (form1 != null)
            {
                VectorDraw.Professional.vdObjects.vdDocument doc = form1.vdScrollableControl1.BaseControl.ActiveDocument;

                Propertiesform.Initialize(doc, GetResourcesPath(), this.panel2);
                Propertiesform.LoadProps(null, doc, true);
                FixSide(Propertiesform.mDocumentProperties.mSide);
            }
        }
        private void OnFileNewPostProcess()
        {
            createForm();

            Childform form1 = this.ActiveMdiChild as Childform;
            frmPostProcess1 = form1;
            if (form1 != null)
            {

                VectorDraw.Professional.vdObjects.vdDocument doc = form1.vdScrollableControl1.BaseControl.ActiveDocument;
                postProcess1 = doc;
                Propertiesform.Initialize(doc, GetResourcesPath(), this.panel2);
                Propertiesform.LoadProps(null, doc, true);
                FixSide(Propertiesform.mDocumentProperties.mSide);
            }
            createForm();

            form1 = this.ActiveMdiChild as Childform;
            frmPostProcess2 = form1;
            if (form1 != null)
            {

                VectorDraw.Professional.vdObjects.vdDocument doc = form1.vdScrollableControl1.BaseControl.ActiveDocument;
                postProcess2 = doc;
                Propertiesform.Initialize(doc, GetResourcesPath(), this.panel2);
                Propertiesform.LoadProps(null, doc, true);
                FixSide(Propertiesform.mDocumentProperties.mSide);
            }
            createForm();

            form1 = this.ActiveMdiChild as Childform;
            frmPostProcess3 = form1;
            if (form1 != null)
            {

                VectorDraw.Professional.vdObjects.vdDocument doc = form1.vdScrollableControl1.BaseControl.ActiveDocument;
                postProcess3 = doc;
                Propertiesform.Initialize(doc, GetResourcesPath(), this.panel2);
                Propertiesform.LoadProps(null, doc, true);
                FixSide(Propertiesform.mDocumentProperties.mSide);
            }

            #region Calculate Windows Location And Size

            panel2.Width = 210; // Left Side Property Box
            double x, y, w, h;
            x = y = w = h = 0.0d;
            x = panel2.Location.X + panel2.Width;
            y = panel2.Location.Y;
            w = this.Width - RightToolbarsPanel.Width * 1.5 - panel2.Width;
            h = panel2.Height;

            this.MdiChildren[0].Location = new Point(0, 0);
            this.MdiChildren[0].Size = new Size((int)w * 3 / 5, (int)h - 50);
            this.MdiChildren[0].WindowState = FormWindowState.Normal;

            Childform form = this.MdiChildren[0] as Childform;
            mainDoc = form.vdScrollableControl1.BaseControl.ActiveDocument;
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(mainDoc);


            this.MdiChildren[1].Location = new Point((int)w * 3 / 5, 0);
            this.MdiChildren[1].Size = new Size((int)w * 2 / 5, (int)h / 3);
            this.MdiChildren[1].WindowState = FormWindowState.Normal;

            this.MdiChildren[2].Location = new Point((int)w * 3 / 5, (int)(h / 3));
            this.MdiChildren[2].Size = new Size((int)w * 2 / 5, (int)(h / 3));
            this.MdiChildren[2].WindowState = FormWindowState.Normal;

            this.MdiChildren[3].Location = new Point((int)w * 3 / 5, (int)(h / 3) * 2);
            this.MdiChildren[3].Size = new Size((int)w * 2 / 5, (int)h / 3);
            this.MdiChildren[3].WindowState = FormWindowState.Normal;

            #endregion

            try
            {
                nodeGrid.Close();
                memGrid.Close();
            }
            catch (Exception ex) { }
        }
        private void OnHelpAbout()
        {
            FormAboutBox abt = new FormAboutBox();
            abt.Owner = this;
            abt.ShowDialog();
        }
        private void OnCloseCurrentDoc()
        {
            if (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close();
            }
        }
        private void OnWorkingDir()
        {
            IWorkingDirSelector wrkdirselector = HeadsFunctions.CreateWkDirDialog(this);
            wrkdirselector.DialogInstance.Owner = this;
            if (wrkdirselector.DialogInstance.ShowDialog() == DialogResult.OK)
            {
                this.AppDataPath = wrkdirselector.WorkingFolderPath;
            }
        }

        #region DRAWSTRING
        private void OnDrawString()
        {
            if (!Directory.Exists(AppDataPath))
            {
                string fpth = System.Environment.GetEnvironmentVariable("SURVEY");

                if (fpth == null)
                    fpth = System.Environment.GetEnvironmentVariable("DEM0");
                if (fpth != null)
                {
                    AppDataPath = Path.GetDirectoryName(fpth);
                }
            }

            //if (this.ActiveMdiChild != null)
            //{
            //    if (this.ValidateWorkingDir())
            //    {
            //        System.Windows.Forms.Form frmDrawString = HeadsFunctions.CreateDrawStringDialog(this);

            //        if (frmDrawString != null)
            //        {
            //            frmDrawString.Owner = this;
            //            frmDrawString.ShowDialog();
            //        }
            //    }
            //}

            //Chiranjit [2011 09 15]
            if (this.ActiveMdiChild != null)
            {
                if (this.ValidateWorkingDir())
                {
                    //System.Windows.Forms.Form frmDrawString = HeadsFunctions.CreateDrawStringDialog(this);
                    System.Windows.Forms.Form frmDrawString = new HEADSNeed.StringDraw.frm_StringDraw(this, VDoc);

                    if (frmDrawString != null)
                    {
                        frmDrawString.Owner = this;
                        frmDrawString.ShowDialog();
                    }
                }
            }
        }
        #endregion

        #region HALIGNMENT
        private void OnDesignNewHalignHIPMethod()
        {
            if (this.ActiveMdiChild != null)
            {
                if (this.ValidateWorkingDir())
                {
                    if (this.frmHalignValign == null || this.frmHalignValign.IsDisposed)
                    {
                        this.frmHalignValign = HeadsFunctions.CreateNewHalignHIPMethodDialog(this);
                        if (this.frmHalignValign != null && this.frmHalignValign.IsDisposed != true)
                        {
                            this.frmHalignValign.Owner = this;
                            this.frmHalignValign.Show();
                        }
                    }
                    else
                    {
                        if (this.frmHalignValign.WindowState == FormWindowState.Minimized)
                        {
                            this.frmHalignValign.WindowState = FormWindowState.Normal;
                        }
                    }
                }
            }
        }

        private void OnDesignOpenHalignHIPMethod()
        {
            if (this.ActiveMdiChild != null)
            {
                if (this.ValidateWorkingDir())
                {
                    if (this.frmHalignValign == null || this.frmHalignValign.IsDisposed == true)
                    {
                        IHalignFilSelector halignfilselector = HeadsFunctions.CreateHIPMethodSelectorDialog(this);

                        if (halignfilselector != null)
                        {
                            Form frmHIPSelector = halignfilselector.DialogInstance;
                            frmHIPSelector.Owner = this;
                            if (frmHIPSelector.ShowDialog() == DialogResult.OK)
                            {
                                this.frmHalignValign = HeadsFunctions.CreateOpenHIPMethodDialog(this, halignfilselector.SelectedItem);
                                this.frmHalignValign.Owner = this;
                                this.frmHalignValign.Show();
                            }
                        }
                    }
                    else
                    {
                        if (this.frmHalignValign.WindowState == FormWindowState.Minimized)
                        {
                            this.frmHalignValign.WindowState = FormWindowState.Normal;
                        }

                    }
                }
            }
        }

        private void OnDesignOpenHalignElementMethod()
        {
            if (this.ActiveMdiChild != null)
            {
                if (this.ValidateWorkingDir())
                {
                    if (this.frmHalignValign == null || this.frmHalignValign.IsDisposed == true)
                    {
                        IHalignFilSelector halignfilselector = HeadsFunctions.CreateElementMethodSelectorDialog(this);

                        if (halignfilselector != null)
                        {
                            Form frmHIPSelector = halignfilselector.DialogInstance;
                            frmHIPSelector.Owner = this;
                            if (frmHIPSelector.ShowDialog() == DialogResult.OK)
                            {
                                this.frmHalignValign = HeadsFunctions.CreateOpenElementMethodDialog(this, halignfilselector.SelectedItem);
                                this.frmHalignValign.Owner = this;
                                this.frmHalignValign.Show();
                            }
                        }
                    }
                }
            }
        }

        private void OnDesignNewHalignElementMethod()
        {
            if (this.ActiveMdiChild != null)
            {
                if (this.ValidateWorkingDir())
                {
                    if (this.frmHalignValign == null || this.frmHalignValign.IsDisposed)
                    {
                        this.frmHalignValign = HeadsFunctions.CreateNewHalignElementMethodDialog(this);
                    }


                    if (this.frmHalignValign != null && this.frmHalignValign.IsDisposed != true)
                    {
                        this.frmHalignValign.Owner = this;
                        this.frmHalignValign.Show();
                    }
                }
            }
        }
        #endregion

        #region VALIGNMENT
        private void OnDesignNewValignment()
        {
            if (this.ActiveMdiChild != null)
            {
                if (this.ValidateWorkingDir())
                {
                    if (this.frmHalignValign == null || this.frmHalignValign.IsDisposed)
                    {
                        this.frmHalignValign = HeadsFunctions.CreateNewValignDialog(this);
                        if (this.frmHalignValign != null && this.frmHalignValign.IsDisposed != true)
                        {
                            this.frmHalignValign.Owner = this;
                            this.frmHalignValign.Show();
                        }
                    }
                    else
                    {
                        if (this.frmHalignValign.WindowState == FormWindowState.Minimized)
                        {
                            this.frmHalignValign.WindowState = FormWindowState.Normal;
                        }
                    }
                }
            }
        }

        private void OnDesignOpenValignment()
        {
            if (this.ActiveMdiChild != null)
            {
                if (this.ValidateWorkingDir())
                {
                    if (this.frmHalignValign == null || this.frmHalignValign.IsDisposed == true)
                    {
                        IValignFilSelector valignfilselector = HeadsFunctions.CreateValignSelectorDialog(this);

                        if (valignfilselector != null)
                        {
                            Form frmValignSelector = valignfilselector.DialogInstance;
                            frmValignSelector.Owner = this;
                            if (frmValignSelector.ShowDialog() == DialogResult.OK)
                            {
                                this.frmHalignValign = HeadsFunctions.CreateOpenValignmentDialog(this, valignfilselector.SelectedItem);
                                this.frmHalignValign.Owner = this;
                                this.frmHalignValign.Show();
                            }
                        }
                    }
                    else
                    {
                        if (this.frmHalignValign.WindowState == FormWindowState.Minimized)
                        {
                            this.frmHalignValign.WindowState = FormWindowState.Normal;
                        }

                    }
                }
            }
        }
        #endregion

        #region L-SECTION
        private void OnDesignLSecDrawLSection()
        {
            if (this.ActiveMdiChild != null)
            {
                if (this.ValidateWorkingDir())
                {
                    System.Windows.Forms.Form frmLSec = HeadsFunctions.CreateLSectionDialog(this);

                    if (frmLSec != null)
                    {
                        frmLSec.Owner = this;
                        frmLSec.ShowDialog();
                    }
                }
            }
        }
        private void OnDesignLSecGridOn()
        {
            if (this.ActiveMdiChild != null)
            {
                if (this.ValidateWorkingDir())
                {
                    System.Windows.Forms.Form frmLSecGridOn = HeadsFunctions.CreateLSecGridDialog(this);

                    if (frmLSecGridOn != null)
                    {
                        frmLSecGridOn.Owner = this;
                        frmLSecGridOn.ShowDialog();
                    }
                }
            }
        }
        private void OnDesignLSecGridOff()
        {
            if (this.ActiveMdiChild != null)
            {
                HeadsFunctions.DeleteLSecGrid(this);
            }
        }

        private void OnDesignLSecnVerticalDetailsOn()
        {
            if (this.ActiveMdiChild != null)
            {
                if (this.ValidateWorkingDir())
                {
                    System.Windows.Forms.Form frmLSecVDetails = HeadsFunctions.CreateVaricalDetailsDialog(this);

                    if (frmLSecVDetails != null)
                    {
                        frmLSecVDetails.Owner = this;
                        frmLSecVDetails.ShowDialog();
                    }
                }
            }
        }
        #endregion

        #region CONFIG PARAM
        private void OnDesignConfigParam()
        {
            if (this.ActiveMdiChild != null)
            {
                Form frmCfgParam = HeadsFunctions.CreateConfigParamDialog(this);
                frmCfgParam.Owner = this;
                frmCfgParam.ShowDialog();
            }
        }
        #endregion

        #region CHAINAGE
        private void OnDesignChainageOn()
        {
            if (this.ActiveMdiChild != null)
            {
                if (this.ValidateWorkingDir())
                {
                    Form frmChainage = HeadsFunctions.CreateChainageDialog(this);
                    if (frmChainage != null)
                    {
                        frmChainage.Owner = this;
                        frmChainage.ShowDialog();
                    }
                }
            }
        }
        private void OnDesignChainageOff()
        {
            if (this.ActiveMdiChild != null)
            {
                HeadsFunctions.DeleteChainage(this);
            }
        }
        #endregion

        #region DETAILS
        private void OnDesignDetailsOn()
        {
            if (this.ActiveMdiChild != null)
            {
                if (this.ValidateWorkingDir())
                {
                    Form frmDetails = HeadsFunctions.CreateDetailsDialog(this);
                    if (frmDetails != null)
                    {
                        frmDetails.Owner = this;
                        frmDetails.ShowDialog();
                    }
                }
            }
        }

        private void OnDesignDetailsOff()
        {
            if (this.ActiveMdiChild != null)
            {
                HeadsFunctions.DeleteDetails(this);
            }
        }
        #endregion

        #region COORDINATE
        public void OnDesignCoordinatesOn()
        {
            if (this.ActiveMdiChild != null)
            {
                Form frmCoordinates = HeadsFunctions.CreateCoordinateDialog(this);
                if (frmCoordinates != null)
                {
                    frmCoordinates.Owner = this;
                    frmCoordinates.ShowDialog();
                }
            }
        }

        public void OnDesignCoordinatesOff()
        {
            if (this.ActiveMdiChild != null)
            {
                HeadsFunctions.DeleteCoordinates(this);
            }
        }

        #endregion

        #region BOUNDARY
        private void OnDesignBoundary()
        {
            if (this.ActiveMdiChild != null)
            {
                if (this.ValidateWorkingDir())
                {
                    Form frmBoundary = HeadsFunctions.CreateBoundaryDialog(this, false);

                    if (frmBoundary != null)
                    {
                        frmBoundary.Owner = this;
                        frmBoundary.Show();
                    }
                }
            }
        }
        #endregion

        #region Modelling
        private void OnModellingHalignment()
        {
            if (this.ActiveMdiChild != null)
            {
                if (this.ValidateWorkingDir())
                {
                    System.Windows.Forms.Form form = HeadsFunctions.CreateModellingHalignDialog(this);
                    if (form != null)
                    {
                        form.Owner = this;
                        form.ShowDialog();
                    }

                }
            }
        }

        private void OnModellingValignment()
        {
            if (this.ActiveMdiChild != null)
            {
                if (this.ValidateWorkingDir())
                {
                    System.Windows.Forms.Form form = HeadsFunctions.CreateModellingValignDialog(this);
                    if (form != null)
                    {
                        form.Owner = this;
                        form.ShowDialog();
                    }
                }
            }
        }
        private void OnModellingOffsets()
        {
            if (this.ActiveMdiChild != null)
            {
                if (this.ValidateWorkingDir())
                {
                    System.Windows.Forms.Form form = HeadsFunctions.CreateModellingOffsetDialog(this);
                    if (form != null)
                    {
                        form.Owner = this;
                        form.ShowDialog();
                    }
                }
            }
        }
        #endregion

        #region Layout
        private void OnDesignLayoutApply()
        {
            if (this.ActiveMdiChild != null)
            {
                if (this.ValidateWorkingDir())
                {
                    HeadsFunctions.ApplyLayout(this);
                }
            }
        }
        private void OnDesignLayoutAccept()
        {
            if (this.ActiveMdiChild != null)
            {
                if (this.ValidateWorkingDir())
                {
                    HeadsFunctions.AcceptLayout(this);
                }
            }
        }
        #endregion

        #region Makestring
        private void OnDesignMakeString()
        {
            if (this.ActiveMdiChild != null)
            {
                if (this.ValidateWorkingDir())
                {
                    Form frmMakestring = HeadsFunctions.CreateBoundaryDialog(this, true);

                    if (frmMakestring != null)
                    {
                        frmMakestring.Owner = this;
                        frmMakestring.Show();
                    }
                }
            }
        }
        #endregion

        #region Offset
        void OnDesignOffsets()
        {
            if (this.ActiveMdiChild != null)
            {
                if (this.ValidateWorkingDir())
                {
                    if (this.frmHalignValign == null || this.frmHalignValign.IsDisposed)
                    {
                        this.frmHalignValign = HeadsFunctions.CreateOffsetModelDialog(this);
                        if (this.frmHalignValign != null && this.frmHalignValign.IsDisposed != true)
                        {
                            this.frmHalignValign.Owner = this;
                            this.frmHalignValign.Show();
                        }
                    }
                    else
                    {
                        if (this.frmHalignValign.WindowState == FormWindowState.Minimized)
                        {
                            this.frmHalignValign.WindowState = FormWindowState.Normal;
                        }
                    }
                }
            }
        }
        #endregion

        #region Help
        void OnHelpContent()
        {
            string strPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            strPath = System.IO.Path.Combine(strPath, @"ASTRAHelp\CADBasics.chm");

            if (System.IO.File.Exists(strPath))
            {
                System.Diagnostics.Process.Start(strPath);
            }
            else
            {
                MessageBox.Show(this, "Help module not found\n" + strPath, this.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        #endregion

        #region File Export
        void OnDRGImport()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "DRG Files (*.DRG)|*.DRG";
            openFileDialog1.Multiselect = false;
            openFileDialog1.CheckFileExists = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.OnFileNew();

                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                HeadsFunctions.ImportFromDrgFile(this, openFileDialog1.FileName);
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }

        }
        void OnDRGExport()
        {
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
            SaveFileDialog1.OverwritePrompt = true;

            // Set the file name to myText.txt, set the type filter
            // to text files, and set the initial directory to drive C.
            SaveFileDialog1.FileName = "Heads Drawing";
            SaveFileDialog1.DefaultExt = "DRG";
            SaveFileDialog1.Filter = "Heads Drawing files (*.DRG)|*.DRG";
            if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                HeadsFunctions.ExportToDrgFile(this, SaveFileDialog1.FileName);
            }

        }
        #endregion

        #region Chiranjit [2011 09 30] VIEW MOVING LOAD DEFLECTION
        private void OnViewMovingLoad()
        {
            //CloseAstraWindow();
            // Chiranjit 03/12/2009
            try
            {
                frm_ASTRA_Analysis fa = new frm_ASTRA_Analysis(fName, true);
                fa.Owner = this;
                fa.Show();

                //AstrA_Moving_LOAD_Deflection(); 
            }
            catch (Exception ex) { }
            // end
            return;
        }
        #endregion

        #region LOAD DEFLECTION
        private void OnLoadDeflection()
        {
            CloseAstraWindow();
            // Chiranjit 03/12/2009
            try
            {
                AstrA_LOAD_Deflection(); 
            }
            catch (Exception ex) { }
            // end
            return;
            #region Tirtha Chatterjee
            // Tirtha Chatterjee
            if (this.ActiveMdiChild != null)
            {

                try
                {
                    Childform chld = this.ActiveMdiChild as Childform;
                    chld.vdScrollableControl1.BaseControl.ActiveDocument.Palette.Background = Color.Black;
                    chld.WindowState = FormWindowState.Maximized;
                }
                catch (Exception ex)
                {
                }
                if (this.ValidateWorkingDir())
                {
                    if (this.frmLoadDef == null || this.frmLoadDef.IsDisposed)
                    {
                        this.frmLoadDef = HeadsFunctions.CreateLoadDeflectionDialog(this);
                        if (this.frmLoadDef != null && this.frmLoadDef.IsDisposed != true)
                        {
                            this.frmLoadDef.Owner = this;
                            this.frmLoadDef.Show();
                        }
                    }
                    else
                    {
                        if (this.frmLoadDef.WindowState == FormWindowState.Minimized)
                        {
                            this.frmLoadDef.WindowState = FormWindowState.Normal;
                        }
                    }
                }
            }
            #endregion
        }
        #endregion

        #region Close All Documents
        public void CloseAllDocs()
        {
            for (int i = 1; i < this.MdiChildren.Length; i++)
            {
                Childform chld = this.MdiChildren[i] as Childform;
                vdDocument vDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
                vDoc.SaveAs(Path.Combine(Application.StartupPath, "test.vdml"));
            }
        }
        #endregion Close All Documents
        //
        #region ModelingFromTextData
        private void OnModelingFromTextData()
        {
            if (this.ActiveMdiChild != null)
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                    if (ofd.ShowDialog() != DialogResult.Cancel)
                    {
                        this.AppDataPath = Path.GetDirectoryName(ofd.FileName);
                        SurveyDataCollection sdc = new SurveyDataCollection(ofd.FileName);
                        sdc.Write_Model("GROUND");
                        MessageBox.Show("Model Name and String Labels are written in file MODEL.FIL and MODEL.LST", "ASTRA");
                    }
                }
            }
        }
        #endregion ModelEdit

        #region ModelEdit
        private void OnModelEdit()
        {
            if (this.ActiveMdiChild != null)
            {
                if (this.ValidateWorkingDir())
                {
                    System.Windows.Forms.Form frm_model = new frm_ModelEdit(this);

                    if (frm_model != null)
                    {
                        frm_model.Owner = this;
                        frm_model.ShowDialog();
                    }
                }
            }
        }
        #endregion ModelEdit

        #region GroundModeling
        private void OnGroundModeling()
        {
            if (this.ActiveMdiChild != null)
            {
                if (this.ValidateWorkingDir())
                {
                    System.Windows.Forms.Form frm = new frm_Triangulation(this.AppDataPath);

                    if (frm != null)
                    {
                        frm.Owner = this;
                        frm.ShowDialog();
                    }
                }
            }
        }
        #endregion ModelEdit

        #region ContourModeling
        private void OnContourModeling()
        {
            if (this.ActiveMdiChild != null)
            {
                if (this.ValidateWorkingDir())
                {
                    System.Windows.Forms.Form frm = new frm_ContourModeling(this.AppDataPath);

                    if (frm != null)
                    {
                        frm.Owner = this;
                        frm.ShowDialog();
                    }
                }
            }
        }
        #endregion ContourModeling

        #region IRunProject Members
        public void OpenMultiDrawings()
        {
            frmAstraDrawings frp = new frmAstraDrawings(this);
            frp.Owner = this;
            frp.Show();
        }
        public MainForm MDIParentForm
        {
            get { return this; }
        }

        public void ChildActivate(int mdiChildIndex)
        {
            this.MdiChildren[mdiChildIndex].Activate();

            Childform chld = this.ActiveMdiChild as Childform;
            chld.vdScrollableControl1.BaseControl.ActiveDocument.ActiveLayOut.BkColorEx = Color.Black;
            VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(chld.vdScrollableControl1.BaseControl.ActiveDocument);
        }

        public void OpenDesignDrawings(ref ListBox lstbox)
        {
            List<string> list = null;
            string design_path = DrawingCode;

            lstbox.Items.Clear();
            if ((startFileName != null))
            {
                design_path = Path.GetDirectoryName(startFileName);
            }

            while (design_path != null)
            {
                if (Directory.Exists(design_path))
                {
                    //if (startFileName == null)
                    list = new List<string>(Directory.GetFiles(design_path));
                    //else
                    //    list = new List<string>(startFileName);
                    for (int i = 0; i < list.Count; i++)
                    {
                        try
                        {
                            if (Path.GetExtension(list[i].ToLower()) == ".dxf" ||
                                Path.GetExtension(list[i].ToLower()) == ".drg" ||
                                Path.GetExtension(list[i].ToLower()) == ".vdml" ||
                                Path.GetExtension(list[i].ToLower()) == ".vdcl" ||
                                Path.GetExtension(list[i].ToLower()) == ".dwg")
                            {
                                FileOpen(list[i], ref lstbox);
                                if (i < list.Count - 1)
                                    createForm();
                            }
                        }
                        catch (Exception ex) { }
                    }
                }
                if (design_path == DrawingCode)
                {
                    design_path = Path.GetDirectoryName(DrawingCode);
                    design_path = Path.Combine(design_path, "Design Pavements");
                }
                else
                    design_path = null;

                if (!Directory.Exists(design_path)) design_path = null;
            }
        }
        void FileOpen(string fname, ref ListBox lstbox)
        {
            if (Path.GetExtension(fname).ToLower() == ".drg")
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
                HeadsFunctions.ImportFromDrgFile(this, fname);
                this.Cursor = System.Windows.Forms.Cursors.Default;
                lstbox.Items.Add(Path.GetFileName(fname));
            }
            else
            {
                Childform form = this.ActiveMdiChild as Childform;
                VectorDraw.Professional.vdObjects.vdDocument doc = form.vdScrollableControl1.BaseControl.ActiveDocument;
                bool successopen = doc.Open(fname);
                if (!successopen)
                {
                    System.Windows.Forms.MessageBox.Show("Error openning " + fname, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    form.Close();
                    return;
                }
                else
                {
                    lstbox.Items.Add(Path.GetFileName(fname));
                    AddToRecentFiles(doc.FileName);
                }
                doc.Redraw(false);
                //Updates the window list
                ActivateMdiChild(null);
                ActivateMdiChild(this.ActiveMdiChild);
            }
        }
        frmAstraDrawings frp = null;

        public void OpenMultipleDrawings()
        {
            try
            {
                frp.Close();
            }
            catch (Exception ex) { }
            frp = new frmAstraDrawings(this);
            frp.Owner = this;
            frp.Show();
        }

        void DRG_File_Import(string fileName)
        {

        }

        public string DrawingCode
        {
            get
            {
                return drawingCode;
            }
        }
        public string DrawingPath
        {
            get
            {
                return drawingPath;
            }
            set
            {
                drawingPath = value;
            }
        }

        #endregion


        #region IASTRA_Control Members
        public bool IS_ASTRA_Control = false;
        public void ASTRA_CommandExecute(string commandname)
        {
            bool isDefaultImplemented = true;
            bool success = false;
            IS_ASTRA_Control = true;
            CommandExecute(commandname, isDefaultImplemented, ref  success);
        }

        #endregion


        #region FIND& REPLACE
        private void OnFind()
        {
            if (this.ActiveMdiChild != null)
            {

                if (this.frmFind == null || this.frmFind.IsDisposed)
                {
                    this.frmFind = HeadsFunctions.CreateFindOptionDialog(this);
                    if (this.frmFind != null && this.frmFind.IsDisposed != true)
                    {
                        this.frmFind.Owner = this;
                        this.frmFind.Show();
                    }
                }
                else
                {
                    if (this.frmLoadDef.WindowState == FormWindowState.Minimized)
                    {
                        this.frmLoadDef.WindowState = FormWindowState.Normal;
                    }
                }
            }
            //HeadsFunctions.CreateFindOptionDialog(this);
        }
        #endregion FIND

    }

}
//Chiranjit [2011 09 30] VIEW MOVING LOAD DEFLECTION
