namespace HeadsFunctions1.Valignment
{
    partial class FormDesignValign
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
            this.labelModelName = new System.Windows.Forms.Label();
            this.textBoxModelName = new System.Windows.Forms.TextBox();
            this.textBoxStartChainageM = new System.Windows.Forms.TextBox();
            this.textBoxStringLabel = new System.Windows.Forms.TextBox();
            this.textBoxEndChainageM = new System.Windows.Forms.TextBox();
            this.textBoxDVCL = new System.Windows.Forms.TextBox();
            this.textBoxChainageIntervalM = new System.Windows.Forms.TextBox();
            this.labelStartChainageM = new System.Windows.Forms.Label();
            this.labelStringLabel = new System.Windows.Forms.Label();
            this.labelEndChainageM = new System.Windows.Forms.Label();
            this.labelChainageIntervalM = new System.Windows.Forms.Label();
            this.checkBoxDVCL = new System.Windows.Forms.CheckBox();
            this.groupBoxVIPParameters = new System.Windows.Forms.GroupBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonInsert = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.buttonCanel = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonSaveFinish = new System.Windows.Forms.Button();
            this.groupBoxVIPParameters.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelModelName
            // 
            this.labelModelName.AutoSize = true;
            this.labelModelName.Location = new System.Drawing.Point(19, 18);
            this.labelModelName.Name = "labelModelName";
            this.labelModelName.Size = new System.Drawing.Size(70, 13);
            this.labelModelName.TabIndex = 0;
            this.labelModelName.Text = "Model Name:";
            // 
            // textBoxModelName
            // 
            this.textBoxModelName.Location = new System.Drawing.Point(123, 15);
            this.textBoxModelName.Name = "textBoxModelName";
            this.textBoxModelName.Size = new System.Drawing.Size(84, 20);
            this.textBoxModelName.TabIndex = 0;
            this.textBoxModelName.Text = "DESIGN";
            // 
            // textBoxStartChainageM
            // 
            this.textBoxStartChainageM.Location = new System.Drawing.Point(123, 41);
            this.textBoxStartChainageM.Name = "textBoxStartChainageM";
            this.textBoxStartChainageM.Size = new System.Drawing.Size(84, 20);
            this.textBoxStartChainageM.TabIndex = 2;
            this.textBoxStartChainageM.Text = "0.0";
            this.textBoxStartChainageM.TextChanged += new System.EventHandler(this.textBoxStartChainageM_TextChanged);
            // 
            // textBoxStringLabel
            // 
            this.textBoxStringLabel.Location = new System.Drawing.Point(310, 15);
            this.textBoxStringLabel.Name = "textBoxStringLabel";
            this.textBoxStringLabel.Size = new System.Drawing.Size(84, 20);
            this.textBoxStringLabel.TabIndex = 1;
            this.textBoxStringLabel.Text = "M001";
            // 
            // textBoxEndChainageM
            // 
            this.textBoxEndChainageM.Location = new System.Drawing.Point(310, 41);
            this.textBoxEndChainageM.Name = "textBoxEndChainageM";
            this.textBoxEndChainageM.Size = new System.Drawing.Size(84, 20);
            this.textBoxEndChainageM.TabIndex = 3;
            this.textBoxEndChainageM.Text = "0.0";
            this.textBoxEndChainageM.TextChanged += new System.EventHandler(this.textBoxEndChainageM_TextChanged);
            // 
            // textBoxDVCL
            // 
            this.textBoxDVCL.Location = new System.Drawing.Point(310, 67);
            this.textBoxDVCL.Name = "textBoxDVCL";
            this.textBoxDVCL.Size = new System.Drawing.Size(84, 20);
            this.textBoxDVCL.TabIndex = 6;
            this.textBoxDVCL.Text = "150.0";
            // 
            // textBoxChainageIntervalM
            // 
            this.textBoxChainageIntervalM.Location = new System.Drawing.Point(515, 41);
            this.textBoxChainageIntervalM.Name = "textBoxChainageIntervalM";
            this.textBoxChainageIntervalM.Size = new System.Drawing.Size(84, 20);
            this.textBoxChainageIntervalM.TabIndex = 4;
            this.textBoxChainageIntervalM.Text = "5.0";
            // 
            // labelStartChainageM
            // 
            this.labelStartChainageM.AutoSize = true;
            this.labelStartChainageM.Location = new System.Drawing.Point(19, 44);
            this.labelStartChainageM.Name = "labelStartChainageM";
            this.labelStartChainageM.Size = new System.Drawing.Size(98, 13);
            this.labelStartChainageM.TabIndex = 7;
            this.labelStartChainageM.Text = "Start Chainage (M):";
            // 
            // labelStringLabel
            // 
            this.labelStringLabel.AutoSize = true;
            this.labelStringLabel.Location = new System.Drawing.Point(211, 18);
            this.labelStringLabel.Name = "labelStringLabel";
            this.labelStringLabel.Size = new System.Drawing.Size(66, 13);
            this.labelStringLabel.TabIndex = 8;
            this.labelStringLabel.Text = "String Label:";
            // 
            // labelEndChainageM
            // 
            this.labelEndChainageM.AutoSize = true;
            this.labelEndChainageM.Location = new System.Drawing.Point(211, 44);
            this.labelEndChainageM.Name = "labelEndChainageM";
            this.labelEndChainageM.Size = new System.Drawing.Size(95, 13);
            this.labelEndChainageM.TabIndex = 9;
            this.labelEndChainageM.Text = "End Chainage (M):";
            // 
            // labelChainageIntervalM
            // 
            this.labelChainageIntervalM.AutoSize = true;
            this.labelChainageIntervalM.Location = new System.Drawing.Point(400, 44);
            this.labelChainageIntervalM.Name = "labelChainageIntervalM";
            this.labelChainageIntervalM.Size = new System.Drawing.Size(111, 13);
            this.labelChainageIntervalM.TabIndex = 10;
            this.labelChainageIntervalM.Text = "Chainage Interval (M):";
            // 
            // checkBoxDVCL
            // 
            this.checkBoxDVCL.AutoSize = true;
            this.checkBoxDVCL.Location = new System.Drawing.Point(116, 69);
            this.checkBoxDVCL.Name = "checkBoxDVCL";
            this.checkBoxDVCL.Size = new System.Drawing.Size(194, 17);
            this.checkBoxDVCL.TabIndex = 5;
            this.checkBoxDVCL.Text = "Default Vertical Curve Length (VCL)";
            this.checkBoxDVCL.UseVisualStyleBackColor = true;
            this.checkBoxDVCL.CheckStateChanged += new System.EventHandler(this.checkBoxDVCL_CheckStateChanged);
            // 
            // groupBoxVIPParameters
            // 
            this.groupBoxVIPParameters.Controls.Add(this.buttonDelete);
            this.groupBoxVIPParameters.Controls.Add(this.buttonEdit);
            this.groupBoxVIPParameters.Controls.Add(this.buttonInsert);
            this.groupBoxVIPParameters.Controls.Add(this.listView1);
            this.groupBoxVIPParameters.Location = new System.Drawing.Point(15, 90);
            this.groupBoxVIPParameters.Name = "groupBoxVIPParameters";
            this.groupBoxVIPParameters.Size = new System.Drawing.Size(588, 243);
            this.groupBoxVIPParameters.TabIndex = 7;
            this.groupBoxVIPParameters.TabStop = false;
            this.groupBoxVIPParameters.Text = "VIP Parameters";
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(349, 214);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 3;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(253, 214);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(75, 23);
            this.buttonEdit.TabIndex = 2;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonInsert
            // 
            this.buttonInsert.Location = new System.Drawing.Point(156, 214);
            this.buttonInsert.Name = "buttonInsert";
            this.buttonInsert.Size = new System.Drawing.Size(75, 23);
            this.buttonInsert.TabIndex = 1;
            this.buttonInsert.Text = "Insert";
            this.buttonInsert.UseVisualStyleBackColor = true;
            this.buttonInsert.Click += new System.EventHandler(this.buttonInsert_Click);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Location = new System.Drawing.Point(11, 16);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(568, 192);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.Click += new System.EventHandler(this.listView1_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "VIP_CHAIN(M)";
            this.columnHeader1.Width = 90;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "VIP_LEVEL(M)";
            this.columnHeader2.Width = 93;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Grade(%)";
            this.columnHeader3.Width = 70;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Symmetrical";
            this.columnHeader4.Width = 73;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "VCL / VCL1(M)";
            this.columnHeader5.Width = 95;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "VCL2(M)";
            this.columnHeader6.Width = 83;
            // 
            // buttonCanel
            // 
            this.buttonCanel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCanel.Location = new System.Drawing.Point(366, 340);
            this.buttonCanel.Name = "buttonCanel";
            this.buttonCanel.Size = new System.Drawing.Size(75, 23);
            this.buttonCanel.TabIndex = 10;
            this.buttonCanel.Text = "Cancel";
            this.buttonCanel.UseVisualStyleBackColor = true;
            this.buttonCanel.Click += new System.EventHandler(this.buttonCanel_Click);
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(270, 340);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 9;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // buttonSaveFinish
            // 
            this.buttonSaveFinish.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSaveFinish.Location = new System.Drawing.Point(173, 340);
            this.buttonSaveFinish.Name = "buttonSaveFinish";
            this.buttonSaveFinish.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveFinish.TabIndex = 8;
            this.buttonSaveFinish.Text = "Save+Finish";
            this.buttonSaveFinish.UseVisualStyleBackColor = true;
            this.buttonSaveFinish.Click += new System.EventHandler(this.buttonSaveFinish_Click);
            // 
            // FormDesignValign
            // 
            this.AcceptButton = this.buttonSaveFinish;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCanel;
            this.ClientSize = new System.Drawing.Size(615, 370);
            this.Controls.Add(this.buttonCanel);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.groupBoxVIPParameters);
            this.Controls.Add(this.buttonSaveFinish);
            this.Controls.Add(this.checkBoxDVCL);
            this.Controls.Add(this.labelChainageIntervalM);
            this.Controls.Add(this.labelEndChainageM);
            this.Controls.Add(this.labelStringLabel);
            this.Controls.Add(this.labelStartChainageM);
            this.Controls.Add(this.textBoxChainageIntervalM);
            this.Controls.Add(this.textBoxDVCL);
            this.Controls.Add(this.textBoxEndChainageM);
            this.Controls.Add(this.textBoxStringLabel);
            this.Controls.Add(this.textBoxStartChainageM);
            this.Controls.Add(this.textBoxModelName);
            this.Controls.Add(this.labelModelName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormDesignValign";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormDesignValign";
            this.Click += new System.EventHandler(this.FormDesignValign_Click);
            this.groupBoxVIPParameters.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelModelName;
        private System.Windows.Forms.TextBox textBoxModelName;
        private System.Windows.Forms.TextBox textBoxStartChainageM;
        private System.Windows.Forms.TextBox textBoxStringLabel;
        private System.Windows.Forms.TextBox textBoxEndChainageM;
        private System.Windows.Forms.TextBox textBoxDVCL;
        private System.Windows.Forms.TextBox textBoxChainageIntervalM;
        private System.Windows.Forms.Label labelStartChainageM;
        private System.Windows.Forms.Label labelStringLabel;
        private System.Windows.Forms.Label labelEndChainageM;
        private System.Windows.Forms.Label labelChainageIntervalM;
        private System.Windows.Forms.CheckBox checkBoxDVCL;
        private System.Windows.Forms.GroupBox groupBoxVIPParameters;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonInsert;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonCanel;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonSaveFinish;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
    }
}