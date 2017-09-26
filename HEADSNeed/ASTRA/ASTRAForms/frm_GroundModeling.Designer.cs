namespace HEADSNeed.ASTRA.ASTRAForms
{
    partial class frm_GroundModeling
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
            this.grbGroundModeling = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbModelAndStringName = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDeSelect = new System.Windows.Forms.TextBox();
            this.txtSelect = new System.Windows.Forms.TextBox();
            this.btnDeSelect = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDeSelectAll = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grbGroundModeling.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbGroundModeling
            // 
            this.grbGroundModeling.Controls.Add(this.groupBox1);
            this.grbGroundModeling.Controls.Add(this.groupBox3);
            this.grbGroundModeling.Controls.Add(this.groupBox2);
            this.grbGroundModeling.Controls.Add(this.btnOK);
            this.grbGroundModeling.Controls.Add(this.btnCancel);
            this.grbGroundModeling.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbGroundModeling.Location = new System.Drawing.Point(0, 0);
            this.grbGroundModeling.Name = "grbGroundModeling";
            this.grbGroundModeling.Size = new System.Drawing.Size(442, 486);
            this.grbGroundModeling.TabIndex = 2;
            this.grbGroundModeling.TabStop = false;
            this.grbGroundModeling.Text = "Ground Modeling ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbModelAndStringName);
            this.groupBox1.Location = new System.Drawing.Point(16, 164);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(417, 280);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Model Name and String Names";
            // 
            // lbModelAndStringName
            // 
            this.lbModelAndStringName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbModelAndStringName.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbModelAndStringName.FormattingEnabled = true;
            this.lbModelAndStringName.ItemHeight = 16;
            this.lbModelAndStringName.Location = new System.Drawing.Point(3, 16);
            this.lbModelAndStringName.Name = "lbModelAndStringName";
            this.lbModelAndStringName.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbModelAndStringName.Size = new System.Drawing.Size(411, 260);
            this.lbModelAndStringName.Sorted = true;
            this.lbModelAndStringName.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtDeSelect);
            this.groupBox3.Controls.Add(this.txtSelect);
            this.groupBox3.Controls.Add(this.btnDeSelect);
            this.groupBox3.Controls.Add(this.btnSelect);
            this.groupBox3.Location = new System.Drawing.Point(174, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(259, 140);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 32);
            this.label1.TabIndex = 3;
            this.label1.Text = "Wild Character may be used, \r\nExample \"K?\" or \'\'KE?\"";
            // 
            // txtDeSelect
            // 
            this.txtDeSelect.Location = new System.Drawing.Point(126, 59);
            this.txtDeSelect.Name = "txtDeSelect";
            this.txtDeSelect.Size = new System.Drawing.Size(127, 20);
            this.txtDeSelect.TabIndex = 2;
            this.txtDeSelect.Text = "K?";
            // 
            // txtSelect
            // 
            this.txtSelect.AccessibleDescription = "";
            this.txtSelect.AllowDrop = true;
            this.txtSelect.Location = new System.Drawing.Point(126, 21);
            this.txtSelect.Name = "txtSelect";
            this.txtSelect.Size = new System.Drawing.Size(127, 20);
            this.txtSelect.TabIndex = 2;
            this.txtSelect.Text = "K?";
            // 
            // btnDeSelect
            // 
            this.btnDeSelect.Location = new System.Drawing.Point(16, 59);
            this.btnDeSelect.Name = "btnDeSelect";
            this.btnDeSelect.Size = new System.Drawing.Size(96, 23);
            this.btnDeSelect.TabIndex = 1;
            this.btnDeSelect.Text = "DeSelect";
            this.btnDeSelect.UseVisualStyleBackColor = true;
            this.btnDeSelect.Click += new System.EventHandler(this.btnDeSelect_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(16, 19);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(96, 23);
            this.btnSelect.TabIndex = 0;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDeSelectAll);
            this.groupBox2.Controls.Add(this.btnSelectAll);
            this.groupBox2.Location = new System.Drawing.Point(12, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(114, 94);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // btnDeSelectAll
            // 
            this.btnDeSelectAll.Location = new System.Drawing.Point(12, 60);
            this.btnDeSelectAll.Name = "btnDeSelectAll";
            this.btnDeSelectAll.Size = new System.Drawing.Size(96, 23);
            this.btnDeSelectAll.TabIndex = 1;
            this.btnDeSelectAll.Text = "DeSelect All";
            this.btnDeSelectAll.UseVisualStyleBackColor = true;
            this.btnDeSelectAll.Click += new System.EventHandler(this.btnDeSelectAll_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(12, 19);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(96, 23);
            this.btnSelectAll.TabIndex = 0;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(115, 453);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(96, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(228, 453);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frm_GroundModeling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 486);
            this.Controls.Add(this.grbGroundModeling);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "frm_GroundModeling";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ground Modeling";
            this.Load += new System.EventHandler(this.frmGroundModeling_Load);
            this.Click += new System.EventHandler(this.frmGroundModeling_Load);
            this.grbGroundModeling.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbGroundModeling;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lbModelAndStringName;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDeSelect;
        private System.Windows.Forms.TextBox txtSelect;
        private System.Windows.Forms.Button btnDeSelect;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDeSelectAll;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}