namespace HEADSNeed.ASTRA.ASTRAForms
{
    partial class frmTextSize
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
            this.cmbSize = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkJoint = new System.Windows.Forms.CheckBox();
            this.chkMember = new System.Windows.Forms.CheckBox();
            this.chkJointLoad = new System.Windows.Forms.CheckBox();
            this.chkSupportFixed = new System.Windows.Forms.CheckBox();
            this.chkSupportPinned = new System.Windows.Forms.CheckBox();
            this.chkElements = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cmbSize
            // 
            this.cmbSize.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSize.FormattingEnabled = true;
            this.cmbSize.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "56",
            "57",
            "58",
            "59",
            "60"});
            this.cmbSize.Location = new System.Drawing.Point(391, 15);
            this.cmbSize.Name = "cmbSize";
            this.cmbSize.Size = new System.Drawing.Size(49, 21);
            this.cmbSize.TabIndex = 2;
            this.cmbSize.SelectedIndexChanged += new System.EventHandler(this.cmbSize_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(385, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Text Size";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // chkJoint
            // 
            this.chkJoint.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkJoint.AutoSize = true;
            this.chkJoint.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkJoint.Checked = true;
            this.chkJoint.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkJoint.Location = new System.Drawing.Point(52, 6);
            this.chkJoint.Name = "chkJoint";
            this.chkJoint.Size = new System.Drawing.Size(38, 31);
            this.chkJoint.TabIndex = 4;
            this.chkJoint.Text = "Joint";
            this.chkJoint.UseVisualStyleBackColor = true;
            this.chkJoint.CheckedChanged += new System.EventHandler(this.chkMember_CheckedChanged);
            // 
            // chkMember
            // 
            this.chkMember.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkMember.AutoSize = true;
            this.chkMember.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkMember.Checked = true;
            this.chkMember.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMember.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkMember.Location = new System.Drawing.Point(96, 6);
            this.chkMember.Name = "chkMember";
            this.chkMember.Size = new System.Drawing.Size(55, 31);
            this.chkMember.TabIndex = 5;
            this.chkMember.Text = "Member";
            this.chkMember.UseVisualStyleBackColor = true;
            this.chkMember.CheckedChanged += new System.EventHandler(this.chkMember_CheckedChanged);
            // 
            // chkJointLoad
            // 
            this.chkJointLoad.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkJointLoad.AutoSize = true;
            this.chkJointLoad.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkJointLoad.Checked = true;
            this.chkJointLoad.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkJointLoad.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkJointLoad.Location = new System.Drawing.Point(157, 6);
            this.chkJointLoad.Name = "chkJointLoad";
            this.chkJointLoad.Size = new System.Drawing.Size(49, 31);
            this.chkJointLoad.TabIndex = 7;
            this.chkJointLoad.Text = "J Load";
            this.chkJointLoad.UseVisualStyleBackColor = true;
            this.chkJointLoad.CheckedChanged += new System.EventHandler(this.chkSupportPinned_CheckedChanged);
            // 
            // chkSupportFixed
            // 
            this.chkSupportFixed.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkSupportFixed.AutoSize = true;
            this.chkSupportFixed.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkSupportFixed.Checked = true;
            this.chkSupportFixed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSupportFixed.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkSupportFixed.Location = new System.Drawing.Point(335, 6);
            this.chkSupportFixed.Name = "chkSupportFixed";
            this.chkSupportFixed.Size = new System.Drawing.Size(47, 31);
            this.chkSupportFixed.TabIndex = 9;
            this.chkSupportFixed.Text = "FIXED";
            this.chkSupportFixed.UseVisualStyleBackColor = true;
            this.chkSupportFixed.CheckedChanged += new System.EventHandler(this.chkSupportPinned_CheckedChanged);
            // 
            // chkSupportPinned
            // 
            this.chkSupportPinned.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkSupportPinned.AutoSize = true;
            this.chkSupportPinned.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkSupportPinned.Checked = true;
            this.chkSupportPinned.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSupportPinned.Location = new System.Drawing.Point(273, 6);
            this.chkSupportPinned.Name = "chkSupportPinned";
            this.chkSupportPinned.Size = new System.Drawing.Size(58, 31);
            this.chkSupportPinned.TabIndex = 8;
            this.chkSupportPinned.Text = "PINNED";
            this.chkSupportPinned.UseVisualStyleBackColor = true;
            this.chkSupportPinned.CheckedChanged += new System.EventHandler(this.chkSupportPinned_CheckedChanged);
            // 
            // chkElements
            // 
            this.chkElements.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkElements.AutoSize = true;
            this.chkElements.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkElements.Checked = true;
            this.chkElements.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkElements.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkElements.Location = new System.Drawing.Point(211, 5);
            this.chkElements.Name = "chkElements";
            this.chkElements.Size = new System.Drawing.Size(56, 31);
            this.chkElements.TabIndex = 10;
            this.chkElements.Text = "Element";
            this.chkElements.UseVisualStyleBackColor = true;
            this.chkElements.CheckedChanged += new System.EventHandler(this.chkSupportPinned_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBox1.AutoSize = true;
            this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(12, 6);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(34, 31);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "Axis";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // frmTextSize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 40);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.chkElements);
            this.Controls.Add(this.chkSupportFixed);
            this.Controls.Add(this.chkSupportPinned);
            this.Controls.Add(this.chkJointLoad);
            this.Controls.Add(this.chkMember);
            this.Controls.Add(this.chkJoint);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbSize);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.Name = "frmTextSize";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Text Size";
            this.Load += new System.EventHandler(this.frmTextSize_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSize;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox chkJoint;
        public System.Windows.Forms.CheckBox chkMember;
        public System.Windows.Forms.CheckBox chkJointLoad;
        public System.Windows.Forms.CheckBox chkSupportFixed;
        public System.Windows.Forms.CheckBox chkSupportPinned;
        public System.Windows.Forms.CheckBox chkElements;
        public System.Windows.Forms.CheckBox checkBox1;
    }
}