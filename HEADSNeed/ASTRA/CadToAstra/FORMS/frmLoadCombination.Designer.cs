namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    partial class frmLoadCombination
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_remove_all = new System.Windows.Forms.Button();
            this.txt_load_name = new System.Windows.Forms.TextBox();
            this.btn_remove = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_insert_all = new System.Windows.Forms.Button();
            this.txt_load_no = new System.Windows.Forms.TextBox();
            this.btn_insert = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv_combinations = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.lst_loadcases = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_add = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_combinations)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btn_remove_all);
            this.groupBox1.Controls.Add(this.txt_load_name);
            this.groupBox1.Controls.Add(this.btn_remove);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btn_insert_all);
            this.groupBox1.Controls.Add(this.txt_load_no);
            this.groupBox1.Controls.Add(this.btn_insert);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dgv_combinations);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lst_loadcases);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(489, 246);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btn_remove_all
            // 
            this.btn_remove_all.Location = new System.Drawing.Point(237, 188);
            this.btn_remove_all.Name = "btn_remove_all";
            this.btn_remove_all.Size = new System.Drawing.Size(34, 23);
            this.btn_remove_all.TabIndex = 3;
            this.btn_remove_all.Text = "<<";
            this.btn_remove_all.UseVisualStyleBackColor = true;
            this.btn_remove_all.Click += new System.EventHandler(this.btn_insert_Click);
            // 
            // txt_load_name
            // 
            this.txt_load_name.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_load_name.Location = new System.Drawing.Point(195, 17);
            this.txt_load_name.Name = "txt_load_name";
            this.txt_load_name.Size = new System.Drawing.Size(281, 21);
            this.txt_load_name.TabIndex = 1;
            // 
            // btn_remove
            // 
            this.btn_remove.Location = new System.Drawing.Point(237, 159);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(34, 23);
            this.btn_remove.TabIndex = 3;
            this.btn_remove.Text = "<";
            this.btn_remove.UseVisualStyleBackColor = true;
            this.btn_remove.Click += new System.EventHandler(this.btn_insert_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(150, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "NAME";
            // 
            // btn_insert_all
            // 
            this.btn_insert_all.Location = new System.Drawing.Point(237, 112);
            this.btn_insert_all.Name = "btn_insert_all";
            this.btn_insert_all.Size = new System.Drawing.Size(34, 23);
            this.btn_insert_all.TabIndex = 3;
            this.btn_insert_all.Text = ">>";
            this.btn_insert_all.UseVisualStyleBackColor = true;
            this.btn_insert_all.Click += new System.EventHandler(this.btn_insert_Click);
            // 
            // txt_load_no
            // 
            this.txt_load_no.Location = new System.Drawing.Point(65, 17);
            this.txt_load_no.Name = "txt_load_no";
            this.txt_load_no.Size = new System.Drawing.Size(56, 21);
            this.txt_load_no.TabIndex = 1;
            // 
            // btn_insert
            // 
            this.btn_insert.Location = new System.Drawing.Point(237, 83);
            this.btn_insert.Name = "btn_insert";
            this.btn_insert.Size = new System.Drawing.Size(34, 23);
            this.btn_insert.TabIndex = 3;
            this.btn_insert.Text = ">";
            this.btn_insert.UseVisualStyleBackColor = true;
            this.btn_insert.Click += new System.EventHandler(this.btn_insert_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Load No";
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
            this.dgv_combinations.Location = new System.Drawing.Point(281, 74);
            this.dgv_combinations.Name = "dgv_combinations";
            this.dgv_combinations.RowHeadersWidth = 20;
            this.dgv_combinations.Size = new System.Drawing.Size(195, 160);
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Available Load Cases :";
            // 
            // lst_loadcases
            // 
            this.lst_loadcases.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lst_loadcases.FormattingEnabled = true;
            this.lst_loadcases.Location = new System.Drawing.Point(9, 74);
            this.lst_loadcases.Name = "lst_loadcases";
            this.lst_loadcases.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lst_loadcases.Size = new System.Drawing.Size(215, 158);
            this.lst_loadcases.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(278, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Load Definitions";
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(203, 264);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(106, 26);
            this.btn_add.TabIndex = 4;
            this.btn_add.Text = "Add Data";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(233, 143);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Delete";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(240, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Add";
            // 
            // frmLoadCombination
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 293);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLoadCombination";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Load Combination";
            this.Load += new System.EventHandler(this.frmLoadCombination_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_combinations)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_load_name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lst_loadcases;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgv_combinations;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_insert;
        private System.Windows.Forms.Button btn_insert_all;
        private System.Windows.Forms.Button btn_remove;
        private System.Windows.Forms.Button btn_remove_all;
        private System.Windows.Forms.Button btn_add;
        public System.Windows.Forms.TextBox txt_load_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
    }
}