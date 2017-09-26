namespace HeadsFunctions1.Halignment.Modelling
{
    partial class FormModellingHalign
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Available Design(s)");
            System.Windows.Forms.Label label1;
            this.treeViewDesigns_ = new System.Windows.Forms.TreeView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDelete_ = new System.Windows.Forms.Button();
            this.btnInsert_ = new System.Windows.Forms.Button();
            this.dataGridSpChainage_ = new System.Windows.Forms.DataGridView();
            this.btnOk_ = new System.Windows.Forms.Button();
            this.btnCancel_ = new System.Windows.Forms.Button();
            this.tbInterval_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            groupBox1 = new System.Windows.Forms.GroupBox();
            label1 = new System.Windows.Forms.Label();
            groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSpChainage_)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.treeViewDesigns_);
            groupBox1.Location = new System.Drawing.Point(9, 10);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(201, 260);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Available Design(s)";
            // 
            // treeViewDesigns_
            // 
            this.treeViewDesigns_.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewDesigns_.Location = new System.Drawing.Point(8, 16);
            this.treeViewDesigns_.Name = "treeViewDesigns_";
            treeNode1.Name = "NodeRoot";
            treeNode1.Text = "Available Design(s)";
            this.treeViewDesigns_.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeViewDesigns_.Size = new System.Drawing.Size(184, 237);
            this.treeViewDesigns_.TabIndex = 2;
            this.treeViewDesigns_.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewDesigns__NodeMouseDoubleClick);
            this.treeViewDesigns_.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewDesigns__AfterSelect);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(13, 279);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(42, 13);
            label1.TabIndex = 1;
            label1.Text = "Interval";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnDelete_);
            this.groupBox3.Controls.Add(this.btnInsert_);
            this.groupBox3.Controls.Add(this.dataGridSpChainage_);
            this.groupBox3.Location = new System.Drawing.Point(229, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(192, 285);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Special Chainage";
            // 
            // btnDelete_
            // 
            this.btnDelete_.Location = new System.Drawing.Point(128, 255);
            this.btnDelete_.Name = "btnDelete_";
            this.btnDelete_.Size = new System.Drawing.Size(60, 23);
            this.btnDelete_.TabIndex = 2;
            this.btnDelete_.Text = "Delete";
            this.btnDelete_.UseVisualStyleBackColor = true;
            this.btnDelete_.Click += new System.EventHandler(this.btnDelete__Click);
            // 
            // btnInsert_
            // 
            this.btnInsert_.Location = new System.Drawing.Point(63, 255);
            this.btnInsert_.Name = "btnInsert_";
            this.btnInsert_.Size = new System.Drawing.Size(59, 23);
            this.btnInsert_.TabIndex = 1;
            this.btnInsert_.Text = "Insert";
            this.btnInsert_.UseVisualStyleBackColor = true;
            this.btnInsert_.Click += new System.EventHandler(this.btnInsert__Click);
            // 
            // dataGridSpChainage_
            // 
            this.dataGridSpChainage_.AllowUserToAddRows = false;
            this.dataGridSpChainage_.AllowUserToDeleteRows = false;
            this.dataGridSpChainage_.AllowUserToOrderColumns = true;
            this.dataGridSpChainage_.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridSpChainage_.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridSpChainage_.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridSpChainage_.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dataGridSpChainage_.Location = new System.Drawing.Point(3, 16);
            this.dataGridSpChainage_.MultiSelect = false;
            this.dataGridSpChainage_.Name = "dataGridSpChainage_";
            this.dataGridSpChainage_.RowHeadersWidth = 15;
            this.dataGridSpChainage_.Size = new System.Drawing.Size(186, 233);
            this.dataGridSpChainage_.TabIndex = 0;
            this.dataGridSpChainage_.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridSpChainage__CellValidated);
            this.dataGridSpChainage_.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridSpChainage__DataError);
            this.dataGridSpChainage_.SelectionChanged += new System.EventHandler(this.dataGridSpChainage__SelectionChanged);
            // 
            // btnOk_
            // 
            this.btnOk_.Location = new System.Drawing.Point(271, 303);
            this.btnOk_.Name = "btnOk_";
            this.btnOk_.Size = new System.Drawing.Size(66, 29);
            this.btnOk_.TabIndex = 5;
            this.btnOk_.Text = "OK";
            this.btnOk_.UseVisualStyleBackColor = true;
            this.btnOk_.Click += new System.EventHandler(this.btnOk__Click);
            // 
            // btnCancel_
            // 
            this.btnCancel_.Location = new System.Drawing.Point(352, 303);
            this.btnCancel_.Name = "btnCancel_";
            this.btnCancel_.Size = new System.Drawing.Size(66, 29);
            this.btnCancel_.TabIndex = 6;
            this.btnCancel_.Text = "Cancel";
            this.btnCancel_.UseVisualStyleBackColor = true;
            this.btnCancel_.Click += new System.EventHandler(this.btnCancel__Click);
            // 
            // tbInterval_
            // 
            this.tbInterval_.Enabled = false;
            this.tbInterval_.Location = new System.Drawing.Point(85, 276);
            this.tbInterval_.MaskFormat = "";
            this.tbInterval_.MaximumValue = 1000;
            this.tbInterval_.MinimumValue = 0;
            this.tbInterval_.Name = "tbInterval_";
            this.tbInterval_.Size = new System.Drawing.Size(116, 20);
            this.tbInterval_.TabIndex = 0;
            this.tbInterval_.Text = "0";
            this.tbInterval_.Value = 0;
            // 
            // FormModellingHalign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 340);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.tbInterval_);
            this.Controls.Add(label1);
            this.Controls.Add(this.btnCancel_);
            this.Controls.Add(this.btnOk_);
            this.Controls.Add(groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormModellingHalign";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Modelling: Halign";
            groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSpChainage_)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewDesigns_;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridSpChainage_;
        private System.Windows.Forms.Button btnDelete_;
        private System.Windows.Forms.Button btnInsert_;
        private System.Windows.Forms.Button btnOk_;
        private System.Windows.Forms.Button btnCancel_;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble tbInterval_;
    }
}