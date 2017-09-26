namespace HEADSNeed.ASTRA.ASTRAForms
{
    partial class frmASTRALoadDeflections
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
            this.lblLoadCase = new System.Windows.Forms.Label();
            this.cmbLoadCase = new System.Windows.Forms.ComboBox();
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
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.grb_manual.SuspendLayout();
            this.grb_Auto.SuspendLayout();
            this.SuspendLayout();
            // 
            // grb_manual
            // 
            this.grb_manual.Controls.Add(this.lblLoadCase);
            this.grb_manual.Controls.Add(this.cmbLoadCase);
            this.grb_manual.Location = new System.Drawing.Point(2, 1);
            this.grb_manual.Name = "grb_manual";
            this.grb_manual.Size = new System.Drawing.Size(156, 51);
            this.grb_manual.TabIndex = 6;
            this.grb_manual.TabStop = false;
            this.grb_manual.Text = "Manual";
            // 
            // lblLoadCase
            // 
            this.lblLoadCase.AutoSize = true;
            this.lblLoadCase.Location = new System.Drawing.Point(6, 27);
            this.lblLoadCase.Name = "lblLoadCase";
            this.lblLoadCase.Size = new System.Drawing.Size(58, 13);
            this.lblLoadCase.TabIndex = 23;
            this.lblLoadCase.Text = "Load Case";
            // 
            // cmbLoadCase
            // 
            this.cmbLoadCase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLoadCase.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLoadCase.FormattingEnabled = true;
            this.cmbLoadCase.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.cmbLoadCase.Location = new System.Drawing.Point(81, 23);
            this.cmbLoadCase.Name = "cmbLoadCase";
            this.cmbLoadCase.Size = new System.Drawing.Size(69, 22);
            this.cmbLoadCase.TabIndex = 8;
            this.cmbLoadCase.SelectedIndexChanged += new System.EventHandler(this.cmbLoadCase_SelectedIndexChanged);
            // 
            // tmrLoadDeflection
            // 
            this.tmrLoadDeflection.Tick += new System.EventHandler(this.tmrLoadDeflection_Tick);
            // 
            // btnAutoNext
            // 
            this.btnAutoNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAutoNext.Location = new System.Drawing.Point(231, 19);
            this.btnAutoNext.Name = "btnAutoNext";
            this.btnAutoNext.Size = new System.Drawing.Size(77, 28);
            this.btnAutoNext.TabIndex = 22;
            this.btnAutoNext.Text = "Run Forward";
            this.btnAutoNext.UseVisualStyleBackColor = true;
            this.btnAutoNext.Click += new System.EventHandler(this.btnNextAuto_Click);
            // 
            // btnAutoPrev
            // 
            this.btnAutoPrev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAutoPrev.Location = new System.Drawing.Point(57, 19);
            this.btnAutoPrev.Name = "btnAutoPrev";
            this.btnAutoPrev.Size = new System.Drawing.Size(89, 28);
            this.btnAutoPrev.TabIndex = 21;
            this.btnAutoPrev.Text = "Run Backward";
            this.btnAutoPrev.UseVisualStyleBackColor = true;
            this.btnAutoPrev.Click += new System.EventHandler(this.btnAutoPrev_Click);
            // 
            // btnPause
            // 
            this.btnPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPause.Location = new System.Drawing.Point(6, 19);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(45, 28);
            this.btnPause.TabIndex = 15;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStop.Location = new System.Drawing.Point(314, 19);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(45, 28);
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
            this.grb_Auto.Controls.Add(this.btnPause);
            this.grb_Auto.Controls.Add(this.btnAutoNext);
            this.grb_Auto.Controls.Add(this.btnAutoPrev);
            this.grb_Auto.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_Auto.Location = new System.Drawing.Point(158, 1);
            this.grb_Auto.Name = "grb_Auto";
            this.grb_Auto.Size = new System.Drawing.Size(372, 51);
            this.grb_Auto.TabIndex = 21;
            this.grb_Auto.TabStop = false;
            this.grb_Auto.Text = "Auto";
            // 
            // cmbInterval
            // 
            this.cmbInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInterval.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbInterval.FormattingEnabled = true;
            this.cmbInterval.Items.AddRange(new object[] {
            "0.1 Sec",
            "0.2 Sec",
            "0.3 Sec",
            "0.4 Sec",
            "0.5 Sec",
            "0.6 Sec",
            "0.7 Sec",
            "0.8 Sec",
            "0.9 Sec",
            "1 Sec",
            "2 Sec",
            "3 Sec",
            "4 Sec",
            "5 Sec",
            "6 Sec",
            "7 Sec",
            "8 Sec",
            "9 Sec",
            "10 Sec",
            "15 Sec",
            "20 Sec",
            "25 Sec",
            "30 Sec"});
            this.cmbInterval.Location = new System.Drawing.Point(152, 24);
            this.cmbInterval.Name = "cmbInterval";
            this.cmbInterval.Size = new System.Drawing.Size(73, 21);
            this.cmbInterval.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(164, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Interval";
            // 
            // pbLoadDeflection
            // 
            this.pbLoadDeflection.Location = new System.Drawing.Point(143, 13);
            this.pbLoadDeflection.Name = "pbLoadDeflection";
            this.pbLoadDeflection.Size = new System.Drawing.Size(388, 26);
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
            this.lblPleaseWait.Location = new System.Drawing.Point(17, 19);
            this.lblPleaseWait.Name = "lblPleaseWait";
            this.lblPleaseWait.Size = new System.Drawing.Size(120, 16);
            this.lblPleaseWait.TabIndex = 23;
            this.lblPleaseWait.Text = "Please wait...";
            // 
            // lblFactor
            // 
            this.lblFactor.AutoSize = true;
            this.lblFactor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFactor.Location = new System.Drawing.Point(140, 75);
            this.lblFactor.Name = "lblFactor";
            this.lblFactor.Size = new System.Drawing.Size(43, 13);
            this.lblFactor.TabIndex = 25;
            this.lblFactor.Text = "Factor";
            this.lblFactor.Visible = false;
            // 
            // txtDefFactor
            // 
            this.txtDefFactor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDefFactor.Location = new System.Drawing.Point(143, 91);
            this.txtDefFactor.Name = "txtDefFactor";
            this.txtDefFactor.Size = new System.Drawing.Size(56, 21);
            this.txtDefFactor.TabIndex = 26;
            this.txtDefFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDefFactor.Visible = false;
            // 
            // btnFirst
            // 
            this.btnFirst.BackgroundImage = global::HEADSNeed.Properties.Resources.first;
            this.btnFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFirst.Location = new System.Drawing.Point(215, 86);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(30, 28);
            this.btnFirst.TabIndex = 17;
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.BackgroundImage = global::HEADSNeed.Properties.Resources.prev;
            this.btnPrev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrev.Location = new System.Drawing.Point(251, 86);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(30, 28);
            this.btnPrev.TabIndex = 20;
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackgroundImage = global::HEADSNeed.Properties.Resources.next;
            this.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNext.Location = new System.Drawing.Point(287, 86);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(30, 28);
            this.btnNext.TabIndex = 19;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click_1);
            // 
            // btnLast
            // 
            this.btnLast.BackgroundImage = global::HEADSNeed.Properties.Resources.last;
            this.btnLast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLast.Location = new System.Drawing.Point(323, 86);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(30, 28);
            this.btnLast.TabIndex = 18;
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // frmASTRALoadDeflections
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 170);
            this.Controls.Add(this.txtDefFactor);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.lblFactor);
            this.Controls.Add(this.grb_Auto);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.grb_manual);
            this.Controls.Add(this.pbLoadDeflection);
            this.Controls.Add(this.lblPleaseWait);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmASTRALoadDeflections";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EIGEN VALUE ANALYSIS";
            this.Load += new System.EventHandler(this.frmASTRALoadDeflections_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmASTRALoadDeflections_FormClosing);
            this.grb_manual.ResumeLayout(false);
            this.grb_manual.PerformLayout();
            this.grb_Auto.ResumeLayout(false);
            this.grb_Auto.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grb_manual;
        public System.Windows.Forms.ComboBox cmbLoadCase;
        private System.Windows.Forms.Timer tmrLoadDeflection;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnAutoNext;
        private System.Windows.Forms.Button btnAutoPrev;
        private System.Windows.Forms.GroupBox grb_Auto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLoadCase;
        public System.Windows.Forms.ComboBox cmbInterval;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar pbLoadDeflection;
        private System.Windows.Forms.Label lblPleaseWait;
        private System.Windows.Forms.TextBox txtDefFactor;
        private System.Windows.Forms.Label lblFactor;
    }
}