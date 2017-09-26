namespace HeadsFunctions1.WorkingFolder
{
    partial class FormWorkingFolderSelector
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
            System.Windows.Forms.Button btnFolderSelector_;
            System.Windows.Forms.GroupBox btnCancel_;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWorkingFolderSelector));
            this.txtWkDirDetail_ = new System.Windows.Forms.TextBox();
            this.txtPath_ = new System.Windows.Forms.TextBox();
            this.btnOk_ = new System.Windows.Forms.Button();
            this.btnCancel__ = new System.Windows.Forms.Button();
            btnFolderSelector_ = new System.Windows.Forms.Button();
            btnCancel_ = new System.Windows.Forms.GroupBox();
            btnCancel_.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnFolderSelector_
            // 
            btnFolderSelector_.Location = new System.Drawing.Point(387, 18);
            btnFolderSelector_.Name = "btnFolderSelector_";
            btnFolderSelector_.Size = new System.Drawing.Size(32, 23);
            btnFolderSelector_.TabIndex = 2;
            btnFolderSelector_.Text = "...";
            btnFolderSelector_.UseVisualStyleBackColor = true;
            btnFolderSelector_.Click += new System.EventHandler(this.btnFolderSelector__Click);
            // 
            // btnCancel_
            // 
            btnCancel_.Controls.Add(this.txtWkDirDetail_);
            btnCancel_.Controls.Add(btnFolderSelector_);
            btnCancel_.Controls.Add(this.txtPath_);
            btnCancel_.Location = new System.Drawing.Point(7, 3);
            btnCancel_.Name = "btnCancel_";
            btnCancel_.Size = new System.Drawing.Size(424, 87);
            btnCancel_.TabIndex = 3;
            btnCancel_.TabStop = false;
            btnCancel_.Text = "Working Folder";
            // 
            // txtWkDirDetail_
            // 
            this.txtWkDirDetail_.Location = new System.Drawing.Point(6, 48);
            this.txtWkDirDetail_.Multiline = true;
            this.txtWkDirDetail_.Name = "txtWkDirDetail_";
            this.txtWkDirDetail_.ReadOnly = true;
            this.txtWkDirDetail_.Size = new System.Drawing.Size(413, 32);
            this.txtWkDirDetail_.TabIndex = 3;
            // 
            // txtPath_
            // 
            this.txtPath_.BackColor = System.Drawing.SystemColors.Window;
            this.txtPath_.Location = new System.Drawing.Point(6, 19);
            this.txtPath_.Name = "txtPath_";
            this.txtPath_.Size = new System.Drawing.Size(381, 20);
            this.txtPath_.TabIndex = 0;
            this.txtPath_.TextChanged += new System.EventHandler(this.txtPath__TextChanged);
            // 
            // btnOk_
            // 
            this.btnOk_.Location = new System.Drawing.Point(275, 99);
            this.btnOk_.Name = "btnOk_";
            this.btnOk_.Size = new System.Drawing.Size(75, 29);
            this.btnOk_.TabIndex = 4;
            this.btnOk_.Text = "OK";
            this.btnOk_.UseVisualStyleBackColor = true;
            this.btnOk_.Click += new System.EventHandler(this.btnOk__Click);
            // 
            // btnCancel__
            // 
            this.btnCancel__.Location = new System.Drawing.Point(356, 99);
            this.btnCancel__.Name = "btnCancel__";
            this.btnCancel__.Size = new System.Drawing.Size(75, 29);
            this.btnCancel__.TabIndex = 5;
            this.btnCancel__.Text = "Cancel";
            this.btnCancel__.UseVisualStyleBackColor = true;
            this.btnCancel__.Click += new System.EventHandler(this.btnCancel___Click);
            // 
            // FormWorkingFolderSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 132);
            this.Controls.Add(this.btnCancel__);
            this.Controls.Add(this.btnOk_);
            this.Controls.Add(btnCancel_);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormWorkingFolderSelector";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Working Folder";
            btnCancel_.ResumeLayout(false);
            btnCancel_.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtPath_;
        private System.Windows.Forms.TextBox txtWkDirDetail_;
        private System.Windows.Forms.Button btnOk_;
        private System.Windows.Forms.Button btnCancel__;
    }
}