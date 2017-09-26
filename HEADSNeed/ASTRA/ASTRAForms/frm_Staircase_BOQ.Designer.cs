namespace HEADSNeed.ASTRA.ASTRAForms
{
    partial class frm_Staircase_BOQ
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Staircase_BOQ));
            this.dgv_columns = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_Modify = new System.Windows.Forms.Button();
            this.btn_oprn_report = new System.Windows.Forms.Button();
            this.chk_sele_all = new System.Windows.Forms.CheckBox();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_columns)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_columns
            // 
            this.dgv_columns.AllowUserToAddRows = false;
            this.dgv_columns.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_columns.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_columns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_columns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column5,
            this.Column3,
            this.Column26,
            this.Column6,
            this.Column7,
            this.Column4,
            this.Column11,
            this.Column10});
            this.dgv_columns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_columns.Location = new System.Drawing.Point(0, 0);
            this.dgv_columns.Name = "dgv_columns";
            this.dgv_columns.RowHeadersWidth = 27;
            this.dgv_columns.Size = new System.Drawing.Size(728, 427);
            this.dgv_columns.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.Frozen = true;
            this.Column1.HeaderText = "Select";
            this.Column1.Name = "Column1";
            this.Column1.Width = 50;
            // 
            // Column2
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Column2.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column2.Frozen = true;
            this.Column2.HeaderText = "Floor Level (m)";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 90;
            // 
            // Column5
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column5.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column5.Frozen = true;
            this.Column5.HeaderText = "Flight Nos. ";
            this.Column5.Name = "Column5";
            this.Column5.Width = 70;
            // 
            // Column3
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Column3.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column3.HeaderText = "Height of Floor (mm)";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 90;
            // 
            // Column26
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column26.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column26.HeaderText = "Width of Stair Flight Slab (mm)";
            this.Column26.Name = "Column26";
            // 
            // Column6
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column6.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column6.HeaderText = "Width of Landing Slab (mm)";
            this.Column6.Name = "Column6";
            this.Column6.Width = 70;
            // 
            // Column7
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column7.DefaultCellStyle = dataGridViewCellStyle7;
            this.Column7.HeaderText = "Thickness of Stair Waist Slab (mm)";
            this.Column7.Name = "Column7";
            this.Column7.Width = 70;
            // 
            // Column4
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle8;
            this.Column4.HeaderText = "Main Bar Dia (mm)";
            this.Column4.Name = "Column4";
            this.Column4.Width = 80;
            // 
            // Column11
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column11.DefaultCellStyle = dataGridViewCellStyle9;
            this.Column11.HeaderText = "Transverse Bar Dia (mm)";
            this.Column11.Name = "Column11";
            this.Column11.Width = 80;
            // 
            // Column10
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Column10.DefaultCellStyle = dataGridViewCellStyle10;
            this.Column10.HeaderText = "Design Result";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 90;
            // 
            // btn_Modify
            // 
            this.btn_Modify.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_Modify.Location = new System.Drawing.Point(169, 445);
            this.btn_Modify.Name = "btn_Modify";
            this.btn_Modify.Size = new System.Drawing.Size(141, 41);
            this.btn_Modify.TabIndex = 24;
            this.btn_Modify.Text = "Modify Values in Selected Members";
            this.btn_Modify.UseVisualStyleBackColor = true;
            this.btn_Modify.Click += new System.EventHandler(this.btn_Modify_Click);
            // 
            // btn_oprn_report
            // 
            this.btn_oprn_report.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_oprn_report.Location = new System.Drawing.Point(442, 445);
            this.btn_oprn_report.Name = "btn_oprn_report";
            this.btn_oprn_report.Size = new System.Drawing.Size(141, 41);
            this.btn_oprn_report.TabIndex = 22;
            this.btn_oprn_report.Text = "Open Reports for Selected Members";
            this.btn_oprn_report.UseVisualStyleBackColor = true;
            this.btn_oprn_report.Click += new System.EventHandler(this.btn_oprn_report_Click);
            // 
            // chk_sele_all
            // 
            this.chk_sele_all.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.chk_sele_all.AutoSize = true;
            this.chk_sele_all.Checked = true;
            this.chk_sele_all.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_sele_all.Location = new System.Drawing.Point(84, 458);
            this.chk_sele_all.Name = "chk_sele_all";
            this.chk_sele_all.Size = new System.Drawing.Size(79, 17);
            this.chk_sele_all.TabIndex = 21;
            this.chk_sele_all.Text = "Select All";
            this.chk_sele_all.UseVisualStyleBackColor = true;
            this.chk_sele_all.Click += new System.EventHandler(this.cmb_sele_all_CheckedChanged);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_cancel.Location = new System.Drawing.Point(603, 445);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(122, 41);
            this.btn_cancel.TabIndex = 20;
            this.btn_cancel.Text = "Close";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_OK.Location = new System.Drawing.Point(316, 445);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(120, 41);
            this.btn_OK.TabIndex = 19;
            this.btn_OK.Text = "Process Design";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.dgv_columns);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(728, 427);
            this.panel1.TabIndex = 18;
            // 
            // frm_Staircase_BOQ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 498);
            this.Controls.Add(this.btn_Modify);
            this.Controls.Add(this.btn_oprn_report);
            this.Controls.Add(this.chk_sele_all);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_Staircase_BOQ";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Staircase Design";
            this.Load += new System.EventHandler(this.frm_Staircase_BOQ_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_columns)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_columns;
        private System.Windows.Forms.Button btn_Modify;
        private System.Windows.Forms.Button btn_oprn_report;
        private System.Windows.Forms.CheckBox chk_sele_all;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column26;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
    }
}