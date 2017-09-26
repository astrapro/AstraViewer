namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    partial class frmMaterialProperties
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
            this.cmbLengthUnit = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_den_val = new System.Windows.Forms.TextBox();
            this.cmb_mat_prop = new System.Windows.Forms.ComboBox();
            this.txt_alpha = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_poisson_val = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_e_mod = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddData = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_member_nos = new System.Windows.Forms.TextBox();
            this.label77 = new System.Windows.Forms.Label();
            this.grb_unit = new System.Windows.Forms.GroupBox();
            this.label78 = new System.Windows.Forms.Label();
            this.cmbMassUnit = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmb_range = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.grb_unit.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbLengthUnit
            // 
            this.cmbLengthUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLengthUnit.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLengthUnit.FormattingEnabled = true;
            this.cmbLengthUnit.Items.AddRange(new object[] {
            "METRES",
            "CM",
            "MM",
            "YDS",
            "FT",
            "INCH"});
            this.cmbLengthUnit.Location = new System.Drawing.Point(307, 15);
            this.cmbLengthUnit.Name = "cmbLengthUnit";
            this.cmbLengthUnit.Size = new System.Drawing.Size(103, 22);
            this.cmbLengthUnit.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "Density";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txt_den_val);
            this.groupBox2.Controls.Add(this.cmb_mat_prop);
            this.groupBox2.Controls.Add(this.txt_alpha);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txt_poisson_val);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txt_e_mod);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(15, 48);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(418, 165);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(215, 14);
            this.label3.TabIndex = 7;
            this.label3.Text = "Material Properties / Constants";
            // 
            // txt_den_val
            // 
            this.txt_den_val.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_den_val.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_den_val.Location = new System.Drawing.Point(264, 71);
            this.txt_den_val.Name = "txt_den_val";
            this.txt_den_val.Size = new System.Drawing.Size(146, 22);
            this.txt_den_val.TabIndex = 2;
            this.txt_den_val.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmb_mat_prop
            // 
            this.cmb_mat_prop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_mat_prop.FormattingEnabled = true;
            this.cmb_mat_prop.Items.AddRange(new object[] {
            "CONCRETE",
            "STEEL",
            "USER\'S VALUE"});
            this.cmb_mat_prop.Location = new System.Drawing.Point(264, 14);
            this.cmb_mat_prop.Name = "cmb_mat_prop";
            this.cmb_mat_prop.Size = new System.Drawing.Size(146, 21);
            this.cmb_mat_prop.TabIndex = 0;
            this.cmb_mat_prop.SelectedIndexChanged += new System.EventHandler(this.cmb_mat_prop_SelectedIndexChanged);
            // 
            // txt_alpha
            // 
            this.txt_alpha.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_alpha.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_alpha.Location = new System.Drawing.Point(264, 127);
            this.txt_alpha.Name = "txt_alpha";
            this.txt_alpha.Size = new System.Drawing.Size(146, 22);
            this.txt_alpha.TabIndex = 3;
            this.txt_alpha.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 14);
            this.label5.TabIndex = 6;
            this.label5.Text = "ALPHA";
            // 
            // txt_poisson_val
            // 
            this.txt_poisson_val.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_poisson_val.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_poisson_val.Location = new System.Drawing.Point(264, 99);
            this.txt_poisson_val.Name = "txt_poisson_val";
            this.txt_poisson_val.Size = new System.Drawing.Size(146, 22);
            this.txt_poisson_val.TabIndex = 3;
            this.txt_poisson_val.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 14);
            this.label4.TabIndex = 6;
            this.label4.Text = "Poisson\'s Ratio";
            // 
            // txt_e_mod
            // 
            this.txt_e_mod.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_e_mod.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_e_mod.Location = new System.Drawing.Point(264, 41);
            this.txt_e_mod.Name = "txt_e_mod";
            this.txt_e_mod.Size = new System.Drawing.Size(146, 22);
            this.txt_e_mod.TabIndex = 1;
            this.txt_e_mod.Text = "2.85E6";
            this.txt_e_mod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "Elastic Modulus";
            // 
            // btnAddData
            // 
            this.btnAddData.Location = new System.Drawing.Point(174, 238);
            this.btnAddData.Name = "btnAddData";
            this.btnAddData.Size = new System.Drawing.Size(119, 32);
            this.btnAddData.TabIndex = 1;
            this.btnAddData.Text = "Add Data";
            this.btnAddData.UseVisualStyleBackColor = true;
            this.btnAddData.Click += new System.EventHandler(this.btnAddData_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(128, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(123, 14);
            this.label8.TabIndex = 6;
            this.label8.Text = "Member Numbers";
            // 
            // txt_member_nos
            // 
            this.txt_member_nos.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_member_nos.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_member_nos.Location = new System.Drawing.Point(132, 20);
            this.txt_member_nos.Name = "txt_member_nos";
            this.txt_member_nos.Size = new System.Drawing.Size(293, 22);
            this.txt_member_nos.TabIndex = 1;
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label77.Location = new System.Drawing.Point(205, 19);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(81, 13);
            this.label77.TabIndex = 137;
            this.label77.Text = "Length Unit";
            // 
            // grb_unit
            // 
            this.grb_unit.Controls.Add(this.cmbLengthUnit);
            this.grb_unit.Controls.Add(this.label77);
            this.grb_unit.Controls.Add(this.label78);
            this.grb_unit.Controls.Add(this.cmbMassUnit);
            this.grb_unit.Location = new System.Drawing.Point(23, 282);
            this.grb_unit.Name = "grb_unit";
            this.grb_unit.Size = new System.Drawing.Size(418, 43);
            this.grb_unit.TabIndex = 3;
            this.grb_unit.TabStop = false;
            this.grb_unit.Text = "UNIT";
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label78.Location = new System.Drawing.Point(3, 17);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(69, 13);
            this.label78.TabIndex = 136;
            this.label78.Text = "Mass Unit";
            // 
            // cmbMassUnit
            // 
            this.cmbMassUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMassUnit.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMassUnit.FormattingEnabled = true;
            this.cmbMassUnit.Items.AddRange(new object[] {
            "KG",
            "KN",
            "MTON",
            "NEW",
            "GMS",
            "LBS",
            "KIP"});
            this.cmbMassUnit.Location = new System.Drawing.Point(89, 14);
            this.cmbMassUnit.Name = "cmbMassUnit";
            this.cmbMassUnit.Size = new System.Drawing.Size(105, 22);
            this.cmbMassUnit.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.cmb_range);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.txt_member_nos);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Location = new System.Drawing.Point(14, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(443, 220);
            this.panel1.TabIndex = 0;
            // 
            // cmb_range
            // 
            this.cmb_range.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_range.FormattingEnabled = true;
            this.cmb_range.Items.AddRange(new object[] {
            "ALL",
            "NUMBERS"});
            this.cmb_range.Location = new System.Drawing.Point(15, 20);
            this.cmb_range.Name = "cmb_range";
            this.cmb_range.Size = new System.Drawing.Size(109, 21);
            this.cmb_range.TabIndex = 0;
            this.cmb_range.SelectedIndexChanged += new System.EventHandler(this.cmb_range_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(190, 331);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "FINISH";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmMaterialProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 270);
            this.Controls.Add(this.btnAddData);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grb_unit);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMaterialProperties";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Material Properties / Constants";
            this.Load += new System.EventHandler(this.frmMaterialProperties_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grb_unit.ResumeLayout(false);
            this.grb_unit.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbLengthUnit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_poisson_val;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_e_mod;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddData;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_member_nos;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.GroupBox grb_unit;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.ComboBox cmbMassUnit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txt_den_val;
        private System.Windows.Forms.ComboBox cmb_mat_prop;
        private System.Windows.Forms.ComboBox cmb_range;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_alpha;
        private System.Windows.Forms.Label label5;
    }
}