namespace HEADSNeed.ASTRA.ASTRAForms
{
    partial class frm_SeismicAnalysis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_SeismicAnalysis));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_remove_all = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_dir_6 = new System.Windows.Forms.RadioButton();
            this.btn_dir_5 = new System.Windows.Forms.RadioButton();
            this.btn_dir_4 = new System.Windows.Forms.RadioButton();
            this.btn_dir_3 = new System.Windows.Forms.RadioButton();
            this.btn_dir_2 = new System.Windows.Forms.RadioButton();
            this.btn_dir_1 = new System.Windows.Forms.RadioButton();
            this.txt_SC = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_load_name = new System.Windows.Forms.TextBox();
            this.btn_remove = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_insert_all = new System.Windows.Forms.Button();
            this.txt_load_no = new System.Windows.Forms.TextBox();
            this.btn_insert = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dgv_combinations = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.lst_loadcases = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_process = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_create = new System.Windows.Forms.Button();
            this.btn_view = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_combinations)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(425, 473);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 44);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            this.label1.Visible = false;
            // 
            // btn_remove_all
            // 
            this.btn_remove_all.Location = new System.Drawing.Point(244, 241);
            this.btn_remove_all.Name = "btn_remove_all";
            this.btn_remove_all.Size = new System.Drawing.Size(34, 23);
            this.btn_remove_all.TabIndex = 3;
            this.btn_remove_all.Text = "<<";
            this.btn_remove_all.UseVisualStyleBackColor = true;
            this.btn_remove_all.Click += new System.EventHandler(this.btn_insert_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.txt_SC);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btn_remove_all);
            this.groupBox1.Controls.Add(this.txt_load_name);
            this.groupBox1.Controls.Add(this.btn_remove);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btn_insert_all);
            this.groupBox1.Controls.Add(this.txt_load_no);
            this.groupBox1.Controls.Add(this.btn_insert);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dgv_combinations);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lst_loadcases);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(12, 88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(489, 295);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_dir_6);
            this.groupBox2.Controls.Add(this.btn_dir_5);
            this.groupBox2.Controls.Add(this.btn_dir_4);
            this.groupBox2.Controls.Add(this.btn_dir_3);
            this.groupBox2.Controls.Add(this.btn_dir_2);
            this.groupBox2.Controls.Add(this.btn_dir_1);
            this.groupBox2.Location = new System.Drawing.Point(20, 39);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(463, 47);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Direction";
            // 
            // btn_dir_6
            // 
            this.btn_dir_6.AutoSize = true;
            this.btn_dir_6.Location = new System.Drawing.Point(389, 20);
            this.btn_dir_6.Name = "btn_dir_6";
            this.btn_dir_6.Size = new System.Drawing.Size(68, 17);
            this.btn_dir_6.TabIndex = 0;
            this.btn_dir_6.Text = "+Z / -Z";
            this.btn_dir_6.UseVisualStyleBackColor = true;
            // 
            // btn_dir_5
            // 
            this.btn_dir_5.AutoSize = true;
            this.btn_dir_5.Location = new System.Drawing.Point(315, 20);
            this.btn_dir_5.Name = "btn_dir_5";
            this.btn_dir_5.Size = new System.Drawing.Size(68, 17);
            this.btn_dir_5.TabIndex = 0;
            this.btn_dir_5.Text = "+X / -X";
            this.btn_dir_5.UseVisualStyleBackColor = true;
            // 
            // btn_dir_4
            // 
            this.btn_dir_4.AutoSize = true;
            this.btn_dir_4.Location = new System.Drawing.Point(236, 20);
            this.btn_dir_4.Name = "btn_dir_4";
            this.btn_dir_4.Size = new System.Drawing.Size(68, 17);
            this.btn_dir_4.TabIndex = 0;
            this.btn_dir_4.Text = "-X / +Z";
            this.btn_dir_4.UseVisualStyleBackColor = true;
            // 
            // btn_dir_3
            // 
            this.btn_dir_3.AutoSize = true;
            this.btn_dir_3.Location = new System.Drawing.Point(162, 20);
            this.btn_dir_3.Name = "btn_dir_3";
            this.btn_dir_3.Size = new System.Drawing.Size(68, 17);
            this.btn_dir_3.TabIndex = 0;
            this.btn_dir_3.Text = "+X / -Z";
            this.btn_dir_3.UseVisualStyleBackColor = true;
            // 
            // btn_dir_2
            // 
            this.btn_dir_2.AutoSize = true;
            this.btn_dir_2.Location = new System.Drawing.Point(84, 20);
            this.btn_dir_2.Name = "btn_dir_2";
            this.btn_dir_2.Size = new System.Drawing.Size(64, 17);
            this.btn_dir_2.TabIndex = 0;
            this.btn_dir_2.Text = "-X / -Z";
            this.btn_dir_2.UseVisualStyleBackColor = true;
            // 
            // btn_dir_1
            // 
            this.btn_dir_1.AutoSize = true;
            this.btn_dir_1.Checked = true;
            this.btn_dir_1.Location = new System.Drawing.Point(6, 20);
            this.btn_dir_1.Name = "btn_dir_1";
            this.btn_dir_1.Size = new System.Drawing.Size(72, 17);
            this.btn_dir_1.TabIndex = 0;
            this.btn_dir_1.TabStop = true;
            this.btn_dir_1.Text = "+X / +Z";
            this.btn_dir_1.UseVisualStyleBackColor = true;
            // 
            // txt_SC
            // 
            this.txt_SC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_SC.Location = new System.Drawing.Point(163, 14);
            this.txt_SC.Name = "txt_SC";
            this.txt_SC.Size = new System.Drawing.Size(58, 21);
            this.txt_SC.TabIndex = 8;
            this.txt_SC.Text = "0.18";
            this.txt_SC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(237, 196);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Delete";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(147, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Seismic Coefficient [SC]";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(244, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Add";
            // 
            // textBox1
            // 
            this.textBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox1.Location = new System.Drawing.Point(426, 269);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(57, 21);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "1.0";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(285, 272);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Seismic Load Factor";
            // 
            // txt_load_name
            // 
            this.txt_load_name.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_load_name.Location = new System.Drawing.Point(202, 92);
            this.txt_load_name.Name = "txt_load_name";
            this.txt_load_name.Size = new System.Drawing.Size(281, 21);
            this.txt_load_name.TabIndex = 1;
            this.txt_load_name.Text = "SEISMIC LOAD";
            // 
            // btn_remove
            // 
            this.btn_remove.Location = new System.Drawing.Point(244, 212);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(34, 23);
            this.btn_remove.TabIndex = 2;
            this.btn_remove.Text = "<";
            this.btn_remove.UseVisualStyleBackColor = true;
            this.btn_remove.Click += new System.EventHandler(this.btn_insert_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(157, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "NAME";
            // 
            // btn_insert_all
            // 
            this.btn_insert_all.Location = new System.Drawing.Point(244, 170);
            this.btn_insert_all.Name = "btn_insert_all";
            this.btn_insert_all.Size = new System.Drawing.Size(34, 23);
            this.btn_insert_all.TabIndex = 1;
            this.btn_insert_all.Text = ">>";
            this.btn_insert_all.UseVisualStyleBackColor = true;
            this.btn_insert_all.Click += new System.EventHandler(this.btn_insert_Click);
            // 
            // txt_load_no
            // 
            this.txt_load_no.Location = new System.Drawing.Point(72, 92);
            this.txt_load_no.Name = "txt_load_no";
            this.txt_load_no.Size = new System.Drawing.Size(56, 21);
            this.txt_load_no.TabIndex = 1;
            // 
            // btn_insert
            // 
            this.btn_insert.Location = new System.Drawing.Point(244, 141);
            this.btn_insert.Name = "btn_insert";
            this.btn_insert.Size = new System.Drawing.Size(34, 23);
            this.btn_insert.TabIndex = 0;
            this.btn_insert.Text = ">";
            this.btn_insert.UseVisualStyleBackColor = true;
            this.btn_insert.Click += new System.EventHandler(this.btn_insert_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Load No";
            // 
            // dgv_combinations
            // 
            this.dgv_combinations.AllowUserToAddRows = false;
            this.dgv_combinations.AllowUserToDeleteRows = false;
            this.dgv_combinations.AllowUserToOrderColumns = true;
            this.dgv_combinations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_combinations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dgv_combinations.Location = new System.Drawing.Point(288, 132);
            this.dgv_combinations.Name = "dgv_combinations";
            this.dgv_combinations.RowHeadersWidth = 20;
            this.dgv_combinations.Size = new System.Drawing.Size(195, 128);
            this.dgv_combinations.TabIndex = 2;
            // 
            // Column1
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Column1.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column1.HeaderText = "Load Cases";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Factor";
            this.Column2.Name = "Column2";
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 70;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Available Load Cases :";
            // 
            // lst_loadcases
            // 
            this.lst_loadcases.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lst_loadcases.FormattingEnabled = true;
            this.lst_loadcases.Location = new System.Drawing.Point(16, 132);
            this.lst_loadcases.Name = "lst_loadcases";
            this.lst_loadcases.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lst_loadcases.Size = new System.Drawing.Size(215, 158);
            this.lst_loadcases.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(285, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Load Definitions";
            // 
            // btn_process
            // 
            this.btn_process.Enabled = false;
            this.btn_process.Location = new System.Drawing.Point(300, 389);
            this.btn_process.Name = "btn_process";
            this.btn_process.Size = new System.Drawing.Size(115, 43);
            this.btn_process.TabIndex = 2;
            this.btn_process.Text = "Process Analysis";
            this.btn_process.UseVisualStyleBackColor = true;
            this.btn_process.Click += new System.EventHandler(this.btn_process_Click);
            // 
            // label11
            // 
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(12, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(489, 76);
            this.label11.TabIndex = 0;
            this.label11.Text = resources.GetString("label11.Text");
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(421, 389);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(80, 43);
            this.btn_cancel.TabIndex = 3;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_create
            // 
            this.btn_create.Location = new System.Drawing.Point(12, 389);
            this.btn_create.Name = "btn_create";
            this.btn_create.Size = new System.Drawing.Size(139, 43);
            this.btn_create.TabIndex = 0;
            this.btn_create.Text = "Create Seismic Analysis Input Data";
            this.btn_create.UseVisualStyleBackColor = true;
            this.btn_create.Click += new System.EventHandler(this.btn_create_Click);
            // 
            // btn_view
            // 
            this.btn_view.Enabled = false;
            this.btn_view.Location = new System.Drawing.Point(158, 389);
            this.btn_view.Name = "btn_view";
            this.btn_view.Size = new System.Drawing.Size(135, 43);
            this.btn_view.TabIndex = 1;
            this.btn_view.Text = "View Seismic Analysis Input Data";
            this.btn_view.UseVisualStyleBackColor = true;
            this.btn_view.Click += new System.EventHandler(this.btn_view_Click);
            // 
            // frm_SeismicAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 437);
            this.Controls.Add(this.btn_view);
            this.Controls.Add(this.btn_create);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_process);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_SeismicAnalysis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Process Analysis for Seismic Load";
            this.Load += new System.EventHandler(this.frm_SeismicAnalysis_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_combinations)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_remove_all;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_load_name;
        private System.Windows.Forms.Button btn_remove;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_insert_all;
        public System.Windows.Forms.TextBox txt_load_no;
        private System.Windows.Forms.Button btn_insert;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgv_combinations;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lst_loadcases;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_process;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_SC;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton btn_dir_2;
        private System.Windows.Forms.RadioButton btn_dir_1;
        private System.Windows.Forms.RadioButton btn_dir_6;
        private System.Windows.Forms.RadioButton btn_dir_5;
        private System.Windows.Forms.RadioButton btn_dir_4;
        private System.Windows.Forms.RadioButton btn_dir_3;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_create;
        private System.Windows.Forms.Button btn_view;
    }
}