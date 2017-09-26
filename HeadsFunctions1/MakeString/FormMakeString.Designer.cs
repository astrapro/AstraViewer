namespace HeadsFunctions1.MakeString
{
    partial class FormMakeString
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
            this.tbStringlabel_ = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbModelName_ = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelChainageInterval_ = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkMasterString_ = new System.Windows.Forms.CheckBox();
            this.btnCancel_ = new System.Windows.Forms.Button();
            this.btnOk_ = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.tbStartChainage_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.tbChainageInterval_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // tbStringlabel_
            // 
            this.tbStringlabel_.Location = new System.Drawing.Point(124, 37);
            this.tbStringlabel_.MaxLength = 20;
            this.tbStringlabel_.Name = "tbStringlabel_";
            this.tbStringlabel_.Size = new System.Drawing.Size(132, 20);
            this.tbStringlabel_.TabIndex = 7;
            this.tbStringlabel_.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "String Label";
            // 
            // tbModelName_
            // 
            this.tbModelName_.Location = new System.Drawing.Point(124, 9);
            this.tbModelName_.MaxLength = 30;
            this.tbModelName_.Name = "tbModelName_";
            this.tbModelName_.Size = new System.Drawing.Size(132, 20);
            this.tbModelName_.TabIndex = 5;
            this.tbModelName_.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Model Name";
            // 
            // labelChainageInterval_
            // 
            this.labelChainageInterval_.AutoSize = true;
            this.labelChainageInterval_.Location = new System.Drawing.Point(7, 96);
            this.labelChainageInterval_.Name = "labelChainageInterval_";
            this.labelChainageInterval_.Size = new System.Drawing.Size(111, 13);
            this.labelChainageInterval_.TabIndex = 12;
            this.labelChainageInterval_.Text = "Chainage Intervale(M)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Start Chainage(M)";
            // 
            // checkMasterString_
            // 
            this.checkMasterString_.AutoSize = true;
            this.checkMasterString_.Location = new System.Drawing.Point(277, 95);
            this.checkMasterString_.Name = "checkMasterString_";
            this.checkMasterString_.Size = new System.Drawing.Size(88, 17);
            this.checkMasterString_.TabIndex = 14;
            this.checkMasterString_.Text = "Master String";
            this.checkMasterString_.UseVisualStyleBackColor = true;
            // 
            // btnCancel_
            // 
            this.btnCancel_.Location = new System.Drawing.Point(277, 56);
            this.btnCancel_.Name = "btnCancel_";
            this.btnCancel_.Size = new System.Drawing.Size(88, 29);
            this.btnCancel_.TabIndex = 16;
            this.btnCancel_.Text = "Cancel";
            this.btnCancel_.UseVisualStyleBackColor = true;
            this.btnCancel_.Click += new System.EventHandler(this.btnCancel__Click);
            // 
            // btnOk_
            // 
            this.btnOk_.Location = new System.Drawing.Point(277, 12);
            this.btnOk_.Name = "btnOk_";
            this.btnOk_.Size = new System.Drawing.Size(88, 29);
            this.btnOk_.TabIndex = 15;
            this.btnOk_.Text = "OK";
            this.btnOk_.UseVisualStyleBackColor = true;
            this.btnOk_.Click += new System.EventHandler(this.btnOk__Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // tbStartChainage_
            // 
            this.tbStartChainage_.Location = new System.Drawing.Point(124, 65);
            this.tbStartChainage_.MaskFormat = "";
            this.tbStartChainage_.MaximumValue = 1.7976931348623157E+308;
            this.tbStartChainage_.MinimumValue = 0;
            this.tbStartChainage_.Name = "tbStartChainage_";
            this.tbStartChainage_.Size = new System.Drawing.Size(132, 20);
            this.tbStartChainage_.TabIndex = 11;
            this.tbStartChainage_.Text = "0";
            this.tbStartChainage_.Value = 0;
            // 
            // tbChainageInterval_
            // 
            this.tbChainageInterval_.Location = new System.Drawing.Point(124, 93);
            this.tbChainageInterval_.MaskFormat = "";
            this.tbChainageInterval_.MaximumValue = 1.7976931348623157E+308;
            this.tbChainageInterval_.MinimumValue = -1.7976931348623157E+308;
            this.tbChainageInterval_.Name = "tbChainageInterval_";
            this.tbChainageInterval_.Size = new System.Drawing.Size(132, 20);
            this.tbChainageInterval_.TabIndex = 13;
            this.tbChainageInterval_.Text = "10";
            this.tbChainageInterval_.Value = 10;
            // 
            // FormMakeString
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 127);
            this.Controls.Add(this.btnCancel_);
            this.Controls.Add(this.btnOk_);
            this.Controls.Add(this.checkMasterString_);
            this.Controls.Add(this.tbStartChainage_);
            this.Controls.Add(this.tbChainageInterval_);
            this.Controls.Add(this.labelChainageInterval_);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbStringlabel_);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbModelName_);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMakeString";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Make String";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbStringlabel_;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbModelName_;
        private System.Windows.Forms.Label label1;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble tbStartChainage_;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble tbChainageInterval_;
        private System.Windows.Forms.Label labelChainageInterval_;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkMasterString_;
        private System.Windows.Forms.Button btnCancel_;
        private System.Windows.Forms.Button btnOk_;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}