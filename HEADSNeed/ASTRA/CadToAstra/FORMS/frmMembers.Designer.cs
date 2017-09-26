namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    partial class frmMembers
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
            this.btn_add_mem = new System.Windows.Forms.Button();
            this.label30 = new System.Windows.Forms.Label();
            this.txt_mbr_end_jnt = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.txt_mbr_start_no = new System.Windows.Forms.TextBox();
            this.txt_mbr_start_jnt = new System.Windows.Forms.TextBox();
            this.grb_Member = new System.Windows.Forms.GroupBox();
            this.btn_close = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_incr_end_jnt = new System.Windows.Forms.TextBox();
            this.txt_incr_start_no = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_incr_start_jnt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_memtype = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.grb_Member.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_add_mem
            // 
            this.btn_add_mem.Location = new System.Drawing.Point(89, 164);
            this.btn_add_mem.Name = "btn_add_mem";
            this.btn_add_mem.Size = new System.Drawing.Size(139, 34);
            this.btn_add_mem.TabIndex = 2;
            this.btn_add_mem.Text = "ADD MEMBERS";
            this.btn_add_mem.UseVisualStyleBackColor = true;
            this.btn_add_mem.Click += new System.EventHandler(this.btn_mbr_add_Click);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(329, 23);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(77, 13);
            this.label30.TabIndex = 1;
            this.label30.Text = "End Joint No";
            // 
            // txt_mbr_end_jnt
            // 
            this.txt_mbr_end_jnt.Location = new System.Drawing.Point(412, 20);
            this.txt_mbr_end_jnt.Name = "txt_mbr_end_jnt";
            this.txt_mbr_end_jnt.Size = new System.Drawing.Size(46, 21);
            this.txt_mbr_end_jnt.TabIndex = 0;
            this.txt_mbr_end_jnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(5, 23);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(72, 13);
            this.label29.TabIndex = 1;
            this.label29.Text = "Member No";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(164, 23);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(84, 13);
            this.label33.TabIndex = 1;
            this.label33.Text = "Start Joint No";
            // 
            // txt_mbr_start_no
            // 
            this.txt_mbr_start_no.Location = new System.Drawing.Point(87, 20);
            this.txt_mbr_start_no.Name = "txt_mbr_start_no";
            this.txt_mbr_start_no.Size = new System.Drawing.Size(46, 21);
            this.txt_mbr_start_no.TabIndex = 0;
            this.txt_mbr_start_no.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_mbr_start_jnt
            // 
            this.txt_mbr_start_jnt.Location = new System.Drawing.Point(254, 20);
            this.txt_mbr_start_jnt.Name = "txt_mbr_start_jnt";
            this.txt_mbr_start_jnt.Size = new System.Drawing.Size(46, 21);
            this.txt_mbr_start_jnt.TabIndex = 0;
            this.txt_mbr_start_jnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grb_Member
            // 
            this.grb_Member.Controls.Add(this.label30);
            this.grb_Member.Controls.Add(this.txt_mbr_end_jnt);
            this.grb_Member.Controls.Add(this.txt_mbr_start_no);
            this.grb_Member.Controls.Add(this.label29);
            this.grb_Member.Controls.Add(this.txt_mbr_start_jnt);
            this.grb_Member.Controls.Add(this.label33);
            this.grb_Member.Location = new System.Drawing.Point(12, 31);
            this.grb_Member.Name = "grb_Member";
            this.grb_Member.Size = new System.Drawing.Size(469, 51);
            this.grb_Member.TabIndex = 3;
            this.grb_Member.TabStop = false;
            this.grb_Member.Text = "MEMBER";
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(238, 164);
            this.btn_close.Margin = new System.Windows.Forms.Padding(7, 3, 7, 3);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(139, 34);
            this.btn_close.TabIndex = 55;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_incr_end_jnt);
            this.groupBox1.Controls.Add(this.txt_incr_start_no);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_incr_start_jnt);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(469, 51);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "INCREMENT";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(329, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "End Joint No";
            // 
            // txt_incr_end_jnt
            // 
            this.txt_incr_end_jnt.Location = new System.Drawing.Point(412, 20);
            this.txt_incr_end_jnt.Name = "txt_incr_end_jnt";
            this.txt_incr_end_jnt.Size = new System.Drawing.Size(46, 21);
            this.txt_incr_end_jnt.TabIndex = 0;
            this.txt_incr_end_jnt.Text = "0";
            this.txt_incr_end_jnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_incr_start_no
            // 
            this.txt_incr_start_no.Location = new System.Drawing.Point(87, 20);
            this.txt_incr_start_no.Name = "txt_incr_start_no";
            this.txt_incr_start_no.Size = new System.Drawing.Size(46, 21);
            this.txt_incr_start_no.TabIndex = 0;
            this.txt_incr_start_no.Text = "0";
            this.txt_incr_start_no.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Member No";
            // 
            // txt_incr_start_jnt
            // 
            this.txt_incr_start_jnt.Location = new System.Drawing.Point(254, 20);
            this.txt_incr_start_jnt.Name = "txt_incr_start_jnt";
            this.txt_incr_start_jnt.Size = new System.Drawing.Size(46, 21);
            this.txt_incr_start_jnt.TabIndex = 0;
            this.txt_incr_start_jnt.Text = "0";
            this.txt_incr_start_jnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(164, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Start Joint No";
            // 
            // cmb_memtype
            // 
            this.cmb_memtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_memtype.FormattingEnabled = true;
            this.cmb_memtype.Items.AddRange(new object[] {
            "BEAM",
            "TRUSS",
            "CABLE"});
            this.cmb_memtype.Location = new System.Drawing.Point(107, 6);
            this.cmb_memtype.Name = "cmb_memtype";
            this.cmb_memtype.Size = new System.Drawing.Size(121, 21);
            this.cmb_memtype.TabIndex = 56;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Member Type";
            // 
            // frmMembers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 210);
            this.Controls.Add(this.cmb_memtype);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.grb_Member);
            this.Controls.Add(this.btn_add_mem);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMembers";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Members";
            this.Load += new System.EventHandler(this.frmMembers_Load);
            this.grb_Member.ResumeLayout(false);
            this.grb_Member.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_add_mem;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox txt_mbr_end_jnt;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox txt_mbr_start_no;
        private System.Windows.Forms.TextBox txt_mbr_start_jnt;
        private System.Windows.Forms.GroupBox grb_Member;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_incr_end_jnt;
        private System.Windows.Forms.TextBox txt_incr_start_no;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_incr_start_jnt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_memtype;
        private System.Windows.Forms.Label label4;
    }
}