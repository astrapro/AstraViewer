namespace HeadsFunctions1.LoadDeflection
{
    partial class FormLoadDeflection
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
            this.progressBar_ = new System.Windows.Forms.ProgressBar();
            this.buttonFirst_ = new System.Windows.Forms.Button();
            this.buttonPrev_ = new System.Windows.Forms.Button();
            this.buttonLast_ = new System.Windows.Forms.Button();
            this.buttonNext_ = new System.Windows.Forms.Button();
            this.buttonStop_ = new System.Windows.Forms.Button();
            this.buttonPlay_ = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressBar_
            // 
            this.progressBar_.Location = new System.Drawing.Point(5, 9);
            this.progressBar_.MarqueeAnimationSpeed = 30;
            this.progressBar_.Name = "progressBar_";
            this.progressBar_.Size = new System.Drawing.Size(274, 23);
            this.progressBar_.Step = 1;
            this.progressBar_.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar_.TabIndex = 0;
            // 
            // buttonFirst_
            // 
            this.buttonFirst_.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonFirst_.Image = global::HeadsFunctions1.Properties.Resources.FirstButton;
            this.buttonFirst_.Location = new System.Drawing.Point(12, 3);
            this.buttonFirst_.Name = "buttonFirst_";
            this.buttonFirst_.Size = new System.Drawing.Size(35, 34);
            this.buttonFirst_.TabIndex = 1;
            this.buttonFirst_.UseVisualStyleBackColor = true;
            this.buttonFirst_.Click += new System.EventHandler(this.buttonFirst__Click);
            // 
            // buttonPrev_
            // 
            this.buttonPrev_.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonPrev_.Image = global::HeadsFunctions1.Properties.Resources.PrevButton;
            this.buttonPrev_.Location = new System.Drawing.Point(47, 3);
            this.buttonPrev_.Name = "buttonPrev_";
            this.buttonPrev_.Size = new System.Drawing.Size(35, 34);
            this.buttonPrev_.TabIndex = 2;
            this.buttonPrev_.UseVisualStyleBackColor = true;
            this.buttonPrev_.Click += new System.EventHandler(this.buttonPrev__Click);
            // 
            // buttonLast_
            // 
            this.buttonLast_.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonLast_.Image = global::HeadsFunctions1.Properties.Resources.LastButton;
            this.buttonLast_.Location = new System.Drawing.Point(236, 3);
            this.buttonLast_.Name = "buttonLast_";
            this.buttonLast_.Size = new System.Drawing.Size(35, 34);
            this.buttonLast_.TabIndex = 4;
            this.buttonLast_.UseVisualStyleBackColor = true;
            this.buttonLast_.Click += new System.EventHandler(this.buttonLast__Click);
            // 
            // buttonNext_
            // 
            this.buttonNext_.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonNext_.Image = global::HeadsFunctions1.Properties.Resources.NextButton;
            this.buttonNext_.Location = new System.Drawing.Point(201, 3);
            this.buttonNext_.Name = "buttonNext_";
            this.buttonNext_.Size = new System.Drawing.Size(35, 34);
            this.buttonNext_.TabIndex = 3;
            this.buttonNext_.UseVisualStyleBackColor = true;
            this.buttonNext_.Click += new System.EventHandler(this.buttonNext__Click);
            // 
            // buttonStop_
            // 
            this.buttonStop_.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonStop_.Image = global::HeadsFunctions1.Properties.Resources.PauseButton;
            this.buttonStop_.Location = new System.Drawing.Point(106, 3);
            this.buttonStop_.Name = "buttonStop_";
            this.buttonStop_.Size = new System.Drawing.Size(35, 34);
            this.buttonStop_.TabIndex = 6;
            this.buttonStop_.UseVisualStyleBackColor = true;
            this.buttonStop_.Click += new System.EventHandler(this.buttonStop__Click);
            // 
            // buttonPlay_
            // 
            this.buttonPlay_.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonPlay_.Image = global::HeadsFunctions1.Properties.Resources.PlayButton;
            this.buttonPlay_.Location = new System.Drawing.Point(141, 3);
            this.buttonPlay_.Name = "buttonPlay_";
            this.buttonPlay_.Size = new System.Drawing.Size(35, 34);
            this.buttonPlay_.TabIndex = 5;
            this.buttonPlay_.UseVisualStyleBackColor = true;
            this.buttonPlay_.Click += new System.EventHandler(this.buttonPlay__Click);
            // 
            // FormLoadDeflection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 43);
            this.Controls.Add(this.buttonStop_);
            this.Controls.Add(this.buttonPlay_);
            this.Controls.Add(this.buttonLast_);
            this.Controls.Add(this.buttonNext_);
            this.Controls.Add(this.buttonPrev_);
            this.Controls.Add(this.buttonFirst_);
            this.Controls.Add(this.progressBar_);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormLoadDeflection";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Load Deflection [Loading data...]";
            this.Load += new System.EventHandler(this.FormLoadDeflection_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLoadDeflection_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar_;
        private System.Windows.Forms.Button buttonFirst_;
        private System.Windows.Forms.Button buttonPrev_;
        private System.Windows.Forms.Button buttonLast_;
        private System.Windows.Forms.Button buttonNext_;
        private System.Windows.Forms.Button buttonStop_;
        private System.Windows.Forms.Button buttonPlay_;
    }
}