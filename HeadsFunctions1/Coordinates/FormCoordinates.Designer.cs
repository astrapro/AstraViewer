namespace HeadsFunctions1.Coordinates
{
    partial class FormCoordinates
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
            System.Windows.Forms.Label label3;
            this.btnOk_ = new System.Windows.Forms.Button();
            this.btnCancel_ = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.tbYInterval_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.tbXInterval_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            this.tbTextSize_ = new HeadsFunctions1.CustomCtrls.TextBoxDouble();
            groupBox1 = new System.Windows.Forms.GroupBox();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.tbYInterval_);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(this.tbXInterval_);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(this.tbTextSize_);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new System.Drawing.Point(7, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(226, 121);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Parameter";
            // 
            // btnOk_
            // 
            this.btnOk_.Location = new System.Drawing.Point(93, 131);
            this.btnOk_.Name = "btnOk_";
            this.btnOk_.Size = new System.Drawing.Size(62, 25);
            this.btnOk_.TabIndex = 5;
            this.btnOk_.Text = "OK";
            this.btnOk_.UseVisualStyleBackColor = true;
            this.btnOk_.Click += new System.EventHandler(this.btnOk__Click);
            // 
            // btnCancel_
            // 
            this.btnCancel_.Location = new System.Drawing.Point(169, 131);
            this.btnCancel_.Name = "btnCancel_";
            this.btnCancel_.Size = new System.Drawing.Size(62, 25);
            this.btnCancel_.TabIndex = 4;
            this.btnCancel_.Text = "Cancel";
            this.btnCancel_.UseVisualStyleBackColor = true;
            this.btnCancel_.Click += new System.EventHandler(this.btnCancel__Click);
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 93);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(76, 13);
            label2.TabIndex = 5;
            label2.Text = "Text Size (mm)";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 20);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(52, 13);
            label1.TabIndex = 7;
            label1.Text = "X-Interval";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(12, 56);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(52, 13);
            label3.TabIndex = 9;
            label3.Text = "Y-Interval";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // tbYInterval_
            // 
            this.tbYInterval_.Location = new System.Drawing.Point(118, 52);
            this.tbYInterval_.MaskFormat = "";
            this.tbYInterval_.MaximumValue = 1.7976931348623157E+308;
            this.tbYInterval_.MinimumValue = 0;
            this.tbYInterval_.Name = "tbYInterval_";
            this.tbYInterval_.Size = new System.Drawing.Size(91, 20);
            this.tbYInterval_.TabIndex = 10;
            this.tbYInterval_.Text = "200";
            this.tbYInterval_.Value = 200;
            this.tbYInterval_.TextChanged += new System.EventHandler(this.TextBox__TextChanged);
            // 
            // tbXInterval_
            // 
            this.tbXInterval_.Location = new System.Drawing.Point(118, 16);
            this.tbXInterval_.MaskFormat = "";
            this.tbXInterval_.MaximumValue = 1.7976931348623157E+308;
            this.tbXInterval_.MinimumValue = 0;
            this.tbXInterval_.Name = "tbXInterval_";
            this.tbXInterval_.Size = new System.Drawing.Size(91, 20);
            this.tbXInterval_.TabIndex = 8;
            this.tbXInterval_.Text = "200";
            this.tbXInterval_.Value = 200;
            this.tbXInterval_.TextChanged += new System.EventHandler(this.TextBox__TextChanged);
            // 
            // tbTextSize_
            // 
            this.tbTextSize_.Location = new System.Drawing.Point(118, 89);
            this.tbTextSize_.MaskFormat = "";
            this.tbTextSize_.MaximumValue = 1.7976931348623157E+308;
            this.tbTextSize_.MinimumValue = 0;
            this.tbTextSize_.Name = "tbTextSize_";
            this.tbTextSize_.Size = new System.Drawing.Size(91, 20);
            this.tbTextSize_.TabIndex = 6;
            this.tbTextSize_.Text = "5";
            this.tbTextSize_.Value = 5;
            this.tbTextSize_.TextChanged += new System.EventHandler(this.TextBox__TextChanged);
            // 
            // FormCoordinates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 165);
            this.Controls.Add(this.btnOk_);
            this.Controls.Add(this.btnCancel_);
            this.Controls.Add(groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCoordinates";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Coordinates";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk_;
        private System.Windows.Forms.Button btnCancel_;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble tbYInterval_;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble tbXInterval_;
        private HeadsFunctions1.CustomCtrls.TextBoxDouble tbTextSize_;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}