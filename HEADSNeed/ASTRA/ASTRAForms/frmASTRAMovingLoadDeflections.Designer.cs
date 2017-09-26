namespace HEADSNeed.ASTRA.ASTRAForms
{
    partial class frmASTRAMovingLoadDeflections
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
            this.grb_manual = new System.Windows.Forms.GroupBox();
            this.lbl_distance = new System.Windows.Forms.Label();
            this.txt_dist = new System.Windows.Forms.TextBox();
            this.cmbLoadCase = new System.Windows.Forms.ComboBox();
            this.btn_ZoomIn = new System.Windows.Forms.Button();
            this.btn_ZoomOut = new System.Windows.Forms.Button();
            this.btn_Pan = new System.Windows.Forms.Button();
            this.btn_member_force = new System.Windows.Forms.Button();
            this.tmrLoadDeflection = new System.Windows.Forms.Timer(this.components);
            this.btnAutoNext = new System.Windows.Forms.Button();
            this.btnAutoPrev = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.grb_Auto = new System.Windows.Forms.GroupBox();
            this.cmbInterval = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pbLoadDeflection = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.lblPleaseWait = new System.Windows.Forms.Label();
            this.lblFactor = new System.Windows.Forms.Label();
            this.txtDefFactor = new System.Windows.Forms.TextBox();
            this.btn_top_view = new System.Windows.Forms.Button();
            this.btn_side_view = new System.Windows.Forms.Button();
            this.grb_View = new System.Windows.Forms.GroupBox();
            this.txt_text_size = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_force_loadcase = new System.Windows.Forms.Button();
            this.grb_moving_controls = new System.Windows.Forms.GroupBox();
            this.grb_manual.SuspendLayout();
            this.grb_Auto.SuspendLayout();
            this.grb_View.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grb_moving_controls.SuspendLayout();
            this.SuspendLayout();
            // 
            // grb_manual
            // 
            this.grb_manual.Controls.Add(this.lbl_distance);
            this.grb_manual.Controls.Add(this.txt_dist);
            this.grb_manual.Controls.Add(this.cmbLoadCase);
            this.grb_manual.Location = new System.Drawing.Point(234, 15);
            this.grb_manual.Name = "grb_manual";
            this.grb_manual.Size = new System.Drawing.Size(84, 64);
            this.grb_manual.TabIndex = 6;
            this.grb_manual.TabStop = false;
            this.grb_manual.Tag = "";
            this.grb_manual.Text = "Load Case";
            // 
            // lbl_distance
            // 
            this.lbl_distance.AutoSize = true;
            this.lbl_distance.Location = new System.Drawing.Point(65, 45);
            this.lbl_distance.Name = "lbl_distance";
            this.lbl_distance.Size = new System.Drawing.Size(15, 13);
            this.lbl_distance.TabIndex = 9;
            this.lbl_distance.Text = "m";
            // 
            // txt_dist
            // 
            this.txt_dist.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_dist.Location = new System.Drawing.Point(3, 40);
            this.txt_dist.Name = "txt_dist";
            this.txt_dist.Size = new System.Drawing.Size(56, 21);
            this.txt_dist.TabIndex = 27;
            this.txt_dist.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmbLoadCase
            // 
            this.cmbLoadCase.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbLoadCase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLoadCase.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLoadCase.FormattingEnabled = true;
            this.cmbLoadCase.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.cmbLoadCase.Location = new System.Drawing.Point(3, 16);
            this.cmbLoadCase.Name = "cmbLoadCase";
            this.cmbLoadCase.Size = new System.Drawing.Size(78, 22);
            this.cmbLoadCase.TabIndex = 8;
            this.cmbLoadCase.SelectedIndexChanged += new System.EventHandler(this.cmbLoadCase_SelectedIndexChanged);
            // 
            // btn_ZoomIn
            // 
            this.btn_ZoomIn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_ZoomIn.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ZoomIn.Location = new System.Drawing.Point(6, 40);
            this.btn_ZoomIn.Name = "btn_ZoomIn";
            this.btn_ZoomIn.Size = new System.Drawing.Size(68, 23);
            this.btn_ZoomIn.TabIndex = 17;
            this.btn_ZoomIn.Text = "Zoom +";
            this.btn_ZoomIn.UseVisualStyleBackColor = true;
            this.btn_ZoomIn.Click += new System.EventHandler(this.btn_ZoomIn_Click);
            // 
            // btn_ZoomOut
            // 
            this.btn_ZoomOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_ZoomOut.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ZoomOut.Location = new System.Drawing.Point(76, 40);
            this.btn_ZoomOut.Name = "btn_ZoomOut";
            this.btn_ZoomOut.Size = new System.Drawing.Size(68, 23);
            this.btn_ZoomOut.TabIndex = 18;
            this.btn_ZoomOut.Text = "Zoom -";
            this.btn_ZoomOut.UseVisualStyleBackColor = true;
            this.btn_ZoomOut.Click += new System.EventHandler(this.btn_ZoomOut_Click);
            // 
            // btn_Pan
            // 
            this.btn_Pan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Pan.Location = new System.Drawing.Point(147, 40);
            this.btn_Pan.Name = "btn_Pan";
            this.btn_Pan.Size = new System.Drawing.Size(63, 23);
            this.btn_Pan.TabIndex = 19;
            this.btn_Pan.Text = "Pan";
            this.btn_Pan.UseVisualStyleBackColor = true;
            this.btn_Pan.Click += new System.EventHandler(this.btn_Pan_Click_1);
            // 
            // btn_member_force
            // 
            this.btn_member_force.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_member_force.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_member_force.Location = new System.Drawing.Point(3, 16);
            this.btn_member_force.Name = "btn_member_force";
            this.btn_member_force.Size = new System.Drawing.Size(125, 23);
            this.btn_member_force.TabIndex = 20;
            this.btn_member_force.Text = "Member Force";
            this.btn_member_force.UseVisualStyleBackColor = true;
            this.btn_member_force.Click += new System.EventHandler(this.btn_member_force_Click);
            // 
            // tmrLoadDeflection
            // 
            this.tmrLoadDeflection.Tick += new System.EventHandler(this.tmrLoadDeflection_Tick);
            // 
            // btnAutoNext
            // 
            this.btnAutoNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAutoNext.Location = new System.Drawing.Point(225, 13);
            this.btnAutoNext.Name = "btnAutoNext";
            this.btnAutoNext.Size = new System.Drawing.Size(86, 23);
            this.btnAutoNext.TabIndex = 22;
            this.btnAutoNext.Text = "Run Forward";
            this.btnAutoNext.UseVisualStyleBackColor = true;
            this.btnAutoNext.Click += new System.EventHandler(this.btnNextAuto_Click);
            // 
            // btnAutoPrev
            // 
            this.btnAutoPrev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAutoPrev.Location = new System.Drawing.Point(62, 14);
            this.btnAutoPrev.Name = "btnAutoPrev";
            this.btnAutoPrev.Size = new System.Drawing.Size(86, 23);
            this.btnAutoPrev.TabIndex = 21;
            this.btnAutoPrev.Text = "Step Back";
            this.btnAutoPrev.UseVisualStyleBackColor = true;
            this.btnAutoPrev.Click += new System.EventHandler(this.btnAutoPrev_Click);
            // 
            // btnPause
            // 
            this.btnPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPause.Location = new System.Drawing.Point(6, 13);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(50, 23);
            this.btnPause.TabIndex = 15;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStop.Location = new System.Drawing.Point(317, 13);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(50, 23);
            this.btnStop.TabIndex = 20;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // grb_Auto
            // 
            this.grb_Auto.Controls.Add(this.cmbInterval);
            this.grb_Auto.Controls.Add(this.label1);
            this.grb_Auto.Controls.Add(this.btnStop);
            this.grb_Auto.Controls.Add(this.btnAutoNext);
            this.grb_Auto.Controls.Add(this.btnAutoPrev);
            this.grb_Auto.Controls.Add(this.btnPause);
            this.grb_Auto.Location = new System.Drawing.Point(324, 15);
            this.grb_Auto.Name = "grb_Auto";
            this.grb_Auto.Size = new System.Drawing.Size(375, 44);
            this.grb_Auto.TabIndex = 21;
            this.grb_Auto.TabStop = false;
            this.grb_Auto.Text = "Run Moving Load";
            // 
            // cmbInterval
            // 
            this.cmbInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInterval.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbInterval.FormattingEnabled = true;
            this.cmbInterval.Items.AddRange(new object[] {
            "0.1 s",
            "0.2 s",
            "0.3 s",
            "0.4 s",
            "0.5 s",
            "0.6 s",
            "0.7 s",
            "0.8 s",
            "0.9 s",
            "1 s",
            "2 s",
            "3 s",
            "4 s",
            "5 s",
            "6 Sec",
            "7 Sec",
            "8 Sec",
            "9 Sec",
            "10 Sec",
            "15 Sec",
            "20 Sec",
            "25 Sec",
            "30 Sec"});
            this.cmbInterval.Location = new System.Drawing.Point(154, 15);
            this.cmbInterval.Name = "cmbInterval";
            this.cmbInterval.Size = new System.Drawing.Size(65, 21);
            this.cmbInterval.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(151, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Time Interval";
            // 
            // pbLoadDeflection
            // 
            this.pbLoadDeflection.Location = new System.Drawing.Point(133, 108);
            this.pbLoadDeflection.Name = "pbLoadDeflection";
            this.pbLoadDeflection.Size = new System.Drawing.Size(703, 21);
            this.pbLoadDeflection.TabIndex = 22;
            this.pbLoadDeflection.VisibleChanged += new System.EventHandler(this.pbLoadDeflection_VisibleChanged);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // lblPleaseWait
            // 
            this.lblPleaseWait.AutoSize = true;
            this.lblPleaseWait.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPleaseWait.Location = new System.Drawing.Point(7, 108);
            this.lblPleaseWait.Name = "lblPleaseWait";
            this.lblPleaseWait.Size = new System.Drawing.Size(120, 16);
            this.lblPleaseWait.TabIndex = 23;
            this.lblPleaseWait.Text = "Please wait...";
            // 
            // lblFactor
            // 
            this.lblFactor.AutoSize = true;
            this.lblFactor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFactor.Location = new System.Drawing.Point(327, 65);
            this.lblFactor.Name = "lblFactor";
            this.lblFactor.Size = new System.Drawing.Size(105, 13);
            this.lblFactor.TabIndex = 25;
            this.lblFactor.Text = "Deflection Factor";
            // 
            // txtDefFactor
            // 
            this.txtDefFactor.Enabled = false;
            this.txtDefFactor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDefFactor.Location = new System.Drawing.Point(438, 62);
            this.txtDefFactor.Name = "txtDefFactor";
            this.txtDefFactor.Size = new System.Drawing.Size(65, 21);
            this.txtDefFactor.TabIndex = 26;
            this.txtDefFactor.Text = "1.0";
            this.txtDefFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btn_top_view
            // 
            this.btn_top_view.Location = new System.Drawing.Point(3, 16);
            this.btn_top_view.Name = "btn_top_view";
            this.btn_top_view.Size = new System.Drawing.Size(101, 23);
            this.btn_top_view.TabIndex = 27;
            this.btn_top_view.Text = "TOP";
            this.btn_top_view.UseVisualStyleBackColor = true;
            this.btn_top_view.Click += new System.EventHandler(this.btn_top_view_Click);
            // 
            // btn_side_view
            // 
            this.btn_side_view.Location = new System.Drawing.Point(110, 16);
            this.btn_side_view.Name = "btn_side_view";
            this.btn_side_view.Size = new System.Drawing.Size(100, 23);
            this.btn_side_view.TabIndex = 28;
            this.btn_side_view.Text = "SIDE";
            this.btn_side_view.UseVisualStyleBackColor = true;
            this.btn_side_view.Click += new System.EventHandler(this.btn_top_view_Click);
            // 
            // grb_View
            // 
            this.grb_View.Controls.Add(this.btn_side_view);
            this.grb_View.Controls.Add(this.btn_top_view);
            this.grb_View.Controls.Add(this.btn_ZoomIn);
            this.grb_View.Controls.Add(this.btn_ZoomOut);
            this.grb_View.Controls.Add(this.btn_Pan);
            this.grb_View.Location = new System.Drawing.Point(6, 15);
            this.grb_View.Name = "grb_View";
            this.grb_View.Size = new System.Drawing.Size(222, 68);
            this.grb_View.TabIndex = 9;
            this.grb_View.TabStop = false;
            this.grb_View.Tag = "";
            this.grb_View.Text = "View";
            // 
            // txt_text_size
            // 
            this.txt_text_size.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_text_size.Location = new System.Drawing.Point(624, 62);
            this.txt_text_size.Name = "txt_text_size";
            this.txt_text_size.Size = new System.Drawing.Size(67, 21);
            this.txt_text_size.TabIndex = 28;
            this.txt_text_size.Text = "0.2";
            this.txt_text_size.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_text_size.TextChanged += new System.EventHandler(this.txt_text_size_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(558, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Text Size";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_force_loadcase);
            this.groupBox1.Controls.Add(this.btn_member_force);
            this.groupBox1.Location = new System.Drawing.Point(705, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(131, 69);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "View Report";
            // 
            // btn_force_loadcase
            // 
            this.btn_force_loadcase.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_force_loadcase.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_force_loadcase.Location = new System.Drawing.Point(3, 39);
            this.btn_force_loadcase.Name = "btn_force_loadcase";
            this.btn_force_loadcase.Size = new System.Drawing.Size(125, 23);
            this.btn_force_loadcase.TabIndex = 21;
            this.btn_force_loadcase.Text = "Force at Load Case";
            this.btn_force_loadcase.UseVisualStyleBackColor = true;
            this.btn_force_loadcase.Click += new System.EventHandler(this.btn_force_loadcase_Click);
            // 
            // grb_moving_controls
            // 
            this.grb_moving_controls.Controls.Add(this.grb_View);
            this.grb_moving_controls.Controls.Add(this.groupBox1);
            this.grb_moving_controls.Controls.Add(this.grb_manual);
            this.grb_moving_controls.Controls.Add(this.txt_text_size);
            this.grb_moving_controls.Controls.Add(this.grb_Auto);
            this.grb_moving_controls.Controls.Add(this.label2);
            this.grb_moving_controls.Controls.Add(this.lblFactor);
            this.grb_moving_controls.Controls.Add(this.txtDefFactor);
            this.grb_moving_controls.Dock = System.Windows.Forms.DockStyle.Top;
            this.grb_moving_controls.Location = new System.Drawing.Point(0, 0);
            this.grb_moving_controls.Name = "grb_moving_controls";
            this.grb_moving_controls.Size = new System.Drawing.Size(842, 90);
            this.grb_moving_controls.TabIndex = 30;
            this.grb_moving_controls.TabStop = false;
            // 
            // frmASTRAMovingLoadDeflections
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 95);
            this.Controls.Add(this.grb_moving_controls);
            this.Controls.Add(this.pbLoadDeflection);
            this.Controls.Add(this.lblPleaseWait);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmASTRAMovingLoadDeflections";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Moving Load Analysis";
            this.Load += new System.EventHandler(this.frmASTRALoadDeflections_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmASTRALoadDeflections_FormClosing);
            this.grb_manual.ResumeLayout(false);
            this.grb_manual.PerformLayout();
            this.grb_Auto.ResumeLayout(false);
            this.grb_Auto.PerformLayout();
            this.grb_View.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.grb_moving_controls.ResumeLayout(false);
            this.grb_moving_controls.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grb_manual;
        public System.Windows.Forms.ComboBox cmbLoadCase;
        private System.Windows.Forms.Timer tmrLoadDeflection;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btn_ZoomIn;
        private System.Windows.Forms.Button btn_ZoomOut;
        private System.Windows.Forms.Button btn_Pan;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btn_member_force;
        private System.Windows.Forms.Button btnAutoNext;
        private System.Windows.Forms.Button btnAutoPrev;
        private System.Windows.Forms.GroupBox grb_Auto;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox cmbInterval;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar pbLoadDeflection;
        private System.Windows.Forms.Label lblPleaseWait;
        private System.Windows.Forms.TextBox txtDefFactor;
        private System.Windows.Forms.Label lblFactor;
        private System.Windows.Forms.Button btn_top_view;
        private System.Windows.Forms.Button btn_side_view;
        private System.Windows.Forms.GroupBox grb_View;
        private System.Windows.Forms.TextBox txt_text_size;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_force_loadcase;
        private System.Windows.Forms.Label lbl_distance;
        private System.Windows.Forms.TextBox txt_dist;
        private System.Windows.Forms.GroupBox grb_moving_controls;
    }
}