namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    partial class frmSeismicLoad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSeismicLoad));
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_sc = new System.Windows.Forms.TextBox();
            this.btn_sc_cal = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtn_fz_negative = new System.Windows.Forms.RadioButton();
            this.rbtn_fz_positive = new System.Windows.Forms.RadioButton();
            this.rbtn_fx_negative = new System.Windows.Forms.RadioButton();
            this.rbtn_fx_positive = new System.Windows.Forms.RadioButton();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(441, 63);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Seismic Coefficient [sc]";
            // 
            // txt_sc
            // 
            this.txt_sc.Location = new System.Drawing.Point(169, 132);
            this.txt_sc.Name = "txt_sc";
            this.txt_sc.Size = new System.Drawing.Size(66, 21);
            this.txt_sc.TabIndex = 2;
            this.txt_sc.Text = "0.180";
            this.txt_sc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btn_sc_cal
            // 
            this.btn_sc_cal.Location = new System.Drawing.Point(241, 130);
            this.btn_sc_cal.Name = "btn_sc_cal";
            this.btn_sc_cal.Size = new System.Drawing.Size(75, 23);
            this.btn_sc_cal.TabIndex = 3;
            this.btn_sc_cal.Text = "Help";
            this.btn_sc_cal.UseVisualStyleBackColor = true;
            this.btn_sc_cal.Click += new System.EventHandler(this.btn_sc_cal_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtn_fz_negative);
            this.groupBox1.Controls.Add(this.rbtn_fz_positive);
            this.groupBox1.Controls.Add(this.rbtn_fx_negative);
            this.groupBox1.Controls.Add(this.rbtn_fx_positive);
            this.groupBox1.Location = new System.Drawing.Point(12, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(304, 47);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Direction";
            // 
            // rbtn_fz_negative
            // 
            this.rbtn_fz_negative.AutoSize = true;
            this.rbtn_fz_negative.Location = new System.Drawing.Point(243, 20);
            this.rbtn_fz_negative.Name = "rbtn_fz_negative";
            this.rbtn_fz_negative.Size = new System.Drawing.Size(58, 17);
            this.rbtn_fz_negative.TabIndex = 0;
            this.rbtn_fz_negative.Text = "FZ (-)";
            this.rbtn_fz_negative.UseVisualStyleBackColor = true;
            // 
            // rbtn_fz_positive
            // 
            this.rbtn_fz_positive.AutoSize = true;
            this.rbtn_fz_positive.Location = new System.Drawing.Point(161, 20);
            this.rbtn_fz_positive.Name = "rbtn_fz_positive";
            this.rbtn_fz_positive.Size = new System.Drawing.Size(62, 17);
            this.rbtn_fz_positive.TabIndex = 0;
            this.rbtn_fz_positive.Text = "FZ (+)";
            this.rbtn_fz_positive.UseVisualStyleBackColor = true;
            // 
            // rbtn_fx_negative
            // 
            this.rbtn_fx_negative.AutoSize = true;
            this.rbtn_fx_negative.Location = new System.Drawing.Point(85, 20);
            this.rbtn_fx_negative.Name = "rbtn_fx_negative";
            this.rbtn_fx_negative.Size = new System.Drawing.Size(58, 17);
            this.rbtn_fx_negative.TabIndex = 0;
            this.rbtn_fx_negative.Text = "FX (-)";
            this.rbtn_fx_negative.UseVisualStyleBackColor = true;
            // 
            // rbtn_fx_positive
            // 
            this.rbtn_fx_positive.AutoSize = true;
            this.rbtn_fx_positive.Checked = true;
            this.rbtn_fx_positive.Location = new System.Drawing.Point(6, 20);
            this.rbtn_fx_positive.Name = "rbtn_fx_positive";
            this.rbtn_fx_positive.Size = new System.Drawing.Size(62, 17);
            this.rbtn_fx_positive.TabIndex = 0;
            this.rbtn_fx_positive.TabStop = true;
            this.rbtn_fx_positive.Text = "FX (+)";
            this.rbtn_fx_positive.UseVisualStyleBackColor = true;
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(114, 179);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(98, 32);
            this.btn_add.TabIndex = 5;
            this.btn_add.Text = "Add";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_jload_add_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(255, 179);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(98, 32);
            this.btn_cancel.TabIndex = 6;
            this.btn_cancel.Text = "Close";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // frmSeismicLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 223);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_sc_cal);
            this.Controls.Add(this.txt_sc);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSeismicLoad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seismic Load";
            this.Load += new System.EventHandler(this.frmSeismicLoad_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_sc_cal;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtn_fz_positive;
        private System.Windows.Forms.RadioButton rbtn_fx_positive;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.RadioButton rbtn_fz_negative;
        private System.Windows.Forms.RadioButton rbtn_fx_negative;
        public System.Windows.Forms.TextBox txt_sc;
    }
}