namespace HeadsFunctions1.Halignment
{
    partial class FormHipMethod
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHipMethod));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtModelName_ = new System.Windows.Forms.TextBox();
            this.txtStringlabel_ = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxDefaultRead_ = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridParams_ = new System.Windows.Forms.DataGridView();
            this.btnInsert_ = new System.Windows.Forms.Button();
            this.btnEdit_ = new System.Windows.Forms.Button();
            this.btnDelete_ = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel_ = new System.Windows.Forms.Button();
            this.btnSave_ = new System.Windows.Forms.Button();
            this.checkBoxShowDetails_ = new System.Windows.Forms.CheckBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtStartInterval_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.txtChainageInterval_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.txtDefaultRadious_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridParams_)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Model Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Start Chainage(M)";
            // 
            // txtModelName_
            // 
            this.txtModelName_.Location = new System.Drawing.Point(118, 13);
            this.txtModelName_.MaxLength = 30;
            this.txtModelName_.Name = "txtModelName_";
            this.txtModelName_.Size = new System.Drawing.Size(125, 20);
            this.txtModelName_.TabIndex = 3;
            this.txtModelName_.TextChanged += new System.EventHandler(this.txtModelName__TextChanged);
            // 
            // txtStringlabel_
            // 
            this.txtStringlabel_.Location = new System.Drawing.Point(468, 12);
            this.txtStringlabel_.MaxLength = 20;
            this.txtStringlabel_.Name = "txtStringlabel_";
            this.txtStringlabel_.Size = new System.Drawing.Size(125, 20);
            this.txtStringlabel_.TabIndex = 7;
            this.txtStringlabel_.TextChanged += new System.EventHandler(this.txtStringlabel__TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(350, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Chainage Intervale(M)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(398, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "String Label";
            // 
            // checkBoxDefaultRead_
            // 
            this.checkBoxDefaultRead_.AutoSize = true;
            this.checkBoxDefaultRead_.Location = new System.Drawing.Point(135, 66);
            this.checkBoxDefaultRead_.Name = "checkBoxDefaultRead_";
            this.checkBoxDefaultRead_.Size = new System.Drawing.Size(122, 17);
            this.checkBoxDefaultRead_.TabIndex = 9;
            this.checkBoxDefaultRead_.Text = "All Curve Radius (M)";
            this.checkBoxDefaultRead_.UseVisualStyleBackColor = true;
            this.checkBoxDefaultRead_.CheckedChanged += new System.EventHandler(this.checkBoxDefaultRead__CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridParams_);
            this.groupBox1.Controls.Add(this.btnInsert_);
            this.groupBox1.Controls.Add(this.btnEdit_);
            this.groupBox1.Controls.Add(this.btnDelete_);
            this.groupBox1.Location = new System.Drawing.Point(6, 90);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(603, 245);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "HIP Parameters";
            // 
            // dataGridParams_
            // 
            this.dataGridParams_.AllowUserToAddRows = false;
            this.dataGridParams_.AllowUserToDeleteRows = false;
            this.dataGridParams_.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridParams_.Location = new System.Drawing.Point(9, 15);
            this.dataGridParams_.MultiSelect = false;
            this.dataGridParams_.Name = "dataGridParams_";
            this.dataGridParams_.RowHeadersWidth = 20;
            this.dataGridParams_.Size = new System.Drawing.Size(585, 195);
            this.dataGridParams_.TabIndex = 7;
            this.dataGridParams_.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridParams__CellValidated);
            this.dataGridParams_.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridParams__CellClick);
            this.dataGridParams_.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridParams__DataError);
            // 
            // btnInsert_
            // 
            this.btnInsert_.Location = new System.Drawing.Point(174, 216);
            this.btnInsert_.Name = "btnInsert_";
            this.btnInsert_.Size = new System.Drawing.Size(77, 23);
            this.btnInsert_.TabIndex = 6;
            this.btnInsert_.Text = "Insert";
            this.btnInsert_.UseVisualStyleBackColor = true;
            this.btnInsert_.Click += new System.EventHandler(this.btnInsert__Click);
            // 
            // btnEdit_
            // 
            this.btnEdit_.Location = new System.Drawing.Point(263, 216);
            this.btnEdit_.Name = "btnEdit_";
            this.btnEdit_.Size = new System.Drawing.Size(77, 23);
            this.btnEdit_.TabIndex = 5;
            this.btnEdit_.Text = "Edit";
            this.btnEdit_.UseVisualStyleBackColor = true;
            this.btnEdit_.Click += new System.EventHandler(this.btnEdit__Click);
            // 
            // btnDelete_
            // 
            this.btnDelete_.Location = new System.Drawing.Point(352, 216);
            this.btnDelete_.Name = "btnDelete_";
            this.btnDelete_.Size = new System.Drawing.Size(77, 23);
            this.btnDelete_.TabIndex = 3;
            this.btnDelete_.Text = "Delete";
            this.btnDelete_.UseVisualStyleBackColor = true;
            this.btnDelete_.Click += new System.EventHandler(this.btnDelete__Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(439, 344);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(80, 31);
            this.btnApply.TabIndex = 14;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel_
            // 
            this.btnCancel_.Location = new System.Drawing.Point(525, 344);
            this.btnCancel_.Name = "btnCancel_";
            this.btnCancel_.Size = new System.Drawing.Size(80, 31);
            this.btnCancel_.TabIndex = 13;
            this.btnCancel_.Text = "Cancel";
            this.btnCancel_.UseVisualStyleBackColor = true;
            this.btnCancel_.Click += new System.EventHandler(this.btnCancel__Click);
            // 
            // btnSave_
            // 
            this.btnSave_.Location = new System.Drawing.Point(353, 344);
            this.btnSave_.Name = "btnSave_";
            this.btnSave_.Size = new System.Drawing.Size(80, 31);
            this.btnSave_.TabIndex = 12;
            this.btnSave_.Text = "Save+Finish";
            this.btnSave_.UseVisualStyleBackColor = true;
            this.btnSave_.Click += new System.EventHandler(this.btnSave__Click);
            // 
            // checkBoxShowDetails_
            // 
            this.checkBoxShowDetails_.AutoSize = true;
            this.checkBoxShowDetails_.Location = new System.Drawing.Point(15, 351);
            this.checkBoxShowDetails_.Name = "checkBoxShowDetails_";
            this.checkBoxShowDetails_.Size = new System.Drawing.Size(88, 17);
            this.checkBoxShowDetails_.TabIndex = 18;
            this.checkBoxShowDetails_.Text = "Show Details";
            this.checkBoxShowDetails_.UseVisualStyleBackColor = true;
            this.checkBoxShowDetails_.CheckedChanged += new System.EventHandler(this.checkBoxShowDetails__CheckedChanged);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // txtStartInterval_
            // 
            this.txtStartInterval_.Location = new System.Drawing.Point(118, 40);
            this.txtStartInterval_.MaskFormat = "";
            this.txtStartInterval_.MaximumValue = 1.7976931348623157E+308;
            this.txtStartInterval_.MinimumValue = -1.7976931348623157E+308;
            this.txtStartInterval_.Name = "txtStartInterval_";
            this.txtStartInterval_.Size = new System.Drawing.Size(125, 20);
            this.txtStartInterval_.TabIndex = 17;
            this.txtStartInterval_.Text = "0";
            this.txtStartInterval_.Value = 0;
            // 
            // txtChainageInterval_
            // 
            this.txtChainageInterval_.Location = new System.Drawing.Point(468, 39);
            this.txtChainageInterval_.MaskFormat = "";
            this.txtChainageInterval_.MaximumValue = 1.7976931348623157E+308;
            this.txtChainageInterval_.MinimumValue = -1.7976931348623157E+308;
            this.txtChainageInterval_.Name = "txtChainageInterval_";
            this.txtChainageInterval_.Size = new System.Drawing.Size(125, 20);
            this.txtChainageInterval_.TabIndex = 16;
            this.txtChainageInterval_.Text = "5";
            this.txtChainageInterval_.Value = 5;
            // 
            // txtDefaultRadious_
            // 
            this.txtDefaultRadious_.Location = new System.Drawing.Point(258, 64);
            this.txtDefaultRadious_.MaskFormat = "";
            this.txtDefaultRadious_.MaximumValue = 1.7976931348623157E+308;
            this.txtDefaultRadious_.MinimumValue = -1.7976931348623157E+308;
            this.txtDefaultRadious_.Name = "txtDefaultRadious_";
            this.txtDefaultRadious_.Size = new System.Drawing.Size(100, 20);
            this.txtDefaultRadious_.TabIndex = 15;
            this.txtDefaultRadious_.Text = "2000";
            this.txtDefaultRadious_.Value = 2000;
            // 
            // FormHipMethod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 381);
            this.Controls.Add(this.checkBoxShowDetails_);
            this.Controls.Add(this.txtStartInterval_);
            this.Controls.Add(this.txtChainageInterval_);
            this.Controls.Add(this.txtDefaultRadious_);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnCancel_);
            this.Controls.Add(this.btnSave_);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBoxDefaultRead_);
            this.Controls.Add(this.txtStringlabel_);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtModelName_);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormHipMethod";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "HIP Method";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormHipMethod_FormClosed);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridParams_)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtModelName_;
        private System.Windows.Forms.TextBox txtStringlabel_;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxDefaultRead_;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDelete_;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel_;
        private System.Windows.Forms.Button btnSave_;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble txtDefaultRadious_;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble txtChainageInterval_;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble txtStartInterval_;
        private System.Windows.Forms.Button btnInsert_;
        private System.Windows.Forms.Button btnEdit_;
        private System.Windows.Forms.CheckBox checkBoxShowDetails_;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.DataGridView dataGridParams_;
    }
}