namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    partial class frmProjectTitle
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbLengthUnit = new System.Windows.Forms.ComboBox();
            this.label77 = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.cmbMassUnit = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAst = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbStructureType = new System.Windows.Forms.ListBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtUserTitle = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbLengthUnit);
            this.groupBox2.Controls.Add(this.label77);
            this.groupBox2.Controls.Add(this.label78);
            this.groupBox2.Controls.Add(this.cmbMassUnit);
            this.groupBox2.Location = new System.Drawing.Point(176, 59);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(222, 68);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Define Unit";
            // 
            // cmbLengthUnit
            // 
            this.cmbLengthUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLengthUnit.FormattingEnabled = true;
            this.cmbLengthUnit.Items.AddRange(new object[] {
            "METRES",
            "CM",
            "MM",
            "YDS",
            "FT",
            "INCH"});
            this.cmbLengthUnit.Location = new System.Drawing.Point(111, 40);
            this.cmbLengthUnit.Name = "cmbLengthUnit";
            this.cmbLengthUnit.Size = new System.Drawing.Size(103, 21);
            this.cmbLengthUnit.TabIndex = 135;
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Location = new System.Drawing.Point(28, 43);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(71, 13);
            this.label77.TabIndex = 137;
            this.label77.Text = "Length Unit";
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Location = new System.Drawing.Point(28, 17);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(61, 13);
            this.label78.TabIndex = 136;
            this.label78.Text = "Mass Unit";
            // 
            // cmbMassUnit
            // 
            this.cmbMassUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMassUnit.FormattingEnabled = true;
            this.cmbMassUnit.Items.AddRange(new object[] {
            "KG",
            "KN",
            "MTON",
            "NEW",
            "GMS",
            "LBS",
            "KIP"});
            this.cmbMassUnit.Location = new System.Drawing.Point(111, 14);
            this.cmbMassUnit.Name = "cmbMassUnit";
            this.cmbMassUnit.Size = new System.Drawing.Size(103, 21);
            this.cmbMassUnit.TabIndex = 134;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "User\'s Title";
            // 
            // txtAst
            // 
            this.txtAst.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAst.Location = new System.Drawing.Point(92, 12);
            this.txtAst.Name = "txtAst";
            this.txtAst.ReadOnly = true;
            this.txtAst.Size = new System.Drawing.Size(101, 21);
            this.txtAst.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "StructureType";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbStructureType
            // 
            this.lbStructureType.FormattingEnabled = true;
            this.lbStructureType.Items.AddRange(new object[] {
            "SPACE",
            "FLOOR",
            "PLANE"});
            this.lbStructureType.Location = new System.Drawing.Point(96, 59);
            this.lbStructureType.Name = "lbStructureType";
            this.lbStructureType.Size = new System.Drawing.Size(73, 43);
            this.lbStructureType.TabIndex = 14;
            this.lbStructureType.SelectedIndexChanged += new System.EventHandler(this.lbStructureType_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(215, 134);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 24);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(82, 134);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(87, 24);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtUserTitle
            // 
            this.txtUserTitle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUserTitle.Location = new System.Drawing.Point(92, 34);
            this.txtUserTitle.Name = "txtUserTitle";
            this.txtUserTitle.Size = new System.Drawing.Size(305, 21);
            this.txtUserTitle.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Project Title";
            // 
            // frmProjectTitle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 161);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAst);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbStructureType);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtUserTitle);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProjectTitle";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Settings";
            this.Load += new System.EventHandler(this.frmProjectTitle_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbLengthUnit;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.ComboBox cmbMassUnit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAst;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbStructureType;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtUserTitle;
        private System.Windows.Forms.Label label1;
    }
}