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
            this.comboSymmetrical_ = new System.Windows.Forms.ComboBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tbVipChain_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.tbVipLevel_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.tbVcl1_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.tbVcl2_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
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
            // comboSymmetrical_
            // 
            this.comboSymmetrical_.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSymmetrical_.FormattingEnabled = true;
            this.comboSymmetrical_.Location = new System.Drawing.Point(197, 35);
            this.comboSymmetrical_.Name = "comboSymmetrical_";
            this.comboSymmetrical_.Size = new System.Drawing.Size(85, 21);
            this.comboSymmetrical_.TabIndex = 2;
            this.comboSymmetrical_.SelectedIndexChanged += new System.EventHandler(this.comboBoxSymmetrical_SelectedIndexChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(305, 67);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 29);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(389, 67);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 29);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // tbVipChain_
            // 
            this.tbVipChain_.Location = new System.Drawing.Point(12, 35);
            this.tbVipChain_.MaskFormat = "";
            this.tbVipChain_.MaximumValue = 1.7976931348623157E+308;
            this.tbVipChain_.MinimumValue = -1.7976931348623157E+308;
            this.tbVipChain_.Name = "tbVipChain_";
            this.tbVipChain_.Size = new System.Drawing.Size(85, 20);
            this.tbVipChain_.TabIndex = 7;
            this.tbVipChain_.Value = 0;
            // 
            // tbVipLevel_
            // 
            this.tbVipLevel_.Location = new System.Drawing.Point(106, 36);
            this.tbVipLevel_.MaskFormat = "";
            this.tbVipLevel_.MaximumValue = 1.7976931348623157E+308;
            this.tbVipLevel_.MinimumValue = -1.7976931348623157E+308;
            this.tbVipLevel_.Name = "tbVipLevel_";
            this.tbVipLevel_.Size = new System.Drawing.Size(85, 20);
            this.tbVipLevel_.TabIndex = 8;
            this.tbVipLevel_.Value = 0;
            // 
            // tbVcl1_
            // 
            this.tbVcl1_.Location = new System.Drawing.Point(288, 35);
            this.tbVcl1_.MaskFormat = "";
            this.tbVcl1_.MaximumValue = 1.7976931348623157E+308;
            this.tbVcl1_.MinimumValue = -1.7976931348623157E+308;
            this.tbVcl1_.Name = "tbVcl1_";
            this.tbVcl1_.Size = new System.Drawing.Size(85, 20);
            this.tbVcl1_.TabIndex = 9;
            this.tbVcl1_.Value = 0;
            // 
            // tbVcl2_
            // 
            this.tbVcl2_.Location = new System.Drawing.Point(379, 35);
            this.tbVcl2_.MaskFormat = "";
            this.tbVcl2_.MaximumValue = 1.7976931348623157E+308;
            this.tbVcl2_.MinimumValue = -1.7976931348623157E+308;
            this.tbVcl2_.Name = "tbVcl2_";
            this.tbVcl2_.Size = new System.Drawing.Size(85, 20);
            this.tbVcl2_.TabIndex = 10;
            this.tbVcl2_.Value = 0;
            // 
            // FormEditValign
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(474, 104);
            this.Controls.Add(this.tbVcl2_);
            this.Controls.Add(this.tbVcl1_);
            this.Controls.Add(this.tbVipLevel_);
            this.Controls.Add(this.tbVipChain_);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.comboSymmetrical_);
            this.Controls.Add(this.labelVcl2);
            this.Controls.Add(this.labelVclVcl1);
            this.Controls.Add(this.labelSymmetrical);
            this.Controls.Add(this.labelVipLevel);
            this.Controls.Add(this.labelVipChain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEditValign";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Valignment Parameters";
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
        private System.Windows.Forms.ComboBox comboSymmetrical_;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble tbVcl2_;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble tbVcl1_;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble tbVipLevel_;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble tbVipChain_;
    }
}