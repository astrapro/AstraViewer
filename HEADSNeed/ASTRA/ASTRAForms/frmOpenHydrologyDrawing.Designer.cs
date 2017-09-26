namespace HEADSNeed.ASTRA.ASTRAForms
{
    partial class frmOpenHydrologyDrawing
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
            this.btn_sample_drawing_data = new System.Windows.Forms.Button();
            this.btn_project_drawing = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_sample_drawing_data
            // 
            this.btn_sample_drawing_data.Location = new System.Drawing.Point(12, 12);
            this.btn_sample_drawing_data.Name = "btn_sample_drawing_data";
            this.btn_sample_drawing_data.Size = new System.Drawing.Size(127, 23);
            this.btn_sample_drawing_data.TabIndex = 0;
            this.btn_sample_drawing_data.Text = "Sample Drawing && Data";
            this.btn_sample_drawing_data.UseVisualStyleBackColor = true;
            this.btn_sample_drawing_data.Click += new System.EventHandler(this.btn_sample_drawing_data_Click);
            // 
            // btn_project_drawing
            // 
            this.btn_project_drawing.Location = new System.Drawing.Point(145, 12);
            this.btn_project_drawing.Name = "btn_project_drawing";
            this.btn_project_drawing.Size = new System.Drawing.Size(127, 23);
            this.btn_project_drawing.TabIndex = 1;
            this.btn_project_drawing.Text = "Project Drawing";
            this.btn_project_drawing.UseVisualStyleBackColor = true;
            this.btn_project_drawing.Click += new System.EventHandler(this.btn_sample_drawing_data_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(278, 12);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(127, 23);
            this.btn_cancel.TabIndex = 2;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_sample_drawing_data_Click);
            // 
            // frmOpenHydrologyDrawing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 49);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_project_drawing);
            this.Controls.Add(this.btn_sample_drawing_data);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOpenHydrologyDrawing";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Open Drawing for River Alignment";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_sample_drawing_data;
        private System.Windows.Forms.Button btn_project_drawing;
        private System.Windows.Forms.Button btn_cancel;
    }
}