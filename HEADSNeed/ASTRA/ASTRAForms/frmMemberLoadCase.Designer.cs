namespace HEADSNeed.ASTRA.ASTRAForms
{
    partial class frmMemberLoadCase
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
            CloseAllWindow();
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
            this.cmbLoadCase = new System.Windows.Forms.ComboBox();
            this.btnLoadCaseForword = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLoadCaseBack = new System.Windows.Forms.Button();
            this.cmbMembers = new System.Windows.Forms.ComboBox();
            this.btnMemberForword = new System.Windows.Forms.Button();
            this.btnMemberBack = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkLocal = new System.Windows.Forms.CheckBox();
            this.chkGlobal = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tv_loads = new System.Windows.Forms.TreeView();
            this.chkJoint = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(158, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Member No ";
            // 
            // cmbLoadCase
            // 
            this.cmbLoadCase.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLoadCase.FormattingEnabled = true;
            this.cmbLoadCase.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.cmbLoadCase.Location = new System.Drawing.Point(48, 81);
            this.cmbLoadCase.Name = "cmbLoadCase";
            this.cmbLoadCase.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmbLoadCase.Size = new System.Drawing.Size(45, 24);
            this.cmbLoadCase.TabIndex = 3;
            this.cmbLoadCase.Text = "1";
            this.cmbLoadCase.SelectedIndexChanged += new System.EventHandler(this.cmbLoadCase_SelectedIndexChanged);
            this.cmbLoadCase.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbMembers_KeyUp);
            this.cmbLoadCase.Click += new System.EventHandler(this.cmbLoadCase_Click);
            // 
            // btnLoadCaseForword
            // 
            this.btnLoadCaseForword.BackgroundImage = global::HEADSNeed.Properties.Resources.forword;
            this.btnLoadCaseForword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLoadCaseForword.Location = new System.Drawing.Point(99, 81);
            this.btnLoadCaseForword.Name = "btnLoadCaseForword";
            this.btnLoadCaseForword.Size = new System.Drawing.Size(25, 22);
            this.btnLoadCaseForword.TabIndex = 2;
            this.btnLoadCaseForword.UseVisualStyleBackColor = true;
            this.btnLoadCaseForword.Click += new System.EventHandler(this.btnLoadCaseForword_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(29, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Load Case No ";
            // 
            // btnLoadCaseBack
            // 
            this.btnLoadCaseBack.BackgroundImage = global::HEADSNeed.Properties.Resources.back;
            this.btnLoadCaseBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLoadCaseBack.Location = new System.Drawing.Point(15, 80);
            this.btnLoadCaseBack.Name = "btnLoadCaseBack";
            this.btnLoadCaseBack.Size = new System.Drawing.Size(26, 23);
            this.btnLoadCaseBack.TabIndex = 0;
            this.btnLoadCaseBack.UseVisualStyleBackColor = true;
            this.btnLoadCaseBack.Click += new System.EventHandler(this.btnLoadCaseBack_Click);
            // 
            // cmbMembers
            // 
            this.cmbMembers.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMembers.FormattingEnabled = true;
            this.cmbMembers.Location = new System.Drawing.Point(161, 79);
            this.cmbMembers.Name = "cmbMembers";
            this.cmbMembers.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmbMembers.Size = new System.Drawing.Size(48, 24);
            this.cmbMembers.TabIndex = 6;
            this.cmbMembers.Text = "1";
            this.cmbMembers.SelectedIndexChanged += new System.EventHandler(this.cmbMembers_SelectedIndexChanged);
            this.cmbMembers.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbMembers_KeyUp);
            // 
            // btnMemberForword
            // 
            this.btnMemberForword.BackgroundImage = global::HEADSNeed.Properties.Resources.forword;
            this.btnMemberForword.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMemberForword.Location = new System.Drawing.Point(215, 81);
            this.btnMemberForword.Name = "btnMemberForword";
            this.btnMemberForword.Size = new System.Drawing.Size(25, 22);
            this.btnMemberForword.TabIndex = 8;
            this.btnMemberForword.UseVisualStyleBackColor = true;
            this.btnMemberForword.Click += new System.EventHandler(this.btnMemberForword_Click);
            // 
            // btnMemberBack
            // 
            this.btnMemberBack.BackgroundImage = global::HEADSNeed.Properties.Resources.back;
            this.btnMemberBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMemberBack.Location = new System.Drawing.Point(131, 80);
            this.btnMemberBack.Name = "btnMemberBack";
            this.btnMemberBack.Size = new System.Drawing.Size(26, 23);
            this.btnMemberBack.TabIndex = 7;
            this.btnMemberBack.UseVisualStyleBackColor = true;
            this.btnMemberBack.Click += new System.EventHandler(this.btnMemberBack_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkLocal);
            this.groupBox1.Controls.Add(this.chkGlobal);
            this.groupBox1.Location = new System.Drawing.Point(5, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(124, 41);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Member Load On/Off";
            // 
            // chkLocal
            // 
            this.chkLocal.AutoSize = true;
            this.chkLocal.Checked = true;
            this.chkLocal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLocal.Location = new System.Drawing.Point(60, 19);
            this.chkLocal.Name = "chkLocal";
            this.chkLocal.Size = new System.Drawing.Size(52, 17);
            this.chkLocal.TabIndex = 1;
            this.chkLocal.Text = "Local";
            this.chkLocal.UseVisualStyleBackColor = true;
            this.chkLocal.CheckedChanged += new System.EventHandler(this.chkLocal_CheckedChanged);
            // 
            // chkGlobal
            // 
            this.chkGlobal.AutoSize = true;
            this.chkGlobal.Checked = true;
            this.chkGlobal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGlobal.Location = new System.Drawing.Point(6, 19);
            this.chkGlobal.Name = "chkGlobal";
            this.chkGlobal.Size = new System.Drawing.Size(56, 17);
            this.chkGlobal.TabIndex = 0;
            this.chkGlobal.Text = "Global";
            this.chkGlobal.UseVisualStyleBackColor = true;
            this.chkGlobal.CheckedChanged += new System.EventHandler(this.chkGlobal_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.tv_loads);
            this.groupBox2.Location = new System.Drawing.Point(2, 113);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(293, 363);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "LOAD CASES DETAILS";
            // 
            // tv_loads
            // 
            this.tv_loads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv_loads.Location = new System.Drawing.Point(3, 16);
            this.tv_loads.Name = "tv_loads";
            this.tv_loads.Size = new System.Drawing.Size(287, 344);
            this.tv_loads.TabIndex = 0;
            this.tv_loads.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // chkJoint
            // 
            this.chkJoint.AutoSize = true;
            this.chkJoint.Checked = true;
            this.chkJoint.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkJoint.Location = new System.Drawing.Point(6, 16);
            this.chkJoint.Name = "chkJoint";
            this.chkJoint.Size = new System.Drawing.Size(75, 17);
            this.chkJoint.TabIndex = 0;
            this.chkJoint.Text = "Joint Load";
            this.chkJoint.UseVisualStyleBackColor = true;
            this.chkJoint.CheckedChanged += new System.EventHandler(this.chkGlobal_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkJoint);
            this.groupBox3.Location = new System.Drawing.Point(135, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(108, 41);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Joint Load On/Off";
            // 
            // frmMemberLoadCase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 476);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnMemberForword);
            this.Controls.Add(this.btnMemberBack);
            this.Controls.Add(this.cmbMembers);
            this.Controls.Add(this.cmbLoadCase);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLoadCaseForword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnLoadCaseBack);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMemberLoadCase";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Member And Load Case";
            this.Load += new System.EventHandler(this.frmMemberLoadCase_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMemberLoadCase_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbLoadCase;
        private System.Windows.Forms.Button btnLoadCaseForword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLoadCaseBack;
        private System.Windows.Forms.ComboBox cmbMembers;
        private System.Windows.Forms.Button btnMemberForword;
        private System.Windows.Forms.Button btnMemberBack;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkLocal;
        private System.Windows.Forms.CheckBox chkGlobal;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TreeView tv_loads;
        private System.Windows.Forms.CheckBox chkJoint;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}