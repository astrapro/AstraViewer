﻿namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    partial class frmElementLoad
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_mnos = new System.Windows.Forms.TextBox();
            this.btn_aload_add = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_aload_val = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(276, 65);
            this.btn_close.Margin = new System.Windows.Forms.Padding(7, 3, 7, 3);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(122, 29);
            this.btn_close.TabIndex = 53;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(169, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(246, 14);
            this.label1.TabIndex = 52;
            this.label1.Text = "Seperated by comma (\',\') or space (\' \')";
            // 
            // txt_mnos
            // 
            this.txt_mnos.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_mnos.Location = new System.Drawing.Point(5, 37);
            this.txt_mnos.Margin = new System.Windows.Forms.Padding(7, 3, 7, 3);
            this.txt_mnos.Name = "txt_mnos";
            this.txt_mnos.Size = new System.Drawing.Size(425, 22);
            this.txt_mnos.TabIndex = 51;
            // 
            // btn_aload_add
            // 
            this.btn_aload_add.Location = new System.Drawing.Point(140, 65);
            this.btn_aload_add.Margin = new System.Windows.Forms.Padding(7, 3, 7, 3);
            this.btn_aload_add.Name = "btn_aload_add";
            this.btn_aload_add.Size = new System.Drawing.Size(122, 29);
            this.btn_aload_add.TabIndex = 49;
            this.btn_aload_add.Text = "Add Load";
            this.btn_aload_add.UseVisualStyleBackColor = true;
            this.btn_aload_add.Click += new System.EventHandler(this.btn_aload_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(2, 20);
            this.label10.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(118, 14);
            this.label10.TabIndex = 50;
            this.label10.Text = "Element Numbers";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(447, 21);
            this.label7.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 14);
            this.label7.TabIndex = 48;
            this.label7.Text = "PRESSURE";
            // 
            // txt_aload_val
            // 
            this.txt_aload_val.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_aload_val.Location = new System.Drawing.Point(435, 38);
            this.txt_aload_val.Margin = new System.Windows.Forms.Padding(7, 3, 7, 3);
            this.txt_aload_val.Name = "txt_aload_val";
            this.txt_aload_val.Size = new System.Drawing.Size(97, 21);
            this.txt_aload_val.TabIndex = 47;
            this.txt_aload_val.Text = "-2.8";
            this.txt_aload_val.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // frmElementLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 101);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_mnos);
            this.Controls.Add(this.btn_aload_add);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txt_aload_val);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmElementLoad";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Element Load";
            this.Load += new System.EventHandler(this.frmElementLoad_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_mnos;
        private System.Windows.Forms.Button btn_aload_add;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_aload_val;
    }
}