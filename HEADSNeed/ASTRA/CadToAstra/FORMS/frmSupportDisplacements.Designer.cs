namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    partial class frmSupportDisplacements
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
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_jload_add = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_mz = new System.Windows.Forms.TextBox();
            this.txt_fz = new System.Windows.Forms.TextBox();
            this.txt_my = new System.Windows.Forms.TextBox();
            this.txt_mx = new System.Windows.Forms.TextBox();
            this.txt_fy = new System.Windows.Forms.TextBox();
            this.txt_fx = new System.Windows.Forms.TextBox();
            this.txt_joint_number = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(224, 173);
            this.btn_close.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(125, 32);
            this.btn_close.TabIndex = 5;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_jload_add
            // 
            this.btn_jload_add.Location = new System.Drawing.Point(84, 173);
            this.btn_jload_add.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_jload_add.Name = "btn_jload_add";
            this.btn_jload_add.Size = new System.Drawing.Size(131, 32);
            this.btn_jload_add.TabIndex = 4;
            this.btn_jload_add.Text = "Add Load";
            this.btn_jload_add.UseVisualStyleBackColor = true;
            this.btn_jload_add.Click += new System.EventHandler(this.btn_sdload_add_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label5);
            this.groupBox8.Controls.Add(this.label6);
            this.groupBox8.Controls.Add(this.label2);
            this.groupBox8.Controls.Add(this.label4);
            this.groupBox8.Controls.Add(this.label1);
            this.groupBox8.Controls.Add(this.label3);
            this.groupBox8.Controls.Add(this.label9);
            this.groupBox8.Controls.Add(this.txt_mz);
            this.groupBox8.Controls.Add(this.txt_fz);
            this.groupBox8.Controls.Add(this.txt_my);
            this.groupBox8.Controls.Add(this.txt_mx);
            this.groupBox8.Controls.Add(this.txt_fy);
            this.groupBox8.Controls.Add(this.txt_fx);
            this.groupBox8.Controls.Add(this.txt_joint_number);
            this.groupBox8.Controls.Add(this.label8);
            this.groupBox8.Location = new System.Drawing.Point(13, 12);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox8.Size = new System.Drawing.Size(423, 155);
            this.groupBox8.TabIndex = 3;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Support Displacement Load";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(310, 121);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 14);
            this.label5.TabIndex = 30;
            this.label5.Text = "MZ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(146, 12);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(246, 14);
            this.label6.TabIndex = 49;
            this.label6.Text = "Seperated by comma (\',\') or space (\' \')";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(310, 78);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 14);
            this.label2.TabIndex = 30;
            this.label2.Text = "FZ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(164, 121);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 14);
            this.label4.TabIndex = 30;
            this.label4.Text = "MY";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(164, 78);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 14);
            this.label1.TabIndex = 30;
            this.label1.Text = "FY";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 121);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 14);
            this.label3.TabIndex = 30;
            this.label3.Text = "MX";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 78);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(22, 14);
            this.label9.TabIndex = 30;
            this.label9.Text = "FX";
            // 
            // txt_mz
            // 
            this.txt_mz.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_mz.Location = new System.Drawing.Point(340, 117);
            this.txt_mz.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_mz.Name = "txt_mz";
            this.txt_mz.Size = new System.Drawing.Size(65, 21);
            this.txt_mz.TabIndex = 6;
            this.txt_mz.Text = "0.0";
            this.txt_mz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_fz
            // 
            this.txt_fz.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_fz.Location = new System.Drawing.Point(340, 74);
            this.txt_fz.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_fz.Name = "txt_fz";
            this.txt_fz.Size = new System.Drawing.Size(65, 21);
            this.txt_fz.TabIndex = 3;
            this.txt_fz.Text = "0.0";
            this.txt_fz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_my
            // 
            this.txt_my.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_my.Location = new System.Drawing.Point(191, 117);
            this.txt_my.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_my.Name = "txt_my";
            this.txt_my.Size = new System.Drawing.Size(65, 21);
            this.txt_my.TabIndex = 5;
            this.txt_my.Text = "0.0";
            this.txt_my.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_mx
            // 
            this.txt_mx.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_mx.Location = new System.Drawing.Point(57, 117);
            this.txt_mx.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_mx.Name = "txt_mx";
            this.txt_mx.Size = new System.Drawing.Size(65, 21);
            this.txt_mx.TabIndex = 4;
            this.txt_mx.Text = "0.0";
            this.txt_mx.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_fy
            // 
            this.txt_fy.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_fy.Location = new System.Drawing.Point(191, 74);
            this.txt_fy.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_fy.Name = "txt_fy";
            this.txt_fy.Size = new System.Drawing.Size(65, 21);
            this.txt_fy.TabIndex = 2;
            this.txt_fy.Text = "0.0";
            this.txt_fy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_fx
            // 
            this.txt_fx.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_fx.Location = new System.Drawing.Point(57, 74);
            this.txt_fx.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_fx.Name = "txt_fx";
            this.txt_fx.Size = new System.Drawing.Size(65, 21);
            this.txt_fx.TabIndex = 1;
            this.txt_fx.Text = "0.0";
            this.txt_fx.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_joint_number
            // 
            this.txt_joint_number.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_joint_number.Location = new System.Drawing.Point(134, 29);
            this.txt_joint_number.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txt_joint_number.Name = "txt_joint_number";
            this.txt_joint_number.Size = new System.Drawing.Size(271, 22);
            this.txt_joint_number.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 32);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 14);
            this.label8.TabIndex = 29;
            this.label8.Text = "Support Numbers";
            // 
            // frmSupportDisplacements
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 212);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_jload_add);
            this.Controls.Add(this.groupBox8);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSupportDisplacements";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Support Displacement Load";
            this.Load += new System.EventHandler(this.frmSupportDisplacements_Load);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_jload_add;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_mz;
        private System.Windows.Forms.TextBox txt_fz;
        private System.Windows.Forms.TextBox txt_my;
        private System.Windows.Forms.TextBox txt_mx;
        private System.Windows.Forms.TextBox txt_fy;
        private System.Windows.Forms.TextBox txt_fx;
        private System.Windows.Forms.TextBox txt_joint_number;
        private System.Windows.Forms.Label label8;
    }
}