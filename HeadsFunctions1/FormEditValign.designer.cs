namespace HeadsFunctions1.Valignment
{
    partial class FormEditValign
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
            this.labelVipChain = new System.Windows.Forms.Label();
            this.labelVipLevel = new System.Windows.Forms.Label();
            this.labelSymmetrical = new System.Windows.Forms.Label();
            this.labelVclVcl1 = new System.Windows.Forms.Label();
            this.labelVcl2 = new System.Windows.Forms.Label();
            this.textBoxVipChain = new System.Windows.Forms.TextBox();
            this.comboBoxSymmetrical = new System.Windows.Forms.ComboBox();
            this.textBoxVipLevel = new System.Windows.Forms.TextBox();
            this.textBoxVclVcl1 = new System.Windows.Forms.TextBox();
            this.textBoxVcl2 = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelVipChain
            // 
            this.labelVipChain.AutoSize = true;
            this.labelVipChain.Location = new System.Drawing.Point(12, 16);
            this.labelVipChain.Name = "labelVipChain";
            this.labelVipChain.Size = new System.Drawing.Size(70, 13);
            this.labelVipChain.TabIndex = 0;
            this.labelVipChain.Text = "VIP_Chain(X)";
            // 
            // labelVipLevel
            // 
            this.labelVipLevel.AutoSize = true;
            this.labelVipLevel.Location = new System.Drawing.Point(103, 16);
            this.labelVipLevel.Name = "labelVipLevel";
            this.labelVipLevel.Size = new System.Drawing.Size(69, 13);
            this.labelVipLevel.TabIndex = 1;
            this.labelVipLevel.Text = "VIP_Level(Y)";
            // 
            // labelSymmetrical
            // 
            this.labelSymmetrical.AutoSize = true;
            this.labelSymmetrical.Location = new System.Drawing.Point(194, 16);
            this.labelSymmetrical.Name = "labelSymmetrical";
            this.labelSymmetrical.Size = new System.Drawing.Size(63, 13);
            this.labelSymmetrical.TabIndex = 2;
            this.labelSymmetrical.Text = "Symmetrical";
            // 
            // labelVclVcl1
            // 
            this.labelVclVcl1.AutoSize = true;
            this.labelVclVcl1.Location = new System.Drawing.Point(295, 16);
            this.labelVclVcl1.Name = "labelVclVcl1";
            this.labelVclVcl1.Size = new System.Drawing.Size(64, 13);
            this.labelVclVcl1.TabIndex = 3;
            this.labelVclVcl1.Text = "VCL / VCL1";
            // 
            // labelVcl2
            // 
            this.labelVcl2.AutoSize = true;
            this.labelVcl2.Location = new System.Drawing.Point(386, 16);
            this.labelVcl2.Name = "labelVcl2";
            this.labelVcl2.Size = new System.Drawing.Size(33, 13);
            this.labelVcl2.TabIndex = 4;
            this.labelVcl2.Text = "VCL2";
            // 
            // textBoxVipChain
            // 
            this.textBoxVipChain.Location = new System.Drawing.Point(15, 40);
            this.textBoxVipChain.Name = "textBoxVipChain";
            this.textBoxVipChain.Size = new System.Drawing.Size(85, 20);
            this.textBoxVipChain.TabIndex = 0;
            // 
            // comboBoxSymmetrical
            // 
            this.comboBoxSymmetrical.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSymmetrical.FormattingEnabled = true;
            this.comboBoxSymmetrical.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.comboBoxSymmetrical.Location = new System.Drawing.Point(197, 40);
            this.comboBoxSymmetrical.Name = "comboBoxSymmetrical";
            this.comboBoxSymmetrical.Size = new System.Drawing.Size(85, 21);
            this.comboBoxSymmetrical.TabIndex = 2;
            this.comboBoxSymmetrical.SelectedIndexChanged += new System.EventHandler(this.comboBoxSymmetrical_SelectedIndexChanged);
            // 
            // textBoxVipLevel
            // 
            this.textBoxVipLevel.Location = new System.Drawing.Point(106, 40);
            this.textBoxVipLevel.Name = "textBoxVipLevel";
            this.textBoxVipLevel.Size = new System.Drawing.Size(85, 20);
            this.textBoxVipLevel.TabIndex = 1;
            // 
            // textBoxVclVcl1
            // 
            this.textBoxVclVcl1.Location = new System.Drawing.Point(288, 40);
            this.textBoxVclVcl1.Name = "textBoxVclVcl1";
            this.textBoxVclVcl1.Size = new System.Drawing.Size(85, 20);
            this.textBoxVclVcl1.TabIndex = 3;
            // 
            // textBoxVcl2
            // 
            this.textBoxVcl2.Location = new System.Drawing.Point(379, 40);
            this.textBoxVcl2.Name = "textBoxVcl2";
            this.textBoxVcl2.Size = new System.Drawing.Size(85, 20);
            this.textBoxVcl2.TabIndex = 4;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(162, 83);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(246, 83);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FormEditValign
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(482, 118);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textBoxVcl2);
            this.Controls.Add(this.textBoxVclVcl1);
            this.Controls.Add(this.textBoxVipLevel);
            this.Controls.Add(this.comboBoxSymmetrical);
            this.Controls.Add(this.textBoxVipChain);
            this.Controls.Add(this.labelVcl2);
            this.Controls.Add(this.labelVclVcl1);
            this.Controls.Add(this.labelSymmetrical);
            this.Controls.Add(this.labelVipLevel);
            this.Controls.Add(this.labelVipChain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEditValign";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Valignment Parameters";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEditValign_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelVipChain;
        private System.Windows.Forms.Label labelVipLevel;
        private System.Windows.Forms.Label labelSymmetrical;
        private System.Windows.Forms.Label labelVclVcl1;
        private System.Windows.Forms.Label labelVcl2;
        private System.Windows.Forms.TextBox textBoxVipChain;
        private System.Windows.Forms.ComboBox comboBoxSymmetrical;
        private System.Windows.Forms.TextBox textBoxVipLevel;
        private System.Windows.Forms.TextBox textBoxVclVcl1;
        private System.Windows.Forms.TextBox textBoxVcl2;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}