namespace HeadsViewer
{
    partial class GeneralProperties
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
            this.vdPropertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // vdPropertyGrid1
            // 
            this.vdPropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vdPropertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.vdPropertyGrid1.Name = "vdPropertyGrid1";
            this.vdPropertyGrid1.Size = new System.Drawing.Size(521, 340);
            this.vdPropertyGrid1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(315, 294);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GeneralProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(521, 340);
            this.Controls.Add(this.vdPropertyGrid1);
            this.Controls.Add(this.button1);
            this.Name = "GeneralProperties";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "GeneralProperties";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GeneralProperties_FormClosed);
            this.Load += new System.EventHandler(this.GeneralProperties_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid vdPropertyGrid1;
        private System.Windows.Forms.Button button1;
    }
}