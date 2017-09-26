namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    partial class frmFloorLoad
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtn_ZLimit = new System.Windows.Forms.RadioButton();
            this.rbtn_YLimit = new System.Windows.Forms.RadioButton();
            this.rbtn_XLimit = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbtn_GZ = new System.Windows.Forms.RadioButton();
            this.rbtn_GY = new System.Windows.Forms.RadioButton();
            this.rbtn_GX = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_pressure = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_X_max = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_X_min = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Y_max = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_Y_min = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_Z_max = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_Z_min = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.chk_One = new System.Windows.Forms.CheckBox();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_fload_add = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtn_ZLimit);
            this.groupBox1.Controls.Add(this.rbtn_YLimit);
            this.groupBox1.Controls.Add(this.rbtn_XLimit);
            this.groupBox1.Location = new System.Drawing.Point(12, 270);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(301, 47);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Visible = false;
            // 
            // rbtn_ZLimit
            // 
            this.rbtn_ZLimit.AutoSize = true;
            this.rbtn_ZLimit.Location = new System.Drawing.Point(215, 20);
            this.rbtn_ZLimit.Name = "rbtn_ZLimit";
            this.rbtn_ZLimit.Size = new System.Drawing.Size(65, 17);
            this.rbtn_ZLimit.TabIndex = 0;
            this.rbtn_ZLimit.Text = "ZLIMIT";
            this.rbtn_ZLimit.UseVisualStyleBackColor = true;
            // 
            // rbtn_YLimit
            // 
            this.rbtn_YLimit.AutoSize = true;
            this.rbtn_YLimit.Checked = true;
            this.rbtn_YLimit.Location = new System.Drawing.Point(124, 20);
            this.rbtn_YLimit.Name = "rbtn_YLimit";
            this.rbtn_YLimit.Size = new System.Drawing.Size(64, 17);
            this.rbtn_YLimit.TabIndex = 0;
            this.rbtn_YLimit.TabStop = true;
            this.rbtn_YLimit.Text = "YLIMIT";
            this.rbtn_YLimit.UseVisualStyleBackColor = true;
            // 
            // rbtn_XLimit
            // 
            this.rbtn_XLimit.AutoSize = true;
            this.rbtn_XLimit.Location = new System.Drawing.Point(17, 20);
            this.rbtn_XLimit.Name = "rbtn_XLimit";
            this.rbtn_XLimit.Size = new System.Drawing.Size(65, 17);
            this.rbtn_XLimit.TabIndex = 0;
            this.rbtn_XLimit.Text = "XLIMIT";
            this.rbtn_XLimit.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbtn_GZ);
            this.groupBox2.Controls.Add(this.rbtn_GY);
            this.groupBox2.Controls.Add(this.rbtn_GX);
            this.groupBox2.Location = new System.Drawing.Point(12, 323);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(301, 49);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Direction";
            this.groupBox2.Visible = false;
            // 
            // rbtn_GZ
            // 
            this.rbtn_GZ.AutoSize = true;
            this.rbtn_GZ.Location = new System.Drawing.Point(215, 20);
            this.rbtn_GZ.Name = "rbtn_GZ";
            this.rbtn_GZ.Size = new System.Drawing.Size(73, 17);
            this.rbtn_GZ.TabIndex = 0;
            this.rbtn_GZ.Text = "Global Z";
            this.rbtn_GZ.UseVisualStyleBackColor = true;
            // 
            // rbtn_GY
            // 
            this.rbtn_GY.AutoSize = true;
            this.rbtn_GY.Checked = true;
            this.rbtn_GY.Location = new System.Drawing.Point(124, 20);
            this.rbtn_GY.Name = "rbtn_GY";
            this.rbtn_GY.Size = new System.Drawing.Size(72, 17);
            this.rbtn_GY.TabIndex = 0;
            this.rbtn_GY.TabStop = true;
            this.rbtn_GY.Text = "Global Y";
            this.rbtn_GY.UseVisualStyleBackColor = true;
            // 
            // rbtn_GX
            // 
            this.rbtn_GX.AutoSize = true;
            this.rbtn_GX.Location = new System.Drawing.Point(17, 20);
            this.rbtn_GX.Name = "rbtn_GX";
            this.rbtn_GX.Size = new System.Drawing.Size(73, 17);
            this.rbtn_GX.TabIndex = 0;
            this.rbtn_GX.Text = "Global X";
            this.rbtn_GX.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pressure";
            // 
            // txt_pressure
            // 
            this.txt_pressure.Location = new System.Drawing.Point(78, 14);
            this.txt_pressure.Name = "txt_pressure";
            this.txt_pressure.Size = new System.Drawing.Size(63, 21);
            this.txt_pressure.TabIndex = 1;
            this.txt_pressure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txt_X_max);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txt_X_min);
            this.groupBox3.Location = new System.Drawing.Point(12, 121);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(301, 52);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Define X Limit";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(159, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Maximum";
            // 
            // txt_X_max
            // 
            this.txt_X_max.Location = new System.Drawing.Point(227, 20);
            this.txt_X_max.Name = "txt_X_max";
            this.txt_X_max.Size = new System.Drawing.Size(63, 21);
            this.txt_X_max.TabIndex = 2;
            this.txt_X_max.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Minimum";
            // 
            // txt_X_min
            // 
            this.txt_X_min.Location = new System.Drawing.Point(78, 20);
            this.txt_X_min.Name = "txt_X_min";
            this.txt_X_min.Size = new System.Drawing.Size(63, 21);
            this.txt_X_min.TabIndex = 2;
            this.txt_X_min.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.txt_Y_max);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.txt_Y_min);
            this.groupBox4.Location = new System.Drawing.Point(12, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(301, 52);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Define Y Limit";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(159, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Maximum";
            // 
            // txt_Y_max
            // 
            this.txt_Y_max.Location = new System.Drawing.Point(227, 20);
            this.txt_Y_max.Name = "txt_Y_max";
            this.txt_Y_max.Size = new System.Drawing.Size(63, 21);
            this.txt_Y_max.TabIndex = 2;
            this.txt_Y_max.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Minimum";
            // 
            // txt_Y_min
            // 
            this.txt_Y_min.Location = new System.Drawing.Point(78, 20);
            this.txt_Y_min.Name = "txt_Y_min";
            this.txt_Y_min.Size = new System.Drawing.Size(63, 21);
            this.txt_Y_min.TabIndex = 2;
            this.txt_Y_min.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.txt_Z_max);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.txt_Z_min);
            this.groupBox5.Location = new System.Drawing.Point(12, 179);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(301, 52);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Define Z Limit";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(159, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Maximum";
            // 
            // txt_Z_max
            // 
            this.txt_Z_max.Location = new System.Drawing.Point(227, 20);
            this.txt_Z_max.Name = "txt_Z_max";
            this.txt_Z_max.Size = new System.Drawing.Size(63, 21);
            this.txt_Z_max.TabIndex = 2;
            this.txt_Z_max.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Minimum";
            // 
            // txt_Z_min
            // 
            this.txt_Z_min.Location = new System.Drawing.Point(78, 20);
            this.txt_Z_min.Name = "txt_Z_min";
            this.txt_Z_min.Size = new System.Drawing.Size(63, 21);
            this.txt_Z_min.TabIndex = 2;
            this.txt_Z_min.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.chk_One);
            this.groupBox6.Controls.Add(this.label1);
            this.groupBox6.Controls.Add(this.txt_pressure);
            this.groupBox6.Location = new System.Drawing.Point(12, 70);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(301, 45);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Load";
            // 
            // chk_One
            // 
            this.chk_One.AutoSize = true;
            this.chk_One.Location = new System.Drawing.Point(147, 16);
            this.chk_One.Name = "chk_One";
            this.chk_One.Size = new System.Drawing.Size(146, 17);
            this.chk_One.TabIndex = 1;
            this.chk_One.Text = "One Way Distribution";
            this.chk_One.UseVisualStyleBackColor = true;
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(174, 237);
            this.btn_close.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(125, 27);
            this.btn_close.TabIndex = 8;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_fload_add
            // 
            this.btn_fload_add.Location = new System.Drawing.Point(29, 237);
            this.btn_fload_add.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btn_fload_add.Name = "btn_fload_add";
            this.btn_fload_add.Size = new System.Drawing.Size(125, 27);
            this.btn_fload_add.TabIndex = 7;
            this.btn_fload_add.Text = "Add Load";
            this.btn_fload_add.UseVisualStyleBackColor = true;
            this.btn_fload_add.Click += new System.EventHandler(this.btn_fload_Click);
            // 
            // frmFloorLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 270);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_fload_add);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFloorLoad";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FLOOR LOAD";
            this.Load += new System.EventHandler(this.frmFloorLoad_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtn_ZLimit;
        private System.Windows.Forms.RadioButton rbtn_YLimit;
        private System.Windows.Forms.RadioButton rbtn_XLimit;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_pressure;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbtn_GZ;
        private System.Windows.Forms.RadioButton rbtn_GY;
        private System.Windows.Forms.RadioButton rbtn_GX;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_X_max;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_X_min;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Y_max;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_Y_min;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_Z_max;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_Z_min;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_fload_add;
        private System.Windows.Forms.CheckBox chk_One;
    }
}