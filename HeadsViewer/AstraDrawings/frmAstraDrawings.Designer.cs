namespace HeadsViewer.AstraDrawings
{
    partial class frmAstraDrawings
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
            if (lst_drawings.Items.Count != 1)
                irp.CloseAllDrawings();
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
            this.lst_drawings = new System.Windows.Forms.ListBox();
            this.btn_conv_dwg = new System.Windows.Forms.Button();
            this.pb = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // lst_drawings
            // 
            this.lst_drawings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lst_drawings.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lst_drawings.FormattingEnabled = true;
            this.lst_drawings.Location = new System.Drawing.Point(0, 0);
            this.lst_drawings.Name = "lst_drawings";
            this.lst_drawings.Size = new System.Drawing.Size(188, 264);
            this.lst_drawings.TabIndex = 1;
            this.lst_drawings.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // btn_conv_dwg
            // 
            this.btn_conv_dwg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_conv_dwg.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_conv_dwg.Location = new System.Drawing.Point(0, 264);
            this.btn_conv_dwg.Name = "btn_conv_dwg";
            this.btn_conv_dwg.Size = new System.Drawing.Size(188, 24);
            this.btn_conv_dwg.TabIndex = 2;
            this.btn_conv_dwg.Text = "Convert All Drawings to DXF";
            this.btn_conv_dwg.UseVisualStyleBackColor = true;
            this.btn_conv_dwg.Click += new System.EventHandler(this.btn_conv_dwg_Click);
            // 
            // pb
            // 
            this.pb.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pb.Location = new System.Drawing.Point(0, 288);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(188, 23);
            this.pb.TabIndex = 3;
            this.pb.Visible = false;
            this.pb.VisibleChanged += new System.EventHandler(this.pb_VisibleChanged);
            // 
            // frmAstraDrawings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(188, 311);
            this.Controls.Add(this.lst_drawings);
            this.Controls.Add(this.btn_conv_dwg);
            this.Controls.Add(this.pb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAstraDrawings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Multiple Drawings";
            this.Load += new System.EventHandler(this.frmAstraDrawings_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lst_drawings;
        private System.Windows.Forms.Button btn_conv_dwg;
        private System.Windows.Forms.ProgressBar pb;
    }
}