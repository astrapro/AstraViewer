using System;
using System.Collections.Generic;
using System.Collections;
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
using HeadsUtils.Interfaces;
using HeadsUtils;
using System.Runtime.InteropServices;
using HEADSNeed.ASTRA.ASTRADrawingTools;
using HEADSNeed.ASTRA.ASTRAClasses;
using HEADSNeed.ASTRA.CadToAstra;
using HEADSNeed.ASTRA.CadToAstra.FORMS;
using HEADSNeed.ASTRA.ASTRAForms;
using HEADSNeed.ASTRA;


namespace HeadsViewer
{
    /// <summary>
    /// This is the main form of the application.
    /// </summary>

    public partial class MainForm : Form, IHeadsApplication, AstraDrawings.IOpenDrawings
    {
        //global basic variables for the application.
        internal bool mDisplayPolarCoord = false;
        static private string ToolBarsFilename = "UserData.txt";
        static private ToolStripPanel tsTop = new ToolStripPanel();
        static private ToolStripPanel tsRight = new ToolStripPanel();
        static private ContextMenuStrip VisibleToolbarsFloatingMenu = new ContextMenuStrip();
        private vdArray<string> RecentFiles = new vdArray<string>();
        private int RecentFilesCount = 5;
        private int CommandsHeight = 0;
        public GeneralProperties Propertiesform = new GeneralProperties();
        private Size ChildSize = new Size(800, 450);
        private Point ChildLocation = new Point(0, 0);
        private FormWindowState state = FormWindowState.Normal;

        private IHeadsFunctions HeadsFunctions;

        string drawingCode = "";
        string drawingPath = "";
        //Chiranjit 30/09/2009
        string resPath = "";
        string str_Design_slab = "";

        // Chiranjit [2010 04 14]
        IASTRACAD iACAD = null;


        //frmMemberGrid formMemGrid = null;
        //frmNodeGrid formNodeGrid = null;
        //frmMemberLoadCase formMemLoadCase = null;

        /// <summary>
        /// Initialize the main form.
        /// </summary>
        ///
        //public string startFileName = "";
        public string startFileName = null;
        DesignDrawings Des_Drawngs = null;
        public MainForm()
        {

            //change the NumberFormat of Culture in order to work properly to save,open of GeneralProps.txt 
            System.Threading.Thread.CurrentThread.CurrentCulture = VectorDraw.Serialize.Activator.GetCulture();

            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

            this.Text = this.ApplicationTitle + " " + this.ProductVersion;
            MdiClientBackroundImage();
            vdCommandLine1.LoadCommands(GetResourcesPath(), "Commands.txt");
            commandLine.CommandExecute += new VectorDraw.Professional.vdCommandLine.CommandExecuteEventHandler(CommandExecute);
            VectorDraw.Serialize.GlobalizedDictionary.Dictionary = new VectorDraw.Serialize.GlobalizedDictionary(GetResourcesPath() + "vdres.txt");
            tsTop.Click += new EventHandler(tsTop_Click);
            this.RegisterHeadsCommands();
            this.SetWorkingDirFromEnvVariable();

            HeadsFunctions = LoadHeadsFunction();

            iACAD = new ASTRACAD();
            iACAD.MainForm = this;
            //// Chiranjit [30/09/09]
            //memGrid = new frmMemberGrid(astDoc);
            //nodeGrid = new frmNodeGrid(astDoc);
            //memLoadCase = new frmMemberLoadCase(

            //Chiranjit [2011 06 22]
            //Set Interactive Drawing and default drawing Path
            string draw_path = Path.Combine(Application.StartupPath, "DRAWINGS\\Drawing_Path.txt");
            if (File.Exists(draw_path))
                Des_Drawngs = new DesignDrawings(draw_path);

        }

        /// <summary>
        /// This event is used to show the floating menu when right click on the top toolbar. This floating menu allows the user
        /// to show/hide toolbars.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tsTop_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MouseEventArgs e1 = e as System.Windows.Forms.MouseEventArgs;
            if (e1 != null)
            {
                if (e1.Button == MouseButtons.Right)
                {
                    VisibleToolbarsFloatingMenu.Show(System.Windows.Forms.Control.MousePosition);
                }
            }
        }
        /// <summary>
        /// Get's the resources directory when it is necessary.Get's the directory of the executable and adds the vdRes path.
        /// </summary>
        /// <returns></returns>
        public string GetResourcesPath()
        {

            //The same function in implemented also in EditCreateBlock.cs

            if (resPath == "")
                resPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\vdRes\\";
            return resPath;
        }
        /// <summary>
        /// Load initializations of the main form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {


            string astra_version = "";
            //Chiranjit [2012 11 12] ADD Lock Check
            if (HASP_Lock.IsProfessionalVersion())
            {
                astra_version = "PRO";
            }
            else
            {
                astra_version = "DEMO";
            }


            //if (File.Exists(Path.Combine(Application.StartupPath, "astra.pmt")))
            //{
            //    astra_version = "PRO";
            //}
            //else
            //    astra_version = System.Environment.GetEnvironmentVariable("ASTRA_VERSION");


            //astra_version = "DEMO";

            if (astra_version == null ||
                astra_version == "DEMO" ||
                astra_version == "DEM0")
            {
                HeadsUtils.Constants.BuildType = eHEADS_RELEASE_TYPE.DEMO;
            }
            //System.Environment.SetEnvironmentVariable("ASTRA", @"E:\Chiranjit\Software Development\ASTRA Pro Code Locked + Unlocked_AASHTO\ASTRA\bin\Debug\env.set");
            //System.Environment.SetEnvironmentVariable("ASTRA", @"E:\Chiranjit\Software Development\ASTRA Pro Code Locked + Unlocked_AASHTO\ASTRA\bin\Debug\env.set");


            //System.Environment.SetEnvironmentVariable("ASTRA", @"D:\Software Development\ASTRA Pro Main Screen Professional\ASTRA\bin\x86\Debug\env.set");
            //E:\WRITE FILES\[2010 06 07] ASTRA\ASTRA [2010 06 05]\ASTRA\frmCheckHasp.cs

            //System.Environment.SetEnvironmentVariable("ASTRA_DRAWINGS", @"C:\Program Files\ASTRA Pro Release 4.0\DRAWINGS\CompositDrawings");
            //System.Environment.SetEnvironmentVariable("ASTRA_DRAWINGS", null);
            LoadMenusAndToolbars();
            //commandLine.ExecuteCommand("new");
            //fName = System.Environment.GetEnvironmentVariable("ASTRA_DRAWINGS");
            //System.Environment.SetEnvironmentVariable("ASTRA_DRAWINGS", null);
            commandLine.ExecuteCommand("new");


            fName = System.Environment.GetEnvironmentVariable("ASTRA");

            string str = "";

            #region Exception Handling
            try
            {
                if (fName != null)
                {
                    ASTRA_CODE_SET(fName);
                }
                else
                {
                    fName = System.Environment.GetEnvironmentVariable("DISNET");
                    if (fName != null)
                    {
                        AppDataPath = Path.GetDirectoryName(fName);
                        commandLine.ExecuteCommand("drawdisnetpipe");
                    }
                    else
                    {
                        //commandLine.ExecuteCommand("new");
                        Childform form = this.ActiveMdiChild as Childform;

                        AerialView.BackColor = form.vdScrollableControl1.BaseControl.ActiveDocument.Palette.Background;
                        AerialView.Update();

                        form.Location = ChildLocation;
                        form.Size = ChildSize;
                        //form.WindowState = state;
                        //Chiranjit
                        form.WindowState = FormWindowState.Maximized;

                        //MessageBox.Show("This application's source code is provided to all our VDF subscribers.","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                //fName = null;


                //if (startFileName != "")
                //{
                //    Childform form = this.ActiveMdiChild as Childform;
                //    form.vdScrollableControl1.BaseControl.ActiveDocument.Open(startFileName);
                //    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(
                //    form.vdScrollableControl1.BaseControl.ActiveDocument);
                //}
                if (startFileName != null)
                {
                    //OpenMultiDrawings();
                    Childform form = this.ActiveMdiChild as Childform;

                    if (!File.Exists(startFileName))
                        File.WriteAllText(startFileName, "");
                    if (!form.vdScrollableControl1.BaseControl.ActiveDocument.Open(startFileName))
                    {
                        VDoc.SaveAs(startFileName);
                        VDoc.Open(startFileName);
                        //this.Text = "ASTRA Viewer [" + Path.GetFileName(startFileName) + "]";
                    }
                    VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(
                    form.vdScrollableControl1.BaseControl.ActiveDocument);
                }
                ASTRALoadingMenu.Enabled = false;
                ASTRAAnalysisMenu.Enabled = false;
                ASTRAReportMenu.Enabled = false;
                ASTRAPostProcessMenu.Enabled = false;
                ASTRAStructureMenu.Enabled = false;
                ASTRATextSizeMenu.Enabled = false;
                ASTRALoadDeflectionMenu.Enabled = false;
                ASTRAMenuSetup();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error : " + ex.ToString());
            }
            #endregion

            iACAD.Menu = this.MainMenuStrip;

            iACAD.EnableAllMenu(false);
            //tmr_check_hasp.Tick += new EventHandler(tmrZoomMember_Tick);
            //tmrZoomMember_Tick
        }
        /// <summary>
        /// Saving properties when the main form is closing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseAllDocs();
            closeAstra();
            SaveApplicationsSettings();

            Childform form1 = this.ActiveMdiChild as Childform;
            if (form1 != null)
            {
                VectorDraw.Professional.vdObjects.vdDocument doc = form1.vdScrollableControl1.BaseControl.ActiveDocument;
                Propertiesform.Initialize(doc, GetResourcesPath(), this.panel2);
                Propertiesform.SaveProps(GetResourcesPath());
            }

            //Set the app data path to empty. This will help us to delete all the tmp files
            this.AppDataPath = "";


        }
        private void closeAstra()
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
            catch (Exception exx)
            { }
            try
            {
                frmMemLoadCase.Close();

            }
            catch (Exception exx)
            { }

            try
            {
                frmPostProc.Close();
            }
            catch (Exception exx)
            { }

            try
            {
                frmPostProcess1.Close();
                frmPostProcess2.Close();
                frmPostProcess3.Close();
            }
            catch (Exception exx)
            { }
        }
        /// <summary>
        /// This event is used in order to hide the left toolbar panel when there no controls attached.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tsRight_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (tsRight.Controls.Count == 0) this.RightToolbarsPanel.Width = 5;
        }
        /// <summary>
        /// This event is used in order to fix the width of the right toolbar panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tsRight_Paint(object sender, PaintEventArgs e)
        {
            this.RightToolbarsPanel.Width = this.RightToolbarsPanel.Controls[0].Width + 5;
        }
        /// <summary>
        /// This event is fired each time a button is added to the menu and also to the toolbars so we can initialize
        /// for each of these items their click events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="item"></param>
        void vdMenu_OnAddMenuButton(object sender, ToolStripItem item)
        {
            item.Click += new EventHandler(item_Click);
            item.MouseDown += new MouseEventHandler(item_MouseDown);
        }
        /// <summary>
        /// This event is the mouse Down event of the floating menu that controls the visibility of the toolbars.This menu
        /// is shown when right click is pressed on the top panel or above any toolbar button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void button_MouseDown(object sender, MouseEventArgs e)
        {
            ToolStripDropDownItem item = sender as ToolStripDropDownItem;
            if (item != null)
            {
                if (item.Name == "CommandLine")
                {
                    bool visible = !vdCommandLine1.Visible;
                    vdCommandLine1.Visible = visible;
                    if (visible)
                    {
                        splitter2.MinSize = 70;
                        panel1.MaximumSize = new Size(0, 0);

                        if (CommandsHeight != 0)
                            panel1.Height = toolStrip1.Height + CommandsHeight;
                        else
                            panel1.Height = toolStrip1.Height + 100;
                    }
                    else
                    {
                        CommandsHeight = vdCommandLine1.Height;
                        panel1.Height = toolStrip1.Height;
                        splitter2.MinSize = toolStrip1.Height;
                        panel1.MaximumSize = new Size(panel1.Width, toolStrip1.Height);
                    }
                }
                else if (item.Name == "Properties List")
                {
                    bool visible = !vdPropertyGrid1.Visible;
                    vdPropertyGrid1.Visible = visible;
                    if (visible)
                    {
                        panel2.Visible = visible;
                        splitter1.MinSize = 230;
                        if (mdi != null) mdi.Invalidate();
                    }
                    else
                    {
                        panel2.Visible = visible;
                        splitter1.MinSize = 0;
                        if (mdi != null) mdi.Invalidate();
                    }
                }
                else
                {
                    Control c = tsTop.Controls[item.Name];
                    if (c == null) c = tsRight.Controls[item.Name];
                    if (c != null)
                    {
                        c.Visible = !c.Visible;
                    }
                }
            }

        }
        /// <summary>
        /// Key Down event of the main application.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) commandLine.Focus();
            return false;
        }
        /// <summary>
        /// Show/Hide the coord display.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CoordDisplay_Click(object sender, EventArgs e)
        {
            mDisplayPolarCoord = !mDisplayPolarCoord;
        }
        /// <summary>
        /// Enable/Disable menus and toolbars when there is no active MDI child window.
        /// </summary>
        /// <param name="p"></param>
        internal void UpdateMenu(bool bNoChild)
        {
            if (bNoChild)
            {
                //Disable Menu
                MenuStrip menu = this.MainMenuStrip;
                foreach (ToolStripItem var in this.MainMenuStrip.Items)
                {
                    var.Visible = false;
                }
                ToolStripItem[] items = this.MainMenuStrip.Items.Find("File", false);
                if (items.GetLength(0) == 1)
                {
                    ToolStripMenuItem file = items[0] as ToolStripMenuItem;
                    if (file != null)
                    {
                        file.Visible = true;
                        foreach (ToolStripItem var in file.DropDownItems)
                        {
                            var.Enabled = false;
                        }

                        file.DropDownItems["Import"].Enabled = true;
                        file.DropDownItems["Open"].Enabled = true;
                        file.DropDownItems["New"].Enabled = true;
                        file.DropDownItems["Exit"].Enabled = true;
                        if (file.DropDownItems["RecentFiles"] != null) file.DropDownItems["RecentFiles"].Enabled = true;
                    }
                }

                items = this.MainMenuStrip.Items.Find("Help", false);
                if (items.GetLength(0) == 1)
                {
                    ToolStripMenuItem help = items[0] as ToolStripMenuItem;
                    if (help != null)
                    {
                        help.Visible = true;
                    }
                }



                //Disable Toolbars
                foreach (ToolStripPanelRow row in tsTop.Rows)
                {
                    foreach (ToolStrip var in row.Controls)
                    {
                        var.Enabled = false;
                    }
                }
                foreach (ToolStripPanelRow row in tsRight.Rows)
                {
                    foreach (ToolStrip var in row.Controls)
                    {
                        var.Enabled = false;
                    }
                }

                Control c = tsTop.Controls["File"];
                if (c == null) c = tsRight.Controls["File"];
                if (c != null)
                {
                    c.Enabled = true;
                    ToolStrip tool = c as ToolStrip;
                    foreach (ToolStripItem var in tool.Items)
                    {
                        var.Enabled = false;
                    }
                    if (tool.Items["New"] != null)
                        tool.Items["New"].Enabled = true;
                    if (tool.Items["Open"] != null)
                        tool.Items["Open"].Enabled = true;
                }

                Childform form1 = this.ActiveMdiChild as Childform;
                if (form1 != null)
                {
                    VectorDraw.Professional.vdObjects.vdDocument doc = form1.vdScrollableControl1.BaseControl.ActiveDocument;

                    Propertiesform.Initialize(doc, GetResourcesPath(), this.panel2);
                    Propertiesform.SaveProps(GetResourcesPath());
                }

            }
            else
            {
                //Enable Menu
                MenuStrip menu = this.MainMenuStrip;
                foreach (ToolStripItem var in this.MainMenuStrip.Items)
                {
                    var.Visible = true;
                }
                ToolStripItem[] items = this.MainMenuStrip.Items.Find("File", false);
                if (items.GetLength(0) == 1)
                {
                    ToolStripMenuItem file = items[0] as ToolStripMenuItem;
                    if (file != null)
                    {
                        foreach (ToolStripItem var in file.DropDownItems)
                        {
                            var.Enabled = true;
                        }
                    }
                }
                //Enable Toolbars
                foreach (ToolStripPanelRow row in tsTop.Rows)
                {
                    foreach (ToolStrip var in row.Controls)
                    {
                        var.Enabled = true;
                    }
                }

                Control c = tsTop.Controls["File"];
                if (c == null) c = tsRight.Controls["File"];
                if (c != null)
                {
                    c.Enabled = true;
                    ToolStrip tool = c as ToolStrip;
                    foreach (ToolStripItem var in tool.Items)
                    {
                        var.Enabled = true;
                    }
                }

                foreach (ToolStripPanelRow row in tsRight.Rows)
                {
                    foreach (ToolStrip var in row.Controls)
                    {
                        var.Enabled = true;
                    }
                }
            }
        }

        /// <summary>
        /// This method Adds the menu and the toolbars to the main application. Using the menu.txt from the resources any
        /// command that is in that text file is loaded to the main menu. If you want to add a command to the menu you simply
        /// change the menu.txt and also implement the command(like in the ExtraMethods). Also a Window list menu is added to the 
        /// Window menu already existed in the menu.txt. The top and the right(initially) toolbars are also added here by code.
        /// </summary>
        private void LoadMenusAndToolbars()
        {
            VectorDraw.Professional.Control.vdMenuStrip vdMenu = new VectorDraw.Professional.Control.vdMenuStrip();
            vdMenu.OnAddMenuButton += new VectorDraw.Professional.Control.vdMenuStrip.AddMenuButtonDelegate(vdMenu_OnAddMenuButton);
            vdMenu.CreateMenu(GetResourcesPath(), "menu.txt", ToolStripItemAlignment.Left);

            tsRight.Height = 400;

            tsTop.Dock = DockStyle.Top;
            tsTop.Orientation = Orientation.Horizontal;
            tsRight.Dock = DockStyle.Right;
            tsRight.Orientation = Orientation.Vertical;

            this.RightToolbarsPanel.Controls.Add(tsRight);

            this.Controls.Add(tsTop);
            tsRight.Paint += new PaintEventHandler(tsRight_Paint);
            tsRight.ControlRemoved += new ControlEventHandler(tsRight_ControlRemoved);
            this.RightToolbarsPanel.Width = this.RightToolbarsPanel.Controls[0].Width + 5;

            MenuStrip mainmenu = vdMenu.GetMenuStrip();
            if (HeadsUtils.Constants.BuildType == eHEADS_RELEASE_TYPE.DEMO)
            {
                ToolStripMenuItem tsmi = (ToolStripMenuItem)mainmenu.Items["Insert"];

                if (tsmi.DropDown.Items.Count > 3)
                {
                    tsmi.DropDown.Items[0].Visible = false;
                    //tsmi.DropDown.Items[1].Visible = false;
                    //tsmi.DropDown.Items[2].Visible = false;
                }
            }

            LoadToolBars(vdMenu);

            ////Add Window List
            ToolStripItem[] items = mainmenu.Items.Find("Window", false);
            if (items.GetLength(0) == 1)
            {
                ToolStripMenuItem window = items[0] as ToolStripMenuItem;
                if (window != null)
                {
                    window.DropDownItems.Add(new ToolStripSeparator());
                    mainmenu.MdiWindowListItem = window;
                }
            }

            //MainMenu.
            this.Controls.Add(mainmenu);
            this.MainMenuStrip = mainmenu;
            //Recent Files Menu
            AddToRecentFiles(null);
        }

        /// <summary>
        /// This method is called to Load the application's settings(toolbars,window state,properties list position etc...) from the UserData file.
        /// </summary>
        /// <param name="vdMenu"></param>
        private void LoadToolBars(VectorDraw.Professional.Control.vdMenuStrip vdMenu)
        {
            VectorDraw.Generics.vdArray<ToolStrip> toolbars = vdMenu.CreateToolbars();

            VisibleToolbarsFloatingMenu.ShowImageMargin = false;
            VisibleToolbarsFloatingMenu.ShowCheckMargin = true;
            if (VectorDraw.Professional.Utilities.vdGlobals.IsFileExist(GetResourcesPath() + ToolBarsFilename))
            {
                StreamReader stream = new StreamReader(GetResourcesPath() + ToolBarsFilename);
                ToolStripPanel panel = null;

                //RecentFiles.
                string line = stream.ReadLine().Trim();
                while (!line.StartsWith("MainSize:"))
                {
                    RecentFiles.AddItem(line);
                    line = stream.ReadLine().Trim();
                }

                if (line.StartsWith("MainSize"))
                {
                    string[] AppSize = line.Trim().Split(':');
                    this.Width = int.Parse(AppSize[1]);
                    this.Height = int.Parse(AppSize[2]);
                    this.WindowState = (FormWindowState)int.Parse(AppSize[3]);
                    this.Location = new Point(int.Parse(AppSize[4]), int.Parse(AppSize[5]));
                    line = stream.ReadLine().Trim();
                }

                if (line.StartsWith("ClintSize"))
                {
                    string[] ClientWindow = line.Split(':');
                    this.ChildSize.Width = int.Parse(ClientWindow[1]);
                    this.ChildSize.Height = int.Parse(ClientWindow[2]);
                    this.state = (FormWindowState)int.Parse(ClientWindow[3]);
                    this.ChildLocation = new Point(int.Parse(ClientWindow[4]), int.Parse(ClientWindow[5]));
                    line = stream.ReadLine().Trim();
                }

                if (line.StartsWith("CommandLine"))
                {
                    string[] commandlineHeight = line.Split(':');
                    int height = int.Parse(commandlineHeight[1]);
                    if (height == this.toolStrip1.Height)
                    {
                        vdCommandLine1.Visible = false;
                        panel1.Height = this.toolStrip1.Height;
                        splitter2.MinSize = toolStrip1.Height;
                        panel1.MaximumSize = new Size(panel1.Width, toolStrip1.Height);
                    }
                    else this.panel1.Height = height;

                    line = stream.ReadLine().Trim();
                }

                if (line.StartsWith("Property"))
                {
                    string[] ProperrtyListWidth = line.Split(':');
                    int width = int.Parse(ProperrtyListWidth[1]);
                    if (width == 0)
                    {
                        vdPropertyGrid1.Visible = false;
                        panel2.Visible = false;
                        splitter1.MinSize = 0;
                    }
                    else this.panel2.Width = width;
                }

                while (!stream.EndOfStream)
                {
                    line = stream.ReadLine().Trim();
                    if (line == "TopToolStrip") panel = tsTop;
                    if (line == "RightToolStrip") panel = tsRight;
                    string[] strs = line.Split(';');
                    int num = 0;
                    string name = "";
                    int x = 0, y = 0;
                    bool toolvisible = true;
                    foreach (string var in strs)
                    {
                        switch (num)
                        {
                            case 0: name = var; break;
                            case 1: try { x = int.Parse(var); }
                                catch { } break;
                            case 2: try { y = int.Parse(var); }
                                catch { } break;
                            case 3: try { toolvisible = bool.Parse(var); }
                                catch { } break;
                        }
                        num++;
                    }

                    if (name != "")
                    {
                        foreach (ToolStrip var in toolbars)
                        {
                            if (var.Name == name)
                            {
                                panel.Join(var, new Point(x, y));
                                var.Visible = toolvisible;

                                ToolStripMenuItem button = new ToolStripMenuItem(var.Name);
                                button.Name = var.Name;
                                button.DisplayStyle = ToolStripItemDisplayStyle.Text;
                                button.CheckOnClick = true;
                                button.Checked = toolvisible;
                                button.MouseDown += new MouseEventHandler(button_MouseDown);
                                VisibleToolbarsFloatingMenu.Items.Add(button);
                            }
                        }
                    }
                }
                stream.Close();
                //Add any additional toolbars that are not in the userData.txt
                foreach (ToolStrip var in toolbars)
                {
                    bool found = false;
                    foreach (ToolStripDropDownItem var1 in VisibleToolbarsFloatingMenu.Items)
                    {
                        if (var.Name == var1.Name) { found = true; break; }
                    }
                    if (!found)
                    {
                        tsTop.Join(var);

                        ToolStripMenuItem button = new ToolStripMenuItem(var.Name);
                        button.Name = var.Name;
                        button.DisplayStyle = ToolStripItemDisplayStyle.Text;
                        button.CheckOnClick = true;
                        button.Checked = true;
                        button.MouseDown += new MouseEventHandler(button_MouseDown);
                        VisibleToolbarsFloatingMenu.Items.Add(button);
                    }
                }
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                //Default Load of Menus
                //Add And arrange toolbars.

                //TOP 1 Row
                AddInitialToolbar(toolbars, "Osnaps", 0, 0, true);
                AddInitialToolbar(toolbars, "Edit", 0, 0, true);
                AddInitialToolbar(toolbars, "Zoom", 0, 0, true);
                AddInitialToolbar(toolbars, "Clip", 0, 0, true);
                AddInitialToolbar(toolbars, "File", 0, 0, true);
                //TOP 2 Row
                AddInitialToolbar(toolbars, "Modify", 0, 1, true);
                AddInitialToolbar(toolbars, "View 3D", 0, 1, true);
                AddInitialToolbar(toolbars, "Views", 0, 1, true);

                //Right 1 Row
                AddInitialToolbar(toolbars, "3D Surfaces", 1, 0, true);
                AddInitialToolbar(toolbars, "Draw", 1, 0, true);

                //Right 2 Row
                AddInitialToolbar(toolbars, "Dimensions", 1, 1, true);
                AddInitialToolbar(toolbars, "Blocks", 1, 1, true);
                AddInitialToolbar(toolbars, "Array Entities", 1, 1, true);

                //Invisible toolbars.
                AddInitialToolbar(toolbars, "UCS", 0, 0, false);
                AddInitialToolbar(toolbars, "Format", 0, 0, false);
                AddInitialToolbar(toolbars, "Utility", 0, 0, false);
            }

            VisibleToolbarsFloatingMenu.Items.Add(new ToolStripSeparator());

            string buttonname = "CommandLine";
            ToolStripMenuItem button1 = new ToolStripMenuItem(buttonname);
            button1.Name = buttonname;
            button1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            button1.CheckOnClick = true;
            button1.Checked = vdCommandLine1.Visible;
            button1.MouseDown += new MouseEventHandler(button_MouseDown);
            VisibleToolbarsFloatingMenu.Items.Add(button1);

            buttonname = "Properties List";
            button1 = new ToolStripMenuItem(buttonname);
            button1.Name = buttonname;
            button1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            button1.CheckOnClick = true;
            button1.Checked = vdPropertyGrid1.Visible;
            button1.MouseDown += new MouseEventHandler(button_MouseDown);
            VisibleToolbarsFloatingMenu.Items.Add(button1);

        }
        /// <summary>
        /// This method is used to position the toolbars when there is not present a properties file(the first time the application runs or if the file is deleted).
        /// </summary>
        /// <param name="toolbars">The collection of all toolbars.</param>
        /// <param name="name">The name of the toolbar to position.</param>
        /// <param name="Position">The position of the toolbar(top or right panel).</param>
        /// <param name="row">The row where the toolbar is going to be placed.</param>
        /// <param name="visible">True if the toolbar is going to be visible.</param>
        private void AddInitialToolbar(VectorDraw.Generics.vdArray<ToolStrip> toolbars, string name, int Position, int row, bool visible)
        {
            ToolStrip strip = null;
            foreach (ToolStrip var in toolbars)
            {
                if (var.Name == name) { strip = var; break; }
            }
            if (strip == null) return;
            strip.Visible = visible;
            if (Position == 0)
            {
                tsTop.Join(strip, row);
            }
            else if (Position == 1)
            {
                tsRight.Join(strip, row);
            }

            ToolStripMenuItem button = new ToolStripMenuItem(name);
            button.Name = name;
            button.DisplayStyle = ToolStripItemDisplayStyle.Text;
            button.CheckOnClick = true;
            button.Checked = visible;
            button.MouseDown += new MouseEventHandler(button_MouseDown);
            VisibleToolbarsFloatingMenu.Items.Add(button);
        }
        /// <summary>
        /// This method saves the application's settings(toolbars etc...) to the UserData file.
        /// </summary>
        private void SaveApplicationsSettings()
        {
            if (this.WindowState == FormWindowState.Minimized) return;
            //Save Top and right ToolPanel objects.
            StreamWriter stream = new StreamWriter(GetResourcesPath() + ToolBarsFilename, false);

            //RecentFiles.
            foreach (string var in RecentFiles)
            {
                stream.WriteLine(var);
            }

            stream.WriteLine("MainSize:" + this.Size.Width.ToString() + ":" + this.Size.Height.ToString() + ":" + ((int)this.WindowState).ToString() + ":" + this.Location.X.ToString() + ":" + this.Location.Y.ToString());
            Childform form1 = this.ActiveMdiChild as Childform;
            if (form1 != null)
                stream.WriteLine("ClintSize:" + form1.Size.Width.ToString() + ":" + form1.Size.Height.ToString() + ":" + ((int)form1.WindowState).ToString() + ":" + form1.Location.X.ToString() + ":" + form1.Location.Y.ToString());

            stream.WriteLine("CommandLineHeight:" + this.panel1.Height.ToString());
            if (panel2.Visible)
                stream.WriteLine("PropertyListWidth:" + this.panel2.Width.ToString());
            else
                stream.WriteLine("PropertyListWidth:0");

            stream.WriteLine("TopToolStrip");
            foreach (ToolStripPanelRow row in tsTop.Rows)
            {
                foreach (ToolStrip var in row.Controls)
                {
                    string tool = var.Name + ";" + var.Location.X.ToString() + ";" + var.Location.Y.ToString() + ";" + var.Visible.ToString();
                    stream.WriteLine(tool);
                }
            }

            stream.WriteLine("RightToolStrip");
            foreach (ToolStripPanelRow row in tsRight.Rows)
            {
                foreach (ToolStrip var in row.Controls)
                {
                    string tool = var.Name + ";" + var.Location.X.ToString() + ";" + var.Location.Y.ToString() + ";" + var.Visible.ToString();
                    stream.WriteLine(tool);
                }
            }

            stream.Close();

        }

        #region methods to draw our logo in mdi client window
        private System.Windows.Forms.MdiClient mdi = null;
        /// <summary>
        /// Initializes the Paint event for the MdiClient control(the area where the image is painted).
        /// </summary>
        private void MdiClientBackroundImage()
        {

            foreach (object var in Controls)
            {
                if (var is System.Windows.Forms.MdiClient)
                {
                    mdi = var as System.Windows.Forms.MdiClient;
                    break;
                }
            }
            if (mdi != null)
            {
                mdi.Paint += new PaintEventHandler(mdi_Paint);
            }
        }
        /// <summary>
        /// Calculate the position and draw the Logo image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mdi_Paint(object sender, PaintEventArgs e)
        {
            Control control = sender as Control;
            Bitmap logoBitmap = HeadsViewer.Properties.Resources.TechSoftLogo;
            logoBitmap.MakeTransparent(Color.FromArgb(0, 0, 255));
            double aspect = ((double)logoBitmap.Height / (double)logoBitmap.Width);
            int nw = (control.Width / 2);
            int nh = (int)(nw * aspect);
            int x = (control.Width - nw) / 2;
            int y = (control.Height - nh) / 2;
            e.Graphics.DrawImage(logoBitmap, new Rectangle(x, y, nw, nh));
            VectorDraw.Professional.Memory.vdMemory.Collect();
        }
        /// <summary>
        /// This event is used to redraw the image when the size of the client area is resized.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (mdi != null) mdi.Invalidate();
        }
        #endregion

        #region Commands
        static int commandExecute = VectorDraw.WinMessages.MessageManager.RegisterWindowMessage("CommandExecute");
        /// <summary>
        /// This event is fired when an item is clicked either at the menu or at the toolbars and the command is executed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void item_Click(object sender, EventArgs e)
        {
            string command = (string)((ToolStripItem)sender).Tag;
            commandLine.PostExecuteCommand(command);
        }
        /// <summary>
        /// This event is used in order to show the floating menu that controls the visibility of the toolbars when right
        /// click is pressed over the top panel or above any toolbar button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void item_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                VisibleToolbarsFloatingMenu.Show(System.Windows.Forms.Control.MousePosition);
        }
        /// <summary>
        /// This method creates a new MDI child window.
        /// </summary>
        void createForm()
        {
            Childform form = new Childform();
            form.MdiParent = this;
            form.Show();

            Propertiesform.Initialize(form.vdScrollableControl1.BaseControl.ActiveDocument, GetResourcesPath(), this.panel2);
            form.vdScrollableControl1.BaseControl.ActiveDocument.UndoHistory.PushEnable(false);
            form.vdScrollableControl1.BaseControl.ActiveDocument.Palette.Background = GeneralProperties.GetBackGround();
            form.vdScrollableControl1.BaseControl.ActiveDocument.ZoomExtents();
            form.vdScrollableControl1.BaseControl.ActiveDocument.UndoHistory.PopEnable();
        }

        /// <summary>
        /// This method is used to fix the sides ot the right/left toolbar panel and the properties list depending the passed parameter.
        /// </summary>
        /// <param name="side"></param>
        private void FixSide(GeneralProperties.Side side)
        {
            if (side == GeneralProperties.Side.Left)
            {
                this.panel2.Dock = DockStyle.Left;
                this.splitter1.Dock = DockStyle.Left;
                this.RightToolbarsPanel.Dock = DockStyle.Right;
            }
            else
            {
                this.panel2.Dock = DockStyle.Right;
                this.splitter1.Dock = DockStyle.Right;
                this.RightToolbarsPanel.Dock = DockStyle.Left;
            }
        }
        public VectorDraw.Professional.vdCommandLine.vdCommandLine commandLine { get { return vdCommandLine1; } }
        public vdPropertyGrid.vdPropertyGrid vdgrid { get { return vdPropertyGrid1; } }
        #endregion

        #region RecentFiles
        /// <summary>
        /// This event is fired when a recentfile button from the menu is clicked and opens the file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RecentFilesClick(object sender, EventArgs e)
        {
            ToolStripItem item = sender as ToolStripItem;
            if (item != null)
            {
                string filename = item.Text;
                createForm();
                Childform form = this.ActiveMdiChild as Childform;
                VectorDraw.Professional.vdObjects.vdDocument doc = form.vdScrollableControl1.BaseControl.ActiveDocument;


                bool successopen = File.Exists(filename);
                if (successopen)
                {
                    successopen = doc.Open(filename);
                }

                if (!successopen)
                {
                    System.Windows.Forms.MessageBox.Show("Error openning " + filename, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    form.Close();
                    return;
                }
                //set the working dir path on opening a recent file
                this.AppDataPath = System.IO.Path.GetDirectoryName(filename);

                doc.Redraw(false);
                //Updates the window list
                ActivateMdiChild(null);
                ActivateMdiChild(this.ActiveMdiChild);
            }
        }
        /// <summary>
        /// Inserts the passed string filename value to the recent files list and menu.
        /// </summary>
        /// <param name="value"></param>
        private void AddToRecentFiles(string value)
        {
            if ((value != null) && (value != ""))
            {
                bool exists = false;
                foreach (string var in RecentFiles)
                {
                    if (var == value) { exists = true; break; }
                }

                if (!exists)
                    RecentFiles.InsertAt(0, value);
            }

            if (RecentFiles.Count > RecentFilesCount) RecentFiles.RemoveLast();

            MenuStrip mainmenu = this.MainMenuStrip;
            ToolStripItem[] Fileitems = mainmenu.Items.Find("File", false);
            if (Fileitems.GetLength(0) == 1)
            {
                ToolStripMenuItem file = Fileitems[0] as ToolStripMenuItem;
                if (file != null)
                {
                    ToolStripItem[] Recent = file.DropDownItems.Find("RecentFiles", false);
                    if (Recent != null && Recent.Length != 0)
                    {
                        ToolStripMenuItem submenu = Recent[0] as ToolStripMenuItem;
                        if (submenu != null)
                        {
                            submenu.DropDownItems.Clear();
                            foreach (string var in RecentFiles)
                            {
                                submenu.DropDownItems.Add(var, null, new EventHandler(RecentFilesClick));
                            }
                        }
                    }
                    else
                    {
                        ToolStripMenuItem submenu = new ToolStripMenuItem("RecentFiles");
                        submenu.Name = submenu.Text = "RecentFiles";
                        if (RecentFiles.Count != 0)
                        {
                            foreach (string var in RecentFiles)
                            {
                                submenu.DropDownItems.Add(var, null, new EventHandler(RecentFilesClick));
                            }

                            file.DropDownItems.Insert(file.DropDownItems.Count - 2, submenu);
                        }
                    }
                }
            }
        }
        #endregion

        #region Bottom Toolbar Buttons
        /// <summary>
        /// This event is fired when the GRID button at the bottom of the applications is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolGrid_Click(object sender, EventArgs e)
        {
            Childform form = this.ActiveMdiChild as Childform;
            VectorDraw.Professional.vdObjects.vdDocument doc = form.vdScrollableControl1.BaseControl.ActiveDocument;
            doc.GridMode = !doc.GridMode;
            if (doc.GridMode)
            {
                this.toolGrid.Checked = true; this.toolGrid.Text = "GRID ON";
            }
            else
            {
                this.toolGrid.Checked = false; this.toolGrid.Text = "GRID OFF";
            }
        }
        /// <summary>
        /// This event is fired when the SNAP button at the bottom of the applications is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolSnap_Click(object sender, EventArgs e)
        {
            Childform form = this.ActiveMdiChild as Childform;
            VectorDraw.Professional.vdObjects.vdDocument doc = form.vdScrollableControl1.BaseControl.ActiveDocument;
            doc.SnapMode = !doc.SnapMode;
            if (doc.SnapMode)
            {
                this.toolSnap.Checked = true; this.toolSnap.Text = "SNAP ON";
            }
            else
            {
                this.toolSnap.Checked = false; this.toolSnap.Text = "SNAP OFF";
            }
        }
        /// <summary>
        /// This event is fired when the ORTHO button at the bottom of the applications is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolOrtho_Click(object sender, EventArgs e)
        {
            Childform form = this.ActiveMdiChild as Childform;
            VectorDraw.Professional.vdObjects.vdDocument doc = form.vdScrollableControl1.BaseControl.ActiveDocument;
            doc.OrthoMode = !doc.OrthoMode;
            if (doc.OrthoMode)
            {
                this.toolOrtho.Checked = true; this.toolOrtho.Text = "ORTHO ON";
            }
            else
            {
                this.toolOrtho.Checked = false; this.toolOrtho.Text = "ORTHO OFF";
            }
        }
        /// <summary>
        /// This event is fired when the OSNAP button at the bottom of the applications is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolOsnap_Click(object sender, EventArgs e)
        {
            Childform form = this.ActiveMdiChild as Childform;
            VectorDraw.Professional.vdObjects.vdDocument doc = form.vdScrollableControl1.BaseControl.ActiveDocument;
            VectorDraw.Professional.Dialogs.OSnapDialog.Show(doc, doc.ActionControl);
        }
        #endregion

        #region Aerial View Methods
        /// <summary>
        /// The Size of the Red Box of the Aerial View controlled by the trackBar1 slider.
        /// </summary>
        int SIZE = 20;
        /// <summary>
        /// A boolean value that represents if the Aerial view bos is locked or not. This value is controlled by clicking on the Aerial view.
        /// </summary>
        bool lEnable = true;
        /// <summary>
        /// Values that keep the current center of the aerial red box.
        /// </summary>
        public int PrevX = -1; public int PrevY = -1;
        /// <summary>
        /// Creates an Image from the ActiveLayout entities and draws it to the PictureBox of the Aerial View.
        /// </summary>
        public void UpdatePictureBox(bool Clear)
        {

            Childform form = this.ActiveMdiChild as Childform;
            if (form == null) return;
            VectorDraw.Professional.vdObjects.vdDocument doc = form.vdScrollableControl1.BaseControl.ActiveDocument;
            VectorDraw.Geometry.Box bbox = doc.ActiveLayOut.Entities.GetBoundingBox(true, false);

            Image img = new Bitmap(AerialView.Width, AerialView.Height);
            System.Drawing.Graphics graph = Graphics.FromImage(img);
            if (bbox.IsEmpty || Clear)
            {
                graph.Clear(doc.Palette.Background);
            }
            else
            {
                doc.ActiveLayOut.RenderToGraphics(graph, bbox, img.Width, img.Height);
            }
            AerialView.Image = img;
            AerialView.Update();
        }
        /// <summary>
        /// Updates the Image of the Aerial View when the Update Button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AerialViewUpdate_Click(object sender, EventArgs e)
        {
            UpdatePictureBox(false);
        }
        private void UpdateVdraw(VectorDraw.Professional.vdObjects.vdDocument doc, double x, double y)
        {

            //Calculate Viewcenter and viewSize.
            VectorDraw.Geometry.Box ext = doc.ActiveLayOut.Entities.GetBoundingBox(true, false);
            double pureX = x;
            double pureY = AerialView.Height - y;
            VectorDraw.Geometry.gPoint center = null;
            double pixelSize = CalcDCPixelSize(ext, AerialView.Width, AerialView.Height, ref pureX, ref pureY, ref center);
            doc.ActiveLayOut.ViewCenter = new VectorDraw.Geometry.gPoint(center);
            doc.ActiveLayOut.ViewSize = SIZE * pixelSize;
            doc.Redraw(true);
        }
        /// <summary>
        /// Updates the scrollable control when the mouse is moved to the PictureBox of the Aerial view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AerialView_MouseMove(object sender, MouseEventArgs e)
        {
            if (AerialView.Image == null) UpdatePictureBox(false);
            if (!lEnable) return;

            Childform form = this.ActiveMdiChild as Childform;
            if (form != null)
            {
                VectorDraw.Professional.vdObjects.vdDocument doc = form.vdScrollableControl1.BaseControl.ActiveDocument;

                UpdateVdraw(doc, e.X, e.Y);

                //Draw the Red Box.
                PrevX = e.Location.X;
                PrevY = e.Location.Y;
                int w = 0;
                if (form.vdScrollableControl1.BaseControl.Width != 0 || form.vdScrollableControl1.BaseControl.Height != 0)
                {
                    w = (this.SIZE * form.vdScrollableControl1.BaseControl.Width) / form.vdScrollableControl1.BaseControl.Height;
                }

                Rectangle rc = new Rectangle(new Point(e.Location.X - w / 2, e.Location.Y - SIZE / 2), new Size(w, SIZE));
                Graphics gr = AerialView.CreateGraphics();
                gr.DrawImage(AerialView.Image, new Point());
                gr.DrawRectangle(new Pen(Color.Red), rc);
                gr.Dispose();
            }
        }
        /// <summary>
        /// Calculates the center and pX,pY , PixelSize variables from the given parameters.
        /// </summary>
        /// <param name="bbox"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <param name="center"></param>
        /// <returns></returns>
        private double CalcDCPixelSize(VectorDraw.Geometry.Box bbox, double width, double height, ref double pX, ref double pY, ref VectorDraw.Geometry.gPoint center)
        {
            double w = bbox.Width;
            double h = bbox.Height;

            double l1 = width / w;
            double l2 = height / h;
            double l = System.Math.Min(l1, l2);

            double vs = height / l + 2.0 / l;

            double pixelSize = vs / height;
            double nh = pixelSize * height;
            double nW = pixelSize * width;

            double cenx = w / 2.0 + bbox.Left;
            double ceny = h / 2.0 + bbox.Bottom;

            double minx = cenx - (nW / 2.0);
            double miny = ceny - (nh / 2.0);
            double maxx = cenx + (nW / 2.0);
            double maxy = ceny + (nh / 2.0);

            double pcenx = width / 2.0;
            double pceny = height / 2.0;

            double pdx = pX - pcenx;
            double pdy = pY - pceny;

            center = new VectorDraw.Geometry.gPoint(cenx + pdx * pixelSize, ceny + pdy * pixelSize);
            return pixelSize;
        }
        /// <summary>
        /// Calculate the pX,pY from the given parameters. pX and pY represent the center of the Red box of the Aerial view used in AfterModfify Event of the ChildForm.
        /// </summary>
        /// <param name="bbox"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="pX"></param>
        /// <param name="pY"></param>
        /// <param name="center"></param>
        public void CalculatePictureCenter(VectorDraw.Geometry.Box bbox, double width, double height, ref double pX, ref double pY, VectorDraw.Geometry.gPoint center)
        {
            double w = bbox.Width;
            double h = bbox.Height;

            if (w == 0.0 || h == 0.0)
            {
                pX = -1;
                pY = -1;
                return;
            }

            double l1 = width / w;
            double l2 = height / h;
            double l = System.Math.Min(l1, l2);

            double vs = height / l + 2.0 / l;

            double pixelSize = vs / height;
            double nh = pixelSize * height;
            double nW = pixelSize * width;

            double cenx = w / 2.0 + bbox.Left;
            double ceny = h / 2.0 + bbox.Bottom;

            double minx = cenx - (nW / 2.0);
            double miny = ceny - (nh / 2.0);
            double maxx = cenx + (nW / 2.0);
            double maxy = ceny + (nh / 2.0);

            double pcenx = width / 2.0;
            double pceny = height / 2.0;

            double pdx = (center.x - cenx) / pixelSize;
            double pdy = (center.y - ceny) / pixelSize;

            pX = pdx + pcenx;
            pY = AerialView.Height - pdy - pceny;
        }
        /// <summary>
        /// Draws the red rectangle using the PrevX and PrevY as center of the box.
        /// </summary>
        public void DrawRect()
        {
            if (AerialView.Image == null) UpdatePictureBox(false);
            if (PrevX != -1 && PrevY != -1)
            {
                Childform form = this.ActiveMdiChild as Childform;
                VectorDraw.Professional.vdObjects.vdDocument doc = form.vdScrollableControl1.BaseControl.ActiveDocument;

                int w = (SIZE * form.vdScrollableControl1.BaseControl.Width) / form.vdScrollableControl1.BaseControl.Height;
                Rectangle rc = new Rectangle(new Point(PrevX - w / 2, PrevY - SIZE / 2), new Size(w, SIZE));
                Graphics graph = AerialView.CreateGraphics();
                graph.DrawImage(AerialView.Image, new Point());
                graph.DrawRectangle(new Pen(Color.Red), rc);
                graph.Dispose();
            }
        }
        /// <summary>
        /// Update the Size of the Box when the trackBar is moved by the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Childform form = this.ActiveMdiChild as Childform;
            VectorDraw.Professional.vdObjects.vdDocument doc = form.vdScrollableControl1.BaseControl.ActiveDocument;

            SIZE = trackBar1.Value;
            UpdateVdraw(doc, PrevX, PrevY);
            DrawRect();

        }
        /// <summary>
        /// Enable or Disable the red box of the Aerial View when the pictureBox is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AerialView_MouseClick(object sender, MouseEventArgs e)
        {
            lEnable = !lEnable;
        }
        #endregion

        private IHeadsFunctions LoadHeadsFunction()
        {
            string strPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(System.IO.Path.Combine(strPath, "HeadsFunctions1.dll"));
            return (IHeadsFunctions)assembly.CreateInstance("HeadsFunctions1.CHeadsFunctionFactory");
        }

#if DEBUG
        string strWorkingDir = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Heads\Test data";
#else
            string strWorkingDir = "";
#endif

        string EnvVerName
        {
            get
            {
                string strEnvVer = string.Empty;
                if (this.ReleaseType == eHEADS_RELEASE_TYPE.PROFESSIONAL)
                {
                    strEnvVer = "SURVEY";
                }
                else if (this.ReleaseType == eHEADS_RELEASE_TYPE.DEMO)
                {
                    strEnvVer = "DEM0";
                }
                else if (this.ReleaseType == eHEADS_RELEASE_TYPE.ASTRA)
                {
                    strEnvVer = "ASTRA";
                }
                return strEnvVer;
            }
        }

        void SetWorkingDirFromEnvVariable()
        {
            string strFileName = System.Environment.GetEnvironmentVariable(this.EnvVerName);
            if (strFileName != null)
            {
                if (strFileName != "" && File.Exists(strFileName))
                {
                    this.AppDataPath = Path.GetDirectoryName(strFileName);
                }
            }
        }

        void DeleteTmpFiles(string strPath)
        {
            if (strPath.Trim() != "" && Directory.Exists(strPath))
            {
                string[] arrfiles = Directory.GetFiles(strPath, "*.TMP");
                foreach (string strFile in arrfiles)
                {
                    ViewerUtils.DeleteFileIfExists(strFile);
                }
            }
        }


        #region IHeadsApplication Members

        public IHeadsDocument ActiveDocument
        {
            get
            {

                IHeadsDocument headsdoc = null;
                if (ActiveMdiChild != null)
                {
                    headsdoc = (HeadsViewer.Childform)this.ActiveMdiChild;
                }

                return headsdoc;
            }
        }

        public string AppDataPath
        {
            get
            {
                return this.strWorkingDir;
            }
            set
            {
                if (this.strWorkingDir.Trim().ToLower() != value.Trim().ToLower())
                {
                    this.DeleteTmpFiles(this.strWorkingDir);
                }
                this.strWorkingDir = value;
            }
        }

        public void ZoomExtents()
        {
            if (this.ActiveMdiChild != null)
            {
                this.commandLine.ExecuteCommand("ze");
            }
        }

        public void ZoomWindow(HeadsUtils.CPoint3D ptLowerLeft, HeadsUtils.CPoint3D ptUpperRight)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public eHEADS_RELEASE_TYPE ReleaseType
        {
            get
            {
                return HeadsUtils.Constants.BuildType;
            }
        }
        public string ApplicationName
        {
            get
            {
                return HeadsUtils.Constants.ProductTitle;
            }
        }
        public string ApplicationTitle
        {
            get
            {
                return HeadsUtils.Constants.ProductTitle;
            }
        }
        #endregion

        #region CHIRANJIT
        public void ASTRA_CODE_SET(string fileName)
        {
            string code, item, part;
            string draw_code = "";

            //drawing_path


            code = "";
            item = "";
            part = "";
            //fileName = "";
            string path = "";

            //fileName = Environment.GetEnvironmentVariable("ASTRA");

            List<string> lstStr = new List<string>(File.ReadAllLines(fileName));

            MyStrings mList = null;
            string kStr = "";
            for (int i = 0; i < lstStr.Count; i++)
            {
                kStr = lstStr[i].Trim().TrimEnd().TrimStart().ToUpper();

                if (!kStr.StartsWith("PATH"))
                {
                    mList = new MyStrings(MyStrings.RemoveAllSpaces(kStr), '=');
                }
                else
                {
                    mList = new MyStrings(kStr, '=');
                    kStr = kStr.Replace("PATH =", "");
                    mList.StringList[1] = kStr;

                }


                switch (mList.StringList[0].Trim().TrimEnd().TrimStart().ToUpper())
                {
                    case "ASTRA":
                        code = "ASTRA";
                        break;
                    case "ASTRA_MOVINGLOAD":
                        code = "ASTRA_MOVINGLOAD";
                        break;
                    case "ASTRA_INPUT":
                        code = "ASTRA_INPUT";
                        break;
                    case "CODE":
                        code = "ASTRA_DESIGN";
                        mList = new MyStrings(mList.StringList[1], ' ');
                        item = mList.StringList[0];
                        if (mList.Count > 1)
                            part = mList.StringList[1];
                        break;
                    case "ASTRA_DRAWINGS":
                        draw_code = "ASTRA_DRAWINGS";
                        break;
                    case "DRAWINGS":
                        draw_code = "DRAWINGS";
                        break;
                    case "PATH":
                        if (draw_code == "DRAWINGS")
                        {
                            this.drawingPath = mList.StringList[1];
                            if (!Directory.Exists(drawingPath)) Directory.CreateDirectory(drawingPath);

                        }
                        else if (draw_code == "ASTRA_DRAWINGS")
                        {
                            this.drawingCode = mList.StringList[1];
                        }
                        else
                        {
                            path = mList.StringList[1];
                        }
                        break;
                }
            }

            switch (code)
            {
                case "ASTRA":
                    fName = path;
                    commandLine.ExecuteCommand("drawStructure");
                    break;

                case "ASTRA_INPUT":
                    fName = path;
                    commandLine.ExecuteCommand("astraInput");
                    break;
                case "ASTRA_MOVINGLOAD":
                    fName = path;
                    //DrawStructure();
                    //commandLine.ExecuteCommand("drawStructure");
                    if (File.Exists(Path.Combine(Path.GetDirectoryName(path), "ANALYSIS_REP.TXT")))
                    {
                        //commandLine.ExecuteCommand("LoadDeflection");
                        //Chiranjit [2011 12 09]
                        commandLine.ExecuteCommand("ViewMovingLoad");
                      //  MessageBox.Show("Select menu item \"View3D>>Rotate3D\" by pressing the left mouse button rotate the structure." +
                      //"\n\nRotate the scroll wheel of the mouse for 'Zoom In' and 'Zoom Out'" +
                      //"\n\nPress the scroll wheel of the mouse and move the drawing in the screen." +
                      //"\n\nPress 'Esc' key to come out from the 'Rotate3D' mode" +
                      //"\n\nTo control the text in 'Joint Nos' and 'Member Nos' use the menu item 'Details'." +
                      //"\n\nFor other controls Use the panel 'Text Size' at the bottom of the screen.",
                      //"ASTRA");
                    }
                    break;
                case "ASTRA_DESIGN":
                    switch (item)
                    {
                        #region SLAB01
                        case "SLAB01":
                            {
                                switch (part)
                                {
                                    case "VIEW":
                                        {
                                            RccSlab01_View(path);
                                        }
                                        break;
                                    case "DRAWING":
                                        {
                                            RccSlab01_Drawing(path);

                                        }
                                        break;

                                    case "BOQ":
                                        {
                                            RccSlab01_BoQ(path);
                                        }
                                        break;
                                }
                            }
                            break;

                        #endregion

                        #region ONE_WAY_RCC_SLAB
                        case "ONE_WAY_RCC_SLAB":
                            {
                                switch (part)
                                {
                                    case "VIEW":
                                        {
                                            Rcc_Slab_One_Way_View(path);
                                        }
                                        break;
                                    case "DRAWING":
                                        {
                                            Rcc_Slab_One_Way_Drawing(path);
                                        }
                                        break;
                                    case "BOQ":
                                        {
                                            Rcc_Slab_One_Way_BoQ(path);
                                        }
                                        break;
                                }
                            }
                            break;
                        #endregion

                        #region ONE_WAY_CONTINUOUS_RCC_SLAB
                        case "ONE_WAY_CONTINUOUS_RCC_SLAB":
                            {
                                switch (part)
                                {
                                    case "VIEW":
                                        {
                                            Rcc_Slab_One_Way_Continuous_View(path);
                                        }
                                        break;
                                    case "DRAWING":
                                        {
                                            Rcc_Slab_One_Way_Continuous_Drawing(path);
                                        }
                                        break;

                                    case "BOQ":
                                        {
                                            Rcc_Slab_One_Way_Continuous_BoQ(path);
                                        }
                                        break;
                                }
                            }
                            break;
                        #endregion

                        #region TWO_WAY_RCC_SLAB
                        case "TWO_WAY_RCC_SLAB":
                            {
                                switch (part)
                                {
                                    case "VIEW":
                                        {
                                            Rcc_Slab_Two_Way_View(path);
                                        }
                                        break;
                                    case "DRAWING":
                                        {
                                            Rcc_Slab_Two_Way_Drawing(path);
                                        }
                                        break;

                                    case "BOQ":
                                        {
                                            Rcc_Slab_Two_Way_BoQ(path);
                                        }
                                        break;
                                }
                            }
                            break;
                        #endregion

                        #region DESIGN_REPORT
                        case "HYDROGRAPH":
                            HYDROGRAPH(path);
                            break;
                        default:
                            Open_Interactive_Drawing(item, path);
                            break;
                        #endregion
                    }
                    break;
            }
            if (draw_code == "ASTRA_DRAWINGS")
            {
                try
                {

                    if (File.Exists(path))
                    {
                        path = Path.GetDirectoryName(path);
                        if (Path.GetFileName(path) == "ASTRASYS")
                            path = Path.GetDirectoryName(path);
                    }


                    if (Directory.Exists(path))
                    {


                        if (Directory.Exists(path) && Directory.Exists(drawingPath))
                        {

                            foreach (string s in Directory.GetFiles(path))
                            {
                                kStr = Path.Combine(DrawingPath, Path.GetFileName(s));
                                if (Path.GetExtension(s).ToUpper() == ".DWG" ||
                                    Path.GetExtension(s).ToUpper() == ".VDCL" ||
                                    Path.GetExtension(s).ToUpper() == ".DXF" ||
                                    Path.GetExtension(s).ToUpper() == ".VDML")
                                {
                                    File.Copy(s, kStr, true);
                                }
                            }
                        }

                        MyStrings mlist = new MyStrings(DrawingCode, ',');

                        //design_path = Des_Drawngs.Get_Default_Drawing_Path(mlist.StringList[0]);
                        foreach (string str in mlist.StringList)
                        {
                            this.drawingCode = Des_Drawngs.Get_Default_Drawing_Path(str);
                            if (Directory.Exists(this.drawingCode))
                            {
                                foreach (string s in Directory.GetFiles(this.drawingCode))
                                {
                                    if (Directory.Exists(drawingPath))
                                        kStr = Path.Combine(drawingPath, Path.GetFileName(s));
                                    else
                                        kStr = Path.Combine(path, Path.GetFileName(s));
                                    if (kStr.ToUpper().Contains("INTERACTIVE"))
                                    {
                                        if (File.Exists(kStr)) continue;
                                    }
                                    File.Copy(s, kStr, true);
                                }
                            }

                        }
                        if (Directory.Exists(drawingPath)) this.drawingCode = drawingPath;
                        else this.drawingCode = path;

                        //this.drawingCode = path;
                    }
                }
                catch (Exception ex) { }

                OpenMultiDrawings();
            }
        }
        #endregion


        #region IOpenDrawings Members


        public void CloseAllDrawings()
        {
            try
            {
                Childform chld = null;
                while (this.MdiChildren.Length != 0)
                {
                    chld = this.MdiChildren[0] as Childform;
                    vdDocument vDoc = chld.vdScrollableControl1.BaseControl.ActiveDocument;
                    vDoc.SaveAs(Path.Combine(Application.StartupPath, "test.vdml"));
                    chld.Close();
                }
                File.Delete(Path.Combine(Application.StartupPath, "test.vdml"));
                OnFileNew();
            }
            catch (Exception ex) { }
        }

        #endregion

        private void tmr_check_hasp_Tick(object sender, EventArgs e)
        {
            if (HASP_Lock.IsProfessionalVersion())
            {
                HeadsUtils.Constants.BuildType = eHEADS_RELEASE_TYPE.PROFESSIONAL;

            }
            else
            {
                HeadsUtils.Constants.BuildType = eHEADS_RELEASE_TYPE.DEMO;
            }
        }

        #region IOpenDrawings Members

        
        
        /// <summary>
        /// Convert All open Drawings to the given Extension.
        /// </summary>
        /// <param name="ext">Extension of the Drawing files. Like DWG; DXF</param>
        /// <returns>Return number of count save file.</returns>
        /// 
        public int Convert_All_Drawings(string ext, ProgressBar pb, ref ListBox lst_drawings)
        {
            //if (HeadsUtils.Constants.BuildType == eHEADS_RELEASE_TYPE.DEMO)
            //{
            //    MessageBox.Show(this, "This feature is not available in Unauthorize Version of ASTRA Pro", "ASTRA Pro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return -1;
            //}
            pb.Visible = true;
           
            Childform chld = null;

            string src_file = "";
            string dest_file = "";
            if (ext.StartsWith(".")) ext = ext.Remove(0);
            int count = 0;
            pb.Minimum = 0;
            pb.Maximum = this.MdiChildren.Length;
            //pb.Value = 1;
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                try
                {
                    pb.Value = i + 1;
                    chld = this.MdiChildren[i] as Childform;
                    src_file = chld.vdScrollableControl1.BaseControl.ActiveDocument.FileName;

                    //dest_file = Path.Combine(Path.GetDirectoryName(src_file), ext.ToUpper() + " DRAWINGS");
                    dest_file =Path.GetDirectoryName(src_file);
                    if (!Directory.Exists(dest_file)) Directory.CreateDirectory(dest_file);
                    dest_file = Path.Combine(dest_file, Path.GetFileNameWithoutExtension(src_file) + "." + ext);

                    if (src_file.ToUpper() != dest_file.ToUpper())
                    {
                        if (File.Exists(dest_file))
                            File.Delete(dest_file);
                        if (chld.vdScrollableControl1.BaseControl.ActiveDocument.SaveAs(dest_file))
                        {
                            lst_drawings.SelectedIndex = i;
                            File.Delete(src_file);
                            count++;
                        }
                    }
                    //VectorDraw.Professional.ActionUtilities.vdCommandAction.View3D_VTop(chld.vdScrollableControl1.BaseControl.ActiveDocument);
                }
                catch (Exception ex) { }
            }

            pb.Visible = false;

            return count;
        }

        #endregion
    }
    public class DesignDrawings
    {
        Hashtable hash_Interactive;
        Hashtable hash_DefaultDrawing;

        public DesignDrawings(string file_path)
        {
            hash_DefaultDrawing = new Hashtable();
            hash_Interactive = new Hashtable();
            Read_From_file(file_path);
        }
        public void Read_From_file(string file_name)
        {
            hash_Interactive.Clear();
            hash_DefaultDrawing.Clear();

            List<string> list_cont = new List<string>(File.ReadAllLines(file_name));

            MyStrings mlist = null;
            string kStr = "";
            bool drw_flag = false;
            bool drw_interactive_flag = false;
            bool drw_default_drawing_flag = false;

            for (int i = 0; i < list_cont.Count; i++)
            {
                kStr = list_cont[i].ToUpper();
                mlist = new MyStrings(kStr, '=');
                if (mlist.StringList[0] == "<DRAWING>")
                {
                    drw_flag = true; continue;
                }
                else if (mlist.StringList[0] == "<ENDDRAWING>")
                {
                    drw_flag = false; continue;
                }

                if (mlist.StringList[0] == "<INTERACTIVE>")
                {
                    drw_interactive_flag = true; continue;
                }
                else if (mlist.StringList[0] == "<ENDINTERACTIVE>")
                {
                    drw_interactive_flag = false; continue;
                }

                if (mlist.StringList[0] == "<DEFAULTDRAWING>")
                {
                    drw_default_drawing_flag = true; continue;
                }
                else if (mlist.StringList[0] == "<ENDDEFAULTDRAWING>")
                {
                    drw_default_drawing_flag = false; continue;
                }

                if (drw_interactive_flag)
                {
                    if (mlist.Count > 1)
                        Add_Interactive_Drawing(mlist.StringList[0].Trim().TrimStart().TrimEnd(), mlist.StringList[1]);
                }
                if (drw_default_drawing_flag)
                {
                    if (mlist.Count > 1)
                        Add_Default_Drawing(mlist.StringList[0].Trim().TrimStart().TrimEnd(), mlist.StringList[1]);
                }

            }
        }
        public void Add_Interactive_Drawing(string _cmdString, string drawing_file_path)
        {
            hash_Interactive.Add(_cmdString, Path.Combine(Application.StartupPath, "DRAWINGS\\" + drawing_file_path));
        }
        public string Get_Interactive_Drawing_Path(string _cmdString)
        {
            return (string)hash_Interactive[_cmdString];
        }
        public void Add_Default_Drawing(string _cmdString, string drawing_file_path)
        {
            hash_DefaultDrawing.Add(_cmdString, Path.Combine(Application.StartupPath, "DRAWINGS\\" + drawing_file_path));
        }
        public string Get_Default_Drawing_Path(string _cmdString)
        {
            return (string)hash_DefaultDrawing[_cmdString];
        }
    }


}