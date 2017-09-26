namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    partial class frmMemberTruss
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
            this.lbl_txt = new System.Windows.Forms.Label();
            this.txt_mem_nos = new System.Windows.Forms.TextBox();
            this.btn_add_data = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.cmb_range = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lbl_txt
            // 
            this.lbl_txt.AutoSize = true;
            this.lbl_txt.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_txt.Location = new System.Drawing.Point(111, 20);
            this.lbl_txt.Name = "lbl_txt";
            this.lbl_txt.Size = new System.Drawing.Size(108, 13);
            this.lbl_txt.TabIndex = 0;
            this.lbl_txt.Text = "Member Numbers";
            // 
            // txt_mem_nos
            // 
            this.txt_mem_nos.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_mem_nos.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_mem_nos.Location = new System.Drawing.Point(114, 36);
            this.txt_mem_nos.Name = "txt_mem_nos";
            this.txt_mem_nos.Size = new System.Drawing.Size(409, 22);
            this.txt_mem_nos.TabIndex = 1;
            // 
            // btn_add_data
            // 
            this.btn_add_data.Location = new System.Drawing.Point(137, 85);
            this.btn_add_data.Name = "btn_add_data";
            this.btn_add_data.Size = new System.Drawing.Size(97, 34);
            this.btn_add_data.TabIndex = 2;
            this.btn_add_data.Text = "Add Data";
            this.btn_add_data.UseVisualStyleBackColor = true;
            this.btn_add_data.Click += new System.EventHandler(this.btn_add_data_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(275, 85);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(97, 34);
            this.btn_cancel.TabIndex = 3;
            this.btn_cancel.Text = "FINISH";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // cmb_range
            // 
            this.cmb_range.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_range.FormattingEnabled = true;
            this.cmb_range.Items.AddRange(new object[] {
            "ALL",
            "NUMBERS"});
            this.cmb_range.Location = new System.Drawing.Point(12, 37);
            this.cmb_range.Name = "cmb_range";
            this.cmb_range.Size = new System.Drawing.Size(93, 21);
            this.cmb_range.TabIndex = 4;
            this.cmb_range.SelectedIndexChanged += new System.EventHandler(this.cmb_range_SelectedIndexChanged);
            // 
            // frmMemberTruss
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 127);
            this.Controls.Add(this.cmb_range);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_add_data);
            this.Controls.Add(this.txt_mem_nos);
            this.Controls.Add(this.lbl_txt);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMemberTruss";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Member Truss";
            this.Load += new System.EventHandler(this.frmMemberTruss_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_txt;
        private System.Windows.Forms.TextBox txt_mem_nos;
        private System.Windows.Forms.Button btn_add_data;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.ComboBox cmb_range;
    }
}