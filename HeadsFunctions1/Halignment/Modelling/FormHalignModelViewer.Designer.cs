namespace HeadsFunctions1.Halignment.Modelling
{
    partial class FormHalignModelViewer
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
            this.btnQuit_ = new System.Windows.Forms.Button();
            this.listBoxView = new System.Windows.Forms.ListBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            groupBox1.Controls.Add(this.listBoxView);
            groupBox1.Controls.Add(this.btnProceed_);
            groupBox1.Controls.Add(this.btnQuit_);
            groupBox1.Location = new System.Drawing.Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(768, 422);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Process Model Halign";
            // 
            // btnProceed_
            // 
            this.btnProceed_.Location = new System.Drawing.Point(261, 19);
            this.btnProceed_.Name = "btnProceed_";
            this.btnProceed_.Size = new System.Drawing.Size(86, 31);
            this.btnProceed_.TabIndex = 2;
            this.btnProceed_.Text = "Proceed";
            this.btnProceed_.UseVisualStyleBackColor = true;
            this.btnProceed_.Click += new System.EventHandler(this.btnProceed__Click);
            // 
            // btnQuit_
            // 
            this.btnQuit_.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuit_.Location = new System.Drawing.Point(392, 19);
            this.btnQuit_.Name = "btnQuit_";
            this.btnQuit_.Size = new System.Drawing.Size(86, 31);
            this.btnQuit_.TabIndex = 1;
            this.btnQuit_.Text = "Quit";
            this.btnQuit_.UseVisualStyleBackColor = true;
            this.btnQuit_.Click += new System.EventHandler(this.btnQuit__Click);
            // 
            // listBoxView
            // 
            this.listBoxView.FormattingEnabled = true;
            this.listBoxView.Location = new System.Drawing.Point(8, 70);
            this.listBoxView.Name = "listBoxView";
            this.listBoxView.Size = new System.Drawing.Size(751, 342);
            this.listBoxView.TabIndex = 3;
            // 
            // FormHalignModelViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 483);
            this.Controls.Add(groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormHalignModelViewer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Modelling: Halign";
            this.Load += new System.EventHandler(this.FormHalignModelViewer_Load);
            groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnQuit_;
        private System.Windows.Forms.Button btnProceed_;
        private System.Windows.Forms.ListBox listBoxView;
    }
}