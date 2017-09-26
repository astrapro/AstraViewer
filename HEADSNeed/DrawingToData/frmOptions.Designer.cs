namespace HEADSNeed.DrawingToData
{
    partial class frmOptions
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
            this.cmb_draw = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_add_to_list = new System.Windows.Forms.Button();
            this.btn_proceed = new System.Windows.Forms.Button();
            this.btn_finish = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_layer = new System.Windows.Forms.ComboBox();
            this.txt_drawing_lib = new System.Windows.Forms.TextBox();
            this.btn_browse_lib = new System.Windows.Forms.Button();
            this.grb_from_file = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_select_settings = new System.Windows.Forms.TextBox();
            this.btn_browse_settings = new System.Windows.Forms.Button();
            this.lbl_text = new System.Windows.Forms.Label();
            this.btn_save_settings = new System.Windows.Forms.Button();
            this.dgv_all_data = new System.Windows.Forms.DataGridView();
            this.colDraw = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.col_label = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_draw_el = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_layer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btn_delete_rows = new System.Windows.Forms.Button();
            this.chk_Draw_All = new System.Windows.Forms.CheckBox();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.grb_data_to_poly = new System.Windows.Forms.GroupBox();
            this.lbl_moredata = new System.Windows.Forms.Label();
            this.btn_browse = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_select_file = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.grb_from_file.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_all_data)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.grb_data_to_poly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmb_draw
            // 
            this.cmb_draw.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_draw.FormattingEnabled = true;
            this.cmb_draw.Items.AddRange(new object[] {
            "POLYLINE",
            "POINT",
            "TEXT"});
            this.cmb_draw.Location = new System.Drawing.Point(297, 122);
            this.cmb_draw.Name = "cmb_draw";
            this.cmb_draw.Size = new System.Drawing.Size(198, 21);
            this.cmb_draw.TabIndex = 3;
            this.cmb_draw.SelectedIndexChanged += new System.EventHandler(this.cmb_draw_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(244, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Draw As";
            // 
            // btn_add_to_list
            // 
            this.btn_add_to_list.Location = new System.Drawing.Point(501, 120);
            this.btn_add_to_list.Name = "btn_add_to_list";
            this.btn_add_to_list.Size = new System.Drawing.Size(92, 23);
            this.btn_add_to_list.TabIndex = 5;
            this.btn_add_to_list.Text = "Add to List";
            this.btn_add_to_list.UseVisualStyleBackColor = true;
            this.btn_add_to_list.Click += new System.EventHandler(this.btn_Add_to_list_Click);
            // 
            // btn_proceed
            // 
            this.btn_proceed.Location = new System.Drawing.Point(275, 428);
            this.btn_proceed.Name = "btn_proceed";
            this.btn_proceed.Size = new System.Drawing.Size(92, 23);
            this.btn_proceed.TabIndex = 7;
            this.btn_proceed.Text = "Draw";
            this.btn_proceed.UseVisualStyleBackColor = true;
            this.btn_proceed.Click += new System.EventHandler(this.btn_proceed_Click);
            // 
            // btn_finish
            // 
            this.btn_finish.Location = new System.Drawing.Point(514, 428);
            this.btn_finish.Name = "btn_finish";
            this.btn_finish.Size = new System.Drawing.Size(77, 23);
            this.btn_finish.TabIndex = 8;
            this.btn_finish.Text = "Finish";
            this.btn_finish.UseVisualStyleBackColor = true;
            this.btn_finish.Click += new System.EventHandler(this.btn_finish_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Select Layer";
            // 
            // cmb_layer
            // 
            this.cmb_layer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_layer.FormattingEnabled = true;
            this.cmb_layer.Items.AddRange(new object[] {
            "POINT",
            "POLYLINE",
            "TEXT"});
            this.cmb_layer.Location = new System.Drawing.Point(89, 122);
            this.cmb_layer.Name = "cmb_layer";
            this.cmb_layer.Size = new System.Drawing.Size(149, 21);
            this.cmb_layer.TabIndex = 9;
            // 
            // txt_drawing_lib
            // 
            this.txt_drawing_lib.Location = new System.Drawing.Point(5, 32);
            this.txt_drawing_lib.Name = "txt_drawing_lib";
            this.txt_drawing_lib.Size = new System.Drawing.Size(543, 22);
            this.txt_drawing_lib.TabIndex = 12;
            // 
            // btn_browse_lib
            // 
            this.btn_browse_lib.Location = new System.Drawing.Point(556, 32);
            this.btn_browse_lib.Name = "btn_browse_lib";
            this.btn_browse_lib.Size = new System.Drawing.Size(33, 21);
            this.btn_browse_lib.TabIndex = 13;
            this.btn_browse_lib.Text = "...";
            this.btn_browse_lib.UseVisualStyleBackColor = true;
            this.btn_browse_lib.Click += new System.EventHandler(this.btn_browse_lib_Click);
            // 
            // grb_from_file
            // 
            this.grb_from_file.Controls.Add(this.label3);
            this.grb_from_file.Controls.Add(this.txt_select_settings);
            this.grb_from_file.Controls.Add(this.btn_browse_settings);
            this.grb_from_file.Controls.Add(this.lbl_text);
            this.grb_from_file.Controls.Add(this.txt_drawing_lib);
            this.grb_from_file.Controls.Add(this.btn_browse_lib);
            this.grb_from_file.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_from_file.ForeColor = System.Drawing.Color.Red;
            this.grb_from_file.Location = new System.Drawing.Point(8, 6);
            this.grb_from_file.Name = "grb_from_file";
            this.grb_from_file.Size = new System.Drawing.Size(591, 110);
            this.grb_from_file.TabIndex = 14;
            this.grb_from_file.TabStop = false;
            this.grb_from_file.Text = "Optional";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(4, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(215, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Select User\'s Settings File (Optional)";
            // 
            // txt_select_settings
            // 
            this.txt_select_settings.Location = new System.Drawing.Point(7, 82);
            this.txt_select_settings.Name = "txt_select_settings";
            this.txt_select_settings.Size = new System.Drawing.Size(543, 22);
            this.txt_select_settings.TabIndex = 21;
            // 
            // btn_browse_settings
            // 
            this.btn_browse_settings.Location = new System.Drawing.Point(556, 81);
            this.btn_browse_settings.Name = "btn_browse_settings";
            this.btn_browse_settings.Size = new System.Drawing.Size(33, 22);
            this.btn_browse_settings.TabIndex = 22;
            this.btn_browse_settings.Text = "...";
            this.btn_browse_settings.UseVisualStyleBackColor = true;
            this.btn_browse_settings.Click += new System.EventHandler(this.btn_browse_settings_Click);
            // 
            // lbl_text
            // 
            this.lbl_text.AutoSize = true;
            this.lbl_text.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_text.ForeColor = System.Drawing.Color.Black;
            this.lbl_text.Location = new System.Drawing.Point(4, 16);
            this.lbl_text.Name = "lbl_text";
            this.lbl_text.Size = new System.Drawing.Size(262, 13);
            this.lbl_text.TabIndex = 19;
            this.lbl_text.Text = "Select Folder for Drawing from Block Library";
            // 
            // btn_save_settings
            // 
            this.btn_save_settings.Location = new System.Drawing.Point(373, 428);
            this.btn_save_settings.Name = "btn_save_settings";
            this.btn_save_settings.Size = new System.Drawing.Size(135, 23);
            this.btn_save_settings.TabIndex = 16;
            this.btn_save_settings.Text = "Save Settings in a File";
            this.btn_save_settings.UseVisualStyleBackColor = true;
            this.btn_save_settings.Click += new System.EventHandler(this.btn_save_settings_Click);
            // 
            // dgv_all_data
            // 
            this.dgv_all_data.AllowUserToAddRows = false;
            this.dgv_all_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_all_data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDraw,
            this.col_label,
            this.col_draw_el,
            this.col_layer});
            this.dgv_all_data.Location = new System.Drawing.Point(11, 149);
            this.dgv_all_data.Name = "dgv_all_data";
            this.dgv_all_data.RowHeadersWidth = 27;
            this.dgv_all_data.Size = new System.Drawing.Size(580, 262);
            this.dgv_all_data.TabIndex = 18;
            // 
            // colDraw
            // 
            this.colDraw.HeaderText = "Draw";
            this.colDraw.Name = "colDraw";
            this.colDraw.Width = 60;
            // 
            // col_label
            // 
            this.col_label.HeaderText = "LABEL Name";
            this.col_label.Name = "col_label";
            this.col_label.Width = 150;
            // 
            // col_draw_el
            // 
            this.col_draw_el.HeaderText = "Drawing Element Name";
            this.col_draw_el.Name = "col_draw_el";
            this.col_draw_el.Width = 210;
            // 
            // col_layer
            // 
            this.col_layer.HeaderText = "LAYER Name";
            this.col_layer.Name = "col_layer";
            // 
            // ofd
            // 
            this.ofd.Filter = "User\'s Settings File(*.txt)|*.txt";
            // 
            // btn_delete_rows
            // 
            this.btn_delete_rows.Location = new System.Drawing.Point(148, 428);
            this.btn_delete_rows.Name = "btn_delete_rows";
            this.btn_delete_rows.Size = new System.Drawing.Size(79, 23);
            this.btn_delete_rows.TabIndex = 19;
            this.btn_delete_rows.Text = "Delete Row";
            this.btn_delete_rows.UseVisualStyleBackColor = true;
            this.btn_delete_rows.Click += new System.EventHandler(this.btn_delete_rows_Click);
            // 
            // chk_Draw_All
            // 
            this.chk_Draw_All.AutoSize = true;
            this.chk_Draw_All.Checked = true;
            this.chk_Draw_All.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Draw_All.Location = new System.Drawing.Point(18, 434);
            this.chk_Draw_All.Name = "chk_Draw_All";
            this.chk_Draw_All.Size = new System.Drawing.Size(65, 17);
            this.chk_Draw_All.TabIndex = 20;
            this.chk_Draw_All.Text = "Draw All";
            this.chk_Draw_All.UseVisualStyleBackColor = true;
            this.chk_Draw_All.CheckedChanged += new System.EventHandler(this.chk_Draw_All_CheckedChanged);
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_Refresh.Location = new System.Drawing.Point(89, 428);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(53, 23);
            this.btn_Refresh.TabIndex = 21;
            this.btn_Refresh.Text = "Refresh";
            this.btn_Refresh.UseVisualStyleBackColor = true;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(677, 491);
            this.tabControl1.TabIndex = 22;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.grb_data_to_poly);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(669, 465);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Select File";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // grb_data_to_poly
            // 
            this.grb_data_to_poly.Controls.Add(this.lbl_moredata);
            this.grb_data_to_poly.Controls.Add(this.btn_browse);
            this.grb_data_to_poly.Controls.Add(this.dataGridView1);
            this.grb_data_to_poly.Controls.Add(this.dataGridView2);
            this.grb_data_to_poly.Controls.Add(this.txt_select_file);
            this.grb_data_to_poly.Location = new System.Drawing.Point(8, 6);
            this.grb_data_to_poly.Name = "grb_data_to_poly";
            this.grb_data_to_poly.Size = new System.Drawing.Size(654, 442);
            this.grb_data_to_poly.TabIndex = 23;
            this.grb_data_to_poly.TabStop = false;
            this.grb_data_to_poly.Text = "Select File Name :";
            // 
            // lbl_moredata
            // 
            this.lbl_moredata.BackColor = System.Drawing.Color.Red;
            this.lbl_moredata.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_moredata.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbl_moredata.Location = new System.Drawing.Point(7, 376);
            this.lbl_moredata.Name = "lbl_moredata";
            this.lbl_moredata.Size = new System.Drawing.Size(629, 23);
            this.lbl_moredata.TabIndex = 21;
            this.lbl_moredata.Text = "There is more data present in the file.";
            this.lbl_moredata.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_moredata.Visible = false;
            // 
            // btn_browse
            // 
            this.btn_browse.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_browse.Location = new System.Drawing.Point(612, 15);
            this.btn_browse.Name = "btn_browse";
            this.btn_browse.Size = new System.Drawing.Size(33, 23);
            this.btn_browse.TabIndex = 1;
            this.btn_browse.Text = "....";
            this.btn_browse.UseVisualStyleBackColor = true;
            this.btn_browse.Click += new System.EventHandler(this.btn_browse_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10});
            this.dataGridView1.Location = new System.Drawing.Point(6, 41);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView1.Size = new System.Drawing.Size(639, 45);
            this.dataGridView1.TabIndex = 19;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Items.AddRange(new object[] {
            "Point Nos.",
            "Chainage",
            "X",
            "Y",
            "Z",
            "Label",
            "Segment Lengths",
            "Layer",
            "Height",
            "None (Default)"});
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Items.AddRange(new object[] {
            "Point Nos.",
            "Chainage",
            "X",
            "Y",
            "Z",
            "Label",
            "Segment Lengths",
            "Layer",
            "Height",
            "None (Default)"});
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Items.AddRange(new object[] {
            "Point Nos.",
            "Chainage",
            "X",
            "Y",
            "Z",
            "Label",
            "Segment Lengths",
            "Layer",
            "Height",
            "None (Default)"});
            this.Column3.Name = "Column3";
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.Items.AddRange(new object[] {
            "Point Nos.",
            "Chainage",
            "X",
            "Y",
            "Z",
            "Label",
            "Segment Lengths",
            "Layer",
            "Height",
            "None (Default)"});
            this.Column4.Name = "Column4";
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Column5";
            this.Column5.Items.AddRange(new object[] {
            "Point Nos.",
            "Chainage",
            "X",
            "Y",
            "Z",
            "Label",
            "Segment Lengths",
            "Layer",
            "Height",
            "None (Default)"});
            this.Column5.Name = "Column5";
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Column6";
            this.Column6.Items.AddRange(new object[] {
            "Point Nos.",
            "Chainage",
            "X",
            "Y",
            "Z",
            "Label",
            "Segment Lengths",
            "Layer",
            "Height",
            "None (Default)"});
            this.Column6.Name = "Column6";
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Column7";
            this.Column7.Items.AddRange(new object[] {
            "Point Nos.",
            "Chainage",
            "X",
            "Y",
            "Z",
            "Label",
            "Segment Lengths",
            "Layer",
            "Height",
            "None (Default)"});
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Column8";
            this.Column8.Items.AddRange(new object[] {
            "Point Nos.",
            "Chainage",
            "X",
            "Y",
            "Z",
            "Label",
            "Segment Lengths",
            "Layer",
            "Height",
            "None (Default)"});
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Column9";
            this.Column9.Items.AddRange(new object[] {
            "Point Nos.",
            "Chainage",
            "X",
            "Y",
            "Z",
            "Label",
            "Segment Lengths",
            "Layer",
            "Height",
            "None (Default)"});
            this.Column9.Name = "Column9";
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Column10";
            this.Column10.Items.AddRange(new object[] {
            "Point Nos.",
            "Chainage",
            "X",
            "Y",
            "Z",
            "Label",
            "Segment Lengths",
            "Layer",
            "Height",
            "None (Default)"});
            this.Column10.Name = "Column10";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.ColumnHeadersVisible = false;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column14,
            this.Column15,
            this.Column16,
            this.Column17,
            this.Column18,
            this.Column19,
            this.Column20});
            this.dataGridView2.Location = new System.Drawing.Point(6, 83);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(639, 353);
            this.dataGridView2.TabIndex = 20;
            this.dataGridView2.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridView2_Scroll);
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Column11";
            this.Column11.MaxInputLength = 2147483647;
            this.Column11.Name = "Column11";
            // 
            // Column12
            // 
            this.Column12.HeaderText = "Column12";
            this.Column12.MaxInputLength = 2147483647;
            this.Column12.Name = "Column12";
            // 
            // Column13
            // 
            this.Column13.HeaderText = "Column13";
            this.Column13.MaxInputLength = 2147483647;
            this.Column13.Name = "Column13";
            // 
            // Column14
            // 
            this.Column14.HeaderText = "Column14";
            this.Column14.MaxInputLength = 2147483647;
            this.Column14.Name = "Column14";
            // 
            // Column15
            // 
            this.Column15.HeaderText = "Column15";
            this.Column15.MaxInputLength = 2147483647;
            this.Column15.Name = "Column15";
            // 
            // Column16
            // 
            this.Column16.HeaderText = "Column16";
            this.Column16.MaxInputLength = 2147483647;
            this.Column16.Name = "Column16";
            // 
            // Column17
            // 
            this.Column17.HeaderText = "Column17";
            this.Column17.Name = "Column17";
            // 
            // Column18
            // 
            this.Column18.HeaderText = "Column18";
            this.Column18.Name = "Column18";
            // 
            // Column19
            // 
            this.Column19.HeaderText = "Column19";
            this.Column19.Name = "Column19";
            // 
            // Column20
            // 
            this.Column20.HeaderText = "Column20";
            this.Column20.Name = "Column20";
            // 
            // txt_select_file
            // 
            this.txt_select_file.Location = new System.Drawing.Point(10, 18);
            this.txt_select_file.Name = "txt_select_file";
            this.txt_select_file.Size = new System.Drawing.Size(596, 20);
            this.txt_select_file.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.grb_from_file);
            this.tabPage2.Controls.Add(this.cmb_layer);
            this.tabPage2.Controls.Add(this.btn_proceed);
            this.tabPage2.Controls.Add(this.btn_Refresh);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.cmb_draw);
            this.tabPage2.Controls.Add(this.btn_add_to_list);
            this.tabPage2.Controls.Add(this.dgv_all_data);
            this.tabPage2.Controls.Add(this.btn_delete_rows);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.btn_finish);
            this.tabPage2.Controls.Add(this.btn_save_settings);
            this.tabPage2.Controls.Add(this.chk_Draw_All);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(669, 465);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Draw";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Enter += new System.EventHandler(this.tabPage2_Enter);
            // 
            // frmOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 491);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "frmOptions";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Making Survey Base Plan from Text File";
            this.Load += new System.EventHandler(this.frmOptions_Load);
            this.grb_from_file.ResumeLayout(false);
            this.grb_from_file.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_all_data)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.grb_data_to_poly.ResumeLayout(false);
            this.grb_data_to_poly.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_draw;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_add_to_list;
        private System.Windows.Forms.Button btn_proceed;
        private System.Windows.Forms.Button btn_finish;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_layer;
        private System.Windows.Forms.TextBox txt_drawing_lib;
        private System.Windows.Forms.Button btn_browse_lib;
        private System.Windows.Forms.Button btn_save_settings;
        private System.Windows.Forms.Label lbl_text;
        private System.Windows.Forms.DataGridView dgv_all_data;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_select_settings;
        private System.Windows.Forms.Button btn_browse_settings;
        private System.Windows.Forms.GroupBox grb_from_file;
        private System.Windows.Forms.Button btn_delete_rows;
        private System.Windows.Forms.CheckBox chk_Draw_All;
        private System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colDraw;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_label;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_draw_el;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_layer;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox grb_data_to_poly;
        private System.Windows.Forms.Label lbl_moredata;
        private System.Windows.Forms.Button btn_browse;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column1;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column2;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column3;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column4;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column5;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column6;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column7;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column8;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column9;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column10;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column18;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column19;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column20;
        private System.Windows.Forms.TextBox txt_select_file;
    }
}