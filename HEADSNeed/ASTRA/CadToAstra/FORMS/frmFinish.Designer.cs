namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    partial class frmFinish
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
            this.btn_add_data = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.chk_print_support = new System.Windows.Forms.CheckBox();
            this.chk_print_max_force = new System.Windows.Forms.CheckBox();
            this.txt_max = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_add_data
            // 
            this.btn_add_data.Location = new System.Drawing.Point(136, 59);
            this.btn_add_data.Name = "btn_add_data";
            this.btn_add_data.Size = new System.Drawing.Size(87, 23);
            this.btn_add_data.TabIndex = 2;
            this.btn_add_data.Text = "Add Data";
            this.btn_add_data.UseVisualStyleBackColor = true;
            this.btn_add_data.Click += new System.EventHandler(this.btn_add_data_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(231, 59);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(87, 23);
            this.btn_cancel.TabIndex = 3;
            this.btn_cancel.Text = "FINISH";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // chk_print_support
            // 
            this.chk_print_support.AutoSize = true;
            this.chk_print_support.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_print_support.Location = new System.Drawing.Point(14, 12);
            this.chk_print_support.Name = "chk_print_support";
            this.chk_print_support.Size = new System.Drawing.Size(201, 18);
            this.chk_print_support.TabIndex = 4;
            this.chk_print_support.Text = "PRINT SUPPORT REACTIONS";
            this.chk_print_support.UseVisualStyleBackColor = true;
            // 
            // chk_print_max_force
            // 
            this.chk_print_max_force.AutoSize = true;
            this.chk_print_max_force.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_print_max_force.Location = new System.Drawing.Point(14, 35);
            this.chk_print_max_force.Name = "chk_print_max_force";
            this.chk_print_max_force.Size = new System.Drawing.Size(240, 18);
            this.chk_print_max_force.TabIndex = 5;
            this.chk_print_max_force.Text = "PRINT MAX FORCE ENVELOPE LIST";
            this.chk_print_max_force.UseVisualStyleBackColor = true;
            // 
            // txt_max
            // 
            this.txt_max.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_max.Location = new System.Drawing.Point(301, 33);
            this.txt_max.Name = "txt_max";
            this.txt_max.Size = new System.Drawing.Size(149, 21);
            this.txt_max.TabIndex = 6;
            this.txt_max.Text = "1 TO 21";
            // 
            // frmFinish
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 90);
            this.Controls.Add(this.txt_max);
            this.Controls.Add(this.chk_print_max_force);
            this.Controls.Add(this.chk_print_support);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_add_data);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFinish";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Finish";
            this.Load += new System.EventHandler(this.frmFinish_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_add_data;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.CheckBox chk_print_support;
        private System.Windows.Forms.CheckBox chk_print_max_force;
        private System.Windows.Forms.TextBox txt_max;
    }
}