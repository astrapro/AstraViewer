namespace HeadsFunctions1.Valignment
{
    partial class FormOpenValignment
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
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Valign Fil Data");
            this.treeViewModels_ = new System.Windows.Forms.TreeView();
            this.btnCancel_ = new System.Windows.Forms.Button();
            this.btnOk_ = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            groupBox1.Controls.Add(this.treeViewModels_);
            groupBox1.Location = new System.Drawing.Point(9, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(251, 314);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Select Models and Strings from Existing Valign";
            // 
            // treeViewModels_
            // 
            this.treeViewModels_.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewModels_.Location = new System.Drawing.Point(12, 21);
            this.treeViewModels_.Name = "treeViewModels_";
            treeNode2.Name = "NodeRoot";
            treeNode2.Tag = "";
            treeNode2.Text = "Valign Fil Data";
            this.treeViewModels_.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.treeViewModels_.Size = new System.Drawing.Size(229, 283);
            this.treeViewModels_.TabIndex = 0;
            this.treeViewModels_.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewModels__AfterSelect);
            // 
            // btnCancel_
            // 
            this.btnCancel_.Location = new System.Drawing.Point(266, 132);
            this.btnCancel_.Name = "btnCancel_";
            this.btnCancel_.Size = new System.Drawing.Size(63, 29);
            this.btnCancel_.TabIndex = 4;
            this.btnCancel_.Text = "Cancel";
            this.btnCancel_.UseVisualStyleBackColor = true;
            this.btnCancel_.Click += new System.EventHandler(this.btnCancel__Click);
            // 
            // btnOk_
            // 
            this.btnOk_.Enabled = false;
            this.btnOk_.Location = new System.Drawing.Point(266, 91);
            this.btnOk_.Name = "btnOk_";
            this.btnOk_.Size = new System.Drawing.Size(63, 29);
            this.btnOk_.TabIndex = 3;
            this.btnOk_.Text = "OK";
            this.btnOk_.UseVisualStyleBackColor = true;
            this.btnOk_.Click += new System.EventHandler(this.btnOk__Click);
            // 
            // FormOpenValignment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 327);
            this.Controls.Add(this.btnCancel_);
            this.Controls.Add(this.btnOk_);
            this.Controls.Add(groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormOpenValignment";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Open Valignment";
            groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewModels_;
        private System.Windows.Forms.Button btnCancel_;
        private System.Windows.Forms.Button btnOk_;
    }
}