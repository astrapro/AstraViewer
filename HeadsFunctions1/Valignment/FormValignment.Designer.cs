namespace HeadsFunctions1.Valignment
{
    partial class FormValignment
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
            System.Windows.Forms.GroupBox groupBox1;
            this.dataGridParams_ = new System.Windows.Forms.DataGridView();
            this.btnDelete_ = new System.Windows.Forms.Button();
            this.btnInsert_ = new System.Windows.Forms.Button();
            this.tbStringlabel_ = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbModelName_ = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxDVCL = new System.Windows.Forms.CheckBox();
            this.btnCancel_ = new System.Windows.Forms.Button();
            this.btnApply_ = new System.Windows.Forms.Button();
            this.btnFinish_ = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.tbDefaultVertCurveLen_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.tbEndChainage_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.tbStartChainage_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.tbChainageInterval_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.btnEdit_ = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridParams_)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.btnEdit_);
            groupBox1.Controls.Add(this.dataGridParams_);
            groupBox1.Controls.Add(this.btnDelete_);
            groupBox1.Controls.Add(this.btnInsert_);
            groupBox1.Location = new System.Drawing.Point(12, 96);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(608, 248);
            groupBox1.TabIndex = 12;
            groupBox1.TabStop = false;
            groupBox1.Text = "VIP Parameters";
            // 
            // dataGridParams_
            // 
            this.dataGridParams_.AllowUserToAddRows = false;
            this.dataGridParams_.AllowUserToDeleteRows = false;
            this.dataGridParams_.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridParams_.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridParams_.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dataGridParams_.Location = new System.Drawing.Point(9, 17);
            this.dataGridParams_.MultiSelect = false;
            this.dataGridParams_.Name = "dataGridParams_";
            this.dataGridParams_.RowHeadersWidth = 20;
            this.dataGridParams_.Size = new System.Drawing.Size(590, 196);
            this.dataGridParams_.TabIndex = 0;
            this.dataGridParams_.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridParams__CellValidated);
            this.dataGridParams_.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridParams__CellClick);
            this.dataGridParams_.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridParams__DataError);
            // 
            // btnDelete_
            // 
            this.btnDelete_.Location = new System.Drawing.Point(267, 221);
            this.btnDelete_.Name = "btnDelete_";
            this.btnDelete_.Size = new System.Drawing.Size(75, 23);
            this.btnDelete_.TabIndex = 2;
            this.btnDelete_.Text = "Delete";
            this.btnDelete_.UseVisualStyleBackColor = true;
            this.btnDelete_.Click += new System.EventHandler(this.btnDelete__Click);
            // 
            // btnInsert_
            // 
            this.btnInsert_.Location = new System.Drawing.Point(186, 221);
            this.btnInsert_.Name = "btnInsert_";
            this.btnInsert_.Size = new System.Drawing.Size(75, 23);
            this.btnInsert_.TabIndex = 1;
            this.btnInsert_.Text = "Insert";
            this.btnInsert_.UseVisualStyleBackColor = true;
            this.btnInsert_.Click += new System.EventHandler(this.btnInsert__Click);
            // 
            // tbStringlabel_
            // 
            this.tbStringlabel_.Location = new System.Drawing.Point(309, 9);
            this.tbStringlabel_.MaxLength = 20;
            this.tbStringlabel_.Name = "tbStringlabel_";
            this.tbStringlabel_.Size = new System.Drawing.Size(90, 20);
            this.tbStringlabel_.TabIndex = 3;
            this.tbStringlabel_.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(402, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Chainage Intervale(M)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(239, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "String Label";
            // 
            // tbModelName_
            // 
            this.tbModelName_.Location = new System.Drawing.Point(118, 9);
            this.tbModelName_.MaxLength = 30;
            this.tbModelName_.Name = "tbModelName_";
            this.tbModelName_.Size = new System.Drawing.Size(90, 20);
            this.tbModelName_.TabIndex = 1;
            this.tbModelName_.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Start Chainage(M)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Model Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(212, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "End Chainage(M)";
            // 
            // checkBoxDVCL
            // 
            this.checkBoxDVCL.AutoSize = true;
            this.checkBoxDVCL.Location = new System.Drawing.Point(132, 73);
            this.checkBoxDVCL.Name = "checkBoxDVCL";
            this.checkBoxDVCL.Size = new System.Drawing.Size(176, 17);
            this.checkBoxDVCL.TabIndex = 10;
            this.checkBoxDVCL.Text = "All Vertical Curve Lengths (VCL)";
            this.checkBoxDVCL.UseVisualStyleBackColor = true;
            this.checkBoxDVCL.CheckedChanged += new System.EventHandler(this.checkBoxDVCL_CheckedChanged);
            // 
            // btnCancel_
            // 
            this.btnCancel_.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel_.Location = new System.Drawing.Point(548, 350);
            this.btnCancel_.Name = "btnCancel_";
            this.btnCancel_.Size = new System.Drawing.Size(75, 23);
            this.btnCancel_.TabIndex = 15;
            this.btnCancel_.Text = "Cancel";
            this.btnCancel_.UseVisualStyleBackColor = true;
            this.btnCancel_.Click += new System.EventHandler(this.btnCancel__Click);
            // 
            // btnApply_
            // 
            this.btnApply_.Location = new System.Drawing.Point(455, 350);
            this.btnApply_.Name = "btnApply_";
            this.btnApply_.Size = new System.Drawing.Size(75, 23);
            this.btnApply_.TabIndex = 13;
            this.btnApply_.Text = "Apply";
            this.btnApply_.UseVisualStyleBackColor = true;
            this.btnApply_.Click += new System.EventHandler(this.btnApply__Click);
            // 
            // btnFinish_
            // 
            this.btnFinish_.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnFinish_.Location = new System.Drawing.Point(365, 350);
            this.btnFinish_.Name = "btnFinish_";
            this.btnFinish_.Size = new System.Drawing.Size(75, 23);
            this.btnFinish_.TabIndex = 14;
            this.btnFinish_.Text = "Save+Finish";
            this.btnFinish_.UseVisualStyleBackColor = true;
            this.btnFinish_.Click += new System.EventHandler(this.btnFinish__Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // tbDefaultVertCurveLen_
            // 
            this.tbDefaultVertCurveLen_.Location = new System.Drawing.Point(309, 70);
            this.tbDefaultVertCurveLen_.MaskFormat = "";
            this.tbDefaultVertCurveLen_.MaximumValue = 1.7976931348623157E+308;
            this.tbDefaultVertCurveLen_.MinimumValue = -1.7976931348623157E+308;
            this.tbDefaultVertCurveLen_.Name = "tbDefaultVertCurveLen_";
            this.tbDefaultVertCurveLen_.Size = new System.Drawing.Size(90, 20);
            this.tbDefaultVertCurveLen_.TabIndex = 11;
            this.tbDefaultVertCurveLen_.Text = "150";
            this.tbDefaultVertCurveLen_.Value = 150;
            // 
            // tbEndChainage_
            // 
            this.tbEndChainage_.Location = new System.Drawing.Point(309, 41);
            this.tbEndChainage_.MaskFormat = "";
            this.tbEndChainage_.MaximumValue = 1.7976931348623157E+308;
            this.tbEndChainage_.MinimumValue = -1.7976931348623157E+308;
            this.tbEndChainage_.Name = "tbEndChainage_";
            this.tbEndChainage_.Size = new System.Drawing.Size(90, 20);
            this.tbEndChainage_.TabIndex = 7;
            this.tbEndChainage_.Text = "0";
            this.tbEndChainage_.Value = 0;
            // 
            // tbStartChainage_
            // 
            this.tbStartChainage_.Location = new System.Drawing.Point(117, 37);
            this.tbStartChainage_.MaskFormat = "";
            this.tbStartChainage_.MaximumValue = 1.7976931348623157E+308;
            this.tbStartChainage_.MinimumValue = 0;
            this.tbStartChainage_.Name = "tbStartChainage_";
            this.tbStartChainage_.Size = new System.Drawing.Size(90, 20);
            this.tbStartChainage_.TabIndex = 5;
            this.tbStartChainage_.Text = "0";
            this.tbStartChainage_.Value = 0;
            // 
            // tbChainageInterval_
            // 
            this.tbChainageInterval_.Location = new System.Drawing.Point(514, 40);
            this.tbChainageInterval_.MaskFormat = "";
            this.tbChainageInterval_.MaximumValue = 1.7976931348623157E+308;
            this.tbChainageInterval_.MinimumValue = -1.7976931348623157E+308;
            this.tbChainageInterval_.Name = "tbChainageInterval_";
            this.tbChainageInterval_.Size = new System.Drawing.Size(90, 20);
            this.tbChainageInterval_.TabIndex = 9;
            this.tbChainageInterval_.Text = "5";
            this.tbChainageInterval_.Value = 5;
            // 
            // btnEdit_
            // 
            this.btnEdit_.Location = new System.Drawing.Point(348, 221);
            this.btnEdit_.Name = "btnEdit_";
            this.btnEdit_.Size = new System.Drawing.Size(75, 23);
            this.btnEdit_.TabIndex = 3;
            this.btnEdit_.Text = "Edit";
            this.btnEdit_.UseVisualStyleBackColor = true;
            this.btnEdit_.Click += new System.EventHandler(this.btnEdit__Click);
            // 
            // FormValignment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 382);
            this.Controls.Add(this.btnCancel_);
            this.Controls.Add(this.btnApply_);
            this.Controls.Add(this.btnFinish_);
            this.Controls.Add(groupBox1);
            this.Controls.Add(this.tbDefaultVertCurveLen_);
            this.Controls.Add(this.checkBoxDVCL);
            this.Controls.Add(this.tbEndChainage_);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbStartChainage_);
            this.Controls.Add(this.tbChainageInterval_);
            this.Controls.Add(this.tbStringlabel_);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbModelName_);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormValignment";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Valignment";
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridParams_)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HeadsFunctions1.CustomCtrls.TextBoxDouble tbStartChainage_;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble tbChainageInterval_;
        private System.Windows.Forms.TextBox tbStringlabel_;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbModelName_;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble tbEndChainage_;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxDVCL;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble tbDefaultVertCurveLen_;
        private System.Windows.Forms.Button btnCancel_;
        private System.Windows.Forms.Button btnApply_;
        private System.Windows.Forms.Button btnFinish_;
        private System.Windows.Forms.Button btnDelete_;
        private System.Windows.Forms.Button btnInsert_;
        private System.Windows.Forms.DataGridView dataGridParams_;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Button btnEdit_;
    }
}