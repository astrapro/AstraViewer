namespace HEADSNeed.ASTRA.CadToAstra.FORMS
{
    partial class frmNodeGridBox
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvNodeGrid = new System.Windows.Forms.DataGridView();
            this.Node = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgTxtX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgTxtY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgTxtZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgTxtSupport = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnl_button = new System.Windows.Forms.Panel();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_add_data = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNodeGrid)).BeginInit();
            this.pnl_button.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvNodeGrid
            // 
            this.dgvNodeGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNodeGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvNodeGrid.ColumnHeadersHeight = 21;
            this.dgvNodeGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvNodeGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Node,
            this.dgTxtX,
            this.dgTxtY,
            this.dgTxtZ,
            this.dgTxtSupport});
            this.dgvNodeGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNodeGrid.Location = new System.Drawing.Point(0, 0);
            this.dgvNodeGrid.Name = "dgvNodeGrid";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.Format = "N3";
            dataGridViewCellStyle5.NullValue = null;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNodeGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvNodeGrid.RowHeadersWidth = 20;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgvNodeGrid.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvNodeGrid.RowTemplate.Height = 14;
            this.dgvNodeGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNodeGrid.Size = new System.Drawing.Size(308, 259);
            this.dgvNodeGrid.TabIndex = 2;
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
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "N3";
            dataGridViewCellStyle2.NullValue = null;
            this.dgTxtX.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgTxtX.HeaderText = "X";
            this.dgTxtX.Name = "dgTxtX";
            this.dgTxtX.ReadOnly = true;
            this.dgTxtX.Width = 50;
            // 
            // dgTxtY
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "N3";
            dataGridViewCellStyle3.NullValue = null;
            this.dgTxtY.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgTxtY.HeaderText = "Y";
            this.dgTxtY.Name = "dgTxtY";
            this.dgTxtY.ReadOnly = true;
            this.dgTxtY.Width = 50;
            // 
            // dgTxtZ
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Format = "N3";
            dataGridViewCellStyle4.NullValue = null;
            this.dgTxtZ.DefaultCellStyle = dataGridViewCellStyle4;
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
            // pnl_button
            // 
            this.pnl_button.Controls.Add(this.btn_cancel);
            this.pnl_button.Controls.Add(this.btn_add_data);
            this.pnl_button.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_button.Location = new System.Drawing.Point(0, 259);
            this.pnl_button.Name = "pnl_button";
            this.pnl_button.Size = new System.Drawing.Size(308, 31);
            this.pnl_button.TabIndex = 3;
            this.pnl_button.Visible = false;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_cancel.Location = new System.Drawing.Point(158, 2);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(87, 23);
            this.btn_cancel.TabIndex = 1;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_add_data
            // 
            this.btn_add_data.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_add_data.Location = new System.Drawing.Point(63, 2);
            this.btn_add_data.Name = "btn_add_data";
            this.btn_add_data.Size = new System.Drawing.Size(87, 23);
            this.btn_add_data.TabIndex = 0;
            this.btn_add_data.Text = "Add Data";
            this.btn_add_data.UseVisualStyleBackColor = true;
            this.btn_add_data.Click += new System.EventHandler(this.btn_add_data_Click);
            // 
            // frmNodeGridBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 290);
            this.Controls.Add(this.dgvNodeGrid);
            this.Controls.Add(this.pnl_button);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmNodeGridBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Node Grid Box";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmNodeGridBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNodeGrid)).EndInit();
            this.pnl_button.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvNodeGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Node;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgTxtX;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgTxtY;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgTxtZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgTxtSupport;
        private System.Windows.Forms.Panel pnl_button;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_add_data;
    }
}