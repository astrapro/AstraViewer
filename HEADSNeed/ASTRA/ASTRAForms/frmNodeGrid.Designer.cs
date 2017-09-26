namespace HEADSNeed.ASTRA.ASTRAForms
{
    partial class frmNodeGrid
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvNodeGrid = new System.Windows.Forms.DataGridView();
            this.Node = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgTxtX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgTxtY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgTxtZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgTxtSupport = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssl_zoom = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNodeGrid)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvNodeGrid
            // 
            this.dgvNodeGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNodeGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvNodeGrid.ColumnHeadersHeight = 21;
            this.dgvNodeGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Node,
            this.dgTxtX,
            this.dgTxtY,
            this.dgTxtZ,
            this.dgTxtSupport});
            this.dgvNodeGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNodeGrid.Location = new System.Drawing.Point(0, 0);
            this.dgvNodeGrid.Name = "dgvNodeGrid";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.Format = "N3";
            dataGridViewCellStyle11.NullValue = null;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNodeGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvNodeGrid.RowHeadersWidth = 20;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgvNodeGrid.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvNodeGrid.RowTemplate.Height = 14;
            this.dgvNodeGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNodeGrid.Size = new System.Drawing.Size(298, 237);
            this.dgvNodeGrid.TabIndex = 0;
            this.dgvNodeGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNodeGrid_CellEnter);
            // 
            // Node
            // 
            this.Node.HeaderText = "Node#";
            this.Node.Name = "Node";
            this.Node.ReadOnly = true;
            this.Node.Width = 50;
            // 
            // dgTxtX
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.Format = "N3";
            dataGridViewCellStyle8.NullValue = null;
            this.dgTxtX.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgTxtX.HeaderText = "X";
            this.dgTxtX.Name = "dgTxtX";
            this.dgTxtX.ReadOnly = true;
            this.dgTxtX.Width = 50;
            // 
            // dgTxtY
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.Format = "N3";
            dataGridViewCellStyle9.NullValue = null;
            this.dgTxtY.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgTxtY.HeaderText = "Y";
            this.dgTxtY.Name = "dgTxtY";
            this.dgTxtY.ReadOnly = true;
            this.dgTxtY.Width = 50;
            // 
            // dgTxtZ
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.Format = "N3";
            dataGridViewCellStyle10.NullValue = null;
            this.dgTxtZ.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgTxtZ.HeaderText = "Z";
            this.dgTxtZ.Name = "dgTxtZ";
            this.dgTxtZ.ReadOnly = true;
            this.dgTxtZ.Width = 50;
            // 
            // dgTxtSupport
            // 
            this.dgTxtSupport.HeaderText = "Support";
            this.dgTxtSupport.Name = "dgTxtSupport";
            this.dgTxtSupport.ReadOnly = true;
            this.dgTxtSupport.Width = 75;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_zoom});
            this.statusStrip1.Location = new System.Drawing.Point(0, 237);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(298, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssl_zoom
            // 
            this.tssl_zoom.Name = "tssl_zoom";
            this.tssl_zoom.Size = new System.Drawing.Size(59, 17);
            this.tssl_zoom.Text = "Zoom Off";
            this.tssl_zoom.Click += new System.EventHandler(this.tsbtn_zoom_Click);
            // 
            // frmNodeGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 259);
            this.Controls.Add(this.dgvNodeGrid);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmNodeGrid";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Joint Nodes Data";
            this.Load += new System.EventHandler(this.frmNodeGrid_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNodeGrid)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvNodeGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Node;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgTxtX;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgTxtY;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgTxtZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgTxtSupport;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssl_zoom;

    }
}