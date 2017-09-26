namespace HeadsViewer
{
    partial class frmExport
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboExport = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkAspect = new System.Windows.Forms.CheckBox();
            this.textHeight = new System.Windows.Forms.TextBox();
            this.textWidth = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.butSelect = new System.Windows.Forms.Button();
            this.butAll = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.butExport = new System.Windows.Forms.Button();
            this.butExit = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Export To:";
            // 
            // comboExport
            // 
            this.comboExport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboExport.FormattingEnabled = true;
            this.comboExport.Location = new System.Drawing.Point(74, 6);
            this.comboExport.Name = "comboExport";
            this.comboExport.Size = new System.Drawing.Size(430, 21);
            this.comboExport.TabIndex = 1;
            this.comboExport.SelectedIndexChanged += new System.EventHandler(this.comboExport_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.checkAspect);
            this.panel1.Controls.Add(this.textHeight);
            this.panel1.Controls.Add(this.textWidth);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.butSelect);
            this.panel1.Controls.Add(this.butAll);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(11, 42);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(493, 318);
            this.panel1.TabIndex = 2;
            // 
            // checkAspect
            // 
            this.checkAspect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkAspect.AutoSize = true;
            this.checkAspect.Checked = true;
            this.checkAspect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkAspect.Location = new System.Drawing.Point(394, 155);
            this.checkAspect.Name = "checkAspect";
            this.checkAspect.Size = new System.Drawing.Size(87, 17);
            this.checkAspect.TabIndex = 8;
            this.checkAspect.Text = "Keep Aspect";
            this.checkAspect.UseVisualStyleBackColor = true;
            this.checkAspect.CheckedChanged += new System.EventHandler(this.checkAspect_CheckedChanged);
            // 
            // textHeight
            // 
            this.textHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textHeight.Location = new System.Drawing.Point(429, 125);
            this.textHeight.Name = "textHeight";
            this.textHeight.Size = new System.Drawing.Size(54, 20);
            this.textHeight.TabIndex = 6;
            this.textHeight.Text = "200";
            this.textHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textHeight_KeyPress);
            // 
            // textWidth
            // 
            this.textWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textWidth.Location = new System.Drawing.Point(429, 100);
            this.textWidth.Name = "textWidth";
            this.textWidth.Size = new System.Drawing.Size(54, 20);
            this.textWidth.TabIndex = 5;
            this.textWidth.Text = "200";
            this.textWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textWidth_KeyPress);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(385, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Height:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(388, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Width:";
            // 
            // butSelect
            // 
            this.butSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butSelect.Location = new System.Drawing.Point(398, 48);
            this.butSelect.Name = "butSelect";
            this.butSelect.Size = new System.Drawing.Size(72, 36);
            this.butSelect.TabIndex = 2;
            this.butSelect.Text = "Window Select";
            this.butSelect.UseVisualStyleBackColor = true;
            this.butSelect.Click += new System.EventHandler(this.butSelect_Click);
            // 
            // butAll
            // 
            this.butAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butAll.Location = new System.Drawing.Point(398, 11);
            this.butAll.Name = "butAll";
            this.butAll.Size = new System.Drawing.Size(72, 31);
            this.butAll.TabIndex = 1;
            this.butAll.Text = "Extends";
            this.butAll.UseVisualStyleBackColor = true;
            this.butAll.Click += new System.EventHandler(this.butAll_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(10, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(366, 294);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(5, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(376, 308);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // butExport
            // 
            this.butExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butExport.Enabled = false;
            this.butExport.Location = new System.Drawing.Point(318, 365);
            this.butExport.Name = "butExport";
            this.butExport.Size = new System.Drawing.Size(75, 24);
            this.butExport.TabIndex = 3;
            this.butExport.Text = "Export";
            this.butExport.UseVisualStyleBackColor = true;
            this.butExport.Click += new System.EventHandler(this.butExport_Click);
            // 
            // butExit
            // 
            this.butExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butExit.Location = new System.Drawing.Point(430, 365);
            this.butExit.Name = "butExit";
            this.butExit.Size = new System.Drawing.Size(75, 24);
            this.butExit.TabIndex = 4;
            this.butExit.Text = "Cancel";
            this.butExit.UseVisualStyleBackColor = true;
            this.butExit.Click += new System.EventHandler(this.butExit_Click);
            // 
            // frmExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.butExit;
            this.ClientSize = new System.Drawing.Size(516, 393);
            this.Controls.Add(this.butExit);
            this.Controls.Add(this.butExport);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.comboExport);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimumSize = new System.Drawing.Size(310, 285);
            this.Name = "frmExport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Export";
            this.Load += new System.EventHandler(this.frmExport_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboExport;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button butSelect;
        private System.Windows.Forms.Button butAll;
        private System.Windows.Forms.Button butExport;
        private System.Windows.Forms.Button butExit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textWidth;
        private System.Windows.Forms.TextBox textHeight;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkAspect;
    }
}