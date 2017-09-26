namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    partial class frmElementProp
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
            this.txt_thk_val = new System.Windows.Forms.TextBox();
            this.txt_DEN_val = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_EXX_val = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_EXY_val = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_EXG_val = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_EYY_val = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_EYG_val = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_GXY_val = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(258, 145);
            this.btn_close.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(142, 29);
            this.btn_close.TabIndex = 60;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(120, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 13);
            this.label1.TabIndex = 59;
            this.label1.Text = "Seperated by comma (\',\') or space (\' \')";
            // 
            // txt_mnos
            // 
            this.txt_mnos.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_mnos.Location = new System.Drawing.Point(13, 39);
            this.txt_mnos.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.txt_mnos.Name = "txt_mnos";
            this.txt_mnos.Size = new System.Drawing.Size(338, 21);
            this.txt_mnos.TabIndex = 58;
            // 
            // btn_aload_add
            // 
            this.btn_aload_add.Location = new System.Drawing.Point(99, 145);
            this.btn_aload_add.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.btn_aload_add.Name = "btn_aload_add";
            this.btn_aload_add.Size = new System.Drawing.Size(142, 29);
            this.btn_aload_add.TabIndex = 56;
            this.btn_aload_add.Text = "Add Load";
            this.btn_aload_add.UseVisualStyleBackColor = true;
            this.btn_aload_add.Click += new System.EventHandler(this.btn_aload_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 22);
            this.label10.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(108, 13);
            this.label10.TabIndex = 57;
            this.label10.Text = "Element Numbers";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(355, 22);
            this.label7.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 55;
            this.label7.Text = "Thickness";
            // 
            // txt_thk_val
            // 
            this.txt_thk_val.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_thk_val.Location = new System.Drawing.Point(358, 39);
            this.txt_thk_val.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.txt_thk_val.Name = "txt_thk_val";
            this.txt_thk_val.Size = new System.Drawing.Size(56, 21);
            this.txt_thk_val.TabIndex = 54;
            this.txt_thk_val.Text = "-2.8";
            this.txt_thk_val.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_DEN_val
            // 
            this.txt_DEN_val.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_DEN_val.Location = new System.Drawing.Point(417, 39);
            this.txt_DEN_val.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.txt_DEN_val.Name = "txt_DEN_val";
            this.txt_DEN_val.Size = new System.Drawing.Size(56, 21);
            this.txt_DEN_val.TabIndex = 54;
            this.txt_DEN_val.Text = "0.0";
            this.txt_DEN_val.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(423, 23);
            this.label2.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 55;
            this.label2.Text = "Density";
            // 
            // txt_EXX_val
            // 
            this.txt_EXX_val.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_EXX_val.Location = new System.Drawing.Point(46, 69);
            this.txt_EXX_val.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.txt_EXX_val.Name = "txt_EXX_val";
            this.txt_EXX_val.Size = new System.Drawing.Size(100, 21);
            this.txt_EXX_val.TabIndex = 54;
            this.txt_EXX_val.Text = "0.0";
            this.txt_EXX_val.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 72);
            this.label3.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 55;
            this.label3.Text = "EXX";
            // 
            // txt_EXY_val
            // 
            this.txt_EXY_val.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_EXY_val.Location = new System.Drawing.Point(210, 69);
            this.txt_EXY_val.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.txt_EXY_val.Name = "txt_EXY_val";
            this.txt_EXY_val.Size = new System.Drawing.Size(100, 21);
            this.txt_EXY_val.TabIndex = 54;
            this.txt_EXY_val.Text = "0.0";
            this.txt_EXY_val.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(174, 74);
            this.label4.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 55;
            this.label4.Text = "EXY";
            // 
            // txt_EXG_val
            // 
            this.txt_EXG_val.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_EXG_val.Location = new System.Drawing.Point(373, 69);
            this.txt_EXG_val.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.txt_EXG_val.Name = "txt_EXG_val";
            this.txt_EXG_val.Size = new System.Drawing.Size(100, 21);
            this.txt_EXG_val.TabIndex = 54;
            this.txt_EXG_val.Text = "0.0";
            this.txt_EXG_val.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(331, 72);
            this.label5.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 55;
            this.label5.Text = "EXG ";
            // 
            // txt_EYY_val
            // 
            this.txt_EYY_val.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_EYY_val.Location = new System.Drawing.Point(46, 98);
            this.txt_EYY_val.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.txt_EYY_val.Name = "txt_EYY_val";
            this.txt_EYY_val.Size = new System.Drawing.Size(100, 21);
            this.txt_EYY_val.TabIndex = 54;
            this.txt_EYY_val.Text = "0.0";
            this.txt_EYY_val.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 101);
            this.label6.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 55;
            this.label6.Text = "EYY";
            // 
            // txt_EYG_val
            // 
            this.txt_EYG_val.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_EYG_val.Location = new System.Drawing.Point(210, 98);
            this.txt_EYG_val.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.txt_EYG_val.Name = "txt_EYG_val";
            this.txt_EYG_val.Size = new System.Drawing.Size(100, 21);
            this.txt_EYG_val.TabIndex = 54;
            this.txt_EYG_val.Text = "0.0";
            this.txt_EYG_val.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(173, 101);
            this.label8.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 13);
            this.label8.TabIndex = 55;
            this.label8.Text = "EYG";
            // 
            // txt_GXY_val
            // 
            this.txt_GXY_val.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_GXY_val.Location = new System.Drawing.Point(373, 98);
            this.txt_GXY_val.Margin = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.txt_GXY_val.Name = "txt_GXY_val";
            this.txt_GXY_val.Size = new System.Drawing.Size(100, 21);
            this.txt_GXY_val.TabIndex = 54;
            this.txt_GXY_val.Text = "0.0";
            this.txt_GXY_val.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(335, 101);
            this.label9.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 55;
            this.label9.Text = "GXY";
            // 
            // frmElementProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 180);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_mnos);
            this.Controls.Add(this.btn_aload_add);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txt_GXY_val);
            this.Controls.Add(this.txt_EYG_val);
            this.Controls.Add(this.txt_EYY_val);
            this.Controls.Add(this.txt_EXG_val);
            this.Controls.Add(this.txt_EXY_val);
            this.Controls.Add(this.txt_EXX_val);
            this.Controls.Add(this.txt_DEN_val);
            this.Controls.Add(this.txt_thk_val);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmElementProp";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Element Property";
            this.Load += new System.EventHandler(this.frmElementProp_Load);
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
        private System.Windows.Forms.TextBox txt_thk_val;
        private System.Windows.Forms.TextBox txt_DEN_val;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_EXX_val;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_EXY_val;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_EXG_val;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_EYY_val;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_EYG_val;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_GXY_val;
        private System.Windows.Forms.Label label9;
    }
}