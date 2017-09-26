namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    partial class frmTimeHistory
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
            this.label12 = new System.Windows.Forms.Label();
            this.chkRz = new System.Windows.Forms.CheckBox();
            this.chkRy = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkRx = new System.Windows.Forms.CheckBox();
            this.chkTz = new System.Windows.Forms.CheckBox();
            this.chkTy = new System.Windows.Forms.CheckBox();
            this.chkTx = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.epAstra = new System.Windows.Forms.ErrorProvider(this.components);
            this.grbNodalConstraint = new System.Windows.Forms.GroupBox();
            this.txtNodeNumbers = new System.Windows.Forms.TextBox();
            this.chkNodalConstraint = new System.Windows.Forms.CheckBox();
            this.txtMemberNumbers = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.rbtnEnd = new System.Windows.Forms.RadioButton();
            this.grbMemberStress = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbtnStart = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtTimeFunction = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTimeValues = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtScaleFactor = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtXDivision = new System.Windows.Forms.TextBox();
            this.cmbGroundMotion = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDampingFactor = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtStepInterval = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPrintInterval = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTimeSteps = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTotalFrequencies = new System.Windows.Forms.TextBox();
            this.ttAstra = new System.Windows.Forms.ToolTip(this.components);
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epAstra)).BeginInit();
            this.grbNodalConstraint.SuspendLayout();
            this.grbMemberStress.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(14, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(230, 13);
            this.label12.TabIndex = 33;
            this.label12.Text = "Node Numbers ( separated by comma)";
            // 
            // chkRz
            // 
            this.chkRz.AutoSize = true;
            this.chkRz.Location = new System.Drawing.Point(278, 19);
            this.chkRz.Name = "chkRz";
            this.chkRz.Size = new System.Drawing.Size(42, 17);
            this.chkRz.TabIndex = 5;
            this.chkRz.Text = "RZ";
            this.chkRz.UseVisualStyleBackColor = true;
            // 
            // chkRy
            // 
            this.chkRy.AutoSize = true;
            this.chkRy.Location = new System.Drawing.Point(223, 19);
            this.chkRy.Name = "chkRy";
            this.chkRy.Size = new System.Drawing.Size(41, 17);
            this.chkRy.TabIndex = 4;
            this.chkRy.Text = "RY";
            this.chkRy.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkRz);
            this.groupBox1.Controls.Add(this.chkRy);
            this.groupBox1.Controls.Add(this.chkRx);
            this.groupBox1.Controls.Add(this.chkTz);
            this.groupBox1.Controls.Add(this.chkTy);
            this.groupBox1.Controls.Add(this.chkTx);
            this.groupBox1.Location = new System.Drawing.Point(19, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(338, 49);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Movement";
            // 
            // chkRx
            // 
            this.chkRx.AutoSize = true;
            this.chkRx.Checked = true;
            this.chkRx.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRx.Location = new System.Drawing.Point(168, 19);
            this.chkRx.Name = "chkRx";
            this.chkRx.Size = new System.Drawing.Size(42, 17);
            this.chkRx.TabIndex = 3;
            this.chkRx.Text = "RX";
            this.chkRx.UseVisualStyleBackColor = true;
            // 
            // chkTz
            // 
            this.chkTz.AutoSize = true;
            this.chkTz.Checked = true;
            this.chkTz.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTz.Location = new System.Drawing.Point(114, 19);
            this.chkTz.Name = "chkTz";
            this.chkTz.Size = new System.Drawing.Size(41, 17);
            this.chkTz.TabIndex = 2;
            this.chkTz.Text = "TZ";
            this.chkTz.UseVisualStyleBackColor = true;
            // 
            // chkTy
            // 
            this.chkTy.AutoSize = true;
            this.chkTy.Location = new System.Drawing.Point(61, 19);
            this.chkTy.Name = "chkTy";
            this.chkTy.Size = new System.Drawing.Size(40, 17);
            this.chkTy.TabIndex = 1;
            this.chkTy.Text = "TY";
            this.chkTy.UseVisualStyleBackColor = true;
            // 
            // chkTx
            // 
            this.chkTx.AutoSize = true;
            this.chkTx.Location = new System.Drawing.Point(7, 19);
            this.chkTx.Name = "chkTx";
            this.chkTx.Size = new System.Drawing.Size(41, 17);
            this.chkTx.TabIndex = 0;
            this.chkTx.Text = "TX";
            this.chkTx.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(9, 248);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "Time Function";
            // 
            // epAstra
            // 
            this.epAstra.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
            this.epAstra.ContainerControl = this;
            this.epAstra.RightToLeft = true;
            // 
            // grbNodalConstraint
            // 
            this.grbNodalConstraint.Controls.Add(this.label12);
            this.grbNodalConstraint.Controls.Add(this.groupBox1);
            this.grbNodalConstraint.Controls.Add(this.txtNodeNumbers);
            this.grbNodalConstraint.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbNodalConstraint.Location = new System.Drawing.Point(247, 45);
            this.grbNodalConstraint.Name = "grbNodalConstraint";
            this.grbNodalConstraint.Size = new System.Drawing.Size(384, 98);
            this.grbNodalConstraint.TabIndex = 37;
            this.grbNodalConstraint.TabStop = false;
            this.grbNodalConstraint.Text = "Nodal Constraint DOF";
            // 
            // txtNodeNumbers
            // 
            this.txtNodeNumbers.Location = new System.Drawing.Point(250, 13);
            this.txtNodeNumbers.Multiline = true;
            this.txtNodeNumbers.Name = "txtNodeNumbers";
            this.txtNodeNumbers.Size = new System.Drawing.Size(106, 20);
            this.txtNodeNumbers.TabIndex = 0;
            this.txtNodeNumbers.Text = "5,9";
            // 
            // chkNodalConstraint
            // 
            this.chkNodalConstraint.AutoSize = true;
            this.chkNodalConstraint.Checked = true;
            this.chkNodalConstraint.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNodalConstraint.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkNodalConstraint.Location = new System.Drawing.Point(247, 21);
            this.chkNodalConstraint.Name = "chkNodalConstraint";
            this.chkNodalConstraint.Size = new System.Drawing.Size(129, 18);
            this.chkNodalConstraint.TabIndex = 32;
            this.chkNodalConstraint.Text = "Nodal Constraint DOF";
            this.chkNodalConstraint.UseVisualStyleBackColor = true;
            this.chkNodalConstraint.CheckedChanged += new System.EventHandler(this.chkNodalConstraint_CheckedChanged);
            // 
            // txtMemberNumbers
            // 
            this.txtMemberNumbers.Location = new System.Drawing.Point(9, 33);
            this.txtMemberNumbers.Multiline = true;
            this.txtMemberNumbers.Name = "txtMemberNumbers";
            this.txtMemberNumbers.Size = new System.Drawing.Size(353, 20);
            this.txtMemberNumbers.TabIndex = 0;
            this.txtMemberNumbers.Text = "1,2,3,4,5,6,7,8";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(327, 296);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 33);
            this.btnCancel.TabIndex = 36;
            this.btnCancel.Text = "FINISH";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // rbtnEnd
            // 
            this.rbtnEnd.AutoSize = true;
            this.rbtnEnd.Location = new System.Drawing.Point(152, 19);
            this.rbtnEnd.Name = "rbtnEnd";
            this.rbtnEnd.Size = new System.Drawing.Size(49, 17);
            this.rbtnEnd.TabIndex = 1;
            this.rbtnEnd.Text = "END";
            this.rbtnEnd.UseVisualStyleBackColor = true;
            // 
            // grbMemberStress
            // 
            this.grbMemberStress.Controls.Add(this.label13);
            this.grbMemberStress.Controls.Add(this.groupBox4);
            this.grbMemberStress.Controls.Add(this.txtMemberNumbers);
            this.grbMemberStress.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbMemberStress.Location = new System.Drawing.Point(247, 182);
            this.grbMemberStress.Name = "grbMemberStress";
            this.grbMemberStress.Size = new System.Drawing.Size(384, 108);
            this.grbMemberStress.TabIndex = 34;
            this.grbMemberStress.TabStop = false;
            this.grbMemberStress.Text = "Member Stress Component";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 17);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(247, 13);
            this.label13.TabIndex = 33;
            this.label13.Text = "Member Numbers ( separated by comma)";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbtnEnd);
            this.groupBox4.Controls.Add(this.rbtnStart);
            this.groupBox4.Location = new System.Drawing.Point(9, 59);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(353, 43);
            this.groupBox4.TabIndex = 30;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Member End";
            // 
            // rbtnStart
            // 
            this.rbtnStart.AutoSize = true;
            this.rbtnStart.Checked = true;
            this.rbtnStart.Location = new System.Drawing.Point(21, 19);
            this.rbtnStart.Name = "rbtnStart";
            this.rbtnStart.Size = new System.Drawing.Size(62, 17);
            this.rbtnStart.TabIndex = 0;
            this.rbtnStart.TabStop = true;
            this.rbtnStart.Text = "START";
            this.rbtnStart.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.txtTimeFunction);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.txtTimeValues);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.txtScaleFactor);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.txtXDivision);
            this.groupBox5.Controls.Add(this.cmbGroundMotion);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.txtDampingFactor);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.txtStepInterval);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.txtPrintInterval);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.txtTimeSteps);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.txtTotalFrequencies);
            this.groupBox5.Location = new System.Drawing.Point(3, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(232, 278);
            this.groupBox5.TabIndex = 33;
            this.groupBox5.TabStop = false;
            // 
            // txtTimeFunction
            // 
            this.txtTimeFunction.Location = new System.Drawing.Point(124, 245);
            this.txtTimeFunction.Name = "txtTimeFunction";
            this.txtTimeFunction.Size = new System.Drawing.Size(100, 21);
            this.txtTimeFunction.TabIndex = 9;
            this.txtTimeFunction.Text = "0.0,1.0,-1.0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(9, 222);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Time Values";
            // 
            // txtTimeValues
            // 
            this.txtTimeValues.Location = new System.Drawing.Point(124, 219);
            this.txtTimeValues.Name = "txtTimeValues";
            this.txtTimeValues.Size = new System.Drawing.Size(100, 21);
            this.txtTimeValues.TabIndex = 8;
            this.txtTimeValues.Text = "0.0,10.0,30.0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(9, 196);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Scale Factor";
            // 
            // txtScaleFactor
            // 
            this.txtScaleFactor.Location = new System.Drawing.Point(124, 193);
            this.txtScaleFactor.Name = "txtScaleFactor";
            this.txtScaleFactor.Size = new System.Drawing.Size(100, 21);
            this.txtScaleFactor.TabIndex = 7;
            this.txtScaleFactor.Text = "1000";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(9, 170);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "X Division";
            // 
            // txtXDivision
            // 
            this.txtXDivision.Location = new System.Drawing.Point(124, 167);
            this.txtXDivision.Name = "txtXDivision";
            this.txtXDivision.Size = new System.Drawing.Size(100, 21);
            this.txtXDivision.TabIndex = 6;
            this.txtXDivision.Text = "3";
            // 
            // cmbGroundMotion
            // 
            this.cmbGroundMotion.FormattingEnabled = true;
            this.cmbGroundMotion.Items.AddRange(new object[] {
            "TX",
            "TY",
            "TZ",
            "RX",
            "RY",
            "RZ"});
            this.cmbGroundMotion.Location = new System.Drawing.Point(124, 140);
            this.cmbGroundMotion.Name = "cmbGroundMotion";
            this.cmbGroundMotion.Size = new System.Drawing.Size(100, 21);
            this.cmbGroundMotion.TabIndex = 5;
            this.cmbGroundMotion.Text = "TZ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(9, 143);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Ground Motion";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Damping Factor";
            // 
            // txtDampingFactor
            // 
            this.txtDampingFactor.Location = new System.Drawing.Point(124, 114);
            this.txtDampingFactor.Name = "txtDampingFactor";
            this.txtDampingFactor.Size = new System.Drawing.Size(100, 21);
            this.txtDampingFactor.TabIndex = 4;
            this.txtDampingFactor.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Step Interval";
            // 
            // txtStepInterval
            // 
            this.txtStepInterval.Location = new System.Drawing.Point(124, 88);
            this.txtStepInterval.Name = "txtStepInterval";
            this.txtStepInterval.Size = new System.Drawing.Size(100, 21);
            this.txtStepInterval.TabIndex = 3;
            this.txtStepInterval.Text = "0.1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Print Interval";
            // 
            // txtPrintInterval
            // 
            this.txtPrintInterval.Location = new System.Drawing.Point(124, 62);
            this.txtPrintInterval.Name = "txtPrintInterval";
            this.txtPrintInterval.Size = new System.Drawing.Size(100, 21);
            this.txtPrintInterval.TabIndex = 2;
            this.txtPrintInterval.Text = "10";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Time Steps";
            // 
            // txtTimeSteps
            // 
            this.txtTimeSteps.Location = new System.Drawing.Point(124, 36);
            this.txtTimeSteps.Name = "txtTimeSteps";
            this.txtTimeSteps.Size = new System.Drawing.Size(100, 21);
            this.txtTimeSteps.TabIndex = 1;
            this.txtTimeSteps.Text = "200";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Total Frequencies";
            // 
            // txtTotalFrequencies
            // 
            this.txtTotalFrequencies.Location = new System.Drawing.Point(124, 10);
            this.txtTotalFrequencies.Name = "txtTotalFrequencies";
            this.txtTotalFrequencies.Size = new System.Drawing.Size(100, 21);
            this.txtTotalFrequencies.TabIndex = 0;
            this.txtTotalFrequencies.Text = "8";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(202, 296);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(87, 33);
            this.btnOk.TabIndex = 35;
            this.btnOk.Text = "Add Data";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // frmTimeHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 331);
            this.Controls.Add(this.grbNodalConstraint);
            this.Controls.Add(this.chkNodalConstraint);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grbMemberStress);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.btnOk);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTimeHistory";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TIME HISTORY ANALYSIS";
            this.Load += new System.EventHandler(this.frmTimeHistory_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epAstra)).EndInit();
            this.grbNodalConstraint.ResumeLayout(false);
            this.grbNodalConstraint.PerformLayout();
            this.grbMemberStress.ResumeLayout(false);
            this.grbMemberStress.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox chkRz;
        private System.Windows.Forms.CheckBox chkRy;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkRx;
        private System.Windows.Forms.CheckBox chkTz;
        private System.Windows.Forms.CheckBox chkTy;
        private System.Windows.Forms.CheckBox chkTx;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ErrorProvider epAstra;
        private System.Windows.Forms.GroupBox grbNodalConstraint;
        private System.Windows.Forms.TextBox txtNodeNumbers;
        private System.Windows.Forms.CheckBox chkNodalConstraint;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox grbMemberStress;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbtnEnd;
        private System.Windows.Forms.RadioButton rbtnStart;
        private System.Windows.Forms.TextBox txtMemberNumbers;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtTimeFunction;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTimeValues;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtScaleFactor;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtXDivision;
        private System.Windows.Forms.ComboBox cmbGroundMotion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDampingFactor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtStepInterval;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPrintInterval;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTimeSteps;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTotalFrequencies;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ToolTip ttAstra;
    }
}