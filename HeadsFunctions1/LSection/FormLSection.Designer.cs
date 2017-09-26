namespace HeadsFunctions1.LSection
{
    partial class FormLSection
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
            this.groupBoxLSectionParameters = new System.Windows.Forms.GroupBox();
            this.tbYScale_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.tbXScale_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.lbStringLebels_ = new System.Windows.Forms.CheckedListBox();
            this.labelScaleYDir = new System.Windows.Forms.Label();
            this.labelScaleXDir = new System.Windows.Forms.Label();
            this.labelStringLabel = new System.Windows.Forms.Label();
            this.comboBoxModelName = new System.Windows.Forms.ComboBox();
            this.labelModelName = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.progressBar_ = new System.Windows.Forms.ProgressBar();
            this.groupBoxLSectionParameters.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxLSectionParameters
            // 
            this.groupBoxLSectionParameters.Controls.Add(this.tbYScale_);
            this.groupBoxLSectionParameters.Controls.Add(this.tbXScale_);
            this.groupBoxLSectionParameters.Controls.Add(this.lbStringLebels_);
            this.groupBoxLSectionParameters.Controls.Add(this.labelScaleYDir);
            this.groupBoxLSectionParameters.Controls.Add(this.labelScaleXDir);
            this.groupBoxLSectionParameters.Controls.Add(this.labelStringLabel);
            this.groupBoxLSectionParameters.Controls.Add(this.comboBoxModelName);
            this.groupBoxLSectionParameters.Controls.Add(this.labelModelName);
            this.groupBoxLSectionParameters.Location = new System.Drawing.Point(7, 2);
            this.groupBoxLSectionParameters.Name = "groupBoxLSectionParameters";
            this.groupBoxLSectionParameters.Size = new System.Drawing.Size(339, 279);
            this.groupBoxLSectionParameters.TabIndex = 0;
            this.groupBoxLSectionParameters.TabStop = false;
            this.groupBoxLSectionParameters.Text = "L-Section Parameters";
            // 
            // tbYScale_
            // 
            this.tbYScale_.Location = new System.Drawing.Point(176, 248);
            this.tbYScale_.MaskFormat = "";
            this.tbYScale_.MaximumValue = 1.7976931348623157E+308;
            this.tbYScale_.MinimumValue = 0;
            this.tbYScale_.Name = "tbYScale_";
            this.tbYScale_.Size = new System.Drawing.Size(150, 20);
            this.tbYScale_.TabIndex = 12;
            this.tbYScale_.Text = "10";
            this.tbYScale_.Value = 10;
            // 
            // tbXScale_
            // 
            this.tbXScale_.Location = new System.Drawing.Point(15, 248);
            this.tbXScale_.MaskFormat = "";
            this.tbXScale_.MaximumValue = 1.7976931348623157E+308;
            this.tbXScale_.MinimumValue = 0;
            this.tbXScale_.Name = "tbXScale_";
            this.tbXScale_.Size = new System.Drawing.Size(141, 20);
            this.tbXScale_.TabIndex = 11;
            this.tbXScale_.Text = "1";
            this.tbXScale_.Value = 1;
            // 
            // lbStringLebels_
            // 
            this.lbStringLebels_.CheckOnClick = true;
            this.lbStringLebels_.FormattingEnabled = true;
            this.lbStringLebels_.Location = new System.Drawing.Point(175, 42);
            this.lbStringLebels_.Name = "lbStringLebels_";
            this.lbStringLebels_.Size = new System.Drawing.Size(151, 184);
            this.lbStringLebels_.TabIndex = 10;
            this.lbStringLebels_.SelectedValueChanged += new System.EventHandler(this.lbStringLebels__SelectedValueChanged);
            // 
            // labelScaleYDir
            // 
            this.labelScaleYDir.AutoSize = true;
            this.labelScaleYDir.Location = new System.Drawing.Point(173, 232);
            this.labelScaleYDir.Name = "labelScaleYDir";
            this.labelScaleYDir.Size = new System.Drawing.Size(67, 13);
            this.labelScaleYDir.TabIndex = 6;
            this.labelScaleYDir.Text = "Scale (Y-dir):";
            // 
            // labelScaleXDir
            // 
            this.labelScaleXDir.AutoSize = true;
            this.labelScaleXDir.Location = new System.Drawing.Point(13, 232);
            this.labelScaleXDir.Name = "labelScaleXDir";
            this.labelScaleXDir.Size = new System.Drawing.Size(67, 13);
            this.labelScaleXDir.TabIndex = 4;
            this.labelScaleXDir.Text = "Scale (X-dir):";
            // 
            // labelStringLabel
            // 
            this.labelStringLabel.AutoSize = true;
            this.labelStringLabel.Location = new System.Drawing.Point(171, 25);
            this.labelStringLabel.Name = "labelStringLabel";
            this.labelStringLabel.Size = new System.Drawing.Size(66, 13);
            this.labelStringLabel.TabIndex = 2;
            this.labelStringLabel.Text = "String Label:";
            // 
            // comboBoxModelName
            // 
            this.comboBoxModelName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxModelName.FormattingEnabled = true;
            this.comboBoxModelName.Location = new System.Drawing.Point(15, 41);
            this.comboBoxModelName.Name = "comboBoxModelName";
            this.comboBoxModelName.Size = new System.Drawing.Size(141, 21);
            this.comboBoxModelName.TabIndex = 1;
            this.comboBoxModelName.SelectedIndexChanged += new System.EventHandler(this.comboBoxModelName_SelectedIndexChanged);
            // 
            // labelModelName
            // 
            this.labelModelName.AutoSize = true;
            this.labelModelName.Location = new System.Drawing.Point(12, 25);
            this.labelModelName.Name = "labelModelName";
            this.labelModelName.Size = new System.Drawing.Size(70, 13);
            this.labelModelName.TabIndex = 0;
            this.labelModelName.Text = "Model Name:";
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(267, 317);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 28);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(181, 317);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 28);
            this.buttonOK.TabIndex = 8;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // progressBar_
            // 
            this.progressBar_.Location = new System.Drawing.Point(7, 289);
            this.progressBar_.Name = "progressBar_";
            this.progressBar_.Size = new System.Drawing.Size(339, 18);
            this.progressBar_.TabIndex = 10;
            // 
            // FormLSection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 352);
            this.Controls.Add(this.progressBar_);
            this.Controls.Add(this.groupBoxLSectionParameters);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLSection";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "L-Section";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormLSection_FormClosed);
            this.groupBoxLSectionParameters.ResumeLayout(false);
            this.groupBoxLSectionParameters.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxLSectionParameters;
        private System.Windows.Forms.ComboBox comboBoxModelName;
        private System.Windows.Forms.Label labelModelName;
        private System.Windows.Forms.Label labelStringLabel;
        private System.Windows.Forms.Label labelScaleYDir;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.CheckedListBox lbStringLebels_;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble tbYScale_;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble tbXScale_;
        private System.Windows.Forms.Label labelScaleXDir;
        private System.Windows.Forms.ProgressBar progressBar_;
    }
}