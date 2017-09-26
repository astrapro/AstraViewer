namespace ExtraMethods
{
    partial class BlockLibrary
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
            this.combMainBlocks = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.butSelect = new System.Windows.Forms.Button();
            this.textLibraryfilename = new System.Windows.Forms.TextBox();
            this.vectorDrawBaseControl1 = new VectorDraw.Professional.Control.VectorDrawBaseControl();
            this.groupPreview = new System.Windows.Forms.GroupBox();
            this.GroupLibraryBlocks = new System.Windows.Forms.GroupBox();
            this.butCreateBlock = new System.Windows.Forms.Button();
            this.butEdit = new System.Windows.Forms.Button();
            this.butRemoveBlock = new System.Windows.Forms.Button();
            this.butAddMain = new System.Windows.Forms.Button();
            this.listBlocks = new System.Windows.Forms.ListBox();
            this.butExit = new System.Windows.Forms.Button();
            this.groupLibrary = new System.Windows.Forms.GroupBox();
            this.butCreate = new System.Windows.Forms.Button();
            this.butSave = new System.Windows.Forms.Button();
            this.groupPreview.SuspendLayout();
            this.GroupLibraryBlocks.SuspendLayout();
            this.groupLibrary.SuspendLayout();
            this.SuspendLayout();
            // 
            // combMainBlocks
            // 
            this.combMainBlocks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.combMainBlocks.FormattingEnabled = true;
            this.combMainBlocks.Location = new System.Drawing.Point(170, 12);
            this.combMainBlocks.Name = "combMainBlocks";
            this.combMainBlocks.Size = new System.Drawing.Size(614, 21);
            this.combMainBlocks.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Main Document Blocks:";
            // 
            // butSelect
            // 
            this.butSelect.Location = new System.Drawing.Point(8, 18);
            this.butSelect.Name = "butSelect";
            this.butSelect.Size = new System.Drawing.Size(61, 27);
            this.butSelect.TabIndex = 2;
            this.butSelect.Text = "Select";
            this.butSelect.UseVisualStyleBackColor = true;
            this.butSelect.Click += new System.EventHandler(this.butSelect_Click);
            // 
            // textLibraryfilename
            // 
            this.textLibraryfilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textLibraryfilename.Enabled = false;
            this.textLibraryfilename.Location = new System.Drawing.Point(244, 58);
            this.textLibraryfilename.Name = "textLibraryfilename";
            this.textLibraryfilename.Size = new System.Drawing.Size(540, 20);
            this.textLibraryfilename.TabIndex = 3;
            // 
            // vectorDrawBaseControl1
            // 
            this.vectorDrawBaseControl1.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.vectorDrawBaseControl1.AllowDrop = true;
            this.vectorDrawBaseControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.vectorDrawBaseControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.vectorDrawBaseControl1.DisableVdrawDxf = false;
            this.vectorDrawBaseControl1.EnableAutoGripOn = true;
            this.vectorDrawBaseControl1.Location = new System.Drawing.Point(150, 10);
            this.vectorDrawBaseControl1.Name = "vectorDrawBaseControl1";
            this.vectorDrawBaseControl1.Size = new System.Drawing.Size(517, 309);
            this.vectorDrawBaseControl1.TabIndex = 4;
            // 
            // groupPreview
            // 
            this.groupPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPreview.Controls.Add(this.GroupLibraryBlocks);
            this.groupPreview.Controls.Add(this.butAddMain);
            this.groupPreview.Controls.Add(this.listBlocks);
            this.groupPreview.Controls.Add(this.vectorDrawBaseControl1);
            this.groupPreview.Location = new System.Drawing.Point(12, 92);
            this.groupPreview.Name = "groupPreview";
            this.groupPreview.Size = new System.Drawing.Size(772, 328);
            this.groupPreview.TabIndex = 5;
            this.groupPreview.TabStop = false;
            this.groupPreview.Text = "Preview";
            // 
            // GroupLibraryBlocks
            // 
            this.GroupLibraryBlocks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupLibraryBlocks.Controls.Add(this.butCreateBlock);
            this.GroupLibraryBlocks.Controls.Add(this.butEdit);
            this.GroupLibraryBlocks.Controls.Add(this.butRemoveBlock);
            this.GroupLibraryBlocks.Location = new System.Drawing.Point(673, 69);
            this.GroupLibraryBlocks.Name = "GroupLibraryBlocks";
            this.GroupLibraryBlocks.Size = new System.Drawing.Size(92, 122);
            this.GroupLibraryBlocks.TabIndex = 11;
            this.GroupLibraryBlocks.TabStop = false;
            this.GroupLibraryBlocks.Text = "Library Blocks";
            // 
            // butCreateBlock
            // 
            this.butCreateBlock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butCreateBlock.Location = new System.Drawing.Point(16, 19);
            this.butCreateBlock.Name = "butCreateBlock";
            this.butCreateBlock.Size = new System.Drawing.Size(61, 27);
            this.butCreateBlock.TabIndex = 6;
            this.butCreateBlock.Text = "Create";
            this.butCreateBlock.UseVisualStyleBackColor = true;
            this.butCreateBlock.Click += new System.EventHandler(this.butCreateBlock_Click);
            // 
            // butEdit
            // 
            this.butEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butEdit.Location = new System.Drawing.Point(16, 52);
            this.butEdit.Name = "butEdit";
            this.butEdit.Size = new System.Drawing.Size(61, 27);
            this.butEdit.TabIndex = 10;
            this.butEdit.Text = "Edit";
            this.butEdit.UseVisualStyleBackColor = true;
            this.butEdit.Click += new System.EventHandler(this.butEdit_Click);
            // 
            // butRemoveBlock
            // 
            this.butRemoveBlock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butRemoveBlock.Location = new System.Drawing.Point(16, 85);
            this.butRemoveBlock.Name = "butRemoveBlock";
            this.butRemoveBlock.Size = new System.Drawing.Size(61, 27);
            this.butRemoveBlock.TabIndex = 8;
            this.butRemoveBlock.Text = "Remove";
            this.butRemoveBlock.UseVisualStyleBackColor = true;
            this.butRemoveBlock.Click += new System.EventHandler(this.butRemoveBlock_Click);
            // 
            // butAddMain
            // 
            this.butAddMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butAddMain.Location = new System.Drawing.Point(674, 10);
            this.butAddMain.Name = "butAddMain";
            this.butAddMain.Size = new System.Drawing.Size(92, 48);
            this.butAddMain.TabIndex = 7;
            this.butAddMain.Text = "Add Selected Block To Main Document";
            this.butAddMain.UseVisualStyleBackColor = true;
            this.butAddMain.Click += new System.EventHandler(this.butAddMain_Click);
            // 
            // listBlocks
            // 
            this.listBlocks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listBlocks.FormattingEnabled = true;
            this.listBlocks.Location = new System.Drawing.Point(11, 21);
            this.listBlocks.Name = "listBlocks";
            this.listBlocks.Size = new System.Drawing.Size(130, 251);
            this.listBlocks.TabIndex = 5;
            this.listBlocks.SelectedIndexChanged += new System.EventHandler(this.listBlocks_SelectedIndexChanged);
            // 
            // butExit
            // 
            this.butExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butExit.Location = new System.Drawing.Point(701, 426);
            this.butExit.Name = "butExit";
            this.butExit.Size = new System.Drawing.Size(61, 23);
            this.butExit.TabIndex = 12;
            this.butExit.Text = "Exit";
            this.butExit.UseVisualStyleBackColor = true;
            this.butExit.Click += new System.EventHandler(this.butExit_Click);
            // 
            // groupLibrary
            // 
            this.groupLibrary.Controls.Add(this.butSelect);
            this.groupLibrary.Controls.Add(this.butCreate);
            this.groupLibrary.Controls.Add(this.butSave);
            this.groupLibrary.Location = new System.Drawing.Point(28, 36);
            this.groupLibrary.Name = "groupLibrary";
            this.groupLibrary.Size = new System.Drawing.Size(208, 50);
            this.groupLibrary.TabIndex = 12;
            this.groupLibrary.TabStop = false;
            this.groupLibrary.Text = "Library";
            // 
            // butCreate
            // 
            this.butCreate.Location = new System.Drawing.Point(75, 18);
            this.butCreate.Name = "butCreate";
            this.butCreate.Size = new System.Drawing.Size(61, 27);
            this.butCreate.TabIndex = 13;
            this.butCreate.Text = "Create";
            this.butCreate.UseVisualStyleBackColor = true;
            this.butCreate.Click += new System.EventHandler(this.butCreate_Click);
            // 
            // butSave
            // 
            this.butSave.Location = new System.Drawing.Point(142, 17);
            this.butSave.Name = "butSave";
            this.butSave.Size = new System.Drawing.Size(61, 27);
            this.butSave.TabIndex = 9;
            this.butSave.Text = "Save";
            this.butSave.UseVisualStyleBackColor = true;
            this.butSave.Click += new System.EventHandler(this.butSave_Click);
            // 
            // BlockLibrary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 451);
            this.Controls.Add(this.butExit);
            this.Controls.Add(this.groupLibrary);
            this.Controls.Add(this.textLibraryfilename);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.combMainBlocks);
            this.Controls.Add(this.groupPreview);
            this.MinimumSize = new System.Drawing.Size(400, 360);
            this.Name = "BlockLibrary";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "BlockLibrary";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BlockLibrary_FormClosed);
            this.Load += new System.EventHandler(this.BlockLibrary_Load);
            this.groupPreview.ResumeLayout(false);
            this.GroupLibraryBlocks.ResumeLayout(false);
            this.groupLibrary.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox combMainBlocks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button butSelect;
        private System.Windows.Forms.TextBox textLibraryfilename;
        private VectorDraw.Professional.Control.VectorDrawBaseControl vectorDrawBaseControl1;
        private System.Windows.Forms.GroupBox groupPreview;
        private System.Windows.Forms.ListBox listBlocks;
        private System.Windows.Forms.Button butCreateBlock;
        private System.Windows.Forms.Button butAddMain;
        private System.Windows.Forms.Button butRemoveBlock;
        private System.Windows.Forms.Button butSave;
        private System.Windows.Forms.Button butEdit;
        private System.Windows.Forms.GroupBox GroupLibraryBlocks;
        private System.Windows.Forms.GroupBox groupLibrary;
        private System.Windows.Forms.Button butCreate;
        private System.Windows.Forms.Button butExit;
    }
}