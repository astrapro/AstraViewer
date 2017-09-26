namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    partial class frmTempLoad
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
            this.btn_jload_add = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_final_temp = new System.Windows.Forms.TextBox();
            this.txt_init_temp = new System.Windows.Forms.TextBox();
            this.txt_joint_number = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_close = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_jload_add
            // 
            this.btn_jload_add.Location = new System.Drawing.Point(109, 116);
            this.btn_jload_add.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btn_jload_add.Name = "btn_jload_add";
            this.btn_jload_add.Size = new System.Drawing.Size(125, 25);
            this.btn_jload_add.TabIndex = 32;
            this.btn_jload_add.Text = "Add Load";
            this.btn_jload_add.UseVisualStyleBackColor = true;
            this.btn_jload_add.Click += new System.EventHandler(this.btn_jload_add_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label1);
            this.groupBox8.Controls.Add(this.label6);
            this.groupBox8.Controls.Add(this.label9);
            this.groupBox8.Controls.Add(this.txt_final_temp);
            this.groupBox8.Controls.Add(this.txt_init_temp);
            this.groupBox8.Controls.Add(this.txt_joint_number);
            this.groupBox8.Controls.Add(this.label8);
            this.groupBox8.Location = new System.Drawing.Point(5, 2);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.groupBox8.Size = new System.Drawing.Size(475, 95);
            this.groupBox8.TabIndex = 33;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Temperature Load";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(256, 66);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Final Temperature";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 66);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(116, 13);
            this.label9.TabIndex = 30;
            this.label9.Text = "Initial Temperature";
            // 
            // txt_final_temp
            // 
            this.txt_final_temp.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_final_temp.Location = new System.Drawing.Point(376, 63);
            this.txt_final_temp.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.txt_final_temp.Name = "txt_final_temp";
            this.txt_final_temp.Size = new System.Drawing.Size(80, 21);
            this.txt_final_temp.TabIndex = 29;
            this.txt_final_temp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_init_temp
            // 
            this.txt_init_temp.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_init_temp.Location = new System.Drawing.Point(141, 63);
            this.txt_init_temp.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.txt_init_temp.Name = "txt_init_temp";
            this.txt_init_temp.Size = new System.Drawing.Size(80, 21);
            this.txt_init_temp.TabIndex = 29;
            this.txt_init_temp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_joint_number
            // 
            this.txt_joint_number.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_joint_number.Location = new System.Drawing.Point(141, 36);
            this.txt_joint_number.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.txt_joint_number.Name = "txt_joint_number";
            this.txt_joint_number.Size = new System.Drawing.Size(315, 21);
            this.txt_joint_number.TabIndex = 29;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 39);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Joint Numbers";
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(252, 116);
            this.btn_close.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(125, 25);
            this.btn_close.TabIndex = 50;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(138, 20);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(231, 13);
            this.label6.TabIndex = 51;
            this.label6.Text = "Seperated by comma (\',\') or space (\' \')";
            // 
            // frmTempLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 150);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_jload_add);
            this.Controls.Add(this.groupBox8);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTempLoad";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Temperature Load";
            this.Load += new System.EventHandler(this.frmTempLoad_Load);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_jload_add;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_init_temp;
        private System.Windows.Forms.TextBox txt_joint_number;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_final_temp;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_close;
    }
}