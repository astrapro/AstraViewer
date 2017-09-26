namespace HeadsFunctions1.Halignment
{
    partial class FormHipEdit
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
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.GroupBox groupBox1;
            this.buttonOK = new System.Windows.Forms.Button();
            this.txtYValue_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.txtTrailTrans_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.txtLeadTrans_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.txtRadius_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.txtXValue_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(8, 16);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(41, 13);
            label1.TabIndex = 1;
            label1.Text = "XValue";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(91, 16);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(41, 13);
            label2.TabIndex = 2;
            label2.Text = "YValue";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(169, 16);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(40, 13);
            label3.TabIndex = 3;
            label3.Text = "Radius";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(249, 16);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(61, 13);
            label4.TabIndex = 4;
            label4.Text = "Lead Trans";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(328, 16);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(57, 13);
            label5.TabIndex = 5;
            label5.Text = "Trail Trans";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.txtYValue_);
            groupBox1.Controls.Add(this.txtTrailTrans_);
            groupBox1.Controls.Add(this.txtLeadTrans_);
            groupBox1.Controls.Add(this.txtRadius_);
            groupBox1.Controls.Add(this.txtXValue_);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Location = new System.Drawing.Point(7, 8);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(415, 63);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(351, 77);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(71, 23);
            this.buttonOK.TabIndex = 11;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // txtYValue_
            // 
            this.txtYValue_.Location = new System.Drawing.Point(89, 32);
            this.txtYValue_.MaskFormat = "0.000000";
            this.txtYValue_.MaximumValue = 1.7976931348623157E+308;
            this.txtYValue_.MinimumValue = -1.7976931348623157E+308;
            this.txtYValue_.Name = "txtYValue_";
            this.txtYValue_.Size = new System.Drawing.Size(74, 20);
            this.txtYValue_.TabIndex = 15;
            this.txtYValue_.Text = "0.000000";
            this.txtYValue_.Value = 0;
            // 
            // txtTrailTrans_
            // 
            this.txtTrailTrans_.Location = new System.Drawing.Point(332, 32);
            this.txtTrailTrans_.MaskFormat = "0.000000";
            this.txtTrailTrans_.MaximumValue = 1.7976931348623157E+308;
            this.txtTrailTrans_.MinimumValue = -1.7976931348623157E+308;
            this.txtTrailTrans_.Name = "txtTrailTrans_";
            this.txtTrailTrans_.Size = new System.Drawing.Size(74, 20);
            this.txtTrailTrans_.TabIndex = 14;
            this.txtTrailTrans_.Text = "0.000000";
            this.txtTrailTrans_.Value = 0;
            // 
            // txtLeadTrans_
            // 
            this.txtLeadTrans_.Location = new System.Drawing.Point(251, 32);
            this.txtLeadTrans_.MaskFormat = "0.000000";
            this.txtLeadTrans_.MaximumValue = 1.7976931348623157E+308;
            this.txtLeadTrans_.MinimumValue = -1.7976931348623157E+308;
            this.txtLeadTrans_.Name = "txtLeadTrans_";
            this.txtLeadTrans_.Size = new System.Drawing.Size(74, 20);
            this.txtLeadTrans_.TabIndex = 13;
            this.txtLeadTrans_.Text = "0.000000";
            this.txtLeadTrans_.Value = 0;
            // 
            // txtRadius_
            // 
            this.txtRadius_.Location = new System.Drawing.Point(170, 32);
            this.txtRadius_.MaskFormat = "0.000000";
            this.txtRadius_.MaximumValue = 1.7976931348623157E+308;
            this.txtRadius_.MinimumValue = -1.7976931348623157E+308;
            this.txtRadius_.Name = "txtRadius_";
            this.txtRadius_.Size = new System.Drawing.Size(74, 20);
            this.txtRadius_.TabIndex = 12;
            this.txtRadius_.Text = "0.000000";
            this.txtRadius_.Value = 0;
            // 
            // txtXValue_
            // 
            this.txtXValue_.Location = new System.Drawing.Point(8, 32);
            this.txtXValue_.MaskFormat = "0.000000";
            this.txtXValue_.MaximumValue = 1.7976931348623157E+308;
            this.txtXValue_.MinimumValue = -1.7976931348623157E+308;
            this.txtXValue_.Name = "txtXValue_";
            this.txtXValue_.Size = new System.Drawing.Size(74, 20);
            this.txtXValue_.TabIndex = 10;
            this.txtXValue_.Text = "0.000000";
            this.txtXValue_.Value = 0;
            // 
            // FormHipEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 103);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormHipEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit HIP";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble txtXValue_;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble txtTrailTrans_;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble txtLeadTrans_;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble txtRadius_;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble txtYValue_;
    }
}