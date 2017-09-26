namespace HEADSNeed.ASTRA.ASTRAForms
{
    partial class frm_Intact_Diag1
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
            this.pcb = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtn_4_side = new System.Windows.Forms.RadioButton();
            this.rbtn_2_side = new System.Windows.Forms.RadioButton();
            this.txt_rebar_nos = new System.Windows.Forms.TextBox();
            this.txt_rebar_fck = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_proceed = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_d_D = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcb)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pcb
            // 
            this.pcb.BackgroundImage = global::HEADSNeed.Properties.Resources.Reinf_4_side;
            this.pcb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pcb.Location = new System.Drawing.Point(15, 179);
            this.pcb.Name = "pcb";
            this.pcb.Size = new System.Drawing.Size(320, 214);
            this.pcb.TabIndex = 0;
            this.pcb.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "Number of Main Steel \r\nReinforcement Bars ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Steel Grade [fy]";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtn_4_side);
            this.groupBox1.Controls.Add(this.rbtn_2_side);
            this.groupBox1.Location = new System.Drawing.Point(15, 126);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 47);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // rbtn_4_side
            // 
            this.rbtn_4_side.AutoSize = true;
            this.rbtn_4_side.Checked = true;
            this.rbtn_4_side.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_4_side.Location = new System.Drawing.Point(79, 20);
            this.rbtn_4_side.Name = "rbtn_4_side";
            this.rbtn_4_side.Size = new System.Drawing.Size(74, 18);
            this.rbtn_4_side.TabIndex = 0;
            this.rbtn_4_side.TabStop = true;
            this.rbtn_4_side.Text = "4 Sides";
            this.rbtn_4_side.UseVisualStyleBackColor = true;
            this.rbtn_4_side.CheckedChanged += new System.EventHandler(this.rbtn_side_CheckedChanged);
            // 
            // rbtn_2_side
            // 
            this.rbtn_2_side.AutoSize = true;
            this.rbtn_2_side.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_2_side.Location = new System.Drawing.Point(184, 20);
            this.rbtn_2_side.Name = "rbtn_2_side";
            this.rbtn_2_side.Size = new System.Drawing.Size(74, 18);
            this.rbtn_2_side.TabIndex = 0;
            this.rbtn_2_side.Text = "2 Sides";
            this.rbtn_2_side.UseVisualStyleBackColor = true;
            this.rbtn_2_side.CheckedChanged += new System.EventHandler(this.rbtn_side_CheckedChanged);
            // 
            // txt_rebar_nos
            // 
            this.txt_rebar_nos.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_rebar_nos.ForeColor = System.Drawing.Color.Blue;
            this.txt_rebar_nos.Location = new System.Drawing.Point(181, 25);
            this.txt_rebar_nos.Name = "txt_rebar_nos";
            this.txt_rebar_nos.Size = new System.Drawing.Size(68, 22);
            this.txt_rebar_nos.TabIndex = 3;
            this.txt_rebar_nos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_rebar_fck
            // 
            this.txt_rebar_fck.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_rebar_fck.ForeColor = System.Drawing.Color.Blue;
            this.txt_rebar_fck.Location = new System.Drawing.Point(181, 52);
            this.txt_rebar_fck.Name = "txt_rebar_fck";
            this.txt_rebar_fck.Size = new System.Drawing.Size(68, 22);
            this.txt_rebar_fck.TabIndex = 3;
            this.txt_rebar_fck.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(155, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "Fe";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(255, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 14);
            this.label4.TabIndex = 5;
            this.label4.Text = "nos";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_proceed
            // 
            this.btn_proceed.Location = new System.Drawing.Point(75, 399);
            this.btn_proceed.Name = "btn_proceed";
            this.btn_proceed.Size = new System.Drawing.Size(103, 32);
            this.btn_proceed.TabIndex = 6;
            this.btn_proceed.Text = "Proceed";
            this.btn_proceed.UseVisualStyleBackColor = true;
            this.btn_proceed.Click += new System.EventHandler(this.btn_proceed_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(184, 399);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(103, 32);
            this.btn_cancel.TabIndex = 6;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_proceed_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "d’/D = ";
            // 
            // txt_d_D
            // 
            this.txt_d_D.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_d_D.ForeColor = System.Drawing.Color.Blue;
            this.txt_d_D.Location = new System.Drawing.Point(181, 80);
            this.txt_d_D.Name = "txt_d_D";
            this.txt_d_D.Size = new System.Drawing.Size(68, 22);
            this.txt_d_D.TabIndex = 3;
            this.txt_d_D.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // frm_Intact_Diag1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 444);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_proceed);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_d_D);
            this.Controls.Add(this.txt_rebar_fck);
            this.Controls.Add(this.txt_rebar_nos);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pcb);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Intact_Diag1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Column Design";
            this.Load += new System.EventHandler(this.frm_Intact_Diag1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcb)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pcb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtn_4_side;
        private System.Windows.Forms.RadioButton rbtn_2_side;
        private System.Windows.Forms.TextBox txt_rebar_nos;
        private System.Windows.Forms.TextBox txt_rebar_fck;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_proceed;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_d_D;
    }
}