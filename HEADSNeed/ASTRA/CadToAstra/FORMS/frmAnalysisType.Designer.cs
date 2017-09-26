namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    partial class frmAnalysisType
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
            this.rbtn_static = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grb_dyna = new System.Windows.Forms.GroupBox();
            this.txt_frequencies = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rbtn_response_spectrum = new System.Windows.Forms.RadioButton();
            this.grb_time = new System.Windows.Forms.GroupBox();
            this.rbtn_without_nodal_cons = new System.Windows.Forms.RadioButton();
            this.rbtn_with_nodal_cons = new System.Windows.Forms.RadioButton();
            this.rbtn_perform_time_history = new System.Windows.Forms.RadioButton();
            this.rbtn_perform_eigen = new System.Windows.Forms.RadioButton();
            this.rbtn_dynamic = new System.Windows.Forms.RadioButton();
            this.btn_add_data = new System.Windows.Forms.Button();
            this.q = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.grb_dyna.SuspendLayout();
            this.grb_time.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbtn_static
            // 
            this.rbtn_static.AutoSize = true;
            this.rbtn_static.Checked = true;
            this.rbtn_static.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_static.Location = new System.Drawing.Point(6, 13);
            this.rbtn_static.Name = "rbtn_static";
            this.rbtn_static.Size = new System.Drawing.Size(130, 18);
            this.rbtn_static.TabIndex = 2;
            this.rbtn_static.TabStop = true;
            this.rbtn_static.Text = "STATIC ANALYSIS";
            this.rbtn_static.UseVisualStyleBackColor = true;
            this.rbtn_static.CheckedChanged += new System.EventHandler(this.rbtn_static_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grb_dyna);
            this.groupBox1.Controls.Add(this.rbtn_dynamic);
            this.groupBox1.Controls.Add(this.rbtn_static);
            this.groupBox1.Location = new System.Drawing.Point(7, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(365, 259);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // grb_dyna
            // 
            this.grb_dyna.Controls.Add(this.txt_frequencies);
            this.grb_dyna.Controls.Add(this.label1);
            this.grb_dyna.Controls.Add(this.rbtn_response_spectrum);
            this.grb_dyna.Controls.Add(this.grb_time);
            this.grb_dyna.Controls.Add(this.rbtn_perform_time_history);
            this.grb_dyna.Controls.Add(this.rbtn_perform_eigen);
            this.grb_dyna.Enabled = false;
            this.grb_dyna.Location = new System.Drawing.Point(24, 59);
            this.grb_dyna.Name = "grb_dyna";
            this.grb_dyna.Size = new System.Drawing.Size(318, 193);
            this.grb_dyna.TabIndex = 4;
            this.grb_dyna.TabStop = false;
            this.grb_dyna.Text = "Dynamic Analysis";
            // 
            // txt_frequencies
            // 
            this.txt_frequencies.Location = new System.Drawing.Point(215, 37);
            this.txt_frequencies.Name = "txt_frequencies";
            this.txt_frequencies.Size = new System.Drawing.Size(74, 21);
            this.txt_frequencies.TabIndex = 5;
            this.txt_frequencies.Text = "3";
            this.txt_frequencies.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(111, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "FREQUENCIES";
            // 
            // rbtn_response_spectrum
            // 
            this.rbtn_response_spectrum.AutoSize = true;
            this.rbtn_response_spectrum.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_response_spectrum.Location = new System.Drawing.Point(24, 160);
            this.rbtn_response_spectrum.Name = "rbtn_response_spectrum";
            this.rbtn_response_spectrum.Size = new System.Drawing.Size(226, 18);
            this.rbtn_response_spectrum.TabIndex = 3;
            this.rbtn_response_spectrum.TabStop = true;
            this.rbtn_response_spectrum.Text = "RESPONSE SPECTRUM ANALYSIS";
            this.rbtn_response_spectrum.UseVisualStyleBackColor = true;
            this.rbtn_response_spectrum.CheckedChanged += new System.EventHandler(this.rbtn_static_CheckedChanged);
            // 
            // grb_time
            // 
            this.grb_time.Controls.Add(this.rbtn_without_nodal_cons);
            this.grb_time.Controls.Add(this.rbtn_with_nodal_cons);
            this.grb_time.Location = new System.Drawing.Point(44, 87);
            this.grb_time.Name = "grb_time";
            this.grb_time.Size = new System.Drawing.Size(260, 67);
            this.grb_time.TabIndex = 2;
            this.grb_time.TabStop = false;
            this.grb_time.Text = "Perform Time History Analysis";
            // 
            // rbtn_without_nodal_cons
            // 
            this.rbtn_without_nodal_cons.AutoSize = true;
            this.rbtn_without_nodal_cons.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_without_nodal_cons.Location = new System.Drawing.Point(7, 42);
            this.rbtn_without_nodal_cons.Name = "rbtn_without_nodal_cons";
            this.rbtn_without_nodal_cons.Size = new System.Drawing.Size(215, 18);
            this.rbtn_without_nodal_cons.TabIndex = 1;
            this.rbtn_without_nodal_cons.TabStop = true;
            this.rbtn_without_nodal_cons.Text = "Without Nodal Constraint DOF";
            this.rbtn_without_nodal_cons.UseVisualStyleBackColor = true;
            this.rbtn_without_nodal_cons.CheckedChanged += new System.EventHandler(this.rbtn_static_CheckedChanged);
            // 
            // rbtn_with_nodal_cons
            // 
            this.rbtn_with_nodal_cons.AutoSize = true;
            this.rbtn_with_nodal_cons.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_with_nodal_cons.Location = new System.Drawing.Point(7, 19);
            this.rbtn_with_nodal_cons.Name = "rbtn_with_nodal_cons";
            this.rbtn_with_nodal_cons.Size = new System.Drawing.Size(194, 18);
            this.rbtn_with_nodal_cons.TabIndex = 0;
            this.rbtn_with_nodal_cons.TabStop = true;
            this.rbtn_with_nodal_cons.Text = "With Nodal Constraint DOF";
            this.rbtn_with_nodal_cons.UseVisualStyleBackColor = true;
            this.rbtn_with_nodal_cons.CheckedChanged += new System.EventHandler(this.rbtn_static_CheckedChanged);
            // 
            // rbtn_perform_time_history
            // 
            this.rbtn_perform_time_history.AutoSize = true;
            this.rbtn_perform_time_history.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_perform_time_history.Location = new System.Drawing.Point(24, 68);
            this.rbtn_perform_time_history.Name = "rbtn_perform_time_history";
            this.rbtn_perform_time_history.Size = new System.Drawing.Size(239, 18);
            this.rbtn_perform_time_history.TabIndex = 1;
            this.rbtn_perform_time_history.TabStop = true;
            this.rbtn_perform_time_history.Text = "PERFORM TIME HISTORY ANALYSIS";
            this.rbtn_perform_time_history.UseVisualStyleBackColor = true;
            this.rbtn_perform_time_history.CheckedChanged += new System.EventHandler(this.rbtn_static_CheckedChanged);
            // 
            // rbtn_perform_eigen
            // 
            this.rbtn_perform_eigen.AutoSize = true;
            this.rbtn_perform_eigen.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_perform_eigen.Location = new System.Drawing.Point(24, 19);
            this.rbtn_perform_eigen.Name = "rbtn_perform_eigen";
            this.rbtn_perform_eigen.Size = new System.Drawing.Size(234, 18);
            this.rbtn_perform_eigen.TabIndex = 0;
            this.rbtn_perform_eigen.TabStop = true;
            this.rbtn_perform_eigen.Text = "PERFORM EIGEN VALUE ANALYSIS";
            this.rbtn_perform_eigen.UseVisualStyleBackColor = true;
            this.rbtn_perform_eigen.CheckedChanged += new System.EventHandler(this.rbtn_static_CheckedChanged);
            // 
            // rbtn_dynamic
            // 
            this.rbtn_dynamic.AutoSize = true;
            this.rbtn_dynamic.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_dynamic.Location = new System.Drawing.Point(6, 38);
            this.rbtn_dynamic.Name = "rbtn_dynamic";
            this.rbtn_dynamic.Size = new System.Drawing.Size(145, 18);
            this.rbtn_dynamic.TabIndex = 3;
            this.rbtn_dynamic.TabStop = true;
            this.rbtn_dynamic.Text = "DYNAMIC ANALYSIS";
            this.rbtn_dynamic.UseVisualStyleBackColor = true;
            this.rbtn_dynamic.CheckedChanged += new System.EventHandler(this.rbtn_static_CheckedChanged);
            // 
            // btn_add_data
            // 
            this.btn_add_data.Location = new System.Drawing.Point(96, 270);
            this.btn_add_data.Name = "btn_add_data";
            this.btn_add_data.Size = new System.Drawing.Size(87, 23);
            this.btn_add_data.TabIndex = 5;
            this.btn_add_data.Text = "Add Data";
            this.btn_add_data.UseVisualStyleBackColor = true;
            this.btn_add_data.Click += new System.EventHandler(this.btn_add_data_Click);
            // 
            // q
            // 
            this.q.Location = new System.Drawing.Point(217, 270);
            this.q.Name = "q";
            this.q.Size = new System.Drawing.Size(87, 23);
            this.q.TabIndex = 6;
            this.q.Text = "FINISH";
            this.q.UseVisualStyleBackColor = true;
            this.q.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmAnalysisType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 297);
            this.Controls.Add(this.q);
            this.Controls.Add(this.btn_add_data);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAnalysisType";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Analysis Type";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grb_dyna.ResumeLayout(false);
            this.grb_dyna.PerformLayout();
            this.grb_time.ResumeLayout(false);
            this.grb_time.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rbtn_static;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtn_dynamic;
        private System.Windows.Forms.GroupBox grb_dyna;
        private System.Windows.Forms.RadioButton rbtn_perform_eigen;
        private System.Windows.Forms.GroupBox grb_time;
        private System.Windows.Forms.RadioButton rbtn_without_nodal_cons;
        private System.Windows.Forms.RadioButton rbtn_with_nodal_cons;
        private System.Windows.Forms.RadioButton rbtn_perform_time_history;
        private System.Windows.Forms.RadioButton rbtn_response_spectrum;
        private System.Windows.Forms.Button btn_add_data;
        private System.Windows.Forms.Button q;
        private System.Windows.Forms.TextBox txt_frequencies;
        private System.Windows.Forms.Label label1;
    }
}