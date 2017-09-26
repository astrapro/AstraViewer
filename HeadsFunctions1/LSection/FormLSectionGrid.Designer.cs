namespace HeadsFunctions1.LSection
{
    partial class FormLSectionGrid
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
            this.groupBoxGridSettings = new System.Windows.Forms.GroupBox();
            this.panelColor_ = new System.Windows.Forms.Panel();
            this.tbTextSize_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.tbLevelInterval_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.tbChainageInterval_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.comboBoxTextColor = new System.Windows.Forms.ComboBox();
            this.labelTextColor = new System.Windows.Forms.Label();
            this.labelTextSize = new System.Windows.Forms.Label();
            this.labelLevelInterval = new System.Windows.Forms.Label();
            this.labelChainageInterval = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxGridSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxGridSettings
            // 
            this.groupBoxGridSettings.Controls.Add(this.panelColor_);
            this.groupBoxGridSettings.Controls.Add(this.tbTextSize_);
            this.groupBoxGridSettings.Controls.Add(this.tbLevelInterval_);
            this.groupBoxGridSettings.Controls.Add(this.tbChainageInterval_);
            this.groupBoxGridSettings.Controls.Add(this.comboBoxTextColor);
            this.groupBoxGridSettings.Controls.Add(this.labelTextColor);
            this.groupBoxGridSettings.Controls.Add(this.labelTextSize);
            this.groupBoxGridSettings.Controls.Add(this.labelLevelInterval);
            this.groupBoxGridSettings.Controls.Add(this.labelChainageInterval);
            this.groupBoxGridSettings.Location = new System.Drawing.Point(7, 2);
            this.groupBoxGridSettings.Name = "groupBoxGridSettings";
            this.groupBoxGridSettings.Size = new System.Drawing.Size(297, 113);
            this.groupBoxGridSettings.TabIndex = 0;
            this.groupBoxGridSettings.TabStop = false;
            this.groupBoxGridSettings.Text = "Grid Setting";
            // 
            // panelColor_
            // 
            this.panelColor_.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelColor_.Location = new System.Drawing.Point(253, 76);
            this.panelColor_.Name = "panelColor_";
            this.panelColor_.Size = new System.Drawing.Size(33, 21);
            this.panelColor_.TabIndex = 11;
            // 
            // tbTextSize_
            // 
            this.tbTextSize_.Location = new System.Drawing.Point(19, 78);
            this.tbTextSize_.MaskFormat = "";
            this.tbTextSize_.MaximumValue = 1.7976931348623157E+308;
            this.tbTextSize_.MinimumValue = -1.7976931348623157E+308;
            this.tbTextSize_.Name = "tbTextSize_";
            this.tbTextSize_.Size = new System.Drawing.Size(100, 20);
            this.tbTextSize_.TabIndex = 10;
            this.tbTextSize_.Text = "12";
            this.tbTextSize_.Value = 12;
            // 
            // tbLevelInterval_
            // 
            this.tbLevelInterval_.Location = new System.Drawing.Point(146, 33);
            this.tbLevelInterval_.MaskFormat = "";
            this.tbLevelInterval_.MaximumValue = 1.7976931348623157E+308;
            this.tbLevelInterval_.MinimumValue = -1.7976931348623157E+308;
            this.tbLevelInterval_.Name = "tbLevelInterval_";
            this.tbLevelInterval_.Size = new System.Drawing.Size(100, 20);
            this.tbLevelInterval_.TabIndex = 9;
            this.tbLevelInterval_.Text = "5";
            this.tbLevelInterval_.Value = 5;
            // 
            // tbChainageInterval_
            // 
            this.tbChainageInterval_.Location = new System.Drawing.Point(19, 33);
            this.tbChainageInterval_.MaskFormat = "";
            this.tbChainageInterval_.MaximumValue = 1.7976931348623157E+308;
            this.tbChainageInterval_.MinimumValue = -1.7976931348623157E+308;
            this.tbChainageInterval_.Name = "tbChainageInterval_";
            this.tbChainageInterval_.Size = new System.Drawing.Size(100, 20);
            this.tbChainageInterval_.TabIndex = 8;
            this.tbChainageInterval_.Text = "50";
            this.tbChainageInterval_.Value = 50;
            // 
            // comboBoxTextColor
            // 
            this.comboBoxTextColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTextColor.FormattingEnabled = true;
            this.comboBoxTextColor.Location = new System.Drawing.Point(146, 77);
            this.comboBoxTextColor.Name = "comboBoxTextColor";
            this.comboBoxTextColor.Size = new System.Drawing.Size(100, 21);
            this.comboBoxTextColor.TabIndex = 7;
            this.comboBoxTextColor.SelectedIndexChanged += new System.EventHandler(this.comboBoxTextColor_SelectedIndexChanged);
            // 
            // labelTextColor
            // 
            this.labelTextColor.AutoSize = true;
            this.labelTextColor.Location = new System.Drawing.Point(143, 61);
            this.labelTextColor.Name = "labelTextColor";
            this.labelTextColor.Size = new System.Drawing.Size(55, 13);
            this.labelTextColor.TabIndex = 6;
            this.labelTextColor.Text = "Text Color";
            // 
            // labelTextSize
            // 
            this.labelTextSize.AutoSize = true;
            this.labelTextSize.Location = new System.Drawing.Point(16, 61);
            this.labelTextSize.Name = "labelTextSize";
            this.labelTextSize.Size = new System.Drawing.Size(76, 13);
            this.labelTextSize.TabIndex = 4;
            this.labelTextSize.Text = "Text Size (mm)";
            // 
            // labelLevelInterval
            // 
            this.labelLevelInterval.AutoSize = true;
            this.labelLevelInterval.Location = new System.Drawing.Point(143, 16);
            this.labelLevelInterval.Name = "labelLevelInterval";
            this.labelLevelInterval.Size = new System.Drawing.Size(71, 13);
            this.labelLevelInterval.TabIndex = 2;
            this.labelLevelInterval.Text = "Level Interval";
            // 
            // labelChainageInterval
            // 
            this.labelChainageInterval.AutoSize = true;
            this.labelChainageInterval.Location = new System.Drawing.Point(16, 16);
            this.labelChainageInterval.Name = "labelChainageInterval";
            this.labelChainageInterval.Size = new System.Drawing.Size(90, 13);
            this.labelChainageInterval.TabIndex = 0;
            this.labelChainageInterval.Text = "Chainage Interval";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(310, 22);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(70, 29);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(310, 63);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(70, 29);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // FormLSectionGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 123);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBoxGridSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLSectionGrid";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Grid on Vertical Profile";
            this.groupBoxGridSettings.ResumeLayout(false);
            this.groupBoxGridSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxGridSettings;
        private System.Windows.Forms.Label labelTextSize;
        private System.Windows.Forms.Label labelLevelInterval;
        private System.Windows.Forms.Label labelChainageInterval;
        private System.Windows.Forms.Label labelTextColor;
        private System.Windows.Forms.ComboBox comboBoxTextColor;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble tbChainageInterval_;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble tbTextSize_;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble tbLevelInterval_;
        private System.Windows.Forms.Panel panelColor_;
    }
}