namespace HeadsFunctions1.Offset.Modelling
{
    partial class FormModellingOffsetViewer
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
            this.btnProceed_ = new System.Windows.Forms.Button();
            this.listModelCtrl_ = new System.Windows.Forms.ListBox();
            this.btnQuit_ = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.btnProceed_);
            groupBox1.Controls.Add(this.listModelCtrl_);
            groupBox1.Controls.Add(this.btnQuit_);
            groupBox1.Location = new System.Drawing.Point(4, 5);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(615, 388);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Process Model Offset";
            // 
            // btnProceed_
            // 
            this.btnProceed_.Location = new System.Drawing.Point(194, 19);
            this.btnProceed_.Name = "btnProceed_";
            this.btnProceed_.Size = new System.Drawing.Size(83, 25);
            this.btnProceed_.TabIndex = 0;
            this.btnProceed_.Text = "Proceed";
            this.btnProceed_.UseVisualStyleBackColor = true;
            this.btnProceed_.Click += new System.EventHandler(this.btnProceed__Click);
            // 
            // listModelCtrl_
            // 
            this.listModelCtrl_.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listModelCtrl_.FormattingEnabled = true;
            this.listModelCtrl_.Location = new System.Drawing.Point(6, 52);
            this.listModelCtrl_.Name = "listModelCtrl_";
            this.listModelCtrl_.Size = new System.Drawing.Size(603, 329);
            this.listModelCtrl_.TabIndex = 2;
            // 
            // btnQuit_
            // 
            this.btnQuit_.Location = new System.Drawing.Point(297, 19);
            this.btnQuit_.Name = "btnQuit_";
            this.btnQuit_.Size = new System.Drawing.Size(83, 25);
            this.btnQuit_.TabIndex = 1;
            this.btnQuit_.Text = "Quit";
            this.btnQuit_.UseVisualStyleBackColor = true;
            this.btnQuit_.Click += new System.EventHandler(this.btnQuit__Click);
            // 
            // FormModellingOffsetViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 398);
            this.Controls.Add(groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormModellingOffsetViewer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Modelling:Offset";
            groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnProceed_;
        private System.Windows.Forms.ListBox listModelCtrl_;
        private System.Windows.Forms.Button btnQuit_;
    }
}