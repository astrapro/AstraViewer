namespace HEADSNeed.ASTRA.ASTRAForms
{
    partial class frmASTRAReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmASTRAReport));
            this.rtbData = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsb_page_setup = new System.Windows.Forms.ToolStripButton();
            this.tss1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_print_prev = new System.Windows.Forms.ToolStripButton();
            this.tss2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_print = new System.Windows.Forms.ToolStripButton();
            this.tsmi_land_scape = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmb_step = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_prev = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_next = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.spc_1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lsv_steps = new System.Windows.Forms.ListView();
            this.dessteps = new System.Windows.Forms.ColumnHeader();
            this.trv_steps = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbl_line_col = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_last = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssb_find = new System.Windows.Forms.ToolStripSplitButton();
            this.tslbl_notok = new System.Windows.Forms.ToolStripStatusLabel();
            this.ppd = new System.Windows.Forms.PrintPreviewDialog();
            this.pd = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.psd = new System.Windows.Forms.PageSetupDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pageSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.spc_1.Panel1.SuspendLayout();
            this.spc_1.Panel2.SuspendLayout();
            this.spc_1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbData
            // 
            this.rtbData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbData.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbData.Location = new System.Drawing.Point(0, 0);
            this.rtbData.Name = "rtbData";
            this.rtbData.Size = new System.Drawing.Size(743, 460);
            this.rtbData.TabIndex = 1;
            this.rtbData.Text = "";
            this.rtbData.WordWrap = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_page_setup,
            this.tss1,
            this.tsb_print_prev,
            this.tss2,
            this.tsb_print,
            this.tsmi_land_scape,
            this.toolStripSeparator1,
            this.cmb_step,
            this.toolStripSeparator2,
            this.tsb_prev,
            this.toolStripSeparator4,
            this.tsb_next,
            this.toolStripSeparator3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(974, 25);
            this.toolStrip1.TabIndex = 2;
            // 
            // tsb_page_setup
            // 
            this.tsb_page_setup.Image = ((System.Drawing.Image)(resources.GetObject("tsb_page_setup.Image")));
            this.tsb_page_setup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_page_setup.Name = "tsb_page_setup";
            this.tsb_page_setup.Size = new System.Drawing.Size(86, 22);
            this.tsb_page_setup.Text = "Page &Setup";
            // 
            // tss1
            // 
            this.tss1.Name = "tss1";
            this.tss1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsb_print_prev
            // 
            this.tsb_print_prev.Image = ((System.Drawing.Image)(resources.GetObject("tsb_print_prev.Image")));
            this.tsb_print_prev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_print_prev.Name = "tsb_print_prev";
            this.tsb_print_prev.Size = new System.Drawing.Size(96, 22);
            this.tsb_print_prev.Text = "Print Pre&view";
            // 
            // tss2
            // 
            this.tss2.Name = "tss2";
            this.tss2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsb_print
            // 
            this.tsb_print.Image = ((System.Drawing.Image)(resources.GetObject("tsb_print.Image")));
            this.tsb_print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_print.Name = "tsb_print";
            this.tsb_print.Size = new System.Drawing.Size(52, 22);
            this.tsb_print.Text = "&Print";
            // 
            // tsmi_land_scape
            // 
            this.tsmi_land_scape.CheckOnClick = true;
            this.tsmi_land_scape.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_land_scape.Image")));
            this.tsmi_land_scape.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsmi_land_scape.Name = "tsmi_land_scape";
            this.tsmi_land_scape.Size = new System.Drawing.Size(83, 22);
            this.tsmi_land_scape.Text = "Landscape";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // cmb_step
            // 
            this.cmb_step.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_step.DropDownWidth = 500;
            this.cmb_step.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_step.ForeColor = System.Drawing.Color.Blue;
            this.cmb_step.Name = "cmb_step";
            this.cmb_step.Size = new System.Drawing.Size(599, 25);
            this.cmb_step.ToolTipText = "Click Here to go STEPS and TABLES quick references.";
            this.cmb_step.SelectedIndexChanged += new System.EventHandler(this.cmb_step_SelectedIndexChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsb_prev
            // 
            this.tsb_prev.Checked = true;
            this.tsb_prev.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.tsb_prev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_prev.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_prev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_prev.Name = "tsb_prev";
            this.tsb_prev.Size = new System.Drawing.Size(23, 4);
            this.tsb_prev.Text = "Previos Selected Text";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsb_next
            // 
            this.tsb_next.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_next.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_next.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_next.Name = "tsb_next";
            this.tsb_next.Size = new System.Drawing.Size(23, 4);
            this.tsb_next.Text = "Next Selected Text";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // spc_1
            // 
            this.spc_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spc_1.Location = new System.Drawing.Point(0, 49);
            this.spc_1.Name = "spc_1";
            // 
            // spc_1.Panel1
            // 
            this.spc_1.Panel1.Controls.Add(this.splitContainer1);
            this.spc_1.Panel1.Controls.Add(this.label1);
            // 
            // spc_1.Panel2
            // 
            this.spc_1.Panel2.Controls.Add(this.rtbData);
            this.spc_1.Size = new System.Drawing.Size(974, 460);
            this.spc_1.SplitterDistance = 227;
            this.spc_1.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 23);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lsv_steps);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.trv_steps);
            this.splitContainer1.Size = new System.Drawing.Size(227, 437);
            this.splitContainer1.SplitterDistance = 187;
            this.splitContainer1.TabIndex = 5;
            // 
            // lsv_steps
            // 
            this.lsv_steps.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lsv_steps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.dessteps});
            this.lsv_steps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsv_steps.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsv_steps.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsv_steps.Location = new System.Drawing.Point(0, 0);
            this.lsv_steps.Name = "lsv_steps";
            this.lsv_steps.ShowItemToolTips = true;
            this.lsv_steps.Size = new System.Drawing.Size(225, 185);
            this.lsv_steps.TabIndex = 4;
            this.lsv_steps.UseCompatibleStateImageBehavior = false;
            this.lsv_steps.View = System.Windows.Forms.View.Details;
            this.lsv_steps.SelectedIndexChanged += new System.EventHandler(this.lstb_steps_SelectedIndexChanged);
            // 
            // dessteps
            // 
            this.dessteps.Text = "DESIGN STEPS";
            this.dessteps.Width = 702;
            // 
            // trv_steps
            // 
            this.trv_steps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trv_steps.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trv_steps.Location = new System.Drawing.Point(0, 0);
            this.trv_steps.Name = "trv_steps";
            this.trv_steps.Size = new System.Drawing.Size(225, 244);
            this.trv_steps.TabIndex = 0;
            this.trv_steps.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trv_steps_AfterSelect);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(227, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "DESIGN STEPS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbl_line_col,
            this.tssl_last,
            this.toolStripStatusLabel1,
            this.tssb_find,
            this.tslbl_notok});
            this.statusStrip1.Location = new System.Drawing.Point(0, 509);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(974, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lbl_line_col
            // 
            this.lbl_line_col.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_line_col.Name = "lbl_line_col";
            this.lbl_line_col.Size = new System.Drawing.Size(101, 17);
            this.lbl_line_col.Text = "Line : 1 , Col : 1";
            this.lbl_line_col.ToolTipText = "Click Here to go this selected lines.";
            // 
            // tssl_last
            // 
            this.tssl_last.BackColor = System.Drawing.Color.Yellow;
            this.tssl_last.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tssl_last.ForeColor = System.Drawing.Color.Blue;
            this.tssl_last.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tssl_last.Name = "tssl_last";
            this.tssl_last.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(82, 17);
            this.toolStripStatusLabel1.Text = "                         ";
            // 
            // tssb_find
            // 
            this.tssb_find.Image = ((System.Drawing.Image)(resources.GetObject("tssb_find.Image")));
            this.tssb_find.ImageTransparentColor = System.Drawing.Color.Black;
            this.tssb_find.Name = "tssb_find";
            this.tssb_find.Size = new System.Drawing.Size(87, 20);
            this.tssb_find.Text = "Find Text";
            // 
            // tslbl_notok
            // 
            this.tslbl_notok.ForeColor = System.Drawing.Color.Red;
            this.tslbl_notok.Name = "tslbl_notok";
            this.tslbl_notok.Size = new System.Drawing.Size(197, 17);
            this.tslbl_notok.Text = "            \"NOT OK\" Statement Found! ";
            this.tslbl_notok.Visible = false;
            // 
            // ppd
            // 
            this.ppd.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.ppd.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.ppd.ClientSize = new System.Drawing.Size(400, 300);
            this.ppd.Document = this.pd;
            this.ppd.Enabled = true;
            this.ppd.Icon = ((System.Drawing.Icon)(resources.GetObject("ppd.Icon")));
            this.ppd.Name = "ppd";
            this.ppd.Visible = false;
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(974, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAsToolStripMenuItem,
            this.pageSetupToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.printToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // pageSetupToolStripMenuItem
            // 
            this.pageSetupToolStripMenuItem.Name = "pageSetupToolStripMenuItem";
            this.pageSetupToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.pageSetupToolStripMenuItem.Text = "Page Setup";
            // 
            // printPreviewToolStripMenuItem
            // 
            this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.printPreviewToolStripMenuItem.Text = "Print Preview";
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.printToolStripMenuItem.Text = "Print";
            // 
            // frmASTRAReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 531);
            this.Controls.Add(this.spc_1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmASTRAReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ASTRA Analysis Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmASTRAReport_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.spc_1.Panel1.ResumeLayout(false);
            this.spc_1.Panel2.ResumeLayout(false);
            this.spc_1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbData;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsb_page_setup;
        private System.Windows.Forms.ToolStripSeparator tss1;
        private System.Windows.Forms.ToolStripButton tsb_print_prev;
        private System.Windows.Forms.ToolStripSeparator tss2;
        private System.Windows.Forms.ToolStripButton tsb_print;
        private System.Windows.Forms.ToolStripButton tsmi_land_scape;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox cmb_step;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsb_prev;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsb_next;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.SplitContainer spc_1;
        private System.Windows.Forms.ListView lsv_steps;
        private System.Windows.Forms.ColumnHeader dessteps;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lbl_line_col;
        private System.Windows.Forms.ToolStripStatusLabel tssl_last;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripSplitButton tssb_find;
        private System.Windows.Forms.ToolStripStatusLabel tslbl_notok;
        private System.Windows.Forms.PrintPreviewDialog ppd;
        private System.Drawing.Printing.PrintDocument pd;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.PageSetupDialog psd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView trv_steps;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pageSetupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;

    }
}