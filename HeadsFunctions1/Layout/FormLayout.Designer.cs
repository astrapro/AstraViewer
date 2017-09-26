namespace HeadsFunctions1.Layout
{
    partial class FormLayout
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
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.GroupBox groupBox2;
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label label9;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.GroupBox groupBox3;
            System.Windows.Forms.Label label4;
            this.comboPaperSize_ = new System.Windows.Forms.ComboBox();
            this.btnCancel_ = new System.Windows.Forms.Button();
            this.btnOk_ = new System.Windows.Forms.Button();
            this.tbScale_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.tbMarginLeft_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.tbMarginBottom_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.tbMarginRight_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.tbMarginTop_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.tbPageHeight_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.tbPageWidth_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            groupBox1 = new System.Windows.Forms.GroupBox();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            groupBox2 = new System.Windows.Forms.GroupBox();
            label10 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            groupBox3 = new System.Windows.Forms.GroupBox();
            label4 = new System.Windows.Forms.Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            groupBox1.Controls.Add(this.tbPageHeight_);
            groupBox1.Controls.Add(this.tbPageWidth_);
            groupBox1.Controls.Add(this.comboPaperSize_);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new System.Drawing.Point(10, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(306, 113);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Paper Size";
            // 
            // comboPaperSize_
            // 
            this.comboPaperSize_.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPaperSize_.FormattingEnabled = true;
            this.comboPaperSize_.Location = new System.Drawing.Point(103, 21);
            this.comboPaperSize_.Name = "comboPaperSize_";
            this.comboPaperSize_.Size = new System.Drawing.Size(100, 21);
            this.comboPaperSize_.TabIndex = 3;
            this.comboPaperSize_.SelectedIndexChanged += new System.EventHandler(this.comboPaperSize__SelectedIndexChanged);
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(32, 80);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(38, 13);
            label3.TabIndex = 2;
            label3.Text = "Height";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(32, 52);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(35, 13);
            label2.TabIndex = 1;
            label2.Text = "Width";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(32, 24);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(62, 13);
            label1.TabIndex = 0;
            label1.Text = "Paper Type";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(this.tbMarginLeft_);
            groupBox2.Controls.Add(this.tbMarginBottom_);
            groupBox2.Controls.Add(this.tbMarginRight_);
            groupBox2.Controls.Add(this.tbMarginTop_);
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(label7);
            groupBox2.Location = new System.Drawing.Point(12, 122);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(304, 83);
            groupBox2.TabIndex = 13;
            groupBox2.TabStop = false;
            groupBox2.Text = "Margins (mm)";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(152, 53);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(25, 13);
            label10.TabIndex = 3;
            label10.Text = "Left";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(7, 53);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(32, 13);
            label9.TabIndex = 2;
            label9.Text = "Right";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(152, 20);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(40, 13);
            label8.TabIndex = 1;
            label8.Text = "Bottom";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(7, 20);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(26, 13);
            label7.TabIndex = 0;
            label7.Text = "Top";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(this.tbScale_);
            groupBox3.Controls.Add(label4);
            groupBox3.Location = new System.Drawing.Point(10, 211);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(306, 56);
            groupBox3.TabIndex = 14;
            groupBox3.TabStop = false;
            groupBox3.Text = "Scale";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(72, 25);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(19, 13);
            label4.TabIndex = 0;
            label4.Text = "1 :";
            // 
            // btnCancel_
            // 
            this.btnCancel_.Location = new System.Drawing.Point(250, 278);
            this.btnCancel_.Name = "btnCancel_";
            this.btnCancel_.Size = new System.Drawing.Size(66, 29);
            this.btnCancel_.TabIndex = 18;
            this.btnCancel_.Text = "Cancel";
            this.btnCancel_.UseVisualStyleBackColor = true;
            this.btnCancel_.Click += new System.EventHandler(this.btnCancel__Click);
            // 
            // btnOk_
            // 
            this.btnOk_.Location = new System.Drawing.Point(169, 278);
            this.btnOk_.Name = "btnOk_";
            this.btnOk_.Size = new System.Drawing.Size(66, 29);
            this.btnOk_.TabIndex = 17;
            this.btnOk_.Text = "OK";
            this.btnOk_.UseVisualStyleBackColor = true;
            this.btnOk_.Click += new System.EventHandler(this.btnOk__Click);
            // 
            // tbScale_
            // 
            this.tbScale_.Location = new System.Drawing.Point(103, 22);
            this.tbScale_.MaskFormat = "";
            this.tbScale_.MaximumValue = 1.7976931348623157E+308;
            this.tbScale_.MinimumValue = 1;
            this.tbScale_.Name = "tbScale_";
            this.tbScale_.Size = new System.Drawing.Size(100, 20);
            this.tbScale_.TabIndex = 5;
            this.tbScale_.Text = "2000";
            this.tbScale_.Value = 2000;
            // 
            // tbMarginLeft_
            // 
            this.tbMarginLeft_.Location = new System.Drawing.Point(206, 50);
            this.tbMarginLeft_.MaskFormat = "";
            this.tbMarginLeft_.MaximumValue = 1.7976931348623157E+308;
            this.tbMarginLeft_.MinimumValue = -1.7976931348623157E+308;
            this.tbMarginLeft_.Name = "tbMarginLeft_";
            this.tbMarginLeft_.Size = new System.Drawing.Size(82, 20);
            this.tbMarginLeft_.TabIndex = 7;
            this.tbMarginLeft_.Text = "10";
            this.tbMarginLeft_.Value = 10;
            // 
            // tbMarginBottom_
            // 
            this.tbMarginBottom_.Location = new System.Drawing.Point(206, 20);
            this.tbMarginBottom_.MaskFormat = "";
            this.tbMarginBottom_.MaximumValue = 1.7976931348623157E+308;
            this.tbMarginBottom_.MinimumValue = -1.7976931348623157E+308;
            this.tbMarginBottom_.Name = "tbMarginBottom_";
            this.tbMarginBottom_.Size = new System.Drawing.Size(82, 20);
            this.tbMarginBottom_.TabIndex = 6;
            this.tbMarginBottom_.Text = "10";
            this.tbMarginBottom_.Value = 10;
            // 
            // tbMarginRight_
            // 
            this.tbMarginRight_.Location = new System.Drawing.Point(46, 50);
            this.tbMarginRight_.MaskFormat = "";
            this.tbMarginRight_.MaximumValue = 1.7976931348623157E+308;
            this.tbMarginRight_.MinimumValue = -1.7976931348623157E+308;
            this.tbMarginRight_.Name = "tbMarginRight_";
            this.tbMarginRight_.Size = new System.Drawing.Size(82, 20);
            this.tbMarginRight_.TabIndex = 5;
            this.tbMarginRight_.Text = "10";
            this.tbMarginRight_.Value = 10;
            // 
            // tbMarginTop_
            // 
            this.tbMarginTop_.Location = new System.Drawing.Point(46, 20);
            this.tbMarginTop_.MaskFormat = "";
            this.tbMarginTop_.MaximumValue = 1.7976931348623157E+308;
            this.tbMarginTop_.MinimumValue = -1.7976931348623157E+308;
            this.tbMarginTop_.Name = "tbMarginTop_";
            this.tbMarginTop_.Size = new System.Drawing.Size(82, 20);
            this.tbMarginTop_.TabIndex = 4;
            this.tbMarginTop_.Text = "10";
            this.tbMarginTop_.Value = 10;
            // 
            // tbPageHeight_
            // 
            this.tbPageHeight_.Location = new System.Drawing.Point(103, 80);
            this.tbPageHeight_.MaskFormat = "";
            this.tbPageHeight_.MaximumValue = 1.7976931348623157E+308;
            this.tbPageHeight_.MinimumValue = -1.7976931348623157E+308;
            this.tbPageHeight_.Name = "tbPageHeight_";
            this.tbPageHeight_.Size = new System.Drawing.Size(100, 20);
            this.tbPageHeight_.TabIndex = 5;
            this.tbPageHeight_.Text = "0";
            this.tbPageHeight_.Value = 0;
            // 
            // tbPageWidth_
            // 
            this.tbPageWidth_.Location = new System.Drawing.Point(103, 52);
            this.tbPageWidth_.MaskFormat = "";
            this.tbPageWidth_.MaximumValue = 841;
            this.tbPageWidth_.MinimumValue = -1.7976931348623157E+308;
            this.tbPageWidth_.Name = "tbPageWidth_";
            this.tbPageWidth_.Size = new System.Drawing.Size(100, 20);
            this.tbPageWidth_.TabIndex = 4;
            this.tbPageWidth_.Text = "0";
            this.tbPageWidth_.Value = 0;
            // 
            // FormLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 317);
            this.Controls.Add(this.btnCancel_);
            this.Controls.Add(this.btnOk_);
            this.Controls.Add(groupBox3);
            this.Controls.Add(groupBox2);
            this.Controls.Add(groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLayout";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Layout";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboPaperSize_;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble tbPageHeight_;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble tbPageWidth_;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble tbMarginLeft_;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble tbMarginBottom_;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble tbMarginRight_;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble tbMarginTop_;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble tbScale_;
        private System.Windows.Forms.Button btnCancel_;
        private System.Windows.Forms.Button btnOk_;
    }
}