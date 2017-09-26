namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    partial class frmJointWeight
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_fy = new System.Windows.Forms.TextBox();
            this.txt_joint_number = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_str_wgt = new System.Windows.Forms.Button();
            this.btn_jload_add = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.dgv_str_jnt_wgt = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_factor = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_wall_uwgt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_str_jnt_wgt)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label1);
            this.groupBox8.Controls.Add(this.txt_fy);
            this.groupBox8.Controls.Add(this.txt_joint_number);
            this.groupBox8.Controls.Add(this.label8);
            this.groupBox8.Location = new System.Drawing.Point(14, 6);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.groupBox8.Size = new System.Drawing.Size(319, 69);
            this.groupBox8.TabIndex = 3;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Joint Weight";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 48);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Joint Weight :";
            // 
            // txt_fy
            // 
            this.txt_fy.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_fy.Location = new System.Drawing.Point(97, 43);
            this.txt_fy.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.txt_fy.Name = "txt_fy";
            this.txt_fy.Size = new System.Drawing.Size(61, 21);
            this.txt_fy.TabIndex = 2;
            this.txt_fy.Text = "0.0";
            this.txt_fy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_joint_number
            // 
            this.txt_joint_number.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_joint_number.Location = new System.Drawing.Point(97, 17);
            this.txt_joint_number.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.txt_joint_number.Name = "txt_joint_number";
            this.txt_joint_number.Size = new System.Drawing.Size(212, 21);
            this.txt_joint_number.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 20);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Joint No:";
            // 
            // btn_str_wgt
            // 
            this.btn_str_wgt.Location = new System.Drawing.Point(47, 82);
            this.btn_str_wgt.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btn_str_wgt.Name = "btn_str_wgt";
            this.btn_str_wgt.Size = new System.Drawing.Size(205, 25);
            this.btn_str_wgt.TabIndex = 50;
            this.btn_str_wgt.Text = "Calculate Structure Joint Weight";
            this.btn_str_wgt.UseVisualStyleBackColor = true;
            this.btn_str_wgt.Click += new System.EventHandler(this.btn_str_wgt_Click);
            // 
            // btn_jload_add
            // 
            this.btn_jload_add.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_jload_add.Location = new System.Drawing.Point(27, 78);
            this.btn_jload_add.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btn_jload_add.Name = "btn_jload_add";
            this.btn_jload_add.Size = new System.Drawing.Size(134, 25);
            this.btn_jload_add.TabIndex = 4;
            this.btn_jload_add.Text = "Add Load";
            this.btn_jload_add.UseVisualStyleBackColor = true;
            this.btn_jload_add.Click += new System.EventHandler(this.btn_jload_add_Click);
            // 
            // btn_close
            // 
            this.btn_close.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_close.Location = new System.Drawing.Point(170, 78);
            this.btn_close.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(127, 25);
            this.btn_close.TabIndex = 5;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // dgv_str_jnt_wgt
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_str_jnt_wgt.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_str_jnt_wgt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_str_jnt_wgt.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dgv_str_jnt_wgt.Location = new System.Drawing.Point(47, 113);
            this.dgv_str_jnt_wgt.Name = "dgv_str_jnt_wgt";
            this.dgv_str_jnt_wgt.Size = new System.Drawing.Size(205, 177);
            this.dgv_str_jnt_wgt.TabIndex = 51;
            // 
            // Column1
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.HeaderText = "Joint No";
            this.Column1.Name = "Column1";
            this.Column1.Width = 60;
            // 
            // Column2
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column2.HeaderText = "Weight";
            this.Column2.Name = "Column2";
            this.Column2.Width = 80;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 26);
            this.label2.TabIndex = 52;
            this.label2.Text = "Factor for opening in the \r\nwall load for Joint Weight";
            // 
            // txt_factor
            // 
            this.txt_factor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_factor.Location = new System.Drawing.Point(176, 22);
            this.txt_factor.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.txt_factor.Name = "txt_factor";
            this.txt_factor.Size = new System.Drawing.Size(56, 21);
            this.txt_factor.TabIndex = 53;
            this.txt_factor.Text = "0.60";
            this.txt_factor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 13);
            this.label3.TabIndex = 52;
            this.label3.Text = "Unit Weight of Wall Material";
            // 
            // txt_wall_uwgt
            // 
            this.txt_wall_uwgt.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_wall_uwgt.Location = new System.Drawing.Point(176, 55);
            this.txt_wall_uwgt.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.txt_wall_uwgt.Name = "txt_wall_uwgt";
            this.txt_wall_uwgt.Size = new System.Drawing.Size(56, 21);
            this.txt_wall_uwgt.TabIndex = 53;
            this.txt_wall_uwgt.Text = "20.0";
            this.txt_wall_uwgt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(240, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 54;
            this.label4.Text = "kN/Cu.m";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dgv_str_jnt_wgt);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btn_str_wgt);
            this.groupBox1.Controls.Add(this.txt_factor);
            this.groupBox1.Controls.Add(this.txt_wall_uwgt);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(14, 81);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(302, 296);
            this.groupBox1.TabIndex = 55;
            this.groupBox1.TabStop = false;
            // 
            // frmJointWeight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 110);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.btn_jload_add);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmJointWeight";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Joint Weight";
            this.Load += new System.EventHandler(this.frmJointWeight_Load);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_str_jnt_wgt)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_fy;
        private System.Windows.Forms.TextBox txt_joint_number;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_jload_add;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_str_wgt;
        private System.Windows.Forms.DataGridView dgv_str_jnt_wgt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_factor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_wall_uwgt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}