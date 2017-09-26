namespace HeadsViewer
{
    partial class Publish
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
            this.butBrowse = new System.Windows.Forms.Button();
            this.textPath = new System.Windows.Forms.TextBox();
            this.comboVersion = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.butPublish = new System.Windows.Forms.Button();
            this.butExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // butBrowse
            // 
            this.butBrowse.Location = new System.Drawing.Point(457, 4);
            this.butBrowse.Name = "butBrowse";
            this.butBrowse.Size = new System.Drawing.Size(75, 23);
            this.butBrowse.TabIndex = 0;
            this.butBrowse.Text = "Browse";
            this.butBrowse.UseVisualStyleBackColor = true;
            this.butBrowse.Click += new System.EventHandler(this.butBrowse_Click);
            // 
            // textPath
            // 
            this.textPath.Location = new System.Drawing.Point(90, 6);
            this.textPath.Name = "textPath";
            this.textPath.ReadOnly = true;
            this.textPath.Size = new System.Drawing.Size(361, 20);
            this.textPath.TabIndex = 1;
            // 
            // comboVersion
            // 
            this.comboVersion.FormattingEnabled = true;
            this.comboVersion.Items.AddRange(new object[] {
            ""});
            this.comboVersion.Location = new System.Drawing.Point(93, 40);
            this.comboVersion.Name = "comboVersion";
            this.comboVersion.Size = new System.Drawing.Size(277, 21);
            this.comboVersion.TabIndex = 2;
            this.comboVersion.SelectedIndexChanged += new System.EventHandler(this.comboVersion_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Export Folder:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Export Version";
            // 
            // butPublish
            // 
            this.butPublish.Location = new System.Drawing.Point(376, 39);
            this.butPublish.Name = "butPublish";
            this.butPublish.Size = new System.Drawing.Size(75, 23);
            this.butPublish.TabIndex = 5;
            this.butPublish.Text = "Publish";
            this.butPublish.UseVisualStyleBackColor = true;
            this.butPublish.Click += new System.EventHandler(this.butPublish_Click);
            // 
            // butExit
            // 
            this.butExit.Location = new System.Drawing.Point(457, 39);
            this.butExit.Name = "butExit";
            this.butExit.Size = new System.Drawing.Size(75, 23);
            this.butExit.TabIndex = 6;
            this.butExit.Text = "Exit";
            this.butExit.UseVisualStyleBackColor = true;
            this.butExit.Click += new System.EventHandler(this.butExit_Click);
            // 
            // Publish
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 75);
            this.Controls.Add(this.butExit);
            this.Controls.Add(this.butPublish);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboVersion);
            this.Controls.Add(this.textPath);
            this.Controls.Add(this.butBrowse);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Publish";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Publish";
            this.Load += new System.EventHandler(this.Publish_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button butBrowse;
        private System.Windows.Forms.TextBox textPath;
        private System.Windows.Forms.ComboBox comboVersion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button butPublish;
        private System.Windows.Forms.Button butExit;
    }
}