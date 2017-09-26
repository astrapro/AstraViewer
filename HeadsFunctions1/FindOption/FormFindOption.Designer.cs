namespace HeadsFunctions1.FindOption
{
    partial class FormFindOption
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
            SaveData();
            m_app.ActiveDocument.Stop_Blink();
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chk_match_whole_word = new System.Windows.Forms.CheckBox();
            this.chk_match_case = new System.Windows.Forms.CheckBox();
            this.btn_Find_Next = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtn_Find = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtn_replace = new System.Windows.Forms.ToolStripButton();
            this.txt_search_text = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Replace_All = new System.Windows.Forms.Button();
            this.btn_Replace_Next = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txt_replace_text = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslbl_find_text = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_res = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(335, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "Find What :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chk_match_whole_word);
            this.groupBox1.Controls.Add(this.chk_match_case);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 119);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(335, 53);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Find options";
            // 
            // chk_match_whole_word
            // 
            this.chk_match_whole_word.AutoSize = true;
            this.chk_match_whole_word.Location = new System.Drawing.Point(144, 17);
            this.chk_match_whole_word.Name = "chk_match_whole_word";
            this.chk_match_whole_word.Size = new System.Drawing.Size(136, 16);
            this.chk_match_whole_word.TabIndex = 1;
            this.chk_match_whole_word.Text = "Match whole word";
            this.chk_match_whole_word.UseVisualStyleBackColor = true;
            // 
            // chk_match_case
            // 
            this.chk_match_case.AutoSize = true;
            this.chk_match_case.Location = new System.Drawing.Point(18, 17);
            this.chk_match_case.Name = "chk_match_case";
            this.chk_match_case.Size = new System.Drawing.Size(94, 16);
            this.chk_match_case.TabIndex = 0;
            this.chk_match_case.Text = "Match case";
            this.chk_match_case.UseVisualStyleBackColor = true;
            // 
            // btn_Find_Next
            // 
            this.btn_Find_Next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Find_Next.Location = new System.Drawing.Point(120, 178);
            this.btn_Find_Next.Name = "btn_Find_Next";
            this.btn_Find_Next.Size = new System.Drawing.Size(99, 27);
            this.btn_Find_Next.TabIndex = 5;
            this.btn_Find_Next.Text = "Find Next";
            this.btn_Find_Next.UseVisualStyleBackColor = true;
            this.btn_Find_Next.Click += new System.EventHandler(this.btn_Find_Next_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowDrop = true;
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Menu;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtn_Find,
            this.toolStripSeparator1,
            this.tsbtn_replace});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(339, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtn_Find
            // 
            this.tsbtn_Find.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_Find.Name = "tsbtn_Find";
            this.tsbtn_Find.Size = new System.Drawing.Size(34, 22);
            this.tsbtn_Find.Text = "Find";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtn_replace
            // 
            this.tsbtn_replace.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_replace.Name = "tsbtn_replace";
            this.tsbtn_replace.Size = new System.Drawing.Size(52, 22);
            this.tsbtn_replace.Text = "Replace";
            // 
            // txt_search_text
            // 
            this.txt_search_text.Dock = System.Windows.Forms.DockStyle.Top;
            this.txt_search_text.FormattingEnabled = true;
            this.txt_search_text.ItemHeight = 15;
            this.txt_search_text.Location = new System.Drawing.Point(0, 21);
            this.txt_search_text.Name = "txt_search_text";
            this.txt_search_text.Size = new System.Drawing.Size(335, 23);
            this.txt_search_text.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.lbl_res);
            this.panel1.Controls.Add(this.btn_Replace_All);
            this.panel1.Controls.Add(this.btn_Replace_Next);
            this.panel1.Controls.Add(this.btn_Find_Next);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.txt_replace_text);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Controls.Add(this.txt_search_text);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(339, 279);
            this.panel1.TabIndex = 7;
            // 
            // btn_Replace_All
            // 
            this.btn_Replace_All.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Replace_All.Location = new System.Drawing.Point(236, 211);
            this.btn_Replace_All.Name = "btn_Replace_All";
            this.btn_Replace_All.Size = new System.Drawing.Size(99, 27);
            this.btn_Replace_All.TabIndex = 5;
            this.btn_Replace_All.Text = "Replace All";
            this.btn_Replace_All.UseVisualStyleBackColor = true;
            this.btn_Replace_All.Click += new System.EventHandler(this.btn_Replace_All_Click);
            // 
            // btn_Replace_Next
            // 
            this.btn_Replace_Next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Replace_Next.Location = new System.Drawing.Point(236, 178);
            this.btn_Replace_Next.Name = "btn_Replace_Next";
            this.btn_Replace_Next.Size = new System.Drawing.Size(99, 27);
            this.btn_Replace_Next.TabIndex = 5;
            this.btn_Replace_Next.Text = "Replace";
            this.btn_Replace_Next.UseVisualStyleBackColor = true;
            this.btn_Replace_Next.Click += new System.EventHandler(this.btn_Replace_Next_Click);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 91);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(335, 28);
            this.panel2.TabIndex = 10;
            // 
            // txt_replace_text
            // 
            this.txt_replace_text.Dock = System.Windows.Forms.DockStyle.Top;
            this.txt_replace_text.FormattingEnabled = true;
            this.txt_replace_text.ItemHeight = 15;
            this.txt_replace_text.Location = new System.Drawing.Point(0, 68);
            this.txt_replace_text.Name = "txt_replace_text";
            this.txt_replace_text.Size = new System.Drawing.Size(335, 23);
            this.txt_replace_text.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(0, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(335, 24);
            this.label4.TabIndex = 8;
            this.label4.Text = "Replace With :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslbl_find_text});
            this.statusStrip1.Location = new System.Drawing.Point(0, 253);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(335, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslbl_find_text
            // 
            this.tsslbl_find_text.Name = "tsslbl_find_text";
            this.tsslbl_find_text.Size = new System.Drawing.Size(0, 17);
            // 
            // lbl_res
            // 
            this.lbl_res.AutoSize = true;
            this.lbl_res.Location = new System.Drawing.Point(3, 238);
            this.lbl_res.Name = "lbl_res";
            this.lbl_res.Size = new System.Drawing.Size(48, 15);
            this.lbl_res.TabIndex = 11;
            this.lbl_res.Text = "Results";
            // 
            // FormFindOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 304);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormFindOption";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Find and Replace";
            this.Load += new System.EventHandler(this.FormFindOption_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_Find_Next;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtn_Find;
        private System.Windows.Forms.ToolStripButton tsbtn_replace;
        private System.Windows.Forms.ComboBox txt_search_text;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chk_match_whole_word;
        private System.Windows.Forms.CheckBox chk_match_case;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslbl_find_text;
        private System.Windows.Forms.ComboBox txt_replace_text;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_Replace_All;
        private System.Windows.Forms.Button btn_Replace_Next;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbl_res;
    }
}