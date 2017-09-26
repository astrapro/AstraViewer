namespace HeadsFunctions1.Details
{
    partial class FormDetails
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Model:String");
            this.tbTextSize_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.treeViewDetails = new System.Windows.Forms.TreeView();
            this.btnCancel_ = new System.Windows.Forms.Button();
            this.btnOk_ = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            groupBox1 = new System.Windows.Forms.GroupBox();
            label2 = new System.Windows.Forms.Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            groupBox1.Controls.Add(this.tbTextSize_);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(this.treeViewDetails);
            groupBox1.Location = new System.Drawing.Point(8, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(222, 283);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Details Parameters";
            // 
            // tbTextSize_
            // 
            this.tbTextSize_.Location = new System.Drawing.Point(114, 262);
            this.tbTextSize_.MaskFormat = "";
            this.tbTextSize_.MaximumValue = 1.7976931348623157E+308;
            this.tbTextSize_.MinimumValue = 0;
            this.tbTextSize_.Name = "tbTextSize_";
            this.tbTextSize_.Size = new System.Drawing.Size(91, 20);
            this.tbTextSize_.TabIndex = 4;
            this.tbTextSize_.Text = "5";
            this.tbTextSize_.Value = 5;
            this.tbTextSize_.TextChanged += new System.EventHandler(this.tbTextSize__TextChanged);
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(8, 266);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(76, 13);
            label2.TabIndex = 2;
            label2.Text = "Text Size (mm)";
            // 
            // treeViewDetails
            // 
            this.treeViewDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewDetails.Location = new System.Drawing.Point(15, 17);
            this.treeViewDetails.Name = "treeViewDetails";
            treeNode1.Name = "NodeRoot";
            treeNode1.Text = "Model:String";
            this.treeViewDetails.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeViewDetails.ShowNodeToolTips = true;
            this.treeViewDetails.Size = new System.Drawing.Size(192, 239);
            this.treeViewDetails.TabIndex = 0;
            this.treeViewDetails.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewDetails_AfterSelect);
            // 
            // btnCancel_
            // 
            this.btnCancel_.Location = new System.Drawing.Point(169, 298);
            this.btnCancel_.Name = "btnCancel_";
            this.btnCancel_.Size = new System.Drawing.Size(63, 26);
            this.btnCancel_.TabIndex = 2;
            this.btnCancel_.Text = "Cancel";
            this.btnCancel_.UseVisualStyleBackColor = true;
            this.btnCancel_.Click += new System.EventHandler(this.btnCancel__Click);
            // 
            // btnOk_
            // 
            this.btnOk_.Location = new System.Drawing.Point(84, 298);
            this.btnOk_.Name = "btnOk_";
            this.btnOk_.Size = new System.Drawing.Size(63, 26);
            this.btnOk_.TabIndex = 3;
            this.btnOk_.Text = "OK";
            this.btnOk_.UseVisualStyleBackColor = true;
            this.btnOk_.Click += new System.EventHandler(this.btnOk__Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // FormDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(239, 329);
            this.Controls.Add(this.btnOk_);
            this.Controls.Add(this.btnCancel_);
            this.Controls.Add(groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDetails";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Details";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private HeadsFunctions1.CustomCtrls.TextBoxDouble tbTextSize_;
        private System.Windows.Forms.TreeView treeViewDetails;
        private System.Windows.Forms.Button btnCancel_;
        private System.Windows.Forms.Button btnOk_;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}