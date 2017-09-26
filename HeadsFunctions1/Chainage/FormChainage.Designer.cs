namespace HeadsFunctions1.Chainage
{
    partial class FormChainage
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Model:String");
            this.treeViewChainage = new System.Windows.Forms.TreeView();
            this.btnCancel_ = new System.Windows.Forms.Button();
            this.btnOk_ = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.tbTextSize_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.tbChainageInterval_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            groupBox1 = new System.Windows.Forms.GroupBox();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
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
            groupBox1.Controls.Add(this.tbChainageInterval_);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(this.treeViewChainage);
            groupBox1.Location = new System.Drawing.Point(8, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(315, 242);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Chainage Parameters";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(189, 116);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(76, 13);
            label2.TabIndex = 2;
            label2.Text = "Text Size (mm)";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(189, 61);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(90, 13);
            label1.TabIndex = 1;
            label1.Text = "Chainage Interval";
            // 
            // treeViewChainage
            // 
            this.treeViewChainage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewChainage.Location = new System.Drawing.Point(10, 17);
            this.treeViewChainage.Name = "treeViewChainage";
            treeNode1.Name = "NodeRoot";
            treeNode1.Text = "Model:String";
            this.treeViewChainage.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeViewChainage.ShowNodeToolTips = true;
            this.treeViewChainage.Size = new System.Drawing.Size(173, 216);
            this.treeViewChainage.TabIndex = 0;
            this.treeViewChainage.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewChainage_AfterSelect);
            // 
            // btnCancel_
            // 
            this.btnCancel_.Location = new System.Drawing.Point(248, 252);
            this.btnCancel_.Name = "btnCancel_";
            this.btnCancel_.Size = new System.Drawing.Size(75, 29);
            this.btnCancel_.TabIndex = 1;
            this.btnCancel_.Text = "Cancel";
            this.btnCancel_.UseVisualStyleBackColor = true;
            this.btnCancel_.Click += new System.EventHandler(this.btnCancel__Click);
            // 
            // btnOk_
            // 
            this.btnOk_.Location = new System.Drawing.Point(158, 251);
            this.btnOk_.Name = "btnOk_";
            this.btnOk_.Size = new System.Drawing.Size(75, 29);
            this.btnOk_.TabIndex = 2;
            this.btnOk_.Text = "OK";
            this.btnOk_.UseVisualStyleBackColor = true;
            this.btnOk_.Click += new System.EventHandler(this.btnOk__Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // tbTextSize_
            // 
            this.tbTextSize_.Location = new System.Drawing.Point(192, 133);
            this.tbTextSize_.MaskFormat = "";
            this.tbTextSize_.MaximumValue = 1.7976931348623157E+308;
            this.tbTextSize_.MinimumValue = 0;
            this.tbTextSize_.Name = "tbTextSize_";
            this.tbTextSize_.Size = new System.Drawing.Size(100, 20);
            this.tbTextSize_.TabIndex = 4;
            this.tbTextSize_.Text = "5";
            this.tbTextSize_.Value = 5;
            this.tbTextSize_.TextChanged += new System.EventHandler(this.tbTextSize__TextChanged);
            // 
            // tbChainageInterval_
            // 
            this.tbChainageInterval_.Location = new System.Drawing.Point(192, 78);
            this.tbChainageInterval_.MaskFormat = "";
            this.tbChainageInterval_.MaximumValue = 1.7976931348623157E+308;
            this.tbChainageInterval_.MinimumValue = 0;
            this.tbChainageInterval_.Name = "tbChainageInterval_";
            this.tbChainageInterval_.Size = new System.Drawing.Size(100, 20);
            this.tbChainageInterval_.TabIndex = 3;
            this.tbChainageInterval_.Text = "100";
            this.tbChainageInterval_.Value = 100;
            this.tbChainageInterval_.TextChanged += new System.EventHandler(this.tbChainageInterval__TextChanged);
            // 
            // FormChainage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 286);
            this.Controls.Add(this.btnOk_);
            this.Controls.Add(this.btnCancel_);
            this.Controls.Add(groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormChainage";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chainage";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewChainage;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble tbTextSize_;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble tbChainageInterval_;
        private System.Windows.Forms.Button btnCancel_;
        private System.Windows.Forms.Button btnOk_;
        private System.Windows.Forms.ErrorProvider errorProvider;

    }
}