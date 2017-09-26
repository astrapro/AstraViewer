namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    partial class frmMemberGroups
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
            this.btn_add_data = new System.Windows.Forms.Button();
            this.txt_mem_nos = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_group_name = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_add_data
            // 
            this.btn_add_data.Location = new System.Drawing.Point(143, 73);
            this.btn_add_data.Name = "btn_add_data";
            this.btn_add_data.Size = new System.Drawing.Size(112, 29);
            this.btn_add_data.TabIndex = 5;
            this.btn_add_data.Text = "Add Data";
            this.btn_add_data.UseVisualStyleBackColor = true;
            this.btn_add_data.Click += new System.EventHandler(this.btn_add_data_Click);
            // 
            // txt_mem_nos
            // 
            this.txt_mem_nos.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_mem_nos.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_mem_nos.Location = new System.Drawing.Point(110, 45);
            this.txt_mem_nos.Name = "txt_mem_nos";
            this.txt_mem_nos.Size = new System.Drawing.Size(368, 22);
            this.txt_mem_nos.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Member Numbers ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Group Name";
            // 
            // txt_group_name
            // 
            this.txt_group_name.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_group_name.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_group_name.Location = new System.Drawing.Point(110, 12);
            this.txt_group_name.Name = "txt_group_name";
            this.txt_group_name.Size = new System.Drawing.Size(109, 22);
            this.txt_group_name.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(225, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(253, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "(Name must be start with a underscore \'_\')";
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(261, 73);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(112, 29);
            this.btn_cancel.TabIndex = 5;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // frmMemberGroups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 114);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_add_data);
            this.Controls.Add(this.txt_group_name);
            this.Controls.Add(this.txt_mem_nos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmMemberGroups";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Member Groups";
            this.Load += new System.EventHandler(this.frmMemberGroups_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_add_data;
        private System.Windows.Forms.TextBox txt_mem_nos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_group_name;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_cancel;
    }
}