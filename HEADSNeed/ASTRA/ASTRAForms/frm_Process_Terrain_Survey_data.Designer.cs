namespace HEADSNeed.ASTRA.ASTRAForms
{
    partial class frm_Process_Terrain_Survey_data
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
            AbortThread();
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
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_process = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txt_ele_model = new System.Windows.Forms.TextBox();
            this.txt_ele_inc = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_ele_string = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_sec_model = new System.Windows.Forms.TextBox();
            this.txt_sec_inc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_sec_string = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_pri_model = new System.Windows.Forms.TextBox();
            this.txt_pri_inc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_pri_string = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_left_ho = new System.Windows.Forms.TextBox();
            this.txt_right_ho = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_string = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_model = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btn_browse = new System.Windows.Forms.Button();
            this.txt_select_survey_data = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_chn_interval = new System.Windows.Forms.TextBox();
            this.txt_sc = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txt_make_str_String = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txt_make_str_Model = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Location = new System.Drawing.Point(334, 415);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(82, 23);
            this.btn_Cancel.TabIndex = 15;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_process
            // 
            this.btn_process.Enabled = false;
            this.btn_process.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_process.Location = new System.Drawing.Point(142, 415);
            this.btn_process.Name = "btn_process";
            this.btn_process.Size = new System.Drawing.Size(82, 23);
            this.btn_process.TabIndex = 14;
            this.btn_process.Text = "Proceed";
            this.btn_process.UseVisualStyleBackColor = true;
            this.btn_process.Click += new System.EventHandler(this.btn_process_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txt_ele_model);
            this.groupBox3.Controls.Add(this.txt_ele_inc);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txt_ele_string);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Location = new System.Drawing.Point(5, 124);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(524, 47);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Contour Elevation Text";
            // 
            // txt_ele_model
            // 
            this.txt_ele_model.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ele_model.Location = new System.Drawing.Point(85, 18);
            this.txt_ele_model.Name = "txt_ele_model";
            this.txt_ele_model.Size = new System.Drawing.Size(100, 22);
            this.txt_ele_model.TabIndex = 1;
            this.txt_ele_model.Text = "CONTOUR";
            // 
            // txt_ele_inc
            // 
            this.txt_ele_inc.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ele_inc.Location = new System.Drawing.Point(449, 18);
            this.txt_ele_inc.Name = "txt_ele_inc";
            this.txt_ele_inc.Size = new System.Drawing.Size(61, 22);
            this.txt_ele_inc.TabIndex = 5;
            this.txt_ele_inc.Text = "10.0";
            this.txt_ele_inc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(383, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Increament";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Model Name";
            // 
            // txt_ele_string
            // 
            this.txt_ele_string.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ele_string.Location = new System.Drawing.Point(264, 19);
            this.txt_ele_string.Name = "txt_ele_string";
            this.txt_ele_string.Size = new System.Drawing.Size(100, 22);
            this.txt_ele_string.TabIndex = 3;
            this.txt_ele_string.Text = "ELEV";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(200, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "String Label";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_sec_model);
            this.groupBox2.Controls.Add(this.txt_sec_inc);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txt_sec_string);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(5, 71);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(524, 47);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Secondary Contour Model";
            // 
            // txt_sec_model
            // 
            this.txt_sec_model.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_sec_model.Location = new System.Drawing.Point(85, 18);
            this.txt_sec_model.Name = "txt_sec_model";
            this.txt_sec_model.Size = new System.Drawing.Size(100, 22);
            this.txt_sec_model.TabIndex = 1;
            this.txt_sec_model.Text = "CONTOUR";
            // 
            // txt_sec_inc
            // 
            this.txt_sec_inc.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_sec_inc.Location = new System.Drawing.Point(449, 18);
            this.txt_sec_inc.Name = "txt_sec_inc";
            this.txt_sec_inc.Size = new System.Drawing.Size(61, 22);
            this.txt_sec_inc.TabIndex = 5;
            this.txt_sec_inc.Text = "10.0";
            this.txt_sec_inc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(383, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Increament";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Model Name";
            // 
            // txt_sec_string
            // 
            this.txt_sec_string.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_sec_string.Location = new System.Drawing.Point(264, 18);
            this.txt_sec_string.Name = "txt_sec_string";
            this.txt_sec_string.Size = new System.Drawing.Size(100, 22);
            this.txt_sec_string.TabIndex = 3;
            this.txt_sec_string.Text = "C005";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(200, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "String Label";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_pri_model);
            this.groupBox1.Controls.Add(this.txt_pri_inc);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_pri_string);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(5, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(524, 47);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Primary Contour Model";
            // 
            // txt_pri_model
            // 
            this.txt_pri_model.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_pri_model.Location = new System.Drawing.Point(85, 18);
            this.txt_pri_model.Name = "txt_pri_model";
            this.txt_pri_model.Size = new System.Drawing.Size(100, 22);
            this.txt_pri_model.TabIndex = 1;
            this.txt_pri_model.Text = "CONTOUR";
            // 
            // txt_pri_inc
            // 
            this.txt_pri_inc.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_pri_inc.Location = new System.Drawing.Point(449, 19);
            this.txt_pri_inc.Name = "txt_pri_inc";
            this.txt_pri_inc.Size = new System.Drawing.Size(61, 22);
            this.txt_pri_inc.TabIndex = 5;
            this.txt_pri_inc.Text = "2.0";
            this.txt_pri_inc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(383, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Increament";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Model Name";
            // 
            // txt_pri_string
            // 
            this.txt_pri_string.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_pri_string.Location = new System.Drawing.Point(264, 19);
            this.txt_pri_string.Name = "txt_pri_string";
            this.txt_pri_string.Size = new System.Drawing.Size(100, 22);
            this.txt_pri_string.TabIndex = 3;
            this.txt_pri_string.Text = "C001";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(200, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "String Label";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.txt_left_ho);
            this.groupBox4.Controls.Add(this.txt_right_ho);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.txt_string);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.txt_model);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Location = new System.Drawing.Point(12, 151);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(551, 80);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Ground Modeling by Triangulation";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(296, 49);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(132, 13);
            this.label14.TabIndex = 14;
            this.label14.Text = "Right Side Offset Distance";
            // 
            // txt_left_ho
            // 
            this.txt_left_ho.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_left_ho.Location = new System.Drawing.Point(438, 46);
            this.txt_left_ho.Name = "txt_left_ho";
            this.txt_left_ho.Size = new System.Drawing.Size(64, 21);
            this.txt_left_ho.TabIndex = 13;
            this.txt_left_ho.Text = "500";
            this.txt_left_ho.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_right_ho
            // 
            this.txt_right_ho.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_right_ho.Location = new System.Drawing.Point(152, 46);
            this.txt_right_ho.Name = "txt_right_ho";
            this.txt_right_ho.Size = new System.Drawing.Size(71, 21);
            this.txt_right_ho.TabIndex = 11;
            this.txt_right_ho.Text = "500";
            this.txt_right_ho.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(19, 49);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(125, 13);
            this.label13.TabIndex = 10;
            this.label13.Text = "Left Side Offset Distance";
            // 
            // txt_string
            // 
            this.txt_string.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_string.Location = new System.Drawing.Point(438, 19);
            this.txt_string.Name = "txt_string";
            this.txt_string.Size = new System.Drawing.Size(64, 21);
            this.txt_string.TabIndex = 9;
            this.txt_string.Text = "BDRY";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(296, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(111, 13);
            this.label10.TabIndex = 8;
            this.label10.Text = "Boundary String Label";
            // 
            // txt_model
            // 
            this.txt_model.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_model.Location = new System.Drawing.Point(152, 19);
            this.txt_model.Name = "txt_model";
            this.txt_model.Size = new System.Drawing.Size(71, 21);
            this.txt_model.TabIndex = 7;
            this.txt_model.Text = "BOUND";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(19, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(115, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "Boundary Model Name";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.groupBox3);
            this.groupBox5.Controls.Add(this.groupBox2);
            this.groupBox5.Controls.Add(this.groupBox1);
            this.groupBox5.Location = new System.Drawing.Point(12, 229);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(551, 180);
            this.groupBox5.TabIndex = 17;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Contour Modeling";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btn_browse);
            this.groupBox6.Controls.Add(this.txt_select_survey_data);
            this.groupBox6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(12, 12);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(551, 47);
            this.groupBox6.TabIndex = 18;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Select Terrain Data File";
            // 
            // btn_browse
            // 
            this.btn_browse.Location = new System.Drawing.Point(483, 18);
            this.btn_browse.Name = "btn_browse";
            this.btn_browse.Size = new System.Drawing.Size(62, 23);
            this.btn_browse.TabIndex = 11;
            this.btn_browse.Text = "Browse";
            this.btn_browse.UseVisualStyleBackColor = true;
            this.btn_browse.Click += new System.EventHandler(this.btn_browse_Click);
            // 
            // txt_select_survey_data
            // 
            this.txt_select_survey_data.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_select_survey_data.Location = new System.Drawing.Point(7, 19);
            this.txt_select_survey_data.Name = "txt_select_survey_data";
            this.txt_select_survey_data.Size = new System.Drawing.Size(470, 21);
            this.txt_select_survey_data.TabIndex = 10;
            this.txt_select_survey_data.TextChanged += new System.EventHandler(this.txt_select_survey_data_TextChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label12);
            this.groupBox7.Controls.Add(this.txt_chn_interval);
            this.groupBox7.Controls.Add(this.txt_sc);
            this.groupBox7.Controls.Add(this.label15);
            this.groupBox7.Controls.Add(this.txt_make_str_String);
            this.groupBox7.Controls.Add(this.label16);
            this.groupBox7.Controls.Add(this.txt_make_str_Model);
            this.groupBox7.Controls.Add(this.label17);
            this.groupBox7.Location = new System.Drawing.Point(12, 65);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(551, 80);
            this.groupBox7.TabIndex = 17;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Make River Centre Line as String";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(296, 49);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(90, 13);
            this.label12.TabIndex = 14;
            this.label12.Text = "Chainage Interval";
            // 
            // txt_chn_interval
            // 
            this.txt_chn_interval.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_chn_interval.Location = new System.Drawing.Point(438, 46);
            this.txt_chn_interval.Name = "txt_chn_interval";
            this.txt_chn_interval.Size = new System.Drawing.Size(64, 21);
            this.txt_chn_interval.TabIndex = 13;
            this.txt_chn_interval.Text = "10";
            this.txt_chn_interval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_sc
            // 
            this.txt_sc.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_sc.Location = new System.Drawing.Point(152, 46);
            this.txt_sc.Name = "txt_sc";
            this.txt_sc.Size = new System.Drawing.Size(71, 21);
            this.txt_sc.TabIndex = 11;
            this.txt_sc.Text = "0";
            this.txt_sc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(19, 49);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(77, 13);
            this.label15.TabIndex = 10;
            this.label15.Text = "Start Chainage";
            // 
            // txt_make_str_String
            // 
            this.txt_make_str_String.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_make_str_String.Location = new System.Drawing.Point(438, 19);
            this.txt_make_str_String.Name = "txt_make_str_String";
            this.txt_make_str_String.Size = new System.Drawing.Size(64, 21);
            this.txt_make_str_String.TabIndex = 9;
            this.txt_make_str_String.Text = "M001";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(296, 22);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(63, 13);
            this.label16.TabIndex = 8;
            this.label16.Text = "String Label";
            // 
            // txt_make_str_Model
            // 
            this.txt_make_str_Model.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_make_str_Model.Location = new System.Drawing.Point(152, 19);
            this.txt_make_str_Model.Name = "txt_make_str_Model";
            this.txt_make_str_Model.Size = new System.Drawing.Size(71, 21);
            this.txt_make_str_Model.TabIndex = 7;
            this.txt_make_str_Model.Text = "DESIGN";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(19, 22);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(67, 13);
            this.label17.TabIndex = 6;
            this.label17.Text = "Model Name";
            // 
            // frm_Process_Terrain_Survey_data
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 450);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_process);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frm_Process_Terrain_Survey_data";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Process Terrain Survey Data";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_process;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txt_ele_model;
        private System.Windows.Forms.TextBox txt_ele_inc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_ele_string;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_sec_model;
        private System.Windows.Forms.TextBox txt_sec_inc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_sec_string;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_pri_model;
        private System.Windows.Forms.TextBox txt_pri_inc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_pri_string;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txt_string;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_model;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btn_browse;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_left_ho;
        private System.Windows.Forms.TextBox txt_right_ho;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_chn_interval;
        private System.Windows.Forms.TextBox txt_sc;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt_make_str_String;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txt_make_str_Model;
        private System.Windows.Forms.Label label17;
        public System.Windows.Forms.TextBox txt_select_survey_data;
    }
}