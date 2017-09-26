namespace HEADSNeed.ASTRA.ASTRAForms
{
    partial class frm_ASTRA_Analysis
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {


                vdScrollableControl1.BaseControl.ActiveDocument.ActionRemoveAll(false);
                vdScrollableControl2.BaseControl.ActiveDocument.ActionRemoveAll(false);
                vdScrollableControl1.BaseControl.ActiveDocument.ClearAll();
                vdScrollableControl2.BaseControl.ActiveDocument.ClearAll();


                vdScrollableControl1.BaseControl.vdMouseDown -= new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
                vdScrollableControl1.BaseControl.vdMouseUp -= new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);
                vdScrollableControl2.BaseControl.vdMouseDown -= new VectorDraw.Professional.Control.MouseDownEventHandler(BaseControl_vdMouseDown);
                vdScrollableControl2.BaseControl.vdMouseUp -= new VectorDraw.Professional.Control.MouseUpEventHandler(BaseControl_vdMouseUp);


            }
            catch (System.Exception exx) { }

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);

        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle39 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle40 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle41 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle42 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle43 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle44 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle45 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle46 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle47 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle48 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle49 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle50 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle51 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle52 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle53 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle54 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle55 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle56 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle57 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle58 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle59 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle60 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle61 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle62 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle63 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle64 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle65 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle66 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle67 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle68 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ASTRA_Analysis));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_close = new System.Windows.Forms.Button();
            this.cmbLengthUnit = new System.Windows.Forms.ComboBox();
            this.label77 = new System.Windows.Forms.Label();
            this.cmb_structure_type = new System.Windows.Forms.ComboBox();
            this.label78 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbForceUnit = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtUserTitle = new System.Windows.Forms.TextBox();
            this.btn_open_ana_rep = new System.Windows.Forms.Button();
            this.btn_process_analysis = new System.Windows.Forms.Button();
            this.tc1 = new System.Windows.Forms.TabControl();
            this.tab_forces = new System.Windows.Forms.TabPage();
            this.tab_load_deflection = new System.Windows.Forms.TabPage();
            this.lblLoadCase = new System.Windows.Forms.Label();
            this.grb_Auto = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbInterval = new System.Windows.Forms.ComboBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnAutoNext = new System.Windows.Forms.Button();
            this.btnAutoPrev = new System.Windows.Forms.Button();
            this.txtDefFactor = new System.Windows.Forms.TextBox();
            this.grb_manual = new System.Windows.Forms.GroupBox();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.lblFactor = new System.Windows.Forms.Label();
            this.cmbLoadCase = new System.Windows.Forms.ComboBox();
            this.tab_max_force = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.tab_moving_load = new System.Windows.Forms.TabPage();
            this.grb_moving_controls = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lbl_distance = new System.Windows.Forms.Label();
            this.txt_dist = new System.Windows.Forms.TextBox();
            this.cmb_mov_loadcase = new System.Windows.Forms.ComboBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cmb_mov_time = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_mov_stop = new System.Windows.Forms.Button();
            this.btn_mov_run = new System.Windows.Forms.Button();
            this.btn_mov_pause = new System.Windows.Forms.Button();
            this.tab_envelop = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbtn_env_bottom = new System.Windows.Forms.RadioButton();
            this.rbtn_env_top = new System.Windows.Forms.RadioButton();
            this.txt_env_mnos = new System.Windows.Forms.TextBox();
            this.btn_env_show = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_text_size = new System.Windows.Forms.ComboBox();
            this.chk_suprts = new System.Windows.Forms.CheckBox();
            this.chk_elems = new System.Windows.Forms.CheckBox();
            this.chk_mems = new System.Windows.Forms.CheckBox();
            this.chkJoint = new System.Windows.Forms.CheckBox();
            this.chkLocal = new System.Windows.Forms.CheckBox();
            this.chkGlobal = new System.Windows.Forms.CheckBox();
            this.sc1 = new System.Windows.Forms.SplitContainer();
            this.tc_pp_panel = new System.Windows.Forms.TabControl();
            this.tab1_geometry = new System.Windows.Forms.TabPage();
            this.tc3 = new System.Windows.Forms.TabControl();
            this.tab2_joints = new System.Windows.Forms.TabPage();
            this.dgv_joints = new System.Windows.Forms.DataGridView();
            this.col_jno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_x = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_z = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_support = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tab2_members = new System.Windows.Forms.TabPage();
            this.dgvMemberGrid = new System.Windows.Forms.DataGridView();
            this.dgTxtMemberNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgTxtMember = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgTxtNode1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgTxtNode2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgTxtYD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgTxtZD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgTxtArea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgTxtIX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgTxtIY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgTxtIZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgTxtE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgTxtDEN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgTxtPR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgTxtMAT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tab2_elements = new System.Windows.Forms.TabPage();
            this.dgvElementGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tab1_loading = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tv_loads = new System.Windows.Forms.TreeView();
            this.tab1_forces = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_3_joint_frc = new System.Windows.Forms.TabPage();
            this.dgv_joint_frcs = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn29 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tab_3_mem_frc = new System.Windows.Forms.TabPage();
            this.dgv_beam_frcs = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tab_3_truss_frc = new System.Windows.Forms.TabPage();
            this.dgv_truss_frcs = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tab1_load_deflection = new System.Windows.Forms.TabPage();
            this.dgv_node_disp = new System.Windows.Forms.DataGridView();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txt_max_deflection_load = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_max_deflection_node = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_max_deflection = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tab1_max_force = new System.Windows.Forms.TabPage();
            this.dgv_max_frc = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_cmf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_tf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_af = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_tor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_bm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_sf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tab1_moving_load = new System.Windows.Forms.TabPage();
            this.sc2 = new System.Windows.Forms.SplitContainer();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tv_ml_loads = new System.Windows.Forms.TreeView();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.tv_ml_def = new System.Windows.Forms.TreeView();
            this.tab1_truss_env = new System.Windows.Forms.TabPage();
            this.tc4 = new System.Windows.Forms.TabControl();
            this.tab_org_doc = new System.Windows.Forms.TabPage();
            this.vdScrollableControl1 = new vdScrollableControl.vdScrollableControl();
            this.tab_defl_doc = new System.Windows.Forms.TabPage();
            this.vdScrollableControl2 = new vdScrollableControl.vdScrollableControl();
            this.tab_max_doc = new System.Windows.Forms.TabPage();
            this.vdScrollableControl5 = new vdScrollableControl.vdScrollableControl();
            this.tab_mov_doc = new System.Windows.Forms.TabPage();
            this.vdScrollableControl4 = new vdScrollableControl.vdScrollableControl();
            this.tab_evlp_doc = new System.Windows.Forms.TabPage();
            this.vdScrollableControl3 = new vdScrollableControl.vdScrollableControl();
            this.panel5 = new System.Windows.Forms.Panel();
            this.chk_joints = new System.Windows.Forms.CheckBox();
            this.pnl_progress = new System.Windows.Forms.Panel();
            this.pbLoadDeflection = new System.Windows.Forms.ProgressBar();
            this.lblPleaseWait = new System.Windows.Forms.Label();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsb_3D_rotate = new System.Windows.Forms.ToolStripButton();
            this.tsb_VTop = new System.Windows.Forms.ToolStripButton();
            this.tsb_VBot = new System.Windows.Forms.ToolStripButton();
            this.tsb_VLeft = new System.Windows.Forms.ToolStripButton();
            this.tsb_VRight = new System.Windows.Forms.ToolStripButton();
            this.tsb_VFront = new System.Windows.Forms.ToolStripButton();
            this.tsb_VBack = new System.Windows.Forms.ToolStripButton();
            this.tsb_VNE = new System.Windows.Forms.ToolStripButton();
            this.tsb_VNW = new System.Windows.Forms.ToolStripButton();
            this.tsb_VSE = new System.Windows.Forms.ToolStripButton();
            this.tsb_VSW = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_ShadeOn = new System.Windows.Forms.ToolStripButton();
            this.tsb_Wire = new System.Windows.Forms.ToolStripButton();
            this.tss1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_ZoomA = new System.Windows.Forms.ToolStripButton();
            this.tsb_ZoomE = new System.Windows.Forms.ToolStripButton();
            this.tsb_ZoomP = new System.Windows.Forms.ToolStripButton();
            this.tsb_ZoomW = new System.Windows.Forms.ToolStripButton();
            this.tsb_ZoomIn = new System.Windows.Forms.ToolStripButton();
            this.tsb_ZoomOut = new System.Windows.Forms.ToolStripButton();
            this.tsb_Pan = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tmrLoadDeflection = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tmr_moving_load = new System.Windows.Forms.Timer(this.components);
            this.tc5 = new System.Windows.Forms.TabControl();
            this.tab5_pre_prs = new System.Windows.Forms.TabPage();
            this.tc6 = new System.Windows.Forms.TabControl();
            this.tab_file_open = new System.Windows.Forms.TabPage();
            this.sc4 = new System.Windows.Forms.SplitContainer();
            this.rtb_input_file = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rtb_ll_txt = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ts_shortcut = new System.Windows.Forms.ToolStrip();
            this.tsb_open_data = new System.Windows.Forms.ToolStripButton();
            this.tsb_save_drawing = new System.Windows.Forms.ToolStripButton();
            this.tsb_save_data = new System.Windows.Forms.ToolStripButton();
            this.tsb_print_drawing = new System.Windows.Forms.ToolStripButton();
            this.tsb_close = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmi_file = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_data_open = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_dwg_save = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_file_save = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_file_print = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_close = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_edit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_astra_viewer = new System.Windows.Forms.ToolStripMenuItem();
            this.tab_geometry = new System.Windows.Forms.TabPage();
            this.tab_loading = new System.Windows.Forms.TabPage();
            this.tab5_prs = new System.Windows.Forms.TabPage();
            this.rtb_ana_rep = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbl_process_caption = new System.Windows.Forms.Label();
            this.tab5_post_prs = new System.Windows.Forms.TabPage();
            this.sc3 = new System.Windows.Forms.SplitContainer();
            this.panel1.SuspendLayout();
            this.tc1.SuspendLayout();
            this.tab_load_deflection.SuspendLayout();
            this.grb_Auto.SuspendLayout();
            this.grb_manual.SuspendLayout();
            this.tab_max_force.SuspendLayout();
            this.tab_moving_load.SuspendLayout();
            this.grb_moving_controls.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tab_envelop.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.sc1.Panel1.SuspendLayout();
            this.sc1.Panel2.SuspendLayout();
            this.sc1.SuspendLayout();
            this.tc_pp_panel.SuspendLayout();
            this.tab1_geometry.SuspendLayout();
            this.tc3.SuspendLayout();
            this.tab2_joints.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_joints)).BeginInit();
            this.tab2_members.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMemberGrid)).BeginInit();
            this.tab2_elements.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvElementGrid)).BeginInit();
            this.tab1_loading.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tab1_forces.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tab_3_joint_frc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_joint_frcs)).BeginInit();
            this.tab_3_mem_frc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_beam_frcs)).BeginInit();
            this.tab_3_truss_frc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_truss_frcs)).BeginInit();
            this.tab1_load_deflection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_node_disp)).BeginInit();
            this.panel3.SuspendLayout();
            this.tab1_max_force.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_max_frc)).BeginInit();
            this.tab1_moving_load.SuspendLayout();
            this.sc2.Panel1.SuspendLayout();
            this.sc2.Panel2.SuspendLayout();
            this.sc2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.tc4.SuspendLayout();
            this.tab_org_doc.SuspendLayout();
            this.tab_defl_doc.SuspendLayout();
            this.tab_max_doc.SuspendLayout();
            this.tab_mov_doc.SuspendLayout();
            this.tab_evlp_doc.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pnl_progress.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.tc5.SuspendLayout();
            this.tab5_pre_prs.SuspendLayout();
            this.tc6.SuspendLayout();
            this.tab_file_open.SuspendLayout();
            this.sc4.Panel1.SuspendLayout();
            this.sc4.Panel2.SuspendLayout();
            this.sc4.SuspendLayout();
            this.panel4.SuspendLayout();
            this.ts_shortcut.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tab_loading.SuspendLayout();
            this.tab5_prs.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tab5_post_prs.SuspendLayout();
            this.sc3.Panel1.SuspendLayout();
            this.sc3.Panel2.SuspendLayout();
            this.sc3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_close);
            this.panel1.Controls.Add(this.cmbLengthUnit);
            this.panel1.Controls.Add(this.label77);
            this.panel1.Controls.Add(this.cmb_structure_type);
            this.panel1.Controls.Add(this.label78);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.cmbForceUnit);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.txtUserTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 616);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(906, 44);
            this.panel1.TabIndex = 0;
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(823, 6);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 32);
            this.btn_close.TabIndex = 146;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // cmbLengthUnit
            // 
            this.cmbLengthUnit.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmbLengthUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLengthUnit.FormattingEnabled = true;
            this.cmbLengthUnit.Items.AddRange(new object[] {
            "METRES",
            "CM",
            "MM",
            "YDS",
            "FT",
            "INCH"});
            this.cmbLengthUnit.Location = new System.Drawing.Point(692, 19);
            this.cmbLengthUnit.Name = "cmbLengthUnit";
            this.cmbLengthUnit.Size = new System.Drawing.Size(79, 22);
            this.cmbLengthUnit.TabIndex = 143;
            // 
            // label77
            // 
            this.label77.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label77.AutoSize = true;
            this.label77.Location = new System.Drawing.Point(689, 3);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(80, 14);
            this.label77.TabIndex = 145;
            this.label77.Text = "Length Unit";
            // 
            // cmb_structure_type
            // 
            this.cmb_structure_type.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmb_structure_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_structure_type.FormattingEnabled = true;
            this.cmb_structure_type.Items.AddRange(new object[] {
            "SPACE",
            "FLOOR",
            "PLANE"});
            this.cmb_structure_type.Location = new System.Drawing.Point(13, 19);
            this.cmb_structure_type.Name = "cmb_structure_type";
            this.cmb_structure_type.Size = new System.Drawing.Size(92, 22);
            this.cmb_structure_type.TabIndex = 142;
            // 
            // label78
            // 
            this.label78.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label78.AutoSize = true;
            this.label78.Location = new System.Drawing.Point(603, 3);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(70, 14);
            this.label78.TabIndex = 144;
            this.label78.Text = "Force Unit";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(152, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 14);
            this.label10.TabIndex = 140;
            this.label10.Text = "Project Title";
            // 
            // cmbForceUnit
            // 
            this.cmbForceUnit.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmbForceUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbForceUnit.FormattingEnabled = true;
            this.cmbForceUnit.Items.AddRange(new object[] {
            "KG",
            "KN",
            "MTON",
            "NEW",
            "GMS",
            "LBS",
            "KIP"});
            this.cmbForceUnit.Location = new System.Drawing.Point(606, 19);
            this.cmbForceUnit.Name = "cmbForceUnit";
            this.cmbForceUnit.Size = new System.Drawing.Size(67, 22);
            this.cmbForceUnit.TabIndex = 141;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 14);
            this.label11.TabIndex = 139;
            this.label11.Text = "StructureType";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtUserTitle
            // 
            this.txtUserTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtUserTitle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUserTitle.Location = new System.Drawing.Point(155, 19);
            this.txtUserTitle.Name = "txtUserTitle";
            this.txtUserTitle.Size = new System.Drawing.Size(440, 22);
            this.txtUserTitle.TabIndex = 138;
            // 
            // btn_open_ana_rep
            // 
            this.btn_open_ana_rep.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_open_ana_rep.Location = new System.Drawing.Point(470, 24);
            this.btn_open_ana_rep.Name = "btn_open_ana_rep";
            this.btn_open_ana_rep.Size = new System.Drawing.Size(183, 26);
            this.btn_open_ana_rep.TabIndex = 1;
            this.btn_open_ana_rep.Text = "View Analysis Report";
            this.btn_open_ana_rep.UseVisualStyleBackColor = true;
            this.btn_open_ana_rep.Click += new System.EventHandler(this.btn_open_ana_rep_Click);
            // 
            // btn_process_analysis
            // 
            this.btn_process_analysis.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_process_analysis.Location = new System.Drawing.Point(243, 24);
            this.btn_process_analysis.Name = "btn_process_analysis";
            this.btn_process_analysis.Size = new System.Drawing.Size(183, 26);
            this.btn_process_analysis.TabIndex = 0;
            this.btn_process_analysis.Text = "Process Analysis";
            this.btn_process_analysis.UseVisualStyleBackColor = true;
            this.btn_process_analysis.Click += new System.EventHandler(this.btn_process_analysis_Click);
            // 
            // tc1
            // 
            this.tc1.Controls.Add(this.tab_forces);
            this.tc1.Controls.Add(this.tab_load_deflection);
            this.tc1.Controls.Add(this.tab_max_force);
            this.tc1.Controls.Add(this.tab_moving_load);
            this.tc1.Controls.Add(this.tab_envelop);
            this.tc1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tc1.Location = new System.Drawing.Point(3, 3);
            this.tc1.Name = "tc1";
            this.tc1.SelectedIndex = 0;
            this.tc1.Size = new System.Drawing.Size(890, 64);
            this.tc1.TabIndex = 1;
            this.tc1.SelectedIndexChanged += new System.EventHandler(this.tc1_SelectedIndexChanged);
            // 
            // tab_forces
            // 
            this.tab_forces.Location = new System.Drawing.Point(4, 22);
            this.tab_forces.Name = "tab_forces";
            this.tab_forces.Size = new System.Drawing.Size(882, 38);
            this.tab_forces.TabIndex = 4;
            this.tab_forces.Text = "Forces";
            this.tab_forces.UseVisualStyleBackColor = true;
            // 
            // tab_load_deflection
            // 
            this.tab_load_deflection.Controls.Add(this.lblLoadCase);
            this.tab_load_deflection.Controls.Add(this.grb_Auto);
            this.tab_load_deflection.Controls.Add(this.txtDefFactor);
            this.tab_load_deflection.Controls.Add(this.grb_manual);
            this.tab_load_deflection.Controls.Add(this.lblFactor);
            this.tab_load_deflection.Controls.Add(this.cmbLoadCase);
            this.tab_load_deflection.Location = new System.Drawing.Point(4, 22);
            this.tab_load_deflection.Name = "tab_load_deflection";
            this.tab_load_deflection.Size = new System.Drawing.Size(882, 38);
            this.tab_load_deflection.TabIndex = 5;
            this.tab_load_deflection.Text = "Load Deflection";
            this.tab_load_deflection.UseVisualStyleBackColor = true;
            // 
            // lblLoadCase
            // 
            this.lblLoadCase.AutoSize = true;
            this.lblLoadCase.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoadCase.Location = new System.Drawing.Point(738, 10);
            this.lblLoadCase.Name = "lblLoadCase";
            this.lblLoadCase.Size = new System.Drawing.Size(73, 13);
            this.lblLoadCase.TabIndex = 23;
            this.lblLoadCase.Text = "Load Case";
            // 
            // grb_Auto
            // 
            this.grb_Auto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grb_Auto.Controls.Add(this.label2);
            this.grb_Auto.Controls.Add(this.cmbInterval);
            this.grb_Auto.Controls.Add(this.btnStop);
            this.grb_Auto.Controls.Add(this.btnPause);
            this.grb_Auto.Controls.Add(this.btnAutoNext);
            this.grb_Auto.Controls.Add(this.btnAutoPrev);
            this.grb_Auto.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_Auto.Location = new System.Drawing.Point(179, 3);
            this.grb_Auto.Name = "grb_Auto";
            this.grb_Auto.Size = new System.Drawing.Size(396, 49);
            this.grb_Auto.TabIndex = 23;
            this.grb_Auto.TabStop = false;
            this.grb_Auto.Text = "Auto";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(164, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 14);
            this.label2.TabIndex = 22;
            this.label2.Text = "Interval";
            // 
            // cmbInterval
            // 
            this.cmbInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInterval.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbInterval.FormattingEnabled = true;
            this.cmbInterval.Items.AddRange(new object[] {
            "0.1 Sec",
            "0.2 Sec",
            "0.3 Sec",
            "0.4 Sec",
            "0.5 Sec",
            "0.6 Sec",
            "0.7 Sec",
            "0.8 Sec",
            "0.9 Sec",
            "1 Sec",
            "2 Sec",
            "3 Sec",
            "4 Sec",
            "5 Sec",
            "6 Sec",
            "7 Sec",
            "8 Sec",
            "9 Sec",
            "10 Sec",
            "15 Sec",
            "20 Sec",
            "25 Sec",
            "30 Sec"});
            this.cmbInterval.Location = new System.Drawing.Point(152, 23);
            this.cmbInterval.Name = "cmbInterval";
            this.cmbInterval.Size = new System.Drawing.Size(73, 21);
            this.cmbInterval.TabIndex = 24;
            // 
            // btnStop
            // 
            this.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStop.Location = new System.Drawing.Point(344, 23);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(45, 22);
            this.btnStop.TabIndex = 20;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPause
            // 
            this.btnPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPause.Location = new System.Drawing.Point(6, 24);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(45, 22);
            this.btnPause.TabIndex = 15;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnAutoNext
            // 
            this.btnAutoNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAutoNext.Location = new System.Drawing.Point(261, 23);
            this.btnAutoNext.Name = "btnAutoNext";
            this.btnAutoNext.Size = new System.Drawing.Size(77, 22);
            this.btnAutoNext.TabIndex = 22;
            this.btnAutoNext.Text = "Run Forward";
            this.btnAutoNext.UseVisualStyleBackColor = true;
            this.btnAutoNext.Click += new System.EventHandler(this.btnNextAuto_Click);
            // 
            // btnAutoPrev
            // 
            this.btnAutoPrev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAutoPrev.Location = new System.Drawing.Point(57, 24);
            this.btnAutoPrev.Name = "btnAutoPrev";
            this.btnAutoPrev.Size = new System.Drawing.Size(89, 22);
            this.btnAutoPrev.TabIndex = 21;
            this.btnAutoPrev.Text = "Run Backward";
            this.btnAutoPrev.UseVisualStyleBackColor = true;
            this.btnAutoPrev.Click += new System.EventHandler(this.btnAutoPrev_Click);
            // 
            // txtDefFactor
            // 
            this.txtDefFactor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDefFactor.Location = new System.Drawing.Point(638, 25);
            this.txtDefFactor.Name = "txtDefFactor";
            this.txtDefFactor.Size = new System.Drawing.Size(76, 21);
            this.txtDefFactor.TabIndex = 32;
            this.txtDefFactor.Text = "1";
            this.txtDefFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grb_manual
            // 
            this.grb_manual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grb_manual.Controls.Add(this.btnFirst);
            this.grb_manual.Controls.Add(this.btnPrev);
            this.grb_manual.Controls.Add(this.btnNext);
            this.grb_manual.Controls.Add(this.btnLast);
            this.grb_manual.Location = new System.Drawing.Point(14, 3);
            this.grb_manual.Name = "grb_manual";
            this.grb_manual.Size = new System.Drawing.Size(159, 49);
            this.grb_manual.TabIndex = 22;
            this.grb_manual.TabStop = false;
            this.grb_manual.Text = "Manual";
            // 
            // btnFirst
            // 
            this.btnFirst.BackgroundImage = global::HEADSNeed.Properties.Resources.first;
            this.btnFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFirst.Location = new System.Drawing.Point(6, 15);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(30, 28);
            this.btnFirst.TabIndex = 27;
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.BackgroundImage = global::HEADSNeed.Properties.Resources.prev;
            this.btnPrev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrev.Location = new System.Drawing.Point(42, 15);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(30, 28);
            this.btnPrev.TabIndex = 30;
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackgroundImage = global::HEADSNeed.Properties.Resources.next;
            this.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNext.Location = new System.Drawing.Point(78, 15);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(30, 28);
            this.btnNext.TabIndex = 29;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.BackgroundImage = global::HEADSNeed.Properties.Resources.last;
            this.btnLast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLast.Location = new System.Drawing.Point(118, 15);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(30, 28);
            this.btnLast.TabIndex = 28;
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // lblFactor
            // 
            this.lblFactor.AutoSize = true;
            this.lblFactor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFactor.Location = new System.Drawing.Point(644, 11);
            this.lblFactor.Name = "lblFactor";
            this.lblFactor.Size = new System.Drawing.Size(43, 13);
            this.lblFactor.TabIndex = 31;
            this.lblFactor.Text = "Factor";
            // 
            // cmbLoadCase
            // 
            this.cmbLoadCase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLoadCase.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLoadCase.FormattingEnabled = true;
            this.cmbLoadCase.Location = new System.Drawing.Point(738, 25);
            this.cmbLoadCase.Name = "cmbLoadCase";
            this.cmbLoadCase.Size = new System.Drawing.Size(69, 22);
            this.cmbLoadCase.TabIndex = 8;
            this.cmbLoadCase.SelectedIndexChanged += new System.EventHandler(this.cmbLoadCase_SelectedIndexChanged);
            // 
            // tab_max_force
            // 
            this.tab_max_force.Controls.Add(this.label6);
            this.tab_max_force.Location = new System.Drawing.Point(4, 22);
            this.tab_max_force.Name = "tab_max_force";
            this.tab_max_force.Size = new System.Drawing.Size(882, 38);
            this.tab_max_force.TabIndex = 6;
            this.tab_max_force.Text = "Max Force";
            this.tab_max_force.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(271, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(293, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Click on the Member to get forces in the List.";
            // 
            // tab_moving_load
            // 
            this.tab_moving_load.Controls.Add(this.grb_moving_controls);
            this.tab_moving_load.Location = new System.Drawing.Point(4, 22);
            this.tab_moving_load.Name = "tab_moving_load";
            this.tab_moving_load.Size = new System.Drawing.Size(882, 38);
            this.tab_moving_load.TabIndex = 7;
            this.tab_moving_load.Text = "Moving Load";
            this.tab_moving_load.UseVisualStyleBackColor = true;
            // 
            // grb_moving_controls
            // 
            this.grb_moving_controls.Controls.Add(this.groupBox5);
            this.grb_moving_controls.Controls.Add(this.groupBox6);
            this.grb_moving_controls.Dock = System.Windows.Forms.DockStyle.Top;
            this.grb_moving_controls.Location = new System.Drawing.Point(0, 0);
            this.grb_moving_controls.Name = "grb_moving_controls";
            this.grb_moving_controls.Size = new System.Drawing.Size(882, 59);
            this.grb_moving_controls.TabIndex = 31;
            this.grb_moving_controls.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.lbl_distance);
            this.groupBox5.Controls.Add(this.txt_dist);
            this.groupBox5.Controls.Add(this.cmb_mov_loadcase);
            this.groupBox5.Location = new System.Drawing.Point(346, 4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(170, 44);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Tag = "";
            this.groupBox5.Text = "Load Case";
            // 
            // lbl_distance
            // 
            this.lbl_distance.AutoSize = true;
            this.lbl_distance.Location = new System.Drawing.Point(149, 20);
            this.lbl_distance.Name = "lbl_distance";
            this.lbl_distance.Size = new System.Drawing.Size(18, 13);
            this.lbl_distance.TabIndex = 9;
            this.lbl_distance.Text = "m";
            // 
            // txt_dist
            // 
            this.txt_dist.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_dist.Location = new System.Drawing.Point(87, 17);
            this.txt_dist.Name = "txt_dist";
            this.txt_dist.Size = new System.Drawing.Size(56, 21);
            this.txt_dist.TabIndex = 27;
            this.txt_dist.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmb_mov_loadcase
            // 
            this.cmb_mov_loadcase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cmb_mov_loadcase.Enabled = false;
            this.cmb_mov_loadcase.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_mov_loadcase.FormattingEnabled = true;
            this.cmb_mov_loadcase.Location = new System.Drawing.Point(3, 16);
            this.cmb_mov_loadcase.Name = "cmb_mov_loadcase";
            this.cmb_mov_loadcase.Size = new System.Drawing.Size(78, 22);
            this.cmb_mov_loadcase.TabIndex = 8;
            this.cmb_mov_loadcase.SelectedIndexChanged += new System.EventHandler(this.cmb_mov_loadcase_SelectedIndexChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.cmb_mov_time);
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Controls.Add(this.btn_mov_stop);
            this.groupBox6.Controls.Add(this.btn_mov_run);
            this.groupBox6.Controls.Add(this.btn_mov_pause);
            this.groupBox6.Location = new System.Drawing.Point(521, 7);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(355, 41);
            this.groupBox6.TabIndex = 21;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Run Moving Load";
            // 
            // cmb_mov_time
            // 
            this.cmb_mov_time.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_mov_time.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_mov_time.FormattingEnabled = true;
            this.cmb_mov_time.Items.AddRange(new object[] {
            "0.1 s",
            "0.2 s",
            "0.3 s",
            "0.4 s",
            "0.5 s",
            "0.6 s",
            "0.7 s",
            "0.8 s",
            "0.9 s",
            "1 s",
            "2 s",
            "3 s",
            "4 s",
            "5 s",
            "6 Sec",
            "7 Sec",
            "8 Sec",
            "9 Sec",
            "10 Sec",
            "15 Sec",
            "20 Sec",
            "25 Sec",
            "30 Sec"});
            this.cmb_mov_time.Location = new System.Drawing.Point(109, 13);
            this.cmb_mov_time.Name = "cmb_mov_time";
            this.cmb_mov_time.Size = new System.Drawing.Size(65, 21);
            this.cmb_mov_time.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Time Interval";
            // 
            // btn_mov_stop
            // 
            this.btn_mov_stop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_mov_stop.Location = new System.Drawing.Point(292, 11);
            this.btn_mov_stop.Name = "btn_mov_stop";
            this.btn_mov_stop.Size = new System.Drawing.Size(50, 23);
            this.btn_mov_stop.TabIndex = 20;
            this.btn_mov_stop.Text = "Stop";
            this.btn_mov_stop.UseVisualStyleBackColor = true;
            this.btn_mov_stop.Click += new System.EventHandler(this.btn_mov_stop_Click);
            // 
            // btn_mov_run
            // 
            this.btn_mov_run.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_mov_run.Location = new System.Drawing.Point(180, 11);
            this.btn_mov_run.Name = "btn_mov_run";
            this.btn_mov_run.Size = new System.Drawing.Size(50, 23);
            this.btn_mov_run.TabIndex = 22;
            this.btn_mov_run.Text = "Run";
            this.btn_mov_run.UseVisualStyleBackColor = true;
            this.btn_mov_run.Click += new System.EventHandler(this.btn_mov_run_Click);
            // 
            // btn_mov_pause
            // 
            this.btn_mov_pause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_mov_pause.Location = new System.Drawing.Point(236, 11);
            this.btn_mov_pause.Name = "btn_mov_pause";
            this.btn_mov_pause.Size = new System.Drawing.Size(50, 23);
            this.btn_mov_pause.TabIndex = 15;
            this.btn_mov_pause.Text = "Pause";
            this.btn_mov_pause.UseVisualStyleBackColor = true;
            this.btn_mov_pause.Click += new System.EventHandler(this.btn_mov_pause_Click);
            // 
            // tab_envelop
            // 
            this.tab_envelop.Controls.Add(this.groupBox3);
            this.tab_envelop.Controls.Add(this.txt_env_mnos);
            this.tab_envelop.Controls.Add(this.btn_env_show);
            this.tab_envelop.Location = new System.Drawing.Point(4, 22);
            this.tab_envelop.Name = "tab_envelop";
            this.tab_envelop.Padding = new System.Windows.Forms.Padding(3);
            this.tab_envelop.Size = new System.Drawing.Size(882, 38);
            this.tab_envelop.TabIndex = 8;
            this.tab_envelop.Text = "Truss Envelope";
            this.tab_envelop.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbtn_env_bottom);
            this.groupBox3.Controls.Add(this.rbtn_env_top);
            this.groupBox3.Location = new System.Drawing.Point(481, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(233, 38);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Select Member";
            // 
            // rbtn_env_bottom
            // 
            this.rbtn_env_bottom.AutoSize = true;
            this.rbtn_env_bottom.Location = new System.Drawing.Point(107, 15);
            this.rbtn_env_bottom.Name = "rbtn_env_bottom";
            this.rbtn_env_bottom.Size = new System.Drawing.Size(105, 17);
            this.rbtn_env_bottom.TabIndex = 4;
            this.rbtn_env_bottom.TabStop = true;
            this.rbtn_env_bottom.Text = "Bottom Chord";
            this.rbtn_env_bottom.UseVisualStyleBackColor = true;
            // 
            // rbtn_env_top
            // 
            this.rbtn_env_top.AutoSize = true;
            this.rbtn_env_top.Checked = true;
            this.rbtn_env_top.Location = new System.Drawing.Point(17, 15);
            this.rbtn_env_top.Name = "rbtn_env_top";
            this.rbtn_env_top.Size = new System.Drawing.Size(84, 17);
            this.rbtn_env_top.TabIndex = 4;
            this.rbtn_env_top.TabStop = true;
            this.rbtn_env_top.Text = "Top Chord";
            this.rbtn_env_top.UseVisualStyleBackColor = true;
            // 
            // txt_env_mnos
            // 
            this.txt_env_mnos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_env_mnos.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_env_mnos.Location = new System.Drawing.Point(243, 17);
            this.txt_env_mnos.Name = "txt_env_mnos";
            this.txt_env_mnos.Size = new System.Drawing.Size(211, 21);
            this.txt_env_mnos.TabIndex = 2;
            this.txt_env_mnos.Visible = false;
            // 
            // btn_env_show
            // 
            this.btn_env_show.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_env_show.Location = new System.Drawing.Point(733, 15);
            this.btn_env_show.Name = "btn_env_show";
            this.btn_env_show.Size = new System.Drawing.Size(121, 23);
            this.btn_env_show.TabIndex = 0;
            this.btn_env_show.Text = "Show Envelope";
            this.btn_env_show.UseVisualStyleBackColor = true;
            this.btn_env_show.Click += new System.EventHandler(this.btn_env_show_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(435, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Text Size";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // cmb_text_size
            // 
            this.cmb_text_size.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb_text_size.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_text_size.FormattingEnabled = true;
            this.cmb_text_size.Location = new System.Drawing.Point(504, 3);
            this.cmb_text_size.Name = "cmb_text_size";
            this.cmb_text_size.Size = new System.Drawing.Size(52, 21);
            this.cmb_text_size.TabIndex = 3;
            this.cmb_text_size.SelectedIndexChanged += new System.EventHandler(this.cmb_text_size_SelectedIndexChanged);
            this.cmb_text_size.Click += new System.EventHandler(this.cmb_text_size_SelectedIndexChanged);
            // 
            // chk_suprts
            // 
            this.chk_suprts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chk_suprts.AutoSize = true;
            this.chk_suprts.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_suprts.Location = new System.Drawing.Point(317, 5);
            this.chk_suprts.Name = "chk_suprts";
            this.chk_suprts.Size = new System.Drawing.Size(77, 17);
            this.chk_suprts.TabIndex = 2;
            this.chk_suprts.Text = "Supports";
            this.chk_suprts.UseVisualStyleBackColor = true;
            this.chk_suprts.CheckedChanged += new System.EventHandler(this.chk_joints_CheckedChanged);
            // 
            // chk_elems
            // 
            this.chk_elems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chk_elems.AutoSize = true;
            this.chk_elems.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_elems.Location = new System.Drawing.Point(216, 5);
            this.chk_elems.Name = "chk_elems";
            this.chk_elems.Size = new System.Drawing.Size(78, 17);
            this.chk_elems.TabIndex = 2;
            this.chk_elems.Text = "Elements";
            this.chk_elems.UseVisualStyleBackColor = true;
            this.chk_elems.CheckedChanged += new System.EventHandler(this.chk_joints_CheckedChanged);
            // 
            // chk_mems
            // 
            this.chk_mems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chk_mems.AutoSize = true;
            this.chk_mems.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_mems.Location = new System.Drawing.Point(115, 5);
            this.chk_mems.Name = "chk_mems";
            this.chk_mems.Size = new System.Drawing.Size(78, 17);
            this.chk_mems.TabIndex = 2;
            this.chk_mems.Text = "Members";
            this.chk_mems.UseVisualStyleBackColor = true;
            this.chk_mems.CheckedChanged += new System.EventHandler(this.chk_joints_CheckedChanged);
            // 
            // chkJoint
            // 
            this.chkJoint.AutoSize = true;
            this.chkJoint.Checked = true;
            this.chkJoint.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkJoint.Location = new System.Drawing.Point(784, 12);
            this.chkJoint.Name = "chkJoint";
            this.chkJoint.Size = new System.Drawing.Size(83, 17);
            this.chkJoint.TabIndex = 0;
            this.chkJoint.Text = "Joint Load";
            this.chkJoint.UseVisualStyleBackColor = true;
            this.chkJoint.CheckedChanged += new System.EventHandler(this.chkGlobal_CheckedChanged);
            // 
            // chkLocal
            // 
            this.chkLocal.AutoSize = true;
            this.chkLocal.Checked = true;
            this.chkLocal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLocal.Location = new System.Drawing.Point(624, 12);
            this.chkLocal.Name = "chkLocal";
            this.chkLocal.Size = new System.Drawing.Size(136, 17);
            this.chkLocal.TabIndex = 1;
            this.chkLocal.Text = "Local Member Load";
            this.chkLocal.UseVisualStyleBackColor = true;
            this.chkLocal.CheckedChanged += new System.EventHandler(this.chkGlobal_CheckedChanged);
            // 
            // chkGlobal
            // 
            this.chkGlobal.AutoSize = true;
            this.chkGlobal.Checked = true;
            this.chkGlobal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGlobal.Location = new System.Drawing.Point(475, 12);
            this.chkGlobal.Name = "chkGlobal";
            this.chkGlobal.Size = new System.Drawing.Size(143, 17);
            this.chkGlobal.TabIndex = 0;
            this.chkGlobal.Text = "Global Member Load";
            this.chkGlobal.UseVisualStyleBackColor = true;
            this.chkGlobal.CheckedChanged += new System.EventHandler(this.chkGlobal_CheckedChanged);
            // 
            // sc1
            // 
            this.sc1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sc1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc1.Location = new System.Drawing.Point(0, 0);
            this.sc1.Name = "sc1";
            // 
            // sc1.Panel1
            // 
            this.sc1.Panel1.Controls.Add(this.tc_pp_panel);
            this.sc1.Panel1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // sc1.Panel2
            // 
            this.sc1.Panel2.Controls.Add(this.tc4);
            this.sc1.Panel2.Controls.Add(this.panel5);
            this.sc1.Panel2.Controls.Add(this.pnl_progress);
            this.sc1.Panel2.Controls.Add(this.toolStrip2);
            this.sc1.Size = new System.Drawing.Size(906, 514);
            this.sc1.SplitterDistance = 337;
            this.sc1.TabIndex = 2;
            // 
            // tc_pp_panel
            // 
            this.tc_pp_panel.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tc_pp_panel.Controls.Add(this.tab1_geometry);
            this.tc_pp_panel.Controls.Add(this.tab1_loading);
            this.tc_pp_panel.Controls.Add(this.tab1_forces);
            this.tc_pp_panel.Controls.Add(this.tab1_load_deflection);
            this.tc_pp_panel.Controls.Add(this.tab1_max_force);
            this.tc_pp_panel.Controls.Add(this.tab1_moving_load);
            this.tc_pp_panel.Controls.Add(this.tab1_truss_env);
            this.tc_pp_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_pp_panel.Location = new System.Drawing.Point(0, 0);
            this.tc_pp_panel.Multiline = true;
            this.tc_pp_panel.Name = "tc_pp_panel";
            this.tc_pp_panel.SelectedIndex = 0;
            this.tc_pp_panel.Size = new System.Drawing.Size(335, 512);
            this.tc_pp_panel.TabIndex = 0;
            this.tc_pp_panel.Visible = false;
            this.tc_pp_panel.SelectedIndexChanged += new System.EventHandler(this.tc2_SelectedIndexChanged);
            // 
            // tab1_geometry
            // 
            this.tab1_geometry.Controls.Add(this.tc3);
            this.tab1_geometry.Location = new System.Drawing.Point(24, 4);
            this.tab1_geometry.Name = "tab1_geometry";
            this.tab1_geometry.Padding = new System.Windows.Forms.Padding(3);
            this.tab1_geometry.Size = new System.Drawing.Size(307, 504);
            this.tab1_geometry.TabIndex = 0;
            this.tab1_geometry.Text = "Geometry";
            this.tab1_geometry.UseVisualStyleBackColor = true;
            // 
            // tc3
            // 
            this.tc3.Controls.Add(this.tab2_joints);
            this.tc3.Controls.Add(this.tab2_members);
            this.tc3.Controls.Add(this.tab2_elements);
            this.tc3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc3.Location = new System.Drawing.Point(3, 3);
            this.tc3.Name = "tc3";
            this.tc3.SelectedIndex = 0;
            this.tc3.Size = new System.Drawing.Size(301, 498);
            this.tc3.TabIndex = 0;
            this.tc3.SelectedIndexChanged += new System.EventHandler(this.tc2_SelectedIndexChanged);
            // 
            // tab2_joints
            // 
            this.tab2_joints.Controls.Add(this.dgv_joints);
            this.tab2_joints.Location = new System.Drawing.Point(4, 22);
            this.tab2_joints.Name = "tab2_joints";
            this.tab2_joints.Padding = new System.Windows.Forms.Padding(3);
            this.tab2_joints.Size = new System.Drawing.Size(293, 472);
            this.tab2_joints.TabIndex = 0;
            this.tab2_joints.Text = "Joints";
            this.tab2_joints.UseVisualStyleBackColor = true;
            // 
            // dgv_joints
            // 
            this.dgv_joints.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_joints.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_joints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_joints.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_jno,
            this.col_x,
            this.col_y,
            this.col_z,
            this.col_support});
            this.dgv_joints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_joints.Location = new System.Drawing.Point(3, 3);
            this.dgv_joints.Name = "dgv_joints";
            this.dgv_joints.RowHeadersWidth = 21;
            this.dgv_joints.RowTemplate.Height = 15;
            this.dgv_joints.RowTemplate.ReadOnly = true;
            this.dgv_joints.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_joints.Size = new System.Drawing.Size(287, 466);
            this.dgv_joints.TabIndex = 0;
            this.dgv_joints.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_joints_CellEnter);
            // 
            // col_jno
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.col_jno.DefaultCellStyle = dataGridViewCellStyle2;
            this.col_jno.Frozen = true;
            this.col_jno.HeaderText = "Joint#";
            this.col_jno.Name = "col_jno";
            this.col_jno.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.col_jno.Width = 40;
            // 
            // col_x
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_x.DefaultCellStyle = dataGridViewCellStyle3;
            this.col_x.HeaderText = "X";
            this.col_x.Name = "col_x";
            this.col_x.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.col_x.Width = 57;
            // 
            // col_y
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_y.DefaultCellStyle = dataGridViewCellStyle4;
            this.col_y.HeaderText = "Y";
            this.col_y.Name = "col_y";
            this.col_y.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.col_y.Width = 57;
            // 
            // col_z
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.col_z.DefaultCellStyle = dataGridViewCellStyle5;
            this.col_z.HeaderText = "Z";
            this.col_z.Name = "col_z";
            this.col_z.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.col_z.Width = 57;
            // 
            // col_support
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.col_support.DefaultCellStyle = dataGridViewCellStyle6;
            this.col_support.HeaderText = "Support";
            this.col_support.Name = "col_support";
            this.col_support.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.col_support.Width = 70;
            // 
            // tab2_members
            // 
            this.tab2_members.Controls.Add(this.dgvMemberGrid);
            this.tab2_members.Location = new System.Drawing.Point(4, 22);
            this.tab2_members.Name = "tab2_members";
            this.tab2_members.Padding = new System.Windows.Forms.Padding(3);
            this.tab2_members.Size = new System.Drawing.Size(293, 472);
            this.tab2_members.TabIndex = 1;
            this.tab2_members.Text = "Members";
            this.tab2_members.UseVisualStyleBackColor = true;
            // 
            // dgvMemberGrid
            // 
            this.dgvMemberGrid.AllowUserToAddRows = false;
            this.dgvMemberGrid.AllowUserToDeleteRows = false;
            this.dgvMemberGrid.ColumnHeadersHeight = 20;
            this.dgvMemberGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgTxtMemberNo,
            this.dgTxtMember,
            this.dgTxtNode1,
            this.dgTxtNode2,
            this.dgTxtYD,
            this.dgTxtZD,
            this.dgTxtArea,
            this.dgTxtIX,
            this.dgTxtIY,
            this.dgTxtIZ,
            this.dgTxtE,
            this.dgTxtDEN,
            this.dgTxtPR,
            this.dgTxtMAT});
            this.dgvMemberGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMemberGrid.Location = new System.Drawing.Point(3, 3);
            this.dgvMemberGrid.Name = "dgvMemberGrid";
            this.dgvMemberGrid.RowHeadersWidth = 13;
            this.dgvMemberGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvMemberGrid.RowsDefaultCellStyle = dataGridViewCellStyle20;
            this.dgvMemberGrid.RowTemplate.Height = 14;
            this.dgvMemberGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMemberGrid.Size = new System.Drawing.Size(287, 466);
            this.dgvMemberGrid.TabIndex = 1;
            this.dgvMemberGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMemberGrid_CellEnter);
            // 
            // dgTxtMemberNo
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgTxtMemberNo.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgTxtMemberNo.Frozen = true;
            this.dgTxtMemberNo.HeaderText = "Member#";
            this.dgTxtMemberNo.Name = "dgTxtMemberNo";
            this.dgTxtMemberNo.ReadOnly = true;
            this.dgTxtMemberNo.Width = 63;
            // 
            // dgTxtMember
            // 
            this.dgTxtMember.HeaderText = "Type";
            this.dgTxtMember.Name = "dgTxtMember";
            this.dgTxtMember.ReadOnly = true;
            this.dgTxtMember.Width = 60;
            // 
            // dgTxtNode1
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgTxtNode1.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgTxtNode1.HeaderText = "Node 1";
            this.dgTxtNode1.Name = "dgTxtNode1";
            this.dgTxtNode1.ReadOnly = true;
            this.dgTxtNode1.Width = 50;
            // 
            // dgTxtNode2
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgTxtNode2.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgTxtNode2.HeaderText = "Node 2";
            this.dgTxtNode2.Name = "dgTxtNode2";
            this.dgTxtNode2.ReadOnly = true;
            this.dgTxtNode2.Width = 50;
            // 
            // dgTxtYD
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgTxtYD.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgTxtYD.HeaderText = "YD";
            this.dgTxtYD.Name = "dgTxtYD";
            this.dgTxtYD.ReadOnly = true;
            this.dgTxtYD.Width = 50;
            // 
            // dgTxtZD
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgTxtZD.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgTxtZD.HeaderText = "ZD";
            this.dgTxtZD.Name = "dgTxtZD";
            this.dgTxtZD.ReadOnly = true;
            this.dgTxtZD.Width = 50;
            // 
            // dgTxtArea
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgTxtArea.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgTxtArea.HeaderText = "AREA";
            this.dgTxtArea.Name = "dgTxtArea";
            this.dgTxtArea.ReadOnly = true;
            this.dgTxtArea.Width = 90;
            // 
            // dgTxtIX
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgTxtIX.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgTxtIX.HeaderText = "IX";
            this.dgTxtIX.Name = "dgTxtIX";
            this.dgTxtIX.ReadOnly = true;
            this.dgTxtIX.Width = 90;
            // 
            // dgTxtIY
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgTxtIY.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgTxtIY.HeaderText = "IY";
            this.dgTxtIY.Name = "dgTxtIY";
            this.dgTxtIY.ReadOnly = true;
            this.dgTxtIY.Width = 90;
            // 
            // dgTxtIZ
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgTxtIZ.DefaultCellStyle = dataGridViewCellStyle15;
            this.dgTxtIZ.HeaderText = "IZ";
            this.dgTxtIZ.Name = "dgTxtIZ";
            this.dgTxtIZ.ReadOnly = true;
            this.dgTxtIZ.Width = 90;
            // 
            // dgTxtE
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgTxtE.DefaultCellStyle = dataGridViewCellStyle16;
            this.dgTxtE.HeaderText = "E";
            this.dgTxtE.Name = "dgTxtE";
            this.dgTxtE.ReadOnly = true;
            this.dgTxtE.Width = 90;
            // 
            // dgTxtDEN
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgTxtDEN.DefaultCellStyle = dataGridViewCellStyle17;
            this.dgTxtDEN.HeaderText = "DEN";
            this.dgTxtDEN.Name = "dgTxtDEN";
            this.dgTxtDEN.ReadOnly = true;
            this.dgTxtDEN.Width = 90;
            // 
            // dgTxtPR
            // 
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgTxtPR.DefaultCellStyle = dataGridViewCellStyle18;
            this.dgTxtPR.HeaderText = "PR";
            this.dgTxtPR.Name = "dgTxtPR";
            this.dgTxtPR.ReadOnly = true;
            this.dgTxtPR.Width = 90;
            // 
            // dgTxtMAT
            // 
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgTxtMAT.DefaultCellStyle = dataGridViewCellStyle19;
            this.dgTxtMAT.HeaderText = "Length";
            this.dgTxtMAT.Name = "dgTxtMAT";
            this.dgTxtMAT.ReadOnly = true;
            this.dgTxtMAT.Width = 65;
            // 
            // tab2_elements
            // 
            this.tab2_elements.Controls.Add(this.dgvElementGrid);
            this.tab2_elements.Location = new System.Drawing.Point(4, 22);
            this.tab2_elements.Name = "tab2_elements";
            this.tab2_elements.Size = new System.Drawing.Size(293, 472);
            this.tab2_elements.TabIndex = 2;
            this.tab2_elements.Text = "Elements";
            this.tab2_elements.UseVisualStyleBackColor = true;
            // 
            // dgvElementGrid
            // 
            this.dgvElementGrid.AllowUserToAddRows = false;
            this.dgvElementGrid.AllowUserToDeleteRows = false;
            this.dgvElementGrid.ColumnHeadersHeight = 20;
            this.dgvElementGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14});
            this.dgvElementGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvElementGrid.Location = new System.Drawing.Point(0, 0);
            this.dgvElementGrid.Name = "dgvElementGrid";
            this.dgvElementGrid.RowHeadersWidth = 13;
            this.dgvElementGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle27.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvElementGrid.RowsDefaultCellStyle = dataGridViewCellStyle27;
            this.dgvElementGrid.RowTemplate.Height = 14;
            this.dgvElementGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvElementGrid.Size = new System.Drawing.Size(293, 472);
            this.dgvElementGrid.TabIndex = 2;
            this.dgvElementGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvElementGrid_CellEnter);
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle21;
            this.dataGridViewTextBoxColumn1.Frozen = true;
            this.dataGridViewTextBoxColumn1.HeaderText = "Element#";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 63;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Type";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 60;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle22;
            this.dataGridViewTextBoxColumn3.HeaderText = "Node 1";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 50;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle23;
            this.dataGridViewTextBoxColumn4.HeaderText = "Node 2";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 50;
            // 
            // dataGridViewTextBoxColumn12
            // 
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn12.DefaultCellStyle = dataGridViewCellStyle24;
            this.dataGridViewTextBoxColumn12.HeaderText = "Node 3";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.Width = 50;
            // 
            // dataGridViewTextBoxColumn13
            // 
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn13.DefaultCellStyle = dataGridViewCellStyle25;
            this.dataGridViewTextBoxColumn13.HeaderText = "Node 4";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            this.dataGridViewTextBoxColumn13.Width = 50;
            // 
            // dataGridViewTextBoxColumn14
            // 
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn14.DefaultCellStyle = dataGridViewCellStyle26;
            this.dataGridViewTextBoxColumn14.HeaderText = "Thickness";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            this.dataGridViewTextBoxColumn14.Width = 70;
            // 
            // tab1_loading
            // 
            this.tab1_loading.Controls.Add(this.groupBox2);
            this.tab1_loading.Location = new System.Drawing.Point(24, 4);
            this.tab1_loading.Name = "tab1_loading";
            this.tab1_loading.Padding = new System.Windows.Forms.Padding(3);
            this.tab1_loading.Size = new System.Drawing.Size(307, 504);
            this.tab1_loading.TabIndex = 1;
            this.tab1_loading.Text = "Loading";
            this.tab1_loading.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tv_loads);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(301, 498);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "LOAD CASES DETAILS";
            // 
            // tv_loads
            // 
            this.tv_loads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv_loads.Location = new System.Drawing.Point(3, 17);
            this.tv_loads.Name = "tv_loads";
            this.tv_loads.Size = new System.Drawing.Size(295, 478);
            this.tv_loads.TabIndex = 0;
            this.tv_loads.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // tab1_forces
            // 
            this.tab1_forces.Controls.Add(this.tabControl1);
            this.tab1_forces.Location = new System.Drawing.Point(24, 4);
            this.tab1_forces.Name = "tab1_forces";
            this.tab1_forces.Size = new System.Drawing.Size(307, 504);
            this.tab1_forces.TabIndex = 2;
            this.tab1_forces.Text = "Analysis Forces";
            this.tab1_forces.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab_3_joint_frc);
            this.tabControl1.Controls.Add(this.tab_3_mem_frc);
            this.tabControl1.Controls.Add(this.tab_3_truss_frc);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(307, 504);
            this.tabControl1.TabIndex = 0;
            // 
            // tab_3_joint_frc
            // 
            this.tab_3_joint_frc.Controls.Add(this.dgv_joint_frcs);
            this.tab_3_joint_frc.Location = new System.Drawing.Point(4, 22);
            this.tab_3_joint_frc.Name = "tab_3_joint_frc";
            this.tab_3_joint_frc.Padding = new System.Windows.Forms.Padding(3);
            this.tab_3_joint_frc.Size = new System.Drawing.Size(299, 478);
            this.tab_3_joint_frc.TabIndex = 0;
            this.tab_3_joint_frc.Text = "Joint Forces";
            this.tab_3_joint_frc.UseVisualStyleBackColor = true;
            // 
            // dgv_joint_frcs
            // 
            this.dgv_joint_frcs.AllowUserToAddRows = false;
            this.dgv_joint_frcs.AllowUserToDeleteRows = false;
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle28.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle28.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle28.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle28.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle28.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle28.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_joint_frcs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle28;
            this.dgv_joint_frcs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_joint_frcs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn23,
            this.dataGridViewTextBoxColumn24,
            this.dataGridViewTextBoxColumn26,
            this.dataGridViewTextBoxColumn27,
            this.dataGridViewTextBoxColumn28,
            this.dataGridViewTextBoxColumn29,
            this.dataGridViewTextBoxColumn30,
            this.dataGridViewTextBoxColumn31});
            this.dgv_joint_frcs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_joint_frcs.Location = new System.Drawing.Point(3, 3);
            this.dgv_joint_frcs.Name = "dgv_joint_frcs";
            this.dgv_joint_frcs.RowHeadersWidth = 21;
            this.dgv_joint_frcs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_joint_frcs.Size = new System.Drawing.Size(293, 472);
            this.dgv_joint_frcs.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn23
            // 
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridViewTextBoxColumn23.DefaultCellStyle = dataGridViewCellStyle29;
            this.dataGridViewTextBoxColumn23.Frozen = true;
            this.dataGridViewTextBoxColumn23.HeaderText = "Joint#";
            this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
            this.dataGridViewTextBoxColumn23.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn23.Width = 50;
            // 
            // dataGridViewTextBoxColumn24
            // 
            dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGridViewTextBoxColumn24.DefaultCellStyle = dataGridViewCellStyle30;
            this.dataGridViewTextBoxColumn24.Frozen = true;
            this.dataGridViewTextBoxColumn24.HeaderText = "Load No";
            this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
            this.dataGridViewTextBoxColumn24.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn24.Width = 50;
            // 
            // dataGridViewTextBoxColumn26
            // 
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn26.DefaultCellStyle = dataGridViewCellStyle31;
            this.dataGridViewTextBoxColumn26.HeaderText = "FX";
            this.dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
            this.dataGridViewTextBoxColumn26.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn26.Width = 70;
            // 
            // dataGridViewTextBoxColumn27
            // 
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn27.DefaultCellStyle = dataGridViewCellStyle32;
            this.dataGridViewTextBoxColumn27.HeaderText = "FY";
            this.dataGridViewTextBoxColumn27.Name = "dataGridViewTextBoxColumn27";
            this.dataGridViewTextBoxColumn27.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn27.Width = 70;
            // 
            // dataGridViewTextBoxColumn28
            // 
            dataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn28.DefaultCellStyle = dataGridViewCellStyle33;
            this.dataGridViewTextBoxColumn28.HeaderText = "FZ";
            this.dataGridViewTextBoxColumn28.Name = "dataGridViewTextBoxColumn28";
            this.dataGridViewTextBoxColumn28.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn28.Width = 70;
            // 
            // dataGridViewTextBoxColumn29
            // 
            dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn29.DefaultCellStyle = dataGridViewCellStyle34;
            this.dataGridViewTextBoxColumn29.HeaderText = "MX";
            this.dataGridViewTextBoxColumn29.Name = "dataGridViewTextBoxColumn29";
            this.dataGridViewTextBoxColumn29.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn29.Width = 70;
            // 
            // dataGridViewTextBoxColumn30
            // 
            dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn30.DefaultCellStyle = dataGridViewCellStyle35;
            this.dataGridViewTextBoxColumn30.HeaderText = "MY";
            this.dataGridViewTextBoxColumn30.Name = "dataGridViewTextBoxColumn30";
            this.dataGridViewTextBoxColumn30.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn30.Width = 70;
            // 
            // dataGridViewTextBoxColumn31
            // 
            dataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn31.DefaultCellStyle = dataGridViewCellStyle36;
            this.dataGridViewTextBoxColumn31.HeaderText = "MZ";
            this.dataGridViewTextBoxColumn31.Name = "dataGridViewTextBoxColumn31";
            this.dataGridViewTextBoxColumn31.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn31.Width = 70;
            // 
            // tab_3_mem_frc
            // 
            this.tab_3_mem_frc.Controls.Add(this.dgv_beam_frcs);
            this.tab_3_mem_frc.Location = new System.Drawing.Point(4, 22);
            this.tab_3_mem_frc.Name = "tab_3_mem_frc";
            this.tab_3_mem_frc.Padding = new System.Windows.Forms.Padding(3);
            this.tab_3_mem_frc.Size = new System.Drawing.Size(300, 478);
            this.tab_3_mem_frc.TabIndex = 1;
            this.tab_3_mem_frc.Text = "Beam Forces";
            this.tab_3_mem_frc.UseVisualStyleBackColor = true;
            // 
            // dgv_beam_frcs
            // 
            this.dgv_beam_frcs.AllowUserToAddRows = false;
            this.dgv_beam_frcs.AllowUserToDeleteRows = false;
            dataGridViewCellStyle37.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle37.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle37.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle37.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle37.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle37.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle37.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_beam_frcs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle37;
            this.dgv_beam_frcs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_beam_frcs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn15,
            this.dataGridViewTextBoxColumn16,
            this.dataGridViewTextBoxColumn17,
            this.dataGridViewTextBoxColumn18,
            this.dataGridViewTextBoxColumn19,
            this.dataGridViewTextBoxColumn20,
            this.dataGridViewTextBoxColumn21,
            this.dataGridViewTextBoxColumn22,
            this.Column13});
            this.dgv_beam_frcs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_beam_frcs.Location = new System.Drawing.Point(3, 3);
            this.dgv_beam_frcs.Name = "dgv_beam_frcs";
            this.dgv_beam_frcs.RowHeadersWidth = 21;
            this.dgv_beam_frcs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_beam_frcs.Size = new System.Drawing.Size(294, 472);
            this.dgv_beam_frcs.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn15
            // 
            dataGridViewCellStyle38.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridViewTextBoxColumn15.DefaultCellStyle = dataGridViewCellStyle38;
            this.dataGridViewTextBoxColumn15.Frozen = true;
            this.dataGridViewTextBoxColumn15.HeaderText = "Beam#";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn15.Width = 50;
            // 
            // dataGridViewTextBoxColumn16
            // 
            dataGridViewCellStyle39.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGridViewTextBoxColumn16.DefaultCellStyle = dataGridViewCellStyle39;
            this.dataGridViewTextBoxColumn16.Frozen = true;
            this.dataGridViewTextBoxColumn16.HeaderText = "Load No";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn16.Width = 50;
            // 
            // dataGridViewTextBoxColumn17
            // 
            dataGridViewCellStyle40.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn17.DefaultCellStyle = dataGridViewCellStyle40;
            this.dataGridViewTextBoxColumn17.HeaderText = "Node No";
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn17.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn17.Width = 50;
            // 
            // dataGridViewTextBoxColumn18
            // 
            dataGridViewCellStyle41.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn18.DefaultCellStyle = dataGridViewCellStyle41;
            this.dataGridViewTextBoxColumn18.HeaderText = "FX / AXIAL";
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            this.dataGridViewTextBoxColumn18.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn18.Width = 70;
            // 
            // dataGridViewTextBoxColumn19
            // 
            dataGridViewCellStyle42.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn19.DefaultCellStyle = dataGridViewCellStyle42;
            this.dataGridViewTextBoxColumn19.HeaderText = "FY / SHEAR";
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            this.dataGridViewTextBoxColumn19.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn19.Width = 70;
            // 
            // dataGridViewTextBoxColumn20
            // 
            dataGridViewCellStyle43.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn20.DefaultCellStyle = dataGridViewCellStyle43;
            this.dataGridViewTextBoxColumn20.HeaderText = "FZ / SHEAR";
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            this.dataGridViewTextBoxColumn20.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn20.Width = 70;
            // 
            // dataGridViewTextBoxColumn21
            // 
            dataGridViewCellStyle44.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn21.DefaultCellStyle = dataGridViewCellStyle44;
            this.dataGridViewTextBoxColumn21.HeaderText = "MX / TORSION";
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            this.dataGridViewTextBoxColumn21.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn21.Width = 70;
            // 
            // dataGridViewTextBoxColumn22
            // 
            dataGridViewCellStyle45.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn22.DefaultCellStyle = dataGridViewCellStyle45;
            this.dataGridViewTextBoxColumn22.HeaderText = "MY / MOMENT";
            this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
            this.dataGridViewTextBoxColumn22.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn22.Width = 70;
            // 
            // Column13
            // 
            dataGridViewCellStyle46.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column13.DefaultCellStyle = dataGridViewCellStyle46;
            this.Column13.HeaderText = "MZ / MOMENT";
            this.Column13.Name = "Column13";
            this.Column13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column13.Width = 70;
            // 
            // tab_3_truss_frc
            // 
            this.tab_3_truss_frc.Controls.Add(this.dgv_truss_frcs);
            this.tab_3_truss_frc.Location = new System.Drawing.Point(4, 22);
            this.tab_3_truss_frc.Name = "tab_3_truss_frc";
            this.tab_3_truss_frc.Size = new System.Drawing.Size(300, 478);
            this.tab_3_truss_frc.TabIndex = 2;
            this.tab_3_truss_frc.Text = "Truss / Cable Forces";
            this.tab_3_truss_frc.UseVisualStyleBackColor = true;
            // 
            // dgv_truss_frcs
            // 
            this.dgv_truss_frcs.AllowUserToAddRows = false;
            this.dgv_truss_frcs.AllowUserToDeleteRows = false;
            this.dgv_truss_frcs.AllowUserToOrderColumns = true;
            dataGridViewCellStyle47.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle47.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle47.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle47.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle47.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle47.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle47.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_truss_frcs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle47;
            this.dgv_truss_frcs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_truss_frcs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dgv_truss_frcs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_truss_frcs.Location = new System.Drawing.Point(0, 0);
            this.dgv_truss_frcs.Name = "dgv_truss_frcs";
            this.dgv_truss_frcs.RowHeadersWidth = 21;
            this.dgv_truss_frcs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_truss_frcs.Size = new System.Drawing.Size(300, 478);
            this.dgv_truss_frcs.TabIndex = 0;
            // 
            // Column1
            // 
            dataGridViewCellStyle48.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle48.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Column1.DefaultCellStyle = dataGridViewCellStyle48;
            this.Column1.Frozen = true;
            this.Column1.HeaderText = "Member#";
            this.Column1.Name = "Column1";
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 70;
            // 
            // Column2
            // 
            dataGridViewCellStyle49.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle49.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.Column2.DefaultCellStyle = dataGridViewCellStyle49;
            this.Column2.Frozen = true;
            this.Column2.HeaderText = "Load Case";
            this.Column2.Name = "Column2";
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 50;
            // 
            // Column3
            // 
            dataGridViewCellStyle50.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle50;
            this.Column3.HeaderText = "Stress";
            this.Column3.Name = "Column3";
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 99;
            // 
            // Column4
            // 
            dataGridViewCellStyle51.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle51;
            this.Column4.HeaderText = "Force";
            this.Column4.Name = "Column4";
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 99;
            // 
            // tab1_load_deflection
            // 
            this.tab1_load_deflection.Controls.Add(this.dgv_node_disp);
            this.tab1_load_deflection.Controls.Add(this.panel3);
            this.tab1_load_deflection.Location = new System.Drawing.Point(24, 4);
            this.tab1_load_deflection.Name = "tab1_load_deflection";
            this.tab1_load_deflection.Padding = new System.Windows.Forms.Padding(3);
            this.tab1_load_deflection.Size = new System.Drawing.Size(307, 504);
            this.tab1_load_deflection.TabIndex = 3;
            this.tab1_load_deflection.Text = "Deflection";
            this.tab1_load_deflection.UseVisualStyleBackColor = true;
            // 
            // dgv_node_disp
            // 
            this.dgv_node_disp.AllowUserToAddRows = false;
            this.dgv_node_disp.AllowUserToDeleteRows = false;
            this.dgv_node_disp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_node_disp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12});
            this.dgv_node_disp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_node_disp.Location = new System.Drawing.Point(3, 3);
            this.dgv_node_disp.Name = "dgv_node_disp";
            this.dgv_node_disp.ReadOnly = true;
            this.dgv_node_disp.RowHeadersWidth = 21;
            this.dgv_node_disp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_node_disp.Size = new System.Drawing.Size(301, 432);
            this.dgv_node_disp.TabIndex = 0;
            // 
            // Column5
            // 
            dataGridViewCellStyle52.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle52.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Column5.DefaultCellStyle = dataGridViewCellStyle52;
            this.Column5.Frozen = true;
            this.Column5.HeaderText = "Node#";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column5.Width = 50;
            // 
            // Column6
            // 
            dataGridViewCellStyle53.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle53.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.Column6.DefaultCellStyle = dataGridViewCellStyle53;
            this.Column6.Frozen = true;
            this.Column6.HeaderText = "Load Case";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column6.Width = 50;
            // 
            // Column7
            // 
            dataGridViewCellStyle54.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column7.DefaultCellStyle = dataGridViewCellStyle54;
            this.Column7.HeaderText = "X-Translation";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column7.Width = 90;
            // 
            // Column8
            // 
            dataGridViewCellStyle55.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column8.DefaultCellStyle = dataGridViewCellStyle55;
            this.Column8.HeaderText = "Y-Translation";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column8.Width = 90;
            // 
            // Column9
            // 
            dataGridViewCellStyle56.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column9.DefaultCellStyle = dataGridViewCellStyle56;
            this.Column9.HeaderText = "Z-Translation";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column9.Width = 90;
            // 
            // Column10
            // 
            dataGridViewCellStyle57.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column10.DefaultCellStyle = dataGridViewCellStyle57;
            this.Column10.HeaderText = "X-Rotation";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column10.Width = 90;
            // 
            // Column11
            // 
            dataGridViewCellStyle58.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column11.DefaultCellStyle = dataGridViewCellStyle58;
            this.Column11.HeaderText = "Y-Rotation";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column11.Width = 90;
            // 
            // Column12
            // 
            dataGridViewCellStyle59.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column12.DefaultCellStyle = dataGridViewCellStyle59;
            this.Column12.HeaderText = "Z-Rotation";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column12.Width = 90;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txt_max_deflection_load);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.txt_max_deflection_node);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.txt_max_deflection);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(3, 435);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(301, 66);
            this.panel3.TabIndex = 5;
            // 
            // txt_max_deflection_load
            // 
            this.txt_max_deflection_load.Location = new System.Drawing.Point(242, 35);
            this.txt_max_deflection_load.Name = "txt_max_deflection_load";
            this.txt_max_deflection_load.ReadOnly = true;
            this.txt_max_deflection_load.Size = new System.Drawing.Size(55, 21);
            this.txt_max_deflection_load.TabIndex = 4;
            this.txt_max_deflection_load.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(242, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Load No";
            // 
            // txt_max_deflection_node
            // 
            this.txt_max_deflection_node.Location = new System.Drawing.Point(151, 35);
            this.txt_max_deflection_node.Name = "txt_max_deflection_node";
            this.txt_max_deflection_node.ReadOnly = true;
            this.txt_max_deflection_node.Size = new System.Drawing.Size(55, 21);
            this.txt_max_deflection_node.TabIndex = 4;
            this.txt_max_deflection_node.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(151, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Node No";
            // 
            // txt_max_deflection
            // 
            this.txt_max_deflection.Location = new System.Drawing.Point(12, 35);
            this.txt_max_deflection.Name = "txt_max_deflection";
            this.txt_max_deflection.ReadOnly = true;
            this.txt_max_deflection.Size = new System.Drawing.Size(108, 21);
            this.txt_max_deflection.TabIndex = 2;
            this.txt_max_deflection.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 26);
            this.label7.TabIndex = 1;
            this.label7.Text = "Maximum Vertical\r\n Deflection";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tab1_max_force
            // 
            this.tab1_max_force.Controls.Add(this.dgv_max_frc);
            this.tab1_max_force.Location = new System.Drawing.Point(24, 4);
            this.tab1_max_force.Name = "tab1_max_force";
            this.tab1_max_force.Padding = new System.Windows.Forms.Padding(3);
            this.tab1_max_force.Size = new System.Drawing.Size(307, 504);
            this.tab1_max_force.TabIndex = 4;
            this.tab1_max_force.Text = "Max Force";
            this.tab1_max_force.UseVisualStyleBackColor = true;
            // 
            // dgv_max_frc
            // 
            this.dgv_max_frc.AllowUserToAddRows = false;
            this.dgv_max_frc.AllowUserToDeleteRows = false;
            dataGridViewCellStyle60.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle60.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle60.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle60.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle60.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle60.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle60.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_max_frc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle60;
            this.dgv_max_frc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_max_frc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn25,
            this.dataGridViewTextBoxColumn32,
            this.col_cmf,
            this.col_tf,
            this.col_af,
            this.col_tor,
            this.col_bm,
            this.col_sf});
            this.dgv_max_frc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_max_frc.Location = new System.Drawing.Point(3, 3);
            this.dgv_max_frc.Name = "dgv_max_frc";
            this.dgv_max_frc.RowHeadersWidth = 21;
            this.dgv_max_frc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_max_frc.Size = new System.Drawing.Size(301, 498);
            this.dgv_max_frc.TabIndex = 1;
            this.dgv_max_frc.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_max_frc_CellEnter);
            this.dgv_max_frc.SelectionChanged += new System.EventHandler(this.dgv_max_frc_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn25
            // 
            dataGridViewCellStyle61.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle61.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridViewTextBoxColumn25.DefaultCellStyle = dataGridViewCellStyle61;
            this.dataGridViewTextBoxColumn25.Frozen = true;
            this.dataGridViewTextBoxColumn25.HeaderText = "Member#";
            this.dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
            this.dataGridViewTextBoxColumn25.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn25.Width = 70;
            // 
            // dataGridViewTextBoxColumn32
            // 
            dataGridViewCellStyle62.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle62.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGridViewTextBoxColumn32.DefaultCellStyle = dataGridViewCellStyle62;
            this.dataGridViewTextBoxColumn32.Frozen = true;
            this.dataGridViewTextBoxColumn32.HeaderText = "Type";
            this.dataGridViewTextBoxColumn32.Name = "dataGridViewTextBoxColumn32";
            this.dataGridViewTextBoxColumn32.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn32.Width = 50;
            // 
            // col_cmf
            // 
            dataGridViewCellStyle63.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle63.Format = "N3";
            this.col_cmf.DefaultCellStyle = dataGridViewCellStyle63;
            this.col_cmf.HeaderText = "Compressive Force";
            this.col_cmf.Name = "col_cmf";
            this.col_cmf.Width = 99;
            // 
            // col_tf
            // 
            dataGridViewCellStyle64.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle64.Format = "N3";
            this.col_tf.DefaultCellStyle = dataGridViewCellStyle64;
            this.col_tf.HeaderText = "Tensile Force";
            this.col_tf.Name = "col_tf";
            this.col_tf.Width = 99;
            // 
            // col_af
            // 
            dataGridViewCellStyle65.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle65.Format = "N3";
            dataGridViewCellStyle65.NullValue = null;
            this.col_af.DefaultCellStyle = dataGridViewCellStyle65;
            this.col_af.HeaderText = "Axial Force";
            this.col_af.Name = "col_af";
            this.col_af.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.col_af.Width = 99;
            // 
            // col_tor
            // 
            dataGridViewCellStyle66.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle66.Format = "N3";
            this.col_tor.DefaultCellStyle = dataGridViewCellStyle66;
            this.col_tor.HeaderText = "Torsion";
            this.col_tor.Name = "col_tor";
            this.col_tor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.col_tor.Width = 99;
            // 
            // col_bm
            // 
            dataGridViewCellStyle67.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle67.Format = "N3";
            this.col_bm.DefaultCellStyle = dataGridViewCellStyle67;
            this.col_bm.HeaderText = "Bending Moment";
            this.col_bm.Name = "col_bm";
            this.col_bm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.col_bm.Width = 99;
            // 
            // col_sf
            // 
            dataGridViewCellStyle68.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle68.Format = "N3";
            this.col_sf.DefaultCellStyle = dataGridViewCellStyle68;
            this.col_sf.HeaderText = "Shear Force";
            this.col_sf.Name = "col_sf";
            this.col_sf.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.col_sf.Width = 99;
            // 
            // tab1_moving_load
            // 
            this.tab1_moving_load.Controls.Add(this.sc2);
            this.tab1_moving_load.Location = new System.Drawing.Point(24, 4);
            this.tab1_moving_load.Name = "tab1_moving_load";
            this.tab1_moving_load.Padding = new System.Windows.Forms.Padding(3);
            this.tab1_moving_load.Size = new System.Drawing.Size(307, 504);
            this.tab1_moving_load.TabIndex = 5;
            this.tab1_moving_load.Text = "Moving Load";
            this.tab1_moving_load.UseVisualStyleBackColor = true;
            // 
            // sc2
            // 
            this.sc2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sc2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc2.Location = new System.Drawing.Point(3, 3);
            this.sc2.Name = "sc2";
            this.sc2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // sc2.Panel1
            // 
            this.sc2.Panel1.Controls.Add(this.groupBox4);
            // 
            // sc2.Panel2
            // 
            this.sc2.Panel2.Controls.Add(this.groupBox9);
            this.sc2.Size = new System.Drawing.Size(301, 498);
            this.sc2.SplitterDistance = 330;
            this.sc2.TabIndex = 10;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tv_ml_loads);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(299, 328);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Define Vehicle Axle Loads";
            // 
            // tv_ml_loads
            // 
            this.tv_ml_loads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv_ml_loads.Location = new System.Drawing.Point(3, 17);
            this.tv_ml_loads.Name = "tv_ml_loads";
            this.tv_ml_loads.Size = new System.Drawing.Size(293, 308);
            this.tv_ml_loads.TabIndex = 0;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.tv_ml_def);
            this.groupBox9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox9.ForeColor = System.Drawing.Color.Black;
            this.groupBox9.Location = new System.Drawing.Point(0, 0);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(299, 162);
            this.groupBox9.TabIndex = 9;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Define Load Start position";
            // 
            // tv_ml_def
            // 
            this.tv_ml_def.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv_ml_def.Location = new System.Drawing.Point(3, 17);
            this.tv_ml_def.Name = "tv_ml_def";
            this.tv_ml_def.Size = new System.Drawing.Size(293, 142);
            this.tv_ml_def.TabIndex = 1;
            // 
            // tab1_truss_env
            // 
            this.tab1_truss_env.Location = new System.Drawing.Point(24, 4);
            this.tab1_truss_env.Name = "tab1_truss_env";
            this.tab1_truss_env.Padding = new System.Windows.Forms.Padding(3);
            this.tab1_truss_env.Size = new System.Drawing.Size(307, 504);
            this.tab1_truss_env.TabIndex = 6;
            this.tab1_truss_env.Text = "Env";
            this.tab1_truss_env.UseVisualStyleBackColor = true;
            // 
            // tc4
            // 
            this.tc4.Controls.Add(this.tab_org_doc);
            this.tc4.Controls.Add(this.tab_defl_doc);
            this.tc4.Controls.Add(this.tab_max_doc);
            this.tc4.Controls.Add(this.tab_mov_doc);
            this.tc4.Controls.Add(this.tab_evlp_doc);
            this.tc4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tc4.Location = new System.Drawing.Point(0, 25);
            this.tc4.Name = "tc4";
            this.tc4.SelectedIndex = 0;
            this.tc4.Size = new System.Drawing.Size(563, 436);
            this.tc4.TabIndex = 3;
            // 
            // tab_org_doc
            // 
            this.tab_org_doc.Controls.Add(this.vdScrollableControl1);
            this.tab_org_doc.Location = new System.Drawing.Point(4, 22);
            this.tab_org_doc.Name = "tab_org_doc";
            this.tab_org_doc.Padding = new System.Windows.Forms.Padding(3);
            this.tab_org_doc.Size = new System.Drawing.Size(555, 410);
            this.tab_org_doc.TabIndex = 0;
            this.tab_org_doc.Text = "Original Structure";
            this.tab_org_doc.UseVisualStyleBackColor = true;
            // 
            // vdScrollableControl1
            // 
            this.vdScrollableControl1.BackColor = System.Drawing.SystemColors.Control;
            this.vdScrollableControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.vdScrollableControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vdScrollableControl1.Location = new System.Drawing.Point(3, 3);
            this.vdScrollableControl1.Name = "vdScrollableControl1";
            this.vdScrollableControl1.ShowLayoutPopupMenu = true;
            this.vdScrollableControl1.Size = new System.Drawing.Size(549, 404);
            this.vdScrollableControl1.TabIndex = 0;
            // 
            // tab_defl_doc
            // 
            this.tab_defl_doc.Controls.Add(this.vdScrollableControl2);
            this.tab_defl_doc.Location = new System.Drawing.Point(4, 22);
            this.tab_defl_doc.Name = "tab_defl_doc";
            this.tab_defl_doc.Padding = new System.Windows.Forms.Padding(3);
            this.tab_defl_doc.Size = new System.Drawing.Size(555, 410);
            this.tab_defl_doc.TabIndex = 1;
            this.tab_defl_doc.Text = "Load Deflection";
            this.tab_defl_doc.UseVisualStyleBackColor = true;
            // 
            // vdScrollableControl2
            // 
            this.vdScrollableControl2.BackColor = System.Drawing.SystemColors.Control;
            this.vdScrollableControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vdScrollableControl2.Location = new System.Drawing.Point(3, 3);
            this.vdScrollableControl2.Name = "vdScrollableControl2";
            this.vdScrollableControl2.ShowLayoutPopupMenu = true;
            this.vdScrollableControl2.Size = new System.Drawing.Size(549, 404);
            this.vdScrollableControl2.TabIndex = 0;
            // 
            // tab_max_doc
            // 
            this.tab_max_doc.Controls.Add(this.vdScrollableControl5);
            this.tab_max_doc.Location = new System.Drawing.Point(4, 22);
            this.tab_max_doc.Name = "tab_max_doc";
            this.tab_max_doc.Padding = new System.Windows.Forms.Padding(3);
            this.tab_max_doc.Size = new System.Drawing.Size(555, 410);
            this.tab_max_doc.TabIndex = 4;
            this.tab_max_doc.Text = "Max Force";
            this.tab_max_doc.UseVisualStyleBackColor = true;
            // 
            // vdScrollableControl5
            // 
            this.vdScrollableControl5.BackColor = System.Drawing.SystemColors.Control;
            this.vdScrollableControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vdScrollableControl5.Location = new System.Drawing.Point(3, 3);
            this.vdScrollableControl5.Name = "vdScrollableControl5";
            this.vdScrollableControl5.ShowLayoutPopupMenu = true;
            this.vdScrollableControl5.Size = new System.Drawing.Size(549, 404);
            this.vdScrollableControl5.TabIndex = 0;
            // 
            // tab_mov_doc
            // 
            this.tab_mov_doc.Controls.Add(this.vdScrollableControl4);
            this.tab_mov_doc.Location = new System.Drawing.Point(4, 22);
            this.tab_mov_doc.Name = "tab_mov_doc";
            this.tab_mov_doc.Padding = new System.Windows.Forms.Padding(3);
            this.tab_mov_doc.Size = new System.Drawing.Size(555, 410);
            this.tab_mov_doc.TabIndex = 3;
            this.tab_mov_doc.Text = "Moving Load";
            this.tab_mov_doc.UseVisualStyleBackColor = true;
            // 
            // vdScrollableControl4
            // 
            this.vdScrollableControl4.BackColor = System.Drawing.SystemColors.Control;
            this.vdScrollableControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vdScrollableControl4.Location = new System.Drawing.Point(3, 3);
            this.vdScrollableControl4.Name = "vdScrollableControl4";
            this.vdScrollableControl4.ShowLayoutPopupMenu = true;
            this.vdScrollableControl4.Size = new System.Drawing.Size(549, 404);
            this.vdScrollableControl4.TabIndex = 0;
            // 
            // tab_evlp_doc
            // 
            this.tab_evlp_doc.Controls.Add(this.vdScrollableControl3);
            this.tab_evlp_doc.Location = new System.Drawing.Point(4, 22);
            this.tab_evlp_doc.Name = "tab_evlp_doc";
            this.tab_evlp_doc.Padding = new System.Windows.Forms.Padding(3);
            this.tab_evlp_doc.Size = new System.Drawing.Size(555, 410);
            this.tab_evlp_doc.TabIndex = 2;
            this.tab_evlp_doc.Text = "Truss Envelope";
            this.tab_evlp_doc.UseVisualStyleBackColor = true;
            // 
            // vdScrollableControl3
            // 
            this.vdScrollableControl3.BackColor = System.Drawing.SystemColors.Control;
            this.vdScrollableControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vdScrollableControl3.Location = new System.Drawing.Point(3, 3);
            this.vdScrollableControl3.Name = "vdScrollableControl3";
            this.vdScrollableControl3.ShowLayoutPopupMenu = true;
            this.vdScrollableControl3.Size = new System.Drawing.Size(549, 404);
            this.vdScrollableControl3.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.cmb_text_size);
            this.panel5.Controls.Add(this.chk_elems);
            this.panel5.Controls.Add(this.chk_joints);
            this.panel5.Controls.Add(this.chk_suprts);
            this.panel5.Controls.Add(this.chk_mems);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 461);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(563, 25);
            this.panel5.TabIndex = 4;
            // 
            // chk_joints
            // 
            this.chk_joints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chk_joints.AutoSize = true;
            this.chk_joints.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_joints.Location = new System.Drawing.Point(47, 5);
            this.chk_joints.Name = "chk_joints";
            this.chk_joints.Size = new System.Drawing.Size(61, 17);
            this.chk_joints.TabIndex = 2;
            this.chk_joints.Text = "Nodes";
            this.chk_joints.UseVisualStyleBackColor = true;
            this.chk_joints.CheckedChanged += new System.EventHandler(this.chk_joints_CheckedChanged);
            // 
            // pnl_progress
            // 
            this.pnl_progress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_progress.Controls.Add(this.pbLoadDeflection);
            this.pnl_progress.Controls.Add(this.lblPleaseWait);
            this.pnl_progress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_progress.Location = new System.Drawing.Point(0, 486);
            this.pnl_progress.Name = "pnl_progress";
            this.pnl_progress.Size = new System.Drawing.Size(563, 26);
            this.pnl_progress.TabIndex = 2;
            // 
            // pbLoadDeflection
            // 
            this.pbLoadDeflection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbLoadDeflection.Location = new System.Drawing.Point(0, 0);
            this.pbLoadDeflection.Name = "pbLoadDeflection";
            this.pbLoadDeflection.Size = new System.Drawing.Size(561, 24);
            this.pbLoadDeflection.TabIndex = 24;
            this.pbLoadDeflection.VisibleChanged += new System.EventHandler(this.pbLoadDeflection_VisibleChanged);
            // 
            // lblPleaseWait
            // 
            this.lblPleaseWait.AutoSize = true;
            this.lblPleaseWait.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPleaseWait.Location = new System.Drawing.Point(3, 5);
            this.lblPleaseWait.Name = "lblPleaseWait";
            this.lblPleaseWait.Size = new System.Drawing.Size(120, 16);
            this.lblPleaseWait.TabIndex = 25;
            this.lblPleaseWait.Text = "Please wait...";
            this.lblPleaseWait.Visible = false;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_3D_rotate,
            this.tsb_VTop,
            this.tsb_VBot,
            this.tsb_VLeft,
            this.tsb_VRight,
            this.tsb_VFront,
            this.tsb_VBack,
            this.tsb_VNE,
            this.tsb_VNW,
            this.tsb_VSE,
            this.tsb_VSW,
            this.toolStripSeparator2,
            this.tsb_ShadeOn,
            this.tsb_Wire,
            this.tss1,
            this.tsb_ZoomA,
            this.tsb_ZoomE,
            this.tsb_ZoomP,
            this.tsb_ZoomW,
            this.tsb_ZoomIn,
            this.tsb_ZoomOut,
            this.tsb_Pan,
            this.toolStripSeparator1,
            this.tsb_Save});
            this.toolStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(563, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "ts_1";
            // 
            // tsb_3D_rotate
            // 
            this.tsb_3D_rotate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_3D_rotate.Image = ((System.Drawing.Image)(resources.GetObject("tsb_3D_rotate.Image")));
            this.tsb_3D_rotate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_3D_rotate.Name = "tsb_3D_rotate";
            this.tsb_3D_rotate.Size = new System.Drawing.Size(23, 22);
            this.tsb_3D_rotate.Text = "Rotate 3D";
            this.tsb_3D_rotate.Click += new System.EventHandler(this.toolStripButtons_Click);
            // 
            // tsb_VTop
            // 
            this.tsb_VTop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_VTop.Image = ((System.Drawing.Image)(resources.GetObject("tsb_VTop.Image")));
            this.tsb_VTop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_VTop.Name = "tsb_VTop";
            this.tsb_VTop.Size = new System.Drawing.Size(23, 22);
            this.tsb_VTop.Text = "View Top";
            this.tsb_VTop.Click += new System.EventHandler(this.toolStripButtons_Click);
            // 
            // tsb_VBot
            // 
            this.tsb_VBot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_VBot.Image = global::HEADSNeed.Properties.Resources._3DVBOTTOM;
            this.tsb_VBot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_VBot.Name = "tsb_VBot";
            this.tsb_VBot.Size = new System.Drawing.Size(23, 22);
            this.tsb_VBot.Text = "View Bottom";
            this.tsb_VBot.Click += new System.EventHandler(this.toolStripButtons_Click);
            // 
            // tsb_VLeft
            // 
            this.tsb_VLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_VLeft.Image = global::HEADSNeed.Properties.Resources._3DVLEFT;
            this.tsb_VLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_VLeft.Name = "tsb_VLeft";
            this.tsb_VLeft.Size = new System.Drawing.Size(23, 22);
            this.tsb_VLeft.Text = "View Left";
            this.tsb_VLeft.Click += new System.EventHandler(this.toolStripButtons_Click);
            // 
            // tsb_VRight
            // 
            this.tsb_VRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_VRight.Image = global::HEADSNeed.Properties.Resources._3DVRIGHT;
            this.tsb_VRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_VRight.Name = "tsb_VRight";
            this.tsb_VRight.Size = new System.Drawing.Size(23, 22);
            this.tsb_VRight.Text = "View Right";
            this.tsb_VRight.Click += new System.EventHandler(this.toolStripButtons_Click);
            // 
            // tsb_VFront
            // 
            this.tsb_VFront.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_VFront.Image = global::HEADSNeed.Properties.Resources._3DVFRONT;
            this.tsb_VFront.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_VFront.Name = "tsb_VFront";
            this.tsb_VFront.Size = new System.Drawing.Size(23, 22);
            this.tsb_VFront.Text = "View Front";
            this.tsb_VFront.Click += new System.EventHandler(this.toolStripButtons_Click);
            // 
            // tsb_VBack
            // 
            this.tsb_VBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_VBack.Image = global::HEADSNeed.Properties.Resources._3DVBACK;
            this.tsb_VBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_VBack.Name = "tsb_VBack";
            this.tsb_VBack.Size = new System.Drawing.Size(23, 22);
            this.tsb_VBack.Text = "View Back";
            this.tsb_VBack.Click += new System.EventHandler(this.toolStripButtons_Click);
            // 
            // tsb_VNE
            // 
            this.tsb_VNE.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_VNE.Image = global::HEADSNeed.Properties.Resources._3DVNE;
            this.tsb_VNE.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_VNE.Name = "tsb_VNE";
            this.tsb_VNE.Size = new System.Drawing.Size(23, 22);
            this.tsb_VNE.Text = "View NE";
            this.tsb_VNE.Click += new System.EventHandler(this.toolStripButtons_Click);
            // 
            // tsb_VNW
            // 
            this.tsb_VNW.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_VNW.Image = global::HEADSNeed.Properties.Resources._3DVNW;
            this.tsb_VNW.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_VNW.Name = "tsb_VNW";
            this.tsb_VNW.Size = new System.Drawing.Size(23, 22);
            this.tsb_VNW.Text = "View NW";
            this.tsb_VNW.Click += new System.EventHandler(this.toolStripButtons_Click);
            // 
            // tsb_VSE
            // 
            this.tsb_VSE.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_VSE.Image = global::HEADSNeed.Properties.Resources._3DVSE;
            this.tsb_VSE.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_VSE.Name = "tsb_VSE";
            this.tsb_VSE.Size = new System.Drawing.Size(23, 22);
            this.tsb_VSE.Text = "View SE";
            this.tsb_VSE.Click += new System.EventHandler(this.toolStripButtons_Click);
            // 
            // tsb_VSW
            // 
            this.tsb_VSW.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_VSW.Image = global::HEADSNeed.Properties.Resources._3DVSW;
            this.tsb_VSW.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_VSW.Name = "tsb_VSW";
            this.tsb_VSW.Size = new System.Drawing.Size(23, 22);
            this.tsb_VSW.Text = "View SW";
            this.tsb_VSW.Click += new System.EventHandler(this.toolStripButtons_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsb_ShadeOn
            // 
            this.tsb_ShadeOn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_ShadeOn.Image = global::HEADSNeed.Properties.Resources._3dEdgeON;
            this.tsb_ShadeOn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ShadeOn.Name = "tsb_ShadeOn";
            this.tsb_ShadeOn.Size = new System.Drawing.Size(23, 22);
            this.tsb_ShadeOn.Text = "Shade On";
            this.tsb_ShadeOn.Click += new System.EventHandler(this.toolStripButtons_Click);
            // 
            // tsb_Wire
            // 
            this.tsb_Wire.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_Wire.Image = global::HEADSNeed.Properties.Resources._3D_wire;
            this.tsb_Wire.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Wire.Name = "tsb_Wire";
            this.tsb_Wire.Size = new System.Drawing.Size(23, 22);
            this.tsb_Wire.Text = "Wire";
            this.tsb_Wire.Click += new System.EventHandler(this.toolStripButtons_Click);
            // 
            // tss1
            // 
            this.tss1.Name = "tss1";
            this.tss1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsb_ZoomA
            // 
            this.tsb_ZoomA.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_ZoomA.Image = global::HEADSNeed.Properties.Resources.zoomALL;
            this.tsb_ZoomA.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ZoomA.Name = "tsb_ZoomA";
            this.tsb_ZoomA.Size = new System.Drawing.Size(23, 22);
            this.tsb_ZoomA.Text = "Zom All";
            this.tsb_ZoomA.Click += new System.EventHandler(this.toolStripButtons_Click);
            // 
            // tsb_ZoomE
            // 
            this.tsb_ZoomE.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_ZoomE.Image = global::HEADSNeed.Properties.Resources.ZoomE;
            this.tsb_ZoomE.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ZoomE.Name = "tsb_ZoomE";
            this.tsb_ZoomE.Size = new System.Drawing.Size(23, 22);
            this.tsb_ZoomE.Text = "Zoom Extent";
            this.tsb_ZoomE.Click += new System.EventHandler(this.toolStripButtons_Click);
            // 
            // tsb_ZoomP
            // 
            this.tsb_ZoomP.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_ZoomP.Image = global::HEADSNeed.Properties.Resources.zoomP;
            this.tsb_ZoomP.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ZoomP.Name = "tsb_ZoomP";
            this.tsb_ZoomP.Size = new System.Drawing.Size(23, 22);
            this.tsb_ZoomP.Text = "Zoom Previous";
            this.tsb_ZoomP.Click += new System.EventHandler(this.toolStripButtons_Click);
            // 
            // tsb_ZoomW
            // 
            this.tsb_ZoomW.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_ZoomW.Image = global::HEADSNeed.Properties.Resources.ZoomW;
            this.tsb_ZoomW.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ZoomW.Name = "tsb_ZoomW";
            this.tsb_ZoomW.Size = new System.Drawing.Size(23, 22);
            this.tsb_ZoomW.Text = "Zoom Window";
            this.tsb_ZoomW.Click += new System.EventHandler(this.toolStripButtons_Click);
            // 
            // tsb_ZoomIn
            // 
            this.tsb_ZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_ZoomIn.Image = global::HEADSNeed.Properties.Resources.zoomIN;
            this.tsb_ZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ZoomIn.Name = "tsb_ZoomIn";
            this.tsb_ZoomIn.Size = new System.Drawing.Size(23, 22);
            this.tsb_ZoomIn.Text = "Zoom In";
            this.tsb_ZoomIn.Click += new System.EventHandler(this.toolStripButtons_Click);
            // 
            // tsb_ZoomOut
            // 
            this.tsb_ZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_ZoomOut.Image = global::HEADSNeed.Properties.Resources.ZoomOUT;
            this.tsb_ZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ZoomOut.Name = "tsb_ZoomOut";
            this.tsb_ZoomOut.Size = new System.Drawing.Size(23, 22);
            this.tsb_ZoomOut.Text = "Zoom Out";
            this.tsb_ZoomOut.Click += new System.EventHandler(this.toolStripButtons_Click);
            // 
            // tsb_Pan
            // 
            this.tsb_Pan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_Pan.Image = global::HEADSNeed.Properties.Resources.pan;
            this.tsb_Pan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Pan.Name = "tsb_Pan";
            this.tsb_Pan.Size = new System.Drawing.Size(23, 22);
            this.tsb_Pan.Text = "Pan";
            this.tsb_Pan.Click += new System.EventHandler(this.toolStripButtons_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsb_Save
            // 
            this.tsb_Save.Image = global::HEADSNeed.Properties.Resources.saveFile;
            this.tsb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Save.Name = "tsb_Save";
            this.tsb_Save.Size = new System.Drawing.Size(51, 22);
            this.tsb_Save.Text = "Save";
            this.tsb_Save.Click += new System.EventHandler(this.toolStripButtons_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 999;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tmrLoadDeflection
            // 
            this.tmrLoadDeflection.Tick += new System.EventHandler(this.tmrLoadDeflection_Tick);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // tmr_moving_load
            // 
            this.tmr_moving_load.Tick += new System.EventHandler(this.tmr_moving_load_Tick);
            // 
            // tc5
            // 
            this.tc5.Controls.Add(this.tab5_pre_prs);
            this.tc5.Controls.Add(this.tab5_prs);
            this.tc5.Controls.Add(this.tab5_post_prs);
            this.tc5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tc5.Location = new System.Drawing.Point(0, 0);
            this.tc5.Name = "tc5";
            this.tc5.SelectedIndex = 0;
            this.tc5.Size = new System.Drawing.Size(904, 96);
            this.tc5.TabIndex = 3;
            this.tc5.SelectedIndexChanged += new System.EventHandler(this.tc5_SelectedIndexChanged);
            // 
            // tab5_pre_prs
            // 
            this.tab5_pre_prs.Controls.Add(this.tc6);
            this.tab5_pre_prs.Location = new System.Drawing.Point(4, 22);
            this.tab5_pre_prs.Name = "tab5_pre_prs";
            this.tab5_pre_prs.Padding = new System.Windows.Forms.Padding(3);
            this.tab5_pre_prs.Size = new System.Drawing.Size(896, 70);
            this.tab5_pre_prs.TabIndex = 0;
            this.tab5_pre_prs.Text = "Pre Process";
            this.tab5_pre_prs.UseVisualStyleBackColor = true;
            // 
            // tc6
            // 
            this.tc6.Controls.Add(this.tab_file_open);
            this.tc6.Controls.Add(this.tab_geometry);
            this.tc6.Controls.Add(this.tab_loading);
            this.tc6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tc6.Location = new System.Drawing.Point(3, 3);
            this.tc6.Name = "tc6";
            this.tc6.SelectedIndex = 0;
            this.tc6.Size = new System.Drawing.Size(890, 64);
            this.tc6.TabIndex = 1;
            this.tc6.SelectedIndexChanged += new System.EventHandler(this.tc6_SelectedIndexChanged);
            // 
            // tab_file_open
            // 
            this.tab_file_open.Controls.Add(this.sc4);
            this.tab_file_open.Controls.Add(this.panel4);
            this.tab_file_open.Location = new System.Drawing.Point(4, 22);
            this.tab_file_open.Name = "tab_file_open";
            this.tab_file_open.Size = new System.Drawing.Size(882, 38);
            this.tab_file_open.TabIndex = 4;
            this.tab_file_open.Text = "File";
            this.tab_file_open.UseVisualStyleBackColor = true;
            // 
            // sc4
            // 
            this.sc4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sc4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc4.Location = new System.Drawing.Point(0, 49);
            this.sc4.Name = "sc4";
            // 
            // sc4.Panel1
            // 
            this.sc4.Panel1.Controls.Add(this.rtb_input_file);
            this.sc4.Panel1.Controls.Add(this.label4);
            // 
            // sc4.Panel2
            // 
            this.sc4.Panel2.Controls.Add(this.rtb_ll_txt);
            this.sc4.Panel2.Controls.Add(this.label5);
            this.sc4.Size = new System.Drawing.Size(882, 0);
            this.sc4.SplitterDistance = 549;
            this.sc4.TabIndex = 2;
            // 
            // rtb_input_file
            // 
            this.rtb_input_file.BackColor = System.Drawing.Color.AliceBlue;
            this.rtb_input_file.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_input_file.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_input_file.Location = new System.Drawing.Point(0, 21);
            this.rtb_input_file.Name = "rtb_input_file";
            this.rtb_input_file.Size = new System.Drawing.Size(547, 0);
            this.rtb_input_file.TabIndex = 1;
            this.rtb_input_file.Text = "";
            this.rtb_input_file.WordWrap = false;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(547, 21);
            this.label4.TabIndex = 3;
            this.label4.Text = "Analysis Input Data File";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rtb_ll_txt
            // 
            this.rtb_ll_txt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.rtb_ll_txt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_ll_txt.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_ll_txt.Location = new System.Drawing.Point(0, 21);
            this.rtb_ll_txt.Name = "rtb_ll_txt";
            this.rtb_ll_txt.Size = new System.Drawing.Size(327, 0);
            this.rtb_ll_txt.TabIndex = 1;
            this.rtb_ll_txt.Text = "";
            this.rtb_ll_txt.WordWrap = false;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(327, 21);
            this.label5.TabIndex = 3;
            this.label5.Text = "Moving Load Definition File (LL.TXT)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.ts_shortcut);
            this.panel4.Controls.Add(this.menuStrip1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(882, 49);
            this.panel4.TabIndex = 3;
            // 
            // ts_shortcut
            // 
            this.ts_shortcut.Dock = System.Windows.Forms.DockStyle.None;
            this.ts_shortcut.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_open_data,
            this.tsb_save_drawing,
            this.tsb_save_data,
            this.tsb_print_drawing,
            this.tsb_close});
            this.ts_shortcut.Location = new System.Drawing.Point(127, 11);
            this.ts_shortcut.Name = "ts_shortcut";
            this.ts_shortcut.Size = new System.Drawing.Size(127, 25);
            this.ts_shortcut.TabIndex = 4;
            this.ts_shortcut.Text = "toolStrip1";
            // 
            // tsb_open_data
            // 
            this.tsb_open_data.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_open_data.Image = global::HEADSNeed.Properties.Resources.OpenFile;
            this.tsb_open_data.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_open_data.Name = "tsb_open_data";
            this.tsb_open_data.Size = new System.Drawing.Size(23, 22);
            this.tsb_open_data.Text = "Open Data File";
            this.tsb_open_data.Click += new System.EventHandler(this.tsmi_data_open_Click);
            // 
            // tsb_save_drawing
            // 
            this.tsb_save_drawing.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_save_drawing.Image = global::HEADSNeed.Properties.Resources.HdViewerApp;
            this.tsb_save_drawing.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_save_drawing.Name = "tsb_save_drawing";
            this.tsb_save_drawing.Size = new System.Drawing.Size(23, 22);
            this.tsb_save_drawing.Text = "Save Drawing File";
            this.tsb_save_drawing.Click += new System.EventHandler(this.tsmi_data_open_Click);
            // 
            // tsb_save_data
            // 
            this.tsb_save_data.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_save_data.Image = global::HEADSNeed.Properties.Resources.saveFile;
            this.tsb_save_data.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_save_data.Name = "tsb_save_data";
            this.tsb_save_data.Size = new System.Drawing.Size(23, 22);
            this.tsb_save_data.Text = "Save Data File";
            this.tsb_save_data.Click += new System.EventHandler(this.tsmi_data_open_Click);
            // 
            // tsb_print_drawing
            // 
            this.tsb_print_drawing.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_print_drawing.Image = global::HEADSNeed.Properties.Resources.printFile;
            this.tsb_print_drawing.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_print_drawing.Name = "tsb_print_drawing";
            this.tsb_print_drawing.Size = new System.Drawing.Size(23, 22);
            this.tsb_print_drawing.Text = "Print Drawing";
            this.tsb_print_drawing.Click += new System.EventHandler(this.tsmi_data_open_Click);
            // 
            // tsb_close
            // 
            this.tsb_close.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_close.Image = global::HEADSNeed.Properties.Resources.Exit;
            this.tsb_close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_close.Name = "tsb_close";
            this.tsb_close.Size = new System.Drawing.Size(23, 22);
            this.tsb_close.Text = "Close";
            this.tsb_close.Click += new System.EventHandler(this.tsmi_data_open_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_file,
            this.tsmi_edit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(43, 49);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmi_file
            // 
            this.tsmi_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_data_open,
            this.tsmi_dwg_save,
            this.tsmi_file_save,
            this.tsmi_file_print,
            this.tsmi_close});
            this.tsmi_file.Name = "tsmi_file";
            this.tsmi_file.Size = new System.Drawing.Size(30, 19);
            this.tsmi_file.Text = "File";
            // 
            // tsmi_data_open
            // 
            this.tsmi_data_open.Image = global::HEADSNeed.Properties.Resources.OpenFile;
            this.tsmi_data_open.Name = "tsmi_data_open";
            this.tsmi_data_open.Size = new System.Drawing.Size(166, 22);
            this.tsmi_data_open.Text = "Open Data File";
            this.tsmi_data_open.Click += new System.EventHandler(this.tsmi_data_open_Click);
            // 
            // tsmi_dwg_save
            // 
            this.tsmi_dwg_save.Image = global::HEADSNeed.Properties.Resources.HdViewerApp;
            this.tsmi_dwg_save.Name = "tsmi_dwg_save";
            this.tsmi_dwg_save.Size = new System.Drawing.Size(166, 22);
            this.tsmi_dwg_save.Text = "Save Drawing File";
            this.tsmi_dwg_save.Click += new System.EventHandler(this.tsmi_data_open_Click);
            // 
            // tsmi_file_save
            // 
            this.tsmi_file_save.Image = global::HEADSNeed.Properties.Resources.saveFile;
            this.tsmi_file_save.Name = "tsmi_file_save";
            this.tsmi_file_save.Size = new System.Drawing.Size(166, 22);
            this.tsmi_file_save.Text = "Save Data File";
            this.tsmi_file_save.Click += new System.EventHandler(this.tsmi_data_open_Click);
            // 
            // tsmi_file_print
            // 
            this.tsmi_file_print.Image = global::HEADSNeed.Properties.Resources.printFile;
            this.tsmi_file_print.Name = "tsmi_file_print";
            this.tsmi_file_print.Size = new System.Drawing.Size(166, 22);
            this.tsmi_file_print.Text = "Print Drawing";
            this.tsmi_file_print.Click += new System.EventHandler(this.tsmi_data_open_Click);
            // 
            // tsmi_close
            // 
            this.tsmi_close.Image = global::HEADSNeed.Properties.Resources.Exit;
            this.tsmi_close.Name = "tsmi_close";
            this.tsmi_close.Size = new System.Drawing.Size(166, 22);
            this.tsmi_close.Text = "Close";
            this.tsmi_close.Click += new System.EventHandler(this.tsmi_data_open_Click);
            // 
            // tsmi_edit
            // 
            this.tsmi_edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_astra_viewer});
            this.tsmi_edit.Name = "tsmi_edit";
            this.tsmi_edit.Size = new System.Drawing.Size(32, 19);
            this.tsmi_edit.Text = "Edit";
            this.tsmi_edit.Visible = false;
            // 
            // tsmi_astra_viewer
            // 
            this.tsmi_astra_viewer.Name = "tsmi_astra_viewer";
            this.tsmi_astra_viewer.Size = new System.Drawing.Size(148, 22);
            this.tsmi_astra_viewer.Text = "ASTRA Viewer";
            this.tsmi_astra_viewer.Click += new System.EventHandler(this.tsmi_astra_viewer_Click);
            // 
            // tab_geometry
            // 
            this.tab_geometry.Location = new System.Drawing.Point(4, 22);
            this.tab_geometry.Name = "tab_geometry";
            this.tab_geometry.Padding = new System.Windows.Forms.Padding(3);
            this.tab_geometry.Size = new System.Drawing.Size(882, 38);
            this.tab_geometry.TabIndex = 0;
            this.tab_geometry.Text = "Geometry";
            this.tab_geometry.UseVisualStyleBackColor = true;
            // 
            // tab_loading
            // 
            this.tab_loading.Controls.Add(this.chkJoint);
            this.tab_loading.Controls.Add(this.chkLocal);
            this.tab_loading.Controls.Add(this.chkGlobal);
            this.tab_loading.Location = new System.Drawing.Point(4, 22);
            this.tab_loading.Name = "tab_loading";
            this.tab_loading.Size = new System.Drawing.Size(882, 38);
            this.tab_loading.TabIndex = 3;
            this.tab_loading.Text = "Loadings";
            this.tab_loading.UseVisualStyleBackColor = true;
            // 
            // tab5_prs
            // 
            this.tab5_prs.Controls.Add(this.rtb_ana_rep);
            this.tab5_prs.Controls.Add(this.panel2);
            this.tab5_prs.Location = new System.Drawing.Point(4, 22);
            this.tab5_prs.Name = "tab5_prs";
            this.tab5_prs.Size = new System.Drawing.Size(896, 70);
            this.tab5_prs.TabIndex = 2;
            this.tab5_prs.Text = "Process";
            this.tab5_prs.UseVisualStyleBackColor = true;
            // 
            // rtb_ana_rep
            // 
            this.rtb_ana_rep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_ana_rep.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_ana_rep.Location = new System.Drawing.Point(0, 53);
            this.rtb_ana_rep.Name = "rtb_ana_rep";
            this.rtb_ana_rep.Size = new System.Drawing.Size(896, 17);
            this.rtb_ana_rep.TabIndex = 3;
            this.rtb_ana_rep.Text = "";
            this.rtb_ana_rep.WordWrap = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbl_process_caption);
            this.panel2.Controls.Add(this.btn_process_analysis);
            this.panel2.Controls.Add(this.btn_open_ana_rep);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(896, 53);
            this.panel2.TabIndex = 2;
            // 
            // lbl_process_caption
            // 
            this.lbl_process_caption.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_process_caption.Location = new System.Drawing.Point(0, 0);
            this.lbl_process_caption.Name = "lbl_process_caption";
            this.lbl_process_caption.Size = new System.Drawing.Size(896, 21);
            this.lbl_process_caption.TabIndex = 2;
            this.lbl_process_caption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tab5_post_prs
            // 
            this.tab5_post_prs.Controls.Add(this.tc1);
            this.tab5_post_prs.Location = new System.Drawing.Point(4, 22);
            this.tab5_post_prs.Name = "tab5_post_prs";
            this.tab5_post_prs.Padding = new System.Windows.Forms.Padding(3);
            this.tab5_post_prs.Size = new System.Drawing.Size(896, 70);
            this.tab5_post_prs.TabIndex = 1;
            this.tab5_post_prs.Text = "Post Process";
            this.tab5_post_prs.UseVisualStyleBackColor = true;
            // 
            // sc3
            // 
            this.sc3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sc3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sc3.Location = new System.Drawing.Point(0, 0);
            this.sc3.Name = "sc3";
            this.sc3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // sc3.Panel1
            // 
            this.sc3.Panel1.Controls.Add(this.tc5);
            // 
            // sc3.Panel2
            // 
            this.sc3.Panel2.Controls.Add(this.sc1);
            this.sc3.Size = new System.Drawing.Size(906, 616);
            this.sc3.SplitterDistance = 98;
            this.sc3.TabIndex = 4;
            // 
            // frm_ASTRA_Analysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 660);
            this.Controls.Add(this.sc3);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_ASTRA_Analysis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Analysis Process & Results";
            this.Load += new System.EventHandler(this.frm_ASTRA_Analysis_Load);
            this.SizeChanged += new System.EventHandler(this.frm_ASTRA_Analysis_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tc1.ResumeLayout(false);
            this.tab_load_deflection.ResumeLayout(false);
            this.tab_load_deflection.PerformLayout();
            this.grb_Auto.ResumeLayout(false);
            this.grb_Auto.PerformLayout();
            this.grb_manual.ResumeLayout(false);
            this.tab_max_force.ResumeLayout(false);
            this.tab_max_force.PerformLayout();
            this.tab_moving_load.ResumeLayout(false);
            this.grb_moving_controls.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.tab_envelop.ResumeLayout(false);
            this.tab_envelop.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.sc1.Panel1.ResumeLayout(false);
            this.sc1.Panel2.ResumeLayout(false);
            this.sc1.Panel2.PerformLayout();
            this.sc1.ResumeLayout(false);
            this.tc_pp_panel.ResumeLayout(false);
            this.tab1_geometry.ResumeLayout(false);
            this.tc3.ResumeLayout(false);
            this.tab2_joints.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_joints)).EndInit();
            this.tab2_members.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMemberGrid)).EndInit();
            this.tab2_elements.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvElementGrid)).EndInit();
            this.tab1_loading.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tab1_forces.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tab_3_joint_frc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_joint_frcs)).EndInit();
            this.tab_3_mem_frc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_beam_frcs)).EndInit();
            this.tab_3_truss_frc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_truss_frcs)).EndInit();
            this.tab1_load_deflection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_node_disp)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tab1_max_force.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_max_frc)).EndInit();
            this.tab1_moving_load.ResumeLayout(false);
            this.sc2.Panel1.ResumeLayout(false);
            this.sc2.Panel2.ResumeLayout(false);
            this.sc2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.tc4.ResumeLayout(false);
            this.tab_org_doc.ResumeLayout(false);
            this.tab_defl_doc.ResumeLayout(false);
            this.tab_max_doc.ResumeLayout(false);
            this.tab_mov_doc.ResumeLayout(false);
            this.tab_evlp_doc.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.pnl_progress.ResumeLayout(false);
            this.pnl_progress.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tc5.ResumeLayout(false);
            this.tab5_pre_prs.ResumeLayout(false);
            this.tc6.ResumeLayout(false);
            this.tab_file_open.ResumeLayout(false);
            this.sc4.Panel1.ResumeLayout(false);
            this.sc4.Panel2.ResumeLayout(false);
            this.sc4.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ts_shortcut.ResumeLayout(false);
            this.ts_shortcut.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tab_loading.ResumeLayout(false);
            this.tab_loading.PerformLayout();
            this.tab5_prs.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tab5_post_prs.ResumeLayout(false);
            this.sc3.Panel1.ResumeLayout(false);
            this.sc3.Panel2.ResumeLayout(false);
            this.sc3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tc1;
        private System.Windows.Forms.SplitContainer sc1;
        private System.Windows.Forms.TabPage tab_forces;
        private System.Windows.Forms.TabPage tab_load_deflection;
        private System.Windows.Forms.TabPage tab_max_force;
        private System.Windows.Forms.TabPage tab_moving_load;
        private System.Windows.Forms.Button btn_process_analysis;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tsb_3D_rotate;
        private System.Windows.Forms.ToolStripButton tsb_VTop;
        private System.Windows.Forms.ToolStripButton tsb_VBot;
        private System.Windows.Forms.ToolStripButton tsb_VLeft;
        private System.Windows.Forms.ToolStripButton tsb_VRight;
        private vdScrollableControl.vdScrollableControl vdScrollableControl1;
        private System.Windows.Forms.ToolStripButton tsb_VFront;
        private System.Windows.Forms.ToolStripButton tsb_VBack;
        private System.Windows.Forms.ToolStripButton tsb_VNE;
        private System.Windows.Forms.ToolStripButton tsb_VNW;
        private System.Windows.Forms.ToolStripButton tsb_VSE;
        private System.Windows.Forms.ToolStripButton tsb_VSW;
        private System.Windows.Forms.ToolStripSeparator tss1;
        private System.Windows.Forms.ToolStripButton tsb_ZoomA;
        private System.Windows.Forms.ToolStripButton tsb_ZoomE;
        private System.Windows.Forms.ToolStripButton tsb_ZoomP;
        private System.Windows.Forms.ToolStripButton tsb_ZoomW;
        private System.Windows.Forms.ToolStripButton tsb_ZoomIn;
        private System.Windows.Forms.ToolStripButton tsb_ZoomOut;
        private System.Windows.Forms.ToolStripButton tsb_Pan;
        private System.Windows.Forms.TabControl tc_pp_panel;
        private System.Windows.Forms.TabPage tab1_geometry;
        private System.Windows.Forms.TabPage tab1_loading;
        private System.Windows.Forms.TabControl tc3;
        private System.Windows.Forms.TabPage tab2_joints;
        private System.Windows.Forms.TabPage tab2_members;
        private System.Windows.Forms.DataGridView dgv_joints;
        private System.Windows.Forms.DataGridView dgvMemberGrid;
        private System.Windows.Forms.Button btn_open_ana_rep;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.CheckBox chk_suprts;
        private System.Windows.Forms.CheckBox chk_mems;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_text_size;
        private System.Windows.Forms.Panel pnl_progress;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TreeView tv_loads;
        private System.Windows.Forms.CheckBox chkJoint;
        private System.Windows.Forms.CheckBox chkLocal;
        private System.Windows.Forms.CheckBox chkGlobal;
        private System.Windows.Forms.TabPage tab1_forces;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_3_joint_frc;
        private System.Windows.Forms.TabPage tab_3_mem_frc;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridView dgv_beam_frcs;
        private System.Windows.Forms.TabPage tab_3_truss_frc;
        private System.Windows.Forms.DataGridView dgv_truss_frcs;
        private System.Windows.Forms.TabPage tab2_elements;
        private System.Windows.Forms.DataGridView dgvElementGrid;
        private System.Windows.Forms.GroupBox grb_Auto;
        public System.Windows.Forms.ComboBox cmbInterval;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnAutoNext;
        private System.Windows.Forms.Button btnAutoPrev;
        private System.Windows.Forms.GroupBox grb_manual;
        private System.Windows.Forms.Label lblLoadCase;
        public System.Windows.Forms.ComboBox cmbLoadCase;
        private System.Windows.Forms.TabPage tab1_load_deflection;
        private System.Windows.Forms.DataGridView dgv_node_disp;
        private System.Windows.Forms.Timer tmrLoadDeflection;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox txtDefFactor;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Label lblFactor;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.ProgressBar pbLoadDeflection;
        private System.Windows.Forms.Label lblPleaseWait;
        private vdScrollableControl.vdScrollableControl vdScrollableControl2;
        private System.Windows.Forms.TabControl tc4;
        private System.Windows.Forms.TabPage tab_org_doc;
        private System.Windows.Forms.TabPage tab_defl_doc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridView dgv_joint_frcs;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn27;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn28;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn29;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn30;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn31;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.TabPage tab1_max_force;
        private System.Windows.Forms.TabPage tab1_moving_load;
        private System.Windows.Forms.DataGridView dgv_max_frc;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_jno;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_x;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_y;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_z;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_support;
        private System.Windows.Forms.GroupBox grb_moving_controls;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lbl_distance;
        private System.Windows.Forms.TextBox txt_dist;
        public System.Windows.Forms.ComboBox cmb_mov_loadcase;
        private System.Windows.Forms.GroupBox groupBox6;
        public System.Windows.Forms.ComboBox cmb_mov_time;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_mov_stop;
        private System.Windows.Forms.Button btn_mov_run;
        private System.Windows.Forms.Button btn_mov_pause;
        private System.Windows.Forms.Timer tmr_moving_load;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.SplitContainer sc2;
        private System.Windows.Forms.TreeView tv_ml_loads;
        private System.Windows.Forms.TreeView tv_ml_def;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chk_elems;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgTxtMemberNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgTxtMember;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgTxtNode1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgTxtNode2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgTxtYD;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgTxtZD;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgTxtArea;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgTxtIX;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgTxtIY;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgTxtIZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgTxtE;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgTxtDEN;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgTxtPR;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgTxtMAT;
        private System.Windows.Forms.TabControl tc5;
        private System.Windows.Forms.TabPage tab5_pre_prs;
        private System.Windows.Forms.TabPage tab5_prs;
        private System.Windows.Forms.TabPage tab5_post_prs;
        private System.Windows.Forms.TabControl tc6;
        private System.Windows.Forms.TabPage tab_geometry;
        private System.Windows.Forms.TabPage tab_loading;
        private System.Windows.Forms.SplitContainer sc3;
        private System.Windows.Forms.CheckBox chk_joints;
        private System.Windows.Forms.TabPage tab_file_open;
        private System.Windows.Forms.RichTextBox rtb_input_file;
        private System.Windows.Forms.RichTextBox rtb_ana_rep;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SplitContainer sc4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox rtb_ll_txt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txt_max_deflection_load;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_max_deflection_node;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_max_deflection;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripButton tsb_Save;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.ComboBox cmbLengthUnit;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.ComboBox cmb_structure_type;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbForceUnit;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtUserTitle;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsb_ShadeOn;
        private System.Windows.Forms.ToolStripButton tsb_Wire;
        private System.Windows.Forms.TabPage tab_evlp_doc;
        private vdScrollableControl.vdScrollableControl vdScrollableControl3;
        private System.Windows.Forms.TabPage tab_envelop;
        private System.Windows.Forms.TabPage tab1_truss_env;
        private System.Windows.Forms.TextBox txt_env_mnos;
        private System.Windows.Forms.Button btn_env_show;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbtn_env_bottom;
        private System.Windows.Forms.RadioButton rbtn_env_top;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_file;
        private System.Windows.Forms.ToolStripMenuItem tsmi_data_open;
        private System.Windows.Forms.ToolStripMenuItem tsmi_file_save;
        private System.Windows.Forms.ToolStripMenuItem tsmi_file_print;
        private System.Windows.Forms.ToolStripMenuItem tsmi_close;
        private System.Windows.Forms.ToolStripMenuItem tsmi_dwg_save;
        private System.Windows.Forms.ToolStrip ts_shortcut;
        private System.Windows.Forms.ToolStripButton tsb_open_data;
        private System.Windows.Forms.ToolStripButton tsb_save_drawing;
        private System.Windows.Forms.ToolStripButton tsb_save_data;
        private System.Windows.Forms.ToolStripButton tsb_print_drawing;
        private System.Windows.Forms.ToolStripButton tsb_close;
        private System.Windows.Forms.TabPage tab_mov_doc;
        private System.Windows.Forms.TabPage tab_max_doc;
        private vdScrollableControl.vdScrollableControl vdScrollableControl4;
        private vdScrollableControl.vdScrollableControl vdScrollableControl5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn32;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_cmf;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_tf;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_af;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_tor;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_bm;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_sf;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.ToolStripMenuItem tsmi_edit;
        private System.Windows.Forms.ToolStripMenuItem tsmi_astra_viewer;
        private System.Windows.Forms.Label lbl_process_caption;
    }
}