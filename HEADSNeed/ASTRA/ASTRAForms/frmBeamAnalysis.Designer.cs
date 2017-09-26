namespace HEADSNeed.ASTRA.ASTRAForms
{
    partial class frmBeamAnalysis
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
            this.rtbBeamAnalysis = new System.Windows.Forms.RichTextBox();
            this.btnSetColor = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkHide = new System.Windows.Forms.CheckBox();
            this.rbtnMoment = new System.Windows.Forms.RadioButton();
            this.rbtnShear = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbLoadCase = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSpanNo = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbBeamAnalysis
            // 
            this.rtbBeamAnalysis.BackColor = System.Drawing.Color.Black;
            this.rtbBeamAnalysis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbBeamAnalysis.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbBeamAnalysis.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.rtbBeamAnalysis.Location = new System.Drawing.Point(0, 33);
            this.rtbBeamAnalysis.Name = "rtbBeamAnalysis";
            this.rtbBeamAnalysis.ReadOnly = true;
            this.rtbBeamAnalysis.Size = new System.Drawing.Size(474, 387);
            this.rtbBeamAnalysis.TabIndex = 0;
            this.rtbBeamAnalysis.Text = "";
            this.rtbBeamAnalysis.WordWrap = false;
            // 
            // btnSetColor
            // 
            this.btnSetColor.Location = new System.Drawing.Point(476, 8);
            this.btnSetColor.Name = "btnSetColor";
            this.btnSetColor.Size = new System.Drawing.Size(44, 23);
            this.btnSetColor.TabIndex = 1;
            this.btnSetColor.Text = "Color";
            this.btnSetColor.UseVisualStyleBackColor = true;
            this.btnSetColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkHide);
            this.groupBox1.Controls.Add(this.rbtnMoment);
            this.groupBox1.Controls.Add(this.rbtnShear);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbLoadCase);
            this.groupBox1.Controls.Add(this.btnSetColor);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbSpanNo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(474, 33);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // chkHide
            // 
            this.chkHide.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkHide.AutoSize = true;
            this.chkHide.Location = new System.Drawing.Point(396, 8);
            this.chkHide.Name = "chkHide";
            this.chkHide.Size = new System.Drawing.Size(74, 23);
            this.chkHide.TabIndex = 9;
            this.chkHide.Text = "Hide Details";
            this.chkHide.UseVisualStyleBackColor = true;
            this.chkHide.CheckedChanged += new System.EventHandler(this.chkHide_CheckedChanged);
            // 
            // rbtnMoment
            // 
            this.rbtnMoment.AutoSize = true;
            this.rbtnMoment.Location = new System.Drawing.Point(305, 9);
            this.rbtnMoment.Name = "rbtnMoment";
            this.rbtnMoment.Size = new System.Drawing.Size(73, 17);
            this.rbtnMoment.TabIndex = 8;
            this.rbtnMoment.Text = "MOMENT";
            this.rbtnMoment.UseVisualStyleBackColor = true;
            this.rbtnMoment.CheckedChanged += new System.EventHandler(this.cmbSpanNo_SelectedIndexChanged);
            // 
            // rbtnShear
            // 
            this.rbtnShear.AutoSize = true;
            this.rbtnShear.Checked = true;
            this.rbtnShear.Location = new System.Drawing.Point(237, 9);
            this.rbtnShear.Name = "rbtnShear";
            this.rbtnShear.Size = new System.Drawing.Size(62, 17);
            this.rbtnShear.TabIndex = 7;
            this.rbtnShear.TabStop = true;
            this.rbtnShear.Text = "SHEAR";
            this.rbtnShear.UseVisualStyleBackColor = true;
            this.rbtnShear.CheckedChanged += new System.EventHandler(this.cmbSpanNo_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(116, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Load Case :";
            // 
            // cmbLoadCase
            // 
            this.cmbLoadCase.FormattingEnabled = true;
            this.cmbLoadCase.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.cmbLoadCase.Location = new System.Drawing.Point(181, 8);
            this.cmbLoadCase.Name = "cmbLoadCase";
            this.cmbLoadCase.Size = new System.Drawing.Size(50, 21);
            this.cmbLoadCase.TabIndex = 4;
            this.cmbLoadCase.SelectedIndexChanged += new System.EventHandler(this.cmbSpanNo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Span No. :";
            // 
            // cmbSpanNo
            // 
            this.cmbSpanNo.FormattingEnabled = true;
            this.cmbSpanNo.Location = new System.Drawing.Point(67, 8);
            this.cmbSpanNo.Name = "cmbSpanNo";
            this.cmbSpanNo.Size = new System.Drawing.Size(48, 21);
            this.cmbSpanNo.TabIndex = 2;
            this.cmbSpanNo.SelectedIndexChanged += new System.EventHandler(this.cmbSpanNo_SelectedIndexChanged);
            // 
            // frmBeamAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 420);
            this.Controls.Add(this.rtbBeamAnalysis);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmBeamAnalysis";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Beam Analysis";
            this.Load += new System.EventHandler(this.frmBeamAnalysis_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbBeamAnalysis;
        private System.Windows.Forms.Button btnSetColor;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSpanNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbLoadCase;
        private System.Windows.Forms.RadioButton rbtnShear;
        private System.Windows.Forms.RadioButton rbtnMoment;
        private System.Windows.Forms.CheckBox chkHide;
    }
}