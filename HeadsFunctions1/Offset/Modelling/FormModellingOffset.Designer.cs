namespace HeadsFunctions1.Offset.Modelling
{
    partial class FormModellingOffset
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
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.GroupBox groupBox2;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label3;
            this.listBoxRefStr_ = new System.Windows.Forms.ListBox();
            this.comboRefModel_ = new System.Windows.Forms.ComboBox();
            this.listBoxOffsetStr_ = new System.Windows.Forms.ListBox();
            this.comboOffModel_ = new System.Windows.Forms.ComboBox();
            this.btnCancel_ = new System.Windows.Forms.Button();
            this.btnOk_ = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            groupBox2 = new System.Windows.Forms.GroupBox();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.listBoxRefStr_);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(this.comboRefModel_);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new System.Drawing.Point(9, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(185, 232);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Reference";
            // 
            // listBoxRefStr_
            // 
            this.listBoxRefStr_.FormattingEnabled = true;
            this.listBoxRefStr_.Location = new System.Drawing.Point(10, 77);
            this.listBoxRefStr_.Name = "listBoxRefStr_";
            this.listBoxRefStr_.Size = new System.Drawing.Size(166, 147);
            this.listBoxRefStr_.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(10, 60);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(63, 13);
            label2.TabIndex = 2;
            label2.Text = "String Label";
            // 
            // comboRefModel_
            // 
            this.comboRefModel_.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboRefModel_.FormattingEnabled = true;
            this.comboRefModel_.Location = new System.Drawing.Point(10, 36);
            this.comboRefModel_.Name = "comboRefModel_";
            this.comboRefModel_.Size = new System.Drawing.Size(166, 21);
            this.comboRefModel_.TabIndex = 1;
            this.comboRefModel_.SelectedIndexChanged += new System.EventHandler(this.comboRefModel__SelectedIndexChanged);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(7, 20);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(67, 13);
            label1.TabIndex = 0;
            label1.Text = "Model Name";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(this.listBoxOffsetStr_);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(this.comboOffModel_);
            groupBox2.Controls.Add(label3);
            groupBox2.Location = new System.Drawing.Point(200, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(185, 232);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Offset";
            // 
            // listBoxOffsetStr_
            // 
            this.listBoxOffsetStr_.FormattingEnabled = true;
            this.listBoxOffsetStr_.Location = new System.Drawing.Point(10, 77);
            this.listBoxOffsetStr_.Name = "listBoxOffsetStr_";
            this.listBoxOffsetStr_.Size = new System.Drawing.Size(166, 147);
            this.listBoxOffsetStr_.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(10, 60);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(63, 13);
            label4.TabIndex = 2;
            label4.Text = "String Label";
            // 
            // comboOffModel_
            // 
            this.comboOffModel_.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboOffModel_.FormattingEnabled = true;
            this.comboOffModel_.Location = new System.Drawing.Point(10, 35);
            this.comboOffModel_.Name = "comboOffModel_";
            this.comboOffModel_.Size = new System.Drawing.Size(166, 21);
            this.comboOffModel_.TabIndex = 1;
            this.comboOffModel_.SelectedIndexChanged += new System.EventHandler(this.comboOffModel__SelectedIndexChanged);
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(7, 20);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(67, 13);
            label3.TabIndex = 0;
            label3.Text = "Model Name";
            // 
            // btnCancel_
            // 
            this.btnCancel_.Location = new System.Drawing.Point(310, 241);
            this.btnCancel_.Name = "btnCancel_";
            this.btnCancel_.Size = new System.Drawing.Size(75, 28);
            this.btnCancel_.TabIndex = 2;
            this.btnCancel_.Text = "Cancel";
            this.btnCancel_.UseVisualStyleBackColor = true;
            this.btnCancel_.Click += new System.EventHandler(this.btnCancel__Click);
            // 
            // btnOk_
            // 
            this.btnOk_.Location = new System.Drawing.Point(213, 241);
            this.btnOk_.Name = "btnOk_";
            this.btnOk_.Size = new System.Drawing.Size(75, 28);
            this.btnOk_.TabIndex = 3;
            this.btnOk_.Text = "OK";
            this.btnOk_.UseVisualStyleBackColor = true;
            this.btnOk_.Click += new System.EventHandler(this.btnOk__Click);
            // 
            // FormModellingOffset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 278);
            this.Controls.Add(this.btnOk_);
            this.Controls.Add(this.btnCancel_);
            this.Controls.Add(groupBox2);
            this.Controls.Add(groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormModellingOffset";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Modelling:Offset";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboRefModel_;
        private System.Windows.Forms.ComboBox comboOffModel_;
        private System.Windows.Forms.Button btnCancel_;
        private System.Windows.Forms.Button btnOk_;
        private System.Windows.Forms.ListBox listBoxRefStr_;
        private System.Windows.Forms.ListBox listBoxOffsetStr_;


    }
}