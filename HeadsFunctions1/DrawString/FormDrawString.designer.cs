namespace HeadsFunctions1.DrawString
{
    partial class FormDrawString
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.GroupBox groupBox5;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.GroupBox groupBox4;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label2;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDrawString));
            this.rb3DFace_ = new System.Windows.Forms.RadioButton();
            this.rbPoints_ = new System.Windows.Forms.RadioButton();
            this.rb3DLine_ = new System.Windows.Forms.RadioButton();
            this.rb3dPolyLine_ = new System.Windows.Forms.RadioButton();
            this.groupBoxSelection2 = new System.Windows.Forms.GroupBox();
            this.tbDeSelStringLebel_ = new System.Windows.Forms.TextBox();
            this.tbSelStringLebel_ = new System.Windows.Forms.TextBox();
            this.btnDeSelStringLabels_ = new System.Windows.Forms.Button();
            this.btnSelStringLabels_ = new System.Windows.Forms.Button();
            this.groupBoxSelection1 = new System.Windows.Forms.GroupBox();
            this.btnDeSelAllStringLabels_ = new System.Windows.Forms.Button();
            this.btnSelAllStringLabels_ = new System.Windows.Forms.Button();
            this.groupBoxParameters = new System.Windows.Forms.GroupBox();
            this.lbStringLebels_ = new System.Windows.Forms.CheckedListBox();
            this.cbModelName_ = new System.Windows.Forms.ComboBox();
            this.OkBtn = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.progressBar_ = new System.Windows.Forms.ProgressBar();
            this.tbScaleX_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.tbScaleY_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            label1 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            groupBox5 = new System.Windows.Forms.GroupBox();
            label5 = new System.Windows.Forms.Label();
            groupBox4 = new System.Windows.Forms.GroupBox();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            groupBox5.SuspendLayout();
            groupBox4.SuspendLayout();
            this.groupBoxSelection2.SuspendLayout();
            this.groupBoxSelection1.SuspendLayout();
            this.groupBoxParameters.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.Location = new System.Drawing.Point(6, 85);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(251, 21);
            label1.TabIndex = 6;
            label1.Text = "Wild Character ? may be used, Example  J?  or  JE?";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(246, 263);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(67, 13);
            label7.TabIndex = 9;
            label7.Text = "Scale (Y-dir):";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(14, 263);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(67, 13);
            label6.TabIndex = 7;
            label6.Text = "Scale (X-dir):";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(label5);
            groupBox5.Location = new System.Drawing.Point(11, 216);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new System.Drawing.Size(418, 35);
            groupBox5.TabIndex = 6;
            groupBox5.TabStop = false;
            groupBox5.Text = "Pre-Select";
            // 
            // label5
            // 
            label5.Location = new System.Drawing.Point(52, 15);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(315, 19);
            label5.TabIndex = 5;
            label5.Text = "Current Layer-Color-Line Type and Format-Point Style";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(this.rb3DFace_);
            groupBox4.Controls.Add(this.rbPoints_);
            groupBox4.Controls.Add(this.rb3DLine_);
            groupBox4.Controls.Add(this.rb3dPolyLine_);
            groupBox4.Location = new System.Drawing.Point(11, 157);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new System.Drawing.Size(418, 43);
            groupBox4.TabIndex = 5;
            groupBox4.TabStop = false;
            groupBox4.Text = "All other string labels may be drawn with : ";
            // 
            // rb3DFace_
            // 
            this.rb3DFace_.AutoSize = true;
            this.rb3DFace_.Location = new System.Drawing.Point(344, 20);
            this.rb3DFace_.Name = "rb3DFace_";
            this.rb3DFace_.Size = new System.Drawing.Size(66, 17);
            this.rb3DFace_.TabIndex = 3;
            this.rb3DFace_.Text = "3D Face";
            this.rb3DFace_.UseVisualStyleBackColor = true;
            // 
            // rbPoints_
            // 
            this.rbPoints_.AutoSize = true;
            this.rbPoints_.Location = new System.Drawing.Point(245, 20);
            this.rbPoints_.Name = "rbPoints_";
            this.rbPoints_.Size = new System.Drawing.Size(54, 17);
            this.rbPoints_.TabIndex = 2;
            this.rbPoints_.Text = "Points";
            this.rbPoints_.UseVisualStyleBackColor = true;
            // 
            // rb3DLine_
            // 
            this.rb3DLine_.AutoSize = true;
            this.rb3DLine_.Location = new System.Drawing.Point(138, 20);
            this.rb3DLine_.Name = "rb3DLine_";
            this.rb3DLine_.Size = new System.Drawing.Size(62, 17);
            this.rb3DLine_.TabIndex = 1;
            this.rb3DLine_.Text = "3D Line";
            this.rb3DLine_.UseVisualStyleBackColor = true;
            // 
            // rb3dPolyLine_
            // 
            this.rb3dPolyLine_.AutoSize = true;
            this.rb3dPolyLine_.Checked = true;
            this.rb3dPolyLine_.Location = new System.Drawing.Point(8, 20);
            this.rb3dPolyLine_.Name = "rb3dPolyLine_";
            this.rb3dPolyLine_.Size = new System.Drawing.Size(85, 17);
            this.rb3dPolyLine_.TabIndex = 0;
            this.rb3dPolyLine_.TabStop = true;
            this.rb3dPolyLine_.Text = "3D Poly Line";
            this.rb3dPolyLine_.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.Location = new System.Drawing.Point(6, 68);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(169, 81);
            label4.TabIndex = 4;
            label4.Text = "String Labels starting with letters \"P0\" are drawn as points with elevations. Oth" +
                "er string labels starting with letter \"P\" are drawn as points with labels";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(185, 18);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(71, 13);
            label3.TabIndex = 3;
            label3.Text = "String Labels:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(9, 18);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(70, 13);
            label2.TabIndex = 2;
            label2.Text = "Model Name:";
            // 
            // groupBoxSelection2
            // 
            this.groupBoxSelection2.Controls.Add(this.tbDeSelStringLebel_);
            this.groupBoxSelection2.Controls.Add(this.tbSelStringLebel_);
            this.groupBoxSelection2.Controls.Add(this.btnDeSelStringLabels_);
            this.groupBoxSelection2.Controls.Add(this.btnSelStringLabels_);
            this.groupBoxSelection2.Controls.Add(label1);
            this.groupBoxSelection2.Location = new System.Drawing.Point(180, 8);
            this.groupBoxSelection2.Name = "groupBoxSelection2";
            this.groupBoxSelection2.Size = new System.Drawing.Size(263, 108);
            this.groupBoxSelection2.TabIndex = 7;
            this.groupBoxSelection2.TabStop = false;
            // 
            // tbDeSelStringLebel_
            // 
            this.tbDeSelStringLebel_.Location = new System.Drawing.Point(175, 55);
            this.tbDeSelStringLebel_.Name = "tbDeSelStringLebel_";
            this.tbDeSelStringLebel_.Size = new System.Drawing.Size(82, 20);
            this.tbDeSelStringLebel_.TabIndex = 5;
            // 
            // tbSelStringLebel_
            // 
            this.tbSelStringLebel_.Location = new System.Drawing.Point(175, 18);
            this.tbSelStringLebel_.Name = "tbSelStringLebel_";
            this.tbSelStringLebel_.Size = new System.Drawing.Size(82, 20);
            this.tbSelStringLebel_.TabIndex = 4;
            // 
            // btnDeSelStringLabels_
            // 
            this.btnDeSelStringLabels_.Location = new System.Drawing.Point(8, 51);
            this.btnDeSelStringLabels_.Name = "btnDeSelStringLabels_";
            this.btnDeSelStringLabels_.Size = new System.Drawing.Size(148, 27);
            this.btnDeSelStringLabels_.TabIndex = 3;
            this.btnDeSelStringLabels_.Text = "De-Select String Labels";
            this.btnDeSelStringLabels_.UseVisualStyleBackColor = true;
            this.btnDeSelStringLabels_.Click += new System.EventHandler(this.btnDeSelStringLabels__Click);
            // 
            // btnSelStringLabels_
            // 
            this.btnSelStringLabels_.Location = new System.Drawing.Point(8, 15);
            this.btnSelStringLabels_.Name = "btnSelStringLabels_";
            this.btnSelStringLabels_.Size = new System.Drawing.Size(148, 27);
            this.btnSelStringLabels_.TabIndex = 2;
            this.btnSelStringLabels_.Text = "Select String Labels";
            this.btnSelStringLabels_.UseVisualStyleBackColor = true;
            this.btnSelStringLabels_.Click += new System.EventHandler(this.btnSelStringLabels__Click);
            // 
            // groupBoxSelection1
            // 
            this.groupBoxSelection1.Controls.Add(this.btnDeSelAllStringLabels_);
            this.groupBoxSelection1.Controls.Add(this.btnSelAllStringLabels_);
            this.groupBoxSelection1.Location = new System.Drawing.Point(3, 6);
            this.groupBoxSelection1.Name = "groupBoxSelection1";
            this.groupBoxSelection1.Size = new System.Drawing.Size(167, 110);
            this.groupBoxSelection1.TabIndex = 8;
            this.groupBoxSelection1.TabStop = false;
            // 
            // btnDeSelAllStringLabels_
            // 
            this.btnDeSelAllStringLabels_.Location = new System.Drawing.Point(9, 52);
            this.btnDeSelAllStringLabels_.Name = "btnDeSelAllStringLabels_";
            this.btnDeSelAllStringLabels_.Size = new System.Drawing.Size(148, 27);
            this.btnDeSelAllStringLabels_.TabIndex = 1;
            this.btnDeSelAllStringLabels_.Text = "De-Select All String Labels";
            this.btnDeSelAllStringLabels_.UseVisualStyleBackColor = true;
            this.btnDeSelAllStringLabels_.Click += new System.EventHandler(this.btnDeSelAllStringLabels__Click);
            // 
            // btnSelAllStringLabels_
            // 
            this.btnSelAllStringLabels_.Location = new System.Drawing.Point(9, 16);
            this.btnSelAllStringLabels_.Name = "btnSelAllStringLabels_";
            this.btnSelAllStringLabels_.Size = new System.Drawing.Size(148, 27);
            this.btnSelAllStringLabels_.TabIndex = 0;
            this.btnSelAllStringLabels_.Text = "Select All String Labels";
            this.btnSelAllStringLabels_.UseVisualStyleBackColor = true;
            this.btnSelAllStringLabels_.Click += new System.EventHandler(this.btnSelAllStringLabels__Click);
            // 
            // groupBoxParameters
            // 
            this.groupBoxParameters.Controls.Add(this.tbScaleX_);
            this.groupBoxParameters.Controls.Add(this.tbScaleY_);
            this.groupBoxParameters.Controls.Add(label7);
            this.groupBoxParameters.Controls.Add(label6);
            this.groupBoxParameters.Controls.Add(groupBox5);
            this.groupBoxParameters.Controls.Add(groupBox4);
            this.groupBoxParameters.Controls.Add(label4);
            this.groupBoxParameters.Controls.Add(label3);
            this.groupBoxParameters.Controls.Add(label2);
            this.groupBoxParameters.Controls.Add(this.lbStringLebels_);
            this.groupBoxParameters.Controls.Add(this.cbModelName_);
            this.groupBoxParameters.Location = new System.Drawing.Point(8, 122);
            this.groupBoxParameters.Name = "groupBoxParameters";
            this.groupBoxParameters.Size = new System.Drawing.Size(434, 296);
            this.groupBoxParameters.TabIndex = 9;
            this.groupBoxParameters.TabStop = false;
            this.groupBoxParameters.Text = "Parameters";
            // 
            // lbStringLebels_
            // 
            this.lbStringLebels_.CheckOnClick = true;
            this.lbStringLebels_.FormattingEnabled = true;
            this.lbStringLebels_.Location = new System.Drawing.Point(188, 37);
            this.lbStringLebels_.Name = "lbStringLebels_";
            this.lbStringLebels_.Size = new System.Drawing.Size(241, 109);
            this.lbStringLebels_.TabIndex = 1;
            this.lbStringLebels_.ThreeDCheckBoxes = true;
            // 
            // cbModelName_
            // 
            this.cbModelName_.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModelName_.FormattingEnabled = true;
            this.cbModelName_.Location = new System.Drawing.Point(6, 37);
            this.cbModelName_.Name = "cbModelName_";
            this.cbModelName_.Size = new System.Drawing.Size(161, 21);
            this.cbModelName_.TabIndex = 0;
            this.cbModelName_.SelectedIndexChanged += new System.EventHandler(this.cbModelName__SelectedIndexChanged);
            // 
            // OkBtn
            // 
            this.OkBtn.Location = new System.Drawing.Point(242, 457);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(91, 28);
            this.OkBtn.TabIndex = 11;
            this.OkBtn.Text = "OK";
            this.OkBtn.UseVisualStyleBackColor = true;
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(352, 457);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(91, 28);
            this.BtnCancel.TabIndex = 12;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // progressBar_
            // 
            this.progressBar_.ForeColor = System.Drawing.Color.Blue;
            this.progressBar_.Location = new System.Drawing.Point(8, 428);
            this.progressBar_.MarqueeAnimationSpeed = 0;
            this.progressBar_.Name = "progressBar_";
            this.progressBar_.Size = new System.Drawing.Size(435, 20);
            this.progressBar_.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar_.TabIndex = 13;
            // 
            // tbScaleX_
            // 
            this.tbScaleX_.Location = new System.Drawing.Point(85, 260);
            this.tbScaleX_.MaskFormat = "";
            this.tbScaleX_.MaximumValue = 1.7976931348623157E+308;
            this.tbScaleX_.MinimumValue = -1.7976931348623157E+308;
            this.tbScaleX_.Name = "tbScaleX_";
            this.tbScaleX_.Size = new System.Drawing.Size(110, 20);
            this.tbScaleX_.TabIndex = 15;
            this.tbScaleX_.Text = "0";
            this.tbScaleX_.Value = 0;
            // 
            // tbScaleY_
            // 
            this.tbScaleY_.Location = new System.Drawing.Point(319, 260);
            this.tbScaleY_.MaskFormat = "";
            this.tbScaleY_.MaximumValue = 1.7976931348623157E+308;
            this.tbScaleY_.MinimumValue = -1.7976931348623157E+308;
            this.tbScaleY_.Name = "tbScaleY_";
            this.tbScaleY_.Size = new System.Drawing.Size(110, 20);
            this.tbScaleY_.TabIndex = 14;
            this.tbScaleY_.Text = "0";
            this.tbScaleY_.Value = 0;
            // 
            // FormDrawString
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 495);
            this.Controls.Add(this.progressBar_);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.OkBtn);
            this.Controls.Add(this.groupBoxParameters);
            this.Controls.Add(this.groupBoxSelection1);
            this.Controls.Add(this.groupBoxSelection2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormDrawString";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Draw String";
            groupBox5.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            this.groupBoxSelection2.ResumeLayout(false);
            this.groupBoxSelection2.PerformLayout();
            this.groupBoxSelection1.ResumeLayout(false);
            this.groupBoxParameters.ResumeLayout(false);
            this.groupBoxParameters.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSelAllStringLabels_;
        private System.Windows.Forms.Button btnDeSelAllStringLabels_;
        private System.Windows.Forms.Button btnDeSelStringLabels_;
        private System.Windows.Forms.Button btnSelStringLabels_;
        private System.Windows.Forms.TextBox tbSelStringLebel_;
        private System.Windows.Forms.TextBox tbDeSelStringLebel_;
        private System.Windows.Forms.CheckedListBox lbStringLebels_;
        private System.Windows.Forms.ComboBox cbModelName_;
        private System.Windows.Forms.RadioButton rb3DFace_;
        private System.Windows.Forms.RadioButton rbPoints_;
        private System.Windows.Forms.RadioButton rb3DLine_;
        private System.Windows.Forms.RadioButton rb3dPolyLine_;
        private System.Windows.Forms.Button OkBtn;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.GroupBox groupBoxParameters;
        private System.Windows.Forms.GroupBox groupBoxSelection2;
        private System.Windows.Forms.GroupBox groupBoxSelection1;
        private System.Windows.Forms.ProgressBar progressBar_;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble tbScaleX_;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble tbScaleY_;
    }
}