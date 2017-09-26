namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    partial class frmLoadCase
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
            this.txt_load_case = new System.Windows.Forms.TextBox();
            this.txt_load_title = new System.Windows.Forms.TextBox();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_jload_add
            // 
            this.btn_jload_add.Location = new System.Drawing.Point(127, 81);
            this.btn_jload_add.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btn_jload_add.Name = "btn_jload_add";
            this.btn_jload_add.Size = new System.Drawing.Size(153, 25);
            this.btn_jload_add.TabIndex = 32;
            this.btn_jload_add.Text = "Add Load Case";
            this.btn_jload_add.UseVisualStyleBackColor = true;
            this.btn_jload_add.Click += new System.EventHandler(this.btn_jload_add_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label1);
            this.groupBox8.Controls.Add(this.label9);
            this.groupBox8.Controls.Add(this.txt_load_case);
            this.groupBox8.Controls.Add(this.txt_load_title);
            this.groupBox8.Location = new System.Drawing.Point(3, 1);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.groupBox8.Size = new System.Drawing.Size(400, 74);
            this.groupBox8.TabIndex = 33;
            this.groupBox8.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 49);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Load Title";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 17);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 13);
            this.label9.TabIndex = 30;
            this.label9.Text = "Load Case";
            // 
            // txt_load_case
            // 
            this.txt_load_case.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_load_case.Location = new System.Drawing.Point(87, 14);
            this.txt_load_case.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.txt_load_case.Name = "txt_load_case";
            this.txt_load_case.Size = new System.Drawing.Size(70, 21);
            this.txt_load_case.TabIndex = 29;
            this.txt_load_case.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_load_title
            // 
            this.txt_load_title.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_load_title.Location = new System.Drawing.Point(87, 46);
            this.txt_load_title.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.txt_load_title.Name = "txt_load_title";
            this.txt_load_title.Size = new System.Drawing.Size(305, 21);
            this.txt_load_title.TabIndex = 29;
            // 
            // frmLoadCase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 107);
            this.Controls.Add(this.btn_jload_add);
            this.Controls.Add(this.groupBox8);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLoadCase";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Define Load Case";
            this.Load += new System.EventHandler(this.frmLoadCase_Load);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_jload_add;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_load_title;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txt_load_case;
    }
}