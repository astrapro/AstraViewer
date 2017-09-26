namespace HEADSNeed.ASTRA.ASTRAForms
{
    partial class frm_ModelEdit
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
            this.txt_HEADS_Data = new System.Windows.Forms.TextBox();
            this.btn_Proceed = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_HEADS_Data
            // 
            this.txt_HEADS_Data.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_HEADS_Data.Location = new System.Drawing.Point(12, 12);
            this.txt_HEADS_Data.Multiline = true;
            this.txt_HEADS_Data.Name = "txt_HEADS_Data";
            this.txt_HEADS_Data.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_HEADS_Data.Size = new System.Drawing.Size(374, 168);
            this.txt_HEADS_Data.TabIndex = 0;
            this.txt_HEADS_Data.Text = "HEADS\r\n800 MODELEDIT\r\n891 REPORT MODEL=? STRING=?\r\nFINISH";
            // 
            // btn_Proceed
            // 
            this.btn_Proceed.Location = new System.Drawing.Point(86, 186);
            this.btn_Proceed.Name = "btn_Proceed";
            this.btn_Proceed.Size = new System.Drawing.Size(75, 23);
            this.btn_Proceed.TabIndex = 1;
            this.btn_Proceed.Text = "Proceed";
            this.btn_Proceed.UseVisualStyleBackColor = true;
            this.btn_Proceed.Click += new System.EventHandler(this.btn_Proceed_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(246, 186);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 2;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            // 
            // frm_ModelEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 210);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_Proceed);
            this.Controls.Add(this.txt_HEADS_Data);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "frm_ModelEdit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Model Edit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_HEADS_Data;
        private System.Windows.Forms.Button btn_Proceed;
        private System.Windows.Forms.Button btn_cancel;
    }
}