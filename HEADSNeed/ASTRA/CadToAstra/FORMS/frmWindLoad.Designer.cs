namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    partial class frmWindLoad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWindLoad));
            this.label1 = new System.Windows.Forms.Label();
            this.txt_velocity = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_pressure = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_area_factor = new System.Windows.Forms.TextBox();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_add = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtn_fz_negative = new System.Windows.Forms.RadioButton();
            this.rbtn_fz_positive = new System.Windows.Forms.RadioButton();
            this.rbtn_fx_negative = new System.Windows.Forms.RadioButton();
            this.rbtn_fx_positive = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 70);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Wind Velocity";
            // 
            // txt_velocity
            // 
            this.txt_velocity.Location = new System.Drawing.Point(157, 67);
            this.txt_velocity.Name = "txt_velocity";
            this.txt_velocity.Size = new System.Drawing.Size(69, 21);
            this.txt_velocity.TabIndex = 1;
            this.txt_velocity.Text = "30.0";
            this.txt_velocity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(233, 70);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "metres/sec";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 97);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Wind Pressure";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(233, 97);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "N/Sq.mm";
            // 
            // txt_pressure
            // 
            this.txt_pressure.Location = new System.Drawing.Point(157, 94);
            this.txt_pressure.Name = "txt_pressure";
            this.txt_pressure.Size = new System.Drawing.Size(69, 21);
            this.txt_pressure.TabIndex = 1;
            this.txt_pressure.Text = "540";
            this.txt_pressure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 124);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Effective Area Factor ";
            // 
            // txt_area_factor
            // 
            this.txt_area_factor.Location = new System.Drawing.Point(157, 121);
            this.txt_area_factor.Name = "txt_area_factor";
            this.txt_area_factor.Size = new System.Drawing.Size(69, 21);
            this.txt_area_factor.TabIndex = 1;
            this.txt_area_factor.Text = "0.60";
            this.txt_area_factor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(186, 148);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(98, 32);
            this.btn_cancel.TabIndex = 8;
            this.btn_cancel.Text = "Close";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(45, 148);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(98, 32);
            this.btn_add.TabIndex = 7;
            this.btn_add.Text = "Add";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_jload_add_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtn_fz_negative);
            this.groupBox1.Controls.Add(this.rbtn_fz_positive);
            this.groupBox1.Controls.Add(this.rbtn_fx_negative);
            this.groupBox1.Controls.Add(this.rbtn_fx_positive);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(304, 47);
            this.groupBox1.TabIndex = 9;
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
            // frmWindLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 192);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.txt_area_factor);
            this.Controls.Add(this.txt_pressure);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_velocity);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmWindLoad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wind Load";
            this.Click += new System.EventHandler(this.frmWindLoad_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_velocity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_pressure;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_area_factor;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtn_fz_negative;
        private System.Windows.Forms.RadioButton rbtn_fz_positive;
        private System.Windows.Forms.RadioButton rbtn_fx_negative;
        private System.Windows.Forms.RadioButton rbtn_fx_positive;
    }
}