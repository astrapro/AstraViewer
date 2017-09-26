namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    partial class frmSectionProperties
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmb_range = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.grb_Calculated_Values = new System.Windows.Forms.GroupBox();
            this.txt_IZ = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_IY = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_IX = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_AX = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.grb_Dimension = new System.Windows.Forms.GroupBox();
            this.txt_ZB = new System.Windows.Forms.TextBox();
            this.txt_ZD = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_YB = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_YD = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.rbtn_calculated_values = new System.Windows.Forms.RadioButton();
            this.rbtn_dimension = new System.Windows.Forms.RadioButton();
            this.txt_member_nos = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_add_data = new System.Windows.Forms.Button();
            this.gb1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbtn_member_select = new System.Windows.Forms.RadioButton();
            this.rbtn_column_all = new System.Windows.Forms.RadioButton();
            this.rbtn_column_select = new System.Windows.Forms.RadioButton();
            this.cmb_floor_levels = new System.Windows.Forms.ComboBox();
            this.rbtn_beam_select = new System.Windows.Forms.RadioButton();
            this.rbtn_beam_floor = new System.Windows.Forms.RadioButton();
            this.rbtn_beam_all = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grb_Calculated_Values.SuspendLayout();
            this.grb_Dimension.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.gb1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.cmb_range);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.txt_member_nos);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 105);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(510, 377);
            this.panel1.TabIndex = 0;
            // 
            // cmb_range
            // 
            this.cmb_range.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_range.DropDownWidth = 300;
            this.cmb_range.FormattingEnabled = true;
            this.cmb_range.Items.AddRange(new object[] {
            "ALL",
            "NUMBERS"});
            this.cmb_range.Location = new System.Drawing.Point(15, 21);
            this.cmb_range.Name = "cmb_range";
            this.cmb_range.Size = new System.Drawing.Size(93, 21);
            this.cmb_range.TabIndex = 0;
            this.cmb_range.SelectedIndexChanged += new System.EventHandler(this.cmb_range_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.grb_Calculated_Values);
            this.groupBox1.Controls.Add(this.grb_Dimension);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.rbtn_calculated_values);
            this.groupBox1.Controls.Add(this.rbtn_dimension);
            this.groupBox1.Location = new System.Drawing.Point(15, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(487, 306);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(325, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(124, 14);
            this.label11.TabIndex = 16;
            this.label11.Text = "Calculated Values";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(36, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 14);
            this.label10.TabIndex = 15;
            this.label10.Text = "Dimension";
            // 
            // grb_Calculated_Values
            // 
            this.grb_Calculated_Values.Controls.Add(this.txt_IZ);
            this.grb_Calculated_Values.Controls.Add(this.label6);
            this.grb_Calculated_Values.Controls.Add(this.txt_IY);
            this.grb_Calculated_Values.Controls.Add(this.label5);
            this.grb_Calculated_Values.Controls.Add(this.txt_IX);
            this.grb_Calculated_Values.Controls.Add(this.label4);
            this.grb_Calculated_Values.Controls.Add(this.txt_AX);
            this.grb_Calculated_Values.Controls.Add(this.label3);
            this.grb_Calculated_Values.Location = new System.Drawing.Point(292, 41);
            this.grb_Calculated_Values.Name = "grb_Calculated_Values";
            this.grb_Calculated_Values.Size = new System.Drawing.Size(187, 117);
            this.grb_Calculated_Values.TabIndex = 0;
            this.grb_Calculated_Values.TabStop = false;
            // 
            // txt_IZ
            // 
            this.txt_IZ.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_IZ.Location = new System.Drawing.Point(66, 89);
            this.txt_IZ.Name = "txt_IZ";
            this.txt_IZ.Size = new System.Drawing.Size(115, 22);
            this.txt_IZ.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(22, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 14);
            this.label6.TabIndex = 12;
            this.label6.Text = "IZ";
            // 
            // txt_IY
            // 
            this.txt_IY.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_IY.Location = new System.Drawing.Point(66, 63);
            this.txt_IY.Name = "txt_IY";
            this.txt_IY.Size = new System.Drawing.Size(115, 22);
            this.txt_IY.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(22, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 14);
            this.label5.TabIndex = 10;
            this.label5.Text = "IY";
            // 
            // txt_IX
            // 
            this.txt_IX.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_IX.Location = new System.Drawing.Point(66, 37);
            this.txt_IX.Name = "txt_IX";
            this.txt_IX.Size = new System.Drawing.Size(115, 22);
            this.txt_IX.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(22, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 14);
            this.label4.TabIndex = 8;
            this.label4.Text = "IX";
            // 
            // txt_AX
            // 
            this.txt_AX.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_AX.Location = new System.Drawing.Point(66, 11);
            this.txt_AX.Name = "txt_AX";
            this.txt_AX.Size = new System.Drawing.Size(115, 22);
            this.txt_AX.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(22, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "AX";
            // 
            // grb_Dimension
            // 
            this.grb_Dimension.Controls.Add(this.txt_ZB);
            this.grb_Dimension.Controls.Add(this.txt_ZD);
            this.grb_Dimension.Controls.Add(this.label9);
            this.grb_Dimension.Controls.Add(this.label2);
            this.grb_Dimension.Controls.Add(this.txt_YB);
            this.grb_Dimension.Controls.Add(this.label7);
            this.grb_Dimension.Controls.Add(this.txt_YD);
            this.grb_Dimension.Controls.Add(this.label1);
            this.grb_Dimension.Location = new System.Drawing.Point(6, 40);
            this.grb_Dimension.Name = "grb_Dimension";
            this.grb_Dimension.Size = new System.Drawing.Size(177, 117);
            this.grb_Dimension.TabIndex = 14;
            this.grb_Dimension.TabStop = false;
            // 
            // txt_ZB
            // 
            this.txt_ZB.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ZB.Location = new System.Drawing.Point(66, 89);
            this.txt_ZB.Name = "txt_ZB";
            this.txt_ZB.Size = new System.Drawing.Size(93, 22);
            this.txt_ZB.TabIndex = 3;
            this.txt_ZB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_ZB.TextChanged += new System.EventHandler(this.txt_YD_TextChanged);
            this.txt_ZB.Leave += new System.EventHandler(this.txt_YD_TextChanged);
            // 
            // txt_ZD
            // 
            this.txt_ZD.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ZD.Location = new System.Drawing.Point(66, 37);
            this.txt_ZD.Name = "txt_ZD";
            this.txt_ZD.Size = new System.Drawing.Size(93, 22);
            this.txt_ZD.TabIndex = 1;
            this.txt_ZD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_ZD.TextChanged += new System.EventHandler(this.txt_YD_TextChanged);
            this.txt_ZD.Leave += new System.EventHandler(this.txt_YD_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 92);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(24, 14);
            this.label9.TabIndex = 4;
            this.label9.Text = "ZB";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "ZD";
            // 
            // txt_YB
            // 
            this.txt_YB.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_YB.Location = new System.Drawing.Point(66, 63);
            this.txt_YB.Name = "txt_YB";
            this.txt_YB.Size = new System.Drawing.Size(93, 22);
            this.txt_YB.TabIndex = 2;
            this.txt_YB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_YB.TextChanged += new System.EventHandler(this.txt_YD_TextChanged);
            this.txt_YB.Leave += new System.EventHandler(this.txt_YD_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 14);
            this.label7.TabIndex = 2;
            this.label7.Text = "YB";
            // 
            // txt_YD
            // 
            this.txt_YD.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_YD.Location = new System.Drawing.Point(66, 11);
            this.txt_YD.Name = "txt_YD";
            this.txt_YD.Size = new System.Drawing.Size(93, 22);
            this.txt_YD.TabIndex = 0;
            this.txt_YD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_YD.TextChanged += new System.EventHandler(this.txt_YD_TextChanged);
            this.txt_YD.Leave += new System.EventHandler(this.txt_YD_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "YD";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::HEADSNeed.Properties.Resources.MProps;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(6, 163);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(474, 145);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // rbtn_calculated_values
            // 
            this.rbtn_calculated_values.AutoSize = true;
            this.rbtn_calculated_values.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_calculated_values.Location = new System.Drawing.Point(189, 17);
            this.rbtn_calculated_values.Name = "rbtn_calculated_values";
            this.rbtn_calculated_values.Size = new System.Drawing.Size(140, 17);
            this.rbtn_calculated_values.TabIndex = 0;
            this.rbtn_calculated_values.Text = "Calculated Values";
            this.rbtn_calculated_values.UseVisualStyleBackColor = true;
            this.rbtn_calculated_values.Visible = false;
            this.rbtn_calculated_values.CheckedChanged += new System.EventHandler(this.rbtn_dimension_CheckedChanged);
            // 
            // rbtn_dimension
            // 
            this.rbtn_dimension.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_dimension.Location = new System.Drawing.Point(189, 40);
            this.rbtn_dimension.Name = "rbtn_dimension";
            this.rbtn_dimension.Size = new System.Drawing.Size(93, 17);
            this.rbtn_dimension.TabIndex = 0;
            this.rbtn_dimension.Text = "Dimension";
            this.rbtn_dimension.UseVisualStyleBackColor = true;
            this.rbtn_dimension.Visible = false;
            this.rbtn_dimension.CheckedChanged += new System.EventHandler(this.rbtn_dimension_CheckedChanged);
            // 
            // txt_member_nos
            // 
            this.txt_member_nos.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_member_nos.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_member_nos.Location = new System.Drawing.Point(115, 21);
            this.txt_member_nos.Name = "txt_member_nos";
            this.txt_member_nos.Size = new System.Drawing.Size(387, 22);
            this.txt_member_nos.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(115, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(123, 14);
            this.label8.TabIndex = 2;
            this.label8.Text = "Member Numbers";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btn_cancel);
            this.groupBox4.Controls.Add(this.btn_add_data);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox4.Location = new System.Drawing.Point(0, 482);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(510, 51);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(258, 10);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(94, 30);
            this.btn_cancel.TabIndex = 1;
            this.btn_cancel.Text = "Close";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_add_data
            // 
            this.btn_add_data.Location = new System.Drawing.Point(150, 10);
            this.btn_add_data.Name = "btn_add_data";
            this.btn_add_data.Size = new System.Drawing.Size(94, 30);
            this.btn_add_data.TabIndex = 0;
            this.btn_add_data.Text = "Add Data";
            this.btn_add_data.UseVisualStyleBackColor = true;
            this.btn_add_data.Click += new System.EventHandler(this.btn_add_data_Click);
            // 
            // gb1
            // 
            this.gb1.Controls.Add(this.groupBox3);
            this.gb1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gb1.Location = new System.Drawing.Point(0, 0);
            this.gb1.Name = "gb1";
            this.gb1.Size = new System.Drawing.Size(510, 105);
            this.gb1.TabIndex = 5;
            this.gb1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbtn_member_select);
            this.groupBox3.Controls.Add(this.rbtn_column_all);
            this.groupBox3.Controls.Add(this.rbtn_column_select);
            this.groupBox3.Controls.Add(this.cmb_floor_levels);
            this.groupBox3.Controls.Add(this.rbtn_beam_select);
            this.groupBox3.Controls.Add(this.rbtn_beam_floor);
            this.groupBox3.Controls.Add(this.rbtn_beam_all);
            this.groupBox3.Location = new System.Drawing.Point(6, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(498, 87);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "SELECT BEAMS OR COLUMNS";
            // 
            // rbtn_member_select
            // 
            this.rbtn_member_select.AutoSize = true;
            this.rbtn_member_select.Location = new System.Drawing.Point(238, 66);
            this.rbtn_member_select.Name = "rbtn_member_select";
            this.rbtn_member_select.Size = new System.Drawing.Size(150, 17);
            this.rbtn_member_select.TabIndex = 3;
            this.rbtn_member_select.TabStop = true;
            this.rbtn_member_select.Text = "On Selected Members";
            this.rbtn_member_select.UseVisualStyleBackColor = true;
            this.rbtn_member_select.CheckedChanged += new System.EventHandler(this.rbtn_beam_all_CheckedChanged);
            // 
            // rbtn_column_all
            // 
            this.rbtn_column_all.AutoSize = true;
            this.rbtn_column_all.Location = new System.Drawing.Point(11, 66);
            this.rbtn_column_all.Name = "rbtn_column_all";
            this.rbtn_column_all.Size = new System.Drawing.Size(93, 17);
            this.rbtn_column_all.TabIndex = 1;
            this.rbtn_column_all.TabStop = true;
            this.rbtn_column_all.Text = "All Columns";
            this.rbtn_column_all.UseVisualStyleBackColor = true;
            this.rbtn_column_all.CheckedChanged += new System.EventHandler(this.rbtn_beam_all_CheckedChanged);
            // 
            // rbtn_column_select
            // 
            this.rbtn_column_select.AutoSize = true;
            this.rbtn_column_select.Location = new System.Drawing.Point(238, 43);
            this.rbtn_column_select.Name = "rbtn_column_select";
            this.rbtn_column_select.Size = new System.Drawing.Size(148, 17);
            this.rbtn_column_select.TabIndex = 2;
            this.rbtn_column_select.TabStop = true;
            this.rbtn_column_select.Text = "On Selected Columns";
            this.rbtn_column_select.UseVisualStyleBackColor = true;
            this.rbtn_column_select.CheckedChanged += new System.EventHandler(this.rbtn_beam_all_CheckedChanged);
            // 
            // cmb_floor_levels
            // 
            this.cmb_floor_levels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_floor_levels.FormattingEnabled = true;
            this.cmb_floor_levels.Location = new System.Drawing.Point(120, 18);
            this.cmb_floor_levels.Name = "cmb_floor_levels";
            this.cmb_floor_levels.Size = new System.Drawing.Size(94, 21);
            this.cmb_floor_levels.TabIndex = 1;
            this.cmb_floor_levels.SelectedIndexChanged += new System.EventHandler(this.cmb_floor_levels_SelectedIndexChanged);
            // 
            // rbtn_beam_select
            // 
            this.rbtn_beam_select.AutoSize = true;
            this.rbtn_beam_select.Location = new System.Drawing.Point(238, 20);
            this.rbtn_beam_select.Name = "rbtn_beam_select";
            this.rbtn_beam_select.Size = new System.Drawing.Size(137, 17);
            this.rbtn_beam_select.TabIndex = 0;
            this.rbtn_beam_select.Text = "On Selected Beams";
            this.rbtn_beam_select.UseVisualStyleBackColor = true;
            this.rbtn_beam_select.CheckedChanged += new System.EventHandler(this.rbtn_beam_all_CheckedChanged);
            // 
            // rbtn_beam_floor
            // 
            this.rbtn_beam_floor.AutoSize = true;
            this.rbtn_beam_floor.Location = new System.Drawing.Point(11, 19);
            this.rbtn_beam_floor.Name = "rbtn_beam_floor";
            this.rbtn_beam_floor.Size = new System.Drawing.Size(103, 17);
            this.rbtn_beam_floor.TabIndex = 0;
            this.rbtn_beam_floor.Text = "At Floor Level";
            this.rbtn_beam_floor.UseVisualStyleBackColor = true;
            this.rbtn_beam_floor.CheckedChanged += new System.EventHandler(this.rbtn_beam_all_CheckedChanged);
            // 
            // rbtn_beam_all
            // 
            this.rbtn_beam_all.AutoSize = true;
            this.rbtn_beam_all.Location = new System.Drawing.Point(11, 42);
            this.rbtn_beam_all.Name = "rbtn_beam_all";
            this.rbtn_beam_all.Size = new System.Drawing.Size(82, 17);
            this.rbtn_beam_all.TabIndex = 0;
            this.rbtn_beam_all.Text = "All Beams";
            this.rbtn_beam_all.UseVisualStyleBackColor = true;
            this.rbtn_beam_all.CheckedChanged += new System.EventHandler(this.rbtn_beam_all_CheckedChanged);
            // 
            // frmSectionProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 533);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gb1);
            this.Controls.Add(this.groupBox4);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSectionProperties";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Section Properties";
            this.Load += new System.EventHandler(this.frmSectionProperties_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grb_Calculated_Values.ResumeLayout(false);
            this.grb_Calculated_Values.PerformLayout();
            this.grb_Dimension.ResumeLayout(false);
            this.grb_Dimension.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.gb1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_add_data;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtn_calculated_values;
        private System.Windows.Forms.RadioButton rbtn_dimension;
        private System.Windows.Forms.GroupBox grb_Calculated_Values;
        private System.Windows.Forms.TextBox txt_IZ;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_IY;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_IX;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_AX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox grb_Dimension;
        private System.Windows.Forms.TextBox txt_ZD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_YD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_member_nos;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cmb_range;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txt_ZB;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_YB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox gb1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbtn_column_select;
        private System.Windows.Forms.RadioButton rbtn_column_all;
        private System.Windows.Forms.ComboBox cmb_floor_levels;
        private System.Windows.Forms.RadioButton rbtn_beam_select;
        private System.Windows.Forms.RadioButton rbtn_beam_floor;
        private System.Windows.Forms.RadioButton rbtn_beam_all;
        private System.Windows.Forms.RadioButton rbtn_member_select;
    }
}