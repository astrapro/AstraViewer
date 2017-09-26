namespace HEADSNeed.ASTRA.ASTRAForms
{
    partial class frmPostProcess
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
            this.cmbLoadCase = new System.Windows.Forms.ComboBox();
            this.cmbMemberNo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbForce = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(271, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Member";
            // 
            // cmbLoadCase
            // 
            this.cmbLoadCase.FormattingEnabled = true;
            this.cmbLoadCase.Location = new System.Drawing.Point(81, 5);
            this.cmbLoadCase.Name = "cmbLoadCase";
            this.cmbLoadCase.Size = new System.Drawing.Size(63, 21);
            this.cmbLoadCase.TabIndex = 1;
            this.cmbLoadCase.SelectedIndexChanged += new System.EventHandler(this.cmbLoadCase_SelectedIndexChanged);
            // 
            // cmbMemberNo
            // 
            this.cmbMemberNo.FormattingEnabled = true;
            this.cmbMemberNo.Location = new System.Drawing.Point(331, 5);
            this.cmbMemberNo.Name = "cmbMemberNo";
            this.cmbMemberNo.Size = new System.Drawing.Size(63, 21);
            this.cmbMemberNo.TabIndex = 0;
            this.cmbMemberNo.SelectedIndexChanged += new System.EventHandler(this.cmbMemberNo_SelectedIndexChanged);
            this.cmbMemberNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbMemberNo_KeyDown);
            this.cmbMemberNo.Click += new System.EventHandler(this.cmbMemberNo_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Load Case";
            // 
            // cmbForce
            // 
            this.cmbForce.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbForce.FormattingEnabled = true;
            this.cmbForce.Items.AddRange(new object[] {
            "SHEAR",
            "BENDING"});
            this.cmbForce.Location = new System.Drawing.Point(185, 95);
            this.cmbForce.Name = "cmbForce";
            this.cmbForce.Size = new System.Drawing.Size(86, 21);
            this.cmbForce.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(134, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "FORCE";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmPostProcess
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Application;
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 31);
            this.Controls.Add(this.cmbForce);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbMemberNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbLoadCase);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPostProcess";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Post Process";
            this.Load += new System.EventHandler(this.frmPostProcess_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPostProcess_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbLoadCase;
        private System.Windows.Forms.ComboBox cmbMemberNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbForce;
        private System.Windows.Forms.Label label3;
    }
}