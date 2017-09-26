namespace HEADSNeed.ASTRA.ASTRAForms
{
    partial class frm_ContourModeling
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
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txt_ele_model = new System.Windows.Forms.TextBox();
            this.txt_ele_inc = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_ele_string = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_sec_model = new System.Windows.Forms.TextBox();
            this.txt_sec_inc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_sec_string = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_pri_model = new System.Windows.Forms.TextBox();
            this.txt_pri_inc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_pri_string = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(284, 171);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 15;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(127, 171);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 14;
            this.btn_Save.Text = "Proceed";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txt_ele_model);
            this.groupBox3.Controls.Add(this.txt_ele_inc);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txt_ele_string);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Location = new System.Drawing.Point(12, 118);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(503, 47);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Contour Elevation Text";
            // 
            // txt_ele_model
            // 
            this.txt_ele_model.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ele_model.Location = new System.Drawing.Point(63, 19);
            this.txt_ele_model.Name = "txt_ele_model";
            this.txt_ele_model.Size = new System.Drawing.Size(100, 22);
            this.txt_ele_model.TabIndex = 1;
            this.txt_ele_model.Text = "CONTOUR";
            // 
            // txt_ele_inc
            // 
            this.txt_ele_inc.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ele_inc.Location = new System.Drawing.Point(433, 19);
            this.txt_ele_inc.Name = "txt_ele_inc";
            this.txt_ele_inc.Size = new System.Drawing.Size(61, 22);
            this.txt_ele_inc.TabIndex = 5;
            this.txt_ele_inc.Text = "5.0";
            this.txt_ele_inc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(359, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Increament";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Model";
            // 
            // txt_ele_string
            // 
            this.txt_ele_string.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ele_string.Location = new System.Drawing.Point(231, 19);
            this.txt_ele_string.Name = "txt_ele_string";
            this.txt_ele_string.Size = new System.Drawing.Size(100, 22);
            this.txt_ele_string.TabIndex = 3;
            this.txt_ele_string.Text = "ELEV";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(191, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "String";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_sec_model);
            this.groupBox2.Controls.Add(this.txt_sec_inc);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txt_sec_string);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(12, 65);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(503, 47);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Secondary Contour Model";
            // 
            // txt_sec_model
            // 
            this.txt_sec_model.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_sec_model.Location = new System.Drawing.Point(63, 19);
            this.txt_sec_model.Name = "txt_sec_model";
            this.txt_sec_model.Size = new System.Drawing.Size(100, 22);
            this.txt_sec_model.TabIndex = 1;
            this.txt_sec_model.Text = "CONTOUR";
            // 
            // txt_sec_inc
            // 
            this.txt_sec_inc.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_sec_inc.Location = new System.Drawing.Point(433, 19);
            this.txt_sec_inc.Name = "txt_sec_inc";
            this.txt_sec_inc.Size = new System.Drawing.Size(61, 22);
            this.txt_sec_inc.TabIndex = 5;
            this.txt_sec_inc.Text = "5.0";
            this.txt_sec_inc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(359, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Increament";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Model";
            // 
            // txt_sec_string
            // 
            this.txt_sec_string.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_sec_string.Location = new System.Drawing.Point(231, 19);
            this.txt_sec_string.Name = "txt_sec_string";
            this.txt_sec_string.Size = new System.Drawing.Size(100, 22);
            this.txt_sec_string.TabIndex = 3;
            this.txt_sec_string.Text = "C005";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(191, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "String";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_pri_model);
            this.groupBox1.Controls.Add(this.txt_pri_inc);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_pri_string);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(503, 47);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Primary Contour Model";
            // 
            // txt_pri_model
            // 
            this.txt_pri_model.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_pri_model.Location = new System.Drawing.Point(63, 19);
            this.txt_pri_model.Name = "txt_pri_model";
            this.txt_pri_model.Size = new System.Drawing.Size(100, 22);
            this.txt_pri_model.TabIndex = 1;
            this.txt_pri_model.Text = "CONTOUR";
            // 
            // txt_pri_inc
            // 
            this.txt_pri_inc.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_pri_inc.Location = new System.Drawing.Point(433, 19);
            this.txt_pri_inc.Name = "txt_pri_inc";
            this.txt_pri_inc.Size = new System.Drawing.Size(61, 22);
            this.txt_pri_inc.TabIndex = 5;
            this.txt_pri_inc.Text = "1.0";
            this.txt_pri_inc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_pri_inc.Click += new System.EventHandler(this.txt_pri_inc_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(359, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Increament";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Model";
            // 
            // txt_pri_string
            // 
            this.txt_pri_string.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_pri_string.Location = new System.Drawing.Point(231, 19);
            this.txt_pri_string.Name = "txt_pri_string";
            this.txt_pri_string.Size = new System.Drawing.Size(100, 22);
            this.txt_pri_string.TabIndex = 3;
            this.txt_pri_string.Text = "C001";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(191, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "String";
            // 
            // frm_ContourModeling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 198);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frm_ContourModeling";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Contour Modeling";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txt_ele_model;
        private System.Windows.Forms.TextBox txt_ele_inc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_ele_string;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_sec_model;
        private System.Windows.Forms.TextBox txt_sec_inc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_sec_string;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_pri_model;
        private System.Windows.Forms.TextBox txt_pri_inc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_pri_string;
        private System.Windows.Forms.Label label2;
    }
}