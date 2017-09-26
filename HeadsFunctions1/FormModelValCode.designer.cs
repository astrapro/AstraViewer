namespace HeadsFunctions1.Valignment
{
    partial class FormModelValCode
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
            this.labelProcessModelValign = new System.Windows.Forms.Label();
            this.listBoxModel = new System.Windows.Forms.ListBox();
            this.buttonProceed = new System.Windows.Forms.Button();
            this.buttonQuit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelProcessModelValign
            // 
            this.labelProcessModelValign.AutoSize = true;
            this.labelProcessModelValign.Location = new System.Drawing.Point(12, 19);
            this.labelProcessModelValign.Name = "labelProcessModelValign";
            this.labelProcessModelValign.Size = new System.Drawing.Size(109, 13);
            this.labelProcessModelValign.TabIndex = 0;
            this.labelProcessModelValign.Text = "Process Model Valign";
            // 
            // listBoxModel
            // 
            this.listBoxModel.FormattingEnabled = true;
            this.listBoxModel.Location = new System.Drawing.Point(2, 79);
            this.listBoxModel.Name = "listBoxModel";
            this.listBoxModel.Size = new System.Drawing.Size(710, 355);
            this.listBoxModel.TabIndex = 1;
            // 
            // buttonProceed
            // 
            this.buttonProceed.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonProceed.Location = new System.Drawing.Point(273, 41);
            this.buttonProceed.Name = "buttonProceed";
            this.buttonProceed.Size = new System.Drawing.Size(75, 23);
            this.buttonProceed.TabIndex = 2;
            this.buttonProceed.Text = "Proceed";
            this.buttonProceed.UseVisualStyleBackColor = true;
            // 
            // buttonQuit
            // 
            this.buttonQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonQuit.Location = new System.Drawing.Point(366, 41);
            this.buttonQuit.Name = "buttonQuit";
            this.buttonQuit.Size = new System.Drawing.Size(75, 23);
            this.buttonQuit.TabIndex = 3;
            this.buttonQuit.Text = "Quit";
            this.buttonQuit.UseVisualStyleBackColor = true;
            // 
            // FormModelValCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 440);
            this.Controls.Add(this.buttonQuit);
            this.Controls.Add(this.buttonProceed);
            this.Controls.Add(this.listBoxModel);
            this.Controls.Add(this.labelProcessModelValign);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormModelValCode";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Modelling : Valign";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelProcessModelValign;
        private System.Windows.Forms.ListBox listBoxModel;
        private System.Windows.Forms.Button buttonProceed;
        private System.Windows.Forms.Button buttonQuit;
    }
}