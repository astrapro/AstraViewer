namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    partial class frmResponse
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grbPeriods = new System.Windows.Forms.GroupBox();
            this.dgv_acceleration = new System.Windows.Forms.DataGridView();
            this.col_periods = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_acce_disp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_remove_all = new System.Windows.Forms.Button();
            this.txtScaleFactor = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSpectrumPoints = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_remove = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbtnDisplacement = new System.Windows.Forms.RadioButton();
            this.rbtnAcceleration = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtZ = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtY = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtX = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCutOffFrequencies = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTotalFrequencies = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.toolTipAstra = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.grbPeriods.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_acceleration)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grbPeriods);
            this.groupBox1.Controls.Add(this.btn_remove_all);
            this.groupBox1.Controls.Add(this.txtScaleFactor);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtSpectrumPoints);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.btn_remove);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.txtCutOffFrequencies);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtTotalFrequencies);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(17, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(327, 505);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // grbPeriods
            // 
            this.grbPeriods.Controls.Add(this.dgv_acceleration);
            this.grbPeriods.Location = new System.Drawing.Point(55, 198);
            this.grbPeriods.Name = "grbPeriods";
            this.grbPeriods.Size = new System.Drawing.Size(213, 267);
            this.grbPeriods.TabIndex = 13;
            this.grbPeriods.TabStop = false;
            this.grbPeriods.Text = "Period and Acceleration ";
            // 
            // dgv_acceleration
            // 
            this.dgv_acceleration.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_acceleration.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_periods,
            this.col_acce_disp});
            this.dgv_acceleration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_acceleration.Location = new System.Drawing.Point(3, 17);
            this.dgv_acceleration.Name = "dgv_acceleration";
            this.dgv_acceleration.RowHeadersWidth = 27;
            this.dgv_acceleration.Size = new System.Drawing.Size(207, 247);
            this.dgv_acceleration.TabIndex = 19;
            this.dgv_acceleration.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_acceleration_CellEnter);
            this.dgv_acceleration.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_acceleration_CellEnter);
            // 
            // col_periods
            // 
            this.col_periods.HeaderText = "Periods";
            this.col_periods.Name = "col_periods";
            this.col_periods.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.col_periods.Width = 70;
            // 
            // col_acce_disp
            // 
            this.col_acce_disp.HeaderText = "Accelerations";
            this.col_acce_disp.Name = "col_acce_disp";
            this.col_acce_disp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.col_acce_disp.Width = 90;
            // 
            // btn_remove_all
            // 
            this.btn_remove_all.Location = new System.Drawing.Point(176, 471);
            this.btn_remove_all.Name = "btn_remove_all";
            this.btn_remove_all.Size = new System.Drawing.Size(87, 23);
            this.btn_remove_all.TabIndex = 5;
            this.btn_remove_all.Text = "Remove All";
            this.btn_remove_all.UseVisualStyleBackColor = true;
            this.btn_remove_all.Click += new System.EventHandler(this.btn_remove_all_Click);
            // 
            // txtScaleFactor
            // 
            this.txtScaleFactor.Location = new System.Drawing.Point(269, 171);
            this.txtScaleFactor.Name = "txtScaleFactor";
            this.txtScaleFactor.Size = new System.Drawing.Size(44, 21);
            this.txtScaleFactor.TabIndex = 3;
            this.txtScaleFactor.Text = "1.0";
            this.txtScaleFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(187, 174);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Scale Factor";
            // 
            // txtSpectrumPoints
            // 
            this.txtSpectrumPoints.Location = new System.Drawing.Point(119, 171);
            this.txtSpectrumPoints.Name = "txtSpectrumPoints";
            this.txtSpectrumPoints.Size = new System.Drawing.Size(58, 21);
            this.txtSpectrumPoints.TabIndex = 2;
            this.txtSpectrumPoints.Text = "16";
            this.txtSpectrumPoints.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 174);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Spectrum Points";
            // 
            // btn_remove
            // 
            this.btn_remove.Location = new System.Drawing.Point(58, 471);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(87, 23);
            this.btn_remove.TabIndex = 4;
            this.btn_remove.Text = "Remove";
            this.btn_remove.UseVisualStyleBackColor = true;
            this.btn_remove.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbtnDisplacement);
            this.groupBox3.Controls.Add(this.rbtnAcceleration);
            this.groupBox3.Location = new System.Drawing.Point(6, 122);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(307, 43);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Spectrum Type";
            // 
            // rbtnDisplacement
            // 
            this.rbtnDisplacement.AutoSize = true;
            this.rbtnDisplacement.Location = new System.Drawing.Point(160, 19);
            this.rbtnDisplacement.Name = "rbtnDisplacement";
            this.rbtnDisplacement.Size = new System.Drawing.Size(102, 17);
            this.rbtnDisplacement.TabIndex = 10;
            this.rbtnDisplacement.Text = "Displacement";
            this.rbtnDisplacement.UseVisualStyleBackColor = true;
            this.rbtnDisplacement.CheckedChanged += new System.EventHandler(this.rbtnAcceleration_CheckedChanged);
            // 
            // rbtnAcceleration
            // 
            this.rbtnAcceleration.AutoSize = true;
            this.rbtnAcceleration.Checked = true;
            this.rbtnAcceleration.Location = new System.Drawing.Point(52, 20);
            this.rbtnAcceleration.Name = "rbtnAcceleration";
            this.rbtnAcceleration.Size = new System.Drawing.Size(95, 17);
            this.rbtnAcceleration.TabIndex = 0;
            this.rbtnAcceleration.TabStop = true;
            this.rbtnAcceleration.Text = "Acceleration";
            this.rbtnAcceleration.UseVisualStyleBackColor = true;
            this.rbtnAcceleration.CheckedChanged += new System.EventHandler(this.rbtnAcceleration_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtZ);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtY);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtX);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(6, 63);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(307, 53);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Direction Factors";
            // 
            // txtZ
            // 
            this.txtZ.Location = new System.Drawing.Point(243, 24);
            this.txtZ.Name = "txtZ";
            this.txtZ.Size = new System.Drawing.Size(56, 21);
            this.txtZ.TabIndex = 2;
            this.txtZ.Text = "0.0";
            this.txtZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(208, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Z = ";
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(134, 24);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(56, 21);
            this.txtY.TabIndex = 1;
            this.txtY.Text = "0.6667";
            this.txtY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(99, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Y = ";
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(35, 24);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(56, 21);
            this.txtX.TabIndex = 0;
            this.txtX.Text = "1.0";
            this.txtX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "X = ";
            // 
            // txtCutOffFrequencies
            // 
            this.txtCutOffFrequencies.Location = new System.Drawing.Point(150, 36);
            this.txtCutOffFrequencies.Name = "txtCutOffFrequencies";
            this.txtCutOffFrequencies.Size = new System.Drawing.Size(68, 21);
            this.txtCutOffFrequencies.TabIndex = 1;
            this.txtCutOffFrequencies.Text = "10.5";
            this.txtCutOffFrequencies.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "CUTOFF Frequencies";
            // 
            // txtTotalFrequencies
            // 
            this.txtTotalFrequencies.Location = new System.Drawing.Point(150, 9);
            this.txtTotalFrequencies.Name = "txtTotalFrequencies";
            this.txtTotalFrequencies.Size = new System.Drawing.Size(68, 21);
            this.txtTotalFrequencies.TabIndex = 0;
            this.txtTotalFrequencies.Text = "5";
            this.txtTotalFrequencies.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Total Frequencies";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(188, 532);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "FINISH";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(68, 532);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(87, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Add Data";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // frmResponse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 567);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmResponse";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Response Spectrum";
            this.Load += new System.EventHandler(this.frmResponse_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grbPeriods.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_acceleration)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox grbPeriods;
        private System.Windows.Forms.TextBox txtScaleFactor;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSpectrumPoints;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbtnDisplacement;
        private System.Windows.Forms.RadioButton rbtnAcceleration;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtZ;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCutOffFrequencies;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTotalFrequencies;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTipAstra;
        private System.Windows.Forms.DataGridView dgv_acceleration;
        private System.Windows.Forms.Button btn_remove_all;
        private System.Windows.Forms.Button btn_remove;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_periods;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_acce_disp;
    }
}