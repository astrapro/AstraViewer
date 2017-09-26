namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    partial class frmDefineMovingLoad
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_apply = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_ld_delete = new System.Windows.Forms.Button();
            this.btn_ld_insert = new System.Windows.Forms.Button();
            this.dgv_loads = new System.Windows.Forms.DataGridView();
            this.txt_load_distance = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_load_width = new System.Windows.Forms.TextBox();
            this.txt_lm = new System.Windows.Forms.TextBox();
            this.txt_type = new System.Windows.Forms.TextBox();
            this.btn_new = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lst_pdl = new System.Windows.Forms.ListBox();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_loads)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btn_apply);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.txt_load_width);
            this.groupBox2.Controls.Add(this.txt_lm);
            this.groupBox2.Controls.Add(this.txt_type);
            this.groupBox2.Location = new System.Drawing.Point(4, 188);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(726, 290);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Pre Defined Loads";
            // 
            // btn_apply
            // 
            this.btn_apply.Location = new System.Drawing.Point(141, 124);
            this.btn_apply.Name = "btn_apply";
            this.btn_apply.Size = new System.Drawing.Size(70, 23);
            this.btn_apply.TabIndex = 8;
            this.btn_apply.Text = "Apply";
            this.btn_apply.UseVisualStyleBackColor = true;
            this.btn_apply.Click += new System.EventHandler(this.btn_apply_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Load Width";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Load Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "TYPE";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_ld_delete);
            this.groupBox3.Controls.Add(this.btn_ld_insert);
            this.groupBox3.Controls.Add(this.dgv_loads);
            this.groupBox3.Controls.Add(this.txt_load_distance);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(425, 15);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(269, 264);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Define Loads && Distances ";
            // 
            // btn_ld_delete
            // 
            this.btn_ld_delete.Location = new System.Drawing.Point(138, 236);
            this.btn_ld_delete.Name = "btn_ld_delete";
            this.btn_ld_delete.Size = new System.Drawing.Size(70, 23);
            this.btn_ld_delete.TabIndex = 6;
            this.btn_ld_delete.Text = "Delete";
            this.btn_ld_delete.UseVisualStyleBackColor = true;
            this.btn_ld_delete.Click += new System.EventHandler(this.btn_ld_delete_Click);
            // 
            // btn_ld_insert
            // 
            this.btn_ld_insert.Location = new System.Drawing.Point(61, 236);
            this.btn_ld_insert.Name = "btn_ld_insert";
            this.btn_ld_insert.Size = new System.Drawing.Size(70, 23);
            this.btn_ld_insert.TabIndex = 6;
            this.btn_ld_insert.Text = "Insert";
            this.btn_ld_insert.UseVisualStyleBackColor = true;
            this.btn_ld_insert.Click += new System.EventHandler(this.btn_insert_Click);
            // 
            // dgv_loads
            // 
            this.dgv_loads.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_loads.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dgv_loads.Location = new System.Drawing.Point(6, 20);
            this.dgv_loads.Name = "dgv_loads";
            this.dgv_loads.RowHeadersWidth = 21;
            this.dgv_loads.Size = new System.Drawing.Size(246, 177);
            this.dgv_loads.TabIndex = 6;
            this.dgv_loads.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_loads_CellEnter);
            this.dgv_loads.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_loads_CellEnter);
            // 
            // txt_load_distance
            // 
            this.txt_load_distance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_load_distance.Location = new System.Drawing.Point(158, 203);
            this.txt_load_distance.Name = "txt_load_distance";
            this.txt_load_distance.ReadOnly = true;
            this.txt_load_distance.Size = new System.Drawing.Size(82, 21);
            this.txt_load_distance.TabIndex = 1;
            this.txt_load_distance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 208);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Total Distance";
            // 
            // txt_load_width
            // 
            this.txt_load_width.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_load_width.Location = new System.Drawing.Point(99, 71);
            this.txt_load_width.Name = "txt_load_width";
            this.txt_load_width.Size = new System.Drawing.Size(112, 21);
            this.txt_load_width.TabIndex = 1;
            this.txt_load_width.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_lm
            // 
            this.txt_lm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_lm.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_lm.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_lm.Location = new System.Drawing.Point(99, 43);
            this.txt_lm.Name = "txt_lm";
            this.txt_lm.Size = new System.Drawing.Size(242, 22);
            this.txt_lm.TabIndex = 1;
            this.txt_lm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_type
            // 
            this.txt_type.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_type.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_type.Location = new System.Drawing.Point(99, 15);
            this.txt_type.Name = "txt_type";
            this.txt_type.Size = new System.Drawing.Size(64, 22);
            this.txt_type.TabIndex = 1;
            this.txt_type.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_new
            // 
            this.btn_new.Location = new System.Drawing.Point(298, 484);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(141, 28);
            this.btn_new.TabIndex = 4;
            this.btn_new.Text = "Add Data";
            this.btn_new.UseVisualStyleBackColor = true;
            this.btn_new.Click += new System.EventHandler(this.btn_new_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::HEADSNeed.Properties.Resources.MovingLoads;
            this.pictureBox1.Location = new System.Drawing.Point(4, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(726, 181);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // lst_pdl
            // 
            this.lst_pdl.FormattingEnabled = true;
            this.lst_pdl.Location = new System.Drawing.Point(20, 341);
            this.lst_pdl.Name = "lst_pdl";
            this.lst_pdl.Size = new System.Drawing.Size(332, 121);
            this.lst_pdl.TabIndex = 6;
            // 
            // Column1
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Column1.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column1.HeaderText = "SL.No";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 50;
            // 
            // Column2
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column2.HeaderText = "Loads";
            this.Column2.Name = "Column2";
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 90;
            // 
            // Column3
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column3.HeaderText = "Distances";
            this.Column3.Name = "Column3";
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 70;
            // 
            // frmDefineMovingLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 517);
            this.Controls.Add(this.lst_pdl);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_new);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDefineMovingLoad";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Define Moving Load Data";
            this.Load += new System.EventHandler(this.frmDefineMovingLoad_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_loads)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_new;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_load_width;
        private System.Windows.Forms.TextBox txt_lm;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.TextBox txt_type;
        private System.Windows.Forms.DataGridView dgv_loads;
        private System.Windows.Forms.Button btn_ld_delete;
        private System.Windows.Forms.Button btn_ld_insert;
        private System.Windows.Forms.ListBox lst_pdl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_apply;
        private System.Windows.Forms.TextBox txt_load_distance;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}