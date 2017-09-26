namespace HEADSNeed.ASTRA.ASTRAForms
{
    partial class frm_ASTRA_Controls
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_geometry = new System.Windows.Forms.TabPage();
            this.tab_props = new System.Windows.Forms.TabPage();
            this.tab_specs = new System.Windows.Forms.TabPage();
            this.tab_supports = new System.Windows.Forms.TabPage();
            this.tab_loading = new System.Windows.Forms.TabPage();
            this.tab_max_force = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl1.Controls.Add(this.tab_geometry);
            this.tabControl1.Controls.Add(this.tab_props);
            this.tabControl1.Controls.Add(this.tab_specs);
            this.tabControl1.Controls.Add(this.tab_supports);
            this.tabControl1.Controls.Add(this.tab_loading);
            this.tabControl1.Controls.Add(this.tab_max_force);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(37, 479);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tab_geometry
            // 
            this.tab_geometry.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab_geometry.Location = new System.Drawing.Point(25, 4);
            this.tab_geometry.Name = "tab_geometry";
            this.tab_geometry.Padding = new System.Windows.Forms.Padding(3);
            this.tab_geometry.Size = new System.Drawing.Size(8, 471);
            this.tab_geometry.TabIndex = 0;
            this.tab_geometry.Text = "Geometry";
            this.tab_geometry.UseVisualStyleBackColor = true;
            // 
            // tab_props
            // 
            this.tab_props.Location = new System.Drawing.Point(25, 4);
            this.tab_props.Name = "tab_props";
            this.tab_props.Padding = new System.Windows.Forms.Padding(3);
            this.tab_props.Size = new System.Drawing.Size(8, 471);
            this.tab_props.TabIndex = 1;
            this.tab_props.Text = "Property";
            this.tab_props.UseVisualStyleBackColor = true;
            // 
            // tab_specs
            // 
            this.tab_specs.Location = new System.Drawing.Point(25, 4);
            this.tab_specs.Name = "tab_specs";
            this.tab_specs.Size = new System.Drawing.Size(8, 471);
            this.tab_specs.TabIndex = 2;
            this.tab_specs.Text = "Specifications";
            this.tab_specs.UseVisualStyleBackColor = true;
            // 
            // tab_supports
            // 
            this.tab_supports.Location = new System.Drawing.Point(25, 4);
            this.tab_supports.Name = "tab_supports";
            this.tab_supports.Size = new System.Drawing.Size(8, 471);
            this.tab_supports.TabIndex = 3;
            this.tab_supports.Text = "Supports";
            this.tab_supports.UseVisualStyleBackColor = true;
            // 
            // tab_loading
            // 
            this.tab_loading.Location = new System.Drawing.Point(25, 4);
            this.tab_loading.Name = "tab_loading";
            this.tab_loading.Size = new System.Drawing.Size(8, 471);
            this.tab_loading.TabIndex = 4;
            this.tab_loading.Text = "Loading";
            this.tab_loading.UseVisualStyleBackColor = true;
            // 
            // tab_max_force
            // 
            this.tab_max_force.Location = new System.Drawing.Point(25, 4);
            this.tab_max_force.Name = "tab_max_force";
            this.tab_max_force.Size = new System.Drawing.Size(8, 471);
            this.tab_max_force.TabIndex = 5;
            this.tab_max_force.Text = "Max Force";
            this.tab_max_force.UseVisualStyleBackColor = true;
            // 
            // frm_ASTRA_Controls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(37, 479);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frm_ASTRA_Controls";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_geometry;
        private System.Windows.Forms.TabPage tab_props;
        private System.Windows.Forms.TabPage tab_specs;
        private System.Windows.Forms.TabPage tab_supports;
        private System.Windows.Forms.TabPage tab_loading;
        private System.Windows.Forms.TabPage tab_max_force;
    }
}