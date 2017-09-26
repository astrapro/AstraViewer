namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    partial class frmMemberRelease
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_node = new System.Windows.Forms.ComboBox();
            this.txt_mem_nos = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chk_Fx = new System.Windows.Forms.CheckBox();
            this.chk_Fy = new System.Windows.Forms.CheckBox();
            this.chk_Fz = new System.Windows.Forms.CheckBox();
            this.chk_Mx = new System.Windows.Forms.CheckBox();
            this.chk_My = new System.Windows.Forms.CheckBox();
            this.chk_Mz = new System.Windows.Forms.CheckBox();
            this.btn_add_data = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Release for Member Numbers";
            // 
            // cmb_node
            // 
            this.cmb_node.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_node.FormattingEnabled = true;
            this.cmb_node.Items.AddRange(new object[] {
            "START",
            "END"});
            this.cmb_node.Location = new System.Drawing.Point(204, 40);
            this.cmb_node.Name = "cmb_node";
            this.cmb_node.Size = new System.Drawing.Size(98, 21);
            this.cmb_node.TabIndex = 2;
            // 
            // txt_mem_nos
            // 
            this.txt_mem_nos.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_mem_nos.Location = new System.Drawing.Point(204, 14);
            this.txt_mem_nos.Name = "txt_mem_nos";
            this.txt_mem_nos.Size = new System.Drawing.Size(358, 21);
            this.txt_mem_nos.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(53, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "At Node START or END";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(120, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "Release for";
            // 
            // chk_Fx
            // 
            this.chk_Fx.AutoSize = true;
            this.chk_Fx.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_Fx.Location = new System.Drawing.Point(204, 67);
            this.chk_Fx.Name = "chk_Fx";
            this.chk_Fx.Size = new System.Drawing.Size(39, 17);
            this.chk_Fx.TabIndex = 6;
            this.chk_Fx.Text = "Fx";
            this.chk_Fx.UseVisualStyleBackColor = true;
            // 
            // chk_Fy
            // 
            this.chk_Fy.AutoSize = true;
            this.chk_Fy.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_Fy.Location = new System.Drawing.Point(246, 67);
            this.chk_Fy.Name = "chk_Fy";
            this.chk_Fy.Size = new System.Drawing.Size(39, 17);
            this.chk_Fy.TabIndex = 7;
            this.chk_Fy.Text = "Fy";
            this.chk_Fy.UseVisualStyleBackColor = true;
            // 
            // chk_Fz
            // 
            this.chk_Fz.AutoSize = true;
            this.chk_Fz.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_Fz.Location = new System.Drawing.Point(290, 67);
            this.chk_Fz.Name = "chk_Fz";
            this.chk_Fz.Size = new System.Drawing.Size(38, 17);
            this.chk_Fz.TabIndex = 8;
            this.chk_Fz.Text = "Fz";
            this.chk_Fz.UseVisualStyleBackColor = true;
            // 
            // chk_Mx
            // 
            this.chk_Mx.AutoSize = true;
            this.chk_Mx.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_Mx.Location = new System.Drawing.Point(332, 67);
            this.chk_Mx.Name = "chk_Mx";
            this.chk_Mx.Size = new System.Drawing.Size(42, 17);
            this.chk_Mx.TabIndex = 9;
            this.chk_Mx.Text = "Mx";
            this.chk_Mx.UseVisualStyleBackColor = true;
            // 
            // chk_My
            // 
            this.chk_My.AutoSize = true;
            this.chk_My.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_My.Location = new System.Drawing.Point(377, 67);
            this.chk_My.Name = "chk_My";
            this.chk_My.Size = new System.Drawing.Size(42, 17);
            this.chk_My.TabIndex = 10;
            this.chk_My.Text = "My";
            this.chk_My.UseVisualStyleBackColor = true;
            // 
            // chk_Mz
            // 
            this.chk_Mz.AutoSize = true;
            this.chk_Mz.Checked = true;
            this.chk_Mz.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_Mz.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_Mz.Location = new System.Drawing.Point(423, 67);
            this.chk_Mz.Name = "chk_Mz";
            this.chk_Mz.Size = new System.Drawing.Size(41, 17);
            this.chk_Mz.TabIndex = 11;
            this.chk_Mz.Text = "Mz";
            this.chk_Mz.UseVisualStyleBackColor = true;
            // 
            // btn_add_data
            // 
            this.btn_add_data.Location = new System.Drawing.Point(149, 101);
            this.btn_add_data.Name = "btn_add_data";
            this.btn_add_data.Size = new System.Drawing.Size(124, 23);
            this.btn_add_data.TabIndex = 12;
            this.btn_add_data.Text = "Add Data";
            this.btn_add_data.UseVisualStyleBackColor = true;
            this.btn_add_data.Click += new System.EventHandler(this.btn_add_data_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(313, 101);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(124, 23);
            this.btn_cancel.TabIndex = 13;
            this.btn_cancel.Text = "FINISH";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chk_Mz);
            this.groupBox1.Controls.Add(this.chk_My);
            this.groupBox1.Controls.Add(this.chk_Mx);
            this.groupBox1.Controls.Add(this.chk_Fz);
            this.groupBox1.Controls.Add(this.chk_Fy);
            this.groupBox1.Controls.Add(this.chk_Fx);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_mem_nos);
            this.groupBox1.Controls.Add(this.cmb_node);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(4, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(571, 93);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            // 
            // frmMemberRelease
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 131);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_add_data);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMemberRelease";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Member Release";
            this.Load += new System.EventHandler(this.frmMemberRelease_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_node;
        private System.Windows.Forms.TextBox txt_mem_nos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chk_Fx;
        private System.Windows.Forms.CheckBox chk_Fy;
        private System.Windows.Forms.CheckBox chk_Fz;
        private System.Windows.Forms.CheckBox chk_Mx;
        private System.Windows.Forms.CheckBox chk_My;
        private System.Windows.Forms.CheckBox chk_Mz;
        private System.Windows.Forms.Button btn_add_data;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}