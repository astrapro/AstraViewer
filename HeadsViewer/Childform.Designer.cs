namespace HeadsViewer
{
    partial class Childform
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Childform));
            this.vdScrollableControl1 = new vdScrollableControl.vdScrollableControl();
            this.vdDocumentComponent1 = new VectorDraw.Professional.Components.vdDocumentComponent(this.components);
            this.SuspendLayout();
            // 
            // vdScrollableControl1
            // 
            this.vdScrollableControl1.AllowDrop = true;
            this.vdScrollableControl1.BackColor = System.Drawing.SystemColors.Control;
            this.vdScrollableControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vdScrollableControl1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.vdScrollableControl1.Location = new System.Drawing.Point(0, 0);
            this.vdScrollableControl1.Name = "vdScrollableControl1";
            this.vdScrollableControl1.ShowLayoutPopupMenu = true;
            this.vdScrollableControl1.Size = new System.Drawing.Size(721, 413);
            this.vdScrollableControl1.TabIndex = 0;
            // 
            // Childform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 413);
            this.Controls.Add(this.vdScrollableControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Childform";
            this.Text = "Childform";
            this.Load += new System.EventHandler(this.Childform_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Childform_MouseClick);
            this.ResumeLayout(false);

        }

        #endregion

        internal vdScrollableControl.vdScrollableControl vdScrollableControl1;
        private VectorDraw.Professional.Components.vdDocumentComponent vdDocumentComponent1;
    }
}