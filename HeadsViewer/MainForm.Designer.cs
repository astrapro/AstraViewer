namespace HeadsViewer
{
    partial class MainForm
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
            //CloseASTRADocs();
            //CloseAllDocs();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.AerialViewUpdate = new System.Windows.Forms.Button();
            this.AerialView = new System.Windows.Forms.PictureBox();
            this.vdCommandLine1 = new VectorDraw.Professional.vdCommandLine.vdCommandLine();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.CoordDisplay = new System.Windows.Forms.ToolStripLabel();
            this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.status = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolSnap = new System.Windows.Forms.ToolStripButton();
            this.toolGrid = new System.Windows.Forms.ToolStripButton();
            this.toolOrtho = new System.Windows.Forms.ToolStripButton();
            this.toolOsnap = new System.Windows.Forms.ToolStripButton();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.vdPropertyGrid1 = new vdPropertyGrid.vdPropertyGrid();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.RightToolbarsPanel = new System.Windows.Forms.Panel();
            this.tmr_check_hasp = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AerialView)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.trackBar1);
            this.panel1.Controls.Add(this.AerialViewUpdate);
            this.panel1.Controls.Add(this.AerialView);
            this.panel1.Controls.Add(this.vdCommandLine1);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 274);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(882, 100);
            this.panel1.TabIndex = 7;
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.AutoSize = false;
            this.trackBar1.LargeChange = 10;
            this.trackBar1.Location = new System.Drawing.Point(753, 53);
            this.trackBar1.Maximum = 60;
            this.trackBar1.Minimum = 10;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(126, 19);
            this.trackBar1.SmallChange = 10;
            this.trackBar1.TabIndex = 5;
            this.trackBar1.TickFrequency = 10;
            this.trackBar1.Value = 20;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // AerialViewUpdate
            // 
            this.AerialViewUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AerialViewUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.AerialViewUpdate.Location = new System.Drawing.Point(702, 53);
            this.AerialViewUpdate.Name = "AerialViewUpdate";
            this.AerialViewUpdate.Size = new System.Drawing.Size(45, 19);
            this.AerialViewUpdate.TabIndex = 4;
            this.AerialViewUpdate.Text = "Update";
            this.AerialViewUpdate.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.AerialViewUpdate.UseVisualStyleBackColor = true;
            this.AerialViewUpdate.Click += new System.EventHandler(this.AerialViewUpdate_Click);
            // 
            // AerialView
            // 
            this.AerialView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AerialView.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.AerialView.Cursor = System.Windows.Forms.Cursors.Cross;
            this.AerialView.InitialImage = null;
            this.AerialView.Location = new System.Drawing.Point(702, 0);
            this.AerialView.Name = "AerialView";
            this.AerialView.Size = new System.Drawing.Size(180, 51);
            this.AerialView.TabIndex = 3;
            this.AerialView.TabStop = false;
            this.AerialView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.AerialView_MouseMove);
            this.AerialView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AerialView_MouseClick);
            // 
            // vdCommandLine1
            // 
            this.vdCommandLine1.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.vdCommandLine1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.vdCommandLine1.Location = new System.Drawing.Point(0, 0);
            this.vdCommandLine1.Name = "vdCommandLine1";
            this.vdCommandLine1.ProcessKeyMessages = true;
            this.vdCommandLine1.Size = new System.Drawing.Size(696, 75);
            this.vdCommandLine1.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CoordDisplay,
            this.ProgressBar,
            this.status,
            this.toolStripSeparator1,
            this.toolSnap,
            this.toolGrid,
            this.toolOrtho,
            this.toolOsnap});
            this.toolStrip1.Location = new System.Drawing.Point(0, 75);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(882, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // CoordDisplay
            // 
            this.CoordDisplay.AutoSize = false;
            this.CoordDisplay.Name = "CoordDisplay";
            this.CoordDisplay.Size = new System.Drawing.Size(140, 22);
            this.CoordDisplay.Text = "CoordDisplay";
            this.CoordDisplay.Click += new System.EventHandler(this.CoordDisplay_Click);
            // 
            // ProgressBar
            // 
            this.ProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(150, 22);
            // 
            // status
            // 
            this.status.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(0, 22);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolSnap
            // 
            this.toolSnap.AutoSize = false;
            this.toolSnap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolSnap.Image = ((System.Drawing.Image)(resources.GetObject("toolSnap.Image")));
            this.toolSnap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSnap.Name = "toolSnap";
            this.toolSnap.Size = new System.Drawing.Size(60, 22);
            this.toolSnap.Text = "SNAP OFF";
            this.toolSnap.Click += new System.EventHandler(this.toolSnap_Click);
            // 
            // toolGrid
            // 
            this.toolGrid.AutoSize = false;
            this.toolGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolGrid.Image = ((System.Drawing.Image)(resources.GetObject("toolGrid.Image")));
            this.toolGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolGrid.Name = "toolGrid";
            this.toolGrid.Size = new System.Drawing.Size(59, 22);
            this.toolGrid.Text = "GRID OFF";
            this.toolGrid.Click += new System.EventHandler(this.toolGrid_Click);
            // 
            // toolOrtho
            // 
            this.toolOrtho.AutoSize = false;
            this.toolOrtho.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolOrtho.Image = ((System.Drawing.Image)(resources.GetObject("toolOrtho.Image")));
            this.toolOrtho.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolOrtho.Name = "toolOrtho";
            this.toolOrtho.Size = new System.Drawing.Size(70, 22);
            this.toolOrtho.Text = "ORTHO OFF";
            this.toolOrtho.Click += new System.EventHandler(this.toolOrtho_Click);
            // 
            // toolOsnap
            // 
            this.toolOsnap.AutoSize = false;
            this.toolOsnap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolOsnap.Image = ((System.Drawing.Image)(resources.GetObject("toolOsnap.Image")));
            this.toolOsnap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolOsnap.Name = "toolOsnap";
            this.toolOsnap.Size = new System.Drawing.Size(45, 22);
            this.toolOsnap.Text = "OSNAP";
            this.toolOsnap.Click += new System.EventHandler(this.toolOsnap_Click);
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Location = new System.Drawing.Point(0, 264);
            this.splitter2.MinSize = 70;
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(882, 10);
            this.splitter2.TabIndex = 9;
            this.splitter2.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.vdPropertyGrid1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(264, 264);
            this.panel2.TabIndex = 10;
            // 
            // vdPropertyGrid1
            // 
            this.vdPropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vdPropertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.vdPropertyGrid1.Name = "vdPropertyGrid1";
            this.vdPropertyGrid1.ReadOnly = false;
            this.vdPropertyGrid1.SelectedObject = new object[] {
        ((object)(new object[] {
                ((object)(new object[] {
                        ((object)(new object[] {
                                ((object)(new object[] {
                                        ((object)(new object[] {
                                                ((object)(new object[] {
                                                        ((object)(new object[] {
                                                                ((object)(new object[] {
                                                                        ((object)(new object[] {
                                                                                ((object)(new object[] {
                                                                                        ((object)(new object[] {
                                                                                                ((object)(new object[0]))}))}))}))}))}))}))}))}))}))}))}))};
            this.vdPropertyGrid1.ShowSelectedItemComboBox = true;
            this.vdPropertyGrid1.Size = new System.Drawing.Size(264, 264);
            this.vdPropertyGrid1.TabIndex = 1;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(264, 0);
            this.splitter1.MinSize = 230;
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(6, 264);
            this.splitter1.TabIndex = 11;
            this.splitter1.TabStop = false;
            // 
            // RightToolbarsPanel
            // 
            this.RightToolbarsPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightToolbarsPanel.Location = new System.Drawing.Point(872, 0);
            this.RightToolbarsPanel.Name = "RightToolbarsPanel";
            this.RightToolbarsPanel.Size = new System.Drawing.Size(10, 264);
            this.RightToolbarsPanel.TabIndex = 13;
            // 
            // tmr_check_hasp
            // 
            this.tmr_check_hasp.Enabled = true;
            this.tmr_check_hasp.Interval = 999;
            this.tmr_check_hasp.Tick += new System.EventHandler(this.tmr_check_hasp_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 374);
            this.Controls.Add(this.RightToolbarsPanel);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Heads Viewer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AerialView)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter2;
        public  System.Windows.Forms.Panel panel2;
        private vdPropertyGrid.vdPropertyGrid vdPropertyGrid1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private VectorDraw.Professional.vdCommandLine.vdCommandLine vdCommandLine1;
        internal System.Windows.Forms.ToolStripLabel CoordDisplay;
        internal System.Windows.Forms.ToolStripProgressBar ProgressBar;
        internal System.Windows.Forms.ToolStripLabel status;
        private System.Windows.Forms.Panel RightToolbarsPanel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        internal System.Windows.Forms.ToolStripButton toolSnap;
        internal System.Windows.Forms.ToolStripButton toolGrid;
        internal System.Windows.Forms.ToolStripButton toolOrtho;
        internal System.Windows.Forms.ToolStripButton toolOsnap;
        private System.Windows.Forms.Button AerialViewUpdate;
        private System.Windows.Forms.TrackBar trackBar1;
        public System.Windows.Forms.PictureBox AerialView;
        private System.Windows.Forms.Timer tmr_check_hasp;
    }
}

