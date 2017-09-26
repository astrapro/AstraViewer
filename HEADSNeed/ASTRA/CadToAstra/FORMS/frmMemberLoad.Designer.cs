namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    partial class frmMemberLoad
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_mem_load = new System.Windows.Forms.TabPage();
            this.btn_close = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_mnos_uni = new System.Windows.Forms.TextBox();
            this.btn_mload_add = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_mload_d1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_mload_d2 = new System.Windows.Forms.TextBox();
            this.txt_mload_val = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmb_mload_type = new System.Windows.Forms.ComboBox();
            this.cmb_mload_dir = new System.Windows.Forms.ComboBox();
            this.tab_LIN = new System.Windows.Forms.TabPage();
            this.btn_lin_close = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_mnos_lin = new System.Windows.Forms.TextBox();
            this.btn_lload_add = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_lload_end = new System.Windows.Forms.TextBox();
            this.txt_lload_start = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmb_lload_dir = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtn_UNI = new System.Windows.Forms.RadioButton();
            this.rbtn_UMOM = new System.Windows.Forms.RadioButton();
            this.rbtn_CMOM = new System.Windows.Forms.RadioButton();
            this.rbtn_CON = new System.Windows.Forms.RadioButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.tab_mem_load.SuspendLayout();
            this.tab_LIN.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab_mem_load);
            this.tabControl1.Controls.Add(this.tab_LIN);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(509, 442);
            this.tabControl1.TabIndex = 31;
            // 
            // tab_mem_load
            // 
            this.tab_mem_load.Controls.Add(this.pictureBox2);
            this.tab_mem_load.Controls.Add(this.groupBox1);
            this.tab_mem_load.Controls.Add(this.btn_close);
            this.tab_mem_load.Controls.Add(this.label9);
            this.tab_mem_load.Controls.Add(this.txt_mnos_uni);
            this.tab_mem_load.Controls.Add(this.btn_mload_add);
            this.tab_mem_load.Controls.Add(this.label10);
            this.tab_mem_load.Controls.Add(this.txt_mload_d1);
            this.tab_mem_load.Controls.Add(this.label7);
            this.tab_mem_load.Controls.Add(this.txt_mload_d2);
            this.tab_mem_load.Controls.Add(this.txt_mload_val);
            this.tab_mem_load.Controls.Add(this.label2);
            this.tab_mem_load.Controls.Add(this.label1);
            this.tab_mem_load.Controls.Add(this.label6);
            this.tab_mem_load.Controls.Add(this.cmb_mload_dir);
            this.tab_mem_load.Location = new System.Drawing.Point(4, 23);
            this.tab_mem_load.Name = "tab_mem_load";
            this.tab_mem_load.Padding = new System.Windows.Forms.Padding(3);
            this.tab_mem_load.Size = new System.Drawing.Size(501, 415);
            this.tab_mem_load.TabIndex = 0;
            this.tab_mem_load.Text = "Member Load";
            this.tab_mem_load.UseVisualStyleBackColor = true;
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(250, 238);
            this.btn_close.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(125, 25);
            this.btn_close.TabIndex = 48;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(143, 10);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(246, 14);
            this.label9.TabIndex = 47;
            this.label9.Text = "Seperated by comma (\',\') or space (\' \')";
            // 
            // txt_mnos_uni
            // 
            this.txt_mnos_uni.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_mnos_uni.Location = new System.Drawing.Point(144, 27);
            this.txt_mnos_uni.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_mnos_uni.Name = "txt_mnos_uni";
            this.txt_mnos_uni.Size = new System.Drawing.Size(344, 22);
            this.txt_mnos_uni.TabIndex = 31;
            this.txt_mnos_uni.Text = "1";
            // 
            // btn_mload_add
            // 
            this.btn_mload_add.Location = new System.Drawing.Point(125, 238);
            this.btn_mload_add.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_mload_add.Name = "btn_mload_add";
            this.btn_mload_add.Size = new System.Drawing.Size(116, 25);
            this.btn_mload_add.TabIndex = 25;
            this.btn_mload_add.Text = "Add Load";
            this.btn_mload_add.UseVisualStyleBackColor = true;
            this.btn_mload_add.Click += new System.EventHandler(this.btn_mload_add_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 30);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(117, 14);
            this.label10.TabIndex = 28;
            this.label10.Text = "Member Numbers";
            // 
            // txt_mload_d1
            // 
            this.txt_mload_d1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_mload_d1.Location = new System.Drawing.Point(413, 172);
            this.txt_mload_d1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_mload_d1.Name = "txt_mload_d1";
            this.txt_mload_d1.Size = new System.Drawing.Size(74, 21);
            this.txt_mload_d1.TabIndex = 17;
            this.txt_mload_d1.Text = "0.0";
            this.txt_mload_d1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(247, 145);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 14);
            this.label7.TabIndex = 24;
            this.label7.Text = "Load Value (W)";
            // 
            // txt_mload_d2
            // 
            this.txt_mload_d2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_mload_d2.Location = new System.Drawing.Point(413, 198);
            this.txt_mload_d2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_mload_d2.Name = "txt_mload_d2";
            this.txt_mload_d2.Size = new System.Drawing.Size(74, 21);
            this.txt_mload_d2.TabIndex = 17;
            this.txt_mload_d2.Text = "0.0";
            this.txt_mload_d2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_mload_val
            // 
            this.txt_mload_val.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_mload_val.Location = new System.Drawing.Point(358, 143);
            this.txt_mload_val.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_mload_val.Name = "txt_mload_val";
            this.txt_mload_val.Size = new System.Drawing.Size(74, 21);
            this.txt_mload_val.TabIndex = 23;
            this.txt_mload_val.Text = "0.0";
            this.txt_mload_val.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 200);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(365, 14);
            this.label2.TabIndex = 22;
            this.label2.Text = "From the start of the member to the end of the load (d2)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 145);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 14);
            this.label1.TabIndex = 19;
            this.label1.Text = "Direction";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 174);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(371, 14);
            this.label6.TabIndex = 22;
            this.label6.Text = "From the start of the member to the start of the load (d1)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmb_mload_type
            // 
            this.cmb_mload_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_mload_type.FormattingEnabled = true;
            this.cmb_mload_type.Items.AddRange(new object[] {
            "UNI",
            "UMOM",
            "CONC",
            "CMOM"});
            this.cmb_mload_type.Location = new System.Drawing.Point(20, 30);
            this.cmb_mload_type.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmb_mload_type.Name = "cmb_mload_type";
            this.cmb_mload_type.Size = new System.Drawing.Size(76, 22);
            this.cmb_mload_type.TabIndex = 20;
            this.cmb_mload_type.SelectedIndexChanged += new System.EventHandler(this.cmb_mload_type_SelectedIndexChanged);
            // 
            // cmb_mload_dir
            // 
            this.cmb_mload_dir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_mload_dir.FormattingEnabled = true;
            this.cmb_mload_dir.Items.AddRange(new object[] {
            "X",
            "Y",
            "Z",
            "GX",
            "GY",
            "GZ",
            "PX",
            "PY",
            "PZ"});
            this.cmb_mload_dir.Location = new System.Drawing.Point(144, 140);
            this.cmb_mload_dir.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmb_mload_dir.Name = "cmb_mload_dir";
            this.cmb_mload_dir.Size = new System.Drawing.Size(65, 22);
            this.cmb_mload_dir.TabIndex = 21;
            // 
            // tab_LIN
            // 
            this.tab_LIN.Controls.Add(this.pictureBox1);
            this.tab_LIN.Controls.Add(this.btn_lin_close);
            this.tab_LIN.Controls.Add(this.label12);
            this.tab_LIN.Controls.Add(this.txt_mnos_lin);
            this.tab_LIN.Controls.Add(this.btn_lload_add);
            this.tab_LIN.Controls.Add(this.label3);
            this.tab_LIN.Controls.Add(this.label8);
            this.tab_LIN.Controls.Add(this.label4);
            this.tab_LIN.Controls.Add(this.txt_lload_end);
            this.tab_LIN.Controls.Add(this.txt_lload_start);
            this.tab_LIN.Controls.Add(this.label11);
            this.tab_LIN.Controls.Add(this.cmb_lload_dir);
            this.tab_LIN.Location = new System.Drawing.Point(4, 23);
            this.tab_LIN.Name = "tab_LIN";
            this.tab_LIN.Padding = new System.Windows.Forms.Padding(3);
            this.tab_LIN.Size = new System.Drawing.Size(514, 415);
            this.tab_LIN.TabIndex = 1;
            this.tab_LIN.Text = "Linear Load";
            this.tab_LIN.UseVisualStyleBackColor = true;
            // 
            // btn_lin_close
            // 
            this.btn_lin_close.Location = new System.Drawing.Point(248, 150);
            this.btn_lin_close.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btn_lin_close.Name = "btn_lin_close";
            this.btn_lin_close.Size = new System.Drawing.Size(125, 31);
            this.btn_lin_close.TabIndex = 48;
            this.btn_lin_close.Text = "Close";
            this.btn_lin_close.UseVisualStyleBackColor = true;
            this.btn_lin_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(147, 18);
            this.label12.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(246, 14);
            this.label12.TabIndex = 47;
            this.label12.Text = "Seperated by comma (\',\') or space (\' \')";
            // 
            // txt_mnos_lin
            // 
            this.txt_mnos_lin.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_mnos_lin.Location = new System.Drawing.Point(150, 35);
            this.txt_mnos_lin.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_mnos_lin.Name = "txt_mnos_lin";
            this.txt_mnos_lin.Size = new System.Drawing.Size(321, 22);
            this.txt_mnos_lin.TabIndex = 43;
            this.txt_mnos_lin.Text = "1";
            // 
            // btn_lload_add
            // 
            this.btn_lload_add.Location = new System.Drawing.Point(123, 150);
            this.btn_lload_add.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_lload_add.Name = "btn_lload_add";
            this.btn_lload_add.Size = new System.Drawing.Size(116, 31);
            this.btn_lload_add.TabIndex = 25;
            this.btn_lload_add.Text = "Add Load";
            this.btn_lload_add.UseVisualStyleBackColor = true;
            this.btn_lload_add.Click += new System.EventHandler(this.btn_lload_add_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 38);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 14);
            this.label3.TabIndex = 42;
            this.label3.Text = "Member Numbers";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(283, 113);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 14);
            this.label8.TabIndex = 41;
            this.label8.Text = "End Load (W2)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 113);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 14);
            this.label4.TabIndex = 41;
            this.label4.Text = "Start Load (w1)";
            // 
            // txt_lload_end
            // 
            this.txt_lload_end.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_lload_end.Location = new System.Drawing.Point(392, 111);
            this.txt_lload_end.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_lload_end.Name = "txt_lload_end";
            this.txt_lload_end.Size = new System.Drawing.Size(74, 21);
            this.txt_lload_end.TabIndex = 40;
            this.txt_lload_end.Text = "0.0";
            this.txt_lload_end.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_lload_start
            // 
            this.txt_lload_start.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_lload_start.Location = new System.Drawing.Point(150, 111);
            this.txt_lload_start.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_lload_start.Name = "txt_lload_start";
            this.txt_lload_start.Size = new System.Drawing.Size(74, 21);
            this.txt_lload_start.TabIndex = 40;
            this.txt_lload_start.Text = "0.0";
            this.txt_lload_start.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 82);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 14);
            this.label11.TabIndex = 34;
            this.label11.Text = "Direction";
            // 
            // cmb_lload_dir
            // 
            this.cmb_lload_dir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_lload_dir.FormattingEnabled = true;
            this.cmb_lload_dir.Items.AddRange(new object[] {
            "X",
            "Y",
            "Z"});
            this.cmb_lload_dir.Location = new System.Drawing.Point(150, 74);
            this.cmb_lload_dir.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmb_lload_dir.Name = "cmb_lload_dir";
            this.cmb_lload_dir.Size = new System.Drawing.Size(74, 22);
            this.cmb_lload_dir.TabIndex = 37;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtn_CMOM);
            this.groupBox1.Controls.Add(this.rbtn_UMOM);
            this.groupBox1.Controls.Add(this.rbtn_CON);
            this.groupBox1.Controls.Add(this.rbtn_UNI);
            this.groupBox1.Controls.Add(this.cmb_mload_type);
            this.groupBox1.Location = new System.Drawing.Point(10, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(477, 68);
            this.groupBox1.TabIndex = 49;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Load Type";
            // 
            // rbtn_UNI
            // 
            this.rbtn_UNI.AutoSize = true;
            this.rbtn_UNI.Checked = true;
            this.rbtn_UNI.Location = new System.Drawing.Point(134, 21);
            this.rbtn_UNI.Name = "rbtn_UNI";
            this.rbtn_UNI.Size = new System.Drawing.Size(111, 18);
            this.rbtn_UNI.TabIndex = 0;
            this.rbtn_UNI.TabStop = true;
            this.rbtn_UNI.Text = "Uniform Force";
            this.rbtn_UNI.UseVisualStyleBackColor = true;
            this.rbtn_UNI.CheckedChanged += new System.EventHandler(this.rbtn_UNI_CheckedChanged);
            // 
            // rbtn_UMOM
            // 
            this.rbtn_UMOM.AutoSize = true;
            this.rbtn_UMOM.Location = new System.Drawing.Point(297, 18);
            this.rbtn_UMOM.Name = "rbtn_UMOM";
            this.rbtn_UMOM.Size = new System.Drawing.Size(127, 18);
            this.rbtn_UMOM.TabIndex = 0;
            this.rbtn_UMOM.Text = "Uniform Moment";
            this.rbtn_UMOM.UseVisualStyleBackColor = true;
            this.rbtn_UMOM.CheckedChanged += new System.EventHandler(this.rbtn_UNI_CheckedChanged);
            // 
            // rbtn_CMOM
            // 
            this.rbtn_CMOM.AutoSize = true;
            this.rbtn_CMOM.Location = new System.Drawing.Point(297, 42);
            this.rbtn_CMOM.Name = "rbtn_CMOM";
            this.rbtn_CMOM.Size = new System.Drawing.Size(142, 18);
            this.rbtn_CMOM.TabIndex = 0;
            this.rbtn_CMOM.TabStop = true;
            this.rbtn_CMOM.Text = "Concertric Moment";
            this.rbtn_CMOM.UseVisualStyleBackColor = true;
            this.rbtn_CMOM.CheckedChanged += new System.EventHandler(this.rbtn_UNI_CheckedChanged);
            // 
            // rbtn_CON
            // 
            this.rbtn_CON.AutoSize = true;
            this.rbtn_CON.Location = new System.Drawing.Point(134, 42);
            this.rbtn_CON.Name = "rbtn_CON";
            this.rbtn_CON.Size = new System.Drawing.Size(130, 18);
            this.rbtn_CON.TabIndex = 0;
            this.rbtn_CON.TabStop = true;
            this.rbtn_CON.Text = " Concertric Force";
            this.rbtn_CON.UseVisualStyleBackColor = true;
            this.rbtn_CON.CheckedChanged += new System.EventHandler(this.rbtn_UNI_CheckedChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::HEADSNeed.Properties.Resources.MLoads;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(10, 269);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(478, 135);
            this.pictureBox2.TabIndex = 50;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::HEADSNeed.Properties.Resources.lINLoads;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(165, 226);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(180, 101);
            this.pictureBox1.TabIndex = 49;
            this.pictureBox1.TabStop = false;
            // 
            // frmMemberLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 442);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMemberLoad";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Member Load";
            this.Load += new System.EventHandler(this.frmMemberLoad_Load);
            this.tabControl1.ResumeLayout(false);
            this.tab_mem_load.ResumeLayout(false);
            this.tab_mem_load.PerformLayout();
            this.tab_LIN.ResumeLayout(false);
            this.tab_LIN.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txt_mnos_uni;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_mload_val;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmb_mload_dir;
        private System.Windows.Forms.ComboBox cmb_mload_type;
        private System.Windows.Forms.TextBox txt_mload_d2;
        private System.Windows.Forms.TextBox txt_mload_d1;
        private System.Windows.Forms.Button btn_mload_add;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_mem_load;
        private System.Windows.Forms.TabPage tab_LIN;
        private System.Windows.Forms.TextBox txt_mnos_lin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_lload_start;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmb_lload_dir;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_lload_end;
        private System.Windows.Forms.Button btn_lload_add;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btn_lin_close;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RadioButton rbtn_CMOM;
        private System.Windows.Forms.RadioButton rbtn_CON;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtn_UMOM;
        private System.Windows.Forms.RadioButton rbtn_UNI;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}