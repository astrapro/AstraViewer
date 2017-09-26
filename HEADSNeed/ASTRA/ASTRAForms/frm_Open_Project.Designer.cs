namespace HEADSNeed.ASTRA.ASTRAForms
{
    partial class frm_Open_Project
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
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lst_proj_folders = new System.Windows.Forms.ListBox();
            this.txt_working_folder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(211, 323);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(115, 30);
            this.btn_Cancel.TabIndex = 9;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(51, 323);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(115, 30);
            this.btn_OK.TabIndex = 10;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Select User\'s Project Folder";
            // 
            // lst_proj_folders
            // 
            this.lst_proj_folders.FormattingEnabled = true;
            this.lst_proj_folders.Location = new System.Drawing.Point(15, 79);
            this.lst_proj_folders.Name = "lst_proj_folders";
            this.lst_proj_folders.ScrollAlwaysVisible = true;
            this.lst_proj_folders.Size = new System.Drawing.Size(350, 238);
            this.lst_proj_folders.TabIndex = 7;
            // 
            // txt_working_folder
            // 
            this.txt_working_folder.Location = new System.Drawing.Point(14, 28);
            this.txt_working_folder.Name = "txt_working_folder";
            this.txt_working_folder.ReadOnly = true;
            this.txt_working_folder.Size = new System.Drawing.Size(351, 21);
            this.txt_working_folder.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "User\'s Working Folder";
            // 
            // frm_Open_Project
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(373, 361);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lst_proj_folders);
            this.Controls.Add(this.txt_working_folder);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Open_Project";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Folder Selection Dialog";
            this.Load += new System.EventHandler(this.frm_Open_Project_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lst_proj_folders;
        private System.Windows.Forms.TextBox txt_working_folder;
        private System.Windows.Forms.Label label1;
    }
}