namespace HEADSNeed.ASTRA.ASTRAForms
{
    partial class frmCatchmentAreaVolume
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
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_Proceed = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmb_storage_type = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(188, 63);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(87, 23);
            this.btn_cancel.TabIndex = 3;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_Proceed
            // 
            this.btn_Proceed.Location = new System.Drawing.Point(22, 63);
            this.btn_Proceed.Name = "btn_Proceed";
            this.btn_Proceed.Size = new System.Drawing.Size(87, 23);
            this.btn_Proceed.TabIndex = 2;
            this.btn_Proceed.Text = "Proceed";
            this.btn_Proceed.UseVisualStyleBackColor = true;
            this.btn_Proceed.Click += new System.EventHandler(this.btn_Proceed_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmb_storage_type);
            this.groupBox1.Location = new System.Drawing.Point(12, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(266, 49);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Storage Type";
            // 
            // cmb_storage_type
            // 
            this.cmb_storage_type.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmb_storage_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_storage_type.FormattingEnabled = true;
            this.cmb_storage_type.Items.AddRange(new object[] {
            "Stock Pile on Ground Surface",
            "Storage below Ground Surface"});
            this.cmb_storage_type.Location = new System.Drawing.Point(3, 17);
            this.cmb_storage_type.Name = "cmb_storage_type";
            this.cmb_storage_type.Size = new System.Drawing.Size(260, 21);
            this.cmb_storage_type.TabIndex = 0;
            // 
            // frmCatchmentAreaVolume
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 98);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_Proceed);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCatchmentAreaVolume";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Storage Area / Volume";
            this.Load += new System.EventHandler(this.frmCatchmentAreaVolume_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_Proceed;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmb_storage_type;
    }
}