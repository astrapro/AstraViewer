namespace HeadsFunctions1.Valignment.Modelling
{
    partial class FormModellingValign
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
            this.treeViewModels_ = new System.Windows.Forms.TreeView();
            this.btnCancel_ = new System.Windows.Forms.Button();
            this.btnOK_ = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textChainInt_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.SuspendLayout();
            // 
            // treeViewModels_
            // 
            this.treeViewModels_.Location = new System.Drawing.Point(12, 12);
            this.treeViewModels_.Name = "treeViewModels_";
            this.treeViewModels_.Size = new System.Drawing.Size(246, 246);
            this.treeViewModels_.TabIndex = 0;
            this.treeViewModels_.DoubleClick += new System.EventHandler(this.btnOK__Click);
            this.treeViewModels_.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewModels__AfterSelect);
            // 
            // btnCancel_
            // 
            this.btnCancel_.Location = new System.Drawing.Point(184, 295);
            this.btnCancel_.Name = "btnCancel_";
            this.btnCancel_.Size = new System.Drawing.Size(74, 28);
            this.btnCancel_.TabIndex = 1;
            this.btnCancel_.Text = "Cancel";
            this.btnCancel_.UseVisualStyleBackColor = true;
            this.btnCancel_.Click += new System.EventHandler(this.btnCancel__Click);
            // 
            // btnOK_
            // 
            this.btnOK_.Location = new System.Drawing.Point(12, 299);
            this.btnOK_.Name = "btnOK_";
            this.btnOK_.Size = new System.Drawing.Size(74, 28);
            this.btnOK_.TabIndex = 2;
            this.btnOK_.Text = "OK";
            this.btnOK_.UseVisualStyleBackColor = true;
            this.btnOK_.Click += new System.EventHandler(this.btnOK__Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 272);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Chainage Interval (m)";
            // 
            // textChainInt_
            // 
            this.textChainInt_.Location = new System.Drawing.Point(125, 269);
            this.textChainInt_.MaskFormat = "";
            this.textChainInt_.MaximumValue = 1.7976931348623157E+308;
            this.textChainInt_.MinimumValue = -1.7976931348623157E+308;
            this.textChainInt_.Name = "textChainInt_";
            this.textChainInt_.Size = new System.Drawing.Size(133, 20);
            this.textChainInt_.TabIndex = 10;
            this.textChainInt_.Text = "5";
            this.textChainInt_.Value = 5;
            // 
            // FormModellingValign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 334);
            this.Controls.Add(this.textChainInt_);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOK_);
            this.Controls.Add(this.btnCancel_);
            this.Controls.Add(this.treeViewModels_);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormModellingValign";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Modelling : Valign";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewModels_;
        private System.Windows.Forms.Button btnCancel_;
        private System.Windows.Forms.Button btnOK_;
        private System.Windows.Forms.Label label1;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble textChainInt_;
    }
}