namespace ExtraMethods
{
    partial class frmPurge
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
            this.tree = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.butAll = new System.Windows.Forms.Button();
            this.checkHatchPatterns = new System.Windows.Forms.CheckBox();
            this.checkImages = new System.Windows.Forms.CheckBox();
            this.checkTextStyles = new System.Windows.Forms.CheckBox();
            this.checkLineTypes = new System.Windows.Forms.CheckBox();
            this.checkDimstyles = new System.Windows.Forms.CheckBox();
            this.checkBlocks = new System.Windows.Forms.CheckBox();
            this.checkLayers = new System.Windows.Forms.CheckBox();
            this.butCancel = new System.Windows.Forms.Button();
            this.butPurge = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tree
            // 
            this.tree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tree.Location = new System.Drawing.Point(5, 24);
            this.tree.Name = "tree";
            this.tree.Size = new System.Drawing.Size(406, 400);
            this.tree.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Items Not Used in Document";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.butAll);
            this.panel1.Controls.Add(this.checkHatchPatterns);
            this.panel1.Controls.Add(this.checkImages);
            this.panel1.Controls.Add(this.checkTextStyles);
            this.panel1.Controls.Add(this.checkLineTypes);
            this.panel1.Controls.Add(this.checkDimstyles);
            this.panel1.Controls.Add(this.checkBlocks);
            this.panel1.Controls.Add(this.checkLayers);
            this.panel1.Location = new System.Drawing.Point(419, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(131, 399);
            this.panel1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(30, 333);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(78, 28);
            this.button1.TabIndex = 8;
            this.button1.Text = "DeSelect All";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // butAll
            // 
            this.butAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.butAll.Location = new System.Drawing.Point(30, 367);
            this.butAll.Name = "butAll";
            this.butAll.Size = new System.Drawing.Size(78, 28);
            this.butAll.TabIndex = 7;
            this.butAll.Text = "Select All";
            this.butAll.UseVisualStyleBackColor = true;
            this.butAll.Click += new System.EventHandler(this.butAll_Click);
            // 
            // checkHatchPatterns
            // 
            this.checkHatchPatterns.AutoSize = true;
            this.checkHatchPatterns.Checked = true;
            this.checkHatchPatterns.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkHatchPatterns.Location = new System.Drawing.Point(14, 152);
            this.checkHatchPatterns.Name = "checkHatchPatterns";
            this.checkHatchPatterns.Size = new System.Drawing.Size(94, 17);
            this.checkHatchPatterns.TabIndex = 6;
            this.checkHatchPatterns.Text = "HatchPatterns";
            this.checkHatchPatterns.UseVisualStyleBackColor = true;
            // 
            // checkImages
            // 
            this.checkImages.AutoSize = true;
            this.checkImages.Checked = true;
            this.checkImages.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkImages.Location = new System.Drawing.Point(14, 129);
            this.checkImages.Name = "checkImages";
            this.checkImages.Size = new System.Drawing.Size(60, 17);
            this.checkImages.TabIndex = 5;
            this.checkImages.Text = "Images";
            this.checkImages.UseVisualStyleBackColor = true;
            // 
            // checkTextStyles
            // 
            this.checkTextStyles.AutoSize = true;
            this.checkTextStyles.Checked = true;
            this.checkTextStyles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkTextStyles.Location = new System.Drawing.Point(14, 106);
            this.checkTextStyles.Name = "checkTextStyles";
            this.checkTextStyles.Size = new System.Drawing.Size(75, 17);
            this.checkTextStyles.TabIndex = 4;
            this.checkTextStyles.Text = "TextStyles";
            this.checkTextStyles.UseVisualStyleBackColor = true;
            // 
            // checkLineTypes
            // 
            this.checkLineTypes.AutoSize = true;
            this.checkLineTypes.Checked = true;
            this.checkLineTypes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkLineTypes.Location = new System.Drawing.Point(14, 83);
            this.checkLineTypes.Name = "checkLineTypes";
            this.checkLineTypes.Size = new System.Drawing.Size(75, 17);
            this.checkLineTypes.TabIndex = 3;
            this.checkLineTypes.Text = "LineTypes";
            this.checkLineTypes.UseVisualStyleBackColor = true;
            // 
            // checkDimstyles
            // 
            this.checkDimstyles.AutoSize = true;
            this.checkDimstyles.Checked = true;
            this.checkDimstyles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkDimstyles.Location = new System.Drawing.Point(14, 60);
            this.checkDimstyles.Name = "checkDimstyles";
            this.checkDimstyles.Size = new System.Drawing.Size(72, 17);
            this.checkDimstyles.TabIndex = 2;
            this.checkDimstyles.Text = "DimStyles";
            this.checkDimstyles.UseVisualStyleBackColor = true;
            // 
            // checkBlocks
            // 
            this.checkBlocks.AutoSize = true;
            this.checkBlocks.Checked = true;
            this.checkBlocks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBlocks.Location = new System.Drawing.Point(13, 37);
            this.checkBlocks.Name = "checkBlocks";
            this.checkBlocks.Size = new System.Drawing.Size(58, 17);
            this.checkBlocks.TabIndex = 1;
            this.checkBlocks.Text = "Blocks";
            this.checkBlocks.UseVisualStyleBackColor = true;
            // 
            // checkLayers
            // 
            this.checkLayers.AutoSize = true;
            this.checkLayers.Checked = true;
            this.checkLayers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkLayers.Location = new System.Drawing.Point(13, 14);
            this.checkLayers.Name = "checkLayers";
            this.checkLayers.Size = new System.Drawing.Size(57, 17);
            this.checkLayers.TabIndex = 0;
            this.checkLayers.Text = "Layers";
            this.checkLayers.UseVisualStyleBackColor = true;
            // 
            // butCancel
            // 
            this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.Location = new System.Drawing.Point(472, 434);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(78, 28);
            this.butCancel.TabIndex = 8;
            this.butCancel.Text = "Close";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butPurge
            // 
            this.butPurge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butPurge.Location = new System.Drawing.Point(365, 434);
            this.butPurge.Name = "butPurge";
            this.butPurge.Size = new System.Drawing.Size(78, 28);
            this.butPurge.TabIndex = 9;
            this.butPurge.Text = "Purge";
            this.butPurge.UseVisualStyleBackColor = true;
            this.butPurge.Click += new System.EventHandler(this.butPurge_Click);
            // 
            // frmPurge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.butCancel;
            this.ClientSize = new System.Drawing.Size(558, 470);
            this.Controls.Add(this.butPurge);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tree);
            this.MinimumSize = new System.Drawing.Size(300, 354);
            this.Name = "frmPurge";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Purge";
            this.Load += new System.EventHandler(this.frmPurge_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tree;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkDimstyles;
        private System.Windows.Forms.CheckBox checkBlocks;
        private System.Windows.Forms.CheckBox checkLayers;
        private System.Windows.Forms.CheckBox checkLineTypes;
        private System.Windows.Forms.Button butAll;
        private System.Windows.Forms.CheckBox checkHatchPatterns;
        private System.Windows.Forms.CheckBox checkImages;
        private System.Windows.Forms.CheckBox checkTextStyles;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button butPurge;
    }
}